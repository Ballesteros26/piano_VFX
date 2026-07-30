using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001DB RID: 475
	public class UxmlTemplateFactory : UxmlFactory<VisualElement, UxmlTemplateTraits>
	{
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x000375A8 File Offset: 0x000357A8
		public override string uxmlName
		{
			get
			{
				return "Template";
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x000375AF File Offset: 0x000357AF
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00037433 File Offset: 0x00035633
		public override string substituteForTypeName
		{
			get
			{
				return typeof(VisualElement).Name;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00037444 File Offset: 0x00035644
		public override string substituteForTypeNamespace
		{
			get
			{
				return typeof(VisualElement).Namespace ?? string.Empty;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x0003745E File Offset: 0x0003565E
		public override string substituteForTypeQualifiedName
		{
			get
			{
				return typeof(VisualElement).FullName;
			}
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x000375C8 File Offset: 0x000357C8
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}

		// Token: 0x04000607 RID: 1543
		internal const string k_ElementName = "Template";
	}
}
