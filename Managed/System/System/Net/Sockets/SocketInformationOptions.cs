using System;

namespace System.Net.Sockets
{
	/// <summary>Describes states for a <see cref="T:System.Net.Sockets.Socket" />.</summary>
	// Token: 0x020005D0 RID: 1488
	[Flags]
	public enum SocketInformationOptions
	{
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is nonblocking.</summary>
		// Token: 0x040026B1 RID: 9905
		NonBlocking = 1,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is connected.</summary>
		// Token: 0x040026B2 RID: 9906
		Connected = 2,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> is listening for new connections.</summary>
		// Token: 0x040026B3 RID: 9907
		Listening = 4,
		/// <summary>The <see cref="T:System.Net.Sockets.Socket" /> uses overlapped I/O.</summary>
		// Token: 0x040026B4 RID: 9908
		UseOnlyOverlappedIO = 8
	}
}
