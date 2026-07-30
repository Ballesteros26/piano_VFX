using System;
using System.Collections;
using Unity;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Supports a simple iteration over a <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />. This class cannot be inherited.</summary>
	// Token: 0x020003BC RID: 956
	public sealed class X509ExtensionEnumerator : IEnumerator
	{
		// Token: 0x06001D52 RID: 7506 RVA: 0x000742BF File Offset: 0x000724BF
		internal X509ExtensionEnumerator(ArrayList list)
		{
			this.enumerator = list.GetEnumerator();
		}

		/// <summary>Gets the current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x000742D3 File Offset: 0x000724D3
		public X509Extension Current
		{
			get
			{
				return (X509Extension)this.enumerator.Current;
			}
		}

		/// <summary>Gets an object from a collection.</summary>
		/// <returns>The current element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enumerator is positioned before the first element of the collection or after the last element. </exception>
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x000742E5 File Offset: 0x000724E5
		object IEnumerator.Current
		{
			get
			{
				return this.enumerator.Current;
			}
		}

		/// <summary>Advances the enumerator to the next element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</summary>
		/// <returns>true if the enumerator was successfully advanced to the next element; false if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06001D55 RID: 7509 RVA: 0x000742F2 File Offset: 0x000724F2
		public bool MoveNext()
		{
			return this.enumerator.MoveNext();
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The collection was modified after the enumerator was created. </exception>
		// Token: 0x06001D56 RID: 7510 RVA: 0x000742FF File Offset: 0x000724FF
		public void Reset()
		{
			this.enumerator.Reset();
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal X509ExtensionEnumerator()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040019CB RID: 6603
		private IEnumerator enumerator;
	}
}
