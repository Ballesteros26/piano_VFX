using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x020000A1 RID: 161
	public class SplitPartitionRequest : LdapExtendedOperation
	{
		// Token: 0x0600042F RID: 1071 RVA: 0x00013E5C File Offset: 0x0001205C
		public SplitPartitionRequest(string dn, int flags)
			: base("2.16.840.1.113719.1.27.100.3", null)
		{
			try
			{
				if (dn == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1Integer(flags);
				Asn1OctetString asn1OctetString = new Asn1OctetString(dn);
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
