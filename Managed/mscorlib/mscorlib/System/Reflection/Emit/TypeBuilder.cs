using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Defines and creates new instances of classes during run time.</summary>
	// Token: 0x02000380 RID: 896
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_TypeBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class TypeBuilder : TypeInfo, _TypeBuilder
	{
		// Token: 0x060028DB RID: 10459 RVA: 0x000912EA File Offset: 0x0008F4EA
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this.attrs;
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x000912F4 File Offset: 0x0008F4F4
		internal TypeBuilder(ModuleBuilder mb, TypeAttributes attr, int table_idx)
		{
			this.parent = null;
			this.attrs = attr;
			this.class_size = 0;
			this.table_idx = table_idx;
			this.tname = ((table_idx == 1) ? "<Module>" : ("type_" + table_idx.ToString()));
			this.nspace = string.Empty;
			this.fullname = TypeIdentifiers.WithoutEscape(this.tname);
			this.pmodule = mb;
		}

		// Token: 0x060028DD RID: 10461 RVA: 0x00091368 File Offset: 0x0008F568
		internal TypeBuilder(ModuleBuilder mb, string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packing_size, int type_size, Type nesting_type)
		{
			this.parent = TypeBuilder.ResolveUserType(parent);
			this.attrs = attr;
			this.class_size = type_size;
			this.packing_size = packing_size;
			this.nesting_type = nesting_type;
			this.check_name("fullname", name);
			if (parent == null && (attr & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic && (attr & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
			{
				throw new InvalidOperationException("Interface must be declared abstract.");
			}
			int num = name.LastIndexOf('.');
			if (num != -1)
			{
				this.tname = name.Substring(num + 1);
				this.nspace = name.Substring(0, num);
			}
			else
			{
				this.tname = name;
				this.nspace = string.Empty;
			}
			if (interfaces != null)
			{
				this.interfaces = new Type[interfaces.Length];
				Array.Copy(interfaces, this.interfaces, interfaces.Length);
			}
			this.pmodule = mb;
			if ((attr & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && parent == null)
			{
				this.parent = typeof(object);
			}
			this.table_idx = mb.get_next_table_index(this, 2, true);
			this.fullname = this.GetFullName();
		}

		/// <summary>Retrieves the dynamic assembly that contains this type definition.</summary>
		/// <returns>Read-only. Retrieves the dynamic assembly that contains this type definition.</returns>
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x060028DE RID: 10462 RVA: 0x0009147B File Offset: 0x0008F67B
		public override Assembly Assembly
		{
			get
			{
				return this.pmodule.Assembly;
			}
		}

		/// <summary>Returns the full name of this type qualified by the display name of the assembly.</summary>
		/// <returns>Read-only. The full name of this type qualified by the display name of the assembly.</returns>
		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x060028DF RID: 10463 RVA: 0x00091488 File Offset: 0x0008F688
		public override string AssemblyQualifiedName
		{
			get
			{
				return this.fullname.DisplayName + ", " + this.Assembly.FullName;
			}
		}

		/// <summary>Retrieves the base type of this type.</summary>
		/// <returns>Read-only. Retrieves the base type of this type.</returns>
		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x060028E0 RID: 10464 RVA: 0x000914AA File Offset: 0x0008F6AA
		public override Type BaseType
		{
			get
			{
				return this.parent;
			}
		}

		/// <summary>Returns the type that declared this type.</summary>
		/// <returns>Read-only. The type that declared this type.</returns>
		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x000914B2 File Offset: 0x0008F6B2
		public override Type DeclaringType
		{
			get
			{
				return this.nesting_type;
			}
		}

		/// <summary>Determines whether this type is derived from a specified type.</summary>
		/// <returns>Read-only. Returns true if this type is the same as the type <paramref name="c" />, or is a subtype of type <paramref name="c" />; otherwise, false.</returns>
		/// <param name="c">A <see cref="T:System.Type" /> that is to be checked. </param>
		// Token: 0x060028E2 RID: 10466 RVA: 0x000914BC File Offset: 0x0008F6BC
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (c == this)
			{
				return false;
			}
			Type baseType = this.parent;
			while (baseType != null)
			{
				if (c == baseType)
				{
					return true;
				}
				baseType = baseType.BaseType;
			}
			return false;
		}

		/// <summary>Returns the underlying system type for this TypeBuilder.</summary>
		/// <returns>Read-only. Returns the underlying system type.</returns>
		/// <exception cref="T:System.InvalidOperationException">This type is an enumeration, but there is no underlying system type. </exception>
		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x00091504 File Offset: 0x0008F704
		public override Type UnderlyingSystemType
		{
			get
			{
				if (this.is_created)
				{
					return this.created.UnderlyingSystemType;
				}
				if (!this.IsEnum)
				{
					return this;
				}
				if (this.underlying_type != null)
				{
					return this.underlying_type;
				}
				throw new InvalidOperationException("Enumeration type is not defined.");
			}
		}

		// Token: 0x060028E4 RID: 10468 RVA: 0x00091544 File Offset: 0x0008F744
		private TypeName GetFullName()
		{
			TypeIdentifier typeIdentifier = TypeIdentifiers.FromInternal(this.tname);
			if (this.nesting_type != null)
			{
				return TypeNames.FromDisplay(this.nesting_type.FullName).NestedName(typeIdentifier);
			}
			if (this.nspace != null && this.nspace.Length > 0)
			{
				return TypeIdentifiers.FromInternal(this.nspace, typeIdentifier);
			}
			return typeIdentifier;
		}

		/// <summary>Retrieves the full path of this type.</summary>
		/// <returns>Read-only. Retrieves the full path of this type.</returns>
		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x000915A6 File Offset: 0x0008F7A6
		public override string FullName
		{
			get
			{
				return this.fullname.DisplayName;
			}
		}

		/// <summary>Retrieves the GUID of this type.</summary>
		/// <returns>Read-only. Retrieves the GUID of this type </returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. </exception>
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x060028E6 RID: 10470 RVA: 0x000915B3 File Offset: 0x0008F7B3
		public override Guid GUID
		{
			get
			{
				this.check_created();
				return this.created.GUID;
			}
		}

		/// <summary>Retrieves the dynamic module that contains this type definition.</summary>
		/// <returns>Read-only. Retrieves the dynamic module that contains this type definition.</returns>
		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x000915C6 File Offset: 0x0008F7C6
		public override Module Module
		{
			get
			{
				return this.pmodule;
			}
		}

		/// <summary>Retrieves the name of this type.</summary>
		/// <returns>Read-only. Retrieves the <see cref="T:System.String" /> name of this type.</returns>
		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060028E8 RID: 10472 RVA: 0x000915CE File Offset: 0x0008F7CE
		public override string Name
		{
			get
			{
				return this.tname;
			}
		}

		/// <summary>Retrieves the namespace where this TypeBuilder is defined.</summary>
		/// <returns>Read-only. Retrieves the namespace where this TypeBuilder is defined.</returns>
		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060028E9 RID: 10473 RVA: 0x000915D6 File Offset: 0x0008F7D6
		public override string Namespace
		{
			get
			{
				return this.nspace;
			}
		}

		/// <summary>Retrieves the packing size of this type.</summary>
		/// <returns>Read-only. Retrieves the packing size of this type.</returns>
		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x060028EA RID: 10474 RVA: 0x000915DE File Offset: 0x0008F7DE
		public PackingSize PackingSize
		{
			get
			{
				return this.packing_size;
			}
		}

		/// <summary>Retrieves the total size of a type.</summary>
		/// <returns>Read-only. Retrieves this type’s total size.</returns>
		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x000915E6 File Offset: 0x0008F7E6
		public int Size
		{
			get
			{
				return this.class_size;
			}
		}

		/// <summary>Returns the type that was used to obtain this type.</summary>
		/// <returns>Read-only. The type that was used to obtain this type.</returns>
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060028EC RID: 10476 RVA: 0x000914B2 File Offset: 0x0008F6B2
		public override Type ReflectedType
		{
			get
			{
				return this.nesting_type;
			}
		}

		/// <summary>Adds declarative security to this type.</summary>
		/// <param name="action">The security action to be taken such as Demand, Assert, and so on. </param>
		/// <param name="pset">The set of permissions the action applies to. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="action" /> is invalid (RequestMinimum, RequestOptional, and RequestRefuse are invalid). </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or- The permission set <paramref name="pset" /> contains an action that was added earlier by AddDeclarativeSecurity. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pset" /> is null. </exception>
		// Token: 0x060028ED RID: 10477 RVA: 0x000915F0 File Offset: 0x0008F7F0
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			if (action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("Request* values are not permitted", "action");
			}
			this.check_not_created();
			if (this.permissions != null)
			{
				RefEmitPermissionSet[] array = this.permissions;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].action == action)
					{
						throw new InvalidOperationException("Multiple permission sets specified with the same SecurityAction.");
					}
				}
				RefEmitPermissionSet[] array2 = new RefEmitPermissionSet[this.permissions.Length + 1];
				this.permissions.CopyTo(array2, 0);
				this.permissions = array2;
			}
			else
			{
				this.permissions = new RefEmitPermissionSet[1];
			}
			this.permissions[this.permissions.Length - 1] = new RefEmitPermissionSet(action, pset.ToXml().ToString());
			this.attrs |= TypeAttributes.HasSecurity;
		}

		/// <summary>Adds an interface that this type implements.</summary>
		/// <param name="interfaceType">The interface that this type implements. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="interfaceType" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x060028EE RID: 10478 RVA: 0x000916D0 File Offset: 0x0008F8D0
		[ComVisible(true)]
		public void AddInterfaceImplementation(Type interfaceType)
		{
			if (interfaceType == null)
			{
				throw new ArgumentNullException("interfaceType");
			}
			this.check_not_created();
			if (this.interfaces != null)
			{
				Type[] array = this.interfaces;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == interfaceType)
					{
						return;
					}
				}
				Type[] array2 = new Type[this.interfaces.Length + 1];
				this.interfaces.CopyTo(array2, 0);
				array2[this.interfaces.Length] = interfaceType;
				this.interfaces = array2;
				return;
			}
			this.interfaces = new Type[1];
			this.interfaces[0] = interfaceType;
		}

		// Token: 0x060028EF RID: 10479 RVA: 0x00091768 File Offset: 0x0008F968
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			this.check_created();
			if (!(this.created == typeof(object)))
			{
				return this.created.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
			}
			if (this.ctors == null)
			{
				return null;
			}
			ConstructorBuilder constructorBuilder = null;
			int num = 0;
			foreach (ConstructorBuilder constructorBuilder2 in this.ctors)
			{
				if (callConvention == CallingConventions.Any || constructorBuilder2.CallingConvention == callConvention)
				{
					constructorBuilder = constructorBuilder2;
					num++;
				}
			}
			if (num == 0)
			{
				return null;
			}
			if (types != null)
			{
				MethodBase[] array2 = new MethodBase[num];
				if (num == 1)
				{
					array2[0] = constructorBuilder;
				}
				else
				{
					num = 0;
					foreach (ConstructorBuilder constructorInfo in this.ctors)
					{
						if (callConvention == CallingConventions.Any || constructorInfo.CallingConvention == callConvention)
						{
							array2[num++] = constructorInfo;
						}
					}
				}
				if (binder == null)
				{
					binder = Type.DefaultBinder;
				}
				return (ConstructorInfo)binder.SelectMethod(bindingAttr, array2, types, modifiers);
			}
			if (num > 1)
			{
				throw new AmbiguousMatchException();
			}
			return constructorBuilder;
		}

		/// <summary>Determines whether a custom attribute is applied to the current type.</summary>
		/// <returns>true if one or more instances of <paramref name="attributeType" />, or an attribute derived from <paramref name="attributeType" />, is defined on this type; otherwise, false.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. Retrieve the type using <see cref="M:System.Type.GetType" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Type" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not defined.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null.</exception>
		// Token: 0x060028F0 RID: 10480 RVA: 0x00091863 File Offset: 0x0008FA63
		[SecuritySafeCritical]
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (!this.is_created)
			{
				throw new NotSupportedException();
			}
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		/// <summary>Returns all the custom attributes defined for this type.</summary>
		/// <returns>Returns an array of objects representing all the custom attributes of this type.</returns>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. Retrieve the type using <see cref="M:System.Type.GetType" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Type" />. </exception>
		// Token: 0x060028F1 RID: 10481 RVA: 0x0009187B File Offset: 0x0008FA7B
		[SecuritySafeCritical]
		public override object[] GetCustomAttributes(bool inherit)
		{
			this.check_created();
			return this.created.GetCustomAttributes(inherit);
		}

		/// <summary>Returns all the custom attributes of the current type that are assignable to a specified type.</summary>
		/// <returns>An array of custom attributes defined on the current type.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. Retrieve the type using <see cref="M:System.Type.GetType" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Type" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">The type must be a type provided by the underlying runtime system.</exception>
		// Token: 0x060028F2 RID: 10482 RVA: 0x0009188F File Offset: 0x0008FA8F
		[SecuritySafeCritical]
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			this.check_created();
			return this.created.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Defines a nested type, given its name.</summary>
		/// <returns>The defined nested type.</returns>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="name" /> is zero or greater than 1023. -or-This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x060028F3 RID: 10483 RVA: 0x000918A4 File Offset: 0x0008FAA4
		public TypeBuilder DefineNestedType(string name)
		{
			return this.DefineNestedType(name, TypeAttributes.NestedPrivate, this.pmodule.assemblyb.corlib_object_type, null);
		}

		/// <summary>Defines a nested type, given its name and attributes.</summary>
		/// <returns>The defined nested type.</returns>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes of the type. </param>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.-or- This type is sealed.-or- This type is an array.-or- This type is an interface, but the nested type is not an interface.-or- The length of <paramref name="name" /> is zero or greater than 1023. -or-This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x060028F4 RID: 10484 RVA: 0x000918BF File Offset: 0x0008FABF
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr)
		{
			return this.DefineNestedType(name, attr, this.pmodule.assemblyb.corlib_object_type, null);
		}

		/// <summary>Defines a nested type, given its name, attributes, and the type that it extends.</summary>
		/// <returns>The defined nested type.</returns>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes of the type. </param>
		/// <param name="parent">The type that the nested type extends. </param>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.-or- This type is sealed.-or- This type is an array.-or- This type is an interface, but the nested type is not an interface.-or- The length of <paramref name="name" /> is zero or greater than 1023. -or-This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x060028F5 RID: 10485 RVA: 0x000918DA File Offset: 0x0008FADA
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent)
		{
			return this.DefineNestedType(name, attr, parent, null);
		}

		// Token: 0x060028F6 RID: 10486 RVA: 0x000918E8 File Offset: 0x0008FAE8
		private TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, Type[] interfaces, PackingSize packSize, int typeSize)
		{
			if (interfaces != null)
			{
				for (int i = 0; i < interfaces.Length; i++)
				{
					if (interfaces[i] == null)
					{
						throw new ArgumentNullException("interfaces");
					}
				}
			}
			TypeBuilder typeBuilder = new TypeBuilder(this.pmodule, name, attr, parent, interfaces, packSize, typeSize, this);
			typeBuilder.fullname = typeBuilder.GetFullName();
			this.pmodule.RegisterTypeName(typeBuilder, typeBuilder.fullname);
			if (this.subtypes != null)
			{
				TypeBuilder[] array = new TypeBuilder[this.subtypes.Length + 1];
				Array.Copy(this.subtypes, array, this.subtypes.Length);
				array[this.subtypes.Length] = typeBuilder;
				this.subtypes = array;
			}
			else
			{
				this.subtypes = new TypeBuilder[1];
				this.subtypes[0] = typeBuilder;
			}
			return typeBuilder;
		}

		/// <summary>Defines a nested type, given its name, attributes, the type that it extends, and the interfaces that it implements.</summary>
		/// <returns>The defined nested type.</returns>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes of the type. </param>
		/// <param name="parent">The type that the nested type extends. </param>
		/// <param name="interfaces">The interfaces that the nested type implements. </param>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.-or- This type is sealed.-or- This type is an array.-or- This type is an interface, but the nested type is not an interface.-or- The length of <paramref name="name" /> is zero or greater than 1023. -or-This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or-An element of the <paramref name="interfaces" /> array is null.</exception>
		// Token: 0x060028F7 RID: 10487 RVA: 0x000919AA File Offset: 0x0008FBAA
		[ComVisible(true)]
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, Type[] interfaces)
		{
			return this.DefineNestedType(name, attr, parent, interfaces, PackingSize.Unspecified, 0);
		}

		/// <summary>Defines a nested type, given its name, attributes, the total size of the type, and the type that it extends.</summary>
		/// <returns>The defined nested type.</returns>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes of the type. </param>
		/// <param name="parent">The type that the nested type extends. </param>
		/// <param name="typeSize">The total size of the type. </param>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.-or- This type is sealed.-or- This type is an array.-or- This type is an interface, but the nested type is not an interface.-or- The length of <paramref name="name" /> is zero or greater than 1023. -or-This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x060028F8 RID: 10488 RVA: 0x000919B9 File Offset: 0x0008FBB9
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, int typeSize)
		{
			return this.DefineNestedType(name, attr, parent, null, PackingSize.Unspecified, typeSize);
		}

		/// <summary>Defines a nested type, given its name, attributes, the type that it extends, and the packing size.</summary>
		/// <returns>The defined nested type.</returns>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attr">The attributes of the type. </param>
		/// <param name="parent">The type that the nested type extends. </param>
		/// <param name="packSize">The packing size of the type. </param>
		/// <exception cref="T:System.ArgumentException">The nested attribute is not specified.-or- This type is sealed.-or- This type is an array.-or- This type is an interface, but the nested type is not an interface.-or- The length of <paramref name="name" /> is zero or greater than 1023. -or-This operation would create a type with a duplicate <see cref="P:System.Reflection.Emit.TypeBuilder.FullName" /> in the current assembly.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x060028F9 RID: 10489 RVA: 0x000919C8 File Offset: 0x0008FBC8
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, PackingSize packSize)
		{
			return this.DefineNestedType(name, attr, parent, null, packSize, 0);
		}

		/// <summary>Defines a nested type, given its name, attributes, size, and the type that it extends.</summary>
		/// <returns>The defined nested type.</returns>
		/// <param name="name">The short name of the type. <paramref name="name" /> cannot contain embedded null values.</param>
		/// <param name="attr">The attributes of the type.</param>
		/// <param name="parent">The type that the nested type extends.</param>
		/// <param name="packSize">The packing size of the type.</param>
		/// <param name="typeSize">The total size of the type.</param>
		// Token: 0x060028FA RID: 10490 RVA: 0x000919D7 File Offset: 0x0008FBD7
		public TypeBuilder DefineNestedType(string name, TypeAttributes attr, Type parent, PackingSize packSize, int typeSize)
		{
			return this.DefineNestedType(name, attr, parent, null, packSize, typeSize);
		}

		/// <summary>Adds a new constructor to the type, with the given attributes and signature.</summary>
		/// <returns>The defined constructor.</returns>
		/// <param name="attributes">The attributes of the constructor. </param>
		/// <param name="callingConvention">The calling convention of the constructor. </param>
		/// <param name="parameterTypes">The parameter types of the constructor. </param>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x060028FB RID: 10491 RVA: 0x000919E7 File Offset: 0x0008FBE7
		[ComVisible(true)]
		public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes)
		{
			return this.DefineConstructor(attributes, callingConvention, parameterTypes, null, null);
		}

		/// <summary>Adds a new constructor to the type, with the given attributes, signature, and custom modifiers.</summary>
		/// <returns>The defined constructor.</returns>
		/// <param name="attributes">The attributes of the constructor. </param>
		/// <param name="callingConvention">The calling convention of the constructor. </param>
		/// <param name="parameterTypes">The parameter types of the constructor. </param>
		/// <param name="requiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify null instead of an array of types. If none of the parameters have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="optionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify null instead of an array of types. If none of the parameters have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <exception cref="T:System.ArgumentException">The size of <paramref name="requiredCustomModifiers" /> or <paramref name="optionalCustomModifiers" /> does not equal the size of <paramref name="parameterTypes" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false.</exception>
		// Token: 0x060028FC RID: 10492 RVA: 0x000919F4 File Offset: 0x0008FBF4
		[ComVisible(true)]
		public ConstructorBuilder DefineConstructor(MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] requiredCustomModifiers, Type[][] optionalCustomModifiers)
		{
			this.check_not_created();
			ConstructorBuilder constructorBuilder = new ConstructorBuilder(this, attributes, callingConvention, parameterTypes, requiredCustomModifiers, optionalCustomModifiers);
			if (this.ctors != null)
			{
				ConstructorBuilder[] array = new ConstructorBuilder[this.ctors.Length + 1];
				Array.Copy(this.ctors, array, this.ctors.Length);
				array[this.ctors.Length] = constructorBuilder;
				this.ctors = array;
			}
			else
			{
				this.ctors = new ConstructorBuilder[1];
				this.ctors[0] = constructorBuilder;
			}
			return constructorBuilder;
		}

		/// <summary>Defines the default constructor. The constructor defined here will simply call the default constructor of the parent.</summary>
		/// <returns>Returns the constructor.</returns>
		/// <param name="attributes">A MethodAttributes object representing the attributes to be applied to the constructor. </param>
		/// <exception cref="T:System.NotSupportedException">The parent type (base type) does not have a default constructor. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false.</exception>
		// Token: 0x060028FD RID: 10493 RVA: 0x00091A6C File Offset: 0x0008FC6C
		[ComVisible(true)]
		public ConstructorBuilder DefineDefaultConstructor(MethodAttributes attributes)
		{
			Type type;
			if (this.parent != null)
			{
				type = this.parent;
			}
			else
			{
				type = this.pmodule.assemblyb.corlib_object_type;
			}
			Type type2 = type;
			type = type.InternalResolve();
			if (type == typeof(object) || type == typeof(ValueType))
			{
				type = type2;
			}
			ConstructorInfo constructor = type.GetConstructor(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			if (constructor == null)
			{
				throw new NotSupportedException("Parent does not have a default constructor. The default constructor must be explicitly defined.");
			}
			ConstructorBuilder constructorBuilder = this.DefineConstructor(attributes, CallingConventions.Standard, Type.EmptyTypes);
			ILGenerator ilgenerator = constructorBuilder.GetILGenerator();
			ilgenerator.Emit(OpCodes.Ldarg_0);
			ilgenerator.Emit(OpCodes.Call, constructor);
			ilgenerator.Emit(OpCodes.Ret);
			return constructorBuilder;
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x00091B28 File Offset: 0x0008FD28
		private void append_method(MethodBuilder mb)
		{
			if (this.methods != null)
			{
				if (this.methods.Length == this.num_methods)
				{
					MethodBuilder[] array = new MethodBuilder[this.methods.Length * 2];
					Array.Copy(this.methods, array, this.num_methods);
					this.methods = array;
				}
			}
			else
			{
				this.methods = new MethodBuilder[1];
			}
			this.methods[this.num_methods] = mb;
			this.num_methods++;
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, and method signature.</summary>
		/// <returns>The defined method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="parameterTypes">The types of the parameters of the method. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.-or- The type of the parent of this method is an interface, and this method is not virtual (Overridable in Visual Basic). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false. </exception>
		// Token: 0x060028FF RID: 10495 RVA: 0x00091BA0 File Offset: 0x0008FDA0
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, CallingConventions.Standard, returnType, parameterTypes);
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, calling convention, and method signature.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the newly defined method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="callingConvention">The calling convention of the method. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="parameterTypes">The types of the parameters of the method. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.-or- The type of the parent of this method is an interface, and this method is not virtual (Overridable in Visual Basic). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false. </exception>
		// Token: 0x06002900 RID: 10496 RVA: 0x00091BB0 File Offset: 0x0008FDB0
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineMethod(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, calling convention, method signature, and custom modifiers.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> object representing the newly added method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="callingConvention">The calling convention of the method. </param>
		/// <param name="returnType">The return type of the method. </param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no required custom modifiers, specify null.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no optional custom modifiers, specify null.</param>
		/// <param name="parameterTypes">The types of the parameters of the method.</param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify null instead of an array of types. If none of the parameters have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify null instead of an array of types. If none of the parameters have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.-or- The type of the parent of this method is an interface, and this method is not virtual (Overridable in Visual Basic). -or-The size of <paramref name="parameterTypeRequiredCustomModifiers" /> or <paramref name="parameterTypeOptionalCustomModifiers" /> does not equal the size of <paramref name="parameterTypes" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false.</exception>
		// Token: 0x06002901 RID: 10497 RVA: 0x00091BD0 File Offset: 0x0008FDD0
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.check_name("name", name);
			this.check_not_created();
			if (base.IsInterface && ((attributes & MethodAttributes.Abstract) == MethodAttributes.PrivateScope || (attributes & MethodAttributes.Virtual) == MethodAttributes.PrivateScope) && (attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				throw new ArgumentException("Interface method must be abstract and virtual.");
			}
			if (returnType == null)
			{
				returnType = this.pmodule.assemblyb.corlib_void_type;
			}
			MethodBuilder methodBuilder = new MethodBuilder(this, name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			this.append_method(methodBuilder);
			return methodBuilder;
		}

		/// <summary>Defines a PInvoke method given its name, the name of the DLL in which the method is defined, the name of the entry point, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, and the PInvoke flags.</summary>
		/// <returns>The defined PInvoke method.</returns>
		/// <param name="name">The name of the PInvoke method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="dllName">The name of the DLL in which the PInvoke method is defined. </param>
		/// <param name="entryName">The name of the entry point in the DLL. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="callingConvention">The method's calling convention. </param>
		/// <param name="returnType">The method's return type. </param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <param name="nativeCallConv">The native calling convention. </param>
		/// <param name="nativeCharSet">The method's native character set. </param>
		/// <exception cref="T:System.ArgumentException">The method is not static.-or- The parent type is an interface.-or- The method is abstract.-or- The method was previously defined.-or- The length of <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x06002902 RID: 10498 RVA: 0x00091C54 File Offset: 0x0008FE54
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethod(name, dllName, entryName, attributes, callingConvention, returnType, null, null, parameterTypes, null, null, nativeCallConv, nativeCharSet);
		}

		/// <summary>Defines a PInvoke method given its name, the name of the DLL in which the method is defined, the name of the entry point, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, the PInvoke flags, and custom modifiers for the parameters and return type.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the defined PInvoke method.</returns>
		/// <param name="name">The name of the PInvoke method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="dllName">The name of the DLL in which the PInvoke method is defined. </param>
		/// <param name="entryName">The name of the entry point in the DLL. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="callingConvention">The method's calling convention. </param>
		/// <param name="returnType">The method's return type. </param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no required custom modifiers, specify null.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no optional custom modifiers, specify null.</param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify null instead of an array of types. If none of the parameters have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify null instead of an array of types. If none of the parameters have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="nativeCallConv">The native calling convention. </param>
		/// <param name="nativeCharSet">The method's native character set. </param>
		/// <exception cref="T:System.ArgumentException">The method is not static.-or- The parent type is an interface.-or- The method is abstract.-or- The method was previously defined.-or- The length of <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is zero. -or-The size of <paramref name="parameterTypeRequiredCustomModifiers" /> or <paramref name="parameterTypeOptionalCustomModifiers" /> does not equal the size of <paramref name="parameterTypes" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" />, <paramref name="dllName" />, or <paramref name="entryName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false.</exception>
		// Token: 0x06002903 RID: 10499 RVA: 0x00091C7C File Offset: 0x0008FE7C
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, string entryName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			this.check_name("name", name);
			this.check_name("dllName", dllName);
			this.check_name("entryName", entryName);
			if ((attributes & MethodAttributes.Abstract) != MethodAttributes.PrivateScope)
			{
				throw new ArgumentException("PInvoke methods must be static and native and cannot be abstract.");
			}
			if (base.IsInterface)
			{
				throw new ArgumentException("PInvoke methods cannot exist on interfaces.");
			}
			this.check_not_created();
			MethodBuilder methodBuilder = new MethodBuilder(this, name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers, dllName, entryName, nativeCallConv, nativeCharSet);
			this.append_method(methodBuilder);
			return methodBuilder;
		}

		/// <summary>Defines a PInvoke method given its name, the name of the DLL in which the method is defined, the attributes of the method, the calling convention of the method, the return type of the method, the types of the parameters of the method, and the PInvoke flags.</summary>
		/// <returns>The defined PInvoke method.</returns>
		/// <param name="name">The name of the PInvoke method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="dllName">The name of the DLL in which the PInvoke method is defined. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="callingConvention">The method's calling convention. </param>
		/// <param name="returnType">The method's return type. </param>
		/// <param name="parameterTypes">The types of the method's parameters. </param>
		/// <param name="nativeCallConv">The native calling convention. </param>
		/// <param name="nativeCharSet">The method's native character set. </param>
		/// <exception cref="T:System.ArgumentException">The method is not static.-or- The parent type is an interface.-or- The method is abstract.-or- The method was previously defined.-or- The length of <paramref name="name" /> or <paramref name="dllName" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="dllName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x06002904 RID: 10500 RVA: 0x00091D04 File Offset: 0x0008FF04
		public MethodBuilder DefinePInvokeMethod(string name, string dllName, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, CallingConvention nativeCallConv, CharSet nativeCharSet)
		{
			return this.DefinePInvokeMethod(name, dllName, name, attributes, callingConvention, returnType, parameterTypes, nativeCallConv, nativeCharSet);
		}

		/// <summary>Adds a new method to the type, with the specified name and method attributes.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the newly defined method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.-or- The type of the parent of this method is an interface, and this method is not virtual (Overridable in Visual Basic). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false. </exception>
		// Token: 0x06002905 RID: 10501 RVA: 0x00091D25 File Offset: 0x0008FF25
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes)
		{
			return this.DefineMethod(name, attributes, CallingConventions.Standard);
		}

		/// <summary>Adds a new method to the type, with the specified name, method attributes, and calling convention.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.MethodBuilder" /> representing the newly defined method.</returns>
		/// <param name="name">The name of the method. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the method. </param>
		/// <param name="callingConvention">The calling convention of the method. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero.-or- The type of the parent of this method is an interface and this method is not virtual (Overridable in Visual Basic). </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false. </exception>
		// Token: 0x06002906 RID: 10502 RVA: 0x00091D30 File Offset: 0x0008FF30
		public MethodBuilder DefineMethod(string name, MethodAttributes attributes, CallingConventions callingConvention)
		{
			return this.DefineMethod(name, attributes, callingConvention, null, null);
		}

		/// <summary>Specifies a given method body that implements a given method declaration, potentially with a different name.</summary>
		/// <param name="methodInfoBody">The method body to be used. This should be a MethodBuilder object. </param>
		/// <param name="methodInfoDeclaration">The method whose declaration is to be used. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="methodInfoBody" /> does not belong to this class. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="methodInfoBody" /> or <paramref name="methodInfoDeclaration" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or- The declaring type of <paramref name="methodInfoBody" /> is not the type represented by this <see cref="T:System.Reflection.Emit.TypeBuilder" />. </exception>
		// Token: 0x06002907 RID: 10503 RVA: 0x00091D40 File Offset: 0x0008FF40
		public void DefineMethodOverride(MethodInfo methodInfoBody, MethodInfo methodInfoDeclaration)
		{
			if (methodInfoBody == null)
			{
				throw new ArgumentNullException("methodInfoBody");
			}
			if (methodInfoDeclaration == null)
			{
				throw new ArgumentNullException("methodInfoDeclaration");
			}
			this.check_not_created();
			if (methodInfoBody.DeclaringType != this)
			{
				throw new ArgumentException("method body must belong to this type");
			}
			if (methodInfoBody is MethodBuilder)
			{
				((MethodBuilder)methodInfoBody).set_override(methodInfoDeclaration);
			}
		}

		/// <summary>Adds a new field to the type, with the given name, attributes, and field type.</summary>
		/// <returns>The defined field.</returns>
		/// <param name="fieldName">The name of the field. <paramref name="fieldName" /> cannot contain embedded nulls. </param>
		/// <param name="type">The type of the field </param>
		/// <param name="attributes">The attributes of the field. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="fieldName" /> is zero.-or- <paramref name="type" /> is System.Void.-or- A total size was specified for the parent class of this field. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fieldName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x06002908 RID: 10504 RVA: 0x00091DA8 File Offset: 0x0008FFA8
		public FieldBuilder DefineField(string fieldName, Type type, FieldAttributes attributes)
		{
			return this.DefineField(fieldName, type, null, null, attributes);
		}

		/// <summary>Adds a new field to the type, with the given name, attributes, field type, and custom modifiers.</summary>
		/// <returns>The defined field.</returns>
		/// <param name="fieldName">The name of the field. <paramref name="fieldName" /> cannot contain embedded nulls. </param>
		/// <param name="type">The type of the field </param>
		/// <param name="requiredCustomModifiers">An array of types representing the required custom modifiers for the field, such as <see cref="T:Microsoft.VisualC.IsConstModifier" />.</param>
		/// <param name="optionalCustomModifiers">An array of types representing the optional custom modifiers for the field, such as <see cref="T:Microsoft.VisualC.IsConstModifier" />.</param>
		/// <param name="attributes">The attributes of the field. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="fieldName" /> is zero.-or- <paramref name="type" /> is System.Void.-or- A total size was specified for the parent class of this field. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fieldName" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x06002909 RID: 10505 RVA: 0x00091DB8 File Offset: 0x0008FFB8
		public FieldBuilder DefineField(string fieldName, Type type, Type[] requiredCustomModifiers, Type[] optionalCustomModifiers, FieldAttributes attributes)
		{
			this.check_name("fieldName", fieldName);
			if (type == typeof(void))
			{
				throw new ArgumentException("Bad field type in defining field.");
			}
			this.check_not_created();
			FieldBuilder fieldBuilder = new FieldBuilder(this, fieldName, type, attributes, requiredCustomModifiers, optionalCustomModifiers);
			if (this.fields != null)
			{
				if (this.fields.Length == this.num_fields)
				{
					FieldBuilder[] array = new FieldBuilder[this.fields.Length * 2];
					Array.Copy(this.fields, array, this.num_fields);
					this.fields = array;
				}
				this.fields[this.num_fields] = fieldBuilder;
				this.num_fields++;
			}
			else
			{
				this.fields = new FieldBuilder[1];
				this.fields[0] = fieldBuilder;
				this.num_fields++;
			}
			if (this.IsEnum && this.underlying_type == null && (attributes & FieldAttributes.Static) == FieldAttributes.PrivateScope)
			{
				this.underlying_type = type;
			}
			return fieldBuilder;
		}

		/// <summary>Adds a new property to the type, with the given name and property signature.</summary>
		/// <returns>The defined property.</returns>
		/// <param name="name">The name of the property. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the property. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="parameterTypes">The types of the parameters of the property. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. -or- Any of the elements of the <paramref name="parameterTypes" /> array is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x0600290A RID: 10506 RVA: 0x00091EAC File Offset: 0x000900AC
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, Type[] parameterTypes)
		{
			return this.DefineProperty(name, attributes, (CallingConventions)0, returnType, null, null, parameterTypes, null, null);
		}

		/// <summary>Adds a new property to the type, with the given name, attributes, calling convention, and property signature.</summary>
		/// <returns>The defined property.</returns>
		/// <param name="name">The name of the property. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the property. </param>
		/// <param name="callingConvention">The calling convention of the property accessors. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="parameterTypes">The types of the parameters of the property.</param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. -or- Any of the elements of the <paramref name="parameterTypes" /> array is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x0600290B RID: 10507 RVA: 0x00091ECC File Offset: 0x000900CC
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes)
		{
			return this.DefineProperty(name, attributes, callingConvention, returnType, null, null, parameterTypes, null, null);
		}

		/// <summary>Adds a new property to the type, with the given name, property signature, and custom modifiers.</summary>
		/// <returns>The defined property.</returns>
		/// <param name="name">The name of the property. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the property. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the property. If the return type has no required custom modifiers, specify null.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the property. If the return type has no optional custom modifiers, specify null.</param>
		/// <param name="parameterTypes">The types of the parameters of the property. </param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify null instead of an array of types. If none of the parameters have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify null instead of an array of types. If none of the parameters have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null-or- Any of the elements of the <paramref name="parameterTypes" /> array is null</exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x0600290C RID: 10508 RVA: 0x00091EEC File Offset: 0x000900EC
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			return this.DefineProperty(name, attributes, (CallingConventions)0, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
		}

		/// <summary>Adds a new property to the type, with the given name, calling convention, property signature, and custom modifiers.</summary>
		/// <returns>The defined property.</returns>
		/// <param name="name">The name of the property. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the property. </param>
		/// <param name="callingConvention">The calling convention of the property accessors. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the property. If the return type has no required custom modifiers, specify null.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the property. If the return type has no optional custom modifiers, specify null.</param>
		/// <param name="parameterTypes">The types of the parameters of the property. </param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify null instead of an array of types. If none of the parameters have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify null instead of an array of types. If none of the parameters have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. -or- Any of the elements of the <paramref name="parameterTypes" /> array is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x0600290D RID: 10509 RVA: 0x00091F10 File Offset: 0x00090110
		public PropertyBuilder DefineProperty(string name, PropertyAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.check_name("name", name);
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentNullException("parameterTypes");
					}
				}
			}
			this.check_not_created();
			PropertyBuilder propertyBuilder = new PropertyBuilder(this, name, attributes, callingConvention, returnType, returnTypeRequiredCustomModifiers, returnTypeOptionalCustomModifiers, parameterTypes, parameterTypeRequiredCustomModifiers, parameterTypeOptionalCustomModifiers);
			if (this.properties != null)
			{
				Array.Resize<PropertyBuilder>(ref this.properties, this.properties.Length + 1);
				this.properties[this.properties.Length - 1] = propertyBuilder;
			}
			else
			{
				this.properties = new PropertyBuilder[] { propertyBuilder };
			}
			return propertyBuilder;
		}

		/// <summary>Defines the initializer for this type.</summary>
		/// <returns>Returns a type initializer.</returns>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x0600290E RID: 10510 RVA: 0x00091FB2 File Offset: 0x000901B2
		[ComVisible(true)]
		public ConstructorBuilder DefineTypeInitializer()
		{
			return this.DefineConstructor(MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard, null);
		}

		// Token: 0x0600290F RID: 10511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern TypeInfo create_runtime_class();

		// Token: 0x06002910 RID: 10512 RVA: 0x00091FC1 File Offset: 0x000901C1
		private bool is_nested_in(Type t)
		{
			while (t != null)
			{
				if (t == this)
				{
					return true;
				}
				t = t.DeclaringType;
			}
			return false;
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x00091FE4 File Offset: 0x000901E4
		private bool has_ctor_method()
		{
			MethodAttributes methodAttributes = MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
			for (int i = 0; i < this.num_methods; i++)
			{
				MethodBuilder methodBuilder = this.methods[i];
				if (methodBuilder.Name == ConstructorInfo.ConstructorName && (methodBuilder.Attributes & methodAttributes) == methodAttributes)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates a <see cref="T:System.Type" /> object for the class. After defining fields and methods on the class, CreateType is called in order to load its Type object.</summary>
		/// <returns>Returns the new <see cref="T:System.Type" /> object for this class.</returns>
		/// <exception cref="T:System.InvalidOperationException">The enclosing type has not been created.-or- This type is non-abstract and contains an abstract method.-or- This type is not an abstract class or an interface and has a method without a method body. </exception>
		/// <exception cref="T:System.NotSupportedException">The type contains invalid Microsoft intermediate language (MSIL) code.-or- The branch target is specified using a 1-byte offset, but the target is at a distance greater than 127 bytes from the branch. </exception>
		/// <exception cref="T:System.TypeLoadException">The type cannot be loaded. For example, it contains a static method that has the calling convention <see cref="F:System.Reflection.CallingConventions.HasThis" />.</exception>
		// Token: 0x06002912 RID: 10514 RVA: 0x00092031 File Offset: 0x00090231
		public Type CreateType()
		{
			return this.CreateTypeInfo();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.TypeInfo" /> object that represents this type.</summary>
		/// <returns>An object that represents this type.</returns>
		// Token: 0x06002913 RID: 10515 RVA: 0x0009203C File Offset: 0x0009023C
		public TypeInfo CreateTypeInfo()
		{
			if (this.createTypeCalled)
			{
				return this.created;
			}
			if (!base.IsInterface && this.parent == null && this != this.pmodule.assemblyb.corlib_object_type && this.FullName != "<Module>")
			{
				this.SetParent(this.pmodule.assemblyb.corlib_object_type);
			}
			if (this.fields != null)
			{
				foreach (FieldBuilder fieldBuilder in this.fields)
				{
					if (!(fieldBuilder == null))
					{
						Type fieldType = fieldBuilder.FieldType;
						if (!fieldBuilder.IsStatic && fieldType is TypeBuilder && fieldType.IsValueType && fieldType != this && this.is_nested_in(fieldType))
						{
							TypeBuilder typeBuilder = (TypeBuilder)fieldType;
							if (!typeBuilder.is_created)
							{
								AppDomain.CurrentDomain.DoTypeResolve(typeBuilder);
								bool is_created = typeBuilder.is_created;
							}
						}
					}
				}
			}
			if (!base.IsInterface && !base.IsValueType && this.ctors == null && this.tname != "<Module>" && ((this.GetAttributeFlagsImpl() & TypeAttributes.Abstract) | TypeAttributes.Sealed) != (TypeAttributes.Abstract | TypeAttributes.Sealed) && !this.has_ctor_method())
			{
				this.DefineDefaultConstructor(MethodAttributes.Public);
			}
			this.createTypeCalled = true;
			if (this.parent != null && this.parent.IsSealed)
			{
				throw new TypeLoadException(string.Concat(new object[] { "Could not load type '", this.FullName, "' from assembly '", this.Assembly, "' because the parent type is sealed." }));
			}
			if (this.parent == this.pmodule.assemblyb.corlib_enum_type && this.methods != null)
			{
				throw new TypeLoadException(string.Concat(new object[] { "Could not load type '", this.FullName, "' from assembly '", this.Assembly, "' because it is an enum with methods." }));
			}
			if (this.interfaces != null)
			{
				foreach (Type type in this.interfaces)
				{
					if (type.IsNestedPrivate && type.Assembly != this.Assembly)
					{
						throw new TypeLoadException(string.Concat(new object[] { "Could not load type '", this.FullName, "' from assembly '", this.Assembly, "' because it is implements the inaccessible interface '", type.FullName, "'." }));
					}
				}
			}
			if (this.methods != null)
			{
				bool flag = !base.IsAbstract;
				for (int j = 0; j < this.num_methods; j++)
				{
					MethodBuilder methodBuilder = this.methods[j];
					if (flag && methodBuilder.IsAbstract)
					{
						throw new InvalidOperationException("Type is concrete but has abstract method " + methodBuilder);
					}
					methodBuilder.check_override();
					methodBuilder.fixup();
				}
			}
			if (this.ctors != null)
			{
				ConstructorBuilder[] array3 = this.ctors;
				for (int i = 0; i < array3.Length; i++)
				{
					array3[i].fixup();
				}
			}
			this.ResolveUserTypes();
			this.created = this.create_runtime_class();
			if (this.created != null)
			{
				return this.created;
			}
			return this;
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x00092384 File Offset: 0x00090584
		private void ResolveUserTypes()
		{
			this.parent = TypeBuilder.ResolveUserType(this.parent);
			TypeBuilder.ResolveUserTypes(this.interfaces);
			if (this.fields != null)
			{
				foreach (FieldBuilder fieldBuilder in this.fields)
				{
					if (fieldBuilder != null)
					{
						fieldBuilder.ResolveUserTypes();
					}
				}
			}
			if (this.methods != null)
			{
				foreach (MethodBuilder methodBuilder in this.methods)
				{
					if (methodBuilder != null)
					{
						methodBuilder.ResolveUserTypes();
					}
				}
			}
			if (this.ctors != null)
			{
				foreach (ConstructorBuilder constructorBuilder in this.ctors)
				{
					if (constructorBuilder != null)
					{
						constructorBuilder.ResolveUserTypes();
					}
				}
			}
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x00092448 File Offset: 0x00090648
		internal static void ResolveUserTypes(Type[] types)
		{
			if (types != null)
			{
				for (int i = 0; i < types.Length; i++)
				{
					types[i] = TypeBuilder.ResolveUserType(types[i]);
				}
			}
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x00092474 File Offset: 0x00090674
		internal static Type ResolveUserType(Type t)
		{
			if (!(t != null) || (!(t.GetType().Assembly != typeof(int).Assembly) && !(t is TypeDelegator)))
			{
				return t;
			}
			t = t.UnderlyingSystemType;
			if (t != null && (t.GetType().Assembly != typeof(int).Assembly || t is TypeDelegator))
			{
				throw new NotSupportedException("User defined subclasses of System.Type are not yet supported.");
			}
			return t;
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x000924FC File Offset: 0x000906FC
		internal void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map)
		{
			if (this.methods != null)
			{
				for (int i = 0; i < this.num_methods; i++)
				{
					this.methods[i].FixupTokens(token_map, member_map);
				}
			}
			if (this.ctors != null)
			{
				ConstructorBuilder[] array = this.ctors;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].FixupTokens(token_map, member_map);
				}
			}
			if (this.subtypes != null)
			{
				TypeBuilder[] array2 = this.subtypes;
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].FixupTokens(token_map, member_map);
				}
			}
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x00092580 File Offset: 0x00090780
		internal void GenerateDebugInfo(ISymbolWriter symbolWriter)
		{
			symbolWriter.OpenNamespace(this.Namespace);
			if (this.methods != null)
			{
				for (int i = 0; i < this.num_methods; i++)
				{
					this.methods[i].GenerateDebugInfo(symbolWriter);
				}
			}
			if (this.ctors != null)
			{
				ConstructorBuilder[] array = this.ctors;
				for (int j = 0; j < array.Length; j++)
				{
					array[j].GenerateDebugInfo(symbolWriter);
				}
			}
			symbolWriter.CloseNamespace();
			if (this.subtypes != null)
			{
				for (int k = 0; k < this.subtypes.Length; k++)
				{
					this.subtypes[k].GenerateDebugInfo(symbolWriter);
				}
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the public and non-public constructors defined for this class, as specified.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the specified constructors defined for this class. If no constructors are defined, an empty array is returned.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> as in InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002919 RID: 10521 RVA: 0x00092615 File Offset: 0x00090815
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetConstructors(bindingAttr);
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x00092634 File Offset: 0x00090834
		internal ConstructorInfo[] GetConstructorsInternal(BindingFlags bindingAttr)
		{
			if (this.ctors == null)
			{
				return new ConstructorInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (ConstructorBuilder constructorBuilder in this.ctors)
			{
				bool flag = false;
				MethodAttributes attributes = constructorBuilder.Attributes;
				if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
				{
					if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
					{
						flag = true;
					}
				}
				else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
				{
					flag = true;
				}
				if (flag)
				{
					flag = false;
					if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
					{
						if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						arrayList.Add(constructorBuilder);
					}
				}
			}
			ConstructorInfo[] array2 = new ConstructorInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		/// <summary>Calling this method always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>This method is not supported. No value is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not supported. </exception>
		// Token: 0x0600291B RID: 10523 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override Type GetElementType()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns the event with the specified name.</summary>
		/// <returns>An <see cref="T:System.Reflection.EventInfo" /> object representing the event declared or inherited by this type with the specified name, or null if there are no matches.</returns>
		/// <param name="name">The name of the event to search for. </param>
		/// <param name="bindingAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values that limits the search. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		// Token: 0x0600291C RID: 10524 RVA: 0x000926D5 File Offset: 0x000908D5
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			this.check_created();
			return this.created.GetEvent(name, bindingAttr);
		}

		/// <summary>Returns the public events declared or inherited by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing the public events declared or inherited by this type. An empty array is returned if there are no public events.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		// Token: 0x0600291D RID: 10525 RVA: 0x00057516 File Offset: 0x00055716
		public override EventInfo[] GetEvents()
		{
			return this.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Returns the public and non-public events that are declared by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing the events declared or inherited by this type that match the specified binding flags. An empty array is returned if there are no matching events.</returns>
		/// <param name="bindingAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values that limits the search.</param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600291E RID: 10526 RVA: 0x000926EA File Offset: 0x000908EA
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetEvents(bindingAttr);
			}
			throw new NotSupportedException();
		}

		/// <summary>Returns the field specified by the given name.</summary>
		/// <returns>Returns the <see cref="T:System.Reflection.FieldInfo" /> object representing the field declared or inherited by this type with the specified name and public or non-public modifier. If there are no matches then null is returned.</returns>
		/// <param name="name">The name of the field to get. </param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> as in InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		// Token: 0x0600291F RID: 10527 RVA: 0x00092708 File Offset: 0x00090908
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			if (this.created != null)
			{
				return this.created.GetField(name, bindingAttr);
			}
			if (this.fields == null)
			{
				return null;
			}
			foreach (FieldBuilder fieldInfo in this.fields)
			{
				if (!(fieldInfo == null) && !(fieldInfo.Name != name))
				{
					bool flag = false;
					FieldAttributes attributes = fieldInfo.Attributes;
					if ((attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							return fieldInfo;
						}
					}
				}
			}
			return null;
		}

		/// <summary>Returns the public and non-public fields that are declared by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.FieldInfo" /> objects representing the public and non-public fields declared or inherited by this type. An empty array is returned if there are no fields, as specified.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002920 RID: 10528 RVA: 0x000927B4 File Offset: 0x000909B4
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			if (this.created != null)
			{
				return this.created.GetFields(bindingAttr);
			}
			if (this.fields == null)
			{
				return new FieldInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (FieldBuilder fieldInfo in this.fields)
			{
				if (!(fieldInfo == null))
				{
					bool flag = false;
					FieldAttributes attributes = fieldInfo.Attributes;
					if ((attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & FieldAttributes.Static) != FieldAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							arrayList.Add(fieldInfo);
						}
					}
				}
			}
			FieldInfo[] array2 = new FieldInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		/// <summary>Returns the interface implemented (directly or indirectly) by this class with the fully qualified name matching the given interface name.</summary>
		/// <returns>Returns a <see cref="T:System.Type" /> object representing the implemented interface. Returns null if no interface matching name is found.</returns>
		/// <param name="name">The name of the interface. </param>
		/// <param name="ignoreCase">If true, the search is case-insensitive. If false, the search is case-sensitive. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		// Token: 0x06002921 RID: 10529 RVA: 0x0009287A File Offset: 0x00090A7A
		public override Type GetInterface(string name, bool ignoreCase)
		{
			this.check_created();
			return this.created.GetInterface(name, ignoreCase);
		}

		/// <summary>Returns an array of all the interfaces implemented on this type and its base types.</summary>
		/// <returns>Returns an array of <see cref="T:System.Type" /> objects representing the implemented interfaces. If none are defined, an empty array is returned.</returns>
		// Token: 0x06002922 RID: 10530 RVA: 0x00092890 File Offset: 0x00090A90
		public override Type[] GetInterfaces()
		{
			if (this.is_created)
			{
				return this.created.GetInterfaces();
			}
			if (this.interfaces != null)
			{
				Type[] array = new Type[this.interfaces.Length];
				this.interfaces.CopyTo(array, 0);
				return array;
			}
			return Type.EmptyTypes;
		}

		/// <summary>Returns all the public and non-public members declared or inherited by this type, as specified.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public members are returned.</returns>
		/// <param name="name">The name of the member. </param>
		/// <param name="type">The type of the member to return. </param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, as in InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		// Token: 0x06002923 RID: 10531 RVA: 0x000928DB File Offset: 0x00090ADB
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			this.check_created();
			return this.created.GetMember(name, type, bindingAttr);
		}

		/// <summary>Returns the members for the public and non-public members declared or inherited by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members declared or inherited by this type. An empty array is returned if there are no matching members.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002924 RID: 10532 RVA: 0x000928F1 File Offset: 0x00090AF1
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			this.check_created();
			return this.created.GetMembers(bindingAttr);
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x00092908 File Offset: 0x00090B08
		private MethodInfo[] GetMethodsByName(string name, BindingFlags bindingAttr, bool ignoreCase, Type reflected_type)
		{
			MethodInfo[] array2;
			if ((bindingAttr & BindingFlags.DeclaredOnly) == BindingFlags.Default && this.parent != null)
			{
				MethodInfo[] array = this.parent.GetMethods(bindingAttr);
				ArrayList arrayList = new ArrayList(array.Length);
				bool flag = (bindingAttr & BindingFlags.FlattenHierarchy) > BindingFlags.Default;
				foreach (MethodInfo methodInfo in array)
				{
					MethodAttributes methodAttributes = methodInfo.Attributes;
					if (!methodInfo.IsStatic || flag)
					{
						MethodAttributes methodAttributes2 = methodAttributes & MethodAttributes.MemberAccessMask;
						bool flag2;
						if (methodAttributes2 != MethodAttributes.Private)
						{
							if (methodAttributes2 != MethodAttributes.Assembly)
							{
								if (methodAttributes2 == MethodAttributes.Public)
								{
									flag2 = (bindingAttr & BindingFlags.Public) > BindingFlags.Default;
								}
								else
								{
									flag2 = (bindingAttr & BindingFlags.NonPublic) > BindingFlags.Default;
								}
							}
							else
							{
								flag2 = (bindingAttr & BindingFlags.NonPublic) > BindingFlags.Default;
							}
						}
						else
						{
							flag2 = false;
						}
						if (flag2)
						{
							arrayList.Add(methodInfo);
						}
					}
				}
				if (this.methods == null)
				{
					array2 = new MethodInfo[arrayList.Count];
					arrayList.CopyTo(array2);
				}
				else
				{
					array2 = new MethodInfo[this.methods.Length + arrayList.Count];
					arrayList.CopyTo(array2, 0);
					this.methods.CopyTo(array2, arrayList.Count);
				}
			}
			else
			{
				array2 = this.methods;
			}
			if (array2 == null)
			{
				return new MethodInfo[0];
			}
			ArrayList arrayList2 = new ArrayList();
			foreach (MethodInfo methodInfo2 in array2)
			{
				if (!(methodInfo2 == null) && (name == null || string.Compare(methodInfo2.Name, name, ignoreCase) == 0))
				{
					bool flag2 = false;
					MethodAttributes methodAttributes = methodInfo2.Attributes;
					if ((methodAttributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag2 = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag2 = true;
					}
					if (flag2)
					{
						flag2 = false;
						if ((methodAttributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag2 = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag2 = true;
						}
						if (flag2)
						{
							arrayList2.Add(methodInfo2);
						}
					}
				}
			}
			MethodInfo[] array4 = new MethodInfo[arrayList2.Count];
			arrayList2.CopyTo(array4);
			return array4;
		}

		/// <summary>Returns all the public and non-public methods declared or inherited by this type, as specified.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MethodInfo" /> objects representing the public and non-public methods defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public methods are returned.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> as in InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002926 RID: 10534 RVA: 0x00092AC4 File Offset: 0x00090CC4
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this.GetMethodsByName(null, bindingAttr, false, this);
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x00092AD0 File Offset: 0x00090CD0
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			this.check_created();
			if (types == null)
			{
				return this.created.GetMethod(name, bindingAttr);
			}
			return this.created.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns the public and non-public nested types that are declared by this type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the nested type that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the nested type to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to conduct a case-sensitive search for public methods. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		// Token: 0x06002928 RID: 10536 RVA: 0x00092B00 File Offset: 0x00090D00
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			this.check_created();
			if (this.subtypes == null)
			{
				return null;
			}
			foreach (TypeBuilder typeBuilder in this.subtypes)
			{
				if (typeBuilder.is_created)
				{
					if ((typeBuilder.attrs & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic)
					{
						if ((bindingAttr & BindingFlags.Public) == BindingFlags.Default)
						{
							goto IL_0055;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) == BindingFlags.Default)
					{
						goto IL_0055;
					}
					if (typeBuilder.Name == name)
					{
						return typeBuilder.created;
					}
				}
				IL_0055:;
			}
			return null;
		}

		/// <summary>Returns the public and non-public nested types that are declared or inherited by this type.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the types nested within the current <see cref="T:System.Type" /> that match the specified binding constraints.An empty array of type <see cref="T:System.Type" />, if no types are nested within the current <see cref="T:System.Type" />, or if none of the nested types match the binding constraints.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, as in InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002929 RID: 10537 RVA: 0x00092B70 File Offset: 0x00090D70
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			if (!this.is_created)
			{
				throw new NotSupportedException();
			}
			ArrayList arrayList = new ArrayList();
			if (this.subtypes == null)
			{
				return Type.EmptyTypes;
			}
			foreach (TypeBuilder typeBuilder in this.subtypes)
			{
				bool flag = false;
				if ((typeBuilder.attrs & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic)
				{
					if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
					{
						flag = true;
					}
				}
				else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
				{
					flag = true;
				}
				if (flag)
				{
					arrayList.Add(typeBuilder);
				}
			}
			Type[] array2 = new Type[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		/// <summary>Returns all the public and non-public properties declared or inherited by this type, as specified.</summary>
		/// <returns>Returns an array of PropertyInfo objects representing the public and non-public properties defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public properties are returned.</returns>
		/// <param name="bindingAttr">This invocation attribute. This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600292A RID: 10538 RVA: 0x00092C00 File Offset: 0x00090E00
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			if (this.is_created)
			{
				return this.created.GetProperties(bindingAttr);
			}
			if (this.properties == null)
			{
				return new PropertyInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (PropertyBuilder propertyInfo in this.properties)
			{
				bool flag = false;
				MethodInfo methodInfo = propertyInfo.GetGetMethod(true);
				if (methodInfo == null)
				{
					methodInfo = propertyInfo.GetSetMethod(true);
				}
				if (!(methodInfo == null))
				{
					MethodAttributes attributes = methodInfo.Attributes;
					if ((attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public)
					{
						if ((bindingAttr & BindingFlags.Public) != BindingFlags.Default)
						{
							flag = true;
						}
					}
					else if ((bindingAttr & BindingFlags.NonPublic) != BindingFlags.Default)
					{
						flag = true;
					}
					if (flag)
					{
						flag = false;
						if ((attributes & MethodAttributes.Static) != MethodAttributes.PrivateScope)
						{
							if ((bindingAttr & BindingFlags.Static) != BindingFlags.Default)
							{
								flag = true;
							}
						}
						else if ((bindingAttr & BindingFlags.Instance) != BindingFlags.Default)
						{
							flag = true;
						}
						if (flag)
						{
							arrayList.Add(propertyInfo);
						}
					}
				}
			}
			PropertyInfo[] array2 = new PropertyInfo[arrayList.Count];
			arrayList.CopyTo(array2);
			return array2;
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x00092CDF File Offset: 0x00090EDF
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x00092CE7 File Offset: 0x00090EE7
		protected override bool HasElementTypeImpl()
		{
			return this.is_created && this.created.HasElementType;
		}

		/// <summary>Invokes the specified member. The method that is to be invoked must be accessible and provide the most specific match with the specified argument list, under the constraints of the specified binder and invocation attributes.</summary>
		/// <returns>Returns the return value of the invoked member.</returns>
		/// <param name="name">The name of the member to invoke. This can be a constructor, method, property, or field. A suitable invocation attribute must be specified. Note that it is possible to invoke the default member of a class by passing an empty string as the name of the member. </param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from BindingFlags. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects using reflection. If binder is null, the default binder is used. See <see cref="T:System.Reflection.Binder" />. </param>
		/// <param name="target">The object on which to invoke the specified member. If the member is static, this parameter is ignored. </param>
		/// <param name="args">An argument list. This is an array of Objects that contains the number, order, and type of the parameters of the member to be invoked. If there are no parameters this should be null. </param>
		/// <param name="modifiers">An array of the same length as <paramref name="args" /> with elements that represent the attributes associated with the arguments of the member to be invoked. A parameter has attributes associated with it in the metadata. They are used by various interoperability services. See the metadata specs for more details. </param>
		/// <param name="culture">An instance of CultureInfo used to govern the coercion of types. If this is null, the CultureInfo for the current thread is used. (Note that this is necessary to, for example, convert a String that represents 1000 to a Double value, since 1000 is represented differently by different cultures.) </param>
		/// <param name="namedParameters">Each parameter in the <paramref name="namedParameters" /> array gets the value in the corresponding element in the <paramref name="args" /> array. If the length of <paramref name="args" /> is greater than the length of <paramref name="namedParameters" />, the remaining argument values are passed in order. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported for incomplete types. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600292D RID: 10541 RVA: 0x00092D00 File Offset: 0x00090F00
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			this.check_created();
			return this.created.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x0600292E RID: 10542 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x0600292F RID: 10543 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x06002930 RID: 10544 RVA: 0x00057B79 File Offset: 0x00055D79
		protected override bool IsCOMObjectImpl()
		{
			return (this.GetAttributeFlagsImpl() & TypeAttributes.Import) > TypeAttributes.NotPublic;
		}

		// Token: 0x06002931 RID: 10545 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06002932 RID: 10546 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06002933 RID: 10547 RVA: 0x00092D2C File Offset: 0x00090F2C
		protected override bool IsValueTypeImpl()
		{
			if (this == this.pmodule.assemblyb.corlib_value_type || this == this.pmodule.assemblyb.corlib_enum_type)
			{
				return false;
			}
			Type baseType = this.parent;
			while (baseType != null)
			{
				if (baseType == this.pmodule.assemblyb.corlib_value_type)
				{
					return true;
				}
				baseType = baseType.BaseType;
			}
			return false;
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a one-dimensional array of the current type, with a lower bound of zero.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a one-dimensional array type whose element type is the current type, with a lower bound of zero.</returns>
		// Token: 0x06002934 RID: 10548 RVA: 0x000848C1 File Offset: 0x00082AC1
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents an array of the current type, with the specified number of dimensions.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents a one-dimensional array of the current type.</returns>
		/// <param name="rank">The number of dimensions for the array. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is not a valid array dimension.</exception>
		// Token: 0x06002935 RID: 10549 RVA: 0x000848CA File Offset: 0x00082ACA
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the current type when passed as a ref parameter (ByRef in Visual Basic).</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the current type when passed as a ref parameter (ByRef in Visual Basic).</returns>
		// Token: 0x06002936 RID: 10550 RVA: 0x000848DD File Offset: 0x00082ADD
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		/// <summary>Substitutes the elements of an array of types for the type parameters of the current generic type definition, and returns the resulting constructed type.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the constructed type formed by substituting the elements of <paramref name="typeArguments" /> for the type parameters of the current generic type. </returns>
		/// <param name="typeArguments">An array of types to be substituted for the type parameters of the current generic type definition.</param>
		/// <exception cref="T:System.InvalidOperationException">The current type does not represent the definition of a generic type. That is, <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> returns false. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeArguments" /> is null.-or- Any element of <paramref name="typeArguments" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">Any element of <paramref name="typeArguments" /> does not satisfy the constraints specified for the corresponding type parameter of the current generic type. </exception>
		// Token: 0x06002937 RID: 10551 RVA: 0x00092DA0 File Offset: 0x00090FA0
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			if (!this.IsGenericTypeDefinition)
			{
				throw new InvalidOperationException("not a generic type definition");
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			if (this.generic_params.Length != typeArguments.Length)
			{
				throw new ArgumentException(string.Format("The type or method has {0} generic parameter(s) but {1} generic argument(s) where provided. A generic argument must be provided for each generic parameter.", this.generic_params.Length, typeArguments.Length), "typeArguments");
			}
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i] == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			Type[] array = new Type[typeArguments.Length];
			typeArguments.CopyTo(array, 0);
			return this.pmodule.assemblyb.MakeGenericType(this, array);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the type of an unmanaged pointer to the current type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of an unmanaged pointer to the current type.</returns>
		// Token: 0x06002938 RID: 10552 RVA: 0x000848E5 File Offset: 0x00082AE5
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		/// <summary>Not supported in dynamic modules.</summary>
		/// <returns>Read-only.</returns>
		/// <exception cref="T:System.NotSupportedException">Not supported in dynamic modules. </exception>
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002939 RID: 10553 RVA: 0x00092E4E File Offset: 0x0009104E
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				this.check_created();
				return this.created.TypeHandle;
			}
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false.</exception>
		// Token: 0x0600293A RID: 10554 RVA: 0x00092E64 File Offset: 0x00091064
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.InteropServices.StructLayoutAttribute")
			{
				byte[] data = customBuilder.Data;
				int num = (int)data[2] | ((int)data[3] << 8);
				this.attrs &= ~TypeAttributes.LayoutMask;
				switch (num)
				{
				case 0:
					this.attrs |= TypeAttributes.SequentialLayout;
					goto IL_00A5;
				case 2:
					this.attrs |= TypeAttributes.ExplicitLayout;
					goto IL_00A5;
				case 3:
					this.attrs |= TypeAttributes.NotPublic;
					goto IL_00A5;
				}
				throw new Exception("Error in customattr");
				IL_00A5:
				Type type = ((customBuilder.Ctor is ConstructorBuilder) ? ((ConstructorBuilder)customBuilder.Ctor).parameters[0] : customBuilder.Ctor.GetParametersInternal()[0].ParameterType);
				int num2 = 6;
				if (type.FullName == "System.Int16")
				{
					num2 = 4;
				}
				int num3 = (int)data[num2++];
				num3 |= (int)data[num2++] << 8;
				for (int i = 0; i < num3; i++)
				{
					num2++;
					int num4;
					if (data[num2++] == 85)
					{
						num4 = CustomAttributeBuilder.decode_len(data, num2, out num2);
						CustomAttributeBuilder.string_from_bytes(data, num2, num4);
						num2 += num4;
					}
					num4 = CustomAttributeBuilder.decode_len(data, num2, out num2);
					string text = CustomAttributeBuilder.string_from_bytes(data, num2, num4);
					num2 += num4;
					int num5 = (int)data[num2++];
					num5 |= (int)data[num2++] << 8;
					num5 |= (int)data[num2++] << 16;
					num5 |= (int)data[num2++] << 24;
					if (!(text == "CharSet"))
					{
						if (!(text == "Pack"))
						{
							if (text == "Size")
							{
								this.class_size = num5;
							}
						}
						else
						{
							this.packing_size = (PackingSize)num5;
						}
					}
					else
					{
						switch (num5)
						{
						case 1:
						case 2:
							this.attrs &= ~TypeAttributes.StringFormatMask;
							break;
						case 3:
							this.attrs &= ~TypeAttributes.AutoClass;
							this.attrs |= TypeAttributes.UnicodeClass;
							break;
						case 4:
							this.attrs &= ~TypeAttributes.UnicodeClass;
							this.attrs |= TypeAttributes.AutoClass;
							break;
						}
					}
				}
				return;
			}
			if (fullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
			{
				this.attrs |= TypeAttributes.SpecialName;
				return;
			}
			if (fullName == "System.SerializableAttribute")
			{
				this.attrs |= TypeAttributes.Serializable;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.ComImportAttribute")
			{
				this.attrs |= TypeAttributes.Import;
				return;
			}
			if (fullName == "System.Security.SuppressUnmanagedCodeSecurityAttribute")
			{
				this.attrs |= TypeAttributes.HasSecurity;
			}
			if (this.cattrs != null)
			{
				CustomAttributeBuilder[] array = new CustomAttributeBuilder[this.cattrs.Length + 1];
				this.cattrs.CopyTo(array, 0);
				array[this.cattrs.Length] = customBuilder;
				this.cattrs = array;
				return;
			}
			this.cattrs = new CustomAttributeBuilder[1];
			this.cattrs[0] = customBuilder;
		}

		/// <summary>Sets a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="binaryAttribute">A byte blob representing the attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false.</exception>
		// Token: 0x0600293B RID: 10555 RVA: 0x00093198 File Offset: 0x00091398
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		/// <summary>Adds a new event to the type, with the given name, attributes and event type.</summary>
		/// <returns>The defined event.</returns>
		/// <param name="name">The name of the event. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="attributes">The attributes of the event. </param>
		/// <param name="eventtype">The type of the event. </param>
		/// <exception cref="T:System.ArgumentException">The length of <paramref name="name" /> is zero. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="eventtype" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x0600293C RID: 10556 RVA: 0x000931A8 File Offset: 0x000913A8
		public EventBuilder DefineEvent(string name, EventAttributes attributes, Type eventtype)
		{
			this.check_name("name", name);
			if (eventtype == null)
			{
				throw new ArgumentNullException("type");
			}
			this.check_not_created();
			EventBuilder eventBuilder = new EventBuilder(this, name, attributes, eventtype);
			if (this.events != null)
			{
				EventBuilder[] array = new EventBuilder[this.events.Length + 1];
				Array.Copy(this.events, array, this.events.Length);
				array[this.events.Length] = eventBuilder;
				this.events = array;
			}
			else
			{
				this.events = new EventBuilder[1];
				this.events[0] = eventBuilder;
			}
			return eventBuilder;
		}

		/// <summary>Defines initialized data field in the .sdata section of the portable executable (PE) file.</summary>
		/// <returns>A field to reference the data.</returns>
		/// <param name="name">The name used to refer to the data. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="data">The blob of data. </param>
		/// <param name="attributes">The attributes for the field. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="name" /> is zero.-or- The size of the data is less than or equal to zero, or greater than or equal to 0x3f0000. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> or <paramref name="data" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has been previously called. </exception>
		// Token: 0x0600293D RID: 10557 RVA: 0x0009323B File Offset: 0x0009143B
		public FieldBuilder DefineInitializedData(string name, byte[] data, FieldAttributes attributes)
		{
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			FieldBuilder fieldBuilder = this.DefineUninitializedData(name, data.Length, attributes);
			fieldBuilder.SetRVAData(data);
			return fieldBuilder;
		}

		/// <summary>Defines an uninitialized data field in the .sdata section of the portable executable (PE) file.</summary>
		/// <returns>A field to reference the data.</returns>
		/// <param name="name">The name used to refer to the data. <paramref name="name" /> cannot contain embedded nulls. </param>
		/// <param name="size">The size of the data field. </param>
		/// <param name="attributes">The attributes for the field. </param>
		/// <exception cref="T:System.ArgumentException">Length of <paramref name="name" /> is zero.-or- <paramref name="size" /> is less than or equal to zero, or greater than or equal to 0x003f0000. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x0600293E RID: 10558 RVA: 0x00093260 File Offset: 0x00091460
		public FieldBuilder DefineUninitializedData(string name, int size, FieldAttributes attributes)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal", "name");
			}
			if (size <= 0 || size > 4128768)
			{
				throw new ArgumentException("Data size must be > 0 and < 0x3f0000");
			}
			this.check_not_created();
			string text = "$ArrayType$" + size;
			TypeIdentifier typeIdentifier = TypeIdentifiers.WithoutEscape(text);
			Type type = this.pmodule.GetRegisteredType(this.fullname.NestedName(typeIdentifier));
			if (type == null)
			{
				TypeBuilder typeBuilder = this.DefineNestedType(text, TypeAttributes.Public | TypeAttributes.NestedPublic | TypeAttributes.ExplicitLayout | TypeAttributes.Sealed, this.pmodule.assemblyb.corlib_value_type, null, PackingSize.Size1, size);
				typeBuilder.CreateType();
				type = typeBuilder;
			}
			return this.DefineField(name, type, attributes | FieldAttributes.Static | FieldAttributes.HasFieldRVA);
		}

		/// <summary>Returns the type token of this type.</summary>
		/// <returns>Read-only. Returns the TypeToken of this type.</returns>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.</exception>
		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x0600293F RID: 10559 RVA: 0x00093322 File Offset: 0x00091522
		public TypeToken TypeToken
		{
			get
			{
				return new TypeToken(33554432 | this.table_idx);
			}
		}

		/// <summary>Sets the base type of the type currently under construction.</summary>
		/// <param name="parent">The new base type. </param>
		/// <exception cref="T:System.InvalidOperationException">The type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-<paramref name="parent" /> is null, and the current instance represents an interface whose attributes do not include <see cref="F:System.Reflection.TypeAttributes.Abstract" />.-or-For the current dynamic type, the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> property is true, but the <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericTypeDefinition" /> property is false. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="parent" /> is an interface. This exception condition is new in the .NET Framework version 2.0. </exception>
		// Token: 0x06002940 RID: 10560 RVA: 0x00093338 File Offset: 0x00091538
		public void SetParent(Type parent)
		{
			this.check_not_created();
			if (parent == null)
			{
				if ((this.attrs & TypeAttributes.ClassSemanticsMask) != TypeAttributes.NotPublic)
				{
					if ((this.attrs & TypeAttributes.Abstract) == TypeAttributes.NotPublic)
					{
						throw new InvalidOperationException("Interface must be declared abstract.");
					}
					this.parent = null;
				}
				else
				{
					this.parent = typeof(object);
				}
			}
			else
			{
				this.parent = parent;
			}
			this.parent = TypeBuilder.ResolveUserType(this.parent);
		}

		// Token: 0x06002941 RID: 10561 RVA: 0x000933AB File Offset: 0x000915AB
		internal int get_next_table_index(object obj, int table, bool inc)
		{
			return this.pmodule.get_next_table_index(obj, table, inc);
		}

		/// <summary>Returns an interface mapping for the requested interface.</summary>
		/// <returns>Returns the requested interface mapping.</returns>
		/// <param name="interfaceType">The <see cref="T:System.Type" /> of the interface for which the mapping is to be retrieved. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not implemented for incomplete types. </exception>
		// Token: 0x06002942 RID: 10562 RVA: 0x000933BB File Offset: 0x000915BB
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			if (this.created == null)
			{
				throw new NotSupportedException("This method is not implemented for incomplete types.");
			}
			return this.created.GetInterfaceMap(interfaceType);
		}

		// Token: 0x06002943 RID: 10563 RVA: 0x000933E2 File Offset: 0x000915E2
		internal override Type InternalResolve()
		{
			this.check_created();
			return this.created;
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000933E2 File Offset: 0x000915E2
		internal override Type RuntimeResolve()
		{
			this.check_created();
			return this.created;
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002945 RID: 10565 RVA: 0x000933F0 File Offset: 0x000915F0
		internal bool is_created
		{
			get
			{
				return this.createTypeCalled;
			}
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x00085D62 File Offset: 0x00083F62
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x06002947 RID: 10567 RVA: 0x000933F8 File Offset: 0x000915F8
		private void check_not_created()
		{
			if (this.is_created)
			{
				throw new InvalidOperationException("Unable to change after type has been created.");
			}
		}

		// Token: 0x06002948 RID: 10568 RVA: 0x0009340D File Offset: 0x0009160D
		private void check_created()
		{
			if (!this.is_created)
			{
				throw this.not_supported();
			}
		}

		// Token: 0x06002949 RID: 10569 RVA: 0x0009341E File Offset: 0x0009161E
		private void check_name(string argName, string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(argName);
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Empty name is not legal", argName);
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException("Illegal name", argName);
			}
		}

		/// <summary>Returns the name of the type excluding the namespace.</summary>
		/// <returns>Read-only. The name of the type excluding the namespace.</returns>
		// Token: 0x0600294A RID: 10570 RVA: 0x00093453 File Offset: 0x00091653
		public override string ToString()
		{
			return this.FullName;
		}

		/// <summary>Gets a value that indicates whether a specified <see cref="T:System.Type" /> can be assigned to this object.</summary>
		/// <returns>true if the <paramref name="c" /> parameter and the current type represent the same type, or if the current type is in the inheritance hierarchy of <paramref name="c" />, or if the current type is an interface that <paramref name="c" /> supports. false if none of these conditions are valid, or if <paramref name="c" /> is null.</returns>
		/// <param name="c">The object to test. </param>
		// Token: 0x0600294B RID: 10571 RVA: 0x0009345B File Offset: 0x0009165B
		[MonoTODO]
		public override bool IsAssignableFrom(Type c)
		{
			return base.IsAssignableFrom(c);
		}

		// Token: 0x0600294C RID: 10572 RVA: 0x00093464 File Offset: 0x00091664
		[MonoTODO("arrays")]
		internal bool IsAssignableTo(Type c)
		{
			if (c == this)
			{
				return true;
			}
			if (c.IsInterface)
			{
				if (this.parent != null && this.is_created && c.IsAssignableFrom(this.parent))
				{
					return true;
				}
				if (this.interfaces == null)
				{
					return false;
				}
				foreach (Type type in this.interfaces)
				{
					if (c.IsAssignableFrom(type))
					{
						return true;
					}
				}
				if (!this.is_created)
				{
					return false;
				}
			}
			if (this.parent == null)
			{
				return c == typeof(object);
			}
			return c.IsAssignableFrom(this.parent);
		}

		/// <summary>Returns a value that indicates whether the current dynamic type has been created.</summary>
		/// <returns>true if the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method has been called; otherwise, false. </returns>
		// Token: 0x0600294D RID: 10573 RVA: 0x0009350D File Offset: 0x0009170D
		public bool IsCreated()
		{
			return this.is_created;
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects representing the type arguments of a generic type or the type parameters of a generic type definition.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects. The elements of the array represent the type arguments of a generic type or the type parameters of a generic type definition.</returns>
		// Token: 0x0600294E RID: 10574 RVA: 0x00093518 File Offset: 0x00091718
		public override Type[] GetGenericArguments()
		{
			if (this.generic_params == null)
			{
				return null;
			}
			Type[] array = new Type[this.generic_params.Length];
			this.generic_params.CopyTo(array, 0);
			return array;
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a generic type definition from which the current type can be obtained.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a generic type definition from which the current type can be obtained.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type is not generic. That is, <see cref="P:System.Reflection.Emit.TypeBuilder.IsGenericType" /> returns false.</exception>
		// Token: 0x0600294F RID: 10575 RVA: 0x0009354B File Offset: 0x0009174B
		public override Type GetGenericTypeDefinition()
		{
			if (this.generic_params == null)
			{
				throw new InvalidOperationException("Type is not generic");
			}
			return this;
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x00093561 File Offset: 0x00091761
		public override bool ContainsGenericParameters
		{
			get
			{
				return this.generic_params != null;
			}
		}

		/// <summary>Gets a value indicating whether the current type is a generic type parameter.</summary>
		/// <returns>true if the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> object represents a generic type parameter; otherwise, false.</returns>
		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06002951 RID: 10577 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates the covariance and special constraints of the current generic type parameter. </summary>
		/// <returns>A bitwise combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> values that describes the covariance and special constraints of the current generic type parameter.</returns>
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06002952 RID: 10578 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				return GenericParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> represents a generic type definition from which other generic types can be constructed.</summary>
		/// <returns>true if this <see cref="T:System.Reflection.Emit.TypeBuilder" /> object represents a generic type definition; otherwise, false.</returns>
		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x00093561 File Offset: 0x00091761
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return this.generic_params != null;
			}
		}

		/// <summary>Gets a value indicating whether the current type is a generic type. </summary>
		/// <returns>true if the type represented by the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> object is generic; otherwise, false.</returns>
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002954 RID: 10580 RVA: 0x0009356C File Offset: 0x0009176C
		public override bool IsGenericType
		{
			get
			{
				return this.IsGenericTypeDefinition;
			}
		}

		/// <summary>Gets the position of a type parameter in the type parameter list of the generic type that declared the parameter.</summary>
		/// <returns>If the current <see cref="T:System.Reflection.Emit.TypeBuilder" /> object represents a generic type parameter, the position of the type parameter in the type parameter list of the generic type that declared the parameter; otherwise, undefined.</returns>
		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x00015ED5 File Offset: 0x000140D5
		[MonoTODO]
		public override int GenericParameterPosition
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the method that declared the current generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> that represents the method that declared the current type, if the current type is a generic type parameter; otherwise, null.</returns>
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06002956 RID: 10582 RVA: 0x0000A42E File Offset: 0x0000862E
		public override MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		/// <summary>Defines the generic type parameters for the current type, specifying their number and their names, and returns an array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects that can be used to set their constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects that can be used to define the constraints of the generic type parameters for the current type.</returns>
		/// <param name="names">An array of names for the generic type parameters.</param>
		/// <exception cref="T:System.InvalidOperationException">Generic type parameters have already been defined for this type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="names" /> is null.-or-An element of <paramref name="names" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="names" /> is an empty array.</exception>
		// Token: 0x06002957 RID: 10583 RVA: 0x00093574 File Offset: 0x00091774
		public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			if (names.Length == 0)
			{
				throw new ArgumentException("names");
			}
			this.generic_params = new GenericTypeParameterBuilder[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				string text = names[i];
				if (text == null)
				{
					throw new ArgumentNullException("names");
				}
				this.generic_params[i] = new GenericTypeParameterBuilder(this, null, text, i);
			}
			return this.generic_params;
		}

		/// <summary>Returns the constructor of the specified constructed generic type that corresponds to the specified constructor of the generic type definition. </summary>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object that represents the constructor of <paramref name="type" /> corresponding to <paramref name="constructor" />, which specifies a constructor belonging to the generic type definition of <paramref name="type" />.</returns>
		/// <param name="type">The constructed generic type whose constructor is returned.</param>
		/// <param name="constructor">A constructor on the generic type definition of <paramref name="type" />, which specifies which constructor of <paramref name="type" /> to return.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not represent a generic type. -or-<paramref name="type" /> is not of type <see cref="T:System.Reflection.Emit.TypeBuilder" />.-or-The declaring type of <paramref name="constructor" /> is not a generic type definition. -or-The declaring type of <paramref name="constructor" /> is not the generic type definition of <paramref name="type" />.</exception>
		// Token: 0x06002958 RID: 10584 RVA: 0x000935E4 File Offset: 0x000917E4
		public static ConstructorInfo GetConstructor(Type type, ConstructorInfo constructor)
		{
			if (type == null)
			{
				throw new ArgumentException("Type is not generic", "type");
			}
			if (!type.IsGenericType)
			{
				throw new ArgumentException("Type is not a generic type", "type");
			}
			if (type.IsGenericTypeDefinition)
			{
				throw new ArgumentException("Type cannot be a generic type definition", "type");
			}
			if (constructor == null)
			{
				throw new NullReferenceException();
			}
			if (!constructor.DeclaringType.IsGenericTypeDefinition)
			{
				throw new ArgumentException("constructor declaring type is not a generic type definition", "constructor");
			}
			if (constructor.DeclaringType != type.GetGenericTypeDefinition())
			{
				throw new ArgumentException("constructor declaring type is not the generic type definition of type", "constructor");
			}
			ConstructorInfo constructor2 = type.GetConstructor(constructor);
			if (constructor2 == null)
			{
				throw new ArgumentException("constructor not found");
			}
			return constructor2;
		}

		// Token: 0x06002959 RID: 10585 RVA: 0x000936A4 File Offset: 0x000918A4
		private static bool IsValidGetMethodType(Type type)
		{
			if (type is TypeBuilder || type is TypeBuilderInstantiation)
			{
				return true;
			}
			if (type.Module is ModuleBuilder)
			{
				return true;
			}
			if (type.IsGenericParameter)
			{
				return false;
			}
			Type[] genericArguments = type.GetGenericArguments();
			if (genericArguments == null)
			{
				return false;
			}
			for (int i = 0; i < genericArguments.Length; i++)
			{
				if (TypeBuilder.IsValidGetMethodType(genericArguments[i]))
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Returns the method of the specified constructed generic type that corresponds to the specified method of the generic type definition. </summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object that represents the method of <paramref name="type" /> corresponding to <paramref name="method" />, which specifies a method belonging to the generic type definition of <paramref name="type" />.</returns>
		/// <param name="type">The constructed generic type whose method is returned.</param>
		/// <param name="method">A method on the generic type definition of <paramref name="type" />, which specifies which method of <paramref name="type" /> to return.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="method" /> is a generic method that is not a generic method definition.-or-<paramref name="type" /> does not represent a generic type.-or-<paramref name="type" /> is not of type <see cref="T:System.Reflection.Emit.TypeBuilder" />.-or-The declaring type of <paramref name="method" /> is not a generic type definition. -or-The declaring type of <paramref name="method" /> is not the generic type definition of <paramref name="type" />.</exception>
		// Token: 0x0600295A RID: 10586 RVA: 0x00093704 File Offset: 0x00091904
		public static MethodInfo GetMethod(Type type, MethodInfo method)
		{
			if (!TypeBuilder.IsValidGetMethodType(type))
			{
				throw new ArgumentException("type is not TypeBuilder but " + type.GetType(), "type");
			}
			if (type is TypeBuilder && type.ContainsGenericParameters)
			{
				type = type.MakeGenericType(type.GetGenericArguments());
			}
			if (!type.IsGenericType)
			{
				throw new ArgumentException("type is not a generic type", "type");
			}
			if (!method.DeclaringType.IsGenericTypeDefinition)
			{
				throw new ArgumentException("method declaring type is not a generic type definition", "method");
			}
			if (method.DeclaringType != type.GetGenericTypeDefinition())
			{
				throw new ArgumentException("method declaring type is not the generic type definition of type", "method");
			}
			if (method == null)
			{
				throw new NullReferenceException();
			}
			MethodInfo method2 = type.GetMethod(method);
			if (method2 == null)
			{
				throw new ArgumentException(string.Format("method {0} not found in type {1}", method.Name, type));
			}
			return method2;
		}

		/// <summary>Returns the field of the specified constructed generic type that corresponds to the specified field of the generic type definition. </summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object that represents the field of <paramref name="type" /> corresponding to <paramref name="field" />, which specifies a field belonging to the generic type definition of <paramref name="type" />.</returns>
		/// <param name="type">The constructed generic type whose field is returned.</param>
		/// <param name="field">A field on the generic type definition of <paramref name="type" />, which specifies which field of <paramref name="type" /> to return.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="type" /> does not represent a generic type. -or-<paramref name="type" /> is not of type <see cref="T:System.Reflection.Emit.TypeBuilder" />.-or-The declaring type of <paramref name="field" /> is not a generic type definition. -or-The declaring type of <paramref name="field" /> is not the generic type definition of <paramref name="type" />.</exception>
		// Token: 0x0600295B RID: 10587 RVA: 0x000937E0 File Offset: 0x000919E0
		public static FieldInfo GetField(Type type, FieldInfo field)
		{
			if (!type.IsGenericType)
			{
				throw new ArgumentException("Type is not a generic type", "type");
			}
			if (type.IsGenericTypeDefinition)
			{
				throw new ArgumentException("Type cannot be a generic type definition", "type");
			}
			if (field is FieldOnTypeBuilderInst)
			{
				throw new ArgumentException("The specified field must be declared on a generic type definition.", "field");
			}
			if (field.DeclaringType != type.GetGenericTypeDefinition())
			{
				throw new ArgumentException("field declaring type is not the generic type definition of type", "method");
			}
			FieldInfo field2 = type.GetField(field);
			if (field2 == null)
			{
				throw new Exception("field not found");
			}
			return field2;
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600295C RID: 10588 RVA: 0x0002126B File Offset: 0x0001F46B
		void _TypeBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600295D RID: 10589 RVA: 0x0002126B File Offset: 0x0001F46B
		void _TypeBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600295E RID: 10590 RVA: 0x0002126B File Offset: 0x0001F46B
		void _TypeBuilder.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600295F RID: 10591 RVA: 0x0002126B File Offset: 0x0001F46B
		void _TypeBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06002960 RID: 10592 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal override bool IsUserType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether this object represents a constructed generic type.</summary>
		/// <returns>true if this object represents a constructed generic type; otherwise, false.</returns>
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override bool IsConstructedGenericType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether a specified <see cref="T:System.Reflection.TypeInfo" /> object can be assigned to this object.</summary>
		/// <returns>true if <paramref name="typeInfo" /> can be assigned to this object; otherwise, false.</returns>
		/// <param name="typeInfo">The object to test.</param>
		// Token: 0x06002962 RID: 10594 RVA: 0x000888FD File Offset: 0x00086AFD
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return base.IsAssignableFrom(typeInfo);
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal TypeBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001601 RID: 5633
		private string tname;

		// Token: 0x04001602 RID: 5634
		private string nspace;

		// Token: 0x04001603 RID: 5635
		private Type parent;

		// Token: 0x04001604 RID: 5636
		private Type nesting_type;

		// Token: 0x04001605 RID: 5637
		internal Type[] interfaces;

		// Token: 0x04001606 RID: 5638
		internal int num_methods;

		// Token: 0x04001607 RID: 5639
		internal MethodBuilder[] methods;

		// Token: 0x04001608 RID: 5640
		internal ConstructorBuilder[] ctors;

		// Token: 0x04001609 RID: 5641
		internal PropertyBuilder[] properties;

		// Token: 0x0400160A RID: 5642
		internal int num_fields;

		// Token: 0x0400160B RID: 5643
		internal FieldBuilder[] fields;

		// Token: 0x0400160C RID: 5644
		internal EventBuilder[] events;

		// Token: 0x0400160D RID: 5645
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x0400160E RID: 5646
		internal TypeBuilder[] subtypes;

		// Token: 0x0400160F RID: 5647
		internal TypeAttributes attrs;

		// Token: 0x04001610 RID: 5648
		private int table_idx;

		// Token: 0x04001611 RID: 5649
		private ModuleBuilder pmodule;

		// Token: 0x04001612 RID: 5650
		private int class_size;

		// Token: 0x04001613 RID: 5651
		private PackingSize packing_size;

		// Token: 0x04001614 RID: 5652
		private IntPtr generic_container;

		// Token: 0x04001615 RID: 5653
		private GenericTypeParameterBuilder[] generic_params;

		// Token: 0x04001616 RID: 5654
		private RefEmitPermissionSet[] permissions;

		// Token: 0x04001617 RID: 5655
		private TypeInfo created;

		// Token: 0x04001618 RID: 5656
		private int state;

		// Token: 0x04001619 RID: 5657
		private TypeName fullname;

		// Token: 0x0400161A RID: 5658
		private bool createTypeCalled;

		// Token: 0x0400161B RID: 5659
		private Type underlying_type;

		/// <summary>Represents that total size for the type is not specified.</summary>
		// Token: 0x0400161C RID: 5660
		public const int UnspecifiedTypeSize = 0;
	}
}
