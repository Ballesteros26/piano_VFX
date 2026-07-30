using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200008C RID: 140
	public class GetReplicationFilterRequest : LdapExtendedOperation
	{
		// Token: 0x0600040F RID: 1039 RVA: 0x00012D7C File Offset: 0x00010F7C
		static GetReplicationFilterRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.38", Type.GetType("Novell.Directory.Ldap.Extensions.GetReplicationFilterResponse"));
			}
			catch (Exception)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00012DC4 File Offset: 0x00010FC4
		public GetReplicationFilterRequest(string serverDN)
			: base("2.16.840.1.113719.1.27.100.37", null)
		{
			try
			{
				if (serverDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				new Asn1OctetString(serverDN).encode(lberencoder, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
