using System;

namespace System.Net
{
	/// <summary>Provides a container class for Internet host address information.</summary>
	// Token: 0x02000433 RID: 1075
	public class IPHostEntry
	{
		/// <summary>Gets or sets the DNS name of the host.</summary>
		/// <returns>A string that contains the primary host name for the server.</returns>
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x0007EBAF File Offset: 0x0007CDAF
		// (set) Token: 0x0600207E RID: 8318 RVA: 0x0007EBB7 File Offset: 0x0007CDB7
		public string HostName
		{
			get
			{
				return this.hostName;
			}
			set
			{
				this.hostName = value;
			}
		}

		/// <summary>Gets or sets a list of aliases that are associated with a host.</summary>
		/// <returns>An array of strings that contain DNS names that resolve to the IP addresses in the <see cref="P:System.Net.IPHostEntry.AddressList" /> property.</returns>
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x0007EBC0 File Offset: 0x0007CDC0
		// (set) Token: 0x06002080 RID: 8320 RVA: 0x0007EBC8 File Offset: 0x0007CDC8
		public string[] Aliases
		{
			get
			{
				return this.aliases;
			}
			set
			{
				this.aliases = value;
			}
		}

		/// <summary>Gets or sets a list of IP addresses that are associated with a host.</summary>
		/// <returns>An array of type <see cref="T:System.Net.IPAddress" /> that contains IP addresses that resolve to the host names that are contained in the <see cref="P:System.Net.IPHostEntry.Aliases" /> property.</returns>
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x0007EBD1 File Offset: 0x0007CDD1
		// (set) Token: 0x06002082 RID: 8322 RVA: 0x0007EBD9 File Offset: 0x0007CDD9
		public IPAddress[] AddressList
		{
			get
			{
				return this.addressList;
			}
			set
			{
				this.addressList = value;
			}
		}

		// Token: 0x04001CAD RID: 7341
		private string hostName;

		// Token: 0x04001CAE RID: 7342
		private string[] aliases;

		// Token: 0x04001CAF RID: 7343
		private IPAddress[] addressList;

		// Token: 0x04001CB0 RID: 7344
		internal bool isTrustedHost = true;
	}
}
