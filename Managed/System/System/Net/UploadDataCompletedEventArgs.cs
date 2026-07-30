using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadDataCompleted" /> event.</summary>
	// Token: 0x020004E7 RID: 1255
	public class UploadDataCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060025B1 RID: 9649 RVA: 0x00092688 File Offset: 0x00090888
		internal UploadDataCompletedEventArgs(byte[] result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		/// <summary>Gets the server reply to a data upload operation started by calling an <see cref="Overload:System.Net.WebClient.UploadDataAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array containing the server reply.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x0009269B File Offset: 0x0009089B
		public byte[] Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x060025B3 RID: 9651 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal UploadDataCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040020AC RID: 8364
		private byte[] m_Result;
	}
}
