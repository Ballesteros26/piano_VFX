using System;
using System.Runtime.Serialization;

namespace System.Web
{
	// Token: 0x02000076 RID: 118
	[Serializable]
	internal abstract class HtmlizedException : HttpException
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x0000956E File Offset: 0x0000776E
		protected HtmlizedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00009578 File Offset: 0x00007778
		protected HtmlizedException()
		{
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00009580 File Offset: 0x00007780
		protected HtmlizedException(string message)
			: base(message)
		{
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00009589 File Offset: 0x00007789
		protected HtmlizedException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000484 RID: 1156
		public abstract string Title { get; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000485 RID: 1157
		public new abstract string Description { get; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000486 RID: 1158
		public abstract string ErrorMessage { get; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000487 RID: 1159
		public abstract string FileName { get; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000488 RID: 1160
		public abstract string SourceFile { get; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000489 RID: 1161
		public abstract string FileText { get; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600048A RID: 1162
		public abstract int[] ErrorLines { get; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600048B RID: 1163
		public abstract bool ErrorLinesPaired { get; }
	}
}
