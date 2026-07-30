using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Xml.XmlConfiguration
{
	/// <summary>Represents an XSLT configuration section.</summary>
	// Token: 0x020004B9 RID: 1209
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XsltConfigSection : ConfigurationSection
	{
		/// <summary>Gets or sets a string that represents the XSLT prohibit default resolver.</summary>
		/// <returns>A string that represents the XSLT prohibit default resolver.</returns>
		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x0600310E RID: 12558 RVA: 0x0011C411 File Offset: 0x0011A611
		// (set) Token: 0x0600310F RID: 12559 RVA: 0x0011C423 File Offset: 0x0011A623
		[ConfigurationProperty("prohibitDefaultResolver", DefaultValue = "false")]
		public string ProhibitDefaultResolverString
		{
			get
			{
				return (string)base["prohibitDefaultResolver"];
			}
			set
			{
				base["prohibitDefaultResolver"] = value;
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06003110 RID: 12560 RVA: 0x0011C4F4 File Offset: 0x0011A6F4
		private bool _ProhibitDefaultResolver
		{
			get
			{
				bool flag;
				XmlConvert.TryToBoolean(this.ProhibitDefaultResolverString, out flag);
				return flag;
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06003111 RID: 12561 RVA: 0x0011C510 File Offset: 0x0011A710
		private static bool s_ProhibitDefaultUrlResolver
		{
			get
			{
				XsltConfigSection xsltConfigSection = ConfigurationManager.GetSection(XmlConfigurationString.XsltSectionPath) as XsltConfigSection;
				return xsltConfigSection != null && xsltConfigSection._ProhibitDefaultResolver;
			}
		}

		// Token: 0x06003112 RID: 12562 RVA: 0x0011C538 File Offset: 0x0011A738
		internal static XmlResolver CreateDefaultResolver()
		{
			if (XsltConfigSection.s_ProhibitDefaultUrlResolver)
			{
				return XmlNullResolver.Singleton;
			}
			return new XmlUrlResolver();
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06003113 RID: 12563 RVA: 0x0011C54C File Offset: 0x0011A74C
		// (set) Token: 0x06003114 RID: 12564 RVA: 0x0011C55E File Offset: 0x0011A75E
		[ConfigurationProperty("limitXPathComplexity", DefaultValue = "true")]
		internal string LimitXPathComplexityString
		{
			get
			{
				return (string)base["limitXPathComplexity"];
			}
			set
			{
				base["limitXPathComplexity"] = value;
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06003115 RID: 12565 RVA: 0x0011C56C File Offset: 0x0011A76C
		private bool _LimitXPathComplexity
		{
			get
			{
				string limitXPathComplexityString = this.LimitXPathComplexityString;
				bool flag = true;
				XmlConvert.TryToBoolean(limitXPathComplexityString, out flag);
				return flag;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x0011C58C File Offset: 0x0011A78C
		internal static bool LimitXPathComplexity
		{
			get
			{
				XsltConfigSection xsltConfigSection = ConfigurationManager.GetSection(XmlConfigurationString.XsltSectionPath) as XsltConfigSection;
				return xsltConfigSection == null || xsltConfigSection._LimitXPathComplexity;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06003117 RID: 12567 RVA: 0x0011C5B4 File Offset: 0x0011A7B4
		// (set) Token: 0x06003118 RID: 12568 RVA: 0x0011C5C6 File Offset: 0x0011A7C6
		[ConfigurationProperty("enableMemberAccessForXslCompiledTransform", DefaultValue = "False")]
		internal string EnableMemberAccessForXslCompiledTransformString
		{
			get
			{
				return (string)base["enableMemberAccessForXslCompiledTransform"];
			}
			set
			{
				base["enableMemberAccessForXslCompiledTransform"] = value;
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06003119 RID: 12569 RVA: 0x0011C5D4 File Offset: 0x0011A7D4
		private bool _EnableMemberAccessForXslCompiledTransform
		{
			get
			{
				string enableMemberAccessForXslCompiledTransformString = this.EnableMemberAccessForXslCompiledTransformString;
				bool flag = false;
				XmlConvert.TryToBoolean(enableMemberAccessForXslCompiledTransformString, out flag);
				return flag;
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x0011C5F4 File Offset: 0x0011A7F4
		internal static bool EnableMemberAccessForXslCompiledTransform
		{
			get
			{
				XsltConfigSection xsltConfigSection = ConfigurationManager.GetSection(XmlConfigurationString.XsltSectionPath) as XsltConfigSection;
				return xsltConfigSection != null && xsltConfigSection._EnableMemberAccessForXslCompiledTransform;
			}
		}
	}
}
