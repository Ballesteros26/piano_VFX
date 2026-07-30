using System;
using Mono.Security;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest" /> class defines the message digest of a CMS/PKCS #7 message.</summary>
	// Token: 0x0200002A RID: 42
	public sealed class Pkcs9MessageDigest : Pkcs9AttributeObject
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest" /> class.</summary>
		// Token: 0x060000D4 RID: 212 RVA: 0x00003FF5 File Offset: 0x000021F5
		public Pkcs9MessageDigest()
		{
			base.Oid = new Oid("1.2.840.113549.1.9.4", "Message Digest");
			this._encoded = null;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x0000401C File Offset: 0x0000221C
		internal Pkcs9MessageDigest(byte[] messageDigest, bool encoded)
		{
			if (messageDigest == null)
			{
				throw new ArgumentNullException("messageDigest");
			}
			if (encoded)
			{
				base.Oid = new Oid("1.2.840.113549.1.9.4", "Message Digest");
				base.RawData = messageDigest;
				this.Decode(messageDigest);
				return;
			}
			base.Oid = new Oid("1.2.840.113549.1.9.4", "Message Digest");
			this._messageDigest = (byte[])this._messageDigest.Clone();
			base.RawData = this.Encode();
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9MessageDigest.MessageDigest" /> property retrieves the message digest.</summary>
		/// <returns>An array of byte values that contains the message digest.</returns>
		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x0000409B File Offset: 0x0000229B
		public byte[] MessageDigest
		{
			get
			{
				if (this._encoded != null)
				{
					this.Decode(this._encoded);
				}
				return this._messageDigest;
			}
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		// Token: 0x060000D7 RID: 215 RVA: 0x000040B7 File Offset: 0x000022B7
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this._encoded = asnEncodedData.RawData;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000040CC File Offset: 0x000022CC
		internal void Decode(byte[] attribute)
		{
			if (attribute == null || attribute[0] != 4)
			{
				throw new CryptographicException(Locale.GetText("Expected an OCTETSTRING."));
			}
			ASN1 asn = new ASN1(attribute);
			this._messageDigest = asn.Value;
			this._encoded = null;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000410C File Offset: 0x0000230C
		internal byte[] Encode()
		{
			return new ASN1(4, this._messageDigest).GetBytes();
		}

		// Token: 0x040000D9 RID: 217
		internal const string oid = "1.2.840.113549.1.9.4";

		// Token: 0x040000DA RID: 218
		internal const string friendlyName = "Message Digest";

		// Token: 0x040000DB RID: 219
		private byte[] _messageDigest;

		// Token: 0x040000DC RID: 220
		private byte[] _encoded;
	}
}
