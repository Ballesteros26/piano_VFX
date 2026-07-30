using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics
{
	/// <summary>Encapsulates a single record in the event log. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001F1 RID: 497
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
	[Serializable]
	public sealed class EventLogEntry : Component, ISerializable
	{
		// Token: 0x06000FE7 RID: 4071 RVA: 0x00048E50 File Offset: 0x00047050
		internal EventLogEntry(string category, short categoryNumber, int index, int eventID, string source, string message, string userName, string machineName, EventLogEntryType entryType, DateTime timeGenerated, DateTime timeWritten, byte[] data, string[] replacementStrings, long instanceId)
		{
			this.category = category;
			this.categoryNumber = categoryNumber;
			this.data = data;
			this.entryType = entryType;
			this.eventID = eventID;
			this.index = index;
			this.machineName = machineName;
			this.message = message;
			this.replacementStrings = replacementStrings;
			this.source = source;
			this.timeGenerated = timeGenerated;
			this.timeWritten = timeWritten;
			this.userName = userName;
			this.instanceId = instanceId;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x00048ED0 File Offset: 0x000470D0
		[MonoTODO]
		private EventLogEntry(SerializationInfo info, StreamingContext context)
		{
		}

		/// <summary>Gets the text associated with the <see cref="P:System.Diagnostics.EventLogEntry.CategoryNumber" /> property for this entry.</summary>
		/// <returns>The application-specific category text.</returns>
		/// <exception cref="T:System.Exception">The space could not be allocated for one of the insertion strings associated with the category. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x00048ED8 File Offset: 0x000470D8
		[MonitoringDescription("The category of this event entry.")]
		public string Category
		{
			get
			{
				return this.category;
			}
		}

		/// <summary>Gets the category number of the event log entry.</summary>
		/// <returns>The application-specific category number for this entry.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000FEA RID: 4074 RVA: 0x00048EE0 File Offset: 0x000470E0
		[MonitoringDescription("An ID for the category of this event entry.")]
		public short CategoryNumber
		{
			get
			{
				return this.categoryNumber;
			}
		}

		/// <summary>Gets the binary data associated with the entry.</summary>
		/// <returns>An array of bytes that holds the binary data associated with the entry.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x00048EE8 File Offset: 0x000470E8
		[MonitoringDescription("Binary data associated with this event entry.")]
		public byte[] Data
		{
			get
			{
				return this.data;
			}
		}

		/// <summary>Gets the event type of this entry.</summary>
		/// <returns>The event type that is associated with the entry in the event log.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x00048EF0 File Offset: 0x000470F0
		[MonitoringDescription("The type of this event entry.")]
		public EventLogEntryType EntryType
		{
			get
			{
				return this.entryType;
			}
		}

		/// <summary>Gets the application-specific event identifier for the current event entry.</summary>
		/// <returns>The application-specific identifier for the event message.</returns>
		/// <filterpriority>3</filterpriority>
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x00048EF8 File Offset: 0x000470F8
		[MonitoringDescription("An ID number for this event entry.")]
		[Obsolete("Use InstanceId")]
		public int EventID
		{
			get
			{
				return this.eventID;
			}
		}

		/// <summary>Gets the index of this entry in the event log.</summary>
		/// <returns>The index of this entry in the event log.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x00048F00 File Offset: 0x00047100
		[MonitoringDescription("Sequence numer of this event entry.")]
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		/// <summary>Gets the resource identifier that designates the message text of the event entry.</summary>
		/// <returns>A resource identifier that corresponds to a string definition in the message resource file of the event source.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000FEF RID: 4079 RVA: 0x00048F08 File Offset: 0x00047108
		[ComVisible(false)]
		[MonitoringDescription("The instance ID for this event entry.")]
		public long InstanceId
		{
			get
			{
				return this.instanceId;
			}
		}

		/// <summary>Gets the name of the computer on which this entry was generated.</summary>
		/// <returns>The name of the computer that contains the event log.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x00048F10 File Offset: 0x00047110
		[MonitoringDescription("The Computer on which this event entry occured.")]
		public string MachineName
		{
			get
			{
				return this.machineName;
			}
		}

		/// <summary>Gets the localized message associated with this event entry.</summary>
		/// <returns>The formatted, localized text for the message. This includes associated replacement strings.</returns>
		/// <exception cref="T:System.Exception">The space could not be allocated for one of the insertion strings associated with the message. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.EventLogPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000FF1 RID: 4081 RVA: 0x00048F18 File Offset: 0x00047118
		[MonitoringDescription("The message of this event entry.")]
		[Editor("System.ComponentModel.Design.BinaryEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		/// <summary>Gets the replacement strings associated with the event log entry.</summary>
		/// <returns>An array that holds the replacement strings stored in the event entry.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x00048F20 File Offset: 0x00047120
		[MonitoringDescription("Application strings for this event entry.")]
		public string[] ReplacementStrings
		{
			get
			{
				return this.replacementStrings;
			}
		}

		/// <summary>Gets the name of the application that generated this event.</summary>
		/// <returns>The name registered with the event log as the source of this event.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000FF3 RID: 4083 RVA: 0x00048F28 File Offset: 0x00047128
		[MonitoringDescription("The source application of this event entry.")]
		public string Source
		{
			get
			{
				return this.source;
			}
		}

		/// <summary>Gets the local time at which this event was generated.</summary>
		/// <returns>The local time at which this event was generated.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x00048F30 File Offset: 0x00047130
		[MonitoringDescription("Generation time of this event entry.")]
		public DateTime TimeGenerated
		{
			get
			{
				return this.timeGenerated;
			}
		}

		/// <summary>Gets the local time at which this event was written to the log.</summary>
		/// <returns>The local time at which this event was written to the log.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x00048F38 File Offset: 0x00047138
		[MonitoringDescription("The time at which this event entry was written to the logfile.")]
		public DateTime TimeWritten
		{
			get
			{
				return this.timeWritten;
			}
		}

		/// <summary>Gets the name of the user who is responsible for this event.</summary>
		/// <returns>The security identifier (SID) that uniquely identifies a user or group.</returns>
		/// <exception cref="T:System.SystemException">Account information could not be obtained for the user's SID. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00048F40 File Offset: 0x00047140
		[MonitoringDescription("The name of a user associated with this event entry.")]
		public string UserName
		{
			get
			{
				return this.userName;
			}
		}

		/// <summary>Performs a comparison between two event log entries.</summary>
		/// <returns>true if the <see cref="T:System.Diagnostics.EventLogEntry" /> objects are identical; otherwise, false.</returns>
		/// <param name="otherEntry">The <see cref="T:System.Diagnostics.EventLogEntry" /> to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000FF7 RID: 4087 RVA: 0x00048F48 File Offset: 0x00047148
		public bool Equals(EventLogEntry otherEntry)
		{
			return otherEntry == this || (otherEntry.Category == this.category && otherEntry.CategoryNumber == this.categoryNumber && otherEntry.Data.Equals(this.data) && otherEntry.EntryType == this.entryType && otherEntry.InstanceId == this.instanceId && otherEntry.Index == this.index && otherEntry.MachineName == this.machineName && otherEntry.Message == this.message && otherEntry.ReplacementStrings.Equals(this.replacementStrings) && otherEntry.Source == this.source && otherEntry.TimeGenerated.Equals(this.timeGenerated) && otherEntry.TimeWritten.Equals(this.timeWritten) && otherEntry.UserName == this.userName);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for this serialization. </param>
		// Token: 0x06000FF8 RID: 4088 RVA: 0x00004239 File Offset: 0x00002439
		[MonoTODO("Needs serialization support")]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal EventLogEntry()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001130 RID: 4400
		private string category;

		// Token: 0x04001131 RID: 4401
		private short categoryNumber;

		// Token: 0x04001132 RID: 4402
		private byte[] data;

		// Token: 0x04001133 RID: 4403
		private EventLogEntryType entryType;

		// Token: 0x04001134 RID: 4404
		private int eventID;

		// Token: 0x04001135 RID: 4405
		private int index;

		// Token: 0x04001136 RID: 4406
		private string machineName;

		// Token: 0x04001137 RID: 4407
		private string message;

		// Token: 0x04001138 RID: 4408
		private string[] replacementStrings;

		// Token: 0x04001139 RID: 4409
		private string source;

		// Token: 0x0400113A RID: 4410
		private DateTime timeGenerated;

		// Token: 0x0400113B RID: 4411
		private DateTime timeWritten;

		// Token: 0x0400113C RID: 4412
		private string userName;

		// Token: 0x0400113D RID: 4413
		private long instanceId;
	}
}
