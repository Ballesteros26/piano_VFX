using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x02000062 RID: 98
	public class RfcControl : Asn1Sequence
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600035C RID: 860 RVA: 0x000107D3 File Offset: 0x0000E9D3
		public virtual Asn1OctetString ControlType
		{
			get
			{
				return (Asn1OctetString)base.get_Renamed(0);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600035D RID: 861 RVA: 0x000107E4 File Offset: 0x0000E9E4
		public virtual Asn1Boolean Criticality
		{
			get
			{
				if (base.size() > 1)
				{
					Asn1Object asn1Object = base.get_Renamed(1);
					if (asn1Object is Asn1Boolean)
					{
						return (Asn1Boolean)asn1Object;
					}
				}
				return new Asn1Boolean(false);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00010818 File Offset: 0x0000EA18
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0001085C File Offset: 0x0000EA5C
		public virtual Asn1OctetString ControlValue
		{
			get
			{
				if (base.size() > 2)
				{
					return (Asn1OctetString)base.get_Renamed(2);
				}
				if (base.size() > 1)
				{
					Asn1Object asn1Object = base.get_Renamed(1);
					if (asn1Object is Asn1OctetString)
					{
						return (Asn1OctetString)asn1Object;
					}
				}
				return null;
			}
			set
			{
				if (value == null)
				{
					return;
				}
				if (base.size() == 3)
				{
					base.set_Renamed(2, value);
					return;
				}
				if (base.size() != 2)
				{
					return;
				}
				if (base.get_Renamed(1) is Asn1OctetString)
				{
					base.set_Renamed(1, value);
					return;
				}
				base.add(value);
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0001089C File Offset: 0x0000EA9C
		public RfcControl(RfcLdapOID controlType)
			: this(controlType, new Asn1Boolean(false), null)
		{
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000108AC File Offset: 0x0000EAAC
		public RfcControl(RfcLdapOID controlType, Asn1Boolean criticality)
			: this(controlType, criticality, null)
		{
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000108B7 File Offset: 0x0000EAB7
		public RfcControl(RfcLdapOID controlType, Asn1Boolean criticality, Asn1OctetString controlValue)
			: base(3)
		{
			base.add(controlType);
			if (criticality.booleanValue())
			{
				base.add(criticality);
			}
			if (controlValue != null)
			{
				base.add(controlValue);
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000108E0 File Offset: 0x0000EAE0
		[CLSCompliant(false)]
		public RfcControl(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000108EC File Offset: 0x0000EAEC
		public RfcControl(Asn1Sequence seqObj)
			: base(3)
		{
			int num = seqObj.size();
			for (int i = 0; i < num; i++)
			{
				base.add(seqObj.get_Renamed(i));
			}
		}
	}
}
