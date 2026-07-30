using System;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	/// <summary>Specifies a key BLOB format for use with Microsoft Cryptography Next Generation (CNG) objects. </summary>
	// Token: 0x02000064 RID: 100
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	[Serializable]
	public sealed class CngKeyBlobFormat : IEquatable<CngKeyBlobFormat>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> class by using the specified format.</summary>
		/// <param name="format">The key BLOB format to initialize.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="format" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="format" /> parameter length is 0 (zero).</exception>
		// Token: 0x0600022F RID: 559 RVA: 0x0000583C File Offset: 0x00003A3C
		public CngKeyBlobFormat(string format)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			if (format.Length == 0)
			{
				throw new ArgumentException(global::SR.GetString("The key blob format '{0}' is invalid.", new object[] { format }), "format");
			}
			this.m_format = format;
		}

		/// <summary>Gets the name of the key BLOB format that the current <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object specifies.</summary>
		/// <returns>The embedded key BLOB format name.</returns>
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000588B File Offset: 0x00003A8B
		public string Format
		{
			get
			{
				return this.m_format;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> objects specify the same key BLOB format.</summary>
		/// <returns>true if the two objects specify the same key BLOB format; otherwise, false.</returns>
		/// <param name="left">An object that specifies a key BLOB format.</param>
		/// <param name="right">A second object, to be compared to the object identified by the <paramref name="left" /> parameter.</param>
		// Token: 0x06000231 RID: 561 RVA: 0x00005893 File Offset: 0x00003A93
		public static bool operator ==(CngKeyBlobFormat left, CngKeyBlobFormat right)
		{
			if (left == null)
			{
				return right == null;
			}
			return left.Equals(right);
		}

		/// <summary>Determines whether two <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> objects do not specify the same key BLOB format.</summary>
		/// <returns>true if the two objects do not specify the same key BLOB format; otherwise, false.</returns>
		/// <param name="left">An object that specifies a key BLOB format.</param>
		/// <param name="right">A second object, to be compared to the object identified by the <paramref name="left" /> parameter.</param>
		// Token: 0x06000232 RID: 562 RVA: 0x000058A4 File Offset: 0x00003AA4
		public static bool operator !=(CngKeyBlobFormat left, CngKeyBlobFormat right)
		{
			if (left == null)
			{
				return right != null;
			}
			return !left.Equals(right);
		}

		/// <summary>Compares the specified object to the current <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter is a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies the same key BLOB format as the current object; otherwise, false.</returns>
		/// <param name="obj">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object.</param>
		// Token: 0x06000233 RID: 563 RVA: 0x000058B8 File Offset: 0x00003AB8
		public override bool Equals(object obj)
		{
			return this.Equals(obj as CngKeyBlobFormat);
		}

		/// <summary>Compares the specified <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object to the current <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object.</summary>
		/// <returns>true if the <paramref name="other" /> parameter specifies the same key BLOB format as the current object; otherwise, false.</returns>
		/// <param name="other">An object to be compared to the current <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object.</param>
		// Token: 0x06000234 RID: 564 RVA: 0x000058C6 File Offset: 0x00003AC6
		public bool Equals(CngKeyBlobFormat other)
		{
			return other != null && this.m_format.Equals(other.Format);
		}

		/// <summary>Generates a hash value for the embedded key BLOB format in the current <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object.</summary>
		/// <returns>The hash value of the embedded key BLOB format. </returns>
		// Token: 0x06000235 RID: 565 RVA: 0x000058DE File Offset: 0x00003ADE
		public override int GetHashCode()
		{
			return this.m_format.GetHashCode();
		}

		/// <summary>Gets the name of the key BLOB format that the current <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object specifies.</summary>
		/// <returns>The embedded key BLOB format name.</returns>
		// Token: 0x06000236 RID: 566 RVA: 0x0000588B File Offset: 0x00003A8B
		public override string ToString()
		{
			return this.m_format;
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies a private key BLOB for an elliptic curve cryptography (ECC) key.</summary>
		/// <returns>An object that specifies an ECC private key BLOB.</returns>
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000237 RID: 567 RVA: 0x000058EB File Offset: 0x00003AEB
		public static CngKeyBlobFormat EccPrivateBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_eccPrivate == null)
				{
					CngKeyBlobFormat.s_eccPrivate = new CngKeyBlobFormat("ECCPRIVATEBLOB");
				}
				return CngKeyBlobFormat.s_eccPrivate;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies a public key BLOB for an elliptic curve cryptography (ECC) key.</summary>
		/// <returns>An object that specifies an ECC public key BLOB.</returns>
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00005914 File Offset: 0x00003B14
		public static CngKeyBlobFormat EccPublicBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_eccPublic == null)
				{
					CngKeyBlobFormat.s_eccPublic = new CngKeyBlobFormat("ECCPUBLICBLOB");
				}
				return CngKeyBlobFormat.s_eccPublic;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000593D File Offset: 0x00003B3D
		public static CngKeyBlobFormat EccFullPrivateBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_eccFullPrivate == null)
				{
					CngKeyBlobFormat.s_eccFullPrivate = new CngKeyBlobFormat("ECCFULLPRIVATEBLOB");
				}
				return CngKeyBlobFormat.s_eccFullPrivate;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00005966 File Offset: 0x00003B66
		public static CngKeyBlobFormat EccFullPublicBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_eccFullPublic == null)
				{
					CngKeyBlobFormat.s_eccFullPublic = new CngKeyBlobFormat("ECCFULLPUBLICBLOB");
				}
				return CngKeyBlobFormat.s_eccFullPublic;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies a generic private key BLOB.</summary>
		/// <returns>An object that specifies a generic private key BLOB.</returns>
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600023B RID: 571 RVA: 0x0000598F File Offset: 0x00003B8F
		public static CngKeyBlobFormat GenericPrivateBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_genericPrivate == null)
				{
					CngKeyBlobFormat.s_genericPrivate = new CngKeyBlobFormat("PRIVATEBLOB");
				}
				return CngKeyBlobFormat.s_genericPrivate;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies a generic public key BLOB.</summary>
		/// <returns>An object that specifies a generic public key BLOB.</returns>
		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600023C RID: 572 RVA: 0x000059B8 File Offset: 0x00003BB8
		public static CngKeyBlobFormat GenericPublicBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_genericPublic == null)
				{
					CngKeyBlobFormat.s_genericPublic = new CngKeyBlobFormat("PUBLICBLOB");
				}
				return CngKeyBlobFormat.s_genericPublic;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies an opaque transport key BLOB.</summary>
		/// <returns>An object that specifies an opaque transport key BLOB.</returns>
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000059E1 File Offset: 0x00003BE1
		public static CngKeyBlobFormat OpaqueTransportBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_opaqueTransport == null)
				{
					CngKeyBlobFormat.s_opaqueTransport = new CngKeyBlobFormat("OpaqueTransport");
				}
				return CngKeyBlobFormat.s_opaqueTransport;
			}
		}

		/// <summary>Gets a <see cref="T:System.Security.Cryptography.CngKeyBlobFormat" /> object that specifies a Private Key Information Syntax Standard (PKCS #8) key BLOB.</summary>
		/// <returns>An object that specifies a PKCS #8 private key BLOB.</returns>
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00005A0A File Offset: 0x00003C0A
		public static CngKeyBlobFormat Pkcs8PrivateBlob
		{
			get
			{
				if (CngKeyBlobFormat.s_pkcs8Private == null)
				{
					CngKeyBlobFormat.s_pkcs8Private = new CngKeyBlobFormat("PKCS8_PRIVATEKEY");
				}
				return CngKeyBlobFormat.s_pkcs8Private;
			}
		}

		// Token: 0x0400029F RID: 671
		private static volatile CngKeyBlobFormat s_eccPrivate;

		// Token: 0x040002A0 RID: 672
		private static volatile CngKeyBlobFormat s_eccPublic;

		// Token: 0x040002A1 RID: 673
		private static volatile CngKeyBlobFormat s_eccFullPrivate;

		// Token: 0x040002A2 RID: 674
		private static volatile CngKeyBlobFormat s_eccFullPublic;

		// Token: 0x040002A3 RID: 675
		private static volatile CngKeyBlobFormat s_genericPrivate;

		// Token: 0x040002A4 RID: 676
		private static volatile CngKeyBlobFormat s_genericPublic;

		// Token: 0x040002A5 RID: 677
		private static volatile CngKeyBlobFormat s_opaqueTransport;

		// Token: 0x040002A6 RID: 678
		private static volatile CngKeyBlobFormat s_pkcs8Private;

		// Token: 0x040002A7 RID: 679
		private string m_format;
	}
}
