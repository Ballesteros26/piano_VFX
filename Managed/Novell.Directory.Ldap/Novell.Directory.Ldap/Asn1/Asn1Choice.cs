using System;
using System.IO;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000CF RID: 207
	public class Asn1Choice : Asn1Object
	{
		// Token: 0x17000170 RID: 368
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00016F29 File Offset: 0x00015129
		[CLSCompliant(false)]
		protected internal virtual Asn1Object ChoiceValue
		{
			set
			{
				this.content = value;
			}
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00016F32 File Offset: 0x00015132
		public Asn1Choice(Asn1Object content)
			: base(null)
		{
			this.content = content;
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00016F42 File Offset: 0x00015142
		protected internal Asn1Choice()
			: base(null)
		{
			this.content = null;
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00016F52 File Offset: 0x00015152
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			this.content.encode(enc, out_Renamed);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00016F61 File Offset: 0x00015161
		public Asn1Object choiceValue()
		{
			return this.content;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x00016F69 File Offset: 0x00015169
		public override Asn1Identifier getIdentifier()
		{
			return this.content.getIdentifier();
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00016F76 File Offset: 0x00015176
		public override void setIdentifier(Asn1Identifier id)
		{
			this.content.setIdentifier(id);
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00016F84 File Offset: 0x00015184
		public override string ToString()
		{
			return this.content.ToString();
		}

		// Token: 0x0400049C RID: 1180
		private Asn1Object content;
	}
}
