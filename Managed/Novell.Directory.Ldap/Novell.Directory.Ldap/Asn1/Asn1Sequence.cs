using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000DA RID: 218
	public class Asn1Sequence : Asn1Structured
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x000175EF File Offset: 0x000157EF
		public Asn1Sequence()
			: base(Asn1Sequence.ID, 10)
		{
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x000175FE File Offset: 0x000157FE
		public Asn1Sequence(int size)
			: base(Asn1Sequence.ID, size)
		{
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x0001760C File Offset: 0x0001580C
		public Asn1Sequence(Asn1Object[] newContent, int size)
			: base(Asn1Sequence.ID, newContent, size)
		{
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001761B File Offset: 0x0001581B
		[CLSCompliant(false)]
		public Asn1Sequence(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1Sequence.ID)
		{
			base.decodeStructured(dec, in_Renamed, len);
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x00017631 File Offset: 0x00015831
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SEQUENCE: { ");
		}

		// Token: 0x040004B2 RID: 1202
		public const int TAG = 16;

		// Token: 0x040004B3 RID: 1203
		private static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 16);
	}
}
