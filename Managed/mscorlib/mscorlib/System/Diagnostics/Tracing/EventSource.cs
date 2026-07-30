using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Reflection;
using Microsoft.Win32;

namespace System.Diagnostics.Tracing
{
	/// <summary>Provides the ability to create events for event tracing for Windows (ETW).</summary>
	// Token: 0x02000AE6 RID: 2790
	public class EventSource : IDisposable
	{
		// Token: 0x06006431 RID: 25649 RVA: 0x00144A72 File Offset: 0x00142C72
		public EventSource(string eventSourceName)
			: this(eventSourceName, EventSourceSettings.EtwSelfDescribingEventFormat)
		{
		}

		// Token: 0x06006432 RID: 25650 RVA: 0x00144A7C File Offset: 0x00142C7C
		public EventSource(string eventSourceName, EventSourceSettings config)
			: this(eventSourceName, config, null)
		{
		}

		// Token: 0x06006433 RID: 25651 RVA: 0x00144A88 File Offset: 0x00142C88
		public EventSource(string eventSourceName, EventSourceSettings config, params string[] traits)
			: this((eventSourceName == null) ? default(Guid) : EventSource.GenerateGuidFromName(eventSourceName.ToUpperInvariant()), eventSourceName, config, traits)
		{
			if (eventSourceName == null)
			{
				throw new ArgumentNullException("eventSourceName");
			}
		}

		// Token: 0x06006434 RID: 25652 RVA: 0x00144AC8 File Offset: 0x00142CC8
		[SecuritySafeCritical]
		public void Write(string eventName)
		{
			if (eventName == null)
			{
				throw new ArgumentNullException("eventName");
			}
			if (!this.IsEnabled())
			{
				return;
			}
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.WriteImpl<EmptyStruct>(eventName, ref eventSourceOptions, ref emptyStruct, null, null);
		}

		// Token: 0x06006435 RID: 25653 RVA: 0x00144B0C File Offset: 0x00142D0C
		[SecuritySafeCritical]
		public void Write(string eventName, EventSourceOptions options)
		{
			if (eventName == null)
			{
				throw new ArgumentNullException("eventName");
			}
			if (!this.IsEnabled())
			{
				return;
			}
			EmptyStruct emptyStruct = default(EmptyStruct);
			this.WriteImpl<EmptyStruct>(eventName, ref options, ref emptyStruct, null, null);
		}

		// Token: 0x06006436 RID: 25654 RVA: 0x00144B48 File Offset: 0x00142D48
		[SecuritySafeCritical]
		public void Write<T>(string eventName, T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			this.WriteImpl<T>(eventName, ref eventSourceOptions, ref data, null, null);
		}

		// Token: 0x06006437 RID: 25655 RVA: 0x00144B75 File Offset: 0x00142D75
		[SecuritySafeCritical]
		public void Write<T>(string eventName, EventSourceOptions options, T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			this.WriteImpl<T>(eventName, ref options, ref data, null, null);
		}

		// Token: 0x06006438 RID: 25656 RVA: 0x00144B8F File Offset: 0x00142D8F
		[SecuritySafeCritical]
		public void Write<T>(string eventName, ref EventSourceOptions options, ref T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			this.WriteImpl<T>(eventName, ref options, ref data, null, null);
		}

		// Token: 0x06006439 RID: 25657 RVA: 0x00144BA8 File Offset: 0x00142DA8
		[SecuritySafeCritical]
		public unsafe void Write<T>(string eventName, ref EventSourceOptions options, ref Guid activityId, ref Guid relatedActivityId, ref T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			fixed (Guid* ptr = &activityId)
			{
				Guid* ptr2 = ptr;
				fixed (Guid* ptr3 = &relatedActivityId)
				{
					Guid* ptr4 = ptr3;
					this.WriteImpl<T>(eventName, ref options, ref data, ptr2, (relatedActivityId == Guid.Empty) ? null : ptr4);
					ptr = null;
				}
				return;
			}
		}

		// Token: 0x0600643A RID: 25658 RVA: 0x00144BF4 File Offset: 0x00142DF4
		[SecuritySafeCritical]
		private unsafe void WriteMultiMerge(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, params object[] values)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			byte b = (((options.valuesSet & 4) != 0) ? options.level : eventTypes.level);
			EventKeywords eventKeywords = (((options.valuesSet & 1) != 0) ? options.keywords : eventTypes.keywords);
			if (this.IsEnabled((EventLevel)b, eventKeywords))
			{
				this.WriteMultiMergeInner(eventName, ref options, eventTypes, activityID, childActivityID, values);
			}
		}

		// Token: 0x0600643B RID: 25659 RVA: 0x00144C58 File Offset: 0x00142E58
		[SecuritySafeCritical]
		private unsafe void WriteMultiMergeInner(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, params object[] values)
		{
			byte b = (((options.valuesSet & 4) != 0) ? options.level : eventTypes.level);
			byte b2 = (((options.valuesSet & 8) != 0) ? options.opcode : eventTypes.opcode);
			EventTags eventTags = (((options.valuesSet & 2) != 0) ? options.tags : eventTypes.Tags);
			EventKeywords eventKeywords = (((options.valuesSet & 1) != 0) ? options.keywords : eventTypes.keywords);
			NameInfo nameInfo = eventTypes.GetNameInfo(eventName ?? eventTypes.Name, eventTags);
			if (nameInfo == null)
			{
				return;
			}
			int identity = nameInfo.identity;
			EventDescriptor eventDescriptor = new EventDescriptor(identity, b, b2, (long)eventKeywords);
			int pinCount = eventTypes.pinCount;
			byte* ptr = stackalloc byte[(UIntPtr)eventTypes.scratchSize];
			EventSource.EventData* ptr2;
			GCHandle* ptr3;
			byte[] array;
			byte[] array2;
			byte* ptr5;
			byte[] array3;
			byte* ptr6;
			checked
			{
				ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)(eventTypes.dataCount + 3)) * (UIntPtr)sizeof(EventSource.EventData)];
				ptr3 = stackalloc GCHandle[unchecked((UIntPtr)pinCount) * (UIntPtr)sizeof(GCHandle)];
				byte* ptr4;
				if ((array = this.providerMetadata) == null || array.Length == 0)
				{
					ptr4 = null;
				}
				else
				{
					ptr4 = &array[0];
				}
				if ((array2 = nameInfo.nameMetadata) == null || array2.Length == 0)
				{
					ptr5 = null;
				}
				else
				{
					ptr5 = &array2[0];
				}
				if ((array3 = eventTypes.typeMetadata) == null || array3.Length == 0)
				{
					ptr6 = null;
				}
				else
				{
					ptr6 = &array3[0];
				}
				ptr2->SetMetadata(ptr4, this.providerMetadata.Length, 2);
			}
			ptr2[1].SetMetadata(ptr5, nameInfo.nameMetadata.Length, 1);
			ptr2[2].SetMetadata(ptr6, eventTypes.typeMetadata.Length, 1);
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				DataCollector.ThreadInstance.Enable(ptr, eventTypes.scratchSize, ptr2 + 3, eventTypes.dataCount, ptr3, pinCount);
				for (int i = 0; i < eventTypes.typeInfos.Length; i++)
				{
					eventTypes.typeInfos[i].WriteObjectData(TraceLoggingDataCollector.Instance, values[i]);
				}
				this.WriteEventRaw(eventName, ref eventDescriptor, activityID, childActivityID, (int)((long)(DataCollector.ThreadInstance.Finish() - ptr2)), (IntPtr)((void*)ptr2));
			}
			finally
			{
				this.WriteCleanup(ptr3, pinCount);
			}
			array = null;
			array2 = null;
			array3 = null;
		}

		// Token: 0x0600643C RID: 25660 RVA: 0x00144E8C File Offset: 0x0014308C
		[SecuritySafeCritical]
		internal unsafe void WriteMultiMerge(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, EventSource.EventData* data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			fixed (EventSourceOptions* ptr = &options)
			{
				EventDescriptor eventDescriptor;
				NameInfo nameInfo = this.UpdateDescriptor(eventName, eventTypes, ref options, out eventDescriptor);
				if (nameInfo == null)
				{
					return;
				}
				EventSource.EventData* ptr2;
				byte[] array;
				byte[] array2;
				byte* ptr4;
				byte[] array3;
				byte* ptr5;
				checked
				{
					ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)(eventTypes.dataCount + eventTypes.typeInfos.Length * 2 + 3)) * (UIntPtr)sizeof(EventSource.EventData)];
					byte* ptr3;
					if ((array = this.providerMetadata) == null || array.Length == 0)
					{
						ptr3 = null;
					}
					else
					{
						ptr3 = &array[0];
					}
					if ((array2 = nameInfo.nameMetadata) == null || array2.Length == 0)
					{
						ptr4 = null;
					}
					else
					{
						ptr4 = &array2[0];
					}
					if ((array3 = eventTypes.typeMetadata) == null || array3.Length == 0)
					{
						ptr5 = null;
					}
					else
					{
						ptr5 = &array3[0];
					}
					ptr2->SetMetadata(ptr3, this.providerMetadata.Length, 2);
				}
				ptr2[1].SetMetadata(ptr4, nameInfo.nameMetadata.Length, 1);
				ptr2[2].SetMetadata(ptr5, eventTypes.typeMetadata.Length, 1);
				int num = 3;
				for (int i = 0; i < eventTypes.typeInfos.Length; i++)
				{
					if (eventTypes.typeInfos[i].DataType == typeof(string))
					{
						ptr2[num].m_Ptr = &ptr2[num + 1].m_Size;
						ptr2[num].m_Size = 2;
						num++;
						ptr2[num].m_Ptr = data[i].m_Ptr;
						ptr2[num].m_Size = data[i].m_Size - 2;
						num++;
					}
					else
					{
						ptr2[num].m_Ptr = data[i].m_Ptr;
						ptr2[num].m_Size = data[i].m_Size;
						if (data[i].m_Size == 4 && eventTypes.typeInfos[i].DataType == typeof(bool))
						{
							ptr2[num].m_Size = 1;
						}
						num++;
					}
				}
				this.WriteEventRaw(eventName, ref eventDescriptor, activityID, childActivityID, num, (IntPtr)((void*)ptr2));
				array = null;
				array2 = null;
				array3 = null;
			}
		}

		// Token: 0x0600643D RID: 25661 RVA: 0x00145104 File Offset: 0x00143304
		[SecuritySafeCritical]
		private unsafe void WriteImpl<T>(string eventName, ref EventSourceOptions options, ref T data, Guid* pActivityId, Guid* pRelatedActivityId)
		{
			try
			{
				SimpleEventTypes<T> instance = SimpleEventTypes<T>.Instance;
				try
				{
					fixed (EventSourceOptions* ptr = &options)
					{
						options.Opcode = (options.IsOpcodeSet ? options.Opcode : EventSource.GetOpcodeWithDefault(options.Opcode, eventName));
						EventDescriptor eventDescriptor;
						NameInfo nameInfo = this.UpdateDescriptor(eventName, instance, ref options, out eventDescriptor);
						if (nameInfo != null)
						{
							int pinCount = instance.pinCount;
							byte* ptr2 = stackalloc byte[(UIntPtr)instance.scratchSize];
							EventSource.EventData* ptr3;
							GCHandle* ptr4;
							checked
							{
								ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)(instance.dataCount + 3)) * (UIntPtr)sizeof(EventSource.EventData)];
								ptr4 = stackalloc GCHandle[unchecked((UIntPtr)pinCount) * (UIntPtr)sizeof(GCHandle)];
							}
							try
							{
								byte[] array;
								byte* ptr5;
								if ((array = this.providerMetadata) == null || array.Length == 0)
								{
									ptr5 = null;
								}
								else
								{
									ptr5 = &array[0];
								}
								byte[] array2;
								byte* ptr6;
								if ((array2 = nameInfo.nameMetadata) == null || array2.Length == 0)
								{
									ptr6 = null;
								}
								else
								{
									ptr6 = &array2[0];
								}
								byte[] array3;
								byte* ptr7;
								if ((array3 = instance.typeMetadata) == null || array3.Length == 0)
								{
									ptr7 = null;
								}
								else
								{
									ptr7 = &array3[0];
								}
								ptr3->SetMetadata(ptr5, this.providerMetadata.Length, 2);
								ptr3[1].SetMetadata(ptr6, nameInfo.nameMetadata.Length, 1);
								ptr3[2].SetMetadata(ptr7, instance.typeMetadata.Length, 1);
								RuntimeHelpers.PrepareConstrainedRegions();
								EventOpcode opcode = (EventOpcode)eventDescriptor.Opcode;
								Guid empty = Guid.Empty;
								Guid empty2 = Guid.Empty;
								if (pActivityId == null && pRelatedActivityId == null && (options.ActivityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
								{
									if (opcode == EventOpcode.Start)
									{
										this.m_activityTracker.OnStart(this.m_name, eventName, 0, ref empty, ref empty2, options.ActivityOptions);
									}
									else if (opcode == EventOpcode.Stop)
									{
										this.m_activityTracker.OnStop(this.m_name, eventName, 0, ref empty);
									}
									if (empty != Guid.Empty)
									{
										pActivityId = &empty;
									}
									if (empty2 != Guid.Empty)
									{
										pRelatedActivityId = &empty2;
									}
								}
								try
								{
									DataCollector.ThreadInstance.Enable(ptr2, instance.scratchSize, ptr3 + 3, instance.dataCount, ptr4, pinCount);
									instance.typeInfo.WriteData(TraceLoggingDataCollector.Instance, ref data);
									this.WriteEventRaw(eventName, ref eventDescriptor, pActivityId, pRelatedActivityId, (int)((long)(DataCollector.ThreadInstance.Finish() - ptr3)), (IntPtr)((void*)ptr3));
									if (this.m_Dispatchers != null)
									{
										EventPayload eventPayload = (EventPayload)instance.typeInfo.GetData(data);
										this.WriteToAllListeners(eventName, ref eventDescriptor, nameInfo.tags, pActivityId, eventPayload);
									}
								}
								catch (Exception ex)
								{
									if (ex is EventSourceException)
									{
										throw;
									}
									this.ThrowEventSourceException(eventName, ex);
								}
								finally
								{
									this.WriteCleanup(ptr4, pinCount);
								}
							}
							finally
							{
								byte[] array = null;
								byte[] array2 = null;
								byte[] array3 = null;
							}
						}
					}
				}
				finally
				{
					EventSourceOptions* ptr = null;
				}
			}
			catch (Exception ex2)
			{
				if (ex2 is EventSourceException)
				{
					throw;
				}
				this.ThrowEventSourceException(eventName, ex2);
			}
		}

		// Token: 0x0600643E RID: 25662 RVA: 0x00145438 File Offset: 0x00143638
		[SecurityCritical]
		private unsafe void WriteToAllListeners(string eventName, ref EventDescriptor eventDescriptor, EventTags tags, Guid* pActivityId, EventPayload payload)
		{
			EventWrittenEventArgs eventWrittenEventArgs = new EventWrittenEventArgs(this);
			eventWrittenEventArgs.EventName = eventName;
			eventWrittenEventArgs.m_keywords = (EventKeywords)eventDescriptor.Keywords;
			eventWrittenEventArgs.m_opcode = (EventOpcode)eventDescriptor.Opcode;
			eventWrittenEventArgs.m_tags = tags;
			eventWrittenEventArgs.EventId = -1;
			if (pActivityId != null)
			{
				eventWrittenEventArgs.RelatedActivityId = *pActivityId;
			}
			if (payload != null)
			{
				eventWrittenEventArgs.Payload = new ReadOnlyCollection<object>((IList<object>)payload.Values);
				eventWrittenEventArgs.PayloadNames = new ReadOnlyCollection<string>((IList<string>)payload.Keys);
			}
			this.DispatchToAllListeners(-1, pActivityId, eventWrittenEventArgs);
		}

		// Token: 0x0600643F RID: 25663 RVA: 0x001454C8 File Offset: 0x001436C8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SecurityCritical]
		[NonEvent]
		private unsafe void WriteCleanup(GCHandle* pPins, int cPins)
		{
			DataCollector.ThreadInstance.Disable();
			for (int num = 0; num != cPins; num++)
			{
				if (IntPtr.Zero != (IntPtr)pPins[num])
				{
					pPins[num].Free();
				}
			}
		}

		// Token: 0x06006440 RID: 25664 RVA: 0x0014551C File Offset: 0x0014371C
		private void InitializeProviderMetadata()
		{
			if (this.m_traits != null)
			{
				List<byte> list = new List<byte>(100);
				for (int i = 0; i < this.m_traits.Length - 1; i += 2)
				{
					if (this.m_traits[i].StartsWith("ETW_"))
					{
						string text = this.m_traits[i].Substring(4);
						byte b;
						if (!byte.TryParse(text, out b))
						{
							if (!(text == "GROUP"))
							{
								throw new ArgumentException(Environment.GetResourceString("UnknownEtwTrait", new object[] { text }), "traits");
							}
							b = 1;
						}
						string text2 = this.m_traits[i + 1];
						int count = list.Count;
						list.Add(0);
						list.Add(0);
						list.Add(b);
						int num = EventSource.AddValueToMetaData(list, text2) + 3;
						list[count] = (byte)num;
						list[count + 1] = (byte)(num >> 8);
					}
				}
				this.providerMetadata = Statics.MetadataForString(this.Name, 0, list.Count, 0);
				int num2 = this.providerMetadata.Length - list.Count;
				using (List<byte>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						byte b2 = enumerator.Current;
						this.providerMetadata[num2++] = b2;
					}
					return;
				}
			}
			this.providerMetadata = Statics.MetadataForString(this.Name, 0, 0, 0);
		}

		// Token: 0x06006441 RID: 25665 RVA: 0x00145690 File Offset: 0x00143890
		private static int AddValueToMetaData(List<byte> metaData, string value)
		{
			if (value.Length == 0)
			{
				return 0;
			}
			int count = metaData.Count;
			char c = value[0];
			if (c == '@')
			{
				metaData.AddRange(Encoding.UTF8.GetBytes(value.Substring(1)));
			}
			else if (c == '{')
			{
				metaData.AddRange(new Guid(value).ToByteArray());
			}
			else if (c == '#')
			{
				for (int i = 1; i < value.Length; i++)
				{
					if (value[i] != ' ')
					{
						if (i + 1 >= value.Length)
						{
							throw new ArgumentException(Environment.GetResourceString("EvenHexDigits"), "traits");
						}
						metaData.Add((byte)(EventSource.HexDigit(value[i]) * 16 + EventSource.HexDigit(value[i + 1])));
						i++;
					}
				}
			}
			else
			{
				if (' ' > c)
				{
					throw new ArgumentException(Environment.GetResourceString("IllegalValue", new object[] { value }), "traits");
				}
				metaData.AddRange(Encoding.UTF8.GetBytes(value));
			}
			return metaData.Count - count;
		}

		// Token: 0x06006442 RID: 25666 RVA: 0x001457A0 File Offset: 0x001439A0
		private static int HexDigit(char c)
		{
			if ('0' <= c && c <= '9')
			{
				return (int)(c - '0');
			}
			if ('a' <= c)
			{
				c -= ' ';
			}
			if ('A' <= c && c <= 'F')
			{
				return (int)(c - 'A' + '\n');
			}
			throw new ArgumentException(Environment.GetResourceString("BadHexDigit", new object[] { c }), "traits");
		}

		// Token: 0x06006443 RID: 25667 RVA: 0x00145800 File Offset: 0x00143A00
		private NameInfo UpdateDescriptor(string name, TraceLoggingEventTypes eventInfo, ref EventSourceOptions options, out EventDescriptor descriptor)
		{
			NameInfo nameInfo = null;
			int num = 0;
			byte b = (((options.valuesSet & 4) != 0) ? options.level : eventInfo.level);
			byte b2 = (((options.valuesSet & 8) != 0) ? options.opcode : eventInfo.opcode);
			EventTags eventTags = (((options.valuesSet & 2) != 0) ? options.tags : eventInfo.Tags);
			EventKeywords eventKeywords = (((options.valuesSet & 1) != 0) ? options.keywords : eventInfo.keywords);
			if (this.IsEnabled((EventLevel)b, eventKeywords))
			{
				nameInfo = eventInfo.GetNameInfo(name ?? eventInfo.Name, eventTags);
				num = nameInfo.identity;
			}
			descriptor = new EventDescriptor(num, b, b2, (long)eventKeywords);
			return nameInfo;
		}

		/// <summary>The friendly name of the class that is derived from the event source.</summary>
		/// <returns>The friendly name of the derived class.  The default is the simple name of the class.</returns>
		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06006444 RID: 25668 RVA: 0x001458AF File Offset: 0x00143AAF
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		/// <summary>The unique identifier for the event source.</summary>
		/// <returns>A unique identifier for the event source.</returns>
		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06006445 RID: 25669 RVA: 0x001458B7 File Offset: 0x00143AB7
		public Guid Guid
		{
			get
			{
				return this.m_guid;
			}
		}

		/// <summary>Determines whether the current event source is enabled.</summary>
		/// <returns>true if the current event source is enabled; otherwise, false.</returns>
		// Token: 0x06006446 RID: 25670 RVA: 0x001458BF File Offset: 0x00143ABF
		public bool IsEnabled()
		{
			return this.m_eventSourceEnabled;
		}

		/// <summary>Determines whether the current event source that has the specified level and keyword is enabled.</summary>
		/// <returns>true if the event source is enabled; otherwise, false.</returns>
		/// <param name="level">The level of the event source.</param>
		/// <param name="keywords">The keyword of the event source.</param>
		// Token: 0x06006447 RID: 25671 RVA: 0x001458C7 File Offset: 0x00143AC7
		public bool IsEnabled(EventLevel level, EventKeywords keywords)
		{
			return this.IsEnabled(level, keywords, EventChannel.None);
		}

		// Token: 0x06006448 RID: 25672 RVA: 0x001458D2 File Offset: 0x00143AD2
		public bool IsEnabled(EventLevel level, EventKeywords keywords, EventChannel channel)
		{
			return this.m_eventSourceEnabled && this.IsEnabledCommon(this.m_eventSourceEnabled, this.m_level, this.m_matchAnyKeyword, level, keywords, channel);
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06006449 RID: 25673 RVA: 0x001458FE File Offset: 0x00143AFE
		public EventSourceSettings Settings
		{
			get
			{
				return this.m_config;
			}
		}

		/// <summary>Gets the unique identifier for this implementation of the event source.</summary>
		/// <returns>A unique identifier for this event source type.</returns>
		/// <param name="eventSourceType">The type of the event source.</param>
		// Token: 0x0600644A RID: 25674 RVA: 0x00145908 File Offset: 0x00143B08
		public static Guid GetGuid(Type eventSourceType)
		{
			if (eventSourceType == null)
			{
				throw new ArgumentNullException("eventSourceType");
			}
			EventSourceAttribute eventSourceAttribute = (EventSourceAttribute)EventSource.GetCustomAttributeHelper(eventSourceType, typeof(EventSourceAttribute), EventManifestOptions.None);
			string text = eventSourceType.Name;
			if (eventSourceAttribute != null)
			{
				if (eventSourceAttribute.Guid != null)
				{
					Guid empty = Guid.Empty;
					if (Guid.TryParse(eventSourceAttribute.Guid, out empty))
					{
						return empty;
					}
				}
				if (eventSourceAttribute.Name != null)
				{
					text = eventSourceAttribute.Name;
				}
			}
			if (text == null)
			{
				throw new ArgumentException(Environment.GetResourceString("The name of the type is invalid."), "eventSourceType");
			}
			return EventSource.GenerateGuidFromName(text.ToUpperInvariant());
		}

		/// <summary>Gets the friendly name of the event source.</summary>
		/// <returns>The friendly name of the event source. The default is the simple name of the class.</returns>
		/// <param name="eventSourceType">The type of the event source.</param>
		// Token: 0x0600644B RID: 25675 RVA: 0x0014599B File Offset: 0x00143B9B
		public static string GetName(Type eventSourceType)
		{
			return EventSource.GetName(eventSourceType, EventManifestOptions.None);
		}

		/// <summary>Returns a string of the XML manifest that is associated with the current event source.</summary>
		/// <returns>The XML data string.</returns>
		/// <param name="eventSourceType">The type of the event source.</param>
		/// <param name="assemblyPathToIncludeInManifest">The path to the .dll file to include in the manifest. </param>
		// Token: 0x0600644C RID: 25676 RVA: 0x001459A4 File Offset: 0x00143BA4
		public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest)
		{
			return EventSource.GenerateManifest(eventSourceType, assemblyPathToIncludeInManifest, EventManifestOptions.None);
		}

		// Token: 0x0600644D RID: 25677 RVA: 0x001459B0 File Offset: 0x00143BB0
		public static string GenerateManifest(Type eventSourceType, string assemblyPathToIncludeInManifest, EventManifestOptions flags)
		{
			if (eventSourceType == null)
			{
				throw new ArgumentNullException("eventSourceType");
			}
			byte[] array = EventSource.CreateManifestAndDescriptors(eventSourceType, assemblyPathToIncludeInManifest, null, flags);
			if (array != null)
			{
				return Encoding.UTF8.GetString(array, 0, array.Length);
			}
			return null;
		}

		/// <summary>Gets a snapshot of all the event sources for the application domain.</summary>
		/// <returns>An enumeration of all the event sources in the application domain.</returns>
		// Token: 0x0600644E RID: 25678 RVA: 0x001459F0 File Offset: 0x00143BF0
		public static IEnumerable<EventSource> GetSources()
		{
			List<EventSource> list = new List<EventSource>();
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null && !eventSource.IsDisposed)
					{
						list.Add(eventSource);
					}
				}
			}
			return list;
		}

		/// <summary>Sends a command to a specified event source.</summary>
		/// <param name="eventSource">The event source to send the command to.</param>
		/// <param name="command">The event command to send.</param>
		/// <param name="commandArguments">The arguments for the event command.</param>
		// Token: 0x0600644F RID: 25679 RVA: 0x00145A88 File Offset: 0x00143C88
		public static void SendCommand(EventSource eventSource, EventCommand command, IDictionary<string, string> commandArguments)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			if (command <= EventCommand.Update && command != EventCommand.SendManifest)
			{
				throw new ArgumentException(Environment.GetResourceString("Invalid command value."), "command");
			}
			eventSource.SendCommand(null, 0, 0, command, true, EventLevel.LogAlways, EventKeywords.None, commandArguments);
		}

		/// <summary>Sets the activity ID on the current thread.</summary>
		/// <param name="activityId">The current thread's new activity ID, or <see cref="F:System.Guid.Empty" /> to indicate that work on the current thread is not associated with any activity. </param>
		// Token: 0x06006450 RID: 25680 RVA: 0x00145AD0 File Offset: 0x00143CD0
		[SecuritySafeCritical]
		public static void SetCurrentThreadActivityId(Guid activityId)
		{
			Guid guid = activityId;
			if (UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_SET_ID, ref activityId) == 0)
			{
				Action<Guid> action = EventSource.s_activityDying;
				if (action != null && guid != activityId)
				{
					if (activityId == Guid.Empty)
					{
						activityId = EventSource.FallbackActivityId;
					}
					action(activityId);
				}
			}
			if (TplEtwProvider.Log != null)
			{
				TplEtwProvider.Log.SetActivityId(activityId);
			}
		}

		/// <summary>Sets the activity ID on the current thread, and returns the previous activity ID.</summary>
		/// <param name="activityId">The current thread's new activity ID, or <see cref="F:System.Guid.Empty" /> to indicate that work on the current thread is not associated with any activity.</param>
		/// <param name="oldActivityThatWillContinue">When this method returns, contains the previous activity ID on the current thread. </param>
		// Token: 0x06006451 RID: 25681 RVA: 0x00145B28 File Offset: 0x00143D28
		[SecuritySafeCritical]
		public static void SetCurrentThreadActivityId(Guid activityId, out Guid oldActivityThatWillContinue)
		{
			oldActivityThatWillContinue = activityId;
			UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_SET_ID, ref oldActivityThatWillContinue);
			if (TplEtwProvider.Log != null)
			{
				TplEtwProvider.Log.SetActivityId(activityId);
			}
		}

		/// <summary>Gets the activity ID of the current thread. </summary>
		/// <returns>The activity ID of the current thread. </returns>
		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06006452 RID: 25682 RVA: 0x00145B4C File Offset: 0x00143D4C
		public static Guid CurrentThreadActivityId
		{
			[SecuritySafeCritical]
			get
			{
				Guid guid = default(Guid);
				UnsafeNativeMethods.ManifestEtw.EventActivityIdControl(UnsafeNativeMethods.ManifestEtw.ActivityControl.EVENT_ACTIVITY_CTRL_GET_ID, ref guid);
				return guid;
			}
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06006453 RID: 25683 RVA: 0x00145B6C File Offset: 0x00143D6C
		internal static Guid InternalCurrentThreadActivityId
		{
			[SecurityCritical]
			get
			{
				Guid guid = EventSource.CurrentThreadActivityId;
				if (guid == Guid.Empty)
				{
					guid = EventSource.FallbackActivityId;
				}
				return guid;
			}
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x06006454 RID: 25684 RVA: 0x00145B94 File Offset: 0x00143D94
		internal static Guid FallbackActivityId
		{
			[SecurityCritical]
			get
			{
				return new Guid((uint)AppDomain.GetCurrentThreadId(), (ushort)EventSource.s_currentPid, (ushort)(EventSource.s_currentPid >> 16), 148, 27, 135, 213, 166, 92, 54, 100);
			}
		}

		/// <summary>Gets any exception that was thrown during the construction of the event source. </summary>
		/// <returns>The exception that was thrown during the construction of the event source, or null if no exception was thrown. </returns>
		// Token: 0x170011DD RID: 4573
		// (get) Token: 0x06006455 RID: 25685 RVA: 0x00145BD6 File Offset: 0x00143DD6
		public Exception ConstructionException
		{
			get
			{
				return this.m_constructionException;
			}
		}

		// Token: 0x06006456 RID: 25686 RVA: 0x00145BE0 File Offset: 0x00143DE0
		public string GetTrait(string key)
		{
			if (this.m_traits != null)
			{
				for (int i = 0; i < this.m_traits.Length - 1; i += 2)
				{
					if (this.m_traits[i] == key)
					{
						return this.m_traits[i + 1];
					}
				}
			}
			return null;
		}

		/// <summary>Obtains a string representation of the current event source instance.</summary>
		/// <returns>The name and unique identifier that identify the current event source.</returns>
		// Token: 0x06006457 RID: 25687 RVA: 0x00145C26 File Offset: 0x00143E26
		public override string ToString()
		{
			return Environment.GetResourceString("EventSource({0}, {1})", new object[] { this.Name, this.Guid });
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06006458 RID: 25688 RVA: 0x00145C50 File Offset: 0x00143E50
		// (remove) Token: 0x06006459 RID: 25689 RVA: 0x00145C8F File Offset: 0x00143E8F
		public event EventHandler<EventCommandEventArgs> EventCommandExecuted
		{
			add
			{
				this.m_eventCommandExecuted = (EventHandler<EventCommandEventArgs>)Delegate.Combine(this.m_eventCommandExecuted, value);
				for (EventCommandEventArgs eventCommandEventArgs = this.m_deferredCommands; eventCommandEventArgs != null; eventCommandEventArgs = eventCommandEventArgs.nextCommand)
				{
					value(this, eventCommandEventArgs);
				}
			}
			remove
			{
				this.m_eventCommandExecuted = (EventHandler<EventCommandEventArgs>)Delegate.Remove(this.m_eventCommandExecuted, value);
			}
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class.</summary>
		// Token: 0x0600645A RID: 25690 RVA: 0x00145CA8 File Offset: 0x00143EA8
		protected EventSource()
			: this(EventSourceSettings.EtwManifestEventFormat)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class and specifies whether to throw an exception when an error occurs in the underlying Windows code.</summary>
		/// <param name="throwOnEventWriteErrors">true to throw an exception when an error occurs in the underlying Windows code; otherwise, false.</param>
		// Token: 0x0600645B RID: 25691 RVA: 0x00145CB1 File Offset: 0x00143EB1
		protected EventSource(bool throwOnEventWriteErrors)
			: this(EventSourceSettings.EtwManifestEventFormat | (throwOnEventWriteErrors ? EventSourceSettings.ThrowOnEventWriteErrors : EventSourceSettings.Default))
		{
		}

		// Token: 0x0600645C RID: 25692 RVA: 0x00145CC2 File Offset: 0x00143EC2
		protected EventSource(EventSourceSettings settings)
			: this(settings, null)
		{
		}

		// Token: 0x0600645D RID: 25693 RVA: 0x00145CCC File Offset: 0x00143ECC
		protected EventSource(EventSourceSettings settings, params string[] traits)
		{
			this.m_config = this.ValidateSettings(settings);
			Type type = base.GetType();
			this.Initialize(EventSource.GetGuid(type), EventSource.GetName(type), traits);
		}

		/// <summary>Called when the current event source is updated by the controller.</summary>
		/// <param name="command">The arguments for the event.</param>
		// Token: 0x0600645E RID: 25694 RVA: 0x00002194 File Offset: 0x00000394
		protected virtual void OnEventCommand(EventCommandEventArgs command)
		{
		}

		/// <summary>Writes an event by using the provided event identifier.</summary>
		/// <param name="eventId">The event identifier.</param>
		// Token: 0x0600645F RID: 25695 RVA: 0x00145D06 File Offset: 0x00143F06
		[SecuritySafeCritical]
		protected void WriteEvent(int eventId)
		{
			this.WriteEventCore(eventId, 0, null);
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer argument.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">An integer argument.</param>
		// Token: 0x06006460 RID: 25696 RVA: 0x00145D14 File Offset: 0x00143F14
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, int arg1)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 4;
				this.WriteEventCore(eventId, 1, ptr);
			}
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">An integer argument.</param>
		/// <param name="arg2">An integer argument.</param>
		// Token: 0x06006461 RID: 25697 RVA: 0x00145D54 File Offset: 0x00143F54
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, int arg1, int arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 4;
				this.WriteEventCore(eventId, 2, ptr);
			}
		}

		/// <summary>Writes an event by using the provided event identifier and 32-bit integer arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">An integer argument.</param>
		/// <param name="arg2">An integer argument.</param>
		/// <param name="arg3">An integer argument.</param>
		// Token: 0x06006462 RID: 25698 RVA: 0x00145DB8 File Offset: 0x00143FB8
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, int arg1, int arg2, int arg3)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&arg3));
				ptr[2].Size = 4;
				this.WriteEventCore(eventId, 3, ptr);
			}
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit integer argument.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A 64 bit integer argument.</param>
		// Token: 0x06006463 RID: 25699 RVA: 0x00145E44 File Offset: 0x00144044
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, long arg1)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				this.WriteEventCore(eventId, 1, ptr);
			}
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A 64 bit integer argument.</param>
		/// <param name="arg2">A 64 bit integer argument.</param>
		// Token: 0x06006464 RID: 25700 RVA: 0x00145E84 File Offset: 0x00144084
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, long arg1, long arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 8;
				this.WriteEventCore(eventId, 2, ptr);
			}
		}

		/// <summary>Writes an event by using the provided event identifier and 64-bit arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A 64 bit integer argument.</param>
		/// <param name="arg2">A 64 bit integer argument.</param>
		/// <param name="arg3">A 64 bit integer argument.</param>
		// Token: 0x06006465 RID: 25701 RVA: 0x00145EE8 File Offset: 0x001440E8
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, long arg1, long arg2, long arg3)
		{
			if (this.m_eventSourceEnabled)
			{
				EventSource.EventData* ptr;
				checked
				{
					ptr = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
				}
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 8;
				ptr[2].DataPointer = (IntPtr)((void*)(&arg3));
				ptr[2].Size = 8;
				this.WriteEventCore(eventId, 3, ptr);
			}
		}

		/// <summary>Writes an event by using the provided event identifier and string argument.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A string argument.</param>
		// Token: 0x06006466 RID: 25702 RVA: 0x00145F74 File Offset: 0x00144174
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, string arg1)
		{
			if (this.m_eventSourceEnabled)
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
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData)];
						ptr2->DataPointer = (IntPtr)((void*)ptr);
					}
					ptr2->Size = (arg1.Length + 1) * 2;
					this.WriteEventCore(eventId, 1, ptr2);
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and string arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A string argument.</param>
		// Token: 0x06006467 RID: 25703 RVA: 0x00145FD8 File Offset: 0x001441D8
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, string arg1, string arg2)
		{
			if (this.m_eventSourceEnabled)
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
							ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
							ptr3->DataPointer = (IntPtr)((void*)ptr);
						}
						ptr3->Size = (arg1.Length + 1) * 2;
						ptr3[1].DataPointer = (IntPtr)((void*)ptr2);
						ptr3[1].Size = (arg2.Length + 1) * 2;
						this.WriteEventCore(eventId, 2, ptr3);
					}
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and string arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A string argument.</param>
		/// <param name="arg3">A string argument.</param>
		// Token: 0x06006468 RID: 25704 RVA: 0x0014608C File Offset: 0x0014428C
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, string arg1, string arg2, string arg3)
		{
			if (this.m_eventSourceEnabled)
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
								ptr4 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
								ptr4->DataPointer = (IntPtr)((void*)ptr);
							}
							ptr4->Size = (arg1.Length + 1) * 2;
							ptr4[1].DataPointer = (IntPtr)((void*)ptr2);
							ptr4[1].Size = (arg2.Length + 1) * 2;
							ptr4[2].DataPointer = (IntPtr)((void*)ptr3);
							ptr4[2].Size = (arg3.Length + 1) * 2;
							this.WriteEventCore(eventId, 3, ptr4);
						}
					}
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A 32 bit integer argument.</param>
		// Token: 0x06006469 RID: 25705 RVA: 0x00146198 File Offset: 0x00144398
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, string arg1, int arg2)
		{
			if (this.m_eventSourceEnabled)
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
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
						ptr2->DataPointer = (IntPtr)((void*)ptr);
					}
					ptr2->Size = (arg1.Length + 1) * 2;
					ptr2[1].DataPointer = (IntPtr)((void*)(&arg2));
					ptr2[1].Size = 4;
					this.WriteEventCore(eventId, 2, ptr2);
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A 32 bit integer argument.</param>
		/// <param name="arg3">A 32 bit integer argument.</param>
		// Token: 0x0600646A RID: 25706 RVA: 0x00146220 File Offset: 0x00144420
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, string arg1, int arg2, int arg3)
		{
			if (this.m_eventSourceEnabled)
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
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
						ptr2->DataPointer = (IntPtr)((void*)ptr);
					}
					ptr2->Size = (arg1.Length + 1) * 2;
					ptr2[1].DataPointer = (IntPtr)((void*)(&arg2));
					ptr2[1].Size = 4;
					ptr2[2].DataPointer = (IntPtr)((void*)(&arg3));
					ptr2[2].Size = 4;
					this.WriteEventCore(eventId, 3, ptr2);
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="arg1">A string argument.</param>
		/// <param name="arg2">A 64 bit integer argument.</param>
		// Token: 0x0600646B RID: 25707 RVA: 0x001462D4 File Offset: 0x001444D4
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, string arg1, long arg2)
		{
			if (this.m_eventSourceEnabled)
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
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
						ptr2->DataPointer = (IntPtr)((void*)ptr);
					}
					ptr2->Size = (arg1.Length + 1) * 2;
					ptr2[1].DataPointer = (IntPtr)((void*)(&arg2));
					ptr2[1].Size = 8;
					this.WriteEventCore(eventId, 2, ptr2);
				}
			}
		}

		// Token: 0x0600646C RID: 25708 RVA: 0x0014635C File Offset: 0x0014455C
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, long arg1, string arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg2 == null)
				{
					arg2 = "";
				}
				fixed (string text = arg2)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr2->DataPointer = (IntPtr)((void*)(&arg1));
					ptr2->Size = 8;
					ptr2[1].DataPointer = (IntPtr)((void*)ptr);
					ptr2[1].Size = (arg2.Length + 1) * 2;
					this.WriteEventCore(eventId, 2, ptr2);
				}
			}
		}

		// Token: 0x0600646D RID: 25709 RVA: 0x001463E4 File Offset: 0x001445E4
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, int arg1, string arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg2 == null)
				{
					arg2 = "";
				}
				fixed (string text = arg2)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventSource.EventData* ptr2;
					checked
					{
						ptr2 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr2->DataPointer = (IntPtr)((void*)(&arg1));
					ptr2->Size = 4;
					ptr2[1].DataPointer = (IntPtr)((void*)ptr);
					ptr2[1].Size = (arg2.Length + 1) * 2;
					this.WriteEventCore(eventId, 2, ptr2);
				}
			}
		}

		// Token: 0x0600646E RID: 25710 RVA: 0x0014646C File Offset: 0x0014466C
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, byte[] arg1)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg1 == null)
				{
					arg1 = new byte[0];
				}
				int num = arg1.Length;
				fixed (byte* ptr = &arg1[0])
				{
					byte* ptr2 = ptr;
					EventSource.EventData* ptr3;
					checked
					{
						ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr3->DataPointer = (IntPtr)((void*)(&num));
					ptr3->Size = 4;
					ptr3[1].DataPointer = (IntPtr)((void*)ptr2);
					ptr3[1].Size = num;
					this.WriteEventCore(eventId, 2, ptr3);
				}
			}
		}

		// Token: 0x0600646F RID: 25711 RVA: 0x001464EC File Offset: 0x001446EC
		[SecuritySafeCritical]
		protected unsafe void WriteEvent(int eventId, long arg1, byte[] arg2)
		{
			if (this.m_eventSourceEnabled)
			{
				if (arg2 == null)
				{
					arg2 = new byte[0];
				}
				int num = arg2.Length;
				fixed (byte* ptr = &arg2[0])
				{
					byte* ptr2 = ptr;
					EventSource.EventData* ptr3;
					checked
					{
						ptr3 = stackalloc EventSource.EventData[unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData)];
					}
					ptr3->DataPointer = (IntPtr)((void*)(&arg1));
					ptr3->Size = 8;
					ptr3[1].DataPointer = (IntPtr)((void*)(&num));
					ptr3[1].Size = 4;
					ptr3[2].DataPointer = (IntPtr)((void*)ptr2);
					ptr3[2].Size = num;
					this.WriteEventCore(eventId, 3, ptr3);
				}
			}
		}

		/// <summary>Creates a new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload by using the provided event identifier and event data.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="eventDataCount">The number of event data items.</param>
		/// <param name="data">The structure that contains the event data.</param>
		// Token: 0x06006470 RID: 25712 RVA: 0x00146595 File Offset: 0x00144795
		[CLSCompliant(false)]
		[SecurityCritical]
		protected unsafe void WriteEventCore(int eventId, int eventDataCount, EventSource.EventData* data)
		{
			this.WriteEventWithRelatedActivityIdCore(eventId, null, eventDataCount, data);
		}

		/// <summary>Writes an event that indicates that the current activity is related to another activity.</summary>
		/// <param name="eventId">An identifier that uniquely identifies this event within the <see cref="T:System.Diagnostics.Tracing.EventSource" />.</param>
		/// <param name="childActivityID">A pointer to the GUID of the child activity ID. </param>
		/// <param name="eventDataCount">The number of items in the <paramref name="data" /> field. </param>
		/// <param name="data">A pointer to the first item in the event data field. </param>
		// Token: 0x06006471 RID: 25713 RVA: 0x001465A4 File Offset: 0x001447A4
		[CLSCompliant(false)]
		[SecurityCritical]
		protected unsafe void WriteEventWithRelatedActivityIdCore(int eventId, Guid* relatedActivityId, int eventDataCount, EventSource.EventData* data)
		{
			if (this.m_eventSourceEnabled)
			{
				try
				{
					if (relatedActivityId != null)
					{
						this.ValidateEventOpcodeForTransfer(ref this.m_eventData[eventId], this.m_eventData[eventId].Name);
					}
					if (this.m_eventData[eventId].EnabledForETW)
					{
						EventOpcode opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode;
						EventActivityOptions activityOptions = this.m_eventData[eventId].ActivityOptions;
						Guid* ptr = null;
						Guid empty = Guid.Empty;
						Guid empty2 = Guid.Empty;
						if (opcode != EventOpcode.Info && relatedActivityId == null && (activityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
						{
							if (opcode == EventOpcode.Start)
							{
								this.m_activityTracker.OnStart(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty, ref empty2, this.m_eventData[eventId].ActivityOptions);
							}
							else if (opcode == EventOpcode.Stop)
							{
								this.m_activityTracker.OnStop(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty);
							}
							if (empty != Guid.Empty)
							{
								ptr = &empty;
							}
							if (empty2 != Guid.Empty)
							{
								relatedActivityId = &empty2;
							}
						}
						SessionMask sessionMask = SessionMask.All;
						if ((ulong)this.m_curLiveSessions != 0UL)
						{
							sessionMask = this.GetEtwSessionMask(eventId, relatedActivityId);
						}
						if ((ulong)sessionMask != 0UL || (this.m_legacySessions != null && this.m_legacySessions.Count > 0))
						{
							if (!this.SelfDescribingEvents)
							{
								if (sessionMask.IsEqualOrSupersetOf(this.m_curLiveSessions))
								{
									if (!this.m_provider.WriteEvent(ref this.m_eventData[eventId].Descriptor, ptr, relatedActivityId, eventDataCount, (IntPtr)((void*)data)))
									{
										this.ThrowEventSourceException(this.m_eventData[eventId].Name, null);
									}
								}
								else
								{
									long num = this.m_eventData[eventId].Descriptor.Keywords & (long)(~(long)SessionMask.All.ToEventKeywords());
									EventDescriptor eventDescriptor = new EventDescriptor(this.m_eventData[eventId].Descriptor.EventId, this.m_eventData[eventId].Descriptor.Version, this.m_eventData[eventId].Descriptor.Channel, this.m_eventData[eventId].Descriptor.Level, this.m_eventData[eventId].Descriptor.Opcode, this.m_eventData[eventId].Descriptor.Task, (long)(sessionMask.ToEventKeywords() | (ulong)num));
									if (!this.m_provider.WriteEvent(ref eventDescriptor, ptr, relatedActivityId, eventDataCount, (IntPtr)((void*)data)))
									{
										this.ThrowEventSourceException(this.m_eventData[eventId].Name, null);
									}
								}
							}
							else
							{
								TraceLoggingEventTypes traceLoggingEventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
								if (traceLoggingEventTypes == null)
								{
									traceLoggingEventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
									Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, traceLoggingEventTypes, null);
								}
								long num2 = this.m_eventData[eventId].Descriptor.Keywords & (long)(~(long)SessionMask.All.ToEventKeywords());
								EventSourceOptions eventSourceOptions = new EventSourceOptions
								{
									Keywords = (EventKeywords)(sessionMask.ToEventKeywords() | (ulong)num2),
									Level = (EventLevel)this.m_eventData[eventId].Descriptor.Level,
									Opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode
								};
								this.WriteMultiMerge(this.m_eventData[eventId].Name, ref eventSourceOptions, traceLoggingEventTypes, ptr, relatedActivityId, data);
							}
						}
					}
					if (this.m_Dispatchers != null && this.m_eventData[eventId].EnabledForAnyListener)
					{
						this.WriteToAllListeners(eventId, relatedActivityId, eventDataCount, data);
					}
				}
				catch (Exception ex)
				{
					if (ex is EventSourceException)
					{
						throw;
					}
					this.ThrowEventSourceException(this.m_eventData[eventId].Name, ex);
				}
			}
		}

		/// <summary>Writes an event by using the provided event identifier and array of arguments.</summary>
		/// <param name="eventId">The event identifier.</param>
		/// <param name="args">An array of objects.</param>
		// Token: 0x06006472 RID: 25714 RVA: 0x00146A40 File Offset: 0x00144C40
		[SecuritySafeCritical]
		protected void WriteEvent(int eventId, params object[] args)
		{
			this.WriteEventVarargs(eventId, null, args);
		}

		/// <summary>Writes an event that indicates that the current activity is related to another activity. </summary>
		/// <param name="eventId">An identifier that uniquely identifies this event within the <see cref="T:System.Diagnostics.Tracing.EventSource" />. </param>
		/// <param name="childActivityID">The related activity identifier. </param>
		/// <param name="args">An array of objects that contain data about the event, or null if only the <paramref name="childActivityID" /> is needed.</param>
		// Token: 0x06006473 RID: 25715 RVA: 0x00146A4C File Offset: 0x00144C4C
		[SecuritySafeCritical]
		protected unsafe void WriteEventWithRelatedActivityId(int eventId, Guid relatedActivityId, params object[] args)
		{
			this.WriteEventVarargs(eventId, &relatedActivityId, args);
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class.</summary>
		// Token: 0x06006474 RID: 25716 RVA: 0x00146A59 File Offset: 0x00144C59
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Diagnostics.Tracing.EventSource" /> class and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06006475 RID: 25717 RVA: 0x00146A68 File Offset: 0x00144C68
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.m_eventSourceEnabled)
				{
					try
					{
						this.SendManifest(this.m_rawManifest);
					}
					catch (Exception)
					{
					}
					this.m_eventSourceEnabled = false;
				}
				if (this.m_provider != null)
				{
					this.m_provider.Dispose();
					this.m_provider = null;
				}
			}
			this.m_eventSourceEnabled = false;
		}

		// Token: 0x06006476 RID: 25718 RVA: 0x00146AD4 File Offset: 0x00144CD4
		~EventSource()
		{
			this.Dispose(false);
		}

		// Token: 0x06006477 RID: 25719 RVA: 0x00146B04 File Offset: 0x00144D04
		internal void WriteStringToListener(EventListener listener, string msg, SessionMask m)
		{
			if (this.m_eventSourceEnabled)
			{
				if (listener == null)
				{
					this.WriteEventString(EventLevel.LogAlways, (long)m.ToEventKeywords(), msg);
					return;
				}
				List<object> list = new List<object>();
				list.Add(msg);
				listener.OnEventWritten(new EventWrittenEventArgs(this)
				{
					EventId = 0,
					Payload = new ReadOnlyCollection<object>(list)
				});
			}
		}

		// Token: 0x06006478 RID: 25720 RVA: 0x00146B5A File Offset: 0x00144D5A
		[SecurityCritical]
		private unsafe void WriteEventRaw(string eventName, ref EventDescriptor eventDescriptor, Guid* activityID, Guid* relatedActivityID, int dataCount, IntPtr data)
		{
			if (this.m_provider == null)
			{
				this.ThrowEventSourceException(eventName, null);
				return;
			}
			if (!this.m_provider.WriteEventRaw(ref eventDescriptor, activityID, relatedActivityID, dataCount, data))
			{
				this.ThrowEventSourceException(eventName, null);
			}
		}

		// Token: 0x06006479 RID: 25721 RVA: 0x00146B8E File Offset: 0x00144D8E
		internal EventSource(Guid eventSourceGuid, string eventSourceName)
			: this(eventSourceGuid, eventSourceName, EventSourceSettings.EtwManifestEventFormat, null)
		{
		}

		// Token: 0x0600647A RID: 25722 RVA: 0x00146B9A File Offset: 0x00144D9A
		internal EventSource(Guid eventSourceGuid, string eventSourceName, EventSourceSettings settings, string[] traits = null)
		{
			this.m_config = this.ValidateSettings(settings);
			this.Initialize(eventSourceGuid, eventSourceName, traits);
		}

		// Token: 0x0600647B RID: 25723 RVA: 0x00146BBC File Offset: 0x00144DBC
		[SecuritySafeCritical]
		private unsafe void Initialize(Guid eventSourceGuid, string eventSourceName, string[] traits)
		{
			try
			{
				this.m_traits = traits;
				if (this.m_traits != null && this.m_traits.Length % 2 != 0)
				{
					throw new ArgumentException(Environment.GetResourceString("TraitEven"), "traits");
				}
				if (eventSourceGuid == Guid.Empty)
				{
					throw new ArgumentException(Environment.GetResourceString("The Guid of an EventSource must be non zero."));
				}
				if (eventSourceName == null)
				{
					throw new ArgumentException(Environment.GetResourceString("The name of an EventSource must not be null."));
				}
				this.m_name = eventSourceName;
				this.m_guid = eventSourceGuid;
				this.m_curLiveSessions = new SessionMask(0U);
				this.m_etwSessionIdMap = new EtwSession[4];
				this.m_activityTracker = ActivityTracker.Instance;
				this.InitializeProviderMetadata();
				EventSource.OverideEventProvider overideEventProvider = new EventSource.OverideEventProvider(this);
				overideEventProvider.Register(eventSourceGuid);
				EventListener.AddEventSource(this);
				this.m_provider = overideEventProvider;
				int num = Environment.OSVersion.Version.Major * 10 + Environment.OSVersion.Version.Minor;
				if (this.Name != "System.Diagnostics.Eventing.FrameworkEventSource" || num >= 62)
				{
					try
					{
						byte[] array;
						void* ptr;
						if ((array = this.providerMetadata) == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = (void*)(&array[0]);
						}
						this.m_provider.SetInformation(UnsafeNativeMethods.ManifestEtw.EVENT_INFO_CLASS.SetTraits, ptr, this.providerMetadata.Length);
					}
					finally
					{
						byte[] array = null;
					}
				}
				this.m_completelyInited = true;
			}
			catch (Exception ex)
			{
				if (this.m_constructionException == null)
				{
					this.m_constructionException = ex;
				}
				this.ReportOutOfBandMessage("ERROR: Exception during construction of EventSource " + this.Name + ": " + ex.Message, true);
			}
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				for (EventCommandEventArgs eventCommandEventArgs = this.m_deferredCommands; eventCommandEventArgs != null; eventCommandEventArgs = eventCommandEventArgs.nextCommand)
				{
					this.DoCommand(eventCommandEventArgs);
				}
			}
		}

		// Token: 0x0600647C RID: 25724 RVA: 0x00146DC0 File Offset: 0x00144FC0
		private static string GetName(Type eventSourceType, EventManifestOptions flags)
		{
			if (eventSourceType == null)
			{
				throw new ArgumentNullException("eventSourceType");
			}
			EventSourceAttribute eventSourceAttribute = (EventSourceAttribute)EventSource.GetCustomAttributeHelper(eventSourceType, typeof(EventSourceAttribute), flags);
			if (eventSourceAttribute != null && eventSourceAttribute.Name != null)
			{
				return eventSourceAttribute.Name;
			}
			return eventSourceType.Name;
		}

		// Token: 0x0600647D RID: 25725 RVA: 0x00146E10 File Offset: 0x00145010
		private static Guid GenerateGuidFromName(string name)
		{
			byte[] bytes = Encoding.BigEndianUnicode.GetBytes(name);
			EventSource.Sha1ForNonSecretPurposes sha1ForNonSecretPurposes = default(EventSource.Sha1ForNonSecretPurposes);
			sha1ForNonSecretPurposes.Start();
			sha1ForNonSecretPurposes.Append(EventSource.namespaceBytes);
			sha1ForNonSecretPurposes.Append(bytes);
			Array.Resize<byte>(ref bytes, 16);
			sha1ForNonSecretPurposes.Finish(bytes);
			bytes[7] = (bytes[7] & 15) | 80;
			return new Guid(bytes);
		}

		// Token: 0x0600647E RID: 25726 RVA: 0x00146E70 File Offset: 0x00145070
		[SecurityCritical]
		private unsafe object DecodeObject(int eventId, int parameterId, ref EventSource.EventData* data)
		{
			IntPtr intPtr = data.DataPointer;
			data += (IntPtr)sizeof(EventSource.EventData);
			Type type = this.m_eventData[eventId].Parameters[parameterId].ParameterType;
			while (!(type == typeof(IntPtr)))
			{
				if (type == typeof(int))
				{
					return *(int*)(void*)intPtr;
				}
				if (type == typeof(uint))
				{
					return *(uint*)(void*)intPtr;
				}
				if (type == typeof(long))
				{
					return *(long*)(void*)intPtr;
				}
				if (type == typeof(ulong))
				{
					return (ulong)(*(long*)(void*)intPtr);
				}
				if (type == typeof(byte))
				{
					return *(byte*)(void*)intPtr;
				}
				if (type == typeof(sbyte))
				{
					return *(sbyte*)(void*)intPtr;
				}
				if (type == typeof(short))
				{
					return *(short*)(void*)intPtr;
				}
				if (type == typeof(ushort))
				{
					return *(ushort*)(void*)intPtr;
				}
				if (type == typeof(float))
				{
					return *(float*)(void*)intPtr;
				}
				if (type == typeof(double))
				{
					return *(double*)(void*)intPtr;
				}
				if (type == typeof(decimal))
				{
					return *(decimal*)(void*)intPtr;
				}
				if (type == typeof(bool))
				{
					if (*(int*)(void*)intPtr == 1)
					{
						return true;
					}
					return false;
				}
				else
				{
					if (type == typeof(Guid))
					{
						return *(Guid*)(void*)intPtr;
					}
					if (type == typeof(char))
					{
						return (char)(*(ushort*)(void*)intPtr);
					}
					if (type == typeof(DateTime))
					{
						return DateTime.FromFileTimeUtc(*(long*)(void*)intPtr);
					}
					if (type == typeof(byte[]))
					{
						int num = *(int*)(void*)intPtr;
						byte[] array = new byte[num];
						intPtr = data.DataPointer;
						data += (IntPtr)sizeof(EventSource.EventData);
						for (int i = 0; i < num; i++)
						{
							array[i] = ((byte*)(void*)intPtr)[i];
						}
						return array;
					}
					if (type == typeof(byte*))
					{
						return null;
					}
					if (!type.IsEnum())
					{
						return Marshal.PtrToStringUni(intPtr);
					}
					type = Enum.GetUnderlyingType(type);
				}
			}
			return *(IntPtr*)(void*)intPtr;
		}

		// Token: 0x0600647F RID: 25727 RVA: 0x00147138 File Offset: 0x00145338
		private EventDispatcher GetDispatcher(EventListener listener)
		{
			EventDispatcher eventDispatcher;
			for (eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
			{
				if (eventDispatcher.m_Listener == listener)
				{
					return eventDispatcher;
				}
			}
			return eventDispatcher;
		}

		// Token: 0x06006480 RID: 25728 RVA: 0x00147168 File Offset: 0x00145368
		[SecurityCritical]
		private unsafe void WriteEventVarargs(int eventId, Guid* childActivityID, object[] args)
		{
			if (this.m_eventSourceEnabled)
			{
				try
				{
					if (childActivityID != null)
					{
						this.ValidateEventOpcodeForTransfer(ref this.m_eventData[eventId], this.m_eventData[eventId].Name);
						if (!this.m_eventData[eventId].HasRelatedActivityID)
						{
							throw new ArgumentException(Environment.GetResourceString("EventSource expects the first parameter of the Event method to be of type Guid and to be named \"relatedActivityId\" when calling WriteEventWithRelatedActivityId."));
						}
					}
					this.LogEventArgsMismatches(this.m_eventData[eventId].Parameters, args);
					if (this.m_eventData[eventId].EnabledForETW)
					{
						Guid* ptr = null;
						Guid empty = Guid.Empty;
						Guid empty2 = Guid.Empty;
						EventOpcode opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode;
						EventActivityOptions activityOptions = this.m_eventData[eventId].ActivityOptions;
						if (childActivityID == null && (activityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
						{
							if (opcode == EventOpcode.Start)
							{
								this.m_activityTracker.OnStart(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty, ref empty2, this.m_eventData[eventId].ActivityOptions);
							}
							else if (opcode == EventOpcode.Stop)
							{
								this.m_activityTracker.OnStop(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty);
							}
							if (empty != Guid.Empty)
							{
								ptr = &empty;
							}
							if (empty2 != Guid.Empty)
							{
								childActivityID = &empty2;
							}
						}
						SessionMask sessionMask = SessionMask.All;
						if ((ulong)this.m_curLiveSessions != 0UL)
						{
							sessionMask = this.GetEtwSessionMask(eventId, childActivityID);
						}
						if ((ulong)sessionMask != 0UL || (this.m_legacySessions != null && this.m_legacySessions.Count > 0))
						{
							if (!this.SelfDescribingEvents)
							{
								if (sessionMask.IsEqualOrSupersetOf(this.m_curLiveSessions))
								{
									if (!this.m_provider.WriteEvent(ref this.m_eventData[eventId].Descriptor, ptr, childActivityID, args))
									{
										this.ThrowEventSourceException(this.m_eventData[eventId].Name, null);
									}
								}
								else
								{
									long num = this.m_eventData[eventId].Descriptor.Keywords & (long)(~(long)SessionMask.All.ToEventKeywords());
									EventDescriptor eventDescriptor = new EventDescriptor(this.m_eventData[eventId].Descriptor.EventId, this.m_eventData[eventId].Descriptor.Version, this.m_eventData[eventId].Descriptor.Channel, this.m_eventData[eventId].Descriptor.Level, this.m_eventData[eventId].Descriptor.Opcode, this.m_eventData[eventId].Descriptor.Task, (long)(sessionMask.ToEventKeywords() | (ulong)num));
									if (!this.m_provider.WriteEvent(ref eventDescriptor, ptr, childActivityID, args))
									{
										this.ThrowEventSourceException(this.m_eventData[eventId].Name, null);
									}
								}
							}
							else
							{
								TraceLoggingEventTypes traceLoggingEventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
								if (traceLoggingEventTypes == null)
								{
									traceLoggingEventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
									Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, traceLoggingEventTypes, null);
								}
								long num2 = this.m_eventData[eventId].Descriptor.Keywords & (long)(~(long)SessionMask.All.ToEventKeywords());
								EventSourceOptions eventSourceOptions = new EventSourceOptions
								{
									Keywords = (EventKeywords)(sessionMask.ToEventKeywords() | (ulong)num2),
									Level = (EventLevel)this.m_eventData[eventId].Descriptor.Level,
									Opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode
								};
								this.WriteMultiMerge(this.m_eventData[eventId].Name, ref eventSourceOptions, traceLoggingEventTypes, ptr, childActivityID, args);
							}
						}
					}
					if (this.m_Dispatchers != null && this.m_eventData[eventId].EnabledForAnyListener)
					{
						if (AppContextSwitches.PreserveEventListnerObjectIdentity)
						{
							this.WriteToAllListeners(eventId, childActivityID, args);
						}
						else
						{
							object[] array = this.SerializeEventArgs(eventId, args);
							this.WriteToAllListeners(eventId, childActivityID, array);
						}
					}
				}
				catch (Exception ex)
				{
					if (ex is EventSourceException)
					{
						throw;
					}
					this.ThrowEventSourceException(this.m_eventData[eventId].Name, ex);
				}
			}
		}

		// Token: 0x06006481 RID: 25729 RVA: 0x0014764C File Offset: 0x0014584C
		[SecurityCritical]
		private object[] SerializeEventArgs(int eventId, object[] args)
		{
			TraceLoggingEventTypes traceLoggingEventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
			if (traceLoggingEventTypes == null)
			{
				traceLoggingEventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
				Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, traceLoggingEventTypes, null);
			}
			object[] array = new object[traceLoggingEventTypes.typeInfos.Length];
			for (int i = 0; i < traceLoggingEventTypes.typeInfos.Length; i++)
			{
				array[i] = traceLoggingEventTypes.typeInfos[i].GetData(args[i]);
			}
			return array;
		}

		// Token: 0x06006482 RID: 25730 RVA: 0x001476F0 File Offset: 0x001458F0
		private void LogEventArgsMismatches(ParameterInfo[] infos, object[] args)
		{
			bool flag = args.Length == infos.Length;
			int num = 0;
			while (flag && num < args.Length)
			{
				Type parameterType = infos[num].ParameterType;
				if ((args[num] != null && args[num].GetType() != parameterType) || (args[num] == null && (!parameterType.IsGenericType || !(parameterType.GetGenericTypeDefinition() == typeof(Nullable<>)))))
				{
					flag = false;
					break;
				}
				num++;
			}
			if (!flag)
			{
				Debugger.Log(0, null, Environment.GetResourceString("The parameters to the Event method do not match the parameters to the WriteEvent method. This may cause the event to be displayed incorrectly.") + "\r\n");
			}
		}

		// Token: 0x06006483 RID: 25731 RVA: 0x0014777C File Offset: 0x0014597C
		private int GetParamLengthIncludingByteArray(ParameterInfo[] parameters)
		{
			int num = 0;
			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i].ParameterType == typeof(byte[]))
				{
					num += 2;
				}
				else
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06006484 RID: 25732 RVA: 0x001477C0 File Offset: 0x001459C0
		[SecurityCritical]
		private unsafe void WriteToAllListeners(int eventId, Guid* childActivityID, int eventDataCount, EventSource.EventData* data)
		{
			int num = this.m_eventData[eventId].Parameters.Length;
			int paramLengthIncludingByteArray = this.GetParamLengthIncludingByteArray(this.m_eventData[eventId].Parameters);
			if (eventDataCount != paramLengthIncludingByteArray)
			{
				this.ReportOutOfBandMessage(Environment.GetResourceString("Event {0} was called with {1} argument(s) , but it is defined with {2} paramenter(s).", new object[] { eventId, eventDataCount, num }), true);
				num = Math.Min(num, eventDataCount);
			}
			object[] array = new object[num];
			EventSource.EventData* ptr = data;
			for (int i = 0; i < num; i++)
			{
				array[i] = this.DecodeObject(eventId, i, ref ptr);
			}
			this.WriteToAllListeners(eventId, childActivityID, array);
		}

		// Token: 0x06006485 RID: 25733 RVA: 0x00147870 File Offset: 0x00145A70
		[SecurityCritical]
		private unsafe void WriteToAllListeners(int eventId, Guid* childActivityID, params object[] args)
		{
			EventWrittenEventArgs eventWrittenEventArgs = new EventWrittenEventArgs(this);
			eventWrittenEventArgs.EventId = eventId;
			if (childActivityID != null)
			{
				eventWrittenEventArgs.RelatedActivityId = *childActivityID;
			}
			eventWrittenEventArgs.EventName = this.m_eventData[eventId].Name;
			eventWrittenEventArgs.Message = this.m_eventData[eventId].Message;
			eventWrittenEventArgs.Payload = new ReadOnlyCollection<object>(args);
			this.DispatchToAllListeners(eventId, childActivityID, eventWrittenEventArgs);
		}

		// Token: 0x06006486 RID: 25734 RVA: 0x001478E4 File Offset: 0x00145AE4
		[SecurityCritical]
		private unsafe void DispatchToAllListeners(int eventId, Guid* childActivityID, EventWrittenEventArgs eventCallbackArgs)
		{
			Exception ex = null;
			for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
			{
				if (eventId == -1 || eventDispatcher.m_EventEnabled[eventId])
				{
					ActivityFilter activityFilter = eventDispatcher.m_Listener.m_activityFilter;
					if (activityFilter == null || ActivityFilter.PassesActivityFilter(activityFilter, childActivityID, this.m_eventData[eventId].TriggersActivityTracking > 0, this, eventId) || !eventDispatcher.m_activityFilteringEnabled)
					{
						try
						{
							eventDispatcher.m_Listener.OnEventWritten(eventCallbackArgs);
						}
						catch (Exception ex2)
						{
							this.ReportOutOfBandMessage("ERROR: Exception during EventSource.OnEventWritten: " + ex2.Message, false);
							ex = ex2;
						}
					}
				}
			}
			if (ex != null)
			{
				throw new EventSourceException(ex);
			}
		}

		// Token: 0x06006487 RID: 25735 RVA: 0x00147994 File Offset: 0x00145B94
		[SecuritySafeCritical]
		private unsafe void WriteEventString(EventLevel level, long keywords, string msgString)
		{
			if (this.m_provider != null)
			{
				string text = "EventSourceMessage";
				if (this.SelfDescribingEvents)
				{
					EventSourceOptions eventSourceOptions = new EventSourceOptions
					{
						Keywords = (EventKeywords)keywords,
						Level = level
					};
					var <>f__AnonymousType = new
					{
						message = msgString
					};
					TraceLoggingEventTypes traceLoggingEventTypes = new TraceLoggingEventTypes(text, EventTags.None, new Type[] { <>f__AnonymousType.GetType() });
					this.WriteMultiMergeInner(text, ref eventSourceOptions, traceLoggingEventTypes, null, null, new object[] { <>f__AnonymousType });
					return;
				}
				if (this.m_rawManifest == null && this.m_outOfBandMessageCount == 1)
				{
					ManifestBuilder manifestBuilder = new ManifestBuilder(this.Name, this.Guid, this.Name, null, EventManifestOptions.None);
					manifestBuilder.StartEvent(text, new EventAttribute(0)
					{
						Level = EventLevel.LogAlways,
						Task = (EventTask)65534
					});
					manifestBuilder.AddEventParameter(typeof(string), "message");
					manifestBuilder.EndEvent();
					this.SendManifest(manifestBuilder.CreateManifest());
				}
				fixed (string text2 = msgString)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					EventDescriptor eventDescriptor = new EventDescriptor(0, 0, 0, (byte)level, 0, 0, keywords);
					EventProvider.EventData eventData = default(EventProvider.EventData);
					eventData.Ptr = ptr;
					eventData.Size = (uint)(2 * (msgString.Length + 1));
					eventData.Reserved = 0U;
					this.m_provider.WriteEvent(ref eventDescriptor, null, null, 1, (IntPtr)((void*)(&eventData)));
				}
			}
		}

		// Token: 0x06006488 RID: 25736 RVA: 0x00147AF8 File Offset: 0x00145CF8
		private void WriteStringToAllListeners(string eventName, string msg)
		{
			EventWrittenEventArgs eventWrittenEventArgs = new EventWrittenEventArgs(this);
			eventWrittenEventArgs.EventId = 0;
			eventWrittenEventArgs.Message = msg;
			eventWrittenEventArgs.Payload = new ReadOnlyCollection<object>(new List<object> { msg });
			eventWrittenEventArgs.PayloadNames = new ReadOnlyCollection<string>(new List<string> { "message" });
			eventWrittenEventArgs.EventName = eventName;
			for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
			{
				bool flag = false;
				if (eventDispatcher.m_EventEnabled == null)
				{
					flag = true;
				}
				else
				{
					for (int i = 0; i < eventDispatcher.m_EventEnabled.Length; i++)
					{
						if (eventDispatcher.m_EventEnabled[i])
						{
							flag = true;
							break;
						}
					}
				}
				try
				{
					if (flag)
					{
						eventDispatcher.m_Listener.OnEventWritten(eventWrittenEventArgs);
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06006489 RID: 25737 RVA: 0x00147BBC File Offset: 0x00145DBC
		[SecurityCritical]
		private unsafe SessionMask GetEtwSessionMask(int eventId, Guid* childActivityID)
		{
			SessionMask sessionMask = default(SessionMask);
			int num = 0;
			while ((long)num < 4L)
			{
				EtwSession etwSession = this.m_etwSessionIdMap[num];
				if (etwSession != null)
				{
					ActivityFilter activityFilter = etwSession.m_activityFilter;
					if ((activityFilter == null && !this.m_activityFilteringForETWEnabled[num]) || (activityFilter != null && ActivityFilter.PassesActivityFilter(activityFilter, childActivityID, this.m_eventData[eventId].TriggersActivityTracking > 0, this, eventId)) || !this.m_activityFilteringForETWEnabled[num])
					{
						sessionMask[num] = true;
					}
				}
				num++;
			}
			if (this.m_legacySessions != null && this.m_legacySessions.Count > 0 && this.m_eventData[eventId].Descriptor.Opcode == 9)
			{
				Guid* ptr = null;
				foreach (EtwSession etwSession2 in this.m_legacySessions)
				{
					if (etwSession2 != null)
					{
						ActivityFilter activityFilter2 = etwSession2.m_activityFilter;
						if (activityFilter2 != null)
						{
							if (ptr == null)
							{
								Guid internalCurrentThreadActivityId = EventSource.InternalCurrentThreadActivityId;
								ptr = &internalCurrentThreadActivityId;
							}
							ActivityFilter.FlowActivityIfNeeded(activityFilter2, ptr, childActivityID);
						}
					}
				}
			}
			return sessionMask;
		}

		// Token: 0x0600648A RID: 25738 RVA: 0x00147CE8 File Offset: 0x00145EE8
		private bool IsEnabledByDefault(int eventNum, bool enable, EventLevel currentLevel, EventKeywords currentMatchAnyKeyword)
		{
			if (!enable)
			{
				return false;
			}
			EventLevel level = (EventLevel)this.m_eventData[eventNum].Descriptor.Level;
			EventKeywords eventKeywords = (EventKeywords)(this.m_eventData[eventNum].Descriptor.Keywords & (long)(~(long)SessionMask.All.ToEventKeywords()));
			EventChannel eventChannel = EventChannel.None;
			return this.IsEnabledCommon(enable, currentLevel, currentMatchAnyKeyword, level, eventKeywords, eventChannel);
		}

		// Token: 0x0600648B RID: 25739 RVA: 0x00147D4A File Offset: 0x00145F4A
		private bool IsEnabledCommon(bool enabled, EventLevel currentLevel, EventKeywords currentMatchAnyKeyword, EventLevel eventLevel, EventKeywords eventKeywords, EventChannel eventChannel)
		{
			return enabled && (currentLevel == EventLevel.LogAlways || currentLevel >= eventLevel) && (currentMatchAnyKeyword == EventKeywords.None || eventKeywords == EventKeywords.None || (eventKeywords & currentMatchAnyKeyword) != EventKeywords.None);
		}

		// Token: 0x0600648C RID: 25740 RVA: 0x00147D6C File Offset: 0x00145F6C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ThrowEventSourceException(string eventName, Exception innerEx = null)
		{
			if (EventSource.m_EventSourceExceptionRecurenceCount > 0)
			{
				return;
			}
			try
			{
				EventSource.m_EventSourceExceptionRecurenceCount += 1;
				string text = "EventSourceException";
				if (eventName != null)
				{
					text = text + " while processing event \"" + eventName + "\"";
				}
				switch (EventProvider.GetLastWriteEventError())
				{
				case EventProvider.WriteEventErrorCode.NoFreeBuffers:
					this.ReportOutOfBandMessage(text + ": " + Environment.GetResourceString("No Free Buffers available from the operating system (e.g. event rate too fast)."), true);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(Environment.GetResourceString("No Free Buffers available from the operating system (e.g. event rate too fast)."), innerEx);
					}
					break;
				case EventProvider.WriteEventErrorCode.EventTooBig:
					this.ReportOutOfBandMessage(text + ": " + Environment.GetResourceString("EventSource_EventTooBig"), true);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(Environment.GetResourceString("EventSource_EventTooBig"), innerEx);
					}
					break;
				case EventProvider.WriteEventErrorCode.NullInput:
					this.ReportOutOfBandMessage(text + ": " + Environment.GetResourceString("Null passed as a event argument."), true);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(Environment.GetResourceString("Null passed as a event argument."), innerEx);
					}
					break;
				case EventProvider.WriteEventErrorCode.TooManyArgs:
					this.ReportOutOfBandMessage(text + ": " + Environment.GetResourceString("Too many arguments."), true);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(Environment.GetResourceString("Too many arguments."), innerEx);
					}
					break;
				default:
					if (innerEx != null)
					{
						this.ReportOutOfBandMessage(string.Concat(new object[]
						{
							text,
							": ",
							innerEx.GetType(),
							":",
							innerEx.Message
						}), true);
					}
					else
					{
						this.ReportOutOfBandMessage(text, true);
					}
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(innerEx);
					}
					break;
				}
			}
			finally
			{
				EventSource.m_EventSourceExceptionRecurenceCount -= 1;
			}
		}

		// Token: 0x0600648D RID: 25741 RVA: 0x00147F2C File Offset: 0x0014612C
		private void ValidateEventOpcodeForTransfer(ref EventSource.EventMetadata eventData, string eventName)
		{
			if (eventData.Descriptor.Opcode != 9 && eventData.Descriptor.Opcode != 240 && eventData.Descriptor.Opcode != 1)
			{
				this.ThrowEventSourceException(eventName, null);
			}
		}

		// Token: 0x0600648E RID: 25742 RVA: 0x00147F65 File Offset: 0x00146165
		internal static EventOpcode GetOpcodeWithDefault(EventOpcode opcode, string eventName)
		{
			if (opcode == EventOpcode.Info && eventName != null)
			{
				if (eventName.EndsWith("Start"))
				{
					return EventOpcode.Start;
				}
				if (eventName.EndsWith("Stop"))
				{
					return EventOpcode.Stop;
				}
			}
			return opcode;
		}

		// Token: 0x0600648F RID: 25743 RVA: 0x00147F8C File Offset: 0x0014618C
		internal void SendCommand(EventListener listener, int perEventSourceSessionId, int etwSessionId, EventCommand command, bool enable, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> commandArguments)
		{
			EventCommandEventArgs eventCommandEventArgs = new EventCommandEventArgs(command, commandArguments, this, listener, perEventSourceSessionId, etwSessionId, enable, level, matchAnyKeyword);
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (this.m_completelyInited)
				{
					this.m_deferredCommands = null;
					this.DoCommand(eventCommandEventArgs);
				}
				else
				{
					eventCommandEventArgs.nextCommand = this.m_deferredCommands;
					this.m_deferredCommands = eventCommandEventArgs;
				}
			}
		}

		// Token: 0x06006490 RID: 25744 RVA: 0x00148004 File Offset: 0x00146204
		internal void DoCommand(EventCommandEventArgs commandArgs)
		{
			if (this.m_provider == null)
			{
				return;
			}
			this.m_outOfBandMessageCount = 0;
			bool flag = commandArgs.perEventSourceSessionId > 0 && (long)commandArgs.perEventSourceSessionId <= 4L;
			try
			{
				this.EnsureDescriptorsInitialized();
				commandArgs.dispatcher = this.GetDispatcher(commandArgs.listener);
				if (commandArgs.dispatcher == null && commandArgs.listener != null)
				{
					throw new ArgumentException(Environment.GetResourceString("Listener not found."));
				}
				if (commandArgs.Arguments == null)
				{
					commandArgs.Arguments = new Dictionary<string, string>();
				}
				if (commandArgs.Command == EventCommand.Update)
				{
					for (int i = 0; i < this.m_eventData.Length; i++)
					{
						this.EnableEventForDispatcher(commandArgs.dispatcher, i, this.IsEnabledByDefault(i, commandArgs.enable, commandArgs.level, commandArgs.matchAnyKeyword));
					}
					if (commandArgs.enable)
					{
						if (!this.m_eventSourceEnabled)
						{
							this.m_level = commandArgs.level;
							this.m_matchAnyKeyword = commandArgs.matchAnyKeyword;
						}
						else
						{
							if (commandArgs.level > this.m_level)
							{
								this.m_level = commandArgs.level;
							}
							if (commandArgs.matchAnyKeyword == EventKeywords.None)
							{
								this.m_matchAnyKeyword = EventKeywords.None;
							}
							else if (this.m_matchAnyKeyword != EventKeywords.None)
							{
								this.m_matchAnyKeyword |= commandArgs.matchAnyKeyword;
							}
						}
					}
					bool flag2 = commandArgs.perEventSourceSessionId >= 0;
					if (commandArgs.perEventSourceSessionId == 0 && !commandArgs.enable)
					{
						flag2 = false;
					}
					if (commandArgs.listener == null)
					{
						if (!flag2)
						{
							commandArgs.perEventSourceSessionId = -commandArgs.perEventSourceSessionId;
						}
						commandArgs.perEventSourceSessionId--;
					}
					commandArgs.Command = (flag2 ? EventCommand.Enable : EventCommand.Disable);
					if (flag2 && commandArgs.dispatcher == null && !this.SelfDescribingEvents)
					{
						this.SendManifest(this.m_rawManifest);
					}
					if (flag2 && commandArgs.perEventSourceSessionId != -1)
					{
						bool flag3 = false;
						string text;
						int num;
						EventSource.ParseCommandArgs(commandArgs.Arguments, out flag3, out text, out num);
						if (commandArgs.listener == null && commandArgs.Arguments.Count > 0 && commandArgs.perEventSourceSessionId != num)
						{
							throw new ArgumentException(Environment.GetResourceString("Bit position in AllKeywords ({0}) must equal the command argument named \"EtwSessionKeyword\" ({1}).", new object[]
							{
								commandArgs.perEventSourceSessionId + 44,
								num + 44
							}));
						}
						if (commandArgs.listener == null)
						{
							this.UpdateEtwSession(commandArgs.perEventSourceSessionId, commandArgs.etwSessionId, true, text, flag3);
						}
						else
						{
							ActivityFilter.UpdateFilter(ref commandArgs.listener.m_activityFilter, this, 0, text);
							commandArgs.dispatcher.m_activityFilteringEnabled = flag3;
						}
					}
					else if (!flag2 && commandArgs.listener == null && commandArgs.perEventSourceSessionId >= 0 && (long)commandArgs.perEventSourceSessionId < 4L)
					{
						commandArgs.Arguments["EtwSessionKeyword"] = (commandArgs.perEventSourceSessionId + 44).ToString(CultureInfo.InvariantCulture);
					}
					if (commandArgs.enable)
					{
						this.m_eventSourceEnabled = true;
					}
					this.OnEventCommand(commandArgs);
					EventHandler<EventCommandEventArgs> eventCommandExecuted = this.m_eventCommandExecuted;
					if (eventCommandExecuted != null)
					{
						eventCommandExecuted(this, commandArgs);
					}
					if (commandArgs.listener == null && !flag2 && commandArgs.perEventSourceSessionId != -1)
					{
						this.UpdateEtwSession(commandArgs.perEventSourceSessionId, commandArgs.etwSessionId, false, null, false);
					}
					if (!commandArgs.enable)
					{
						if (commandArgs.listener == null)
						{
							int num2 = 0;
							while ((long)num2 < 4L)
							{
								EtwSession etwSession = this.m_etwSessionIdMap[num2];
								if (etwSession != null)
								{
									ActivityFilter.DisableFilter(ref etwSession.m_activityFilter, this);
								}
								num2++;
							}
							this.m_activityFilteringForETWEnabled = new SessionMask(0U);
							this.m_curLiveSessions = new SessionMask(0U);
							if (this.m_etwSessionIdMap != null)
							{
								int num3 = 0;
								while ((long)num3 < 4L)
								{
									this.m_etwSessionIdMap[num3] = null;
									num3++;
								}
							}
							if (this.m_legacySessions != null)
							{
								this.m_legacySessions.Clear();
							}
						}
						else
						{
							ActivityFilter.DisableFilter(ref commandArgs.listener.m_activityFilter, this);
							commandArgs.dispatcher.m_activityFilteringEnabled = false;
						}
						for (int j = 0; j < this.m_eventData.Length; j++)
						{
							bool flag4 = false;
							for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
							{
								if (eventDispatcher.m_EventEnabled[j])
								{
									flag4 = true;
									break;
								}
							}
							this.m_eventData[j].EnabledForAnyListener = flag4;
						}
						if (!this.AnyEventEnabled())
						{
							this.m_level = EventLevel.LogAlways;
							this.m_matchAnyKeyword = EventKeywords.None;
							this.m_eventSourceEnabled = false;
						}
					}
					this.UpdateKwdTriggers(commandArgs.enable);
				}
				else
				{
					if (commandArgs.Command == EventCommand.SendManifest && this.m_rawManifest != null)
					{
						this.SendManifest(this.m_rawManifest);
					}
					this.OnEventCommand(commandArgs);
					EventHandler<EventCommandEventArgs> eventCommandExecuted2 = this.m_eventCommandExecuted;
					if (eventCommandExecuted2 != null)
					{
						eventCommandExecuted2(this, commandArgs);
					}
				}
				if (this.m_completelyInited && (commandArgs.listener != null || flag))
				{
					SessionMask sessionMask = SessionMask.FromId(commandArgs.perEventSourceSessionId);
					this.ReportActivitySamplingInfo(commandArgs.listener, sessionMask);
				}
			}
			catch (Exception ex)
			{
				this.ReportOutOfBandMessage("ERROR: Exception in Command Processing for EventSource " + this.Name + ": " + ex.Message, true);
			}
		}

		// Token: 0x06006491 RID: 25745 RVA: 0x001484F8 File Offset: 0x001466F8
		internal void UpdateEtwSession(int sessionIdBit, int etwSessionId, bool bEnable, string activityFilters, bool participateInSampling)
		{
			if ((long)sessionIdBit < 4L)
			{
				if (bEnable)
				{
					EtwSession etwSession = EtwSession.GetEtwSession(etwSessionId, true);
					ActivityFilter.UpdateFilter(ref etwSession.m_activityFilter, this, sessionIdBit, activityFilters);
					this.m_etwSessionIdMap[sessionIdBit] = etwSession;
					this.m_activityFilteringForETWEnabled[sessionIdBit] = participateInSampling;
				}
				else
				{
					EtwSession etwSession2 = EtwSession.GetEtwSession(etwSessionId, false);
					this.m_etwSessionIdMap[sessionIdBit] = null;
					this.m_activityFilteringForETWEnabled[sessionIdBit] = false;
					if (etwSession2 != null)
					{
						ActivityFilter.DisableFilter(ref etwSession2.m_activityFilter, this);
						EtwSession.RemoveEtwSession(etwSession2);
					}
				}
				this.m_curLiveSessions[sessionIdBit] = bEnable;
				return;
			}
			if (bEnable)
			{
				if (this.m_legacySessions == null)
				{
					this.m_legacySessions = new List<EtwSession>(8);
				}
				EtwSession etwSession3 = EtwSession.GetEtwSession(etwSessionId, true);
				if (!this.m_legacySessions.Contains(etwSession3))
				{
					this.m_legacySessions.Add(etwSession3);
					return;
				}
			}
			else
			{
				EtwSession etwSession4 = EtwSession.GetEtwSession(etwSessionId, false);
				if (etwSession4 != null)
				{
					if (this.m_legacySessions != null)
					{
						this.m_legacySessions.Remove(etwSession4);
					}
					EtwSession.RemoveEtwSession(etwSession4);
				}
			}
		}

		// Token: 0x06006492 RID: 25746 RVA: 0x001485E0 File Offset: 0x001467E0
		internal static bool ParseCommandArgs(IDictionary<string, string> commandArguments, out bool participateInSampling, out string activityFilters, out int sessionIdBit)
		{
			bool flag = true;
			participateInSampling = false;
			if (commandArguments.TryGetValue("ActivitySamplingStartEvent", out activityFilters))
			{
				participateInSampling = true;
			}
			string text;
			if (commandArguments.TryGetValue("ActivitySampling", out text))
			{
				if (string.Compare(text, "false", StringComparison.OrdinalIgnoreCase) == 0 || text == "0")
				{
					participateInSampling = false;
				}
				else
				{
					participateInSampling = true;
				}
			}
			int num = -1;
			string text2;
			if (!commandArguments.TryGetValue("EtwSessionKeyword", out text2) || !int.TryParse(text2, out num) || num < 44 || (long)num >= 48L)
			{
				sessionIdBit = -1;
				flag = false;
			}
			else
			{
				sessionIdBit = num - 44;
			}
			return flag;
		}

		// Token: 0x06006493 RID: 25747 RVA: 0x0014866C File Offset: 0x0014686C
		internal void UpdateKwdTriggers(bool enable)
		{
			if (enable)
			{
				ulong num = (ulong)this.m_matchAnyKeyword;
				if (num == 0UL)
				{
					num = ulong.MaxValue;
				}
				this.m_keywordTriggers = 0L;
				int num2 = 0;
				while ((long)num2 < 4L)
				{
					EtwSession etwSession = this.m_etwSessionIdMap[num2];
					if (etwSession != null)
					{
						ActivityFilter.UpdateKwdTriggers(etwSession.m_activityFilter, this.m_guid, this, (EventKeywords)num);
					}
					num2++;
				}
				return;
			}
			this.m_keywordTriggers = 0L;
		}

		// Token: 0x06006494 RID: 25748 RVA: 0x001486C8 File Offset: 0x001468C8
		internal bool EnableEventForDispatcher(EventDispatcher dispatcher, int eventId, bool value)
		{
			if (dispatcher == null)
			{
				if (eventId >= this.m_eventData.Length)
				{
					return false;
				}
				if (this.m_provider != null)
				{
					this.m_eventData[eventId].EnabledForETW = value;
				}
			}
			else
			{
				if (eventId >= dispatcher.m_EventEnabled.Length)
				{
					return false;
				}
				dispatcher.m_EventEnabled[eventId] = value;
				if (value)
				{
					this.m_eventData[eventId].EnabledForAnyListener = true;
				}
			}
			return true;
		}

		// Token: 0x06006495 RID: 25749 RVA: 0x00148738 File Offset: 0x00146938
		private bool AnyEventEnabled()
		{
			for (int i = 0; i < this.m_eventData.Length; i++)
			{
				if (this.m_eventData[i].EnabledForETW || this.m_eventData[i].EnabledForAnyListener)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06006496 RID: 25750 RVA: 0x00148787 File Offset: 0x00146987
		private bool IsDisposed
		{
			get
			{
				return this.m_provider == null || this.m_provider.m_disposed;
			}
		}

		// Token: 0x06006497 RID: 25751 RVA: 0x001487A4 File Offset: 0x001469A4
		[SecuritySafeCritical]
		private void EnsureDescriptorsInitialized()
		{
			if (this.m_eventData == null)
			{
				this.m_rawManifest = EventSource.CreateManifestAndDescriptors(base.GetType(), this.Name, this, EventManifestOptions.None);
				foreach (WeakReference weakReference in EventListener.s_EventSources)
				{
					EventSource eventSource = weakReference.Target as EventSource;
					if (eventSource != null && eventSource.Guid == this.m_guid && !eventSource.IsDisposed && eventSource != this)
					{
						throw new ArgumentException(Environment.GetResourceString("An instance of EventSource with Guid {0} already exists.", new object[] { this.m_guid }));
					}
				}
				for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
				{
					if (eventDispatcher.m_EventEnabled == null)
					{
						eventDispatcher.m_EventEnabled = new bool[this.m_eventData.Length];
					}
				}
			}
			if (EventSource.s_currentPid == 0U)
			{
				EventSource.s_currentPid = Win32Native.GetCurrentProcessId();
			}
		}

		// Token: 0x06006498 RID: 25752 RVA: 0x001488AC File Offset: 0x00146AAC
		[SecuritySafeCritical]
		private unsafe bool SendManifest(byte[] rawManifest)
		{
			bool flag = true;
			if (rawManifest == null)
			{
				return false;
			}
			fixed (byte[] array = rawManifest)
			{
				byte* ptr;
				if (rawManifest == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				EventDescriptor eventDescriptor = new EventDescriptor(65534, 1, 0, 0, 254, 65534, 72057594037927935L);
				ManifestEnvelope manifestEnvelope = default(ManifestEnvelope);
				manifestEnvelope.Format = ManifestEnvelope.ManifestFormats.SimpleXmlFormat;
				manifestEnvelope.MajorVersion = 1;
				manifestEnvelope.MinorVersion = 0;
				manifestEnvelope.Magic = 91;
				int i = rawManifest.Length;
				manifestEnvelope.ChunkNumber = 0;
				EventProvider.EventData* ptr2;
				checked
				{
					ptr2 = stackalloc EventProvider.EventData[unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventProvider.EventData)];
					ptr2->Ptr = &manifestEnvelope;
					ptr2->Size = (uint)sizeof(ManifestEnvelope);
					ptr2->Reserved = 0U;
				}
				ptr2[1].Ptr = ptr;
				ptr2[1].Reserved = 0U;
				int num = 65280;
				for (;;)
				{
					IL_00CA:
					manifestEnvelope.TotalChunks = (ushort)((i + (num - 1)) / num);
					while (i > 0)
					{
						ptr2[1].Size = (uint)Math.Min(i, num);
						if (this.m_provider != null && !this.m_provider.WriteEvent(ref eventDescriptor, null, null, 2, (IntPtr)((void*)ptr2)))
						{
							if (EventProvider.GetLastWriteEventError() == EventProvider.WriteEventErrorCode.EventTooBig && manifestEnvelope.ChunkNumber == 0 && num > 256)
							{
								num /= 2;
								goto IL_00CA;
							}
							goto IL_0141;
						}
						else
						{
							i -= num;
							ptr2[1].Ptr += (ulong)num;
							manifestEnvelope.ChunkNumber += 1;
							if (manifestEnvelope.ChunkNumber % 5 == 0)
							{
								Thread.Sleep(15);
							}
						}
					}
					goto IL_019C;
				}
				IL_0141:
				flag = false;
				if (this.ThrowOnEventWriteErrors)
				{
					this.ThrowEventSourceException("SendManifest", null);
				}
				IL_019C:;
			}
			return flag;
		}

		// Token: 0x06006499 RID: 25753 RVA: 0x00148A58 File Offset: 0x00146C58
		internal static Attribute GetCustomAttributeHelper(MemberInfo member, Type attributeType, EventManifestOptions flags = EventManifestOptions.None)
		{
			if (!member.Module.Assembly.ReflectionOnly() && (flags & EventManifestOptions.AllowEventSourceOverride) == EventManifestOptions.None)
			{
				Attribute attribute = null;
				object[] customAttributes = member.GetCustomAttributes(attributeType, false);
				int num = 0;
				if (num < customAttributes.Length)
				{
					attribute = (Attribute)customAttributes[num];
				}
				return attribute;
			}
			string fullName = attributeType.FullName;
			foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(member))
			{
				if (EventSource.AttributeTypeNamesMatch(attributeType, customAttributeData.Constructor.ReflectedType))
				{
					Attribute attribute2 = null;
					if (customAttributeData.ConstructorArguments.Count == 1)
					{
						attribute2 = (Attribute)Activator.CreateInstance(attributeType, new object[] { customAttributeData.ConstructorArguments[0].Value });
					}
					else if (customAttributeData.ConstructorArguments.Count == 0)
					{
						attribute2 = (Attribute)Activator.CreateInstance(attributeType);
					}
					if (attribute2 != null)
					{
						Type type = attribute2.GetType();
						foreach (CustomAttributeNamedArgument customAttributeNamedArgument in customAttributeData.NamedArguments)
						{
							PropertyInfo property = type.GetProperty(customAttributeNamedArgument.MemberInfo.Name, BindingFlags.Instance | BindingFlags.Public);
							object obj = customAttributeNamedArgument.TypedValue.Value;
							if (property.PropertyType.IsEnum)
							{
								obj = Enum.Parse(property.PropertyType, obj.ToString());
							}
							property.SetValue(attribute2, obj, null);
						}
						return attribute2;
					}
				}
			}
			return null;
		}

		// Token: 0x0600649A RID: 25754 RVA: 0x00148C20 File Offset: 0x00146E20
		private static bool AttributeTypeNamesMatch(Type attributeType, Type reflectedAttributeType)
		{
			return attributeType == reflectedAttributeType || string.Equals(attributeType.FullName, reflectedAttributeType.FullName, StringComparison.Ordinal) || (string.Equals(attributeType.Name, reflectedAttributeType.Name, StringComparison.Ordinal) && attributeType.Namespace.EndsWith("Diagnostics.Tracing") && reflectedAttributeType.Namespace.EndsWith("Diagnostics.Tracing"));
		}

		// Token: 0x0600649B RID: 25755 RVA: 0x00148C84 File Offset: 0x00146E84
		private static Type GetEventSourceBaseType(Type eventSourceType, bool allowEventSourceOverride, bool reflectionOnly)
		{
			if (eventSourceType.BaseType() == null)
			{
				return null;
			}
			do
			{
				eventSourceType = eventSourceType.BaseType();
			}
			while (eventSourceType != null && eventSourceType.IsAbstract());
			if (eventSourceType != null)
			{
				if (!allowEventSourceOverride)
				{
					if ((reflectionOnly && eventSourceType.FullName != typeof(EventSource).FullName) || (!reflectionOnly && eventSourceType != typeof(EventSource)))
					{
						return null;
					}
				}
				else if (eventSourceType.Name != "EventSource")
				{
					return null;
				}
			}
			return eventSourceType;
		}

		// Token: 0x0600649C RID: 25756 RVA: 0x00148D14 File Offset: 0x00146F14
		private static byte[] CreateManifestAndDescriptors(Type eventSourceType, string eventSourceDllName, EventSource source, EventManifestOptions flags = EventManifestOptions.None)
		{
			ManifestBuilder manifestBuilder = null;
			bool flag = source == null || !source.SelfDescribingEvents;
			Exception ex = null;
			byte[] array = null;
			if (eventSourceType.IsAbstract() && (flags & EventManifestOptions.Strict) == EventManifestOptions.None)
			{
				return null;
			}
			try
			{
				MethodInfo[] methods = eventSourceType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				int num = 1;
				EventSource.EventMetadata[] array2 = null;
				Dictionary<string, string> dictionary = null;
				if (source != null || (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
				{
					array2 = new EventSource.EventMetadata[methods.Length + 1];
					array2[0].Name = "";
				}
				ResourceManager resourceManager = null;
				EventSourceAttribute eventSourceAttribute = (EventSourceAttribute)EventSource.GetCustomAttributeHelper(eventSourceType, typeof(EventSourceAttribute), flags);
				if (eventSourceAttribute != null && eventSourceAttribute.LocalizationResources != null)
				{
					resourceManager = new ResourceManager(eventSourceAttribute.LocalizationResources, eventSourceType.Assembly());
				}
				manifestBuilder = new ManifestBuilder(EventSource.GetName(eventSourceType, flags), EventSource.GetGuid(eventSourceType), eventSourceDllName, resourceManager, flags);
				manifestBuilder.StartEvent("EventSourceMessage", new EventAttribute(0)
				{
					Level = EventLevel.LogAlways,
					Task = (EventTask)65534
				});
				manifestBuilder.AddEventParameter(typeof(string), "message");
				manifestBuilder.EndEvent();
				if ((flags & EventManifestOptions.Strict) != EventManifestOptions.None)
				{
					if (!(EventSource.GetEventSourceBaseType(eventSourceType, (flags & EventManifestOptions.AllowEventSourceOverride) > EventManifestOptions.None, eventSourceType.Assembly().ReflectionOnly()) != null))
					{
						manifestBuilder.ManifestError(Environment.GetResourceString("Event source types must derive from EventSource."), false);
					}
					if (!eventSourceType.IsAbstract() && !eventSourceType.IsSealed())
					{
						manifestBuilder.ManifestError(Environment.GetResourceString("Event source types must be sealed or abstract."), false);
					}
				}
				foreach (string text in new string[] { "Keywords", "Tasks", "Opcodes" })
				{
					Type nestedType = eventSourceType.GetNestedType(text);
					if (nestedType != null)
					{
						if (eventSourceType.IsAbstract())
						{
							manifestBuilder.ManifestError(Environment.GetResourceString("Abstract event source must not declare {0} nested type.", new object[] { nestedType.Name }), false);
						}
						else
						{
							foreach (FieldInfo fieldInfo in nestedType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
							{
								EventSource.AddProviderEnumKind(manifestBuilder, fieldInfo, text);
							}
						}
					}
				}
				manifestBuilder.AddKeyword("Session3", 17592186044416UL);
				manifestBuilder.AddKeyword("Session2", 35184372088832UL);
				manifestBuilder.AddKeyword("Session1", 70368744177664UL);
				manifestBuilder.AddKeyword("Session0", 140737488355328UL);
				if (eventSourceType != typeof(EventSource))
				{
					foreach (MethodInfo methodInfo in methods)
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						EventAttribute eventAttribute = (EventAttribute)EventSource.GetCustomAttributeHelper(methodInfo, typeof(EventAttribute), flags);
						if (eventAttribute != null && source != null && eventAttribute.EventId <= 3 && source.Guid.Equals(EventSource.AspNetEventSourceGuid))
						{
							eventAttribute.ActivityOptions |= EventActivityOptions.Disable;
						}
						if (!methodInfo.IsStatic)
						{
							if (eventSourceType.IsAbstract())
							{
								if (eventAttribute != null)
								{
									manifestBuilder.ManifestError(Environment.GetResourceString("Abstract event source must not declare event methods ({0} with ID {1}).", new object[] { methodInfo.Name, eventAttribute.EventId }), false);
								}
							}
							else
							{
								if (eventAttribute == null)
								{
									if (methodInfo.ReturnType != typeof(void) || methodInfo.IsVirtual || EventSource.GetCustomAttributeHelper(methodInfo, typeof(NonEventAttribute), flags) != null)
									{
										goto IL_063A;
									}
									eventAttribute = new EventAttribute(num);
								}
								else if (eventAttribute.EventId <= 0)
								{
									manifestBuilder.ManifestError(Environment.GetResourceString("Event IDs must be positive integers.", new object[] { methodInfo.Name }), true);
									goto IL_063A;
								}
								if (methodInfo.Name.LastIndexOf('.') >= 0)
								{
									manifestBuilder.ManifestError(Environment.GetResourceString("Event method {0} (with ID {1}) is an explicit interface method implementation. Re-write method as implicit implementation.", new object[] { methodInfo.Name, eventAttribute.EventId }), false);
								}
								num++;
								string name = methodInfo.Name;
								if (eventAttribute.Opcode == EventOpcode.Info)
								{
									bool flag2 = eventAttribute.Task == EventTask.None;
									if (flag2)
									{
										eventAttribute.Task = (EventTask)65534 - eventAttribute.EventId;
									}
									if (!eventAttribute.IsOpcodeSet)
									{
										eventAttribute.Opcode = EventSource.GetOpcodeWithDefault(EventOpcode.Info, name);
									}
									if (flag2)
									{
										if (eventAttribute.Opcode == EventOpcode.Start)
										{
											string text2 = name.Substring(0, name.Length - "Start".Length);
											if (string.Compare(name, 0, text2, 0, text2.Length) == 0 && string.Compare(name, text2.Length, "Start", 0, Math.Max(name.Length - text2.Length, "Start".Length)) == 0)
											{
												manifestBuilder.AddTask(text2, (int)eventAttribute.Task);
											}
										}
										else if (eventAttribute.Opcode == EventOpcode.Stop)
										{
											int num2 = eventAttribute.EventId - 1;
											if (array2 != null && num2 < array2.Length)
											{
												EventSource.EventMetadata eventMetadata = array2[num2];
												string text3 = name.Substring(0, name.Length - "Stop".Length);
												if (eventMetadata.Descriptor.Opcode == 1 && string.Compare(eventMetadata.Name, 0, text3, 0, text3.Length) == 0 && string.Compare(eventMetadata.Name, text3.Length, "Start", 0, Math.Max(eventMetadata.Name.Length - text3.Length, "Start".Length)) == 0)
												{
													eventAttribute.Task = (EventTask)eventMetadata.Descriptor.Task;
													flag2 = false;
												}
											}
											if (flag2 && (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
											{
												throw new ArgumentException(Environment.GetResourceString("An event with stop suffix must follow a corresponding event with a start suffix."));
											}
										}
									}
								}
								bool flag3 = EventSource.RemoveFirstArgIfRelatedActivityId(ref parameters);
								if (source == null || !source.SelfDescribingEvents)
								{
									manifestBuilder.StartEvent(name, eventAttribute);
									for (int l = 0; l < parameters.Length; l++)
									{
										manifestBuilder.AddEventParameter(parameters[l].ParameterType, parameters[l].Name);
									}
									manifestBuilder.EndEvent();
								}
								if (source != null || (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
								{
									EventSource.DebugCheckEvent(ref dictionary, array2, methodInfo, eventAttribute, manifestBuilder, flags);
									string text4 = "event_" + name;
									string localizedMessage = manifestBuilder.GetLocalizedMessage(text4, CultureInfo.CurrentUICulture, false);
									if (localizedMessage != null)
									{
										eventAttribute.Message = localizedMessage;
									}
									EventSource.AddEventDescriptor(ref array2, name, eventAttribute, parameters, flag3);
								}
							}
						}
						IL_063A:;
					}
				}
				NameInfo.ReserveEventIDsBelow(num);
				if (source != null)
				{
					EventSource.TrimEventDescriptors(ref array2);
					source.m_eventData = array2;
				}
				if (!eventSourceType.IsAbstract() && (source == null || !source.SelfDescribingEvents))
				{
					flag = (flags & EventManifestOptions.OnlyIfNeededForRegistration) == EventManifestOptions.None;
					if (!flag && (flags & EventManifestOptions.Strict) == EventManifestOptions.None)
					{
						return null;
					}
					array = manifestBuilder.CreateManifest();
				}
			}
			catch (Exception ex2)
			{
				if ((flags & EventManifestOptions.Strict) == EventManifestOptions.None)
				{
					throw;
				}
				ex = ex2;
			}
			if ((flags & EventManifestOptions.Strict) != EventManifestOptions.None && (manifestBuilder.Errors.Count > 0 || ex != null))
			{
				string text5 = string.Empty;
				if (manifestBuilder.Errors.Count > 0)
				{
					bool flag4 = true;
					using (IEnumerator<string> enumerator = manifestBuilder.Errors.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text6 = enumerator.Current;
							if (!flag4)
							{
								text5 += Environment.NewLine;
							}
							flag4 = false;
							text5 += text6;
						}
						goto IL_0738;
					}
				}
				text5 = "Unexpected error: " + ex.Message;
				IL_0738:
				throw new ArgumentException(text5, ex);
			}
			if (!flag)
			{
				return null;
			}
			return array;
		}

		// Token: 0x0600649D RID: 25757 RVA: 0x001494A0 File Offset: 0x001476A0
		private static bool RemoveFirstArgIfRelatedActivityId(ref ParameterInfo[] args)
		{
			if (args.Length != 0 && args[0].ParameterType == typeof(Guid) && string.Compare(args[0].Name, "relatedActivityId", StringComparison.OrdinalIgnoreCase) == 0)
			{
				ParameterInfo[] array = new ParameterInfo[args.Length - 1];
				Array.Copy(args, 1, array, 0, args.Length - 1);
				args = array;
				return true;
			}
			return false;
		}

		// Token: 0x0600649E RID: 25758 RVA: 0x00149504 File Offset: 0x00147704
		private static void AddProviderEnumKind(ManifestBuilder manifest, FieldInfo staticField, string providerEnumKind)
		{
			bool flag = staticField.Module.Assembly.ReflectionOnly();
			Type fieldType = staticField.FieldType;
			if ((!flag && fieldType == typeof(EventOpcode)) || EventSource.AttributeTypeNamesMatch(fieldType, typeof(EventOpcode)))
			{
				if (!(providerEnumKind != "Opcodes"))
				{
					int num = (int)staticField.GetRawConstantValue();
					manifest.AddOpcode(staticField.Name, num);
					return;
				}
			}
			else
			{
				if ((flag || !(fieldType == typeof(EventTask))) && !EventSource.AttributeTypeNamesMatch(fieldType, typeof(EventTask)))
				{
					if ((!flag && fieldType == typeof(EventKeywords)) || EventSource.AttributeTypeNamesMatch(fieldType, typeof(EventKeywords)))
					{
						if (providerEnumKind != "Keywords")
						{
							goto IL_0107;
						}
						ulong num2 = (ulong)((long)staticField.GetRawConstantValue());
						manifest.AddKeyword(staticField.Name, num2);
					}
					return;
				}
				if (!(providerEnumKind != "Tasks"))
				{
					int num3 = (int)staticField.GetRawConstantValue();
					manifest.AddTask(staticField.Name, num3);
					return;
				}
			}
			IL_0107:
			manifest.ManifestError(Environment.GetResourceString("The type of {0} is not expected in {1}.", new object[]
			{
				staticField.Name,
				staticField.FieldType.Name,
				providerEnumKind
			}), false);
		}

		// Token: 0x0600649F RID: 25759 RVA: 0x0014964C File Offset: 0x0014784C
		private static void AddEventDescriptor(ref EventSource.EventMetadata[] eventData, string eventName, EventAttribute eventAttribute, ParameterInfo[] eventParameters, bool hasRelatedActivityID)
		{
			if (eventData == null || eventData.Length <= eventAttribute.EventId)
			{
				EventSource.EventMetadata[] array = new EventSource.EventMetadata[Math.Max(eventData.Length + 16, eventAttribute.EventId + 1)];
				Array.Copy(eventData, array, eventData.Length);
				eventData = array;
			}
			eventData[eventAttribute.EventId].Descriptor = new EventDescriptor(eventAttribute.EventId, eventAttribute.Version, 0, (byte)eventAttribute.Level, (byte)eventAttribute.Opcode, (int)eventAttribute.Task, (long)(eventAttribute.Keywords | (EventKeywords)SessionMask.All.ToEventKeywords()));
			eventData[eventAttribute.EventId].Tags = eventAttribute.Tags;
			eventData[eventAttribute.EventId].Name = eventName;
			eventData[eventAttribute.EventId].Parameters = eventParameters;
			eventData[eventAttribute.EventId].Message = eventAttribute.Message;
			eventData[eventAttribute.EventId].ActivityOptions = eventAttribute.ActivityOptions;
			eventData[eventAttribute.EventId].HasRelatedActivityID = hasRelatedActivityID;
		}

		// Token: 0x060064A0 RID: 25760 RVA: 0x00149760 File Offset: 0x00147960
		private static void TrimEventDescriptors(ref EventSource.EventMetadata[] eventData)
		{
			int num = eventData.Length;
			while (0 < num)
			{
				num--;
				if (eventData[num].Descriptor.EventId != 0)
				{
					break;
				}
			}
			if (eventData.Length - num > 2)
			{
				EventSource.EventMetadata[] array = new EventSource.EventMetadata[num + 1];
				Array.Copy(eventData, array, array.Length);
				eventData = array;
			}
		}

		// Token: 0x060064A1 RID: 25761 RVA: 0x001497B0 File Offset: 0x001479B0
		internal void AddListener(EventListener listener)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				bool[] array = null;
				if (this.m_eventData != null)
				{
					array = new bool[this.m_eventData.Length];
				}
				this.m_Dispatchers = new EventDispatcher(this.m_Dispatchers, array, listener);
				listener.OnEventSourceCreated(this);
			}
		}

		// Token: 0x060064A2 RID: 25762 RVA: 0x00149824 File Offset: 0x00147A24
		private static void DebugCheckEvent(ref Dictionary<string, string> eventsByName, EventSource.EventMetadata[] eventData, MethodInfo method, EventAttribute eventAttribute, ManifestBuilder manifest, EventManifestOptions options)
		{
			int eventId = eventAttribute.EventId;
			string name = method.Name;
			int helperCallFirstArg = EventSource.GetHelperCallFirstArg(method);
			if (helperCallFirstArg >= 0 && eventId != helperCallFirstArg)
			{
				manifest.ManifestError(Environment.GetResourceString("Event {0} is givien event ID {1} but {2} was passed to WriteEvent.", new object[] { name, eventId, helperCallFirstArg }), true);
			}
			if (eventId < eventData.Length && eventData[eventId].Descriptor.EventId != 0)
			{
				manifest.ManifestError(Environment.GetResourceString("Event {0} has ID {1} which is already in use.", new object[]
				{
					name,
					eventId,
					eventData[eventId].Name
				}), true);
			}
			for (int i = 0; i < eventData.Length; i++)
			{
				if (eventData[i].Name != null && eventData[i].Descriptor.Task == (int)eventAttribute.Task && (EventOpcode)eventData[i].Descriptor.Opcode == eventAttribute.Opcode)
				{
					manifest.ManifestError(Environment.GetResourceString("Event {0} (with ID {1}) has the same task/opcode pair as event {2} (with ID {3}).", new object[]
					{
						name,
						eventId,
						eventData[i].Name,
						i
					}), false);
					if ((options & EventManifestOptions.Strict) == EventManifestOptions.None)
					{
						break;
					}
				}
			}
			if (eventAttribute.Opcode != EventOpcode.Info)
			{
				bool flag = false;
				if (eventAttribute.Task == EventTask.None)
				{
					flag = true;
				}
				else
				{
					EventTask eventTask = (EventTask)65534 - eventId;
					if (eventAttribute.Opcode != EventOpcode.Start && eventAttribute.Opcode != EventOpcode.Stop && eventAttribute.Task == eventTask)
					{
						flag = true;
					}
				}
				if (flag)
				{
					manifest.ManifestError(Environment.GetResourceString("Event {0} (with ID {1}) has a non-default opcode but not a task.", new object[] { name, eventId }), false);
				}
			}
			if (eventsByName == null)
			{
				eventsByName = new Dictionary<string, string>();
			}
			if (eventsByName.ContainsKey(name))
			{
				manifest.ManifestError(Environment.GetResourceString("Event name {0} used more than once.  If you wish to overload a method, the overloaded method should have a NonEvent attribute.", new object[] { name }), true);
			}
			eventsByName[name] = name;
		}

		// Token: 0x060064A3 RID: 25763 RVA: 0x00149A04 File Offset: 0x00147C04
		[SecuritySafeCritical]
		private static int GetHelperCallFirstArg(MethodInfo method)
		{
			new ReflectionPermission(ReflectionPermissionFlag.MemberAccess).Assert();
			byte[] ilasByteArray = method.GetMethodBody().GetILAsByteArray();
			int num = -1;
			for (int i = 0; i < ilasByteArray.Length; i++)
			{
				byte b = ilasByteArray[i];
				if (b <= 110)
				{
					switch (b)
					{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					case 6:
					case 7:
					case 8:
					case 9:
					case 10:
					case 11:
					case 12:
					case 13:
					case 20:
					case 37:
						break;
					case 14:
					case 16:
						i++;
						break;
					case 15:
					case 17:
					case 18:
					case 19:
					case 33:
					case 34:
					case 35:
					case 36:
					case 38:
					case 39:
					case 41:
					case 42:
					case 43:
					case 46:
					case 47:
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
						return -1;
					case 21:
					case 22:
					case 23:
					case 24:
					case 25:
					case 26:
					case 27:
					case 28:
					case 29:
					case 30:
						if (i > 0 && ilasByteArray[i - 1] == 2)
						{
							num = (int)(ilasByteArray[i] - 22);
						}
						break;
					case 31:
						if (i > 0 && ilasByteArray[i - 1] == 2)
						{
							num = (int)ilasByteArray[i + 1];
						}
						i++;
						break;
					case 32:
						i += 4;
						break;
					case 40:
						i += 4;
						if (num >= 0)
						{
							for (int j = i + 1; j < ilasByteArray.Length; j++)
							{
								if (ilasByteArray[j] == 42)
								{
									return num;
								}
								if (ilasByteArray[j] != 0)
								{
									break;
								}
							}
						}
						num = -1;
						break;
					case 44:
					case 45:
						num = -1;
						i++;
						break;
					case 57:
					case 58:
						num = -1;
						i += 4;
						break;
					default:
						if (b - 103 > 3 && b - 109 > 1)
						{
							return -1;
						}
						break;
					}
				}
				else if (b - 140 > 1)
				{
					if (b != 162)
					{
						if (b != 254)
						{
							return -1;
						}
						i++;
						if (i >= ilasByteArray.Length || ilasByteArray[i] >= 6)
						{
							return -1;
						}
					}
				}
				else
				{
					i += 4;
				}
			}
			return -1;
		}

		// Token: 0x060064A4 RID: 25764 RVA: 0x00149C18 File Offset: 0x00147E18
		internal void ReportOutOfBandMessage(string msg, bool flush)
		{
			try
			{
				Debugger.Log(0, null, msg + "\r\n");
				if (this.m_outOfBandMessageCount < 15)
				{
					this.m_outOfBandMessageCount += 1;
				}
				else
				{
					if (this.m_outOfBandMessageCount == 16)
					{
						return;
					}
					this.m_outOfBandMessageCount = 16;
					msg = "Reached message limit.   End of EventSource error messages.";
				}
				this.WriteEventString(EventLevel.LogAlways, -1L, msg);
				this.WriteStringToAllListeners("EventSourceMessage", msg);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060064A5 RID: 25765 RVA: 0x00149C98 File Offset: 0x00147E98
		private EventSourceSettings ValidateSettings(EventSourceSettings settings)
		{
			EventSourceSettings eventSourceSettings = EventSourceSettings.EtwManifestEventFormat | EventSourceSettings.EtwSelfDescribingEventFormat;
			if ((settings & eventSourceSettings) == eventSourceSettings)
			{
				throw new ArgumentException(Environment.GetResourceString("Can't specify both etw event format flags."), "settings");
			}
			if ((settings & eventSourceSettings) == EventSourceSettings.Default)
			{
				settings |= EventSourceSettings.EtwSelfDescribingEventFormat;
			}
			return settings;
		}

		// Token: 0x170011DF RID: 4575
		// (get) Token: 0x060064A6 RID: 25766 RVA: 0x00149CCE File Offset: 0x00147ECE
		// (set) Token: 0x060064A7 RID: 25767 RVA: 0x00149CDB File Offset: 0x00147EDB
		private bool ThrowOnEventWriteErrors
		{
			get
			{
				return (this.m_config & EventSourceSettings.ThrowOnEventWriteErrors) > EventSourceSettings.Default;
			}
			set
			{
				if (value)
				{
					this.m_config |= EventSourceSettings.ThrowOnEventWriteErrors;
					return;
				}
				this.m_config &= ~EventSourceSettings.ThrowOnEventWriteErrors;
			}
		}

		// Token: 0x170011E0 RID: 4576
		// (get) Token: 0x060064A8 RID: 25768 RVA: 0x00149CFE File Offset: 0x00147EFE
		// (set) Token: 0x060064A9 RID: 25769 RVA: 0x00149D0B File Offset: 0x00147F0B
		private bool SelfDescribingEvents
		{
			get
			{
				return (this.m_config & EventSourceSettings.EtwSelfDescribingEventFormat) > EventSourceSettings.Default;
			}
			set
			{
				if (!value)
				{
					this.m_config |= EventSourceSettings.EtwManifestEventFormat;
					this.m_config &= ~EventSourceSettings.EtwSelfDescribingEventFormat;
					return;
				}
				this.m_config |= EventSourceSettings.EtwSelfDescribingEventFormat;
				this.m_config &= ~EventSourceSettings.EtwManifestEventFormat;
			}
		}

		// Token: 0x060064AA RID: 25770 RVA: 0x00149D4C File Offset: 0x00147F4C
		private void ReportActivitySamplingInfo(EventListener listener, SessionMask sessions)
		{
			int num = 0;
			while ((long)num < 4L)
			{
				if (sessions[num])
				{
					ActivityFilter activityFilter;
					if (listener == null)
					{
						activityFilter = this.m_etwSessionIdMap[num].m_activityFilter;
					}
					else
					{
						activityFilter = listener.m_activityFilter;
					}
					if (activityFilter != null)
					{
						SessionMask sessionMask = default(SessionMask);
						sessionMask[num] = true;
						foreach (Tuple<int, int> tuple in activityFilter.GetFilterAsTuple(this.m_guid))
						{
							this.WriteStringToListener(listener, string.Format(CultureInfo.InvariantCulture, "Session {0}: {1} = {2}", num, tuple.Item1, tuple.Item2), sessionMask);
						}
						bool flag = ((listener == null) ? this.m_activityFilteringForETWEnabled[num] : this.GetDispatcher(listener).m_activityFilteringEnabled);
						this.WriteStringToListener(listener, string.Format(CultureInfo.InvariantCulture, "Session {0}: Activity Sampling support: {1}", num, flag ? "enabled" : "disabled"), sessionMask);
					}
				}
				num++;
			}
		}

		// Token: 0x040031BF RID: 12735
		private byte[] providerMetadata;

		// Token: 0x040031C0 RID: 12736
		private string m_name;

		// Token: 0x040031C1 RID: 12737
		internal int m_id;

		// Token: 0x040031C2 RID: 12738
		private Guid m_guid;

		// Token: 0x040031C3 RID: 12739
		internal volatile EventSource.EventMetadata[] m_eventData;

		// Token: 0x040031C4 RID: 12740
		private volatile byte[] m_rawManifest;

		// Token: 0x040031C5 RID: 12741
		private EventHandler<EventCommandEventArgs> m_eventCommandExecuted;

		// Token: 0x040031C6 RID: 12742
		private EventSourceSettings m_config;

		// Token: 0x040031C7 RID: 12743
		private bool m_eventSourceEnabled;

		// Token: 0x040031C8 RID: 12744
		internal EventLevel m_level;

		// Token: 0x040031C9 RID: 12745
		internal EventKeywords m_matchAnyKeyword;

		// Token: 0x040031CA RID: 12746
		internal volatile EventDispatcher m_Dispatchers;

		// Token: 0x040031CB RID: 12747
		private volatile EventSource.OverideEventProvider m_provider;

		// Token: 0x040031CC RID: 12748
		private bool m_completelyInited;

		// Token: 0x040031CD RID: 12749
		private Exception m_constructionException;

		// Token: 0x040031CE RID: 12750
		private byte m_outOfBandMessageCount;

		// Token: 0x040031CF RID: 12751
		private EventCommandEventArgs m_deferredCommands;

		// Token: 0x040031D0 RID: 12752
		private string[] m_traits;

		// Token: 0x040031D1 RID: 12753
		internal static uint s_currentPid;

		// Token: 0x040031D2 RID: 12754
		[ThreadStatic]
		private static byte m_EventSourceExceptionRecurenceCount = 0;

		// Token: 0x040031D3 RID: 12755
		private SessionMask m_curLiveSessions;

		// Token: 0x040031D4 RID: 12756
		private EtwSession[] m_etwSessionIdMap;

		// Token: 0x040031D5 RID: 12757
		private List<EtwSession> m_legacySessions;

		// Token: 0x040031D6 RID: 12758
		internal long m_keywordTriggers;

		// Token: 0x040031D7 RID: 12759
		internal SessionMask m_activityFilteringForETWEnabled;

		// Token: 0x040031D8 RID: 12760
		internal static Action<Guid> s_activityDying;

		// Token: 0x040031D9 RID: 12761
		private ActivityTracker m_activityTracker;

		// Token: 0x040031DA RID: 12762
		internal const string s_ActivityStartSuffix = "Start";

		// Token: 0x040031DB RID: 12763
		internal const string s_ActivityStopSuffix = "Stop";

		// Token: 0x040031DC RID: 12764
		private static readonly byte[] namespaceBytes = new byte[]
		{
			72, 44, 45, 178, 195, 144, 71, 200, 135, 248,
			26, 21, 191, 193, 48, 251
		};

		// Token: 0x040031DD RID: 12765
		private static readonly Guid AspNetEventSourceGuid = new Guid("ee799f41-cfa5-550b-bf2c-344747c1c668");

		/// <summary>Provides the event data for creating fast <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overloads by using the <see cref="M:System.Diagnostics.Tracing.EventSource.WriteEventCore(System.Int32,System.Int32,System.Diagnostics.Tracing.EventSource.EventData*)" /> method.</summary>
		// Token: 0x02000AE7 RID: 2791
		protected internal struct EventData
		{
			/// <summary>Gets or sets the pointer to the data for the new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload.</summary>
			/// <returns>The pointer to the data.</returns>
			// Token: 0x170011E1 RID: 4577
			// (get) Token: 0x060064AC RID: 25772 RVA: 0x00149E9E File Offset: 0x0014809E
			// (set) Token: 0x060064AD RID: 25773 RVA: 0x00149EAB File Offset: 0x001480AB
			public IntPtr DataPointer
			{
				get
				{
					return (IntPtr)this.m_Ptr;
				}
				set
				{
					this.m_Ptr = (long)value;
				}
			}

			/// <summary>Gets or sets the number of payload items in the new <see cref="Overload:System.Diagnostics.Tracing.EventSource.WriteEvent" /> overload.</summary>
			/// <returns>The number of payload items in the new overload.</returns>
			// Token: 0x170011E2 RID: 4578
			// (get) Token: 0x060064AE RID: 25774 RVA: 0x00149EB9 File Offset: 0x001480B9
			// (set) Token: 0x060064AF RID: 25775 RVA: 0x00149EC1 File Offset: 0x001480C1
			public int Size
			{
				get
				{
					return this.m_Size;
				}
				set
				{
					this.m_Size = value;
				}
			}

			// Token: 0x060064B0 RID: 25776 RVA: 0x00149ECA File Offset: 0x001480CA
			[SecurityCritical]
			internal unsafe void SetMetadata(byte* pointer, int size, int reserved)
			{
				this.m_Ptr = (long)(ulong)((UIntPtr)((void*)pointer));
				this.m_Size = size;
				this.m_Reserved = reserved;
			}

			// Token: 0x040031DE RID: 12766
			internal long m_Ptr;

			// Token: 0x040031DF RID: 12767
			internal int m_Size;

			// Token: 0x040031E0 RID: 12768
			internal int m_Reserved;
		}

		// Token: 0x02000AE8 RID: 2792
		private struct Sha1ForNonSecretPurposes
		{
			// Token: 0x060064B1 RID: 25777 RVA: 0x00149EEC File Offset: 0x001480EC
			public void Start()
			{
				if (this.w == null)
				{
					this.w = new uint[85];
				}
				this.length = 0L;
				this.pos = 0;
				this.w[80] = 1732584193U;
				this.w[81] = 4023233417U;
				this.w[82] = 2562383102U;
				this.w[83] = 271733878U;
				this.w[84] = 3285377520U;
			}

			// Token: 0x060064B2 RID: 25778 RVA: 0x00149F64 File Offset: 0x00148164
			public void Append(byte input)
			{
				this.w[this.pos / 4] = (this.w[this.pos / 4] << 8) | (uint)input;
				int num = 64;
				int num2 = this.pos + 1;
				this.pos = num2;
				if (num == num2)
				{
					this.Drain();
				}
			}

			// Token: 0x060064B3 RID: 25779 RVA: 0x00149FB0 File Offset: 0x001481B0
			public void Append(byte[] input)
			{
				foreach (byte b in input)
				{
					this.Append(b);
				}
			}

			// Token: 0x060064B4 RID: 25780 RVA: 0x00149FD8 File Offset: 0x001481D8
			public void Finish(byte[] output)
			{
				long num = this.length + (long)(8 * this.pos);
				this.Append(128);
				while (this.pos != 56)
				{
					this.Append(0);
				}
				this.Append((byte)(num >> 56));
				this.Append((byte)(num >> 48));
				this.Append((byte)(num >> 40));
				this.Append((byte)(num >> 32));
				this.Append((byte)(num >> 24));
				this.Append((byte)(num >> 16));
				this.Append((byte)(num >> 8));
				this.Append((byte)num);
				int num2 = ((output.Length < 20) ? output.Length : 20);
				for (int num3 = 0; num3 != num2; num3++)
				{
					uint num4 = this.w[80 + num3 / 4];
					output[num3] = (byte)(num4 >> 24);
					this.w[80 + num3 / 4] = num4 << 8;
				}
			}

			// Token: 0x060064B5 RID: 25781 RVA: 0x0014A0AC File Offset: 0x001482AC
			private void Drain()
			{
				for (int num = 16; num != 80; num++)
				{
					this.w[num] = EventSource.Sha1ForNonSecretPurposes.Rol1(this.w[num - 3] ^ this.w[num - 8] ^ this.w[num - 14] ^ this.w[num - 16]);
				}
				uint num2 = this.w[80];
				uint num3 = this.w[81];
				uint num4 = this.w[82];
				uint num5 = this.w[83];
				uint num6 = this.w[84];
				for (int num7 = 0; num7 != 20; num7++)
				{
					uint num8 = (num3 & num4) | (~num3 & num5);
					uint num9 = EventSource.Sha1ForNonSecretPurposes.Rol5(num2) + num8 + num6 + 1518500249U + this.w[num7];
					num6 = num5;
					num5 = num4;
					num4 = EventSource.Sha1ForNonSecretPurposes.Rol30(num3);
					num3 = num2;
					num2 = num9;
				}
				for (int num10 = 20; num10 != 40; num10++)
				{
					uint num11 = num3 ^ num4 ^ num5;
					uint num12 = EventSource.Sha1ForNonSecretPurposes.Rol5(num2) + num11 + num6 + 1859775393U + this.w[num10];
					num6 = num5;
					num5 = num4;
					num4 = EventSource.Sha1ForNonSecretPurposes.Rol30(num3);
					num3 = num2;
					num2 = num12;
				}
				for (int num13 = 40; num13 != 60; num13++)
				{
					uint num14 = (num3 & num4) | (num3 & num5) | (num4 & num5);
					uint num15 = EventSource.Sha1ForNonSecretPurposes.Rol5(num2) + num14 + num6 + 2400959708U + this.w[num13];
					num6 = num5;
					num5 = num4;
					num4 = EventSource.Sha1ForNonSecretPurposes.Rol30(num3);
					num3 = num2;
					num2 = num15;
				}
				for (int num16 = 60; num16 != 80; num16++)
				{
					uint num17 = num3 ^ num4 ^ num5;
					uint num18 = EventSource.Sha1ForNonSecretPurposes.Rol5(num2) + num17 + num6 + 3395469782U + this.w[num16];
					num6 = num5;
					num5 = num4;
					num4 = EventSource.Sha1ForNonSecretPurposes.Rol30(num3);
					num3 = num2;
					num2 = num18;
				}
				this.w[80] += num2;
				this.w[81] += num3;
				this.w[82] += num4;
				this.w[83] += num5;
				this.w[84] += num6;
				this.length += 512L;
				this.pos = 0;
			}

			// Token: 0x060064B6 RID: 25782 RVA: 0x0014A2D0 File Offset: 0x001484D0
			private static uint Rol1(uint input)
			{
				return (input << 1) | (input >> 31);
			}

			// Token: 0x060064B7 RID: 25783 RVA: 0x0014A2DA File Offset: 0x001484DA
			private static uint Rol5(uint input)
			{
				return (input << 5) | (input >> 27);
			}

			// Token: 0x060064B8 RID: 25784 RVA: 0x0014A2E4 File Offset: 0x001484E4
			private static uint Rol30(uint input)
			{
				return (input << 30) | (input >> 2);
			}

			// Token: 0x040031E1 RID: 12769
			private long length;

			// Token: 0x040031E2 RID: 12770
			private uint[] w;

			// Token: 0x040031E3 RID: 12771
			private int pos;
		}

		// Token: 0x02000AE9 RID: 2793
		private class OverideEventProvider : EventProvider
		{
			// Token: 0x060064B9 RID: 25785 RVA: 0x0014A2EE File Offset: 0x001484EE
			public OverideEventProvider(EventSource eventSource)
			{
				this.m_eventSource = eventSource;
			}

			// Token: 0x060064BA RID: 25786 RVA: 0x0014A300 File Offset: 0x00148500
			protected override void OnControllerCommand(ControllerCommand command, IDictionary<string, string> arguments, int perEventSourceSessionId, int etwSessionId)
			{
				EventListener eventListener = null;
				this.m_eventSource.SendCommand(eventListener, perEventSourceSessionId, etwSessionId, (EventCommand)command, base.IsEnabled(), base.Level, base.MatchAnyKeyword, arguments);
			}

			// Token: 0x040031E4 RID: 12772
			private EventSource m_eventSource;
		}

		// Token: 0x02000AEA RID: 2794
		internal struct EventMetadata
		{
			// Token: 0x040031E5 RID: 12773
			public EventDescriptor Descriptor;

			// Token: 0x040031E6 RID: 12774
			public EventTags Tags;

			// Token: 0x040031E7 RID: 12775
			public bool EnabledForAnyListener;

			// Token: 0x040031E8 RID: 12776
			public bool EnabledForETW;

			// Token: 0x040031E9 RID: 12777
			public bool HasRelatedActivityID;

			// Token: 0x040031EA RID: 12778
			public byte TriggersActivityTracking;

			// Token: 0x040031EB RID: 12779
			public string Name;

			// Token: 0x040031EC RID: 12780
			public string Message;

			// Token: 0x040031ED RID: 12781
			public ParameterInfo[] Parameters;

			// Token: 0x040031EE RID: 12782
			public TraceLoggingEventTypes TraceLoggingEventTypes;

			// Token: 0x040031EF RID: 12783
			public EventActivityOptions ActivityOptions;
		}
	}
}
