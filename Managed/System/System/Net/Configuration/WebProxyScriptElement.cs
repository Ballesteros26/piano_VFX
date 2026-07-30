using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents information used to configure Web proxy scripts. This class cannot be inherited.</summary>
	// Token: 0x020006B3 RID: 1715
	public sealed class WebProxyScriptElement : ConfigurationElement
	{
		// Token: 0x060035C0 RID: 13760 RVA: 0x000C59FC File Offset: 0x000C3BFC
		static WebProxyScriptElement()
		{
			WebProxyScriptElement.properties.Add(WebProxyScriptElement.downloadTimeoutProp);
		}

		// Token: 0x060035C1 RID: 13761 RVA: 0x000027E8 File Offset: 0x000009E8
		protected override void PostDeserialize()
		{
		}

		/// <summary>Gets or sets the Web proxy script download timeout using the format hours:minutes:seconds.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object that contains the timeout value. The default download timeout is one minute.</returns>
		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x060035C2 RID: 13762 RVA: 0x000C5A49 File Offset: 0x000C3C49
		// (set) Token: 0x060035C3 RID: 13763 RVA: 0x000C5A5B File Offset: 0x000C3C5B
		[ConfigurationProperty("downloadTimeout", DefaultValue = "00:02:00")]
		public TimeSpan DownloadTimeout
		{
			get
			{
				return (TimeSpan)base[WebProxyScriptElement.downloadTimeoutProp];
			}
			set
			{
				base[WebProxyScriptElement.downloadTimeoutProp] = value;
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x060035C4 RID: 13764 RVA: 0x000C5A6E File Offset: 0x000C3C6E
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebProxyScriptElement.properties;
			}
		}

		// Token: 0x04002AA3 RID: 10915
		private static ConfigurationProperty downloadTimeoutProp = new ConfigurationProperty("downloadTimeout", typeof(TimeSpan), new TimeSpan(0, 0, 2, 0));

		// Token: 0x04002AA4 RID: 10916
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
