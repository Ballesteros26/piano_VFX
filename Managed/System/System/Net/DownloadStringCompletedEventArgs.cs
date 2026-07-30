using System;
using System.ComponentModel;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.DownloadStringCompleted" /> event.</summary>
	// Token: 0x020004E1 RID: 1249
	public class DownloadStringCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600259C RID: 9628 RVA: 0x00092625 File Offset: 0x00090825
		internal DownloadStringCompletedEventArgs(string result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		/// <summary>Gets the data that is downloaded by a <see cref="Overload:System.Net.WebClient.DownloadStringAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the downloaded data.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x00092638 File Offset: 0x00090838
		public string Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal DownloadStringCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040020A9 RID: 8361
		private string m_Result;
	}
}
