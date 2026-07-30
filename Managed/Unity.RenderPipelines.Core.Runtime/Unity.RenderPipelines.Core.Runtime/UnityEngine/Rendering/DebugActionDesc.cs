using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000033 RID: 51
	internal class DebugActionDesc
	{
		// Token: 0x040000EB RID: 235
		public List<string[]> buttonTriggerList = new List<string[]>();

		// Token: 0x040000EC RID: 236
		public string axisTrigger = "";

		// Token: 0x040000ED RID: 237
		public List<KeyCode[]> keyTriggerList = new List<KeyCode[]>();

		// Token: 0x040000EE RID: 238
		public DebugActionRepeatMode repeatMode;

		// Token: 0x040000EF RID: 239
		public float repeatDelay;
	}
}
