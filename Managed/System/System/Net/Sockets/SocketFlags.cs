using System;

namespace System.Net.Sockets
{
	/// <summary>Specifies socket send and receive behaviors.</summary>
	// Token: 0x020005CE RID: 1486
	[Flags]
	public enum SocketFlags
	{
		/// <summary>Use no flags for this call.</summary>
		// Token: 0x040026A3 RID: 9891
		None = 0,
		/// <summary>Process out-of-band data.</summary>
		// Token: 0x040026A4 RID: 9892
		OutOfBand = 1,
		/// <summary>Peek at the incoming message.</summary>
		// Token: 0x040026A5 RID: 9893
		Peek = 2,
		/// <summary>Send without using routing tables.</summary>
		// Token: 0x040026A6 RID: 9894
		DontRoute = 4,
		/// <summary>Provides a standard value for the number of WSABUF structures that are used to send and receive data. This value is not used or supported on .NET Framework 4.5.</summary>
		// Token: 0x040026A7 RID: 9895
		MaxIOVectorLength = 16,
		/// <summary>The message was too large to fit into the specified buffer and was truncated.</summary>
		// Token: 0x040026A8 RID: 9896
		Truncated = 256,
		/// <summary>Indicates that the control data did not fit into an internal 64-KB buffer and was truncated.</summary>
		// Token: 0x040026A9 RID: 9897
		ControlDataTruncated = 512,
		/// <summary>Indicates a broadcast packet.</summary>
		// Token: 0x040026AA RID: 9898
		Broadcast = 1024,
		/// <summary>Indicates a multicast packet.</summary>
		// Token: 0x040026AB RID: 9899
		Multicast = 2048,
		/// <summary>Partial send or receive for message.</summary>
		// Token: 0x040026AC RID: 9900
		Partial = 32768
	}
}
