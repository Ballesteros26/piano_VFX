using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;

namespace Mono.Security.Cryptography
{
	// Token: 0x02000006 RID: 6
	internal class NativeDapiProtection
	{
		// Token: 0x06000009 RID: 9
		[SuppressUnmanagedCodeSecurity]
		[DllImport("crypt32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptProtectData(ref NativeDapiProtection.DATA_BLOB pDataIn, string szDataDescr, ref NativeDapiProtection.DATA_BLOB pOptionalEntropy, IntPtr pvReserved, ref NativeDapiProtection.CRYPTPROTECT_PROMPTSTRUCT pPromptStruct, uint dwFlags, ref NativeDapiProtection.DATA_BLOB pDataOut);

		// Token: 0x0600000A RID: 10
		[SuppressUnmanagedCodeSecurity]
		[DllImport("crypt32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, SetLastError = true)]
		private static extern bool CryptUnprotectData(ref NativeDapiProtection.DATA_BLOB pDataIn, string szDataDescr, ref NativeDapiProtection.DATA_BLOB pOptionalEntropy, IntPtr pvReserved, ref NativeDapiProtection.CRYPTPROTECT_PROMPTSTRUCT pPromptStruct, uint dwFlags, ref NativeDapiProtection.DATA_BLOB pDataOut);

		// Token: 0x0600000B RID: 11
		[SuppressUnmanagedCodeSecurity]
		[DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Auto, EntryPoint = "RtlZeroMemory")]
		private static extern void ZeroMemory(IntPtr dest, int size);

		// Token: 0x0600000C RID: 12 RVA: 0x00002620 File Offset: 0x00000820
		public static byte[] Protect(byte[] userData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			byte[] array = null;
			int num = 0;
			NativeDapiProtection.DATA_BLOB data_BLOB = default(NativeDapiProtection.DATA_BLOB);
			NativeDapiProtection.DATA_BLOB data_BLOB2 = default(NativeDapiProtection.DATA_BLOB);
			NativeDapiProtection.DATA_BLOB data_BLOB3 = default(NativeDapiProtection.DATA_BLOB);
			try
			{
				NativeDapiProtection.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT = new NativeDapiProtection.CRYPTPROTECT_PROMPTSTRUCT(0U);
				data_BLOB.Alloc(userData);
				data_BLOB2.Alloc(optionalEntropy);
				uint num2 = 1U;
				if (scope == DataProtectionScope.LocalMachine)
				{
					num2 |= 4U;
				}
				if (NativeDapiProtection.CryptProtectData(ref data_BLOB, string.Empty, ref data_BLOB2, IntPtr.Zero, ref cryptprotect_PROMPTSTRUCT, num2, ref data_BLOB3))
				{
					array = data_BLOB3.ToBytes();
				}
				else
				{
					num = Marshal.GetLastWin32Error();
				}
			}
			catch (Exception ex)
			{
				throw new CryptographicException(Locale.GetText("Error protecting data."), ex);
			}
			finally
			{
				data_BLOB3.Free();
				data_BLOB.Free();
				data_BLOB2.Free();
			}
			if (array == null || num != 0)
			{
				throw new CryptographicException(num);
			}
			return array;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000026F0 File Offset: 0x000008F0
		public static byte[] Unprotect(byte[] encryptedData, byte[] optionalEntropy, DataProtectionScope scope)
		{
			byte[] array = null;
			int num = 0;
			NativeDapiProtection.DATA_BLOB data_BLOB = default(NativeDapiProtection.DATA_BLOB);
			NativeDapiProtection.DATA_BLOB data_BLOB2 = default(NativeDapiProtection.DATA_BLOB);
			NativeDapiProtection.DATA_BLOB data_BLOB3 = default(NativeDapiProtection.DATA_BLOB);
			try
			{
				NativeDapiProtection.CRYPTPROTECT_PROMPTSTRUCT cryptprotect_PROMPTSTRUCT = new NativeDapiProtection.CRYPTPROTECT_PROMPTSTRUCT(0U);
				data_BLOB.Alloc(encryptedData);
				data_BLOB2.Alloc(optionalEntropy);
				uint num2 = 1U;
				if (scope == DataProtectionScope.LocalMachine)
				{
					num2 |= 4U;
				}
				if (NativeDapiProtection.CryptUnprotectData(ref data_BLOB, null, ref data_BLOB2, IntPtr.Zero, ref cryptprotect_PROMPTSTRUCT, num2, ref data_BLOB3))
				{
					array = data_BLOB3.ToBytes();
				}
				else
				{
					num = Marshal.GetLastWin32Error();
				}
			}
			catch (Exception ex)
			{
				throw new CryptographicException(Locale.GetText("Error protecting data."), ex);
			}
			finally
			{
				data_BLOB.Free();
				data_BLOB3.Free();
				data_BLOB2.Free();
			}
			if (array == null || num != 0)
			{
				throw new CryptographicException(num);
			}
			return array;
		}

		// Token: 0x04000085 RID: 133
		private const uint CRYPTPROTECT_UI_FORBIDDEN = 1U;

		// Token: 0x04000086 RID: 134
		private const uint CRYPTPROTECT_LOCAL_MACHINE = 4U;

		// Token: 0x02000007 RID: 7
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct DATA_BLOB
		{
			// Token: 0x0600000F RID: 15 RVA: 0x000027BC File Offset: 0x000009BC
			public void Alloc(int size)
			{
				if (size > 0)
				{
					this.pbData = Marshal.AllocHGlobal(size);
					this.cbData = size;
				}
			}

			// Token: 0x06000010 RID: 16 RVA: 0x000027D8 File Offset: 0x000009D8
			public void Alloc(byte[] managedMemory)
			{
				if (managedMemory != null)
				{
					int num = managedMemory.Length;
					this.pbData = Marshal.AllocHGlobal(num);
					this.cbData = num;
					Marshal.Copy(managedMemory, 0, this.pbData, this.cbData);
				}
			}

			// Token: 0x06000011 RID: 17 RVA: 0x00002814 File Offset: 0x00000A14
			public void Free()
			{
				if (this.pbData != IntPtr.Zero)
				{
					NativeDapiProtection.ZeroMemory(this.pbData, this.cbData);
					Marshal.FreeHGlobal(this.pbData);
					this.pbData = IntPtr.Zero;
					this.cbData = 0;
				}
			}

			// Token: 0x06000012 RID: 18 RVA: 0x00002864 File Offset: 0x00000A64
			public byte[] ToBytes()
			{
				if (this.cbData <= 0)
				{
					return new byte[0];
				}
				byte[] array = new byte[this.cbData];
				Marshal.Copy(this.pbData, array, 0, this.cbData);
				return array;
			}

			// Token: 0x04000087 RID: 135
			private int cbData;

			// Token: 0x04000088 RID: 136
			private IntPtr pbData;
		}

		// Token: 0x02000008 RID: 8
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		private struct CRYPTPROTECT_PROMPTSTRUCT
		{
			// Token: 0x06000013 RID: 19 RVA: 0x000028A1 File Offset: 0x00000AA1
			public CRYPTPROTECT_PROMPTSTRUCT(uint flags)
			{
				this.cbSize = Marshal.SizeOf(typeof(NativeDapiProtection.CRYPTPROTECT_PROMPTSTRUCT));
				this.dwPromptFlags = flags;
				this.hwndApp = IntPtr.Zero;
				this.szPrompt = null;
			}

			// Token: 0x04000089 RID: 137
			private int cbSize;

			// Token: 0x0400008A RID: 138
			private uint dwPromptFlags;

			// Token: 0x0400008B RID: 139
			private IntPtr hwndApp;

			// Token: 0x0400008C RID: 140
			private string szPrompt;
		}
	}
}
