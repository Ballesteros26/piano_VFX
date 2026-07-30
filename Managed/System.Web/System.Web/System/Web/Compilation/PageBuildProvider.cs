using System;
using System.IO;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200065F RID: 1631
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
	internal sealed class PageBuildProvider : TemplateBuildProvider
	{
		// Token: 0x060045D1 RID: 17873 RVA: 0x000BF10F File Offset: 0x000BD30F
		protected override string MapPath(VirtualPath virtualPath)
		{
			if (virtualPath.IsFake)
			{
				return virtualPath.PhysicalPath;
			}
			return base.MapPath(virtualPath);
		}

		// Token: 0x060045D2 RID: 17874 RVA: 0x000BF127 File Offset: 0x000BD327
		protected override TextReader SpecialOpenReader(VirtualPath virtualPath, out string physicalPath)
		{
			if (virtualPath.IsFake)
			{
				physicalPath = virtualPath.PhysicalPath;
				return new StreamReader(physicalPath);
			}
			physicalPath = null;
			return base.SpecialOpenReader(virtualPath, out physicalPath);
		}

		// Token: 0x060045D3 RID: 17875 RVA: 0x000BF14C File Offset: 0x000BD34C
		protected override BaseCompiler CreateCompiler(TemplateParser parser)
		{
			return new PageCompiler(parser as PageParser);
		}

		// Token: 0x060045D4 RID: 17876 RVA: 0x000B3191 File Offset: 0x000B1391
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, HttpContext context)
		{
			return this.CreateParser(virtualPath, physicalPath, base.OpenReader(virtualPath.Original), context);
		}

		// Token: 0x060045D5 RID: 17877 RVA: 0x000BF159 File Offset: 0x000BD359
		protected override TemplateParser CreateParser(VirtualPath virtualPath, string physicalPath, TextReader reader, HttpContext context)
		{
			return new PageParser(virtualPath, physicalPath, reader, context);
		}
	}
}
