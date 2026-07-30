using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Rfc2251
{
	// Token: 0x0200006B RID: 107
	public class RfcLdapMessage : Asn1Sequence
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600039F RID: 927 RVA: 0x00011C9D File Offset: 0x0000FE9D
		public virtual int MessageID
		{
			get
			{
				return ((Asn1Integer)base.get_Renamed(0)).intValue();
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x00011CB0 File Offset: 0x0000FEB0
		public virtual int Type
		{
			get
			{
				return base.get_Renamed(1).getIdentifier().Tag;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00011CC3 File Offset: 0x0000FEC3
		public virtual Asn1Object Response
		{
			get
			{
				return base.get_Renamed(1);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003A2 RID: 930 RVA: 0x00011CCC File Offset: 0x0000FECC
		public virtual RfcControls Controls
		{
			get
			{
				if (base.size() > 2)
				{
					return (RfcControls)base.get_Renamed(2);
				}
				return null;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00011CE5 File Offset: 0x0000FEE5
		public virtual string RequestDN
		{
			get
			{
				return ((RfcRequest)this.op).getRequestDN();
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003A4 RID: 932 RVA: 0x00011CF7 File Offset: 0x0000FEF7
		// (set) Token: 0x060003A5 RID: 933 RVA: 0x00011CFF File Offset: 0x0000FEFF
		public virtual LdapMessage RequestingMessage
		{
			get
			{
				return this.requestMessage;
			}
			set
			{
				this.requestMessage = value;
			}
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00011D08 File Offset: 0x0000FF08
		internal RfcLdapMessage(Asn1Object[] origContent, RfcRequest origRequest, string dn, string filter, bool reference)
			: base(origContent, origContent.Length)
		{
			base.set_Renamed(0, new RfcMessageID());
			RfcRequest rfcRequest = ((RfcRequest)origContent[1]).dupRequest(dn, filter, reference);
			this.op = (Asn1Object)rfcRequest;
			base.set_Renamed(1, (Asn1Object)rfcRequest);
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00011D57 File Offset: 0x0000FF57
		public RfcLdapMessage(RfcRequest op)
			: this(op, null)
		{
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00011D61 File Offset: 0x0000FF61
		public RfcLdapMessage(RfcRequest op, RfcControls controls)
			: base(3)
		{
			this.op = (Asn1Object)op;
			this.controls = controls;
			base.add(new RfcMessageID());
			base.add((Asn1Object)op);
			if (controls != null)
			{
				base.add(controls);
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00011D9E File Offset: 0x0000FF9E
		public RfcLdapMessage(Asn1Sequence op)
			: this(op, null)
		{
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00011DA8 File Offset: 0x0000FFA8
		public RfcLdapMessage(Asn1Sequence op, RfcControls controls)
			: base(3)
		{
			this.op = op;
			this.controls = controls;
			base.add(new RfcMessageID());
			base.add(op);
			if (controls != null)
			{
				base.add(controls);
			}
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00011DDC File Offset: 0x0000FFDC
		[CLSCompliant(false)]
		public RfcLdapMessage(Asn1Decoder dec, Stream in_Renamed, int len)
			: base(dec, in_Renamed, len)
		{
			Asn1Tagged asn1Tagged = (Asn1Tagged)base.get_Renamed(1);
			Asn1Identifier identifier = asn1Tagged.getIdentifier();
			sbyte[] array = ((Asn1OctetString)asn1Tagged.taggedValue()).byteValue();
			MemoryStream memoryStream = new MemoryStream(SupportClass.ToByteArray(array));
			int tag = identifier.Tag;
			if (tag <= 19)
			{
				switch (tag)
				{
				case 1:
					base.set_Renamed(1, new RfcBindResponse(dec, memoryStream, array.Length));
					goto IL_01A2;
				case 2:
				case 3:
				case 6:
				case 8:
				case 10:
				case 12:
				case 14:
					break;
				case 4:
					base.set_Renamed(1, new RfcSearchResultEntry(dec, memoryStream, array.Length));
					goto IL_01A2;
				case 5:
					base.set_Renamed(1, new RfcSearchResultDone(dec, memoryStream, array.Length));
					goto IL_01A2;
				case 7:
					base.set_Renamed(1, new RfcModifyResponse(dec, memoryStream, array.Length));
					goto IL_01A2;
				case 9:
					base.set_Renamed(1, new RfcAddResponse(dec, memoryStream, array.Length));
					goto IL_01A2;
				case 11:
					base.set_Renamed(1, new RfcDelResponse(dec, memoryStream, array.Length));
					goto IL_01A2;
				case 13:
					base.set_Renamed(1, new RfcModifyDNResponse(dec, memoryStream, array.Length));
					goto IL_01A2;
				case 15:
					base.set_Renamed(1, new RfcCompareResponse(dec, memoryStream, array.Length));
					goto IL_01A2;
				default:
					if (tag == 19)
					{
						base.set_Renamed(1, new RfcSearchResultReference(dec, memoryStream, array.Length));
						goto IL_01A2;
					}
					break;
				}
			}
			else
			{
				if (tag == 24)
				{
					base.set_Renamed(1, new RfcExtendedResponse(dec, memoryStream, array.Length));
					goto IL_01A2;
				}
				if (tag == 25)
				{
					base.set_Renamed(1, new RfcIntermediateResponse(dec, memoryStream, array.Length));
					goto IL_01A2;
				}
			}
			throw new SystemException("RfcLdapMessage: Invalid tag: " + identifier.Tag);
			IL_01A2:
			if (base.size() > 2)
			{
				array = ((Asn1OctetString)((Asn1Tagged)base.get_Renamed(2)).taggedValue()).byteValue();
				memoryStream = new MemoryStream(SupportClass.ToByteArray(array));
				base.set_Renamed(2, new RfcControls(dec, memoryStream, array.Length));
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00011FCD File Offset: 0x000101CD
		public RfcRequest getRequest()
		{
			return (RfcRequest)base.get_Renamed(1);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00011FDB File Offset: 0x000101DB
		public virtual bool isRequest()
		{
			return base.get_Renamed(1) is RfcRequest;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00011FEC File Offset: 0x000101EC
		public object dupMessage(string dn, string filter, bool reference)
		{
			if (this.op == null)
			{
				throw new LdapException("DUP_ERROR", 82, null);
			}
			return new RfcLdapMessage(base.toArray(), (RfcRequest)base.get_Renamed(1), dn, filter, reference);
		}

		// Token: 0x04000248 RID: 584
		private Asn1Object op;

		// Token: 0x04000249 RID: 585
		private RfcControls controls;

		// Token: 0x0400024A RID: 586
		private LdapMessage requestMessage;
	}
}
