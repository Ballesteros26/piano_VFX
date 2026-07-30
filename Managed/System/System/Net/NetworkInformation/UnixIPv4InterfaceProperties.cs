using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000638 RID: 1592
	internal abstract class UnixIPv4InterfaceProperties : IPv4InterfaceProperties
	{
		// Token: 0x060032CA RID: 13002 RVA: 0x000BFF28 File Offset: 0x000BE128
		public UnixIPv4InterfaceProperties(UnixNetworkInterface iface)
		{
			this.iface = iface;
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x060032CB RID: 13003 RVA: 0x000BFF37 File Offset: 0x000BE137
		public override int Index
		{
			get
			{
				return this.iface.NameIndex;
			}
		}

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x00004240 File Offset: 0x00002440
		public override bool IsAutomaticPrivateAddressingActive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B7C RID: 2940
		// (get) Token: 0x060032CD RID: 13005 RVA: 0x00004240 File Offset: 0x00002440
		public override bool IsAutomaticPrivateAddressingEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B7D RID: 2941
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x00004240 File Offset: 0x00002440
		public override bool IsDhcpEnabled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B7E RID: 2942
		// (get) Token: 0x060032CF RID: 13007 RVA: 0x00004240 File Offset: 0x00002440
		public override bool UsesWins
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04002897 RID: 10391
		protected UnixNetworkInterface iface;
	}
}
