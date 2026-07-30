using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Defines the alias associated with the target user agent for which ASP.NET server controls should render content. This class cannot be inherited.</summary>
	// Token: 0x0200058E RID: 1422
	public sealed class ClientTarget : ConfigurationElement
	{
		// Token: 0x06003C1A RID: 15386 RVA: 0x000A08D4 File Offset: 0x0009EAD4
		static ClientTarget()
		{
			ClientTarget.properties.Add(ClientTarget.aliasProp);
			ClientTarget.properties.Add(ClientTarget.userAgentProp);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ClientTarget" /> class.</summary>
		/// <param name="alias">The name used to refer to a specific user agent.</param>
		/// <param name="userAgent">The user agent's identification name.</param>
		// Token: 0x06003C1B RID: 15387 RVA: 0x000A0967 File Offset: 0x0009EB67
		public ClientTarget(string alias, string userAgent)
		{
			this.Alias = alias;
			this.UserAgent = userAgent;
		}

		/// <summary>Gets the user agent's alias.</summary>
		/// <returns>The name used to refer to a specific user agent. </returns>
		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06003C1C RID: 15388 RVA: 0x000A097D File Offset: 0x0009EB7D
		// (set) Token: 0x06003C1D RID: 15389 RVA: 0x000A098F File Offset: 0x0009EB8F
		[ConfigurationProperty("alias", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		[StringValidator(MinLength = 1)]
		public string Alias
		{
			get
			{
				return (string)base[ClientTarget.aliasProp];
			}
			internal set
			{
				base[ClientTarget.aliasProp] = value;
			}
		}

		/// <summary>Gets the user agent's identification name.</summary>
		/// <returns>The user agent's identification name.</returns>
		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06003C1E RID: 15390 RVA: 0x000A099D File Offset: 0x0009EB9D
		// (set) Token: 0x06003C1F RID: 15391 RVA: 0x000A09AF File Offset: 0x0009EBAF
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("userAgent", Options = ConfigurationPropertyOptions.IsRequired)]
		public string UserAgent
		{
			get
			{
				return (string)base[ClientTarget.userAgentProp];
			}
			internal set
			{
				base[ClientTarget.userAgentProp] = value;
			}
		}

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06003C20 RID: 15392 RVA: 0x000A09BD File Offset: 0x0009EBBD
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ClientTarget.properties;
			}
		}

		// Token: 0x040020AC RID: 8364
		private static ConfigurationProperty aliasProp = new ConfigurationProperty("alias", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x040020AD RID: 8365
		private static ConfigurationProperty userAgentProp = new ConfigurationProperty("userAgent", typeof(string), null, TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x040020AE RID: 8366
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
