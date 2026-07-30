using System;
using System.Runtime.InteropServices;
using Unity;

namespace System.Reflection.Emit
{
	/// <summary>Creates or associates parameter information.</summary>
	// Token: 0x02000376 RID: 886
	[ComVisible(true)]
	[ComDefaultInterface(typeof(_ParameterBuilder))]
	[ClassInterface(ClassInterfaceType.None)]
	[StructLayout(LayoutKind.Sequential)]
	public class ParameterBuilder : _ParameterBuilder
	{
		// Token: 0x06002855 RID: 10325 RVA: 0x0009058C File Offset: 0x0008E78C
		internal ParameterBuilder(MethodBase mb, int pos, ParameterAttributes attributes, string strParamName)
		{
			this.name = strParamName;
			this.position = pos;
			this.attrs = attributes;
			this.methodb = mb;
			if (mb is DynamicMethod)
			{
				this.table_idx = 0;
				return;
			}
			this.table_idx = mb.get_next_table_index(this, 8, true);
		}

		/// <summary>Retrieves the attributes for this parameter.</summary>
		/// <returns>Read-only. Retrieves the attributes for this parameter.</returns>
		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06002856 RID: 10326 RVA: 0x000905DB File Offset: 0x0008E7DB
		public virtual int Attributes
		{
			get
			{
				return (int)this.attrs;
			}
		}

		/// <summary>Retrieves whether this is an input parameter.</summary>
		/// <returns>Read-only. Retrieves whether this is an input parameter.</returns>
		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06002857 RID: 10327 RVA: 0x000905E3 File Offset: 0x0008E7E3
		public bool IsIn
		{
			get
			{
				return (this.attrs & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		/// <summary>Retrieves whether this parameter is an output parameter.</summary>
		/// <returns>Read-only. Retrieves whether this parameter is an output parameter.</returns>
		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06002858 RID: 10328 RVA: 0x000905F0 File Offset: 0x0008E7F0
		public bool IsOut
		{
			get
			{
				return (this.attrs & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		/// <summary>Retrieves whether this parameter is optional.</summary>
		/// <returns>Read-only. Specifies whether this parameter is optional.</returns>
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06002859 RID: 10329 RVA: 0x000905FD File Offset: 0x0008E7FD
		public bool IsOptional
		{
			get
			{
				return (this.attrs & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		/// <summary>Retrieves the name of this parameter.</summary>
		/// <returns>Read-only. Retrieves the name of this parameter.</returns>
		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x0600285A RID: 10330 RVA: 0x0009060B File Offset: 0x0008E80B
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Retrieves the signature position for this parameter.</summary>
		/// <returns>Read-only. Retrieves the signature position for this parameter.</returns>
		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x0600285B RID: 10331 RVA: 0x00090613 File Offset: 0x0008E813
		public virtual int Position
		{
			get
			{
				return this.position;
			}
		}

		/// <summary>Retrieves the token for this parameter.</summary>
		/// <returns>Returns the token for this parameter.</returns>
		// Token: 0x0600285C RID: 10332 RVA: 0x0009061B File Offset: 0x0008E81B
		public virtual ParameterToken GetToken()
		{
			return new ParameterToken(8 | this.table_idx);
		}

		/// <summary>Sets the default value of the parameter.</summary>
		/// <param name="defaultValue">The default value of this parameter. </param>
		/// <exception cref="T:System.ArgumentException">The parameter is not one of the supported types.-or-The type of <paramref name="defaultValue" /> does not match the type of the parameter.-or-The parameter is of type <see cref="T:System.Object" /> or other reference type, <paramref name="defaultValue" /> is not null, and the value cannot be assigned to the reference type. </exception>
		// Token: 0x0600285D RID: 10333 RVA: 0x0009062C File Offset: 0x0008E82C
		public virtual void SetConstant(object defaultValue)
		{
			if (this.position > 0)
			{
				Type parameterType = this.methodb.GetParameterType(this.position - 1);
				if (defaultValue != null && parameterType != defaultValue.GetType() && (!parameterType.IsEnum || parameterType.UnderlyingSystemType != defaultValue.GetType()))
				{
					throw new ArgumentException("Constant does not match the defined type.");
				}
				if (parameterType.IsValueType && !parameterType.IsPrimitive && !parameterType.IsEnum && parameterType != typeof(DateTime))
				{
					throw new ArgumentException(parameterType + " is not a supported constant type.");
				}
			}
			this.def_value = defaultValue;
			this.attrs |= ParameterAttributes.HasDefault;
		}

		/// <summary>Set a custom attribute using a custom attribute builder.</summary>
		/// <param name="customBuilder">An instance of a helper class to define the custom attribute. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="con" /> is null. </exception>
		// Token: 0x0600285E RID: 10334 RVA: 0x000906E4 File Offset: 0x0008E8E4
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			string fullName = customBuilder.Ctor.ReflectedType.FullName;
			if (fullName == "System.Runtime.InteropServices.InAttribute")
			{
				this.attrs |= ParameterAttributes.In;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.OutAttribute")
			{
				this.attrs |= ParameterAttributes.Out;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.OptionalAttribute")
			{
				this.attrs |= ParameterAttributes.Optional;
				return;
			}
			if (fullName == "System.Runtime.InteropServices.MarshalAsAttribute")
			{
				this.attrs |= ParameterAttributes.HasFieldMarshal;
				this.marshal_info = CustomAttributeBuilder.get_umarshal(customBuilder, false);
				return;
			}
			if (fullName == "System.Runtime.InteropServices.DefaultParameterValueAttribute")
			{
				CustomAttributeBuilder.CustomAttributeInfo customAttributeInfo = CustomAttributeBuilder.decode_cattr(customBuilder);
				this.SetConstant(customAttributeInfo.ctorArgs[0]);
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
		// Token: 0x0600285F RID: 10335 RVA: 0x000907F4 File Offset: 0x0008E9F4
		[ComVisible(true)]
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			this.SetCustomAttribute(new CustomAttributeBuilder(con, binaryAttribute));
		}

		/// <summary>Specifies the marshaling for this parameter.</summary>
		/// <param name="unmanagedMarshal">The marshaling information for this parameter. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unmanagedMarshal" /> is null. </exception>
		// Token: 0x06002860 RID: 10336 RVA: 0x00090803 File Offset: 0x0008EA03
		[Obsolete("An alternate API is available: Emit the MarshalAs custom attribute instead.")]
		public virtual void SetMarshal(UnmanagedMarshal unmanagedMarshal)
		{
			this.marshal_info = unmanagedMarshal;
			this.attrs |= ParameterAttributes.HasFieldMarshal;
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002861 RID: 10337 RVA: 0x0002126B File Offset: 0x0001F46B
		void _ParameterBuilder.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002862 RID: 10338 RVA: 0x0002126B File Offset: 0x0001F46B
		void _ParameterBuilder.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002863 RID: 10339 RVA: 0x0002126B File Offset: 0x0001F46B
		void _ParameterBuilder.GetTypeInfoCount(out uint pcTInfo)
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
		/// <exception cref="T:System.NotImplementedException">The method is called late-bound using the COM IDispatch interface.</exception>
		// Token: 0x06002864 RID: 10340 RVA: 0x0002126B File Offset: 0x0001F46B
		void _ParameterBuilder.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002865 RID: 10341 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal ParameterBuilder()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040015B6 RID: 5558
		private MethodBase methodb;

		// Token: 0x040015B7 RID: 5559
		private string name;

		// Token: 0x040015B8 RID: 5560
		private CustomAttributeBuilder[] cattrs;

		// Token: 0x040015B9 RID: 5561
		private UnmanagedMarshal marshal_info;

		// Token: 0x040015BA RID: 5562
		private ParameterAttributes attrs;

		// Token: 0x040015BB RID: 5563
		private int position;

		// Token: 0x040015BC RID: 5564
		private int table_idx;

		// Token: 0x040015BD RID: 5565
		private object def_value;
	}
}
