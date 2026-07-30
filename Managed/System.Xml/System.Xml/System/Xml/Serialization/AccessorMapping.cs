using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002EE RID: 750
	internal abstract class AccessorMapping : Mapping
	{
		// Token: 0x06001C0E RID: 7182 RVA: 0x0009A06C File Offset: 0x0009826C
		internal AccessorMapping()
		{
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x0009A590 File Offset: 0x00098790
		protected AccessorMapping(AccessorMapping mapping)
			: base(mapping)
		{
			this.typeDesc = mapping.typeDesc;
			this.attribute = mapping.attribute;
			this.elements = mapping.elements;
			this.sortedElements = mapping.sortedElements;
			this.text = mapping.text;
			this.choiceIdentifier = mapping.choiceIdentifier;
			this.xmlns = mapping.xmlns;
			this.ignore = mapping.ignore;
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001C10 RID: 7184 RVA: 0x0009A604 File Offset: 0x00098804
		internal bool IsAttribute
		{
			get
			{
				return this.attribute != null;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001C11 RID: 7185 RVA: 0x0009A60F File Offset: 0x0009880F
		internal bool IsText
		{
			get
			{
				return this.text != null && (this.elements == null || this.elements.Length == 0);
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001C12 RID: 7186 RVA: 0x0009A62F File Offset: 0x0009882F
		internal bool IsParticle
		{
			get
			{
				return this.elements != null && this.elements.Length != 0;
			}
		}

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x06001C13 RID: 7187 RVA: 0x0009A645 File Offset: 0x00098845
		// (set) Token: 0x06001C14 RID: 7188 RVA: 0x0009A64D File Offset: 0x0009884D
		internal TypeDesc TypeDesc
		{
			get
			{
				return this.typeDesc;
			}
			set
			{
				this.typeDesc = value;
			}
		}

		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06001C15 RID: 7189 RVA: 0x0009A656 File Offset: 0x00098856
		// (set) Token: 0x06001C16 RID: 7190 RVA: 0x0009A65E File Offset: 0x0009885E
		internal AttributeAccessor Attribute
		{
			get
			{
				return this.attribute;
			}
			set
			{
				this.attribute = value;
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06001C17 RID: 7191 RVA: 0x0009A667 File Offset: 0x00098867
		// (set) Token: 0x06001C18 RID: 7192 RVA: 0x0009A66F File Offset: 0x0009886F
		internal ElementAccessor[] Elements
		{
			get
			{
				return this.elements;
			}
			set
			{
				this.elements = value;
				this.sortedElements = null;
			}
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x0009A67F File Offset: 0x0009887F
		internal static void SortMostToLeastDerived(ElementAccessor[] elements)
		{
			Array.Sort(elements, new AccessorMapping.AccessorComparer());
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06001C1A RID: 7194 RVA: 0x0009A68C File Offset: 0x0009888C
		internal ElementAccessor[] ElementsSortedByDerivation
		{
			get
			{
				if (this.sortedElements != null)
				{
					return this.sortedElements;
				}
				if (this.elements == null)
				{
					return null;
				}
				this.sortedElements = new ElementAccessor[this.elements.Length];
				Array.Copy(this.elements, 0, this.sortedElements, 0, this.elements.Length);
				AccessorMapping.SortMostToLeastDerived(this.sortedElements);
				return this.sortedElements;
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06001C1B RID: 7195 RVA: 0x0009A6F1 File Offset: 0x000988F1
		// (set) Token: 0x06001C1C RID: 7196 RVA: 0x0009A6F9 File Offset: 0x000988F9
		internal TextAccessor Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06001C1D RID: 7197 RVA: 0x0009A702 File Offset: 0x00098902
		// (set) Token: 0x06001C1E RID: 7198 RVA: 0x0009A70A File Offset: 0x0009890A
		internal ChoiceIdentifierAccessor ChoiceIdentifier
		{
			get
			{
				return this.choiceIdentifier;
			}
			set
			{
				this.choiceIdentifier = value;
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x0009A713 File Offset: 0x00098913
		// (set) Token: 0x06001C20 RID: 7200 RVA: 0x0009A71B File Offset: 0x0009891B
		internal XmlnsAccessor Xmlns
		{
			get
			{
				return this.xmlns;
			}
			set
			{
				this.xmlns = value;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x0009A724 File Offset: 0x00098924
		// (set) Token: 0x06001C22 RID: 7202 RVA: 0x0009A72C File Offset: 0x0009892C
		internal bool Ignore
		{
			get
			{
				return this.ignore;
			}
			set
			{
				this.ignore = value;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001C23 RID: 7203 RVA: 0x0009A735 File Offset: 0x00098935
		internal Accessor Accessor
		{
			get
			{
				if (this.xmlns != null)
				{
					return this.xmlns;
				}
				if (this.attribute != null)
				{
					return this.attribute;
				}
				if (this.elements != null && this.elements.Length != 0)
				{
					return this.elements[0];
				}
				return this.text;
			}
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0009A778 File Offset: 0x00098978
		private static bool IsNeedNullableMember(ElementAccessor element)
		{
			if (element.Mapping is ArrayMapping)
			{
				ArrayMapping arrayMapping = (ArrayMapping)element.Mapping;
				return arrayMapping.Elements != null && arrayMapping.Elements.Length == 1 && AccessorMapping.IsNeedNullableMember(arrayMapping.Elements[0]);
			}
			return element.IsNullable && element.Mapping.TypeDesc.IsValueType;
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001C25 RID: 7205 RVA: 0x0009A7DB File Offset: 0x000989DB
		internal bool IsNeedNullable
		{
			get
			{
				return this.xmlns == null && this.attribute == null && (this.elements != null && this.elements.Length == 1) && AccessorMapping.IsNeedNullableMember(this.elements[0]);
			}
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0009A814 File Offset: 0x00098A14
		internal static bool ElementsMatch(ElementAccessor[] a, ElementAccessor[] b)
		{
			if (a == null)
			{
				return b == null;
			}
			if (b == null)
			{
				return false;
			}
			if (a.Length != b.Length)
			{
				return false;
			}
			for (int i = 0; i < a.Length; i++)
			{
				if (a[i].Name != b[i].Name || a[i].Namespace != b[i].Namespace || a[i].Form != b[i].Form || a[i].IsNullable != b[i].IsNullable)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0009A8A0 File Offset: 0x00098AA0
		internal bool Match(AccessorMapping mapping)
		{
			if (this.Elements != null && this.Elements.Length != 0)
			{
				if (!AccessorMapping.ElementsMatch(this.Elements, mapping.Elements))
				{
					return false;
				}
				if (this.Text == null)
				{
					return mapping.Text == null;
				}
			}
			if (this.Attribute != null)
			{
				return mapping.Attribute != null && (this.Attribute.Name == mapping.Attribute.Name && this.Attribute.Namespace == mapping.Attribute.Namespace) && this.Attribute.Form == mapping.Attribute.Form;
			}
			if (this.Text != null)
			{
				return mapping.Text != null;
			}
			return mapping.Accessor == null;
		}

		// Token: 0x04001627 RID: 5671
		private TypeDesc typeDesc;

		// Token: 0x04001628 RID: 5672
		private AttributeAccessor attribute;

		// Token: 0x04001629 RID: 5673
		private ElementAccessor[] elements;

		// Token: 0x0400162A RID: 5674
		private ElementAccessor[] sortedElements;

		// Token: 0x0400162B RID: 5675
		private TextAccessor text;

		// Token: 0x0400162C RID: 5676
		private ChoiceIdentifierAccessor choiceIdentifier;

		// Token: 0x0400162D RID: 5677
		private XmlnsAccessor xmlns;

		// Token: 0x0400162E RID: 5678
		private bool ignore;

		// Token: 0x020002EF RID: 751
		internal class AccessorComparer : IComparer
		{
			// Token: 0x06001C28 RID: 7208 RVA: 0x0009A968 File Offset: 0x00098B68
			public int Compare(object o1, object o2)
			{
				if (o1 == o2)
				{
					return 0;
				}
				Accessor accessor = (Accessor)o1;
				Accessor accessor2 = (Accessor)o2;
				int weight = accessor.Mapping.TypeDesc.Weight;
				int weight2 = accessor2.Mapping.TypeDesc.Weight;
				if (weight == weight2)
				{
					return 0;
				}
				if (weight < weight2)
				{
					return 1;
				}
				return -1;
			}
		}
	}
}
