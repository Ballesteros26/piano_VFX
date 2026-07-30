using System;
using System.ComponentModel;
using System.IO;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.OpenReadCompleted" /> event.</summary>
	// Token: 0x020004DD RID: 1245
	public class OpenReadCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x0600258E RID: 9614 RVA: 0x000925E3 File Offset: 0x000907E3
		internal OpenReadCompletedEventArgs(Stream result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		/// <summary>Gets a readable stream that contains data downloaded by a <see cref="Overload:System.Net.WebClient.DownloadDataAsync" /> method.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> that contains the downloaded data.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600258F RID: 9615 RVA: 0x000925F6 File Offset: 0x000907F6
		public Stream Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal OpenReadCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040020A7 RID: 8359
		private Stream m_Result;
	}
}
