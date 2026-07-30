using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Exposes the public members of the <see cref="T:System.Reflection.MethodInfo" /> class to unmanaged code.</summary>
	// Token: 0x0200094B RID: 2379
	[TypeLibImportClass(typeof(MethodInfo))]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[ComVisible(true)]
	[Guid("FFCC1B5D-ECB8-38DD-9B01-3DC8ABC2AA5F")]
	public interface _MethodInfo
	{
		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="other">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		// Token: 0x0600584D RID: 22605
		bool Equals(object other);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MethodInfo.GetBaseDefinition" /> method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> object for the first implementation of this method.</returns>
		// Token: 0x0600584E RID: 22606
		MethodInfo GetBaseDefinition();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> method.</summary>
		/// <returns>An array that contains all the custom attributes, or an array with zero (0) elements if no attributes are defined.</returns>
		/// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false.</param>
		// Token: 0x0600584F RID: 22607
		object[] GetCustomAttributes(bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Type,System.Boolean)" /> method.</summary>
		/// <returns>An array of custom attributes applied to this member, or an array with zero (0) elements if no attributes have been applied.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned. </param>
		/// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false. </param>
		// Token: 0x06005850 RID: 22608
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>The hash code for the current instance.</returns>
		// Token: 0x06005851 RID: 22609
		int GetHashCode();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MethodBase.GetMethodImplementationFlags" /> method.</summary>
		/// <returns>One of the <see cref="T:System.Reflection.MethodImplAttributes" /> values.</returns>
		// Token: 0x06005852 RID: 22610
		MethodImplAttributes GetMethodImplementationFlags();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MethodBase.GetParameters" /> method.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.ParameterInfo" /> containing information that matches the signature of the method (or constructor) reflected by this instance.</returns>
		// Token: 0x06005853 RID: 22611
		ParameterInfo[] GetParameters();

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">An array of names to be mapped.</param>
		/// <param name="cNames">The count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">An array allocated by the caller that receives the identifiers corresponding to the names.</param>
		// Token: 0x06005854 RID: 22612
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		/// <summary>Retrieves the type information for an object, which can be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">A pointer to the requested type information object.</param>
		// Token: 0x06005855 RID: 22613
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">When this method returns, contains a pointer to a location that receives the number of type information interfaces provided by the object. This parameter is passed uninitialized.</param>
		// Token: 0x06005856 RID: 22614
		void GetTypeInfoCount(out uint pcTInfo);

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">An identifier for the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">A pointer to a structure containing an array of arguments, an array of argument DISPIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">A pointer to the location where the result will be stored.</param>
		/// <param name="pExcepInfo">A pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		// Token: 0x06005857 RID: 22615
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetType" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> object.</returns>
		// Token: 0x06005858 RID: 22616
		Type GetType();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MethodBase.Invoke(System.Object,System.Object[])" /> method.</summary>
		/// <returns>An instance of the class associated with the constructor.</returns>
		/// <param name="obj">The instance that created this method. </param>
		/// <param name="parameters">An argument list for the invoked method or constructor. This is an array of objects with the same number, order, and type as the parameters of the method or constructor to be invoked. If there are no parameters, <paramref name="parameters" /> should be null.If the method or constructor represented by this instance takes a ref parameter (ByRef in Visual Basic), no special attribute is required for that parameter to invoke the method or constructor using this function. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference type elements, this value is null. For value type elements, this value is 0, 0.0, or false, depending on the specific element type. </param>
		// Token: 0x06005859 RID: 22617
		object Invoke(object obj, object[] parameters);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MethodBase.Invoke(System.Object,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> method.</summary>
		/// <returns>An instance of the class associated with the constructor.</returns>
		/// <param name="obj">The instance that created this method.</param>
		/// <param name="invokeAttr">One of the BindingFlags values that specifies the type of binding.</param>
		/// <param name="binder">A Binder that defines a set of properties and enables the binding, coercion of argument types, and invocation of members using reflection. If <paramref name="binder" /> is null, then Binder.DefaultBinding is used.</param>
		/// <param name="parameters">An array of type Object used to match the number, order, and type of the parameters for this constructor, under the constraints of <paramref name="binder" />. If this constructor does not require parameters, pass an array with zero elements, as in Object[] parameters = new Object[0]. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference type elements, this value is null. For value type elements, this value is 0, 0.0, or false, depending on the specific element type.</param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> object used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used.</param>
		// Token: 0x0600585A RID: 22618
		object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> method.</summary>
		/// <returns>true if one or more instance of the <paramref name="attributeType" /> parameter is applied to this member; otherwise, false.</returns>
		/// <param name="attributeType">The Type object to which the custom attributes are applied. </param>
		/// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false. </param>
		// Token: 0x0600585B RID: 22619
		bool IsDefined(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Object" />.</returns>
		// Token: 0x0600585C RID: 22620
		string ToString();

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.Attributes" /> property.</summary>
		/// <returns>One of the <see cref="T:System.Reflection.MethodAttributes" /> values.</returns>
		// Token: 0x17000F5F RID: 3935
		// (get) Token: 0x0600585D RID: 22621
		MethodAttributes Attributes { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.CallingConvention" /> property.</summary>
		/// <returns>One of the <see cref="T:System.Reflection.CallingConventions" /> values.</returns>
		// Token: 0x17000F60 RID: 3936
		// (get) Token: 0x0600585E RID: 22622
		CallingConventions CallingConvention { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.DeclaringType" /> property.</summary>
		/// <returns>The Type object for the class that declares this member.</returns>
		// Token: 0x17000F61 RID: 3937
		// (get) Token: 0x0600585F RID: 22623
		Type DeclaringType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsAbstract" /> property.</summary>
		/// <returns>true if the method is abstract; otherwise, false.</returns>
		// Token: 0x17000F62 RID: 3938
		// (get) Token: 0x06005860 RID: 22624
		bool IsAbstract { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsAssembly" /> property.</summary>
		/// <returns>true if this method can be called by other classes in the same assembly; otherwise, false.</returns>
		// Token: 0x17000F63 RID: 3939
		// (get) Token: 0x06005861 RID: 22625
		bool IsAssembly { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsConstructor" /> property.</summary>
		/// <returns>true if this method is a constructor; otherwise, false.</returns>
		// Token: 0x17000F64 RID: 3940
		// (get) Token: 0x06005862 RID: 22626
		bool IsConstructor { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsFamily" /> property.</summary>
		/// <returns>true if access to the class is restricted to members of the class itself and to members of its derived classes; otherwise, false.</returns>
		// Token: 0x17000F65 RID: 3941
		// (get) Token: 0x06005863 RID: 22627
		bool IsFamily { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsFamilyAndAssembly" /> property.</summary>
		/// <returns>true if access to this method is restricted to members of the class itself and to members of derived classes that are in the same assembly; otherwise, false.</returns>
		// Token: 0x17000F66 RID: 3942
		// (get) Token: 0x06005864 RID: 22628
		bool IsFamilyAndAssembly { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsFamilyOrAssembly" /> property.</summary>
		/// <returns>true if access to this method is restricted to members of the class itself, members of derived classes wherever they are, and members of other classes in the same assembly; otherwise, false.</returns>
		// Token: 0x17000F67 RID: 3943
		// (get) Token: 0x06005865 RID: 22629
		bool IsFamilyOrAssembly { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsFinal" /> property.</summary>
		/// <returns>true if this method is final; otherwise, false.</returns>
		// Token: 0x17000F68 RID: 3944
		// (get) Token: 0x06005866 RID: 22630
		bool IsFinal { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsHideBySig" /> property.</summary>
		/// <returns>true if the member is hidden by signature; otherwise, false.</returns>
		// Token: 0x17000F69 RID: 3945
		// (get) Token: 0x06005867 RID: 22631
		bool IsHideBySig { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsPrivate" /> property.</summary>
		/// <returns>true if access to this method is restricted to other members of the class itself; otherwise, false.</returns>
		// Token: 0x17000F6A RID: 3946
		// (get) Token: 0x06005868 RID: 22632
		bool IsPrivate { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsPublic" /> property.</summary>
		/// <returns>true if this method is public; otherwise, false.</returns>
		// Token: 0x17000F6B RID: 3947
		// (get) Token: 0x06005869 RID: 22633
		bool IsPublic { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsSpecialName" /> property.</summary>
		/// <returns>true if this method has a special name; otherwise, false.</returns>
		// Token: 0x17000F6C RID: 3948
		// (get) Token: 0x0600586A RID: 22634
		bool IsSpecialName { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsStatic" /> property.</summary>
		/// <returns>true if this method is static; otherwise, false.</returns>
		// Token: 0x17000F6D RID: 3949
		// (get) Token: 0x0600586B RID: 22635
		bool IsStatic { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.IsVirtual" /> property.</summary>
		/// <returns>true if this method is virtual; otherwise, false.</returns>
		// Token: 0x17000F6E RID: 3950
		// (get) Token: 0x0600586C RID: 22636
		bool IsVirtual { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.MemberType" /> property.</summary>
		/// <returns>One of the <see cref="T:System.Reflection.MemberTypes" /> values indicating the type of member.</returns>
		// Token: 0x17000F6F RID: 3951
		// (get) Token: 0x0600586D RID: 22637
		MemberTypes MemberType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodBase.MethodHandle" /> property.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> object.</returns>
		// Token: 0x17000F70 RID: 3952
		// (get) Token: 0x0600586E RID: 22638
		RuntimeMethodHandle MethodHandle { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.Name" /> property.</summary>
		/// <returns>A <see cref="T:System.String" /> object containing the name of this member.</returns>
		// Token: 0x17000F71 RID: 3953
		// (get) Token: 0x0600586F RID: 22639
		string Name { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.ReflectedType" /> property.</summary>
		/// <returns>The Type object that was used to obtain this MemberInfo object.</returns>
		// Token: 0x17000F72 RID: 3954
		// (get) Token: 0x06005870 RID: 22640
		Type ReflectedType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodInfo.ReturnType" /> property.</summary>
		/// <returns>The return type of this method.</returns>
		// Token: 0x17000F73 RID: 3955
		// (get) Token: 0x06005871 RID: 22641
		Type ReturnType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MethodInfo.ReturnTypeCustomAttributes" /> property.</summary>
		/// <returns>An <see cref="T:System.Reflection.ICustomAttributeProvider" /> object representing the custom attributes for the return type.</returns>
		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x06005872 RID: 22642
		ICustomAttributeProvider ReturnTypeCustomAttributes { get; }
	}
}
