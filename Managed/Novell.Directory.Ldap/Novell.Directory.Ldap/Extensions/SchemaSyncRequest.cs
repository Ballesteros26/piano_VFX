using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200009D RID: 157
	public class SchemaSyncRequest : LdapExtendedOperation
	{
		// Token: 0x0600042B RID: 1067 RVA: 0x00013BBC File Offset: 0x00011DBC
		public SchemaSyncRequest(string serverName, int delay)
			: base("2.16.840.1.113719.1.27.100.27", null)
		{
			try
			{
				if (serverName == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1OctetString(serverName);
				Asn1Integer asn1Integer = new Asn1Integer(delay);
				asn1Object.encode(lberencoder, memoryStream);
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
