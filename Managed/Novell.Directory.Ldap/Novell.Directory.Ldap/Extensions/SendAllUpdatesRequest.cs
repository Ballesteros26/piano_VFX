using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200009E RID: 158
	public class SendAllUpdatesRequest : LdapExtendedOperation
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x00013C40 File Offset: 0x00011E40
		public SendAllUpdatesRequest(string partitionRoot, string origServerDN)
			: base("2.16.840.1.113719.1.27.100.23", null)
		{
			try
			{
				if (partitionRoot == null || origServerDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1OctetString(partitionRoot);
				Asn1OctetString asn1OctetString = new Asn1OctetString(origServerDN);
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
