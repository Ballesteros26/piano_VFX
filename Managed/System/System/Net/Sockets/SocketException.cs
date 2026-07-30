using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Net.Sockets
{
	/// <summary>The exception that is thrown when a socket error occurs.</summary>
	// Token: 0x020005B5 RID: 1461
	[Serializable]
	public class SocketException : Win32Exception
	{
		// Token: 0x06002D7C RID: 11644
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int WSAGetLastError_internal();

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class with the last operating system error code.</summary>
		// Token: 0x06002D7D RID: 11645 RVA: 0x000B4219 File Offset: 0x000B2419
		public SocketException()
			: base(SocketException.WSAGetLastError_internal())
		{
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x0007D76E File Offset: 0x0007B96E
		internal SocketException(int error, string message)
			: base(error, message)
		{
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000B4226 File Offset: 0x000B2426
		internal SocketException(EndPoint endPoint)
			: base(Marshal.GetLastWin32Error())
		{
			this.m_EndPoint = endPoint;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class with the specified error code.</summary>
		/// <param name="errorCode">The error code that indicates the error that occurred. </param>
		// Token: 0x06002D80 RID: 11648 RVA: 0x0007D765 File Offset: 0x0007B965
		public SocketException(int errorCode)
			: base(errorCode)
		{
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000B423A File Offset: 0x000B243A
		internal SocketException(int errorCode, EndPoint endPoint)
			: base(errorCode)
		{
			this.m_EndPoint = endPoint;
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x0007D765 File Offset: 0x0007B965
		internal SocketException(SocketError socketError)
			: base((int)socketError)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Sockets.SocketException" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance that contains the information that is required to serialize the new <see cref="T:System.Net.Sockets.SocketException" /> instance. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.Sockets.SocketException" /> instance. </param>
		// Token: 0x06002D83 RID: 11651 RVA: 0x0007D778 File Offset: 0x0007B978
		protected SocketException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Gets the error code that is associated with this exception.</summary>
		/// <returns>An integer error code that is associated with this exception.</returns>
		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x0007D782 File Offset: 0x0007B982
		public override int ErrorCode
		{
			get
			{
				return base.NativeErrorCode;
			}
		}

		/// <summary>Gets the error message that is associated with this exception.</summary>
		/// <returns>A string that contains the error message. </returns>
		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x000B424A File Offset: 0x000B244A
		public override string Message
		{
			get
			{
				if (this.m_EndPoint == null)
				{
					return base.Message;
				}
				return base.Message + " " + this.m_EndPoint.ToString();
			}
		}

		/// <summary>Gets the error code that is associated with this exception.</summary>
		/// <returns>An integer error code that is associated with this exception.</returns>
		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x0007D782 File Offset: 0x0007B982
		public SocketError SocketErrorCode
		{
			get
			{
				return (SocketError)base.NativeErrorCode;
			}
		}

		// Token: 0x0400258D RID: 9613
		[NonSerialized]
		private EndPoint m_EndPoint;
	}
}
