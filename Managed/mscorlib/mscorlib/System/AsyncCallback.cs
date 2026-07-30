using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>References a method to be called when a corresponding asynchronous operation completes.</summary>
	/// <param name="ar">The result of the asynchronous operation. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012D RID: 301
	// (Invoke) Token: 0x06000A84 RID: 2692
	[ComVisible(true)]
	[Serializable]
	public delegate void AsyncCallback(IAsyncResult ar);
}
