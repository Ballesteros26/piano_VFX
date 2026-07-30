using System;
using System.Reflection;

namespace System.Xml.Serialization
{
	// Token: 0x020002E2 RID: 738
	internal class ChoiceIdentifierAccessor : Accessor
	{
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x00099D2E File Offset: 0x00097F2E
		// (set) Token: 0x06001BB3 RID: 7091 RVA: 0x00099D36 File Offset: 0x00097F36
		internal string MemberName
		{
			get
			{
				return this.memberName;
			}
			set
			{
				this.memberName = value;
			}
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001BB4 RID: 7092 RVA: 0x00099D3F File Offset: 0x00097F3F
		// (set) Token: 0x06001BB5 RID: 7093 RVA: 0x00099D47 File Offset: 0x00097F47
		internal string[] MemberIds
		{
			get
			{
				return this.memberIds;
			}
			set
			{
				this.memberIds = value;
			}
		}

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001BB6 RID: 7094 RVA: 0x00099D50 File Offset: 0x00097F50
		// (set) Token: 0x06001BB7 RID: 7095 RVA: 0x00099D58 File Offset: 0x00097F58
		internal MemberInfo MemberInfo
		{
			get
			{
				return this.memberInfo;
			}
			set
			{
				this.memberInfo = value;
			}
		}

		// Token: 0x04001604 RID: 5636
		private string memberName;

		// Token: 0x04001605 RID: 5637
		private string[] memberIds;

		// Token: 0x04001606 RID: 5638
		private MemberInfo memberInfo;
	}
}
