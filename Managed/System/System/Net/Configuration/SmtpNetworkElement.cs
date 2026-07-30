using System;
using System.Configuration;
using Unity;

namespace System.Net.Configuration
{
	/// <summary>Represents the network element in the SMTP configuration file. This class cannot be inherited.</summary>
	// Token: 0x020006AF RID: 1711
	public sealed class SmtpNetworkElement : ConfigurationElement
	{
		/// <summary>Determines whether or not default user credentials are used to access an SMTP server. The default value is false.</summary>
		/// <returns>true indicates that default user credentials will be used to access the SMTP server; otherwise, false.</returns>
		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06003595 RID: 13717 RVA: 0x000C573A File Offset: 0x000C393A
		// (set) Token: 0x06003596 RID: 13718 RVA: 0x000C574C File Offset: 0x000C394C
		[ConfigurationProperty("defaultCredentials", DefaultValue = "False")]
		public bool DefaultCredentials
		{
			get
			{
				return (bool)base["defaultCredentials"];
			}
			set
			{
				base["defaultCredentials"] = value;
			}
		}

		/// <summary>Gets or sets the name of the SMTP server.</summary>
		/// <returns>A string that represents the name of the SMTP server to connect to.</returns>
		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06003597 RID: 13719 RVA: 0x000C575F File Offset: 0x000C395F
		// (set) Token: 0x06003598 RID: 13720 RVA: 0x000C5771 File Offset: 0x000C3971
		[ConfigurationProperty("host")]
		public string Host
		{
			get
			{
				return (string)base["host"];
			}
			set
			{
				base["host"] = value;
			}
		}

		/// <summary>Gets or sets the user password to use to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the password to use to connect to an SMTP mail server.</returns>
		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06003599 RID: 13721 RVA: 0x000C577F File Offset: 0x000C397F
		// (set) Token: 0x0600359A RID: 13722 RVA: 0x000C5791 File Offset: 0x000C3991
		[ConfigurationProperty("password")]
		public string Password
		{
			get
			{
				return (string)base["password"];
			}
			set
			{
				base["password"] = value;
			}
		}

		/// <summary>Gets or sets the port that SMTP clients use to connect to an SMTP mail server. The default value is 25.</summary>
		/// <returns>A string that represents the port to connect to an SMTP mail server.</returns>
		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x0600359B RID: 13723 RVA: 0x000C579F File Offset: 0x000C399F
		// (set) Token: 0x0600359C RID: 13724 RVA: 0x000C57B1 File Offset: 0x000C39B1
		[ConfigurationProperty("port", DefaultValue = "25")]
		public int Port
		{
			get
			{
				return (int)base["port"];
			}
			set
			{
				base["port"] = value;
			}
		}

		/// <summary>Gets or sets the user name to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the user name to connect to an SMTP mail server.</returns>
		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x0600359D RID: 13725 RVA: 0x000C57C4 File Offset: 0x000C39C4
		// (set) Token: 0x0600359E RID: 13726 RVA: 0x000C57D6 File Offset: 0x000C39D6
		[ConfigurationProperty("userName", DefaultValue = null)]
		public string UserName
		{
			get
			{
				return (string)base["userName"];
			}
			set
			{
				base["userName"] = value;
			}
		}

		/// <summary>Gets or sets the Service Provider Name (SPN) to use for authentication when using extended protection to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the SPN to use for authentication when using extended protection to connect to an SMTP mail server.</returns>
		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x0600359F RID: 13727 RVA: 0x000C57E4 File Offset: 0x000C39E4
		// (set) Token: 0x060035A0 RID: 13728 RVA: 0x000C57F6 File Offset: 0x000C39F6
		[ConfigurationProperty("targetName", DefaultValue = null)]
		public string TargetName
		{
			get
			{
				return (string)base["targetName"];
			}
			set
			{
				base["targetName"] = value;
			}
		}

		/// <summary>Gets or sets whether SSL is used to access an SMTP mail server. The default value is false.</summary>
		/// <returns>true indicates that SSL will be used to access the SMTP mail server; otherwise, false.</returns>
		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x060035A1 RID: 13729 RVA: 0x000C5804 File Offset: 0x000C3A04
		// (set) Token: 0x060035A2 RID: 13730 RVA: 0x000C5816 File Offset: 0x000C3A16
		[ConfigurationProperty("enableSsl", DefaultValue = false)]
		public bool EnableSsl
		{
			get
			{
				return (bool)base["enableSsl"];
			}
			set
			{
				base["enableSsl"] = value;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x060035A3 RID: 13731 RVA: 0x0003C203 File Offset: 0x0003A403
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return base.Properties;
			}
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x000027E8 File Offset: 0x000009E8
		protected override void PostDeserialize()
		{
		}

		/// <summary>Gets or sets the client domain name used in the initial SMTP protocol request to connect to an SMTP mail server.</summary>
		/// <returns>A string that represents the client domain name used in the initial SMTP protocol request to connect to an SMTP mail server.</returns>
		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x060035A6 RID: 13734 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		// (set) Token: 0x060035A7 RID: 13735 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		public string ClientDomain
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}
	}
}
