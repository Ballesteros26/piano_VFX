using System;
using System.Diagnostics;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004F RID: 79
	internal static class TimelineUndo
	{
		// Token: 0x060002E2 RID: 738 RVA: 0x0000A59D File Offset: 0x0000879D
		public static void PushDestroyUndo(TimelineAsset timeline, Object thingToDirty, Object objectToDestroy, string operation)
		{
			if (objectToDestroy != null)
			{
				Object.Destroy(objectToDestroy);
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000028DC File Offset: 0x00000ADC
		[Conditional("UNITY_EDITOR")]
		public static void PushUndo(Object thingToDirty, string operation)
		{
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000028DC File Offset: 0x00000ADC
		[Conditional("UNITY_EDITOR")]
		public static void RegisterCreatedObjectUndo(Object thingCreated, string operation)
		{
		}
	}
}
