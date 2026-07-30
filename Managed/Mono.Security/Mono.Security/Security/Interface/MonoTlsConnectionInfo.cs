using System;

namespace Mono.Security.Interface
{
	// Token: 0x02000081 RID: 129
	public class MonoTlsConnectionInfo
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x00017084 File Offset: 0x00015284
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x0001708C File Offset: 0x0001528C
		[CLSCompliant(false)]
		public CipherSuiteCode CipherSuiteCode { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x00017095 File Offset: 0x00015295
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0001709D File Offset: 0x0001529D
		public TlsProtocols ProtocolVersion { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x000170A6 File Offset: 0x000152A6
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x000170AE File Offset: 0x000152AE
		public CipherAlgorithmType CipherAlgorithmType { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x000170B7 File Offset: 0x000152B7
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x000170BF File Offset: 0x000152BF
		public HashAlgorithmType HashAlgorithmType { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x000170C8 File Offset: 0x000152C8
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x000170D0 File Offset: 0x000152D0
		public ExchangeAlgorithmType ExchangeAlgorithmType { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x000170D9 File Offset: 0x000152D9
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x000170E1 File Offset: 0x000152E1
		public string PeerDomainName { get; set; }

		// Token: 0x060004BB RID: 1211 RVA: 0x000170EA File Offset: 0x000152EA
		public override string ToString()
		{
			return string.Format("[MonoTlsConnectionInfo: {0}:{1}]", this.ProtocolVersion, this.CipherSuiteCode);
		}
	}
}
