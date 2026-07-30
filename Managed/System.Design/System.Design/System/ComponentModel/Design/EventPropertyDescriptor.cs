using System;

namespace System.ComponentModel.Design
{
	// Token: 0x02000124 RID: 292
	internal class EventPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x060008B3 RID: 2227 RVA: 0x0000EFBB File Offset: 0x0000D1BB
		public EventPropertyDescriptor(EventDescriptor eventDescriptor)
			: base(eventDescriptor)
		{
			if (eventDescriptor == null)
			{
				throw new ArgumentNullException("eventDescriptor");
			}
			this._eventDescriptor = eventDescriptor;
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x000023D8 File Offset: 0x000005D8
		public override bool CanResetValue(object component)
		{
			return true;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0000EFD9 File Offset: 0x0000D1D9
		public override Type ComponentType
		{
			get
			{
				return this._eventDescriptor.ComponentType;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0000241E File Offset: 0x0000061E
		public override bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060008B7 RID: 2231 RVA: 0x0000EFE6 File Offset: 0x0000D1E6
		public override Type PropertyType
		{
			get
			{
				return this._eventDescriptor.EventType;
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x0000EFF3 File Offset: 0x0000D1F3
		public override void ResetValue(object component)
		{
			this.SetValue(component, null);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0000F000 File Offset: 0x0000D200
		public override object GetValue(object component)
		{
			if (component is IComponent && ((IComponent)component).Site != null)
			{
				IDictionaryService dictionaryService = ((IComponent)component).Site.GetService(typeof(IDictionaryService)) as IDictionaryService;
				if (dictionaryService != null)
				{
					return dictionaryService.GetValue(base.Name);
				}
			}
			return null;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0000F054 File Offset: 0x0000D254
		public override void SetValue(object component, object value)
		{
			if (component is IComponent && ((IComponent)component).Site != null)
			{
				IDictionaryService dictionaryService = ((IComponent)component).Site.GetService(typeof(IDictionaryService)) as IDictionaryService;
				if (dictionaryService != null)
				{
					dictionaryService.SetValue(base.Name, value);
				}
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0000F0A6 File Offset: 0x0000D2A6
		public override bool ShouldSerializeValue(object component)
		{
			return this.GetValue(component) != null;
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		public override TypeConverter Converter
		{
			get
			{
				return TypeDescriptor.GetConverter(string.Empty);
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x0000F0C0 File Offset: 0x0000D2C0
		internal EventDescriptor InternalEventDescriptor
		{
			get
			{
				return this._eventDescriptor;
			}
		}

		// Token: 0x040001F3 RID: 499
		private EventDescriptor _eventDescriptor;
	}
}
