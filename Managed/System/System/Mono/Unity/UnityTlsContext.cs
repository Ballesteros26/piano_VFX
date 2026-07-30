using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Mono.Net.Security;
using Mono.Security.Cryptography;
using Mono.Security.Interface;
using Mono.Util;

namespace Mono.Unity
{
	// Token: 0x02000047 RID: 71
	internal class UnityTlsContext : MobileTlsContext
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00002E20 File Offset: 0x00001020
		public unsafe UnityTlsContext(MobileAuthenticatedStream parent, bool serverMode, string targetHost, SslProtocols enabledProtocols, X509Certificate serverCertificate, X509CertificateCollection clientCertificates, bool askForClientCert)
			: base(parent, serverMode, targetHost, enabledProtocols, serverCertificate, clientCertificates, askForClientCert)
		{
			this.handle = GCHandle.Alloc(this);
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			UnityTls.unitytls_tlsctx_protocolrange unitytls_tlsctx_protocolrange = new UnityTls.unitytls_tlsctx_protocolrange
			{
				min = UnityTlsConversions.GetMinProtocol(enabledProtocols),
				max = UnityTlsConversions.GetMaxProtocol(enabledProtocols)
			};
			this.readCallback = new UnityTls.unitytls_tlsctx_read_callback(UnityTlsContext.ReadCallback);
			this.writeCallback = new UnityTls.unitytls_tlsctx_write_callback(UnityTlsContext.WriteCallback);
			UnityTls.unitytls_tlsctx_callbacks unitytls_tlsctx_callbacks = new UnityTls.unitytls_tlsctx_callbacks
			{
				write = this.writeCallback,
				read = this.readCallback,
				data = (void*)((IntPtr)this.handle)
			};
			if (serverMode)
			{
				UnityTls.unitytls_x509list* ptr;
				UnityTls.unitytls_key* ptr2;
				UnityTlsContext.ExtractNativeKeyAndChainFromManagedCertificate(serverCertificate, &unitytls_errorstate, out ptr, out ptr2);
				try
				{
					UnityTls.unitytls_x509list_ref unitytls_x509list_ref = UnityTls.NativeInterface.unitytls_x509list_get_ref(ptr, &unitytls_errorstate);
					UnityTls.unitytls_key_ref unitytls_key_ref = UnityTls.NativeInterface.unitytls_key_get_ref(ptr2, &unitytls_errorstate);
					Mono.Unity.Debug.CheckAndThrow(unitytls_errorstate, "Failed to parse server key/certificate", AlertDescription.InternalError);
					this.tlsContext = UnityTls.NativeInterface.unitytls_tlsctx_create_server(unitytls_tlsctx_protocolrange, unitytls_tlsctx_callbacks, unitytls_x509list_ref.handle, unitytls_key_ref.handle, &unitytls_errorstate);
					if (askForClientCert)
					{
						UnityTls.unitytls_x509list* ptr3 = null;
						try
						{
							ptr3 = UnityTls.NativeInterface.unitytls_x509list_create(&unitytls_errorstate);
							UnityTls.unitytls_x509list_ref unitytls_x509list_ref2 = UnityTls.NativeInterface.unitytls_x509list_get_ref(ptr3, &unitytls_errorstate);
							UnityTls.NativeInterface.unitytls_tlsctx_server_require_client_authentication(this.tlsContext, unitytls_x509list_ref2, &unitytls_errorstate);
						}
						finally
						{
							UnityTls.NativeInterface.unitytls_x509list_free(ptr3);
						}
					}
					goto IL_025E;
				}
				finally
				{
					UnityTls.NativeInterface.unitytls_x509list_free(ptr);
					UnityTls.NativeInterface.unitytls_key_free(ptr2);
				}
			}
			byte[] bytes = Encoding.UTF8.GetBytes(targetHost);
			byte[] array;
			byte* ptr4;
			if ((array = bytes) == null || array.Length == 0)
			{
				ptr4 = null;
			}
			else
			{
				ptr4 = &array[0];
			}
			this.tlsContext = UnityTls.NativeInterface.unitytls_tlsctx_create_client(unitytls_tlsctx_protocolrange, unitytls_tlsctx_callbacks, ptr4, (IntPtr)bytes.Length, &unitytls_errorstate);
			array = null;
			this.certificateCallback = new UnityTls.unitytls_tlsctx_certificate_callback(UnityTlsContext.CertificateCallback);
			UnityTls.NativeInterface.unitytls_tlsctx_set_certificate_callback(this.tlsContext, this.certificateCallback, (void*)((IntPtr)this.handle), &unitytls_errorstate);
			IL_025E:
			this.verifyCallback = new UnityTls.unitytls_tlsctx_x509verify_callback(UnityTlsContext.VerifyCallback);
			UnityTls.NativeInterface.unitytls_tlsctx_set_x509verify_callback(this.tlsContext, this.verifyCallback, (void*)((IntPtr)this.handle), &unitytls_errorstate);
			Mono.Unity.Debug.CheckAndThrow(unitytls_errorstate, "Failed to create UnityTls context", AlertDescription.InternalError);
			this.hasContext = true;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000030FC File Offset: 0x000012FC
		private unsafe static void ExtractNativeKeyAndChainFromManagedCertificate(X509Certificate cert, UnityTls.unitytls_errorstate* errorState, out UnityTls.unitytls_x509list* nativeCertChain, out UnityTls.unitytls_key* nativeKey)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			X509Certificate2 x509Certificate = cert as X509Certificate2;
			if (x509Certificate == null || x509Certificate.PrivateKey == null)
			{
				throw new ArgumentException("Certificate does not have a private key", "cert");
			}
			nativeCertChain = (IntPtr)((UIntPtr)0);
			nativeKey = (IntPtr)((UIntPtr)0);
			try
			{
				nativeCertChain = UnityTls.NativeInterface.unitytls_x509list_create(errorState);
				CertHelper.AddCertificateToNativeChain(nativeCertChain, cert, errorState);
				byte[] array = Mono.Security.Cryptography.PKCS8.PrivateKeyInfo.Encode(x509Certificate.PrivateKey);
				try
				{
					byte[] array2;
					byte* ptr;
					if ((array2 = array) == null || array2.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array2[0];
					}
					nativeKey = UnityTls.NativeInterface.unitytls_key_parse_der(ptr, (IntPtr)array.Length, null, (IntPtr)0, errorState);
				}
				finally
				{
					byte[] array2 = null;
				}
			}
			catch
			{
				UnityTls.NativeInterface.unitytls_x509list_free(nativeCertChain);
				UnityTls.NativeInterface.unitytls_key_free(nativeKey);
				throw;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000031EC File Offset: 0x000013EC
		public override bool HasContext
		{
			get
			{
				return this.hasContext;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000031F4 File Offset: 0x000013F4
		public override bool IsAuthenticated
		{
			get
			{
				return this.isAuthenticated;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x000031FC File Offset: 0x000013FC
		public override MonoTlsConnectionInfo ConnectionInfo
		{
			get
			{
				return this.connectioninfo;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003204 File Offset: 0x00001404
		internal override bool IsRemoteCertificateAvailable
		{
			get
			{
				return this.remoteCertificate != null;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000320F File Offset: 0x0000140F
		internal override X509Certificate LocalClientCertificate
		{
			get
			{
				return this.localClientCertificate;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00003217 File Offset: 0x00001417
		public override X509Certificate RemoteCertificate
		{
			get
			{
				return this.remoteCertificate;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000321F File Offset: 0x0000141F
		public override TlsProtocols NegotiatedProtocol
		{
			get
			{
				return this.ConnectionInfo.ProtocolVersion;
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void Flush()
		{
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000322C File Offset: 0x0000142C
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		public unsafe override ValueTuple<int, bool> Read(byte[] buffer, int offset, int count)
		{
			this.lastException = null;
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			int num;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				num = (int)UnityTls.NativeInterface.unitytls_tlsctx_read(this.tlsContext, ptr + offset, (IntPtr)count, &unitytls_errorstate);
			}
			if (this.lastException != null)
			{
				throw this.lastException;
			}
			UnityTls.unitytls_error_code code = unitytls_errorstate.code;
			if (code == UnityTls.unitytls_error_code.UNITYTLS_SUCCESS)
			{
				return new ValueTuple<int, bool>(num, num < count);
			}
			if (code == UnityTls.unitytls_error_code.UNITYTLS_STREAM_CLOSED)
			{
				return new ValueTuple<int, bool>(0, false);
			}
			if (code != UnityTls.unitytls_error_code.UNITYTLS_USER_WOULD_BLOCK)
			{
				if (!this.closedGraceful)
				{
					Mono.Unity.Debug.CheckAndThrow(unitytls_errorstate, "Failed to read data to TLS context", AlertDescription.InternalError);
				}
				return new ValueTuple<int, bool>(0, false);
			}
			return new ValueTuple<int, bool>(num, true);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000032F8 File Offset: 0x000014F8
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		public unsafe override ValueTuple<int, bool> Write(byte[] buffer, int offset, int count)
		{
			this.lastException = null;
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			int num;
			fixed (byte[] array = buffer)
			{
				byte* ptr;
				if (buffer == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				num = (int)UnityTls.NativeInterface.unitytls_tlsctx_write(this.tlsContext, ptr + offset, (IntPtr)count, &unitytls_errorstate);
			}
			if (this.lastException != null)
			{
				throw this.lastException;
			}
			UnityTls.unitytls_error_code code = unitytls_errorstate.code;
			if (code == UnityTls.unitytls_error_code.UNITYTLS_SUCCESS)
			{
				return new ValueTuple<int, bool>(num, num < count);
			}
			if (code == UnityTls.unitytls_error_code.UNITYTLS_STREAM_CLOSED)
			{
				return new ValueTuple<int, bool>(0, false);
			}
			if (code != UnityTls.unitytls_error_code.UNITYTLS_USER_WOULD_BLOCK)
			{
				Mono.Unity.Debug.CheckAndThrow(unitytls_errorstate, "Failed to write data to TLS context", AlertDescription.InternalError);
				return new ValueTuple<int, bool>(0, false);
			}
			return new ValueTuple<int, bool>(num, true);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000033BC File Offset: 0x000015BC
		public unsafe override void Shutdown()
		{
			if (base.Settings != null && base.Settings.SendCloseNotify)
			{
				UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
				UnityTls.NativeInterface.unitytls_tlsctx_notify_close(this.tlsContext, &unitytls_errorstate);
			}
			UnityTls.NativeInterface.unitytls_x509list_free(this.requestedClientCertChain);
			UnityTls.NativeInterface.unitytls_key_free(this.requestedClientKey);
			UnityTls.NativeInterface.unitytls_tlsctx_free(this.tlsContext);
			this.tlsContext = null;
			this.hasContext = false;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00003454 File Offset: 0x00001654
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.Shutdown();
					this.localClientCertificate = null;
					this.remoteCertificate = null;
					if (this.localClientCertificate != null)
					{
						this.localClientCertificate.Dispose();
						this.localClientCertificate = null;
					}
					if (this.remoteCertificate != null)
					{
						this.remoteCertificate.Dispose();
						this.remoteCertificate = null;
					}
					this.connectioninfo = null;
					this.isAuthenticated = false;
					this.hasContext = false;
				}
				this.handle.Free();
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000034E8 File Offset: 0x000016E8
		public unsafe override void StartHandshake()
		{
			if (base.Settings != null && base.Settings.EnabledCiphers != null)
			{
				UnityTls.unitytls_ciphersuite[] array = new UnityTls.unitytls_ciphersuite[base.Settings.EnabledCiphers.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (UnityTls.unitytls_ciphersuite)base.Settings.EnabledCiphers[i];
				}
				UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
				UnityTls.unitytls_ciphersuite[] array2;
				UnityTls.unitytls_ciphersuite* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				UnityTls.NativeInterface.unitytls_tlsctx_set_supported_ciphersuites(this.tlsContext, ptr, (IntPtr)array.Length, &unitytls_errorstate);
				array2 = null;
				Mono.Unity.Debug.CheckAndThrow(unitytls_errorstate, "Failed to set list of supported ciphers", AlertDescription.HandshakeFailure);
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000035A0 File Offset: 0x000017A0
		public unsafe override bool ProcessHandshake()
		{
			this.lastException = null;
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			UnityTls.unitytls_x509verify_result unitytls_x509verify_result = UnityTls.NativeInterface.unitytls_tlsctx_process_handshake(this.tlsContext, &unitytls_errorstate);
			if (unitytls_errorstate.code == UnityTls.unitytls_error_code.UNITYTLS_USER_WOULD_BLOCK)
			{
				return false;
			}
			if (this.lastException != null)
			{
				throw this.lastException;
			}
			if (base.IsServer && unitytls_x509verify_result == (UnityTls.unitytls_x509verify_result)2147483648U)
			{
				Mono.Unity.Debug.CheckAndThrow(unitytls_errorstate, "Handshake failed", AlertDescription.HandshakeFailure);
				if (!base.ValidateCertificate(null, null))
				{
					throw new TlsException(AlertDescription.HandshakeFailure, "Verification failure during handshake");
				}
			}
			else
			{
				Mono.Unity.Debug.CheckAndThrow(unitytls_errorstate, unitytls_x509verify_result, "Handshake failed", AlertDescription.HandshakeFailure);
			}
			return true;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00003640 File Offset: 0x00001840
		public unsafe override void FinishHandshake()
		{
			UnityTls.unitytls_errorstate unitytls_errorstate = UnityTls.NativeInterface.unitytls_errorstate_create();
			UnityTls.unitytls_ciphersuite unitytls_ciphersuite = UnityTls.NativeInterface.unitytls_tlsctx_get_ciphersuite(this.tlsContext, &unitytls_errorstate);
			UnityTls.unitytls_protocol unitytls_protocol = UnityTls.NativeInterface.unitytls_tlsctx_get_protocol(this.tlsContext, &unitytls_errorstate);
			this.connectioninfo = new MonoTlsConnectionInfo
			{
				CipherSuiteCode = (CipherSuiteCode)unitytls_ciphersuite,
				ProtocolVersion = UnityTlsConversions.ConvertProtocolVersion(unitytls_protocol),
				PeerDomainName = base.ServerName
			};
			this.isAuthenticated = true;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000036C4 File Offset: 0x000018C4
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_tlsctx_write_callback))]
		private unsafe static IntPtr WriteCallback(void* userData, byte* data, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState)
		{
			return ((UnityTlsContext)((GCHandle)((IntPtr)userData)).Target).WriteCallback(data, bufferLen, errorState);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000036F4 File Offset: 0x000018F4
		private unsafe IntPtr WriteCallback(byte* data, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState)
		{
			IntPtr intPtr;
			try
			{
				if (this.writeBuffer == null || this.writeBuffer.Length < (int)bufferLen)
				{
					this.writeBuffer = new byte[(int)bufferLen];
				}
				Marshal.Copy((IntPtr)((void*)data), this.writeBuffer, 0, (int)bufferLen);
				if (!base.Parent.InternalWrite(this.writeBuffer, 0, (int)bufferLen))
				{
					UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_WRITE_FAILED);
					intPtr = (IntPtr)0;
				}
				else
				{
					intPtr = bufferLen;
				}
			}
			catch (Exception ex)
			{
				UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_UNKNOWN_ERROR);
				if (this.lastException == null)
				{
					this.lastException = ex;
				}
				intPtr = (IntPtr)0;
			}
			return intPtr;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000037C0 File Offset: 0x000019C0
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_tlsctx_read_callback))]
		private unsafe static IntPtr ReadCallback(void* userData, byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState)
		{
			return ((UnityTlsContext)((GCHandle)((IntPtr)userData)).Target).ReadCallback(buffer, bufferLen, errorState);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000037F0 File Offset: 0x000019F0
		private unsafe IntPtr ReadCallback(byte* buffer, IntPtr bufferLen, UnityTls.unitytls_errorstate* errorState)
		{
			IntPtr intPtr;
			try
			{
				if (this.readBuffer == null || this.readBuffer.Length < (int)bufferLen)
				{
					this.readBuffer = new byte[(int)bufferLen];
				}
				bool flag;
				int num = base.Parent.InternalRead(this.readBuffer, 0, (int)bufferLen, out flag);
				if (num < 0)
				{
					UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_READ_FAILED);
				}
				else if (num > 0)
				{
					Marshal.Copy(this.readBuffer, 0, (IntPtr)((void*)buffer), (int)bufferLen);
				}
				else if (flag)
				{
					UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_WOULD_BLOCK);
				}
				else
				{
					this.closedGraceful = true;
					UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_READ_FAILED);
				}
				intPtr = (IntPtr)num;
			}
			catch (Exception ex)
			{
				UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_UNKNOWN_ERROR);
				if (this.lastException == null)
				{
					this.lastException = ex;
				}
				intPtr = (IntPtr)0;
			}
			return intPtr;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000038F8 File Offset: 0x00001AF8
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_tlsctx_x509verify_callback))]
		private unsafe static UnityTls.unitytls_x509verify_result VerifyCallback(void* userData, UnityTls.unitytls_x509list_ref chain, UnityTls.unitytls_errorstate* errorState)
		{
			return ((UnityTlsContext)((GCHandle)((IntPtr)userData)).Target).VerifyCallback(chain, errorState);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003924 File Offset: 0x00001B24
		private unsafe UnityTls.unitytls_x509verify_result VerifyCallback(UnityTls.unitytls_x509list_ref chain, UnityTls.unitytls_errorstate* errorState)
		{
			UnityTls.unitytls_x509verify_result unitytls_x509verify_result;
			try
			{
				using (X509ChainImplUnityTls x509ChainImplUnityTls = new X509ChainImplUnityTls(chain))
				{
					using (X509Chain x509Chain = new X509Chain(x509ChainImplUnityTls))
					{
						this.remoteCertificate = x509Chain.ChainElements[0].Certificate;
						if (base.ValidateCertificate(this.remoteCertificate, x509Chain))
						{
							unitytls_x509verify_result = UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_SUCCESS;
						}
						else
						{
							unitytls_x509verify_result = UnityTls.unitytls_x509verify_result.UNITYTLS_X509VERIFY_FLAG_NOT_TRUSTED;
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (this.lastException == null)
				{
					this.lastException = ex;
				}
				unitytls_x509verify_result = (UnityTls.unitytls_x509verify_result)4294967295U;
			}
			return unitytls_x509verify_result;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000039C0 File Offset: 0x00001BC0
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_tlsctx_certificate_callback))]
		private unsafe static void CertificateCallback(void* userData, UnityTls.unitytls_tlsctx* ctx, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509name* caList, IntPtr caListLen, UnityTls.unitytls_x509list_ref* chain, UnityTls.unitytls_key_ref* key, UnityTls.unitytls_errorstate* errorState)
		{
			((UnityTlsContext)((GCHandle)((IntPtr)userData)).Target).CertificateCallback(ctx, cn, cnLen, caList, caListLen, chain, key, errorState);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000039F8 File Offset: 0x00001BF8
		private unsafe void CertificateCallback(UnityTls.unitytls_tlsctx* ctx, byte* cn, IntPtr cnLen, UnityTls.unitytls_x509name* caList, IntPtr caListLen, UnityTls.unitytls_x509list_ref* chain, UnityTls.unitytls_key_ref* key, UnityTls.unitytls_errorstate* errorState)
		{
			try
			{
				if (this.remoteCertificate == null)
				{
					throw new TlsException(AlertDescription.InternalError, "Cannot request client certificate before receiving one from the server.");
				}
				this.localClientCertificate = base.SelectClientCertificate(this.remoteCertificate, null);
				if (this.localClientCertificate == null)
				{
					*chain = new UnityTls.unitytls_x509list_ref
					{
						handle = UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE
					};
					*key = new UnityTls.unitytls_key_ref
					{
						handle = UnityTls.NativeInterface.UNITYTLS_INVALID_HANDLE
					};
				}
				else
				{
					UnityTls.NativeInterface.unitytls_x509list_free(this.requestedClientCertChain);
					UnityTls.NativeInterface.unitytls_key_free(this.requestedClientKey);
					UnityTlsContext.ExtractNativeKeyAndChainFromManagedCertificate(this.localClientCertificate, errorState, out this.requestedClientCertChain, out this.requestedClientKey);
					*chain = UnityTls.NativeInterface.unitytls_x509list_get_ref(this.requestedClientCertChain, errorState);
					*key = UnityTls.NativeInterface.unitytls_key_get_ref(this.requestedClientKey, errorState);
				}
				Mono.Unity.Debug.CheckAndThrow(*errorState, "Failed to retrieve certificates on request.", AlertDescription.HandshakeFailure);
			}
			catch (Exception ex)
			{
				UnityTls.NativeInterface.unitytls_errorstate_raise_error(errorState, UnityTls.unitytls_error_code.UNITYTLS_USER_UNKNOWN_ERROR);
				if (this.lastException == null)
				{
					this.lastException = ex;
				}
			}
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003B54 File Offset: 0x00001D54
		[MonoPInvokeCallback(typeof(UnityTls.unitytls_tlsctx_trace_callback))]
		private unsafe static void TraceCallback(void* userData, UnityTls.unitytls_tlsctx* ctx, byte* traceMessage, IntPtr traceMessageLen)
		{
			Console.Write(Encoding.UTF8.GetString(traceMessage, (int)traceMessageLen));
		}

		// Token: 0x0400072C RID: 1836
		private const bool ActivateTracing = false;

		// Token: 0x0400072D RID: 1837
		private unsafe UnityTls.unitytls_tlsctx* tlsContext = null;

		// Token: 0x0400072E RID: 1838
		private unsafe UnityTls.unitytls_x509list* requestedClientCertChain = null;

		// Token: 0x0400072F RID: 1839
		private unsafe UnityTls.unitytls_key* requestedClientKey = null;

		// Token: 0x04000730 RID: 1840
		private UnityTls.unitytls_tlsctx_read_callback readCallback;

		// Token: 0x04000731 RID: 1841
		private UnityTls.unitytls_tlsctx_write_callback writeCallback;

		// Token: 0x04000732 RID: 1842
		private UnityTls.unitytls_tlsctx_trace_callback traceCallback;

		// Token: 0x04000733 RID: 1843
		private UnityTls.unitytls_tlsctx_certificate_callback certificateCallback;

		// Token: 0x04000734 RID: 1844
		private UnityTls.unitytls_tlsctx_x509verify_callback verifyCallback;

		// Token: 0x04000735 RID: 1845
		private X509Certificate localClientCertificate;

		// Token: 0x04000736 RID: 1846
		private X509Certificate remoteCertificate;

		// Token: 0x04000737 RID: 1847
		private MonoTlsConnectionInfo connectioninfo;

		// Token: 0x04000738 RID: 1848
		private bool isAuthenticated;

		// Token: 0x04000739 RID: 1849
		private bool hasContext;

		// Token: 0x0400073A RID: 1850
		private bool closedGraceful;

		// Token: 0x0400073B RID: 1851
		private byte[] writeBuffer;

		// Token: 0x0400073C RID: 1852
		private byte[] readBuffer;

		// Token: 0x0400073D RID: 1853
		private GCHandle handle;

		// Token: 0x0400073E RID: 1854
		private Exception lastException;
	}
}
