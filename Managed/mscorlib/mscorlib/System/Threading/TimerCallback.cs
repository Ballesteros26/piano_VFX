using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents the method that handles calls from a <see cref="T:System.Threading.Timer" />.</summary>
	/// <param name="state">An object containing application-specific information relevant to the method invoked by this delegate, or null. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004B1 RID: 1201
	// (Invoke) Token: 0x06003842 RID: 14402
	[ComVisible(true)]
	public delegate void TimerCallback(object state);
}
