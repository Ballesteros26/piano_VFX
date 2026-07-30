using System;
using System.Collections.Generic;
using System.Security;

namespace UnityEngine
{
	// Token: 0x02000027 RID: 39
	internal class GUIStateObjects
	{
		// Token: 0x060002BD RID: 701 RVA: 0x0000AE88 File Offset: 0x00009088
		[SecuritySafeCritical]
		internal static object GetStateObject(Type t, int controlID)
		{
			object obj;
			bool flag = !GUIStateObjects.s_StateCache.TryGetValue(controlID, ref obj) || obj.GetType() != t;
			if (flag)
			{
				obj = Activator.CreateInstance(t);
				GUIStateObjects.s_StateCache[controlID] = obj;
			}
			return obj;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000AED4 File Offset: 0x000090D4
		internal static object QueryStateObject(Type t, int controlID)
		{
			object obj = GUIStateObjects.s_StateCache[controlID];
			bool flag = t.IsInstanceOfType(obj);
			object obj2;
			if (flag)
			{
				obj2 = obj;
			}
			else
			{
				obj2 = null;
			}
			return obj2;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000AF03 File Offset: 0x00009103
		internal static void Tests_ClearObjects()
		{
			GUIStateObjects.s_StateCache.Clear();
		}

		// Token: 0x040000C2 RID: 194
		private static Dictionary<int, object> s_StateCache = new Dictionary<int, object>();
	}
}
