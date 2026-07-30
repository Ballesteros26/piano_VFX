using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200010E RID: 270
	internal static class DragAndDropUtility
	{
		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x00021848 File Offset: 0x0001FA48
		public static IDragAndDrop dragAndDrop
		{
			get
			{
				bool flag = DragAndDropUtility.s_DragAndDrop == null;
				if (flag)
				{
					bool flag2 = DragAndDropUtility.s_MakeClientFunc != null;
					if (flag2)
					{
						DragAndDropUtility.s_DragAndDrop = DragAndDropUtility.s_MakeClientFunc.Invoke();
					}
					else
					{
						DragAndDropUtility.s_DragAndDrop = new DefaultDragAndDropClient();
					}
				}
				return DragAndDropUtility.s_DragAndDrop;
			}
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x00021894 File Offset: 0x0001FA94
		internal static void RegisterMakeClientFunc(Func<IDragAndDrop> makeClient)
		{
			bool flag = DragAndDropUtility.s_MakeClientFunc != null;
			if (flag)
			{
				throw new UnityException("The MakeClientFunc has already been registered. Registration denied.");
			}
			DragAndDropUtility.s_MakeClientFunc = makeClient;
		}

		// Token: 0x040003B2 RID: 946
		private static Func<IDragAndDrop> s_MakeClientFunc;

		// Token: 0x040003B3 RID: 947
		private static IDragAndDrop s_DragAndDrop;
	}
}
