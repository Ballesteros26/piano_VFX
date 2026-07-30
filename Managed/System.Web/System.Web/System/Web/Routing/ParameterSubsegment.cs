using System;

namespace System.Web.Routing
{
	// Token: 0x020004E6 RID: 1254
	internal sealed class ParameterSubsegment : PathSubsegment
	{
		// Token: 0x0600386A RID: 14442 RVA: 0x000978C8 File Offset: 0x00095AC8
		public ParameterSubsegment(string parameterName)
		{
			if (parameterName.StartsWith("*", StringComparison.Ordinal))
			{
				this.ParameterName = parameterName.Substring(1);
				this.IsCatchAll = true;
				return;
			}
			this.ParameterName = parameterName;
		}

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x0600386B RID: 14443 RVA: 0x000978FA File Offset: 0x00095AFA
		// (set) Token: 0x0600386C RID: 14444 RVA: 0x00097902 File Offset: 0x00095B02
		public bool IsCatchAll { get; private set; }

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x0600386D RID: 14445 RVA: 0x0009790B File Offset: 0x00095B0B
		// (set) Token: 0x0600386E RID: 14446 RVA: 0x00097913 File Offset: 0x00095B13
		public string ParameterName { get; private set; }
	}
}
