using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace System.Xml.Serialization
{
	// Token: 0x0200034A RID: 842
	internal class XmlSerializationILGen
	{
		// Token: 0x060020CD RID: 8397 RVA: 0x000B7870 File Offset: 0x000B5A70
		internal XmlSerializationILGen(TypeScope[] scopes, string access, string className)
		{
			this.scopes = scopes;
			if (scopes.Length != 0)
			{
				this.stringTypeDesc = scopes[0].GetTypeDesc(typeof(string));
				this.qnameTypeDesc = scopes[0].GetTypeDesc(typeof(XmlQualifiedName));
			}
			this.raCodeGen = new ReflectionAwareILGen();
			this.className = className;
			this.typeAttributes = TypeAttributes.Public;
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x000B790E File Offset: 0x000B5B0E
		// (set) Token: 0x060020CF RID: 8399 RVA: 0x000B7916 File Offset: 0x000B5B16
		internal int NextMethodNumber
		{
			get
			{
				return this.nextMethodNumber;
			}
			set
			{
				this.nextMethodNumber = value;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x000B791F File Offset: 0x000B5B1F
		internal ReflectionAwareILGen RaCodeGen
		{
			get
			{
				return this.raCodeGen;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x000B7927 File Offset: 0x000B5B27
		internal TypeDesc StringTypeDesc
		{
			get
			{
				return this.stringTypeDesc;
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x000B792F File Offset: 0x000B5B2F
		internal TypeDesc QnameTypeDesc
		{
			get
			{
				return this.qnameTypeDesc;
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x000B7937 File Offset: 0x000B5B37
		internal string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060020D4 RID: 8404 RVA: 0x000B793F File Offset: 0x000B5B3F
		internal TypeScope[] Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x060020D5 RID: 8405 RVA: 0x000B7947 File Offset: 0x000B5B47
		internal Hashtable MethodNames
		{
			get
			{
				return this.methodNames;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x060020D6 RID: 8406 RVA: 0x000B794F File Offset: 0x000B5B4F
		internal Hashtable GeneratedMethods
		{
			get
			{
				return this.generatedMethods;
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x060020D7 RID: 8407 RVA: 0x000B7957 File Offset: 0x000B5B57
		// (set) Token: 0x060020D8 RID: 8408 RVA: 0x000B795F File Offset: 0x000B5B5F
		internal ModuleBuilder ModuleBuilder
		{
			get
			{
				return this.moduleBuilder;
			}
			set
			{
				this.moduleBuilder = value;
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060020D9 RID: 8409 RVA: 0x000B7968 File Offset: 0x000B5B68
		internal TypeAttributes TypeAttributes
		{
			get
			{
				return this.typeAttributes;
			}
		}

		// Token: 0x060020DA RID: 8410 RVA: 0x000B7970 File Offset: 0x000B5B70
		internal static Regex NewRegex(string pattern)
		{
			Dictionary<string, Regex> dictionary = XmlSerializationILGen.regexs;
			Regex regex;
			lock (dictionary)
			{
				if (!XmlSerializationILGen.regexs.TryGetValue(pattern, out regex))
				{
					regex = new Regex(pattern);
					XmlSerializationILGen.regexs.Add(pattern, regex);
				}
			}
			return regex;
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x000B79CC File Offset: 0x000B5BCC
		internal MethodBuilder EnsureMethodBuilder(TypeBuilder typeBuilder, string methodName, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			MethodBuilderInfo methodBuilderInfo;
			if (!this.methodBuilders.TryGetValue(methodName, out methodBuilderInfo))
			{
				methodBuilderInfo = new MethodBuilderInfo(typeBuilder.DefineMethod(methodName, attributes, returnType, parameterTypes), parameterTypes);
				this.methodBuilders.Add(methodName, methodBuilderInfo);
			}
			return methodBuilderInfo.MethodBuilder;
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x000B7A10 File Offset: 0x000B5C10
		internal MethodBuilderInfo GetMethodBuilder(string methodName)
		{
			return this.methodBuilders[methodName];
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x00002F50 File Offset: 0x00001150
		internal virtual void GenerateMethod(TypeMapping mapping)
		{
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x000B7A20 File Offset: 0x000B5C20
		internal void GenerateReferencedMethods()
		{
			while (this.references > 0)
			{
				TypeMapping[] array = this.referencedMethods;
				int num = this.references - 1;
				this.references = num;
				TypeMapping typeMapping = array[num];
				this.GenerateMethod(typeMapping);
			}
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x000B7A58 File Offset: 0x000B5C58
		internal string ReferenceMapping(TypeMapping mapping)
		{
			if (this.generatedMethods[mapping] == null)
			{
				this.referencedMethods = this.EnsureArrayIndex(this.referencedMethods, this.references);
				TypeMapping[] array = this.referencedMethods;
				int num = this.references;
				this.references = num + 1;
				array[num] = mapping;
			}
			return (string)this.methodNames[mapping];
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x000B7AB8 File Offset: 0x000B5CB8
		private TypeMapping[] EnsureArrayIndex(TypeMapping[] a, int index)
		{
			if (a == null)
			{
				return new TypeMapping[32];
			}
			if (index < a.Length)
			{
				return a;
			}
			TypeMapping[] array = new TypeMapping[a.Length + 32];
			Array.Copy(a, array, index);
			return array;
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x000B7AF0 File Offset: 0x000B5CF0
		internal FieldBuilder GenerateHashtableGetBegin(string privateName, string publicName, TypeBuilder serializerContractTypeBuilder)
		{
			FieldBuilder fieldBuilder = serializerContractTypeBuilder.DefineField(privateName, typeof(Hashtable), FieldAttributes.Private);
			this.ilg = new CodeGenerator(serializerContractTypeBuilder);
			PropertyBuilder propertyBuilder = serializerContractTypeBuilder.DefineProperty(publicName, PropertyAttributes.None, CallingConventions.HasThis, typeof(Hashtable), null, null, null, null, null);
			this.ilg.BeginMethod(typeof(Hashtable), "get_" + publicName, CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicOverrideMethodAttributes | MethodAttributes.SpecialName);
			propertyBuilder.SetGetMethod(this.ilg.MethodBuilder);
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(fieldBuilder);
			this.ilg.Load(null);
			this.ilg.If(Cmp.EqualTo);
			ConstructorInfo constructor = typeof(Hashtable).GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			LocalBuilder localBuilder = this.ilg.DeclareLocal(typeof(Hashtable), "_tmp");
			this.ilg.New(constructor);
			this.ilg.Stloc(localBuilder);
			return fieldBuilder;
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x000B7BFC File Offset: 0x000B5DFC
		internal void GenerateHashtableGetEnd(FieldBuilder fieldBuilder)
		{
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(fieldBuilder);
			this.ilg.Load(null);
			this.ilg.If(Cmp.EqualTo);
			this.ilg.Ldarg(0);
			this.ilg.Ldloc(typeof(Hashtable), "_tmp");
			this.ilg.StoreMember(fieldBuilder);
			this.ilg.EndIf();
			this.ilg.EndIf();
			this.ilg.Ldarg(0);
			this.ilg.LoadMember(fieldBuilder);
			this.ilg.GotoMethodEnd();
			this.ilg.EndMethod();
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x000B7CB4 File Offset: 0x000B5EB4
		internal FieldBuilder GeneratePublicMethods(string privateName, string publicName, string[] methods, XmlMapping[] xmlMappings, TypeBuilder serializerContractTypeBuilder)
		{
			FieldBuilder fieldBuilder = this.GenerateHashtableGetBegin(privateName, publicName, serializerContractTypeBuilder);
			if (methods != null && methods.Length != 0 && xmlMappings != null && xmlMappings.Length == methods.Length)
			{
				MethodInfo method = typeof(Hashtable).GetMethod("set_Item", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(object),
					typeof(object)
				}, null);
				for (int i = 0; i < methods.Length; i++)
				{
					if (methods[i] != null)
					{
						this.ilg.Ldloc(typeof(Hashtable), "_tmp");
						this.ilg.Ldstr(xmlMappings[i].Key);
						this.ilg.Ldstr(methods[i]);
						this.ilg.Call(method);
					}
				}
			}
			this.GenerateHashtableGetEnd(fieldBuilder);
			return fieldBuilder;
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x000B7D8C File Offset: 0x000B5F8C
		internal void GenerateSupportedTypes(Type[] types, TypeBuilder serializerContractTypeBuilder)
		{
			this.ilg = new CodeGenerator(serializerContractTypeBuilder);
			this.ilg.BeginMethod(typeof(bool), "CanSerialize", new Type[] { typeof(Type) }, new string[] { "type" }, CodeGenerator.PublicOverrideMethodAttributes);
			Hashtable hashtable = new Hashtable();
			foreach (Type type in types)
			{
				if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && hashtable[type] == null && !type.IsGenericType && !type.ContainsGenericParameters)
				{
					hashtable[type] = type;
					this.ilg.Ldarg("type");
					this.ilg.Ldc(type);
					this.ilg.If(Cmp.EqualTo);
					this.ilg.Ldc(true);
					this.ilg.GotoMethodEnd();
					this.ilg.EndIf();
				}
			}
			this.ilg.Ldc(false);
			this.ilg.GotoMethodEnd();
			this.ilg.EndMethod();
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x000B7EAC File Offset: 0x000B60AC
		internal string GenerateBaseSerializer(string baseSerializer, string readerClass, string writerClass, CodeIdentifiers classes)
		{
			baseSerializer = CodeIdentifier.MakeValid(baseSerializer);
			baseSerializer = classes.AddUnique(baseSerializer, baseSerializer);
			TypeBuilder typeBuilder = CodeGenerator.CreateTypeBuilder(this.moduleBuilder, CodeIdentifier.GetCSharpName(baseSerializer), TypeAttributes.Public | TypeAttributes.Abstract | TypeAttributes.BeforeFieldInit, typeof(XmlSerializer), CodeGenerator.EmptyTypeArray);
			ConstructorInfo constructor = this.CreatedTypes[readerClass].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg = new CodeGenerator(typeBuilder);
			this.ilg.BeginMethod(typeof(XmlSerializationReader), "CreateReader", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.ProtectedOverrideMethodAttributes);
			this.ilg.New(constructor);
			this.ilg.EndMethod();
			ConstructorInfo constructor2 = this.CreatedTypes[writerClass].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.BeginMethod(typeof(XmlSerializationWriter), "CreateWriter", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.ProtectedOverrideMethodAttributes);
			this.ilg.New(constructor2);
			this.ilg.EndMethod();
			typeBuilder.DefineDefaultConstructor(MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName);
			Type type = typeBuilder.CreateType();
			this.CreatedTypes.Add(type.Name, type);
			return baseSerializer;
		}

		// Token: 0x060020E6 RID: 8422 RVA: 0x000B7FE4 File Offset: 0x000B61E4
		internal string GenerateTypedSerializer(string readMethod, string writeMethod, XmlMapping mapping, CodeIdentifiers classes, string baseSerializer, string readerClass, string writerClass)
		{
			string text = CodeIdentifier.MakeValid(Accessor.UnescapeName(mapping.Accessor.Mapping.TypeDesc.Name));
			text = classes.AddUnique(text + "Serializer", mapping);
			TypeBuilder typeBuilder = CodeGenerator.CreateTypeBuilder(this.moduleBuilder, CodeIdentifier.GetCSharpName(text), TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.BeforeFieldInit, this.CreatedTypes[baseSerializer], CodeGenerator.EmptyTypeArray);
			this.ilg = new CodeGenerator(typeBuilder);
			this.ilg.BeginMethod(typeof(bool), "CanDeserialize", new Type[] { typeof(XmlReader) }, new string[] { "xmlReader" }, CodeGenerator.PublicOverrideMethodAttributes);
			if (mapping.Accessor.Any)
			{
				this.ilg.Ldc(true);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
			}
			else
			{
				MethodInfo method = typeof(XmlReader).GetMethod("IsStartElement", CodeGenerator.InstanceBindingFlags, null, new Type[]
				{
					typeof(string),
					typeof(string)
				}, null);
				this.ilg.Ldarg(this.ilg.GetArg("xmlReader"));
				this.ilg.Ldstr(mapping.Accessor.Name);
				this.ilg.Ldstr(mapping.Accessor.Namespace);
				this.ilg.Call(method);
				this.ilg.Stloc(this.ilg.ReturnLocal);
				this.ilg.Br(this.ilg.ReturnLabel);
			}
			this.ilg.MarkLabel(this.ilg.ReturnLabel);
			this.ilg.Ldloc(this.ilg.ReturnLocal);
			this.ilg.EndMethod();
			if (writeMethod != null)
			{
				this.ilg = new CodeGenerator(typeBuilder);
				this.ilg.BeginMethod(typeof(void), "Serialize", new Type[]
				{
					typeof(object),
					typeof(XmlSerializationWriter)
				}, new string[] { "objectToSerialize", "writer" }, CodeGenerator.ProtectedOverrideMethodAttributes);
				MethodInfo method2 = this.CreatedTypes[writerClass].GetMethod(writeMethod, CodeGenerator.InstanceBindingFlags, null, new Type[] { (mapping is XmlMembersMapping) ? typeof(object[]) : typeof(object) }, null);
				this.ilg.Ldarg("writer");
				this.ilg.Castclass(this.CreatedTypes[writerClass]);
				this.ilg.Ldarg("objectToSerialize");
				if (mapping is XmlMembersMapping)
				{
					this.ilg.ConvertValue(typeof(object), typeof(object[]));
				}
				this.ilg.Call(method2);
				this.ilg.EndMethod();
			}
			if (readMethod != null)
			{
				this.ilg = new CodeGenerator(typeBuilder);
				this.ilg.BeginMethod(typeof(object), "Deserialize", new Type[] { typeof(XmlSerializationReader) }, new string[] { "reader" }, CodeGenerator.ProtectedOverrideMethodAttributes);
				MethodInfo method3 = this.CreatedTypes[readerClass].GetMethod(readMethod, CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldarg("reader");
				this.ilg.Castclass(this.CreatedTypes[readerClass]);
				this.ilg.Call(method3);
				this.ilg.EndMethod();
			}
			typeBuilder.DefineDefaultConstructor(CodeGenerator.PublicMethodAttributes);
			Type type = typeBuilder.CreateType();
			this.CreatedTypes.Add(type.Name, type);
			return type.Name;
		}

		// Token: 0x060020E7 RID: 8423 RVA: 0x000B83D8 File Offset: 0x000B65D8
		private FieldBuilder GenerateTypedSerializers(Hashtable serializers, TypeBuilder serializerContractTypeBuilder)
		{
			string text = "typedSerializers";
			FieldBuilder fieldBuilder = this.GenerateHashtableGetBegin(text, "TypedSerializers", serializerContractTypeBuilder);
			MethodInfo method = typeof(Hashtable).GetMethod("Add", CodeGenerator.InstanceBindingFlags, null, new Type[]
			{
				typeof(object),
				typeof(object)
			}, null);
			foreach (object obj in serializers.Keys)
			{
				string text2 = (string)obj;
				ConstructorInfo constructor = this.CreatedTypes[(string)serializers[text2]].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
				this.ilg.Ldloc(typeof(Hashtable), "_tmp");
				this.ilg.Ldstr(text2);
				this.ilg.New(constructor);
				this.ilg.Call(method);
			}
			this.GenerateHashtableGetEnd(fieldBuilder);
			return fieldBuilder;
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x000B84F4 File Offset: 0x000B66F4
		private void GenerateGetSerializer(Hashtable serializers, XmlMapping[] xmlMappings, TypeBuilder serializerContractTypeBuilder)
		{
			this.ilg = new CodeGenerator(serializerContractTypeBuilder);
			this.ilg.BeginMethod(typeof(XmlSerializer), "GetSerializer", new Type[] { typeof(Type) }, new string[] { "type" }, CodeGenerator.PublicOverrideMethodAttributes);
			for (int i = 0; i < xmlMappings.Length; i++)
			{
				if (xmlMappings[i] is XmlTypeMapping)
				{
					Type type = xmlMappings[i].Accessor.Mapping.TypeDesc.Type;
					if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && !type.IsGenericType && !type.ContainsGenericParameters)
					{
						this.ilg.Ldarg("type");
						this.ilg.Ldc(type);
						this.ilg.If(Cmp.EqualTo);
						ConstructorInfo constructor = this.CreatedTypes[(string)serializers[xmlMappings[i].Key]].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
						this.ilg.New(constructor);
						this.ilg.Stloc(this.ilg.ReturnLocal);
						this.ilg.Br(this.ilg.ReturnLabel);
						this.ilg.EndIf();
					}
				}
			}
			this.ilg.Load(null);
			this.ilg.Stloc(this.ilg.ReturnLocal);
			this.ilg.Br(this.ilg.ReturnLabel);
			this.ilg.MarkLabel(this.ilg.ReturnLabel);
			this.ilg.Ldloc(this.ilg.ReturnLocal);
			this.ilg.EndMethod();
		}

		// Token: 0x060020E9 RID: 8425 RVA: 0x000B86C8 File Offset: 0x000B68C8
		internal void GenerateSerializerContract(string className, XmlMapping[] xmlMappings, Type[] types, string readerType, string[] readMethods, string writerType, string[] writerMethods, Hashtable serializers)
		{
			TypeBuilder typeBuilder = CodeGenerator.CreateTypeBuilder(this.moduleBuilder, "XmlSerializerContract", TypeAttributes.Public | TypeAttributes.BeforeFieldInit, typeof(XmlSerializerImplementation), CodeGenerator.EmptyTypeArray);
			this.ilg = new CodeGenerator(typeBuilder);
			PropertyBuilder propertyBuilder = typeBuilder.DefineProperty("Reader", PropertyAttributes.None, CallingConventions.HasThis, typeof(XmlSerializationReader), null, null, null, null, null);
			this.ilg.BeginMethod(typeof(XmlSerializationReader), "get_Reader", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicOverrideMethodAttributes | MethodAttributes.SpecialName);
			propertyBuilder.SetGetMethod(this.ilg.MethodBuilder);
			ConstructorInfo constructorInfo = this.CreatedTypes[readerType].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.New(constructorInfo);
			this.ilg.EndMethod();
			this.ilg = new CodeGenerator(typeBuilder);
			PropertyBuilder propertyBuilder2 = typeBuilder.DefineProperty("Writer", PropertyAttributes.None, CallingConventions.HasThis, typeof(XmlSerializationWriter), null, null, null, null, null);
			this.ilg.BeginMethod(typeof(XmlSerializationWriter), "get_Writer", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicOverrideMethodAttributes | MethodAttributes.SpecialName);
			propertyBuilder2.SetGetMethod(this.ilg.MethodBuilder);
			constructorInfo = this.CreatedTypes[writerType].GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg.New(constructorInfo);
			this.ilg.EndMethod();
			FieldBuilder fieldBuilder = this.GeneratePublicMethods("readMethods", "ReadMethods", readMethods, xmlMappings, typeBuilder);
			FieldBuilder fieldBuilder2 = this.GeneratePublicMethods("writeMethods", "WriteMethods", writerMethods, xmlMappings, typeBuilder);
			FieldBuilder fieldBuilder3 = this.GenerateTypedSerializers(serializers, typeBuilder);
			this.GenerateSupportedTypes(types, typeBuilder);
			this.GenerateGetSerializer(serializers, xmlMappings, typeBuilder);
			ConstructorInfo constructor = typeof(XmlSerializerImplementation).GetConstructor(CodeGenerator.InstanceBindingFlags, null, CodeGenerator.EmptyTypeArray, null);
			this.ilg = new CodeGenerator(typeBuilder);
			this.ilg.BeginMethod(typeof(void), ".ctor", CodeGenerator.EmptyTypeArray, CodeGenerator.EmptyStringArray, CodeGenerator.PublicMethodAttributes | MethodAttributes.RTSpecialName | MethodAttributes.SpecialName);
			this.ilg.Ldarg(0);
			this.ilg.Load(null);
			this.ilg.StoreMember(fieldBuilder);
			this.ilg.Ldarg(0);
			this.ilg.Load(null);
			this.ilg.StoreMember(fieldBuilder2);
			this.ilg.Ldarg(0);
			this.ilg.Load(null);
			this.ilg.StoreMember(fieldBuilder3);
			this.ilg.Ldarg(0);
			this.ilg.Call(constructor);
			this.ilg.EndMethod();
			Type type = typeBuilder.CreateType();
			this.CreatedTypes.Add(type.Name, type);
		}

		// Token: 0x060020EA RID: 8426 RVA: 0x000B784C File Offset: 0x000B5A4C
		internal static bool IsWildcard(SpecialMapping mapping)
		{
			if (mapping is SerializableMapping)
			{
				return ((SerializableMapping)mapping).IsAny;
			}
			return mapping.TypeDesc.CanBeElementValue;
		}

		// Token: 0x060020EB RID: 8427 RVA: 0x000B898B File Offset: 0x000B6B8B
		internal void ILGenLoad(string source)
		{
			this.ILGenLoad(source, null);
		}

		// Token: 0x060020EC RID: 8428 RVA: 0x000B8998 File Offset: 0x000B6B98
		internal void ILGenLoad(string source, Type type)
		{
			if (source.StartsWith("o.@", StringComparison.Ordinal))
			{
				MemberInfo memberInfo = this.memberInfos[source.Substring(3)];
				this.ilg.LoadMember(this.ilg.GetVariable("o"), memberInfo);
				if (type != null)
				{
					Type type2 = ((memberInfo.MemberType == MemberTypes.Field) ? ((FieldInfo)memberInfo).FieldType : ((PropertyInfo)memberInfo).PropertyType);
					this.ilg.ConvertValue(type2, type);
					return;
				}
			}
			else
			{
				new SourceInfo(source, null, null, null, this.ilg).Load(type);
			}
		}

		// Token: 0x0400179C RID: 6044
		private int nextMethodNumber;

		// Token: 0x0400179D RID: 6045
		private Hashtable methodNames = new Hashtable();

		// Token: 0x0400179E RID: 6046
		private Dictionary<string, MethodBuilderInfo> methodBuilders = new Dictionary<string, MethodBuilderInfo>();

		// Token: 0x0400179F RID: 6047
		internal Dictionary<string, Type> CreatedTypes = new Dictionary<string, Type>();

		// Token: 0x040017A0 RID: 6048
		internal Dictionary<string, MemberInfo> memberInfos = new Dictionary<string, MemberInfo>();

		// Token: 0x040017A1 RID: 6049
		private ReflectionAwareILGen raCodeGen;

		// Token: 0x040017A2 RID: 6050
		private TypeScope[] scopes;

		// Token: 0x040017A3 RID: 6051
		private TypeDesc stringTypeDesc;

		// Token: 0x040017A4 RID: 6052
		private TypeDesc qnameTypeDesc;

		// Token: 0x040017A5 RID: 6053
		private string className;

		// Token: 0x040017A6 RID: 6054
		private TypeMapping[] referencedMethods;

		// Token: 0x040017A7 RID: 6055
		private int references;

		// Token: 0x040017A8 RID: 6056
		private Hashtable generatedMethods = new Hashtable();

		// Token: 0x040017A9 RID: 6057
		private ModuleBuilder moduleBuilder;

		// Token: 0x040017AA RID: 6058
		private TypeAttributes typeAttributes;

		// Token: 0x040017AB RID: 6059
		protected TypeBuilder typeBuilder;

		// Token: 0x040017AC RID: 6060
		protected CodeGenerator ilg;

		// Token: 0x040017AD RID: 6061
		private static Dictionary<string, Regex> regexs = new Dictionary<string, Regex>();
	}
}
