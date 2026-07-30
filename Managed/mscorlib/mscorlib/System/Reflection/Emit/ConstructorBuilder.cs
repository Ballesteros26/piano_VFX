using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Defines and represents a constructor of a dynamic class.</summary>
	// Token: 0x0200034A RID: 842
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_ConstructorBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ConstructorBuilder : ConstructorInfo, _ConstructorBuilder
	{
		// Token: 0x06002544 RID: 9540 RVA: 0x0008625C File Offset: 0x0008445C
		internal ConstructorBuilder(TypeBuilder tb, MethodAttributes attributes, CallingConventions callingConvention, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.init_locals = true;
			base..ctor();
			this.attrs = attributes | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName;
			this.call_conv = callingConvention;
			if (parameterTypes != null)
			{
				for (int i = 0; i < parameterTypes.Length; i++)
				{
					if (parameterTypes[i] == null)
					{
						throw new ArgumentException("Elements of the parameterTypes array cannot be null", "parameterTypes");
					}
				}
				this.parameters = new Type[parameterTypes.Length];
				Array.Copy(parameterTypes, this.parameters, parameterTypes.Length);
			}
			this.type = tb;
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			this.table_idx = this.get_next_table_index(this, 6, true);
			((ModuleBuilder)tb.Module).RegisterToken(this, this.GetToken().Token);
		}

		/// <summary>Gets a <see cref="T:System.Reflection.CallingConventions" /> value that depends on whether the declaring type is generic.</summary>
		/// <returns>
		///   <see cref="F:System.Reflection.CallingConventions.HasThis" /> if the declaring type is generic; otherwise, <see cref="F:System.Reflection.CallingConventions.Standard" />. </returns>
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002545 RID: 9541 RVA: 0x00086324 File Offset: 0x00084524
		[MonoTODO]
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.call_conv;
			}
		}

		/// <summary>Gets or sets whether the local variables in this constructor should be zero-initialized.</summary>
		/// <returns>Read/write. Gets or sets whether the local variables in this constructor should be zero-initialized.</returns>
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06002546 RID: 9542 RVA: 0x0008632C File Offset: 0x0008452C
		// (set) Token: 0x06002547 RID: 9543 RVA: 0x00086334 File Offset: 0x00084534
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

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06002548 RID: 9544 RVA: 0x0008633D File Offset: 0x0008453D
		internal TypeBuilder TypeBuilder
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Returns the method implementation flags for this constructor.</summary>
		/// <returns>The method implementation flags for this constructor.</returns>
		// Token: 0x06002549 RID: 9545 RVA: 0x00086345 File Offset: 0x00084545
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.iattrs;
		}

		/// <summary>Returns the parameters of this constructor.</summary>
		/// <returns>Returns an array of <see cref="T:System.Reflection.ParameterInfo" /> objects that represent the parameters of this constructor.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called on this constructor's type, in the .NET Framework versions 1.0 and 1.1. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> has not been called on this constructor's type, in the .NET Framework version 2.0. </exception>
		// Token: 0x0600254A RID: 9546 RVA: 0x0008634D File Offset: 0x0008454D
		public override ParameterInfo[] GetParameters()
		{
			if (!this.type.is_created)
			{
				throw this.not_created();
			}
			return this.GetParametersInternal();
		}

		// Token: 0x0600254B RID: 9547 RVA: 0x0008636C File Offset: 0x0008456C
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

		// Token: 0x0600254C RID: 9548 RVA: 0x000863D2 File Offset: 0x000845D2
		internal override int GetParametersCount()
		{
			if (this.parameters == null)
			{
				return 0;
			}
			return this.parameters.Length;
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000863E6 File Offset: 0x000845E6
		internal override Type GetParameterType(int pos)
		{
			return this.parameters[pos];
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x000863F0 File Offset: 0x000845F0
		internal MethodBase RuntimeResolve()
		{
			return this.type.RuntimeResolve().GetConstructor(this);
		}

		/// <summary>Dynamically invokes the constructor reflected by this instance with the specified arguments, under the constraints of the specified Binder.</summary>
		/// <returns>An instance of the class associated with the constructor.</returns>
		/// <param name="obj">The object that needs to be reinitialized. </param>
		/// <param name="invokeAttr">One of the BindingFlags values that specifies the type of binding that is desired. </param>
		/// <param name="binder">A Binder that defines a set of properties and enables the binding, coercion of argument types, and invocation of members using reflection. If <paramref name="binder" /> is null, then Binder.DefaultBinding is used. </param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the constructor to be invoked. If there are no parameters, this should be a null reference (Nothing in Visual Basic). </param>
		/// <param name="culture">A <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. You can retrieve the constructor using <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> on the returned <see cref="T:System.Reflection.ConstructorInfo" />. </exception>
		// Token: 0x0600254F RID: 9551 RVA: 0x00086403 File Offset: 0x00084603
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.not_supported();
		}

		/// <summary>Invokes the constructor dynamically reflected by this instance on the given object, passing along the specified parameters, and under the constraints of the given binder.</summary>
		/// <returns>Returns an <see cref="T:System.Object" /> that is the return value of the invoked constructor.</returns>
		/// <param name="invokeAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" />, such as InvokeMethod, NonPublic, and so on. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects using reflection. If binder is null, the default binder is used. See <see cref="T:System.Reflection.Binder" />. </param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the constructor to be invoked. If there are no parameters this should be null. </param>
		/// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. (For example, this is necessary to convert a <see cref="T:System.String" /> that represents 1000 to a <see cref="T:System.Double" /> value, since 1000 is represented differently by different cultures.) </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. You can retrieve the constructor using <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.ConstructorInfo.Invoke(System.Reflection.BindingFlags,System.Reflection.Binder,System.Object[],System.Globalization.CultureInfo)" /> on the returned <see cref="T:System.Reflection.ConstructorInfo" />. </exception>
		// Token: 0x06002550 RID: 9552 RVA: 0x00086403 File Offset: 0x00084603
		public override object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.not_supported();
		}

		/// <summary>Retrieves the internal handle for the method. Use this handle to access the underlying metadata handle.</summary>
		/// <returns>Returns the internal handle for the method. Use this handle to access the underlying metadata handle.</returns>
		/// <exception cref="T:System.NotSupportedException">This property is not supported on this class. </exception>
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06002551 RID: 9553 RVA: 0x00086403 File Offset: 0x00084603
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw this.not_supported();
			}
		}

		/// <summary>Retrieves the attributes for this constructor.</summary>
		/// <returns>Returns the attributes for this constructor.</returns>
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06002552 RID: 9554 RVA: 0x0008640B File Offset: 0x0008460B
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		/// <summary>Holds a reference to the <see cref="T:System.Type" /> object from which this object was obtained.</summary>
		/// <returns>Returns the Type object from which this object was obtained.</returns>
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06002553 RID: 9555 RVA: 0x0008633D File Offset: 0x0008453D
		public override Type ReflectedType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Retrieves a reference to the <see cref="T:System.Type" /> object for the type that declares this member.</summary>
		/// <returns>Returns the <see cref="T:System.Type" /> object for the type that declares this member.</returns>
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x0008633D File Offset: 0x0008453D
		public override Type DeclaringType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Gets null.</summary>
		/// <returns>Returns null.</returns>
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06002555 RID: 9557 RVA: 0x0000A42E File Offset: 0x0000862E
		[Obsolete]
		public Type ReturnType
		{
			get
			{
				return null;
			}
		}

		/// <summary>Retrieves the name of this constructor.</summary>
		/// <returns>Returns the name of this constructor.</returns>
		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06002556 RID: 9558 RVA: 0x00086413 File Offset: 0x00084613
		public override string Name
		{
			get
			{
				if ((this.attrs & MethodAttributes.Static) == MethodAttributes.PrivateScope)
				{
					return ConstructorInfo.ConstructorName;
				}
				return ConstructorInfo.TypeConstructorName;
			}
		}

		/// <summary>Retrieves the signature of the field in the form of a string.</summary>
		/// <returns>Returns the signature of the field.</returns>
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06002557 RID: 9559 RVA: 0x0008642B File Offset: 0x0008462B
		public string Signature
		{
			get
			{
				return "constructor signature";
			}
		}

		/// <summary>Adds declarative security to this constructor.</summary>
		/// <param name="action">The security action to be taken, such as Demand, Assert, and so on. </param>
		/// <param name="pset">The set of permissions the action applies to. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="action" /> is invalid (RequestMinimum, RequestOptional, and RequestRefuse are invalid). </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or- The permission set <paramref name="pset" /> contains an action that was added earlier by AddDeclarativeSecurity. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pset" /> is null. </exception>
		// Token: 0x06002558 RID: 9560 RVA: 0x00086434 File Offset: 0x00084634
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			if (action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("action", "Request* values are not permitted");
			}
			this.RejectIfCreated();
			if (this.permissions != null)
			{
				RefEmitPermissionSet[] array = this.permissions;
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i].action == action)
					{
						throw new InvalidOperationException("Multiple permission sets specified with the same SecurityAction.");
					}
				}
				RefEmitPermissionSet[] array2 = new RefEmitPermissionSet[this.permissions.Length + 1];
				this.permissions.CopyTo(array2, 0);
				this.permissions = array2;
			}
			else
			{
				this.permissions = new RefEmitPermissionSet[1];
			}
			this.permissions[this.permissions.Length - 1] = new RefEmitPermissionSet(action, pset.ToXml().ToString());
			this.attrs |= MethodAttributes.HasSecurity;
		}

		/// <summary>Defines a parameter of this constructor.</summary>
		/// <returns>Returns a ParameterBuilder object that represents the new parameter of this constructor.</returns>
		/// <param name="iSequence">The position of the parameter in the parameter list. Parameters are indexed beginning with the number 1 for the first parameter. </param>
		/// <param name="attributes">The attributes of the parameter. </param>
		/// <param name="strParamName">The name of the parameter. The name can be the null string. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="iSequence" /> is less than 0 (zero), or it is greater than the number of parameters of the constructor. </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x06002559 RID: 9561 RVA: 0x00086514 File Offset: 0x00084714
		public ParameterBuilder DefineParameter(int iSequence, ParameterAttributes attributes, string strParamName)
		{
			if (iSequence < 0 || iSequence > this.GetParametersCount())
			{
				throw new ArgumentOutOfRangeException("iSequence");
			}
			if (this.type.is_created)
			{
				throw this.not_after_created();
			}
			ParameterBuilder parameterBuilder = new ParameterBuilder(this, iSequence, attributes, strParamName);
			if (this.pinfo == null)
			{
				this.pinfo = new ParameterBuilder[this.parameters.Length + 1];
			}
			this.pinfo[iSequence] = parameterBuilder;
			return parameterBuilder;
		}

		/// <summary>Checks if the specified custom attribute type is defined.</summary>
		/// <returns>true if the specified custom attribute type is defined; otherwise, false.</returns>
		/// <param name="attributeType">A custom attribute type. </param>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. This parameter is ignored. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. You can retrieve the constructor using <see cref="M:System.Type.GetConstructor(System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Reflection.ConstructorInfo" />. </exception>
		// Token: 0x0600255A RID: 9562 RVA: 0x00086403 File Offset: 0x00084603
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Returns all the custom attributes defined for this constructor.</summary>
		/// <returns>Returns an array of objects representing all the custom attributes of the constructor represented by this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> instance.</returns>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. This parameter is ignored. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		// Token: 0x0600255B RID: 9563 RVA: 0x00086403 File Offset: 0x00084603
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Returns the custom attributes identified by the given type.</summary>
		/// <returns>Returns an array of type <see cref="T:System.Object" /> representing the attributes of this constructor.</returns>
		/// <param name="attributeType">The custom attribute type. </param>
		/// <param name="inherit">Controls inheritance of custom attributes from base classes. This parameter is ignored. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. </exception>
		// Token: 0x0600255C RID: 9564 RVA: 0x00086403 File Offset: 0x00084603
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw this.not_supported();
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Emit.ILGenerator" /> for this constructor.</summary>
		/// <returns>Returns an <see cref="T:System.Reflection.Emit.ILGenerator" /> object for this constructor.</returns>
		/// <exception cref="T:System.InvalidOperationException">The constructor is a default constructor.-or-The constructor has <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags indicating that it should not have a method body.</exception>
		// Token: 0x0600255D RID: 9565 RVA: 0x0008657E File Offset: 0x0008477E
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Emit.ILGenerator" /> object, with the specified MSIL stream size, that can be used to build a method body for this constructor.</summary>
		/// <returns>An <see cref="T:System.Reflection.Emit.ILGenerator" /> for this constructor.</returns>
		/// <param name="streamSize">The size of the MSIL stream, in bytes.</param>
		/// <exception cref="T:System.InvalidOperationException">The constructor is a default constructor.-or-The constructor has <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags indicating that it should not have a method body. </exception>
		// Token: 0x0600255E RID: 9566 RVA: 0x00086588 File Offset: 0x00084788
		public ILGenerator GetILGenerator(int streamSize)
		{
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.type.Module, ((ModuleBuilder)this.type.Module).GetTokenGenerator(), streamSize);
			return this.ilgen;
		}

		/// <summary>Creates the body of the constructor by using a specified byte array of Microsoft intermediate language (MSIL) instructions.</summary>
		/// <param name="il">An array that contains valid MSIL instructions.</param>
		/// <param name="maxStack">The maximum stack evaluation depth.</param>
		/// <param name="localSignature">An array of bytes that contain the serialized local variable structure. Specify null if the constructor has no local variables.</param>
		/// <param name="exceptionHandlers">A collection that contains the exception handlers for the constructor. Specify null if the constructor has no exception handlers.</param>
		/// <param name="tokenFixups">A collection of values that represent offsets in <paramref name="il" />, each of which specifies the beginning of a token that may be modified. Specify null if the constructor has no tokens that have to be modified.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="il" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxStack" /> is negative.-or- One of <paramref name="exceptionHandlers" /> specifies an offset outside of <paramref name="il" />.-or- One of <paramref name="tokenFixups" /> specifies an offset that is outside the <paramref name="il" /> array.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method.-or- This method was called previously on this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> object.</exception>
		// Token: 0x0600255F RID: 9567 RVA: 0x000865D6 File Offset: 0x000847D6
		public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			this.GetILGenerator().Init(il, maxStack, localSignature, exceptionHandlers, tokenFixups);
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is null. </exception>
		// Token: 0x06002560 RID: 9568 RVA: 0x000865EC File Offset: 0x000847EC
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			if (customBuilder.Ctor.ReflectedType.FullName == "System.Runtime.CompilerServices.MethodImplAttribute")
			{
				byte[] data = customBuilder.Data;
				int num = (int)data[2];
				num |= (int)data[3] << 8;
				this.SetImplementationFlags((MethodImplAttributes)num);
				return;
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

		/// <summary>Set a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="binaryAttribute">A byte blob representing the attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is null. </exception>
		// Token: 0x06002561 RID: 9569 RVA: 0x0008668B File Offset: 0x0008488B
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		/// <summary>Sets the method implementation flags for this constructor.</summary>
		/// <param name="attributes">The method implementation flags. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />. </exception>
		// Token: 0x06002562 RID: 9570 RVA: 0x000866BC File Offset: 0x000848BC
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			if (this.type.is_created)
			{
				throw this.not_after_created();
			}
			this.iattrs = attributes;
		}

		/// <summary>Returns a reference to the module that contains this constructor.</summary>
		/// <returns>The module that contains this constructor.</returns>
		// Token: 0x06002563 RID: 9571 RVA: 0x000866D9 File Offset: 0x000848D9
		public Module GetModule()
		{
			return this.type.Module;
		}

		/// <summary>Returns the <see cref="T:System.Reflection.Emit.MethodToken" /> that represents the token for this constructor.</summary>
		/// <returns>Returns the <see cref="T:System.Reflection.Emit.MethodToken" /> of this constructor.</returns>
		// Token: 0x06002564 RID: 9572 RVA: 0x000866E6 File Offset: 0x000848E6
		public MethodToken GetToken()
		{
			return new MethodToken(100663296 | this.table_idx);
		}

		/// <summary>Sets this constructor's custom attribute associated with symbolic information.</summary>
		/// <param name="name">The name of the custom attribute. </param>
		/// <param name="data">The value of the custom attribute. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or- The module does not have a symbol writer defined. For example, the module is not a debug module. </exception>
		// Token: 0x06002565 RID: 9573 RVA: 0x000866F9 File Offset: 0x000848F9
		[MonoTODO]
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			if (this.type.is_created)
			{
				throw this.not_after_created();
			}
		}

		/// <summary>Gets the dynamic module in which this constructor is defined.</summary>
		/// <returns>A <see cref="T:System.Reflection.Module" /> object that represents the dynamic module in which this constructor is defined.</returns>
		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06002566 RID: 9574 RVA: 0x0008670F File Offset: 0x0008490F
		public override Module Module
		{
			get
			{
				return this.GetModule();
			}
		}

		/// <summary>Returns this <see cref="T:System.Reflection.Emit.ConstructorBuilder" /> instance as a <see cref="T:System.String" />.</summary>
		/// <returns>Returns a <see cref="T:System.String" /> containing the name, attributes, and exceptions of this constructor, followed by the current Microsoft intermediate language (MSIL) stream.</returns>
		// Token: 0x06002567 RID: 9575 RVA: 0x00086717 File Offset: 0x00084917
		public override string ToString()
		{
			return "ConstructorBuilder ['" + this.type.Name + "']";
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x00086734 File Offset: 0x00084934
		internal void fixup()
		{
			if ((this.attrs & (MethodAttributes.Abstract | MethodAttributes.PinvokeImpl)) == MethodAttributes.PrivateScope && (this.iattrs & (MethodImplAttributes)4099) == MethodImplAttributes.IL && (this.ilgen == null || this.ilgen.ILOffset == 0))
			{
				throw new InvalidOperationException("Method '" + this.Name + "' does not have a method body.");
			}
			if (this.ilgen != null)
			{
				this.ilgen.label_fixup(this);
			}
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x000867A4 File Offset: 0x000849A4
		internal void ResolveUserTypes()
		{
			TypeBuilder.ResolveUserTypes(this.parameters);
			if (this.paramModReq != null)
			{
				Type[][] array = this.paramModReq;
				for (int i = 0; i < array.Length; i++)
				{
					TypeBuilder.ResolveUserTypes(array[i]);
				}
			}
			if (this.paramModOpt != null)
			{
				Type[][] array = this.paramModOpt;
				for (int i = 0; i < array.Length; i++)
				{
					TypeBuilder.ResolveUserTypes(array[i]);
				}
			}
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x00086806 File Offset: 0x00084A06
		internal void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map)
		{
			if (this.ilgen != null)
			{
				this.ilgen.FixupTokens(token_map, member_map);
			}
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x00086820 File Offset: 0x00084A20
		internal void GenerateDebugInfo(ISymbolWriter symbolWriter)
		{
			if (this.ilgen != null && this.ilgen.HasDebugInfo)
			{
				SymbolToken symbolToken = new SymbolToken(this.GetToken().Token);
				symbolWriter.OpenMethod(symbolToken);
				symbolWriter.SetSymAttribute(symbolToken, "__name", Encoding.UTF8.GetBytes(this.Name));
				this.ilgen.GenerateDebugInfo(symbolWriter);
				symbolWriter.CloseMethod();
			}
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x0008688C File Offset: 0x00084A8C
		internal override int get_next_table_index(object obj, int table, bool inc)
		{
			return this.type.get_next_table_index(obj, table, inc);
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x0008689C File Offset: 0x00084A9C
		private void RejectIfCreated()
		{
			if (this.type.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x00085D62 File Offset: 0x00083F62
		private Exception not_supported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000868B6 File Offset: 0x00084AB6
		private Exception not_after_created()
		{
			return new InvalidOperationException("Unable to change after type has been created.");
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x000868C2 File Offset: 0x00084AC2
		private Exception not_created()
		{
			return new NotSupportedException("The type is not yet created.");
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002571 RID: 9585 RVA: 0x0002126B File Offset: 0x0001F46B
		void _ConstructorBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002572 RID: 9586 RVA: 0x0002126B File Offset: 0x0001F46B
		void _ConstructorBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002573 RID: 9587 RVA: 0x0002126B File Offset: 0x0001F46B
		void _ConstructorBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x06002574 RID: 9588 RVA: 0x0002126B File Offset: 0x0001F46B
		void _ConstructorBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002575 RID: 9589 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal ConstructorBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040013C3 RID: 5059
		private RuntimeMethodHandle mhandle;

		// Token: 0x040013C4 RID: 5060
		private ILGenerator ilgen;

		// Token: 0x040013C5 RID: 5061
		internal Type[] parameters;

		// Token: 0x040013C6 RID: 5062
		private MethodAttributes attrs;

		// Token: 0x040013C7 RID: 5063
		private MethodImplAttributes iattrs;

		// Token: 0x040013C8 RID: 5064
		private int table_idx;

		// Token: 0x040013C9 RID: 5065
		private CallingConventions call_conv;

		// Token: 0x040013CA RID: 5066
		private TypeBuilder type;

		// Token: 0x040013CB RID: 5067
		internal ParameterBuilder[] pinfo;

		// Token: 0x040013CC RID: 5068
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040013CD RID: 5069
		private bool init_locals;

		// Token: 0x040013CE RID: 5070
		private Type[][] paramModReq;

		// Token: 0x040013CF RID: 5071
		private Type[][] paramModOpt;

		// Token: 0x040013D0 RID: 5072
		private RefEmitPermissionSet[] permissions;
	}
}
