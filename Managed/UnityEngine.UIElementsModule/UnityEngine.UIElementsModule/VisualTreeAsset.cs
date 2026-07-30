using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x0200020D RID: 525
	[Serializable]
	public class VisualTreeAsset : ScriptableObject
	{
		// Token: 0x06000FDB RID: 4059 RVA: 0x00039ADC File Offset: 0x00037CDC
		internal int GetNextChildSerialNumber()
		{
			List<VisualElementAsset> visualElementAssets = this.m_VisualElementAssets;
			int num = ((visualElementAssets != null) ? visualElementAssets.Count : 0);
			int num2 = num;
			List<TemplateAsset> templateAssets = this.m_TemplateAssets;
			return num2 + ((templateAssets != null) ? templateAssets.Count : 0);
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00039B18 File Offset: 0x00037D18
		public IEnumerable<VisualTreeAsset> templateDependencies
		{
			get
			{
				HashSet<VisualTreeAsset> sent = new HashSet<VisualTreeAsset>();
				foreach (VisualTreeAsset.UsingEntry entry in this.m_Usings)
				{
					bool flag = entry.asset != null && !sent.Contains(entry.asset);
					if (flag)
					{
						sent.Add(entry.asset);
						yield return entry.asset;
					}
					else
					{
						bool flag2 = !string.IsNullOrEmpty(entry.path);
						if (flag2)
						{
							VisualTreeAsset vta = Panel.LoadResource(entry.path, typeof(VisualTreeAsset), GUIUtility.pixelsPerPoint) as VisualTreeAsset;
							bool flag3 = vta != null && !sent.Contains(entry.asset);
							if (flag3)
							{
								sent.Add(entry.asset);
								yield return vta;
							}
							vta = null;
						}
					}
					entry = default(VisualTreeAsset.UsingEntry);
				}
				List<VisualTreeAsset.UsingEntry>.Enumerator enumerator = default(List<VisualTreeAsset.UsingEntry>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00039B38 File Offset: 0x00037D38
		public IEnumerable<StyleSheet> stylesheets
		{
			get
			{
				HashSet<StyleSheet> sent = new HashSet<StyleSheet>();
				foreach (VisualElementAsset vea in this.m_VisualElementAssets)
				{
					bool hasStylesheets = vea.hasStylesheets;
					if (hasStylesheets)
					{
						foreach (StyleSheet stylesheet in vea.stylesheets)
						{
							bool flag = !sent.Contains(stylesheet);
							if (flag)
							{
								sent.Add(stylesheet);
								yield return stylesheet;
							}
							stylesheet = null;
						}
						List<StyleSheet>.Enumerator enumerator2 = default(List<StyleSheet>.Enumerator);
					}
					bool hasStylesheetPaths = vea.hasStylesheetPaths;
					if (hasStylesheetPaths)
					{
						foreach (string stylesheetPath in vea.stylesheetPaths)
						{
							StyleSheet stylesheet2 = Panel.LoadResource(stylesheetPath, typeof(StyleSheet), GUIUtility.pixelsPerPoint) as StyleSheet;
							bool flag2 = stylesheet2 != null && !sent.Contains(stylesheet2);
							if (flag2)
							{
								sent.Add(stylesheet2);
								yield return stylesheet2;
							}
							stylesheet2 = null;
							stylesheetPath = null;
						}
						List<string>.Enumerator enumerator3 = default(List<string>.Enumerator);
					}
					vea = null;
				}
				List<VisualElementAsset>.Enumerator enumerator = default(List<VisualElementAsset>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00039B58 File Offset: 0x00037D58
		// (set) Token: 0x06000FDF RID: 4063 RVA: 0x00039B70 File Offset: 0x00037D70
		internal List<VisualElementAsset> visualElementAssets
		{
			get
			{
				return this.m_VisualElementAssets;
			}
			set
			{
				this.m_VisualElementAssets = value;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00039B7C File Offset: 0x00037D7C
		// (set) Token: 0x06000FE1 RID: 4065 RVA: 0x00039B94 File Offset: 0x00037D94
		internal List<TemplateAsset> templateAssets
		{
			get
			{
				return this.m_TemplateAssets;
			}
			set
			{
				this.m_TemplateAssets = value;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000FE2 RID: 4066 RVA: 0x00039BA0 File Offset: 0x00037DA0
		// (set) Token: 0x06000FE3 RID: 4067 RVA: 0x00039BB8 File Offset: 0x00037DB8
		internal List<VisualTreeAsset.SlotDefinition> slots
		{
			get
			{
				return this.m_Slots;
			}
			set
			{
				this.m_Slots = value;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000FE4 RID: 4068 RVA: 0x00039BC4 File Offset: 0x00037DC4
		// (set) Token: 0x06000FE5 RID: 4069 RVA: 0x00039BDC File Offset: 0x00037DDC
		internal int contentContainerId
		{
			get
			{
				return this.m_ContentContainerId;
			}
			set
			{
				this.m_ContentContainerId = value;
			}
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00039BE8 File Offset: 0x00037DE8
		public TemplateContainer Instantiate()
		{
			TemplateContainer templateContainer = new TemplateContainer(base.name);
			try
			{
				this.CloneTree(templateContainer, VisualTreeAsset.s_TemporarySlotInsertionPoints, null);
			}
			finally
			{
				VisualTreeAsset.s_TemporarySlotInsertionPoints.Clear();
			}
			return templateContainer;
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00039C38 File Offset: 0x00037E38
		public TemplateContainer Instantiate(string bindingPath)
		{
			TemplateContainer templateContainer = this.Instantiate();
			templateContainer.bindingPath = bindingPath;
			return templateContainer;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00039C5C File Offset: 0x00037E5C
		public TemplateContainer CloneTree()
		{
			return this.Instantiate();
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x00039C74 File Offset: 0x00037E74
		public TemplateContainer CloneTree(string bindingPath)
		{
			return this.Instantiate(bindingPath);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x00039C90 File Offset: 0x00037E90
		public void CloneTree(VisualElement target)
		{
			int num;
			int num2;
			this.CloneTree(target, out num, out num2);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00039CAC File Offset: 0x00037EAC
		public void CloneTree(VisualElement target, out int firstElementIndex, out int elementAddedCount)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			firstElementIndex = target.childCount;
			try
			{
				this.CloneTree(target, VisualTreeAsset.s_TemporarySlotInsertionPoints, null);
			}
			finally
			{
				elementAddedCount = target.childCount - firstElementIndex;
				VisualTreeAsset.s_TemporarySlotInsertionPoints.Clear();
			}
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00039D10 File Offset: 0x00037F10
		internal void CloneTree(VisualElement target, Dictionary<string, VisualElement> slotInsertionPoints, List<TemplateAsset.AttributeOverride> attributeOverrides)
		{
			bool flag = target == null;
			if (flag)
			{
				throw new ArgumentNullException("target");
			}
			bool flag2 = (this.visualElementAssets == null || this.visualElementAssets.Count <= 0) && (this.templateAssets == null || this.templateAssets.Count <= 0);
			if (!flag2)
			{
				Dictionary<int, List<VisualElementAsset>> dictionary = new Dictionary<int, List<VisualElementAsset>>();
				int num = ((this.visualElementAssets == null) ? 0 : this.visualElementAssets.Count);
				int num2 = ((this.templateAssets == null) ? 0 : this.templateAssets.Count);
				for (int i = 0; i < num + num2; i++)
				{
					VisualElementAsset visualElementAsset = ((i < num) ? this.visualElementAssets[i] : this.templateAssets[i - num]);
					List<VisualElementAsset> list;
					bool flag3 = !dictionary.TryGetValue(visualElementAsset.parentId, ref list);
					if (flag3)
					{
						list = new List<VisualElementAsset>();
						dictionary.Add(visualElementAsset.parentId, list);
					}
					list.Add(visualElementAsset);
				}
				List<VisualElementAsset> list2;
				dictionary.TryGetValue(0, ref list2);
				bool flag4 = list2 == null || list2.Count == 0;
				if (!flag4)
				{
					Debug.Assert(list2.Count == 1);
					VisualElementAsset visualElementAsset2 = list2[0];
					VisualTreeAsset.AssignClassListFromAssetToElement(visualElementAsset2, target);
					VisualTreeAsset.AssignStyleSheetFromAssetToElement(visualElementAsset2, target);
					list2.Clear();
					dictionary.TryGetValue(visualElementAsset2.id, ref list2);
					bool flag5 = list2 == null || list2.Count == 0;
					if (!flag5)
					{
						list2.Sort(new Comparison<VisualElementAsset>(VisualTreeAsset.CompareForOrder));
						foreach (VisualElementAsset visualElementAsset3 in list2)
						{
							Assert.IsNotNull<VisualElementAsset>(visualElementAsset3);
							VisualElement visualElement = this.CloneSetupRecursively(visualElementAsset3, dictionary, new CreationContext(slotInsertionPoints, attributeOverrides, this, target));
							bool flag6 = visualElement != null;
							if (flag6)
							{
								target.hierarchy.Add(visualElement);
							}
							else
							{
								Debug.LogWarning("VisualTreeAsset instantiated an empty UI. Check the syntax of your UXML document.");
							}
						}
					}
				}
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00039F40 File Offset: 0x00038140
		private VisualElement CloneSetupRecursively(VisualElementAsset root, Dictionary<int, List<VisualElementAsset>> idToChildren, CreationContext context)
		{
			VisualElement visualElement = VisualTreeAsset.Create(root, context);
			bool flag = visualElement == null;
			VisualElement visualElement2;
			if (flag)
			{
				visualElement2 = null;
			}
			else
			{
				bool flag2 = root.id == context.visualTreeAsset.contentContainerId;
				if (flag2)
				{
					bool flag3 = context.target is TemplateContainer;
					if (flag3)
					{
						((TemplateContainer)context.target).SetContentContainer(visualElement);
					}
					else
					{
						Debug.LogError("Trying to clone a VisualTreeAsset with a custom content container into a element which is not a template container");
					}
				}
				string text;
				bool flag4 = context.slotInsertionPoints != null && this.TryGetSlotInsertionPoint(root.id, out text);
				if (flag4)
				{
					context.slotInsertionPoints.Add(text, visualElement);
				}
				bool flag5 = root.ruleIndex != -1;
				if (flag5)
				{
					bool flag6 = this.inlineSheet == null;
					if (flag6)
					{
						Debug.LogWarning("VisualElementAsset has a RuleIndex but no inlineStyleSheet");
					}
					else
					{
						StyleRule styleRule = this.inlineSheet.rules[root.ruleIndex];
						visualElement.SetInlineRule(this.inlineSheet, styleRule);
					}
				}
				TemplateAsset templateAsset = root as TemplateAsset;
				List<VisualElementAsset> list;
				bool flag7 = idToChildren.TryGetValue(root.id, ref list);
				if (flag7)
				{
					list.Sort(new Comparison<VisualElementAsset>(VisualTreeAsset.CompareForOrder));
					using (List<VisualElementAsset>.Enumerator enumerator = list.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							VisualElementAsset childVea = enumerator.Current;
							VisualElement visualElement3 = this.CloneSetupRecursively(childVea, idToChildren, context);
							bool flag8 = visualElement3 == null;
							if (!flag8)
							{
								bool flag9 = templateAsset == null;
								if (flag9)
								{
									visualElement.Add(visualElement3);
								}
								else
								{
									int num = ((templateAsset.slotUsages == null) ? (-1) : templateAsset.slotUsages.FindIndex((VisualTreeAsset.SlotUsageEntry u) => u.assetId == childVea.id));
									bool flag10 = num != -1;
									if (flag10)
									{
										string slotName = templateAsset.slotUsages[num].slotName;
										Assert.IsFalse(string.IsNullOrEmpty(slotName), "a lost name should not be null or empty, this probably points to an importer or serialization bug");
										VisualElement visualElement4;
										bool flag11 = context.slotInsertionPoints == null || !context.slotInsertionPoints.TryGetValue(slotName, ref visualElement4);
										if (flag11)
										{
											Debug.LogErrorFormat("Slot '{0}' was not found. Existing slots: {1}", new object[]
											{
												slotName,
												(context.slotInsertionPoints == null) ? string.Empty : string.Join(", ", Enumerable.ToArray<string>(context.slotInsertionPoints.Keys))
											});
											visualElement.Add(visualElement3);
										}
										else
										{
											visualElement4.Add(visualElement3);
										}
									}
									else
									{
										visualElement.Add(visualElement3);
									}
								}
							}
						}
					}
				}
				bool flag12 = templateAsset != null && context.slotInsertionPoints != null;
				if (flag12)
				{
					context.slotInsertionPoints.Clear();
				}
				visualElement2 = visualElement;
			}
			return visualElement2;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003A224 File Offset: 0x00038424
		private static int CompareForOrder(VisualElementAsset a, VisualElementAsset b)
		{
			return a.orderInDocument.CompareTo(b.orderInDocument);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0003A248 File Offset: 0x00038448
		internal bool TryGetSlotInsertionPoint(int insertionPointId, out string slotName)
		{
			bool flag = this.m_Slots == null;
			bool flag2;
			if (flag)
			{
				slotName = null;
				flag2 = false;
			}
			else
			{
				for (int i = 0; i < this.m_Slots.Count; i++)
				{
					VisualTreeAsset.SlotDefinition slotDefinition = this.m_Slots[i];
					bool flag3 = slotDefinition.insertionPointId == insertionPointId;
					if (flag3)
					{
						slotName = slotDefinition.name;
						return true;
					}
				}
				slotName = null;
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003A2BC File Offset: 0x000384BC
		internal VisualTreeAsset ResolveTemplate(string templateName)
		{
			bool flag = this.m_Usings == null || this.m_Usings.Count == 0;
			VisualTreeAsset visualTreeAsset;
			if (flag)
			{
				visualTreeAsset = null;
			}
			else
			{
				int num = this.m_Usings.BinarySearch(new VisualTreeAsset.UsingEntry(templateName, string.Empty), VisualTreeAsset.UsingEntry.comparer);
				bool flag2 = num < 0;
				if (flag2)
				{
					visualTreeAsset = null;
				}
				else
				{
					bool flag3 = this.m_Usings[num].asset;
					if (flag3)
					{
						visualTreeAsset = this.m_Usings[num].asset;
					}
					else
					{
						string path = this.m_Usings[num].path;
						visualTreeAsset = Panel.LoadResource(path, typeof(VisualTreeAsset), GUIUtility.pixelsPerPoint) as VisualTreeAsset;
					}
				}
			}
			return visualTreeAsset;
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003A378 File Offset: 0x00038578
		internal static VisualElement Create(VisualElementAsset asset, CreationContext ctx)
		{
			List<IUxmlFactory> list;
			bool flag = !VisualElementFactoryRegistry.TryGetValue(asset.fullTypeName, out list);
			if (flag)
			{
				bool flag2 = asset.fullTypeName.StartsWith("UnityEngine.Experimental.UIElements.") || asset.fullTypeName.StartsWith("UnityEditor.Experimental.UIElements.");
				if (flag2)
				{
					string text = asset.fullTypeName.Replace(".Experimental.UIElements", ".UIElements");
					bool flag3 = !VisualElementFactoryRegistry.TryGetValue(text, out list);
					if (flag3)
					{
						Debug.LogErrorFormat("Element '{0}' has no registered factory method.", new object[] { asset.fullTypeName });
						return new Label(string.Format("Unknown type: '{0}'", asset.fullTypeName));
					}
				}
				else
				{
					bool flag4 = asset.fullTypeName == "UXML";
					if (!flag4)
					{
						Debug.LogErrorFormat("Element '{0}' has no registered factory method.", new object[] { asset.fullTypeName });
						return new Label(string.Format("Unknown type: '{0}'", asset.fullTypeName));
					}
					VisualElementFactoryRegistry.TryGetValue(typeof(UxmlRootElementFactory).Namespace + "." + asset.fullTypeName, out list);
				}
			}
			IUxmlFactory uxmlFactory = null;
			foreach (IUxmlFactory uxmlFactory2 in list)
			{
				bool flag5 = uxmlFactory2.AcceptsAttributeBag(asset, ctx);
				if (flag5)
				{
					uxmlFactory = uxmlFactory2;
					break;
				}
			}
			bool flag6 = uxmlFactory == null;
			VisualElement visualElement;
			if (flag6)
			{
				Debug.LogErrorFormat("Element '{0}' has a no factory that accept the set of XML attributes specified.", new object[] { asset.fullTypeName });
				visualElement = new Label(string.Format("Type with no factory: '{0}'", asset.fullTypeName));
			}
			else
			{
				VisualElement visualElement2 = uxmlFactory.Create(asset, ctx);
				bool flag7 = visualElement2 != null;
				if (flag7)
				{
					VisualTreeAsset.AssignClassListFromAssetToElement(asset, visualElement2);
					VisualTreeAsset.AssignStyleSheetFromAssetToElement(asset, visualElement2);
				}
				visualElement = visualElement2;
			}
			return visualElement;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0003A568 File Offset: 0x00038768
		private static void AssignClassListFromAssetToElement(VisualElementAsset asset, VisualElement element)
		{
			bool flag = asset.classes != null;
			if (flag)
			{
				for (int i = 0; i < asset.classes.Length; i++)
				{
					element.AddToClassList(asset.classes[i]);
				}
			}
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0003A5AC File Offset: 0x000387AC
		private static void AssignStyleSheetFromAssetToElement(VisualElementAsset asset, VisualElement element)
		{
			bool hasStylesheetPaths = asset.hasStylesheetPaths;
			if (hasStylesheetPaths)
			{
				for (int i = 0; i < asset.stylesheetPaths.Count; i++)
				{
					element.AddStyleSheetPath(asset.stylesheetPaths[i]);
				}
			}
			bool hasStylesheets = asset.hasStylesheets;
			if (hasStylesheets)
			{
				for (int j = 0; j < asset.stylesheets.Count; j++)
				{
					bool flag = asset.stylesheets[j] != null;
					if (flag)
					{
						element.styleSheets.Add(asset.stylesheets[j]);
					}
				}
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0003A65C File Offset: 0x0003885C
		// (set) Token: 0x06000FF5 RID: 4085 RVA: 0x0003A674 File Offset: 0x00038874
		public int contentHash
		{
			get
			{
				return this.m_ContentHash;
			}
			set
			{
				this.m_ContentHash = value;
			}
		}

		// Token: 0x0400068F RID: 1679
		private static readonly Dictionary<string, VisualElement> s_TemporarySlotInsertionPoints = new Dictionary<string, VisualElement>();

		// Token: 0x04000690 RID: 1680
		[SerializeField]
		private List<VisualTreeAsset.UsingEntry> m_Usings;

		// Token: 0x04000691 RID: 1681
		[SerializeField]
		internal StyleSheet inlineSheet;

		// Token: 0x04000692 RID: 1682
		[SerializeField]
		private List<VisualElementAsset> m_VisualElementAssets;

		// Token: 0x04000693 RID: 1683
		[SerializeField]
		private List<TemplateAsset> m_TemplateAssets;

		// Token: 0x04000694 RID: 1684
		[SerializeField]
		private List<VisualTreeAsset.SlotDefinition> m_Slots;

		// Token: 0x04000695 RID: 1685
		[SerializeField]
		private int m_ContentContainerId;

		// Token: 0x04000696 RID: 1686
		[SerializeField]
		private int m_ContentHash;

		// Token: 0x0200020E RID: 526
		[Serializable]
		internal struct UsingEntry
		{
			// Token: 0x06000FF8 RID: 4088 RVA: 0x0003A68A File Offset: 0x0003888A
			public UsingEntry(string alias, string path)
			{
				this.alias = alias;
				this.path = path;
				this.asset = null;
			}

			// Token: 0x06000FF9 RID: 4089 RVA: 0x0003A6A2 File Offset: 0x000388A2
			public UsingEntry(string alias, VisualTreeAsset asset)
			{
				this.alias = alias;
				this.path = null;
				this.asset = asset;
			}

			// Token: 0x04000697 RID: 1687
			internal static readonly IComparer<VisualTreeAsset.UsingEntry> comparer = new VisualTreeAsset.UsingEntryComparer();

			// Token: 0x04000698 RID: 1688
			[SerializeField]
			public string alias;

			// Token: 0x04000699 RID: 1689
			[SerializeField]
			public string path;

			// Token: 0x0400069A RID: 1690
			[SerializeField]
			public VisualTreeAsset asset;
		}

		// Token: 0x0200020F RID: 527
		private class UsingEntryComparer : IComparer<VisualTreeAsset.UsingEntry>
		{
			// Token: 0x06000FFB RID: 4091 RVA: 0x0003A6C8 File Offset: 0x000388C8
			public int Compare(VisualTreeAsset.UsingEntry x, VisualTreeAsset.UsingEntry y)
			{
				return string.CompareOrdinal(x.alias, y.alias);
			}
		}

		// Token: 0x02000210 RID: 528
		[Serializable]
		internal struct SlotDefinition
		{
			// Token: 0x0400069B RID: 1691
			[SerializeField]
			public string name;

			// Token: 0x0400069C RID: 1692
			[SerializeField]
			public int insertionPointId;
		}

		// Token: 0x02000211 RID: 529
		[Serializable]
		internal struct SlotUsageEntry
		{
			// Token: 0x06000FFD RID: 4093 RVA: 0x0003A6EB File Offset: 0x000388EB
			public SlotUsageEntry(string slotName, int assetId)
			{
				this.slotName = slotName;
				this.assetId = assetId;
			}

			// Token: 0x0400069D RID: 1693
			[SerializeField]
			public string slotName;

			// Token: 0x0400069E RID: 1694
			[SerializeField]
			public int assetId;
		}
	}
}
