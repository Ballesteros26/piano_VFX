using System;
using System.Runtime.CompilerServices;
using Unity;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemCommand" /> event.</summary>
	// Token: 0x0200039C RID: 924
	public class FormViewCommandEventArgs : CommandEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewCommandEventArgs" /> class.</summary>
		/// <param name="commandSource">The source of the command.</param>
		/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains event data.</param>
		// Token: 0x06002503 RID: 9475 RVA: 0x00060961 File Offset: 0x0005EB61
		public FormViewCommandEventArgs(object commandSource, CommandEventArgs originalArgs)
			: base(originalArgs)
		{
			this.source = commandSource;
		}

		/// <summary>Gets the control that raised the event.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the control that raised the event.</returns>
		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06002504 RID: 9476 RVA: 0x00060971 File Offset: 0x0005EB71
		public object CommandSource
		{
			get
			{
				return this.source;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the control has handled the event.</summary>
		/// <returns>true if data-bound event code was skipped or has finished; otherwise, false.</returns>
		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06002505 RID: 9477 RVA: 0x0006097C File Offset: 0x0005EB7C
		// (set) Token: 0x06002506 RID: 9478 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public bool Handled
		{
			[CompilerGenerated]
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return default(bool);
			}
			[CompilerGenerated]
			set
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
			}
		}

		// Token: 0x040019DB RID: 6619
		private object source;
	}
}
