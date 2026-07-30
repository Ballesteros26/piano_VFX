using System;
using System.Collections;
using System.Globalization;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects. This class cannot be inherited.</summary>
	// Token: 0x020003AA RID: 938
	public class X509Certificate2Collection : X509CertificateCollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> class without any <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> information.</summary>
		// Token: 0x06001C37 RID: 7223 RVA: 0x000704DF File Offset: 0x0006E6DF
		public X509Certificate2Collection()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> class using the specified certificate collection.</summary>
		/// <param name="certificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object. </param>
		// Token: 0x06001C38 RID: 7224 RVA: 0x000704E7 File Offset: 0x0006E6E7
		public X509Certificate2Collection(X509Certificate2Collection certificates)
		{
			this.AddRange(certificates);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> class using an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</summary>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to start the collection from.</param>
		// Token: 0x06001C39 RID: 7225 RVA: 0x000704F6 File Offset: 0x0006E6F6
		public X509Certificate2Collection(X509Certificate2 certificate)
		{
			this.Add(certificate);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> class using an array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects.</summary>
		/// <param name="certificates">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects. </param>
		// Token: 0x06001C3A RID: 7226 RVA: 0x00070506 File Offset: 0x0006E706
		public X509Certificate2Collection(X509Certificate2[] certificates)
		{
			this.AddRange(certificates);
		}

		/// <summary>Gets or sets the element at the specified index.</summary>
		/// <returns>The element at the specified index.</returns>
		/// <param name="index">The zero-based index of the element to get or set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is equal to or greater than the <see cref="P:System.Collections.CollectionBase.Count" /> property. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="index" /> is null. </exception>
		// Token: 0x170005BF RID: 1471
		public X509Certificate2 this[int index]
		{
			get
			{
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("negative index");
				}
				if (index >= base.InnerList.Count)
				{
					throw new ArgumentOutOfRangeException("index >= Count");
				}
				return (X509Certificate2)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		/// <summary>Adds an object to the end of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" />.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> index at which the <paramref name="certificate" /> has been added.</returns>
		/// <param name="certificate">An X.509 certificate represented as an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is null. </exception>
		// Token: 0x06001C3D RID: 7229 RVA: 0x00070550 File Offset: 0x0006E750
		public int Add(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			return base.InnerList.Add(certificate);
		}

		/// <summary>Adds multiple <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects in an array to the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificates">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is null. </exception>
		// Token: 0x06001C3E RID: 7230 RVA: 0x0007056C File Offset: 0x0006E76C
		[MonoTODO("Method isn't transactional (like documented)")]
		public void AddRange(X509Certificate2[] certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			for (int i = 0; i < certificates.Length; i++)
			{
				base.InnerList.Add(certificates[i]);
			}
		}

		/// <summary>Adds multiple <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects in an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object to another <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is null. </exception>
		// Token: 0x06001C3F RID: 7231 RVA: 0x000705A4 File Offset: 0x0006E7A4
		[MonoTODO("Method isn't transactional (like documented)")]
		public void AddRange(X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificates");
			}
			base.InnerList.AddRange(certificates);
		}

		/// <summary>Determines whether the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object contains a specific certificate.</summary>
		/// <returns>true if the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> contains the specified <paramref name="certificate" />; otherwise, false.</returns>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to locate in the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is null. </exception>
		// Token: 0x06001C40 RID: 7232 RVA: 0x000705C0 File Offset: 0x0006E7C0
		public bool Contains(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			using (IEnumerator enumerator = base.InnerList.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (((X509Certificate2)enumerator.Current).Equals(certificate))
					{
						return true;
					}
				}
			}
			return false;
		}

		/// <summary>Exports X.509 certificate information into a byte array.</summary>
		/// <returns>X.509 certificate information in a byte array.</returns>
		/// <param name="contentType">A supported <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> object. </param>
		// Token: 0x06001C41 RID: 7233 RVA: 0x00070630 File Offset: 0x0006E830
		[MonoTODO("only support X509ContentType.Cert")]
		public byte[] Export(X509ContentType contentType)
		{
			return this.Export(contentType, null);
		}

		/// <summary>Exports X.509 certificate information into a byte array using a password.</summary>
		/// <returns>X.509 certificate information in a byte array.</returns>
		/// <param name="contentType">A supported <see cref="T:System.Security.Cryptography.X509Certificates.X509ContentType" /> object. </param>
		/// <param name="password">A string used to protect the byte array. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The certificate is unreadable, the content is invalid or, in the case of a certificate requiring a password, the private key could not be exported because the password provided was incorrect. </exception>
		// Token: 0x06001C42 RID: 7234 RVA: 0x0007063C File Offset: 0x0006E83C
		[MonoTODO("only support X509ContentType.Cert")]
		public byte[] Export(X509ContentType contentType, string password)
		{
			switch (contentType)
			{
			case X509ContentType.Cert:
			case X509ContentType.SerializedCert:
			case X509ContentType.Pfx:
				if (base.Count > 0)
				{
					return this[base.Count - 1].Export(contentType, password);
				}
				break;
			case X509ContentType.SerializedStore:
			case X509ContentType.Pkcs7:
				break;
			default:
				throw new CryptographicException(global::Locale.GetText("Cannot export certificate(s) to the '{0}' format", new object[] { contentType }));
			}
			return null;
		}

		/// <summary>Searches an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object using the search criteria specified by the <see cref="T:System.Security.Cryptography.X509Certificates.X509FindType" /> enumeration and the <paramref name="findValue" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		/// <param name="findType">One of the <see cref="T:System.Security.Cryptography.X509Certificates.X509FindType" />  values. </param>
		/// <param name="findValue">The search criteria as an object. </param>
		/// <param name="validOnly">true to allow only valid certificates to be returned from the search; otherwise, false. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="findType" /> is invalid. </exception>
		// Token: 0x06001C43 RID: 7235 RVA: 0x000706A8 File Offset: 0x0006E8A8
		[MonoTODO("Does not support X509FindType.FindByTemplateName, FindByApplicationPolicy and FindByCertificatePolicy")]
		public X509Certificate2Collection Find(X509FindType findType, object findValue, bool validOnly)
		{
			if (findValue == null)
			{
				throw new ArgumentNullException("findValue");
			}
			string text = string.Empty;
			string text2 = string.Empty;
			X509KeyUsageFlags x509KeyUsageFlags = X509KeyUsageFlags.None;
			DateTime dateTime = DateTime.MinValue;
			switch (findType)
			{
			case X509FindType.FindByThumbprint:
			case X509FindType.FindBySubjectName:
			case X509FindType.FindBySubjectDistinguishedName:
			case X509FindType.FindByIssuerName:
			case X509FindType.FindByIssuerDistinguishedName:
			case X509FindType.FindBySerialNumber:
			case X509FindType.FindByTemplateName:
			case X509FindType.FindBySubjectKeyIdentifier:
				try
				{
					text = (string)findValue;
					goto IL_0190;
				}
				catch (Exception ex)
				{
					throw new CryptographicException(global::Locale.GetText("Invalid find value type '{0}', expected '{1}'.", new object[]
					{
						findValue.GetType(),
						"string"
					}), ex);
				}
				break;
			case X509FindType.FindByTimeValid:
			case X509FindType.FindByTimeNotYetValid:
			case X509FindType.FindByTimeExpired:
				goto IL_013C;
			case X509FindType.FindByApplicationPolicy:
			case X509FindType.FindByCertificatePolicy:
			case X509FindType.FindByExtension:
				break;
			case X509FindType.FindByKeyUsage:
				goto IL_0107;
			default:
				goto IL_0171;
			}
			try
			{
				text2 = (string)findValue;
			}
			catch (Exception ex2)
			{
				throw new CryptographicException(global::Locale.GetText("Invalid find value type '{0}', expected '{1}'.", new object[]
				{
					findValue.GetType(),
					"X509KeyUsageFlags"
				}), ex2);
			}
			try
			{
				CryptoConfig.EncodeOID(text2);
				goto IL_0190;
			}
			catch (CryptographicUnexpectedOperationException)
			{
				string text3 = global::Locale.GetText("Invalid OID value '{0}'.", new object[] { text2 });
				throw new ArgumentException("findValue", text3);
			}
			IL_0107:
			try
			{
				x509KeyUsageFlags = (X509KeyUsageFlags)findValue;
				goto IL_0190;
			}
			catch (Exception ex3)
			{
				throw new CryptographicException(global::Locale.GetText("Invalid find value type '{0}', expected '{1}'.", new object[]
				{
					findValue.GetType(),
					"X509KeyUsageFlags"
				}), ex3);
			}
			IL_013C:
			try
			{
				dateTime = (DateTime)findValue;
				goto IL_0190;
			}
			catch (Exception ex4)
			{
				throw new CryptographicException(global::Locale.GetText("Invalid find value type '{0}', expected '{1}'.", new object[]
				{
					findValue.GetType(),
					"X509DateTime"
				}), ex4);
			}
			IL_0171:
			throw new CryptographicException(global::Locale.GetText("Invalid find type '{0}'.", new object[] { findType }));
			IL_0190:
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			X509Certificate2Collection x509Certificate2Collection = new X509Certificate2Collection();
			foreach (object obj in base.InnerList)
			{
				X509Certificate2 x509Certificate = (X509Certificate2)obj;
				bool flag = false;
				switch (findType)
				{
				case X509FindType.FindByThumbprint:
					flag = string.Compare(text, x509Certificate.Thumbprint, true, invariantCulture) == 0 || string.Compare(text, x509Certificate.GetCertHashString(), true, invariantCulture) == 0;
					break;
				case X509FindType.FindBySubjectName:
					foreach (string text4 in x509Certificate.SubjectName.Format(true).Split(X509Certificate2Collection.newline_split, StringSplitOptions.RemoveEmptyEntries))
					{
						int num = text4.IndexOf('=');
						flag = text4.IndexOf(text, num, StringComparison.InvariantCultureIgnoreCase) >= 0;
						if (flag)
						{
							break;
						}
					}
					break;
				case X509FindType.FindBySubjectDistinguishedName:
					flag = string.Compare(text, x509Certificate.Subject, true, invariantCulture) == 0;
					break;
				case X509FindType.FindByIssuerName:
					flag = x509Certificate.GetNameInfo(X509NameType.SimpleName, true).IndexOf(text, StringComparison.InvariantCultureIgnoreCase) >= 0;
					break;
				case X509FindType.FindByIssuerDistinguishedName:
					flag = string.Compare(text, x509Certificate.Issuer, true, invariantCulture) == 0;
					break;
				case X509FindType.FindBySerialNumber:
					flag = string.Compare(text, x509Certificate.SerialNumber, true, invariantCulture) == 0;
					break;
				case X509FindType.FindByTimeValid:
					flag = dateTime >= x509Certificate.NotBefore && dateTime <= x509Certificate.NotAfter;
					break;
				case X509FindType.FindByTimeNotYetValid:
					flag = dateTime < x509Certificate.NotBefore;
					break;
				case X509FindType.FindByTimeExpired:
					flag = dateTime > x509Certificate.NotAfter;
					break;
				case X509FindType.FindByApplicationPolicy:
					flag = x509Certificate.Extensions.Count == 0;
					break;
				case X509FindType.FindByExtension:
					flag = x509Certificate.Extensions[text2] != null;
					break;
				case X509FindType.FindByKeyUsage:
				{
					X509KeyUsageExtension x509KeyUsageExtension = x509Certificate.Extensions["2.5.29.15"] as X509KeyUsageExtension;
					flag = x509KeyUsageExtension == null || (x509KeyUsageExtension.KeyUsages & x509KeyUsageFlags) == x509KeyUsageFlags;
					break;
				}
				case X509FindType.FindBySubjectKeyIdentifier:
				{
					X509SubjectKeyIdentifierExtension x509SubjectKeyIdentifierExtension = x509Certificate.Extensions["2.5.29.14"] as X509SubjectKeyIdentifierExtension;
					if (x509SubjectKeyIdentifierExtension != null)
					{
						flag = string.Compare(text, x509SubjectKeyIdentifierExtension.SubjectKeyIdentifier, true, invariantCulture) == 0;
					}
					break;
				}
				}
				if (flag)
				{
					if (validOnly)
					{
						try
						{
							if (x509Certificate.Verify())
							{
								x509Certificate2Collection.Add(x509Certificate);
							}
							continue;
						}
						catch
						{
							continue;
						}
					}
					x509Certificate2Collection.Add(x509Certificate);
				}
			}
			return x509Certificate2Collection;
		}

		/// <summary>Returns an enumerator that can iterate through a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Enumerator" /> object that can iterate through the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		// Token: 0x06001C44 RID: 7236 RVA: 0x00070B84 File Offset: 0x0006ED84
		public new X509Certificate2Enumerator GetEnumerator()
		{
			return new X509Certificate2Enumerator(this);
		}

		/// <summary>Imports a certificate in the form of a byte array into a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="rawData">A byte array containing data from an X.509 certificate. </param>
		// Token: 0x06001C45 RID: 7237 RVA: 0x00070B8C File Offset: 0x0006ED8C
		[MonoTODO("same limitations as X509Certificate2.Import")]
		public void Import(byte[] rawData)
		{
			X509Certificate2 x509Certificate = new X509Certificate2();
			x509Certificate.Import(rawData);
			this.Add(x509Certificate);
		}

		/// <summary>Imports a certificate, in the form of a byte array that requires a password to access the certificate, into a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="rawData">A byte array containing data from an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object. </param>
		/// <param name="password">The password required to access the certificate information. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control how and where the certificate is imported. </param>
		// Token: 0x06001C46 RID: 7238 RVA: 0x00070BB0 File Offset: 0x0006EDB0
		[MonoTODO("same limitations as X509Certificate2.Import")]
		public void Import(byte[] rawData, string password, X509KeyStorageFlags keyStorageFlags)
		{
			X509Certificate2 x509Certificate = new X509Certificate2();
			x509Certificate.Import(rawData, password, keyStorageFlags);
			this.Add(x509Certificate);
		}

		/// <summary>Imports a certificate file into a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="fileName">The name of the file containing the certificate information. </param>
		// Token: 0x06001C47 RID: 7239 RVA: 0x00070BD4 File Offset: 0x0006EDD4
		[MonoTODO("same limitations as X509Certificate2.Import")]
		public void Import(string fileName)
		{
			X509Certificate2 x509Certificate = new X509Certificate2();
			x509Certificate.Import(fileName);
			this.Add(x509Certificate);
		}

		/// <summary>Imports a certificate file that requires a password into a <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="fileName">The name of the file containing the certificate information. </param>
		/// <param name="password">The password required to access the certificate information. </param>
		/// <param name="keyStorageFlags">A bitwise combination of the enumeration values that control how and where the certificate is imported. </param>
		// Token: 0x06001C48 RID: 7240 RVA: 0x00070BF8 File Offset: 0x0006EDF8
		[MonoTODO("same limitations as X509Certificate2.Import")]
		public void Import(string fileName, string password, X509KeyStorageFlags keyStorageFlags)
		{
			X509Certificate2 x509Certificate = new X509Certificate2();
			x509Certificate.Import(fileName, password, keyStorageFlags);
			this.Add(x509Certificate);
		}

		/// <summary>Inserts an object into the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object at the specified index.</summary>
		/// <param name="index">The zero-based index at which to insert <paramref name="certificate" />. </param>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to insert. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or- <paramref name="index" /> is greater than the <see cref="P:System.Collections.CollectionBase.Count" /> property. </exception>
		/// <exception cref="T:System.NotSupportedException">The collection is read-only.-or- The collection has a fixed size. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is null. </exception>
		// Token: 0x06001C49 RID: 7241 RVA: 0x00070C1C File Offset: 0x0006EE1C
		public void Insert(int index, X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("negative index");
			}
			if (index >= base.InnerList.Count)
			{
				throw new ArgumentOutOfRangeException("index >= Count");
			}
			base.InnerList.Insert(index, certificate);
		}

		/// <summary>Removes the first occurrence of a certificate from the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificate">The <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object to be removed from the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificate" /> is null. </exception>
		// Token: 0x06001C4A RID: 7242 RVA: 0x00070C6C File Offset: 0x0006EE6C
		public void Remove(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				if (((X509Certificate)base.InnerList[i]).Equals(certificate))
				{
					base.InnerList.RemoveAt(i);
					return;
				}
			}
		}

		/// <summary>Removes multiple <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects in an array from an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificates">An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is null. </exception>
		// Token: 0x06001C4B RID: 7243 RVA: 0x00070CC4 File Offset: 0x0006EEC4
		[MonoTODO("Method isn't transactional (like documented)")]
		public void RemoveRange(X509Certificate2[] certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificate");
			}
			foreach (X509Certificate2 x509Certificate in certificates)
			{
				this.Remove(x509Certificate);
			}
		}

		/// <summary>Removes multiple <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> objects in an <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object from another <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</summary>
		/// <param name="certificates">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="certificates" /> is null. </exception>
		// Token: 0x06001C4C RID: 7244 RVA: 0x00070CFC File Offset: 0x0006EEFC
		[MonoTODO("Method isn't transactional (like documented)")]
		public void RemoveRange(X509Certificate2Collection certificates)
		{
			if (certificates == null)
			{
				throw new ArgumentNullException("certificate");
			}
			foreach (X509Certificate2 x509Certificate in certificates)
			{
				this.Remove(x509Certificate);
			}
		}

		// Token: 0x04001992 RID: 6546
		private static string[] newline_split = new string[] { Environment.NewLine };
	}
}
