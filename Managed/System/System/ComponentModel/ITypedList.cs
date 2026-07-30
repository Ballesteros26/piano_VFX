using System;

namespace System.ComponentModel
{
	/// <summary>Provides functionality to discover the schema for a bindable list, where the properties available for binding differ from the public properties of the object to bind to. </summary>
	// Token: 0x0200028F RID: 655
	public interface ITypedList
	{
		/// <summary>Returns the name of the list.</summary>
		/// <returns>The name of the list.</returns>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects, for which the list name is returned. This can be null. </param>
		// Token: 0x0600148B RID: 5259
		string GetListName(PropertyDescriptor[] listAccessors);

		/// <summary>Returns the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties on each item used to bind data.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that represents the properties on each item used to bind data.</returns>
		/// <param name="listAccessors">An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects to find in the collection as bindable. This can be null. </param>
		// Token: 0x0600148C RID: 5260
		PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors);
	}
}
