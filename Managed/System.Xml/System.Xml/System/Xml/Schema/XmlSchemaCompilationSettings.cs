using System;

namespace System.Xml.Schema
{
	/// <summary>Provides schema compilation options for the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> class This class cannot be inherited.</summary>
	// Token: 0x02000442 RID: 1090
	public sealed class XmlSchemaCompilationSettings
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Schema.XmlSchemaCompilationSettings" /> class. </summary>
		// Token: 0x06002B55 RID: 11093 RVA: 0x001056D3 File Offset: 0x001038D3
		public XmlSchemaCompilationSettings()
		{
			this.enableUpaCheck = true;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> should check for Unique Particle Attribution (UPA) violations.</summary>
		/// <returns>true if the <see cref="T:System.Xml.Schema.XmlSchemaSet" /> should check for Unique Particle Attribution (UPA) violations; otherwise, false. The default is true.</returns>
		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002B56 RID: 11094 RVA: 0x001056E2 File Offset: 0x001038E2
		// (set) Token: 0x06002B57 RID: 11095 RVA: 0x001056EA File Offset: 0x001038EA
		public bool EnableUpaCheck
		{
			get
			{
				return this.enableUpaCheck;
			}
			set
			{
				this.enableUpaCheck = value;
			}
		}

		// Token: 0x04001D4E RID: 7502
		private bool enableUpaCheck;
	}
}
