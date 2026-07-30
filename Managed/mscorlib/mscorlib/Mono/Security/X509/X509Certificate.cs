using System;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using Mono.Security.Cryptography;

namespace Mono.Security.X509
{
	// Token: 0x0200005C RID: 92
	internal class X509Certificate : ISerializable
	{
		// Token: 0x060002E8 RID: 744 RVA: 0x00012E2C File Offset: 0x0001102C
		private void Parse(byte[] data)
		{
			try
			{
				this.decoder = new ASN1(data);
				if (this.decoder.Tag != 48)
				{
					throw new CryptographicException(X509Certificate.encoding_error);
				}
				if (this.decoder[0].Tag != 48)
				{
					throw new CryptographicException(X509Certificate.encoding_error);
				}
				ASN1 asn = this.decoder[0];
				int num = 0;
				ASN1 asn2 = this.decoder[0][num];
				this.version = 1;
				if (asn2.Tag == 160 && asn2.Count > 0)
				{
					this.version += (int)asn2[0].Value[0];
					num++;
				}
				ASN1 asn3 = this.decoder[0][num++];
				if (asn3.Tag != 2)
				{
					throw new CryptographicException(X509Certificate.encoding_error);
				}
				this.serialnumber = asn3.Value;
				Array.Reverse<byte>(this.serialnumber, 0, this.serialnumber.Length);
				num++;
				this.issuer = asn.Element(num++, 48);
				this.m_issuername = X501.ToString(this.issuer);
				ASN1 asn4 = asn.Element(num++, 48);
				ASN1 asn5 = asn4[0];
				this.m_from = ASN1Convert.ToDateTime(asn5);
				ASN1 asn6 = asn4[1];
				this.m_until = ASN1Convert.ToDateTime(asn6);
				this.subject = asn.Element(num++, 48);
				this.m_subject = X501.ToString(this.subject);
				ASN1 asn7 = asn.Element(num++, 48);
				ASN1 asn8 = asn7.Element(0, 48);
				ASN1 asn9 = asn8.Element(0, 6);
				this.m_keyalgo = ASN1Convert.ToOid(asn9);
				ASN1 asn10 = asn8[1];
				this.m_keyalgoparams = ((asn8.Count > 1) ? asn10.GetBytes() : null);
				ASN1 asn11 = asn7.Element(1, 3);
				int num2 = asn11.Length - 1;
				this.m_publickey = new byte[num2];
				Buffer.BlockCopy(asn11.Value, 1, this.m_publickey, 0, num2);
				byte[] value = this.decoder[2].Value;
				this.signature = new byte[value.Length - 1];
				Buffer.BlockCopy(value, 1, this.signature, 0, this.signature.Length);
				asn8 = this.decoder[1];
				asn9 = asn8.Element(0, 6);
				this.m_signaturealgo = ASN1Convert.ToOid(asn9);
				asn10 = asn8[1];
				if (asn10 != null)
				{
					this.m_signaturealgoparams = asn10.GetBytes();
				}
				else
				{
					this.m_signaturealgoparams = null;
				}
				ASN1 asn12 = asn.Element(num, 129);
				if (asn12 != null)
				{
					num++;
					this.issuerUniqueID = asn12.Value;
				}
				ASN1 asn13 = asn.Element(num, 130);
				if (asn13 != null)
				{
					num++;
					this.subjectUniqueID = asn13.Value;
				}
				ASN1 asn14 = asn.Element(num, 163);
				if (asn14 != null && asn14.Count == 1)
				{
					this.extensions = new X509ExtensionCollection(asn14[0]);
				}
				else
				{
					this.extensions = new X509ExtensionCollection(null);
				}
				this.m_encodedcert = (byte[])data.Clone();
			}
			catch (Exception ex)
			{
				throw new CryptographicException(X509Certificate.encoding_error, ex);
			}
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00013178 File Offset: 0x00011378
		public X509Certificate(byte[] data)
		{
			if (data != null)
			{
				if (data.Length != 0 && data[0] != 48)
				{
					try
					{
						data = X509Certificate.PEM("CERTIFICATE", data);
					}
					catch (Exception ex)
					{
						throw new CryptographicException(X509Certificate.encoding_error, ex);
					}
				}
				this.Parse(data);
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000131CC File Offset: 0x000113CC
		private byte[] GetUnsignedBigInteger(byte[] integer)
		{
			if (integer[0] == 0)
			{
				int num = integer.Length - 1;
				byte[] array = new byte[num];
				Buffer.BlockCopy(integer, 1, array, 0, num);
				return array;
			}
			return integer;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002EB RID: 747 RVA: 0x000131F8 File Offset: 0x000113F8
		// (set) Token: 0x060002EC RID: 748 RVA: 0x00013332 File Offset: 0x00011532
		public DSA DSA
		{
			get
			{
				if (this.m_keyalgoparams == null)
				{
					throw new CryptographicException("Missing key algorithm parameters.");
				}
				if (this._dsa == null && this.m_keyalgo == "1.2.840.10040.4.1")
				{
					DSAParameters dsaparameters = default(DSAParameters);
					ASN1 asn = new ASN1(this.m_publickey);
					if (asn == null || asn.Tag != 2)
					{
						return null;
					}
					dsaparameters.Y = this.GetUnsignedBigInteger(asn.Value);
					ASN1 asn2 = new ASN1(this.m_keyalgoparams);
					if (asn2 == null || asn2.Tag != 48 || asn2.Count < 3)
					{
						return null;
					}
					if (asn2[0].Tag != 2 || asn2[1].Tag != 2 || asn2[2].Tag != 2)
					{
						return null;
					}
					dsaparameters.P = this.GetUnsignedBigInteger(asn2[0].Value);
					dsaparameters.Q = this.GetUnsignedBigInteger(asn2[1].Value);
					dsaparameters.G = this.GetUnsignedBigInteger(asn2[2].Value);
					this._dsa = new DSACryptoServiceProvider(dsaparameters.Y.Length << 3);
					this._dsa.ImportParameters(dsaparameters);
				}
				return this._dsa;
			}
			set
			{
				this._dsa = value;
				if (value != null)
				{
					this._rsa = null;
				}
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00013345 File Offset: 0x00011545
		public X509ExtensionCollection Extensions
		{
			get
			{
				return this.extensions;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00013350 File Offset: 0x00011550
		public byte[] Hash
		{
			get
			{
				if (this.certhash == null)
				{
					if (this.decoder == null || this.decoder.Count < 1)
					{
						return null;
					}
					string text = PKCS1.HashNameFromOid(this.m_signaturealgo, false);
					if (text == null)
					{
						return null;
					}
					byte[] bytes = this.decoder[0].GetBytes();
					using (HashAlgorithm hashAlgorithm = PKCS1.CreateFromName(text))
					{
						this.certhash = hashAlgorithm.ComputeHash(bytes, 0, bytes.Length);
					}
				}
				return (byte[])this.certhash.Clone();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002EF RID: 751 RVA: 0x000133E8 File Offset: 0x000115E8
		public virtual string IssuerName
		{
			get
			{
				return this.m_issuername;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x000133F0 File Offset: 0x000115F0
		public virtual string KeyAlgorithm
		{
			get
			{
				return this.m_keyalgo;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x000133F8 File Offset: 0x000115F8
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00013414 File Offset: 0x00011614
		public virtual byte[] KeyAlgorithmParameters
		{
			get
			{
				if (this.m_keyalgoparams == null)
				{
					return null;
				}
				return (byte[])this.m_keyalgoparams.Clone();
			}
			set
			{
				this.m_keyalgoparams = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0001341D File Offset: 0x0001161D
		public virtual byte[] PublicKey
		{
			get
			{
				if (this.m_publickey == null)
				{
					return null;
				}
				return (byte[])this.m_publickey.Clone();
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0001343C File Offset: 0x0001163C
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x000134F1 File Offset: 0x000116F1
		public virtual RSA RSA
		{
			get
			{
				if (this._rsa == null && this.m_keyalgo == "1.2.840.113549.1.1.1")
				{
					RSAParameters rsaparameters = default(RSAParameters);
					ASN1 asn = new ASN1(this.m_publickey);
					ASN1 asn2 = asn[0];
					if (asn2 == null || asn2.Tag != 2)
					{
						return null;
					}
					ASN1 asn3 = asn[1];
					if (asn3.Tag != 2)
					{
						return null;
					}
					rsaparameters.Modulus = this.GetUnsignedBigInteger(asn2.Value);
					rsaparameters.Exponent = asn3.Value;
					int num = rsaparameters.Modulus.Length << 3;
					this._rsa = new RSACryptoServiceProvider(num);
					this._rsa.ImportParameters(rsaparameters);
				}
				return this._rsa;
			}
			set
			{
				if (value != null)
				{
					this._dsa = null;
				}
				this._rsa = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00013504 File Offset: 0x00011704
		public virtual byte[] RawData
		{
			get
			{
				if (this.m_encodedcert == null)
				{
					return null;
				}
				return (byte[])this.m_encodedcert.Clone();
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x00013520 File Offset: 0x00011720
		public virtual byte[] SerialNumber
		{
			get
			{
				if (this.serialnumber == null)
				{
					return null;
				}
				return (byte[])this.serialnumber.Clone();
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0001353C File Offset: 0x0001173C
		public virtual byte[] Signature
		{
			get
			{
				if (this.signature == null)
				{
					return null;
				}
				string signaturealgo = this.m_signaturealgo;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(signaturealgo);
				if (num <= 719034781U)
				{
					if (num <= 601591448U)
					{
						if (num != 510574318U)
						{
							if (num != 601591448U)
							{
								goto IL_021C;
							}
							if (!(signaturealgo == "1.2.840.113549.1.1.5"))
							{
								goto IL_021C;
							}
						}
						else
						{
							if (!(signaturealgo == "1.2.840.10040.4.3"))
							{
								goto IL_021C;
							}
							ASN1 asn = new ASN1(this.signature);
							if (asn == null || asn.Count != 2)
							{
								return null;
							}
							byte[] value = asn[0].Value;
							byte[] value2 = asn[1].Value;
							byte[] array = new byte[40];
							int num2 = Math.Max(0, value.Length - 20);
							int num3 = Math.Max(0, 20 - value.Length);
							Buffer.BlockCopy(value, num2, array, num3, value.Length - num2);
							int num4 = Math.Max(0, value2.Length - 20);
							int num5 = Math.Max(20, 40 - value2.Length);
							Buffer.BlockCopy(value2, num4, array, num5, value2.Length - num4);
							return array;
						}
					}
					else if (num != 618369067U)
					{
						if (num != 702257162U)
						{
							if (num != 719034781U)
							{
								goto IL_021C;
							}
							if (!(signaturealgo == "1.2.840.113549.1.1.2"))
							{
								goto IL_021C;
							}
						}
						else if (!(signaturealgo == "1.2.840.113549.1.1.3"))
						{
							goto IL_021C;
						}
					}
					else if (!(signaturealgo == "1.2.840.113549.1.1.4"))
					{
						goto IL_021C;
					}
				}
				else if (num <= 2477476687U)
				{
					if (num != 875536856U)
					{
						if (num != 2477476687U)
						{
							goto IL_021C;
						}
						if (!(signaturealgo == "1.2.840.113549.1.1.11"))
						{
							goto IL_021C;
						}
					}
					else if (!(signaturealgo == "1.3.14.3.2.29"))
					{
						goto IL_021C;
					}
				}
				else if (num != 2494254306U)
				{
					if (num != 2511031925U)
					{
						if (num != 3493391575U)
						{
							goto IL_021C;
						}
						if (!(signaturealgo == "1.3.36.3.3.1.2"))
						{
							goto IL_021C;
						}
					}
					else if (!(signaturealgo == "1.2.840.113549.1.1.13"))
					{
						goto IL_021C;
					}
				}
				else if (!(signaturealgo == "1.2.840.113549.1.1.12"))
				{
					goto IL_021C;
				}
				return (byte[])this.signature.Clone();
				IL_021C:
				throw new CryptographicException("Unsupported hash algorithm: " + this.m_signaturealgo);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0001377A File Offset: 0x0001197A
		public virtual string SignatureAlgorithm
		{
			get
			{
				return this.m_signaturealgo;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00013782 File Offset: 0x00011982
		public virtual byte[] SignatureAlgorithmParameters
		{
			get
			{
				if (this.m_signaturealgoparams == null)
				{
					return this.m_signaturealgoparams;
				}
				return (byte[])this.m_signaturealgoparams.Clone();
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000137A3 File Offset: 0x000119A3
		public virtual string SubjectName
		{
			get
			{
				return this.m_subject;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000137AB File Offset: 0x000119AB
		public virtual DateTime ValidFrom
		{
			get
			{
				return this.m_from;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002FD RID: 765 RVA: 0x000137B3 File Offset: 0x000119B3
		public virtual DateTime ValidUntil
		{
			get
			{
				return this.m_until;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002FE RID: 766 RVA: 0x000137BB File Offset: 0x000119BB
		public int Version
		{
			get
			{
				return this.version;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002FF RID: 767 RVA: 0x000137C3 File Offset: 0x000119C3
		public bool IsCurrent
		{
			get
			{
				return this.WasCurrent(DateTime.UtcNow);
			}
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000137D0 File Offset: 0x000119D0
		public bool WasCurrent(DateTime instant)
		{
			return instant > this.ValidFrom && instant <= this.ValidUntil;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000301 RID: 769 RVA: 0x000137EE File Offset: 0x000119EE
		public byte[] IssuerUniqueIdentifier
		{
			get
			{
				if (this.issuerUniqueID == null)
				{
					return null;
				}
				return (byte[])this.issuerUniqueID.Clone();
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0001380A File Offset: 0x00011A0A
		public byte[] SubjectUniqueIdentifier
		{
			get
			{
				if (this.subjectUniqueID == null)
				{
					return null;
				}
				return (byte[])this.subjectUniqueID.Clone();
			}
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00013826 File Offset: 0x00011A26
		internal bool VerifySignature(DSA dsa)
		{
			DSASignatureDeformatter dsasignatureDeformatter = new DSASignatureDeformatter(dsa);
			dsasignatureDeformatter.SetHashAlgorithm("SHA1");
			return dsasignatureDeformatter.VerifySignature(this.Hash, this.Signature);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0001384A File Offset: 0x00011A4A
		internal bool VerifySignature(RSA rsa)
		{
			if (this.m_signaturealgo == "1.2.840.10040.4.3")
			{
				return false;
			}
			RSAPKCS1SignatureDeformatter rsapkcs1SignatureDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
			rsapkcs1SignatureDeformatter.SetHashAlgorithm(PKCS1.HashNameFromOid(this.m_signaturealgo, true));
			return rsapkcs1SignatureDeformatter.VerifySignature(this.Hash, this.Signature);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001388C File Offset: 0x00011A8C
		public bool VerifySignature(AsymmetricAlgorithm aa)
		{
			if (aa == null)
			{
				throw new ArgumentNullException("aa");
			}
			if (aa is RSA)
			{
				return this.VerifySignature(aa as RSA);
			}
			if (aa is DSA)
			{
				return this.VerifySignature(aa as DSA);
			}
			throw new NotSupportedException("Unknown Asymmetric Algorithm " + aa.ToString());
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000138E6 File Offset: 0x00011AE6
		public bool CheckSignature(byte[] hash, string hashAlgorithm, byte[] signature)
		{
			return ((RSACryptoServiceProvider)this.RSA).VerifyHash(hash, hashAlgorithm, signature);
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000307 RID: 775 RVA: 0x000138FC File Offset: 0x00011AFC
		public bool IsSelfSigned
		{
			get
			{
				if (this.m_issuername != this.m_subject)
				{
					return false;
				}
				bool flag;
				try
				{
					if (this.RSA != null)
					{
						flag = this.VerifySignature(this.RSA);
					}
					else if (this.DSA != null)
					{
						flag = this.VerifySignature(this.DSA);
					}
					else
					{
						flag = false;
					}
				}
				catch (CryptographicException)
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00013968 File Offset: 0x00011B68
		public ASN1 GetIssuerName()
		{
			return this.issuer;
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00013970 File Offset: 0x00011B70
		public ASN1 GetSubjectName()
		{
			return this.subject;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00013978 File Offset: 0x00011B78
		protected X509Certificate(SerializationInfo info, StreamingContext context)
		{
			this.Parse((byte[])info.GetValue("raw", typeof(byte[])));
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000139A0 File Offset: 0x00011BA0
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("raw", this.m_encodedcert);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000139B4 File Offset: 0x00011BB4
		private static byte[] PEM(string type, byte[] data)
		{
			string @string = Encoding.ASCII.GetString(data);
			string text = string.Format("-----BEGIN {0}-----", type);
			string text2 = string.Format("-----END {0}-----", type);
			int num = @string.IndexOf(text) + text.Length;
			int num2 = @string.IndexOf(text2, num);
			return Convert.FromBase64String(@string.Substring(num, num2 - num));
		}

		// Token: 0x040004F2 RID: 1266
		private ASN1 decoder;

		// Token: 0x040004F3 RID: 1267
		private byte[] m_encodedcert;

		// Token: 0x040004F4 RID: 1268
		private DateTime m_from;

		// Token: 0x040004F5 RID: 1269
		private DateTime m_until;

		// Token: 0x040004F6 RID: 1270
		private ASN1 issuer;

		// Token: 0x040004F7 RID: 1271
		private string m_issuername;

		// Token: 0x040004F8 RID: 1272
		private string m_keyalgo;

		// Token: 0x040004F9 RID: 1273
		private byte[] m_keyalgoparams;

		// Token: 0x040004FA RID: 1274
		private ASN1 subject;

		// Token: 0x040004FB RID: 1275
		private string m_subject;

		// Token: 0x040004FC RID: 1276
		private byte[] m_publickey;

		// Token: 0x040004FD RID: 1277
		private byte[] signature;

		// Token: 0x040004FE RID: 1278
		private string m_signaturealgo;

		// Token: 0x040004FF RID: 1279
		private byte[] m_signaturealgoparams;

		// Token: 0x04000500 RID: 1280
		private byte[] certhash;

		// Token: 0x04000501 RID: 1281
		private RSA _rsa;

		// Token: 0x04000502 RID: 1282
		private DSA _dsa;

		// Token: 0x04000503 RID: 1283
		private const string OID_DSA = "1.2.840.10040.4.1";

		// Token: 0x04000504 RID: 1284
		private const string OID_RSA = "1.2.840.113549.1.1.1";

		// Token: 0x04000505 RID: 1285
		private int version;

		// Token: 0x04000506 RID: 1286
		private byte[] serialnumber;

		// Token: 0x04000507 RID: 1287
		private byte[] issuerUniqueID;

		// Token: 0x04000508 RID: 1288
		private byte[] subjectUniqueID;

		// Token: 0x04000509 RID: 1289
		private X509ExtensionCollection extensions;

		// Token: 0x0400050A RID: 1290
		private static string encoding_error = Locale.GetText("Input data cannot be coded as a valid certificate.");
	}
}
