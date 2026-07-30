using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> class provides enumeration functionality for the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection. <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoEnumerator" /> implements the <see cref="T:System.Collections.IEnumerator" /> interface. </summary>
	// Token: 0x02000034 RID: 52
	public sealed class SignerInfoEnumerator : IEnumerator
	{
		// Token: 0x0600012D RID: 301 RVA: 0x00004A99 File Offset: 0x00002C99
		internal SignerInfoEnumerator(IEnumerable enumerable)
		{
			this.enumerator = enumerable.GetEnumerator();
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object that represents the current signer information structure in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00004AAD File Offset: 0x00002CAD
		public SignerInfo Current
		{
			get
			{
				return (SignerInfo)this.enumerator.Current;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.System#Collections#IEnumerator#Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object that represents the current signer information structure in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</returns>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00004ABF File Offset: 0x00002CBF
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.MoveNext" /> method advances the enumeration to the next   <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		/// <returns>This method returns a bool value that specifies whether the enumeration successfully advanced. If the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object, the method returns true. If the enumeration moved past the last item in the enumeration, it returns false.</returns>
		// Token: 0x06000130 RID: 304 RVA: 0x00004ACC File Offset: 0x00002CCC
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.SignerInfoEnumerator.Reset" /> method resets the enumeration to the first <see cref="T:System.Security.Cryptography.Pkcs.SignerInfo" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.SignerInfoCollection" /> collection.</summary>
		// Token: 0x06000131 RID: 305 RVA: 0x00004AD9 File Offset: 0x00002CD9
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00002FF8 File Offset: 0x000011F8
		internal SignerInfoEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000F7 RID: 247
		private IEnumerator enumerator;
	}
}
