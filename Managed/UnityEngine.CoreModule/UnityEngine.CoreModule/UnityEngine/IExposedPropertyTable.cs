using System;

namespace UnityEngine
{
	// Token: 0x020000C2 RID: 194
	public interface IExposedPropertyTable
	{
		// Token: 0x060004A7 RID: 1191
		void SetReferenceValue(PropertyName id, Object value);

		// Token: 0x060004A8 RID: 1192
		Object GetReferenceValue(PropertyName id, out bool idValid);

		// Token: 0x060004A9 RID: 1193
		void ClearReferenceValue(PropertyName id);
	}
}
