using System;
using System.Collections;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200009B RID: 155
	public sealed class PKCS8
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x0001ACF5 File Offset: 0x00018EF5
		private PKCS8()
		{
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001AD00 File Offset: 0x00018F00
		public static PKCS8.KeyInfo GetType(byte[] data)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			PKCS8.KeyInfo keyInfo = PKCS8.KeyInfo.Unknown;
			try
			{
				ASN1 asn = new ASN1(data);
				if (asn.Tag == 48 && asn.Count > 0)
				{
					byte tag = asn[0].Tag;
					if (tag != 2)
					{
						if (tag == 48)
						{
							keyInfo = PKCS8.KeyInfo.EncryptedPrivateKey;
						}
					}
					else
					{
						keyInfo = PKCS8.KeyInfo.PrivateKey;
					}
				}
			}
			catch
			{
				throw new CryptographicException("invalid ASN.1 data");
			}
			return keyInfo;
		}

		// Token: 0x020000E4 RID: 228
		public enum KeyInfo
		{
			// Token: 0x04000516 RID: 1302
			PrivateKey,
			// Token: 0x04000517 RID: 1303
			EncryptedPrivateKey,
			// Token: 0x04000518 RID: 1304
			Unknown
		}

		// Token: 0x020000E5 RID: 229
		public class PrivateKeyInfo
		{
			// Token: 0x060007B6 RID: 1974 RVA: 0x00022352 File Offset: 0x00020552
			public PrivateKeyInfo()
			{
				this._version = 0;
				this._list = new ArrayList();
			}

			// Token: 0x060007B7 RID: 1975 RVA: 0x0002236C File Offset: 0x0002056C
			public PrivateKeyInfo(byte[] data)
				: this()
			{
				this.Decode(data);
			}

			// Token: 0x170001FB RID: 507
			// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0002237B File Offset: 0x0002057B
			// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00022383 File Offset: 0x00020583
			public string Algorithm
			{
				get
				{
					return this._algorithm;
				}
				set
				{
					this._algorithm = value;
				}
			}

			// Token: 0x170001FC RID: 508
			// (get) Token: 0x060007BA RID: 1978 RVA: 0x0002238C File Offset: 0x0002058C
			public ArrayList Attributes
			{
				get
				{
					return this._list;
				}
			}

			// Token: 0x170001FD RID: 509
			// (get) Token: 0x060007BB RID: 1979 RVA: 0x00022394 File Offset: 0x00020594
			// (set) Token: 0x060007BC RID: 1980 RVA: 0x000223B0 File Offset: 0x000205B0
			public byte[] PrivateKey
			{
				get
				{
					if (this._key == null)
					{
						return null;
					}
					return (byte[])this._key.Clone();
				}
				set
				{
					if (value == null)
					{
						throw new ArgumentNullException("PrivateKey");
					}
					this._key = (byte[])value.Clone();
				}
			}

			// Token: 0x170001FE RID: 510
			// (get) Token: 0x060007BD RID: 1981 RVA: 0x000223D1 File Offset: 0x000205D1
			// (set) Token: 0x060007BE RID: 1982 RVA: 0x000223D9 File Offset: 0x000205D9
			public int Version
			{
				get
				{
					return this._version;
				}
				set
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("negative version");
					}
					this._version = value;
				}
			}

			// Token: 0x060007BF RID: 1983 RVA: 0x000223F4 File Offset: 0x000205F4
			private void Decode(byte[] data)
			{
				ASN1 asn = new ASN1(data);
				if (asn.Tag != 48)
				{
					throw new CryptographicException("invalid PrivateKeyInfo");
				}
				ASN1 asn2 = asn[0];
				if (asn2.Tag != 2)
				{
					throw new CryptographicException("invalid version");
				}
				this._version = (int)asn2.Value[0];
				ASN1 asn3 = asn[1];
				if (asn3.Tag != 48)
				{
					throw new CryptographicException("invalid algorithm");
				}
				ASN1 asn4 = asn3[0];
				if (asn4.Tag != 6)
				{
					throw new CryptographicException("missing algorithm OID");
				}
				this._algorithm = ASN1Convert.ToOid(asn4);
				ASN1 asn5 = asn[2];
				this._key = asn5.Value;
				if (asn.Count > 3)
				{
					ASN1 asn6 = asn[3];
					for (int i = 0; i < asn6.Count; i++)
					{
						this._list.Add(asn6[i]);
					}
				}
			}

			// Token: 0x060007C0 RID: 1984 RVA: 0x000224DC File Offset: 0x000206DC
			public byte[] GetBytes()
			{
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this._algorithm));
				asn.Add(new ASN1(5));
				ASN1 asn2 = new ASN1(48);
				asn2.Add(new ASN1(2, new byte[] { (byte)this._version }));
				asn2.Add(asn);
				asn2.Add(new ASN1(4, this._key));
				if (this._list.Count > 0)
				{
					ASN1 asn3 = new ASN1(160);
					foreach (object obj in this._list)
					{
						ASN1 asn4 = (ASN1)obj;
						asn3.Add(asn4);
					}
					asn2.Add(asn3);
				}
				return asn2.GetBytes();
			}

			// Token: 0x060007C1 RID: 1985 RVA: 0x000225CC File Offset: 0x000207CC
			private static byte[] RemoveLeadingZero(byte[] bigInt)
			{
				int num = 0;
				int num2 = bigInt.Length;
				if (bigInt[0] == 0)
				{
					num = 1;
					num2--;
				}
				byte[] array = new byte[num2];
				Buffer.BlockCopy(bigInt, num, array, 0, num2);
				return array;
			}

			// Token: 0x060007C2 RID: 1986 RVA: 0x000225FC File Offset: 0x000207FC
			private static byte[] Normalize(byte[] bigInt, int length)
			{
				if (bigInt.Length == length)
				{
					return bigInt;
				}
				if (bigInt.Length > length)
				{
					return PKCS8.PrivateKeyInfo.RemoveLeadingZero(bigInt);
				}
				byte[] array = new byte[length];
				Buffer.BlockCopy(bigInt, 0, array, length - bigInt.Length, bigInt.Length);
				return array;
			}

			// Token: 0x060007C3 RID: 1987 RVA: 0x00022638 File Offset: 0x00020838
			public static RSA DecodeRSA(byte[] keypair)
			{
				ASN1 asn = new ASN1(keypair);
				if (asn.Tag != 48)
				{
					throw new CryptographicException("invalid private key format");
				}
				if (asn[0].Tag != 2)
				{
					throw new CryptographicException("missing version");
				}
				if (asn.Count < 9)
				{
					throw new CryptographicException("not enough key parameters");
				}
				RSAParameters rsaparameters = new RSAParameters
				{
					Modulus = PKCS8.PrivateKeyInfo.RemoveLeadingZero(asn[1].Value)
				};
				int num = rsaparameters.Modulus.Length;
				int num2 = num >> 1;
				rsaparameters.D = PKCS8.PrivateKeyInfo.Normalize(asn[3].Value, num);
				rsaparameters.DP = PKCS8.PrivateKeyInfo.Normalize(asn[6].Value, num2);
				rsaparameters.DQ = PKCS8.PrivateKeyInfo.Normalize(asn[7].Value, num2);
				rsaparameters.Exponent = PKCS8.PrivateKeyInfo.RemoveLeadingZero(asn[2].Value);
				rsaparameters.InverseQ = PKCS8.PrivateKeyInfo.Normalize(asn[8].Value, num2);
				rsaparameters.P = PKCS8.PrivateKeyInfo.Normalize(asn[4].Value, num2);
				rsaparameters.Q = PKCS8.PrivateKeyInfo.Normalize(asn[5].Value, num2);
				RSA rsa = null;
				try
				{
					rsa = RSA.Create();
					rsa.ImportParameters(rsaparameters);
				}
				catch (CryptographicException)
				{
					rsa = new RSACryptoServiceProvider(new CspParameters
					{
						Flags = CspProviderFlags.UseMachineKeyStore
					});
					rsa.ImportParameters(rsaparameters);
				}
				return rsa;
			}

			// Token: 0x060007C4 RID: 1988 RVA: 0x000227B0 File Offset: 0x000209B0
			public static byte[] Encode(RSA rsa)
			{
				RSAParameters rsaparameters = rsa.ExportParameters(true);
				ASN1 asn = new ASN1(48);
				asn.Add(new ASN1(2, new byte[1]));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Modulus));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Exponent));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.D));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.P));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.Q));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.DP));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.DQ));
				asn.Add(ASN1Convert.FromUnsignedBigInteger(rsaparameters.InverseQ));
				return asn.GetBytes();
			}

			// Token: 0x060007C5 RID: 1989 RVA: 0x00022878 File Offset: 0x00020A78
			public static DSA DecodeDSA(byte[] privateKey, DSAParameters dsaParameters)
			{
				ASN1 asn = new ASN1(privateKey);
				if (asn.Tag != 2)
				{
					throw new CryptographicException("invalid private key format");
				}
				dsaParameters.X = PKCS8.PrivateKeyInfo.Normalize(asn.Value, 20);
				DSA dsa = DSA.Create();
				dsa.ImportParameters(dsaParameters);
				return dsa;
			}

			// Token: 0x060007C6 RID: 1990 RVA: 0x000228C0 File Offset: 0x00020AC0
			public static byte[] Encode(DSA dsa)
			{
				return ASN1Convert.FromUnsignedBigInteger(dsa.ExportParameters(true).X).GetBytes();
			}

			// Token: 0x060007C7 RID: 1991 RVA: 0x000228D8 File Offset: 0x00020AD8
			public static byte[] Encode(AsymmetricAlgorithm aa)
			{
				if (aa is RSA)
				{
					return PKCS8.PrivateKeyInfo.Encode((RSA)aa);
				}
				if (aa is DSA)
				{
					return PKCS8.PrivateKeyInfo.Encode((DSA)aa);
				}
				throw new CryptographicException("Unknown asymmetric algorithm {0}", aa.ToString());
			}

			// Token: 0x04000519 RID: 1305
			private int _version;

			// Token: 0x0400051A RID: 1306
			private string _algorithm;

			// Token: 0x0400051B RID: 1307
			private byte[] _key;

			// Token: 0x0400051C RID: 1308
			private ArrayList _list;
		}

		// Token: 0x020000E6 RID: 230
		public class EncryptedPrivateKeyInfo
		{
			// Token: 0x060007C8 RID: 1992 RVA: 0x00022912 File Offset: 0x00020B12
			public EncryptedPrivateKeyInfo()
			{
			}

			// Token: 0x060007C9 RID: 1993 RVA: 0x0002291A File Offset: 0x00020B1A
			public EncryptedPrivateKeyInfo(byte[] data)
				: this()
			{
				this.Decode(data);
			}

			// Token: 0x170001FF RID: 511
			// (get) Token: 0x060007CA RID: 1994 RVA: 0x00022929 File Offset: 0x00020B29
			// (set) Token: 0x060007CB RID: 1995 RVA: 0x00022931 File Offset: 0x00020B31
			public string Algorithm
			{
				get
				{
					return this._algorithm;
				}
				set
				{
					this._algorithm = value;
				}
			}

			// Token: 0x17000200 RID: 512
			// (get) Token: 0x060007CC RID: 1996 RVA: 0x0002293A File Offset: 0x00020B3A
			// (set) Token: 0x060007CD RID: 1997 RVA: 0x00022956 File Offset: 0x00020B56
			public byte[] EncryptedData
			{
				get
				{
					if (this._data != null)
					{
						return (byte[])this._data.Clone();
					}
					return null;
				}
				set
				{
					this._data = ((value == null) ? null : ((byte[])value.Clone()));
				}
			}

			// Token: 0x17000201 RID: 513
			// (get) Token: 0x060007CE RID: 1998 RVA: 0x0002296F File Offset: 0x00020B6F
			// (set) Token: 0x060007CF RID: 1999 RVA: 0x000229A5 File Offset: 0x00020BA5
			public byte[] Salt
			{
				get
				{
					if (this._salt == null)
					{
						RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
						this._salt = new byte[8];
						randomNumberGenerator.GetBytes(this._salt);
					}
					return (byte[])this._salt.Clone();
				}
				set
				{
					this._salt = (byte[])value.Clone();
				}
			}

			// Token: 0x17000202 RID: 514
			// (get) Token: 0x060007D0 RID: 2000 RVA: 0x000229B8 File Offset: 0x00020BB8
			// (set) Token: 0x060007D1 RID: 2001 RVA: 0x000229C0 File Offset: 0x00020BC0
			public int IterationCount
			{
				get
				{
					return this._iterations;
				}
				set
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("IterationCount", "Negative");
					}
					this._iterations = value;
				}
			}

			// Token: 0x060007D2 RID: 2002 RVA: 0x000229E0 File Offset: 0x00020BE0
			private void Decode(byte[] data)
			{
				ASN1 asn = new ASN1(data);
				if (asn.Tag != 48)
				{
					throw new CryptographicException("invalid EncryptedPrivateKeyInfo");
				}
				ASN1 asn2 = asn[0];
				if (asn2.Tag != 48)
				{
					throw new CryptographicException("invalid encryptionAlgorithm");
				}
				ASN1 asn3 = asn2[0];
				if (asn3.Tag != 6)
				{
					throw new CryptographicException("invalid algorithm");
				}
				this._algorithm = ASN1Convert.ToOid(asn3);
				if (asn2.Count > 1)
				{
					ASN1 asn4 = asn2[1];
					if (asn4.Tag != 48)
					{
						throw new CryptographicException("invalid parameters");
					}
					ASN1 asn5 = asn4[0];
					if (asn5.Tag != 4)
					{
						throw new CryptographicException("invalid salt");
					}
					this._salt = asn5.Value;
					ASN1 asn6 = asn4[1];
					if (asn6.Tag != 2)
					{
						throw new CryptographicException("invalid iterationCount");
					}
					this._iterations = ASN1Convert.ToInt32(asn6);
				}
				ASN1 asn7 = asn[1];
				if (asn7.Tag != 4)
				{
					throw new CryptographicException("invalid EncryptedData");
				}
				this._data = asn7.Value;
			}

			// Token: 0x060007D3 RID: 2003 RVA: 0x00022AEC File Offset: 0x00020CEC
			public byte[] GetBytes()
			{
				if (this._algorithm == null)
				{
					throw new CryptographicException("No algorithm OID specified");
				}
				ASN1 asn = new ASN1(48);
				asn.Add(ASN1Convert.FromOid(this._algorithm));
				if (this._iterations > 0 || this._salt != null)
				{
					ASN1 asn2 = new ASN1(4, this._salt);
					ASN1 asn3 = ASN1Convert.FromInt32(this._iterations);
					ASN1 asn4 = new ASN1(48);
					asn4.Add(asn2);
					asn4.Add(asn3);
					asn.Add(asn4);
				}
				ASN1 asn5 = new ASN1(4, this._data);
				ASN1 asn6 = new ASN1(48);
				asn6.Add(asn);
				asn6.Add(asn5);
				return asn6.GetBytes();
			}

			// Token: 0x0400051D RID: 1309
			private string _algorithm;

			// Token: 0x0400051E RID: 1310
			private byte[] _salt;

			// Token: 0x0400051F RID: 1311
			private int _iterations;

			// Token: 0x04000520 RID: 1312
			private byte[] _data;
		}
	}
}
