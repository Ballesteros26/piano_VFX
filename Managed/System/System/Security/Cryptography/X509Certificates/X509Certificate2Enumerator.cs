using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Supports a simple iteration over a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object. This class cannot be inherited.</summary>
	// Token: 0x020003AB RID: 939
	public sealed class X509Certificate2Enumerator : IEnumerator
	{
		// Token: 0x06001C4E RID: 7246 RVA: 0x00070D4B File Offset: 0x0006EF4B
		internal X509Certificate2Enumerator(X509Certificate2Collection collection)
		{
			this.enumerator = ((IEnumerable)collection).GetEnumerator();
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x00070D5F File Offset: 0x0006EF5F
		public X509Certificate2 Current
		{
			get
			{
				return (X509Certificate2)this.enumerator.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06001C50 RID: 7248 RVA: 0x00070D71 File Offset: 0x0006EF71
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06001C51 RID: 7249 RVA: 0x00070D7E File Offset: 0x0006EF7E
		public void Reset()
		{
			this.enumerator.Reset();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Collections.IEnumerator.Current" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001C52 RID: 7250 RVA: 0x00070D8B File Offset: 0x0006EF8B
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.MoveNext" />.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06001C53 RID: 7251 RVA: 0x00070D71 File Offset: 0x0006EF71
		bool IEnumerator.MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Collections.IEnumerator.Reset" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06001C54 RID: 7252 RVA: 0x00070D7E File Offset: 0x0006EF7E
		void IEnumerator.Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal X509Certificate2Enumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001993 RID: 6547
		private IEnumerator enumerator;
	}
}
