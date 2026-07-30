using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the data source and data member properties for a component that supports complex data binding. This class cannot be inherited.</summary>
	// Token: 0x02000244 RID: 580
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ComplexBindingPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using no parameters. </summary>
		// Token: 0x060012BA RID: 4794 RVA: 0x0004E64F File Offset: 0x0004C84F
		public ComplexBindingPropertiesAttribute()
		{
			this.dataSource = null;
			this.dataMember = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using the specified data source. </summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		// Token: 0x060012BB RID: 4795 RVA: 0x0004E665 File Offset: 0x0004C865
		public ComplexBindingPropertiesAttribute(string dataSource)
		{
			this.dataSource = dataSource;
			this.dataMember = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using the specified data source and data member. </summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		/// <param name="dataMember">The name of the property to be used as the source for data.</param>
		// Token: 0x060012BC RID: 4796 RVA: 0x0004E67B File Offset: 0x0004C87B
		public ComplexBindingPropertiesAttribute(string dataSource, string dataMember)
		{
			this.dataSource = dataSource;
			this.dataMember = dataMember;
		}

		/// <summary>Gets the name of the data source property for the component to which the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the data source property for the component to which <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0004E691 File Offset: 0x0004C891
		public string DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		/// <summary>Gets the name of the data member property for the component to which the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the data member property for the component to which <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound</returns>
		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x0004E699 File Offset: 0x0004C899
		public string DataMember
		{
			get
			{
				return this.dataMember;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> instance. </summary>
		/// <returns>true if the object is equal to the current instance; otherwise, false, indicating they are not equal.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> instance </param>
		// Token: 0x060012BF RID: 4799 RVA: 0x0004E6A4 File Offset: 0x0004C8A4
		public override bool Equals(object obj)
		{
			ComplexBindingPropertiesAttribute complexBindingPropertiesAttribute = obj as ComplexBindingPropertiesAttribute;
			return complexBindingPropertiesAttribute != null && complexBindingPropertiesAttribute.DataSource == this.dataSource && complexBindingPropertiesAttribute.DataMember == this.dataMember;
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060012C0 RID: 4800 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04001280 RID: 4736
		private readonly string dataSource;

		// Token: 0x04001281 RID: 4737
		private readonly string dataMember;

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class.</summary>
		// Token: 0x04001282 RID: 4738
		public static readonly ComplexBindingPropertiesAttribute Default = new ComplexBindingPropertiesAttribute();
	}
}
