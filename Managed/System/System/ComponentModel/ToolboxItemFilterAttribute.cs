using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the filter string and filter type to use for a toolbox item.</summary>
	// Token: 0x020002D8 RID: 728
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	[Serializable]
	public sealed class ToolboxItemFilterAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> class using the specified filter string.</summary>
		/// <param name="filterString">The filter string for the toolbox item. </param>
		// Token: 0x06001723 RID: 5923 RVA: 0x0005BEB7 File Offset: 0x0005A0B7
		public ToolboxItemFilterAttribute(string filterString)
			: this(filterString, ToolboxItemFilterType.Allow)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> class using the specified filter string and type.</summary>
		/// <param name="filterString">The filter string for the toolbox item. </param>
		/// <param name="filterType">A <see cref="T:System.ComponentModel.ToolboxItemFilterType" /> indicating the type of the filter. </param>
		// Token: 0x06001724 RID: 5924 RVA: 0x0005BEC1 File Offset: 0x0005A0C1
		public ToolboxItemFilterAttribute(string filterString, ToolboxItemFilterType filterType)
		{
			if (filterString == null)
			{
				filterString = string.Empty;
			}
			this.filterString = filterString;
			this.filterType = filterType;
		}

		/// <summary>Gets the filter string for the toolbox item.</summary>
		/// <returns>The filter string for the toolbox item.</returns>
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x0005BEE1 File Offset: 0x0005A0E1
		public string FilterString
		{
			get
			{
				return this.filterString;
			}
		}

		/// <summary>Gets the type of the filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.ToolboxItemFilterType" /> that indicates the type of the filter.</returns>
		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001726 RID: 5926 RVA: 0x0005BEE9 File Offset: 0x0005A0E9
		public ToolboxItemFilterType FilterType
		{
			get
			{
				return this.filterType;
			}
		}

		/// <summary>Gets the type ID for the attribute.</summary>
		/// <returns>The type ID for this attribute. All <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> objects with the same filter string return the same type ID.</returns>
		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x0005BEF1 File Offset: 0x0005A0F1
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					this.typeId = base.GetType().FullName + this.filterString;
				}
				return this.typeId;
			}
		}

		/// <param name="obj">The object to compare.</param>
		// Token: 0x06001728 RID: 5928 RVA: 0x0005BF20 File Offset: 0x0005A120
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj as ToolboxItemFilterAttribute;
			return toolboxItemFilterAttribute != null && toolboxItemFilterAttribute.FilterType.Equals(this.FilterType) && toolboxItemFilterAttribute.FilterString.Equals(this.FilterString);
		}

		// Token: 0x06001729 RID: 5929 RVA: 0x0005BF71 File Offset: 0x0005A171
		public override int GetHashCode()
		{
			return this.filterString.GetHashCode();
		}

		/// <summary>Indicates whether the specified object has a matching filter string.</summary>
		/// <returns>true if the specified object has a matching filter string; otherwise, false.</returns>
		/// <param name="obj">The object to test for a matching filter string. </param>
		// Token: 0x0600172A RID: 5930 RVA: 0x0005BF80 File Offset: 0x0005A180
		public override bool Match(object obj)
		{
			ToolboxItemFilterAttribute toolboxItemFilterAttribute = obj as ToolboxItemFilterAttribute;
			return toolboxItemFilterAttribute != null && toolboxItemFilterAttribute.FilterString.Equals(this.FilterString);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0005BFAF File Offset: 0x0005A1AF
		public override string ToString()
		{
			return this.filterString + "," + Enum.GetName(typeof(ToolboxItemFilterType), this.filterType);
		}

		// Token: 0x040013E9 RID: 5097
		private ToolboxItemFilterType filterType;

		// Token: 0x040013EA RID: 5098
		private string filterString;

		// Token: 0x040013EB RID: 5099
		private string typeId;
	}
}
