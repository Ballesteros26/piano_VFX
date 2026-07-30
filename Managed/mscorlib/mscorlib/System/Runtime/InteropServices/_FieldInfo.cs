using System;
using System.Globalization;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Exposes the public members of the <see cref="T:System.Reflection.FieldInfo" /> class to unmanaged code.</summary>
	// Token: 0x02000945 RID: 2373
	[Guid("8A7C1442-A9FB-366B-80D8-4939FFA6DBE0")]
	[TypeLibImportClass(typeof(FieldInfo))]
	[ComVisible(true)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	public interface _FieldInfo
	{
		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="other">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		// Token: 0x060057EC RID: 22508
		bool Equals(object other);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> method.</summary>
		/// <returns>An array that contains all the custom attributes, or an array with zero elements if no attributes are defined.</returns>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes.</param>
		// Token: 0x060057ED RID: 22509
		object[] GetCustomAttributes(bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Type,System.Boolean)" /> method.</summary>
		/// <returns>An array of custom attributes applied to this member, or an array with zero (0) elements if no attributes have been applied.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		// Token: 0x060057EE RID: 22510
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>The hash code for the current instance.</returns>
		// Token: 0x060057EF RID: 22511
		int GetHashCode();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetType" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> object.</returns>
		// Token: 0x060057F0 RID: 22512
		Type GetType();

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		// Token: 0x060057F1 RID: 22513
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		// Token: 0x060057F2 RID: 22514
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		// Token: 0x060057F3 RID: 22515
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
		// Token: 0x060057F4 RID: 22516
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.FieldInfo.GetValue(System.Object)" /> method.</summary>
		/// <returns>An object containing the value of the field reflected by this instance.</returns>
		/// <param name="obj">The object whose field value will be returned.</param>
		// Token: 0x060057F5 RID: 22517
		object GetValue(object obj);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.FieldInfo.GetValueDirect(System.TypedReference)" /> method.</summary>
		/// <returns>An <see cref="T:System.Object" /> containing a field value.</returns>
		/// <param name="obj">A <see cref="T:System.TypedReference" /> structure that encapsulates a managed pointer to a location and a runtime representation of the type that might be stored at that location.</param>
		// Token: 0x060057F6 RID: 22518
		object GetValueDirect(TypedReference obj);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> method.</summary>
		/// <returns>true if one or more instance of <paramref name="attributeType" /> is applied to this member; otherwise, false.</returns>
		/// <param name="attributeType">The <see cref="T:System.Type" /> object to which the custom attributes are applied. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the attributes. </param>
		// Token: 0x060057F7 RID: 22519
		bool IsDefined(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.FieldInfo.SetValue(System.Object,System.Object)" /> method.</summary>
		/// <param name="obj">The object whose field value will be set.</param>
		/// <param name="value">The value to assign to the field. </param>
		// Token: 0x060057F8 RID: 22520
		void SetValue(object obj, object value);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.PropertyInfo.SetValue(System.Object,System.Object,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> method.</summary>
		/// <param name="obj">The object whose field value will be set. </param>
		/// <param name="value">The value to assign to the field. </param>
		/// <param name="invokeAttr">A field of <see cref="T:System.Reflection.Binder" /> that specifies the type of binding that is desired (for example, Binder.CreateInstance or Binder.ExactBinding). </param>
		/// <param name="binder">A set of properties that enables the binding, coercion of argument types, and invocation of members through reflection. If <paramref name="binder" /> is null, then Binder.DefaultBinding is used. </param>
		/// <param name="culture">The software preferences of a particular culture. </param>
		// Token: 0x060057F9 RID: 22521
		void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.FieldInfo.SetValueDirect(System.TypedReference,System.Object)" /> method.</summary>
		/// <param name="obj">The object whose field value will be set.</param>
		/// <param name="value">The value to assign to the field.</param>
		// Token: 0x060057FA RID: 22522
		void SetValueDirect(TypedReference obj, object value);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Object" />.</returns>
		// Token: 0x060057FB RID: 22523
		string ToString();

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.Attributes" /> property. </summary>
		/// <returns>The <see cref="T:System.Reflection.FieldAttributes" /> for this field.</returns>
		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x060057FC RID: 22524
		FieldAttributes Attributes { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.DeclaringType" /> property. </summary>
		/// <returns>The <see cref="T:System.Type" /> object for the class that declares this member.</returns>
		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x060057FD RID: 22525
		Type DeclaringType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.FieldHandle" /> property.</summary>
		/// <returns>A handle to the internal metadata representation of a field.</returns>
		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x060057FE RID: 22526
		RuntimeFieldHandle FieldHandle { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.FieldType" /> property.</summary>
		/// <returns>The type of this field object.</returns>
		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x060057FF RID: 22527
		Type FieldType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.IsAssembly" /> property.</summary>
		/// <returns>true if the field has the Assembly attribute set; otherwise, false.</returns>
		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06005800 RID: 22528
		bool IsAssembly { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.IsFamily" /> property.</summary>
		/// <returns>true if the field has the Family attribute set; otherwise, false.</returns>
		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06005801 RID: 22529
		bool IsFamily { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.IsFamilyAndAssembly" /> property.</summary>
		/// <returns>true if the field has the FamANDAssem attribute set; otherwise, false.</returns>
		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06005802 RID: 22530
		bool IsFamilyAndAssembly { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.IsFamilyOrAssembly" /> property.</summary>
		/// <returns>true if the field has the FamORAssem attribute set; otherwise, false.</returns>
		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x06005803 RID: 22531
		bool IsFamilyOrAssembly { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.IsInitOnly" /> property.</summary>
		/// <returns>true if the field has the InitOnly attribute set; otherwise, false.</returns>
		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x06005804 RID: 22532
		bool IsInitOnly { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.IsLiteral" /> property.</summary>
		/// <returns>true if the field has the Literal attribute set; otherwise, false.</returns>
		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06005805 RID: 22533
		bool IsLiteral { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.IsNotSerialized" /> property.</summary>
		/// <returns>true if the field has the NotSerialized attribute set; otherwise, false.</returns>
		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06005806 RID: 22534
		bool IsNotSerialized { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.IsPinvokeImpl" /> property.</summary>
		/// <returns>true if the PinvokeImpl attribute is set in <see cref="T:System.Reflection.FieldAttributes" />; otherwise, false.</returns>
		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06005807 RID: 22535
		bool IsPinvokeImpl { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.IsPrivate" /> property.</summary>
		/// <returns>true if the field is private; otherwise; false.</returns>
		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06005808 RID: 22536
		bool IsPrivate { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.IsPublic" /> property.</summary>
		/// <returns>true if this field is public; otherwise, false.</returns>
		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06005809 RID: 22537
		bool IsPublic { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.IsSpecialName" /> property.</summary>
		/// <returns>true if the SpecialName attribute is set in <see cref="T:System.Reflection.FieldAttributes" />; otherwise, false.</returns>
		// Token: 0x17000F42 RID: 3906
		// (get) Token: 0x0600580A RID: 22538
		bool IsSpecialName { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.IsStatic" /> property.</summary>
		/// <returns>true if this field is static; otherwise, false.</returns>
		// Token: 0x17000F43 RID: 3907
		// (get) Token: 0x0600580B RID: 22539
		bool IsStatic { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.FieldInfo.MemberType" /> property.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a field.</returns>
		// Token: 0x17000F44 RID: 3908
		// (get) Token: 0x0600580C RID: 22540
		MemberTypes MemberType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.Name" /> property.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of this member.</returns>
		// Token: 0x17000F45 RID: 3909
		// (get) Token: 0x0600580D RID: 22541
		string Name { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.ReflectedType" /> property.</summary>
		/// <returns>The <see cref="T:System.Type" /> object through which this object was obtained.</returns>
		// Token: 0x17000F46 RID: 3910
		// (get) Token: 0x0600580E RID: 22542
		Type ReflectedType { get; }
	}
}
