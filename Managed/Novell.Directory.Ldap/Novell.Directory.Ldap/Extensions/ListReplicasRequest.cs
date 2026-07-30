using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000091 RID: 145
	public class ListReplicasRequest : LdapExtendedOperation
	{
		// Token: 0x0600041B RID: 1051 RVA: 0x0001360C File Offset: 0x0001180C
		static ListReplicasRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.20", Type.GetType("Novell.Directory.Ldap.Extensions.ListReplicasResponse"));
			}
			catch (Exception)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x00013654 File Offset: 0x00011854
		public ListReplicasRequest(string serverName)
			: base("2.16.840.1.113719.1.27.100.19", null)
		{
			try
			{
				if (serverName == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				new Asn1OctetString(serverName).encode(lberencoder, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
