using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono.Unity
{
	// Token: 0x02000010 RID: 16
	internal static class UnityTls
	{
		// Token: 0x06000058 RID: 88
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetUnityTlsInterface();

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002DD7 File Offset: 0x00000FD7
		public static bool IsSupported
		{
			get
			{
				return UnityTls.NativeInterface != null;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public static UnityTls.unitytls_interface_struct NativeInterface
		{
			get
			{
				if (UnityTls.marshalledInterface == null)
				{
					IntPtr unityTlsInterface = UnityTls.GetUnityTlsInterface();
					if (unityTlsInterface == IntPtr.Zero)
					{
						return null;
					}
					UnityTls.marshalledInterface = Marshal.PtrToStructure<UnityTls.unitytls_interface_struct>(unityTlsInterface);
				}
				return UnityTls.marshalledInterface;
			}
		}

		// Token: 0x040006D4 RID: 1748
		private static UnityTls.unitytls_interface_struct marshalledInterface;

		// Token: 0x02000011 RID: 17
		public enum unitytls_error_code : uint
		{
			// Token: 0x040006D6 RID: 1750
			UNITYTLS_SUCCESS,
			// Token: 0x040006D7 RID: 1751
			UNITYTLS_INVALID_ARGUMENT,
			// Token: 0x040006D8 RID: 1752
			UNITYTLS_INVALID_FORMAT,
			// Token: 0x040006D9 RID: 1753
			UNITYTLS_INVALID_PASSWORD,
			// Token: 0x040006DA RID: 1754
			UNITYTLS_INVALID_STATE,
			// Token: 0x040006DB RID: 1755
			UNITYTLS_BUFFER_OVERFLOW,
			// Token: 0x040006DC RID: 1756
			UNITYTLS_OUT_OF_MEMORY,
			// Token: 0x040006DD RID: 1757
			UNITYTLS_INTERNAL_ERROR,
			// Token: 0x040006DE RID: 1758
			UNITYTLS_NOT_SUPPORTED,
			// Token: 0x040006DF RID: 1759
			UNITYTLS_ENTROPY_SOURCE_FAILED,
			// Token: 0x040006E0 RID: 1760
			UNITYTLS_STREAM_CLOSED,
			// Token: 0x040006E1 RID: 1761
			UNITYTLS_USER_CUSTOM_ERROR_START = 1048576U,
			// Token: 0x040006E2 RID: 1762
			UNITYTLS_USER_WOULD_BLOCK,
			// Token: 0x040006E3 RID: 1763
			UNITYTLS_USER_READ_FAILED,
			// Token: 0x040006E4 RID: 1764
			UNITYTLS_USER_WRITE_FAILED,
			// Token: 0x040006E5 RID: 1765
			UNITYTLS_USER_UNKNOWN_ERROR,
			// Token: 0x040006E6 RID: 1766
			UNITYTLS_USER_CUSTOM_ERROR_END = 2097152U
		}

		// Token: 0x02000012 RID: 18
		public struct unitytls_errorstate
		{
			// Token: 0x040006E7 RID: 1767
			private uint magic;

			// Token: 0x040006E8 RID: 1768
			public UnityTls.unitytls_error_code code;

			// Token: 0x040006E9 RID: 1769
			private ulong reserved;
		}

		// Token: 0x02000013 RID: 19
		public struct unitytls_key
		{
		}

		// Token: 0x02000014 RID: 20
		public struct unitytls_key_ref
		{
			// Token: 0x040006EA RID: 1770
			public ulong handle;
		}

		// Token: 0x02000015 RID: 21
		public struct unitytls_x509
		{
		}

		// Token: 0x02000016 RID: 22
		public struct unitytls_x509_ref
		{
			// Token: 0x040006EB RID: 1771
			public ulong handle;
		}

		// Token: 0x02000017 RID: 23
		public struct unitytls_x509list
		{
		}

		// Token: 0x02000018 RID: 24
		public struct unitytls_x509list_ref
		{
			// Token: 0x040006EC RID: 1772
			public ulong handle;
		}

		// Token: 0x02000019 RID: 25
		[Flags]
		public enum unitytls_x509verify_result : uint
		{
			// Token: 0x040006EE RID: 1774
			UNITYTLS_X509VERIFY_SUCCESS = 0U,
			// Token: 0x040006EF RID: 1775
			UNITYTLS_X509VERIFY_NOT_DONE = 2147483648U,
			// Token: 0x040006F0 RID: 1776
			UNITYTLS_X509VERIFY_FATAL_ERROR = 4294967295U,
			// Token: 0x040006F1 RID: 1777
			UNITYTLS_X509VERIFY_FLAG_EXPIRED = 1U,
			// Token: 0x040006F2 RID: 1778
			UNITYTLS_X509VERIFY_FLAG_REVOKED = 2U,
			// Token: 0x040006F3 RID: 1779
			UNITYTLS_X509VERIFY_FLAG_CN_MISMATCH = 4U,
			// Token: 0x040006F4 RID: 1780
			UNITYTLS_X509VERIFY_FLAG_NOT_TRUSTED = 8U,
			// Token: 0x040006F5 RID: 1781
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR1 = 65536U,
			// Token: 0x040006F6 RID: 1782
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR2 = 131072U,
			// Token: 0x040006F7 RID: 1783
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR3 = 262144U,
			// Token: 0x040006F8 RID: 1784
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR4 = 524288U,
			// Token: 0x040006F9 RID: 1785
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR5 = 1048576U,
			// Token: 0x040006FA RID: 1786
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR6 = 2097152U,
			// Token: 0x040006FB RID: 1787
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR7 = 4194304U,
			// Token: 0x040006FC RID: 1788
			UNITYTLS_X509VERIFY_FLAG_USER_ERROR8 = 8388608U,
			// Token: 0x040006FD RID: 1789
			UNITYTLS_X509VERIFY_FLAG_UNKNOWN_ERROR = 134217728U
		}

		// Token: 0x0200001A RID: 26
		// (Invoke) Token: 0x0600005D RID: 93
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_x509verify_callback(void* userData, UnityTls.unitytls_x509_ref cert, UnityTls.unitytls_x509verify_result result, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x0200001B RID: 27
		public struct unitytls_tlsctx
		{
		}

		// Token: 0x0200001C RID: 28
		public struct unitytls_tlsctx_ref
		{
			// Token: 0x040006FE RID: 1790
			public ulong handle;
		}

		// Token: 0x0200001D RID: 29
		public struct unitytls_x509name
		{
		}

		// Token: 0x0200001E RID: 30
		public enum unitytls_ciphersuite : uint
		{
			// Token: 0x04000700 RID: 1792
			UNITYTLS_CIPHERSUITE_INVALID = 16777215U
		}

		// Token: 0x0200001F RID: 31
		public enum unitytls_protocol : uint
		{
			// Token: 0x04000702 RID: 1794
			UNITYTLS_PROTOCOL_TLS_1_0,
			// Token: 0x04000703 RID: 1795
			UNITYTLS_PROTOCOL_TLS_1_1,
			// Token: 0x04000704 RID: 1796
			UNITYTLS_PROTOCOL_TLS_1_2,
			// Token: 0x04000705 RID: 1797
			UNITYTLS_PROTOCOL_INVALID
		}

		// Token: 0x02000020 RID: 32
		public struct unitytls_tlsctx_protocolrange
		{
			// Token: 0x04000706 RID: 1798
			public UnityTls.unitytls_protocol min;

			// Token: 0x04000707 RID: 1799
			public UnityTls.unitytls_protocol max;
		}

		// Token: 0x02000021 RID: 33
		// (Invoke) Token: 0x06000061 RID: 97
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate IntPtr unitytls_tlsctx_write_callback(void* userData, byte* data, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x02000022 RID: 34
		// (Invoke) Token: 0x06000065 RID: 101
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate IntPtr unitytls_tlsctx_read_callback(void* userData, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x02000023 RID: 35
		// (Invoke) Token: 0x06000069 RID: 105
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void unitytls_tlsctx_trace_callback(void* userData, UnityTls.unitytls_tlsctx* ctx, byte* traceMessage, IntPtr traceMessageLen);

		// Token: 0x02000024 RID: 36
		// (Invoke) Token: 0x0600006D RID: 109
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate void unitytls_tlsctx_certificate_callback(void* userData, UnityTls.unitytls_tlsctx* ctx, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509name* caList, IntPtr caListLen, UnityTls.unitytls_x509list_ref* chain, UnityTls.unitytls_key_ref* key, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x02000025 RID: 37
		// (Invoke) Token: 0x06000071 RID: 113
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_tlsctx_x509verify_callback(void* userData, UnityTls.unitytls_x509list_ref chain, UnityTls.unitytls_errorstate* errorState);

		// Token: 0x02000026 RID: 38
		public struct unitytls_tlsctx_callbacks
		{
			// Token: 0x04000708 RID: 1800
			public UnityTls.unitytls_tlsctx_read_callback read;

			// Token: 0x04000709 RID: 1801
			public UnityTls.unitytls_tlsctx_write_callback write;

			// Token: 0x0400070A RID: 1802
			public unsafe void* data;
		}

		// Token: 0x02000027 RID: 39
		[StructLayout(LayoutKind.Sequential)]
		public class unitytls_interface_struct
		{
			// Token: 0x0400070B RID: 1803
			public readonly ulong UNITYTLS_INVALID_HANDLE;

			// Token: 0x0400070C RID: 1804
			public readonly UnityTls.unitytls_tlsctx_protocolrange UNITYTLS_TLSCTX_PROTOCOLRANGE_DEFAULT;

			// Token: 0x0400070D RID: 1805
			public UnityTls.unitytls_interface_struct.unitytls_errorstate_create_t unitytls_errorstate_create;

			// Token: 0x0400070E RID: 1806
			public UnityTls.unitytls_interface_struct.unitytls_errorstate_raise_error_t unitytls_errorstate_raise_error;

			// Token: 0x0400070F RID: 1807
			public UnityTls.unitytls_interface_struct.unitytls_key_get_ref_t unitytls_key_get_ref;

			// Token: 0x04000710 RID: 1808
			public UnityTls.unitytls_interface_struct.unitytls_key_parse_der_t unitytls_key_parse_der;

			// Token: 0x04000711 RID: 1809
			public UnityTls.unitytls_interface_struct.unitytls_key_parse_pem_t unitytls_key_parse_pem;

			// Token: 0x04000712 RID: 1810
			public UnityTls.unitytls_interface_struct.unitytls_key_free_t unitytls_key_free;

			// Token: 0x04000713 RID: 1811
			public UnityTls.unitytls_interface_struct.unitytls_x509_export_der_t unitytls_x509_export_der;

			// Token: 0x04000714 RID: 1812
			public UnityTls.unitytls_interface_struct.unitytls_x509list_get_ref_t unitytls_x509list_get_ref;

			// Token: 0x04000715 RID: 1813
			public UnityTls.unitytls_interface_struct.unitytls_x509list_get_x509_t unitytls_x509list_get_x509;

			// Token: 0x04000716 RID: 1814
			public UnityTls.unitytls_interface_struct.unitytls_x509list_create_t unitytls_x509list_create;

			// Token: 0x04000717 RID: 1815
			public UnityTls.unitytls_interface_struct.unitytls_x509list_append_t unitytls_x509list_append;

			// Token: 0x04000718 RID: 1816
			public UnityTls.unitytls_interface_struct.unitytls_x509list_append_der_t unitytls_x509list_append_der;

			// Token: 0x04000719 RID: 1817
			public UnityTls.unitytls_interface_struct.unitytls_x509list_append_der_t unitytls_x509list_append_pem;

			// Token: 0x0400071A RID: 1818
			public UnityTls.unitytls_interface_struct.unitytls_x509list_free_t unitytls_x509list_free;

			// Token: 0x0400071B RID: 1819
			public UnityTls.unitytls_interface_struct.unitytls_x509verify_default_ca_t unitytls_x509verify_default_ca;

			// Token: 0x0400071C RID: 1820
			public UnityTls.unitytls_interface_struct.unitytls_x509verify_explicit_ca_t unitytls_x509verify_explicit_ca;

			// Token: 0x0400071D RID: 1821
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_create_server_t unitytls_tlsctx_create_server;

			// Token: 0x0400071E RID: 1822
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_create_client_t unitytls_tlsctx_create_client;

			// Token: 0x0400071F RID: 1823
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_server_require_client_authentication_t unitytls_tlsctx_server_require_client_authentication;

			// Token: 0x04000720 RID: 1824
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_set_certificate_callback_t unitytls_tlsctx_set_certificate_callback;

			// Token: 0x04000721 RID: 1825
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_set_trace_callback_t unitytls_tlsctx_set_trace_callback;

			// Token: 0x04000722 RID: 1826
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_set_x509verify_callback_t unitytls_tlsctx_set_x509verify_callback;

			// Token: 0x04000723 RID: 1827
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_set_supported_ciphersuites_t unitytls_tlsctx_set_supported_ciphersuites;

			// Token: 0x04000724 RID: 1828
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_get_ciphersuite_t unitytls_tlsctx_get_ciphersuite;

			// Token: 0x04000725 RID: 1829
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_get_protocol_t unitytls_tlsctx_get_protocol;

			// Token: 0x04000726 RID: 1830
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_process_handshake_t unitytls_tlsctx_process_handshake;

			// Token: 0x04000727 RID: 1831
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_read_t unitytls_tlsctx_read;

			// Token: 0x04000728 RID: 1832
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_write_t unitytls_tlsctx_write;

			// Token: 0x04000729 RID: 1833
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_notify_close_t unitytls_tlsctx_notify_close;

			// Token: 0x0400072A RID: 1834
			public UnityTls.unitytls_interface_struct.unitytls_tlsctx_free_t unitytls_tlsctx_free;

			// Token: 0x0400072B RID: 1835
			public UnityTls.unitytls_interface_struct.unitytls_random_generate_bytes_t unitytls_random_generate_bytes;

			// Token: 0x02000028 RID: 40
			// (Invoke) Token: 0x06000076 RID: 118
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public delegate UnityTls.unitytls_errorstate unitytls_errorstate_create_t();

			// Token: 0x02000029 RID: 41
			// (Invoke) Token: 0x0600007A RID: 122
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_errorstate_raise_error_t(UnityTls.unitytls_errorstate* errorState, UnityTls.unitytls_error_code errorCode);

			// Token: 0x0200002A RID: 42
			// (Invoke) Token: 0x0600007E RID: 126
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_key_ref unitytls_key_get_ref_t(UnityTls.unitytls_key* key, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200002B RID: 43
			// (Invoke) Token: 0x06000082 RID: 130
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_key* unitytls_key_parse_der_t(byte* buffer, IntPtr bufferLen, byte* password, IntPtr passwordLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200002C RID: 44
			// (Invoke) Token: 0x06000086 RID: 134
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_key* unitytls_key_parse_pem_t(byte* buffer, IntPtr bufferLen, byte* password, IntPtr passwordLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200002D RID: 45
			// (Invoke) Token: 0x0600008A RID: 138
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_key_free_t(UnityTls.unitytls_key* key);

			// Token: 0x0200002E RID: 46
			// (Invoke) Token: 0x0600008E RID: 142
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate IntPtr unitytls_x509_export_der_t(UnityTls.unitytls_x509_ref cert, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200002F RID: 47
			// (Invoke) Token: 0x06000092 RID: 146
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509list_ref unitytls_x509list_get_ref_t(UnityTls.unitytls_x509list* list, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000030 RID: 48
			// (Invoke) Token: 0x06000096 RID: 150
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509_ref unitytls_x509list_get_x509_t(UnityTls.unitytls_x509list_ref list, IntPtr index, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000031 RID: 49
			// (Invoke) Token: 0x0600009A RID: 154
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509list* unitytls_x509list_create_t(UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000032 RID: 50
			// (Invoke) Token: 0x0600009E RID: 158
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_x509list_append_t(UnityTls.unitytls_x509list* list, UnityTls.unitytls_x509_ref cert, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000033 RID: 51
			// (Invoke) Token: 0x060000A2 RID: 162
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_x509list_append_der_t(UnityTls.unitytls_x509list* list, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000034 RID: 52
			// (Invoke) Token: 0x060000A6 RID: 166
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_x509list_append_pem_t(UnityTls.unitytls_x509list* list, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000035 RID: 53
			// (Invoke) Token: 0x060000AA RID: 170
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_x509list_free_t(UnityTls.unitytls_x509list* list);

			// Token: 0x02000036 RID: 54
			// (Invoke) Token: 0x060000AE RID: 174
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_x509verify_default_ca_t(UnityTls.unitytls_x509list_ref chain, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509verify_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000037 RID: 55
			// (Invoke) Token: 0x060000B2 RID: 178
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_x509verify_explicit_ca_t(UnityTls.unitytls_x509list_ref chain, UnityTls.unitytls_x509list_ref trustCA, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509verify_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000038 RID: 56
			// (Invoke) Token: 0x060000B6 RID: 182
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_tlsctx* unitytls_tlsctx_create_server_t(UnityTls.unitytls_tlsctx_protocolrange supportedProtocols, UnityTls.unitytls_tlsctx_callbacks callbacks, ulong certChain, ulong leafCertificateKey, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000039 RID: 57
			// (Invoke) Token: 0x060000BA RID: 186
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_tlsctx* unitytls_tlsctx_create_client_t(UnityTls.unitytls_tlsctx_protocolrange supportedProtocols, UnityTls.unitytls_tlsctx_callbacks callbacks, byte* cn, IntPtr cnLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200003A RID: 58
			// (Invoke) Token: 0x060000BE RID: 190
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_server_require_client_authentication_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_x509list_ref clientAuthCAList, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200003B RID: 59
			// (Invoke) Token: 0x060000C2 RID: 194
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_set_certificate_callback_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_tlsctx_certificate_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200003C RID: 60
			// (Invoke) Token: 0x060000C6 RID: 198
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_set_trace_callback_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_tlsctx_trace_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200003D RID: 61
			// (Invoke) Token: 0x060000CA RID: 202
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_set_x509verify_callback_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_tlsctx_x509verify_callback cb, void* userData, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200003E RID: 62
			// (Invoke) Token: 0x060000CE RID: 206
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_set_supported_ciphersuites_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_ciphersuite* supportedCiphersuites, IntPtr supportedCiphersuitesLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x0200003F RID: 63
			// (Invoke) Token: 0x060000D2 RID: 210
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_ciphersuite unitytls_tlsctx_get_ciphersuite_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000040 RID: 64
			// (Invoke) Token: 0x060000D6 RID: 214
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_protocol unitytls_tlsctx_get_protocol_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000041 RID: 65
			// (Invoke) Token: 0x060000DA RID: 218
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate UnityTls.unitytls_x509verify_result unitytls_tlsctx_process_handshake_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000042 RID: 66
			// (Invoke) Token: 0x060000DE RID: 222
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate IntPtr unitytls_tlsctx_read_t(UnityTls.unitytls_tlsctx* ctx, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000043 RID: 67
			// (Invoke) Token: 0x060000E2 RID: 226
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate IntPtr unitytls_tlsctx_write_t(UnityTls.unitytls_tlsctx* ctx, byte* data, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000044 RID: 68
			// (Invoke) Token: 0x060000E6 RID: 230
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_notify_close_t(UnityTls.unitytls_tlsctx* ctx, UnityTls.unitytls_errorstate* errorState);

			// Token: 0x02000045 RID: 69
			// (Invoke) Token: 0x060000EA RID: 234
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_tlsctx_free_t(UnityTls.unitytls_tlsctx* ctx);

			// Token: 0x02000046 RID: 70
			// (Invoke) Token: 0x060000EE RID: 238
			[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
			public unsafe delegate void unitytls_random_generate_bytes_t(byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState);
		}
	}
}
