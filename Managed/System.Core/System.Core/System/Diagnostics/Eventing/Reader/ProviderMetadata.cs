using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing.Reader
{
	/// <summary>Contains static information about an event provider, such as the name and id of the provider, and the collection of events defined in the provider.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020003A4 RID: 932
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class ProviderMetadata : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.ProviderMetadata" /> class by specifying the name of the provider that you want to retrieve information about.</summary>
		/// <param name="providerName">The name of the event provider that you want to retrieve information about.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001BA5 RID: 7077 RVA: 0x0000220F File Offset: 0x0000040F
		public ProviderMetadata(string providerName)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.Reader.ProviderMetadata" /> class by specifying the name of the provider that you want to retrieve information about, the event log service that the provider is registered with, and the language that you want to return the information in.</summary>
		/// <param name="providerName">The name of the event provider that you want to retrieve information about.</param>
		/// <param name="session">The <see cref="T:System.Diagnostics.Eventing.Reader.EventLogSession" /> object that specifies whether to get the provider information from a provider on the local computer or a provider on a remote computer.</param>
		/// <param name="targetCultureInfo">The culture that specifies the language that the information should be returned in.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001BA6 RID: 7078 RVA: 0x0000220F File Offset: 0x0000040F
		public ProviderMetadata(string providerName, EventLogSession session, CultureInfo targetCultureInfo)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets the localized name of the event provider.</summary>
		/// <returns>Returns a string that contains the localized name of the event provider.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06001BA7 RID: 7079 RVA: 0x000560B4 File Offset: 0x000542B4
		public string DisplayName
		{
			[SecurityCritical]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventMetadata" /> objects, each of which represents an event that is defined in the provider.</summary>
		/// <returns>Returns an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventMetadata" /> objects.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06001BA8 RID: 7080 RVA: 0x0005672F File Offset: 0x0005492F
		public IEnumerable<EventMetadata> Events
		{
			[SecurityCritical]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the base of the URL used to form help requests for the events in this event provider.</summary>
		/// <returns>Returns a <see cref="T:System.Uri" /> value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06001BA9 RID: 7081 RVA: 0x000560B4 File Offset: 0x000542B4
		public Uri HelpLink
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the globally unique identifier (GUID) for the event provider.</summary>
		/// <returns>Returns the GUID value for the event provider.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06001BAA RID: 7082 RVA: 0x00056AD4 File Offset: 0x00054CD4
		public Guid Id
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(Guid);
			}
		}

		/// <summary>Gets an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventKeyword" /> objects, each of which represent an event keyword that is defined in the event provider.</summary>
		/// <returns>Returns an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventKeyword" /> objects.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001BAB RID: 7083 RVA: 0x0005672F File Offset: 0x0005492F
		public IList<EventKeyword> Keywords
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventLevel" /> objects, each of which represent a level that is defined in the event provider.</summary>
		/// <returns>Returns an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventLevel" /> objects.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001BAC RID: 7084 RVA: 0x0005672F File Offset: 0x0005492F
		public IList<EventLevel> Levels
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventLogLink" /> objects, each of which represent a link to an event log that is used by the event provider.</summary>
		/// <returns>Returns an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventLogLink" /> objects.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x06001BAD RID: 7085 RVA: 0x0005672F File Offset: 0x0005492F
		public IList<EventLogLink> LogLinks
		{
			[SecurityCritical]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the path of the file that contains the message table resource that has the strings associated with the provider metadata.</summary>
		/// <returns>Returns a string that contains the path of the provider message file.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x06001BAE RID: 7086 RVA: 0x000560B4 File Offset: 0x000542B4
		public string MessageFilePath
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the unique name of the event provider.</summary>
		/// <returns>Returns a string that contains the unique name of the event provider.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x000560B4 File Offset: 0x000542B4
		public string Name
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventOpcode" /> objects, each of which represent an opcode that is defined in the event provider.</summary>
		/// <returns>Returns an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventOpcode" /> objects.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001BB0 RID: 7088 RVA: 0x0005672F File Offset: 0x0005492F
		public IList<EventOpcode> Opcodes
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Gets the path of the file that contains the message table resource that has the strings used for parameter substitutions in event descriptions.</summary>
		/// <returns>Returns a string that contains the path of the file that contains the message table resource that has the strings used for parameter substitutions in event descriptions.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001BB1 RID: 7089 RVA: 0x000560B4 File Offset: 0x000542B4
		public string ParameterFilePath
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets the path to the file that contains the metadata associated with the provider.</summary>
		/// <returns>Returns a string that contains the path to the file that contains the metadata associated with the provider.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001BB2 RID: 7090 RVA: 0x000560B4 File Offset: 0x000542B4
		public string ResourceFilePath
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
		}

		/// <summary>Gets an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventTask" /> objects, each of which represent a task that is defined in the event provider.</summary>
		/// <returns>Returns an enumerable collection of <see cref="T:System.Diagnostics.Eventing.Reader.EventTask" /> objects.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001BB3 RID: 7091 RVA: 0x0005672F File Offset: 0x0005492F
		public IList<EventTask> Tasks
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Releases all the resources used by this object.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001BB4 RID: 7092 RVA: 0x0000220F File Offset: 0x0000040F
		public void Dispose()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Releases the unmanaged resources used by this object, and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06001BB5 RID: 7093 RVA: 0x0000220F File Offset: 0x0000040F
		[SecuritySafeCritical]
		protected virtual void Dispose(bool disposing)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
