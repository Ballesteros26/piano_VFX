using System;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000672 RID: 1650
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
	internal sealed class UserControlBuildProvider : TemplateBuildProvider
	{
		// Token: 0x060046BE RID: 18110 RVA: 0x000C67DC File Offset: 0x000C49DC
		protected override BaseCompiler CreateCompiler(TemplateParser parser)
		{
			return new UserControlCompiler(parser as UserControlParser);
		}

		// Token: 0x060046BF RID: 18111 RVA: 0x000B3191 File Offset: 0x000B1391
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
		{
			return this.CreateParser(virtualPath, physicalPath, base.OpenReader(virtualPath.Original), context);
		}

		// Token: 0x060046C0 RID: 18112 RVA: 0x000C67E9 File Offset: 0x000C49E9
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
		{
			return new UserControlParser(virtualPath, physicalPath, reader, context);
		}
	}
}
