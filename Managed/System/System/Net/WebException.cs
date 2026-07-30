using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net
{
	/// <summary>The exception that is thrown when an error occurs while accessing the network through a pluggable protocol.</summary>
	// Token: 0x02000463 RID: 1123
	[Serializable]
	public class WebException : InvalidOperationException, ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class.</summary>
		// Token: 0x06002104 RID: 8452 RVA: 0x0007FFCF File Offset: 0x0007E1CF
		public WebException()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message.</summary>
		/// <param name="message">The text of the error message. </param>
		// Token: 0x06002105 RID: 8453 RVA: 0x0007FFDF File Offset: 0x0007E1DF
		public WebException(string message)
			: this(message, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message and nested exception.</summary>
		/// <param name="message">The text of the error message. </param>
		/// <param name="innerException">A nested exception. </param>
		// Token: 0x06002106 RID: 8454 RVA: 0x0007FFE9 File Offset: 0x0007E1E9
		public WebException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message and status.</summary>
		/// <param name="message">The text of the error message. </param>
		/// <param name="status">One of the <see cref="T:System.Net.WebExceptionStatus" /> values. </param>
		// Token: 0x06002107 RID: 8455 RVA: 0x0007FFFB File Offset: 0x0007E1FB
		public WebException(string message, WebExceptionStatus status)
			: this(message, null, status, null)
		{
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x00080007 File Offset: 0x0007E207
		internal WebException(string message, WebExceptionStatus status, WebExceptionInternalStatus internalStatus, Exception innerException)
			: this(message, innerException, status, null, internalStatus)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class with the specified error message, nested exception, status, and response.</summary>
		/// <param name="message">The text of the error message. </param>
		/// <param name="innerException">A nested exception. </param>
		/// <param name="status">One of the <see cref="T:System.Net.WebExceptionStatus" /> values. </param>
		/// <param name="response">A <see cref="T:System.Net.WebResponse" /> instance that contains the response from the remote host. </param>
		// Token: 0x06002109 RID: 8457 RVA: 0x00080015 File Offset: 0x0007E215
		public WebException(string message, Exception innerException, WebExceptionStatus status, WebResponse response)
			: this(message, null, innerException, status, response)
		{
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x00080024 File Offset: 0x0007E224
		internal WebException(string message, string data, Exception innerException, WebExceptionStatus status, WebResponse response)
			: base(message + ((data != null) ? (": '" + data + "'") : ""), innerException)
		{
			this.m_Status = status;
			this.m_Response = response;
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x00080070 File Offset: 0x0007E270
		internal WebException(string message, Exception innerException, WebExceptionStatus status, WebResponse response, WebExceptionInternalStatus internalStatus)
			: this(message, null, innerException, status, response, internalStatus)
		{
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x00080080 File Offset: 0x0007E280
		internal WebException(string message, string data, Exception innerException, WebExceptionStatus status, WebResponse response, WebExceptionInternalStatus internalStatus)
			: base(message + ((data != null) ? (": '" + data + "'") : ""), innerException)
		{
			this.m_Status = status;
			this.m_Response = response;
			this.m_InternalStatus = internalStatus;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebException" /> class from the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> instances.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that contains the information required to serialize the new <see cref="T:System.Net.WebException" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains the source of the serialized stream that is associated with the new <see cref="T:System.Net.WebException" />. </param>
		// Token: 0x0600210D RID: 8461 RVA: 0x000800D4 File Offset: 0x0007E2D4
		protected WebException(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(serializationInfo, streamingContext)
		{
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.WebException" /> will be serialized. </param>
		/// <param name="streamingContext">The destination of the serialization. </param>
		// Token: 0x0600210E RID: 8462 RVA: 0x000800E6 File Offset: 0x0007E2E6
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> instance with the data needed to serialize the <see cref="T:System.Net.WebException" />.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to be used. </param>
		/// <param name="streamingContext">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> to be used. </param>
		// Token: 0x0600210F RID: 8463 RVA: 0x00023A82 File Offset: 0x00021C82
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Gets the status of the response.</summary>
		/// <returns>One of the <see cref="T:System.Net.WebExceptionStatus" /> values.</returns>
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x000800F0 File Offset: 0x0007E2F0
		public WebExceptionStatus Status
		{
			get
			{
				return this.m_Status;
			}
		}

		/// <summary>Gets the response that the remote host returned.</summary>
		/// <returns>If a response is available from the Internet resource, a <see cref="T:System.Net.WebResponse" /> instance that contains the error response from an Internet resource; otherwise, null.</returns>
		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x000800F8 File Offset: 0x0007E2F8
		public WebResponse Response
		{
			get
			{
				return this.m_Response;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x00080100 File Offset: 0x0007E300
		internal WebExceptionInternalStatus InternalStatus
		{
			get
			{
				return this.m_InternalStatus;
			}
		}

		// Token: 0x04001E00 RID: 7680
		private WebExceptionStatus m_Status = WebExceptionStatus.UnknownError;

		// Token: 0x04001E01 RID: 7681
		private WebResponse m_Response;

		// Token: 0x04001E02 RID: 7682
		[NonSerialized]
		private WebExceptionInternalStatus m_InternalStatus;
	}
}
