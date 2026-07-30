using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Specifies the contextual information about an application thread.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000041 RID: 65
	public class ApplicationContext : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ApplicationContext" /> class with no context.</summary>
		// Token: 0x0600023B RID: 571 RVA: 0x00010E1C File Offset: 0x0000F01C
		public ApplicationContext()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ApplicationContext" /> class with the specified <see cref="T:System.Windows.Forms.Form" />.</summary>
		/// <param name="mainForm">The main <see cref="T:System.Windows.Forms.Form" /> of the application to use for context. </param>
		// Token: 0x0600023C RID: 572 RVA: 0x00010E28 File Offset: 0x0000F028
		public ApplicationContext(Form mainForm)
		{
			this.MainForm = mainForm;
		}

		/// <summary>Occurs when the message loop of the thread should be terminated, by calling <see cref="M:System.Windows.Forms.ApplicationContext.ExitThread" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600023D RID: 573 RVA: 0x00010E38 File Offset: 0x0000F038
		// (remove) Token: 0x0600023E RID: 574 RVA: 0x00010E54 File Offset: 0x0000F054
		public event EventHandler ThreadExit;

		/// <summary>Attempts to free resources and perform other cleanup operations before the application context is reclaimed by garbage collection.</summary>
		// Token: 0x0600023F RID: 575 RVA: 0x00010E70 File Offset: 0x0000F070
		~ApplicationContext()
		{
			this.Dispose(false);
		}

		/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.Form" /> to use as context.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Form" /> to use as context.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00010EAC File Offset: 0x0000F0AC
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00010EB4 File Offset: 0x0000F0B4
		public Form MainForm
		{
			get
			{
				return this.main_form;
			}
			set
			{
				if (this.main_form != value)
				{
					if (this.main_form != null)
					{
						this.main_form.HandleDestroyed -= new EventHandler(this.OnMainFormClosed);
					}
					this.main_form = value;
					if (this.main_form != null)
					{
						this.main_form.HandleDestroyed += new EventHandler(this.OnMainFormClosed);
					}
				}
			}
		}

		/// <summary>Gets or sets an object that contains data about the control.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the control. The default is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00010F1C File Offset: 0x0000F11C
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00010F24 File Offset: 0x0000F124
		[Bindable(true)]
		[Localizable(false)]
		[TypeConverter(typeof(StringConverter))]
		[DefaultValue(null)]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.ApplicationContext" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000244 RID: 580 RVA: 0x00010F30 File Offset: 0x0000F130
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Terminates the message loop of the thread.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000245 RID: 581 RVA: 0x00010F40 File Offset: 0x0000F140
		public void ExitThread()
		{
			this.ExitThreadCore();
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.ApplicationContext" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06000246 RID: 582 RVA: 0x00010F48 File Offset: 0x0000F148
		protected virtual void Dispose(bool disposing)
		{
			this.MainForm = null;
			this.tag = null;
		}

		/// <summary>Terminates the message loop of the thread.</summary>
		// Token: 0x06000247 RID: 583 RVA: 0x00010F58 File Offset: 0x0000F158
		protected virtual void ExitThreadCore()
		{
			if (Application.MWFThread.Current.Context == this)
			{
				XplatUI.PostQuitMessage(0);
			}
			if (!this.thread_exit_raised && this.ThreadExit != null)
			{
				this.thread_exit_raised = true;
				this.ThreadExit.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>Calls <see cref="M:System.Windows.Forms.ApplicationContext.ExitThreadCore" />, which raises the <see cref="E:System.Windows.Forms.ApplicationContext.ThreadExit" /> event.</summary>
		/// <param name="sender">The object that raised the event. </param>
		/// <param name="e">The <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06000248 RID: 584 RVA: 0x00010FAC File Offset: 0x0000F1AC
		protected virtual void OnMainFormClosed(object sender, EventArgs e)
		{
			if (!this.MainForm.RecreatingHandle)
			{
				this.ExitThreadCore();
			}
		}

		// Token: 0x040005C2 RID: 1474
		private Form main_form;

		// Token: 0x040005C3 RID: 1475
		private object tag;

		// Token: 0x040005C4 RID: 1476
		private bool thread_exit_raised;
	}
}
