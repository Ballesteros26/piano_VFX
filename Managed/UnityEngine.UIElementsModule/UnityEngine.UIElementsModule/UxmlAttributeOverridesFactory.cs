using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DE RID: 478
	public class UxmlAttributeOverridesFactory : UxmlFactory<VisualElement, UxmlAttributeOverridesTraits>
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00037708 File Offset: 0x00035908
		public override string uxmlName
		{
			get
			{
				return "AttributeOverrides";
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0003770F File Offset: 0x0003590F
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00037433 File Offset: 0x00035633
		public override string substituteForTypeName
		{
			get
			{
				return typeof(VisualElement).Name;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000EDC RID: 3804 RVA: 0x00037444 File Offset: 0x00035644
		public override string substituteForTypeNamespace
		{
			get
			{
				return typeof(VisualElement).Namespace ?? string.Empty;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x0003745E File Offset: 0x0003565E
		public override string substituteForTypeQualifiedName
		{
			get
			{
				return typeof(VisualElement).FullName;
			}
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00037728 File Offset: 0x00035928
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}

		// Token: 0x0400060F RID: 1551
		internal const string k_ElementName = "AttributeOverrides";
	}
}
