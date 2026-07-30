using System;
using System.Collections;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace System.Xml.Serialization
{
	// Token: 0x02000318 RID: 792
	internal class SourceInfo
	{
		// Token: 0x06001DC3 RID: 7619 RVA: 0x000A3DFB File Offset: 0x000A1FFB
		public SourceInfo(string source, string arg, MemberInfo memberInfo, Type type, CodeGenerator ilg)
		{
			this.Source = source;
			this.Arg = arg ?? source;
			this.MemberInfo = memberInfo;
			this.Type = type;
			this.ILG = ilg;
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x000A3E30 File Offset: 0x000A2030
		public SourceInfo CastTo(TypeDesc td)
		{
			return new SourceInfo(string.Concat(new string[] { "((", td.CSharpName, ")", this.Source, ")" }), this.Arg, this.MemberInfo, td.Type, this.ILG);
		}

		// Token: 0x06001DC5 RID: 7621 RVA: 0x000A3E8F File Offset: 0x000A208F
		public void LoadAddress(Type elementType)
		{
			this.InternalLoad(elementType, true);
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x000A3E99 File Offset: 0x000A2099
		public void Load(Type elementType)
		{
			this.InternalLoad(elementType, false);
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000A3EA4 File Offset: 0x000A20A4
		private void InternalLoad(Type elementType, bool asAddress = false)
		{
			Match match = SourceInfo.regex.Match(this.Arg);
			if (match.Success)
			{
				object variable = this.ILG.GetVariable(match.Groups["a"].Value);
				Type variableType = this.ILG.GetVariableType(variable);
				object variable2 = this.ILG.GetVariable(match.Groups["ia"].Value);
				if (variableType.IsArray)
				{
					this.ILG.Load(variable);
					this.ILG.Load(variable2);
					Type elementType2 = variableType.GetElementType();
					if (CodeGenerator.IsNullableGenericType(elementType2))
					{
						this.ILG.Ldelema(elementType2);
						this.ConvertNullableValue(elementType2, elementType);
						return;
					}
					if (elementType2.IsValueType)
					{
						this.ILG.Ldelema(elementType2);
						if (!asAddress)
						{
							this.ILG.Ldobj(elementType2);
						}
					}
					else
					{
						this.ILG.Ldelem(elementType2);
					}
					if (elementType != null)
					{
						this.ILG.ConvertValue(elementType2, elementType);
						return;
					}
				}
				else
				{
					this.ILG.Load(variable);
					this.ILG.Load(variable2);
					MethodInfo methodInfo = variableType.GetMethod("get_Item", CodeGenerator.InstanceBindingFlags, null, new Type[] { typeof(int) }, null);
					if (methodInfo == null && typeof(IList).IsAssignableFrom(variableType))
					{
						methodInfo = SourceInfo.iListGetItemMethod.Value;
					}
					this.ILG.Call(methodInfo);
					Type returnType = methodInfo.ReturnType;
					if (CodeGenerator.IsNullableGenericType(returnType))
					{
						LocalBuilder tempLocal = this.ILG.GetTempLocal(returnType);
						this.ILG.Stloc(tempLocal);
						this.ILG.Ldloca(tempLocal);
						this.ConvertNullableValue(returnType, elementType);
						return;
					}
					if (elementType != null && !returnType.IsAssignableFrom(elementType) && !elementType.IsAssignableFrom(returnType))
					{
						throw new CodeGeneratorConversionException(returnType, elementType, asAddress, "IsNotAssignableFrom");
					}
					this.Convert(returnType, elementType, asAddress);
					return;
				}
			}
			else
			{
				if (this.Source == "null")
				{
					this.ILG.Load(null);
					return;
				}
				Type type;
				if (this.Arg.StartsWith("o.@", StringComparison.Ordinal) || this.MemberInfo != null)
				{
					object obj = this.ILG.GetVariable(this.Arg.StartsWith("o.@", StringComparison.Ordinal) ? "o" : this.Arg);
					type = this.ILG.GetVariableType(obj);
					if (type.IsValueType)
					{
						this.ILG.LoadAddress(obj);
					}
					else
					{
						this.ILG.Load(obj);
					}
				}
				else
				{
					object obj = this.ILG.GetVariable(this.Arg);
					type = this.ILG.GetVariableType(obj);
					if (CodeGenerator.IsNullableGenericType(type) && type.GetGenericArguments()[0] == elementType)
					{
						this.ILG.LoadAddress(obj);
						this.ConvertNullableValue(type, elementType);
					}
					else if (asAddress)
					{
						this.ILG.LoadAddress(obj);
					}
					else
					{
						this.ILG.Load(obj);
					}
				}
				if (this.MemberInfo != null)
				{
					Type type2 = ((this.MemberInfo is FieldInfo) ? ((FieldInfo)this.MemberInfo).FieldType : ((PropertyInfo)this.MemberInfo).PropertyType);
					if (CodeGenerator.IsNullableGenericType(type2))
					{
						this.ILG.LoadMemberAddress(this.MemberInfo);
						this.ConvertNullableValue(type2, elementType);
						return;
					}
					this.ILG.LoadMember(this.MemberInfo);
					this.Convert(type2, elementType, asAddress);
					return;
				}
				else
				{
					match = SourceInfo.regex2.Match(this.Source);
					if (match.Success)
					{
						if (asAddress)
						{
							this.ILG.ConvertAddress(type, this.Type);
						}
						else
						{
							this.ILG.ConvertValue(type, this.Type);
						}
						type = this.Type;
					}
					this.Convert(type, elementType, asAddress);
				}
			}
		}

		// Token: 0x06001DC8 RID: 7624 RVA: 0x000A4297 File Offset: 0x000A2497
		private void Convert(Type sourceType, Type targetType, bool asAddress)
		{
			if (targetType != null)
			{
				if (asAddress)
				{
					this.ILG.ConvertAddress(sourceType, targetType);
					return;
				}
				this.ILG.ConvertValue(sourceType, targetType);
			}
		}

		// Token: 0x06001DC9 RID: 7625 RVA: 0x000A42C0 File Offset: 0x000A24C0
		private void ConvertNullableValue(Type nullableType, Type targetType)
		{
			if (targetType != nullableType)
			{
				MethodInfo method = nullableType.GetMethod("get_Value", CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ILG.Call(method);
				if (targetType != null)
				{
					this.ILG.ConvertValue(method.ReturnType, targetType);
				}
			}
		}

		// Token: 0x06001DCA RID: 7626 RVA: 0x000A4315 File Offset: 0x000A2515
		public static implicit operator string(SourceInfo source)
		{
			return source.Source;
		}

		// Token: 0x06001DCB RID: 7627 RVA: 0x000A431D File Offset: 0x000A251D
		public static bool operator !=(SourceInfo a, SourceInfo b)
		{
			if (a != null)
			{
				return !a.Equals(b);
			}
			return b != null;
		}

		// Token: 0x06001DCC RID: 7628 RVA: 0x000A4331 File Offset: 0x000A2531
		public static bool operator ==(SourceInfo a, SourceInfo b)
		{
			if (a != null)
			{
				return a.Equals(b);
			}
			return b == null;
		}

		// Token: 0x06001DCD RID: 7629 RVA: 0x000A4344 File Offset: 0x000A2544
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return this.Source == null;
			}
			SourceInfo sourceInfo = obj as SourceInfo;
			return sourceInfo != null && this.Source == sourceInfo.Source;
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x000A4381 File Offset: 0x000A2581
		public override int GetHashCode()
		{
			if (this.Source != null)
			{
				return this.Source.GetHashCode();
			}
			return 0;
		}

		// Token: 0x040016AE RID: 5806
		private static Regex regex = new Regex("([(][(](?<t>[^)]+)[)])?(?<a>[^[]+)[[](?<ia>.+)[]][)]?");

		// Token: 0x040016AF RID: 5807
		private static Regex regex2 = new Regex("[(][(](?<cast>[^)]+)[)](?<arg>[^)]+)[)]");

		// Token: 0x040016B0 RID: 5808
		private static readonly Lazy<MethodInfo> iListGetItemMethod = new Lazy<MethodInfo>(() => typeof(IList).GetMethod("get_Item", CodeGenerator.InstanceBindingFlags, null, new Type[] { typeof(int) }, null));

		// Token: 0x040016B1 RID: 5809
		public string Source;

		// Token: 0x040016B2 RID: 5810
		public readonly string Arg;

		// Token: 0x040016B3 RID: 5811
		public readonly MemberInfo MemberInfo;

		// Token: 0x040016B4 RID: 5812
		public readonly Type Type;

		// Token: 0x040016B5 RID: 5813
		public readonly CodeGenerator ILG;
	}
}
