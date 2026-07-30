using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020002CA RID: 714
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal sealed class ReflectPropertyDescriptor : PropertyDescriptor
	{
		// Token: 0x06001691 RID: 5777 RVA: 0x0005865C File Offset: 0x0005685C
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, Attribute[] attributes)
			: base(name, attributes)
		{
			try
			{
				if (type == null)
				{
					throw new ArgumentException(global::SR.GetString("Invalid type for the {0} property.", new object[] { name }));
				}
				if (componentClass == null)
				{
					throw new ArgumentException(global::SR.GetString("Null is not a valid value for {0}.", new object[] { "componentClass" }));
				}
				this.type = type;
				this.componentClass = componentClass;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x000586E0 File Offset: 0x000568E0
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, PropertyInfo propInfo, MethodInfo getMethod, MethodInfo setMethod, Attribute[] attrs)
			: this(componentClass, name, type, attrs)
		{
			this.propInfo = propInfo;
			this.getMethod = getMethod;
			this.setMethod = setMethod;
			if (getMethod != null && propInfo != null && setMethod == null)
			{
				this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetOnDemand] = true;
				return;
			}
			this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetQueried] = true;
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0005875D File Offset: 0x0005695D
		public ReflectPropertyDescriptor(Type componentClass, string name, Type type, Type receiverType, MethodInfo getMethod, MethodInfo setMethod, Attribute[] attrs)
			: this(componentClass, name, type, attrs)
		{
			this.receiverType = receiverType;
			this.getMethod = getMethod;
			this.setMethod = setMethod;
			this.state[ReflectPropertyDescriptor.BitGetQueried | ReflectPropertyDescriptor.BitSetQueried] = true;
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0005879C File Offset: 0x0005699C
		public ReflectPropertyDescriptor(Type componentClass, PropertyDescriptor oldReflectPropertyDescriptor, Attribute[] attributes)
			: base(oldReflectPropertyDescriptor, attributes)
		{
			this.componentClass = componentClass;
			this.type = oldReflectPropertyDescriptor.PropertyType;
			if (componentClass == null)
			{
				throw new ArgumentException(global::SR.GetString("Null is not a valid value for {0}.", new object[] { "componentClass" }));
			}
			ReflectPropertyDescriptor reflectPropertyDescriptor = oldReflectPropertyDescriptor as ReflectPropertyDescriptor;
			if (reflectPropertyDescriptor != null)
			{
				if (reflectPropertyDescriptor.ComponentType == componentClass)
				{
					this.propInfo = reflectPropertyDescriptor.propInfo;
					this.getMethod = reflectPropertyDescriptor.getMethod;
					this.setMethod = reflectPropertyDescriptor.setMethod;
					this.shouldSerializeMethod = reflectPropertyDescriptor.shouldSerializeMethod;
					this.resetMethod = reflectPropertyDescriptor.resetMethod;
					this.defaultValue = reflectPropertyDescriptor.defaultValue;
					this.ambientValue = reflectPropertyDescriptor.ambientValue;
					this.state = reflectPropertyDescriptor.state;
				}
				if (attributes != null)
				{
					foreach (Attribute attribute in attributes)
					{
						DefaultValueAttribute defaultValueAttribute = attribute as DefaultValueAttribute;
						if (defaultValueAttribute != null)
						{
							this.defaultValue = defaultValueAttribute.Value;
							if (this.defaultValue != null && this.PropertyType.IsEnum && this.PropertyType.GetEnumUnderlyingType() == this.defaultValue.GetType())
							{
								this.defaultValue = Enum.ToObject(this.PropertyType, this.defaultValue);
							}
							this.state[ReflectPropertyDescriptor.BitDefaultValueQueried] = true;
						}
						else
						{
							AmbientValueAttribute ambientValueAttribute = attribute as AmbientValueAttribute;
							if (ambientValueAttribute != null)
							{
								this.ambientValue = ambientValueAttribute.Value;
								this.state[ReflectPropertyDescriptor.BitAmbientValueQueried] = true;
							}
						}
					}
				}
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001695 RID: 5781 RVA: 0x00058928 File Offset: 0x00056B28
		private object AmbientValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitAmbientValueQueried])
				{
					this.state[ReflectPropertyDescriptor.BitAmbientValueQueried] = true;
					Attribute attribute = this.Attributes[typeof(AmbientValueAttribute)];
					if (attribute != null)
					{
						this.ambientValue = ((AmbientValueAttribute)attribute).Value;
					}
					else
					{
						this.ambientValue = ReflectPropertyDescriptor.noValue;
					}
				}
				return this.ambientValue;
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001696 RID: 5782 RVA: 0x00058998 File Offset: 0x00056B98
		private EventDescriptor ChangedEventValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitChangedQueried])
				{
					this.state[ReflectPropertyDescriptor.BitChangedQueried] = true;
					this.realChangedEvent = TypeDescriptor.GetEvents(this.ComponentType)[string.Format(CultureInfo.InvariantCulture, "{0}Changed", this.Name)];
				}
				return this.realChangedEvent;
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001697 RID: 5783 RVA: 0x000589FC File Offset: 0x00056BFC
		// (set) Token: 0x06001698 RID: 5784 RVA: 0x00058A68 File Offset: 0x00056C68
		private EventDescriptor IPropChangedEventValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitIPropChangedQueried])
				{
					this.state[ReflectPropertyDescriptor.BitIPropChangedQueried] = true;
					if (typeof(INotifyPropertyChanged).IsAssignableFrom(this.ComponentType))
					{
						this.realIPropChangedEvent = TypeDescriptor.GetEvents(typeof(INotifyPropertyChanged))["PropertyChanged"];
					}
				}
				return this.realIPropChangedEvent;
			}
			set
			{
				this.realIPropChangedEvent = value;
				this.state[ReflectPropertyDescriptor.BitIPropChangedQueried] = true;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x00058A82 File Offset: 0x00056C82
		public override Type ComponentType
		{
			get
			{
				return this.componentClass;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600169A RID: 5786 RVA: 0x00058A8C File Offset: 0x00056C8C
		private object DefaultValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitDefaultValueQueried])
				{
					this.state[ReflectPropertyDescriptor.BitDefaultValueQueried] = true;
					Attribute attribute = this.Attributes[typeof(DefaultValueAttribute)];
					if (attribute != null)
					{
						this.defaultValue = ((DefaultValueAttribute)attribute).Value;
						if (this.defaultValue != null && this.PropertyType.IsEnum && this.PropertyType.GetEnumUnderlyingType() == this.defaultValue.GetType())
						{
							this.defaultValue = Enum.ToObject(this.PropertyType, this.defaultValue);
						}
					}
					else
					{
						this.defaultValue = ReflectPropertyDescriptor.noValue;
					}
				}
				return this.defaultValue;
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600169B RID: 5787 RVA: 0x00058B48 File Offset: 0x00056D48
		private MethodInfo GetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitGetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitGetQueried] = true;
					if (this.receiverType == null)
					{
						if (this.propInfo == null)
						{
							BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;
							this.propInfo = this.componentClass.GetProperty(this.Name, bindingFlags, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
						}
						if (this.propInfo != null)
						{
							this.getMethod = this.propInfo.GetGetMethod(true);
						}
						if (this.getMethod == null)
						{
							throw new InvalidOperationException(global::SR.GetString("Accessor methods for the {0} property are missing.", new object[] { this.componentClass.FullName + "." + this.Name }));
						}
					}
					else
					{
						this.getMethod = MemberDescriptor.FindMethod(this.componentClass, "Get" + this.Name, new Type[] { this.receiverType }, this.type);
						if (this.getMethod == null)
						{
							throw new ArgumentException(global::SR.GetString("Accessor methods for the {0} property are missing.", new object[] { this.Name }));
						}
					}
				}
				return this.getMethod;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600169C RID: 5788 RVA: 0x00058C99 File Offset: 0x00056E99
		private bool IsExtender
		{
			get
			{
				return this.receiverType != null;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600169D RID: 5789 RVA: 0x00058CA7 File Offset: 0x00056EA7
		public override bool IsReadOnly
		{
			get
			{
				return this.SetMethodValue == null || ((ReadOnlyAttribute)this.Attributes[typeof(ReadOnlyAttribute)]).IsReadOnly;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600169E RID: 5790 RVA: 0x00058CD8 File Offset: 0x00056ED8
		public override Type PropertyType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600169F RID: 5791 RVA: 0x00058CE0 File Offset: 0x00056EE0
		private MethodInfo ResetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitResetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitResetQueried] = true;
					Type[] array;
					if (this.receiverType == null)
					{
						array = ReflectPropertyDescriptor.argsNone;
					}
					else
					{
						array = new Type[] { this.receiverType };
					}
					this.resetMethod = MemberDescriptor.FindMethod(this.componentClass, "Reset" + this.Name, array, typeof(void), false);
				}
				return this.resetMethod;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060016A0 RID: 5792 RVA: 0x00058D6C File Offset: 0x00056F6C
		private MethodInfo SetMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitSetQueried] && this.state[ReflectPropertyDescriptor.BitSetOnDemand])
				{
					this.state[ReflectPropertyDescriptor.BitSetQueried] = true;
					BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public;
					string name = this.propInfo.Name;
					if (this.setMethod == null)
					{
						Type type = this.ComponentType.BaseType;
						while (type != null && type != typeof(object) && !(type == null))
						{
							PropertyInfo property = type.GetProperty(name, bindingFlags, null, this.PropertyType, new Type[0], null);
							if (property != null)
							{
								this.setMethod = property.GetSetMethod();
								if (this.setMethod != null)
								{
									break;
								}
							}
							type = type.BaseType;
						}
					}
				}
				if (!this.state[ReflectPropertyDescriptor.BitSetQueried])
				{
					this.state[ReflectPropertyDescriptor.BitSetQueried] = true;
					if (this.receiverType == null)
					{
						if (this.propInfo == null)
						{
							BindingFlags bindingFlags2 = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;
							this.propInfo = this.componentClass.GetProperty(this.Name, bindingFlags2, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
						}
						if (this.propInfo != null)
						{
							this.setMethod = this.propInfo.GetSetMethod(true);
						}
					}
					else
					{
						this.setMethod = MemberDescriptor.FindMethod(this.componentClass, "Set" + this.Name, new Type[] { this.receiverType, this.type }, typeof(void));
					}
				}
				return this.setMethod;
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x00058F24 File Offset: 0x00057124
		private MethodInfo ShouldSerializeMethodValue
		{
			get
			{
				if (!this.state[ReflectPropertyDescriptor.BitShouldSerializeQueried])
				{
					this.state[ReflectPropertyDescriptor.BitShouldSerializeQueried] = true;
					Type[] array;
					if (this.receiverType == null)
					{
						array = ReflectPropertyDescriptor.argsNone;
					}
					else
					{
						array = new Type[] { this.receiverType };
					}
					this.shouldSerializeMethod = MemberDescriptor.FindMethod(this.componentClass, "ShouldSerialize" + this.Name, array, typeof(bool), false);
				}
				return this.shouldSerializeMethod;
			}
		}

		// Token: 0x060016A2 RID: 5794 RVA: 0x00058FB0 File Offset: 0x000571B0
		public override void AddValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			EventDescriptor changedEventValue = this.ChangedEventValue;
			if (changedEventValue != null && changedEventValue.EventType.IsInstanceOfType(handler))
			{
				changedEventValue.AddEventHandler(component, handler);
				return;
			}
			if (base.GetValueChangedHandler(component) == null)
			{
				EventDescriptor ipropChangedEventValue = this.IPropChangedEventValue;
				if (ipropChangedEventValue != null)
				{
					ipropChangedEventValue.AddEventHandler(component, new PropertyChangedEventHandler(this.OnINotifyPropertyChanged));
				}
			}
			base.AddValueChanged(component, handler);
		}

		// Token: 0x060016A3 RID: 5795 RVA: 0x00059028 File Offset: 0x00057228
		internal bool ExtenderCanResetValue(IExtenderProvider provider, object component)
		{
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				return !object.Equals(this.ExtenderGetValue(provider, component), this.defaultValue);
			}
			if (this.ResetMethodValue != null)
			{
				MethodInfo shouldSerializeMethodValue = this.ShouldSerializeMethodValue;
				if (shouldSerializeMethodValue != null)
				{
					try
					{
						provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
						return (bool)shouldSerializeMethodValue.Invoke(provider, new object[] { component });
					}
					catch
					{
					}
					return true;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x000590BC File Offset: 0x000572BC
		internal Type ExtenderGetReceiverType()
		{
			return this.receiverType;
		}

		// Token: 0x060016A5 RID: 5797 RVA: 0x000590C4 File Offset: 0x000572C4
		internal Type ExtenderGetType(IExtenderProvider provider)
		{
			return this.PropertyType;
		}

		// Token: 0x060016A6 RID: 5798 RVA: 0x000590CC File Offset: 0x000572CC
		internal object ExtenderGetValue(IExtenderProvider provider, object component)
		{
			if (provider != null)
			{
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				return this.GetMethodValue.Invoke(provider, new object[] { component });
			}
			return null;
		}

		// Token: 0x060016A7 RID: 5799 RVA: 0x00059100 File Offset: 0x00057300
		internal void ExtenderResetValue(IExtenderProvider provider, object component, PropertyDescriptor notifyDesc)
		{
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				this.ExtenderSetValue(provider, component, this.DefaultValue, notifyDesc);
				return;
			}
			if (this.AmbientValue != ReflectPropertyDescriptor.noValue)
			{
				this.ExtenderSetValue(provider, component, this.AmbientValue, notifyDesc);
				return;
			}
			if (this.ResetMethodValue != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					obj = this.ExtenderGetValue(provider, component);
					try
					{
						componentChangeService.OnComponentChanging(component, notifyDesc);
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
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				if (this.ResetMethodValue != null)
				{
					this.ResetMethodValue.Invoke(provider, new object[] { component });
					if (componentChangeService != null)
					{
						object obj2 = this.ExtenderGetValue(provider, component);
						componentChangeService.OnComponentChanged(component, notifyDesc, obj, obj2);
					}
				}
			}
		}

		// Token: 0x060016A8 RID: 5800 RVA: 0x00059204 File Offset: 0x00057404
		internal void ExtenderSetValue(IExtenderProvider provider, object component, object value, PropertyDescriptor notifyDesc)
		{
			if (provider != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					obj = this.ExtenderGetValue(provider, component);
					try
					{
						componentChangeService.OnComponentChanging(component, notifyDesc);
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
				provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
				if (this.SetMethodValue != null)
				{
					this.SetMethodValue.Invoke(provider, new object[] { component, value });
					if (componentChangeService != null)
					{
						componentChangeService.OnComponentChanged(component, notifyDesc, obj, value);
					}
				}
			}
		}

		// Token: 0x060016A9 RID: 5801 RVA: 0x000592BC File Offset: 0x000574BC
		internal bool ExtenderShouldSerializeValue(IExtenderProvider provider, object component)
		{
			provider = (IExtenderProvider)this.GetInvocationTarget(this.componentClass, provider);
			if (this.IsReadOnly)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(provider, new object[] { component });
					}
					catch
					{
					}
				}
				return this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			if (this.DefaultValue == ReflectPropertyDescriptor.noValue)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(provider, new object[] { component });
					}
					catch
					{
					}
				}
				return true;
			}
			return !object.Equals(this.DefaultValue, this.ExtenderGetValue(provider, component));
		}

		// Token: 0x060016AA RID: 5802 RVA: 0x00059398 File Offset: 0x00057598
		public override bool CanResetValue(object component)
		{
			if (this.IsExtender || this.IsReadOnly)
			{
				return false;
			}
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				return !object.Equals(this.GetValue(component), this.DefaultValue);
			}
			if (this.ResetMethodValue != null)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					component = this.GetInvocationTarget(this.componentClass, component);
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
					return true;
				}
				return true;
			}
			return this.AmbientValue != ReflectPropertyDescriptor.noValue && this.ShouldSerializeValue(component);
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x00059448 File Offset: 0x00057648
		protected override void FillAttributes(IList attributes)
		{
			foreach (object obj in TypeDescriptor.GetAttributes(this.PropertyType))
			{
				Attribute attribute = (Attribute)obj;
				attributes.Add(attribute);
			}
			BindingFlags bindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			Type type = this.componentClass;
			int num = 0;
			while (type != null && type != typeof(object))
			{
				num++;
				type = type.BaseType;
			}
			if (num > 0)
			{
				type = this.componentClass;
				Attribute[][] array = new Attribute[num][];
				while (type != null && type != typeof(object))
				{
					MemberInfo memberInfo;
					if (this.IsExtender)
					{
						memberInfo = type.GetMethod("Get" + this.Name, bindingFlags, null, new Type[] { this.receiverType }, null);
					}
					else
					{
						memberInfo = type.GetProperty(this.Name, bindingFlags, null, this.PropertyType, new Type[0], new ParameterModifier[0]);
					}
					if (memberInfo != null)
					{
						array[--num] = ReflectTypeDescriptionProvider.ReflectGetAttributes(memberInfo);
					}
					type = type.BaseType;
				}
				foreach (Attribute[] array3 in array)
				{
					if (array3 != null)
					{
						Attribute[] array4 = array3;
						for (int j = 0; j < array4.Length; j++)
						{
							AttributeProviderAttribute attributeProviderAttribute = array4[j] as AttributeProviderAttribute;
							if (attributeProviderAttribute != null)
							{
								Type type2 = Type.GetType(attributeProviderAttribute.TypeName);
								if (type2 != null)
								{
									Attribute[] array5 = null;
									if (!string.IsNullOrEmpty(attributeProviderAttribute.PropertyName))
									{
										MemberInfo[] member = type2.GetMember(attributeProviderAttribute.PropertyName);
										if (member.Length != 0 && member[0] != null)
										{
											array5 = ReflectTypeDescriptionProvider.ReflectGetAttributes(member[0]);
										}
									}
									else
									{
										array5 = ReflectTypeDescriptionProvider.ReflectGetAttributes(type2);
									}
									if (array5 != null)
									{
										foreach (Attribute attribute2 in array5)
										{
											attributes.Add(attribute2);
										}
									}
								}
							}
						}
					}
				}
				foreach (Attribute[] array7 in array)
				{
					if (array7 != null)
					{
						foreach (Attribute attribute3 in array7)
						{
							attributes.Add(attribute3);
						}
					}
				}
			}
			base.FillAttributes(attributes);
			if (this.SetMethodValue == null)
			{
				attributes.Add(ReadOnlyAttribute.Yes);
			}
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x000596E0 File Offset: 0x000578E0
		public override object GetValue(object component)
		{
			if (this.IsExtender)
			{
				return null;
			}
			if (component != null)
			{
				component = this.GetInvocationTarget(this.componentClass, component);
				try
				{
					return SecurityUtils.MethodInfoInvoke(this.GetMethodValue, component, null);
				}
				catch (Exception innerException)
				{
					string text = null;
					IComponent component2 = component as IComponent;
					if (component2 != null)
					{
						ISite site = component2.Site;
						if (site != null && site.Name != null)
						{
							text = site.Name;
						}
					}
					if (text == null)
					{
						text = component.GetType().FullName;
					}
					if (innerException is TargetInvocationException)
					{
						innerException = innerException.InnerException;
					}
					string text2 = innerException.Message;
					if (text2 == null)
					{
						text2 = innerException.GetType().Name;
					}
					throw new TargetInvocationException(global::SR.GetString("Property accessor '{0}' on object '{1}' threw the following exception:'{2}'", new object[] { this.Name, text, text2 }), innerException);
				}
			}
			return null;
		}

		// Token: 0x060016AD RID: 5805 RVA: 0x000597BC File Offset: 0x000579BC
		internal void OnINotifyPropertyChanged(object component, PropertyChangedEventArgs e)
		{
			if (string.IsNullOrEmpty(e.PropertyName) || string.Compare(e.PropertyName, this.Name, true, CultureInfo.InvariantCulture) == 0)
			{
				this.OnValueChanged(component, e);
			}
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x000597EC File Offset: 0x000579EC
		protected override void OnValueChanged(object component, EventArgs e)
		{
			if (this.state[ReflectPropertyDescriptor.BitChangedQueried] && this.realChangedEvent == null)
			{
				base.OnValueChanged(component, e);
			}
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x00059810 File Offset: 0x00057A10
		public override void RemoveValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			EventDescriptor changedEventValue = this.ChangedEventValue;
			if (changedEventValue != null && changedEventValue.EventType.IsInstanceOfType(handler))
			{
				changedEventValue.RemoveEventHandler(component, handler);
				return;
			}
			base.RemoveValueChanged(component, handler);
			if (base.GetValueChangedHandler(component) == null)
			{
				EventDescriptor ipropChangedEventValue = this.IPropChangedEventValue;
				if (ipropChangedEventValue != null)
				{
					ipropChangedEventValue.RemoveEventHandler(component, new PropertyChangedEventHandler(this.OnINotifyPropertyChanged));
				}
			}
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00059888 File Offset: 0x00057A88
		public override void ResetValue(object component)
		{
			object invocationTarget = this.GetInvocationTarget(this.componentClass, component);
			if (this.DefaultValue != ReflectPropertyDescriptor.noValue)
			{
				this.SetValue(component, this.DefaultValue);
				return;
			}
			if (this.AmbientValue != ReflectPropertyDescriptor.noValue)
			{
				this.SetValue(component, this.AmbientValue);
				return;
			}
			if (this.ResetMethodValue != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				if (site != null)
				{
					componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				}
				if (componentChangeService != null)
				{
					obj = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
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
				if (this.ResetMethodValue != null)
				{
					SecurityUtils.MethodInfoInvoke(this.ResetMethodValue, invocationTarget, null);
					if (componentChangeService != null)
					{
						object obj2 = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
						componentChangeService.OnComponentChanged(component, this, obj, obj2);
					}
				}
			}
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x00059984 File Offset: 0x00057B84
		public override void SetValue(object component, object value)
		{
			if (component != null)
			{
				ISite site = MemberDescriptor.GetSite(component);
				IComponentChangeService componentChangeService = null;
				object obj = null;
				object invocationTarget = this.GetInvocationTarget(this.componentClass, component);
				if (!this.IsReadOnly)
				{
					if (site != null)
					{
						componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
					}
					if (componentChangeService != null)
					{
						obj = SecurityUtils.MethodInfoInvoke(this.GetMethodValue, invocationTarget, null);
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
					try
					{
						SecurityUtils.MethodInfoInvoke(this.SetMethodValue, invocationTarget, new object[] { value });
						this.OnValueChanged(invocationTarget, EventArgs.Empty);
					}
					catch (Exception ex2)
					{
						value = obj;
						if (ex2 is TargetInvocationException && ex2.InnerException != null)
						{
							throw ex2.InnerException;
						}
						throw ex2;
					}
					finally
					{
						if (componentChangeService != null)
						{
							componentChangeService.OnComponentChanged(component, this, obj, value);
						}
					}
				}
			}
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x00059A80 File Offset: 0x00057C80
		public override bool ShouldSerializeValue(object component)
		{
			component = this.GetInvocationTarget(this.componentClass, component);
			if (this.IsReadOnly)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
				}
				return this.Attributes.Contains(DesignerSerializationVisibilityAttribute.Content);
			}
			if (this.DefaultValue == ReflectPropertyDescriptor.noValue)
			{
				if (this.ShouldSerializeMethodValue != null)
				{
					try
					{
						return (bool)this.ShouldSerializeMethodValue.Invoke(component, null);
					}
					catch
					{
					}
				}
				return true;
			}
			return !object.Equals(this.DefaultValue, this.GetValue(component));
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060016B3 RID: 5811 RVA: 0x00059B44 File Offset: 0x00057D44
		public override bool SupportsChangeEvents
		{
			get
			{
				return this.IPropChangedEventValue != null || this.ChangedEventValue != null;
			}
		}

		// Token: 0x040013AC RID: 5036
		private static readonly Type[] argsNone = new Type[0];

		// Token: 0x040013AD RID: 5037
		private static readonly object noValue = new object();

		// Token: 0x040013AE RID: 5038
		private static TraceSwitch PropDescCreateSwitch = new TraceSwitch("PropDescCreate", "ReflectPropertyDescriptor: Dump errors when creating property info");

		// Token: 0x040013AF RID: 5039
		private static TraceSwitch PropDescUsageSwitch = new TraceSwitch("PropDescUsage", "ReflectPropertyDescriptor: Debug propertydescriptor usage");

		// Token: 0x040013B0 RID: 5040
		private static readonly int BitDefaultValueQueried = BitVector32.CreateMask();

		// Token: 0x040013B1 RID: 5041
		private static readonly int BitGetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitDefaultValueQueried);

		// Token: 0x040013B2 RID: 5042
		private static readonly int BitSetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitGetQueried);

		// Token: 0x040013B3 RID: 5043
		private static readonly int BitShouldSerializeQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitSetQueried);

		// Token: 0x040013B4 RID: 5044
		private static readonly int BitResetQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitShouldSerializeQueried);

		// Token: 0x040013B5 RID: 5045
		private static readonly int BitChangedQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitResetQueried);

		// Token: 0x040013B6 RID: 5046
		private static readonly int BitIPropChangedQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitChangedQueried);

		// Token: 0x040013B7 RID: 5047
		private static readonly int BitReadOnlyChecked = BitVector32.CreateMask(ReflectPropertyDescriptor.BitIPropChangedQueried);

		// Token: 0x040013B8 RID: 5048
		private static readonly int BitAmbientValueQueried = BitVector32.CreateMask(ReflectPropertyDescriptor.BitReadOnlyChecked);

		// Token: 0x040013B9 RID: 5049
		private static readonly int BitSetOnDemand = BitVector32.CreateMask(ReflectPropertyDescriptor.BitAmbientValueQueried);

		// Token: 0x040013BA RID: 5050
		private BitVector32 state;

		// Token: 0x040013BB RID: 5051
		private Type componentClass;

		// Token: 0x040013BC RID: 5052
		private Type type;

		// Token: 0x040013BD RID: 5053
		private object defaultValue;

		// Token: 0x040013BE RID: 5054
		private object ambientValue;

		// Token: 0x040013BF RID: 5055
		private PropertyInfo propInfo;

		// Token: 0x040013C0 RID: 5056
		private MethodInfo getMethod;

		// Token: 0x040013C1 RID: 5057
		private MethodInfo setMethod;

		// Token: 0x040013C2 RID: 5058
		private MethodInfo shouldSerializeMethod;

		// Token: 0x040013C3 RID: 5059
		private MethodInfo resetMethod;

		// Token: 0x040013C4 RID: 5060
		private EventDescriptor realChangedEvent;

		// Token: 0x040013C5 RID: 5061
		private EventDescriptor realIPropChangedEvent;

		// Token: 0x040013C6 RID: 5062
		private Type receiverType;
	}
}
