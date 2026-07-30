using System;

namespace System.Web.Hosting
{
	/// <summary>Defines methods for objects that are managed by the hosting environment.</summary>
	// Token: 0x02000543 RID: 1347
	public interface IRegisteredObject
	{
		/// <summary>Requests a registered object to unregister.</summary>
		/// <param name="immediate">true to indicate the registered object should unregister from the hosting environment before returning; otherwise, false.</param>
		// Token: 0x06003A87 RID: 14983
		void Stop(bool immediate);
	}
}
