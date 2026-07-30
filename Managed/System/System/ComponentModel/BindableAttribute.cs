using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a member is typically used for binding. This class cannot be inherited.</summary>
	// Token: 0x02000234 RID: 564
	[AttributeUsage(AttributeTargets.All)]
	public sealed class BindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class with a Boolean value.</summary>
		/// <param name="bindable">true to use property for binding; otherwise, false.</param>
		// Token: 0x06001235 RID: 4661 RVA: 0x0004D5B2 File Offset: 0x0004B7B2
		public BindableAttribute(bool bindable)
			: this(bindable, BindingDirection.OneWay)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <param name="bindable">true to use property for binding; otherwise, false.</param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.BindingDirection" /> values.</param>
		// Token: 0x06001236 RID: 4662 RVA: 0x0004D5BC File Offset: 0x0004B7BC
		public BindableAttribute(bool bindable, BindingDirection direction)
		{
			this.bindable = bindable;
			this.direction = direction;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class with one of the <see cref="T:System.ComponentModel.BindableSupport" /> values.</summary>
		/// <param name="flags">One of the <see cref="T:System.ComponentModel.BindableSupport" /> values. </param>
		// Token: 0x06001237 RID: 4663 RVA: 0x0004D5D2 File Offset: 0x0004B7D2
		public BindableAttribute(BindableSupport flags)
			: this(flags, BindingDirection.OneWay)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <param name="flags">One of the <see cref="T:System.ComponentModel.BindableSupport" /> values. </param>
		/// <param name="direction">One of the <see cref="T:System.ComponentModel.BindingDirection" /> values.</param>
		// Token: 0x06001238 RID: 4664 RVA: 0x0004D5DC File Offset: 0x0004B7DC
		public BindableAttribute(BindableSupport flags, BindingDirection direction)
		{
			this.bindable = flags > BindableSupport.No;
			this.isDefault = flags == BindableSupport.Default;
			this.direction = direction;
		}

		/// <summary>Gets a value indicating that a property is typically used for binding.</summary>
		/// <returns>true if the property is typically used for binding; otherwise, false.</returns>
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x0004D5FF File Offset: 0x0004B7FF
		public bool Bindable
		{
			get
			{
				return this.bindable;
			}
		}

		/// <summary>Gets a value indicating the direction or directions of this property's data binding.</summary>
		/// <returns>The direction of this property’s data binding.</returns>
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x0004D607 File Offset: 0x0004B807
		public BindingDirection Direction
		{
			get
			{
				return this.direction;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.BindableAttribute" /> objects are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.BindableAttribute" /> is equal to the current <see cref="T:System.ComponentModel.BindableAttribute" />; false if it is not equal.</returns>
		/// <param name="obj">The object to compare.</param>
		// Token: 0x0600123B RID: 4667 RVA: 0x0004D60F File Offset: 0x0004B80F
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is BindableAttribute && ((BindableAttribute)obj).Bindable == this.bindable);
		}

		/// <summary>Serves as a hash function for the <see cref="T:System.ComponentModel.BindableAttribute" /> class.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.BindableAttribute" />.</returns>
		// Token: 0x0600123C RID: 4668 RVA: 0x0004D637 File Offset: 0x0004B837
		public override int GetHashCode()
		{
			return this.bindable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x0600123D RID: 4669 RVA: 0x0004D644 File Offset: 0x0004B844
		public override bool IsDefaultAttribute()
		{
			return this.Equals(BindableAttribute.Default) || this.isDefault;
		}

		/// <summary>Specifies that a property is typically used for binding. This field is read-only.</summary>
		// Token: 0x04001249 RID: 4681
		public static readonly BindableAttribute Yes = new BindableAttribute(true);

		/// <summary>Specifies that a property is not typically used for binding. This field is read-only.</summary>
		// Token: 0x0400124A RID: 4682
		public static readonly BindableAttribute No = new BindableAttribute(false);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.BindableAttribute" />, which is <see cref="F:System.ComponentModel.BindableAttribute.No" />. This field is read-only.</summary>
		// Token: 0x0400124B RID: 4683
		public static readonly BindableAttribute Default = BindableAttribute.No;

		// Token: 0x0400124C RID: 4684
		private bool bindable;

		// Token: 0x0400124D RID: 4685
		private bool isDefault;

		// Token: 0x0400124E RID: 4686
		private BindingDirection direction;
	}
}
