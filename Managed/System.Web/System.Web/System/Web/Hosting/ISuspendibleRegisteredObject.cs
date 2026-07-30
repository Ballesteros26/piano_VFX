using System;

namespace System.Web.Hosting
{
	/// <summary>Listens for suspend and resume notifications.</summary>
	// Token: 0x02000545 RID: 1349
	public interface ISuspendibleRegisteredObject : IRegisteredObject
	{
		/// <summary>Called when ASP.NET notifies an application that a process is being suspended.</summary>
		// Token: 0x06003A89 RID: 14985
		Action Suspend();
	}
}
