using System;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000674 RID: 1652
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
	internal sealed class WebHandlerBuildProvider : SimpleBuildProvider
	{
		// Token: 0x060046C9 RID: 18121 RVA: 0x000C6988 File Offset: 0x000C4B88
		protected override SimpleWebHandlerParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
		{
			return new WebHandlerParser(context, virtualPath, physicalPath, reader);
		}

		// Token: 0x060046CA RID: 18122 RVA: 0x000C6994 File Offset: 0x000C4B94
		protected override SimpleWebHandlerParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
		{
			return new WebHandlerParser(context, virtualPath, physicalPath, base.OpenReader(virtualPath.Original));
		}
	}
}
