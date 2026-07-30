using System;
using System.IO;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000639 RID: 1593
	internal sealed class LinuxIPv4InterfaceProperties : UnixIPv4InterfaceProperties
	{
		// Token: 0x060032D0 RID: 13008 RVA: 0x000BFF44 File Offset: 0x000BE144
		public LinuxIPv4InterfaceProperties(LinuxNetworkInterface iface)
			: base(iface)
		{
		}

		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x060032D1 RID: 13009 RVA: 0x000BFF50 File Offset: 0x000BE150
		public override bool IsForwardingEnabled
		{
			get
			{
				string text = "/proc/sys/net/ipv4/conf/" + this.iface.Name + "/forwarding";
				return File.Exists(text) && LinuxNetworkInterface.ReadLine(text) != "0";
			}
		}

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x060032D2 RID: 13010 RVA: 0x000BFF94 File Offset: 0x000BE194
		public override int Mtu
		{
			get
			{
				string text = (this.iface as LinuxNetworkInterface).IfacePath + "mtu";
				int num = 0;
				if (File.Exists(text))
				{
					string text2 = LinuxNetworkInterface.ReadLine(text);
					try
					{
						num = int.Parse(text2);
					}
					catch
					{
					}
				}
				return num;
			}
		}
	}
}
