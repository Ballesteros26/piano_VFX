using System;

namespace System.ComponentModel
{
	/// <summary>Specifies a property that is offered by an extender provider. This class cannot be inherited.</summary>
	// Token: 0x02000272 RID: 626
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ExtenderProvidedPropertyAttribute : Attribute
	{
		// Token: 0x06001411 RID: 5137 RVA: 0x00052BE5 File Offset: 0x00050DE5
		internal static ExtenderProvidedPropertyAttribute Create(PropertyDescriptor extenderProperty, Type receiverType, IExtenderProvider provider)
		{
			return new ExtenderProvidedPropertyAttribute
			{
				extenderProperty = extenderProperty,
				receiverType = receiverType,
				provider = provider
			};
		}

		/// <summary>Gets the property that is being provided.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> encapsulating the property that is being provided.</returns>
		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06001413 RID: 5139 RVA: 0x00052C01 File Offset: 0x00050E01
		public PropertyDescriptor ExtenderProperty
		{
			get
			{
				return this.extenderProperty;
			}
		}

		/// <summary>Gets the extender provider that is providing the property.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IExtenderProvider" /> that is providing the property.</returns>
		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x00052C09 File Offset: 0x00050E09
		public IExtenderProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		/// <summary>Gets the type of object that can receive the property.</summary>
		/// <returns>A <see cref="T:System.Type" /> describing the type of object that can receive the property.</returns>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x00052C11 File Offset: 0x00050E11
		public Type ReceiverType
		{
			get
			{
				return this.receiverType;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x06001416 RID: 5142 RVA: 0x00052C1C File Offset: 0x00050E1C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = obj as ExtenderProvidedPropertyAttribute;
			return extenderProvidedPropertyAttribute != null && extenderProvidedPropertyAttribute.extenderProperty.Equals(this.extenderProperty) && extenderProvidedPropertyAttribute.provider.Equals(this.provider) && extenderProvidedPropertyAttribute.receiverType.Equals(this.receiverType);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001417 RID: 5143 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Provides an indication whether the value of this instance is the default value for the derived class.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06001418 RID: 5144 RVA: 0x00052C72 File Offset: 0x00050E72
		public override bool IsDefaultAttribute()
		{
			return this.receiverType == null;
		}

		// Token: 0x040012E7 RID: 4839
		private PropertyDescriptor extenderProperty;

		// Token: 0x040012E8 RID: 4840
		private IExtenderProvider provider;

		// Token: 0x040012E9 RID: 4841
		private Type receiverType;
	}
}
