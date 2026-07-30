using System;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200006F RID: 111
	public class RfcLdapSuperDN : Asn1Tagged
	{
		// Token: 0x060003BB RID: 955 RVA: 0x00012144 File Offset: 0x00010344
		public RfcLdapSuperDN(string s)
			: base(RfcLdapSuperDN.ID, new Asn1OctetString(s), false)
		{
			try
			{
				sbyte[] array = SupportClass.ToSByteArray(Encoding.GetEncoding("utf-8").GetBytes(s));
				this.content = array;
			}
			catch (IOException ex)
			{
				throw new SystemException(ex.ToString());
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x000121A0 File Offset: 0x000103A0
		[CLSCompliant(false)]
		public RfcLdapSuperDN(sbyte[] ba)
			: base(RfcLdapSuperDN.ID, new Asn1OctetString(ba), false)
		{
			this.content = ba;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x000121BB File Offset: 0x000103BB
		public override void encode(Asn1Encoder enc, Stream out_Renamed)
		{
			enc.encode(this, out_Renamed);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x000121C5 File Offset: 0x000103C5
		[CLSCompliant(false)]
		public sbyte[] byteValue()
		{
			return this.content;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x000121D0 File Offset: 0x000103D0
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

		// Token: 0x060003C0 RID: 960 RVA: 0x00012220 File Offset: 0x00010420
		public override string ToString()
		{
			return base.ToString() + " " + this.stringValue();
		}

		// Token: 0x0400024C RID: 588
		private sbyte[] content;

		// Token: 0x0400024D RID: 589
		public static readonly int TAG = 0;

		// Token: 0x0400024E RID: 590
		protected static readonly Asn1Identifier ID = new Asn1Identifier(2, false, RfcLdapSuperDN.TAG);
	}
}
