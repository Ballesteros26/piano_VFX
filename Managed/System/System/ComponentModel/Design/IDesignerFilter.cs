using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Provides an interface that enables a designer to access and filter the dictionaries of a <see cref="T:System.ComponentModel.TypeDescriptor" /> that stores the property, attribute, and event descriptors that a component designer can expose to the design-time environment.</summary>
	// Token: 0x02000329 RID: 809
	public interface IDesignerFilter
	{
		/// <summary>When overridden in a derived class, allows a designer to change or remove items from the set of attributes that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="attributes">The <see cref="T:System.Attribute" /> objects for the class of the component. The keys in the dictionary of attributes are the <see cref="P:System.Attribute.TypeId" /> values of the attributes. </param>
		// Token: 0x060019A2 RID: 6562
		void PostFilterAttributes(IDictionary attributes);

		/// <summary>When overridden in a derived class, allows a designer to change or remove items from the set of events that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="events">The <see cref="T:System.ComponentModel.EventDescriptor" /> objects that represent the events of the class of the component. The keys in the dictionary of events are event names. </param>
		// Token: 0x060019A3 RID: 6563
		void PostFilterEvents(IDictionary events);

		/// <summary>When overridden in a derived class, allows a designer to change or remove items from the set of properties that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="properties">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that represent the properties of the class of the component. The keys in the dictionary of properties are property names. </param>
		// Token: 0x060019A4 RID: 6564
		void PostFilterProperties(IDictionary properties);

		/// <summary>When overridden in a derived class, allows a designer to add items to the set of attributes that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="attributes">The <see cref="T:System.Attribute" /> objects for the class of the component. The keys in the dictionary of attributes are the <see cref="P:System.Attribute.TypeId" /> values of the attributes. </param>
		// Token: 0x060019A5 RID: 6565
		void PreFilterAttributes(IDictionary attributes);

		/// <summary>When overridden in a derived class, allows a designer to add items to the set of events that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="events">The <see cref="T:System.ComponentModel.EventDescriptor" /> objects that represent the events of the class of the component. The keys in the dictionary of events are event names. </param>
		// Token: 0x060019A6 RID: 6566
		void PreFilterEvents(IDictionary events);

		/// <summary>When overridden in a derived class, allows a designer to add items to the set of properties that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="properties">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that represent the properties of the class of the component. The keys in the dictionary of properties are property names. </param>
		// Token: 0x060019A7 RID: 6567
		void PreFilterProperties(IDictionary properties);
	}
}
