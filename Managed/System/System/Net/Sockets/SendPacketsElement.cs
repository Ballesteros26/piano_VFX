using System;

namespace System.Net.Sockets
{
	/// <summary>Represents an element in a <see cref="T:System.Net.Sockets.SendPacketsElement" /> array.</summary>
	// Token: 0x020005CB RID: 1483
	public class SendPacketsElement
	{
		// Token: 0x06002EE6 RID: 12006 RVA: 0x000020EB File Offset: 0x000002EB
		private SendPacketsElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified file.</summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		// Token: 0x06002EE7 RID: 12007 RVA: 0x000B9BBF File Offset: 0x000B7DBF
		public SendPacketsElement(string filepath)
			: this(filepath, 0, 0, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified filename path, offset, and count.</summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the file to the location in the file to start sending the file specified in the <paramref name="filepath" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, the entire file is sent. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero. The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the file indicated by the <paramref name="filepath" /> parameter.</exception>
		// Token: 0x06002EE8 RID: 12008 RVA: 0x000B9BCB File Offset: 0x000B7DCB
		public SendPacketsElement(string filepath, int offset, int count)
			: this(filepath, offset, count, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified filename path, buffer offset, and count with an option to combine this element with the next element in a single send request from the sockets layer to the transport. </summary>
		/// <param name="filepath">The filename of the file to be transmitted using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the file to the location in the file to start sending the file specified in the <paramref name="filepath" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, the entire file is sent.</param>
		/// <param name="endOfPacket">A Boolean value that specifies that this element should not be combined with the next element in a single send request from the sockets layer to the transport. This flag is used for granular control of the content of each message on a datagram or message-oriented socket.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="filepath" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero. The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the file indicated by the <paramref name="filepath" /> parameter.</exception>
		// Token: 0x06002EE9 RID: 12009 RVA: 0x000B9BD7 File Offset: 0x000B7DD7
		public SendPacketsElement(string filepath, int offset, int count, bool endOfPacket)
		{
			if (filepath == null)
			{
				throw new ArgumentNullException("filepath");
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Initialize(filepath, null, offset, count, endOfPacket);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer.</summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		// Token: 0x06002EEA RID: 12010 RVA: 0x000B9C17 File Offset: 0x000B7E17
		public SendPacketsElement(byte[] buffer)
			: this(buffer, 0, (buffer != null) ? buffer.Length : 0, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer, buffer offset, and count.</summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the <paramref name="buffer" /> to the location in the <paramref name="buffer" /> to start sending the data specified in the <paramref name="buffer" /> parameter.</param>
		/// <param name="count">The number of bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, no bytes are sent.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero. The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the buffer</exception>
		// Token: 0x06002EEB RID: 12011 RVA: 0x000B9C2B File Offset: 0x000B7E2B
		public SendPacketsElement(byte[] buffer, int offset, int count)
			: this(buffer, offset, count, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class using the specified buffer, buffer offset, and count with an option to combine this element with the next element in a single send request from the sockets layer to the transport. </summary>
		/// <param name="buffer">A byte array of data to send using the <see cref="M:System.Net.Sockets.Socket.SendPacketsAsync(System.Net.Sockets.SocketAsyncEventArgs)" /> method.</param>
		/// <param name="offset">The offset, in bytes, from the beginning of the <paramref name="buffer" /> to the location in the <paramref name="buffer" /> to start sending the data specified in the <paramref name="buffer" /> parameter.</param>
		/// <param name="count">The number bytes to send starting from the <paramref name="offset" /> parameter. If <paramref name="count" /> is zero, no bytes are sent.</param>
		/// <param name="endOfPacket">A Boolean value that specifies that this element should not be combined with the next element in a single send request from the sockets layer to the transport. This flag is used for granular control of the content of each message on a datagram or message-oriented socket. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="buffer" /> parameter cannot be null</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="offset" /> and <paramref name="count" /> parameters must be greater than or equal to zero. The <paramref name="offset" /> and <paramref name="count" /> must be less than the size of the buffer</exception>
		// Token: 0x06002EEC RID: 12012 RVA: 0x000B9C38 File Offset: 0x000B7E38
		public SendPacketsElement(byte[] buffer, int offset, int count, bool endOfPacket)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset > buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || count > buffer.Length - offset)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			this.Initialize(null, buffer, offset, count, endOfPacket);
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x000B9C91 File Offset: 0x000B7E91
		private void Initialize(string filePath, byte[] buffer, int offset, int count, bool endOfPacket)
		{
			this.m_FilePath = filePath;
			this.m_Buffer = buffer;
			this.m_Offset = offset;
			this.m_Count = count;
			this.m_endOfPacket = endOfPacket;
		}

		/// <summary>Gets the filename of the file to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="filepath" /> parameter.</summary>
		/// <returns>The filename of the file to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="filepath" /> parameter.</returns>
		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06002EEE RID: 12014 RVA: 0x000B9CB8 File Offset: 0x000B7EB8
		public string FilePath
		{
			get
			{
				return this.m_FilePath;
			}
		}

		/// <summary>Gets the buffer to be sent if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="buffer" /> parameter.</summary>
		/// <returns>The byte buffer to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="buffer" /> parameter.</returns>
		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002EEF RID: 12015 RVA: 0x000B9CC0 File Offset: 0x000B7EC0
		public byte[] Buffer
		{
			get
			{
				return this.m_Buffer;
			}
		}

		/// <summary>Gets the count of bytes to be sent. </summary>
		/// <returns>The count of bytes to send if the <see cref="T:System.Net.Sockets.SendPacketsElement" /> class was initialized with a <paramref name="count" /> parameter.</returns>
		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x000B9CC8 File Offset: 0x000B7EC8
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		/// <summary>Gets the offset, in bytes, from the beginning of the data buffer or file to the location in the buffer or file to start sending the data. </summary>
		/// <returns>The offset, in bytes, from the beginning of the data buffer or file to the location in the buffer or file to start sending the data.</returns>
		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002EF1 RID: 12017 RVA: 0x000B9CD0 File Offset: 0x000B7ED0
		public int Offset
		{
			get
			{
				return this.m_Offset;
			}
		}

		/// <summary>Gets a Boolean value that indicates if this element should not be combined with the next element in a single send request from the sockets layer to the transport.</summary>
		/// <returns>A Boolean value that indicates if this element should not be combined with the next element in a single send request.</returns>
		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002EF2 RID: 12018 RVA: 0x000B9CD8 File Offset: 0x000B7ED8
		public bool EndOfPacket
		{
			get
			{
				return this.m_endOfPacket;
			}
		}

		// Token: 0x0400266A RID: 9834
		internal string m_FilePath;

		// Token: 0x0400266B RID: 9835
		internal byte[] m_Buffer;

		// Token: 0x0400266C RID: 9836
		internal int m_Offset;

		// Token: 0x0400266D RID: 9837
		internal int m_Count;

		// Token: 0x0400266E RID: 9838
		private bool m_endOfPacket;
	}
}
