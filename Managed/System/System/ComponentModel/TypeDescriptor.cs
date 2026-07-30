using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Provides information about the characteristics for a component, such as its attributes, properties, and events. This class cannot be inherited.</summary>
	// Token: 0x020002E1 RID: 737
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public sealed class TypeDescriptor
	{
		// Token: 0x06001781 RID: 6017 RVA: 0x000020EB File Offset: 0x000002EB
		private TypeDescriptor()
		{
		}

		/// <summary>Gets or sets the provider for the Component Object Model (COM) type information for the target component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.IComNativeDescriptorHandler" /> instance representing the COM type information provider.</returns>
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001782 RID: 6018 RVA: 0x0005C798 File Offset: 0x0005A998
		// (set) Token: 0x06001783 RID: 6019 RVA: 0x0005C7D8 File Offset: 0x0005A9D8
		[Obsolete("This property has been deprecated.  Use a type description provider to supply type information for COM types instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public static IComNativeDescriptorHandler ComNativeDescriptorHandler
		{
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			get
			{
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode = TypeDescriptor.NodeFor(TypeDescriptor.ComObjectType);
				TypeDescriptor.ComNativeDescriptionProvider comNativeDescriptionProvider;
				do
				{
					comNativeDescriptionProvider = typeDescriptionNode.Provider as TypeDescriptor.ComNativeDescriptionProvider;
					typeDescriptionNode = typeDescriptionNode.Next;
				}
				while (typeDescriptionNode != null && comNativeDescriptionProvider == null);
				if (comNativeDescriptionProvider != null)
				{
					return comNativeDescriptionProvider.Handler;
				}
				return null;
			}
			[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
			set
			{
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode = TypeDescriptor.NodeFor(TypeDescriptor.ComObjectType);
				while (typeDescriptionNode != null && !(typeDescriptionNode.Provider is TypeDescriptor.ComNativeDescriptionProvider))
				{
					typeDescriptionNode = typeDescriptionNode.Next;
				}
				if (typeDescriptionNode == null)
				{
					TypeDescriptor.AddProvider(new TypeDescriptor.ComNativeDescriptionProvider(value), TypeDescriptor.ComObjectType);
					return;
				}
				((TypeDescriptor.ComNativeDescriptionProvider)typeDescriptionNode.Provider).Handler = value;
			}
		}

		/// <summary>Gets the type of the Component Object Model (COM) object represented by the target component.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the COM object represented by this component, or null for non-COM objects.</returns>
		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001784 RID: 6020 RVA: 0x0005C82E File Offset: 0x0005AA2E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Type ComObjectType
		{
			get
			{
				return typeof(TypeDescriptor.TypeDescriptorComObject);
			}
		}

		/// <summary>Gets a type that represents a type description provider for all interface types. </summary>
		/// <returns>A <see cref="T:System.Type" /> that represents a custom type description provider for all interface types. </returns>
		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x06001785 RID: 6021 RVA: 0x0005C83A File Offset: 0x0005AA3A
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Type InterfaceType
		{
			get
			{
				return typeof(TypeDescriptor.TypeDescriptorInterface);
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x06001786 RID: 6022 RVA: 0x0005C846 File Offset: 0x0005AA46
		internal static int MetadataVersion
		{
			get
			{
				return TypeDescriptor._metadataVersion;
			}
		}

		/// <summary>Occurs when the cache for a component is cleared.</summary>
		// Token: 0x1400002A RID: 42
		// (add) Token: 0x06001787 RID: 6023 RVA: 0x0005C850 File Offset: 0x0005AA50
		// (remove) Token: 0x06001788 RID: 6024 RVA: 0x0005C884 File Offset: 0x0005AA84
		public static event RefreshEventHandler Refreshed;

		/// <summary>Adds class-level attributes to the target component type.</summary>
		/// <returns>The newly created <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that was used to add the specified attributes.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects to add to the component's class.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters is null.</exception>
		// Token: 0x06001789 RID: 6025 RVA: 0x0005C8B7 File Offset: 0x0005AAB7
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static TypeDescriptionProvider AddAttributes(Type type, params Attribute[] attributes)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (attributes == null)
			{
				throw new ArgumentNullException("attributes");
			}
			TypeDescriptor.AttributeProvider attributeProvider = new TypeDescriptor.AttributeProvider(TypeDescriptor.GetProvider(type), attributes);
			TypeDescriptor.AddProvider(attributeProvider, type);
			return attributeProvider;
		}

		/// <summary>Adds class-level attributes to the target component instance.</summary>
		/// <returns>The newly created <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> that was used to add the specified attributes.</returns>
		/// <param name="instance">An instance of the target component.</param>
		/// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects to add to the component's class.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters is null.</exception>
		// Token: 0x0600178A RID: 6026 RVA: 0x0005C8EE File Offset: 0x0005AAEE
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static TypeDescriptionProvider AddAttributes(object instance, params Attribute[] attributes)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (attributes == null)
			{
				throw new ArgumentNullException("attributes");
			}
			TypeDescriptor.AttributeProvider attributeProvider = new TypeDescriptor.AttributeProvider(TypeDescriptor.GetProvider(instance), attributes);
			TypeDescriptor.AddProvider(attributeProvider, instance);
			return attributeProvider;
		}

		/// <summary>Adds an editor table for the given editor base type. </summary>
		/// <param name="editorBaseType">The editor base type to add the editor table for. If a table already exists for this type, this method will do nothing. </param>
		/// <param name="table">The <see cref="T:System.Collections.Hashtable" /> to add. </param>
		// Token: 0x0600178B RID: 6027 RVA: 0x0005C91F File Offset: 0x0005AB1F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void AddEditorTable(Type editorBaseType, Hashtable table)
		{
			ReflectTypeDescriptionProvider.AddEditorTable(editorBaseType, table);
		}

		/// <summary>Adds a type description provider for a component class.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to add.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x0600178C RID: 6028 RVA: 0x0005C928 File Offset: 0x0005AB28
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void AddProvider(TypeDescriptionProvider provider, Type type)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			WeakHashtable providerTable = TypeDescriptor._providerTable;
			lock (providerTable)
			{
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode = TypeDescriptor.NodeFor(type, true);
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode2 = new TypeDescriptor.TypeDescriptionNode(provider);
				typeDescriptionNode2.Next = typeDescriptionNode;
				TypeDescriptor._providerTable[type] = typeDescriptionNode2;
				TypeDescriptor._providerTypeTable.Clear();
			}
			TypeDescriptor.Refresh(type);
		}

		/// <summary>Adds a type description provider for a single instance of a component.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to add.</param>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x0600178D RID: 6029 RVA: 0x0005C9B8 File Offset: 0x0005ABB8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void AddProvider(TypeDescriptionProvider provider, object instance)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			WeakHashtable providerTable = TypeDescriptor._providerTable;
			bool flag2;
			lock (providerTable)
			{
				flag2 = TypeDescriptor._providerTable.ContainsKey(instance);
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode = TypeDescriptor.NodeFor(instance, true);
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode2 = new TypeDescriptor.TypeDescriptionNode(provider);
				typeDescriptionNode2.Next = typeDescriptionNode;
				TypeDescriptor._providerTable.SetWeak(instance, typeDescriptionNode2);
				TypeDescriptor._providerTypeTable.Clear();
			}
			if (flag2)
			{
				TypeDescriptor.Refresh(instance, false);
			}
		}

		/// <summary>Adds a type description provider for a component class.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to add.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x0600178E RID: 6030 RVA: 0x0005CA54 File Offset: 0x0005AC54
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void AddProviderTransparent(TypeDescriptionProvider provider, Type type)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			TypeDescriptor.AddProvider(provider, type);
		}

		/// <summary>Adds a type description provider for a single instance of a component.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to add.</param>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x0600178F RID: 6031 RVA: 0x0005CA7F File Offset: 0x0005AC7F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void AddProviderTransparent(TypeDescriptionProvider provider, object instance)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			TypeDescriptor.AddProvider(provider, instance);
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0005CAA4 File Offset: 0x0005ACA4
		private static void CheckDefaultProvider(Type type)
		{
			object obj;
			if (TypeDescriptor._defaultProviders == null)
			{
				obj = TypeDescriptor._internalSyncObject;
				lock (obj)
				{
					if (TypeDescriptor._defaultProviders == null)
					{
						TypeDescriptor._defaultProviders = new Hashtable();
					}
				}
			}
			if (TypeDescriptor._defaultProviders.ContainsKey(type))
			{
				return;
			}
			obj = TypeDescriptor._internalSyncObject;
			lock (obj)
			{
				if (TypeDescriptor._defaultProviders.ContainsKey(type))
				{
					return;
				}
				TypeDescriptor._defaultProviders[type] = null;
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(TypeDescriptionProviderAttribute), false);
			bool flag2 = false;
			for (int i = customAttributes.Length - 1; i >= 0; i--)
			{
				Type type2 = Type.GetType(((TypeDescriptionProviderAttribute)customAttributes[i]).TypeName);
				if (type2 != null && typeof(TypeDescriptionProvider).IsAssignableFrom(type2))
				{
					TypeDescriptor.AddProvider((TypeDescriptionProvider)Activator.CreateInstance(type2), type);
					flag2 = true;
				}
			}
			if (!flag2)
			{
				Type baseType = type.BaseType;
				if (baseType != null && baseType != type)
				{
					TypeDescriptor.CheckDefaultProvider(baseType);
				}
			}
		}

		/// <summary>Creates a primary-secondary association between two objects.</summary>
		/// <param name="primary">The primary <see cref="T:System.Object" />.</param>
		/// <param name="secondary">The secondary <see cref="T:System.Object" />.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="primary" /> is equal to <paramref name="secondary" />.</exception>
		// Token: 0x06001791 RID: 6033 RVA: 0x0005CBEC File Offset: 0x0005ADEC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void CreateAssociation(object primary, object secondary)
		{
			if (primary == null)
			{
				throw new ArgumentNullException("primary");
			}
			if (secondary == null)
			{
				throw new ArgumentNullException("secondary");
			}
			if (primary == secondary)
			{
				throw new ArgumentException(global::SR.GetString("Cannot create an association when the primary and secondary objects are the same."));
			}
			if (TypeDescriptor._associationTable == null)
			{
				object internalSyncObject = TypeDescriptor._internalSyncObject;
				lock (internalSyncObject)
				{
					if (TypeDescriptor._associationTable == null)
					{
						TypeDescriptor._associationTable = new WeakHashtable();
					}
				}
			}
			IList list = (IList)TypeDescriptor._associationTable[primary];
			if (list == null)
			{
				WeakHashtable associationTable = TypeDescriptor._associationTable;
				lock (associationTable)
				{
					list = (IList)TypeDescriptor._associationTable[primary];
					if (list == null)
					{
						list = new ArrayList(4);
						TypeDescriptor._associationTable.SetWeak(primary, list);
					}
					goto IL_0112;
				}
			}
			for (int i = list.Count - 1; i >= 0; i--)
			{
				WeakReference weakReference = (WeakReference)list[i];
				if (weakReference.IsAlive && weakReference.Target == secondary)
				{
					throw new ArgumentException(global::SR.GetString("The primary and secondary objects are already associated with each other."));
				}
			}
			IL_0112:
			IList list2 = list;
			lock (list2)
			{
				list.Add(new WeakReference(secondary));
			}
		}

		/// <summary>Creates an instance of the designer associated with the specified component and of the specified type of designer.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.Design.IDesigner" /> that is an instance of the designer for the component, or null if no designer can be found.</returns>
		/// <param name="component">An <see cref="T:System.ComponentModel.IComponent" /> that specifies the component to associate with the designer. </param>
		/// <param name="designerBaseType">A <see cref="T:System.Type" /> that represents the type of designer to create. </param>
		// Token: 0x06001792 RID: 6034 RVA: 0x0005CD5C File Offset: 0x0005AF5C
		public static IDesigner CreateDesigner(IComponent component, Type designerBaseType)
		{
			Type type = null;
			IDesigner designer = null;
			AttributeCollection attributes = TypeDescriptor.GetAttributes(component);
			for (int i = 0; i < attributes.Count; i++)
			{
				DesignerAttribute designerAttribute = attributes[i] as DesignerAttribute;
				if (designerAttribute != null)
				{
					Type type2 = Type.GetType(designerAttribute.DesignerBaseTypeName);
					if (type2 != null && type2 == designerBaseType)
					{
						ISite site = component.Site;
						bool flag = false;
						if (site != null)
						{
							ITypeResolutionService typeResolutionService = (ITypeResolutionService)site.GetService(typeof(ITypeResolutionService));
							if (typeResolutionService != null)
							{
								flag = true;
								type = typeResolutionService.GetType(designerAttribute.DesignerTypeName);
							}
						}
						if (!flag)
						{
							type = Type.GetType(designerAttribute.DesignerTypeName);
						}
						if (type != null)
						{
							break;
						}
					}
				}
			}
			if (type != null)
			{
				designer = (IDesigner)SecurityUtils.SecureCreateInstance(type, null, true);
			}
			return designer;
		}

		/// <summary>Creates a new event descriptor that is identical to an existing event descriptor by dynamically generating descriptor information from a specified event on a type.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> that is bound to a type.</returns>
		/// <param name="componentType">The type of the component the event lives on. </param>
		/// <param name="name">The name of the event. </param>
		/// <param name="type">The type of the delegate that handles the event. </param>
		/// <param name="attributes">The attributes for this event. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="TypeInformation, MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001793 RID: 6035 RVA: 0x0005CE2E File Offset: 0x0005B02E
		[ReflectionPermission(SecurityAction.LinkDemand, Flags = ReflectionPermissionFlag.MemberAccess)]
		public static EventDescriptor CreateEvent(Type componentType, string name, Type type, params Attribute[] attributes)
		{
			return new ReflectEventDescriptor(componentType, name, type, attributes);
		}

		/// <summary>Creates a new event descriptor that is identical to an existing event descriptor, when passed the existing <see cref="T:System.ComponentModel.EventDescriptor" />.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.EventDescriptor" /> that has merged the specified metadata attributes with the existing metadata attributes.</returns>
		/// <param name="componentType">The type of the component for which to create the new event. </param>
		/// <param name="oldEventDescriptor">The existing event information. </param>
		/// <param name="attributes">The new attributes. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="TypeInformation, MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001794 RID: 6036 RVA: 0x0005CE39 File Offset: 0x0005B039
		[ReflectionPermission(SecurityAction.LinkDemand, Flags = ReflectionPermissionFlag.MemberAccess)]
		public static EventDescriptor CreateEvent(Type componentType, EventDescriptor oldEventDescriptor, params Attribute[] attributes)
		{
			return new ReflectEventDescriptor(componentType, oldEventDescriptor, attributes);
		}

		/// <summary>Creates an object that can substitute for another data type. </summary>
		/// <returns>An instance of the substitute data type if an associated <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> is found; otherwise, null.</returns>
		/// <param name="provider">The service provider that provides a <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> service. This parameter can be null.</param>
		/// <param name="objectType">The <see cref="T:System.Type" /> of object to create.</param>
		/// <param name="argTypes">An optional array of parameter types to be passed to the object's constructor. This parameter can be null or an array of zero length.</param>
		/// <param name="args">An optional array of parameter values to pass to the object's constructor. If not null, the number of elements must be the same as <paramref name="argTypes" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="objectType" /> is null, or <paramref name="args" /> is null when <paramref name="argTypes" /> is not null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="argTypes" /> and <paramref name="args" /> have different number of elements.</exception>
		// Token: 0x06001795 RID: 6037 RVA: 0x0005CE44 File Offset: 0x0005B044
		public static object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
		{
			if (objectType == null)
			{
				throw new ArgumentNullException("objectType");
			}
			if (argTypes != null)
			{
				if (args == null)
				{
					throw new ArgumentNullException("args");
				}
				if (argTypes.Length != args.Length)
				{
					throw new ArgumentException(global::SR.GetString("The number of elements in the Type and Object arrays must match."));
				}
			}
			object obj = null;
			if (provider != null)
			{
				TypeDescriptionProvider typeDescriptionProvider = provider.GetService(typeof(TypeDescriptionProvider)) as TypeDescriptionProvider;
				if (typeDescriptionProvider != null)
				{
					obj = typeDescriptionProvider.CreateInstance(provider, objectType, argTypes, args);
				}
			}
			if (obj == null)
			{
				obj = TypeDescriptor.NodeFor(objectType).CreateInstance(provider, objectType, argTypes, args);
			}
			return obj;
		}

		/// <summary>Creates and dynamically binds a property descriptor to a type, using the specified property name, type, and attribute array.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is bound to the specified type and that has the specified metadata attributes merged with the existing metadata attributes.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the component that the property is a member of. </param>
		/// <param name="name">The name of the property. </param>
		/// <param name="type">The <see cref="T:System.Type" /> of the property. </param>
		/// <param name="attributes">The new attributes for this property. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="TypeInformation, MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001796 RID: 6038 RVA: 0x0005CECB File Offset: 0x0005B0CB
		[ReflectionPermission(SecurityAction.LinkDemand, Flags = ReflectionPermissionFlag.MemberAccess)]
		public static PropertyDescriptor CreateProperty(Type componentType, string name, Type type, params Attribute[] attributes)
		{
			return new ReflectPropertyDescriptor(componentType, name, type, attributes);
		}

		/// <summary>Creates a new property descriptor from an existing property descriptor, using the specified existing <see cref="T:System.ComponentModel.PropertyDescriptor" /> and attribute array.</summary>
		/// <returns>A new <see cref="T:System.ComponentModel.PropertyDescriptor" /> that has the specified metadata attributes merged with the existing metadata attributes.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the component that the property is a member of. </param>
		/// <param name="oldPropertyDescriptor">The existing property descriptor. </param>
		/// <param name="attributes">The new attributes for this property. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="TypeInformation, MemberAccess" />
		/// </PermissionSet>
		// Token: 0x06001797 RID: 6039 RVA: 0x0005CED8 File Offset: 0x0005B0D8
		[ReflectionPermission(SecurityAction.LinkDemand, Flags = ReflectionPermissionFlag.MemberAccess)]
		public static PropertyDescriptor CreateProperty(Type componentType, PropertyDescriptor oldPropertyDescriptor, params Attribute[] attributes)
		{
			if (componentType == oldPropertyDescriptor.ComponentType && ((ExtenderProvidedPropertyAttribute)oldPropertyDescriptor.Attributes[typeof(ExtenderProvidedPropertyAttribute)]).ExtenderProperty is ReflectPropertyDescriptor)
			{
				return new ExtendedPropertyDescriptor(oldPropertyDescriptor, attributes);
			}
			return new ReflectPropertyDescriptor(componentType, oldPropertyDescriptor, attributes);
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		private static void DebugValidate(Type type, AttributeCollection attributes, AttributeCollection debugAttributes)
		{
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		private static void DebugValidate(AttributeCollection attributes, AttributeCollection debugAttributes)
		{
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		private static void DebugValidate(AttributeCollection attributes, Type type)
		{
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		private static void DebugValidate(AttributeCollection attributes, object instance, bool noCustomTypeDesc)
		{
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		private static void DebugValidate(TypeConverter converter, Type type)
		{
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		private static void DebugValidate(TypeConverter converter, object instance, bool noCustomTypeDesc)
		{
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		private static void DebugValidate(EventDescriptorCollection events, Type type, Attribute[] attributes)
		{
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		private static void DebugValidate(EventDescriptorCollection events, object instance, Attribute[] attributes, bool noCustomTypeDesc)
		{
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		private static void DebugValidate(PropertyDescriptorCollection properties, Type type, Attribute[] attributes)
		{
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		private static void DebugValidate(PropertyDescriptorCollection properties, object instance, Attribute[] attributes, bool noCustomTypeDesc)
		{
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x0005CF2C File Offset: 0x0005B12C
		private static ArrayList FilterMembers(IList members, Attribute[] attributes)
		{
			ArrayList arrayList = null;
			int count = members.Count;
			for (int i = 0; i < count; i++)
			{
				bool flag = false;
				for (int j = 0; j < attributes.Length; j++)
				{
					if (TypeDescriptor.ShouldHideMember((MemberDescriptor)members[i], attributes[j]))
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList(count);
						for (int k = 0; k < i; k++)
						{
							arrayList.Add(members[k]);
						}
					}
				}
				else if (arrayList != null)
				{
					arrayList.Add(members[i]);
				}
			}
			return arrayList;
		}

		/// <summary>Returns an instance of the type associated with the specified primary object.</summary>
		/// <returns>An instance of the secondary type that has been associated with the primary object if an association exists; otherwise, <paramref name="primary" /> if no specified association exists.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="primary">The primary object of the association.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x060017A3 RID: 6051 RVA: 0x0005CFC0 File Offset: 0x0005B1C0
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static object GetAssociation(Type type, object primary)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (primary == null)
			{
				throw new ArgumentNullException("primary");
			}
			object obj = primary;
			if (!type.IsInstanceOfType(primary))
			{
				Hashtable associationTable = TypeDescriptor._associationTable;
				if (associationTable != null)
				{
					IList list = (IList)associationTable[primary];
					if (list != null)
					{
						IList list2 = list;
						lock (list2)
						{
							for (int i = list.Count - 1; i >= 0; i--)
							{
								object target = ((WeakReference)list[i]).Target;
								if (target == null)
								{
									list.RemoveAt(i);
								}
								else if (type.IsInstanceOfType(target))
								{
									obj = target;
								}
							}
						}
					}
				}
				if (obj == primary)
				{
					IComponent component = primary as IComponent;
					if (component != null)
					{
						ISite site = component.Site;
						if (site != null && site.DesignMode)
						{
							IDesignerHost designerHost = site.GetService(typeof(IDesignerHost)) as IDesignerHost;
							if (designerHost != null)
							{
								object designer = designerHost.GetDesigner(component);
								if (designer != null && type.IsInstanceOfType(designer))
								{
									obj = designer;
								}
							}
						}
					}
				}
			}
			return obj;
		}

		/// <summary>Returns a collection of attributes for the specified type of component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> with the attributes for the type of the component. If the component is null, this method returns an empty collection.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component. </param>
		// Token: 0x060017A4 RID: 6052 RVA: 0x0005D0E8 File Offset: 0x0005B2E8
		public static AttributeCollection GetAttributes(Type componentType)
		{
			if (componentType == null)
			{
				return new AttributeCollection(null);
			}
			return TypeDescriptor.GetDescriptor(componentType, "componentType").GetAttributes();
		}

		/// <summary>Returns the collection of attributes for the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for the component. If <paramref name="component" /> is null, this method returns an empty collection.</returns>
		/// <param name="component">The component for which you want to get attributes. </param>
		// Token: 0x060017A5 RID: 6053 RVA: 0x0005D10A File Offset: 0x0005B30A
		public static AttributeCollection GetAttributes(object component)
		{
			return TypeDescriptor.GetAttributes(component, false);
		}

		/// <summary>Returns a collection of attributes for the specified component and a Boolean indicating that a custom type descriptor has been created.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> with the attributes for the component. If the component is null, this method returns an empty collection.</returns>
		/// <param name="component">The component for which you want to get attributes. </param>
		/// <param name="noCustomTypeDesc">true to use a baseline set of attributes from the custom type descriptor if <paramref name="component" /> is of type <see cref="T:System.ComponentModel.ICustomTypeDescriptor" />; otherwise, false.</param>
		// Token: 0x060017A6 RID: 6054 RVA: 0x0005D114 File Offset: 0x0005B314
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static AttributeCollection GetAttributes(object component, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				return new AttributeCollection(null);
			}
			ICollection collection = TypeDescriptor.GetDescriptor(component, noCustomTypeDesc).GetAttributes();
			if (component is ICustomTypeDescriptor)
			{
				if (noCustomTypeDesc)
				{
					ICustomTypeDescriptor extendedDescriptor = TypeDescriptor.GetExtendedDescriptor(component);
					if (extendedDescriptor != null)
					{
						ICollection attributes = extendedDescriptor.GetAttributes();
						collection = TypeDescriptor.PipelineMerge(0, collection, attributes, component, null);
					}
				}
				else
				{
					collection = TypeDescriptor.PipelineFilter(0, collection, component, null);
				}
			}
			else
			{
				IDictionary cache = TypeDescriptor.GetCache(component);
				collection = TypeDescriptor.PipelineInitialize(0, collection, cache);
				ICustomTypeDescriptor extendedDescriptor2 = TypeDescriptor.GetExtendedDescriptor(component);
				if (extendedDescriptor2 != null)
				{
					ICollection attributes2 = extendedDescriptor2.GetAttributes();
					collection = TypeDescriptor.PipelineMerge(0, collection, attributes2, component, cache);
				}
				collection = TypeDescriptor.PipelineFilter(0, collection, component, cache);
			}
			AttributeCollection attributeCollection = collection as AttributeCollection;
			if (attributeCollection == null)
			{
				Attribute[] array = new Attribute[collection.Count];
				collection.CopyTo(array, 0);
				attributeCollection = new AttributeCollection(array);
			}
			return attributeCollection;
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0005D1D5 File Offset: 0x0005B3D5
		internal static IDictionary GetCache(object instance)
		{
			return TypeDescriptor.NodeFor(instance).GetCache(instance);
		}

		/// <summary>Returns the name of the class for the specified component using the default type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the class for the specified component.</returns>
		/// <param name="component">The <see cref="T:System.Object" /> for which you want the class name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		// Token: 0x060017A8 RID: 6056 RVA: 0x0005D1E3 File Offset: 0x0005B3E3
		public static string GetClassName(object component)
		{
			return TypeDescriptor.GetClassName(component, false);
		}

		/// <summary>Returns the name of the class for the specified component using a custom type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the class for the specified component.</returns>
		/// <param name="component">The <see cref="T:System.Object" /> for which you want the class name. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017A9 RID: 6057 RVA: 0x0005D1EC File Offset: 0x0005B3EC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static string GetClassName(object component, bool noCustomTypeDesc)
		{
			return TypeDescriptor.GetDescriptor(component, noCustomTypeDesc).GetClassName();
		}

		/// <summary>Returns the name of the class for the specified type.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the class for the specified component type.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="componentType" /> is null.</exception>
		// Token: 0x060017AA RID: 6058 RVA: 0x0005D1FA File Offset: 0x0005B3FA
		public static string GetClassName(Type componentType)
		{
			return TypeDescriptor.GetDescriptor(componentType, "componentType").GetClassName();
		}

		/// <summary>Returns the name of the specified component using the default type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the specified component, or null if there is no component name.</returns>
		/// <param name="component">The <see cref="T:System.Object" /> for which you want the class name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017AB RID: 6059 RVA: 0x0005D20C File Offset: 0x0005B40C
		public static string GetComponentName(object component)
		{
			return TypeDescriptor.GetComponentName(component, false);
		}

		/// <summary>Returns the name of the specified component using a custom type descriptor.</summary>
		/// <returns>The name of the class for the specified component, or null if there is no component name.</returns>
		/// <param name="component">The <see cref="T:System.Object" /> for which you want the class name. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017AC RID: 6060 RVA: 0x0005D215 File Offset: 0x0005B415
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static string GetComponentName(object component, bool noCustomTypeDesc)
		{
			return TypeDescriptor.GetDescriptor(component, noCustomTypeDesc).GetComponentName();
		}

		/// <summary>Returns a type converter for the type of the specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified component.</returns>
		/// <param name="component">A component to get the converter for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017AD RID: 6061 RVA: 0x0005D223 File Offset: 0x0005B423
		public static TypeConverter GetConverter(object component)
		{
			return TypeDescriptor.GetConverter(component, false);
		}

		/// <summary>Returns a type converter for the type of the specified component with a custom type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified component.</returns>
		/// <param name="component">A component to get the converter for. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017AE RID: 6062 RVA: 0x0005D22C File Offset: 0x0005B42C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static TypeConverter GetConverter(object component, bool noCustomTypeDesc)
		{
			return TypeDescriptor.GetDescriptor(component, noCustomTypeDesc).GetConverter();
		}

		/// <summary>Returns a type converter for the specified type.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		// Token: 0x060017AF RID: 6063 RVA: 0x0005D23A File Offset: 0x0005B43A
		public static TypeConverter GetConverter(Type type)
		{
			return TypeDescriptor.GetDescriptor(type, "type").GetConverter();
		}

		/// <summary>Returns the default event for the specified type of component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> with the default event, or null if there are no events.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		// Token: 0x060017B0 RID: 6064 RVA: 0x0005D24C File Offset: 0x0005B44C
		public static EventDescriptor GetDefaultEvent(Type componentType)
		{
			if (componentType == null)
			{
				return null;
			}
			return TypeDescriptor.GetDescriptor(componentType, "componentType").GetDefaultEvent();
		}

		/// <summary>Returns the default event for the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> with the default event, or null if there are no events.</returns>
		/// <param name="component">The component to get the event for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017B1 RID: 6065 RVA: 0x0005D269 File Offset: 0x0005B469
		public static EventDescriptor GetDefaultEvent(object component)
		{
			return TypeDescriptor.GetDefaultEvent(component, false);
		}

		/// <summary>Returns the default event for a component with a custom type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptor" /> with the default event, or null if there are no events.</returns>
		/// <param name="component">The component to get the event for. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017B2 RID: 6066 RVA: 0x0005D272 File Offset: 0x0005B472
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static EventDescriptor GetDefaultEvent(object component, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				return null;
			}
			return TypeDescriptor.GetDescriptor(component, noCustomTypeDesc).GetDefaultEvent();
		}

		/// <summary>Returns the default property for the specified type of component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the default property, or null if there are no properties.</returns>
		/// <param name="componentType">A <see cref="T:System.Type" /> that represents the class to get the property for. </param>
		// Token: 0x060017B3 RID: 6067 RVA: 0x0005D285 File Offset: 0x0005B485
		public static PropertyDescriptor GetDefaultProperty(Type componentType)
		{
			if (componentType == null)
			{
				return null;
			}
			return TypeDescriptor.GetDescriptor(componentType, "componentType").GetDefaultProperty();
		}

		/// <summary>Returns the default property for the specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the default property, or null if there are no properties.</returns>
		/// <param name="component">The component to get the default property for. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017B4 RID: 6068 RVA: 0x0005D2A2 File Offset: 0x0005B4A2
		public static PropertyDescriptor GetDefaultProperty(object component)
		{
			return TypeDescriptor.GetDefaultProperty(component, false);
		}

		/// <summary>Returns the default property for the specified component with a custom type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> with the default property, or null if there are no properties.</returns>
		/// <param name="component">The component to get the default property for. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017B5 RID: 6069 RVA: 0x0005D2AB File Offset: 0x0005B4AB
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static PropertyDescriptor GetDefaultProperty(object component, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				return null;
			}
			return TypeDescriptor.GetDescriptor(component, noCustomTypeDesc).GetDefaultProperty();
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0005D2BE File Offset: 0x0005B4BE
		internal static ICustomTypeDescriptor GetDescriptor(Type type, string typeName)
		{
			if (type == null)
			{
				throw new ArgumentNullException(typeName);
			}
			return TypeDescriptor.NodeFor(type).GetTypeDescriptor(type);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0005D2DC File Offset: 0x0005B4DC
		internal static ICustomTypeDescriptor GetDescriptor(object component, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				throw new ArgumentException("component");
			}
			if (component is TypeDescriptor.IUnimplemented)
			{
				throw new NotSupportedException(global::SR.GetString("The object {0} is being remoted by a proxy that does not support interface discovery.  This type of remoted object is not supported.", new object[] { component.GetType().FullName }));
			}
			ICustomTypeDescriptor customTypeDescriptor = TypeDescriptor.NodeFor(component).GetTypeDescriptor(component);
			ICustomTypeDescriptor customTypeDescriptor2 = component as ICustomTypeDescriptor;
			if (!noCustomTypeDesc && customTypeDescriptor2 != null)
			{
				customTypeDescriptor = new TypeDescriptor.MergedTypeDescriptor(customTypeDescriptor2, customTypeDescriptor);
			}
			return customTypeDescriptor;
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0005D346 File Offset: 0x0005B546
		internal static ICustomTypeDescriptor GetExtendedDescriptor(object component)
		{
			if (component == null)
			{
				throw new ArgumentException("component");
			}
			return TypeDescriptor.NodeFor(component).GetExtendedTypeDescriptor(component);
		}

		/// <summary>Gets an editor with the specified base type for the specified component.</summary>
		/// <returns>An instance of the editor that can be cast to the specified editor type, or null if no editor of the requested type can be found.</returns>
		/// <param name="component">The component to get the editor for. </param>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the base type of the editor you want to find. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="editorBaseType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017B9 RID: 6073 RVA: 0x0005D362 File Offset: 0x0005B562
		public static object GetEditor(object component, Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(component, editorBaseType, false);
		}

		/// <summary>Returns an editor with the specified base type and with a custom type descriptor for the specified component.</summary>
		/// <returns>An instance of the editor that can be cast to the specified editor type, or null if no editor of the requested type can be found.</returns>
		/// <param name="component">The component to get the editor for. </param>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the base type of the editor you want to find. </param>
		/// <param name="noCustomTypeDesc">A flag indicating whether custom type description information should be considered.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> or <paramref name="editorBaseType" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017BA RID: 6074 RVA: 0x0005D36C File Offset: 0x0005B56C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static object GetEditor(object component, Type editorBaseType, bool noCustomTypeDesc)
		{
			if (editorBaseType == null)
			{
				throw new ArgumentNullException("editorBaseType");
			}
			return TypeDescriptor.GetDescriptor(component, noCustomTypeDesc).GetEditor(editorBaseType);
		}

		/// <summary>Returns an editor with the specified base type for the specified type.</summary>
		/// <returns>An instance of the editor object that can be cast to the given base type, or null if no editor of the requested type can be found.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="editorBaseType">A <see cref="T:System.Type" /> that represents the base type of the editor you are trying to find. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> or <paramref name="editorBaseType" /> is null. </exception>
		// Token: 0x060017BB RID: 6075 RVA: 0x0005D38F File Offset: 0x0005B58F
		public static object GetEditor(Type type, Type editorBaseType)
		{
			if (editorBaseType == null)
			{
				throw new ArgumentNullException("editorBaseType");
			}
			return TypeDescriptor.GetDescriptor(type, "type").GetEditor(editorBaseType);
		}

		/// <summary>Returns the collection of events for a specified type of component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events for this component.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		// Token: 0x060017BC RID: 6076 RVA: 0x0005D3B6 File Offset: 0x0005B5B6
		public static EventDescriptorCollection GetEvents(Type componentType)
		{
			if (componentType == null)
			{
				return new EventDescriptorCollection(null, true);
			}
			return TypeDescriptor.GetDescriptor(componentType, "componentType").GetEvents();
		}

		/// <summary>Returns the collection of events for a specified type of component using a specified array of attributes as a filter.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events that match the specified attributes for this component.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that you can use as a filter. </param>
		// Token: 0x060017BD RID: 6077 RVA: 0x0005D3DC File Offset: 0x0005B5DC
		public static EventDescriptorCollection GetEvents(Type componentType, Attribute[] attributes)
		{
			if (componentType == null)
			{
				return new EventDescriptorCollection(null, true);
			}
			EventDescriptorCollection eventDescriptorCollection = TypeDescriptor.GetDescriptor(componentType, "componentType").GetEvents(attributes);
			if (attributes != null && attributes.Length != 0)
			{
				ArrayList arrayList = TypeDescriptor.FilterMembers(eventDescriptorCollection, attributes);
				if (arrayList != null)
				{
					eventDescriptorCollection = new EventDescriptorCollection((EventDescriptor[])arrayList.ToArray(typeof(EventDescriptor)), true);
				}
			}
			return eventDescriptorCollection;
		}

		/// <summary>Returns the collection of events for the specified component.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events for this component.</returns>
		/// <param name="component">A component to get the events for. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017BE RID: 6078 RVA: 0x0005D43B File Offset: 0x0005B63B
		public static EventDescriptorCollection GetEvents(object component)
		{
			return TypeDescriptor.GetEvents(component, null, false);
		}

		/// <summary>Returns the collection of events for a specified component with a custom type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events for this component.</returns>
		/// <param name="component">A component to get the events for. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017BF RID: 6079 RVA: 0x0005D445 File Offset: 0x0005B645
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static EventDescriptorCollection GetEvents(object component, bool noCustomTypeDesc)
		{
			return TypeDescriptor.GetEvents(component, null, noCustomTypeDesc);
		}

		/// <summary>Returns the collection of events for a specified component using a specified array of attributes as a filter.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events that match the specified attributes for this component.</returns>
		/// <param name="component">A component to get the events for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> that you can use as a filter. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017C0 RID: 6080 RVA: 0x0005D44F File Offset: 0x0005B64F
		public static EventDescriptorCollection GetEvents(object component, Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(component, attributes, false);
		}

		/// <summary>Returns the collection of events for a specified component using a specified array of attributes as a filter and using a custom type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> with the events that match the specified attributes for this component.</returns>
		/// <param name="component">A component to get the events for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017C1 RID: 6081 RVA: 0x0005D45C File Offset: 0x0005B65C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static EventDescriptorCollection GetEvents(object component, Attribute[] attributes, bool noCustomTypeDesc)
		{
			if (component == null)
			{
				return new EventDescriptorCollection(null, true);
			}
			ICustomTypeDescriptor descriptor = TypeDescriptor.GetDescriptor(component, noCustomTypeDesc);
			ICollection collection;
			if (component is ICustomTypeDescriptor)
			{
				collection = descriptor.GetEvents(attributes);
				if (noCustomTypeDesc)
				{
					ICustomTypeDescriptor extendedDescriptor = TypeDescriptor.GetExtendedDescriptor(component);
					if (extendedDescriptor != null)
					{
						ICollection events = extendedDescriptor.GetEvents(attributes);
						collection = TypeDescriptor.PipelineMerge(2, collection, events, component, null);
					}
				}
				else
				{
					collection = TypeDescriptor.PipelineFilter(2, collection, component, null);
					collection = TypeDescriptor.PipelineAttributeFilter(2, collection, attributes, component, null);
				}
			}
			else
			{
				IDictionary cache = TypeDescriptor.GetCache(component);
				collection = descriptor.GetEvents(attributes);
				collection = TypeDescriptor.PipelineInitialize(2, collection, cache);
				ICustomTypeDescriptor extendedDescriptor2 = TypeDescriptor.GetExtendedDescriptor(component);
				if (extendedDescriptor2 != null)
				{
					ICollection events2 = extendedDescriptor2.GetEvents(attributes);
					collection = TypeDescriptor.PipelineMerge(2, collection, events2, component, cache);
				}
				collection = TypeDescriptor.PipelineFilter(2, collection, component, cache);
				collection = TypeDescriptor.PipelineAttributeFilter(2, collection, attributes, component, cache);
			}
			EventDescriptorCollection eventDescriptorCollection = collection as EventDescriptorCollection;
			if (eventDescriptorCollection == null)
			{
				EventDescriptor[] array = new EventDescriptor[collection.Count];
				collection.CopyTo(array, 0);
				eventDescriptorCollection = new EventDescriptorCollection(array, true);
			}
			return eventDescriptorCollection;
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x0005D548 File Offset: 0x0005B748
		private static string GetExtenderCollisionSuffix(MemberDescriptor member)
		{
			string text = null;
			ExtenderProvidedPropertyAttribute extenderProvidedPropertyAttribute = member.Attributes[typeof(ExtenderProvidedPropertyAttribute)] as ExtenderProvidedPropertyAttribute;
			if (extenderProvidedPropertyAttribute != null)
			{
				IExtenderProvider provider = extenderProvidedPropertyAttribute.Provider;
				if (provider != null)
				{
					string text2 = null;
					IComponent component = provider as IComponent;
					if (component != null && component.Site != null)
					{
						text2 = component.Site.Name;
					}
					if (text2 == null || text2.Length == 0)
					{
						text2 = (Interlocked.Increment(ref TypeDescriptor._collisionIndex) - 1).ToString(CultureInfo.InvariantCulture);
					}
					text = string.Format(CultureInfo.InvariantCulture, "_{0}", text2);
				}
			}
			return text;
		}

		/// <summary>Returns the fully qualified name of the component.</summary>
		/// <returns>The fully qualified name of the specified component, or null if the component has no name.</returns>
		/// <param name="component">The <see cref="T:System.ComponentModel.Component" /> to find the name for.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="component" /> is null.</exception>
		// Token: 0x060017C3 RID: 6083 RVA: 0x0005D5DB File Offset: 0x0005B7DB
		public static string GetFullComponentName(object component)
		{
			if (component == null)
			{
				throw new ArgumentNullException("component");
			}
			return TypeDescriptor.GetProvider(component).GetFullComponentName(component);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x0005D5F7 File Offset: 0x0005B7F7
		private static Type GetNodeForBaseType(Type searchType)
		{
			if (searchType.IsInterface)
			{
				return TypeDescriptor.InterfaceType;
			}
			if (searchType == TypeDescriptor.InterfaceType)
			{
				return null;
			}
			return searchType.BaseType;
		}

		/// <summary>Returns the collection of properties for a specified type of component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties for a specified type of component.</returns>
		/// <param name="componentType">A <see cref="T:System.Type" /> that represents the component to get properties for.</param>
		// Token: 0x060017C5 RID: 6085 RVA: 0x0005D61C File Offset: 0x0005B81C
		public static PropertyDescriptorCollection GetProperties(Type componentType)
		{
			if (componentType == null)
			{
				return new PropertyDescriptorCollection(null, true);
			}
			return TypeDescriptor.GetDescriptor(componentType, "componentType").GetProperties();
		}

		/// <summary>Returns the collection of properties for a specified type of component using a specified array of attributes as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes for this type of component.</returns>
		/// <param name="componentType">The <see cref="T:System.Type" /> of the target component.</param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		// Token: 0x060017C6 RID: 6086 RVA: 0x0005D640 File Offset: 0x0005B840
		public static PropertyDescriptorCollection GetProperties(Type componentType, Attribute[] attributes)
		{
			if (componentType == null)
			{
				return new PropertyDescriptorCollection(null, true);
			}
			PropertyDescriptorCollection propertyDescriptorCollection = TypeDescriptor.GetDescriptor(componentType, "componentType").GetProperties(attributes);
			if (attributes != null && attributes.Length != 0)
			{
				ArrayList arrayList = TypeDescriptor.FilterMembers(propertyDescriptorCollection, attributes);
				if (arrayList != null)
				{
					propertyDescriptorCollection = new PropertyDescriptorCollection((PropertyDescriptor[])arrayList.ToArray(typeof(PropertyDescriptor)), true);
				}
			}
			return propertyDescriptorCollection;
		}

		/// <summary>Returns the collection of properties for a specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties for the specified component.</returns>
		/// <param name="component">A component to get the properties for. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017C7 RID: 6087 RVA: 0x0005D69F File Offset: 0x0005B89F
		public static PropertyDescriptorCollection GetProperties(object component)
		{
			return TypeDescriptor.GetProperties(component, false);
		}

		/// <summary>Returns the collection of properties for a specified component using the default type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties for a specified component.</returns>
		/// <param name="component">A component to get the properties for. </param>
		/// <param name="noCustomTypeDesc">true to not consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017C8 RID: 6088 RVA: 0x0005D6A8 File Offset: 0x0005B8A8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static PropertyDescriptorCollection GetProperties(object component, bool noCustomTypeDesc)
		{
			return TypeDescriptor.GetPropertiesImpl(component, null, noCustomTypeDesc, true);
		}

		/// <summary>Returns the collection of properties for a specified component using a specified array of attributes as a filter.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the properties that match the specified attributes for the specified component.</returns>
		/// <param name="component">A component to get the properties for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017C9 RID: 6089 RVA: 0x0005D6B3 File Offset: 0x0005B8B3
		public static PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes)
		{
			return TypeDescriptor.GetProperties(component, attributes, false);
		}

		/// <summary>Returns the collection of properties for a specified component using a specified array of attributes as a filter and using a custom type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> with the events that match the specified attributes for the specified component.</returns>
		/// <param name="component">A component to get the properties for. </param>
		/// <param name="attributes">An array of type <see cref="T:System.Attribute" /> to use as a filter. </param>
		/// <param name="noCustomTypeDesc">true to consider custom type description information; otherwise, false.</param>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="component" /> is a cross-process remoted object.</exception>
		// Token: 0x060017CA RID: 6090 RVA: 0x0005D6BD File Offset: 0x0005B8BD
		public static PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes, bool noCustomTypeDesc)
		{
			return TypeDescriptor.GetPropertiesImpl(component, attributes, noCustomTypeDesc, false);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x0005D6C8 File Offset: 0x0005B8C8
		private static PropertyDescriptorCollection GetPropertiesImpl(object component, Attribute[] attributes, bool noCustomTypeDesc, bool noAttributes)
		{
			if (component == null)
			{
				return new PropertyDescriptorCollection(null, true);
			}
			ICustomTypeDescriptor descriptor = TypeDescriptor.GetDescriptor(component, noCustomTypeDesc);
			ICollection collection;
			if (component is ICustomTypeDescriptor)
			{
				collection = (noAttributes ? descriptor.GetProperties() : descriptor.GetProperties(attributes));
				if (noCustomTypeDesc)
				{
					ICustomTypeDescriptor extendedDescriptor = TypeDescriptor.GetExtendedDescriptor(component);
					if (extendedDescriptor != null)
					{
						ICollection collection2 = (noAttributes ? extendedDescriptor.GetProperties() : extendedDescriptor.GetProperties(attributes));
						collection = TypeDescriptor.PipelineMerge(1, collection, collection2, component, null);
					}
				}
				else
				{
					collection = TypeDescriptor.PipelineFilter(1, collection, component, null);
					collection = TypeDescriptor.PipelineAttributeFilter(1, collection, attributes, component, null);
				}
			}
			else
			{
				IDictionary cache = TypeDescriptor.GetCache(component);
				collection = (noAttributes ? descriptor.GetProperties() : descriptor.GetProperties(attributes));
				collection = TypeDescriptor.PipelineInitialize(1, collection, cache);
				ICustomTypeDescriptor extendedDescriptor2 = TypeDescriptor.GetExtendedDescriptor(component);
				if (extendedDescriptor2 != null)
				{
					ICollection collection3 = (noAttributes ? extendedDescriptor2.GetProperties() : extendedDescriptor2.GetProperties(attributes));
					collection = TypeDescriptor.PipelineMerge(1, collection, collection3, component, cache);
				}
				collection = TypeDescriptor.PipelineFilter(1, collection, component, cache);
				collection = TypeDescriptor.PipelineAttributeFilter(1, collection, attributes, component, cache);
			}
			PropertyDescriptorCollection propertyDescriptorCollection = collection as PropertyDescriptorCollection;
			if (propertyDescriptorCollection == null)
			{
				PropertyDescriptor[] array = new PropertyDescriptor[collection.Count];
				collection.CopyTo(array, 0);
				propertyDescriptorCollection = new PropertyDescriptorCollection(array, true);
			}
			return propertyDescriptorCollection;
		}

		/// <summary>Returns the type description provider for the specified type.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> associated with the specified type.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x060017CC RID: 6092 RVA: 0x0005D7E4 File Offset: 0x0005B9E4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static TypeDescriptionProvider GetProvider(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return TypeDescriptor.NodeFor(type, true);
		}

		/// <summary>Returns the type description provider for the specified component.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> associated with the specified component.</returns>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x060017CD RID: 6093 RVA: 0x0005D801 File Offset: 0x0005BA01
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static TypeDescriptionProvider GetProvider(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return TypeDescriptor.NodeFor(instance, true);
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x0005D818 File Offset: 0x0005BA18
		internal static TypeDescriptionProvider GetProviderRecursive(Type type)
		{
			return TypeDescriptor.NodeFor(type, false);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> that can be used to perform reflection, given a class type.</summary>
		/// <returns>A <see cref="T:System.Type" /> of the specified class.</returns>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="type" /> is null.</exception>
		// Token: 0x060017CF RID: 6095 RVA: 0x0005D821 File Offset: 0x0005BA21
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Type GetReflectionType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return TypeDescriptor.NodeFor(type).GetReflectionType(type);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> that can be used to perform reflection, given an object.</summary>
		/// <returns>A <see cref="T:System.Type" /> for the specified object.</returns>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="instance" /> is null.</exception>
		// Token: 0x060017D0 RID: 6096 RVA: 0x0005D843 File Offset: 0x0005BA43
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static Type GetReflectionType(object instance)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			return TypeDescriptor.NodeFor(instance).GetReflectionType(instance);
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x0005D818 File Offset: 0x0005BA18
		private static TypeDescriptor.TypeDescriptionNode NodeFor(Type type)
		{
			return TypeDescriptor.NodeFor(type, false);
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x0005D860 File Offset: 0x0005BA60
		private static TypeDescriptor.TypeDescriptionNode NodeFor(Type type, bool createDelegator)
		{
			TypeDescriptor.CheckDefaultProvider(type);
			TypeDescriptor.TypeDescriptionNode typeDescriptionNode = null;
			Type type2 = type;
			while (typeDescriptionNode == null)
			{
				typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)TypeDescriptor._providerTypeTable[type2];
				if (typeDescriptionNode == null)
				{
					typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)TypeDescriptor._providerTable[type2];
				}
				if (typeDescriptionNode == null)
				{
					Type nodeForBaseType = TypeDescriptor.GetNodeForBaseType(type2);
					if (type2 == typeof(object) || nodeForBaseType == null)
					{
						WeakHashtable weakHashtable = TypeDescriptor._providerTable;
						lock (weakHashtable)
						{
							typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)TypeDescriptor._providerTable[type2];
							if (typeDescriptionNode == null)
							{
								typeDescriptionNode = new TypeDescriptor.TypeDescriptionNode(new ReflectTypeDescriptionProvider());
								TypeDescriptor._providerTable[type2] = typeDescriptionNode;
							}
							continue;
						}
					}
					if (createDelegator)
					{
						typeDescriptionNode = new TypeDescriptor.TypeDescriptionNode(new DelegatingTypeDescriptionProvider(nodeForBaseType));
						WeakHashtable weakHashtable = TypeDescriptor._providerTable;
						lock (weakHashtable)
						{
							TypeDescriptor._providerTypeTable[type2] = typeDescriptionNode;
							continue;
						}
					}
					type2 = nodeForBaseType;
				}
			}
			return typeDescriptionNode;
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x0005D970 File Offset: 0x0005BB70
		private static TypeDescriptor.TypeDescriptionNode NodeFor(object instance)
		{
			return TypeDescriptor.NodeFor(instance, false);
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x0005D97C File Offset: 0x0005BB7C
		private static TypeDescriptor.TypeDescriptionNode NodeFor(object instance, bool createDelegator)
		{
			TypeDescriptor.TypeDescriptionNode typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)TypeDescriptor._providerTable[instance];
			if (typeDescriptionNode == null)
			{
				Type type = instance.GetType();
				if (type.IsCOMObject)
				{
					type = TypeDescriptor.ComObjectType;
				}
				if (createDelegator)
				{
					typeDescriptionNode = new TypeDescriptor.TypeDescriptionNode(new DelegatingTypeDescriptionProvider(type));
				}
				else
				{
					typeDescriptionNode = TypeDescriptor.NodeFor(type);
				}
			}
			return typeDescriptionNode;
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x0005D9CC File Offset: 0x0005BBCC
		private static void NodeRemove(object key, TypeDescriptionProvider provider)
		{
			WeakHashtable providerTable = TypeDescriptor._providerTable;
			lock (providerTable)
			{
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)TypeDescriptor._providerTable[key];
				TypeDescriptor.TypeDescriptionNode typeDescriptionNode2 = typeDescriptionNode;
				while (typeDescriptionNode2 != null && typeDescriptionNode2.Provider != provider)
				{
					typeDescriptionNode2 = typeDescriptionNode2.Next;
				}
				if (typeDescriptionNode2 != null)
				{
					if (typeDescriptionNode2.Next != null)
					{
						typeDescriptionNode2.Provider = typeDescriptionNode2.Next.Provider;
						typeDescriptionNode2.Next = typeDescriptionNode2.Next.Next;
						if (typeDescriptionNode2 == typeDescriptionNode && typeDescriptionNode2.Provider is DelegatingTypeDescriptionProvider)
						{
							TypeDescriptor._providerTable.Remove(key);
						}
					}
					else if (typeDescriptionNode2 != typeDescriptionNode)
					{
						Type type = key as Type;
						if (type == null)
						{
							type = key.GetType();
						}
						typeDescriptionNode2.Provider = new DelegatingTypeDescriptionProvider(type.BaseType);
					}
					else
					{
						TypeDescriptor._providerTable.Remove(key);
					}
					TypeDescriptor._providerTypeTable.Clear();
				}
			}
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x0005DAC4 File Offset: 0x0005BCC4
		private static ICollection PipelineAttributeFilter(int pipelineType, ICollection members, Attribute[] filter, object instance, IDictionary cache)
		{
			IList list = members as ArrayList;
			if (filter == null || filter.Length == 0)
			{
				return members;
			}
			if (cache != null && (list == null || list.IsReadOnly))
			{
				TypeDescriptor.AttributeFilterCacheItem attributeFilterCacheItem = cache[TypeDescriptor._pipelineAttributeFilterKeys[pipelineType]] as TypeDescriptor.AttributeFilterCacheItem;
				if (attributeFilterCacheItem != null && attributeFilterCacheItem.IsValid(filter))
				{
					return attributeFilterCacheItem.FilteredMembers;
				}
			}
			if (list == null || list.IsReadOnly)
			{
				list = new ArrayList(members);
			}
			ArrayList arrayList = TypeDescriptor.FilterMembers(list, filter);
			if (arrayList != null)
			{
				list = arrayList;
			}
			if (cache != null)
			{
				ICollection collection;
				if (pipelineType != 1)
				{
					if (pipelineType != 2)
					{
						collection = null;
					}
					else
					{
						EventDescriptor[] array = new EventDescriptor[list.Count];
						list.CopyTo(array, 0);
						collection = new EventDescriptorCollection(array, true);
					}
				}
				else
				{
					PropertyDescriptor[] array2 = new PropertyDescriptor[list.Count];
					list.CopyTo(array2, 0);
					collection = new PropertyDescriptorCollection(array2, true);
				}
				TypeDescriptor.AttributeFilterCacheItem attributeFilterCacheItem2 = new TypeDescriptor.AttributeFilterCacheItem(filter, collection);
				cache[TypeDescriptor._pipelineAttributeFilterKeys[pipelineType]] = attributeFilterCacheItem2;
			}
			return list;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x0005DBB4 File Offset: 0x0005BDB4
		private static ICollection PipelineFilter(int pipelineType, ICollection members, object instance, IDictionary cache)
		{
			IComponent component = instance as IComponent;
			ITypeDescriptorFilterService typeDescriptorFilterService = null;
			if (component != null)
			{
				ISite site = component.Site;
				if (site != null)
				{
					typeDescriptorFilterService = site.GetService(typeof(ITypeDescriptorFilterService)) as ITypeDescriptorFilterService;
				}
			}
			IList list = members as ArrayList;
			if (typeDescriptorFilterService == null)
			{
				return members;
			}
			if (cache != null && (list == null || list.IsReadOnly))
			{
				TypeDescriptor.FilterCacheItem filterCacheItem = cache[TypeDescriptor._pipelineFilterKeys[pipelineType]] as TypeDescriptor.FilterCacheItem;
				if (filterCacheItem != null && filterCacheItem.IsValid(typeDescriptorFilterService))
				{
					return filterCacheItem.FilteredMembers;
				}
			}
			OrderedDictionary orderedDictionary = new OrderedDictionary(members.Count);
			bool flag;
			if (pipelineType != 0)
			{
				if (pipelineType - 1 > 1)
				{
					flag = false;
				}
				else
				{
					foreach (object obj in members)
					{
						MemberDescriptor memberDescriptor = (MemberDescriptor)obj;
						string name = memberDescriptor.Name;
						if (orderedDictionary.Contains(name))
						{
							string text = TypeDescriptor.GetExtenderCollisionSuffix(memberDescriptor);
							if (text != null)
							{
								orderedDictionary[name + text] = memberDescriptor;
							}
							MemberDescriptor memberDescriptor2 = (MemberDescriptor)orderedDictionary[name];
							text = TypeDescriptor.GetExtenderCollisionSuffix(memberDescriptor2);
							if (text != null)
							{
								orderedDictionary.Remove(name);
								orderedDictionary[memberDescriptor2.Name + text] = memberDescriptor2;
							}
						}
						else
						{
							orderedDictionary[name] = memberDescriptor;
						}
					}
					if (pipelineType == 1)
					{
						flag = typeDescriptorFilterService.FilterProperties(component, orderedDictionary);
					}
					else
					{
						flag = typeDescriptorFilterService.FilterEvents(component, orderedDictionary);
					}
				}
			}
			else
			{
				foreach (object obj2 in members)
				{
					Attribute attribute = (Attribute)obj2;
					orderedDictionary[attribute.TypeId] = attribute;
				}
				flag = typeDescriptorFilterService.FilterAttributes(component, orderedDictionary);
			}
			if (list == null || list.IsReadOnly)
			{
				list = new ArrayList(orderedDictionary.Values);
			}
			else
			{
				list.Clear();
				foreach (object obj3 in orderedDictionary.Values)
				{
					list.Add(obj3);
				}
			}
			if (flag && cache != null)
			{
				ICollection collection;
				switch (pipelineType)
				{
				case 0:
				{
					Attribute[] array = new Attribute[list.Count];
					try
					{
						list.CopyTo(array, 0);
					}
					catch (InvalidCastException)
					{
						throw new ArgumentException(global::SR.GetString("Expected types in the collection to be of type {0}.", new object[] { typeof(Attribute).FullName }));
					}
					collection = new AttributeCollection(array);
					break;
				}
				case 1:
				{
					PropertyDescriptor[] array2 = new PropertyDescriptor[list.Count];
					try
					{
						list.CopyTo(array2, 0);
					}
					catch (InvalidCastException)
					{
						throw new ArgumentException(global::SR.GetString("Expected types in the collection to be of type {0}.", new object[] { typeof(PropertyDescriptor).FullName }));
					}
					collection = new PropertyDescriptorCollection(array2, true);
					break;
				}
				case 2:
				{
					EventDescriptor[] array3 = new EventDescriptor[list.Count];
					try
					{
						list.CopyTo(array3, 0);
					}
					catch (InvalidCastException)
					{
						throw new ArgumentException(global::SR.GetString("Expected types in the collection to be of type {0}.", new object[] { typeof(EventDescriptor).FullName }));
					}
					collection = new EventDescriptorCollection(array3, true);
					break;
				}
				default:
					collection = null;
					break;
				}
				TypeDescriptor.FilterCacheItem filterCacheItem2 = new TypeDescriptor.FilterCacheItem(typeDescriptorFilterService, collection);
				cache[TypeDescriptor._pipelineFilterKeys[pipelineType]] = filterCacheItem2;
				cache.Remove(TypeDescriptor._pipelineAttributeFilterKeys[pipelineType]);
			}
			return list;
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x0005DF78 File Offset: 0x0005C178
		private static ICollection PipelineInitialize(int pipelineType, ICollection members, IDictionary cache)
		{
			if (cache != null)
			{
				bool flag = true;
				ICollection collection = cache[TypeDescriptor._pipelineInitializeKeys[pipelineType]] as ICollection;
				if (collection != null && collection.Count == members.Count)
				{
					IEnumerator enumerator = collection.GetEnumerator();
					IEnumerator enumerator2 = members.GetEnumerator();
					while (enumerator.MoveNext() && enumerator2.MoveNext())
					{
						if (enumerator.Current != enumerator2.Current)
						{
							flag = false;
							break;
						}
					}
				}
				if (!flag)
				{
					cache.Remove(TypeDescriptor._pipelineMergeKeys[pipelineType]);
					cache.Remove(TypeDescriptor._pipelineFilterKeys[pipelineType]);
					cache.Remove(TypeDescriptor._pipelineAttributeFilterKeys[pipelineType]);
					cache[TypeDescriptor._pipelineInitializeKeys[pipelineType]] = members;
				}
			}
			return members;
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0005E04C File Offset: 0x0005C24C
		private static ICollection PipelineMerge(int pipelineType, ICollection primary, ICollection secondary, object instance, IDictionary cache)
		{
			if (secondary == null || secondary.Count == 0)
			{
				return primary;
			}
			if (cache != null)
			{
				ICollection collection = cache[TypeDescriptor._pipelineMergeKeys[pipelineType]] as ICollection;
				if (collection != null && collection.Count == primary.Count + secondary.Count)
				{
					IEnumerator enumerator = collection.GetEnumerator();
					IEnumerator enumerator2 = primary.GetEnumerator();
					bool flag = true;
					while (enumerator2.MoveNext() && enumerator.MoveNext())
					{
						if (enumerator2.Current != enumerator.Current)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						IEnumerator enumerator3 = secondary.GetEnumerator();
						while (enumerator3.MoveNext() && enumerator.MoveNext())
						{
							if (enumerator3.Current != enumerator.Current)
							{
								flag = false;
								break;
							}
						}
					}
					if (flag)
					{
						return collection;
					}
				}
			}
			ArrayList arrayList = new ArrayList(primary.Count + secondary.Count);
			foreach (object obj in primary)
			{
				arrayList.Add(obj);
			}
			foreach (object obj2 in secondary)
			{
				arrayList.Add(obj2);
			}
			if (cache != null)
			{
				ICollection collection2;
				switch (pipelineType)
				{
				case 0:
				{
					Attribute[] array = new Attribute[arrayList.Count];
					arrayList.CopyTo(array, 0);
					collection2 = new AttributeCollection(array);
					break;
				}
				case 1:
				{
					PropertyDescriptor[] array2 = new PropertyDescriptor[arrayList.Count];
					arrayList.CopyTo(array2, 0);
					collection2 = new PropertyDescriptorCollection(array2, true);
					break;
				}
				case 2:
				{
					EventDescriptor[] array3 = new EventDescriptor[arrayList.Count];
					arrayList.CopyTo(array3, 0);
					collection2 = new EventDescriptorCollection(array3, true);
					break;
				}
				default:
					collection2 = null;
					break;
				}
				cache[TypeDescriptor._pipelineMergeKeys[pipelineType]] = collection2;
				cache.Remove(TypeDescriptor._pipelineFilterKeys[pipelineType]);
				cache.Remove(TypeDescriptor._pipelineAttributeFilterKeys[pipelineType]);
			}
			return arrayList;
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0005E288 File Offset: 0x0005C488
		private static void RaiseRefresh(object component)
		{
			RefreshEventHandler refreshEventHandler = Volatile.Read<RefreshEventHandler>(ref TypeDescriptor.Refreshed);
			if (refreshEventHandler != null)
			{
				refreshEventHandler(new RefreshEventArgs(component));
			}
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0005E2B0 File Offset: 0x0005C4B0
		private static void RaiseRefresh(Type type)
		{
			RefreshEventHandler refreshEventHandler = Volatile.Read<RefreshEventHandler>(ref TypeDescriptor.Refreshed);
			if (refreshEventHandler != null)
			{
				refreshEventHandler(new RefreshEventArgs(type));
			}
		}

		/// <summary>Clears the properties and events for the specified component from the cache.</summary>
		/// <param name="component">A component for which the properties or events have changed. </param>
		// Token: 0x060017DC RID: 6108 RVA: 0x0005E2D7 File Offset: 0x0005C4D7
		public static void Refresh(object component)
		{
			TypeDescriptor.Refresh(component, true);
		}

		// Token: 0x060017DD RID: 6109 RVA: 0x0005E2E0 File Offset: 0x0005C4E0
		private static void Refresh(object component, bool refreshReflectionProvider)
		{
			if (component == null)
			{
				return;
			}
			bool flag = false;
			if (refreshReflectionProvider)
			{
				Type type = component.GetType();
				WeakHashtable providerTable = TypeDescriptor._providerTable;
				lock (providerTable)
				{
					foreach (object obj in TypeDescriptor._providerTable)
					{
						DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
						Type type2 = dictionaryEntry.Key as Type;
						if ((type2 != null && type.IsAssignableFrom(type2)) || type2 == typeof(object))
						{
							TypeDescriptor.TypeDescriptionNode typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)dictionaryEntry.Value;
							while (typeDescriptionNode != null && !(typeDescriptionNode.Provider is ReflectTypeDescriptionProvider))
							{
								flag = true;
								typeDescriptionNode = typeDescriptionNode.Next;
							}
							if (typeDescriptionNode != null)
							{
								ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = (ReflectTypeDescriptionProvider)typeDescriptionNode.Provider;
								if (reflectTypeDescriptionProvider.IsPopulated(type))
								{
									flag = true;
									reflectTypeDescriptionProvider.Refresh(type);
								}
							}
						}
					}
				}
			}
			IDictionary cache = TypeDescriptor.GetCache(component);
			if (flag || cache != null)
			{
				if (cache != null)
				{
					for (int i = 0; i < TypeDescriptor._pipelineFilterKeys.Length; i++)
					{
						cache.Remove(TypeDescriptor._pipelineFilterKeys[i]);
						cache.Remove(TypeDescriptor._pipelineMergeKeys[i]);
						cache.Remove(TypeDescriptor._pipelineAttributeFilterKeys[i]);
					}
				}
				Interlocked.Increment(ref TypeDescriptor._metadataVersion);
				TypeDescriptor.RaiseRefresh(component);
			}
		}

		/// <summary>Clears the properties and events for the specified type of component from the cache.</summary>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		// Token: 0x060017DE RID: 6110 RVA: 0x0005E480 File Offset: 0x0005C680
		public static void Refresh(Type type)
		{
			if (type == null)
			{
				return;
			}
			bool flag = false;
			WeakHashtable providerTable = TypeDescriptor._providerTable;
			lock (providerTable)
			{
				foreach (object obj in TypeDescriptor._providerTable)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					Type type2 = dictionaryEntry.Key as Type;
					if ((type2 != null && type.IsAssignableFrom(type2)) || type2 == typeof(object))
					{
						TypeDescriptor.TypeDescriptionNode typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)dictionaryEntry.Value;
						while (typeDescriptionNode != null && !(typeDescriptionNode.Provider is ReflectTypeDescriptionProvider))
						{
							flag = true;
							typeDescriptionNode = typeDescriptionNode.Next;
						}
						if (typeDescriptionNode != null)
						{
							ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = (ReflectTypeDescriptionProvider)typeDescriptionNode.Provider;
							if (reflectTypeDescriptionProvider.IsPopulated(type))
							{
								flag = true;
								reflectTypeDescriptionProvider.Refresh(type);
							}
						}
					}
				}
			}
			if (flag)
			{
				Interlocked.Increment(ref TypeDescriptor._metadataVersion);
				TypeDescriptor.RaiseRefresh(type);
			}
		}

		/// <summary>Clears the properties and events for the specified module from the cache.</summary>
		/// <param name="module">The <see cref="T:System.Reflection.Module" /> that represents the module to refresh. Each <see cref="T:System.Type" /> in this module will be refreshed. </param>
		// Token: 0x060017DF RID: 6111 RVA: 0x0005E5AC File Offset: 0x0005C7AC
		public static void Refresh(Module module)
		{
			if (module == null)
			{
				return;
			}
			Hashtable hashtable = null;
			WeakHashtable providerTable = TypeDescriptor._providerTable;
			lock (providerTable)
			{
				foreach (object obj in TypeDescriptor._providerTable)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					Type type = dictionaryEntry.Key as Type;
					if ((type != null && type.Module.Equals(module)) || type == typeof(object))
					{
						TypeDescriptor.TypeDescriptionNode typeDescriptionNode = (TypeDescriptor.TypeDescriptionNode)dictionaryEntry.Value;
						while (typeDescriptionNode != null && !(typeDescriptionNode.Provider is ReflectTypeDescriptionProvider))
						{
							if (hashtable == null)
							{
								hashtable = new Hashtable();
							}
							hashtable[type] = type;
							typeDescriptionNode = typeDescriptionNode.Next;
						}
						if (typeDescriptionNode != null)
						{
							ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = (ReflectTypeDescriptionProvider)typeDescriptionNode.Provider;
							foreach (Type type2 in reflectTypeDescriptionProvider.GetPopulatedTypes(module))
							{
								reflectTypeDescriptionProvider.Refresh(type2);
								if (hashtable == null)
								{
									hashtable = new Hashtable();
								}
								hashtable[type2] = type2;
							}
						}
					}
				}
			}
			if (hashtable != null && TypeDescriptor.Refreshed != null)
			{
				foreach (object obj2 in hashtable.Keys)
				{
					TypeDescriptor.RaiseRefresh((Type)obj2);
				}
			}
		}

		/// <summary>Clears the properties and events for the specified assembly from the cache.</summary>
		/// <param name="assembly">The <see cref="T:System.Reflection.Assembly" /> that represents the assembly to refresh. Each <see cref="T:System.Type" /> in this assembly will be refreshed. </param>
		// Token: 0x060017E0 RID: 6112 RVA: 0x0005E780 File Offset: 0x0005C980
		public static void Refresh(Assembly assembly)
		{
			if (assembly == null)
			{
				return;
			}
			Module[] modules = assembly.GetModules();
			for (int i = 0; i < modules.Length; i++)
			{
				TypeDescriptor.Refresh(modules[i]);
			}
		}

		/// <summary>Removes an association between two objects.</summary>
		/// <param name="primary">The primary <see cref="T:System.Object" />.</param>
		/// <param name="secondary">The secondary <see cref="T:System.Object" />.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x060017E1 RID: 6113 RVA: 0x0005E7B4 File Offset: 0x0005C9B4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void RemoveAssociation(object primary, object secondary)
		{
			if (primary == null)
			{
				throw new ArgumentNullException("primary");
			}
			if (secondary == null)
			{
				throw new ArgumentNullException("secondary");
			}
			Hashtable associationTable = TypeDescriptor._associationTable;
			if (associationTable != null)
			{
				IList list = (IList)associationTable[primary];
				if (list != null)
				{
					IList list2 = list;
					lock (list2)
					{
						for (int i = list.Count - 1; i >= 0; i--)
						{
							object target = ((WeakReference)list[i]).Target;
							if (target == null || target == secondary)
							{
								list.RemoveAt(i);
							}
						}
					}
				}
			}
		}

		/// <summary>Removes all associations for a primary object.</summary>
		/// <param name="primary">The primary <see cref="T:System.Object" /> in an association.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="primary" /> is null.</exception>
		// Token: 0x060017E2 RID: 6114 RVA: 0x0005E85C File Offset: 0x0005CA5C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void RemoveAssociations(object primary)
		{
			if (primary == null)
			{
				throw new ArgumentNullException("primary");
			}
			Hashtable associationTable = TypeDescriptor._associationTable;
			if (associationTable != null)
			{
				associationTable.Remove(primary);
			}
		}

		/// <summary>Removes a previously added type description provider that is associated with the specified type.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to remove.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x060017E3 RID: 6115 RVA: 0x0005E889 File Offset: 0x0005CA89
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void RemoveProvider(TypeDescriptionProvider provider, Type type)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			TypeDescriptor.NodeRemove(type, provider);
			TypeDescriptor.RaiseRefresh(type);
		}

		/// <summary>Removes a previously added type description provider that is associated with the specified object.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to remove.</param>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x060017E4 RID: 6116 RVA: 0x0005E8BA File Offset: 0x0005CABA
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
		public static void RemoveProvider(TypeDescriptionProvider provider, object instance)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			TypeDescriptor.NodeRemove(instance, provider);
			TypeDescriptor.RaiseRefresh(instance);
		}

		/// <summary>Removes a previously added type description provider that is associated with the specified type.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to remove.</param>
		/// <param name="type">The <see cref="T:System.Type" /> of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x060017E5 RID: 6117 RVA: 0x0005E8E5 File Offset: 0x0005CAE5
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void RemoveProviderTransparent(TypeDescriptionProvider provider, Type type)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			TypeDescriptor.RemoveProvider(provider, type);
		}

		/// <summary>Removes a previously added type description provider that is associated with the specified object.</summary>
		/// <param name="provider">The <see cref="T:System.ComponentModel.TypeDescriptionProvider" /> to remove.</param>
		/// <param name="instance">An instance of the target component.</param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the parameters are null.</exception>
		// Token: 0x060017E6 RID: 6118 RVA: 0x0005E910 File Offset: 0x0005CB10
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static void RemoveProviderTransparent(TypeDescriptionProvider provider, object instance)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			TypeDescriptor.RemoveProvider(provider, instance);
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x0005E938 File Offset: 0x0005CB38
		private static bool ShouldHideMember(MemberDescriptor member, Attribute attribute)
		{
			if (member == null || attribute == null)
			{
				return true;
			}
			Attribute attribute2 = member.Attributes[attribute.GetType()];
			if (attribute2 == null)
			{
				return !attribute.IsDefaultAttribute();
			}
			return !attribute.Match(attribute2);
		}

		/// <summary>Sorts descriptors using the name of the descriptor.</summary>
		/// <param name="infos">An <see cref="T:System.Collections.IList" /> that contains the descriptors to sort. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="infos" /> is null.</exception>
		// Token: 0x060017E8 RID: 6120 RVA: 0x0005E976 File Offset: 0x0005CB76
		public static void SortDescriptorArray(IList infos)
		{
			if (infos == null)
			{
				throw new ArgumentNullException("infos");
			}
			ArrayList.Adapter(infos).Sort(TypeDescriptor.MemberDescriptorComparer.Instance);
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG")]
		internal static void Trace(string message, params object[] args)
		{
		}

		// Token: 0x040013FC RID: 5116
		private static WeakHashtable _providerTable = new WeakHashtable();

		// Token: 0x040013FD RID: 5117
		private static Hashtable _providerTypeTable = new Hashtable();

		// Token: 0x040013FE RID: 5118
		private static volatile Hashtable _defaultProviders = new Hashtable();

		// Token: 0x040013FF RID: 5119
		private static volatile WeakHashtable _associationTable;

		// Token: 0x04001400 RID: 5120
		private static int _metadataVersion;

		// Token: 0x04001401 RID: 5121
		private static int _collisionIndex;

		// Token: 0x04001402 RID: 5122
		private static BooleanSwitch TraceDescriptor = new BooleanSwitch("TypeDescriptor", "Debug TypeDescriptor.");

		// Token: 0x04001403 RID: 5123
		private const int PIPELINE_ATTRIBUTES = 0;

		// Token: 0x04001404 RID: 5124
		private const int PIPELINE_PROPERTIES = 1;

		// Token: 0x04001405 RID: 5125
		private const int PIPELINE_EVENTS = 2;

		// Token: 0x04001406 RID: 5126
		private static readonly Guid[] _pipelineInitializeKeys = new Guid[]
		{
			Guid.NewGuid(),
			Guid.NewGuid(),
			Guid.NewGuid()
		};

		// Token: 0x04001407 RID: 5127
		private static readonly Guid[] _pipelineMergeKeys = new Guid[]
		{
			Guid.NewGuid(),
			Guid.NewGuid(),
			Guid.NewGuid()
		};

		// Token: 0x04001408 RID: 5128
		private static readonly Guid[] _pipelineFilterKeys = new Guid[]
		{
			Guid.NewGuid(),
			Guid.NewGuid(),
			Guid.NewGuid()
		};

		// Token: 0x04001409 RID: 5129
		private static readonly Guid[] _pipelineAttributeFilterKeys = new Guid[]
		{
			Guid.NewGuid(),
			Guid.NewGuid(),
			Guid.NewGuid()
		};

		// Token: 0x0400140A RID: 5130
		private static object _internalSyncObject = new object();

		// Token: 0x020002E2 RID: 738
		private sealed class AttributeProvider : TypeDescriptionProvider
		{
			// Token: 0x060017EB RID: 6123 RVA: 0x0005EA9F File Offset: 0x0005CC9F
			internal AttributeProvider(TypeDescriptionProvider existingProvider, params Attribute[] attrs)
				: base(existingProvider)
			{
				this._attrs = attrs;
			}

			// Token: 0x060017EC RID: 6124 RVA: 0x0005EAAF File Offset: 0x0005CCAF
			public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
			{
				return new TypeDescriptor.AttributeProvider.AttributeTypeDescriptor(this._attrs, base.GetTypeDescriptor(objectType, instance));
			}

			// Token: 0x0400140C RID: 5132
			private Attribute[] _attrs;

			// Token: 0x020002E3 RID: 739
			private class AttributeTypeDescriptor : CustomTypeDescriptor
			{
				// Token: 0x060017ED RID: 6125 RVA: 0x0005EAC4 File Offset: 0x0005CCC4
				internal AttributeTypeDescriptor(Attribute[] attrs, ICustomTypeDescriptor parent)
					: base(parent)
				{
					this._attributeArray = attrs;
				}

				// Token: 0x060017EE RID: 6126 RVA: 0x0005EAD4 File Offset: 0x0005CCD4
				public override AttributeCollection GetAttributes()
				{
					AttributeCollection attributes = base.GetAttributes();
					Attribute[] attributeArray = this._attributeArray;
					Attribute[] array = new Attribute[attributes.Count + attributeArray.Length];
					int count = attributes.Count;
					attributes.CopyTo(array, 0);
					for (int i = 0; i < attributeArray.Length; i++)
					{
						bool flag = false;
						for (int j = 0; j < attributes.Count; j++)
						{
							if (array[j].TypeId.Equals(attributeArray[i].TypeId))
							{
								flag = true;
								array[j] = attributeArray[i];
								break;
							}
						}
						if (!flag)
						{
							array[count++] = attributeArray[i];
						}
					}
					Attribute[] array2;
					if (count < array.Length)
					{
						array2 = new Attribute[count];
						Array.Copy(array, 0, array2, 0, count);
					}
					else
					{
						array2 = array;
					}
					return new AttributeCollection(array2);
				}

				// Token: 0x0400140D RID: 5133
				private Attribute[] _attributeArray;
			}
		}

		// Token: 0x020002E4 RID: 740
		private sealed class ComNativeDescriptionProvider : TypeDescriptionProvider
		{
			// Token: 0x060017EF RID: 6127 RVA: 0x0005EB96 File Offset: 0x0005CD96
			internal ComNativeDescriptionProvider(IComNativeDescriptorHandler handler)
			{
				this._handler = handler;
			}

			// Token: 0x170004EF RID: 1263
			// (get) Token: 0x060017F0 RID: 6128 RVA: 0x0005EBA5 File Offset: 0x0005CDA5
			// (set) Token: 0x060017F1 RID: 6129 RVA: 0x0005EBAD File Offset: 0x0005CDAD
			internal IComNativeDescriptorHandler Handler
			{
				get
				{
					return this._handler;
				}
				set
				{
					this._handler = value;
				}
			}

			// Token: 0x060017F2 RID: 6130 RVA: 0x0005EBB6 File Offset: 0x0005CDB6
			public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
			{
				if (objectType == null)
				{
					throw new ArgumentNullException("objectType");
				}
				if (instance == null)
				{
					return null;
				}
				if (!objectType.IsInstanceOfType(instance))
				{
					throw new ArgumentException("instance");
				}
				return new TypeDescriptor.ComNativeDescriptionProvider.ComNativeTypeDescriptor(this._handler, instance);
			}

			// Token: 0x0400140E RID: 5134
			private IComNativeDescriptorHandler _handler;

			// Token: 0x020002E5 RID: 741
			private sealed class ComNativeTypeDescriptor : ICustomTypeDescriptor
			{
				// Token: 0x060017F3 RID: 6131 RVA: 0x0005EBF1 File Offset: 0x0005CDF1
				internal ComNativeTypeDescriptor(IComNativeDescriptorHandler handler, object instance)
				{
					this._handler = handler;
					this._instance = instance;
				}

				// Token: 0x060017F4 RID: 6132 RVA: 0x0005EC07 File Offset: 0x0005CE07
				AttributeCollection ICustomTypeDescriptor.GetAttributes()
				{
					return this._handler.GetAttributes(this._instance);
				}

				// Token: 0x060017F5 RID: 6133 RVA: 0x0005EC1A File Offset: 0x0005CE1A
				string ICustomTypeDescriptor.GetClassName()
				{
					return this._handler.GetClassName(this._instance);
				}

				// Token: 0x060017F6 RID: 6134 RVA: 0x00009E57 File Offset: 0x00008057
				string ICustomTypeDescriptor.GetComponentName()
				{
					return null;
				}

				// Token: 0x060017F7 RID: 6135 RVA: 0x0005EC2D File Offset: 0x0005CE2D
				TypeConverter ICustomTypeDescriptor.GetConverter()
				{
					return this._handler.GetConverter(this._instance);
				}

				// Token: 0x060017F8 RID: 6136 RVA: 0x0005EC40 File Offset: 0x0005CE40
				EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
				{
					return this._handler.GetDefaultEvent(this._instance);
				}

				// Token: 0x060017F9 RID: 6137 RVA: 0x0005EC53 File Offset: 0x0005CE53
				PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
				{
					return this._handler.GetDefaultProperty(this._instance);
				}

				// Token: 0x060017FA RID: 6138 RVA: 0x0005EC66 File Offset: 0x0005CE66
				object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
				{
					return this._handler.GetEditor(this._instance, editorBaseType);
				}

				// Token: 0x060017FB RID: 6139 RVA: 0x0005EC7A File Offset: 0x0005CE7A
				EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
				{
					return this._handler.GetEvents(this._instance);
				}

				// Token: 0x060017FC RID: 6140 RVA: 0x0005EC8D File Offset: 0x0005CE8D
				EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
				{
					return this._handler.GetEvents(this._instance, attributes);
				}

				// Token: 0x060017FD RID: 6141 RVA: 0x0005ECA1 File Offset: 0x0005CEA1
				PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
				{
					return this._handler.GetProperties(this._instance, null);
				}

				// Token: 0x060017FE RID: 6142 RVA: 0x0005ECB5 File Offset: 0x0005CEB5
				PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
				{
					return this._handler.GetProperties(this._instance, attributes);
				}

				// Token: 0x060017FF RID: 6143 RVA: 0x0005ECC9 File Offset: 0x0005CEC9
				object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
				{
					return this._instance;
				}

				// Token: 0x0400140F RID: 5135
				private IComNativeDescriptorHandler _handler;

				// Token: 0x04001410 RID: 5136
				private object _instance;
			}
		}

		// Token: 0x020002E6 RID: 742
		private sealed class AttributeFilterCacheItem
		{
			// Token: 0x06001800 RID: 6144 RVA: 0x0005ECD1 File Offset: 0x0005CED1
			internal AttributeFilterCacheItem(Attribute[] filter, ICollection filteredMembers)
			{
				this._filter = filter;
				this.FilteredMembers = filteredMembers;
			}

			// Token: 0x06001801 RID: 6145 RVA: 0x0005ECE8 File Offset: 0x0005CEE8
			internal bool IsValid(Attribute[] filter)
			{
				if (this._filter.Length != filter.Length)
				{
					return false;
				}
				for (int i = 0; i < filter.Length; i++)
				{
					if (this._filter[i] != filter[i])
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x04001411 RID: 5137
			private Attribute[] _filter;

			// Token: 0x04001412 RID: 5138
			internal ICollection FilteredMembers;
		}

		// Token: 0x020002E7 RID: 743
		private sealed class FilterCacheItem
		{
			// Token: 0x06001802 RID: 6146 RVA: 0x0005ED22 File Offset: 0x0005CF22
			internal FilterCacheItem(ITypeDescriptorFilterService filterService, ICollection filteredMembers)
			{
				this._filterService = filterService;
				this.FilteredMembers = filteredMembers;
			}

			// Token: 0x06001803 RID: 6147 RVA: 0x0005ED38 File Offset: 0x0005CF38
			internal bool IsValid(ITypeDescriptorFilterService filterService)
			{
				return this._filterService == filterService;
			}

			// Token: 0x04001413 RID: 5139
			private ITypeDescriptorFilterService _filterService;

			// Token: 0x04001414 RID: 5140
			internal ICollection FilteredMembers;
		}

		// Token: 0x020002E8 RID: 744
		private interface IUnimplemented
		{
		}

		// Token: 0x020002E9 RID: 745
		private sealed class MemberDescriptorComparer : IComparer
		{
			// Token: 0x06001804 RID: 6148 RVA: 0x0005ED46 File Offset: 0x0005CF46
			public int Compare(object left, object right)
			{
				return string.Compare(((MemberDescriptor)left).Name, ((MemberDescriptor)right).Name, false, CultureInfo.InvariantCulture);
			}

			// Token: 0x04001415 RID: 5141
			public static readonly TypeDescriptor.MemberDescriptorComparer Instance = new TypeDescriptor.MemberDescriptorComparer();
		}

		// Token: 0x020002EA RID: 746
		private sealed class MergedTypeDescriptor : ICustomTypeDescriptor
		{
			// Token: 0x06001807 RID: 6151 RVA: 0x0005ED75 File Offset: 0x0005CF75
			internal MergedTypeDescriptor(ICustomTypeDescriptor primary, ICustomTypeDescriptor secondary)
			{
				this._primary = primary;
				this._secondary = secondary;
			}

			// Token: 0x06001808 RID: 6152 RVA: 0x0005ED8C File Offset: 0x0005CF8C
			AttributeCollection ICustomTypeDescriptor.GetAttributes()
			{
				AttributeCollection attributeCollection = this._primary.GetAttributes();
				if (attributeCollection == null)
				{
					attributeCollection = this._secondary.GetAttributes();
				}
				return attributeCollection;
			}

			// Token: 0x06001809 RID: 6153 RVA: 0x0005EDB8 File Offset: 0x0005CFB8
			string ICustomTypeDescriptor.GetClassName()
			{
				string text = this._primary.GetClassName();
				if (text == null)
				{
					text = this._secondary.GetClassName();
				}
				return text;
			}

			// Token: 0x0600180A RID: 6154 RVA: 0x0005EDE4 File Offset: 0x0005CFE4
			string ICustomTypeDescriptor.GetComponentName()
			{
				string text = this._primary.GetComponentName();
				if (text == null)
				{
					text = this._secondary.GetComponentName();
				}
				return text;
			}

			// Token: 0x0600180B RID: 6155 RVA: 0x0005EE10 File Offset: 0x0005D010
			TypeConverter ICustomTypeDescriptor.GetConverter()
			{
				TypeConverter typeConverter = this._primary.GetConverter();
				if (typeConverter == null)
				{
					typeConverter = this._secondary.GetConverter();
				}
				return typeConverter;
			}

			// Token: 0x0600180C RID: 6156 RVA: 0x0005EE3C File Offset: 0x0005D03C
			EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
			{
				EventDescriptor eventDescriptor = this._primary.GetDefaultEvent();
				if (eventDescriptor == null)
				{
					eventDescriptor = this._secondary.GetDefaultEvent();
				}
				return eventDescriptor;
			}

			// Token: 0x0600180D RID: 6157 RVA: 0x0005EE68 File Offset: 0x0005D068
			PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
			{
				PropertyDescriptor propertyDescriptor = this._primary.GetDefaultProperty();
				if (propertyDescriptor == null)
				{
					propertyDescriptor = this._secondary.GetDefaultProperty();
				}
				return propertyDescriptor;
			}

			// Token: 0x0600180E RID: 6158 RVA: 0x0005EE94 File Offset: 0x0005D094
			object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
			{
				if (editorBaseType == null)
				{
					throw new ArgumentNullException("editorBaseType");
				}
				object obj = this._primary.GetEditor(editorBaseType);
				if (obj == null)
				{
					obj = this._secondary.GetEditor(editorBaseType);
				}
				return obj;
			}

			// Token: 0x0600180F RID: 6159 RVA: 0x0005EED4 File Offset: 0x0005D0D4
			EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
			{
				EventDescriptorCollection eventDescriptorCollection = this._primary.GetEvents();
				if (eventDescriptorCollection == null)
				{
					eventDescriptorCollection = this._secondary.GetEvents();
				}
				return eventDescriptorCollection;
			}

			// Token: 0x06001810 RID: 6160 RVA: 0x0005EF00 File Offset: 0x0005D100
			EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
			{
				EventDescriptorCollection eventDescriptorCollection = this._primary.GetEvents(attributes);
				if (eventDescriptorCollection == null)
				{
					eventDescriptorCollection = this._secondary.GetEvents(attributes);
				}
				return eventDescriptorCollection;
			}

			// Token: 0x06001811 RID: 6161 RVA: 0x0005EF2C File Offset: 0x0005D12C
			PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
			{
				PropertyDescriptorCollection propertyDescriptorCollection = this._primary.GetProperties();
				if (propertyDescriptorCollection == null)
				{
					propertyDescriptorCollection = this._secondary.GetProperties();
				}
				return propertyDescriptorCollection;
			}

			// Token: 0x06001812 RID: 6162 RVA: 0x0005EF58 File Offset: 0x0005D158
			PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
			{
				PropertyDescriptorCollection propertyDescriptorCollection = this._primary.GetProperties(attributes);
				if (propertyDescriptorCollection == null)
				{
					propertyDescriptorCollection = this._secondary.GetProperties(attributes);
				}
				return propertyDescriptorCollection;
			}

			// Token: 0x06001813 RID: 6163 RVA: 0x0005EF84 File Offset: 0x0005D184
			object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
			{
				object obj = this._primary.GetPropertyOwner(pd);
				if (obj == null)
				{
					obj = this._secondary.GetPropertyOwner(pd);
				}
				return obj;
			}

			// Token: 0x04001416 RID: 5142
			private ICustomTypeDescriptor _primary;

			// Token: 0x04001417 RID: 5143
			private ICustomTypeDescriptor _secondary;
		}

		// Token: 0x020002EB RID: 747
		private sealed class TypeDescriptionNode : TypeDescriptionProvider
		{
			// Token: 0x06001814 RID: 6164 RVA: 0x0005EFAF File Offset: 0x0005D1AF
			internal TypeDescriptionNode(TypeDescriptionProvider provider)
			{
				this.Provider = provider;
			}

			// Token: 0x06001815 RID: 6165 RVA: 0x0005EFC0 File Offset: 0x0005D1C0
			public override object CreateInstance(IServiceProvider provider, Type objectType, Type[] argTypes, object[] args)
			{
				if (objectType == null)
				{
					throw new ArgumentNullException("objectType");
				}
				if (argTypes != null)
				{
					if (args == null)
					{
						throw new ArgumentNullException("args");
					}
					if (argTypes.Length != args.Length)
					{
						throw new ArgumentException(global::SR.GetString("The number of elements in the Type and Object arrays must match."));
					}
				}
				return this.Provider.CreateInstance(provider, objectType, argTypes, args);
			}

			// Token: 0x06001816 RID: 6166 RVA: 0x0005F01C File Offset: 0x0005D21C
			public override IDictionary GetCache(object instance)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				return this.Provider.GetCache(instance);
			}

			// Token: 0x06001817 RID: 6167 RVA: 0x0005F038 File Offset: 0x0005D238
			public override ICustomTypeDescriptor GetExtendedTypeDescriptor(object instance)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				return new TypeDescriptor.TypeDescriptionNode.DefaultExtendedTypeDescriptor(this, instance);
			}

			// Token: 0x06001818 RID: 6168 RVA: 0x0005F054 File Offset: 0x0005D254
			protected internal override IExtenderProvider[] GetExtenderProviders(object instance)
			{
				if (instance == null)
				{
					throw new ArgumentNullException("instance");
				}
				return this.Provider.GetExtenderProviders(instance);
			}

			// Token: 0x06001819 RID: 6169 RVA: 0x0005F070 File Offset: 0x0005D270
			public override string GetFullComponentName(object component)
			{
				if (component == null)
				{
					throw new ArgumentNullException("component");
				}
				return this.Provider.GetFullComponentName(component);
			}

			// Token: 0x0600181A RID: 6170 RVA: 0x0005F08C File Offset: 0x0005D28C
			public override Type GetReflectionType(Type objectType, object instance)
			{
				if (objectType == null)
				{
					throw new ArgumentNullException("objectType");
				}
				return this.Provider.GetReflectionType(objectType, instance);
			}

			// Token: 0x0600181B RID: 6171 RVA: 0x0005F0AF File Offset: 0x0005D2AF
			public override Type GetRuntimeType(Type objectType)
			{
				if (objectType == null)
				{
					throw new ArgumentNullException("objectType");
				}
				return this.Provider.GetRuntimeType(objectType);
			}

			// Token: 0x0600181C RID: 6172 RVA: 0x0005F0D1 File Offset: 0x0005D2D1
			public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
			{
				if (objectType == null)
				{
					throw new ArgumentNullException("objectType");
				}
				if (instance != null && !objectType.IsInstanceOfType(instance))
				{
					throw new ArgumentException("instance");
				}
				return new TypeDescriptor.TypeDescriptionNode.DefaultTypeDescriptor(this, objectType, instance);
			}

			// Token: 0x0600181D RID: 6173 RVA: 0x0005F10B File Offset: 0x0005D30B
			public override bool IsSupportedType(Type type)
			{
				if (type == null)
				{
					throw new ArgumentNullException("type");
				}
				return this.Provider.IsSupportedType(type);
			}

			// Token: 0x04001418 RID: 5144
			internal TypeDescriptor.TypeDescriptionNode Next;

			// Token: 0x04001419 RID: 5145
			internal TypeDescriptionProvider Provider;

			// Token: 0x020002EC RID: 748
			private struct DefaultExtendedTypeDescriptor : ICustomTypeDescriptor
			{
				// Token: 0x0600181E RID: 6174 RVA: 0x0005F12D File Offset: 0x0005D32D
				internal DefaultExtendedTypeDescriptor(TypeDescriptor.TypeDescriptionNode node, object instance)
				{
					this._node = node;
					this._instance = instance;
				}

				// Token: 0x0600181F RID: 6175 RVA: 0x0005F140 File Offset: 0x0005D340
				AttributeCollection ICustomTypeDescriptor.GetAttributes()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedAttributes(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					AttributeCollection attributes = extendedTypeDescriptor.GetAttributes();
					if (attributes == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetAttributes"
						}));
					}
					return attributes;
				}

				// Token: 0x06001820 RID: 6176 RVA: 0x0005F1F8 File Offset: 0x0005D3F8
				string ICustomTypeDescriptor.GetClassName()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedClassName(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					string text = extendedTypeDescriptor.GetClassName();
					if (text == null)
					{
						text = this._instance.GetType().FullName;
					}
					return text;
				}

				// Token: 0x06001821 RID: 6177 RVA: 0x0005F28C File Offset: 0x0005D48C
				string ICustomTypeDescriptor.GetComponentName()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedComponentName(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					return extendedTypeDescriptor.GetComponentName();
				}

				// Token: 0x06001822 RID: 6178 RVA: 0x0005F308 File Offset: 0x0005D508
				TypeConverter ICustomTypeDescriptor.GetConverter()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedConverter(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					TypeConverter converter = extendedTypeDescriptor.GetConverter();
					if (converter == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetConverter"
						}));
					}
					return converter;
				}

				// Token: 0x06001823 RID: 6179 RVA: 0x0005F3C0 File Offset: 0x0005D5C0
				EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedDefaultEvent(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					return extendedTypeDescriptor.GetDefaultEvent();
				}

				// Token: 0x06001824 RID: 6180 RVA: 0x0005F43C File Offset: 0x0005D63C
				PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedDefaultProperty(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					return extendedTypeDescriptor.GetDefaultProperty();
				}

				// Token: 0x06001825 RID: 6181 RVA: 0x0005F4B8 File Offset: 0x0005D6B8
				object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
				{
					if (editorBaseType == null)
					{
						throw new ArgumentNullException("editorBaseType");
					}
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedEditor(this._instance, editorBaseType);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					return extendedTypeDescriptor.GetEditor(editorBaseType);
				}

				// Token: 0x06001826 RID: 6182 RVA: 0x0005F54C File Offset: 0x0005D74C
				EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedEvents(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					EventDescriptorCollection events = extendedTypeDescriptor.GetEvents();
					if (events == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetEvents"
						}));
					}
					return events;
				}

				// Token: 0x06001827 RID: 6183 RVA: 0x0005F604 File Offset: 0x0005D804
				EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedEvents(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					EventDescriptorCollection events = extendedTypeDescriptor.GetEvents(attributes);
					if (events == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetEvents"
						}));
					}
					return events;
				}

				// Token: 0x06001828 RID: 6184 RVA: 0x0005F6BC File Offset: 0x0005D8BC
				PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedProperties(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					PropertyDescriptorCollection properties = extendedTypeDescriptor.GetProperties();
					if (properties == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetProperties"
						}));
					}
					return properties;
				}

				// Token: 0x06001829 RID: 6185 RVA: 0x0005F774 File Offset: 0x0005D974
				PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedProperties(this._instance);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					PropertyDescriptorCollection properties = extendedTypeDescriptor.GetProperties(attributes);
					if (properties == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetProperties"
						}));
					}
					return properties;
				}

				// Token: 0x0600182A RID: 6186 RVA: 0x0005F82C File Offset: 0x0005DA2C
				object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					if (reflectTypeDescriptionProvider != null)
					{
						return reflectTypeDescriptionProvider.GetExtendedPropertyOwner(this._instance, pd);
					}
					ICustomTypeDescriptor extendedTypeDescriptor = provider.GetExtendedTypeDescriptor(this._instance);
					if (extendedTypeDescriptor == null)
					{
						throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
						{
							this._node.Provider.GetType().FullName,
							"GetExtendedTypeDescriptor"
						}));
					}
					object obj = extendedTypeDescriptor.GetPropertyOwner(pd);
					if (obj == null)
					{
						obj = this._instance;
					}
					return obj;
				}

				// Token: 0x0400141A RID: 5146
				private TypeDescriptor.TypeDescriptionNode _node;

				// Token: 0x0400141B RID: 5147
				private object _instance;
			}

			// Token: 0x020002ED RID: 749
			private struct DefaultTypeDescriptor : ICustomTypeDescriptor
			{
				// Token: 0x0600182B RID: 6187 RVA: 0x0005F8B6 File Offset: 0x0005DAB6
				internal DefaultTypeDescriptor(TypeDescriptor.TypeDescriptionNode node, Type objectType, object instance)
				{
					this._node = node;
					this._objectType = objectType;
					this._instance = instance;
				}

				// Token: 0x0600182C RID: 6188 RVA: 0x0005F8D0 File Offset: 0x0005DAD0
				AttributeCollection ICustomTypeDescriptor.GetAttributes()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					AttributeCollection attributeCollection;
					if (reflectTypeDescriptionProvider != null)
					{
						attributeCollection = reflectTypeDescriptionProvider.GetAttributes(this._objectType);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						attributeCollection = typeDescriptor.GetAttributes();
						if (attributeCollection == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetAttributes"
							}));
						}
					}
					return attributeCollection;
				}

				// Token: 0x0600182D RID: 6189 RVA: 0x0005F994 File Offset: 0x0005DB94
				string ICustomTypeDescriptor.GetClassName()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					string text;
					if (reflectTypeDescriptionProvider != null)
					{
						text = reflectTypeDescriptionProvider.GetClassName(this._objectType);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						text = typeDescriptor.GetClassName();
						if (text == null)
						{
							text = this._objectType.FullName;
						}
					}
					return text;
				}

				// Token: 0x0600182E RID: 6190 RVA: 0x0005FA2C File Offset: 0x0005DC2C
				string ICustomTypeDescriptor.GetComponentName()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					string text;
					if (reflectTypeDescriptionProvider != null)
					{
						text = reflectTypeDescriptionProvider.GetComponentName(this._objectType, this._instance);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						text = typeDescriptor.GetComponentName();
					}
					return text;
				}

				// Token: 0x0600182F RID: 6191 RVA: 0x0005FAB8 File Offset: 0x0005DCB8
				TypeConverter ICustomTypeDescriptor.GetConverter()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					TypeConverter typeConverter;
					if (reflectTypeDescriptionProvider != null)
					{
						typeConverter = reflectTypeDescriptionProvider.GetConverter(this._objectType, this._instance);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						typeConverter = typeDescriptor.GetConverter();
						if (typeConverter == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetConverter"
							}));
						}
					}
					return typeConverter;
				}

				// Token: 0x06001830 RID: 6192 RVA: 0x0005FB80 File Offset: 0x0005DD80
				EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					EventDescriptor eventDescriptor;
					if (reflectTypeDescriptionProvider != null)
					{
						eventDescriptor = reflectTypeDescriptionProvider.GetDefaultEvent(this._objectType, this._instance);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						eventDescriptor = typeDescriptor.GetDefaultEvent();
					}
					return eventDescriptor;
				}

				// Token: 0x06001831 RID: 6193 RVA: 0x0005FC0C File Offset: 0x0005DE0C
				PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					PropertyDescriptor propertyDescriptor;
					if (reflectTypeDescriptionProvider != null)
					{
						propertyDescriptor = reflectTypeDescriptionProvider.GetDefaultProperty(this._objectType, this._instance);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						propertyDescriptor = typeDescriptor.GetDefaultProperty();
					}
					return propertyDescriptor;
				}

				// Token: 0x06001832 RID: 6194 RVA: 0x0005FC98 File Offset: 0x0005DE98
				object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
				{
					if (editorBaseType == null)
					{
						throw new ArgumentNullException("editorBaseType");
					}
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					object obj;
					if (reflectTypeDescriptionProvider != null)
					{
						obj = reflectTypeDescriptionProvider.GetEditor(this._objectType, this._instance, editorBaseType);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						obj = typeDescriptor.GetEditor(editorBaseType);
					}
					return obj;
				}

				// Token: 0x06001833 RID: 6195 RVA: 0x0005FD3C File Offset: 0x0005DF3C
				EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					EventDescriptorCollection eventDescriptorCollection;
					if (reflectTypeDescriptionProvider != null)
					{
						eventDescriptorCollection = reflectTypeDescriptionProvider.GetEvents(this._objectType);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						eventDescriptorCollection = typeDescriptor.GetEvents();
						if (eventDescriptorCollection == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetEvents"
							}));
						}
					}
					return eventDescriptorCollection;
				}

				// Token: 0x06001834 RID: 6196 RVA: 0x0005FE00 File Offset: 0x0005E000
				EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					EventDescriptorCollection eventDescriptorCollection;
					if (reflectTypeDescriptionProvider != null)
					{
						eventDescriptorCollection = reflectTypeDescriptionProvider.GetEvents(this._objectType);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						eventDescriptorCollection = typeDescriptor.GetEvents(attributes);
						if (eventDescriptorCollection == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetEvents"
							}));
						}
					}
					return eventDescriptorCollection;
				}

				// Token: 0x06001835 RID: 6197 RVA: 0x0005FEC4 File Offset: 0x0005E0C4
				PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					PropertyDescriptorCollection propertyDescriptorCollection;
					if (reflectTypeDescriptionProvider != null)
					{
						propertyDescriptorCollection = reflectTypeDescriptionProvider.GetProperties(this._objectType);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						propertyDescriptorCollection = typeDescriptor.GetProperties();
						if (propertyDescriptorCollection == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetProperties"
							}));
						}
					}
					return propertyDescriptorCollection;
				}

				// Token: 0x06001836 RID: 6198 RVA: 0x0005FF88 File Offset: 0x0005E188
				PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					PropertyDescriptorCollection propertyDescriptorCollection;
					if (reflectTypeDescriptionProvider != null)
					{
						propertyDescriptorCollection = reflectTypeDescriptionProvider.GetProperties(this._objectType);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						propertyDescriptorCollection = typeDescriptor.GetProperties(attributes);
						if (propertyDescriptorCollection == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetProperties"
							}));
						}
					}
					return propertyDescriptorCollection;
				}

				// Token: 0x06001837 RID: 6199 RVA: 0x0006004C File Offset: 0x0005E24C
				object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
				{
					TypeDescriptionProvider provider = this._node.Provider;
					ReflectTypeDescriptionProvider reflectTypeDescriptionProvider = provider as ReflectTypeDescriptionProvider;
					object obj;
					if (reflectTypeDescriptionProvider != null)
					{
						obj = reflectTypeDescriptionProvider.GetPropertyOwner(this._objectType, this._instance, pd);
					}
					else
					{
						ICustomTypeDescriptor typeDescriptor = provider.GetTypeDescriptor(this._objectType, this._instance);
						if (typeDescriptor == null)
						{
							throw new InvalidOperationException(global::SR.GetString("The type description provider {0} has returned null from {1} which is illegal.", new object[]
							{
								this._node.Provider.GetType().FullName,
								"GetTypeDescriptor"
							}));
						}
						obj = typeDescriptor.GetPropertyOwner(pd);
						if (obj == null)
						{
							obj = this._instance;
						}
					}
					return obj;
				}

				// Token: 0x0400141C RID: 5148
				private TypeDescriptor.TypeDescriptionNode _node;

				// Token: 0x0400141D RID: 5149
				private Type _objectType;

				// Token: 0x0400141E RID: 5150
				private object _instance;
			}
		}

		// Token: 0x020002EE RID: 750
		[TypeDescriptionProvider("System.Windows.Forms.ComponentModel.Com2Interop.ComNativeDescriptor, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		private sealed class TypeDescriptorComObject
		{
		}

		// Token: 0x020002EF RID: 751
		private sealed class TypeDescriptorInterface
		{
		}
	}
}
