using System;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000619 RID: 1561
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
	internal class ApplicationFileBuildProvider : TemplateBuildProvider
	{
		// Token: 0x06004321 RID: 17185 RVA: 0x000B3184 File Offset: 0x000B1384
		protected override BaseCompiler CreateCompiler(TemplateParser parser)
		{
			return new GlobalAsaxCompiler(parser as ApplicationFileParser);
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x000B3191 File Offset: 0x000B1391
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
		{
			return this.CreateParser(virtualPath, physicalPath, base.OpenReader(virtualPath.Original), context);
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x000B31A8 File Offset: 0x000B13A8
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
		{
			return new ApplicationFileParser(virtualPath, physicalPath, reader, context);
		}
	}
}
