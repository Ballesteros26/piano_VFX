using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200007C RID: 124
	public struct UQueryState<T> : IEquatable<UQueryState<T>> where T : VisualElement
	{
		// Token: 0x060002DB RID: 731 RVA: 0x0000A929 File Offset: 0x00008B29
		internal UQueryState(VisualElement element, List<RuleMatcher> matchers)
		{
			this.m_Element = element;
			this.m_Matchers = matchers;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000A93C File Offset: 0x00008B3C
		public UQueryState<T> RebuildOn(VisualElement element)
		{
			return new UQueryState<T>(element, this.m_Matchers);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000A95C File Offset: 0x00008B5C
		public T First()
		{
			UQueryState<T>.s_First.Run(this.m_Element, this.m_Matchers);
			T t = UQueryState<T>.s_First.match as T;
			UQueryState<T>.s_First.match = null;
			return t;
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000A9A8 File Offset: 0x00008BA8
		public T Last()
		{
			UQueryState<T>.s_Last.Run(this.m_Element, this.m_Matchers);
			T t = UQueryState<T>.s_Last.match as T;
			UQueryState<T>.s_Last.match = null;
			return t;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000A9F3 File Offset: 0x00008BF3
		public void ToList(List<T> results)
		{
			UQueryState<T>.s_List.matches = results;
			UQueryState<T>.s_List.Run(this.m_Element, this.m_Matchers);
			UQueryState<T>.s_List.Reset();
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000AA24 File Offset: 0x00008C24
		public List<T> ToList()
		{
			List<T> list = new List<T>();
			this.ToList(list);
			return list;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000AA48 File Offset: 0x00008C48
		public T AtIndex(int index)
		{
			UQueryState<T>.s_Index.matchIndex = index;
			UQueryState<T>.s_Index.Run(this.m_Element, this.m_Matchers);
			T t = UQueryState<T>.s_Index.match as T;
			UQueryState<T>.s_Index.match = null;
			return t;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000AAA0 File Offset: 0x00008CA0
		public void ForEach(Action<T> funcCall)
		{
			UQueryState<T>.ActionQueryMatcher actionQueryMatcher = UQueryState<T>.s_Action;
			bool flag = actionQueryMatcher.callBack != null;
			if (flag)
			{
				actionQueryMatcher = new UQueryState<T>.ActionQueryMatcher();
			}
			try
			{
				actionQueryMatcher.callBack = funcCall;
				actionQueryMatcher.Run(this.m_Element, this.m_Matchers);
			}
			finally
			{
				actionQueryMatcher.callBack = null;
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000AB04 File Offset: 0x00008D04
		public void ForEach<T2>(List<T2> result, Func<T, T2> funcCall)
		{
			UQueryState<T>.DelegateQueryMatcher<T2> delegateQueryMatcher = UQueryState<T>.DelegateQueryMatcher<T2>.s_Instance;
			bool flag = delegateQueryMatcher.callBack != null;
			if (flag)
			{
				delegateQueryMatcher = new UQueryState<T>.DelegateQueryMatcher<T2>();
			}
			try
			{
				delegateQueryMatcher.callBack = funcCall;
				delegateQueryMatcher.result = result;
				delegateQueryMatcher.Run(this.m_Element, this.m_Matchers);
			}
			finally
			{
				delegateQueryMatcher.callBack = null;
				delegateQueryMatcher.result = null;
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000AB78 File Offset: 0x00008D78
		public List<T2> ForEach<T2>(Func<T, T2> funcCall)
		{
			List<T2> list = new List<T2>();
			this.ForEach<T2>(list, funcCall);
			return list;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000AB9C File Offset: 0x00008D9C
		public bool Equals(UQueryState<T> other)
		{
			return this.m_Element == other.m_Element && EqualityComparer<List<RuleMatcher>>.Default.Equals(this.m_Matchers, other.m_Matchers);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000ABD8 File Offset: 0x00008DD8
		public override bool Equals(object obj)
		{
			bool flag = !(obj is UQueryState<T>);
			return !flag && this.Equals((UQueryState<T>)obj);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000AC0C File Offset: 0x00008E0C
		public override int GetHashCode()
		{
			int num = 488160421;
			num = num * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.m_Element);
			return num * -1521134295 + EqualityComparer<List<RuleMatcher>>.Default.GetHashCode(this.m_Matchers);
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000AC58 File Offset: 0x00008E58
		public static bool operator ==(UQueryState<T> state1, UQueryState<T> state2)
		{
			return state1.Equals(state2);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000AC74 File Offset: 0x00008E74
		public static bool operator !=(UQueryState<T> state1, UQueryState<T> state2)
		{
			return !(state1 == state2);
		}

		// Token: 0x04000165 RID: 357
		private static UQuery.FirstQueryMatcher s_First = new UQuery.FirstQueryMatcher();

		// Token: 0x04000166 RID: 358
		private static UQuery.LastQueryMatcher s_Last = new UQuery.LastQueryMatcher();

		// Token: 0x04000167 RID: 359
		private static UQuery.IndexQueryMatcher s_Index = new UQuery.IndexQueryMatcher();

		// Token: 0x04000168 RID: 360
		private static UQueryState<T>.ActionQueryMatcher s_Action = new UQueryState<T>.ActionQueryMatcher();

		// Token: 0x04000169 RID: 361
		private readonly VisualElement m_Element;

		// Token: 0x0400016A RID: 362
		internal readonly List<RuleMatcher> m_Matchers;

		// Token: 0x0400016B RID: 363
		private static readonly UQueryState<T>.ListQueryMatcher s_List = new UQueryState<T>.ListQueryMatcher();

		// Token: 0x0200007D RID: 125
		private class ListQueryMatcher : UQuery.UQueryMatcher
		{
			// Token: 0x17000093 RID: 147
			// (get) Token: 0x060002EB RID: 747 RVA: 0x0000ACC4 File Offset: 0x00008EC4
			// (set) Token: 0x060002EC RID: 748 RVA: 0x0000ACCC File Offset: 0x00008ECC
			public List<T> matches { get; set; }

			// Token: 0x060002ED RID: 749 RVA: 0x0000ACD8 File Offset: 0x00008ED8
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				this.matches.Add(element as T);
				return false;
			}

			// Token: 0x060002EE RID: 750 RVA: 0x0000AD02 File Offset: 0x00008F02
			public void Reset()
			{
				this.matches = null;
			}
		}

		// Token: 0x0200007E RID: 126
		private class ActionQueryMatcher : UQuery.UQueryMatcher
		{
			// Token: 0x17000094 RID: 148
			// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000AD0D File Offset: 0x00008F0D
			// (set) Token: 0x060002F1 RID: 753 RVA: 0x0000AD15 File Offset: 0x00008F15
			internal Action<T> callBack { get; set; }

			// Token: 0x060002F2 RID: 754 RVA: 0x0000AD20 File Offset: 0x00008F20
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				T t = element as T;
				bool flag = t != null;
				if (flag)
				{
					this.callBack.Invoke(t);
				}
				return false;
			}
		}

		// Token: 0x0200007F RID: 127
		private class DelegateQueryMatcher<TReturnType> : UQuery.UQueryMatcher
		{
			// Token: 0x17000095 RID: 149
			// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000AD5B File Offset: 0x00008F5B
			// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000AD63 File Offset: 0x00008F63
			public Func<T, TReturnType> callBack { get; set; }

			// Token: 0x17000096 RID: 150
			// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000AD6C File Offset: 0x00008F6C
			// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000AD74 File Offset: 0x00008F74
			public List<TReturnType> result { get; set; }

			// Token: 0x060002F8 RID: 760 RVA: 0x0000AD80 File Offset: 0x00008F80
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				T t = element as T;
				bool flag = t != null;
				if (flag)
				{
					this.result.Add(this.callBack.Invoke(t));
				}
				return false;
			}

			// Token: 0x04000170 RID: 368
			public static UQueryState<T>.DelegateQueryMatcher<TReturnType> s_Instance = new UQueryState<T>.DelegateQueryMatcher<TReturnType>();
		}
	}
}
