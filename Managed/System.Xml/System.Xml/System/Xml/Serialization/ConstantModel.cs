using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x020002FD RID: 765
	internal class ConstantModel
	{
		// Token: 0x06001C87 RID: 7303 RVA: 0x0009BC0F File Offset: 0x00099E0F
		internal ConstantModel(FieldInfo fieldInfo, long value)
		{
			this.fieldInfo = fieldInfo;
			this.value = value;
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001C88 RID: 7304 RVA: 0x0009BC25 File Offset: 0x00099E25
		internal string Name
		{
			get
			{
				return this.fieldInfo.Name;
			}
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x0009BC32 File Offset: 0x00099E32
		internal long Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001C8A RID: 7306 RVA: 0x0009BC3A File Offset: 0x00099E3A
		internal FieldInfo FieldInfo
		{
			get
			{
				return this.fieldInfo;
			}
		}

		// Token: 0x0400165F RID: 5727
		private FieldInfo fieldInfo;

		// Token: 0x04001660 RID: 5728
		private long value;
	}
}
