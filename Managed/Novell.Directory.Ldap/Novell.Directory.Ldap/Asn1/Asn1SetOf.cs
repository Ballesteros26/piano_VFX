using System;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000DD RID: 221
	public class Asn1SetOf : Asn1Structured
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x00017703 File Offset: 0x00015903
		public Asn1SetOf()
			: base(Asn1SetOf.ID)
		{
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00017710 File Offset: 0x00015910
		public Asn1SetOf(int size)
			: base(Asn1SetOf.ID, size)
		{
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x0001771E File Offset: 0x0001591E
		public Asn1SetOf(Asn1Set set_Renamed)
			: base(Asn1SetOf.ID, set_Renamed.toArray(), set_Renamed.size())
		{
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00017737 File Offset: 0x00015937
		[CLSCompliant(false)]
		public override string ToString()
		{
			return base.toString("SET OF: { ");
		}

		// Token: 0x040004B8 RID: 1208
		public const int TAG = 17;

		// Token: 0x040004B9 RID: 1209
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, true, 17);
	}
}
