using System;
using System.Reflection;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000048 RID: 72
	public class ExtResponseFactory
	{
		// Token: 0x060002D0 RID: 720 RVA: 0x0000DFC8 File Offset: 0x0000C1C8
		public static LdapExtendedResponse convertToExtendedResponse(RfcLdapMessage inResponse)
		{
			LdapExtendedResponse ldapExtendedResponse = new LdapExtendedResponse(inResponse);
			string id = ldapExtendedResponse.ID;
			RespExtensionSet registeredResponses = LdapExtendedResponse.RegisteredResponses;
			try
			{
				Type type = registeredResponses.findResponseExtension(id);
				if (type == null)
				{
					return ldapExtendedResponse;
				}
				Type[] array = new Type[] { typeof(RfcLdapMessage) };
				object[] array2 = new object[] { inResponse };
				try
				{
					ConstructorInfo constructor = type.GetConstructor(array);
					try
					{
						return (LdapExtendedResponse)constructor.Invoke(array2);
					}
					catch (UnauthorizedAccessException)
					{
					}
					catch (TargetInvocationException)
					{
					}
					catch (Exception)
					{
					}
				}
				catch (MethodAccessException)
				{
				}
			}
			catch (FieldAccessException)
			{
			}
			return ldapExtendedResponse;
		}
	}
}
