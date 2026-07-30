using System;
using System.Collections.Specialized;
using System.Globalization;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000631 RID: 1585
	internal class MibIPGlobalStatistics : IPGlobalStatistics
	{
		// Token: 0x06003276 RID: 12918 RVA: 0x000BF3FF File Offset: 0x000BD5FF
		public MibIPGlobalStatistics(StringDictionary dic)
		{
			this.dic = dic;
		}

		// Token: 0x06003277 RID: 12919 RVA: 0x000BF40E File Offset: 0x000BD60E
		private long Get(string name)
		{
			if (this.dic[name] == null)
			{
				return 0L;
			}
			return long.Parse(this.dic[name], NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x06003278 RID: 12920 RVA: 0x000BF437 File Offset: 0x000BD637
		public override int DefaultTtl
		{
			get
			{
				return (int)this.Get("DefaultTTL");
			}
		}

		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x000BF445 File Offset: 0x000BD645
		public override bool ForwardingEnabled
		{
			get
			{
				return this.Get("Forwarding") != 0L;
			}
		}

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x0600327A RID: 12922 RVA: 0x000BF456 File Offset: 0x000BD656
		public override int NumberOfInterfaces
		{
			get
			{
				return (int)this.Get("NumIf");
			}
		}

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x000BF464 File Offset: 0x000BD664
		public override int NumberOfIPAddresses
		{
			get
			{
				return (int)this.Get("NumAddr");
			}
		}

		// Token: 0x17000B3D RID: 2877
		// (get) Token: 0x0600327C RID: 12924 RVA: 0x000BF472 File Offset: 0x000BD672
		public override int NumberOfRoutes
		{
			get
			{
				return (int)this.Get("NumRoutes");
			}
		}

		// Token: 0x17000B3E RID: 2878
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x000BF480 File Offset: 0x000BD680
		public override long OutputPacketRequests
		{
			get
			{
				return this.Get("OutRequests");
			}
		}

		// Token: 0x17000B3F RID: 2879
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x000BF48D File Offset: 0x000BD68D
		public override long OutputPacketRoutingDiscards
		{
			get
			{
				return this.Get("RoutingDiscards");
			}
		}

		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000BF49A File Offset: 0x000BD69A
		public override long OutputPacketsDiscarded
		{
			get
			{
				return this.Get("OutDiscards");
			}
		}

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06003280 RID: 12928 RVA: 0x000BF4A7 File Offset: 0x000BD6A7
		public override long OutputPacketsWithNoRoute
		{
			get
			{
				return this.Get("OutNoRoutes");
			}
		}

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x000BF4B4 File Offset: 0x000BD6B4
		public override long PacketFragmentFailures
		{
			get
			{
				return this.Get("FragFails");
			}
		}

		// Token: 0x17000B43 RID: 2883
		// (get) Token: 0x06003282 RID: 12930 RVA: 0x000BF4C1 File Offset: 0x000BD6C1
		public override long PacketReassembliesRequired
		{
			get
			{
				return this.Get("ReasmReqds");
			}
		}

		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x000BF4CE File Offset: 0x000BD6CE
		public override long PacketReassemblyFailures
		{
			get
			{
				return this.Get("ReasmFails");
			}
		}

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06003284 RID: 12932 RVA: 0x000BF4DB File Offset: 0x000BD6DB
		public override long PacketReassemblyTimeout
		{
			get
			{
				return this.Get("ReasmTimeout");
			}
		}

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06003285 RID: 12933 RVA: 0x000BF4E8 File Offset: 0x000BD6E8
		public override long PacketsFragmented
		{
			get
			{
				return this.Get("FragOks");
			}
		}

		// Token: 0x17000B47 RID: 2887
		// (get) Token: 0x06003286 RID: 12934 RVA: 0x000BF4F5 File Offset: 0x000BD6F5
		public override long PacketsReassembled
		{
			get
			{
				return this.Get("ReasmOks");
			}
		}

		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x06003287 RID: 12935 RVA: 0x000BF502 File Offset: 0x000BD702
		public override long ReceivedPackets
		{
			get
			{
				return this.Get("InReceives");
			}
		}

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06003288 RID: 12936 RVA: 0x000BF50F File Offset: 0x000BD70F
		public override long ReceivedPacketsDelivered
		{
			get
			{
				return this.Get("InDelivers");
			}
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06003289 RID: 12937 RVA: 0x000BF51C File Offset: 0x000BD71C
		public override long ReceivedPacketsDiscarded
		{
			get
			{
				return this.Get("InDiscards");
			}
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x0600328A RID: 12938 RVA: 0x000BF529 File Offset: 0x000BD729
		public override long ReceivedPacketsForwarded
		{
			get
			{
				return this.Get("ForwDatagrams");
			}
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x000BF536 File Offset: 0x000BD736
		public override long ReceivedPacketsWithAddressErrors
		{
			get
			{
				return this.Get("InAddrErrors");
			}
		}

		// Token: 0x17000B4D RID: 2893
		// (get) Token: 0x0600328C RID: 12940 RVA: 0x000BF543 File Offset: 0x000BD743
		public override long ReceivedPacketsWithHeadersErrors
		{
			get
			{
				return this.Get("InHdrErrors");
			}
		}

		// Token: 0x17000B4E RID: 2894
		// (get) Token: 0x0600328D RID: 12941 RVA: 0x000BF550 File Offset: 0x000BD750
		public override long ReceivedPacketsWithUnknownProtocol
		{
			get
			{
				return this.Get("InUnknownProtos");
			}
		}

		// Token: 0x04002873 RID: 10355
		private StringDictionary dic;
	}
}
