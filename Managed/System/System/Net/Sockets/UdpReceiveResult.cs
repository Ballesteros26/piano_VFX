using System;

namespace System.Net.Sockets
{
	/// <summary>Presents UDP receive result information from a call to the <see cref="M:System.Net.Sockets.UdpClient.ReceiveAsync" /> method.</summary>
	// Token: 0x020005DA RID: 1498
	public struct UdpReceiveResult : IEquatable<UdpReceiveResult>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.UdpReceiveResult" /> class.</summary>
		/// <param name="buffer">A buffer for data to receive in the UDP packet.</param>
		/// <param name="remoteEndPoint">The remote endpoint of the UDP packet.</param>
		// Token: 0x06002F79 RID: 12153 RVA: 0x000BB8BD File Offset: 0x000B9ABD
		public UdpReceiveResult(byte[] buffer, IPEndPoint remoteEndPoint)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (remoteEndPoint == null)
			{
				throw new ArgumentNullException("remoteEndPoint");
			}
			this.m_buffer = buffer;
			this.m_remoteEndPoint = remoteEndPoint;
		}

		/// <summary>Gets a buffer with the data received in the UDP packet.</summary>
		/// <returns>Returns <see cref="T:System.Byte" />.A <see cref="T:System.Byte" /> array with the data received in the UDP packet.</returns>
		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06002F7A RID: 12154 RVA: 0x000BB8E9 File Offset: 0x000B9AE9
		public byte[] Buffer
		{
			get
			{
				return this.m_buffer;
			}
		}

		/// <summary>Gets the remote endpoint from which the UDP packet was received. </summary>
		/// <returns>Returns <see cref="T:System.Net.IPEndPoint" />.The remote endpoint from which the UDP packet was received.</returns>
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002F7B RID: 12155 RVA: 0x000BB8F1 File Offset: 0x000B9AF1
		public IPEndPoint RemoteEndPoint
		{
			get
			{
				return this.m_remoteEndPoint;
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>Returns <see cref="T:System.Int32" />.The hash code.</returns>
		// Token: 0x06002F7C RID: 12156 RVA: 0x000BB8F9 File Offset: 0x000B9AF9
		public override int GetHashCode()
		{
			if (this.m_buffer == null)
			{
				return 0;
			}
			return this.m_buffer.GetHashCode() ^ this.m_remoteEndPoint.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="obj" /> is an instance of <see cref="T:System.Net.Sockets.UdpReceiveResult" /> and equals the value of the instance; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x06002F7D RID: 12157 RVA: 0x000BB91C File Offset: 0x000B9B1C
		public override bool Equals(object obj)
		{
			return obj is UdpReceiveResult && this.Equals((UdpReceiveResult)obj);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="other" /> is an instance of <see cref="T:System.Net.Sockets.UdpReceiveResult" /> and equals the value of the instance; otherwise, false.</returns>
		/// <param name="other">The object to compare with this instance.</param>
		// Token: 0x06002F7E RID: 12158 RVA: 0x000BB934 File Offset: 0x000B9B34
		public bool Equals(UdpReceiveResult other)
		{
			return object.Equals(this.m_buffer, other.m_buffer) && object.Equals(this.m_remoteEndPoint, other.m_remoteEndPoint);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instances are equivalent.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="left" /> and <paramref name="right" /> are equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the right of the equality operator.</param>
		// Token: 0x06002F7F RID: 12159 RVA: 0x000BB95C File Offset: 0x000B9B5C
		public static bool operator ==(UdpReceiveResult left, UdpReceiveResult right)
		{
			return left.Equals(right);
		}

		/// <summary>Tests whether two specified <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instances are not equal.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if <paramref name="left" /> and <paramref name="right" /> are unequal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the left of the not equal operator.</param>
		/// <param name="right">The <see cref="T:System.Net.Sockets.UdpReceiveResult" /> instance that is to the right of the not equal operator.</param>
		// Token: 0x06002F80 RID: 12160 RVA: 0x000BB966 File Offset: 0x000B9B66
		public static bool operator !=(UdpReceiveResult left, UdpReceiveResult right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04002711 RID: 10001
		private byte[] m_buffer;

		// Token: 0x04002712 RID: 10002
		private IPEndPoint m_remoteEndPoint;
	}
}
