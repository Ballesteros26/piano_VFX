using System;

namespace System.Net.Sockets
{
	/// <summary>Defines the polling modes for the <see cref="M:System.Net.Sockets.Socket.Poll(System.Int32,System.Net.Sockets.SelectMode)" /> method.</summary>
	// Token: 0x020005C0 RID: 1472
	public enum SelectMode
	{
		/// <summary>Read status mode.</summary>
		// Token: 0x04002622 RID: 9762
		SelectRead,
		/// <summary>Write status mode.</summary>
		// Token: 0x04002623 RID: 9763
		SelectWrite,
		/// <summary>Error status mode.</summary>
		// Token: 0x04002624 RID: 9764
		SelectError
	}
}
