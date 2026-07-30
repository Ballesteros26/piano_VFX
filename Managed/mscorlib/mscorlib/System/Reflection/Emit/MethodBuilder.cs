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
	/// <summary>Defines and represents a method (or constructor) on a dynamic class.</summary>
	// Token: 0x02000369 RID: 873
	[ComDefaultInterface(typeof(_MethodBuilder))]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class MethodBuilder : MethodInfo, _MethodBuilder
	{
		// Token: 0x06002756 RID: 10070 RVA: 0x0008BAFC File Offset: 0x00089CFC
		internal MethodBuilder(TypeBuilder tb, string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt)
		{
			this.init_locals = true;
			base..ctor();
			this.name = name;
			this.attrs = attributes;
			this.call_conv = callingConvention;
			this.rtype = returnType;
			this.returnModReq = returnModReq;
			this.returnModOpt = returnModOpt;
			this.paramModReq = paramModReq;
			this.paramModOpt = paramModOpt;
			if ((attributes & MethodAttributes.Static) == MethodAttributes.PrivateScope)
			{
				this.call_conv |= CallingConventions.HasThis;
			}
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
			this.table_idx = this.get_next_table_index(this, 6, true);
			((ModuleBuilder)tb.Module).RegisterToken(this, this.GetToken().Token);
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x0008BBF0 File Offset: 0x00089DF0
		internal MethodBuilder(TypeBuilder tb, string name, MethodAttributes attributes, CallingConventions callingConvention, Type returnType, Type[] returnModReq, Type[] returnModOpt, Type[] parameterTypes, Type[][] paramModReq, Type[][] paramModOpt, string dllName, string entryName, CallingConvention nativeCConv, CharSet nativeCharset)
			: this(tb, name, attributes, callingConvention, returnType, returnModReq, returnModOpt, parameterTypes, paramModReq, paramModOpt)
		{
			this.pi_dll = dllName;
			this.pi_entry = entryName;
			this.native_cc = nativeCConv;
			this.charset = nativeCharset;
		}

		/// <summary>Not supported for this type.</summary>
		/// <returns>Not supported.</returns>
		/// <exception cref="T:System.NotSupportedException">The invoked method is not supported in the base class.</exception>
		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06002758 RID: 10072 RVA: 0x00014B5A File Offset: 0x00012D5A
		public override bool ContainsGenericParameters
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		/// <summary>Gets or sets a Boolean value that specifies whether the local variables in this method are zero initialized. The default value of this property is true.</summary>
		/// <returns>true if the local variables in this method should be zero initialized; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. (Get or set.)</exception>
		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06002759 RID: 10073 RVA: 0x0008BC34 File Offset: 0x00089E34
		// (set) Token: 0x0600275A RID: 10074 RVA: 0x0008BC3C File Offset: 0x00089E3C
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

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x0008BC45 File Offset: 0x00089E45
		internal TypeBuilder TypeBuilder
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Retrieves the internal handle for the method. Use this handle to access the underlying metadata handle.</summary>
		/// <returns>Read-only. The internal handle for the method. Use this handle to access the underlying metadata handle.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="P:System.Reflection.MethodBase.MethodHandle" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600275C RID: 10076 RVA: 0x0008BC4D File Offset: 0x00089E4D
		public override RuntimeMethodHandle MethodHandle
		{
			get
			{
				throw this.NotSupported();
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x0008BC55 File Offset: 0x00089E55
		internal RuntimeMethodHandle MethodHandleInternal
		{
			get
			{
				return this.mhandle;
			}
		}

		/// <summary>Gets the return type of the method represented by this <see cref="T:System.Reflection.Emit.MethodBuilder" />.</summary>
		/// <returns>The return type of the method.</returns>
		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x0600275E RID: 10078 RVA: 0x0008BC5D File Offset: 0x00089E5D
		public override Type ReturnType
		{
			get
			{
				return this.rtype;
			}
		}

		/// <summary>Retrieves the class that was used in reflection to obtain this object.</summary>
		/// <returns>Read-only. The type used to obtain this method.</returns>
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x0008BC45 File Offset: 0x00089E45
		public override Type ReflectedType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Returns the type that declares this method.</summary>
		/// <returns>Read-only. The type that declares this method.</returns>
		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06002760 RID: 10080 RVA: 0x0008BC45 File Offset: 0x00089E45
		public override Type DeclaringType
		{
			get
			{
				return this.type;
			}
		}

		/// <summary>Retrieves the name of this method.</summary>
		/// <returns>Read-only. Retrieves a string containing the simple name of this method.</returns>
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06002761 RID: 10081 RVA: 0x0008BC65 File Offset: 0x00089E65
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Retrieves the attributes for this method.</summary>
		/// <returns>Read-only. Retrieves the MethodAttributes for this method.</returns>
		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06002762 RID: 10082 RVA: 0x0008BC6D File Offset: 0x00089E6D
		public override MethodAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		/// <summary>Returns the custom attributes of the method's return type.</summary>
		/// <returns>Read-only. The custom attributes of the method's return type.</returns>
		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x0000A42E File Offset: 0x0000862E
		public override ICustomAttributeProvider ReturnTypeCustomAttributes
		{
			get
			{
				return null;
			}
		}

		/// <summary>Returns the calling convention of the method.</summary>
		/// <returns>Read-only. The calling convention of the method.</returns>
		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x0008BC75 File Offset: 0x00089E75
		public override CallingConventions CallingConvention
		{
			get
			{
				return this.call_conv;
			}
		}

		/// <summary>Retrieves the signature of the method.</summary>
		/// <returns>Read-only. A String containing the signature of the method reflected by this MethodBase instance.</returns>
		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002765 RID: 10085 RVA: 0x0002126B File Offset: 0x0001F46B
		[MonoTODO("Not implemented")]
		public string Signature
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000640 RID: 1600
		// (set) Token: 0x06002766 RID: 10086 RVA: 0x0008BC7D File Offset: 0x00089E7D
		internal bool BestFitMapping
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709551567UL) | (value ? 16UL : 32UL));
			}
		}

		// Token: 0x17000641 RID: 1601
		// (set) Token: 0x06002767 RID: 10087 RVA: 0x0008BC9C File Offset: 0x00089E9C
		internal bool ThrowOnUnmappableChar
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709539327UL) | (value ? 4096UL : 8192UL));
			}
		}

		// Token: 0x17000642 RID: 1602
		// (set) Token: 0x06002768 RID: 10088 RVA: 0x0008BCC4 File Offset: 0x00089EC4
		internal bool ExactSpelling
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709551614UL) | (value ? 1UL : 0UL));
			}
		}

		// Token: 0x17000643 RID: 1603
		// (set) Token: 0x06002769 RID: 10089 RVA: 0x0008BCE1 File Offset: 0x00089EE1
		internal bool SetLastError
		{
			set
			{
				this.extra_flags = (uint)(((ulong)this.extra_flags & 18446744073709551551UL) | (value ? 64UL : 0UL));
			}
		}

		/// <summary>Returns the MethodToken that represents the token for this method.</summary>
		/// <returns>Returns the MethodToken of this method.</returns>
		// Token: 0x0600276A RID: 10090 RVA: 0x0008BCFF File Offset: 0x00089EFF
		public MethodToken GetToken()
		{
			return new MethodToken(100663296 | this.table_idx);
		}

		/// <summary>Return the base implementation for a method.</summary>
		/// <returns>The base implementation of this method.</returns>
		// Token: 0x0600276B RID: 10091 RVA: 0x00002119 File Offset: 0x00000319
		public override MethodInfo GetBaseDefinition()
		{
			return this;
		}

		/// <summary>Returns the implementation flags for the method.</summary>
		/// <returns>Returns the implementation flags for the method.</returns>
		// Token: 0x0600276C RID: 10092 RVA: 0x0008BD12 File Offset: 0x00089F12
		public override MethodImplAttributes GetMethodImplementationFlags()
		{
			return this.iattrs;
		}

		/// <summary>Returns the parameters of this method.</summary>
		/// <returns>An array of ParameterInfo objects that represent the parameters of the method.</returns>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call GetParameters on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x0600276D RID: 10093 RVA: 0x0008BD1A File Offset: 0x00089F1A
		public override ParameterInfo[] GetParameters()
		{
			if (!this.type.is_created)
			{
				throw this.NotSupported();
			}
			return this.GetParametersInternal();
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x0008BD38 File Offset: 0x00089F38
		internal override ParameterInfo[] GetParametersInternal()
		{
			if (this.parameters == null)
			{
				return null;
			}
			ParameterInfo[] array = new ParameterInfo[this.parameters.Length];
			for (int i = 0; i < this.parameters.Length; i++)
			{
				array[i] = ParameterInfo.New((this.pinfo == null) ? null : this.pinfo[i + 1], this.parameters[i], this, i + 1);
			}
			return array;
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x0008BD9A File Offset: 0x00089F9A
		internal override int GetParametersCount()
		{
			if (this.parameters == null)
			{
				return 0;
			}
			return this.parameters.Length;
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x0008BDAE File Offset: 0x00089FAE
		internal override Type GetParameterType(int pos)
		{
			return this.parameters[pos];
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x0008BDB8 File Offset: 0x00089FB8
		internal MethodBase RuntimeResolve()
		{
			return this.type.RuntimeResolve().GetMethod(this);
		}

		/// <summary>Returns a reference to the module that contains this method.</summary>
		/// <returns>Returns a reference to the module that contains this method.</returns>
		// Token: 0x06002772 RID: 10098 RVA: 0x0008BDCB File Offset: 0x00089FCB
		public Module GetModule()
		{
			return this.type.Module;
		}

		/// <summary>Creates the body of the method using a supplied byte array of Microsoft intermediate language (MSIL) instructions.</summary>
		/// <param name="il">An array containing valid MSIL instructions. If this parameter is null, the method's body is cleared. </param>
		/// <param name="count">The number of valid bytes in the MSIL array. This value is ignored if MSIL is null. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="count" /> is not within the range of indexes of the supplied MSIL instruction array and <paramref name="il" /> is not null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or- This method was called previously on this MethodBuilder with an <paramref name="il" /> argument that was not null.-or-For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. </exception>
		// Token: 0x06002773 RID: 10099 RVA: 0x0008BDD8 File Offset: 0x00089FD8
		public void CreateMethodBody(byte[] il, int count)
		{
			if (il != null && (count < 0 || count > il.Length))
			{
				throw new ArgumentOutOfRangeException("Index was out of range.  Must be non-negative and less than the size of the collection.");
			}
			if (this.code != null || this.type.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
			if (il == null)
			{
				this.code = null;
				return;
			}
			this.code = new byte[count];
			Array.Copy(il, this.code, count);
		}

		/// <summary>Creates the body of the method by using a specified byte array of Microsoft intermediate language (MSIL) instructions.</summary>
		/// <param name="il">An array that contains valid MSIL instructions.</param>
		/// <param name="maxStack">The maximum stack evaluation depth.</param>
		/// <param name="localSignature">An array of bytes that contain the serialized local variable structure. Specify null if the method has no local variables.</param>
		/// <param name="exceptionHandlers">A collection that contains the exception handlers for the method. Specify null if the method has no exception handlers.</param>
		/// <param name="tokenFixups">A collection of values that represent offsets in <paramref name="il" />, each of which specifies the beginning of a token that may be modified. Specify null if the method has no tokens that have to be modified.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="il" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxStack" /> is negative.-or- One of <paramref name="exceptionHandlers" /> specifies an offset outside of <paramref name="il" />.-or- One of <paramref name="tokenFixups" /> specifies an offset that is outside the <paramref name="il" /> array.</exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using the <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" /> method.-or- This method was called previously on this <see cref="T:System.Reflection.Emit.MethodBuilder" /> object.</exception>
		// Token: 0x06002774 RID: 10100 RVA: 0x0008BE41 File Offset: 0x0008A041
		public void SetMethodBody(byte[] il, int maxStack, byte[] localSignature, IEnumerable<ExceptionHandler> exceptionHandlers, IEnumerable<int> tokenFixups)
		{
			this.GetILGenerator().Init(il, maxStack, localSignature, exceptionHandlers, tokenFixups);
		}

		/// <summary>Dynamically invokes the method reflected by this instance on the given object, passing along the specified parameters, and under the constraints of the given binder.</summary>
		/// <returns>Returns an object containing the return value of the invoked method.</returns>
		/// <param name="obj">The object on which to invoke the specified method. If the method is static, this parameter is ignored. </param>
		/// <param name="invokeAttr">This must be a bit flag from <see cref="T:System.Reflection.BindingFlags" /> : InvokeMethod, NonPublic, and so on. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of MemberInfo objects via reflection. If binder is null, the default binder is used. For more details, see <see cref="T:System.Reflection.Binder" />. </param>
		/// <param name="parameters">An argument list. This is an array of arguments with the same number, order, and type as the parameters of the method to be invoked. If there are no parameters this should be null. </param>
		/// <param name="culture">An instance of <see cref="T:System.Globalization.CultureInfo" /> used to govern the coercion of types. If this is null, the <see cref="T:System.Globalization.CultureInfo" /> for the current thread is used. (Note that this is necessary to, for example, convert a <see cref="T:System.String" /> that represents 1000 to a <see cref="T:System.Double" /> value, since 1000 is represented differently by different cultures.) </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Type.InvokeMember(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Object,System.Object[],System.Reflection.ParameterModifier[],System.Globalization.CultureInfo,System.String[])" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x06002775 RID: 10101 RVA: 0x0008BC4D File Offset: 0x00089E4D
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			throw this.NotSupported();
		}

		/// <summary>Checks if the specified custom attribute type is defined.</summary>
		/// <returns>true if the specified custom attribute type is defined; otherwise, false.</returns>
		/// <param name="attributeType">The custom attribute type. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the custom attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.IsDefined(System.Type,System.Boolean)" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x06002776 RID: 10102 RVA: 0x0008BC4D File Offset: 0x00089E4D
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw this.NotSupported();
		}

		/// <summary>Returns all the custom attributes defined for this method.</summary>
		/// <returns>Returns an array of objects representing all the custom attributes of this method.</returns>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the custom attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x06002777 RID: 10103 RVA: 0x0008BE55 File Offset: 0x0008A055
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (this.type.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, inherit);
			}
			throw this.NotSupported();
		}

		/// <summary>Returns the custom attributes identified by the given type.</summary>
		/// <returns>Returns an array of objects representing the attributes of this method that are of type <paramref name="attributeType" />.</returns>
		/// <param name="attributeType">The custom attribute type. </param>
		/// <param name="inherit">Specifies whether to search this member's inheritance chain to find the custom attributes. </param>
		/// <exception cref="T:System.NotSupportedException">This method is not currently supported. Retrieve the method using <see cref="M:System.Type.GetMethod(System.String,System.Reflection.BindingFlags,System.Reflection.Binder,System.Reflection.CallingConventions,System.Type[],System.Reflection.ParameterModifier[])" /> and call <see cref="M:System.Reflection.MemberInfo.GetCustomAttributes(System.Boolean)" /> on the returned <see cref="T:System.Reflection.MethodInfo" />. </exception>
		// Token: 0x06002778 RID: 10104 RVA: 0x0008BE72 File Offset: 0x0008A072
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (this.type.is_created)
			{
				return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
			}
			throw this.NotSupported();
		}

		/// <summary>Returns an ILGenerator for this method with a default Microsoft intermediate language (MSIL) stream size of 64 bytes.</summary>
		/// <returns>Returns an ILGenerator object for this method.</returns>
		/// <exception cref="T:System.InvalidOperationException">The method should not have a body because of its <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags, for example because it has the <see cref="F:System.Reflection.MethodAttributes.PinvokeImpl" /> flag. -or-The method is a generic method, but not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. </exception>
		// Token: 0x06002779 RID: 10105 RVA: 0x0008BE90 File Offset: 0x0008A090
		public ILGenerator GetILGenerator()
		{
			return this.GetILGenerator(64);
		}

		/// <summary>Returns an ILGenerator for this method with the specified Microsoft intermediate language (MSIL) stream size.</summary>
		/// <returns>Returns an ILGenerator object for this method.</returns>
		/// <param name="size">The size of the MSIL stream, in bytes. </param>
		/// <exception cref="T:System.InvalidOperationException">The method should not have a body because of its <see cref="T:System.Reflection.MethodAttributes" /> or <see cref="T:System.Reflection.MethodImplAttributes" /> flags, for example because it has the <see cref="F:System.Reflection.MethodAttributes.PinvokeImpl" /> flag. -or-The method is a generic method, but not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.   </exception>
		// Token: 0x0600277A RID: 10106 RVA: 0x0008BE9C File Offset: 0x0008A09C
		public ILGenerator GetILGenerator(int size)
		{
			if ((this.iattrs & MethodImplAttributes.CodeTypeMask) != MethodImplAttributes.IL || (this.iattrs & MethodImplAttributes.ManagedMask) != MethodImplAttributes.IL)
			{
				throw new InvalidOperationException("Method body should not exist.");
			}
			if (this.ilgen != null)
			{
				return this.ilgen;
			}
			this.ilgen = new ILGenerator(this.type.Module, ((ModuleBuilder)this.type.Module).GetTokenGenerator(), size);
			return this.ilgen;
		}

		/// <summary>Sets the parameter attributes and the name of a parameter of this method, or of the return value of this method. Returns a ParameterBuilder that can be used to apply custom attributes.</summary>
		/// <returns>Returns a ParameterBuilder object that represents a parameter of this method or the return value of this method.</returns>
		/// <param name="position">The position of the parameter in the parameter list. Parameters are indexed beginning with the number 1 for the first parameter; the number 0 represents the return value of the method. </param>
		/// <param name="attributes">The parameter attributes of the parameter. </param>
		/// <param name="strParamName">The name of the parameter. The name can be the null string. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The method has no parameters.-or- <paramref name="position" /> is less than zero.-or- <paramref name="position" /> is greater than the number of the method's parameters. </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. </exception>
		// Token: 0x0600277B RID: 10107 RVA: 0x0008BF0C File Offset: 0x0008A10C
		public ParameterBuilder DefineParameter(int position, ParameterAttributes attributes, string strParamName)
		{
			this.RejectIfCreated();
			if (position < 0 || this.parameters == null || position > this.parameters.Length)
			{
				throw new ArgumentOutOfRangeException("position");
			}
			ParameterBuilder parameterBuilder = new ParameterBuilder(this, position, attributes, strParamName);
			if (this.pinfo == null)
			{
				this.pinfo = new ParameterBuilder[this.parameters.Length + 1];
			}
			this.pinfo[position] = parameterBuilder;
			return parameterBuilder;
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x0008BF74 File Offset: 0x0008A174
		internal void check_override()
		{
			if (this.override_methods != null)
			{
				foreach (MethodInfo methodInfo in this.override_methods)
				{
					if (methodInfo.IsVirtual && !base.IsVirtual)
					{
						throw new TypeLoadException(string.Format("Method '{0}' override '{1}' but it is not virtual", this.name, methodInfo));
					}
				}
			}
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x0008BFCC File Offset: 0x0008A1CC
		internal void fixup()
		{
			if ((this.attrs & (MethodAttributes.Abstract | MethodAttributes.PinvokeImpl)) == MethodAttributes.PrivateScope && (this.iattrs & (MethodImplAttributes)4099) == MethodImplAttributes.IL && (this.ilgen == null || this.ilgen.ILOffset == 0) && (this.code == null || this.code.Length == 0))
			{
				throw new InvalidOperationException(string.Format("Method '{0}.{1}' does not have a method body.", this.DeclaringType.FullName, this.Name));
			}
			if (this.ilgen != null)
			{
				this.ilgen.label_fixup(this);
			}
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x0008C050 File Offset: 0x0008A250
		internal void ResolveUserTypes()
		{
			this.rtype = TypeBuilder.ResolveUserType(this.rtype);
			TypeBuilder.ResolveUserTypes(this.parameters);
			TypeBuilder.ResolveUserTypes(this.returnModReq);
			TypeBuilder.ResolveUserTypes(this.returnModOpt);
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

		// Token: 0x0600277F RID: 10111 RVA: 0x0008C0D9 File Offset: 0x0008A2D9
		internal void FixupTokens(Dictionary<int, int> token_map, Dictionary<int, MemberInfo> member_map)
		{
			if (this.ilgen != null)
			{
				this.ilgen.FixupTokens(token_map, member_map);
			}
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x0008C0F0 File Offset: 0x0008A2F0
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

		/// <summary>Sets a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to describe the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="customBuilder" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.</exception>
		// Token: 0x06002781 RID: 10113 RVA: 0x0008C15C File Offset: 0x0008A35C
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.CompilerServices.MethodImplAttribute")
			{
				byte[] data = customBuilder.Data;
				int num = (int)data[2];
				num |= (int)data[3] << 8;
				this.iattrs |= (MethodImplAttributes)num;
				return;
			}
			if (!(fullName == "System.Runtime.InteropServices.DllImportAttribute"))
			{
				if (fullName == "System.Runtime.InteropServices.PreserveSigAttribute")
				{
					this.iattrs |= MethodImplAttributes.PreserveSig;
					return;
				}
				if (fullName == "System.Runtime.CompilerServices.SpecialNameAttribute")
				{
					this.attrs |= MethodAttributes.SpecialName;
					return;
				}
				if (fullName == "System.Security.SuppressUnmanagedCodeSecurityAttribute")
				{
					this.attrs |= MethodAttributes.HasSecurity;
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
				return;
			}
			else
			{
				CustomAttributeBuilder.CustomAttributeInfo customAttributeInfo = CustomAttributeBuilder.decode_cattr(customBuilder);
				bool flag = true;
				this.pi_dll = (string)customAttributeInfo.ctorArgs[0];
				if (this.pi_dll == null || this.pi_dll.Length == 0)
				{
					throw new ArgumentException("DllName cannot be empty");
				}
				this.native_cc = global::System.Runtime.InteropServices.CallingConvention.Winapi;
				for (int i = 0; i < customAttributeInfo.namedParamNames.Length; i++)
				{
					string text = customAttributeInfo.namedParamNames[i];
					object obj = customAttributeInfo.namedParamValues[i];
					if (text == "CallingConvention")
					{
						this.native_cc = (CallingConvention)obj;
					}
					else if (text == "CharSet")
					{
						this.charset = (CharSet)obj;
					}
					else if (text == "EntryPoint")
					{
						this.pi_entry = (string)obj;
					}
					else if (text == "ExactSpelling")
					{
						this.ExactSpelling = (bool)obj;
					}
					else if (text == "SetLastError")
					{
						this.SetLastError = (bool)obj;
					}
					else if (text == "PreserveSig")
					{
						flag = (bool)obj;
					}
					else if (text == "BestFitMapping")
					{
						this.BestFitMapping = (bool)obj;
					}
					else if (text == "ThrowOnUnmappableChar")
					{
						this.ThrowOnUnmappableChar = (bool)obj;
					}
				}
				this.attrs |= MethodAttributes.PinvokeImpl;
				if (flag)
				{
					this.iattrs |= MethodImplAttributes.PreserveSig;
				}
				return;
			}
		}

		/// <summary>Sets a custom attribute using a specified custom attribute blob.</summary>
		/// <param name="con">The constructor for the custom attribute. </param>
		/// <param name="binaryAttribute">A byte blob representing the attributes. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> or <paramref name="binaryAttribute" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.</exception>
		// Token: 0x06002782 RID: 10114 RVA: 0x0008C40D File Offset: 0x0008A60D
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

		/// <summary>Sets the implementation flags for this method.</summary>
		/// <param name="attributes">The implementation flags to set. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. </exception>
		// Token: 0x06002783 RID: 10115 RVA: 0x0008C43E File Offset: 0x0008A63E
		public void SetImplementationFlags(MethodImplAttributes attributes)
		{
			this.RejectIfCreated();
			this.iattrs = attributes;
		}

		/// <summary>Adds declarative security to this method.</summary>
		/// <param name="action">The security action to be taken (Demand, Assert, and so on). </param>
		/// <param name="pset">The set of permissions the action applies to. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="action" /> is invalid (RequestMinimum, RequestOptional, and RequestRefuse are invalid). </exception>
		/// <exception cref="T:System.InvalidOperationException">The containing type has been created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-The permission set <paramref name="pset" /> contains an action that was added earlier by <see cref="M:System.Reflection.Emit.MethodBuilder.AddDeclarativeSecurity(System.Security.Permissions.SecurityAction,System.Security.PermissionSet)" />.-or-For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="pset" /> is null. </exception>
		// Token: 0x06002784 RID: 10116 RVA: 0x0008C450 File Offset: 0x0008A650
		public void AddDeclarativeSecurity(SecurityAction action, PermissionSet pset)
		{
			if (pset == null)
			{
				throw new ArgumentNullException("pset");
			}
			if (action == SecurityAction.RequestMinimum || action == SecurityAction.RequestOptional || action == SecurityAction.RequestRefuse)
			{
				throw new ArgumentOutOfRangeException("Request* values are not permitted", "action");
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

		/// <summary>Sets marshaling information for the return type of this method.</summary>
		/// <param name="unmanagedMarshal">Marshaling information for the return type of this method. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or-For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false. </exception>
		// Token: 0x06002785 RID: 10117 RVA: 0x0008C530 File Offset: 0x0008A730
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
		public void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.RejectIfCreated();
			throw new NotImplementedException();
		}

		/// <summary>Set a symbolic custom attribute using a blob.</summary>
		/// <param name="name">The name of the symbolic custom attribute. </param>
		/// <param name="data">The byte blob that represents the value of the symbolic custom attribute. </param>
		/// <exception cref="T:System.InvalidOperationException">The containing type was previously created using <see cref="M:System.Reflection.Emit.TypeBuilder.CreateType" />.-or- The module that contains this method is not a debug module. -or-For the current method, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.</exception>
		// Token: 0x06002786 RID: 10118 RVA: 0x0008C530 File Offset: 0x0008A730
		[MonoTODO]
		public void SetSymCustomAttribute(string name, byte[] data)
		{
			this.RejectIfCreated();
			throw new NotImplementedException();
		}

		/// <summary>Returns this MethodBuilder instance as a string.</summary>
		/// <returns>Returns a string containing the name, attributes, method signature, exceptions, and local signature of this method followed by the current Microsoft intermediate language (MSIL) stream.</returns>
		// Token: 0x06002787 RID: 10119 RVA: 0x0008C53D File Offset: 0x0008A73D
		[SecuritySafeCritical]
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"MethodBuilder [",
				this.type.Name,
				"::",
				this.name,
				"]"
			});
		}

		/// <summary>Determines whether the given object is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of MethodBuilder and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this MethodBuilder instance. </param>
		// Token: 0x06002788 RID: 10120 RVA: 0x0008C579 File Offset: 0x0008A779
		[SecuritySafeCritical]
		[MonoTODO]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		/// <summary>Gets the hash code for this method.</summary>
		/// <returns>The hash code for this method.</returns>
		// Token: 0x06002789 RID: 10121 RVA: 0x0008C582 File Offset: 0x0008A782
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x0008C58F File Offset: 0x0008A78F
		internal override int get_next_table_index(object obj, int table, bool inc)
		{
			return this.type.get_next_table_index(obj, table, inc);
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x0008C5A0 File Offset: 0x0008A7A0
		private void ExtendArray<T>(ref T[] array, T elem)
		{
			if (array == null)
			{
				array = new T[1];
			}
			else
			{
				T[] array2 = new T[array.Length + 1];
				Array.Copy(array, array2, array.Length);
				array = array2;
			}
			array[array.Length - 1] = elem;
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x0008C5E4 File Offset: 0x0008A7E4
		internal void set_override(MethodInfo mdecl)
		{
			this.ExtendArray<MethodInfo>(ref this.override_methods, mdecl);
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x0008C5F3 File Offset: 0x0008A7F3
		private void RejectIfCreated()
		{
			if (this.type.is_created)
			{
				throw new InvalidOperationException("Type definition of the method is complete.");
			}
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x00085D62 File Offset: 0x00083F62
		private Exception NotSupported()
		{
			return new NotSupportedException("The invoked member is not supported in a dynamic module.");
		}

		/// <summary>Returns a generic method constructed from the current generic method definition using the specified generic type arguments.</summary>
		/// <returns>A <see cref="T:System.Reflection.MethodInfo" /> representing the generic method constructed from the current generic method definition using the specified generic type arguments.</returns>
		/// <param name="typeArguments">An array of <see cref="T:System.Type" /> objects that represent the type arguments for the generic method.</param>
		// Token: 0x0600278F RID: 10127 RVA: 0x0008C610 File Offset: 0x0008A810
		public override MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException("Method is not a generic method definition");
			}
			if (typeArguments == null)
			{
				throw new ArgumentNullException("typeArguments");
			}
			if (this.generic_params.Length != typeArguments.Length)
			{
				throw new ArgumentException("Incorrect length", "typeArguments");
			}
			for (int i = 0; i < typeArguments.Length; i++)
			{
				if (typeArguments[i] == null)
				{
					throw new ArgumentNullException("typeArguments");
				}
			}
			return new MethodOnTypeBuilderInst(this, typeArguments);
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Reflection.Emit.MethodBuilder" /> object represents the definition of a generic method.</summary>
		/// <returns>true if the current <see cref="T:System.Reflection.Emit.MethodBuilder" /> object represents the definition of a generic method; otherwise, false.</returns>
		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002790 RID: 10128 RVA: 0x0008C688 File Offset: 0x0008A888
		public override bool IsGenericMethodDefinition
		{
			get
			{
				return this.generic_params != null;
			}
		}

		/// <summary>Gets a value indicating whether the method is a generic method.</summary>
		/// <returns>true if the method is generic; otherwise, false.</returns>
		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x0008C688 File Offset: 0x0008A888
		public override bool IsGenericMethod
		{
			get
			{
				return this.generic_params != null;
			}
		}

		/// <summary>Returns this method.</summary>
		/// <returns>The current instance of <see cref="T:System.Reflection.Emit.MethodBuilder" />. </returns>
		/// <exception cref="T:System.InvalidOperationException">The current method is not generic. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property returns false.</exception>
		// Token: 0x06002792 RID: 10130 RVA: 0x0008C693 File Offset: 0x0008A893
		public override MethodInfo GetGenericMethodDefinition()
		{
			if (!this.IsGenericMethodDefinition)
			{
				throw new InvalidOperationException();
			}
			return this;
		}

		/// <summary>Returns an array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects that represent the type parameters of the method, if it is generic.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects representing the type parameters, if the method is generic, or null if the method is not generic. </returns>
		// Token: 0x06002793 RID: 10131 RVA: 0x0008C6A4 File Offset: 0x0008A8A4
		public override Type[] GetGenericArguments()
		{
			if (this.generic_params == null)
			{
				return null;
			}
			Type[] array = new Type[this.generic_params.Length];
			for (int i = 0; i < this.generic_params.Length; i++)
			{
				array[i] = this.generic_params[i];
			}
			return array;
		}

		/// <summary>Sets the number of generic type parameters for the current method, specifies their names, and returns an array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects that can be used to define their constraints.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.Emit.GenericTypeParameterBuilder" /> objects representing the type parameters of the generic method.</returns>
		/// <param name="names">An array of strings that represent the names of the generic type parameters.</param>
		/// <exception cref="T:System.InvalidOperationException">Generic type parameters have already been defined for this method.-or-The method has been completed already.-or-The <see cref="M:System.Reflection.Emit.MethodBuilder.SetImplementationFlags(System.Reflection.MethodImplAttributes)" /> method has been called for the current method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="names" /> is null.-or-An element of <paramref name="names" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="names" /> is an empty array.</exception>
		// Token: 0x06002794 RID: 10132 RVA: 0x0008C6E8 File Offset: 0x0008A8E8
		public GenericTypeParameterBuilder[] DefineGenericParameters(params string[] names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			if (names.Length == 0)
			{
				throw new ArgumentException("names");
			}
			this.generic_params = new GenericTypeParameterBuilder[names.Length];
			for (int i = 0; i < names.Length; i++)
			{
				string text = names[i];
				if (text == null)
				{
					throw new ArgumentNullException("names");
				}
				this.generic_params[i] = new GenericTypeParameterBuilder(this.type, this, text, i);
			}
			return this.generic_params;
		}

		/// <summary>Sets the return type of the method.</summary>
		/// <param name="returnType">A <see cref="T:System.Type" /> object that represents the return type of the method.</param>
		/// <exception cref="T:System.InvalidOperationException">The current method is generic, but is not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.</exception>
		// Token: 0x06002795 RID: 10133 RVA: 0x0008C75C File Offset: 0x0008A95C
		public void SetReturnType(Type returnType)
		{
			this.rtype = returnType;
		}

		/// <summary>Sets the number and types of parameters for a method. </summary>
		/// <param name="parameterTypes">An array of <see cref="T:System.Type" /> objects representing the parameter types.</param>
		/// <exception cref="T:System.InvalidOperationException">The current method is generic, but is not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.</exception>
		// Token: 0x06002796 RID: 10134 RVA: 0x0008C768 File Offset: 0x0008A968
		public void SetParameters(params Type[] parameterTypes)
		{
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
		}

		/// <summary>Sets the method signature, including the return type, the parameter types, and the required and optional custom modifiers of the return type and parameter types.</summary>
		/// <param name="returnType">The return type of the method.</param>
		/// <param name="returnTypeRequiredCustomModifiers">An array of types representing the required custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no required custom modifiers, specify null.</param>
		/// <param name="returnTypeOptionalCustomModifiers">An array of types representing the optional custom modifiers, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />, for the return type of the method. If the return type has no optional custom modifiers, specify null.</param>
		/// <param name="parameterTypes">The types of the parameters of the method.</param>
		/// <param name="parameterTypeRequiredCustomModifiers">An array of arrays of types. Each array of types represents the required custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no required custom modifiers, specify null instead of an array of types. If none of the parameters have required custom modifiers, specify null instead of an array of arrays.</param>
		/// <param name="parameterTypeOptionalCustomModifiers">An array of arrays of types. Each array of types represents the optional custom modifiers for the corresponding parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" />. If a particular parameter has no optional custom modifiers, specify null instead of an array of types. If none of the parameters have optional custom modifiers, specify null instead of an array of arrays.</param>
		/// <exception cref="T:System.InvalidOperationException">The current method is generic, but is not a generic method definition. That is, the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethod" /> property is true, but the <see cref="P:System.Reflection.Emit.MethodBuilder.IsGenericMethodDefinition" /> property is false.</exception>
		// Token: 0x06002797 RID: 10135 RVA: 0x0008C7BE File Offset: 0x0008A9BE
		public void SetSignature(Type returnType, Type[] returnTypeRequiredCustomModifiers, Type[] returnTypeOptionalCustomModifiers, Type[] parameterTypes, Type[][] parameterTypeRequiredCustomModifiers, Type[][] parameterTypeOptionalCustomModifiers)
		{
			this.SetReturnType(returnType);
			this.SetParameters(parameterTypes);
			this.returnModReq = returnTypeRequiredCustomModifiers;
			this.returnModOpt = returnTypeOptionalCustomModifiers;
			this.paramModReq = parameterTypeRequiredCustomModifiers;
			this.paramModOpt = parameterTypeOptionalCustomModifiers;
		}

		/// <summary>Gets the module in which the current method is being defined.</summary>
		/// <returns>The <see cref="T:System.Reflection.Module" /> in which the member represented by the current <see cref="T:System.Reflection.MemberInfo" /> is being defined.</returns>
		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06002798 RID: 10136 RVA: 0x0008C7ED File Offset: 0x0008A9ED
		public override Module Module
		{
			get
			{
				return this.GetModule();
			}
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array that receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002799 RID: 10137 RVA: 0x0002126B File Offset: 0x0001F46B
		void _MethodBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600279A RID: 10138 RVA: 0x0002126B File Offset: 0x0001F46B
		void _MethodBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600279B RID: 10139 RVA: 0x0002126B File Offset: 0x0001F46B
		void _MethodBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x0600279C RID: 10140 RVA: 0x0002126B File Offset: 0x0001F46B
		void _MethodBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Reflection.ParameterInfo" /> object that contains information about the return type of the method, such as whether the return type has custom modifiers. </summary>
		/// <returns>A <see cref="T:System.Reflection.ParameterInfo" /> object that contains information about the return type.</returns>
		/// <exception cref="T:System.InvalidOperationException">The declaring type has not been created.</exception>
		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x0600279D RID: 10141 RVA: 0x0008C7F5 File Offset: 0x0008A9F5
		public override ParameterInfo ReturnParameter
		{
			get
			{
				return base.ReturnParameter;
			}
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal MethodBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001462 RID: 5218
		private RuntimeMethodHandle mhandle;

		// Token: 0x04001463 RID: 5219
		private Type rtype;

		// Token: 0x04001464 RID: 5220
		internal Type[] parameters;

		// Token: 0x04001465 RID: 5221
		private MethodAttributes attrs;

		// Token: 0x04001466 RID: 5222
		private MethodImplAttributes iattrs;

		// Token: 0x04001467 RID: 5223
		private string name;

		// Token: 0x04001468 RID: 5224
		private int table_idx;

		// Token: 0x04001469 RID: 5225
		private byte[] code;

		// Token: 0x0400146A RID: 5226
		private ILGenerator ilgen;

		// Token: 0x0400146B RID: 5227
		private TypeBuilder type;

		// Token: 0x0400146C RID: 5228
		internal ParameterBuilder[] pinfo;

		// Token: 0x0400146D RID: 5229
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x0400146E RID: 5230
		private MethodInfo[] override_methods;

		// Token: 0x0400146F RID: 5231
		private string pi_dll;

		// Token: 0x04001470 RID: 5232
		private string pi_entry;

		// Token: 0x04001471 RID: 5233
		private CharSet charset;

		// Token: 0x04001472 RID: 5234
		private uint extra_flags;

		// Token: 0x04001473 RID: 5235
		private CallingConvention native_cc;

		// Token: 0x04001474 RID: 5236
		private CallingConventions call_conv;

		// Token: 0x04001475 RID: 5237
		private bool init_locals;

		// Token: 0x04001476 RID: 5238
		private IntPtr generic_container;

		// Token: 0x04001477 RID: 5239
		internal GenericTypeParameterBuilder[] generic_params;

		// Token: 0x04001478 RID: 5240
		private Type[] returnModReq;

		// Token: 0x04001479 RID: 5241
		private Type[] returnModOpt;

		// Token: 0x0400147A RID: 5242
		private Type[][] paramModReq;

		// Token: 0x0400147B RID: 5243
		private Type[][] paramModOpt;

		// Token: 0x0400147C RID: 5244
		private RefEmitPermissionSet[] permissions;
	}
}
