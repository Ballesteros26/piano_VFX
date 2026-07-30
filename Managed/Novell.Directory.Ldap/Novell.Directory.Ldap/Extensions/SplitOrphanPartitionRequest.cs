using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A0 RID: 160
	public class SplitOrphanPartitionRequest : LdapExtendedOperation
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x00013DD8 File Offset: 0x00011FD8
		public SplitOrphanPartitionRequest(string serverDN, string contextName)
			: base("2.16.840.1.113719.1.27.100.39", null)
		{
			try
			{
				if (serverDN == null || contextName == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1OctetString(serverDN);
				Asn1OctetString asn1OctetString = new Asn1OctetString(contextName);
				asn1Object.encode(lberencoder, memoryStream);
				asn1OctetString.encode(lberencoder, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
