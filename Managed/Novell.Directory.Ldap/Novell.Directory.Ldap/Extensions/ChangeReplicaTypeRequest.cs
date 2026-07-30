using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000085 RID: 133
	public class ChangeReplicaTypeRequest : LdapExtendedOperation
	{
		// Token: 0x060003FB RID: 1019 RVA: 0x000127A0 File Offset: 0x000109A0
		public ChangeReplicaTypeRequest(string dn, string serverDN, int replicaType, int flags)
			: base("2.16.840.1.113719.1.27.100.15", null)
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
				Asn1Integer asn1Integer = new Asn1Integer(replicaType);
				Asn1OctetString asn1OctetString = new Asn1OctetString(serverDN);
				Asn1OctetString asn1OctetString2 = new Asn1OctetString(dn);
				asn1Object.encode(lberencoder, memoryStream);
				asn1Integer.encode(lberencoder, memoryStream);
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
