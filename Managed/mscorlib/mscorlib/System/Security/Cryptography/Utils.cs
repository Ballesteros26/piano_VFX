using System;
using System.Reflection;
using Mono.Security.Cryptography;

namespace System.Security.Cryptography
{
	// Token: 0x02000696 RID: 1686
	internal static class Utils
	{
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x0600481B RID: 18459 RVA: 0x0010156F File Offset: 0x000FF76F
		internal static RNGCryptoServiceProvider StaticRandomNumberGenerator
		{
			get
			{
				if (Utils._rng == null)
				{
					Utils._rng = new RNGCryptoServiceProvider();
				}
				return Utils._rng;
			}
		}

		// Token: 0x0600481C RID: 18460 RVA: 0x00101590 File Offset: 0x000FF790
		internal static byte[] GenerateRandom(int keySize)
		{
			byte[] array = new byte[keySize];
			Utils.StaticRandomNumberGenerator.GetBytes(array);
			return array;
		}

		// Token: 0x0600481D RID: 18461 RVA: 0x00003B29 File Offset: 0x00001D29
		[SecurityCritical]
		internal static bool HasAlgorithm(int dwCalg, int dwKeySize)
		{
			return true;
		}

		// Token: 0x0600481E RID: 18462 RVA: 0x001015B0 File Offset: 0x000FF7B0
		internal static string DiscardWhiteSpaces(string inputBuffer)
		{
			return Utils.DiscardWhiteSpaces(inputBuffer, 0, inputBuffer.Length);
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x001015C0 File Offset: 0x000FF7C0
		internal static string DiscardWhiteSpaces(string inputBuffer, int inputOffset, int inputCount)
		{
			int num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					num++;
				}
			}
			char[] array = new char[inputCount - num];
			num = 0;
			for (int i = 0; i < inputCount; i++)
			{
				if (!char.IsWhiteSpace(inputBuffer[inputOffset + i]))
				{
					array[num++] = inputBuffer[inputOffset + i];
				}
			}
			return new string(array);
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x0010162C File Offset: 0x000FF82C
		internal static int ConvertByteArrayToInt(byte[] input)
		{
			int num = 0;
			for (int i = 0; i < input.Length; i++)
			{
				num *= 256;
				num += (int)input[i];
			}
			return num;
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x00101658 File Offset: 0x000FF858
		internal static byte[] ConvertIntToByteArray(int dwInput)
		{
			byte[] array = new byte[8];
			int num = 0;
			if (dwInput == 0)
			{
				return new byte[1];
			}
			int i = dwInput;
			while (i > 0)
			{
				int num2 = i % 256;
				array[num] = (byte)num2;
				i = (i - num2) / 256;
				num++;
			}
			byte[] array2 = new byte[num];
			for (int j = 0; j < num; j++)
			{
				array2[j] = array[num - j - 1];
			}
			return array2;
		}

		// Token: 0x06004822 RID: 18466 RVA: 0x001016C4 File Offset: 0x000FF8C4
		internal static void ConvertIntToByteArray(uint dwInput, ref byte[] counter)
		{
			uint num = dwInput;
			int num2 = 0;
			Array.Clear(counter, 0, counter.Length);
			if (dwInput == 0U)
			{
				return;
			}
			while (num > 0U)
			{
				uint num3 = num % 256U;
				counter[3 - num2] = (byte)num3;
				num = (num - num3) / 256U;
				num2++;
			}
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x00101708 File Offset: 0x000FF908
		internal static byte[] FixupKeyParity(byte[] key)
		{
			byte[] array = new byte[key.Length];
			for (int i = 0; i < key.Length; i++)
			{
				array[i] = key[i] & 254;
				byte b = (byte)((int)(array[i] & 15) ^ (array[i] >> 4));
				byte b2 = (byte)((int)(b & 3) ^ (b >> 2));
				if ((byte)((int)(b2 & 1) ^ (b2 >> 1)) == 0)
				{
					byte[] array2 = array;
					int num = i;
					array2[num] |= 1;
				}
			}
			return array;
		}

		// Token: 0x06004824 RID: 18468 RVA: 0x00101768 File Offset: 0x000FF968
		[SecurityCritical]
		internal unsafe static void DWORDFromLittleEndian(uint* x, int digits, byte* block)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				x[i] = (uint)((int)block[num] | ((int)block[num + 1] << 8) | ((int)block[num + 2] << 16) | ((int)block[num + 3] << 24));
				i++;
				num += 4;
			}
		}

		// Token: 0x06004825 RID: 18469 RVA: 0x001017B0 File Offset: 0x000FF9B0
		internal static void DWORDToLittleEndian(byte[] block, uint[] x, int digits)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				block[num] = (byte)(x[i] & 255U);
				block[num + 1] = (byte)((x[i] >> 8) & 255U);
				block[num + 2] = (byte)((x[i] >> 16) & 255U);
				block[num + 3] = (byte)((x[i] >> 24) & 255U);
				i++;
				num += 4;
			}
		}

		// Token: 0x06004826 RID: 18470 RVA: 0x00101814 File Offset: 0x000FFA14
		[SecurityCritical]
		internal unsafe static void DWORDFromBigEndian(uint* x, int digits, byte* block)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				x[i] = (uint)(((int)block[num] << 24) | ((int)block[num + 1] << 16) | ((int)block[num + 2] << 8) | (int)block[num + 3]);
				i++;
				num += 4;
			}
		}

		// Token: 0x06004827 RID: 18471 RVA: 0x0010185C File Offset: 0x000FFA5C
		internal static void DWORDToBigEndian(byte[] block, uint[] x, int digits)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				block[num] = (byte)((x[i] >> 24) & 255U);
				block[num + 1] = (byte)((x[i] >> 16) & 255U);
				block[num + 2] = (byte)((x[i] >> 8) & 255U);
				block[num + 3] = (byte)(x[i] & 255U);
				i++;
				num += 4;
			}
		}

		// Token: 0x06004828 RID: 18472 RVA: 0x001018C0 File Offset: 0x000FFAC0
		[SecurityCritical]
		internal unsafe static void QuadWordFromBigEndian(ulong* x, int digits, byte* block)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				x[i] = ((ulong)block[num] << 56) | ((ulong)block[num + 1] << 48) | ((ulong)block[num + 2] << 40) | ((ulong)block[num + 3] << 32) | ((ulong)block[num + 4] << 24) | ((ulong)block[num + 5] << 16) | ((ulong)block[num + 6] << 8) | (ulong)block[num + 7];
				i++;
				num += 8;
			}
		}

		// Token: 0x06004829 RID: 18473 RVA: 0x00101938 File Offset: 0x000FFB38
		internal static void QuadWordToBigEndian(byte[] block, ulong[] x, int digits)
		{
			int i = 0;
			int num = 0;
			while (i < digits)
			{
				block[num] = (byte)((x[i] >> 56) & 255UL);
				block[num + 1] = (byte)((x[i] >> 48) & 255UL);
				block[num + 2] = (byte)((x[i] >> 40) & 255UL);
				block[num + 3] = (byte)((x[i] >> 32) & 255UL);
				block[num + 4] = (byte)((x[i] >> 24) & 255UL);
				block[num + 5] = (byte)((x[i] >> 16) & 255UL);
				block[num + 6] = (byte)((x[i] >> 8) & 255UL);
				block[num + 7] = (byte)(x[i] & 255UL);
				i++;
				num += 8;
			}
		}

		// Token: 0x0600482A RID: 18474 RVA: 0x001019EF File Offset: 0x000FFBEF
		internal static byte[] Int(uint i)
		{
			return new byte[]
			{
				(byte)(i >> 24),
				(byte)(i >> 16),
				(byte)(i >> 8),
				(byte)i
			};
		}

		// Token: 0x0600482B RID: 18475 RVA: 0x00101A13 File Offset: 0x000FFC13
		[SecurityCritical]
		internal static byte[] RsaOaepEncrypt(RSA rsa, HashAlgorithm hash, PKCS1MaskGenerationMethod mgf, RandomNumberGenerator rng, byte[] data)
		{
			return PKCS1.Encrypt_OAEP(rsa, hash, rng, data);
		}

		// Token: 0x0600482C RID: 18476 RVA: 0x00101A1F File Offset: 0x000FFC1F
		[SecurityCritical]
		internal static byte[] RsaOaepDecrypt(RSA rsa, HashAlgorithm hash, PKCS1MaskGenerationMethod mgf, byte[] encryptedData)
		{
			byte[] array = PKCS1.Decrypt_OAEP(rsa, hash, encryptedData);
			if (array == null)
			{
				throw new CryptographicException(Environment.GetResourceString("Error occurred while decoding OAEP padding."));
			}
			return array;
		}

		// Token: 0x0600482D RID: 18477 RVA: 0x00101A3C File Offset: 0x000FFC3C
		[SecurityCritical]
		internal static byte[] RsaPkcs1Padding(RSA rsa, byte[] oid, byte[] hash)
		{
			int num = rsa.KeySize / 8;
			byte[] array = new byte[num];
			byte[] array2 = new byte[oid.Length + 8 + hash.Length];
			array2[0] = 48;
			int num2 = array2.Length - 2;
			array2[1] = (byte)num2;
			array2[2] = 48;
			num2 = oid.Length + 2;
			array2[3] = (byte)num2;
			Buffer.InternalBlockCopy(oid, 0, array2, 4, oid.Length);
			array2[4 + oid.Length] = 5;
			array2[4 + oid.Length + 1] = 0;
			array2[4 + oid.Length + 2] = 4;
			array2[4 + oid.Length + 3] = (byte)hash.Length;
			Buffer.InternalBlockCopy(hash, 0, array2, oid.Length + 8, hash.Length);
			int num3 = num - array2.Length;
			if (num3 <= 2)
			{
				throw new CryptographicUnexpectedOperationException(Environment.GetResourceString("Object identifier (OID) is unknown."));
			}
			array[0] = 0;
			array[1] = 1;
			for (int i = 2; i < num3 - 1; i++)
			{
				array[i] = byte.MaxValue;
			}
			array[num3 - 1] = 0;
			Buffer.InternalBlockCopy(array2, 0, array, num3, array2.Length);
			return array;
		}

		// Token: 0x0600482E RID: 18478 RVA: 0x00101B20 File Offset: 0x000FFD20
		internal static bool CompareBigIntArrays(byte[] lhs, byte[] rhs)
		{
			if (lhs == null)
			{
				return rhs == null;
			}
			int i = 0;
			int num = 0;
			while (i < lhs.Length)
			{
				if (lhs[i] != 0)
				{
					break;
				}
				i++;
			}
			while (num < rhs.Length && rhs[num] == 0)
			{
				num++;
			}
			int num2 = lhs.Length - i;
			if (rhs.Length - num != num2)
			{
				return false;
			}
			for (int j = 0; j < num2; j++)
			{
				if (lhs[i + j] != rhs[num + j])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600482F RID: 18479 RVA: 0x00101B88 File Offset: 0x000FFD88
		internal static HashAlgorithmName OidToHashAlgorithmName(string oid)
		{
			if (oid == "1.3.14.3.2.26")
			{
				return HashAlgorithmName.SHA1;
			}
			if (oid == "2.16.840.1.101.3.4.2.1")
			{
				return HashAlgorithmName.SHA256;
			}
			if (oid == "2.16.840.1.101.3.4.2.2")
			{
				return HashAlgorithmName.SHA384;
			}
			if (!(oid == "2.16.840.1.101.3.4.2.3"))
			{
				throw new NotSupportedException();
			}
			return HashAlgorithmName.SHA512;
		}

		// Token: 0x06004830 RID: 18480 RVA: 0x00101BE8 File Offset: 0x000FFDE8
		internal static bool DoesRsaKeyOverride(RSA rsaKey, string methodName, Type[] parameterTypes)
		{
			Type type = rsaKey.GetType();
			return rsaKey is RSACryptoServiceProvider || type.FullName == "System.Security.Cryptography.RSACng" || Utils.DoesRsaKeyOverrideSlowPath(type, methodName, parameterTypes);
		}

		// Token: 0x06004831 RID: 18481 RVA: 0x00101C22 File Offset: 0x000FFE22
		private static bool DoesRsaKeyOverrideSlowPath(Type t, string methodName, Type[] parameterTypes)
		{
			return !(t.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public, null, parameterTypes, null).DeclaringType == typeof(RSA));
		}

		// Token: 0x06004832 RID: 18482 RVA: 0x00101C49 File Offset: 0x000FFE49
		internal static bool _ProduceLegacyHmacValues()
		{
			return Environment.GetEnvironmentVariable("legacyHMACMode") == "1";
		}

		// Token: 0x0400251C RID: 9500
		internal const int DefaultRsaProviderType = 1;

		// Token: 0x0400251D RID: 9501
		private static volatile RNGCryptoServiceProvider _rng;
	}
}
