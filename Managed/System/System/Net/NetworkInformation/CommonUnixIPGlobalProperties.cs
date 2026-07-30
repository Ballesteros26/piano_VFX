using System;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000627 RID: 1575
	internal abstract class CommonUnixIPGlobalProperties : IPGlobalProperties
	{
		// Token: 0x06003224 RID: 12836
		[DllImport("libc")]
		private static extern int gethostname([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] name, int len);

		// Token: 0x06003225 RID: 12837
		[DllImport("libc")]
		private static extern int getdomainname([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] name, int len);

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x000BE716 File Offset: 0x000BC916
		public override string DhcpScopeName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06003227 RID: 12839 RVA: 0x000BE720 File Offset: 0x000BC920
		public override string DomainName
		{
			get
			{
				byte[] array = new byte[256];
				try
				{
					if (CommonUnixIPGlobalProperties.getdomainname(array, 256) != 0)
					{
						throw new NetworkInformationException();
					}
				}
				catch (EntryPointNotFoundException)
				{
					return string.Empty;
				}
				int num = Array.IndexOf<byte>(array, 0);
				return Encoding.ASCII.GetString(array, 0, (num < 0) ? 256 : num);
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06003228 RID: 12840 RVA: 0x000BE788 File Offset: 0x000BC988
		public override string HostName
		{
			get
			{
				byte[] array = new byte[256];
				if (CommonUnixIPGlobalProperties.gethostname(array, 256) != 0)
				{
					throw new NetworkInformationException();
				}
				int num = Array.IndexOf<byte>(array, 0);
				return Encoding.ASCII.GetString(array, 0, (num < 0) ? 256 : num);
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06003229 RID: 12841 RVA: 0x00004240 File Offset: 0x00002440
		public override bool IsWinsProxy
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x00004240 File Offset: 0x00002440
		public override NetBiosNodeType NodeType
		{
			get
			{
				return NetBiosNodeType.Unknown;
			}
		}
	}
}
