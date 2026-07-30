using System;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Windows.Forms.Design
{
	/// <summary>Provides a <see cref="T:System.Windows.Forms.Design.PropertyTab" /> that can display events for selection and linking.</summary>
	// Token: 0x02000014 RID: 20
	public class EventsTab : PropertyTab
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x00004218 File Offset: 0x00002418
		private EventsTab()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.EventsTab" /> class.</summary>
		/// <param name="sp">An <see cref="T:System.IServiceProvider" /> to use. </param>
		// Token: 0x060000A8 RID: 168 RVA: 0x00004220 File Offset: 0x00002420
		public EventsTab(IServiceProvider sp)
		{
			this.serviceProvider = sp;
		}

		/// <summary>Gets the Help keyword for the tab.</summary>
		/// <returns>The Help keyword for the tab.</returns>
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004230 File Offset: 0x00002430
		public override string HelpKeyword
		{
			get
			{
				return this.TabName;
			}
		}

		/// <summary>Gets the name of the tab.</summary>
		/// <returns>The name of the tab.</returns>
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00004238 File Offset: 0x00002438
		public override string TabName
		{
			get
			{
				return Locale.GetText("Events");
			}
		}

		/// <summary>Gets all the properties of the event tab that match the specified attributes and context.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties. This will be an empty <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> if the component does not implement an event service.</returns>
		/// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to gain context information. </param>
		/// <param name="component">The component to retrieve the properties of. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that indicates the attributes of the event properties to retrieve. </param>
		// Token: 0x060000AB RID: 171 RVA: 0x00004244 File Offset: 0x00002444
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object component, Attribute[] attributes)
		{
			IEventBindingService eventBindingService = null;
			if (this.serviceProvider != null)
			{
				eventBindingService = (IEventBindingService)this.serviceProvider.GetService(typeof(IEventBindingService));
			}
			if (eventBindingService == null)
			{
				return new PropertyDescriptorCollection(null);
			}
			EventDescriptorCollection eventDescriptorCollection;
			if (attributes != null)
			{
				eventDescriptorCollection = TypeDescriptor.GetEvents(component, attributes);
			}
			else
			{
				eventDescriptorCollection = TypeDescriptor.GetEvents(component);
			}
			return eventBindingService.GetEventProperties(eventDescriptorCollection);
		}

		/// <summary>Gets all the properties of the event tab that match the specified attributes.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> that contains the properties. This will be an empty <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> if the component does not implement an event service.</returns>
		/// <param name="component">The component to retrieve the properties of. </param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> that indicates the attributes of the event properties to retrieve. </param>
		// Token: 0x060000AC RID: 172 RVA: 0x000042AC File Offset: 0x000024AC
		public override PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
		{
			return this.GetProperties(null, component, attributes);
		}

		/// <summary>Gets a value indicating whether the specified object can be extended.</summary>
		/// <returns>true if the specified object can be extended; otherwise, false.</returns>
		/// <param name="extendee">The object to test for extensibility. </param>
		// Token: 0x060000AD RID: 173 RVA: 0x000042B8 File Offset: 0x000024B8
		public override bool CanExtend(object extendee)
		{
			return false;
		}

		/// <summary>Gets the default property from the specified object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> indicating the default property.</returns>
		/// <param name="obj">The object to retrieve the default property of. </param>
		// Token: 0x060000AE RID: 174 RVA: 0x000042BC File Offset: 0x000024BC
		public override PropertyDescriptor GetDefaultProperty(object obj)
		{
			if (this.serviceProvider == null)
			{
				return null;
			}
			EventDescriptor defaultEvent = TypeDescriptor.GetDefaultEvent(obj);
			IEventBindingService eventBindingService = (IEventBindingService)this.serviceProvider.GetService(typeof(IEventBindingService));
			if (defaultEvent != null && eventBindingService != null)
			{
				return eventBindingService.GetEventProperty(defaultEvent);
			}
			return null;
		}

		// Token: 0x0400004C RID: 76
		private IServiceProvider serviceProvider;
	}
}
