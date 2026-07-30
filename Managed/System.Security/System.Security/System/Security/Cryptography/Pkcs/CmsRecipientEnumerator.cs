using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> class provides enumeration functionality for the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection. <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator" /> implements the <see cref="T:System.Collections.IEnumerator" /> interface. </summary>
	// Token: 0x0200001F RID: 31
	public sealed class CmsRecipientEnumerator : IEnumerator
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000035DE File Offset: 0x000017DE
		internal CmsRecipientEnumerator(IEnumerable enumerable)
		{
			this.enumerator = enumerable.GetEnumerator();
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the current recipient in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000035F2 File Offset: 0x000017F2
		public CmsRecipient Current
		{
			get
			{
				return (CmsRecipient)this.enumerator.Current;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.System#Collections#IEnumerator#Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object that represents the current recipient in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</returns>
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00003604 File Offset: 0x00001804
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.MoveNext" /> method advances the enumeration to the next <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		/// <returns>true if the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object; false if the enumeration moved past the last item in the enumeration.</returns>
		// Token: 0x0600007C RID: 124 RVA: 0x00003611 File Offset: 0x00001811
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.CmsRecipientEnumerator.Reset" /> method resets the enumeration to the first <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipient" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.CmsRecipientCollection" /> collection.</summary>
		// Token: 0x0600007D RID: 125 RVA: 0x0000361E File Offset: 0x0000181E
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002FF8 File Offset: 0x000011F8
		internal CmsRecipientEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000B6 RID: 182
		private IEnumerator enumerator;
	}
}
