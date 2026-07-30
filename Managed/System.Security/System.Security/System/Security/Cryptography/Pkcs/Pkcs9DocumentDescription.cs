using System;
using System.Text;
using Mono.Security;

namespace System.Security.Cryptography.Pkcs
{
	/// <summary>The <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class defines the description of the content of a CMS/PKCS #7 message.</summary>
	// Token: 0x02000028 RID: 40
	public sealed class Pkcs9DocumentDescription : Pkcs9AttributeObject
	{
		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class.</summary>
		// Token: 0x060000C6 RID: 198 RVA: 0x00003DD6 File Offset: 0x00001FD6
		public Pkcs9DocumentDescription()
		{
			base.Oid = new Oid("1.3.6.1.4.1.311.88.2.2", null);
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor(System.String)" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class by using the specified description of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="documentDescription">An instance of the <see cref="T:System.String" />  class that specifies the description for the CMS/PKCS #7 message.</param>
		// Token: 0x060000C7 RID: 199 RVA: 0x00003DEF File Offset: 0x00001FEF
		public Pkcs9DocumentDescription(string documentDescription)
		{
			if (documentDescription == null)
			{
				throw new ArgumentNullException("documentName");
			}
			base.Oid = new Oid("1.3.6.1.4.1.311.88.2.2", null);
			this._desc = documentDescription;
			base.RawData = this.Encode();
		}

		/// <summary>The <see cref="M:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.#ctor(System.Byte[])" /> constructor creates an instance of the <see cref="T:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription" /> class by using the specified array of byte values as the encoded description of the content of a CMS/PKCS #7 message.</summary>
		/// <param name="encodedDocumentDescription">An array of byte values that specifies the encoded description of the CMS/PKCS #7 message.</param>
		// Token: 0x060000C8 RID: 200 RVA: 0x00003E29 File Offset: 0x00002029
		public Pkcs9DocumentDescription(byte[] encodedDocumentDescription)
		{
			if (encodedDocumentDescription == null)
			{
				throw new ArgumentNullException("encodedDocumentDescription");
			}
			base.Oid = new Oid("1.3.6.1.4.1.311.88.2.2", null);
			base.RawData = encodedDocumentDescription;
			this.Decode(encodedDocumentDescription);
		}

		/// <summary>The <see cref="P:System.Security.Cryptography.Pkcs.Pkcs9DocumentDescription.DocumentDescription" /> property retrieves the document description.</summary>
		/// <returns>A <see cref="T:System.String" /> object that contains the document description.</returns>
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003E5E File Offset: 0x0000205E
		public string DocumentDescription
		{
			get
			{
				return this._desc;
			}
		}

		/// <summary>Copies information from an <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object.</summary>
		/// <param name="asnEncodedData">The <see cref="T:System.Security.Cryptography.AsnEncodedData" /> object from which to copy information.</param>
		// Token: 0x060000CA RID: 202 RVA: 0x00003E66 File Offset: 0x00002066
		public override void CopyFrom(AsnEncodedData asnEncodedData)
		{
			base.CopyFrom(asnEncodedData);
			this.Decode(base.RawData);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00003E7C File Offset: 0x0000207C
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
			this._desc = Encoding.Unicode.GetString(value, 0, num);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003EBE File Offset: 0x000020BE
		internal byte[] Encode()
		{
			return new ASN1(4, Encoding.Unicode.GetBytes(this._desc + "\0")).GetBytes();
		}

		// Token: 0x040000D3 RID: 211
		internal const string oid = "1.3.6.1.4.1.311.88.2.2";

		// Token: 0x040000D4 RID: 212
		internal const string friendlyName = null;

		// Token: 0x040000D5 RID: 213
		private string _desc;
	}
}
