using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000023 RID: 35
	internal interface IDataWatchService
	{
		// Token: 0x060000B6 RID: 182
		IDataWatchHandle AddWatch(Object watched, Action<Object> onDataChanged);

		// Token: 0x060000B7 RID: 183
		void RemoveWatch(IDataWatchHandle handle);

		// Token: 0x060000B8 RID: 184
		void ForceDirtyNextPoll(Object obj);
	}
}
