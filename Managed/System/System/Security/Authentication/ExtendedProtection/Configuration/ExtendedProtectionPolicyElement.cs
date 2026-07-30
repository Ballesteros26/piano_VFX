using System;
using System.Configuration;

namespace System.Security.Authentication.ExtendedProtection.Configuration
{
	/// <summary>The <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ExtendedProtectionPolicyElement" /> class represents a configuration element for an <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" />.</summary>
	// Token: 0x0200038A RID: 906
	[MonoTODO]
	public sealed class ExtendedProtectionPolicyElement : ConfigurationElement
	{
		// Token: 0x06001B71 RID: 7025 RVA: 0x0006D604 File Offset: 0x0006B804
		static ExtendedProtectionPolicyElement()
		{
			Type typeFromHandle = typeof(ExtendedProtectionPolicyElement);
			ExtendedProtectionPolicyElement.custom_service_names = ConfigUtil.BuildProperty(typeFromHandle, "CustomServiceNames");
			ExtendedProtectionPolicyElement.policy_enforcement = ConfigUtil.BuildProperty(typeFromHandle, "PolicyEnforcement");
			ExtendedProtectionPolicyElement.protection_scenario = ConfigUtil.BuildProperty(typeFromHandle, "ProtectionScenario");
			foreach (ConfigurationProperty configurationProperty in new ConfigurationProperty[]
			{
				ExtendedProtectionPolicyElement.custom_service_names,
				ExtendedProtectionPolicyElement.policy_enforcement,
				ExtendedProtectionPolicyElement.protection_scenario
			})
			{
				ExtendedProtectionPolicyElement.properties.Add(configurationProperty);
			}
		}

		/// <summary>Gets or sets the custom Service Provider Name (SPN) list used to match against a client's SPN for this configuration policy element. </summary>
		/// <returns>Returns a <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ServiceNameElementCollection" /> that includes the custom SPN list used to match against a client's SPN.</returns>
		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x0006D690 File Offset: 0x0006B890
		[ConfigurationProperty("customServiceNames")]
		public ServiceNameElementCollection CustomServiceNames
		{
			get
			{
				return (ServiceNameElementCollection)base[ExtendedProtectionPolicyElement.custom_service_names];
			}
		}

		/// <summary>Gets or sets the policy enforcement value for this configuration policy element.</summary>
		/// <returns>Returns a <see cref="T:System.Security.Authentication.ExtendedProtection.PolicyEnforcement" /> value that indicates when the extended protection policy should be enforced.</returns>
		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x0006D6A2 File Offset: 0x0006B8A2
		// (set) Token: 0x06001B74 RID: 7028 RVA: 0x0006D6B4 File Offset: 0x0006B8B4
		[ConfigurationProperty("policyEnforcement")]
		public PolicyEnforcement PolicyEnforcement
		{
			get
			{
				return (PolicyEnforcement)base[ExtendedProtectionPolicyElement.policy_enforcement];
			}
			set
			{
				base[ExtendedProtectionPolicyElement.policy_enforcement] = value;
			}
		}

		/// <summary>Gets or sets the kind of protection enforced by the extended protection policy for this configuration policy element.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ProtectionScenario" /> value that indicates the kind of protection enforced by the policy.</returns>
		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0006D6C7 File Offset: 0x0006B8C7
		// (set) Token: 0x06001B76 RID: 7030 RVA: 0x0006D6D9 File Offset: 0x0006B8D9
		[ConfigurationProperty("protectionScenario", DefaultValue = ProtectionScenario.TransportSelected)]
		public ProtectionScenario ProtectionScenario
		{
			get
			{
				return (ProtectionScenario)base[ExtendedProtectionPolicyElement.protection_scenario];
			}
			set
			{
				base[ExtendedProtectionPolicyElement.protection_scenario] = value;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x0006D6EC File Offset: 0x0006B8EC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ExtendedProtectionPolicyElement.properties;
			}
		}

		/// <summary>The <see cref="M:System.Security.Authentication.ExtendedProtection.Configuration.ExtendedProtectionPolicyElement.BuildPolicy" /> method builds a new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance based on the properties set on the <see cref="T:System.Security.Authentication.ExtendedProtection.Configuration.ExtendedProtectionPolicyElement" /> class. </summary>
		/// <returns>A new <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> instance that represents the extended protection policy created.</returns>
		// Token: 0x06001B78 RID: 7032 RVA: 0x00004239 File Offset: 0x00002439
		public ExtendedProtectionPolicy BuildPolicy()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040018D3 RID: 6355
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x040018D4 RID: 6356
		private static ConfigurationProperty custom_service_names;

		// Token: 0x040018D5 RID: 6357
		private static ConfigurationProperty policy_enforcement;

		// Token: 0x040018D6 RID: 6358
		private static ConfigurationProperty protection_scenario;
	}
}
