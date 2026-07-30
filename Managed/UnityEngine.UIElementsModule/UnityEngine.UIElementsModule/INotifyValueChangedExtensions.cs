using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000D0 RID: 208
	public static class INotifyValueChangedExtensions
	{
		// Token: 0x060005DF RID: 1503 RVA: 0x000168E0 File Offset: 0x00014AE0
		public static bool RegisterValueChangedCallback<T>(this INotifyValueChanged<T> control, EventCallback<ChangeEvent<T>> callback)
		{
			CallbackEventHandler callbackEventHandler = control as CallbackEventHandler;
			bool flag = callbackEventHandler != null;
			bool flag2;
			if (flag)
			{
				callbackEventHandler.RegisterCallback<ChangeEvent<T>>(callback, TrickleDown.NoTrickleDown);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00016910 File Offset: 0x00014B10
		public static bool UnregisterValueChangedCallback<T>(this INotifyValueChanged<T> control, EventCallback<ChangeEvent<T>> callback)
		{
			CallbackEventHandler callbackEventHandler = control as CallbackEventHandler;
			bool flag = callbackEventHandler != null;
			bool flag2;
			if (flag)
			{
				callbackEventHandler.UnregisterCallback<ChangeEvent<T>>(callback, TrickleDown.NoTrickleDown);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}
	}
}
