using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> class provides enumeration functionality for the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection. <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator" /> implements the <see cref="T:System.Collections.IEnumerator" /> interface. </summary>
	// Token: 0x0200002F RID: 47
	public sealed class RecipientInfoEnumerator : IEnumerator
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00004390 File Offset: 0x00002590
		internal RecipientInfoEnumerator(IEnumerable enumerable)
		{
			this.enumerator = enumerable.GetEnumerator();
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object that represents the current recipient information structure in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x000043A4 File Offset: 0x000025A4
		public RecipientInfo Current
		{
			get
			{
				return (RecipientInfo)this.enumerator.Current;
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.System#Collections#IEnumerator#Current" /> property retrieves the current <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object from the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object that represents the current recipient information structure in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</returns>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000043B6 File Offset: 0x000025B6
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.MoveNext" /> method advances the enumeration to the next <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		/// <returns>This method returns a bool that specifies whether the enumeration successfully advanced. If the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object, the method returns true. If the enumeration moved past the last item in the enumeration, it returns false.</returns>
		// Token: 0x060000F9 RID: 249 RVA: 0x000043C3 File Offset: 0x000025C3
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.RecipientInfoEnumerator.Reset" /> method resets the enumeration to the first <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfo" /> object in the <see cref="T:System.Security.Cryptography.Pkcs.RecipientInfoCollection" /> collection.</summary>
		// Token: 0x060000FA RID: 250 RVA: 0x000043D0 File Offset: 0x000025D0
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00002FF8 File Offset: 0x000011F8
		internal RecipientInfoEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000E4 RID: 228
		private IEnumerator enumerator;
	}
}
