using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Maps a URL that is displayed to users to the URL of a page in your Web application. This class cannot be inherited.</summary>
	// Token: 0x020005E8 RID: 1512
	public sealed class UrlMapping : ConfigurationElement
	{
		// Token: 0x06004197 RID: 16791 RVA: 0x000ABA40 File Offset: 0x000A9C40
		private static void ValidateUrl(object value)
		{
			string text = value as string;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (!VirtualPathUtility.IsAppRelative(text))
			{
				throw new ConfigurationException("Only app-relative (~/) URLs are allowed");
			}
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x000ABA70 File Offset: 0x000A9C70
		static UrlMapping()
		{
			UrlMapping.properties.Add(UrlMapping.mappedUrlProp);
			UrlMapping.properties.Add(UrlMapping.urlProp);
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x0009F629 File Offset: 0x0009D829
		internal UrlMapping()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.UrlMapping" /> class.</summary>
		/// <param name="url">The URL to be displayed to the user.</param>
		/// <param name="mappedUrl">A URL that exists in your Web application.</param>
		// Token: 0x0600419A RID: 16794 RVA: 0x000ABB05 File Offset: 0x000A9D05
		public UrlMapping(string url, string mappedUrl)
		{
			this.Url = url;
			this.MappedUrl = mappedUrl;
		}

		/// <summary>A URL in your Web application.</summary>
		/// <returns>The URL in your Web application that has been mapped to the value specified by the <see cref="P:System.Web.Configuration.UrlMapping.Url" /> property. </returns>
		// Token: 0x170014E0 RID: 5344
		// (get) Token: 0x0600419B RID: 16795 RVA: 0x000ABB1B File Offset: 0x000A9D1B
		// (set) Token: 0x0600419C RID: 16796 RVA: 0x000ABB2D File Offset: 0x000A9D2D
		[ConfigurationProperty("mappedUrl", Options = ConfigurationPropertyOptions.IsRequired)]
		public string MappedUrl
		{
			get
			{
				return (string)base[UrlMapping.mappedUrlProp];
			}
			internal set
			{
				base[UrlMapping.mappedUrlProp] = value;
			}
		}

		/// <summary>Gets the URL that is displayed to the user.</summary>
		/// <returns>The URL that is displayed to the user.</returns>
		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x0600419D RID: 16797 RVA: 0x000ABB3B File Offset: 0x000A9D3B
		// (set) Token: 0x0600419E RID: 16798 RVA: 0x000ABB4D File Offset: 0x000A9D4D
		[ConfigurationProperty("url", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
		public string Url
		{
			get
			{
				return (string)base[UrlMapping.urlProp];
			}
			internal set
			{
				base[UrlMapping.urlProp] = value;
			}
		}

		// Token: 0x170014E2 RID: 5346
		// (get) Token: 0x0600419F RID: 16799 RVA: 0x000ABB5B File Offset: 0x000A9D5B
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return UrlMapping.properties;
			}
		}

		// Token: 0x0400233E RID: 9022
		private static ConfigurationProperty mappedUrlProp = new ConfigurationProperty("mappedUrl", typeof(string), null, PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400233F RID: 9023
		private static ConfigurationProperty urlProp = new ConfigurationProperty("url", typeof(string), null, PropertyHelper.WhiteSpaceTrimStringConverter, new CallbackValidator(typeof(string), new ValidatorCallback(UrlMapping.ValidateUrl)), ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002340 RID: 9024
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
