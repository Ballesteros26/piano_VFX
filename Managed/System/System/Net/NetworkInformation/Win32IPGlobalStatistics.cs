using System;

namespace System.Net.NetworkInformation
{
	// Token: 0x02000632 RID: 1586
	internal class Win32IPGlobalStatistics : IPGlobalStatistics
	{
		// Token: 0x0600328E RID: 12942 RVA: 0x000BF55D File Offset: 0x000BD75D
		public Win32IPGlobalStatistics(Win32_MIB_IPSTATS info)
		{
			this.info = info;
		}

		// Token: 0x17000B4F RID: 2895
		// (get) Token: 0x0600328F RID: 12943 RVA: 0x000BF56C File Offset: 0x000BD76C
		public override int DefaultTtl
		{
			get
			{
				return this.info.DefaultTTL;
			}
		}

		// Token: 0x17000B50 RID: 2896
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x000BF579 File Offset: 0x000BD779
		public override bool ForwardingEnabled
		{
			get
			{
				return this.info.Forwarding != 0;
			}
		}

		// Token: 0x17000B51 RID: 2897
		// (get) Token: 0x06003291 RID: 12945 RVA: 0x000BF589 File Offset: 0x000BD789
		public override int NumberOfInterfaces
		{
			get
			{
				return this.info.NumIf;
			}
		}

		// Token: 0x17000B52 RID: 2898
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x000BF596 File Offset: 0x000BD796
		public override int NumberOfIPAddresses
		{
			get
			{
				return this.info.NumAddr;
			}
		}

		// Token: 0x17000B53 RID: 2899
		// (get) Token: 0x06003293 RID: 12947 RVA: 0x000BF5A3 File Offset: 0x000BD7A3
		public override int NumberOfRoutes
		{
			get
			{
				return this.info.NumRoutes;
			}
		}

		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06003294 RID: 12948 RVA: 0x000BF5B0 File Offset: 0x000BD7B0
		public override long OutputPacketRequests
		{
			get
			{
				return (long)((ulong)this.info.OutRequests);
			}
		}

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x000BF5BE File Offset: 0x000BD7BE
		public override long OutputPacketRoutingDiscards
		{
			get
			{
				return (long)((ulong)this.info.RoutingDiscards);
			}
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x000BF5CC File Offset: 0x000BD7CC
		public override long OutputPacketsDiscarded
		{
			get
			{
				return (long)((ulong)this.info.OutDiscards);
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06003297 RID: 12951 RVA: 0x000BF5DA File Offset: 0x000BD7DA
		public override long OutputPacketsWithNoRoute
		{
			get
			{
				return (long)((ulong)this.info.OutNoRoutes);
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06003298 RID: 12952 RVA: 0x000BF5E8 File Offset: 0x000BD7E8
		public override long PacketFragmentFailures
		{
			get
			{
				return (long)((ulong)this.info.FragFails);
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x000BF5F6 File Offset: 0x000BD7F6
		public override long PacketReassembliesRequired
		{
			get
			{
				return (long)((ulong)this.info.ReasmReqds);
			}
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x0600329A RID: 12954 RVA: 0x000BF604 File Offset: 0x000BD804
		public override long PacketReassemblyFailures
		{
			get
			{
				return (long)((ulong)this.info.ReasmFails);
			}
		}

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x000BF612 File Offset: 0x000BD812
		public override long PacketReassemblyTimeout
		{
			get
			{
				return (long)((ulong)this.info.ReasmTimeout);
			}
		}

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x0600329C RID: 12956 RVA: 0x000BF620 File Offset: 0x000BD820
		public override long PacketsFragmented
		{
			get
			{
				return (long)((ulong)this.info.FragOks);
			}
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x0600329D RID: 12957 RVA: 0x000BF62E File Offset: 0x000BD82E
		public override long PacketsReassembled
		{
			get
			{
				return (long)((ulong)this.info.ReasmOks);
			}
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x0600329E RID: 12958 RVA: 0x000BF63C File Offset: 0x000BD83C
		public override long ReceivedPackets
		{
			get
			{
				return (long)((ulong)this.info.InReceives);
			}
		}

		// Token: 0x17000B5F RID: 2911
		// (get) Token: 0x0600329F RID: 12959 RVA: 0x000BF64A File Offset: 0x000BD84A
		public override long ReceivedPacketsDelivered
		{
			get
			{
				return (long)((ulong)this.info.InDelivers);
			}
		}

		// Token: 0x17000B60 RID: 2912
		// (get) Token: 0x060032A0 RID: 12960 RVA: 0x000BF658 File Offset: 0x000BD858
		public override long ReceivedPacketsDiscarded
		{
			get
			{
				return (long)((ulong)this.info.InDiscards);
			}
		}

		// Token: 0x17000B61 RID: 2913
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x000BF666 File Offset: 0x000BD866
		public override long ReceivedPacketsForwarded
		{
			get
			{
				return (long)((ulong)this.info.ForwDatagrams);
			}
		}

		// Token: 0x17000B62 RID: 2914
		// (get) Token: 0x060032A2 RID: 12962 RVA: 0x000BF674 File Offset: 0x000BD874
		public override long ReceivedPacketsWithAddressErrors
		{
			get
			{
				return (long)((ulong)this.info.InAddrErrors);
			}
		}

		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060032A3 RID: 12963 RVA: 0x000BF682 File Offset: 0x000BD882
		public override long ReceivedPacketsWithHeadersErrors
		{
			get
			{
				return (long)((ulong)this.info.InHdrErrors);
			}
		}

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x060032A4 RID: 12964 RVA: 0x000BF690 File Offset: 0x000BD890
		public override long ReceivedPacketsWithUnknownProtocol
		{
			get
			{
				return (long)((ulong)this.info.InUnknownProtos);
			}
		}

		// Token: 0x04002874 RID: 10356
		private Win32_MIB_IPSTATS info;
	}
}
