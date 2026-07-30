using System;

namespace Mono.Net.Dns
{
	// Token: 0x02000092 RID: 146
	internal enum DnsQType : ushort
	{
		// Token: 0x04000840 RID: 2112
		A = 1,
		// Token: 0x04000841 RID: 2113
		NS,
		// Token: 0x04000842 RID: 2114
		[Obsolete]
		MD,
		// Token: 0x04000843 RID: 2115
		[Obsolete]
		MF,
		// Token: 0x04000844 RID: 2116
		CNAME,
		// Token: 0x04000845 RID: 2117
		SOA,
		// Token: 0x04000846 RID: 2118
		[Obsolete]
		MB,
		// Token: 0x04000847 RID: 2119
		[Obsolete]
		MG,
		// Token: 0x04000848 RID: 2120
		[Obsolete]
		MR,
		// Token: 0x04000849 RID: 2121
		[Obsolete]
		NULL,
		// Token: 0x0400084A RID: 2122
		[Obsolete]
		WKS,
		// Token: 0x0400084B RID: 2123
		PTR,
		// Token: 0x0400084C RID: 2124
		[Obsolete]
		HINFO,
		// Token: 0x0400084D RID: 2125
		[Obsolete]
		MINFO,
		// Token: 0x0400084E RID: 2126
		MX,
		// Token: 0x0400084F RID: 2127
		TXT,
		// Token: 0x04000850 RID: 2128
		[Obsolete]
		RP,
		// Token: 0x04000851 RID: 2129
		AFSDB,
		// Token: 0x04000852 RID: 2130
		[Obsolete]
		X25,
		// Token: 0x04000853 RID: 2131
		[Obsolete]
		ISDN,
		// Token: 0x04000854 RID: 2132
		[Obsolete]
		RT,
		// Token: 0x04000855 RID: 2133
		[Obsolete]
		NSAP,
		// Token: 0x04000856 RID: 2134
		[Obsolete]
		NSAPPTR,
		// Token: 0x04000857 RID: 2135
		SIG,
		// Token: 0x04000858 RID: 2136
		KEY,
		// Token: 0x04000859 RID: 2137
		[Obsolete]
		PX,
		// Token: 0x0400085A RID: 2138
		[Obsolete]
		GPOS,
		// Token: 0x0400085B RID: 2139
		AAAA,
		// Token: 0x0400085C RID: 2140
		LOC,
		// Token: 0x0400085D RID: 2141
		[Obsolete]
		NXT,
		// Token: 0x0400085E RID: 2142
		[Obsolete]
		EID,
		// Token: 0x0400085F RID: 2143
		[Obsolete]
		NIMLOC,
		// Token: 0x04000860 RID: 2144
		SRV,
		// Token: 0x04000861 RID: 2145
		[Obsolete]
		ATMA,
		// Token: 0x04000862 RID: 2146
		NAPTR,
		// Token: 0x04000863 RID: 2147
		KX,
		// Token: 0x04000864 RID: 2148
		CERT,
		// Token: 0x04000865 RID: 2149
		[Obsolete]
		A6,
		// Token: 0x04000866 RID: 2150
		DNAME,
		// Token: 0x04000867 RID: 2151
		[Obsolete]
		SINK,
		// Token: 0x04000868 RID: 2152
		OPT,
		// Token: 0x04000869 RID: 2153
		[Obsolete]
		APL,
		// Token: 0x0400086A RID: 2154
		DS,
		// Token: 0x0400086B RID: 2155
		SSHFP,
		// Token: 0x0400086C RID: 2156
		IPSECKEY,
		// Token: 0x0400086D RID: 2157
		RRSIG,
		// Token: 0x0400086E RID: 2158
		NSEC,
		// Token: 0x0400086F RID: 2159
		DNSKEY,
		// Token: 0x04000870 RID: 2160
		DHCID,
		// Token: 0x04000871 RID: 2161
		NSEC3,
		// Token: 0x04000872 RID: 2162
		NSEC3PARAM,
		// Token: 0x04000873 RID: 2163
		HIP = 55,
		// Token: 0x04000874 RID: 2164
		NINFO,
		// Token: 0x04000875 RID: 2165
		RKEY,
		// Token: 0x04000876 RID: 2166
		TALINK,
		// Token: 0x04000877 RID: 2167
		SPF = 99,
		// Token: 0x04000878 RID: 2168
		[Obsolete]
		UINFO,
		// Token: 0x04000879 RID: 2169
		[Obsolete]
		UID,
		// Token: 0x0400087A RID: 2170
		[Obsolete]
		GID,
		// Token: 0x0400087B RID: 2171
		[Obsolete]
		UNSPEC,
		// Token: 0x0400087C RID: 2172
		TKEY = 249,
		// Token: 0x0400087D RID: 2173
		TSIG,
		// Token: 0x0400087E RID: 2174
		IXFR,
		// Token: 0x0400087F RID: 2175
		AXFR,
		// Token: 0x04000880 RID: 2176
		[Obsolete]
		MAILB,
		// Token: 0x04000881 RID: 2177
		[Obsolete]
		MAILA,
		// Token: 0x04000882 RID: 2178
		ALL,
		// Token: 0x04000883 RID: 2179
		URI,
		// Token: 0x04000884 RID: 2180
		TA = 32768,
		// Token: 0x04000885 RID: 2181
		DLV
	}
}
