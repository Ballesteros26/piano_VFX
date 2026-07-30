using System;

namespace System.ComponentModel
{
	/// <summary>Enables attribute redirection. This class cannot be inherited.</summary>
	// Token: 0x02000230 RID: 560
	[AttributeUsage(AttributeTargets.Property)]
	public class AttributeProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type name.</summary>
		/// <param name="typeName">The name of the type to specify.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null.</exception>
		// Token: 0x06001210 RID: 4624 RVA: 0x0004D224 File Offset: 0x0004B424
		public AttributeProviderAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this._typeName = typeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type name and property name.</summary>
		/// <param name="typeName">The name of the type to specify.</param>
		/// <param name="propertyName">The name of the property for which attributes will be retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="propertyName" /> is null.</exception>
		// Token: 0x06001211 RID: 4625 RVA: 0x0004D241 File Offset: 0x0004B441
		public AttributeProviderAttribute(string typeName, string propertyName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (propertyName == null)
			{
				throw new ArgumentNullException("propertyName");
			}
			this._typeName = typeName;
			this._propertyName = propertyName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AttributeProviderAttribute" /> class with the given type.</summary>
		/// <param name="type">The type to specify.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x06001212 RID: 4626 RVA: 0x0004D273 File Offset: 0x0004B473
		public AttributeProviderAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this._typeName = type.AssemblyQualifiedName;
		}

		/// <summary>Gets the assembly qualified type name passed into the constructor.</summary>
		/// <returns>The assembly qualified name of the type specified in the constructor.</returns>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x0004D29B File Offset: 0x0004B49B
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		/// <summary>Gets the name of the property for which attributes will be retrieved.</summary>
		/// <returns>The name of the property for which attributes will be retrieved.</returns>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x0004D2A3 File Offset: 0x0004B4A3
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x0400123C RID: 4668
		private string _typeName;

		// Token: 0x0400123D RID: 4669
		private string _propertyName;
	}
}
