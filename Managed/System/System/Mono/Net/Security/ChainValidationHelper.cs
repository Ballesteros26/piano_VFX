using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Mono.Net.Security.Private;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x02000070 RID: 112
	internal class ChainValidationHelper : ICertificateValidator2, ICertificateValidator
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00006231 File Offset: 0x00004431
		internal static ICertificateValidator GetInternalValidator(MonoTlsProvider provider, MonoTlsSettings settings)
		{
			if (settings == null)
			{
				return new ChainValidationHelper(provider, null, false, null, null);
			}
			if (settings.CertificateValidator != null)
			{
				return settings.CertificateValidator;
			}
			return new ChainValidationHelper(provider, settings, false, null, null);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000625C File Offset: 0x0000445C
		internal static ICertificateValidator GetDefaultValidator(MonoTlsSettings settings)
		{
			MonoTlsProvider monoTlsProvider = MonoTlsProviderFactory.GetProvider();
			if (settings == null)
			{
				return new ChainValidationHelper(monoTlsProvider, null, false, null, null);
			}
			if (settings.CertificateValidator != null)
			{
				throw new NotSupportedException();
			}
			return new ChainValidationHelper(monoTlsProvider, settings, false, null, null);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006298 File Offset: 0x00004498
		internal static ChainValidationHelper CloneWithCallbackWrapper(MonoTlsProvider provider, ref MonoTlsSettings settings, ServerCertValidationCallbackWrapper wrapper)
		{
			ChainValidationHelper chainValidationHelper = (ChainValidationHelper)settings.CertificateValidator;
			if (chainValidationHelper == null)
			{
				chainValidationHelper = new ChainValidationHelper(provider, settings, true, null, wrapper);
			}
			else
			{
				chainValidationHelper = new ChainValidationHelper(chainValidationHelper, provider, settings, wrapper);
			}
			settings = chainValidationHelper.settings;
			return chainValidationHelper;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000062D7 File Offset: 0x000044D7
		internal static bool InvokeCallback(ServerCertValidationCallback callback, object sender, X509Certificate certificate, X509Chain chain, MonoSslPolicyErrors sslPolicyErrors)
		{
			return callback.Invoke(sender, certificate, chain, (SslPolicyErrors)sslPolicyErrors);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000062E4 File Offset: 0x000044E4
		private ChainValidationHelper(ChainValidationHelper other, MonoTlsProvider provider, MonoTlsSettings settings, ServerCertValidationCallbackWrapper callbackWrapper = null)
		{
			this.sender = other.sender;
			this.certValidationCallback = other.certValidationCallback;
			this.certSelectionCallback = other.certSelectionCallback;
			this.tlsStream = other.tlsStream;
			this.request = other.request;
			if (settings == null)
			{
				settings = MonoTlsSettings.DefaultSettings;
			}
			this.provider = provider;
			this.settings = settings.CloneWithValidator(this);
			this.callbackWrapper = callbackWrapper;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000635C File Offset: 0x0000455C
		internal static ChainValidationHelper Create(MonoTlsProvider provider, ref MonoTlsSettings settings, MonoTlsStream stream)
		{
			ChainValidationHelper chainValidationHelper = new ChainValidationHelper(provider, settings, true, stream, null);
			settings = chainValidationHelper.settings;
			return chainValidationHelper;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006380 File Offset: 0x00004580
		private ChainValidationHelper(MonoTlsProvider provider, MonoTlsSettings settings, bool cloneSettings, MonoTlsStream stream, ServerCertValidationCallbackWrapper callbackWrapper)
		{
			if (settings == null)
			{
				settings = MonoTlsSettings.CopyDefaultSettings();
			}
			if (cloneSettings)
			{
				settings = settings.CloneWithValidator(this);
			}
			if (provider == null)
			{
				provider = MonoTlsProviderFactory.GetProvider();
			}
			this.provider = provider;
			this.settings = settings;
			this.tlsStream = stream;
			this.callbackWrapper = callbackWrapper;
			bool flag = false;
			if (settings != null)
			{
				if (settings.RemoteCertificateValidationCallback != null)
				{
					RemoteCertificateValidationCallback remoteCertificateValidationCallback = CallbackHelpers.MonoToPublic(settings.RemoteCertificateValidationCallback);
					this.certValidationCallback = new ServerCertValidationCallback(remoteCertificateValidationCallback);
				}
				this.certSelectionCallback = CallbackHelpers.MonoToInternal(settings.ClientCertificateSelectionCallback);
				flag = settings.UseServicePointManagerCallback ?? (stream != null);
			}
			if (stream != null)
			{
				this.request = stream.Request;
				this.sender = this.request;
				if (this.certValidationCallback == null)
				{
					this.certValidationCallback = this.request.ServerCertValidationCallback;
				}
				if (this.certSelectionCallback == null)
				{
					this.certSelectionCallback = new LocalCertSelectionCallback(ChainValidationHelper.DefaultSelectionCallback);
				}
				if (settings == null)
				{
					flag = true;
				}
			}
			if (flag && this.certValidationCallback == null)
			{
				this.certValidationCallback = ServicePointManager.ServerCertValidationCallback;
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006494 File Offset: 0x00004694
		private static X509Certificate DefaultSelectionCallback(string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
		{
			X509Certificate x509Certificate;
			if (localCertificates == null || localCertificates.Count == 0)
			{
				x509Certificate = null;
			}
			else
			{
				x509Certificate = localCertificates[0];
			}
			return x509Certificate;
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000064B9 File Offset: 0x000046B9
		public MonoTlsProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000064C1 File Offset: 0x000046C1
		public MonoTlsSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000209 RID: 521 RVA: 0x000064C9 File Offset: 0x000046C9
		public bool HasCertificateSelectionCallback
		{
			get
			{
				return this.certSelectionCallback != null;
			}
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000064D4 File Offset: 0x000046D4
		public bool SelectClientCertificate(string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers, out X509Certificate clientCertificate)
		{
			if (this.certSelectionCallback == null)
			{
				clientCertificate = null;
				return false;
			}
			clientCertificate = this.certSelectionCallback(targetHost, localCertificates, remoteCertificate, acceptableIssuers);
			return true;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000064F8 File Offset: 0x000046F8
		internal X509Certificate SelectClientCertificate(string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
		{
			if (this.certSelectionCallback == null)
			{
				return null;
			}
			return this.certSelectionCallback(targetHost, localCertificates, remoteCertificate, acceptableIssuers);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x00006514 File Offset: 0x00004714
		internal bool ValidateClientCertificate(X509Certificate certificate, MonoSslPolicyErrors errors)
		{
			X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
			x509CertificateCollection.Add(new X509Certificate2(certificate.GetRawCertData()));
			ValidationResult validationResult = this.ValidateChain(string.Empty, true, certificate, null, x509CertificateCollection, (SslPolicyErrors)errors);
			return validationResult != null && validationResult.Trusted && !validationResult.UserDenied;
		}

		// Token: 0x0600020D RID: 525 RVA: 0x00006564 File Offset: 0x00004764
		public ValidationResult ValidateCertificate(string host, bool serverMode, X509CertificateCollection certs)
		{
			ValidationResult validationResult2;
			try
			{
				X509Certificate x509Certificate;
				if (certs != null && certs.Count != 0)
				{
					x509Certificate = certs[0];
				}
				else
				{
					x509Certificate = null;
				}
				ValidationResult validationResult = this.ValidateChain(host, serverMode, x509Certificate, null, certs, SslPolicyErrors.None);
				if (this.tlsStream != null)
				{
					this.tlsStream.CertificateValidationFailed = validationResult == null || !validationResult.Trusted || validationResult.UserDenied;
				}
				validationResult2 = validationResult;
			}
			catch
			{
				if (this.tlsStream != null)
				{
					this.tlsStream.CertificateValidationFailed = true;
				}
				throw;
			}
			return validationResult2;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000065E8 File Offset: 0x000047E8
		public ValidationResult ValidateCertificate(string host, bool serverMode, X509Certificate leaf, X509Chain chain)
		{
			ValidationResult validationResult2;
			try
			{
				ValidationResult validationResult = this.ValidateChain(host, serverMode, leaf, chain, null, SslPolicyErrors.None);
				if (this.tlsStream != null)
				{
					this.tlsStream.CertificateValidationFailed = validationResult == null || !validationResult.Trusted || validationResult.UserDenied;
				}
				validationResult2 = validationResult;
			}
			catch
			{
				if (this.tlsStream != null)
				{
					this.tlsStream.CertificateValidationFailed = true;
				}
				throw;
			}
			return validationResult2;
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006658 File Offset: 0x00004858
		private ValidationResult ValidateChain(string host, bool server, X509Certificate leaf, X509Chain chain, X509CertificateCollection certs, SslPolicyErrors errors)
		{
			X509Chain x509Chain = chain;
			bool flag = chain == null;
			ValidationResult validationResult2;
			try
			{
				ValidationResult validationResult = this.ValidateChain(host, server, leaf, ref chain, certs, errors);
				if (chain != x509Chain)
				{
					flag = true;
				}
				validationResult2 = validationResult;
			}
			finally
			{
				if (flag && chain != null)
				{
					chain.Dispose();
				}
			}
			return validationResult2;
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000066A8 File Offset: 0x000048A8
		private ValidationResult ValidateChain(string host, bool server, X509Certificate leaf, ref X509Chain chain, X509CertificateCollection certs, SslPolicyErrors errors)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = this.certValidationCallback != null || this.callbackWrapper != null;
			if (this.tlsStream != null)
			{
				this.request.ServicePoint.UpdateServerCertificate(leaf);
			}
			if (leaf == null)
			{
				errors |= SslPolicyErrors.RemoteCertificateNotAvailable;
				if (flag3)
				{
					if (this.callbackWrapper != null)
					{
						flag2 = this.callbackWrapper(this.certValidationCallback, leaf, null, (MonoSslPolicyErrors)errors);
					}
					else
					{
						flag2 = this.certValidationCallback.Invoke(this.sender, leaf, null, errors);
					}
					flag = !flag2;
				}
				return new ValidationResult(flag2, flag, 0, new MonoSslPolicyErrors?((MonoSslPolicyErrors)errors));
			}
			if (!string.IsNullOrEmpty(host))
			{
				int num = host.IndexOf(':');
				if (num > 0)
				{
					host = host.Substring(0, num);
				}
			}
			ICertificatePolicy legacyCertificatePolicy = ServicePointManager.GetLegacyCertificatePolicy();
			int num2 = 0;
			bool flag4 = SystemCertificateValidator.NeedsChain(this.settings);
			if (!flag4 && flag3 && (this.settings == null || this.settings.CallbackNeedsCertificateChain))
			{
				flag4 = true;
			}
			MonoSslPolicyErrors monoSslPolicyErrors = (MonoSslPolicyErrors)errors;
			flag2 = this.provider.ValidateCertificate(this, host, server, certs, flag4, ref chain, ref monoSslPolicyErrors, ref num2);
			errors = (SslPolicyErrors)monoSslPolicyErrors;
			if (num2 == 0 && errors != SslPolicyErrors.None)
			{
				num2 = -2146762485;
			}
			if (legacyCertificatePolicy != null && (!(legacyCertificatePolicy is DefaultCertificatePolicy) || this.certValidationCallback == null))
			{
				ServicePoint servicePoint = null;
				if (this.request != null)
				{
					servicePoint = this.request.ServicePointNoLock;
				}
				flag2 = legacyCertificatePolicy.CheckValidationResult(servicePoint, leaf, this.request, num2);
				flag = !flag2 && !(legacyCertificatePolicy is DefaultCertificatePolicy);
			}
			if (flag3)
			{
				if (this.callbackWrapper != null)
				{
					flag2 = this.callbackWrapper(this.certValidationCallback, leaf, chain, (MonoSslPolicyErrors)errors);
				}
				else
				{
					flag2 = this.certValidationCallback.Invoke(this.sender, leaf, chain, errors);
				}
				flag = !flag2;
			}
			return new ValidationResult(flag2, flag, num2, new MonoSslPolicyErrors?((MonoSslPolicyErrors)errors));
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000686C File Offset: 0x00004A6C
		private bool InvokeSystemValidator(string targetHost, bool serverMode, X509CertificateCollection certificates, X509Chain chain, ref MonoSslPolicyErrors xerrors, ref int status11)
		{
			SslPolicyErrors sslPolicyErrors = (SslPolicyErrors)xerrors;
			bool flag = SystemCertificateValidator.Evaluate(this.settings, targetHost, certificates, chain, ref sslPolicyErrors, ref status11);
			xerrors = (MonoSslPolicyErrors)sslPolicyErrors;
			return flag;
		}

		// Token: 0x040007A9 RID: 1961
		private readonly object sender;

		// Token: 0x040007AA RID: 1962
		private readonly MonoTlsSettings settings;

		// Token: 0x040007AB RID: 1963
		private readonly MonoTlsProvider provider;

		// Token: 0x040007AC RID: 1964
		private readonly ServerCertValidationCallback certValidationCallback;

		// Token: 0x040007AD RID: 1965
		private readonly LocalCertSelectionCallback certSelectionCallback;

		// Token: 0x040007AE RID: 1966
		private readonly ServerCertValidationCallbackWrapper callbackWrapper;

		// Token: 0x040007AF RID: 1967
		private readonly MonoTlsStream tlsStream;

		// Token: 0x040007B0 RID: 1968
		private readonly HttpWebRequest request;
	}
}
