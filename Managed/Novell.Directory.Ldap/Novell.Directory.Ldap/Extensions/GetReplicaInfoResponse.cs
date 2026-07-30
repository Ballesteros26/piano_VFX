using System;
using System.IO;
using Novell.Directory.Ldap.Asn1;
using Novell.Directory.Ldap.Rfc2251;

namespace Novell.Directory.Ldap.Extensions
{
	// Token: 0x0200008B RID: 139
	public class GetReplicaInfoResponse : LdapExtendedResponse
	{
		// Token: 0x06000406 RID: 1030 RVA: 0x00012B50 File Offset: 0x00010D50
		public GetReplicaInfoResponse(RfcLdapMessage rfcMessage)
			: base(rfcMessage)
		{
			if (this.ResultCode != 0)
			{
				this.partitionID = 0;
				this.replicaState = 0;
				this.modificationTime = 0;
				this.purgeTime = 0;
				this.localPartitionID = 0;
				this.partitionDN = "";
				this.replicaType = 0;
				this.flags = 0;
				return;
			}
			sbyte[] value = this.Value;
			if (value == null)
			{
				throw new IOException("No returned value");
			}
			LBERDecoder lberdecoder = new LBERDecoder();
			if (lberdecoder == null)
			{
				throw new IOException("Decoding error");
			}
			MemoryStream memoryStream = new MemoryStream(SupportClass.ToByteArray(value));
			Asn1Integer asn1Integer = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer == null)
			{
				throw new IOException("Decoding error");
			}
			this.partitionID = asn1Integer.intValue();
			Asn1Integer asn1Integer2 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer2 == null)
			{
				throw new IOException("Decoding error");
			}
			this.replicaState = asn1Integer2.intValue();
			Asn1Integer asn1Integer3 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer3 == null)
			{
				throw new IOException("Decoding error");
			}
			this.modificationTime = asn1Integer3.intValue();
			Asn1Integer asn1Integer4 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer4 == null)
			{
				throw new IOException("Decoding error");
			}
			this.purgeTime = asn1Integer4.intValue();
			Asn1Integer asn1Integer5 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer5 == null)
			{
				throw new IOException("Decoding error");
			}
			this.localPartitionID = asn1Integer5.intValue();
			Asn1OctetString asn1OctetString = (Asn1OctetString)lberdecoder.decode(memoryStream);
			if (asn1OctetString == null)
			{
				throw new IOException("Decoding error");
			}
			this.partitionDN = asn1OctetString.stringValue();
			if (this.partitionDN == null)
			{
				throw new IOException("Decoding error");
			}
			Asn1Integer asn1Integer6 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer6 == null)
			{
				throw new IOException("Decoding error");
			}
			this.replicaType = asn1Integer6.intValue();
			Asn1Integer asn1Integer7 = (Asn1Integer)lberdecoder.decode(memoryStream);
			if (asn1Integer7 == null)
			{
				throw new IOException("Decoding error");
			}
			this.flags = asn1Integer7.intValue();
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00012D3C File Offset: 0x00010F3C
		public virtual int getpartitionID()
		{
			return this.partitionID;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00012D44 File Offset: 0x00010F44
		public virtual int getreplicaState()
		{
			return this.replicaState;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00012D4C File Offset: 0x00010F4C
		public virtual int getmodificationTime()
		{
			return this.modificationTime;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00012D54 File Offset: 0x00010F54
		public virtual int getpurgeTime()
		{
			return this.purgeTime;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00012D5C File Offset: 0x00010F5C
		public virtual int getlocalPartitionID()
		{
			return this.localPartitionID;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00012D64 File Offset: 0x00010F64
		public virtual string getpartitionDN()
		{
			return this.partitionDN;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x00012D6C File Offset: 0x00010F6C
		public virtual int getreplicaType()
		{
			return this.replicaType;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00012D74 File Offset: 0x00010F74
		public virtual int getflags()
		{
			return this.flags;
		}

		// Token: 0x04000257 RID: 599
		private int partitionID;

		// Token: 0x04000258 RID: 600
		private int replicaState;

		// Token: 0x04000259 RID: 601
		private int modificationTime;

		// Token: 0x0400025A RID: 602
		private int purgeTime;

		// Token: 0x0400025B RID: 603
		private int localPartitionID;

		// Token: 0x0400025C RID: 604
		private string partitionDN;

		// Token: 0x0400025D RID: 605
		private int replicaType;

		// Token: 0x0400025E RID: 606
		private int flags;
	}
}
