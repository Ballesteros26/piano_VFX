using System;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002A RID: 42
	[AssetFileNameExtension("signal", new string[] { })]
	public class SignalAsset : ScriptableObject
	{
		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600022F RID: 559 RVA: 0x00007FDC File Offset: 0x000061DC
		// (remove) Token: 0x06000230 RID: 560 RVA: 0x00008010 File Offset: 0x00006210
		internal static event Action<SignalAsset> OnEnableCallback;

		// Token: 0x06000231 RID: 561 RVA: 0x00008043 File Offset: 0x00006243
		private void OnEnable()
		{
			if (SignalAsset.OnEnableCallback != null)
			{
				SignalAsset.OnEnableCallback(this);
			}
		}
	}
}
