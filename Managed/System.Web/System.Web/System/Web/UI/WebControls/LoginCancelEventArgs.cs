using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for a cancelable event.</summary>
	// Token: 0x020002E0 RID: 736
	public class LoginCancelEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> class with the <see cref="P:System.Web.UI.WebControls.LoginCancelEventArgs.Cancel" /> property set to false.</summary>
		// Token: 0x06001B92 RID: 7058 RVA: 0x000460C2 File Offset: 0x000442C2
		public LoginCancelEventArgs()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> class with the <see cref="P:System.Web.UI.WebControls.LoginCancelEventArgs.Cancel" /> property set to the specified value.</summary>
		/// <param name="cancel">true to cancel the event; otherwise, false.</param>
		// Token: 0x06001B93 RID: 7059 RVA: 0x000460CB File Offset: 0x000442CB
		public LoginCancelEventArgs(bool cancel)
		{
			this._cancel = cancel;
		}

		/// <summary>Gets or sets a value indicating whether the event should be canceled.</summary>
		/// <returns>true if the event should be canceled; otherwise, false.</returns>
		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x000460DA File Offset: 0x000442DA
		// (set) Token: 0x06001B95 RID: 7061 RVA: 0x000460E2 File Offset: 0x000442E2
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = value;
			}
		}

		// Token: 0x04001713 RID: 5907
		private bool _cancel;
	}
}
