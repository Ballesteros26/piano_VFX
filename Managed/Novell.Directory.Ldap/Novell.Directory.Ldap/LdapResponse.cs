using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200002F RID: 47
	public class LdapResponse : LdapMessage
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x000096B3 File Offset: 0x000078B3
		public virtual string ErrorMessage
		{
			get
			{
				if (this.exception != null)
				{
					return this.exception.LdapErrorMessage;
				}
				return ((RfcResponse)this.message.Response).getErrorMessage().stringValue();
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x000096E3 File Offset: 0x000078E3
		public virtual string MatchedDN
		{
			get
			{
				if (this.exception != null)
				{
					return this.exception.MatchedDN;
				}
				return ((RfcResponse)this.message.Response).getMatchedDN().stringValue();
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00009714 File Offset: 0x00007914
		public virtual string[] Referrals
		{
			get
			{
				string[] array = null;
				RfcReferral referral = ((RfcResponse)this.message.Response).getReferral();
				if (referral == null)
				{
					array = new string[0];
				}
				else
				{
					int num = referral.size();
					array = new string[num];
					for (int i = 0; i < num; i++)
					{
						string text = ((Asn1OctetString)referral.get_Renamed(i)).stringValue();
						try
						{
							LdapUrl ldapUrl = new LdapUrl(text);
							string requestDN;
							if (ldapUrl.getDN() == null && (requestDN = base.Asn1Object.RequestingMessage.Asn1Object.RequestDN) != null)
							{
								ldapUrl.setDN(requestDN);
								text = ldapUrl.ToString();
							}
						}
						catch (UriFormatException)
						{
						}
						finally
						{
							array[i] = text;
						}
					}
				}
				return array;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x000097D8 File Offset: 0x000079D8
		public virtual int ResultCode
		{
			get
			{
				if (this.exception != null)
				{
					return this.exception.ResultCode;
				}
				if (((RfcResponse)this.message.Response) is RfcIntermediateResponse)
				{
					return 0;
				}
				return ((RfcResponse)this.message.Response).getResultCode().intValue();
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000982C File Offset: 0x00007A2C
		internal virtual LdapException ResultException
		{
			get
			{
				LdapException ex = null;
				int resultCode = this.ResultCode;
				if (resultCode != 0 && resultCode - 5 > 1)
				{
					if (resultCode == 10)
					{
						string[] referrals = this.Referrals;
						ex = new LdapReferralException("Automatic referral following not enabled", 10, this.ErrorMessage);
						((LdapReferralException)ex).setReferrals(referrals);
					}
					else
					{
						ex = new LdapException(LdapException.resultCodeToString(this.ResultCode), this.ResultCode, this.ErrorMessage, this.MatchedDN);
					}
				}
				return ex;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001DB RID: 475 RVA: 0x0000989C File Offset: 0x00007A9C
		public override LdapControl[] Controls
		{
			get
			{
				if (this.exception != null)
				{
					return null;
				}
				return base.Controls;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000098AE File Offset: 0x00007AAE
		public override int MessageID
		{
			get
			{
				if (this.exception != null)
				{
					return this.exception.MessageID;
				}
				return base.MessageID;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000098CA File Offset: 0x00007ACA
		public override int Type
		{
			get
			{
				if (this.exception != null)
				{
					return this.exception.ReplyType;
				}
				return base.Type;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001DE RID: 478 RVA: 0x000098E6 File Offset: 0x00007AE6
		internal virtual LdapException Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001DF RID: 479 RVA: 0x000098EE File Offset: 0x00007AEE
		internal virtual ReferralInfo ActiveReferral
		{
			get
			{
				return this.activeReferral;
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000098F6 File Offset: 0x00007AF6
		public LdapResponse(InterThreadException ex, ReferralInfo activeReferral)
		{
			this.exception = ex;
			this.activeReferral = activeReferral;
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000990C File Offset: 0x00007B0C
		internal LdapResponse(RfcLdapMessage message)
			: base(message)
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009915 File Offset: 0x00007B15
		public LdapResponse(int type)
			: this(type, 0, null, null, null, null)
		{
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009923 File Offset: 0x00007B23
		public LdapResponse(int type, int resultCode, string matchedDN, string serverMessage, string[] referrals, LdapControl[] controls)
			: base(new RfcLdapMessage(LdapResponse.RfcResultFactory(type, resultCode, matchedDN, serverMessage, referrals)))
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000993C File Offset: 0x00007B3C
		private static Asn1Sequence RfcResultFactory(int type, int resultCode, string matchedDN, string serverMessage, string[] referrals)
		{
			if (matchedDN == null)
			{
				matchedDN = "";
			}
			if (serverMessage == null)
			{
				serverMessage = "";
			}
			switch (type)
			{
			case 1:
				return null;
			case 2:
			case 3:
			case 6:
			case 8:
			case 10:
			case 12:
			case 14:
				break;
			case 4:
				return null;
			case 5:
				return new RfcSearchResultDone(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 7:
				return new RfcModifyResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 9:
				return new RfcAddResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 11:
				return new RfcDelResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 13:
				return new RfcModifyDNResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			case 15:
				return new RfcCompareResponse(new Asn1Enumerated(resultCode), new RfcLdapDN(matchedDN), new RfcLdapString(serverMessage), null);
			default:
				if (type == 19)
				{
					return null;
				}
				if (type == 24)
				{
					return null;
				}
				break;
			}
			throw new SystemException("Type " + type + " Not Supported");
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00009A90 File Offset: 0x00007C90
		internal virtual void chkResultCode()
		{
			if (this.exception != null)
			{
				throw this.exception;
			}
			LdapException resultException = this.ResultException;
			if (resultException != null)
			{
				throw resultException;
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00009AB8 File Offset: 0x00007CB8
		internal virtual bool hasException()
		{
			return this.exception != null;
		}

		// Token: 0x04000130 RID: 304
		private InterThreadException exception;

		// Token: 0x04000131 RID: 305
		private ReferralInfo activeReferral;
	}
}
