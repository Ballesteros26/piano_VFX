using System;

namespace System.Xml.Xsl.Xslt
{
	// Token: 0x0200057E RID: 1406
	internal struct Pattern
	{
		// Token: 0x060037B8 RID: 14264 RVA: 0x00136522 File Offset: 0x00134722
		public Pattern(TemplateMatch match, int priority)
		{
			this.Match = match;
			this.Priority = priority;
		}

		// Token: 0x0400243B RID: 9275
		public readonly TemplateMatch Match;

		// Token: 0x0400243C RID: 9276
		public readonly int Priority;
	}
}
