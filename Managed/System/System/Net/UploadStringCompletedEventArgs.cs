using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.UploadStringCompleted" /> event.</summary>
	// Token: 0x020004E5 RID: 1253
	public class UploadStringCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x060025AA RID: 9642 RVA: 0x00092667 File Offset: 0x00090867
		internal UploadStringCompletedEventArgs(string result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		/// <summary>Gets the server reply to a string upload operation that is started by calling an <see cref="Overload:System.Net.WebClient.UploadStringAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the server reply.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060025AB RID: 9643 RVA: 0x0009267A File Offset: 0x0009087A
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal UploadStringCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040020AB RID: 8363
		private string m_Result;
	}
}
