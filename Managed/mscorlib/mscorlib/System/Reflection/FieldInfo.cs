using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a field and provides access to field metadata. </summary>
	// Token: 0x0200031A RID: 794
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_FieldInfo))]
	[ClassInterface(ClassInterfaceType.None)]
	[Serializable]
	public abstract class FieldInfo : MemberInfo, _FieldInfo
	{
		/// <summary>Gets the attributes associated with this field.</summary>
		/// <returns>The FieldAttributes for this field.</returns>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060022B2 RID: 8882
		public abstract FieldAttributes Attributes { get; }

		/// <summary>Gets a RuntimeFieldHandle, which is a handle to the internal metadata representation of a field.</summary>
		/// <returns>A handle to the internal metadata representation of a field.</returns>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060022B3 RID: 8883
		public abstract RuntimeFieldHandle FieldHandle { get; }

		/// <summary>Gets the type of this field object.</summary>
		/// <returns>The type of this field object.</returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060022B5 RID: 8885
		public abstract Type FieldType { get; }

		/// <summary>When overridden in a derived class, returns the value of a field supported by a given object.</summary>
		/// <returns>An object containing the value of the field reflected by this instance.</returns>
		/// <param name="obj">The object whose field value will be returned. </param>
		/// <exception cref="T:System.Reflection.TargetException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.The field is non-static and <paramref name="obj" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">A field is marked literal, but the field does not have one of the accepted literal types. </exception>
		/// <exception cref="T:System.FieldAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to access this field. </exception>
		/// <exception cref="T:System.ArgumentException">The method is neither declared nor inherited by the class of <paramref name="obj" />. </exception>
		// Token: 0x060022B6 RID: 8886
		public abstract object GetValue(object obj);

		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a field.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a field.</returns>
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x00028BDC File Offset: 0x00026DDC
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		/// <summary>Gets a value indicating whether the value is written at compile time and cannot be changed.</summary>
		/// <returns>true if the field has the Literal attribute set; otherwise, false.</returns>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060022B8 RID: 8888 RVA: 0x00081B25 File Offset: 0x0007FD25
		public bool IsLiteral
		{
			get
			{
				return (this.Attributes & FieldAttributes.Literal) > FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the field is static.</summary>
		/// <returns>true if this field is static; otherwise, false.</returns>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x00081B33 File Offset: 0x0007FD33
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & FieldAttributes.Static) > FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the field can only be set in the body of the constructor.</summary>
		/// <returns>true if the field has the InitOnly attribute set; otherwise, false.</returns>
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060022BA RID: 8890 RVA: 0x00081B41 File Offset: 0x0007FD41
		public bool IsInitOnly
		{
			get
			{
				return (this.Attributes & FieldAttributes.InitOnly) > FieldAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the field is public.</summary>
		/// <returns>true if this field is public; otherwise, false.</returns>
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060022BB RID: 8891 RVA: 0x00081B4F File Offset: 0x0007FD4F
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
			}
		}

		/// <summary>Gets a value indicating whether the field is private.</summary>
		/// <returns>true if the field is private; otherwise; false.</returns>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060022BC RID: 8892 RVA: 0x00081B5C File Offset: 0x0007FD5C
		public bool IsPrivate
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Private;
			}
		}

		/// <summary>Gets a value indicating whether the visibility of this field is described by <see cref="F:System.Reflection.FieldAttributes.Family" />; that is, the field is visible only within its class and derived classes.</summary>
		/// <returns>true if access to this field is exactly described by <see cref="F:System.Reflection.FieldAttributes.Family" />; otherwise, false.</returns>
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060022BD RID: 8893 RVA: 0x00081B69 File Offset: 0x0007FD69
		public bool IsFamily
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Family;
			}
		}

		/// <summary>Gets a value indicating whether the potential visibility of this field is described by <see cref="F:System.Reflection.FieldAttributes.Assembly" />; that is, the field is visible at most to other types in the same assembly, and is not visible to derived types outside the assembly.</summary>
		/// <returns>true if the visibility of this field is exactly described by <see cref="F:System.Reflection.FieldAttributes.Assembly" />; otherwise, false.</returns>
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x00081B76 File Offset: 0x0007FD76
		public bool IsAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Assembly;
			}
		}

		/// <summary>Gets a value indicating whether the visibility of this field is described by <see cref="F:System.Reflection.FieldAttributes.FamANDAssem" />; that is, the field can be accessed from derived classes, but only if they are in the same assembly.</summary>
		/// <returns>true if access to this field is exactly described by <see cref="F:System.Reflection.FieldAttributes.FamANDAssem" />; otherwise, false.</returns>
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x00081B83 File Offset: 0x0007FD83
		public bool IsFamilyAndAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamANDAssem;
			}
		}

		/// <summary>Gets a value indicating whether the potential visibility of this field is described by <see cref="F:System.Reflection.FieldAttributes.FamORAssem" />; that is, the field can be accessed by derived classes wherever they are, and by classes in the same assembly.</summary>
		/// <returns>true if access to this field is exactly described by <see cref="F:System.Reflection.FieldAttributes.FamORAssem" />; otherwise, false.</returns>
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x00081B90 File Offset: 0x0007FD90
		public bool IsFamilyOrAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamORAssem;
			}
		}

		/// <summary>Gets a value indicating whether the corresponding PinvokeImpl attribute is set in <see cref="T:System.Reflection.FieldAttributes" />.</summary>
		/// <returns>true if the PinvokeImpl attribute is set in <see cref="T:System.Reflection.FieldAttributes" />; otherwise, false.</returns>
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x00081B9D File Offset: 0x0007FD9D
		public bool IsPinvokeImpl
		{
			get
			{
				return (this.Attributes & FieldAttributes.PinvokeImpl) == FieldAttributes.PinvokeImpl;
			}
		}

		/// <summary>Gets a value indicating whether the corresponding SpecialName attribute is set in the <see cref="T:System.Reflection.FieldAttributes" /> enumerator.</summary>
		/// <returns>true if the SpecialName attribute is set in <see cref="T:System.Reflection.FieldAttributes" />; otherwise, false.</returns>
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060022C2 RID: 8898 RVA: 0x00081BB2 File Offset: 0x0007FDB2
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & FieldAttributes.SpecialName) == FieldAttributes.SpecialName;
			}
		}

		/// <summary>Gets a value indicating whether this field has the NotSerialized attribute.</summary>
		/// <returns>true if the field has the NotSerialized attribute set; otherwise, false.</returns>
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x00081BC7 File Offset: 0x0007FDC7
		public bool IsNotSerialized
		{
			get
			{
				return (this.Attributes & FieldAttributes.NotSerialized) == FieldAttributes.NotSerialized;
			}
		}

		/// <summary>When overridden in a derived class, sets the value of the field supported by the given object.</summary>
		/// <param name="obj">The object whose field value will be set. </param>
		/// <param name="value">The value to assign to the field. </param>
		/// <param name="invokeAttr">A field of Binder that specifies the type of binding that is desired (for example, Binder.CreateInstance or Binder.ExactBinding). </param>
		/// <param name="binder">A set of properties that enables the binding, coercion of argument types, and invocation of members through reflection. If <paramref name="binder" /> is null, then Binder.DefaultBinding is used. </param>
		/// <param name="culture">The software preferences of a particular culture. </param>
		/// <exception cref="T:System.FieldAccessException">The caller does not have permission to access this field. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The <paramref name="obj" /> parameter is null and the field is an instance field. </exception>
		/// <exception cref="T:System.ArgumentException">The field does not exist on the object.-or- The <paramref name="value" /> parameter cannot be converted and stored in the field. </exception>
		// Token: 0x060022C4 RID: 8900
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		/// <summary>Sets the value of the field supported by the given object.</summary>
		/// <param name="obj">The object whose field value will be set. </param>
		/// <param name="value">The value to assign to the field. </param>
		/// <exception cref="T:System.FieldAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to access this field. </exception>
		/// <exception cref="T:System.Reflection.TargetException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.The <paramref name="obj" /> parameter is null and the field is an instance field. </exception>
		/// <exception cref="T:System.ArgumentException">The field does not exist on the object.-or- The <paramref name="value" /> parameter cannot be converted and stored in the field. </exception>
		// Token: 0x060022C5 RID: 8901 RVA: 0x00081BDC File Offset: 0x0007FDDC
		[DebuggerHidden]
		[DebuggerStepThrough]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, BindingFlags.Default, null, null);
		}

		// Token: 0x060022C6 RID: 8902
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern FieldInfo internal_from_handle_type(IntPtr field_handle, IntPtr type_handle);

		/// <summary>Gets a <see cref="T:System.Reflection.FieldInfo" /> for the field represented by the specified handle.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field specified by <paramref name="handle" />.</returns>
		/// <param name="handle">A <see cref="T:System.RuntimeFieldHandle" /> structure that contains the handle to the internal metadata representation of a field. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.</exception>
		// Token: 0x060022C7 RID: 8903 RVA: 0x00081BE9 File Offset: 0x0007FDE9
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			return FieldInfo.internal_from_handle_type(handle.Value, IntPtr.Zero);
		}

		/// <summary>Gets a <see cref="T:System.Reflection.FieldInfo" /> for the field represented by the specified handle, for the specified generic type.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field specified by <paramref name="handle" />, in the generic type specified by <paramref name="declaringType" />.</returns>
		/// <param name="handle">A <see cref="T:System.RuntimeFieldHandle" /> structure that contains the handle to the internal metadata representation of a field.</param>
		/// <param name="declaringType">A <see cref="T:System.RuntimeTypeHandle" /> structure that contains the handle to the generic type that defines the field.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.-or-<paramref name="declaringType" /> is not compatible with <paramref name="handle" />. For example, <paramref name="declaringType" /> is the runtime type handle of the generic type definition, and <paramref name="handle" /> comes from a constructed type. See Remarks.</exception>
		// Token: 0x060022C8 RID: 8904 RVA: 0x00081C1C File Offset: 0x0007FE1C
		[ComVisible(false)]
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			FieldInfo fieldInfo = FieldInfo.internal_from_handle_type(handle.Value, declaringType.Value);
			if (fieldInfo == null)
			{
				throw new ArgumentException("The field handle and the type handle are incompatible.");
			}
			return fieldInfo;
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x00081C6E File Offset: 0x0007FE6E
		internal virtual int GetFieldOffset()
		{
			throw new SystemException("This method should not be called");
		}

		/// <summary>Returns the value of a field supported by a given object.</summary>
		/// <returns>An Object containing a field value.</returns>
		/// <param name="obj">A <see cref="T:System.TypedReference" /> structure that encapsulates a managed pointer to a location and a runtime representation of the type that might be stored at that location. </param>
		/// <exception cref="T:System.NotSupportedException">The caller requires the Common Language Specification (CLS) alternative, but called this method instead. </exception>
		// Token: 0x060022CA RID: 8906 RVA: 0x00081C7A File Offset: 0x0007FE7A
		[CLSCompliant(false)]
		public virtual object GetValueDirect(TypedReference obj)
		{
			throw new NotSupportedException(Environment.GetResourceString("This non-CLS method is not implemented."));
		}

		/// <summary>Sets the value of the field supported by the given object.</summary>
		/// <param name="obj">A <see cref="T:System.TypedReference" /> structure that encapsulates a managed pointer to a location and a runtime representation of the type that can be stored at that location. </param>
		/// <param name="value">The value to assign to the field. </param>
		/// <exception cref="T:System.NotSupportedException">The caller requires the Common Language Specification (CLS) alternative, but called this method instead. </exception>
		// Token: 0x060022CB RID: 8907 RVA: 0x00081C7A File Offset: 0x0007FE7A
		[CLSCompliant(false)]
		public virtual void SetValueDirect(TypedReference obj, object value)
		{
			throw new NotSupportedException(Environment.GetResourceString("This non-CLS method is not implemented."));
		}

		// Token: 0x060022CC RID: 8908
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern MarshalAsAttribute get_marshal_info();

		// Token: 0x060022CD RID: 8909 RVA: 0x00081C8C File Offset: 0x0007FE8C
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			if (this.IsNotSerialized)
			{
				num++;
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				num++;
			}
			MarshalAsAttribute marshal_info = this.get_marshal_info();
			if (marshal_info != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if (this.IsNotSerialized)
			{
				array[num++] = new NonSerializedAttribute();
			}
			if (this.DeclaringType.IsExplicitLayout)
			{
				array[num++] = new FieldOffsetAttribute(this.GetFieldOffset());
			}
			if (marshal_info != null)
			{
				array[num++] = marshal_info;
			}
			return array;
		}

		// Token: 0x060022CE RID: 8910
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type[] GetTypeModifiers(bool optional);

		/// <summary>Gets an array of types that identify the optional custom modifiers of the field.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the optional custom modifiers of the current field, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />.</returns>
		// Token: 0x060022CF RID: 8911 RVA: 0x00081D14 File Offset: 0x0007FF14
		public virtual Type[] GetOptionalCustomModifiers()
		{
			Type[] typeModifiers = this.GetTypeModifiers(true);
			if (typeModifiers == null)
			{
				return Type.EmptyTypes;
			}
			return typeModifiers;
		}

		/// <summary>Gets an array of types that identify the required custom modifiers of the property.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the required custom modifiers of the current property, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x060022D0 RID: 8912 RVA: 0x00081D34 File Offset: 0x0007FF34
		public virtual Type[] GetRequiredCustomModifiers()
		{
			Type[] typeModifiers = this.GetTypeModifiers(false);
			if (typeModifiers == null)
			{
				return Type.EmptyTypes;
			}
			return typeModifiers;
		}

		/// <summary>Returns a literal value associated with the field by a compiler. </summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the literal value associated with the field. If the literal value is a class type with an element value of zero, the return value is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current field.</exception>
		/// <exception cref="T:System.FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification Metadata Logical Format: Other Structures, Element Types used in Signatures. </exception>
		/// <exception cref="T:System.NotSupportedException">The constant value for the field is not set. </exception>
		// Token: 0x060022D1 RID: 8913 RVA: 0x00081D53 File Offset: 0x0007FF53
		public virtual object GetRawConstantValue()
		{
			throw new NotSupportedException("This non-CLS method is not implemented.");
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x060022D2 RID: 8914 RVA: 0x0004BD58 File Offset: 0x00049F58
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060022D3 RID: 8915 RVA: 0x0007E911 File Offset: 0x0007CB11
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.FieldInfo" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x060022D4 RID: 8916 RVA: 0x00080329 File Offset: 0x0007E529
		public static bool operator ==(FieldInfo left, FieldInfo right)
		{
			return left == right || (!((left == null) ^ (right == null)) && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.FieldInfo" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x060022D5 RID: 8917 RVA: 0x00080345 File Offset: 0x0007E545
		public static bool operator !=(FieldInfo left, FieldInfo right)
		{
			return left != right && (((left == null) ^ (right == null)) || !left.Equals(right));
		}

		/// <summary>Gets a value that indicates whether the current field is security-critical or security-safe-critical at the current trust level. </summary>
		/// <returns>true if the current field is security-critical or security-safe-critical at the current trust level; false if it is transparent. </returns>
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060022D6 RID: 8918 RVA: 0x00014B5A File Offset: 0x00012D5A
		public virtual bool IsSecurityCritical
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether the current field is security-safe-critical at the current trust level. </summary>
		/// <returns>true if the current field is security-safe-critical at the current trust level; false if it is security-critical or transparent.</returns>
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x00014B5A File Offset: 0x00012D5A
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value that indicates whether the current field is transparent at the current trust level.</summary>
		/// <returns>true if the field is security-transparent at the current trust level; otherwise, false.</returns>
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060022D8 RID: 8920 RVA: 0x00014B5A File Offset: 0x00012D5A
		public virtual bool IsSecurityTransparent
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060022D9 RID: 8921 RVA: 0x0002126B File Offset: 0x0001F46B
		void _FieldInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.FieldInfo" /> type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.FieldInfo" /> type.</returns>
		// Token: 0x060022DA RID: 8922 RVA: 0x00033A19 File Offset: 0x00031C19
		Type _FieldInfo.GetType()
		{
			return base.GetType();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060022DB RID: 8923 RVA: 0x0002126B File Offset: 0x0001F46B
		void _FieldInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060022DC RID: 8924 RVA: 0x0002126B File Offset: 0x0001F46B
		void _FieldInfo.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x060022DD RID: 8925 RVA: 0x0002126B File Offset: 0x0001F46B
		void _FieldInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}
	}
}
