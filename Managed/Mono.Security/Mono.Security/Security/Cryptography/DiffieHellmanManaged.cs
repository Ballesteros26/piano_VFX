using System;
using System.Security.Cryptography;
using Mono.Math;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000093 RID: 147
	public sealed class DiffieHellmanManaged : DiffieHellman
	{
		// Token: 0x0600054A RID: 1354 RVA: 0x00018BD4 File Offset: 0x00016DD4
		public DiffieHellmanManaged()
			: this(1024, 160, DHKeyGeneration.Static)
		{
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x00018BE8 File Offset: 0x00016DE8
		public DiffieHellmanManaged(int bitLength, int l, DHKeyGeneration method)
		{
			if (bitLength < 256 || l < 0)
			{
				throw new ArgumentException();
			}
			BigInteger bigInteger;
			BigInteger bigInteger2;
			this.GenerateKey(bitLength, method, out bigInteger, out bigInteger2);
			this.Initialize(bigInteger, bigInteger2, null, l, false);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00018C24 File Offset: 0x00016E24
		public DiffieHellmanManaged(byte[] p, byte[] g, byte[] x)
		{
			if (p == null || g == null)
			{
				throw new ArgumentNullException();
			}
			if (x == null)
			{
				this.Initialize(new BigInteger(p), new BigInteger(g), null, 0, true);
				return;
			}
			this.Initialize(new BigInteger(p), new BigInteger(g), new BigInteger(x), 0, true);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00018C76 File Offset: 0x00016E76
		public DiffieHellmanManaged(byte[] p, byte[] g, int l)
		{
			if (p == null || g == null)
			{
				throw new ArgumentNullException();
			}
			if (l < 0)
			{
				throw new ArgumentException();
			}
			this.Initialize(new BigInteger(p), new BigInteger(g), null, l, true);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00018CAC File Offset: 0x00016EAC
		private void Initialize(BigInteger p, BigInteger g, BigInteger x, int secretLen, bool checkInput)
		{
			if (checkInput && (!p.IsProbablePrime() || g <= 0 || g >= p || (x != null && (x <= 0 || x > p - 2))))
			{
				throw new CryptographicException();
			}
			if (secretLen == 0)
			{
				secretLen = p.BitCount();
			}
			this.m_P = p;
			this.m_G = g;
			if (x == null)
			{
				BigInteger bigInteger = this.m_P - 1;
				this.m_X = BigInteger.GenerateRandom(secretLen);
				while (this.m_X >= bigInteger || this.m_X == 0U)
				{
					this.m_X = BigInteger.GenerateRandom(secretLen);
				}
				return;
			}
			this.m_X = x;
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00018D84 File Offset: 0x00016F84
		public override byte[] CreateKeyExchange()
		{
			BigInteger bigInteger = this.m_G.ModPow(this.m_X, this.m_P);
			byte[] bytes = bigInteger.GetBytes();
			bigInteger.Clear();
			return bytes;
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00018DB8 File Offset: 0x00016FB8
		public override byte[] DecryptKeyExchange(byte[] keyEx)
		{
			BigInteger bigInteger = new BigInteger(keyEx).ModPow(this.m_X, this.m_P);
			byte[] bytes = bigInteger.GetBytes();
			bigInteger.Clear();
			return bytes;
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x00018DE9 File Offset: 0x00016FE9
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return "1.2.840.113549.1.3";
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x00018DF0 File Offset: 0x00016FF0
		public override string SignatureAlgorithm
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00018DF4 File Offset: 0x00016FF4
		protected override void Dispose(bool disposing)
		{
			if (!this.m_Disposed)
			{
				if (this.m_P != null)
				{
					this.m_P.Clear();
				}
				if (this.m_G != null)
				{
					this.m_G.Clear();
				}
				if (this.m_X != null)
				{
					this.m_X.Clear();
				}
			}
			this.m_Disposed = true;
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00018E5C File Offset: 0x0001705C
		public override DHParameters ExportParameters(bool includePrivateParameters)
		{
			DHParameters dhparameters = default(DHParameters);
			dhparameters.P = this.m_P.GetBytes();
			dhparameters.G = this.m_G.GetBytes();
			if (includePrivateParameters)
			{
				dhparameters.X = this.m_X.GetBytes();
			}
			return dhparameters;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00018EAC File Offset: 0x000170AC
		public override void ImportParameters(DHParameters parameters)
		{
			if (parameters.P == null)
			{
				throw new CryptographicException("Missing P value.");
			}
			if (parameters.G == null)
			{
				throw new CryptographicException("Missing G value.");
			}
			BigInteger bigInteger = new BigInteger(parameters.P);
			BigInteger bigInteger2 = new BigInteger(parameters.G);
			BigInteger bigInteger3 = null;
			if (parameters.X != null)
			{
				bigInteger3 = new BigInteger(parameters.X);
			}
			this.Initialize(bigInteger, bigInteger2, bigInteger3, 0, true);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00018F18 File Offset: 0x00017118
		~DiffieHellmanManaged()
		{
			this.Dispose(false);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00018F48 File Offset: 0x00017148
		private void GenerateKey(int bitlen, DHKeyGeneration keygen, out BigInteger p, out BigInteger g)
		{
			if (keygen == DHKeyGeneration.Static)
			{
				if (bitlen == 768)
				{
					p = new BigInteger(DiffieHellmanManaged.m_OAKLEY768);
				}
				else if (bitlen == 1024)
				{
					p = new BigInteger(DiffieHellmanManaged.m_OAKLEY1024);
				}
				else
				{
					if (bitlen != 1536)
					{
						throw new ArgumentException("Invalid bit size.");
					}
					p = new BigInteger(DiffieHellmanManaged.m_OAKLEY1536);
				}
				g = new BigInteger(22U);
				return;
			}
			p = BigInteger.GeneratePseudoPrime(bitlen);
			g = new BigInteger(3U);
		}

		// Token: 0x040003A5 RID: 933
		private BigInteger m_P;

		// Token: 0x040003A6 RID: 934
		private BigInteger m_G;

		// Token: 0x040003A7 RID: 935
		private BigInteger m_X;

		// Token: 0x040003A8 RID: 936
		private bool m_Disposed;

		// Token: 0x040003A9 RID: 937
		private static byte[] m_OAKLEY768 = new byte[]
		{
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 201, 15,
			218, 162, 33, 104, 194, 52, 196, 198, 98, 139,
			128, 220, 28, 209, 41, 2, 78, 8, 138, 103,
			204, 116, 2, 11, 190, 166, 59, 19, 155, 34,
			81, 74, 8, 121, 142, 52, 4, 221, 239, 149,
			25, 179, 205, 58, 67, 27, 48, 43, 10, 109,
			242, 95, 20, 55, 79, 225, 53, 109, 109, 81,
			194, 69, 228, 133, 181, 118, 98, 94, 126, 198,
			244, 76, 66, 233, 166, 58, 54, 32, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue
		};

		// Token: 0x040003AA RID: 938
		private static byte[] m_OAKLEY1024 = new byte[]
		{
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 201, 15,
			218, 162, 33, 104, 194, 52, 196, 198, 98, 139,
			128, 220, 28, 209, 41, 2, 78, 8, 138, 103,
			204, 116, 2, 11, 190, 166, 59, 19, 155, 34,
			81, 74, 8, 121, 142, 52, 4, 221, 239, 149,
			25, 179, 205, 58, 67, 27, 48, 43, 10, 109,
			242, 95, 20, 55, 79, 225, 53, 109, 109, 81,
			194, 69, 228, 133, 181, 118, 98, 94, 126, 198,
			244, 76, 66, 233, 166, 55, 237, 107, 11, byte.MaxValue,
			92, 182, 244, 6, 183, 237, 238, 56, 107, 251,
			90, 137, 159, 165, 174, 159, 36, 17, 124, 75,
			31, 230, 73, 40, 102, 81, 236, 230, 83, 129,
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue
		};

		// Token: 0x040003AB RID: 939
		private static byte[] m_OAKLEY1536 = new byte[]
		{
			byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, 201, 15,
			218, 162, 33, 104, 194, 52, 196, 198, 98, 139,
			128, 220, 28, 209, 41, 2, 78, 8, 138, 103,
			204, 116, 2, 11, 190, 166, 59, 19, 155, 34,
			81, 74, 8, 121, 142, 52, 4, 221, 239, 149,
			25, 179, 205, 58, 67, 27, 48, 43, 10, 109,
			242, 95, 20, 55, 79, 225, 53, 109, 109, 81,
			194, 69, 228, 133, 181, 118, 98, 94, 126, 198,
			244, 76, 66, 233, 166, 55, 237, 107, 11, byte.MaxValue,
			92, 182, 244, 6, 183, 237, 238, 56, 107, 251,
			90, 137, 159, 165, 174, 159, 36, 17, 124, 75,
			31, 230, 73, 40, 102, 81, 236, 228, 91, 61,
			194, 0, 124, 184, 161, 99, 191, 5, 152, 218,
			72, 54, 28, 85, 211, 154, 105, 22, 63, 168,
			253, 36, 207, 95, 131, 101, 93, 35, 220, 163,
			173, 150, 28, 98, 243, 86, 32, 133, 82, 187,
			158, 213, 41, 7, 112, 150, 150, 109, 103, 12,
			53, 78, 74, 188, 152, 4, 241, 116, 108, 8,
			202, 35, 115, 39, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue,
			byte.MaxValue, byte.MaxValue
		};
	}
}
