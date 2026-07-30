using System;

namespace System.Threading
{
	/// <summary>Represents a method to be called when a message is to be dispatched to a synchronization context.  </summary>
	/// <param name="state">The object passed to the delegate.</param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200047D RID: 1149
	// (Invoke) Token: 0x06003641 RID: 13889
	public delegate void SendOrPostCallback(object state);
}
