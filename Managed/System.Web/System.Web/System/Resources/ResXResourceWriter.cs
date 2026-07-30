using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;

namespace System.Resources
{
	// Token: 0x0200002A RID: 42
	internal class ResXResourceWriter : IResourceWriter, IDisposable
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00004B8C File Offset: 0x00002D8C
		public ResXResourceWriter(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanWrite)
			{
				throw new ArgumentException("stream is not writable.", "stream");
			}
			this.stream = stream;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004BC1 File Offset: 0x00002DC1
		public ResXResourceWriter(TextWriter textWriter)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			this.textwriter = textWriter;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004BDE File Offset: 0x00002DDE
		public ResXResourceWriter(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.filename = fileName;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004BFC File Offset: 0x00002DFC
		~ResXResourceWriter()
		{
			this.Dispose(false);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004C2C File Offset: 0x00002E2C
		private void InitWriter()
		{
			if (this.filename != null)
			{
				this.stream = File.Open(this.filename, FileMode.Create);
			}
			if (this.textwriter == null)
			{
				this.textwriter = new StreamWriter(this.stream, Encoding.UTF8);
			}
			this.writer = new XmlTextWriter(this.textwriter);
			this.writer.Formatting = Formatting.Indented;
			this.writer.WriteStartDocument();
			this.writer.WriteStartElement("root");
			this.writer.WriteRaw(ResXResourceWriter.schema);
			this.WriteHeader("resmimetype", "text/microsoft-resx");
			this.WriteHeader("version", "1.3");
			this.WriteHeader("reader", typeof(ResXResourceReader).AssemblyQualifiedName);
			this.WriteHeader("writer", typeof(ResXResourceWriter).AssemblyQualifiedName);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00004D10 File Offset: 0x00002F10
		private void WriteHeader(string name, string value)
		{
			this.writer.WriteStartElement("resheader");
			this.writer.WriteAttributeString("name", name);
			this.writer.WriteStartElement("value");
			this.writer.WriteString(value);
			this.writer.WriteEndElement();
			this.writer.WriteEndElement();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004D70 File Offset: 0x00002F70
		private void WriteNiceBase64(byte[] value, int offset, int length)
		{
			string text = Convert.ToBase64String(value, offset, length);
			StringBuilder stringBuilder = new StringBuilder(text, text.Length + (text.Length + 160) / 80 * 3);
			int i = 0;
			int num = 80 + Environment.NewLine.Length + 1;
			string text2 = Environment.NewLine + "\t";
			while (i < stringBuilder.Length)
			{
				stringBuilder.Insert(i, text2);
				i += num;
			}
			stringBuilder.Insert(stringBuilder.Length, Environment.NewLine);
			this.writer.WriteString(stringBuilder.ToString());
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004E04 File Offset: 0x00003004
		private void WriteBytes(string name, Type type, byte[] value, int offset, int length)
		{
			this.WriteBytes(name, type, value, offset, length, string.Empty);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004E18 File Offset: 0x00003018
		private void WriteBytes(string name, Type type, byte[] value, int offset, int length, string comment)
		{
			this.writer.WriteStartElement("data");
			this.writer.WriteAttributeString("name", name);
			if (type != null)
			{
				this.writer.WriteAttributeString("type", type.AssemblyQualifiedName);
				if (type != typeof(byte[]))
				{
					this.writer.WriteAttributeString("mimetype", ResXResourceWriter.ByteArraySerializedObjectMimeType);
				}
				this.writer.WriteStartElement("value");
				this.WriteNiceBase64(value, offset, length);
			}
			else
			{
				this.writer.WriteAttributeString("mimetype", ResXResourceWriter.BinSerializedObjectMimeType);
				this.writer.WriteStartElement("value");
				this.writer.WriteBase64(value, offset, length);
			}
			this.writer.WriteEndElement();
			if (comment != null && !comment.Equals(string.Empty))
			{
				this.writer.WriteStartElement("comment");
				this.writer.WriteString(comment);
				this.writer.WriteEndElement();
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004F2E File Offset: 0x0000312E
		private void WriteBytes(string name, Type type, byte[] value, string comment)
		{
			this.WriteBytes(name, type, value, 0, value.Length, comment);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004F3F File Offset: 0x0000313F
		private void WriteString(string name, string value)
		{
			this.WriteString(name, value, null);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004F4A File Offset: 0x0000314A
		private void WriteString(string name, string value, Type type)
		{
			this.WriteString(name, value, type, string.Empty);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004F5C File Offset: 0x0000315C
		private void WriteString(string name, string value, Type type, string comment)
		{
			this.writer.WriteStartElement("data");
			this.writer.WriteAttributeString("name", name);
			if (type != null)
			{
				this.writer.WriteAttributeString("type", type.AssemblyQualifiedName);
			}
			this.writer.WriteStartElement("value");
			this.writer.WriteString(value);
			this.writer.WriteEndElement();
			if (comment != null && !comment.Equals(string.Empty))
			{
				this.writer.WriteStartElement("comment");
				this.writer.WriteString(comment);
				this.writer.WriteEndElement();
			}
			this.writer.WriteEndElement();
			this.writer.WriteWhitespace("\n  ");
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00005028 File Offset: 0x00003228
		public void AddResource(string name, byte[] value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.written)
			{
				throw new InvalidOperationException("The resource is already generated.");
			}
			if (this.writer == null)
			{
				this.InitWriter();
			}
			this.WriteBytes(name, value.GetType(), value, null);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00005081 File Offset: 0x00003281
		public void AddResource(string name, object value)
		{
			this.AddResource(name, value, string.Empty);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00005090 File Offset: 0x00003290
		private void AddResource(string name, object value, string comment)
		{
			if (value is string)
			{
				this.AddResource(name, (string)value, comment);
				return;
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value != null && !value.GetType().IsSerializable)
			{
				throw new InvalidOperationException(string.Format("The element '{0}' of type '{1}' is not serializable.", name, value.GetType().Name));
			}
			if (this.written)
			{
				throw new InvalidOperationException("The resource is already generated.");
			}
			if (this.writer == null)
			{
				this.InitWriter();
			}
			if (value is byte[])
			{
				this.WriteBytes(name, value.GetType(), (byte[])value, comment);
				return;
			}
			if (value == null)
			{
				this.WriteString(name, "", typeof(ResXNullRef), comment);
				return;
			}
			TypeConverter converter = TypeDescriptor.GetConverter(value);
			if (value is ResXFileRef)
			{
				ResXFileRef resXFileRef = this.ProcessFileRefBasePath((ResXFileRef)value);
				string text = converter.ConvertToInvariantString(resXFileRef);
				this.WriteString(name, text, value.GetType(), comment);
				return;
			}
			if (converter != null && converter.CanConvertTo(typeof(string)) && converter.CanConvertFrom(typeof(string)))
			{
				string text2 = converter.ConvertToInvariantString(value);
				this.WriteString(name, text2, value.GetType(), comment);
				return;
			}
			if (converter != null && converter.CanConvertTo(typeof(byte[])) && converter.CanConvertFrom(typeof(byte[])))
			{
				byte[] array = (byte[])converter.ConvertTo(value, typeof(byte[]));
				this.WriteBytes(name, value.GetType(), array, comment);
				return;
			}
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			try
			{
				binaryFormatter.Serialize(memoryStream, value);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"Cannot add a ",
					value.GetType(),
					"because it cannot be serialized: ",
					ex.Message
				}));
			}
			this.WriteBytes(name, null, memoryStream.GetBuffer(), 0, (int)memoryStream.Length, comment);
			memoryStream.Close();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00005288 File Offset: 0x00003488
		public void AddResource(string name, string value)
		{
			this.AddResource(name, value, string.Empty);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00005298 File Offset: 0x00003498
		private void AddResource(string name, string value, string comment)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.written)
			{
				throw new InvalidOperationException("The resource is already generated.");
			}
			if (this.writer == null)
			{
				this.InitWriter();
			}
			this.WriteString(name, value, null, comment);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x0000393A File Offset: 0x00001B3A
		[global::System.MonoTODO("Stub, not implemented")]
		public virtual void AddAlias(string aliasName, AssemblyName assemblyName)
		{
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000052EC File Offset: 0x000034EC
		public void AddResource(ResXDataNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (this.writer == null)
			{
				this.InitWriter();
			}
			if (node.IsWritable)
			{
				this.WriteWritableNode(node);
				return;
			}
			if (node.FileRef != null)
			{
				this.AddResource(node.Name, node.FileRef, node.Comment);
				return;
			}
			this.AddResource(node.Name, node.GetValue(null), node.Comment);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000535F File Offset: 0x0000355F
		private ResXFileRef ProcessFileRefBasePath(ResXFileRef fileRef)
		{
			if (string.IsNullOrEmpty(this.BasePath))
			{
				return fileRef;
			}
			return new ResXFileRef(ResXResourceWriter.AbsoluteToRelativePath(this.BasePath, fileRef.FileName), fileRef.TypeName, fileRef.TextFileEncoding);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005392 File Offset: 0x00003592
		private static bool IsSeparator(char ch)
		{
			return ch == Path.DirectorySeparatorChar || ch == Path.AltDirectorySeparatorChar || ch == Path.VolumeSeparatorChar;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000053B0 File Offset: 0x000035B0
		private unsafe static string AbsoluteToRelativePath(string baseDirectoryPath, string absPath)
		{
			if (string.IsNullOrEmpty(baseDirectoryPath))
			{
				return absPath;
			}
			baseDirectoryPath = baseDirectoryPath.TrimEnd(new char[] { Path.DirectorySeparatorChar });
			fixed (string text = baseDirectoryPath)
			{
				char* ptr = text;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr2 = absPath;
				if (ptr2 != null)
				{
					ptr2 += RuntimeHelpers.OffsetToStringData / 2;
				}
				char* ptr3 = ptr + baseDirectoryPath.Length;
				char* ptr4 = ptr2 + absPath.Length;
				char* ptr5 = ptr4;
				char* ptr6 = ptr3;
				int num = 0;
				char* ptr7 = ptr2;
				char* ptr8 = ptr;
				while (ptr7 < ptr4 && *ptr7 == *ptr8)
				{
					if (ResXResourceWriter.IsSeparator(*ptr7))
					{
						num++;
						ptr5 = ptr7 + 1;
						ptr6 = ptr8;
					}
					ptr7++;
					ptr8++;
					if (ptr8 >= ptr3)
					{
						if (ptr7 >= ptr4 || ResXResourceWriter.IsSeparator(*ptr7))
						{
							num++;
							ptr5 = ptr7 + 1;
							ptr6 = ptr8;
							break;
						}
						break;
					}
				}
				if (num == 0)
				{
					return absPath;
				}
				if (ptr5 >= ptr4)
				{
					return ".";
				}
				if (ptr7 >= ptr4 && ResXResourceWriter.IsSeparator(*ptr8))
				{
					ptr5 = ptr7 + 1;
					ptr6 = ptr8;
				}
				int num2 = 0;
				while (ptr6 < ptr3)
				{
					if (ResXResourceWriter.IsSeparator(*ptr6))
					{
						num2++;
					}
					ptr6++;
				}
				char[] array = new char[(long)((num2 * 2 + num2) * 2 + ptr4 / 2 - ptr5)];
				char[] array2;
				char* ptr9;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr9 = null;
				}
				else
				{
					ptr9 = &array2[0];
				}
				char* ptr10 = ptr9;
				for (int i = 0; i < num2; i++)
				{
					*(ptr10++) = '.';
					*(ptr10++) = '.';
					*(ptr10++) = Path.DirectorySeparatorChar;
				}
				while (ptr5 < ptr4)
				{
					*(ptr10++) = *(ptr5++);
				}
				array2 = null;
				return new string(array);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00005564 File Offset: 0x00003764
		private void WriteWritableNode(ResXDataNode node)
		{
			this.writer.WriteStartElement("data");
			this.writer.WriteAttributeString("name", node.Name);
			if (node.Type != null && !node.Type.Equals(string.Empty))
			{
				this.writer.WriteAttributeString("type", node.Type);
			}
			if (node.MimeType != null && !node.MimeType.Equals(string.Empty))
			{
				this.writer.WriteAttributeString("mimetype", node.MimeType);
			}
			this.writer.WriteStartElement("value");
			this.writer.WriteString(node.DataString);
			this.writer.WriteEndElement();
			if (node.Comment != null && !node.Comment.Equals(string.Empty))
			{
				this.writer.WriteStartElement("comment");
				this.writer.WriteString(node.Comment);
				this.writer.WriteEndElement();
			}
			this.writer.WriteEndElement();
			this.writer.WriteWhitespace("\n  ");
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005684 File Offset: 0x00003884
		public void AddMetadata(string name, string value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.written)
			{
				throw new InvalidOperationException("The resource is already generated.");
			}
			if (this.writer == null)
			{
				this.InitWriter();
			}
			this.writer.WriteStartElement("metadata");
			this.writer.WriteAttributeString("name", name);
			this.writer.WriteAttributeString("xml:space", "preserve");
			this.writer.WriteElementString("value", value);
			this.writer.WriteEndElement();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00005720 File Offset: 0x00003920
		public void AddMetadata(string name, byte[] value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this.written)
			{
				throw new InvalidOperationException("The resource is already generated.");
			}
			if (this.writer == null)
			{
				this.InitWriter();
			}
			this.writer.WriteStartElement("metadata");
			this.writer.WriteAttributeString("name", name);
			this.writer.WriteAttributeString("type", value.GetType().AssemblyQualifiedName);
			this.writer.WriteStartElement("value");
			this.WriteNiceBase64(value, 0, value.Length);
			this.writer.WriteEndElement();
			this.writer.WriteEndElement();
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000057D8 File Offset: 0x000039D8
		public void AddMetadata(string name, object value)
		{
			if (value is string)
			{
				this.AddMetadata(name, (string)value);
				return;
			}
			if (value is byte[])
			{
				this.AddMetadata(name, (byte[])value);
				return;
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!value.GetType().IsSerializable)
			{
				throw new InvalidOperationException(string.Format("The element '{0}' of type '{1}' is not serializable.", name, value.GetType().Name));
			}
			if (this.written)
			{
				throw new InvalidOperationException("The resource is already generated.");
			}
			if (this.writer == null)
			{
				this.InitWriter();
			}
			Type type = value.GetType();
			TypeConverter converter = TypeDescriptor.GetConverter(value);
			if (converter != null && converter.CanConvertTo(typeof(string)) && converter.CanConvertFrom(typeof(string)))
			{
				string text = converter.ConvertToInvariantString(value);
				this.writer.WriteStartElement("metadata");
				this.writer.WriteAttributeString("name", name);
				if (type != null)
				{
					this.writer.WriteAttributeString("type", type.AssemblyQualifiedName);
				}
				this.writer.WriteStartElement("value");
				this.writer.WriteString(text);
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
				this.writer.WriteWhitespace("\n  ");
				return;
			}
			if (converter != null && converter.CanConvertTo(typeof(byte[])) && converter.CanConvertFrom(typeof(byte[])))
			{
				byte[] array = (byte[])converter.ConvertTo(value, typeof(byte[]));
				this.writer.WriteStartElement("metadata");
				this.writer.WriteAttributeString("name", name);
				if (type != null)
				{
					this.writer.WriteAttributeString("type", type.AssemblyQualifiedName);
					this.writer.WriteAttributeString("mimetype", ResXResourceWriter.ByteArraySerializedObjectMimeType);
					this.writer.WriteStartElement("value");
					this.WriteNiceBase64(array, 0, array.Length);
				}
				else
				{
					this.writer.WriteAttributeString("mimetype", ResXResourceWriter.BinSerializedObjectMimeType);
					this.writer.WriteStartElement("value");
					this.writer.WriteBase64(array, 0, array.Length);
				}
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
				return;
			}
			MemoryStream memoryStream = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			try
			{
				binaryFormatter.Serialize(memoryStream, value);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(string.Concat(new object[]
				{
					"Cannot add a ",
					value.GetType(),
					"because it cannot be serialized: ",
					ex.Message
				}));
			}
			this.writer.WriteStartElement("metadata");
			this.writer.WriteAttributeString("name", name);
			if (type != null)
			{
				this.writer.WriteAttributeString("type", type.AssemblyQualifiedName);
				this.writer.WriteAttributeString("mimetype", ResXResourceWriter.ByteArraySerializedObjectMimeType);
				this.writer.WriteStartElement("value");
				this.WriteNiceBase64(memoryStream.GetBuffer(), 0, memoryStream.GetBuffer().Length);
			}
			else
			{
				this.writer.WriteAttributeString("mimetype", ResXResourceWriter.BinSerializedObjectMimeType);
				this.writer.WriteStartElement("value");
				this.writer.WriteBase64(memoryStream.GetBuffer(), 0, memoryStream.GetBuffer().Length);
			}
			this.writer.WriteEndElement();
			this.writer.WriteEndElement();
			memoryStream.Close();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00005B80 File Offset: 0x00003D80
		public void Close()
		{
			if (!this.written)
			{
				this.Generate();
			}
			if (this.writer != null)
			{
				this.writer.Close();
				this.stream = null;
				this.filename = null;
				this.textwriter = null;
			}
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00005BB8 File Offset: 0x00003DB8
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005BC7 File Offset: 0x00003DC7
		public void Generate()
		{
			if (this.written)
			{
				throw new InvalidOperationException("The resource is already generated.");
			}
			this.written = true;
			this.writer.WriteEndElement();
			this.writer.Flush();
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00005BF9 File Offset: 0x00003DF9
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005C04 File Offset: 0x00003E04
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00005C0C File Offset: 0x00003E0C
		public string BasePath
		{
			get
			{
				return this.base_path;
			}
			set
			{
				this.base_path = value;
			}
		}

		// Token: 0x04000D81 RID: 3457
		private string filename;

		// Token: 0x04000D82 RID: 3458
		private Stream stream;

		// Token: 0x04000D83 RID: 3459
		private TextWriter textwriter;

		// Token: 0x04000D84 RID: 3460
		private XmlTextWriter writer;

		// Token: 0x04000D85 RID: 3461
		private bool written;

		// Token: 0x04000D86 RID: 3462
		private string base_path;

		// Token: 0x04000D87 RID: 3463
		public static readonly string BinSerializedObjectMimeType = "application/x-microsoft.net.object.binary.base64";

		// Token: 0x04000D88 RID: 3464
		public static readonly string ByteArraySerializedObjectMimeType = "application/x-microsoft.net.object.bytearray.base64";

		// Token: 0x04000D89 RID: 3465
		public static readonly string DefaultSerializedObjectMimeType = ResXResourceWriter.BinSerializedObjectMimeType;

		// Token: 0x04000D8A RID: 3466
		public static readonly string ResMimeType = "text/microsoft-resx";

		// Token: 0x04000D8B RID: 3467
		public static readonly string ResourceSchema = ResXResourceWriter.schema;

		// Token: 0x04000D8C RID: 3468
		public static readonly string SoapSerializedObjectMimeType = "application/x-microsoft.net.object.soap.base64";

		// Token: 0x04000D8D RID: 3469
		public static readonly string Version = "2.0";

		// Token: 0x04000D8E RID: 3470
		private static string schema = "\n  <xsd:schema id='root' xmlns='' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:msdata='urn:schemas-microsoft-com:xml-msdata'>\n    <xsd:element name='root' msdata:IsDataSet='true'>\n      <xsd:complexType>\n        <xsd:choice maxOccurs='unbounded'>\n          <xsd:element name='data'>\n            <xsd:complexType>\n              <xsd:sequence>\n                <xsd:element name='value' type='xsd:string' minOccurs='0' msdata:Ordinal='1' />\n                <xsd:element name='comment' type='xsd:string' minOccurs='0' msdata:Ordinal='2' />\n              </xsd:sequence>\n              <xsd:attribute name='name' type='xsd:string' msdata:Ordinal='1' />\n              <xsd:attribute name='type' type='xsd:string' msdata:Ordinal='3' />\n              <xsd:attribute name='mimetype' type='xsd:string' msdata:Ordinal='4' />\n            </xsd:complexType>\n          </xsd:element>\n          <xsd:element name='resheader'>\n            <xsd:complexType>\n              <xsd:sequence>\n                <xsd:element name='value' type='xsd:string' minOccurs='0' msdata:Ordinal='1' />\n              </xsd:sequence>\n              <xsd:attribute name='name' type='xsd:string' use='required' />\n            </xsd:complexType>\n          </xsd:element>\n        </xsd:choice>\n      </xsd:complexType>\n    </xsd:element>\n  </xsd:schema>\n".Replace("'", "\"");
	}
}
