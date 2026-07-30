using System;
using System.IO;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x0200024E RID: 590
	internal class WebHandlerParser : SimpleWebHandlerParser
	{
		// Token: 0x06001816 RID: 6166 RVA: 0x00040E42 File Offset: 0x0003F042
		private WebHandlerParser(HttpContext context, string virtualPath, string physicalPath)
			: base(context, virtualPath, physicalPath)
		{
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00040E4D File Offset: 0x0003F04D
		internal WebHandlerParser(HttpContext context, VirtualPath virtualPath, TextReader reader)
			: this(context, virtualPath, null, reader)
		{
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00040E59 File Offset: 0x0003F059
		internal WebHandlerParser(HttpContext context, VirtualPath virtualPath, string physicalPath, TextReader reader)
			: base(context, virtualPath.Original, physicalPath, reader)
		{
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00040E6C File Offset: 0x0003F06C
		public static Type GetCompiledType(HttpContext context, string virtualPath, string physicalPath)
		{
			WebHandlerParser webHandlerParser = new WebHandlerParser(context, virtualPath, physicalPath);
			Type compiledTypeFromCache = webHandlerParser.GetCompiledTypeFromCache();
			if (compiledTypeFromCache != null)
			{
				return compiledTypeFromCache;
			}
			return WebServiceCompiler.CompileIntoType(webHandlerParser);
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x00040E9A File Offset: 0x0003F09A
		protected override string DefaultDirectiveName
		{
			get
			{
				return "webhandler";
			}
		}
	}
}
