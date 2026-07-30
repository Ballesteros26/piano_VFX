using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x02000216 RID: 534
	internal sealed class PageThemeFileParser : UserControlParser
	{
		// Token: 0x060015FB RID: 5627 RVA: 0x0003B560 File Offset: 0x00039760
		internal PageThemeFileParser(VirtualPath virtualPath, string inputFile, HttpContext context)
			: base(virtualPath, inputFile, context, "System.Web.UI.PageTheme")
		{
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x0000393A File Offset: 0x00001B3A
		internal override void HandleOptions(object obj)
		{
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0003B570 File Offset: 0x00039770
		internal override void AddDirective(string directive, IDictionary atts)
		{
			if (string.Compare("Register", directive, StringComparison.OrdinalIgnoreCase) == 0)
			{
				base.AddDirective(directive, atts);
				return;
			}
			base.ThrowParseException("Unknown directive: " + directive, Array.Empty<object>());
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x0003B59F File Offset: 0x0003979F
		internal override string DefaultBaseTypeName
		{
			get
			{
				return "System.Web.UI.PageTheme";
			}
		}
	}
}
