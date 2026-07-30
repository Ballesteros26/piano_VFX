using System;

namespace System.Data
{
	/// <summary>Describes the current state of the connection to a data source.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000056 RID: 86
	[Flags]
	public enum ConnectionState
	{
		/// <summary>The connection is closed.</summary>
		// Token: 0x040004FF RID: 1279
		Closed = 0,
		/// <summary>The connection is open.</summary>
		// Token: 0x04000500 RID: 1280
		Open = 1,
		/// <summary>The connection object is connecting to the data source.</summary>
		// Token: 0x04000501 RID: 1281
		Connecting = 2,
		/// <summary>The connection object is executing a command. (This value is reserved for future versions of the product.) </summary>
		// Token: 0x04000502 RID: 1282
		Executing = 4,
		/// <summary>The connection object is retrieving data. (This value is reserved for future versions of the product.) </summary>
		// Token: 0x04000503 RID: 1283
		Fetching = 8,
		/// <summary>The connection to the data source is broken. This can occur only after the connection has been opened. A connection in this state may be closed and then re-opened. (This value is reserved for future versions of the product.) </summary>
		// Token: 0x04000504 RID: 1284
		Broken = 16
	}
}
