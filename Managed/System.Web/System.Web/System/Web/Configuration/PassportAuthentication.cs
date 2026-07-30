using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures Passport-based authentication in ASP.NET applications.</summary>
	// Token: 0x020005C4 RID: 1476
	[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
	public sealed class PassportAuthentication : ConfigurationElement
	{
		// Token: 0x06003F7A RID: 16250 RVA: 0x000A7D68 File Offset: 0x000A5F68
		static PassportAuthentication()
		{
			PassportAuthentication.properties.Add(PassportAuthentication.redirectUrlProp);
			PassportAuthentication.elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(PassportAuthentication), new ValidatorCallback(PassportAuthentication.ValidateElement)));
		}

		// Token: 0x06003F7B RID: 16251 RVA: 0x0000393A File Offset: 0x00001B3A
		private static void ValidateElement(object o)
		{
		}

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x06003F7C RID: 16252 RVA: 0x000A7DD1 File Offset: 0x000A5FD1
		protected internal override ConfigurationElementProperty ElementProperty
		{
			get
			{
				return PassportAuthentication.elementProperty;
			}
		}

		/// <summary>Gets or sets the URL to which the request is redirected.</summary>
		/// <returns>The URL of the page to which the request is redirected.</returns>
		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x06003F7D RID: 16253 RVA: 0x000A7DD8 File Offset: 0x000A5FD8
		// (set) Token: 0x06003F7E RID: 16254 RVA: 0x000A7DEA File Offset: 0x000A5FEA
		[StringValidator]
		[ConfigurationProperty("redirectUrl", DefaultValue = "internal")]
		public string RedirectUrl
		{
			get
			{
				return (string)base[PassportAuthentication.redirectUrlProp];
			}
			set
			{
				base[PassportAuthentication.redirectUrlProp] = value;
			}
		}

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x06003F7F RID: 16255 RVA: 0x000A7DF8 File Offset: 0x000A5FF8
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PassportAuthentication.properties;
			}
		}

		// Token: 0x04002292 RID: 8850
		private static ConfigurationProperty redirectUrlProp = new ConfigurationProperty("redirectUrl", typeof(string), "internal");

		// Token: 0x04002293 RID: 8851
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002294 RID: 8852
		private static ConfigurationElementProperty elementProperty;
	}
}
