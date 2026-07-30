using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace System.Resources
{
	// Token: 0x02000027 RID: 39
	internal class ResXResourceReader : IResourceReader, IEnumerable, IDisposable
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x000042CC File Offset: 0x000024CC
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

		// Token: 0x060000B7 RID: 183 RVA: 0x000042FC File Offset: 0x000024FC
		public ResXResourceReader(Stream stream, ITypeResolutionService typeResolver)
			: this(stream)
		{
			this.typeresolver = typeResolver;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000430C File Offset: 0x0000250C
		public ResXResourceReader(string fileName)
		{
			this.fileName = fileName;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000431B File Offset: 0x0000251B
		public ResXResourceReader(string fileName, ITypeResolutionService typeResolver)
			: this(fileName)
		{
			this.typeresolver = typeResolver;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000432B File Offset: 0x0000252B
		public ResXResourceReader(TextReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000433A File Offset: 0x0000253A
		public ResXResourceReader(TextReader reader, ITypeResolutionService typeResolver)
			: this(reader)
		{
			this.typeresolver = typeResolver;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000434A File Offset: 0x0000254A
		public ResXResourceReader(Stream stream, AssemblyName[] assemblyNames)
			: this(stream)
		{
			this.assemblyNames = assemblyNames;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000435A File Offset: 0x0000255A
		public ResXResourceReader(string fileName, AssemblyName[] assemblyNames)
			: this(fileName)
		{
			this.assemblyNames = assemblyNames;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000436A File Offset: 0x0000256A
		public ResXResourceReader(TextReader reader, AssemblyName[] assemblyNames)
			: this(reader)
		{
			this.assemblyNames = assemblyNames;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000437C File Offset: 0x0000257C
		~ResXResourceReader()
		{
			this.Dispose(false);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000043AC File Offset: 0x000025AC
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000043B4 File Offset: 0x000025B4
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

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000043BD File Offset: 0x000025BD
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000043C5 File Offset: 0x000025C5
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

		// Token: 0x060000C4 RID: 196 RVA: 0x000043DC File Offset: 0x000025DC
		private void LoadData()
		{
			this.hasht = new OrderedDictionary();
			this.hashtm = new OrderedDictionary();
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
				this.xmlReader.WhitespaceHandling = WhitespaceHandling.None;
				ResXResourceReader.ResXHeader resXHeader = new ResXResourceReader.ResXHeader();
				try
				{
					while (this.xmlReader.Read())
					{
						if (this.xmlReader.NodeType == XmlNodeType.Element)
						{
							string localName = this.xmlReader.LocalName;
							if (!(localName == "resheader"))
							{
								if (!(localName == "data"))
								{
									if (localName == "metadata")
									{
										this.ParseDataNode(true);
									}
								}
								else
								{
									this.ParseDataNode(false);
								}
							}
							else
							{
								this.ParseHeaderNode(resXHeader);
							}
						}
					}
				}
				catch (XmlException ex)
				{
					throw new ArgumentException("Invalid ResX input.", ex);
				}
				catch (SerializationException ex2)
				{
					throw ex2;
				}
				catch (TargetInvocationException ex3)
				{
					throw ex3;
				}
				catch (Exception ex4)
				{
					XmlException ex5 = new XmlException(ex4.Message, ex4, this.xmlReader.LineNumber, this.xmlReader.LinePosition);
					throw new ArgumentException("Invalid ResX input.", ex5);
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

		// Token: 0x060000C5 RID: 197 RVA: 0x000045D0 File Offset: 0x000027D0
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
				return;
			}
			if (string.Compare(attribute, "reader", true) == 0)
			{
				header.Reader = this.GetHeaderValue();
				return;
			}
			if (string.Compare(attribute, "version", true) == 0)
			{
				header.Version = this.GetHeaderValue();
				return;
			}
			if (string.Compare(attribute, "writer", true) == 0)
			{
				header.Writer = this.GetHeaderValue();
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004658 File Offset: 0x00002858
		private string GetHeaderValue()
		{
			this.xmlReader.ReadStartElement();
			string text;
			if (this.xmlReader.NodeType == XmlNodeType.Element)
			{
				text = this.xmlReader.ReadElementString();
			}
			else
			{
				text = this.xmlReader.Value.Trim();
			}
			return text;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000046A0 File Offset: 0x000028A0
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

		// Token: 0x060000C8 RID: 200 RVA: 0x00004718 File Offset: 0x00002918
		private string GetDataValue(bool meta, out string comment)
		{
			string text = null;
			comment = null;
			while (this.xmlReader.Read() && (this.xmlReader.NodeType != XmlNodeType.EndElement || !(this.xmlReader.LocalName == (meta ? "metadata" : "data"))))
			{
				if (this.xmlReader.NodeType == XmlNodeType.Element)
				{
					if (this.xmlReader.Name.Equals("value"))
					{
						this.xmlReader.WhitespaceHandling = WhitespaceHandling.Significant;
						text = this.xmlReader.ReadString();
						this.xmlReader.WhitespaceHandling = WhitespaceHandling.None;
					}
					else if (this.xmlReader.Name.Equals("comment"))
					{
						this.xmlReader.WhitespaceHandling = WhitespaceHandling.Significant;
						comment = this.xmlReader.ReadString();
						this.xmlReader.WhitespaceHandling = WhitespaceHandling.None;
						if (this.xmlReader.NodeType == XmlNodeType.EndElement && this.xmlReader.LocalName == (meta ? "metadata" : "data"))
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

		// Token: 0x060000C9 RID: 201 RVA: 0x00004840 File Offset: 0x00002A40
		private void ParseDataNode(bool meta)
		{
			OrderedDictionary orderedDictionary = ((meta && !this.useResXDataNodes) ? this.hashtm : this.hasht);
			Point point = new Point(this.xmlReader.LineNumber, this.xmlReader.LinePosition);
			string attribute = this.GetAttribute("name");
			string attribute2 = this.GetAttribute("type");
			string attribute3 = this.GetAttribute("mimetype");
			string text = null;
			string dataValue = this.GetDataValue(meta, out text);
			ResXDataNode resXDataNode = new ResXDataNode(attribute, attribute3, attribute2, dataValue, text, point, this.BasePath);
			if (this.useResXDataNodes)
			{
				orderedDictionary[attribute] = resXDataNode;
				return;
			}
			if (attribute == null)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "Could not find a name for a resource. The resource value was '{0}'.", resXDataNode.GetValue(null).ToString()));
			}
			if (this.assemblyNames != null)
			{
				try
				{
					orderedDictionary[attribute] = resXDataNode.GetValue(this.assemblyNames);
					return;
				}
				catch (TypeLoadException ex)
				{
					if (resXDataNode.handler is TypeConverterFromResXHandler)
					{
						orderedDictionary[attribute] = null;
						return;
					}
					throw ex;
				}
			}
			try
			{
				orderedDictionary[attribute] = resXDataNode.GetValue(this.typeresolver);
			}
			catch (TypeLoadException ex2)
			{
				if (!(resXDataNode.handler is TypeConverterFromResXHandler))
				{
					throw ex2;
				}
				orderedDictionary[attribute] = null;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004994 File Offset: 0x00002B94
		public void Close()
		{
			if (this.reader != null)
			{
				this.reader.Close();
				this.reader = null;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000049B0 File Offset: 0x00002BB0
		public IDictionaryEnumerator GetEnumerator()
		{
			if (this.hasht == null)
			{
				this.LoadData();
			}
			return this.hasht.GetEnumerator();
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000049CB File Offset: 0x00002BCB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IResourceReader)this).GetEnumerator();
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000049D3 File Offset: 0x00002BD3
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000049E2 File Offset: 0x00002BE2
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000049ED File Offset: 0x00002BED
		public static ResXResourceReader FromFileContents(string fileContents)
		{
			return new ResXResourceReader(new StringReader(fileContents));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x000049FA File Offset: 0x00002BFA
		public static ResXResourceReader FromFileContents(string fileContents, ITypeResolutionService typeResolver)
		{
			return new ResXResourceReader(new StringReader(fileContents), typeResolver);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00004A08 File Offset: 0x00002C08
		public static ResXResourceReader FromFileContents(string fileContents, AssemblyName[] assemblyNames)
		{
			return new ResXResourceReader(new StringReader(fileContents), assemblyNames);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00004A16 File Offset: 0x00002C16
		public IDictionaryEnumerator GetMetadataEnumerator()
		{
			if (this.hashtm == null)
			{
				this.LoadData();
			}
			return this.hashtm.GetEnumerator();
		}

		// Token: 0x04000D73 RID: 3443
		private string fileName;

		// Token: 0x04000D74 RID: 3444
		private Stream stream;

		// Token: 0x04000D75 RID: 3445
		private TextReader reader;

		// Token: 0x04000D76 RID: 3446
		private OrderedDictionary hasht;

		// Token: 0x04000D77 RID: 3447
		private ITypeResolutionService typeresolver;

		// Token: 0x04000D78 RID: 3448
		private XmlTextReader xmlReader;

		// Token: 0x04000D79 RID: 3449
		private string basepath;

		// Token: 0x04000D7A RID: 3450
		private bool useResXDataNodes;

		// Token: 0x04000D7B RID: 3451
		private AssemblyName[] assemblyNames;

		// Token: 0x04000D7C RID: 3452
		private OrderedDictionary hashtm;

		// Token: 0x02000028 RID: 40
		private class ResXHeader
		{
			// Token: 0x1700001E RID: 30
			// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004A31 File Offset: 0x00002C31
			// (set) Token: 0x060000D4 RID: 212 RVA: 0x00004A39 File Offset: 0x00002C39
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

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x060000D5 RID: 213 RVA: 0x00004A42 File Offset: 0x00002C42
			// (set) Token: 0x060000D6 RID: 214 RVA: 0x00004A4A File Offset: 0x00002C4A
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

			// Token: 0x17000020 RID: 32
			// (get) Token: 0x060000D7 RID: 215 RVA: 0x00004A53 File Offset: 0x00002C53
			// (set) Token: 0x060000D8 RID: 216 RVA: 0x00004A5B File Offset: 0x00002C5B
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

			// Token: 0x17000021 RID: 33
			// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004A64 File Offset: 0x00002C64
			// (set) Token: 0x060000DA RID: 218 RVA: 0x00004A6C File Offset: 0x00002C6C
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

			// Token: 0x060000DB RID: 219 RVA: 0x00004A75 File Offset: 0x00002C75
			public void Verify()
			{
				if (!this.IsValid)
				{
					throw new ArgumentException("Invalid ResX input.  Could not find valid \"resheader\" tags for the ResX reader & writer type names.");
				}
			}

			// Token: 0x17000022 RID: 34
			// (get) Token: 0x060000DC RID: 220 RVA: 0x00004A8C File Offset: 0x00002C8C
			public bool IsValid
			{
				get
				{
					return string.Compare(this.ResMimeType, ResXResourceWriter.ResMimeType) == 0 && this.Reader != null && this.Writer != null && !(this.Reader.Split(new char[] { ',' })[0].Trim() != typeof(ResXResourceReader).FullName) && !(this.Writer.Split(new char[] { ',' })[0].Trim() != typeof(ResXResourceWriter).FullName);
				}
			}

			// Token: 0x04000D7D RID: 3453
			private string resMimeType;

			// Token: 0x04000D7E RID: 3454
			private string reader;

			// Token: 0x04000D7F RID: 3455
			private string version;

			// Token: 0x04000D80 RID: 3456
			private string writer;
		}
	}
}
