using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200009F RID: 159
	public class SetReplicationFilterRequest : LdapExtendedOperation
	{
		// Token: 0x0600042D RID: 1069 RVA: 0x00013CC4 File Offset: 0x00011EC4
		public SetReplicationFilterRequest(string serverDN, string[][] replicationFilter)
			: base("2.16.840.1.113719.1.27.100.35", null)
		{
			try
			{
				if (serverDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				new Asn1OctetString(serverDN).encode(lberencoder, memoryStream);
				Asn1SequenceOf asn1SequenceOf = new Asn1SequenceOf();
				if (replicationFilter == null)
				{
					asn1SequenceOf.encode(lberencoder, memoryStream);
					this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
				}
				else
				{
					int num = 0;
					while (num < replicationFilter.Length && replicationFilter[num] != null)
					{
						Asn1Sequence asn1Sequence = new Asn1Sequence();
						asn1Sequence.add(new Asn1OctetString(replicationFilter[num][0]));
						Asn1SequenceOf asn1SequenceOf2 = new Asn1SequenceOf();
						int num2 = 1;
						while (num2 < replicationFilter[num].Length && replicationFilter[num][num2] != null)
						{
							asn1SequenceOf2.add(new Asn1OctetString(replicationFilter[num][num2]));
							num2++;
						}
						asn1Sequence.add(asn1SequenceOf2);
						asn1SequenceOf.add(asn1Sequence);
						num++;
					}
					asn1SequenceOf.encode(lberencoder, memoryStream);
					this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
				}
			}
			catch (IOException)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
		}
	}
}
