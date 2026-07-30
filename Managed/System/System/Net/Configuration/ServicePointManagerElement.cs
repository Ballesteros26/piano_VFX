using System;
using System.Configuration;
using System.Net.Security;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the default settings used to create connections to a remote computer. This class cannot be inherited.</summary>
	// Token: 0x020006AD RID: 1709
	public sealed class ServicePointManagerElement : ConfigurationElement
	{
		// Token: 0x06003578 RID: 13688 RVA: 0x000C5390 File Offset: 0x000C3590
		static ServicePointManagerElement()
		{
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.checkCertificateNameProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.checkCertificateRevocationListProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.dnsRefreshTimeoutProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.enableDnsRoundRobinProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.expect100ContinueProp);
			ServicePointManagerElement.properties.Add(ServicePointManagerElement.useNagleAlgorithmProp);
		}

		/// <summary>Gets or sets a Boolean value that controls checking host name information in an X509 certificate.</summary>
		/// <returns>true to specify host name checking; otherwise, false. </returns>
		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x0600357A RID: 13690 RVA: 0x000C54BF File Offset: 0x000C36BF
		// (set) Token: 0x0600357B RID: 13691 RVA: 0x000C54D1 File Offset: 0x000C36D1
		[ConfigurationProperty("checkCertificateName", DefaultValue = "True")]
		public bool CheckCertificateName
		{
			get
			{
				return (bool)base[ServicePointManagerElement.checkCertificateNameProp];
			}
			set
			{
				base[ServicePointManagerElement.checkCertificateNameProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the certificate is checked against the certificate authority revocation list.</summary>
		/// <returns>true if the certificate revocation list is checked; otherwise, false.The default value is false.</returns>
		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x0600357C RID: 13692 RVA: 0x000C54E4 File Offset: 0x000C36E4
		// (set) Token: 0x0600357D RID: 13693 RVA: 0x000C54F6 File Offset: 0x000C36F6
		[ConfigurationProperty("checkCertificateRevocationList", DefaultValue = "False")]
		public bool CheckCertificateRevocationList
		{
			get
			{
				return (bool)base[ServicePointManagerElement.checkCertificateRevocationListProp];
			}
			set
			{
				base[ServicePointManagerElement.checkCertificateRevocationListProp] = value;
			}
		}

		/// <summary>Gets or sets the amount of time after which address information is refreshed.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that specifies when addresses are resolved using DNS.</returns>
		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x0600357E RID: 13694 RVA: 0x000C5509 File Offset: 0x000C3709
		// (set) Token: 0x0600357F RID: 13695 RVA: 0x000C551B File Offset: 0x000C371B
		[ConfigurationProperty("dnsRefreshTimeout", DefaultValue = "120000")]
		public int DnsRefreshTimeout
		{
			get
			{
				return (int)base[ServicePointManagerElement.dnsRefreshTimeoutProp];
			}
			set
			{
				base[ServicePointManagerElement.dnsRefreshTimeoutProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that controls using different IP addresses on connections to the same server.</summary>
		/// <returns>true to enable DNS round-robin behavior; otherwise, false.</returns>
		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06003580 RID: 13696 RVA: 0x000C552E File Offset: 0x000C372E
		// (set) Token: 0x06003581 RID: 13697 RVA: 0x000C5540 File Offset: 0x000C3740
		[ConfigurationProperty("enableDnsRoundRobin", DefaultValue = "False")]
		public bool EnableDnsRoundRobin
		{
			get
			{
				return (bool)base[ServicePointManagerElement.enableDnsRoundRobinProp];
			}
			set
			{
				base[ServicePointManagerElement.enableDnsRoundRobinProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>true to expect 100-Continue responses for POST requests; otherwise, false. The default value is true.</returns>
		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06003582 RID: 13698 RVA: 0x000C5553 File Offset: 0x000C3753
		// (set) Token: 0x06003583 RID: 13699 RVA: 0x000C5565 File Offset: 0x000C3765
		[ConfigurationProperty("expect100Continue", DefaultValue = "True")]
		public bool Expect100Continue
		{
			get
			{
				return (bool)base[ServicePointManagerElement.expect100ContinueProp];
			}
			set
			{
				base[ServicePointManagerElement.expect100ContinueProp] = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that determines whether the Nagle algorithm is used.</summary>
		/// <returns>true to use the Nagle algorithm; otherwise, false. The default value is true.</returns>
		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06003584 RID: 13700 RVA: 0x000C5578 File Offset: 0x000C3778
		// (set) Token: 0x06003585 RID: 13701 RVA: 0x000C558A File Offset: 0x000C378A
		[ConfigurationProperty("useNagleAlgorithm", DefaultValue = "True")]
		public bool UseNagleAlgorithm
		{
			get
			{
				return (bool)base[ServicePointManagerElement.useNagleAlgorithmProp];
			}
			set
			{
				base[ServicePointManagerElement.useNagleAlgorithmProp] = value;
			}
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06003586 RID: 13702 RVA: 0x000C559D File Offset: 0x000C379D
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ServicePointManagerElement.properties;
			}
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x000027E8 File Offset: 0x000009E8
		[MonoTODO]
		protected override void PostDeserialize()
		{
		}

		/// <summary>Gets or sets the <see cref="T:System.Net.Security.EncryptionPolicy" /> to use.</summary>
		/// <returns>The encryption policy to use for a <see cref="T:System.Net.ServicePointManager" /> instance.</returns>
		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06003588 RID: 13704 RVA: 0x000C55A4 File Offset: 0x000C37A4
		// (set) Token: 0x06003589 RID: 13705 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		public EncryptionPolicy EncryptionPolicy
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return EncryptionPolicy.RequireEncryption;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x04002A90 RID: 10896
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002A91 RID: 10897
		private static ConfigurationProperty checkCertificateNameProp = new ConfigurationProperty("checkCertificateName", typeof(bool), true);

		// Token: 0x04002A92 RID: 10898
		private static ConfigurationProperty checkCertificateRevocationListProp = new ConfigurationProperty("checkCertificateRevocationList", typeof(bool), false);

		// Token: 0x04002A93 RID: 10899
		private static ConfigurationProperty dnsRefreshTimeoutProp = new ConfigurationProperty("dnsRefreshTimeout", typeof(int), 120000);

		// Token: 0x04002A94 RID: 10900
		private static ConfigurationProperty enableDnsRoundRobinProp = new ConfigurationProperty("enableDnsRoundRobin", typeof(bool), false);

		// Token: 0x04002A95 RID: 10901
		private static ConfigurationProperty expect100ContinueProp = new ConfigurationProperty("expect100Continue", typeof(bool), true);

		// Token: 0x04002A96 RID: 10902
		private static ConfigurationProperty useNagleAlgorithmProp = new ConfigurationProperty("useNagleAlgorithm", typeof(bool), true);
	}
}
