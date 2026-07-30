using System;
using System.Text;

namespace System.Xml
{
	/// <summary>Represents the XML declaration node &lt;?xml version='1.0'...?&gt;.</summary>
	// Token: 0x0200021C RID: 540
	public class XmlDeclaration : XmlLinkedNode
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlDeclaration" /> class.</summary>
		/// <param name="version">The XML version; see the <see cref="P:System.Xml.XmlDeclaration.Version" /> property.</param>
		/// <param name="encoding">The encoding scheme; see the <see cref="P:System.Xml.XmlDeclaration.Encoding" /> property.</param>
		/// <param name="standalone">Indicates whether the XML document depends on an external DTD; see the <see cref="P:System.Xml.XmlDeclaration.Standalone" /> property.</param>
		/// <param name="doc">The parent XML document.</param>
		// Token: 0x060013AC RID: 5036 RVA: 0x00072E64 File Offset: 0x00071064
		protected internal XmlDeclaration(string version, string encoding, string standalone, XmlDocument doc)
			: base(doc)
		{
			if (!this.IsValidXmlVersion(version))
			{
				throw new ArgumentException(Res.GetString("Wrong XML version information. The XML must match production \"VersionNum ::= '1.' [0-9]+\"."));
			}
			if (standalone != null && standalone.Length > 0 && standalone != "yes" && standalone != "no")
			{
				throw new ArgumentException(Res.GetString("Wrong value for the XML declaration standalone attribute of '{0}'.", new object[] { standalone }));
			}
			this.Encoding = encoding;
			this.Standalone = standalone;
			this.Version = version;
		}

		/// <summary>Gets the XML version of the document.</summary>
		/// <returns>The value is always 1.0.</returns>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00072EE7 File Offset: 0x000710E7
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x00072EEF File Offset: 0x000710EF
		public string Version
		{
			get
			{
				return this.version;
			}
			internal set
			{
				this.version = value;
			}
		}

		/// <summary>Gets or sets the encoding level of the XML document.</summary>
		/// <returns>The valid character encoding name. The most commonly supported character encoding names for XML are the following: Category Encoding Names Unicode UTF-8, UTF-16 ISO 10646 ISO-10646-UCS-2, ISO-10646-UCS-4 ISO 8859 ISO-8859-n (where "n" is a digit from 1 to 9) JIS X-0208-1997 ISO-2022-JP, Shift_JIS, EUC-JP This value is optional. If a value is not set, this property returns String.Empty.If an encoding attribute is not included, UTF-8 encoding is assumed when the document is written or saved out.</returns>
		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x00072EF8 File Offset: 0x000710F8
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x00072F00 File Offset: 0x00071100
		public string Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = ((value == null) ? string.Empty : value);
			}
		}

		/// <summary>Gets or sets the value of the standalone attribute.</summary>
		/// <returns>Valid values are yes if all entity declarations required by the XML document are contained within the document or no if an external document type definition (DTD) is required. If a standalone attribute is not present in the XML declaration, this property returns String.Empty.</returns>
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x00072F13 File Offset: 0x00071113
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x00072F1C File Offset: 0x0007111C
		public string Standalone
		{
			get
			{
				return this.standalone;
			}
			set
			{
				if (value == null)
				{
					this.standalone = string.Empty;
					return;
				}
				if (value.Length == 0 || value == "yes" || value == "no")
				{
					this.standalone = value;
					return;
				}
				throw new ArgumentException(Res.GetString("Wrong value for the XML declaration standalone attribute of '{0}'.", new object[] { value }));
			}
		}

		/// <summary>Gets or sets the value of the XmlDeclaration.</summary>
		/// <returns>The contents of the XmlDeclaration (that is, everything between &lt;?xml and ?&gt;).</returns>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00071CC5 File Offset: 0x0006FEC5
		// (set) Token: 0x060013B4 RID: 5044 RVA: 0x00071CCD File Offset: 0x0006FECD
		public override string Value
		{
			get
			{
				return this.InnerText;
			}
			set
			{
				this.InnerText = value;
			}
		}

		/// <summary>Gets or sets the concatenated values of the XmlDeclaration.</summary>
		/// <returns>The concatenated values of the XmlDeclaration (that is, everything between &lt;?xml and ?&gt;).</returns>
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x060013B5 RID: 5045 RVA: 0x00072F7C File Offset: 0x0007117C
		// (set) Token: 0x060013B6 RID: 5046 RVA: 0x00073010 File Offset: 0x00071210
		public override string InnerText
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder("version=\"" + this.Version + "\"");
				if (this.Encoding.Length > 0)
				{
					stringBuilder.Append(" encoding=\"");
					stringBuilder.Append(this.Encoding);
					stringBuilder.Append("\"");
				}
				if (this.Standalone.Length > 0)
				{
					stringBuilder.Append(" standalone=\"");
					stringBuilder.Append(this.Standalone);
					stringBuilder.Append("\"");
				}
				return stringBuilder.ToString();
			}
			set
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				string text4 = this.Encoding;
				string text5 = this.Standalone;
				string text6 = this.Version;
				XmlLoader.ParseXmlDeclarationValue(value, out text, out text2, out text3);
				try
				{
					if (text != null && !this.IsValidXmlVersion(text))
					{
						throw new ArgumentException(Res.GetString("Wrong XML version information. The XML must match production \"VersionNum ::= '1.' [0-9]+\"."));
					}
					this.Version = text;
					if (text2 != null)
					{
						this.Encoding = text2;
					}
					if (text3 != null)
					{
						this.Standalone = text3;
					}
				}
				catch
				{
					this.Encoding = text4;
					this.Standalone = text5;
					this.Version = text6;
					throw;
				}
			}
		}

		/// <summary>Gets the qualified name of the node.</summary>
		/// <returns>For XmlDeclaration nodes, the name is xml.</returns>
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x060013B7 RID: 5047 RVA: 0x000730AC File Offset: 0x000712AC
		public override string Name
		{
			get
			{
				return "xml";
			}
		}

		/// <summary>Gets the local name of the node.</summary>
		/// <returns>For XmlDeclaration nodes, the local name is xml.</returns>
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x000730B3 File Offset: 0x000712B3
		public override string LocalName
		{
			get
			{
				return this.Name;
			}
		}

		/// <summary>Gets the type of the current node.</summary>
		/// <returns>For XmlDeclaration nodes, this value is XmlNodeType.XmlDeclaration.</returns>
		// Token: 0x17000385 RID: 901
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x000730BB File Offset: 0x000712BB
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.XmlDeclaration;
			}
		}

		/// <summary>Creates a duplicate of this node.</summary>
		/// <returns>The cloned node.</returns>
		/// <param name="deep">true to recursively clone the subtree under the specified node; false to clone only the node itself. Because XmlDeclaration nodes do not have children, the cloned node always includes the data value, regardless of the parameter setting. </param>
		// Token: 0x060013BA RID: 5050 RVA: 0x000730BF File Offset: 0x000712BF
		public override XmlNode CloneNode(bool deep)
		{
			return this.OwnerDocument.CreateXmlDeclaration(this.Version, this.Encoding, this.Standalone);
		}

		/// <summary>Saves the node to the specified <see cref="T:System.Xml.XmlWriter" />.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060013BB RID: 5051 RVA: 0x000730DE File Offset: 0x000712DE
		public override void WriteTo(XmlWriter w)
		{
			w.WriteProcessingInstruction(this.Name, this.InnerText);
		}

		/// <summary>Saves the children of the node to the specified <see cref="T:System.Xml.XmlWriter" />. Because XmlDeclaration nodes do not have children, this method has no effect.</summary>
		/// <param name="w">The XmlWriter to which you want to save. </param>
		// Token: 0x060013BC RID: 5052 RVA: 0x00002F50 File Offset: 0x00001150
		public override void WriteContentTo(XmlWriter w)
		{
		}

		// Token: 0x060013BD RID: 5053 RVA: 0x000730F2 File Offset: 0x000712F2
		private bool IsValidXmlVersion(string ver)
		{
			return ver.Length >= 3 && ver[0] == '1' && ver[1] == '.' && XmlCharType.IsOnlyDigits(ver, 2, ver.Length - 2);
		}

		// Token: 0x04000D86 RID: 3462
		private const string YES = "yes";

		// Token: 0x04000D87 RID: 3463
		private const string NO = "no";

		// Token: 0x04000D88 RID: 3464
		private string version;

		// Token: 0x04000D89 RID: 3465
		private string encoding;

		// Token: 0x04000D8A RID: 3466
		private string standalone;
	}
}
