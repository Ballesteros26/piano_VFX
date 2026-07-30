using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Provides programmatic access to the webParts configuration file section. This class cannot be inherited.</summary>
	// Token: 0x020005F7 RID: 1527
	public sealed class WebPartsSection : ConfigurationSection
	{
		// Token: 0x06004247 RID: 16967 RVA: 0x000AD510 File Offset: 0x000AB710
		static WebPartsSection()
		{
			WebPartsSection.properties.Add(WebPartsSection.enableExportProp);
			WebPartsSection.properties.Add(WebPartsSection.personalizationProp);
			WebPartsSection.properties.Add(WebPartsSection.transformersProp);
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x00002058 File Offset: 0x00000258
		[global::System.MonoTODO("why override this?")]
		protected internal override object GetRuntimeObject()
		{
			return this;
		}

		/// <summary>Gets or sets a value indicating whether to enable the export of control data to an XML description file.</summary>
		/// <returns>true to enable the export of control data to an XML description file; otherwise, false.</returns>
		// Token: 0x17001508 RID: 5384
		// (get) Token: 0x06004249 RID: 16969 RVA: 0x000AD5B5 File Offset: 0x000AB7B5
		// (set) Token: 0x0600424A RID: 16970 RVA: 0x000AD5C7 File Offset: 0x000AB7C7
		[ConfigurationProperty("enableExport", DefaultValue = "False")]
		public bool EnableExport
		{
			get
			{
				return (bool)base[WebPartsSection.enableExportProp];
			}
			set
			{
				base[WebPartsSection.enableExportProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.Configuration.WebPartsPersonalization" /> object that allows you to specify the Web Parts personalization provider and set Web Parts personalization authorizations.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.WebPartsPersonalization" /> object that allows you to specify the personalization provider and set personalization authorizations.</returns>
		// Token: 0x17001509 RID: 5385
		// (get) Token: 0x0600424B RID: 16971 RVA: 0x000AD5DA File Offset: 0x000AB7DA
		[ConfigurationProperty("personalization")]
		public WebPartsPersonalization Personalization
		{
			get
			{
				return (WebPartsPersonalization)base[WebPartsSection.personalizationProp];
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Web.Configuration.TransformerInfo" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.TransformerInfoCollection" /> collection of <see cref="T:System.Web.Configuration.TransformerInfo" /> objects.</returns>
		// Token: 0x1700150A RID: 5386
		// (get) Token: 0x0600424C RID: 16972 RVA: 0x000AD5EC File Offset: 0x000AB7EC
		[ConfigurationProperty("transformers")]
		public TransformerInfoCollection Transformers
		{
			get
			{
				return (TransformerInfoCollection)base[WebPartsSection.transformersProp];
			}
		}

		// Token: 0x1700150B RID: 5387
		// (get) Token: 0x0600424D RID: 16973 RVA: 0x000AD5FE File Offset: 0x000AB7FE
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebPartsSection.properties;
			}
		}

		// Token: 0x04002375 RID: 9077
		private static ConfigurationProperty enableExportProp = new ConfigurationProperty("enableExport", typeof(bool), false);

		// Token: 0x04002376 RID: 9078
		private static ConfigurationProperty personalizationProp = new ConfigurationProperty("personalization", typeof(WebPartsPersonalization), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002377 RID: 9079
		private static ConfigurationProperty transformersProp = new ConfigurationProperty("transformers", typeof(TransformerInfoCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x04002378 RID: 9080
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
