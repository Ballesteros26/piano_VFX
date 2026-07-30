using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that the designer for a class belongs to a certain category.</summary>
	// Token: 0x02000261 RID: 609
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class DesignerCategoryAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerCategoryAttribute" /> class with an empty string ("").</summary>
		// Token: 0x06001388 RID: 5000 RVA: 0x00051858 File Offset: 0x0004FA58
		public DesignerCategoryAttribute()
		{
			this.category = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerCategoryAttribute" /> class with the given category name.</summary>
		/// <param name="category">The name of the category. </param>
		// Token: 0x06001389 RID: 5001 RVA: 0x0005186B File Offset: 0x0004FA6B
		public DesignerCategoryAttribute(string category)
		{
			this.category = category;
		}

		/// <summary>Gets the name of the category.</summary>
		/// <returns>The name of the category.</returns>
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600138A RID: 5002 RVA: 0x0005187A File Offset: 0x0004FA7A
		public string Category
		{
			get
			{
				return this.category;
			}
		}

		/// <summary>Gets a unique identifier for this attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600138B RID: 5003 RVA: 0x00051882 File Offset: 0x0004FA82
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					this.typeId = base.GetType().FullName + this.Category;
				}
				return this.typeId;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignOnlyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x0600138C RID: 5004 RVA: 0x000518B0 File Offset: 0x0004FAB0
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerCategoryAttribute designerCategoryAttribute = obj as DesignerCategoryAttribute;
			return designerCategoryAttribute != null && designerCategoryAttribute.category == this.category;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600138D RID: 5005 RVA: 0x000518E0 File Offset: 0x0004FAE0
		public override int GetHashCode()
		{
			return this.category.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x0600138E RID: 5006 RVA: 0x000518ED File Offset: 0x0004FAED
		public override bool IsDefaultAttribute()
		{
			return this.category.Equals(DesignerCategoryAttribute.Default.Category);
		}

		// Token: 0x040012BB RID: 4795
		private string category;

		// Token: 0x040012BC RID: 4796
		private string typeId;

		/// <summary>Specifies that a component marked with this category use a component designer. This field is read-only.</summary>
		// Token: 0x040012BD RID: 4797
		public static readonly DesignerCategoryAttribute Component = new DesignerCategoryAttribute("Component");

		/// <summary>Specifies that a component marked with this category cannot use a visual designer. This static field is read-only.</summary>
		// Token: 0x040012BE RID: 4798
		public static readonly DesignerCategoryAttribute Default = new DesignerCategoryAttribute();

		/// <summary>Specifies that a component marked with this category use a form designer. This static field is read-only.</summary>
		// Token: 0x040012BF RID: 4799
		public static readonly DesignerCategoryAttribute Form = new DesignerCategoryAttribute("Form");

		/// <summary>Specifies that a component marked with this category use a generic designer. This static field is read-only.</summary>
		// Token: 0x040012C0 RID: 4800
		public static readonly DesignerCategoryAttribute Generic = new DesignerCategoryAttribute("Designer");
	}
}
