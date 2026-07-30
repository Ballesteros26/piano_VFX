using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace Mono.Security.Cryptography
{
	// Token: 0x0200007F RID: 127
	internal sealed class CryptoConvert
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x00002111 File Offset: 0x00000311
		private CryptoConvert()
		{
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00016142 File Offset: 0x00014342
		private static int ToInt32LE(byte[] bytes, int offset)
		{
			return ((int)bytes[offset + 3] << 24) | ((int)bytes[offset + 2] << 16) | ((int)bytes[offset + 1] << 8) | (int)bytes[offset];
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00016142 File Offset: 0x00014342
		private static uint ToUInt32LE(byte[] bytes, int offset)
		{
			return (uint)(((int)bytes[offset + 3] << 24) | ((int)bytes[offset + 2] << 16) | ((int)bytes[offset + 1] << 8) | (int)bytes[offset]);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00016161 File Offset: 0x00014361
		private static byte[] GetBytesLE(int val)
		{
			return new byte[]
			{
				(byte)(val & 255),
				(byte)((val >> 8) & 255),
				(byte)((val >> 16) & 255),
				(byte)((val >> 24) & 255)
			};
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000161A0 File Offset: 0x000143A0
		private static byte[] Trim(byte[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != 0)
				{
					byte[] array2 = new byte[array.Length - i];
					Buffer.BlockCopy(array, i, array2, 0, array2.Length);
					return array2;
				}
			}
			return null;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x000161DA File Offset: 0x000143DA
		public static RSA FromCapiPrivateKeyBlob(byte[] blob)
		{
			return CryptoConvert.FromCapiPrivateKeyBlob(blob, 0);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x000161E4 File Offset: 0x000143E4
		public static RSA FromCapiPrivateKeyBlob(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			RSAParameters rsaparameters = default(RSAParameters);
			try
			{
				if (blob[offset] != 7 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || CryptoConvert.ToUInt32LE(blob, offset + 8) != 843141970U)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = CryptoConvert.ToInt32LE(blob, offset + 12);
				byte[] array = new byte[4];
				Buffer.BlockCopy(blob, offset + 16, array, 0, 4);
				Array.Reverse<byte>(array);
				rsaparameters.Exponent = CryptoConvert.Trim(array);
				int num2 = offset + 20;
				int num3 = num >> 3;
				rsaparameters.Modulus = new byte[num3];
				Buffer.BlockCopy(blob, num2, rsaparameters.Modulus, 0, num3);
				Array.Reverse<byte>(rsaparameters.Modulus);
				num2 += num3;
				int num4 = num3 >> 1;
				rsaparameters.P = new byte[num4];
				Buffer.BlockCopy(blob, num2, rsaparameters.P, 0, num4);
				Array.Reverse<byte>(rsaparameters.P);
				num2 += num4;
				rsaparameters.Q = new byte[num4];
				Buffer.BlockCopy(blob, num2, rsaparameters.Q, 0, num4);
				Array.Reverse<byte>(rsaparameters.Q);
				num2 += num4;
				rsaparameters.DP = new byte[num4];
				Buffer.BlockCopy(blob, num2, rsaparameters.DP, 0, num4);
				Array.Reverse<byte>(rsaparameters.DP);
				num2 += num4;
				rsaparameters.DQ = new byte[num4];
				Buffer.BlockCopy(blob, num2, rsaparameters.DQ, 0, num4);
				Array.Reverse<byte>(rsaparameters.DQ);
				num2 += num4;
				rsaparameters.InverseQ = new byte[num4];
				Buffer.BlockCopy(blob, num2, rsaparameters.InverseQ, 0, num4);
				Array.Reverse<byte>(rsaparameters.InverseQ);
				num2 += num4;
				rsaparameters.D = new byte[num3];
				if (num2 + num3 + offset <= blob.Length)
				{
					Buffer.BlockCopy(blob, num2, rsaparameters.D, 0, num3);
					Array.Reverse<byte>(rsaparameters.D);
				}
			}
			catch (Exception ex)
			{
				throw new CryptographicException("Invalid blob.", ex);
			}
			RSA rsa = null;
			try
			{
				rsa = RSA.Create();
				rsa.ImportParameters(rsaparameters);
			}
			catch (CryptographicException ex2)
			{
				try
				{
					rsa = new RSACryptoServiceProvider(new CspParameters
					{
						Flags = CspProviderFlags.UseMachineKeyStore
					});
					rsa.ImportParameters(rsaparameters);
				}
				catch
				{
					throw ex2;
				}
			}
			return rsa;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0001646C File Offset: 0x0001466C
		public static DSA FromCapiPrivateKeyBlobDSA(byte[] blob)
		{
			return CryptoConvert.FromCapiPrivateKeyBlobDSA(blob, 0);
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00016478 File Offset: 0x00014678
		public static DSA FromCapiPrivateKeyBlobDSA(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			DSAParameters dsaparameters = default(DSAParameters);
			try
			{
				if (blob[offset] != 7 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || CryptoConvert.ToUInt32LE(blob, offset + 8) != 844321604U)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = CryptoConvert.ToInt32LE(blob, offset + 12) >> 3;
				int num2 = offset + 16;
				dsaparameters.P = new byte[num];
				Buffer.BlockCopy(blob, num2, dsaparameters.P, 0, num);
				Array.Reverse<byte>(dsaparameters.P);
				num2 += num;
				dsaparameters.Q = new byte[20];
				Buffer.BlockCopy(blob, num2, dsaparameters.Q, 0, 20);
				Array.Reverse<byte>(dsaparameters.Q);
				num2 += 20;
				dsaparameters.G = new byte[num];
				Buffer.BlockCopy(blob, num2, dsaparameters.G, 0, num);
				Array.Reverse<byte>(dsaparameters.G);
				num2 += num;
				dsaparameters.X = new byte[20];
				Buffer.BlockCopy(blob, num2, dsaparameters.X, 0, 20);
				Array.Reverse<byte>(dsaparameters.X);
				num2 += 20;
				dsaparameters.Counter = CryptoConvert.ToInt32LE(blob, num2);
				num2 += 4;
				dsaparameters.Seed = new byte[20];
				Buffer.BlockCopy(blob, num2, dsaparameters.Seed, 0, 20);
				Array.Reverse<byte>(dsaparameters.Seed);
				num2 += 20;
			}
			catch (Exception ex)
			{
				throw new CryptographicException("Invalid blob.", ex);
			}
			DSA dsa = null;
			try
			{
				dsa = DSA.Create();
				dsa.ImportParameters(dsaparameters);
			}
			catch (CryptographicException ex2)
			{
				try
				{
					dsa = new DSACryptoServiceProvider(new CspParameters
					{
						Flags = CspProviderFlags.UseMachineKeyStore
					});
					dsa.ImportParameters(dsaparameters);
				}
				catch
				{
					throw ex2;
				}
			}
			return dsa;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0001667C File Offset: 0x0001487C
		public static byte[] ToCapiPrivateKeyBlob(RSA rsa)
		{
			RSAParameters rsaparameters = rsa.ExportParameters(true);
			int num = rsaparameters.Modulus.Length;
			byte[] array = new byte[20 + (num << 2) + (num >> 1)];
			array[0] = 7;
			array[1] = 2;
			array[5] = 36;
			array[8] = 82;
			array[9] = 83;
			array[10] = 65;
			array[11] = 50;
			byte[] bytesLE = CryptoConvert.GetBytesLE(num << 3);
			array[12] = bytesLE[0];
			array[13] = bytesLE[1];
			array[14] = bytesLE[2];
			array[15] = bytesLE[3];
			int num2 = 16;
			int i = rsaparameters.Exponent.Length;
			while (i > 0)
			{
				array[num2++] = rsaparameters.Exponent[--i];
			}
			num2 = 20;
			byte[] modulus = rsaparameters.Modulus;
			int num3 = modulus.Length;
			Array.Reverse<byte>(modulus, 0, num3);
			Buffer.BlockCopy(modulus, 0, array, num2, num3);
			num2 += num3;
			byte[] p = rsaparameters.P;
			num3 = p.Length;
			Array.Reverse<byte>(p, 0, num3);
			Buffer.BlockCopy(p, 0, array, num2, num3);
			num2 += num3;
			byte[] q = rsaparameters.Q;
			num3 = q.Length;
			Array.Reverse<byte>(q, 0, num3);
			Buffer.BlockCopy(q, 0, array, num2, num3);
			num2 += num3;
			byte[] dp = rsaparameters.DP;
			num3 = dp.Length;
			Array.Reverse<byte>(dp, 0, num3);
			Buffer.BlockCopy(dp, 0, array, num2, num3);
			num2 += num3;
			byte[] dq = rsaparameters.DQ;
			num3 = dq.Length;
			Array.Reverse<byte>(dq, 0, num3);
			Buffer.BlockCopy(dq, 0, array, num2, num3);
			num2 += num3;
			byte[] inverseQ = rsaparameters.InverseQ;
			num3 = inverseQ.Length;
			Array.Reverse<byte>(inverseQ, 0, num3);
			Buffer.BlockCopy(inverseQ, 0, array, num2, num3);
			num2 += num3;
			byte[] d = rsaparameters.D;
			num3 = d.Length;
			Array.Reverse<byte>(d, 0, num3);
			Buffer.BlockCopy(d, 0, array, num2, num3);
			return array;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00016828 File Offset: 0x00014A28
		public static byte[] ToCapiPrivateKeyBlob(DSA dsa)
		{
			DSAParameters dsaparameters = dsa.ExportParameters(true);
			int num = dsaparameters.P.Length;
			byte[] array = new byte[16 + num + 20 + num + 20 + 4 + 20];
			array[0] = 7;
			array[1] = 2;
			array[5] = 34;
			array[8] = 68;
			array[9] = 83;
			array[10] = 83;
			array[11] = 50;
			byte[] bytesLE = CryptoConvert.GetBytesLE(num << 3);
			array[12] = bytesLE[0];
			array[13] = bytesLE[1];
			array[14] = bytesLE[2];
			array[15] = bytesLE[3];
			int num2 = 16;
			byte[] p = dsaparameters.P;
			Array.Reverse<byte>(p);
			Buffer.BlockCopy(p, 0, array, num2, num);
			num2 += num;
			byte[] q = dsaparameters.Q;
			Array.Reverse<byte>(q);
			Buffer.BlockCopy(q, 0, array, num2, 20);
			num2 += 20;
			byte[] g = dsaparameters.G;
			Array.Reverse<byte>(g);
			Buffer.BlockCopy(g, 0, array, num2, num);
			num2 += num;
			byte[] x = dsaparameters.X;
			Array.Reverse<byte>(x);
			Buffer.BlockCopy(x, 0, array, num2, 20);
			num2 += 20;
			Buffer.BlockCopy(CryptoConvert.GetBytesLE(dsaparameters.Counter), 0, array, num2, 4);
			num2 += 4;
			byte[] seed = dsaparameters.Seed;
			Array.Reverse<byte>(seed);
			Buffer.BlockCopy(seed, 0, array, num2, 20);
			return array;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001693E File Offset: 0x00014B3E
		public static RSA FromCapiPublicKeyBlob(byte[] blob)
		{
			return CryptoConvert.FromCapiPublicKeyBlob(blob, 0);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00016948 File Offset: 0x00014B48
		public static RSA FromCapiPublicKeyBlob(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			RSA rsa2;
			try
			{
				if (blob[offset] != 6 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || CryptoConvert.ToUInt32LE(blob, offset + 8) != 826364754U)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = CryptoConvert.ToInt32LE(blob, offset + 12);
				RSAParameters rsaparameters = new RSAParameters
				{
					Exponent = new byte[3]
				};
				rsaparameters.Exponent[0] = blob[offset + 18];
				rsaparameters.Exponent[1] = blob[offset + 17];
				rsaparameters.Exponent[2] = blob[offset + 16];
				int num2 = offset + 20;
				int num3 = num >> 3;
				rsaparameters.Modulus = new byte[num3];
				Buffer.BlockCopy(blob, num2, rsaparameters.Modulus, 0, num3);
				Array.Reverse<byte>(rsaparameters.Modulus);
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
				rsa2 = rsa;
			}
			catch (Exception ex)
			{
				throw new CryptographicException("Invalid blob.", ex);
			}
			return rsa2;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00016A84 File Offset: 0x00014C84
		public static DSA FromCapiPublicKeyBlobDSA(byte[] blob)
		{
			return CryptoConvert.FromCapiPublicKeyBlobDSA(blob, 0);
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00016A90 File Offset: 0x00014C90
		public static DSA FromCapiPublicKeyBlobDSA(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			DSA dsa2;
			try
			{
				if (blob[offset] != 6 || blob[offset + 1] != 2 || blob[offset + 2] != 0 || blob[offset + 3] != 0 || CryptoConvert.ToUInt32LE(blob, offset + 8) != 827544388U)
				{
					throw new CryptographicException("Invalid blob header");
				}
				int num = CryptoConvert.ToInt32LE(blob, offset + 12);
				DSAParameters dsaparameters = default(DSAParameters);
				int num2 = num >> 3;
				int num3 = offset + 16;
				dsaparameters.P = new byte[num2];
				Buffer.BlockCopy(blob, num3, dsaparameters.P, 0, num2);
				Array.Reverse<byte>(dsaparameters.P);
				num3 += num2;
				dsaparameters.Q = new byte[20];
				Buffer.BlockCopy(blob, num3, dsaparameters.Q, 0, 20);
				Array.Reverse<byte>(dsaparameters.Q);
				num3 += 20;
				dsaparameters.G = new byte[num2];
				Buffer.BlockCopy(blob, num3, dsaparameters.G, 0, num2);
				Array.Reverse<byte>(dsaparameters.G);
				num3 += num2;
				dsaparameters.Y = new byte[num2];
				Buffer.BlockCopy(blob, num3, dsaparameters.Y, 0, num2);
				Array.Reverse<byte>(dsaparameters.Y);
				num3 += num2;
				dsaparameters.Counter = CryptoConvert.ToInt32LE(blob, num3);
				num3 += 4;
				dsaparameters.Seed = new byte[20];
				Buffer.BlockCopy(blob, num3, dsaparameters.Seed, 0, 20);
				Array.Reverse<byte>(dsaparameters.Seed);
				num3 += 20;
				DSA dsa = DSA.Create();
				dsa.ImportParameters(dsaparameters);
				dsa2 = dsa;
			}
			catch (Exception ex)
			{
				throw new CryptographicException("Invalid blob.", ex);
			}
			return dsa2;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00016C38 File Offset: 0x00014E38
		public static byte[] ToCapiPublicKeyBlob(RSA rsa)
		{
			RSAParameters rsaparameters = rsa.ExportParameters(false);
			int num = rsaparameters.Modulus.Length;
			byte[] array = new byte[20 + num];
			array[0] = 6;
			array[1] = 2;
			array[5] = 36;
			array[8] = 82;
			array[9] = 83;
			array[10] = 65;
			array[11] = 49;
			byte[] bytesLE = CryptoConvert.GetBytesLE(num << 3);
			array[12] = bytesLE[0];
			array[13] = bytesLE[1];
			array[14] = bytesLE[2];
			array[15] = bytesLE[3];
			int num2 = 16;
			int i = rsaparameters.Exponent.Length;
			while (i > 0)
			{
				array[num2++] = rsaparameters.Exponent[--i];
			}
			num2 = 20;
			byte[] modulus = rsaparameters.Modulus;
			int num3 = modulus.Length;
			Array.Reverse<byte>(modulus, 0, num3);
			Buffer.BlockCopy(modulus, 0, array, num2, num3);
			num2 += num3;
			return array;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00016D00 File Offset: 0x00014F00
		public static byte[] ToCapiPublicKeyBlob(DSA dsa)
		{
			DSAParameters dsaparameters = dsa.ExportParameters(false);
			int num = dsaparameters.P.Length;
			byte[] array = new byte[16 + num + 20 + num + num + 4 + 20];
			array[0] = 6;
			array[1] = 2;
			array[5] = 34;
			array[8] = 68;
			array[9] = 83;
			array[10] = 83;
			array[11] = 49;
			byte[] bytesLE = CryptoConvert.GetBytesLE(num << 3);
			array[12] = bytesLE[0];
			array[13] = bytesLE[1];
			array[14] = bytesLE[2];
			array[15] = bytesLE[3];
			int num2 = 16;
			byte[] p = dsaparameters.P;
			Array.Reverse<byte>(p);
			Buffer.BlockCopy(p, 0, array, num2, num);
			num2 += num;
			byte[] q = dsaparameters.Q;
			Array.Reverse<byte>(q);
			Buffer.BlockCopy(q, 0, array, num2, 20);
			num2 += 20;
			byte[] g = dsaparameters.G;
			Array.Reverse<byte>(g);
			Buffer.BlockCopy(g, 0, array, num2, num);
			num2 += num;
			byte[] y = dsaparameters.Y;
			Array.Reverse<byte>(y);
			Buffer.BlockCopy(y, 0, array, num2, num);
			num2 += num;
			Buffer.BlockCopy(CryptoConvert.GetBytesLE(dsaparameters.Counter), 0, array, num2, 4);
			num2 += 4;
			byte[] seed = dsaparameters.Seed;
			Array.Reverse<byte>(seed);
			Buffer.BlockCopy(seed, 0, array, num2, 20);
			return array;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00016E13 File Offset: 0x00015013
		public static RSA FromCapiKeyBlob(byte[] blob)
		{
			return CryptoConvert.FromCapiKeyBlob(blob, 0);
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00016E1C File Offset: 0x0001501C
		public static RSA FromCapiKeyBlob(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			byte b = blob[offset];
			if (b != 0)
			{
				if (b == 6)
				{
					return CryptoConvert.FromCapiPublicKeyBlob(blob, offset);
				}
				if (b == 7)
				{
					return CryptoConvert.FromCapiPrivateKeyBlob(blob, offset);
				}
			}
			else if (blob[offset + 12] == 6)
			{
				return CryptoConvert.FromCapiPublicKeyBlob(blob, offset + 12);
			}
			throw new CryptographicException("Unknown blob format.");
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x00016E87 File Offset: 0x00015087
		public static DSA FromCapiKeyBlobDSA(byte[] blob)
		{
			return CryptoConvert.FromCapiKeyBlobDSA(blob, 0);
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x00016E90 File Offset: 0x00015090
		public static DSA FromCapiKeyBlobDSA(byte[] blob, int offset)
		{
			if (blob == null)
			{
				throw new ArgumentNullException("blob");
			}
			if (offset >= blob.Length)
			{
				throw new ArgumentException("blob is too small.");
			}
			byte b = blob[offset];
			if (b == 6)
			{
				return CryptoConvert.FromCapiPublicKeyBlobDSA(blob, offset);
			}
			if (b != 7)
			{
				throw new CryptographicException("Unknown blob format.");
			}
			return CryptoConvert.FromCapiPrivateKeyBlobDSA(blob, offset);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00016EE4 File Offset: 0x000150E4
		public static byte[] ToCapiKeyBlob(AsymmetricAlgorithm keypair, bool includePrivateKey)
		{
			if (keypair == null)
			{
				throw new ArgumentNullException("keypair");
			}
			if (keypair is RSA)
			{
				return CryptoConvert.ToCapiKeyBlob((RSA)keypair, includePrivateKey);
			}
			if (keypair is DSA)
			{
				return CryptoConvert.ToCapiKeyBlob((DSA)keypair, includePrivateKey);
			}
			return null;
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00016F1F File Offset: 0x0001511F
		public static byte[] ToCapiKeyBlob(RSA rsa, bool includePrivateKey)
		{
			if (rsa == null)
			{
				throw new ArgumentNullException("rsa");
			}
			if (includePrivateKey)
			{
				return CryptoConvert.ToCapiPrivateKeyBlob(rsa);
			}
			return CryptoConvert.ToCapiPublicKeyBlob(rsa);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00016F3F File Offset: 0x0001513F
		public static byte[] ToCapiKeyBlob(DSA dsa, bool includePrivateKey)
		{
			if (dsa == null)
			{
				throw new ArgumentNullException("dsa");
			}
			if (includePrivateKey)
			{
				return CryptoConvert.ToCapiPrivateKeyBlob(dsa);
			}
			return CryptoConvert.ToCapiPublicKeyBlob(dsa);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00016F60 File Offset: 0x00015160
		public static string ToHex(byte[] input)
		{
			if (input == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(input.Length * 2);
			foreach (byte b in input)
			{
				stringBuilder.Append(b.ToString("X2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00016FB0 File Offset: 0x000151B0
		private static byte FromHexChar(char c)
		{
			if (c >= 'a' && c <= 'f')
			{
				return (byte)(c - 'a' + '\n');
			}
			if (c >= 'A' && c <= 'F')
			{
				return (byte)(c - 'A' + '\n');
			}
			if (c >= '0' && c <= '9')
			{
				return (byte)(c - '0');
			}
			throw new ArgumentException("invalid hex char");
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00017000 File Offset: 0x00015200
		public static byte[] FromHex(string hex)
		{
			if (hex == null)
			{
				return null;
			}
			if ((hex.Length & 1) == 1)
			{
				throw new ArgumentException("Length must be a multiple of 2");
			}
			byte[] array = new byte[hex.Length >> 1];
			int i = 0;
			int num = 0;
			while (i < array.Length)
			{
				array[i] = (byte)(CryptoConvert.FromHexChar(hex[num++]) << 4);
				byte[] array2 = array;
				int num2 = i++;
				array2[num2] += CryptoConvert.FromHexChar(hex[num++]);
			}
			return array;
		}
	}
}
