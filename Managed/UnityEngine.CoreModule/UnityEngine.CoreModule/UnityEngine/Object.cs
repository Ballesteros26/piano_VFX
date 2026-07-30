using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020001C5 RID: 453
	[RequiredByNativeCode(GenerateProxy = true)]
	[NativeHeader("Runtime/Export/Scripting/UnityEngineObject.bindings.h")]
	[NativeHeader("Runtime/SceneManager/SceneManager.h")]
	[NativeHeader("Runtime/GameCode/CloneObject.h")]
	[StructLayout(0)]
	public class Object
	{
		// Token: 0x06001418 RID: 5144 RVA: 0x00020F10 File Offset: 0x0001F110
		[SecuritySafeCritical]
		public unsafe int GetInstanceID()
		{
			bool flag = this.m_CachedPtr == IntPtr.Zero;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				bool flag2 = Object.OffsetOfInstanceIDInCPlusPlusObject == -1;
				if (flag2)
				{
					Object.OffsetOfInstanceIDInCPlusPlusObject = Object.GetOffsetOfInstanceIDInCPlusPlusObject();
				}
				num = *(int*)(void*)new IntPtr(this.m_CachedPtr.ToInt64() + (long)Object.OffsetOfInstanceIDInCPlusPlusObject);
			}
			return num;
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x00020F70 File Offset: 0x0001F170
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x00020F88 File Offset: 0x0001F188
		public override bool Equals(object other)
		{
			Object @object = other as Object;
			bool flag = @object == null && other != null && !(other is Object);
			return !flag && Object.CompareBaseObjects(this, @object);
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x00020FCC File Offset: 0x0001F1CC
		public static implicit operator bool(Object exists)
		{
			return !Object.CompareBaseObjects(exists, null);
		}

		// Token: 0x0600141C RID: 5148 RVA: 0x00020FE8 File Offset: 0x0001F1E8
		private static bool CompareBaseObjects(Object lhs, Object rhs)
		{
			bool flag = lhs == null;
			bool flag2 = rhs == null;
			bool flag3 = flag2 && flag;
			bool flag4;
			if (flag3)
			{
				flag4 = true;
			}
			else
			{
				bool flag5 = flag2;
				if (flag5)
				{
					flag4 = !Object.IsNativeObjectAlive(lhs);
				}
				else
				{
					bool flag6 = flag;
					if (flag6)
					{
						flag4 = !Object.IsNativeObjectAlive(rhs);
					}
					else
					{
						flag4 = lhs == rhs;
					}
				}
			}
			return flag4;
		}

		// Token: 0x0600141D RID: 5149 RVA: 0x0002103C File Offset: 0x0001F23C
		private void EnsureRunningOnMainThread()
		{
			bool flag = !Object.CurrentThreadIsMainThread();
			if (flag)
			{
				throw new InvalidOperationException("EnsureRunningOnMainThread can only be called from the main thread");
			}
		}

		// Token: 0x0600141E RID: 5150 RVA: 0x00021064 File Offset: 0x0001F264
		private static bool IsNativeObjectAlive(Object o)
		{
			return o.GetCachedPtr() != IntPtr.Zero;
		}

		// Token: 0x0600141F RID: 5151 RVA: 0x00021088 File Offset: 0x0001F288
		private IntPtr GetCachedPtr()
		{
			return this.m_CachedPtr;
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x000210A0 File Offset: 0x0001F2A0
		// (set) Token: 0x06001421 RID: 5153 RVA: 0x000210B8 File Offset: 0x0001F2B8
		public string name
		{
			get
			{
				return Object.GetName(this);
			}
			set
			{
				Object.SetName(this, value);
			}
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x000210C4 File Offset: 0x0001F2C4
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation)
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			bool flag = original is ScriptableObject;
			if (flag)
			{
				throw new ArgumentException("Cannot instantiate a ScriptableObject with a position and rotation");
			}
			Object @object = Object.Internal_InstantiateSingle(original, position, rotation);
			bool flag2 = @object == null;
			if (flag2)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return @object;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x0002111C File Offset: 0x0001F31C
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent)
		{
			bool flag = parent == null;
			Object @object;
			if (flag)
			{
				@object = Object.Instantiate(original, position, rotation);
			}
			else
			{
				Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
				Object object2 = Object.Internal_InstantiateSingleWithParent(original, parent, position, rotation);
				bool flag2 = object2 == null;
				if (flag2)
				{
					throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
				}
				@object = object2;
			}
			return @object;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x00021174 File Offset: 0x0001F374
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original)
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			Object @object = Object.Internal_CloneSingle(original);
			bool flag = @object == null;
			if (flag)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return @object;
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x000211B0 File Offset: 0x0001F3B0
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Transform parent)
		{
			return Object.Instantiate(original, parent, false);
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x000211CC File Offset: 0x0001F3CC
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Transform parent, bool instantiateInWorldSpace)
		{
			bool flag = parent == null;
			Object @object;
			if (flag)
			{
				@object = Object.Instantiate(original);
			}
			else
			{
				Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
				Object object2 = Object.Internal_CloneSingleWithParent(original, parent, instantiateInWorldSpace);
				bool flag2 = object2 == null;
				if (flag2)
				{
					throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
				}
				@object = object2;
			}
			return @object;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00021220 File Offset: 0x0001F420
		public static T Instantiate<T>(T original) where T : Object
		{
			Object.CheckNullArgument(original, "The Object you want to instantiate is null.");
			T t = (T)((object)Object.Internal_CloneSingle(original));
			bool flag = t == null;
			if (flag)
			{
				throw new UnityException("Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.");
			}
			return t;
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x00021270 File Offset: 0x0001F470
		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation) where T : Object
		{
			return (T)((object)Object.Instantiate(original, position, rotation));
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00021294 File Offset: 0x0001F494
		public static T Instantiate<T>(T original, Vector3 position, Quaternion rotation, Transform parent) where T : Object
		{
			return (T)((object)Object.Instantiate(original, position, rotation, parent));
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x000212BC File Offset: 0x0001F4BC
		public static T Instantiate<T>(T original, Transform parent) where T : Object
		{
			return Object.Instantiate<T>(original, parent, false);
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x000212D8 File Offset: 0x0001F4D8
		public static T Instantiate<T>(T original, Transform parent, bool worldPositionStays) where T : Object
		{
			return (T)((object)Object.Instantiate(original, parent, worldPositionStays));
		}

		// Token: 0x0600142C RID: 5164
		[NativeMethod(Name = "Scripting::DestroyObjectFromScripting", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern void Destroy(Object obj, [DefaultValue("0.0F")] float t);

		// Token: 0x0600142D RID: 5165 RVA: 0x000212FC File Offset: 0x0001F4FC
		[ExcludeFromDocs]
		public static void Destroy(Object obj)
		{
			float num = 0f;
			Object.Destroy(obj, num);
		}

		// Token: 0x0600142E RID: 5166
		[NativeMethod(Name = "Scripting::DestroyObjectFromScriptingImmediate", IsFreeFunction = true, ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern void DestroyImmediate(Object obj, [DefaultValue("false")] bool allowDestroyingAssets);

		// Token: 0x0600142F RID: 5167 RVA: 0x00021318 File Offset: 0x0001F518
		[ExcludeFromDocs]
		public static void DestroyImmediate(Object obj)
		{
			bool flag = false;
			Object.DestroyImmediate(obj, flag);
		}

		// Token: 0x06001430 RID: 5168 RVA: 0x00021330 File Offset: 0x0001F530
		public static Object[] FindObjectsOfType(Type type)
		{
			return Object.FindObjectsOfType(type, false);
		}

		// Token: 0x06001431 RID: 5169
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[FreeFunction("UnityEngineObjectBindings::FindObjectsOfType")]
		[MethodImpl(4096)]
		public static extern Object[] FindObjectsOfType(Type type, bool includeInactive);

		// Token: 0x06001432 RID: 5170
		[FreeFunction("GetSceneManager().DontDestroyOnLoad", ThrowsException = true)]
		[MethodImpl(4096)]
		public static extern void DontDestroyOnLoad(Object target);

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001433 RID: 5171
		// (set) Token: 0x06001434 RID: 5172
		public extern HideFlags hideFlags
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06001435 RID: 5173 RVA: 0x00021349 File Offset: 0x0001F549
		[Obsolete("use Object.Destroy instead.")]
		public static void DestroyObject(Object obj, [DefaultValue("0.0F")] float t)
		{
			Object.Destroy(obj, t);
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00021354 File Offset: 0x0001F554
		[ExcludeFromDocs]
		[Obsolete("use Object.Destroy instead.")]
		public static void DestroyObject(Object obj)
		{
			float num = 0f;
			Object.Destroy(obj, num);
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x00021370 File Offset: 0x0001F570
		[Obsolete("warning use Object.FindObjectsOfType instead.")]
		public static Object[] FindSceneObjectsOfType(Type type)
		{
			return Object.FindObjectsOfType(type);
		}

		// Token: 0x06001438 RID: 5176
		[FreeFunction("UnityEngineObjectBindings::FindObjectsOfTypeIncludingAssets")]
		[Obsolete("use Resources.FindObjectsOfTypeAll instead.")]
		[MethodImpl(4096)]
		public static extern Object[] FindObjectsOfTypeIncludingAssets(Type type);

		// Token: 0x06001439 RID: 5177 RVA: 0x00021388 File Offset: 0x0001F588
		public static T[] FindObjectsOfType<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsOfType(typeof(T), false));
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x000213B0 File Offset: 0x0001F5B0
		public static T[] FindObjectsOfType<T>(bool includeInactive) where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsOfType(typeof(T), includeInactive));
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x000213D8 File Offset: 0x0001F5D8
		public static T FindObjectOfType<T>() where T : Object
		{
			return (T)((object)Object.FindObjectOfType(typeof(T), false));
		}

		// Token: 0x0600143C RID: 5180 RVA: 0x00021400 File Offset: 0x0001F600
		public static T FindObjectOfType<T>(bool includeInactive) where T : Object
		{
			return (T)((object)Object.FindObjectOfType(typeof(T), includeInactive));
		}

		// Token: 0x0600143D RID: 5181 RVA: 0x00021428 File Offset: 0x0001F628
		[Obsolete("Please use Resources.FindObjectsOfTypeAll instead")]
		public static Object[] FindObjectsOfTypeAll(Type type)
		{
			return Resources.FindObjectsOfTypeAll(type);
		}

		// Token: 0x0600143E RID: 5182 RVA: 0x00021440 File Offset: 0x0001F640
		private static void CheckNullArgument(object arg, string message)
		{
			bool flag = arg == null;
			if (flag)
			{
				throw new ArgumentException(message);
			}
		}

		// Token: 0x0600143F RID: 5183 RVA: 0x00021460 File Offset: 0x0001F660
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public static Object FindObjectOfType(Type type)
		{
			Object[] array = Object.FindObjectsOfType(type, false);
			bool flag = array.Length != 0;
			Object @object;
			if (flag)
			{
				@object = array[0];
			}
			else
			{
				@object = null;
			}
			return @object;
		}

		// Token: 0x06001440 RID: 5184 RVA: 0x0002148C File Offset: 0x0001F68C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public static Object FindObjectOfType(Type type, bool includeInactive)
		{
			Object[] array = Object.FindObjectsOfType(type, includeInactive);
			bool flag = array.Length != 0;
			Object @object;
			if (flag)
			{
				@object = array[0];
			}
			else
			{
				@object = null;
			}
			return @object;
		}

		// Token: 0x06001441 RID: 5185 RVA: 0x000214B8 File Offset: 0x0001F6B8
		public override string ToString()
		{
			return Object.ToString(this);
		}

		// Token: 0x06001442 RID: 5186 RVA: 0x000214D0 File Offset: 0x0001F6D0
		public static bool operator ==(Object x, Object y)
		{
			return Object.CompareBaseObjects(x, y);
		}

		// Token: 0x06001443 RID: 5187 RVA: 0x000214EC File Offset: 0x0001F6EC
		public static bool operator !=(Object x, Object y)
		{
			return !Object.CompareBaseObjects(x, y);
		}

		// Token: 0x06001444 RID: 5188
		[NativeMethod(Name = "Object::GetOffsetOfInstanceIdMember", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern int GetOffsetOfInstanceIDInCPlusPlusObject();

		// Token: 0x06001445 RID: 5189
		[NativeMethod(Name = "CurrentThreadIsMainThread", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern bool CurrentThreadIsMainThread();

		// Token: 0x06001446 RID: 5190
		[FreeFunction("CloneObject")]
		[MethodImpl(4096)]
		private static extern Object Internal_CloneSingle(Object data);

		// Token: 0x06001447 RID: 5191
		[FreeFunction("CloneObject")]
		[MethodImpl(4096)]
		private static extern Object Internal_CloneSingleWithParent(Object data, Transform parent, bool worldPositionStays);

		// Token: 0x06001448 RID: 5192 RVA: 0x00021508 File Offset: 0x0001F708
		[FreeFunction("InstantiateObject")]
		private static Object Internal_InstantiateSingle(Object data, Vector3 pos, Quaternion rot)
		{
			return Object.Internal_InstantiateSingle_Injected(data, ref pos, ref rot);
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x00021514 File Offset: 0x0001F714
		[FreeFunction("InstantiateObject")]
		private static Object Internal_InstantiateSingleWithParent(Object data, Transform parent, Vector3 pos, Quaternion rot)
		{
			return Object.Internal_InstantiateSingleWithParent_Injected(data, parent, ref pos, ref rot);
		}

		// Token: 0x0600144A RID: 5194
		[FreeFunction("UnityEngineObjectBindings::ToString")]
		[MethodImpl(4096)]
		private static extern string ToString(Object obj);

		// Token: 0x0600144B RID: 5195
		[FreeFunction("UnityEngineObjectBindings::GetName")]
		[MethodImpl(4096)]
		private static extern string GetName(Object obj);

		// Token: 0x0600144C RID: 5196
		[FreeFunction("UnityEngineObjectBindings::IsPersistent")]
		[MethodImpl(4096)]
		internal static extern bool IsPersistent(Object obj);

		// Token: 0x0600144D RID: 5197
		[FreeFunction("UnityEngineObjectBindings::SetName")]
		[MethodImpl(4096)]
		private static extern void SetName(Object obj, string name);

		// Token: 0x0600144E RID: 5198
		[NativeMethod(Name = "UnityEngineObjectBindings::DoesObjectWithInstanceIDExist", IsFreeFunction = true, IsThreadSafe = true)]
		[MethodImpl(4096)]
		internal static extern bool DoesObjectWithInstanceIDExist(int instanceID);

		// Token: 0x0600144F RID: 5199
		[VisibleToOtherModules]
		[FreeFunction("UnityEngineObjectBindings::FindObjectFromInstanceID")]
		[MethodImpl(4096)]
		internal static extern Object FindObjectFromInstanceID(int instanceID);

		// Token: 0x06001450 RID: 5200
		[VisibleToOtherModules]
		[FreeFunction("UnityEngineObjectBindings::ForceLoadFromInstanceID")]
		[MethodImpl(4096)]
		internal static extern Object ForceLoadFromInstanceID(int instanceID);

		// Token: 0x06001453 RID: 5203
		[MethodImpl(4096)]
		private static extern Object Internal_InstantiateSingle_Injected(Object data, ref Vector3 pos, ref Quaternion rot);

		// Token: 0x06001454 RID: 5204
		[MethodImpl(4096)]
		private static extern Object Internal_InstantiateSingleWithParent_Injected(Object data, Transform parent, ref Vector3 pos, ref Quaternion rot);

		// Token: 0x04000676 RID: 1654
		private IntPtr m_CachedPtr;

		// Token: 0x04000677 RID: 1655
		internal static int OffsetOfInstanceIDInCPlusPlusObject = -1;

		// Token: 0x04000678 RID: 1656
		private const string objectIsNullMessage = "The Object you want to instantiate is null.";

		// Token: 0x04000679 RID: 1657
		private const string cloneDestroyedMessage = "Instantiate failed because the clone was destroyed during creation. This can happen if DestroyImmediate is called in MonoBehaviour.Awake.";
	}
}
