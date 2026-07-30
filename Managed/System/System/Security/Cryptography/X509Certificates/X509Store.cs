using System;
using System.Security.Permissions;
using Mono.Security.X509;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an X.509 store, which is a physical store where certificates are persisted and managed. This class cannot be inherited.</summary>
	// Token: 0x020003C0 RID: 960
	public sealed class X509Store : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the personal certificates of the current user store.</summary>
		// Token: 0x06001D72 RID: 7538 RVA: 0x0007497F File Offset: 0x00072B7F
		public X509Store()
			: this("MY", StoreLocation.CurrentUser)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the specified store name.</summary>
		/// <param name="storeName">A string value that represents the store name. See <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" />  for more information. </param>
		// Token: 0x06001D73 RID: 7539 RVA: 0x0007498D File Offset: 0x00072B8D
		public X509Store(string storeName)
			: this(storeName, StoreLocation.CurrentUser)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> value.</summary>
		/// <param name="storeName">One of the enumeration values that specifies the name of the X.509 certificate store. </param>
		// Token: 0x06001D74 RID: 7540 RVA: 0x00074997 File Offset: 0x00072B97
		public X509Store(StoreName storeName)
			: this(storeName, StoreLocation.CurrentUser)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.StoreLocation" /> value.</summary>
		/// <param name="storeLocation">One of the enumeration values that specifies the location of the X.509 certificate store. </param>
		// Token: 0x06001D75 RID: 7541 RVA: 0x000749A1 File Offset: 0x00072BA1
		public X509Store(StoreLocation storeLocation)
			: this("MY", storeLocation)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using the specified <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> and <see cref="T:System.Security.Cryptography.X509Certificates.StoreLocation" /> values.</summary>
		/// <param name="storeName">One of the enumeration values that specifies the name of the X.509 certificate store. </param>
		/// <param name="storeLocation">One of the enumeration values that specifies the location of the X.509 certificate store. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="storeLocation" /> is not a valid location or <paramref name="storeName" /> is not a valid name. </exception>
		// Token: 0x06001D76 RID: 7542 RVA: 0x000749B0 File Offset: 0x00072BB0
		public X509Store(StoreName storeName, StoreLocation storeLocation)
		{
			if (storeName < StoreName.AddressBook || storeName > StoreName.TrustedPublisher)
			{
				throw new ArgumentException("storeName");
			}
			if (storeLocation < StoreLocation.CurrentUser || storeLocation > StoreLocation.LocalMachine)
			{
				throw new ArgumentException("storeLocation");
			}
			if (storeName == StoreName.CertificateAuthority)
			{
				this._name = "CA";
			}
			else
			{
				this._name = storeName.ToString();
			}
			this._location = storeLocation;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using an Intptr handle to an HCERTSTORE store.</summary>
		/// <param name="storeHandle">A handle to an HCERTSTORE store.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="storeHandle" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="storeHandle" /> parameter points to an invalid context.</exception>
		// Token: 0x06001D77 RID: 7543 RVA: 0x00074A14 File Offset: 0x00072C14
		[MonoTODO("Mono's stores are fully managed. All handles are invalid.")]
		[SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
		public X509Store(IntPtr storeHandle)
		{
			if (storeHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("storeHandle");
			}
			throw new CryptographicException("Invalid handle.");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Store" /> class using a string that represents a value from the <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> enumeration and a value from the <see cref="T:System.Security.Cryptography.X509Certificates.StoreLocation" /> enumeration.</summary>
		/// <param name="storeName">A string that represents a value from the <see cref="T:System.Security.Cryptography.X509Certificates.StoreName" /> enumeration. </param>
		/// <param name="storeLocation">One of the enumeration values that specifies the location of the X.509 certificate store. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="storeLocation" /> contains invalid values. </exception>
		// Token: 0x06001D78 RID: 7544 RVA: 0x00074A3E File Offset: 0x00072C3E
		public X509Store(string storeName, StoreLocation storeLocation)
		{
			if (storeLocation < StoreLocation.CurrentUser || storeLocation > StoreLocation.LocalMachine)
			{
				throw new ArgumentException("storeLocation");
			}
			this._name = storeName;
			this._location = storeLocation;
		}

		/// <summary>Returns a collection of certificates located in an X.509 certificate store.</summary>
		/// <returns>A collection of certificates.</returns>
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001D79 RID: 7545 RVA: 0x00074A67 File Offset: 0x00072C67
		public X509Certificate2Collection Certificates
		{
			get
			{
				if (this.list == null)
				{
					this.list = new X509Certificate2Collection();
				}
				else if (this.store == null)
				{
					this.list.Clear();
				}
				return this.list;
			}
		}

		/// <summary>Gets the location of the X.509 certificate store.</summary>
		/// <returns>The location of the certificate store.</returns>
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001D7A RID: 7546 RVA: 0x00074A97 File Offset: 0x00072C97
		public StoreLocation Location
		{
			get
			{
				return this._location;
			}
		}

		/// <summary>Gets the name of the X.509 certificate store.</summary>
		/// <returns>The name of the certificate store.</returns>
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001D7B RID: 7547 RVA: 0x00074A9F File Offset: 0x00072C9F
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001D7C RID: 7548 RVA: 0x00074AA7 File Offset: 0x00072CA7
		private Mono.Security.X509.X509Stores Factory
		{
			get
			{
				if (this._location == StoreLocation.CurrentUser)
				{
					return Mono.Security.X509.X509StoreManager.CurrentUser;
				}
				return Mono.Security.X509.X509StoreManager.LocalMachine;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001D7D RID: 7549 RVA: 0x00074ABD File Offset: 0x00072CBD
		private bool IsOpen
		{
			get
			{
				return this.store != null;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001D7E RID: 7550 RVA: 0x00074AC8 File Offset: 0x00072CC8
		private bool IsReadOnly
		{
			get
			{
				return (this._flags & OpenFlags.ReadWrite) == OpenFlags.ReadOnly;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001D7F RID: 7551 RVA: 0x00074AD5 File Offset: 0x00072CD5
		internal Mono.Security.X509.X509Store Store
		{
			get
			{
				return this.store;
			}
		}

		/// <summary>Gets an <see cref="T:System.IntPtr" /> handle to an HCERTSTORE store.  </summary>
		/// <returns>A handle to an HCERTSTORE store.</returns>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The store is not open. </exception>
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001D80 RID: 7552 RVA: 0x00070DAB File Offset: 0x0006EFAB
		[MonoTODO("Mono's stores are fully managed. Always returns IntPtr.Zero.")]
		public IntPtr StoreHandle
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		/// <summary>Adds a certificate to an X.509 certificate store.</summary>
		/// <param name="certificate">The certificate to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate could not be added to the store.</exception>
		// Token: 0x06001D81 RID: 7553 RVA: 0x00074AE0 File Offset: 0x00072CE0
		public void Add(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!this.IsOpen)
			{
				throw new CryptographicException(global::Locale.GetText("Store isn't opened."));
			}
			if (this.IsReadOnly)
			{
				throw new CryptographicException(global::Locale.GetText("Store is read-only."));
			}
			if (!this.Exists(certificate))
			{
				try
				{
					this.store.Import(new Mono.Security.X509.X509Certificate(certificate.RawData));
				}
				finally
				{
					this.Certificates.Add(certificate);
				}
			}
		}

		/// <summary>Adds a collection of certificates to an X.509 certificate store.</summary>
		/// <param name="certificates">The collection of certificates to add. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06001D82 RID: 7554 RVA: 0x00074B6C File Offset: 0x00072D6C
		[MonoTODO("Method isn't transactional (like documented)")]
		public void AddRange(X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			if (certificates.Count == 0)
			{
				return;
			}
			if (!this.IsOpen)
			{
				throw new CryptographicException(global::Locale.GetText("Store isn't opened."));
			}
			if (this.IsReadOnly)
			{
				throw new CryptographicException(global::Locale.GetText("Store is read-only."));
			}
			foreach (X509Certificate2 x509Certificate in certificates)
			{
				if (!this.Exists(x509Certificate))
				{
					try
					{
						this.store.Import(new Mono.Security.X509.X509Certificate(x509Certificate.RawData));
					}
					finally
					{
						this.Certificates.Add(x509Certificate);
					}
				}
			}
		}

		/// <summary>Closes an X.509 certificate store.</summary>
		// Token: 0x06001D83 RID: 7555 RVA: 0x00074C18 File Offset: 0x00072E18
		public void Close()
		{
			this.store = null;
			if (this.list != null)
			{
				this.list.Clear();
			}
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x00074C34 File Offset: 0x00072E34
		public void Dispose()
		{
			this.Close();
		}

		/// <summary>Opens an X.509 certificate store or creates a new store, depending on <see cref="T:System.Security.Cryptography.X509Certificates.OpenFlags" /> flag settings.</summary>
		/// <param name="flags">A bitwise combination of enumeration values that specifies the way to open the X.509 certificate store. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The store is unreadable. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ArgumentException">The store contains invalid values.</exception>
		// Token: 0x06001D85 RID: 7557 RVA: 0x00074C3C File Offset: 0x00072E3C
		public void Open(OpenFlags flags)
		{
			if (string.IsNullOrEmpty(this._name))
			{
				throw new CryptographicException(global::Locale.GetText("Invalid store name (null or empty)."));
			}
			string name = this._name;
			string text;
			if (name == "Root")
			{
				text = "Trust";
			}
			else
			{
				text = this._name;
			}
			bool flag = (flags & OpenFlags.OpenExistingOnly) != OpenFlags.OpenExistingOnly;
			this.store = this.Factory.Open(text, flag);
			if (this.store == null)
			{
				throw new CryptographicException(global::Locale.GetText("Store {0} doesn't exists.", new object[] { this._name }));
			}
			this._flags = flags;
			foreach (Mono.Security.X509.X509Certificate x509Certificate in this.store.Certificates)
			{
				X509Certificate2 x509Certificate2 = new X509Certificate2(x509Certificate.RawData);
				x509Certificate2.PrivateKey = x509Certificate.RSA;
				this.Certificates.Add(x509Certificate2);
			}
		}

		/// <summary>Removes a certificate from an X.509 certificate store.</summary>
		/// <param name="certificate">The certificate to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06001D86 RID: 7558 RVA: 0x00074D48 File Offset: 0x00072F48
		public void Remove(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (!this.IsOpen)
			{
				throw new CryptographicException(global::Locale.GetText("Store isn't opened."));
			}
			if (!this.Exists(certificate))
			{
				return;
			}
			if (this.IsReadOnly)
			{
				throw new CryptographicException(global::Locale.GetText("Store is read-only."));
			}
			try
			{
				this.store.Remove(new Mono.Security.X509.X509Certificate(certificate.RawData));
			}
			finally
			{
				this.Certificates.Remove(certificate);
			}
		}

		/// <summary>Removes a range of certificates from an X.509 certificate store.</summary>
		/// <param name="certificates">A range of certificates to remove.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06001D87 RID: 7559 RVA: 0x00074DD4 File Offset: 0x00072FD4
		[MonoTODO("Method isn't transactional (like documented)")]
		public void RemoveRange(X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			if (certificates.Count == 0)
			{
				return;
			}
			if (!this.IsOpen)
			{
				throw new CryptographicException(global::Locale.GetText("Store isn't opened."));
			}
			bool flag = false;
			foreach (X509Certificate2 x509Certificate in certificates)
			{
				if (this.Exists(x509Certificate))
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return;
			}
			if (this.IsReadOnly)
			{
				throw new CryptographicException(global::Locale.GetText("Store is read-only."));
			}
			try
			{
				foreach (X509Certificate2 x509Certificate2 in certificates)
				{
					this.store.Remove(new Mono.Security.X509.X509Certificate(x509Certificate2.RawData));
				}
			}
			finally
			{
				this.Certificates.RemoveRange(certificates);
			}
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x00074EA0 File Offset: 0x000730A0
		private bool Exists(X509Certificate2 certificate)
		{
			if (this.store == null || this.list == null || certificate == null)
			{
				return false;
			}
			foreach (X509Certificate2 x509Certificate in this.list)
			{
				if (certificate.Equals(x509Certificate))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x040019D1 RID: 6609
		private string _name;

		// Token: 0x040019D2 RID: 6610
		private StoreLocation _location;

		// Token: 0x040019D3 RID: 6611
		private X509Certificate2Collection list;

		// Token: 0x040019D4 RID: 6612
		private OpenFlags _flags;

		// Token: 0x040019D5 RID: 6613
		private Mono.Security.X509.X509Store store;
	}
}
