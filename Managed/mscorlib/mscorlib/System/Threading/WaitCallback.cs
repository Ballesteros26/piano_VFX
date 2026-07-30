using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents a callback method to be executed by a thread pool thread.</summary>
	/// <param name="state">An object containing information to be used by the callback method. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000489 RID: 1161
	// (Invoke) Token: 0x060036FE RID: 14078
	[ComVisible(true)]
	public delegate void WaitCallback(object state);
}
