using System;
using Mono.Security;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> class represents the CMS/PKCS #7 ContentInfo data structure as defined in the CMS/PKCS #7 standards document. This data structure is the basis for all CMS/PKCS #7 messages.</summary>
	// Token: 0x02000021 RID: 33
	public sealed class ContentInfo
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.#ctor(System.Byte[])" /> constructor  creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> class by using an array of byte values as the data and a default <paramref name="object identifier" /> (OID) that represents the content type.</summary>
		/// <param name="content">An array of byte values that represents the data from which to create the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument. </exception>
		// Token: 0x0600008F RID: 143 RVA: 0x00003729 File Offset: 0x00001929
		public ContentInfo(byte[] content)
			: this(new Oid("1.2.840.113549.1.7.1"), content)
		{
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.#ctor(System.Security.Cryptography.Oid,System.Byte[])" />  constructor  creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> class by using the specified content type and an array of byte values as the data.</summary>
		/// <param name="contentType">An <see cref="T:System.Security.Cryptography.Oid" /> object that contains an <paramref name="object identifier" /> (OID) that specifies the content type of the content. This can be data, digestedData, encryptedData, envelopedData, hashedData, signedAndEnvelopedData, or signedData.  For more information, see  Remarks.</param>
		/// <param name="content">An array of byte values that represents the data from which to create the <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument. </exception>
		// Token: 0x06000090 RID: 144 RVA: 0x0000373C File Offset: 0x0000193C
		public ContentInfo(Oid contentType, byte[] content)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			this._oid = contentType;
			this._content = content;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003770 File Offset: 0x00001970
		~ContentInfo()
		{
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.ContentInfo.Content" /> property  retrieves the content of the CMS/PKCS #7 message.</summary>
		/// <returns>An array of byte values that represents the content data.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003798 File Offset: 0x00001998
		public byte[] Content
		{
			get
			{
				return (byte[])this._content.Clone();
			}
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.ContentInfo.ContentType" /> property  retrieves the <see cref="T:System.Security.Cryptography.Oid" />   object that contains the <paramref name="object identifier" /> (OID)  of the content type of the inner content of the CMS/PKCS #7 message.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.Oid" />  object that contains the OID value that represents the content type.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000037AA File Offset: 0x000019AA
		public Oid ContentType
		{
			get
			{
				return this._oid;
			}
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.ContentInfo.GetContentType(System.Byte[])" /> static method  retrieves the outer content type of the encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> message represented by an array of byte values.</summary>
		/// <returns>If the method succeeds, the method returns an <see cref="T:System.Security.Cryptography.Oid" /> object that contains the outer content type of the specified encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> message.If the method fails, it throws an exception.</returns>
		/// <param name="encodedMessage">An array of byte values that represents the encoded <see cref="T:System.Security.Cryptography.Pkcs.ContentInfo" /> message from which to retrieve the outer content type.</param>
		/// <exception cref="T:System.ArgumentNullException">A null reference  was passed to a method that does not accept it as a valid argument.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">An error occurred during a cryptographic operation.</exception>
		// Token: 0x06000094 RID: 148 RVA: 0x000037B4 File Offset: 0x000019B4
		[MonoTODO("MS is stricter than us about the content structure")]
		public static Oid GetContentType(byte[] encodedMessage)
		{
			if (encodedMessage == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			Oid oid;
			try
			{
				PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(encodedMessage);
				string contentType = contentInfo.ContentType;
				if (!(contentType == "1.2.840.113549.1.7.1") && !(contentType == "1.2.840.113549.1.7.2") && !(contentType == "1.2.840.113549.1.7.3") && !(contentType == "1.2.840.113549.1.7.5") && !(contentType == "1.2.840.113549.1.7.6"))
				{
					throw new CryptographicException(string.Format(Locale.GetText("Bad ASN1 - invalid OID '{0}'"), contentInfo.ContentType));
				}
				oid = new Oid(contentInfo.ContentType);
			}
			catch (Exception ex)
			{
				throw new CryptographicException(Locale.GetText("Bad ASN1 - invalid structure"), ex);
			}
			return oid;
		}

		// Token: 0x040000BE RID: 190
		private Oid _oid;

		// Token: 0x040000BF RID: 191
		private byte[] _content;
	}
}
