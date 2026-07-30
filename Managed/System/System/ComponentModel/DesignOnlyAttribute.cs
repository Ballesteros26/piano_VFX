using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property can only be set at design time.</summary>
	// Token: 0x0200025E RID: 606
	[AttributeUsage(AttributeTargets.All)]
	public sealed class DesignOnlyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignOnlyAttribute" /> class.</summary>
		/// <param name="isDesignOnly">true if a property can be set only at design time; false if the property can be set at design time and at run time. </param>
		// Token: 0x06001371 RID: 4977 RVA: 0x000515B6 File Offset: 0x0004F7B6
		public DesignOnlyAttribute(bool isDesignOnly)
		{
			this.isDesignOnly = isDesignOnly;
		}

		/// <summary>Gets a value indicating whether a property can be set only at design time.</summary>
		/// <returns>true if a property can be set only at design time; otherwise, false.</returns>
		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001372 RID: 4978 RVA: 0x000515C5 File Offset: 0x0004F7C5
		public bool IsDesignOnly
		{
			get
			{
				return this.isDesignOnly;
			}
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06001373 RID: 4979 RVA: 0x000515CD File Offset: 0x0004F7CD
		public override bool IsDefaultAttribute()
		{
			return this.IsDesignOnly == DesignOnlyAttribute.Default.IsDesignOnly;
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001374 RID: 4980 RVA: 0x000515E4 File Offset: 0x0004F7E4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignOnlyAttribute designOnlyAttribute = obj as DesignOnlyAttribute;
			return designOnlyAttribute != null && designOnlyAttribute.isDesignOnly == this.isDesignOnly;
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x00051611 File Offset: 0x0004F811
		public override int GetHashCode()
		{
			return this.isDesignOnly.GetHashCode();
		}

		// Token: 0x040012B0 RID: 4784
		private bool isDesignOnly;

		/// <summary>Specifies that a property can be set only at design time. This static field is read-only.</summary>
		// Token: 0x040012B1 RID: 4785
		public static readonly DesignOnlyAttribute Yes = new DesignOnlyAttribute(true);

		/// <summary>Specifies that a property can be set at design time or at run time. This static field is read-only.</summary>
		// Token: 0x040012B2 RID: 4786
		public static readonly DesignOnlyAttribute No = new DesignOnlyAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DesignOnlyAttribute" />, which is <see cref="F:System.ComponentModel.DesignOnlyAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x040012B3 RID: 4787
		public static readonly DesignOnlyAttribute Default = DesignOnlyAttribute.No;
	}
}
