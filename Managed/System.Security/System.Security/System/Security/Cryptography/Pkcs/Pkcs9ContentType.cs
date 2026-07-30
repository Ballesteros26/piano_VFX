using System;
using Mono.Security;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9ContentType" /> class defines the type of the content of a CMS/PKCS #7 message.</summary>
	// Token: 0x02000027 RID: 39
	public sealed class Pkcs9ContentType : Pkcs9AttributeObject
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9ContentType.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9ContentType" /> class.</summary>
		// Token: 0x060000BF RID: 191 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public Pkcs9ContentType()
		{
			base.Oid = new Oid("1.2.840.113549.1.9.3", "Content Type");
			this._encoded = null;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003CC8 File Offset: 0x00001EC8
		internal Pkcs9ContentType(string contentType)
		{
			base.Oid = new Oid("1.2.840.113549.1.9.3", "Content Type");
			this._contentType = new Oid(contentType);
			base.RawData = this.Encode();
			this._encoded = null;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003D04 File Offset: 0x00001F04
		internal Pkcs9ContentType(byte[] encodedContentType)
		{
			if (encodedContentType == null)
			{
				throw new ArgumentNullException("encodedContentType");
			}
			base.Oid = new Oid("1.2.840.113549.1.9.3", "Content Type");
			base.RawData = encodedContentType;
			this.Decode(encodedContentType);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9ContentType.ContentType" /> property gets an <see cref="T:System.Security.Cryptography.Oid" /> object that contains the content type.</summary>
		/// <returns>An  <see cref="T:System.Security.Cryptography.Oid" /> object that contains the content type.</returns>
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003D3D File Offset: 0x00001F3D
		public Oid ContentType
		{
			get
			{
				if (this._encoded != null)
				{
					this.Decode(this._encoded);
				}
				return this._contentType;
			}
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		// Token: 0x060000C3 RID: 195 RVA: 0x00003D59 File Offset: 0x00001F59
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this._encoded = asnEncodedData.RawData;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003D70 File Offset: 0x00001F70
		internal void Decode(byte[] attribute)
		{
			if (attribute == null || attribute[0] != 6)
			{
				throw new CryptographicException(Locale.GetText("Expected an OID."));
			}
			ASN1 asn = new ASN1(attribute);
			this._contentType = new Oid(ASN1Convert.ToOid(asn));
			this._encoded = null;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003DB5 File Offset: 0x00001FB5
		internal byte[] Encode()
		{
			if (this._contentType == null)
			{
				return null;
			}
			return ASN1Convert.FromOid(this._contentType.Value).GetBytes();
		}

		// Token: 0x040000CF RID: 207
		internal const string oid = "1.2.840.113549.1.9.3";

		// Token: 0x040000D0 RID: 208
		internal const string friendlyName = "Content Type";

		// Token: 0x040000D1 RID: 209
		private Oid _contentType;

		// Token: 0x040000D2 RID: 210
		private byte[] _encoded;
	}
}
