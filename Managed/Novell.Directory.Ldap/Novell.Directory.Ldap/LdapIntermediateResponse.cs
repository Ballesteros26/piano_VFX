using System;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x02000022 RID: 34
	public class LdapIntermediateResponse : LdapResponse
	{
		// Token: 0x0600017E RID: 382 RVA: 0x00007CDD File Offset: 0x00005EDD
		public static void register(string oid, Type extendedResponseClass)
		{
			LdapIntermediateResponse.registeredResponses.registerResponseExtension(oid, extendedResponseClass);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007CEB File Offset: 0x00005EEB
		public static RespExtensionSet getRegisteredResponses()
		{
			return LdapIntermediateResponse.registeredResponses;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007CF2 File Offset: 0x00005EF2
		public LdapIntermediateResponse(RfcLdapMessage message)
			: base(message)
		{
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007CFC File Offset: 0x00005EFC
		public string getID()
		{
			RfcLdapOID responseName = ((RfcIntermediateResponse)this.message.Response).getResponseName();
			if (responseName == null)
			{
				return null;
			}
			return responseName.stringValue();
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007D2C File Offset: 0x00005F2C
		[CLSCompliant(false)]
		public sbyte[] getValue()
		{
			Asn1OctetString response = ((RfcIntermediateResponse)this.message.Response).getResponse();
			if (response == null)
			{
				return null;
			}
			return response.byteValue();
		}

		// Token: 0x040000FE RID: 254
		private static RespExtensionSet registeredResponses = new RespExtensionSet();
	}
}
