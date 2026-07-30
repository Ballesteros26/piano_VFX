using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for events that can be handled completely in an event handler. </summary>
	// Token: 0x02000274 RID: 628
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class HandledEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.HandledEventArgs" /> class with a default <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property value of false.</summary>
		// Token: 0x0600141E RID: 5150 RVA: 0x00052D36 File Offset: 0x00050F36
		public HandledEventArgs()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.HandledEventArgs" /> class with the specified default value for the <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property.</summary>
		/// <param name="defaultHandledValue">The default value for the <see cref="P:System.ComponentModel.HandledEventArgs.Handled" /> property.</param>
		// Token: 0x0600141F RID: 5151 RVA: 0x00052D3F File Offset: 0x00050F3F
		public HandledEventArgs(bool defaultHandledValue)
		{
			this.handled = defaultHandledValue;
		}

		/// <summary>Gets or sets a value that indicates whether the event handler has completely handled the event or whether the system should continue its own processing.</summary>
		/// <returns>true if the event has been completely handled; otherwise, false.</returns>
		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06001420 RID: 5152 RVA: 0x00052D4E File Offset: 0x00050F4E
		// (set) Token: 0x06001421 RID: 5153 RVA: 0x00052D56 File Offset: 0x00050F56
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		// Token: 0x040012EA RID: 4842
		private bool handled;
	}
}
