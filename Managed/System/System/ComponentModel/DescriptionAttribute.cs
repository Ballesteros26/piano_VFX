using System;

namespace System.ComponentModel
{
	/// <summary>Specifies a description for a property or event.</summary>
	// Token: 0x0200025D RID: 605
	[AttributeUsage(AttributeTargets.All)]
	public class DescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DescriptionAttribute" /> class with no parameters.</summary>
		// Token: 0x06001368 RID: 4968 RVA: 0x0005152B File Offset: 0x0004F72B
		public DescriptionAttribute()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DescriptionAttribute" /> class with a description.</summary>
		/// <param name="description">The description text. </param>
		// Token: 0x06001369 RID: 4969 RVA: 0x00051538 File Offset: 0x0004F738
		public DescriptionAttribute(string description)
		{
			this.description = description;
		}

		/// <summary>Gets the description stored in this attribute.</summary>
		/// <returns>The description stored in this attribute.</returns>
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x00051547 File Offset: 0x0004F747
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		/// <summary>Gets or sets the string stored as the description.</summary>
		/// <returns>The string stored as the description. The default value is an empty string ("").</returns>
		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x0005154F File Offset: 0x0004F74F
		// (set) Token: 0x0600136C RID: 4972 RVA: 0x00051557 File Offset: 0x0004F757
		protected string DescriptionValue
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DescriptionAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x0600136D RID: 4973 RVA: 0x00051560 File Offset: 0x0004F760
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DescriptionAttribute descriptionAttribute = obj as DescriptionAttribute;
			return descriptionAttribute != null && descriptionAttribute.Description == this.Description;
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00051590 File Offset: 0x0004F790
		public override int GetHashCode()
		{
			return this.Description.GetHashCode();
		}

		/// <summary>Returns a value indicating whether this is the default <see cref="T:System.ComponentModel.DescriptionAttribute" /> instance.</summary>
		/// <returns>true, if this is the default <see cref="T:System.ComponentModel.DescriptionAttribute" /> instance; otherwise, false.</returns>
		// Token: 0x0600136F RID: 4975 RVA: 0x0005159D File Offset: 0x0004F79D
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DescriptionAttribute.Default);
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DescriptionAttribute" />, which is an empty string (""). This static field is read-only.</summary>
		// Token: 0x040012AE RID: 4782
		public static readonly DescriptionAttribute Default = new DescriptionAttribute();

		// Token: 0x040012AF RID: 4783
		private string description;
	}
}
