using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x02000082 RID: 130
	public class AbortPartitionOperationRequest : LdapExtendedOperation
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x0001266C File Offset: 0x0001086C
		public AbortPartitionOperationRequest(string partitionDN, int flags)
			: base("2.16.840.1.113719.1.27.100.29", null)
		{
			try
			{
				if (partitionDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1Integer(flags);
				Asn1OctetString asn1OctetString = new Asn1OctetString(partitionDN);
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
