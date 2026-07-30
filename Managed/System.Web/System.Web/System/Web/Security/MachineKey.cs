using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Security
{
	/// <summary>Provides a way to encrypt or hash data (or both) by using the same algorithms and key values that are used for ASP.NET forms authentication and view state.</summary>
	/// <exception cref="T:System.ArgumentNullException">The data to encrypt, hash, decrypt, or validate does not exist</exception>
	// Token: 0x020004C2 RID: 1218
	public static class MachineKey
	{
		/// <summary>Decodes and/or validates data that has been encrypted or provided with a hash-based message authentication code (HMAC). </summary>
		/// <returns>A <see cref="T:System.Byte" /> array that represents the decrypted data.</returns>
		/// <param name="encodedData">The encrypted data to decrypt and/or validate.</param>
		/// <param name="protectionOption">Indicates whether the <paramref name="encodedData" /> parameter should be encrypted and/or hashed. </param>
		// Token: 0x060036D2 RID: 14034 RVA: 0x0008FAA8 File Offset: 0x0008DCA8
		public static byte[] Decode(string encodedData, MachineKeyProtection protectionOption)
		{
			if (encodedData == null)
			{
				throw new ArgumentNullException("encodedData");
			}
			int length = encodedData.Length;
			if (length == 0 || length % 2 == 1)
			{
				throw new ArgumentException("encodedData");
			}
			byte[] bytes = MachineKeySectionUtils.GetBytes(encodedData, length);
			if (bytes == null || bytes.Length == 0)
			{
				throw new ArgumentException("encodedData");
			}
			MachineKeySection machineKeySection = WebConfigurationManager.GetWebApplicationSection("system.web/machineKey") as MachineKeySection;
			byte[] array = null;
			Exception ex = null;
			try
			{
				switch (protectionOption)
				{
				case MachineKeyProtection.All:
					array = MachineKeySectionUtils.VerifyDecrypt(machineKeySection, bytes);
					break;
				case MachineKeyProtection.Encryption:
					array = MachineKeySectionUtils.Decrypt(machineKeySection, bytes);
					break;
				case MachineKeyProtection.Validation:
					array = MachineKeySectionUtils.Verify(machineKeySection, bytes);
					break;
				default:
					return MachineKeySectionUtils.GetBytes(encodedData, length);
				}
			}
			catch (Exception ex)
			{
			}
			if (array == null || ex != null)
			{
				throw new HttpException("Unable to verify passed data.", ex);
			}
			return array;
		}

		/// <summary>Encrypts data and/or appends a hash-based message authentication code (HMAC).</summary>
		/// <returns>The encrypted value, the input value with an HMAC appended, or the result of encrypting the input value with an HMAC appended. </returns>
		/// <param name="data">The data to encrypt. </param>
		/// <param name="protectionOption">Indicates whether the <paramref name="data" /> parameter should be encrypted and/or hashed.</param>
		// Token: 0x060036D3 RID: 14035 RVA: 0x0008FB78 File Offset: 0x0008DD78
		public static string Encode(byte[] data, MachineKeyProtection protectionOption)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			MachineKeySection machineKeySection = WebConfigurationManager.GetWebApplicationSection("system.web/machineKey") as MachineKeySection;
			byte[] array;
			switch (protectionOption)
			{
			case MachineKeyProtection.All:
				array = MachineKeySectionUtils.EncryptSign(machineKeySection, data);
				break;
			case MachineKeyProtection.Encryption:
				array = MachineKeySectionUtils.Encrypt(machineKeySection, data);
				break;
			case MachineKeyProtection.Validation:
				array = MachineKeySectionUtils.Sign(machineKeySection, data);
				break;
			default:
				return string.Empty;
			}
			return MachineKeySectionUtils.GetHexString(array);
		}

		/// <summary>Protects the specified data by encrypting or signing it.</summary>
		/// <returns>The ciphertext data.</returns>
		/// <param name="userData">The data to protect. This data is passed as plaintext.</param>
		/// <param name="purposes">A list of purposes for the data. If this value is specified, the same list must be passed to the <see cref="M:System.Web.Security.MachineKey.Unprotect(System.Byte[],System.String[])" /> method in order to decipher the returned ciphertext.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="userData" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The purposes array contains one or more whitespace-only entries.</exception>
		// Token: 0x060036D4 RID: 14036 RVA: 0x0008FBE4 File Offset: 0x0008DDE4
		public static byte[] Protect(byte[] userData, params string[] purposes)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			for (int i = 0; i < purposes.Length; i++)
			{
				if (string.IsNullOrWhiteSpace(purposes[i]))
				{
					throw new ArgumentException("all purpose parameters must contain text");
				}
			}
			MachineKeySection machineKeySection = WebConfigurationManager.GetWebApplicationSection("system.web/machineKey") as MachineKeySection;
			byte[] hashed = MachineKey.GetHashed(string.Join(";", purposes));
			byte[] array = new byte[userData.Length + hashed.Length];
			hashed.CopyTo(array, 0);
			userData.CopyTo(array, hashed.Length);
			return MachineKeySectionUtils.Encrypt(machineKeySection, array);
		}

		/// <summary>Unprotects the specified data, which was protected by the <see cref="M:System.Web.Security.MachineKey.Protect(System.Byte[],System.String[])" /> method.</summary>
		/// <returns>The plaintext data.</returns>
		/// <param name="protectedData">The ciphertext data to unprotect.</param>
		/// <param name="purposes">A list of purposes that describe what the data is meant for. This must be the same value that was passed to the <see cref="M:System.Web.Security.MachineKey.Protect(System.Byte[],System.String[])" /> method when the data was protected. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="protectedData" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The purposes array contains one or more whitespace-only entries.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">Possible causes include the following: The protected data was tampered with.The value of the <paramref name="purposes" /> parameter is not the same as the value that was specified when the data was protected.The application is deployed to more than one server and is using auto-generated encryption keys.</exception>
		// Token: 0x060036D5 RID: 14037 RVA: 0x0008FC6C File Offset: 0x0008DE6C
		public static byte[] Unprotect(byte[] protectedData, params string[] purposes)
		{
			if (protectedData == null)
			{
				throw new ArgumentNullException("protectedData");
			}
			for (int i = 0; i < purposes.Length; i++)
			{
				if (string.IsNullOrWhiteSpace(purposes[i]))
				{
					throw new ArgumentException("all purpose parameters must contain text");
				}
			}
			MachineKeySection machineKeySection = WebConfigurationManager.GetWebApplicationSection("system.web/machineKey") as MachineKeySection;
			byte[] hashed = MachineKey.GetHashed(string.Join(";", purposes));
			byte[] array = MachineKeySectionUtils.Decrypt(machineKeySection, protectedData);
			for (int j = 0; j < hashed.Length; j++)
			{
				if (hashed[j] != array[j])
				{
					throw new CryptographicException();
				}
			}
			int num = array.Length - hashed.Length;
			byte[] array2 = new byte[num];
			Array.Copy(array, hashed.Length, array2, 0, num);
			return array2;
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x0008FD1C File Offset: 0x0008DF1C
		private static byte[] GetHashed(string purposes)
		{
			byte[] array;
			using (SHA512 sha = SHA512.Create())
			{
				byte[] bytes = Encoding.UTF8.GetBytes(purposes);
				array = sha.ComputeHash(bytes, 0, bytes.Length);
			}
			return array;
		}
	}
}
