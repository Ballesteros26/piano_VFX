using System;
using System.Configuration;
using System.Net.Mail;

namespace System.Net.Configuration
{
	/// <summary>Represents the SMTP section in the System.Net configuration file.</summary>
	// Token: 0x020006B0 RID: 1712
	public sealed class SmtpSection : ConfigurationSection
	{
		/// <summary>Gets or sets the Simple Mail Transport Protocol (SMTP) delivery method. The default delivery method is <see cref="F:System.Net.Mail.SmtpDeliveryMethod.Network" />.</summary>
		/// <returns>A string that represents the SMTP delivery method.</returns>
		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x060035A8 RID: 13736 RVA: 0x000C5829 File Offset: 0x000C3A29
		// (set) Token: 0x060035A9 RID: 13737 RVA: 0x000C583B File Offset: 0x000C3A3B
		[ConfigurationProperty("deliveryMethod", DefaultValue = "Network")]
		public SmtpDeliveryMethod DeliveryMethod
		{
			get
			{
				return (SmtpDeliveryMethod)base["deliveryMethod"];
			}
			set
			{
				base["deliveryMethod"] = value;
			}
		}

		/// <summary>Gets or sets the delivery format to use for sending outgoing e-mail using the Simple Mail Transport Protocol (SMTP).</summary>
		/// <returns>Returns <see cref="T:System.Net.Mail.SmtpDeliveryFormat" />.The delivery format to use for sending outgoing e-mail using SMTP.</returns>
		// Token: 0x17000CEE RID: 3310
		// (get) Token: 0x060035AA RID: 13738 RVA: 0x000C584E File Offset: 0x000C3A4E
		// (set) Token: 0x060035AB RID: 13739 RVA: 0x000C5860 File Offset: 0x000C3A60
		[ConfigurationProperty("deliveryFormat", DefaultValue = SmtpDeliveryFormat.SevenBit)]
		public SmtpDeliveryFormat DeliveryFormat
		{
			get
			{
				return (SmtpDeliveryFormat)base["deliveryFormat"];
			}
			set
			{
				base["deliveryFormat"] = value;
			}
		}

		/// <summary>Gets or sets the default value that indicates who the email message is from.</summary>
		/// <returns>A string that represents the default value indicating who a mail message is from.</returns>
		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x060035AC RID: 13740 RVA: 0x000C5873 File Offset: 0x000C3A73
		// (set) Token: 0x060035AD RID: 13741 RVA: 0x000C5885 File Offset: 0x000C3A85
		[ConfigurationProperty("from")]
		public string From
		{
			get
			{
				return (string)base["from"];
			}
			set
			{
				base["from"] = value;
			}
		}

		/// <summary>Gets the configuration element that controls the network settings used by the Simple Mail Transport Protocol (SMTP). file.<see cref="T:System.Net.Configuration.SmtpNetworkElement" />.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpNetworkElement" /> object.The configuration element that controls the network settings used by SMTP.</returns>
		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x060035AE RID: 13742 RVA: 0x000C5893 File Offset: 0x000C3A93
		[ConfigurationProperty("network")]
		public SmtpNetworkElement Network
		{
			get
			{
				return (SmtpNetworkElement)base["network"];
			}
		}

		/// <summary>Gets the pickup directory that will be used by the SMPT client.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpSpecifiedPickupDirectoryElement" /> object that specifies the pickup directory folder.</returns>
		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x060035AF RID: 13743 RVA: 0x000C58A5 File Offset: 0x000C3AA5
		[ConfigurationProperty("specifiedPickupDirectory")]
		public SmtpSpecifiedPickupDirectoryElement SpecifiedPickupDirectory
		{
			get
			{
				return (SmtpSpecifiedPickupDirectoryElement)base["specifiedPickupDirectory"];
			}
		}

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x060035B0 RID: 13744 RVA: 0x0003C203 File Offset: 0x0003A403
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return base.Properties;
			}
		}
	}
}
