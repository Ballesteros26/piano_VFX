using System;

namespace System.Web.Compilation
{
	/// <summary>Contains event data for the <see cref="E:System.Web.Compilation.ClientBuildManager.AppDomainShutdown" /> event and the <see cref="E:System.Web.Compilation.ClientBuildManager.AppDomainUnloaded" /> event. </summary>
	// Token: 0x0200063F RID: 1599
	public class BuildManagerHostUnloadEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.BuildManagerHostUnloadEventArgs" /> class. </summary>
		/// <param name="reason">The reason for the hosted application domain shutdown.</param>
		// Token: 0x060044CD RID: 17613 RVA: 0x000BCB99 File Offset: 0x000BAD99
		public BuildManagerHostUnloadEventArgs(ApplicationShutdownReason reason)
		{
			this.reason = reason;
		}

		/// <summary>Gets the reason the hosted application domain was shut down.</summary>
		/// <returns>One of the <see cref="T:System.Web.ApplicationShutdownReason" /> enumerated values.</returns>
		// Token: 0x1700157F RID: 5503
		// (get) Token: 0x060044CE RID: 17614 RVA: 0x000BCBA8 File Offset: 0x000BADA8
		public ApplicationShutdownReason Reason
		{
			get
			{
				return this.reason;
			}
		}

		// Token: 0x040024B9 RID: 9401
		private ApplicationShutdownReason reason;
	}
}
