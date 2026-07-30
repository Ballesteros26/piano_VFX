using System;
using System.ComponentModel;
using System.IO;
using Unity;

namespace System.Net
{
	/// <summary>Provides data for the <see cref="E:System.Net.WebClient.OpenWriteCompleted" /> event.</summary>
	// Token: 0x020004DF RID: 1247
	public class OpenWriteCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06002595 RID: 9621 RVA: 0x00092604 File Offset: 0x00090804
		internal OpenWriteCompletedEventArgs(Stream result, Exception exception, bool cancelled, object userToken)
			: base(exception, cancelled, userToken)
		{
			this.m_Result = result;
		}

		/// <summary>Gets a writable stream that is used to send data to a server.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> where you can write data to be uploaded.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x00092617 File Offset: 0x00090817
		public Stream Result
		{
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.m_Result;
			}
		}

		// Token: 0x06002597 RID: 9623 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal OpenWriteCompletedEventArgs()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040020A8 RID: 8360
		private Stream m_Result;
	}
}
