using System;
using System.Reflection;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000049 RID: 73
	public class IntermediateResponseFactory
	{
		// Token: 0x060002D2 RID: 722 RVA: 0x0000E09C File Offset: 0x0000C29C
		public static LdapIntermediateResponse convertToIntermediateResponse(RfcLdapMessage inResponse)
		{
			LdapIntermediateResponse ldapIntermediateResponse = new LdapIntermediateResponse(inResponse);
			string id = ldapIntermediateResponse.getID();
			RespExtensionSet registeredResponses = LdapIntermediateResponse.getRegisteredResponses();
			try
			{
				Type type = registeredResponses.findResponseExtension(id);
				if (type == null)
				{
					return ldapIntermediateResponse;
				}
				Type[] array = new Type[] { typeof(RfcLdapMessage) };
				object[] array2 = new object[] { inResponse };
				try
				{
					ConstructorInfo constructor = type.GetConstructor(array);
					try
					{
						return (LdapIntermediateResponse)constructor.Invoke(array2);
					}
					catch (UnauthorizedAccessException)
					{
					}
					catch (TargetInvocationException)
					{
					}
				}
				catch (MissingMethodException)
				{
				}
			}
			catch (MissingFieldException)
			{
			}
			return ldapIntermediateResponse;
		}
	}
}
