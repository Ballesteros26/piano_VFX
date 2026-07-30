using System;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Reflection
{
	/// <summary>Provides information about methods and constructors. </summary>
	// Token: 0x020002F2 RID: 754
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_MethodBase))]
	[ClassInterface(ClassInterfaceType.None)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	[Serializable]
	public abstract class MethodBase : MemberInfo, _MethodBase
	{
		/// <summary>Gets method information by using the method's internal metadata representation (handle).</summary>
		/// <returns>A MethodBase containing information about the method.</returns>
		/// <param name="handle">The method's handle. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.</exception>
		// Token: 0x0600208E RID: 8334 RVA: 0x0007E7B8 File Offset: 0x0007C9B8
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("The handle is invalid."));
			}
			MethodBase methodFromHandleInternalType = MethodBase.GetMethodFromHandleInternalType(handle.Value, IntPtr.Zero);
			if (methodFromHandleInternalType == null)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			Type declaringType = methodFromHandleInternalType.DeclaringType;
			if (declaringType != null && declaringType.IsGenericType)
			{
				throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Cannot resolve method {0} because the declaring type of the method handle {1} is generic. Explicitly provide the declaring type to GetMethodFromHandle."), methodFromHandleInternalType, declaringType.GetGenericTypeDefinition()));
			}
			return methodFromHandleInternalType;
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MethodBase" /> object for the constructor or method represented by the specified handle, for the specified generic type.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBase" /> object representing the method or constructor specified by <paramref name="handle" />, in the generic type specified by <paramref name="declaringType" />.</returns>
		/// <param name="handle">A handle to the internal metadata representation of a constructor or method.</param>
		/// <param name="declaringType">A handle to the generic type that defines the constructor or method.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="handle" /> is invalid.</exception>
		// Token: 0x0600208F RID: 8335 RVA: 0x0007E840 File Offset: 0x0007CA40
		[ComVisible(false)]
		public static MethodBase GetMethodFromHandle(RuntimeMethodHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(Environment.GetResourceString("The handle is invalid."));
			}
			MethodBase methodFromHandleInternalType = MethodBase.GetMethodFromHandleInternalType(handle.Value, declaringType.Value);
			if (methodFromHandleInternalType == null)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			return methodFromHandleInternalType;
		}

		/// <summary>Returns a MethodBase object representing the currently executing method.</summary>
		/// <returns>A MethodBase object representing the currently executing method.</returns>
		/// <exception cref="T:System.Reflection.TargetException">This member was invoked with a late-binding mechanism.</exception>
		// Token: 0x06002090 RID: 8336
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern MethodBase GetCurrentMethod();

		/// <summary>Indicates whether two <see cref="T:System.Reflection.MethodBase" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002092 RID: 8338 RVA: 0x0007E890 File Offset: 0x0007CA90
		public static bool operator ==(MethodBase left, MethodBase right)
		{
			if (left == right)
			{
				return true;
			}
			if (left == null || right == null)
			{
				return false;
			}
			MethodInfo methodInfo;
			MethodInfo methodInfo2;
			if ((methodInfo = left as MethodInfo) != null && (methodInfo2 = right as MethodInfo) != null)
			{
				return methodInfo == methodInfo2;
			}
			ConstructorInfo constructorInfo;
			ConstructorInfo constructorInfo2;
			return (constructorInfo = left as ConstructorInfo) != null && (constructorInfo2 = right as ConstructorInfo) != null && constructorInfo == constructorInfo2;
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.MethodBase" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002093 RID: 8339 RVA: 0x0007E8FC File Offset: 0x0007CAFC
		public static bool operator !=(MethodBase left, MethodBase right)
		{
			return !(left == right);
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06002094 RID: 8340 RVA: 0x0007E908 File Offset: 0x0007CB08
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002095 RID: 8341 RVA: 0x0007E911 File Offset: 0x0007CB11
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06002096 RID: 8342 RVA: 0x0007E91C File Offset: 0x0007CB1C
		[SecurityCritical]
		private IntPtr GetMethodDesc()
		{
			return this.MethodHandle.Value;
		}

		// Token: 0x06002097 RID: 8343 RVA: 0x0007E937 File Offset: 0x0007CB37
		internal virtual ParameterInfo[] GetParametersNoCopy()
		{
			return this.GetParameters();
		}

		/// <summary>When overridden in a derived class, gets the parameters of the specified method or constructor.</summary>
		/// <returns>An array of type ParameterInfo containing information that matches the signature of the method (or constructor) reflected by this MethodBase instance.</returns>
		// Token: 0x06002098 RID: 8344
		public abstract ParameterInfo[] GetParameters();

		/// <summary>Gets the <see cref="T:System.Reflection.MethodImplAttributes" /> flags that specify the attributes of a method implementation.</summary>
		/// <returns>The method implementation flags.</returns>
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x06002099 RID: 8345 RVA: 0x0007E93F File Offset: 0x0007CB3F
		public virtual MethodImplAttributes MethodImplementationFlags
		{
			get
			{
				return this.GetMethodImplementationFlags();
			}
		}

		/// <summary>When overridden in a derived class, returns the <see cref="T:System.Reflection.MethodImplAttributes" /> flags.</summary>
		/// <returns>The MethodImplAttributes flags.</returns>
		// Token: 0x0600209A RID: 8346
		public abstract MethodImplAttributes GetMethodImplementationFlags();

		/// <summary>Gets a handle to the internal metadata representation of a method.</summary>
		/// <returns>A <see cref="T:System.RuntimeMethodHandle" /> object.</returns>
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600209B RID: 8347
		public abstract RuntimeMethodHandle MethodHandle { get; }

		/// <summary>Gets the attributes associated with this method.</summary>
		/// <returns>One of the <see cref="T:System.Reflection.MethodAttributes" /> values.</returns>
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600209C RID: 8348
		public abstract MethodAttributes Attributes { get; }

		/// <summary>When overridden in a derived class, invokes the reflected method or constructor with the given parameters.</summary>
		/// <returns>An Object containing the return value of the invoked method, or null in the case of a constructor, or null if the method's return type is void. Before calling the method or constructor, Invoke checks to see if the user has access permission and verifies that the parameters are valid.CautionElements of the <paramref name="parameters" /> array that represent parameters declared with the ref or out keyword may also be modified.</returns>
		/// <param name="obj">The object on which to invoke the method or constructor. If a method is static, this argument is ignored. If a constructor is static, this argument must be null or an instance of the class that defines the constructor.</param>
		/// <param name="invokeAttr">A bitmask that is a combination of 0 or more bit flags from <see cref="T:System.Reflection.BindingFlags" />. If <paramref name="binder" /> is null, this parameter is assigned the value <see cref="F:System.Reflection.BindingFlags.Default" />; thus, whatever you pass in is ignored. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects via reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="parameters">An argument list for the invoked method or constructor. This is an array of objects with the same number, order, and type as the parameters of the method or constructor to be invoked. If there are no parameters, this should be null.If the method or constructor represented by this instance takes a ByRef parameter, there is no special attribute required for that parameter in order to invoke the method or constructor using this function. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or false, depending on the specific element type. </param>
		/// <param name="culture">An instance of CultureInfo used to govern the coercion of types. If this is null, the CultureInfo for the current thread is used. (This is necessary to convert a String that represents 1000 to a Double value, for example, since 1000 is represented differently by different cultures.) </param>
		/// <exception cref="T:System.Reflection.TargetException">The <paramref name="obj" /> parameter is null and the method is not static.-or- The method is not declared or inherited by the class of <paramref name="obj" />. -or-A static constructor is invoked, and <paramref name="obj" /> is neither null nor an instance of the class that declared the constructor.</exception>
		/// <exception cref="T:System.ArgumentException">The type of the <paramref name="parameters" /> parameter does not match the signature of the method or constructor reflected by this instance. </exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The <paramref name="parameters" /> array does not have the correct number of arguments. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked method or constructor throws an exception. </exception>
		/// <exception cref="T:System.MethodAccessException">The caller does not have permission to execute the method or constructor that is represented by the current instance. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type that declares the method is an open generic type. That is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns true for the declaring type.</exception>
		// Token: 0x0600209D RID: 8349
		public abstract object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		/// <summary>Gets a value indicating the calling conventions for this method.</summary>
		/// <returns>The <see cref="T:System.Reflection.CallingConventions" /> for this method.</returns>
		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600209E RID: 8350 RVA: 0x00003B29 File Offset: 0x00001D29
		public virtual CallingConventions CallingConvention
		{
			get
			{
				return CallingConventions.Standard;
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic method or the type parameters of a generic method definition.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that represent the type arguments of a generic method or the type parameters of a generic method definition. Returns an empty array if the current method is not a generic method.</returns>
		/// <exception cref="T:System.NotSupportedException">The current object is a <see cref="T:System.Reflection.ConstructorInfo" />. Generic constructors are not supported in the .NET Framework version 2.0. This exception is the default behavior if this method is not overridden in a derived class.</exception>
		// Token: 0x0600209F RID: 8351 RVA: 0x000571DB File Offset: 0x000553DB
		[ComVisible(true)]
		public virtual Type[] GetGenericArguments()
		{
			throw new NotSupportedException(Environment.GetResourceString("Derived classes must provide an implementation."));
		}

		/// <summary>Gets a value indicating whether the method is a generic method definition.</summary>
		/// <returns>true if the current <see cref="T:System.Reflection.MethodBase" /> object represents the definition of a generic method; otherwise, false.</returns>
		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsGenericMethodDefinition
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the generic method contains unassigned generic type parameters.</summary>
		/// <returns>true if the current <see cref="T:System.Reflection.MethodBase" /> object represents a generic method that contains unassigned generic type parameters; otherwise, false.</returns>
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060020A1 RID: 8353 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool ContainsGenericParameters
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the method is generic.</summary>
		/// <returns>true if the current <see cref="T:System.Reflection.MethodBase" /> represents a generic method; otherwise, false.</returns>
		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060020A2 RID: 8354 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsGenericMethod
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value that indicates whether the current method or constructor is security-critical or security-safe-critical at the current trust level, and therefore can perform critical operations. </summary>
		/// <returns>true if the current method or constructor is security-critical or security-safe-critical at the current trust level; false if it is transparent. </returns>
		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual bool IsSecurityCritical
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the current method or constructor is security-safe-critical at the current trust level; that is, whether it can perform critical operations and can be accessed by transparent code. </summary>
		/// <returns>true if the method or constructor is security-safe-critical at the current trust level; false if it is security-critical or transparent.</returns>
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060020A4 RID: 8356 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the current method or constructor is transparent at the current trust level, and therefore cannot perform critical operations.</summary>
		/// <returns>true if the method or constructor is security-transparent at the current trust level; otherwise, false.</returns>
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060020A5 RID: 8357 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual bool IsSecurityTransparent
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Invokes the method or constructor represented by the current instance, using the specified parameters.</summary>
		/// <returns>An object containing the return value of the invoked method, or null in the case of a constructor.CautionElements of the <paramref name="parameters" /> array that represent parameters declared with the ref or out keyword may also be modified.</returns>
		/// <param name="obj">The object on which to invoke the method or constructor. If a method is static, this argument is ignored. If a constructor is static, this argument must be null or an instance of the class that defines the constructor. </param>
		/// <param name="parameters">An argument list for the invoked method or constructor. This is an array of objects with the same number, order, and type as the parameters of the method or constructor to be invoked. If there are no parameters, <paramref name="parameters" /> should be null.If the method or constructor represented by this instance takes a ref parameter (ByRef in Visual Basic), no special attribute is required for that parameter in order to invoke the method or constructor using this function. Any object in this array that is not explicitly initialized with a value will contain the default value for that object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or false, depending on the specific element type. </param>
		/// <exception cref="T:System.Reflection.TargetException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.The <paramref name="obj" /> parameter is null and the method is not static.-or- The method is not declared or inherited by the class of <paramref name="obj" />. -or-A static constructor is invoked, and <paramref name="obj" /> is neither null nor an instance of the class that declared the constructor.</exception>
		/// <exception cref="T:System.ArgumentException">The elements of the <paramref name="parameters" />array do not match the signature of the method or constructor reflected by this instance. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The invoked method or constructor throws an exception. -or-The current instance is a <see cref="T:System.Reflection.Emit.DynamicMethod" /> that contains unverifiable code. See the "Verification" section in Remarks for <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The <paramref name="parameters" /> array does not have the correct number of arguments. </exception>
		/// <exception cref="T:System.MethodAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.The caller does not have permission to execute the method or constructor that is represented by the current instance. </exception>
		/// <exception cref="T:System.InvalidOperationException">The type that declares the method is an open generic type. That is, the <see cref="P:System.Type.ContainsGenericParameters" /> property returns true for the declaring type.</exception>
		/// <exception cref="T:System.NotSupportedException">The current instance is a <see cref="T:System.Reflection.Emit.MethodBuilder" />.</exception>
		// Token: 0x060020A6 RID: 8358 RVA: 0x0007E947 File Offset: 0x0007CB47
		[DebuggerStepThrough]
		[DebuggerHidden]
		public object Invoke(object obj, object[] parameters)
		{
			return this.Invoke(obj, BindingFlags.Default, null, parameters, null);
		}

		/// <summary>Gets a value indicating whether this is a public method.</summary>
		/// <returns>true if this method is public; otherwise, false.</returns>
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x0007E954 File Offset: 0x0007CB54
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Public;
			}
		}

		/// <summary>Gets a value indicating whether this member is private.</summary>
		/// <returns>true if access to this method is restricted to other members of the class itself; otherwise, false.</returns>
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060020A8 RID: 8360 RVA: 0x0007E961 File Offset: 0x0007CB61
		public bool IsPrivate
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Private;
			}
		}

		/// <summary>Gets a value indicating whether the visibility of this method or constructor is described by <see cref="F:System.Reflection.MethodAttributes.Family" />; that is, the method or constructor is visible only within its class and derived classes.</summary>
		/// <returns>true if access to this method or constructor is exactly described by <see cref="F:System.Reflection.MethodAttributes.Family" />; otherwise, false.</returns>
		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x060020A9 RID: 8361 RVA: 0x0007E96E File Offset: 0x0007CB6E
		public bool IsFamily
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Family;
			}
		}

		/// <summary>Gets a value indicating whether the potential visibility of this method or constructor is described by <see cref="F:System.Reflection.MethodAttributes.Assembly" />; that is, the method or constructor is visible at most to other types in the same assembly, and is not visible to derived types outside the assembly.</summary>
		/// <returns>true if the visibility of this method or constructor is exactly described by <see cref="F:System.Reflection.MethodAttributes.Assembly" />; otherwise, false.</returns>
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x0007E97B File Offset: 0x0007CB7B
		public bool IsAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.Assembly;
			}
		}

		/// <summary>Gets a value indicating whether the visibility of this method or constructor is described by <see cref="F:System.Reflection.MethodAttributes.FamANDAssem" />; that is, the method or constructor can be called by derived classes, but only if they are in the same assembly.</summary>
		/// <returns>true if access to this method or constructor is exactly described by <see cref="F:System.Reflection.MethodAttributes.FamANDAssem" />; otherwise, false.</returns>
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060020AB RID: 8363 RVA: 0x0007E988 File Offset: 0x0007CB88
		public bool IsFamilyAndAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamANDAssem;
			}
		}

		/// <summary>Gets a value indicating whether the potential visibility of this method or constructor is described by <see cref="F:System.Reflection.MethodAttributes.FamORAssem" />; that is, the method or constructor can be called by derived classes wherever they are, and by classes in the same assembly.</summary>
		/// <returns>true if access to this method or constructor is exactly described by <see cref="F:System.Reflection.MethodAttributes.FamORAssem" />; otherwise, false.</returns>
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060020AC RID: 8364 RVA: 0x0007E995 File Offset: 0x0007CB95
		public bool IsFamilyOrAssembly
		{
			get
			{
				return (this.Attributes & MethodAttributes.MemberAccessMask) == MethodAttributes.FamORAssem;
			}
		}

		/// <summary>Gets a value indicating whether the method is static.</summary>
		/// <returns>true if this method is static; otherwise, false.</returns>
		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060020AD RID: 8365 RVA: 0x0007E9A2 File Offset: 0x0007CBA2
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & MethodAttributes.Static) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether this method is final.</summary>
		/// <returns>true if this method is final; otherwise, false.</returns>
		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060020AE RID: 8366 RVA: 0x0007E9B0 File Offset: 0x0007CBB0
		public bool IsFinal
		{
			get
			{
				return (this.Attributes & MethodAttributes.Final) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the method is virtual.</summary>
		/// <returns>true if this method is virtual; otherwise, false.</returns>
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060020AF RID: 8367 RVA: 0x0007E9BE File Offset: 0x0007CBBE
		public bool IsVirtual
		{
			get
			{
				return (this.Attributes & MethodAttributes.Virtual) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether only a member of the same kind with exactly the same signature is hidden in the derived class.</summary>
		/// <returns>true if the member is hidden by signature; otherwise, false.</returns>
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x0007E9CC File Offset: 0x0007CBCC
		public bool IsHideBySig
		{
			get
			{
				return (this.Attributes & MethodAttributes.HideBySig) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the method is abstract.</summary>
		/// <returns>true if the method is abstract; otherwise, false.</returns>
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060020B1 RID: 8369 RVA: 0x0007E9DD File Offset: 0x0007CBDD
		public bool IsAbstract
		{
			get
			{
				return (this.Attributes & MethodAttributes.Abstract) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether this method has a special name.</summary>
		/// <returns>true if this method has a special name; otherwise, false.</returns>
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060020B2 RID: 8370 RVA: 0x0007E9EE File Offset: 0x0007CBEE
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & MethodAttributes.SpecialName) > MethodAttributes.PrivateScope;
			}
		}

		/// <summary>Gets a value indicating whether the method is a constructor.</summary>
		/// <returns>true if this method is a constructor represented by a <see cref="T:System.Reflection.ConstructorInfo" /> object (see note in Remarks about <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> objects); otherwise, false.</returns>
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x0007E9FF File Offset: 0x0007CBFF
		[ComVisible(true)]
		public bool IsConstructor
		{
			get
			{
				return this is ConstructorInfo && !this.IsStatic && (this.Attributes & MethodAttributes.RTSpecialName) == MethodAttributes.RTSpecialName;
			}
		}

		/// <summary>When overridden in a derived class, gets a <see cref="T:System.Reflection.MethodBody" /> object that provides access to the MSIL stream, local variables, and exceptions for the current method.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodBody" /> object that provides access to the MSIL stream, local variables, and exceptions for the current method.</returns>
		/// <exception cref="T:System.InvalidOperationException">This method is invalid unless overridden in a derived class.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060020B4 RID: 8372 RVA: 0x0007EA26 File Offset: 0x0007CC26
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Demand, Flags = ReflectionPermissionFlag.MemberAccess)]
		public virtual MethodBody GetMethodBody()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x0007EA30 File Offset: 0x0007CC30
		internal static string ConstructParameters(Type[] parameterTypes, CallingConventions callingConvention, bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = "";
			foreach (Type type in parameterTypes)
			{
				stringBuilder.Append(text);
				string text2 = type.FormatTypeName(serialization);
				if (type.IsByRef && !serialization)
				{
					stringBuilder.Append(text2.TrimEnd(new char[] { '&' }));
					stringBuilder.Append(" ByRef");
				}
				else
				{
					stringBuilder.Append(text2);
				}
				text = ", ";
			}
			if ((callingConvention & CallingConventions.VarArgs) == CallingConventions.VarArgs)
			{
				stringBuilder.Append(text);
				stringBuilder.Append("...");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x0007EAC8 File Offset: 0x0007CCC8
		internal string FullName
		{
			get
			{
				return string.Format("{0}.{1}", this.DeclaringType.FullName, this.FormatNameAndSig());
			}
		}

		// Token: 0x060020B7 RID: 8375 RVA: 0x0007EAE5 File Offset: 0x0007CCE5
		internal string FormatNameAndSig()
		{
			return this.FormatNameAndSig(false);
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x0007EAF0 File Offset: 0x0007CCF0
		internal virtual string FormatNameAndSig(bool serialization)
		{
			StringBuilder stringBuilder = new StringBuilder(this.Name);
			stringBuilder.Append("(");
			stringBuilder.Append(MethodBase.ConstructParameters(this.GetParameterTypes(), this.CallingConvention, serialization));
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x060020B9 RID: 8377 RVA: 0x0007EB40 File Offset: 0x0007CD40
		internal virtual Type[] GetParameterTypes()
		{
			ParameterInfo[] parametersNoCopy = this.GetParametersNoCopy();
			Type[] array = new Type[parametersNoCopy.Length];
			for (int i = 0; i < parametersNoCopy.Length; i++)
			{
				array[i] = parametersNoCopy[i].ParameterType;
			}
			return array;
		}

		/// <summary>For a description of this member, see <see cref="M:System.Runtime.InteropServices._MethodBase.GetType" />.</summary>
		/// <returns>For a description of this member, see <see cref="M:System.Runtime.InteropServices._MethodBase.GetType" />.</returns>
		// Token: 0x060020BA RID: 8378 RVA: 0x00033A19 File Offset: 0x00031C19
		Type _MethodBase.GetType()
		{
			return base.GetType();
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsPublic" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsPublic" />.</returns>
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x0007EB77 File Offset: 0x0007CD77
		bool _MethodBase.IsPublic
		{
			get
			{
				return this.IsPublic;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsPrivate" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsPrivate" />.</returns>
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060020BC RID: 8380 RVA: 0x0007EB7F File Offset: 0x0007CD7F
		bool _MethodBase.IsPrivate
		{
			get
			{
				return this.IsPrivate;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFamily" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFamily" />.</returns>
		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x0007EB87 File Offset: 0x0007CD87
		bool _MethodBase.IsFamily
		{
			get
			{
				return this.IsFamily;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsAssembly" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsAssembly" />.</returns>
		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060020BE RID: 8382 RVA: 0x0007EB8F File Offset: 0x0007CD8F
		bool _MethodBase.IsAssembly
		{
			get
			{
				return this.IsAssembly;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFamilyAndAssembly" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFamilyAndAssembly" />.</returns>
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x0007EB97 File Offset: 0x0007CD97
		bool _MethodBase.IsFamilyAndAssembly
		{
			get
			{
				return this.IsFamilyAndAssembly;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFamilyOrAssembly" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFamilyOrAssembly" />.</returns>
		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060020C0 RID: 8384 RVA: 0x0007EB9F File Offset: 0x0007CD9F
		bool _MethodBase.IsFamilyOrAssembly
		{
			get
			{
				return this.IsFamilyOrAssembly;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsStatic" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsStatic" />.</returns>
		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x0007EBA7 File Offset: 0x0007CDA7
		bool _MethodBase.IsStatic
		{
			get
			{
				return this.IsStatic;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFinal" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsFinal" />.</returns>
		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x0007EBAF File Offset: 0x0007CDAF
		bool _MethodBase.IsFinal
		{
			get
			{
				return this.IsFinal;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsVirtual" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsVirtual" />.</returns>
		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x0007EBB7 File Offset: 0x0007CDB7
		bool _MethodBase.IsVirtual
		{
			get
			{
				return this.IsVirtual;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsHideBySig" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsHideBySig" />.</returns>
		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060020C4 RID: 8388 RVA: 0x0007EBBF File Offset: 0x0007CDBF
		bool _MethodBase.IsHideBySig
		{
			get
			{
				return this.IsHideBySig;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsAbstract" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsAbstract" />.</returns>
		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x0007EBC7 File Offset: 0x0007CDC7
		bool _MethodBase.IsAbstract
		{
			get
			{
				return this.IsAbstract;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsSpecialName" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsSpecialName" />.</returns>
		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060020C6 RID: 8390 RVA: 0x0007EBCF File Offset: 0x0007CDCF
		bool _MethodBase.IsSpecialName
		{
			get
			{
				return this.IsSpecialName;
			}
		}

		/// <summary>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsConstructor" />.</summary>
		/// <returns>For a description of this member, see <see cref="P:System.Runtime.InteropServices._MethodBase.IsConstructor" />.</returns>
		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x0007EBD7 File Offset: 0x0007CDD7
		bool _MethodBase.IsConstructor
		{
			get
			{
				return this.IsConstructor;
			}
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060020C8 RID: 8392 RVA: 0x0002126B File Offset: 0x0001F46B
		void _MethodBase.GetTypeInfoCount(out uint pcTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x060020C9 RID: 8393 RVA: 0x0002126B File Offset: 0x0001F46B
		void _MethodBase.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
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
		// Token: 0x060020CA RID: 8394 RVA: 0x0002126B File Offset: 0x0001F46B
		void _MethodBase.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
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
		// Token: 0x060020CB RID: 8395 RVA: 0x0002126B File Offset: 0x0001F46B
		void _MethodBase.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0007E937 File Offset: 0x0007CB37
		internal virtual ParameterInfo[] GetParametersInternal()
		{
			return this.GetParameters();
		}

		// Token: 0x060020CD RID: 8397 RVA: 0x0007EBDF File Offset: 0x0007CDDF
		internal virtual int GetParametersCount()
		{
			return this.GetParametersInternal().Length;
		}

		// Token: 0x060020CE RID: 8398 RVA: 0x0002126B File Offset: 0x0001F46B
		internal virtual Type GetParameterType(int pos)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060020CF RID: 8399 RVA: 0x0007EBE9 File Offset: 0x0007CDE9
		internal virtual int get_next_table_index(object obj, int table, bool inc)
		{
			if (this is MethodBuilder)
			{
				return ((MethodBuilder)this).get_next_table_index(obj, table, inc);
			}
			if (this is ConstructorBuilder)
			{
				return ((ConstructorBuilder)this).get_next_table_index(obj, table, inc);
			}
			throw new Exception("Method is not a builder method");
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x0007EC23 File Offset: 0x0007CE23
		internal static MethodBase GetMethodFromHandleNoGenericCheck(RuntimeMethodHandle handle)
		{
			return MethodBase.GetMethodFromHandleInternalType_native(handle.Value, IntPtr.Zero, false);
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x0007EC37 File Offset: 0x0007CE37
		internal static MethodBase GetMethodFromHandleNoGenericCheck(RuntimeMethodHandle handle, RuntimeTypeHandle reflectedType)
		{
			return MethodBase.GetMethodFromHandleInternalType_native(handle.Value, reflectedType.Value, false);
		}

		// Token: 0x060020D2 RID: 8402
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodBody GetMethodBodyInternal(IntPtr handle);

		// Token: 0x060020D3 RID: 8403 RVA: 0x0007EC4D File Offset: 0x0007CE4D
		internal static MethodBody GetMethodBody(IntPtr handle)
		{
			return MethodBase.GetMethodBodyInternal(handle);
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x0007EC55 File Offset: 0x0007CE55
		private static MethodBase GetMethodFromHandleInternalType(IntPtr method_handle, IntPtr type_handle)
		{
			return MethodBase.GetMethodFromHandleInternalType_native(method_handle, type_handle, true);
		}

		// Token: 0x060020D5 RID: 8405
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern MethodBase GetMethodFromHandleInternalType_native(IntPtr method_handle, IntPtr type_handle, bool genericCheck);
	}
}
