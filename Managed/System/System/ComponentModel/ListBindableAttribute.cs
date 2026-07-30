using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that a list can be used as a data source. A visual designer should use this attribute to determine whether to display a particular list in a data-binding picker. This class cannot be inherited.</summary>
	// Token: 0x020002A5 RID: 677
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ListBindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListBindableAttribute" /> class using a value to indicate whether the list is bindable.</summary>
		/// <param name="listBindable">true if the list is bindable; otherwise, false. </param>
		// Token: 0x060014F8 RID: 5368 RVA: 0x00053A06 File Offset: 0x00051C06
		public ListBindableAttribute(bool listBindable)
		{
			this.listBindable = listBindable;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ListBindableAttribute" /> class using <see cref="T:System.ComponentModel.BindableSupport" /> to indicate whether the list is bindable.</summary>
		/// <param name="flags">A <see cref="T:System.ComponentModel.BindableSupport" /> that indicates whether the list is bindable. </param>
		// Token: 0x060014F9 RID: 5369 RVA: 0x00053A15 File Offset: 0x00051C15
		public ListBindableAttribute(BindableSupport flags)
		{
			this.listBindable = flags > BindableSupport.No;
			this.isDefault = flags == BindableSupport.Default;
		}

		/// <summary>Gets whether the list is bindable.</summary>
		/// <returns>true if the list is bindable; otherwise, false.</returns>
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x00053A31 File Offset: 0x00051C31
		public bool ListBindable
		{
			get
			{
				return this.listBindable;
			}
		}

		/// <summary>Returns whether the object passed is equal to this <see cref="T:System.ComponentModel.ListBindableAttribute" />.</summary>
		/// <returns>true if the object passed is equal to this <see cref="T:System.ComponentModel.ListBindableAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The object to test equality with. </param>
		// Token: 0x060014FB RID: 5371 RVA: 0x00053A3C File Offset: 0x00051C3C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ListBindableAttribute listBindableAttribute = obj as ListBindableAttribute;
			return listBindableAttribute != null && listBindableAttribute.ListBindable == this.listBindable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ListBindableAttribute" />.</returns>
		// Token: 0x060014FC RID: 5372 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns whether <see cref="P:System.ComponentModel.ListBindableAttribute.ListBindable" /> is set to the default value.</summary>
		/// <returns>true if <see cref="P:System.ComponentModel.ListBindableAttribute.ListBindable" /> is set to the default value; otherwise, false.</returns>
		// Token: 0x060014FD RID: 5373 RVA: 0x00053A69 File Offset: 0x00051C69
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ListBindableAttribute.Default) || this.isDefault;
		}

		/// <summary>Specifies that the list is bindable. This static field is read-only.</summary>
		// Token: 0x0400130A RID: 4874
		public static readonly ListBindableAttribute Yes = new ListBindableAttribute(true);

		/// <summary>Specifies that the list is not bindable. This static field is read-only.</summary>
		// Token: 0x0400130B RID: 4875
		public static readonly ListBindableAttribute No = new ListBindableAttribute(false);

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ListBindableAttribute" />.</summary>
		// Token: 0x0400130C RID: 4876
		public static readonly ListBindableAttribute Default = ListBindableAttribute.Yes;

		// Token: 0x0400130D RID: 4877
		private bool listBindable;

		// Token: 0x0400130E RID: 4878
		private bool isDefault;
	}
}
