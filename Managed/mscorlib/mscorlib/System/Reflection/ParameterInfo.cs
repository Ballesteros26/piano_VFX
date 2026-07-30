using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a parameter and provides access to parameter metadata.</summary>
	// Token: 0x0200033B RID: 827
	[ComDefaultInterface(typeof(_ParameterInfo))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class ParameterInfo : ICustomAttributeProvider, _ParameterInfo, IObjectReference
	{
		/// <summary>Initializes a new instance of the ParameterInfo class.</summary>
		// Token: 0x06002456 RID: 9302 RVA: 0x00002111 File Offset: 0x00000311
		protected ParameterInfo()
		{
		}

		/// <summary>Gets the parameter type and name represented as a string.</summary>
		/// <returns>A string containing the type and the name of the parameter.</returns>
		// Token: 0x06002457 RID: 9303 RVA: 0x00083E04 File Offset: 0x00082004
		public override string ToString()
		{
			Type type = this.ClassImpl;
			while (type.HasElementType)
			{
				type = type.GetElementType();
			}
			string text = ((type.IsPrimitive || this.ClassImpl == typeof(void) || this.ClassImpl.Namespace == this.MemberImpl.DeclaringType.Namespace) ? this.ClassImpl.Name : this.ClassImpl.FullName);
			if (!this.IsRetval)
			{
				text += " ";
				text += this.NameImpl;
			}
			return text;
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x00083EA8 File Offset: 0x000820A8
		internal static void FormatParameters(StringBuilder sb, ParameterInfo[] p, CallingConventions callingConvention, bool serialization)
		{
			for (int i = 0; i < p.Length; i++)
			{
				if (i > 0)
				{
					sb.Append(", ");
				}
				Type parameterType = p[i].ParameterType;
				string text = parameterType.FormatTypeName(serialization);
				if (parameterType.IsByRef && !serialization)
				{
					sb.Append(text.TrimEnd(new char[] { '&' }));
					sb.Append(" ByRef");
				}
				else
				{
					sb.Append(text);
				}
			}
			if ((callingConvention & CallingConventions.VarArgs) != (CallingConventions)0)
			{
				if (p.Length != 0)
				{
					sb.Append(", ");
				}
				sb.Append("...");
			}
		}

		/// <summary>Gets the Type of this parameter.</summary>
		/// <returns>The Type object that represents the Type of this parameter.</returns>
		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x06002459 RID: 9305 RVA: 0x00083F3C File Offset: 0x0008213C
		public virtual Type ParameterType
		{
			get
			{
				return this.ClassImpl;
			}
		}

		/// <summary>Gets the attributes for this parameter.</summary>
		/// <returns>A ParameterAttributes object representing the attributes for this parameter.</returns>
		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x00083F44 File Offset: 0x00082144
		public virtual ParameterAttributes Attributes
		{
			get
			{
				return this.AttrsImpl;
			}
		}

		/// <summary>Gets a value indicating whether this is an input parameter.</summary>
		/// <returns>true if the parameter is an input parameter; otherwise, false.</returns>
		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x0600245B RID: 9307 RVA: 0x00083F4C File Offset: 0x0008214C
		public bool IsIn
		{
			get
			{
				return (this.Attributes & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this parameter is a locale identifier (lcid).</summary>
		/// <returns>true if the parameter is a locale identifier; otherwise, false.</returns>
		// Token: 0x17000593 RID: 1427
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x00083F59 File Offset: 0x00082159
		public bool IsLcid
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Lcid) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this parameter is optional.</summary>
		/// <returns>true if the parameter is optional; otherwise, false.</returns>
		// Token: 0x17000594 RID: 1428
		// (get) Token: 0x0600245D RID: 9309 RVA: 0x00083F66 File Offset: 0x00082166
		public bool IsOptional
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this is an output parameter.</summary>
		/// <returns>true if the parameter is an output parameter; otherwise, false.</returns>
		// Token: 0x17000595 RID: 1429
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x00083F74 File Offset: 0x00082174
		public bool IsOut
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating whether this is a Retval parameter.</summary>
		/// <returns>true if the parameter is a Retval; otherwise, false.</returns>
		// Token: 0x17000596 RID: 1430
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x00083F81 File Offset: 0x00082181
		public bool IsRetval
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Retval) > ParameterAttributes.None;
			}
		}

		/// <summary>Gets a value indicating the member in which the parameter is implemented.</summary>
		/// <returns>The member which implanted the parameter represented by this <see cref="T:System.Reflection.ParameterInfo" />.</returns>
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x00083F8E File Offset: 0x0008218E
		public virtual MemberInfo Member
		{
			get
			{
				return this.MemberImpl;
			}
		}

		/// <summary>Gets the name of the parameter.</summary>
		/// <returns>The simple name of this parameter.</returns>
		// Token: 0x17000598 RID: 1432
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x00083F96 File Offset: 0x00082196
		public virtual string Name
		{
			get
			{
				return this.NameImpl;
			}
		}

		/// <summary>Gets the zero-based position of the parameter in the formal parameter list.</summary>
		/// <returns>An integer representing the position this parameter occupies in the parameter list.</returns>
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x00083F9E File Offset: 0x0008219E
		public virtual int Position
		{
			get
			{
				return this.PositionImpl;
			}
		}

		// Token: 0x06002463 RID: 9315
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern int GetMetadataToken();

		// Token: 0x06002464 RID: 9316 RVA: 0x00083FA8 File Offset: 0x000821A8
		internal object[] GetPseudoCustomAttributes()
		{
			int num = 0;
			if (this.IsIn)
			{
				num++;
			}
			if (this.IsOut)
			{
				num++;
			}
			if (this.IsOptional)
			{
				num++;
			}
			if (this.marshalAs != null)
			{
				num++;
			}
			if (num == 0)
			{
				return null;
			}
			object[] array = new object[num];
			num = 0;
			if (this.IsIn)
			{
				array[num++] = new InAttribute();
			}
			if (this.IsOptional)
			{
				array[num++] = new OptionalAttribute();
			}
			if (this.IsOut)
			{
				array[num++] = new OutAttribute();
			}
			if (this.marshalAs != null)
			{
				array[num++] = this.marshalAs.Copy();
			}
			return array;
		}

		// Token: 0x06002465 RID: 9317
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Type[] GetTypeModifiers(bool optional);

		// Token: 0x06002466 RID: 9318 RVA: 0x0008404C File Offset: 0x0008224C
		internal object GetDefaultValueImpl()
		{
			return this.DefaultValueImpl;
		}

		/// <summary>Gets a collection that contains this parameter's custom attributes.</summary>
		/// <returns>A collection that contains this parameter's custom attributes.</returns>
		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x00084054 File Offset: 0x00082254
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		/// <summary>Gets a value that indicates whether this parameter has a default value.</summary>
		/// <returns>true if this parameter has a default value; otherwise, false.</returns>
		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06002468 RID: 9320 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual bool HasDefaultValue
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x06002469 RID: 9321 RVA: 0x0002126B File Offset: 0x0001F46B
		void _ParameterInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600246A RID: 9322 RVA: 0x0002126B File Offset: 0x0001F46B
		void _ParameterInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600246B RID: 9323 RVA: 0x0002126B File Offset: 0x0001F46B
		void _ParameterInfo.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x0600246C RID: 9324 RVA: 0x0002126B File Offset: 0x0001F46B
		void _ParameterInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating the default value if the parameter has a default value.</summary>
		/// <returns>The default value of the parameter, or <see cref="F:System.DBNull.Value" /> if the parameter has no default value.</returns>
		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual object DefaultValue
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating the default value if the parameter has a default value.</summary>
		/// <returns>The default value of the parameter, or <see cref="F:System.DBNull.Value" /> if the parameter has no default value.</returns>
		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x0600246E RID: 9326 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual object RawDefaultValue
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that identifies this parameter in metadata.</summary>
		/// <returns>A value which, in combination with the module, uniquely identifies this parameter in metadata.</returns>
		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x0008405C File Offset: 0x0008225C
		public virtual int MetadataToken
		{
			get
			{
				return 134217728;
			}
		}

		/// <summary>Gets all the custom attributes defined on this parameter.</summary>
		/// <returns>An array that contains all the custom attributes applied to this parameter.</returns>
		/// <param name="inherit">This argument is ignored for objects of this type. See Remarks.</param>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type could not be loaded. </exception>
		// Token: 0x06002470 RID: 9328 RVA: 0x00084063 File Offset: 0x00082263
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return new object[0];
		}

		/// <summary>Gets the custom attributes of the specified type or its derived types that are applied to this parameter.</summary>
		/// <returns>An array that contains the custom attributes of the specified type or its derived types.</returns>
		/// <param name="attributeType">The custom attributes identified by type. </param>
		/// <param name="inherit">This argument is ignored for objects of this type. See Remarks.</param>
		/// <exception cref="T:System.ArgumentException">The type must be a type provided by the underlying runtime system.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null.</exception>
		/// <exception cref="T:System.TypeLoadException">A custom attribute type could not be loaded. </exception>
		// Token: 0x06002471 RID: 9329 RVA: 0x00084063 File Offset: 0x00082263
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return new object[0];
		}

		/// <summary>Returns the real object that should be deserialized instead of the object that the serialized stream specifies.</summary>
		/// <returns>The actual object that is put into the graph.</returns>
		/// <param name="context">The serialized stream from which the current object is deserialized.</param>
		/// <exception cref="T:System.Runtime.Serialization.SerializationException">The parameter's position in the parameter list of its associated member is not valid for that member's type.</exception>
		// Token: 0x06002472 RID: 9330 RVA: 0x0002126B File Offset: 0x0001F46B
		[SecurityCritical]
		public object GetRealObject(StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the custom attribute of the specified type or its derived types is applied to this parameter.</summary>
		/// <returns>true if one or more instances of <paramref name="attributeType" /> or its derived types are applied to this parameter; otherwise, false.</returns>
		/// <param name="attributeType">The Type object to search for. </param>
		/// <param name="inherit">This argument is ignored for objects of this type. See Remarks.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="attributeType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="attributeType" /> is not a <see cref="T:System.Type" /> object supplied by the common language runtime.</exception>
		// Token: 0x06002473 RID: 9331 RVA: 0x00015ED5 File Offset: 0x000140D5
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			return false;
		}

		/// <summary>Gets the required custom modifiers of the parameter.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the required custom modifiers of the current parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x06002474 RID: 9332 RVA: 0x0008406B File Offset: 0x0008226B
		public virtual Type[] GetRequiredCustomModifiers()
		{
			return new Type[0];
		}

		/// <summary>Gets the optional custom modifiers of the parameter.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the optional custom modifiers of the current parameter, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x06002475 RID: 9333 RVA: 0x0008406B File Offset: 0x0008226B
		public virtual Type[] GetOptionalCustomModifiers()
		{
			return new Type[0];
		}

		/// <summary>Returns a list of <see cref="T:System.Reflection.CustomAttributeData" /> objects for the current parameter, which can be used in the reflection-only context.</summary>
		/// <returns>A generic list of <see cref="T:System.Reflection.CustomAttributeData" /> objects representing data about the attributes that have been applied to the current parameter.</returns>
		// Token: 0x06002476 RID: 9334 RVA: 0x0002126B File Offset: 0x0001F46B
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x00084073 File Offset: 0x00082273
		internal static ParameterInfo New(ParameterBuilder pb, Type type, MemberInfo member, int position)
		{
			return new MonoParameterInfo(pb, type, member, position);
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x0008407E File Offset: 0x0008227E
		internal static ParameterInfo New(ParameterInfo pinfo, Type type, MemberInfo member, int position)
		{
			return new MonoParameterInfo(pinfo, type, member, position);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x00084089 File Offset: 0x00082289
		internal static ParameterInfo New(ParameterInfo pinfo, MemberInfo member)
		{
			return new MonoParameterInfo(pinfo, member);
		}

		// Token: 0x0600247A RID: 9338 RVA: 0x00084092 File Offset: 0x00082292
		internal static ParameterInfo New(Type type, MemberInfo member, MarshalAsAttribute marshalAs)
		{
			return new MonoParameterInfo(type, member, marshalAs);
		}

		/// <summary>The Type of the parameter.</summary>
		// Token: 0x04001364 RID: 4964
		protected Type ClassImpl;

		/// <summary>The default value of the parameter.</summary>
		// Token: 0x04001365 RID: 4965
		protected object DefaultValueImpl;

		/// <summary>The member in which the field is implemented.</summary>
		// Token: 0x04001366 RID: 4966
		protected MemberInfo MemberImpl;

		/// <summary>The name of the parameter.</summary>
		// Token: 0x04001367 RID: 4967
		protected string NameImpl;

		/// <summary>The zero-based position of the parameter in the parameter list.</summary>
		// Token: 0x04001368 RID: 4968
		protected int PositionImpl;

		/// <summary>The attributes of the parameter.</summary>
		// Token: 0x04001369 RID: 4969
		protected ParameterAttributes AttrsImpl;

		// Token: 0x0400136A RID: 4970
		internal MarshalAsAttribute marshalAs;
	}
}
