using System;
using System.Globalization;
using System.Security.Cryptography;

namespace Mono.Security.X509
{
	// Token: 0x02000011 RID: 17
	public abstract class X509Builder
	{
		// Token: 0x060000AD RID: 173 RVA: 0x000081A2 File Offset: 0x000063A2
		protected X509Builder()
		{
			this.hashName = "SHA1";
		}

		// Token: 0x060000AE RID: 174
		protected abstract ASN1 ToBeSigned(string hashName);

		// Token: 0x060000AF RID: 175 RVA: 0x000081B8 File Offset: 0x000063B8
		protected string GetOid(string hashName)
		{
			string text = hashName.ToLower(CultureInfo.InvariantCulture);
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2393554675U)
			{
				if (num != 2070555668U)
				{
					if (num != 2376777056U)
					{
						if (num == 2393554675U)
						{
							if (text == "md5")
							{
								return "1.2.840.113549.1.1.4";
							}
						}
					}
					else if (text == "md4")
					{
						return "1.2.840.113549.1.1.3";
					}
				}
				else if (text == "sha1")
				{
					return "1.2.840.113549.1.1.5";
				}
			}
			else if (num <= 2631153146U)
			{
				if (num != 2477442770U)
				{
					if (num == 2631153146U)
					{
						if (text == "sha256")
						{
							return "1.2.840.113549.1.1.11";
						}
					}
				}
				else if (text == "md2")
				{
					return "1.2.840.113549.1.1.2";
				}
			}
			else if (num != 2694049387U)
			{
				if (num == 2700614742U)
				{
					if (text == "sha384")
					{
						return "1.2.840.113549.1.1.12";
					}
				}
			}
			else if (text == "sha512")
			{
				return "1.2.840.113549.1.1.13";
			}
			throw new NotSupportedException("Unknown hash algorithm " + hashName);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000082D5 File Offset: 0x000064D5
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000082DD File Offset: 0x000064DD
		public string Hash
		{
			get
			{
				return this.hashName;
			}
			set
			{
				if (this.hashName == null)
				{
					this.hashName = "SHA1";
					return;
				}
				this.hashName = value;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000082FC File Offset: 0x000064FC
		public virtual byte[] Sign(AsymmetricAlgorithm aa)
		{
			if (aa is RSA)
			{
				return this.Sign(aa as RSA);
			}
			if (aa is DSA)
			{
				return this.Sign(aa as DSA);
			}
			throw new NotSupportedException("Unknown Asymmetric Algorithm " + aa.ToString());
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00008348 File Offset: 0x00006548
		private byte[] Build(ASN1 tbs, string hashoid, byte[] signature)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(tbs);
			asn.Add(PKCS7.AlgorithmIdentifier(hashoid));
			byte[] array = new byte[signature.Length + 1];
			Buffer.BlockCopy(signature, 0, array, 1, signature.Length);
			asn.Add(new ASN1(3, array));
			return asn.GetBytes();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000839C File Offset: 0x0000659C
		public virtual byte[] Sign(RSA key)
		{
			string oid = this.GetOid(this.hashName);
			ASN1 asn = this.ToBeSigned(oid);
			byte[] array = HashAlgorithm.Create(this.hashName).ComputeHash(asn.GetBytes());
			RSAPKCS1SignatureFormatter rsapkcs1SignatureFormatter = new RSAPKCS1SignatureFormatter(key);
			rsapkcs1SignatureFormatter.SetHashAlgorithm(this.hashName);
			byte[] array2 = rsapkcs1SignatureFormatter.CreateSignature(array);
			return this.Build(asn, oid, array2);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000083F8 File Offset: 0x000065F8
		public virtual byte[] Sign(DSA key)
		{
			string text = "1.2.840.10040.4.3";
			ASN1 asn = this.ToBeSigned(text);
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create(this.hashName);
			if (!(hashAlgorithm is SHA1))
			{
				throw new NotSupportedException("Only SHA-1 is supported for DSA");
			}
			byte[] array = hashAlgorithm.ComputeHash(asn.GetBytes());
			DSASignatureFormatter dsasignatureFormatter = new DSASignatureFormatter(key);
			dsasignatureFormatter.SetHashAlgorithm(this.hashName);
			byte[] array2 = dsasignatureFormatter.CreateSignature(array);
			byte[] array3 = new byte[20];
			Buffer.BlockCopy(array2, 0, array3, 0, 20);
			byte[] array4 = new byte[20];
			Buffer.BlockCopy(array2, 20, array4, 0, 20);
			ASN1 asn2 = new ASN1(48);
			asn2.Add(new ASN1(2, array3));
			asn2.Add(new ASN1(2, array4));
			return this.Build(asn, text, asn2.GetBytes());
		}

		// Token: 0x04000080 RID: 128
		private const string defaultHash = "SHA1";

		// Token: 0x04000081 RID: 129
		private string hashName;
	}
}
