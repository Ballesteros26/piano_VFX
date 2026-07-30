using System;

namespace System.Web.Hosting
{
	// Token: 0x020006A3 RID: 1699
	public interface IApplicationMonitor : IDisposable
	{
		// Token: 0x060047F7 RID: 18423
		void Start();

		// Token: 0x060047F8 RID: 18424
		void Stop();
	}
}
