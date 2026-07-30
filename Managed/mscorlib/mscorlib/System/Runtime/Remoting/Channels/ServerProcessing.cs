using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Indicates the status of the server message processing.</summary>
	// Token: 0x020007B7 RID: 1975
	[ComVisible(true)]
	[Serializable]
	public enum ServerProcessing
	{
		/// <summary>The server synchronously processed the message.</summary>
		// Token: 0x04002A61 RID: 10849
		Complete,
		/// <summary>The message was dispatched and no response can be sent.</summary>
		// Token: 0x04002A62 RID: 10850
		OneWay,
		/// <summary>The call was dispatched asynchronously, which indicates that the sink must store response data on the stack for later processing.</summary>
		// Token: 0x04002A63 RID: 10851
		Async
	}
}
