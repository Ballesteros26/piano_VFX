using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Reflection
{
	/// <summary>Performs reflection on a module.</summary>
	// Token: 0x0200031F RID: 799
	[ClassInterface(ClassInterfaceType.None)]
	[ComDefaultInterface(typeof(_Module))]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class Module : ISerializable, ICustomAttributeProvider, _Module
	{
		/// <summary>Gets a handle for the module.</summary>
		/// <returns>A <see cref="T:System.ModuleHandle" /> structure for the current module.</returns>
		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x00081DFE File Offset: 0x0007FFFE
		public ModuleHandle ModuleHandle
		{
			get
			{
				return new ModuleHandle(this._impl);
			}
		}

		// Token: 0x060022EC RID: 8940
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_MetadataToken(Module module);

		// Token: 0x060022ED RID: 8941
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetMDStreamVersion(IntPtr module_handle);

		/// <summary>Returns a field having the specified name.</summary>
		/// <returns>A FieldInfo object having the specified name, or null if the field does not exist.</returns>
		/// <param name="name">The field name. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x060022EE RID: 8942 RVA: 0x00081E0B File Offset: 0x0008000B
		public FieldInfo GetField(string name)
		{
			return this.GetField(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Returns the global fields defined on the module.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.FieldInfo" /> objects representing the global fields defined on the module; if there are no global fields, an empty array is returned.</returns>
		// Token: 0x060022EF RID: 8943 RVA: 0x00081E16 File Offset: 0x00080016
		public FieldInfo[] GetFields()
		{
			return this.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Returns a method having the specified name.</summary>
		/// <returns>A MethodInfo object having the specified name, or null if the method does not exist.</returns>
		/// <param name="name">The method name. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		// Token: 0x060022F0 RID: 8944 RVA: 0x00081E20 File Offset: 0x00080020
		public MethodInfo GetMethod(string name)
		{
			return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, null, null);
		}

		/// <summary>Returns a method having the specified name and parameter types.</summary>
		/// <returns>A MethodInfo object in accordance with the specified criteria, or null if the method does not exist.</returns>
		/// <param name="name">The method name. </param>
		/// <param name="types">The parameter types to search for. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null, <paramref name="types" /> is null, or <paramref name="types" /> (i) is null. </exception>
		// Token: 0x060022F1 RID: 8945 RVA: 0x00081E2F File Offset: 0x0008002F
		public MethodInfo GetMethod(string name, Type[] types)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			return this.GetMethodImpl(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public, null, CallingConventions.Any, types, null);
		}

		/// <summary>Returns a method having the specified name, binding information, calling convention, and parameter types and modifiers.</summary>
		/// <returns>A MethodInfo object in accordance with the specified criteria, or null if the method does not exist.</returns>
		/// <param name="name">The method name. </param>
		/// <param name="bindingAttr">One of the BindingFlags bit flags used to control the search. </param>
		/// <param name="binder">An object that implements Binder, containing properties related to this method. </param>
		/// <param name="callConvention">The calling convention for the method. </param>
		/// <param name="types">The parameter types to search for. </param>
		/// <param name="modifiers">An array of parameter modifiers used to make binding work with parameter signatures in which the types have been modified. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null, <paramref name="types" /> is null, or <paramref name="types" /> (i) is null. </exception>
		// Token: 0x060022F2 RID: 8946 RVA: 0x00081E4C File Offset: 0x0008004C
		public MethodInfo GetMethod(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			if (types == null)
			{
				throw new ArgumentNullException("types");
			}
			return this.GetMethodImpl(name, bindingAttr, binder, callConvention, types, modifiers);
		}

		/// <summary>Returns the global methods defined on the module.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects representing all the global methods defined on the module; if there are no global methods, an empty array is returned.</returns>
		// Token: 0x060022F3 RID: 8947 RVA: 0x00081E6C File Offset: 0x0008006C
		public MethodInfo[] GetMethods()
		{
			return this.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
		}

		/// <summary>Provides an <see cref="T:System.Runtime.Serialization.ISerializable" /> implementation for serialized objects.</summary>
		/// <param name="info">The information and data needed to serialize or deserialize an object. </param>
		/// <param name="context">The context for the serialization. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
		/// </PermissionSet>
		// Token: 0x060022F4 RID: 8948 RVA: 0x0002126B File Offset: 0x0001F46B
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the specified type, performing a case-sensitive search.</summary>
		/// <returns>A Type object representing the given type, if the type is in this module; otherwise, null.</returns>
		/// <param name="className">The name of the type to locate. The name must be fully qualified with the namespace. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The class initializers are invoked and an exception is thrown. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="className" /> is a zero-length string. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="className" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="className" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="className" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="className" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="className" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x060022F5 RID: 8949 RVA: 0x00081E76 File Offset: 0x00080076
		[ComVisible(true)]
		public virtual Type GetType(string className)
		{
			return this.GetType(className, false, false);
		}

		/// <summary>Returns the specified type, searching the module with the specified case sensitivity.</summary>
		/// <returns>A Type object representing the given type, if the type is in this module; otherwise, null.</returns>
		/// <param name="className">The name of the type to locate. The name must be fully qualified with the namespace. </param>
		/// <param name="ignoreCase">true for case-insensitive search; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The class initializers are invoked and an exception is thrown. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="className" /> is a zero-length string. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="className" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="className" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="className" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="className" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="className" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x060022F6 RID: 8950 RVA: 0x00081E81 File Offset: 0x00080081
		[ComVisible(true)]
		public virtual Type GetType(string className, bool ignoreCase)
		{
			return this.GetType(className, false, ignoreCase);
		}

		// Token: 0x060022F7 RID: 8951
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type[] InternalGetTypes();

		/// <summary>Returns the name of the module.</summary>
		/// <returns>A String representing the name of this module.</returns>
		// Token: 0x060022F8 RID: 8952 RVA: 0x00081E8C File Offset: 0x0008008C
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x060022F9 RID: 8953 RVA: 0x00081E94 File Offset: 0x00080094
		internal Guid MvId
		{
			get
			{
				return this.GetModuleVersionId();
			}
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x00081E9C File Offset: 0x0008009C
		internal Exception resolve_token_exception(int metadataToken, ResolveTokenError error, string tokenType)
		{
			if (error == ResolveTokenError.OutOfRange)
			{
				return new ArgumentOutOfRangeException("metadataToken", string.Format("Token 0x{0:x} is not valid in the scope of module {1}", metadataToken, this.name));
			}
			return new ArgumentException(string.Format("Token 0x{0:x} is not a valid {1} token in the scope of module {2}", metadataToken, tokenType, this.name), "metadataToken");
		}

		// Token: 0x060022FB RID: 8955 RVA: 0x00081EF0 File Offset: 0x000800F0
		internal IntPtr[] ptrs_from_types(Type[] types)
		{
			if (types == null)
			{
				return null;
			}
			IntPtr[] array = new IntPtr[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				if (types[i] == null)
				{
					throw new ArgumentException();
				}
				array[i] = types[i].TypeHandle.Value;
			}
			return array;
		}

		/// <summary>Returns the field identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a field in the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a field in the scope of the current module.-or-<paramref name="metadataToken" /> identifies a field whose parent TypeSpec has a signature containing element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x060022FC RID: 8956 RVA: 0x00081F3E File Offset: 0x0008013E
		public FieldInfo ResolveField(int metadataToken)
		{
			return this.ResolveField(metadataToken, null, null);
		}

		/// <summary>Returns the type or member identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberInfo" /> object representing the type or member that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a type or member in the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a type or member in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec or TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method).-or-<paramref name="metadataToken" /> identifies a property or event.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x060022FD RID: 8957 RVA: 0x00081F49 File Offset: 0x00080149
		public MemberInfo ResolveMember(int metadataToken)
		{
			return this.ResolveMember(metadataToken, null, null);
		}

		/// <summary>Returns the method or constructor identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> object representing the method or constructor that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a method or constructor in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x060022FE RID: 8958 RVA: 0x00081F54 File Offset: 0x00080154
		public MethodBase ResolveMethod(int metadataToken)
		{
			return this.ResolveMethod(metadataToken, null, null);
		}

		/// <summary>Returns the type identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the type that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a type in the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a type in the scope of the current module.-or-<paramref name="metadataToken" /> is a TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method). </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x060022FF RID: 8959 RVA: 0x00081F5F File Offset: 0x0008015F
		public Type ResolveType(int metadataToken)
		{
			return this.ResolveType(metadataToken, null, null);
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x00081F6C File Offset: 0x0008016C
		internal static Type MonoDebugger_ResolveType(Module module, int token)
		{
			ResolveTokenError resolveTokenError;
			IntPtr intPtr = Module.ResolveTypeToken(module._impl, token, null, null, out resolveTokenError);
			if (intPtr == IntPtr.Zero)
			{
				return null;
			}
			return Type.GetTypeFromHandle(new RuntimeTypeHandle(intPtr));
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x00081E94 File Offset: 0x00080094
		internal static Guid Mono_GetGuid(Module module)
		{
			return module.GetModuleVersionId();
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x00081FA4 File Offset: 0x000801A4
		internal virtual Guid GetModuleVersionId()
		{
			return new Guid(this.GetGuidInternal());
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x00081FB4 File Offset: 0x000801B4
		private static bool filter_by_type_name(Type m, object filterCriteria)
		{
			string text = (string)filterCriteria;
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				return m.Name.StartsWithOrdinalUnchecked(text.Substring(0, text.Length - 1));
			}
			return m.Name == text;
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x0008200C File Offset: 0x0008020C
		private static bool filter_by_type_name_ignore_case(Type m, object filterCriteria)
		{
			string text = (string)filterCriteria;
			if (text.Length > 0 && text[text.Length - 1] == '*')
			{
				return m.Name.StartsWith(text.Substring(0, text.Length - 1), StringComparison.OrdinalIgnoreCase);
			}
			return string.Compare(m.Name, text, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002305 RID: 8965
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern IntPtr GetHINSTANCE();

		// Token: 0x06002306 RID: 8966
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern string GetGuidInternal();

		// Token: 0x06002307 RID: 8967
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type GetGlobalType();

		// Token: 0x06002308 RID: 8968
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ResolveTypeToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x06002309 RID: 8969
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ResolveMethodToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x0600230A RID: 8970
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr ResolveFieldToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x0600230B RID: 8971
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string ResolveStringToken(IntPtr module, int token, out ResolveTokenError error);

		// Token: 0x0600230C RID: 8972
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MemberInfo ResolveMemberToken(IntPtr module, int token, IntPtr[] type_args, IntPtr[] method_args, out ResolveTokenError error);

		// Token: 0x0600230D RID: 8973
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern byte[] ResolveSignature(IntPtr module, int metadataToken, out ResolveTokenError error);

		// Token: 0x0600230E RID: 8974
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetPEKind(IntPtr module, out PortableExecutableKinds peKind, out ImageFileMachine machine);

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600230F RID: 8975 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Module.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002310 RID: 8976 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Module.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002311 RID: 8977 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Module.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides access to properties and methods exposed by an object.</summary>
		/// <param name="dispIdMember">Identifies the member.</param>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="lcid">The locale context in which to interpret arguments.</param>
		/// <param name="wFlags">Flags describing the context of the call.</param>
		/// <param name="pDispParams">Pointer to a structure containing an array of arguments, an array of argument DispIDs for named arguments, and counts for the number of elements in the arrays.</param>
		/// <param name="pVarResult">Pointer to the location where the result is to be stored.</param>
		/// <param name="pExcepInfo">Pointer to a structure that contains exception information.</param>
		/// <param name="puArgErr">The index of the first argument that has an error.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002312 RID: 8978 RVA: 0x0002126B File Offset: 0x0001F46B
		void _Module.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether this module and the specified object are equal.</summary>
		/// <returns>true if <paramref name="o" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="o">The object to compare with this instance. </param>
		// Token: 0x06002313 RID: 8979 RVA: 0x0004BD58 File Offset: 0x00049F58
		public override bool Equals(object o)
		{
			return o == this;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002314 RID: 8980 RVA: 0x0007E1C5 File Offset: 0x0007C3C5
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Module" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare. </param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002315 RID: 8981 RVA: 0x00080329 File Offset: 0x0007E529
		public static bool operator ==(Module left, Module right)
		{
			return left == right || (!((left == null) ^ (right == null)) && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.Module" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002316 RID: 8982 RVA: 0x00080345 File Offset: 0x0007E545
		public static bool operator !=(Module left, Module right)
		{
			return left != right && (((left == null) ^ (right == null)) || !left.Equals(right));
		}

		/// <summary>Gets the appropriate <see cref="T:System.Reflection.Assembly" /> for this instance of <see cref="T:System.Reflection.Module" />.</summary>
		/// <returns>An Assembly object.</returns>
		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x00082067 File Offset: 0x00080267
		public virtual Assembly Assembly
		{
			get
			{
				throw Module.CreateNIE();
			}
		}

		/// <summary>Gets a String representing the name of the module with the path removed.</summary>
		/// <returns>The module name with no path.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x00082067 File Offset: 0x00080267
		public virtual string Name
		{
			get
			{
				throw Module.CreateNIE();
			}
		}

		/// <summary>Gets a string representing the name of the module.</summary>
		/// <returns>The module name.</returns>
		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06002319 RID: 8985 RVA: 0x00082067 File Offset: 0x00080267
		public virtual string ScopeName
		{
			get
			{
				throw Module.CreateNIE();
			}
		}

		/// <summary>Gets the metadata stream version.</summary>
		/// <returns>A 32-bit integer representing the metadata stream version. The high-order two bytes represent the major version number, and the low-order two bytes represent the minor version number.</returns>
		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x00082067 File Offset: 0x00080267
		public virtual int MDStreamVersion
		{
			get
			{
				throw Module.CreateNIE();
			}
		}

		/// <summary>Gets a universally unique identifier (UUID) that can be used to distinguish between two versions of a module.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that can be used to distinguish between two versions of a module.</returns>
		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x00082067 File Offset: 0x00080267
		public virtual Guid ModuleVersionId
		{
			get
			{
				throw Module.CreateNIE();
			}
		}

		/// <summary>Gets a string representing the fully qualified name and path to this module.</summary>
		/// <returns>The fully qualified module name.</returns>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600231C RID: 8988 RVA: 0x00082067 File Offset: 0x00080267
		public virtual string FullyQualifiedName
		{
			get
			{
				throw Module.CreateNIE();
			}
		}

		/// <summary>Gets a token that identifies the module in metadata.</summary>
		/// <returns>An integer token that identifies the current module in metadata.</returns>
		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x00082067 File Offset: 0x00080267
		public virtual int MetadataToken
		{
			get
			{
				throw Module.CreateNIE();
			}
		}

		// Token: 0x0600231E RID: 8990 RVA: 0x00080315 File Offset: 0x0007E515
		private static Exception CreateNIE()
		{
			return new NotImplementedException("Derived classes must implement it");
		}

		/// <summary>Gets a value indicating whether the object is a resource.</summary>
		/// <returns>true if the object is a resource; otherwise, false.</returns>
		// Token: 0x0600231F RID: 8991 RVA: 0x00082067 File Offset: 0x00080267
		public virtual bool IsResource()
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns an array of classes accepted by the given filter and filter criteria.</summary>
		/// <returns>An array of type Type containing classes that were accepted by the filter.</returns>
		/// <param name="filter">The delegate used to filter the classes. </param>
		/// <param name="filterCriteria">An Object used to filter the classes. </param>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">One or more classes in a module could not be loaded. </exception>
		// Token: 0x06002320 RID: 8992 RVA: 0x00082067 File Offset: 0x00080267
		public virtual Type[] FindTypes(TypeFilter filter, object filterCriteria)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns all custom attributes.</summary>
		/// <returns>An array of type Object containing all custom attributes.</returns>
		/// <param name="inherit">This argument is ignored for objects of this type. </param>
		// Token: 0x06002321 RID: 8993 RVA: 0x00082067 File Offset: 0x00080267
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Gets custom attributes of the specified type.</summary>
		/// <returns>An array of type Object containing all custom attributes of the specified type.</returns>
		/// <param name="attributeType">The type of attribute to get. </param>
		/// <param name="inherit">This argument is ignored for objects of this type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the runtime. For example, <paramref name="attributeType" /> is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> object.</exception>
		// Token: 0x06002322 RID: 8994 RVA: 0x00082067 File Offset: 0x00080267
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects for the current module, which can be used in the reflection-only context.</summary>
		/// <returns>A generic list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the current module.</returns>
		// Token: 0x06002323 RID: 8995 RVA: 0x00082067 File Offset: 0x00080267
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns a field having the specified name and binding attributes.</summary>
		/// <returns>A FieldInfo object having the specified name and binding attributes, or null if the field does not exist.</returns>
		/// <param name="name">The field name. </param>
		/// <param name="bindingAttr">One of the BindingFlags bit flags used to control the search. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is null. </exception>
		// Token: 0x06002324 RID: 8996 RVA: 0x00082067 File Offset: 0x00080267
		public virtual FieldInfo GetField(string name, BindingFlags bindingAttr)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns the global fields defined on the module that match the specified binding flags.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.FieldInfo" /> representing the global fields defined on the module that match the specified binding flags; if no global fields match the binding flags, an empty array is returned.</returns>
		/// <param name="bindingFlags">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values that limit the search.</param>
		// Token: 0x06002325 RID: 8997 RVA: 0x00082067 File Offset: 0x00080267
		public virtual FieldInfo[] GetFields(BindingFlags bindingFlags)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns the method implementation in accordance with the specified criteria.</summary>
		/// <returns>A MethodInfo object containing implementation information as specified, or null if the method does not exist.</returns>
		/// <param name="name">The method name. </param>
		/// <param name="bindingAttr">One of the BindingFlags bit flags used to control the search. </param>
		/// <param name="binder">An object that implements Binder, containing properties related to this method. </param>
		/// <param name="callConvention">The calling convention for the method. </param>
		/// <param name="types">The parameter types to search for. </param>
		/// <param name="modifiers">An array of parameter modifiers used to make binding work with parameter signatures in which the types have been modified. </param>
		/// <exception cref="T:System.Reflection.AmbiguousMatchException">
		///   <paramref name="types" /> is null. </exception>
		// Token: 0x06002326 RID: 8998 RVA: 0x00082067 File Offset: 0x00080267
		protected virtual MethodInfo GetMethodImpl(string name, BindingFlags bindingAttr, Binder binder, CallingConventions callConvention, Type[] types, ParameterModifier[] modifiers)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns the global methods defined on the module that match the specified binding flags.</summary>
		/// <returns>An array of type <see cref="T:System.Reflection.MethodInfo" /> representing the global methods defined on the module that match the specified binding flags; if no global methods match the binding flags, an empty array is returned.</returns>
		/// <param name="bindingFlags">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values that limit the search.</param>
		// Token: 0x06002327 RID: 8999 RVA: 0x00082067 File Offset: 0x00080267
		public virtual MethodInfo[] GetMethods(BindingFlags bindingFlags)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Gets a pair of values indicating the nature of the code in a module and the platform targeted by the module.</summary>
		/// <param name="peKind">When this method returns, a combination of the <see cref="T:System.Reflection.PortableExecutableKinds" /> values indicating the nature of the code in the module.</param>
		/// <param name="machine">When this method returns, one of the <see cref="T:System.Reflection.ImageFileMachine" /> values indicating the platform targeted by the module.</param>
		// Token: 0x06002328 RID: 9000 RVA: 0x00082067 File Offset: 0x00080267
		public virtual void GetPEKind(out PortableExecutableKinds peKind, out ImageFileMachine machine)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns the specified type, specifying whether to make a case-sensitive search of the module and whether to throw an exception if the type cannot be found.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the specified type, if the type is declared in this module; otherwise, null.</returns>
		/// <param name="className">The name of the type to locate. The name must be fully qualified with the namespace. </param>
		/// <param name="throwOnError">true to throw an exception if the type cannot be found; false to return null. </param>
		/// <param name="ignoreCase">true for case-insensitive search; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="className" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The class initializers are invoked and an exception is thrown. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="className" /> is a zero-length string. </exception>
		/// <exception cref="T:System.TypeLoadException">
		///   <paramref name="throwOnError" /> is true, and the type cannot be found. </exception>
		/// <exception cref="T:System.IO.FileNotFoundException">
		///   <paramref name="className" /> requires a dependent assembly that could not be found. </exception>
		/// <exception cref="T:System.IO.FileLoadException">
		///   <paramref name="className" /> requires a dependent assembly that was found but could not be loaded.-or-The current assembly was loaded into the reflection-only context, and <paramref name="className" /> requires a dependent assembly that was not preloaded. </exception>
		/// <exception cref="T:System.BadImageFormatException">
		///   <paramref name="className" /> requires a dependent assembly, but the file is not a valid assembly. -or-<paramref name="className" /> requires a dependent assembly which was compiled for a version of the runtime later than the currently loaded version.</exception>
		// Token: 0x06002329 RID: 9001 RVA: 0x00082067 File Offset: 0x00080267
		[ComVisible(true)]
		public virtual Type GetType(string className, bool throwOnError, bool ignoreCase)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns a value that indicates whether the specified attribute type has been applied to this module.</summary>
		/// <returns>true if one or more instances of <paramref name="attributeType" /> have been applied to this module; otherwise, false.</returns>
		/// <param name="attributeType">The type of custom attribute to test for. </param>
		/// <param name="inherit">This argument is ignored for objects of this type. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the runtime. For example, <paramref name="attributeType" /> is a <see cref="T:System.Reflection.Emit.TypeBuilder" /> object.</exception>
		// Token: 0x0600232A RID: 9002 RVA: 0x00082067 File Offset: 0x00080267
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns the field identified by the specified metadata token, in the context defined by the specified generic type parameters.</summary>
		/// <returns>A <see cref="T:System.Reflection.FieldInfo" /> object representing the field that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a field in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a field in the scope of the current module.-or-<paramref name="metadataToken" /> identifies a field whose parent TypeSpec has a signature containing element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x0600232B RID: 9003 RVA: 0x00082067 File Offset: 0x00080267
		public virtual FieldInfo ResolveField(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns the type or member identified by the specified metadata token, in the context defined by the specified generic type parameters.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberInfo" /> object representing the type or member that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a type or member in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a type or member in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec or TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.-or-<paramref name="metadataToken" /> identifies a property or event.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x0600232C RID: 9004 RVA: 0x00082067 File Offset: 0x00080267
		public virtual MemberInfo ResolveMember(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns the method or constructor identified by the specified metadata token, in the context defined by the specified generic type parameters. </summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> object representing the method that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a method or constructor in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a method or constructor in the scope of the current module.-or-<paramref name="metadataToken" /> is a MethodSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x0600232D RID: 9005 RVA: 0x00082067 File Offset: 0x00080267
		public virtual MethodBase ResolveMethod(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns the signature blob identified by a metadata token.</summary>
		/// <returns>An array of bytes representing the signature blob.</returns>
		/// <param name="metadataToken">A metadata token that identifies a signature in the module.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a valid MemberRef, MethodDef, TypeSpec, signature, or FieldDef token in the scope of the current module.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x0600232E RID: 9006 RVA: 0x00082067 File Offset: 0x00080267
		public virtual byte[] ResolveSignature(int metadataToken)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns the string identified by the specified metadata token.</summary>
		/// <returns>A <see cref="T:System.String" /> containing a string value from the metadata string heap.</returns>
		/// <param name="metadataToken">A metadata token that identifies a string in the string heap of the module. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a string in the scope of the current module. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x0600232F RID: 9007 RVA: 0x00082067 File Offset: 0x00080267
		public virtual string ResolveString(int metadataToken)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns the type identified by the specified metadata token, in the context defined by the specified generic type parameters.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the type that is identified by the specified metadata token.</returns>
		/// <param name="metadataToken">A metadata token that identifies a type in the module.</param>
		/// <param name="genericTypeArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the type where the token is in scope, or null if that type is not generic. </param>
		/// <param name="genericMethodArguments">An array of <see cref="T:System.Type" /> objects representing the generic type arguments of the method where the token is in scope, or null if that method is not generic.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="metadataToken" /> is not a token for a type in the scope of the current module.-or-<paramref name="metadataToken" /> is a TypeSpec whose signature contains element type var (a type parameter of a generic type) or mvar (a type parameter of a generic method), and the necessary generic type arguments were not supplied for either or both of <paramref name="genericTypeArguments" /> and <paramref name="genericMethodArguments" />.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="metadataToken" /> is not a valid token in the scope of the current module.</exception>
		// Token: 0x06002330 RID: 9008 RVA: 0x00082067 File Offset: 0x00080267
		public virtual Type ResolveType(int metadataToken, Type[] genericTypeArguments, Type[] genericMethodArguments)
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns an X509Certificate object corresponding to the certificate included in the Authenticode signature of the assembly which this module belongs to. If the assembly has not been Authenticode signed, null is returned.</summary>
		/// <returns>An X509Certificate object, or null if the assembly to which this module belongs has not been Authenticode signed.</returns>
		// Token: 0x06002331 RID: 9009 RVA: 0x00082067 File Offset: 0x00080267
		public virtual X509Certificate GetSignerCertificate()
		{
			throw Module.CreateNIE();
		}

		/// <summary>Returns all the types defined within this module.</summary>
		/// <returns>An array of type Type containing types defined within the module that is reflected by this instance.</returns>
		/// <exception cref="T:System.Reflection.ReflectionTypeLoadException">One or more classes in a module could not be loaded. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		// Token: 0x06002332 RID: 9010 RVA: 0x00082067 File Offset: 0x00080267
		public virtual Type[] GetTypes()
		{
			throw Module.CreateNIE();
		}

		/// <summary>Gets a collection that contains this module's custom attributes.</summary>
		/// <returns>A collection that contains this module's custom attributes.</returns>
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x0008206E File Offset: 0x0008026E
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		/// <summary>A TypeFilter object that filters the list of types defined in this module based upon the name. This field is case-sensitive and read-only.</summary>
		// Token: 0x0400132E RID: 4910
		public static readonly TypeFilter FilterTypeName = new TypeFilter(Module.filter_by_type_name);

		/// <summary>A TypeFilter object that filters the list of types defined in this module based upon the name. This field is case-insensitive and read-only.</summary>
		// Token: 0x0400132F RID: 4911
		public static readonly TypeFilter FilterTypeNameIgnoreCase = new TypeFilter(Module.filter_by_type_name_ignore_case);

		// Token: 0x04001330 RID: 4912
		internal IntPtr _impl;

		// Token: 0x04001331 RID: 4913
		internal Assembly assembly;

		// Token: 0x04001332 RID: 4914
		internal string fqname;

		// Token: 0x04001333 RID: 4915
		internal string name;

		// Token: 0x04001334 RID: 4916
		internal string scopename;

		// Token: 0x04001335 RID: 4917
		internal bool is_resource;

		// Token: 0x04001336 RID: 4918
		internal int token;

		// Token: 0x04001337 RID: 4919
		private const BindingFlags defaultBindingFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public;
	}
}
