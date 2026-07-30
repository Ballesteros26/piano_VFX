using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000072 RID: 114
	public static class UQuery
	{
		// Token: 0x02000073 RID: 115
		internal interface IVisualPredicateWrapper
		{
			// Token: 0x060002BF RID: 703
			bool Predicate(object e);
		}

		// Token: 0x02000074 RID: 116
		internal class IsOfType<T> : UQuery.IVisualPredicateWrapper where T : VisualElement
		{
			// Token: 0x060002C0 RID: 704 RVA: 0x0000A684 File Offset: 0x00008884
			public bool Predicate(object e)
			{
				return e is T;
			}

			// Token: 0x0400015D RID: 349
			public static UQuery.IsOfType<T> s_Instance = new UQuery.IsOfType<T>();
		}

		// Token: 0x02000075 RID: 117
		internal class PredicateWrapper<T> : UQuery.IVisualPredicateWrapper where T : VisualElement
		{
			// Token: 0x060002C3 RID: 707 RVA: 0x0000A6AB File Offset: 0x000088AB
			public PredicateWrapper(Func<T, bool> p)
			{
				this.predicate = p;
			}

			// Token: 0x060002C4 RID: 708 RVA: 0x0000A6BC File Offset: 0x000088BC
			public bool Predicate(object e)
			{
				T t = e as T;
				bool flag = t != null;
				return flag && this.predicate.Invoke(t);
			}

			// Token: 0x0400015E RID: 350
			private Func<T, bool> predicate;
		}

		// Token: 0x02000076 RID: 118
		internal abstract class UQueryMatcher : HierarchyTraversal
		{
			// Token: 0x060002C6 RID: 710 RVA: 0x0000A702 File Offset: 0x00008902
			public override void Traverse(VisualElement element)
			{
				base.Traverse(element);
			}

			// Token: 0x060002C7 RID: 711 RVA: 0x0000A710 File Offset: 0x00008910
			protected virtual bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				return false;
			}

			// Token: 0x060002C8 RID: 712 RVA: 0x000062F3 File Offset: 0x000044F3
			private static void NoProcessResult(VisualElement e, MatchResultInfo i)
			{
			}

			// Token: 0x060002C9 RID: 713 RVA: 0x0000A724 File Offset: 0x00008924
			public override void TraverseRecursive(VisualElement element, int depth)
			{
				int count = this.m_Matchers.Count;
				int count2 = this.m_Matchers.Count;
				for (int j = 0; j < count2; j++)
				{
					RuleMatcher ruleMatcher = this.m_Matchers[j];
					bool flag = StyleSelectorHelper.MatchRightToLeft(element, ruleMatcher.complexSelector, delegate(VisualElement e, MatchResultInfo i)
					{
						UQuery.UQueryMatcher.NoProcessResult(e, i);
					});
					if (flag)
					{
						bool flag2 = this.OnRuleMatchedElement(ruleMatcher, element);
						if (flag2)
						{
							return;
						}
					}
				}
				base.Recurse(element, depth);
				bool flag3 = this.m_Matchers.Count > count;
				if (flag3)
				{
					this.m_Matchers.RemoveRange(count, this.m_Matchers.Count - count);
					return;
				}
			}

			// Token: 0x060002CA RID: 714 RVA: 0x0000A7E8 File Offset: 0x000089E8
			public virtual void Run(VisualElement root, List<RuleMatcher> matchers)
			{
				this.m_Matchers = matchers;
				this.Traverse(root);
			}

			// Token: 0x0400015F RID: 351
			internal List<RuleMatcher> m_Matchers;
		}

		// Token: 0x02000078 RID: 120
		internal abstract class SingleQueryMatcher : UQuery.UQueryMatcher
		{
			// Token: 0x17000091 RID: 145
			// (get) Token: 0x060002CE RID: 718 RVA: 0x0000A810 File Offset: 0x00008A10
			// (set) Token: 0x060002CF RID: 719 RVA: 0x0000A818 File Offset: 0x00008A18
			public VisualElement match { get; set; }

			// Token: 0x060002D0 RID: 720 RVA: 0x0000A821 File Offset: 0x00008A21
			public override void Run(VisualElement root, List<RuleMatcher> matchers)
			{
				this.match = null;
				base.Run(root, matchers);
			}
		}

		// Token: 0x02000079 RID: 121
		internal class FirstQueryMatcher : UQuery.SingleQueryMatcher
		{
			// Token: 0x060002D2 RID: 722 RVA: 0x0000A840 File Offset: 0x00008A40
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				bool flag = base.match == null;
				if (flag)
				{
					base.match = element;
				}
				return true;
			}
		}

		// Token: 0x0200007A RID: 122
		internal class LastQueryMatcher : UQuery.SingleQueryMatcher
		{
			// Token: 0x060002D4 RID: 724 RVA: 0x0000A874 File Offset: 0x00008A74
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				base.match = element;
				return false;
			}
		}

		// Token: 0x0200007B RID: 123
		internal class IndexQueryMatcher : UQuery.SingleQueryMatcher
		{
			// Token: 0x17000092 RID: 146
			// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000A890 File Offset: 0x00008A90
			// (set) Token: 0x060002D7 RID: 727 RVA: 0x0000A8A8 File Offset: 0x00008AA8
			public int matchIndex
			{
				get
				{
					return this._matchIndex;
				}
				set
				{
					this.matchCount = -1;
					this._matchIndex = value;
				}
			}

			// Token: 0x060002D8 RID: 728 RVA: 0x0000A8B9 File Offset: 0x00008AB9
			public override void Run(VisualElement root, List<RuleMatcher> matchers)
			{
				this.matchCount = -1;
				base.Run(root, matchers);
			}

			// Token: 0x060002D9 RID: 729 RVA: 0x0000A8CC File Offset: 0x00008ACC
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				this.matchCount++;
				bool flag = this.matchCount == this._matchIndex;
				if (flag)
				{
					base.match = element;
				}
				return this.matchCount >= this._matchIndex;
			}

			// Token: 0x04000163 RID: 355
			private int matchCount = -1;

			// Token: 0x04000164 RID: 356
			private int _matchIndex;
		}
	}
}
