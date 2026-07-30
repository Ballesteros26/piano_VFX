using System;
using System.Threading.Tasks;

namespace System.Web.SessionState
{
	// Token: 0x020006E0 RID: 1760
	public interface ISessionStateModule : IHttpModule
	{
		// Token: 0x06004AAE RID: 19118
		void ReleaseSessionState(HttpContext context);

		// Token: 0x06004AAF RID: 19119
		Task ReleaseSessionStateAsync(HttpContext context);
	}
}
