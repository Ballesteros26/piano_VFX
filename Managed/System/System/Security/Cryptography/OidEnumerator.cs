using System;
using System.Collections;

namespace System.Security.Cryptography
{
	/// <summary>Provides the ability to navigate through an <see cref="T:System.Security.Cryptography.OidCollection" /> object. This class cannot be inherited.</summary>
	// Token: 0x02000390 RID: 912
	public sealed class OidEnumerator : IEnumerator
	{
		// Token: 0x06001BA3 RID: 7075 RVA: 0x000020EB File Offset: 0x000002EB
		private OidEnumerator()
		{
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0006DA8E File Offset: 0x0006BC8E
		internal OidEnumerator(OidCollection oids)
		{
			this.m_oids = oids;
			this.m_current = -1;
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.Oid" /> object in the collection.</returns>
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001BA5 RID: 7077 RVA: 0x0006DAA4 File Offset: 0x0006BCA4
		public Oid Current
		{
			get
			{
				return this.m_oids[this.m_current];
			}
		}

		/// <summary>Gets the current <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>The current <see cref="T:System.Security.Cryptography.Oid" /> object.</returns>
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001BA6 RID: 7078 RVA: 0x0006DAA4 File Offset: 0x0006BCA4
		object IEnumerator.Current
		{
			get
			{
				return this.m_oids[this.m_current];
			}
		}

		/// <summary>Advances to the next <see cref="T:System.Security.Cryptography.Oid" /> object in an <see cref="T:System.Security.Cryptography.OidCollection" /> object.</summary>
		/// <returns>true, if the enumerator was successfully advanced to the next element; false, if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06001BA7 RID: 7079 RVA: 0x0006DAB7 File Offset: 0x0006BCB7
		public bool MoveNext()
		{
			if (this.m_current == this.m_oids.Count - 1)
			{
				return false;
			}
			this.m_current++;
			return true;
		}

		/// <summary>Sets an enumerator to its initial position.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created.</exception>
		// Token: 0x06001BA8 RID: 7080 RVA: 0x0006DADF File Offset: 0x0006BCDF
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x040018E9 RID: 6377
		private OidCollection m_oids;

		// Token: 0x040018EA RID: 6378
		private int m_current;
	}
}
