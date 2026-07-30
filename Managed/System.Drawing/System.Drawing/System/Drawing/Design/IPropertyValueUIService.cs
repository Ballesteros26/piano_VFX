using System;
using System.ComponentModel;

namespace System.Drawing.Design
{
	/// <summary>Provides an interface to manage the images, ToolTips, and event handlers for the properties of a component displayed in a property browser.</summary>
	// Token: 0x0200011C RID: 284
	public interface IPropertyValueUIService
	{
		/// <summary>Occurs when the list of <see cref="T:System.Drawing.Design.PropertyValueUIItem" /> objects is modified.</summary>
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000D40 RID: 3392
		// (remove) Token: 0x06000D41 RID: 3393
		event EventHandler PropertyUIValueItemsChanged;

		/// <summary>Adds the specified <see cref="T:System.Drawing.Design.PropertyValueUIHandler" /> to this service.</summary>
		/// <param name="newHandler">The property value UI handler to add. </param>
		// Token: 0x06000D42 RID: 3394
		void AddPropertyValueUIHandler(PropertyValueUIHandler newHandler);

		/// <summary>Gets the <see cref="T:System.Drawing.Design.PropertyValueUIItem" /> objects that match the specified context and property descriptor characteristics.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.Design.PropertyValueUIItem" /> objects that match the specified parameters.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain additional context information. </param>
		/// <param name="propDesc">A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that indicates the property to match with the properties to return. </param>
		// Token: 0x06000D43 RID: 3395
		PropertyValueUIItem[] GetPropertyUIValueItems(ITypeDescriptorContext context, PropertyDescriptor propDesc);

		/// <summary>Notifies the <see cref="T:System.Drawing.Design.IPropertyValueUIService" /> implementation that the global list of <see cref="T:System.Drawing.Design.PropertyValueUIItem" /> objects has been modified.</summary>
		// Token: 0x06000D44 RID: 3396
		void NotifyPropertyValueUIItemsChanged();

		/// <summary>Removes the specified <see cref="T:System.Drawing.Design.PropertyValueUIHandler" /> from the property value UI service.</summary>
		/// <param name="newHandler">The handler to remove. </param>
		// Token: 0x06000D45 RID: 3397
		void RemovePropertyValueUIHandler(PropertyValueUIHandler newHandler);
	}
}
