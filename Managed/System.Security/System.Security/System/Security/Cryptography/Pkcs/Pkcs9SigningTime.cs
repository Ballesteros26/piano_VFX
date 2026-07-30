using System;
using System.Globalization;
using System.Text;
using Mono.Security;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> class defines the signing date and time of a signature. A <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> object can  be used as an authenticated attribute of a <see cref="T:System.Security.Cryptography.Pkcs.CmsSigner" />  object when an authenticated date and time are to accompany a digital signature.</summary>
	// Token: 0x0200002B RID: 43
	public sealed class Pkcs9SigningTime : Pkcs9AttributeObject
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> class.</summary>
		// Token: 0x060000DA RID: 218 RVA: 0x0000411F File Offset: 0x0000231F
		public Pkcs9SigningTime()
		{
			base.Oid = new Oid("1.2.840.113549.1.9.5", "Signing Time");
			this._signingTime = DateTime.Now;
			base.RawData = this.Encode();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.#ctor(System.DateTime)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> class by using the specified signing date and time.</summary>
		/// <param name="signingTime">A <see cref="T:System.DateTime" />  structure that represents the signing date and time of the signature.</param>
		// Token: 0x060000DB RID: 219 RVA: 0x00004153 File Offset: 0x00002353
		public Pkcs9SigningTime(DateTime signingTime)
		{
			base.Oid = new Oid("1.2.840.113549.1.9.5", "Signing Time");
			this._signingTime = signingTime;
			base.RawData = this.Encode();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.#ctor(System.Byte[])" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9SigningTime" /> class by using the specified array of byte values as the encoded signing date and time of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="encodedSigningTime">An array of byte values that specifies the encoded signing date and time of the CMS/PKCS #7 message.</param>
		// Token: 0x060000DC RID: 220 RVA: 0x00004183 File Offset: 0x00002383
		public Pkcs9SigningTime(byte[] encodedSigningTime)
		{
			if (encodedSigningTime == null)
			{
				throw new ArgumentNullException("encodedSigningTime");
			}
			base.Oid = new Oid("1.2.840.113549.1.9.5", "Signing Time");
			base.RawData = encodedSigningTime;
			this.Decode(encodedSigningTime);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9SigningTime.SigningTime" /> property retrieves a <see cref="T:System.DateTime" /> structure that represents the date and time that the message was signed.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> structure that contains the date and time the document was signed.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000041BC File Offset: 0x000023BC
		public DateTime SigningTime
		{
			get
			{
				return this._signingTime;
			}
		}

		/// <summary>Copies information from a <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		// Token: 0x060000DE RID: 222 RVA: 0x000041C4 File Offset: 0x000023C4
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			if (asnEncodedData == null)
			{
				throw new ArgumentNullException("asnEncodedData");
			}
			this.Decode(asnEncodedData.RawData);
			base.Oid = asnEncodedData.Oid;
			base.RawData = asnEncodedData.RawData;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000041F8 File Offset: 0x000023F8
		internal void Decode(byte[] attribute)
		{
			if (attribute[0] != 23)
			{
				throw new CryptographicException(Locale.GetText("Only UTCTIME is supported."));
			}
			byte[] value = new ASN1(attribute).Value;
			string @string = Encoding.ASCII.GetString(value, 0, value.Length - 1);
			this._signingTime = DateTime.ParseExact(@string, "yyMMddHHmmss", null);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000424C File Offset: 0x0000244C
		internal byte[] Encode()
		{
			if (this._signingTime.Year <= 1600)
			{
				throw new ArgumentOutOfRangeException("<= 1600");
			}
			if (this._signingTime.Year < 1950 || this._signingTime.Year >= 2050)
			{
				throw new CryptographicException("[1950,2049]");
			}
			string text = this._signingTime.ToString("yyMMddHHmmss", CultureInfo.InvariantCulture) + "Z";
			return new ASN1(23, Encoding.ASCII.GetBytes(text)).GetBytes();
		}

		// Token: 0x040000DD RID: 221
		internal const string oid = "1.2.840.113549.1.9.5";

		// Token: 0x040000DE RID: 222
		internal const string friendlyName = "Signing Time";

		// Token: 0x040000DF RID: 223
		private DateTime _signingTime;
	}
}
