using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.DownloadDataCompleted" /> event.</summary>
	// Token: 0x020004E3 RID: 1251
	public class DownloadDataCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060025A3 RID: 9635 RVA: 0x00092646 File Offset: 0x00090846
		internal DownloadDataCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		/// <summary>Gets the data that is downloaded by a <see cref="Overload:System.Net.WebClient.DownloadDataAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the downloaded data.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060025A4 RID: 9636 RVA: 0x00092659 File Offset: 0x00090859
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal DownloadDataCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040020AA RID: 8362
		private byte[] m_Result;
	}
}
