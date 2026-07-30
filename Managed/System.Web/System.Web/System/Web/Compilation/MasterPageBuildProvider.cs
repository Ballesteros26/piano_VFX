using System;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200065D RID: 1629
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
	internal sealed class MasterPageBuildProvider : TemplateBuildProvider
	{
		// Token: 0x060045CB RID: 17867 RVA: 0x000BF034 File Offset: 0x000BD234
		protected override BaseCompiler CreateCompiler(TemplateParser parser)
		{
			return new MasterPageCompiler(parser as MasterPageParser);
		}

		// Token: 0x060045CC RID: 17868 RVA: 0x000B3191 File Offset: 0x000B1391
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
		{
			return this.CreateParser(virtualPath, physicalPath, base.OpenReader(virtualPath.Original), context);
		}

		// Token: 0x060045CD RID: 17869 RVA: 0x000BF041 File Offset: 0x000BD241
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
		{
			return new MasterPageParser(virtualPath, physicalPath, reader, context);
		}
	}
}
