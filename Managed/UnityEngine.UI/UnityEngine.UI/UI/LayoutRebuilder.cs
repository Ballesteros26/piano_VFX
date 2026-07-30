using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace UnityEngine.UI
{
	// Token: 0x02000026 RID: 38
	public class LayoutRebuilder : ICanvasElement
	{
		// Token: 0x0600029B RID: 667 RVA: 0x0000E239 File Offset: 0x0000C439
		private void Initialize(RectTransform controller)
		{
			this.m_ToRebuild = controller;
			this.m_CachedHashFromTransform = controller.GetHashCode();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000E24E File Offset: 0x0000C44E
		private void Clear()
		{
			this.m_ToRebuild = null;
			this.m_CachedHashFromTransform = 0;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000E25E File Offset: 0x0000C45E
		static LayoutRebuilder()
		{
			RectTransform.reapplyDrivenProperties += LayoutRebuilder.ReapplyDrivenProperties;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000E28C File Offset: 0x0000C48C
		private static void ReapplyDrivenProperties(RectTransform driven)
		{
			LayoutRebuilder.MarkLayoutForRebuild(driven);
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000E294 File Offset: 0x0000C494
		public Transform transform
		{
			get
			{
				return this.m_ToRebuild;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000E29C File Offset: 0x0000C49C
		public bool IsDestroyed()
		{
			return this.m_ToRebuild == null;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000E2AA File Offset: 0x0000C4AA
		private static void StripDisabledBehavioursFromList(List<Component> components)
		{
			components.RemoveAll((Component e) => e is Behaviour && !((Behaviour)e).isActiveAndEnabled);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000E2D4 File Offset: 0x0000C4D4
		public static void ForceRebuildLayoutImmediate(RectTransform layoutRoot)
		{
			LayoutRebuilder layoutRebuilder = LayoutRebuilder.s_Rebuilders.Get();
			layoutRebuilder.Initialize(layoutRoot);
			layoutRebuilder.Rebuild(CanvasUpdate.Layout);
			LayoutRebuilder.s_Rebuilders.Release(layoutRebuilder);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000E308 File Offset: 0x0000C508
		public void Rebuild(CanvasUpdate executing)
		{
			if (executing == CanvasUpdate.Layout)
			{
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputHorizontal();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutHorizontal();
				});
				this.PerformLayoutCalculation(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutElement).CalculateLayoutInputVertical();
				});
				this.PerformLayoutControl(this.m_ToRebuild, delegate(Component e)
				{
					(e as ILayoutController).SetLayoutVertical();
				});
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000E3C8 File Offset: 0x0000C5C8
		private void PerformLayoutControl(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = ListPool<Component>.Get();
			rect.GetComponents(typeof(ILayoutController), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					if (list[i] is ILayoutSelfController)
					{
						action(list[i]);
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					if (!(list[j] is ILayoutSelfController))
					{
						Component component = list[j];
						if (component && component is ScrollRect)
						{
							if (((ScrollRect)component).content != rect)
							{
								action(list[j]);
							}
						}
						else
						{
							action(list[j]);
						}
					}
				}
				for (int k = 0; k < rect.childCount; k++)
				{
					this.PerformLayoutControl(rect.GetChild(k) as RectTransform, action);
				}
			}
			ListPool<Component>.Release(list);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000E4CC File Offset: 0x0000C6CC
		private void PerformLayoutCalculation(RectTransform rect, UnityAction<Component> action)
		{
			if (rect == null)
			{
				return;
			}
			List<Component> list = ListPool<Component>.Get();
			rect.GetComponents(typeof(ILayoutElement), list);
			LayoutRebuilder.StripDisabledBehavioursFromList(list);
			if (list.Count > 0 || rect.GetComponent(typeof(ILayoutGroup)))
			{
				for (int i = 0; i < rect.childCount; i++)
				{
					this.PerformLayoutCalculation(rect.GetChild(i) as RectTransform, action);
				}
				for (int j = 0; j < list.Count; j++)
				{
					action(list[j]);
				}
			}
			ListPool<Component>.Release(list);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000E568 File Offset: 0x0000C768
		public static void MarkLayoutForRebuild(RectTransform rect)
		{
			if (rect == null || rect.gameObject == null)
			{
				return;
			}
			List<Component> list = ListPool<Component>.Get();
			bool flag = true;
			RectTransform rectTransform = rect;
			RectTransform rectTransform2 = rectTransform.parent as RectTransform;
			while (flag && !(rectTransform2 == null) && !(rectTransform2.gameObject == null))
			{
				flag = false;
				rectTransform2.GetComponents(typeof(ILayoutGroup), list);
				for (int i = 0; i < list.Count; i++)
				{
					Component component = list[i];
					if (component != null && component is Behaviour && ((Behaviour)component).isActiveAndEnabled)
					{
						flag = true;
						rectTransform = rectTransform2;
						break;
					}
				}
				rectTransform2 = rectTransform2.parent as RectTransform;
			}
			if (rectTransform == rect && !LayoutRebuilder.ValidController(rectTransform, list))
			{
				ListPool<Component>.Release(list);
				return;
			}
			LayoutRebuilder.MarkLayoutRootForRebuild(rectTransform);
			ListPool<Component>.Release(list);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000E64C File Offset: 0x0000C84C
		private static bool ValidController(RectTransform layoutRoot, List<Component> comps)
		{
			if (layoutRoot == null || layoutRoot.gameObject == null)
			{
				return false;
			}
			layoutRoot.GetComponents(typeof(ILayoutController), comps);
			for (int i = 0; i < comps.Count; i++)
			{
				Component component = comps[i];
				if (component != null && component is Behaviour && ((Behaviour)component).isActiveAndEnabled)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000E6C0 File Offset: 0x0000C8C0
		private static void MarkLayoutRootForRebuild(RectTransform controller)
		{
			if (controller == null)
			{
				return;
			}
			LayoutRebuilder layoutRebuilder = LayoutRebuilder.s_Rebuilders.Get();
			layoutRebuilder.Initialize(controller);
			if (!CanvasUpdateRegistry.TryRegisterCanvasElementForLayoutRebuild(layoutRebuilder))
			{
				LayoutRebuilder.s_Rebuilders.Release(layoutRebuilder);
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000E6FC File Offset: 0x0000C8FC
		public void LayoutComplete()
		{
			LayoutRebuilder.s_Rebuilders.Release(this);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00004C7A File Offset: 0x00002E7A
		public void GraphicUpdateComplete()
		{
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000E709 File Offset: 0x0000C909
		public override int GetHashCode()
		{
			return this.m_CachedHashFromTransform;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000E711 File Offset: 0x0000C911
		public override bool Equals(object obj)
		{
			return obj.GetHashCode() == this.GetHashCode();
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000E721 File Offset: 0x0000C921
		public override string ToString()
		{
			return "(Layout Rebuilder for) " + this.m_ToRebuild;
		}

		// Token: 0x040000E8 RID: 232
		private RectTransform m_ToRebuild;

		// Token: 0x040000E9 RID: 233
		private int m_CachedHashFromTransform;

		// Token: 0x040000EA RID: 234
		private static ObjectPool<LayoutRebuilder> s_Rebuilders = new ObjectPool<LayoutRebuilder>(null, delegate(LayoutRebuilder x)
		{
			x.Clear();
		});
	}
}
