using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x020006F8 RID: 1784
	internal class ValueTypeFixupInfo
	{
		// Token: 0x06004B15 RID: 19221 RVA: 0x0010C6FC File Offset: 0x0010A8FC
		public ValueTypeFixupInfo(long containerID, FieldInfo member, int[] parentIndex)
		{
			if (member == null && parentIndex == null)
			{
				throw new ArgumentException(Environment.GetResourceString("When supplying the ID of a containing object, the FieldInfo that identifies the current field within that object must also be supplied."));
			}
			if (containerID == 0L && member == null)
			{
				this.m_containerID = containerID;
				this.m_parentField = member;
				this.m_parentIndex = parentIndex;
			}
			if (member != null)
			{
				if (parentIndex != null)
				{
					throw new ArgumentException(Environment.GetResourceString("Cannot supply both a MemberInfo and an Array to indicate the parent of a value type."));
				}
				if (member.FieldType.IsValueType && containerID == 0L)
				{
					throw new ArgumentException(Environment.GetResourceString("When supplying a FieldInfo for fixing up a nested type, a valid ID for that containing object must also be supplied."));
				}
			}
			this.m_containerID = containerID;
			this.m_parentField = member;
			this.m_parentIndex = parentIndex;
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x06004B16 RID: 19222 RVA: 0x0010C79D File Offset: 0x0010A99D
		public long ContainerID
		{
			get
			{
				return this.m_containerID;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x06004B17 RID: 19223 RVA: 0x0010C7A5 File Offset: 0x0010A9A5
		public FieldInfo ParentField
		{
			get
			{
				return this.m_parentField;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x06004B18 RID: 19224 RVA: 0x0010C7AD File Offset: 0x0010A9AD
		public int[] ParentIndex
		{
			get
			{
				return this.m_parentIndex;
			}
		}

		// Token: 0x04002733 RID: 10035
		private long m_containerID;

		// Token: 0x04002734 RID: 10036
		private FieldInfo m_parentField;

		// Token: 0x04002735 RID: 10037
		private int[] m_parentIndex;
	}
}
