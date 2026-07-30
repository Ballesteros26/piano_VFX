using System;
using System.Text;
using Mono.Security;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class defines the name of a CMS/PKCS #7 message.</summary>
	// Token: 0x02000029 RID: 41
	public sealed class Pkcs9DocumentName : Pkcs9AttributeObject
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class.</summary>
		// Token: 0x060000CD RID: 205 RVA: 0x00003EE5 File Offset: 0x000020E5
		public Pkcs9DocumentName()
		{
			base.Oid = new Oid("1.3.6.1.4.1.311.88.2.1", null);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor(System.String)" /> constructor creates an instance of the  <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class by using the specified name for the CMS/PKCS #7 message.</summary>
		/// <param name="documentName">A  <see cref="T:System.String" />   object that specifies the name for the CMS/PKCS #7 message.</param>
		// Token: 0x060000CE RID: 206 RVA: 0x00003EFE File Offset: 0x000020FE
		public Pkcs9DocumentName(string documentName)
		{
			if (documentName == null)
			{
				throw new ArgumentNullException("documentName");
			}
			base.Oid = new Oid("1.3.6.1.4.1.311.88.2.1", null);
			this._name = documentName;
			base.RawData = this.Encode();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.#ctor(System.Byte[])" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentName" /> class by using the specified array of byte values as the encoded name of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="encodedDocumentName">An array of byte values that specifies the encoded name of the CMS/PKCS #7 message.</param>
		// Token: 0x060000CF RID: 207 RVA: 0x00003F38 File Offset: 0x00002138
		public Pkcs9DocumentName(byte[] encodedDocumentName)
		{
			if (encodedDocumentName == null)
			{
				throw new ArgumentNullException("encodedDocumentName");
			}
			base.Oid = new Oid("1.3.6.1.4.1.311.88.2.1", null);
			base.RawData = encodedDocumentName;
			this.Decode(encodedDocumentName);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9DocumentName.DocumentName" /> property retrieves the document name.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the document name.</returns>
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003F6D File Offset: 0x0000216D
		public string DocumentName
		{
			get
			{
				return this._name;
			}
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		// Token: 0x060000D1 RID: 209 RVA: 0x00003F75 File Offset: 0x00002175
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.Decode(base.RawData);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003F8C File Offset: 0x0000218C
		internal void Decode(byte[] attribute)
		{
			if (attribute[0] != 4)
			{
				return;
			}
			byte[] value = new ASN1(attribute).Value;
			int num = value.Length;
			if (value[num - 2] == 0)
			{
				num -= 2;
			}
			this._name = Encoding.Unicode.GetString(value, 0, num);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003FCE File Offset: 0x000021CE
		internal byte[] Encode()
		{
			return new ASN1(4, Encoding.Unicode.GetBytes(this._name + "\0")).GetBytes();
		}

		// Token: 0x040000D6 RID: 214
		internal const string oid = "1.3.6.1.4.1.311.88.2.1";

		// Token: 0x040000D7 RID: 215
		internal const string friendlyName = null;

		// Token: 0x040000D8 RID: 216
		private string _name;
	}
}
