using System;
using System.CodeDom.Compiler;

namespace System.Xml.Xsl
{
	/// <summary>Specifies the XSLT features to support during execution of the XSLT style sheet.</summary>
	// Token: 0x020004E2 RID: 1250
	public sealed class XsltSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltSettings" /> class with default settings.</summary>
		// Token: 0x060032E9 RID: 13033 RVA: 0x001249E7 File Offset: 0x00122BE7
		public XsltSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Xsl.XsltSettings" /> class with the specified settings.</summary>
		/// <param name="enableDocumentFunction">true to enable support for the XSLT document() function; otherwise, false.</param>
		/// <param name="enableScript">true to enable support for embedded scripts blocks; otherwise, false.</param>
		// Token: 0x060032EA RID: 13034 RVA: 0x001249F6 File Offset: 0x00122BF6
		public XsltSettings(bool enableDocumentFunction, bool enableScript)
		{
			this.enableDocumentFunction = enableDocumentFunction;
			this.enableScript = enableScript;
		}

		/// <summary>Gets an <see cref="T:System.Xml.Xsl.XsltSettings" /> object with default settings. Support for the XSLT document() function and embedded script blocks is disabled.</summary>
		/// <returns>An <see cref="T:System.Xml.Xsl.XsltSettings" /> object with the <see cref="P:System.Xml.Xsl.XsltSettings.EnableDocumentFunction" /> and <see cref="P:System.Xml.Xsl.XsltSettings.EnableScript" /> properties set to false.</returns>
		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x060032EB RID: 13035 RVA: 0x00124A13 File Offset: 0x00122C13
		public static XsltSettings Default
		{
			get
			{
				return new XsltSettings(false, false);
			}
		}

		/// <summary>Gets an <see cref="T:System.Xml.Xsl.XsltSettings" /> object that enables support for the XSLT document() function and embedded script blocks.</summary>
		/// <returns>An <see cref="T:System.Xml.Xsl.XsltSettings" /> object with the <see cref="P:System.Xml.Xsl.XsltSettings.EnableDocumentFunction" /> and <see cref="P:System.Xml.Xsl.XsltSettings.EnableScript" /> properties set to true.</returns>
		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x060032EC RID: 13036 RVA: 0x00124A1C File Offset: 0x00122C1C
		public static XsltSettings TrustedXslt
		{
			get
			{
				return new XsltSettings(true, true);
			}
		}

		/// <summary>Gets or sets a value indicating whether to enable support for the XSLT document() function.</summary>
		/// <returns>true to support the XSLT document() function; otherwise, false. The default is false.</returns>
		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x060032ED RID: 13037 RVA: 0x00124A25 File Offset: 0x00122C25
		// (set) Token: 0x060032EE RID: 13038 RVA: 0x00124A2D File Offset: 0x00122C2D
		public bool EnableDocumentFunction
		{
			get
			{
				return this.enableDocumentFunction;
			}
			set
			{
				this.enableDocumentFunction = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether to enable support for embedded script blocks.</summary>
		/// <returns>true to support script blocks in XSLT style sheets; otherwise, false. The default is false.</returns>
		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x060032EF RID: 13039 RVA: 0x00124A36 File Offset: 0x00122C36
		// (set) Token: 0x060032F0 RID: 13040 RVA: 0x00124A3E File Offset: 0x00122C3E
		public bool EnableScript
		{
			get
			{
				return this.enableScript;
			}
			set
			{
				this.enableScript = value;
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x060032F1 RID: 13041 RVA: 0x00124A47 File Offset: 0x00122C47
		// (set) Token: 0x060032F2 RID: 13042 RVA: 0x00124A4F File Offset: 0x00122C4F
		internal bool CheckOnly
		{
			get
			{
				return this.checkOnly;
			}
			set
			{
				this.checkOnly = value;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x00124A58 File Offset: 0x00122C58
		// (set) Token: 0x060032F4 RID: 13044 RVA: 0x00124A60 File Offset: 0x00122C60
		internal bool IncludeDebugInformation
		{
			get
			{
				return this.includeDebugInformation;
			}
			set
			{
				this.includeDebugInformation = value;
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x00124A69 File Offset: 0x00122C69
		// (set) Token: 0x060032F6 RID: 13046 RVA: 0x00124A71 File Offset: 0x00122C71
		internal int WarningLevel
		{
			get
			{
				return this.warningLevel;
			}
			set
			{
				this.warningLevel = value;
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x060032F7 RID: 13047 RVA: 0x00124A7A File Offset: 0x00122C7A
		// (set) Token: 0x060032F8 RID: 13048 RVA: 0x00124A82 File Offset: 0x00122C82
		internal bool TreatWarningsAsErrors
		{
			get
			{
				return this.treatWarningsAsErrors;
			}
			set
			{
				this.treatWarningsAsErrors = value;
			}
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x060032F9 RID: 13049 RVA: 0x00124A8B File Offset: 0x00122C8B
		// (set) Token: 0x060032FA RID: 13050 RVA: 0x00124A93 File Offset: 0x00122C93
		internal TempFileCollection TempFiles
		{
			get
			{
				return this.tempFiles;
			}
			set
			{
				this.tempFiles = value;
			}
		}

		// Token: 0x040020FD RID: 8445
		private bool enableDocumentFunction;

		// Token: 0x040020FE RID: 8446
		private bool enableScript;

		// Token: 0x040020FF RID: 8447
		private bool checkOnly;

		// Token: 0x04002100 RID: 8448
		private bool includeDebugInformation;

		// Token: 0x04002101 RID: 8449
		private int warningLevel = -1;

		// Token: 0x04002102 RID: 8450
		private bool treatWarningsAsErrors;

		// Token: 0x04002103 RID: 8451
		private TempFileCollection tempFiles;
	}
}
