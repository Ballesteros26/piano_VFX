using System;
using Unity;

namespace System.Web.Management
{
	/// <summary>Defines the base class for the ASP.NET health-monitoring events.</summary>
	// Token: 0x02000530 RID: 1328
	public class WebBaseEvent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebBaseEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The description of the event. </param>
		/// <param name="eventSource">The object that raised the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />. </param>
		// Token: 0x06003A3D RID: 14909 RVA: 0x0009D694 File Offset: 0x0009B894
		protected WebBaseEvent(string message, object eventSource, int eventCode)
		{
			this.message = message;
			this.event_source = eventSource;
			this.event_code = eventCode;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Management.WebBaseEvent" /> class using the supplied parameters.</summary>
		/// <param name="message">The description of the raised event. </param>
		/// <param name="eventSource">The object that raised the event. </param>
		/// <param name="eventCode">The code associated with the event. When you implement a custom event, the event code must be greater than <see cref="F:System.Web.Management.WebEventCodes.WebExtendedBase" />. </param>
		/// <param name="eventDetailCode">The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</param>
		// Token: 0x06003A3E RID: 14910 RVA: 0x0009D6B1 File Offset: 0x0009B8B1
		protected WebBaseEvent(string message, object eventSource, int eventCode, int eventDetailCode)
		{
			this.message = message;
			this.event_source = eventSource;
			this.event_code = eventCode;
			this.event_detail_code = eventDetailCode;
		}

		/// <summary>Gets a <see cref="T:System.Web.Management.WebApplicationInformation" /> object that contains information about the current application being monitored.</summary>
		/// <returns>A <see cref="T:System.Web.Management.WebApplicationInformation" /> object that contains information about the application being monitored.</returns>
		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x06003A3F RID: 14911 RVA: 0x00003A1F File Offset: 0x00001C1F
		public static WebApplicationInformation ApplicationInformation
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the code value associated with the event.</summary>
		/// <returns>One of the <see cref="T:System.Web.Management.WebEventCodes" /> values.</returns>
		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06003A40 RID: 14912 RVA: 0x0009D6D6 File Offset: 0x0009B8D6
		public int EventCode
		{
			get
			{
				return this.event_code;
			}
		}

		/// <summary>Gets the event detail code.</summary>
		/// <returns>The <see cref="T:System.Web.Management.WebEventCodes" /> value that specifies the detailed identifier for the event.</returns>
		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06003A41 RID: 14913 RVA: 0x0009D6DE File Offset: 0x0009B8DE
		public int EventDetailCode
		{
			get
			{
				return this.event_detail_code;
			}
		}

		/// <summary>Gets the identifier associated with the event.</summary>
		/// <returns>The identifier associated with the event.</returns>
		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06003A42 RID: 14914 RVA: 0x00003A1F File Offset: 0x00001C1F
		public Guid EventID
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the number of times the event has been raised by the application.</summary>
		/// <returns>The number of times the event has been raised.</returns>
		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06003A43 RID: 14915 RVA: 0x00003A1F File Offset: 0x00001C1F
		public long EventSequence
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the object that raises the event.</summary>
		/// <returns>The object that raised the event.</returns>
		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x06003A44 RID: 14916 RVA: 0x0009D6E6 File Offset: 0x0009B8E6
		public object EventSource
		{
			get
			{
				return this.event_source;
			}
		}

		/// <summary>Gets the time when the event was raised.</summary>
		/// <returns>The time that the event was raised.</returns>
		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x06003A45 RID: 14917 RVA: 0x00003A1F File Offset: 0x00001C1F
		public DateTime EventTime
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the time when the event was raised.</summary>
		/// <returns>The time of the event in Coordinated Universal Time (UTC) format.</returns>
		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x06003A46 RID: 14918 RVA: 0x00003A1F File Offset: 0x00001C1F
		public DateTime EventTimeUtc
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the message that describes the event.</summary>
		/// <returns>The description of the event.</returns>
		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06003A47 RID: 14919 RVA: 0x0009D6EE File Offset: 0x0009B8EE
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		/// <summary>Provides standard formatting of the event information.</summary>
		/// <param name="formatter">A <see cref="T:System.Web.Management.WebEventFormatter" /> object that contains the formatted event information.</param>
		// Token: 0x06003A48 RID: 14920 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void FormatCustomEventDetails(WebEventFormatter formatter)
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises an event by notifying any configured provider that the event has occurred.</summary>
		// Token: 0x06003A49 RID: 14921 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual void Raise()
		{
			throw new NotImplementedException();
		}

		/// <summary>Raises the specified event by notifying any configured provider that the event has occurred.</summary>
		/// <param name="eventRaised">A <see cref="T:System.Web.Management.WebBaseEvent" /> object. </param>
		/// <exception cref="T:System.Web.HttpException">The <see cref="P:System.Web.Management.WebBaseEvent.EventCode" /> property of <paramref name="eventRaised" /> has a value that is reserved for ASP.NET.</exception>
		// Token: 0x06003A4A RID: 14922 RVA: 0x00003A1F File Offset: 0x00001C1F
		public static void Raise(WebBaseEvent eventRaised)
		{
			throw new NotImplementedException();
		}

		/// <summary>Formats event information for display purposes.</summary>
		/// <returns>The event information.</returns>
		// Token: 0x06003A4B RID: 14923 RVA: 0x00003A1F File Offset: 0x00001C1F
		public override string ToString()
		{
			throw new NotImplementedException();
		}

		/// <summary>Formats event information for display purposes.</summary>
		/// <returns>The event information.</returns>
		/// <param name="includeAppInfo">true if standard application information must be displayed as part of the event information; otherwise, false. </param>
		/// <param name="includeCustomEventDetails">true if custom information must be displayed as part of the event information; otherwise, false.</param>
		// Token: 0x06003A4C RID: 14924 RVA: 0x00003A1F File Offset: 0x00001C1F
		public virtual string ToString(bool includeAppInfo, bool includeCustomEventDetails)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a counter that represents the number of times the event has occurred.</summary>
		/// <returns>A counter that represents the number of times the event has occurred.</returns>
		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06003A4D RID: 14925 RVA: 0x0009D6F8 File Offset: 0x0009B8F8
		public long EventOccurrence
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0L;
			}
		}

		/// <summary>Used internally to increment performance counters.</summary>
		// Token: 0x06003A4E RID: 14926 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected internal virtual void IncrementPerfCounters()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001F77 RID: 8055
		private string message;

		// Token: 0x04001F78 RID: 8056
		private object event_source;

		// Token: 0x04001F79 RID: 8057
		private int event_code;

		// Token: 0x04001F7A RID: 8058
		private int event_detail_code;
	}
}
