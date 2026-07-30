using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Provides methods for protecting and unprotecting memory. This class cannot be inherited.</summary>
	// Token: 0x0200001A RID: 26
	public sealed class ProtectedMemory
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002050 File Offset: 0x00000250
		private ProtectedMemory()
		{
		}

		/// <summary>Protects the specified data.</summary>
		/// <param name="userData">The byte array containing data in memory to protect. The array must be a multiple of 16 bytes. </param>
		/// <param name="scope">One of the enumeration values that specifies the scope of memory protection. </param>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="userData" /> must be 16 bytes in length or in multiples of 16 bytes. </exception>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this method. This method can be used only with the Windows 2000 or later operating systems. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="userData " />is null.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.DataProtectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ProtectMemory" />
		/// </PermissionSet>
		// Token: 0x06000057 RID: 87 RVA: 0x000031D8 File Offset: 0x000013D8
		[MonoTODO("only supported on Windows 2000 SP3 and later")]
		public static void Protect(byte[] userData, MemoryProtectionScope scope)
		{
			if (userData == null)
			{
				throw new ArgumentNullException("userData");
			}
			ProtectedMemory.Check(userData.Length, scope);
			try
			{
				uint num = (uint)userData.Length;
				ProtectedMemory.MemoryProtectionImplementation memoryProtectionImplementation = ProtectedMemory.impl;
				if (memoryProtectionImplementation != ProtectedMemory.MemoryProtectionImplementation.Win32RtlEncryptMemory)
				{
					if (memoryProtectionImplementation != ProtectedMemory.MemoryProtectionImplementation.Win32CryptoProtect)
					{
						throw new PlatformNotSupportedException();
					}
					if (!ProtectedMemory.CryptProtectMemory(userData, num, (uint)scope))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
				}
				else
				{
					int num2 = ProtectedMemory.RtlEncryptMemory(userData, num, (uint)scope);
					if (num2 < 0)
					{
						throw new CryptographicException(Locale.GetText("Error. NTSTATUS = {0}.", new object[] { num2 }));
					}
				}
			}
			catch
			{
				ProtectedMemory.impl = ProtectedMemory.MemoryProtectionImplementation.Unsupported;
				throw new PlatformNotSupportedException();
			}
		}

		/// <summary>Unprotects data in memory that was protected using the <see cref="M:System.Security.Cryptography.ProtectedMemory.Protect(System.Byte[],System.Security.Cryptography.MemoryProtectionScope)" /> method.</summary>
		/// <param name="encryptedData">The byte array in memory to unencrypt. </param>
		/// <param name="scope">One of the enumeration values that specifies the scope of memory protection. </param>
		/// <exception cref="T:System.NotSupportedException">The operating system does not support this method. This method can be used only with the Windows 2000 or later operating systems. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="encryptedData " />is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">
		///   <paramref name="encryptedData " />is empty.-or-This call was not implemented.-or-NTSTATUS contains an error.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.DataProtectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnprotectMemory" />
		/// </PermissionSet>
		// Token: 0x06000058 RID: 88 RVA: 0x0000327C File Offset: 0x0000147C
		[MonoTODO("only supported on Windows 2000 SP3 and later")]
		public static void Unprotect(byte[] encryptedData, MemoryProtectionScope scope)
		{
			if (encryptedData == null)
			{
				throw new ArgumentNullException("encryptedData");
			}
			ProtectedMemory.Check(encryptedData.Length, scope);
			try
			{
				uint num = (uint)encryptedData.Length;
				ProtectedMemory.MemoryProtectionImplementation memoryProtectionImplementation = ProtectedMemory.impl;
				if (memoryProtectionImplementation != ProtectedMemory.MemoryProtectionImplementation.Win32RtlEncryptMemory)
				{
					if (memoryProtectionImplementation != ProtectedMemory.MemoryProtectionImplementation.Win32CryptoProtect)
					{
						throw new PlatformNotSupportedException();
					}
					if (!ProtectedMemory.CryptUnprotectMemory(encryptedData, num, (uint)scope))
					{
						throw new CryptographicException(Marshal.GetLastWin32Error());
					}
				}
				else
				{
					int num2 = ProtectedMemory.RtlDecryptMemory(encryptedData, num, (uint)scope);
					if (num2 < 0)
					{
						throw new CryptographicException(Locale.GetText("Error. NTSTATUS = {0}.", new object[] { num2 }));
					}
				}
			}
			catch
			{
				ProtectedMemory.impl = ProtectedMemory.MemoryProtectionImplementation.Unsupported;
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003320 File Offset: 0x00001520
		private static void Detect()
		{
			OperatingSystem osversion = Environment.OSVersion;
			PlatformID platform = osversion.Platform;
			if (platform != PlatformID.Win32NT)
			{
				ProtectedMemory.impl = ProtectedMemory.MemoryProtectionImplementation.Unsupported;
				return;
			}
			Version version = osversion.Version;
			if (version.Major < 5)
			{
				ProtectedMemory.impl = ProtectedMemory.MemoryProtectionImplementation.Unsupported;
				return;
			}
			if (version.Major != 5)
			{
				ProtectedMemory.impl = ProtectedMemory.MemoryProtectionImplementation.Win32CryptoProtect;
				return;
			}
			if (version.Minor < 2)
			{
				ProtectedMemory.impl = ProtectedMemory.MemoryProtectionImplementation.Win32RtlEncryptMemory;
				return;
			}
			ProtectedMemory.impl = ProtectedMemory.MemoryProtectionImplementation.Win32CryptoProtect;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000338C File Offset: 0x0000158C
		private static void Check(int size, MemoryProtectionScope scope)
		{
			if (size % 16 != 0)
			{
				throw new CryptographicException(Locale.GetText("Not a multiple of {0} bytes.", new object[] { 16 }));
			}
			if (scope < MemoryProtectionScope.SameProcess || scope > MemoryProtectionScope.SameLogon)
			{
				throw new ArgumentException(Locale.GetText("Invalid enum value for '{0}'.", new object[] { "MemoryProtectionScope" }), "scope");
			}
			ProtectedMemory.MemoryProtectionImplementation memoryProtectionImplementation = ProtectedMemory.impl;
			if (memoryProtectionImplementation == ProtectedMemory.MemoryProtectionImplementation.Unsupported)
			{
				throw new PlatformNotSupportedException();
			}
			if (memoryProtectionImplementation == ProtectedMemory.MemoryProtectionImplementation.Unknown)
			{
				ProtectedMemory.Detect();
				return;
			}
		}

		// Token: 0x0600005B RID: 91
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "SystemFunction040", SetLastError = true)]
		private static extern int RtlEncryptMemory(byte[] pData, uint cbData, uint dwFlags);

		// Token: 0x0600005C RID: 92
		[SuppressUnmanagedCodeSecurity]
		[DllImport("advapi32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "SystemFunction041", SetLastError = true)]
		private static extern int RtlDecryptMemory(byte[] pData, uint cbData, uint dwFlags);

		// Token: 0x0600005D RID: 93
		[SuppressUnmanagedCodeSecurity]
		[DllImport("crypt32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptProtectMemory(byte[] pData, uint cbData, uint dwFlags);

		// Token: 0x0600005E RID: 94
		[SuppressUnmanagedCodeSecurity]
		[DllImport("crypt32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectMemory(byte[] pData, uint cbData, uint dwFlags);

		// Token: 0x040000A9 RID: 169
		private const int BlockSize = 16;

		// Token: 0x040000AA RID: 170
		private static ProtectedMemory.MemoryProtectionImplementation impl;

		// Token: 0x0200001B RID: 27
		private enum MemoryProtectionImplementation
		{
			// Token: 0x040000AC RID: 172
			Unknown,
			// Token: 0x040000AD RID: 173
			Win32RtlEncryptMemory,
			// Token: 0x040000AE RID: 174
			Win32CryptoProtect,
			// Token: 0x040000AF RID: 175
			Unsupported = -2147483648
		}
	}
}
