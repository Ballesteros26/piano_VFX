using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Mono.Net;
using Mono.Net.Security;
using Mono.Security.Interface;
using Mono.Util;

namespace Mono.AppleTls
{
	// Token: 0x020000A7 RID: 167
	internal class AppleTlsContext : MobileTlsContext
	{
		// Token: 0x060003BD RID: 957 RVA: 0x0000C370 File Offset: 0x0000A570
		public AppleTlsContext(MobileAuthenticatedStream parent, bool serverMode, string targetHost, SslProtocols enabledProtocols, X509Certificate serverCertificate, X509CertificateCollection clientCertificates, bool askForClientCert)
			: base(parent, serverMode, targetHost, enabledProtocols, serverCertificate, clientCertificates, askForClientCert)
		{
			this.handle = GCHandle.Alloc(this, GCHandleType.Weak);
			this.readFunc = new SslReadFunc(AppleTlsContext.NativeReadCallback);
			this.writeFunc = new SslWriteFunc(AppleTlsContext.NativeWriteCallback);
			if (base.IsServer && serverCertificate == null)
			{
				throw new ArgumentNullException("serverCertificate");
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0000C3D6 File Offset: 0x0000A5D6
		public IntPtr Handle
		{
			get
			{
				if (!this.HasContext)
				{
					throw new ObjectDisposedException("AppleTlsContext");
				}
				return this.context;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000C3F1 File Offset: 0x0000A5F1
		public override bool HasContext
		{
			get
			{
				return !this.disposed && this.context != IntPtr.Zero;
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000C410 File Offset: 0x0000A610
		private void CheckStatusAndThrow(SslStatus status, params SslStatus[] acceptable)
		{
			Exception ex = Interlocked.Exchange<Exception>(ref this.lastException, null);
			if (ex != null)
			{
				throw ex;
			}
			if (status == SslStatus.Success || Array.IndexOf<SslStatus>(acceptable, status) > -1)
			{
				return;
			}
			switch (status)
			{
			case SslStatus.CertNotYetValid:
			case SslStatus.CertExpired:
				throw new TlsException(AlertDescription.CertificateExpired);
			case SslStatus.NoRootCert:
			case SslStatus.UnknownRootCert:
			case SslStatus.XCertChainInvalid:
				throw new TlsException(AlertDescription.CertificateUnknown, status.ToString());
			case SslStatus.ModuleAttach:
			case SslStatus.Internal:
			case SslStatus.Crypto:
				break;
			case SslStatus.BadCert:
				throw new TlsException(AlertDescription.BadCertificate);
			case SslStatus.ClosedAbort:
				throw new IOException("Connection closed.");
			default:
				if (status == SslStatus.Protocol)
				{
					throw new TlsException(AlertDescription.ProtocolVersion);
				}
				break;
			}
			throw new TlsException(AlertDescription.InternalError, "Unknown Secure Transport error `{0}'.", new object[] { status });
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000C4CE File Offset: 0x0000A6CE
		public override bool IsAuthenticated
		{
			get
			{
				return this.isAuthenticated;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		public override void StartHandshake()
		{
			if (Interlocked.CompareExchange(ref this.handshakeStarted, 1, 1) != 0)
			{
				throw new InvalidOperationException();
			}
			this.InitializeConnection();
			this.SetSessionOption(SslSessionOption.BreakOnCertRequested, true);
			this.SetSessionOption(SslSessionOption.BreakOnClientAuth, true);
			this.SetSessionOption(SslSessionOption.BreakOnServerAuth, true);
			if (base.IsServer)
			{
				SecCertificate[] array;
				this.serverIdentity = AppleCertificateHelper.GetIdentity(base.LocalServerCertificate, out array);
				if (this.serverIdentity == null)
				{
					throw new AuthenticationException("Unable to get server certificate from keychain.");
				}
				this.SetCertificate(this.serverIdentity, array);
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Dispose();
				}
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000C569 File Offset: 0x0000A769
		public override void FinishHandshake()
		{
			this.InitializeSession();
			this.isAuthenticated = true;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000027E8 File Offset: 0x000009E8
		public override void Flush()
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000C578 File Offset: 0x0000A778
		public override bool ProcessHandshake()
		{
			if (this.handshakeFinished)
			{
				throw new NotSupportedException("Handshake already finished.");
			}
			for (;;)
			{
				this.lastException = null;
				SslStatus sslStatus = AppleTlsContext.SSLHandshake(this.Handle);
				this.CheckStatusAndThrow(sslStatus, new SslStatus[]
				{
					SslStatus.WouldBlock,
					SslStatus.PeerAuthCompleted,
					SslStatus.PeerClientCertRequested
				});
				if (sslStatus == SslStatus.PeerAuthCompleted)
				{
					this.RequirePeerTrust();
				}
				else if (sslStatus == SslStatus.PeerClientCertRequested)
				{
					this.RequirePeerTrust();
					if (this.remoteCertificate == null)
					{
						break;
					}
					this.localClientCertificate = base.SelectClientCertificate(this.remoteCertificate, null);
					if (this.localClientCertificate != null)
					{
						this.clientIdentity = AppleCertificateHelper.GetIdentity(this.localClientCertificate);
						if (this.clientIdentity == null)
						{
							goto Block_6;
						}
						this.SetCertificate(this.clientIdentity, new SecCertificate[0]);
					}
				}
				else
				{
					if (sslStatus == SslStatus.WouldBlock)
					{
						return false;
					}
					if (sslStatus == SslStatus.Success)
					{
						goto Block_8;
					}
				}
			}
			throw new TlsException(AlertDescription.InternalError, "Cannot request client certificate before receiving one from the server.");
			Block_6:
			throw new TlsException(AlertDescription.CertificateUnknown);
			Block_8:
			this.handshakeFinished = true;
			return true;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000C661 File Offset: 0x0000A861
		private void RequirePeerTrust()
		{
			if (!this.havePeerTrust)
			{
				this.EvaluateTrust();
				this.havePeerTrust = true;
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000C678 File Offset: 0x0000A878
		private void EvaluateTrust()
		{
			this.InitializeSession();
			SecTrust secTrust = null;
			X509CertificateCollection x509CertificateCollection = null;
			bool flag;
			try
			{
				secTrust = this.GetPeerTrust(!base.IsServer);
				if (secTrust == null || secTrust.Count == 0)
				{
					this.remoteCertificate = null;
					if (!base.IsServer)
					{
						throw new TlsException(AlertDescription.CertificateUnknown);
					}
					x509CertificateCollection = null;
				}
				else
				{
					int count = secTrust.Count;
					x509CertificateCollection = new X509CertificateCollection();
					for (int i = 0; i < secTrust.Count; i++)
					{
						x509CertificateCollection.Add(secTrust.GetCertificate(i));
					}
					this.remoteCertificate = new X509Certificate(x509CertificateCollection[0]);
				}
				flag = base.ValidateCertificate(x509CertificateCollection);
			}
			catch (Exception)
			{
				throw new TlsException(AlertDescription.CertificateUnknown, "Certificate validation threw exception.");
			}
			finally
			{
				if (secTrust != null)
				{
					secTrust.Dispose();
				}
				if (x509CertificateCollection != null)
				{
					for (int j = 0; j < x509CertificateCollection.Count; j++)
					{
						x509CertificateCollection[j].Dispose();
					}
				}
			}
			if (!flag)
			{
				throw new TlsException(AlertDescription.CertificateUnknown);
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000C774 File Offset: 0x0000A974
		private void InitializeConnection()
		{
			this.context = AppleTlsContext.SSLCreateContext(IntPtr.Zero, base.IsServer ? SslProtocolSide.Server : SslProtocolSide.Client, SslConnectionType.Stream);
			SslStatus sslStatus = AppleTlsContext.SSLSetIOFuncs(this.Handle, this.readFunc, this.writeFunc);
			this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
			sslStatus = AppleTlsContext.SSLSetConnection(this.Handle, GCHandle.ToIntPtr(this.handle));
			this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
			if ((base.EnabledProtocols & SslProtocols.Tls) != SslProtocols.None)
			{
				this.MinProtocol = SslProtocol.Tls_1_0;
			}
			else if ((base.EnabledProtocols & SslProtocols.Tls11) != SslProtocols.None)
			{
				this.MinProtocol = SslProtocol.Tls_1_1;
			}
			else
			{
				this.MinProtocol = SslProtocol.Tls_1_2;
			}
			if ((base.EnabledProtocols & SslProtocols.Tls12) != SslProtocols.None)
			{
				this.MaxProtocol = SslProtocol.Tls_1_2;
			}
			else if ((base.EnabledProtocols & SslProtocols.Tls11) != SslProtocols.None)
			{
				this.MaxProtocol = SslProtocol.Tls_1_1;
			}
			else
			{
				this.MaxProtocol = SslProtocol.Tls_1_0;
			}
			if (base.Settings != null && base.Settings.EnabledCiphers != null)
			{
				SslCipherSuite[] array = new SslCipherSuite[base.Settings.EnabledCiphers.Length];
				for (int i = 0; i < base.Settings.EnabledCiphers.Length; i++)
				{
					array[i] = (SslCipherSuite)base.Settings.EnabledCiphers[i];
				}
				this.SetEnabledCiphers(array);
			}
			if (base.AskForClientCertificate)
			{
				this.SetClientSideAuthenticate(SslAuthenticate.Try);
			}
			IPAddress ipaddress;
			if (!base.IsServer && !string.IsNullOrEmpty(base.TargetHost) && !IPAddress.TryParse(base.TargetHost, out ipaddress))
			{
				this.PeerDomainName = base.ServerName;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
		private void InitializeSession()
		{
			if (this.connectionInfo != null)
			{
				return;
			}
			SslCipherSuite negotiatedCipher = this.NegotiatedCipher;
			SslProtocol negotiatedProtocolVersion = this.GetNegotiatedProtocolVersion();
			this.connectionInfo = new MonoTlsConnectionInfo
			{
				CipherSuiteCode = (CipherSuiteCode)negotiatedCipher,
				ProtocolVersion = AppleTlsContext.GetProtocol(negotiatedProtocolVersion),
				PeerDomainName = this.PeerDomainName
			};
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000C936 File Offset: 0x0000AB36
		private static TlsProtocols GetProtocol(SslProtocol protocol)
		{
			switch (protocol)
			{
			case SslProtocol.Tls_1_0:
				return TlsProtocols.Tls10;
			case SslProtocol.Tls_1_1:
				return TlsProtocols.Tls11;
			case SslProtocol.Tls_1_2:
				return TlsProtocols.Tls12;
			}
			throw new NotSupportedException();
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000C96D File Offset: 0x0000AB6D
		public override MonoTlsConnectionInfo ConnectionInfo
		{
			get
			{
				return this.connectionInfo;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000C975 File Offset: 0x0000AB75
		internal override bool IsRemoteCertificateAvailable
		{
			get
			{
				return this.remoteCertificate != null;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000C980 File Offset: 0x0000AB80
		internal override X509Certificate LocalClientCertificate
		{
			get
			{
				return this.localClientCertificate;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000C988 File Offset: 0x0000AB88
		public override X509Certificate RemoteCertificate
		{
			get
			{
				return this.remoteCertificate;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000C990 File Offset: 0x0000AB90
		public override TlsProtocols NegotiatedProtocol
		{
			get
			{
				return this.connectionInfo.ProtocolVersion;
			}
		}

		// Token: 0x060003D0 RID: 976
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLGetProtocolVersionMax(IntPtr context, out SslProtocol maxVersion);

		// Token: 0x060003D1 RID: 977
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLSetProtocolVersionMax(IntPtr context, SslProtocol maxVersion);

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x0000C9C8 File Offset: 0x0000ABC8
		public SslProtocol MaxProtocol
		{
			get
			{
				SslProtocol sslProtocol;
				SslStatus sslStatus = AppleTlsContext.SSLGetProtocolVersionMax(this.Handle, out sslProtocol);
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
				return sslProtocol;
			}
			set
			{
				SslStatus sslStatus = AppleTlsContext.SSLSetProtocolVersionMax(this.Handle, value);
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
			}
		}

		// Token: 0x060003D4 RID: 980
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLGetProtocolVersionMin(IntPtr context, out SslProtocol minVersion);

		// Token: 0x060003D5 RID: 981
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLSetProtocolVersionMin(IntPtr context, SslProtocol minVersion);

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x0000CA18 File Offset: 0x0000AC18
		public SslProtocol MinProtocol
		{
			get
			{
				SslProtocol sslProtocol;
				SslStatus sslStatus = AppleTlsContext.SSLGetProtocolVersionMin(this.Handle, out sslProtocol);
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
				return sslProtocol;
			}
			set
			{
				SslStatus sslStatus = AppleTlsContext.SSLSetProtocolVersionMin(this.Handle, value);
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
			}
		}

		// Token: 0x060003D8 RID: 984
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLGetNegotiatedProtocolVersion(IntPtr context, out SslProtocol protocol);

		// Token: 0x060003D9 RID: 985 RVA: 0x0000CA40 File Offset: 0x0000AC40
		public SslProtocol GetNegotiatedProtocolVersion()
		{
			SslProtocol sslProtocol;
			SslStatus sslStatus = AppleTlsContext.SSLGetNegotiatedProtocolVersion(this.Handle, out sslProtocol);
			this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
			return sslProtocol;
		}

		// Token: 0x060003DA RID: 986
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLGetSessionOption(IntPtr context, SslSessionOption option, out bool value);

		// Token: 0x060003DB RID: 987 RVA: 0x0000CA68 File Offset: 0x0000AC68
		public bool GetSessionOption(SslSessionOption option)
		{
			bool flag;
			SslStatus sslStatus = AppleTlsContext.SSLGetSessionOption(this.Handle, option, out flag);
			this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
			return flag;
		}

		// Token: 0x060003DC RID: 988
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLSetSessionOption(IntPtr context, SslSessionOption option, bool value);

		// Token: 0x060003DD RID: 989 RVA: 0x0000CA94 File Offset: 0x0000AC94
		public void SetSessionOption(SslSessionOption option, bool value)
		{
			SslStatus sslStatus = AppleTlsContext.SSLSetSessionOption(this.Handle, option, value);
			this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
		}

		// Token: 0x060003DE RID: 990
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLSetClientSideAuthenticate(IntPtr context, SslAuthenticate auth);

		// Token: 0x060003DF RID: 991 RVA: 0x0000CABC File Offset: 0x0000ACBC
		public void SetClientSideAuthenticate(SslAuthenticate auth)
		{
			SslStatus sslStatus = AppleTlsContext.SSLSetClientSideAuthenticate(this.Handle, auth);
			this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
		}

		// Token: 0x060003E0 RID: 992
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLHandshake(IntPtr context);

		// Token: 0x060003E1 RID: 993
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLGetSessionState(IntPtr context, ref SslSessionState state);

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000CAE4 File Offset: 0x0000ACE4
		public SslSessionState SessionState
		{
			get
			{
				SslSessionState sslSessionState = SslSessionState.Invalid;
				SslStatus sslStatus = AppleTlsContext.SSLGetSessionState(this.Handle, ref sslSessionState);
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
				return sslSessionState;
			}
		}

		// Token: 0x060003E3 RID: 995
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLGetPeerID(IntPtr context, out IntPtr peerID, out IntPtr peerIDLen);

		// Token: 0x060003E4 RID: 996
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private unsafe static extern SslStatus SSLSetPeerID(IntPtr context, byte* peerID, IntPtr peerIDLen);

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000CB10 File Offset: 0x0000AD10
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000CB64 File Offset: 0x0000AD64
		public unsafe byte[] PeerId
		{
			get
			{
				IntPtr intPtr;
				IntPtr intPtr2;
				SslStatus sslStatus = AppleTlsContext.SSLGetPeerID(this.Handle, out intPtr, out intPtr2);
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
				if (sslStatus != SslStatus.Success || (int)intPtr2 == 0)
				{
					return null;
				}
				byte[] array = new byte[(int)intPtr2];
				Marshal.Copy(intPtr, array, 0, (int)intPtr2);
				return array;
			}
			set
			{
				IntPtr intPtr = ((value == null) ? IntPtr.Zero : ((IntPtr)value.Length));
				SslStatus sslStatus;
				fixed (byte[] array = value)
				{
					byte* ptr;
					if (value == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					sslStatus = AppleTlsContext.SSLSetPeerID(this.Handle, ptr, intPtr);
				}
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
			}
		}

		// Token: 0x060003E7 RID: 999
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLGetBufferedReadSize(IntPtr context, out IntPtr bufSize);

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000CBB8 File Offset: 0x0000ADB8
		public IntPtr BufferedReadSize
		{
			get
			{
				IntPtr intPtr;
				SslStatus sslStatus = AppleTlsContext.SSLGetBufferedReadSize(this.Handle, out intPtr);
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
				return intPtr;
			}
		}

		// Token: 0x060003E9 RID: 1001
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLGetNumberSupportedCiphers(IntPtr context, out IntPtr numCiphers);

		// Token: 0x060003EA RID: 1002
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private unsafe static extern SslStatus SSLGetSupportedCiphers(IntPtr context, SslCipherSuite* ciphers, ref IntPtr numCiphers);

		// Token: 0x060003EB RID: 1003 RVA: 0x0000CBE0 File Offset: 0x0000ADE0
		public unsafe IList<SslCipherSuite> GetSupportedCiphers()
		{
			IntPtr intPtr;
			SslStatus sslStatus = AppleTlsContext.SSLGetNumberSupportedCiphers(this.Handle, out intPtr);
			this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
			if (sslStatus != SslStatus.Success || (int)intPtr <= 0)
			{
				return null;
			}
			SslCipherSuite[] array2;
			SslCipherSuite[] array = (array2 = new SslCipherSuite[(int)intPtr]);
			SslCipherSuite* ptr;
			if (array == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			sslStatus = AppleTlsContext.SSLGetSupportedCiphers(this.Handle, ptr, ref intPtr);
			array2 = null;
			this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
			return array;
		}

		// Token: 0x060003EC RID: 1004
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLGetNumberEnabledCiphers(IntPtr context, out IntPtr numCiphers);

		// Token: 0x060003ED RID: 1005
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private unsafe static extern SslStatus SSLGetEnabledCiphers(IntPtr context, SslCipherSuite* ciphers, ref IntPtr numCiphers);

		// Token: 0x060003EE RID: 1006 RVA: 0x0000CC58 File Offset: 0x0000AE58
		public unsafe IList<SslCipherSuite> GetEnabledCiphers()
		{
			IntPtr intPtr;
			SslStatus sslStatus = AppleTlsContext.SSLGetNumberEnabledCiphers(this.Handle, out intPtr);
			this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
			if (sslStatus != SslStatus.Success || (int)intPtr <= 0)
			{
				return null;
			}
			SslCipherSuite[] array2;
			SslCipherSuite[] array = (array2 = new SslCipherSuite[(int)intPtr]);
			SslCipherSuite* ptr;
			if (array == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			sslStatus = AppleTlsContext.SSLGetEnabledCiphers(this.Handle, ptr, ref intPtr);
			array2 = null;
			this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
			return array;
		}

		// Token: 0x060003EF RID: 1007
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private unsafe static extern SslStatus SSLSetEnabledCiphers(IntPtr context, SslCipherSuite* ciphers, IntPtr numCiphers);

		// Token: 0x060003F0 RID: 1008 RVA: 0x0000CCD0 File Offset: 0x0000AED0
		public unsafe void SetEnabledCiphers(SslCipherSuite[] ciphers)
		{
			if (ciphers == null)
			{
				throw new ArgumentNullException("ciphers");
			}
			SslStatus sslStatus;
			fixed (SslCipherSuite[] array = ciphers)
			{
				SslCipherSuite* ptr;
				if (ciphers == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				sslStatus = AppleTlsContext.SSLSetEnabledCiphers(this.Handle, ptr, (IntPtr)ciphers.Length);
			}
			this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
		}

		// Token: 0x060003F1 RID: 1009
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLGetNegotiatedCipher(IntPtr context, out SslCipherSuite cipherSuite);

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000CD28 File Offset: 0x0000AF28
		public SslCipherSuite NegotiatedCipher
		{
			get
			{
				SslCipherSuite sslCipherSuite;
				SslStatus sslStatus = AppleTlsContext.SSLGetNegotiatedCipher(this.Handle, out sslCipherSuite);
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
				return sslCipherSuite;
			}
		}

		// Token: 0x060003F3 RID: 1011
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLGetPeerDomainNameLength(IntPtr context, out IntPtr peerNameLen);

		// Token: 0x060003F4 RID: 1012
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLGetPeerDomainName(IntPtr context, byte[] peerName, ref IntPtr peerNameLen);

		// Token: 0x060003F5 RID: 1013
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLSetPeerDomainName(IntPtr context, byte[] peerName, IntPtr peerNameLen);

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000CD50 File Offset: 0x0000AF50
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000CDDC File Offset: 0x0000AFDC
		public string PeerDomainName
		{
			get
			{
				IntPtr intPtr;
				SslStatus sslStatus = AppleTlsContext.SSLGetPeerDomainNameLength(this.Handle, out intPtr);
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
				if (sslStatus != SslStatus.Success || (int)intPtr == 0)
				{
					return string.Empty;
				}
				byte[] array = new byte[(int)intPtr];
				sslStatus = AppleTlsContext.SSLGetPeerDomainName(this.Handle, array, ref intPtr);
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
				int num = (int)intPtr;
				if (sslStatus != SslStatus.Success)
				{
					return string.Empty;
				}
				if (num > 0 && array[num - 1] == 0)
				{
					num--;
				}
				return Encoding.UTF8.GetString(array, 0, num);
			}
			set
			{
				SslStatus sslStatus;
				if (value == null)
				{
					sslStatus = AppleTlsContext.SSLSetPeerDomainName(this.Handle, null, (IntPtr)0);
				}
				else
				{
					byte[] bytes = Encoding.UTF8.GetBytes(value);
					sslStatus = AppleTlsContext.SSLSetPeerDomainName(this.Handle, bytes, (IntPtr)bytes.Length);
				}
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
			}
		}

		// Token: 0x060003F8 RID: 1016
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLSetCertificate(IntPtr context, IntPtr certRefs);

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000CE30 File Offset: 0x0000B030
		private CFArray Bundle(SecIdentity identity, IEnumerable<SecCertificate> certificates)
		{
			if (identity == null)
			{
				throw new ArgumentNullException("identity");
			}
			int num = 0;
			int num2 = 0;
			if (certificates != null)
			{
				foreach (SecCertificate secCertificate in certificates)
				{
					num2++;
				}
			}
			IntPtr[] array = new IntPtr[num2 + 1];
			array[0] = identity.Handle;
			foreach (SecCertificate secCertificate2 in certificates)
			{
				array[++num] = secCertificate2.Handle;
			}
			return CFArray.CreateArray(array);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000CEE4 File Offset: 0x0000B0E4
		public void SetCertificate(SecIdentity identify, IEnumerable<SecCertificate> certificates)
		{
			using (CFArray cfarray = this.Bundle(identify, certificates))
			{
				SslStatus sslStatus = AppleTlsContext.SSLSetCertificate(this.Handle, cfarray.Handle);
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
			}
		}

		// Token: 0x060003FB RID: 1019
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLGetClientCertificateState(IntPtr context, out SslClientCertificateState clientState);

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000CF34 File Offset: 0x0000B134
		public SslClientCertificateState ClientCertificateState
		{
			get
			{
				SslClientCertificateState sslClientCertificateState;
				SslStatus sslStatus = AppleTlsContext.SSLGetClientCertificateState(this.Handle, out sslClientCertificateState);
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
				return sslClientCertificateState;
			}
		}

		// Token: 0x060003FD RID: 1021
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLCopyPeerTrust(IntPtr context, out IntPtr trust);

		// Token: 0x060003FE RID: 1022 RVA: 0x0000CF5C File Offset: 0x0000B15C
		public SecTrust GetPeerTrust(bool requireTrust)
		{
			IntPtr intPtr;
			SslStatus sslStatus = AppleTlsContext.SSLCopyPeerTrust(this.Handle, out intPtr);
			if (requireTrust)
			{
				this.CheckStatusAndThrow(sslStatus, Array.Empty<SslStatus>());
				if (intPtr == IntPtr.Zero)
				{
					throw new TlsException(AlertDescription.CertificateUnknown);
				}
			}
			if (!(intPtr == IntPtr.Zero))
			{
				return new SecTrust(intPtr, true);
			}
			return null;
		}

		// Token: 0x060003FF RID: 1023
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern IntPtr SSLCreateContext(IntPtr alloc, SslProtocolSide protocolSide, SslConnectionType connectionType);

		// Token: 0x06000400 RID: 1024
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLSetConnection(IntPtr context, IntPtr connection);

		// Token: 0x06000401 RID: 1025
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLSetIOFuncs(IntPtr context, SslReadFunc readFunc, SslWriteFunc writeFunc);

		// Token: 0x06000402 RID: 1026 RVA: 0x0000CFB4 File Offset: 0x0000B1B4
		[MonoPInvokeCallback(typeof(SslReadFunc))]
		private static SslStatus NativeReadCallback(IntPtr ptr, IntPtr data, ref IntPtr dataLength)
		{
			AppleTlsContext appleTlsContext = null;
			SslStatus sslStatus;
			try
			{
				GCHandle gchandle = GCHandle.FromIntPtr(ptr);
				if (!gchandle.IsAllocated)
				{
					sslStatus = SslStatus.Internal;
				}
				else
				{
					appleTlsContext = (AppleTlsContext)gchandle.Target;
					if (appleTlsContext == null || appleTlsContext.disposed)
					{
						sslStatus = SslStatus.ClosedAbort;
					}
					else
					{
						sslStatus = appleTlsContext.NativeReadCallback(data, ref dataLength);
					}
				}
			}
			catch (Exception ex)
			{
				if (appleTlsContext != null && appleTlsContext.lastException == null)
				{
					appleTlsContext.lastException = ex;
				}
				sslStatus = SslStatus.Internal;
			}
			return sslStatus;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000D034 File Offset: 0x0000B234
		[MonoPInvokeCallback(typeof(SslWriteFunc))]
		private static SslStatus NativeWriteCallback(IntPtr ptr, IntPtr data, ref IntPtr dataLength)
		{
			AppleTlsContext appleTlsContext = null;
			SslStatus sslStatus;
			try
			{
				GCHandle gchandle = GCHandle.FromIntPtr(ptr);
				if (!gchandle.IsAllocated)
				{
					sslStatus = SslStatus.Internal;
				}
				else
				{
					appleTlsContext = (AppleTlsContext)gchandle.Target;
					if (appleTlsContext == null || appleTlsContext.disposed)
					{
						sslStatus = SslStatus.ClosedAbort;
					}
					else
					{
						sslStatus = appleTlsContext.NativeWriteCallback(data, ref dataLength);
					}
				}
			}
			catch (Exception ex)
			{
				if (appleTlsContext != null && appleTlsContext.lastException == null)
				{
					appleTlsContext.lastException = ex;
				}
				sslStatus = SslStatus.Internal;
			}
			return sslStatus;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000D0B4 File Offset: 0x0000B2B4
		private SslStatus NativeReadCallback(IntPtr data, ref IntPtr dataLength)
		{
			if (this.closed || this.disposed || base.Parent == null)
			{
				return SslStatus.ClosedAbort;
			}
			int num = (int)dataLength;
			byte[] array = new byte[num];
			bool flag;
			int num2 = base.Parent.InternalRead(array, 0, num, out flag);
			dataLength = (IntPtr)num2;
			if (num2 < 0)
			{
				return SslStatus.ClosedAbort;
			}
			Marshal.Copy(array, 0, data, num2);
			if (num2 > 0)
			{
				return SslStatus.Success;
			}
			if (flag)
			{
				return SslStatus.WouldBlock;
			}
			if (num2 == 0)
			{
				this.closedGraceful = true;
				return SslStatus.ClosedGraceful;
			}
			return SslStatus.Success;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000D13C File Offset: 0x0000B33C
		private SslStatus NativeWriteCallback(IntPtr data, ref IntPtr dataLength)
		{
			if (this.closed || this.disposed || base.Parent == null)
			{
				return SslStatus.ClosedAbort;
			}
			int num = (int)dataLength;
			byte[] array = new byte[num];
			Marshal.Copy(data, array, 0, num);
			if (!base.Parent.InternalWrite(array, 0, num))
			{
				return SslStatus.ClosedAbort;
			}
			return SslStatus.Success;
		}

		// Token: 0x06000406 RID: 1030
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private unsafe static extern SslStatus SSLRead(IntPtr context, byte* data, IntPtr dataLength, out IntPtr processed);

		// Token: 0x06000407 RID: 1031 RVA: 0x0000D198 File Offset: 0x0000B398
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		public unsafe override ValueTuple<int, bool> Read(byte[] buffer, int offset, int count)
		{
			if (Interlocked.Exchange(ref this.pendingIO, 1) == 1)
			{
				throw new InvalidOperationException();
			}
			this.lastException = null;
			ValueTuple<int, bool> valueTuple;
			try
			{
				IntPtr intPtr;
				SslStatus sslStatus;
				try
				{
					fixed (byte* ptr = &buffer[offset])
					{
						byte* ptr2 = ptr;
						sslStatus = AppleTlsContext.SSLRead(this.Handle, ptr2, (IntPtr)count, out intPtr);
					}
				}
				finally
				{
					byte* ptr = null;
				}
				if (this.closedGraceful && (sslStatus == SslStatus.ClosedAbort || sslStatus == SslStatus.ClosedGraceful))
				{
					valueTuple = new ValueTuple<int, bool>(0, false);
				}
				else
				{
					this.CheckStatusAndThrow(sslStatus, new SslStatus[]
					{
						SslStatus.WouldBlock,
						SslStatus.ClosedGraceful
					});
					bool flag = sslStatus == SslStatus.WouldBlock;
					valueTuple = new ValueTuple<int, bool>((int)intPtr, flag);
				}
			}
			catch (Exception)
			{
				throw;
			}
			finally
			{
				this.pendingIO = 0;
			}
			return valueTuple;
		}

		// Token: 0x06000408 RID: 1032
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private unsafe static extern SslStatus SSLWrite(IntPtr context, byte* data, IntPtr dataLength, out IntPtr processed);

		// Token: 0x06000409 RID: 1033 RVA: 0x0000D278 File Offset: 0x0000B478
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		public unsafe override ValueTuple<int, bool> Write(byte[] buffer, int offset, int count)
		{
			if (Interlocked.Exchange(ref this.pendingIO, 1) == 1)
			{
				throw new InvalidOperationException();
			}
			this.lastException = null;
			ValueTuple<int, bool> valueTuple;
			try
			{
				SslStatus sslStatus = SslStatus.ClosedAbort;
				IntPtr intPtr = (IntPtr)(-1);
				try
				{
					fixed (byte* ptr = &buffer[offset])
					{
						byte* ptr2 = ptr;
						sslStatus = AppleTlsContext.SSLWrite(this.Handle, ptr2, (IntPtr)count, out intPtr);
					}
				}
				finally
				{
					byte* ptr = null;
				}
				this.CheckStatusAndThrow(sslStatus, new SslStatus[] { SslStatus.WouldBlock });
				bool flag = sslStatus == SslStatus.WouldBlock;
				valueTuple = new ValueTuple<int, bool>((int)intPtr, flag);
			}
			finally
			{
				this.pendingIO = 0;
			}
			return valueTuple;
		}

		// Token: 0x0600040A RID: 1034
		[DllImport("/System/Library/Frameworks/Security.framework/Security")]
		private static extern SslStatus SSLClose(IntPtr context);

		// Token: 0x0600040B RID: 1035 RVA: 0x0000D32C File Offset: 0x0000B52C
		public override void Shutdown()
		{
			this.closed = true;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000D338 File Offset: 0x0000B538
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (!this.disposed)
				{
					if (disposing)
					{
						this.disposed = true;
						if (this.serverIdentity != null)
						{
							this.serverIdentity.Dispose();
							this.serverIdentity = null;
						}
						if (this.clientIdentity != null)
						{
							this.clientIdentity.Dispose();
							this.clientIdentity = null;
						}
						if (this.remoteCertificate != null)
						{
							this.remoteCertificate.Dispose();
							this.remoteCertificate = null;
						}
					}
				}
			}
			finally
			{
				this.disposed = true;
				if (this.context != IntPtr.Zero)
				{
					CFObject.CFRelease(this.context);
					this.context = IntPtr.Zero;
				}
				base.Dispose(disposing);
			}
		}

		// Token: 0x04000918 RID: 2328
		public const string SecurityLibrary = "/System/Library/Frameworks/Security.framework/Security";

		// Token: 0x04000919 RID: 2329
		private GCHandle handle;

		// Token: 0x0400091A RID: 2330
		private IntPtr context;

		// Token: 0x0400091B RID: 2331
		private SslReadFunc readFunc;

		// Token: 0x0400091C RID: 2332
		private SslWriteFunc writeFunc;

		// Token: 0x0400091D RID: 2333
		private SecIdentity serverIdentity;

		// Token: 0x0400091E RID: 2334
		private SecIdentity clientIdentity;

		// Token: 0x0400091F RID: 2335
		private X509Certificate remoteCertificate;

		// Token: 0x04000920 RID: 2336
		private X509Certificate localClientCertificate;

		// Token: 0x04000921 RID: 2337
		private MonoTlsConnectionInfo connectionInfo;

		// Token: 0x04000922 RID: 2338
		private bool havePeerTrust;

		// Token: 0x04000923 RID: 2339
		private bool isAuthenticated;

		// Token: 0x04000924 RID: 2340
		private bool handshakeFinished;

		// Token: 0x04000925 RID: 2341
		private int handshakeStarted;

		// Token: 0x04000926 RID: 2342
		private bool closed;

		// Token: 0x04000927 RID: 2343
		private bool disposed;

		// Token: 0x04000928 RID: 2344
		private bool closedGraceful;

		// Token: 0x04000929 RID: 2345
		private int pendingIO;

		// Token: 0x0400092A RID: 2346
		private Exception lastException;
	}
}
