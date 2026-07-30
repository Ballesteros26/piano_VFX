using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Supports a simple iteration over an <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />. This class cannot be inherited.</summary>
	// Token: 0x020003B4 RID: 948
	public sealed class X509ChainElementEnumerator : IEnumerator
	{
		// Token: 0x06001CE2 RID: 7394 RVA: 0x00072538 File Offset: 0x00070738
		internal X509ChainElementEnumerator(IEnumerable enumerable)
		{
			this.enumerator = enumerable.GetEnumerator();
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x0007254C File Offset: 0x0007074C
		public X509ChainElement Current
		{
			get
			{
				return (X509ChainElement)this.enumerator.Current;
			}
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001CE4 RID: 7396 RVA: 0x0007255E File Offset: 0x0007075E
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06001CE5 RID: 7397 RVA: 0x0007256B File Offset: 0x0007076B
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06001CE6 RID: 7398 RVA: 0x00072578 File Offset: 0x00070778
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal X509ChainElementEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040019A8 RID: 6568
		private IEnumerator enumerator;
	}
}
