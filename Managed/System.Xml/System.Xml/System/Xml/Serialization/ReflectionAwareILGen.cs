using System;
using System.Globalization;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x0200035F RID: 863
	internal class ReflectionAwareILGen
	{
		// Token: 0x06002329 RID: 9001 RVA: 0x000020FD File Offset: 0x000002FD
		internal ReflectionAwareILGen()
		{
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x000DA360 File Offset: 0x000D8560
		internal void WriteReflectionInit(TypeScope scope)
		{
			foreach (object obj in scope.Types)
			{
				Type type = (Type)obj;
				scope.GetTypeDesc(type);
			}
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x000DA3BC File Offset: 0x000D85BC
		internal void ILGenForEnumLongValue(CodeGenerator ilg, string variable)
		{
			ArgBuilder arg = ilg.GetArg(variable);
			ilg.Ldarg(arg);
			ilg.ConvertValue(arg.ArgType, typeof(long));
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x000DA3EE File Offset: 0x000D85EE
		internal string GetStringForTypeof(string typeFullName)
		{
			return "typeof(" + typeFullName + ")";
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000DA400 File Offset: 0x000D8600
		internal string GetStringForMember(string obj, string memberName, TypeDesc typeDesc)
		{
			return obj + ".@" + memberName;
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x000DA40E File Offset: 0x000D860E
		internal SourceInfo GetSourceForMember(string obj, MemberMapping member, TypeDesc typeDesc, CodeGenerator ilg)
		{
			return this.GetSourceForMember(obj, member, member.MemberInfo, typeDesc, ilg);
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000DA421 File Offset: 0x000D8621
		internal SourceInfo GetSourceForMember(string obj, MemberMapping member, MemberInfo memberInfo, TypeDesc typeDesc, CodeGenerator ilg)
		{
			return new SourceInfo(this.GetStringForMember(obj, member.Name, typeDesc), obj, memberInfo, member.TypeDesc.Type, ilg);
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000DA446 File Offset: 0x000D8646
		internal void ILGenForEnumMember(CodeGenerator ilg, Type type, string memberName)
		{
			ilg.Ldc(Enum.Parse(type, memberName, false));
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000DA456 File Offset: 0x000D8656
		internal string GetStringForArrayMember(string arrayName, string subscript, TypeDesc arrayTypeDesc)
		{
			return arrayName + "[" + subscript + "]";
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x000DA469 File Offset: 0x000D8669
		internal string GetStringForMethod(string obj, string typeFullName, string memberName)
		{
			return obj + "." + memberName + "(";
		}

		// Token: 0x06002333 RID: 9011 RVA: 0x000DA47C File Offset: 0x000D867C
		internal void ILGenForCreateInstance(CodeGenerator ilg, Type type, bool ctorInaccessible, bool cast)
		{
			if (ctorInaccessible)
			{
				this.ILGenForCreateInstance(ilg, type, cast ? type : null, ctorInaccessible);
				return;
			}
			ConstructorInfo constructor = type.GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			if (constructor != null)
			{
				ilg.New(constructor);
				return;
			}
			LocalBuilder tempLocal = ilg.GetTempLocal(type);
			ilg.Ldloca(tempLocal);
			ilg.InitObj(type);
			ilg.Ldloc(tempLocal);
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x000DA4E0 File Offset: 0x000D86E0
		internal void ILGenForCreateInstance(CodeGenerator ilg, Type type, Type cast, bool nonPublic)
		{
			if (type == typeof(DBNull))
			{
				FieldInfo field = typeof(DBNull).GetField("Value", CodeGenerator.StaticBindingFlags);
				ilg.LoadMember(field);
				return;
			}
			if (type.FullName == "System.Xml.Linq.XElement")
			{
				Type type2 = type.Assembly.GetType("System.Xml.Linq.XName");
				if (type2 != null)
				{
					MethodInfo method = type2.GetMethod("op_Implicit", CodeGenerator.StaticBindingFlags, null, new Type[] { typeof(string) }, null);
					ConstructorInfo constructor = type.GetConstructor(CodeGenerator.InstanceBindingFlags, null, new Type[] { type2 }, null);
					if (method != null && constructor != null)
					{
						ilg.Ldstr("default");
						ilg.Call(method);
						ilg.New(constructor);
						return;
					}
				}
			}
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.CreateInstance;
			if (nonPublic)
			{
				bindingFlags |= BindingFlags.NonPublic;
			}
			MethodInfo method2 = typeof(Activator).GetMethod("CreateInstance", CodeGenerator.StaticBindingFlags, null, new Type[]
			{
				typeof(Type),
				typeof(BindingFlags),
				typeof(Binder),
				typeof(object[]),
				typeof(CultureInfo)
			}, null);
			ilg.Ldc(type);
			ilg.Load((int)bindingFlags);
			ilg.Load(null);
			ilg.NewArray(typeof(object), 0);
			ilg.Load(null);
			ilg.Call(method2);
			if (cast != null)
			{
				ilg.ConvertValue(method2.ReturnType, cast);
			}
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x000DA688 File Offset: 0x000D8888
		internal void WriteLocalDecl(string variableName, SourceInfo initValue)
		{
			Type type = initValue.Type;
			LocalBuilder localBuilder = initValue.ILG.DeclareOrGetLocal(type, variableName);
			if (initValue.Source != null)
			{
				if (initValue == "null")
				{
					initValue.ILG.Load(null);
				}
				else if (initValue.Arg.StartsWith("o.@", StringComparison.Ordinal))
				{
					initValue.ILG.LoadMember(initValue.ILG.GetLocal("o"), initValue.MemberInfo);
				}
				else if (initValue.Source.EndsWith("]", StringComparison.Ordinal))
				{
					initValue.Load(initValue.Type);
				}
				else if (initValue.Source == "fixup.Source" || initValue.Source == "e.Current")
				{
					string[] array = initValue.Source.Split(new char[] { '.' });
					object variable = initValue.ILG.GetVariable(array[0]);
					PropertyInfo property = initValue.ILG.GetVariableType(variable).GetProperty(array[1]);
					initValue.ILG.LoadMember(variable, property);
					initValue.ILG.ConvertValue(property.PropertyType, localBuilder.LocalType);
				}
				else
				{
					object variable2 = initValue.ILG.GetVariable(initValue.Arg);
					initValue.ILG.Load(variable2);
					initValue.ILG.ConvertValue(initValue.ILG.GetVariableType(variable2), localBuilder.LocalType);
				}
				initValue.ILG.Stloc(localBuilder);
			}
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x000DA80C File Offset: 0x000D8A0C
		internal void WriteCreateInstance(string source, bool ctorInaccessible, Type type, CodeGenerator ilg)
		{
			LocalBuilder localBuilder = ilg.DeclareOrGetLocal(type, source);
			this.ILGenForCreateInstance(ilg, type, ctorInaccessible, ctorInaccessible);
			ilg.Stloc(localBuilder);
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x000DA836 File Offset: 0x000D8A36
		internal void WriteInstanceOf(SourceInfo source, Type type, CodeGenerator ilg)
		{
			source.Load(typeof(object));
			ilg.IsInst(type);
			ilg.Load(null);
			ilg.Cne();
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x000DA85C File Offset: 0x000D8A5C
		internal void WriteArrayLocalDecl(string typeName, string variableName, SourceInfo initValue, TypeDesc arrayTypeDesc)
		{
			Type type = ((typeName == arrayTypeDesc.CSharpName) ? arrayTypeDesc.Type : arrayTypeDesc.Type.MakeArrayType());
			LocalBuilder localBuilder = initValue.ILG.DeclareOrGetLocal(type, variableName);
			if (initValue != null)
			{
				initValue.Load(localBuilder.LocalType);
				initValue.ILG.Stloc(localBuilder);
			}
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x000DA8BD File Offset: 0x000D8ABD
		internal void WriteTypeCompare(string variable, Type type, CodeGenerator ilg)
		{
			ilg.Ldloc(typeof(Type), variable);
			ilg.Ldc(type);
			ilg.Ceq();
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x000DA8BD File Offset: 0x000D8ABD
		internal void WriteArrayTypeCompare(string variable, Type arrayType, CodeGenerator ilg)
		{
			ilg.Ldloc(typeof(Type), variable);
			ilg.Ldc(arrayType);
			ilg.Ceq();
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x000DA8DD File Offset: 0x000D8ADD
		internal static string GetQuotedCSharpString(IndentedWriter notUsed, string value)
		{
			if (value == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("@\"");
			stringBuilder.Append(ReflectionAwareILGen.GetCSharpString(value));
			stringBuilder.Append("\"");
			return stringBuilder.ToString();
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x000DA914 File Offset: 0x000D8B14
		internal static string GetCSharpString(string value)
		{
			if (value == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in value)
			{
				if (c < ' ')
				{
					if (c == '\r')
					{
						stringBuilder.Append("\\r");
					}
					else if (c == '\n')
					{
						stringBuilder.Append("\\n");
					}
					else if (c == '\t')
					{
						stringBuilder.Append("\\t");
					}
					else
					{
						byte b = (byte)c;
						stringBuilder.Append("\\x");
						stringBuilder.Append("0123456789ABCDEF"[b >> 4]);
						stringBuilder.Append("0123456789ABCDEF"[(int)(b & 15)]);
					}
				}
				else if (c == '"')
				{
					stringBuilder.Append("\"\"");
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04001855 RID: 6229
		private const string hexDigits = "0123456789ABCDEF";

		// Token: 0x04001856 RID: 6230
		private const string arrayMemberKey = "0";
	}
}
