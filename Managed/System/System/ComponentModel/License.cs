using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides the abstract base class for all licenses. A license is granted to a specific instance of a component.</summary>
	// Token: 0x0200029C RID: 668
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class License : IDisposable
	{
		/// <summary>When overridden in a derived class, gets the license key granted to this component.</summary>
		/// <returns>A license key granted to this component.</returns>
		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060014C0 RID: 5312
		public abstract string LicenseKey { get; }

		/// <summary>When overridden in a derived class, disposes of the resources used by the license.</summary>
		// Token: 0x060014C1 RID: 5313
		public abstract void Dispose();
	}
}
