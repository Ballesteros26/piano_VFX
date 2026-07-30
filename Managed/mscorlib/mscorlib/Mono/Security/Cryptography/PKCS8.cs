using System;
using System.Collections;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000088 RID: 136
	internal sealed class PKCS8
	{
		// Token: 0x0600044D RID: 1101 RVA: 0x00002111 File Offset: 0x00000311
		private PKCS8()
		{
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00018DA8 File Offset: 0x00016FA8
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

		// Token: 0x02000089 RID: 137
		public enum KeyInfo
		{
			// Token: 0x0400057A RID: 1402
			PrivateKey,
			// Token: 0x0400057B RID: 1403
			EncryptedPrivateKey,
			// Token: 0x0400057C RID: 1404
			Unknown
		}

		// Token: 0x0200008A RID: 138
		public class PrivateKeyInfo
		{
			// Token: 0x0600044F RID: 1103 RVA: 0x00018E1C File Offset: 0x0001701C
			public PrivateKeyInfo()
			{
				this._version = 0;
				this._list = new ArrayList();
			}

			// Token: 0x06000450 RID: 1104 RVA: 0x00018E36 File Offset: 0x00017036
			public PrivateKeyInfo(byte[] data)
				: this()
			{
				this.Decode(data);
			}

			// Token: 0x170000D5 RID: 213
			// (get) Token: 0x06000451 RID: 1105 RVA: 0x00018E45 File Offset: 0x00017045
			// (set) Token: 0x06000452 RID: 1106 RVA: 0x00018E4D File Offset: 0x0001704D
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

			// Token: 0x170000D6 RID: 214
			// (get) Token: 0x06000453 RID: 1107 RVA: 0x00018E56 File Offset: 0x00017056
			public ArrayList Attributes
			{
				get
				{
					return this._list;
				}
			}

			// Token: 0x170000D7 RID: 215
			// (get) Token: 0x06000454 RID: 1108 RVA: 0x00018E5E File Offset: 0x0001705E
			// (set) Token: 0x06000455 RID: 1109 RVA: 0x00018E7A File Offset: 0x0001707A
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

			// Token: 0x170000D8 RID: 216
			// (get) Token: 0x06000456 RID: 1110 RVA: 0x00018E9B File Offset: 0x0001709B
			// (set) Token: 0x06000457 RID: 1111 RVA: 0x00018EA3 File Offset: 0x000170A3
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

			// Token: 0x06000458 RID: 1112 RVA: 0x00018EBC File Offset: 0x000170BC
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

			// Token: 0x06000459 RID: 1113 RVA: 0x00018FA4 File Offset: 0x000171A4
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

			// Token: 0x0600045A RID: 1114 RVA: 0x00019094 File Offset: 0x00017294
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

			// Token: 0x0600045B RID: 1115 RVA: 0x000190C4 File Offset: 0x000172C4
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

			// Token: 0x0600045C RID: 1116 RVA: 0x00019100 File Offset: 0x00017300
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

			// Token: 0x0600045D RID: 1117 RVA: 0x00019278 File Offset: 0x00017478
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

			// Token: 0x0600045E RID: 1118 RVA: 0x00019340 File Offset: 0x00017540
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

			// Token: 0x0600045F RID: 1119 RVA: 0x00019388 File Offset: 0x00017588
			public static byte[] Encode(DSA dsa)
			{
				return ASN1Convert.FromUnsignedBigInteger(dsa.ExportParameters(true).X).GetBytes();
			}

			// Token: 0x06000460 RID: 1120 RVA: 0x000193A0 File Offset: 0x000175A0
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

			// Token: 0x0400057D RID: 1405
			private int _version;

			// Token: 0x0400057E RID: 1406
			private string _algorithm;

			// Token: 0x0400057F RID: 1407
			private byte[] _key;

			// Token: 0x04000580 RID: 1408
			private ArrayList _list;
		}

		// Token: 0x0200008B RID: 139
		public class EncryptedPrivateKeyInfo
		{
			// Token: 0x06000461 RID: 1121 RVA: 0x00002111 File Offset: 0x00000311
			public EncryptedPrivateKeyInfo()
			{
			}

			// Token: 0x06000462 RID: 1122 RVA: 0x000193DA File Offset: 0x000175DA
			public EncryptedPrivateKeyInfo(byte[] data)
				: this()
			{
				this.Decode(data);
			}

			// Token: 0x170000D9 RID: 217
			// (get) Token: 0x06000463 RID: 1123 RVA: 0x000193E9 File Offset: 0x000175E9
			// (set) Token: 0x06000464 RID: 1124 RVA: 0x000193F1 File Offset: 0x000175F1
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

			// Token: 0x170000DA RID: 218
			// (get) Token: 0x06000465 RID: 1125 RVA: 0x000193FA File Offset: 0x000175FA
			// (set) Token: 0x06000466 RID: 1126 RVA: 0x00019416 File Offset: 0x00017616
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

			// Token: 0x170000DB RID: 219
			// (get) Token: 0x06000467 RID: 1127 RVA: 0x0001942F File Offset: 0x0001762F
			// (set) Token: 0x06000468 RID: 1128 RVA: 0x00019465 File Offset: 0x00017665
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

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x06000469 RID: 1129 RVA: 0x00019478 File Offset: 0x00017678
			// (set) Token: 0x0600046A RID: 1130 RVA: 0x00019480 File Offset: 0x00017680
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

			// Token: 0x0600046B RID: 1131 RVA: 0x000194A0 File Offset: 0x000176A0
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

			// Token: 0x0600046C RID: 1132 RVA: 0x000195AC File Offset: 0x000177AC
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

			// Token: 0x04000581 RID: 1409
			private string _algorithm;

			// Token: 0x04000582 RID: 1410
			private byte[] _salt;

			// Token: 0x04000583 RID: 1411
			private int _iterations;

			// Token: 0x04000584 RID: 1412
			private byte[] _data;
		}
	}
}
