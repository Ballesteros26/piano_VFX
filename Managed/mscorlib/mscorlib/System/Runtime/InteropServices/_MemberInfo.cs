using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Exposes the public members of the <see cref="T:System.Reflection.MemberInfo" /> class to unmanaged code.</summary>
	// Token: 0x02000948 RID: 2376
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[CLSCompliant(false)]
	[ComVisible(true)]
	[TypeLibImportClass(typeof(MemberInfo))]
	[Guid("f7102fa9-cabb-3a74-a6da-b4567ef1b079")]
	public interface _MemberInfo
	{
		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="other">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />.</param>
		// Token: 0x06005817 RID: 22551
		bool Equals(object other);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> method.</summary>
		/// <returns>An array that contains all the custom attributes, or an array with zero (0) elements if no attributes are defined.</returns>
		/// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false.</param>
		// Token: 0x06005818 RID: 22552
		object[] GetCustomAttributes(bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.Assembly.GetCustomAttributes(System.Type,System.Boolean)" /> method.</summary>
		/// <returns>An array of custom attributes applied to this member, or an array with zero (0) elements if no attributes have been applied.</returns>
		/// <param name="attributeType">The type of attribute to search for. Only attributes that are assignable to this type are returned. </param>
		/// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false. </param>
		// Token: 0x06005819 RID: 22553
		object[] GetCustomAttributes(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>The hash code for the current instance.</returns>
		// Token: 0x0600581A RID: 22554
		int GetHashCode();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Type.GetType" /> method.</summary>
		/// <returns>A <see cref="T:System.Type" /> object.</returns>
		// Token: 0x0600581B RID: 22555
		Type GetType();

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> method.</summary>
		/// <returns>true if one or more instance of the <paramref name="attributeType" /> parameter is applied to this member; otherwise, false.</returns>
		/// <param name="attributeType">The <see cref="T:System.Type" /> object to which the custom attributes are applied. </param>
		/// <param name="inherit">true to search this member's inheritance chain to find the attributes; otherwise, false. </param>
		// Token: 0x0600581C RID: 22556
		bool IsDefined(Type attributeType, bool inherit);

		/// <summary>Provides COM objects with version-independent access to the <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Object" />.</returns>
		// Token: 0x0600581D RID: 22557
		string ToString();

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.DeclaringType" /> property.</summary>
		/// <returns>The <see cref="T:System.Type" /> object for the class that declares this member.</returns>
		// Token: 0x17000F47 RID: 3911
		// (get) Token: 0x0600581E RID: 22558
		Type DeclaringType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.MemberType" /> property.</summary>
		/// <returns>One of the <see cref="T:System.Reflection.MemberTypes" /> values indicating the type of member.</returns>
		// Token: 0x17000F48 RID: 3912
		// (get) Token: 0x0600581F RID: 22559
		MemberTypes MemberType { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.Name" /> property.</summary>
		/// <returns>A <see cref="T:System.String" /> object containing the name of this member.</returns>
		// Token: 0x17000F49 RID: 3913
		// (get) Token: 0x06005820 RID: 22560
		string Name { get; }

		/// <summary>Provides COM objects with version-independent access to the <see cref="P:System.Reflection.MemberInfo.ReflectedType" /> property.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that was used to obtain this <see cref="T:System.Reflection.MemberInfo" /> object.</returns>
		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06005821 RID: 22561
		Type ReflectedType { get; }

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">An  array of names to be mapped.</param>
		/// <param name="cNames">The count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">An array allocated by the caller that receives the identifiers corresponding to the names.</param>
		// Token: 0x06005822 RID: 22562
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		/// <summary>Retrieves the type information for an object, which can be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">A pointer to the requested type information object.</param>
		// Token: 0x06005823 RID: 22563
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">When this method returns, contains a pointer to a location that receives the number of type information interfaces provided by the object. This parameter is passed uninitialized.</param>
		// Token: 0x06005824 RID: 22564
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
		// Token: 0x06005825 RID: 22565
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
