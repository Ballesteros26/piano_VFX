using System;
using System.IO;

namespace System.Net.Security
{
	/// <summary>Provides methods for passing credentials across a stream and requesting or performing authentication for client-server applications.</summary>
	// Token: 0x020005E8 RID: 1512
	public abstract class AuthenticatedStream : Stream
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Security.AuthenticatedStream" /> class. </summary>
		/// <param name="innerStream">A <see cref="T:System.IO.Stream" /> object used by the <see cref="T:System.Net.Security.AuthenticatedStream" />  for sending and receiving data.</param>
		/// <param name="leaveInnerStreamOpen">A <see cref="T:System.Boolean" /> that indicates whether closing this <see cref="T:System.Net.Security.AuthenticatedStream" />  object also closes <paramref name="innerStream" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="innerStream" /> is null.-or-<paramref name="innerStream" /> is equal to <see cref="F:System.IO.Stream.Null" />.</exception>
		// Token: 0x06003011 RID: 12305 RVA: 0x000BD08C File Offset: 0x000BB28C
		protected AuthenticatedStream(Stream innerStream, bool leaveInnerStreamOpen)
		{
			if (innerStream == null || innerStream == Stream.Null)
			{
				throw new ArgumentNullException("innerStream");
			}
			if (!innerStream.CanRead || !innerStream.CanWrite)
			{
				throw new ArgumentException(global::SR.GetString("The stream has to be read/write."), "innerStream");
			}
			this._InnerStream = innerStream;
			this._LeaveStreamOpen = leaveInnerStreamOpen;
		}

		/// <summary>Gets whether the stream used by this <see cref="T:System.Net.Security.AuthenticatedStream" /> for sending and receiving data has been left open.</summary>
		/// <returns>true if the inner stream has been left open; otherwise, false.</returns>
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06003012 RID: 12306 RVA: 0x000BD0E8 File Offset: 0x000BB2E8
		public bool LeaveInnerStreamOpen
		{
			get
			{
				return this._LeaveStreamOpen;
			}
		}

		/// <summary>Gets the stream used by this <see cref="T:System.Net.Security.AuthenticatedStream" /> for sending and receiving data.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> object.</returns>
		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x06003013 RID: 12307 RVA: 0x000BD0F0 File Offset: 0x000BB2F0
		protected Stream InnerStream
		{
			get
			{
				return this._InnerStream;
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Security.AuthenticatedStream" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06003014 RID: 12308 RVA: 0x000BD0F8 File Offset: 0x000BB2F8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (this._LeaveStreamOpen)
					{
						this._InnerStream.Flush();
					}
					else
					{
						this._InnerStream.Close();
					}
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether authentication was successful.</summary>
		/// <returns>true if successful authentication occurred; otherwise, false. </returns>
		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06003015 RID: 12309
		public abstract bool IsAuthenticated { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether both server and client have been authenticated.</summary>
		/// <returns>true if the client and server have been authenticated; otherwise, false.</returns>
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06003016 RID: 12310
		public abstract bool IsMutuallyAuthenticated { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether data sent using this <see cref="T:System.Net.Security.AuthenticatedStream" /> is encrypted.</summary>
		/// <returns>true if data is encrypted before being transmitted over the network and decrypted when it reaches the remote endpoint; otherwise, false.</returns>
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06003017 RID: 12311
		public abstract bool IsEncrypted { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the data sent using this stream is signed.</summary>
		/// <returns>true if the data is signed before being transmitted; otherwise, false.</returns>
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06003018 RID: 12312
		public abstract bool IsSigned { get; }

		/// <summary>Gets a <see cref="T:System.Boolean" /> value that indicates whether the local side of the connection was authenticated as the server.</summary>
		/// <returns>true if the local endpoint was authenticated as the server side of a client-server authenticated connection; false if the local endpoint was authenticated as the client.</returns>
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06003019 RID: 12313
		public abstract bool IsServer { get; }

		// Token: 0x04002783 RID: 10115
		private Stream _InnerStream;

		// Token: 0x04002784 RID: 10116
		private bool _LeaveStreamOpen;
	}
}
