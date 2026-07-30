using System;

namespace System.Web.Handlers
{
	// Token: 0x02000107 RID: 263
	[Serializable]
	internal class TraceNotAvailableException : HttpException
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x0002588C File Offset: 0x00023A8C
		public TraceNotAvailableException(bool notLocal)
			: base(notLocal ? 403 : 500, "Trace Error")
		{
			this.notLocal = notLocal;
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000258AF File Offset: 0x00023AAF
		internal override string Description
		{
			get
			{
				if (this.notLocal)
				{
					return "Trace is not enabled for remote clients.";
				}
				return "Trace.axd is not enabled in the configuration file for this application.";
			}
		}

		// Token: 0x0400116E RID: 4462
		private bool notLocal;
	}
}
