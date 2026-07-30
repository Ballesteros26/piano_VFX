using System;
using System.Security.Cryptography;
using System.Text;
using Mono.Math;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200008D RID: 141
	internal class RSAManaged : RSA
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x00019708 File Offset: 0x00017908
		public RSAManaged()
			: this(1024)
		{
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x00019715 File Offset: 0x00017915
		public RSAManaged(int keySize)
		{
			this.LegalKeySizesValue = new KeySizes[1];
			this.LegalKeySizesValue[0] = new KeySizes(384, 16384, 8);
			base.KeySize = keySize;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00019750 File Offset: 0x00017950
		~RSAManaged()
		{
			this.Dispose(false);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x00019780 File Offset: 0x00017980
		private void GenerateKeyPair()
		{
			int num = this.KeySize + 1 >> 1;
			int num2 = this.KeySize - num;
			this.e = 65537U;
			do
			{
				this.p = BigInteger.GeneratePseudoPrime(num);
			}
			while (this.p % 65537U == 1U);
			for (;;)
			{
				this.q = BigInteger.GeneratePseudoPrime(num2);
				if (this.q % 65537U != 1U && this.p != this.q)
				{
					this.n = this.p * this.q;
					if (this.n.BitCount() == this.KeySize)
					{
						break;
					}
					if (this.p < this.q)
					{
						this.p = this.q;
					}
				}
			}
			BigInteger bigInteger = this.p - 1;
			BigInteger bigInteger2 = this.q - 1;
			BigInteger bigInteger3 = bigInteger * bigInteger2;
			this.d = this.e.ModInverse(bigInteger3);
			this.dp = this.d % bigInteger;
			this.dq = this.d % bigInteger2;
			this.qInv = this.q.ModInverse(this.p);
			this.keypairGenerated = true;
			this.isCRTpossible = true;
			if (this.KeyGenerated != null)
			{
				this.KeyGenerated(this, null);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x000198EC File Offset: 0x00017AEC
		public override int KeySize
		{
			get
			{
				if (this.m_disposed)
				{
					throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
				}
				if (this.keypairGenerated)
				{
					int num = this.n.BitCount();
					if ((num & 7) != 0)
					{
						num += 8 - (num & 7);
					}
					return num;
				}
				return base.KeySize;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0001993A File Offset: 0x00017B3A
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "RSA-PKCS1-KeyEx";
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00019941 File Offset: 0x00017B41
		public bool PublicOnly
		{
			get
			{
				return this.keypairGenerated && (this.d == null || this.n == null);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00019969 File Offset: 0x00017B69
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#rsa-sha1";
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00019970 File Offset: 0x00017B70
		public override byte[] DecryptValue(byte[] rgb)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("private key");
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			BigInteger bigInteger = new BigInteger(rgb);
			BigInteger bigInteger2 = null;
			if (this.keyBlinding)
			{
				bigInteger2 = BigInteger.GenerateRandom(this.n.BitCount());
				bigInteger = bigInteger2.ModPow(this.e, this.n) * bigInteger % this.n;
			}
			BigInteger bigInteger6;
			if (this.isCRTpossible)
			{
				BigInteger bigInteger3 = bigInteger.ModPow(this.dp, this.p);
				BigInteger bigInteger4 = bigInteger.ModPow(this.dq, this.q);
				if (bigInteger4 > bigInteger3)
				{
					BigInteger bigInteger5 = this.p - (bigInteger4 - bigInteger3) * this.qInv % this.p;
					bigInteger6 = bigInteger4 + this.q * bigInteger5;
				}
				else
				{
					BigInteger bigInteger5 = (bigInteger3 - bigInteger4) * this.qInv % this.p;
					bigInteger6 = bigInteger4 + this.q * bigInteger5;
				}
			}
			else
			{
				if (this.PublicOnly)
				{
					throw new CryptographicException(Locale.GetText("Missing private key to decrypt value."));
				}
				bigInteger6 = bigInteger.ModPow(this.d, this.n);
			}
			if (this.keyBlinding)
			{
				bigInteger6 = bigInteger6 * bigInteger2.ModInverse(this.n) % this.n;
				bigInteger2.Clear();
			}
			byte[] paddedValue = this.GetPaddedValue(bigInteger6, this.KeySize >> 3);
			bigInteger.Clear();
			bigInteger6.Clear();
			return paddedValue;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x00019B10 File Offset: 0x00017D10
		public override byte[] EncryptValue(byte[] rgb)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException("public key");
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			BigInteger bigInteger = new BigInteger(rgb);
			BigInteger bigInteger2 = bigInteger.ModPow(this.e, this.n);
			byte[] paddedValue = this.GetPaddedValue(bigInteger2, this.KeySize >> 3);
			bigInteger.Clear();
			bigInteger2.Clear();
			return paddedValue;
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00019B74 File Offset: 0x00017D74
		public override RSAParameters ExportParameters(bool includePrivateParameters)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (!this.keypairGenerated)
			{
				this.GenerateKeyPair();
			}
			RSAParameters rsaparameters = default(RSAParameters);
			rsaparameters.Exponent = this.e.GetBytes();
			rsaparameters.Modulus = this.n.GetBytes();
			if (includePrivateParameters)
			{
				if (this.d == null)
				{
					throw new CryptographicException("Missing private key");
				}
				rsaparameters.D = this.d.GetBytes();
				if (rsaparameters.D.Length != rsaparameters.Modulus.Length)
				{
					byte[] array = new byte[rsaparameters.Modulus.Length];
					Buffer.BlockCopy(rsaparameters.D, 0, array, array.Length - rsaparameters.D.Length, rsaparameters.D.Length);
					rsaparameters.D = array;
				}
				if (this.p != null && this.q != null && this.dp != null && this.dq != null && this.qInv != null)
				{
					int num = this.KeySize >> 4;
					rsaparameters.P = this.GetPaddedValue(this.p, num);
					rsaparameters.Q = this.GetPaddedValue(this.q, num);
					rsaparameters.DP = this.GetPaddedValue(this.dp, num);
					rsaparameters.DQ = this.GetPaddedValue(this.dq, num);
					rsaparameters.InverseQ = this.GetPaddedValue(this.qInv, num);
				}
			}
			return rsaparameters;
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x00019D0C File Offset: 0x00017F0C
		public override void ImportParameters(RSAParameters parameters)
		{
			if (this.m_disposed)
			{
				throw new ObjectDisposedException(Locale.GetText("Keypair was disposed"));
			}
			if (parameters.Exponent == null)
			{
				throw new CryptographicException(Locale.GetText("Missing Exponent"));
			}
			if (parameters.Modulus == null)
			{
				throw new CryptographicException(Locale.GetText("Missing Modulus"));
			}
			this.e = new BigInteger(parameters.Exponent);
			this.n = new BigInteger(parameters.Modulus);
			this.d = (this.dp = (this.dq = (this.qInv = (this.p = (this.q = null)))));
			if (parameters.D != null)
			{
				this.d = new BigInteger(parameters.D);
			}
			if (parameters.DP != null)
			{
				this.dp = new BigInteger(parameters.DP);
			}
			if (parameters.DQ != null)
			{
				this.dq = new BigInteger(parameters.DQ);
			}
			if (parameters.InverseQ != null)
			{
				this.qInv = new BigInteger(parameters.InverseQ);
			}
			if (parameters.P != null)
			{
				this.p = new BigInteger(parameters.P);
			}
			if (parameters.Q != null)
			{
				this.q = new BigInteger(parameters.Q);
			}
			this.keypairGenerated = true;
			bool flag = this.p != null && this.q != null && this.dp != null;
			this.isCRTpossible = flag && this.dq != null && this.qInv != null;
			if (!flag)
			{
				return;
			}
			bool flag2 = this.n == this.p * this.q;
			if (flag2)
			{
				BigInteger bigInteger = this.p - 1;
				BigInteger bigInteger2 = this.q - 1;
				BigInteger bigInteger3 = bigInteger * bigInteger2;
				BigInteger bigInteger4 = this.e.ModInverse(bigInteger3);
				flag2 = this.d == bigInteger4;
				if (!flag2 && this.isCRTpossible)
				{
					flag2 = this.dp == bigInteger4 % bigInteger && this.dq == bigInteger4 % bigInteger2 && this.qInv == this.q.ModInverse(this.p);
				}
			}
			if (!flag2)
			{
				throw new CryptographicException(Locale.GetText("Private/public key mismatch"));
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00019F84 File Offset: 0x00018184
		protected override void Dispose(bool disposing)
		{
			if (!this.m_disposed)
			{
				if (this.d != null)
				{
					this.d.Clear();
					this.d = null;
				}
				if (this.p != null)
				{
					this.p.Clear();
					this.p = null;
				}
				if (this.q != null)
				{
					this.q.Clear();
					this.q = null;
				}
				if (this.dp != null)
				{
					this.dp.Clear();
					this.dp = null;
				}
				if (this.dq != null)
				{
					this.dq.Clear();
					this.dq = null;
				}
				if (this.qInv != null)
				{
					this.qInv.Clear();
					this.qInv = null;
				}
				if (disposing)
				{
					if (this.e != null)
					{
						this.e.Clear();
						this.e = null;
					}
					if (this.n != null)
					{
						this.n.Clear();
						this.n = null;
					}
				}
			}
			this.m_disposed = true;
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000480 RID: 1152 RVA: 0x0001A0A8 File Offset: 0x000182A8
		// (remove) Token: 0x06000481 RID: 1153 RVA: 0x0001A0E0 File Offset: 0x000182E0
		public event RSAManaged.KeyGeneratedEventHandler KeyGenerated;

		// Token: 0x06000482 RID: 1154 RVA: 0x0001A118 File Offset: 0x00018318
		public override string ToXmlString(bool includePrivateParameters)
		{
			StringBuilder stringBuilder = new StringBuilder();
			RSAParameters rsaparameters = this.ExportParameters(includePrivateParameters);
			try
			{
				stringBuilder.Append("<RSAKeyValue>");
				stringBuilder.Append("<Modulus>");
				stringBuilder.Append(Convert.ToBase64String(rsaparameters.Modulus));
				stringBuilder.Append("</Modulus>");
				stringBuilder.Append("<Exponent>");
				stringBuilder.Append(Convert.ToBase64String(rsaparameters.Exponent));
				stringBuilder.Append("</Exponent>");
				if (includePrivateParameters)
				{
					if (rsaparameters.P != null)
					{
						stringBuilder.Append("<P>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.P));
						stringBuilder.Append("</P>");
					}
					if (rsaparameters.Q != null)
					{
						stringBuilder.Append("<Q>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.Q));
						stringBuilder.Append("</Q>");
					}
					if (rsaparameters.DP != null)
					{
						stringBuilder.Append("<DP>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.DP));
						stringBuilder.Append("</DP>");
					}
					if (rsaparameters.DQ != null)
					{
						stringBuilder.Append("<DQ>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.DQ));
						stringBuilder.Append("</DQ>");
					}
					if (rsaparameters.InverseQ != null)
					{
						stringBuilder.Append("<InverseQ>");
						stringBuilder.Append(Convert.ToBase64String(rsaparameters.InverseQ));
						stringBuilder.Append("</InverseQ>");
					}
					stringBuilder.Append("<D>");
					stringBuilder.Append(Convert.ToBase64String(rsaparameters.D));
					stringBuilder.Append("</D>");
				}
				stringBuilder.Append("</RSAKeyValue>");
			}
			catch
			{
				if (rsaparameters.P != null)
				{
					Array.Clear(rsaparameters.P, 0, rsaparameters.P.Length);
				}
				if (rsaparameters.Q != null)
				{
					Array.Clear(rsaparameters.Q, 0, rsaparameters.Q.Length);
				}
				if (rsaparameters.DP != null)
				{
					Array.Clear(rsaparameters.DP, 0, rsaparameters.DP.Length);
				}
				if (rsaparameters.DQ != null)
				{
					Array.Clear(rsaparameters.DQ, 0, rsaparameters.DQ.Length);
				}
				if (rsaparameters.InverseQ != null)
				{
					Array.Clear(rsaparameters.InverseQ, 0, rsaparameters.InverseQ.Length);
				}
				if (rsaparameters.D != null)
				{
					Array.Clear(rsaparameters.D, 0, rsaparameters.D.Length);
				}
				throw;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0001A39C File Offset: 0x0001859C
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0001A3A4 File Offset: 0x000185A4
		public bool UseKeyBlinding
		{
			get
			{
				return this.keyBlinding;
			}
			set
			{
				this.keyBlinding = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0001A3AD File Offset: 0x000185AD
		public bool IsCrtPossible
		{
			get
			{
				return !this.keypairGenerated || this.isCRTpossible;
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001A3C0 File Offset: 0x000185C0
		private byte[] GetPaddedValue(BigInteger value, int length)
		{
			byte[] bytes = value.GetBytes();
			if (bytes.Length >= length)
			{
				return bytes;
			}
			byte[] array = new byte[length];
			Buffer.BlockCopy(bytes, 0, array, length - bytes.Length, bytes.Length);
			Array.Clear(bytes, 0, bytes.Length);
			return array;
		}

		// Token: 0x04000587 RID: 1415
		private const int defaultKeySize = 1024;

		// Token: 0x04000588 RID: 1416
		private bool isCRTpossible;

		// Token: 0x04000589 RID: 1417
		private bool keyBlinding = true;

		// Token: 0x0400058A RID: 1418
		private bool keypairGenerated;

		// Token: 0x0400058B RID: 1419
		private bool m_disposed;

		// Token: 0x0400058C RID: 1420
		private BigInteger d;

		// Token: 0x0400058D RID: 1421
		private BigInteger p;

		// Token: 0x0400058E RID: 1422
		private BigInteger q;

		// Token: 0x0400058F RID: 1423
		private BigInteger dp;

		// Token: 0x04000590 RID: 1424
		private BigInteger dq;

		// Token: 0x04000591 RID: 1425
		private BigInteger qInv;

		// Token: 0x04000592 RID: 1426
		private BigInteger n;

		// Token: 0x04000593 RID: 1427
		private BigInteger e;

		// Token: 0x0200008E RID: 142
		// (Invoke) Token: 0x06000488 RID: 1160
		public delegate void KeyGeneratedEventHandler(object sender, EventArgs e);
	}
}
