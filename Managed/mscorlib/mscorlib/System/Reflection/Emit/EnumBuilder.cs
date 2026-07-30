using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Describes and represents an enumeration type.</summary>
	// Token: 0x02000355 RID: 853
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_EnumBuilder))]
	[ComVisible(true)]
	public sealed class EnumBuilder : TypeInfo, _EnumBuilder
	{
		// Token: 0x060025FD RID: 9725 RVA: 0x000885D8 File Offset: 0x000867D8
		internal EnumBuilder(ModuleBuilder mb, string name, TypeAttributes visibility, Type underlyingType)
		{
			this._tb = new TypeBuilder(mb, name, visibility | TypeAttributes.Sealed, typeof(Enum), null, PackingSize.Unspecified, 0, null);
			this._underlyingType = underlyingType;
			this._underlyingField = this._tb.DefineField("value__", underlyingType, FieldAttributes.Private | FieldAttributes.SpecialName | FieldAttributes.RTSpecialName);
			this.setup_enum_type(this._tb);
		}

		// Token: 0x060025FE RID: 9726 RVA: 0x0008863E File Offset: 0x0008683E
		internal TypeBuilder GetTypeBuilder()
		{
			return this._tb;
		}

		// Token: 0x060025FF RID: 9727 RVA: 0x00088646 File Offset: 0x00086846
		internal override Type InternalResolve()
		{
			return this._tb.InternalResolve();
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x00088653 File Offset: 0x00086853
		internal override Type RuntimeResolve()
		{
			return this._tb.RuntimeResolve();
		}

		/// <summary>Retrieves the dynamic assembly that contains this enum definition.</summary>
		/// <returns>Read-only. The dynamic assembly that contains this enum definition.</returns>
		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002601 RID: 9729 RVA: 0x00088660 File Offset: 0x00086860
		public override Assembly Assembly
		{
			get
			{
				return this._tb.Assembly;
			}
		}

		/// <summary>Returns the full path of this enum qualified by the display name of the parent assembly.</summary>
		/// <returns>Read-only. The full path of this enum qualified by the display name of the parent assembly.</returns>
		/// <exception cref="T:System.NotSupportedException">If <see cref="M:System.Reflection.Emit.EnumBuilder.CreateType" /> has not been called previously. </exception>
		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x0008866D File Offset: 0x0008686D
		public override string AssemblyQualifiedName
		{
			get
			{
				return this._tb.AssemblyQualifiedName;
			}
		}

		/// <summary>Returns the parent <see cref="T:System.Type" /> of this type which is always <see cref="T:System.Enum" />.</summary>
		/// <returns>Read-only. The parent <see cref="T:System.Type" /> of this type.</returns>
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06002603 RID: 9731 RVA: 0x0008867A File Offset: 0x0008687A
		public override Type BaseType
		{
			get
			{
				return this._tb.BaseType;
			}
		}

		/// <summary>Returns the type that declared this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</summary>
		/// <returns>Read-only. The type that declared this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</returns>
		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06002604 RID: 9732 RVA: 0x00088687 File Offset: 0x00086887
		public override Type DeclaringType
		{
			get
			{
				return this._tb.DeclaringType;
			}
		}

		/// <summary>Returns the full path of this enum.</summary>
		/// <returns>Read-only. The full path of this enum.</returns>
		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06002605 RID: 9733 RVA: 0x00088694 File Offset: 0x00086894
		public override string FullName
		{
			get
			{
				return this._tb.FullName;
			}
		}

		/// <summary>Returns the GUID of this enum.</summary>
		/// <returns>Read-only. The GUID of this enum.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06002606 RID: 9734 RVA: 0x000886A1 File Offset: 0x000868A1
		public override Guid GUID
		{
			get
			{
				return this._tb.GUID;
			}
		}

		/// <summary>Retrieves the dynamic module that contains this <see cref="T:System.Reflection.Emit.EnumBuilder" /> definition.</summary>
		/// <returns>Read-only. The dynamic module that contains this <see cref="T:System.Reflection.Emit.EnumBuilder" /> definition.</returns>
		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06002607 RID: 9735 RVA: 0x000886AE File Offset: 0x000868AE
		public override Module Module
		{
			get
			{
				return this._tb.Module;
			}
		}

		/// <summary>Returns the name of this enum.</summary>
		/// <returns>Read-only. The name of this enum.</returns>
		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x06002608 RID: 9736 RVA: 0x000886BB File Offset: 0x000868BB
		public override string Name
		{
			get
			{
				return this._tb.Name;
			}
		}

		/// <summary>Returns the namespace of this enum.</summary>
		/// <returns>Read-only. The namespace of this enum.</returns>
		// Token: 0x170005F5 RID: 1525
		// (get) Token: 0x06002609 RID: 9737 RVA: 0x000886C8 File Offset: 0x000868C8
		public override string Namespace
		{
			get
			{
				return this._tb.Namespace;
			}
		}

		/// <summary>Returns the type that was used to obtain this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</summary>
		/// <returns>Read-only. The type that was used to obtain this <see cref="T:System.Reflection.Emit.EnumBuilder" />.</returns>
		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x0600260A RID: 9738 RVA: 0x000886D5 File Offset: 0x000868D5
		public override Type ReflectedType
		{
			get
			{
				return this._tb.ReflectedType;
			}
		}

		/// <summary>Retrieves the internal handle for this enum.</summary>
		/// <returns>Read-only. The internal handle for this enum.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not currently supported. </exception>
		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x0600260B RID: 9739 RVA: 0x000886E2 File Offset: 0x000868E2
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				return this._tb.TypeHandle;
			}
		}

		/// <summary>Returns the internal metadata type token of this enum.</summary>
		/// <returns>Read-only. The type token of this enum.</returns>
		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x0600260C RID: 9740 RVA: 0x000886EF File Offset: 0x000868EF
		public TypeToken TypeToken
		{
			get
			{
				return this._tb.TypeToken;
			}
		}

		/// <summary>Returns the underlying field for this enum.</summary>
		/// <returns>Read-only. The underlying field for this enum.</returns>
		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x0600260D RID: 9741 RVA: 0x000886FC File Offset: 0x000868FC
		public FieldBuilder UnderlyingField
		{
			get
			{
				return this._underlyingField;
			}
		}

		/// <summary>Returns the underlying system type for this enum.</summary>
		/// <returns>Read-only. Returns the underlying system type.</returns>
		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x0600260E RID: 9742 RVA: 0x00088704 File Offset: 0x00086904
		public override Type UnderlyingSystemType
		{
			get
			{
				return this._underlyingType;
			}
		}

		/// <summary>Creates a <see cref="T:System.Type" /> object for this enum.</summary>
		/// <returns>A <see cref="T:System.Type" /> object for this enum.</returns>
		/// <exception cref="T:System.InvalidOperationException">This type has been previously created.-or- The enclosing type has not been created. </exception>
		// Token: 0x0600260F RID: 9743 RVA: 0x0008870C File Offset: 0x0008690C
		public Type CreateType()
		{
			return this._tb.CreateType();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.TypeInfo" /> object that represents this enumeration.</summary>
		/// <returns>An object that represents this enumeration.</returns>
		// Token: 0x06002610 RID: 9744 RVA: 0x00088719 File Offset: 0x00086919
		public TypeInfo CreateTypeInfo()
		{
			return this._tb.CreateTypeInfo();
		}

		/// <summary>Returns the underlying integer type of the current enumeration, which is set when the enumeration builder is defined.</summary>
		/// <returns>The underlying type.</returns>
		// Token: 0x06002611 RID: 9745 RVA: 0x00088704 File Offset: 0x00086904
		public override Type GetEnumUnderlyingType()
		{
			return this._underlyingType;
		}

		// Token: 0x06002612 RID: 9746
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void setup_enum_type(Type t);

		/// <summary>Defines the named static field in an enumeration type with the specified constant value.</summary>
		/// <returns>The defined field.</returns>
		/// <param name="literalName">The name of the static field. </param>
		/// <param name="literalValue">The constant value of the literal. </param>
		// Token: 0x06002613 RID: 9747 RVA: 0x00088728 File Offset: 0x00086928
		public FieldBuilder DefineLiteral(string literalName, object literalValue)
		{
			FieldBuilder fieldBuilder = this._tb.DefineField(literalName, this, FieldAttributes.FamANDAssem | FieldAttributes.Family | FieldAttributes.Static | FieldAttributes.Literal);
			fieldBuilder.SetConstant(literalValue);
			return fieldBuilder;
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x0008874D File Offset: 0x0008694D
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return this._tb.attrs;
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x0008875A File Offset: 0x0008695A
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			return this._tb.GetConstructor(bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the public and non-public constructors defined for this class, as specified.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing the specified constructors defined for this class. If no constructors are defined, an empty array is returned.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002616 RID: 9750 RVA: 0x0008876E File Offset: 0x0008696E
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			return this._tb.GetConstructors(bindingAttr);
		}

		/// <summary>Returns all the custom attributes defined for this constructor.</summary>
		/// <returns>Returns an array of objects representing all the custom attributes of the constructor represented by this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> instance.</returns>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x06002617 RID: 9751 RVA: 0x0008877C File Offset: 0x0008697C
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this._tb.GetCustomAttributes(inherit);
		}

		/// <summary>Returns the custom attributes identified by the given type.</summary>
		/// <returns>Returns an array of objects representing the attributes of this constructor that are of <see cref="T:System.Type" /><paramref name="attributeType" />.</returns>
		/// <param name="attributeType">The Type object to which the custom attributes are applied. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x06002618 RID: 9752 RVA: 0x0008878A File Offset: 0x0008698A
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this._tb.GetCustomAttributes(attributeType, inherit);
		}

		/// <summary>Calling this method always throws <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>This method is not supported. No value is returned.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		// Token: 0x06002619 RID: 9753 RVA: 0x00088799 File Offset: 0x00086999
		public override Type GetElementType()
		{
			return this._tb.GetElementType();
		}

		/// <summary>Returns the event with the specified name.</summary>
		/// <returns>Returns an <see cref="T:System.Reflection.EventInfo" /> object representing the event declared or inherited by this type with the specified name. If there are no matches, null is returned.</returns>
		/// <param name="name">The name of the event to get. </param>
		/// <param name="bindingAttr">This invocation attribute. This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x0600261A RID: 9754 RVA: 0x000887A6 File Offset: 0x000869A6
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			return this._tb.GetEvent(name, bindingAttr);
		}

		/// <summary>Returns the events for the public events declared or inherited by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing the public events declared or inherited by this type. An empty array is returned if there are no public events.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x0600261B RID: 9755 RVA: 0x000887B5 File Offset: 0x000869B5
		public override EventInfo[] GetEvents()
		{
			return this._tb.GetEvents();
		}

		/// <summary>Returns the public and non-public events that are declared by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.EventInfo" /> objects representing the public and non-public events declared or inherited by this type. An empty array is returned if there are no events, as specified.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600261C RID: 9756 RVA: 0x000887C2 File Offset: 0x000869C2
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			return this._tb.GetEvents(bindingAttr);
		}

		/// <summary>Returns the field specified by the given name.</summary>
		/// <returns>Returns the <see cref="T:System.Reflection.FieldInfo" /> object representing the field declared or inherited by this type with the specified name and public or non-public modifier. If there are no matches, then null is returned.</returns>
		/// <param name="name">The name of the field to get. </param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x0600261D RID: 9757 RVA: 0x000887D0 File Offset: 0x000869D0
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			return this._tb.GetField(name, bindingAttr);
		}

		/// <summary>Returns the public and non-public fields that are declared by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.FieldInfo" /> objects representing the public and non-public fields declared or inherited by this type. An empty array is returned if there are no fields, as specified.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600261E RID: 9758 RVA: 0x000887DF File Offset: 0x000869DF
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			return this._tb.GetFields(bindingAttr);
		}

		/// <summary>Returns the interface implemented (directly or indirectly) by this type, with the specified fully-qualified name.</summary>
		/// <returns>Returns a <see cref="T:System.Type" /> object representing the implemented interface. Returns null if no interface matching name is found.</returns>
		/// <param name="name">The name of the interface. </param>
		/// <param name="ignoreCase">If true, the search is case-insensitive. If false, the search is case-sensitive. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x0600261F RID: 9759 RVA: 0x000887ED File Offset: 0x000869ED
		public override Type GetInterface(string name, bool ignoreCase)
		{
			return this._tb.GetInterface(name, ignoreCase);
		}

		/// <summary>Returns an interface mapping for the interface requested.</summary>
		/// <returns>The requested interface mapping.</returns>
		/// <param name="interfaceType">The type of the interface for which the interface mapping is to be retrieved. </param>
		/// <exception cref="T:System.ArgumentException">The type does not implement the interface. </exception>
		// Token: 0x06002620 RID: 9760 RVA: 0x000887FC File Offset: 0x000869FC
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			return this._tb.GetInterfaceMap(interfaceType);
		}

		/// <summary>Returns an array of all the interfaces implemented on this a class and its base classes.</summary>
		/// <returns>Returns an array of <see cref="T:System.Type" /> objects representing the implemented interfaces. If none are defined, an empty array is returned.</returns>
		// Token: 0x06002621 RID: 9761 RVA: 0x0008880A File Offset: 0x00086A0A
		public override Type[] GetInterfaces()
		{
			return this._tb.GetInterfaces();
		}

		/// <summary>Returns all members with the specified name, type, and binding that are declared or inherited by this type.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public members are returned.</returns>
		/// <param name="name">The name of the member. </param>
		/// <param name="type">The type of member that is to be returned. </param>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x06002622 RID: 9762 RVA: 0x00088817 File Offset: 0x00086A17
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			return this._tb.GetMember(name, type, bindingAttr);
		}

		/// <summary>Returns the specified members declared or inherited by this type,.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public and non-public members declared or inherited by this type. An empty array is returned if there are no matching members.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002623 RID: 9763 RVA: 0x00088827 File Offset: 0x00086A27
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			return this._tb.GetMembers(bindingAttr);
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x00088835 File Offset: 0x00086A35
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				return this._tb.GetMethod(name, bindingAttr);
			}
			return this._tb.GetMethod(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns all the public and non-public methods declared or inherited by this type, as specified.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.MethodInfo" /> objects representing the public and non-public methods defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public methods are returned.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002625 RID: 9765 RVA: 0x0008885D File Offset: 0x00086A5D
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			return this._tb.GetMethods(bindingAttr);
		}

		/// <summary>Returns the specified nested type that is declared by this type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the nested type that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The <see cref="T:System.String" /> containing the name of the nested type to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to conduct a case-sensitive search for public methods. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x06002626 RID: 9766 RVA: 0x0008886B File Offset: 0x00086A6B
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			return this._tb.GetNestedType(name, bindingAttr);
		}

		/// <summary>Returns the public and non-public nested types that are declared or inherited by this type.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the types nested within the current <see cref="T:System.Type" /> that match the specified binding constraints.An empty array of type <see cref="T:System.Type" />, if no types are nested within the current <see cref="T:System.Type" />, or if none of the nested types match the binding constraints.</returns>
		/// <param name="bindingAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002627 RID: 9767 RVA: 0x0008887A File Offset: 0x00086A7A
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			return this._tb.GetNestedTypes(bindingAttr);
		}

		/// <summary>Returns all the public and non-public properties declared or inherited by this type, as specified.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing the public and non-public properties defined on this type if <paramref name="nonPublic" /> is used; otherwise, only the public properties are returned.</returns>
		/// <param name="bindingAttr">This invocation attribute. This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002628 RID: 9768 RVA: 0x00088888 File Offset: 0x00086A88
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			return this._tb.GetProperties(bindingAttr);
		}

		// Token: 0x06002629 RID: 9769 RVA: 0x00088896 File Offset: 0x00086A96
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.CreateNotSupportedException();
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x0008889E File Offset: 0x00086A9E
		protected override bool HasElementTypeImpl()
		{
			return this._tb.HasElementType;
		}

		/// <summary>Invokes the specified member. The method that is to be invoked must be accessible and provide the most specific match with the specified argument list, under the contraints of the specified binder and invocation attributes.</summary>
		/// <returns>Returns the return value of the invoked member.</returns>
		/// <param name="name">The name of the member to invoke. This can be a constructor, method, property, or field. A suitable invocation attribute must be specified. Note that it is possible to invoke the default member of a class by passing an empty string as the name of the member. </param>
		/// <param name="invokeAttr">The invocation attribute. This must be a bit flag from BindingFlags. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects using reflection. If binder is null, the default binder is used. See <see cref="T:System.Reflection.Binder" />. </param>
		/// <param name="target">The object on which to invoke the specified member. If the member is static, this parameter is ignored. </param>
		/// <param name="args">An argument list. This is an array of objects that contains the number, order, and type of the parameters of the member to be invoked. If there are no parameters this should be null. </param>
		/// <param name="modifiers">An array of the same length as <paramref name="args" /> with elements that represent the attributes associated with the arguments of the member to be invoked. A parameter has attributes associated with it in the metadata. They are used by various interoperability services. See the metadata specs for details such as this. </param>
		/// <param name="culture">An instance of CultureInfo used to govern the coercion of types. If this is null, the CultureInfo for the current thread is used. (Note that this is necessary to, for example, convert a string that represents 1000 to a double value, since 1000 is represented differently by different cultures.) </param>
		/// <param name="namedParameters">Each parameter in the <paramref name="namedParameters" /> array gets the value in the corresponding element in the <paramref name="args" /> array. If the length of <paramref name="args" /> is greater than the length of <paramref name="namedParameters" />, the remaining argument values are passed in order. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x0600262B RID: 9771 RVA: 0x000888AC File Offset: 0x00086AAC
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			return this._tb.InvokeMember(name, invokeAttr, binder, target, args, modifiers, culture, namedParameters);
		}

		// Token: 0x0600262C RID: 9772 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x0600262D RID: 9773 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x06002630 RID: 9776 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x06002631 RID: 9777 RVA: 0x00003B29 File Offset: 0x00001D29
		protected override bool IsValueTypeImpl()
		{
			return true;
		}

		/// <summary>Checks if the specified custom attribute type is defined.</summary>
		/// <returns>true if one or more instance of <paramref name="attributeType" /> is defined on this member; otherwise, false.</returns>
		/// <param name="attributeType">The Type object to which the custom attributes are applied. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported in types that are not complete. </exception>
		// Token: 0x06002632 RID: 9778 RVA: 0x000888D1 File Offset: 0x00086AD1
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this._tb.IsDefined(attributeType, inherit);
		}

		// Token: 0x06002633 RID: 9779 RVA: 0x000848C1 File Offset: 0x00082AC1
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is less than 1.</exception>
		// Token: 0x06002634 RID: 9780 RVA: 0x000848CA File Offset: 0x00082ACA
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		// Token: 0x06002635 RID: 9781 RVA: 0x000848DD File Offset: 0x00082ADD
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x000848E5 File Offset: 0x00082AE5
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		/// <summary>Sets a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is null. </exception>
		// Token: 0x06002637 RID: 9783 RVA: 0x000888E0 File Offset: 0x00086AE0
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			this._tb.SetCustomAttribute(customBuilder);
		}

		/// <summary>Sets a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="binaryAttribute">A byte blob representing the attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is null. </exception>
		// Token: 0x06002638 RID: 9784 RVA: 0x000888EE File Offset: 0x00086AEE
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x06002639 RID: 9785 RVA: 0x00085D62 File Offset: 0x00083F62
		private Exception CreateNotSupportedException()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x0600263A RID: 9786 RVA: 0x0002126B File Offset: 0x0001F46B
		void _EnumBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x0600263B RID: 9787 RVA: 0x0002126B File Offset: 0x0001F46B
		void _EnumBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x0600263C RID: 9788 RVA: 0x0002126B File Offset: 0x0001F46B
		void _EnumBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x0600263D RID: 9789 RVA: 0x0002126B File Offset: 0x0001F46B
		void _EnumBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x0600263E RID: 9790 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal override bool IsUserType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether this object represents a constructed generic type.</summary>
		/// <returns>true if this object represents a constructed generic type; otherwise, false.</returns>
		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x0600263F RID: 9791 RVA: 0x00015ED5 File Offset: 0x000140D5
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
		// Token: 0x06002640 RID: 9792 RVA: 0x000888FD File Offset: 0x00086AFD
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return base.IsAssignableFrom(typeInfo);
		}

		// Token: 0x06002641 RID: 9793 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal EnumBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040013F5 RID: 5109
		private TypeBuilder _tb;

		// Token: 0x040013F6 RID: 5110
		private FieldBuilder _underlyingField;

		// Token: 0x040013F7 RID: 5111
		private Type _underlyingType;
	}
}
