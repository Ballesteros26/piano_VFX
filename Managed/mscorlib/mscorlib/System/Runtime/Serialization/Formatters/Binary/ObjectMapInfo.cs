using System;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x02000730 RID: 1840
	internal sealed class ObjectMapInfo
	{
		// Token: 0x06004C3A RID: 19514 RVA: 0x00110419 File Offset: 0x0010E619
		internal ObjectMapInfo(int objectId, int numMembers, string[] memberNames, Type[] memberTypes)
		{
			this.objectId = objectId;
			this.numMembers = numMembers;
			this.memberNames = memberNames;
			this.memberTypes = memberTypes;
		}

		// Token: 0x06004C3B RID: 19515 RVA: 0x00110440 File Offset: 0x0010E640
		internal bool isCompatible(int numMembers, string[] memberNames, Type[] memberTypes)
		{
			bool flag = true;
			if (this.numMembers == numMembers)
			{
				for (int i = 0; i < numMembers; i++)
				{
					if (!this.memberNames[i].Equals(memberNames[i]))
					{
						flag = false;
						break;
					}
					if (memberTypes != null && this.memberTypes[i] != memberTypes[i])
					{
						flag = false;
						break;
					}
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0400289B RID: 10395
		internal int objectId;

		// Token: 0x0400289C RID: 10396
		private int numMembers;

		// Token: 0x0400289D RID: 10397
		private string[] memberNames;

		// Token: 0x0400289E RID: 10398
		private Type[] memberTypes;
	}
}
