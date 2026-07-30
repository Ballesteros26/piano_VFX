using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Represents the method that executes on a <see cref="T:System.Threading.Thread" />.</summary>
	/// <param name="obj">An object that contains data for the thread procedure.</param>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200047B RID: 1147
	// (Invoke) Token: 0x06003639 RID: 13881
	[ComVisible(false)]
	public delegate void ParameterizedThreadStart(object obj);
}
