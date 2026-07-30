using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Identifies how to marshal parameters or fields to unmanaged code.</summary>
	// Token: 0x020008BE RID: 2238
	[ComVisible(true)]
	[Serializable]
	public enum UnmanagedType
	{
		/// <summary>A 4-byte Boolean value (true != 0, false = 0). This is the Win32 BOOL type.</summary>
		// Token: 0x04002C69 RID: 11369
		Bool = 2,
		/// <summary>A 1-byte signed integer. You can use this member to transform a Boolean value into a 1-byte, C-style bool (true = 1, false = 0).</summary>
		// Token: 0x04002C6A RID: 11370
		I1,
		/// <summary>A 1-byte unsigned integer.</summary>
		// Token: 0x04002C6B RID: 11371
		U1,
		/// <summary>A 2-byte signed integer.</summary>
		// Token: 0x04002C6C RID: 11372
		I2,
		/// <summary>A 2-byte unsigned integer.</summary>
		// Token: 0x04002C6D RID: 11373
		U2,
		/// <summary>A 4-byte signed integer.</summary>
		// Token: 0x04002C6E RID: 11374
		I4,
		/// <summary>A 4-byte unsigned integer.</summary>
		// Token: 0x04002C6F RID: 11375
		U4,
		/// <summary>An 8-byte signed integer.</summary>
		// Token: 0x04002C70 RID: 11376
		I8,
		/// <summary>An 8-byte unsigned integer.</summary>
		// Token: 0x04002C71 RID: 11377
		U8,
		/// <summary>A 4-byte floating-point number.</summary>
		// Token: 0x04002C72 RID: 11378
		R4,
		/// <summary>An 8-byte floating-point number.</summary>
		// Token: 0x04002C73 RID: 11379
		R8,
		/// <summary>A currency type. Used on a <see cref="T:System.Decimal" /> to marshal the decimal value as a COM currency type instead of as a Decimal.</summary>
		// Token: 0x04002C74 RID: 11380
		Currency = 15,
		/// <summary>A Unicode character string that is a length-prefixed double byte. You can use this member, which is the default string in COM, on the <see cref="T:System.String" /> data type.</summary>
		// Token: 0x04002C75 RID: 11381
		BStr = 19,
		/// <summary>A single byte, null-terminated ANSI character string. You can use this member on the <see cref="T:System.String" /> and <see cref="T:System.Text.StringBuilder" /> data types.</summary>
		// Token: 0x04002C76 RID: 11382
		LPStr,
		/// <summary>A 2-byte, null-terminated Unicode character string.</summary>
		// Token: 0x04002C77 RID: 11383
		LPWStr,
		/// <summary>A platform-dependent character string: ANSI on Windows 98, and Unicode on Windows NT and Windows XP. This value is supported only for platform invoke and not for COM interop, because exporting a string of type LPTStr is not supported.</summary>
		// Token: 0x04002C78 RID: 11384
		LPTStr,
		/// <summary>Used for in-line, fixed-length character arrays that appear within a structure. The character type used with <see cref="F:System.Runtime.InteropServices.UnmanagedType.ByValTStr" /> is determined by the <see cref="T:System.Runtime.InteropServices.CharSet" /> argument of the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> attribute applied to the containing structure. Always use the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeConst" /> field to indicate the size of the array.</summary>
		// Token: 0x04002C79 RID: 11385
		ByValTStr,
		/// <summary>A COM IUnknown pointer. You can use this member on the <see cref="T:System.Object" /> data type.</summary>
		// Token: 0x04002C7A RID: 11386
		IUnknown = 25,
		/// <summary>A COM IDispatch pointer (Object in Microsoft Visual Basic 6.0).</summary>
		// Token: 0x04002C7B RID: 11387
		IDispatch,
		/// <summary>A VARIANT, which is used to marshal managed formatted classes and value types.</summary>
		// Token: 0x04002C7C RID: 11388
		Struct,
		/// <summary>A COM interface pointer. The <see cref="T:System.Guid" /> of the interface is obtained from the class metadata. Use this member to specify the exact interface type or the default interface type if you apply it to a class. This member produces the same behavior as <see cref="F:System.Runtime.InteropServices.UnmanagedType.IUnknown" /> when you apply it to the <see cref="T:System.Object" /> data type.</summary>
		// Token: 0x04002C7D RID: 11389
		Interface,
		/// <summary>A SafeArray, which is a self-describing array that carries the type, rank, and bounds of the associated array data. You can use this member with the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SafeArraySubType" /> field to override the default element type.</summary>
		// Token: 0x04002C7E RID: 11390
		SafeArray,
		/// <summary>When the <see cref="P:System.Runtime.InteropServices.MarshalAsAttribute.Value" /> property is set to ByValArray, the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeConst" /> field must be set to indicate the number of elements in the array. The <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.ArraySubType" /> field can optionally contain the <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> of the array elements when it is necessary to differentiate among string types. You can use this <see cref="T:System.Runtime.InteropServices.UnmanagedType" /> only on an array that whose elements appear as fields in a structure.</summary>
		// Token: 0x04002C7F RID: 11391
		ByValArray,
		/// <summary>A platform-dependent, signed integer: 4 bytes on 32-bit Windows, 8 bytes on 64-bit Windows.</summary>
		// Token: 0x04002C80 RID: 11392
		SysInt,
		/// <summary>A platform-dependent, unsigned integer: 4 bytes on 32-bit Windows, 8 bytes on 64-bit Windows.</summary>
		// Token: 0x04002C81 RID: 11393
		SysUInt,
		/// <summary>A value that enables Visual Basic to change a string in unmanaged code and have the results reflected in managed code. This value is only supported for platform invoke.</summary>
		// Token: 0x04002C82 RID: 11394
		VBByRefStr = 34,
		/// <summary>An ANSI character string that is a length-prefixed single byte. You can use this member on the <see cref="T:System.String" /> data type.</summary>
		// Token: 0x04002C83 RID: 11395
		AnsiBStr,
		/// <summary>A length-prefixed, platform-dependent char string: ANSI on Windows 98, Unicode on Windows NT. You rarely use this BSTR-like member.</summary>
		// Token: 0x04002C84 RID: 11396
		TBStr,
		/// <summary>A 2-byte, OLE-defined VARIANT_BOOL type (true = -1, false = 0).</summary>
		// Token: 0x04002C85 RID: 11397
		VariantBool,
		/// <summary>An integer that can be used as a C-style function pointer. You can use this member on a <see cref="T:System.Delegate" /> data type or on a type that inherits from a <see cref="T:System.Delegate" />.</summary>
		// Token: 0x04002C86 RID: 11398
		FunctionPtr,
		/// <summary>A dynamic type that determines the type of an object at run time and marshals the object as that type. This member is valid for platform invoke methods only.</summary>
		// Token: 0x04002C87 RID: 11399
		AsAny = 40,
		/// <summary>A pointer to the first element of a C-style array. When marshaling from managed to unmanaged code, the length of the array is determined by the length of the managed array. When marshaling from unmanaged to managed code, the length of the array is determined from the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeConst" /> and <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.SizeParamIndex" /> fields, optionally followed by the unmanaged type of the elements within the array when it is necessary to differentiate among string types.</summary>
		// Token: 0x04002C88 RID: 11400
		LPArray = 42,
		/// <summary>A pointer to a C-style structure that you use to marshal managed formatted classes. This member is valid for platform invoke methods only.</summary>
		// Token: 0x04002C89 RID: 11401
		LPStruct,
		/// <summary>Specifies the custom marshaler class when used with the <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalType" /> or <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalTypeRef" /> field. The <see cref="F:System.Runtime.InteropServices.MarshalAsAttribute.MarshalCookie" /> field can be used to pass additional information to the custom marshaler. You can use this member on any reference type.</summary>
		// Token: 0x04002C8A RID: 11402
		CustomMarshaler,
		/// <summary>A native type that is associated with an <see cref="F:System.Runtime.InteropServices.UnmanagedType.I4" /> or an <see cref="F:System.Runtime.InteropServices.UnmanagedType.U4" /> and that causes the parameter to be exported as an HRESULT in the exported type library.</summary>
		// Token: 0x04002C8B RID: 11403
		Error,
		/// <summary>A Windows Runtime interface pointer. You can use this member on the <see cref="T:System.Object" /> data type.</summary>
		// Token: 0x04002C8C RID: 11404
		[ComVisible(false)]
		IInspectable,
		/// <summary>A Windows Runtime string. You can use this member on the <see cref="T:System.String" /> data type.</summary>
		// Token: 0x04002C8D RID: 11405
		[ComVisible(false)]
		HString,
		// Token: 0x04002C8E RID: 11406
		[ComVisible(false)]
		LPUTF8Str
	}
}
