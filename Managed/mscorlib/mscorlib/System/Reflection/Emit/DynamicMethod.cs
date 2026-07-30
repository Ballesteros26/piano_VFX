using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a dynamic method that can be compiled, executed, and discarded. Discarded methods are available for garbage collection.</summary>
	// Token: 0x02000352 RID: 850
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class DynamicMethod : MethodInfo
	{
		/// <summary>Creates a dynamic method that is global to a module, specifying the method name, return type, parameter types, and module.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be null. </param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or null if the method has no return type. </param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or null if the method has no parameters. </param>
		/// <param name="m">A <see cref="T:System.Reflection.Module" /> representing the module with which the dynamic method is to be logically associated. </param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is null or <see cref="T:System.Void" />. -or-<paramref name="m" /> is a module that provides anonymous hosting for dynamic methods.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. -or-<paramref name="m" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="returnType" /> is a type for which <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		// Token: 0x060025CB RID: 9675 RVA: 0x00087E93 File Offset: 0x00086093
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Module m)
			: this(name, returnType, parameterTypes, m, false)
		{
		}

		/// <summary>Creates a dynamic method, specifying the method name, return type, parameter types, and the type with which the dynamic method is logically associated.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be null. </param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or null if the method has no return type. </param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or null if the method has no parameters. </param>
		/// <param name="owner">A <see cref="T:System.Type" /> with which the dynamic method is logically associated. The dynamic method has access to all members of the type. </param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is null or <see cref="T:System.Void" />.-or- <paramref name="owner" /> is an interface, an array, an open generic type, or a type parameter of a generic type or method. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. -or-<paramref name="owner" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="returnType" /> is null, or is a type for which <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		// Token: 0x060025CC RID: 9676 RVA: 0x00087EA1 File Offset: 0x000860A1
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner)
			: this(name, returnType, parameterTypes, owner, false)
		{
		}

		/// <summary>Creates a dynamic method that is global to a module, specifying the method name, return type, parameter types, module, and whether just-in-time (JIT) visibility checks should be skipped for types and members accessed by the Microsoft intermediate language (MSIL) of the dynamic method.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be null. </param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or null if the method has no return type. </param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or null if the method has no parameters. </param>
		/// <param name="m">A <see cref="T:System.Reflection.Module" /> representing the module with which the dynamic method is to be logically associated. </param>
		/// <param name="skipVisibility">true to skip JIT visibility checks on types and members accessed by the MSIL of the dynamic method. </param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is null or <see cref="T:System.Void" />. -or-<paramref name="m" /> is a module that provides anonymous hosting for dynamic methods.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. -or-<paramref name="m" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="returnType" /> is a type for which <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		// Token: 0x060025CD RID: 9677 RVA: 0x00087EAF File Offset: 0x000860AF
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Module m, bool skipVisibility)
			: this(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, m, skipVisibility)
		{
		}

		/// <summary>Creates a dynamic method, specifying the method name, return type, parameter types, the type with which the dynamic method is logically associated, and whether just-in-time (JIT) visibility checks should be skipped for types and members accessed by the Microsoft intermediate language (MSIL) of the dynamic method.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be null. </param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or null if the method has no return type. </param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or null if the method has no parameters. </param>
		/// <param name="owner">A <see cref="T:System.Type" /> with which the dynamic method is logically associated. The dynamic method has access to all members of the type.</param>
		/// <param name="skipVisibility">true to skip JIT visibility checks on types and members accessed by the MSIL of the dynamic method; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is null or <see cref="T:System.Void" />.-or- <paramref name="owner" /> is an interface, an array, an open generic type, or a type parameter of a generic type or method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. -or-<paramref name="owner" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="returnType" /> is null, or is a type for which <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		// Token: 0x060025CE RID: 9678 RVA: 0x00087EC1 File Offset: 0x000860C1
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility)
			: this(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, owner, skipVisibility)
		{
		}

		/// <summary>Creates a dynamic method, specifying the method name, attributes, calling convention, return type, parameter types, the type with which the dynamic method is logically associated, and whether just-in-time (JIT) visibility checks should be skipped for types and members accessed by the Microsoft intermediate language (MSIL) of the dynamic method.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be null.</param>
		/// <param name="attributes">A bitwise combination of <see cref="T:System.Reflection.MethodAttributes" /> values that specifies the attributes of the dynamic method. The only combination allowed is <see cref="F:System.Reflection.MethodAttributes.Public" /> and <see cref="F:System.Reflection.MethodAttributes.Static" />.</param>
		/// <param name="callingConvention">The calling convention for the dynamic method. Must be <see cref="F:System.Reflection.CallingConventions.Standard" />.</param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or null if the method has no return type. </param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or null if the method has no parameters. </param>
		/// <param name="owner">A <see cref="T:System.Type" /> with which the dynamic method is logically associated. The dynamic method has access to all members of the type.</param>
		/// <param name="skipVisibility">true to skip JIT visibility checks on types and members accessed by the MSIL of the dynamic method; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is null or <see cref="T:System.Void" />. -or-<paramref name="owner" /> is an interface, an array, an open generic type, or a type parameter of a generic type or method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. -or-<paramref name="owner" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="attributes" /> is a combination of flags other than <see cref="F:System.Reflection.MethodAttributes.Public" /> and <see cref="F:System.Reflection.MethodAttributes.Static" />.-or-<paramref name="callingConvention" /> is not <see cref="F:System.Reflection.CallingConventions.Standard" />.-or-<paramref name="returnType" /> is a type for which <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		// Token: 0x060025CF RID: 9679 RVA: 0x00087ED4 File Offset: 0x000860D4
		public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type owner, bool skipVisibility)
			: this(name, attributes, callingConvention, returnType, parameterTypes, owner, owner.Module, skipVisibility, false)
		{
		}

		/// <summary>Creates a dynamic method that is global to a module, specifying the method name, attributes, calling convention, return type, parameter types, module, and whether just-in-time (JIT) visibility checks should be skipped for types and members accessed by the Microsoft intermediate language (MSIL) of the dynamic method.</summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be null.</param>
		/// <param name="attributes">A bitwise combination of <see cref="T:System.Reflection.MethodAttributes" /> values that specifies the attributes of the dynamic method. The only combination allowed is <see cref="F:System.Reflection.MethodAttributes.Public" /> and <see cref="F:System.Reflection.MethodAttributes.Static" />.</param>
		/// <param name="callingConvention">The calling convention for the dynamic method. Must be <see cref="F:System.Reflection.CallingConventions.Standard" />.</param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or null if the method has no return type. </param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or null if the method has no parameters. </param>
		/// <param name="m">A <see cref="T:System.Reflection.Module" /> representing the module with which the dynamic method is to be logically associated. </param>
		/// <param name="skipVisibility">true to skip JIT visibility checks on types and members accessed by the MSIL of the dynamic method; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is null or <see cref="T:System.Void" />.-or-<paramref name="m" /> is a module that provides anonymous hosting for dynamic methods.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. -or-<paramref name="m" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="attributes" /> is a combination of flags other than <see cref="F:System.Reflection.MethodAttributes.Public" /> and <see cref="F:System.Reflection.MethodAttributes.Static" />.-or-<paramref name="callingConvention" /> is not <see cref="F:System.Reflection.CallingConventions.Standard" />.-or-<paramref name="returnType" /> is a type for which <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		// Token: 0x060025D0 RID: 9680 RVA: 0x00087EFC File Offset: 0x000860FC
		public DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Module m, bool skipVisibility)
			: this(name, attributes, callingConvention, returnType, parameterTypes, null, m, skipVisibility, false)
		{
		}

		/// <summary>Initializes an anonymously hosted dynamic method, specifying the method name, return type, and parameter types. </summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be null. </param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or null if the method has no return type. </param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or null if the method has no parameters. </param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is null or <see cref="T:System.Void" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="returnType" /> is a type for which <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		// Token: 0x060025D1 RID: 9681 RVA: 0x00087F1C File Offset: 0x0008611C
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes)
			: this(name, returnType, parameterTypes, false)
		{
		}

		/// <summary>Initializes an anonymously hosted dynamic method, specifying the method name, return type, parameter types, and whether just-in-time (JIT) visibility checks should be skipped for types and members accessed by the Microsoft intermediate language (MSIL) of the dynamic method. </summary>
		/// <param name="name">The name of the dynamic method. This can be a zero-length string, but it cannot be null. </param>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that specifies the return type of the dynamic method, or null if the method has no return type. </param>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects specifying the types of the parameters of the dynamic method, or null if the method has no parameters. </param>
		/// <param name="restrictedSkipVisibility">true to skip JIT visibility checks on types and members accessed by the MSIL of the dynamic method, with this restriction: the trust level of the assemblies that contain those types and members must be equal to or less than the trust level of the call stack that emits the dynamic method; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentException">An element of <paramref name="parameterTypes" /> is null or <see cref="T:System.Void" />.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="returnType" /> is a type for which <see cref="P:System.Type.IsByRef" /> returns true. </exception>
		// Token: 0x060025D2 RID: 9682 RVA: 0x00087F28 File Offset: 0x00086128
		[MonoTODO("Visibility is not restricted")]
		public DynamicMethod(string name, Type returnType, Type[] parameterTypes, bool restrictedSkipVisibility)
			: this(name, MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.Static, CallingConventions.Standard, returnType, parameterTypes, null, null, restrictedSkipVisibility, true)
		{
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x00087F48 File Offset: 0x00086148
		private DynamicMethod(string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type owner, Module m, bool skipVisibility, bool anonHosted)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (returnType == null)
			{
				returnType = typeof(void);
			}
			if (m == null && !anonHosted)
			{
				throw new ArgumentNullException("m");
			}
			if (returnType.IsByRef)
			{
				throw new ArgumentException("Return type can't be a byref type", "returnType");
			}
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentException("Parameter " + i + " is null", "parameterTypes");
					}
				}
			}
			if (owner != null && (owner.IsArray || owner.IsInterface))
			{
				throw new ArgumentException("Owner can't be an array or an interface.");
			}
			if (m == null)
			{
				m = DynamicMethod.AnonHostModuleHolder.AnonHostModule;
			}
			this.name = name;
			this.attributes = attributes | MethodAttributes.Static;
			this.callingConvention = callingConvention;
			this.returnType = returnType;
			this.parameters = parameterTypes;
			this.owner = owner;
			this.module = m;
			this.skipVisibility = skipVisibility;
		}

		// Token: 0x060025D4 RID: 9684
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void create_dynamic_method(DynamicMethod m);

		// Token: 0x060025D5 RID: 9685 RVA: 0x00088070 File Offset: 0x00086270
		private void CreateDynMethod()
		{
			if (this.mhandle.Value == IntPtr.Zero)
			{
				if (this.ilgen == null || this.ilgen.ILOffset == 0)
				{
					throw new InvalidOperationException("Method '" + this.name + "' does not have a method body.");
				}
				this.ilgen.label_fixup(this);
				try
				{
					this.creating = true;
					if (this.refs != null)
					{
						for (int i = 0; i < this.refs.Length; i++)
						{
							if (this.refs[i] is DynamicMethod)
							{
								DynamicMethod dynamicMethod = (DynamicMethod)this.refs[i];
								if (!dynamicMethod.creating)
								{
									dynamicMethod.CreateDynMethod();
								}
							}
						}
					}
				}
				finally
				{
					this.creating = false;
				}
				DynamicMethod.create_dynamic_method(this);
			}
		}

		/// <summary>Completes the dynamic method and creates a delegate that can be used to execute it.</summary>
		/// <returns>A delegate of the specified type, which can be used to execute the dynamic method.</returns>
		/// <param name="delegateType">A delegate type whose signature matches that of the dynamic method. </param>
		/// <exception cref="T:System.InvalidOperationException">The dynamic method has no method body.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="delegateType" /> has the wrong number of parameters or the wrong parameter types.</exception>
		// Token: 0x060025D6 RID: 9686 RVA: 0x00088140 File Offset: 0x00086340
		[ComVisible(true)]
		public sealed override Delegate CreateDelegate(Type delegateType)
		{
			if (delegateType == null)
			{
				throw new ArgumentNullException("delegateType");
			}
			if (this.deleg != null)
			{
				return this.deleg;
			}
			this.CreateDynMethod();
			this.deleg = Delegate.CreateDelegate(delegateType, null, this);
			return this.deleg;
		}

		/// <summary>Completes the dynamic method and creates a delegate that can be used to execute it, specifying the delegate type and an object the delegate is bound to.</summary>
		/// <returns>A delegate of the specified type, which can be used to execute the dynamic method with the specified target object.</returns>
		/// <param name="delegateType">A delegate type whose signature matches that of the dynamic method, minus the first parameter.</param>
		/// <param name="target">An object the delegate is bound to. Must be of the same type as the first parameter of the dynamic method. </param>
		/// <exception cref="T:System.InvalidOperationException">The dynamic method has no method body.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="target" /> is not the same type as the first parameter of the dynamic method, and is not assignable to that type.-or-<paramref name="delegateType" /> has the wrong number of parameters or the wrong parameter types.</exception>
		// Token: 0x060025D7 RID: 9687 RVA: 0x0008817F File Offset: 0x0008637F
		[ComVisible(true)]
		public sealed override Delegate CreateDelegate(Type delegateType, object target)
		{
			if (delegateType == null)
			{
				throw new ArgumentNullException("delegateType");
			}
			this.CreateDynMethod();
			return Delegate.CreateDelegate(delegateType, target, this);
		}

		/// <summary>Defines a parameter of the dynamic method.</summary>
		/// <returns>Always returns null. </returns>
		/// <param name="position">The position of the parameter in the parameter list. Parameters are indexed beginning with the number 1 for the first parameter. </param>
		/// <param name="attributes">A bitwise combination of <see cref="T:System.Reflection.ParameterAttributes" /> values that specifies the attributes of the parameter. </param>
		/// <param name="parameterName">The name of the parameter. The name can be a zero-length string. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The method has no parameters.-or- <paramref name="position" /> is less than 0.-or- <paramref name="position" /> is greater than the number of the method's parameters. </exception>
		// Token: 0x060025D8 RID: 9688 RVA: 0x000881A4 File Offset: 0x000863A4
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string parameterName)
		{
			if (position < 0 || position > this.parameters.Length)
			{
				throw new ArgumentOutOfRangeException("position");
			}
			this.RejectIfCreated();
			ParameterBuilder parameterBuilder = new ParameterBuilder(this, position, attributes, parameterName);
			if (this.pinfo == null)
			{
				this.pinfo = new ParameterBuilder[this.parameters.Length + 1];
			}
			this.pinfo[position] = parameterBuilder;
			return parameterBuilder;
		}

		/// <summary>Returns the base implementation for the method.</summary>
		/// <returns>The base implementation of the method.</returns>
		// Token: 0x060025D9 RID: 9689 RVA: 0x00002119 File Offset: 0x00000319
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		/// <summary>Returns all the custom attributes defined for the method.</summary>
		/// <returns>An array of objects representing all the custom attributes of the method.</returns>
		/// <param name="inherit">true to search the method's inheritance chain to find the custom attributes; false to check only the current method. </param>
		// Token: 0x060025DA RID: 9690 RVA: 0x00088202 File Offset: 0x00086402
		public override object[] GetCustomAttributes(bool inherit)
		{
			return new object[]
			{
				new MethodImplAttribute(this.GetMethodImplementationFlags())
			};
		}

		/// <summary>Returns the custom attributes of the specified type that have been applied to the method.</summary>
		/// <returns>An array of objects representing the attributes of the method that are of type <paramref name="attributeType" /> or derive from type <paramref name="attributeType" />.</returns>
		/// <param name="attributeType">A <see cref="T:System.Type" /> representing the type of custom attribute to return. </param>
		/// <param name="inherit">true to search the method's inheritance chain to find the custom attributes; false to check only the current method. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null.</exception>
		// Token: 0x060025DB RID: 9691 RVA: 0x00088218 File Offset: 0x00086418
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (attributeType.IsAssignableFrom(typeof(MethodImplAttribute)))
			{
				return new object[]
				{
					new MethodImplAttribute(this.GetMethodImplementationFlags())
				};
			}
			return EmptyArray<object>.Value;
		}

		/// <summary>Returns a <see cref="T:System.Reflection.Emit.DynamicILInfo" /> object that can be used to generate a method body from metadata tokens, scopes, and Microsoft intermediate language (MSIL) streams.</summary>
		/// <returns>A <see cref="T:System.Reflection.Emit.DynamicILInfo" /> object that can be used to generate a method body from metadata tokens, scopes, and MSIL streams.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060025DC RID: 9692 RVA: 0x00088265 File Offset: 0x00086465
		public DynamicILInfo GetDynamicILInfo()
		{
			if (this.il_info == null)
			{
				this.il_info = new DynamicILInfo(this);
			}
			return this.il_info;
		}

		/// <summary>Returns a Microsoft intermediate language (MSIL) generator for the method with a default MSIL stream size of 64 bytes.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.ILGenerator" /> object for the method.</returns>
		// Token: 0x060025DD RID: 9693 RVA: 0x00088281 File Offset: 0x00086481
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		/// <summary>Returns a Microsoft intermediate language (MSIL) generator for the method with the specified MSIL stream size.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.ILGenerator" /> object for the method, with the specified MSIL stream size.</returns>
		/// <param name="streamSize">The size of the MSIL stream, in bytes. </param>
		// Token: 0x060025DE RID: 9694 RVA: 0x0008828C File Offset: 0x0008648C
		public ILGenerator GetILGenerator(int streamSize)
		{
			if ((this.GetMethodImplementationFlags() & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.GetMethodImplementationFlags() & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL)
			{
				throw new InvalidOperationException("Method body should not exist.");
			}
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.Module, new DynamicMethodTokenGenerator(this), streamSize);
			return this.ilgen;
		}

		/// <summary>Returns the implementation flags for the method.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Reflection.MethodImplAttributes" /> values representing the implementation flags for the method.</returns>
		// Token: 0x060025DF RID: 9695 RVA: 0x00058C99 File Offset: 0x00056E99
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return MethodImplAttributes.NoInlining;
		}

		/// <summary>Returns the parameters of the dynamic method.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.ParameterInfo" /> objects that represent the parameters of the dynamic method.</returns>
		// Token: 0x060025E0 RID: 9696 RVA: 0x0007F7B2 File Offset: 0x0007D9B2
		public override ParameterInfo[] GetParameters()
		{
			return this.GetParametersInternal();
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000882E8 File Offset: 0x000864E8
		internal override ParameterInfo[] GetParametersInternal()
		{
			if (this.parameters == null)
			{
				return EmptyArray<ParameterInfo>.Value;
			}
			ParameterInfo[] array = new ParameterInfo[this.parameters.Length];
			for (int i = 0; i < this.parameters.Length; i++)
			{
				array[i] = ParameterInfo.New((this.pinfo == null) ? null : this.pinfo[i + 1], this.parameters[i], this, i + 1);
			}
			return array;
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x0008834E File Offset: 0x0008654E
		internal override int GetParametersCount()
		{
			if (this.parameters != null)
			{
				return this.parameters.Length;
			}
			return 0;
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x00088362 File Offset: 0x00086562
		internal override Type GetParameterType(int pos)
		{
			return this.parameters[pos];
		}

		/// <summary>Invokes the dynamic method using the specified parameters, under the constraints of the specified binder, with the specified culture information.</summary>
		/// <returns>A <see cref="T:System.Object" /> containing the return value of the invoked method.</returns>
		/// <param name="obj">This parameter is ignored for dynamic methods, because they are static. Specify null. </param>
		/// <param name="invokeAttr">A bitwise combination of <see cref="T:System.Reflection.BindingFlags" /> values.</param>
		/// <param name="binder">A <see cref="T:System.Reflection.Binder" /> object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used. For more details, see <see cref="T:System.Reflection.Binder" />. </param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the method to be invoked. If there are no parameters this parameter should be null. </param>
		/// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. For example, this information is needed to correctly convert a <see cref="T:System.String" /> that represents 1000 to a <see cref="T:System.Double" /> value, because 1000 is represented differently by different cultures. </param>
		/// <exception cref="T:System.NotSupportedException">The <see cref="F:System.Reflection.CallingConventions.VarArgs" /> calling convention is not supported.</exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of elements in <paramref name="parameters" /> does not match the number of parameters in the dynamic method.</exception>
		/// <exception cref="T:System.ArgumentException">The type of one or more elements of <paramref name="parameters" /> does not match the type of the corresponding parameter of the dynamic method.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The dynamic method is associated with a module, is not anonymously hosted, and was constructed with <paramref name="skipVisibility" /> set to false, but the dynamic method accesses members that are not public or internal (Friend in Visual Basic).-or-The dynamic method is anonymously hosted and was constructed with <paramref name="skipVisibility" /> set to false, but it accesses members that are not public.-or-The dynamic method contains unverifiable code. See the "Verification" section in Remarks for <see cref="T:System.Reflection.Emit.DynamicMethod" />.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		/// </PermissionSet>
		// Token: 0x060025E4 RID: 9700 RVA: 0x0008836C File Offset: 0x0008656C
		[SecuritySafeCritical]
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			object obj2;
			try
			{
				this.CreateDynMethod();
				if (this.method == null)
				{
					this.method = new MonoMethod(this.mhandle);
				}
				obj2 = this.method.Invoke(obj, parameters);
			}
			catch (MethodAccessException ex)
			{
				throw new TargetInvocationException("Method cannot be invoked.", ex);
			}
			return obj2;
		}

		/// <summary>Indicates whether the specified custom attribute type is defined.</summary>
		/// <returns>true if the specified custom attribute type is defined; otherwise, false.</returns>
		/// <param name="attributeType">A <see cref="T:System.Type" /> representing the type of custom attribute to search for. </param>
		/// <param name="inherit">true to search the method's inheritance chain to find the custom attributes; false to check only the current method. </param>
		// Token: 0x060025E5 RID: 9701 RVA: 0x000883D0 File Offset: 0x000865D0
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return attributeType.IsAssignableFrom(typeof(MethodImplAttribute));
		}

		/// <summary>Returns the signature of the method, represented as a string.</summary>
		/// <returns>A string representing the method signature.</returns>
		// Token: 0x060025E6 RID: 9702 RVA: 0x000883FC File Offset: 0x000865FC
		public override string ToString()
		{
			string text = string.Empty;
			ParameterInfo[] parametersInternal = this.GetParametersInternal();
			for (int i = 0; i < parametersInternal.Length; i++)
			{
				if (i > 0)
				{
					text += ", ";
				}
				text += parametersInternal[i].ParameterType.Name;
			}
			return string.Concat(new string[]
			{
				this.ReturnType.Name,
				" ",
				this.Name,
				"(",
				text,
				")"
			});
		}

		/// <summary>Gets the attributes specified when the dynamic method was created.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Reflection.MethodAttributes" /> values representing the attributes for the method.</returns>
		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x00088486 File Offset: 0x00086686
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		/// <summary>Gets the calling convention specified when the dynamic method was created.</summary>
		/// <returns>One of the <see cref="T:System.Reflection.CallingConventions" /> values that indicates the calling convention of the method.</returns>
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x060025E8 RID: 9704 RVA: 0x0008848E File Offset: 0x0008668E
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.callingConvention;
			}
		}

		/// <summary>Gets the type that declares the method, which is always null for dynamic methods.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x060025E9 RID: 9705 RVA: 0x0000A42E File Offset: 0x0000862E
		public override Type DeclaringType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets or sets a value indicating whether the local variables in the method are zero-initialized. </summary>
		/// <returns>true if the local variables in the method are zero-initialized; otherwise, false. The default is true.</returns>
		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060025EA RID: 9706 RVA: 0x00088496 File Offset: 0x00086696
		// (set) Token: 0x060025EB RID: 9707 RVA: 0x0008849E File Offset: 0x0008669E
		public bool InitLocals
		{
			get
			{
				return this.init_locals;
			}
			set
			{
				this.init_locals = value;
			}
		}

		/// <summary>Not supported for dynamic methods.</summary>
		/// <returns>Not supported for dynamic methods.</returns>
		/// <exception cref="T:System.InvalidOperationException">Not allowed for dynamic methods.</exception>
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x000884A7 File Offset: 0x000866A7
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				return this.mhandle;
			}
		}

		/// <summary>Gets the module with which the dynamic method is logically associated.</summary>
		/// <returns>The <see cref="T:System.Reflection.Module" /> with which the current dynamic method is associated.</returns>
		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x000884AF File Offset: 0x000866AF
		public override Module Module
		{
			get
			{
				return this.module;
			}
		}

		/// <summary>Gets the name of the dynamic method.</summary>
		/// <returns>The simple name of the method.</returns>
		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x000884B7 File Offset: 0x000866B7
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the class that was used in reflection to obtain the method.</summary>
		/// <returns>Always null.</returns>
		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060025EF RID: 9711 RVA: 0x0000A42E File Offset: 0x0000862E
		public override Type ReflectedType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Gets the return parameter of the dynamic method.</summary>
		/// <returns>Always null. </returns>
		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("Not implemented")]
		public override ParameterInfo ReturnParameter
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the type of return value for the dynamic method.</summary>
		/// <returns>A <see cref="T:System.Type" /> representing the type of the return value of the current method; <see cref="T:System.Void" /> if the method has no return type.</returns>
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x000884BF File Offset: 0x000866BF
		public override Type ReturnType
		{
			get
			{
				return this.returnType;
			}
		}

		/// <summary>Gets the custom attributes of the return type for the dynamic method.</summary>
		/// <returns>An <see cref="T:System.Reflection.ICustomAttributeProvider" /> representing the custom attributes of the return type for the dynamic method.</returns>
		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060025F2 RID: 9714 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("Not implemented")]
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x000884C7 File Offset: 0x000866C7
		private void RejectIfCreated()
		{
			if (this.mhandle.Value != IntPtr.Zero)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x000884EC File Offset: 0x000866EC
		internal int AddRef(object reference)
		{
			if (this.refs == null)
			{
				this.refs = new object[4];
			}
			if (this.nrefs >= this.refs.Length - 1)
			{
				object[] array = new object[this.refs.Length * 2];
				Array.Copy(this.refs, array, this.refs.Length);
				this.refs = array;
			}
			this.refs[this.nrefs] = reference;
			this.refs[this.nrefs + 1] = null;
			this.nrefs += 2;
			return this.nrefs - 1;
		}

		// Token: 0x040013E0 RID: 5088
		private RuntimeMethodHandle mhandle;

		// Token: 0x040013E1 RID: 5089
		private string name;

		// Token: 0x040013E2 RID: 5090
		private Type returnType;

		// Token: 0x040013E3 RID: 5091
		private Type[] parameters;

		// Token: 0x040013E4 RID: 5092
		private MethodAttributes attributes;

		// Token: 0x040013E5 RID: 5093
		private CallingConventions callingConvention;

		// Token: 0x040013E6 RID: 5094
		private Module module;

		// Token: 0x040013E7 RID: 5095
		private bool skipVisibility;

		// Token: 0x040013E8 RID: 5096
		private bool init_locals = true;

		// Token: 0x040013E9 RID: 5097
		private ILGenerator ilgen;

		// Token: 0x040013EA RID: 5098
		private int nrefs;

		// Token: 0x040013EB RID: 5099
		private object[] refs;

		// Token: 0x040013EC RID: 5100
		private IntPtr referenced_by;

		// Token: 0x040013ED RID: 5101
		private Type owner;

		// Token: 0x040013EE RID: 5102
		private Delegate deleg;

		// Token: 0x040013EF RID: 5103
		private MonoMethod method;

		// Token: 0x040013F0 RID: 5104
		private ParameterBuilder[] pinfo;

		// Token: 0x040013F1 RID: 5105
		internal bool creating;

		// Token: 0x040013F2 RID: 5106
		private DynamicILInfo il_info;

		// Token: 0x02000353 RID: 851
		private class AnonHostModuleHolder
		{
			// Token: 0x060025F5 RID: 9717 RVA: 0x00088580 File Offset: 0x00086780
			static AnonHostModuleHolder()
			{
				AssemblyName assemblyName = new AssemblyName();
				assemblyName.Name = "Anonymously Hosted DynamicMethods Assembly";
				DynamicMethod.AnonHostModuleHolder.anon_host_module = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run).GetManifestModule();
			}

			// Token: 0x170005EC RID: 1516
			// (get) Token: 0x060025F6 RID: 9718 RVA: 0x000885B4 File Offset: 0x000867B4
			public static Module AnonHostModule
			{
				get
				{
					return DynamicMethod.AnonHostModuleHolder.anon_host_module;
				}
			}

			// Token: 0x040013F3 RID: 5107
			public static Module anon_host_module;
		}
	}
}
