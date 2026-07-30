using System;
using System.Security.Cryptography;
using Mono.Security.Protocol.Tls.Handshake;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x02000037 RID: 55
	internal abstract class Context
	{
		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000E824 File Offset: 0x0000CA24
		// (set) Token: 0x0600023E RID: 574 RVA: 0x0000E82C File Offset: 0x0000CA2C
		public bool AbbreviatedHandshake
		{
			get
			{
				return this.abbreviatedHandshake;
			}
			set
			{
				this.abbreviatedHandshake = value;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000E835 File Offset: 0x0000CA35
		// (set) Token: 0x06000240 RID: 576 RVA: 0x0000E83D File Offset: 0x0000CA3D
		public bool ProtocolNegotiated
		{
			get
			{
				return this.protocolNegotiated;
			}
			set
			{
				this.protocolNegotiated = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000E846 File Offset: 0x0000CA46
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000E84E File Offset: 0x0000CA4E
		public bool ChangeCipherSpecDone { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000E858 File Offset: 0x0000CA58
		// (set) Token: 0x06000244 RID: 580 RVA: 0x0000E8AB File Offset: 0x0000CAAB
		public SecurityProtocolType SecurityProtocol
		{
			get
			{
				if ((this.securityProtocol & SecurityProtocolType.Tls) == SecurityProtocolType.Tls || (this.securityProtocol & SecurityProtocolType.Default) == SecurityProtocolType.Default)
				{
					return SecurityProtocolType.Tls;
				}
				if ((this.securityProtocol & SecurityProtocolType.Ssl3) == SecurityProtocolType.Ssl3)
				{
					return SecurityProtocolType.Ssl3;
				}
				throw new NotSupportedException("Unsupported security protocol type");
			}
			set
			{
				this.securityProtocol = value;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000E8B4 File Offset: 0x0000CAB4
		public SecurityProtocolType SecurityProtocolFlags
		{
			get
			{
				return this.securityProtocol;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000E8BC File Offset: 0x0000CABC
		public short Protocol
		{
			get
			{
				SecurityProtocolType securityProtocolType = this.SecurityProtocol;
				if (securityProtocolType <= SecurityProtocolType.Ssl2)
				{
					if (securityProtocolType != SecurityProtocolType.Default)
					{
						if (securityProtocolType != SecurityProtocolType.Ssl2)
						{
							goto IL_0034;
						}
						goto IL_0034;
					}
				}
				else
				{
					if (securityProtocolType == SecurityProtocolType.Ssl3)
					{
						return 768;
					}
					if (securityProtocolType != SecurityProtocolType.Tls)
					{
						goto IL_0034;
					}
				}
				return 769;
				IL_0034:
				throw new NotSupportedException("Unsupported security protocol type");
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000247 RID: 583 RVA: 0x0000E907 File Offset: 0x0000CB07
		// (set) Token: 0x06000248 RID: 584 RVA: 0x0000E90F File Offset: 0x0000CB0F
		public byte[] SessionId
		{
			get
			{
				return this.sessionId;
			}
			set
			{
				this.sessionId = value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000E918 File Offset: 0x0000CB18
		// (set) Token: 0x0600024A RID: 586 RVA: 0x0000E920 File Offset: 0x0000CB20
		public SecurityCompressionType CompressionMethod
		{
			get
			{
				return this.compressionMethod;
			}
			set
			{
				this.compressionMethod = value;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000E929 File Offset: 0x0000CB29
		public TlsServerSettings ServerSettings
		{
			get
			{
				return this.serverSettings;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000E931 File Offset: 0x0000CB31
		public TlsClientSettings ClientSettings
		{
			get
			{
				return this.clientSettings;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000E939 File Offset: 0x0000CB39
		// (set) Token: 0x0600024E RID: 590 RVA: 0x0000E941 File Offset: 0x0000CB41
		public HandshakeType LastHandshakeMsg
		{
			get
			{
				return this.lastHandshakeMsg;
			}
			set
			{
				this.lastHandshakeMsg = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000E94A File Offset: 0x0000CB4A
		// (set) Token: 0x06000250 RID: 592 RVA: 0x0000E952 File Offset: 0x0000CB52
		public HandshakeState HandshakeState
		{
			get
			{
				return this.handshakeState;
			}
			set
			{
				this.handshakeState = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000E95B File Offset: 0x0000CB5B
		// (set) Token: 0x06000252 RID: 594 RVA: 0x0000E963 File Offset: 0x0000CB63
		public bool ReceivedConnectionEnd
		{
			get
			{
				return this.receivedConnectionEnd;
			}
			set
			{
				this.receivedConnectionEnd = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000E96C File Offset: 0x0000CB6C
		// (set) Token: 0x06000254 RID: 596 RVA: 0x0000E974 File Offset: 0x0000CB74
		public bool SentConnectionEnd
		{
			get
			{
				return this.sentConnectionEnd;
			}
			set
			{
				this.sentConnectionEnd = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000E97D File Offset: 0x0000CB7D
		// (set) Token: 0x06000256 RID: 598 RVA: 0x0000E985 File Offset: 0x0000CB85
		public CipherSuiteCollection SupportedCiphers
		{
			get
			{
				return this.supportedCiphers;
			}
			set
			{
				this.supportedCiphers = value;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000E98E File Offset: 0x0000CB8E
		public TlsStream HandshakeMessages
		{
			get
			{
				return this.handshakeMessages;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000E996 File Offset: 0x0000CB96
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000E99E File Offset: 0x0000CB9E
		public ulong WriteSequenceNumber
		{
			get
			{
				return this.writeSequenceNumber;
			}
			set
			{
				this.writeSequenceNumber = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000E9A7 File Offset: 0x0000CBA7
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000E9AF File Offset: 0x0000CBAF
		public ulong ReadSequenceNumber
		{
			get
			{
				return this.readSequenceNumber;
			}
			set
			{
				this.readSequenceNumber = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
		// (set) Token: 0x0600025D RID: 605 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		public byte[] ClientRandom
		{
			get
			{
				return this.clientRandom;
			}
			set
			{
				this.clientRandom = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000E9C9 File Offset: 0x0000CBC9
		// (set) Token: 0x0600025F RID: 607 RVA: 0x0000E9D1 File Offset: 0x0000CBD1
		public byte[] ServerRandom
		{
			get
			{
				return this.serverRandom;
			}
			set
			{
				this.serverRandom = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000E9DA File Offset: 0x0000CBDA
		// (set) Token: 0x06000261 RID: 609 RVA: 0x0000E9E2 File Offset: 0x0000CBE2
		public byte[] RandomCS
		{
			get
			{
				return this.randomCS;
			}
			set
			{
				this.randomCS = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000E9EB File Offset: 0x0000CBEB
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000E9F3 File Offset: 0x0000CBF3
		public byte[] RandomSC
		{
			get
			{
				return this.randomSC;
			}
			set
			{
				this.randomSC = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		// (set) Token: 0x06000265 RID: 613 RVA: 0x0000EA04 File Offset: 0x0000CC04
		public byte[] MasterSecret
		{
			get
			{
				return this.masterSecret;
			}
			set
			{
				this.masterSecret = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000EA0D File Offset: 0x0000CC0D
		// (set) Token: 0x06000267 RID: 615 RVA: 0x0000EA15 File Offset: 0x0000CC15
		public byte[] ClientWriteKey
		{
			get
			{
				return this.clientWriteKey;
			}
			set
			{
				this.clientWriteKey = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000EA1E File Offset: 0x0000CC1E
		// (set) Token: 0x06000269 RID: 617 RVA: 0x0000EA26 File Offset: 0x0000CC26
		public byte[] ServerWriteKey
		{
			get
			{
				return this.serverWriteKey;
			}
			set
			{
				this.serverWriteKey = value;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000EA2F File Offset: 0x0000CC2F
		// (set) Token: 0x0600026B RID: 619 RVA: 0x0000EA37 File Offset: 0x0000CC37
		public byte[] ClientWriteIV
		{
			get
			{
				return this.clientWriteIV;
			}
			set
			{
				this.clientWriteIV = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000EA40 File Offset: 0x0000CC40
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000EA48 File Offset: 0x0000CC48
		public byte[] ServerWriteIV
		{
			get
			{
				return this.serverWriteIV;
			}
			set
			{
				this.serverWriteIV = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000EA51 File Offset: 0x0000CC51
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000EA59 File Offset: 0x0000CC59
		public RecordProtocol RecordProtocol
		{
			get
			{
				return this.recordProtocol;
			}
			set
			{
				this.recordProtocol = value;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000EA64 File Offset: 0x0000CC64
		public Context(SecurityProtocolType securityProtocolType)
		{
			this.SecurityProtocol = securityProtocolType;
			this.compressionMethod = SecurityCompressionType.None;
			this.serverSettings = new TlsServerSettings();
			this.clientSettings = new TlsClientSettings();
			this.handshakeMessages = new TlsStream();
			this.sessionId = null;
			this.handshakeState = HandshakeState.None;
			this.random = RandomNumberGenerator.Create();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000EAC0 File Offset: 0x0000CCC0
		public int GetUnixTime()
		{
			return (int)((DateTime.UtcNow.Ticks - 621355968000000000L) / 10000000L);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		public byte[] GetSecureRandomBytes(int count)
		{
			byte[] array = new byte[count];
			this.random.GetNonZeroBytes(array);
			return array;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000EB0D File Offset: 0x0000CD0D
		public virtual void Clear()
		{
			this.compressionMethod = SecurityCompressionType.None;
			this.serverSettings = new TlsServerSettings();
			this.clientSettings = new TlsClientSettings();
			this.handshakeMessages = new TlsStream();
			this.sessionId = null;
			this.handshakeState = HandshakeState.None;
			this.ClearKeyInfo();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000EB4C File Offset: 0x0000CD4C
		public virtual void ClearKeyInfo()
		{
			if (this.masterSecret != null)
			{
				Array.Clear(this.masterSecret, 0, this.masterSecret.Length);
				this.masterSecret = null;
			}
			if (this.clientRandom != null)
			{
				Array.Clear(this.clientRandom, 0, this.clientRandom.Length);
				this.clientRandom = null;
			}
			if (this.serverRandom != null)
			{
				Array.Clear(this.serverRandom, 0, this.serverRandom.Length);
				this.serverRandom = null;
			}
			if (this.randomCS != null)
			{
				Array.Clear(this.randomCS, 0, this.randomCS.Length);
				this.randomCS = null;
			}
			if (this.randomSC != null)
			{
				Array.Clear(this.randomSC, 0, this.randomSC.Length);
				this.randomSC = null;
			}
			if (this.clientWriteKey != null)
			{
				Array.Clear(this.clientWriteKey, 0, this.clientWriteKey.Length);
				this.clientWriteKey = null;
			}
			if (this.clientWriteIV != null)
			{
				Array.Clear(this.clientWriteIV, 0, this.clientWriteIV.Length);
				this.clientWriteIV = null;
			}
			if (this.serverWriteKey != null)
			{
				Array.Clear(this.serverWriteKey, 0, this.serverWriteKey.Length);
				this.serverWriteKey = null;
			}
			if (this.serverWriteIV != null)
			{
				Array.Clear(this.serverWriteIV, 0, this.serverWriteIV.Length);
				this.serverWriteIV = null;
			}
			this.handshakeMessages.Reset();
			SecurityProtocolType securityProtocolType = this.securityProtocol;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000ECA9 File Offset: 0x0000CEA9
		public SecurityProtocolType DecodeProtocolCode(short code, bool allowFallback = false)
		{
			if (code == 768)
			{
				return SecurityProtocolType.Ssl3;
			}
			if (code == 769)
			{
				return SecurityProtocolType.Tls;
			}
			if (allowFallback && code > 769)
			{
				return SecurityProtocolType.Tls;
			}
			throw new NotSupportedException("Unsupported security protocol type");
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000ECE0 File Offset: 0x0000CEE0
		public void ChangeProtocol(short protocol)
		{
			SecurityProtocolType securityProtocolType = this.DecodeProtocolCode(protocol, false);
			if ((securityProtocolType & this.SecurityProtocolFlags) == securityProtocolType || (this.SecurityProtocolFlags & SecurityProtocolType.Default) == SecurityProtocolType.Default)
			{
				this.SecurityProtocol = securityProtocolType;
				this.SupportedCiphers = CipherSuiteFactory.GetSupportedCiphers(this is ServerContext, securityProtocolType);
				return;
			}
			throw new TlsException(AlertDescription.ProtocolVersion, "Incorrect protocol version received from server");
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000ED3D File Offset: 0x0000CF3D
		public SecurityParameters Current
		{
			get
			{
				if (this.current == null)
				{
					this.current = new SecurityParameters();
				}
				if (this.current.Cipher != null)
				{
					this.current.Cipher.Context = this;
				}
				return this.current;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000ED76 File Offset: 0x0000CF76
		public SecurityParameters Negotiating
		{
			get
			{
				if (this.negotiating == null)
				{
					this.negotiating = new SecurityParameters();
				}
				if (this.negotiating.Cipher != null)
				{
					this.negotiating.Cipher.Context = this;
				}
				return this.negotiating;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000EDAF File Offset: 0x0000CFAF
		public SecurityParameters Read
		{
			get
			{
				return this.read;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000EDB7 File Offset: 0x0000CFB7
		public SecurityParameters Write
		{
			get
			{
				return this.write;
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		public void StartSwitchingSecurityParameters(bool client)
		{
			if (client)
			{
				this.write = this.negotiating;
				this.read = this.current;
			}
			else
			{
				this.read = this.negotiating;
				this.write = this.current;
			}
			this.current = this.negotiating;
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000EE10 File Offset: 0x0000D010
		public void EndSwitchingSecurityParameters(bool client)
		{
			SecurityParameters securityParameters;
			if (client)
			{
				securityParameters = this.read;
				this.read = this.current;
			}
			else
			{
				securityParameters = this.write;
				this.write = this.current;
			}
			if (securityParameters != null)
			{
				securityParameters.Clear();
			}
			this.negotiating = securityParameters;
		}

		// Token: 0x04000143 RID: 323
		internal const short MAX_FRAGMENT_SIZE = 16384;

		// Token: 0x04000144 RID: 324
		internal const short TLS1_PROTOCOL_CODE = 769;

		// Token: 0x04000145 RID: 325
		internal const short SSL3_PROTOCOL_CODE = 768;

		// Token: 0x04000146 RID: 326
		internal const long UNIX_BASE_TICKS = 621355968000000000L;

		// Token: 0x04000147 RID: 327
		private SecurityProtocolType securityProtocol;

		// Token: 0x04000148 RID: 328
		private byte[] sessionId;

		// Token: 0x04000149 RID: 329
		private SecurityCompressionType compressionMethod;

		// Token: 0x0400014A RID: 330
		private TlsServerSettings serverSettings;

		// Token: 0x0400014B RID: 331
		private TlsClientSettings clientSettings;

		// Token: 0x0400014C RID: 332
		private SecurityParameters current;

		// Token: 0x0400014D RID: 333
		private SecurityParameters negotiating;

		// Token: 0x0400014E RID: 334
		private SecurityParameters read;

		// Token: 0x0400014F RID: 335
		private SecurityParameters write;

		// Token: 0x04000150 RID: 336
		private CipherSuiteCollection supportedCiphers;

		// Token: 0x04000151 RID: 337
		private HandshakeType lastHandshakeMsg;

		// Token: 0x04000152 RID: 338
		private HandshakeState handshakeState;

		// Token: 0x04000153 RID: 339
		private bool abbreviatedHandshake;

		// Token: 0x04000154 RID: 340
		private bool receivedConnectionEnd;

		// Token: 0x04000155 RID: 341
		private bool sentConnectionEnd;

		// Token: 0x04000156 RID: 342
		private bool protocolNegotiated;

		// Token: 0x04000157 RID: 343
		private ulong writeSequenceNumber;

		// Token: 0x04000158 RID: 344
		private ulong readSequenceNumber;

		// Token: 0x04000159 RID: 345
		private byte[] clientRandom;

		// Token: 0x0400015A RID: 346
		private byte[] serverRandom;

		// Token: 0x0400015B RID: 347
		private byte[] randomCS;

		// Token: 0x0400015C RID: 348
		private byte[] randomSC;

		// Token: 0x0400015D RID: 349
		private byte[] masterSecret;

		// Token: 0x0400015E RID: 350
		private byte[] clientWriteKey;

		// Token: 0x0400015F RID: 351
		private byte[] serverWriteKey;

		// Token: 0x04000160 RID: 352
		private byte[] clientWriteIV;

		// Token: 0x04000161 RID: 353
		private byte[] serverWriteIV;

		// Token: 0x04000162 RID: 354
		private TlsStream handshakeMessages;

		// Token: 0x04000163 RID: 355
		private RandomNumberGenerator random;

		// Token: 0x04000164 RID: 356
		private RecordProtocol recordProtocol;
	}
}
