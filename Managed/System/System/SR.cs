using System;
using System.Globalization;

// Token: 0x02000002 RID: 2
internal static class SR
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	internal static string GetString(string name, params object[] args)
	{
		return global::SR.GetString(CultureInfo.InvariantCulture, name, args);
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000205E File Offset: 0x0000025E
	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
	internal static string GetString(string name)
	{
		return name;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000206B File Offset: 0x0000026B
	internal static string GetString(CultureInfo culture, string name)
	{
		return name;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000206E File Offset: 0x0000026E
	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002081 File Offset: 0x00000281
	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000208F File Offset: 0x0000028F
	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000209E File Offset: 0x0000029E
	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002068 File Offset: 0x00000268
	public static object GetObject(string name)
	{
		return name;
	}

	// Token: 0x04000001 RID: 1
	public const string RTL = "RTL_False";

	// Token: 0x04000002 RID: 2
	public const string ContinueButtonText = "Continue";

	// Token: 0x04000003 RID: 3
	public const string DebugMessageTruncated = "{0}...\n<truncated>";

	// Token: 0x04000004 RID: 4
	public const string DebugAssertTitleShort = "Assertion Failed";

	// Token: 0x04000005 RID: 5
	public const string DebugAssertTitle = "Assertion Failed: Cancel=Debug, OK=Continue";

	// Token: 0x04000006 RID: 6
	public const string NotSupported = "This operation is not supported.";

	// Token: 0x04000007 RID: 7
	public const string DebugLaunchFailed = "Cannot launch the debugger.  Make sure that a Microsoft (R) .NET Framework debugger is properly installed.";

	// Token: 0x04000008 RID: 8
	public const string DebugLaunchFailedTitle = "Microsoft .NET Framework Debug Launch Failure";

	// Token: 0x04000009 RID: 9
	public const string ObjectDisposed = "Object {0} has been disposed and can no longer be used.";

	// Token: 0x0400000A RID: 10
	public const string ExceptionOccurred = "An exception occurred writing trace output to log file '{0}'. {1}";

	// Token: 0x0400000B RID: 11
	public const string MustAddListener = "Only TraceListeners can be added to a TraceListenerCollection.";

	// Token: 0x0400000C RID: 12
	public const string ToStringNull = "(null)";

	// Token: 0x0400000D RID: 13
	public const string EnumConverterInvalidValue = "The value '{0}' is not a valid value for the enum '{1}'.";

	// Token: 0x0400000E RID: 14
	public const string ConvertFromException = "{0} cannot convert from {1}.";

	// Token: 0x0400000F RID: 15
	public const string ConvertToException = "'{0}' is unable to convert '{1}' to '{2}'.";

	// Token: 0x04000010 RID: 16
	public const string ConvertInvalidPrimitive = "{0} is not a valid value for {1}.";

	// Token: 0x04000011 RID: 17
	public const string ErrorMissingPropertyAccessors = "Accessor methods for the {0} property are missing.";

	// Token: 0x04000012 RID: 18
	public const string ErrorInvalidPropertyType = "Invalid type for the {0} property.";

	// Token: 0x04000013 RID: 19
	public const string ErrorMissingEventAccessors = "Accessor methods for the {0} event are missing.";

	// Token: 0x04000014 RID: 20
	public const string ErrorInvalidEventHandler = "Invalid event handler for the {0} event.";

	// Token: 0x04000015 RID: 21
	public const string ErrorInvalidEventType = "Invalid type for the {0} event.";

	// Token: 0x04000016 RID: 22
	public const string InvalidMemberName = "Invalid member name.";

	// Token: 0x04000017 RID: 23
	public const string ErrorBadExtenderType = "The {0} extender provider is not compatible with the {1} type.";

	// Token: 0x04000018 RID: 24
	public const string NullableConverterBadCtorArg = "The specified type is not a nullable type.";

	// Token: 0x04000019 RID: 25
	public const string TypeDescriptorExpectedElementType = "Expected types in the collection to be of type {0}.";

	// Token: 0x0400001A RID: 26
	public const string TypeDescriptorSameAssociation = "Cannot create an association when the primary and secondary objects are the same.";

	// Token: 0x0400001B RID: 27
	public const string TypeDescriptorAlreadyAssociated = "The primary and secondary objects are already associated with each other.";

	// Token: 0x0400001C RID: 28
	public const string TypeDescriptorProviderError = "The type description provider {0} has returned null from {1} which is illegal.";

	// Token: 0x0400001D RID: 29
	public const string TypeDescriptorUnsupportedRemoteObject = "The object {0} is being remoted by a proxy that does not support interface discovery.  This type of remoted object is not supported.";

	// Token: 0x0400001E RID: 30
	public const string TypeDescriptorArgsCountMismatch = "The number of elements in the Type and Object arrays must match.";

	// Token: 0x0400001F RID: 31
	public const string ErrorCreateSystemEvents = "Failed to create system events window thread.";

	// Token: 0x04000020 RID: 32
	public const string ErrorCreateTimer = "Cannot create timer.";

	// Token: 0x04000021 RID: 33
	public const string ErrorKillTimer = "Cannot end timer.";

	// Token: 0x04000022 RID: 34
	public const string ErrorSystemEventsNotSupported = "System event notifications are not supported under the current context. Server processes, for example, may not support global system event notifications.";

	// Token: 0x04000023 RID: 35
	public const string ErrorGetTempPath = "Cannot get temporary file name";

	// Token: 0x04000024 RID: 36
	public const string CHECKOUTCanceled = "The checkout was canceled by the user.";

	// Token: 0x04000025 RID: 37
	public const string ErrorInvalidServiceInstance = "The service instance must derive from or implement {0}.";

	// Token: 0x04000026 RID: 38
	public const string ErrorServiceExists = "The service {0} already exists in the service container.";

	// Token: 0x04000027 RID: 39
	public const string Argument_InvalidNumberStyles = "An undefined NumberStyles value is being used.";

	// Token: 0x04000028 RID: 40
	public const string Argument_InvalidHexStyle = "With the AllowHexSpecifier bit set in the enum bit field, the only other valid bits that can be combined into the enum value must be a subset of those in HexNumber.";

	// Token: 0x04000029 RID: 41
	public const string Argument_ByteArrayLengthMustBeAMultipleOf4 = "The Byte[] length must be a multiple of 4.";

	// Token: 0x0400002A RID: 42
	public const string Argument_InvalidCharactersInString = "The string contained an invalid character.";

	// Token: 0x0400002B RID: 43
	public const string Argument_ParsedStringWasInvalid = "The parsed string was invalid.";

	// Token: 0x0400002C RID: 44
	public const string Argument_MustBeBigInt = "The parameter must be a BigInteger.";

	// Token: 0x0400002D RID: 45
	public const string Format_InvalidFormatSpecifier = "Format specifier was invalid.";

	// Token: 0x0400002E RID: 46
	public const string Format_TooLarge = "The value is too large to be represented by this format specifier.";

	// Token: 0x0400002F RID: 47
	public const string ArgumentOutOfRange_MustBeLessThanUInt32MaxValue = "The value must be less than UInt32.MaxValue (2^32).";

	// Token: 0x04000030 RID: 48
	public const string ArgumentOutOfRange_MustBeNonNeg = "The number must be greater than or equal to zero.";

	// Token: 0x04000031 RID: 49
	public const string NotSupported_NumberStyle = "The NumberStyle option is not supported.";

	// Token: 0x04000032 RID: 50
	public const string Overflow_BigIntInfinity = "BigInteger cannot represent infinity.";

	// Token: 0x04000033 RID: 51
	public const string Overflow_NotANumber = "The value is not a number.";

	// Token: 0x04000034 RID: 52
	public const string Overflow_ParseBigInteger = "The value could not be parsed.";

	// Token: 0x04000035 RID: 53
	public const string Overflow_Int32 = "Value was either too large or too small for an Int32.";

	// Token: 0x04000036 RID: 54
	public const string Overflow_Int64 = "Value was either too large or too small for an Int64.";

	// Token: 0x04000037 RID: 55
	public const string Overflow_UInt32 = "Value was either too large or too small for a UInt32.";

	// Token: 0x04000038 RID: 56
	public const string Overflow_UInt64 = "Value was either too large or too small for a UInt64.";

	// Token: 0x04000039 RID: 57
	public const string Overflow_Decimal = "Value was either too large or too small for a Decimal.";

	// Token: 0x0400003A RID: 58
	public const string Argument_FrameworkNameTooShort = "FrameworkName cannot have less than two components or more than three components.";

	// Token: 0x0400003B RID: 59
	public const string Argument_FrameworkNameInvalid = "FrameworkName is invalid.";

	// Token: 0x0400003C RID: 60
	public const string Argument_FrameworkNameInvalidVersion = "FrameworkName version component is invalid.";

	// Token: 0x0400003D RID: 61
	public const string Argument_FrameworkNameMissingVersion = "FrameworkName version component is missing.";

	// Token: 0x0400003E RID: 62
	public const string ArgumentNull_Key = "Key cannot be null.";

	// Token: 0x0400003F RID: 63
	public const string Argument_InvalidValue = "Argument {0} should be larger than {1}.";

	// Token: 0x04000040 RID: 64
	public const string Arg_MultiRank = "Multi dimension array is not supported on this operation.";

	// Token: 0x04000041 RID: 65
	public const string Barrier_ctor_ArgumentOutOfRange = "The participantCount argument must be non-negative and less than or equal to 32767.";

	// Token: 0x04000042 RID: 66
	public const string Barrier_AddParticipants_NonPositive_ArgumentOutOfRange = "The participantCount argument must be a positive value.";

	// Token: 0x04000043 RID: 67
	public const string Barrier_AddParticipants_Overflow_ArgumentOutOfRange = "Adding participantCount participants would result in the number of participants exceeding the maximum number allowed.";

	// Token: 0x04000044 RID: 68
	public const string Barrier_InvalidOperation_CalledFromPHA = "This method may not be called from within the postPhaseAction.";

	// Token: 0x04000045 RID: 69
	public const string Barrier_RemoveParticipants_NonPositive_ArgumentOutOfRange = "The participantCount argument must be a positive value.";

	// Token: 0x04000046 RID: 70
	public const string Barrier_RemoveParticipants_ArgumentOutOfRange = "The participantCount argument must be less than or equal the number of participants.";

	// Token: 0x04000047 RID: 71
	public const string Barrier_RemoveParticipants_InvalidOperation = "The participantCount argument is greater than the number of participants that haven't yet arrived at the barrier in this phase.";

	// Token: 0x04000048 RID: 72
	public const string Barrier_SignalAndWait_ArgumentOutOfRange = "The specified timeout must represent a value between -1 and Int32.MaxValue, inclusive.";

	// Token: 0x04000049 RID: 73
	public const string Barrier_SignalAndWait_InvalidOperation_ZeroTotal = "The barrier has no registered participants.";

	// Token: 0x0400004A RID: 74
	public const string Barrier_SignalAndWait_InvalidOperation_ThreadsExceeded = "The number of threads using the barrier exceeded the total number of registered participants.";

	// Token: 0x0400004B RID: 75
	public const string Barrier_Dispose = "The barrier has been disposed.";

	// Token: 0x0400004C RID: 76
	public const string BarrierPostPhaseException = "The postPhaseAction failed with an exception.";

	// Token: 0x0400004D RID: 77
	public const string UriTypeConverter_ConvertFrom_CannotConvert = "{0} cannot convert from {1}.";

	// Token: 0x0400004E RID: 78
	public const string UriTypeConverter_ConvertTo_CannotConvert = "{0} cannot convert {1} to {2}.";

	// Token: 0x0400004F RID: 79
	public const string ISupportInitializeDescr = "Specifies support for transacted initialization.";

	// Token: 0x04000050 RID: 80
	public const string CantModifyListSortDescriptionCollection = "Once a ListSortDescriptionCollection has been created it can't be modified.";

	// Token: 0x04000051 RID: 81
	public const string Argument_NullComment = "The 'Comment' property of the CodeCommentStatement '{0}' cannot be null.";

	// Token: 0x04000052 RID: 82
	public const string InvalidPrimitiveType = "Invalid Primitive Type: {0}. Consider using CodeObjectCreateExpression.";

	// Token: 0x04000053 RID: 83
	public const string Cannot_Specify_Both_Compiler_Path_And_Version = "Cannot specify both the '{0}' and '{1}' CodeDom provider options to choose a compiler. Please remove one of them.";

	// Token: 0x04000054 RID: 84
	public const string CodeGenOutputWriter = "The output writer for code generation and the writer supplied don't match and cannot be used. This is generally caused by a bad implementation of a CodeGenerator derived class.";

	// Token: 0x04000055 RID: 85
	public const string CodeGenReentrance = "This code generation API cannot be called while the generator is being used to generate something else.";

	// Token: 0x04000056 RID: 86
	public const string InvalidLanguageIdentifier = "The identifier:\"{0}\" on the property:\"{1}\" of type:\"{2}\" is not a valid language-independent identifier name. Check to see if CodeGenerator.IsValidLanguageIndependentIdentifier allows the identifier name.";

	// Token: 0x04000057 RID: 87
	public const string InvalidTypeName = "The type name:\"{0}\" on the property:\"{1}\" of type:\"{2}\" is not a valid language-independent type name.";

	// Token: 0x04000058 RID: 88
	public const string Empty_attribute = "The '{0}' attribute cannot be an empty string.";

	// Token: 0x04000059 RID: 89
	public const string Invalid_nonnegative_integer_attribute = "The '{0}' attribute must be a non-negative integer.";

	// Token: 0x0400005A RID: 90
	public const string CodeDomProvider_NotDefined = "There is no CodeDom provider defined for the language.";

	// Token: 0x0400005B RID: 91
	public const string Language_Names_Cannot_Be_Empty = "You need to specify a non-empty String for a language name in the CodeDom configuration section.";

	// Token: 0x0400005C RID: 92
	public const string Extension_Names_Cannot_Be_Empty_Or_Non_Period_Based = "An extension name in the CodeDom configuration section must be a non-empty string which starts with a period.";

	// Token: 0x0400005D RID: 93
	public const string Unable_To_Locate_Type = "The CodeDom provider type \"{0}\" could not be located.";

	// Token: 0x0400005E RID: 94
	public const string NotSupported_CodeDomAPI = "This CodeDomProvider does not support this method.";

	// Token: 0x0400005F RID: 95
	public const string ArityDoesntMatch = "The total arity specified in '{0}' does not match the number of TypeArguments supplied.  There were '{1}' TypeArguments supplied.";

	// Token: 0x04000060 RID: 96
	public const string PartialTrustErrorTextReplacement = "<The original value of this property potentially contains file system information and has been suppressed.>";

	// Token: 0x04000061 RID: 97
	public const string PartialTrustIllegalProvider = "When used in partial trust, langID must be C#, VB, J#, or JScript, and the language provider must be in the global assembly cache.";

	// Token: 0x04000062 RID: 98
	public const string IllegalAssemblyReference = "Assembly references cannot begin with '-', or contain a '/' or '\\'.";

	// Token: 0x04000063 RID: 99
	public const string NullOrEmpty_Value_in_Property = "The '{0}' property cannot contain null or empty strings.";

	// Token: 0x04000064 RID: 100
	public const string AutoGen_Comment_Line1 = "auto-generated>";

	// Token: 0x04000065 RID: 101
	public const string AutoGen_Comment_Line2 = "This code was generated by a tool.";

	// Token: 0x04000066 RID: 102
	public const string AutoGen_Comment_Line3 = "Runtime Version:";

	// Token: 0x04000067 RID: 103
	public const string AutoGen_Comment_Line4 = "Changes to this file may cause incorrect behavior and will be lost if";

	// Token: 0x04000068 RID: 104
	public const string AutoGen_Comment_Line5 = "the code is regenerated.";

	// Token: 0x04000069 RID: 105
	public const string CantContainNullEntries = "Array '{0}' cannot contain null entries.";

	// Token: 0x0400006A RID: 106
	public const string InvalidPathCharsInChecksum = "The CodeChecksumPragma file name '{0}' contains invalid path characters.";

	// Token: 0x0400006B RID: 107
	public const string InvalidRegion = "The region directive '{0}' contains invalid characters.  RegionText cannot contain any new line characters.";

	// Token: 0x0400006C RID: 108
	public const string Provider_does_not_support_options = "This CodeDomProvider type does not have a constructor that takes providerOptions - \"{0}\"";

	// Token: 0x0400006D RID: 109
	public const string MetaExtenderName = "{0} on {1}";

	// Token: 0x0400006E RID: 110
	public const string InvalidEnumArgument = "The value of argument '{0}' ({1}) is invalid for Enum type '{2}'.";

	// Token: 0x0400006F RID: 111
	public const string InvalidArgument = "'{1}' is not a valid value for '{0}'.";

	// Token: 0x04000070 RID: 112
	public const string InvalidNullArgument = "Null is not a valid value for {0}.";

	// Token: 0x04000071 RID: 113
	public const string LicExceptionTypeOnly = "A valid license cannot be granted for the type {0}. Contact the manufacturer of the component for more information.";

	// Token: 0x04000072 RID: 114
	public const string LicExceptionTypeAndInstance = "An instance of type '{1}' was being created, and a valid license could not be granted for the type '{0}'. Please,  contact the manufacturer of the component for more information.";

	// Token: 0x04000073 RID: 115
	public const string LicMgrContextCannotBeChanged = "The CurrentContext property of the LicenseManager is currently locked and cannot be changed.";

	// Token: 0x04000074 RID: 116
	public const string LicMgrAlreadyLocked = "The CurrentContext property of the LicenseManager is already locked by another user.";

	// Token: 0x04000075 RID: 117
	public const string LicMgrDifferentUser = "The CurrentContext property of the LicenseManager can only be unlocked with the same contextUser.";

	// Token: 0x04000076 RID: 118
	public const string InvalidElementType = "Element type {0} is not supported.";

	// Token: 0x04000077 RID: 119
	public const string InvalidIdentifier = "Identifier '{0}' is not valid.";

	// Token: 0x04000078 RID: 120
	public const string ExecFailedToCreate = "Failed to create file {0}.";

	// Token: 0x04000079 RID: 121
	public const string ExecTimeout = "Timed out waiting for a program to execute. The command being executed was {0}.";

	// Token: 0x0400007A RID: 122
	public const string ExecBadreturn = "An invalid return code was encountered waiting for a program to execute. The command being executed was {0}.";

	// Token: 0x0400007B RID: 123
	public const string ExecCantGetRetCode = "Unable to get the return code for a program being executed. The command that was being executed was '{0}'.";

	// Token: 0x0400007C RID: 124
	public const string ExecCantExec = "Cannot execute a program. The command being executed was {0}.";

	// Token: 0x0400007D RID: 125
	public const string ExecCantRevert = "Cannot execute a program. Impersonation failed.";

	// Token: 0x0400007E RID: 126
	public const string CompilerNotFound = "Compiler executable file {0} cannot be found.";

	// Token: 0x0400007F RID: 127
	public const string DuplicateFileName = "The file name '{0}' was already in the collection.";

	// Token: 0x04000080 RID: 128
	public const string CollectionReadOnly = "Collection is read-only.";

	// Token: 0x04000081 RID: 129
	public const string BitVectorFull = "Bit vector is full.";

	// Token: 0x04000082 RID: 130
	public const string ArrayConverterText = "{0} Array";

	// Token: 0x04000083 RID: 131
	public const string CollectionConverterText = "(Collection)";

	// Token: 0x04000084 RID: 132
	public const string MultilineStringConverterText = "(Text)";

	// Token: 0x04000085 RID: 133
	public const string CultureInfoConverterDefaultCultureString = "(Default)";

	// Token: 0x04000086 RID: 134
	public const string CultureInfoConverterInvalidCulture = "The {0} culture cannot be converted to a CultureInfo object on this computer.";

	// Token: 0x04000087 RID: 135
	public const string InvalidPrimitive = "The text {0} is not a valid {1}.";

	// Token: 0x04000088 RID: 136
	public const string TimerInvalidInterval = "'{0}' is not a valid value for 'Interval'. 'Interval' must be greater than {1}.";

	// Token: 0x04000089 RID: 137
	public const string TraceSwitchLevelTooHigh = "Attempted to set {0} to a value that is too high.  Setting level to TraceLevel.Verbose";

	// Token: 0x0400008A RID: 138
	public const string TraceSwitchLevelTooLow = "Attempted to set {0} to a value that is too low.  Setting level to TraceLevel.Off";

	// Token: 0x0400008B RID: 139
	public const string TraceSwitchInvalidLevel = "The Level must be set to a value in the enumeration TraceLevel.";

	// Token: 0x0400008C RID: 140
	public const string TraceListenerIndentSize = "The IndentSize property must be non-negative.";

	// Token: 0x0400008D RID: 141
	public const string TraceListenerFail = "Fail:";

	// Token: 0x0400008E RID: 142
	public const string TraceAsTraceSource = "Trace";

	// Token: 0x0400008F RID: 143
	public const string InvalidLowBoundArgument = "'{1}' is not a valid value for '{0}'. '{0}' must be greater than {2}.";

	// Token: 0x04000090 RID: 144
	public const string DuplicateComponentName = "Duplicate component name '{0}'.  Component names must be unique and case-insensitive.";

	// Token: 0x04000091 RID: 145
	public const string NotImplemented = "{0}: Not implemented";

	// Token: 0x04000092 RID: 146
	public const string OutOfMemory = "Could not allocate needed memory.";

	// Token: 0x04000093 RID: 147
	public const string EOF = "End of data stream encountered.";

	// Token: 0x04000094 RID: 148
	public const string IOError = "Unknown input/output failure.";

	// Token: 0x04000095 RID: 149
	public const string BadChar = "Unexpected Character: '{0}'.";

	// Token: 0x04000096 RID: 150
	public const string toStringNone = "(none)";

	// Token: 0x04000097 RID: 151
	public const string toStringUnknown = "(unknown)";

	// Token: 0x04000098 RID: 152
	public const string InvalidEnum = "{0} is not a valid {1} value.";

	// Token: 0x04000099 RID: 153
	public const string IndexOutOfRange = "Index {0} is out of range.";

	// Token: 0x0400009A RID: 154
	public const string ErrorPropertyAccessorException = "Property accessor '{0}' on object '{1}' threw the following exception:'{2}'";

	// Token: 0x0400009B RID: 155
	public const string InvalidOperation = "Invalid operation.";

	// Token: 0x0400009C RID: 156
	public const string EmptyStack = "Stack has no items in it.";

	// Token: 0x0400009D RID: 157
	public const string PerformanceCounterDesc = "Represents a Windows performance counter component.";

	// Token: 0x0400009E RID: 158
	public const string PCCategoryName = "Category name of the performance counter object.";

	// Token: 0x0400009F RID: 159
	public const string PCCounterName = "Counter name of the performance counter object.";

	// Token: 0x040000A0 RID: 160
	public const string PCInstanceName = "Instance name of the performance counter object.";

	// Token: 0x040000A1 RID: 161
	public const string PCMachineName = "Specifies the machine from where to read the performance data.";

	// Token: 0x040000A2 RID: 162
	public const string PCInstanceLifetime = "Specifies the lifetime of the instance.";

	// Token: 0x040000A3 RID: 163
	public const string PropertyCategoryAction = "Action";

	// Token: 0x040000A4 RID: 164
	public const string PropertyCategoryAppearance = "Appearance";

	// Token: 0x040000A5 RID: 165
	public const string PropertyCategoryAsynchronous = "Asynchronous";

	// Token: 0x040000A6 RID: 166
	public const string PropertyCategoryBehavior = "Behavior";

	// Token: 0x040000A7 RID: 167
	public const string PropertyCategoryData = "Data";

	// Token: 0x040000A8 RID: 168
	public const string PropertyCategoryDDE = "DDE";

	// Token: 0x040000A9 RID: 169
	public const string PropertyCategoryDesign = "Design";

	// Token: 0x040000AA RID: 170
	public const string PropertyCategoryDragDrop = "Drag Drop";

	// Token: 0x040000AB RID: 171
	public const string PropertyCategoryFocus = "Focus";

	// Token: 0x040000AC RID: 172
	public const string PropertyCategoryFont = "Font";

	// Token: 0x040000AD RID: 173
	public const string PropertyCategoryFormat = "Format";

	// Token: 0x040000AE RID: 174
	public const string PropertyCategoryKey = "Key";

	// Token: 0x040000AF RID: 175
	public const string PropertyCategoryList = "List";

	// Token: 0x040000B0 RID: 176
	public const string PropertyCategoryLayout = "Layout";

	// Token: 0x040000B1 RID: 177
	public const string PropertyCategoryDefault = "Misc";

	// Token: 0x040000B2 RID: 178
	public const string PropertyCategoryMouse = "Mouse";

	// Token: 0x040000B3 RID: 179
	public const string PropertyCategoryPosition = "Position";

	// Token: 0x040000B4 RID: 180
	public const string PropertyCategoryText = "Text";

	// Token: 0x040000B5 RID: 181
	public const string PropertyCategoryScale = "Scale";

	// Token: 0x040000B6 RID: 182
	public const string PropertyCategoryWindowStyle = "Window Style";

	// Token: 0x040000B7 RID: 183
	public const string PropertyCategoryConfig = "Configurations";

	// Token: 0x040000B8 RID: 184
	public const string ArgumentNull_ArrayWithNullElements = "The array cannot contain null elements.";

	// Token: 0x040000B9 RID: 185
	public const string OnlyAllowedOnce = "This operation is only allowed once per object.";

	// Token: 0x040000BA RID: 186
	public const string BeginIndexNotNegative = "Start index cannot be less than 0 or greater than input length.";

	// Token: 0x040000BB RID: 187
	public const string LengthNotNegative = "Length cannot be less than 0 or exceed input length.";

	// Token: 0x040000BC RID: 188
	public const string UnimplementedState = "Unimplemented state.";

	// Token: 0x040000BD RID: 189
	public const string UnexpectedOpcode = "Unexpected opcode in regular expression generation: {0}.";

	// Token: 0x040000BE RID: 190
	public const string NoResultOnFailed = "Result cannot be called on a failed Match.";

	// Token: 0x040000BF RID: 191
	public const string UnterminatedBracket = "Unterminated [] set.";

	// Token: 0x040000C0 RID: 192
	public const string TooManyParens = "Too many )'s.";

	// Token: 0x040000C1 RID: 193
	public const string NestedQuantify = "Nested quantifier {0}.";

	// Token: 0x040000C2 RID: 194
	public const string QuantifyAfterNothing = "Quantifier {x,y} following nothing.";

	// Token: 0x040000C3 RID: 195
	public const string InternalError = "Internal error in ScanRegex.";

	// Token: 0x040000C4 RID: 196
	public const string IllegalRange = "Illegal {x,y} with x > y.";

	// Token: 0x040000C5 RID: 197
	public const string NotEnoughParens = "Not enough )'s.";

	// Token: 0x040000C6 RID: 198
	public const string BadClassInCharRange = "Cannot include class \\{0} in character range.";

	// Token: 0x040000C7 RID: 199
	public const string ReversedCharRange = "[x-y] range in reverse order.";

	// Token: 0x040000C8 RID: 200
	public const string UndefinedReference = "(?({0}) ) reference to undefined group.";

	// Token: 0x040000C9 RID: 201
	public const string MalformedReference = "(?({0}) ) malformed.";

	// Token: 0x040000CA RID: 202
	public const string UnrecognizedGrouping = "Unrecognized grouping construct.";

	// Token: 0x040000CB RID: 203
	public const string UnterminatedComment = "Unterminated (?#...) comment.";

	// Token: 0x040000CC RID: 204
	public const string IllegalEndEscape = "Illegal \\ at end of pattern.";

	// Token: 0x040000CD RID: 205
	public const string MalformedNameRef = "Malformed \\k<...> named back reference.";

	// Token: 0x040000CE RID: 206
	public const string UndefinedBackref = "Reference to undefined group number {0}.";

	// Token: 0x040000CF RID: 207
	public const string UndefinedNameRef = "Reference to undefined group name {0}.";

	// Token: 0x040000D0 RID: 208
	public const string TooFewHex = "Insufficient hexadecimal digits.";

	// Token: 0x040000D1 RID: 209
	public const string MissingControl = "Missing control character.";

	// Token: 0x040000D2 RID: 210
	public const string UnrecognizedControl = "Unrecognized control character.";

	// Token: 0x040000D3 RID: 211
	public const string UnrecognizedEscape = "Unrecognized escape sequence \\{0}.";

	// Token: 0x040000D4 RID: 212
	public const string IllegalCondition = "Illegal conditional (?(...)) expression.";

	// Token: 0x040000D5 RID: 213
	public const string TooManyAlternates = "Too many | in (?()|).";

	// Token: 0x040000D6 RID: 214
	public const string MakeException = "parsing \"{0}\" - {1}";

	// Token: 0x040000D7 RID: 215
	public const string IncompleteSlashP = "Incomplete \\p{X} character escape.";

	// Token: 0x040000D8 RID: 216
	public const string MalformedSlashP = "Malformed \\p{X} character escape.";

	// Token: 0x040000D9 RID: 217
	public const string InvalidGroupName = "Invalid group name: Group names must begin with a word character.";

	// Token: 0x040000DA RID: 218
	public const string CapnumNotZero = "Capture number cannot be zero.";

	// Token: 0x040000DB RID: 219
	public const string AlternationCantCapture = "Alternation conditions do not capture and cannot be named.";

	// Token: 0x040000DC RID: 220
	public const string AlternationCantHaveComment = "Alternation conditions cannot be comments.";

	// Token: 0x040000DD RID: 221
	public const string CaptureGroupOutOfRange = "Capture group numbers must be less than or equal to Int32.MaxValue.";

	// Token: 0x040000DE RID: 222
	public const string SubtractionMustBeLast = "A subtraction must be the last element in a character class.";

	// Token: 0x040000DF RID: 223
	public const string UnknownProperty = "Unknown property '{0}'.";

	// Token: 0x040000E0 RID: 224
	public const string ReplacementError = "Replacement pattern error.";

	// Token: 0x040000E1 RID: 225
	public const string CountTooSmall = "Count cannot be less than -1.";

	// Token: 0x040000E2 RID: 226
	public const string EnumNotStarted = "Enumeration has either not started or has already finished.";

	// Token: 0x040000E3 RID: 227
	public const string Arg_InvalidArrayType = "Target array type is not compatible with the type of items in the collection.";

	// Token: 0x040000E4 RID: 228
	public const string RegexMatchTimeoutException_Occurred = "The RegEx engine has timed out while trying to match a pattern to an input string. This can occur for many reasons, including very large inputs or excessive backtracking caused by nested quantifiers, back-references and other factors.";

	// Token: 0x040000E5 RID: 229
	public const string IllegalDefaultRegexMatchTimeoutInAppDomain = "AppDomain data '{0}' contains an invalid value or object for specifying a default matching timeout for System.Text.RegularExpressions.Regex.";

	// Token: 0x040000E6 RID: 230
	public const string FileObject_AlreadyOpen = "The file is already open.  Call Close before trying to open the FileObject again.";

	// Token: 0x040000E7 RID: 231
	public const string FileObject_Closed = "The FileObject is currently closed.  Try opening it.";

	// Token: 0x040000E8 RID: 232
	public const string FileObject_NotWhileWriting = "File information cannot be queried while open for writing.";

	// Token: 0x040000E9 RID: 233
	public const string FileObject_FileDoesNotExist = "File information cannot be queried if the file does not exist.";

	// Token: 0x040000EA RID: 234
	public const string FileObject_MustBeClosed = "This operation can only be done when the FileObject is closed.";

	// Token: 0x040000EB RID: 235
	public const string FileObject_MustBeFileName = "You must specify a file name, not a relative or absolute path.";

	// Token: 0x040000EC RID: 236
	public const string FileObject_InvalidInternalState = "FileObject's open mode wasn't set to a valid value.  This FileObject is corrupt.";

	// Token: 0x040000ED RID: 237
	public const string FileObject_PathNotSet = "The path has not been set, or is an empty string.  Please ensure you specify some path.";

	// Token: 0x040000EE RID: 238
	public const string FileObject_Reading = "The file is currently open for reading.  Close the file and reopen it before attempting this.";

	// Token: 0x040000EF RID: 239
	public const string FileObject_Writing = "The file is currently open for writing.  Close the file and reopen it before attempting this.";

	// Token: 0x040000F0 RID: 240
	public const string FileObject_InvalidEnumeration = "Enumerator is positioned before the first line or after the last line of the file.";

	// Token: 0x040000F1 RID: 241
	public const string FileObject_NoReset = "Reset is not supported on a FileLineEnumerator.";

	// Token: 0x040000F2 RID: 242
	public const string DirectoryObject_MustBeDirName = "You must specify a directory name, not a relative or absolute path.";

	// Token: 0x040000F3 RID: 243
	public const string DirectoryObjectPathDescr = "The fully qualified, or relative path to the directory you wish to read from. E.g., \"c:\\temp\".";

	// Token: 0x040000F4 RID: 244
	public const string FileObjectDetectEncodingDescr = "Determines whether the file will be parsed to see if it has a byte order mark indicating its encoding.  If it does, this will be used rather than the current specified encoding.";

	// Token: 0x040000F5 RID: 245
	public const string FileObjectEncodingDescr = "The encoding to use when reading the file. UTF-8 is the default.";

	// Token: 0x040000F6 RID: 246
	public const string FileObjectPathDescr = "The fully qualified, or relative path to the file you wish to read from. E.g., \"myfile.txt\".";

	// Token: 0x040000F7 RID: 247
	public const string Arg_EnumIllegalVal = "Illegal enum value: {0}.";

	// Token: 0x040000F8 RID: 248
	public const string Arg_OutOfRange_NeedNonNegNum = "Non-negative number required.";

	// Token: 0x040000F9 RID: 249
	public const string Argument_InvalidPermissionState = "Invalid permission state.";

	// Token: 0x040000FA RID: 250
	public const string Argument_InvalidOidValue = "The OID value was invalid.";

	// Token: 0x040000FB RID: 251
	public const string Argument_WrongType = "Operation on type '{0}' attempted with target of incorrect type.";

	// Token: 0x040000FC RID: 252
	public const string Arg_EmptyOrNullString = "String cannot be empty or null.";

	// Token: 0x040000FD RID: 253
	public const string Arg_EmptyOrNullArray = "Array cannot be empty or null.";

	// Token: 0x040000FE RID: 254
	public const string Argument_InvalidClassAttribute = "The value of \"class\" attribute is invalid.";

	// Token: 0x040000FF RID: 255
	public const string Argument_InvalidNameType = "The value of \"nameType\" is invalid.";

	// Token: 0x04000100 RID: 256
	public const string InvalidOperation_DuplicateItemNotAllowed = "Duplicate items are not allowed in the collection.";

	// Token: 0x04000101 RID: 257
	public const string Cryptography_Asn_MismatchedOidInCollection = "The AsnEncodedData object does not have the same OID for the collection.";

	// Token: 0x04000102 RID: 258
	public const string Cryptography_Cms_Envelope_Empty_Content = "Cannot create CMS enveloped for empty content.";

	// Token: 0x04000103 RID: 259
	public const string Cryptography_Cms_Invalid_Recipient_Info_Type = "The recipient info type {0} is not valid.";

	// Token: 0x04000104 RID: 260
	public const string Cryptography_Cms_Invalid_Subject_Identifier_Type = "The subject identifier type {0} is not valid.";

	// Token: 0x04000105 RID: 261
	public const string Cryptography_Cms_Invalid_Subject_Identifier_Type_Value_Mismatch = "The subject identifier type {0} does not match the value data type {1}.";

	// Token: 0x04000106 RID: 262
	public const string Cryptography_Cms_Key_Agree_Date_Not_Available = "The Date property is not available for none KID key agree recipient.";

	// Token: 0x04000107 RID: 263
	public const string Cryptography_Cms_Key_Agree_Other_Key_Attribute_Not_Available = "The OtherKeyAttribute property is not available for none KID key agree recipient.";

	// Token: 0x04000108 RID: 264
	public const string Cryptography_Cms_MessageNotSigned = "The CMS message is not signed.";

	// Token: 0x04000109 RID: 265
	public const string Cryptography_Cms_MessageNotSignedByNoSignature = "The CMS message is not signed by NoSignature.";

	// Token: 0x0400010A RID: 266
	public const string Cryptography_Cms_MessageNotEncrypted = "The CMS message is not encrypted.";

	// Token: 0x0400010B RID: 267
	public const string Cryptography_Cms_Not_Supported = "The Cryptographic Message Standard (CMS) is not supported on this platform.";

	// Token: 0x0400010C RID: 268
	public const string Cryptography_Cms_RecipientCertificateNotFound = "The recipient certificate is not specified.";

	// Token: 0x0400010D RID: 269
	public const string Cryptography_Cms_Sign_Empty_Content = "Cannot create CMS signature for empty content.";

	// Token: 0x0400010E RID: 270
	public const string Cryptography_Cms_Sign_No_Signature_First_Signer = "CmsSigner has to be the first signer with NoSignature.";

	// Token: 0x0400010F RID: 271
	public const string Cryptography_DpApi_InvalidMemoryLength = "The length of the data should be a multiple of 16 bytes.";

	// Token: 0x04000110 RID: 272
	public const string Cryptography_InvalidHandle = "{0} is an invalid handle.";

	// Token: 0x04000111 RID: 273
	public const string Cryptography_InvalidContextHandle = "The chain context handle is invalid.";

	// Token: 0x04000112 RID: 274
	public const string Cryptography_InvalidStoreHandle = "The store handle is invalid.";

	// Token: 0x04000113 RID: 275
	public const string Cryptography_Oid_InvalidValue = "The OID value is invalid.";

	// Token: 0x04000114 RID: 276
	public const string Cryptography_Pkcs9_ExplicitAddNotAllowed = "The PKCS 9 attribute cannot be explicitly added to the collection.";

	// Token: 0x04000115 RID: 277
	public const string Cryptography_Pkcs9_InvalidOid = "The OID does not represent a valid PKCS 9 attribute.";

	// Token: 0x04000116 RID: 278
	public const string Cryptography_Pkcs9_MultipleSigningTimeNotAllowed = "Cannot add multiple PKCS 9 signing time attributes.";

	// Token: 0x04000117 RID: 279
	public const string Cryptography_Pkcs9_AttributeMismatch = "The parameter should be a PKCS 9 attribute.";

	// Token: 0x04000118 RID: 280
	public const string Cryptography_X509_AddFailed = "Adding certificate with index '{0}' failed.";

	// Token: 0x04000119 RID: 281
	public const string Cryptography_X509_BadEncoding = "Input data cannot be coded as a valid certificate.";

	// Token: 0x0400011A RID: 282
	public const string Cryptography_X509_ExportFailed = "The certificate export operation failed.";

	// Token: 0x0400011B RID: 283
	public const string Cryptography_X509_ExtensionMismatch = "The parameter should be an X509Extension.";

	// Token: 0x0400011C RID: 284
	public const string Cryptography_X509_InvalidFindType = "Invalid find type.";

	// Token: 0x0400011D RID: 285
	public const string Cryptography_X509_InvalidFindValue = "Invalid find value.";

	// Token: 0x0400011E RID: 286
	public const string Cryptography_X509_InvalidEncodingFormat = "Invalid encoding format.";

	// Token: 0x0400011F RID: 287
	public const string Cryptography_X509_InvalidContentType = "Invalid content type.";

	// Token: 0x04000120 RID: 288
	public const string Cryptography_X509_KeyMismatch = "The public key of the certificate does not match the value specified.";

	// Token: 0x04000121 RID: 289
	public const string Cryptography_X509_RemoveFailed = "Removing certificate with index '{0}' failed.";

	// Token: 0x04000122 RID: 290
	public const string Cryptography_X509_StoreNotOpen = "The X509 certificate store has not been opened.";

	// Token: 0x04000123 RID: 291
	public const string Environment_NotInteractive = "The current session is not interactive.";

	// Token: 0x04000124 RID: 292
	public const string NotSupported_InvalidKeyImpl = "Only asymmetric keys that implement ICspAsymmetricAlgorithm are supported.";

	// Token: 0x04000125 RID: 293
	public const string NotSupported_KeyAlgorithm = "The certificate key algorithm is not supported.";

	// Token: 0x04000126 RID: 294
	public const string NotSupported_PlatformRequiresNT = "This operation is only supported on Windows 2000, Windows XP, and higher.";

	// Token: 0x04000127 RID: 295
	public const string NotSupported_UnreadableStream = "Stream does not support reading.";

	// Token: 0x04000128 RID: 296
	public const string Security_InvalidValue = "The {0} value was invalid.";

	// Token: 0x04000129 RID: 297
	public const string Unknown_Error = "Unknown error.";

	// Token: 0x0400012A RID: 298
	public const string security_ServiceNameCollection_EmptyServiceName = "A service name must not be null or empty.";

	// Token: 0x0400012B RID: 299
	public const string security_ExtendedProtectionPolicy_UseDifferentConstructorForNever = "To construct a policy with PolicyEnforcement.Never, the single-parameter constructor must be used.";

	// Token: 0x0400012C RID: 300
	public const string security_ExtendedProtectionPolicy_NoEmptyServiceNameCollection = "The ServiceNameCollection must contain at least one service name.";

	// Token: 0x0400012D RID: 301
	public const string security_ExtendedProtection_NoOSSupport = "This operation requires OS support for extended protection.";

	// Token: 0x0400012E RID: 302
	public const string net_nonClsCompliantException = "A non-CLS Compliant Exception (i.e. an object that does not derive from System.Exception) was thrown.";

	// Token: 0x0400012F RID: 303
	public const string net_illegalConfigWith = "The '{0}' attribute cannot appear when '{1}' is present.";

	// Token: 0x04000130 RID: 304
	public const string net_illegalConfigWithout = "The '{0}' attribute can only appear when '{1}' is present.";

	// Token: 0x04000131 RID: 305
	public const string net_baddate = "The value of the date string in the header is invalid.";

	// Token: 0x04000132 RID: 306
	public const string net_writestarted = "This property cannot be set after writing has started.";

	// Token: 0x04000133 RID: 307
	public const string net_reqsubmitted = "This operation cannot be performed after the request has been submitted.";

	// Token: 0x04000134 RID: 308
	public const string net_ftp_no_http_cmd = "The requested FTP command is not supported when using HTTP proxy.";

	// Token: 0x04000135 RID: 309
	public const string net_ftp_invalid_method_name = "FTP Method names cannot be null or empty.";

	// Token: 0x04000136 RID: 310
	public const string net_ftp_invalid_renameto = "The RenameTo filename cannot be null or empty.";

	// Token: 0x04000137 RID: 311
	public const string net_ftp_no_defaultcreds = "Default credentials are not supported on an FTP request.";

	// Token: 0x04000138 RID: 312
	public const string net_ftpnoresponse = "This type of FTP request does not return a response stream.";

	// Token: 0x04000139 RID: 313
	public const string net_ftp_response_invalid_format = "The response string '{0}' has invalid format.";

	// Token: 0x0400013A RID: 314
	public const string net_ftp_no_offsetforhttp = "Offsets are not supported when sending an FTP request over an HTTP proxy.";

	// Token: 0x0400013B RID: 315
	public const string net_ftp_invalid_uri = "The requested URI is invalid for this FTP command.";

	// Token: 0x0400013C RID: 316
	public const string net_ftp_invalid_status_response = "The status response ({0}) is not expected in response to '{1}' command.";

	// Token: 0x0400013D RID: 317
	public const string net_ftp_server_failed_passive = "The server failed the passive mode request with status response ({0}).";

	// Token: 0x0400013E RID: 318
	public const string net_ftp_active_address_different = "The data connection was made from an address that is different than the address to which the FTP connection was made.";

	// Token: 0x0400013F RID: 319
	public const string net_ftp_proxy_does_not_support_ssl = "SSL cannot be enabled when using a proxy.";

	// Token: 0x04000140 RID: 320
	public const string net_ftp_invalid_response_filename = "The server returned the filename ({0}) which is not valid.";

	// Token: 0x04000141 RID: 321
	public const string net_ftp_unsupported_method = "This method is not supported.";

	// Token: 0x04000142 RID: 322
	public const string net_resubmitcanceled = "An error occurred on an automatic resubmission of the request.";

	// Token: 0x04000143 RID: 323
	public const string net_redirect_perm = "WebPermission demand failed for redirect URI.";

	// Token: 0x04000144 RID: 324
	public const string net_resubmitprotofailed = "Cannot handle redirect from HTTP/HTTPS protocols to other dissimilar ones.";

	// Token: 0x04000145 RID: 325
	public const string net_needchunked = "TransferEncoding requires the SendChunked property to be set to true.";

	// Token: 0x04000146 RID: 326
	public const string net_connarg = "Keep-Alive and Close may not be set using this property.";

	// Token: 0x04000147 RID: 327
	public const string net_no100 = "100-Continue may not be set using this property.";

	// Token: 0x04000148 RID: 328
	public const string net_fromto = "The From parameter cannot be less than To.";

	// Token: 0x04000149 RID: 329
	public const string net_rangetoosmall = "The From or To parameter cannot be less than 0.";

	// Token: 0x0400014A RID: 330
	public const string net_invalidversion = "This protocol version is not supported.";

	// Token: 0x0400014B RID: 331
	public const string net_toosmall = "The specified value must be greater than 0.";

	// Token: 0x0400014C RID: 332
	public const string net_toolong = "The size of {0} is too long. It cannot be longer than {1} characters.";

	// Token: 0x0400014D RID: 333
	public const string net_connclosed = "The underlying connection was closed: {0}.";

	// Token: 0x0400014E RID: 334
	public const string net_servererror = "The remote server returned an error: {0}.";

	// Token: 0x0400014F RID: 335
	public const string net_nouploadonget = "Cannot send a content-body with this verb-type.";

	// Token: 0x04000150 RID: 336
	public const string net_mutualauthfailed = "The requirement for mutual authentication was not met by the remote server.";

	// Token: 0x04000151 RID: 337
	public const string net_invasync = "Cannot block a call on this socket while an earlier asynchronous call is in progress.";

	// Token: 0x04000152 RID: 338
	public const string net_inasync = "An asynchronous call is already in progress. It must be completed or canceled before you can call this method.";

	// Token: 0x04000153 RID: 339
	public const string net_mustbeuri = "The {0} parameter must represent a valid Uri (see inner exception).";

	// Token: 0x04000154 RID: 340
	public const string net_format_shexp = "The shell expression '{0}' could not be parsed because it is formatted incorrectly.";

	// Token: 0x04000155 RID: 341
	public const string net_cannot_load_proxy_helper = "Failed to load the proxy script runtime environment from the Microsoft.JScript assembly.";

	// Token: 0x04000156 RID: 342
	public const string net_invalid_host = "The specified value is not a valid Host header string.";

	// Token: 0x04000157 RID: 343
	public const string net_repcall = "Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress.";

	// Token: 0x04000158 RID: 344
	public const string net_badmethod = "Cannot set null or blank methods on request.";

	// Token: 0x04000159 RID: 345
	public const string net_io_timeout_use_ge_zero = "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value >= 0.";

	// Token: 0x0400015A RID: 346
	public const string net_io_timeout_use_gt_zero = "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value > 0.";

	// Token: 0x0400015B RID: 347
	public const string net_io_no_0timeouts = "NetworkStream does not support a 0 millisecond timeout, use a value greater than zero for the timeout instead.";

	// Token: 0x0400015C RID: 348
	public const string net_requestaborted = "The request was aborted: {0}.";

	// Token: 0x0400015D RID: 349
	public const string net_tooManyRedirections = "Too many automatic redirections were attempted.";

	// Token: 0x0400015E RID: 350
	public const string net_authmodulenotregistered = "The supplied authentication module is not registered.";

	// Token: 0x0400015F RID: 351
	public const string net_authschemenotregistered = "There is no registered module for this authentication scheme.";

	// Token: 0x04000160 RID: 352
	public const string net_proxyschemenotsupported = "The ServicePointManager does not support proxies with the {0} scheme.";

	// Token: 0x04000161 RID: 353
	public const string net_maxsrvpoints = "The maximum number of service points was exceeded.";

	// Token: 0x04000162 RID: 354
	public const string net_unknown_prefix = "The URI prefix is not recognized.";

	// Token: 0x04000163 RID: 355
	public const string net_notconnected = "The operation is not allowed on non-connected sockets.";

	// Token: 0x04000164 RID: 356
	public const string net_notstream = "The operation is not allowed on non-stream oriented sockets.";

	// Token: 0x04000165 RID: 357
	public const string net_timeout = "The operation has timed out.";

	// Token: 0x04000166 RID: 358
	public const string net_nocontentlengthonget = "Content-Length or Chunked Encoding cannot be set for an operation that does not write data.";

	// Token: 0x04000167 RID: 359
	public const string net_contentlengthmissing = "When performing a write operation with AllowWriteStreamBuffering set to false, you must either set ContentLength to a non-negative number or set SendChunked to true.";

	// Token: 0x04000168 RID: 360
	public const string net_nonhttpproxynotallowed = "The URI scheme for the supplied IWebProxy has the illegal value '{0}'. Only 'http' is supported.";

	// Token: 0x04000169 RID: 361
	public const string net_nottoken = "The supplied string is not a valid HTTP token.";

	// Token: 0x0400016A RID: 362
	public const string net_rangetype = "A different range specifier has already been added to this request.";

	// Token: 0x0400016B RID: 363
	public const string net_need_writebuffering = "This request requires buffering data to succeed.";

	// Token: 0x0400016C RID: 364
	public const string net_securityprotocolnotsupported = "The requested security protocol is not supported.";

	// Token: 0x0400016D RID: 365
	public const string net_nodefaultcreds = "Default credentials cannot be supplied for the {0} authentication scheme.";

	// Token: 0x0400016E RID: 366
	public const string net_stopped = "Not listening. You must call the Start() method before calling this method.";

	// Token: 0x0400016F RID: 367
	public const string net_udpconnected = "Cannot send packets to an arbitrary host while connected.";

	// Token: 0x04000170 RID: 368
	public const string net_no_concurrent_io_allowed = "The stream does not support concurrent IO read or write operations.";

	// Token: 0x04000171 RID: 369
	public const string net_needmorethreads = "There were not enough free threads in the ThreadPool to complete the operation.";

	// Token: 0x04000172 RID: 370
	public const string net_MethodNotSupportedException = "This method is not supported by this class.";

	// Token: 0x04000173 RID: 371
	public const string net_PropertyNotSupportedException = "This property is not supported by this class.";

	// Token: 0x04000174 RID: 372
	public const string net_ProtocolNotSupportedException = "The '{0}' protocol is not supported by this class.";

	// Token: 0x04000175 RID: 373
	public const string net_SelectModeNotSupportedException = "The '{0}' select mode is not supported by this class.";

	// Token: 0x04000176 RID: 374
	public const string net_InvalidSocketHandle = "The socket handle is not valid.";

	// Token: 0x04000177 RID: 375
	public const string net_InvalidAddressFamily = "The AddressFamily {0} is not valid for the {1} end point, use {2} instead.";

	// Token: 0x04000178 RID: 376
	public const string net_InvalidEndPointAddressFamily = "The supplied EndPoint of AddressFamily {0} is not valid for this Socket, use {1} instead.";

	// Token: 0x04000179 RID: 377
	public const string net_InvalidSocketAddressSize = "The supplied {0} is an invalid size for the {1} end point.";

	// Token: 0x0400017A RID: 378
	public const string net_invalidAddressList = "None of the discovered or specified addresses match the socket address family.";

	// Token: 0x0400017B RID: 379
	public const string net_invalidPingBufferSize = "The buffer length must not exceed 65500 bytes.";

	// Token: 0x0400017C RID: 380
	public const string net_cant_perform_during_shutdown = "This operation cannot be performed while the AppDomain is shutting down.";

	// Token: 0x0400017D RID: 381
	public const string net_cant_create_environment = "Unable to create another web proxy script environment at this time.";

	// Token: 0x0400017E RID: 382
	public const string net_completed_result = "This operation cannot be performed on a completed asynchronous result object.";

	// Token: 0x0400017F RID: 383
	public const string net_protocol_invalid_family = "'{0}' Client can only accept InterNetwork or InterNetworkV6 addresses.";

	// Token: 0x04000180 RID: 384
	public const string net_protocol_invalid_multicast_family = "Multicast family is not the same as the family of the '{0}' Client.";

	// Token: 0x04000181 RID: 385
	public const string net_empty_osinstalltype = "The Registry value '{0}' was either empty or not a string type.";

	// Token: 0x04000182 RID: 386
	public const string net_unknown_osinstalltype = "Unknown Windows installation type '{0}'.";

	// Token: 0x04000183 RID: 387
	public const string net_cant_determine_osinstalltype = "Can't determine OS installation type: Can't read key '{0}'. Exception message: {1}";

	// Token: 0x04000184 RID: 388
	public const string net_osinstalltype = "Current OS installation type is '{0}'.";

	// Token: 0x04000185 RID: 389
	public const string net_entire_body_not_written = "You must write ContentLength bytes to the request stream before calling [Begin]GetResponse.";

	// Token: 0x04000186 RID: 390
	public const string net_must_provide_request_body = "You must provide a request body if you set ContentLength>0 or SendChunked==true.  Do this by calling [Begin]GetRequestStream before [Begin]GetResponse.";

	// Token: 0x04000187 RID: 391
	public const string net_sockets_zerolist = "The parameter {0} must contain one or more elements.";

	// Token: 0x04000188 RID: 392
	public const string net_sockets_blocking = "The operation is not allowed on a non-blocking Socket.";

	// Token: 0x04000189 RID: 393
	public const string net_sockets_useblocking = "Use the Blocking property to change the status of the Socket.";

	// Token: 0x0400018A RID: 394
	public const string net_sockets_select = "The operation is not allowed on objects of type {0}. Use only objects of type {1}.";

	// Token: 0x0400018B RID: 395
	public const string net_sockets_toolarge_select = "The {0} list contains too many items; a maximum of {1} is allowed.";

	// Token: 0x0400018C RID: 396
	public const string net_sockets_empty_select = "All lists are either null or empty.";

	// Token: 0x0400018D RID: 397
	public const string net_sockets_mustbind = "You must call the Bind method before performing this operation.";

	// Token: 0x0400018E RID: 398
	public const string net_sockets_mustlisten = "You must call the Listen method before performing this operation.";

	// Token: 0x0400018F RID: 399
	public const string net_sockets_mustnotlisten = "You may not perform this operation after calling the Listen method.";

	// Token: 0x04000190 RID: 400
	public const string net_sockets_mustnotbebound = "The socket must not be bound or connected.";

	// Token: 0x04000191 RID: 401
	public const string net_sockets_namedmustnotbebound = "{0}: The socket must not be bound or connected.";

	// Token: 0x04000192 RID: 402
	public const string net_sockets_invalid_socketinformation = "The specified value for the socket information is invalid.";

	// Token: 0x04000193 RID: 403
	public const string net_sockets_invalid_ipaddress_length = "The number of specified IP addresses has to be greater than 0.";

	// Token: 0x04000194 RID: 404
	public const string net_sockets_invalid_optionValue = "The specified value is not a valid '{0}'.";

	// Token: 0x04000195 RID: 405
	public const string net_sockets_invalid_optionValue_all = "The specified value is not valid.";

	// Token: 0x04000196 RID: 406
	public const string net_sockets_invalid_dnsendpoint = "The parameter {0} must not be of type DnsEndPoint.";

	// Token: 0x04000197 RID: 407
	public const string net_sockets_disconnectedConnect = "Once the socket has been disconnected, you can only reconnect again asynchronously, and only to a different EndPoint.  BeginConnect must be called on a thread that won't exit until the operation has been completed.";

	// Token: 0x04000198 RID: 408
	public const string net_sockets_disconnectedAccept = "Once the socket has been disconnected, you can only accept again asynchronously.  BeginAccept must be called on a thread that won't exit until the operation has been completed.";

	// Token: 0x04000199 RID: 409
	public const string net_tcplistener_mustbestopped = "The TcpListener must not be listening before performing this operation.";

	// Token: 0x0400019A RID: 410
	public const string net_sockets_no_duplicate_async = "BeginConnect cannot be called while another asynchronous operation is in progress on the same Socket.";

	// Token: 0x0400019B RID: 411
	public const string net_socketopinprogress = "An asynchronous socket operation is already in progress using this SocketAsyncEventArgs instance.";

	// Token: 0x0400019C RID: 412
	public const string net_buffercounttoosmall = "The Buffer space specified by the Count property is insufficient for the AcceptAsync method.";

	// Token: 0x0400019D RID: 413
	public const string net_multibuffernotsupported = "Multiple buffers cannot be used with this method.";

	// Token: 0x0400019E RID: 414
	public const string net_ambiguousbuffers = "Buffer and BufferList properties cannot both be non-null.";

	// Token: 0x0400019F RID: 415
	public const string net_sockets_ipv6only = "This operation is only valid for IPv6 Sockets.";

	// Token: 0x040001A0 RID: 416
	public const string net_perfcounter_initialized_success = "System.Net performance counters initialization completed successful.";

	// Token: 0x040001A1 RID: 417
	public const string net_perfcounter_initialized_error = "System.Net performance counters initialization completed with errors. See System.Net trace file for more information.";

	// Token: 0x040001A2 RID: 418
	public const string net_perfcounter_nocategory = "Performance counter category '{0}' doesn't exist. No System.Net performance counter values available.";

	// Token: 0x040001A3 RID: 419
	public const string net_perfcounter_initialization_started = "System.Net performance counter initialization started.";

	// Token: 0x040001A4 RID: 420
	public const string net_perfcounter_cant_queue_workitem = "Can't queue counter initialization logic on a thread pool thread. System.Net performance counters will not be available.";

	// Token: 0x040001A5 RID: 421
	public const string net_config_proxy = "Error creating the Web Proxy specified in the 'system.net/defaultProxy' configuration section.";

	// Token: 0x040001A6 RID: 422
	public const string net_config_proxy_module_not_public = "The specified proxy module type is not public.";

	// Token: 0x040001A7 RID: 423
	public const string net_config_authenticationmodules = "Error creating the modules specified in the 'system.net/authenticationModules' configuration section.";

	// Token: 0x040001A8 RID: 424
	public const string net_config_webrequestmodules = "Error creating the modules specified in the 'system.net/webRequestModules' configuration section.";

	// Token: 0x040001A9 RID: 425
	public const string net_config_requestcaching = "Error creating the Web Request caching policy specified in the 'system.net/requestCaching' configuration section.";

	// Token: 0x040001AA RID: 426
	public const string net_config_section_permission = "Insufficient permissions for setting the configuration section '{0}'.";

	// Token: 0x040001AB RID: 427
	public const string net_config_element_permission = "Insufficient permissions for setting the configuration element '{0}'.";

	// Token: 0x040001AC RID: 428
	public const string net_config_property_permission = "Insufficient permissions for setting the configuration property '{0}'.";

	// Token: 0x040001AD RID: 429
	public const string net_WebResponseParseError_InvalidHeaderName = "Header name is invalid";

	// Token: 0x040001AE RID: 430
	public const string net_WebResponseParseError_InvalidContentLength = "'Content-Length' header value is invalid";

	// Token: 0x040001AF RID: 431
	public const string net_WebResponseParseError_IncompleteHeaderLine = "Invalid header name";

	// Token: 0x040001B0 RID: 432
	public const string net_WebResponseParseError_CrLfError = "CR must be followed by LF";

	// Token: 0x040001B1 RID: 433
	public const string net_WebResponseParseError_InvalidChunkFormat = "Response chunk format is invalid";

	// Token: 0x040001B2 RID: 434
	public const string net_WebResponseParseError_UnexpectedServerResponse = "Unexpected server response received";

	// Token: 0x040001B3 RID: 435
	public const string net_webstatus_Success = "Status success";

	// Token: 0x040001B4 RID: 436
	public const string net_webstatus_NameResolutionFailure = "The remote name could not be resolved";

	// Token: 0x040001B5 RID: 437
	public const string net_webstatus_ConnectFailure = "Unable to connect to the remote server";

	// Token: 0x040001B6 RID: 438
	public const string net_webstatus_ReceiveFailure = "An unexpected error occurred on a receive";

	// Token: 0x040001B7 RID: 439
	public const string net_webstatus_SendFailure = "An unexpected error occurred on a send";

	// Token: 0x040001B8 RID: 440
	public const string net_webstatus_PipelineFailure = "A pipeline failure occurred";

	// Token: 0x040001B9 RID: 441
	public const string net_webstatus_RequestCanceled = "The request was canceled";

	// Token: 0x040001BA RID: 442
	public const string net_webstatus_ConnectionClosed = "The connection was closed unexpectedly";

	// Token: 0x040001BB RID: 443
	public const string net_webstatus_TrustFailure = "Could not establish trust relationship for the SSL/TLS secure channel";

	// Token: 0x040001BC RID: 444
	public const string net_webstatus_SecureChannelFailure = "Could not create SSL/TLS secure channel";

	// Token: 0x040001BD RID: 445
	public const string net_webstatus_ServerProtocolViolation = "The server committed a protocol violation";

	// Token: 0x040001BE RID: 446
	public const string net_webstatus_KeepAliveFailure = "A connection that was expected to be kept alive was closed by the server";

	// Token: 0x040001BF RID: 447
	public const string net_webstatus_ProxyNameResolutionFailure = "The proxy name could not be resolved";

	// Token: 0x040001C0 RID: 448
	public const string net_webstatus_MessageLengthLimitExceeded = "The message length limit was exceeded";

	// Token: 0x040001C1 RID: 449
	public const string net_webstatus_CacheEntryNotFound = "The request cache-only policy does not allow a network request and the response is not found in cache";

	// Token: 0x040001C2 RID: 450
	public const string net_webstatus_RequestProhibitedByCachePolicy = "The request could not be satisfied using a cache-only policy";

	// Token: 0x040001C3 RID: 451
	public const string net_webstatus_Timeout = "The operation has timed out";

	// Token: 0x040001C4 RID: 452
	public const string net_webstatus_RequestProhibitedByProxy = "The IWebProxy object associated with the request did not allow the request to proceed";

	// Token: 0x040001C5 RID: 453
	public const string net_InvalidStatusCode = "The server returned a status code outside the valid range of 100-599.";

	// Token: 0x040001C6 RID: 454
	public const string net_ftpstatuscode_ServiceNotAvailable = "Service not available, closing control connection";

	// Token: 0x040001C7 RID: 455
	public const string net_ftpstatuscode_CantOpenData = "Can't open data connection";

	// Token: 0x040001C8 RID: 456
	public const string net_ftpstatuscode_ConnectionClosed = "Connection closed; transfer aborted";

	// Token: 0x040001C9 RID: 457
	public const string net_ftpstatuscode_ActionNotTakenFileUnavailableOrBusy = "File unavailable (e.g., file busy)";

	// Token: 0x040001CA RID: 458
	public const string net_ftpstatuscode_ActionAbortedLocalProcessingError = "Local error in processing";

	// Token: 0x040001CB RID: 459
	public const string net_ftpstatuscode_ActionNotTakenInsufficentSpace = "Insufficient storage space in system";

	// Token: 0x040001CC RID: 460
	public const string net_ftpstatuscode_CommandSyntaxError = "Syntax error, command unrecognized";

	// Token: 0x040001CD RID: 461
	public const string net_ftpstatuscode_ArgumentSyntaxError = "Syntax error in parameters or arguments";

	// Token: 0x040001CE RID: 462
	public const string net_ftpstatuscode_CommandNotImplemented = "Command not implemented";

	// Token: 0x040001CF RID: 463
	public const string net_ftpstatuscode_BadCommandSequence = "Bad sequence of commands";

	// Token: 0x040001D0 RID: 464
	public const string net_ftpstatuscode_NotLoggedIn = "Not logged in";

	// Token: 0x040001D1 RID: 465
	public const string net_ftpstatuscode_AccountNeeded = "Need account for storing files";

	// Token: 0x040001D2 RID: 466
	public const string net_ftpstatuscode_ActionNotTakenFileUnavailable = "File unavailable (e.g., file not found, no access)";

	// Token: 0x040001D3 RID: 467
	public const string net_ftpstatuscode_ActionAbortedUnknownPageType = "Page type unknown";

	// Token: 0x040001D4 RID: 468
	public const string net_ftpstatuscode_FileActionAborted = "Exceeded storage allocation (for current directory or data set)";

	// Token: 0x040001D5 RID: 469
	public const string net_ftpstatuscode_ActionNotTakenFilenameNotAllowed = "File name not allowed";

	// Token: 0x040001D6 RID: 470
	public const string net_httpstatuscode_NoContent = "No Content";

	// Token: 0x040001D7 RID: 471
	public const string net_httpstatuscode_NonAuthoritativeInformation = "Non Authoritative Information";

	// Token: 0x040001D8 RID: 472
	public const string net_httpstatuscode_ResetContent = "Reset Content";

	// Token: 0x040001D9 RID: 473
	public const string net_httpstatuscode_PartialContent = "Partial Content";

	// Token: 0x040001DA RID: 474
	public const string net_httpstatuscode_MultipleChoices = "Multiple Choices Redirect";

	// Token: 0x040001DB RID: 475
	public const string net_httpstatuscode_Ambiguous = "Ambiguous Redirect";

	// Token: 0x040001DC RID: 476
	public const string net_httpstatuscode_MovedPermanently = "Moved Permanently Redirect";

	// Token: 0x040001DD RID: 477
	public const string net_httpstatuscode_Moved = "Moved Redirect";

	// Token: 0x040001DE RID: 478
	public const string net_httpstatuscode_Found = "Found Redirect";

	// Token: 0x040001DF RID: 479
	public const string net_httpstatuscode_Redirect = "Redirect";

	// Token: 0x040001E0 RID: 480
	public const string net_httpstatuscode_SeeOther = "See Other";

	// Token: 0x040001E1 RID: 481
	public const string net_httpstatuscode_RedirectMethod = "Redirect Method";

	// Token: 0x040001E2 RID: 482
	public const string net_httpstatuscode_NotModified = "Not Modified";

	// Token: 0x040001E3 RID: 483
	public const string net_httpstatuscode_UseProxy = "Use Proxy Redirect";

	// Token: 0x040001E4 RID: 484
	public const string net_httpstatuscode_TemporaryRedirect = "Temporary Redirect";

	// Token: 0x040001E5 RID: 485
	public const string net_httpstatuscode_RedirectKeepVerb = "Redirect Keep Verb";

	// Token: 0x040001E6 RID: 486
	public const string net_httpstatuscode_BadRequest = "Bad Request";

	// Token: 0x040001E7 RID: 487
	public const string net_httpstatuscode_Unauthorized = "Unauthorized";

	// Token: 0x040001E8 RID: 488
	public const string net_httpstatuscode_PaymentRequired = "Payment Required";

	// Token: 0x040001E9 RID: 489
	public const string net_httpstatuscode_Forbidden = "Forbidden";

	// Token: 0x040001EA RID: 490
	public const string net_httpstatuscode_NotFound = "Not Found";

	// Token: 0x040001EB RID: 491
	public const string net_httpstatuscode_MethodNotAllowed = "Method Not Allowed";

	// Token: 0x040001EC RID: 492
	public const string net_httpstatuscode_NotAcceptable = "Not Acceptable";

	// Token: 0x040001ED RID: 493
	public const string net_httpstatuscode_ProxyAuthenticationRequired = "Proxy Authentication Required";

	// Token: 0x040001EE RID: 494
	public const string net_httpstatuscode_RequestTimeout = "Request Timeout";

	// Token: 0x040001EF RID: 495
	public const string net_httpstatuscode_Conflict = "Conflict";

	// Token: 0x040001F0 RID: 496
	public const string net_httpstatuscode_Gone = "Gone";

	// Token: 0x040001F1 RID: 497
	public const string net_httpstatuscode_LengthRequired = "Length Required";

	// Token: 0x040001F2 RID: 498
	public const string net_httpstatuscode_InternalServerError = "Internal Server Error";

	// Token: 0x040001F3 RID: 499
	public const string net_httpstatuscode_NotImplemented = "Not Implemented";

	// Token: 0x040001F4 RID: 500
	public const string net_httpstatuscode_BadGateway = "Bad Gateway";

	// Token: 0x040001F5 RID: 501
	public const string net_httpstatuscode_ServiceUnavailable = "Server Unavailable";

	// Token: 0x040001F6 RID: 502
	public const string net_httpstatuscode_GatewayTimeout = "Gateway Timeout";

	// Token: 0x040001F7 RID: 503
	public const string net_httpstatuscode_HttpVersionNotSupported = "Http Version Not Supported";

	// Token: 0x040001F8 RID: 504
	public const string net_emptystringset = "This property cannot be set to an empty string.";

	// Token: 0x040001F9 RID: 505
	public const string net_emptystringcall = "The parameter '{0}' cannot be an empty string.";

	// Token: 0x040001FA RID: 506
	public const string net_headers_req = "This collection holds response headers and cannot contain the specified request header.";

	// Token: 0x040001FB RID: 507
	public const string net_headers_rsp = "This collection holds request headers and cannot contain the specified response header.";

	// Token: 0x040001FC RID: 508
	public const string net_headers_toolong = "Header values cannot be longer than {0} characters.";

	// Token: 0x040001FD RID: 509
	public const string net_WebHeaderInvalidCRLFChars = "Specified value has invalid CRLF characters.";

	// Token: 0x040001FE RID: 510
	public const string net_WebHeaderInvalidHeaderChars = "Specified value has invalid HTTP Header characters.";

	// Token: 0x040001FF RID: 511
	public const string net_WebHeaderInvalidNonAsciiChars = "Specified value has invalid non-ASCII characters.";

	// Token: 0x04000200 RID: 512
	public const string net_WebHeaderMissingColon = "Specified value does not have a ':' separator.";

	// Token: 0x04000201 RID: 513
	public const string net_headerrestrict = "The '{0}' header must be modified using the appropriate property or method.";

	// Token: 0x04000202 RID: 514
	public const string net_io_completionportwasbound = "The socket has already been bound to an io completion port.";

	// Token: 0x04000203 RID: 515
	public const string net_io_writefailure = "Unable to write data to the transport connection: {0}.";

	// Token: 0x04000204 RID: 516
	public const string net_io_readfailure = "Unable to read data from the transport connection: {0}.";

	// Token: 0x04000205 RID: 517
	public const string net_io_connectionclosed = "The connection was closed";

	// Token: 0x04000206 RID: 518
	public const string net_io_transportfailure = "Unable to create a transport connection.";

	// Token: 0x04000207 RID: 519
	public const string net_io_internal_bind = "Internal Error: A socket handle could not be bound to a completion port.";

	// Token: 0x04000208 RID: 520
	public const string net_io_invalidnestedcall = "The {0} method cannot be called when another {1} operation is pending.";

	// Token: 0x04000209 RID: 521
	public const string net_io_must_be_rw_stream = "The stream has to be read/write.";

	// Token: 0x0400020A RID: 522
	public const string net_io_header_id = "Found a wrong header field {0} read = {1}, expected = {2}.";

	// Token: 0x0400020B RID: 523
	public const string net_io_out_range = "The byte count must not exceed {0} bytes for this stream type.";

	// Token: 0x0400020C RID: 524
	public const string net_io_encrypt = "The encryption operation failed, see inner exception.";

	// Token: 0x0400020D RID: 525
	public const string net_io_decrypt = "The decryption operation failed, see inner exception.";

	// Token: 0x0400020E RID: 526
	public const string net_io_read = "The read operation failed, see inner exception.";

	// Token: 0x0400020F RID: 527
	public const string net_io_write = "The write operation failed, see inner exception.";

	// Token: 0x04000210 RID: 528
	public const string net_io_eof = "Received an unexpected EOF or 0 bytes from the transport stream.";

	// Token: 0x04000211 RID: 529
	public const string net_io_async_result = "The parameter: {0} is not valid. Use the object returned from corresponding Begin async call.";

	// Token: 0x04000212 RID: 530
	public const string net_tls_version = "The SSL version is not supported.";

	// Token: 0x04000213 RID: 531
	public const string net_perm_target = "Cannot cast target permission type.";

	// Token: 0x04000214 RID: 532
	public const string net_perm_both_regex = "Cannot subset Regex. Only support if both patterns are identical.";

	// Token: 0x04000215 RID: 533
	public const string net_perm_none = "There are no permissions to check.";

	// Token: 0x04000216 RID: 534
	public const string net_perm_attrib_count = "The value for '{0}' must be specified.";

	// Token: 0x04000217 RID: 535
	public const string net_perm_invalid_val = "The parameter value '{0}={1}' is invalid.";

	// Token: 0x04000218 RID: 536
	public const string net_perm_attrib_multi = "The permission '{0}={1}' cannot be added. Add a separate Attribute statement.";

	// Token: 0x04000219 RID: 537
	public const string net_perm_epname = "The argument value '{0}' is invalid for creating a SocketPermission object.";

	// Token: 0x0400021A RID: 538
	public const string net_perm_invalid_val_in_element = "The '{0}' element contains one or more invalid values.";

	// Token: 0x0400021B RID: 539
	public const string net_invalid_ip_addr = "IPv4 address 0.0.0.0 and IPv6 address ::0 are unspecified addresses that cannot be used as a target address.";

	// Token: 0x0400021C RID: 540
	public const string dns_bad_ip_address = "An invalid IP address was specified.";

	// Token: 0x0400021D RID: 541
	public const string net_bad_mac_address = "An invalid physical address was specified.";

	// Token: 0x0400021E RID: 542
	public const string net_ping = "An exception occurred during a Ping request.";

	// Token: 0x0400021F RID: 543
	public const string net_bad_ip_address_prefix = "An invalid IP address prefix was specified.";

	// Token: 0x04000220 RID: 544
	public const string net_max_ip_address_list_length_exceeded = "Too many addresses to sort. The maximum number of addresses allowed are {0}.";

	// Token: 0x04000221 RID: 545
	public const string net_ipv4_not_installed = "IPv4 is not installed.";

	// Token: 0x04000222 RID: 546
	public const string net_ipv6_not_installed = "IPv6 is not installed.";

	// Token: 0x04000223 RID: 547
	public const string net_webclient = "An exception occurred during a WebClient request.";

	// Token: 0x04000224 RID: 548
	public const string net_webclient_ContentType = "The Content-Type header cannot be changed from its default value for this request.";

	// Token: 0x04000225 RID: 549
	public const string net_webclient_Multipart = "The Content-Type header cannot be set to a multipart type for this request.";

	// Token: 0x04000226 RID: 550
	public const string net_webclient_no_concurrent_io_allowed = "WebClient does not support concurrent I/O operations.";

	// Token: 0x04000227 RID: 551
	public const string net_webclient_invalid_baseaddress = "The specified value is not a valid base address.";

	// Token: 0x04000228 RID: 552
	public const string net_container_add_cookie = "An error occurred when adding a cookie to the container.";

	// Token: 0x04000229 RID: 553
	public const string net_cookie_invalid = "Invalid contents for cookie = '{0}'.";

	// Token: 0x0400022A RID: 554
	public const string net_cookie_size = "The value size of the cookie is '{0}'. This exceeds the configured maximum size, which is '{1}'.";

	// Token: 0x0400022B RID: 555
	public const string net_cookie_parse_header = "An error occurred when parsing the Cookie header for Uri '{0}'.";

	// Token: 0x0400022C RID: 556
	public const string net_cookie_attribute = "The '{0}'='{1}' part of the cookie is invalid.";

	// Token: 0x0400022D RID: 557
	public const string net_cookie_format = "Cookie format error.";

	// Token: 0x0400022E RID: 558
	public const string net_cookie_capacity_range = "'{0}' has to be greater than '{1}' and less than '{2}'.";

	// Token: 0x0400022F RID: 559
	public const string net_set_token = "Failed to impersonate a thread doing authentication of a Web Request.";

	// Token: 0x04000230 RID: 560
	public const string net_revert_token = "Failed to revert the thread token after authenticating a Web Request.";

	// Token: 0x04000231 RID: 561
	public const string net_ssl_io_async_context = "Async context creation failed.";

	// Token: 0x04000232 RID: 562
	public const string net_ssl_io_encrypt = "The encryption operation failed, see inner exception.";

	// Token: 0x04000233 RID: 563
	public const string net_ssl_io_decrypt = "The decryption operation failed, see inner exception.";

	// Token: 0x04000234 RID: 564
	public const string net_ssl_io_context_expired = "The security context has expired.";

	// Token: 0x04000235 RID: 565
	public const string net_ssl_io_handshake_start = "The handshake failed. The remote side has dropped the stream.";

	// Token: 0x04000236 RID: 566
	public const string net_ssl_io_handshake = "The handshake failed, see inner exception.";

	// Token: 0x04000237 RID: 567
	public const string net_ssl_io_frame = "The handshake failed due to an unexpected packet format.";

	// Token: 0x04000238 RID: 568
	public const string net_ssl_io_corrupted = "The stream is corrupted due to an invalid SSL version number in the SSL protocol header.";

	// Token: 0x04000239 RID: 569
	public const string net_ssl_io_cert_validation = "The remote certificate is invalid according to the validation procedure.";

	// Token: 0x0400023A RID: 570
	public const string net_ssl_io_invalid_end_call = "{0} can only be called once for each asynchronous operation.";

	// Token: 0x0400023B RID: 571
	public const string net_ssl_io_invalid_begin_call = "{0} cannot be called when another {1} operation is pending.";

	// Token: 0x0400023C RID: 572
	public const string net_ssl_io_no_server_cert = "The server mode SSL must use a certificate with the associated private key.";

	// Token: 0x0400023D RID: 573
	public const string net_auth_bad_client_creds = "The server has rejected the client credentials.";

	// Token: 0x0400023E RID: 574
	public const string net_auth_bad_client_creds_or_target_mismatch = "Either the target name is incorrect or the server has rejected the client credentials.";

	// Token: 0x0400023F RID: 575
	public const string net_auth_context_expectation = "A security requirement was not fulfilled during authentication. Required: {0}, negotiated: {1}.";

	// Token: 0x04000240 RID: 576
	public const string net_auth_context_expectation_remote = "A remote side security requirement was not fulfilled during authentication. Try increasing the ProtectionLevel and/or ImpersonationLevel.";

	// Token: 0x04000241 RID: 577
	public const string net_auth_supported_impl_levels = "The supported values are Identification, Impersonation or Delegation.";

	// Token: 0x04000242 RID: 578
	public const string net_auth_no_anonymous_support = "The TokenImpersonationLevel.Anonymous level is not supported for authentication.";

	// Token: 0x04000243 RID: 579
	public const string net_auth_reauth = "This operation is not allowed on a security context that has already been authenticated.";

	// Token: 0x04000244 RID: 580
	public const string net_auth_noauth = "This operation is only allowed using a successfully authenticated context.";

	// Token: 0x04000245 RID: 581
	public const string net_auth_client_server = "Once authentication is attempted as the client or server, additional authentication attempts must use the same client or server role.";

	// Token: 0x04000246 RID: 582
	public const string net_auth_noencryption = "This authenticated context does not support data encryption.";

	// Token: 0x04000247 RID: 583
	public const string net_auth_SSPI = "A call to SSPI failed, see inner exception.";

	// Token: 0x04000248 RID: 584
	public const string net_auth_failure = "Authentication failed, see inner exception.";

	// Token: 0x04000249 RID: 585
	public const string net_auth_eof = "Authentication failed because the remote party has closed the transport stream.";

	// Token: 0x0400024A RID: 586
	public const string net_auth_alert = "Authentication failed on the remote side (the stream might still be available for additional authentication attempts).";

	// Token: 0x0400024B RID: 587
	public const string net_auth_ignored_reauth = "Re-authentication failed because the remote party continued to encrypt more than {0} bytes before answering re-authentication.";

	// Token: 0x0400024C RID: 588
	public const string net_auth_empty_read = "Protocol error: cannot proceed with SSPI handshake because an empty blob was received.";

	// Token: 0x0400024D RID: 589
	public const string net_auth_must_specify_extended_protection_scheme = "An extended protection policy must specify either a custom channel binding or a custom service name collection.";

	// Token: 0x0400024E RID: 590
	public const string net_frame_size = "Received an invalid authentication frame. The message size is limited to {0} bytes, attempted to read {1} bytes.";

	// Token: 0x0400024F RID: 591
	public const string net_frame_read_io = "Received incomplete authentication message. Remote party has probably closed the connection.";

	// Token: 0x04000250 RID: 592
	public const string net_frame_read_size = "Cannot determine the frame size or a corrupted frame was received.";

	// Token: 0x04000251 RID: 593
	public const string net_frame_max_size = "The payload size is limited to {0}, attempted set it to {1}.";

	// Token: 0x04000252 RID: 594
	public const string net_jscript_load = "The proxy JScript file threw an exception while being initialized: {0}.";

	// Token: 0x04000253 RID: 595
	public const string net_proxy_not_gmt = "The specified value is not a valid GMT time.";

	// Token: 0x04000254 RID: 596
	public const string net_proxy_invalid_dayofweek = "The specified value is not a valid day of the week.";

	// Token: 0x04000255 RID: 597
	public const string net_proxy_invalid_url_format = "The system proxy settings contain an invalid proxy server setting: '{0}'.";

	// Token: 0x04000256 RID: 598
	public const string net_param_not_string = "Argument must be a string instead of {0}.";

	// Token: 0x04000257 RID: 599
	public const string net_value_cannot_be_negative = "The specified value cannot be negative.";

	// Token: 0x04000258 RID: 600
	public const string net_invalid_offset = "Value of offset cannot be negative or greater than the length of the buffer.";

	// Token: 0x04000259 RID: 601
	public const string net_offset_plus_count = "Sum of offset and count cannot be greater than the length of the buffer.";

	// Token: 0x0400025A RID: 602
	public const string net_cannot_be_false = "The specified value cannot be false.";

	// Token: 0x0400025B RID: 603
	public const string net_cache_shadowstream_not_writable = "Shadow stream must be writable.";

	// Token: 0x0400025C RID: 604
	public const string net_cache_validator_fail = "The validation method {0}() returned a failure for this request.";

	// Token: 0x0400025D RID: 605
	public const string net_cache_access_denied = "For this RequestCache object, {0} access is denied.";

	// Token: 0x0400025E RID: 606
	public const string net_cache_validator_result = "The validation method {0}() returned the unexpected status: {1}.";

	// Token: 0x0400025F RID: 607
	public const string net_cache_retrieve_failure = "Cache retrieve failed: {0}.";

	// Token: 0x04000260 RID: 608
	public const string net_cache_not_supported_body = "The cached response is not supported for a request with a content body.";

	// Token: 0x04000261 RID: 609
	public const string net_cache_not_supported_command = "The cached response is not supported for a request with the specified request method.";

	// Token: 0x04000262 RID: 610
	public const string net_cache_not_accept_response = "The cache protocol refused the server response. To allow automatic request retrying, set request.AllowAutoRedirect to true.";

	// Token: 0x04000263 RID: 611
	public const string net_cache_method_failed = "The request (Method = {0}) cannot be served from the cache and will fail because of the effective CachePolicy: {1}.";

	// Token: 0x04000264 RID: 612
	public const string net_cache_key_failed = "The request failed because no cache entry (CacheKey = {0}) was found and the effective CachePolicy is {1}.";

	// Token: 0x04000265 RID: 613
	public const string net_cache_no_stream = "The cache protocol returned a cached response but the cache entry is invalid because it has a null stream. (Cache Key = {0}).";

	// Token: 0x04000266 RID: 614
	public const string net_cache_unsupported_partial_stream = "A partial content stream does not support this operation or some method argument is out of range.";

	// Token: 0x04000267 RID: 615
	public const string net_cache_not_configured = "No cache protocol is available for this request.";

	// Token: 0x04000268 RID: 616
	public const string net_cache_non_seekable_stream_not_supported = "The transport stream instance passed in the RangeStream constructor is not seekable and therefore is not supported.";

	// Token: 0x04000269 RID: 617
	public const string net_invalid_cast = "Invalid cast from {0} to {1}.";

	// Token: 0x0400026A RID: 618
	public const string net_collection_readonly = "The collection is read-only.";

	// Token: 0x0400026B RID: 619
	public const string net_not_ipermission = "Specified value does not contain 'IPermission' as its tag.";

	// Token: 0x0400026C RID: 620
	public const string net_no_classname = "Specified value does not contain a 'class' attribute.";

	// Token: 0x0400026D RID: 621
	public const string net_no_typename = "The value class attribute is not valid.";

	// Token: 0x0400026E RID: 622
	public const string net_servicePointAddressNotSupportedInHostMode = "This property is not supported for protocols that do not use URI.";

	// Token: 0x0400026F RID: 623
	public const string net_Websockets_WebSocketBaseFaulted = "An exception caused the WebSocket to enter the Aborted state. Please see the InnerException, if present, for more details.";

	// Token: 0x04000270 RID: 624
	public const string net_WebSockets_Generic = "An internal WebSocket error occurred. Please see the innerException, if present, for more details.";

	// Token: 0x04000271 RID: 625
	public const string net_WebSockets_NotAWebSocket_Generic = "A WebSocket operation was called on a request or response that is not a WebSocket.";

	// Token: 0x04000272 RID: 626
	public const string net_WebSockets_UnsupportedWebSocketVersion_Generic = "Unsupported WebSocket version.";

	// Token: 0x04000273 RID: 627
	public const string net_WebSockets_HeaderError_Generic = "The WebSocket request or response contained unsupported header(s).";

	// Token: 0x04000274 RID: 628
	public const string net_WebSockets_UnsupportedProtocol_Generic = "The WebSocket request or response operation was called with unsupported protocol(s).";

	// Token: 0x04000275 RID: 629
	public const string net_WebSockets_ClientSecWebSocketProtocolsBlank = "The WebSocket client sent a blank '{0}' header; this is not allowed by the WebSocket protocol specification. The client should omit the header if the client is not negotiating any sub-protocols.";

	// Token: 0x04000276 RID: 630
	public const string net_WebSockets_InvalidState_Generic = "The WebSocket instance cannot be used for communication because it has been transitioned into an invalid state.";

	// Token: 0x04000277 RID: 631
	public const string net_WebSockets_InvalidMessageType_Generic = "The received  message type is invalid after calling {0}. {0} should only be used if no more data is expected from the remote endpoint. Use '{1}' instead to keep being able to receive data but close the output channel.";

	// Token: 0x04000278 RID: 632
	public const string net_WebSockets_ConnectionClosedPrematurely_Generic = "The remote party closed the WebSocket connection without completing the close handshake.";

	// Token: 0x04000279 RID: 633
	public const string net_WebSockets_Scheme = "Only Uris starting with 'ws://' or 'wss://' are supported.";

	// Token: 0x0400027A RID: 634
	public const string net_WebSockets_AlreadyStarted = "The WebSocket has already been started.";

	// Token: 0x0400027B RID: 635
	public const string net_WebSockets_Connect101Expected = "The server returned status code '{0}' when status code '101' was expected.";

	// Token: 0x0400027C RID: 636
	public const string net_WebSockets_InvalidResponseHeader = "The '{0}' header value '{1}' is invalid.";

	// Token: 0x0400027D RID: 637
	public const string net_WebSockets_NotConnected = "The WebSocket is not connected.";

	// Token: 0x0400027E RID: 638
	public const string net_WebSockets_InvalidRegistration = "The WebSocket schemes must be registered with the HttpWebRequest class.";

	// Token: 0x0400027F RID: 639
	public const string net_WebSockets_NoDuplicateProtocol = "Duplicate protocols are not allowed: '{0}'.";

	// Token: 0x04000280 RID: 640
	public const string net_log_exception = "Exception in {0}::{1} - {2}.";

	// Token: 0x04000281 RID: 641
	public const string net_log_sspi_enumerating_security_packages = "Enumerating security packages:";

	// Token: 0x04000282 RID: 642
	public const string net_log_sspi_security_package_not_found = "Security package '{0}' was not found.";

	// Token: 0x04000283 RID: 643
	public const string net_log_sspi_security_context_input_buffer = "{0}(In-Buffer length={1}, Out-Buffer length={2}, returned code={3}).";

	// Token: 0x04000284 RID: 644
	public const string net_log_sspi_security_context_input_buffers = "{0}(In-Buffers count={1}, Out-Buffer length={2}, returned code={3}).";

	// Token: 0x04000285 RID: 645
	public const string net_log_sspi_selected_cipher_suite = "{0}(Protocol={1}, Cipher={2} {3} bit strength, Hash={4} {5} bit strength, Key Exchange={6} {7} bit strength).";

	// Token: 0x04000286 RID: 646
	public const string net_log_remote_certificate = "Remote certificate: {0}.";

	// Token: 0x04000287 RID: 647
	public const string net_log_locating_private_key_for_certificate = "Locating the private key for the certificate: {0}.";

	// Token: 0x04000288 RID: 648
	public const string net_log_cert_is_of_type_2 = "Certificate is of type X509Certificate2 and contains the private key.";

	// Token: 0x04000289 RID: 649
	public const string net_log_found_cert_in_store = "Found the certificate in the {0} store.";

	// Token: 0x0400028A RID: 650
	public const string net_log_did_not_find_cert_in_store = "Cannot find the certificate in either the LocalMachine store or the CurrentUser store.";

	// Token: 0x0400028B RID: 651
	public const string net_log_open_store_failed = "Opening Certificate store {0} failed, exception: {1}.";

	// Token: 0x0400028C RID: 652
	public const string net_log_got_certificate_from_delegate = "Got a certificate from the client delegate.";

	// Token: 0x0400028D RID: 653
	public const string net_log_no_delegate_and_have_no_client_cert = "Client delegate did not provide a certificate; and there are not other user-provided certificates. Need to attempt a session restart.";

	// Token: 0x0400028E RID: 654
	public const string net_log_no_delegate_but_have_client_cert = "Client delegate did not provide a certificate; but there are other user-provided certificates\".";

	// Token: 0x0400028F RID: 655
	public const string net_log_attempting_restart_using_cert = "Attempting to restart the session using the user-provided certificate: {0}.";

	// Token: 0x04000290 RID: 656
	public const string net_log_no_issuers_try_all_certs = "We have user-provided certificates. The server has not specified any issuers, so try all the certificates.";

	// Token: 0x04000291 RID: 657
	public const string net_log_server_issuers_look_for_matching_certs = "We have user-provided certificates. The server has specified {0} issuer(s). Looking for certificates that match any of the issuers.";

	// Token: 0x04000292 RID: 658
	public const string net_log_selected_cert = "Selected certificate: {0}.";

	// Token: 0x04000293 RID: 659
	public const string net_log_n_certs_after_filtering = "Left with {0} client certificates to choose from.";

	// Token: 0x04000294 RID: 660
	public const string net_log_finding_matching_certs = "Trying to find a matching certificate in the certificate store.";

	// Token: 0x04000295 RID: 661
	public const string net_log_using_cached_credential = "Using the cached credential handle.";

	// Token: 0x04000296 RID: 662
	public const string net_log_remote_cert_user_declared_valid = "Remote certificate was verified as valid by the user.";

	// Token: 0x04000297 RID: 663
	public const string net_log_remote_cert_user_declared_invalid = "Remote certificate was verified as invalid by the user.";

	// Token: 0x04000298 RID: 664
	public const string net_log_remote_cert_has_no_errors = "Remote certificate has no errors.";

	// Token: 0x04000299 RID: 665
	public const string net_log_remote_cert_has_errors = "Remote certificate has errors:";

	// Token: 0x0400029A RID: 666
	public const string net_log_remote_cert_not_available = "The remote server did not provide a certificate.";

	// Token: 0x0400029B RID: 667
	public const string net_log_remote_cert_name_mismatch = "Certificate name mismatch.";

	// Token: 0x0400029C RID: 668
	public const string net_log_proxy_autodetect_script_location_parse_error = "WebProxy failed to parse the auto-detected location of a proxy script:\"{0}\" into a Uri.";

	// Token: 0x0400029D RID: 669
	public const string net_log_proxy_autodetect_failed = "WebProxy failed to autodetect a Uri for a proxy script.";

	// Token: 0x0400029E RID: 670
	public const string net_log_proxy_script_execution_error = "WebProxy caught an exception while executing the ScriptReturn script: {0}.";

	// Token: 0x0400029F RID: 671
	public const string net_log_proxy_script_download_compile_error = "WebProxy caught an exception while  downloading/compiling the proxy script: {0}.";

	// Token: 0x040002A0 RID: 672
	public const string net_log_proxy_system_setting_update = "ScriptEngine was notified of a potential change in the system's proxy settings and will update WebProxy settings.";

	// Token: 0x040002A1 RID: 673
	public const string net_log_proxy_update_due_to_ip_config_change = "ScriptEngine was notified of a change in the IP configuration and will update WebProxy settings.";

	// Token: 0x040002A2 RID: 674
	public const string net_log_proxy_called_with_null_parameter = "{0} was called with a null '{1}' parameter.";

	// Token: 0x040002A3 RID: 675
	public const string net_log_proxy_called_with_invalid_parameter = "{0} was called with an invalid parameter.";

	// Token: 0x040002A4 RID: 676
	public const string net_log_proxy_ras_supported = "RAS supported: {0}";

	// Token: 0x040002A5 RID: 677
	public const string net_log_proxy_ras_notsupported_exception = "RAS is not supported. Can't create RasHelper instance.";

	// Token: 0x040002A6 RID: 678
	public const string net_log_proxy_winhttp_cant_open_session = "Can't open WinHttp session. Error code: {0}.";

	// Token: 0x040002A7 RID: 679
	public const string net_log_proxy_winhttp_getproxy_failed = "Can't retrieve proxy settings for Uri '{0}'. Error code: {1}.";

	// Token: 0x040002A8 RID: 680
	public const string net_log_proxy_winhttp_timeout_error = "Can't specify proxy discovery timeout. Error code: {0}.";

	// Token: 0x040002A9 RID: 681
	public const string net_log_cache_validation_failed_resubmit = "Resubmitting this request because cache cannot validate the response.";

	// Token: 0x040002AA RID: 682
	public const string net_log_cache_refused_server_response = "Caching protocol has refused the server response. To allow automatic request retrying set request.AllowAutoRedirect=true.";

	// Token: 0x040002AB RID: 683
	public const string net_log_cache_ftp_proxy_doesnt_support_partial = "This FTP request is configured to use a proxy through HTTP protocol. Cache revalidation and partially cached responses are not supported.";

	// Token: 0x040002AC RID: 684
	public const string net_log_cache_ftp_method = "FTP request method={0}.";

	// Token: 0x040002AD RID: 685
	public const string net_log_cache_ftp_supports_bin_only = "Caching is not supported for non-binary FTP request mode.";

	// Token: 0x040002AE RID: 686
	public const string net_log_cache_replacing_entry_with_HTTP_200 = "Replacing cache entry metadata with 'HTTP/1.1 200 OK' status line to satisfy HTTP cache protocol logic.";

	// Token: 0x040002AF RID: 687
	public const string net_log_cache_now_time = "[Now Time (UTC)] = {0}.";

	// Token: 0x040002B0 RID: 688
	public const string net_log_cache_max_age_absolute = "[MaxAge] Absolute time expiration check (sensitive to clock skew), cache Expires: {0}.";

	// Token: 0x040002B1 RID: 689
	public const string net_log_cache_age1 = "[Age1] Now - LastSynchronized = [Age1] Now - LastSynchronized = {0}, Last Synchronized: {1}.";

	// Token: 0x040002B2 RID: 690
	public const string net_log_cache_age1_date_header = "[Age1] NowTime-Date Header = {0}, Date Header: {1}.";

	// Token: 0x040002B3 RID: 691
	public const string net_log_cache_age1_last_synchronized = "[Age1] Now - LastSynchronized + AgeHeader = {0}, Last Synchronized: {1}.";

	// Token: 0x040002B4 RID: 692
	public const string net_log_cache_age1_last_synchronized_age_header = "[Age1] Now - LastSynchronized + AgeHeader = {0}, Last Synchronized: {1}, Age Header: {2}.";

	// Token: 0x040002B5 RID: 693
	public const string net_log_cache_age2 = "[Age2] AgeHeader = {0}.";

	// Token: 0x040002B6 RID: 694
	public const string net_log_cache_max_age_cache_s_max_age = "[MaxAge] Cache s_MaxAge = {0}.";

	// Token: 0x040002B7 RID: 695
	public const string net_log_cache_max_age_expires_date = "[MaxAge] Cache Expires - Date = {0}, Expires: {1}.";

	// Token: 0x040002B8 RID: 696
	public const string net_log_cache_max_age_cache_max_age = "[MaxAge] Cache MaxAge = {0}.";

	// Token: 0x040002B9 RID: 697
	public const string net_log_cache_no_max_age_use_10_percent = "[MaxAge] Cannot compute Cache MaxAge, use 10% since LastModified: {0}, LastModified: {1}.";

	// Token: 0x040002BA RID: 698
	public const string net_log_cache_no_max_age_use_default = "[MaxAge] Cannot compute Cache MaxAge, using default RequestCacheValidator.UnspecifiedMaxAge: {0}.";

	// Token: 0x040002BB RID: 699
	public const string net_log_cache_validator_invalid_for_policy = "This validator should not be called for policy : {0}.";

	// Token: 0x040002BC RID: 700
	public const string net_log_cache_response_last_modified = "Response LastModified={0},  ContentLength= {1}.";

	// Token: 0x040002BD RID: 701
	public const string net_log_cache_cache_last_modified = "Cache    LastModified={0},  ContentLength= {1}.";

	// Token: 0x040002BE RID: 702
	public const string net_log_cache_partial_and_non_zero_content_offset = "A Cache Entry is partial and the user request has non zero ContentOffset = {0}. A restart from cache is not supported for partial cache entries.";

	// Token: 0x040002BF RID: 703
	public const string net_log_cache_response_valid_based_on_policy = "Response is valid based on Policy = {0}.";

	// Token: 0x040002C0 RID: 704
	public const string net_log_cache_null_response_failure = "Response is null so this Request should fail.";

	// Token: 0x040002C1 RID: 705
	public const string net_log_cache_ftp_response_status = "FTP Response Status={0}, {1}.";

	// Token: 0x040002C2 RID: 706
	public const string net_log_cache_resp_valid_based_on_retry = "Accept this response as valid based on the retry count = {0}.";

	// Token: 0x040002C3 RID: 707
	public const string net_log_cache_no_update_based_on_method = "Cache is not updated based on the request Method = {0}.";

	// Token: 0x040002C4 RID: 708
	public const string net_log_cache_removed_existing_invalid_entry = "Existing entry is removed because it was found invalid.";

	// Token: 0x040002C5 RID: 709
	public const string net_log_cache_not_updated_based_on_policy = "Cache is not updated based on Policy = {0}.";

	// Token: 0x040002C6 RID: 710
	public const string net_log_cache_not_updated_because_no_response = "Cache is not updated because there is no response associated with the request.";

	// Token: 0x040002C7 RID: 711
	public const string net_log_cache_removed_existing_based_on_method = "Existing cache entry is removed based on the request Method = {0}.";

	// Token: 0x040002C8 RID: 712
	public const string net_log_cache_existing_not_removed_because_unexpected_response_status = "Existing cache entry should but cannot be removed due to unexpected response Status = ({0}) {1}.";

	// Token: 0x040002C9 RID: 713
	public const string net_log_cache_removed_existing_based_on_policy = "Existing cache entry is removed based on Policy = {0}.";

	// Token: 0x040002CA RID: 714
	public const string net_log_cache_not_updated_based_on_ftp_response_status = "Cache is not updated based on the FTP response status. Expected = {0}, actual = {1}.";

	// Token: 0x040002CB RID: 715
	public const string net_log_cache_update_not_supported_for_ftp_restart = "Cache update is not supported for restarted FTP responses. Restart offset = {0}.";

	// Token: 0x040002CC RID: 716
	public const string net_log_cache_removed_entry_because_ftp_restart_response_changed = "Existing cache entry is removed since a restarted response was changed on the server, cache LastModified date = {0}, new LastModified date = {1}.";

	// Token: 0x040002CD RID: 717
	public const string net_log_cache_last_synchronized = "The cache entry last synchronized time = {0}.";

	// Token: 0x040002CE RID: 718
	public const string net_log_cache_suppress_update_because_synched_last_minute = "Suppressing cache update since the entry was synchronized within the last minute.";

	// Token: 0x040002CF RID: 719
	public const string net_log_cache_updating_last_synchronized = "Updating cache entry last synchronized time = {0}.";

	// Token: 0x040002D0 RID: 720
	public const string net_log_cache_cannot_remove = "{0} Cannot Remove (throw): Key = {1}, Error = {2}.";

	// Token: 0x040002D1 RID: 721
	public const string net_log_cache_key_status = "{0}, Key = {1}, -> Status = {2}.";

	// Token: 0x040002D2 RID: 722
	public const string net_log_cache_key_remove_failed_status = "{0}, Key = {1}, Remove operation failed -> Status = {2}.";

	// Token: 0x040002D3 RID: 723
	public const string net_log_cache_usecount_file = "{0}, UseCount = {1}, File = {2}.";

	// Token: 0x040002D4 RID: 724
	public const string net_log_cache_stream = "{0}, stream = {1}.";

	// Token: 0x040002D5 RID: 725
	public const string net_log_cache_filename = "{0} -> Filename = {1}, Status = {2}.";

	// Token: 0x040002D6 RID: 726
	public const string net_log_cache_lookup_failed = "{0}, Lookup operation failed -> {1}.";

	// Token: 0x040002D7 RID: 727
	public const string net_log_cache_exception = "{0}, Exception = {1}.";

	// Token: 0x040002D8 RID: 728
	public const string net_log_cache_expected_length = "Expected length (0=none)= {0}.";

	// Token: 0x040002D9 RID: 729
	public const string net_log_cache_last_modified = "LastModified    (0=none)= {0}.";

	// Token: 0x040002DA RID: 730
	public const string net_log_cache_expires = "Expires         (0=none)= {0}.";

	// Token: 0x040002DB RID: 731
	public const string net_log_cache_max_stale = "MaxStale (sec)          = {0}.";

	// Token: 0x040002DC RID: 732
	public const string net_log_cache_dumping_metadata = "...Dumping Metadata...";

	// Token: 0x040002DD RID: 733
	public const string net_log_cache_create_failed = "Create operation failed -> {0}.";

	// Token: 0x040002DE RID: 734
	public const string net_log_cache_set_expires = "Set Expires               ={0}.";

	// Token: 0x040002DF RID: 735
	public const string net_log_cache_set_last_modified = "Set LastModified          ={0}.";

	// Token: 0x040002E0 RID: 736
	public const string net_log_cache_set_last_synchronized = "Set LastSynchronized      ={0}.";

	// Token: 0x040002E1 RID: 737
	public const string net_log_cache_enable_max_stale = "Enable MaxStale (sec) ={0}.";

	// Token: 0x040002E2 RID: 738
	public const string net_log_cache_disable_max_stale = "Disable MaxStale (set to 0).";

	// Token: 0x040002E3 RID: 739
	public const string net_log_cache_set_new_metadata = "Set new Metadata.";

	// Token: 0x040002E4 RID: 740
	public const string net_log_cache_dumping = "...Dumping...";

	// Token: 0x040002E5 RID: 741
	public const string net_log_cache_key = "{0}, Key = {1}.";

	// Token: 0x040002E6 RID: 742
	public const string net_log_cache_no_commit = "{0}, Nothing was written to the stream, do not commit that cache entry.";

	// Token: 0x040002E7 RID: 743
	public const string net_log_cache_error_deleting_filename = "{0}, Error deleting a Filename = {1}.";

	// Token: 0x040002E8 RID: 744
	public const string net_log_cache_update_failed = "{0}, Key = {1}, Update operation failed -> {2}.";

	// Token: 0x040002E9 RID: 745
	public const string net_log_cache_delete_failed = "{0}, Key = {1}, Delete operation failed -> {2}.";

	// Token: 0x040002EA RID: 746
	public const string net_log_cache_commit_failed = "{0}, Key = {1}, Commit operation failed -> {2}.";

	// Token: 0x040002EB RID: 747
	public const string net_log_cache_committed_as_partial = "{0}, Key = {1}, Committed entry as partial, not cached bytes count = {2}.";

	// Token: 0x040002EC RID: 748
	public const string net_log_cache_max_stale_and_update_status = "{0}, MaxStale = {1}, Update Status = {2}.";

	// Token: 0x040002ED RID: 749
	public const string net_log_cache_failing_request_with_exception = "Failing request with the WebExceptionStatus = {0}.";

	// Token: 0x040002EE RID: 750
	public const string net_log_cache_request_method = "Request Method = {0}.";

	// Token: 0x040002EF RID: 751
	public const string net_log_cache_http_status_parse_failure = "Cannot Parse Cache HTTP Status Line: {0}.";

	// Token: 0x040002F0 RID: 752
	public const string net_log_cache_http_status_line = "Entry Status Line = HTTP/{0} {1} {2}.";

	// Token: 0x040002F1 RID: 753
	public const string net_log_cache_cache_control = "Cache Cache-Control = {0}.";

	// Token: 0x040002F2 RID: 754
	public const string net_log_cache_invalid_http_version = "The cached version is invalid, assuming HTTP 1.0.";

	// Token: 0x040002F3 RID: 755
	public const string net_log_cache_no_http_response_header = "This Cache Entry does not carry HTTP response headers.";

	// Token: 0x040002F4 RID: 756
	public const string net_log_cache_http_header_parse_error = "Cannot parse HTTP headers in entry metadata, offending string: {0}.";

	// Token: 0x040002F5 RID: 757
	public const string net_log_cache_metadata_name_value_parse_error = "Cannot parse all strings in system metadata as \"name:value\", offending string: {0}.";

	// Token: 0x040002F6 RID: 758
	public const string net_log_cache_content_range_error = "Invalid format of Response Content-Range:{0}.";

	// Token: 0x040002F7 RID: 759
	public const string net_log_cache_cache_control_error = "Invalid CacheControl header = {0}.";

	// Token: 0x040002F8 RID: 760
	public const string net_log_cache_unexpected_status = "The cache protocol method {0} has returned unexpected status: {1}.";

	// Token: 0x040002F9 RID: 761
	public const string net_log_cache_object_and_exception = "{0} exception: {1}.";

	// Token: 0x040002FA RID: 762
	public const string net_log_cache_revalidation_not_needed = "{0}, No cache entry revalidation is needed.";

	// Token: 0x040002FB RID: 763
	public const string net_log_cache_not_updated_based_on_cache_protocol_status = "{0}, Cache is not updated based on the current cache protocol status = {1}.";

	// Token: 0x040002FC RID: 764
	public const string net_log_cache_closing_cache_stream = "{0}: {1} Closing effective cache stream, type = {2}, cache entry key = {3}.";

	// Token: 0x040002FD RID: 765
	public const string net_log_cache_exception_ignored = "{0}: an exception (ignored) on {1} = {2}.";

	// Token: 0x040002FE RID: 766
	public const string net_log_cache_no_cache_entry = "{0} has requested a cache response but the entry does not exist (Stream.Null).";

	// Token: 0x040002FF RID: 767
	public const string net_log_cache_null_cached_stream = "{0} has requested a cache response but the cached stream is null.";

	// Token: 0x04000300 RID: 768
	public const string net_log_cache_requested_combined_but_null_cached_stream = "{0} has requested a combined response but the cached stream is null.";

	// Token: 0x04000301 RID: 769
	public const string net_log_cache_returned_range_cache = "{0} has returned a range cache stream, Offset = {1}, Length = {2}.";

	// Token: 0x04000302 RID: 770
	public const string net_log_cache_entry_not_found_freshness_undefined = "{0}, Cache Entry not found, freshness result = Undefined.";

	// Token: 0x04000303 RID: 771
	public const string net_log_cache_dumping_cache_context = "...Dumping Cache Context...";

	// Token: 0x04000304 RID: 772
	public const string net_log_cache_result = "{0}, result = {1}.";

	// Token: 0x04000305 RID: 773
	public const string net_log_cache_uri_with_query_has_no_expiration = "Request Uri has a Query, and no explicit expiration time is provided.";

	// Token: 0x04000306 RID: 774
	public const string net_log_cache_uri_with_query_and_cached_resp_from_http_10 = "Request Uri has a Query, and cached response is from HTTP 1.0 server.";

	// Token: 0x04000307 RID: 775
	public const string net_log_cache_valid_as_fresh_or_because_policy = "Valid as fresh or because of Cache Policy = {0}.";

	// Token: 0x04000308 RID: 776
	public const string net_log_cache_accept_based_on_retry_count = "Accept this response base on the retry count = {0}.";

	// Token: 0x04000309 RID: 777
	public const string net_log_cache_date_header_older_than_cache_entry = "Response Date header value is older than that of the cache entry.";

	// Token: 0x0400030A RID: 778
	public const string net_log_cache_server_didnt_satisfy_range = "Server did not satisfy the range: {0}.";

	// Token: 0x0400030B RID: 779
	public const string net_log_cache_304_received_on_unconditional_request = "304 response was received on an unconditional request.";

	// Token: 0x0400030C RID: 780
	public const string net_log_cache_304_received_on_unconditional_request_expected_200_206 = "304 response was received on an unconditional request, but expected response code is 200 or 206.";

	// Token: 0x0400030D RID: 781
	public const string net_log_cache_last_modified_header_older_than_cache_entry = "HTTP 1.0 Response Last-Modified header value is older than that of the cache entry.";

	// Token: 0x0400030E RID: 782
	public const string net_log_cache_freshness_outside_policy_limits = "Response freshness is not within the specified policy limits.";

	// Token: 0x0400030F RID: 783
	public const string net_log_cache_need_to_remove_invalid_cache_entry_304 = "Need to remove an invalid cache entry with status code == 304(NotModified).";

	// Token: 0x04000310 RID: 784
	public const string net_log_cache_resp_status = "Response Status = {0}.";

	// Token: 0x04000311 RID: 785
	public const string net_log_cache_resp_304_or_request_head = "Response==304 or Request was HEAD, updating cache entry.";

	// Token: 0x04000312 RID: 786
	public const string net_log_cache_dont_update_cached_headers = "Do not update Cached Headers.";

	// Token: 0x04000313 RID: 787
	public const string net_log_cache_update_cached_headers = "Update Cached Headers.";

	// Token: 0x04000314 RID: 788
	public const string net_log_cache_partial_resp_not_combined_with_existing_entry = "A partial response is not combined with existing cache entry, Cache Stream Size = {0}, response Range Start = {1}.";

	// Token: 0x04000315 RID: 789
	public const string net_log_cache_request_contains_conditional_header = "User Request contains a conditional header.";

	// Token: 0x04000316 RID: 790
	public const string net_log_cache_not_a_get_head_post = "This was Not a GET, HEAD or POST request.";

	// Token: 0x04000317 RID: 791
	public const string net_log_cache_cannot_update_cache_if_304 = "Cannot update cache if Response status == 304 and a cache entry was not found.";

	// Token: 0x04000318 RID: 792
	public const string net_log_cache_cannot_update_cache_with_head_resp = "Cannot update cache with HEAD response if the cache entry does not exist.";

	// Token: 0x04000319 RID: 793
	public const string net_log_cache_http_resp_is_null = "HttpWebResponse is null.";

	// Token: 0x0400031A RID: 794
	public const string net_log_cache_resp_cache_control_is_no_store = "Response Cache-Control = no-store.";

	// Token: 0x0400031B RID: 795
	public const string net_log_cache_resp_cache_control_is_public = "Response Cache-Control = public.";

	// Token: 0x0400031C RID: 796
	public const string net_log_cache_resp_cache_control_is_private = "Response Cache-Control = private, and Cache is public.";

	// Token: 0x0400031D RID: 797
	public const string net_log_cache_resp_cache_control_is_private_plus_headers = "Response Cache-Control = private+Headers, removing those headers.";

	// Token: 0x0400031E RID: 798
	public const string net_log_cache_resp_older_than_cache = "HttpWebResponse date is older than of the cached one.";

	// Token: 0x0400031F RID: 799
	public const string net_log_cache_revalidation_required = "Response revalidation is always required but neither Last-Modified nor ETag header is set on the response.";

	// Token: 0x04000320 RID: 800
	public const string net_log_cache_needs_revalidation = "Response can be cached although it will always require revalidation.";

	// Token: 0x04000321 RID: 801
	public const string net_log_cache_resp_allows_caching = "Response explicitly allows caching = Cache-Control: {0}.";

	// Token: 0x04000322 RID: 802
	public const string net_log_cache_auth_header_and_no_s_max_age = "Request carries Authorization Header and no s-maxage, proxy-revalidate or public directive found.";

	// Token: 0x04000323 RID: 803
	public const string net_log_cache_post_resp_without_cache_control_or_expires = "POST Response without Cache-Control or Expires headers.";

	// Token: 0x04000324 RID: 804
	public const string net_log_cache_valid_based_on_status_code = "Valid based on Status Code: {0}.";

	// Token: 0x04000325 RID: 805
	public const string net_log_cache_resp_no_cache_control = "Response with no CacheControl and Status Code = {0}.";

	// Token: 0x04000326 RID: 806
	public const string net_log_cache_age = "Cache Age = {0}, Cache MaxAge = {1}.";

	// Token: 0x04000327 RID: 807
	public const string net_log_cache_policy_min_fresh = "Client Policy MinFresh = {0}.";

	// Token: 0x04000328 RID: 808
	public const string net_log_cache_policy_max_age = "Client Policy MaxAge = {0}.";

	// Token: 0x04000329 RID: 809
	public const string net_log_cache_policy_cache_sync_date = "Client Policy CacheSyncDate (UTC) = {0}, Cache LastSynchronizedUtc = {1}.";

	// Token: 0x0400032A RID: 810
	public const string net_log_cache_policy_max_stale = "Client Policy MaxStale = {0}.";

	// Token: 0x0400032B RID: 811
	public const string net_log_cache_control_no_cache = "Cached CacheControl = no-cache.";

	// Token: 0x0400032C RID: 812
	public const string net_log_cache_control_no_cache_removing_some_headers = "Cached CacheControl = no-cache, Removing some headers.";

	// Token: 0x0400032D RID: 813
	public const string net_log_cache_control_must_revalidate = "Cached CacheControl = must-revalidate and Cache is not fresh.";

	// Token: 0x0400032E RID: 814
	public const string net_log_cache_cached_auth_header = "The cached entry has Authorization Header and cache is not fresh.";

	// Token: 0x0400032F RID: 815
	public const string net_log_cache_cached_auth_header_no_control_directive = "The cached entry has Authorization Header and no Cache-Control directive present that would allow to use that entry.";

	// Token: 0x04000330 RID: 816
	public const string net_log_cache_after_validation = "After Response Cache Validation.";

	// Token: 0x04000331 RID: 817
	public const string net_log_cache_resp_status_304 = "Response status == 304 but the cache entry does not exist.";

	// Token: 0x04000332 RID: 818
	public const string net_log_cache_head_resp_has_different_content_length = "A response resulted from a HEAD request has different Content-Length header.";

	// Token: 0x04000333 RID: 819
	public const string net_log_cache_head_resp_has_different_content_md5 = "A response resulted from a HEAD request has different Content-MD5 header.";

	// Token: 0x04000334 RID: 820
	public const string net_log_cache_head_resp_has_different_etag = "A response resulted from a HEAD request has different ETag header.";

	// Token: 0x04000335 RID: 821
	public const string net_log_cache_304_head_resp_has_different_last_modified = "A 304 response resulted from a HEAD request has different Last-Modified header.";

	// Token: 0x04000336 RID: 822
	public const string net_log_cache_existing_entry_has_to_be_discarded = "An existing cache entry has to be discarded.";

	// Token: 0x04000337 RID: 823
	public const string net_log_cache_existing_entry_should_be_discarded = "An existing cache entry should be discarded.";

	// Token: 0x04000338 RID: 824
	public const string net_log_cache_206_resp_non_matching_entry = "A 206 Response has been received and either ETag or Last-Modified header value does not match cache entry.";

	// Token: 0x04000339 RID: 825
	public const string net_log_cache_206_resp_starting_position_not_adjusted = "The starting position for 206 Response is not adjusted to the end of cache entry.";

	// Token: 0x0400033A RID: 826
	public const string net_log_cache_combined_resp_requested = "Creation of a combined response has been requested from the cache protocol.";

	// Token: 0x0400033B RID: 827
	public const string net_log_cache_updating_headers_on_304 = "Updating headers on 304 response.";

	// Token: 0x0400033C RID: 828
	public const string net_log_cache_suppressing_headers_update_on_304 = "Suppressing cache headers update on 304, new headers don't add anything.";

	// Token: 0x0400033D RID: 829
	public const string net_log_cache_status_code_not_304_206 = "A Response Status Code is not 304 or 206.";

	// Token: 0x0400033E RID: 830
	public const string net_log_cache_sxx_resp_cache_only = "A 5XX Response and Cache-Only like policy, serving from cache.";

	// Token: 0x0400033F RID: 831
	public const string net_log_cache_sxx_resp_can_be_replaced = "A 5XX Response that can be replaced by existing cache entry.";

	// Token: 0x04000340 RID: 832
	public const string net_log_cache_vary_header_empty = "Cache entry Vary header is empty.";

	// Token: 0x04000341 RID: 833
	public const string net_log_cache_vary_header_contains_asterisks = "Cache entry Vary header contains '*'.";

	// Token: 0x04000342 RID: 834
	public const string net_log_cache_no_headers_in_metadata = "No request headers are found in cached metadata to test based on the cached response Vary header.";

	// Token: 0x04000343 RID: 835
	public const string net_log_cache_vary_header_mismatched_count = "Vary header: Request and cache header fields count does not match, header name = {0}.";

	// Token: 0x04000344 RID: 836
	public const string net_log_cache_vary_header_mismatched_field = "Vary header: A Cache header field mismatch the request one, header name = {0}, cache field = {1}, request field = {2}.";

	// Token: 0x04000345 RID: 837
	public const string net_log_cache_vary_header_match = "All required Request headers match based on cached Vary response header.";

	// Token: 0x04000346 RID: 838
	public const string net_log_cache_range = "Request Range (not in Cache yet) = Range:{0}.";

	// Token: 0x04000347 RID: 839
	public const string net_log_cache_range_invalid_format = "Invalid format of Request Range:{0}.";

	// Token: 0x04000348 RID: 840
	public const string net_log_cache_range_not_in_cache = "Cannot serve from Cache, Range:{0}.";

	// Token: 0x04000349 RID: 841
	public const string net_log_cache_range_in_cache = "Serving Request Range from cache, Range:{0}.";

	// Token: 0x0400034A RID: 842
	public const string net_log_cache_partial_resp = "Serving Partial Response (206) from cache, Content-Range:{0}.";

	// Token: 0x0400034B RID: 843
	public const string net_log_cache_range_request_range = "Range Request (user specified), Range: {0}.";

	// Token: 0x0400034C RID: 844
	public const string net_log_cache_could_be_partial = "Could be a Partial Cached Response, Size = {0}, Response Content Length = {1}.";

	// Token: 0x0400034D RID: 845
	public const string net_log_cache_condition_if_none_match = "Request Condition = If-None-Match:{0}.";

	// Token: 0x0400034E RID: 846
	public const string net_log_cache_condition_if_modified_since = "Request Condition = If-Modified-Since:{0}.";

	// Token: 0x0400034F RID: 847
	public const string net_log_cache_cannot_construct_conditional_request = "A Conditional Request cannot be constructed.";

	// Token: 0x04000350 RID: 848
	public const string net_log_cache_cannot_construct_conditional_range_request = "A Conditional Range request cannot be constructed.";

	// Token: 0x04000351 RID: 849
	public const string net_log_cache_entry_size_too_big = "Cached Entry Size = {0} is too big, cannot do a range request.";

	// Token: 0x04000352 RID: 850
	public const string net_log_cache_condition_if_range = "Request Condition = If-Range:{0}.";

	// Token: 0x04000353 RID: 851
	public const string net_log_cache_conditional_range_not_implemented_on_http_10 = "A Conditional Range request on Http <= 1.0 is not implemented.";

	// Token: 0x04000354 RID: 852
	public const string net_log_cache_saving_request_headers = "Saving Request Headers, Vary: {0}.";

	// Token: 0x04000355 RID: 853
	public const string net_log_cache_only_byte_range_implemented = "Ranges other than bytes are not implemented.";

	// Token: 0x04000356 RID: 854
	public const string net_log_cache_multiple_complex_range_not_implemented = "Multiple/complexe ranges are not implemented.";

	// Token: 0x04000357 RID: 855
	public const string net_log_digest_hash_algorithm_not_supported = "The hash algorithm is not supported by Digest authentication: {0}.";

	// Token: 0x04000358 RID: 856
	public const string net_log_digest_qop_not_supported = "The Quality of Protection value is not supported by Digest authentication: {0}.";

	// Token: 0x04000359 RID: 857
	public const string net_log_digest_requires_nonce = "A nonce parameter required for Digest authentication was not found or was preceded by an invalid parameter.";

	// Token: 0x0400035A RID: 858
	public const string net_log_auth_invalid_challenge = "The challenge string is not valid for this authentication module: {0}";

	// Token: 0x0400035B RID: 859
	public const string net_log_unknown = "unknown";

	// Token: 0x0400035C RID: 860
	public const string net_log_operation_returned_something = "{0} returned {1}.";

	// Token: 0x0400035D RID: 861
	public const string net_log_buffered_n_bytes = "Buffered {0} bytes.";

	// Token: 0x0400035E RID: 862
	public const string net_log_method_equal = "Method={0}.";

	// Token: 0x0400035F RID: 863
	public const string net_log_releasing_connection = "Releasing FTP connection#{0}.";

	// Token: 0x04000360 RID: 864
	public const string net_log_unexpected_exception = "Unexpected exception in {0}.";

	// Token: 0x04000361 RID: 865
	public const string net_log_server_response_error_code = "Error code {0} was received from server response.";

	// Token: 0x04000362 RID: 866
	public const string net_log_resubmitting_request = "Resubmitting request.";

	// Token: 0x04000363 RID: 867
	public const string net_log_retrieving_localhost_exception = "An unexpected exception while retrieving the local address list: {0}.";

	// Token: 0x04000364 RID: 868
	public const string net_log_resolved_servicepoint_may_not_be_remote_server = "A resolved ServicePoint host could be wrongly considered as a remote server.";

	// Token: 0x04000365 RID: 869
	public const string net_log_closed_idle = "{0}#{1} - Closed as idle.";

	// Token: 0x04000366 RID: 870
	public const string net_log_received_status_line = "Received status line: Version={0}, StatusCode={1}, StatusDescription={2}.";

	// Token: 0x04000367 RID: 871
	public const string net_log_sending_headers = "Sending headers\r\n{{\r\n{0}}}.";

	// Token: 0x04000368 RID: 872
	public const string net_log_received_headers = "Received headers\r\n{{\r\n{0}}}.";

	// Token: 0x04000369 RID: 873
	public const string net_log_shell_expression_pattern_format_warning = "ShellServices.ShellExpression.Parse() was called with a badly formatted 'pattern':{0}.";

	// Token: 0x0400036A RID: 874
	public const string net_log_exception_in_callback = "Exception in callback: {0}.";

	// Token: 0x0400036B RID: 875
	public const string net_log_sending_command = "Sending command [{0}]";

	// Token: 0x0400036C RID: 876
	public const string net_log_received_response = "Received response [{0}]";

	// Token: 0x0400036D RID: 877
	public const string net_log_socket_connected = "Created connection from {0} to {1}.";

	// Token: 0x0400036E RID: 878
	public const string net_log_socket_accepted = "Accepted connection from {0} to {1}.";

	// Token: 0x0400036F RID: 879
	public const string net_log_socket_not_logged_file = "Not logging data from file: {0}.";

	// Token: 0x04000370 RID: 880
	public const string net_log_socket_connect_dnsendpoint = "Connecting to a DnsEndPoint.";

	// Token: 0x04000371 RID: 881
	public const string MailAddressInvalidFormat = "The specified string is not in the form required for an e-mail address.";

	// Token: 0x04000372 RID: 882
	public const string MailSubjectInvalidFormat = "The specified string is not in the form required for a subject.";

	// Token: 0x04000373 RID: 883
	public const string MailBase64InvalidCharacter = "An invalid character was found in the Base-64 stream.";

	// Token: 0x04000374 RID: 884
	public const string MailCollectionIsReadOnly = "The collection is read-only.";

	// Token: 0x04000375 RID: 885
	public const string MailDateInvalidFormat = "The date is in an invalid format.";

	// Token: 0x04000376 RID: 886
	public const string MailHeaderFieldAlreadyExists = "The specified singleton field already exists in the collection and cannot be added.";

	// Token: 0x04000377 RID: 887
	public const string MailHeaderFieldInvalidCharacter = "An invalid character was found in the mail header: '{0}'.";

	// Token: 0x04000378 RID: 888
	public const string MailHeaderFieldMalformedHeader = "The mail header is malformed.";

	// Token: 0x04000379 RID: 889
	public const string MailHeaderFieldMismatchedName = "The header name does not match this property.";

	// Token: 0x0400037A RID: 890
	public const string MailHeaderIndexOutOfBounds = "The index value is outside the bounds of the array.";

	// Token: 0x0400037B RID: 891
	public const string MailHeaderItemAccessorOnlySingleton = "The Item property can only be used with singleton fields.";

	// Token: 0x0400037C RID: 892
	public const string MailHeaderListHasChanged = "The underlying list has been changed and the enumeration is out of date.";

	// Token: 0x0400037D RID: 893
	public const string MailHeaderResetCalledBeforeEOF = "The stream should have been consumed before resetting.";

	// Token: 0x0400037E RID: 894
	public const string MailHeaderTargetArrayTooSmall = "The target array is too small to contain all the headers.";

	// Token: 0x0400037F RID: 895
	public const string MailHeaderInvalidCID = "The ContentID cannot contain a '<' or '>' character.";

	// Token: 0x04000380 RID: 896
	public const string MailHostNotFound = "The SMTP host was not found.";

	// Token: 0x04000381 RID: 897
	public const string MailReaderGetContentStreamAlreadyCalled = "GetContentStream() can only be called once.";

	// Token: 0x04000382 RID: 898
	public const string MailReaderTruncated = "Premature end of stream.";

	// Token: 0x04000383 RID: 899
	public const string MailWriterIsInContent = "This operation cannot be performed while in content.";

	// Token: 0x04000384 RID: 900
	public const string MailServerDoesNotSupportStartTls = "Server does not support secure connections.";

	// Token: 0x04000385 RID: 901
	public const string MailServerResponse = "The server response was: {0}";

	// Token: 0x04000386 RID: 902
	public const string SSPIAuthenticationOrSPNNull = "AuthenticationType and ServicePrincipalName cannot be specified as null for server's SSPI Negotiation module.";

	// Token: 0x04000387 RID: 903
	public const string SSPIPInvokeError = "{0} failed with error {1}.";

	// Token: 0x04000388 RID: 904
	public const string SmtpAlreadyConnected = "Already connected.";

	// Token: 0x04000389 RID: 905
	public const string SmtpAuthenticationFailed = "Authentication failed.";

	// Token: 0x0400038A RID: 906
	public const string SmtpAuthenticationFailedNoCreds = "Authentication failed due to lack of credentials.";

	// Token: 0x0400038B RID: 907
	public const string SmtpDataStreamOpen = "Data stream is still open.";

	// Token: 0x0400038C RID: 908
	public const string SmtpDefaultMimePreamble = "This is a multi-part MIME message.";

	// Token: 0x0400038D RID: 909
	public const string SmtpDefaultSubject = "@@SOAP Application Message";

	// Token: 0x0400038E RID: 910
	public const string SmtpInvalidResponse = "Smtp server returned an invalid response.";

	// Token: 0x0400038F RID: 911
	public const string SmtpNotConnected = "Not connected.";

	// Token: 0x04000390 RID: 912
	public const string SmtpSystemStatus = "System status, or system help reply.";

	// Token: 0x04000391 RID: 913
	public const string SmtpHelpMessage = "Help message.";

	// Token: 0x04000392 RID: 914
	public const string SmtpServiceReady = "Service ready.";

	// Token: 0x04000393 RID: 915
	public const string SmtpServiceClosingTransmissionChannel = "Service closing transmission channel.";

	// Token: 0x04000394 RID: 916
	public const string SmtpOK = "Completed.";

	// Token: 0x04000395 RID: 917
	public const string SmtpUserNotLocalWillForward = "User not local; will forward to specified path.";

	// Token: 0x04000396 RID: 918
	public const string SmtpStartMailInput = "Start mail input; end with <CRLF>.<CRLF>.";

	// Token: 0x04000397 RID: 919
	public const string SmtpServiceNotAvailable = "Service not available, closing transmission channel.";

	// Token: 0x04000398 RID: 920
	public const string SmtpMailboxBusy = "Mailbox unavailable.";

	// Token: 0x04000399 RID: 921
	public const string SmtpLocalErrorInProcessing = "Error in processing.";

	// Token: 0x0400039A RID: 922
	public const string SmtpInsufficientStorage = "Insufficient system storage.";

	// Token: 0x0400039B RID: 923
	public const string SmtpPermissionDenied = "Client does not have permission to Send As this sender.";

	// Token: 0x0400039C RID: 924
	public const string SmtpCommandUnrecognized = "Syntax error, command unrecognized.";

	// Token: 0x0400039D RID: 925
	public const string SmtpSyntaxError = "Syntax error in parameters or arguments.";

	// Token: 0x0400039E RID: 926
	public const string SmtpCommandNotImplemented = "Command not implemented.";

	// Token: 0x0400039F RID: 927
	public const string SmtpBadCommandSequence = "Bad sequence of commands.";

	// Token: 0x040003A0 RID: 928
	public const string SmtpCommandParameterNotImplemented = "Command parameter not implemented.";

	// Token: 0x040003A1 RID: 929
	public const string SmtpMailboxUnavailable = "Mailbox unavailable.";

	// Token: 0x040003A2 RID: 930
	public const string SmtpUserNotLocalTryAlternatePath = "User not local; please try a different path.";

	// Token: 0x040003A3 RID: 931
	public const string SmtpExceededStorageAllocation = "Exceeded storage allocation.";

	// Token: 0x040003A4 RID: 932
	public const string SmtpMailboxNameNotAllowed = "Mailbox name not allowed.";

	// Token: 0x040003A5 RID: 933
	public const string SmtpTransactionFailed = "Transaction failed.";

	// Token: 0x040003A6 RID: 934
	public const string SmtpSendMailFailure = "Failure sending mail.";

	// Token: 0x040003A7 RID: 935
	public const string SmtpRecipientFailed = "Unable to send to a recipient.";

	// Token: 0x040003A8 RID: 936
	public const string SmtpRecipientRequired = "A recipient must be specified.";

	// Token: 0x040003A9 RID: 937
	public const string SmtpFromRequired = "A from address must be specified.";

	// Token: 0x040003AA RID: 938
	public const string SmtpAllRecipientsFailed = "Unable to send to all recipients.";

	// Token: 0x040003AB RID: 939
	public const string SmtpClientNotPermitted = "Client does not have permission to submit mail to this server.";

	// Token: 0x040003AC RID: 940
	public const string SmtpMustIssueStartTlsFirst = "The SMTP server requires a secure connection or the client was not authenticated.";

	// Token: 0x040003AD RID: 941
	public const string SmtpNeedAbsolutePickupDirectory = "Only absolute directories are allowed for pickup directory.";

	// Token: 0x040003AE RID: 942
	public const string SmtpGetIisPickupDirectoryFailed = "Cannot get IIS pickup directory.";

	// Token: 0x040003AF RID: 943
	public const string SmtpPickupDirectoryDoesnotSupportSsl = "SSL must not be enabled for pickup-directory delivery methods.";

	// Token: 0x040003B0 RID: 944
	public const string SmtpOperationInProgress = "Previous operation is still in progress.";

	// Token: 0x040003B1 RID: 945
	public const string SmtpAuthResponseInvalid = "The server returned an invalid response in the authentication handshake.";

	// Token: 0x040003B2 RID: 946
	public const string SmtpEhloResponseInvalid = "The server returned an invalid response to the EHLO command.";

	// Token: 0x040003B3 RID: 947
	public const string SmtpNonAsciiUserNotSupported = "The client or server is only configured for E-mail addresses with ASCII local-parts: {0}.";

	// Token: 0x040003B4 RID: 948
	public const string SmtpInvalidHostName = "The address has an invalid host name: {0}.";

	// Token: 0x040003B5 RID: 949
	public const string MimeTransferEncodingNotSupported = "The MIME transfer encoding '{0}' is not supported.";

	// Token: 0x040003B6 RID: 950
	public const string SeekNotSupported = "Seeking is not supported on this stream.";

	// Token: 0x040003B7 RID: 951
	public const string WriteNotSupported = "Writing is not supported on this stream.";

	// Token: 0x040003B8 RID: 952
	public const string InvalidHexDigit = "Invalid hex digit '{0}'.";

	// Token: 0x040003B9 RID: 953
	public const string InvalidSSPIContext = "The SSPI context is not valid.";

	// Token: 0x040003BA RID: 954
	public const string InvalidSSPIContextKey = "A null session key was obtained from SSPI.";

	// Token: 0x040003BB RID: 955
	public const string InvalidSSPINegotiationElement = "Invalid SSPI BinaryNegotiationElement.";

	// Token: 0x040003BC RID: 956
	public const string InvalidHeaderName = "An invalid character was found in header name.";

	// Token: 0x040003BD RID: 957
	public const string InvalidHeaderValue = "An invalid character was found in header value.";

	// Token: 0x040003BE RID: 958
	public const string CannotGetEffectiveTimeOfSSPIContext = "Cannot get the effective time of the SSPI context.";

	// Token: 0x040003BF RID: 959
	public const string CannotGetExpiryTimeOfSSPIContext = "Cannot get the expiry time of the SSPI context.";

	// Token: 0x040003C0 RID: 960
	public const string ReadNotSupported = "Reading is not supported on this stream.";

	// Token: 0x040003C1 RID: 961
	public const string InvalidAsyncResult = "The AsyncResult is not valid.";

	// Token: 0x040003C2 RID: 962
	public const string UnspecifiedHost = "The SMTP host was not specified.";

	// Token: 0x040003C3 RID: 963
	public const string InvalidPort = "The specified port is invalid. The port must be greater than 0.";

	// Token: 0x040003C4 RID: 964
	public const string SmtpInvalidOperationDuringSend = "This operation cannot be performed while a message is being sent.";

	// Token: 0x040003C5 RID: 965
	public const string MimePartCantResetStream = "One of the streams has already been used and can't be reset to the origin.";

	// Token: 0x040003C6 RID: 966
	public const string MediaTypeInvalid = "The specified media type is invalid.";

	// Token: 0x040003C7 RID: 967
	public const string ContentTypeInvalid = "The specified content type is invalid.";

	// Token: 0x040003C8 RID: 968
	public const string ContentDispositionInvalid = "The specified content disposition is invalid.";

	// Token: 0x040003C9 RID: 969
	public const string AttributeNotSupported = "'{0}' is not a valid configuration attribute for type '{1}'.";

	// Token: 0x040003CA RID: 970
	public const string Cannot_remove_with_null = "Cannot remove with null name.";

	// Token: 0x040003CB RID: 971
	public const string Config_base_elements_only = "Only elements allowed.";

	// Token: 0x040003CC RID: 972
	public const string Config_base_no_child_nodes = "Child nodes not allowed.";

	// Token: 0x040003CD RID: 973
	public const string Config_base_required_attribute_empty = "Required attribute '{0}' cannot be empty.";

	// Token: 0x040003CE RID: 974
	public const string Config_base_required_attribute_missing = "Required attribute '{0}' not found.";

	// Token: 0x040003CF RID: 975
	public const string Config_base_time_overflow = "The time span for the property '{0}' exceeds the maximum that can be stored in the configuration.";

	// Token: 0x040003D0 RID: 976
	public const string Config_base_type_must_be_configurationvalidation = "The ConfigurationValidation attribute must be derived from ConfigurationValidation.";

	// Token: 0x040003D1 RID: 977
	public const string Config_base_type_must_be_typeconverter = "The ConfigurationPropertyConverter attribute must be derived from TypeConverter.";

	// Token: 0x040003D2 RID: 978
	public const string Config_base_unknown_format = "Unknown";

	// Token: 0x040003D3 RID: 979
	public const string Config_base_unrecognized_attribute = "Unrecognized attribute '{0}'. Note that attribute names are case-sensitive.";

	// Token: 0x040003D4 RID: 980
	public const string Config_base_unrecognized_element = "Unrecognized element.";

	// Token: 0x040003D5 RID: 981
	public const string Config_invalid_boolean_attribute = "The property '{0}' must have value 'true' or 'false'.";

	// Token: 0x040003D6 RID: 982
	public const string Config_invalid_integer_attribute = "The '{0}' attribute must be set to an integer value.";

	// Token: 0x040003D7 RID: 983
	public const string Config_invalid_positive_integer_attribute = "The '{0}' attribute must be set to a positive integer value.";

	// Token: 0x040003D8 RID: 984
	public const string Config_invalid_type_attribute = "The '{0}' attribute must be set to a valid Type name.";

	// Token: 0x040003D9 RID: 985
	public const string Config_missing_required_attribute = "The '{0}' attribute must be specified on the '{1}' tag.";

	// Token: 0x040003DA RID: 986
	public const string Config_name_value_file_section_file_invalid_root = "The root element must match the name of the section referencing the file, '{0}'";

	// Token: 0x040003DB RID: 987
	public const string Config_provider_must_implement_type = "Provider must implement the class '{0}'.";

	// Token: 0x040003DC RID: 988
	public const string Config_provider_name_null_or_empty = "Provider name cannot be null or empty.";

	// Token: 0x040003DD RID: 989
	public const string Config_provider_not_found = "The provider was not found in the collection.";

	// Token: 0x040003DE RID: 990
	public const string Config_property_name_cannot_be_empty = "Property '{0}' cannot be empty or null.";

	// Token: 0x040003DF RID: 991
	public const string Config_section_cannot_clear_locked_section = "Cannot clear section handlers.  Section '{0}' is locked.";

	// Token: 0x040003E0 RID: 992
	public const string Config_section_record_not_found = "SectionRecord not found.";

	// Token: 0x040003E1 RID: 993
	public const string Config_source_cannot_contain_file = "The 'File' property cannot be used with the ConfigSource property.";

	// Token: 0x040003E2 RID: 994
	public const string Config_system_already_set = "The configuration system can only be set once.  Configuration system is already set";

	// Token: 0x040003E3 RID: 995
	public const string Config_unable_to_read_security_policy = "Unable to read security policy.";

	// Token: 0x040003E4 RID: 996
	public const string Config_write_xml_returned_null = "WriteXml returned null.";

	// Token: 0x040003E5 RID: 997
	public const string Cannot_clear_sections_within_group = "Server cannot clear configuration sections from within section groups.  <clear/> must be a child of <configSections>.";

	// Token: 0x040003E6 RID: 998
	public const string Cannot_exit_up_top_directory = "Cannot use a leading .. to exit above the top directory.";

	// Token: 0x040003E7 RID: 999
	public const string Could_not_create_listener = "Couldn't create listener '{0}'.";

	// Token: 0x040003E8 RID: 1000
	public const string TL_InitializeData_NotSpecified = "initializeData needs to be valid for this TraceListener.";

	// Token: 0x040003E9 RID: 1001
	public const string Could_not_create_type_instance = "Could not create {0}.";

	// Token: 0x040003EA RID: 1002
	public const string Could_not_find_type = "Couldn't find type for class {0}.";

	// Token: 0x040003EB RID: 1003
	public const string Could_not_get_constructor = "Couldn't find constructor for class {0}.";

	// Token: 0x040003EC RID: 1004
	public const string EmptyTypeName_NotAllowed = "switchType needs to be a valid class name. It can't be empty.";

	// Token: 0x040003ED RID: 1005
	public const string Incorrect_base_type = "The specified type, '{0}' is not derived from the appropriate base type, '{1}'.";

	// Token: 0x040003EE RID: 1006
	public const string Only_specify_one = "'switchValue' and 'switchName' cannot both be specified on source '{0}'.";

	// Token: 0x040003EF RID: 1007
	public const string Provider_Already_Initialized = "This provider instance has already been initialized.";

	// Token: 0x040003F0 RID: 1008
	public const string Reference_listener_cant_have_properties = "A listener with no type name specified references the sharedListeners section and cannot have any attributes other than 'Name'.  Listener: '{0}'.";

	// Token: 0x040003F1 RID: 1009
	public const string Reference_to_nonexistent_listener = "Listener '{0}' does not exist in the sharedListeners section.";

	// Token: 0x040003F2 RID: 1010
	public const string SettingsPropertyNotFound = "The settings property '{0}' was not found.";

	// Token: 0x040003F3 RID: 1011
	public const string SettingsPropertyReadOnly = "The settings property '{0}' is read-only.";

	// Token: 0x040003F4 RID: 1012
	public const string SettingsPropertyWrongType = "The settings property '{0}' is of a non-compatible type.";

	// Token: 0x040003F5 RID: 1013
	public const string Type_isnt_tracelistener = "Could not add trace listener {0} because it is not a subclass of TraceListener.";

	// Token: 0x040003F6 RID: 1014
	public const string Unable_to_convert_type_from_string = "Could not find a type-converter to convert object if type '{0}' from string.";

	// Token: 0x040003F7 RID: 1015
	public const string Unable_to_convert_type_to_string = "Could not find a type-converter to convert object if type '{0}' to string.";

	// Token: 0x040003F8 RID: 1016
	public const string Value_must_be_numeric = "Error in trace switch '{0}': The value of a switch must be integral.";

	// Token: 0x040003F9 RID: 1017
	public const string Could_not_create_from_default_value = "The property '{0}' could not be created from it's default value. Error message: {1}";

	// Token: 0x040003FA RID: 1018
	public const string Could_not_create_from_default_value_2 = "The property '{0}' could not be created from it's default value because the default value is of a different type.";

	// Token: 0x040003FB RID: 1019
	public const string InvalidDirName = "The directory name {0} is invalid.";

	// Token: 0x040003FC RID: 1020
	public const string FSW_IOError = "Error reading the {0} directory.";

	// Token: 0x040003FD RID: 1021
	public const string PatternInvalidChar = "The character '{0}' in the pattern provided is not valid.";

	// Token: 0x040003FE RID: 1022
	public const string BufferSizeTooLarge = "The specified buffer size is too large. FileSystemWatcher cannot allocate {0} bytes for the internal buffer.";

	// Token: 0x040003FF RID: 1023
	public const string FSW_ChangedFilter = "Flag to indicate which change event to monitor.";

	// Token: 0x04000400 RID: 1024
	public const string FSW_Enabled = "Flag to indicate whether this component is active or not.";

	// Token: 0x04000401 RID: 1025
	public const string FSW_Filter = "The file pattern filter.";

	// Token: 0x04000402 RID: 1026
	public const string FSW_IncludeSubdirectories = "Flag to watch subdirectories.";

	// Token: 0x04000403 RID: 1027
	public const string FSW_Path = "The path to the directory to monitor.";

	// Token: 0x04000404 RID: 1028
	public const string FSW_SynchronizingObject = "The object used to marshal the event handler calls issued as a result of a Directory change.";

	// Token: 0x04000405 RID: 1029
	public const string FSW_Changed = "Occurs when a file and/or directory change matches the filter.";

	// Token: 0x04000406 RID: 1030
	public const string FSW_Created = "Occurs when a file and/or directory creation matches the filter.";

	// Token: 0x04000407 RID: 1031
	public const string FSW_Deleted = "Occurs when a file and/or directory deletion matches the filter.";

	// Token: 0x04000408 RID: 1032
	public const string FSW_Renamed = "Occurs when a file and/or directory rename matches the filter.";

	// Token: 0x04000409 RID: 1033
	public const string FSW_BufferOverflow = "Too many changes at once in directory:{0}.";

	// Token: 0x0400040A RID: 1034
	public const string FileSystemWatcherDesc = "Monitors file system change notifications and raises events when a directory or file changes.";

	// Token: 0x0400040B RID: 1035
	public const string NotSet = "[Not Set]";

	// Token: 0x0400040C RID: 1036
	public const string TimerAutoReset = "Indicates whether the timer will be restarted when it is enabled.";

	// Token: 0x0400040D RID: 1037
	public const string TimerEnabled = "Indicates whether the timer is enabled to fire events at a defined interval.";

	// Token: 0x0400040E RID: 1038
	public const string TimerInterval = "The number of milliseconds between timer events.";

	// Token: 0x0400040F RID: 1039
	public const string TimerIntervalElapsed = "Occurs when the Interval has elapsed.";

	// Token: 0x04000410 RID: 1040
	public const string TimerSynchronizingObject = "The object used to marshal the event handler calls issued when an interval has elapsed.";

	// Token: 0x04000411 RID: 1041
	public const string MismatchedCounterTypes = "Mismatched counter types.";

	// Token: 0x04000412 RID: 1042
	public const string NoPropertyForAttribute = "Could not find a property for the attribute '{0}'.";

	// Token: 0x04000413 RID: 1043
	public const string InvalidAttributeType = "The value of attribute '{0}' could not be converted to the proper type.";

	// Token: 0x04000414 RID: 1044
	public const string Generic_ArgCantBeEmptyString = "'{0}' can not be empty string.";

	// Token: 0x04000415 RID: 1045
	public const string BadLogName = "Event log names must consist of printable characters and cannot contain \\, *, ?, or spaces";

	// Token: 0x04000416 RID: 1046
	public const string InvalidProperty = "Invalid value {1} for property {0}.";

	// Token: 0x04000417 RID: 1047
	public const string CantMonitorEventLog = "Cannot monitor EntryWritten events for this EventLog. This might be because the EventLog is on a remote machine which is not a supported scenario.";

	// Token: 0x04000418 RID: 1048
	public const string InitTwice = "Cannot initialize the same object twice.";

	// Token: 0x04000419 RID: 1049
	public const string InvalidParameter = "Invalid value '{1}' for parameter '{0}'.";

	// Token: 0x0400041A RID: 1050
	public const string MissingParameter = "Must specify value for {0}.";

	// Token: 0x0400041B RID: 1051
	public const string ParameterTooLong = "The size of {0} is too big. It cannot be longer than {1} characters.";

	// Token: 0x0400041C RID: 1052
	public const string LocalSourceAlreadyExists = "Source {0} already exists on the local computer.";

	// Token: 0x0400041D RID: 1053
	public const string SourceAlreadyExists = "Source {0} already exists on the computer '{1}'.";

	// Token: 0x0400041E RID: 1054
	public const string LocalLogAlreadyExistsAsSource = "Log {0} has already been registered as a source on the local computer.";

	// Token: 0x0400041F RID: 1055
	public const string LogAlreadyExistsAsSource = "Log {0} has already been registered as a source on the computer '{1}'.";

	// Token: 0x04000420 RID: 1056
	public const string DuplicateLogName = "Only the first eight characters of a custom log name are significant, and there is already another log on the system using the first eight characters of the name given. Name given: '{0}', name of existing log: '{1}'.";

	// Token: 0x04000421 RID: 1057
	public const string RegKeyMissing = "Cannot open registry key {0}\\{1}\\{2} on computer '{3}'.";

	// Token: 0x04000422 RID: 1058
	public const string LocalRegKeyMissing = "Cannot open registry key {0}\\{1}\\{2}.";

	// Token: 0x04000423 RID: 1059
	public const string RegKeyMissingShort = "Cannot open registry key {0} on computer {1}.";

	// Token: 0x04000424 RID: 1060
	public const string InvalidParameterFormat = "Invalid format for argument {0}.";

	// Token: 0x04000425 RID: 1061
	public const string NoLogName = "Log to delete was not specified.";

	// Token: 0x04000426 RID: 1062
	public const string RegKeyNoAccess = "Cannot open registry key {0} on computer {1}. You might not have access.";

	// Token: 0x04000427 RID: 1063
	public const string MissingLog = "Cannot find Log {0} on computer '{1}'.";

	// Token: 0x04000428 RID: 1064
	public const string SourceNotRegistered = "The source '{0}' is not registered on machine '{1}', or you do not have write access to the {2} registry key.";

	// Token: 0x04000429 RID: 1065
	public const string LocalSourceNotRegistered = "Source {0} is not registered on the local computer.";

	// Token: 0x0400042A RID: 1066
	public const string CantRetrieveEntries = "Cannot retrieve all entries.";

	// Token: 0x0400042B RID: 1067
	public const string IndexOutOfBounds = "Index {0} is out of bounds.";

	// Token: 0x0400042C RID: 1068
	public const string CantReadLogEntryAt = "Cannot read log entry number {0}.  The event log may be corrupt.";

	// Token: 0x0400042D RID: 1069
	public const string MissingLogProperty = "Log property value has not been specified.";

	// Token: 0x0400042E RID: 1070
	public const string CantOpenLog = "Cannot open log {0} on machine {1}. Windows has not provided an error code.";

	// Token: 0x0400042F RID: 1071
	public const string NeedSourceToOpen = "Source property was not set before opening the event log in write mode.";

	// Token: 0x04000430 RID: 1072
	public const string NeedSourceToWrite = "Source property was not set before writing to the event log.";

	// Token: 0x04000431 RID: 1073
	public const string CantOpenLogAccess = "Cannot open log for source '{0}'. You may not have write access.";

	// Token: 0x04000432 RID: 1074
	public const string LogEntryTooLong = "Log entry string is too long. A string written to the event log cannot exceed 32766 characters.";

	// Token: 0x04000433 RID: 1075
	public const string TooManyReplacementStrings = "The maximum allowed number of replacement strings is 255.";

	// Token: 0x04000434 RID: 1076
	public const string LogSourceMismatch = "The source '{0}' is not registered in log '{1}'. (It is registered in log '{2}'.) \" The Source and Log properties must be matched, or you may set Log to the empty string, and it will automatically be matched to the Source property.";

	// Token: 0x04000435 RID: 1077
	public const string NoAccountInfo = "Cannot obtain account information.";

	// Token: 0x04000436 RID: 1078
	public const string NoCurrentEntry = "No current EventLog entry available, cursor is located before the first or after the last element of the enumeration.";

	// Token: 0x04000437 RID: 1079
	public const string MessageNotFormatted = "The description for Event ID '{0}' in Source '{1}' cannot be found.  The local computer may not have the necessary registry information or message DLL files to display the message, or you may not have permission to access them.  The following information is part of the event:";

	// Token: 0x04000438 RID: 1080
	public const string EventID = "Invalid eventID value '{0}'. It must be in the range between '{1}' and '{2}'.";

	// Token: 0x04000439 RID: 1081
	public const string LogDoesNotExists = "The event log '{0}' on computer '{1}' does not exist.";

	// Token: 0x0400043A RID: 1082
	public const string InvalidCustomerLogName = "The log name: '{0}' is invalid for customer log creation.";

	// Token: 0x0400043B RID: 1083
	public const string CannotDeleteEqualSource = "The event log source '{0}' cannot be deleted, because it's equal to the log name.";

	// Token: 0x0400043C RID: 1084
	public const string RentionDaysOutOfRange = "'retentionDays' must be between 1 and 365 days.";

	// Token: 0x0400043D RID: 1085
	public const string MaximumKilobytesOutOfRange = "MaximumKilobytes must be between 64 KB and 4 GB, and must be in 64K increments.";

	// Token: 0x0400043E RID: 1086
	public const string SomeLogsInaccessible = "The source was not found, but some or all event logs could not be searched.  Inaccessible logs: {0}.";

	// Token: 0x0400043F RID: 1087
	public const string SomeLogsInaccessibleToCreate = "The source was not found, but some or all event logs could not be searched.  To create the source, you need permission to read all event logs to make sure that the new source name is unique.  Inaccessible logs: {0}.";

	// Token: 0x04000440 RID: 1088
	public const string BadConfigSwitchValue = "The config value for Switch '{0}' was invalid.";

	// Token: 0x04000441 RID: 1089
	public const string ConfigSectionsUnique = "The '{0}' section can only appear once per config file.";

	// Token: 0x04000442 RID: 1090
	public const string ConfigSectionsUniquePerSection = "The '{0}' tag can only appear once per section.";

	// Token: 0x04000443 RID: 1091
	public const string SourceListenerDoesntExist = "The listener '{0}' added to source '{1}' must have a listener with the same name defined in the main Trace listeners section.";

	// Token: 0x04000444 RID: 1092
	public const string SourceSwitchDoesntExist = "The source '{0}' must have a switch with the same name defined in the Switches section.";

	// Token: 0x04000445 RID: 1093
	public const string CategoryHelpCorrupt = "Cannot load Category Help data because an invalid index '{0}' was read from the registry.";

	// Token: 0x04000446 RID: 1094
	public const string CounterNameCorrupt = "Cannot load Counter Name data because an invalid index '{0}' was read from the registry.";

	// Token: 0x04000447 RID: 1095
	public const string CounterDataCorrupt = "Cannot load Performance Counter data because an unexpected registry key value type was read from '{0}'.";

	// Token: 0x04000448 RID: 1096
	public const string ReadOnlyCounter = "Cannot update Performance Counter, this object has been initialized as ReadOnly.";

	// Token: 0x04000449 RID: 1097
	public const string ReadOnlyRemoveInstance = "Cannot remove Performance Counter Instance, this object as been initialized as ReadOnly.";

	// Token: 0x0400044A RID: 1098
	public const string NotCustomCounter = "The requested Performance Counter is not a custom counter, it has to be initialized as ReadOnly.";

	// Token: 0x0400044B RID: 1099
	public const string CategoryNameMissing = "Failed to initialize because CategoryName is missing.";

	// Token: 0x0400044C RID: 1100
	public const string CounterNameMissing = "Failed to initialize because CounterName is missing.";

	// Token: 0x0400044D RID: 1101
	public const string InstanceNameProhibited = "Counter is single instance, instance name '{0}' is not valid for this counter category.";

	// Token: 0x0400044E RID: 1102
	public const string InstanceNameRequired = "Counter is not single instance, an instance name needs to be specified.";

	// Token: 0x0400044F RID: 1103
	public const string MissingInstance = "Instance {0} does not exist in category {1}.";

	// Token: 0x04000450 RID: 1104
	public const string PerformanceCategoryExists = "Cannot create Performance Category '{0}' because it already exists.";

	// Token: 0x04000451 RID: 1105
	public const string InvalidCounterName = "Invalid empty or null string for counter name.";

	// Token: 0x04000452 RID: 1106
	public const string DuplicateCounterName = "Cannot create Performance Category with counter name {0} because the name is a duplicate.";

	// Token: 0x04000453 RID: 1107
	public const string CantChangeCategoryRegistration = "Cannot create or delete the Performance Category '{0}' because access is denied.";

	// Token: 0x04000454 RID: 1108
	public const string CantDeleteCategory = "Cannot delete Performance Category because this category is not registered or is a system category.";

	// Token: 0x04000455 RID: 1109
	public const string MissingCategory = "Category does not exist.";

	// Token: 0x04000456 RID: 1110
	public const string MissingCategoryDetail = "Category {0} does not exist.";

	// Token: 0x04000457 RID: 1111
	public const string CantReadCategory = "Cannot read Category {0}.";

	// Token: 0x04000458 RID: 1112
	public const string MissingCounter = "Counter {0} does not exist.";

	// Token: 0x04000459 RID: 1113
	public const string CategoryNameNotSet = "Category name property has not been set.";

	// Token: 0x0400045A RID: 1114
	public const string CounterExists = "Could not locate Performance Counter with specified category name '{0}', counter name '{1}'.";

	// Token: 0x0400045B RID: 1115
	public const string CantReadCategoryIndex = "Could not Read Category Index: {0}.";

	// Token: 0x0400045C RID: 1116
	public const string CantReadCounter = "Counter '{0}' does not exist in the specified Category.";

	// Token: 0x0400045D RID: 1117
	public const string CantReadInstance = "Instance '{0}' does not exist in the specified Category.";

	// Token: 0x0400045E RID: 1118
	public const string RemoteWriting = "Cannot write to a Performance Counter in a remote machine.";

	// Token: 0x0400045F RID: 1119
	public const string CounterLayout = "The Counter layout for the Category specified is invalid, a counter of the type:  AverageCount64, AverageTimer32, CounterMultiTimer, CounterMultiTimerInverse, CounterMultiTimer100Ns, CounterMultiTimer100NsInverse, RawFraction, or SampleFraction has to be immediately followed by any of the base counter types: AverageBase, CounterMultiBase, RawBase or SampleBase.";

	// Token: 0x04000460 RID: 1120
	public const string PossibleDeadlock = "The operation couldn't be completed, potential internal deadlock.";

	// Token: 0x04000461 RID: 1121
	public const string SharedMemoryGhosted = "Cannot access shared memory, AppDomain has been unloaded.";

	// Token: 0x04000462 RID: 1122
	public const string HelpNotAvailable = "Help not available.";

	// Token: 0x04000463 RID: 1123
	public const string PerfInvalidHelp = "Invalid help string. Its length must be in the range between '{0}' and '{1}'.";

	// Token: 0x04000464 RID: 1124
	public const string PerfInvalidCounterName = "Invalid counter name. Its length must be in the range between '{0}' and '{1}'. Double quotes, control characters and leading or trailing spaces are not allowed.";

	// Token: 0x04000465 RID: 1125
	public const string PerfInvalidCategoryName = "Invalid category name. Its length must be in the range between '{0}' and '{1}'. Double quotes, control characters and leading or trailing spaces are not allowed.";

	// Token: 0x04000466 RID: 1126
	public const string MustAddCounterCreationData = "Only objects of type CounterCreationData can be added to a CounterCreationDataCollection.";

	// Token: 0x04000467 RID: 1127
	public const string RemoteCounterAdmin = "Creating or Deleting Performance Counter Categories on remote machines is not supported.";

	// Token: 0x04000468 RID: 1128
	public const string NoInstanceInformation = "The {0} category doesn't provide any instance information, no accurate data can be returned.";

	// Token: 0x04000469 RID: 1129
	public const string PerfCounterPdhError = "There was an error calculating the PerformanceCounter value (0x{0}).";

	// Token: 0x0400046A RID: 1130
	public const string MultiInstanceOnly = "Category '{0}' is marked as multi-instance.  Performance counters in this category can only be created with instance names.";

	// Token: 0x0400046B RID: 1131
	public const string SingleInstanceOnly = "Category '{0}' is marked as single-instance.  Performance counters in this category can only be created without instance names.";

	// Token: 0x0400046C RID: 1132
	public const string InstanceNameTooLong = "Instance names used for writing to custom counters must be 127 characters or less.";

	// Token: 0x0400046D RID: 1133
	public const string CategoryNameTooLong = "Category names must be 1024 characters or less.";

	// Token: 0x0400046E RID: 1134
	public const string InstanceLifetimeProcessonReadOnly = "InstanceLifetime is unused by ReadOnly counters.";

	// Token: 0x0400046F RID: 1135
	public const string InstanceLifetimeProcessforSingleInstance = "Single instance categories are only valid with the Global lifetime.";

	// Token: 0x04000470 RID: 1136
	public const string InstanceAlreadyExists = "Instance '{0}' already exists with a lifetime of Process.  It cannot be recreated or reused until it has been removed or until the process using it has exited.";

	// Token: 0x04000471 RID: 1137
	public const string CantSetLifetimeAfterInitialized = "The InstanceLifetime cannot be set after the instance has been initialized.  You must use the default constructor and set the CategoryName, InstanceName, CounterName, InstanceLifetime and ReadOnly properties manually before setting the RawValue.";

	// Token: 0x04000472 RID: 1138
	public const string ProcessLifetimeNotValidInGlobal = "PerformanceCounterInstanceLifetime.Process is not valid in the global shared memory.  If your performance counter category was created with an older version of the Framework, it uses the global shared memory.  Either use PerformanceCounterInstanceLifetime.Global, or if applications running on older versions of the Framework do not need to write to your category, delete and recreate it.";

	// Token: 0x04000473 RID: 1139
	public const string CantConvertProcessToGlobal = "An instance with a lifetime of Process can only be accessed from a PerformanceCounter with the InstanceLifetime set to PerformanceCounterInstanceLifetime.Process.";

	// Token: 0x04000474 RID: 1140
	public const string CantConvertGlobalToProcess = "An instance with a lifetime of Global can only be accessed from a PerformanceCounter with the InstanceLifetime set to PerformanceCounterInstanceLifetime.Global.";

	// Token: 0x04000475 RID: 1141
	public const string PCNotSupportedUnderAppContainer = "Writeable performance counters are not allowed when running in AppContainer.";

	// Token: 0x04000476 RID: 1142
	public const string PriorityClassNotSupported = "The AboveNormal and BelowNormal priority classes are not available on this platform.";

	// Token: 0x04000477 RID: 1143
	public const string WinNTRequired = "Feature requires Windows NT.";

	// Token: 0x04000478 RID: 1144
	public const string Win2kRequired = "Feature requires Windows 2000.";

	// Token: 0x04000479 RID: 1145
	public const string NoAssociatedProcess = "No process is associated with this object.";

	// Token: 0x0400047A RID: 1146
	public const string ProcessIdRequired = "Feature requires a process identifier.";

	// Token: 0x0400047B RID: 1147
	public const string NotSupportedRemote = "Feature is not supported for remote machines.";

	// Token: 0x0400047C RID: 1148
	public const string NoProcessInfo = "Process has exited, so the requested information is not available.";

	// Token: 0x0400047D RID: 1149
	public const string WaitTillExit = "Process must exit before requested information can be determined.";

	// Token: 0x0400047E RID: 1150
	public const string NoProcessHandle = "Process was not started by this object, so requested information cannot be determined.";

	// Token: 0x0400047F RID: 1151
	public const string MissingProccess = "Process with an Id of {0} is not running.";

	// Token: 0x04000480 RID: 1152
	public const string BadMinWorkset = "Minimum working set size is invalid. It must be less than or equal to the maximum working set size.";

	// Token: 0x04000481 RID: 1153
	public const string BadMaxWorkset = "Maximum working set size is invalid. It must be greater than or equal to the minimum working set size.";

	// Token: 0x04000482 RID: 1154
	public const string WinNTRequiredForRemote = "Operating system does not support accessing processes on remote computers. This feature requires Windows NT or later.";

	// Token: 0x04000483 RID: 1155
	public const string ProcessHasExited = "Cannot process request because the process ({0}) has exited.";

	// Token: 0x04000484 RID: 1156
	public const string ProcessHasExitedNoId = "Cannot process request because the process has exited.";

	// Token: 0x04000485 RID: 1157
	public const string ThreadExited = "The request cannot be processed because the thread ({0}) has exited.";

	// Token: 0x04000486 RID: 1158
	public const string Win2000Required = "Feature requires Windows 2000 or later.";

	// Token: 0x04000487 RID: 1159
	public const string ProcessNotFound = "Thread {0} found, but no process {1} found.";

	// Token: 0x04000488 RID: 1160
	public const string CantGetProcessId = "Cannot retrieve process identifier from the process handle.";

	// Token: 0x04000489 RID: 1161
	public const string ProcessDisabled = "Process performance counter is disabled, so the requested operation cannot be performed.";

	// Token: 0x0400048A RID: 1162
	public const string WaitReasonUnavailable = "WaitReason is only available if the ThreadState is Wait.";

	// Token: 0x0400048B RID: 1163
	public const string NotSupportedRemoteThread = "Feature is not supported for threads on remote computers.";

	// Token: 0x0400048C RID: 1164
	public const string UseShellExecuteRequiresSTA = "Current thread is not in Single Thread Apartment (STA) mode. Starting a process with UseShellExecute set to True requires the current thread be in STA mode.  Ensure that your Main function has STAThreadAttribute marked.";

	// Token: 0x0400048D RID: 1165
	public const string CantRedirectStreams = "The Process object must have the UseShellExecute property set to false in order to redirect IO streams.";

	// Token: 0x0400048E RID: 1166
	public const string CantUseEnvVars = "The Process object must have the UseShellExecute property set to false in order to use environment variables.";

	// Token: 0x0400048F RID: 1167
	public const string CantStartAsUser = "The Process object must have the UseShellExecute property set to false in order to start a process as a user.";

	// Token: 0x04000490 RID: 1168
	public const string CouldntConnectToRemoteMachine = "Couldn't connect to remote machine.";

	// Token: 0x04000491 RID: 1169
	public const string CouldntGetProcessInfos = "Couldn't get process information from performance counter.";

	// Token: 0x04000492 RID: 1170
	public const string InputIdleUnkownError = "WaitForInputIdle failed.  This could be because the process does not have a graphical interface.";

	// Token: 0x04000493 RID: 1171
	public const string FileNameMissing = "Cannot start process because a file name has not been provided.";

	// Token: 0x04000494 RID: 1172
	public const string EnvironmentBlock = "The environment block provided doesn't have the correct format.";

	// Token: 0x04000495 RID: 1173
	public const string EnumProcessModuleFailed = "Unable to enumerate the process modules.";

	// Token: 0x04000496 RID: 1174
	public const string EnumProcessModuleFailedDueToWow = "A 32 bit processes cannot access modules of a 64 bit process.";

	// Token: 0x04000497 RID: 1175
	public const string PendingAsyncOperation = "An async read operation has already been started on the stream.";

	// Token: 0x04000498 RID: 1176
	public const string NoAsyncOperation = "No async read operation is in progress on the stream.";

	// Token: 0x04000499 RID: 1177
	public const string InvalidApplication = "The specified executable is not a valid application for this OS platform.";

	// Token: 0x0400049A RID: 1178
	public const string StandardOutputEncodingNotAllowed = "StandardOutputEncoding is only supported when standard output is redirected.";

	// Token: 0x0400049B RID: 1179
	public const string StandardErrorEncodingNotAllowed = "StandardErrorEncoding is only supported when standard error is redirected.";

	// Token: 0x0400049C RID: 1180
	public const string CountersOOM = "Custom counters file view is out of memory.";

	// Token: 0x0400049D RID: 1181
	public const string MappingCorrupted = "Cannot continue the current operation, the performance counters memory mapping has been corrupted.";

	// Token: 0x0400049E RID: 1182
	public const string SetSecurityDescriptorFailed = "Cannot initialize security descriptor initialized.";

	// Token: 0x0400049F RID: 1183
	public const string CantCreateFileMapping = "Cannot create file mapping.";

	// Token: 0x040004A0 RID: 1184
	public const string CantMapFileView = "Cannot map view of file.";

	// Token: 0x040004A1 RID: 1185
	public const string CantGetMappingSize = "Cannot calculate the size of the file view.";

	// Token: 0x040004A2 RID: 1186
	public const string CantGetStandardOut = "StandardOut has not been redirected or the process hasn't started yet.";

	// Token: 0x040004A3 RID: 1187
	public const string CantGetStandardIn = "StandardIn has not been redirected.";

	// Token: 0x040004A4 RID: 1188
	public const string CantGetStandardError = "StandardError has not been redirected.";

	// Token: 0x040004A5 RID: 1189
	public const string CantMixSyncAsyncOperation = "Cannot mix synchronous and asynchronous operation on process stream.";

	// Token: 0x040004A6 RID: 1190
	public const string NoFileMappingSize = "Cannot retrieve file mapping size while initializing configuration settings.";

	// Token: 0x040004A7 RID: 1191
	public const string EnvironmentBlockTooLong = "The environment block used to start a process cannot be longer than 65535 bytes.  Your environment block is {0} bytes long.  Remove some environment variables and try again.";

	// Token: 0x040004A8 RID: 1192
	public const string Arg_SecurityException = "The port name cannot start with '\\'.";

	// Token: 0x040004A9 RID: 1193
	public const string ArgumentNull_Array = "Array cannot be null.";

	// Token: 0x040004AA RID: 1194
	public const string ArgumentNull_Buffer = "Buffer cannot be null.";

	// Token: 0x040004AB RID: 1195
	public const string IO_UnknownError = "Unknown Error '{0}'.";

	// Token: 0x040004AC RID: 1196
	public const string NotSupported_UnwritableStream = "Stream does not support writing.";

	// Token: 0x040004AD RID: 1197
	public const string ObjectDisposed_WriterClosed = "Can not write to a closed TextWriter.";

	// Token: 0x040004AE RID: 1198
	public const string NotSupportedOS = "GetPortNames is not supported on Win9x platforms.";

	// Token: 0x040004AF RID: 1199
	public const string BaudRate = "The baud rate to use on this serial port.";

	// Token: 0x040004B0 RID: 1200
	public const string DataBits = "The number of data bits per transmitted/received byte.";

	// Token: 0x040004B1 RID: 1201
	public const string DiscardNull = "Whether to discard null bytes received on the port before adding to serial buffer.";

	// Token: 0x040004B2 RID: 1202
	public const string DtrEnable = "Whether to enable the Data Terminal Ready (DTR) line during communications.";

	// Token: 0x040004B3 RID: 1203
	public const string EncodingMonitoringDescription = "The encoding to use when reading and writing strings.";

	// Token: 0x040004B4 RID: 1204
	public const string Handshake = "The handshaking protocol for flow control in data exchange, which can be None.";

	// Token: 0x040004B5 RID: 1205
	public const string NewLine = "The string used by ReadLine and WriteLine to denote a new line.";

	// Token: 0x040004B6 RID: 1206
	public const string Parity = "The scheme for parity checking each received byte and marking each transmitted byte.";

	// Token: 0x040004B7 RID: 1207
	public const string ParityReplace = "Byte with which to replace bytes received with parity errors.";

	// Token: 0x040004B8 RID: 1208
	public const string PortName = "The name of the communications port to open.";

	// Token: 0x040004B9 RID: 1209
	public const string ReadBufferSize = "The size of the read buffer in bytes.  This is the maximum number of read bytes which can be buffered.";

	// Token: 0x040004BA RID: 1210
	public const string ReadTimeout = "The read timeout in Milliseconds.";

	// Token: 0x040004BB RID: 1211
	public const string ReceivedBytesThreshold = "Number of bytes required to be available before the Read event is fired.";

	// Token: 0x040004BC RID: 1212
	public const string RtsEnable = "Whether to enable the Request To Send (RTS) line during communications.";

	// Token: 0x040004BD RID: 1213
	public const string SerialPortDesc = "Represents a serial port resource.";

	// Token: 0x040004BE RID: 1214
	public const string StopBits = "The number of stop bits per transmitted/received byte.";

	// Token: 0x040004BF RID: 1215
	public const string WriteBufferSize = "The size of the write buffer in bytes.  This is the maximum number of bytes which can be queued for write.";

	// Token: 0x040004C0 RID: 1216
	public const string WriteTimeout = "The write timeout in milliseconds.";

	// Token: 0x040004C1 RID: 1217
	public const string SerialErrorReceived = "Raised each time when an error is received from the SerialPort.";

	// Token: 0x040004C2 RID: 1218
	public const string SerialPinChanged = "Raised each time when pin is changed on the SerialPort.";

	// Token: 0x040004C3 RID: 1219
	public const string SerialDataReceived = "Raised each time when data is received from the SerialPort.";

	// Token: 0x040004C4 RID: 1220
	public const string CounterType = "The type of this counter.";

	// Token: 0x040004C5 RID: 1221
	public const string CounterName = "The name of this counter.";

	// Token: 0x040004C6 RID: 1222
	public const string CounterHelp = "Help information for this counter.";

	// Token: 0x040004C7 RID: 1223
	public const string EventLogDesc = "Provides interaction with Windows event logs.";

	// Token: 0x040004C8 RID: 1224
	public const string ErrorDataReceived = "User event handler to call for async IO with StandardError stream.";

	// Token: 0x040004C9 RID: 1225
	public const string LogEntries = "The contents of the log.";

	// Token: 0x040004CA RID: 1226
	public const string LogLog = "Gets or sets the name of the log to read from and write to.";

	// Token: 0x040004CB RID: 1227
	public const string LogMachineName = "The name of the machine on which to read or write events.";

	// Token: 0x040004CC RID: 1228
	public const string LogMonitoring = "Indicates if the component monitors the event log for changes.";

	// Token: 0x040004CD RID: 1229
	public const string LogSynchronizingObject = "The object used to marshal the event handler calls issued as a result of an EventLog change.";

	// Token: 0x040004CE RID: 1230
	public const string LogSource = "The application name (source name) to use when writing to the event log.";

	// Token: 0x040004CF RID: 1231
	public const string LogEntryWritten = "Raised each time any application writes an entry to the event log.";

	// Token: 0x040004D0 RID: 1232
	public const string LogEntryMachineName = "The machine on which this event log resides.";

	// Token: 0x040004D1 RID: 1233
	public const string LogEntryData = "The binary data associated with this entry in the event log.";

	// Token: 0x040004D2 RID: 1234
	public const string LogEntryIndex = "The sequence of this entry in the event log.";

	// Token: 0x040004D3 RID: 1235
	public const string LogEntryCategory = "The category for this message.";

	// Token: 0x040004D4 RID: 1236
	public const string LogEntryCategoryNumber = "An application-specific category number assigned to this entry.";

	// Token: 0x040004D5 RID: 1237
	public const string LogEntryEventID = "The number identifying the message for this source.";

	// Token: 0x040004D6 RID: 1238
	public const string LogEntryEntryType = "The type of entry - Information, Warning, etc.";

	// Token: 0x040004D7 RID: 1239
	public const string LogEntryMessage = "The text of the message for this entry";

	// Token: 0x040004D8 RID: 1240
	public const string LogEntrySource = "The name of the application that wrote this entry.";

	// Token: 0x040004D9 RID: 1241
	public const string LogEntryReplacementStrings = "The application-supplied strings used in the message.";

	// Token: 0x040004DA RID: 1242
	public const string LogEntryResourceId = "The full number identifying the message in the event message dll.";

	// Token: 0x040004DB RID: 1243
	public const string LogEntryTimeGenerated = "The time at which the application logged this entry.";

	// Token: 0x040004DC RID: 1244
	public const string LogEntryTimeWritten = "The time at which the system logged this entry to the event log.";

	// Token: 0x040004DD RID: 1245
	public const string LogEntryUserName = "The username of the account associated with this entry by the writing application.";

	// Token: 0x040004DE RID: 1246
	public const string OutputDataReceived = "User event handler to call for async IO with StandardOutput stream.";

	// Token: 0x040004DF RID: 1247
	public const string PC_CounterHelp = "The description message for this counter.";

	// Token: 0x040004E0 RID: 1248
	public const string PC_CounterType = "The counter type indicates how to interpret the value of the counter, for example an actual count or a rate of change.";

	// Token: 0x040004E1 RID: 1249
	public const string PC_ReadOnly = "Indicates if the counter is read only.  Remote counters and counters not created using this component are read-only.";

	// Token: 0x040004E2 RID: 1250
	public const string PC_RawValue = "Directly accesses the raw value of this counter.  The counter must have been created using this component.";

	// Token: 0x040004E3 RID: 1251
	public const string ProcessAssociated = "Indicates if the process component is associated with a real process.";

	// Token: 0x040004E4 RID: 1252
	public const string ProcessDesc = "Provides access to local and remote processes, enabling starting and stopping of local processes.";

	// Token: 0x040004E5 RID: 1253
	public const string ProcessExitCode = "The value returned from the associated process when it terminated.";

	// Token: 0x040004E6 RID: 1254
	public const string ProcessTerminated = "Indicates if the associated process has been terminated.";

	// Token: 0x040004E7 RID: 1255
	public const string ProcessExitTime = "The time that the associated process exited.";

	// Token: 0x040004E8 RID: 1256
	public const string ProcessHandle = "Returns the native handle for this process.   The handle is only available if the process was started using this component.";

	// Token: 0x040004E9 RID: 1257
	public const string ProcessHandleCount = "The number of native handles associated with this process.";

	// Token: 0x040004EA RID: 1258
	public const string ProcessId = "The unique identifier for the process.";

	// Token: 0x040004EB RID: 1259
	public const string ProcessMachineName = "The name of the machine the running the process.";

	// Token: 0x040004EC RID: 1260
	public const string ProcessMainModule = "The main module for the associated process.";

	// Token: 0x040004ED RID: 1261
	public const string ProcessModules = "The modules that have been loaded by the associated process.";

	// Token: 0x040004EE RID: 1262
	public const string ProcessSynchronizingObject = "The object used to marshal the event handler calls issued as a result of a Process exit.";

	// Token: 0x040004EF RID: 1263
	public const string ProcessSessionId = "The identifier for the session of the process.";

	// Token: 0x040004F0 RID: 1264
	public const string ProcessThreads = "The threads running in the associated process.";

	// Token: 0x040004F1 RID: 1265
	public const string ProcessEnableRaisingEvents = "Whether the process component should watch for the associated process to exit, and raise the Exited event.";

	// Token: 0x040004F2 RID: 1266
	public const string ProcessExited = "If the WatchForExit property is set to true, then this event is raised when the associated process exits.";

	// Token: 0x040004F3 RID: 1267
	public const string ProcessFileName = "The name of the application, document or URL to start.";

	// Token: 0x040004F4 RID: 1268
	public const string ProcessWorkingDirectory = "The initial working directory for the process.";

	// Token: 0x040004F5 RID: 1269
	public const string ProcessBasePriority = "The base priority computed based on the priority class that all threads run relative to.";

	// Token: 0x040004F6 RID: 1270
	public const string ProcessMainWindowHandle = "The handle of the main window for the process.";

	// Token: 0x040004F7 RID: 1271
	public const string ProcessMainWindowTitle = "The caption of the main window for the process.";

	// Token: 0x040004F8 RID: 1272
	public const string ProcessMaxWorkingSet = "The maximum amount of physical memory the process has required since it was started.";

	// Token: 0x040004F9 RID: 1273
	public const string ProcessMinWorkingSet = "The minimum amount of physical memory the process has required since it was started.";

	// Token: 0x040004FA RID: 1274
	public const string ProcessNonpagedSystemMemorySize = "The number of bytes of non pageable system  memory the process is using.";

	// Token: 0x040004FB RID: 1275
	public const string ProcessPagedMemorySize = "The current amount of memory that can be paged to disk that the process is using.";

	// Token: 0x040004FC RID: 1276
	public const string ProcessPagedSystemMemorySize = "The number of bytes of pageable system memory the process is using.";

	// Token: 0x040004FD RID: 1277
	public const string ProcessPeakPagedMemorySize = "The maximum amount of memory that can be paged to disk that the process has used since it was started.";

	// Token: 0x040004FE RID: 1278
	public const string ProcessPeakWorkingSet = "The maximum amount of physical memory the process has used since it was started.";

	// Token: 0x040004FF RID: 1279
	public const string ProcessPeakVirtualMemorySize = "The maximum amount of virtual memory the process has allocated since it was started.";

	// Token: 0x04000500 RID: 1280
	public const string ProcessPriorityBoostEnabled = "Whether this process would like a priority boost when the user interacts with it.";

	// Token: 0x04000501 RID: 1281
	public const string ProcessPriorityClass = "The priority that the threads in the process run relative to.";

	// Token: 0x04000502 RID: 1282
	public const string ProcessPrivateMemorySize = "The current amount of memory that the process has allocated that cannot be shared with other processes.";

	// Token: 0x04000503 RID: 1283
	public const string ProcessPrivilegedProcessorTime = "The amount of CPU time the process spent inside the operating system core.";

	// Token: 0x04000504 RID: 1284
	public const string ProcessProcessName = "The name of the process.";

	// Token: 0x04000505 RID: 1285
	public const string ProcessProcessorAffinity = "A bit mask which represents the processors that the threads within the process are allowed to run on.";

	// Token: 0x04000506 RID: 1286
	public const string ProcessResponding = "Whether this process is currently responding.";

	// Token: 0x04000507 RID: 1287
	public const string ProcessStandardError = "Standard error stream of the process.";

	// Token: 0x04000508 RID: 1288
	public const string ProcessStandardInput = "Standard input stream of the process.";

	// Token: 0x04000509 RID: 1289
	public const string ProcessStandardOutput = "Standard output stream of the process.";

	// Token: 0x0400050A RID: 1290
	public const string ProcessStartInfo = "Specifies information used to start a process.";

	// Token: 0x0400050B RID: 1291
	public const string ProcessStartTime = "The time at which the process was started.";

	// Token: 0x0400050C RID: 1292
	public const string ProcessTotalProcessorTime = "The amount of CPU time the process has used.";

	// Token: 0x0400050D RID: 1293
	public const string ProcessUserProcessorTime = "The amount of CPU time the process spent outside the operating system core.";

	// Token: 0x0400050E RID: 1294
	public const string ProcessVirtualMemorySize = "The amount of virtual memory the process has currently allocated.";

	// Token: 0x0400050F RID: 1295
	public const string ProcessWorkingSet = "The current amount of physical memory the process is using.";

	// Token: 0x04000510 RID: 1296
	public const string ProcModModuleName = "The name of the module.";

	// Token: 0x04000511 RID: 1297
	public const string ProcModFileName = "The file name of the module.";

	// Token: 0x04000512 RID: 1298
	public const string ProcModBaseAddress = "The memory address that the module loaded at.";

	// Token: 0x04000513 RID: 1299
	public const string ProcModModuleMemorySize = "The amount of virtual memory required by the code and data in the module file.";

	// Token: 0x04000514 RID: 1300
	public const string ProcModEntryPointAddress = "The memory address of the function that runs when the module is loaded.";

	// Token: 0x04000515 RID: 1301
	public const string ProcessVerb = "The verb to apply to the document specified by the FileName property.";

	// Token: 0x04000516 RID: 1302
	public const string ProcessArguments = "Command line arguments that will be passed to the application specified by the FileName property.";

	// Token: 0x04000517 RID: 1303
	public const string ProcessErrorDialog = "Whether to show an error dialog to the user if there is an error.";

	// Token: 0x04000518 RID: 1304
	public const string ProcessWindowStyle = "How the main window should be created when the process starts.";

	// Token: 0x04000519 RID: 1305
	public const string ProcessCreateNoWindow = "Whether to start the process without creating a new window to contain it.";

	// Token: 0x0400051A RID: 1306
	public const string ProcessEnvironmentVariables = "Set of environment variables that apply to this process and child processes.";

	// Token: 0x0400051B RID: 1307
	public const string ProcessRedirectStandardInput = "Whether the process command input is read from the Process instance's StandardInput member.";

	// Token: 0x0400051C RID: 1308
	public const string ProcessRedirectStandardOutput = "Whether the process output is written to the Process instance's StandardOutput member.";

	// Token: 0x0400051D RID: 1309
	public const string ProcessRedirectStandardError = "Whether the process's error output is written to the Process instance's StandardError member.";

	// Token: 0x0400051E RID: 1310
	public const string ProcessUseShellExecute = "Whether to use the operating system shell to start the process.";

	// Token: 0x0400051F RID: 1311
	public const string ThreadBasePriority = "The current base priority of the thread.";

	// Token: 0x04000520 RID: 1312
	public const string ThreadCurrentPriority = "The current priority level of the thread.";

	// Token: 0x04000521 RID: 1313
	public const string ThreadId = "The unique identifier for the thread.";

	// Token: 0x04000522 RID: 1314
	public const string ThreadPriorityBoostEnabled = "Whether the thread would like a priority boost when the user interacts with UI associated with the thread.";

	// Token: 0x04000523 RID: 1315
	public const string ThreadPriorityLevel = "The priority level of the thread.";

	// Token: 0x04000524 RID: 1316
	public const string ThreadPrivilegedProcessorTime = "The amount of CPU time the thread spent inside the operating system core.";

	// Token: 0x04000525 RID: 1317
	public const string ThreadStartAddress = "The memory address of the function that was run when the thread started.";

	// Token: 0x04000526 RID: 1318
	public const string ThreadStartTime = "The time the thread was started.";

	// Token: 0x04000527 RID: 1319
	public const string ThreadThreadState = "The execution state of the thread.";

	// Token: 0x04000528 RID: 1320
	public const string ThreadTotalProcessorTime = "The amount of CPU time the thread has consumed since it was started.";

	// Token: 0x04000529 RID: 1321
	public const string ThreadUserProcessorTime = "The amount of CPU time the thread spent outside the operating system core.";

	// Token: 0x0400052A RID: 1322
	public const string ThreadWaitReason = "The reason the thread is waiting, if it is waiting.";

	// Token: 0x0400052B RID: 1323
	public const string VerbEditorDefault = "(Default)";

	// Token: 0x0400052C RID: 1324
	public const string AppSettingsReaderNoKey = "The key '{0}' does not exist in the appSettings configuration section.";

	// Token: 0x0400052D RID: 1325
	public const string AppSettingsReaderNoParser = "Type '{0}' does not have a Parse method.";

	// Token: 0x0400052E RID: 1326
	public const string AppSettingsReaderCantParse = "The value '{0}' was found in the appSettings configuration section for key '{1}', and this value is not a valid {2}.";

	// Token: 0x0400052F RID: 1327
	public const string AppSettingsReaderEmptyString = "(empty string)";

	// Token: 0x04000530 RID: 1328
	public const string InvalidPermissionState = "Invalid permission state.";

	// Token: 0x04000531 RID: 1329
	public const string PermissionNumberOfElements = "The number of elements on the access path must be the same as the number of tag names.";

	// Token: 0x04000532 RID: 1330
	public const string PermissionItemExists = "The item provided already exists.";

	// Token: 0x04000533 RID: 1331
	public const string PermissionItemDoesntExist = "The requested item doesn't exist.";

	// Token: 0x04000534 RID: 1332
	public const string PermissionBadParameterEnum = "Parameter must be of type enum.";

	// Token: 0x04000535 RID: 1333
	public const string PermissionInvalidLength = "Length must be greater than {0}.";

	// Token: 0x04000536 RID: 1334
	public const string PermissionTypeMismatch = "Type mismatch.";

	// Token: 0x04000537 RID: 1335
	public const string Argument_NotAPermissionElement = "'securityElement' was not a permission element.";

	// Token: 0x04000538 RID: 1336
	public const string Argument_InvalidXMLBadVersion = "Invalid Xml - can only parse elements of version one.";

	// Token: 0x04000539 RID: 1337
	public const string InvalidPermissionLevel = "Invalid permission level.";

	// Token: 0x0400053A RID: 1338
	public const string TargetNotWebBrowserPermissionLevel = "Target not WebBrowserPermissionLevel.";

	// Token: 0x0400053B RID: 1339
	public const string WebBrowserBadXml = "Bad Xml {0}";

	// Token: 0x0400053C RID: 1340
	public const string KeyedCollNeedNonNegativeNum = "Need a non negative number for capacity.";

	// Token: 0x0400053D RID: 1341
	public const string KeyedCollDuplicateKey = "Cannot add item since the item with the key already exists in the collection.";

	// Token: 0x0400053E RID: 1342
	public const string KeyedCollReferenceKeyNotFound = "The key reference with respect to which the insertion operation was to be performed was not found.";

	// Token: 0x0400053F RID: 1343
	public const string KeyedCollKeyNotFound = "Cannot find the key {0} in the collection.";

	// Token: 0x04000540 RID: 1344
	public const string KeyedCollInvalidKey = "Keys must be non-null non-empty Strings.";

	// Token: 0x04000541 RID: 1345
	public const string KeyedCollCapacityOverflow = "Capacity overflowed and went negative.  Check capacity of the collection.";

	// Token: 0x04000542 RID: 1346
	public const string OrderedDictionary_ReadOnly = "The OrderedDictionary is readonly and cannot be modified.";

	// Token: 0x04000543 RID: 1347
	public const string OrderedDictionary_SerializationMismatch = "There was an error deserializing the OrderedDictionary.  The ArrayList does not contain DictionaryEntries.";

	// Token: 0x04000544 RID: 1348
	public const string Async_ExceptionOccurred = "An exception occurred during the operation, making the result invalid.  Check InnerException for exception details.";

	// Token: 0x04000545 RID: 1349
	public const string Async_QueueingFailed = "Queuing WaitCallback failed.";

	// Token: 0x04000546 RID: 1350
	public const string Async_OperationCancelled = "Operation has been cancelled.";

	// Token: 0x04000547 RID: 1351
	public const string Async_OperationAlreadyCompleted = "This operation has already had OperationCompleted called on it and further calls are illegal.";

	// Token: 0x04000548 RID: 1352
	public const string Async_NullDelegate = "A non-null SendOrPostCallback must be supplied.";

	// Token: 0x04000549 RID: 1353
	public const string BackgroundWorker_AlreadyRunning = "BackgroundWorker is already running.";

	// Token: 0x0400054A RID: 1354
	public const string BackgroundWorker_CancellationNotSupported = "BackgroundWorker does not support cancellation.";

	// Token: 0x0400054B RID: 1355
	public const string BackgroundWorker_OperationCompleted = "Operation has already been completed.";

	// Token: 0x0400054C RID: 1356
	public const string BackgroundWorker_ProgressNotSupported = "BackgroundWorker does not support progress.";

	// Token: 0x0400054D RID: 1357
	public const string BackgroundWorker_WorkerAlreadyRunning = "This BackgroundWorker is currently busy and cannot run multiple tasks concurrently.";

	// Token: 0x0400054E RID: 1358
	public const string BackgroundWorker_WorkerDoesntReportProgress = "This BackgroundWorker states that it doesn't report progress. Modify WorkerReportsProgress to state that it does report progress.";

	// Token: 0x0400054F RID: 1359
	public const string BackgroundWorker_WorkerDoesntSupportCancellation = "This BackgroundWorker states that it doesn't support cancellation. Modify WorkerSupportsCancellation to state that it does support cancellation.";

	// Token: 0x04000550 RID: 1360
	public const string Async_ProgressChangedEventArgs_ProgressPercentage = "Percentage progress made in operation.";

	// Token: 0x04000551 RID: 1361
	public const string Async_ProgressChangedEventArgs_UserState = "User-supplied state to identify operation.";

	// Token: 0x04000552 RID: 1362
	public const string Async_AsyncEventArgs_Cancelled = "True if operation was cancelled.";

	// Token: 0x04000553 RID: 1363
	public const string Async_AsyncEventArgs_Error = "Exception that occurred during operation.  Null if no error.";

	// Token: 0x04000554 RID: 1364
	public const string Async_AsyncEventArgs_UserState = "User-supplied state to identify operation.";

	// Token: 0x04000555 RID: 1365
	public const string BackgroundWorker_CancellationPending = "Has the user attempted to cancel the operation? To be accessed from DoWork event handler.";

	// Token: 0x04000556 RID: 1366
	public const string BackgroundWorker_DoWork = "Event handler to be run on a different thread when the operation begins.";

	// Token: 0x04000557 RID: 1367
	public const string BackgroundWorker_IsBusy = "Is the worker still currently working on a background operation?";

	// Token: 0x04000558 RID: 1368
	public const string BackgroundWorker_ProgressChanged = "Raised when the worker thread indicates that some progress has been made.";

	// Token: 0x04000559 RID: 1369
	public const string BackgroundWorker_RunWorkerCompleted = "Raised when the worker has completed (either through success, failure, or cancellation).";

	// Token: 0x0400055A RID: 1370
	public const string BackgroundWorker_WorkerReportsProgress = "Whether the worker will report progress.";

	// Token: 0x0400055B RID: 1371
	public const string BackgroundWorker_WorkerSupportsCancellation = "Whether the worker supports cancellation.";

	// Token: 0x0400055C RID: 1372
	public const string BackgroundWorker_DoWorkEventArgs_Argument = "Argument passed into the worker handler from BackgroundWorker.RunWorkerAsync.";

	// Token: 0x0400055D RID: 1373
	public const string BackgroundWorker_DoWorkEventArgs_Result = "Result from the worker function.";

	// Token: 0x0400055E RID: 1374
	public const string BackgroundWorker_Desc = "Executes an operation on a separate thread.";

	// Token: 0x0400055F RID: 1375
	public const string InstanceCreationEditorDefaultText = "(New...)";

	// Token: 0x04000560 RID: 1376
	public const string PropertyTabAttributeBadPropertyTabScope = "Scope must be PropertyTabScope.Document or PropertyTabScope.Component";

	// Token: 0x04000561 RID: 1377
	public const string PropertyTabAttributeTypeLoadException = "Couldn't find type {0}";

	// Token: 0x04000562 RID: 1378
	public const string PropertyTabAttributeArrayLengthMismatch = "tabClasses must have the same number of items as tabScopes";

	// Token: 0x04000563 RID: 1379
	public const string PropertyTabAttributeParamsBothNull = "An array of tab type names or tab types must be specified";

	// Token: 0x04000564 RID: 1380
	public const string InstanceDescriptorCannotBeStatic = "Parameter cannot be static.";

	// Token: 0x04000565 RID: 1381
	public const string InstanceDescriptorMustBeStatic = "Parameter must be static.";

	// Token: 0x04000566 RID: 1382
	public const string InstanceDescriptorMustBeReadable = "Parameter must be readable.";

	// Token: 0x04000567 RID: 1383
	public const string InstanceDescriptorLengthMismatch = "Length mismatch.";

	// Token: 0x04000568 RID: 1384
	public const string ToolboxItemAttributeFailedGetType = "Failed to create ToolboxItem of type: {0}";

	// Token: 0x04000569 RID: 1385
	public const string PropertyDescriptorCollectionBadValue = "Parameter must be of type PropertyDescriptor.";

	// Token: 0x0400056A RID: 1386
	public const string PropertyDescriptorCollectionBadKey = "Parameter must be of type int or string.";

	// Token: 0x0400056B RID: 1387
	public const string AspNetHostingPermissionBadXml = "Bad Xml {0}";

	// Token: 0x0400056C RID: 1388
	public const string CorruptedGZipHeader = "The magic number in GZip header is not correct. Make sure you are passing in a GZip stream.";

	// Token: 0x0400056D RID: 1389
	public const string UnknownCompressionMode = "The compression mode specified in GZip header is unknown.";

	// Token: 0x0400056E RID: 1390
	public const string UnknownState = "Decoder is in some unknown state. This might be caused by corrupted data.";

	// Token: 0x0400056F RID: 1391
	public const string InvalidHuffmanData = "Failed to construct a huffman tree using the length array. The stream might be corrupted.";

	// Token: 0x04000570 RID: 1392
	public const string InvalidCRC = "The CRC in GZip footer does not match the CRC calculated from the decompressed data.";

	// Token: 0x04000571 RID: 1393
	public const string InvalidStreamSize = "The stream size in GZip footer does not match the real stream size.";

	// Token: 0x04000572 RID: 1394
	public const string UnknownBlockType = "Unknown block type. Stream might be corrupted.";

	// Token: 0x04000573 RID: 1395
	public const string InvalidBlockLength = "Block length does not match with its complement.";

	// Token: 0x04000574 RID: 1396
	public const string GenericInvalidData = "Found invalid data while decoding.";

	// Token: 0x04000575 RID: 1397
	public const string CannotReadFromDeflateStream = "Reading from the compression stream is not supported.";

	// Token: 0x04000576 RID: 1398
	public const string CannotWriteToDeflateStream = "Writing to the compression stream is not supported.";

	// Token: 0x04000577 RID: 1399
	public const string NotReadableStream = "The base stream is not readable.";

	// Token: 0x04000578 RID: 1400
	public const string NotWriteableStream = "The base stream is not writeable.";

	// Token: 0x04000579 RID: 1401
	public const string InvalidArgumentOffsetCount = "Offset plus count is larger than the length of target array.";

	// Token: 0x0400057A RID: 1402
	public const string InvalidBeginCall = "Only one asynchronous reader is allowed time at one time.";

	// Token: 0x0400057B RID: 1403
	public const string InvalidEndCall = "EndRead is only callable when there is one pending asynchronous reader.";

	// Token: 0x0400057C RID: 1404
	public const string StreamSizeOverflow = "The gzip stream can't contain more than 4GB data.";

	// Token: 0x0400057D RID: 1405
	public const string ZLibErrorDLLLoadError = "The underlying compression routine could not be loaded correctly.";

	// Token: 0x0400057E RID: 1406
	public const string ZLibErrorUnexpected = "The underlying compression routine returned an unexpected error code.";

	// Token: 0x0400057F RID: 1407
	public const string ZLibErrorInconsistentStream = "The stream state of the underlying compression routine is inconsistent.";

	// Token: 0x04000580 RID: 1408
	public const string ZLibErrorSteamFreedPrematurely = "The stream state of the underlying compression routine was freed prematurely.";

	// Token: 0x04000581 RID: 1409
	public const string ZLibErrorNotEnoughMemory = "The underlying compression routine could not reserve sufficient memory.";

	// Token: 0x04000582 RID: 1410
	public const string ZLibErrorIncorrectInitParameters = "The underlying compression routine received incorrect initialization parameters.";

	// Token: 0x04000583 RID: 1411
	public const string ZLibErrorVersionMismatch = "The version of the underlying compression routine does not match expected version.";

	// Token: 0x04000584 RID: 1412
	public const string InvalidOperation_HCCountOverflow = "Handle collector count overflows or underflows.";

	// Token: 0x04000585 RID: 1413
	public const string Argument_InvalidThreshold = "maximumThreshold cannot be less than initialThreshold.";

	// Token: 0x04000586 RID: 1414
	public const string Argument_SemaphoreInitialMaximum = "The initial count for the semaphore must be greater than or equal to zero and less than the maximum count.";

	// Token: 0x04000587 RID: 1415
	public const string Argument_WaitHandleNameTooLong = "The name can be no more than 260 characters in length.";

	// Token: 0x04000588 RID: 1416
	public const string WaitHandleCannotBeOpenedException_InvalidHandle = "A WaitHandle with system-wide name '{0}' cannot be created. A WaitHandle of a different type might have the same name.";

	// Token: 0x04000589 RID: 1417
	public const string ArgumentNotAPermissionElement = "Argument was not a permission Element.";

	// Token: 0x0400058A RID: 1418
	public const string ArgumentWrongType = "Argument should be of type {0}.";

	// Token: 0x0400058B RID: 1419
	public const string BadXmlVersion = "Xml version was wrong.";

	// Token: 0x0400058C RID: 1420
	public const string BinarySerializationNotSupported = "Binary serialization is current not supported by the LocalFileSettingsProvider.";

	// Token: 0x0400058D RID: 1421
	public const string BothScopeAttributes = "The setting {0} has both an ApplicationScopedSettingAttribute and a UserScopedSettingAttribute.";

	// Token: 0x0400058E RID: 1422
	public const string NoScopeAttributes = "The setting {0} does not have either an ApplicationScopedSettingAttribute or UserScopedSettingAttribute.";

	// Token: 0x0400058F RID: 1423
	public const string PositionOutOfRange = "Position cannot be less than zero.";

	// Token: 0x04000590 RID: 1424
	public const string ProviderInstantiationFailed = "Failed to instantiate provider: {0}.";

	// Token: 0x04000591 RID: 1425
	public const string ProviderTypeLoadFailed = "Failed to load provider type: {0}.";

	// Token: 0x04000592 RID: 1426
	public const string SaveAppScopedNotSupported = "Error saving {0} - The LocalFileSettingsProvider does not support saving changes to application-scoped settings.";

	// Token: 0x04000593 RID: 1427
	public const string SettingsResetFailed = "Failed to reset settings: unable to access the configuration section.";

	// Token: 0x04000594 RID: 1428
	public const string SettingsSaveFailed = "Failed to save settings: {0}";

	// Token: 0x04000595 RID: 1429
	public const string SettingsSaveFailedNoSection = "Failed to save settings: unable to access the configuration section.";

	// Token: 0x04000596 RID: 1430
	public const string StringDeserializationFailed = "Could not use String deserialization for setting: {0}.";

	// Token: 0x04000597 RID: 1431
	public const string StringSerializationFailed = "Could not use String serialization for setting: {0}.";

	// Token: 0x04000598 RID: 1432
	public const string UnknownSerializationFormat = "Unknown serialization format specified.";

	// Token: 0x04000599 RID: 1433
	public const string UnknownSeekOrigin = "Unknown SeekOrigin specified.";

	// Token: 0x0400059A RID: 1434
	public const string UnknownUserLevel = "Unknown ConfigurationUserLevel specified.";

	// Token: 0x0400059B RID: 1435
	public const string UserSettingsNotSupported = "The current configuration system does not support user-scoped settings.";

	// Token: 0x0400059C RID: 1436
	public const string XmlDeserializationFailed = "Could not use Xml deserialization for setting: {0}.";

	// Token: 0x0400059D RID: 1437
	public const string XmlSerializationFailed = "Could not use Xml serialization for setting: {0}.";

	// Token: 0x0400059E RID: 1438
	public const string MemberRelationshipService_RelationshipNotSupported = "Relationships between {0}.{1} and {2}.{3} are not supported.";

	// Token: 0x0400059F RID: 1439
	public const string MaskedTextProviderPasswordAndPromptCharError = "The PasswordChar and PromptChar values cannot be the same.";

	// Token: 0x040005A0 RID: 1440
	public const string MaskedTextProviderInvalidCharError = "The specified character value is not allowed for this property.";

	// Token: 0x040005A1 RID: 1441
	public const string MaskedTextProviderMaskNullOrEmpty = "The Mask value cannot be null or empty.";

	// Token: 0x040005A2 RID: 1442
	public const string MaskedTextProviderMaskInvalidChar = "The specified mask contains invalid characters.";

	// Token: 0x040005A3 RID: 1443
	public const string StandardOleMarshalObjectGetMarshalerFailed = "Failed to get marshaler for IID {0}.";

	// Token: 0x040005A4 RID: 1444
	public const string SoundAPIBadSoundLocation = "Could not determine a universal resource identifier for the sound location.";

	// Token: 0x040005A5 RID: 1445
	public const string SoundAPIFileDoesNotExist = "Please be sure a sound file exists at the specified location.";

	// Token: 0x040005A6 RID: 1446
	public const string SoundAPIFormatNotSupported = "Sound API only supports playing PCM wave files.";

	// Token: 0x040005A7 RID: 1447
	public const string SoundAPIInvalidWaveFile = "The file located at {0} is not a valid wave file.";

	// Token: 0x040005A8 RID: 1448
	public const string SoundAPIInvalidWaveHeader = "The wave header is corrupt.";

	// Token: 0x040005A9 RID: 1449
	public const string SoundAPILoadTimedOut = "The request to load the wave file in memory timed out.";

	// Token: 0x040005AA RID: 1450
	public const string SoundAPILoadTimeout = "The LoadTimeout property of a SoundPlayer cannot be negative.";

	// Token: 0x040005AB RID: 1451
	public const string SoundAPIReadError = "There was an error reading the file located at {0}. Please make sure that a valid wave file exists at the specified location.";

	// Token: 0x040005AC RID: 1452
	public const string WrongActionForCtor = "Constructor supports only the '{0}' action.";

	// Token: 0x040005AD RID: 1453
	public const string MustBeResetAddOrRemoveActionForCtor = "Constructor only supports either a Reset, Add, or Remove action.";

	// Token: 0x040005AE RID: 1454
	public const string ResetActionRequiresNullItem = "Reset action must be initialized with no changed items.";

	// Token: 0x040005AF RID: 1455
	public const string ResetActionRequiresIndexMinus1 = "Reset action must be initialized with index -1.";

	// Token: 0x040005B0 RID: 1456
	public const string IndexCannotBeNegative = "Index cannot be negative.";

	// Token: 0x040005B1 RID: 1457
	public const string ObservableCollectionReentrancyNotAllowed = "Cannot change ObservableCollection during a CollectionChanged event.";

	// Token: 0x040005B2 RID: 1458
	public const string mono_net_io_shutdown = "mono_net_io_shutdown";

	// Token: 0x040005B3 RID: 1459
	public const string mono_net_io_renegotiate = "mono_net_io_renegotiate";

	// Token: 0x040005B4 RID: 1460
	public const string net_ssl_io_already_shutdown = "Write operations are not allowed after the channel was shutdown.";

	// Token: 0x040005B5 RID: 1461
	public const string net_log_set_socketoption_reuseport_default_on = "net_log_set_socketoption_reuseport_default_on";

	// Token: 0x040005B6 RID: 1462
	public const string net_log_set_socketoption_reuseport_not_supported = "net_log_set_socketoption_reuseport_not_supported";

	// Token: 0x040005B7 RID: 1463
	public const string net_log_set_socketoption_reuseport = "net_log_set_socketoption_reuseport";

	// Token: 0x040005B8 RID: 1464
	public const string net_reqaborted = "The request was aborted: The request was canceled.";

	// Token: 0x040005B9 RID: 1465
	public const string BlockingCollection_Add_ConcurrentCompleteAdd = "CompleteAdding may not be used concurrently with additions to the collection.";

	// Token: 0x040005BA RID: 1466
	public const string BlockingCollection_Add_Failed = "The underlying collection didn't accept the item.";

	// Token: 0x040005BB RID: 1467
	public const string BlockingCollection_CantAddAnyWhenCompleted = "At least one of the specified collections is marked as complete with regards to additions.";

	// Token: 0x040005BC RID: 1468
	public const string BlockingCollection_CantTakeAnyWhenAllDone = "All collections are marked as complete with regards to additions.";

	// Token: 0x040005BD RID: 1469
	public const string BlockingCollection_CantTakeWhenDone = "The collection argument is empty and has been marked as complete with regards to additions.";

	// Token: 0x040005BE RID: 1470
	public const string BlockingCollection_Completed = "The collection has been marked as complete with regards to additions.";

	// Token: 0x040005BF RID: 1471
	public const string BlockingCollection_CopyTo_IncorrectType = "The array argument is of the incorrect type.";

	// Token: 0x040005C0 RID: 1472
	public const string BlockingCollection_CopyTo_MultiDim = "The array argument is multidimensional.";

	// Token: 0x040005C1 RID: 1473
	public const string BlockingCollection_CopyTo_NonNegative = "The index argument must be greater than or equal zero.";

	// Token: 0x040005C2 RID: 1474
	public const string Collection_CopyTo_TooManyElems = "The number of elements in the collection is greater than the available space from index to the end of the destination array.";

	// Token: 0x040005C3 RID: 1475
	public const string BlockingCollection_ctor_BoundedCapacityRange = "The boundedCapacity argument must be positive.";

	// Token: 0x040005C4 RID: 1476
	public const string BlockingCollection_ctor_CountMoreThanCapacity = "The collection argument contains more items than are allowed by the boundedCapacity.";

	// Token: 0x040005C5 RID: 1477
	public const string BlockingCollection_Disposed = "The collection has been disposed.";

	// Token: 0x040005C6 RID: 1478
	public const string BlockingCollection_Take_CollectionModified = "The underlying collection was modified from outside of the BlockingCollection<T>.";

	// Token: 0x040005C7 RID: 1479
	public const string BlockingCollection_TimeoutInvalid = "The specified timeout must represent a value between -1 and {0}, inclusive.";

	// Token: 0x040005C8 RID: 1480
	public const string BlockingCollection_ValidateCollectionsArray_DispElems = "The collections argument contains at least one disposed element.";

	// Token: 0x040005C9 RID: 1481
	public const string BlockingCollection_ValidateCollectionsArray_LargeSize = "The collections length is greater than the supported range for 32 bit machine.";

	// Token: 0x040005CA RID: 1482
	public const string BlockingCollection_ValidateCollectionsArray_NullElems = "The collections argument contains at least one null element.";

	// Token: 0x040005CB RID: 1483
	public const string BlockingCollection_ValidateCollectionsArray_ZeroSize = "The collections argument is a zero-length array.";

	// Token: 0x040005CC RID: 1484
	public const string Common_OperationCanceled = "The operation was canceled.";

	// Token: 0x040005CD RID: 1485
	public const string ConcurrentBag_Ctor_ArgumentNullException = "The collection argument is null.";

	// Token: 0x040005CE RID: 1486
	public const string ConcurrentBag_CopyTo_ArgumentNullException = "The array argument is null.";

	// Token: 0x040005CF RID: 1487
	public const string Collection_CopyTo_ArgumentOutOfRangeException = "The index argument must be greater than or equal zero.";

	// Token: 0x040005D0 RID: 1488
	public const string ConcurrentCollection_SyncRoot_NotSupported = "The SyncRoot property may not be used for the synchronization of concurrent collections.";

	// Token: 0x040005D1 RID: 1489
	public const string ConcurrentDictionary_ArrayIncorrectType = "The array is multidimensional, or the type parameter for the set cannot be cast automatically to the type of the destination array.";

	// Token: 0x040005D2 RID: 1490
	public const string ConcurrentDictionary_SourceContainsDuplicateKeys = "The source argument contains duplicate keys.";

	// Token: 0x040005D3 RID: 1491
	public const string ConcurrentDictionary_ConcurrencyLevelMustBePositive = "The concurrencyLevel argument must be positive.";

	// Token: 0x040005D4 RID: 1492
	public const string ConcurrentDictionary_CapacityMustNotBeNegative = "The capacity argument must be greater than or equal to zero.";

	// Token: 0x040005D5 RID: 1493
	public const string ConcurrentDictionary_IndexIsNegative = "The index argument is less than zero.";

	// Token: 0x040005D6 RID: 1494
	public const string ConcurrentDictionary_ArrayNotLargeEnough = "The index is equal to or greater than the length of the array, or the number of elements in the dictionary is greater than the available space from index to the end of the destination array.";

	// Token: 0x040005D7 RID: 1495
	public const string ConcurrentDictionary_KeyAlreadyExisted = "The key already existed in the dictionary.";

	// Token: 0x040005D8 RID: 1496
	public const string ConcurrentDictionary_ItemKeyIsNull = "TKey is a reference type and item.Key is null.";

	// Token: 0x040005D9 RID: 1497
	public const string ConcurrentDictionary_TypeOfKeyIncorrect = "The key was of an incorrect type for this dictionary.";

	// Token: 0x040005DA RID: 1498
	public const string ConcurrentDictionary_TypeOfValueIncorrect = "The value was of an incorrect type for this dictionary.";

	// Token: 0x040005DB RID: 1499
	public const string ConcurrentStack_PushPopRange_CountOutOfRange = "The count argument must be greater than or equal to zero.";

	// Token: 0x040005DC RID: 1500
	public const string ConcurrentStack_PushPopRange_InvalidCount = "The sum of the startIndex and count arguments must be less than or equal to the collection's Count.";

	// Token: 0x040005DD RID: 1501
	public const string ConcurrentStack_PushPopRange_StartOutOfRange = "The startIndex argument must be greater than or equal to zero.";

	// Token: 0x040005DE RID: 1502
	public const string Partitioner_DynamicPartitionsNotSupported = "Dynamic partitions are not supported by this partitioner.";

	// Token: 0x040005DF RID: 1503
	public const string PartitionerStatic_CanNotCallGetEnumeratorAfterSourceHasBeenDisposed = "Can not call GetEnumerator on partitions after the source enumerable is disposed";

	// Token: 0x040005E0 RID: 1504
	public const string PartitionerStatic_CurrentCalledBeforeMoveNext = "MoveNext must be called at least once before calling Current.";

	// Token: 0x040005E1 RID: 1505
	public const string ConcurrentBag_Enumerator_EnumerationNotStartedOrAlreadyFinished = "Enumeration has either not started or has already finished.";

	// Token: 0x040005E2 RID: 1506
	public const string Arg_NonZeroLowerBound = "The lower bound of target array must be zero.";

	// Token: 0x040005E3 RID: 1507
	public const string Arg_WrongType = "The value '{0}' is not of type '{1}' and cannot be used in this generic collection.";

	// Token: 0x040005E4 RID: 1508
	public const string Arg_ArrayPlusOffTooSmall = "Destination array is not long enough to copy all the items in the collection. Check array index and length.";

	// Token: 0x040005E5 RID: 1509
	public const string ArgumentOutOfRange_NeedNonNegNum = "Non-negative number required.";

	// Token: 0x040005E6 RID: 1510
	public const string ArgumentOutOfRange_SmallCapacity = "capacity was less than the current size.";

	// Token: 0x040005E7 RID: 1511
	public const string Argument_InvalidOffLen = "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.";

	// Token: 0x040005E8 RID: 1512
	public const string Argument_AddingDuplicate = "An item with the same key has already been added. Key: {0}";

	// Token: 0x040005E9 RID: 1513
	public const string InvalidOperation_EmptyQueue = "Queue empty.";

	// Token: 0x040005EA RID: 1514
	public const string InvalidOperation_EnumOpCantHappen = "Enumeration has either not started or has already finished.";

	// Token: 0x040005EB RID: 1515
	public const string InvalidOperation_EnumFailedVersion = "Collection was modified; enumeration operation may not execute.";

	// Token: 0x040005EC RID: 1516
	public const string InvalidOperation_EmptyStack = "Stack empty.";

	// Token: 0x040005ED RID: 1517
	public const string InvalidOperation_EnumNotStarted = "Enumeration has not started. Call MoveNext.";

	// Token: 0x040005EE RID: 1518
	public const string InvalidOperation_EnumEnded = "Enumeration already finished.";

	// Token: 0x040005EF RID: 1519
	public const string NotSupported_KeyCollectionSet = "Mutating a key collection derived from a dictionary is not allowed.";

	// Token: 0x040005F0 RID: 1520
	public const string NotSupported_ValueCollectionSet = "Mutating a value collection derived from a dictionary is not allowed.";

	// Token: 0x040005F1 RID: 1521
	public const string Arg_ArrayLengthsDiffer = "Array lengths must be the same.";

	// Token: 0x040005F2 RID: 1522
	public const string Arg_BitArrayTypeUnsupported = "Only supported array types for CopyTo on BitArrays are Boolean[], Int32[] and Byte[].";

	// Token: 0x040005F3 RID: 1523
	public const string Arg_HSCapacityOverflow = "HashSet capacity is too big.";

	// Token: 0x040005F4 RID: 1524
	public const string Arg_HTCapacityOverflow = "Hashtable's capacity overflowed and went negative. Check load factor, capacity and the current size of the table.";

	// Token: 0x040005F5 RID: 1525
	public const string Arg_InsufficientSpace = "Insufficient space in the target location to copy the information.";

	// Token: 0x040005F6 RID: 1526
	public const string Arg_RankMultiDimNotSupported = "Only single dimensional arrays are supported for the requested action.";

	// Token: 0x040005F7 RID: 1527
	public const string Argument_ArrayTooLarge = "The input array length must not exceed Int32.MaxValue / {0}. Otherwise BitArray.Length would exceed Int32.MaxValue.";

	// Token: 0x040005F8 RID: 1528
	public const string Argument_InvalidArrayType = "Target array type is not compatible with the type of items in the collection.";

	// Token: 0x040005F9 RID: 1529
	public const string ArgumentOutOfRange_BiggerThanCollection = "Must be less than or equal to the size of the collection.";

	// Token: 0x040005FA RID: 1530
	public const string ArgumentOutOfRange_Index = "Index was out of range. Must be non-negative and less than the size of the collection.";

	// Token: 0x040005FB RID: 1531
	public const string ExternalLinkedListNode = "The LinkedList node does not belong to current LinkedList.";

	// Token: 0x040005FC RID: 1532
	public const string LinkedListEmpty = "The LinkedList is empty.";

	// Token: 0x040005FD RID: 1533
	public const string LinkedListNodeIsAttached = "The LinkedList node already belongs to a LinkedList.";

	// Token: 0x040005FE RID: 1534
	public const string NotSupported_SortedListNestedWrite = "This operation is not supported on SortedList nested types because they require modifying the original SortedList.";

	// Token: 0x040005FF RID: 1535
	public const string SortedSet_LowerValueGreaterThanUpperValue = "Must be less than or equal to upperValue.";

	// Token: 0x04000600 RID: 1536
	public const string Serialization_InvalidOnDeser = "OnDeserialization method was called while the object was not being deserialized.";

	// Token: 0x04000601 RID: 1537
	public const string Serialization_MismatchedCount = "The serialized Count information doesn't match the number of items.";

	// Token: 0x04000602 RID: 1538
	public const string Serialization_MissingKeys = "The keys for this dictionary are missing.";

	// Token: 0x04000603 RID: 1539
	public const string Serialization_MissingValues = "The values for this dictionary are missing.";

	// Token: 0x04000604 RID: 1540
	public const string ArgumentException_BufferNotFromPool = "The buffer is not associated with this pool and may not be returned to it.";

	// Token: 0x04000605 RID: 1541
	public const string ArgumentOutOfRange_IndexCountBuffer = "Index and count must refer to a location within the buffer.";

	// Token: 0x04000606 RID: 1542
	public const string Argument_InvalidCharSequenceNoIndex = "String contains invalid Unicode code points.";

	// Token: 0x04000607 RID: 1543
	public const string net_uri_BadAuthority = "Invalid URI: The Authority/Host could not be parsed.";

	// Token: 0x04000608 RID: 1544
	public const string net_uri_BadAuthorityTerminator = "Invalid URI: The Authority/Host cannot end with a backslash character ('\\\\').";

	// Token: 0x04000609 RID: 1545
	public const string net_uri_BadFormat = "Invalid URI: The format of the URI could not be determined.";

	// Token: 0x0400060A RID: 1546
	public const string net_uri_NeedFreshParser = "The URI parser instance passed into 'uriParser' parameter is already registered with the scheme name '{0}'.";

	// Token: 0x0400060B RID: 1547
	public const string net_uri_AlreadyRegistered = "A URI scheme name '{0}' already has a registered custom parser.";

	// Token: 0x0400060C RID: 1548
	public const string net_uri_BadHostName = "Invalid URI: The hostname could not be parsed.";

	// Token: 0x0400060D RID: 1549
	public const string net_uri_BadPort = "Invalid URI: Invalid port specified.";

	// Token: 0x0400060E RID: 1550
	public const string net_uri_BadScheme = "Invalid URI: The URI scheme is not valid.";

	// Token: 0x0400060F RID: 1551
	public const string net_uri_BadString = "Invalid URI: There is an invalid sequence in the string.";

	// Token: 0x04000610 RID: 1552
	public const string net_uri_BadUserPassword = "Invalid URI: The username:password construct is badly formed.";

	// Token: 0x04000611 RID: 1553
	public const string net_uri_CannotCreateRelative = "A relative URI cannot be created because the 'uriString' parameter represents an absolute URI.";

	// Token: 0x04000612 RID: 1554
	public const string net_uri_SchemeLimit = "Invalid URI: The Uri scheme is too long.";

	// Token: 0x04000613 RID: 1555
	public const string net_uri_EmptyUri = "Invalid URI: The URI is empty.";

	// Token: 0x04000614 RID: 1556
	public const string net_uri_InvalidUriKind = "The value '{0}' passed for the UriKind parameter is invalid.";

	// Token: 0x04000615 RID: 1557
	public const string net_uri_MustRootedPath = "Invalid URI: A Dos path must be rooted, for example, 'c:\\\\'.";

	// Token: 0x04000616 RID: 1558
	public const string net_uri_NotAbsolute = "This operation is not supported for a relative URI.";

	// Token: 0x04000617 RID: 1559
	public const string net_uri_PortOutOfRange = "A derived type '{0}' has reported an invalid value for the Uri port '{1}'.";

	// Token: 0x04000618 RID: 1560
	public const string net_uri_SizeLimit = "Invalid URI: The Uri string is too long.";

	// Token: 0x04000619 RID: 1561
	public const string net_uri_UserDrivenParsing = "A derived type '{0}' is responsible for parsing this Uri instance. The base implementation must not be used.";

	// Token: 0x0400061A RID: 1562
	public const string net_uri_NotJustSerialization = "UriComponents.SerializationInfoString must not be combined with other UriComponents.";

	// Token: 0x0400061B RID: 1563
	public const string net_uri_BadUnicodeHostForIdn = "An invalid Unicode character by IDN standards was specified in the host.";

	// Token: 0x0400061C RID: 1564
	public const string Argument_ExtraNotValid = "Extra portion of URI not valid.";

	// Token: 0x0400061D RID: 1565
	public const string Argument_InvalidUriSubcomponent = "The subcomponent, {0}, of this uri is not valid.";

	// Token: 0x0400061E RID: 1566
	public const string IO_EOF_ReadBeyondEOF = "Unable to read beyond the end of the stream.";

	// Token: 0x0400061F RID: 1567
	public const string BaseStream_Invalid_Not_Open = "The BaseStream is only available when the port is open.";

	// Token: 0x04000620 RID: 1568
	public const string PortNameEmpty_String = "The PortName cannot be empty.";

	// Token: 0x04000621 RID: 1569
	public const string Port_not_open = "The port is closed.";

	// Token: 0x04000622 RID: 1570
	public const string Port_already_open = "The port is already open.";

	// Token: 0x04000623 RID: 1571
	public const string Cant_be_set_when_open = "'{0}' cannot be set while the port is open.";

	// Token: 0x04000624 RID: 1572
	public const string Max_Baud = "The maximum baud rate for the device is {0}.";

	// Token: 0x04000625 RID: 1573
	public const string In_Break_State = "The port is in the break state and cannot be written to.";

	// Token: 0x04000626 RID: 1574
	public const string Write_timed_out = "The write timed out.";

	// Token: 0x04000627 RID: 1575
	public const string CantSetRtsWithHandshaking = "RtsEnable cannot be accessed if Handshake is set to RequestToSend or RequestToSendXOnXOff.";

	// Token: 0x04000628 RID: 1576
	public const string NotSupportedEncoding = "SerialPort does not support encoding '{0}'.  The supported encodings include ASCIIEncoding, UTF8Encoding, UnicodeEncoding, UTF32Encoding, and most single or double byte code pages.  For a complete list please see the documentation.";

	// Token: 0x04000629 RID: 1577
	public const string Arg_InvalidSerialPort = "The given port name does not start with COM/com or does not resolve to a valid serial port.";

	// Token: 0x0400062A RID: 1578
	public const string Arg_InvalidSerialPortExtended = "The given port name is invalid.  It may be a valid port, but not a serial port.";

	// Token: 0x0400062B RID: 1579
	public const string ArgumentOutOfRange_Bounds_Lower_Upper = "Argument must be between {0} and {1}.";

	// Token: 0x0400062C RID: 1580
	public const string ArgumentOutOfRange_Enum = "Enum value was out of legal range.";

	// Token: 0x0400062D RID: 1581
	public const string ArgumentOutOfRange_NeedNonNegNumRequired = "Non-negative number required.";

	// Token: 0x0400062E RID: 1582
	public const string ArgumentOutOfRange_NeedPosNum = "Positive number required.";

	// Token: 0x0400062F RID: 1583
	public const string ArgumentOutOfRange_Timeout = "The timeout must be greater than or equal to -1.";

	// Token: 0x04000630 RID: 1584
	public const string ArgumentOutOfRange_WriteTimeout = "The timeout must be either a positive number or -1.";

	// Token: 0x04000631 RID: 1585
	public const string IndexOutOfRange_IORaceCondition = "Probable I/O race condition detected while copying memory.  The I/O package is not thread safe by default.  In multithreaded applications, a stream must be accessed in a thread-safe way, such as a thread-safe wrapper returned by TextReader's or TextWriter's Synchronized methods.  This also applies to classes like StreamWriter and StreamReader.";

	// Token: 0x04000632 RID: 1586
	public const string IO_BindHandleFailed = "BindHandle for ThreadPool failed on this handle.";

	// Token: 0x04000633 RID: 1587
	public const string IO_OperationAborted = "The I/O operation has been aborted because of either a thread exit or an application request.";

	// Token: 0x04000634 RID: 1588
	public const string NotSupported_UnseekableStream = "Stream does not support seeking.";

	// Token: 0x04000635 RID: 1589
	public const string ObjectDisposed_StreamClosed = "Can not access a closed Stream.";

	// Token: 0x04000636 RID: 1590
	public const string InvalidNullEmptyArgument = "Argument {0} cannot be null or zero-length.";

	// Token: 0x04000637 RID: 1591
	public const string Arg_WrongAsyncResult = "IAsyncResult object did not come from the corresponding async method on this type.";

	// Token: 0x04000638 RID: 1592
	public const string InvalidOperation_EndReadCalledMultiple = "EndRead can only be called once for each asynchronous operation.";

	// Token: 0x04000639 RID: 1593
	public const string InvalidOperation_EndWriteCalledMultiple = "EndWrite can only be called once for each asynchronous operation.";

	// Token: 0x0400063A RID: 1594
	public const string IO_PortNotFound = "The specified port does not exist.";

	// Token: 0x0400063B RID: 1595
	public const string IO_PortNotFoundFileName = "The port '{0}' does not exist.";

	// Token: 0x0400063C RID: 1596
	public const string UnauthorizedAccess_IODenied_NoPathName = "Access to the port is denied.";

	// Token: 0x0400063D RID: 1597
	public const string IO_PathTooLong = "The specified port name is too long.  The port name must be less than 260 characters.";

	// Token: 0x0400063E RID: 1598
	public const string IO_SharingViolation_NoFileName = "The process cannot access the port because it is being used by another process.";

	// Token: 0x0400063F RID: 1599
	public const string IO_SharingViolation_File = "The process cannot access the port '{0}' because it is being used by another process.";

	// Token: 0x04000640 RID: 1600
	public const string UnauthorizedAccess_IODenied_Path = "Access to the port '{0}' is denied.";

	// Token: 0x04000641 RID: 1601
	public const string net_log_listener_delegate_exception = "Sending 500 response, AuthenticationSchemeSelectorDelegate threw an exception: {0}.";

	// Token: 0x04000642 RID: 1602
	public const string net_log_listener_unsupported_authentication_scheme = "Received a request with an unsupported authentication scheme, Authorization:{0} SupportedSchemes:{1}.";

	// Token: 0x04000643 RID: 1603
	public const string net_log_listener_unmatched_authentication_scheme = "Received a request with an unmatched or no authentication scheme. AuthenticationSchemes:{0}, Authorization:{1}.";

	// Token: 0x04000644 RID: 1604
	public const string net_io_invalidasyncresult = "The IAsyncResult object was not returned from the corresponding asynchronous method on this class.";

	// Token: 0x04000645 RID: 1605
	public const string net_io_invalidendcall = "{0} can only be called once for each asynchronous operation.";

	// Token: 0x04000646 RID: 1606
	public const string net_listener_cannot_set_custom_cbt = "Custom channel bindings are not supported.";

	// Token: 0x04000647 RID: 1607
	public const string net_listener_detach_error = "Can't detach Url group from request queue. Status code: {0}.";

	// Token: 0x04000648 RID: 1608
	public const string net_listener_scheme = "Only Uri prefixes starting with 'http://' or 'https://' are supported.";

	// Token: 0x04000649 RID: 1609
	public const string net_listener_host = "Only Uri prefixes with a valid hostname are supported.";

	// Token: 0x0400064A RID: 1610
	public const string net_listener_mustcall = "Please call the {0} method before calling this method.";

	// Token: 0x0400064B RID: 1611
	public const string net_listener_slash = "Only Uri prefixes ending in '/' are allowed.";

	// Token: 0x0400064C RID: 1612
	public const string net_listener_already = "Failed to listen on prefix '{0}' because it conflicts with an existing registration on the machine.";

	// Token: 0x0400064D RID: 1613
	public const string net_log_listener_no_cbt_disabled = "No channel binding check because extended protection is disabled.";

	// Token: 0x0400064E RID: 1614
	public const string net_log_listener_no_cbt_http = "No channel binding check for requests without a secure channel.";

	// Token: 0x0400064F RID: 1615
	public const string net_log_listener_no_cbt_trustedproxy = "No channel binding check for the trusted proxy scenario.";

	// Token: 0x04000650 RID: 1616
	public const string net_log_listener_cbt = "Channel binding check enabled.";

	// Token: 0x04000651 RID: 1617
	public const string net_log_listener_no_spn_kerberos = "No explicit service name check because Kerberos authentication already validates the service name.";

	// Token: 0x04000652 RID: 1618
	public const string net_log_listener_no_spn_disabled = "No service name check because extended protection is disabled.";

	// Token: 0x04000653 RID: 1619
	public const string net_log_listener_no_spn_cbt = "No service name check because the channel binding was already checked.";

	// Token: 0x04000654 RID: 1620
	public const string net_log_listener_no_spn_whensupported = "No service name check because the client did not provide a service name and the server was configured for PolicyEnforcement.WhenSupported.";

	// Token: 0x04000655 RID: 1621
	public const string net_log_listener_no_spn_loopback = "No service name check because the authentication was from a client on the local machine.";

	// Token: 0x04000656 RID: 1622
	public const string net_log_listener_spn = "Client provided service name '{0}'.";

	// Token: 0x04000657 RID: 1623
	public const string net_log_listener_spn_passed = "Service name check succeeded.";

	// Token: 0x04000658 RID: 1624
	public const string net_log_listener_spn_failed = "Service name check failed.";

	// Token: 0x04000659 RID: 1625
	public const string net_log_listener_spn_failed_always = "Service name check failed because the client did not provide a service name and the server was configured for PolicyEnforcement.Always.";

	// Token: 0x0400065A RID: 1626
	public const string net_log_listener_spn_failed_empty = "No acceptable service names were configured!";

	// Token: 0x0400065B RID: 1627
	public const string net_log_listener_spn_failed_dump = "Dumping acceptable service names:";

	// Token: 0x0400065C RID: 1628
	public const string net_log_listener_spn_add = "Adding default service name '{0}' from prefix '{1}'.";

	// Token: 0x0400065D RID: 1629
	public const string net_log_listener_spn_not_add = "No default service name added for prefix '{0}'.";

	// Token: 0x0400065E RID: 1630
	public const string net_log_listener_spn_remove = "Removing default service name '{0}' from prefix '{1}'.";

	// Token: 0x0400065F RID: 1631
	public const string net_log_listener_spn_not_remove = "No default service name removed for prefix '{0}'.";

	// Token: 0x04000660 RID: 1632
	public const string net_listener_no_spns = "No service names could be determined from the registered prefixes. Either add prefixes from which default service names can be derived or specify an ExtendedProtectionPolicy object which contains an explicit list of service names.";

	// Token: 0x04000661 RID: 1633
	public const string net_ssp_dont_support_cbt = "The Security Service Providers don't support extended protection. Please install the latest Security Service Providers update.";

	// Token: 0x04000662 RID: 1634
	public const string net_PropertyNotImplementedException = "This property is not implemented by this class.";

	// Token: 0x04000663 RID: 1635
	public const string net_array_too_small = "The target array is too small.";

	// Token: 0x04000664 RID: 1636
	public const string net_listener_mustcompletecall = "The in-progress method {0} must be completed first.";

	// Token: 0x04000665 RID: 1637
	public const string net_listener_invalid_cbt_type = "Querying the {0} Channel Binding is not supported.";

	// Token: 0x04000666 RID: 1638
	public const string net_listener_callinprogress = "Cannot re-call {0} while a previous call is still in progress.";

	// Token: 0x04000667 RID: 1639
	public const string net_log_listener_cant_create_uri = "Can't create Uri from string '{0}://{1}{2}{3}'.";

	// Token: 0x04000668 RID: 1640
	public const string net_log_listener_cant_convert_raw_path = "Can't convert Uri path '{0}' using encoding '{1}'.";

	// Token: 0x04000669 RID: 1641
	public const string net_log_listener_cant_convert_percent_value = "Can't convert percent encoded value '{0}'.";

	// Token: 0x0400066A RID: 1642
	public const string net_log_listener_cant_convert_to_utf8 = "Can't convert string '{0}' into UTF-8 bytes: {1}";

	// Token: 0x0400066B RID: 1643
	public const string net_log_listener_cant_convert_bytes = "Can't convert bytes '{0}' into UTF-16 characters: {1}";

	// Token: 0x0400066C RID: 1644
	public const string net_invalidstatus = "The status code must be exactly three digits.";

	// Token: 0x0400066D RID: 1645
	public const string net_WebHeaderInvalidControlChars = "Specified value has invalid Control characters.";

	// Token: 0x0400066E RID: 1646
	public const string net_rspsubmitted = "This operation cannot be performed after the response has been submitted.";

	// Token: 0x0400066F RID: 1647
	public const string net_nochunkuploadonhttp10 = "Chunked encoding upload is not supported on the HTTP/1.0 protocol.";

	// Token: 0x04000670 RID: 1648
	public const string net_cookie_exists = "Cookie already exists.";

	// Token: 0x04000671 RID: 1649
	public const string net_clsmall = "The Content-Length value must be greater than or equal to zero.";

	// Token: 0x04000672 RID: 1650
	public const string net_wrongversion = "Only HTTP/1.0 and HTTP/1.1 version requests are currently supported.";

	// Token: 0x04000673 RID: 1651
	public const string net_noseek = "This stream does not support seek operations.";

	// Token: 0x04000674 RID: 1652
	public const string net_writeonlystream = "The stream does not support reading.";

	// Token: 0x04000675 RID: 1653
	public const string net_entitytoobig = "Bytes to be written to the stream exceed the Content-Length bytes size specified.";

	// Token: 0x04000676 RID: 1654
	public const string net_io_notenoughbyteswritten = "Cannot close stream until all bytes are written.";

	// Token: 0x04000677 RID: 1655
	public const string net_listener_close_urlgroup_error = "Can't close Url group. Status code: {0}.";

	// Token: 0x04000678 RID: 1656
	public const string net_WebSockets_NativeSendResponseHeaders = "An error occurred when sending the WebSocket HTTP upgrade response during the {0} operation. The HRESULT returned is '{1}'";

	// Token: 0x04000679 RID: 1657
	public const string net_WebSockets_ClientAcceptingNoProtocols = "The WebSocket client did not request any protocols, but server attempted to accept '{0}' protocol(s). ";

	// Token: 0x0400067A RID: 1658
	public const string net_WebSockets_AcceptUnsupportedProtocol = "The WebSocket client request requested '{0}' protocol(s), but server is only accepting '{1}' protocol(s).";

	// Token: 0x0400067B RID: 1659
	public const string net_WebSockets_AcceptNotAWebSocket = "The {0} operation was called on an incoming request that did not specify a '{1}: {2}' header or the {2} header not contain '{3}'. {2} specified by the client was '{4}'.";

	// Token: 0x0400067C RID: 1660
	public const string net_WebSockets_AcceptHeaderNotFound = "The {0} operation was called on an incoming WebSocket request without required '{1}' header. ";

	// Token: 0x0400067D RID: 1661
	public const string net_WebSockets_AcceptUnsupportedWebSocketVersion = "The {0} operation was called on an incoming request with WebSocket version '{1}', expected '{2}'. ";

	// Token: 0x0400067E RID: 1662
	public const string net_WebSockets_InvalidEmptySubProtocol = "Empty string is not a valid subprotocol value. Please use \\\"null\\\" to specify no value.";

	// Token: 0x0400067F RID: 1663
	public const string net_WebSockets_InvalidCharInProtocolString = "The WebSocket protocol '{0}' is invalid because it contains the invalid character '{1}'.";

	// Token: 0x04000680 RID: 1664
	public const string net_WebSockets_ReasonNotNull = "The close status description '{0}' is invalid. When using close status code '{1}' the description must be null.";

	// Token: 0x04000681 RID: 1665
	public const string net_WebSockets_InvalidCloseStatusCode = "The close status code '{0}' is reserved for system use only and cannot be specified when calling this method.";

	// Token: 0x04000682 RID: 1666
	public const string net_WebSockets_InvalidCloseStatusDescription = "The close status description '{0}' is too long. The UTF8-representation of the status description must not be longer than {1} bytes.";

	// Token: 0x04000683 RID: 1667
	public const string net_WebSockets_ArgumentOutOfRange_TooSmall = "The argument must be a value greater than {0}.";

	// Token: 0x04000684 RID: 1668
	public const string net_WebSockets_ArgumentOutOfRange_TooBig = "The value of the '{0}' parameter ({1}) must be less than or equal to {2}.";

	// Token: 0x04000685 RID: 1669
	public const string net_WebSockets_UnsupportedPlatform = "The WebSocket protocol is not supported on this platform.";

	// Token: 0x04000686 RID: 1670
	public const string net_readonlystream = "The stream does not support writing.";

	// Token: 0x04000687 RID: 1671
	public const string net_WebSockets_InvalidState_ClosedOrAborted = "The '{0}' instance cannot be used for communication because it has been transitioned into the '{1}' state.";

	// Token: 0x04000688 RID: 1672
	public const string net_WebSockets_ReceiveAsyncDisallowedAfterCloseAsync = "The WebSocket is in an invalid state for this operation. The '{0}' method has already been called before on this instance. Use '{1}' instead to keep being able to receive data but close the output channel.";

	// Token: 0x04000689 RID: 1673
	public const string net_Websockets_AlreadyOneOutstandingOperation = "There is already one outstanding '{0}' call for this WebSocket instance. ReceiveAsync and SendAsync can be called simultaneously, but at most one outstanding operation for each of them is allowed at the same time.";

	// Token: 0x0400068A RID: 1674
	public const string net_WebSockets_InvalidMessageType = "The received message type '{2}' is invalid after calling {0}. {0} should only be used if no more data is expected from the remote endpoint. Use '{1}' instead to keep being able to receive data but close the output channel.";

	// Token: 0x0400068B RID: 1675
	public const string net_WebSockets_InvalidBufferType = "The buffer type '{0}' is invalid. Valid buffer types are: '{1}', '{2}', '{3}', '{4}', '{5}'.";

	// Token: 0x0400068C RID: 1676
	public const string net_WebSockets_ArgumentOutOfRange_InternalBuffer = "The byte array must have a length of at least '{0}' bytes.  ";

	// Token: 0x0400068D RID: 1677
	public const string net_WebSockets_Argument_InvalidMessageType = "The message type '{0}' is not allowed for the '{1}' operation. Valid message types are: '{2}, {3}'. To close the WebSocket, use the '{4}' operation instead. ";

	// Token: 0x0400068E RID: 1678
	public const string net_securitypackagesupport = "The requested security package is not supported.";

	// Token: 0x0400068F RID: 1679
	public const string net_log_operation_failed_with_error = "{0} failed with error {1}.";

	// Token: 0x04000690 RID: 1680
	public const string net_MethodNotImplementedException = "This method is not implemented by this class.";

	// Token: 0x04000691 RID: 1681
	public const string event_OperationReturnedSomething = "{0} returned {1}.";

	// Token: 0x04000692 RID: 1682
	public const string net_invalid_enum = "The specified value is not valid in the '{0}' enumeration.";

	// Token: 0x04000693 RID: 1683
	public const string net_auth_message_not_encrypted = "Protocol error: A received message contains a valid signature but it was not encrypted as required by the effective Protection Level.";

	// Token: 0x04000694 RID: 1684
	public const string SSPIInvalidHandleType = "'{0}' is not a supported handle type.";

	// Token: 0x04000695 RID: 1685
	public const string net_cannot_change_after_headers = "Cannot be changed after headers are sent.";

	// Token: 0x04000696 RID: 1686
	public const string offset_out_of_range = "Offset exceeds the length of buffer.";

	// Token: 0x04000697 RID: 1687
	public const string net_io_operation_aborted = "I/O operation aborted: '{0}'.";

	// Token: 0x04000698 RID: 1688
	public const string net_invalid_path = "Invalid path.";

	// Token: 0x04000699 RID: 1689
	public const string net_no_client_certificate = "Client certificate not found.";

	// Token: 0x0400069A RID: 1690
	public const string net_listener_auth_errors = "Authentication errors.";

	// Token: 0x0400069B RID: 1691
	public const string net_listener_close = "Listener closed.";

	// Token: 0x0400069C RID: 1692
	public const string net_invalid_port = "Invalid port in prefix.";

	// Token: 0x0400069D RID: 1693
	public const string net_WebSockets_InvalidState = "The WebSocket is in an invalid state ('{0}') for this operation. Valid states are: '{1}'";
}
