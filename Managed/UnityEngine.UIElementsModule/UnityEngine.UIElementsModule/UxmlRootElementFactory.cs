using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D5 RID: 469
	public class UxmlRootElementFactory : UxmlFactory<VisualElement, UxmlRootElementTraits>
	{
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x000372AC File Offset: 0x000354AC
		public override string uxmlName
		{
			get
			{
				return "UXML";
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000EA7 RID: 3751 RVA: 0x000372B3 File Offset: 0x000354B3
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x000372CB File Offset: 0x000354CB
		public override string substituteForTypeName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x000372CB File Offset: 0x000354CB
		public override string substituteForTypeNamespace
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x000372CB File Offset: 0x000354CB
		public override string substituteForTypeQualifiedName
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x000372D4 File Offset: 0x000354D4
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}

		// Token: 0x040005F8 RID: 1528
		internal const string k_ElementName = "UXML";
	}
}
