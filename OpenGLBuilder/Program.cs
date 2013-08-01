using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenGLBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = args.Length >= 1 ? args[0] :
                @"D:\VisualStudio\Projects\Ibasa\OpenGL\bin\Debug\Ibasa.OpenGL.dll";
            try
            {
                AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(path);

                Console.WriteLine("Loaded {0}.", path);

                GenerateFunctionCalls(assembly.MainModule);

                Console.WriteLine("Saving new dll.");

                assembly.Write(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("OpenGLBuilder failed with: {0}", e);
            }
        }

        private static void GenerateFunctionCalls(ModuleDefinition module)
        {
            var type = module.Types.First(t => t.Name == "Gl");

            foreach (var method in type.Methods)
            {
                if (method.Attributes.HasFlag(MethodAttributes.SpecialName))
                    continue;

                GenerateFunctionCall(module, method);
            }
        }

        private static void GenerateFunctionCall(ModuleDefinition module, MethodDefinition method)
        {
            Console.WriteLine("Modifing {0}", method.Name);

            method.Body.Instructions.Clear();

            var gen = method.Body.GetILProcessor();

            int paramaters = method.Parameters.Count;

            for (int arg = 0; arg < paramaters; ++arg)
            {
                if (arg == 0)
                {
                    gen.Emit(OpCodes.Ldarg_0);
                }
                else if (arg == 1)
                {
                    gen.Emit(OpCodes.Ldarg_1);
                }
                else if (arg == 2)
                {
                    gen.Emit(OpCodes.Ldarg_2);
                }
                else if (arg == 3)
                {
                    gen.Emit(OpCodes.Ldarg_3);
                }
                else if (arg <= byte.MaxValue)
                {
                    gen.Emit(OpCodes.Ldarg_S, (byte)arg);
                }
                else
                {
                    gen.Emit(OpCodes.Ldarg, (ushort)arg);
                }
            }

            var intptr = module.Import(typeof(IntPtr));

            gen.Emit(OpCodes.Ldsfld,
                new FieldReference("context", method.DeclaringType, method.DeclaringType));
            gen.Emit(OpCodes.Ldfld,
                new FieldReference("gl" + method.Name, intptr, method.DeclaringType));

            var topointer = module.Import(typeof(IntPtr).GetMethod("ToPointer"));

            var callsite = new CallSite(method.ReturnType);
            callsite.CallingConvention = MethodCallingConvention.StdCall;
            foreach (var param in method.Parameters)
            {
                var newParam = new ParameterDefinition(param.ParameterType);
                callsite.Parameters.Add(newParam);
            }

            gen.Emit(OpCodes.Calli, callsite);

            gen.Emit(OpCodes.Ret);
        }
    }
}
