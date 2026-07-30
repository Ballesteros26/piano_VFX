using System;
using System.Globalization;

// Token: 0x02000008 RID: 8
internal static class SR
{
	// Token: 0x06000009 RID: 9 RVA: 0x00002125 File Offset: 0x00000325
	internal static string GetString(string name, params object[] args)
	{
		return SR.GetString(CultureInfo.InvariantCulture, name, args);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002133 File Offset: 0x00000333
	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002119 File Offset: 0x00000319
	internal static string GetString(string name)
	{
		return name;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000213D File Offset: 0x0000033D
	internal static string GetString(CultureInfo culture, string name)
	{
		return name;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002140 File Offset: 0x00000340
	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002153 File Offset: 0x00000353
	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002161 File Offset: 0x00000361
	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00002170 File Offset: 0x00000370
	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}

	// Token: 0x0400003A RID: 58
	public const string Arg_AccessException = "Cannot access member.";

	// Token: 0x0400003B RID: 59
	public const string Arg_AccessViolationException = "Attempted to read or write protected memory. This is often an indication that other memory is corrupt.";

	// Token: 0x0400003C RID: 60
	public const string Arg_ApplicationException = "Error in the application.";

	// Token: 0x0400003D RID: 61
	public const string Arg_ArgumentException = "Value does not fall within the expected range.";

	// Token: 0x0400003E RID: 62
	public const string Arg_ArgumentOutOfRangeException = "Specified argument was out of the range of valid values.";

	// Token: 0x0400003F RID: 63
	public const string Arg_ArithmeticException = "Overflow or underflow in the arithmetic operation.";

	// Token: 0x04000040 RID: 64
	public const string Arg_ArrayPlusOffTooSmall = "Destination array is not long enough to copy all the items in the collection. Check array index and length.";

	// Token: 0x04000041 RID: 65
	public const string Arg_ArrayTypeMismatchException = "Attempted to access an element as a type incompatible with the array.";

	// Token: 0x04000042 RID: 66
	public const string Arg_ArrayZeroError = "Array must not be of length zero.";

	// Token: 0x04000043 RID: 67
	public const string Arg_BadImageFormatException = "Format of the executable (.exe) or library (.dll) is invalid.";

	// Token: 0x04000044 RID: 68
	public const string Arg_BogusIComparer = "Unable to sort because the IComparer.Compare() method returns inconsistent results. Either a value does not compare equal to itself, or one value repeatedly compared to another value yields different results. IComparer: '{0}'.";

	// Token: 0x04000045 RID: 69
	public const string Arg_CannotBeNaN = "TimeSpan does not accept floating point Not-a-Number values.";

	// Token: 0x04000046 RID: 70
	public const string Arg_CannotHaveNegativeValue = "String cannot contain a minus sign if the base is not 10.";

	// Token: 0x04000047 RID: 71
	public const string Arg_CopyNonBlittableArray = "Arrays must contain only blittable data in order to be copied to unmanaged memory.";

	// Token: 0x04000048 RID: 72
	public const string Arg_CopyOutOfRange = "Requested range extends past the end of the array.";

	// Token: 0x04000049 RID: 73
	public const string Arg_CryptographyException = "Error occurred during a cryptographic operation.";

	// Token: 0x0400004A RID: 74
	public const string Arg_DataMisalignedException = "A datatype misalignment was detected in a load or store instruction.";

	// Token: 0x0400004B RID: 75
	public const string Arg_DateTimeRange = "Combination of arguments to the DateTime constructor is out of the legal range.";

	// Token: 0x0400004C RID: 76
	public const string Arg_DirectoryNotFoundException = "Attempted to access a path that is not on the disk.";

	// Token: 0x0400004D RID: 77
	public const string Arg_DecBitCtor = "Decimal byte array constructor requires an array of length four containing valid decimal bytes.";

	// Token: 0x0400004E RID: 78
	public const string Arg_DivideByZero = "Attempted to divide by zero.";

	// Token: 0x0400004F RID: 79
	public const string Arg_DlgtNullInst = "Delegate to an instance method cannot have null 'this'.";

	// Token: 0x04000050 RID: 80
	public const string Arg_DlgtTypeMis = "Delegates must be of the same type.";

	// Token: 0x04000051 RID: 81
	public const string Arg_DuplicateWaitObjectException = "Duplicate objects in argument.";

	// Token: 0x04000052 RID: 82
	public const string Arg_EnumAndObjectMustBeSameType = "Object must be the same type as the enum. The type passed in was '{0}'; the enum type was '{1}'.";

	// Token: 0x04000053 RID: 83
	public const string Arg_EntryPointNotFoundException = "Entry point was not found.";

	// Token: 0x04000054 RID: 84
	public const string Arg_EntryPointNotFoundExceptionParameterized = "Unable to find an entry point named '{0}' in DLL '{1}'.";

	// Token: 0x04000055 RID: 85
	public const string Arg_EnumIllegalVal = "Illegal enum value: {0}.";

	// Token: 0x04000056 RID: 86
	public const string Arg_ExecutionEngineException = "Internal error in the runtime.";

	// Token: 0x04000057 RID: 87
	public const string Arg_ExternalException = "External component has thrown an exception.";

	// Token: 0x04000058 RID: 88
	public const string Arg_FieldAccessException = "Attempted to access a field that is not accessible by the caller.";

	// Token: 0x04000059 RID: 89
	public const string Arg_FormatException = "One of the identified items was in an invalid format.";

	// Token: 0x0400005A RID: 90
	public const string Arg_GuidArrayCtor = "Byte array for GUID must be exactly {0} bytes long.";

	// Token: 0x0400005B RID: 91
	public const string Arg_HexStyleNotSupported = "The number style AllowHexSpecifier is not supported on floating point data types.";

	// Token: 0x0400005C RID: 92
	public const string Arg_HTCapacityOverflow = "Hashtable's capacity overflowed and went negative. Check load factor, capacity and the current size of the table.";

	// Token: 0x0400005D RID: 93
	public const string Arg_IndexOutOfRangeException = "Index was outside the bounds of the array.";

	// Token: 0x0400005E RID: 94
	public const string Arg_InsufficientExecutionStackException = "Insufficient stack to continue executing the program safely. This can happen from having too many functions on the call stack or function on the stack using too much stack space.";

	// Token: 0x0400005F RID: 95
	public const string Arg_InvalidBase = "Invalid Base.";

	// Token: 0x04000060 RID: 96
	public const string Arg_InvalidCastException = "Specified cast is not valid.";

	// Token: 0x04000061 RID: 97
	public const string Arg_InvalidHexStyle = "With the AllowHexSpecifier bit set in the enum bit field, the only other valid bits that can be combined into the enum value must be a subset of those in HexNumber.";

	// Token: 0x04000062 RID: 98
	public const string Arg_InvalidOperationException = "Operation is not valid due to the current state of the object.";

	// Token: 0x04000063 RID: 99
	public const string Arg_OleAutDateInvalid = " Not a legal OleAut date.";

	// Token: 0x04000064 RID: 100
	public const string Arg_OleAutDateScale = "OleAut date did not convert to a DateTime correctly.";

	// Token: 0x04000065 RID: 101
	public const string Arg_InvalidRuntimeTypeHandle = "Invalid RuntimeTypeHandle.";

	// Token: 0x04000066 RID: 102
	public const string Arg_IOException = "I/O error occurred.";

	// Token: 0x04000067 RID: 103
	public const string Arg_KeyNotFound = "The given key was not present in the dictionary.";

	// Token: 0x04000068 RID: 104
	public const string Arg_LongerThanSrcString = "Source string was not long enough. Check sourceIndex and count.";

	// Token: 0x04000069 RID: 105
	public const string Arg_LowerBoundsMustMatch = "The arrays' lower bounds must be identical.";

	// Token: 0x0400006A RID: 106
	public const string Arg_MissingFieldException = "Attempted to access a non-existing field.";

	// Token: 0x0400006B RID: 107
	public const string Arg_MethodAccessException = "Attempt to access the method failed.";

	// Token: 0x0400006C RID: 108
	public const string Arg_MissingMemberException = "Attempted to access a missing member.";

	// Token: 0x0400006D RID: 109
	public const string Arg_MissingMethodException = "Attempted to access a missing method.";

	// Token: 0x0400006E RID: 110
	public const string Arg_MulticastNotSupportedException = "Attempted to add multiple callbacks to a delegate that does not support multicast.";

	// Token: 0x0400006F RID: 111
	public const string Arg_MustBeBoolean = "Object must be of type Boolean.";

	// Token: 0x04000070 RID: 112
	public const string Arg_MustBeByte = "Object must be of type Byte.";

	// Token: 0x04000071 RID: 113
	public const string Arg_MustBeChar = "Object must be of type Char.";

	// Token: 0x04000072 RID: 114
	public const string Arg_MustBeDateTime = "Object must be of type DateTime.";

	// Token: 0x04000073 RID: 115
	public const string Arg_MustBeDateTimeOffset = "Object must be of type DateTimeOffset.";

	// Token: 0x04000074 RID: 116
	public const string Arg_MustBeDecimal = "Object must be of type Decimal.";

	// Token: 0x04000075 RID: 117
	public const string Arg_MustBeDouble = "Object must be of type Double.";

	// Token: 0x04000076 RID: 118
	public const string Arg_MustBeEnum = "Type provided must be an Enum.";

	// Token: 0x04000077 RID: 119
	public const string Arg_MustBeGuid = "Object must be of type GUID.";

	// Token: 0x04000078 RID: 120
	public const string Arg_MustBeInt16 = "Object must be of type Int16.";

	// Token: 0x04000079 RID: 121
	public const string Arg_MustBeInt32 = "Object must be of type Int32.";

	// Token: 0x0400007A RID: 122
	public const string Arg_MustBeInt64 = "Object must be of type Int64.";

	// Token: 0x0400007B RID: 123
	public const string Arg_MustBePrimArray = "Object must be an array of primitives.";

	// Token: 0x0400007C RID: 124
	public const string Arg_MustBeSByte = "Object must be of type SByte.";

	// Token: 0x0400007D RID: 125
	public const string Arg_MustBeSingle = "Object must be of type Single.";

	// Token: 0x0400007E RID: 126
	public const string Arg_MustBeStatic = "Method must be a static method.";

	// Token: 0x0400007F RID: 127
	public const string Arg_MustBeString = "Object must be of type String.";

	// Token: 0x04000080 RID: 128
	public const string Arg_MustBeStringPtrNotAtom = "The pointer passed in as a String must not be in the bottom 64K of the process's address space.";

	// Token: 0x04000081 RID: 129
	public const string Arg_MustBeTimeSpan = "Object must be of type TimeSpan.";

	// Token: 0x04000082 RID: 130
	public const string Arg_MustBeUInt16 = "Object must be of type UInt16.";

	// Token: 0x04000083 RID: 131
	public const string Arg_MustBeUInt32 = "Object must be of type UInt32.";

	// Token: 0x04000084 RID: 132
	public const string Arg_MustBeUInt64 = "Object must be of type UInt64.";

	// Token: 0x04000085 RID: 133
	public const string Arg_MustBeVersion = "Object must be of type Version.";

	// Token: 0x04000086 RID: 134
	public const string Arg_NeedAtLeast1Rank = "Must provide at least one rank.";

	// Token: 0x04000087 RID: 135
	public const string Arg_Need2DArray = "Array was not a two-dimensional array.";

	// Token: 0x04000088 RID: 136
	public const string Arg_Need3DArray = "Array was not a three-dimensional array.";

	// Token: 0x04000089 RID: 137
	public const string Arg_NegativeArgCount = "Argument count must not be negative.";

	// Token: 0x0400008A RID: 138
	public const string Arg_NotFiniteNumberException = "Arg_NotFiniteNumberException = Number encountered was not a finite quantity.";

	// Token: 0x0400008B RID: 139
	public const string Arg_NonZeroLowerBound = "The lower bound of target array must be zero.";

	// Token: 0x0400008C RID: 140
	public const string Arg_NotGenericParameter = "Method may only be called on a Type for which Type.IsGenericParameter is true.";

	// Token: 0x0400008D RID: 141
	public const string Arg_NotImplementedException = "The method or operation is not implemented.";

	// Token: 0x0400008E RID: 142
	public const string Arg_NotSupportedException = "Specified method is not supported.";

	// Token: 0x0400008F RID: 143
	public const string Arg_NotSupportedNonZeroLowerBound = "Arrays with non-zero lower bounds are not supported.";

	// Token: 0x04000090 RID: 144
	public const string Arg_NullReferenceException = "Object reference not set to an instance of an object.";

	// Token: 0x04000091 RID: 145
	public const string Arg_ObjObjEx = "Object of type '{0}' cannot be converted to type '{1}'.";

	// Token: 0x04000092 RID: 146
	public const string Arg_OverflowException = "Arithmetic operation resulted in an overflow.";

	// Token: 0x04000093 RID: 147
	public const string Arg_OutOfMemoryException = "Insufficient memory to continue the execution of the program.";

	// Token: 0x04000094 RID: 148
	public const string Arg_PlatformNotSupported = "Operation is not supported on this platform.";

	// Token: 0x04000095 RID: 149
	public const string Arg_ParamName_Name = "Parameter name: {0}";

	// Token: 0x04000096 RID: 150
	public const string Arg_PathIllegal = "The path is not of a legal form.";

	// Token: 0x04000097 RID: 151
	public const string Arg_PathIllegalUNC = "The UNC path should be of the form \\\\\\\\server\\\\share.";

	// Token: 0x04000098 RID: 152
	public const string Arg_RankException = "Attempted to operate on an array with the incorrect number of dimensions.";

	// Token: 0x04000099 RID: 153
	public const string Arg_RankIndices = "Indices length does not match the array rank.";

	// Token: 0x0400009A RID: 154
	public const string Arg_RankMultiDimNotSupported = "Only single dimensional arrays are supported for the requested action.";

	// Token: 0x0400009B RID: 155
	public const string Arg_RanksAndBounds = "Number of lengths and lowerBounds must match.";

	// Token: 0x0400009C RID: 156
	public const string Arg_RegGetOverflowBug = "RegistryKey.GetValue does not allow a String that has a length greater than Int32.MaxValue.";

	// Token: 0x0400009D RID: 157
	public const string Arg_RegKeyNotFound = "The specified registry key does not exist.";

	// Token: 0x0400009E RID: 158
	public const string Arg_SecurityException = "Security error.";

	// Token: 0x0400009F RID: 159
	public const string Arg_StackOverflowException = "Operation caused a stack overflow.";

	// Token: 0x040000A0 RID: 160
	public const string Arg_SynchronizationLockException = "Object synchronization method was called from an unsynchronized block of code.";

	// Token: 0x040000A1 RID: 161
	public const string Arg_SystemException = "System error.";

	// Token: 0x040000A2 RID: 162
	public const string Arg_TargetInvocationException = "Exception has been thrown by the target of an invocation.";

	// Token: 0x040000A3 RID: 163
	public const string Arg_TargetParameterCountException = "Number of parameters specified does not match the expected number.";

	// Token: 0x040000A4 RID: 164
	public const string Arg_DefaultValueMissingException = "Missing parameter does not have a default value.";

	// Token: 0x040000A5 RID: 165
	public const string Arg_ThreadStartException = "Thread failed to start.";

	// Token: 0x040000A6 RID: 166
	public const string Arg_ThreadStateException = "Thread was in an invalid state for the operation being executed.";

	// Token: 0x040000A7 RID: 167
	public const string Arg_TimeoutException = "The operation has timed out.";

	// Token: 0x040000A8 RID: 168
	public const string Arg_TypeAccessException = "Attempt to access the type failed.";

	// Token: 0x040000A9 RID: 169
	public const string Arg_TypeLoadException = "Failure has occurred while loading a type.";

	// Token: 0x040000AA RID: 170
	public const string Arg_UnauthorizedAccessException = "Attempted to perform an unauthorized operation.";

	// Token: 0x040000AB RID: 171
	public const string Arg_VersionString = "Version string portion was too short or too long.";

	// Token: 0x040000AC RID: 172
	public const string Arg_WrongType = "The value '{0}' is not of type '{1}' and cannot be used in this generic collection.";

	// Token: 0x040000AD RID: 173
	public const string Argument_AbsolutePathRequired = "Absolute path information is required.";

	// Token: 0x040000AE RID: 174
	public const string Argument_AddingDuplicate = "An item with the same key has already been added. Key: {0}";

	// Token: 0x040000AF RID: 175
	public const string Argument_AddingDuplicate__ = "Item has already been added. Key in dictionary: '{0}'  Key being added: '{1}'";

	// Token: 0x040000B0 RID: 176
	public const string Argument_AdjustmentRulesNoNulls = "The AdjustmentRule array cannot contain null elements.";

	// Token: 0x040000B1 RID: 177
	public const string Argument_AdjustmentRulesOutOfOrder = "The elements of the AdjustmentRule array must be in chronological order and must not overlap.";

	// Token: 0x040000B2 RID: 178
	public const string Argument_BadFormatSpecifier = "Format specifier was invalid.";

	// Token: 0x040000B3 RID: 179
	public const string Argument_CodepageNotSupported = "{0} is not a supported code page.";

	// Token: 0x040000B4 RID: 180
	public const string Argument_CompareOptionOrdinal = "CompareOption.Ordinal cannot be used with other options.";

	// Token: 0x040000B5 RID: 181
	public const string Argument_ConflictingDateTimeRoundtripStyles = "The DateTimeStyles value RoundtripKind cannot be used with the values AssumeLocal, AssumeUniversal or AdjustToUniversal.";

	// Token: 0x040000B6 RID: 182
	public const string Argument_ConflictingDateTimeStyles = "The DateTimeStyles values AssumeLocal and AssumeUniversal cannot be used together.";

	// Token: 0x040000B7 RID: 183
	public const string Argument_ConversionOverflow = "Conversion buffer overflow.";

	// Token: 0x040000B8 RID: 184
	public const string Argument_ConvertMismatch = "The conversion could not be completed because the supplied DateTime did not have the Kind property set correctly.  For example, when the Kind property is DateTimeKind.Local, the source time zone must be TimeZoneInfo.Local.";

	// Token: 0x040000B9 RID: 185
	public const string Argument_CultureInvalidIdentifier = "{0} is an invalid culture identifier.";

	// Token: 0x040000BA RID: 186
	public const string Argument_CultureIetfNotSupported = "Culture IETF Name {0} is not a recognized IETF name.";

	// Token: 0x040000BB RID: 187
	public const string Argument_CultureIsNeutral = "Culture ID {0} (0x{0:X4}) is a neutral culture; a region cannot be created from it.";

	// Token: 0x040000BC RID: 188
	public const string Argument_CultureNotSupported = "Culture is not supported.";

	// Token: 0x040000BD RID: 189
	public const string Argument_CustomCultureCannotBePassedByNumber = "Customized cultures cannot be passed by LCID, only by name.";

	// Token: 0x040000BE RID: 190
	public const string Argument_DateTimeBadBinaryData = "The binary data must result in a DateTime with ticks between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.";

	// Token: 0x040000BF RID: 191
	public const string Argument_DateTimeHasTicks = "The supplied DateTime must have the Year, Month, and Day properties set to 1.  The time cannot be specified more precisely than whole milliseconds.";

	// Token: 0x040000C0 RID: 192
	public const string Argument_DateTimeHasTimeOfDay = "The supplied DateTime includes a TimeOfDay setting.   This is not supported.";

	// Token: 0x040000C1 RID: 193
	public const string Argument_DateTimeIsInvalid = "The supplied DateTime represents an invalid time.  For example, when the clock is adjusted forward, any time in the period that is skipped is invalid.";

	// Token: 0x040000C2 RID: 194
	public const string Argument_DateTimeIsNotAmbiguous = "The supplied DateTime is not in an ambiguous time range.";

	// Token: 0x040000C3 RID: 195
	public const string Argument_DateTimeKindMustBeUnspecified = "The supplied DateTime must have the Kind property set to DateTimeKind.Unspecified.";

	// Token: 0x040000C4 RID: 196
	public const string Argument_DateTimeOffsetInvalidDateTimeStyles = "The DateTimeStyles value 'NoCurrentDateDefault' is not allowed when parsing DateTimeOffset.";

	// Token: 0x040000C5 RID: 197
	public const string Argument_DateTimeOffsetIsNotAmbiguous = "The supplied DateTimeOffset is not in an ambiguous time range.";

	// Token: 0x040000C6 RID: 198
	public const string Argument_EmptyDecString = "Decimal separator cannot be the empty string.";

	// Token: 0x040000C7 RID: 199
	public const string Argument_EmptyName = "Empty name is not legal.";

	// Token: 0x040000C8 RID: 200
	public const string Argument_EmptyWaithandleArray = "Waithandle array may not be empty.";

	// Token: 0x040000C9 RID: 201
	public const string Argument_EncoderFallbackNotEmpty = "Must complete Convert() operation or call Encoder.Reset() before calling GetBytes() or GetByteCount(). Encoder '{0}' fallback '{1}'.";

	// Token: 0x040000CA RID: 202
	public const string Argument_EncodingConversionOverflowBytes = "The output byte buffer is too small to contain the encoded data, encoding '{0}' fallback '{1}'.";

	// Token: 0x040000CB RID: 203
	public const string Argument_EncodingConversionOverflowChars = "The output char buffer is too small to contain the decoded characters, encoding '{0}' fallback '{1}'.";

	// Token: 0x040000CC RID: 204
	public const string Argument_EncodingNotSupported = "'{0}' is not a supported encoding name. For information on defining a custom encoding, see the documentation for the Encoding.RegisterProvider method.";

	// Token: 0x040000CD RID: 205
	public const string Argument_EnumTypeDoesNotMatch = "The argument type, '{0}', is not the same as the enum type '{1}'.";

	// Token: 0x040000CE RID: 206
	public const string Argument_FallbackBufferNotEmpty = "Cannot change fallback when buffer is not empty. Previous Convert() call left data in the fallback buffer.";

	// Token: 0x040000CF RID: 207
	public const string Argument_IdnBadLabelSize = "IDN labels must be between 1 and 63 characters long.";

	// Token: 0x040000D0 RID: 208
	public const string Argument_IdnBadPunycode = "Invalid IDN encoded string.";

	// Token: 0x040000D1 RID: 209
	public const string Argument_IdnIllegalName = "Decoded string is not a valid IDN name.";

	// Token: 0x040000D2 RID: 210
	public const string Argument_ImplementIComparable = "At least one object must implement IComparable.";

	// Token: 0x040000D3 RID: 211
	public const string Argument_InvalidArgumentForComparison = "Type of argument is not compatible with the generic comparer.";

	// Token: 0x040000D4 RID: 212
	public const string Argument_InvalidArrayLength = "Length of the array must be {0}.";

	// Token: 0x040000D5 RID: 213
	public const string Argument_InvalidArrayType = "Target array type is not compatible with the type of items in the collection.";

	// Token: 0x040000D6 RID: 214
	public const string Argument_InvalidCalendar = "Not a valid calendar for the given culture.";

	// Token: 0x040000D7 RID: 215
	public const string Argument_InvalidCharSequence = "Invalid Unicode code point found at index {0}.";

	// Token: 0x040000D8 RID: 216
	public const string Argument_InvalidCharSequenceNoIndex = "String contains invalid Unicode code points.";

	// Token: 0x040000D9 RID: 217
	public const string Argument_InvalidCodePageBytesIndex = "Unable to translate bytes {0} at index {1} from specified code page to Unicode.";

	// Token: 0x040000DA RID: 218
	public const string Argument_InvalidCodePageConversionIndex = "Unable to translate Unicode character \\\\u{0:X4} at index {1} to specified code page.";

	// Token: 0x040000DB RID: 219
	public const string Argument_InvalidCultureName = "Culture name '{0}' is not supported.";

	// Token: 0x040000DC RID: 220
	public const string Argument_InvalidDateTimeKind = "Invalid DateTimeKind value.";

	// Token: 0x040000DD RID: 221
	public const string Argument_InvalidDateTimeStyles = "An undefined DateTimeStyles value is being used.";

	// Token: 0x040000DE RID: 222
	public const string Argument_InvalidDigitSubstitution = "The DigitSubstitution property must be of a valid member of the DigitShapes enumeration. Valid entries include Context, NativeNational or None.";

	// Token: 0x040000DF RID: 223
	public const string Argument_InvalidEnumValue = "The value '{0}' is not valid for this usage of the type {1}.";

	// Token: 0x040000E0 RID: 224
	public const string Argument_InvalidFlag = "Value of flags is invalid.";

	// Token: 0x040000E1 RID: 225
	public const string Argument_InvalidGroupSize = "Every element in the value array should be between one and nine, except for the last element, which can be zero.";

	// Token: 0x040000E2 RID: 226
	public const string Argument_InvalidHighSurrogate = "Found a high surrogate char without a following low surrogate at index: {0}. The input may not be in this encoding, or may not contain valid Unicode (UTF-16) characters.";

	// Token: 0x040000E3 RID: 227
	public const string Argument_InvalidId = "The specified ID parameter '{0}' is not supported.";

	// Token: 0x040000E4 RID: 228
	public const string Argument_InvalidLowSurrogate = "Found a low surrogate char without a preceding high surrogate at index: {0}. The input may not be in this encoding, or may not contain valid Unicode (UTF-16) characters.";

	// Token: 0x040000E5 RID: 229
	public const string Argument_InvalidNativeDigitCount = "The NativeDigits array must contain exactly ten members.";

	// Token: 0x040000E6 RID: 230
	public const string Argument_InvalidNativeDigitValue = "Each member of the NativeDigits array must be a single text element (one or more UTF16 code points) with a Unicode Nd (Number, Decimal Digit) property indicating it is a digit.";

	// Token: 0x040000E7 RID: 231
	public const string Argument_InvalidNeutralRegionName = "The region name {0} should not correspond to neutral culture; a specific culture name is required.";

	// Token: 0x040000E8 RID: 232
	public const string Argument_InvalidNormalizationForm = "Invalid normalization form.";

	// Token: 0x040000E9 RID: 233
	public const string Argument_InvalidNumberStyles = "An undefined NumberStyles value is being used.";

	// Token: 0x040000EA RID: 234
	public const string Argument_InvalidOffLen = "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.";

	// Token: 0x040000EB RID: 235
	public const string Argument_InvalidPathChars = "Illegal characters in path.";

	// Token: 0x040000EC RID: 236
	public const string Argument_InvalidREG_TZI_FORMAT = "The REG_TZI_FORMAT structure is corrupt.";

	// Token: 0x040000ED RID: 237
	public const string Argument_InvalidResourceCultureName = "The given culture name '{0}' cannot be used to locate a resource file. Resource filenames must consist of only letters, numbers, hyphens or underscores.";

	// Token: 0x040000EE RID: 238
	public const string Argument_InvalidSerializedString = "The specified serialized string '{0}' is not supported.";

	// Token: 0x040000EF RID: 239
	public const string Argument_InvalidTimeSpanStyles = "An undefined TimeSpanStyles value is being used.";

	// Token: 0x040000F0 RID: 240
	public const string Argument_MustBeFalse = "Argument must be initialized to false";

	// Token: 0x040000F1 RID: 241
	public const string Argument_NoEra = "No Era was supplied.";

	// Token: 0x040000F2 RID: 242
	public const string Argument_NoRegionInvariantCulture = "There is no region associated with the Invariant Culture (Culture ID: 0x7F).";

	// Token: 0x040000F3 RID: 243
	public const string Argument_NotIsomorphic = "Object contains non-primitive or non-blittable data.";

	// Token: 0x040000F4 RID: 244
	public const string Argument_OffsetLocalMismatch = "The UTC Offset of the local dateTime parameter does not match the offset argument.";

	// Token: 0x040000F5 RID: 245
	public const string Argument_OffsetPrecision = "Offset must be specified in whole minutes.";

	// Token: 0x040000F6 RID: 246
	public const string Argument_OffsetOutOfRange = "Offset must be within plus or minus 14 hours.";

	// Token: 0x040000F7 RID: 247
	public const string Argument_OffsetUtcMismatch = "The UTC Offset for Utc DateTime instances must be 0.";

	// Token: 0x040000F8 RID: 248
	public const string Argument_OneOfCulturesNotSupported = "Culture name {0} or {1} is not supported.";

	// Token: 0x040000F9 RID: 249
	public const string Argument_OnlyMscorlib = "Only mscorlib's assembly is valid.";

	// Token: 0x040000FA RID: 250
	public const string Argument_OutOfOrderDateTimes = "The DateStart property must come before the DateEnd property.";

	// Token: 0x040000FB RID: 251
	public const string ArgumentOutOfRange_HugeArrayNotSupported = "Arrays larger than 2GB are not supported.";

	// Token: 0x040000FC RID: 252
	public const string ArgumentOutOfRange_Index = "Index was out of range. Must be non-negative and less than the size of the collection.";

	// Token: 0x040000FD RID: 253
	public const string ArgumentOutOfRange_Length = "The specified length exceeds maximum capacity of SecureString.";

	// Token: 0x040000FE RID: 254
	public const string ArgumentOutOfRange_LengthTooLarge = "The specified length exceeds the maximum value of {0}.";

	// Token: 0x040000FF RID: 255
	public const string ArgumentOutOfRange_NeedNonNegNum = "Non-negative number required.";

	// Token: 0x04000100 RID: 256
	public const string ArgumentOutOfRange_NeedNonNegNumRequired = "Non-negative number required.";

	// Token: 0x04000101 RID: 257
	public const string Argument_PathFormatNotSupported = "The given path's format is not supported.";

	// Token: 0x04000102 RID: 258
	public const string Argument_RecursiveFallback = "Recursive fallback not allowed for character \\\\u{0:X4}.";

	// Token: 0x04000103 RID: 259
	public const string Argument_RecursiveFallbackBytes = "Recursive fallback not allowed for bytes {0}.";

	// Token: 0x04000104 RID: 260
	public const string Argument_ResultCalendarRange = "The result is out of the supported range for this calendar. The result should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive.";

	// Token: 0x04000105 RID: 261
	public const string Argument_SemaphoreInitialMaximum = "The initial count for the semaphore must be greater than or equal to zero and less than the maximum count.";

	// Token: 0x04000106 RID: 262
	public const string Argument_TimeSpanHasSeconds = "The TimeSpan parameter cannot be specified more precisely than whole minutes.";

	// Token: 0x04000107 RID: 263
	public const string Argument_TimeZoneNotFound = "The time zone ID '{0}' was not found on the local computer.";

	// Token: 0x04000108 RID: 264
	public const string Argument_TimeZoneInfoBadTZif = "The tzfile does not begin with the magic characters 'TZif'.  Please verify that the file is not corrupt.";

	// Token: 0x04000109 RID: 265
	public const string Argument_TimeZoneInfoInvalidTZif = "The TZif data structure is corrupt.";

	// Token: 0x0400010A RID: 266
	public const string Argument_ToExclusiveLessThanFromExclusive = "fromInclusive must be less than or equal to toExclusive.";

	// Token: 0x0400010B RID: 267
	public const string Argument_TransitionTimesAreIdentical = "The DaylightTransitionStart property must not equal the DaylightTransitionEnd property.";

	// Token: 0x0400010C RID: 268
	public const string Argument_UTCOutOfRange = "The UTC time represented when the offset is applied must be between year 0 and 10,000.";

	// Token: 0x0400010D RID: 269
	public const string Argument_WaitHandleNameTooLong = "The name can be no more than {0} characters in length.";

	// Token: 0x0400010E RID: 270
	public const string ArgumentException_OtherNotArrayOfCorrectLength = "Object is not a array with the same number of elements as the array to compare it to.";

	// Token: 0x0400010F RID: 271
	public const string ArgumentException_TupleIncorrectType = "Argument must be of type {0}.";

	// Token: 0x04000110 RID: 272
	public const string ArgumentException_TupleLastArgumentNotATuple = "The last element of an eight element tuple must be a Tuple.";

	// Token: 0x04000111 RID: 273
	public const string ArgumentException_ValueTupleIncorrectType = "Argument must be of type {0}.";

	// Token: 0x04000112 RID: 274
	public const string ArgumentException_ValueTupleLastArgumentNotAValueTuple = "The last element of an eight element ValueTuple must be a ValueTuple.";

	// Token: 0x04000113 RID: 275
	public const string ArgumentNull_Array = "Array cannot be null.";

	// Token: 0x04000114 RID: 276
	public const string ArgumentNull_ArrayElement = "At least one element in the specified array was null.";

	// Token: 0x04000115 RID: 277
	public const string ArgumentNull_ArrayValue = "Found a null value within an array.";

	// Token: 0x04000116 RID: 278
	public const string ArgumentNull_Generic = "Value cannot be null.";

	// Token: 0x04000117 RID: 279
	public const string ArgumentNull_Key = "Key cannot be null.";

	// Token: 0x04000118 RID: 280
	public const string ArgumentNull_Obj = "Object cannot be null.";

	// Token: 0x04000119 RID: 281
	public const string ArgumentNull_String = "String reference not set to an instance of a String.";

	// Token: 0x0400011A RID: 282
	public const string ArgumentNull_Type = "Type cannot be null.";

	// Token: 0x0400011B RID: 283
	public const string ArgumentNull_Waithandles = "The waitHandles parameter cannot be null.";

	// Token: 0x0400011C RID: 284
	public const string ArgumentNull_WithParamName = "Parameter '{0}' cannot be null.";

	// Token: 0x0400011D RID: 285
	public const string ArgumentOutOfRange_AddValue = "Value to add was out of range.";

	// Token: 0x0400011E RID: 286
	public const string ArgumentOutOfRange_ActualValue = "Actual value was {0}.";

	// Token: 0x0400011F RID: 287
	public const string ArgumentOutOfRange_BadYearMonthDay = "Year, Month, and Day parameters describe an un-representable DateTime.";

	// Token: 0x04000120 RID: 288
	public const string ArgumentOutOfRange_BadHourMinuteSecond = "Hour, Minute, and Second parameters describe an un-representable DateTime.";

	// Token: 0x04000121 RID: 289
	public const string ArgumentOutOfRange_BiggerThanCollection = "Must be less than or equal to the size of the collection.";

	// Token: 0x04000122 RID: 290
	public const string ArgumentOutOfRange_Bounds_Lower_Upper = "Argument must be between {0} and {1}.";

	// Token: 0x04000123 RID: 291
	public const string ArgumentOutOfRange_CalendarRange = "Specified time is not supported in this calendar. It should be between {0} (Gregorian date) and {1} (Gregorian date), inclusive.";

	// Token: 0x04000124 RID: 292
	public const string ArgumentOutOfRange_Capacity = "Capacity exceeds maximum capacity.";

	// Token: 0x04000125 RID: 293
	public const string ArgumentOutOfRange_Count = "Count must be positive and count must refer to a location within the string/array/collection.";

	// Token: 0x04000126 RID: 294
	public const string ArgumentOutOfRange_DateArithmetic = "The added or subtracted value results in an un-representable DateTime.";

	// Token: 0x04000127 RID: 295
	public const string ArgumentOutOfRange_DateTimeBadMonths = "Months value must be between +/-120000.";

	// Token: 0x04000128 RID: 296
	public const string ArgumentOutOfRange_DateTimeBadTicks = "Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.";

	// Token: 0x04000129 RID: 297
	public const string ArgumentOutOfRange_DateTimeBadYears = "Years value must be between +/-10000.";

	// Token: 0x0400012A RID: 298
	public const string ArgumentOutOfRange_Day = "Day must be between 1 and {0} for month {1}.";

	// Token: 0x0400012B RID: 299
	public const string ArgumentOutOfRange_DayOfWeek = "The DayOfWeek enumeration must be in the range 0 through 6.";

	// Token: 0x0400012C RID: 300
	public const string ArgumentOutOfRange_DayParam = "The Day parameter must be in the range 1 through 31.";

	// Token: 0x0400012D RID: 301
	public const string ArgumentOutOfRange_DecimalRound = "Decimal can only round to between 0 and 28 digits of precision.";

	// Token: 0x0400012E RID: 302
	public const string ArgumentOutOfRange_DecimalScale = "Decimal's scale value must be between 0 and 28, inclusive.";

	// Token: 0x0400012F RID: 303
	public const string ArgumentOutOfRange_EndIndexStartIndex = "endIndex cannot be greater than startIndex.";

	// Token: 0x04000130 RID: 304
	public const string ArgumentOutOfRange_Enum = "Enum value was out of legal range.";

	// Token: 0x04000131 RID: 305
	public const string ArgumentOutOfRange_Era = "Time value was out of era range.";

	// Token: 0x04000132 RID: 306
	public const string ArgumentOutOfRange_FileTimeInvalid = "Not a valid Win32 FileTime.";

	// Token: 0x04000133 RID: 307
	public const string ArgumentOutOfRange_GenericPositive = "Value must be positive.";

	// Token: 0x04000134 RID: 308
	public const string ArgumentOutOfRange_GetByteCountOverflow = "Too many characters. The resulting number of bytes is larger than what can be returned as an int.";

	// Token: 0x04000135 RID: 309
	public const string ArgumentOutOfRange_GetCharCountOverflow = "Too many bytes. The resulting number of chars is larger than what can be returned as an int.";

	// Token: 0x04000136 RID: 310
	public const string ArgumentOutOfRange_IndexCount = "Index and count must refer to a location within the string.";

	// Token: 0x04000137 RID: 311
	public const string ArgumentOutOfRange_IndexCountBuffer = "Index and count must refer to a location within the buffer.";

	// Token: 0x04000138 RID: 312
	public const string ArgumentOutOfRange_IndexLength = "Index and length must refer to a location within the string.";

	// Token: 0x04000139 RID: 313
	public const string ArgumentOutOfRange_IndexString = "Index was out of range. Must be non-negative and less than the length of the string.";

	// Token: 0x0400013A RID: 314
	public const string ArgumentOutOfRange_InvalidEraValue = "Era value was not valid.";

	// Token: 0x0400013B RID: 315
	public const string ArgumentOutOfRange_InvalidHighSurrogate = "A valid high surrogate character is between 0xd800 and 0xdbff, inclusive.";

	// Token: 0x0400013C RID: 316
	public const string ArgumentOutOfRange_InvalidLowSurrogate = "A valid low surrogate character is between 0xdc00 and 0xdfff, inclusive.";

	// Token: 0x0400013D RID: 317
	public const string ArgumentOutOfRange_InvalidUTF32 = "A valid UTF32 value is between 0x000000 and 0x10ffff, inclusive, and should not include surrogate codepoint values (0x00d800 ~ 0x00dfff).";

	// Token: 0x0400013E RID: 318
	public const string ArgumentOutOfRange_LengthGreaterThanCapacity = "The length cannot be greater than the capacity.";

	// Token: 0x0400013F RID: 319
	public const string ArgumentOutOfRange_ListInsert = "Index must be within the bounds of the List.";

	// Token: 0x04000140 RID: 320
	public const string ArgumentOutOfRange_ListItem = "Index was out of range. Must be non-negative and less than the size of the list.";

	// Token: 0x04000141 RID: 321
	public const string ArgumentOutOfRange_ListRemoveAt = "Index was out of range. Must be non-negative and less than the size of the list.";

	// Token: 0x04000142 RID: 322
	public const string ArgumentOutOfRange_Month = "Month must be between one and twelve.";

	// Token: 0x04000143 RID: 323
	public const string ArgumentOutOfRange_MonthParam = "The Month parameter must be in the range 1 through 12.";

	// Token: 0x04000144 RID: 324
	public const string ArgumentOutOfRange_MustBeNonNegInt32 = "Value must be non-negative and less than or equal to Int32.MaxValue.";

	// Token: 0x04000145 RID: 325
	public const string ArgumentOutOfRange_MustBeNonNegNum = "'{0}' must be non-negative.";

	// Token: 0x04000146 RID: 326
	public const string ArgumentOutOfRange_MustBePositive = "'{0}' must be greater than zero.";

	// Token: 0x04000147 RID: 327
	public const string ArgumentOutOfRange_NeedNonNegOrNegative1 = "Number must be either non-negative and less than or equal to Int32.MaxValue or -1.";

	// Token: 0x04000148 RID: 328
	public const string ArgumentOutOfRange_NeedPosNum = "Positive number required.";

	// Token: 0x04000149 RID: 329
	public const string ArgumentOutOfRange_NegativeCapacity = "Capacity must be positive.";

	// Token: 0x0400014A RID: 330
	public const string ArgumentOutOfRange_NegativeCount = "Count cannot be less than zero.";

	// Token: 0x0400014B RID: 331
	public const string ArgumentOutOfRange_NegativeLength = "Length cannot be less than zero.";

	// Token: 0x0400014C RID: 332
	public const string ArgumentOutOfRange_OffsetLength = "Offset and length must refer to a position in the string.";

	// Token: 0x0400014D RID: 333
	public const string ArgumentOutOfRange_OffsetOut = "Either offset did not refer to a position in the string, or there is an insufficient length of destination character array.";

	// Token: 0x0400014E RID: 334
	public const string ArgumentOutOfRange_PartialWCHAR = "Pointer startIndex and length do not refer to a valid string.";

	// Token: 0x0400014F RID: 335
	public const string ArgumentOutOfRange_Range = "Valid values are between {0} and {1}, inclusive.";

	// Token: 0x04000150 RID: 336
	public const string ArgumentOutOfRange_RoundingDigits = "Rounding digits must be between 0 and 15, inclusive.";

	// Token: 0x04000151 RID: 337
	public const string ArgumentOutOfRange_SmallCapacity = "capacity was less than the current size.";

	// Token: 0x04000152 RID: 338
	public const string ArgumentOutOfRange_SmallMaxCapacity = "MaxCapacity must be one or greater.";

	// Token: 0x04000153 RID: 339
	public const string ArgumentOutOfRange_StartIndex = "StartIndex cannot be less than zero.";

	// Token: 0x04000154 RID: 340
	public const string ArgumentOutOfRange_StartIndexLargerThanLength = "startIndex cannot be larger than length of string.";

	// Token: 0x04000155 RID: 341
	public const string ArgumentOutOfRange_StartIndexLessThanLength = "startIndex must be less than length of string.";

	// Token: 0x04000156 RID: 342
	public const string ArgumentOutOfRange_UtcOffset = "The TimeSpan parameter must be within plus or minus 14.0 hours.";

	// Token: 0x04000157 RID: 343
	public const string ArgumentOutOfRange_UtcOffsetAndDaylightDelta = "The sum of the BaseUtcOffset and DaylightDelta properties must within plus or minus 14.0 hours.";

	// Token: 0x04000158 RID: 344
	public const string ArgumentOutOfRange_Version = "Version's parameters must be greater than or equal to zero.";

	// Token: 0x04000159 RID: 345
	public const string ArgumentOutOfRange_Week = "The Week parameter must be in the range 1 through 5.";

	// Token: 0x0400015A RID: 346
	public const string ArgumentOutOfRange_Year = "Year must be between 1 and 9999.";

	// Token: 0x0400015B RID: 347
	public const string Arithmetic_NaN = "Function does not accept floating point Not-a-Number values.";

	// Token: 0x0400015C RID: 348
	public const string ArrayTypeMismatch_CantAssignType = "Source array type cannot be assigned to destination array type.";

	// Token: 0x0400015D RID: 349
	public const string BadImageFormatException_CouldNotLoadFileOrAssembly = "Could not load file or assembly '{0}'. An attempt was made to load a program with an incorrect format.";

	// Token: 0x0400015E RID: 350
	public const string CollectionCorrupted = "A prior operation on this collection was interrupted by an exception. Collection's state is no longer trusted.";

	// Token: 0x0400015F RID: 351
	public const string Exception_EndOfInnerExceptionStack = "--- End of inner exception stack trace ---";

	// Token: 0x04000160 RID: 352
	public const string Exception_WasThrown = "Exception of type '{0}' was thrown.";

	// Token: 0x04000161 RID: 353
	public const string Format_BadBase64Char = "The input is not a valid Base-64 string as it contains a non-base 64 character, more than two padding characters, or an illegal character among the padding characters.";

	// Token: 0x04000162 RID: 354
	public const string Format_BadBase64CharArrayLength = "Invalid length for a Base-64 char array or string.";

	// Token: 0x04000163 RID: 355
	public const string Format_BadBoolean = "String was not recognized as a valid Boolean.";

	// Token: 0x04000164 RID: 356
	public const string Format_BadFormatSpecifier = "Format specifier was invalid.";

	// Token: 0x04000165 RID: 357
	public const string Format_BadQuote = "Cannot find a matching quote character for the character '{0}'.";

	// Token: 0x04000166 RID: 358
	public const string Format_EmptyInputString = "Input string was either empty or contained only whitespace.";

	// Token: 0x04000167 RID: 359
	public const string Format_GuidHexPrefix = "Expected hex 0x in '{0}'.";

	// Token: 0x04000168 RID: 360
	public const string Format_GuidInvLen = "Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).";

	// Token: 0x04000169 RID: 361
	public const string Format_GuidInvalidChar = "Guid string should only contain hexadecimal characters.";

	// Token: 0x0400016A RID: 362
	public const string Format_GuidBrace = "Expected {0xdddddddd, etc}.";

	// Token: 0x0400016B RID: 363
	public const string Format_GuidComma = "Could not find a comma, or the length between the previous token and the comma was zero (i.e., '0x,'etc.).";

	// Token: 0x0400016C RID: 364
	public const string Format_GuidBraceAfterLastNumber = "Could not find a brace, or the length between the previous token and the brace was zero (i.e., '0x,'etc.).";

	// Token: 0x0400016D RID: 365
	public const string Format_GuidDashes = "Dashes are in the wrong position for GUID parsing.";

	// Token: 0x0400016E RID: 366
	public const string Format_GuidEndBrace = "Could not find the ending brace.";

	// Token: 0x0400016F RID: 367
	public const string Format_ExtraJunkAtEnd = "Additional non-parsable characters are at the end of the string.";

	// Token: 0x04000170 RID: 368
	public const string Format_GuidUnrecognized = "Unrecognized Guid format.";

	// Token: 0x04000171 RID: 369
	public const string Format_IndexOutOfRange = "Index (zero based) must be greater than or equal to zero and less than the size of the argument list.";

	// Token: 0x04000172 RID: 370
	public const string Format_InvalidGuidFormatSpecification = "Format String can be only 'D', 'd', 'N', 'n', 'P', 'p', 'B', 'b', 'X' or 'x'.";

	// Token: 0x04000173 RID: 371
	public const string Format_InvalidString = "Input string was not in a correct format.";

	// Token: 0x04000174 RID: 372
	public const string Format_NeedSingleChar = "String must be exactly one character long.";

	// Token: 0x04000175 RID: 373
	public const string Format_NoParsibleDigits = "Could not find any recognizable digits.";

	// Token: 0x04000176 RID: 374
	public const string Format_BadTimeSpan = "String was not recognized as a valid TimeSpan.";

	// Token: 0x04000177 RID: 375
	public const string InsufficientMemory_MemFailPoint = "Insufficient available memory to meet the expected demands of an operation at this time.  Please try again later.";

	// Token: 0x04000178 RID: 376
	public const string InsufficientMemory_MemFailPoint_TooBig = "Insufficient memory to meet the expected demands of an operation, and this system is likely to never satisfy this request.  If this is a 32 bit system, consider booting in 3 GB mode.";

	// Token: 0x04000179 RID: 377
	public const string InsufficientMemory_MemFailPoint_VAFrag = "Insufficient available memory to meet the expected demands of an operation at this time, possibly due to virtual address space fragmentation.  Please try again later.";

	// Token: 0x0400017A RID: 378
	public const string InvalidCast_CannotCastNullToValueType = "Null object cannot be converted to a value type.";

	// Token: 0x0400017B RID: 379
	public const string InvalidCast_DownCastArrayElement = "At least one element in the source array could not be cast down to the destination array type.";

	// Token: 0x0400017C RID: 380
	public const string InvalidCast_FromTo = "Invalid cast from '{0}' to '{1}'.";

	// Token: 0x0400017D RID: 381
	public const string InvalidCast_IConvertible = "Object must implement IConvertible.";

	// Token: 0x0400017E RID: 382
	public const string InvalidCast_StoreArrayElement = "Object cannot be stored in an array of this type.";

	// Token: 0x0400017F RID: 383
	public const string InvalidOperation_Calling = "WinRT Interop has already been initialized and cannot be initialized again.";

	// Token: 0x04000180 RID: 384
	public const string InvalidOperation_DateTimeParsing = "Internal Error in DateTime and Calendar operations.";

	// Token: 0x04000181 RID: 385
	public const string InvalidOperation_EnumEnded = "Enumeration already finished.";

	// Token: 0x04000182 RID: 386
	public const string InvalidOperation_EnumFailedVersion = "Collection was modified; enumeration operation may not execute.";

	// Token: 0x04000183 RID: 387
	public const string InvalidOperation_EnumNotStarted = "Enumeration has not started. Call MoveNext.";

	// Token: 0x04000184 RID: 388
	public const string InvalidOperation_EnumOpCantHappen = "Enumeration has either not started or has already finished.";

	// Token: 0x04000185 RID: 389
	public const string InvalidOperation_HandleIsNotInitialized = "Handle is not initialized.";

	// Token: 0x04000186 RID: 390
	public const string InvalidOperation_IComparerFailed = "Failed to compare two elements in the array.";

	// Token: 0x04000187 RID: 391
	public const string InvalidOperation_NoValue = "Nullable object must have a value.";

	// Token: 0x04000188 RID: 392
	public const string InvalidOperation_NullArray = "The underlying array is null.";

	// Token: 0x04000189 RID: 393
	public const string InvalidOperation_Overlapped_Pack = "Cannot pack a packed Overlapped again.";

	// Token: 0x0400018A RID: 394
	public const string InvalidOperation_ReadOnly = "Instance is read-only.";

	// Token: 0x0400018B RID: 395
	public const string InvalidOperation_ThreadWrongThreadStart = "The thread was created with a ThreadStart delegate that does not accept a parameter.";

	// Token: 0x0400018C RID: 396
	public const string InvalidOperation_UnknownEnumType = "Unknown enum type.";

	// Token: 0x0400018D RID: 397
	public const string InvalidOperation_WriteOnce = "This property has already been set and cannot be modified.";

	// Token: 0x0400018E RID: 398
	public const string InvalidOperation_ArrayCreateInstance_NotARuntimeType = "Array.CreateInstance() can only accept Type objects created by the runtime.";

	// Token: 0x0400018F RID: 399
	public const string InvalidOperation_TooEarly = "Internal Error: This operation cannot be invoked in an eager class constructor.";

	// Token: 0x04000190 RID: 400
	public const string InvalidOperation_NullContext = "Cannot call Set on a null context";

	// Token: 0x04000191 RID: 401
	public const string InvalidOperation_CannotUseAFCOtherThread = "AsyncFlowControl object must be used on the thread where it was created.";

	// Token: 0x04000192 RID: 402
	public const string InvalidOperation_CannotRestoreUnsupressedFlow = "Cannot restore context flow when it is not suppressed.";

	// Token: 0x04000193 RID: 403
	public const string InvalidOperation_CannotSupressFlowMultipleTimes = "Context flow is already suppressed.";

	// Token: 0x04000194 RID: 404
	public const string InvalidOperation_CannotUseAFCMultiple = "AsyncFlowControl object can be used only once to call Undo().";

	// Token: 0x04000195 RID: 405
	public const string InvalidOperation_AsyncFlowCtrlCtxMismatch = "AsyncFlowControl objects can be used to restore flow only on a Context that had its flow suppressed.";

	// Token: 0x04000196 RID: 406
	public const string InvalidProgram_Default = "Common Language Runtime detected an invalid program.";

	// Token: 0x04000197 RID: 407
	public const string InvalidProgram_Specific = "Common Language Runtime detected an invalid program. The body of method '{0}' is invalid.";

	// Token: 0x04000198 RID: 408
	public const string InvalidProgram_Vararg = "Method '{0}' has a variable argument list. Variable argument lists are not supported in .NET Core.";

	// Token: 0x04000199 RID: 409
	public const string InvalidProgram_CallVirtFinalize = "Object.Finalize() can not be called directly. It is only callable by the runtime.";

	// Token: 0x0400019A RID: 410
	public const string InvalidTimeZone_InvalidRegistryData = "The time zone ID '{0}' was found on the local computer, but the registry information was corrupt.";

	// Token: 0x0400019B RID: 411
	public const string IO_FileExists_Name = "The file '{0}' already exists.";

	// Token: 0x0400019C RID: 412
	public const string IO_FileName_Name = "File name: '{0}'";

	// Token: 0x0400019D RID: 413
	public const string IO_FileNotFound = "Unable to find the specified file.";

	// Token: 0x0400019E RID: 414
	public const string IO_FileNotFound_FileName = "Could not load file or assembly '{0}'. The system cannot find the file specified.";

	// Token: 0x0400019F RID: 415
	public const string IO_FileLoad = "Could not load the specified file.";

	// Token: 0x040001A0 RID: 416
	public const string IO_FileLoad_FileName = "Could not load the file '{0}'.";

	// Token: 0x040001A1 RID: 417
	public const string IO_PathNotFound_NoPathName = "Could not find a part of the path.";

	// Token: 0x040001A2 RID: 418
	public const string IO_PathNotFound_Path = "Could not find a part of the path '{0}'.";

	// Token: 0x040001A3 RID: 419
	public const string IO_PathTooLong = "The specified path, file name, or both are too long. The fully qualified file name must be less than 260 characters, and the directory name must be less than 248 characters.";

	// Token: 0x040001A4 RID: 420
	public const string IO_SharingViolation_File = "The process cannot access the file '{0}' because it is being used by another process.";

	// Token: 0x040001A5 RID: 421
	public const string IO_SharingViolation_NoFileName = "The process cannot access the file because it is being used by another process.";

	// Token: 0x040001A6 RID: 422
	public const string IO_AlreadyExists_Name = "Cannot create '{0}' because a file or directory with the same name already exists.";

	// Token: 0x040001A7 RID: 423
	public const string UnauthorizedAccess_IODenied_NoPathName = "Access to the path is denied.";

	// Token: 0x040001A8 RID: 424
	public const string UnauthorizedAccess_IODenied_Path = "Access to the path '{0}' is denied.";

	// Token: 0x040001A9 RID: 425
	public const string Lazy_CreateValue_NoParameterlessCtorForT = "The lazily-initialized type does not have a public, parameterless constructor.";

	// Token: 0x040001AA RID: 426
	public const string Lazy_ctor_ModeInvalid = "The mode argument specifies an invalid value.";

	// Token: 0x040001AB RID: 427
	public const string Lazy_StaticInit_InvalidOperation = "ValueFactory returned null.";

	// Token: 0x040001AC RID: 428
	public const string Lazy_ToString_ValueNotCreated = "Value is not created.";

	// Token: 0x040001AD RID: 429
	public const string Lazy_Value_RecursiveCallsToValue = "ValueFactory attempted to access the Value property of this instance.";

	// Token: 0x040001AE RID: 430
	public const string MissingConstructor_Name = "Constructor on type '{0}' not found.";

	// Token: 0x040001AF RID: 431
	public const string MustUseCCRewrite = "An assembly (probably '{1}') must be rewritten using the code contracts binary rewriter (CCRewrite) because it is calling Contract.{0} and the CONTRACTS_FULL symbol is defined.  Remove any explicit definitions of the CONTRACTS_FULL symbol from your project and rebuild.  CCRewrite can be downloaded from http://go.microsoft.com/fwlink/?LinkID=169180. \\r\\nAfter the rewriter is installed, it can be enabled in Visual Studio from the project's Properties page on the Code Contracts pane.  Ensure that 'Perform Runtime Contract Checking' is enabled, which will define CONTRACTS_FULL.";

	// Token: 0x040001B0 RID: 432
	public const string NotSupported_FixedSizeCollection = "Collection was of a fixed size.";

	// Token: 0x040001B1 RID: 433
	public const string NotSupported_MaxWaitHandles = "The number of WaitHandles must be less than or equal to 64.";

	// Token: 0x040001B2 RID: 434
	public const string NotSupported_NoCodepageData = "No data is available for encoding {0}. For information on defining a custom encoding, see the documentation for the Encoding.RegisterProvider method.";

	// Token: 0x040001B3 RID: 435
	public const string NotSupported_ReadOnlyCollection = "Collection is read-only.";

	// Token: 0x040001B4 RID: 436
	public const string NotSupported_StringComparison = "The string comparison type passed in is currently not supported.";

	// Token: 0x040001B5 RID: 437
	public const string NotSupported_VoidArray = "Arrays of System.Void are not supported.";

	// Token: 0x040001B6 RID: 438
	public const string NotSupported_ByRefLike = "Cannot create boxed ByRef-like values.";

	// Token: 0x040001B7 RID: 439
	public const string NotSupported_Type = "Type is not supported.";

	// Token: 0x040001B8 RID: 440
	public const string NotSupported_WaitAllSTAThread = "WaitAll for multiple handles on a STA thread is not supported.";

	// Token: 0x040001B9 RID: 441
	public const string ObjectDisposed_Generic = "Cannot access a disposed object.";

	// Token: 0x040001BA RID: 442
	public const string ObjectDisposed_ObjectName_Name = "Object name: '{0}'.";

	// Token: 0x040001BB RID: 443
	public const string Overflow_Byte = "Value was either too large or too small for an unsigned byte.";

	// Token: 0x040001BC RID: 444
	public const string Overflow_Char = "Value was either too large or too small for a character.";

	// Token: 0x040001BD RID: 445
	public const string Overflow_Decimal = "Value was either too large or too small for a Decimal.";

	// Token: 0x040001BE RID: 446
	public const string Overflow_Double = "Value was either too large or too small for a Double.";

	// Token: 0x040001BF RID: 447
	public const string Overflow_TimeSpanElementTooLarge = "The TimeSpan could not be parsed because at least one of the numeric components is out of range or contains too many digits.";

	// Token: 0x040001C0 RID: 448
	public const string Overflow_Duration = "The duration cannot be returned for TimeSpan.MinValue because the absolute value of TimeSpan.MinValue exceeds the value of TimeSpan.MaxValue.";

	// Token: 0x040001C1 RID: 449
	public const string Overflow_Int16 = "Value was either too large or too small for an Int16.";

	// Token: 0x040001C2 RID: 450
	public const string Overflow_Int32 = "Value was either too large or too small for an Int32.";

	// Token: 0x040001C3 RID: 451
	public const string Overflow_Int64 = "Value was either too large or too small for an Int64.";

	// Token: 0x040001C4 RID: 452
	public const string Overflow_NegateTwosCompNum = "Negating the minimum value of a twos complement number is invalid.";

	// Token: 0x040001C5 RID: 453
	public const string Overflow_NegativeUnsigned = "The string was being parsed as an unsigned number and could not have a negative sign.";

	// Token: 0x040001C6 RID: 454
	public const string Overflow_SByte = "Value was either too large or too small for a signed byte.";

	// Token: 0x040001C7 RID: 455
	public const string Overflow_Single = "Value was either too large or too small for a Single.";

	// Token: 0x040001C8 RID: 456
	public const string Overflow_TimeSpanTooLong = "TimeSpan overflowed because the duration is too long.";

	// Token: 0x040001C9 RID: 457
	public const string Overflow_UInt16 = "Value was either too large or too small for a UInt16.";

	// Token: 0x040001CA RID: 458
	public const string Overflow_UInt32 = "Value was either too large or too small for a UInt32.";

	// Token: 0x040001CB RID: 459
	public const string Overflow_UInt64 = "Value was either too large or too small for a UInt64.";

	// Token: 0x040001CC RID: 460
	public const string Rank_MultiDimNotSupported = "Only single dimension arrays are supported here.";

	// Token: 0x040001CD RID: 461
	public const string RuntimeWrappedException = "An object that does not derive from System.Exception has been wrapped in a RuntimeWrappedException.";

	// Token: 0x040001CE RID: 462
	public const string SpinWait_SpinUntil_ArgumentNull = "The condition argument is null.";

	// Token: 0x040001CF RID: 463
	public const string Serialization_CorruptField = "The value of the field '{0}' is invalid.  The serialized data is corrupt.";

	// Token: 0x040001D0 RID: 464
	public const string Serialization_InvalidData = "An error occurred while deserializing the object.  The serialized data is corrupt.";

	// Token: 0x040001D1 RID: 465
	public const string Serialization_InvalidEscapeSequence = "The serialized data contained an invalid escape sequence '\\\\{0}'.";

	// Token: 0x040001D2 RID: 466
	public const string Serialization_InvalidType = "Only system-provided types can be passed to the GetUninitializedObject method. '{0}' is not a valid instance of a type.";

	// Token: 0x040001D3 RID: 467
	public const string SpinWait_SpinUntil_TimeoutWrong = "The timeout must represent a value between -1 and Int32.MaxValue, inclusive.";

	// Token: 0x040001D4 RID: 468
	public const string Threading_AbandonedMutexException = "The wait completed due to an abandoned mutex.";

	// Token: 0x040001D5 RID: 469
	public const string Threading_SemaphoreFullException = "Adding the specified count to the semaphore would cause it to exceed its maximum count.";

	// Token: 0x040001D6 RID: 470
	public const string Threading_ThreadInterrupted = "Thread was interrupted from a waiting state.";

	// Token: 0x040001D7 RID: 471
	public const string Threading_WaitHandleCannotBeOpenedException = "No handle of the given name exists.";

	// Token: 0x040001D8 RID: 472
	public const string Threading_WaitHandleCannotBeOpenedException_InvalidHandle = "A WaitHandle with system-wide name '{0}' cannot be created. A WaitHandle of a different type might have the same name.";

	// Token: 0x040001D9 RID: 473
	public const string TimeZoneNotFound_MissingRegistryData = "The time zone ID '{0}' was not found on the local computer.";

	// Token: 0x040001DA RID: 474
	public const string TypeInitialization_Default = "Type constructor threw an exception.";

	// Token: 0x040001DB RID: 475
	public const string TypeInitialization_Type = "The type initializer for '{0}' threw an exception.";

	// Token: 0x040001DC RID: 476
	public const string TypeInitialization_Type_NoTypeAvailable = "A type initializer threw an exception. To determine which type, inspect the InnerException's StackTrace property.";

	// Token: 0x040001DD RID: 477
	public const string Verification_Exception = "Operation could destabilize the runtime.";

	// Token: 0x040001DE RID: 478
	public const string Arg_EnumFormatUnderlyingTypeAndObjectMustBeSameType = "Enum underlying type and the object must be same type or object. Type passed in was '{0}'; the enum underlying type was '{1}'.";

	// Token: 0x040001DF RID: 479
	public const string Format_InvalidEnumFormatSpecification = "Format String can be only 'G', 'g', 'X', 'x', 'F', 'f', 'D' or 'd'.";

	// Token: 0x040001E0 RID: 480
	public const string Arg_MustBeEnumBaseTypeOrEnum = "The value passed in must be an enum base or an underlying type for an enum, such as an Int32.";

	// Token: 0x040001E1 RID: 481
	public const string Arg_EnumUnderlyingTypeAndObjectMustBeSameType = "Enum underlying type and the object must be same type or object must be a String. Type passed in was '{0}'; the enum underlying type was '{1}'.";

	// Token: 0x040001E2 RID: 482
	public const string Arg_MustBeType = "Type must be a type provided by the runtime.";

	// Token: 0x040001E3 RID: 483
	public const string Arg_MustContainEnumInfo = "Must specify valid information for parsing in the string.";

	// Token: 0x040001E4 RID: 484
	public const string Arg_EnumValueNotFound = "Requested value '{0}' was not found.";

	// Token: 0x040001E5 RID: 485
	public const string Argument_StringZeroLength = "String cannot be of zero length.";

	// Token: 0x040001E6 RID: 486
	public const string Argument_StringFirstCharIsZero = "The first char in the string is the null character.";

	// Token: 0x040001E7 RID: 487
	public const string Argument_LongEnvVarValue = "Environment variable name or value is too long.";

	// Token: 0x040001E8 RID: 488
	public const string Argument_IllegalEnvVarName = "Environment variable name cannot contain equal character.";

	// Token: 0x040001E9 RID: 489
	public const string AssumptionFailed = "Assumption failed.";

	// Token: 0x040001EA RID: 490
	public const string AssumptionFailed_Cnd = "Assumption failed: {0}";

	// Token: 0x040001EB RID: 491
	public const string AssertionFailed = "Assertion failed.";

	// Token: 0x040001EC RID: 492
	public const string AssertionFailed_Cnd = "Assertion failed: {0}";

	// Token: 0x040001ED RID: 493
	public const string PreconditionFailed = "Precondition failed.";

	// Token: 0x040001EE RID: 494
	public const string PreconditionFailed_Cnd = "Precondition failed: {0}";

	// Token: 0x040001EF RID: 495
	public const string PostconditionFailed = "Postcondition failed.";

	// Token: 0x040001F0 RID: 496
	public const string PostconditionFailed_Cnd = "Postcondition failed: {0}";

	// Token: 0x040001F1 RID: 497
	public const string PostconditionOnExceptionFailed = "Postcondition failed after throwing an exception.";

	// Token: 0x040001F2 RID: 498
	public const string PostconditionOnExceptionFailed_Cnd = "Postcondition failed after throwing an exception: {0}";

	// Token: 0x040001F3 RID: 499
	public const string InvariantFailed = "Invariant failed.";

	// Token: 0x040001F4 RID: 500
	public const string InvariantFailed_Cnd = "Invariant failed: {0}";

	// Token: 0x040001F5 RID: 501
	public const string MissingEncodingNameResource = "Could not find a resource entry for the encoding codepage '{0} - {1}'";

	// Token: 0x040001F6 RID: 502
	public const string Globalization_cp_1200 = "Unicode";

	// Token: 0x040001F7 RID: 503
	public const string Globalization_cp_1201 = "Unicode (Big-Endian)";

	// Token: 0x040001F8 RID: 504
	public const string Globalization_cp_12000 = "Unicode (UTF-32)";

	// Token: 0x040001F9 RID: 505
	public const string Globalization_cp_12001 = "Unicode (UTF-32 Big-Endian)";

	// Token: 0x040001FA RID: 506
	public const string Globalization_cp_20127 = "US-ASCII";

	// Token: 0x040001FB RID: 507
	public const string Globalization_cp_28591 = "Western European (ISO)";

	// Token: 0x040001FC RID: 508
	public const string Globalization_cp_65000 = "Unicode (UTF-7)";

	// Token: 0x040001FD RID: 509
	public const string Globalization_cp_65001 = "Unicode (UTF-8)";

	// Token: 0x040001FE RID: 510
	public const string DebugAssertBanner = "---- DEBUG ASSERTION FAILED ----";

	// Token: 0x040001FF RID: 511
	public const string DebugAssertLongMessage = "---- Assert Long Message ----";

	// Token: 0x04000200 RID: 512
	public const string DebugAssertShortMessage = "---- Assert Short Message ----";

	// Token: 0x04000201 RID: 513
	public const string InvalidCast_Empty = "Object cannot be cast to Empty.";

	// Token: 0x04000202 RID: 514
	public const string Arg_UnknownTypeCode = "Unknown TypeCode value.";

	// Token: 0x04000203 RID: 515
	public const string Format_BadDatePattern = "Could not determine the order of year, month, and date from '{0}'.";

	// Token: 0x04000204 RID: 516
	public const string Format_BadDateTime = "String was not recognized as a valid DateTime.";

	// Token: 0x04000205 RID: 517
	public const string Format_BadDateTimeCalendar = "The DateTime represented by the string is not supported in calendar {0}.";

	// Token: 0x04000206 RID: 518
	public const string Format_BadDayOfWeek = "String was not recognized as a valid DateTime because the day of week was incorrect.";

	// Token: 0x04000207 RID: 519
	public const string Format_DateOutOfRange = "The DateTime represented by the string is out of range.";

	// Token: 0x04000208 RID: 520
	public const string Format_MissingIncompleteDate = "There must be at least a partial date with a year present in the input.";

	// Token: 0x04000209 RID: 521
	public const string Format_OffsetOutOfRange = "The time zone offset must be within plus or minus 14 hours.";

	// Token: 0x0400020A RID: 522
	public const string Format_RepeatDateTimePattern = "DateTime pattern '{0}' appears more than once with different values.";

	// Token: 0x0400020B RID: 523
	public const string Format_UnknowDateTimeWord = "The string was not recognized as a valid DateTime. There is an unknown word starting at index {0}.";

	// Token: 0x0400020C RID: 524
	public const string Format_UTCOutOfRange = "The UTC representation of the date falls outside the year range 1-9999.";

	// Token: 0x0400020D RID: 525
	public const string RFLCT_Ambiguous = "Ambiguous match found.";

	// Token: 0x0400020E RID: 526
	public const string AggregateException_ctor_DefaultMessage = "One or more errors occurred.";

	// Token: 0x0400020F RID: 527
	public const string AggregateException_ctor_InnerExceptionNull = "An element of innerExceptions was null.";

	// Token: 0x04000210 RID: 528
	public const string AggregateException_DeserializationFailure = "The serialization stream contains no inner exceptions.";

	// Token: 0x04000211 RID: 529
	public const string AggregateException_InnerException = "(Inner Exception #{0}) ";

	// Token: 0x04000212 RID: 530
	public const string ArgumentOutOfRange_TimeoutTooLarge = "Time-out interval must be less than 2^32-2.";

	// Token: 0x04000213 RID: 531
	public const string ArgumentOutOfRange_PeriodTooLarge = "Period must be less than 2^32-2.";

	// Token: 0x04000214 RID: 532
	public const string TaskScheduler_FromCurrentSynchronizationContext_NoCurrent = "The current SynchronizationContext may not be used as a TaskScheduler.";

	// Token: 0x04000215 RID: 533
	public const string TaskScheduler_ExecuteTask_WrongTaskScheduler = "ExecuteTask may not be called for a task which was previously queued to a different TaskScheduler.";

	// Token: 0x04000216 RID: 534
	public const string TaskScheduler_InconsistentStateAfterTryExecuteTaskInline = "The TryExecuteTaskInline call to the underlying scheduler succeeded, but the task body was not invoked.";

	// Token: 0x04000217 RID: 535
	public const string TaskSchedulerException_ctor_DefaultMessage = "An exception was thrown by a TaskScheduler.";

	// Token: 0x04000218 RID: 536
	public const string Task_MultiTaskContinuation_FireOptions = "It is invalid to exclude specific continuation kinds for continuations off of multiple tasks.";

	// Token: 0x04000219 RID: 537
	public const string Task_ContinueWith_ESandLR = "The specified TaskContinuationOptions combined LongRunning and ExecuteSynchronously.  Synchronous continuations should not be long running.";

	// Token: 0x0400021A RID: 538
	public const string Task_MultiTaskContinuation_EmptyTaskList = "The tasks argument contains no tasks.";

	// Token: 0x0400021B RID: 539
	public const string Task_MultiTaskContinuation_NullTask = "The tasks argument included a null value.";

	// Token: 0x0400021C RID: 540
	public const string Task_FromAsync_PreferFairness = "It is invalid to specify TaskCreationOptions.PreferFairness in calls to FromAsync.";

	// Token: 0x0400021D RID: 541
	public const string Task_FromAsync_LongRunning = "It is invalid to specify TaskCreationOptions.LongRunning in calls to FromAsync.";

	// Token: 0x0400021E RID: 542
	public const string AsyncMethodBuilder_InstanceNotInitialized = "The builder was not properly initialized.";

	// Token: 0x0400021F RID: 543
	public const string TaskT_TransitionToFinal_AlreadyCompleted = "An attempt was made to transition a task to a final state when it had already completed.";

	// Token: 0x04000220 RID: 544
	public const string TaskT_DebuggerNoResult = "{Not yet computed}";

	// Token: 0x04000221 RID: 545
	public const string OperationCanceled = "The operation was canceled.";

	// Token: 0x04000222 RID: 546
	public const string CancellationToken_CreateLinkedToken_TokensIsEmpty = "No tokens were supplied.";

	// Token: 0x04000223 RID: 547
	public const string CancellationTokenSource_Disposed = "The CancellationTokenSource has been disposed.";

	// Token: 0x04000224 RID: 548
	public const string CancellationToken_SourceDisposed = "The CancellationTokenSource associated with this CancellationToken has been disposed.";

	// Token: 0x04000225 RID: 549
	public const string TaskExceptionHolder_UnknownExceptionType = "(Internal)Expected an Exception or an IEnumerable<Exception>";

	// Token: 0x04000226 RID: 550
	public const string TaskExceptionHolder_UnhandledException = "A Task's exception(s) were not observed either by Waiting on the Task or accessing its Exception property. As a result, the unobserved exception was rethrown by the finalizer thread.";

	// Token: 0x04000227 RID: 551
	public const string Task_Delay_InvalidMillisecondsDelay = "The value needs to be either -1 (signifying an infinite timeout), 0 or a positive integer.";

	// Token: 0x04000228 RID: 552
	public const string Task_Delay_InvalidDelay = "The value needs to translate in milliseconds to -1 (signifying an infinite timeout), 0 or a positive integer less than or equal to Int32.MaxValue.";

	// Token: 0x04000229 RID: 553
	public const string Task_Dispose_NotCompleted = "A task may only be disposed if it is in a completion state (RanToCompletion, Faulted or Canceled).";

	// Token: 0x0400022A RID: 554
	public const string Task_WaitMulti_NullTask = "The tasks array included at least one null element.";

	// Token: 0x0400022B RID: 555
	public const string Task_ContinueWith_NotOnAnything = "The specified TaskContinuationOptions excluded all continuation kinds.";

	// Token: 0x0400022C RID: 556
	public const string Task_RunSynchronously_AlreadyStarted = "RunSynchronously may not be called on a task that was already started.";

	// Token: 0x0400022D RID: 557
	public const string Task_ThrowIfDisposed = "The task has been disposed.";

	// Token: 0x0400022E RID: 558
	public const string Task_RunSynchronously_TaskCompleted = "RunSynchronously may not be called on a task that has already completed.";

	// Token: 0x0400022F RID: 559
	public const string Task_RunSynchronously_Promise = "RunSynchronously may not be called on a task not bound to a delegate, such as the task returned from an asynchronous method.";

	// Token: 0x04000230 RID: 560
	public const string Task_RunSynchronously_Continuation = "RunSynchronously may not be called on a continuation task.";

	// Token: 0x04000231 RID: 561
	public const string Task_Start_AlreadyStarted = "Start may not be called on a task that was already started.";

	// Token: 0x04000232 RID: 562
	public const string Task_Start_ContinuationTask = "Start may not be called on a continuation task.";

	// Token: 0x04000233 RID: 563
	public const string Task_Start_Promise = "Start may not be called on a promise-style task.";

	// Token: 0x04000234 RID: 564
	public const string Task_Start_TaskCompleted = "Start may not be called on a task that has completed.";

	// Token: 0x04000235 RID: 565
	public const string TaskCanceledException_ctor_DefaultMessage = "A task was canceled.";

	// Token: 0x04000236 RID: 566
	public const string TaskCompletionSourceT_TrySetException_NoExceptions = "The exceptions collection was empty.";

	// Token: 0x04000237 RID: 567
	public const string TaskCompletionSourceT_TrySetException_NullException = "The exceptions collection included at least one null element.";

	// Token: 0x04000238 RID: 568
	public const string Argument_MinMaxValue = "'{0}' cannot be greater than {1}.";

	// Token: 0x04000239 RID: 569
	public const string ExecutionContext_ExceptionInAsyncLocalNotification = "An exception was not handled in an AsyncLocal<T> notification callback.";

	// Token: 0x0400023A RID: 570
	public const string InvalidOperation_WrongAsyncResultOrEndCalledMultiple = "Either the IAsyncResult object did not come from the corresponding async method on this type, or the End method was called multiple times with the same IAsyncResult.";

	// Token: 0x0400023B RID: 571
	public const string SpinLock_IsHeldByCurrentThread = "Thread tracking is disabled.";

	// Token: 0x0400023C RID: 572
	public const string SpinLock_TryEnter_LockRecursionException = "The calling thread already holds the lock.";

	// Token: 0x0400023D RID: 573
	public const string SpinLock_Exit_SynchronizationLockException = "The calling thread does not hold the lock.";

	// Token: 0x0400023E RID: 574
	public const string SpinLock_TryReliableEnter_ArgumentException = "The tookLock argument must be set to false before calling this method.";

	// Token: 0x0400023F RID: 575
	public const string SpinLock_TryEnter_ArgumentOutOfRange = "The timeout must be a value between -1 and Int32.MaxValue, inclusive.";

	// Token: 0x04000240 RID: 576
	public const string ManualResetEventSlim_Disposed = "The event has been disposed.";

	// Token: 0x04000241 RID: 577
	public const string ManualResetEventSlim_ctor_SpinCountOutOfRange = "The spinCount argument must be in the range 0 to {0}, inclusive.";

	// Token: 0x04000242 RID: 578
	public const string ManualResetEventSlim_ctor_TooManyWaiters = "There are too many threads currently waiting on the event. A maximum of {0} waiting threads are supported.";

	// Token: 0x04000243 RID: 579
	public const string InvalidOperation_SendNotSupportedOnWindowsRTSynchronizationContext = "Send is not supported in the Windows Runtime SynchronizationContext";

	// Token: 0x04000244 RID: 580
	public const string InvalidOperation_SetData_OnlyOnce = "SetData can only be used to set the value of a given name once.";

	// Token: 0x04000245 RID: 581
	public const string SemaphoreSlim_Disposed = "The semaphore has been disposed.";

	// Token: 0x04000246 RID: 582
	public const string SemaphoreSlim_Release_CountWrong = "The releaseCount argument must be greater than zero.";

	// Token: 0x04000247 RID: 583
	public const string SemaphoreSlim_Wait_TimeoutWrong = "The timeout must represent a value between -1 and Int32.MaxValue, inclusive.";

	// Token: 0x04000248 RID: 584
	public const string SemaphoreSlim_ctor_MaxCountWrong = "The maximumCount argument must be a positive number. If a maximum is not required, use the constructor without a maxCount parameter.";

	// Token: 0x04000249 RID: 585
	public const string SemaphoreSlim_ctor_InitialCountWrong = "The initialCount argument must be non-negative and less than or equal to the maximumCount.";

	// Token: 0x0400024A RID: 586
	public const string ThreadLocal_ValuesNotAvailable = "The ThreadLocal object is not tracking values. To use the Values property, use a ThreadLocal constructor that accepts the trackAllValues parameter and set the parameter to true.";

	// Token: 0x0400024B RID: 587
	public const string ThreadLocal_Value_RecursiveCallsToValue = "ValueFactory attempted to access the Value property of this instance.";

	// Token: 0x0400024C RID: 588
	public const string ThreadLocal_Disposed = "The ThreadLocal object has been disposed.";

	// Token: 0x0400024D RID: 589
	public const string LockRecursionException_WriteAfterReadNotAllowed = "Write lock may not be acquired with read lock held. This pattern is prone to deadlocks. Please ensure that read locks are released before taking a write lock. If an upgrade is necessary, use an upgrade lock in place of the read lock.";

	// Token: 0x0400024E RID: 590
	public const string LockRecursionException_RecursiveWriteNotAllowed = "Recursive write lock acquisitions not allowed in this mode.";

	// Token: 0x0400024F RID: 591
	public const string LockRecursionException_ReadAfterWriteNotAllowed = "A read lock may not be acquired with the write lock held in this mode.";

	// Token: 0x04000250 RID: 592
	public const string LockRecursionException_RecursiveUpgradeNotAllowed = "Recursive upgradeable lock acquisitions not allowed in this mode.";

	// Token: 0x04000251 RID: 593
	public const string LockRecursionException_RecursiveReadNotAllowed = "Recursive read lock acquisitions not allowed in this mode.";

	// Token: 0x04000252 RID: 594
	public const string SynchronizationLockException_IncorrectDispose = "The lock is being disposed while still being used. It either is being held by a thread and/or has active waiters waiting to acquire the lock.";

	// Token: 0x04000253 RID: 595
	public const string SynchronizationLockException_MisMatchedWrite = "The write lock is being released without being held.";

	// Token: 0x04000254 RID: 596
	public const string LockRecursionException_UpgradeAfterReadNotAllowed = "Upgradeable lock may not be acquired with read lock held.";

	// Token: 0x04000255 RID: 597
	public const string LockRecursionException_UpgradeAfterWriteNotAllowed = "Upgradeable lock may not be acquired with write lock held in this mode. Acquiring Upgradeable lock gives the ability to read along with an option to upgrade to a writer.";

	// Token: 0x04000256 RID: 598
	public const string SynchronizationLockException_MisMatchedUpgrade = "The upgradeable lock is being released without being held.";

	// Token: 0x04000257 RID: 599
	public const string SynchronizationLockException_MisMatchedRead = "The read lock is being released without being held.";

	// Token: 0x04000258 RID: 600
	public const string InvalidOperation_TimeoutsNotSupported = "Timeouts are not supported on this stream.";

	// Token: 0x04000259 RID: 601
	public const string NotSupported_UnreadableStream = "Stream does not support reading.";

	// Token: 0x0400025A RID: 602
	public const string NotSupported_UnwritableStream = "Stream does not support writing.";

	// Token: 0x0400025B RID: 603
	public const string ObjectDisposed_StreamClosed = "Cannot access a closed Stream.";

	// Token: 0x0400025C RID: 604
	public const string NotSupported_SubclassOverride = "Derived classes must provide an implementation.";

	// Token: 0x0400025D RID: 605
	public const string InvalidOperation_NoPublicRemoveMethod = "Cannot remove the event handler since no public remove method exists for the event.";

	// Token: 0x0400025E RID: 606
	public const string InvalidOperation_NoPublicAddMethod = "Cannot add the event handler since no public add method exists for the event.";

	// Token: 0x0400025F RID: 607
	public const string SerializationException = "Serialization error.";

	// Token: 0x04000260 RID: 608
	public const string Serialization_NotFound = "Member '{0}' was not found.";

	// Token: 0x04000261 RID: 609
	public const string Serialization_OptionalFieldVersionValue = "Version value must be positive.";

	// Token: 0x04000262 RID: 610
	public const string Serialization_SameNameTwice = "Cannot add the same member twice to a SerializationInfo object.";

	// Token: 0x04000263 RID: 611
	public const string NotSupported_AbstractNonCLS = "This non-CLS method is not implemented.";

	// Token: 0x04000264 RID: 612
	public const string NotSupported_NoTypeInfo = "Cannot resolve {0} to a TypeInfo object.";

	// Token: 0x04000265 RID: 613
	public const string Arg_CustomAttributeFormatException = "Binary format of the specified custom attribute was invalid.";

	// Token: 0x04000266 RID: 614
	public const string Argument_InvalidMemberForNamedArgument = "The member must be either a field or a property.";

	// Token: 0x04000267 RID: 615
	public const string Arg_InvalidFilterCriteriaException = "Specified filter criteria was invalid.";

	// Token: 0x04000268 RID: 616
	public const string Arg_ParmArraySize = "Must specify one or more parameters.";

	// Token: 0x04000269 RID: 617
	public const string Arg_MustBePointer = "Type must be a Pointer.";

	// Token: 0x0400026A RID: 618
	public const string Arg_InvalidHandle = "Invalid handle.";

	// Token: 0x0400026B RID: 619
	public const string Argument_InvalidEnum = "The Enum type should contain one and only one instance field.";

	// Token: 0x0400026C RID: 620
	public const string Argument_MustHaveAttributeBaseClass = "Type passed in must be derived from System.Attribute or System.Attribute itself.";

	// Token: 0x0400026D RID: 621
	public const string InvalidFilterCriteriaException_CritString = "A String must be provided for the filter criteria.";

	// Token: 0x0400026E RID: 622
	public const string InvalidFilterCriteriaException_CritInt = "An Int32 must be provided for the filter criteria.";

	// Token: 0x0400026F RID: 623
	public const string InvalidOperation_NotSupportedOnWinRTEvent = "Adding or removing event handlers dynamically is not supported on WinRT events.";

	// Token: 0x04000270 RID: 624
	public const string PlatformNotSupported_ReflectionOnly = "ReflectionOnly loading is not supported on this platform.";

	// Token: 0x04000271 RID: 625
	public const string PlatformNotSupported_OSXFileLocking = "Locking/unlocking file regions is not supported on this platform. Use FileShare on the entire file instead.";

	// Token: 0x04000272 RID: 626
	public const string MissingMember_Name = "Member '{0}' not found.";

	// Token: 0x04000273 RID: 627
	public const string MissingMethod_Name = "Method '{0}' not found.";

	// Token: 0x04000274 RID: 628
	public const string MissingField_Name = "Field '{0}' not found.";

	// Token: 0x04000275 RID: 629
	public const string Format_StringZeroLength = "String cannot have zero length.";

	// Token: 0x04000276 RID: 630
	public const string Security_CannotReadRegistryData = "The time zone ID '{0}' was found on the local computer, but the application does not have permission to read the registry information.";

	// Token: 0x04000277 RID: 631
	public const string Security_InvalidAssemblyPublicKey = "Invalid assembly public key.";

	// Token: 0x04000278 RID: 632
	public const string Security_RegistryPermission = "Requested registry access is not allowed.";

	// Token: 0x04000279 RID: 633
	public const string ClassLoad_General = "Could not load type '{0}' from assembly '{1}'.";

	// Token: 0x0400027A RID: 634
	public const string ClassLoad_RankTooLarge = "'{0}' from assembly '{1}' has too many dimensions.";

	// Token: 0x0400027B RID: 635
	public const string ClassLoad_ExplicitGeneric = "Could not load type '{0}' from assembly '{1}' because generic types cannot have explicit layout.";

	// Token: 0x0400027C RID: 636
	public const string ClassLoad_BadFormat = "Could not load type '{0}' from assembly '{1}' because the format is invalid.";

	// Token: 0x0400027D RID: 637
	public const string ClassLoad_ValueClassTooLarge = "Array of type '{0}' from assembly '{1}' cannot be created because base value type is too large.";

	// Token: 0x0400027E RID: 638
	public const string ClassLoad_ExplicitLayout = "Could not load type '{0}' from assembly '{1}' because it contains an object field at offset '{2}' that is incorrectly aligned or overlapped by a non-object field.";

	// Token: 0x0400027F RID: 639
	public const string EE_MissingMethod = "Method not found: '{0}'.";

	// Token: 0x04000280 RID: 640
	public const string EE_MissingField = "Field not found: '{0}'.";

	// Token: 0x04000281 RID: 641
	public const string UnauthorizedAccess_RegistryKeyGeneric_Key = "Access to the registry key '{0}' is denied.";

	// Token: 0x04000282 RID: 642
	public const string UnknownError_Num = "Unknown error '{0}'.";

	// Token: 0x04000283 RID: 643
	public const string Argument_NeedStructWithNoRefs = "The specified Type must be a struct containing no references.";

	// Token: 0x04000284 RID: 644
	public const string ArgumentNull_Buffer = "Buffer cannot be null.";

	// Token: 0x04000285 RID: 645
	public const string ArgumentOutOfRange_AddressSpace = "The number of bytes cannot exceed the virtual address space on a 32 bit machine.";

	// Token: 0x04000286 RID: 646
	public const string ArgumentOutOfRange_UIntPtrMaxMinusOne = "The length of the buffer must be less than the maximum UIntPtr value for your platform.";

	// Token: 0x04000287 RID: 647
	public const string Arg_BufferTooSmall = "Not enough space available in the buffer.";

	// Token: 0x04000288 RID: 648
	public const string InvalidOperation_MustCallInitialize = "You must call Initialize on this object instance before using it.";

	// Token: 0x04000289 RID: 649
	public const string ArgumentException_BufferNotFromPool = "The buffer is not associated with this pool and may not be returned to it.";

	// Token: 0x0400028A RID: 650
	public const string Argument_InvalidSafeBufferOffLen = "Offset and length were greater than the size of the SafeBuffer.";

	// Token: 0x0400028B RID: 651
	public const string Argument_InvalidSeekOrigin = "Invalid seek origin.";

	// Token: 0x0400028C RID: 652
	public const string Argument_NotEnoughBytesToRead = "There are not enough bytes remaining in the accessor to read at this position.";

	// Token: 0x0400028D RID: 653
	public const string Argument_NotEnoughBytesToWrite = "There are not enough bytes remaining in the accessor to write at this position.";

	// Token: 0x0400028E RID: 654
	public const string Argument_OffsetAndCapacityOutOfBounds = "Offset and capacity were greater than the size of the view.";

	// Token: 0x0400028F RID: 655
	public const string ArgumentOutOfRange_UnmanagedMemStreamLength = "UnmanagedMemoryStream length must be non-negative and less than 2^63 - 1 - baseAddress.";

	// Token: 0x04000290 RID: 656
	public const string Argument_UnmanagedMemAccessorWrapAround = "The UnmanagedMemoryAccessor capacity and offset would wrap around the high end of the address space.";

	// Token: 0x04000291 RID: 657
	public const string ArgumentOutOfRange_StreamLength = "Stream length must be non-negative and less than 2^31 - 1 - origin.";

	// Token: 0x04000292 RID: 658
	public const string ArgumentOutOfRange_UnmanagedMemStreamWrapAround = "The UnmanagedMemoryStream capacity would wrap around the high end of the address space.";

	// Token: 0x04000293 RID: 659
	public const string InvalidOperation_CalledTwice = "The method cannot be called twice on the same instance.";

	// Token: 0x04000294 RID: 660
	public const string IO_FixedCapacity = "Unable to expand length of this stream beyond its capacity.";

	// Token: 0x04000295 RID: 661
	public const string IO_SeekBeforeBegin = "An attempt was made to move the position before the beginning of the stream.";

	// Token: 0x04000296 RID: 662
	public const string IO_StreamTooLong = "Stream was too long.";

	// Token: 0x04000297 RID: 663
	public const string Arg_BadDecimal = "Read an invalid decimal value from the buffer.";

	// Token: 0x04000298 RID: 664
	public const string NotSupported_Reading = "Accessor does not support reading.";

	// Token: 0x04000299 RID: 665
	public const string NotSupported_UmsSafeBuffer = "This operation is not supported for an UnmanagedMemoryStream created from a SafeBuffer.";

	// Token: 0x0400029A RID: 666
	public const string NotSupported_Writing = "Accessor does not support writing.";

	// Token: 0x0400029B RID: 667
	public const string NotSupported_UnseekableStream = "Stream does not support seeking.";

	// Token: 0x0400029C RID: 668
	public const string IndexOutOfRange_UMSPosition = "Unmanaged memory stream position was beyond the capacity of the stream.";

	// Token: 0x0400029D RID: 669
	public const string ObjectDisposed_StreamIsClosed = "Cannot access a closed Stream.";

	// Token: 0x0400029E RID: 670
	public const string ObjectDisposed_ViewAccessorClosed = "Cannot access a closed accessor.";

	// Token: 0x0400029F RID: 671
	public const string ArgumentOutOfRange_PositionLessThanCapacityRequired = "The position may not be greater or equal to the capacity of the accessor.";

	// Token: 0x040002A0 RID: 672
	public const string IO_EOF_ReadBeyondEOF = "Unable to read beyond the end of the stream.";

	// Token: 0x040002A1 RID: 673
	public const string Arg_EndOfStreamException = "Attempted to read past the end of the stream.";

	// Token: 0x040002A2 RID: 674
	public const string ObjectDisposed_FileClosed = "Cannot access a closed file.";

	// Token: 0x040002A3 RID: 675
	public const string Arg_InvalidSearchPattern = "Search pattern cannot contain \\\"..\\\" to move up directories and can be contained only internally in file/directory names, as in \\\"a..b\\\".";

	// Token: 0x040002A4 RID: 676
	public const string ArgumentOutOfRange_FileLengthTooBig = "Specified file length was too large for the file system.";

	// Token: 0x040002A5 RID: 677
	public const string Argument_InvalidHandle = "'handle' has been disposed or is an invalid handle.";

	// Token: 0x040002A6 RID: 678
	public const string Argument_AlreadyBoundOrSyncHandle = "'handle' has already been bound to the thread pool, or was not opened for asynchronous I/O.";

	// Token: 0x040002A7 RID: 679
	public const string Argument_PreAllocatedAlreadyAllocated = "'preAllocated' is already in use.";

	// Token: 0x040002A8 RID: 680
	public const string Argument_NativeOverlappedAlreadyFree = "'overlapped' has already been freed.";

	// Token: 0x040002A9 RID: 681
	public const string Argument_NativeOverlappedWrongBoundHandle = "'overlapped' was not allocated by this ThreadPoolBoundHandle instance.";

	// Token: 0x040002AA RID: 682
	public const string Arg_HandleNotAsync = "Handle does not support asynchronous operations. The parameters to the FileStream constructor may need to be changed to indicate that the handle was opened synchronously (that is, it was not opened for overlapped I/O).";

	// Token: 0x040002AB RID: 683
	public const string ArgumentNull_Path = "Path cannot be null.";

	// Token: 0x040002AC RID: 684
	public const string Argument_EmptyPath = "Empty path name is not legal.";

	// Token: 0x040002AD RID: 685
	public const string Argument_InvalidFileModeAndAccessCombo = "Combining FileMode: {0} with FileAccess: {1} is invalid.";

	// Token: 0x040002AE RID: 686
	public const string Argument_InvalidAppendMode = "Append access can be requested only in write-only mode.";

	// Token: 0x040002AF RID: 687
	public const string IO_UnknownFileName = "[Unknown]";

	// Token: 0x040002B0 RID: 688
	public const string IO_FileStreamHandlePosition = "The OS handle's position is not what FileStream expected. Do not use a handle simultaneously in one FileStream and in Win32 code or another FileStream. This may cause data loss.";

	// Token: 0x040002B1 RID: 689
	public const string NotSupported_FileStreamOnNonFiles = "FileStream was asked to open a device that was not a file. For support for devices like 'com1:' or 'lpt1:', call CreateFile, then use the FileStream constructors that take an OS handle as an IntPtr.";

	// Token: 0x040002B2 RID: 690
	public const string IO_BindHandleFailed = "BindHandle for ThreadPool failed on this handle.";

	// Token: 0x040002B3 RID: 691
	public const string Arg_HandleNotSync = "Handle does not support synchronous operations. The parameters to the FileStream constructor may need to be changed to indicate that the handle was opened asynchronously (that is, it was opened explicitly for overlapped I/O).";

	// Token: 0x040002B4 RID: 692
	public const string IO_SetLengthAppendTruncate = "Unable to truncate data that previously existed in a file opened in Append mode.";

	// Token: 0x040002B5 RID: 693
	public const string IO_SeekAppendOverwrite = "Unable seek backward to overwrite data that previously existed in a file opened in Append mode.";

	// Token: 0x040002B6 RID: 694
	public const string IO_FileTooLongOrHandleNotSync = "IO operation will not work. Most likely the file will become too long or the handle was not opened to support synchronous IO operations.";

	// Token: 0x040002B7 RID: 695
	public const string IndexOutOfRange_IORaceCondition = "Probable I/O race condition detected while copying memory. The I/O package is not thread safe by default. In multithreaded applications, a stream must be accessed in a thread-safe way, such as a thread-safe wrapper returned by TextReader's or TextWriter's Synchronized methods. This also applies to classes like StreamWriter and StreamReader.";

	// Token: 0x040002B8 RID: 696
	public const string Arg_ResourceFileUnsupportedVersion = "The ResourceReader class does not know how to read this version of .resources files.";

	// Token: 0x040002B9 RID: 697
	public const string Resources_StreamNotValid = "Stream is not a valid resource file.";

	// Token: 0x040002BA RID: 698
	public const string BadImageFormat_ResourcesHeaderCorrupted = "Corrupt .resources file. Unable to read resources from this file because of invalid header information. Try regenerating the .resources file.";

	// Token: 0x040002BB RID: 699
	public const string Argument_StreamNotReadable = "Stream was not readable.";

	// Token: 0x040002BC RID: 700
	public const string BadImageFormat_NegativeStringLength = "Corrupt .resources file. String length must be non-negative.";

	// Token: 0x040002BD RID: 701
	public const string BadImageFormat_ResourcesNameInvalidOffset = "Corrupt .resources file. The Invalid offset into name section is .";

	// Token: 0x040002BE RID: 702
	public const string BadImageFormat_TypeMismatch = "Corrupt .resources file.  The specified type doesn't match the available data in the stream.";

	// Token: 0x040002BF RID: 703
	public const string BadImageFormat_ResourceNameCorrupted_NameIndex = "Corrupt .resources file. The resource name for name index that extends past the end of the stream is ";

	// Token: 0x040002C0 RID: 704
	public const string BadImageFormat_ResourcesDataInvalidOffset = "Corrupt .resources file. Invalid offset  into data section is ";

	// Token: 0x040002C1 RID: 705
	public const string Format_Bad7BitInt32 = "Too many bytes in what should have been a 7 bit encoded Int32.";

	// Token: 0x040002C2 RID: 706
	public const string BadImageFormat_InvalidType = "Corrupt .resources file.  The specified type doesn't exist.";

	// Token: 0x040002C3 RID: 707
	public const string ResourceReaderIsClosed = "ResourceReader is closed.";

	// Token: 0x040002C4 RID: 708
	public const string Arg_MissingManifestResourceException = "Unable to find manifest resource.";

	// Token: 0x040002C5 RID: 709
	public const string Serialization_MissingKeys = "The keys for this dictionary are missing.";

	// Token: 0x040002C6 RID: 710
	public const string Serialization_NullKey = "One of the serialized keys is null.";

	// Token: 0x040002C7 RID: 711
	public const string NotSupported_KeyCollectionSet = "Mutating a key collection derived from a dictionary is not allowed.";

	// Token: 0x040002C8 RID: 712
	public const string NotSupported_ValueCollectionSet = "Mutating a value collection derived from a dictionary is not allowed.";

	// Token: 0x040002C9 RID: 713
	public const string IO_IO_StreamTooLong = "Stream was too long.";

	// Token: 0x040002CA RID: 714
	public const string UnauthorizedAccess_MemStreamBuffer = "MemoryStream's internal buffer cannot be accessed.";

	// Token: 0x040002CB RID: 715
	public const string NotSupported_MemStreamNotExpandable = "Memory stream is not expandable.";

	// Token: 0x040002CC RID: 716
	public const string IO_IO_SeekBeforeBegin = "An attempt was made to move the position before the beginning of the stream.";

	// Token: 0x040002CD RID: 717
	public const string ArgumentNull_Stream = "Stream cannot be null.";

	// Token: 0x040002CE RID: 718
	public const string IO_IO_InvalidStringLen_Len = "BinaryReader encountered an invalid string length of {0} characters.";

	// Token: 0x040002CF RID: 719
	public const string ArgumentOutOfRange_BinaryReaderFillBuffer = "The number of bytes requested does not fit into BinaryReader's internal buffer.";

	// Token: 0x040002D0 RID: 720
	public const string Serialization_InsufficientDeserializationState = "Insufficient state to deserialize the object. Missing field '{0}'.";

	// Token: 0x040002D1 RID: 721
	public const string NotSupported_UnitySerHolder = "The UnitySerializationHolder object is designed to transmit information about other types and is not serializable itself.";

	// Token: 0x040002D2 RID: 722
	public const string Serialization_UnableToFindModule = "The given module {0} cannot be found within the assembly {1}.";

	// Token: 0x040002D3 RID: 723
	public const string Argument_InvalidUnity = "Invalid Unity type.";

	// Token: 0x040002D4 RID: 724
	public const string InvalidOperation_InvalidHandle = "The handle is invalid.";

	// Token: 0x040002D5 RID: 725
	public const string PlatformNotSupported_NamedSynchronizationPrimitives = "The named version of this synchronization primitive is not supported on this platform.";

	// Token: 0x040002D6 RID: 726
	public const string InvalidOperation_EmptyQueue = "Queue empty.";

	// Token: 0x040002D7 RID: 727
	public const string Overflow_MutexReacquireCount = "The current thread attempted to reacquire a mutex that has reached its maximum acquire count.";

	// Token: 0x040002D8 RID: 728
	public const string Serialization_InsufficientState = "Insufficient state to return the real object.";

	// Token: 0x040002D9 RID: 729
	public const string Serialization_UnknownMember = "Cannot get the member '{0}'.";

	// Token: 0x040002DA RID: 730
	public const string Serialization_NullSignature = "The method signature cannot be null.";

	// Token: 0x040002DB RID: 731
	public const string Serialization_MemberTypeNotRecognized = "Unknown member type.";

	// Token: 0x040002DC RID: 732
	public const string Serialization_BadParameterInfo = "Non existent ParameterInfo. Position bigger than member's parameters length.";

	// Token: 0x040002DD RID: 733
	public const string Serialization_NoParameterInfo = "Serialized member does not have a ParameterInfo.";

	// Token: 0x040002DE RID: 734
	public const string ArgumentNull_Assembly = "Assembly cannot be null.";

	// Token: 0x040002DF RID: 735
	public const string Arg_InvalidNeutralResourcesLanguage_Asm_Culture = "The NeutralResourcesLanguageAttribute on the assembly \"{0}\" specifies an invalid culture name: \"{1}\".";

	// Token: 0x040002E0 RID: 736
	public const string Arg_InvalidNeutralResourcesLanguage_FallbackLoc = "The NeutralResourcesLanguageAttribute specifies an invalid or unrecognized ultimate resource fallback location: \"{0}\".";

	// Token: 0x040002E1 RID: 737
	public const string Arg_InvalidSatelliteContract_Asm_Ver = "Satellite contract version attribute on the assembly '{0}' specifies an invalid version: {1}.";

	// Token: 0x040002E2 RID: 738
	public const string Arg_ResMgrNotResSet = "Type parameter must refer to a subclass of ResourceSet.";

	// Token: 0x040002E3 RID: 739
	public const string BadImageFormat_ResourceNameCorrupted = "Corrupt .resources file. A resource name extends past the end of the stream.";

	// Token: 0x040002E4 RID: 740
	public const string BadImageFormat_ResourcesNameTooLong = "Corrupt .resources file. Resource name extends past the end of the file.";

	// Token: 0x040002E5 RID: 741
	public const string InvalidOperation_ResMgrBadResSet_Type = "'{0}': ResourceSet derived classes must provide a constructor that takes a String file name and a constructor that takes a Stream.";

	// Token: 0x040002E6 RID: 742
	public const string InvalidOperation_ResourceNotStream_Name = "Resource '{0}' was not a Stream - call GetObject instead.";

	// Token: 0x040002E7 RID: 743
	public const string MissingManifestResource_MultipleBlobs = "A case-insensitive lookup for resource file \"{0}\" in assembly \"{1}\" found multiple entries. Remove the duplicates or specify the exact case.";

	// Token: 0x040002E8 RID: 744
	public const string MissingManifestResource_NoNeutralAsm = "Could not find any resources appropriate for the specified culture or the neutral culture.  Make sure \"{0}\" was correctly embedded or linked into assembly \"{1}\" at compile time, or that all the satellite assemblies required are loadable and fully signed.";

	// Token: 0x040002E9 RID: 745
	public const string MissingManifestResource_NoNeutralDisk = "Could not find any resources appropriate for the specified culture (or the neutral culture) on disk.";

	// Token: 0x040002EA RID: 746
	public const string MissingManifestResource_NoPRIresources = "Unable to open Package Resource Index.";

	// Token: 0x040002EB RID: 747
	public const string MissingManifestResource_ResWFileNotLoaded = "Unable to load resources for resource file \"{0}\" in package \"{1}\".";

	// Token: 0x040002EC RID: 748
	public const string MissingSatelliteAssembly_Culture_Name = "The satellite assembly named \"{1}\" for fallback culture \"{0}\" either could not be found or could not be loaded. This is generally a setup problem. Please consider reinstalling or repairing the application.";

	// Token: 0x040002ED RID: 749
	public const string MissingSatelliteAssembly_Default = "Resource lookup fell back to the ultimate fallback resources in a satellite assembly, but that satellite either was not found or could not be loaded. Please consider reinstalling or repairing the application.";

	// Token: 0x040002EE RID: 750
	public const string NotSupported_ObsoleteResourcesFile = "Found an obsolete .resources file in assembly '{0}'. Rebuild that .resources file then rebuild that assembly.";

	// Token: 0x040002EF RID: 751
	public const string NotSupported_ResourceObjectSerialization = "Cannot read resources that depend on serialization.";

	// Token: 0x040002F0 RID: 752
	public const string ObjectDisposed_ResourceSet = "Cannot access a closed resource set.";

	// Token: 0x040002F1 RID: 753
	public const string Arg_ResourceNameNotExist = "The specified resource name \"{0}\" does not exist in the resource file.";

	// Token: 0x040002F2 RID: 754
	public const string BadImageFormat_ResourceDataLengthInvalid = "Corrupt .resources file.  The specified data length '{0}' is not a valid position in the stream.";

	// Token: 0x040002F3 RID: 755
	public const string BadImageFormat_ResourcesIndexTooLong = "Corrupt .resources file. String for name index '{0}' extends past the end of the file.";

	// Token: 0x040002F4 RID: 756
	public const string InvalidOperation_ResourceNotString_Name = "Resource '{0}' was not a String - call GetObject instead.";

	// Token: 0x040002F5 RID: 757
	public const string InvalidOperation_ResourceNotString_Type = "Resource was of type '{0}' instead of String - call GetObject instead.";

	// Token: 0x040002F6 RID: 758
	public const string NotSupported_WrongResourceReader_Type = "This .resources file should not be read with this reader. The resource reader type is \"{0}\".";

	// Token: 0x040002F7 RID: 759
	public const string Arg_MustBeDelegate = "Type must derive from Delegate.";

	// Token: 0x040002F8 RID: 760
	public const string NotSupported_GlobalMethodSerialization = "Serialization of global methods (including implicit serialization via the use of asynchronous delegates) is not supported.";

	// Token: 0x040002F9 RID: 761
	public const string NotSupported_DelegateSerHolderSerial = "DelegateSerializationHolder objects are designed to represent a delegate during serialization and are not serializable themselves.";

	// Token: 0x040002FA RID: 762
	public const string DelegateSer_InsufficientMetadata = "The delegate cannot be serialized properly due to missing metadata for the target method.";

	// Token: 0x040002FB RID: 763
	public const string Argument_NoUninitializedStrings = "Uninitialized Strings cannot be created.";

	// Token: 0x040002FC RID: 764
	public const string ArgumentOutOfRangeException_NoGCRegionSizeTooLarge = "totalSize is too large. For more information about setting the maximum size, see \\\"Latency Modes\\\" in http://go.microsoft.com/fwlink/?LinkId=522706.";

	// Token: 0x040002FD RID: 765
	public const string InvalidOperationException_AlreadyInNoGCRegion = "The NoGCRegion mode was already in progress.";

	// Token: 0x040002FE RID: 766
	public const string InvalidOperationException_NoGCRegionAllocationExceeded = "Allocated memory exceeds specified memory for NoGCRegion mode.";

	// Token: 0x040002FF RID: 767
	public const string InvalidOperationException_NoGCRegionInduced = "Garbage collection was induced in NoGCRegion mode.";

	// Token: 0x04000300 RID: 768
	public const string InvalidOperationException_NoGCRegionNotInProgress = "NoGCRegion mode must be set.";

	// Token: 0x04000301 RID: 769
	public const string InvalidOperationException_SetLatencyModeNoGC = "The NoGCRegion mode is in progress. End it and then set a different mode.";

	// Token: 0x04000302 RID: 770
	public const string InvalidOperation_NotWithConcurrentGC = "This API is not available when the concurrent GC is enabled.";

	// Token: 0x04000303 RID: 771
	public const string ThreadState_AlreadyStarted = "Thread is running or terminated; it cannot restart.";

	// Token: 0x04000304 RID: 772
	public const string ThreadState_Dead_Priority = "Thread is dead; priority cannot be accessed.";

	// Token: 0x04000305 RID: 773
	public const string ThreadState_Dead_State = "Thread is dead; state cannot be accessed.";

	// Token: 0x04000306 RID: 774
	public const string ThreadState_NotStarted = "Thread has not been started.";

	// Token: 0x04000307 RID: 775
	public const string ThreadState_SetPriorityFailed = "Unable to set thread priority.";

	// Token: 0x04000308 RID: 776
	public const string Serialization_InvalidFieldState = "Object fields may not be properly initialized.";

	// Token: 0x04000309 RID: 777
	public const string Acc_CreateAbst = "Cannot create an abstract class.";

	// Token: 0x0400030A RID: 778
	public const string Acc_CreateGeneric = "Cannot create a type for which Type.ContainsGenericParameters is true.";

	// Token: 0x0400030B RID: 779
	public const string Argument_InvalidValue = "Value was invalid.";

	// Token: 0x0400030C RID: 780
	public const string NotSupported_ManagedActivation = "Cannot create uninitialized instances of types requiring managed activation.";

	// Token: 0x0400030D RID: 781
	public const string PlatformNotSupported_ResourceManager_ResWFileUnsupportedMethod = "ResourceManager method '{0}' is not supported when reading from .resw resource files.";

	// Token: 0x0400030E RID: 782
	public const string PlatformNotSupported_ResourceManager_ResWFileUnsupportedProperty = "ResourceManager property '{0}' is not supported when reading from .resw resource files.";

	// Token: 0x0400030F RID: 783
	public const string Serialization_NonSerType = "Type '{0}' in Assembly '{1}' is not marked as serializable.";

	// Token: 0x04000310 RID: 784
	public const string InvalidCast_DBNull = "Object cannot be cast to DBNull.";

	// Token: 0x04000311 RID: 785
	public const string NotSupported_NYI = "This feature is not currently implemented.";

	// Token: 0x04000312 RID: 786
	public const string Delegate_GarbageCollected = "The corresponding delegate has been garbage collected. Please make sure the delegate is still referenced by managed code when you are using the marshalled native function pointer.";

	// Token: 0x04000313 RID: 787
	public const string Arg_AmbiguousMatchException = "Ambiguous match found.";

	// Token: 0x04000314 RID: 788
	public const string NotSupported_ChangeType = "ChangeType operation is not supported.";

	// Token: 0x04000315 RID: 789
	public const string Arg_EmptyArray = "Array may not be empty.";

	// Token: 0x04000316 RID: 790
	public const string MissingMember = "Member not found.";

	// Token: 0x04000317 RID: 791
	public const string MissingField = "Field not found.";

	// Token: 0x04000318 RID: 792
	public const string InvalidCast_FromDBNull = "Object cannot be cast from DBNull to other types.";

	// Token: 0x04000319 RID: 793
	public const string NotSupported_DBNullSerial = "Only one DBNull instance may exist, and calls to DBNull deserialization methods are not allowed.";

	// Token: 0x0400031A RID: 794
	public const string Serialization_StringBuilderCapacity = "The serialized Capacity property of StringBuilder must be positive, less than or equal to MaxCapacity and greater than or equal to the String length.";

	// Token: 0x0400031B RID: 795
	public const string Serialization_StringBuilderMaxCapacity = "The serialized MaxCapacity property of StringBuilder must be positive and greater than or equal to the String length.";

	// Token: 0x0400031C RID: 796
	public const string PlatformNotSupported_Remoting = "Remoting is not supported on this platform.";

	// Token: 0x0400031D RID: 797
	public const string PlatformNotSupported_StrongNameSigning = "Strong-name signing is not supported on this platform.";

	// Token: 0x0400031E RID: 798
	public const string Serialization_MissingDateTimeData = "Invalid serialized DateTime data. Unable to find 'ticks' or 'dateData'.";

	// Token: 0x0400031F RID: 799
	public const string Serialization_DateTimeTicksOutOfRange = "Invalid serialized DateTime data. Ticks must be between DateTime.MinValue.Ticks and DateTime.MaxValue.Ticks.";

	// Token: 0x04000320 RID: 800
	public const string Arg_InvalidANSIString = "The ANSI string passed in could not be converted from the default ANSI code page to Unicode.";

	// Token: 0x04000321 RID: 801
	public const string Arg_ExpectedNulTermination = "The value passed was not NUL terminated.";

	// Token: 0x04000322 RID: 802
	public const string PlatformNotSupported_ArgIterator = "ArgIterator is not supported on this platform.";

	// Token: 0x04000323 RID: 803
	public const string Arg_TypeUnloadedException = "Type had been unloaded.";

	// Token: 0x04000324 RID: 804
	public const string Overflow_Currency = "Value was either too large or too small for a Currency.";

	// Token: 0x04000325 RID: 805
	public const string PlatformNotSupported_SecureBinarySerialization = "Secure binary serialization is not supported on this platform.";

	// Token: 0x04000326 RID: 806
	public const string Serialization_InvalidPtrValue = "An IntPtr or UIntPtr with an eight byte value cannot be deserialized on a machine with a four byte word size.";

	// Token: 0x04000327 RID: 807
	public const string EventSource_ListenerNotFound = "Listener not found.";

	// Token: 0x04000328 RID: 808
	public const string EventSource_ToString = "EventSource({0}, {1})";

	// Token: 0x04000329 RID: 809
	public const string EventSource_ImplementGetMetadata = "Please implement the GetMetadata method in your derived class";

	// Token: 0x0400032A RID: 810
	public const string EventSource_NeedGuid = "The Guid of an EventSource must be non zero.";

	// Token: 0x0400032B RID: 811
	public const string EventSource_NeedName = "The name of an EventSource must not be null.";

	// Token: 0x0400032C RID: 812
	public const string EventSource_NeedDescriptors = "The descriptor of an EventSource must be non-null.";

	// Token: 0x0400032D RID: 813
	public const string EventSource_NeedManifest = "The manifest of an EventSource must be non-null.";

	// Token: 0x0400032E RID: 814
	public const string EventSource_EventSourceGuidInUse = "An instance of EventSource with Guid {0} already exists.";

	// Token: 0x0400032F RID: 815
	public const string EventSource_ListenerWriteFailure = "An error occurred when writing to a listener.";

	// Token: 0x04000330 RID: 816
	public const string EventSource_NoManifest = "A manifest could not be generated for this EventSource because it contains one or more ill-formed event methods.";

	// Token: 0x04000331 RID: 817
	public const string Argument_StreamNotWritable = "Stream was not writable.";

	// Token: 0x04000332 RID: 818
	public const string Arg_SurrogatesNotAllowedAsSingleChar = "Unicode surrogate characters must be written out as pairs together in the same call, not individually. Consider passing in a character array instead.";

	// Token: 0x04000333 RID: 819
	public const string CustomAttributeFormat_InvalidFieldFail = "'{0}' field specified was not found.";

	// Token: 0x04000334 RID: 820
	public const string CustomAttributeFormat_InvalidPropertyFail = "'{0}' property specified was not found.";

	// Token: 0x04000335 RID: 821
	public const string NotSupported_CannotCallEqualsOnSpan = "Equals() on Span and ReadOnlySpan is not supported. Use operator== instead.";

	// Token: 0x04000336 RID: 822
	public const string NotSupported_CannotCallGetHashCodeOnSpan = "GetHashCode() on Span and ReadOnlySpan is not supported.";

	// Token: 0x04000337 RID: 823
	public const string Argument_DestinationTooShort = "Destination is too short.";

	// Token: 0x04000338 RID: 824
	public const string Argument_InvalidTypeWithPointersNotSupported = "Cannot use type '{0}'. Only value types without pointers or references are supported.";

	// Token: 0x04000339 RID: 825
	public const string ArrayTypeMismatch_ConstrainedCopy = "Array.ConstrainedCopy will only work on array types that are provably compatible, without any form of boxing, unboxing, widening, or casting of each array element.  Change the array types (i.e., copy a Derived[] to a Base[]), or use a mitigation strategy in the CER for Array.Copy's less powerful reliability contract, such as cloning the array or throwing away the potentially corrupt destination array.";

	// Token: 0x0400033A RID: 826
	public const string Arg_DllNotFoundException = "Dll was not found.";

	// Token: 0x0400033B RID: 827
	public const string Arg_DllNotFoundExceptionParameterized = "Unable to load DLL '{0}': The specified module could not be found.";

	// Token: 0x0400033C RID: 828
	public const string WrongSizeArrayInNStruct = "Type could not be marshaled because the length of an embedded array instance does not match the declared length in the layout.";

	// Token: 0x0400033D RID: 829
	public const string Arg_InteropMarshalUnmappableChar = "Cannot marshal: Encountered unmappable character.";

	// Token: 0x0400033E RID: 830
	public const string Arg_MarshalDirectiveException = "Marshaling directives are invalid.";

	// Token: 0x0400033F RID: 831
	public const string BlockingCollection_Add_ConcurrentCompleteAdd = "CompleteAdding may not be used concurrently with additions to the collection.";

	// Token: 0x04000340 RID: 832
	public const string BlockingCollection_Add_Failed = "The underlying collection didn't accept the item.";

	// Token: 0x04000341 RID: 833
	public const string BlockingCollection_CantAddAnyWhenCompleted = "At least one of the specified collections is marked as complete with regards to additions.";

	// Token: 0x04000342 RID: 834
	public const string BlockingCollection_CantTakeAnyWhenAllDone = "All collections are marked as complete with regards to additions.";

	// Token: 0x04000343 RID: 835
	public const string BlockingCollection_CantTakeWhenDone = "The collection argument is empty and has been marked as complete with regards to additions.";

	// Token: 0x04000344 RID: 836
	public const string BlockingCollection_Completed = "The collection has been marked as complete with regards to additions.";

	// Token: 0x04000345 RID: 837
	public const string BlockingCollection_CopyTo_IncorrectType = "The array argument is of the incorrect type.";

	// Token: 0x04000346 RID: 838
	public const string BlockingCollection_CopyTo_MultiDim = "The array argument is multidimensional.";

	// Token: 0x04000347 RID: 839
	public const string BlockingCollection_CopyTo_NonNegative = "The index argument must be greater than or equal zero.";

	// Token: 0x04000348 RID: 840
	public const string Collection_CopyTo_TooManyElems = "The number of elements in the collection is greater than the available space from index to the end of the destination array.";

	// Token: 0x04000349 RID: 841
	public const string BlockingCollection_ctor_BoundedCapacityRange = "The boundedCapacity argument must be positive.";

	// Token: 0x0400034A RID: 842
	public const string BlockingCollection_ctor_CountMoreThanCapacity = "The collection argument contains more items than are allowed by the boundedCapacity.";

	// Token: 0x0400034B RID: 843
	public const string BlockingCollection_Disposed = "The collection has been disposed.";

	// Token: 0x0400034C RID: 844
	public const string BlockingCollection_Take_CollectionModified = "The underlying collection was modified from outside of the BlockingCollection<T>.";

	// Token: 0x0400034D RID: 845
	public const string BlockingCollection_TimeoutInvalid = "The specified timeout must represent a value between -1 and {0}, inclusive.";

	// Token: 0x0400034E RID: 846
	public const string BlockingCollection_ValidateCollectionsArray_DispElems = "The collections argument contains at least one disposed element.";

	// Token: 0x0400034F RID: 847
	public const string BlockingCollection_ValidateCollectionsArray_LargeSize = "The collections length is greater than the supported range for 32 bit machine.";

	// Token: 0x04000350 RID: 848
	public const string BlockingCollection_ValidateCollectionsArray_NullElems = "The collections argument contains at least one null element.";

	// Token: 0x04000351 RID: 849
	public const string BlockingCollection_ValidateCollectionsArray_ZeroSize = "The collections argument is a zero-length array.";

	// Token: 0x04000352 RID: 850
	public const string Common_OperationCanceled = "The operation was canceled.";

	// Token: 0x04000353 RID: 851
	public const string ConcurrentBag_Ctor_ArgumentNullException = "The collection argument is null.";

	// Token: 0x04000354 RID: 852
	public const string ConcurrentBag_CopyTo_ArgumentNullException = "The array argument is null.";

	// Token: 0x04000355 RID: 853
	public const string Collection_CopyTo_ArgumentOutOfRangeException = "The index argument must be greater than or equal zero.";

	// Token: 0x04000356 RID: 854
	public const string ConcurrentCollection_SyncRoot_NotSupported = "The SyncRoot property may not be used for the synchronization of concurrent collections.";

	// Token: 0x04000357 RID: 855
	public const string ConcurrentDictionary_ArrayIncorrectType = "The array is multidimensional, or the type parameter for the set cannot be cast automatically to the type of the destination array.";

	// Token: 0x04000358 RID: 856
	public const string ConcurrentDictionary_SourceContainsDuplicateKeys = "The source argument contains duplicate keys.";

	// Token: 0x04000359 RID: 857
	public const string ConcurrentDictionary_ConcurrencyLevelMustBePositive = "The concurrencyLevel argument must be positive.";

	// Token: 0x0400035A RID: 858
	public const string ConcurrentDictionary_CapacityMustNotBeNegative = "The capacity argument must be greater than or equal to zero.";

	// Token: 0x0400035B RID: 859
	public const string ConcurrentDictionary_IndexIsNegative = "The index argument is less than zero.";

	// Token: 0x0400035C RID: 860
	public const string ConcurrentDictionary_ArrayNotLargeEnough = "The index is equal to or greater than the length of the array, or the number of elements in the dictionary is greater than the available space from index to the end of the destination array.";

	// Token: 0x0400035D RID: 861
	public const string ConcurrentDictionary_KeyAlreadyExisted = "The key already existed in the dictionary.";

	// Token: 0x0400035E RID: 862
	public const string ConcurrentDictionary_ItemKeyIsNull = "TKey is a reference type and item.Key is null.";

	// Token: 0x0400035F RID: 863
	public const string ConcurrentDictionary_TypeOfKeyIncorrect = "The key was of an incorrect type for this dictionary.";

	// Token: 0x04000360 RID: 864
	public const string ConcurrentDictionary_TypeOfValueIncorrect = "The value was of an incorrect type for this dictionary.";

	// Token: 0x04000361 RID: 865
	public const string ConcurrentStack_PushPopRange_CountOutOfRange = "The count argument must be greater than or equal to zero.";

	// Token: 0x04000362 RID: 866
	public const string ConcurrentStack_PushPopRange_InvalidCount = "The sum of the startIndex and count arguments must be less than or equal to the collection's Count.";

	// Token: 0x04000363 RID: 867
	public const string ConcurrentStack_PushPopRange_StartOutOfRange = "The startIndex argument must be greater than or equal to zero.";

	// Token: 0x04000364 RID: 868
	public const string Partitioner_DynamicPartitionsNotSupported = "Dynamic partitions are not supported by this partitioner.";

	// Token: 0x04000365 RID: 869
	public const string PartitionerStatic_CanNotCallGetEnumeratorAfterSourceHasBeenDisposed = "Can not call GetEnumerator on partitions after the source enumerable is disposed";

	// Token: 0x04000366 RID: 870
	public const string PartitionerStatic_CurrentCalledBeforeMoveNext = "MoveNext must be called at least once before calling Current.";

	// Token: 0x04000367 RID: 871
	public const string ConcurrentBag_Enumerator_EnumerationNotStartedOrAlreadyFinished = "Enumeration has either not started or has already finished.";

	// Token: 0x04000368 RID: 872
	public const string ArrayTypeMustBeExactMatch = "The array type must be exactly {0}.";

	// Token: 0x04000369 RID: 873
	public const string CannotCallEqualsOnSpan = "Equals() on Span and ReadOnlySpan is not supported. Use operator== instead.";

	// Token: 0x0400036A RID: 874
	public const string CannotCallGetHashCodeOnSpan = "GetHashCode() on Span and ReadOnlySpan is not supported.";

	// Token: 0x0400036B RID: 875
	public const string Argument_EmptyValue = "Value cannot be empty.";

	// Token: 0x0400036C RID: 876
	public const string PlatformNotSupported_RuntimeInformation = "RuntimeInformation is not supported for Portable Class Libraries.";

	// Token: 0x0400036D RID: 877
	public const string MemoryDisposed = "Memory<T> has been disposed.";

	// Token: 0x0400036E RID: 878
	public const string OutstandingReferences = "Release all references before disposing this instance.";
}
