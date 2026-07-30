using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Contributes an interception sink at the context boundary on the client end of a remoting call.</summary>
	// Token: 0x02000786 RID: 1926
	[ComVisible(true)]
	public interface IContributeClientContextSink
	{
		/// <summary>Takes the first sink in the chain of sinks composed so far, and then chains its message sink in front of the chain already formed.</summary>
		/// <returns>The composite sink chain.</returns>
		/// <param name="nextSink">The chain of sinks composed so far. </param>
		// Token: 0x06004F30 RID: 20272
		IMessageSink GetClientContextSink(IMessageSink nextSink);
	}
}
