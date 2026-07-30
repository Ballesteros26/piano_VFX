using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005A RID: 90
	public static class DelegateUtility
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000B600 File Offset: 0x00009800
		public static Delegate Cast(Delegate source, Type type)
		{
			if (source == null)
			{
				return null;
			}
			Delegate[] invocationList = source.GetInvocationList();
			if (invocationList.Length == 1)
			{
				return Delegate.CreateDelegate(type, invocationList[0].Target, invocationList[0].Method);
			}
			Delegate[] array = new Delegate[invocationList.Length];
			for (int i = 0; i < invocationList.Length; i++)
			{
				array[i] = Delegate.CreateDelegate(type, invocationList[i].Target, invocationList[i].Method);
			}
			return Delegate.Combine(array);
		}
	}
}
