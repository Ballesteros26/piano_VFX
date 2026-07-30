using System;
using System.Security.Permissions;
using Unity;

namespace System.Diagnostics.Eventing
{
	/// <summary>A listener for <see cref="T:System.Diagnostics.TraceSource" /> that writes events to the ETW subsytem. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000386 RID: 902
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventProviderTraceListener : TraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.EventProviderTraceListener" /> class using the specified provider identifier.</summary>
		/// <param name="providerId">A unique string <see cref="T:System.Guid" /> that identifies the provider.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AC6 RID: 6854 RVA: 0x0000220F File Offset: 0x0000040F
		public EventProviderTraceListener(string providerId)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.EventProviderTraceListener" /> class using the specified provider identifier and name of the listener.</summary>
		/// <param name="providerId">A unique string <see cref="T:System.Guid" /> that identifies the provider.</param>
		/// <param name="name">Name of the listener.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AC7 RID: 6855 RVA: 0x0000220F File Offset: 0x0000040F
		public EventProviderTraceListener(string providerId, string name)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Eventing.EventProviderTraceListener" /> class using the specified provider identifier, name of the listener, and delimiter.</summary>
		/// <param name="providerId">A unique string <see cref="T:System.Guid" /> that identifies the provider.</param>
		/// <param name="name">Name of the listener.</param>
		/// <param name="delimiter">Delimiter used to delimit the event data. (For more details, see the <see cref="P:System.Diagnostics.Eventing.EventProviderTraceListener.Delimiter" /> property.)</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001AC8 RID: 6856 RVA: 0x0000220F File Offset: 0x0000040F
		public EventProviderTraceListener(string providerId, string name, string delimiter)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets and sets the delimiter used to delimit the event data that is written to the ETW subsystem.</summary>
		/// <returns>The delimiter used to delimit the event data. The default delimiter is a comma.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x000560B4 File Offset: 0x000542B4
		// (set) Token: 0x06001ACA RID: 6858 RVA: 0x0000220F File Offset: 0x0000040F
		public string Delimiter
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return null;
			}
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		/// <filterpriority>2</filterpriority>
		// Token: 0x06001ACB RID: 6859 RVA: 0x0000220F File Offset: 0x0000040F
		public sealed override void Write(string message)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <filterpriority>2</filterpriority>
		// Token: 0x06001ACC RID: 6860 RVA: 0x0000220F File Offset: 0x0000040F
		public sealed override void WriteLine(string message)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
