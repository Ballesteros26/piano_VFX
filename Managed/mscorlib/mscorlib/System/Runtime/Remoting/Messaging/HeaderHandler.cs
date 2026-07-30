using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Represents the method that will handle processing of headers on the stream during deserialization.</summary>
	/// <returns>A <see cref="T:System.Object" /> that conveys information about a remote function call.</returns>
	/// <param name="headers">The headers of the event. </param>
	// Token: 0x02000808 RID: 2056
	// (Invoke) Token: 0x0600524B RID: 21067
	[ComVisible(true)]
	public delegate object HeaderHandler(Header[] headers);
}
