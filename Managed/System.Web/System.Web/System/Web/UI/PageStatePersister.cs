using System;

namespace System.Web.UI
{
	/// <summary>Provides the base functionality for ASP.NET view state persistence mechanisms.</summary>
	// Token: 0x02000213 RID: 531
	public abstract class PageStatePersister
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageStatePersister" /> class.</summary>
		/// <param name="page">The <see cref="T:System.Web.UI.Page" /> that the view state persistence mechanism is created for.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="page" /> parameter is null.</exception>
		// Token: 0x060015DD RID: 5597 RVA: 0x0003B417 File Offset: 0x00039617
		protected PageStatePersister(Page page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page");
			}
			this.page = page;
		}

		/// <summary>Gets or sets an object that represents the data that controls contained by the current <see cref="T:System.Web.UI.Page" /> object use to persist across HTTP requests to the Web server. </summary>
		/// <returns>An object that contains view state data.</returns>
		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x0003B434 File Offset: 0x00039634
		// (set) Token: 0x060015DF RID: 5599 RVA: 0x0003B43C File Offset: 0x0003963C
		public object ControlState
		{
			get
			{
				return this.control_state;
			}
			set
			{
				this.control_state = value;
			}
		}

		/// <summary>Gets or sets an object that represents the data that controls contained by the current <see cref="T:System.Web.UI.Page" /> object use to persist across HTTP requests to the Web server. </summary>
		/// <returns>An object that contains view state data.</returns>
		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060015E0 RID: 5600 RVA: 0x0003B445 File Offset: 0x00039645
		// (set) Token: 0x060015E1 RID: 5601 RVA: 0x0003B44D File Offset: 0x0003964D
		public object ViewState
		{
			get
			{
				return this.view_state;
			}
			set
			{
				this.view_state = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Web.UI.Page" /> object that the view state persistence mechanism is created for.</summary>
		/// <returns>The <see cref="T:System.Web.UI.Page" /> that the <see cref="T:System.Web.UI.PageStatePersister" /> is associated with.</returns>
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060015E2 RID: 5602 RVA: 0x0003B456 File Offset: 0x00039656
		// (set) Token: 0x060015E3 RID: 5603 RVA: 0x0003B45E File Offset: 0x0003965E
		protected Page Page
		{
			get
			{
				return this.page;
			}
			set
			{
				this.page = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Web.UI.IStateFormatter" /> object that is used to serialize and deserialize the state information contained in the <see cref="P:System.Web.UI.PageStatePersister.ViewState" /> and <see cref="P:System.Web.UI.PageStatePersister.ControlState" /> properties during calls to the <see cref="M:System.Web.UI.PageStatePersister.Save" /> and <see cref="M:System.Web.UI.PageStatePersister.Load" /> methods.</summary>
		/// <returns>An instance of <see cref="T:System.Web.UI.IStateFormatter" /> that is used to serialize and deserialize object state.</returns>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x060015E4 RID: 5604 RVA: 0x0003B467 File Offset: 0x00039667
		protected IStateFormatter StateFormatter
		{
			get
			{
				if (this.state_formatter == null)
				{
					this.state_formatter = this.page.GetFormatter();
				}
				return this.state_formatter;
			}
		}

		/// <summary>Overridden by derived classes to deserialize and load persisted state information when a <see cref="T:System.Web.UI.Page" /> object initializes its control hierarchy.</summary>
		// Token: 0x060015E5 RID: 5605
		public abstract void Load();

		/// <summary>Overridden by derived classes to serialize persisted state information when a <see cref="T:System.Web.UI.Page" /> object is unloaded from memory.</summary>
		// Token: 0x060015E6 RID: 5606
		public abstract void Save();

		// Token: 0x0400153A RID: 5434
		private object control_state;

		// Token: 0x0400153B RID: 5435
		private object view_state;

		// Token: 0x0400153C RID: 5436
		private Page page;

		// Token: 0x0400153D RID: 5437
		private IStateFormatter state_formatter;
	}
}
