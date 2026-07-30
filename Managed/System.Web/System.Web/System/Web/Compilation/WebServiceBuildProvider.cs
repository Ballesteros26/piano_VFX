using System;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000675 RID: 1653
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
	internal sealed class WebServiceBuildProvider : SimpleBuildProvider
	{
		// Token: 0x060046CC RID: 18124 RVA: 0x000C69AA File Offset: 0x000C4BAA
		protected override SimpleWebHandlerParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
		{
			return new WebServiceParser(context, virtualPath, physicalPath, reader);
		}

		// Token: 0x060046CD RID: 18125 RVA: 0x000C69B6 File Offset: 0x000C4BB6
		protected override SimpleWebHandlerParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
		{
			return new WebServiceParser(context, virtualPath, physicalPath, base.OpenReader(virtualPath.Original));
		}
	}
}
