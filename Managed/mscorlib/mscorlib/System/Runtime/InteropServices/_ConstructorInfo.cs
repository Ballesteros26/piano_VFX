using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Exposes the public members of the <see cref="T:System.Reflection.ConstructorInfo" /> class to unmanaged code.</summary>
	// Token: 0x0200093E RID: 2366
	[ComVisible(true)]
	[CLSCompliant(false)]
	[TypeLibImportClass(typeof(ConstructorInfo))]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("E9A19478-9646-3679-9B10-8411AE1FD57D")]
	public interface _ConstructorInfo
	{
		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="other">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		// Token: 0x0600578E RID: 22414
		bool Equals(object other);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> method.</summary>
		/// <returns>An array that contains all the custom attributes, or an array with zero elements if no attributes are defined.</returns>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		// Token: 0x0600578F RID: 22415
		object[] GetCustomAttributes(bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Emit.MethodBuilder.GetCustomAttributes(System.Type,System.Boolean)" /> method.</summary>
		/// <returns>An array of custom attributes applied to this member, or an array with zero (0) elements if no attributes have been applied.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		// Token: 0x06005790 RID: 22416
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>The hash code for the current instance.</returns>
		// Token: 0x06005791 RID: 22417
		int GetHashCode();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MethodBase.GetMethodImplementationFlags" /> member.</summary>
		/// <returns>The <see cref="T:System.Reflection.MethodImplAttributes" /> flags.</returns>
		// Token: 0x06005792 RID: 22418
		MethodImplAttributes GetMethodImplementationFlags();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MethodBase.GetParameters" /> method.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.ParameterInfo" /> containing information that matches the signature of the method (or constructor) reflected by this instance.</returns>
		// Token: 0x06005793 RID: 22419
		ParameterInfo[] GetParameters();

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		// Token: 0x06005794 RID: 22420
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		// Token: 0x06005795 RID: 22421
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		// Token: 0x06005796 RID: 22422
		void GetTypeInfoCount(out uint pcTInfo);

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		// Token: 0x06005797 RID: 22423
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetType" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> object.</returns>
		// Token: 0x06005798 RID: 22424
		Type GetType();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Object[])" /> method.</summary>
		/// <returns>An instance of the class associated with the constructor.</returns>
		/// <param name="parameters">An array of values that matches the number, order, and type (under the constraints of the default binder) of the parameters for this constructor. If this constructor takes no parameters, then use either an array with zero elements or null, as in Object[] parameters = new Object[0]. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or false, depending on the specific element type.</param>
		// Token: 0x06005799 RID: 22425
		object Invoke_5(object[] parameters);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MethodBase.Invoke(System.Object,System.Object[])" /> method.</summary>
		/// <returns>An instance of the class associated with the constructor.</returns>
		/// <param name="obj">The instance that created this method. </param>
		/// <param name="parameters">An argument list for the invoked method or constructor. This is an array of objects with the same number, order, and type as the parameters of the method or constructor to be invoked. If there are no parameters, <paramref name="parameters" /> should be null.If the method or constructor represented by this instance takes a ref parameter (ByRef in Visual Basic), no special attribute is required for that parameter in order to invoke the method or constructor using this function. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or false, depending on the specific element type. </param>
		// Token: 0x0600579A RID: 22426
		object Invoke_3(object obj, object[] parameters);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> method.</summary>
		/// <returns>An instance of the class associated with the constructor.</returns>
		/// <param name="invokeAttr">One of the BindingFlags values that specifies the type of binding. </param>
		/// <param name="binder">A Binder that defines a set of properties and enables the binding, coercion of argument types, and invocation of members using reflection. If <paramref name="binder" /> is null, then Binder.DefaultBinding is used. </param>
		/// <param name="parameters">An array of type Object used to match the number, order, and type of the parameters for this constructor, under the constraints of <paramref name="binder" />. If this constructor does not require parameters, pass an array with zero elements, as in Object[] parameters = new Object[0]. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or false, depending on the specific element type. </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		// Token: 0x0600579B RID: 22427
		object Invoke_4(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MethodBase.Invoke(System.Object,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> method.</summary>
		/// <returns>An instance of the class associated with the constructor.</returns>
		/// <param name="obj">The instance that created this method.</param>
		/// <param name="invokeAttr">One of the BindingFlags values that specifies the type of binding.</param>
		/// <param name="binder">A Binder that defines a set of properties and enables the binding, coercion of argument types, and invocation of members using reflection. If <paramref name="binder" /> is null, then Binder.DefaultBinding is used.</param>
		/// <param name="parameters">An array of type Object used to match the number, order, and type of the parameters for this constructor, under the constraints of <paramref name="binder" />. If this constructor does not require parameters, pass an array with zero elements, as in Object[] parameters = new Object[0]. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or false, depending on the specific element type.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		// Token: 0x0600579C RID: 22428
		object Invoke_2(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> member.</summary>
		/// <returns>true if one or more instances of <paramref name="attributeType" /> is applied to this member; otherwise false.</returns>
		/// <param name="attributeType">The Type object to which the custom attributes are applied. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		// Token: 0x0600579D RID: 22429
		bool IsDefined(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Object" />.</returns>
		// Token: 0x0600579E RID: 22430
		string ToString();

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.Attributes" /> property.</summary>
		/// <returns>One of the <see cref="T:System.Reflection.MethodAttributes" /> values.</returns>
		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x0600579F RID: 22431
		MethodAttributes Attributes { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.CallingConvention" /> property.</summary>
		/// <returns>The <see cref="T:System.Reflection.CallingConventions" /> for this method.</returns>
		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x060057A0 RID: 22432
		CallingConventions CallingConvention { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.DeclaringType" /> property.</summary>
		/// <returns>The <see cref="T:System.Type" /> object for the class that declares this member.</returns>
		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x060057A1 RID: 22433
		Type DeclaringType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsAbstract" /> property.</summary>
		/// <returns>true if the method is abstract; otherwise, false.</returns>
		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x060057A2 RID: 22434
		bool IsAbstract { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsAssembly" /> property.</summary>
		/// <returns>true if this method can be called by other classes in the same assembly; otherwise, false.</returns>
		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x060057A3 RID: 22435
		bool IsAssembly { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsConstructor" /> property.</summary>
		/// <returns>true if this method is a constructor; otherwise, false.</returns>
		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x060057A4 RID: 22436
		bool IsConstructor { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsFamily" /> property.</summary>
		/// <returns>true if access to the class is restricted to members of the class itself and to members of its derived classes; otherwise, false.</returns>
		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x060057A5 RID: 22437
		bool IsFamily { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsFamilyAndAssembly" /> property.</summary>
		/// <returns>true if access to this method is restricted to members of the class itself and to members of derived classes that are in the same assembly; otherwise, false.</returns>
		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x060057A6 RID: 22438
		bool IsFamilyAndAssembly { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsFamilyOrAssembly" /> property.</summary>
		/// <returns>true if access to this method is restricted to members of the class itself, members of derived classes wherever they are, and members of other classes in the same assembly; otherwise, false.</returns>
		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x060057A7 RID: 22439
		bool IsFamilyOrAssembly { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsFinal" /> property.</summary>
		/// <returns>true if this method is final; otherwise, false.</returns>
		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x060057A8 RID: 22440
		bool IsFinal { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsHideBySig" /> property.</summary>
		/// <returns>true if the member is hidden by signature; otherwise, false.</returns>
		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x060057A9 RID: 22441
		bool IsHideBySig { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsPrivate" /> property.</summary>
		/// <returns>true if access to this method is restricted to other members of the class itself; otherwise, false.</returns>
		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x060057AA RID: 22442
		bool IsPrivate { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsPublic" /> property.</summary>
		/// <returns>true if this method is public; otherwise, false.</returns>
		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x060057AB RID: 22443
		bool IsPublic { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsSpecialName" /> property.</summary>
		/// <returns>true if this method has a special name; otherwise, false.</returns>
		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x060057AC RID: 22444
		bool IsSpecialName { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsStatic" /> property.</summary>
		/// <returns>true if this method is static; otherwise, false.</returns>
		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x060057AD RID: 22445
		bool IsStatic { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsVirtual" /> property.</summary>
		/// <returns>true if this method is virtual; otherwise, false.</returns>
		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x060057AE RID: 22446
		bool IsVirtual { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.ConstructorInfo.MemberType" /> property.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating the type of member.</returns>
		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x060057AF RID: 22447
		MemberTypes MemberType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.MethodHandle" /> property.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> object.</returns>
		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x060057B0 RID: 22448
		RuntimeMethodHandle MethodHandle { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.Name" /> property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of this member.</returns>
		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x060057B1 RID: 22449
		string Name { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.ReflectedType" /> property.</summary>
		/// <returns>The <see cref="T:System.Type" /> object through which this <see cref="T:System.Reflection.MemberInfo" /> object was obtained.</returns>
		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x060057B2 RID: 22450
		Type ReflectedType { get; }
	}
}
