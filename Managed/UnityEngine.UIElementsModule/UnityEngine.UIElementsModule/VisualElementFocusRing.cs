using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200009E RID: 158
	public class VisualElementFocusRing : IFocusRing
	{
		// Token: 0x060004C3 RID: 1219 RVA: 0x0001211C File Offset: 0x0001031C
		public VisualElementFocusRing(VisualElement root, VisualElementFocusRing.DefaultFocusOrder dfo = VisualElementFocusRing.DefaultFocusOrder.ChildOrder)
		{
			this.defaultFocusOrder = dfo;
			this.root = root;
			this.m_FocusRing = new List<VisualElementFocusRing.FocusRingRecord>();
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00012140 File Offset: 0x00010340
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x00012148 File Offset: 0x00010348
		public VisualElementFocusRing.DefaultFocusOrder defaultFocusOrder { get; set; }

		// Token: 0x060004C6 RID: 1222 RVA: 0x00012154 File Offset: 0x00010354
		private int FocusRingAutoIndexSort(VisualElementFocusRing.FocusRingRecord a, VisualElementFocusRing.FocusRingRecord b)
		{
			int num;
			switch (this.defaultFocusOrder)
			{
			default:
				num = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			case VisualElementFocusRing.DefaultFocusOrder.PositionXY:
			{
				VisualElement visualElement = a.m_Focusable as VisualElement;
				VisualElement visualElement2 = b.m_Focusable as VisualElement;
				bool flag = visualElement != null && visualElement2 != null;
				if (flag)
				{
					bool flag2 = visualElement.layout.position.x < visualElement2.layout.position.x;
					if (flag2)
					{
						num = -1;
						break;
					}
					bool flag3 = visualElement.layout.position.x > visualElement2.layout.position.x;
					if (flag3)
					{
						num = 1;
						break;
					}
					bool flag4 = visualElement.layout.position.y < visualElement2.layout.position.y;
					if (flag4)
					{
						num = -1;
						break;
					}
					bool flag5 = visualElement.layout.position.y > visualElement2.layout.position.y;
					if (flag5)
					{
						num = 1;
						break;
					}
				}
				num = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			}
			case VisualElementFocusRing.DefaultFocusOrder.PositionYX:
			{
				VisualElement visualElement3 = a.m_Focusable as VisualElement;
				VisualElement visualElement4 = b.m_Focusable as VisualElement;
				bool flag6 = visualElement3 != null && visualElement4 != null;
				if (flag6)
				{
					bool flag7 = visualElement3.layout.position.y < visualElement4.layout.position.y;
					if (flag7)
					{
						num = -1;
						break;
					}
					bool flag8 = visualElement3.layout.position.y > visualElement4.layout.position.y;
					if (flag8)
					{
						num = 1;
						break;
					}
					bool flag9 = visualElement3.layout.position.x < visualElement4.layout.position.x;
					if (flag9)
					{
						num = -1;
						break;
					}
					bool flag10 = visualElement3.layout.position.x > visualElement4.layout.position.x;
					if (flag10)
					{
						num = 1;
						break;
					}
				}
				num = Comparer<int>.Default.Compare(a.m_AutoIndex, b.m_AutoIndex);
				break;
			}
			}
			return num;
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x000123F8 File Offset: 0x000105F8
		private int FocusRingSort(VisualElementFocusRing.FocusRingRecord a, VisualElementFocusRing.FocusRingRecord b)
		{
			bool flag = a.m_Focusable.tabIndex == 0 && b.m_Focusable.tabIndex == 0;
			int num;
			if (flag)
			{
				num = this.FocusRingAutoIndexSort(a, b);
			}
			else
			{
				bool flag2 = a.m_Focusable.tabIndex == 0;
				if (flag2)
				{
					num = 1;
				}
				else
				{
					bool flag3 = b.m_Focusable.tabIndex == 0;
					if (flag3)
					{
						num = -1;
					}
					else
					{
						int num2 = Comparer<int>.Default.Compare(a.m_Focusable.tabIndex, b.m_Focusable.tabIndex);
						bool flag4 = num2 == 0;
						if (flag4)
						{
							num2 = this.FocusRingAutoIndexSort(a, b);
						}
						num = num2;
					}
				}
			}
			return num;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000124A4 File Offset: 0x000106A4
		private void DoUpdate()
		{
			this.m_FocusRing.Clear();
			bool flag = this.root != null;
			if (flag)
			{
				List<VisualElementFocusRing.FocusRingRecord> list = new List<VisualElementFocusRing.FocusRingRecord>();
				int num = 0;
				this.BuildRingForScopeRecursive(this.root, ref num, list);
				this.SortAndFlattenScopeLists(list);
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000124F0 File Offset: 0x000106F0
		private void BuildRingForScopeRecursive(VisualElement ve, ref int scopeIndex, List<VisualElementFocusRing.FocusRingRecord> scopeList)
		{
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = visualElement.parent != null && visualElement == visualElement.parent.contentContainer;
				bool flag2 = visualElement.isCompositeRoot || flag;
				if (flag2)
				{
					VisualElementFocusRing.FocusRingRecord focusRingRecord = new VisualElementFocusRing.FocusRingRecord();
					int num = scopeIndex;
					scopeIndex = num + 1;
					focusRingRecord.m_AutoIndex = num;
					focusRingRecord.m_Focusable = visualElement;
					focusRingRecord.m_IsSlot = flag;
					focusRingRecord.m_ScopeNavigationOrder = new List<VisualElementFocusRing.FocusRingRecord>();
					VisualElementFocusRing.FocusRingRecord focusRingRecord2 = focusRingRecord;
					scopeList.Add(focusRingRecord2);
					int num2 = 0;
					this.BuildRingForScopeRecursive(visualElement, ref num2, focusRingRecord2.m_ScopeNavigationOrder);
				}
				else
				{
					bool flag3 = visualElement.canGrabFocus && visualElement.tabIndex >= 0;
					if (flag3)
					{
						VisualElementFocusRing.FocusRingRecord focusRingRecord3 = new VisualElementFocusRing.FocusRingRecord();
						int num = scopeIndex;
						scopeIndex = num + 1;
						focusRingRecord3.m_AutoIndex = num;
						focusRingRecord3.m_Focusable = visualElement;
						focusRingRecord3.m_IsSlot = false;
						focusRingRecord3.m_ScopeNavigationOrder = null;
						scopeList.Add(focusRingRecord3);
					}
					this.BuildRingForScopeRecursive(visualElement, ref scopeIndex, scopeList);
				}
			}
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00012614 File Offset: 0x00010814
		private void SortAndFlattenScopeLists(List<VisualElementFocusRing.FocusRingRecord> rootScopeList)
		{
			bool flag = rootScopeList != null;
			if (flag)
			{
				rootScopeList.Sort(new Comparison<VisualElementFocusRing.FocusRingRecord>(this.FocusRingSort));
				foreach (VisualElementFocusRing.FocusRingRecord focusRingRecord in rootScopeList)
				{
					bool flag2 = focusRingRecord.m_Focusable.canGrabFocus && focusRingRecord.m_Focusable.tabIndex >= 0;
					if (flag2)
					{
						bool flag3 = !focusRingRecord.m_Focusable.excludeFromFocusRing;
						if (flag3)
						{
							this.m_FocusRing.Add(focusRingRecord);
						}
						this.SortAndFlattenScopeLists(focusRingRecord.m_ScopeNavigationOrder);
					}
					else
					{
						bool isSlot = focusRingRecord.m_IsSlot;
						if (isSlot)
						{
							this.SortAndFlattenScopeLists(focusRingRecord.m_ScopeNavigationOrder);
						}
					}
					focusRingRecord.m_ScopeNavigationOrder = null;
				}
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00012704 File Offset: 0x00010904
		private int GetFocusableInternalIndex(Focusable f)
		{
			bool flag = f != null;
			if (flag)
			{
				for (int i = 0; i < this.m_FocusRing.Count; i++)
				{
					bool flag2 = f == this.m_FocusRing[i].m_Focusable;
					if (flag2)
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x0001275C File Offset: 0x0001095C
		public FocusChangeDirection GetFocusChangeDirection(Focusable currentFocusable, EventBase e)
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			bool flag2 = currentFocusable is IMGUIContainer && e.imguiEvent != null;
			FocusChangeDirection focusChangeDirection;
			if (flag2)
			{
				focusChangeDirection = FocusChangeDirection.none;
			}
			else
			{
				bool flag3 = e.eventTypeId == EventBase<KeyDownEvent>.TypeId();
				if (flag3)
				{
					KeyDownEvent keyDownEvent = e as KeyDownEvent;
					bool flag4 = keyDownEvent.character == '\u0019' || keyDownEvent.character == '\t';
					if (flag4)
					{
						bool flag5 = keyDownEvent.modifiers == EventModifiers.Shift;
						if (flag5)
						{
							return VisualElementFocusChangeDirection.left;
						}
						bool flag6 = keyDownEvent.modifiers == EventModifiers.None;
						if (flag6)
						{
							return VisualElementFocusChangeDirection.right;
						}
					}
				}
				focusChangeDirection = FocusChangeDirection.none;
			}
			return focusChangeDirection;
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00012818 File Offset: 0x00010A18
		public Focusable GetNextFocusable(Focusable currentFocusable, FocusChangeDirection direction)
		{
			bool flag = direction == FocusChangeDirection.none || direction == FocusChangeDirection.unspecified;
			Focusable focusable;
			if (flag)
			{
				focusable = currentFocusable;
			}
			else
			{
				this.DoUpdate();
				bool flag2 = this.m_FocusRing.Count == 0;
				if (flag2)
				{
					focusable = null;
				}
				else
				{
					int num = 0;
					bool flag3 = direction == VisualElementFocusChangeDirection.right;
					if (flag3)
					{
						num = this.GetFocusableInternalIndex(currentFocusable) + 1;
						bool flag4 = currentFocusable != null && num == 0;
						if (flag4)
						{
							return VisualElementFocusRing.GetNextFocusableInTree(currentFocusable as VisualElement);
						}
						bool flag5 = num == this.m_FocusRing.Count;
						if (flag5)
						{
							num = 0;
						}
						while (this.m_FocusRing[num].m_Focusable.delegatesFocus)
						{
							num++;
							bool flag6 = num == this.m_FocusRing.Count;
							if (flag6)
							{
								return null;
							}
						}
					}
					else
					{
						bool flag7 = direction == VisualElementFocusChangeDirection.left;
						if (flag7)
						{
							num = this.GetFocusableInternalIndex(currentFocusable) - 1;
							bool flag8 = currentFocusable != null && num == -2;
							if (flag8)
							{
								return VisualElementFocusRing.GetPreviousFocusableInTree(currentFocusable as VisualElement);
							}
							bool flag9 = num < 0;
							if (flag9)
							{
								num = this.m_FocusRing.Count - 1;
							}
							while (this.m_FocusRing[num].m_Focusable.delegatesFocus)
							{
								num--;
								bool flag10 = num == -1;
								if (flag10)
								{
									return null;
								}
							}
						}
					}
					focusable = this.m_FocusRing[num].m_Focusable;
				}
			}
			return focusable;
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x000129A4 File Offset: 0x00010BA4
		internal static Focusable GetNextFocusableInTree(VisualElement currentFocusable)
		{
			bool flag = currentFocusable == null;
			Focusable focusable;
			if (flag)
			{
				focusable = null;
			}
			else
			{
				VisualElement visualElement = currentFocusable.GetNextElementDepthFirst();
				while (!visualElement.canGrabFocus || visualElement.tabIndex < 0 || visualElement.excludeFromFocusRing)
				{
					visualElement = visualElement.GetNextElementDepthFirst();
					bool flag2 = visualElement == null;
					if (flag2)
					{
						visualElement = currentFocusable.GetRoot();
					}
					bool flag3 = visualElement == currentFocusable;
					if (flag3)
					{
						return currentFocusable;
					}
				}
				focusable = visualElement;
			}
			return focusable;
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00012A18 File Offset: 0x00010C18
		internal static Focusable GetPreviousFocusableInTree(VisualElement currentFocusable)
		{
			bool flag = currentFocusable == null;
			Focusable focusable;
			if (flag)
			{
				focusable = null;
			}
			else
			{
				VisualElement visualElement = currentFocusable.GetPreviousElementDepthFirst();
				while (!visualElement.canGrabFocus || visualElement.tabIndex < 0 || visualElement.excludeFromFocusRing)
				{
					visualElement = visualElement.GetPreviousElementDepthFirst();
					bool flag2 = visualElement == null;
					if (flag2)
					{
						visualElement = currentFocusable.GetRoot();
						while (visualElement.childCount > 0)
						{
							visualElement = visualElement.hierarchy.ElementAt(visualElement.childCount - 1);
						}
					}
					bool flag3 = visualElement == currentFocusable;
					if (flag3)
					{
						return currentFocusable;
					}
				}
				focusable = visualElement;
			}
			return focusable;
		}

		// Token: 0x040001F7 RID: 503
		private readonly VisualElement root;

		// Token: 0x040001F9 RID: 505
		private List<VisualElementFocusRing.FocusRingRecord> m_FocusRing;

		// Token: 0x0200009F RID: 159
		public enum DefaultFocusOrder
		{
			// Token: 0x040001FB RID: 507
			ChildOrder,
			// Token: 0x040001FC RID: 508
			PositionXY,
			// Token: 0x040001FD RID: 509
			PositionYX
		}

		// Token: 0x020000A0 RID: 160
		private class FocusRingRecord
		{
			// Token: 0x040001FE RID: 510
			public int m_AutoIndex;

			// Token: 0x040001FF RID: 511
			public Focusable m_Focusable;

			// Token: 0x04000200 RID: 512
			public bool m_IsSlot;

			// Token: 0x04000201 RID: 513
			public List<VisualElementFocusRing.FocusRingRecord> m_ScopeNavigationOrder;
		}
	}
}
