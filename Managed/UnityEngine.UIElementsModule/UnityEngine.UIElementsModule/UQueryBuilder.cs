using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000080 RID: 128
	public struct UQueryBuilder<T> : IEquatable<UQueryBuilder<T>> where T : VisualElement
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000ADD4 File Offset: 0x00008FD4
		private List<StyleSelector> styleSelectors
		{
			get
			{
				List<StyleSelector> list;
				if ((list = this.m_StyleSelectors) == null)
				{
					list = (this.m_StyleSelectors = new List<StyleSelector>());
				}
				return list;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000AE00 File Offset: 0x00009000
		private List<StyleSelectorPart> parts
		{
			get
			{
				List<StyleSelectorPart> list;
				if ((list = this.m_Parts) == null)
				{
					list = (this.m_Parts = new List<StyleSelectorPart>());
				}
				return list;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000AE2C File Offset: 0x0000902C
		public UQueryBuilder(VisualElement visualElement)
		{
			this = default(UQueryBuilder<T>);
			this.m_Element = visualElement;
			this.m_Parts = null;
			this.m_StyleSelectors = null;
			this.m_Relationship = StyleSelectorRelationship.None;
			this.m_Matchers = new List<RuleMatcher>();
			this.pseudoStatesMask = (this.negatedPseudoStatesMask = 0);
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000AE78 File Offset: 0x00009078
		public UQueryBuilder<T> Class(string classname)
		{
			this.AddClass(classname);
			return this;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000AE98 File Offset: 0x00009098
		public UQueryBuilder<T> Name(string id)
		{
			this.AddName(id);
			return this;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000AEB8 File Offset: 0x000090B8
		public UQueryBuilder<T2> Descendents<T2>(string name = null, params string[] classNames) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClasses(classNames);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Descendent);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000AEF0 File Offset: 0x000090F0
		public UQueryBuilder<T2> Descendents<T2>(string name = null, string classname = null) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClass(classname);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Descendent);
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0000AF28 File Offset: 0x00009128
		public UQueryBuilder<T2> Children<T2>(string name = null, params string[] classes) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClasses(classes);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Child);
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0000AF60 File Offset: 0x00009160
		public UQueryBuilder<T2> Children<T2>(string name = null, string className = null) where T2 : VisualElement
		{
			this.FinishCurrentSelector();
			this.AddType<T2>();
			this.AddName(name);
			this.AddClass(className);
			return this.AddRelationship<T2>(StyleSelectorRelationship.Child);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x0000AF98 File Offset: 0x00009198
		public UQueryBuilder<T2> OfType<T2>(string name = null, params string[] classes) where T2 : VisualElement
		{
			this.AddType<T2>();
			this.AddName(name);
			this.AddClasses(classes);
			return this.AddRelationship<T2>(StyleSelectorRelationship.None);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0000AFC8 File Offset: 0x000091C8
		public UQueryBuilder<T2> OfType<T2>(string name = null, string className = null) where T2 : VisualElement
		{
			this.AddType<T2>();
			this.AddName(name);
			this.AddClass(className);
			return this.AddRelationship<T2>(StyleSelectorRelationship.None);
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0000AFF8 File Offset: 0x000091F8
		internal UQueryBuilder<T> SingleBaseType()
		{
			this.parts.Add(StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance));
			return this;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x0000B028 File Offset: 0x00009228
		public UQueryBuilder<T> Where(Func<T, bool> selectorPredicate)
		{
			this.parts.Add(StyleSelectorPart.CreatePredicate(new UQuery.PredicateWrapper<T>(selectorPredicate)));
			return this;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0000B058 File Offset: 0x00009258
		private void AddClass(string c)
		{
			bool flag = c != null;
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreateClass(c));
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000B080 File Offset: 0x00009280
		private void AddClasses(params string[] classes)
		{
			bool flag = classes != null;
			if (flag)
			{
				for (int i = 0; i < classes.Length; i++)
				{
					this.AddClass(classes[i]);
				}
			}
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000B0B4 File Offset: 0x000092B4
		private void AddName(string id)
		{
			bool flag = id != null;
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreateId(id));
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000B0DC File Offset: 0x000092DC
		private void AddType<T2>() where T2 : VisualElement
		{
			bool flag = typeof(T2) != typeof(VisualElement);
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T2>.s_Instance));
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000B120 File Offset: 0x00009320
		private UQueryBuilder<T> AddPseudoState(PseudoStates s)
		{
			this.pseudoStatesMask |= (int)s;
			return this;
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000B148 File Offset: 0x00009348
		private UQueryBuilder<T> AddNegativePseudoState(PseudoStates s)
		{
			this.negatedPseudoStatesMask |= (int)s;
			return this;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000B170 File Offset: 0x00009370
		public UQueryBuilder<T> Active()
		{
			return this.AddPseudoState(PseudoStates.Active);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000B18C File Offset: 0x0000938C
		public UQueryBuilder<T> NotActive()
		{
			return this.AddNegativePseudoState(PseudoStates.Active);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000B1A8 File Offset: 0x000093A8
		public UQueryBuilder<T> Visible()
		{
			return this.Where((T e) => e.visible);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000B1E0 File Offset: 0x000093E0
		public UQueryBuilder<T> NotVisible()
		{
			return this.Where((T e) => !e.visible);
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000B218 File Offset: 0x00009418
		public UQueryBuilder<T> Hovered()
		{
			return this.AddPseudoState(PseudoStates.Hover);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000B234 File Offset: 0x00009434
		public UQueryBuilder<T> NotHovered()
		{
			return this.AddNegativePseudoState(PseudoStates.Hover);
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000B250 File Offset: 0x00009450
		public UQueryBuilder<T> Checked()
		{
			return this.AddPseudoState(PseudoStates.Checked);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000B26C File Offset: 0x0000946C
		public UQueryBuilder<T> NotChecked()
		{
			return this.AddNegativePseudoState(PseudoStates.Checked);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000B288 File Offset: 0x00009488
		[Obsolete("Use Checked() instead")]
		public UQueryBuilder<T> Selected()
		{
			return this.AddPseudoState(PseudoStates.Checked);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000B2A4 File Offset: 0x000094A4
		[Obsolete("Use NotChecked() instead")]
		public UQueryBuilder<T> NotSelected()
		{
			return this.AddNegativePseudoState(PseudoStates.Checked);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000B2C0 File Offset: 0x000094C0
		public UQueryBuilder<T> Enabled()
		{
			return this.AddNegativePseudoState(PseudoStates.Disabled);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000B2DC File Offset: 0x000094DC
		public UQueryBuilder<T> NotEnabled()
		{
			return this.AddPseudoState(PseudoStates.Disabled);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000B2F8 File Offset: 0x000094F8
		public UQueryBuilder<T> Focused()
		{
			return this.AddPseudoState(PseudoStates.Focus);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000B314 File Offset: 0x00009514
		public UQueryBuilder<T> NotFocused()
		{
			return this.AddNegativePseudoState(PseudoStates.Focus);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000B330 File Offset: 0x00009530
		private UQueryBuilder<T2> AddRelationship<T2>(StyleSelectorRelationship relationship) where T2 : VisualElement
		{
			return new UQueryBuilder<T2>(this.m_Element)
			{
				m_Matchers = this.m_Matchers,
				m_Parts = this.m_Parts,
				m_StyleSelectors = this.m_StyleSelectors,
				m_Relationship = ((relationship == StyleSelectorRelationship.None) ? this.m_Relationship : relationship),
				pseudoStatesMask = this.pseudoStatesMask,
				negatedPseudoStatesMask = this.negatedPseudoStatesMask
			};
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000B3A4 File Offset: 0x000095A4
		private void AddPseudoStatesRuleIfNecessasy()
		{
			bool flag = this.pseudoStatesMask != 0 || this.negatedPseudoStatesMask != 0;
			if (flag)
			{
				this.parts.Add(new StyleSelectorPart
				{
					type = StyleSelectorType.PseudoClass
				});
			}
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000B3EC File Offset: 0x000095EC
		private void FinishSelector()
		{
			this.FinishCurrentSelector();
			bool flag = this.styleSelectors.Count > 0;
			if (flag)
			{
				StyleComplexSelector styleComplexSelector = new StyleComplexSelector();
				styleComplexSelector.selectors = this.styleSelectors.ToArray();
				this.styleSelectors.Clear();
				this.m_Matchers.Add(new RuleMatcher
				{
					complexSelector = styleComplexSelector
				});
			}
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000B458 File Offset: 0x00009658
		private bool CurrentSelectorEmpty()
		{
			return this.parts.Count == 0 && this.m_Relationship == StyleSelectorRelationship.None && this.pseudoStatesMask == 0 && this.negatedPseudoStatesMask == 0;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000B494 File Offset: 0x00009694
		private void FinishCurrentSelector()
		{
			bool flag = !this.CurrentSelectorEmpty();
			if (flag)
			{
				StyleSelector styleSelector = new StyleSelector();
				styleSelector.previousRelationship = this.m_Relationship;
				this.AddPseudoStatesRuleIfNecessasy();
				styleSelector.parts = this.m_Parts.ToArray();
				styleSelector.pseudoStateMask = this.pseudoStatesMask;
				styleSelector.negatedPseudoStateMask = this.negatedPseudoStatesMask;
				this.styleSelectors.Add(styleSelector);
				this.m_Parts.Clear();
				this.pseudoStatesMask = (this.negatedPseudoStatesMask = 0);
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000B520 File Offset: 0x00009720
		public UQueryState<T> Build()
		{
			this.FinishSelector();
			return new UQueryState<T>(this.m_Element, this.m_Matchers);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000B54C File Offset: 0x0000974C
		public static implicit operator T(UQueryBuilder<T> s)
		{
			return s.First();
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000B568 File Offset: 0x00009768
		public static bool operator ==(UQueryBuilder<T> builder1, UQueryBuilder<T> builder2)
		{
			return builder1.Equals(builder2);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000B584 File Offset: 0x00009784
		public static bool operator !=(UQueryBuilder<T> builder1, UQueryBuilder<T> builder2)
		{
			return !(builder1 == builder2);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000B5A0 File Offset: 0x000097A0
		public T First()
		{
			return this.Build().First();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000B5C0 File Offset: 0x000097C0
		public T Last()
		{
			return this.Build().Last();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000B5E0 File Offset: 0x000097E0
		public List<T> ToList()
		{
			return this.Build().ToList();
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000B600 File Offset: 0x00009800
		public void ToList(List<T> results)
		{
			this.Build().ToList(results);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000B620 File Offset: 0x00009820
		public T AtIndex(int index)
		{
			return this.Build().AtIndex(index);
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000B644 File Offset: 0x00009844
		public void ForEach<T2>(List<T2> result, Func<T, T2> funcCall)
		{
			this.Build().ForEach<T2>(result, funcCall);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000B664 File Offset: 0x00009864
		public List<T2> ForEach<T2>(Func<T, T2> funcCall)
		{
			return this.Build().ForEach<T2>(funcCall);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000B688 File Offset: 0x00009888
		public void ForEach(Action<T> funcCall)
		{
			this.Build().ForEach(funcCall);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000B6A8 File Offset: 0x000098A8
		public bool Equals(UQueryBuilder<T> other)
		{
			return EqualityComparer<List<StyleSelector>>.Default.Equals(this.m_StyleSelectors, other.m_StyleSelectors) && EqualityComparer<List<StyleSelector>>.Default.Equals(this.styleSelectors, other.styleSelectors) && EqualityComparer<List<StyleSelectorPart>>.Default.Equals(this.m_Parts, other.m_Parts) && EqualityComparer<List<StyleSelectorPart>>.Default.Equals(this.parts, other.parts) && this.m_Element == other.m_Element && EqualityComparer<List<RuleMatcher>>.Default.Equals(this.m_Matchers, other.m_Matchers) && this.m_Relationship == other.m_Relationship && this.pseudoStatesMask == other.pseudoStatesMask && this.negatedPseudoStatesMask == other.negatedPseudoStatesMask;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000B778 File Offset: 0x00009978
		public override bool Equals(object obj)
		{
			bool flag = !(obj is UQueryBuilder<T>);
			return !flag && this.Equals((UQueryBuilder<T>)obj);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000B7AC File Offset: 0x000099AC
		public override int GetHashCode()
		{
			int num = -949812380;
			num = num * -1521134295 + EqualityComparer<List<StyleSelector>>.Default.GetHashCode(this.m_StyleSelectors);
			num = num * -1521134295 + EqualityComparer<List<StyleSelector>>.Default.GetHashCode(this.styleSelectors);
			num = num * -1521134295 + EqualityComparer<List<StyleSelectorPart>>.Default.GetHashCode(this.m_Parts);
			num = num * -1521134295 + EqualityComparer<List<StyleSelectorPart>>.Default.GetHashCode(this.parts);
			num = num * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.m_Element);
			num = num * -1521134295 + EqualityComparer<List<RuleMatcher>>.Default.GetHashCode(this.m_Matchers);
			num = num * -1521134295 + this.m_Relationship.GetHashCode();
			num = num * -1521134295 + this.pseudoStatesMask.GetHashCode();
			return num * -1521134295 + this.negatedPseudoStatesMask.GetHashCode();
		}

		// Token: 0x04000171 RID: 369
		private List<StyleSelector> m_StyleSelectors;

		// Token: 0x04000172 RID: 370
		private List<StyleSelectorPart> m_Parts;

		// Token: 0x04000173 RID: 371
		private VisualElement m_Element;

		// Token: 0x04000174 RID: 372
		private List<RuleMatcher> m_Matchers;

		// Token: 0x04000175 RID: 373
		private StyleSelectorRelationship m_Relationship;

		// Token: 0x04000176 RID: 374
		private int pseudoStatesMask;

		// Token: 0x04000177 RID: 375
		private int negatedPseudoStatesMask;
	}
}
