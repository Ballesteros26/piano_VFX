using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a description of the sort operation applied to a data source.</summary>
	// Token: 0x020002A9 RID: 681
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ListSortDescription
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListSortDescription" /> class with the specified property description and direction.</summary>
		/// <param name="property">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the property by which the data source is sorted.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.ListSortDescription" />  values.</param>
		// Token: 0x0600150B RID: 5387 RVA: 0x00053B18 File Offset: 0x00051D18
		public ListSortDescription(PropertyDescriptor property, ListSortDirection direction)
		{
			this.property = property;
			this.sortDirection = direction;
		}

		/// <summary>Gets or sets the abstract description of a class property associated with this <see cref="T:System.ComponentModel.ListSortDescription" /></summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with this <see cref="T:System.ComponentModel.ListSortDescription" />. </returns>
		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x00053B2E File Offset: 0x00051D2E
		// (set) Token: 0x0600150D RID: 5389 RVA: 0x00053B36 File Offset: 0x00051D36
		public PropertyDescriptor PropertyDescriptor
		{
			get
			{
				return this.property;
			}
			set
			{
				this.property = value;
			}
		}

		/// <summary>Gets or sets the direction of the sort operation associated with this <see cref="T:System.ComponentModel.ListSortDescription" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.ListSortDirection" /> values. </returns>
		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x00053B3F File Offset: 0x00051D3F
		// (set) Token: 0x0600150F RID: 5391 RVA: 0x00053B47 File Offset: 0x00051D47
		public ListSortDirection SortDirection
		{
			get
			{
				return this.sortDirection;
			}
			set
			{
				this.sortDirection = value;
			}
		}

		// Token: 0x0400131C RID: 4892
		private PropertyDescriptor property;

		// Token: 0x0400131D RID: 4893
		private ListSortDirection sortDirection;
	}
}
