using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides supplemental metadata to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
	// Token: 0x020002DE RID: 734
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public abstract class TypeDescriptionProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> class.</summary>
		// Token: 0x0600176E RID: 5998 RVA: 0x000020EB File Offset: 0x000002EB
		protected TypeDescriptionProvider()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> class using a parent type description provider.</summary>
		/// <param name="parent">The parent type description provider.</param>
		// Token: 0x0600176F RID: 5999 RVA: 0x0005C543 File Offset: 0x0005A743
		protected TypeDescriptionProvider(TypeDescriptionProvider parent)
		{
			this._parent = parent;
		}

		/// <summary>Creates an object that can substitute for another data type.</summary>
		/// <returns>The substitute <see cref="T:System.Object" />.</returns>
		/// <param name="provider">An optional service provider.</param>
		/// <param name="objectType">The type of object to create. This parameter is never null.</param>
		/// <param name="argTypes">An optional array of types that represent the parameter types to be passed to the object's constructor. This array can be null or of zero length.</param>
		/// <param name="args">An optional array of parameter values to pass to the object's constructor.</param>
		// Token: 0x06001770 RID: 6000 RVA: 0x0005C552 File Offset: 0x0005A752
		public virtual object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			if (this._parent != null)
			{
				return this._parent.CreateInstance(provider, objectType, argTypes, args);
			}
			if (objectType == null)
			{
				throw new ArgumentNullException("objectType");
			}
			return SecurityUtils.SecureCreateInstance(objectType, args);
		}

		/// <summary>Gets a per-object cache, accessed as an <see cref="T:System.Collections.IDictionary" /> of key/value pairs.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> if the provided object supports caching; otherwise, null.</returns>
		/// <param name="instance">The object for which to get the cache.</param>
		// Token: 0x06001771 RID: 6001 RVA: 0x0005C589 File Offset: 0x0005A789
		public virtual IDictionary GetCache(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetCache(instance);
			}
			return null;
		}

		/// <summary>Gets an extended custom type descriptor for the given object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide extended metadata for the object.</returns>
		/// <param name="instance">The object for which to get the extended type descriptor.</param>
		// Token: 0x06001772 RID: 6002 RVA: 0x0005C5A1 File Offset: 0x0005A7A1
		public virtual ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetExtendedTypeDescriptor(instance);
			}
			if (this._emptyDescriptor == null)
			{
				this._emptyDescriptor = new TypeDescriptionProvider.EmptyCustomTypeDescriptor();
			}
			return this._emptyDescriptor;
		}

		/// <summary>Gets the extender providers for the specified object.</summary>
		/// <returns>An array of extender providers for <paramref name="instance" />.</returns>
		/// <param name="instance">The object to get extender providers for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x06001773 RID: 6003 RVA: 0x0005C5D1 File Offset: 0x0005A7D1
		protected internal virtual IExtenderProvider[] GetExtenderProviders(object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetExtenderProviders(instance);
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return new IExtenderProvider[0];
		}

		/// <summary>Gets the name of the specified component, or null if the component has no name.</summary>
		/// <returns>The name of the specified component.</returns>
		/// <param name="component">The specified component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		// Token: 0x06001774 RID: 6004 RVA: 0x0005C5FC File Offset: 0x0005A7FC
		public virtual string GetFullComponentName(object component)
		{
			if (this._parent != null)
			{
				return this._parent.GetFullComponentName(component);
			}
			return this.GetTypeDescriptor(component).GetComponentName();
		}

		/// <summary>Performs normal reflection against a type.</summary>
		/// <returns>The type of reflection for this <paramref name="objectType" />.</returns>
		/// <param name="objectType">The type of object for which to retrieve the <see cref="T:System.Reflection.IReflect" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="objectType" /> is null.</exception>
		// Token: 0x06001775 RID: 6005 RVA: 0x0005C61F File Offset: 0x0005A81F
		public Type GetReflectionType(Type objectType)
		{
			return this.GetReflectionType(objectType, null);
		}

		/// <summary>Performs normal reflection against the given object.</summary>
		/// <returns>The type of reflection for this <paramref name="instance" />.</returns>
		/// <param name="instance">An instance of the type (should not be null).</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x06001776 RID: 6006 RVA: 0x0005C629 File Offset: 0x0005A829
		public Type GetReflectionType(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return this.GetReflectionType(instance.GetType(), instance);
		}

		/// <summary>Performs normal reflection against the given object with the given type.</summary>
		/// <returns>The type of reflection for this <paramref name="objectType" />.</returns>
		/// <param name="objectType">The type of object for which to retrieve the <see cref="T:System.Reflection.IReflect" />.</param>
		/// <param name="instance">An instance of the type. Can be null.</param>
		// Token: 0x06001777 RID: 6007 RVA: 0x0005C646 File Offset: 0x0005A846
		public virtual Type GetReflectionType(Type objectType, object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetReflectionType(objectType, instance);
			}
			return objectType;
		}

		/// <summary>Converts a reflection type into a runtime type.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the runtime equivalent of <paramref name="reflectionType" />.</returns>
		/// <param name="reflectionType">The type to convert to its runtime equivalent.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="reflectionType" /> is null.</exception>
		// Token: 0x06001778 RID: 6008 RVA: 0x0005C660 File Offset: 0x0005A860
		public virtual Type GetRuntimeType(Type reflectionType)
		{
			if (this._parent != null)
			{
				return this._parent.GetRuntimeType(reflectionType);
			}
			if (reflectionType == null)
			{
				throw new ArgumentNullException("reflectionType");
			}
			if (reflectionType.GetType().Assembly == typeof(object).Assembly)
			{
				return reflectionType;
			}
			return reflectionType.UnderlyingSystemType;
		}

		/// <summary>Gets a custom type descriptor for the given type.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		/// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
		// Token: 0x06001779 RID: 6009 RVA: 0x0005C6BF File Offset: 0x0005A8BF
		public ICustomTypeDescriptor GetTypeDescriptor(Type objectType)
		{
			return this.GetTypeDescriptor(objectType, null);
		}

		/// <summary>Gets a custom type descriptor for the given object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		/// <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x0600177A RID: 6010 RVA: 0x0005C6C9 File Offset: 0x0005A8C9
		public ICustomTypeDescriptor GetTypeDescriptor(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return this.GetTypeDescriptor(instance.GetType(), instance);
		}

		/// <summary>Gets a custom type descriptor for the given type and object.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> that can provide metadata for the type.</returns>
		/// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
		/// <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor" />.</param>
		// Token: 0x0600177B RID: 6011 RVA: 0x0005C6E6 File Offset: 0x0005A8E6
		public virtual ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
		{
			if (this._parent != null)
			{
				return this._parent.GetTypeDescriptor(objectType, instance);
			}
			if (this._emptyDescriptor == null)
			{
				this._emptyDescriptor = new TypeDescriptionProvider.EmptyCustomTypeDescriptor();
			}
			return this._emptyDescriptor;
		}

		/// <summary>Gets a value that indicates whether the specified type is compatible with the type description and its chain of type description providers. </summary>
		/// <returns>true if <paramref name="type" /> is compatible with the type description and its chain of type description providers; otherwise, false. </returns>
		/// <param name="type">The type to test for compatibility.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x0600177C RID: 6012 RVA: 0x0005C717 File Offset: 0x0005A917
		public virtual bool IsSupportedType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return this._parent == null || this._parent.IsSupportedType(type);
		}

		// Token: 0x040013F9 RID: 5113
		private TypeDescriptionProvider _parent;

		// Token: 0x040013FA RID: 5114
		private TypeDescriptionProvider.EmptyCustomTypeDescriptor _emptyDescriptor;

		// Token: 0x020002DF RID: 735
		private sealed class EmptyCustomTypeDescriptor : CustomTypeDescriptor
		{
		}
	}
}
