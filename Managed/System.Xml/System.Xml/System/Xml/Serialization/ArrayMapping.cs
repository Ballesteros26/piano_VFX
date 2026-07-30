using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002EA RID: 746
	internal class ArrayMapping : TypeMapping
	{
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001BDE RID: 7134 RVA: 0x00099F52 File Offset: 0x00098152
		// (set) Token: 0x06001BDF RID: 7135 RVA: 0x00099F5A File Offset: 0x0009815A
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

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001BE0 RID: 7136 RVA: 0x00099F6C File Offset: 0x0009816C
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

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001BE1 RID: 7137 RVA: 0x00099FD1 File Offset: 0x000981D1
		// (set) Token: 0x06001BE2 RID: 7138 RVA: 0x00099FD9 File Offset: 0x000981D9
		internal ArrayMapping Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001BE3 RID: 7139 RVA: 0x00099FE2 File Offset: 0x000981E2
		// (set) Token: 0x06001BE4 RID: 7140 RVA: 0x00099FEA File Offset: 0x000981EA
		internal StructMapping TopLevelMapping
		{
			get
			{
				return this.topLevelMapping;
			}
			set
			{
				this.topLevelMapping = value;
			}
		}

		// Token: 0x04001613 RID: 5651
		private ElementAccessor[] elements;

		// Token: 0x04001614 RID: 5652
		private ElementAccessor[] sortedElements;

		// Token: 0x04001615 RID: 5653
		private ArrayMapping next;

		// Token: 0x04001616 RID: 5654
		private StructMapping topLevelMapping;
	}
}
