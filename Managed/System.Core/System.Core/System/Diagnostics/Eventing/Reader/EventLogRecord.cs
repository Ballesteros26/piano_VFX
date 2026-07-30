using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains the properties of an event instance for an event that is received from an <see cref="T:System.Diagnostics.Eventing.Reader.EventLogReader" /> object. The event properties provide information about the event such as the name of the computer where the event was logged and the time that the event was created.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200039E RID: 926
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventLogRecord : EventRecord
	{
		// Token: 0x06001B65 RID: 7013 RVA: 0x0000220F File Offset: 0x0000040F
		internal EventLogRecord()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the globally unique identifier (GUID) for the activity in process for which the event is involved. This allows consumers to group related activities.</summary>
		/// <returns>Returns a GUID value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x000568A4 File Offset: 0x00054AA4
		public override Guid? ActivityId
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a placeholder (bookmark) that corresponds to this event. This can be used as a placeholder in a stream of events.</summary>
		/// <returns>Returns a <see cref="T:System.Diagnostics.Eventing.Reader.EventBookmark" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x06001B67 RID: 7015 RVA: 0x000560B4 File Offset: 0x000542B4
		public override EventBookmark Bookmark
		{
			[SecuritySafeCritical]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the name of the event log or the event log file in which the event is stored.</summary>
		/// <returns>Returns a string that contains the name of the event log or the event log file in which the event is stored.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x000560B4 File Offset: 0x000542B4
		public string ContainerLog
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the identifier for this event. All events with this identifier value represent the same type of event.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06001B69 RID: 7017 RVA: 0x000568C0 File Offset: 0x00054AC0
		public override int Id
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the keyword mask of the event. Get the value of the <see cref="P:System.Diagnostics.Eventing.Reader.EventLogRecord.KeywordsDisplayNames" /> property to get the name of the keywords used in this mask.</summary>
		/// <returns>Returns a long value. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x000568DC File Offset: 0x00054ADC
		public override long? Keywords
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the display names of the keywords used in the keyword mask for this event.</summary>
		/// <returns>Returns an enumerable collection of strings that contain the display names of the keywords used in the keyword mask for this event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x0005672F File Offset: 0x0005492F
		public override IEnumerable<string> KeywordsDisplayNames
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the level of the event. The level signifies the severity of the event. For the name of the level, get the value of the <see cref="P:System.Diagnostics.Eventing.Reader.EventLogRecord.LevelDisplayName" /> property.</summary>
		/// <returns>Returns a byte value. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x000568F8 File Offset: 0x00054AF8
		public override byte? Level
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the display name of the level for this event.</summary>
		/// <returns>Returns a string that contains the display name of the level for this event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x000560B4 File Offset: 0x000542B4
		public override string LevelDisplayName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the name of the event log where this event is logged.</summary>
		/// <returns>Returns a string that contains a name of the event log that contains this event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06001B6E RID: 7022 RVA: 0x000560B4 File Offset: 0x000542B4
		public override string LogName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the name of the computer on which this event was logged.</summary>
		/// <returns>Returns a string that contains the name of the computer on which this event was logged.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x000560B4 File Offset: 0x000542B4
		public override string MachineName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a list of query identifiers that this event matches. This event matches a query if the query would return this event.</summary>
		/// <returns>Returns an enumerable collection of integer values.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x0005672F File Offset: 0x0005492F
		public IEnumerable<int> MatchedQueryIds
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the opcode of the event. The opcode defines a numeric value that identifies the activity or a point within an activity that the application was performing when it raised the event. For the name of the opcode, get the value of the <see cref="P:System.Diagnostics.Eventing.Reader.EventLogRecord.OpcodeDisplayName" /> property.</summary>
		/// <returns>Returns a short value. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x00056914 File Offset: 0x00054B14
		public override short? Opcode
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the display name of the opcode for this event.</summary>
		/// <returns>Returns a string that contains the display name of the opcode for this event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x000560B4 File Offset: 0x000542B4
		public override string OpcodeDisplayName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the process identifier for the event provider that logged this event.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x00056930 File Offset: 0x00054B30
		public override int? ProcessId
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the user-supplied properties of the event.</summary>
		/// <returns>Returns a list of <see cref="T:System.Diagnostics.Eventing.Reader.EventProperty" /> objects.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x0005672F File Offset: 0x0005492F
		public override IList<EventProperty> Properties
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the globally unique identifier (GUID) of the event provider that published this event.</summary>
		/// <returns>Returns a GUID value. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0005694C File Offset: 0x00054B4C
		public override Guid? ProviderId
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the name of the event provider that published this event.</summary>
		/// <returns>Returns a string that contains the name of the event provider that published this event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001B76 RID: 7030 RVA: 0x000560B4 File Offset: 0x000542B4
		public override string ProviderName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets qualifier numbers that are used for event identification.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001B77 RID: 7031 RVA: 0x00056968 File Offset: 0x00054B68
		public override int? Qualifiers
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the event record identifier of the event in the log.</summary>
		/// <returns>Returns a long value. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001B78 RID: 7032 RVA: 0x00056984 File Offset: 0x00054B84
		public override long? RecordId
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a globally unique identifier (GUID) for a related activity in a process for which an event is involved.</summary>
		/// <returns>Returns a GUID value. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001B79 RID: 7033 RVA: 0x000569A0 File Offset: 0x00054BA0
		public override Guid? RelatedActivityId
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets a task identifier for a portion of an application or a component that publishes an event. A task is a 16-bit value with 16 top values reserved. This type allows any value between 0x0000 and 0xffef to be used. For the name of the task, get the value of the <see cref="P:System.Diagnostics.Eventing.Reader.EventLogRecord.TaskDisplayName" /> property.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x06001B7A RID: 7034 RVA: 0x000569BC File Offset: 0x00054BBC
		public override int? Task
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the display name of the task for the event.</summary>
		/// <returns>Returns a string that contains the display name of the task for the event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x06001B7B RID: 7035 RVA: 0x000560B4 File Offset: 0x000542B4
		public override string TaskDisplayName
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the thread identifier for the thread that the event provider is running in.</summary>
		/// <returns>Returns an integer value. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x06001B7C RID: 7036 RVA: 0x000569D8 File Offset: 0x00054BD8
		public override int? ThreadId
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the time, in <see cref="T:System.DateTime" /> format, that the event was created.</summary>
		/// <returns>Returns a <see cref="T:System.DateTime" /> value. The value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x000569F4 File Offset: 0x00054BF4
		public override DateTime? TimeCreated
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the security descriptor of the user whose context is used to publish the event.</summary>
		/// <returns>Returns a <see cref="T:System.Security.Principal.SecurityIdentifier" /> value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x000560B4 File Offset: 0x000542B4
		public override SecurityIdentifier UserId
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the version number for the event.</summary>
		/// <returns>Returns a byte value. This value can be null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001B7F RID: 7039 RVA: 0x00056A10 File Offset: 0x00054C10
		public override byte? Version
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Releases the unmanaged resources used by this object, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001B80 RID: 7040 RVA: 0x0000220F File Offset: 0x0000040F
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the event message in the current locale.</summary>
		/// <returns>Returns a string that contains the event message in the current locale.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B81 RID: 7041 RVA: 0x000560B4 File Offset: 0x000542B4
		public override string FormatDescription()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the event message, replacing variables in the message with the specified values.</summary>
		/// <returns>Returns a string that contains the event message in the current locale.</returns>
		/// <param name="values">The values used to replace variables in the event message. Variables are represented by %n, where n is a number.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B82 RID: 7042 RVA: 0x000560B4 File Offset: 0x000542B4
		public override string FormatDescription(IEnumerable<object> values)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}

		/// <summary>Gets the enumeration of the values of the user-supplied event properties, or the results of XPath-based data if the event has XML representation.</summary>
		/// <returns>Returns a list of objects.</returns>
		/// <param name="propertySelector">Selects the property values to return.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B83 RID: 7043 RVA: 0x0005672F File Offset: 0x0005492F
		public IList<object> GetPropertyValues(EventLogPropertySelector propertySelector)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return 0;
		}

		/// <summary>Gets the XML representation of the event. All of the event properties are represented in the event's XML. The XML conforms to the event schema.</summary>
		/// <returns>Returns a string that contains the XML representation of the event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001B84 RID: 7044 RVA: 0x000560B4 File Offset: 0x000542B4
		[SecuritySafeCritical]
		public override string ToXml()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
			return null;
		}
	}
}
