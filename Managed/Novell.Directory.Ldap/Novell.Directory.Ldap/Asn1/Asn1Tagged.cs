using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000DF RID: 223
	public class Asn1Tagged : Asn1Object
	{
		// Token: 0x1700017B RID: 379
		// (set) Token: 0x0600057F RID: 1407 RVA: 0x0001798B File Offset: 0x00015B8B
		[CLSCompliant(false)]
		public virtual Asn1Object TaggedValue
		{
			set
			{
				this.content = value;
				if (!this.explicit_Renamed && value != null)
				{
					value.setIdentifier(this.getIdentifier());
				}
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x000179AB File Offset: 0x00015BAB
		public virtual bool Explicit
		{
			get
			{
				return this.explicit_Renamed;
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000179B3 File Offset: 0x00015BB3
		public Asn1Tagged(Asn1Identifier identifier, Asn1Object object_Renamed)
			: this(identifier, object_Renamed, true)
		{
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000179BE File Offset: 0x00015BBE
		public Asn1Tagged(Asn1Identifier identifier, Asn1Object object_Renamed, bool explicit_Renamed)
			: base(identifier)
		{
			this.content = object_Renamed;
			this.explicit_Renamed = explicit_Renamed;
			if (!explicit_Renamed && this.content != null)
			{
				this.content.setIdentifier(identifier);
			}
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x000179EC File Offset: 0x00015BEC
		[CLSCompliant(false)]
		public Asn1Tagged(Asn1Decoder dec, Stream in_Renamed, int len, Asn1Identifier identifier)
			: base(identifier)
		{
			this.content = new Asn1OctetString(dec, in_Renamed, len);
		}

		// Token: 0x06000584 RID: 1412 RVA: 0x00017A04 File Offset: 0x00015C04
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x06000585 RID: 1413 RVA: 0x00017A0E File Offset: 0x00015C0E
		public Asn1Object taggedValue()
		{
			return this.content;
		}

		// Token: 0x06000586 RID: 1414 RVA: 0x00017A16 File Offset: 0x00015C16
		public override string ToString()
		{
			if (this.explicit_Renamed)
			{
				return base.ToString() + this.content.ToString();
			}
			return this.content.ToString();
		}

		// Token: 0x040004BC RID: 1212
		private bool explicit_Renamed;

		// Token: 0x040004BD RID: 1213
		private Asn1Object content;
	}
}
