using System;
using System.CodeDom;

namespace System.Web.Services.Description
{
	// Token: 0x020000D7 RID: 215
	internal class MimeParameter
	{
		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00018CD1 File Offset: 0x00016ED1
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x00018CE7 File Offset: 0x00016EE7
		internal string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return string.Empty;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00018CF0 File Offset: 0x00016EF0
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00018D06 File Offset: 0x00016F06
		internal string TypeName
		{
			get
			{
				if (this.typeName != null)
				{
					return this.typeName;
				}
				return string.Empty;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00018D0F File Offset: 0x00016F0F
		internal CodeAttributeDeclarationCollection Attributes
		{
			get
			{
				if (this.attrs == null)
				{
					this.attrs = new CodeAttributeDeclarationCollection();
				}
				return this.attrs;
			}
		}

		// Token: 0x04000395 RID: 917
		private string name;

		// Token: 0x04000396 RID: 918
		private string typeName;

		// Token: 0x04000397 RID: 919
		private CodeAttributeDeclarationCollection attrs;
	}
}
