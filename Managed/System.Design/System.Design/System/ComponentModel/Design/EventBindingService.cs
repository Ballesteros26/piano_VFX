using System;
using System.Collections;
using System.Collections.Generic;

namespace System.ComponentModel.Design
{
	/// <summary>A default implementation of the <see cref="T:System.ComponentModel.Design.IEventBindingService" /> interface.</summary>
	// Token: 0x02000123 RID: 291
	public abstract class EventBindingService : IEventBindingService
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.EventBindingService" /> class. </summary>
		/// <param name="provider">The service provider from which <see cref="T:System.ComponentModel.Design.EventBindingService" /> will query for services.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="provider" /> is null.</exception>
		// Token: 0x060008A1 RID: 2209 RVA: 0x0000EE3B File Offset: 0x0000D03B
		protected EventBindingService(IServiceProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			this._provider = provider;
		}

		/// <summary>Displays the user code for the specified method.</summary>
		/// <returns>true if it is possible to display the code; otherwise, false.</returns>
		/// <param name="component">The component to which the method is bound.</param>
		/// <param name="e">The <see cref="T:System.ComponentModel.EventDescriptor" /> for the event handler.</param>
		/// <param name="methodName">The name of the method for which to display code.</param>
		// Token: 0x060008A2 RID: 2210
		protected abstract bool ShowCode(IComponent component, EventDescriptor e, string methodName);

		/// <summary>Displays the user code at the given line number.</summary>
		/// <returns>true if it is possible to display the code; otherwise, false.</returns>
		/// <param name="lineNumber">The line number to show.</param>
		// Token: 0x060008A3 RID: 2211
		protected abstract bool ShowCode(int lineNumber);

		/// <summary>Displays user code.</summary>
		/// <returns>true if it is possible to display the code; otherwise, false.</returns>
		// Token: 0x060008A4 RID: 2212
		protected abstract bool ShowCode();

		/// <summary>Creates a unique method name.</summary>
		/// <returns>The unique method name.</returns>
		/// <param name="component">The component for which the method name will be created.</param>
		/// <param name="e">The event to create a name for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="e" /> is null.</exception>
		// Token: 0x060008A5 RID: 2213
		protected abstract string CreateUniqueMethodName(IComponent component, EventDescriptor e);

		/// <summary>Returns a collection of names of compatible methods.</summary>
		/// <returns>A collection of strings that are names of compatible methods.</returns>
		/// <param name="e">The <see cref="T:System.ComponentModel.EventDescriptor" /> containing the compatible delegate.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is null.</exception>
		// Token: 0x060008A6 RID: 2214
		protected abstract ICollection GetCompatibleMethods(EventDescriptor e);

		/// <summary>Provides a notification that a particular method is no longer being used by an event handler.</summary>
		/// <param name="component">The component to which the method is bound.</param>
		/// <param name="e">The <see cref="T:System.ComponentModel.EventDescriptor" /> for the event handler.</param>
		/// <param name="methodName">The name of the method to be freed.</param>
		// Token: 0x060008A7 RID: 2215 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void FreeMethod(IComponent component, EventDescriptor e, string methodName)
		{
		}

		/// <summary>Provides a notification that a particular method is being used by an event handler.</summary>
		/// <param name="component">The component to which the method is bound.</param>
		/// <param name="e">The <see cref="T:System.ComponentModel.EventDescriptor" /> for the event handler.</param>
		/// <param name="methodName">The name of the method.</param>
		// Token: 0x060008A8 RID: 2216 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void UseMethod(IComponent component, EventDescriptor e, string methodName)
		{
		}

		/// <summary>Validates that the provided method name is valid for the language or script being used.</summary>
		/// <param name="methodName">The method name to validate.</param>
		// Token: 0x060008A9 RID: 2217 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void ValidateMethodName(string methodName)
		{
		}

		/// <summary>Gets the requested service from the service provider.</summary>
		/// <returns>A reference to the service specified by <paramref name="serviceType" />, or null if the requested service is not available.</returns>
		/// <param name="serviceType">The type of service to retrieve.</param>
		// Token: 0x060008AA RID: 2218 RVA: 0x0000EE58 File Offset: 0x0000D058
		protected object GetService(Type serviceType)
		{
			if (this._provider != null)
			{
				return this._provider.GetService(serviceType);
			}
			return null;
		}

		/// <summary>Creates a unique name for an event-handler method for the specified component and event.</summary>
		/// <returns>The recommended name for the event-handler method for this event.</returns>
		/// <param name="component">The component instance the event is connected to.</param>
		/// <param name="e">The event to create a name for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="e" /> is null.</exception>
		// Token: 0x060008AB RID: 2219 RVA: 0x0000EE70 File Offset: 0x0000D070
		string IEventBindingService.CreateUniqueMethodName(IComponent component, EventDescriptor eventDescriptor)
		{
			if (eventDescriptor == null)
			{
				throw new ArgumentNullException("eventDescriptor");
			}
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return this.CreateUniqueMethodName(component, eventDescriptor);
		}

		/// <summary>Gets a collection of event-handler methods that have a method signature compatible with the specified event.</summary>
		/// <returns>A collection of strings that are names of compatible methods.</returns>
		/// <param name="e">The event to get the compatible event-handler methods for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is null.</exception>
		// Token: 0x060008AC RID: 2220 RVA: 0x0000EE96 File Offset: 0x0000D096
		ICollection IEventBindingService.GetCompatibleMethods(EventDescriptor eventDescriptor)
		{
			if (eventDescriptor == null)
			{
				throw new ArgumentNullException("eventDescriptor");
			}
			return this.GetCompatibleMethods(eventDescriptor);
		}

		/// <summary>Gets an <see cref="T:System.ComponentModel.EventDescriptor" /> for the event that the specified property descriptor represents, if it represents an event.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> for the event that the property represents, or null if the property does not represent an event.</returns>
		/// <param name="property">The property that represents an event.</param>
		// Token: 0x060008AD RID: 2221 RVA: 0x0000EEB0 File Offset: 0x0000D0B0
		EventDescriptor IEventBindingService.GetEvent(PropertyDescriptor property)
		{
			if (property == null)
			{
				throw new ArgumentNullException("property");
			}
			EventPropertyDescriptor eventPropertyDescriptor = property as EventPropertyDescriptor;
			if (eventPropertyDescriptor == null)
			{
				return null;
			}
			return eventPropertyDescriptor.InternalEventDescriptor;
		}

		/// <summary>Converts a set of event descriptors to a set of property descriptors.</summary>
		/// <returns>An array of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that describe the event set.</returns>
		/// <param name="events">The events to convert to properties.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="events" /> is null.</exception>
		// Token: 0x060008AE RID: 2222 RVA: 0x0000EEE0 File Offset: 0x0000D0E0
		PropertyDescriptorCollection IEventBindingService.GetEventProperties(EventDescriptorCollection events)
		{
			if (events == null)
			{
				throw new ArgumentNullException("events");
			}
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in events)
			{
				EventDescriptor eventDescriptor = (EventDescriptor)obj;
				list.Add(((IEventBindingService)this).GetEventProperty(eventDescriptor));
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		/// <summary>Converts a single event descriptor to a property descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that describes the event.</returns>
		/// <param name="e">The event to convert.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="e" /> is null.</exception>
		// Token: 0x060008AF RID: 2223 RVA: 0x0000EF5C File Offset: 0x0000D15C
		PropertyDescriptor IEventBindingService.GetEventProperty(EventDescriptor eventDescriptor)
		{
			if (eventDescriptor == null)
			{
				throw new ArgumentNullException("eventDescriptor");
			}
			return new EventPropertyDescriptor(eventDescriptor);
		}

		/// <summary>Displays the user code for the specified event.</summary>
		/// <returns>true if the code is displayed; otherwise, false.</returns>
		/// <param name="component">The component that the event is connected to.</param>
		/// <param name="e">The event to display.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="events" /> is null.</exception>
		// Token: 0x060008B0 RID: 2224 RVA: 0x0000EF72 File Offset: 0x0000D172
		bool IEventBindingService.ShowCode(IComponent component, EventDescriptor eventDescriptor)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (eventDescriptor == null)
			{
				throw new ArgumentNullException("eventDescriptor");
			}
			return this.ShowCode(component, eventDescriptor, (string)((IEventBindingService)this).GetEventProperty(eventDescriptor).GetValue(component));
		}

		/// <summary>Displays the user code for the designer at the specified line.</summary>
		/// <returns>true if the code is displayed; otherwise, false.</returns>
		/// <param name="lineNumber">The line number to place the caret on.</param>
		// Token: 0x060008B1 RID: 2225 RVA: 0x0000EFAA File Offset: 0x0000D1AA
		bool IEventBindingService.ShowCode(int lineNumber)
		{
			return this.ShowCode(lineNumber);
		}

		/// <summary>Displays the user code for the designer.</summary>
		/// <returns>true if the code is displayed; otherwise, false.</returns>
		// Token: 0x060008B2 RID: 2226 RVA: 0x0000EFB3 File Offset: 0x0000D1B3
		bool IEventBindingService.ShowCode()
		{
			return this.ShowCode();
		}

		// Token: 0x040001F2 RID: 498
		private IServiceProvider _provider;
	}
}
