using System;
using System.Globalization;
using System.IO;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000664 RID: 1636
	internal sealed class LinuxNetworkInterface : UnixNetworkInterface
	{
		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x060033FD RID: 13309 RVA: 0x000C20BD File Offset: 0x000C02BD
		internal string IfacePath
		{
			get
			{
				return this.iface_path;
			}
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x000C20C8 File Offset: 0x000C02C8
		internal LinuxNetworkInterface(string name)
			: base(name)
		{
			this.iface_path = "/sys/class/net/" + name + "/";
			this.iface_operstate_path = this.iface_path + "operstate";
			this.iface_flags_path = this.iface_path + "flags";
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x000C211E File Offset: 0x000C031E
		public override IPInterfaceProperties GetIPProperties()
		{
			if (this.ipproperties == null)
			{
				this.ipproperties = new LinuxIPInterfaceProperties(this, this.addresses);
			}
			return this.ipproperties;
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x000C2140 File Offset: 0x000C0340
		public override IPv4InterfaceStatistics GetIPv4Statistics()
		{
			if (this.ipv4stats == null)
			{
				this.ipv4stats = new LinuxIPv4InterfaceStatistics(this);
			}
			return this.ipv4stats;
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06003401 RID: 13313 RVA: 0x000C215C File Offset: 0x000C035C
		public override OperationalStatus OperationalStatus
		{
			get
			{
				if (!Directory.Exists(this.iface_path))
				{
					return OperationalStatus.Unknown;
				}
				try
				{
					string text = LinuxNetworkInterface.ReadLine(this.iface_operstate_path);
					uint num = global::<PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 2313571237U)
					{
						if (num != 1035581717U)
						{
							if (num != 1128467232U)
							{
								if (num == 2313571237U)
								{
									if (text == "notpresent")
									{
										return OperationalStatus.NotPresent;
									}
								}
							}
							else if (text == "up")
							{
								return OperationalStatus.Up;
							}
						}
						else if (text == "down")
						{
							return OperationalStatus.Down;
						}
					}
					else if (num <= 2966218339U)
					{
						if (num != 2608177081U)
						{
							if (num == 2966218339U)
							{
								if (text == "lowerlayerdown")
								{
									return OperationalStatus.LowerLayerDown;
								}
							}
						}
						else if (text == "unknown")
						{
							return OperationalStatus.Unknown;
						}
					}
					else if (num != 3340047486U)
					{
						if (num == 3948890523U)
						{
							if (text == "testing")
							{
								return OperationalStatus.Testing;
							}
						}
					}
					else if (text == "dormant")
					{
						return OperationalStatus.Dormant;
					}
				}
				catch
				{
				}
				return OperationalStatus.Unknown;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06003402 RID: 13314 RVA: 0x000C2284 File Offset: 0x000C0484
		public override bool SupportsMulticast
		{
			get
			{
				if (!Directory.Exists(this.iface_path))
				{
					return false;
				}
				bool flag;
				try
				{
					string text = LinuxNetworkInterface.ReadLine(this.iface_flags_path);
					if (text.Length > 2 && text[0] == '0' && text[1] == 'x')
					{
						text = text.Substring(2);
					}
					flag = (ulong.Parse(text, NumberStyles.HexNumber) & 4096UL) == 4096UL;
				}
				catch
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x000C2308 File Offset: 0x000C0508
		internal static string ReadLine(string path)
		{
			string text;
			using (FileStream fileStream = File.OpenRead(path))
			{
				using (StreamReader streamReader = new StreamReader(fileStream))
				{
					text = streamReader.ReadLine();
				}
			}
			return text;
		}

		// Token: 0x0400294A RID: 10570
		private string iface_path;

		// Token: 0x0400294B RID: 10571
		private string iface_operstate_path;

		// Token: 0x0400294C RID: 10572
		private string iface_flags_path;
	}
}
