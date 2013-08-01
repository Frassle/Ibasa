using Ibasa.Numerics;
using Ibasa.OpenCL;
using Ibasa.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static string source = @"
__kernel void vector_add_gpu (__global const float* src_a,
    __global const float* src_b,
    __global float* res,
    const int num)
{
    const int idx = get_global_id(0);
    if (idx < num)
        res[idx] = src_a[idx] + src_b[idx];
}";

        static void ContextCallback(string error, byte[] info, object user_data)
        {
            Console.WriteLine("Error: {0}", error);
        }

        static void ProgramCallback(Ibasa.OpenCL.Program program, object user_data)
        {
            Console.WriteLine("Program built.");

            var device = program.Devices[0];

            Console.WriteLine(device.Name);

            var info = program.GetBuildInfo(device);

            Console.WriteLine(info.BuildOptions);
            Console.WriteLine(info.BuildStatus);
            Console.WriteLine(info.Log);
        }

        struct Pair { public int A; public int B; }

        static string gl_vertex_shader = @"
#version 330

layout(location = 0) in vec4 position;
layout (location = 1) in vec4 color;

smooth out vec4 theColor;

uniform vec2 offset;
uniform mat4 perspectiveMatrix;

void main()
{
    vec4 totalOffset = vec4(offset.x, offset.y, 0.0, 0.0);
    gl_Position = perspectiveMatrix * (position + totalOffset);
    theColor = color;
}";

        static string gl_frag_shader = @"
#version 330

smooth in vec4 theColor;

out vec4 outputColor;

void main()
{
    outputColor = theColor;
}";

        class Window : System.Windows.Forms.Form
        {
            public Window()
            {
                SetStyle(System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
                SetStyle(System.Windows.Forms.ControlStyles.UserPaint, true);
                ClientSize = new System.Drawing.Size(512, 512);

                var width = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;

                StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                DesktopLocation = new System.Drawing.Point(width - ClientRectangle.Width, 0);
            }

            protected override void OnResize(EventArgs e)
            {
                if (Ibasa.OpenGL.Gl.Current != null)
                {
                    Ibasa.OpenGL.Context.Rasterization.Viewport =
                        new Ibasa.Numerics.Geometry.Rectanglei(0, 0, ClientSize.Width, ClientSize.Height);

                    Ibasa.OpenGL.Context.CurrentProgram = theProgram;
                    Ibasa.OpenGL.Context.SetUniform(perspectiveMatrixLocation,
                        Ibasa.Numerics.Matrix4x4f.PerspectiveFov(
                        Ibasa.Numerics.Handedness.Left,
                        (float)Ibasa.Numerics.Functions.ToRadians(70),
                        ClientSize.Width / (float)ClientSize.Height,
                        1,
                        4));
                    Ibasa.OpenGL.Context.CurrentProgram = Ibasa.OpenGL.Program.Null;
                }
                base.OnResize(e);
            }
        }

        static System.Diagnostics.Stopwatch Stopwatch = System.Diagnostics.Stopwatch.StartNew();

        static Ibasa.OpenGL.Program theProgram;
        static int perspectiveMatrixLocation;

        static void ComputePositionOffsets(ref float offsetX, ref float offsetY)
        {
            float time = (float)Stopwatch.Elapsed.TotalSeconds;

            var loop = time % 5.0;
            var scale = (float)(Math.PI * 2.0 / 5.0);

            offsetX = (float)Math.Cos(loop * scale) * 0.5f;
            offsetY = (float)Math.Sin(loop * scale) * 0.5f;
        }

        [STAThread]
        static void Main(string[] args)
        {
            var console = Ibasa.Platform.Windows.Wincon.GetConsoleWindow();

            Ibasa.Platform.Windows.Windef.RECT rect;
            Ibasa.Platform.Windows.Winuser.GetWindowRect(console, out rect);
            var screenwidth = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width;
            var consolewidth = rect.right - rect.left;

            Ibasa.Platform.Windows.Winuser.SetWindowPos(console, IntPtr.Zero,
                screenwidth - consolewidth, 0, consolewidth, rect.bottom - rect.top, 0);

            var form = new Window();
            var graphics = form.CreateGraphics();
            var hdc = graphics.GetHdc();

            unsafe
            {
                Ibasa.Platform.Windows.Wgl.PixelFormatDescriptor pfd = new Ibasa.Platform.Windows.Wgl.PixelFormatDescriptor()
                {
                    nSize = (ushort)Ibasa.Interop.Memory.SizeOf<Ibasa.Platform.Windows.Wgl.PixelFormatDescriptor>(),
                    nVersion = 1,
                    dwFlags = Ibasa.Platform.Windows.Wgl.PFD_DRAW_TO_WINDOW |
                        Ibasa.Platform.Windows.Wgl.PFD_SUPPORT_OPENGL |
                        Ibasa.Platform.Windows.Wgl.PFD_DOUBLEBUFFER,
                    iPixelType = Ibasa.Platform.Windows.Wgl.PFD_TYPE_RGBA,
                    cColorBits = 24,
                    cDepthBits = 16,
                    iLayerType = Ibasa.Platform.Windows.Wgl.PFD_MAIN_PLANE,
                };

                int iPixelFormat = Ibasa.Platform.Windows.Wgl.ChoosePixelFormat(hdc, &pfd);

                Ibasa.Platform.Windows.Wgl.SetPixelFormat(hdc, iPixelFormat, &pfd);

                var hglrc = Ibasa.Platform.Windows.Wgl.CreateContext(hdc);
                Ibasa.Platform.Windows.Wgl.MakeCurrent(hdc, hglrc);

                var opengldll = Ibasa.Platform.Windows.Winbase.GetModuleHandle("opengl32");

                Ibasa.OpenGL.Gl.Current = new Ibasa.OpenGL.Gl(proc =>
                    {
                        var ptr = Ibasa.Platform.Windows.Wgl.GetProcAddress(proc);
                        if (ptr == IntPtr.Zero)
                        {
                            ptr = Ibasa.Platform.Windows.Winbase.GetProcAddress(opengldll, proc);
                        }
                        return ptr;
                    });

                var vao = Ibasa.OpenGL.VertexArray.Create();
                Ibasa.OpenGL.Context.VertexArray = vao;

                Ibasa.OpenGL.Context.Rasterization.DepthTest = true;
                Ibasa.OpenGL.Context.Rasterization.DepthMask = true;
                Ibasa.OpenGL.Context.Rasterization.DepthFunction = Ibasa.OpenGL.Comparison.LessEqual;

                Ibasa.OpenGL.Context.Rasterization.CullFace = true;
                Ibasa.OpenGL.Context.Rasterization.CullFaceMode = Ibasa.OpenGL.Face.Back;
                Ibasa.OpenGL.Context.Rasterization.FrontCounterClockwise = false;

                var vertexPositions = new Ibasa.Interop.UnmanagedArray<Ibasa.Numerics.Vector4f>(
                    new Ibasa.Numerics.Vector4f[] {
                        new Ibasa.Numerics.Vector4f(0.0f, 0.5f, 1.0f, 1.0f), Ibasa.Numerics.Colorf.Blue,
                        new Ibasa.Numerics.Vector4f(0.5f, -0.366f, 1.0f, 1.0f), Ibasa.Numerics.Colorf.Blue,
                        new Ibasa.Numerics.Vector4f(-0.5f, -0.366f, 1.0f, 1.0f), Ibasa.Numerics.Colorf.Blue,
                        
                        new Ibasa.Numerics.Vector4f(0.0f, 0.5f, 2.0f, 1.0f), Ibasa.Numerics.Colorf.Red,
                        new Ibasa.Numerics.Vector4f(0.5f, -0.366f, 2.0f, 1.0f), Ibasa.Numerics.Colorf.Red,
                        new Ibasa.Numerics.Vector4f(-0.5f, -0.366f, 2.0f, 1.0f), Ibasa.Numerics.Colorf.Red,
                        
                        new Ibasa.Numerics.Vector4f(0.0f, 0.5f, 3.0f, 1.0f), Ibasa.Numerics.Colorf.Green,
                        new Ibasa.Numerics.Vector4f(0.5f, -0.366f, 3.0f, 1.0f), Ibasa.Numerics.Colorf.Green,
                        new Ibasa.Numerics.Vector4f(-0.5f, -0.366f, 3.0f, 1.0f), Ibasa.Numerics.Colorf.Green,
                    });

                var positionBufferObject = Ibasa.OpenGL.Buffer.Create();

                Ibasa.OpenGL.Context.ArrayBuffer.Buffer = positionBufferObject;
                Ibasa.OpenGL.Context.ArrayBuffer.BufferData(
                    vertexPositions.Size, vertexPositions.Pointer, Ibasa.OpenGL.Usage.StaticDraw);
                Ibasa.OpenGL.Context.ArrayBuffer.Buffer = Ibasa.OpenGL.Buffer.Null;

                var indices = new Ibasa.Interop.UnmanagedArray<ushort>(
                    new ushort[] {
                        0, 1, 2,
                        3, 4, 5,
                        6, 7, 8,
                    });

                var indexBufferObject = Ibasa.OpenGL.Buffer.Create();

                Ibasa.OpenGL.Context.ElementArrayBuffer.Buffer = indexBufferObject;
                Ibasa.OpenGL.Context.ElementArrayBuffer.BufferData(
                    indices.Size, indices.Pointer, Ibasa.OpenGL.Usage.StaticDraw);
                Ibasa.OpenGL.Context.ElementArrayBuffer.Buffer = Ibasa.OpenGL.Buffer.Null;

                Console.WriteLine(indexBufferObject.Label);

                var vertex_shader = Ibasa.OpenGL.Shader.Create(Ibasa.OpenGL.ShaderType.Vertex);
                vertex_shader.Source = gl_vertex_shader;
                if (!vertex_shader.Compile())
                {
                    Console.WriteLine(vertex_shader.InfoLog);
                }

                var fragment_shader = Ibasa.OpenGL.Shader.Create(Ibasa.OpenGL.ShaderType.Fragment);
                fragment_shader.Source = gl_frag_shader;
                if (!fragment_shader.Compile())
                {
                    Console.WriteLine(fragment_shader.InfoLog);
                }

                theProgram = Ibasa.OpenGL.Program.Create();
                theProgram.AttachShader(vertex_shader);
                theProgram.AttachShader(fragment_shader);

                if (!theProgram.Link())
                {
                    Console.WriteLine(theProgram.InfoLog);
                }

                theProgram.DetachShader(vertex_shader);
                theProgram.DetachShader(fragment_shader);

                var offsetLocation = theProgram.GetUniformLocation("offset");
                perspectiveMatrixLocation = theProgram.GetUniformLocation("perspectiveMatrix");

                float offsetX, offsetY;
                offsetX = offsetY = 0;

                form.Show();
                while (form.Visible)
                {
                    Ibasa.Platform.Windows.Winuser.MSG msg;
                    while (Ibasa.Platform.Windows.Winuser.PeekMessage(&msg, form.Handle, 0, 0, 0))
                    {
                        Ibasa.Platform.Windows.Winuser.GetMessage(&msg, form.Handle, 0, 0);
                        Ibasa.Platform.Windows.Winuser.TranslateMessage(&msg);
                        Ibasa.Platform.Windows.Winuser.DispatchMessage(&msg);
                    }

                    //glClearColor(0.0f, 0.0f, 0.0f, 0.0f);
                    //glClear(GL_COLOR_BUFFER_BIT);

                    //glUseProgram(theProgram);

                    //glBindBuffer(GL_ARRAY_BUFFER, positionBufferObject);
                    //glEnableVertexAttribArray(0);
                    //glVertexAttribPointer(0, 4, GL_FLOAT, GL_FALSE, 0, 0);

                    //glDrawArrays(GL_TRIANGLES, 0, 3);

                    //glDisableVertexAttribArray(0);
                    //glUseProgram(0);

                    ComputePositionOffsets(ref offsetX, ref offsetY);

                    Ibasa.OpenGL.Context.ClearColor = Ibasa.Numerics.Colorf.CornflowerBlue;
                    Ibasa.OpenGL.Context.Clear(Ibasa.OpenGL.ClearFlags.Color | Ibasa.OpenGL.ClearFlags.Depth);

                    Ibasa.OpenGL.Context.CurrentProgram = theProgram;

                    Ibasa.OpenGL.Context.SetUniform(offsetLocation, offsetX, offsetY);

                    Ibasa.OpenGL.Context.ElementArrayBuffer.Buffer = indexBufferObject;
                    Ibasa.OpenGL.Context.ArrayBuffer.Buffer = positionBufferObject;
                    Ibasa.OpenGL.Context.VertexAttributeArrays[0].Enabled = true;
                    Ibasa.OpenGL.Context.VertexAttributeArrays[1].Enabled = true;
                    Ibasa.OpenGL.Context.VertexAttributeArrays[0].VertexAttributePointer(
                        4, Ibasa.OpenGL.DataType.Float, false, 32, 0);
                    Ibasa.OpenGL.Context.VertexAttributeArrays[1].VertexAttributePointer(
                        4, Ibasa.OpenGL.DataType.Float, false, 32, 16);

                    Ibasa.OpenGL.Context.DrawElements(
                        Ibasa.OpenGL.PrimitiveTopology.TriangleList, 9,
                        Ibasa.OpenGL.DataType.UnsignedShort, 0);

                    Ibasa.OpenGL.Context.VertexAttributeArrays[0].Enabled = false;
                    Ibasa.OpenGL.Context.VertexAttributeArrays[1].Enabled = false;
                    Ibasa.OpenGL.Context.ArrayBuffer.Buffer = Ibasa.OpenGL.Buffer.Null;
                    Ibasa.OpenGL.Context.CurrentProgram = Ibasa.OpenGL.Program.Null;

                    Ibasa.Platform.Windows.Wgl.SwapBuffers(hdc);
                }

                Ibasa.Platform.Windows.Wgl.MakeCurrent(IntPtr.Zero, IntPtr.Zero);
                Ibasa.Platform.Windows.Wgl.DeleteContext(hdc);
            }

            graphics.ReleaseHdc();
            graphics.Dispose();
            form.Close();

            Game game = new Game();

            game.Initalize();
            while (true)
            {
                game.Tick();
            }


            //unsafe
            //{
            //    long longs = 1337;
            //    int* ints = stackalloc int[2];

            //    Ibasa.Interop.Memory.Copy(&longs, ints, 8);

            //    Pair pair = new Pair();
            //    Ibasa.Interop.Memory.Read(ints, out pair);

            //    Console.WriteLine("{0}:{1}", ints[0], pair.A);
            //    Console.WriteLine("{0}:{1}", ints[1], pair.B);

            //    pair.A = 8008;
            //    pair.B = 1337;

            //    Ibasa.Interop.Memory.Write(ints, ref pair);

            //    Console.WriteLine("{0}:{1}", ints[0], pair.A);
            //    Console.WriteLine("{0}:{1}", ints[1], pair.B);

            //    Console.WriteLine("Sizeof(Pair) = {0}", Ibasa.Interop.Memory.SizeOf<Pair>());
            //}

            //var platform = Platform.GetPlatforms()[0];
            //var device = platform.GetDevices(DeviceType.All)[0];

            //Console.WriteLine(platform.Name);
            //Console.WriteLine(platform.Vendor);
            //Console.WriteLine(platform.Profile);
            //Console.WriteLine(platform.Version);
            //Console.WriteLine(string.Join(", ", platform.Extensions));

            //Console.WriteLine(device.Name);
            //Console.WriteLine(device.Profile);
            //Console.WriteLine(device.Vendor);
            //Console.WriteLine(device.VendorID);
            //Console.WriteLine(device.Version);
            //Console.WriteLine(device.DriverVersion);
            //Console.WriteLine(device.DoubleFloatingPointCapability);
            //Console.WriteLine(device.SingleFloatingPointCapability);
            //Console.WriteLine(device.Type);

            //var context = new Context(null, new Device[] { device }, ContextCallback, null);

            //var program = new Ibasa.OpenCL.Program(context, source);
            //program.BuildProgram(new Device[] { device }, "", ProgramCallback, null);

            //Console.WriteLine(program.Source);

            //while (program.GetBuildInfo(device).BuildStatus == BuildStatus.InProgress)
            //{
            //}

            //var kernel = new Kernel(program, "vector_add_gpu");

            //Console.WriteLine(kernel.FunctionName);
            //Console.WriteLine(kernel.ArgumentCount);

            //Ibasa.Interop.UnmanagedArray<float> srca = new Ibasa.Interop.UnmanagedArray<float>(50);
            //Ibasa.Interop.UnmanagedArray<float> srcb = new Ibasa.Interop.UnmanagedArray<float>(50);
            //Ibasa.Interop.UnmanagedArray<float> dest = new Ibasa.Interop.UnmanagedArray<float>(50);

            //for (int i = 0; i < 50; ++i)
            //{
            //    srca[i] = i;
            //    srcb[i] = 50;
            //}

            //var buffera = new Ibasa.OpenCL.Buffer(
            //    context, MemoryFlags.ReadOnly | MemoryFlags.CopyHostPtr, srca.Size, srca.Pointer);
            //var bufferb = new Ibasa.OpenCL.Buffer(
            //    context, MemoryFlags.ReadOnly | MemoryFlags.CopyHostPtr, srcb.Size, srcb.Pointer);
            //var bufferd = new Ibasa.OpenCL.Buffer(
            //    context, MemoryFlags.WriteOnly, dest.Size);

            //kernel.SetArgument(0, buffera);
            //kernel.SetArgument(1, bufferb);
            //kernel.SetArgument(2, bufferd);
            //kernel.SetArgument(3, 50);

            //var queue = new CommandQueue(context, device, CommandQueueProperties.ProfilingEnable);

            //var eventk = queue.EnqueueKernel(kernel, null, new ulong[] { 50 }, null, null);

            //eventk.SetCallback((eve, status, obj) => Console.WriteLine(status), null);
            
            //queue.EnqueueReadBuffer(bufferd, true, 0, dest.Size, dest.Pointer, new Event[] { eventk });
            
            //for (int i = 0; i < 50; ++i)
            //{
            //    Console.WriteLine(dest[i]);
            //}

            //var time = eventk.TimeEnded - eventk.TimeStarted;

            //Console.WriteLine("Took {0}", time);

            //Console.ReadLine();
        }
    }
}
