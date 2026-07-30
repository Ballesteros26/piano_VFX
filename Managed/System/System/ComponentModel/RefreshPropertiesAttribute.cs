using System;

namespace System.ComponentModel
{
	/// <summary>Indicates that the property grid should refresh when the associated property value changes. This class cannot be inherited.</summary>
	// Token: 0x020002FE RID: 766
	[AttributeUsage(AttributeTargets.All)]
	public sealed class RefreshPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshPropertiesAttribute" /> class.</summary>
		/// <param name="refresh">A <see cref="T:System.ComponentModel.RefreshProperties" /> value indicating the nature of the refresh.</param>
		// Token: 0x060018A7 RID: 6311 RVA: 0x00068F4D File Offset: 0x0006714D
		public RefreshPropertiesAttribute(RefreshProperties refresh)
		{
			this.refresh = refresh;
		}

		/// <summary>Gets the refresh properties for the member.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.RefreshProperties" /> that indicates the current refresh properties for the member.</returns>
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060018A8 RID: 6312 RVA: 0x00068F5C File Offset: 0x0006715C
		public RefreshProperties RefreshProperties
		{
			get
			{
				return this.refresh;
			}
		}

		/// <summary>Overrides the object's <see cref="Overload:System.Object.Equals" /> method.</summary>
		/// <returns>true if the specified object is the same; otherwise, false.</returns>
		/// <param name="value">The object to test for equality. </param>
		// Token: 0x060018A9 RID: 6313 RVA: 0x00068F64 File Offset: 0x00067164
		public override bool Equals(object value)
		{
			return value is RefreshPropertiesAttribute && ((RefreshPropertiesAttribute)value).RefreshProperties == this.refresh;
		}

		/// <summary>Returns the hash code for this object.</summary>
		/// <returns>The hash code for the object that the attribute belongs to.</returns>
		// Token: 0x060018AA RID: 6314 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default; otherwise, false.</returns>
		// Token: 0x060018AB RID: 6315 RVA: 0x00068F83 File Offset: 0x00067183
		public override bool IsDefaultAttribute()
		{
			return this.Equals(RefreshPropertiesAttribute.Default);
		}

		/// <summary>Indicates that all properties are queried again and refreshed if the property value is changed. This field is read-only.</summary>
		// Token: 0x04001441 RID: 5185
		public static readonly RefreshPropertiesAttribute All = new RefreshPropertiesAttribute(RefreshProperties.All);

		/// <summary>Indicates that all properties are repainted if the property value is changed. This field is read-only.</summary>
		// Token: 0x04001442 RID: 5186
		public static readonly RefreshPropertiesAttribute Repaint = new RefreshPropertiesAttribute(RefreshProperties.Repaint);

		/// <summary>Indicates that no other properties are refreshed if the property value is changed. This field is read-only.</summary>
		// Token: 0x04001443 RID: 5187
		public static readonly RefreshPropertiesAttribute Default = new RefreshPropertiesAttribute(RefreshProperties.None);

		// Token: 0x04001444 RID: 5188
		private RefreshProperties refresh;
	}
}
