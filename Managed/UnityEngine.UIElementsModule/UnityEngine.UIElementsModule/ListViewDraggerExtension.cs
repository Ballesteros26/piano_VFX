using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200011E RID: 286
	internal static class ListViewDraggerExtension
	{
		// Token: 0x0600087A RID: 2170 RVA: 0x00022630 File Offset: 0x00020830
		public static ListView.RecycledItem GetRecycledItemFromIndex(this ListView listView, int index)
		{
			foreach (ListView.RecycledItem recycledItem in listView.Pool)
			{
				bool flag = recycledItem.index.Equals(index);
				if (flag)
				{
					return recycledItem;
				}
			}
			return null;
		}
	}
}
