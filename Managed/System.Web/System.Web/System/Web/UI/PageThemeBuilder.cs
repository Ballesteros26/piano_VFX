using System;

namespace System.Web.UI
{
	// Token: 0x02000215 RID: 533
	internal class PageThemeBuilder : UserControlControlBuilder
	{
		// Token: 0x060015F9 RID: 5625 RVA: 0x0003B54E File Offset: 0x0003974E
		public override void AppendLiteralString(string s)
		{
			throw new HttpException(string.Format("Literal content ('{0}') not allowed within a skin file", s));
		}
	}
}
