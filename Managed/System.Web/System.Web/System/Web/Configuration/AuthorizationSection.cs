using System;
using System.Configuration;
using System.Security.Principal;

namespace System.Web.Configuration
{
	/// <summary>Configures a Web application authorization. This class cannot be inherited.</summary>
	// Token: 0x02000586 RID: 1414
	public sealed class AuthorizationSection : ConfigurationSection
	{
		// Token: 0x06003BC2 RID: 15298 RVA: 0x0009FE41 File Offset: 0x0009E041
		static AuthorizationSection()
		{
			AuthorizationSection.properties.Add(AuthorizationSection.rulesProp);
		}

		// Token: 0x06003BC3 RID: 15299 RVA: 0x0009FE7D File Offset: 0x0009E07D
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		/// <summary>Gets the <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" /> of <see cref="T:System.Web.Configuration.AuthorizationRule" /> rules.</summary>
		/// <returns>Gets the <see cref="T:System.Web.Configuration.AuthorizationRuleCollection" /> of <see cref="T:System.Web.Configuration.AuthorizationRule" /> rules defined by the <see cref="T:System.Web.Configuration.AuthorizationSection" />.</returns>
		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x06003BC4 RID: 15300 RVA: 0x0009FE85 File Offset: 0x0009E085
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public AuthorizationRuleCollection Rules
		{
			get
			{
				return (AuthorizationRuleCollection)base[AuthorizationSection.rulesProp];
			}
		}

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06003BC5 RID: 15301 RVA: 0x0009FE97 File Offset: 0x0009E097
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AuthorizationSection.properties;
			}
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x0009FEA0 File Offset: 0x0009E0A0
		internal bool IsValidUser(IPrincipal user, string verb)
		{
			string text = ((user == null) ? string.Empty : user.Identity.Name);
			foreach (object obj in this.Rules)
			{
				AuthorizationRule authorizationRule = (AuthorizationRule)obj;
				if ((authorizationRule.Verbs.Count == 0 || authorizationRule.CheckVerb(verb)) && (authorizationRule.CheckUser(text) || (user != null && authorizationRule.CheckRole(user))))
				{
					return authorizationRule.Action == AuthorizationRuleAction.Allow;
				}
			}
			return true;
		}

		// Token: 0x04002096 RID: 8342
		private static ConfigurationProperty rulesProp = new ConfigurationProperty(string.Empty, typeof(AuthorizationRuleCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04002097 RID: 8343
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
