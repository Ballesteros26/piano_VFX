using System;
using System.ComponentModel;
using System.Configuration;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures the code-access security level that is applied to an application. This class cannot be inherited.</summary>
	// Token: 0x020005E7 RID: 1511
	public sealed class TrustSection : ConfigurationSection
	{
		// Token: 0x06004188 RID: 16776 RVA: 0x000AB904 File Offset: 0x000A9B04
		static TrustSection()
		{
			TrustSection.properties.Add(TrustSection.levelProp);
			TrustSection.properties.Add(TrustSection.originUrlProp);
			TrustSection.properties.Add(TrustSection.processRequestInApplicationTrustProp);
		}

		/// <summary>Gets or sets the name of the security level under which the application will run. </summary>
		/// <returns>The name of the trust level. The default is "Full".</returns>
		// Token: 0x170014D9 RID: 5337
		// (get) Token: 0x06004189 RID: 16777 RVA: 0x000AB9B8 File Offset: 0x000A9BB8
		// (set) Token: 0x0600418A RID: 16778 RVA: 0x000AB9CA File Offset: 0x000A9BCA
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("level", DefaultValue = "Full", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Level
		{
			get
			{
				return (string)base[TrustSection.levelProp];
			}
			set
			{
				base[TrustSection.levelProp] = value;
			}
		}

		/// <summary>Specifies the URL of origin for an application.</summary>
		/// <returns>A well-formed HTTP URL or an empty string (""). The default is an empty string.</returns>
		// Token: 0x170014DA RID: 5338
		// (get) Token: 0x0600418B RID: 16779 RVA: 0x000AB9D8 File Offset: 0x000A9BD8
		// (set) Token: 0x0600418C RID: 16780 RVA: 0x000AB9EA File Offset: 0x000A9BEA
		[ConfigurationProperty("originUrl", DefaultValue = "")]
		public string OriginUrl
		{
			get
			{
				return (string)base[TrustSection.originUrlProp];
			}
			set
			{
				base[TrustSection.originUrlProp] = value;
			}
		}

		/// <summary>Gets or set a value that indicates whether page requests are automatically restricted to the permissions that are configured in the trust policy file that is applied to the ASP.NET application.</summary>
		/// <returns>true if requests are automatically restricted to the permissions that are configured in the trust policy file; otherwise, false.</returns>
		// Token: 0x170014DB RID: 5339
		// (get) Token: 0x0600418D RID: 16781 RVA: 0x000AB9F8 File Offset: 0x000A9BF8
		// (set) Token: 0x0600418E RID: 16782 RVA: 0x000ABA0A File Offset: 0x000A9C0A
		[ConfigurationProperty("processRequestInApplicationTrust", DefaultValue = "True")]
		public bool ProcessRequestInApplicationTrust
		{
			get
			{
				return (bool)base[TrustSection.processRequestInApplicationTrustProp];
			}
			set
			{
				base[TrustSection.processRequestInApplicationTrustProp] = value;
			}
		}

		// Token: 0x170014DC RID: 5340
		// (get) Token: 0x0600418F RID: 16783 RVA: 0x000ABA1D File Offset: 0x000A9C1D
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return TrustSection.properties;
			}
		}

		/// <summary>Gets or sets the custom security-policy resolution type.</summary>
		/// <returns>The custom security-policy resolution type.</returns>
		// Token: 0x170014DD RID: 5341
		// (get) Token: 0x06004191 RID: 16785 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004192 RID: 16786 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string HostSecurityPolicyResolverType
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or set a value that indicates whether the legacy code access security is enabled.</summary>
		/// <returns>true if legacy code access security is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x170014DE RID: 5342
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x000ABA24 File Offset: 0x000A9C24
		// (set) Token: 0x06004194 RID: 16788 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool LegacyCasModel
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <summary>Gets or sets the name of the permission set. </summary>
		/// <returns>The name of the permission set.</returns>
		// Token: 0x170014DF RID: 5343
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x0000E80B File Offset: 0x0000CA0B
		// (set) Token: 0x06004196 RID: 16790 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public string PermissionSetName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x0400233A RID: 9018
		private static ConfigurationProperty levelProp = new ConfigurationProperty("level", typeof(string), "Full", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400233B RID: 9019
		private static ConfigurationProperty originUrlProp = new ConfigurationProperty("originUrl", typeof(string), "");

		// Token: 0x0400233C RID: 9020
		private static ConfigurationProperty processRequestInApplicationTrustProp = new ConfigurationProperty("processRequestInApplicationTrust", typeof(bool), true);

		// Token: 0x0400233D RID: 9021
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
