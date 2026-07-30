using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000DB RID: 219
	public class Asn1SequenceOf : Asn1Structured
	{
		// Token: 0x06000564 RID: 1380 RVA: 0x0001764E File Offset: 0x0001584E
		public Asn1SequenceOf()
			: base(Asn1SequenceOf.ID)
		{
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001765B File Offset: 0x0001585B
		public Asn1SequenceOf(int size)
			: base(Asn1SequenceOf.ID, size)
		{
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x00017669 File Offset: 0x00015869
		public Asn1SequenceOf(Asn1Sequence sequence)
			: base(Asn1SequenceOf.ID, sequence.toArray(), sequence.size())
		{
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x00017682 File Offset: 0x00015882
		[CLSCompliant(false)]
		public Asn1SequenceOf(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1SequenceOf.ID)
		{
			base.decodeStructured(dec, in_Renamed, len);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x00017698 File Offset: 0x00015898
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SEQUENCE OF: { ");
		}

		// Token: 0x040004B4 RID: 1204
		public const int TAG = 16;

		// Token: 0x040004B5 RID: 1205
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 16);
	}
}
