using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D6 RID: 214
	public class Asn1Null : Asn1Object
	{
		// Token: 0x06000547 RID: 1351 RVA: 0x00017387 File Offset: 0x00015587
		public Asn1Null()
			: base(Asn1Null.ID)
		{
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00017394 File Offset: 0x00015594
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001739E File Offset: 0x0001559E
		public override string ToString()
		{
			return base.ToString() + "NULL: \"\"";
		}

		// Token: 0x040004AB RID: 1195
		public const int TAG = 5;

		// Token: 0x040004AC RID: 1196
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 5);
	}
}
