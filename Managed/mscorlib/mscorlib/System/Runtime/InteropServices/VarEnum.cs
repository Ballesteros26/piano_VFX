using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates how to marshal the array elements when an array is marshaled from managed to unmanaged code as a <see cref="F:System.Runtime.InteropServices.UnmanagedType.SafeArray" />. </summary>
	// Token: 0x020008BD RID: 2237
	[ComVisible(true)]
	[Serializable]
	public enum VarEnum
	{
		/// <summary>Indicates that a value was not specified.</summary>
		// Token: 0x04002C3C RID: 11324
		VT_EMPTY,
		/// <summary>Indicates a null value, similar to a null value in SQL.</summary>
		// Token: 0x04002C3D RID: 11325
		VT_NULL,
		/// <summary>Indicates a short integer.</summary>
		// Token: 0x04002C3E RID: 11326
		VT_I2,
		/// <summary>Indicates a long integer.</summary>
		// Token: 0x04002C3F RID: 11327
		VT_I4,
		/// <summary>Indicates a float value.</summary>
		// Token: 0x04002C40 RID: 11328
		VT_R4,
		/// <summary>Indicates a double value.</summary>
		// Token: 0x04002C41 RID: 11329
		VT_R8,
		/// <summary>Indicates a currency value.</summary>
		// Token: 0x04002C42 RID: 11330
		VT_CY,
		/// <summary>Indicates a DATE value.</summary>
		// Token: 0x04002C43 RID: 11331
		VT_DATE,
		/// <summary>Indicates a BSTR string.</summary>
		// Token: 0x04002C44 RID: 11332
		VT_BSTR,
		/// <summary>Indicates an IDispatch pointer.</summary>
		// Token: 0x04002C45 RID: 11333
		VT_DISPATCH,
		/// <summary>Indicates an SCODE.</summary>
		// Token: 0x04002C46 RID: 11334
		VT_ERROR,
		/// <summary>Indicates a Boolean value.</summary>
		// Token: 0x04002C47 RID: 11335
		VT_BOOL,
		/// <summary>Indicates a VARIANT far pointer.</summary>
		// Token: 0x04002C48 RID: 11336
		VT_VARIANT,
		/// <summary>Indicates an IUnknown pointer.</summary>
		// Token: 0x04002C49 RID: 11337
		VT_UNKNOWN,
		/// <summary>Indicates a decimal value.</summary>
		// Token: 0x04002C4A RID: 11338
		VT_DECIMAL,
		/// <summary>Indicates a char value.</summary>
		// Token: 0x04002C4B RID: 11339
		VT_I1 = 16,
		/// <summary>Indicates a byte.</summary>
		// Token: 0x04002C4C RID: 11340
		VT_UI1,
		/// <summary>Indicates an unsignedshort.</summary>
		// Token: 0x04002C4D RID: 11341
		VT_UI2,
		/// <summary>Indicates an unsignedlong.</summary>
		// Token: 0x04002C4E RID: 11342
		VT_UI4,
		/// <summary>Indicates a 64-bit integer.</summary>
		// Token: 0x04002C4F RID: 11343
		VT_I8,
		/// <summary>Indicates an 64-bit unsigned integer.</summary>
		// Token: 0x04002C50 RID: 11344
		VT_UI8,
		/// <summary>Indicates an integer value.</summary>
		// Token: 0x04002C51 RID: 11345
		VT_INT,
		/// <summary>Indicates an unsigned integer value.</summary>
		// Token: 0x04002C52 RID: 11346
		VT_UINT,
		/// <summary>Indicates a C style void.</summary>
		// Token: 0x04002C53 RID: 11347
		VT_VOID,
		/// <summary>Indicates an HRESULT.</summary>
		// Token: 0x04002C54 RID: 11348
		VT_HRESULT,
		/// <summary>Indicates a pointer type.</summary>
		// Token: 0x04002C55 RID: 11349
		VT_PTR,
		/// <summary>Indicates a SAFEARRAY. Not valid in a VARIANT.</summary>
		// Token: 0x04002C56 RID: 11350
		VT_SAFEARRAY,
		/// <summary>Indicates a C style array.</summary>
		// Token: 0x04002C57 RID: 11351
		VT_CARRAY,
		/// <summary>Indicates a user defined type.</summary>
		// Token: 0x04002C58 RID: 11352
		VT_USERDEFINED,
		/// <summary>Indicates a null-terminated string.</summary>
		// Token: 0x04002C59 RID: 11353
		VT_LPSTR,
		/// <summary>Indicates a wide string terminated by null.</summary>
		// Token: 0x04002C5A RID: 11354
		VT_LPWSTR,
		/// <summary>Indicates a user defined type.</summary>
		// Token: 0x04002C5B RID: 11355
		VT_RECORD = 36,
		/// <summary>Indicates a FILETIME value.</summary>
		// Token: 0x04002C5C RID: 11356
		VT_FILETIME = 64,
		/// <summary>Indicates length prefixed bytes.</summary>
		// Token: 0x04002C5D RID: 11357
		VT_BLOB,
		/// <summary>Indicates that the name of a stream follows.</summary>
		// Token: 0x04002C5E RID: 11358
		VT_STREAM,
		/// <summary>Indicates that the name of a storage follows.</summary>
		// Token: 0x04002C5F RID: 11359
		VT_STORAGE,
		/// <summary>Indicates that a stream contains an object.</summary>
		// Token: 0x04002C60 RID: 11360
		VT_STREAMED_OBJECT,
		/// <summary>Indicates that a storage contains an object.</summary>
		// Token: 0x04002C61 RID: 11361
		VT_STORED_OBJECT,
		/// <summary>Indicates that a blob contains an object.</summary>
		// Token: 0x04002C62 RID: 11362
		VT_BLOB_OBJECT,
		/// <summary>Indicates the clipboard format.</summary>
		// Token: 0x04002C63 RID: 11363
		VT_CF,
		/// <summary>Indicates a class ID.</summary>
		// Token: 0x04002C64 RID: 11364
		VT_CLSID,
		/// <summary>Indicates a simple, counted array.</summary>
		// Token: 0x04002C65 RID: 11365
		VT_VECTOR = 4096,
		/// <summary>Indicates a SAFEARRAY pointer.</summary>
		// Token: 0x04002C66 RID: 11366
		VT_ARRAY = 8192,
		/// <summary>Indicates that a value is a reference.</summary>
		// Token: 0x04002C67 RID: 11367
		VT_BYREF = 16384
	}
}
