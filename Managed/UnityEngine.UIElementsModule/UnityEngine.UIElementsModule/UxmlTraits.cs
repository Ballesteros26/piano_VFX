using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnityEngine.UIElements
{
	// Token: 0x020001F8 RID: 504
	public abstract class UxmlTraits
	{
		// Token: 0x06000F5B RID: 3931 RVA: 0x00038831 File Offset: 0x00036A31
		protected UxmlTraits()
		{
			this.canHaveAnyAttribute = true;
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00038843 File Offset: 0x00036A43
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x0003884B File Offset: 0x00036A4B
		public bool canHaveAnyAttribute { get; protected set; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00038854 File Offset: 0x00036A54
		public virtual IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription
		{
			get
			{
				foreach (UxmlAttributeDescription attributeDescription in this.GetAllAttributeDescriptionForType(base.GetType()))
				{
					yield return attributeDescription;
					attributeDescription = null;
				}
				IEnumerator<UxmlAttributeDescription> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06000F5F RID: 3935 RVA: 0x00038874 File Offset: 0x00036A74
		public virtual IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x000062F3 File Offset: 0x000044F3
		public virtual void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
		{
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x00038893 File Offset: 0x00036A93
		private IEnumerable<UxmlAttributeDescription> GetAllAttributeDescriptionForType(Type t)
		{
			Type baseType = t.BaseType;
			bool flag = baseType != null;
			if (flag)
			{
				foreach (UxmlAttributeDescription ident in this.GetAllAttributeDescriptionForType(baseType))
				{
					yield return ident;
					ident = null;
				}
				IEnumerator<UxmlAttributeDescription> enumerator = null;
			}
			foreach (FieldInfo fieldInfo in Enumerable.Where<FieldInfo>(t.GetFields(54), (FieldInfo f) => typeof(UxmlAttributeDescription).IsAssignableFrom(f.FieldType)))
			{
				yield return (UxmlAttributeDescription)fieldInfo.GetValue(this);
				fieldInfo = null;
			}
			IEnumerator<FieldInfo> enumerator2 = null;
			yield break;
			yield break;
		}
	}
}
