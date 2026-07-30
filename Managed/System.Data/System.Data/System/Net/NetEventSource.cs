using System;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x02000041 RID: 65
	internal sealed class NetEventSource : EventSource
	{
		// Token: 0x06000239 RID: 569 RVA: 0x0000D5A2 File Offset: 0x0000B7A2
		[NonEvent]
		public static void Enter(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000D5CC File Offset: 0x0000B7CC
		[NonEvent]
		public static void Enter(object thisOrContextObject, object arg0, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("({0})", NetEventSource.Format(arg0)));
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000D5F6 File Offset: 0x0000B7F6
		[NonEvent]
		public static void Enter(object thisOrContextObject, object arg0, object arg1, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("({0}, {1})", NetEventSource.Format(arg0), NetEventSource.Format(arg1)));
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000D626 File Offset: 0x0000B826
		[NonEvent]
		public static void Enter(object thisOrContextObject, object arg0, object arg1, object arg2, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("({0}, {1}, {2})", NetEventSource.Format(arg0), NetEventSource.Format(arg1), NetEventSource.Format(arg2)));
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000D65D File Offset: 0x0000B85D
		[Event(1, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void Enter(string thisOrContextObject, string memberName, string parameters)
		{
			base.WriteEvent(1, thisOrContextObject, memberName ?? "(?)", parameters);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000D672 File Offset: 0x0000B872
		[NonEvent]
		public static void Exit(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Exit(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000D69C File Offset: 0x0000B89C
		[NonEvent]
		public static void Exit(object thisOrContextObject, object arg0, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Exit(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(arg0).ToString());
			}
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000D6C1 File Offset: 0x0000B8C1
		[NonEvent]
		public static void Exit(object thisOrContextObject, object arg0, object arg1, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Exit(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("{0}, {1}", NetEventSource.Format(arg0), NetEventSource.Format(arg1)));
			}
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000D6F1 File Offset: 0x0000B8F1
		[Event(2, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void Exit(string thisOrContextObject, string memberName, string result)
		{
			base.WriteEvent(2, thisOrContextObject, memberName ?? "(?)", result);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000D706 File Offset: 0x0000B906
		[NonEvent]
		public static void Info(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Info(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000D730 File Offset: 0x0000B930
		[NonEvent]
		public static void Info(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Info(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000D755 File Offset: 0x0000B955
		[Event(4, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void Info(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(4, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000D76A File Offset: 0x0000B96A
		[NonEvent]
		public static void Error(object thisOrContextObject, FormattableString formattableString, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.ErrorMessage(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(formattableString));
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000D78A File Offset: 0x0000B98A
		[NonEvent]
		public static void Error(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.ErrorMessage(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000D7AF File Offset: 0x0000B9AF
		[Event(5, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		private void ErrorMessage(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(5, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000D7C4 File Offset: 0x0000B9C4
		[NonEvent]
		public static void Fail(object thisOrContextObject, FormattableString formattableString, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.CriticalFailure(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(formattableString));
			}
		}

		// Token: 0x06000249 RID: 585 RVA: 0x0000D7E4 File Offset: 0x0000B9E4
		[NonEvent]
		public static void Fail(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.CriticalFailure(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000D809 File Offset: 0x0000BA09
		[Event(6, Level = EventLevel.Critical, Keywords = (EventKeywords)2L)]
		private void CriticalFailure(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(6, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000D81E File Offset: 0x0000BA1E
		[NonEvent]
		public static void DumpBuffer(object thisOrContextObject, byte[] buffer, [CallerMemberName] string memberName = null)
		{
			NetEventSource.DumpBuffer(thisOrContextObject, buffer, 0, buffer.Length, memberName);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0000D82C File Offset: 0x0000BA2C
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

		// Token: 0x0600024D RID: 589 RVA: 0x0000D8CC File Offset: 0x0000BACC
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

		// Token: 0x0600024E RID: 590 RVA: 0x0000D931 File Offset: 0x0000BB31
		[Event(7, Level = EventLevel.Verbose, Keywords = (EventKeywords)2L)]
		private void DumpBuffer(string thisOrContextObject, string memberName, byte[] buffer)
		{
			this.WriteEvent(7, thisOrContextObject, memberName ?? "(?)", buffer);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000D946 File Offset: 0x0000BB46
		[NonEvent]
		public static void Associate(object first, object second, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Associate(NetEventSource.IdOf(first), memberName, NetEventSource.IdOf(first), NetEventSource.IdOf(second));
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000D96C File Offset: 0x0000BB6C
		[NonEvent]
		public static void Associate(object thisOrContextObject, object first, object second, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Associate(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.IdOf(first), NetEventSource.IdOf(second));
			}
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000D992 File Offset: 0x0000BB92
		[Event(3, Level = EventLevel.Informational, Keywords = (EventKeywords)1L, Message = "[{2}]<-->[{3}]")]
		private void Associate(string thisOrContextObject, string memberName, string first, string second)
		{
			this.WriteEvent(3, thisOrContextObject, memberName ?? "(?)", first, second);
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000D9A9 File Offset: 0x0000BBA9
		[Conditional("DEBUG_NETEVENTSOURCE_MISUSE")]
		private static void DebugValidateArg(object arg)
		{
			bool isEnabled = NetEventSource.IsEnabled;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00005E03 File Offset: 0x00004003
		[Conditional("DEBUG_NETEVENTSOURCE_MISUSE")]
		private static void DebugValidateArg(FormattableString arg)
		{
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000254 RID: 596 RVA: 0x0000D9B1 File Offset: 0x0000BBB1
		public new static bool IsEnabled
		{
			get
			{
				return NetEventSource.Log.IsEnabled();
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000D9BD File Offset: 0x0000BBBD
		[NonEvent]
		public static string IdOf(object value)
		{
			if (value == null)
			{
				return "(null)";
			}
			return value.GetType().Name + "#" + NetEventSource.GetHashCode(value);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000D9E8 File Offset: 0x0000BBE8
		[NonEvent]
		public static int GetHashCode(object value)
		{
			if (value == null)
			{
				return 0;
			}
			return value.GetHashCode();
		}

		// Token: 0x06000257 RID: 599 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
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

		// Token: 0x06000258 RID: 600 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
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

		// Token: 0x06000259 RID: 601 RVA: 0x0000DBB8 File Offset: 0x0000BDB8
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

		// Token: 0x0600025A RID: 602 RVA: 0x0000DD1C File Offset: 0x0000BF1C
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

		// Token: 0x0600025B RID: 603 RVA: 0x0000DE58 File Offset: 0x0000C058
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

		// Token: 0x0600025C RID: 604 RVA: 0x0000DF34 File Offset: 0x0000C134
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

		// Token: 0x0600025D RID: 605 RVA: 0x0000E014 File Offset: 0x0000C214
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

		// Token: 0x0600025E RID: 606 RVA: 0x0000E0F0 File Offset: 0x0000C2F0
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

		// Token: 0x04000478 RID: 1144
		public static readonly NetEventSource Log = new NetEventSource();

		// Token: 0x04000479 RID: 1145
		private const string MissingMember = "(?)";

		// Token: 0x0400047A RID: 1146
		private const string NullInstance = "(null)";

		// Token: 0x0400047B RID: 1147
		private const string StaticMethodObject = "(static)";

		// Token: 0x0400047C RID: 1148
		private const string NoParameters = "";

		// Token: 0x0400047D RID: 1149
		private const int MaxDumpSize = 1024;

		// Token: 0x0400047E RID: 1150
		private const int EnterEventId = 1;

		// Token: 0x0400047F RID: 1151
		private const int ExitEventId = 2;

		// Token: 0x04000480 RID: 1152
		private const int AssociateEventId = 3;

		// Token: 0x04000481 RID: 1153
		private const int InfoEventId = 4;

		// Token: 0x04000482 RID: 1154
		private const int ErrorEventId = 5;

		// Token: 0x04000483 RID: 1155
		private const int CriticalFailureEventId = 6;

		// Token: 0x04000484 RID: 1156
		private const int DumpArrayEventId = 7;

		// Token: 0x04000485 RID: 1157
		private const int NextAvailableEventId = 8;

		// Token: 0x02000042 RID: 66
		public class Keywords
		{
			// Token: 0x04000486 RID: 1158
			public const EventKeywords Default = (EventKeywords)1L;

			// Token: 0x04000487 RID: 1159
			public const EventKeywords Debug = (EventKeywords)2L;

			// Token: 0x04000488 RID: 1160
			public const EventKeywords EnterExit = (EventKeywords)4L;
		}
	}
}
