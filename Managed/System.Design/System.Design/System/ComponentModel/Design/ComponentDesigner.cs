using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	/// <summary>Extends the design mode behavior of a component.</summary>
	// Token: 0x020000FD RID: 253
	public class ComponentDesigner : ITreeDesigner, IDesigner, IDisposable, IDesignerFilter, IComponentInitializer
	{
		/// <summary>Gets the collection of components associated with the component managed by the designer.</summary>
		/// <returns>The components that are associated with the component managed by the designer.</returns>
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0000B8FC File Offset: 0x00009AFC
		public virtual ICollection AssociatedComponents
		{
			get
			{
				return new IComponent[0];
			}
		}

		/// <summary>Gets the component this designer is designing.</summary>
		/// <returns>The component managed by the designer.</returns>
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x0000B904 File Offset: 0x00009B04
		public IComponent Component
		{
			get
			{
				return this._component;
			}
		}

		/// <summary>Gets the design-time verbs supported by the component that is associated with the designer.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.DesignerVerbCollection" /> of <see cref="T:System.ComponentModel.Design.DesignerVerb" /> objects, or null if no designer verbs are available. This default implementation always returns null.</returns>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0000B90C File Offset: 0x00009B0C
		public virtual DesignerVerbCollection Verbs
		{
			get
			{
				if (this._verbs == null)
				{
					this._verbs = new DesignerVerbCollection();
				}
				return this._verbs;
			}
		}

		/// <summary>Gets an attribute that indicates the type of inheritance of the associated component.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.InheritanceAttribute" /> for the associated component.</returns>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0000B928 File Offset: 0x00009B28
		protected virtual InheritanceAttribute InheritanceAttribute
		{
			get
			{
				IInheritanceService inheritanceService = (IInheritanceService)this.GetService(typeof(IInheritanceService));
				if (inheritanceService != null)
				{
					return inheritanceService.GetInheritanceAttribute(this._component);
				}
				return InheritanceAttribute.Default;
			}
		}

		/// <summary>Gets a value indicating whether this component is inherited.</summary>
		/// <returns>true if the component is inherited; otherwise, false.</returns>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0000B960 File Offset: 0x00009B60
		protected bool Inherited
		{
			get
			{
				return !this.InheritanceAttribute.Equals(InheritanceAttribute.NotInherited);
			}
		}

		/// <summary>Gets a collection of property values that override user settings.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.ComponentDesigner.ShadowPropertyCollection" /> that indicates the shadow properties of the design document.</returns>
		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0000B975 File Offset: 0x00009B75
		protected ComponentDesigner.ShadowPropertyCollection ShadowProperties
		{
			get
			{
				if (this._shadowPropertyCollection == null)
				{
					this._shadowPropertyCollection = new ComponentDesigner.ShadowPropertyCollection(this._component);
				}
				return this._shadowPropertyCollection;
			}
		}

		/// <summary>Gets the design-time action lists supported by the component associated with the designer.</summary>
		/// <returns>The design-time action lists supported by the component associated with the designer.</returns>
		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0000B996 File Offset: 0x00009B96
		public virtual DesignerActionListCollection ActionLists
		{
			get
			{
				if (this._designerActionList == null)
				{
					this._designerActionList = new DesignerActionListCollection();
				}
				return this._designerActionList;
			}
		}

		/// <summary>Gets the parent component for this designer.</summary>
		/// <returns>The parent component for this designer, or null if this designer is the root component.</returns>
		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0000B9B4 File Offset: 0x00009BB4
		protected virtual IComponent ParentComponent
		{
			get
			{
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					IComponent rootComponent = designerHost.RootComponent;
					if (rootComponent != this._component)
					{
						return rootComponent;
					}
				}
				return null;
			}
		}

		/// <summary>Initializes a newly created component.</summary>
		/// <param name="defaultValues">A name/value dictionary of default values to apply to properties. May be null if no default values are specified.</param>
		// Token: 0x06000738 RID: 1848 RVA: 0x0000B9ED File Offset: 0x00009BED
		public virtual void InitializeNewComponent(IDictionary defaultValues)
		{
			this.OnSetComponentDefaults();
		}

		/// <summary>Reinitializes an existing component.</summary>
		/// <param name="defaultValues">A name/value dictionary of default values to apply to properties. May be null if no default values are specified.</param>
		// Token: 0x06000739 RID: 1849 RVA: 0x0000B9F5 File Offset: 0x00009BF5
		public virtual void InitializeExistingComponent(IDictionary defaultValues)
		{
			this.InitializeNonDefault();
		}

		/// <summary>Prepares the designer to view, edit, and design the specified component.</summary>
		/// <param name="component">The component for this designer. </param>
		// Token: 0x0600073A RID: 1850 RVA: 0x0000B9FD File Offset: 0x00009BFD
		public virtual void Initialize(IComponent component)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			this._component = component;
		}

		/// <summary>Initializes the settings for an imported component that is already initialized to settings other than the defaults.</summary>
		// Token: 0x0600073B RID: 1851 RVA: 0x00002432 File Offset: 0x00000632
		[Obsolete("This method has been deprecated. Use InitializeExistingComponent instead.")]
		public virtual void InitializeNonDefault()
		{
		}

		/// <summary>Creates a method signature in the source code file for the default event on the component and navigates the user's cursor to that location.</summary>
		/// <exception cref="T:System.ComponentModel.Design.CheckoutException">An attempt to check out a file that is checked into a source code management program failed.</exception>
		// Token: 0x0600073C RID: 1852 RVA: 0x0000BA14 File Offset: 0x00009C14
		public virtual void DoDefaultAction()
		{
			IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
			DesignerTransaction designerTransaction = null;
			if (designerHost != null)
			{
				designerTransaction = designerHost.CreateTransaction("ComponentDesigner_AddEvent");
			}
			IEventBindingService eventBindingService = this.GetService(typeof(IEventBindingService)) as IEventBindingService;
			EventDescriptor eventDescriptor = null;
			if (eventBindingService != null)
			{
				ISelectionService selectionService = this.GetService(typeof(ISelectionService)) as ISelectionService;
				try
				{
					if (selectionService != null)
					{
						foreach (object obj in selectionService.GetSelectedComponents())
						{
							IComponent component = (IComponent)obj;
							EventDescriptor defaultEvent = TypeDescriptor.GetDefaultEvent(component);
							if (defaultEvent != null)
							{
								PropertyDescriptor eventProperty = eventBindingService.GetEventProperty(defaultEvent);
								if (eventProperty != null && !eventProperty.IsReadOnly)
								{
									string text = eventProperty.GetValue(component) as string;
									bool flag = true;
									if (text != null || text != string.Empty)
									{
										using (IEnumerator enumerator2 = eventBindingService.GetCompatibleMethods(defaultEvent).GetEnumerator())
										{
											while (enumerator2.MoveNext())
											{
												if ((string)enumerator2.Current == text)
												{
													flag = false;
													break;
												}
											}
										}
									}
									if (flag)
									{
										if (text == null)
										{
											text = eventBindingService.CreateUniqueMethodName(component, defaultEvent);
										}
										eventProperty.SetValue(component, text);
									}
									if (component == this._component)
									{
										eventDescriptor = defaultEvent;
									}
								}
							}
						}
					}
				}
				catch
				{
					if (designerTransaction != null)
					{
						designerTransaction.Cancel();
						designerTransaction = null;
					}
				}
				finally
				{
					if (designerTransaction != null)
					{
						designerTransaction.Commit();
					}
				}
				if (eventDescriptor != null)
				{
					eventBindingService.ShowCode(this._component, eventDescriptor);
				}
			}
		}

		/// <summary>Sets the default properties for the component.</summary>
		// Token: 0x0600073D RID: 1853 RVA: 0x0000BC20 File Offset: 0x00009E20
		[Obsolete("This method has been deprecated. Use InitializeNewComponent instead.")]
		public virtual void OnSetComponentDefaults()
		{
			if (this._component != null && this._component.Site != null)
			{
				PropertyDescriptor defaultProperty = TypeDescriptor.GetDefaultProperty(this._component);
				if (defaultProperty != null && defaultProperty.PropertyType.Equals(typeof(string)))
				{
					string text = (string)defaultProperty.GetValue(this._component);
					if (text != null && text.Length != 0)
					{
						defaultProperty.SetValue(this._component, this._component.Site.Name);
					}
				}
			}
		}

		/// <summary>Gets the <see cref="T:System.ComponentModel.InheritanceAttribute" /> of the specified <see cref="T:System.ComponentModel.Design.ComponentDesigner" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.InheritanceAttribute" /> of the specified designer.</returns>
		/// <param name="toInvoke">The <see cref="T:System.ComponentModel.Design.ComponentDesigner" /> whose inheritance attribute to retrieve. </param>
		// Token: 0x0600073E RID: 1854 RVA: 0x0000BCA1 File Offset: 0x00009EA1
		protected InheritanceAttribute InvokeGetInheritanceAttribute(ComponentDesigner toInvoke)
		{
			return toInvoke.InheritanceAttribute;
		}

		/// <summary>Allows a designer to change or remove items from the set of attributes that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="attributes">The attributes for the class of the component. </param>
		// Token: 0x0600073F RID: 1855 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void PostFilterAttributes(IDictionary attributes)
		{
		}

		/// <summary>Allows a designer to change or remove items from the set of events that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="events">The events for the class of the component. </param>
		// Token: 0x06000740 RID: 1856 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void PostFilterEvents(IDictionary events)
		{
		}

		/// <summary>Allows a designer to change or remove items from the set of properties that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="properties">The properties for the class of the component. </param>
		// Token: 0x06000741 RID: 1857 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void PostFilterProperties(IDictionary properties)
		{
		}

		/// <summary>Allows a designer to add to the set of attributes that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="attributes">The attributes for the class of the component. </param>
		// Token: 0x06000742 RID: 1858 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void PreFilterAttributes(IDictionary attributes)
		{
		}

		/// <summary>Allows a designer to add to the set of events that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="events">The events for the class of the component. </param>
		// Token: 0x06000743 RID: 1859 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void PreFilterEvents(IDictionary events)
		{
		}

		/// <summary>Allows a designer to add to the set of properties that it exposes through a <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
		/// <param name="properties">The properties for the class of the component. </param>
		// Token: 0x06000744 RID: 1860 RVA: 0x00002432 File Offset: 0x00000632
		protected virtual void PreFilterProperties(IDictionary properties)
		{
		}

		/// <summary>Notifies the <see cref="T:System.ComponentModel.Design.IComponentChangeService" /> that this component has been changed.</summary>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that indicates the member that has been changed. </param>
		/// <param name="oldValue">The old value of the member. </param>
		/// <param name="newValue">The new value of the member. </param>
		// Token: 0x06000745 RID: 1861 RVA: 0x0000BCAC File Offset: 0x00009EAC
		protected void RaiseComponentChanged(MemberDescriptor member, object oldValue, object newValue)
		{
			IComponentChangeService componentChangeService = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if (componentChangeService != null)
			{
				componentChangeService.OnComponentChanged(this._component, member, oldValue, newValue);
			}
		}

		/// <summary>Notifies the <see cref="T:System.ComponentModel.Design.IComponentChangeService" /> that this component is about to be changed.</summary>
		/// <param name="member">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that indicates the member that is about to be changed. </param>
		// Token: 0x06000746 RID: 1862 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		protected void RaiseComponentChanging(MemberDescriptor member)
		{
			IComponentChangeService componentChangeService = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
			if (componentChangeService != null)
			{
				componentChangeService.OnComponentChanging(this._component, member);
			}
		}

		/// <summary>For a description of this member, see the <see cref="M:System.ComponentModel.Design.IDesignerFilter.PostFilterAttributes(System.Collections.IDictionary)" /> method.</summary>
		/// <param name="attributes">The <see cref="T:System.Attribute" /> objects for the class of the component. The keys in the dictionary of attributes are the <see cref="P:System.Attribute.TypeId" /> values of the attributes.</param>
		// Token: 0x06000747 RID: 1863 RVA: 0x0000BD17 File Offset: 0x00009F17
		void IDesignerFilter.PostFilterAttributes(IDictionary attributes)
		{
			this.PostFilterAttributes(attributes);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.ComponentModel.Design.IDesignerFilter.PostFilterEvents(System.Collections.IDictionary)" /> method.</summary>
		/// <param name="events">The <see cref="T:System.ComponentModel.EventDescriptor" /> objects that represent the events of the class of the component. The keys in the dictionary of events are event names.</param>
		// Token: 0x06000748 RID: 1864 RVA: 0x0000BD20 File Offset: 0x00009F20
		void IDesignerFilter.PostFilterEvents(IDictionary events)
		{
			this.PostFilterEvents(events);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.ComponentModel.Design.IDesignerFilter.PostFilterProperties(System.Collections.IDictionary)" /> method.</summary>
		/// <param name="properties">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that represent the properties of the class of the component. The keys in the dictionary of properties are property names.</param>
		// Token: 0x06000749 RID: 1865 RVA: 0x0000BD29 File Offset: 0x00009F29
		void IDesignerFilter.PostFilterProperties(IDictionary properties)
		{
			this.PostFilterProperties(properties);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.ComponentModel.Design.IDesignerFilter.PreFilterAttributes(System.Collections.IDictionary)" /> method.</summary>
		/// <param name="attributes">The <see cref="T:System.Attribute" /> objects for the class of the component. The keys in the dictionary of attributes are the <see cref="P:System.Attribute.TypeId" /> values of the attributes.</param>
		// Token: 0x0600074A RID: 1866 RVA: 0x0000BD32 File Offset: 0x00009F32
		void IDesignerFilter.PreFilterAttributes(IDictionary attributes)
		{
			this.PreFilterAttributes(attributes);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.ComponentModel.Design.IDesignerFilter.PreFilterEvents(System.Collections.IDictionary)" /> method.</summary>
		/// <param name="events">The <see cref="T:System.ComponentModel.EventDescriptor" /> objects that represent the events of the class of the component. The keys in the dictionary of events are event names.</param>
		// Token: 0x0600074B RID: 1867 RVA: 0x0000BD3B File Offset: 0x00009F3B
		void IDesignerFilter.PreFilterEvents(IDictionary events)
		{
			this.PreFilterEvents(events);
		}

		/// <summary>For a description of this member, see the <see cref="M:System.ComponentModel.Design.IDesignerFilter.PreFilterProperties(System.Collections.IDictionary)" /> method.</summary>
		/// <param name="properties">The <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects that represent the properties of the class of the component. The keys in the dictionary of properties are property names.</param>
		// Token: 0x0600074C RID: 1868 RVA: 0x0000BD44 File Offset: 0x00009F44
		void IDesignerFilter.PreFilterProperties(IDictionary properties)
		{
			this.PreFilterProperties(properties);
		}

		/// <summary>For a description of this member, see the <see cref="P:System.ComponentModel.Design.ITreeDesigner.Children" /> property.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the collection of <see cref="T:System.ComponentModel.Design.IDesigner" /> designers contained in the current parent designer. </returns>
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x0000BD50 File Offset: 0x00009F50
		ICollection ITreeDesigner.Children
		{
			get
			{
				ICollection associatedComponents = this.AssociatedComponents;
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					ArrayList arrayList = new ArrayList();
					foreach (object obj in associatedComponents)
					{
						IComponent component = (IComponent)obj;
						IDesigner designer = designerHost.GetDesigner(component);
						if (designer != null)
						{
							arrayList.Add(designer);
						}
					}
					IDesigner[] array = new IDesigner[arrayList.Count];
					arrayList.CopyTo(array);
					return array;
				}
				return new IDesigner[0];
			}
		}

		/// <summary>For a description of this member, see the <see cref="P:System.ComponentModel.Design.ITreeDesigner.Parent" /> property.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" /> representing the parent designer, or null if there is no parent.</returns>
		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0000BE00 File Offset: 0x0000A000
		IDesigner ITreeDesigner.Parent
		{
			get
			{
				IDesignerHost designerHost = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null && this.ParentComponent != null)
				{
					return designerHost.GetDesigner(this.ParentComponent);
				}
				return null;
			}
		}

		/// <summary>Attempts to retrieve the specified type of service from the design mode site of the designer's component.</summary>
		/// <returns>An object implementing the requested service, or null if the service cannot be resolved.</returns>
		/// <param name="serviceType">The type of service to request. </param>
		// Token: 0x0600074F RID: 1871 RVA: 0x0000BE3C File Offset: 0x0000A03C
		protected virtual object GetService(Type serviceType)
		{
			if (this._component != null && this._component.Site != null)
			{
				return this._component.Site.GetService(serviceType);
			}
			return null;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.ComponentModel.Design.ComponentDesigner" />.</summary>
		// Token: 0x06000750 RID: 1872 RVA: 0x0000BE66 File Offset: 0x0000A066
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.ComponentModel.Design.ComponentDesigner" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000751 RID: 1873 RVA: 0x0000BE75 File Offset: 0x0000A075
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._component = null;
			}
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0000BE84 File Offset: 0x0000A084
		~ComponentDesigner()
		{
			this.Dispose(false);
		}

		// Token: 0x04000181 RID: 385
		private IComponent _component;

		// Token: 0x04000182 RID: 386
		private DesignerVerbCollection _verbs;

		// Token: 0x04000183 RID: 387
		private ComponentDesigner.ShadowPropertyCollection _shadowPropertyCollection;

		// Token: 0x04000184 RID: 388
		private DesignerActionListCollection _designerActionList;

		/// <summary>Represents a collection of shadow properties that should override inherited default or assigned values for specific properties. This class cannot be inherited.</summary>
		// Token: 0x020000FE RID: 254
		protected sealed class ShadowPropertyCollection
		{
			// Token: 0x06000753 RID: 1875 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
			internal ShadowPropertyCollection(IComponent component)
			{
				this._component = component;
			}

			/// <summary>Gets or sets the object at the specified index.</summary>
			/// <returns>The value of the specified property, if it exists in the collection. Otherwise, the value is retrieved from the current value of the nonshadowed property.</returns>
			/// <param name="propertyName">The name of the property to access in the collection. </param>
			// Token: 0x170001B4 RID: 436
			public object this[string propertyName]
			{
				get
				{
					if (propertyName == null)
					{
						throw new ArgumentNullException("propertyName");
					}
					if (this._properties != null && this._properties.ContainsKey(propertyName))
					{
						return this._properties[propertyName];
					}
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this._component.GetType())[propertyName];
					if (propertyDescriptor != null)
					{
						return propertyDescriptor.GetValue(this._component);
					}
					throw new Exception("Propery not found!");
				}
				set
				{
					if (this._properties == null)
					{
						this._properties = new Hashtable();
					}
					this._properties[propertyName] = value;
				}
			}

			/// <summary>Indicates whether a property matching the specified name exists in the collection.</summary>
			/// <returns>true if the property exists in the collection; otherwise, false.</returns>
			/// <param name="propertyName">The name of the property to check for in the collection. </param>
			// Token: 0x06000756 RID: 1878 RVA: 0x0000BF55 File Offset: 0x0000A155
			public bool Contains(string propertyName)
			{
				return this._properties != null && this._properties.ContainsKey(propertyName);
			}

			// Token: 0x04000185 RID: 389
			private Hashtable _properties;

			// Token: 0x04000186 RID: 390
			private IComponent _component;
		}
	}
}
