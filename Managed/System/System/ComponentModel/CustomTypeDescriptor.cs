using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides a simple default implementation of the <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> interface.</summary>
	// Token: 0x0200024F RID: 591
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class CustomTypeDescriptor : ICustomTypeDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CustomTypeDescriptor" /> class.</summary>
		// Token: 0x06001303 RID: 4867 RVA: 0x000020EB File Offset: 0x000002EB
		protected CustomTypeDescriptor()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CustomTypeDescriptor" /> class using a parent custom type descriptor.</summary>
		/// <param name="parent">The parent custom type descriptor.</param>
		// Token: 0x06001304 RID: 4868 RVA: 0x00050765 File Offset: 0x0004E965
		protected CustomTypeDescriptor(ICustomTypeDescriptor parent)
		{
			this._parent = parent;
		}

		/// <summary>Returns a collection of custom attributes for the type represented by this type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for the type. The default is <see cref="F:System.ComponentModel.AttributeCollection.Empty" />.</returns>
		// Token: 0x06001305 RID: 4869 RVA: 0x00050774 File Offset: 0x0004E974
		public virtual AttributeCollection GetAttributes()
		{
			if (this._parent != null)
			{
				return this._parent.GetAttributes();
			}
			return AttributeCollection.Empty;
		}

		/// <summary>Returns the fully qualified name of the class represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the fully qualified class name of the type this type descriptor is describing. The default is null.</returns>
		// Token: 0x06001306 RID: 4870 RVA: 0x0005078F File Offset: 0x0004E98F
		public virtual string GetClassName()
		{
			if (this._parent != null)
			{
				return this._parent.GetClassName();
			}
			return null;
		}

		/// <summary>Returns the name of the class represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the component instance this type descriptor is describing. The default is null.</returns>
		// Token: 0x06001307 RID: 4871 RVA: 0x000507A6 File Offset: 0x0004E9A6
		public virtual string GetComponentName()
		{
			if (this._parent != null)
			{
				return this._parent.GetComponentName();
			}
			return null;
		}

		/// <summary>Returns a type converter for the type represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the type represented by this type descriptor. The default is a newly created <see cref="T:System.ComponentModel.TypeConverter" />.</returns>
		// Token: 0x06001308 RID: 4872 RVA: 0x000507BD File Offset: 0x0004E9BD
		public virtual TypeConverter GetConverter()
		{
			if (this._parent != null)
			{
				return this._parent.GetConverter();
			}
			return new TypeConverter();
		}

		/// <summary>Returns the event descriptor for the default event of the object represented by this type descriptor.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> for the default event on the object represented by this type descriptor. The default is null.</returns>
		// Token: 0x06001309 RID: 4873 RVA: 0x000507D8 File Offset: 0x0004E9D8
		public virtual EventDescriptor GetDefaultEvent()
		{
			if (this._parent != null)
			{
				return this._parent.GetDefaultEvent();
			}
			return null;
		}

		/// <summary>Returns the property descriptor for the default property of the object represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the default property on the object represented by this type descriptor. The default is null.</returns>
		// Token: 0x0600130A RID: 4874 RVA: 0x000507EF File Offset: 0x0004E9EF
		public virtual PropertyDescriptor GetDefaultProperty()
		{
			if (this._parent != null)
			{
				return this._parent.GetDefaultProperty();
			}
			return null;
		}

		/// <summary>Returns an editor of the specified type that is to be associated with the class represented by this type descriptor.</summary>
		/// <returns>An editor of the given type that is to be associated with the class represented by this type descriptor. The default is null.</returns>
		/// <param name="editorBaseType">The base type of the editor to retrieve.</param>
		// Token: 0x0600130B RID: 4875 RVA: 0x00050806 File Offset: 0x0004EA06
		public virtual object GetEditor(Type editorBaseType)
		{
			if (this._parent != null)
			{
				return this._parent.GetEditor(editorBaseType);
			}
			return null;
		}

		/// <summary>Returns a collection of event descriptors for the object represented by this type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptors for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
		// Token: 0x0600130C RID: 4876 RVA: 0x0005081E File Offset: 0x0004EA1E
		public virtual EventDescriptorCollection GetEvents()
		{
			if (this._parent != null)
			{
				return this._parent.GetEvents();
			}
			return EventDescriptorCollection.Empty;
		}

		/// <summary>Returns a filtered collection of event descriptors for the object represented by this type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
		/// <param name="attributes">An array of attributes to use as a filter. This can be null.</param>
		// Token: 0x0600130D RID: 4877 RVA: 0x00050839 File Offset: 0x0004EA39
		public virtual EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			if (this._parent != null)
			{
				return this._parent.GetEvents(attributes);
			}
			return EventDescriptorCollection.Empty;
		}

		/// <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
		// Token: 0x0600130E RID: 4878 RVA: 0x00050855 File Offset: 0x0004EA55
		public virtual PropertyDescriptorCollection GetProperties()
		{
			if (this._parent != null)
			{
				return this._parent.GetProperties();
			}
			return PropertyDescriptorCollection.Empty;
		}

		/// <summary>Returns a filtered collection of property descriptors for the object represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
		/// <param name="attributes">An array of attributes to use as a filter. This can be null.</param>
		// Token: 0x0600130F RID: 4879 RVA: 0x00050870 File Offset: 0x0004EA70
		public virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			if (this._parent != null)
			{
				return this._parent.GetProperties(attributes);
			}
			return PropertyDescriptorCollection.Empty;
		}

		/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
		/// <returns>An <see cref="T:System.Object" /> that owns the given property specified by the type descriptor. The default is null.</returns>
		/// <param name="pd">The property descriptor for which to retrieve the owning object.</param>
		// Token: 0x06001310 RID: 4880 RVA: 0x0005088C File Offset: 0x0004EA8C
		public virtual object GetPropertyOwner(PropertyDescriptor pd)
		{
			if (this._parent != null)
			{
				return this._parent.GetPropertyOwner(pd);
			}
			return null;
		}

		// Token: 0x04001294 RID: 4756
		private ICustomTypeDescriptor _parent;
	}
}
