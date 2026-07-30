using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000AAC RID: 2732
	internal sealed class NameInfo : ConcurrentSetItem<KeyValuePair<string, EventTags>, NameInfo>
	{
		// Token: 0x0600633B RID: 25403 RVA: 0x001431DC File Offset: 0x001413DC
		internal static void ReserveEventIDsBelow(int eventId)
		{
			int num;
			int num2;
			do
			{
				num = NameInfo.lastIdentity;
				num2 = (NameInfo.lastIdentity & -16777216) + eventId;
				num2 = Math.Max(num2, num);
			}
			while (Interlocked.CompareExchange(ref NameInfo.lastIdentity, num2, num) != num);
		}

		// Token: 0x0600633C RID: 25404 RVA: 0x00143214 File Offset: 0x00141414
		public NameInfo(string name, EventTags tags, int typeMetadataSize)
		{
			this.name = name;
			this.tags = tags & (EventTags)268435455;
			this.identity = Interlocked.Increment(ref NameInfo.lastIdentity);
			int num = 0;
			Statics.EncodeTags((int)this.tags, ref num, null);
			this.nameMetadata = Statics.MetadataForString(name, num, 0, typeMetadataSize);
			num = 2;
			Statics.EncodeTags((int)this.tags, ref num, this.nameMetadata);
		}

		// Token: 0x0600633D RID: 25405 RVA: 0x0014327F File Offset: 0x0014147F
		public override int Compare(NameInfo other)
		{
			return this.Compare(other.name, other.tags);
		}

		// Token: 0x0600633E RID: 25406 RVA: 0x00143293 File Offset: 0x00141493
		public override int Compare(KeyValuePair<string, EventTags> key)
		{
			return this.Compare(key.Key, key.Value & (EventTags)268435455);
		}

		// Token: 0x0600633F RID: 25407 RVA: 0x001432B0 File Offset: 0x001414B0
		private int Compare(string otherName, EventTags otherTags)
		{
			int num = StringComparer.Ordinal.Compare(this.name, otherName);
			if (num == 0 && this.tags != otherTags)
			{
				num = ((this.tags < otherTags) ? (-1) : 1);
			}
			return num;
		}

		// Token: 0x04003176 RID: 12662
		private static int lastIdentity = 184549376;

		// Token: 0x04003177 RID: 12663
		internal readonly string name;

		// Token: 0x04003178 RID: 12664
		internal readonly EventTags tags;

		// Token: 0x04003179 RID: 12665
		internal readonly int identity;

		// Token: 0x0400317A RID: 12666
		internal readonly byte[] nameMetadata;
	}
}
