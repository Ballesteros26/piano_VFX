using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>The <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> attribute is placed on a serializer to indicate the class to use as a default provider of that type of serializer. </summary>
	// Token: 0x0200034A RID: 842
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class DefaultSerializationProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> class with the given provider type.</summary>
		/// <param name="providerType">The <see cref="T:System.Type" /> of the serialization provider.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerType" /> is null.</exception>
		// Token: 0x06001A4F RID: 6735 RVA: 0x0006AFC5 File Offset: 0x000691C5
		public DefaultSerializationProviderAttribute(Type providerType)
		{
			if (providerType == null)
			{
				throw new ArgumentNullException("providerType");
			}
			this._providerTypeName = providerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DefaultSerializationProviderAttribute" /> class with the named provider type.</summary>
		/// <param name="providerTypeName">The name of the serialization provider type.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerTypeName" /> is null.</exception>
		// Token: 0x06001A50 RID: 6736 RVA: 0x0006AFED File Offset: 0x000691ED
		public DefaultSerializationProviderAttribute(string providerTypeName)
		{
			if (providerTypeName == null)
			{
				throw new ArgumentNullException("providerTypeName");
			}
			this._providerTypeName = providerTypeName;
		}

		/// <summary>Gets the type name of the serialization provider.</summary>
		/// <returns>A string containing the name of the provider.</returns>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x0006B00A File Offset: 0x0006920A
		public string ProviderTypeName
		{
			get
			{
				return this._providerTypeName;
			}
		}

		// Token: 0x04001835 RID: 6197
		private string _providerTypeName;
	}
}
