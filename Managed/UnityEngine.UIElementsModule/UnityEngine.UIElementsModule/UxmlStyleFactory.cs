using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020001D8 RID: 472
	public class UxmlStyleFactory : UxmlFactory<VisualElement, UxmlStyleTraits>
	{
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000EB7 RID: 3767 RVA: 0x00037414 File Offset: 0x00035614
		public override string uxmlName
		{
			get
			{
				return "Style";
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000EB8 RID: 3768 RVA: 0x0003741B File Offset: 0x0003561B
		public override string uxmlQualifiedName
		{
			get
			{
				return this.uxmlNamespace + "." + this.uxmlName;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000EB9 RID: 3769 RVA: 0x00037433 File Offset: 0x00035633
		public override string substituteForTypeName
		{
			get
			{
				return typeof(VisualElement).Name;
			}
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x00037444 File Offset: 0x00035644
		public override string substituteForTypeNamespace
		{
			get
			{
				return typeof(VisualElement).Namespace ?? string.Empty;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000EBB RID: 3771 RVA: 0x0003745E File Offset: 0x0003565E
		public override string substituteForTypeQualifiedName
		{
			get
			{
				return typeof(VisualElement).FullName;
			}
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x00037470 File Offset: 0x00035670
		public override VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			return null;
		}

		// Token: 0x040005FF RID: 1535
		internal const string k_ElementName = "Style";
	}
}
