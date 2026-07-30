using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000095 RID: 149
	public class PartitionEntryCountRequest : LdapExtendedOperation
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x00013828 File Offset: 0x00011A28
		static PartitionEntryCountRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.14", Type.GetType("Novell.Directory.Ldap.Extensions.PartitionEntryCountResponse"));
			}
			catch (Exception)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00013870 File Offset: 0x00011A70
		public PartitionEntryCountRequest(string dn)
			: base("2.16.840.1.113719.1.27.100.13", null)
		{
			try
			{
				if (dn == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				new Asn1OctetString(dn).encode(lberencoder, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
