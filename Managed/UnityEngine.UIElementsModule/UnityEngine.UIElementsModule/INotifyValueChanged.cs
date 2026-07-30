using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000CF RID: 207
	public interface INotifyValueChanged<T>
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060005DC RID: 1500
		// (set) Token: 0x060005DD RID: 1501
		T value { get; set; }

		// Token: 0x060005DE RID: 1502
		void SetValueWithoutNotify(T newValue);
	}
}
