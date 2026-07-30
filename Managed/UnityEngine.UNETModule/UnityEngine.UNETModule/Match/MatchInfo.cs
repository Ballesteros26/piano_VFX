using System;
using UnityEngine.Networking.Types;

namespace UnityEngine.Networking.Match
{
	// Token: 0x02000019 RID: 25
	[Obsolete("The matchmaker and relay feature will be removed in the future, minimal support will continue until this can be safely done.")]
	public class MatchInfo
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00004599 File Offset: 0x00002799
		// (set) Token: 0x06000139 RID: 313 RVA: 0x000045A1 File Offset: 0x000027A1
		public string address { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600013A RID: 314 RVA: 0x000045AA File Offset: 0x000027AA
		// (set) Token: 0x0600013B RID: 315 RVA: 0x000045B2 File Offset: 0x000027B2
		public int port { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600013C RID: 316 RVA: 0x000045BB File Offset: 0x000027BB
		// (set) Token: 0x0600013D RID: 317 RVA: 0x000045C3 File Offset: 0x000027C3
		public int domain { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600013E RID: 318 RVA: 0x000045CC File Offset: 0x000027CC
		// (set) Token: 0x0600013F RID: 319 RVA: 0x000045D4 File Offset: 0x000027D4
		public NetworkID networkId { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000140 RID: 320 RVA: 0x000045DD File Offset: 0x000027DD
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000045E5 File Offset: 0x000027E5
		public NetworkAccessToken accessToken { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000045EE File Offset: 0x000027EE
		// (set) Token: 0x06000143 RID: 323 RVA: 0x000045F6 File Offset: 0x000027F6
		public NodeID nodeId { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000045FF File Offset: 0x000027FF
		// (set) Token: 0x06000145 RID: 325 RVA: 0x00004607 File Offset: 0x00002807
		public bool usingRelay { get; private set; }

		// Token: 0x06000146 RID: 326 RVA: 0x00002371 File Offset: 0x00000571
		public MatchInfo()
		{
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004610 File Offset: 0x00002810
		internal MatchInfo(CreateMatchResponse matchResponse)
		{
			this.address = matchResponse.address;
			this.port = matchResponse.port;
			this.domain = matchResponse.domain;
			this.networkId = (NetworkID)matchResponse.networkId;
			this.accessToken = new NetworkAccessToken(matchResponse.accessTokenString);
			this.nodeId = matchResponse.nodeId;
			this.usingRelay = matchResponse.usingRelay;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00004688 File Offset: 0x00002888
		internal MatchInfo(JoinMatchResponse matchResponse)
		{
			this.address = matchResponse.address;
			this.port = matchResponse.port;
			this.domain = matchResponse.domain;
			this.networkId = (NetworkID)matchResponse.networkId;
			this.accessToken = new NetworkAccessToken(matchResponse.accessTokenString);
			this.nodeId = matchResponse.nodeId;
			this.usingRelay = matchResponse.usingRelay;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00004700 File Offset: 0x00002900
		public override string ToString()
		{
			return UnityString.Format("{0} @ {1}:{2} [{3},{4}]", new object[] { this.networkId, this.address, this.port, this.nodeId, this.usingRelay });
		}
	}
}
