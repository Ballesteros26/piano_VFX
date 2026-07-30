using System;

namespace System.Web
{
	// Token: 0x020000E0 RID: 224
	internal sealed class InfoTraceData
	{
		// Token: 0x06000C12 RID: 3090 RVA: 0x000202E1 File Offset: 0x0001E4E1
		public InfoTraceData(string category, string message, string exception, double timeSinceFirst, double timeSinceLast, bool isWarning)
		{
			this.Category = category;
			this.Message = message;
			this.Exception = exception;
			this.TimeSinceFirst = timeSinceFirst;
			this.TimeSinceLast = timeSinceLast;
			this.IsWarning = isWarning;
		}

		// Token: 0x040010CD RID: 4301
		public string Category;

		// Token: 0x040010CE RID: 4302
		public string Message;

		// Token: 0x040010CF RID: 4303
		public string Exception;

		// Token: 0x040010D0 RID: 4304
		public double TimeSinceFirst;

		// Token: 0x040010D1 RID: 4305
		public double TimeSinceLast;

		// Token: 0x040010D2 RID: 4306
		public bool IsWarning;
	}
}
