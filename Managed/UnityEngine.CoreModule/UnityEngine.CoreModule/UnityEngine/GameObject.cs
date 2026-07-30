using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020001AA RID: 426
	[ExcludeFromPreset]
	[NativeHeader("Runtime/Export/Scripting/GameObject.bindings.h")]
	[UsedByNativeCode]
	public sealed class GameObject : Object
	{
		// Token: 0x0600134F RID: 4943
		[FreeFunction("GameObjectBindings::CreatePrimitive")]
		[MethodImpl(4096)]
		public static extern GameObject CreatePrimitive(PrimitiveType type);

		// Token: 0x06001350 RID: 4944 RVA: 0x0001F950 File Offset: 0x0001DB50
		[SecuritySafeCritical]
		public unsafe T GetComponent<T>()
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			this.GetComponentFastPath(typeof(T), new IntPtr((void*)(&castHelper.onePointerFurtherThanT)));
			return castHelper.t;
		}

		// Token: 0x06001351 RID: 4945
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction(Name = "GameObjectBindings::GetComponentFromType", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Component GetComponent(Type type);

		// Token: 0x06001352 RID: 4946
		[FreeFunction(Name = "GameObjectBindings::GetComponentFastPath", HasExplicitThis = true, ThrowsException = true)]
		[NativeWritableSelf]
		[MethodImpl(4096)]
		internal extern void GetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue);

		// Token: 0x06001353 RID: 4947
		[FreeFunction(Name = "Scripting::GetScriptingWrapperOfComponentOfGameObject", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern Component GetComponentByName(string type);

		// Token: 0x06001354 RID: 4948 RVA: 0x0001F990 File Offset: 0x0001DB90
		public Component GetComponent(string type)
		{
			return this.GetComponentByName(type);
		}

		// Token: 0x06001355 RID: 4949
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction(Name = "GameObjectBindings::GetComponentInChildren", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public extern Component GetComponentInChildren(Type type, bool includeInactive);

		// Token: 0x06001356 RID: 4950 RVA: 0x0001F9AC File Offset: 0x0001DBAC
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type type)
		{
			return this.GetComponentInChildren(type, false);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x0001F9C8 File Offset: 0x0001DBC8
		[ExcludeFromDocs]
		public T GetComponentInChildren<T>()
		{
			bool flag = false;
			return this.GetComponentInChildren<T>(flag);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x0001F9E4 File Offset: 0x0001DBE4
		public T GetComponentInChildren<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInChildren(typeof(T), includeInactive));
		}

		// Token: 0x06001359 RID: 4953
		[FreeFunction(Name = "GameObjectBindings::GetComponentInParent", HasExplicitThis = true, ThrowsException = true)]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[MethodImpl(4096)]
		public extern Component GetComponentInParent(Type type, bool includeInactive);

		// Token: 0x0600135A RID: 4954 RVA: 0x0001FA0C File Offset: 0x0001DC0C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type type)
		{
			return this.GetComponentInParent(type, false);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x0001FA28 File Offset: 0x0001DC28
		[ExcludeFromDocs]
		public T GetComponentInParent<T>()
		{
			bool flag = false;
			return this.GetComponentInParent<T>(flag);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0001FA44 File Offset: 0x0001DC44
		public T GetComponentInParent<T>([DefaultValue("false")] bool includeInactive)
		{
			return (T)((object)this.GetComponentInParent(typeof(T), includeInactive));
		}

		// Token: 0x0600135D RID: 4957
		[FreeFunction(Name = "GameObjectBindings::GetComponentsInternal", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		private extern Array GetComponentsInternal(Type type, bool useSearchTypeAsArrayReturnType, bool recursive, bool includeInactive, bool reverse, object resultList);

		// Token: 0x0600135E RID: 4958 RVA: 0x0001FA6C File Offset: 0x0001DC6C
		public Component[] GetComponents(Type type)
		{
			return (Component[])this.GetComponentsInternal(type, false, false, true, false, null);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0001FA90 File Offset: 0x0001DC90
		public T[] GetComponents<T>()
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, false, true, false, null);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x0001FABC File Offset: 0x0001DCBC
		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsInternal(type, false, false, true, false, results);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x0001FACC File Offset: 0x0001DCCC
		public void GetComponents<T>(List<T> results)
		{
			this.GetComponentsInternal(typeof(T), false, false, true, false, results);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x0001FAE8 File Offset: 0x0001DCE8
		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type type)
		{
			bool flag = false;
			return this.GetComponentsInChildren(type, flag);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0001FB04 File Offset: 0x0001DD04
		public Component[] GetComponentsInChildren(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return (Component[])this.GetComponentsInternal(type, false, true, includeInactive, false, null);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0001FB28 File Offset: 0x0001DD28
		public T[] GetComponentsInChildren<T>(bool includeInactive)
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, false, null);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0001FB54 File Offset: 0x0001DD54
		public void GetComponentsInChildren<T>(bool includeInactive, List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, true, includeInactive, false, results);
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0001FB70 File Offset: 0x0001DD70
		public T[] GetComponentsInChildren<T>()
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x0001FB89 File Offset: 0x0001DD89
		public void GetComponentsInChildren<T>(List<T> results)
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x0001FB98 File Offset: 0x0001DD98
		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type type)
		{
			bool flag = false;
			return this.GetComponentsInParent(type, flag);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x0001FBB4 File Offset: 0x0001DDB4
		public Component[] GetComponentsInParent(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return (Component[])this.GetComponentsInternal(type, false, true, includeInactive, true, null);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x0001FBD7 File Offset: 0x0001DDD7
		public void GetComponentsInParent<T>(bool includeInactive, List<T> results)
		{
			this.GetComponentsInternal(typeof(T), true, true, includeInactive, true, results);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0001FBF0 File Offset: 0x0001DDF0
		public T[] GetComponentsInParent<T>(bool includeInactive)
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, true, null);
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0001FC1C File Offset: 0x0001DE1C
		public T[] GetComponentsInParent<T>()
		{
			return this.GetComponentsInParent<T>(false);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0001FC38 File Offset: 0x0001DE38
		[SecuritySafeCritical]
		public unsafe bool TryGetComponent<T>(out T component)
		{
			CastHelper<T> castHelper = default(CastHelper<T>);
			this.TryGetComponentFastPath(typeof(T), new IntPtr((void*)(&castHelper.onePointerFurtherThanT)));
			component = castHelper.t;
			return castHelper.t != null;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0001FC8C File Offset: 0x0001DE8C
		public bool TryGetComponent(Type type, out Component component)
		{
			component = this.TryGetComponentInternal(type);
			return component != null;
		}

		// Token: 0x0600136F RID: 4975
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[FreeFunction(Name = "GameObjectBindings::TryGetComponentFromType", HasExplicitThis = true, ThrowsException = true)]
		[MethodImpl(4096)]
		internal extern Component TryGetComponentInternal(Type type);

		// Token: 0x06001370 RID: 4976
		[FreeFunction(Name = "GameObjectBindings::TryGetComponentFastPath", HasExplicitThis = true, ThrowsException = true)]
		[NativeWritableSelf]
		[MethodImpl(4096)]
		internal extern void TryGetComponentFastPath(Type type, IntPtr oneFurtherThanResultValue);

		// Token: 0x06001371 RID: 4977 RVA: 0x0001FCB0 File Offset: 0x0001DEB0
		public static GameObject FindWithTag(string tag)
		{
			return GameObject.FindGameObjectWithTag(tag);
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x0001FCC8 File Offset: 0x0001DEC8
		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x0001FCD5 File Offset: 0x0001DED5
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x0001FCE2 File Offset: 0x0001DEE2
		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}

		// Token: 0x06001375 RID: 4981
		[FreeFunction(Name = "MonoAddComponent", HasExplicitThis = true)]
		[MethodImpl(4096)]
		internal extern Component AddComponentInternal(string className);

		// Token: 0x06001376 RID: 4982
		[FreeFunction(Name = "MonoAddComponentWithType", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern Component Internal_AddComponentWithType(Type componentType);

		// Token: 0x06001377 RID: 4983 RVA: 0x0001FCF0 File Offset: 0x0001DEF0
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component AddComponent(Type componentType)
		{
			return this.Internal_AddComponentWithType(componentType);
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x0001FD0C File Offset: 0x0001DF0C
		public T AddComponent<T>() where T : Component
		{
			return this.AddComponent(typeof(T)) as T;
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06001379 RID: 4985
		public extern Transform transform
		{
			[FreeFunction("GameObjectBindings::GetTransform", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600137A RID: 4986
		// (set) Token: 0x0600137B RID: 4987
		public extern int layer
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600137C RID: 4988
		// (set) Token: 0x0600137D RID: 4989
		[Obsolete("GameObject.active is obsolete. Use GameObject.SetActive(), GameObject.activeSelf or GameObject.activeInHierarchy.")]
		public extern bool active
		{
			[NativeMethod(Name = "IsActive")]
			[MethodImpl(4096)]
			get;
			[NativeMethod(Name = "SetSelfActive")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600137E RID: 4990
		[NativeMethod(Name = "SetSelfActive")]
		[MethodImpl(4096)]
		public extern void SetActive(bool value);

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x0600137F RID: 4991
		public extern bool activeSelf
		{
			[NativeMethod(Name = "IsSelfActive")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001380 RID: 4992
		public extern bool activeInHierarchy
		{
			[NativeMethod(Name = "IsActive")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001381 RID: 4993
		[NativeMethod(Name = "SetActiveRecursivelyDeprecated")]
		[Obsolete("gameObject.SetActiveRecursively() is obsolete. Use GameObject.SetActive(), which is now inherited by children.")]
		[MethodImpl(4096)]
		public extern void SetActiveRecursively(bool state);

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001382 RID: 4994
		// (set) Token: 0x06001383 RID: 4995
		public extern bool isStatic
		{
			[NativeMethod(Name = "GetIsStaticDeprecated")]
			[MethodImpl(4096)]
			get;
			[NativeMethod(Name = "SetIsStaticDeprecated")]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001384 RID: 4996
		internal extern bool isStaticBatchable
		{
			[NativeMethod(Name = "IsStaticBatchable")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001385 RID: 4997
		// (set) Token: 0x06001386 RID: 4998
		public extern string tag
		{
			[FreeFunction("GameObjectBindings::GetTag", HasExplicitThis = true, ThrowsException = true)]
			[MethodImpl(4096)]
			get;
			[FreeFunction("GameObjectBindings::SetTag", HasExplicitThis = true)]
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06001387 RID: 4999
		[FreeFunction(Name = "GameObjectBindings::CompareTag", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern bool CompareTag(string tag);

		// Token: 0x06001388 RID: 5000
		[FreeFunction(Name = "GameObjectBindings::FindGameObjectWithTag", ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern GameObject FindGameObjectWithTag(string tag);

		// Token: 0x06001389 RID: 5001
		[FreeFunction(Name = "GameObjectBindings::FindGameObjectsWithTag", ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern GameObject[] FindGameObjectsWithTag(string tag);

		// Token: 0x0600138A RID: 5002
		[FreeFunction(Name = "Scripting::SendScriptingMessageUpwards", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x0600138B RID: 5003 RVA: 0x0001FD38 File Offset: 0x0001DF38
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.SendMessageUpwards(methodName, value, sendMessageOptions);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0001FD54 File Offset: 0x0001DF54
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.SendMessageUpwards(methodName, obj, sendMessageOptions);
		}

		// Token: 0x0600138D RID: 5005
		[FreeFunction(Name = "Scripting::SendScriptingMessage", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SendMessage(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x0600138E RID: 5006 RVA: 0x0001FD70 File Offset: 0x0001DF70
		[ExcludeFromDocs]
		public void SendMessage(string methodName, object value)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.SendMessage(methodName, value, sendMessageOptions);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0001FD8C File Offset: 0x0001DF8C
		[ExcludeFromDocs]
		public void SendMessage(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.SendMessage(methodName, obj, sendMessageOptions);
		}

		// Token: 0x06001390 RID: 5008
		[FreeFunction(Name = "Scripting::BroadcastScriptingMessage", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06001391 RID: 5009 RVA: 0x0001FDA8 File Offset: 0x0001DFA8
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.BroadcastMessage(methodName, parameter, sendMessageOptions);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0001FDC4 File Offset: 0x0001DFC4
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.BroadcastMessage(methodName, obj, sendMessageOptions);
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x0001FDE0 File Offset: 0x0001DFE0
		public GameObject(string name)
		{
			GameObject.Internal_CreateGameObject(this, name);
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x0001FDF2 File Offset: 0x0001DFF2
		public GameObject()
		{
			GameObject.Internal_CreateGameObject(this, null);
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x0001FE04 File Offset: 0x0001E004
		public GameObject(string name, params Type[] components)
		{
			GameObject.Internal_CreateGameObject(this, name);
			foreach (Type type in components)
			{
				this.AddComponent(type);
			}
		}

		// Token: 0x06001396 RID: 5014
		[FreeFunction(Name = "GameObjectBindings::Internal_CreateGameObject")]
		[MethodImpl(4096)]
		private static extern void Internal_CreateGameObject([Writable] GameObject self, string name);

		// Token: 0x06001397 RID: 5015
		[FreeFunction(Name = "GameObjectBindings::Find")]
		[MethodImpl(4096)]
		public static extern GameObject Find(string name);

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x0001FE40 File Offset: 0x0001E040
		public Scene scene
		{
			[FreeFunction("GameObjectBindings::GetScene", HasExplicitThis = true)]
			get
			{
				Scene scene;
				this.get_scene_Injected(out scene);
				return scene;
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0001FE58 File Offset: 0x0001E058
		public GameObject gameObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600139A RID: 5018
		[MethodImpl(4096)]
		private extern void get_scene_Injected(out Scene ret);
	}
}
