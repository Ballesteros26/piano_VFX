using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000190 RID: 400
	[NativeHeader("Runtime/Export/Resources/Resources.bindings.h")]
	[NativeHeader("Runtime/Misc/ResourceManagerUtility.h")]
	public sealed class Resources
	{
		// Token: 0x060012CB RID: 4811 RVA: 0x0001ED88 File Offset: 0x0001CF88
		internal static T[] ConvertObjects<T>(Object[] rawObjects) where T : Object
		{
			bool flag = rawObjects == null;
			T[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				T[] array2 = new T[rawObjects.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					array2[i] = (T)((object)rawObjects[i]);
				}
				array = array2;
			}
			return array;
		}

		// Token: 0x060012CC RID: 4812
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[FreeFunction("Resources_Bindings::FindObjectsOfTypeAll")]
		[MethodImpl(4096)]
		public static extern Object[] FindObjectsOfTypeAll(Type type);

		// Token: 0x060012CD RID: 4813 RVA: 0x0001EDD4 File Offset: 0x0001CFD4
		public static T[] FindObjectsOfTypeAll<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(Resources.FindObjectsOfTypeAll(typeof(T)));
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x0001EDFC File Offset: 0x0001CFFC
		public static Object Load(string path)
		{
			return Resources.Load(path, typeof(Object));
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x0001EE20 File Offset: 0x0001D020
		public static T Load<T>(string path) where T : Object
		{
			return (T)((object)Resources.Load(path, typeof(T)));
		}

		// Token: 0x060012D0 RID: 4816
		[FreeFunction("Resources_Bindings::Load")]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedBySecondArgument)]
		[NativeThrows]
		[MethodImpl(4096)]
		public static extern Object Load(string path, [NotNull] Type systemTypeInstance);

		// Token: 0x060012D1 RID: 4817 RVA: 0x0001EE48 File Offset: 0x0001D048
		public static ResourceRequest LoadAsync(string path)
		{
			return Resources.LoadAsync(path, typeof(Object));
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0001EE6C File Offset: 0x0001D06C
		public static ResourceRequest LoadAsync<T>(string path) where T : Object
		{
			return Resources.LoadAsync(path, typeof(T));
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0001EE90 File Offset: 0x0001D090
		public static ResourceRequest LoadAsync(string path, Type type)
		{
			ResourceRequest resourceRequest = Resources.LoadAsyncInternal(path, type);
			resourceRequest.m_Path = path;
			resourceRequest.m_Type = type;
			return resourceRequest;
		}

		// Token: 0x060012D4 RID: 4820
		[FreeFunction("Resources_Bindings::LoadAsyncInternal")]
		[MethodImpl(4096)]
		internal static extern ResourceRequest LoadAsyncInternal(string path, Type type);

		// Token: 0x060012D5 RID: 4821
		[NativeThrows]
		[FreeFunction("Resources_Bindings::LoadAll")]
		[MethodImpl(4096)]
		public static extern Object[] LoadAll([NotNull] string path, [NotNull] Type systemTypeInstance);

		// Token: 0x060012D6 RID: 4822 RVA: 0x0001EEBC File Offset: 0x0001D0BC
		public static Object[] LoadAll(string path)
		{
			return Resources.LoadAll(path, typeof(Object));
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x0001EEE0 File Offset: 0x0001D0E0
		public static T[] LoadAll<T>(string path) where T : Object
		{
			return Resources.ConvertObjects<T>(Resources.LoadAll(path, typeof(T)));
		}

		// Token: 0x060012D8 RID: 4824
		[FreeFunction("GetScriptingBuiltinResource", ThrowsException = true)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[MethodImpl(4096)]
		public static extern Object GetBuiltinResource([NotNull] Type type, string path);

		// Token: 0x060012D9 RID: 4825 RVA: 0x0001EF08 File Offset: 0x0001D108
		public static T GetBuiltinResource<T>(string path) where T : Object
		{
			return (T)((object)Resources.GetBuiltinResource(typeof(T), path));
		}

		// Token: 0x060012DA RID: 4826
		[FreeFunction("Scripting::UnloadAssetFromScripting")]
		[MethodImpl(4096)]
		public static extern void UnloadAsset(Object assetToUnload);

		// Token: 0x060012DB RID: 4827
		[FreeFunction("Resources_Bindings::UnloadUnusedAssets")]
		[MethodImpl(4096)]
		public static extern AsyncOperation UnloadUnusedAssets();
	}
}
