using System;

namespace System.ComponentModel
{
	/// <summary>Specifies which event is raised on initialization. This class cannot be inherited.</summary>
	// Token: 0x02000291 RID: 657
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class InitializationEventAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InitializationEventAttribute" /> class.</summary>
		/// <param name="eventName">The name of the initialization event.</param>
		// Token: 0x06001493 RID: 5267 RVA: 0x00052DDC File Offset: 0x00050FDC
		public InitializationEventAttribute(string eventName)
		{
			this.eventName = eventName;
		}

		/// <summary>Gets the name of the initialization event.</summary>
		/// <returns>The name of the initialization event.</returns>
		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x00052DEB File Offset: 0x00050FEB
		public string EventName
		{
			get
			{
				return this.eventName;
			}
		}

		// Token: 0x040012EF RID: 4847
		private string eventName;
	}
}
