using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020000AE RID: 174
	internal class VisualTreeStyleUpdaterTraversal : HierarchyTraversal
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x00013682 File Offset: 0x00011882
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0001368A File Offset: 0x0001188A
		private float currentPixelsPerPoint { get; set; } = 1f;

		// Token: 0x06000519 RID: 1305 RVA: 0x00013693 File Offset: 0x00011893
		public void PrepareTraversal(float pixelsPerPoint)
		{
			this.currentPixelsPerPoint = pixelsPerPoint;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001369E File Offset: 0x0001189E
		public void AddChangedElement(VisualElement ve)
		{
			this.m_UpdateList.Add(ve);
			this.PropagateToChildren(ve);
			this.PropagateToParents(ve);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x000136BE File Offset: 0x000118BE
		public void Clear()
		{
			this.m_UpdateList.Clear();
			this.m_ParentList.Clear();
			this.m_TempMatchResults.Clear();
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x000136E8 File Offset: 0x000118E8
		private void PropagateToChildren(VisualElement ve)
		{
			int childCount = ve.hierarchy.childCount;
			for (int i = 0; i < childCount; i++)
			{
				VisualElement visualElement = ve.hierarchy[i];
				bool flag = this.m_UpdateList.Add(visualElement);
				bool flag2 = flag;
				if (flag2)
				{
					this.PropagateToChildren(visualElement);
				}
			}
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00013748 File Offset: 0x00011948
		private void PropagateToParents(VisualElement ve)
		{
			for (VisualElement visualElement = ve.hierarchy.parent; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				bool flag = !this.m_ParentList.Add(visualElement);
				if (flag)
				{
					break;
				}
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00013796 File Offset: 0x00011996
		private static void OnProcessMatchResult(VisualElement current, MatchResultInfo info)
		{
			current.triggerPseudoMask |= info.triggerPseudoMask;
			current.dependencyPseudoMask |= info.dependencyPseudoMask;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x000137C0 File Offset: 0x000119C0
		public override void TraverseRecursive(VisualElement element, int depth)
		{
			bool flag = this.ShouldSkipElement(element);
			if (!flag)
			{
				bool flag2 = this.m_UpdateList.Contains(element);
				bool flag3 = flag2;
				if (flag3)
				{
					element.triggerPseudoMask = (PseudoStates)0;
					element.dependencyPseudoMask = (PseudoStates)0;
				}
				int count = this.m_StyleMatchingContext.styleSheetStack.Count;
				bool flag4 = element.styleSheetList != null;
				if (flag4)
				{
					for (int i = 0; i < element.styleSheetList.Count; i++)
					{
						StyleSheet styleSheet = element.styleSheetList[i];
						this.m_StyleMatchingContext.styleSheetStack.Add(styleSheet);
					}
				}
				int customPropertiesCount = element.computedStyle.customPropertiesCount;
				bool flag5 = flag2;
				if (flag5)
				{
					this.m_StyleMatchingContext.currentElement = element;
					StyleSelectorHelper.FindMatches(this.m_StyleMatchingContext, this.m_TempMatchResults);
					this.ProcessMatchedRules(element, this.m_TempMatchResults);
					element.inheritedStylesHash = element.computedStyle.inheritedData.GetHashCode();
					this.m_StyleMatchingContext.currentElement = null;
					this.m_TempMatchResults.Clear();
				}
				else
				{
					this.m_StyleMatchingContext.variableContext = element.variableContext;
				}
				bool flag6 = flag2 && (customPropertiesCount > 0 || element.computedStyle.customPropertiesCount > 0);
				if (flag6)
				{
					using (CustomStyleResolvedEvent pooled = EventBase<CustomStyleResolvedEvent>.GetPooled())
					{
						pooled.target = element;
						element.SendEvent(pooled);
					}
				}
				base.Recurse(element, depth);
				bool flag7 = this.m_StyleMatchingContext.styleSheetStack.Count > count;
				if (flag7)
				{
					this.m_StyleMatchingContext.styleSheetStack.RemoveRange(count, this.m_StyleMatchingContext.styleSheetStack.Count - count);
				}
			}
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x00013998 File Offset: 0x00011B98
		private bool ShouldSkipElement(VisualElement element)
		{
			return !this.m_ParentList.Contains(element) && !this.m_UpdateList.Contains(element);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x000139CC File Offset: 0x00011BCC
		private void ProcessMatchedRules(VisualElement element, List<SelectorMatchRecord> matchingSelectors)
		{
			matchingSelectors.Sort((SelectorMatchRecord a, SelectorMatchRecord b) => SelectorMatchRecord.Compare(a, b));
			long num = (long)element.fullTypeName.GetHashCode();
			num = (num * 397L) ^ (long)this.currentPixelsPerPoint.GetHashCode();
			int variableHash = this.m_StyleMatchingContext.variableContext.GetVariableHash();
			int num2 = 0;
			foreach (SelectorMatchRecord selectorMatchRecord in matchingSelectors)
			{
				StyleRule rule = selectorMatchRecord.complexSelector.rule;
				int specificity = selectorMatchRecord.complexSelector.specificity;
				num = (num * 397L) ^ (long)rule.GetHashCode();
				num = (num * 397L) ^ (long)specificity;
				bool flag = rule.customPropertiesCount > 0;
				if (flag)
				{
					num2 += rule.customPropertiesCount;
					this.ProcessMatchedVariables(selectorMatchRecord.sheet, rule);
				}
			}
			VisualElement parent = element.hierarchy.parent;
			int num3 = ((parent != null) ? parent.inheritedStylesHash : 0);
			num = (num * 397L) ^ (long)num3;
			int num4 = variableHash;
			bool flag2 = num2 > 0;
			if (flag2)
			{
				this.m_ProcessVarContext.InsertRange(0, this.m_StyleMatchingContext.variableContext);
				num4 = this.m_ProcessVarContext.GetVariableHash();
			}
			num = (num * 397L) ^ (long)num4;
			bool flag3 = variableHash != num4;
			if (flag3)
			{
				StyleVariableContext styleVariableContext;
				bool flag4 = !StyleCache.TryGetValue(num4, out styleVariableContext);
				if (flag4)
				{
					styleVariableContext = new StyleVariableContext(this.m_ProcessVarContext);
					StyleCache.SetValue(num4, styleVariableContext);
				}
				this.m_StyleMatchingContext.variableContext = styleVariableContext;
			}
			element.variableContext = this.m_StyleMatchingContext.variableContext;
			this.m_ProcessVarContext.Clear();
			ComputedStyle computedStyle;
			bool flag5 = StyleCache.TryGetValue(num, out computedStyle);
			if (flag5)
			{
				element.SetSharedStyles(computedStyle);
			}
			else
			{
				ComputedStyle computedStyle2 = ((parent != null) ? parent.computedStyle : null);
				computedStyle = ComputedStyle.Create(computedStyle2, true);
				float scaledPixelsPerPoint = element.scaledPixelsPerPoint;
				foreach (SelectorMatchRecord selectorMatchRecord2 in matchingSelectors)
				{
					this.m_StylePropertyReader.SetContext(selectorMatchRecord2.sheet, selectorMatchRecord2.complexSelector, this.m_StyleMatchingContext.variableContext, scaledPixelsPerPoint);
					computedStyle.ApplyProperties(this.m_StylePropertyReader, computedStyle2);
				}
				computedStyle.FinalizeApply(computedStyle2);
				StyleCache.SetValue(num, computedStyle);
				element.SetSharedStyles(computedStyle);
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00013C84 File Offset: 0x00011E84
		private void ProcessMatchedVariables(StyleSheet sheet, StyleRule rule)
		{
			foreach (StyleProperty styleProperty in rule.properties)
			{
				bool isCustomProperty = styleProperty.isCustomProperty;
				if (isCustomProperty)
				{
					StyleVariable styleVariable = new StyleVariable
					{
						name = styleProperty.name,
						sheet = sheet,
						handles = styleProperty.values
					};
					this.m_ProcessVarContext.Add(styleVariable);
				}
			}
		}

		// Token: 0x04000226 RID: 550
		private StyleVariableContext m_ProcessVarContext = new StyleVariableContext();

		// Token: 0x04000227 RID: 551
		private HashSet<VisualElement> m_UpdateList = new HashSet<VisualElement>();

		// Token: 0x04000228 RID: 552
		private HashSet<VisualElement> m_ParentList = new HashSet<VisualElement>();

		// Token: 0x04000229 RID: 553
		private List<SelectorMatchRecord> m_TempMatchResults = new List<SelectorMatchRecord>();

		// Token: 0x0400022B RID: 555
		private StyleMatchingContext m_StyleMatchingContext = new StyleMatchingContext(new Action<VisualElement, MatchResultInfo>(VisualTreeStyleUpdaterTraversal.OnProcessMatchResult));

		// Token: 0x0400022C RID: 556
		private StylePropertyReader m_StylePropertyReader = new StylePropertyReader();
	}
}
