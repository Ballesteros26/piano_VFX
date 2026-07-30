using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200008A RID: 138
	public class GetReplicaInfoRequest : LdapExtendedOperation
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x00012A84 File Offset: 0x00010C84
		static GetReplicaInfoRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.18", Type.GetType("Novell.Directory.Ldap.Extensions.GetReplicaInfoResponse"));
			}
			catch (Exception)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00012ACC File Offset: 0x00010CCC
		public GetReplicaInfoRequest(string serverDN, string partitionDN)
			: base("2.16.840.1.113719.1.27.100.17", null)
		{
			try
			{
				if (serverDN == null || partitionDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1OctetString(serverDN);
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
