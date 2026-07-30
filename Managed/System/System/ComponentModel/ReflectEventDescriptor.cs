using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020002C9 RID: 713
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class ReflectEventDescriptor : EventDescriptor
	{
		// Token: 0x06001685 RID: 5765 RVA: 0x00057F20 File Offset: 0x00056120
		public ReflectEventDescriptor(Type componentClass, string name, Type type, Attribute[] attributes)
			: base(name, attributes)
		{
			if (componentClass == null)
			{
				throw new ArgumentException(global::SR.GetString("Null is not a valid value for {0}.", new object[] { "componentClass" }));
			}
			if (type == null || !typeof(Delegate).IsAssignableFrom(type))
			{
				throw new ArgumentException(global::SR.GetString("Invalid type for the {0} event.", new object[] { name }));
			}
			this.componentClass = componentClass;
			this.type = type;
		}

		// Token: 0x06001686 RID: 5766 RVA: 0x00057FA0 File Offset: 0x000561A0
		public ReflectEventDescriptor(Type componentClass, EventInfo eventInfo)
			: base(eventInfo.Name, new Attribute[0])
		{
			if (componentClass == null)
			{
				throw new ArgumentException(global::SR.GetString("Null is not a valid value for {0}.", new object[] { "componentClass" }));
			}
			this.componentClass = componentClass;
			this.realEvent = eventInfo;
		}

		// Token: 0x06001687 RID: 5767 RVA: 0x00057FF4 File Offset: 0x000561F4
		public ReflectEventDescriptor(Type componentType, EventDescriptor oldReflectEventDescriptor, Attribute[] attributes)
			: base(oldReflectEventDescriptor, attributes)
		{
			this.componentClass = componentType;
			this.type = oldReflectEventDescriptor.EventType;
			ReflectEventDescriptor reflectEventDescriptor = oldReflectEventDescriptor as ReflectEventDescriptor;
			if (reflectEventDescriptor != null)
			{
				this.addMethod = reflectEventDescriptor.addMethod;
				this.removeMethod = reflectEventDescriptor.removeMethod;
				this.filledMethods = true;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001688 RID: 5768 RVA: 0x00058045 File Offset: 0x00056245
		public override Type ComponentType
		{
			get
			{
				return this.componentClass;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x0005804D File Offset: 0x0005624D
		public override Type EventType
		{
			get
			{
				this.FillMethods();
				return this.type;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600168A RID: 5770 RVA: 0x0005805B File Offset: 0x0005625B
		public override bool IsMulticast
		{
			get
			{
				return typeof(MulticastDelegate).IsAssignableFrom(this.EventType);
			}
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00058074 File Offset: 0x00056274
		public override void AddEventHandler(object component, Delegate value)
		{
			this.FillMethods();
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					try
					{
						componentChangeService.OnComponentChanging(component, this);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				bool flag = false;
				if (site != null && site.DesignMode)
				{
					if (this.EventType != value.GetType())
					{
						throw new ArgumentException(global::SR.GetString("Invalid event handler for the {0} event.", new object[] { this.Name }));
					}
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						Delegate @delegate = (Delegate)dictionaryService.GetValue(this);
						@delegate = Delegate.Combine(@delegate, value);
						dictionaryService.SetValue(this, @delegate);
						flag = true;
					}
				}
				if (!flag)
				{
					SecurityUtils.MethodInfoInvoke(this.addMethod, component, new object[] { value });
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, null, value);
				}
			}
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00058184 File Offset: 0x00056384
		protected override void FillAttributes(IList attributes)
		{
			this.FillMethods();
			if (this.realEvent != null)
			{
				this.FillEventInfoAttribute(this.realEvent, attributes);
			}
			else
			{
				this.FillSingleMethodAttribute(this.removeMethod, attributes);
				this.FillSingleMethodAttribute(this.addMethod, attributes);
			}
			base.FillAttributes(attributes);
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x000581D8 File Offset: 0x000563D8
		private void FillEventInfoAttribute(EventInfo realEventInfo, IList attributes)
		{
			string name = realEventInfo.Name;
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			Type type = realEventInfo.ReflectedType;
			int num = 0;
			while (type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = realEventInfo.ReflectedType;
				Attribute[][] array = new Attribute[num][];
				while (type != typeof(object))
				{
					MemberInfo @event = type.GetEvent(name, bindingFlags);
					if (@event != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(@event);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute attribute in array3)
						{
							attributes.Add(attribute);
						}
					}
				}
			}
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x000582B4 File Offset: 0x000564B4
		private void FillMethods()
		{
			if (this.filledMethods)
			{
				return;
			}
			if (this.realEvent != null)
			{
				this.addMethod = this.realEvent.GetAddMethod();
				this.removeMethod = this.realEvent.GetRemoveMethod();
				EventInfo eventInfo = null;
				if (this.addMethod == null || this.removeMethod == null)
				{
					Type baseType = this.componentClass.BaseType;
					while (baseType != null && baseType != typeof(object))
					{
						BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
						EventInfo @event = baseType.GetEvent(this.realEvent.Name, bindingFlags);
						if (@event.GetAddMethod() != null)
						{
							eventInfo = @event;
							break;
						}
					}
				}
				if (eventInfo != null)
				{
					this.addMethod = eventInfo.GetAddMethod();
					this.removeMethod = eventInfo.GetRemoveMethod();
					this.type = eventInfo.EventHandlerType;
				}
				else
				{
					this.type = this.realEvent.EventHandlerType;
				}
			}
			else
			{
				this.realEvent = this.componentClass.GetEvent(this.Name);
				if (this.realEvent != null)
				{
					this.FillMethods();
					return;
				}
				Type[] array = new Type[] { this.type };
				this.addMethod = MemberDescriptor.FindMethod(this.componentClass, "AddOn" + this.Name, array, typeof(void));
				this.removeMethod = MemberDescriptor.FindMethod(this.componentClass, "RemoveOn" + this.Name, array, typeof(void));
				if (this.addMethod == null || this.removeMethod == null)
				{
					throw new ArgumentException(global::SR.GetString("Accessor methods for the {0} event are missing.", new object[] { this.Name }));
				}
			}
			this.filledMethods = true;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x00058490 File Offset: 0x00056690
		private void FillSingleMethodAttribute(MethodInfo realMethodInfo, IList attributes)
		{
			string name = realMethodInfo.Name;
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
			Type type = realMethodInfo.ReflectedType;
			int num = 0;
			while (type != null && type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = realMethodInfo.ReflectedType;
				Attribute[][] array = new Attribute[num][];
				while (type != null && type != typeof(object))
				{
					MemberInfo method = type.GetMethod(name, bindingFlags);
					if (method != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(method);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						foreach (Attribute attribute in array3)
						{
							attributes.Add(attribute);
						}
					}
				}
			}
		}

		// Token: 0x06001690 RID: 5776 RVA: 0x00058580 File Offset: 0x00056780
		public override void RemoveEventHandler(object component, Delegate value)
		{
			this.FillMethods();
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					try
					{
						componentChangeService.OnComponentChanging(component, this);
					}
					catch (CheckoutException ex)
					{
						if (ex == CheckoutException.Canceled)
						{
							return;
						}
						throw ex;
					}
				}
				bool flag = false;
				if (site != null && site.DesignMode)
				{
					IDictionaryService dictionaryService = (IDictionaryService)site.GetService(typeof(IDictionaryService));
					if (dictionaryService != null)
					{
						Delegate @delegate = (Delegate)dictionaryService.GetValue(this);
						@delegate = Delegate.Remove(@delegate, value);
						dictionaryService.SetValue(this, @delegate);
						flag = true;
					}
				}
				if (!flag)
				{
					SecurityUtils.MethodInfoInvoke(this.removeMethod, component, new object[] { value });
				}
				if (componentChangeService != null)
				{
					componentChangeService.OnComponentChanged(component, this, null, value);
				}
			}
		}

		// Token: 0x040013A6 RID: 5030
		private Type type;

		// Token: 0x040013A7 RID: 5031
		private readonly Type componentClass;

		// Token: 0x040013A8 RID: 5032
		private MethodInfo addMethod;

		// Token: 0x040013A9 RID: 5033
		private MethodInfo removeMethod;

		// Token: 0x040013AA RID: 5034
		private EventInfo realEvent;

		// Token: 0x040013AB RID: 5035
		private bool filledMethods;
	}
}
