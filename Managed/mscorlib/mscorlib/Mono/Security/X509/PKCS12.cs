using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.X509
{
	// Token: 0x02000056 RID: 86
	internal class PKCS12 : ICloneable
	{
		// Token: 0x06000275 RID: 629 RVA: 0x0000E30C File Offset: 0x0000C50C
		public PKCS12()
		{
			this._iterations = 2000;
			this._keyBags = new ArrayList();
			this._secretBags = new ArrayList();
			this._certs = new X509CertificateCollection();
			this._keyBagsChanged = false;
			this._secretBagsChanged = false;
			this._certsChanged = false;
			this._safeBags = new ArrayList();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000E36B File Offset: 0x0000C56B
		public PKCS12(byte[] data)
			: this()
		{
			this.Password = null;
			this.Decode(data);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000E381 File Offset: 0x0000C581
		public PKCS12(byte[] data, string password)
			: this()
		{
			this.Password = password;
			this.Decode(data);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000E397 File Offset: 0x0000C597
		public PKCS12(byte[] data, byte[] password)
			: this()
		{
			this._password = password;
			this.Decode(data);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000E3B0 File Offset: 0x0000C5B0
		private void Decode(byte[] data)
		{
			ASN1 asn = new ASN1(data);
			if (asn.Tag != 48)
			{
				throw new ArgumentException("invalid data");
			}
			if (asn[0].Tag != 2)
			{
				throw new ArgumentException("invalid PFX version");
			}
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(asn[1]);
			if (contentInfo.ContentType != "1.2.840.113549.1.7.1")
			{
				throw new ArgumentException("invalid authenticated safe");
			}
			if (asn.Count > 2)
			{
				ASN1 asn2 = asn[2];
				if (asn2.Tag != 48)
				{
					throw new ArgumentException("invalid MAC");
				}
				ASN1 asn3 = asn2[0];
				if (asn3.Tag != 48)
				{
					throw new ArgumentException("invalid MAC");
				}
				if (ASN1Convert.ToOid(asn3[0][0]) != "1.3.14.3.2.26")
				{
					throw new ArgumentException("unsupported HMAC");
				}
				byte[] value = asn3[1].Value;
				ASN1 asn4 = asn2[1];
				if (asn4.Tag != 4)
				{
					throw new ArgumentException("missing MAC salt");
				}
				this._iterations = 1;
				if (asn2.Count > 2)
				{
					ASN1 asn5 = asn2[2];
					if (asn5.Tag != 2)
					{
						throw new ArgumentException("invalid MAC iteration");
					}
					this._iterations = ASN1Convert.ToInt32(asn5);
				}
				byte[] value2 = contentInfo.Content[0].Value;
				byte[] array = this.MAC(this._password, asn4.Value, this._iterations, value2);
				if (!this.Compare(value, array))
				{
					byte[] array2 = new byte[2];
					array = this.MAC(array2, asn4.Value, this._iterations, value2);
					if (!this.Compare(value, array))
					{
						throw new CryptographicException("Invalid MAC - file may have been tampered with!");
					}
					this._password = array2;
				}
			}
			ASN1 asn6 = new ASN1(contentInfo.Content[0].Value);
			for (int i = 0; i < asn6.Count; i++)
			{
				PKCS7.ContentInfo contentInfo2 = new PKCS7.ContentInfo(asn6[i]);
				string contentType = contentInfo2.ContentType;
				if (!(contentType == "1.2.840.113549.1.7.1"))
				{
					if (!(contentType == "1.2.840.113549.1.7.6"))
					{
						if (!(contentType == "1.2.840.113549.1.7.3"))
						{
							throw new ArgumentException("unknown authenticatedSafe");
						}
						throw new NotImplementedException("public key encrypted");
					}
					else
					{
						PKCS7.EncryptedData encryptedData = new PKCS7.EncryptedData(contentInfo2.Content[0]);
						ASN1 asn7 = new ASN1(this.Decrypt(encryptedData));
						for (int j = 0; j < asn7.Count; j++)
						{
							ASN1 asn8 = asn7[j];
							this.ReadSafeBag(asn8);
						}
					}
				}
				else
				{
					ASN1 asn9 = new ASN1(contentInfo2.Content[0].Value);
					for (int k = 0; k < asn9.Count; k++)
					{
						ASN1 asn10 = asn9[k];
						this.ReadSafeBag(asn10);
					}
				}
			}
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000E694 File Offset: 0x0000C894
		~PKCS12()
		{
			if (this._password != null)
			{
				Array.Clear(this._password, 0, this._password.Length);
			}
			this._password = null;
		}

		// Token: 0x1700006A RID: 106
		// (set) Token: 0x0600027B RID: 635 RVA: 0x0000E6E0 File Offset: 0x0000C8E0
		public string Password
		{
			set
			{
				if (this._password != null)
				{
					Array.Clear(this._password, 0, this._password.Length);
				}
				this._password = null;
				if (value != null)
				{
					if (value.Length > 0)
					{
						int num = value.Length;
						int num2 = 0;
						if (num < PKCS12.MaximumPasswordLength)
						{
							if (value[num - 1] != '\0')
							{
								num2 = 1;
							}
						}
						else
						{
							num = PKCS12.MaximumPasswordLength;
						}
						this._password = new byte[num + num2 << 1];
						Encoding.BigEndianUnicode.GetBytes(value, 0, num, this._password, 0);
						return;
					}
					this._password = new byte[2];
				}
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600027C RID: 636 RVA: 0x0000E774 File Offset: 0x0000C974
		// (set) Token: 0x0600027D RID: 637 RVA: 0x0000E77C File Offset: 0x0000C97C
		public int IterationCount
		{
			get
			{
				return this._iterations;
			}
			set
			{
				this._iterations = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600027E RID: 638 RVA: 0x0000E788 File Offset: 0x0000C988
		public ArrayList Keys
		{
			get
			{
				if (this._keyBagsChanged)
				{
					this._keyBags.Clear();
					foreach (object obj in this._safeBags)
					{
						SafeBag safeBag = (SafeBag)obj;
						if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
						{
							byte[] privateKey = new PKCS8.PrivateKeyInfo(safeBag.ASN1[1].Value).PrivateKey;
							byte b = privateKey[0];
							if (b != 2)
							{
								if (b == 48)
								{
									this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeRSA(privateKey));
								}
							}
							else
							{
								DSAParameters dsaparameters = default(DSAParameters);
								this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, dsaparameters));
							}
							Array.Clear(privateKey, 0, privateKey.Length);
						}
						else if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
						{
							PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(safeBag.ASN1[1].Value);
							byte[] array = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
							byte[] privateKey2 = new PKCS8.PrivateKeyInfo(array).PrivateKey;
							byte b = privateKey2[0];
							if (b != 2)
							{
								if (b == 48)
								{
									this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeRSA(privateKey2));
								}
							}
							else
							{
								DSAParameters dsaparameters2 = default(DSAParameters);
								this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeDSA(privateKey2, dsaparameters2));
							}
							Array.Clear(privateKey2, 0, privateKey2.Length);
							Array.Clear(array, 0, array.Length);
						}
					}
					this._keyBagsChanged = false;
				}
				return ArrayList.ReadOnly(this._keyBags);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000E94C File Offset: 0x0000CB4C
		public ArrayList Secrets
		{
			get
			{
				if (this._secretBagsChanged)
				{
					this._secretBags.Clear();
					foreach (object obj in this._safeBags)
					{
						SafeBag safeBag = (SafeBag)obj;
						if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
						{
							byte[] value = safeBag.ASN1[1].Value;
							this._secretBags.Add(value);
						}
					}
					this._secretBagsChanged = false;
				}
				return ArrayList.ReadOnly(this._secretBags);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000280 RID: 640 RVA: 0x0000E9F4 File Offset: 0x0000CBF4
		public X509CertificateCollection Certificates
		{
			get
			{
				if (this._certsChanged)
				{
					this._certs.Clear();
					foreach (object obj in this._safeBags)
					{
						SafeBag safeBag = (SafeBag)obj;
						if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
						{
							PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(safeBag.ASN1[1].Value);
							this._certs.Add(new X509Certificate(contentInfo.Content[0].Value));
						}
					}
					this._certsChanged = false;
				}
				return this._certs;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
		internal RandomNumberGenerator RNG
		{
			get
			{
				if (this._rng == null)
				{
					this._rng = RandomNumberGenerator.Create();
				}
				return this._rng;
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000EAD0 File Offset: 0x0000CCD0
		private bool Compare(byte[] expected, byte[] actual)
		{
			bool flag = false;
			if (expected.Length == actual.Length)
			{
				for (int i = 0; i < expected.Length; i++)
				{
					if (expected[i] != actual[i])
					{
						return false;
					}
				}
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000EB04 File Offset: 0x0000CD04
		private SymmetricAlgorithm GetSymmetricAlgorithm(string algorithmOid, byte[] salt, int iterationCount)
		{
			string text = null;
			int num = 8;
			int num2 = 8;
			PKCS12.DeriveBytes deriveBytes = new PKCS12.DeriveBytes();
			deriveBytes.Password = this._password;
			deriveBytes.Salt = salt;
			deriveBytes.IterationCount = iterationCount;
			uint num3 = <PrivateImplementationDetails>.ComputeStringHash(algorithmOid);
			if (num3 <= 2949822700U)
			{
				if (num3 <= 2882712224U)
				{
					if (num3 != 1314512600U)
					{
						if (num3 != 1331290219U)
						{
							if (num3 == 2882712224U)
							{
								if (algorithmOid == "1.2.840.113549.1.12.1.6")
								{
									deriveBytes.HashName = "SHA1";
									text = "RC2";
									num = 5;
									goto IL_02FE;
								}
							}
						}
						else if (algorithmOid == "1.2.840.113549.1.5.11")
						{
							deriveBytes.HashName = "SHA1";
							text = "RC2";
							num = 4;
							goto IL_02FE;
						}
					}
					else if (algorithmOid == "1.2.840.113549.1.5.10")
					{
						deriveBytes.HashName = "SHA1";
						text = "DES";
						goto IL_02FE;
					}
				}
				else if (num3 != 2916267462U)
				{
					if (num3 != 2933045081U)
					{
						if (num3 == 2949822700U)
						{
							if (algorithmOid == "1.2.840.113549.1.12.1.2")
							{
								deriveBytes.HashName = "SHA1";
								text = "RC4";
								num = 5;
								num2 = 0;
								goto IL_02FE;
							}
						}
					}
					else if (algorithmOid == "1.2.840.113549.1.12.1.5")
					{
						deriveBytes.HashName = "SHA1";
						text = "RC2";
						num = 16;
						goto IL_02FE;
					}
				}
				else if (algorithmOid == "1.2.840.113549.1.12.1.4")
				{
					deriveBytes.HashName = "SHA1";
					text = "TripleDES";
					num = 16;
					goto IL_02FE;
				}
			}
			else if (num3 <= 3543878904U)
			{
				if (num3 != 2966600319U)
				{
					if (num3 != 3000155557U)
					{
						if (num3 == 3543878904U)
						{
							if (algorithmOid == "1.2.840.113549.1.5.1")
							{
								deriveBytes.HashName = "MD2";
								text = "DES";
								goto IL_02FE;
							}
						}
					}
					else if (algorithmOid == "1.2.840.113549.1.12.1.1")
					{
						deriveBytes.HashName = "SHA1";
						text = "RC4";
						num = 16;
						num2 = 0;
						goto IL_02FE;
					}
				}
				else if (algorithmOid == "1.2.840.113549.1.12.1.3")
				{
					deriveBytes.HashName = "SHA1";
					text = "TripleDES";
					num = 24;
					goto IL_02FE;
				}
			}
			else if (num3 != 3577434142U)
			{
				if (num3 != 3627766999U)
				{
					if (num3 == 3661322237U)
					{
						if (algorithmOid == "1.2.840.113549.1.5.6")
						{
							deriveBytes.HashName = "MD5";
							text = "RC2";
							num = 4;
							goto IL_02FE;
						}
					}
				}
				else if (algorithmOid == "1.2.840.113549.1.5.4")
				{
					deriveBytes.HashName = "MD2";
					text = "RC2";
					num = 4;
					goto IL_02FE;
				}
			}
			else if (algorithmOid == "1.2.840.113549.1.5.3")
			{
				deriveBytes.HashName = "MD5";
				text = "DES";
				goto IL_02FE;
			}
			throw new NotSupportedException("unknown oid " + text);
			IL_02FE:
			SymmetricAlgorithm symmetricAlgorithm = SymmetricAlgorithm.Create(text);
			symmetricAlgorithm.Key = deriveBytes.DeriveKey(num);
			if (num2 > 0)
			{
				symmetricAlgorithm.IV = deriveBytes.DeriveIV(num2);
				symmetricAlgorithm.Mode = CipherMode.CBC;
			}
			return symmetricAlgorithm;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000EE44 File Offset: 0x0000D044
		public byte[] Decrypt(string algorithmOid, byte[] salt, int iterationCount, byte[] encryptedData)
		{
			SymmetricAlgorithm symmetricAlgorithm = null;
			byte[] array = null;
			try
			{
				symmetricAlgorithm = this.GetSymmetricAlgorithm(algorithmOid, salt, iterationCount);
				array = symmetricAlgorithm.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
			}
			finally
			{
				if (symmetricAlgorithm != null)
				{
					symmetricAlgorithm.Clear();
				}
			}
			return array;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000EE90 File Offset: 0x0000D090
		public byte[] Decrypt(PKCS7.EncryptedData ed)
		{
			return this.Decrypt(ed.EncryptionAlgorithm.ContentType, ed.EncryptionAlgorithm.Content[0].Value, ASN1Convert.ToInt32(ed.EncryptionAlgorithm.Content[1]), ed.EncryptedContent);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		public byte[] Encrypt(string algorithmOid, byte[] salt, int iterationCount, byte[] data)
		{
			byte[] array = null;
			using (SymmetricAlgorithm symmetricAlgorithm = this.GetSymmetricAlgorithm(algorithmOid, salt, iterationCount))
			{
				array = symmetricAlgorithm.CreateEncryptor().TransformFinalBlock(data, 0, data.Length);
			}
			return array;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000EF2C File Offset: 0x0000D12C
		private DSAParameters GetExistingParameters(out bool found)
		{
			foreach (X509Certificate x509Certificate in this.Certificates)
			{
				if (x509Certificate.KeyAlgorithmParameters != null)
				{
					DSA dsa = x509Certificate.DSA;
					if (dsa != null)
					{
						found = true;
						return dsa.ExportParameters(false);
					}
				}
			}
			found = false;
			return default(DSAParameters);
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		private void AddPrivateKey(PKCS8.PrivateKeyInfo pki)
		{
			byte[] privateKey = pki.PrivateKey;
			byte b = privateKey[0];
			if (b != 2)
			{
				if (b != 48)
				{
					Array.Clear(privateKey, 0, privateKey.Length);
					throw new CryptographicException("Unknown private key format");
				}
				this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeRSA(privateKey));
			}
			else
			{
				bool flag;
				DSAParameters existingParameters = this.GetExistingParameters(out flag);
				if (flag)
				{
					this._keyBags.Add(PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, existingParameters));
				}
			}
			Array.Clear(privateKey, 0, privateKey.Length);
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000F024 File Offset: 0x0000D224
		private void ReadSafeBag(ASN1 safeBag)
		{
			if (safeBag.Tag != 48)
			{
				throw new ArgumentException("invalid safeBag");
			}
			ASN1 asn = safeBag[0];
			if (asn.Tag != 6)
			{
				throw new ArgumentException("invalid safeBag id");
			}
			ASN1 asn2 = safeBag[1];
			string text = ASN1Convert.ToOid(asn);
			if (!(text == "1.2.840.113549.1.12.10.1.1"))
			{
				if (!(text == "1.2.840.113549.1.12.10.1.2"))
				{
					if (!(text == "1.2.840.113549.1.12.10.1.3"))
					{
						if (!(text == "1.2.840.113549.1.12.10.1.4"))
						{
							if (!(text == "1.2.840.113549.1.12.10.1.5"))
							{
								if (!(text == "1.2.840.113549.1.12.10.1.6"))
								{
									throw new ArgumentException("unknown safeBag oid");
								}
							}
							else
							{
								byte[] value = asn2.Value;
								this._secretBags.Add(value);
							}
						}
					}
					else
					{
						PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(asn2.Value);
						if (contentInfo.ContentType != "1.2.840.113549.1.9.22.1")
						{
							throw new NotSupportedException("unsupport certificate type");
						}
						X509Certificate x509Certificate = new X509Certificate(contentInfo.Content[0].Value);
						this._certs.Add(x509Certificate);
					}
				}
				else
				{
					PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(asn2.Value);
					byte[] array = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
					this.AddPrivateKey(new PKCS8.PrivateKeyInfo(array));
					Array.Clear(array, 0, array.Length);
				}
			}
			else
			{
				this.AddPrivateKey(new PKCS8.PrivateKeyInfo(asn2.Value));
			}
			if (safeBag.Count > 2)
			{
				ASN1 asn3 = safeBag[2];
				if (asn3.Tag != 49)
				{
					throw new ArgumentException("invalid safeBag attributes id");
				}
				for (int i = 0; i < asn3.Count; i++)
				{
					ASN1 asn4 = asn3[i];
					if (asn4.Tag != 48)
					{
						throw new ArgumentException("invalid PKCS12 attributes id");
					}
					ASN1 asn5 = asn4[0];
					if (asn5.Tag != 6)
					{
						throw new ArgumentException("invalid attribute id");
					}
					string text2 = ASN1Convert.ToOid(asn5);
					ASN1 asn6 = asn4[1];
					for (int j = 0; j < asn6.Count; j++)
					{
						ASN1 asn7 = asn6[j];
						if (!(text2 == "1.2.840.113549.1.9.20"))
						{
							if (text2 == "1.2.840.113549.1.9.21")
							{
								if (asn7.Tag != 4)
								{
									throw new ArgumentException("invalid attribute value id");
								}
							}
						}
						else if (asn7.Tag != 30)
						{
							throw new ArgumentException("invalid attribute value id");
						}
					}
				}
			}
			this._safeBags.Add(new SafeBag(text, safeBag));
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000F2A8 File Offset: 0x0000D4A8
		private ASN1 Pkcs8ShroudedKeyBagSafeBag(AsymmetricAlgorithm aa, IDictionary attributes)
		{
			PKCS8.PrivateKeyInfo privateKeyInfo = new PKCS8.PrivateKeyInfo();
			if (aa is RSA)
			{
				privateKeyInfo.Algorithm = "1.2.840.113549.1.1.1";
				privateKeyInfo.PrivateKey = PKCS8.PrivateKeyInfo.Encode((RSA)aa);
			}
			else
			{
				if (!(aa is DSA))
				{
					throw new CryptographicException("Unknown asymmetric algorithm {0}", aa.ToString());
				}
				privateKeyInfo.Algorithm = null;
				privateKeyInfo.PrivateKey = PKCS8.PrivateKeyInfo.Encode((DSA)aa);
			}
			PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo();
			encryptedPrivateKeyInfo.Algorithm = "1.2.840.113549.1.12.1.3";
			encryptedPrivateKeyInfo.IterationCount = this._iterations;
			encryptedPrivateKeyInfo.EncryptedData = this.Encrypt("1.2.840.113549.1.12.1.3", encryptedPrivateKeyInfo.Salt, this._iterations, privateKeyInfo.GetBytes());
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid("1.2.840.113549.1.12.10.1.2"));
			ASN1 asn2 = new ASN1(160);
			asn2.Add(new ASN1(encryptedPrivateKeyInfo.GetBytes()));
			asn.Add(asn2);
			if (attributes != null)
			{
				ASN1 asn3 = new ASN1(49);
				IDictionaryEnumerator enumerator = attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					if (!(text == "1.2.840.113549.1.9.20"))
					{
						if (text == "1.2.840.113549.1.9.21")
						{
							ArrayList arrayList = (ArrayList)enumerator.Value;
							if (arrayList.Count > 0)
							{
								ASN1 asn4 = new ASN1(48);
								asn4.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.21"));
								ASN1 asn5 = new ASN1(49);
								foreach (object obj in arrayList)
								{
									byte[] array = (byte[])obj;
									asn5.Add(new ASN1(4)
									{
										Value = array
									});
								}
								asn4.Add(asn5);
								asn3.Add(asn4);
							}
						}
					}
					else
					{
						ArrayList arrayList2 = (ArrayList)enumerator.Value;
						if (arrayList2.Count > 0)
						{
							ASN1 asn6 = new ASN1(48);
							asn6.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.20"));
							ASN1 asn7 = new ASN1(49);
							foreach (object obj2 in arrayList2)
							{
								byte[] array2 = (byte[])obj2;
								asn7.Add(new ASN1(30)
								{
									Value = array2
								});
							}
							asn6.Add(asn7);
							asn3.Add(asn6);
						}
					}
				}
				if (asn3.Count > 0)
				{
					asn.Add(asn3);
				}
			}
			return asn;
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000F570 File Offset: 0x0000D770
		private ASN1 KeyBagSafeBag(AsymmetricAlgorithm aa, IDictionary attributes)
		{
			PKCS8.PrivateKeyInfo privateKeyInfo = new PKCS8.PrivateKeyInfo();
			if (aa is RSA)
			{
				privateKeyInfo.Algorithm = "1.2.840.113549.1.1.1";
				privateKeyInfo.PrivateKey = PKCS8.PrivateKeyInfo.Encode((RSA)aa);
			}
			else
			{
				if (!(aa is DSA))
				{
					throw new CryptographicException("Unknown asymmetric algorithm {0}", aa.ToString());
				}
				privateKeyInfo.Algorithm = null;
				privateKeyInfo.PrivateKey = PKCS8.PrivateKeyInfo.Encode((DSA)aa);
			}
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid("1.2.840.113549.1.12.10.1.1"));
			ASN1 asn2 = new ASN1(160);
			asn2.Add(new ASN1(privateKeyInfo.GetBytes()));
			asn.Add(asn2);
			if (attributes != null)
			{
				ASN1 asn3 = new ASN1(49);
				IDictionaryEnumerator enumerator = attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					if (!(text == "1.2.840.113549.1.9.20"))
					{
						if (text == "1.2.840.113549.1.9.21")
						{
							ArrayList arrayList = (ArrayList)enumerator.Value;
							if (arrayList.Count > 0)
							{
								ASN1 asn4 = new ASN1(48);
								asn4.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.21"));
								ASN1 asn5 = new ASN1(49);
								foreach (object obj in arrayList)
								{
									byte[] array = (byte[])obj;
									asn5.Add(new ASN1(4)
									{
										Value = array
									});
								}
								asn4.Add(asn5);
								asn3.Add(asn4);
							}
						}
					}
					else
					{
						ArrayList arrayList2 = (ArrayList)enumerator.Value;
						if (arrayList2.Count > 0)
						{
							ASN1 asn6 = new ASN1(48);
							asn6.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.20"));
							ASN1 asn7 = new ASN1(49);
							foreach (object obj2 in arrayList2)
							{
								byte[] array2 = (byte[])obj2;
								asn7.Add(new ASN1(30)
								{
									Value = array2
								});
							}
							asn6.Add(asn7);
							asn3.Add(asn6);
						}
					}
				}
				if (asn3.Count > 0)
				{
					asn.Add(asn3);
				}
			}
			return asn;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000F7F4 File Offset: 0x0000D9F4
		private ASN1 SecretBagSafeBag(byte[] secret, IDictionary attributes)
		{
			ASN1 asn = new ASN1(48);
			asn.Add(ASN1Convert.FromOid("1.2.840.113549.1.12.10.1.5"));
			ASN1 asn2 = new ASN1(128, secret);
			asn.Add(asn2);
			if (attributes != null)
			{
				ASN1 asn3 = new ASN1(49);
				IDictionaryEnumerator enumerator = attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					if (!(text == "1.2.840.113549.1.9.20"))
					{
						if (text == "1.2.840.113549.1.9.21")
						{
							ArrayList arrayList = (ArrayList)enumerator.Value;
							if (arrayList.Count > 0)
							{
								ASN1 asn4 = new ASN1(48);
								asn4.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.21"));
								ASN1 asn5 = new ASN1(49);
								foreach (object obj in arrayList)
								{
									byte[] array = (byte[])obj;
									asn5.Add(new ASN1(4)
									{
										Value = array
									});
								}
								asn4.Add(asn5);
								asn3.Add(asn4);
							}
						}
					}
					else
					{
						ArrayList arrayList2 = (ArrayList)enumerator.Value;
						if (arrayList2.Count > 0)
						{
							ASN1 asn6 = new ASN1(48);
							asn6.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.20"));
							ASN1 asn7 = new ASN1(49);
							foreach (object obj2 in arrayList2)
							{
								byte[] array2 = (byte[])obj2;
								asn7.Add(new ASN1(30)
								{
									Value = array2
								});
							}
							asn6.Add(asn7);
							asn3.Add(asn6);
						}
					}
				}
				if (asn3.Count > 0)
				{
					asn.Add(asn3);
				}
			}
			return asn;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000FA04 File Offset: 0x0000DC04
		private ASN1 CertificateSafeBag(X509Certificate x509, IDictionary attributes)
		{
			ASN1 asn = new ASN1(4, x509.RawData);
			PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo();
			contentInfo.ContentType = "1.2.840.113549.1.9.22.1";
			contentInfo.Content.Add(asn);
			ASN1 asn2 = new ASN1(160);
			asn2.Add(contentInfo.ASN1);
			ASN1 asn3 = new ASN1(48);
			asn3.Add(ASN1Convert.FromOid("1.2.840.113549.1.12.10.1.3"));
			asn3.Add(asn2);
			if (attributes != null)
			{
				ASN1 asn4 = new ASN1(49);
				IDictionaryEnumerator enumerator = attributes.GetEnumerator();
				while (enumerator.MoveNext())
				{
					string text = (string)enumerator.Key;
					if (!(text == "1.2.840.113549.1.9.20"))
					{
						if (text == "1.2.840.113549.1.9.21")
						{
							ArrayList arrayList = (ArrayList)enumerator.Value;
							if (arrayList.Count > 0)
							{
								ASN1 asn5 = new ASN1(48);
								asn5.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.21"));
								ASN1 asn6 = new ASN1(49);
								foreach (object obj in arrayList)
								{
									byte[] array = (byte[])obj;
									asn6.Add(new ASN1(4)
									{
										Value = array
									});
								}
								asn5.Add(asn6);
								asn4.Add(asn5);
							}
						}
					}
					else
					{
						ArrayList arrayList2 = (ArrayList)enumerator.Value;
						if (arrayList2.Count > 0)
						{
							ASN1 asn7 = new ASN1(48);
							asn7.Add(ASN1Convert.FromOid("1.2.840.113549.1.9.20"));
							ASN1 asn8 = new ASN1(49);
							foreach (object obj2 in arrayList2)
							{
								byte[] array2 = (byte[])obj2;
								asn8.Add(new ASN1(30)
								{
									Value = array2
								});
							}
							asn7.Add(asn8);
							asn4.Add(asn7);
						}
					}
				}
				if (asn4.Count > 0)
				{
					asn3.Add(asn4);
				}
			}
			return asn3;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000FC54 File Offset: 0x0000DE54
		private byte[] MAC(byte[] password, byte[] salt, int iterations, byte[] data)
		{
			PKCS12.DeriveBytes deriveBytes = new PKCS12.DeriveBytes();
			deriveBytes.HashName = "SHA1";
			deriveBytes.Password = password;
			deriveBytes.Salt = salt;
			deriveBytes.IterationCount = iterations;
			HMACSHA1 hmacsha = (HMACSHA1)HMAC.Create();
			hmacsha.Key = deriveBytes.DeriveMAC(20);
			return hmacsha.ComputeHash(data, 0, data.Length);
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000FCAC File Offset: 0x0000DEAC
		public byte[] GetBytes()
		{
			ASN1 asn = new ASN1(48);
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this._safeBags)
			{
				SafeBag safeBag = (SafeBag)obj;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					PKCS7.ContentInfo contentInfo = new PKCS7.ContentInfo(safeBag.ASN1[1].Value);
					arrayList.Add(new X509Certificate(contentInfo.Content[0].Value));
				}
			}
			ArrayList arrayList2 = new ArrayList();
			ArrayList arrayList3 = new ArrayList();
			foreach (X509Certificate x509Certificate in this.Certificates)
			{
				bool flag = false;
				foreach (object obj2 in arrayList)
				{
					X509Certificate x509Certificate2 = (X509Certificate)obj2;
					if (this.Compare(x509Certificate.RawData, x509Certificate2.RawData))
					{
						flag = true;
					}
				}
				if (!flag)
				{
					arrayList2.Add(x509Certificate);
				}
			}
			foreach (object obj3 in arrayList)
			{
				X509Certificate x509Certificate3 = (X509Certificate)obj3;
				bool flag2 = false;
				foreach (X509Certificate x509Certificate4 in this.Certificates)
				{
					if (this.Compare(x509Certificate3.RawData, x509Certificate4.RawData))
					{
						flag2 = true;
					}
				}
				if (!flag2)
				{
					arrayList3.Add(x509Certificate3);
				}
			}
			foreach (object obj4 in arrayList3)
			{
				X509Certificate x509Certificate5 = (X509Certificate)obj4;
				this.RemoveCertificate(x509Certificate5);
			}
			foreach (object obj5 in arrayList2)
			{
				X509Certificate x509Certificate6 = (X509Certificate)obj5;
				this.AddCertificate(x509Certificate6);
			}
			if (this._safeBags.Count > 0)
			{
				ASN1 asn2 = new ASN1(48);
				foreach (object obj6 in this._safeBags)
				{
					SafeBag safeBag2 = (SafeBag)obj6;
					if (safeBag2.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
					{
						asn2.Add(safeBag2.ASN1);
					}
				}
				if (asn2.Count > 0)
				{
					PKCS7.ContentInfo contentInfo2 = this.EncryptedContentInfo(asn2, "1.2.840.113549.1.12.1.3");
					asn.Add(contentInfo2.ASN1);
				}
			}
			if (this._safeBags.Count > 0)
			{
				ASN1 asn3 = new ASN1(48);
				foreach (object obj7 in this._safeBags)
				{
					SafeBag safeBag3 = (SafeBag)obj7;
					if (safeBag3.BagOID.Equals("1.2.840.113549.1.12.10.1.1") || safeBag3.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
					{
						asn3.Add(safeBag3.ASN1);
					}
				}
				if (asn3.Count > 0)
				{
					ASN1 asn4 = new ASN1(160);
					asn4.Add(new ASN1(4, asn3.GetBytes()));
					asn.Add(new PKCS7.ContentInfo("1.2.840.113549.1.7.1")
					{
						Content = asn4
					}.ASN1);
				}
			}
			if (this._safeBags.Count > 0)
			{
				ASN1 asn5 = new ASN1(48);
				foreach (object obj8 in this._safeBags)
				{
					SafeBag safeBag4 = (SafeBag)obj8;
					if (safeBag4.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
					{
						asn5.Add(safeBag4.ASN1);
					}
				}
				if (asn5.Count > 0)
				{
					PKCS7.ContentInfo contentInfo3 = this.EncryptedContentInfo(asn5, "1.2.840.113549.1.12.1.3");
					asn.Add(contentInfo3.ASN1);
				}
			}
			ASN1 asn6 = new ASN1(4, asn.GetBytes());
			ASN1 asn7 = new ASN1(160);
			asn7.Add(asn6);
			PKCS7.ContentInfo contentInfo4 = new PKCS7.ContentInfo("1.2.840.113549.1.7.1");
			contentInfo4.Content = asn7;
			ASN1 asn8 = new ASN1(48);
			if (this._password != null)
			{
				byte[] array = new byte[20];
				this.RNG.GetBytes(array);
				byte[] array2 = this.MAC(this._password, array, this._iterations, contentInfo4.Content[0].Value);
				ASN1 asn9 = new ASN1(48);
				asn9.Add(ASN1Convert.FromOid("1.3.14.3.2.26"));
				asn9.Add(new ASN1(5));
				ASN1 asn10 = new ASN1(48);
				asn10.Add(asn9);
				asn10.Add(new ASN1(4, array2));
				asn8.Add(asn10);
				asn8.Add(new ASN1(4, array));
				asn8.Add(ASN1Convert.FromInt32(this._iterations));
			}
			ASN1 asn11 = new ASN1(2, new byte[] { 3 });
			ASN1 asn12 = new ASN1(48);
			asn12.Add(asn11);
			asn12.Add(contentInfo4.ASN1);
			if (asn8.Count > 0)
			{
				asn12.Add(asn8);
			}
			return asn12.GetBytes();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x000102F8 File Offset: 0x0000E4F8
		private PKCS7.ContentInfo EncryptedContentInfo(ASN1 safeBags, string algorithmOid)
		{
			byte[] array = new byte[8];
			this.RNG.GetBytes(array);
			ASN1 asn = new ASN1(48);
			asn.Add(new ASN1(4, array));
			asn.Add(ASN1Convert.FromInt32(this._iterations));
			ASN1 asn2 = new ASN1(48);
			asn2.Add(ASN1Convert.FromOid(algorithmOid));
			asn2.Add(asn);
			byte[] array2 = this.Encrypt(algorithmOid, array, this._iterations, safeBags.GetBytes());
			ASN1 asn3 = new ASN1(128, array2);
			ASN1 asn4 = new ASN1(48);
			asn4.Add(ASN1Convert.FromOid("1.2.840.113549.1.7.1"));
			asn4.Add(asn2);
			asn4.Add(asn3);
			ASN1 asn5 = new ASN1(2, new byte[1]);
			ASN1 asn6 = new ASN1(48);
			asn6.Add(asn5);
			asn6.Add(asn4);
			ASN1 asn7 = new ASN1(160);
			asn7.Add(asn6);
			return new PKCS7.ContentInfo("1.2.840.113549.1.7.6")
			{
				Content = asn7
			};
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00010400 File Offset: 0x0000E600
		public void AddCertificate(X509Certificate cert)
		{
			this.AddCertificate(cert, null);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0001040C File Offset: 0x0000E60C
		public void AddCertificate(X509Certificate cert, IDictionary attributes)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					X509Certificate x509Certificate = new X509Certificate(new PKCS7.ContentInfo(safeBag.ASN1[1].Value).Content[0].Value);
					if (this.Compare(cert.RawData, x509Certificate.RawData))
					{
						flag = true;
					}
				}
				num++;
			}
			if (!flag)
			{
				this._safeBags.Add(new SafeBag("1.2.840.113549.1.12.10.1.3", this.CertificateSafeBag(cert, attributes)));
				this._certsChanged = true;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x000104C2 File Offset: 0x0000E6C2
		public void RemoveCertificate(X509Certificate cert)
		{
			this.RemoveCertificate(cert, null);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x000104CC File Offset: 0x0000E6CC
		public void RemoveCertificate(X509Certificate cert, IDictionary attrs)
		{
			int num = -1;
			int num2 = 0;
			while (num == -1 && num2 < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num2];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					ASN1 asn = safeBag.ASN1;
					X509Certificate x509Certificate = new X509Certificate(new PKCS7.ContentInfo(asn[1].Value).Content[0].Value);
					if (this.Compare(cert.RawData, x509Certificate.RawData))
					{
						if (attrs != null)
						{
							if (asn.Count == 3)
							{
								ASN1 asn2 = asn[2];
								int num3 = 0;
								for (int i = 0; i < asn2.Count; i++)
								{
									ASN1 asn3 = asn2[i];
									string text = ASN1Convert.ToOid(asn3[0]);
									ArrayList arrayList = (ArrayList)attrs[text];
									if (arrayList != null)
									{
										ASN1 asn4 = asn3[1];
										if (arrayList.Count == asn4.Count)
										{
											int num4 = 0;
											for (int j = 0; j < asn4.Count; j++)
											{
												ASN1 asn5 = asn4[j];
												byte[] array = (byte[])arrayList[j];
												if (this.Compare(array, asn5.Value))
												{
													num4++;
												}
											}
											if (num4 == asn4.Count)
											{
												num3++;
											}
										}
									}
								}
								if (num3 == asn2.Count)
								{
									num = num2;
								}
							}
						}
						else
						{
							num = num2;
						}
					}
				}
				num2++;
			}
			if (num != -1)
			{
				this._safeBags.RemoveAt(num);
				this._certsChanged = true;
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0001066E File Offset: 0x0000E86E
		private bool CompareAsymmetricAlgorithm(AsymmetricAlgorithm a1, AsymmetricAlgorithm a2)
		{
			return a1.KeySize == a2.KeySize && a1.ToXmlString(false) == a2.ToXmlString(false);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00010693 File Offset: 0x0000E893
		public void AddPkcs8ShroudedKeyBag(AsymmetricAlgorithm aa)
		{
			this.AddPkcs8ShroudedKeyBag(aa, null);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x000106A0 File Offset: 0x0000E8A0
		public void AddPkcs8ShroudedKeyBag(AsymmetricAlgorithm aa, IDictionary attributes)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(safeBag.ASN1[1].Value);
					byte[] array = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
					byte[] privateKey = new PKCS8.PrivateKeyInfo(array).PrivateKey;
					byte b = privateKey[0];
					AsymmetricAlgorithm asymmetricAlgorithm;
					if (b != 2)
					{
						if (b != 48)
						{
							Array.Clear(array, 0, array.Length);
							Array.Clear(privateKey, 0, privateKey.Length);
							throw new CryptographicException("Unknown private key format");
						}
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
					}
					else
					{
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
					}
					Array.Clear(array, 0, array.Length);
					Array.Clear(privateKey, 0, privateKey.Length);
					if (this.CompareAsymmetricAlgorithm(aa, asymmetricAlgorithm))
					{
						flag = true;
					}
				}
				num++;
			}
			if (!flag)
			{
				this._safeBags.Add(new SafeBag("1.2.840.113549.1.12.10.1.2", this.Pkcs8ShroudedKeyBagSafeBag(aa, attributes)));
				this._keyBagsChanged = true;
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x000107E0 File Offset: 0x0000E9E0
		public void RemovePkcs8ShroudedKeyBag(AsymmetricAlgorithm aa)
		{
			int num = -1;
			int num2 = 0;
			while (num == -1 && num2 < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num2];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(safeBag.ASN1[1].Value);
					byte[] array = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
					byte[] privateKey = new PKCS8.PrivateKeyInfo(array).PrivateKey;
					byte b = privateKey[0];
					AsymmetricAlgorithm asymmetricAlgorithm;
					if (b != 2)
					{
						if (b != 48)
						{
							Array.Clear(array, 0, array.Length);
							Array.Clear(privateKey, 0, privateKey.Length);
							throw new CryptographicException("Unknown private key format");
						}
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
					}
					else
					{
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
					}
					Array.Clear(array, 0, array.Length);
					Array.Clear(privateKey, 0, privateKey.Length);
					if (this.CompareAsymmetricAlgorithm(aa, asymmetricAlgorithm))
					{
						num = num2;
					}
				}
				num2++;
			}
			if (num != -1)
			{
				this._safeBags.RemoveAt(num);
				this._keyBagsChanged = true;
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00010910 File Offset: 0x0000EB10
		public void AddKeyBag(AsymmetricAlgorithm aa)
		{
			this.AddKeyBag(aa, null);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0001091C File Offset: 0x0000EB1C
		public void AddKeyBag(AsymmetricAlgorithm aa, IDictionary attributes)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
				{
					byte[] privateKey = new PKCS8.PrivateKeyInfo(safeBag.ASN1[1].Value).PrivateKey;
					byte b = privateKey[0];
					AsymmetricAlgorithm asymmetricAlgorithm;
					if (b != 2)
					{
						if (b != 48)
						{
							Array.Clear(privateKey, 0, privateKey.Length);
							throw new CryptographicException("Unknown private key format");
						}
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
					}
					else
					{
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
					}
					Array.Clear(privateKey, 0, privateKey.Length);
					if (this.CompareAsymmetricAlgorithm(aa, asymmetricAlgorithm))
					{
						flag = true;
					}
				}
				num++;
			}
			if (!flag)
			{
				this._safeBags.Add(new SafeBag("1.2.840.113549.1.12.10.1.1", this.KeyBagSafeBag(aa, attributes)));
				this._keyBagsChanged = true;
			}
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00010A14 File Offset: 0x0000EC14
		public void RemoveKeyBag(AsymmetricAlgorithm aa)
		{
			int num = -1;
			int num2 = 0;
			while (num == -1 && num2 < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num2];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
				{
					byte[] privateKey = new PKCS8.PrivateKeyInfo(safeBag.ASN1[1].Value).PrivateKey;
					byte b = privateKey[0];
					AsymmetricAlgorithm asymmetricAlgorithm;
					if (b != 2)
					{
						if (b != 48)
						{
							Array.Clear(privateKey, 0, privateKey.Length);
							throw new CryptographicException("Unknown private key format");
						}
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
					}
					else
					{
						asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
					}
					Array.Clear(privateKey, 0, privateKey.Length);
					if (this.CompareAsymmetricAlgorithm(aa, asymmetricAlgorithm))
					{
						num = num2;
					}
				}
				num2++;
			}
			if (num != -1)
			{
				this._safeBags.RemoveAt(num);
				this._keyBagsChanged = true;
			}
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00010AF9 File Offset: 0x0000ECF9
		public void AddSecretBag(byte[] secret)
		{
			this.AddSecretBag(secret, null);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00010B04 File Offset: 0x0000ED04
		public void AddSecretBag(byte[] secret, IDictionary attributes)
		{
			bool flag = false;
			int num = 0;
			while (!flag && num < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
				{
					byte[] value = safeBag.ASN1[1].Value;
					if (this.Compare(secret, value))
					{
						flag = true;
					}
				}
				num++;
			}
			if (!flag)
			{
				this._safeBags.Add(new SafeBag("1.2.840.113549.1.12.10.1.5", this.SecretBagSafeBag(secret, attributes)));
				this._secretBagsChanged = true;
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00010B98 File Offset: 0x0000ED98
		public void RemoveSecretBag(byte[] secret)
		{
			int num = -1;
			int num2 = 0;
			while (num == -1 && num2 < this._safeBags.Count)
			{
				SafeBag safeBag = (SafeBag)this._safeBags[num2];
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
				{
					byte[] value = safeBag.ASN1[1].Value;
					if (this.Compare(secret, value))
					{
						num = num2;
					}
				}
				num2++;
			}
			if (num != -1)
			{
				this._safeBags.RemoveAt(num);
				this._secretBagsChanged = true;
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00010C1C File Offset: 0x0000EE1C
		public AsymmetricAlgorithm GetAsymmetricAlgorithm(IDictionary attrs)
		{
			foreach (object obj in this._safeBags)
			{
				SafeBag safeBag = (SafeBag)obj;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1") || safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					ASN1 asn = safeBag.ASN1;
					if (asn.Count == 3)
					{
						ASN1 asn2 = asn[2];
						int num = 0;
						for (int i = 0; i < asn2.Count; i++)
						{
							ASN1 asn3 = asn2[i];
							string text = ASN1Convert.ToOid(asn3[0]);
							ArrayList arrayList = (ArrayList)attrs[text];
							if (arrayList != null)
							{
								ASN1 asn4 = asn3[1];
								if (arrayList.Count == asn4.Count)
								{
									int num2 = 0;
									for (int j = 0; j < asn4.Count; j++)
									{
										ASN1 asn5 = asn4[j];
										byte[] array = (byte[])arrayList[j];
										if (this.Compare(array, asn5.Value))
										{
											num2++;
										}
									}
									if (num2 == asn4.Count)
									{
										num++;
									}
								}
							}
						}
						if (num == asn2.Count)
						{
							ASN1 asn6 = asn[1];
							AsymmetricAlgorithm asymmetricAlgorithm = null;
							if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
							{
								byte[] privateKey = new PKCS8.PrivateKeyInfo(asn6.Value).PrivateKey;
								byte b = privateKey[0];
								if (b != 2)
								{
									if (b == 48)
									{
										asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
									}
								}
								else
								{
									asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
								}
								Array.Clear(privateKey, 0, privateKey.Length);
							}
							else if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
							{
								PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(asn6.Value);
								byte[] array2 = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
								byte[] privateKey2 = new PKCS8.PrivateKeyInfo(array2).PrivateKey;
								byte b = privateKey2[0];
								if (b != 2)
								{
									if (b == 48)
									{
										asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey2);
									}
								}
								else
								{
									asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey2, default(DSAParameters));
								}
								Array.Clear(privateKey2, 0, privateKey2.Length);
								Array.Clear(array2, 0, array2.Length);
							}
							return asymmetricAlgorithm;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00010EB0 File Offset: 0x0000F0B0
		public byte[] GetSecret(IDictionary attrs)
		{
			foreach (object obj in this._safeBags)
			{
				SafeBag safeBag = (SafeBag)obj;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.5"))
				{
					ASN1 asn = safeBag.ASN1;
					if (asn.Count == 3)
					{
						ASN1 asn2 = asn[2];
						int num = 0;
						for (int i = 0; i < asn2.Count; i++)
						{
							ASN1 asn3 = asn2[i];
							string text = ASN1Convert.ToOid(asn3[0]);
							ArrayList arrayList = (ArrayList)attrs[text];
							if (arrayList != null)
							{
								ASN1 asn4 = asn3[1];
								if (arrayList.Count == asn4.Count)
								{
									int num2 = 0;
									for (int j = 0; j < asn4.Count; j++)
									{
										ASN1 asn5 = asn4[j];
										byte[] array = (byte[])arrayList[j];
										if (this.Compare(array, asn5.Value))
										{
											num2++;
										}
									}
									if (num2 == asn4.Count)
									{
										num++;
									}
								}
							}
						}
						if (num == asn2.Count)
						{
							return asn[1].Value;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00011028 File Offset: 0x0000F228
		public X509Certificate GetCertificate(IDictionary attrs)
		{
			foreach (object obj in this._safeBags)
			{
				SafeBag safeBag = (SafeBag)obj;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					ASN1 asn = safeBag.ASN1;
					if (asn.Count == 3)
					{
						ASN1 asn2 = asn[2];
						int num = 0;
						for (int i = 0; i < asn2.Count; i++)
						{
							ASN1 asn3 = asn2[i];
							string text = ASN1Convert.ToOid(asn3[0]);
							ArrayList arrayList = (ArrayList)attrs[text];
							if (arrayList != null)
							{
								ASN1 asn4 = asn3[1];
								if (arrayList.Count == asn4.Count)
								{
									int num2 = 0;
									for (int j = 0; j < asn4.Count; j++)
									{
										ASN1 asn5 = asn4[j];
										byte[] array = (byte[])arrayList[j];
										if (this.Compare(array, asn5.Value))
										{
											num2++;
										}
									}
									if (num2 == asn4.Count)
									{
										num++;
									}
								}
							}
						}
						if (num == asn2.Count)
						{
							return new X509Certificate(new PKCS7.ContentInfo(asn[1].Value).Content[0].Value);
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000111B8 File Offset: 0x0000F3B8
		public IDictionary GetAttributes(AsymmetricAlgorithm aa)
		{
			IDictionary dictionary = new Hashtable();
			foreach (object obj in this._safeBags)
			{
				SafeBag safeBag = (SafeBag)obj;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1") || safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
				{
					ASN1 asn = safeBag.ASN1;
					ASN1 asn2 = asn[1];
					AsymmetricAlgorithm asymmetricAlgorithm = null;
					if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.1"))
					{
						byte[] privateKey = new PKCS8.PrivateKeyInfo(asn2.Value).PrivateKey;
						byte b = privateKey[0];
						if (b != 2)
						{
							if (b == 48)
							{
								asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey);
							}
						}
						else
						{
							asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey, default(DSAParameters));
						}
						Array.Clear(privateKey, 0, privateKey.Length);
					}
					else if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.2"))
					{
						PKCS8.EncryptedPrivateKeyInfo encryptedPrivateKeyInfo = new PKCS8.EncryptedPrivateKeyInfo(asn2.Value);
						byte[] array = this.Decrypt(encryptedPrivateKeyInfo.Algorithm, encryptedPrivateKeyInfo.Salt, encryptedPrivateKeyInfo.IterationCount, encryptedPrivateKeyInfo.EncryptedData);
						byte[] privateKey2 = new PKCS8.PrivateKeyInfo(array).PrivateKey;
						byte b = privateKey2[0];
						if (b != 2)
						{
							if (b == 48)
							{
								asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeRSA(privateKey2);
							}
						}
						else
						{
							asymmetricAlgorithm = PKCS8.PrivateKeyInfo.DecodeDSA(privateKey2, default(DSAParameters));
						}
						Array.Clear(privateKey2, 0, privateKey2.Length);
						Array.Clear(array, 0, array.Length);
					}
					if (asymmetricAlgorithm != null && this.CompareAsymmetricAlgorithm(asymmetricAlgorithm, aa) && asn.Count == 3)
					{
						ASN1 asn3 = asn[2];
						for (int i = 0; i < asn3.Count; i++)
						{
							ASN1 asn4 = asn3[i];
							string text = ASN1Convert.ToOid(asn4[0]);
							ArrayList arrayList = new ArrayList();
							ASN1 asn5 = asn4[1];
							for (int j = 0; j < asn5.Count; j++)
							{
								ASN1 asn6 = asn5[j];
								arrayList.Add(asn6.Value);
							}
							dictionary.Add(text, arrayList);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00011404 File Offset: 0x0000F604
		public IDictionary GetAttributes(X509Certificate cert)
		{
			IDictionary dictionary = new Hashtable();
			foreach (object obj in this._safeBags)
			{
				SafeBag safeBag = (SafeBag)obj;
				if (safeBag.BagOID.Equals("1.2.840.113549.1.12.10.1.3"))
				{
					ASN1 asn = safeBag.ASN1;
					X509Certificate x509Certificate = new X509Certificate(new PKCS7.ContentInfo(asn[1].Value).Content[0].Value);
					if (this.Compare(cert.RawData, x509Certificate.RawData) && asn.Count == 3)
					{
						ASN1 asn2 = asn[2];
						for (int i = 0; i < asn2.Count; i++)
						{
							ASN1 asn3 = asn2[i];
							string text = ASN1Convert.ToOid(asn3[0]);
							ArrayList arrayList = new ArrayList();
							ASN1 asn4 = asn3[1];
							for (int j = 0; j < asn4.Count; j++)
							{
								ASN1 asn5 = asn4[j];
								arrayList.Add(asn5.Value);
							}
							dictionary.Add(text, arrayList);
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00011558 File Offset: 0x0000F758
		public void SaveToFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			using (FileStream fileStream = File.Create(filename))
			{
				byte[] bytes = this.GetBytes();
				fileStream.Write(bytes, 0, bytes.Length);
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x000115A8 File Offset: 0x0000F7A8
		public object Clone()
		{
			PKCS12 pkcs;
			if (this._password != null)
			{
				pkcs = new PKCS12(this.GetBytes(), Encoding.BigEndianUnicode.GetString(this._password));
			}
			else
			{
				pkcs = new PKCS12(this.GetBytes());
			}
			pkcs.IterationCount = this.IterationCount;
			return pkcs;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x000115F6 File Offset: 0x0000F7F6
		// (set) Token: 0x060002A7 RID: 679 RVA: 0x000115FD File Offset: 0x0000F7FD
		public static int MaximumPasswordLength
		{
			get
			{
				return PKCS12.password_max_length;
			}
			set
			{
				if (value < 32)
				{
					throw new ArgumentOutOfRangeException(Locale.GetText("Maximum password length cannot be less than {0}.", new object[] { 32 }));
				}
				PKCS12.password_max_length = value;
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0001162C File Offset: 0x0000F82C
		private static byte[] LoadFile(string filename)
		{
			byte[] array = null;
			using (FileStream fileStream = File.OpenRead(filename))
			{
				array = new byte[fileStream.Length];
				fileStream.Read(array, 0, array.Length);
				fileStream.Close();
			}
			return array;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00011680 File Offset: 0x0000F880
		public static PKCS12 LoadFromFile(string filename)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			return new PKCS12(PKCS12.LoadFile(filename));
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0001169B File Offset: 0x0000F89B
		public static PKCS12 LoadFromFile(string filename, string password)
		{
			if (filename == null)
			{
				throw new ArgumentNullException("filename");
			}
			return new PKCS12(PKCS12.LoadFile(filename), password);
		}

		// Token: 0x040004AF RID: 1199
		public const string pbeWithSHAAnd128BitRC4 = "1.2.840.113549.1.12.1.1";

		// Token: 0x040004B0 RID: 1200
		public const string pbeWithSHAAnd40BitRC4 = "1.2.840.113549.1.12.1.2";

		// Token: 0x040004B1 RID: 1201
		public const string pbeWithSHAAnd3KeyTripleDESCBC = "1.2.840.113549.1.12.1.3";

		// Token: 0x040004B2 RID: 1202
		public const string pbeWithSHAAnd2KeyTripleDESCBC = "1.2.840.113549.1.12.1.4";

		// Token: 0x040004B3 RID: 1203
		public const string pbeWithSHAAnd128BitRC2CBC = "1.2.840.113549.1.12.1.5";

		// Token: 0x040004B4 RID: 1204
		public const string pbeWithSHAAnd40BitRC2CBC = "1.2.840.113549.1.12.1.6";

		// Token: 0x040004B5 RID: 1205
		public const string keyBag = "1.2.840.113549.1.12.10.1.1";

		// Token: 0x040004B6 RID: 1206
		public const string pkcs8ShroudedKeyBag = "1.2.840.113549.1.12.10.1.2";

		// Token: 0x040004B7 RID: 1207
		public const string certBag = "1.2.840.113549.1.12.10.1.3";

		// Token: 0x040004B8 RID: 1208
		public const string crlBag = "1.2.840.113549.1.12.10.1.4";

		// Token: 0x040004B9 RID: 1209
		public const string secretBag = "1.2.840.113549.1.12.10.1.5";

		// Token: 0x040004BA RID: 1210
		public const string safeContentsBag = "1.2.840.113549.1.12.10.1.6";

		// Token: 0x040004BB RID: 1211
		public const string x509Certificate = "1.2.840.113549.1.9.22.1";

		// Token: 0x040004BC RID: 1212
		public const string sdsiCertificate = "1.2.840.113549.1.9.22.2";

		// Token: 0x040004BD RID: 1213
		public const string x509Crl = "1.2.840.113549.1.9.23.1";

		// Token: 0x040004BE RID: 1214
		private const int recommendedIterationCount = 2000;

		// Token: 0x040004BF RID: 1215
		private byte[] _password;

		// Token: 0x040004C0 RID: 1216
		private ArrayList _keyBags;

		// Token: 0x040004C1 RID: 1217
		private ArrayList _secretBags;

		// Token: 0x040004C2 RID: 1218
		private X509CertificateCollection _certs;

		// Token: 0x040004C3 RID: 1219
		private bool _keyBagsChanged;

		// Token: 0x040004C4 RID: 1220
		private bool _secretBagsChanged;

		// Token: 0x040004C5 RID: 1221
		private bool _certsChanged;

		// Token: 0x040004C6 RID: 1222
		private int _iterations;

		// Token: 0x040004C7 RID: 1223
		private ArrayList _safeBags;

		// Token: 0x040004C8 RID: 1224
		private RandomNumberGenerator _rng;

		// Token: 0x040004C9 RID: 1225
		public const int CryptoApiPasswordLimit = 32;

		// Token: 0x040004CA RID: 1226
		private static int password_max_length = int.MaxValue;

		// Token: 0x02000057 RID: 87
		public class DeriveBytes
		{
			// Token: 0x17000071 RID: 113
			// (get) Token: 0x060002AD RID: 685 RVA: 0x000116C3 File Offset: 0x0000F8C3
			// (set) Token: 0x060002AE RID: 686 RVA: 0x000116CB File Offset: 0x0000F8CB
			public string HashName
			{
				get
				{
					return this._hashName;
				}
				set
				{
					this._hashName = value;
				}
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x060002AF RID: 687 RVA: 0x000116D4 File Offset: 0x0000F8D4
			// (set) Token: 0x060002B0 RID: 688 RVA: 0x000116DC File Offset: 0x0000F8DC
			public int IterationCount
			{
				get
				{
					return this._iterations;
				}
				set
				{
					this._iterations = value;
				}
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x060002B1 RID: 689 RVA: 0x000116E5 File Offset: 0x0000F8E5
			// (set) Token: 0x060002B2 RID: 690 RVA: 0x000116F7 File Offset: 0x0000F8F7
			public byte[] Password
			{
				get
				{
					return (byte[])this._password.Clone();
				}
				set
				{
					if (value == null)
					{
						this._password = new byte[0];
						return;
					}
					this._password = (byte[])value.Clone();
				}
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x060002B3 RID: 691 RVA: 0x0001171A File Offset: 0x0000F91A
			// (set) Token: 0x060002B4 RID: 692 RVA: 0x0001172C File Offset: 0x0000F92C
			public byte[] Salt
			{
				get
				{
					return (byte[])this._salt.Clone();
				}
				set
				{
					if (value != null)
					{
						this._salt = (byte[])value.Clone();
						return;
					}
					this._salt = null;
				}
			}

			// Token: 0x060002B5 RID: 693 RVA: 0x0001174C File Offset: 0x0000F94C
			private void Adjust(byte[] a, int aOff, byte[] b)
			{
				int num = (int)((b[b.Length - 1] & byte.MaxValue) + (a[aOff + b.Length - 1] & byte.MaxValue) + 1);
				a[aOff + b.Length - 1] = (byte)num;
				num >>= 8;
				for (int i = b.Length - 2; i >= 0; i--)
				{
					num += (int)((b[i] & byte.MaxValue) + (a[aOff + i] & byte.MaxValue));
					a[aOff + i] = (byte)num;
					num >>= 8;
				}
			}

			// Token: 0x060002B6 RID: 694 RVA: 0x000117BC File Offset: 0x0000F9BC
			private byte[] Derive(byte[] diversifier, int n)
			{
				HashAlgorithm hashAlgorithm = PKCS1.CreateFromName(this._hashName);
				int num = hashAlgorithm.HashSize >> 3;
				int num2 = 64;
				byte[] array = new byte[n];
				byte[] array2;
				if (this._salt != null && this._salt.Length != 0)
				{
					array2 = new byte[num2 * ((this._salt.Length + num2 - 1) / num2)];
					for (int num3 = 0; num3 != array2.Length; num3++)
					{
						array2[num3] = this._salt[num3 % this._salt.Length];
					}
				}
				else
				{
					array2 = new byte[0];
				}
				byte[] array3;
				if (this._password != null && this._password.Length != 0)
				{
					array3 = new byte[num2 * ((this._password.Length + num2 - 1) / num2)];
					for (int num4 = 0; num4 != array3.Length; num4++)
					{
						array3[num4] = this._password[num4 % this._password.Length];
					}
				}
				else
				{
					array3 = new byte[0];
				}
				byte[] array4 = new byte[array2.Length + array3.Length];
				Buffer.BlockCopy(array2, 0, array4, 0, array2.Length);
				Buffer.BlockCopy(array3, 0, array4, array2.Length, array3.Length);
				byte[] array5 = new byte[num2];
				int num5 = (n + num - 1) / num;
				for (int i = 1; i <= num5; i++)
				{
					hashAlgorithm.TransformBlock(diversifier, 0, diversifier.Length, diversifier, 0);
					hashAlgorithm.TransformFinalBlock(array4, 0, array4.Length);
					byte[] array6 = hashAlgorithm.Hash;
					hashAlgorithm.Initialize();
					for (int num6 = 1; num6 != this._iterations; num6++)
					{
						array6 = hashAlgorithm.ComputeHash(array6, 0, array6.Length);
					}
					for (int num7 = 0; num7 != array5.Length; num7++)
					{
						array5[num7] = array6[num7 % array6.Length];
					}
					for (int num8 = 0; num8 != array4.Length / num2; num8++)
					{
						this.Adjust(array4, num8 * num2, array5);
					}
					if (i == num5)
					{
						Buffer.BlockCopy(array6, 0, array, (i - 1) * num, array.Length - (i - 1) * num);
					}
					else
					{
						Buffer.BlockCopy(array6, 0, array, (i - 1) * num, array6.Length);
					}
				}
				return array;
			}

			// Token: 0x060002B7 RID: 695 RVA: 0x000119C1 File Offset: 0x0000FBC1
			public byte[] DeriveKey(int size)
			{
				return this.Derive(PKCS12.DeriveBytes.keyDiversifier, size);
			}

			// Token: 0x060002B8 RID: 696 RVA: 0x000119CF File Offset: 0x0000FBCF
			public byte[] DeriveIV(int size)
			{
				return this.Derive(PKCS12.DeriveBytes.ivDiversifier, size);
			}

			// Token: 0x060002B9 RID: 697 RVA: 0x000119DD File Offset: 0x0000FBDD
			public byte[] DeriveMAC(int size)
			{
				return this.Derive(PKCS12.DeriveBytes.macDiversifier, size);
			}

			// Token: 0x040004CB RID: 1227
			private static byte[] keyDiversifier = new byte[]
			{
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1, 1, 1, 1, 1, 1, 1,
				1, 1, 1, 1
			};

			// Token: 0x040004CC RID: 1228
			private static byte[] ivDiversifier = new byte[]
			{
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2, 2, 2, 2, 2, 2, 2,
				2, 2, 2, 2
			};

			// Token: 0x040004CD RID: 1229
			private static byte[] macDiversifier = new byte[]
			{
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3, 3, 3, 3, 3, 3, 3,
				3, 3, 3, 3
			};

			// Token: 0x040004CE RID: 1230
			private string _hashName;

			// Token: 0x040004CF RID: 1231
			private int _iterations;

			// Token: 0x040004D0 RID: 1232
			private byte[] _password;

			// Token: 0x040004D1 RID: 1233
			private byte[] _salt;

			// Token: 0x02000058 RID: 88
			public enum Purpose
			{
				// Token: 0x040004D3 RID: 1235
				Key,
				// Token: 0x040004D4 RID: 1236
				IV,
				// Token: 0x040004D5 RID: 1237
				MAC
			}
		}
	}
}
