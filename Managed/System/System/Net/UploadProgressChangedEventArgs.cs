using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadProgressChanged" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	// Token: 0x020004EF RID: 1263
	public class UploadProgressChangedEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x060025CE RID: 9678 RVA: 0x00092714 File Offset: 0x00090914
		internal UploadProgressChangedEventArgs(int progressPercentage, object userToken, long bytesSent, long totalBytesToSend, long bytesReceived, long totalBytesToReceive)
			: base(progressPercentage, userToken)
		{
			this.m_BytesReceived = bytesReceived;
			this.m_TotalBytesToReceive = totalBytesToReceive;
			this.m_BytesSent = bytesSent;
			this.m_TotalBytesToSend = totalBytesToSend;
		}

		/// <summary>Gets the number of bytes received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes received.</returns>
		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x060025CF RID: 9679 RVA: 0x0009273D File Offset: 0x0009093D
		public long BytesReceived
		{
			get
			{
				return this.m_BytesReceived;
			}
		}

		/// <summary>Gets the total number of bytes in a <see cref="T:System.Net.WebClient" /> data upload operation.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be received.</returns>
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x060025D0 RID: 9680 RVA: 0x00092745 File Offset: 0x00090945
		public long TotalBytesToReceive
		{
			get
			{
				return this.m_TotalBytesToReceive;
			}
		}

		/// <summary>Gets the number of bytes sent.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes sent.</returns>
		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x060025D1 RID: 9681 RVA: 0x0009274D File Offset: 0x0009094D
		public long BytesSent
		{
			get
			{
				return this.m_BytesSent;
			}
		}

		/// <summary>Gets the total number of bytes to send.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be sent.</returns>
		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x060025D2 RID: 9682 RVA: 0x00092755 File Offset: 0x00090955
		public long TotalBytesToSend
		{
			get
			{
				return this.m_TotalBytesToSend;
			}
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal UploadProgressChangedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040020B1 RID: 8369
		private long m_BytesReceived;

		// Token: 0x040020B2 RID: 8370
		private long m_TotalBytesToReceive;

		// Token: 0x040020B3 RID: 8371
		private long m_BytesSent;

		// Token: 0x040020B4 RID: 8372
		private long m_TotalBytesToSend;
	}
}
