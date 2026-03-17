using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("AutoMapper.Extensions.ExpressionMapping.UnitTests, PublicKey=0024000004800000940000000602000000240000525341310004000001000100472eaa88b0b938afd1af240d58859cfea02891406705969564edf924cf3eb9920ac169f58fd63cf1b3eae3ad0ce4344cf757dff3b9f931a7afc70084ae161896bd7eaf634b88eb50200444440113e5510d22a80ac26d8057937ac23dcb8dbd4d86ea738e604f9628bc9939717ef93b9ef6250266ecff292a3a6cf5265a5ef3e6")]

namespace AutoMapper.Extensions.ExpressionMapping
{
    internal static class AnonymousTypeFactory
    {
        private static int classCount;

        public static Type CreateAnonymousType(IEnumerable<MemberInfo> memberDetails)
            => CreateAnonymousType(memberDetails.ToDictionary(key => key.Name, element => element.GetMemberType()));

        public static Type CreateAnonymousType(IDictionary<string, Type> memberDetails)
        {
            AssemblyName dynamicAssemblyName = new("TempAssembly.AutoMapper.Extensions.ExpressionMapping");
            AssemblyBuilder dynamicAssembly = AssemblyBuilder.DefineDynamicAssembly(dynamicAssemblyName, AssemblyBuilderAccess.Run);
            ModuleBuilder dynamicModule = dynamicAssembly.DefineDynamicModule("TempAssembly.AutoMapper.Extensions.ExpressionMapping");
            TypeBuilder typeBuilder = dynamicModule.DefineType(GetAnonymousTypeName(), TypeAttributes.Public);
            MethodAttributes getSetAttr = MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig;

            var builders = memberDetails.Select
            (
                info =>
                {
                    Type memberType = info.Value;
                    string memberName = info.Key;
                    return new
                    {
                        FieldBuilder = typeBuilder.DefineField(string.Concat("_", memberName), memberType, FieldAttributes.Private),
                        PropertyBuilder = typeBuilder.DefineProperty(memberName, PropertyAttributes.HasDefault, memberType, null),
                        GetMethodBuilder = typeBuilder.DefineMethod(string.Concat("get_", memberName), getSetAttr, memberType, Type.EmptyTypes),
                        SetMethodBuilder = typeBuilder.DefineMethod(string.Concat("set_", memberName), getSetAttr, null, [memberType])
                    };
                }
            );

            builders.ToList().ForEach(builder =>
            {
                ILGenerator getMethodIL = builder.GetMethodBuilder.GetILGenerator();
                getMethodIL.Emit(OpCodes.Ldarg_0);
                getMethodIL.Emit(OpCodes.Ldfld, builder.FieldBuilder);
                getMethodIL.Emit(OpCodes.Ret);

                ILGenerator setMethodIL = builder.SetMethodBuilder.GetILGenerator();
                setMethodIL.Emit(OpCodes.Ldarg_0);
                setMethodIL.Emit(OpCodes.Ldarg_1);
                setMethodIL.Emit(OpCodes.Stfld, builder.FieldBuilder);
                setMethodIL.Emit(OpCodes.Ret);

                builder.PropertyBuilder.SetGetMethod(builder.GetMethodBuilder);
                builder.PropertyBuilder.SetSetMethod(builder.SetMethodBuilder);
            });

            return typeBuilder.CreateTypeInfo().AsType();
        }

        private static string GetAnonymousTypeName()
            => $"AnonymousType{++classCount}";
    }
}
