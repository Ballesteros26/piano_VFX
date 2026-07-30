using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System
{
	/// <summary>Represents type declarations: class types, interface types, array types, value types, enumeration types, type parameters, generic type definitions, and open or closed constructed generic types.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001DD RID: 477
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_Type))]
	[Serializable]
	public abstract class Type : MemberInfo, _Type, IReflect
	{
		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a type or a nested type.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a type or a nested type.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x00057049 File Offset: 0x00055249
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.TypeInfo;
			}
		}

		/// <summary>Gets the type that declares the current nested type or generic type parameter.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the enclosing type, if the current type is a nested type; or the generic type definition, if the current type is a type parameter of a generic type; or the type that declares the generic method, if the current type is a type parameter of a generic method; otherwise, null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0000A42E File Offset: 0x0000862E
		public override Type DeclaringType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodBase" /> that represents the declaring method, if the current <see cref="T:System.Type" /> represents a type parameter of a generic method.</summary>
		/// <returns>If the current <see cref="T:System.Type" /> represents a type parameter of a generic method, a <see cref="T:System.Reflection.MethodBase" /> that represents declaring method; otherwise, null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x0000A42E File Offset: 0x0000862E
		public virtual MethodBase DeclaringMethod
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the class object that was used to obtain this member. </summary>
		/// <returns>The Type object through which this <see cref="T:System.Type" /> object was obtained. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x0000A42E File Offset: 0x0000862E
		public override Type ReflectedType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the type with the specified name, optionally providing custom methods to resolve the assembly and the type.</summary>
		/// <returns>The type with the specified name, or null if the type is not found. </returns>
		/// <param name="typeName">The name of the type to get. If the <paramref name="typeResolver" /> parameter is provided, the type name can be any string that <paramref name="typeResolver" /> is capable of resolving. If the <paramref name="assemblyResolver" /> parameter is provided or if standard type resolution is used, <paramref name="typeName" /> must be an assembly-qualified name (see <see cref="P:System.Type.AssemblyQualifiedName" />), unless the type is in the currently executing assembly or in Mscorlib.dll, in which case it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="assemblyResolver">A method that locates and returns the assembly that is specified in <paramref name="typeName" />. The assembly name is passed to <paramref name="assemblyResolver" /> as an <see cref="T:System.Reflection.AssemblyName" /> object. If <paramref name="typeName" /> does not contain the name of an assembly, <paramref name="assemblyResolver" /> is not called. If <paramref name="assemblyResolver" /> is not supplied, standard assembly resolution is performed. Caution   Do not pass methods from unknown or untrusted callers. Doing so could result in elevation of privilege for malicious code. Use only methods that you provide or that you are familiar with. </param>
		/// <param name="typeResolver">A method that locates and returns the type that is specified by <paramref name="typeName" /> from the assembly that is returned by <paramref name="assemblyResolver" /> or by standard assembly resolution. If no assembly is provided, the <paramref name="typeResolver" /> method can provide one. The method also takes a parameter that specifies whether to perform a case-insensitive search; false is passed to that parameter. Caution   Do not pass methods from unknown or untrusted callers. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.ArgumentException">An error occurs when <paramref name="typeName" /> is parsed into a type name and an assembly name (for example, when the simple type name includes an unescaped special character).-or-<paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments.-or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. -or-<paramref name="typeName" /> contains an invalid assembly name.-or-<paramref name="typeName" /> is a valid assembly name without a type name.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		// Token: 0x06001525 RID: 5413 RVA: 0x00057050 File Offset: 0x00055250
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, false, false, ref stackCrawlMark);
		}

		/// <summary>Gets the type with the specified name, specifying whether to throw an exception if the type is not found, and optionally providing custom methods to resolve the assembly and the type.</summary>
		/// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError" /> parameter specifies whether null is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError" />. See the Exceptions section. </returns>
		/// <param name="typeName">The name of the type to get. If the <paramref name="typeResolver" /> parameter is provided, the type name can be any string that <paramref name="typeResolver" /> is capable of resolving. If the <paramref name="assemblyResolver" /> parameter is provided or if standard type resolution is used, <paramref name="typeName" /> must be an assembly-qualified name (see <see cref="P:System.Type.AssemblyQualifiedName" />), unless the type is in the currently executing assembly or in Mscorlib.dll, in which case it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="assemblyResolver">A method that locates and returns the assembly that is specified in <paramref name="typeName" />. The assembly name is passed to <paramref name="assemblyResolver" /> as an <see cref="T:System.Reflection.AssemblyName" /> object. If <paramref name="typeName" /> does not contain the name of an assembly, <paramref name="assemblyResolver" /> is not called. If <paramref name="assemblyResolver" /> is not supplied, standard assembly resolution is performed. Caution   Do not pass methods from unknown or untrusted callers. Doing so could result in elevation of privilege for malicious code. Use only methods that you provide or that you are familiar with. </param>
		/// <param name="typeResolver">A method that locates and returns the type that is specified by <paramref name="typeName" /> from the assembly that is returned by <paramref name="assemblyResolver" /> or by standard assembly resolution. If no assembly is provided, the method can provide one. The method also takes a parameter that specifies whether to perform a case-insensitive search; false is passed to that parameter. Caution   Do not pass methods from unknown or untrusted callers. </param>
		/// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null. Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true and the type is not found. -or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> is an empty string.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> represents an array type with an invalid size. -or-<paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />. </exception>
		/// <exception cref="T:System.ArgumentException">An error occurs when <paramref name="typeName" /> is parsed into a type name and an assembly name (for example, when the simple type name includes an unescaped special character).-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid syntax (for example, "MyType[,*,]").-or- <paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments.-or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwOnError" /> is true and the assembly or one of its dependencies was not found. -or-<paramref name="typeName" /> contains an invalid assembly name.-or-<paramref name="typeName" /> is a valid assembly name without a type name.</exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		// Token: 0x06001526 RID: 5414 RVA: 0x0005706C File Offset: 0x0005526C
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, throwOnError, false, ref stackCrawlMark);
		}

		/// <summary>Gets the type with the specified name, specifying whether to perform a case-sensitive search and whether to throw an exception if the type is not found, and optionally providing custom methods to resolve the assembly and the type.</summary>
		/// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError" /> parameter specifies whether null is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError" />. See the Exceptions section. </returns>
		/// <param name="typeName">The name of the type to get. If the <paramref name="typeResolver" /> parameter is provided, the type name can be any string that <paramref name="typeResolver" /> is capable of resolving. If the <paramref name="assemblyResolver" /> parameter is provided or if standard type resolution is used, <paramref name="typeName" /> must be an assembly-qualified name (see <see cref="P:System.Type.AssemblyQualifiedName" />), unless the type is in the currently executing assembly or in Mscorlib.dll, in which case it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="assemblyResolver">A method that locates and returns the assembly that is specified in <paramref name="typeName" />. The assembly name is passed to <paramref name="assemblyResolver" /> as an <see cref="T:System.Reflection.AssemblyName" /> object. If <paramref name="typeName" /> does not contain the name of an assembly, <paramref name="assemblyResolver" /> is not called. If <paramref name="assemblyResolver" /> is not supplied, standard assembly resolution is performed. Caution   Do not pass methods from unknown or untrusted callers. Doing so could result in elevation of privilege for malicious code. Use only methods that you provide or that you are familiar with.</param>
		/// <param name="typeResolver">A method that locates and returns the type that is specified by <paramref name="typeName" /> from the assembly that is returned by <paramref name="assemblyResolver" /> or by standard assembly resolution. If no assembly is provided, the method can provide one. The method also takes a parameter that specifies whether to perform a case-insensitive search; the value of <paramref name="ignoreCase" /> is passed to that parameter. Caution   Do not pass methods from unknown or untrusted callers. </param>
		/// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null. Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <param name="ignoreCase">true to perform a case-insensitive search for <paramref name="typeName" />, false to perform a case-sensitive search for <paramref name="typeName" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true and the type is not found. -or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> is an empty string.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> represents an array type with an invalid size. -or-<paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />. </exception>
		/// <exception cref="T:System.ArgumentException">An error occurs when <paramref name="typeName" /> is parsed into a type name and an assembly name (for example, when the simple type name includes an unescaped special character).-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid syntax (for example, "MyType[,*,]").-or- <paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments.-or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwOnError" /> is true and the assembly or one of its dependencies was not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. -or-<paramref name="typeName" /> contains an invalid assembly name.-or-<paramref name="typeName" /> is a valid assembly name without a type name.</exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		// Token: 0x06001527 RID: 5415 RVA: 0x00057088 File Offset: 0x00055288
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static Type GetType(string typeName, Func<AssemblyName, Assembly> assemblyResolver, Func<Assembly, string, bool, Type> typeResolver, bool throwOnError, bool ignoreCase)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return TypeNameParser.GetType(typeName, assemblyResolver, typeResolver, throwOnError, ignoreCase, ref stackCrawlMark);
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a pointer to the current type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents a pointer to the current type.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.-or-The current type is a ByRef type. That is, <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001528 RID: 5416 RVA: 0x00014B5A File Offset: 0x00012D5A
		public virtual Type MakePointerType()
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> that describes the layout of the current type.</summary>
		/// <returns>Gets a <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> that describes the gross layout features of the current type.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00014B5A File Offset: 0x00012D5A
		public virtual StructLayoutAttribute StructLayoutAttribute
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents the current type when passed as a ref parameter (ByRef parameter in Visual Basic).</summary>
		/// <returns>A <see cref="T:System.Type" /> object that represents the current type when passed as a ref parameter (ByRef parameter in Visual Basic).</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.-or-The current type is a ByRef type. That is, <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600152A RID: 5418 RVA: 0x00014B5A File Offset: 0x00012D5A
		public virtual Type MakeByRefType()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object representing a one-dimensional array of the current type, with a lower bound of zero.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a one-dimensional array of the current type, with a lower bound of zero.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.-or-The current type is a ByRef type. That is, <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600152B RID: 5419 RVA: 0x00014B5A File Offset: 0x00012D5A
		public virtual Type MakeArrayType()
		{
			throw new NotSupportedException();
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object representing an array of the current type, with the specified number of dimensions.</summary>
		/// <returns>An object representing an array of the current type, with the specified number of dimensions.</returns>
		/// <param name="rank">The number of dimensions for the array. This number must be less than or equal to 32.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">
		///   <paramref name="rank" /> is invalid. For example, 0 or negative.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <exception cref="T:System.TypeLoadException">The current type is <see cref="T:System.TypedReference" />.-or-The current type is a ByRef type. That is, <see cref="P:System.Type.IsByRef" /> returns true. -or-<paramref name="rank" /> is greater than 32.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600152C RID: 5420 RVA: 0x00014B5A File Offset: 0x00012D5A
		public virtual Type MakeArrayType(int rank)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the type associated with the specified program identifier (ProgID), returning null if an error is encountered while loading the <see cref="T:System.Type" />.</summary>
		/// <returns>The type associated with the specified ProgID, if <paramref name="progID" /> is a valid entry in the registry and a type is associated with it; otherwise, null.</returns>
		/// <param name="progID">The ProgID of the type to get. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="progID" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600152D RID: 5421 RVA: 0x000570A4 File Offset: 0x000552A4
		[SecurityCritical]
		public static Type GetTypeFromProgID(string progID)
		{
			return RuntimeType.GetTypeFromProgIDImpl(progID, null, false);
		}

		/// <summary>Gets the type associated with the specified program identifier (ProgID), specifying whether to throw an exception if an error occurs while loading the type.</summary>
		/// <returns>The type associated with the specified program identifier (ProgID), if <paramref name="progID" /> is a valid entry in the registry and a type is associated with it; otherwise, null.</returns>
		/// <param name="progID">The ProgID of the type to get. </param>
		/// <param name="throwOnError">true to throw any exception that occurs.-or- false to ignore any exception that occurs. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="progID" /> is null. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The specified ProgID is not registered. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600152E RID: 5422 RVA: 0x000570AE File Offset: 0x000552AE
		[SecurityCritical]
		public static Type GetTypeFromProgID(string progID, bool throwOnError)
		{
			return RuntimeType.GetTypeFromProgIDImpl(progID, null, throwOnError);
		}

		/// <summary>Gets the type associated with the specified program identifier (progID) from the specified server, returning null if an error is encountered while loading the type.</summary>
		/// <returns>The type associated with the specified program identifier (progID), if <paramref name="progID" /> is a valid entry in the registry and a type is associated with it; otherwise, null.</returns>
		/// <param name="progID">The progID of the type to get. </param>
		/// <param name="server">The server from which to load the type. If the server name is null, this method automatically reverts to the local machine. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="prodID" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600152F RID: 5423 RVA: 0x000570B8 File Offset: 0x000552B8
		[SecurityCritical]
		public static Type GetTypeFromProgID(string progID, string server)
		{
			return RuntimeType.GetTypeFromProgIDImpl(progID, server, false);
		}

		/// <summary>Gets the type associated with the specified program identifier (progID) from the specified server, specifying whether to throw an exception if an error occurs while loading the type.</summary>
		/// <returns>The type associated with the specified program identifier (progID), if <paramref name="progID" /> is a valid entry in the registry and a type is associated with it; otherwise, null.</returns>
		/// <param name="progID">The progID of the <see cref="T:System.Type" /> to get. </param>
		/// <param name="server">The server from which to load the type. If the server name is null, this method automatically reverts to the local machine. </param>
		/// <param name="throwOnError">true to throw any exception that occurs.-or- false to ignore any exception that occurs. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="progID" /> is null. </exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The specified progID is not registered. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001530 RID: 5424 RVA: 0x000570C2 File Offset: 0x000552C2
		[SecurityCritical]
		public static Type GetTypeFromProgID(string progID, string server, bool throwOnError)
		{
			return RuntimeType.GetTypeFromProgIDImpl(progID, server, throwOnError);
		}

		/// <summary>Gets the type associated with the specified class identifier (CLSID).</summary>
		/// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
		/// <param name="clsid">The CLSID of the type to get. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001531 RID: 5425 RVA: 0x000570CC File Offset: 0x000552CC
		[SecuritySafeCritical]
		public static Type GetTypeFromCLSID(Guid clsid)
		{
			return RuntimeType.GetTypeFromCLSIDImpl(clsid, null, false);
		}

		/// <summary>Gets the type associated with the specified class identifier (CLSID), specifying whether to throw an exception if an error occurs while loading the type.</summary>
		/// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
		/// <param name="clsid">The CLSID of the type to get. </param>
		/// <param name="throwOnError">true to throw any exception that occurs.-or- false to ignore any exception that occurs. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001532 RID: 5426 RVA: 0x000570D6 File Offset: 0x000552D6
		[SecuritySafeCritical]
		public static Type GetTypeFromCLSID(Guid clsid, bool throwOnError)
		{
			return RuntimeType.GetTypeFromCLSIDImpl(clsid, null, throwOnError);
		}

		/// <summary>Gets the type associated with the specified class identifier (CLSID) from the specified server.</summary>
		/// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
		/// <param name="clsid">The CLSID of the type to get. </param>
		/// <param name="server">The server from which to load the type. If the server name is null, this method automatically reverts to the local machine. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001533 RID: 5427 RVA: 0x000570E0 File Offset: 0x000552E0
		[SecuritySafeCritical]
		public static Type GetTypeFromCLSID(Guid clsid, string server)
		{
			return RuntimeType.GetTypeFromCLSIDImpl(clsid, server, false);
		}

		/// <summary>Gets the type associated with the specified class identifier (CLSID) from the specified server, specifying whether to throw an exception if an error occurs while loading the type.</summary>
		/// <returns>System.__ComObject regardless of whether the CLSID is valid.</returns>
		/// <param name="clsid">The CLSID of the type to get. </param>
		/// <param name="server">The server from which to load the type. If the server name is null, this method automatically reverts to the local machine. </param>
		/// <param name="throwOnError">true to throw any exception that occurs.-or- false to ignore any exception that occurs. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001534 RID: 5428 RVA: 0x000570EA File Offset: 0x000552EA
		[SecuritySafeCritical]
		public static Type GetTypeFromCLSID(Guid clsid, string server, bool throwOnError)
		{
			return RuntimeType.GetTypeFromCLSIDImpl(clsid, server, throwOnError);
		}

		/// <summary>Gets the underlying type code of the specified <see cref="T:System.Type" />.</summary>
		/// <returns>The code of the underlying type, or <see cref="F:System.TypeCode.Empty" /> if <paramref name="type" /> is null.</returns>
		/// <param name="type">The type whose underlying type code to get. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001535 RID: 5429 RVA: 0x000570F4 File Offset: 0x000552F4
		public static TypeCode GetTypeCode(Type type)
		{
			if (type == null)
			{
				return TypeCode.Empty;
			}
			return type.GetTypeCodeImpl();
		}

		/// <summary>Returns the underlying type code of the specified <see cref="T:System.Type" />.</summary>
		/// <returns>The code of the underlying type.</returns>
		// Token: 0x06001536 RID: 5430 RVA: 0x00057107 File Offset: 0x00055307
		protected virtual TypeCode GetTypeCodeImpl()
		{
			if (this != this.UnderlyingSystemType && this.UnderlyingSystemType != null)
			{
				return Type.GetTypeCode(this.UnderlyingSystemType);
			}
			return TypeCode.Object;
		}

		/// <summary>Gets the GUID associated with the <see cref="T:System.Type" />.</summary>
		/// <returns>The GUID associated with the <see cref="T:System.Type" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06001537 RID: 5431
		public abstract Guid GUID { get; }

		/// <summary>Gets a reference to the default binder, which implements internal rules for selecting the appropriate members to be called by <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" />.</summary>
		/// <returns>A reference to the default binder used by the system.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00057132 File Offset: 0x00055332
		public static Binder DefaultBinder
		{
			get
			{
				if (Type.defaultBinder == null)
				{
					Type.CreateBinder();
				}
				return Type.defaultBinder;
			}
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00057148 File Offset: 0x00055348
		private static void CreateBinder()
		{
			if (Type.defaultBinder == null)
			{
				DefaultBinder defaultBinder = new DefaultBinder();
				Interlocked.CompareExchange<Binder>(ref Type.defaultBinder, defaultBinder, null);
			}
		}

		/// <summary>When overridden in a derived class, invokes the specified member, using the specified binding constraints and matching the specified argument list, modifiers and culture.</summary>
		/// <returns>An object representing the return value of the invoked member.</returns>
		/// <param name="name">The string containing the name of the constructor, method, property, or field member to invoke.-or- An empty string ("") to invoke the default member. -or-For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
		/// <param name="invokeAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted. The access can be one of the BindingFlags such as Public, NonPublic, Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified. If the type of lookup is omitted, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static are used. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. Note that explicitly defining a <see cref="T:System.Reflection.Binder" /> object may be required for successfully invoking method overloads with variable arguments.</param>
		/// <param name="target">The object on which to invoke the specified member. </param>
		/// <param name="args">An array containing the arguments to pass to the member to invoke. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="args" /> array. A parameter's associated attributes are stored in the member's signature. The default binder processes this parameter only when calling a COM component. </param>
		/// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> object representing the globalization locale to use, which may be necessary for locale-specific conversions, such as converting a numeric String to a Double.-or- A null reference (Nothing in Visual Basic) to use the current thread's <see cref="T:System.Globalization.CultureInfo" />. </param>
		/// <param name="namedParameters">An array containing the names of the parameters to which the values in the <paramref name="args" /> array are passed. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="invokeAttr" /> does not contain CreateInstance and <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="args" /> and <paramref name="modifiers" /> do not have the same length.-or- <paramref name="invokeAttr" /> is not a valid <see cref="T:System.Reflection.BindingFlags" /> attribute.-or- <paramref name="invokeAttr" /> does not contain one of the following binding flags: InvokeMethod, CreateInstance, GetField, SetField, GetProperty, or SetProperty.-or- <paramref name="invokeAttr" /> contains CreateInstance combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty.-or- <paramref name="invokeAttr" /> contains both GetField and SetField.-or- <paramref name="invokeAttr" /> contains both GetProperty and SetProperty.-or- <paramref name="invokeAttr" /> contains InvokeMethod combined with SetField or SetProperty.-or- <paramref name="invokeAttr" /> contains SetField and <paramref name="args" /> has more than one element.-or- The named parameter array is larger than the argument array.-or- This method is called on a COM object and one of the following binding flags was not passed in: BindingFlags.InvokeMethod, BindingFlags.GetProperty, BindingFlags.SetProperty, BindingFlags.PutDispProperty, or BindingFlags.PutRefDispProperty.-or- One of the named parameter arrays contains a string that is null. </exception>
		/// <exception cref="T:System.MethodAccessException">The specified member is a class initializer. </exception>
		/// <exception cref="T:System.MissingFieldException">The field or property cannot be found. </exception>
		/// <exception cref="T:System.MissingMethodException">No method can be found that matches the arguments in <paramref name="args" />.-or- No member can be found that has the argument names supplied in <paramref name="namedParameters" />.-or- The current <see cref="T:System.Type" /> object represents a type that contains open type parameters, that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns true. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The specified member cannot be invoked on <paramref name="target" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method matches the binding criteria. </exception>
		/// <exception cref="T:System.InvalidOperationException">The method represented by <paramref name="name" /> has one or more unspecified generic type parameters. That is, the method's <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> property returns true.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600153A RID: 5434
		public abstract object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, ParameterModifier[] modifiers, CultureInfo culture, string[] namedParameters);

		/// <summary>Invokes the specified member, using the specified binding constraints and matching the specified argument list and culture.</summary>
		/// <returns>An object representing the return value of the invoked member.</returns>
		/// <param name="name">The string containing the name of the constructor, method, property, or field member to invoke.-or- An empty string ("") to invoke the default member. -or-For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
		/// <param name="invokeAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted. The access can be one of the BindingFlags such as Public, NonPublic, Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified. If the type of lookup is omitted, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static are used. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. Note that explicitly defining a <see cref="T:System.Reflection.Binder" /> object may be required for successfully invoking method overloads with variable arguments.</param>
		/// <param name="target">The object on which to invoke the specified member. </param>
		/// <param name="args">An array containing the arguments to pass to the member to invoke. </param>
		/// <param name="culture">The object representing the globalization locale to use, which may be necessary for locale-specific conversions, such as converting a numeric <see cref="T:System.String" /> to a <see cref="T:System.Double" />.-or- A null reference (Nothing in Visual Basic) to use the current thread's <see cref="T:System.Globalization.CultureInfo" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="invokeAttr" /> does not contain CreateInstance and <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="invokeAttr" /> is not a valid <see cref="T:System.Reflection.BindingFlags" /> attribute. -or- <paramref name="invokeAttr" /> does not contain one of the following binding flags: InvokeMethod, CreateInstance, GetField, SetField, GetProperty, or SetProperty.-or- <paramref name="invokeAttr" /> contains CreateInstance combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty.-or- <paramref name="invokeAttr" /> contains both GetField and SetField.-or- <paramref name="invokeAttr" /> contains both GetProperty and SetProperty.-or- <paramref name="invokeAttr" /> contains InvokeMethod combined with SetField or SetProperty.-or- <paramref name="invokeAttr" /> contains SetField and <paramref name="args" /> has more than one element.-or- This method is called on a COM object and one of the following binding flags was not passed in: BindingFlags.InvokeMethod, BindingFlags.GetProperty, BindingFlags.SetProperty, BindingFlags.PutDispProperty, or BindingFlags.PutRefDispProperty.-or- One of the named parameter arrays contains a string that is null. </exception>
		/// <exception cref="T:System.MethodAccessException">The specified member is a class initializer. </exception>
		/// <exception cref="T:System.MissingFieldException">The field or property cannot be found. </exception>
		/// <exception cref="T:System.MissingMethodException">No method can be found that matches the arguments in <paramref name="args" />.-or- The current <see cref="T:System.Type" /> object represents a type that contains open type parameters, that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns true. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The specified member cannot be invoked on <paramref name="target" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method matches the binding criteria. </exception>
		/// <exception cref="T:System.InvalidOperationException">The method represented by <paramref name="name" /> has one or more unspecified generic type parameters. That is, the method's <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> property returns true.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600153B RID: 5435 RVA: 0x00057170 File Offset: 0x00055370
		[DebuggerStepThrough]
		[DebuggerHidden]
		public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args, CultureInfo culture)
		{
			return this.InvokeMember(name, invokeAttr, binder, target, args, null, culture, null);
		}

		/// <summary>Invokes the specified member, using the specified binding constraints and matching the specified argument list.</summary>
		/// <returns>An object representing the return value of the invoked member.</returns>
		/// <param name="name">The string containing the name of the constructor, method, property, or field member to invoke.-or- An empty string ("") to invoke the default member. -or-For IDispatch members, a string representing the DispID, for example "[DispID=3]".</param>
		/// <param name="invokeAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted. The access can be one of the BindingFlags such as Public, NonPublic, Private, InvokeMethod, GetField, and so on. The type of lookup need not be specified. If the type of lookup is omitted, BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static are used. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. Note that explicitly defining a <see cref="T:System.Reflection.Binder" /> object may be required for successfully invoking method overloads with variable arguments.</param>
		/// <param name="target">The object on which to invoke the specified member. </param>
		/// <param name="args">An array containing the arguments to pass to the member to invoke. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="invokeAttr" /> does not contain CreateInstance and <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="invokeAttr" /> is not a valid <see cref="T:System.Reflection.BindingFlags" /> attribute. -or- <paramref name="invokeAttr" /> does not contain one of the following binding flags: InvokeMethod, CreateInstance, GetField, SetField, GetProperty, or SetProperty. -or- <paramref name="invokeAttr" /> contains CreateInstance combined with InvokeMethod, GetField, SetField, GetProperty, or SetProperty.-or- <paramref name="invokeAttr" /> contains both GetField and SetField.-or- <paramref name="invokeAttr" /> contains both GetProperty and SetProperty.-or- <paramref name="invokeAttr" /> contains InvokeMethod combined with SetField or SetProperty.-or- <paramref name="invokeAttr" /> contains SetField and <paramref name="args" /> has more than one element.-or- This method is called on a COM object and one of the following binding flags was not passed in: BindingFlags.InvokeMethod, BindingFlags.GetProperty, BindingFlags.SetProperty, BindingFlags.PutDispProperty, or BindingFlags.PutRefDispProperty.-or- One of the named parameter arrays contains a string that is null. </exception>
		/// <exception cref="T:System.MethodAccessException">The specified member is a class initializer. </exception>
		/// <exception cref="T:System.MissingFieldException">The field or property cannot be found. </exception>
		/// <exception cref="T:System.MissingMethodException">No method can be found that matches the arguments in <paramref name="args" />.-or- The current <see cref="T:System.Type" /> object represents a type that contains open type parameters, that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns true. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The specified member cannot be invoked on <paramref name="target" />. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method matches the binding criteria. </exception>
		/// <exception cref="T:System.NotSupportedException">The .NET Compact Framework does not currently support this method.</exception>
		/// <exception cref="T:System.InvalidOperationException">The method represented by <paramref name="name" /> has one or more unspecified generic type parameters. That is, the method's <see cref="P:System.Reflection.MethodInfo.ContainsGenericParameters" /> property returns true.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600153C RID: 5436 RVA: 0x00057190 File Offset: 0x00055390
		[DebuggerHidden]
		[DebuggerStepThrough]
		public object InvokeMember(string name, BindingFlags invokeAttr, Binder binder, object target, object[] args)
		{
			return this.InvokeMember(name, invokeAttr, binder, target, args, null, null, null);
		}

		/// <summary>Gets the module (the DLL) in which the current <see cref="T:System.Type" /> is defined.</summary>
		/// <returns>The module in which the current <see cref="T:System.Type" /> is defined.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600153D RID: 5437
		public abstract override Module Module { get; }

		/// <summary>Gets the <see cref="T:System.Reflection.Assembly" /> in which the type is declared. For generic types, gets the <see cref="T:System.Reflection.Assembly" /> in which the generic type is defined.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> instance that describes the assembly containing the current type. For generic types, the instance describes the assembly that contains the generic type definition, not the assembly that creates and uses a particular constructed type.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600153E RID: 5438
		public abstract Assembly Assembly { get; }

		/// <summary>Gets the handle for the current <see cref="T:System.Type" />.</summary>
		/// <returns>The handle for the current <see cref="T:System.Type" />.</returns>
		/// <exception cref="T:System.NotSupportedException">The .NET Compact Framework does not currently support this property.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x00014B5A File Offset: 0x00012D5A
		public virtual RuntimeTypeHandle TypeHandle
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x000571AD File Offset: 0x000553AD
		internal virtual RuntimeTypeHandle GetTypeHandleInternal()
		{
			return this.TypeHandle;
		}

		/// <summary>Gets the handle for the <see cref="T:System.Type" /> of a specified object.</summary>
		/// <returns>The handle for the <see cref="T:System.Type" /> of the specified <see cref="T:System.Object" />.</returns>
		/// <param name="o">The object for which to get the type handle. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="o" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001541 RID: 5441 RVA: 0x000571B5 File Offset: 0x000553B5
		public static RuntimeTypeHandle GetTypeHandle(object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException(null, Environment.GetResourceString("Invalid handle."));
			}
			return new RuntimeTypeHandle((RuntimeType)o.GetType());
		}

		/// <summary>Gets the fully qualified name of the <see cref="T:System.Type" />, including the namespace of the <see cref="T:System.Type" /> but not the assembly.</summary>
		/// <returns>The fully qualified name of the <see cref="T:System.Type" />, including the namespace of the <see cref="T:System.Type" /> but not the assembly; or null if the current instance represents a generic type parameter, an array type, pointer type, or byref type based on a type parameter, or a generic type that is not a generic type definition but contains unresolved type parameters.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001542 RID: 5442
		public abstract string FullName { get; }

		/// <summary>Gets the namespace of the <see cref="T:System.Type" />.</summary>
		/// <returns>The namespace of the <see cref="T:System.Type" />; null if the current instance has no namespace or represents a generic parameter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001543 RID: 5443
		public abstract string Namespace { get; }

		/// <summary>Gets the assembly-qualified name of the <see cref="T:System.Type" />, which includes the name of the assembly from which the <see cref="T:System.Type" /> was loaded.</summary>
		/// <returns>The assembly-qualified name of the <see cref="T:System.Type" />, which includes the name of the assembly from which the <see cref="T:System.Type" /> was loaded, or null if the current instance represents a generic type parameter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001544 RID: 5444
		public abstract string AssemblyQualifiedName { get; }

		/// <summary>Gets the number of dimensions in an <see cref="T:System.Array" />.</summary>
		/// <returns>An <see cref="T:System.Int32" /> containing the number of dimensions in the current Type.</returns>
		/// <exception cref="T:System.NotSupportedException">The functionality of this method is unsupported in the base class and must be implemented in a derived class instead. </exception>
		/// <exception cref="T:System.ArgumentException">The current Type is not an array. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001545 RID: 5445 RVA: 0x000571DB File Offset: 0x000553DB
		public virtual int GetArrayRank()
		{
			throw new NotSupportedException(Environment.GetResourceString("Derived classes must provide an implementation."));
		}

		/// <summary>Gets the type from which the current <see cref="T:System.Type" /> directly inherits.</summary>
		/// <returns>The <see cref="T:System.Type" /> from which the current <see cref="T:System.Type" /> directly inherits, or null if the current Type represents the <see cref="T:System.Object" /> class or an interface.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001546 RID: 5446
		public abstract Type BaseType { get; }

		/// <summary>Searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <returns>An object representing the constructor that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and the stack is cleaned up. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the constructor to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a constructor that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001547 RID: 5447 RVA: 0x000571EC File Offset: 0x000553EC
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetConstructorImpl(bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object representing the constructor that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the constructor to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a constructor that takes no parameters.-or- <see cref="F:System.Type.EmptyTypes" />. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the parameter type array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001548 RID: 5448 RVA: 0x0005723C File Offset: 0x0005543C
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetConstructorImpl(bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		/// <summary>Searches for a public instance constructor whose parameters match the types in the specified array.</summary>
		/// <returns>An object representing the public instance constructor whose parameters match the types in the parameter type array, if found; otherwise, null.</returns>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the desired constructor.-or- An empty array of <see cref="T:System.Type" /> objects, to get a constructor that takes no parameters. Such an empty array is provided by the static field <see cref="F:System.Type.EmptyTypes" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001549 RID: 5449 RVA: 0x00057287 File Offset: 0x00055487
		[ComVisible(true)]
		public ConstructorInfo GetConstructor(Type[] types)
		{
			return this.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, types, null);
		}

		/// <summary>When overridden in a derived class, searches for a constructor whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> object representing the constructor that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and the stack is cleaned up. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the constructor to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a constructor that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <exception cref="T:System.NotSupportedException">The current type is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> or <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</exception>
		// Token: 0x0600154A RID: 5450
		protected abstract ConstructorInfo GetConstructorImpl(BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		/// <summary>Returns all the public constructors defined for the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing all the public instance constructors defined for the current <see cref="T:System.Type" />, but not including the type initializer (static constructor). If no public instance constructors are defined for the current <see cref="T:System.Type" />, or if the current <see cref="T:System.Type" /> represents a type parameter in the definition of a generic type or generic method, an empty array of type <see cref="T:System.Reflection.ConstructorInfo" /> is returned.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600154B RID: 5451 RVA: 0x00057294 File Offset: 0x00055494
		[ComVisible(true)]
		public ConstructorInfo[] GetConstructors()
		{
			return this.GetConstructors(BindingFlags.Instance | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the constructors defined for the current <see cref="T:System.Type" />, using the specified BindingFlags.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.ConstructorInfo" /> objects representing all constructors defined for the current <see cref="T:System.Type" /> that match the specified binding constraints, including the type initializer if it is defined. Returns an empty array of type <see cref="T:System.Reflection.ConstructorInfo" /> if no constructors are defined for the current <see cref="T:System.Type" />, if none of the defined constructors match the binding constraints, or if the current <see cref="T:System.Type" /> represents a type parameter in the definition of a generic type or generic method.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600154C RID: 5452
		[ComVisible(true)]
		public abstract ConstructorInfo[] GetConstructors(BindingFlags bindingAttr);

		/// <summary>Gets the initializer for the <see cref="T:System.Type" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.ConstructorInfo" /> containing the name of the class constructor for the <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x0005729E File Offset: 0x0005549E
		[ComVisible(true)]
		public ConstructorInfo TypeInitializer
		{
			get
			{
				return this.GetConstructorImpl(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, CallingConventions.Any, Type.EmptyTypes, null);
			}
		}

		/// <summary>Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <returns>An object representing the method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the method to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and how the stack is cleaned up. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600154E RID: 5454 RVA: 0x000572B0 File Offset: 0x000554B0
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <returns>An object representing the method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the method to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.</param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600154F RID: 5455 RVA: 0x00057310 File Offset: 0x00055510
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, bindingAttr, binder, CallingConventions.Any, types, modifiers);
		}

		/// <summary>Searches for the specified public method whose parameters match the specified argument types and modifiers.</summary>
		/// <returns>An object representing the public method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public method to get. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. To be only used when calling through COM interop, and only parameters that are passed by reference are handled. The default binder does not process this parameter.  </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and specified parameters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001550 RID: 5456 RVA: 0x00057370 File Offset: 0x00055570
		public MethodInfo GetMethod(string name, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, types, modifiers);
		}

		/// <summary>Searches for the specified public method whose parameters match the specified argument types.</summary>
		/// <returns>An object representing the public method whose parameters match the specified argument types, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public method to get. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of <see cref="T:System.Type" /> objects (as provided by the <see cref="F:System.Type.EmptyTypes" /> field) to get a method that takes no parameters. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and specified parameters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001551 RID: 5457 RVA: 0x000573CC File Offset: 0x000555CC
		public MethodInfo GetMethod(string name, Type[] types)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentNullException("types");
				}
			}
			return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, types, null);
		}

		/// <summary>Searches for the specified method, using the specified binding constraints.</summary>
		/// <returns>An object representing the method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the method to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001552 RID: 5458 RVA: 0x00057426 File Offset: 0x00055626
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetMethodImpl(name, bindingAttr, null, CallingConventions.Any, null, null);
		}

		/// <summary>Searches for the public method with the specified name.</summary>
		/// <returns>An object that represents the public method with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public method to get. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001553 RID: 5459 RVA: 0x00057442 File Offset: 0x00055642
		public MethodInfo GetMethod(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, null, null);
		}

		/// <summary>When overridden in a derived class, searches for the specified method whose parameters match the specified argument types and modifiers, using the specified binding constraints and the specified calling convention.</summary>
		/// <returns>An object representing the method that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the method to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="callConvention">The object that specifies the set of rules to use regarding the order and layout of arguments, how the return value is passed, what registers are used for arguments, and what process cleans up the stack. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the method to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a method that takes no parameters.-or- null. If <paramref name="types" /> is null, arguments are not matched. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one method is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <exception cref="T:System.NotSupportedException">The current type is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> or <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</exception>
		// Token: 0x06001554 RID: 5460
		protected abstract MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers);

		/// <summary>Returns all the public methods of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects representing all the public methods defined for the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.MethodInfo" />, if no public methods are defined for the current <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001555 RID: 5461 RVA: 0x0005745F File Offset: 0x0005565F
		public MethodInfo[] GetMethods()
		{
			return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the methods defined for the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects representing all methods defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.MethodInfo" />, if no methods are defined for the current <see cref="T:System.Type" />, or if none of the defined methods match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001556 RID: 5462
		public abstract MethodInfo[] GetMethods(BindingFlags bindingAttr);

		/// <summary>Searches for the specified field, using the specified binding constraints.</summary>
		/// <returns>An object representing the field that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the data field to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001557 RID: 5463
		public abstract FieldInfo GetField(string name, BindingFlags bindingAttr);

		/// <summary>Searches for the public field with the specified name.</summary>
		/// <returns>An object representing the public field with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the data field to get. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">This <see cref="T:System.Type" /> object is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> whose <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method has not yet been called. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001558 RID: 5464 RVA: 0x00057469 File Offset: 0x00055669
		public FieldInfo GetField(string name)
		{
			return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Returns all the public fields of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects representing all the public fields defined for the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.FieldInfo" />, if no public fields are defined for the current <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001559 RID: 5465 RVA: 0x00057474 File Offset: 0x00055674
		public FieldInfo[] GetFields()
		{
			return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the fields defined for the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects representing all fields defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.FieldInfo" />, if no fields are defined for the current <see cref="T:System.Type" />, or if none of the defined fields match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600155A RID: 5466
		public abstract FieldInfo[] GetFields(BindingFlags bindingAttr);

		/// <summary>Searches for the interface with the specified name.</summary>
		/// <returns>An object representing the interface with the specified name, implemented or inherited by the current <see cref="T:System.Type" />, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the interface to get. For generic interfaces, this is the mangled name.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The current <see cref="T:System.Type" /> represents a type that implements the same generic interface with different type arguments. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600155B RID: 5467 RVA: 0x0005747E File Offset: 0x0005567E
		public Type GetInterface(string name)
		{
			return this.GetInterface(name, false);
		}

		/// <summary>When overridden in a derived class, searches for the specified interface, specifying whether to do a case-insensitive search for the interface name.</summary>
		/// <returns>An object representing the interface with the specified name, implemented or inherited by the current <see cref="T:System.Type" />, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the interface to get. For generic interfaces, this is the mangled name.</param>
		/// <param name="ignoreCase">true to ignore the case of that part of <paramref name="name" /> that specifies the simple interface name (the part that specifies the namespace must be correctly cased).-or- false to perform a case-sensitive search for all parts of <paramref name="name" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">The current <see cref="T:System.Type" /> represents a type that implements the same generic interface with different type arguments. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600155C RID: 5468
		public abstract Type GetInterface(string name, bool ignoreCase);

		/// <summary>When overridden in a derived class, gets all the interfaces implemented or inherited by the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the interfaces implemented or inherited by the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Type" />, if no interfaces are implemented or inherited by the current <see cref="T:System.Type" />.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A static initializer is invoked and throws an exception. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600155D RID: 5469
		public abstract Type[] GetInterfaces();

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects representing a filtered list of interfaces implemented or inherited by the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing a filtered list of the interfaces implemented or inherited by the current <see cref="T:System.Type" />, or an empty array of type <see cref="T:System.Type" /> if no interfaces matching the filter are implemented or inherited by the current <see cref="T:System.Type" />.</returns>
		/// <param name="filter">The delegate that compares the interfaces against <paramref name="filterCriteria" />. </param>
		/// <param name="filterCriteria">The search criteria that determines whether an interface should be included in the returned array. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filter" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A static initializer is invoked and throws an exception. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600155E RID: 5470 RVA: 0x00057488 File Offset: 0x00055688
		public virtual Type[] FindInterfaces(TypeFilter filter, object filterCriteria)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}
			Type[] interfaces = this.GetInterfaces();
			int num = 0;
			for (int i = 0; i < interfaces.Length; i++)
			{
				if (!filter(interfaces[i], filterCriteria))
				{
					interfaces[i] = null;
				}
				else
				{
					num++;
				}
			}
			if (num == interfaces.Length)
			{
				return interfaces;
			}
			Type[] array = new Type[num];
			num = 0;
			for (int j = 0; j < interfaces.Length; j++)
			{
				if (interfaces[j] != null)
				{
					array[num++] = interfaces[j];
				}
			}
			return array;
		}

		/// <summary>Returns the <see cref="T:System.Reflection.EventInfo" /> object representing the specified public event.</summary>
		/// <returns>The object representing the specified public event that is declared or inherited by the current <see cref="T:System.Type" />, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of an event that is declared or inherited by the current <see cref="T:System.Type" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600155F RID: 5471 RVA: 0x0005750B File Offset: 0x0005570B
		public EventInfo GetEvent(string name)
		{
			return this.GetEvent(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Reflection.EventInfo" /> object representing the specified event, using the specified binding constraints.</summary>
		/// <returns>The object representing the specified event that is declared or inherited by the current <see cref="T:System.Type" />, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of an event which is declared or inherited by the current <see cref="T:System.Type" />. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001560 RID: 5472
		public abstract EventInfo GetEvent(string name, BindingFlags bindingAttr);

		/// <summary>Returns all the public events that are declared or inherited by the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing all the public events which are declared or inherited by the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.EventInfo" />, if the current <see cref="T:System.Type" /> does not have public events.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001561 RID: 5473 RVA: 0x00057516 File Offset: 0x00055716
		public virtual EventInfo[] GetEvents()
		{
			return this.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for events that are declared or inherited by the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.EventInfo" /> objects representing all events that are declared or inherited by the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.EventInfo" />, if the current <see cref="T:System.Type" /> does not have events, or if none of the events match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001562 RID: 5474
		public abstract EventInfo[] GetEvents(BindingFlags bindingAttr);

		/// <summary>Searches for the specified property whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <returns>An object representing the property that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the property to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded method, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001563 RID: 5475 RVA: 0x00057520 File Offset: 0x00055720
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			return this.GetPropertyImpl(name, bindingAttr, binder, returnType, types, modifiers);
		}

		/// <summary>Searches for the specified public property whose parameters match the specified argument types and modifiers.</summary>
		/// <returns>An object representing the public property that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public property to get. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified argument types and modifiers. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001564 RID: 5476 RVA: 0x0005754E File Offset: 0x0005574E
		public PropertyInfo GetProperty(string name, Type returnType, Type[] types, ParameterModifier[] modifiers)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, types, modifiers);
		}

		/// <summary>Searches for the specified property, using the specified binding constraints.</summary>
		/// <returns>An object representing the property that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the property to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints. See Remarks.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001565 RID: 5477 RVA: 0x0005757A File Offset: 0x0005577A
		public PropertyInfo GetProperty(string name, BindingFlags bindingAttr)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetPropertyImpl(name, bindingAttr, null, null, null, null);
		}

		/// <summary>Searches for the specified public property whose parameters match the specified argument types.</summary>
		/// <returns>An object representing the public property whose parameters match the specified argument types, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public property to get. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified argument types. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional. </exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001566 RID: 5478 RVA: 0x00057596 File Offset: 0x00055796
		public PropertyInfo GetProperty(string name, Type returnType, Type[] types)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, types, null);
		}

		/// <summary>Searches for the specified public property whose parameters match the specified argument types.</summary>
		/// <returns>An object representing the public property whose parameters match the specified argument types, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public property to get. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified argument types. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional. </exception>
		/// <exception cref="T:System.NullReferenceException">An element of <paramref name="types" /> is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001567 RID: 5479 RVA: 0x000575C1 File Offset: 0x000557C1
		public PropertyInfo GetProperty(string name, Type[] types)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, types, null);
		}

		/// <summary>Searches for the public property with the specified name and return type.</summary>
		/// <returns>An object representing the public property with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public property to get. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null, or <paramref name="returnType" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001568 RID: 5480 RVA: 0x000575EC File Offset: 0x000557EC
		public PropertyInfo GetProperty(string name, Type returnType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, returnType, null, null);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0005761D File Offset: 0x0005581D
		internal PropertyInfo GetProperty(string name, BindingFlags bindingAttr, Type returnType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (returnType == null)
			{
				throw new ArgumentNullException("returnType");
			}
			return this.GetPropertyImpl(name, bindingAttr, null, returnType, null, null);
		}

		/// <summary>Searches for the public property with the specified name.</summary>
		/// <returns>An object representing the public property with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the public property to get. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name. See Remarks.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600156A RID: 5482 RVA: 0x0005764D File Offset: 0x0005584D
		public PropertyInfo GetProperty(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.GetPropertyImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, null, null, null);
		}

		/// <summary>When overridden in a derived class, searches for the specified property whose parameters match the specified argument types and modifiers, using the specified binding constraints.</summary>
		/// <returns>An object representing the property that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the property to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="binder">An object that defines a set of properties and enables binding, which can involve selection of an overloaded member, coercion of argument types, and invocation of a member through reflection.-or- A null reference (Nothing in Visual Basic), to use the <see cref="P:System.Type.DefaultBinder" />. </param>
		/// <param name="returnType">The return type of the property. </param>
		/// <param name="types">An array of <see cref="T:System.Type" /> objects representing the number, order, and type of the parameters for the indexed property to get.-or- An empty array of the type <see cref="T:System.Type" /> (that is, Type[] types = new Type[0]) to get a property that is not indexed. </param>
		/// <param name="modifiers">An array of <see cref="T:System.Reflection.ParameterModifier" /> objects representing the attributes associated with the corresponding element in the <paramref name="types" /> array. The default binder does not process this parameter. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">More than one property is found with the specified name and matching the specified binding constraints. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null.-or- <paramref name="types" /> is null.-or- One of the elements in <paramref name="types" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="types" /> is multidimensional.-or- <paramref name="modifiers" /> is multidimensional.-or- <paramref name="types" /> and <paramref name="modifiers" /> do not have the same length. </exception>
		/// <exception cref="T:System.NotSupportedException">The current type is a <see cref="T:System.Reflection.Emit.TypeBuilder" />, <see cref="T:System.Reflection.Emit.EnumBuilder" />, or <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" />.</exception>
		// Token: 0x0600156B RID: 5483
		protected abstract PropertyInfo GetPropertyImpl(string name, BindingFlags bindingAttr, Binder binder, Type returnType, Type[] types, ParameterModifier[] modifiers);

		/// <summary>When overridden in a derived class, searches for the properties of the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing all properties of the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.PropertyInfo" />, if the current <see cref="T:System.Type" /> does not have properties, or if none of the properties match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600156C RID: 5484
		public abstract PropertyInfo[] GetProperties(BindingFlags bindingAttr);

		/// <summary>Returns all the public properties of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.PropertyInfo" /> objects representing all public properties of the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.PropertyInfo" />, if the current <see cref="T:System.Type" /> does not have public properties.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600156D RID: 5485 RVA: 0x0005766A File Offset: 0x0005586A
		public PropertyInfo[] GetProperties()
		{
			return this.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Returns the public types nested in the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing the public types nested in the current <see cref="T:System.Type" /> (the search is not recursive), or an empty array of type <see cref="T:System.Type" /> if no public types are nested in the current <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600156E RID: 5486 RVA: 0x00057674 File Offset: 0x00055874
		public Type[] GetNestedTypes()
		{
			return this.GetNestedTypes(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the types nested in the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing all the types nested in the current <see cref="T:System.Type" /> that match the specified binding constraints (the search is not recursive), or an empty array of type <see cref="T:System.Type" />, if no nested types are found that match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600156F RID: 5487
		public abstract Type[] GetNestedTypes(BindingFlags bindingAttr);

		/// <summary>Searches for the public nested type with the specified name.</summary>
		/// <returns>An object representing the public nested type with the specified name, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the nested type to get. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001570 RID: 5488 RVA: 0x0005767E File Offset: 0x0005587E
		public Type GetNestedType(string name)
		{
			return this.GetNestedType(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the specified nested type, using the specified binding constraints.</summary>
		/// <returns>An object representing the nested type that matches the specified requirements, if found; otherwise, null.</returns>
		/// <param name="name">The string containing the name of the nested type to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001571 RID: 5489
		public abstract Type GetNestedType(string name, BindingFlags bindingAttr);

		/// <summary>Searches for the public members with the specified name.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		/// <param name="name">The string containing the name of the public members to get. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001572 RID: 5490 RVA: 0x00057689 File Offset: 0x00055889
		public MemberInfo[] GetMember(string name)
		{
			return this.GetMember(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Searches for the specified members, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		/// <param name="name">The string containing the name of the members to get. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return an empty array. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001573 RID: 5491 RVA: 0x00057694 File Offset: 0x00055894
		public virtual MemberInfo[] GetMember(string name, BindingFlags bindingAttr)
		{
			return this.GetMember(name, MemberTypes.All, bindingAttr);
		}

		/// <summary>Searches for the specified members of the specified member type, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing the public members with the specified name, if found; otherwise, an empty array.</returns>
		/// <param name="name">The string containing the name of the members to get. </param>
		/// <param name="type">The value to search for. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return an empty array. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">A derived class must provide an implementation. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001574 RID: 5492 RVA: 0x000571DB File Offset: 0x000553DB
		public virtual MemberInfo[] GetMember(string name, MemberTypes type, BindingFlags bindingAttr)
		{
			throw new NotSupportedException(Environment.GetResourceString("Derived classes must provide an implementation."));
		}

		/// <summary>Returns all the public members of the current <see cref="T:System.Type" />.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing all the public members of the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if the current <see cref="T:System.Type" /> does not have public members.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001575 RID: 5493 RVA: 0x000576A3 File Offset: 0x000558A3
		public MemberInfo[] GetMembers()
		{
			return this.GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>When overridden in a derived class, searches for the members defined for the current <see cref="T:System.Type" />, using the specified binding constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing all members defined for the current <see cref="T:System.Type" /> that match the specified binding constraints.-or- An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if no members are defined for the current <see cref="T:System.Type" />, or if none of the defined members match the binding constraints.</returns>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero (<see cref="F:System.Reflection.BindingFlags.Default" />), to return an empty array. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001576 RID: 5494
		public abstract MemberInfo[] GetMembers(BindingFlags bindingAttr);

		/// <summary>Searches for the members defined for the current <see cref="T:System.Type" /> whose <see cref="T:System.Reflection.DefaultMemberAttribute" /> is set.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MemberInfo" /> objects representing all default members of the current <see cref="T:System.Type" />.-or- An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if the current <see cref="T:System.Type" /> does not have default members.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001577 RID: 5495 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual MemberInfo[] GetDefaultMembers()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a filtered array of <see cref="T:System.Reflection.MemberInfo" /> objects of the specified member type.</summary>
		/// <returns>A filtered array of <see cref="T:System.Reflection.MemberInfo" /> objects of the specified member type.-or- An empty array of type <see cref="T:System.Reflection.MemberInfo" />, if the current <see cref="T:System.Type" /> does not have members of type <paramref name="memberType" /> that match the filter criteria.</returns>
		/// <param name="memberType">An object that indicates the type of member to search for. </param>
		/// <param name="bindingAttr">A bitmask comprised of one or more <see cref="T:System.Reflection.BindingFlags" /> that specify how the search is conducted.-or- Zero, to return null. </param>
		/// <param name="filter">The delegate that does the comparisons, returning true if the member currently being inspected matches the <paramref name="filterCriteria" /> and false otherwise. You can use the FilterAttribute, FilterName, and FilterNameIgnoreCase delegates supplied by this class. The first uses the fields of FieldAttributes, MethodAttributes, and MethodImplAttributes as search criteria, and the other two delegates use String objects as the search criteria. </param>
		/// <param name="filterCriteria">The search criteria that determines whether a member is returned in the array of MemberInfo objects.The fields of FieldAttributes, MethodAttributes, and MethodImplAttributes can be used in conjunction with the FilterAttribute delegate supplied by this class. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filter" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001578 RID: 5496 RVA: 0x000576B0 File Offset: 0x000558B0
		public virtual MemberInfo[] FindMembers(MemberTypes memberType, BindingFlags bindingAttr, MemberFilter filter, object filterCriteria)
		{
			MethodInfo[] array = null;
			ConstructorInfo[] array2 = null;
			FieldInfo[] array3 = null;
			PropertyInfo[] array4 = null;
			EventInfo[] array5 = null;
			Type[] array6 = null;
			int num = 0;
			if ((memberType & MemberTypes.Method) != (MemberTypes)0)
			{
				array = this.GetMethods(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array.Length; i++)
					{
						if (!filter(array[i], filterCriteria))
						{
							array[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array.Length;
				}
			}
			if ((memberType & MemberTypes.Constructor) != (MemberTypes)0)
			{
				array2 = this.GetConstructors(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array2.Length; i++)
					{
						if (!filter(array2[i], filterCriteria))
						{
							array2[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array2.Length;
				}
			}
			if ((memberType & MemberTypes.Field) != (MemberTypes)0)
			{
				array3 = this.GetFields(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array3.Length; i++)
					{
						if (!filter(array3[i], filterCriteria))
						{
							array3[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array3.Length;
				}
			}
			if ((memberType & MemberTypes.Property) != (MemberTypes)0)
			{
				array4 = this.GetProperties(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array4.Length; i++)
					{
						if (!filter(array4[i], filterCriteria))
						{
							array4[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array4.Length;
				}
			}
			if ((memberType & MemberTypes.Event) != (MemberTypes)0)
			{
				array5 = this.GetEvents(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array5.Length; i++)
					{
						if (!filter(array5[i], filterCriteria))
						{
							array5[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array5.Length;
				}
			}
			if ((memberType & MemberTypes.NestedType) != (MemberTypes)0)
			{
				array6 = this.GetNestedTypes(bindingAttr);
				if (filter != null)
				{
					for (int i = 0; i < array6.Length; i++)
					{
						if (!filter(array6[i], filterCriteria))
						{
							array6[i] = null;
						}
						else
						{
							num++;
						}
					}
				}
				else
				{
					num += array6.Length;
				}
			}
			MemberInfo[] array7 = new MemberInfo[num];
			num = 0;
			if (array != null)
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != null)
					{
						array7[num++] = array[i];
					}
				}
			}
			if (array2 != null)
			{
				for (int i = 0; i < array2.Length; i++)
				{
					if (array2[i] != null)
					{
						array7[num++] = array2[i];
					}
				}
			}
			if (array3 != null)
			{
				for (int i = 0; i < array3.Length; i++)
				{
					if (array3[i] != null)
					{
						array7[num++] = array3[i];
					}
				}
			}
			if (array4 != null)
			{
				for (int i = 0; i < array4.Length; i++)
				{
					if (array4[i] != null)
					{
						array7[num++] = array4[i];
					}
				}
			}
			if (array5 != null)
			{
				for (int i = 0; i < array5.Length; i++)
				{
					if (array5[i] != null)
					{
						array7[num++] = array5[i];
					}
				}
			}
			if (array6 != null)
			{
				for (int i = 0; i < array6.Length; i++)
				{
					if (array6[i] != null)
					{
						array7[num++] = array6[i];
					}
				}
			}
			return array7;
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> object represents a type whose definition is nested inside the definition of another type.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested inside another type; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x000579BA File Offset: 0x00055BBA
		public bool IsNested
		{
			get
			{
				return this.DeclaringType != null;
			}
		}

		/// <summary>Gets the attributes associated with the <see cref="T:System.Type" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object representing the attribute set of the <see cref="T:System.Type" />, unless the <see cref="T:System.Type" /> represents a generic type parameter, in which case the value is unspecified. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600157A RID: 5498 RVA: 0x000579C8 File Offset: 0x00055BC8
		public TypeAttributes Attributes
		{
			get
			{
				return this.GetAttributeFlagsImpl();
			}
		}

		/// <summary>Gets a combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> flags that describe the covariance and special constraints of the current generic type parameter. </summary>
		/// <returns>A bitwise combination of <see cref="T:System.Reflection.GenericParameterAttributes" /> values that describes the covariance and special constraints of the current generic type parameter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Type" /> object is not a generic type parameter. That is, the <see cref="P:System.Type.IsGenericParameter" /> property returns false.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600157B RID: 5499 RVA: 0x00014B5A File Offset: 0x00012D5A
		public virtual GenericParameterAttributes GenericParameterAttributes
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> can be accessed by code outside the assembly.</summary>
		/// <returns>true if the current <see cref="T:System.Type" /> is a public type or a public nested type such that all the enclosing types are public; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600157C RID: 5500 RVA: 0x000579D0 File Offset: 0x00055BD0
		public bool IsVisible
		{
			get
			{
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (this.HasElementType)
				{
					return this.GetElementType().IsVisible;
				}
				Type type = this;
				while (type.IsNested)
				{
					if (!type.IsNestedPublic)
					{
						return false;
					}
					type = type.DeclaringType;
				}
				if (!type.IsPublic)
				{
					return false;
				}
				if (this.IsGenericType && !this.IsGenericTypeDefinition)
				{
					Type[] genericArguments = this.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						if (!genericArguments[i].IsVisible)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is not declared public.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is not declared public and is not a nested type; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600157D RID: 5501 RVA: 0x00057A54 File Offset: 0x00055C54
		public bool IsNotPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is declared public.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is declared public and is not a nested type; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x0600157E RID: 5502 RVA: 0x00057A61 File Offset: 0x00055C61
		public bool IsPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.Public;
			}
		}

		/// <summary>Gets a value indicating whether a class is nested and declared public.</summary>
		/// <returns>true if the class is nested and declared public; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x00057A6E File Offset: 0x00055C6E
		public bool IsNestedPublic
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and declared private.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested and declared private; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x00057A7B File Offset: 0x00055C7B
		public bool IsNestedPrivate
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedPrivate;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and visible only within its own family.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested and visible only within its own family; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x00057A88 File Offset: 0x00055C88
		public bool IsNestedFamily
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedFamily;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and visible only within its own assembly.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested and visible only within its own assembly; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x00057A95 File Offset: 0x00055C95
		public bool IsNestedAssembly
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedAssembly;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and visible only to classes that belong to both its own family and its own assembly.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested and visible only to classes that belong to both its own family and its own assembly; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001583 RID: 5507 RVA: 0x00057AA2 File Offset: 0x00055CA2
		public bool IsNestedFamANDAssem
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.NestedFamANDAssem;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is nested and visible only to classes that belong to either its own family or to its own assembly.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is nested and visible only to classes that belong to its own family or to its own assembly; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x00057AAF File Offset: 0x00055CAF
		public bool IsNestedFamORAssem
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.VisibilityMask) == TypeAttributes.VisibilityMask;
			}
		}

		/// <summary>Gets a value indicating whether the fields of the current type are laid out automatically by the common language runtime.</summary>
		/// <returns>true if the <see cref="P:System.Type.Attributes" /> property of the current type includes <see cref="F:System.Reflection.TypeAttributes.AutoLayout" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001585 RID: 5509 RVA: 0x00057ABC File Offset: 0x00055CBC
		public bool IsAutoLayout
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the fields of the current type are laid out sequentially, in the order that they were defined or emitted to the metadata.</summary>
		/// <returns>true if the <see cref="P:System.Type.Attributes" /> property of the current type includes <see cref="F:System.Reflection.TypeAttributes.SequentialLayout" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001586 RID: 5510 RVA: 0x00057ACA File Offset: 0x00055CCA
		public bool IsLayoutSequential
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.SequentialLayout;
			}
		}

		/// <summary>Gets a value indicating whether the fields of the current type are laid out at explicitly specified offsets.</summary>
		/// <returns>true if the <see cref="P:System.Type.Attributes" /> property of the current type includes <see cref="F:System.Reflection.TypeAttributes.ExplicitLayout" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001587 RID: 5511 RVA: 0x00057AD8 File Offset: 0x00055CD8
		public bool IsExplicitLayout
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.LayoutMask) == TypeAttributes.ExplicitLayout;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a class; that is, not a value type or interface.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a class; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06001588 RID: 5512 RVA: 0x00057AE7 File Offset: 0x00055CE7
		public bool IsClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.ClassSemanticsMask) == TypeAttributes.NotPublic && !this.IsValueType;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is an interface; that is, not a class or a value type.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an interface; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06001589 RID: 5513 RVA: 0x00057B00 File Offset: 0x00055D00
		public bool IsInterface
		{
			[SecuritySafeCritical]
			get
			{
				RuntimeType runtimeType = this as RuntimeType;
				if (runtimeType != null)
				{
					return RuntimeTypeHandle.IsInterface(runtimeType);
				}
				return (this.GetAttributeFlagsImpl() & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a value type.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a value type; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600158A RID: 5514 RVA: 0x00057B31 File Offset: 0x00055D31
		public bool IsValueType
		{
			get
			{
				return this.IsValueTypeImpl();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is abstract and must be overridden.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is abstract; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600158B RID: 5515 RVA: 0x00057B39 File Offset: 0x00055D39
		public bool IsAbstract
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Abstract) > TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is declared sealed.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is declared sealed; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x0600158C RID: 5516 RVA: 0x00057B4A File Offset: 0x00055D4A
		public bool IsSealed
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Sealed) > TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> represents an enumeration.</summary>
		/// <returns>true if the current <see cref="T:System.Type" /> represents an enumeration; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x0600158D RID: 5517 RVA: 0x00057B5B File Offset: 0x00055D5B
		public virtual bool IsEnum
		{
			get
			{
				return this.IsSubclassOf(RuntimeType.EnumType);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> has a name that requires special handling.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> has a name that requires special handling; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x0600158E RID: 5518 RVA: 0x00057B68 File Offset: 0x00055D68
		public bool IsSpecialName
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.SpecialName) > TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> has a <see cref="T:System.Runtime.InteropServices.ComImportAttribute" /> attribute applied, indicating that it was imported from a COM type library.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> has a <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x00057B79 File Offset: 0x00055D79
		public bool IsImport
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.Import) > TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is serializable.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is serializable; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x00057B8C File Offset: 0x00055D8C
		public virtual bool IsSerializable
		{
			get
			{
				if ((this.GetAttributeFlagsImpl() & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
				{
					return true;
				}
				RuntimeType runtimeType = this.UnderlyingSystemType as RuntimeType;
				return runtimeType != null && runtimeType.IsSpecialSerializableType();
			}
		}

		/// <summary>Gets a value indicating whether the string format attribute AnsiClass is selected for the <see cref="T:System.Type" />.</summary>
		/// <returns>true if the string format attribute AnsiClass is selected for the <see cref="T:System.Type" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x00057BC6 File Offset: 0x00055DC6
		public bool IsAnsiClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.NotPublic;
			}
		}

		/// <summary>Gets a value indicating whether the string format attribute UnicodeClass is selected for the <see cref="T:System.Type" />.</summary>
		/// <returns>true if the string format attribute UnicodeClass is selected for the <see cref="T:System.Type" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x00057BD7 File Offset: 0x00055DD7
		public bool IsUnicodeClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.UnicodeClass;
			}
		}

		/// <summary>Gets a value indicating whether the string format attribute AutoClass is selected for the <see cref="T:System.Type" />.</summary>
		/// <returns>true if the string format attribute AutoClass is selected for the <see cref="T:System.Type" />; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06001593 RID: 5523 RVA: 0x00057BEC File Offset: 0x00055DEC
		public bool IsAutoClass
		{
			get
			{
				return (this.GetAttributeFlagsImpl() & TypeAttributes.StringFormatMask) == TypeAttributes.AutoClass;
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is an array.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an array; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06001594 RID: 5524 RVA: 0x00057C01 File Offset: 0x00055E01
		public bool IsArray
		{
			get
			{
				return this.IsArrayImpl();
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06001595 RID: 5525 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal virtual bool IsSzArray
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current type is a generic type.</summary>
		/// <returns>true if the current type is a generic type; otherwise, false.</returns>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsGenericType
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> represents a generic type definition, from which other generic types can be constructed.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> object represents a generic type definition; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06001597 RID: 5527 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsGenericTypeDefinition
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether this object represents a constructed generic type. You can create instances of a constructed generic type. </summary>
		/// <returns>true if this object represents a constructed generic type; otherwise, false.</returns>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual bool IsConstructedGenericType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> represents a type parameter in the definition of a generic type or method.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> object represents a type parameter of a generic type definition or generic method definition; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06001599 RID: 5529 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsGenericParameter
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets the position of the type parameter in the type parameter list of the generic type or method that declared the parameter, when the <see cref="T:System.Type" /> object represents a type parameter of a generic type or a generic method.</summary>
		/// <returns>The position of a type parameter in the type parameter list of the generic type or method that defines the parameter. Position numbers begin at 0.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type does not represent a type parameter. That is, <see cref="P:System.Type.IsGenericParameter" /> returns false. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600159A RID: 5530 RVA: 0x00057C09 File Offset: 0x00055E09
		public virtual int GenericParameterPosition
		{
			get
			{
				throw new InvalidOperationException(Environment.GetResourceString("Method may only be called on a Type for which Type.IsGenericParameter is true."));
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> object has type parameters that have not been replaced by specific types.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> object is itself a generic type parameter or has type parameters for which specific types have not been supplied; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600159B RID: 5531 RVA: 0x00057C1C File Offset: 0x00055E1C
		public virtual bool ContainsGenericParameters
		{
			get
			{
				if (this.HasElementType)
				{
					return this.GetRootElementType().ContainsGenericParameters;
				}
				if (this.IsGenericParameter)
				{
					return true;
				}
				if (!this.IsGenericType)
				{
					return false;
				}
				Type[] genericArguments = this.GetGenericArguments();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (genericArguments[i].ContainsGenericParameters)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects that represent the constraints on the current generic type parameter. </summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the constraints on the current generic type parameter.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Type" /> object is not a generic type parameter. That is, the <see cref="P:System.Type.IsGenericParameter" /> property returns false.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600159C RID: 5532 RVA: 0x00057C73 File Offset: 0x00055E73
		public virtual Type[] GetGenericParameterConstraints()
		{
			if (!this.IsGenericParameter)
			{
				throw new InvalidOperationException(Environment.GetResourceString("Method may only be called on a Type for which Type.IsGenericParameter is true."));
			}
			throw new InvalidOperationException();
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is passed by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is passed by reference; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700029A RID: 666
		// (get) Token: 0x0600159D RID: 5533 RVA: 0x00057C92 File Offset: 0x00055E92
		public bool IsByRef
		{
			get
			{
				return this.IsByRefImpl();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a pointer.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a pointer; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600159E RID: 5534 RVA: 0x00057C9A File Offset: 0x00055E9A
		public bool IsPointer
		{
			get
			{
				return this.IsPointerImpl();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is one of the primitive types.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is one of the primitive types; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600159F RID: 5535 RVA: 0x00057CA2 File Offset: 0x00055EA2
		public bool IsPrimitive
		{
			get
			{
				return this.IsPrimitiveImpl();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is a COM object.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a COM object; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060015A0 RID: 5536 RVA: 0x00057CAA File Offset: 0x00055EAA
		public bool IsCOMObject
		{
			get
			{
				return this.IsCOMObjectImpl();
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060015A1 RID: 5537 RVA: 0x00057CB2 File Offset: 0x00055EB2
		internal bool IsWindowsRuntimeObject
		{
			get
			{
				return this.IsWindowsRuntimeObjectImpl();
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060015A2 RID: 5538 RVA: 0x00057CBA File Offset: 0x00055EBA
		internal bool IsExportedToWindowsRuntime
		{
			get
			{
				return this.IsExportedToWindowsRuntimeImpl();
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Type" /> encompasses or refers to another type; that is, whether the current <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060015A3 RID: 5539 RVA: 0x00057CC2 File Offset: 0x00055EC2
		public bool HasElementType
		{
			get
			{
				return this.HasElementTypeImpl();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> can be hosted in a context.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> can be hosted in a context; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060015A4 RID: 5540 RVA: 0x00057CCA File Offset: 0x00055ECA
		public bool IsContextful
		{
			get
			{
				return this.IsContextfulImpl();
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Type" /> is marshaled by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is marshaled by reference; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x060015A5 RID: 5541 RVA: 0x00057CD2 File Offset: 0x00055ED2
		public bool IsMarshalByRef
		{
			get
			{
				return this.IsMarshalByRefImpl();
			}
		}

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x060015A6 RID: 5542 RVA: 0x00057CDA File Offset: 0x00055EDA
		internal bool HasProxyAttribute
		{
			get
			{
				return this.HasProxyAttributeImpl();
			}
		}

		/// <summary>Implements the <see cref="P:System.Type.IsValueType" /> property and determines whether the <see cref="T:System.Type" /> is a value type; that is, not a class or an interface.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a value type; otherwise, false.</returns>
		// Token: 0x060015A7 RID: 5543 RVA: 0x00057CE2 File Offset: 0x00055EE2
		protected virtual bool IsValueTypeImpl()
		{
			return this.IsSubclassOf(RuntimeType.ValueType);
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.Attributes" /> property and gets a bitmask indicating the attributes associated with the <see cref="T:System.Type" />.</summary>
		/// <returns>A <see cref="T:System.Reflection.TypeAttributes" /> object representing the attribute set of the <see cref="T:System.Type" />.</returns>
		// Token: 0x060015A8 RID: 5544
		protected abstract TypeAttributes GetAttributeFlagsImpl();

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsArray" /> property and determines whether the <see cref="T:System.Type" /> is an array.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an array; otherwise, false.</returns>
		// Token: 0x060015A9 RID: 5545
		protected abstract bool IsArrayImpl();

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsByRef" /> property and determines whether the <see cref="T:System.Type" /> is passed by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is passed by reference; otherwise, false.</returns>
		// Token: 0x060015AA RID: 5546
		protected abstract bool IsByRefImpl();

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsPointer" /> property and determines whether the <see cref="T:System.Type" /> is a pointer.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a pointer; otherwise, false.</returns>
		// Token: 0x060015AB RID: 5547
		protected abstract bool IsPointerImpl();

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsPrimitive" /> property and determines whether the <see cref="T:System.Type" /> is one of the primitive types.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is one of the primitive types; otherwise, false.</returns>
		// Token: 0x060015AC RID: 5548
		protected abstract bool IsPrimitiveImpl();

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.IsCOMObject" /> property and determines whether the <see cref="T:System.Type" /> is a COM object.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is a COM object; otherwise, false.</returns>
		// Token: 0x060015AD RID: 5549
		protected abstract bool IsCOMObjectImpl();

		// Token: 0x060015AE RID: 5550 RVA: 0x0002126B File Offset: 0x0001F46B
		internal virtual bool IsWindowsRuntimeObjectImpl()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x0002126B File Offset: 0x0001F46B
		internal virtual bool IsExportedToWindowsRuntimeImpl()
		{
			throw new NotImplementedException();
		}

		/// <summary>Substitutes the elements of an array of types for the type parameters of the current generic type definition and returns a <see cref="T:System.Type" /> object representing the resulting constructed type.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the constructed type formed by substituting the elements of <paramref name="typeArguments" /> for the type parameters of the current generic type.</returns>
		/// <param name="typeArguments">An array of types to be substituted for the type parameters of the current generic type.</param>
		/// <exception cref="T:System.InvalidOperationException">The current type does not represent a generic type definition. That is, <see cref="P:System.Type.IsGenericTypeDefinition" /> returns false. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeArguments" /> is null.-or- Any element of <paramref name="typeArguments" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The number of elements in <paramref name="typeArguments" /> is not the same as the number of type parameters in the current generic type definition.-or- Any element of <paramref name="typeArguments" /> does not satisfy the constraints specified for the corresponding type parameter of the current generic type. -or- <paramref name="typeArguments" /> contains an element that is a pointer type (<see cref="P:System.Type.IsPointer" /> returns true), a by-ref type (<see cref="P:System.Type.IsByRef" /> returns true), or <see cref="T:System.Void" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		// Token: 0x060015B0 RID: 5552 RVA: 0x000571DB File Offset: 0x000553DB
		public virtual Type MakeGenericType(params Type[] typeArguments)
		{
			throw new NotSupportedException(Environment.GetResourceString("Derived classes must provide an implementation."));
		}

		/// <summary>Implements the <see cref="P:System.Type.IsContextful" /> property and determines whether the <see cref="T:System.Type" /> can be hosted in a context.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> can be hosted in a context; otherwise, false.</returns>
		// Token: 0x060015B1 RID: 5553 RVA: 0x00057CEF File Offset: 0x00055EEF
		protected virtual bool IsContextfulImpl()
		{
			return typeof(ContextBoundObject).IsAssignableFrom(this);
		}

		/// <summary>Implements the <see cref="P:System.Type.IsMarshalByRef" /> property and determines whether the <see cref="T:System.Type" /> is marshaled by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is marshaled by reference; otherwise, false.</returns>
		// Token: 0x060015B2 RID: 5554 RVA: 0x00057D01 File Offset: 0x00055F01
		protected virtual bool IsMarshalByRefImpl()
		{
			return typeof(MarshalByRefObject).IsAssignableFrom(this);
		}

		// Token: 0x060015B3 RID: 5555 RVA: 0x00015ED5 File Offset: 0x000140D5
		internal virtual bool HasProxyAttributeImpl()
		{
			return false;
		}

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer or reference type.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object encompassed or referred to by the current array, pointer, or reference type, or null if the current <see cref="T:System.Type" /> is not an array or a pointer, or is not passed by reference, or represents a generic type or a type parameter in the definition of a generic type or generic method.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015B4 RID: 5556
		public abstract Type GetElementType();

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic type or the type parameters of a generic type definition.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic type. Returns an empty array if the current type is not a generic type.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015B5 RID: 5557 RVA: 0x000571DB File Offset: 0x000553DB
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException(Environment.GetResourceString("Derived classes must provide an implementation."));
		}

		/// <summary>Gets an array of the generic type arguments for this type.</summary>
		/// <returns>An array of the generic type arguments for this type.</returns>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x00057D13 File Offset: 0x00055F13
		public virtual Type[] GenericTypeArguments
		{
			get
			{
				if (this.IsGenericType && !this.IsGenericTypeDefinition)
				{
					return this.GetGenericArguments();
				}
				return Type.EmptyTypes;
			}
		}

		/// <summary>Returns a <see cref="T:System.Type" /> object that represents a generic type definition from which the current generic type can be constructed.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing a generic type from which the current type can be constructed.</returns>
		/// <exception cref="T:System.InvalidOperationException">The current type is not a generic type.  That is, <see cref="P:System.Type.IsGenericType" /> returns false. </exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015B7 RID: 5559 RVA: 0x000571DB File Offset: 0x000553DB
		public virtual Type GetGenericTypeDefinition()
		{
			throw new NotSupportedException(Environment.GetResourceString("Derived classes must provide an implementation."));
		}

		/// <summary>When overridden in a derived class, implements the <see cref="P:System.Type.HasElementType" /> property and determines whether the current <see cref="T:System.Type" /> encompasses or refers to another type; that is, whether the current <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference.</summary>
		/// <returns>true if the <see cref="T:System.Type" /> is an array, a pointer, or is passed by reference; otherwise, false.</returns>
		// Token: 0x060015B8 RID: 5560
		protected abstract bool HasElementTypeImpl();

		// Token: 0x060015B9 RID: 5561 RVA: 0x00057D34 File Offset: 0x00055F34
		internal Type GetRootElementType()
		{
			Type type = this;
			while (type.HasElementType)
			{
				type = type.GetElementType();
			}
			return type;
		}

		/// <summary>Returns the names of the members of the current enumeration type.</summary>
		/// <returns>An array that contains the names of the members of the enumeration.</returns>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.</exception>
		// Token: 0x060015BA RID: 5562 RVA: 0x00057D58 File Offset: 0x00055F58
		public virtual string[] GetEnumNames()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			string[] array;
			Array array2;
			this.GetEnumData(out array, out array2);
			return array;
		}

		/// <summary>Returns an array of the values of the constants in the current enumeration type.</summary>
		/// <returns>An array that contains the values. The elements of the array are sorted by the binary values (that is, the unsigned values) of the enumeration constants.</returns>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.</exception>
		// Token: 0x060015BB RID: 5563 RVA: 0x00057D8D File Offset: 0x00055F8D
		public virtual Array GetEnumValues()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			throw new NotImplementedException();
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x00057DB4 File Offset: 0x00055FB4
		private Array GetEnumRawConstantValues()
		{
			string[] array;
			Array array2;
			this.GetEnumData(out array, out array2);
			return array2;
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x00057DCC File Offset: 0x00055FCC
		private void GetEnumData(out string[] enumNames, out Array enumValues)
		{
			FieldInfo[] fields = this.GetFields(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			object[] array = new object[fields.Length];
			string[] array2 = new string[fields.Length];
			for (int i = 0; i < fields.Length; i++)
			{
				array2[i] = fields[i].Name;
				array[i] = fields[i].GetRawConstantValue();
			}
			IComparer @default = Comparer.Default;
			for (int j = 1; j < array.Length; j++)
			{
				int num = j;
				string text = array2[j];
				object obj = array[j];
				bool flag = false;
				while (@default.Compare(array[num - 1], obj) > 0)
				{
					array2[num] = array2[num - 1];
					array[num] = array[num - 1];
					num--;
					flag = true;
					if (num == 0)
					{
						break;
					}
				}
				if (flag)
				{
					array2[num] = text;
					array[num] = obj;
				}
			}
			enumNames = array2;
			enumValues = array;
		}

		/// <summary>Returns the underlying type of the current enumeration type. </summary>
		/// <returns>The underlying type of the current enumeration.</returns>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.-or-The enumeration type is not valid, because it contains more than one instance field.</exception>
		// Token: 0x060015BE RID: 5566 RVA: 0x00057E98 File Offset: 0x00056098
		public virtual Type GetEnumUnderlyingType()
		{
			if (!this.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			FieldInfo[] fields = this.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (fields == null || fields.Length != 1)
			{
				throw new ArgumentException(Environment.GetResourceString("The Enum type should contain one and only one instance field."), "enumType");
			}
			return fields[0].FieldType;
		}

		/// <summary>Returns a value that indicates whether the specified value exists in the current enumeration type.</summary>
		/// <returns>true if the specified value is a member of the current enumeration type; otherwise, false.</returns>
		/// <param name="value">The value to be tested.</param>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="value" /> is of a type that cannot be the underlying type of an enumeration.</exception>
		// Token: 0x060015BF RID: 5567 RVA: 0x00057EF4 File Offset: 0x000560F4
		public virtual bool IsEnumDefined(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			Type type = value.GetType();
			if (type.IsEnum)
			{
				if (!type.IsEquivalentTo(this))
				{
					throw new ArgumentException(Environment.GetResourceString("Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.", new object[]
					{
						type.ToString(),
						this.ToString()
					}));
				}
				type = type.GetEnumUnderlyingType();
			}
			if (type == typeof(string))
			{
				return Array.IndexOf<object>(this.GetEnumNames(), value) >= 0;
			}
			if (Type.IsIntegerType(type))
			{
				Type enumUnderlyingType = this.GetEnumUnderlyingType();
				if (enumUnderlyingType.GetTypeCodeImpl() != type.GetTypeCodeImpl())
				{
					throw new ArgumentException(Environment.GetResourceString("Enum underlying type and the object must be same type or object must be a String. Type passed in was '{0}'; the enum underlying type was '{1}'.", new object[]
					{
						type.ToString(),
						enumUnderlyingType.ToString()
					}));
				}
				return Type.BinarySearch(this.GetEnumRawConstantValues(), value) >= 0;
			}
			else
			{
				if (CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
				{
					throw new ArgumentException(Environment.GetResourceString("Enum underlying type and the object must be same type or object must be a String. Type passed in was '{0}'; the enum underlying type was '{1}'.", new object[]
					{
						type.ToString(),
						this.GetEnumUnderlyingType()
					}));
				}
				throw new InvalidOperationException(Environment.GetResourceString("Unknown enum type."));
			}
		}

		/// <summary>Returns the name of the constant that has the specified value, for the current enumeration type.</summary>
		/// <returns>The name of the member of the current enumeration type that has the specified value, or null if no such constant is found.</returns>
		/// <param name="value">The value whose name is to be retrieved.</param>
		/// <exception cref="T:System.ArgumentException">The current type is not an enumeration.-or-<paramref name="value" /> is neither of the current type nor does it have the same underlying type as the current type.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="value" /> is null.</exception>
		// Token: 0x060015C0 RID: 5568 RVA: 0x00058030 File Offset: 0x00056230
		public virtual string GetEnumName(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!this.IsEnum)
			{
				throw new ArgumentException(Environment.GetResourceString("Type provided must be an Enum."), "enumType");
			}
			Type type = value.GetType();
			if (!type.IsEnum && !Type.IsIntegerType(type))
			{
				throw new ArgumentException(Environment.GetResourceString("The value passed in must be an enum base or an underlying type for an enum, such as an Int32."), "value");
			}
			int num = Type.BinarySearch(this.GetEnumRawConstantValues(), value);
			if (num >= 0)
			{
				return this.GetEnumNames()[num];
			}
			return null;
		}

		// Token: 0x060015C1 RID: 5569 RVA: 0x000580B0 File Offset: 0x000562B0
		private static int BinarySearch(Array array, object value)
		{
			ulong[] array2 = new ulong[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = Enum.ToUInt64(array.GetValue(i));
			}
			ulong num = Enum.ToUInt64(value);
			return Array.BinarySearch<ulong>(array2, num);
		}

		// Token: 0x060015C2 RID: 5570 RVA: 0x000580F8 File Offset: 0x000562F8
		internal static bool IsIntegerType(Type t)
		{
			return t == typeof(int) || t == typeof(short) || t == typeof(ushort) || t == typeof(byte) || t == typeof(sbyte) || t == typeof(uint) || t == typeof(long) || t == typeof(ulong) || t == typeof(char) || t == typeof(bool);
		}

		/// <summary>Gets a value that indicates whether the current type is security-critical or security-safe-critical at the current trust level, and therefore can perform critical operations. </summary>
		/// <returns>true if the current type is security-critical or security-safe-critical at the current trust level; false if it is transparent. </returns>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual bool IsSecurityCritical
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the current type is security-safe-critical at the current trust level; that is, whether it can perform critical operations and can be accessed by transparent code. </summary>
		/// <returns>true if the current type is security-safe-critical at the current trust level; false if it is security-critical or transparent.</returns>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the current type is transparent at the current trust level, and therefore cannot perform critical operations.</summary>
		/// <returns>true if the type is security-transparent at the current trust level; otherwise, false.</returns>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060015C5 RID: 5573 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual bool IsSecurityTransparent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x000581C0 File Offset: 0x000563C0
		internal bool NeedsReflectionSecurityCheck
		{
			get
			{
				if (!this.IsVisible)
				{
					return true;
				}
				if (this.IsSecurityCritical && !this.IsSecuritySafeCritical)
				{
					return true;
				}
				if (this.IsGenericType)
				{
					Type[] genericArguments = this.GetGenericArguments();
					for (int i = 0; i < genericArguments.Length; i++)
					{
						if (genericArguments[i].NeedsReflectionSecurityCheck)
						{
							return true;
						}
					}
				}
				else if (this.IsArray || this.IsPointer)
				{
					return this.GetElementType().NeedsReflectionSecurityCheck;
				}
				return false;
			}
		}

		/// <summary>Indicates the type provided by the common language runtime that represents this type.</summary>
		/// <returns>The underlying system type for the <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x060015C7 RID: 5575
		public abstract Type UnderlyingSystemType { get; }

		/// <summary>Determines whether the class represented by the current <see cref="T:System.Type" /> derives from the class represented by the specified <see cref="T:System.Type" />.</summary>
		/// <returns>true if the Type represented by the <paramref name="c" /> parameter and the current Type represent classes, and the class represented by the current Type derives from the class represented by <paramref name="c" />; otherwise, false. This method also returns false if <paramref name="c" /> and the current Type represent the same class.</returns>
		/// <param name="c">The type to compare with the current type. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="c" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015C8 RID: 5576 RVA: 0x00058234 File Offset: 0x00056434
		[ComVisible(true)]
		public virtual bool IsSubclassOf(Type c)
		{
			Type type = this;
			if (type == c)
			{
				return false;
			}
			while (type != null)
			{
				if (type == c)
				{
					return true;
				}
				type = type.BaseType;
			}
			return false;
		}

		/// <summary>Determines whether the specified object is an instance of the current <see cref="T:System.Type" />.</summary>
		/// <returns>true if the current Type is in the inheritance hierarchy of the object represented by <paramref name="o" />, or if the current Type is an interface that <paramref name="o" /> supports. false if neither of these conditions is the case, or if <paramref name="o" /> is null, or if the current Type is an open generic type (that is, <see cref="P:System.Type.ContainsGenericParameters" /> returns true).</returns>
		/// <param name="o">The object to compare with the current type. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015C9 RID: 5577 RVA: 0x0005826A File Offset: 0x0005646A
		public virtual bool IsInstanceOfType(object o)
		{
			return o != null && this.IsAssignableFrom(o.GetType());
		}

		/// <summary>Determines whether an instance of the current <see cref="T:System.Type" /> can be assigned from an instance of the specified Type.</summary>
		/// <returns>true if <paramref name="c" /> and the current Type represent the same type, or if the current Type is in the inheritance hierarchy of <paramref name="c" />, or if the current Type is an interface that <paramref name="c" /> implements, or if <paramref name="c" /> is a generic type parameter and the current Type represents one of the constraints of <paramref name="c" />, or if <paramref name="c" /> represents a value type and the current Type represents Nullable&lt;c&gt; (Nullable(Of c) in Visual Basic). false if none of these conditions are true, or if <paramref name="c" /> is null.</returns>
		/// <param name="c">The type to compare with the current type. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015CA RID: 5578 RVA: 0x00058280 File Offset: 0x00056480
		public virtual bool IsAssignableFrom(Type c)
		{
			if (c == null)
			{
				return false;
			}
			if (this == c)
			{
				return true;
			}
			RuntimeType runtimeType = this.UnderlyingSystemType as RuntimeType;
			if (runtimeType != null)
			{
				return runtimeType.IsAssignableFrom(c);
			}
			if (c.IsSubclassOf(this))
			{
				return true;
			}
			if (this.IsInterface)
			{
				return c.ImplementInterface(this);
			}
			if (this.IsGenericParameter)
			{
				Type[] genericParameterConstraints = this.GetGenericParameterConstraints();
				for (int i = 0; i < genericParameterConstraints.Length; i++)
				{
					if (!genericParameterConstraints[i].IsAssignableFrom(c))
					{
						return false;
					}
				}
				return true;
			}
			return false;
		}

		/// <summary>Determines whether two COM types have the same identity and are eligible for type equivalence.</summary>
		/// <returns>true if the COM types are equivalent; otherwise, false. This method also returns false if one type is in an assembly that is loaded for execution, and the other is in an assembly that is loaded into the reflection-only context.</returns>
		/// <param name="other">The COM type that is tested for equivalence with the current type.</param>
		// Token: 0x060015CB RID: 5579 RVA: 0x00058308 File Offset: 0x00056508
		public virtual bool IsEquivalentTo(Type other)
		{
			return this == other;
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00058314 File Offset: 0x00056514
		internal bool ImplementInterface(Type ifaceType)
		{
			Type type = this;
			while (type != null)
			{
				Type[] interfaces = type.GetInterfaces();
				if (interfaces != null)
				{
					for (int i = 0; i < interfaces.Length; i++)
					{
						if (interfaces[i] == ifaceType || (interfaces[i] != null && interfaces[i].ImplementInterface(ifaceType)))
						{
							return true;
						}
					}
				}
				type = type.BaseType;
			}
			return false;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x00058371 File Offset: 0x00056571
		internal string FormatTypeName()
		{
			return this.FormatTypeName(false);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0002126B File Offset: 0x0001F46B
		internal virtual string FormatTypeName(bool serialization)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a String representing the name of the current Type.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the name of the current <see cref="T:System.Type" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015CF RID: 5583 RVA: 0x0005837A File Offset: 0x0005657A
		public override string ToString()
		{
			return "Type: " + this.Name;
		}

		/// <summary>Gets the types of the objects in the specified array.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects representing the types of the corresponding elements in <paramref name="args" />.</returns>
		/// <param name="args">An array of objects whose types to determine. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="args" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The class initializers are invoked and at least one throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015D0 RID: 5584 RVA: 0x0005838C File Offset: 0x0005658C
		public static Type[] GetTypeArray(object[] args)
		{
			if (args == null)
			{
				throw new ArgumentNullException("args");
			}
			Type[] array = new Type[args.Length];
			for (int i = 0; i < array.Length; i++)
			{
				if (args[i] == null)
				{
					throw new ArgumentNullException();
				}
				array[i] = args[i].GetType();
			}
			return array;
		}

		/// <summary>Determines if the underlying system type of the current <see cref="T:System.Type" /> is the same as the underlying system type of the specified <see cref="T:System.Object" />.</summary>
		/// <returns>true if the underlying system type of <paramref name="o" /> is the same as the underlying system type of the current <see cref="T:System.Type" />; otherwise, false. This method also returns false if the object specified by the <paramref name="o" /> parameter is not a Type.</returns>
		/// <param name="o">The object whose underlying system type is to be compared with the underlying system type of the current <see cref="T:System.Type" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015D1 RID: 5585 RVA: 0x000583D5 File Offset: 0x000565D5
		public override bool Equals(object o)
		{
			return o != null && this.Equals(o as Type);
		}

		/// <summary>Determines if the underlying system type of the current <see cref="T:System.Type" /> is the same as the underlying system type of the specified <see cref="T:System.Type" />.</summary>
		/// <returns>true if the underlying system type of <paramref name="o" /> is the same as the underlying system type of the current <see cref="T:System.Type" />; otherwise, false.</returns>
		/// <param name="o">The object whose underlying system type is to be compared with the underlying system type of the current <see cref="T:System.Type" />. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015D2 RID: 5586 RVA: 0x000583E8 File Offset: 0x000565E8
		public virtual bool Equals(Type o)
		{
			return o != null && this.UnderlyingSystemType == o.UnderlyingSystemType;
		}

		/// <summary>Indicates whether two <see cref="T:System.Type" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x060015D3 RID: 5587 RVA: 0x0003CBCA File Offset: 0x0003ADCA
		public static bool operator ==(Type left, Type right)
		{
			return left == right;
		}

		/// <summary>Indicates whether two <see cref="T:System.Type" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x060015D4 RID: 5588 RVA: 0x0003CBD0 File Offset: 0x0003ADD0
		public static bool operator !=(Type left, Type right)
		{
			return left != right;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>The hash code for this instance.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015D5 RID: 5589 RVA: 0x00058400 File Offset: 0x00056600
		public override int GetHashCode()
		{
			Type underlyingSystemType = this.UnderlyingSystemType;
			if (underlyingSystemType != this)
			{
				return underlyingSystemType.GetHashCode();
			}
			return base.GetHashCode();
		}

		/// <summary>Returns an interface mapping for the specified interface type.</summary>
		/// <returns>An object that represents the interface mapping for <paramref name="interfaceType" />.</returns>
		/// <param name="interfaceType">The interface type to retrieve a mapping for. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="interfaceType" /> is not implemented by the current type. -or-The <paramref name="interfaceType" /> parameter does not refer to an interface. -or-<paramref name="interfaceType" /> is a generic interface, and the current type is an array type. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="interfaceType" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Type" /> represents a generic type parameter; that is, <see cref="P:System.Type.IsGenericParameter" /> is true. </exception>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class. Derived classes must provide an implementation.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015D6 RID: 5590 RVA: 0x000571DB File Offset: 0x000553DB
		[ComVisible(true)]
		public virtual InterfaceMapping GetInterfaceMap(Type interfaceType)
		{
			throw new NotSupportedException(Environment.GetResourceString("Derived classes must provide an implementation."));
		}

		/// <summary>Gets the current <see cref="T:System.Type" />.</summary>
		/// <returns>The current <see cref="T:System.Type" />.</returns>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060015D7 RID: 5591 RVA: 0x00033A19 File Offset: 0x00031C19
		public new Type GetType()
		{
			return base.GetType();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060015D8 RID: 5592 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Type.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">A pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060015D9 RID: 5593 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Type.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060015DA RID: 5594 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Type.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
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
		// Token: 0x060015DB RID: 5595 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Type.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual bool IsSZArray
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060015DD RID: 5597
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type internal_from_name(string name, bool throwOnError, bool ignoreCase);

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, performing a case-sensitive search.</summary>
		/// <returns>The type with the specified name, if found; otherwise, null.</returns>
		/// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="P:System.Type.AssemblyQualifiedName" />. If the type is in the currently executing assembly or in Mscorlib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments.-or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />. </exception>
		/// <exception cref="T:System.IO.FileLoadException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.The assembly or one of its dependencies was found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015DE RID: 5598 RVA: 0x00058425 File Offset: 0x00056625
		public static Type GetType(string typeName)
		{
			return Type.GetType(typeName, false, false);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, performing a case-sensitive search and specifying whether to throw an exception if the type is not found.</summary>
		/// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError" /> parameter specifies whether null is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError" />. See the Exceptions section. </returns>
		/// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="P:System.Type.AssemblyQualifiedName" />. If the type is in the currently executing assembly or in Mscorlib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null. Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true and the type is not found. -or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> is an empty string.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> represents an array type with an invalid size. -or-<paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid syntax. For example, "MyType[,*,]".-or- <paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments.-or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwOnError" /> is true and the assembly or one of its dependencies was not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.IO.IOException" />, instead.The assembly or one of its dependencies was found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015DF RID: 5599 RVA: 0x0005842F File Offset: 0x0005662F
		public static Type GetType(string typeName, bool throwOnError)
		{
			return Type.GetType(typeName, throwOnError, false);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, specifying whether to perform a case-sensitive search and whether to throw an exception if the type is not found.</summary>
		/// <returns>The type with the specified name. If the type is not found, the <paramref name="throwOnError" /> parameter specifies whether null is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwOnError" />. See the Exceptions section. </returns>
		/// <param name="typeName">The assembly-qualified name of the type to get. See <see cref="P:System.Type.AssemblyQualifiedName" />. If the type is in the currently executing assembly or in Mscorlib.dll, it is sufficient to supply the type name qualified by its namespace.</param>
		/// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null.Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <param name="ignoreCase">true to perform a case-insensitive search for <paramref name="typeName" />, false to perform a case-sensitive search for <paramref name="typeName" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true and the type is not found. -or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> is an empty string.-or-<paramref name="throwOnError" /> is true and <paramref name="typeName" /> represents an array type with an invalid size. -or-<paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="throwOnError" /> is true and <paramref name="typeName" /> contains invalid syntax. For example, "MyType[,*,]".-or- <paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments.-or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwOnError" /> is true and the assembly or one of its dependencies was not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-Version 2.0 or later of the common language runtime is currently loaded, and the assembly was compiled with a later version.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015E0 RID: 5600 RVA: 0x0005843C File Offset: 0x0005663C
		public static Type GetType(string typeName, bool throwOnError, bool ignoreCase)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("TypeName");
			}
			if (typeName == string.Empty)
			{
				if (throwOnError)
				{
					throw new TypeLoadException("A null or zero length string does not represent a valid Type.");
				}
				return null;
			}
			else
			{
				Type type = Type.internal_from_name(typeName, throwOnError, ignoreCase);
				if (throwOnError && type == null)
				{
					throw new TypeLoadException("Error loading '" + typeName + "'");
				}
				return type;
			}
		}

		/// <summary>Gets the <see cref="T:System.Type" /> with the specified name, specifying whether to perform a case-sensitive search and whether to throw an exception if the type is not found. The type is loaded for reflection only, not for execution.</summary>
		/// <returns>The type with the specified name, if found; otherwise, null. If the type is not found, the <paramref name="throwIfNotFound" /> parameter specifies whether null is returned or an exception is thrown. In some cases, an exception is thrown regardless of the value of <paramref name="throwIfNotFound" />. See the Exceptions section.</returns>
		/// <param name="typeName">The assembly-qualified name of the <see cref="T:System.Type" /> to get. </param>
		/// <param name="throwIfNotFound">true to throw a <see cref="T:System.TypeLoadException" /> if the type cannot be found; false to return null if the type cannot be found. Specifying false also suppresses some other exception conditions, but not all of them. See the Exceptions section.</param>
		/// <param name="ignoreCase">true to perform a case-insensitive search for <paramref name="typeName" />; false to perform a case-sensitive search for <paramref name="typeName" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="typeName" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwIfNotFound" /> is true and the type is not found. -or-<paramref name="throwIfNotFound" /> is true and <paramref name="typeName" /> contains invalid characters, such as an embedded tab.-or-<paramref name="throwIfNotFound" /> is true and <paramref name="typeName" /> is an empty string.-or-<paramref name="throwIfNotFound" /> is true and <paramref name="typeName" /> represents an array type with an invalid size. -or-<paramref name="typeName" /> represents an array of <see cref="T:System.TypedReference" /> objects. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="typeName" /> does not include the assembly name.-or-<paramref name="throwIfNotFound" /> is true and <paramref name="typeName" /> contains invalid syntax; for example, "MyType[,*,]".-or-<paramref name="typeName" /> represents a generic type that has a pointer type, a ByRef type, or <see cref="T:System.Void" /> as one of its type arguments.-or-<paramref name="typeName" /> represents a generic type that has an incorrect number of type arguments.-or-<paramref name="typeName" /> represents a generic type, and one of its type arguments does not satisfy the constraints for the corresponding type parameter.</exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="throwIfNotFound" /> is true and the assembly or one of its dependencies was not found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">The assembly or one of its dependencies was found, but could not be loaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">The assembly or one of its dependencies is not valid. -or-The assembly was compiled with a later version of the common language runtime than the version that is currently loaded.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015E1 RID: 5601 RVA: 0x000584A0 File Offset: 0x000566A0
		public static Type ReflectionOnlyGetType(string typeName, bool throwIfNotFound, bool ignoreCase)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (typeName == string.Empty && throwIfNotFound)
			{
				throw new TypeLoadException("A null or zero length string does not represent a valid Type");
			}
			int num = typeName.IndexOf(',');
			if (num < 0 || num == 0 || num == typeName.Length - 1)
			{
				throw new ArgumentException("Assembly qualifed type name is required", "typeName");
			}
			string text = typeName.Substring(num + 1);
			Assembly assembly;
			try
			{
				assembly = Assembly.ReflectionOnlyLoad(text);
			}
			catch
			{
				if (throwIfNotFound)
				{
					throw;
				}
				return null;
			}
			return assembly.GetType(typeName.Substring(0, num), throwIfNotFound, ignoreCase);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x00058540 File Offset: 0x00056740
		internal virtual Type InternalResolve()
		{
			return this.UnderlyingSystemType;
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0002126B File Offset: 0x0001F46B
		internal virtual Type RuntimeResolve()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x00003B29 File Offset: 0x00001D29
		internal virtual bool IsUserType
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00058548 File Offset: 0x00056748
		internal virtual MethodInfo GetMethod(MethodInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x00058548 File Offset: 0x00056748
		internal virtual ConstructorInfo GetConstructor(ConstructorInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00058548 File Offset: 0x00056748
		internal virtual FieldInfo GetField(FieldInfo fromNoninstanciated)
		{
			throw new InvalidOperationException("can only be called in generic type");
		}

		/// <summary>Gets the type referenced by the specified type handle.</summary>
		/// <returns>The type referenced by the specified <see cref="T:System.RuntimeTypeHandle" />, or null if the <see cref="P:System.RuntimeTypeHandle.Value" /> property of <paramref name="handle" /> is null.</returns>
		/// <param name="handle">The object that refers to the type. </param>
		/// <exception cref="T:System.Reflection.TargetInvocationException">A class initializer is invoked and throws an exception. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060015E8 RID: 5608 RVA: 0x00058554 File Offset: 0x00056754
		public static Type GetTypeFromHandle(RuntimeTypeHandle handle)
		{
			if (handle.Value == IntPtr.Zero)
			{
				return null;
			}
			return Type.internal_from_handle(handle.Value);
		}

		// Token: 0x060015E9 RID: 5609
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type internal_from_handle(IntPtr handle);

		/// <summary>Represents the member filter used on attributes. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000BA7 RID: 2983
		public static readonly MemberFilter FilterAttribute = new MemberFilter(__Filters.Instance.FilterAttribute);

		/// <summary>Represents the case-sensitive member filter used on names. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000BA8 RID: 2984
		public static readonly MemberFilter FilterName = new MemberFilter(__Filters.Instance.FilterName);

		/// <summary>Represents the case-insensitive member filter used on names. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000BA9 RID: 2985
		public static readonly MemberFilter FilterNameIgnoreCase = new MemberFilter(__Filters.Instance.FilterIgnoreCase);

		/// <summary>Represents a missing value in the <see cref="T:System.Type" /> information. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000BAA RID: 2986
		public static readonly object Missing = global::System.Reflection.Missing.Value;

		/// <summary>Separates names in the namespace of the <see cref="T:System.Type" />. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000BAB RID: 2987
		public static readonly char Delimiter = '.';

		/// <summary>Represents an empty array of type <see cref="T:System.Type" />. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000BAC RID: 2988
		public static readonly Type[] EmptyTypes = EmptyArray<Type>.Value;

		// Token: 0x04000BAD RID: 2989
		private static Binder defaultBinder;

		// Token: 0x04000BAE RID: 2990
		private const BindingFlags DefaultLookup = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;

		// Token: 0x04000BAF RID: 2991
		internal const BindingFlags DeclaredOnlyLookup = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

		// Token: 0x04000BB0 RID: 2992
		internal RuntimeTypeHandle _impl;
	}
}
