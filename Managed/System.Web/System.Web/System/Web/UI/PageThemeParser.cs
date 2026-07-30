using System;

namespace System.Web.UI
{
	// Token: 0x02000217 RID: 535
	internal sealed class PageThemeParser : UserControlParser
	{
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0003B5A6 File Offset: 0x000397A6
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x0003B5AE File Offset: 0x000397AE
		public string[] LinkedStyleSheets
		{
			get
			{
				return this.linkedStyleSheets;
			}
			set
			{
				this.linkedStyleSheets = value;
			}
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x0003B5B7 File Offset: 0x000397B7
		internal PageThemeParser(VirtualPath virtualPath, HttpContext context)
			: base(virtualPath, virtualPath.PhysicalPath, context, "System.Web.UI.PageTheme")
		{
			this.AddDependency(virtualPath.Original);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0000393A File Offset: 0x00001B3A
		internal override void HandleOptions(object obj)
		{
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x0003B59F File Offset: 0x0003979F
		internal override string DefaultBaseTypeName
		{
			get
			{
				return "System.Web.UI.PageTheme";
			}
		}

		// Token: 0x0400153F RID: 5439
		private string[] linkedStyleSheets;
	}
}
