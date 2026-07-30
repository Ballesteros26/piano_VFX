using System;

namespace UnityEngine
{
	// Token: 0x020001F5 RID: 501
	public struct DrivenRectTransformTracker
	{
		// Token: 0x06001626 RID: 5670 RVA: 0x00024540 File Offset: 0x00022740
		internal static bool CanRecordModifications()
		{
			return true;
		}

		// Token: 0x06001627 RID: 5671 RVA: 0x00002EC3 File Offset: 0x000010C3
		public void Add(Object driver, RectTransform rectTransform, DrivenTransformProperties drivenProperties)
		{
		}

		// Token: 0x06001628 RID: 5672 RVA: 0x00024553 File Offset: 0x00022753
		[Obsolete("revertValues parameter is ignored. Please use Clear() instead.")]
		public void Clear(bool revertValues)
		{
			this.Clear();
		}

		// Token: 0x06001629 RID: 5673 RVA: 0x00002EC3 File Offset: 0x000010C3
		public void Clear()
		{
		}
	}
}
