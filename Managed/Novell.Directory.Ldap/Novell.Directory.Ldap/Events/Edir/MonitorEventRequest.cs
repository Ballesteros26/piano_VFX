using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000B8 RID: 184
	public class MonitorEventRequest : LdapExtendedOperation
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00014B2C File Offset: 0x00012D2C
		static MonitorEventRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.80", Type.GetType("Novell.Directory.Ldap.Events.Edir.MonitorEventResponse", true));
			}
			catch (TypeLoadException)
			{
			}
			catch (Exception)
			{
			}
			try
			{
				LdapIntermediateResponse.register("2.16.840.1.113719.1.27.100.81", Type.GetType("Novell.Directory.Ldap.Events.Edir.EdirEventIntermediateResponse", true));
			}
			catch (TypeLoadException)
			{
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00014BA8 File Offset: 0x00012DA8
		public MonitorEventRequest(EdirEventSpecifier[] specifiers)
			: base("2.16.840.1.113719.1.27.100.79", null)
		{
			if (specifiers == null)
			{
				throw new ArgumentException("PARAM_ERROR");
			}
			MemoryStream memoryStream = new MemoryStream();
			LBEREncoder lberencoder = new LBEREncoder();
			Asn1Sequence asn1Sequence = new Asn1Sequence();
			try
			{
				asn1Sequence.add(new Asn1Integer(specifiers.Length));
				Asn1Set asn1Set = new Asn1Set();
				bool flag = false;
				for (int i = 0; i < specifiers.Length; i++)
				{
					Asn1Sequence asn1Sequence2 = new Asn1Sequence();
					asn1Sequence2.add(new Asn1Integer((int)specifiers[i].EventType));
					asn1Sequence2.add(new Asn1Enumerated((int)specifiers[i].EventResultType));
					if (i == 0)
					{
						flag = specifiers[i].EventFilter != null;
						if (flag)
						{
							this.setID("2.16.840.1.113719.1.27.100.84");
						}
					}
					if (flag)
					{
						if (specifiers[i].EventFilter == null)
						{
							throw new ArgumentException("Filter cannot be null,for Filter events");
						}
						asn1Sequence2.add(new Asn1OctetString(specifiers[i].EventFilter));
					}
					else if (specifiers[i].EventFilter != null)
					{
						throw new ArgumentException("Filter cannot be specified for non Filter events");
					}
					asn1Set.add(asn1Sequence2);
				}
				asn1Sequence.add(asn1Set);
				asn1Sequence.encode(lberencoder, memoryStream);
			}
			catch (Exception)
			{
				throw new LdapException("ENCODING_ERROR", 83, null);
			}
			this.setValue(SupportClass.ToSByteArray(memoryStream.ToArray()));
		}
	}
}
