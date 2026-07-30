using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

// Token: 0x0200000D RID: 13
internal static class Interop
{
	// Token: 0x0200000E RID: 14
	private static class Libraries
	{
		// Token: 0x04000040 RID: 64
		internal const string SystemNative = "System.Native";

		// Token: 0x04000041 RID: 65
		internal const string HttpNative = "System.Net.Http.Native";

		// Token: 0x04000042 RID: 66
		internal const string NetSecurityNative = "System.Net.Security.Native";

		// Token: 0x04000043 RID: 67
		internal const string CryptoNative = "System.Security.Cryptography.Native.OpenSsl";

		// Token: 0x04000044 RID: 68
		internal const string GlobalizationNative = "System.Globalization.Native";

		// Token: 0x04000045 RID: 69
		internal const string CompressionNative = "System.IO.Compression.Native";

		// Token: 0x04000046 RID: 70
		internal const string Libdl = "libdl";
	}

	// Token: 0x0200000F RID: 15
	internal static class NetSecurityNative
	{
		// Token: 0x0600006C RID: 108
		[DllImport("System.Net.Security.Native", EntryPoint = "NetSecurityNative_ReleaseGssBuffer")]
		internal static extern void ReleaseGssBuffer(IntPtr bufferPtr, ulong length);

		// Token: 0x0600006D RID: 109
		[DllImport("System.Net.Security.Native", EntryPoint = "NetSecurityNative_DisplayMinorStatus")]
		internal static extern Interop.NetSecurityNative.Status DisplayMinorStatus(out Interop.NetSecurityNative.Status minorStatus, Interop.NetSecurityNative.Status statusValue, ref Interop.NetSecurityNative.GssBuffer buffer);

		// Token: 0x0600006E RID: 110
		[DllImport("System.Net.Security.Native", EntryPoint = "NetSecurityNative_DisplayMajorStatus")]
		internal static extern Interop.NetSecurityNative.Status DisplayMajorStatus(out Interop.NetSecurityNative.Status minorStatus, Interop.NetSecurityNative.Status statusValue, ref Interop.NetSecurityNative.GssBuffer buffer);

		// Token: 0x0600006F RID: 111
		[DllImport("System.Net.Security.Native", EntryPoint = "NetSecurityNative_ImportUserName")]
		internal static extern Interop.NetSecurityNative.Status ImportUserName(out Interop.NetSecurityNative.Status minorStatus, string inputName, int inputNameByteCount, out SafeGssNameHandle outputName);

		// Token: 0x06000070 RID: 112
		[DllImport("System.Net.Security.Native", EntryPoint = "NetSecurityNative_ImportPrincipalName")]
		internal static extern Interop.NetSecurityNative.Status ImportPrincipalName(out Interop.NetSecurityNative.Status minorStatus, string inputName, int inputNameByteCount, out SafeGssNameHandle outputName);

		// Token: 0x06000071 RID: 113
		[DllImport("System.Net.Security.Native", EntryPoint = "NetSecurityNative_ReleaseName")]
		internal static extern Interop.NetSecurityNative.Status ReleaseName(out Interop.NetSecurityNative.Status minorStatus, ref IntPtr inputName);

		// Token: 0x06000072 RID: 114
		[DllImport("System.Net.Security.Native", EntryPoint = "NetSecurityNative_InitiateCredSpNego")]
		internal static extern Interop.NetSecurityNative.Status InitiateCredSpNego(out Interop.NetSecurityNative.Status minorStatus, SafeGssNameHandle desiredName, out SafeGssCredHandle outputCredHandle);

		// Token: 0x06000073 RID: 115
		[DllImport("System.Net.Security.Native", EntryPoint = "NetSecurityNative_InitiateCredWithPassword")]
		internal static extern Interop.NetSecurityNative.Status InitiateCredWithPassword(out Interop.NetSecurityNative.Status minorStatus, bool isNtlm, SafeGssNameHandle desiredName, string password, int passwordLen, out SafeGssCredHandle outputCredHandle);

		// Token: 0x06000074 RID: 116
		[DllImport("System.Net.Security.Native", EntryPoint = "NetSecurityNative_ReleaseCred")]
		internal static extern Interop.NetSecurityNative.Status ReleaseCred(out Interop.NetSecurityNative.Status minorStatus, ref IntPtr credHandle);

		// Token: 0x06000075 RID: 117
		[DllImport("System.Net.Security.Native", EntryPoint = "NetSecurityNative_InitSecContext")]
		internal static extern Interop.NetSecurityNative.Status InitSecContext(out Interop.NetSecurityNative.Status minorStatus, SafeGssCredHandle initiatorCredHandle, ref SafeGssContextHandle contextHandle, bool isNtlmOnly, SafeGssNameHandle targetName, uint reqFlags, byte[] inputBytes, int inputLength, ref Interop.NetSecurityNative.GssBuffer token, out uint retFlags, out int isNtlmUsed);

		// Token: 0x06000076 RID: 118
		[DllImport("System.Net.Security.Native", EntryPoint = "NetSecurityNative_AcceptSecContext")]
		internal static extern Interop.NetSecurityNative.Status AcceptSecContext(out Interop.NetSecurityNative.Status minorStatus, ref SafeGssContextHandle acceptContextHandle, byte[] inputBytes, int inputLength, ref Interop.NetSecurityNative.GssBuffer token);

		// Token: 0x06000077 RID: 119
		[DllImport("System.Net.Security.Native", EntryPoint = "NetSecurityNative_DeleteSecContext")]
		internal static extern Interop.NetSecurityNative.Status DeleteSecContext(out Interop.NetSecurityNative.Status minorStatus, ref IntPtr contextHandle);

		// Token: 0x06000078 RID: 120
		[DllImport("System.Net.Security.Native", EntryPoint = "NetSecurityNative_Wrap")]
		private static extern Interop.NetSecurityNative.Status Wrap(out Interop.NetSecurityNative.Status minorStatus, SafeGssContextHandle contextHandle, bool isEncrypt, byte[] inputBytes, int offset, int count, ref Interop.NetSecurityNative.GssBuffer outBuffer);

		// Token: 0x06000079 RID: 121
		[DllImport("System.Net.Security.Native", EntryPoint = "NetSecurityNative_Unwrap")]
		private static extern Interop.NetSecurityNative.Status Unwrap(out Interop.NetSecurityNative.Status minorStatus, SafeGssContextHandle contextHandle, byte[] inputBytes, int offset, int count, ref Interop.NetSecurityNative.GssBuffer outBuffer);

		// Token: 0x0600007A RID: 122 RVA: 0x00004293 File Offset: 0x00002493
		internal static Interop.NetSecurityNative.Status WrapBuffer(out Interop.NetSecurityNative.Status minorStatus, SafeGssContextHandle contextHandle, bool isEncrypt, byte[] inputBytes, int offset, int count, ref Interop.NetSecurityNative.GssBuffer outBuffer)
		{
			return Interop.NetSecurityNative.Wrap(out minorStatus, contextHandle, isEncrypt, inputBytes, offset, count, ref outBuffer);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000042A4 File Offset: 0x000024A4
		internal static Interop.NetSecurityNative.Status UnwrapBuffer(out Interop.NetSecurityNative.Status minorStatus, SafeGssContextHandle contextHandle, byte[] inputBytes, int offset, int count, ref Interop.NetSecurityNative.GssBuffer outBuffer)
		{
			return Interop.NetSecurityNative.Unwrap(out minorStatus, contextHandle, inputBytes, offset, count, ref outBuffer);
		}

		// Token: 0x02000010 RID: 16
		internal sealed class GssApiException : Exception
		{
			// Token: 0x17000040 RID: 64
			// (get) Token: 0x0600007C RID: 124 RVA: 0x000042B3 File Offset: 0x000024B3
			public Interop.NetSecurityNative.Status MinorStatus
			{
				get
				{
					return this._minorStatus;
				}
			}

			// Token: 0x0600007D RID: 125 RVA: 0x000042BB File Offset: 0x000024BB
			public GssApiException(string message)
				: base(message)
			{
			}

			// Token: 0x0600007E RID: 126 RVA: 0x000042C4 File Offset: 0x000024C4
			public GssApiException(Interop.NetSecurityNative.Status majorStatus, Interop.NetSecurityNative.Status minorStatus)
				: base(Interop.NetSecurityNative.GssApiException.GetGssApiDisplayStatus(majorStatus, minorStatus))
			{
				base.HResult = (int)majorStatus;
				this._minorStatus = minorStatus;
			}

			// Token: 0x0600007F RID: 127 RVA: 0x000042E4 File Offset: 0x000024E4
			private static string GetGssApiDisplayStatus(Interop.NetSecurityNative.Status majorStatus, Interop.NetSecurityNative.Status minorStatus)
			{
				string gssApiDisplayStatus = Interop.NetSecurityNative.GssApiException.GetGssApiDisplayStatus(majorStatus, false);
				string gssApiDisplayStatus2 = Interop.NetSecurityNative.GssApiException.GetGssApiDisplayStatus(minorStatus, true);
				if (gssApiDisplayStatus == null || gssApiDisplayStatus2 == null)
				{
					return SR.Format("GSSAPI operation failed with status: {0} (Minor status: {1}).", majorStatus.ToString("x"), minorStatus.ToString("x"));
				}
				return SR.Format("GSSAPI operation failed with error - {0} ({1}).", gssApiDisplayStatus, gssApiDisplayStatus2);
			}

			// Token: 0x06000080 RID: 128 RVA: 0x00004340 File Offset: 0x00002540
			private static string GetGssApiDisplayStatus(Interop.NetSecurityNative.Status status, bool isMinor)
			{
				Interop.NetSecurityNative.GssBuffer gssBuffer = default(Interop.NetSecurityNative.GssBuffer);
				string text;
				try
				{
					Interop.NetSecurityNative.Status status2;
					text = (((isMinor ? Interop.NetSecurityNative.DisplayMinorStatus(out status2, status, ref gssBuffer) : Interop.NetSecurityNative.DisplayMajorStatus(out status2, status, ref gssBuffer)) != Interop.NetSecurityNative.Status.GSS_S_COMPLETE) ? null : Marshal.PtrToStringAnsi(gssBuffer._data));
				}
				finally
				{
					gssBuffer.Dispose();
				}
				return text;
			}

			// Token: 0x04000047 RID: 71
			private readonly Interop.NetSecurityNative.Status _minorStatus;
		}

		// Token: 0x02000011 RID: 17
		internal struct GssBuffer : IDisposable
		{
			// Token: 0x06000081 RID: 129 RVA: 0x0000439C File Offset: 0x0000259C
			internal int Copy(byte[] destination, int offset)
			{
				if (this._data == IntPtr.Zero || this._length == 0UL)
				{
					return 0;
				}
				int num = Convert.ToInt32(this._length);
				int num2 = destination.Length - offset;
				if (num > num2)
				{
					throw new Interop.NetSecurityNative.GssApiException(SR.Format("Insufficient buffer space. Required: {0} Actual: {1}.", num, num2));
				}
				Marshal.Copy(this._data, destination, offset, num);
				return num;
			}

			// Token: 0x06000082 RID: 130 RVA: 0x00004408 File Offset: 0x00002608
			internal byte[] ToByteArray()
			{
				if (this._data == IntPtr.Zero || this._length == 0UL)
				{
					return Array.Empty<byte>();
				}
				int num = Convert.ToInt32(this._length);
				byte[] array = new byte[num];
				Marshal.Copy(this._data, array, 0, num);
				return array;
			}

			// Token: 0x06000083 RID: 131 RVA: 0x00004457 File Offset: 0x00002657
			public void Dispose()
			{
				if (this._data != IntPtr.Zero)
				{
					Interop.NetSecurityNative.ReleaseGssBuffer(this._data, this._length);
					this._data = IntPtr.Zero;
				}
				this._length = 0UL;
			}

			// Token: 0x04000048 RID: 72
			internal ulong _length;

			// Token: 0x04000049 RID: 73
			internal IntPtr _data;
		}

		// Token: 0x02000012 RID: 18
		internal enum Status : uint
		{
			// Token: 0x0400004B RID: 75
			GSS_S_COMPLETE,
			// Token: 0x0400004C RID: 76
			GSS_S_CONTINUE_NEEDED
		}

		// Token: 0x02000013 RID: 19
		[Flags]
		internal enum GssFlags : uint
		{
			// Token: 0x0400004E RID: 78
			GSS_C_DELEG_FLAG = 1U,
			// Token: 0x0400004F RID: 79
			GSS_C_MUTUAL_FLAG = 2U,
			// Token: 0x04000050 RID: 80
			GSS_C_REPLAY_FLAG = 4U,
			// Token: 0x04000051 RID: 81
			GSS_C_SEQUENCE_FLAG = 8U,
			// Token: 0x04000052 RID: 82
			GSS_C_CONF_FLAG = 16U,
			// Token: 0x04000053 RID: 83
			GSS_C_INTEG_FLAG = 32U,
			// Token: 0x04000054 RID: 84
			GSS_C_ANON_FLAG = 64U,
			// Token: 0x04000055 RID: 85
			GSS_C_PROT_READY_FLAG = 128U,
			// Token: 0x04000056 RID: 86
			GSS_C_TRANS_FLAG = 256U,
			// Token: 0x04000057 RID: 87
			GSS_C_DCE_STYLE = 4096U,
			// Token: 0x04000058 RID: 88
			GSS_C_IDENTIFY_FLAG = 8192U,
			// Token: 0x04000059 RID: 89
			GSS_C_EXTENDED_ERROR_FLAG = 16384U,
			// Token: 0x0400005A RID: 90
			GSS_C_DELEG_POLICY_FLAG = 32768U
		}
	}
}
