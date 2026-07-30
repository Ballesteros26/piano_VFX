using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000634 RID: 1588
	internal abstract class UnixIPInterfaceProperties : IPInterfaceProperties
	{
		// Token: 0x060032A5 RID: 12965 RVA: 0x000BF69E File Offset: 0x000BD89E
		public UnixIPInterfaceProperties(UnixNetworkInterface iface, List<IPAddress> addresses)
		{
			this.iface = iface;
			this.addresses = addresses;
		}

		// Token: 0x060032A6 RID: 12966 RVA: 0x00004239 File Offset: 0x00002439
		public override IPv6InterfaceProperties GetIPv6Properties()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060032A7 RID: 12967 RVA: 0x000BF6B4 File Offset: 0x000BD8B4
		private void ParseResolvConf()
		{
			try
			{
				DateTime lastWriteTime = File.GetLastWriteTime("/etc/resolv.conf");
				if (!(lastWriteTime <= this.last_parse))
				{
					this.last_parse = lastWriteTime;
					this.dns_suffix = "";
					this.dns_servers = new IPAddressCollection();
					using (StreamReader streamReader = new StreamReader("/etc/resolv.conf"))
					{
						string text;
						while ((text = streamReader.ReadLine()) != null)
						{
							text = text.Trim();
							if (text.Length != 0 && text[0] != '#')
							{
								Match match = UnixIPInterfaceProperties.ns.Match(text);
								if (match.Success)
								{
									try
									{
										string text2 = match.Groups["address"].Value;
										text2 = text2.Trim();
										this.dns_servers.InternalAdd(IPAddress.Parse(text2));
										continue;
									}
									catch
									{
										continue;
									}
								}
								match = UnixIPInterfaceProperties.search.Match(text);
								if (match.Success)
								{
									string text2 = match.Groups["domain"].Value;
									string[] array = text2.Split(new char[] { ',' });
									this.dns_suffix = array[0].Trim();
								}
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x17000B65 RID: 2917
		// (get) Token: 0x060032A8 RID: 12968 RVA: 0x000BF830 File Offset: 0x000BDA30
		public override IPAddressInformationCollection AnycastAddresses
		{
			get
			{
				IPAddressInformationCollection ipaddressInformationCollection = new IPAddressInformationCollection();
				foreach (IPAddress ipaddress in this.addresses)
				{
					ipaddressInformationCollection.InternalAdd(new SystemIPAddressInformation(ipaddress, false, false));
				}
				return ipaddressInformationCollection;
			}
		}

		// Token: 0x17000B66 RID: 2918
		// (get) Token: 0x060032A9 RID: 12969 RVA: 0x000BF894 File Offset: 0x000BDA94
		[MonoTODO("Always returns an empty collection.")]
		public override IPAddressCollection DhcpServerAddresses
		{
			get
			{
				return new IPAddressCollection();
			}
		}

		// Token: 0x17000B67 RID: 2919
		// (get) Token: 0x060032AA RID: 12970 RVA: 0x000BF89B File Offset: 0x000BDA9B
		public override IPAddressCollection DnsAddresses
		{
			get
			{
				this.ParseResolvConf();
				return this.dns_servers;
			}
		}

		// Token: 0x17000B68 RID: 2920
		// (get) Token: 0x060032AB RID: 12971 RVA: 0x000BF8A9 File Offset: 0x000BDAA9
		public override string DnsSuffix
		{
			get
			{
				this.ParseResolvConf();
				return this.dns_suffix;
			}
		}

		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x060032AC RID: 12972 RVA: 0x000027E2 File Offset: 0x000009E2
		[MonoTODO("Always returns true")]
		public override bool IsDnsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B6A RID: 2922
		// (get) Token: 0x060032AD RID: 12973 RVA: 0x00004240 File Offset: 0x00002440
		[MonoTODO("Always returns false")]
		public override bool IsDynamicDnsEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x060032AE RID: 12974 RVA: 0x000BF8B8 File Offset: 0x000BDAB8
		public override MulticastIPAddressInformationCollection MulticastAddresses
		{
			get
			{
				MulticastIPAddressInformationCollection multicastIPAddressInformationCollection = new MulticastIPAddressInformationCollection();
				foreach (IPAddress ipaddress in this.addresses)
				{
					byte[] addressBytes = ipaddress.GetAddressBytes();
					if (addressBytes[0] >= 224 && addressBytes[0] <= 239)
					{
						multicastIPAddressInformationCollection.InternalAdd(new SystemMulticastIPAddressInformation(new SystemIPAddressInformation(ipaddress, true, false)));
					}
				}
				return multicastIPAddressInformationCollection;
			}
		}

		// Token: 0x17000B6C RID: 2924
		// (get) Token: 0x060032AF RID: 12975 RVA: 0x000BF93C File Offset: 0x000BDB3C
		public override UnicastIPAddressInformationCollection UnicastAddresses
		{
			get
			{
				UnicastIPAddressInformationCollection unicastIPAddressInformationCollection = new UnicastIPAddressInformationCollection();
				foreach (IPAddress ipaddress in this.addresses)
				{
					AddressFamily addressFamily = ipaddress.AddressFamily;
					if (addressFamily != AddressFamily.InterNetwork)
					{
						if (addressFamily == AddressFamily.InterNetworkV6)
						{
							if (!ipaddress.IsIPv6Multicast)
							{
								unicastIPAddressInformationCollection.InternalAdd(new LinuxUnicastIPAddressInformation(ipaddress));
							}
						}
					}
					else
					{
						byte b = ipaddress.GetAddressBytes()[0];
						if (b < 224 || b > 239)
						{
							unicastIPAddressInformationCollection.InternalAdd(new LinuxUnicastIPAddressInformation(ipaddress));
						}
					}
				}
				return unicastIPAddressInformationCollection;
			}
		}

		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060032B0 RID: 12976 RVA: 0x000BF894 File Offset: 0x000BDA94
		[MonoTODO("Always returns an empty collection.")]
		public override IPAddressCollection WinsServersAddresses
		{
			get
			{
				return new IPAddressCollection();
			}
		}

		// Token: 0x0400288C RID: 10380
		protected IPv4InterfaceProperties ipv4iface_properties;

		// Token: 0x0400288D RID: 10381
		protected UnixNetworkInterface iface;

		// Token: 0x0400288E RID: 10382
		private List<IPAddress> addresses;

		// Token: 0x0400288F RID: 10383
		private IPAddressCollection dns_servers;

		// Token: 0x04002890 RID: 10384
		private static Regex ns = new Regex("\\s*nameserver\\s+(?<address>.*)");

		// Token: 0x04002891 RID: 10385
		private static Regex search = new Regex("\\s*search\\s+(?<domain>.*)");

		// Token: 0x04002892 RID: 10386
		private string dns_suffix;

		// Token: 0x04002893 RID: 10387
		private DateTime last_parse;
	}
}
