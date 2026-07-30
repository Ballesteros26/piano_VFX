using System;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x02000396 RID: 918
	[RequiredByNativeCode]
	public interface INotificationReceiver
	{
		// Token: 0x06001FF3 RID: 8179
		[RequiredByNativeCode]
		void OnNotify(Playable origin, INotification notification, object context);
	}
}
