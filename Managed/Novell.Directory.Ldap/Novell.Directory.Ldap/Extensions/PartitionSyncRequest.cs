using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000097 RID: 151
	public class PartitionSyncRequest : LdapExtendedOperation
	{
		// Token: 0x06000425 RID: 1061 RVA: 0x00013960 File Offset: 0x00011B60
		public PartitionSyncRequest(string serverName, string partitionRoot, int delay)
			: base("2.16.840.1.113719.1.27.100.25", null)
		{
			try
			{
				if (serverName == null || partitionRoot == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1OctetString(serverName);
				Asn1OctetString asn1OctetString = new Asn1OctetString(partitionRoot);
				Asn1Integer asn1Integer = new Asn1Integer(delay);
				asn1Object.encode(lberencoder, memoryStream);
				asn1OctetString.encode(lberencoder, memoryStream);
				asn1Integer.encode(lberencoder, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
