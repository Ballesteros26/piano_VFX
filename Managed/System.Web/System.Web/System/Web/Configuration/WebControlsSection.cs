using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the webControls section. This class cannot be inherited. </summary>
	// Token: 0x020005F4 RID: 1524
	public sealed class WebControlsSection : ConfigurationSection
	{
		// Token: 0x06004237 RID: 16951 RVA: 0x000AD328 File Offset: 0x000AB528
		static WebControlsSection()
		{
			WebControlsSection.properties.Add(WebControlsSection.clientScriptsLocationProp);
		}

		// Token: 0x06004238 RID: 16952 RVA: 0x000AD381 File Offset: 0x000AB581
		protected internal override object GetRuntimeObject()
		{
			return new Hashtable { { "clientScriptsLocation", this.ClientScriptsLocation } };
		}

		/// <summary>Gets the client scripts location.</summary>
		/// <returns>The location of the client scripts.</returns>
		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x06004239 RID: 16953 RVA: 0x000AD399 File Offset: 0x000AB599
		[StringValidator(MinLength = 1)]
		[ConfigurationProperty("clientScriptsLocation", DefaultValue = "/aspnet_client/{0}/{1}/", Options = ConfigurationPropertyOptions.IsRequired)]
		public string ClientScriptsLocation
		{
			get
			{
				return (string)base[WebControlsSection.clientScriptsLocationProp];
			}
		}

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x0600423A RID: 16954 RVA: 0x000AD3AB File Offset: 0x000AB5AB
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebControlsSection.properties;
			}
		}

		// Token: 0x0400236D RID: 9069
		private static ConfigurationProperty clientScriptsLocationProp = new ConfigurationProperty("clientScriptsLocation", typeof(string), "/aspnet_client/{0}/{1}/", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400236E RID: 9070
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
