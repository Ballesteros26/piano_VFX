using System;
using System.Configuration;

namespace System.Web.Configuration
{
	/// <summary>Configures the xhtmlConformance section. This class cannot be inherited. </summary>
	// Token: 0x020005F8 RID: 1528
	public sealed class XhtmlConformanceSection : ConfigurationSection
	{
		// Token: 0x0600424F RID: 16975 RVA: 0x000AD608 File Offset: 0x000AB808
		static XhtmlConformanceSection()
		{
			XhtmlConformanceSection.properties.Add(XhtmlConformanceSection.modeProp);
		}

		/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.XhtmlConformanceSection.Mode" /> property. </summary>
		/// <returns>One of the <see cref="T:System.Web.Configuration.XhtmlConformanceMode" /> values. The default is <see cref="F:System.Web.Configuration.XhtmlConformanceMode.Transitional" />.</returns>
		// Token: 0x1700150C RID: 5388
		// (get) Token: 0x06004250 RID: 16976 RVA: 0x000AD662 File Offset: 0x000AB862
		// (set) Token: 0x06004251 RID: 16977 RVA: 0x000AD674 File Offset: 0x000AB874
		[ConfigurationProperty("mode", DefaultValue = "Transitional")]
		public XhtmlConformanceMode Mode
		{
			get
			{
				return (XhtmlConformanceMode)base[XhtmlConformanceSection.modeProp];
			}
			set
			{
				base[XhtmlConformanceSection.modeProp] = value;
			}
		}

		// Token: 0x1700150D RID: 5389
		// (get) Token: 0x06004252 RID: 16978 RVA: 0x000AD687 File Offset: 0x000AB887
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return XhtmlConformanceSection.properties;
			}
		}

		// Token: 0x04002379 RID: 9081
		private static ConfigurationProperty modeProp = new ConfigurationProperty("mode", typeof(XhtmlConformanceMode), XhtmlConformanceMode.Transitional, new GenericEnumConverter(typeof(XhtmlConformanceMode)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);

		// Token: 0x0400237A RID: 9082
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
