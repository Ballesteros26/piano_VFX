using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace System.Net.NetworkInformation
{
	// Token: 0x0200062B RID: 1579
	internal class Win32IPGlobalProperties : IPGlobalProperties
	{
		// Token: 0x0600324D RID: 12877 RVA: 0x000BED98 File Offset: 0x000BCF98
		private unsafe void FillTcpTable(out List<Win32IPGlobalProperties.Win32_MIB_TCPROW> tab4, out List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW> tab6)
		{
			tab4 = new List<Win32IPGlobalProperties.Win32_MIB_TCPROW>();
			int num = 0;
			Win32IPGlobalProperties.GetTcpTable(null, ref num, true);
			byte[] array = new byte[num];
			Win32IPGlobalProperties.GetTcpTable(array, ref num, true);
			int num2 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_TCPROW));
			fixed (byte[] array2 = array)
			{
				byte* ptr;
				if (array == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				int num3 = Marshal.ReadInt32((IntPtr)((void*)ptr));
				for (int i = 0; i < num3; i++)
				{
					Win32IPGlobalProperties.Win32_MIB_TCPROW win32_MIB_TCPROW = new Win32IPGlobalProperties.Win32_MIB_TCPROW();
					Marshal.PtrToStructure<Win32IPGlobalProperties.Win32_MIB_TCPROW>((IntPtr)((void*)(ptr + i * num2 + 4)), win32_MIB_TCPROW);
					tab4.Add(win32_MIB_TCPROW);
				}
			}
			tab6 = new List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW>();
			if (Environment.OSVersion.Version.Major >= 6)
			{
				int num4 = 0;
				Win32IPGlobalProperties.GetTcp6Table(null, ref num4, true);
				byte[] array3 = new byte[num4];
				Win32IPGlobalProperties.GetTcp6Table(array3, ref num4, true);
				int num5 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_TCP6ROW));
				fixed (byte[] array2 = array3)
				{
					byte* ptr2;
					if (array3 == null || array2.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array2[0];
					}
					int num6 = Marshal.ReadInt32((IntPtr)((void*)ptr2));
					for (int j = 0; j < num6; j++)
					{
						Win32IPGlobalProperties.Win32_MIB_TCP6ROW win32_MIB_TCP6ROW = new Win32IPGlobalProperties.Win32_MIB_TCP6ROW();
						Marshal.PtrToStructure<Win32IPGlobalProperties.Win32_MIB_TCP6ROW>((IntPtr)((void*)(ptr2 + j * num5 + 4)), win32_MIB_TCP6ROW);
						tab6.Add(win32_MIB_TCP6ROW);
					}
				}
			}
		}

		// Token: 0x0600324E RID: 12878 RVA: 0x000BEEDE File Offset: 0x000BD0DE
		private bool IsListenerState(TcpState state)
		{
			return state - TcpState.Listen <= 1 || state - TcpState.FinWait1 <= 2;
		}

		// Token: 0x0600324F RID: 12879 RVA: 0x000BEEF0 File Offset: 0x000BD0F0
		public override TcpConnectionInformation[] GetActiveTcpConnections()
		{
			List<Win32IPGlobalProperties.Win32_MIB_TCPROW> list = null;
			List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW> list2 = null;
			this.FillTcpTable(out list, out list2);
			int count = list.Count;
			TcpConnectionInformation[] array = new TcpConnectionInformation[count + list2.Count];
			for (int i = 0; i < count; i++)
			{
				array[i] = list[i].TcpInfo;
			}
			for (int j = 0; j < list2.Count; j++)
			{
				array[count + j] = list2[j].TcpInfo;
			}
			return array;
		}

		// Token: 0x06003250 RID: 12880 RVA: 0x000BEF6C File Offset: 0x000BD16C
		public override IPEndPoint[] GetActiveTcpListeners()
		{
			List<Win32IPGlobalProperties.Win32_MIB_TCPROW> list = null;
			List<Win32IPGlobalProperties.Win32_MIB_TCP6ROW> list2 = null;
			this.FillTcpTable(out list, out list2);
			List<IPEndPoint> list3 = new List<IPEndPoint>();
			int i = 0;
			int count = list.Count;
			while (i < count)
			{
				if (this.IsListenerState(list[i].State))
				{
					list3.Add(list[i].LocalEndPoint);
				}
				i++;
			}
			int j = 0;
			int count2 = list2.Count;
			while (j < count2)
			{
				if (this.IsListenerState(list2[j].State))
				{
					list3.Add(list2[j].LocalEndPoint);
				}
				j++;
			}
			return list3.ToArray();
		}

		// Token: 0x06003251 RID: 12881 RVA: 0x000BF010 File Offset: 0x000BD210
		public unsafe override IPEndPoint[] GetActiveUdpListeners()
		{
			List<IPEndPoint> list = new List<IPEndPoint>();
			int num = 0;
			Win32IPGlobalProperties.GetUdpTable(null, ref num, true);
			byte[] array = new byte[num];
			Win32IPGlobalProperties.GetUdpTable(array, ref num, true);
			int num2 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_UDPROW));
			fixed (byte[] array2 = array)
			{
				byte* ptr;
				if (array == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				int num3 = Marshal.ReadInt32((IntPtr)((void*)ptr));
				for (int i = 0; i < num3; i++)
				{
					Win32IPGlobalProperties.Win32_MIB_UDPROW win32_MIB_UDPROW = new Win32IPGlobalProperties.Win32_MIB_UDPROW();
					Marshal.PtrToStructure<Win32IPGlobalProperties.Win32_MIB_UDPROW>((IntPtr)((void*)(ptr + i * num2 + 4)), win32_MIB_UDPROW);
					list.Add(win32_MIB_UDPROW.LocalEndPoint);
				}
			}
			if (Environment.OSVersion.Version.Major >= 6)
			{
				int num4 = 0;
				Win32IPGlobalProperties.GetUdp6Table(null, ref num4, true);
				byte[] array3 = new byte[num4];
				Win32IPGlobalProperties.GetUdp6Table(array3, ref num4, true);
				int num5 = Marshal.SizeOf(typeof(Win32IPGlobalProperties.Win32_MIB_UDP6ROW));
				fixed (byte[] array2 = array3)
				{
					byte* ptr2;
					if (array3 == null || array2.Length == 0)
					{
						ptr2 = null;
					}
					else
					{
						ptr2 = &array2[0];
					}
					int num6 = Marshal.ReadInt32((IntPtr)((void*)ptr2));
					for (int j = 0; j < num6; j++)
					{
						Win32IPGlobalProperties.Win32_MIB_UDP6ROW win32_MIB_UDP6ROW = new Win32IPGlobalProperties.Win32_MIB_UDP6ROW();
						Marshal.PtrToStructure<Win32IPGlobalProperties.Win32_MIB_UDP6ROW>((IntPtr)((void*)(ptr2 + j * num5 + 4)), win32_MIB_UDP6ROW);
						list.Add(win32_MIB_UDP6ROW.LocalEndPoint);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x06003252 RID: 12882 RVA: 0x000BF164 File Offset: 0x000BD364
		public override IcmpV4Statistics GetIcmpV4Statistics()
		{
			if (!Socket.OSSupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIBICMPINFO win32_MIBICMPINFO;
			Win32IPGlobalProperties.GetIcmpStatistics(out win32_MIBICMPINFO, 2);
			return new Win32IcmpV4Statistics(win32_MIBICMPINFO);
		}

		// Token: 0x06003253 RID: 12883 RVA: 0x000BF190 File Offset: 0x000BD390
		public override IcmpV6Statistics GetIcmpV6Statistics()
		{
			if (!Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_ICMP_EX win32_MIB_ICMP_EX;
			Win32IPGlobalProperties.GetIcmpStatisticsEx(out win32_MIB_ICMP_EX, 23);
			return new Win32IcmpV6Statistics(win32_MIB_ICMP_EX);
		}

		// Token: 0x06003254 RID: 12884 RVA: 0x000BF1BC File Offset: 0x000BD3BC
		public override IPGlobalStatistics GetIPv4GlobalStatistics()
		{
			if (!Socket.OSSupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_IPSTATS win32_MIB_IPSTATS;
			Win32IPGlobalProperties.GetIpStatisticsEx(out win32_MIB_IPSTATS, 2);
			return new Win32IPGlobalStatistics(win32_MIB_IPSTATS);
		}

		// Token: 0x06003255 RID: 12885 RVA: 0x000BF1E8 File Offset: 0x000BD3E8
		public override IPGlobalStatistics GetIPv6GlobalStatistics()
		{
			if (!Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_IPSTATS win32_MIB_IPSTATS;
			Win32IPGlobalProperties.GetIpStatisticsEx(out win32_MIB_IPSTATS, 23);
			return new Win32IPGlobalStatistics(win32_MIB_IPSTATS);
		}

		// Token: 0x06003256 RID: 12886 RVA: 0x000BF214 File Offset: 0x000BD414
		public override TcpStatistics GetTcpIPv4Statistics()
		{
			if (!Socket.OSSupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_TCPSTATS win32_MIB_TCPSTATS;
			Win32IPGlobalProperties.GetTcpStatisticsEx(out win32_MIB_TCPSTATS, 2);
			return new Win32TcpStatistics(win32_MIB_TCPSTATS);
		}

		// Token: 0x06003257 RID: 12887 RVA: 0x000BF240 File Offset: 0x000BD440
		public override TcpStatistics GetTcpIPv6Statistics()
		{
			if (!Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_TCPSTATS win32_MIB_TCPSTATS;
			Win32IPGlobalProperties.GetTcpStatisticsEx(out win32_MIB_TCPSTATS, 23);
			return new Win32TcpStatistics(win32_MIB_TCPSTATS);
		}

		// Token: 0x06003258 RID: 12888 RVA: 0x000BF26C File Offset: 0x000BD46C
		public override UdpStatistics GetUdpIPv4Statistics()
		{
			if (!Socket.OSSupportsIPv4)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_UDPSTATS win32_MIB_UDPSTATS;
			Win32IPGlobalProperties.GetUdpStatisticsEx(out win32_MIB_UDPSTATS, 2);
			return new Win32UdpStatistics(win32_MIB_UDPSTATS);
		}

		// Token: 0x06003259 RID: 12889 RVA: 0x000BF298 File Offset: 0x000BD498
		public override UdpStatistics GetUdpIPv6Statistics()
		{
			if (!Socket.OSSupportsIPv6)
			{
				throw new NetworkInformationException();
			}
			Win32_MIB_UDPSTATS win32_MIB_UDPSTATS;
			Win32IPGlobalProperties.GetUdpStatisticsEx(out win32_MIB_UDPSTATS, 23);
			return new Win32UdpStatistics(win32_MIB_UDPSTATS);
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600325A RID: 12890 RVA: 0x000BF2C2 File Offset: 0x000BD4C2
		public override string DhcpScopeName
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.ScopeId;
			}
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000BF2CE File Offset: 0x000BD4CE
		public override string DomainName
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.DomainName;
			}
		}

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x0600325C RID: 12892 RVA: 0x000BF2DA File Offset: 0x000BD4DA
		public override string HostName
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.HostName;
			}
		}

		// Token: 0x17000B2F RID: 2863
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000BF2E6 File Offset: 0x000BD4E6
		public override bool IsWinsProxy
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.EnableProxy > 0U;
			}
		}

		// Token: 0x17000B30 RID: 2864
		// (get) Token: 0x0600325E RID: 12894 RVA: 0x000BF2F5 File Offset: 0x000BD4F5
		public override NetBiosNodeType NodeType
		{
			get
			{
				return Win32NetworkInterface.FixedInfo.NodeType;
			}
		}

		// Token: 0x0600325F RID: 12895
		[DllImport("iphlpapi.dll")]
		private static extern int GetTcpTable(byte[] pTcpTable, ref int pdwSize, bool bOrder);

		// Token: 0x06003260 RID: 12896
		[DllImport("iphlpapi.dll")]
		private static extern int GetTcp6Table(byte[] TcpTable, ref int SizePointer, bool Order);

		// Token: 0x06003261 RID: 12897
		[DllImport("iphlpapi.dll")]
		private static extern int GetUdpTable(byte[] pUdpTable, ref int pdwSize, bool bOrder);

		// Token: 0x06003262 RID: 12898
		[DllImport("iphlpapi.dll")]
		private static extern int GetUdp6Table(byte[] Udp6Table, ref int SizePointer, bool Order);

		// Token: 0x06003263 RID: 12899
		[DllImport("iphlpapi.dll")]
		private static extern int GetTcpStatisticsEx(out Win32_MIB_TCPSTATS pStats, int dwFamily);

		// Token: 0x06003264 RID: 12900
		[DllImport("iphlpapi.dll")]
		private static extern int GetUdpStatisticsEx(out Win32_MIB_UDPSTATS pStats, int dwFamily);

		// Token: 0x06003265 RID: 12901
		[DllImport("iphlpapi.dll")]
		private static extern int GetIcmpStatistics(out Win32_MIBICMPINFO pStats, int dwFamily);

		// Token: 0x06003266 RID: 12902
		[DllImport("iphlpapi.dll")]
		private static extern int GetIcmpStatisticsEx(out Win32_MIB_ICMP_EX pStats, int dwFamily);

		// Token: 0x06003267 RID: 12903
		[DllImport("iphlpapi.dll")]
		private static extern int GetIpStatisticsEx(out Win32_MIB_IPSTATS pStats, int dwFamily);

		// Token: 0x06003268 RID: 12904
		[DllImport("Ws2_32.dll")]
		private static extern ushort ntohs(ushort netshort);

		// Token: 0x0400285F RID: 10335
		public const int AF_INET = 2;

		// Token: 0x04002860 RID: 10336
		public const int AF_INET6 = 23;

		// Token: 0x0200062C RID: 1580
		[StructLayout(LayoutKind.Explicit)]
		private struct Win32_IN6_ADDR
		{
			// Token: 0x04002861 RID: 10337
			[FieldOffset(0)]
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
			public byte[] Bytes;
		}

		// Token: 0x0200062D RID: 1581
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_TCPROW
		{
			// Token: 0x17000B31 RID: 2865
			// (get) Token: 0x0600326A RID: 12906 RVA: 0x000BF301 File Offset: 0x000BD501
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint((long)((ulong)this.LocalAddr), (int)Win32IPGlobalProperties.ntohs((ushort)this.LocalPort));
				}
			}

			// Token: 0x17000B32 RID: 2866
			// (get) Token: 0x0600326B RID: 12907 RVA: 0x000BF31B File Offset: 0x000BD51B
			public IPEndPoint RemoteEndPoint
			{
				get
				{
					return new IPEndPoint((long)((ulong)this.RemoteAddr), (int)Win32IPGlobalProperties.ntohs((ushort)this.RemotePort));
				}
			}

			// Token: 0x17000B33 RID: 2867
			// (get) Token: 0x0600326C RID: 12908 RVA: 0x000BF335 File Offset: 0x000BD535
			public TcpConnectionInformation TcpInfo
			{
				get
				{
					return new SystemTcpConnectionInformation(this.LocalEndPoint, this.RemoteEndPoint, this.State);
				}
			}

			// Token: 0x04002862 RID: 10338
			public TcpState State;

			// Token: 0x04002863 RID: 10339
			public uint LocalAddr;

			// Token: 0x04002864 RID: 10340
			public uint LocalPort;

			// Token: 0x04002865 RID: 10341
			public uint RemoteAddr;

			// Token: 0x04002866 RID: 10342
			public uint RemotePort;
		}

		// Token: 0x0200062E RID: 1582
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_TCP6ROW
		{
			// Token: 0x17000B34 RID: 2868
			// (get) Token: 0x0600326E RID: 12910 RVA: 0x000BF34E File Offset: 0x000BD54E
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint(new IPAddress(this.LocalAddr.Bytes, (long)((ulong)this.LocalScopeId)), (int)Win32IPGlobalProperties.ntohs((ushort)this.LocalPort));
				}
			}

			// Token: 0x17000B35 RID: 2869
			// (get) Token: 0x0600326F RID: 12911 RVA: 0x000BF378 File Offset: 0x000BD578
			public IPEndPoint RemoteEndPoint
			{
				get
				{
					return new IPEndPoint(new IPAddress(this.RemoteAddr.Bytes, (long)((ulong)this.RemoteScopeId)), (int)Win32IPGlobalProperties.ntohs((ushort)this.RemotePort));
				}
			}

			// Token: 0x17000B36 RID: 2870
			// (get) Token: 0x06003270 RID: 12912 RVA: 0x000BF3A2 File Offset: 0x000BD5A2
			public TcpConnectionInformation TcpInfo
			{
				get
				{
					return new SystemTcpConnectionInformation(this.LocalEndPoint, this.RemoteEndPoint, this.State);
				}
			}

			// Token: 0x04002867 RID: 10343
			public TcpState State;

			// Token: 0x04002868 RID: 10344
			public Win32IPGlobalProperties.Win32_IN6_ADDR LocalAddr;

			// Token: 0x04002869 RID: 10345
			public uint LocalScopeId;

			// Token: 0x0400286A RID: 10346
			public uint LocalPort;

			// Token: 0x0400286B RID: 10347
			public Win32IPGlobalProperties.Win32_IN6_ADDR RemoteAddr;

			// Token: 0x0400286C RID: 10348
			public uint RemoteScopeId;

			// Token: 0x0400286D RID: 10349
			public uint RemotePort;
		}

		// Token: 0x0200062F RID: 1583
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_UDPROW
		{
			// Token: 0x17000B37 RID: 2871
			// (get) Token: 0x06003272 RID: 12914 RVA: 0x000BF3BB File Offset: 0x000BD5BB
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint((long)((ulong)this.LocalAddr), (int)Win32IPGlobalProperties.ntohs((ushort)this.LocalPort));
				}
			}

			// Token: 0x0400286E RID: 10350
			public uint LocalAddr;

			// Token: 0x0400286F RID: 10351
			public uint LocalPort;
		}

		// Token: 0x02000630 RID: 1584
		[StructLayout(LayoutKind.Sequential)]
		private class Win32_MIB_UDP6ROW
		{
			// Token: 0x17000B38 RID: 2872
			// (get) Token: 0x06003274 RID: 12916 RVA: 0x000BF3D5 File Offset: 0x000BD5D5
			public IPEndPoint LocalEndPoint
			{
				get
				{
					return new IPEndPoint(new IPAddress(this.LocalAddr.Bytes, (long)((ulong)this.LocalScopeId)), (int)Win32IPGlobalProperties.ntohs((ushort)this.LocalPort));
				}
			}

			// Token: 0x04002870 RID: 10352
			public Win32IPGlobalProperties.Win32_IN6_ADDR LocalAddr;

			// Token: 0x04002871 RID: 10353
			public uint LocalScopeId;

			// Token: 0x04002872 RID: 10354
			public uint LocalPort;
		}
	}
}
