using System;
using System.Globalization;

// Token: 0x02000003 RID: 3
internal static class SR
{
	// Token: 0x06000007 RID: 7 RVA: 0x00002188 File Offset: 0x00000388
	internal static string GetString(string name, params object[] args)
	{
		return global::SR.GetString(CultureInfo.InvariantCulture, name, args);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002196 File Offset: 0x00000396
	internal static string GetString(CultureInfo culture, string name, params object[] args)
	{
		return string.Format(culture, name, args);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000021A0 File Offset: 0x000003A0
	internal static string GetString(string name)
	{
		return name;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x000021A3 File Offset: 0x000003A3
	internal static string GetString(CultureInfo culture, string name)
	{
		return name;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000021A6 File Offset: 0x000003A6
	internal static string Format(string resourceFormat, params object[] args)
	{
		if (args != null)
		{
			return string.Format(CultureInfo.InvariantCulture, resourceFormat, args);
		}
		return resourceFormat;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000021B9 File Offset: 0x000003B9
	internal static string Format(string resourceFormat, object p1)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x000021C7 File Offset: 0x000003C7
	internal static string Format(string resourceFormat, object p1, object p2)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000021D6 File Offset: 0x000003D6
	internal static string Format(string resourceFormat, object p1, object p2, object p3)
	{
		return string.Format(CultureInfo.InvariantCulture, resourceFormat, p1, p2, p3);
	}

	// Token: 0x04000003 RID: 3
	public const string ArgumentOutOfRange_NeedNonNegNum = "Non negative number is required.";

	// Token: 0x04000004 RID: 4
	public const string Argument_WrongAsyncResult = "IAsyncResult object did not come from the corresponding async method on this type.";

	// Token: 0x04000005 RID: 5
	public const string Argument_InvalidOffLen = "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.";

	// Token: 0x04000006 RID: 6
	public const string Argument_NeedNonemptyPipeName = "pipeName cannot be an empty string.";

	// Token: 0x04000007 RID: 7
	public const string Argument_EmptyServerName = "serverName cannot be an empty string.  Use \".\" for current machine.";

	// Token: 0x04000008 RID: 8
	public const string Argument_NonContainerInvalidAnyFlag = "This flag may not be set on a pipe.";

	// Token: 0x04000009 RID: 9
	public const string Argument_InvalidHandle = "Invalid handle.";

	// Token: 0x0400000A RID: 10
	public const string ArgumentNull_Buffer = "Buffer cannot be null.";

	// Token: 0x0400000B RID: 11
	public const string ArgumentNull_ServerName = "serverName cannot be null. Use \".\" for current machine.";

	// Token: 0x0400000C RID: 12
	public const string ArgumentOutOfRange_AdditionalAccessLimited = "additionalAccessRights is limited to the PipeAccessRights.ChangePermissions, PipeAccessRights.TakeOwnership, and PipeAccessRights.AccessSystemSecurity flags when creating NamedPipeServerStreams.";

	// Token: 0x0400000D RID: 13
	public const string ArgumentOutOfRange_AnonymousReserved = "The pipeName \"anonymous\" is reserved.";

	// Token: 0x0400000E RID: 14
	public const string ArgumentOutOfRange_TransmissionModeByteOrMsg = "For named pipes, transmission mode can be TransmissionMode.Byte or PipeTransmissionMode.Message. For anonymous pipes, transmission mode can be TransmissionMode.Byte.";

	// Token: 0x0400000F RID: 15
	public const string ArgumentOutOfRange_DirectionModeInOrOut = "PipeDirection.In or PipeDirection.Out required.";

	// Token: 0x04000010 RID: 16
	public const string ArgumentOutOfRange_DirectionModeInOutOrInOut = "For named pipes, the pipe direction can be PipeDirection.In, PipeDirection.Out or PipeDirection.InOut. For anonymous pipes, the pipe direction can be PipeDirection.In or PipeDirection.Out.";

	// Token: 0x04000011 RID: 17
	public const string ArgumentOutOfRange_ImpersonationInvalid = "TokenImpersonationLevel.None, TokenImpersonationLevel.Anonymous, TokenImpersonationLevel.Identification, TokenImpersonationLevel.Impersonation or TokenImpersonationLevel.Delegation required.";

	// Token: 0x04000012 RID: 18
	public const string ArgumentOutOfRange_ImpersonationOptionsInvalid = "impersonationOptions contains an invalid flag.";

	// Token: 0x04000013 RID: 19
	public const string ArgumentOutOfRange_OptionsInvalid = "options contains an invalid flag.";

	// Token: 0x04000014 RID: 20
	public const string ArgumentOutOfRange_HandleInheritabilityNoneOrInheritable = "HandleInheritability.None or HandleInheritability.Inheritable required.";

	// Token: 0x04000015 RID: 21
	public const string ArgumentOutOfRange_InvalidPipeAccessRights = "Invalid PipeAccessRights flag.";

	// Token: 0x04000016 RID: 22
	public const string ArgumentOutOfRange_InvalidTimeout = "Timeout must be non-negative or equal to -1 (Timeout.Infinite)";

	// Token: 0x04000017 RID: 23
	public const string ArgumentOutOfRange_MaxNumServerInstances = "maxNumberOfServerInstances must either be a value between 1 and 254, or NamedPipeServerStream.MaxAllowedServerInstances (to obtain the maximum number allowed by system resources).";

	// Token: 0x04000018 RID: 24
	public const string ArgumentOutOfRange_NeedValidPipeAccessRights = "Need valid PipeAccessRights value.";

	// Token: 0x04000019 RID: 25
	public const string IndexOutOfRange_IORaceCondition = "Probable I/O race condition detected while copying memory. The I/O package is not thread safe by default unless stated otherwise. In multithreaded applications, access streams in a thread-safe way, such as a thread-safe wrapper returned by TextReader's or TextWriter's Synchronized methods. This also applies to classes like StreamWriter and StreamReader.";

	// Token: 0x0400001A RID: 26
	public const string InvalidOperation_EndReadCalledMultiple = "EndRead can only be called once for each asynchronous operation.";

	// Token: 0x0400001B RID: 27
	public const string InvalidOperation_EndWriteCalledMultiple = "EndWrite can only be called once for each asynchronous operation.";

	// Token: 0x0400001C RID: 28
	public const string InvalidOperation_EndWaitForConnectionCalledMultiple = "EndWaitForConnection can only be called once for each asynchronous operation.";

	// Token: 0x0400001D RID: 29
	public const string InvalidOperation_PipeNotYetConnected = "Pipe hasn't been connected yet.";

	// Token: 0x0400001E RID: 30
	public const string InvalidOperation_PipeDisconnected = "Pipe is in a disconnected state.";

	// Token: 0x0400001F RID: 31
	public const string InvalidOperation_PipeHandleNotSet = "Pipe handle has not been set.  Did your PipeStream implementation call InitializeHandle?";

	// Token: 0x04000020 RID: 32
	public const string InvalidOperation_PipeNotAsync = "Pipe is not opened in asynchronous mode.";

	// Token: 0x04000021 RID: 33
	public const string InvalidOperation_PipeReadModeNotMessage = "ReadMode is not of PipeTransmissionMode.Message.";

	// Token: 0x04000022 RID: 34
	public const string InvalidOperation_PipeMessageTypeNotSupported = "This pipe does not support message type transmission.";

	// Token: 0x04000023 RID: 35
	public const string InvalidOperation_PipeAlreadyConnected = "Already in a connected state.";

	// Token: 0x04000024 RID: 36
	public const string InvalidOperation_PipeAlreadyDisconnected = "Already in a disconnected state.";

	// Token: 0x04000025 RID: 37
	public const string InvalidOperation_PipeClosed = "Pipe is closed.";

	// Token: 0x04000026 RID: 38
	public const string IO_FileTooLongOrHandleNotSync = "IO operation will not work. Most likely the file will become too long or the handle was not opened to support synchronous IO operations.";

	// Token: 0x04000027 RID: 39
	public const string IO_EOF_ReadBeyondEOF = "Unable to read beyond the end of the stream.";

	// Token: 0x04000028 RID: 40
	public const string IO_FileNotFound = "Unable to find the specified file.";

	// Token: 0x04000029 RID: 41
	public const string IO_FileNotFound_FileName = "Could not find file '{0}'.";

	// Token: 0x0400002A RID: 42
	public const string IO_IO_AlreadyExists_Name = "Cannot create \"{0}\" because a file or directory with the same name already exists.";

	// Token: 0x0400002B RID: 43
	public const string IO_IO_BindHandleFailed = "BindHandle for ThreadPool failed on this handle.";

	// Token: 0x0400002C RID: 44
	public const string IO_IO_FileExists_Name = "The file '{0}' already exists.";

	// Token: 0x0400002D RID: 45
	public const string IO_IO_NoPermissionToDirectoryName = "<Path discovery permission to the specified directory was denied.>";

	// Token: 0x0400002E RID: 46
	public const string IO_IO_SharingViolation_File = "The process cannot access the file '{0}' because it is being used by another process.";

	// Token: 0x0400002F RID: 47
	public const string IO_IO_SharingViolation_NoFileName = "The process cannot access the file because it is being used by another process.";

	// Token: 0x04000030 RID: 48
	public const string IO_IO_PipeBroken = "Pipe is broken.";

	// Token: 0x04000031 RID: 49
	public const string IO_IO_InvalidPipeHandle = "Invalid pipe handle.";

	// Token: 0x04000032 RID: 50
	public const string IO_OperationAborted = "IO operation was aborted unexpectedly.";

	// Token: 0x04000033 RID: 51
	public const string IO_DriveNotFound_Drive = "Could not find the drive '{0}'. The drive might not be ready or might not be mapped.";

	// Token: 0x04000034 RID: 52
	public const string IO_PathNotFound_Path = "Could not find a part of the path '{0}'.";

	// Token: 0x04000035 RID: 53
	public const string IO_PathNotFound_NoPathName = "Could not find a part of the path.";

	// Token: 0x04000036 RID: 54
	public const string IO_PathTooLong = "The specified path, file name, or both are too long. The fully qualified file name must be less than 260 characters, and the directory name must be less than 248 characters.";

	// Token: 0x04000037 RID: 55
	public const string NotSupported_MemStreamNotExpandable = "Memory stream is not expandable.";

	// Token: 0x04000038 RID: 56
	public const string NotSupported_UnreadableStream = "Stream does not support reading.";

	// Token: 0x04000039 RID: 57
	public const string NotSupported_UnseekableStream = "Stream does not support seeking.";

	// Token: 0x0400003A RID: 58
	public const string NotSupported_UnwritableStream = "Stream does not support writing.";

	// Token: 0x0400003B RID: 59
	public const string NotSupported_AnonymousPipeUnidirectional = "Anonymous pipes can only be in one direction.";

	// Token: 0x0400003C RID: 60
	public const string NotSupported_AnonymousPipeMessagesNotSupported = "Anonymous pipes do not support PipeTransmissionMode.Message ReadMode.";

	// Token: 0x0400003D RID: 61
	public const string ObjectDisposed_FileClosed = "Cannot access a closed file.";

	// Token: 0x0400003E RID: 62
	public const string ObjectDisposed_PipeClosed = "Cannot access a closed pipe.";

	// Token: 0x0400003F RID: 63
	public const string ObjectDisposed_ReaderClosed = "Cannot read from a closed TextReader.";

	// Token: 0x04000040 RID: 64
	public const string ObjectDisposed_StreamClosed = "Cannot access a closed Stream.";

	// Token: 0x04000041 RID: 65
	public const string ObjectDisposed_WriterClosed = "Cannot write to a closed TextWriter.";

	// Token: 0x04000042 RID: 66
	public const string PlatformNotSupported_NamedPipeServers = "Named Pipe Servers are not supported on Windows 95/98/ME.";

	// Token: 0x04000043 RID: 67
	public const string UnauthorizedAccess_IODenied_Path = "Access to the path '{0}' is denied.";

	// Token: 0x04000044 RID: 68
	public const string UnauthorizedAccess_IODenied_NoPathName = "Access to the path is denied.";

	// Token: 0x04000045 RID: 69
	public const string TraceAsTraceSource = "Trace";

	// Token: 0x04000046 RID: 70
	public const string ArgumentOutOfRange_NeedValidLogRetention = "Need valid log retention option.";

	// Token: 0x04000047 RID: 71
	public const string ArgumentOutOfRange_NeedMaxFileSizeGEBufferSize = "Maximum file size value should be greater than or equal to bufferSize.";

	// Token: 0x04000048 RID: 72
	public const string ArgumentOutOfRange_NeedValidMaxNumFiles = "Maximum number of files value should be greater than or equal to '{0}' for this retention";

	// Token: 0x04000049 RID: 73
	public const string ArgumentOutOfRange_NeedValidId = "The ID parameter must be in the range {0} through {1}.";

	// Token: 0x0400004A RID: 74
	public const string ArgumentOutOfRange_MaxArgExceeded = "The total number of parameters must not exceed {0}.";

	// Token: 0x0400004B RID: 75
	public const string ArgumentOutOfRange_MaxStringsExceeded = "The number of String parameters must not exceed {0}.";

	// Token: 0x0400004C RID: 76
	public const string NotSupported_DownLevelVista = "This functionality is only supported in Windows Vista and above.";

	// Token: 0x0400004D RID: 77
	public const string Argument_NeedNonemptyDelimiter = "Delimiter cannot be an empty string.";

	// Token: 0x0400004E RID: 78
	public const string NotSupported_SetTextWriter = "Setting TextWriter is unsupported on this listener.";

	// Token: 0x0400004F RID: 79
	public const string Perflib_PlatformNotSupported = "Classes in System.Diagnostics.PerformanceData is only supported in Windows Vista and above.";

	// Token: 0x04000050 RID: 80
	public const string Perflib_Argument_CounterSetAlreadyRegister = "CounterSet '{0}' already registered.";

	// Token: 0x04000051 RID: 81
	public const string Perflib_Argument_InvalidCounterType = "CounterType '{0}' is not a valid CounterType.";

	// Token: 0x04000052 RID: 82
	public const string Perflib_Argument_InvalidCounterSetInstanceType = "CounterSetInstanceType '{0}' is not a valid CounterSetInstanceType.";

	// Token: 0x04000053 RID: 83
	public const string Perflib_Argument_InstanceAlreadyExists = "Instance '{0}' already exists in CounterSet '{1}'.";

	// Token: 0x04000054 RID: 84
	public const string Perflib_Argument_CounterAlreadyExists = "CounterId '{0}' already added to CounterSet '{1}'.";

	// Token: 0x04000055 RID: 85
	public const string Perflib_Argument_CounterNameAlreadyExists = "CounterName '{0}' already added to CounterSet '{1}'.";

	// Token: 0x04000056 RID: 86
	public const string Perflib_Argument_ProviderNotFound = "CounterSet provider '{0}' not found.";

	// Token: 0x04000057 RID: 87
	public const string Perflib_Argument_InvalidInstance = "Single instance type CounterSet '{0}' can only have 1 CounterSetInstance.";

	// Token: 0x04000058 RID: 88
	public const string Perflib_Argument_EmptyInstanceName = "Non-empty instanceName required.";

	// Token: 0x04000059 RID: 89
	public const string Perflib_Argument_EmptyCounterName = "Non-empty counterName required.";

	// Token: 0x0400005A RID: 90
	public const string Perflib_InsufficientMemory_InstanceCounterBlock = "Cannot allocate raw counter data for CounterSet '{0}' Instance '{1}'.";

	// Token: 0x0400005B RID: 91
	public const string Perflib_InsufficientMemory_CounterSetTemplate = "Cannot allocate memory for CounterSet '{0}' template with size '{1}'.";

	// Token: 0x0400005C RID: 92
	public const string Perflib_InvalidOperation_CounterRefValue = "Cannot locate raw counter data location for CounterSet '{0}', Counter '{1}, in Instance '{2}'.";

	// Token: 0x0400005D RID: 93
	public const string Perflib_InvalidOperation_CounterSetNotInstalled = "CounterSet '{0}' not installed yet.";

	// Token: 0x0400005E RID: 94
	public const string Perflib_InvalidOperation_InstanceNotFound = "Cannot find Instance '{0}' in CounterSet '{1}'.";

	// Token: 0x0400005F RID: 95
	public const string Perflib_InvalidOperation_AddCounterAfterInstance = "Cannot AddCounter to CounterSet '{0}' after CreateCounterSetInstance.";

	// Token: 0x04000060 RID: 96
	public const string Perflib_InvalidOperation_NoActiveProvider = "CounterSet provider '{0}' not active.";

	// Token: 0x04000061 RID: 97
	public const string Perflib_InvalidOperation_CounterSetContainsNoCounter = "CounterSet '{0}' does not include any counters.";

	// Token: 0x04000062 RID: 98
	public const string Arg_ArrayPlusOffTooSmall = "Destination array is not long enough to copy all the items in the collection. Check array index and length.";

	// Token: 0x04000063 RID: 99
	public const string Arg_HSCapacityOverflow = "HashSet capacity is too big.";

	// Token: 0x04000064 RID: 100
	public const string InvalidOperation_EnumFailedVersion = "Collection was modified; enumeration operation may not execute.";

	// Token: 0x04000065 RID: 101
	public const string InvalidOperation_EnumOpCantHappen = "Enumeration has either not started or has already finished.";

	// Token: 0x04000066 RID: 102
	public const string Serialization_MissingKeys = "The Keys for this dictionary are missing.";

	// Token: 0x04000067 RID: 103
	public const string LockRecursionException_RecursiveReadNotAllowed = "Recursive read lock acquisitions not allowed in this mode.";

	// Token: 0x04000068 RID: 104
	public const string LockRecursionException_RecursiveWriteNotAllowed = "Recursive write lock acquisitions not allowed in this mode.";

	// Token: 0x04000069 RID: 105
	public const string LockRecursionException_RecursiveUpgradeNotAllowed = "Recursive upgradeable lock acquisitions not allowed in this mode.";

	// Token: 0x0400006A RID: 106
	public const string LockRecursionException_ReadAfterWriteNotAllowed = "A read lock may not be acquired with the write lock held in this mode.";

	// Token: 0x0400006B RID: 107
	public const string LockRecursionException_WriteAfterReadNotAllowed = "Write lock may not be acquired with read lock held. This pattern is prone to deadlocks. Please ensure that read locks are released before taking a write lock. If an upgrade is necessary, use an upgrade lock in place of the read lock.";

	// Token: 0x0400006C RID: 108
	public const string LockRecursionException_UpgradeAfterReadNotAllowed = "Upgradeable lock may not be acquired with read lock held.";

	// Token: 0x0400006D RID: 109
	public const string LockRecursionException_UpgradeAfterWriteNotAllowed = "Upgradeable lock may not be acquired with write lock held in this mode. Acquiring Upgradeable lock gives the ability to read along with an option to upgrade to a writer.";

	// Token: 0x0400006E RID: 110
	public const string SynchronizationLockException_MisMatchedRead = "The read lock is being released without being held.";

	// Token: 0x0400006F RID: 111
	public const string SynchronizationLockException_MisMatchedWrite = "The write lock is being released without being held.";

	// Token: 0x04000070 RID: 112
	public const string SynchronizationLockException_MisMatchedUpgrade = "The upgradeable lock is being released without being held.";

	// Token: 0x04000071 RID: 113
	public const string SynchronizationLockException_IncorrectDispose = "The lock is being disposed while still being used. It either is being held by a thread and/or has active waiters waiting to acquire the lock.";

	// Token: 0x04000072 RID: 114
	public const string Cryptography_ArgECDHKeySizeMismatch = "The keys from both parties must be the same size to generate a secret agreement.";

	// Token: 0x04000073 RID: 115
	public const string Cryptography_ArgECDHRequiresECDHKey = "Keys used with the ECDiffieHellmanCng algorithm must have an algorithm group of ECDiffieHellman.";

	// Token: 0x04000074 RID: 116
	public const string Cryptography_ArgECDsaRequiresECDsaKey = "Keys used with the ECDsaCng algorithm must have an algorithm group of ECDsa.";

	// Token: 0x04000075 RID: 117
	public const string Cryptography_ArgExpectedECDiffieHellmanCngPublicKey = "DeriveKeyMaterial requires an ECDiffieHellmanCngPublicKey.";

	// Token: 0x04000076 RID: 118
	public const string Cryptography_ArgMustBeCngAlgorithm = "Object must be of type CngAlgorithm.";

	// Token: 0x04000077 RID: 119
	public const string Cryptography_ArgMustBeCngAlgorithmGroup = "Object must be of type CngAlgorithmGroup.";

	// Token: 0x04000078 RID: 120
	public const string Cryptography_ArgMustBeCngKeyBlobFormat = "Object must be of type CngKeyBlobFormat.";

	// Token: 0x04000079 RID: 121
	public const string Cryptography_ArgMustBeCngProvider = "Object must be of type CngProvider.";

	// Token: 0x0400007A RID: 122
	public const string Cryptography_DecryptWithNoKey = "Decrypting a value requires that a key be set on the algorithm object.";

	// Token: 0x0400007B RID: 123
	public const string Cryptography_ECXmlSerializationFormatRequired = "XML serialization of an elliptic curve key requires using an overload which specifies the XML format to be used.";

	// Token: 0x0400007C RID: 124
	public const string Cryptography_InvalidAlgorithmGroup = "The algorithm group '{0}' is invalid.";

	// Token: 0x0400007D RID: 125
	public const string Cryptography_InvalidAlgorithmName = "The algorithm name '{0}' is invalid.";

	// Token: 0x0400007E RID: 126
	public const string Cryptography_InvalidCipherMode = "The specified cipher mode is not valid for this algorithm.";

	// Token: 0x0400007F RID: 127
	public const string Cryptography_InvalidIVSize = "The specified initialization vector (IV) does not match the block size for this algorithm.";

	// Token: 0x04000080 RID: 128
	public const string Cryptography_InvalidKeyBlobFormat = "The key blob format '{0}' is invalid.";

	// Token: 0x04000081 RID: 129
	public const string Cryptography_InvalidKeySize = "The specified key is not a valid size for this algorithm.";

	// Token: 0x04000082 RID: 130
	public const string Cryptography_InvalidPadding = "Padding is invalid and cannot be removed.";

	// Token: 0x04000083 RID: 131
	public const string Cryptography_InvalidProviderName = "The provider name '{0}' is invalid.";

	// Token: 0x04000084 RID: 132
	public const string Cryptography_MissingDomainParameters = "Could not read the domain parameters from the XML string.";

	// Token: 0x04000085 RID: 133
	public const string Cryptography_MissingPublicKey = "Could not read the public key from the XML string.";

	// Token: 0x04000086 RID: 134
	public const string Cryptography_MissingIV = "The cipher mode specified requires that an initialization vector (IV) be used.";

	// Token: 0x04000087 RID: 135
	public const string Cryptography_MustTransformWholeBlock = "TransformBlock may only process bytes in block sized increments.";

	// Token: 0x04000088 RID: 136
	public const string Cryptography_NonCompliantFIPSAlgorithm = "This implementation is not part of the Windows Platform FIPS validated cryptographic algorithms.";

	// Token: 0x04000089 RID: 137
	public const string Cryptography_OpenInvalidHandle = "Cannot open an invalid handle.";

	// Token: 0x0400008A RID: 138
	public const string Cryptography_OpenEphemeralKeyHandleWithoutEphemeralFlag = "The CNG key handle being opened was detected to be ephemeral, but the EphemeralKey open option was not specified.";

	// Token: 0x0400008B RID: 139
	public const string Cryptography_PartialBlock = "The input data is not a complete block.";

	// Token: 0x0400008C RID: 140
	public const string Cryptography_PlatformNotSupported = "The specified cryptographic algorithm is not supported on this platform.";

	// Token: 0x0400008D RID: 141
	public const string Cryptography_TlsRequiresLabelAndSeed = "The TLS key derivation function requires both the label and seed properties to be set.";

	// Token: 0x0400008E RID: 142
	public const string Cryptography_TransformBeyondEndOfBuffer = "Attempt to transform beyond end of buffer.";

	// Token: 0x0400008F RID: 143
	public const string Cryptography_UnknownEllipticCurve = "Unknown elliptic curve.";

	// Token: 0x04000090 RID: 144
	public const string Cryptography_UnknownEllipticCurveAlgorithm = "Unknown elliptic curve algorithm.";

	// Token: 0x04000091 RID: 145
	public const string Cryptography_UnknownPaddingMode = "Unknown padding mode used.";

	// Token: 0x04000092 RID: 146
	public const string Cryptography_UnexpectedXmlNamespace = "The XML namespace '{0}' was unexpected, expected '{1}'.";

	// Token: 0x04000093 RID: 147
	public const string ArgumentException_RangeMinRangeMaxRangeType = "Cannot accept MinRange {0} because it is not the same type as MaxRange {1}. Verify that the MaxRange and MinRange values are of the same type and try again.";

	// Token: 0x04000094 RID: 148
	public const string ArgumentException_RangeNotIComparable = "Cannot accept MaxRange and MinRange because they are not IComparable.";

	// Token: 0x04000095 RID: 149
	public const string ArgumentException_RangeMaxRangeSmallerThanMinRange = "Cannot accept MaxRange because it is less than MinRange. Specify a MaxRange value that is greater than or equal to the MinRange value and try again.";

	// Token: 0x04000096 RID: 150
	public const string ArgumentException_CountMaxLengthSmallerThanMinLength = "MaxLength should be greater than MinLength.";

	// Token: 0x04000097 RID: 151
	public const string ArgumentException_LengthMaxLengthSmallerThanMinLength = "Cannot accept MaxLength value. Specify MaxLength value greater than the value of MinLength and try again.";

	// Token: 0x04000098 RID: 152
	public const string ArgumentException_UnregisteredParameterName = "Parameter {0} has not been added to this parser.";

	// Token: 0x04000099 RID: 153
	public const string ArgumentException_InvalidParameterName = "{0} is an invalid parameter name.";

	// Token: 0x0400009A RID: 154
	public const string ArgumentException_DuplicateName = "The name {0} is already in use.";

	// Token: 0x0400009B RID: 155
	public const string ArgumentException_DuplicatePosition = "The position {0} is already in use.";

	// Token: 0x0400009C RID: 156
	public const string ArgumentException_NoParametersFound = "The object has no parameters associated with it.";

	// Token: 0x0400009D RID: 157
	public const string ArgumentException_HelpMessageBaseNameNullOrEmpty = "Help message base name may not be null or empty.";

	// Token: 0x0400009E RID: 158
	public const string ArgumentException_HelpMessageResourceIdNullOrEmpty = "Help message resource id may not be null or empty.";

	// Token: 0x0400009F RID: 159
	public const string ArgumentException_HelpMessageNullOrEmpty = "Help message may not be null or empty.";

	// Token: 0x040000A0 RID: 160
	public const string ArgumentException_RegexPatternNullOrEmpty = "The regular expression pattern may not be null or empty.";

	// Token: 0x040000A1 RID: 161
	public const string ArgumentException_RequiredPositionalAfterOptionalPositional = "Optional positional parameter {0} cannot precede required positional parameter {1}.";

	// Token: 0x040000A2 RID: 162
	public const string ArgumentException_DuplicateParameterAttribute = "Duplicate parameter attributes with the same parameter set on parameter {0}.";

	// Token: 0x040000A3 RID: 163
	public const string ArgumentException_MissingBaseNameOrResourceId = "On parameter {0}, either both HelpMessageBaseName and HelpMessageResourceId must be set or neither can be set.";

	// Token: 0x040000A4 RID: 164
	public const string ArgumentException_DuplicateRemainingArgumets = "Can not set {0} as the remaining arguments parameter for parameter set {1} because that parameter set already has a parameter set as the remaining arguments parameter.";

	// Token: 0x040000A5 RID: 165
	public const string ArgumentException_TypeMismatchForRemainingArguments = "Parameter {0} must be an array of strings if it can have its value from the remaining arguments.";

	// Token: 0x040000A6 RID: 166
	public const string ArgumentException_ValidationParameterTypeMismatch = "Validator {0} may not be applied to a parameter of type {1}.";

	// Token: 0x040000A7 RID: 167
	public const string ArgumentException_ParserBuiltWithValueType = "The parameter toBind may not be an instance of a value type.";

	// Token: 0x040000A8 RID: 168
	public const string InvalidOperationException_GetParameterTypeMismatch = "Parameter {0} may not retrieved with type {1} since it is of type {2}.";

	// Token: 0x040000A9 RID: 169
	public const string InvalidOperationException_GetParameterValueBeforeParse = "Parse must be called before retrieving parameter values.";

	// Token: 0x040000AA RID: 170
	public const string InvalidOperationException_SetRemainingArgumentsParameterAfterParse = "AllowRemainingArguments may not be set after Parse has been called.";

	// Token: 0x040000AB RID: 171
	public const string InvalidOperationException_AddParameterAfterParse = "Parameters may not be added after Parse has been called.";

	// Token: 0x040000AC RID: 172
	public const string InvalidOperationException_BindAfterBind = "Parse may only be called once.";

	// Token: 0x040000AD RID: 173
	public const string InvalidOperationException_GetRemainingArgumentsNotAllowed = "GetRemainingArguments may not be called unless AllowRemainingArguments is set to true.";

	// Token: 0x040000AE RID: 174
	public const string InvalidOperationException_ParameterSetBeforeParse = "The SpecifiedParameterSet property may only be accessed after Parse has been called successfully.";

	// Token: 0x040000AF RID: 175
	public const string CommandLineParser_Aliases = "Aliases";

	// Token: 0x040000B0 RID: 176
	public const string CommandLineParser_ErrorMessagePrefix = "Error";

	// Token: 0x040000B1 RID: 177
	public const string CommandLineParser_HelpMessagePrefix = "Usage";

	// Token: 0x040000B2 RID: 178
	public const string ParameterBindingException_AmbiguousParameterName = "Prefix {0} resolves to multiple parameters: {1}.  Use a more specific prefix for this parameter.";

	// Token: 0x040000B3 RID: 179
	public const string ParameterBindingException_ParameterValueAlreadySpecified = "Parameter {0} already given value of {1}.";

	// Token: 0x040000B4 RID: 180
	public const string ParameterBindingException_UnknownParameteName = "Unknown parameter {0}.";

	// Token: 0x040000B5 RID: 181
	public const string ParameterBindingException_RequiredParameterMissingCommandLineValue = "Parameter {0} must be followed by a value.";

	// Token: 0x040000B6 RID: 182
	public const string ParameterBindingException_UnboundCommandLineArguments = "Unbound parameters left on command line: {0}.";

	// Token: 0x040000B7 RID: 183
	public const string ParameterBindingException_UnboundMandatoryParameter = "Values for required parameters missing: {0}.";

	// Token: 0x040000B8 RID: 184
	public const string ParameterBindingException_ResponseFileException = "Could not open response file {0}: {1}";

	// Token: 0x040000B9 RID: 185
	public const string ParameterBindingException_ValididationError = "Could not validate parameter {0}: {1}";

	// Token: 0x040000BA RID: 186
	public const string ParameterBindingException_TransformationError = "Could not convert {0} to type {1}.";

	// Token: 0x040000BB RID: 187
	public const string ParameterBindingException_AmbiguousParameterSet = "Named parameters specify an ambiguous parameter set.  Specify more parameters by name.";

	// Token: 0x040000BC RID: 188
	public const string ParameterBindingException_UnknownParameterSet = "No valid parameter set for named parameters.  Make sure all named parameters belong to the same parameter set.";

	// Token: 0x040000BD RID: 189
	public const string ParameterBindingException_NestedResponseFiles = "A response file may not contain references to other response files.";

	// Token: 0x040000BE RID: 190
	public const string ValidateMetadataException_RangeGreaterThanMaxRangeFailure = "The value {0} was greater than the maximum value {1}. Specify a value less than or equal to the maximum value and try again.";

	// Token: 0x040000BF RID: 191
	public const string ValidateMetadataException_RangeSmallerThanMinRangeFailure = "The value {0} was smaller than the minimum value {1}. Specify a value greater than or equal to the minimum value and try again.";

	// Token: 0x040000C0 RID: 192
	public const string ValidateMetadataException_PatternFailure = "The value {0} does not match the pattern {1}.";

	// Token: 0x040000C1 RID: 193
	public const string ValidateMetadataException_CountMinLengthFailure = "The number of values should be greater than or equal to {0} instead of {1}.";

	// Token: 0x040000C2 RID: 194
	public const string ValidateMetadataException_CountMaxLengthFailure = "The number of values should be less than or equal to {0} instead of {1}.";

	// Token: 0x040000C3 RID: 195
	public const string ValidateMetadataException_LengthMinLengthFailure = "The length should be greater than or equal to {0} instead of {1}.";

	// Token: 0x040000C4 RID: 196
	public const string ValidateMetadataException_LengthMaxLengthFailure = "The length should be less than or equal to {0} instead of {1}.";

	// Token: 0x040000C5 RID: 197
	public const string Argument_MapNameEmptyString = "Map name cannot be an empty string.";

	// Token: 0x040000C6 RID: 198
	public const string Argument_EmptyFile = "A positive capacity must be specified for a Memory Mapped File backed by an empty file.";

	// Token: 0x040000C7 RID: 199
	public const string Argument_NewMMFWriteAccessNotAllowed = "MemoryMappedFileAccess.Write is not permitted when creating new memory mapped files. Use MemoryMappedFileAccess.ReadWrite instead.";

	// Token: 0x040000C8 RID: 200
	public const string Argument_ReadAccessWithLargeCapacity = "When specifying MemoryMappedFileAccess.Read access, the capacity must not be larger than the file size.";

	// Token: 0x040000C9 RID: 201
	public const string Argument_NewMMFAppendModeNotAllowed = "FileMode.Append is not permitted when creating new memory mapped files. Instead, use MemoryMappedFileView to ensure write-only access within a specified region.";

	// Token: 0x040000CA RID: 202
	public const string ArgumentNull_MapName = "Map name cannot be null.";

	// Token: 0x040000CB RID: 203
	public const string ArgumentNull_FileStream = "fileStream cannot be null.";

	// Token: 0x040000CC RID: 204
	public const string ArgumentOutOfRange_CapacityLargerThanLogicalAddressSpaceNotAllowed = "The capacity cannot be greater than the size of the system's logical address space.";

	// Token: 0x040000CD RID: 205
	public const string ArgumentOutOfRange_NeedPositiveNumber = "A positive number is required.";

	// Token: 0x040000CE RID: 206
	public const string ArgumentOutOfRange_PositiveOrDefaultCapacityRequired = "The capacity must be greater than or equal to 0. 0 represents the the size of the file being mapped.";

	// Token: 0x040000CF RID: 207
	public const string ArgumentOutOfRange_PositiveOrDefaultSizeRequired = "The size must be greater than or equal to 0. If 0 is specified, the view extends from the specified offset to the end of the file mapping.";

	// Token: 0x040000D0 RID: 208
	public const string ArgumentOutOfRange_PositionLessThanCapacityRequired = "The position may not be greater or equal to the capacity of the accessor.";

	// Token: 0x040000D1 RID: 209
	public const string ArgumentOutOfRange_CapacityGEFileSizeRequired = "The capacity may not be smaller than the file size.";

	// Token: 0x040000D2 RID: 210
	public const string IO_NotEnoughMemory = "Not enough memory to map view.";

	// Token: 0x040000D3 RID: 211
	public const string InvalidOperation_CalledTwice = "Cannot call this operation twice.";

	// Token: 0x040000D4 RID: 212
	public const string InvalidOperation_CantCreateFileMapping = "Cannot create file mapping.";

	// Token: 0x040000D5 RID: 213
	public const string InvalidOperation_ViewIsNull = "The underlying MemoryMappedView object is null.";

	// Token: 0x040000D6 RID: 214
	public const string NotSupported_DelayAllocateFileBackedNotAllowed = "The MemoryMappedFileOptions.DelayAllocatePages option is not supported with memory mapped files mapping files on disk.";

	// Token: 0x040000D7 RID: 215
	public const string NotSupported_MMViewStreamsFixedLength = "MemoryMappedViewStreams are fixed length.";

	// Token: 0x040000D8 RID: 216
	public const string ObjectDisposed_ViewAccessorClosed = "Cannot access a closed accessor.";

	// Token: 0x040000D9 RID: 217
	public const string ObjectDisposed_StreamIsClosed = "Cannot access a closed Stream.";

	// Token: 0x040000DA RID: 218
	public const string NotSupported_Method = "Method not supported.";

	// Token: 0x040000DB RID: 219
	public const string NotSupported_SubclassOverride = "Method not supported. Derived class must override.";

	// Token: 0x040000DC RID: 220
	public const string Cryptography_ArgDSARequiresDSAKey = "Keys used with the DSACng algorithm must have an algorithm group of DSA.";

	// Token: 0x040000DD RID: 221
	public const string Cryptography_ArgRSAaRequiresRSAKey = "Keys used with the RSACng algorithm must have an algorithm group of RSA.";

	// Token: 0x040000DE RID: 222
	public const string Cryptography_CngKeyWrongAlgorithm = "This key is for algorithm '{0}'. Expected '{1}'.";

	// Token: 0x040000DF RID: 223
	public const string Cryptography_DSA_HashTooShort = "The supplied hash cannot be shorter in length than the DSA key's Q value.";

	// Token: 0x040000E0 RID: 224
	public const string Cryptography_HashAlgorithmNameNullOrEmpty = "The hash algorithm name cannot be null or empty.";

	// Token: 0x040000E1 RID: 225
	public const string Cryptography_InvalidDsaParameters_MissingFields = "The specified DSA parameters are not valid; P, Q, G and Y are all required.";

	// Token: 0x040000E2 RID: 226
	public const string Cryptography_InvalidDsaParameters_MismatchedPGY = "The specified DSA parameters are not valid; P, G and Y must be the same length (the key size).";

	// Token: 0x040000E3 RID: 227
	public const string Cryptography_InvalidDsaParameters_MismatchedQX = "The specified DSA parameters are not valid; Q and X (if present) must be the same length.";

	// Token: 0x040000E4 RID: 228
	public const string Cryptography_InvalidDsaParameters_MismatchedPJ = "The specified DSA parameters are not valid; J (if present) must be shorter than P.";

	// Token: 0x040000E5 RID: 229
	public const string Cryptography_InvalidDsaParameters_SeedRestriction_ShortKey = "The specified DSA parameters are not valid; Seed, if present, must be 20 bytes long for keys shorter than 1024 bits.";

	// Token: 0x040000E6 RID: 230
	public const string Cryptography_InvalidDsaParameters_QRestriction_ShortKey = "The specified DSA parameters are not valid; Q must be 20 bytes long for keys shorter than 1024 bits.";

	// Token: 0x040000E7 RID: 231
	public const string Cryptography_InvalidDsaParameters_QRestriction_LargeKey = "The specified DSA parameters are not valid; Q's length must be one of 20, 32 or 64 bytes.";

	// Token: 0x040000E8 RID: 232
	public const string Cryptography_InvalidRsaParameters = "The specified RSA parameters are not valid; both Exponent and Modulus are required fields.";

	// Token: 0x040000E9 RID: 233
	public const string Cryptography_InvalidSignatureAlgorithm = "The hash algorithm is not supported for signatures. Only MD5, SHA1, SHA256,SHA384, and SHA512 are supported at this time.";

	// Token: 0x040000EA RID: 234
	public const string Cryptography_KeyBlobParsingError = "Key Blob not in expected format.";

	// Token: 0x040000EB RID: 235
	public const string Cryptography_NotSupportedKeyAlgorithm = "Key Algorithm is not supported.";

	// Token: 0x040000EC RID: 236
	public const string Cryptography_NotValidPublicOrPrivateKey = "Key is not a valid public or private key.";

	// Token: 0x040000ED RID: 237
	public const string Cryptography_NotValidPrivateKey = "Key is not a valid private key.";

	// Token: 0x040000EE RID: 238
	public const string Cryptography_UnexpectedTransformTruncation = "CNG provider unexpectedly terminated encryption or decryption prematurely.";

	// Token: 0x040000EF RID: 239
	public const string Cryptography_UnsupportedPaddingMode = "The specified PaddingMode is not supported.";

	// Token: 0x040000F0 RID: 240
	public const string Cryptography_WeakKey = "Specified key is a known weak key for this algorithm and cannot be used.";

	// Token: 0x040000F1 RID: 241
	public const string Cryptography_CurveNotSupported = "The specified curve '{0}' or its parameters are not valid for this platform.";

	// Token: 0x040000F2 RID: 242
	public const string Cryptography_InvalidCurve = "The specified curve '{0}' is not valid for this platform.";

	// Token: 0x040000F3 RID: 243
	public const string Cryptography_InvalidCurveOid = "The specified Oid is not valid. The Oid.FriendlyName or Oid.Value property must be set.";

	// Token: 0x040000F4 RID: 244
	public const string Cryptography_InvalidCurveKeyParameters = "The specified key parameters are not valid. Q.X and Q.Y are required fields. Q.X, Q.Y must be the same length. If D is specified it must be the same length as Q.X and Q.Y for named curves or the same length as Order for explicit curves.";

	// Token: 0x040000F5 RID: 245
	public const string Cryptography_InvalidECCharacteristic2Curve = "The specified Characteristic2 curve parameters are not valid. Polynomial, A, B, G.X, G.Y, and Order are required. A, B, G.X, G.Y must be the same length, and the same length as Q.X, Q.Y and D if those are specified. Seed, Cofactor and Hash are optional. Other parameters are not allowed.";

	// Token: 0x040000F6 RID: 246
	public const string Cryptography_InvalidECPrimeCurve = "The specified prime curve parameters are not valid. Prime, A, B, G.X, G.Y and Order are required and must be the same length, and the same length as Q.X, Q.Y and D if those are specified. Seed, Cofactor and Hash are optional. Other parameters are not allowed.";

	// Token: 0x040000F7 RID: 247
	public const string Cryptography_InvalidECNamedCurve = "The specified named curve parameters are not valid. Only the Oid parameter must be set.";

	// Token: 0x040000F8 RID: 248
	public const string Cryptography_UnknownHashAlgorithm = "'{0}' is not a known hash algorithm.";

	// Token: 0x040000F9 RID: 249
	public const string ReducibleMustOverrideReduce = "reducible nodes must override Expression.Reduce()";

	// Token: 0x040000FA RID: 250
	public const string MustReduceToDifferent = "node cannot reduce to itself or null";

	// Token: 0x040000FB RID: 251
	public const string ReducedNotCompatible = "cannot assign from the reduced node type to the original node type";

	// Token: 0x040000FC RID: 252
	public const string SetterHasNoParams = "Setter must have parameters.";

	// Token: 0x040000FD RID: 253
	public const string PropertyCannotHaveRefType = "Property cannot have a managed pointer type.";

	// Token: 0x040000FE RID: 254
	public const string IndexesOfSetGetMustMatch = "Indexing parameters of getter and setter must match.";

	// Token: 0x040000FF RID: 255
	public const string AccessorsCannotHaveVarArgs = "Accessor method should not have VarArgs.";

	// Token: 0x04000100 RID: 256
	public const string AccessorsCannotHaveByRefArgs = "Accessor indexes cannot be passed ByRef.";

	// Token: 0x04000101 RID: 257
	public const string BoundsCannotBeLessThanOne = "Bounds count cannot be less than 1";

	// Token: 0x04000102 RID: 258
	public const string TypeMustNotBeByRef = "Type must not be ByRef";

	// Token: 0x04000103 RID: 259
	public const string TypeMustNotBePointer = "Type must not be a pointer type";

	// Token: 0x04000104 RID: 260
	public const string TypeDoesNotHaveConstructorForTheSignature = "Type doesn't have constructor with a given signature";

	// Token: 0x04000105 RID: 261
	public const string SetterMustBeVoid = "Setter should have void type.";

	// Token: 0x04000106 RID: 262
	public const string PropertyTypeMustMatchGetter = "Property type must match the value type of getter";

	// Token: 0x04000107 RID: 263
	public const string PropertyTypeMustMatchSetter = "Property type must match the value type of setter";

	// Token: 0x04000108 RID: 264
	public const string BothAccessorsMustBeStatic = "Both accessors must be static.";

	// Token: 0x04000109 RID: 265
	public const string OnlyStaticFieldsHaveNullInstance = "Static field requires null instance, non-static field requires non-null instance.";

	// Token: 0x0400010A RID: 266
	public const string OnlyStaticPropertiesHaveNullInstance = "Static property requires null instance, non-static property requires non-null instance.";

	// Token: 0x0400010B RID: 267
	public const string OnlyStaticMethodsHaveNullInstance = "Static method requires null instance, non-static method requires non-null instance.";

	// Token: 0x0400010C RID: 268
	public const string PropertyTypeCannotBeVoid = "Property cannot have a void type.";

	// Token: 0x0400010D RID: 269
	public const string InvalidUnboxType = "Can only unbox from an object or interface type to a value type.";

	// Token: 0x0400010E RID: 270
	public const string ExpressionMustBeWriteable = "Expression must be writeable";

	// Token: 0x0400010F RID: 271
	public const string ArgumentMustNotHaveValueType = "Argument must not have a value type.";

	// Token: 0x04000110 RID: 272
	public const string MustBeReducible = "must be reducible node";

	// Token: 0x04000111 RID: 273
	public const string AllTestValuesMustHaveSameType = "All test values must have the same type.";

	// Token: 0x04000112 RID: 274
	public const string AllCaseBodiesMustHaveSameType = "All case bodies and the default body must have the same type.";

	// Token: 0x04000113 RID: 275
	public const string DefaultBodyMustBeSupplied = "Default body must be supplied if case bodies are not System.Void.";

	// Token: 0x04000114 RID: 276
	public const string LabelMustBeVoidOrHaveExpression = "Label type must be System.Void if an expression is not supplied";

	// Token: 0x04000115 RID: 277
	public const string LabelTypeMustBeVoid = "Type must be System.Void for this label argument";

	// Token: 0x04000116 RID: 278
	public const string QuotedExpressionMustBeLambda = "Quoted expression must be a lambda";

	// Token: 0x04000117 RID: 279
	public const string VariableMustNotBeByRef = "Variable '{0}' uses unsupported type '{1}'. Reference types are not supported for variables.";

	// Token: 0x04000118 RID: 280
	public const string DuplicateVariable = "Found duplicate parameter '{0}'. Each ParameterExpression in the list must be a unique object.";

	// Token: 0x04000119 RID: 281
	public const string StartEndMustBeOrdered = "Start and End must be well ordered";

	// Token: 0x0400011A RID: 282
	public const string FaultCannotHaveCatchOrFinally = "fault cannot be used with catch or finally clauses";

	// Token: 0x0400011B RID: 283
	public const string TryMustHaveCatchFinallyOrFault = "try must have at least one catch, finally, or fault clause";

	// Token: 0x0400011C RID: 284
	public const string BodyOfCatchMustHaveSameTypeAsBodyOfTry = "Body of catch must have the same type as body of try.";

	// Token: 0x0400011D RID: 285
	public const string ExtensionNodeMustOverrideProperty = "Extension node must override the property {0}.";

	// Token: 0x0400011E RID: 286
	public const string UserDefinedOperatorMustBeStatic = "User-defined operator method '{0}' must be static.";

	// Token: 0x0400011F RID: 287
	public const string UserDefinedOperatorMustNotBeVoid = "User-defined operator method '{0}' must not be void.";

	// Token: 0x04000120 RID: 288
	public const string CoercionOperatorNotDefined = "No coercion operator is defined between types '{0}' and '{1}'.";

	// Token: 0x04000121 RID: 289
	public const string UnaryOperatorNotDefined = "The unary operator {0} is not defined for the type '{1}'.";

	// Token: 0x04000122 RID: 290
	public const string BinaryOperatorNotDefined = "The binary operator {0} is not defined for the types '{1}' and '{2}'.";

	// Token: 0x04000123 RID: 291
	public const string ReferenceEqualityNotDefined = "Reference equality is not defined for the types '{0}' and '{1}'.";

	// Token: 0x04000124 RID: 292
	public const string OperandTypesDoNotMatchParameters = "The operands for operator '{0}' do not match the parameters of method '{1}'.";

	// Token: 0x04000125 RID: 293
	public const string OverloadOperatorTypeDoesNotMatchConversionType = "The return type of overload method for operator '{0}' does not match the parameter type of conversion method '{1}'.";

	// Token: 0x04000126 RID: 294
	public const string ConversionIsNotSupportedForArithmeticTypes = "Conversion is not supported for arithmetic types without operator overloading.";

	// Token: 0x04000127 RID: 295
	public const string ArgumentMustBeArray = "Argument must be array";

	// Token: 0x04000128 RID: 296
	public const string ArgumentMustBeBoolean = "Argument must be boolean";

	// Token: 0x04000129 RID: 297
	public const string EqualityMustReturnBoolean = "The user-defined equality method '{0}' must return a boolean value.";

	// Token: 0x0400012A RID: 298
	public const string ArgumentMustBeFieldInfoOrPropertyInfo = "Argument must be either a FieldInfo or PropertyInfo";

	// Token: 0x0400012B RID: 299
	public const string ArgumentMustBeFieldInfoOrPropertyInfoOrMethod = "Argument must be either a FieldInfo, PropertyInfo or MethodInfo";

	// Token: 0x0400012C RID: 300
	public const string ArgumentMustBeInstanceMember = "Argument must be an instance member";

	// Token: 0x0400012D RID: 301
	public const string ArgumentMustBeInteger = "Argument must be of an integer type";

	// Token: 0x0400012E RID: 302
	public const string ArgumentMustBeArrayIndexType = "Argument for array index must be of type Int32";

	// Token: 0x0400012F RID: 303
	public const string ArgumentMustBeSingleDimensionalArrayType = "Argument must be single-dimensional, zero-based array type";

	// Token: 0x04000130 RID: 304
	public const string ArgumentTypesMustMatch = "Argument types do not match";

	// Token: 0x04000131 RID: 305
	public const string CannotAutoInitializeValueTypeElementThroughProperty = "Cannot auto initialize elements of value type through property '{0}', use assignment instead";

	// Token: 0x04000132 RID: 306
	public const string CannotAutoInitializeValueTypeMemberThroughProperty = "Cannot auto initialize members of value type through property '{0}', use assignment instead";

	// Token: 0x04000133 RID: 307
	public const string IncorrectTypeForTypeAs = "The type used in TypeAs Expression must be of reference or nullable type, {0} is neither";

	// Token: 0x04000134 RID: 308
	public const string CoalesceUsedOnNonNullType = "Coalesce used with type that cannot be null";

	// Token: 0x04000135 RID: 309
	public const string ExpressionTypeCannotInitializeArrayType = "An expression of type '{0}' cannot be used to initialize an array of type '{1}'";

	// Token: 0x04000136 RID: 310
	public const string ArgumentTypeDoesNotMatchMember = " Argument type '{0}' does not match the corresponding member type '{1}'";

	// Token: 0x04000137 RID: 311
	public const string ArgumentMemberNotDeclOnType = " The member '{0}' is not declared on type '{1}' being created";

	// Token: 0x04000138 RID: 312
	public const string ExpressionTypeDoesNotMatchReturn = "Expression of type '{0}' cannot be used for return type '{1}'";

	// Token: 0x04000139 RID: 313
	public const string ExpressionTypeDoesNotMatchAssignment = "Expression of type '{0}' cannot be used for assignment to type '{1}'";

	// Token: 0x0400013A RID: 314
	public const string ExpressionTypeDoesNotMatchLabel = "Expression of type '{0}' cannot be used for label of type '{1}'";

	// Token: 0x0400013B RID: 315
	public const string ExpressionTypeNotInvocable = "Expression of type '{0}' cannot be invoked";

	// Token: 0x0400013C RID: 316
	public const string FieldNotDefinedForType = "Field '{0}' is not defined for type '{1}'";

	// Token: 0x0400013D RID: 317
	public const string InstanceFieldNotDefinedForType = "Instance field '{0}' is not defined for type '{1}'";

	// Token: 0x0400013E RID: 318
	public const string FieldInfoNotDefinedForType = "Field '{0}.{1}' is not defined for type '{2}'";

	// Token: 0x0400013F RID: 319
	public const string IncorrectNumberOfIndexes = "Incorrect number of indexes";

	// Token: 0x04000140 RID: 320
	public const string IncorrectNumberOfLambdaDeclarationParameters = "Incorrect number of parameters supplied for lambda declaration";

	// Token: 0x04000141 RID: 321
	public const string IncorrectNumberOfMembersForGivenConstructor = " Incorrect number of members for constructor";

	// Token: 0x04000142 RID: 322
	public const string IncorrectNumberOfArgumentsForMembers = "Incorrect number of arguments for the given members ";

	// Token: 0x04000143 RID: 323
	public const string LambdaTypeMustBeDerivedFromSystemDelegate = "Lambda type parameter must be derived from System.MulticastDelegate";

	// Token: 0x04000144 RID: 324
	public const string MemberNotFieldOrProperty = "Member '{0}' not field or property";

	// Token: 0x04000145 RID: 325
	public const string MethodContainsGenericParameters = "Method {0} contains generic parameters";

	// Token: 0x04000146 RID: 326
	public const string MethodIsGeneric = "Method {0} is a generic method definition";

	// Token: 0x04000147 RID: 327
	public const string MethodNotPropertyAccessor = "The method '{0}.{1}' is not a property accessor";

	// Token: 0x04000148 RID: 328
	public const string PropertyDoesNotHaveGetter = "The property '{0}' has no 'get' accessor";

	// Token: 0x04000149 RID: 329
	public const string PropertyDoesNotHaveSetter = "The property '{0}' has no 'set' accessor";

	// Token: 0x0400014A RID: 330
	public const string PropertyDoesNotHaveAccessor = "The property '{0}' has no 'get' or 'set' accessors";

	// Token: 0x0400014B RID: 331
	public const string NotAMemberOfType = "'{0}' is not a member of type '{1}'";

	// Token: 0x0400014C RID: 332
	public const string NotAMemberOfAnyType = "'{0}' is not a member of any type";

	// Token: 0x0400014D RID: 333
	public const string ExpressionNotSupportedForType = "The expression '{0}' is not supported for type '{1}'";

	// Token: 0x0400014E RID: 334
	public const string UnsupportedExpressionType = "The expression type '{0}' is not supported";

	// Token: 0x0400014F RID: 335
	public const string ParameterExpressionNotValidAsDelegate = "ParameterExpression of type '{0}' cannot be used for delegate parameter of type '{1}'";

	// Token: 0x04000150 RID: 336
	public const string PropertyNotDefinedForType = "Property '{0}' is not defined for type '{1}'";

	// Token: 0x04000151 RID: 337
	public const string InstancePropertyNotDefinedForType = "Instance property '{0}' is not defined for type '{1}'";

	// Token: 0x04000152 RID: 338
	public const string InstancePropertyWithoutParameterNotDefinedForType = "Instance property '{0}' that takes no argument is not defined for type '{1}'";

	// Token: 0x04000153 RID: 339
	public const string InstancePropertyWithSpecifiedParametersNotDefinedForType = "Instance property '{0}{1}' is not defined for type '{2}'";

	// Token: 0x04000154 RID: 340
	public const string InstanceAndMethodTypeMismatch = "Method '{0}' declared on type '{1}' cannot be called with instance of type '{2}'";

	// Token: 0x04000155 RID: 341
	public const string TypeContainsGenericParameters = "Type {0} contains generic parameters";

	// Token: 0x04000156 RID: 342
	public const string TypeIsGeneric = "Type {0} is a generic type definition";

	// Token: 0x04000157 RID: 343
	public const string TypeMissingDefaultConstructor = "Type '{0}' does not have a default constructor";

	// Token: 0x04000158 RID: 344
	public const string ElementInitializerMethodNotAdd = "Element initializer method must be named 'Add'";

	// Token: 0x04000159 RID: 345
	public const string ElementInitializerMethodNoRefOutParam = "Parameter '{0}' of element initializer method '{1}' must not be a pass by reference parameter";

	// Token: 0x0400015A RID: 346
	public const string ElementInitializerMethodWithZeroArgs = "Element initializer method must have at least 1 parameter";

	// Token: 0x0400015B RID: 347
	public const string ElementInitializerMethodStatic = "Element initializer method must be an instance method";

	// Token: 0x0400015C RID: 348
	public const string TypeNotIEnumerable = "Type '{0}' is not IEnumerable";

	// Token: 0x0400015D RID: 349
	public const string UnexpectedCoalesceOperator = "Unexpected coalesce operator.";

	// Token: 0x0400015E RID: 350
	public const string InvalidCast = "Cannot cast from type '{0}' to type '{1}";

	// Token: 0x0400015F RID: 351
	public const string UnhandledBinary = "Unhandled binary: {0}";

	// Token: 0x04000160 RID: 352
	public const string UnhandledBinding = "Unhandled binding ";

	// Token: 0x04000161 RID: 353
	public const string UnhandledBindingType = "Unhandled Binding Type: {0}";

	// Token: 0x04000162 RID: 354
	public const string UnhandledConvert = "Unhandled convert: {0}";

	// Token: 0x04000163 RID: 355
	public const string UnhandledUnary = "Unhandled unary: {0}";

	// Token: 0x04000164 RID: 356
	public const string UnknownBindingType = "Unknown binding type";

	// Token: 0x04000165 RID: 357
	public const string UserDefinedOpMustHaveConsistentTypes = "The user-defined operator method '{1}' for operator '{0}' must have identical parameter and return types.";

	// Token: 0x04000166 RID: 358
	public const string UserDefinedOpMustHaveValidReturnType = "The user-defined operator method '{1}' for operator '{0}' must return the same type as its parameter or a derived type.";

	// Token: 0x04000167 RID: 359
	public const string LogicalOperatorMustHaveBooleanOperators = "The user-defined operator method '{1}' for operator '{0}' must have associated boolean True and False operators.";

	// Token: 0x04000168 RID: 360
	public const string MethodWithArgsDoesNotExistOnType = "No method '{0}' on type '{1}' is compatible with the supplied arguments.";

	// Token: 0x04000169 RID: 361
	public const string GenericMethodWithArgsDoesNotExistOnType = "No generic method '{0}' on type '{1}' is compatible with the supplied type arguments and arguments. No type arguments should be provided if the method is non-generic. ";

	// Token: 0x0400016A RID: 362
	public const string MethodWithMoreThanOneMatch = "More than one method '{0}' on type '{1}' is compatible with the supplied arguments.";

	// Token: 0x0400016B RID: 363
	public const string PropertyWithMoreThanOneMatch = "More than one property '{0}' on type '{1}' is compatible with the supplied arguments.";

	// Token: 0x0400016C RID: 364
	public const string IncorrectNumberOfTypeArgsForFunc = "An incorrect number of type arguments were specified for the declaration of a Func type.";

	// Token: 0x0400016D RID: 365
	public const string IncorrectNumberOfTypeArgsForAction = "An incorrect number of type arguments were specified for the declaration of an Action type.";

	// Token: 0x0400016E RID: 366
	public const string ArgumentCannotBeOfTypeVoid = "Argument type cannot be System.Void.";

	// Token: 0x0400016F RID: 367
	public const string OutOfRange = "{0} must be greater than or equal to {1}";

	// Token: 0x04000170 RID: 368
	public const string LabelTargetAlreadyDefined = "Cannot redefine label '{0}' in an inner block.";

	// Token: 0x04000171 RID: 369
	public const string LabelTargetUndefined = "Cannot jump to undefined label '{0}'.";

	// Token: 0x04000172 RID: 370
	public const string ControlCannotLeaveFinally = "Control cannot leave a finally block.";

	// Token: 0x04000173 RID: 371
	public const string ControlCannotLeaveFilterTest = "Control cannot leave a filter test.";

	// Token: 0x04000174 RID: 372
	public const string AmbiguousJump = "Cannot jump to ambiguous label '{0}'.";

	// Token: 0x04000175 RID: 373
	public const string ControlCannotEnterTry = "Control cannot enter a try block.";

	// Token: 0x04000176 RID: 374
	public const string ControlCannotEnterExpression = "Control cannot enter an expression--only statements can be jumped into.";

	// Token: 0x04000177 RID: 375
	public const string NonLocalJumpWithValue = "Cannot jump to non-local label '{0}' with a value. Only jumps to labels defined in outer blocks can pass values.";

	// Token: 0x04000178 RID: 376
	public const string ExtensionNotReduced = "Extension should have been reduced.";

	// Token: 0x04000179 RID: 377
	public const string CannotCompileConstant = "CompileToMethod cannot compile constant '{0}' because it is a non-trivial value, such as a live object. Instead, create an expression tree that can construct this value.";

	// Token: 0x0400017A RID: 378
	public const string CannotCompileDynamic = "Dynamic expressions are not supported by CompileToMethod. Instead, create an expression tree that uses System.Runtime.CompilerServices.CallSite.";

	// Token: 0x0400017B RID: 379
	public const string InvalidLvalue = "Invalid lvalue for assignment: {0}.";

	// Token: 0x0400017C RID: 380
	public const string UnknownLiftType = "unknown lift type: '{0}'.";

	// Token: 0x0400017D RID: 381
	public const string UndefinedVariable = "variable '{0}' of type '{1}' referenced from scope '{2}', but it is not defined";

	// Token: 0x0400017E RID: 382
	public const string CannotCloseOverByRef = "Cannot close over byref parameter '{0}' referenced in lambda '{1}'";

	// Token: 0x0400017F RID: 383
	public const string UnexpectedVarArgsCall = "Unexpected VarArgs call to method '{0}'";

	// Token: 0x04000180 RID: 384
	public const string RethrowRequiresCatch = "Rethrow statement is valid only inside a Catch block.";

	// Token: 0x04000181 RID: 385
	public const string TryNotAllowedInFilter = "Try expression is not allowed inside a filter body.";

	// Token: 0x04000182 RID: 386
	public const string MustRewriteToSameNode = "When called from '{0}', rewriting a node of type '{1}' must return a non-null value of the same type. Alternatively, override '{2}' and change it to not visit children of this type.";

	// Token: 0x04000183 RID: 387
	public const string MustRewriteChildToSameType = "Rewriting child expression from type '{0}' to type '{1}' is not allowed, because it would change the meaning of the operation. If this is intentional, override '{2}' and change it to allow this rewrite.";

	// Token: 0x04000184 RID: 388
	public const string MustRewriteWithoutMethod = "Rewritten expression calls operator method '{0}', but the original node had no operator method. If this is intentional, override '{1}' and change it to allow this rewrite.";

	// Token: 0x04000185 RID: 389
	public const string InvalidNullValue = "The value null is not of type '{0}' and cannot be used in this collection.";

	// Token: 0x04000186 RID: 390
	public const string InvalidObjectType = "The value '{0}' is not of type '{1}' and cannot be used in this collection.";

	// Token: 0x04000187 RID: 391
	public const string TryNotSupportedForMethodsWithRefArgs = "TryExpression is not supported as an argument to method '{0}' because it has an argument with by-ref type. Construct the tree so the TryExpression is not nested inside of this expression.";

	// Token: 0x04000188 RID: 392
	public const string TryNotSupportedForValueTypeInstances = "TryExpression is not supported as a child expression when accessing a member on type '{0}' because it is a value type. Construct the tree so the TryExpression is not nested inside of this expression.";

	// Token: 0x04000189 RID: 393
	public const string EnumerationIsDone = "Enumeration has either not started or has already finished.";

	// Token: 0x0400018A RID: 394
	public const string TestValueTypeDoesNotMatchComparisonMethodParameter = "Test value of type '{0}' cannot be used for the comparison method parameter of type '{1}'";

	// Token: 0x0400018B RID: 395
	public const string SwitchValueTypeDoesNotMatchComparisonMethodParameter = "Switch value of type '{0}' cannot be used for the comparison method parameter of type '{1}'";

	// Token: 0x0400018C RID: 396
	public const string PdbGeneratorNeedsExpressionCompiler = "DebugInfoGenerator created by CreatePdbGenerator can only be used with LambdaExpression.CompileToMethod.";

	// Token: 0x0400018D RID: 397
	public const string InvalidArgumentValue = "Invalid argument value";

	// Token: 0x0400018E RID: 398
	public const string NonEmptyCollectionRequired = "Non-empty collection required";

	// Token: 0x0400018F RID: 399
	public const string CollectionModifiedWhileEnumerating = "Collection was modified; enumeration operation may not execute.";

	// Token: 0x04000190 RID: 400
	public const string ExpressionMustBeReadable = "Expression must be readable";

	// Token: 0x04000191 RID: 401
	public const string ExpressionTypeDoesNotMatchMethodParameter = "Expression of type '{0}' cannot be used for parameter of type '{1}' of method '{2}'";

	// Token: 0x04000192 RID: 402
	public const string ExpressionTypeDoesNotMatchParameter = "Expression of type '{0}' cannot be used for parameter of type '{1}'";

	// Token: 0x04000193 RID: 403
	public const string ExpressionTypeDoesNotMatchConstructorParameter = "Expression of type '{0}' cannot be used for constructor parameter of type '{1}'";

	// Token: 0x04000194 RID: 404
	public const string IncorrectNumberOfMethodCallArguments = "Incorrect number of arguments supplied for call to method '{0}'";

	// Token: 0x04000195 RID: 405
	public const string IncorrectNumberOfLambdaArguments = "Incorrect number of arguments supplied for lambda invocation";

	// Token: 0x04000196 RID: 406
	public const string IncorrectNumberOfConstructorArguments = "Incorrect number of arguments for constructor";

	// Token: 0x04000197 RID: 407
	public const string OperatorNotImplementedForType = "The operator '{0}' is not implemented for type '{1}'";

	// Token: 0x04000198 RID: 408
	public const string NonStaticConstructorRequired = "The constructor should not be static";

	// Token: 0x04000199 RID: 409
	public const string NonAbstractConstructorRequired = "Can't compile a NewExpression with a constructor declared on an abstract class";

	// Token: 0x0400019A RID: 410
	public const string FirstArgumentMustBeCallSite = "First argument of delegate must be CallSite";

	// Token: 0x0400019B RID: 411
	public const string NoOrInvalidRuleProduced = "No or Invalid rule produced";

	// Token: 0x0400019C RID: 412
	public const string TypeMustBeDerivedFromSystemDelegate = "Type must be derived from System.Delegate";

	// Token: 0x0400019D RID: 413
	public const string TypeParameterIsNotDelegate = "Type parameter is {0}. Expected a delegate.";

	// Token: 0x0400019E RID: 414
	public const string ArgumentTypeCannotBeVoid = "Argument type cannot be void";

	// Token: 0x0400019F RID: 415
	public const string ArgCntMustBeGreaterThanNameCnt = "Argument count must be greater than number of named arguments.";

	// Token: 0x040001A0 RID: 416
	public const string BinderNotCompatibleWithCallSite = "The result type '{0}' of the binder '{1}' is not compatible with the result type '{2}' expected by the call site.";

	// Token: 0x040001A1 RID: 417
	public const string BindingCannotBeNull = "Bind cannot return null.";

	// Token: 0x040001A2 RID: 418
	public const string DynamicBinderResultNotAssignable = "The result type '{0}' of the dynamic binding produced by binder '{1}' is not compatible with the result type '{2}' expected by the call site.";

	// Token: 0x040001A3 RID: 419
	public const string DynamicBindingNeedsRestrictions = "The result of the dynamic binding produced by the object with type '{0}' for the binder '{1}' needs at least one restriction.";

	// Token: 0x040001A4 RID: 420
	public const string DynamicObjectResultNotAssignable = "The result type '{0}' of the dynamic binding produced by the object with type '{1}' for the binder '{2}' is not compatible with the result type '{3}' expected by the call site.";

	// Token: 0x040001A5 RID: 421
	public const string InvalidMetaObjectCreated = "An IDynamicMetaObjectProvider {0} created an invalid DynamicMetaObject instance.";

	// Token: 0x040001A6 RID: 422
	public const string AmbiguousMatchInExpandoObject = "More than one key matching '{0}' was found in the ExpandoObject.";

	// Token: 0x040001A7 RID: 423
	public const string CollectionReadOnly = "Collection is read-only.";

	// Token: 0x040001A8 RID: 424
	public const string KeyDoesNotExistInExpando = "The specified key '{0}' does not exist in the ExpandoObject.";

	// Token: 0x040001A9 RID: 425
	public const string SameKeyExistsInExpando = "An element with the same key '{0}' already exists in the ExpandoObject.";

	// Token: 0x040001AA RID: 426
	public const string EmptyEnumerable = "Enumeration yielded no results";

	// Token: 0x040001AB RID: 427
	public const string MoreThanOneElement = "Sequence contains more than one element";

	// Token: 0x040001AC RID: 428
	public const string MoreThanOneMatch = "Sequence contains more than one matching element";

	// Token: 0x040001AD RID: 429
	public const string NoElements = "Sequence contains no elements";

	// Token: 0x040001AE RID: 430
	public const string NoMatch = "Sequence contains no matching element";

	// Token: 0x040001AF RID: 431
	public const string ParallelPartitionable_NullReturn = "The return value must not be null.";

	// Token: 0x040001B0 RID: 432
	public const string ParallelPartitionable_IncorretElementCount = "The returned array's length must equal the number of partitions requested.";

	// Token: 0x040001B1 RID: 433
	public const string ParallelPartitionable_NullElement = "Elements returned must not be null.";

	// Token: 0x040001B2 RID: 434
	public const string PLINQ_CommonEnumerator_Current_NotStarted = "Enumeration has not started. MoveNext must be called to initiate enumeration.";

	// Token: 0x040001B3 RID: 435
	public const string PLINQ_ExternalCancellationRequested = "The query has been canceled via the token supplied to WithCancellation.";

	// Token: 0x040001B4 RID: 436
	public const string PLINQ_DisposeRequested = "The query enumerator has been disposed.";

	// Token: 0x040001B5 RID: 437
	public const string ParallelQuery_DuplicateTaskScheduler = "The WithTaskScheduler operator may be used at most once in a query.";

	// Token: 0x040001B6 RID: 438
	public const string ParallelQuery_DuplicateDOP = "The WithDegreeOfParallelism operator may be used at most once in a query.";

	// Token: 0x040001B7 RID: 439
	public const string ParallelQuery_DuplicateExecutionMode = "The WithExecutionMode operator may be used at most once in a query.";

	// Token: 0x040001B8 RID: 440
	public const string PartitionerQueryOperator_NullPartitionList = "Partitioner returned null instead of a list of partitions.";

	// Token: 0x040001B9 RID: 441
	public const string PartitionerQueryOperator_WrongNumberOfPartitions = "Partitioner returned a wrong number of partitions.";

	// Token: 0x040001BA RID: 442
	public const string PartitionerQueryOperator_NullPartition = "Partitioner returned a null partition.";

	// Token: 0x040001BB RID: 443
	public const string ParallelQuery_DuplicateWithCancellation = "The WithCancellation operator may by used at most once in a query.";

	// Token: 0x040001BC RID: 444
	public const string ParallelQuery_DuplicateMergeOptions = "The WithMergeOptions operator may be used at most once in a query.";

	// Token: 0x040001BD RID: 445
	public const string PLINQ_EnumerationPreviouslyFailed = "The query enumerator previously threw an exception.";

	// Token: 0x040001BE RID: 446
	public const string ParallelQuery_PartitionerNotOrderable = "AsOrdered may not be used with a partitioner that is not orderable.";

	// Token: 0x040001BF RID: 447
	public const string ParallelQuery_InvalidAsOrderedCall = "AsOrdered may only be called on the result of AsParallel, ParallelEnumerable.Range, or ParallelEnumerable.Repeat.";

	// Token: 0x040001C0 RID: 448
	public const string ParallelQuery_InvalidNonGenericAsOrderedCall = "Non-generic AsOrdered may only be called on the result of the non-generic AsParallel.";

	// Token: 0x040001C1 RID: 449
	public const string ParallelEnumerable_BinaryOpMustUseAsParallel = "The second data source of a binary operator must be of type System.Linq.ParallelQuery<T> rather than System.Collections.Generic.IEnumerable<T>. To fix this problem, use the AsParallel() extension method to convert the right data source to System.Linq.ParallelQuery<T>.";

	// Token: 0x040001C2 RID: 450
	public const string ParallelEnumerable_WithQueryExecutionMode_InvalidMode = "The executionMode argument contains an invalid value.";

	// Token: 0x040001C3 RID: 451
	public const string ParallelEnumerable_WithMergeOptions_InvalidOptions = "The mergeOptions argument contains an invalid value.";

	// Token: 0x040001C4 RID: 452
	public const string ArgumentNotIEnumerableGeneric = "{0} is not IEnumerable<>";

	// Token: 0x040001C5 RID: 453
	public const string ArgumentNotValid = "Argument {0} is not valid";

	// Token: 0x040001C6 RID: 454
	public const string NoMethodOnType = "There is no method '{0}' on type '{1}'";

	// Token: 0x040001C7 RID: 455
	public const string NoMethodOnTypeMatchingArguments = "There is no method '{0}' on type '{1}' that matches the specified arguments";

	// Token: 0x040001C8 RID: 456
	public const string EnumeratingNullEnumerableExpression = "Cannot enumerate a query created from a null IEnumerable<>";

	// Token: 0x040001C9 RID: 457
	public const string MethodBuilderDoesNotHaveTypeBuilder = "MethodBuilder does not have a valid TypeBuilder";
}
