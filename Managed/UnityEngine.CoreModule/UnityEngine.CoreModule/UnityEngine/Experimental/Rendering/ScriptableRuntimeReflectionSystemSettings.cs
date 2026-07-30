using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003D0 RID: 976
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Camera/ScriptableRuntimeReflectionSystem.h")]
	public static class ScriptableRuntimeReflectionSystemSettings
	{
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060021D5 RID: 8661 RVA: 0x00039644 File Offset: 0x00037844
		// (set) Token: 0x060021D6 RID: 8662 RVA: 0x0003965C File Offset: 0x0003785C
		public static IScriptableRuntimeReflectionSystem system
		{
			get
			{
				return ScriptableRuntimeReflectionSystemSettings.Internal_ScriptableRuntimeReflectionSystemSettings_system;
			}
			set
			{
				bool flag = value == null || value.Equals(null);
				if (flag)
				{
					Debug.LogError("'null' cannot be assigned to ScriptableRuntimeReflectionSystemSettings.system");
				}
				else
				{
					bool flag2 = !(ScriptableRuntimeReflectionSystemSettings.system is BuiltinRuntimeReflectionSystem) && !(value is BuiltinRuntimeReflectionSystem) && ScriptableRuntimeReflectionSystemSettings.system != value;
					if (flag2)
					{
						Debug.LogWarningFormat("ScriptableRuntimeReflectionSystemSettings.system is assigned more than once. Only a the last instance will be used. (Last instance {0}, New instance {1})", new object[]
						{
							ScriptableRuntimeReflectionSystemSettings.system,
							value
						});
					}
					ScriptableRuntimeReflectionSystemSettings.Internal_ScriptableRuntimeReflectionSystemSettings_system = value;
				}
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060021D7 RID: 8663 RVA: 0x000396D4 File Offset: 0x000378D4
		// (set) Token: 0x060021D8 RID: 8664 RVA: 0x000396F0 File Offset: 0x000378F0
		private static IScriptableRuntimeReflectionSystem Internal_ScriptableRuntimeReflectionSystemSettings_system
		{
			get
			{
				return ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation;
			}
			[RequiredByNativeCode]
			set
			{
				bool flag = ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation != value;
				if (flag)
				{
					bool flag2 = ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation != null;
					if (flag2)
					{
						ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation.Dispose();
					}
				}
				ScriptableRuntimeReflectionSystemSettings.s_Instance.implementation = value;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x00039744 File Offset: 0x00037944
		private static ScriptableRuntimeReflectionSystemWrapper Internal_ScriptableRuntimeReflectionSystemSettings_instance
		{
			[RequiredByNativeCode]
			get
			{
				return ScriptableRuntimeReflectionSystemSettings.s_Instance;
			}
		}

		// Token: 0x060021DA RID: 8666
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
		[StaticAccessor("ScriptableRuntimeReflectionSystem", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern void ScriptingDirtyReflectionSystemInstance();

		// Token: 0x04000C47 RID: 3143
		private static ScriptableRuntimeReflectionSystemWrapper s_Instance = new ScriptableRuntimeReflectionSystemWrapper();
	}
}
