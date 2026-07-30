using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Mono.Security.Interface
{
	// Token: 0x02000088 RID: 136
	public sealed class MonoTlsSettings
	{
		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00017197 File Offset: 0x00015397
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0001719F File Offset: 0x0001539F
		public MonoRemoteCertificateValidationCallback RemoteCertificateValidationCallback { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x000171A8 File Offset: 0x000153A8
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x000171B0 File Offset: 0x000153B0
		public MonoLocalCertificateSelectionCallback ClientCertificateSelectionCallback { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x000171B9 File Offset: 0x000153B9
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x000171C1 File Offset: 0x000153C1
		public bool CheckCertificateName
		{
			get
			{
				return this.checkCertName;
			}
			set
			{
				this.checkCertName = value;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x000171CA File Offset: 0x000153CA
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x000171D2 File Offset: 0x000153D2
		public bool CheckCertificateRevocationStatus
		{
			get
			{
				return this.checkCertRevocationStatus;
			}
			set
			{
				this.checkCertRevocationStatus = value;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x000171DB File Offset: 0x000153DB
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x000171E3 File Offset: 0x000153E3
		public bool? UseServicePointManagerCallback
		{
			get
			{
				return this.useServicePointManagerCallback;
			}
			set
			{
				this.useServicePointManagerCallback = value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x000171EC File Offset: 0x000153EC
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x000171F4 File Offset: 0x000153F4
		public bool SkipSystemValidators
		{
			get
			{
				return this.skipSystemValidators;
			}
			set
			{
				this.skipSystemValidators = value;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x000171FD File Offset: 0x000153FD
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x00017205 File Offset: 0x00015405
		public bool CallbackNeedsCertificateChain
		{
			get
			{
				return this.callbackNeedsChain;
			}
			set
			{
				this.callbackNeedsChain = value;
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0001720E File Offset: 0x0001540E
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x00017216 File Offset: 0x00015416
		public DateTime? CertificateValidationTime { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0001721F File Offset: 0x0001541F
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x00017227 File Offset: 0x00015427
		public X509CertificateCollection TrustAnchors { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x00017230 File Offset: 0x00015430
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x00017238 File Offset: 0x00015438
		public object UserSettings { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00017241 File Offset: 0x00015441
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00017249 File Offset: 0x00015449
		internal string[] CertificateSearchPaths { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00017252 File Offset: 0x00015452
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x0001725A File Offset: 0x0001545A
		internal bool SendCloseNotify { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00017263 File Offset: 0x00015463
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x0001726B File Offset: 0x0001546B
		public TlsProtocols? EnabledProtocols { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00017274 File Offset: 0x00015474
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x0001727C File Offset: 0x0001547C
		[CLSCompliant(false)]
		public CipherSuiteCode[] EnabledCiphers { get; set; }

		// Token: 0x060004F9 RID: 1273 RVA: 0x00017285 File Offset: 0x00015485
		public MonoTlsSettings()
		{
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x0001729B File Offset: 0x0001549B
		// (set) Token: 0x060004FB RID: 1275 RVA: 0x000172BA File Offset: 0x000154BA
		public static MonoTlsSettings DefaultSettings
		{
			get
			{
				if (MonoTlsSettings.defaultSettings == null)
				{
					Interlocked.CompareExchange<MonoTlsSettings>(ref MonoTlsSettings.defaultSettings, new MonoTlsSettings(), null);
				}
				return MonoTlsSettings.defaultSettings;
			}
			set
			{
				MonoTlsSettings.defaultSettings = value ?? new MonoTlsSettings();
			}
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x000172CB File Offset: 0x000154CB
		public static MonoTlsSettings CopyDefaultSettings()
		{
			return MonoTlsSettings.DefaultSettings.Clone();
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x000172D7 File Offset: 0x000154D7
		[Obsolete("Do not use outside System.dll!")]
		public ICertificateValidator CertificateValidator
		{
			get
			{
				return this.certificateValidator;
			}
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x000172DF File Offset: 0x000154DF
		[Obsolete("Do not use outside System.dll!")]
		public MonoTlsSettings CloneWithValidator(ICertificateValidator validator)
		{
			if (this.cloned)
			{
				this.certificateValidator = validator;
				return this;
			}
			return new MonoTlsSettings(this)
			{
				certificateValidator = validator
			};
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x000172FF File Offset: 0x000154FF
		public MonoTlsSettings Clone()
		{
			return new MonoTlsSettings(this);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00017308 File Offset: 0x00015508
		private MonoTlsSettings(MonoTlsSettings other)
		{
			this.RemoteCertificateValidationCallback = other.RemoteCertificateValidationCallback;
			this.ClientCertificateSelectionCallback = other.ClientCertificateSelectionCallback;
			this.checkCertName = other.checkCertName;
			this.checkCertRevocationStatus = other.checkCertRevocationStatus;
			this.UseServicePointManagerCallback = other.useServicePointManagerCallback;
			this.skipSystemValidators = other.skipSystemValidators;
			this.callbackNeedsChain = other.callbackNeedsChain;
			this.UserSettings = other.UserSettings;
			this.EnabledProtocols = other.EnabledProtocols;
			this.EnabledCiphers = other.EnabledCiphers;
			this.CertificateValidationTime = other.CertificateValidationTime;
			this.SendCloseNotify = other.SendCloseNotify;
			if (other.TrustAnchors != null)
			{
				this.TrustAnchors = new X509CertificateCollection(other.TrustAnchors);
			}
			if (other.CertificateSearchPaths != null)
			{
				this.CertificateSearchPaths = new string[other.CertificateSearchPaths.Length];
				other.CertificateSearchPaths.CopyTo(this.CertificateSearchPaths, 0);
			}
			this.cloned = true;
		}

		// Token: 0x0400037B RID: 891
		private bool cloned;

		// Token: 0x0400037C RID: 892
		private bool checkCertName = true;

		// Token: 0x0400037D RID: 893
		private bool checkCertRevocationStatus;

		// Token: 0x0400037E RID: 894
		private bool? useServicePointManagerCallback;

		// Token: 0x0400037F RID: 895
		private bool skipSystemValidators;

		// Token: 0x04000380 RID: 896
		private bool callbackNeedsChain = true;

		// Token: 0x04000381 RID: 897
		private ICertificateValidator certificateValidator;

		// Token: 0x04000382 RID: 898
		private static MonoTlsSettings defaultSettings;
	}
}
