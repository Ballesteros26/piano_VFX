using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020001FE RID: 510
	public class UxmlFactory<TCreatedType, TTraits> : IUxmlFactory where TCreatedType : VisualElement, new() where TTraits : UxmlTraits, new()
	{
		// Token: 0x06000F8B RID: 3979 RVA: 0x00038E00 File Offset: 0x00037000
		protected UxmlFactory()
		{
			this.m_Traits = new TTraits();
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06000F8C RID: 3980 RVA: 0x00038E18 File Offset: 0x00037018
		public virtual string uxmlName
		{
			get
			{
				return typeof(TCreatedType).Name;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x00038E3C File Offset: 0x0003703C
		public virtual string uxmlNamespace
		{
			get
			{
				return typeof(TCreatedType).Namespace ?? string.Empty;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x00038E68 File Offset: 0x00037068
		public virtual string uxmlQualifiedName
		{
			get
			{
				return typeof(TCreatedType).FullName;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06000F8F RID: 3983 RVA: 0x00038E8C File Offset: 0x0003708C
		public bool canHaveAnyAttribute
		{
			get
			{
				return this.m_Traits.canHaveAnyAttribute;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x00038EB0 File Offset: 0x000370B0
		public virtual IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription
		{
			get
			{
				foreach (UxmlAttributeDescription attr in this.m_Traits.uxmlAttributesDescription)
				{
					yield return attr;
					attr = null;
				}
				IEnumerator<UxmlAttributeDescription> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x00038ED0 File Offset: 0x000370D0
		public virtual IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				foreach (UxmlChildElementDescription child in this.m_Traits.uxmlChildElementsDescription)
				{
					yield return child;
					child = null;
				}
				IEnumerator<UxmlChildElementDescription> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x00038EF0 File Offset: 0x000370F0
		public virtual string substituteForTypeName
		{
			get
			{
				bool flag = typeof(TCreatedType) == typeof(VisualElement);
				string text;
				if (flag)
				{
					text = string.Empty;
				}
				else
				{
					text = typeof(VisualElement).Name;
				}
				return text;
			}
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x06000F93 RID: 3987 RVA: 0x00038F34 File Offset: 0x00037134
		public virtual string substituteForTypeNamespace
		{
			get
			{
				bool flag = typeof(TCreatedType) == typeof(VisualElement);
				string text;
				if (flag)
				{
					text = string.Empty;
				}
				else
				{
					text = typeof(VisualElement).Namespace ?? string.Empty;
				}
				return text;
			}
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000F94 RID: 3988 RVA: 0x00038F84 File Offset: 0x00037184
		public virtual string substituteForTypeQualifiedName
		{
			get
			{
				bool flag = typeof(TCreatedType) == typeof(VisualElement);
				string text;
				if (flag)
				{
					text = string.Empty;
				}
				else
				{
					text = typeof(VisualElement).FullName;
				}
				return text;
			}
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x00038FCC File Offset: 0x000371CC
		public virtual bool AcceptsAttributeBag(IUxmlAttributes bag, CreationContext cc)
		{
			return true;
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x00038FE0 File Offset: 0x000371E0
		public virtual VisualElement Create(IUxmlAttributes bag, CreationContext cc)
		{
			TCreatedType tcreatedType = new TCreatedType();
			this.m_Traits.Init(tcreatedType, bag, cc);
			return tcreatedType;
		}

		// Token: 0x0400065B RID: 1627
		internal TTraits m_Traits;
	}
}
