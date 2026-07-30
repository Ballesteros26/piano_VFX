using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D2 RID: 210
	public class Asn1Enumerated : Asn1Numeric
	{
		// Token: 0x06000527 RID: 1319 RVA: 0x00016F91 File Offset: 0x00015191
		public Asn1Enumerated(int content)
			: base(Asn1Enumerated.ID, content)
		{
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00016F9F File Offset: 0x0001519F
		public Asn1Enumerated(long content)
			: base(Asn1Enumerated.ID, content)
		{
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00016FAD File Offset: 0x000151AD
		[CLSCompliant(false)]
		public Asn1Enumerated(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1Enumerated.ID, (long)dec.decodeNumeric(in_Renamed, len))
		{
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x00016FC7 File Offset: 0x000151C7
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x00016FD1 File Offset: 0x000151D1
		public override string ToString()
		{
			return base.ToString() + "ENUMERATED: " + base.longValue();
		}

		// Token: 0x0400049D RID: 1181
		public const int TAG = 10;

		// Token: 0x0400049E RID: 1182
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 10);
	}
}
