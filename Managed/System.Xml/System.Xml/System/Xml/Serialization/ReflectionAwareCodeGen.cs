using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Text;

namespace System.Xml.Serialization
{
	// Token: 0x0200035D RID: 861
	internal class ReflectionAwareCodeGen
	{
		// Token: 0x060022DA RID: 8922 RVA: 0x000D3DD1 File Offset: 0x000D1FD1
		internal ReflectionAwareCodeGen(IndentedWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x000D3DE0 File Offset: 0x000D1FE0
		internal void WriteReflectionInit(TypeScope scope)
		{
			foreach (object obj in scope.Types)
			{
				Type type = (Type)obj;
				TypeDesc typeDesc = scope.GetTypeDesc(type);
				if (typeDesc.UseReflection)
				{
					this.WriteTypeInfo(scope, typeDesc, type);
				}
			}
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x000D3E4C File Offset: 0x000D204C
		private string WriteTypeInfo(TypeScope scope, TypeDesc typeDesc, Type type)
		{
			this.InitTheFirstTime();
			string csharpName = typeDesc.CSharpName;
			string text = (string)this.reflectionVariables[csharpName];
			if (text != null)
			{
				return text;
			}
			if (type.IsArray)
			{
				text = this.GenerateVariableName("array", typeDesc.CSharpName);
				TypeDesc arrayElementTypeDesc = typeDesc.ArrayElementTypeDesc;
				if (arrayElementTypeDesc.UseReflection)
				{
					string text2 = this.WriteTypeInfo(scope, arrayElementTypeDesc, scope.GetTypeFromTypeDesc(arrayElementTypeDesc));
					this.writer.WriteLine(string.Concat(new string[]
					{
						"static ",
						typeof(Type).FullName,
						" ",
						text,
						" = ",
						text2,
						".MakeArrayType();"
					}));
				}
				else
				{
					string text3 = this.WriteAssemblyInfo(type);
					this.writer.Write(string.Concat(new string[]
					{
						"static ",
						typeof(Type).FullName,
						" ",
						text,
						" = ",
						text3,
						".GetType("
					}));
					this.WriteQuotedCSharpString(type.FullName);
					this.writer.WriteLine(");");
				}
			}
			else
			{
				text = this.GenerateVariableName("type", typeDesc.CSharpName);
				Type underlyingType = Nullable.GetUnderlyingType(type);
				if (underlyingType != null)
				{
					string text4 = this.WriteTypeInfo(scope, scope.GetTypeDesc(underlyingType), underlyingType);
					this.writer.WriteLine(string.Concat(new string[]
					{
						"static ",
						typeof(Type).FullName,
						" ",
						text,
						" = typeof(System.Nullable<>).MakeGenericType(new ",
						typeof(Type).FullName,
						"[] {",
						text4,
						"});"
					}));
				}
				else
				{
					string text5 = this.WriteAssemblyInfo(type);
					this.writer.Write(string.Concat(new string[]
					{
						"static ",
						typeof(Type).FullName,
						" ",
						text,
						" = ",
						text5,
						".GetType("
					}));
					this.WriteQuotedCSharpString(type.FullName);
					this.writer.WriteLine(");");
				}
			}
			this.reflectionVariables.Add(csharpName, text);
			TypeMapping typeMappingFromTypeDesc = scope.GetTypeMappingFromTypeDesc(typeDesc);
			if (typeMappingFromTypeDesc != null)
			{
				this.WriteMappingInfo(typeMappingFromTypeDesc, text, type);
			}
			if (typeDesc.IsCollection || typeDesc.IsEnumerable)
			{
				TypeDesc arrayElementTypeDesc2 = typeDesc.ArrayElementTypeDesc;
				if (arrayElementTypeDesc2.UseReflection)
				{
					this.WriteTypeInfo(scope, arrayElementTypeDesc2, scope.GetTypeFromTypeDesc(arrayElementTypeDesc2));
				}
				this.WriteCollectionInfo(text, typeDesc, type);
			}
			return text;
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000D4108 File Offset: 0x000D2308
		private void InitTheFirstTime()
		{
			if (this.reflectionVariables == null)
			{
				this.reflectionVariables = new Hashtable();
				this.writer.Write(string.Format(CultureInfo.InvariantCulture, ReflectionAwareCodeGen.helperClassesForUseReflection, new object[]
				{
					"object",
					"string",
					typeof(Type).FullName,
					typeof(FieldInfo).FullName,
					typeof(PropertyInfo).FullName,
					typeof(MemberInfo).FullName,
					typeof(MemberTypes).FullName
				}));
				this.WriteDefaultIndexerInit(typeof(IList), typeof(Array).FullName, false, false);
			}
		}

		// Token: 0x060022DE RID: 8926 RVA: 0x000D41D8 File Offset: 0x000D23D8
		private void WriteMappingInfo(TypeMapping mapping, string typeVariable, Type type)
		{
			string csharpName = mapping.TypeDesc.CSharpName;
			if (mapping is StructMapping)
			{
				StructMapping structMapping = mapping as StructMapping;
				for (int i = 0; i < structMapping.Members.Length; i++)
				{
					MemberMapping memberMapping = structMapping.Members[i];
					this.WriteMemberInfo(type, csharpName, typeVariable, memberMapping.Name);
					if (memberMapping.CheckShouldPersist)
					{
						string text = "ShouldSerialize" + memberMapping.Name;
						this.WriteMethodInfo(csharpName, typeVariable, text, false, Array.Empty<string>());
					}
					if (memberMapping.CheckSpecified != SpecifiedAccessor.None)
					{
						string text2 = memberMapping.Name + "Specified";
						this.WriteMemberInfo(type, csharpName, typeVariable, text2);
					}
					if (memberMapping.ChoiceIdentifier != null)
					{
						string memberName = memberMapping.ChoiceIdentifier.MemberName;
						this.WriteMemberInfo(type, csharpName, typeVariable, memberName);
					}
				}
				return;
			}
			if (mapping is EnumMapping)
			{
				FieldInfo[] fields = type.GetFields();
				for (int j = 0; j < fields.Length; j++)
				{
					this.WriteMemberInfo(type, csharpName, typeVariable, fields[j].Name);
				}
			}
		}

		// Token: 0x060022DF RID: 8927 RVA: 0x000D42E0 File Offset: 0x000D24E0
		private void WriteCollectionInfo(string typeVariable, TypeDesc typeDesc, Type type)
		{
			string csharpName = CodeIdentifier.GetCSharpName(type);
			string csharpName2 = typeDesc.ArrayElementTypeDesc.CSharpName;
			bool useReflection = typeDesc.ArrayElementTypeDesc.UseReflection;
			if (typeDesc.IsCollection)
			{
				this.WriteDefaultIndexerInit(type, csharpName, typeDesc.UseReflection, useReflection);
			}
			else if (typeDesc.IsEnumerable)
			{
				if (typeDesc.IsGenericInterface)
				{
					this.WriteMethodInfo(csharpName, typeVariable, "System.Collections.Generic.IEnumerable*", true, Array.Empty<string>());
				}
				else if (!typeDesc.IsPrivateImplementation)
				{
					this.WriteMethodInfo(csharpName, typeVariable, "GetEnumerator", true, Array.Empty<string>());
				}
			}
			this.WriteMethodInfo(csharpName, typeVariable, "Add", false, new string[] { this.GetStringForTypeof(csharpName2, useReflection) });
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000D4388 File Offset: 0x000D2588
		private string WriteAssemblyInfo(Type type)
		{
			string fullName = type.Assembly.FullName;
			string text = (string)this.reflectionVariables[fullName];
			if (text == null)
			{
				int num = fullName.IndexOf(',');
				string text2 = ((num > -1) ? fullName.Substring(0, num) : fullName);
				text = this.GenerateVariableName("assembly", text2);
				this.writer.Write(string.Concat(new string[]
				{
					"static ",
					typeof(Assembly).FullName,
					" ",
					text,
					" = ResolveDynamicAssembly("
				}));
				this.WriteQuotedCSharpString(DynamicAssemblies.GetName(type.Assembly));
				this.writer.WriteLine(");");
				this.reflectionVariables.Add(fullName, text);
			}
			return text;
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000D4454 File Offset: 0x000D2654
		private string WriteMemberInfo(Type type, string escapedName, string typeVariable, string memberName)
		{
			MemberInfo[] member = type.GetMember(memberName);
			for (int i = 0; i < member.Length; i++)
			{
				MemberTypes memberType = member[i].MemberType;
				if (memberType == MemberTypes.Property)
				{
					string text = this.GenerateVariableName("prop", memberName);
					this.writer.Write(string.Concat(new string[] { "static XSPropInfo ", text, " = new XSPropInfo(", typeVariable, ", " }));
					this.WriteQuotedCSharpString(memberName);
					this.writer.WriteLine(");");
					this.reflectionVariables.Add(memberName + ":" + escapedName, text);
					return text;
				}
				if (memberType == MemberTypes.Field)
				{
					string text2 = this.GenerateVariableName("field", memberName);
					this.writer.Write(string.Concat(new string[] { "static XSFieldInfo ", text2, " = new XSFieldInfo(", typeVariable, ", " }));
					this.WriteQuotedCSharpString(memberName);
					this.writer.WriteLine(");");
					this.reflectionVariables.Add(memberName + ":" + escapedName, text2);
					return text2;
				}
			}
			throw new InvalidOperationException(Res.GetString("{0} is an unsupported type. Please use [XmlIgnore] attribute to exclude members of this type from serialization graph.", new object[] { member[0].ToString() }));
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x000D45A4 File Offset: 0x000D27A4
		private string WriteMethodInfo(string escapedName, string typeVariable, string memberName, bool isNonPublic, params string[] paramTypes)
		{
			string text = this.GenerateVariableName("method", memberName);
			this.writer.Write(string.Concat(new string[]
			{
				"static ",
				typeof(MethodInfo).FullName,
				" ",
				text,
				" = ",
				typeVariable,
				".GetMethod("
			}));
			this.WriteQuotedCSharpString(memberName);
			this.writer.Write(", ");
			string fullName = typeof(BindingFlags).FullName;
			this.writer.Write(fullName);
			this.writer.Write(".Public | ");
			this.writer.Write(fullName);
			this.writer.Write(".Instance | ");
			this.writer.Write(fullName);
			this.writer.Write(".Static");
			if (isNonPublic)
			{
				this.writer.Write(" | ");
				this.writer.Write(fullName);
				this.writer.Write(".NonPublic");
			}
			this.writer.Write(", null, ");
			this.writer.Write("new " + typeof(Type).FullName + "[] { ");
			for (int i = 0; i < paramTypes.Length; i++)
			{
				this.writer.Write(paramTypes[i]);
				if (i < paramTypes.Length - 1)
				{
					this.writer.Write(", ");
				}
			}
			this.writer.WriteLine("}, null);");
			this.reflectionVariables.Add(memberName + ":" + escapedName, text);
			return text;
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x000D4754 File Offset: 0x000D2954
		private string WriteDefaultIndexerInit(Type type, string escapedName, bool collectionUseReflection, bool elementUseReflection)
		{
			string text = this.GenerateVariableName("item", escapedName);
			PropertyInfo defaultIndexer = TypeScope.GetDefaultIndexer(type, null);
			this.writer.Write("static XSArrayInfo ");
			this.writer.Write(text);
			this.writer.Write("= new XSArrayInfo(");
			this.writer.Write(this.GetStringForTypeof(CodeIdentifier.GetCSharpName(type), collectionUseReflection));
			this.writer.Write(".GetProperty(");
			this.WriteQuotedCSharpString(defaultIndexer.Name);
			this.writer.Write(",");
			this.writer.Write(this.GetStringForTypeof(CodeIdentifier.GetCSharpName(defaultIndexer.PropertyType), elementUseReflection));
			this.writer.Write(",new ");
			this.writer.Write(typeof(Type[]).FullName);
			this.writer.WriteLine("{typeof(int)}));");
			this.reflectionVariables.Add("0:" + escapedName, text);
			return text;
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x000D4858 File Offset: 0x000D2A58
		private string GenerateVariableName(string prefix, string fullName)
		{
			this.nextReflectionVariableNumber++;
			return string.Concat(new object[]
			{
				prefix,
				this.nextReflectionVariableNumber,
				"_",
				CodeIdentifier.MakeValidInternal(fullName.Replace('.', '_'))
			});
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x000D48AC File Offset: 0x000D2AAC
		internal string GetReflectionVariable(string typeFullName, string memberName)
		{
			string text;
			if (memberName == null)
			{
				text = typeFullName;
			}
			else
			{
				text = memberName + ":" + typeFullName;
			}
			return (string)this.reflectionVariables[text];
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000D48E0 File Offset: 0x000D2AE0
		internal string GetStringForMethodInvoke(string obj, string escapedTypeName, string methodName, bool useReflection, params string[] args)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (useReflection)
			{
				stringBuilder.Append(this.GetReflectionVariable(escapedTypeName, methodName));
				stringBuilder.Append(".Invoke(");
				stringBuilder.Append(obj);
				stringBuilder.Append(", new object[] {");
			}
			else
			{
				stringBuilder.Append(obj);
				stringBuilder.Append(".@");
				stringBuilder.Append(methodName);
				stringBuilder.Append("(");
			}
			for (int i = 0; i < args.Length; i++)
			{
				if (i != 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(args[i]);
			}
			if (useReflection)
			{
				stringBuilder.Append("})");
			}
			else
			{
				stringBuilder.Append(")");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000D49A0 File Offset: 0x000D2BA0
		internal string GetStringForEnumCompare(EnumMapping mapping, string memberName, bool useReflection)
		{
			if (!useReflection)
			{
				CodeIdentifier.CheckValidIdentifier(memberName);
				return mapping.TypeDesc.CSharpName + ".@" + memberName;
			}
			string stringForEnumMember = this.GetStringForEnumMember(mapping.TypeDesc.CSharpName, memberName, useReflection);
			return this.GetStringForEnumLongValue(stringForEnumMember, useReflection);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000D49EC File Offset: 0x000D2BEC
		internal string GetStringForEnumLongValue(string variable, bool useReflection)
		{
			if (useReflection)
			{
				return typeof(Convert).FullName + ".ToInt64(" + variable + ")";
			}
			return string.Concat(new string[]
			{
				"((",
				typeof(long).FullName,
				")",
				variable,
				")"
			});
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x000D4A55 File Offset: 0x000D2C55
		internal string GetStringForTypeof(string typeFullName, bool useReflection)
		{
			if (useReflection)
			{
				return this.GetReflectionVariable(typeFullName, null);
			}
			return "typeof(" + typeFullName + ")";
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x000D4A74 File Offset: 0x000D2C74
		internal string GetStringForMember(string obj, string memberName, TypeDesc typeDesc)
		{
			if (!typeDesc.UseReflection)
			{
				return obj + ".@" + memberName;
			}
			while (typeDesc != null)
			{
				string csharpName = typeDesc.CSharpName;
				string reflectionVariable = this.GetReflectionVariable(csharpName, memberName);
				if (reflectionVariable != null)
				{
					return reflectionVariable + "[" + obj + "]";
				}
				typeDesc = typeDesc.BaseTypeDesc;
				if (typeDesc != null && !typeDesc.UseReflection)
				{
					return string.Concat(new string[] { "((", typeDesc.CSharpName, ")", obj, ").@", memberName });
				}
			}
			return "[" + obj + "]";
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x000D4B16 File Offset: 0x000D2D16
		internal string GetStringForEnumMember(string typeFullName, string memberName, bool useReflection)
		{
			if (!useReflection)
			{
				return typeFullName + ".@" + memberName;
			}
			return this.GetReflectionVariable(typeFullName, memberName) + "[null]";
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x000D4B3C File Offset: 0x000D2D3C
		internal string GetStringForArrayMember(string arrayName, string subscript, TypeDesc arrayTypeDesc)
		{
			if (!arrayTypeDesc.UseReflection)
			{
				return arrayName + "[" + subscript + "]";
			}
			string text = (arrayTypeDesc.IsCollection ? arrayTypeDesc.CSharpName : typeof(Array).FullName);
			string reflectionVariable = this.GetReflectionVariable(text, "0");
			return string.Concat(new string[] { reflectionVariable, "[", arrayName, ", ", subscript, "]" });
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x000D4BBF File Offset: 0x000D2DBF
		internal string GetStringForMethod(string obj, string typeFullName, string memberName, bool useReflection)
		{
			if (!useReflection)
			{
				return obj + "." + memberName + "(";
			}
			return this.GetReflectionVariable(typeFullName, memberName) + ".Invoke(" + obj + ", new object[]{";
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x000D4BEF File Offset: 0x000D2DEF
		internal string GetStringForCreateInstance(string escapedTypeName, bool useReflection, bool ctorInaccessible, bool cast)
		{
			return this.GetStringForCreateInstance(escapedTypeName, useReflection, ctorInaccessible, cast, string.Empty);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x000D4C04 File Offset: 0x000D2E04
		internal string GetStringForCreateInstance(string escapedTypeName, bool useReflection, bool ctorInaccessible, bool cast, string arg)
		{
			if (!useReflection && !ctorInaccessible)
			{
				return string.Concat(new string[] { "new ", escapedTypeName, "(", arg, ")" });
			}
			return this.GetStringForCreateInstance(this.GetStringForTypeof(escapedTypeName, useReflection), (cast && !useReflection) ? escapedTypeName : null, ctorInaccessible, arg);
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000D4C60 File Offset: 0x000D2E60
		internal string GetStringForCreateInstance(string type, string cast, bool nonPublic, string arg)
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (cast != null && cast.Length > 0)
			{
				stringBuilder.Append("(");
				stringBuilder.Append(cast);
				stringBuilder.Append(")");
			}
			stringBuilder.Append(typeof(Activator).FullName);
			stringBuilder.Append(".CreateInstance(");
			stringBuilder.Append(type);
			stringBuilder.Append(", ");
			string fullName = typeof(BindingFlags).FullName;
			stringBuilder.Append(fullName);
			stringBuilder.Append(".Instance | ");
			stringBuilder.Append(fullName);
			stringBuilder.Append(".Public | ");
			stringBuilder.Append(fullName);
			stringBuilder.Append(".CreateInstance");
			if (nonPublic)
			{
				stringBuilder.Append(" | ");
				stringBuilder.Append(fullName);
				stringBuilder.Append(".NonPublic");
			}
			if (arg == null || arg.Length == 0)
			{
				stringBuilder.Append(", null, new object[0], null)");
			}
			else
			{
				stringBuilder.Append(", null, new object[] { ");
				stringBuilder.Append(arg);
				stringBuilder.Append(" }, null)");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000D4D88 File Offset: 0x000D2F88
		internal void WriteLocalDecl(string typeFullName, string variableName, string initValue, bool useReflection)
		{
			if (useReflection)
			{
				typeFullName = "object";
			}
			this.writer.Write(typeFullName);
			this.writer.Write(" ");
			this.writer.Write(variableName);
			if (initValue != null)
			{
				this.writer.Write(" = ");
				if (!useReflection && initValue != "null")
				{
					this.writer.Write("(" + typeFullName + ")");
				}
				this.writer.Write(initValue);
			}
			this.writer.WriteLine(";");
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000D4E24 File Offset: 0x000D3024
		internal void WriteCreateInstance(string escapedName, string source, bool useReflection, bool ctorInaccessible)
		{
			this.writer.Write(useReflection ? "object" : escapedName);
			this.writer.Write(" ");
			this.writer.Write(source);
			this.writer.Write(" = ");
			this.writer.Write(this.GetStringForCreateInstance(escapedName, useReflection, ctorInaccessible, !useReflection && ctorInaccessible));
			this.writer.WriteLine(";");
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x000D4EA0 File Offset: 0x000D30A0
		internal void WriteInstanceOf(string source, string escapedTypeName, bool useReflection)
		{
			if (!useReflection)
			{
				this.writer.Write(source);
				this.writer.Write(" is ");
				this.writer.Write(escapedTypeName);
				return;
			}
			this.writer.Write(this.GetReflectionVariable(escapedTypeName, null));
			this.writer.Write(".IsAssignableFrom(");
			this.writer.Write(source);
			this.writer.Write(".GetType())");
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000D4F18 File Offset: 0x000D3118
		internal void WriteArrayLocalDecl(string typeName, string variableName, string initValue, TypeDesc arrayTypeDesc)
		{
			if (arrayTypeDesc.UseReflection)
			{
				if (arrayTypeDesc.IsEnumerable)
				{
					typeName = typeof(IEnumerable).FullName;
				}
				else if (arrayTypeDesc.IsCollection)
				{
					typeName = typeof(ICollection).FullName;
				}
				else
				{
					typeName = typeof(Array).FullName;
				}
			}
			this.writer.Write(typeName);
			this.writer.Write(" ");
			this.writer.Write(variableName);
			if (initValue != null)
			{
				this.writer.Write(" = ");
				if (initValue != "null")
				{
					this.writer.Write("(" + typeName + ")");
				}
				this.writer.Write(initValue);
			}
			this.writer.WriteLine(";");
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000D4FF8 File Offset: 0x000D31F8
		internal void WriteEnumCase(string fullTypeName, ConstantMapping c, bool useReflection)
		{
			this.writer.Write("case ");
			if (useReflection)
			{
				this.writer.Write(c.Value.ToString(CultureInfo.InvariantCulture));
			}
			else
			{
				this.writer.Write(fullTypeName);
				this.writer.Write(".@");
				CodeIdentifier.CheckValidIdentifier(c.Name);
				this.writer.Write(c.Name);
			}
			this.writer.Write(": ");
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x000D5080 File Offset: 0x000D3280
		internal void WriteTypeCompare(string variable, string escapedTypeName, bool useReflection)
		{
			this.writer.Write(variable);
			this.writer.Write(" == ");
			this.writer.Write(this.GetStringForTypeof(escapedTypeName, useReflection));
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x000D50B4 File Offset: 0x000D32B4
		internal void WriteArrayTypeCompare(string variable, string escapedTypeName, string elementTypeName, bool useReflection)
		{
			if (!useReflection)
			{
				this.writer.Write(variable);
				this.writer.Write(" == typeof(");
				this.writer.Write(escapedTypeName);
				this.writer.Write(")");
				return;
			}
			this.writer.Write(variable);
			this.writer.Write(".IsArray ");
			this.writer.Write(" && ");
			this.WriteTypeCompare(variable + ".GetElementType()", elementTypeName, useReflection);
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000D5140 File Offset: 0x000D3340
		internal static void WriteQuotedCSharpString(IndentedWriter writer, string value)
		{
			if (value == null)
			{
				writer.Write("null");
				return;
			}
			writer.Write("@\"");
			foreach (char c in value)
			{
				if (c < ' ')
				{
					if (c == '\r')
					{
						writer.Write("\\r");
					}
					else if (c == '\n')
					{
						writer.Write("\\n");
					}
					else if (c == '\t')
					{
						writer.Write("\\t");
					}
					else
					{
						byte b = (byte)c;
						writer.Write("\\x");
						writer.Write("0123456789ABCDEF"[b >> 4]);
						writer.Write("0123456789ABCDEF"[(int)(b & 15)]);
					}
				}
				else if (c == '"')
				{
					writer.Write("\"\"");
				}
				else
				{
					writer.Write(c);
				}
			}
			writer.Write("\"");
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000D521E File Offset: 0x000D341E
		internal void WriteQuotedCSharpString(string value)
		{
			ReflectionAwareCodeGen.WriteQuotedCSharpString(this.writer, value);
		}

		// Token: 0x0400184F RID: 6223
		private const string hexDigits = "0123456789ABCDEF";

		// Token: 0x04001850 RID: 6224
		private const string arrayMemberKey = "0";

		// Token: 0x04001851 RID: 6225
		private Hashtable reflectionVariables;

		// Token: 0x04001852 RID: 6226
		private int nextReflectionVariableNumber;

		// Token: 0x04001853 RID: 6227
		private IndentedWriter writer;

		// Token: 0x04001854 RID: 6228
		private static string helperClassesForUseReflection = "\n    sealed class XSFieldInfo {{\n       {3} fieldInfo;\n        public XSFieldInfo({2} t, {1} memberName){{\n            fieldInfo = t.GetField(memberName);\n        }}\n        public {0} this[{0} o] {{\n            get {{\n                return fieldInfo.GetValue(o);\n            }}\n            set {{\n                fieldInfo.SetValue(o, value);\n            }}\n        }}\n\n    }}\n    sealed class XSPropInfo {{\n        {4} propInfo;\n        public XSPropInfo({2} t, {1} memberName){{\n            propInfo = t.GetProperty(memberName);\n        }}\n        public {0} this[{0} o] {{\n            get {{\n                return propInfo.GetValue(o, null);\n            }}\n            set {{\n                propInfo.SetValue(o, value, null);\n            }}\n        }}\n    }}\n    sealed class XSArrayInfo {{\n        {4} propInfo;\n        public XSArrayInfo({4} propInfo){{\n            this.propInfo = propInfo;\n        }}\n        public {0} this[{0} a, int i] {{\n            get {{\n                return propInfo.GetValue(a, new {0}[]{{i}});\n            }}\n            set {{\n                propInfo.SetValue(a, value, new {0}[]{{i}});\n            }}\n        }}\n    }}\n";
	}
}
