using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace Unity.Baselib.LowLevel
{
	// Token: 0x02000015 RID: 21
	[NativeHeader("External/baselib/builds/CSharp/UnityBinding/Baselib_ErrorState.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/UnityBinding/Baselib_Memory.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/UnityBinding/Baselib_RegisteredNetwork.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/UnityBinding/Baselib_ErrorCode.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/UnityBinding/Baselib_NetworkAddress.gen.binding.h")]
	[NativeHeader("External/baselib/builds/CSharp/UnityBinding/Baselib_SourceLocation.gen.binding.h")]
	internal static class Binding
	{
		// Token: 0x06000027 RID: 39
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public unsafe static extern uint Baselib_ErrorState_Explain(Binding.Baselib_ErrorState* errorState, byte* buffer, uint bufferLen, Binding.Baselib_ErrorState_ExplainVerbosity verbosity);

		// Token: 0x06000028 RID: 40
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public unsafe static extern void Baselib_Memory_GetPageSizeInfo(Binding.Baselib_Memory_PageSizeInfo* outPagesSizeInfo);

		// Token: 0x06000029 RID: 41
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern IntPtr Baselib_Memory_Allocate(UIntPtr size);

		// Token: 0x0600002A RID: 42
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern IntPtr Baselib_Memory_Reallocate(IntPtr ptr, UIntPtr newSize);

		// Token: 0x0600002B RID: 43
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern void Baselib_Memory_Free(IntPtr ptr);

		// Token: 0x0600002C RID: 44
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern IntPtr Baselib_Memory_AlignedAllocate(UIntPtr size, UIntPtr alignment);

		// Token: 0x0600002D RID: 45
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern IntPtr Baselib_Memory_AlignedReallocate(IntPtr ptr, UIntPtr newSize, UIntPtr alignment);

		// Token: 0x0600002E RID: 46
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern void Baselib_Memory_AlignedFree(IntPtr ptr);

		// Token: 0x0600002F RID: 47 RVA: 0x00002238 File Offset: 0x00000438
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_Memory_PageAllocation Baselib_Memory_AllocatePages(ulong pageSize, ulong pageCount, ulong alignmentInMultipleOfPageSize, Binding.Baselib_Memory_PageState pageState, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Memory_PageAllocation baselib_Memory_PageAllocation;
			Binding.Baselib_Memory_AllocatePages_Injected(pageSize, pageCount, alignmentInMultipleOfPageSize, pageState, errorState, out baselib_Memory_PageAllocation);
			return baselib_Memory_PageAllocation;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002253 File Offset: 0x00000453
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_Memory_ReleasePages(Binding.Baselib_Memory_PageAllocation pageAllocation, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_Memory_ReleasePages_Injected(ref pageAllocation, errorState);
		}

		// Token: 0x06000031 RID: 49
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public unsafe static extern void Baselib_Memory_SetPageState(IntPtr addressOfFirstPage, ulong pageSize, ulong pageCount, Binding.Baselib_Memory_PageState pageState, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000032 RID: 50
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public unsafe static extern void Baselib_NetworkAddress_Encode(Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_NetworkAddress_Family family, byte* ip, ushort port, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000033 RID: 51
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public unsafe static extern void Baselib_NetworkAddress_Decode(Binding.Baselib_NetworkAddress* srcAddress, Binding.Baselib_NetworkAddress_Family* family, byte* ipAddressBuffer, uint ipAddressBufferLen, ushort* port, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000034 RID: 52 RVA: 0x00002260 File Offset: 0x00000460
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_Buffer Baselib_RegisteredNetwork_Buffer_Register(Binding.Baselib_Memory_PageAllocation pageAllocation, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Buffer baselib_RegisteredNetwork_Buffer;
			Binding.Baselib_RegisteredNetwork_Buffer_Register_Injected(ref pageAllocation, errorState, out baselib_RegisteredNetwork_Buffer);
			return baselib_RegisteredNetwork_Buffer;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002278 File Offset: 0x00000478
		[FreeFunction(IsThreadSafe = true)]
		public static void Baselib_RegisteredNetwork_Buffer_Deregister(Binding.Baselib_RegisteredNetwork_Buffer buffer)
		{
			Binding.Baselib_RegisteredNetwork_Buffer_Deregister_Injected(ref buffer);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002284 File Offset: 0x00000484
		[FreeFunction(IsThreadSafe = true)]
		public static Binding.Baselib_RegisteredNetwork_BufferSlice Baselib_RegisteredNetwork_BufferSlice_Create(Binding.Baselib_RegisteredNetwork_Buffer buffer, uint offset, uint size)
		{
			Binding.Baselib_RegisteredNetwork_BufferSlice baselib_RegisteredNetwork_BufferSlice;
			Binding.Baselib_RegisteredNetwork_BufferSlice_Create_Injected(ref buffer, offset, size, out baselib_RegisteredNetwork_BufferSlice);
			return baselib_RegisteredNetwork_BufferSlice;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000022A0 File Offset: 0x000004A0
		[FreeFunction(IsThreadSafe = true)]
		public static Binding.Baselib_RegisteredNetwork_BufferSlice Baselib_RegisteredNetwork_BufferSlice_Empty()
		{
			Binding.Baselib_RegisteredNetwork_BufferSlice baselib_RegisteredNetwork_BufferSlice;
			Binding.Baselib_RegisteredNetwork_BufferSlice_Empty_Injected(out baselib_RegisteredNetwork_BufferSlice);
			return baselib_RegisteredNetwork_BufferSlice;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000022B8 File Offset: 0x000004B8
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_Endpoint Baselib_RegisteredNetwork_Endpoint_Create(Binding.Baselib_NetworkAddress* srcAddress, Binding.Baselib_RegisteredNetwork_BufferSlice dstSlice, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Endpoint baselib_RegisteredNetwork_Endpoint;
			Binding.Baselib_RegisteredNetwork_Endpoint_Create_Injected(srcAddress, ref dstSlice, errorState, out baselib_RegisteredNetwork_Endpoint);
			return baselib_RegisteredNetwork_Endpoint;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000022D4 File Offset: 0x000004D4
		[FreeFunction(IsThreadSafe = true)]
		public static Binding.Baselib_RegisteredNetwork_Endpoint Baselib_RegisteredNetwork_Endpoint_Empty()
		{
			Binding.Baselib_RegisteredNetwork_Endpoint baselib_RegisteredNetwork_Endpoint;
			Binding.Baselib_RegisteredNetwork_Endpoint_Empty_Injected(out baselib_RegisteredNetwork_Endpoint);
			return baselib_RegisteredNetwork_Endpoint;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000022E9 File Offset: 0x000004E9
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_RegisteredNetwork_Endpoint_GetNetworkAddress(Binding.Baselib_RegisteredNetwork_Endpoint endpoint, Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Endpoint_GetNetworkAddress_Injected(ref endpoint, dstAddress, errorState);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000022F4 File Offset: 0x000004F4
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_Socket_UDP Baselib_RegisteredNetwork_Socket_UDP_Create(Binding.Baselib_NetworkAddress* bindAddress, Binding.Baselib_NetworkAddress_AddressReuse endpointReuse, uint sendQueueSize, uint recvQueueSize, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Socket_UDP baselib_RegisteredNetwork_Socket_UDP;
			Binding.Baselib_RegisteredNetwork_Socket_UDP_Create_Injected(bindAddress, endpointReuse, sendQueueSize, recvQueueSize, errorState, out baselib_RegisteredNetwork_Socket_UDP);
			return baselib_RegisteredNetwork_Socket_UDP;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000230F File Offset: 0x0000050F
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_RegisteredNetwork_Socket_UDP_ScheduleRecv(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_Request* requests, uint requestsCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_ScheduleRecv_Injected(ref socket, requests, requestsCount, errorState);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000231B File Offset: 0x0000051B
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_RegisteredNetwork_Socket_UDP_ScheduleSend(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_Request* requests, uint requestsCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_ScheduleSend_Injected(ref socket, requests, requestsCount, errorState);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002327 File Offset: 0x00000527
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_ProcessStatus Baselib_RegisteredNetwork_Socket_UDP_ProcessRecv(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_ProcessRecv_Injected(ref socket, errorState);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002331 File Offset: 0x00000531
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_ProcessStatus Baselib_RegisteredNetwork_Socket_UDP_ProcessSend(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_ProcessSend_Injected(ref socket, errorState);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000233B File Offset: 0x0000053B
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_CompletionQueueStatus Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedRecv(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, uint timeoutInMilliseconds, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedRecv_Injected(ref socket, timeoutInMilliseconds, errorState);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002346 File Offset: 0x00000546
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static Binding.Baselib_RegisteredNetwork_CompletionQueueStatus Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedSend(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, uint timeoutInMilliseconds, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedSend_Injected(ref socket, timeoutInMilliseconds, errorState);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002351 File Offset: 0x00000551
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_RegisteredNetwork_Socket_UDP_DequeueRecv(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_CompletionResult* results, uint resultsCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_DequeueRecv_Injected(ref socket, results, resultsCount, errorState);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000235D File Offset: 0x0000055D
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static uint Baselib_RegisteredNetwork_Socket_UDP_DequeueSend(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_CompletionResult* results, uint resultsCount, Binding.Baselib_ErrorState* errorState)
		{
			return Binding.Baselib_RegisteredNetwork_Socket_UDP_DequeueSend_Injected(ref socket, results, resultsCount, errorState);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002369 File Offset: 0x00000569
		[FreeFunction(IsThreadSafe = true)]
		public unsafe static void Baselib_RegisteredNetwork_Socket_UDP_GetNetworkAddress(Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_ErrorState* errorState)
		{
			Binding.Baselib_RegisteredNetwork_Socket_UDP_GetNetworkAddress_Injected(ref socket, dstAddress, errorState);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002374 File Offset: 0x00000574
		[FreeFunction(IsThreadSafe = true)]
		public static void Baselib_RegisteredNetwork_Socket_UDP_Close(Binding.Baselib_RegisteredNetwork_Socket_UDP socket)
		{
			Binding.Baselib_RegisteredNetwork_Socket_UDP_Close_Injected(ref socket);
		}

		// Token: 0x06000047 RID: 71
		[MethodImpl(4096)]
		private unsafe static extern void Baselib_Memory_AllocatePages_Injected(ulong pageSize, ulong pageCount, ulong alignmentInMultipleOfPageSize, Binding.Baselib_Memory_PageState pageState, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_Memory_PageAllocation ret);

		// Token: 0x06000048 RID: 72
		[MethodImpl(4096)]
		private unsafe static extern void Baselib_Memory_ReleasePages_Injected(ref Binding.Baselib_Memory_PageAllocation pageAllocation, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000049 RID: 73
		[MethodImpl(4096)]
		private unsafe static extern void Baselib_RegisteredNetwork_Buffer_Register_Injected(ref Binding.Baselib_Memory_PageAllocation pageAllocation, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_RegisteredNetwork_Buffer ret);

		// Token: 0x0600004A RID: 74
		[MethodImpl(4096)]
		private static extern void Baselib_RegisteredNetwork_Buffer_Deregister_Injected(ref Binding.Baselib_RegisteredNetwork_Buffer buffer);

		// Token: 0x0600004B RID: 75
		[MethodImpl(4096)]
		private static extern void Baselib_RegisteredNetwork_BufferSlice_Create_Injected(ref Binding.Baselib_RegisteredNetwork_Buffer buffer, uint offset, uint size, out Binding.Baselib_RegisteredNetwork_BufferSlice ret);

		// Token: 0x0600004C RID: 76
		[MethodImpl(4096)]
		private static extern void Baselib_RegisteredNetwork_BufferSlice_Empty_Injected(out Binding.Baselib_RegisteredNetwork_BufferSlice ret);

		// Token: 0x0600004D RID: 77
		[MethodImpl(4096)]
		private unsafe static extern void Baselib_RegisteredNetwork_Endpoint_Create_Injected(Binding.Baselib_NetworkAddress* srcAddress, ref Binding.Baselib_RegisteredNetwork_BufferSlice dstSlice, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_RegisteredNetwork_Endpoint ret);

		// Token: 0x0600004E RID: 78
		[MethodImpl(4096)]
		private static extern void Baselib_RegisteredNetwork_Endpoint_Empty_Injected(out Binding.Baselib_RegisteredNetwork_Endpoint ret);

		// Token: 0x0600004F RID: 79
		[MethodImpl(4096)]
		private unsafe static extern void Baselib_RegisteredNetwork_Endpoint_GetNetworkAddress_Injected(ref Binding.Baselib_RegisteredNetwork_Endpoint endpoint, Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000050 RID: 80
		[MethodImpl(4096)]
		private unsafe static extern void Baselib_RegisteredNetwork_Socket_UDP_Create_Injected(Binding.Baselib_NetworkAddress* bindAddress, Binding.Baselib_NetworkAddress_AddressReuse endpointReuse, uint sendQueueSize, uint recvQueueSize, Binding.Baselib_ErrorState* errorState, out Binding.Baselib_RegisteredNetwork_Socket_UDP ret);

		// Token: 0x06000051 RID: 81
		[MethodImpl(4096)]
		private unsafe static extern uint Baselib_RegisteredNetwork_Socket_UDP_ScheduleRecv_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_Request* requests, uint requestsCount, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000052 RID: 82
		[MethodImpl(4096)]
		private unsafe static extern uint Baselib_RegisteredNetwork_Socket_UDP_ScheduleSend_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_Request* requests, uint requestsCount, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000053 RID: 83
		[MethodImpl(4096)]
		private unsafe static extern Binding.Baselib_RegisteredNetwork_ProcessStatus Baselib_RegisteredNetwork_Socket_UDP_ProcessRecv_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000054 RID: 84
		[MethodImpl(4096)]
		private unsafe static extern Binding.Baselib_RegisteredNetwork_ProcessStatus Baselib_RegisteredNetwork_Socket_UDP_ProcessSend_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000055 RID: 85
		[MethodImpl(4096)]
		private unsafe static extern Binding.Baselib_RegisteredNetwork_CompletionQueueStatus Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedRecv_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, uint timeoutInMilliseconds, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000056 RID: 86
		[MethodImpl(4096)]
		private unsafe static extern Binding.Baselib_RegisteredNetwork_CompletionQueueStatus Baselib_RegisteredNetwork_Socket_UDP_WaitForCompletedSend_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, uint timeoutInMilliseconds, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000057 RID: 87
		[MethodImpl(4096)]
		private unsafe static extern uint Baselib_RegisteredNetwork_Socket_UDP_DequeueRecv_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_CompletionResult* results, uint resultsCount, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000058 RID: 88
		[MethodImpl(4096)]
		private unsafe static extern uint Baselib_RegisteredNetwork_Socket_UDP_DequeueSend_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_RegisteredNetwork_CompletionResult* results, uint resultsCount, Binding.Baselib_ErrorState* errorState);

		// Token: 0x06000059 RID: 89
		[MethodImpl(4096)]
		private unsafe static extern void Baselib_RegisteredNetwork_Socket_UDP_GetNetworkAddress_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket, Binding.Baselib_NetworkAddress* dstAddress, Binding.Baselib_ErrorState* errorState);

		// Token: 0x0600005A RID: 90
		[MethodImpl(4096)]
		private static extern void Baselib_RegisteredNetwork_Socket_UDP_Close_Injected(ref Binding.Baselib_RegisteredNetwork_Socket_UDP socket);

		// Token: 0x04000027 RID: 39
		public static readonly UIntPtr Baselib_Memory_MaxAlignment = new UIntPtr(65536U);

		// Token: 0x04000028 RID: 40
		public static readonly Binding.Baselib_Memory_PageAllocation Baselib_Memory_PageAllocation_Invalid = default(Binding.Baselib_Memory_PageAllocation);

		// Token: 0x04000029 RID: 41
		public const uint Baselib_NetworkAddress_IpMaxStringLength = 46U;

		// Token: 0x0400002A RID: 42
		public static readonly IntPtr Baselib_RegisteredNetwork_Buffer_Id_Invalid = IntPtr.Zero;

		// Token: 0x0400002B RID: 43
		public const uint Baselib_RegisteredNetwork_Endpoint_MaxSize = 28U;

		// Token: 0x0400002C RID: 44
		public static readonly Binding.Baselib_RegisteredNetwork_Socket_UDP Baselib_RegisteredNetwork_Socket_UDP_Invalid = default(Binding.Baselib_RegisteredNetwork_Socket_UDP);

		// Token: 0x02000016 RID: 22
		public enum Baselib_ErrorCode
		{
			// Token: 0x0400002E RID: 46
			Success,
			// Token: 0x0400002F RID: 47
			OutOfMemory = 16777216,
			// Token: 0x04000030 RID: 48
			OutOfSystemResources,
			// Token: 0x04000031 RID: 49
			InvalidAddressRange,
			// Token: 0x04000032 RID: 50
			InvalidArgument,
			// Token: 0x04000033 RID: 51
			InvalidBufferSize,
			// Token: 0x04000034 RID: 52
			InvalidState,
			// Token: 0x04000035 RID: 53
			NotSupported,
			// Token: 0x04000036 RID: 54
			Timeout,
			// Token: 0x04000037 RID: 55
			UnsupportedAlignment = 33554432,
			// Token: 0x04000038 RID: 56
			InvalidPageSize,
			// Token: 0x04000039 RID: 57
			InvalidPageCount,
			// Token: 0x0400003A RID: 58
			UnsupportedPageState,
			// Token: 0x0400003B RID: 59
			UninitializedThreadConfig = 50331648,
			// Token: 0x0400003C RID: 60
			ThreadEntryPointFunctionNotSet,
			// Token: 0x0400003D RID: 61
			ThreadCannotJoinSelf,
			// Token: 0x0400003E RID: 62
			NetworkInitializationError = 67108864,
			// Token: 0x0400003F RID: 63
			AddressInUse,
			// Token: 0x04000040 RID: 64
			AddressUnreachable,
			// Token: 0x04000041 RID: 65
			AddressFamilyNotSupported,
			// Token: 0x04000042 RID: 66
			UnexpectedError = -1
		}

		// Token: 0x02000017 RID: 23
		public enum Baselib_ErrorState_NativeErrorCodeType : byte
		{
			// Token: 0x04000044 RID: 68
			None,
			// Token: 0x04000045 RID: 69
			PlatformDefined
		}

		// Token: 0x02000018 RID: 24
		public struct Baselib_ErrorState
		{
			// Token: 0x04000046 RID: 70
			public Binding.Baselib_ErrorCode code;

			// Token: 0x04000047 RID: 71
			public Binding.Baselib_ErrorState_NativeErrorCodeType nativeErrorCodeType;

			// Token: 0x04000048 RID: 72
			public ulong nativeErrorCode;

			// Token: 0x04000049 RID: 73
			public Binding.Baselib_SourceLocation sourceLocation;
		}

		// Token: 0x02000019 RID: 25
		public enum Baselib_ErrorState_ExplainVerbosity
		{
			// Token: 0x0400004B RID: 75
			ErrorType,
			// Token: 0x0400004C RID: 76
			ErrorType_SourceLocation_Explanation
		}

		// Token: 0x0200001A RID: 26
		public struct Baselib_Memory_PageSizeInfo
		{
			// Token: 0x0400004D RID: 77
			public ulong defaultPageSize;

			// Token: 0x0400004E RID: 78
			public ulong pageSizes0;

			// Token: 0x0400004F RID: 79
			public ulong pageSizes1;

			// Token: 0x04000050 RID: 80
			public ulong pageSizes2;

			// Token: 0x04000051 RID: 81
			public ulong pageSizes3;

			// Token: 0x04000052 RID: 82
			public ulong pageSizes4;

			// Token: 0x04000053 RID: 83
			public ulong pageSizes5;

			// Token: 0x04000054 RID: 84
			public ulong pageSizesLen;
		}

		// Token: 0x0200001B RID: 27
		public struct Baselib_Memory_PageAllocation
		{
			// Token: 0x04000055 RID: 85
			public IntPtr ptr;

			// Token: 0x04000056 RID: 86
			public ulong pageSize;

			// Token: 0x04000057 RID: 87
			public ulong pageCount;
		}

		// Token: 0x0200001C RID: 28
		public enum Baselib_Memory_PageState
		{
			// Token: 0x04000059 RID: 89
			Reserved,
			// Token: 0x0400005A RID: 90
			NoAccess,
			// Token: 0x0400005B RID: 91
			ReadOnly,
			// Token: 0x0400005C RID: 92
			ReadWrite = 4,
			// Token: 0x0400005D RID: 93
			ReadOnly_Executable = 18,
			// Token: 0x0400005E RID: 94
			ReadWrite_Executable = 20
		}

		// Token: 0x0200001D RID: 29
		public enum Baselib_NetworkAddress_Family
		{
			// Token: 0x04000060 RID: 96
			Invalid,
			// Token: 0x04000061 RID: 97
			IPv4,
			// Token: 0x04000062 RID: 98
			IPv6
		}

		// Token: 0x0200001E RID: 30
		public struct Baselib_NetworkAddress
		{
			// Token: 0x04000063 RID: 99
			public byte data0;

			// Token: 0x04000064 RID: 100
			public byte data1;

			// Token: 0x04000065 RID: 101
			public byte data2;

			// Token: 0x04000066 RID: 102
			public byte data3;

			// Token: 0x04000067 RID: 103
			public byte data4;

			// Token: 0x04000068 RID: 104
			public byte data5;

			// Token: 0x04000069 RID: 105
			public byte data6;

			// Token: 0x0400006A RID: 106
			public byte data7;

			// Token: 0x0400006B RID: 107
			public byte data8;

			// Token: 0x0400006C RID: 108
			public byte data9;

			// Token: 0x0400006D RID: 109
			public byte data10;

			// Token: 0x0400006E RID: 110
			public byte data11;

			// Token: 0x0400006F RID: 111
			public byte data12;

			// Token: 0x04000070 RID: 112
			public byte data13;

			// Token: 0x04000071 RID: 113
			public byte data14;

			// Token: 0x04000072 RID: 114
			public byte data15;

			// Token: 0x04000073 RID: 115
			public byte port0;

			// Token: 0x04000074 RID: 116
			public byte port1;

			// Token: 0x04000075 RID: 117
			public byte family;

			// Token: 0x04000076 RID: 118
			public byte _padding;
		}

		// Token: 0x0200001F RID: 31
		public enum Baselib_NetworkAddress_AddressReuse
		{
			// Token: 0x04000078 RID: 120
			DoNotAllow,
			// Token: 0x04000079 RID: 121
			Allow
		}

		// Token: 0x02000020 RID: 32
		public struct Baselib_RegisteredNetwork_Buffer
		{
			// Token: 0x0400007A RID: 122
			public IntPtr id;

			// Token: 0x0400007B RID: 123
			public Binding.Baselib_Memory_PageAllocation allocation;
		}

		// Token: 0x02000021 RID: 33
		public struct Baselib_RegisteredNetwork_BufferSlice
		{
			// Token: 0x0400007C RID: 124
			public IntPtr id;

			// Token: 0x0400007D RID: 125
			public IntPtr data;

			// Token: 0x0400007E RID: 126
			public uint size;

			// Token: 0x0400007F RID: 127
			public uint offset;
		}

		// Token: 0x02000022 RID: 34
		public struct Baselib_RegisteredNetwork_Endpoint
		{
			// Token: 0x04000080 RID: 128
			public Binding.Baselib_RegisteredNetwork_BufferSlice slice;
		}

		// Token: 0x02000023 RID: 35
		public struct Baselib_RegisteredNetwork_Request
		{
			// Token: 0x04000081 RID: 129
			public Binding.Baselib_RegisteredNetwork_BufferSlice payload;

			// Token: 0x04000082 RID: 130
			public Binding.Baselib_RegisteredNetwork_Endpoint remoteEndpoint;

			// Token: 0x04000083 RID: 131
			public IntPtr requestUserdata;
		}

		// Token: 0x02000024 RID: 36
		public enum Baselib_RegisteredNetwork_CompletionStatus
		{
			// Token: 0x04000085 RID: 133
			Failed,
			// Token: 0x04000086 RID: 134
			Success
		}

		// Token: 0x02000025 RID: 37
		public struct Baselib_RegisteredNetwork_CompletionResult
		{
			// Token: 0x04000087 RID: 135
			public Binding.Baselib_RegisteredNetwork_CompletionStatus status;

			// Token: 0x04000088 RID: 136
			public uint bytesTransferred;

			// Token: 0x04000089 RID: 137
			public IntPtr requestUserdata;
		}

		// Token: 0x02000026 RID: 38
		public struct Baselib_RegisteredNetwork_Socket_UDP
		{
			// Token: 0x0400008A RID: 138
			public IntPtr handle;
		}

		// Token: 0x02000027 RID: 39
		public enum Baselib_RegisteredNetwork_ProcessStatus
		{
			// Token: 0x0400008C RID: 140
			Done,
			// Token: 0x0400008D RID: 141
			Pending
		}

		// Token: 0x02000028 RID: 40
		public enum Baselib_RegisteredNetwork_CompletionQueueStatus
		{
			// Token: 0x0400008F RID: 143
			NoResultsAvailable,
			// Token: 0x04000090 RID: 144
			ResultsAvailable
		}

		// Token: 0x02000029 RID: 41
		public struct Baselib_SourceLocation
		{
			// Token: 0x04000091 RID: 145
			public unsafe byte* file;

			// Token: 0x04000092 RID: 146
			public unsafe byte* function;

			// Token: 0x04000093 RID: 147
			public uint lineNumber;
		}
	}
}
