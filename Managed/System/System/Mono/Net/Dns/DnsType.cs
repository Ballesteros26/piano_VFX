using System;

namespace Mono.Net.Dns
{
	// Token: 0x0200009D RID: 157
	internal enum DnsType : ushort
	{
		// Token: 0x040008AE RID: 2222
		A = 1,
		// Token: 0x040008AF RID: 2223
		NS,
		// Token: 0x040008B0 RID: 2224
		[Obsolete]
		MD,
		// Token: 0x040008B1 RID: 2225
		[Obsolete]
		MF,
		// Token: 0x040008B2 RID: 2226
		CNAME,
		// Token: 0x040008B3 RID: 2227
		SOA,
		// Token: 0x040008B4 RID: 2228
		[Obsolete]
		MB,
		// Token: 0x040008B5 RID: 2229
		[Obsolete]
		MG,
		// Token: 0x040008B6 RID: 2230
		[Obsolete]
		MR,
		// Token: 0x040008B7 RID: 2231
		[Obsolete]
		NULL,
		// Token: 0x040008B8 RID: 2232
		[Obsolete]
		WKS,
		// Token: 0x040008B9 RID: 2233
		PTR,
		// Token: 0x040008BA RID: 2234
		[Obsolete]
		HINFO,
		// Token: 0x040008BB RID: 2235
		[Obsolete]
		MINFO,
		// Token: 0x040008BC RID: 2236
		MX,
		// Token: 0x040008BD RID: 2237
		TXT,
		// Token: 0x040008BE RID: 2238
		[Obsolete]
		RP,
		// Token: 0x040008BF RID: 2239
		AFSDB,
		// Token: 0x040008C0 RID: 2240
		[Obsolete]
		X25,
		// Token: 0x040008C1 RID: 2241
		[Obsolete]
		ISDN,
		// Token: 0x040008C2 RID: 2242
		[Obsolete]
		RT,
		// Token: 0x040008C3 RID: 2243
		[Obsolete]
		NSAP,
		// Token: 0x040008C4 RID: 2244
		[Obsolete]
		NSAPPTR,
		// Token: 0x040008C5 RID: 2245
		SIG,
		// Token: 0x040008C6 RID: 2246
		KEY,
		// Token: 0x040008C7 RID: 2247
		[Obsolete]
		PX,
		// Token: 0x040008C8 RID: 2248
		[Obsolete]
		GPOS,
		// Token: 0x040008C9 RID: 2249
		AAAA,
		// Token: 0x040008CA RID: 2250
		LOC,
		// Token: 0x040008CB RID: 2251
		[Obsolete]
		NXT,
		// Token: 0x040008CC RID: 2252
		[Obsolete]
		EID,
		// Token: 0x040008CD RID: 2253
		[Obsolete]
		NIMLOC,
		// Token: 0x040008CE RID: 2254
		SRV,
		// Token: 0x040008CF RID: 2255
		[Obsolete]
		ATMA,
		// Token: 0x040008D0 RID: 2256
		NAPTR,
		// Token: 0x040008D1 RID: 2257
		KX,
		// Token: 0x040008D2 RID: 2258
		CERT,
		// Token: 0x040008D3 RID: 2259
		[Obsolete]
		A6,
		// Token: 0x040008D4 RID: 2260
		DNAME,
		// Token: 0x040008D5 RID: 2261
		[Obsolete]
		SINK,
		// Token: 0x040008D6 RID: 2262
		OPT,
		// Token: 0x040008D7 RID: 2263
		[Obsolete]
		APL,
		// Token: 0x040008D8 RID: 2264
		DS,
		// Token: 0x040008D9 RID: 2265
		SSHFP,
		// Token: 0x040008DA RID: 2266
		IPSECKEY,
		// Token: 0x040008DB RID: 2267
		RRSIG,
		// Token: 0x040008DC RID: 2268
		NSEC,
		// Token: 0x040008DD RID: 2269
		DNSKEY,
		// Token: 0x040008DE RID: 2270
		DHCID,
		// Token: 0x040008DF RID: 2271
		NSEC3,
		// Token: 0x040008E0 RID: 2272
		NSEC3PARAM,
		// Token: 0x040008E1 RID: 2273
		HIP = 55,
		// Token: 0x040008E2 RID: 2274
		NINFO,
		// Token: 0x040008E3 RID: 2275
		RKEY,
		// Token: 0x040008E4 RID: 2276
		TALINK,
		// Token: 0x040008E5 RID: 2277
		SPF = 99,
		// Token: 0x040008E6 RID: 2278
		[Obsolete]
		UINFO,
		// Token: 0x040008E7 RID: 2279
		[Obsolete]
		UID,
		// Token: 0x040008E8 RID: 2280
		[Obsolete]
		GID,
		// Token: 0x040008E9 RID: 2281
		[Obsolete]
		UNSPEC,
		// Token: 0x040008EA RID: 2282
		TKEY = 249,
		// Token: 0x040008EB RID: 2283
		TSIG,
		// Token: 0x040008EC RID: 2284
		IXFR,
		// Token: 0x040008ED RID: 2285
		AXFR,
		// Token: 0x040008EE RID: 2286
		[Obsolete]
		MAILB,
		// Token: 0x040008EF RID: 2287
		[Obsolete]
		MAILA,
		// Token: 0x040008F0 RID: 2288
		URI = 256,
		// Token: 0x040008F1 RID: 2289
		TA = 32768,
		// Token: 0x040008F2 RID: 2290
		DLV
	}
}
