using System;
using System.Globalization;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Defines and creates generic type parameters for dynamically defined generic types and methods. This class cannot be inherited. </summary>
	// Token: 0x0200035D RID: 861
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GenericTypeParameterBuilder : TypeInfo
	{
		/// <summary>Sets the base type that a type must inherit in order to be substituted for the type parameter.</summary>
		/// <param name="baseTypeConstraint">The <see cref="T:System.Type" /> that must be inherited by any type that is to be substituted for the type parameter.</param>
		// Token: 0x0600269B RID: 9883 RVA: 0x000891EE File Offset: 0x000873EE
		public void SetBaseTypeConstraint(Type baseTypeConstraint)
		{
			this.base_type = baseTypeConstraint ?? typeof(object);
		}

		/// <summary>Sets the interfaces a type must implement in order to be substituted for the type parameter. </summary>
		/// <param name="interfaceConstraints">An array of <see cref="T:System.Type" /> objects that represent the interfaces a type must implement in order to be substituted for the type parameter.</param>
		// Token: 0x0600269C RID: 9884 RVA: 0x00089205 File Offset: 0x00087405
		[ComVisible(true)]
		public void SetInterfaceConstraints(params Type[] interfaceConstraints)
		{
			this.iface_constraints = interfaceConstraints;
		}

		/// <summary>Sets the variance characteristics and special constraints of the generic parameter, such as the parameterless constructor constraint.</summary>
		/// <param name="genericParameterAttributes">A bitwise combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> values that represent the variance characteristics and special constraints of the generic type parameter.</param>
		// Token: 0x0600269D RID: 9885 RVA: 0x0008920E File Offset: 0x0008740E
		public void SetGenericParameterAttributes(GenericParameterAttributes genericParameterAttributes)
		{
			this.attrs = genericParameterAttributes;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x00089217 File Offset: 0x00087417
		internal GenericTypeParameterBuilder(TypeBuilder tbuilder, MethodBuilder mbuilder, string name, int index)
		{
			this.tbuilder = tbuilder;
			this.mbuilder = mbuilder;
			this.name = name;
			this.index = index;
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x0008923C File Offset: 0x0008743C
		internal override Type InternalResolve()
		{
			if (this.mbuilder != null)
			{
				return MethodBase.GetMethodFromHandle(this.mbuilder.MethodHandleInternal, this.mbuilder.TypeBuilder.InternalResolve().TypeHandle).GetGenericArguments()[this.index];
			}
			return this.tbuilder.InternalResolve().GetGenericArguments()[this.index];
		}

		// Token: 0x060026A0 RID: 9888 RVA: 0x000892A0 File Offset: 0x000874A0
		internal override Type RuntimeResolve()
		{
			if (this.mbuilder != null)
			{
				return MethodBase.GetMethodFromHandle(this.mbuilder.MethodHandleInternal, this.mbuilder.TypeBuilder.RuntimeResolve().TypeHandle).GetGenericArguments()[this.index];
			}
			return this.tbuilder.RuntimeResolve().GetGenericArguments()[this.index];
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="c">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026A1 RID: 9889 RVA: 0x00089304 File Offset: 0x00087504
		[ComVisible(true)]
		public override bool IsSubclassOf(Type c)
		{
			throw this.not_supported();
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x00003B29 File Offset: 0x00001D29
		protected override TypeAttributes GetAttributeFlagsImpl()
		{
			return TypeAttributes.Public;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x00089304 File Offset: 0x00087504
		protected override ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases. </exception>
		// Token: 0x060026A4 RID: 9892 RVA: 0x00089304 File Offset: 0x00087504
		[ComVisible(true)]
		public override ConstructorInfo[] GetConstructors(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported. </param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026A5 RID: 9893 RVA: 0x00089304 File Offset: 0x00087504
		public override EventInfo GetEvent(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026A6 RID: 9894 RVA: 0x00089304 File Offset: 0x00087504
		public override EventInfo[] GetEvents()
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026A7 RID: 9895 RVA: 0x00089304 File Offset: 0x00087504
		public override EventInfo[] GetEvents(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026A8 RID: 9896 RVA: 0x00089304 File Offset: 0x00087504
		public override FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026A9 RID: 9897 RVA: 0x00089304 File Offset: 0x00087504
		public override FieldInfo[] GetFields(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">The name of the interface.</param>
		/// <param name="ignoreCase">true to search without regard for case; false to make a case-sensitive search.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026AA RID: 9898 RVA: 0x00089304 File Offset: 0x00087504
		public override Type GetInterface(string name, bool ignoreCase)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026AB RID: 9899 RVA: 0x00089304 File Offset: 0x00087504
		public override Type[] GetInterfaces()
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026AC RID: 9900 RVA: 0x00089304 File Offset: 0x00087504
		public override MemberInfo[] GetMembers(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">Not supported.</param>
		/// <param name="type">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026AD RID: 9901 RVA: 0x00089304 File Offset: 0x00087504
		public override MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026AE RID: 9902 RVA: 0x00089304 File Offset: 0x00087504
		public override MethodInfo[] GetMethods(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x00089304 File Offset: 0x00087504
		protected override MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">Not supported.</param>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026B0 RID: 9904 RVA: 0x00089304 File Offset: 0x00087504
		public override Type GetNestedType(string name, BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026B1 RID: 9905 RVA: 0x00089304 File Offset: 0x00087504
		public override Type[] GetNestedTypes(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="bindingAttr">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026B2 RID: 9906 RVA: 0x00089304 File Offset: 0x00087504
		public override PropertyInfo[] GetProperties(BindingFlags bindingAttr)
		{
			throw this.not_supported();
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x00089304 File Offset: 0x00087504
		protected override PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			throw this.not_supported();
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool HasElementTypeImpl()
		{
			return false;
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <returns>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</returns>
		/// <param name="c">The object to test.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026B5 RID: 9909 RVA: 0x00089304 File Offset: 0x00087504
		public override bool IsAssignableFrom(Type c)
		{
			throw this.not_supported();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
		/// <returns>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</returns>
		/// <param name="typeInfo">The object to test.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026B6 RID: 9910 RVA: 0x0004AF8A File Offset: 0x0004918A
		public override bool IsAssignableFrom(TypeInfo typeInfo)
		{
			return !(typeInfo == null) && this.IsAssignableFrom(typeInfo.AsType());
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x00089304 File Offset: 0x00087504
		public override bool IsInstanceOfType(object o)
		{
			throw this.not_supported();
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsArrayImpl()
		{
			return false;
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsByRefImpl()
		{
			return false;
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsCOMObjectImpl()
		{
			return false;
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsPointerImpl()
		{
			return false;
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x00015ED5 File Offset: 0x000140D5
		protected override bool IsPrimitiveImpl()
		{
			return false;
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x0008930C File Offset: 0x0008750C
		protected override bool IsValueTypeImpl()
		{
			return this.base_type != null && this.base_type.IsValueType;
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="name">Not supported. </param>
		/// <param name="invokeAttr">Not supported.</param>
		/// <param name="binder">Not supported.</param>
		/// <param name="target">Not supported.</param>
		/// <param name="args">Not supported.</param>
		/// <param name="modifiers">Not supported.</param>
		/// <param name="culture">Not supported.</param>
		/// <param name="namedParameters">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026BE RID: 9918 RVA: 0x00089304 File Offset: 0x00087504
		public override object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters)
		{
			throw this.not_supported();
		}

		/// <summary>Throws a <see cref="T:System.NotSupportedException" /> in all cases. </summary>
		/// <returns>The type referred to by the current array type, pointer type, or ByRef type; or null if the current type is not an array type, is not a pointer type, and is not passed by reference.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026BF RID: 9919 RVA: 0x00089304 File Offset: 0x00087504
		public override Type GetElementType()
		{
			throw this.not_supported();
		}

		/// <summary>Gets the current generic type parameter.</summary>
		/// <returns>The current <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> object.</returns>
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x00002119 File Offset: 0x00000319
		public override Type UnderlyingSystemType
		{
			get
			{
				return this;
			}
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Assembly" /> object representing the dynamic assembly that contains the generic type definition the current type parameter belongs to.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> object representing the dynamic assembly that contains the generic type definition the current type parameter belongs to.</returns>
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060026C1 RID: 9921 RVA: 0x00089329 File Offset: 0x00087529
		public override Assembly Assembly
		{
			get
			{
				return this.tbuilder.Assembly;
			}
		}

		/// <summary>Gets null in all cases.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) in all cases.</returns>
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x0000A42E File Offset: 0x0000862E
		public override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the base type constraint of the current generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the base type constraint of the generic type parameter, or null if the type parameter has no base type constraint.</returns>
		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x060026C3 RID: 9923 RVA: 0x00089336 File Offset: 0x00087536
		public override Type BaseType
		{
			get
			{
				return this.base_type;
			}
		}

		/// <summary>Gets null in all cases.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) in all cases.</returns>
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x0000A42E File Offset: 0x0000862E
		public override string FullName
		{
			get
			{
				return null;
			}
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases. </exception>
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x00089304 File Offset: 0x00087504
		public override Guid GUID
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="attributeType">Not supported.</param>
		/// <param name="inherit">Not supported.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026C6 RID: 9926 RVA: 0x00089304 File Offset: 0x00087504
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026C7 RID: 9927 RVA: 0x00089304 File Offset: 0x00087504
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned.</param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026C8 RID: 9928 RVA: 0x00089304 File Offset: 0x00087504
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <param name="interfaceType">A <see cref="T:System.Type" /> object that represents the interface type for which the mapping is to be retrieved.</param>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x060026C9 RID: 9929 RVA: 0x00089304 File Offset: 0x00087504
		[ComVisible(true)]
		public override InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw this.not_supported();
		}

		/// <summary>Gets the name of the generic type parameter.</summary>
		/// <returns>The name of the generic type parameter.</returns>
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x0008933E File Offset: 0x0008753E
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets null in all cases.</summary>
		/// <returns>A null reference (Nothing in Visual Basic) in all cases.</returns>
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060026CB RID: 9931 RVA: 0x0000A42E File Offset: 0x0000862E
		public override string Namespace
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the dynamic module that contains the generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> object that represents the dynamic module that contains the generic type parameter.</returns>
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x00089346 File Offset: 0x00087546
		public override Module Module
		{
			get
			{
				return this.tbuilder.Module;
			}
		}

		/// <summary>Gets the generic type definition or generic method definition to which the generic type parameter belongs.</summary>
		/// <returns>If the type parameter belongs to a generic type, a <see cref="T:System.Type" /> object representing that generic type; if the type parameter belongs to a generic method, a <see cref="T:System.Type" /> object representing that type that declared that generic method.</returns>
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060026CD RID: 9933 RVA: 0x00089353 File Offset: 0x00087553
		public override Type DeclaringType
		{
			get
			{
				if (!(this.mbuilder != null))
				{
					return this.tbuilder;
				}
				return this.mbuilder.DeclaringType;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> object that was used to obtain the <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that was used to obtain the <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</returns>
		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x0004BEB9 File Offset: 0x0004A0B9
		public override Type ReflectedType
		{
			get
			{
				return this.DeclaringType;
			}
		}

		/// <summary>Not supported for incomplete generic type parameters.</summary>
		/// <returns>Not supported for incomplete generic type parameters.</returns>
		/// <exception cref="T:System.NotSupportedException">In all cases.</exception>
		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060026CF RID: 9935 RVA: 0x00089304 File Offset: 0x00087504
		public override RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Not valid for generic type parameters.</summary>
		/// <returns>Not valid for generic type parameters.</returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x060026D0 RID: 9936 RVA: 0x0007EA26 File Offset: 0x0007CC26
		public override Type[] GetGenericArguments()
		{
			throw new InvalidOperationException();
		}

		/// <summary>Not valid for generic type parameters.</summary>
		/// <returns>Not valid for generic type parameters.</returns>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x060026D1 RID: 9937 RVA: 0x0007EA26 File Offset: 0x0007CC26
		public override Type GetGenericTypeDefinition()
		{
			throw new InvalidOperationException();
		}

		/// <summary>Gets true in all cases.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060026D2 RID: 9938 RVA: 0x00003B29 File Offset: 0x00001D29
		public override bool ContainsGenericParameters
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets true in all cases.</summary>
		/// <returns>true in all cases.</returns>
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060026D3 RID: 9939 RVA: 0x00003B29 File Offset: 0x00001D29
		public override bool IsGenericParameter
		{
			get
			{
				return true;
			}
		}

		/// <summary>Returns false in all cases.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets false in all cases.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x060026D5 RID: 9941 RVA: 0x00015ED5 File Offset: 0x000140D5
		public override bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets the position of the type parameter in the type parameter list of the generic type or method that declared the parameter.</summary>
		/// <returns>The position of the type parameter in the type parameter list of the generic type or method that declared the parameter.</returns>
		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060026D7 RID: 9943 RVA: 0x00089375 File Offset: 0x00087575
		public override int GenericParameterPosition
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x0007EA26 File Offset: 0x0007CC26
		public override Type[] GetGenericParameterConstraints()
		{
			throw new InvalidOperationException();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodInfo" /> that represents the declaring method, if the current <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> represents a type parameter of a generic method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> that represents the declaring method, if the current <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> represents a type parameter of a generic method; otherwise, null.</returns>
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060026D9 RID: 9945 RVA: 0x0008937D File Offset: 0x0008757D
		public override MethodBase DeclaringMethod
		{
			get
			{
				return this.mbuilder;
			}
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class that defines the custom attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is null.</exception>
		// Token: 0x060026DA RID: 9946 RVA: 0x00089388 File Offset: 0x00087588
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
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
		/// <param name="con">The constructor for the custom attribute.</param>
		/// <param name="binaryAttribute">A byte blob representing the attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is null.-or-<paramref name="binaryAttribute" /> is a null reference.</exception>
		// Token: 0x060026DB RID: 9947 RVA: 0x000893F0 File Offset: 0x000875F0
		[MonoTODO("unverified implementation")]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		// Token: 0x060026DC RID: 9948 RVA: 0x000893FF File Offset: 0x000875FF
		private Exception not_supported()
		{
			return new NotSupportedException();
		}

		/// <summary>Returns a string representation of the current generic type parameter.</summary>
		/// <returns>A string that contains the name of the generic type parameter.</returns>
		// Token: 0x060026DD RID: 9949 RVA: 0x0008933E File Offset: 0x0008753E
		public override string ToString()
		{
			return this.name;
		}

		/// <summary>Tests whether the given object is an instance of EventToken and is equal to the current instance.</summary>
		/// <returns>Returns true if <paramref name="o" /> is an instance of EventToken and equals the current instance; otherwise, false.</returns>
		/// <param name="o">The object to be compared with the current instance.</param>
		// Token: 0x060026DE RID: 9950 RVA: 0x00089406 File Offset: 0x00087606
		[MonoTODO]
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		/// <summary>Returns a 32-bit integer hash code for the current instance.</summary>
		/// <returns>A 32-bit integer hash code.</returns>
		// Token: 0x060026DF RID: 9951 RVA: 0x0008940F File Offset: 0x0008760F
		[MonoTODO]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Returns the type of a one-dimensional array whose element type is the generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of a one-dimensional array whose element type is the generic type parameter.</returns>
		// Token: 0x060026E0 RID: 9952 RVA: 0x000848C1 File Offset: 0x00082AC1
		public override Type MakeArrayType()
		{
			return new ArrayType(this, 0);
		}

		/// <summary>Returns the type of an array whose element type is the generic type parameter, with the specified number of dimensions.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the type of an array whose element type is the generic type parameter, with the specified number of dimensions.</returns>
		/// <param name="rank">The number of dimensions for the array.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is not a valid number of dimensions. For example, its value is less than 1.</exception>
		// Token: 0x060026E1 RID: 9953 RVA: 0x000848CA File Offset: 0x00082ACA
		public override Type MakeArrayType(int rank)
		{
			if (rank < 1)
			{
				throw new IndexOutOfRangeException();
			}
			return new ArrayType(this, rank);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the current generic type parameter when passed as a reference parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the current generic type parameter when passed as a reference parameter.</returns>
		// Token: 0x060026E2 RID: 9954 RVA: 0x000848DD File Offset: 0x00082ADD
		public override Type MakeByRefType()
		{
			return new ByRefType(this);
		}

		/// <summary>Not valid for incomplete generic type parameters.</summary>
		/// <returns>This method is invalid for incomplete generic type parameters.</returns>
		/// <param name="typeArguments">An array of type arguments.</param>
		/// <exception cref="T:System.InvalidOperationException">In all cases.</exception>
		// Token: 0x060026E3 RID: 9955 RVA: 0x00089417 File Offset: 0x00087617
		public override Type MakeGenericType(params Type[] typeArguments)
		{
			throw new InvalidOperationException(Environment.GetResourceString("{0} is not a GenericTypeDefinition. MakeGenericType may only be called on a type for which Type.IsGenericTypeDefinition is true."));
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a pointer to the current generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents a pointer to the current generic type parameter.</returns>
		// Token: 0x060026E4 RID: 9956 RVA: 0x000848E5 File Offset: 0x00082AE5
		public override Type MakePointerType()
		{
			return new PointerType(this);
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal override bool IsUserType
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060026E6 RID: 9958 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal GenericTypeParameterBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001422 RID: 5154
		private TypeBuilder tbuilder;

		// Token: 0x04001423 RID: 5155
		private MethodBuilder mbuilder;

		// Token: 0x04001424 RID: 5156
		private string name;

		// Token: 0x04001425 RID: 5157
		private int index;

		// Token: 0x04001426 RID: 5158
		private Type base_type;

		// Token: 0x04001427 RID: 5159
		private Type[] iface_constraints;

		// Token: 0x04001428 RID: 5160
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x04001429 RID: 5161
		private GenericParameterAttributes attrs;
	}
}
