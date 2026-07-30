using System;
using System.Collections.Generic;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x020001E1 RID: 481
	[Serializable]
	internal class TemplateAsset : VisualElementAsset
	{
		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000EEA RID: 3818 RVA: 0x0003782C File Offset: 0x00035A2C
		// (set) Token: 0x06000EEB RID: 3819 RVA: 0x00037844 File Offset: 0x00035A44
		public string templateAlias
		{
			get
			{
				return this.m_TemplateAlias;
			}
			set
			{
				this.m_TemplateAlias = value;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x00037850 File Offset: 0x00035A50
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x00037880 File Offset: 0x00035A80
		public List<TemplateAsset.AttributeOverride> attributeOverrides
		{
			get
			{
				return (this.m_AttributeOverrides == null) ? (this.m_AttributeOverrides = new List<TemplateAsset.AttributeOverride>()) : this.m_AttributeOverrides;
			}
			set
			{
				this.m_AttributeOverrides = value;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0003788C File Offset: 0x00035A8C
		// (set) Token: 0x06000EEF RID: 3823 RVA: 0x000378A4 File Offset: 0x00035AA4
		internal List<VisualTreeAsset.SlotUsageEntry> slotUsages
		{
			get
			{
				return this.m_SlotUsages;
			}
			set
			{
				this.m_SlotUsages = value;
			}
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x000378AE File Offset: 0x00035AAE
		public TemplateAsset(string templateAlias, string fullTypeName)
			: base(fullTypeName)
		{
			Assert.IsFalse(string.IsNullOrEmpty(templateAlias), "Template alias must not be null or empty");
			this.m_TemplateAlias = templateAlias;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x000378D4 File Offset: 0x00035AD4
		public void AddSlotUsage(string slotName, int resId)
		{
			bool flag = this.m_SlotUsages == null;
			if (flag)
			{
				this.m_SlotUsages = new List<VisualTreeAsset.SlotUsageEntry>();
			}
			this.m_SlotUsages.Add(new VisualTreeAsset.SlotUsageEntry(slotName, resId));
		}

		// Token: 0x04000616 RID: 1558
		[SerializeField]
		private string m_TemplateAlias;

		// Token: 0x04000617 RID: 1559
		[SerializeField]
		private List<TemplateAsset.AttributeOverride> m_AttributeOverrides;

		// Token: 0x04000618 RID: 1560
		[SerializeField]
		private List<VisualTreeAsset.SlotUsageEntry> m_SlotUsages;

		// Token: 0x020001E2 RID: 482
		[Serializable]
		public struct AttributeOverride
		{
			// Token: 0x04000619 RID: 1561
			public string m_ElementName;

			// Token: 0x0400061A RID: 1562
			public string m_AttributeName;

			// Token: 0x0400061B RID: 1563
			public string m_Value;
		}
	}
}
