using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020006DC RID: 1756
	[Serializable]
	internal class FixupHolder
	{
		// Token: 0x06004A65 RID: 19045 RVA: 0x0010A8FE File Offset: 0x00108AFE
		internal FixupHolder(long id, object fixupInfo, int fixupType)
		{
			this.m_id = id;
			this.m_fixupInfo = fixupInfo;
			this.m_fixupType = fixupType;
		}

		// Token: 0x040026DF RID: 9951
		internal const int ArrayFixup = 1;

		// Token: 0x040026E0 RID: 9952
		internal const int MemberFixup = 2;

		// Token: 0x040026E1 RID: 9953
		internal const int DelayedFixup = 4;

		// Token: 0x040026E2 RID: 9954
		internal long m_id;

		// Token: 0x040026E3 RID: 9955
		internal object m_fixupInfo;

		// Token: 0x040026E4 RID: 9956
		internal int m_fixupType;
	}
}
