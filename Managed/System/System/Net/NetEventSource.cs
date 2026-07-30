using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000414 RID: 1044
	internal sealed class NetEventSource : EventSource
	{
		// Token: 0x06001FC7 RID: 8135 RVA: 0x0007C027 File Offset: 0x0007A227
		[NonEvent]
		public static void Enter(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0007C051 File Offset: 0x0007A251
		[NonEvent]
		public static void Enter(object thisOrContextObject, object arg0, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("({0})", NetEventSource.Format(arg0)));
			}
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0007C07B File Offset: 0x0007A27B
		[NonEvent]
		public static void Enter(object thisOrContextObject, object arg0, object arg1, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("({0}, {1})", NetEventSource.Format(arg0), NetEventSource.Format(arg1)));
			}
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x0007C0AB File Offset: 0x0007A2AB
		[NonEvent]
		public static void Enter(object thisOrContextObject, object arg0, object arg1, object arg2, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("({0}, {1}, {2})", NetEventSource.Format(arg0), NetEventSource.Format(arg1), NetEventSource.Format(arg2)));
			}
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x0007C0E2 File Offset: 0x0007A2E2
		[Event(1, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void Enter(string thisOrContextObject, string memberName, string parameters)
		{
			base.WriteEvent(1, thisOrContextObject, memberName ?? "(?)", parameters);
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x0007C0F7 File Offset: 0x0007A2F7
		[NonEvent]
		public static void Exit(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Exit(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x0007C121 File Offset: 0x0007A321
		[NonEvent]
		public static void Exit(object thisOrContextObject, object arg0, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Exit(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(arg0).ToString());
			}
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x0007C146 File Offset: 0x0007A346
		[NonEvent]
		public static void Exit(object thisOrContextObject, object arg0, object arg1, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Exit(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("{0}, {1}", NetEventSource.Format(arg0), NetEventSource.Format(arg1)));
			}
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x0007C176 File Offset: 0x0007A376
		[Event(2, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void Exit(string thisOrContextObject, string memberName, string result)
		{
			base.WriteEvent(2, thisOrContextObject, memberName ?? "(?)", result);
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x0007C18B File Offset: 0x0007A38B
		[NonEvent]
		public static void Info(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Info(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x0007C1B5 File Offset: 0x0007A3B5
		[NonEvent]
		public static void Info(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Info(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x0007C1DA File Offset: 0x0007A3DA
		[Event(4, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void Info(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(4, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x06001FD3 RID: 8147 RVA: 0x0007C1EF File Offset: 0x0007A3EF
		[NonEvent]
		public static void Error(object thisOrContextObject, FormattableString formattableString, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.ErrorMessage(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(formattableString));
			}
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0007C20F File Offset: 0x0007A40F
		[NonEvent]
		public static void Error(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.ErrorMessage(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x0007C234 File Offset: 0x0007A434
		[Event(5, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		private void ErrorMessage(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(5, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x0007C249 File Offset: 0x0007A449
		[NonEvent]
		public static void Fail(object thisOrContextObject, FormattableString formattableString, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.CriticalFailure(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(formattableString));
			}
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x0007C269 File Offset: 0x0007A469
		[NonEvent]
		public static void Fail(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.CriticalFailure(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x0007C28E File Offset: 0x0007A48E
		[Event(6, Level = EventLevel.Critical, Keywords = (EventKeywords)2L)]
		private void CriticalFailure(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(6, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x0007C2A3 File Offset: 0x0007A4A3
		[NonEvent]
		public static void DumpBuffer(object thisOrContextObject, byte[] buffer, [CallerMemberName] string memberName = null)
		{
			NetEventSource.DumpBuffer(thisOrContextObject, buffer, 0, buffer.Length, memberName);
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0007C2B4 File Offset: 0x0007A4B4
		[NonEvent]
		public static void DumpBuffer(object thisOrContextObject, byte[] buffer, int offset, int count, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				if (offset < 0 || offset > buffer.Length - count)
				{
					NetEventSource.Fail(thisOrContextObject, FormattableStringFactory.Create("Invalid {0} Args. Length={1}, Offset={2}, Count={3}", new object[] { "DumpBuffer", buffer.Length, offset, count }), memberName);
					return;
				}
				count = Math.Min(count, 1024);
				byte[] array = buffer;
				if (offset != 0 || count != buffer.Length)
				{
					array = new byte[count];
					Buffer.BlockCopy(buffer, offset, array, 0, count);
				}
				NetEventSource.Log.DumpBuffer(NetEventSource.IdOf(thisOrContextObject), memberName, array);
			}
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x0007C354 File Offset: 0x0007A554
		[NonEvent]
		public unsafe static void DumpBuffer(object thisOrContextObject, IntPtr bufferPtr, int count, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				byte[] array = new byte[Math.Min(count, 1024)];
				byte[] array2;
				byte* ptr;
				if ((array2 = array) == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				Buffer.MemoryCopy((void*)bufferPtr, (void*)ptr, (long)array.Length, (long)array.Length);
				array2 = null;
				NetEventSource.Log.DumpBuffer(NetEventSource.IdOf(thisOrContextObject), memberName, array);
			}
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0007C3B9 File Offset: 0x0007A5B9
		[Event(7, Level = EventLevel.Verbose, Keywords = (EventKeywords)2L)]
		private void DumpBuffer(string thisOrContextObject, string memberName, byte[] buffer)
		{
			this.WriteEvent(7, thisOrContextObject, memberName ?? "(?)", buffer);
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0007C3CE File Offset: 0x0007A5CE
		[NonEvent]
		public static void Associate(object first, object second, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Associate(NetEventSource.IdOf(first), memberName, NetEventSource.IdOf(first), NetEventSource.IdOf(second));
			}
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0007C3F4 File Offset: 0x0007A5F4
		[NonEvent]
		public static void Associate(object thisOrContextObject, object first, object second, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Associate(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.IdOf(first), NetEventSource.IdOf(second));
			}
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0007C41A File Offset: 0x0007A61A
		[Event(3, Level = EventLevel.Informational, Keywords = (EventKeywords)1L, Message = "[{2}]<-->[{3}]")]
		private void Associate(string thisOrContextObject, string memberName, string first, string second)
		{
			this.WriteEvent(3, thisOrContextObject, memberName ?? "(?)", first, second);
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x0007C431 File Offset: 0x0007A631
		[Conditional("DEBUG_NETEVENTSOURCE_MISUSE")]
		private static void DebugValidateArg(object arg)
		{
			bool isEnabled = NetEventSource.IsEnabled;
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x000027E8 File Offset: 0x000009E8
		[Conditional("DEBUG_NETEVENTSOURCE_MISUSE")]
		private static void DebugValidateArg(FormattableString arg)
		{
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x0007C439 File Offset: 0x0007A639
		public new static bool IsEnabled
		{
			get
			{
				return NetEventSource.Log.IsEnabled();
			}
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x0007C445 File Offset: 0x0007A645
		[NonEvent]
		public static string IdOf(object value)
		{
			if (value == null)
			{
				return "(null)";
			}
			return value.GetType().Name + "#" + NetEventSource.GetHashCode(value);
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x0007C470 File Offset: 0x0007A670
		[NonEvent]
		public static int GetHashCode(object value)
		{
			if (value == null)
			{
				return 0;
			}
			return value.GetHashCode();
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x0007C480 File Offset: 0x0007A680
		[NonEvent]
		public static object Format(object value)
		{
			if (value == null)
			{
				return "(null)";
			}
			string text = null;
			if (text != null)
			{
				return text;
			}
			Array array = value as Array;
			if (array != null)
			{
				return string.Format("{0}[{1}]", array.GetType().GetElementType(), ((Array)value).Length);
			}
			ICollection collection = value as ICollection;
			if (collection != null)
			{
				return string.Format("{0}({1})", collection.GetType().Name, collection.Count);
			}
			SafeHandle safeHandle = value as SafeHandle;
			if (safeHandle != null)
			{
				return string.Format("{0}:{1}(0x{2:X})", safeHandle.GetType().Name, safeHandle.GetHashCode(), safeHandle.DangerousGetHandle());
			}
			if (value is IntPtr)
			{
				return string.Format("0x{0:X}", value);
			}
			string text2 = value.ToString();
			if (text2 == null || text2 == value.GetType().FullName)
			{
				return NetEventSource.IdOf(value);
			}
			return value;
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x0007C56C File Offset: 0x0007A76C
		[NonEvent]
		private static string Format(FormattableString s)
		{
			switch (s.ArgumentCount)
			{
			case 0:
				return s.Format;
			case 1:
				return string.Format(s.Format, NetEventSource.Format(s.GetArgument(0)));
			case 2:
				return string.Format(s.Format, NetEventSource.Format(s.GetArgument(0)), NetEventSource.Format(s.GetArgument(1)));
			case 3:
				return string.Format(s.Format, NetEventSource.Format(s.GetArgument(0)), NetEventSource.Format(s.GetArgument(1)), NetEventSource.Format(s.GetArgument(2)));
			default:
			{
				object[] arguments = s.GetArguments();
				object[] array = new object[arguments.Length];
				for (int i = 0; i < arguments.Length; i++)
				{
					array[i] = NetEventSource.Format(arguments[i]);
				}
				return string.Format(s.Format, array);
			}
			}
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x0007C640 File Offset: 0x0007A840
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, string arg2, string arg3, string arg4)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				if (arg3 == null)
				{
					arg3 = "";
				}
				if (arg4 == null)
				{
					arg4 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						fixed (string text3 = arg3)
						{
							char* ptr3 = text3;
							if (ptr3 != null)
							{
								ptr3 += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (string text4 = arg4)
							{
								char* ptr4 = text4;
								if (ptr4 != null)
								{
									ptr4 += RuntimeHelpers.OffsetToStringData / 2;
								}
								EventSource.EventData* ptr5;
								checked
								{
									ptr5 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
									ptr5->DataPointer = (IntPtr)((void*)ptr);
								}
								ptr5->Size = (arg1.Length + 1) * 2;
								ptr5[1].DataPointer = (IntPtr)((void*)ptr2);
								ptr5[1].Size = (arg2.Length + 1) * 2;
								ptr5[2].DataPointer = (IntPtr)((void*)ptr3);
								ptr5[2].Size = (arg3.Length + 1) * 2;
								ptr5[3].DataPointer = (IntPtr)((void*)ptr4);
								ptr5[3].Size = (arg4.Length + 1) * 2;
								base.WriteEventCore(eventId, 4, ptr5);
							}
						}
					}
				}
			}
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x0007C7A4 File Offset: 0x0007A9A4
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, string arg2, byte[] arg3)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				if (arg3 == null)
				{
					arg3 = Array.Empty<byte>();
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						byte[] array;
						byte* ptr3;
						if ((array = arg3) == null || array.Length == 0)
						{
							ptr3 = null;
						}
						else
						{
							ptr3 = &array[0];
						}
						int num = arg3.Length;
						EventSource.EventData* ptr4;
						checked
						{
							ptr4 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
							ptr4->DataPointer = (IntPtr)((void*)ptr);
						}
						ptr4->Size = (arg1.Length + 1) * 2;
						ptr4[1].DataPointer = (IntPtr)((void*)ptr2);
						ptr4[1].Size = (arg2.Length + 1) * 2;
						ptr4[2].DataPointer = (IntPtr)((void*)(&num));
						ptr4[2].Size = 4;
						ptr4[3].DataPointer = (IntPtr)((void*)ptr3);
						ptr4[3].Size = num;
						base.WriteEventCore(eventId, 4, ptr4);
						array = null;
					}
				}
			}
		}

		// Token: 0x06001FE9 RID: 8169 RVA: 0x0007C8E0 File Offset: 0x0007AAE0
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, int arg2, int arg3, int arg4)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
						ptr2->DataPointer = (IntPtr)((void*)ptr);
					}
					ptr2->Size = (arg1.Length + 1) * 2;
					ptr2[1].DataPointer = (IntPtr)((void*)(&arg2));
					ptr2[1].Size = 4;
					ptr2[2].DataPointer = (IntPtr)((void*)(&arg3));
					ptr2[2].Size = 4;
					ptr2[3].DataPointer = (IntPtr)((void*)(&arg4));
					ptr2[3].Size = 4;
					base.WriteEventCore(eventId, 4, ptr2);
				}
			}
		}

		// Token: 0x06001FEA RID: 8170 RVA: 0x0007C9BC File Offset: 0x0007ABBC
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, int arg2, string arg3)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg3 == null)
				{
					arg3 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg3)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						EventSource.EventData* ptr3;
						checked
						{
							ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
							ptr3->DataPointer = (IntPtr)((void*)ptr);
						}
						ptr3->Size = (arg1.Length + 1) * 2;
						ptr3[1].DataPointer = (IntPtr)((void*)(&arg2));
						ptr3[1].Size = 4;
						ptr3[2].DataPointer = (IntPtr)((void*)ptr2);
						ptr3[2].Size = (arg3.Length + 1) * 2;
						base.WriteEventCore(eventId, 3, ptr3);
					}
				}
			}
		}

		// Token: 0x06001FEB RID: 8171 RVA: 0x0007CA9C File Offset: 0x0007AC9C
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, string arg2, int arg3)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						EventSource.EventData* ptr3;
						checked
						{
							ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
							ptr3->DataPointer = (IntPtr)((void*)ptr);
						}
						ptr3->Size = (arg1.Length + 1) * 2;
						ptr3[1].DataPointer = (IntPtr)((void*)ptr2);
						ptr3[1].Size = (arg2.Length + 1) * 2;
						ptr3[2].DataPointer = (IntPtr)((void*)(&arg3));
						ptr3[2].Size = 4;
						base.WriteEventCore(eventId, 3, ptr3);
					}
				}
			}
		}

		// Token: 0x06001FEC RID: 8172 RVA: 0x0007CB78 File Offset: 0x0007AD78
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, string arg2, string arg3, int arg4)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				if (arg3 == null)
				{
					arg3 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						fixed (string text3 = arg3)
						{
							char* ptr3 = text3;
							if (ptr3 != null)
							{
								ptr3 += RuntimeHelpers.OffsetToStringData / 2;
							}
							EventSource.EventData* ptr4;
							checked
							{
								ptr4 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
								ptr4->DataPointer = (IntPtr)((void*)ptr);
							}
							ptr4->Size = (arg1.Length + 1) * 2;
							ptr4[1].DataPointer = (IntPtr)((void*)ptr2);
							ptr4[1].Size = (arg2.Length + 1) * 2;
							ptr4[2].DataPointer = (IntPtr)((void*)ptr3);
							ptr4[2].Size = (arg3.Length + 1) * 2;
							ptr4[3].DataPointer = (IntPtr)((void*)(&arg4));
							ptr4[3].Size = 4;
							base.WriteEventCore(eventId, 4, ptr4);
						}
					}
				}
			}
		}

		// Token: 0x04001B96 RID: 7062
		public static readonly NetEventSource Log = new NetEventSource();

		// Token: 0x04001B97 RID: 7063
		private const string MissingMember = "(?)";

		// Token: 0x04001B98 RID: 7064
		private const string NullInstance = "(null)";

		// Token: 0x04001B99 RID: 7065
		private const string StaticMethodObject = "(static)";

		// Token: 0x04001B9A RID: 7066
		private const string NoParameters = "";

		// Token: 0x04001B9B RID: 7067
		private const int MaxDumpSize = 1024;

		// Token: 0x04001B9C RID: 7068
		private const int EnterEventId = 1;

		// Token: 0x04001B9D RID: 7069
		private const int ExitEventId = 2;

		// Token: 0x04001B9E RID: 7070
		private const int AssociateEventId = 3;

		// Token: 0x04001B9F RID: 7071
		private const int InfoEventId = 4;

		// Token: 0x04001BA0 RID: 7072
		private const int ErrorEventId = 5;

		// Token: 0x04001BA1 RID: 7073
		private const int CriticalFailureEventId = 6;

		// Token: 0x04001BA2 RID: 7074
		private const int DumpArrayEventId = 7;

		// Token: 0x04001BA3 RID: 7075
		private const int NextAvailableEventId = 8;

		// Token: 0x02000415 RID: 1045
		public class Keywords
		{
			// Token: 0x04001BA4 RID: 7076
			public const EventKeywords Default = (EventKeywords)1L;

			// Token: 0x04001BA5 RID: 7077
			public const EventKeywords Debug = (EventKeywords)2L;

			// Token: 0x04001BA6 RID: 7078
			public const EventKeywords EnterExit = (EventKeywords)4L;
		}
	}
}
