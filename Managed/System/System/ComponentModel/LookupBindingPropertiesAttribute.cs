using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the properties that support lookup-based binding. This class cannot be inherited.</summary>
	// Token: 0x020002AD RID: 685
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class LookupBindingPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class using no parameters. </summary>
		// Token: 0x0600152A RID: 5418 RVA: 0x00053C89 File Offset: 0x00051E89
		public LookupBindingPropertiesAttribute()
		{
			this.dataSource = null;
			this.displayMember = null;
			this.valueMember = null;
			this.lookupMember = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class. </summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		/// <param name="displayMember">The name of the property to be used for the display name.</param>
		/// <param name="valueMember">The name of the property to be used as the source for values.</param>
		/// <param name="lookupMember">The name of the property to be used for lookups.</param>
		// Token: 0x0600152B RID: 5419 RVA: 0x00053CAD File Offset: 0x00051EAD
		public LookupBindingPropertiesAttribute(string dataSource, string displayMember, string valueMember, string lookupMember)
		{
			this.dataSource = dataSource;
			this.displayMember = displayMember;
			this.valueMember = valueMember;
			this.lookupMember = lookupMember;
		}

		/// <summary>Gets the name of the data source property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The data source property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600152C RID: 5420 RVA: 0x00053CD2 File Offset: 0x00051ED2
		public string DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		/// <summary>Gets the name of the display member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the display member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x00053CDA File Offset: 0x00051EDA
		public string DisplayMember
		{
			get
			{
				return this.displayMember;
			}
		}

		/// <summary>Gets the name of the value member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the value member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x00053CE2 File Offset: 0x00051EE2
		public string ValueMember
		{
			get
			{
				return this.valueMember;
			}
		}

		/// <summary>Gets the name of the lookup member for the component to which this attribute is bound.</summary>
		/// <returns>The name of the lookup member for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x00053CEA File Offset: 0x00051EEA
		public string LookupMember
		{
			get
			{
				return this.lookupMember;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> instance. </summary>
		/// <returns>true if the object is equal to the current instance; otherwise, false, indicating they are not equal.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> instance </param>
		// Token: 0x06001530 RID: 5424 RVA: 0x00053CF4 File Offset: 0x00051EF4
		public override bool Equals(object obj)
		{
			LookupBindingPropertiesAttribute lookupBindingPropertiesAttribute = obj as LookupBindingPropertiesAttribute;
			return lookupBindingPropertiesAttribute != null && lookupBindingPropertiesAttribute.DataSource == this.dataSource && lookupBindingPropertiesAttribute.displayMember == this.displayMember && lookupBindingPropertiesAttribute.valueMember == this.valueMember && lookupBindingPropertiesAttribute.lookupMember == this.lookupMember;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" />.</returns>
		// Token: 0x06001531 RID: 5425 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04001326 RID: 4902
		private readonly string dataSource;

		// Token: 0x04001327 RID: 4903
		private readonly string displayMember;

		// Token: 0x04001328 RID: 4904
		private readonly string valueMember;

		// Token: 0x04001329 RID: 4905
		private readonly string lookupMember;

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class.</summary>
		// Token: 0x0400132A RID: 4906
		public static readonly LookupBindingPropertiesAttribute Default = new LookupBindingPropertiesAttribute();
	}
}
