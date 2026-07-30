using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000215 RID: 533
	public struct CreationContext : IEquatable<CreationContext>
	{
		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x0003ADE5 File Offset: 0x00038FE5
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x0003ADED File Offset: 0x00038FED
		public VisualElement target { get; private set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0003ADF6 File Offset: 0x00038FF6
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x0003ADFE File Offset: 0x00038FFE
		public VisualTreeAsset visualTreeAsset { get; private set; }

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0003AE07 File Offset: 0x00039007
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x0003AE0F File Offset: 0x0003900F
		public Dictionary<string, VisualElement> slotInsertionPoints { get; private set; }

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x0003AE18 File Offset: 0x00039018
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x0003AE20 File Offset: 0x00039020
		internal List<TemplateAsset.AttributeOverride> attributeOverrides { get; private set; }

		// Token: 0x0600101C RID: 4124 RVA: 0x0003AE29 File Offset: 0x00039029
		internal CreationContext(Dictionary<string, VisualElement> slotInsertionPoints, VisualTreeAsset vta, VisualElement target)
		{
			this = new CreationContext(slotInsertionPoints, null, vta, target);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0003AE37 File Offset: 0x00039037
		internal CreationContext(Dictionary<string, VisualElement> slotInsertionPoints, List<TemplateAsset.AttributeOverride> attributeOverrides, VisualTreeAsset vta, VisualElement target)
		{
			this.target = target;
			this.slotInsertionPoints = slotInsertionPoints;
			this.attributeOverrides = attributeOverrides;
			this.visualTreeAsset = vta;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0003AE5C File Offset: 0x0003905C
		public override bool Equals(object obj)
		{
			return obj is CreationContext && this.Equals((CreationContext)obj);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0003AE88 File Offset: 0x00039088
		public bool Equals(CreationContext other)
		{
			return EqualityComparer<VisualElement>.Default.Equals(this.target, other.target) && EqualityComparer<VisualTreeAsset>.Default.Equals(this.visualTreeAsset, other.visualTreeAsset) && EqualityComparer<Dictionary<string, VisualElement>>.Default.Equals(this.slotInsertionPoints, other.slotInsertionPoints);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0003AEE8 File Offset: 0x000390E8
		public override int GetHashCode()
		{
			int num = -2123482148;
			num = num * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.target);
			num = num * -1521134295 + EqualityComparer<VisualTreeAsset>.Default.GetHashCode(this.visualTreeAsset);
			return num * -1521134295 + EqualityComparer<Dictionary<string, VisualElement>>.Default.GetHashCode(this.slotInsertionPoints);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0003AF4C File Offset: 0x0003914C
		public static bool operator ==(CreationContext context1, CreationContext context2)
		{
			return context1.Equals(context2);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0003AF68 File Offset: 0x00039168
		public static bool operator !=(CreationContext context1, CreationContext context2)
		{
			return !(context1 == context2);
		}

		// Token: 0x040006B4 RID: 1716
		public static readonly CreationContext Default = default(CreationContext);
	}
}
