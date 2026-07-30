using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;

namespace System.Resources
{
	/// <summary>Enumerates XML resource (.resx) files and streams, and reads the sequential resource name and value pairs.</summary>
	// Token: 0x0200000E RID: 14
	public class ResXResourceReader : IDisposable, IResourceReader, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class for the specified stream.</summary>
		/// <param name="stream">An input stream that contains resources. </param>
		// Token: 0x06000028 RID: 40 RVA: 0x00002628 File Offset: 0x00000828
		public ResXResourceReader(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			if (!stream.CanRead)
			{
				throw new ArgumentException("Stream was not readable.");
			}
			this.stream = stream;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using an input stream and a type resolution service.  </summary>
		/// <param name="stream">An input stream that contains resources. </param>
		/// <param name="typeResolver">An object that resolves type names specified in a resource.</param>
		// Token: 0x06000029 RID: 41 RVA: 0x0000266C File Offset: 0x0000086C
		public ResXResourceReader(Stream stream, ITypeResolutionService typeResolver)
			: this(stream)
		{
			this.typeresolver = typeResolver;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class for the specified resource file.</summary>
		/// <param name="fileName">The path of the resource file to read. </param>
		// Token: 0x0600002A RID: 42 RVA: 0x0000267C File Offset: 0x0000087C
		public ResXResourceReader(string fileName)
		{
			this.fileName = fileName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using a file name and a type resolution service. </summary>
		/// <param name="fileName">The name of an XML resource file that contains resources. </param>
		/// <param name="typeResolver">An object that resolves type names specified in a resource.</param>
		// Token: 0x0600002B RID: 43 RVA: 0x0000268C File Offset: 0x0000088C
		public ResXResourceReader(string fileName, ITypeResolutionService typeResolver)
			: this(fileName)
		{
			this.typeresolver = typeResolver;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class for the specified <see cref="T:System.IO.TextReader" />.</summary>
		/// <param name="reader">A text input stream that contains resources. </param>
		// Token: 0x0600002C RID: 44 RVA: 0x0000269C File Offset: 0x0000089C
		public ResXResourceReader(TextReader reader)
		{
			this.reader = reader;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using a text stream reader and a type resolution service.  </summary>
		/// <param name="reader">A text stream reader that contains resources. </param>
		/// <param name="typeResolver">An object that resolves type names specified in a resource.</param>
		// Token: 0x0600002D RID: 45 RVA: 0x000026AC File Offset: 0x000008AC
		public ResXResourceReader(TextReader reader, ITypeResolutionService typeResolver)
			: this(reader)
		{
			this.typeresolver = typeResolver;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using a stream and an array of assembly names. </summary>
		/// <param name="stream">An input stream that contains resources. </param>
		/// <param name="assemblyNames">An array of <see cref="T:System.Reflection.AssemblyName" /> objects that specifies one or more assemblies. The assemblies are used to resolve a type name in the resource to an actual type. </param>
		// Token: 0x0600002E RID: 46 RVA: 0x000026BC File Offset: 0x000008BC
		public ResXResourceReader(Stream stream, AssemblyName[] assemblyNames)
			: this(stream)
		{
			this.assemblyNames = assemblyNames;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using an XML resource file name and an array of assembly names. </summary>
		/// <param name="fileName">The name of an XML resource file that contains resources. </param>
		/// <param name="assemblyNames">An array of <see cref="T:System.Reflection.AssemblyName" /> objects that specifies one or more assemblies. The assemblies are used to resolve a type name in the resource to an actual type. </param>
		// Token: 0x0600002F RID: 47 RVA: 0x000026CC File Offset: 0x000008CC
		public ResXResourceReader(string fileName, AssemblyName[] assemblyNames)
			: this(fileName)
		{
			this.assemblyNames = assemblyNames;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Resources.ResXResourceReader" /> class using a <see cref="T:System.IO.TextReader" /> object and an array of assembly names.</summary>
		/// <param name="reader">An object used to read resources from a stream of text. </param>
		/// <param name="assemblyNames">An array of <see cref="T:System.Reflection.AssemblyName" /> objects that specifies one or more assemblies. The assemblies are used to resolve a type name in the resource to an actual type. </param>
		// Token: 0x06000030 RID: 48 RVA: 0x000026DC File Offset: 0x000008DC
		public ResXResourceReader(TextReader reader, AssemblyName[] assemblyNames)
			: this(reader)
		{
			this.assemblyNames = assemblyNames;
		}

		/// <summary>Returns an enumerator for the current <see cref="T:System.Resources.ResXResourceReader" /> object. For a description of this member, see <see cref="M:System.Collections.IEnumerable.GetEnumerator" />. </summary>
		/// <returns>An enumerator that can iterate through the name/value pairs in the XML resource (.resx) stream or string associated with the current <see cref="T:System.Resources.ResXResourceReader" /> object.</returns>
		// Token: 0x06000031 RID: 49 RVA: 0x000026EC File Offset: 0x000008EC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Resources.ResXResourceReader" /> and optionally releases the managed resources. For a description of this member, see <see cref="M:System.IDisposable.Dispose" />. </summary>
		// Token: 0x06000032 RID: 50 RVA: 0x000026F4 File Offset: 0x000008F4
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>This member overrides the <see cref="M:System.Object.Finalize" /> method. </summary>
		// Token: 0x06000033 RID: 51 RVA: 0x00002704 File Offset: 0x00000904
		~ResXResourceReader()
		{
			this.Dispose(false);
		}

		/// <summary>Gets or sets the base path for the relative file path specified in a <see cref="T:System.Resources.ResXFileRef" /> object.</summary>
		/// <returns>A path that, if prepended to the relative file path specified in a <see cref="T:System.Resources.ResXFileRef" /> object, yields an absolute path to a resource file.</returns>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, a value cannot be specified because the XML resource file has already been accessed and is in use.</exception>
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002740 File Offset: 0x00000940
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002748 File Offset: 0x00000948
		public string BasePath
		{
			get
			{
				return this.basepath;
			}
			set
			{
				this.basepath = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether <see cref="T:System.Resources.ResXDataNode" /> objects are returned when reading the current XML resource file or stream.</summary>
		/// <returns>true if resource data nodes are retrieved; false if resource data nodes are ignored.</returns>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the enumerator for the resource file or stream is already open.</exception>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002754 File Offset: 0x00000954
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000275C File Offset: 0x0000095C
		public bool UseResXDataNodes
		{
			get
			{
				return this.useResXDataNodes;
			}
			set
			{
				if (this.xmlReader != null)
				{
					throw new InvalidOperationException();
				}
				this.useResXDataNodes = value;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002778 File Offset: 0x00000978
		private void LoadData()
		{
			this.hasht = new Hashtable();
			this.hashtm = new Hashtable();
			if (this.fileName != null)
			{
				this.stream = File.OpenRead(this.fileName);
			}
			try
			{
				this.xmlReader = null;
				if (this.stream != null)
				{
					this.xmlReader = new XmlTextReader(this.stream);
				}
				else if (this.reader != null)
				{
					this.xmlReader = new XmlTextReader(this.reader);
				}
				if (this.xmlReader == null)
				{
					throw new InvalidOperationException("ResourceReader is closed.");
				}
				this.xmlReader.WhitespaceHandling = 2;
				ResXResourceReader.ResXHeader resXHeader = new ResXResourceReader.ResXHeader();
				try
				{
					while (this.xmlReader.Read())
					{
						if (this.xmlReader.NodeType == 1)
						{
							string localName = this.xmlReader.LocalName;
							if (localName != null)
							{
								if (ResXResourceReader.<>f__switch$map0 == null)
								{
									Dictionary<string, int> dictionary = new Dictionary<string, int>(3);
									dictionary.Add("resheader", 0);
									dictionary.Add("data", 1);
									dictionary.Add("metadata", 2);
									ResXResourceReader.<>f__switch$map0 = dictionary;
								}
								int num;
								if (ResXResourceReader.<>f__switch$map0.TryGetValue(localName, ref num))
								{
									switch (num)
									{
									case 0:
										this.ParseHeaderNode(resXHeader);
										break;
									case 1:
										this.ParseDataNode(false);
										break;
									case 2:
										this.ParseDataNode(true);
										break;
									}
								}
							}
						}
					}
				}
				catch (XmlException ex)
				{
					throw new ArgumentException("Invalid ResX input.", ex);
				}
				catch (Exception ex2)
				{
					XmlException ex3 = new XmlException(ex2.Message, ex2, this.xmlReader.LineNumber, this.xmlReader.LinePosition);
					throw new ArgumentException("Invalid ResX input.", ex3);
				}
				resXHeader.Verify();
			}
			finally
			{
				if (this.fileName != null)
				{
					this.stream.Close();
					this.stream = null;
				}
				this.xmlReader = null;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000029BC File Offset: 0x00000BBC
		private void ParseHeaderNode(ResXResourceReader.ResXHeader header)
		{
			string attribute = this.GetAttribute("name");
			if (attribute == null)
			{
				return;
			}
			if (string.Compare(attribute, "resmimetype", true) == 0)
			{
				header.ResMimeType = this.GetHeaderValue();
			}
			else if (string.Compare(attribute, "reader", true) == 0)
			{
				header.Reader = this.GetHeaderValue();
			}
			else if (string.Compare(attribute, "version", true) == 0)
			{
				header.Version = this.GetHeaderValue();
			}
			else if (string.Compare(attribute, "writer", true) == 0)
			{
				header.Writer = this.GetHeaderValue();
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002A60 File Offset: 0x00000C60
		private string GetHeaderValue()
		{
			this.xmlReader.ReadStartElement();
			string text;
			if (this.xmlReader.NodeType == 1)
			{
				text = this.xmlReader.ReadElementString();
			}
			else
			{
				text = this.xmlReader.Value.Trim();
			}
			return text;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002AB0 File Offset: 0x00000CB0
		private string GetAttribute(string name)
		{
			if (!this.xmlReader.HasAttributes)
			{
				return null;
			}
			for (int i = 0; i < this.xmlReader.AttributeCount; i++)
			{
				this.xmlReader.MoveToAttribute(i);
				if (string.Compare(this.xmlReader.Name, name, true) == 0)
				{
					string value = this.xmlReader.Value;
					this.xmlReader.MoveToElement();
					return value;
				}
			}
			this.xmlReader.MoveToElement();
			return null;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002B38 File Offset: 0x00000D38
		private string GetDataValue(bool meta, out string comment)
		{
			string text = null;
			comment = null;
			while (this.xmlReader.Read())
			{
				if (this.xmlReader.NodeType == 15 && this.xmlReader.LocalName == ((!meta) ? "data" : "metadata"))
				{
					break;
				}
				if (this.xmlReader.NodeType == 1)
				{
					if (this.xmlReader.Name.Equals("value"))
					{
						this.xmlReader.WhitespaceHandling = 1;
						text = this.xmlReader.ReadString();
						this.xmlReader.WhitespaceHandling = 2;
					}
					else if (this.xmlReader.Name.Equals("comment"))
					{
						this.xmlReader.WhitespaceHandling = 1;
						comment = this.xmlReader.ReadString();
						this.xmlReader.WhitespaceHandling = 2;
						if (this.xmlReader.NodeType == 15 && this.xmlReader.LocalName == ((!meta) ? "data" : "metadata"))
						{
							break;
						}
					}
				}
				else
				{
					text = this.xmlReader.Value.Trim();
				}
			}
			return text;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C8C File Offset: 0x00000E8C
		private void ParseDataNode(bool meta)
		{
			Hashtable hashtable = ((!meta || this.useResXDataNodes) ? this.hasht : this.hashtm);
			Point point;
			point..ctor(this.xmlReader.LineNumber, this.xmlReader.LinePosition);
			string attribute = this.GetAttribute("name");
			string attribute2 = this.GetAttribute("type");
			string attribute3 = this.GetAttribute("mimetype");
			Type type = ((attribute2 != null) ? this.ResolveType(attribute2) : null);
			if (attribute2 != null && type == null)
			{
				throw new ArgumentException(string.Format("The type '{0}' of the element '{1}' could not be resolved.", attribute2, attribute));
			}
			if (type == typeof(ResXNullRef))
			{
				if (this.useResXDataNodes)
				{
					hashtable[attribute] = new ResXDataNode(attribute, null, point);
				}
				else
				{
					hashtable[attribute] = null;
				}
				return;
			}
			string text = null;
			string dataValue = this.GetDataValue(meta, out text);
			object obj = null;
			if (attribute3 != null && attribute3.Length > 0)
			{
				if (attribute3 == ResXResourceWriter.BinSerializedObjectMimeType)
				{
					byte[] array = Convert.FromBase64String(dataValue);
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					using (MemoryStream memoryStream = new MemoryStream(array))
					{
						obj = binaryFormatter.Deserialize(memoryStream);
					}
				}
				else if (attribute3 == ResXResourceWriter.ByteArraySerializedObjectMimeType && type != null)
				{
					TypeConverter converter = TypeDescriptor.GetConverter(type);
					if (converter.CanConvertFrom(typeof(byte[])))
					{
						obj = converter.ConvertFrom(Convert.FromBase64String(dataValue));
					}
				}
			}
			else if (type != null)
			{
				if (type == typeof(byte[]))
				{
					obj = Convert.FromBase64String(dataValue);
				}
				else
				{
					TypeConverter converter2 = TypeDescriptor.GetConverter(type);
					if (converter2.CanConvertFrom(typeof(string)))
					{
						if (this.BasePath != null && type == typeof(ResXFileRef))
						{
							string[] array2 = ResXFileRef.Parse(dataValue);
							array2[0] = Path.Combine(this.BasePath, array2[0]);
							obj = converter2.ConvertFromInvariantString(string.Join(";", array2));
						}
						else
						{
							obj = converter2.ConvertFromInvariantString(dataValue);
						}
					}
				}
			}
			else
			{
				obj = dataValue;
			}
			if (attribute == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Could not find a name for a resource. The resource value was '{0}'.", new object[] { obj }));
			}
			if (this.useResXDataNodes)
			{
				hashtable[attribute] = new ResXDataNode(attribute, obj, point)
				{
					Comment = text
				};
			}
			else
			{
				hashtable[attribute] = obj;
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002F50 File Offset: 0x00001150
		private Type ResolveType(string type)
		{
			if (this.typeresolver != null)
			{
				return this.typeresolver.GetType(type);
			}
			if (this.assemblyNames != null)
			{
				foreach (AssemblyName assemblyName in this.assemblyNames)
				{
					Assembly assembly = Assembly.Load(assemblyName);
					Type type2 = assembly.GetType(type, false);
					if (type2 != null)
					{
						return type2;
					}
				}
			}
			return Type.GetType(type);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Resources.ResXResourceReader" />.</summary>
		// Token: 0x0600003F RID: 63 RVA: 0x00002FC0 File Offset: 0x000011C0
		public void Close()
		{
			if (this.reader != null)
			{
				this.reader.Close();
				this.reader = null;
			}
		}

		/// <summary>Returns an enumerator for the current <see cref="T:System.Resources.ResXResourceReader" /> object.</summary>
		/// <returns>An enumerator for the current <see cref="T:System.Resources.ResourceReader" /> object.</returns>
		// Token: 0x06000040 RID: 64 RVA: 0x00002FE0 File Offset: 0x000011E0
		public IDictionaryEnumerator GetEnumerator()
		{
			if (this.hasht == null)
			{
				this.LoadData();
			}
			return this.hasht.GetEnumerator();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Resources.ResXResourceReader" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000041 RID: 65 RVA: 0x00003000 File Offset: 0x00001200
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		/// <summary>Creates a new <see cref="T:System.Resources.ResXResourceReader" /> object and initializes it to read a string whose contents are in the form of an XML resource file.</summary>
		/// <returns>A <see cref="T:System.Resources.ResXResourceReader" /> object that reads resources from the <paramref name="fileContents" /> string.</returns>
		/// <param name="fileContents">A string containing XML resource-formatted information. </param>
		// Token: 0x06000042 RID: 66 RVA: 0x00003010 File Offset: 0x00001210
		public static ResXResourceReader FromFileContents(string fileContents)
		{
			return new ResXResourceReader(new StringReader(fileContents));
		}

		/// <summary>Creates a new <see cref="T:System.Resources.ResXResourceReader" /> object and initializes it to read a string whose contents are in the form of an XML resource file, and to use an <see cref="T:System.ComponentModel.Design.ITypeResolutionService" /> object to resolve type names specified in a resource.</summary>
		/// <returns>An object that reads resources from the <paramref name="fileContents" /> string.</returns>
		/// <param name="fileContents">A string containing XML resource-formatted information. </param>
		/// <param name="typeResolver">An object that resolves type names specified in a resource.</param>
		// Token: 0x06000043 RID: 67 RVA: 0x00003020 File Offset: 0x00001220
		public static ResXResourceReader FromFileContents(string fileContents, ITypeResolutionService typeResolver)
		{
			return new ResXResourceReader(new StringReader(fileContents), typeResolver);
		}

		/// <summary>Creates a new <see cref="T:System.Resources.ResXResourceReader" /> object and initializes it to read a string whose contents are in the form of an XML resource file, and to use an array of <see cref="T:System.Reflection.AssemblyName" /> objects to resolve type names specified in a resource. </summary>
		/// <returns>An object that reads resources from the <paramref name="fileContents" /> string.</returns>
		/// <param name="fileContents">A string whose contents are in the form of an XML resource file. </param>
		/// <param name="assemblyNames">An array of <see cref="T:System.Reflection.AssemblyName" /> objects that specifies one or more assemblies. The assemblies are used to resolve a type name in the resource to an actual type. </param>
		// Token: 0x06000044 RID: 68 RVA: 0x00003030 File Offset: 0x00001230
		public static ResXResourceReader FromFileContents(string fileContents, AssemblyName[] assemblyNames)
		{
			return new ResXResourceReader(new StringReader(fileContents), assemblyNames);
		}

		/// <summary>Provides a dictionary enumerator that can retrieve the design-time properties from the current XML resource file or stream.</summary>
		/// <returns>An enumerator for the metadata in a resource.</returns>
		// Token: 0x06000045 RID: 69 RVA: 0x00003040 File Offset: 0x00001240
		public IDictionaryEnumerator GetMetadataEnumerator()
		{
			if (this.hashtm == null)
			{
				this.LoadData();
			}
			return this.hashtm.GetEnumerator();
		}

		// Token: 0x04000028 RID: 40
		private string fileName;

		// Token: 0x04000029 RID: 41
		private Stream stream;

		// Token: 0x0400002A RID: 42
		private TextReader reader;

		// Token: 0x0400002B RID: 43
		private Hashtable hasht;

		// Token: 0x0400002C RID: 44
		private ITypeResolutionService typeresolver;

		// Token: 0x0400002D RID: 45
		private XmlTextReader xmlReader;

		// Token: 0x0400002E RID: 46
		private string basepath;

		// Token: 0x0400002F RID: 47
		private bool useResXDataNodes;

		// Token: 0x04000030 RID: 48
		private AssemblyName[] assemblyNames;

		// Token: 0x04000031 RID: 49
		private Hashtable hashtm;

		// Token: 0x0200000F RID: 15
		private class ResXHeader
		{
			// Token: 0x1700000B RID: 11
			// (get) Token: 0x06000047 RID: 71 RVA: 0x00003068 File Offset: 0x00001268
			// (set) Token: 0x06000048 RID: 72 RVA: 0x00003070 File Offset: 0x00001270
			public string ResMimeType
			{
				get
				{
					return this.resMimeType;
				}
				set
				{
					this.resMimeType = value;
				}
			}

			// Token: 0x1700000C RID: 12
			// (get) Token: 0x06000049 RID: 73 RVA: 0x0000307C File Offset: 0x0000127C
			// (set) Token: 0x0600004A RID: 74 RVA: 0x00003084 File Offset: 0x00001284
			public string Reader
			{
				get
				{
					return this.reader;
				}
				set
				{
					this.reader = value;
				}
			}

			// Token: 0x1700000D RID: 13
			// (get) Token: 0x0600004B RID: 75 RVA: 0x00003090 File Offset: 0x00001290
			// (set) Token: 0x0600004C RID: 76 RVA: 0x00003098 File Offset: 0x00001298
			public string Version
			{
				get
				{
					return this.version;
				}
				set
				{
					this.version = value;
				}
			}

			// Token: 0x1700000E RID: 14
			// (get) Token: 0x0600004D RID: 77 RVA: 0x000030A4 File Offset: 0x000012A4
			// (set) Token: 0x0600004E RID: 78 RVA: 0x000030AC File Offset: 0x000012AC
			public string Writer
			{
				get
				{
					return this.writer;
				}
				set
				{
					this.writer = value;
				}
			}

			// Token: 0x0600004F RID: 79 RVA: 0x000030B8 File Offset: 0x000012B8
			public void Verify()
			{
				if (!this.IsValid)
				{
					throw new ArgumentException("Invalid ResX input.  Could not find valid \"resheader\" tags for the ResX reader & writer type names.");
				}
			}

			// Token: 0x1700000F RID: 15
			// (get) Token: 0x06000050 RID: 80 RVA: 0x000030D0 File Offset: 0x000012D0
			public bool IsValid
			{
				get
				{
					if (string.Compare(this.ResMimeType, ResXResourceWriter.ResMimeType) != 0)
					{
						return false;
					}
					if (this.Reader == null || this.Writer == null)
					{
						return false;
					}
					string text = this.Reader.Split(new char[] { ',' })[0].Trim();
					if (text != typeof(ResXResourceReader).FullName)
					{
						return false;
					}
					string text2 = this.Writer.Split(new char[] { ',' })[0].Trim();
					return !(text2 != typeof(ResXResourceWriter).FullName);
				}
			}

			// Token: 0x04000033 RID: 51
			private string resMimeType;

			// Token: 0x04000034 RID: 52
			private string reader;

			// Token: 0x04000035 RID: 53
			private string version;

			// Token: 0x04000036 RID: 54
			private string writer;
		}
	}
}
