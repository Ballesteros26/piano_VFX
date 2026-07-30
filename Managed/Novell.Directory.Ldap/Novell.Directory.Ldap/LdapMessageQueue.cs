using System;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000027 RID: 39
	public abstract class LdapMessageQueue
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000086D9 File Offset: 0x000068D9
		internal virtual string DebugName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000086E1 File Offset: 0x000068E1
		internal virtual MessageAgent MessageAgent
		{
			get
			{
				return this.agent;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000086E9 File Offset: 0x000068E9
		public virtual int[] MessageIDs
		{
			get
			{
				return this.agent.MessageIDs;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000086F6 File Offset: 0x000068F6
		internal LdapMessageQueue(string myname, MessageAgent agent)
		{
			this.agent = agent;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008710 File Offset: 0x00006910
		public virtual LdapMessage getResponse()
		{
			return this.getResponse(null);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00008719 File Offset: 0x00006919
		public virtual LdapMessage getResponse(int msgid)
		{
			return this.getResponse(new Integer32(msgid));
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00008728 File Offset: 0x00006928
		private LdapMessage getResponse(Integer32 msgid)
		{
			object ldapMessage;
			if ((ldapMessage = this.agent.getLdapMessage(msgid)) == null)
			{
				return null;
			}
			if (ldapMessage is LdapResponse)
			{
				return (LdapMessage)ldapMessage;
			}
			RfcLdapMessage rfcLdapMessage = (RfcLdapMessage)ldapMessage;
			int type = rfcLdapMessage.Type;
			if (type <= 19)
			{
				if (type == 4)
				{
					return new LdapSearchResult(rfcLdapMessage);
				}
				if (type == 19)
				{
					return new LdapSearchResultReference(rfcLdapMessage);
				}
			}
			else
			{
				if (type == 24)
				{
					new ExtResponseFactory();
					return ExtResponseFactory.convertToExtendedResponse(rfcLdapMessage);
				}
				if (type == 25)
				{
					return IntermediateResponseFactory.convertToIntermediateResponse(rfcLdapMessage);
				}
			}
			return new LdapResponse(rfcLdapMessage);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000087B2 File Offset: 0x000069B2
		public virtual bool isResponseReceived()
		{
			return this.agent.isResponseReceived();
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000087BF File Offset: 0x000069BF
		public virtual bool isResponseReceived(int msgid)
		{
			return this.agent.isResponseReceived(msgid);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000087CD File Offset: 0x000069CD
		public virtual bool isComplete(int msgid)
		{
			return this.agent.isComplete(msgid);
		}

		// Token: 0x0400011B RID: 283
		internal MessageAgent agent;

		// Token: 0x0400011C RID: 284
		internal string name = "";

		// Token: 0x0400011D RID: 285
		internal static object nameLock = new object();

		// Token: 0x0400011E RID: 286
		internal static int queueNum;
	}
}
