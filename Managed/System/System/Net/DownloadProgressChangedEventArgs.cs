using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.DownloadProgressChanged" /> event of a <see cref="T:System.Net.WebClient" />.</summary>
	// Token: 0x020004ED RID: 1261
	public class DownloadProgressChangedEventArgs : ProgressChangedEventArgs
	{
		// Token: 0x060025C6 RID: 9670 RVA: 0x000926EB File Offset: 0x000908EB
		internal DownloadProgressChangedEventArgs(int progressPercentage, object userToken, long bytesReceived, long totalBytesToReceive)
			: base(progressPercentage, userToken)
		{
			this.m_BytesReceived = bytesReceived;
			this.m_TotalBytesToReceive = totalBytesToReceive;
		}

		/// <summary>Gets the number of bytes received.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes received.</returns>
		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x060025C7 RID: 9671 RVA: 0x00092704 File Offset: 0x00090904
		public long BytesReceived
		{
			get
			{
				return this.m_BytesReceived;
			}
		}

		/// <summary>Gets the total number of bytes in a <see cref="T:System.Net.WebClient" /> data download operation.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that indicates the number of bytes that will be received.</returns>
		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x060025C8 RID: 9672 RVA: 0x0009270C File Offset: 0x0009090C
		public long TotalBytesToReceive
		{
			get
			{
				return this.m_TotalBytesToReceive;
			}
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal DownloadProgressChangedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040020AF RID: 8367
		private long m_BytesReceived;

		// Token: 0x040020B0 RID: 8368
		private long m_TotalBytesToReceive;
	}
}
