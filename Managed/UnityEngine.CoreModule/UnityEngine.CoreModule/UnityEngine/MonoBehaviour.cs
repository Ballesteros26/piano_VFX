using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001AE RID: 430
	[RequiredByNativeCode]
	[ExtensionOfNativeClass]
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[NativeHeader("Runtime/Scripting/DelayedCallUtility.h")]
	public class MonoBehaviour : Behaviour
	{
		// Token: 0x060013B0 RID: 5040 RVA: 0x000201D8 File Offset: 0x0001E3D8
		public bool IsInvoking()
		{
			return MonoBehaviour.Internal_IsInvokingAll(this);
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x000201F0 File Offset: 0x0001E3F0
		public void CancelInvoke()
		{
			MonoBehaviour.Internal_CancelInvokeAll(this);
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x000201FA File Offset: 0x0001E3FA
		public void Invoke(string methodName, float time)
		{
			MonoBehaviour.InvokeDelayed(this, methodName, time, 0f);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0002020C File Offset: 0x0001E40C
		public void InvokeRepeating(string methodName, float time, float repeatRate)
		{
			bool flag = repeatRate <= 1E-05f && repeatRate != 0f;
			if (flag)
			{
				throw new UnityException("Invoke repeat rate has to be larger than 0.00001F)");
			}
			MonoBehaviour.InvokeDelayed(this, methodName, time, repeatRate);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x00020249 File Offset: 0x0001E449
		public void CancelInvoke(string methodName)
		{
			MonoBehaviour.CancelInvoke(this, methodName);
		}

		// Token: 0x060013B5 RID: 5045 RVA: 0x00020254 File Offset: 0x0001E454
		public bool IsInvoking(string methodName)
		{
			return MonoBehaviour.IsInvoking(this, methodName);
		}

		// Token: 0x060013B6 RID: 5046 RVA: 0x00020270 File Offset: 0x0001E470
		[ExcludeFromDocs]
		public Coroutine StartCoroutine(string methodName)
		{
			object obj = null;
			return this.StartCoroutine(methodName, obj);
		}

		// Token: 0x060013B7 RID: 5047 RVA: 0x0002028C File Offset: 0x0001E48C
		public Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value)
		{
			bool flag = string.IsNullOrEmpty(methodName);
			if (flag)
			{
				throw new NullReferenceException("methodName is null or empty");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			return this.StartCoroutineManaged(methodName, value);
		}

		// Token: 0x060013B8 RID: 5048 RVA: 0x000202D4 File Offset: 0x0001E4D4
		public Coroutine StartCoroutine(IEnumerator routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			return this.StartCoroutineManaged2(routine);
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00020318 File Offset: 0x0001E518
		[Obsolete("StartCoroutine_Auto has been deprecated. Use StartCoroutine instead (UnityUpgradable) -> StartCoroutine([mscorlib] System.Collections.IEnumerator)", false)]
		public Coroutine StartCoroutine_Auto(IEnumerator routine)
		{
			return this.StartCoroutine(routine);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x00020334 File Offset: 0x0001E534
		public void StopCoroutine(IEnumerator routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			this.StopCoroutineFromEnumeratorManaged(routine);
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x00020378 File Offset: 0x0001E578
		public void StopCoroutine(Coroutine routine)
		{
			bool flag = routine == null;
			if (flag)
			{
				throw new NullReferenceException("routine is null");
			}
			bool flag2 = !MonoBehaviour.IsObjectMonoBehaviour(this);
			if (flag2)
			{
				throw new ArgumentException("Coroutines can only be stopped on a MonoBehaviour");
			}
			this.StopCoroutineManaged(routine);
		}

		// Token: 0x060013BC RID: 5052
		[MethodImpl(4096)]
		public extern void StopCoroutine(string methodName);

		// Token: 0x060013BD RID: 5053
		[MethodImpl(4096)]
		public extern void StopAllCoroutines();

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060013BE RID: 5054
		// (set) Token: 0x060013BF RID: 5055
		public extern bool useGUILayout
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060013C0 RID: 5056 RVA: 0x000203B9 File Offset: 0x0001E5B9
		public static void print(object message)
		{
			Debug.Log(message);
		}

		// Token: 0x060013C1 RID: 5057
		[FreeFunction("CancelInvoke")]
		[MethodImpl(4096)]
		private static extern void Internal_CancelInvokeAll(MonoBehaviour self);

		// Token: 0x060013C2 RID: 5058
		[FreeFunction("IsInvoking")]
		[MethodImpl(4096)]
		private static extern bool Internal_IsInvokingAll(MonoBehaviour self);

		// Token: 0x060013C3 RID: 5059
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern void InvokeDelayed(MonoBehaviour self, string methodName, float time, float repeatRate);

		// Token: 0x060013C4 RID: 5060
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern void CancelInvoke(MonoBehaviour self, string methodName);

		// Token: 0x060013C5 RID: 5061
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern bool IsInvoking(MonoBehaviour self, string methodName);

		// Token: 0x060013C6 RID: 5062
		[FreeFunction]
		[MethodImpl(4096)]
		private static extern bool IsObjectMonoBehaviour(Object obj);

		// Token: 0x060013C7 RID: 5063
		[MethodImpl(4096)]
		private extern Coroutine StartCoroutineManaged(string methodName, object value);

		// Token: 0x060013C8 RID: 5064
		[MethodImpl(4096)]
		private extern Coroutine StartCoroutineManaged2(IEnumerator enumerator);

		// Token: 0x060013C9 RID: 5065
		[MethodImpl(4096)]
		private extern void StopCoroutineManaged(Coroutine routine);

		// Token: 0x060013CA RID: 5066
		[MethodImpl(4096)]
		private extern void StopCoroutineFromEnumeratorManaged(IEnumerator routine);

		// Token: 0x060013CB RID: 5067
		[MethodImpl(4096)]
		internal extern string GetScriptClassName();
	}
}
