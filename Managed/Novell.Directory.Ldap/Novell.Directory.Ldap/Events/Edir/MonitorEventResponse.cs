using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000B9 RID: 185
	public class MonitorEventResponse : LdapExtendedResponse
	{
		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00014CF4 File Offset: 0x00012EF4
		public EdirEventSpecifier[] SpecifierList
		{
			get
			{
				return this.specifier_list;
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00014CFC File Offset: 0x00012EFC
		public MonitorEventResponse(RfcLdapMessage message)
			: base(message)
		{
			sbyte[] value = this.Value;
			if (value == null)
			{
				throw new LdapException(LdapException.resultCodeToString(this.ResultCode), this.ResultCode, null);
			}
			Asn1Sequence asn1Sequence = (Asn1Sequence)new LBERDecoder().decode(value);
			int num = ((Asn1Integer)asn1Sequence.get_Renamed(0)).intValue();
			Asn1Set asn1Set = (Asn1Set)asn1Sequence.get_Renamed(1);
			this.specifier_list = new EdirEventSpecifier[num];
			for (int i = 0; i < num; i++)
			{
				Asn1Sequence asn1Sequence2 = (Asn1Sequence)asn1Set.get_Renamed(i);
				int num2 = ((Asn1Integer)asn1Sequence2.get_Renamed(0)).intValue();
				int num3 = ((Asn1Enumerated)asn1Sequence2.get_Renamed(1)).intValue();
				this.specifier_list[i] = new EdirEventSpecifier((EdirEventType)num2, (EdirEventResultType)num3);
			}
		}

		// Token: 0x0400042C RID: 1068
		protected EdirEventSpecifier[] specifier_list;
	}
}
