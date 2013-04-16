using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteropBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var path = args.Length >= 1 ? args[0] : "Ibasa.Interop.dll";
                AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(path);

                Console.WriteLine("Loaded {0}.", path);

                GenerateMemoryType(assembly.MainModule);

                Console.WriteLine("Saving new dll.");

                assembly.Write(path);
            }
            catch (Exception e)
            {
                Console.WriteLine("InteropBuilder failed with: {0}", e);
            }
        }

        private static void GenerateMemoryType(ModuleDefinition module)
        {
            var type = module.Types.First(t => t.Name == "Memory");
            type.BaseType = module.Import(typeof(object));

            GenerateCopy(module, type);
            GenerateFill(module, type);
            GenerateWrite(module, type);
            GenerateRead(module, type);
            GenerateSizeOf(module, type);
        }

        private static void GenerateCopy(ModuleDefinition module, TypeDefinition type)
        {
            var method = type.Methods.First(m =>
                m.Name == "Copy" &&
                m.Parameters[0].ParameterType.IsPointer);

            method.Body.Instructions.Clear();

            var gen = method.Body.GetILProcessor();

            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Unaligned, (byte)1);
            gen.Emit(OpCodes.Cpblk);
            gen.Emit(OpCodes.Ret);
        }

        private static void GenerateFill(ModuleDefinition module, TypeDefinition type)
        {
            var method = type.Methods.First(m =>
                m.Name == "Fill" &&
                m.Parameters[0].ParameterType.IsPointer);

            method.Body.Instructions.Clear();

            var gen = method.Body.GetILProcessor();

            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Ldarg_2);
            gen.Emit(OpCodes.Unaligned, (byte)1);
            gen.Emit(OpCodes.Initblk);
            gen.Emit(OpCodes.Ret);
        }

        private static void GenerateWrite(ModuleDefinition module, TypeDefinition type)
        {
            var method = type.Methods.First(m =>
                m.Name == "Write" &&
                m.Parameters[0].ParameterType.IsPointer);

            var generic_parameter = method.GenericParameters[0];

            method.Body.Instructions.Clear();
            method.Body.InitLocals = true;

            var gen = method.Body.GetILProcessor();

            method.Body.Variables.Add(new VariableDefinition(module.Import(typeof(int))));
            method.Body.Variables.Add(new VariableDefinition(new PinnedType(new ByReferenceType(generic_parameter))));

            gen.Emit(OpCodes.Ldarg_0);

            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Stloc_1);

            gen.Emit(OpCodes.Ldloc_1);

            gen.Emit(OpCodes.Sizeof, generic_parameter);
            gen.Emit(OpCodes.Conv_I4);
            gen.Emit(OpCodes.Stloc_0);

            gen.Emit(OpCodes.Ldloc_0);

            gen.Emit(OpCodes.Unaligned, (byte)1);
            gen.Emit(OpCodes.Cpblk);

            gen.Emit(OpCodes.Ldloc_0);
            gen.Emit(OpCodes.Conv_I);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Add);

            gen.Emit(OpCodes.Ret);
        }

        private static void GenerateRead(ModuleDefinition module, TypeDefinition type)
        {
            var method = type.Methods.First(m =>
                m.Name == "Read" &&
                m.Parameters[0].ParameterType.IsPointer);

            var generic_parameter = method.GenericParameters[0];

            method.Body.Instructions.Clear();
            method.Body.InitLocals = true;

            var gen = method.Body.GetILProcessor();

            method.Body.Variables.Add(new VariableDefinition(module.Import(typeof(int))));
            method.Body.Variables.Add(new VariableDefinition(new PinnedType(new ByReferenceType(generic_parameter))));
            
            gen.Emit(OpCodes.Ldarg_1);
            gen.Emit(OpCodes.Stloc_1);

            gen.Emit(OpCodes.Ldloc_1);
            gen.Emit(OpCodes.Ldarg_0);

            gen.Emit(OpCodes.Sizeof, generic_parameter);
            gen.Emit(OpCodes.Conv_I4);
            gen.Emit(OpCodes.Stloc_0);

            gen.Emit(OpCodes.Ldloc_0);

            gen.Emit(OpCodes.Unaligned, (byte)1);
            gen.Emit(OpCodes.Cpblk);

            gen.Emit(OpCodes.Ldloc_0);
            gen.Emit(OpCodes.Conv_I);
            gen.Emit(OpCodes.Ldarg_0);
            gen.Emit(OpCodes.Add);

            gen.Emit(OpCodes.Ret);
        }

        private static void GenerateSizeOf(ModuleDefinition module, TypeDefinition type)
        {
            var method = type.Methods.First(m =>
                m.Name == "SizeOf");

            var generic_parameter = method.GenericParameters[0];

            method.Body.Instructions.Clear();

            var gen = method.Body.GetILProcessor();

            gen.Emit(OpCodes.Sizeof, generic_parameter);
            gen.Emit(OpCodes.Conv_I4);
            gen.Emit(OpCodes.Ret);
        }
    }
}
