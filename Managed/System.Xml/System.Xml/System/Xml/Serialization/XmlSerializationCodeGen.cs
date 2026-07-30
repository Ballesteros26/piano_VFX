using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000349 RID: 841
	internal class XmlSerializationCodeGen
	{
		// Token: 0x060020B2 RID: 8370 RVA: 0x000B68B0 File Offset: 0x000B4AB0
		internal XmlSerializationCodeGen(IndentedWriter writer, TypeScope[] scopes, string access, string className)
		{
			this.writer = writer;
			this.scopes = scopes;
			if (scopes.Length != 0)
			{
				this.stringTypeDesc = scopes[0].GetTypeDesc(typeof(string));
				this.qnameTypeDesc = scopes[0].GetTypeDesc(typeof(XmlQualifiedName));
			}
			this.raCodeGen = new ReflectionAwareCodeGen(writer);
			this.className = className;
			this.access = access;
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x000B6936 File Offset: 0x000B4B36
		internal IndentedWriter Writer
		{
			get
			{
				return this.writer;
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x060020B4 RID: 8372 RVA: 0x000B693E File Offset: 0x000B4B3E
		// (set) Token: 0x060020B5 RID: 8373 RVA: 0x000B6946 File Offset: 0x000B4B46
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

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x000B694F File Offset: 0x000B4B4F
		internal ReflectionAwareCodeGen RaCodeGen
		{
			get
			{
				return this.raCodeGen;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x000B6957 File Offset: 0x000B4B57
		internal TypeDesc StringTypeDesc
		{
			get
			{
				return this.stringTypeDesc;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060020B8 RID: 8376 RVA: 0x000B695F File Offset: 0x000B4B5F
		internal TypeDesc QnameTypeDesc
		{
			get
			{
				return this.qnameTypeDesc;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x000B6967 File Offset: 0x000B4B67
		internal string ClassName
		{
			get
			{
				return this.className;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060020BA RID: 8378 RVA: 0x000B696F File Offset: 0x000B4B6F
		internal string Access
		{
			get
			{
				return this.access;
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x000B6977 File Offset: 0x000B4B77
		internal TypeScope[] Scopes
		{
			get
			{
				return this.scopes;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x000B697F File Offset: 0x000B4B7F
		internal Hashtable MethodNames
		{
			get
			{
				return this.methodNames;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x000B6987 File Offset: 0x000B4B87
		internal Hashtable GeneratedMethods
		{
			get
			{
				return this.generatedMethods;
			}
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x00002F50 File Offset: 0x00001150
		internal virtual void GenerateMethod(TypeMapping mapping)
		{
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x000B6990 File Offset: 0x000B4B90
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

		// Token: 0x060020C0 RID: 8384 RVA: 0x000B69C8 File Offset: 0x000B4BC8
		internal string ReferenceMapping(TypeMapping mapping)
		{
			if (!mapping.IsSoap && this.generatedMethods[mapping] == null)
			{
				this.referencedMethods = this.EnsureArrayIndex(this.referencedMethods, this.references);
				TypeMapping[] array = this.referencedMethods;
				int num = this.references;
				this.references = num + 1;
				array[num] = mapping;
			}
			return (string)this.methodNames[mapping];
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x000B6A30 File Offset: 0x000B4C30
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

		// Token: 0x060020C2 RID: 8386 RVA: 0x000B6A65 File Offset: 0x000B4C65
		internal void WriteQuotedCSharpString(string value)
		{
			this.raCodeGen.WriteQuotedCSharpString(value);
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000B6A74 File Offset: 0x000B4C74
		internal void GenerateHashtableGetBegin(string privateName, string publicName)
		{
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.Write(" ");
			this.writer.Write(privateName);
			this.writer.WriteLine(" = null;");
			this.writer.Write("public override ");
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.Write(" ");
			this.writer.Write(publicName);
			this.writer.WriteLine(" {");
			IndentedWriter indentedWriter = this.writer;
			int num = indentedWriter.Indent;
			indentedWriter.Indent = num + 1;
			this.writer.WriteLine("get {");
			IndentedWriter indentedWriter2 = this.writer;
			num = indentedWriter2.Indent;
			indentedWriter2.Indent = num + 1;
			this.writer.Write("if (");
			this.writer.Write(privateName);
			this.writer.WriteLine(" == null) {");
			IndentedWriter indentedWriter3 = this.writer;
			num = indentedWriter3.Indent;
			indentedWriter3.Indent = num + 1;
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.Write(" _tmp = new ");
			this.writer.Write(typeof(Hashtable).FullName);
			this.writer.WriteLine("();");
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000B6BEC File Offset: 0x000B4DEC
		internal void GenerateHashtableGetEnd(string privateName)
		{
			this.writer.Write("if (");
			this.writer.Write(privateName);
			this.writer.Write(" == null) ");
			this.writer.Write(privateName);
			this.writer.WriteLine(" = _tmp;");
			IndentedWriter indentedWriter = this.writer;
			int num = indentedWriter.Indent;
			indentedWriter.Indent = num - 1;
			this.writer.WriteLine("}");
			this.writer.Write("return ");
			this.writer.Write(privateName);
			this.writer.WriteLine(";");
			IndentedWriter indentedWriter2 = this.writer;
			num = indentedWriter2.Indent;
			indentedWriter2.Indent = num - 1;
			this.writer.WriteLine("}");
			IndentedWriter indentedWriter3 = this.writer;
			num = indentedWriter3.Indent;
			indentedWriter3.Indent = num - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x000B6CDC File Offset: 0x000B4EDC
		internal void GeneratePublicMethods(string privateName, string publicName, string[] methods, XmlMapping[] xmlMappings)
		{
			this.GenerateHashtableGetBegin(privateName, publicName);
			if (methods != null && methods.Length != 0 && xmlMappings != null && xmlMappings.Length == methods.Length)
			{
				for (int i = 0; i < methods.Length; i++)
				{
					if (methods[i] != null)
					{
						this.writer.Write("_tmp[");
						this.WriteQuotedCSharpString(xmlMappings[i].Key);
						this.writer.Write("] = ");
						this.WriteQuotedCSharpString(methods[i]);
						this.writer.WriteLine(";");
					}
				}
			}
			this.GenerateHashtableGetEnd(privateName);
		}

		// Token: 0x060020C6 RID: 8390 RVA: 0x000B6D68 File Offset: 0x000B4F68
		internal void GenerateSupportedTypes(Type[] types)
		{
			this.writer.Write("public override ");
			this.writer.Write(typeof(bool).FullName);
			this.writer.Write(" CanSerialize(");
			this.writer.Write(typeof(Type).FullName);
			this.writer.WriteLine(" type) {");
			IndentedWriter indentedWriter = this.writer;
			int num = indentedWriter.Indent;
			indentedWriter.Indent = num + 1;
			Hashtable hashtable = new Hashtable();
			foreach (Type type in types)
			{
				if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && hashtable[type] == null && !DynamicAssemblies.IsTypeDynamic(type) && !type.IsGenericType && (!type.ContainsGenericParameters || !DynamicAssemblies.IsTypeDynamic(type.GetGenericArguments())))
				{
					hashtable[type] = type;
					this.writer.Write("if (type == typeof(");
					this.writer.Write(CodeIdentifier.GetCSharpName(type));
					this.writer.WriteLine(")) return true;");
				}
			}
			this.writer.WriteLine("return false;");
			IndentedWriter indentedWriter2 = this.writer;
			num = indentedWriter2.Indent;
			indentedWriter2.Indent = num - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x060020C7 RID: 8391 RVA: 0x000B6EC4 File Offset: 0x000B50C4
		internal string GenerateBaseSerializer(string baseSerializer, string readerClass, string writerClass, CodeIdentifiers classes)
		{
			baseSerializer = CodeIdentifier.MakeValid(baseSerializer);
			baseSerializer = classes.AddUnique(baseSerializer, baseSerializer);
			this.writer.WriteLine();
			this.writer.Write("public abstract class ");
			this.writer.Write(CodeIdentifier.GetCSharpName(baseSerializer));
			this.writer.Write(" : ");
			this.writer.Write(typeof(XmlSerializer).FullName);
			this.writer.WriteLine(" {");
			IndentedWriter indentedWriter = this.writer;
			int num = indentedWriter.Indent;
			indentedWriter.Indent = num + 1;
			this.writer.Write("protected override ");
			this.writer.Write(typeof(XmlSerializationReader).FullName);
			this.writer.WriteLine(" CreateReader() {");
			IndentedWriter indentedWriter2 = this.writer;
			num = indentedWriter2.Indent;
			indentedWriter2.Indent = num + 1;
			this.writer.Write("return new ");
			this.writer.Write(readerClass);
			this.writer.WriteLine("();");
			IndentedWriter indentedWriter3 = this.writer;
			num = indentedWriter3.Indent;
			indentedWriter3.Indent = num - 1;
			this.writer.WriteLine("}");
			this.writer.Write("protected override ");
			this.writer.Write(typeof(XmlSerializationWriter).FullName);
			this.writer.WriteLine(" CreateWriter() {");
			IndentedWriter indentedWriter4 = this.writer;
			num = indentedWriter4.Indent;
			indentedWriter4.Indent = num + 1;
			this.writer.Write("return new ");
			this.writer.Write(writerClass);
			this.writer.WriteLine("();");
			IndentedWriter indentedWriter5 = this.writer;
			num = indentedWriter5.Indent;
			indentedWriter5.Indent = num - 1;
			this.writer.WriteLine("}");
			IndentedWriter indentedWriter6 = this.writer;
			num = indentedWriter6.Indent;
			indentedWriter6.Indent = num - 1;
			this.writer.WriteLine("}");
			return baseSerializer;
		}

		// Token: 0x060020C8 RID: 8392 RVA: 0x000B70C8 File Offset: 0x000B52C8
		internal string GenerateTypedSerializer(string readMethod, string writeMethod, XmlMapping mapping, CodeIdentifiers classes, string baseSerializer, string readerClass, string writerClass)
		{
			string text = CodeIdentifier.MakeValid(Accessor.UnescapeName(mapping.Accessor.Mapping.TypeDesc.Name));
			text = classes.AddUnique(text + "Serializer", mapping);
			this.writer.WriteLine();
			this.writer.Write("public sealed class ");
			this.writer.Write(CodeIdentifier.GetCSharpName(text));
			this.writer.Write(" : ");
			this.writer.Write(baseSerializer);
			this.writer.WriteLine(" {");
			IndentedWriter indentedWriter = this.writer;
			int num = indentedWriter.Indent;
			indentedWriter.Indent = num + 1;
			this.writer.WriteLine();
			this.writer.Write("public override ");
			this.writer.Write(typeof(bool).FullName);
			this.writer.Write(" CanDeserialize(");
			this.writer.Write(typeof(XmlReader).FullName);
			this.writer.WriteLine(" xmlReader) {");
			IndentedWriter indentedWriter2 = this.writer;
			num = indentedWriter2.Indent;
			indentedWriter2.Indent = num + 1;
			if (mapping.Accessor.Any)
			{
				this.writer.WriteLine("return true;");
			}
			else
			{
				this.writer.Write("return xmlReader.IsStartElement(");
				this.WriteQuotedCSharpString(mapping.Accessor.Name);
				this.writer.Write(", ");
				this.WriteQuotedCSharpString(mapping.Accessor.Namespace);
				this.writer.WriteLine(");");
			}
			IndentedWriter indentedWriter3 = this.writer;
			num = indentedWriter3.Indent;
			indentedWriter3.Indent = num - 1;
			this.writer.WriteLine("}");
			if (writeMethod != null)
			{
				this.writer.WriteLine();
				this.writer.Write("protected override void Serialize(object objectToSerialize, ");
				this.writer.Write(typeof(XmlSerializationWriter).FullName);
				this.writer.WriteLine(" writer) {");
				IndentedWriter indentedWriter4 = this.writer;
				num = indentedWriter4.Indent;
				indentedWriter4.Indent = num + 1;
				this.writer.Write("((");
				this.writer.Write(writerClass);
				this.writer.Write(")writer).");
				this.writer.Write(writeMethod);
				this.writer.Write("(");
				if (mapping is XmlMembersMapping)
				{
					this.writer.Write("(object[])");
				}
				this.writer.WriteLine("objectToSerialize);");
				IndentedWriter indentedWriter5 = this.writer;
				num = indentedWriter5.Indent;
				indentedWriter5.Indent = num - 1;
				this.writer.WriteLine("}");
			}
			if (readMethod != null)
			{
				this.writer.WriteLine();
				this.writer.Write("protected override object Deserialize(");
				this.writer.Write(typeof(XmlSerializationReader).FullName);
				this.writer.WriteLine(" reader) {");
				IndentedWriter indentedWriter6 = this.writer;
				num = indentedWriter6.Indent;
				indentedWriter6.Indent = num + 1;
				this.writer.Write("return ((");
				this.writer.Write(readerClass);
				this.writer.Write(")reader).");
				this.writer.Write(readMethod);
				this.writer.WriteLine("();");
				IndentedWriter indentedWriter7 = this.writer;
				num = indentedWriter7.Indent;
				indentedWriter7.Indent = num - 1;
				this.writer.WriteLine("}");
			}
			IndentedWriter indentedWriter8 = this.writer;
			num = indentedWriter8.Indent;
			indentedWriter8.Indent = num - 1;
			this.writer.WriteLine("}");
			return text;
		}

		// Token: 0x060020C9 RID: 8393 RVA: 0x000B747C File Offset: 0x000B567C
		private void GenerateTypedSerializers(Hashtable serializers)
		{
			string text = "typedSerializers";
			this.GenerateHashtableGetBegin(text, "TypedSerializers");
			foreach (object obj in serializers.Keys)
			{
				string text2 = (string)obj;
				this.writer.Write("_tmp.Add(");
				this.WriteQuotedCSharpString(text2);
				this.writer.Write(", new ");
				this.writer.Write((string)serializers[text2]);
				this.writer.WriteLine("());");
			}
			this.GenerateHashtableGetEnd("typedSerializers");
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x000B753C File Offset: 0x000B573C
		private void GenerateGetSerializer(Hashtable serializers, XmlMapping[] xmlMappings)
		{
			this.writer.Write("public override ");
			this.writer.Write(typeof(XmlSerializer).FullName);
			this.writer.Write(" GetSerializer(");
			this.writer.Write(typeof(Type).FullName);
			this.writer.WriteLine(" type) {");
			IndentedWriter indentedWriter = this.writer;
			int num = indentedWriter.Indent;
			indentedWriter.Indent = num + 1;
			for (int i = 0; i < xmlMappings.Length; i++)
			{
				if (xmlMappings[i] is XmlTypeMapping)
				{
					Type type = xmlMappings[i].Accessor.Mapping.TypeDesc.Type;
					if (!(type == null) && (type.IsPublic || type.IsNestedPublic) && !DynamicAssemblies.IsTypeDynamic(type) && !type.IsGenericType && (!type.ContainsGenericParameters || !DynamicAssemblies.IsTypeDynamic(type.GetGenericArguments())))
					{
						this.writer.Write("if (type == typeof(");
						this.writer.Write(CodeIdentifier.GetCSharpName(type));
						this.writer.Write(")) return new ");
						this.writer.Write((string)serializers[xmlMappings[i].Key]);
						this.writer.WriteLine("();");
					}
				}
			}
			this.writer.WriteLine("return null;");
			IndentedWriter indentedWriter2 = this.writer;
			num = indentedWriter2.Indent;
			indentedWriter2.Indent = num - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x000B76D4 File Offset: 0x000B58D4
		internal void GenerateSerializerContract(string className, XmlMapping[] xmlMappings, Type[] types, string readerType, string[] readMethods, string writerType, string[] writerMethods, Hashtable serializers)
		{
			this.writer.WriteLine();
			this.writer.Write("public class XmlSerializerContract : global::");
			this.writer.Write(typeof(XmlSerializerImplementation).FullName);
			this.writer.WriteLine(" {");
			IndentedWriter indentedWriter = this.writer;
			int num = indentedWriter.Indent;
			indentedWriter.Indent = num + 1;
			this.writer.Write("public override global::");
			this.writer.Write(typeof(XmlSerializationReader).FullName);
			this.writer.Write(" Reader { get { return new ");
			this.writer.Write(readerType);
			this.writer.WriteLine("(); } }");
			this.writer.Write("public override global::");
			this.writer.Write(typeof(XmlSerializationWriter).FullName);
			this.writer.Write(" Writer { get { return new ");
			this.writer.Write(writerType);
			this.writer.WriteLine("(); } }");
			this.GeneratePublicMethods("readMethods", "ReadMethods", readMethods, xmlMappings);
			this.GeneratePublicMethods("writeMethods", "WriteMethods", writerMethods, xmlMappings);
			this.GenerateTypedSerializers(serializers);
			this.GenerateSupportedTypes(types);
			this.GenerateGetSerializer(serializers, xmlMappings);
			IndentedWriter indentedWriter2 = this.writer;
			num = indentedWriter2.Indent;
			indentedWriter2.Indent = num - 1;
			this.writer.WriteLine("}");
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x000B784C File Offset: 0x000B5A4C
		internal static bool IsWildcard(SpecialMapping mapping)
		{
			if (mapping is SerializableMapping)
			{
				return ((SerializableMapping)mapping).IsAny;
			}
			return mapping.TypeDesc.CanBeElementValue;
		}

		// Token: 0x04001790 RID: 6032
		private IndentedWriter writer;

		// Token: 0x04001791 RID: 6033
		private int nextMethodNumber;

		// Token: 0x04001792 RID: 6034
		private Hashtable methodNames = new Hashtable();

		// Token: 0x04001793 RID: 6035
		private ReflectionAwareCodeGen raCodeGen;

		// Token: 0x04001794 RID: 6036
		private TypeScope[] scopes;

		// Token: 0x04001795 RID: 6037
		private TypeDesc stringTypeDesc;

		// Token: 0x04001796 RID: 6038
		private TypeDesc qnameTypeDesc;

		// Token: 0x04001797 RID: 6039
		private string access;

		// Token: 0x04001798 RID: 6040
		private string className;

		// Token: 0x04001799 RID: 6041
		private TypeMapping[] referencedMethods;

		// Token: 0x0400179A RID: 6042
		private int references;

		// Token: 0x0400179B RID: 6043
		private Hashtable generatedMethods = new Hashtable();
	}
}
