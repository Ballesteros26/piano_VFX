using System;
using Unity;

namespace System.Configuration
{
	/// <summary>Defines the configuration file mapping for an .exe application. This class cannot be inherited.</summary>
	// Token: 0x02000040 RID: 64
	public sealed class ExeConfigurationFileMap : ConfigurationFileMap
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ExeConfigurationFileMap" /> class.</summary>
		// Token: 0x06000233 RID: 563 RVA: 0x00007948 File Offset: 0x00005B48
		public ExeConfigurationFileMap()
		{
			this.exeConfigFilename = "";
			this.localUserConfigFilename = "";
			this.roamingUserConfigFilename = "";
		}

		/// <summary>Gets or sets the name of the configuration file.</summary>
		/// <returns>The configuration file name.</returns>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00007971 File Offset: 0x00005B71
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00007979 File Offset: 0x00005B79
		public string ExeConfigFilename
		{
			get
			{
				return this.exeConfigFilename;
			}
			set
			{
				this.exeConfigFilename = value;
			}
		}

		/// <summary>Gets or sets the name of the configuration file for the local user.</summary>
		/// <returns>The configuration file name.</returns>
		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000236 RID: 566 RVA: 0x00007982 File Offset: 0x00005B82
		// (set) Token: 0x06000237 RID: 567 RVA: 0x0000798A File Offset: 0x00005B8A
		public string LocalUserConfigFilename
		{
			get
			{
				return this.localUserConfigFilename;
			}
			set
			{
				this.localUserConfigFilename = value;
			}
		}

		/// <summary>Gets or sets the name of the configuration file for the roaming user.</summary>
		/// <returns>The configuration file name.</returns>
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00007993 File Offset: 0x00005B93
		// (set) Token: 0x06000239 RID: 569 RVA: 0x0000799B File Offset: 0x00005B9B
		public string RoamingUserConfigFilename
		{
			get
			{
				return this.roamingUserConfigFilename;
			}
			set
			{
				this.roamingUserConfigFilename = value;
			}
		}

		/// <summary>Creates a copy of the existing <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object.</summary>
		/// <returns>An <see cref="T:System.Configuration.ExeConfigurationFileMap" /> object.</returns>
		// Token: 0x0600023A RID: 570 RVA: 0x000079A4 File Offset: 0x00005BA4
		public override object Clone()
		{
			return new ExeConfigurationFileMap
			{
				exeConfigFilename = this.exeConfigFilename,
				localUserConfigFilename = this.localUserConfigFilename,
				roamingUserConfigFilename = this.roamingUserConfigFilename,
				MachineConfigFilename = base.MachineConfigFilename
			};
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ExeConfigurationFileMap" /> class by using the specified machine configuration file name.</summary>
		/// <param name="machineConfigFileName">The name of the machine configuration file that includes the complete physical path (for example, c:\Windows\Microsoft.NET\Framework\v2.0.50727\CONFIG\machine.config).</param>
		// Token: 0x0600023B RID: 571 RVA: 0x00003524 File Offset: 0x00001724
		public ExeConfigurationFileMap(string machineConfigFileName)
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040000E5 RID: 229
		private string exeConfigFilename;

		// Token: 0x040000E6 RID: 230
		private string localUserConfigFilename;

		// Token: 0x040000E7 RID: 231
		private string roamingUserConfigFilename;
	}
}
