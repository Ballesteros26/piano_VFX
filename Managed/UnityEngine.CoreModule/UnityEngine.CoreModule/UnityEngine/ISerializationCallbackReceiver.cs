using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001D4 RID: 468
	[RequiredByNativeCode]
	public interface ISerializationCallbackReceiver
	{
		// Token: 0x06001479 RID: 5241
		[RequiredByNativeCode]
		void OnBeforeSerialize();

		// Token: 0x0600147A RID: 5242
		[RequiredByNativeCode]
		void OnAfterDeserialize();
	}
}
