using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Util
{
	// Token: 0x02000142 RID: 322
	internal static class MachineKeySectionUtils
	{
		// Token: 0x06000EA2 RID: 3746 RVA: 0x00029884 File Offset: 0x00027A84
		private static byte ToHexValue(char c, bool high)
		{
			byte b;
			if (c >= '0' && c <= '9')
			{
				b = (byte)(c - '0');
			}
			else if (c >= 'a' && c <= 'f')
			{
				b = (byte)(c - 'a' + '\n');
			}
			else
			{
				if (c < 'A' || c > 'F')
				{
					throw new ArgumentException("Invalid hex character");
				}
				b = (byte)(c - 'A' + '\n');
			}
			if (high)
			{
				b = (byte)(b << 4);
			}
			return b;
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x000298E4 File Offset: 0x00027AE4
		internal static byte[] GetBytes(string key, int len)
		{
			byte[] array = new byte[len / 2];
			for (int i = 0; i < len; i += 2)
			{
				array[i / 2] = MachineKeySectionUtils.ToHexValue(key[i], true) + MachineKeySectionUtils.ToHexValue(key[i + 1], false);
			}
			return array;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x0002992C File Offset: 0x00027B2C
		public static string GetHexString(byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder(bytes.Length * 2);
			int num = 55;
			foreach (byte b in bytes)
			{
				int num2 = (int)(b & 15);
				int num3 = (b >> 4) & 15;
				stringBuilder.Append((char)((num3 > 9) ? (num + num3) : (48 + num3)));
				stringBuilder.Append((char)((num2 > 9) ? (num + num2) : (48 + num2)));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x0002999C File Offset: 0x00027B9C
		public static SymmetricAlgorithm GetDecryptionAlgorithm(string name)
		{
			SymmetricAlgorithm symmetricAlgorithm;
			if (!(name == "AES") && !(name == "Auto"))
			{
				if (!(name == "DES"))
				{
					if (!(name == "3DES"))
					{
						if (!name.StartsWith("alg:"))
						{
							throw new ConfigurationErrorsException();
						}
						symmetricAlgorithm = SymmetricAlgorithm.Create(name.Substring(4));
					}
					else
					{
						symmetricAlgorithm = TripleDES.Create();
					}
				}
				else
				{
					symmetricAlgorithm = DES.Create();
				}
			}
			else
			{
				symmetricAlgorithm = Rijndael.Create();
			}
			return symmetricAlgorithm;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x00029A1C File Offset: 0x00027C1C
		public static KeyedHashAlgorithm GetValidationAlgorithm(MachineKeySection section)
		{
			KeyedHashAlgorithm keyedHashAlgorithm = null;
			switch (section.Validation)
			{
			case MachineKeyValidation.MD5:
				keyedHashAlgorithm = new HMACMD5();
				break;
			case MachineKeyValidation.SHA1:
			case MachineKeyValidation.TripleDES:
			case MachineKeyValidation.AES:
				keyedHashAlgorithm = new HMACSHA1();
				break;
			case MachineKeyValidation.HMACSHA256:
				keyedHashAlgorithm = new HMACSHA256();
				break;
			case MachineKeyValidation.HMACSHA384:
				keyedHashAlgorithm = new HMACSHA384();
				break;
			case MachineKeyValidation.HMACSHA512:
				keyedHashAlgorithm = new HMACSHA512();
				break;
			case MachineKeyValidation.Custom:
			{
				string validationAlgorithm = section.ValidationAlgorithm;
				if (validationAlgorithm.StartsWith("alg:"))
				{
					keyedHashAlgorithm = KeyedHashAlgorithm.Create(validationAlgorithm.Substring(4));
				}
				break;
			}
			}
			return keyedHashAlgorithm;
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00029AA4 File Offset: 0x00027CA4
		private static SymmetricAlgorithm GetDecryptionAlgorithm(MachineKeySection section)
		{
			return section.GetDecryptionAlgorithm();
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00029AAC File Offset: 0x00027CAC
		private static byte[] GetDecryptionKey(MachineKeySection section)
		{
			return section.GetDecryptionKey();
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00029AB4 File Offset: 0x00027CB4
		public static byte[] GetValidationKey(MachineKeySection section)
		{
			return section.GetValidationKey();
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00029ABC File Offset: 0x00027CBC
		public static byte[] Decrypt(MachineKeySection section, byte[] encodedData)
		{
			return MachineKeySectionUtils.Decrypt(section, encodedData, 0, encodedData.Length);
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x00029ACC File Offset: 0x00027CCC
		private static byte[] Decrypt(MachineKeySection section, byte[] encodedData, int offset, int length)
		{
			byte[] array;
			using (SymmetricAlgorithm decryptionAlgorithm = MachineKeySectionUtils.GetDecryptionAlgorithm(section))
			{
				decryptionAlgorithm.Key = MachineKeySectionUtils.GetDecryptionKey(section);
				array = MachineKeySectionUtils.Decrypt(decryptionAlgorithm, encodedData, offset, length);
			}
			return array;
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x00029B14 File Offset: 0x00027D14
		public static byte[] Decrypt(SymmetricAlgorithm alg, byte[] encodedData, int offset, int length)
		{
			byte[] array = new byte[alg.IV.Length];
			Array.Copy(encodedData, 0, array, 0, array.Length);
			byte[] array2;
			using (ICryptoTransform cryptoTransform = alg.CreateDecryptor(alg.Key, array))
			{
				try
				{
					array2 = cryptoTransform.TransformFinalBlock(encodedData, array.Length + offset, length - array.Length);
				}
				catch (CryptographicException)
				{
					array2 = null;
				}
			}
			return array2;
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00029B8C File Offset: 0x00027D8C
		public static byte[] Encrypt(MachineKeySection section, byte[] data)
		{
			byte[] array;
			using (SymmetricAlgorithm decryptionAlgorithm = MachineKeySectionUtils.GetDecryptionAlgorithm(section))
			{
				decryptionAlgorithm.Key = MachineKeySectionUtils.GetDecryptionKey(section);
				array = MachineKeySectionUtils.Encrypt(decryptionAlgorithm, data);
			}
			return array;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00029BD4 File Offset: 0x00027DD4
		public static byte[] Encrypt(SymmetricAlgorithm alg, byte[] data)
		{
			byte[] iv = alg.IV;
			byte[] array3;
			using (ICryptoTransform cryptoTransform = alg.CreateEncryptor(alg.Key, iv))
			{
				byte[] array = cryptoTransform.TransformFinalBlock(data, 0, data.Length);
				byte[] array2 = new byte[iv.Length + array.Length];
				Array.Copy(iv, 0, array2, 0, iv.Length);
				Array.Copy(array, 0, array2, iv.Length, array.Length);
				array3 = array2;
			}
			return array3;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00029C4C File Offset: 0x00027E4C
		public static byte[] Sign(MachineKeySection section, byte[] data)
		{
			return MachineKeySectionUtils.Sign(section, data, 0, data.Length);
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00029C5C File Offset: 0x00027E5C
		private static byte[] Sign(MachineKeySection section, byte[] data, int offset, int length)
		{
			byte[] array3;
			using (KeyedHashAlgorithm validationAlgorithm = MachineKeySectionUtils.GetValidationAlgorithm(section))
			{
				validationAlgorithm.Key = MachineKeySectionUtils.GetValidationKey(section);
				byte[] array = validationAlgorithm.ComputeHash(data, offset, length);
				byte[] array2 = new byte[length + array.Length];
				Array.Copy(data, array2, length);
				Array.Copy(array, 0, array2, length, array.Length);
				array3 = array2;
			}
			return array3;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00029CC4 File Offset: 0x00027EC4
		public static byte[] Verify(MachineKeySection section, byte[] data)
		{
			byte[] array = null;
			bool flag = true;
			using (KeyedHashAlgorithm validationAlgorithm = MachineKeySectionUtils.GetValidationAlgorithm(section))
			{
				validationAlgorithm.Key = MachineKeySectionUtils.GetValidationKey(section);
				int num = validationAlgorithm.HashSize >> 3;
				byte[] array2 = MachineKeySectionUtils.Sign(section, data, 0, data.Length - num);
				for (int i = 0; i < array2.Length; i++)
				{
					if (array2[i] != data[data.Length - array2.Length + i])
					{
						flag = false;
					}
				}
				array = new byte[data.Length - num];
				Array.Copy(data, 0, array, 0, array.Length);
			}
			if (!flag)
			{
				return null;
			}
			return array;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00029D64 File Offset: 0x00027F64
		public static byte[] EncryptSign(MachineKeySection section, byte[] data)
		{
			byte[] array = MachineKeySectionUtils.Encrypt(section, data);
			return MachineKeySectionUtils.Sign(section, array);
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00029D80 File Offset: 0x00027F80
		public static byte[] VerifyDecrypt(MachineKeySection section, byte[] block)
		{
			bool flag = true;
			int num;
			using (KeyedHashAlgorithm validationAlgorithm = MachineKeySectionUtils.GetValidationAlgorithm(section))
			{
				validationAlgorithm.Key = MachineKeySectionUtils.GetValidationKey(section);
				num = validationAlgorithm.HashSize >> 3;
				byte[] array = MachineKeySectionUtils.Sign(section, block, 0, block.Length - num);
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != block[block.Length - array.Length + i])
					{
						flag = false;
					}
				}
			}
			byte[] array3;
			try
			{
				byte[] array2 = MachineKeySectionUtils.Decrypt(section, block, 0, block.Length - num);
				array3 = (flag ? array2 : null);
			}
			catch
			{
				array3 = null;
			}
			return array3;
		}
	}
}
