using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the maximum length for response headers. This class cannot be inherited.</summary>
	// Token: 0x020006A0 RID: 1696
	public sealed class HttpWebRequestElement : ConfigurationElement
	{
		// Token: 0x06003531 RID: 13617 RVA: 0x000C492C File Offset: 0x000C2B2C
		static HttpWebRequestElement()
		{
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.maximumErrorResponseLengthProp);
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.maximumResponseHeadersLengthProp);
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.maximumUnauthorizedUploadLengthProp);
			HttpWebRequestElement.properties.Add(HttpWebRequestElement.useUnsafeHeaderParsingProp);
		}

		/// <summary>Gets or sets the maximum allowed length of an error response.</summary>
		/// <returns>A 32-bit signed integer containing the maximum length in kilobytes (1024 bytes) of the error response. The default value is 64.</returns>
		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06003533 RID: 13619 RVA: 0x000C49FD File Offset: 0x000C2BFD
		// (set) Token: 0x06003534 RID: 13620 RVA: 0x000C4A0F File Offset: 0x000C2C0F
		[ConfigurationProperty("maximumErrorResponseLength", DefaultValue = "64")]
		public int MaximumErrorResponseLength
		{
			get
			{
				return (int)base[HttpWebRequestElement.maximumErrorResponseLengthProp];
			}
			set
			{
				base[HttpWebRequestElement.maximumErrorResponseLengthProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum allowed length of the response headers.</summary>
		/// <returns>A 32-bit signed integer containing the maximum length in kilobytes (1024 bytes) of the response headers. The default value is 64.</returns>
		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06003535 RID: 13621 RVA: 0x000C4A22 File Offset: 0x000C2C22
		// (set) Token: 0x06003536 RID: 13622 RVA: 0x000C4A34 File Offset: 0x000C2C34
		[ConfigurationProperty("maximumResponseHeadersLength", DefaultValue = "64")]
		public int MaximumResponseHeadersLength
		{
			get
			{
				return (int)base[HttpWebRequestElement.maximumResponseHeadersLengthProp];
			}
			set
			{
				base[HttpWebRequestElement.maximumResponseHeadersLengthProp] = value;
			}
		}

		/// <summary>Gets or sets the maximum length of an upload in response to an unauthorized error code.</summary>
		/// <returns>A 32-bit signed integer containing the maximum length (in multiple of 1,024 byte units) of an upload in response to an unauthorized error code. A value of -1 indicates that no size limit will be imposed on the upload. Setting the <see cref="P:System.Net.Configuration.HttpWebRequestElement.MaximumUnauthorizedUploadLength" /> property to any other value will only send the request body if it is smaller than the number of bytes specified. So a value of 1 would indicate to only send the request body if it is smaller than 1,024 bytes. The default value for this property is -1.</returns>
		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06003537 RID: 13623 RVA: 0x000C4A47 File Offset: 0x000C2C47
		// (set) Token: 0x06003538 RID: 13624 RVA: 0x000C4A59 File Offset: 0x000C2C59
		[ConfigurationProperty("maximumUnauthorizedUploadLength", DefaultValue = "-1")]
		public int MaximumUnauthorizedUploadLength
		{
			get
			{
				return (int)base[HttpWebRequestElement.maximumUnauthorizedUploadLengthProp];
			}
			set
			{
				base[HttpWebRequestElement.maximumUnauthorizedUploadLengthProp] = value;
			}
		}

		/// <summary>Setting this property ignores validation errors that occur during HTTP parsing.</summary>
		/// <returns>Boolean that indicates whether this property has been set. </returns>
		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x000C4A6C File Offset: 0x000C2C6C
		// (set) Token: 0x0600353A RID: 13626 RVA: 0x000C4A7E File Offset: 0x000C2C7E
		[ConfigurationProperty("useUnsafeHeaderParsing", DefaultValue = "False")]
		public bool UseUnsafeHeaderParsing
		{
			get
			{
				return (bool)base[HttpWebRequestElement.useUnsafeHeaderParsingProp];
			}
			set
			{
				base[HttpWebRequestElement.useUnsafeHeaderParsingProp] = value;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x0600353B RID: 13627 RVA: 0x000C4A91 File Offset: 0x000C2C91
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return HttpWebRequestElement.properties;
			}
		}

		// Token: 0x0600353C RID: 13628 RVA: 0x000C4A98 File Offset: 0x000C2C98
		[MonoTODO]
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x04002A6C RID: 10860
		private static ConfigurationProperty maximumErrorResponseLengthProp = new ConfigurationProperty("maximumErrorResponseLength", typeof(int), 64);

		// Token: 0x04002A6D RID: 10861
		private static ConfigurationProperty maximumResponseHeadersLengthProp = new ConfigurationProperty("maximumResponseHeadersLength", typeof(int), 64);

		// Token: 0x04002A6E RID: 10862
		private static ConfigurationProperty maximumUnauthorizedUploadLengthProp = new ConfigurationProperty("maximumUnauthorizedUploadLength", typeof(int), -1);

		// Token: 0x04002A6F RID: 10863
		private static ConfigurationProperty useUnsafeHeaderParsingProp = new ConfigurationProperty("useUnsafeHeaderParsing", typeof(bool), false);

		// Token: 0x04002A70 RID: 10864
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
