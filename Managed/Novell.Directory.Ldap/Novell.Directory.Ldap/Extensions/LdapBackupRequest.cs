using System;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Asn1;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200008E RID: 142
	public class LdapBackupRequest : LdapExtendedOperation
	{
		// Token: 0x06000413 RID: 1043 RVA: 0x00012FE4 File Offset: 0x000111E4
		static LdapBackupRequest()
		{
			try
			{
				LdapExtendedResponse.register("2.16.840.1.113719.1.27.100.97", Type.GetType("Novell.Directory.Ldap.Extensions.LdapBackupResponse"));
			}
			catch (TypeLoadException)
			{
				Console.Error.WriteLine("Could not register Extended Response - Class not found");
			}
			catch (Exception ex)
			{
				Console.Error.WriteLine(ex.StackTrace);
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00013048 File Offset: 0x00011248
		public LdapBackupRequest(string objectDN, byte[] passwd, string stateInfo)
			: base("2.16.840.1.113719.1.27.100.96", null)
		{
			try
			{
				if (objectDN == null)
				{
					throw new ArgumentException("PARAM_ERROR");
				}
				if (passwd == null)
				{
					passwd = Encoding.UTF8.GetBytes("");
				}
				int num;
				int num2;
				if (stateInfo == null)
				{
					num = 0;
					num2 = 0;
				}
				else
				{
					stateInfo = stateInfo.Trim();
					int num3 = stateInfo.IndexOf('+');
					if (num3 == -1)
					{
						throw new ArgumentException("PARAM_ERROR");
					}
					string text = stateInfo.Substring(0, num3);
					string text2 = stateInfo.Substring(num3 + 1);
					try
					{
						num = int.Parse(text);
					}
					catch (FormatException)
					{
						throw new LdapLocalException("Invalid Modification Timestamp send in the request", 83);
					}
					try
					{
						num2 = int.Parse(text2);
					}
					catch (FormatException)
					{
						throw new LdapLocalException("Invalid Revision send in the request", 83);
					}
				}
				MemoryStream memoryStream = new MemoryStream();
				LBEREncoder lberencoder = new LBEREncoder();
				Asn1Object asn1Object = new Asn1OctetString(objectDN);
				Asn1Integer asn1Integer = new Asn1Integer(num);
				Asn1Integer asn1Integer2 = new Asn1Integer(num2);
				Asn1OctetString asn1OctetString = new Asn1OctetString(SupportClass.ToSByteArray(passwd));
				asn1Object.encode(lberencoder, memoryStream);
				asn1Integer.encode(lberencoder, memoryStream);
				asn1Integer2.encode(lberencoder, memoryStream);
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
