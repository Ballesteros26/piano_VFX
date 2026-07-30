using System;
using System.Diagnostics.Tracing;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32
{
	// Token: 0x020000A2 RID: 162
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	internal static class UnsafeNativeMethods
	{
		// Token: 0x020000A3 RID: 163
		[SuppressUnmanagedCodeSecurity]
		[SecurityCritical]
		internal static class ManifestEtw
		{
			// Token: 0x06000562 RID: 1378
			[SecurityCritical]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal unsafe static extern uint EventRegister([In] ref Guid providerId, [In] UnsafeNativeMethods.ManifestEtw.EtwEnableCallback enableCallback, [In] void* callbackContext, [In] [Out] ref long registrationHandle);

			// Token: 0x06000563 RID: 1379
			[SecurityCritical]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal static extern uint EventUnregister([In] long registrationHandle);

			// Token: 0x06000564 RID: 1380
			[SecurityCritical]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal unsafe static extern int EventWrite([In] long registrationHandle, [In] ref EventDescriptor eventDescriptor, [In] int userDataCount, [In] EventProvider.EventData* userData);

			// Token: 0x06000565 RID: 1381
			[SecurityCritical]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal static extern int EventWriteString([In] long registrationHandle, [In] byte level, [In] long keyword, [In] string msg);

			// Token: 0x06000566 RID: 1382 RVA: 0x0001F118 File Offset: 0x0001D318
			internal unsafe static int EventWriteTransferWrapper(long registrationHandle, ref EventDescriptor eventDescriptor, Guid* activityId, Guid* relatedActivityId, int userDataCount, EventProvider.EventData* userData)
			{
				int num = UnsafeNativeMethods.ManifestEtw.EventWriteTransfer(registrationHandle, ref eventDescriptor, activityId, relatedActivityId, userDataCount, userData);
				if (num == 87 && relatedActivityId == null)
				{
					Guid empty = Guid.Empty;
					num = UnsafeNativeMethods.ManifestEtw.EventWriteTransfer(registrationHandle, ref eventDescriptor, activityId, &empty, userDataCount, userData);
				}
				return num;
			}

			// Token: 0x06000567 RID: 1383
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			private unsafe static extern int EventWriteTransfer([In] long registrationHandle, [In] ref EventDescriptor eventDescriptor, [In] Guid* activityId, [In] Guid* relatedActivityId, [In] int userDataCount, [In] EventProvider.EventData* userData);

			// Token: 0x06000568 RID: 1384
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal static extern int EventActivityIdControl([In] UnsafeNativeMethods.ManifestEtw.ActivityControl ControlCode, [In] [Out] ref Guid ActivityId);

			// Token: 0x06000569 RID: 1385
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal unsafe static extern int EventSetInformation([In] long registrationHandle, [In] UnsafeNativeMethods.ManifestEtw.EVENT_INFO_CLASS informationClass, [In] void* eventInformation, [In] int informationLength);

			// Token: 0x0600056A RID: 1386
			[SuppressUnmanagedCodeSecurity]
			[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
			internal unsafe static extern int EnumerateTraceGuidsEx(UnsafeNativeMethods.ManifestEtw.TRACE_QUERY_INFO_CLASS TraceQueryInfoClass, void* InBuffer, int InBufferSize, void* OutBuffer, int OutBufferSize, ref int ReturnLength);

			// Token: 0x040005E5 RID: 1509
			internal const int ERROR_ARITHMETIC_OVERFLOW = 534;

			// Token: 0x040005E6 RID: 1510
			internal const int ERROR_NOT_ENOUGH_MEMORY = 8;

			// Token: 0x040005E7 RID: 1511
			internal const int ERROR_MORE_DATA = 234;

			// Token: 0x040005E8 RID: 1512
			internal const int ERROR_NOT_SUPPORTED = 50;

			// Token: 0x040005E9 RID: 1513
			internal const int ERROR_INVALID_PARAMETER = 87;

			// Token: 0x040005EA RID: 1514
			internal const int EVENT_CONTROL_CODE_DISABLE_PROVIDER = 0;

			// Token: 0x040005EB RID: 1515
			internal const int EVENT_CONTROL_CODE_ENABLE_PROVIDER = 1;

			// Token: 0x040005EC RID: 1516
			internal const int EVENT_CONTROL_CODE_CAPTURE_STATE = 2;

			// Token: 0x020000A4 RID: 164
			// (Invoke) Token: 0x0600056C RID: 1388
			[SecurityCritical]
			internal unsafe delegate void EtwEnableCallback([In] ref Guid sourceId, [In] int isEnabled, [In] byte level, [In] long matchAnyKeywords, [In] long matchAllKeywords, [In] UnsafeNativeMethods.ManifestEtw.EVENT_FILTER_DESCRIPTOR* filterData, [In] void* callbackContext);

			// Token: 0x020000A5 RID: 165
			internal struct EVENT_FILTER_DESCRIPTOR
			{
				// Token: 0x040005ED RID: 1517
				public long Ptr;

				// Token: 0x040005EE RID: 1518
				public int Size;

				// Token: 0x040005EF RID: 1519
				public int Type;
			}

			// Token: 0x020000A6 RID: 166
			internal enum ActivityControl : uint
			{
				// Token: 0x040005F1 RID: 1521
				EVENT_ACTIVITY_CTRL_GET_ID = 1U,
				// Token: 0x040005F2 RID: 1522
				EVENT_ACTIVITY_CTRL_SET_ID,
				// Token: 0x040005F3 RID: 1523
				EVENT_ACTIVITY_CTRL_CREATE_ID,
				// Token: 0x040005F4 RID: 1524
				EVENT_ACTIVITY_CTRL_GET_SET_ID,
				// Token: 0x040005F5 RID: 1525
				EVENT_ACTIVITY_CTRL_CREATE_SET_ID
			}

			// Token: 0x020000A7 RID: 167
			internal enum EVENT_INFO_CLASS
			{
				// Token: 0x040005F7 RID: 1527
				BinaryTrackInfo,
				// Token: 0x040005F8 RID: 1528
				SetEnableAllKeywords,
				// Token: 0x040005F9 RID: 1529
				SetTraits
			}

			// Token: 0x020000A8 RID: 168
			internal enum TRACE_QUERY_INFO_CLASS
			{
				// Token: 0x040005FB RID: 1531
				TraceGuidQueryList,
				// Token: 0x040005FC RID: 1532
				TraceGuidQueryInfo,
				// Token: 0x040005FD RID: 1533
				TraceGuidQueryProcess,
				// Token: 0x040005FE RID: 1534
				TraceStackTracingInfo,
				// Token: 0x040005FF RID: 1535
				MaxTraceSetInfoClass
			}

			// Token: 0x020000A9 RID: 169
			internal struct TRACE_GUID_INFO
			{
				// Token: 0x04000600 RID: 1536
				public int InstanceCount;

				// Token: 0x04000601 RID: 1537
				public int Reserved;
			}

			// Token: 0x020000AA RID: 170
			internal struct TRACE_PROVIDER_INSTANCE_INFO
			{
				// Token: 0x04000602 RID: 1538
				public int NextOffset;

				// Token: 0x04000603 RID: 1539
				public int EnableCount;

				// Token: 0x04000604 RID: 1540
				public int Pid;

				// Token: 0x04000605 RID: 1541
				public int Flags;
			}

			// Token: 0x020000AB RID: 171
			internal struct TRACE_ENABLE_INFO
			{
				// Token: 0x04000606 RID: 1542
				public int IsEnabled;

				// Token: 0x04000607 RID: 1543
				public byte Level;

				// Token: 0x04000608 RID: 1544
				public byte Reserved1;

				// Token: 0x04000609 RID: 1545
				public ushort LoggerId;

				// Token: 0x0400060A RID: 1546
				public int EnableProperty;

				// Token: 0x0400060B RID: 1547
				public int Reserved2;

				// Token: 0x0400060C RID: 1548
				public long MatchAnyKeyword;

				// Token: 0x0400060D RID: 1549
				public long MatchAllKeyword;
			}
		}
	}
}
