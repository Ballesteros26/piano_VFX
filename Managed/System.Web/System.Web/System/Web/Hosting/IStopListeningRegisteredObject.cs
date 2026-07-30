using System;

namespace System.Web.Hosting
{
	/// <summary>Listens for GL_STOP_LISTENING notifications from IIS.</summary>
	// Token: 0x02000544 RID: 1348
	public interface IStopListeningRegisteredObject : IRegisteredObject
	{
		/// <summary>Stops listening for new requests.</summary>
		// Token: 0x06003A88 RID: 14984
		void StopListening();
	}
}
