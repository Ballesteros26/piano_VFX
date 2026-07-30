using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography
{
	/// <summary>Provides enumeration functionality for the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection. This class cannot be inherited. </summary>
	// Token: 0x02000015 RID: 21
	public sealed class CryptographicAttributeObjectEnumerator : IEnumerator
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002FAB File Offset: 0x000011AB
		internal CryptographicAttributeObjectEnumerator(IEnumerable enumerable)
		{
			this.enumerator = enumerable.GetEnumerator();
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object from the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object that represents the current cryptographic attribute in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002FBF File Offset: 0x000011BF
		public CryptographicAttributeObject Current
		{
			get
			{
				return (CryptographicAttributeObject)this.enumerator.Current;
			}
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object from the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>A <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object that represents the current cryptographic attribute in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</returns>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002FD1 File Offset: 0x000011D1
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>Advances the enumeration to the next <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		/// <returns>true if the enumeration successfully moved to the next <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object; false if the enumerator is at the end of the enumeration.</returns>
		// Token: 0x0600004E RID: 78 RVA: 0x00002FDE File Offset: 0x000011DE
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Resets the enumeration to the first <see cref="T:System.Security.Cryptography.CryptographicAttributeObject" /> object in the <see cref="T:System.Security.Cryptography.CryptographicAttributeObjectCollection" /> collection.</summary>
		// Token: 0x0600004F RID: 79 RVA: 0x00002FEB File Offset: 0x000011EB
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002FF8 File Offset: 0x000011F8
		internal CryptographicAttributeObjectEnumerator()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400009B RID: 155
		private IEnumerator enumerator;
	}
}
