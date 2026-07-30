using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001B3 RID: 435
	[ExtensionOfNativeClass]
	[NativeClass(null)]
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[StructLayout(0)]
	public class ScriptableObject : Object
	{
		// Token: 0x060013DB RID: 5083 RVA: 0x0002052E File Offset: 0x0001E72E
		public ScriptableObject()
		{
			ScriptableObject.CreateScriptableObject(this);
		}

		// Token: 0x060013DC RID: 5084
		[NativeConditional("ENABLE_MONO")]
		[Obsolete("Use EditorUtility.SetDirty instead")]
		[MethodImpl(4096)]
		public extern void SetDirty();

		// Token: 0x060013DD RID: 5085 RVA: 0x00020540 File Offset: 0x0001E740
		public static ScriptableObject CreateInstance(string className)
		{
			return ScriptableObject.CreateScriptableObjectInstanceFromName(className);
		}

		// Token: 0x060013DE RID: 5086 RVA: 0x00020558 File Offset: 0x0001E758
		public static ScriptableObject CreateInstance(Type type)
		{
			return ScriptableObject.CreateScriptableObjectInstanceFromType(type, true);
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x00020574 File Offset: 0x0001E774
		public static T CreateInstance<T>() where T : ScriptableObject
		{
			return (T)((object)ScriptableObject.CreateInstance(typeof(T)));
		}

		// Token: 0x060013E0 RID: 5088
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private static extern void CreateScriptableObject([Writable] ScriptableObject self);

		// Token: 0x060013E1 RID: 5089
		[FreeFunction("Scripting::CreateScriptableObject")]
		[MethodImpl(4096)]
		private static extern ScriptableObject CreateScriptableObjectInstanceFromName(string className);

		// Token: 0x060013E2 RID: 5090
		[FreeFunction("Scripting::CreateScriptableObjectWithType")]
		[MethodImpl(4096)]
		internal static extern ScriptableObject CreateScriptableObjectInstanceFromType(Type type, bool applyDefaultsAndReset);

		// Token: 0x060013E3 RID: 5091
		[FreeFunction("Scripting::ResetAndApplyDefaultInstances")]
		[MethodImpl(4096)]
		internal static extern void ResetAndApplyDefaultInstances(Object obj);
	}
}
