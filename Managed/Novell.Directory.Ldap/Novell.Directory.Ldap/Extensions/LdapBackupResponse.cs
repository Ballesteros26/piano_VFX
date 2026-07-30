using System;
using System.IO;
using System.Text;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200008F RID: 143
	public class LdapBackupResponse : LdapExtendedResponse
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x000131BC File Offset: 0x000113BC
		public LdapBackupResponse(RfcLdapMessage rfcMessage)
			: base(rfcMessage)
		{
			if (this.ID == null || !this.ID.Equals("2.16.840.1.113719.1.27.100.97"))
			{
				throw new IOException("LDAP Extended Operation not supported");
			}
			if (this.ResultCode != 0)
			{
				this.bufferLength = 0;
				this.stateInfo = null;
				this.chunkSizesString = null;
				this.returnedBuffer = null;
				return;
			}
			byte[] array = SupportClass.ToByteArray(this.Value);
			if (array == null)
			{
				throw new Exception("LDAP Operations error. No returned value.");
			}
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new Exception("Decoding error");
			}
			MemoryStream memoryStream = new MemoryStream(array);
			Asn1Integer asn1Integer = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer == null)
			{
				throw new IOException("Decoding error");
			}
			this.bufferLength = asn1Integer.intValue();
			Asn1Integer asn1Integer2 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer2 == null)
			{
				throw new IOException("Decoding error");
			}
			int num = asn1Integer2.intValue();
			Asn1Integer asn1Integer3 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer3 == null)
			{
				throw new IOException("Decoding error");
			}
			int num2 = asn1Integer3.intValue();
			this.stateInfo = num + "+" + num2;
			Asn1OctetString asn1OctetString = (Asn1OctetString)lberdecoder.decode(memoryStream);
			if (asn1OctetString == null)
			{
				throw new IOException("Decoding error");
			}
			this.returnedBuffer = SupportClass.ToByteArray(asn1OctetString.byteValue());
			Asn1Sequence asn1Sequence = (Asn1Sequence)lberdecoder.decode(memoryStream);
			if (asn1Sequence == null)
			{
				throw new IOException("Decoding error");
			}
			int num3 = ((Asn1Integer)asn1Sequence.get_Renamed(0)).intValue();
			int[] array2 = new int[num3];
			Asn1Set asn1Set = (Asn1Set)asn1Sequence.get_Renamed(1);
			for (int i = 0; i < num3; i++)
			{
				Asn1Sequence asn1Sequence2 = (Asn1Sequence)asn1Set.get_Renamed(i);
				array2[i] = ((Asn1Integer)asn1Sequence2.get_Renamed(0)).intValue();
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(num3);
			stringBuilder.Append(";");
			int j;
			for (j = 0; j < num3 - 1; j++)
			{
				stringBuilder.Append(array2[j]);
				stringBuilder.Append(";");
			}
			stringBuilder.Append(array2[j]);
			this.chunkSizesString = stringBuilder.ToString();
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000133ED File Offset: 0x000115ED
		public int getBufferLength()
		{
			return this.bufferLength;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000133F5 File Offset: 0x000115F5
		public string getStatusInfo()
		{
			return this.stateInfo;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000133FD File Offset: 0x000115FD
		public string getChunkSizesString()
		{
			return this.chunkSizesString;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00013405 File Offset: 0x00011605
		public byte[] getReturnedBuffer()
		{
			return this.returnedBuffer;
		}

		// Token: 0x04000260 RID: 608
		private int bufferLength;

		// Token: 0x04000261 RID: 609
		private string stateInfo;

		// Token: 0x04000262 RID: 610
		private string chunkSizesString;

		// Token: 0x04000263 RID: 611
		private byte[] returnedBuffer;
	}
}
