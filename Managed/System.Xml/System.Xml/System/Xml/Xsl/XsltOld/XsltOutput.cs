using System;
using System.Collections;
using System.Text;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x02000562 RID: 1378
	internal class XsltOutput : CompiledAction
	{
		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06003724 RID: 14116 RVA: 0x00133E05 File Offset: 0x00132005
		internal XsltOutput.OutputMethod Method
		{
			get
			{
				return this.method;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06003725 RID: 14117 RVA: 0x00133E0D File Offset: 0x0013200D
		internal bool OmitXmlDeclaration
		{
			get
			{
				return this.omitXmlDecl;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06003726 RID: 14118 RVA: 0x00133E15 File Offset: 0x00132015
		internal bool HasStandalone
		{
			get
			{
				return this.standaloneSId != int.MaxValue;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06003727 RID: 14119 RVA: 0x00133E27 File Offset: 0x00132027
		internal bool Standalone
		{
			get
			{
				return this.standalone;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06003728 RID: 14120 RVA: 0x00133E2F File Offset: 0x0013202F
		internal string DoctypePublic
		{
			get
			{
				return this.doctypePublic;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06003729 RID: 14121 RVA: 0x00133E37 File Offset: 0x00132037
		internal string DoctypeSystem
		{
			get
			{
				return this.doctypeSystem;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x00133E3F File Offset: 0x0013203F
		internal Hashtable CDataElements
		{
			get
			{
				return this.cdataElements;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x0600372B RID: 14123 RVA: 0x00133E47 File Offset: 0x00132047
		internal bool Indent
		{
			get
			{
				return this.indent;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x0600372C RID: 14124 RVA: 0x00133E4F File Offset: 0x0013204F
		internal Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x0600372D RID: 14125 RVA: 0x00133E57 File Offset: 0x00132057
		internal string MediaType
		{
			get
			{
				return this.mediaType;
			}
		}

		// Token: 0x0600372E RID: 14126 RVA: 0x00133E60 File Offset: 0x00132060
		internal XsltOutput CreateDerivedOutput(XsltOutput.OutputMethod method)
		{
			XsltOutput xsltOutput = (XsltOutput)base.MemberwiseClone();
			xsltOutput.method = method;
			if (method == XsltOutput.OutputMethod.Html && this.indentSId == 2147483647)
			{
				xsltOutput.indent = true;
			}
			return xsltOutput;
		}

		// Token: 0x0600372F RID: 14127 RVA: 0x00133E99 File Offset: 0x00132099
		internal override void Compile(Compiler compiler)
		{
			base.CompileAttributes(compiler);
			base.CheckEmpty(compiler);
		}

		// Token: 0x06003730 RID: 14128 RVA: 0x00133EAC File Offset: 0x001320AC
		internal override bool CompileAttribute(Compiler compiler)
		{
			string localName = compiler.Input.LocalName;
			string value = compiler.Input.Value;
			if (Ref.Equal(localName, compiler.Atoms.Method))
			{
				if (compiler.Stylesheetid <= this.methodSId)
				{
					this.method = XsltOutput.ParseOutputMethod(value, compiler);
					this.methodSId = compiler.Stylesheetid;
					if (this.indentSId == 2147483647)
					{
						this.indent = this.method == XsltOutput.OutputMethod.Html;
					}
				}
			}
			else if (Ref.Equal(localName, compiler.Atoms.Version))
			{
				if (compiler.Stylesheetid <= this.versionSId)
				{
					this.version = value;
					this.versionSId = compiler.Stylesheetid;
				}
			}
			else
			{
				if (Ref.Equal(localName, compiler.Atoms.Encoding))
				{
					if (compiler.Stylesheetid > this.encodingSId)
					{
						return true;
					}
					try
					{
						this.encoding = Encoding.GetEncoding(value);
						this.encodingSId = compiler.Stylesheetid;
						return true;
					}
					catch (NotSupportedException)
					{
						return true;
					}
					catch (ArgumentException)
					{
						return true;
					}
				}
				if (Ref.Equal(localName, compiler.Atoms.OmitXmlDeclaration))
				{
					if (compiler.Stylesheetid <= this.omitXmlDeclSId)
					{
						this.omitXmlDecl = compiler.GetYesNo(value);
						this.omitXmlDeclSId = compiler.Stylesheetid;
					}
				}
				else if (Ref.Equal(localName, compiler.Atoms.Standalone))
				{
					if (compiler.Stylesheetid <= this.standaloneSId)
					{
						this.standalone = compiler.GetYesNo(value);
						this.standaloneSId = compiler.Stylesheetid;
					}
				}
				else if (Ref.Equal(localName, compiler.Atoms.DocTypePublic))
				{
					if (compiler.Stylesheetid <= this.doctypePublicSId)
					{
						this.doctypePublic = value;
						this.doctypePublicSId = compiler.Stylesheetid;
					}
				}
				else if (Ref.Equal(localName, compiler.Atoms.DocTypeSystem))
				{
					if (compiler.Stylesheetid <= this.doctypeSystemSId)
					{
						this.doctypeSystem = value;
						this.doctypeSystemSId = compiler.Stylesheetid;
					}
				}
				else if (Ref.Equal(localName, compiler.Atoms.Indent))
				{
					if (compiler.Stylesheetid <= this.indentSId)
					{
						this.indent = compiler.GetYesNo(value);
						this.indentSId = compiler.Stylesheetid;
					}
				}
				else if (Ref.Equal(localName, compiler.Atoms.MediaType))
				{
					if (compiler.Stylesheetid <= this.mediaTypeSId)
					{
						this.mediaType = value;
						this.mediaTypeSId = compiler.Stylesheetid;
					}
				}
				else
				{
					if (!Ref.Equal(localName, compiler.Atoms.CDataSectionElements))
					{
						return false;
					}
					string[] array = XmlConvert.SplitString(value);
					if (this.cdataElements == null)
					{
						this.cdataElements = new Hashtable(array.Length);
					}
					for (int i = 0; i < array.Length; i++)
					{
						XmlQualifiedName xmlQualifiedName = compiler.CreateXmlQName(array[i]);
						this.cdataElements[xmlQualifiedName] = xmlQualifiedName;
					}
				}
			}
			return true;
		}

		// Token: 0x06003731 RID: 14129 RVA: 0x00002F50 File Offset: 0x00001150
		internal override void Execute(Processor processor, ActionFrame frame)
		{
		}

		// Token: 0x06003732 RID: 14130 RVA: 0x001341A8 File Offset: 0x001323A8
		private static XsltOutput.OutputMethod ParseOutputMethod(string value, Compiler compiler)
		{
			XmlQualifiedName xmlQualifiedName = compiler.CreateXPathQName(value);
			if (xmlQualifiedName.Namespace.Length != 0)
			{
				return XsltOutput.OutputMethod.Other;
			}
			string name = xmlQualifiedName.Name;
			if (name == "xml")
			{
				return XsltOutput.OutputMethod.Xml;
			}
			if (name == "html")
			{
				return XsltOutput.OutputMethod.Html;
			}
			if (name == "text")
			{
				return XsltOutput.OutputMethod.Text;
			}
			if (compiler.ForwardCompatibility)
			{
				return XsltOutput.OutputMethod.Unknown;
			}
			throw XsltException.Create("'{1}' is an invalid value for the '{0}' attribute.", new string[] { "method", value });
		}

		// Token: 0x0400233C RID: 9020
		private XsltOutput.OutputMethod method = XsltOutput.OutputMethod.Unknown;

		// Token: 0x0400233D RID: 9021
		private int methodSId = int.MaxValue;

		// Token: 0x0400233E RID: 9022
		private Encoding encoding = Encoding.UTF8;

		// Token: 0x0400233F RID: 9023
		private int encodingSId = int.MaxValue;

		// Token: 0x04002340 RID: 9024
		private string version;

		// Token: 0x04002341 RID: 9025
		private int versionSId = int.MaxValue;

		// Token: 0x04002342 RID: 9026
		private bool omitXmlDecl;

		// Token: 0x04002343 RID: 9027
		private int omitXmlDeclSId = int.MaxValue;

		// Token: 0x04002344 RID: 9028
		private bool standalone;

		// Token: 0x04002345 RID: 9029
		private int standaloneSId = int.MaxValue;

		// Token: 0x04002346 RID: 9030
		private string doctypePublic;

		// Token: 0x04002347 RID: 9031
		private int doctypePublicSId = int.MaxValue;

		// Token: 0x04002348 RID: 9032
		private string doctypeSystem;

		// Token: 0x04002349 RID: 9033
		private int doctypeSystemSId = int.MaxValue;

		// Token: 0x0400234A RID: 9034
		private bool indent;

		// Token: 0x0400234B RID: 9035
		private int indentSId = int.MaxValue;

		// Token: 0x0400234C RID: 9036
		private string mediaType = "text/html";

		// Token: 0x0400234D RID: 9037
		private int mediaTypeSId = int.MaxValue;

		// Token: 0x0400234E RID: 9038
		private Hashtable cdataElements;

		// Token: 0x02000563 RID: 1379
		internal enum OutputMethod
		{
			// Token: 0x04002350 RID: 9040
			Xml,
			// Token: 0x04002351 RID: 9041
			Html,
			// Token: 0x04002352 RID: 9042
			Text,
			// Token: 0x04002353 RID: 9043
			Other,
			// Token: 0x04002354 RID: 9044
			Unknown
		}
	}
}
