using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D4 RID: 212
	public class Asn1Integer : Asn1Numeric
	{
		// Token: 0x0600053B RID: 1339 RVA: 0x000171CC File Offset: 0x000153CC
		public Asn1Integer(int content)
			: base(Asn1Integer.ID, content)
		{
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000171DA File Offset: 0x000153DA
		public Asn1Integer(long content)
			: base(Asn1Integer.ID, content)
		{
		}

		// Token: 0x0600053D RID: 1341 RVA: 0x000171E8 File Offset: 0x000153E8
		[CLSCompliant(false)]
		public Asn1Integer(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1Integer.ID, (long)dec.decodeNumeric(in_Renamed, len))
		{
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x00017202 File Offset: 0x00015402
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001720C File Offset: 0x0001540C
		public override string ToString()
		{
			return base.ToString() + "INTEGER: " + base.longValue();
		}

		// Token: 0x040004A7 RID: 1191
		public const int TAG = 2;

		// Token: 0x040004A8 RID: 1192
		public static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 2);
	}
}
