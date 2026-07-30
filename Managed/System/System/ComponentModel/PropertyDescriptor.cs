using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides an abstraction of a property on a class.</summary>
	// Token: 0x020002C1 RID: 705
	[ComVisible(true)]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class PropertyDescriptor : MemberDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the specified name and attributes.</summary>
		/// <param name="name">The name of the property. </param>
		/// <param name="attrs">An array of type <see cref="T:System.Attribute" /> that contains the property attributes. </param>
		// Token: 0x0600160F RID: 5647 RVA: 0x00052259 File Offset: 0x00050459
		protected PropertyDescriptor(string name, Attribute[] attrs)
			: base(name, attrs)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the name and attributes in the specified <see cref="T:System.ComponentModel.MemberDescriptor" />.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> that contains the name of the property and its attributes. </param>
		// Token: 0x06001610 RID: 5648 RVA: 0x00052263 File Offset: 0x00050463
		protected PropertyDescriptor(MemberDescriptor descr)
			: base(descr)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> class with the name in the specified <see cref="T:System.ComponentModel.MemberDescriptor" /> and the attributes in both the <see cref="T:System.ComponentModel.MemberDescriptor" /> and the <see cref="T:System.Attribute" /> array.</summary>
		/// <param name="descr">A <see cref="T:System.ComponentModel.MemberDescriptor" /> containing the name of the member and its attributes. </param>
		/// <param name="attrs">An <see cref="T:System.Attribute" /> array containing the attributes you want to associate with the property. </param>
		// Token: 0x06001611 RID: 5649 RVA: 0x0005226C File Offset: 0x0005046C
		protected PropertyDescriptor(MemberDescriptor descr, Attribute[] attrs)
			: base(descr, attrs)
		{
		}

		/// <summary>When overridden in a derived class, gets the type of the component this property is bound to.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of component this property is bound to. When the <see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)" /> or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)" /> methods are invoked, the object specified might be an instance of this type.</returns>
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001612 RID: 5650
		public abstract Type ComponentType { get; }

		/// <summary>Gets the type converter for this property.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> that is used to convert the <see cref="T:System.Type" /> of this property.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x00056BAC File Offset: 0x00054DAC
		public virtual TypeConverter Converter
		{
			get
			{
				AttributeCollection attributes = this.Attributes;
				if (this.converter == null)
				{
					TypeConverterAttribute typeConverterAttribute = (TypeConverterAttribute)attributes[typeof(TypeConverterAttribute)];
					if (typeConverterAttribute.ConverterTypeName != null && typeConverterAttribute.ConverterTypeName.Length > 0)
					{
						Type typeFromName = this.GetTypeFromName(typeConverterAttribute.ConverterTypeName);
						if (typeFromName != null && typeof(TypeConverter).IsAssignableFrom(typeFromName))
						{
							this.converter = (TypeConverter)this.CreateInstance(typeFromName);
						}
					}
					if (this.converter == null)
					{
						this.converter = TypeDescriptor.GetConverter(this.PropertyType);
					}
				}
				return this.converter;
			}
		}

		/// <summary>Gets a value indicating whether this property should be localized, as specified in the <see cref="T:System.ComponentModel.LocalizableAttribute" />.</summary>
		/// <returns>true if the member is marked with the <see cref="T:System.ComponentModel.LocalizableAttribute" /> set to true; otherwise, false.</returns>
		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001614 RID: 5652 RVA: 0x00056C4D File Offset: 0x00054E4D
		public virtual bool IsLocalizable
		{
			get
			{
				return LocalizableAttribute.Yes.Equals(this.Attributes[typeof(LocalizableAttribute)]);
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether this property is read-only.</summary>
		/// <returns>true if the property is read-only; otherwise, false.</returns>
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001615 RID: 5653
		public abstract bool IsReadOnly { get; }

		/// <summary>Gets a value indicating whether this property should be serialized, as specified in the <see cref="T:System.ComponentModel.DesignerSerializationVisibilityAttribute" />.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> enumeration values that specifies whether this property should be serialized.</returns>
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001616 RID: 5654 RVA: 0x00056C6E File Offset: 0x00054E6E
		public DesignerSerializationVisibility SerializationVisibility
		{
			get
			{
				return ((DesignerSerializationVisibilityAttribute)this.Attributes[typeof(DesignerSerializationVisibilityAttribute)]).Visibility;
			}
		}

		/// <summary>When overridden in a derived class, gets the type of the property.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the property.</returns>
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001617 RID: 5655
		public abstract Type PropertyType { get; }

		/// <summary>Enables other objects to be notified when this property changes.</summary>
		/// <param name="component">The component to add the handler for. </param>
		/// <param name="handler">The delegate to add as a listener. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="handler" /> is null.</exception>
		// Token: 0x06001618 RID: 5656 RVA: 0x00056C90 File Offset: 0x00054E90
		public virtual void AddValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (this.valueChangedHandlers == null)
			{
				this.valueChangedHandlers = new Hashtable();
			}
			EventHandler eventHandler = (EventHandler)this.valueChangedHandlers[component];
			this.valueChangedHandlers[component] = Delegate.Combine(eventHandler, handler);
		}

		/// <summary>When overridden in a derived class, returns whether resetting an object changes its value.</summary>
		/// <returns>true if resetting the component changes its value; otherwise, false.</returns>
		/// <param name="component">The component to test for reset capability. </param>
		// Token: 0x06001619 RID: 5657
		public abstract bool CanResetValue(object component);

		/// <summary>Compares this to another object to see if they are equivalent.</summary>
		/// <returns>true if the values are equivalent; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this <see cref="T:System.ComponentModel.PropertyDescriptor" />. </param>
		// Token: 0x0600161A RID: 5658 RVA: 0x00056CF4 File Offset: 0x00054EF4
		public override bool Equals(object obj)
		{
			try
			{
				if (obj == this)
				{
					return true;
				}
				if (obj == null)
				{
					return false;
				}
				PropertyDescriptor propertyDescriptor = obj as PropertyDescriptor;
				if (propertyDescriptor != null && propertyDescriptor.NameHashCode == this.NameHashCode && propertyDescriptor.PropertyType == this.PropertyType && propertyDescriptor.Name.Equals(this.Name))
				{
					return true;
				}
			}
			catch
			{
			}
			return false;
		}

		/// <summary>Creates an instance of the specified type.</summary>
		/// <returns>A new instance of the type.</returns>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type to create. </param>
		// Token: 0x0600161B RID: 5659 RVA: 0x00056D6C File Offset: 0x00054F6C
		protected object CreateInstance(Type type)
		{
			Type[] array = new Type[] { typeof(Type) };
			if (type.GetConstructor(array) != null)
			{
				return TypeDescriptor.CreateInstance(null, type, array, new object[] { this.PropertyType });
			}
			return TypeDescriptor.CreateInstance(null, type, null, null);
		}

		/// <summary>Adds the attributes of the <see cref="T:System.ComponentModel.PropertyDescriptor" /> to the specified list of attributes in the parent class.</summary>
		/// <param name="attributeList">An <see cref="T:System.Collections.IList" /> that lists the attributes in the parent class. Initially, this is empty.</param>
		// Token: 0x0600161C RID: 5660 RVA: 0x00056DBD File Offset: 0x00054FBD
		protected override void FillAttributes(IList attributeList)
		{
			this.converter = null;
			this.editors = null;
			this.editorTypes = null;
			this.editorCount = 0;
			base.FillAttributes(attributeList);
		}

		/// <summary>Returns the default <see cref="T:System.ComponentModel.PropertyDescriptorCollection" />.</summary>
		/// <returns>A collection of property descriptor.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600161D RID: 5661 RVA: 0x00056DE2 File Offset: 0x00054FE2
		public PropertyDescriptorCollection GetChildProperties()
		{
			return this.GetChildProperties(null, null);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> using a specified array of attributes as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes.</returns>
		/// <param name="filter">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		// Token: 0x0600161E RID: 5662 RVA: 0x00056DEC File Offset: 0x00054FEC
		public PropertyDescriptorCollection GetChildProperties(Attribute[] filter)
		{
			return this.GetChildProperties(null, filter);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> for a given object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties for the specified component.</returns>
		/// <param name="instance">A component to get the properties for. </param>
		// Token: 0x0600161F RID: 5663 RVA: 0x00056DF6 File Offset: 0x00054FF6
		public PropertyDescriptorCollection GetChildProperties(object instance)
		{
			return this.GetChildProperties(instance, null);
		}

		/// <summary>Returns a <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> for a given object using a specified array of attributes as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes for the specified component.</returns>
		/// <param name="instance">A component to get the properties for. </param>
		/// <param name="filter">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		// Token: 0x06001620 RID: 5664 RVA: 0x00056E00 File Offset: 0x00055000
		public virtual PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
		{
			if (instance == null)
			{
				return TypeDescriptor.GetProperties(this.PropertyType, filter);
			}
			return TypeDescriptor.GetProperties(instance, filter);
		}

		/// <summary>Gets an editor of the specified type.</summary>
		/// <returns>An instance of the requested editor type, or null if an editor cannot be found.</returns>
		/// <param name="editorBaseType">The base type of editor, which is used to differentiate between multiple editors that a property supports. </param>
		// Token: 0x06001621 RID: 5665 RVA: 0x00056E1C File Offset: 0x0005501C
		public virtual object GetEditor(Type editorBaseType)
		{
			object obj = null;
			AttributeCollection attributes = this.Attributes;
			if (this.editorTypes != null)
			{
				for (int i = 0; i < this.editorCount; i++)
				{
					if (this.editorTypes[i] == editorBaseType)
					{
						return this.editors[i];
					}
				}
			}
			if (obj == null)
			{
				for (int j = 0; j < attributes.Count; j++)
				{
					EditorAttribute editorAttribute = attributes[j] as EditorAttribute;
					if (editorAttribute != null)
					{
						Type typeFromName = this.GetTypeFromName(editorAttribute.EditorBaseTypeName);
						if (editorBaseType == typeFromName)
						{
							Type typeFromName2 = this.GetTypeFromName(editorAttribute.EditorTypeName);
							if (typeFromName2 != null)
							{
								obj = this.CreateInstance(typeFromName2);
								break;
							}
						}
					}
				}
				if (obj == null)
				{
					obj = TypeDescriptor.GetEditor(this.PropertyType, editorBaseType);
				}
				if (this.editorTypes == null)
				{
					this.editorTypes = new Type[5];
					this.editors = new object[5];
				}
				if (this.editorCount >= this.editorTypes.Length)
				{
					Type[] array = new Type[this.editorTypes.Length * 2];
					object[] array2 = new object[this.editors.Length * 2];
					Array.Copy(this.editorTypes, array, this.editorTypes.Length);
					Array.Copy(this.editors, array2, this.editors.Length);
					this.editorTypes = array;
					this.editors = array2;
				}
				this.editorTypes[this.editorCount] = editorBaseType;
				object[] array3 = this.editors;
				int num = this.editorCount;
				this.editorCount = num + 1;
				array3[num] = obj;
			}
			return obj;
		}

		/// <summary>Returns the hash code for this object.</summary>
		/// <returns>The hash code for this object.</returns>
		// Token: 0x06001622 RID: 5666 RVA: 0x00056F91 File Offset: 0x00055191
		public override int GetHashCode()
		{
			return this.NameHashCode ^ this.PropertyType.GetHashCode();
		}

		/// <summary>This method returns the object that should be used during invocation of members.</summary>
		/// <returns>The <see cref="T:System.Object" /> that should be used during invocation of members.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the invocation target.</param>
		/// <param name="instance">The potential invocation target.</param>
		// Token: 0x06001623 RID: 5667 RVA: 0x00056FA8 File Offset: 0x000551A8
		protected override object GetInvocationTarget(Type type, object instance)
		{
			object obj = base.GetInvocationTarget(type, instance);
			ICustomTypeDescriptor customTypeDescriptor = obj as ICustomTypeDescriptor;
			if (customTypeDescriptor != null)
			{
				obj = customTypeDescriptor.GetPropertyOwner(this);
			}
			return obj;
		}

		/// <summary>Returns a type using its name.</summary>
		/// <returns>A <see cref="T:System.Type" /> that matches the given type name, or null if a match cannot be found.</returns>
		/// <param name="typeName">The assembly-qualified name of the type to retrieve. </param>
		// Token: 0x06001624 RID: 5668 RVA: 0x00056FD4 File Offset: 0x000551D4
		protected Type GetTypeFromName(string typeName)
		{
			if (typeName == null || typeName.Length == 0)
			{
				return null;
			}
			Type type = Type.GetType(typeName);
			Type type2 = null;
			if (this.ComponentType != null && (type == null || this.ComponentType.Assembly.FullName.Equals(type.Assembly.FullName)))
			{
				int num = typeName.IndexOf(',');
				if (num != -1)
				{
					typeName = typeName.Substring(0, num);
				}
				type2 = this.ComponentType.Assembly.GetType(typeName);
			}
			return type2 ?? type;
		}

		/// <summary>When overridden in a derived class, gets the current value of the property on a component.</summary>
		/// <returns>The value of a property for a given component.</returns>
		/// <param name="component">The component with the property for which to retrieve the value. </param>
		// Token: 0x06001625 RID: 5669
		public abstract object GetValue(object component);

		/// <summary>Raises the ValueChanged event that you implemented.</summary>
		/// <param name="component">The object that raises the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001626 RID: 5670 RVA: 0x00057060 File Offset: 0x00055260
		protected virtual void OnValueChanged(object component, EventArgs e)
		{
			if (component != null && this.valueChangedHandlers != null)
			{
				EventHandler eventHandler = (EventHandler)this.valueChangedHandlers[component];
				if (eventHandler != null)
				{
					eventHandler(component, e);
				}
			}
		}

		/// <summary>Enables other objects to be notified when this property changes.</summary>
		/// <param name="component">The component to remove the handler for. </param>
		/// <param name="handler">The delegate to remove as a listener. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="handler" /> is null.</exception>
		// Token: 0x06001627 RID: 5671 RVA: 0x00057098 File Offset: 0x00055298
		public virtual void RemoveValueChanged(object component, EventHandler handler)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			if (this.valueChangedHandlers != null)
			{
				EventHandler eventHandler = (EventHandler)this.valueChangedHandlers[component];
				eventHandler = (EventHandler)Delegate.Remove(eventHandler, handler);
				if (eventHandler != null)
				{
					this.valueChangedHandlers[component] = eventHandler;
					return;
				}
				this.valueChangedHandlers.Remove(component);
			}
		}

		/// <summary>Retrieves the current set of ValueChanged event handlers for a specific component</summary>
		/// <returns>A combined multicast event handler, or null if no event handlers are currently assigned to <paramref name="component" />.</returns>
		/// <param name="component">The component for which to retrieve event handlers.</param>
		// Token: 0x06001628 RID: 5672 RVA: 0x00057105 File Offset: 0x00055305
		protected internal EventHandler GetValueChangedHandler(object component)
		{
			if (component != null && this.valueChangedHandlers != null)
			{
				return (EventHandler)this.valueChangedHandlers[component];
			}
			return null;
		}

		/// <summary>When overridden in a derived class, resets the value for this property of the component to the default value.</summary>
		/// <param name="component">The component with the property value that is to be reset to the default value. </param>
		// Token: 0x06001629 RID: 5673
		public abstract void ResetValue(object component);

		/// <summary>When overridden in a derived class, sets the value of the component to a different value.</summary>
		/// <param name="component">The component with the property value that is to be set. </param>
		/// <param name="value">The new value. </param>
		// Token: 0x0600162A RID: 5674
		public abstract void SetValue(object component, object value);

		/// <summary>When overridden in a derived class, determines a value indicating whether the value of this property needs to be persisted.</summary>
		/// <returns>true if the property should be persisted; otherwise, false.</returns>
		/// <param name="component">The component with the property to be examined for persistence. </param>
		// Token: 0x0600162B RID: 5675
		public abstract bool ShouldSerializeValue(object component);

		/// <summary>Gets a value indicating whether value change notifications for this property may originate from outside the property descriptor.</summary>
		/// <returns>true if value change notifications may originate from outside the property descriptor; otherwise, false.</returns>
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x0600162C RID: 5676 RVA: 0x00004240 File Offset: 0x00002440
		public virtual bool SupportsChangeEvents
		{
			get
			{
				return false;
			}
		}

		// Token: 0x04001388 RID: 5000
		private TypeConverter converter;

		// Token: 0x04001389 RID: 5001
		private Hashtable valueChangedHandlers;

		// Token: 0x0400138A RID: 5002
		private object[] editors;

		// Token: 0x0400138B RID: 5003
		private Type[] editorTypes;

		// Token: 0x0400138C RID: 5004
		private int editorCount;
	}
}
