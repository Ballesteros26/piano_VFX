using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the name of the property that an implementer of <see cref="T:System.ComponentModel.IExtenderProvider" /> offers to other components. This class cannot be inherited</summary>
	// Token: 0x020002C4 RID: 708
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class ProvidePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProvidePropertyAttribute" /> class with the name of the property and its <see cref="T:System.Type" />.</summary>
		/// <param name="propertyName">The name of the property extending to an object of the specified type. </param>
		/// <param name="receiverType">The <see cref="T:System.Type" /> of the data type of the object that can receive the property. </param>
		// Token: 0x06001667 RID: 5735 RVA: 0x00057AB4 File Offset: 0x00055CB4
		public ProvidePropertyAttribute(string propertyName, Type receiverType)
		{
			this.propertyName = propertyName;
			this.receiverTypeName = receiverType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProvidePropertyAttribute" /> class with the name of the property and the type of its receiver.</summary>
		/// <param name="propertyName">The name of the property extending to an object of the specified type. </param>
		/// <param name="receiverTypeName">The name of the data type this property can extend. </param>
		// Token: 0x06001668 RID: 5736 RVA: 0x00057ACF File Offset: 0x00055CCF
		public ProvidePropertyAttribute(string propertyName, string receiverTypeName)
		{
			this.propertyName = propertyName;
			this.receiverTypeName = receiverTypeName;
		}

		/// <summary>Gets the name of a property that this class provides.</summary>
		/// <returns>The name of a property that this class provides.</returns>
		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001669 RID: 5737 RVA: 0x00057AE5 File Offset: 0x00055CE5
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		/// <summary>Gets the name of the data type this property can extend.</summary>
		/// <returns>The name of the data type this property can extend.</returns>
		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600166A RID: 5738 RVA: 0x00057AED File Offset: 0x00055CED
		public string ReceiverTypeName
		{
			get
			{
				return this.receiverTypeName;
			}
		}

		/// <summary>Gets a unique identifier for this attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600166B RID: 5739 RVA: 0x00057AF5 File Offset: 0x00055CF5
		public override object TypeId
		{
			get
			{
				return base.GetType().FullName + this.propertyName;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.ProvidePropertyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x0600166C RID: 5740 RVA: 0x00057B10 File Offset: 0x00055D10
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ProvidePropertyAttribute providePropertyAttribute = obj as ProvidePropertyAttribute;
			return providePropertyAttribute != null && providePropertyAttribute.propertyName == this.propertyName && providePropertyAttribute.receiverTypeName == this.receiverTypeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ProvidePropertyAttribute" />.</returns>
		// Token: 0x0600166D RID: 5741 RVA: 0x00057B53 File Offset: 0x00055D53
		public override int GetHashCode()
		{
			return this.propertyName.GetHashCode() ^ this.receiverTypeName.GetHashCode();
		}

		// Token: 0x04001399 RID: 5017
		private readonly string propertyName;

		// Token: 0x0400139A RID: 5018
		private readonly string receiverTypeName;
	}
}
