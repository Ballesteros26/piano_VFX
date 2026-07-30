using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200009B RID: 155
	public class RemoveReplicaRequest : LdapExtendedOperation
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x00013B20 File Offset: 0x00011D20
		public RemoveReplicaRequest(string dn, string serverDN, int flags)
			: base("2.16.840.1.113719.1.27.100.11", null)
		{
			try
			{
				if (dn == null || serverDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1Integer(flags);
				Asn1OctetString asn1OctetString = new Asn1OctetString(serverDN);
				Asn1OctetString asn1OctetString2 = new Asn1OctetString(dn);
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
