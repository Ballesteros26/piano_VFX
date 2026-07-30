using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000021 RID: 33
	public class LdapExtendedResponse : LdapResponse
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00007C54 File Offset: 0x00005E54
		public virtual string ID
		{
			get
			{
				RfcLdapOID responseName = ((RfcExtendedResponse)this.message.Response).ResponseName;
				if (responseName == null)
				{
					return null;
				}
				return responseName.stringValue();
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00007C8E File Offset: 0x00005E8E
		public static RespExtensionSet RegisteredResponses
		{
			get
			{
				return LdapExtendedResponse.registeredResponses;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00007C98 File Offset: 0x00005E98
		[CLSCompliant(false)]
		public virtual sbyte[] Value
		{
			get
			{
				Asn1OctetString response = ((RfcExtendedResponse)this.message.Response).Response;
				if (response == null)
				{
					return null;
				}
				return response.byteValue();
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00007CC6 File Offset: 0x00005EC6
		public LdapExtendedResponse(RfcLdapMessage message)
			: base(message)
		{
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007CCF File Offset: 0x00005ECF
		public static void register(string oid, Type extendedResponseClass)
		{
			LdapExtendedResponse.registeredResponses.registerResponseExtension(oid, extendedResponseClass);
		}

		// Token: 0x040000FD RID: 253
		private static RespExtensionSet registeredResponses = new RespExtensionSet();
	}
}
