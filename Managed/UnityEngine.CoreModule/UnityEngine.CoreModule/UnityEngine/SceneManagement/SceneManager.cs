using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Events;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine.SceneManagement
{
	// Token: 0x02000271 RID: 625
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/SceneManager/SceneManager.bindings.h")]
	public class SceneManager
	{
		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x06001A15 RID: 6677
		public static extern int sceneCount
		{
			[NativeHeader("Runtime/SceneManager/SceneManager.h")]
			[StaticAccessor("GetSceneManager()", StaticAccessorType.Dot)]
			[NativeMethod("GetSceneCount")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001A16 RID: 6678
		public static extern int sceneCountInBuildSettings
		{
			[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
			[NativeMethod("GetNumScenesInBuildSettings")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001A17 RID: 6679 RVA: 0x0002AA48 File Offset: 0x00028C48
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetActiveScene()
		{
			Scene scene;
			SceneManager.GetActiveScene_Injected(out scene);
			return scene;
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0002AA5D File Offset: 0x00028C5D
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public static bool SetActiveScene(Scene scene)
		{
			return SceneManager.SetActiveScene_Injected(ref scene);
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0002AA68 File Offset: 0x00028C68
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneByPath(string scenePath)
		{
			Scene scene;
			SceneManager.GetSceneByPath_Injected(scenePath, out scene);
			return scene;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x0002AA80 File Offset: 0x00028C80
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneByName(string name)
		{
			Scene scene;
			SceneManager.GetSceneByName_Injected(name, out scene);
			return scene;
		}

		// Token: 0x06001A1B RID: 6683 RVA: 0x0002AA98 File Offset: 0x00028C98
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public static Scene GetSceneByBuildIndex(int buildIndex)
		{
			Scene scene;
			SceneManager.GetSceneByBuildIndex_Injected(buildIndex, out scene);
			return scene;
		}

		// Token: 0x06001A1C RID: 6684 RVA: 0x0002AAB0 File Offset: 0x00028CB0
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene GetSceneAt(int index)
		{
			Scene scene;
			SceneManager.GetSceneAt_Injected(index, out scene);
			return scene;
		}

		// Token: 0x06001A1D RID: 6685 RVA: 0x0002AAC8 File Offset: 0x00028CC8
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static Scene CreateScene([NotNull] string sceneName, CreateSceneParameters parameters)
		{
			Scene scene;
			SceneManager.CreateScene_Injected(sceneName, ref parameters, out scene);
			return scene;
		}

		// Token: 0x06001A1E RID: 6686 RVA: 0x0002AAE0 File Offset: 0x00028CE0
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		private static bool UnloadSceneInternal(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneInternal_Injected(ref scene, options);
		}

		// Token: 0x06001A1F RID: 6687 RVA: 0x0002AAEA File Offset: 0x00028CEA
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		private static AsyncOperation UnloadSceneAsyncInternal(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneAsyncInternal_Injected(ref scene, options);
		}

		// Token: 0x06001A20 RID: 6688 RVA: 0x0002AAF4 File Offset: 0x00028CF4
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		private static AsyncOperation LoadSceneAsyncNameIndexInternal(string sceneName, int sceneBuildIndex, LoadSceneParameters parameters, bool mustCompleteNextFrame)
		{
			return SceneManager.LoadSceneAsyncNameIndexInternal_Injected(sceneName, sceneBuildIndex, ref parameters, mustCompleteNextFrame);
		}

		// Token: 0x06001A21 RID: 6689
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		[MethodImpl(4096)]
		private static extern AsyncOperation UnloadSceneNameIndexInternal(string sceneName, int sceneBuildIndex, bool immediately, UnloadSceneOptions options, out bool outSuccess);

		// Token: 0x06001A22 RID: 6690 RVA: 0x0002AB00 File Offset: 0x00028D00
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		[NativeThrows]
		public static void MergeScenes(Scene sourceScene, Scene destinationScene)
		{
			SceneManager.MergeScenes_Injected(ref sourceScene, ref destinationScene);
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0002AB0B File Offset: 0x00028D0B
		[NativeThrows]
		[StaticAccessor("SceneManagerBindings", StaticAccessorType.DoubleColon)]
		public static void MoveGameObjectToScene([NotNull] GameObject go, Scene scene)
		{
			SceneManager.MoveGameObjectToScene_Injected(go, ref scene);
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001A24 RID: 6692 RVA: 0x0002AB18 File Offset: 0x00028D18
		// (remove) Token: 0x06001A25 RID: 6693 RVA: 0x0002AB4C File Offset: 0x00028D4C
		[field: DebuggerBrowsable(0)]
		public static event UnityAction<Scene, LoadSceneMode> sceneLoaded;

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001A26 RID: 6694 RVA: 0x0002AB80 File Offset: 0x00028D80
		// (remove) Token: 0x06001A27 RID: 6695 RVA: 0x0002ABB4 File Offset: 0x00028DB4
		[field: DebuggerBrowsable(0)]
		public static event UnityAction<Scene> sceneUnloaded;

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06001A28 RID: 6696 RVA: 0x0002ABE8 File Offset: 0x00028DE8
		// (remove) Token: 0x06001A29 RID: 6697 RVA: 0x0002AC1C File Offset: 0x00028E1C
		[field: DebuggerBrowsable(0)]
		public static event UnityAction<Scene, Scene> activeSceneChanged;

		// Token: 0x06001A2A RID: 6698 RVA: 0x0002AC50 File Offset: 0x00028E50
		[Obsolete("Use SceneManager.sceneCount and SceneManager.GetSceneAt(int index) to loop the all scenes instead.")]
		public static Scene[] GetAllScenes()
		{
			Scene[] array = new Scene[SceneManager.sceneCount];
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				array[i] = SceneManager.GetSceneAt(i);
			}
			return array;
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x0002AC94 File Offset: 0x00028E94
		public static Scene CreateScene(string sceneName)
		{
			CreateSceneParameters createSceneParameters = new CreateSceneParameters(LocalPhysicsMode.None);
			return SceneManager.CreateScene(sceneName, createSceneParameters);
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x0002ACB8 File Offset: 0x00028EB8
		public static void LoadScene(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(mode);
			SceneManager.LoadScene(sceneName, loadSceneParameters);
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0002ACD8 File Offset: 0x00028ED8
		[ExcludeFromDocs]
		public static void LoadScene(string sceneName)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);
			SceneManager.LoadScene(sceneName, loadSceneParameters);
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0002ACF8 File Offset: 0x00028EF8
		public static Scene LoadScene(string sceneName, LoadSceneParameters parameters)
		{
			SceneManager.LoadSceneAsyncNameIndexInternal(sceneName, -1, parameters, true);
			return SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0002AD20 File Offset: 0x00028F20
		public static void LoadScene(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(mode);
			SceneManager.LoadScene(sceneBuildIndex, loadSceneParameters);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0002AD40 File Offset: 0x00028F40
		[ExcludeFromDocs]
		public static void LoadScene(int sceneBuildIndex)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);
			SceneManager.LoadScene(sceneBuildIndex, loadSceneParameters);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x0002AD60 File Offset: 0x00028F60
		public static Scene LoadScene(int sceneBuildIndex, LoadSceneParameters parameters)
		{
			SceneManager.LoadSceneAsyncNameIndexInternal(null, sceneBuildIndex, parameters, true);
			return SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x0002AD88 File Offset: 0x00028F88
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(mode);
			return SceneManager.LoadSceneAsync(sceneBuildIndex, loadSceneParameters);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0002ADAC File Offset: 0x00028FAC
		[ExcludeFromDocs]
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);
			return SceneManager.LoadSceneAsync(sceneBuildIndex, loadSceneParameters);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0002ADD0 File Offset: 0x00028FD0
		public static AsyncOperation LoadSceneAsync(int sceneBuildIndex, LoadSceneParameters parameters)
		{
			return SceneManager.LoadSceneAsyncNameIndexInternal(null, sceneBuildIndex, parameters, false);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0002ADEC File Offset: 0x00028FEC
		public static AsyncOperation LoadSceneAsync(string sceneName, [DefaultValue("LoadSceneMode.Single")] LoadSceneMode mode)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(mode);
			return SceneManager.LoadSceneAsync(sceneName, loadSceneParameters);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0002AE10 File Offset: 0x00029010
		[ExcludeFromDocs]
		public static AsyncOperation LoadSceneAsync(string sceneName)
		{
			LoadSceneParameters loadSceneParameters = new LoadSceneParameters(LoadSceneMode.Single);
			return SceneManager.LoadSceneAsync(sceneName, loadSceneParameters);
		}

		// Token: 0x06001A37 RID: 6711 RVA: 0x0002AE34 File Offset: 0x00029034
		public static AsyncOperation LoadSceneAsync(string sceneName, LoadSceneParameters parameters)
		{
			return SceneManager.LoadSceneAsyncNameIndexInternal(sceneName, -1, parameters, false);
		}

		// Token: 0x06001A38 RID: 6712 RVA: 0x0002AE50 File Offset: 0x00029050
		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(Scene scene)
		{
			return SceneManager.UnloadSceneInternal(scene, UnloadSceneOptions.None);
		}

		// Token: 0x06001A39 RID: 6713 RVA: 0x0002AE6C File Offset: 0x0002906C
		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(int sceneBuildIndex)
		{
			bool flag;
			SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, true, UnloadSceneOptions.None, out flag);
			return flag;
		}

		// Token: 0x06001A3A RID: 6714 RVA: 0x0002AE90 File Offset: 0x00029090
		[Obsolete("Use SceneManager.UnloadSceneAsync. This function is not safe to use during triggers and under other circumstances. See Scripting reference for more details.")]
		public static bool UnloadScene(string sceneName)
		{
			bool flag;
			SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, true, UnloadSceneOptions.None, out flag);
			return flag;
		}

		// Token: 0x06001A3B RID: 6715 RVA: 0x0002AEB0 File Offset: 0x000290B0
		public static AsyncOperation UnloadSceneAsync(int sceneBuildIndex)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, false, UnloadSceneOptions.None, out flag);
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x0002AED4 File Offset: 0x000290D4
		public static AsyncOperation UnloadSceneAsync(string sceneName)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, false, UnloadSceneOptions.None, out flag);
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x0002AEF4 File Offset: 0x000290F4
		public static AsyncOperation UnloadSceneAsync(Scene scene)
		{
			return SceneManager.UnloadSceneAsyncInternal(scene, UnloadSceneOptions.None);
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x0002AF10 File Offset: 0x00029110
		public static AsyncOperation UnloadSceneAsync(int sceneBuildIndex, UnloadSceneOptions options)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal("", sceneBuildIndex, false, options, out flag);
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x0002AF34 File Offset: 0x00029134
		public static AsyncOperation UnloadSceneAsync(string sceneName, UnloadSceneOptions options)
		{
			bool flag;
			return SceneManager.UnloadSceneNameIndexInternal(sceneName, -1, false, options, out flag);
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x0002AF54 File Offset: 0x00029154
		public static AsyncOperation UnloadSceneAsync(Scene scene, UnloadSceneOptions options)
		{
			return SceneManager.UnloadSceneAsyncInternal(scene, options);
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x0002AF70 File Offset: 0x00029170
		[RequiredByNativeCode]
		private static void Internal_SceneLoaded(Scene scene, LoadSceneMode mode)
		{
			bool flag = SceneManager.sceneLoaded != null;
			if (flag)
			{
				SceneManager.sceneLoaded(scene, mode);
			}
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x0002AF9C File Offset: 0x0002919C
		[RequiredByNativeCode]
		private static void Internal_SceneUnloaded(Scene scene)
		{
			bool flag = SceneManager.sceneUnloaded != null;
			if (flag)
			{
				SceneManager.sceneUnloaded(scene);
			}
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x0002AFC4 File Offset: 0x000291C4
		[RequiredByNativeCode]
		private static void Internal_ActiveSceneChanged(Scene previousActiveScene, Scene newActiveScene)
		{
			bool flag = SceneManager.activeSceneChanged != null;
			if (flag)
			{
				SceneManager.activeSceneChanged(previousActiveScene, newActiveScene);
			}
		}

		// Token: 0x06001A45 RID: 6725
		[MethodImpl(4096)]
		private static extern void GetActiveScene_Injected(out Scene ret);

		// Token: 0x06001A46 RID: 6726
		[MethodImpl(4096)]
		private static extern bool SetActiveScene_Injected(ref Scene scene);

		// Token: 0x06001A47 RID: 6727
		[MethodImpl(4096)]
		private static extern void GetSceneByPath_Injected(string scenePath, out Scene ret);

		// Token: 0x06001A48 RID: 6728
		[MethodImpl(4096)]
		private static extern void GetSceneByName_Injected(string name, out Scene ret);

		// Token: 0x06001A49 RID: 6729
		[MethodImpl(4096)]
		private static extern void GetSceneByBuildIndex_Injected(int buildIndex, out Scene ret);

		// Token: 0x06001A4A RID: 6730
		[MethodImpl(4096)]
		private static extern void GetSceneAt_Injected(int index, out Scene ret);

		// Token: 0x06001A4B RID: 6731
		[MethodImpl(4096)]
		private static extern void CreateScene_Injected(string sceneName, ref CreateSceneParameters parameters, out Scene ret);

		// Token: 0x06001A4C RID: 6732
		[MethodImpl(4096)]
		private static extern bool UnloadSceneInternal_Injected(ref Scene scene, UnloadSceneOptions options);

		// Token: 0x06001A4D RID: 6733
		[MethodImpl(4096)]
		private static extern AsyncOperation UnloadSceneAsyncInternal_Injected(ref Scene scene, UnloadSceneOptions options);

		// Token: 0x06001A4E RID: 6734
		[MethodImpl(4096)]
		private static extern AsyncOperation LoadSceneAsyncNameIndexInternal_Injected(string sceneName, int sceneBuildIndex, ref LoadSceneParameters parameters, bool mustCompleteNextFrame);

		// Token: 0x06001A4F RID: 6735
		[MethodImpl(4096)]
		private static extern void MergeScenes_Injected(ref Scene sourceScene, ref Scene destinationScene);

		// Token: 0x06001A50 RID: 6736
		[MethodImpl(4096)]
		private static extern void MoveGameObjectToScene_Injected(GameObject go, ref Scene scene);
	}
}
