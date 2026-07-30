using System;
using System.Collections;
using System.Security;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001A4 RID: 420
	[RequiredByNativeCode]
	internal class SetupCoroutine
	{
		// Token: 0x06001341 RID: 4929 RVA: 0x0001F834 File Offset: 0x0001DA34
		[SecuritySafeCritical]
		[RequiredByNativeCode]
		public unsafe static void InvokeMoveNext(IEnumerator enumerator, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			*(byte*)(void*)returnValueAddress = (enumerator.MoveNext() ? 1 : 0);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0001F870 File Offset: 0x0001DA70
		[RequiredByNativeCode]
		public static object InvokeMember(object behaviour, string name, object variable)
		{
			object[] array = null;
			bool flag = variable != null;
			if (flag)
			{
				array = new object[] { variable };
			}
			return behaviour.GetType().InvokeMember(name, 308, null, behaviour, array, null, null, null);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0001F8B0 File Offset: 0x0001DAB0
		public static object InvokeStatic(Type klass, string name, object variable)
		{
			object[] array = null;
			bool flag = variable != null;
			if (flag)
			{
				array = new object[] { variable };
			}
			return klass.InvokeMember(name, 312, null, null, array, null, null, null);
		}
	}
}
