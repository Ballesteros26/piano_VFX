using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200017F RID: 383
	internal class SceneObjectIDMap
	{
		// Token: 0x06000AE4 RID: 2788 RVA: 0x00054164 File Offset: 0x00052364
		public static bool TryGetSceneObjectID<TCategory>(GameObject gameObject, out int index, out TCategory category) where TCategory : struct, IConvertible
		{
			if (!typeof(TCategory).IsEnum)
			{
				throw new ArgumentException("'TCategory' must be an Enum type.");
			}
			if (gameObject == null)
			{
				throw new ArgumentNullException("gameObject");
			}
			index = 0;
			category = default(TCategory);
			SceneObjectIDMapSceneAsset sceneObjectIDMapSceneAsset;
			return SceneObjectIDMap.TryGetOrCreateSceneIDMapFor(gameObject.scene, out sceneObjectIDMapSceneAsset) && sceneObjectIDMapSceneAsset.TryGetSceneIDFor<TCategory>(gameObject, out index, out category);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000541C8 File Offset: 0x000523C8
		public static int GetOrCreateSceneObjectID<TCategory>(GameObject gameObject, TCategory category) where TCategory : struct, IConvertible
		{
			if (!typeof(TCategory).IsEnum)
			{
				throw new ArgumentException("'TCategory' must be an Enum type.");
			}
			if (gameObject == null)
			{
				throw new ArgumentNullException("gameObject");
			}
			SceneObjectIDMapSceneAsset sceneObjectIDMapSceneAsset;
			if (!SceneObjectIDMap.TryGetOrCreateSceneIDMapFor(gameObject.scene, out sceneObjectIDMapSceneAsset))
			{
				throw new ArgumentException(string.Format("Provided GameObject {0} does not belong to a loaded scene.", gameObject));
			}
			int num;
			TCategory tcategory;
			if (!sceneObjectIDMapSceneAsset.TryGetSceneIDFor<TCategory>(gameObject, out num, out tcategory))
			{
				sceneObjectIDMapSceneAsset.TryInsert<TCategory>(gameObject, category, out num);
			}
			return num;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00054240 File Offset: 0x00052440
		public static void GetAllIDsForAllScenes<TCategory>(TCategory category, List<GameObject> outGameObjects, List<int> outIndices, List<Scene> outScenes) where TCategory : struct, IConvertible
		{
			if (outGameObjects == null)
			{
				throw new ArgumentNullException("outGameObjects");
			}
			if (outIndices == null)
			{
				throw new ArgumentNullException("outIndices");
			}
			if (outIndices == null)
			{
				throw new ArgumentNullException("outScenes");
			}
			int count = outGameObjects.Count;
			for (int i = 0; i < SceneManager.sceneCount; i++)
			{
				Scene sceneAt = SceneManager.GetSceneAt(i);
				SceneObjectIDMap.GetAllIDsFor<TCategory>(category, sceneAt, outGameObjects, outIndices);
				int j = 0;
				int num = outGameObjects.Count - count;
				while (j < num)
				{
					outScenes.Add(sceneAt);
					j++;
				}
			}
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000542BC File Offset: 0x000524BC
		public static void GetAllIDsFor<TCategory>(TCategory category, Scene scene, List<GameObject> outGameObjects, List<int> outIndices) where TCategory : struct, IConvertible
		{
			if (outGameObjects == null)
			{
				throw new ArgumentNullException("outGameObjects");
			}
			if (outIndices == null)
			{
				throw new ArgumentNullException("outIndices");
			}
			SceneObjectIDMapSceneAsset sceneObjectIDMapSceneAsset;
			if (SceneObjectIDMap.TryGetSceneIDMapFor(scene, out sceneObjectIDMapSceneAsset))
			{
				sceneObjectIDMapSceneAsset.GetALLIDsFor<TCategory>(category, outGameObjects, outIndices);
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000542F8 File Offset: 0x000524F8
		private static bool TryGetSceneIDMapFor(Scene scene, out SceneObjectIDMapSceneAsset map)
		{
			if (!scene.isLoaded)
			{
				map = null;
				return false;
			}
			GameObject[] rootGameObjects = scene.GetRootGameObjects();
			for (int i = 0; i < rootGameObjects.Length; i++)
			{
				if (rootGameObjects[i].name == "SceneIDMap")
				{
					SceneObjectIDMapSceneAsset component;
					map = (component = rootGameObjects[i].GetComponent<SceneObjectIDMapSceneAsset>());
					if (component != null && !map.Equals(null))
					{
						return true;
					}
				}
			}
			map = null;
			return false;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00054364 File Offset: 0x00052564
		private static SceneObjectIDMapSceneAsset CreateSceneIDMapFor(Scene scene)
		{
			GameObject gameObject = new GameObject("SceneIDMap");
			gameObject.hideFlags = HideFlags.HideInHierarchy | HideFlags.HideInInspector | HideFlags.DontSaveInBuild;
			SceneObjectIDMapSceneAsset sceneObjectIDMapSceneAsset = gameObject.AddComponent<SceneObjectIDMapSceneAsset>();
			SceneManager.MoveGameObjectToScene(gameObject, scene);
			return sceneObjectIDMapSceneAsset;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00054391 File Offset: 0x00052591
		private static bool TryGetOrCreateSceneIDMapFor(Scene scene, out SceneObjectIDMapSceneAsset map)
		{
			if (!scene.isLoaded)
			{
				map = null;
				return false;
			}
			if (!SceneObjectIDMap.TryGetSceneIDMapFor(scene, out map))
			{
				map = SceneObjectIDMap.CreateSceneIDMapFor(scene);
			}
			return true;
		}
	}
}
