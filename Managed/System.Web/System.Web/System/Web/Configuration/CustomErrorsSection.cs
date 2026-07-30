using System;
using System.Configuration;
using System.Xml;
using Unity;

namespace System.Web.Configuration
{
	/// <summary>Configures the ASP.NET custom errors. This class cannot be inherited.</summary>
	// Token: 0x02000598 RID: 1432
	public sealed class CustomErrorsSection : ConfigurationSection
	{
		// Token: 0x06003CAF RID: 15535 RVA: 0x000A16F8 File Offset: 0x0009F8F8
		static CustomErrorsSection()
		{
			CustomErrorsSection.properties.Add(CustomErrorsSection.defaultRedirectProp);
			CustomErrorsSection.properties.Add(CustomErrorsSection.errorsProp);
			CustomErrorsSection.properties.Add(CustomErrorsSection.modeProp);
			CustomErrorsSection.properties.Add(CustomErrorsSection.redirectModeProp);
		}

		// Token: 0x06003CB0 RID: 15536 RVA: 0x000A17EE File Offset: 0x0009F9EE
		protected internal override void DeserializeSection(XmlReader reader)
		{
			base.DeserializeSection(reader);
		}

		// Token: 0x06003CB1 RID: 15537 RVA: 0x0009F722 File Offset: 0x0009D922
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
		}

		/// <summary>Gets or sets the default URL for redirection.</summary>
		/// <returns>The default URL to which the application is redirected when an error occurs.</returns>
		/// <exception cref="T:System.NullReferenceException">The <see cref="P:System.Web.Configuration.CustomErrorsSection.DefaultRedirect" /> property is null. This is the default.</exception>
		// Token: 0x170012AF RID: 4783
		// (get) Token: 0x06003CB2 RID: 15538 RVA: 0x000A17F7 File Offset: 0x0009F9F7
		// (set) Token: 0x06003CB3 RID: 15539 RVA: 0x000A1809 File Offset: 0x0009FA09
		[ConfigurationProperty("defaultRedirect")]
		public string DefaultRedirect
		{
			get
			{
				return (string)base[CustomErrorsSection.defaultRedirectProp];
			}
			set
			{
				base[CustomErrorsSection.defaultRedirectProp] = value;
			}
		}

		/// <summary>Gets the collection of the <see cref="T:System.Web.Configuration.CustomError" /> objects.</summary>
		/// <returns>A <see cref="T:System.Web.Configuration.CustomErrorCollection" /> that contains the custom errors.</returns>
		// Token: 0x170012B0 RID: 4784
		// (get) Token: 0x06003CB4 RID: 15540 RVA: 0x000A1817 File Offset: 0x0009FA17
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public CustomErrorCollection Errors
		{
			get
			{
				return (CustomErrorCollection)base[CustomErrorsSection.errorsProp];
			}
		}

		/// <summary>Gets or sets the error display modality.</summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.CustomErrorsMode" /> values. The default is <see cref="F:System.Web.Configuration.CustomErrorsMode.RemoteOnly" />.</returns>
		// Token: 0x170012B1 RID: 4785
		// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x000A1829 File Offset: 0x0009FA29
		// (set) Token: 0x06003CB6 RID: 15542 RVA: 0x000A183B File Offset: 0x0009FA3B
		[ConfigurationProperty("mode", DefaultValue = "RemoteOnly")]
		public CustomErrorsMode Mode
		{
			get
			{
				return (CustomErrorsMode)base[CustomErrorsSection.modeProp];
			}
			set
			{
				base[CustomErrorsSection.modeProp] = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the URL of the request should be changed when the user is redirected to a custom error page.</summary>
		/// <returns>A value that indicates whether the URL is changed when the user is redirected to the custom error page. The default value is <see cref="F:System.Web.Configuration.CustomErrorsRedirectMode.ResponseRedirect" />.</returns>
		// Token: 0x170012B2 RID: 4786
		// (get) Token: 0x06003CB7 RID: 15543 RVA: 0x000A184E File Offset: 0x0009FA4E
		// (set) Token: 0x06003CB8 RID: 15544 RVA: 0x000A1860 File Offset: 0x0009FA60
		[ConfigurationProperty("redirectMode", DefaultValue = CustomErrorsRedirectMode.ResponseRedirect)]
		public CustomErrorsRedirectMode RedirectMode
		{
			get
			{
				return (CustomErrorsRedirectMode)base[CustomErrorsSection.redirectModeProp];
			}
			set
			{
				base[CustomErrorsSection.redirectModeProp] = value;
			}
		}

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x000A1873 File Offset: 0x0009FA73
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CustomErrorsSection.properties;
			}
		}

		/// <summary>Gets or sets a value that indicates whether ASP.NET should display a generic error message when the custom error page cannot be displayed.</summary>
		/// <returns>true if a generic error message should be displayed when the custom error page cannot be displayed; otherwise, false. The default is false.</returns>
		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06003CBB RID: 15547 RVA: 0x000A187C File Offset: 0x0009FA7C
		// (set) Token: 0x06003CBC RID: 15548 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool AllowNestedErrors
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

		// Token: 0x040020D4 RID: 8404
		private static ConfigurationProperty defaultRedirectProp = new ConfigurationProperty("defaultRedirect", typeof(string), null);

		// Token: 0x040020D5 RID: 8405
		private static ConfigurationProperty errorsProp = new ConfigurationProperty(string.Empty, typeof(CustomErrorCollection), null, null, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x040020D6 RID: 8406
		private static ConfigurationProperty modeProp = new ConfigurationProperty("mode", typeof(CustomErrorsMode), CustomErrorsMode.RemoteOnly, new GenericEnumConverter(typeof(CustomErrorsMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020D7 RID: 8407
		private static ConfigurationProperty redirectModeProp = new ConfigurationProperty("redirectMode", typeof(CustomErrorsRedirectMode), CustomErrorsRedirectMode.ResponseRedirect, new GenericEnumConverter(typeof(CustomErrorsRedirectMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x040020D8 RID: 8408
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
