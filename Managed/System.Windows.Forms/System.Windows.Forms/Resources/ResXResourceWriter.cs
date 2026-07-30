using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;

namespace System.Resources
{
	/// <summary>Writes resources in an XML resource (.resx) file or an output stream.</summary>
	// Token: 0x02000011 RID: 17
	public class ResXResourceWriter : IDisposable, IResourceWriter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceWriter" /> class that writes the resources to the specified stream object.</summary>
		/// <param name="stream">The output stream. </param>
		// Token: 0x06000055 RID: 85 RVA: 0x000031EC File Offset: 0x000013EC
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

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceWriter" /> class that writes to the specified <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="textWriter">The <see cref="T:System.IO.TextWriter" /> object to send the output to. </param>
		// Token: 0x06000056 RID: 86 RVA: 0x00003228 File Offset: 0x00001428
		public ResXResourceWriter(TextWriter textWriter)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			this.textwriter = textWriter;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceWriter" /> class that writes the resources to the specified file.</summary>
		/// <param name="fileName">The output file name. </param>
		// Token: 0x06000057 RID: 87 RVA: 0x00003248 File Offset: 0x00001448
		public ResXResourceWriter(string fileName)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			this.filename = fileName;
		}

		/// <summary>This member overrides the <see cref="M:System.Object.Finalize" /> method. </summary>
		// Token: 0x06000059 RID: 89 RVA: 0x000032D4 File Offset: 0x000014D4
		~ResXResourceWriter()
		{
			this.Dispose(false);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003310 File Offset: 0x00001510
		private void InitWriter()
		{
			if (this.filename != null)
			{
				this.stream = File.OpenWrite(this.filename);
			}
			if (this.textwriter == null)
			{
				this.textwriter = new StreamWriter(this.stream, Encoding.UTF8);
			}
			this.writer = new XmlTextWriter(this.textwriter);
			this.writer.Formatting = 1;
			this.writer.WriteStartDocument();
			this.writer.WriteStartElement("root");
			this.writer.WriteRaw(ResXResourceWriter.schema);
			this.WriteHeader("resmimetype", "text/microsoft-resx");
			this.WriteHeader("version", "1.3");
			this.WriteHeader("reader", typeof(ResXResourceReader).AssemblyQualifiedName);
			this.WriteHeader("writer", typeof(ResXResourceWriter).AssemblyQualifiedName);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000033F8 File Offset: 0x000015F8
		private void WriteHeader(string name, string value)
		{
			this.writer.WriteStartElement("resheader");
			this.writer.WriteAttributeString("name", name);
			this.writer.WriteStartElement("value");
			this.writer.WriteString(value);
			this.writer.WriteEndElement();
			this.writer.WriteEndElement();
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003458 File Offset: 0x00001658
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

		// Token: 0x0600005D RID: 93 RVA: 0x000034F4 File Offset: 0x000016F4
		private void WriteBytes(string name, Type type, byte[] value, int offset, int length)
		{
			this.WriteBytes(name, type, value, offset, length, string.Empty);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003508 File Offset: 0x00001708
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

		// Token: 0x0600005F RID: 95 RVA: 0x00003624 File Offset: 0x00001824
		private void WriteBytes(string name, Type type, byte[] value)
		{
			this.WriteBytes(name, type, value, 0, value.Length);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003634 File Offset: 0x00001834
		private void WriteString(string name, string value)
		{
			this.WriteString(name, value, null);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003640 File Offset: 0x00001840
		private void WriteString(string name, string value, Type type)
		{
			this.WriteString(name, value, type, string.Empty);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003650 File Offset: 0x00001850
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

		/// <summary>Adds a named resource specified as a byte array to the list of resources to write.</summary>
		/// <param name="name">The name of the resource. </param>
		/// <param name="value">The value of the resource to add as an 8-bit unsigned integer array. </param>
		// Token: 0x06000063 RID: 99 RVA: 0x0000371C File Offset: 0x0000191C
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
			this.WriteBytes(name, value.GetType(), value);
		}

		/// <summary>Adds a named resource specified as an object to the list of resources to write.</summary>
		/// <param name="name">The name of the resource. </param>
		/// <param name="value">The value of the resource. </param>
		// Token: 0x06000064 RID: 100 RVA: 0x00003780 File Offset: 0x00001980
		public void AddResource(string name, object value)
		{
			this.AddResource(name, value, string.Empty);
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00003790 File Offset: 0x00001990
		private void AddResource(string name, object value, string comment)
		{
			if (value is string)
			{
				this.AddResource(name, (string)value);
				return;
			}
			if (value is byte[])
			{
				this.AddResource(name, (byte[])value);
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
			TypeConverter converter = TypeDescriptor.GetConverter(value);
			if (converter != null && converter.CanConvertTo(typeof(string)) && converter.CanConvertFrom(typeof(string)))
			{
				string text = converter.ConvertToInvariantString(value);
				this.WriteString(name, text, value.GetType());
				return;
			}
			if (converter != null && converter.CanConvertTo(typeof(byte[])) && converter.CanConvertFrom(typeof(byte[])))
			{
				byte[] array = (byte[])converter.ConvertTo(value, typeof(byte[]));
				this.WriteBytes(name, value.GetType(), array);
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

		/// <summary>Adds a string resource to the resources.</summary>
		/// <param name="name">The name of the resource. </param>
		/// <param name="value">The value of the resource. </param>
		// Token: 0x06000066 RID: 102 RVA: 0x00003978 File Offset: 0x00001B78
		public void AddResource(string name, string value)
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
			this.WriteString(name, value);
		}

		/// <summary>Adds the specified alias to a list of aliases. </summary>
		/// <param name="aliasName">The name of the alias.</param>
		/// <param name="assemblyName">The name of the assembly represented by <paramref name="aliasName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyName" /> is null.</exception>
		// Token: 0x06000067 RID: 103 RVA: 0x000039D8 File Offset: 0x00001BD8
		[MonoTODO("Stub, not implemented")]
		public virtual void AddAlias(string aliasName, AssemblyName assemblyName)
		{
		}

		/// <summary>Adds a named resource specified in a <see cref="T:System.Resources.ResXDataNode" /> object to the list of resources to write.</summary>
		/// <param name="node">A <see cref="T:System.Resources.ResXDataNode" /> object that contains a resource name/value pair.</param>
		// Token: 0x06000068 RID: 104 RVA: 0x000039DC File Offset: 0x00001BDC
		public void AddResource(ResXDataNode node)
		{
			this.AddResource(node.Name, node.Value, node.Comment);
		}

		/// <summary>Adds a design-time property whose value is specified as a string to the list of resources to write.</summary>
		/// <param name="name">The name of a property.</param>
		/// <param name="value">A string that is the value of the property to add.</param>
		/// <exception cref="T:System.InvalidOperationException">The resource specified by the <paramref name="name" /> property has already been added.</exception>
		// Token: 0x06000069 RID: 105 RVA: 0x00003A04 File Offset: 0x00001C04
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

		/// <summary>Adds a design-time property whose value is specifed as a byte array to the list of resources to write.</summary>
		/// <param name="name">The name of a property.</param>
		/// <param name="value">A byte array containing the value of the property to add.</param>
		/// <exception cref="T:System.InvalidOperationException">The resource specified by the <paramref name="name" /> parameter has already been added.</exception>
		// Token: 0x0600006A RID: 106 RVA: 0x00003AAC File Offset: 0x00001CAC
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

		/// <summary>Adds a design-time property whose value is specified as an object to the list of resources to write.</summary>
		/// <param name="name">The name of a property.</param>
		/// <param name="value">An object that is the value of the property to add.</param>
		/// <exception cref="T:System.InvalidOperationException">The resource specified by the <paramref name="name" /> parameter has already been added.</exception>
		// Token: 0x0600006B RID: 107 RVA: 0x00003B70 File Offset: 0x00001D70
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

		/// <summary>Releases all resources used by the <see cref="T:System.Resources.ResXResourceWriter" />.</summary>
		// Token: 0x0600006C RID: 108 RVA: 0x00003F40 File Offset: 0x00002140
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

		/// <summary>Releases all resources used by the <see cref="T:System.Resources.ResXResourceWriter" />.</summary>
		// Token: 0x0600006D RID: 109 RVA: 0x00003F8C File Offset: 0x0000218C
		public virtual void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Writes all resources added by the <see cref="M:System.Resources.ResXResourceWriter.AddResource(System.String,System.Byte[])" /> method to the output file or stream.</summary>
		/// <exception cref="T:System.InvalidOperationException">The resource has already been saved. </exception>
		// Token: 0x0600006E RID: 110 RVA: 0x00003F9C File Offset: 0x0000219C
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

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Resources.ResXResourceWriter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600006F RID: 111 RVA: 0x00003FD4 File Offset: 0x000021D4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		/// <summary>Gets or sets the base path for the relative file path specified in a <see cref="T:System.Resources.ResXFileRef" /> object.</summary>
		/// <returns>A path that, if prepended to the relative file path specified in a <see cref="T:System.Resources.ResXFileRef" /> object, yields an absolute path to an XML resource file.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003FE4 File Offset: 0x000021E4
		// (set) Token: 0x06000071 RID: 113 RVA: 0x00003FEC File Offset: 0x000021EC
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

		// Token: 0x04000037 RID: 55
		private string filename;

		// Token: 0x04000038 RID: 56
		private Stream stream;

		// Token: 0x04000039 RID: 57
		private TextWriter textwriter;

		// Token: 0x0400003A RID: 58
		private XmlTextWriter writer;

		// Token: 0x0400003B RID: 59
		private bool written;

		// Token: 0x0400003C RID: 60
		private string base_path;

		/// <summary>Specifies the default content type for a binary object. This field is read-only.</summary>
		// Token: 0x0400003D RID: 61
		public static readonly string BinSerializedObjectMimeType = "application/x-microsoft.net.object.binary.base64";

		/// <summary>Specifies the default content type for a byte array object. This field is read-only.</summary>
		// Token: 0x0400003E RID: 62
		public static readonly string ByteArraySerializedObjectMimeType = "application/x-microsoft.net.object.bytearray.base64";

		/// <summary>Specifies the default content type for an object. This field is read-only.</summary>
		// Token: 0x0400003F RID: 63
		public static readonly string DefaultSerializedObjectMimeType = ResXResourceWriter.BinSerializedObjectMimeType;

		/// <summary>Specifies the content type of an XML resource. This field is read-only.</summary>
		// Token: 0x04000040 RID: 64
		public static readonly string ResMimeType = "text/microsoft-resx";

		/// <summary>Specifies the schema to use in writing the XML file. This field is read-only.</summary>
		// Token: 0x04000041 RID: 65
		public static readonly string ResourceSchema = ResXResourceWriter.schema;

		/// <summary>Specifies the content type for a SOAP object. This field is read-only.</summary>
		// Token: 0x04000042 RID: 66
		public static readonly string SoapSerializedObjectMimeType = "application/x-microsoft.net.object.soap.base64";

		/// <summary>Specifies the version of the schema that the XML output conforms to. This field is read-only.</summary>
		// Token: 0x04000043 RID: 67
		public static readonly string Version = "2.0";

		// Token: 0x04000044 RID: 68
		private static string schema = "\n  <xsd:schema id='root' xmlns='' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:msdata='urn:schemas-microsoft-com:xml-msdata'>\n    <xsd:element name='root' msdata:IsDataSet='true'>\n      <xsd:complexType>\n        <xsd:choice maxOccurs='unbounded'>\n          <xsd:element name='data'>\n            <xsd:complexType>\n              <xsd:sequence>\n                <xsd:element name='value' type='xsd:string' minOccurs='0' msdata:Ordinal='1' />\n                <xsd:element name='comment' type='xsd:string' minOccurs='0' msdata:Ordinal='2' />\n              </xsd:sequence>\n              <xsd:attribute name='name' type='xsd:string' msdata:Ordinal='1' />\n              <xsd:attribute name='type' type='xsd:string' msdata:Ordinal='3' />\n              <xsd:attribute name='mimetype' type='xsd:string' msdata:Ordinal='4' />\n            </xsd:complexType>\n          </xsd:element>\n          <xsd:element name='resheader'>\n            <xsd:complexType>\n              <xsd:sequence>\n                <xsd:element name='value' type='xsd:string' minOccurs='0' msdata:Ordinal='1' />\n              </xsd:sequence>\n              <xsd:attribute name='name' type='xsd:string' use='required' />\n            </xsd:complexType>\n          </xsd:element>\n        </xsd:choice>\n      </xsd:complexType>\n    </xsd:element>\n  </xsd:schema>\n".Replace("'", "\"");
	}
}
