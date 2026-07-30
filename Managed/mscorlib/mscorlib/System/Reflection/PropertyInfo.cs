using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Mono;

namespace System.Reflection
{
	/// <summary>Discovers the attributes of a property and provides access to property metadata.</summary>
	// Token: 0x0200033D RID: 829
	[ComDefaultInterface(typeof(_PropertyInfo))]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	[Serializable]
	public abstract class PropertyInfo : MemberInfo, _PropertyInfo
	{
		/// <summary>Gets the attributes for this property.</summary>
		/// <returns>Attributes of this property.</returns>
		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x0600247B RID: 9339
		public abstract PropertyAttributes Attributes { get; }

		/// <summary>Gets a value indicating whether the property can be read.</summary>
		/// <returns>true if this property can be read; otherwise, false.</returns>
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x0600247C RID: 9340
		public abstract bool CanRead { get; }

		/// <summary>Gets a value indicating whether the property can be written to.</summary>
		/// <returns>true if this property can be written to; otherwise, false.</returns>
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600247D RID: 9341
		public abstract bool CanWrite { get; }

		/// <summary>Gets the get accessor for this property.</summary>
		/// <returns>The get accessor for this property.</returns>
		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x0008409C File Offset: 0x0008229C
		public virtual MethodInfo GetMethod
		{
			get
			{
				return this.GetGetMethod(true);
			}
		}

		/// <summary>Gets the set accessor for this property.</summary>
		/// <returns>The set accessor for this property.</returns>
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x000840A5 File Offset: 0x000822A5
		public virtual MethodInfo SetMethod
		{
			get
			{
				return this.GetSetMethod(true);
			}
		}

		/// <summary>Gets a value indicating whether the property is the special name.</summary>
		/// <returns>true if this property is the special name; otherwise, false.</returns>
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06002480 RID: 9344 RVA: 0x000840AE File Offset: 0x000822AE
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & PropertyAttributes.SpecialName) > PropertyAttributes.None;
			}
		}

		/// <summary>Gets a <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a property.</summary>
		/// <returns>A <see cref="T:System.Reflection.MemberTypes" /> value indicating that this member is a property.</returns>
		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06002481 RID: 9345 RVA: 0x000393E2 File Offset: 0x000375E2
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Property;
			}
		}

		/// <summary>Gets the type of this property.</summary>
		/// <returns>The type of this property.</returns>
		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06002482 RID: 9346
		public abstract Type PropertyType { get; }

		/// <summary>Returns an array whose elements reflect the public get, set, and other accessors of the property reflected by the current instance.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects that reflect the public get, set, and other accessors of the property reflected by the current instance, if found; otherwise, this method returns an array with zero (0) elements.</returns>
		// Token: 0x06002484 RID: 9348 RVA: 0x000840BF File Offset: 0x000822BF
		public MethodInfo[] GetAccessors()
		{
			return this.GetAccessors(false);
		}

		/// <summary>Returns an array whose elements reflect the public and, if specified, non-public get, set, and other accessors of the property reflected by the current instance.</summary>
		/// <returns>An array of <see cref="T:System.Reflection.MethodInfo" /> objects whose elements reflect the get, set, and other accessors of the property reflected by the current instance. If <paramref name="nonPublic" /> is true, this array contains public and non-public get, set, and other accessors. If <paramref name="nonPublic" /> is false, this array contains only public get, set, and other accessors. If no accessors with the specified visibility are found, this method returns an array with zero (0) elements.</returns>
		/// <param name="nonPublic">Indicates whether non-public methods should be returned in the MethodInfo array. true if non-public methods are to be included; otherwise, false. </param>
		// Token: 0x06002485 RID: 9349
		public abstract MethodInfo[] GetAccessors(bool nonPublic);

		/// <summary>Returns the public get accessor for this property.</summary>
		/// <returns>A MethodInfo object representing the public get accessor for this property, or null if the get accessor is non-public or does not exist.</returns>
		// Token: 0x06002486 RID: 9350 RVA: 0x000840C8 File Offset: 0x000822C8
		public MethodInfo GetGetMethod()
		{
			return this.GetGetMethod(false);
		}

		/// <summary>When overridden in a derived class, returns the public or non-public get accessor for this property.</summary>
		/// <returns>A MethodInfo object representing the get accessor for this property, if <paramref name="nonPublic" /> is true. Returns null if <paramref name="nonPublic" /> is false and the get accessor is non-public, or if <paramref name="nonPublic" /> is true but no get accessors exist.</returns>
		/// <param name="nonPublic">Indicates whether a non-public get accessor should be returned. true if a non-public accessor is to be returned; otherwise, false. </param>
		/// <exception cref="T:System.Security.SecurityException">The requested method is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect on this non-public method. </exception>
		// Token: 0x06002487 RID: 9351
		public abstract MethodInfo GetGetMethod(bool nonPublic);

		/// <summary>When overridden in a derived class, returns an array of all the index parameters for the property.</summary>
		/// <returns>An array of type ParameterInfo containing the parameters for the indexes. If the property is not indexed, the array has 0 (zero) elements.</returns>
		// Token: 0x06002488 RID: 9352
		public abstract ParameterInfo[] GetIndexParameters();

		/// <summary>Returns the public set accessor for this property.</summary>
		/// <returns>The MethodInfo object representing the Set method for this property if the set accessor is public, or null if the set accessor is not public.</returns>
		// Token: 0x06002489 RID: 9353 RVA: 0x000840D1 File Offset: 0x000822D1
		public MethodInfo GetSetMethod()
		{
			return this.GetSetMethod(false);
		}

		/// <summary>When overridden in a derived class, returns the set accessor for this property.</summary>
		/// <returns>Value Condition A <see cref="T:System.Reflection.MethodInfo" /> object representing the Set method for this property. The set accessor is public.-or- <paramref name="nonPublic" /> is true and the set accessor is non-public. null<paramref name="nonPublic" /> is true, but the property is read-only.-or- <paramref name="nonPublic" /> is false and the set accessor is non-public.-or- There is no set accessor. </returns>
		/// <param name="nonPublic">Indicates whether the accessor should be returned if it is non-public. true if a non-public accessor is to be returned; otherwise, false. </param>
		/// <exception cref="T:System.Security.SecurityException">The requested method is non-public and the caller does not have <see cref="T:System.Security.Permissions.ReflectionPermission" /> to reflect on this non-public method. </exception>
		// Token: 0x0600248A RID: 9354
		public abstract MethodInfo GetSetMethod(bool nonPublic);

		/// <summary>Returns the property value of a specified object with optional index values for indexed properties.</summary>
		/// <returns>The property value of the specified object.</returns>
		/// <param name="obj">The object whose property value will be returned. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.-or- The property's get accessor is not found. </exception>
		/// <exception cref="T:System.Reflection.TargetException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes. </exception>
		/// <exception cref="T:System.MethodAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.There was an illegal attempt to access a private or protected method inside a class. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while retrieving the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x0600248B RID: 9355 RVA: 0x000840DA File Offset: 0x000822DA
		[DebuggerHidden]
		[DebuggerStepThrough]
		public virtual object GetValue(object obj, object[] index)
		{
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		/// <summary>Returns the property value of a specified object.</summary>
		/// <returns>The property value of the specified object.</returns>
		/// <param name="obj">The object whose property value will be returned.</param>
		// Token: 0x0600248C RID: 9356 RVA: 0x000840E7 File Offset: 0x000822E7
		[DebuggerStepThrough]
		[DebuggerHidden]
		public object GetValue(object obj)
		{
			return this.GetValue(obj, BindingFlags.Default, null, null, null);
		}

		/// <summary>When overridden in a derived class, returns the property value of a specified object that has the specified binding, index, and culture-specific information.</summary>
		/// <returns>The property value of the specified object.</returns>
		/// <param name="obj">The object whose property value will be returned. </param>
		/// <param name="invokeAttr">A bitwise combination of the following enumeration members that specify the invocation attribute: InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty, and SetProperty. You must specify a suitable invocation attribute. For example, to invoke a static member, set the Static flag. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <param name="culture">The culture for which the resource is to be localized. If the resource is not localized for this culture, the <see cref="P:System.Globalization.CultureInfo.Parent" /> property will be called successively in search of a match. If this value is null, the culture-specific information is obtained from the <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> property. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.-or- The property's get accessor is not found. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes. </exception>
		/// <exception cref="T:System.MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while retrieving the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x0600248D RID: 9357
		public abstract object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		/// <summary>Sets the property value of a specified object with optional index values for index properties.</summary>
		/// <param name="obj">The object whose property value will be set. </param>
		/// <param name="value">The new property value. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.-or- The property's set accessor is not found. </exception>
		/// <exception cref="T:System.Reflection.TargetException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch <see cref="T:System.Exception" /> instead.The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes. </exception>
		/// <exception cref="T:System.MethodAccessException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.MemberAccessException" />, instead.There was an illegal attempt to access a private or protected method inside a class. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x0600248E RID: 9358 RVA: 0x000840F4 File Offset: 0x000822F4
		[DebuggerStepThrough]
		[DebuggerHidden]
		public virtual void SetValue(object obj, object value, object[] index)
		{
			this.SetValue(obj, value, BindingFlags.Default, null, index, null);
		}

		/// <summary>Sets the property value of a specified object.</summary>
		/// <param name="obj">The object whose property value will be set.</param>
		/// <param name="value">The new property value.</param>
		// Token: 0x0600248F RID: 9359 RVA: 0x00084102 File Offset: 0x00082302
		[DebuggerStepThrough]
		[DebuggerHidden]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, BindingFlags.Default, null, null, null);
		}

		/// <summary>When overridden in a derived class, sets the property value for a specified object that has the specified binding, index, and culture-specific information.</summary>
		/// <param name="obj">The object whose property value will be set. </param>
		/// <param name="value">The new property value. </param>
		/// <param name="invokeAttr">A bitwise combination of the following enumeration members that specify the invocation attribute: InvokeMethod, CreateInstance, Static, GetField, SetField, GetProperty, or SetProperty. You must specify a suitable invocation attribute. For example, to invoke a static member, set the Static flag. </param>
		/// <param name="binder">An object that enables the binding, coercion of argument types, invocation of members, and retrieval of <see cref="T:System.Reflection.MemberInfo" /> objects through reflection. If <paramref name="binder" /> is null, the default binder is used. </param>
		/// <param name="index">Optional index values for indexed properties. This value should be null for non-indexed properties. </param>
		/// <param name="culture">The culture for which the resource is to be localized. If the resource is not localized for this culture, the <see cref="P:System.Globalization.CultureInfo.Parent" /> property will be called successively in search of a match. If this value is null, the culture-specific information is obtained from the <see cref="P:System.Globalization.CultureInfo.CurrentUICulture" /> property. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="index" /> array does not contain the type of arguments needed.-or- The property's set accessor is not found. </exception>
		/// <exception cref="T:System.Reflection.TargetException">The object does not match the target type, or a property is an instance property but <paramref name="obj" /> is null. </exception>
		/// <exception cref="T:System.Reflection.TargetParameterCountException">The number of parameters in <paramref name="index" /> does not match the number of parameters the indexed property takes. </exception>
		/// <exception cref="T:System.MethodAccessException">There was an illegal attempt to access a private or protected method inside a class. </exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">An error occurred while setting the property value. For example, an index value specified for an indexed property is out of range. The <see cref="P:System.Exception.InnerException" /> property indicates the reason for the error.</exception>
		// Token: 0x06002490 RID: 9360
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		/// <summary>Returns an array of types representing the optional custom modifiers of the property.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the optional custom modifiers of the current property, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x06002491 RID: 9361 RVA: 0x00084110 File Offset: 0x00082310
		public virtual Type[] GetOptionalCustomModifiers()
		{
			return Type.EmptyTypes;
		}

		/// <summary>Returns an array of types representing the required custom modifiers of the property.</summary>
		/// <returns>An array of <see cref="T:System.Type" /> objects that identify the required custom modifiers of the current property, such as <see cref="T:System.Runtime.CompilerServices.IsConst" /> or <see cref="T:System.Runtime.CompilerServices.IsImplicitlyDereferenced" />.</returns>
		// Token: 0x06002492 RID: 9362 RVA: 0x00084110 File Offset: 0x00082310
		public virtual Type[] GetRequiredCustomModifiers()
		{
			return Type.EmptyTypes;
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x00084117 File Offset: 0x00082317
		private static NotImplementedException CreateNIE()
		{
			return new NotImplementedException();
		}

		/// <summary>Returns a literal value associated with the property by a compiler. </summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the literal value associated with the property. If the literal value is a class type with an element value of zero, the return value is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current property.</exception>
		/// <exception cref="T:System.FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification, Metadata. </exception>
		// Token: 0x06002494 RID: 9364 RVA: 0x0008411E File Offset: 0x0008231E
		public virtual object GetConstantValue()
		{
			throw PropertyInfo.CreateNIE();
		}

		/// <summary>Returns a literal value associated with the property by a compiler. </summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the literal value associated with the property. If the literal value is a class type with an element value of zero, the return value is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The Constant table in unmanaged metadata does not contain a constant value for the current property.</exception>
		/// <exception cref="T:System.FormatException">The type of the value is not one of the types permitted by the Common Language Specification (CLS). See the ECMA Partition II specification, Metadata Logical Format: Other Structures, Element Types used in Signatures. </exception>
		// Token: 0x06002495 RID: 9365 RVA: 0x0008411E File Offset: 0x0008231E
		public virtual object GetRawConstantValue()
		{
			throw PropertyInfo.CreateNIE();
		}

		/// <summary>Returns a value that indicates whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An object to compare with this instance, or null.</param>
		// Token: 0x06002496 RID: 9366 RVA: 0x0004BD58 File Offset: 0x00049F58
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06002497 RID: 9367 RVA: 0x0007E911 File Offset: 0x0007CB11
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.PropertyInfo" /> objects are equal.</summary>
		/// <returns>true if <paramref name="left" /> is equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002498 RID: 9368 RVA: 0x00080329 File Offset: 0x0007E529
		public static bool operator ==(PropertyInfo left, PropertyInfo right)
		{
			return left == right || (!((left == null) ^ (right == null)) && left.Equals(right));
		}

		/// <summary>Indicates whether two <see cref="T:System.Reflection.PropertyInfo" /> objects are not equal.</summary>
		/// <returns>true if <paramref name="left" /> is not equal to <paramref name="right" />; otherwise, false.</returns>
		/// <param name="left">The first object to compare.</param>
		/// <param name="right">The second object to compare.</param>
		// Token: 0x06002499 RID: 9369 RVA: 0x00080345 File Offset: 0x0007E545
		public static bool operator !=(PropertyInfo left, PropertyInfo right)
		{
			return left != right && (((left == null) ^ (right == null)) || !left.Equals(right));
		}

		/// <summary>Maps a set of names to a corresponding set of dispatch identifiers.</summary>
		/// <param name="riid">Reserved for future use. Must be IID_NULL.</param>
		/// <param name="rgszNames">Passed-in array of names to be mapped.</param>
		/// <param name="cNames">Count of the names to be mapped.</param>
		/// <param name="lcid">The locale context in which to interpret the names.</param>
		/// <param name="rgDispId">Caller-allocated array which receives the IDs corresponding to the names.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600249A RID: 9370 RVA: 0x0002126B File Offset: 0x0001F46B
		void _PropertyInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.PropertyInfo" /> type.</summary>
		/// <returns>A <see cref="T:System.Type" /> object representing the <see cref="T:System.Reflection.PropertyInfo" /> type.</returns>
		// Token: 0x0600249B RID: 9371 RVA: 0x00033A19 File Offset: 0x00031C19
		Type _PropertyInfo.GetType()
		{
			return base.GetType();
		}

		/// <summary>Retrieves the type information for an object, which can then be used to get the type information for an interface.</summary>
		/// <param name="iTInfo">The type information to return.</param>
		/// <param name="lcid">The locale identifier for the type information.</param>
		/// <param name="ppTInfo">Receives a pointer to the requested type information object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600249C RID: 9372 RVA: 0x0002126B File Offset: 0x0001F46B
		void _PropertyInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the number of type information interfaces that an object provides (either 0 or 1).</summary>
		/// <param name="pcTInfo">Points to a location that receives the number of type information interfaces provided by the object.</param>
		/// <exception cref="T:System.NotImplementedException">Late-bound access using the COM IDispatch interface is not supported.</exception>
		// Token: 0x0600249D RID: 9373 RVA: 0x0002126B File Offset: 0x0001F46B
		void _PropertyInfo.GetTypeInfoCount(out uint pcTInfo)
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
		// Token: 0x0600249E RID: 9374 RVA: 0x0002126B File Offset: 0x0001F46B
		void _PropertyInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600249F RID: 9375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern PropertyInfo internal_from_handle_type(IntPtr event_handle, IntPtr type_handle);

		// Token: 0x060024A0 RID: 9376 RVA: 0x00084128 File Offset: 0x00082328
		internal static PropertyInfo GetPropertyFromHandle(RuntimePropertyHandle handle, RuntimeTypeHandle reflectedType)
		{
			if (handle.Value == IntPtr.Zero)
			{
				throw new ArgumentException("The handle is invalid.");
			}
			PropertyInfo propertyInfo = PropertyInfo.internal_from_handle_type(handle.Value, reflectedType.Value);
			if (propertyInfo == null)
			{
				throw new ArgumentException("The property handle and the type handle are incompatible.");
			}
			return propertyInfo;
		}
	}
}
