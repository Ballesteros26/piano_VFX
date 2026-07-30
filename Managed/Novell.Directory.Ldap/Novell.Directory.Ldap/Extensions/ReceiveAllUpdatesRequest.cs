using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000098 RID: 152
	public class ReceiveAllUpdatesRequest : LdapExtendedOperation
	{
		// Token: 0x06000426 RID: 1062 RVA: 0x000139F4 File Offset: 0x00011BF4
		public ReceiveAllUpdatesRequest(string partitionRoot, string toServerDN, string fromServerDN)
			: base("2.16.840.1.113719.1.27.100.21", null)
		{
			try
			{
				if (partitionRoot == null || toServerDN == null || fromServerDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1OctetString(partitionRoot);
				Asn1OctetString asn1OctetString = new Asn1OctetString(toServerDN);
				Asn1OctetString asn1OctetString2 = new Asn1OctetString(fromServerDN);
				asn1Object.encode(lberencoder, memoryStream);
				asn1OctetString.encode(lberencoder, memoryStream);
				asn1OctetString2.encode(lberencoder, memoryStream);
				this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
