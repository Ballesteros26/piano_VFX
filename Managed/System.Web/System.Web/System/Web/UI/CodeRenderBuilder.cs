using System;
using System.Web.Compilation;

namespace System.Web.UI
{
	// Token: 0x020001B0 RID: 432
	internal sealed class CodeRenderBuilder : CodeBuilder
	{
		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060010B6 RID: 4278 RVA: 0x0002E101 File Offset: 0x0002C301
		// (set) Token: 0x060010B7 RID: 4279 RVA: 0x0002E109 File Offset: 0x0002C309
		public bool HtmlEncode { get; private set; }

		// Token: 0x060010B8 RID: 4280 RVA: 0x0002E112 File Offset: 0x0002C312
		public CodeRenderBuilder(string code, bool isAssign, ILocation location, bool doHtmlEncode)
			: base(code, isAssign, location)
		{
			this.HtmlEncode = doHtmlEncode;
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x0002E125 File Offset: 0x0002C325
		public CodeRenderBuilder(string code, bool isAssign, ILocation location)
			: base(code, isAssign, location)
		{
		}
	}
}
