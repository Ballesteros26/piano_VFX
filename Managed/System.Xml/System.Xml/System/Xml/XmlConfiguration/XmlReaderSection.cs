using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Xml.XmlConfiguration
{
	/// <summary>Represents an XML reader section.</summary>
	// Token: 0x020004B8 RID: 1208
	[EditorBrowsable(EditorBrowsableState.Never)]
	public sealed class XmlReaderSection : ConfigurationSection
	{
		/// <summary>Gets or sets the string that represents the prohibit default resolver.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the prohibit default resolver.</returns>
		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06003104 RID: 12548 RVA: 0x0011C411 File Offset: 0x0011A611
		// (set) Token: 0x06003105 RID: 12549 RVA: 0x0011C423 File Offset: 0x0011A623
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

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06003106 RID: 12550 RVA: 0x0011C434 File Offset: 0x0011A634
		private bool _ProhibitDefaultResolver
		{
			get
			{
				bool flag;
				XmlConvert.TryToBoolean(this.ProhibitDefaultResolverString, out flag);
				return flag;
			}
		}

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06003107 RID: 12551 RVA: 0x0011C450 File Offset: 0x0011A650
		internal static bool ProhibitDefaultUrlResolver
		{
			get
			{
				XmlReaderSection xmlReaderSection = ConfigurationManager.GetSection(XmlConfigurationString.XmlReaderSectionPath) as XmlReaderSection;
				return xmlReaderSection != null && xmlReaderSection._ProhibitDefaultResolver;
			}
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x0011C478 File Offset: 0x0011A678
		internal static XmlResolver CreateDefaultResolver()
		{
			if (XmlReaderSection.ProhibitDefaultUrlResolver)
			{
				return null;
			}
			return new XmlUrlResolver();
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06003109 RID: 12553 RVA: 0x0011C488 File Offset: 0x0011A688
		// (set) Token: 0x0600310A RID: 12554 RVA: 0x0011C49A File Offset: 0x0011A69A
		[ConfigurationProperty("CollapseWhiteSpaceIntoEmptyString", DefaultValue = "false")]
		public string CollapseWhiteSpaceIntoEmptyStringString
		{
			get
			{
				return (string)base["CollapseWhiteSpaceIntoEmptyString"];
			}
			set
			{
				base["CollapseWhiteSpaceIntoEmptyString"] = value;
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x0011C4A8 File Offset: 0x0011A6A8
		private bool _CollapseWhiteSpaceIntoEmptyString
		{
			get
			{
				bool flag;
				XmlConvert.TryToBoolean(this.CollapseWhiteSpaceIntoEmptyStringString, out flag);
				return flag;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x0600310C RID: 12556 RVA: 0x0011C4C4 File Offset: 0x0011A6C4
		internal static bool CollapseWhiteSpaceIntoEmptyString
		{
			get
			{
				XmlReaderSection xmlReaderSection = ConfigurationManager.GetSection(XmlConfigurationString.XmlReaderSectionPath) as XmlReaderSection;
				return xmlReaderSection != null && xmlReaderSection._CollapseWhiteSpaceIntoEmptyString;
			}
		}
	}
}
