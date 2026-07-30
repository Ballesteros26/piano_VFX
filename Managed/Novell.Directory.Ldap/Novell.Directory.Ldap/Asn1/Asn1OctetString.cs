using System;
using System.IO;
using System.Text;

namespace Novell.Directory.Ldap.Asn1
{
	// Token: 0x020000D9 RID: 217
	public class Asn1OctetString : Asn1Object
	{
		// Token: 0x06000556 RID: 1366 RVA: 0x000174CD File Offset: 0x000156CD
		[CLSCompliant(false)]
		public Asn1OctetString(sbyte[] content)
			: base(Asn1OctetString.ID)
		{
			this.content = content;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x000174E4 File Offset: 0x000156E4
		public Asn1OctetString(string content)
			: base(Asn1OctetString.ID)
		{
			try
			{
				sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(content));
				this.content = array;
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00017538 File Offset: 0x00015738
		[CLSCompliant(false)]
		public Asn1OctetString(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(Asn1OctetString.ID)
		{
			this.content = ((len > 0) ? ((sbyte[])dec.decodeOctetString(in_Renamed, len)) : new sbyte[0]);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00017564 File Offset: 0x00015764
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x0001756E File Offset: 0x0001576E
		[CLSCompliant(false)]
		public sbyte[] byteValue()
		{
			return this.content;
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00017578 File Offset: 0x00015778
		public string stringValue()
		{
			string text = null;
			try
			{
				text = new string(Encoding.GetEncoding("utf-8").GetChars(SupportClass.ToByteArray(this.content)));
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
			return text;
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x000175C8 File Offset: 0x000157C8
		public override string ToString()
		{
			return base.ToString() + "OCTET STRING: " + this.stringValue();
		}

		// Token: 0x040004AF RID: 1199
		private sbyte[] content;

		// Token: 0x040004B0 RID: 1200
		public const int TAG = 4;

		// Token: 0x040004B1 RID: 1201
		protected internal static readonly Asn1Identifier ID = new Asn1Identifier(0, false, 4);
	}
}
