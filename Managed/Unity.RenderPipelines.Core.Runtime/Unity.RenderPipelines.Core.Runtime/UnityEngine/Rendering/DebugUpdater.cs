using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000038 RID: 56
	internal class DebugUpdater : MonoBehaviour
	{
		// Token: 0x0600015D RID: 349 RVA: 0x00007701 File Offset: 0x00005901
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		private static void RuntimeInit()
		{
			if (!Debug.isDebugBuild || Object.FindObjectOfType<DebugUpdater>() != null)
			{
				return;
			}
			GameObject gameObject = new GameObject();
			gameObject.name = "[Debug Updater]";
			gameObject.AddComponent<DebugUpdater>();
			Object.DontDestroyOnLoad(gameObject);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007734 File Offset: 0x00005934
		private void Update()
		{
			DebugManager.instance.UpdateActions();
			if (DebugManager.instance.GetAction(DebugAction.EnableDebugMenu) != 0f)
			{
				DebugManager.instance.displayRuntimeUI = !DebugManager.instance.displayRuntimeUI;
			}
			if (DebugManager.instance.displayRuntimeUI && DebugManager.instance.GetAction(DebugAction.ResetAll) != 0f)
			{
				DebugManager.instance.Reset();
			}
		}
	}
}
