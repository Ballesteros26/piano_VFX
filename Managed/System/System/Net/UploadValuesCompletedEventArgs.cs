using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadValuesCompleted" /> event.</summary>
	// Token: 0x020004EB RID: 1259
	public class UploadValuesCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060025BF RID: 9663 RVA: 0x000926CA File Offset: 0x000908CA
		internal UploadValuesCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		/// <summary>Gets the server reply to a data upload operation started by calling an <see cref="Overload:System.Net.WebClient.UploadValuesAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the server reply.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x060025C0 RID: 9664 RVA: 0x000926DD File Offset: 0x000908DD
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal UploadValuesCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040020AE RID: 8366
		private byte[] m_Result;
	}
}
