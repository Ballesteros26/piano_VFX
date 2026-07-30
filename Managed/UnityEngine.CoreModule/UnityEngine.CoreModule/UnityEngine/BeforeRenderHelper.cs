using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine
{
	// Token: 0x020000D2 RID: 210
	internal static class BeforeRenderHelper
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x00009B94 File Offset: 0x00007D94
		private static int GetUpdateOrder(UnityAction callback)
		{
			object[] customAttributes = callback.Method.GetCustomAttributes(typeof(BeforeRenderOrderAttribute), true);
			BeforeRenderOrderAttribute beforeRenderOrderAttribute = ((customAttributes != null && customAttributes.Length != 0) ? (customAttributes[0] as BeforeRenderOrderAttribute) : null);
			return (beforeRenderOrderAttribute != null) ? beforeRenderOrderAttribute.order : 0;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00009BDC File Offset: 0x00007DDC
		public static void RegisterCallback(UnityAction callback)
		{
			int updateOrder = BeforeRenderHelper.GetUpdateOrder(callback);
			List<BeforeRenderHelper.OrderBlock> list = BeforeRenderHelper.s_OrderBlocks;
			lock (list)
			{
				int num = 0;
				while (num < BeforeRenderHelper.s_OrderBlocks.Count && BeforeRenderHelper.s_OrderBlocks[num].order <= updateOrder)
				{
					bool flag = BeforeRenderHelper.s_OrderBlocks[num].order == updateOrder;
					if (flag)
					{
						BeforeRenderHelper.OrderBlock orderBlock = BeforeRenderHelper.s_OrderBlocks[num];
						orderBlock.callback = (UnityAction)Delegate.Combine(orderBlock.callback, callback);
						BeforeRenderHelper.s_OrderBlocks[num] = orderBlock;
						return;
					}
					num++;
				}
				BeforeRenderHelper.OrderBlock orderBlock2 = default(BeforeRenderHelper.OrderBlock);
				orderBlock2.order = updateOrder;
				orderBlock2.callback = (UnityAction)Delegate.Combine(orderBlock2.callback, callback);
				BeforeRenderHelper.s_OrderBlocks.Insert(num, orderBlock2);
			}
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00009CD0 File Offset: 0x00007ED0
		public static void UnregisterCallback(UnityAction callback)
		{
			int updateOrder = BeforeRenderHelper.GetUpdateOrder(callback);
			List<BeforeRenderHelper.OrderBlock> list = BeforeRenderHelper.s_OrderBlocks;
			lock (list)
			{
				int num = 0;
				while (num < BeforeRenderHelper.s_OrderBlocks.Count && BeforeRenderHelper.s_OrderBlocks[num].order <= updateOrder)
				{
					bool flag = BeforeRenderHelper.s_OrderBlocks[num].order == updateOrder;
					if (flag)
					{
						BeforeRenderHelper.OrderBlock orderBlock = BeforeRenderHelper.s_OrderBlocks[num];
						orderBlock.callback = (UnityAction)Delegate.Remove(orderBlock.callback, callback);
						BeforeRenderHelper.s_OrderBlocks[num] = orderBlock;
						bool flag2 = orderBlock.callback == null;
						if (flag2)
						{
							BeforeRenderHelper.s_OrderBlocks.RemoveAt(num);
						}
						break;
					}
					num++;
				}
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00009DB0 File Offset: 0x00007FB0
		public static void Invoke()
		{
			List<BeforeRenderHelper.OrderBlock> list = BeforeRenderHelper.s_OrderBlocks;
			lock (list)
			{
				for (int i = 0; i < BeforeRenderHelper.s_OrderBlocks.Count; i++)
				{
					UnityAction callback = BeforeRenderHelper.s_OrderBlocks[i].callback;
					bool flag = callback != null;
					if (flag)
					{
						callback();
					}
				}
			}
		}

		// Token: 0x04000253 RID: 595
		private static List<BeforeRenderHelper.OrderBlock> s_OrderBlocks = new List<BeforeRenderHelper.OrderBlock>();

		// Token: 0x020000D3 RID: 211
		private struct OrderBlock
		{
			// Token: 0x04000254 RID: 596
			internal int order;

			// Token: 0x04000255 RID: 597
			internal UnityAction callback;
		}
	}
}
