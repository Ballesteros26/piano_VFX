using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadFileCompleted" /> event.</summary>
	// Token: 0x020004E9 RID: 1257
	public class UploadFileCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060025B8 RID: 9656 RVA: 0x000926A9 File Offset: 0x000908A9
		internal UploadFileCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		/// <summary>Gets the server reply to a data upload operation that is started by calling an <see cref="Overload:System.Net.WebClient.UploadFileAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the server reply.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x060025B9 RID: 9657 RVA: 0x000926BC File Offset: 0x000908BC
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal UploadFileCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040020AD RID: 8365
		private byte[] m_Result;
	}
}
