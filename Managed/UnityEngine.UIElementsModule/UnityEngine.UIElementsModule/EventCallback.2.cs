using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200013F RID: 319
	// (Invoke) Token: 0x0600091C RID: 2332
	public delegate void EventCallback<in TEventType, in TCallbackArgs>(TEventType evt, TCallbackArgs userArgs);
}
