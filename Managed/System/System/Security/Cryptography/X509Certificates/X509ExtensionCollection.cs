using System;
using System.Collections;
using Mono.Security;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020003BB RID: 955
	public sealed class X509ExtensionCollection : ICollection, IEnumerable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> class. </summary>
		// Token: 0x06001D45 RID: 7493 RVA: 0x0007400C File Offset: 0x0007220C
		public X509ExtensionCollection()
		{
			this._list = new ArrayList();
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x00074020 File Offset: 0x00072220
		internal X509ExtensionCollection(Mono.Security.X509.X509Certificate cert)
		{
			this._list = new ArrayList(cert.Extensions.Count);
			if (cert.Extensions.Count == 0)
			{
				return;
			}
			foreach (object obj in cert.Extensions)
			{
				Mono.Security.X509.X509Extension x509Extension = (Mono.Security.X509.X509Extension)obj;
				bool critical = x509Extension.Critical;
				string oid = x509Extension.Oid;
				byte[] array = null;
				Mono.Security.ASN1 value = x509Extension.Value;
				if (value.Tag == 4 && value.Count > 0)
				{
					array = value[0].GetBytes();
				}
				global::System.Security.Cryptography.X509Certificates.X509Extension x509Extension2 = (global::System.Security.Cryptography.X509Certificates.X509Extension)CryptoConfig.CreateFromName(oid, new object[]
				{
					new AsnEncodedData(oid, array ?? global::System.Security.Cryptography.X509Certificates.X509ExtensionCollection.Empty),
					critical
				});
				if (x509Extension2 == null)
				{
					x509Extension2 = new global::System.Security.Cryptography.X509Certificates.X509Extension(oid, array ?? global::System.Security.Cryptography.X509Certificates.X509ExtensionCollection.Empty, critical);
				}
				this._list.Add(x509Extension2);
			}
		}

		/// <summary>Gets the number of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects in a <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An integer representing the number of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects in the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001D47 RID: 7495 RVA: 0x0007413C File Offset: 0x0007233C
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		/// <summary>Gets a value indicating whether the collection is guaranteed to be thread safe.</summary>
		/// <returns>true if the collection is thread safe; otherwise, false.</returns>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001D48 RID: 7496 RVA: 0x00074149 File Offset: 0x00072349
		public bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		/// <summary>Gets an object that you can use to synchronize access to the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An object that you can use to synchronize access to the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001D49 RID: 7497 RVA: 0x00002068 File Offset: 0x00000268
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object at the specified index.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object.</returns>
		/// <param name="index">The location of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object to retrieve. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="index" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is equal to or greater than the length of the array. </exception>
		// Token: 0x17000611 RID: 1553
		public global::System.Security.Cryptography.X509Certificates.X509Extension this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new InvalidOperationException("index");
				}
				return (global::System.Security.Cryptography.X509Certificates.X509Extension)this._list[index];
			}
		}

		/// <summary>Gets the first <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object whose value or friendly name is specified by an object identifier (OID).</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object.</returns>
		/// <param name="oid">The object identifier (OID) of the extension to retrieve. </param>
		// Token: 0x17000612 RID: 1554
		public global::System.Security.Cryptography.X509Certificates.X509Extension this[string oid]
		{
			get
			{
				if (oid == null)
				{
					throw new ArgumentNullException("oid");
				}
				if (this._list.Count == 0 || oid.Length == 0)
				{
					return null;
				}
				foreach (object obj in this._list)
				{
					global::System.Security.Cryptography.X509Certificates.X509Extension x509Extension = (global::System.Security.Cryptography.X509Certificates.X509Extension)obj;
					if (x509Extension.Oid.Value.Equals(oid))
					{
						return x509Extension;
					}
				}
				return null;
			}
		}

		/// <summary>Adds an <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> object to an <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>The index at which the <paramref name="extension" /> parameter was added.</returns>
		/// <param name="extension">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" />  object to add to the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="extension" /> parameter is null.</exception>
		// Token: 0x06001D4C RID: 7500 RVA: 0x0007420C File Offset: 0x0007240C
		public int Add(global::System.Security.Cryptography.X509Certificates.X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			return this._list.Add(extension);
		}

		/// <summary>Copies a collection into an array starting at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects. </param>
		/// <param name="index">The location in the array at which copying starts. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is a zero-length string or contains an invalid value. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="index" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> specifies a value that is not in the range of the array. </exception>
		// Token: 0x06001D4D RID: 7501 RVA: 0x00074228 File Offset: 0x00072428
		public void CopyTo(global::System.Security.Cryptography.X509Certificates.X509Extension[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("negative index");
			}
			if (index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index >= array.Length");
			}
			this._list.CopyTo(array, index);
		}

		/// <summary>Copies the collection into an array starting at the specified index.</summary>
		/// <param name="array">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Extension" /> objects. </param>
		/// <param name="index">The location in the array at which copying starts. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is a zero-length string or contains an invalid value. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="index" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> specifies a value that is not in the range of the array. </exception>
		// Token: 0x06001D4E RID: 7502 RVA: 0x00074265 File Offset: 0x00072465
		void ICollection.CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("negative index");
			}
			if (index >= array.Length)
			{
				throw new ArgumentOutOfRangeException("index >= array.Length");
			}
			this._list.CopyTo(array, index);
		}

		/// <summary>Returns an enumerator that can iterate through an <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionEnumerator" /> object to use to iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x06001D4F RID: 7503 RVA: 0x000742A5 File Offset: 0x000724A5
		public X509ExtensionEnumerator GetEnumerator()
		{
			return new X509ExtensionEnumerator(this._list);
		}

		/// <summary>Returns an enumerator that can iterate through an <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object to use to iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509ExtensionCollection" /> object.</returns>
		// Token: 0x06001D50 RID: 7504 RVA: 0x000742A5 File Offset: 0x000724A5
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new X509ExtensionEnumerator(this._list);
		}

		// Token: 0x040019C9 RID: 6601
		private static byte[] Empty = new byte[0];

		// Token: 0x040019CA RID: 6602
		private ArrayList _list;
	}
}
