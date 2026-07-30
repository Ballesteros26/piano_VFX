using System;
using System.Collections;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Mono.Security.Interface;
using Mono.Security.X509;
using Mono.Security.X509.Extensions;

namespace Mono.Security.Protocol.Tls.Handshake.Client
{
	// Token: 0x02000066 RID: 102
	internal class TlsServerCertificate : HandshakeMessage
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x00014B28 File Offset: 0x00012D28
		public TlsServerCertificate(Context context, byte[] buffer)
			: base(context, HandshakeType.Certificate, buffer)
		{
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00014B34 File Offset: 0x00012D34
		public override void Update()
		{
			base.Update();
			base.Context.ServerSettings.Certificates = this.certificates;
			base.Context.ServerSettings.UpdateCertificateRSA();
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00014B62 File Offset: 0x00012D62
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00014B6C File Offset: 0x00012D6C
		protected override void ProcessAsTls1()
		{
			this.certificates = new Mono.Security.X509.X509CertificateCollection();
			int i = 0;
			int num = base.ReadInt24();
			while (i < num)
			{
				int num2 = base.ReadInt24();
				i += 3;
				if (num2 > 0)
				{
					Mono.Security.X509.X509Certificate x509Certificate = new Mono.Security.X509.X509Certificate(base.ReadBytes(num2));
					this.certificates.Add(x509Certificate);
					i += num2;
				}
			}
			this.validateCertificates(this.certificates);
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00014BCC File Offset: 0x00012DCC
		private bool checkCertificateUsage(Mono.Security.X509.X509Certificate cert)
		{
			ClientContext clientContext = (ClientContext)base.Context;
			if (cert.Version < 3)
			{
				return true;
			}
			KeyUsages keyUsages = KeyUsages.none;
			switch (clientContext.Negotiating.Cipher.ExchangeAlgorithmType)
			{
			case ExchangeAlgorithmType.DiffieHellman:
				keyUsages = KeyUsages.keyAgreement;
				break;
			case ExchangeAlgorithmType.Fortezza:
				return false;
			case ExchangeAlgorithmType.RsaKeyX:
				keyUsages = KeyUsages.keyEncipherment;
				break;
			case ExchangeAlgorithmType.RsaSign:
				keyUsages = KeyUsages.digitalSignature;
				break;
			}
			KeyUsageExtension keyUsageExtension = null;
			ExtendedKeyUsageExtension extendedKeyUsageExtension = null;
			Mono.Security.X509.X509Extension x509Extension = cert.Extensions["2.5.29.15"];
			if (x509Extension != null)
			{
				keyUsageExtension = new KeyUsageExtension(x509Extension);
			}
			x509Extension = cert.Extensions["2.5.29.37"];
			if (x509Extension != null)
			{
				extendedKeyUsageExtension = new ExtendedKeyUsageExtension(x509Extension);
			}
			if (keyUsageExtension != null && extendedKeyUsageExtension != null)
			{
				return keyUsageExtension.Support(keyUsages) && (extendedKeyUsageExtension.KeyPurpose.Contains("1.3.6.1.5.5.7.3.1") || extendedKeyUsageExtension.KeyPurpose.Contains("2.16.840.1.113730.4.1"));
			}
			if (keyUsageExtension != null)
			{
				return keyUsageExtension.Support(keyUsages);
			}
			if (extendedKeyUsageExtension != null)
			{
				return extendedKeyUsageExtension.KeyPurpose.Contains("1.3.6.1.5.5.7.3.1") || extendedKeyUsageExtension.KeyPurpose.Contains("2.16.840.1.113730.4.1");
			}
			x509Extension = cert.Extensions["2.16.840.1.113730.1.1"];
			return x509Extension == null || new NetscapeCertTypeExtension(x509Extension).Support(NetscapeCertTypeExtension.CertTypes.SslServer);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00014D04 File Offset: 0x00012F04
		private void validateCertificates(Mono.Security.X509.X509CertificateCollection certificates)
		{
			ClientContext clientContext = (ClientContext)base.Context;
			AlertDescription alertDescription = AlertDescription.BadCertificate;
			if (clientContext.SslStream.HaveRemoteValidation2Callback)
			{
				this.RemoteValidation(clientContext, alertDescription);
				return;
			}
			this.LocalValidation(clientContext, alertDescription);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00014D40 File Offset: 0x00012F40
		private void RemoteValidation(ClientContext context, AlertDescription description)
		{
			ValidationResult validationResult = context.SslStream.RaiseServerCertificateValidation2(this.certificates);
			if (validationResult.Trusted)
			{
				return;
			}
			long num = (long)validationResult.ErrorCode;
			if (num != (long)((ulong)(-2146762495)))
			{
				if (num != (long)((ulong)(-2146762487)))
				{
					if (num != (long)((ulong)(-2146762486)))
					{
						description = AlertDescription.CertificateUnknown;
					}
					else
					{
						description = AlertDescription.UnknownCA;
					}
				}
				else
				{
					description = AlertDescription.UnknownCA;
				}
			}
			else
			{
				description = AlertDescription.CertificateExpired;
			}
			string text = string.Format("Invalid certificate received from server. Error code: 0x{0:x}", num);
			throw new TlsException(description, text);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00014DBC File Offset: 0x00012FBC
		private void LocalValidation(ClientContext context, AlertDescription description)
		{
			Mono.Security.X509.X509Certificate x509Certificate = this.certificates[0];
			global::System.Security.Cryptography.X509Certificates.X509Certificate x509Certificate2 = new global::System.Security.Cryptography.X509Certificates.X509Certificate(x509Certificate.RawData);
			ArrayList arrayList = new ArrayList();
			if (!this.checkCertificateUsage(x509Certificate))
			{
				arrayList.Add(-2146762490);
			}
			if (!this.checkServerIdentity(x509Certificate))
			{
				arrayList.Add(-2146762481);
			}
			Mono.Security.X509.X509CertificateCollection x509CertificateCollection = new Mono.Security.X509.X509CertificateCollection(this.certificates);
			x509CertificateCollection.Remove(x509Certificate);
			Mono.Security.X509.X509Chain x509Chain = new Mono.Security.X509.X509Chain(x509CertificateCollection);
			bool flag = false;
			try
			{
				flag = x509Chain.Build(x509Certificate);
			}
			catch (Exception)
			{
				flag = false;
			}
			if (!flag)
			{
				Mono.Security.X509.X509ChainStatusFlags status = x509Chain.Status;
				if (status <= Mono.Security.X509.X509ChainStatusFlags.NotSignatureValid)
				{
					if (status == Mono.Security.X509.X509ChainStatusFlags.NotTimeValid)
					{
						description = AlertDescription.CertificateExpired;
						arrayList.Add(-2146762495);
						goto IL_015E;
					}
					if (status == Mono.Security.X509.X509ChainStatusFlags.NotTimeNested)
					{
						arrayList.Add(-2146762494);
						goto IL_015E;
					}
					if (status == Mono.Security.X509.X509ChainStatusFlags.NotSignatureValid)
					{
						arrayList.Add(-2146869232);
						goto IL_015E;
					}
				}
				else
				{
					if (status == Mono.Security.X509.X509ChainStatusFlags.UntrustedRoot)
					{
						description = AlertDescription.UnknownCA;
						arrayList.Add(-2146762487);
						goto IL_015E;
					}
					if (status == Mono.Security.X509.X509ChainStatusFlags.InvalidBasicConstraints)
					{
						arrayList.Add(-2146869223);
						goto IL_015E;
					}
					if (status == Mono.Security.X509.X509ChainStatusFlags.PartialChain)
					{
						description = AlertDescription.UnknownCA;
						arrayList.Add(-2146762486);
						goto IL_015E;
					}
				}
				description = AlertDescription.CertificateUnknown;
				arrayList.Add((int)x509Chain.Status);
			}
			IL_015E:
			int[] array = (int[])arrayList.ToArray(typeof(int));
			if (!context.SslStream.RaiseServerCertificateValidation(x509Certificate2, array))
			{
				throw new TlsException(description, "Invalid certificate received from server.");
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00014F6C File Offset: 0x0001316C
		private bool checkServerIdentity(Mono.Security.X509.X509Certificate cert)
		{
			string targetHost = ((ClientContext)base.Context).ClientSettings.TargetHost;
			Mono.Security.X509.X509Extension x509Extension = cert.Extensions["2.5.29.17"];
			if (x509Extension != null)
			{
				SubjectAltNameExtension subjectAltNameExtension = new SubjectAltNameExtension(x509Extension);
				foreach (string text in subjectAltNameExtension.DNSNames)
				{
					if (TlsServerCertificate.Match(targetHost, text))
					{
						return true;
					}
				}
				string[] array = subjectAltNameExtension.IPAddresses;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == targetHost)
					{
						return true;
					}
				}
			}
			return this.checkDomainName(cert.SubjectName);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00015008 File Offset: 0x00013208
		private bool checkDomainName(string subjectName)
		{
			Context context = (ClientContext)base.Context;
			string text = string.Empty;
			MatchCollection matchCollection = new Regex("CN\\s*=\\s*([^,]*)").Matches(subjectName);
			if (matchCollection.Count == 1 && matchCollection[0].Success)
			{
				text = matchCollection[0].Groups[1].Value.ToString();
			}
			return TlsServerCertificate.Match(context.ClientSettings.TargetHost, text);
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001507C File Offset: 0x0001327C
		private static bool Match(string hostname, string pattern)
		{
			int num = pattern.IndexOf('*');
			if (num == -1)
			{
				return string.Compare(hostname, pattern, true, CultureInfo.InvariantCulture) == 0;
			}
			if (num != pattern.Length - 1 && pattern[num + 1] != '.')
			{
				return false;
			}
			if (pattern.IndexOf('*', num + 1) != -1)
			{
				return false;
			}
			string text = pattern.Substring(num + 1);
			int num2 = hostname.Length - text.Length;
			if (num2 <= 0)
			{
				return false;
			}
			if (string.Compare(hostname, num2, text, 0, text.Length, true, CultureInfo.InvariantCulture) != 0)
			{
				return false;
			}
			if (num == 0)
			{
				int num3 = hostname.IndexOf('.');
				return num3 == -1 || num3 >= hostname.Length - text.Length;
			}
			string text2 = pattern.Substring(0, num);
			return string.Compare(hostname, 0, text2, 0, text2.Length, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x040001E7 RID: 487
		private Mono.Security.X509.X509CertificateCollection certificates;
	}
}
