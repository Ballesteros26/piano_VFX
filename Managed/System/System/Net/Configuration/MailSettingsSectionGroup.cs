using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.MailSettingsSectionGroup" /> class.</summary>
	// Token: 0x020006A2 RID: 1698
	public sealed class MailSettingsSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Gets the SMTP settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpSection" /> object that contains configuration information for the local computer.</returns>
		// Token: 0x17000CBA RID: 3258
		// (get) Token: 0x06003542 RID: 13634 RVA: 0x000C4B06 File Offset: 0x000C2D06
		public SmtpSection Smtp
		{
			get
			{
				return (SmtpSection)base.Sections["smtp"];
			}
		}
	}
}
