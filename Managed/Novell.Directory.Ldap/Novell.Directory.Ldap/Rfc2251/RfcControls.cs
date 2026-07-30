using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000063 RID: 99
	public class RfcControls : Asn1SequenceOf
	{
		// Token: 0x06000365 RID: 869 RVA: 0x00010920 File Offset: 0x0000EB20
		public RfcControls()
			: base(5)
		{
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001092C File Offset: 0x0000EB2C
		[CLSCompliant(false)]
		public RfcControls(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
			for (int i = 0; i < base.size(); i++)
			{
				RfcControl rfcControl = new RfcControl((Asn1Sequence)base.get_Renamed(i));
				this.set_Renamed(i, rfcControl);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001096D File Offset: 0x0000EB6D
		public void add(RfcControl control)
		{
			base.add(control);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00010976 File Offset: 0x0000EB76
		public void set_Renamed(int index, RfcControl control)
		{
			base.set_Renamed(index, control);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00010980 File Offset: 0x0000EB80
		public override Asn1Identifier getIdentifier()
		{
			return new Asn1Identifier(2, true, 0);
		}

		// Token: 0x0400022B RID: 555
		public const int CONTROLS = 0;
	}
}
