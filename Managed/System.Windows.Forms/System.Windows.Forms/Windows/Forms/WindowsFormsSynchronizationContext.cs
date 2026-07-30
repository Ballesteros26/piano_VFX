using System;
using System.ComponentModel;
using System.Threading;

namespace System.Windows.Forms
{
	/// <summary>Provides a synchronization context for the Windows Forms application model. </summary>
	// Token: 0x020003C4 RID: 964
	public sealed class WindowsFormsSynchronizationContext : SynchronizationContext, IDisposable
	{
		// Token: 0x06004574 RID: 17780 RVA: 0x0010EF40 File Offset: 0x0010D140
		static WindowsFormsSynchronizationContext()
		{
			WindowsFormsSynchronizationContext.invoke_control.CreateControl();
			WindowsFormsSynchronizationContext.auto_installed = true;
			WindowsFormsSynchronizationContext.previous_context = SynchronizationContext.Current;
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.WindowsFormsSynchronizationContext" /> is installed when a control is created.</summary>
		/// <returns>true if the <see cref="T:System.Windows.Forms.WindowsFormsSynchronizationContext" /> is installed; otherwise, false. The default is true.</returns>
		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x06004575 RID: 17781 RVA: 0x0010EF74 File Offset: 0x0010D174
		// (set) Token: 0x06004576 RID: 17782 RVA: 0x0010EF7C File Offset: 0x0010D17C
		[EditorBrowsable(2)]
		public static bool AutoInstall
		{
			get
			{
				return WindowsFormsSynchronizationContext.auto_installed;
			}
			set
			{
				WindowsFormsSynchronizationContext.auto_installed = value;
			}
		}

		/// <summary>Copies the synchronization context.</summary>
		/// <returns>A copy of the synchronization context.</returns>
		// Token: 0x06004577 RID: 17783 RVA: 0x0010EF84 File Offset: 0x0010D184
		public override SynchronizationContext CreateCopy()
		{
			return base.CreateCopy();
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Windows.Forms.WindowsFormsSynchronizationContext" />. </summary>
		// Token: 0x06004578 RID: 17784 RVA: 0x0010EF8C File Offset: 0x0010D18C
		public void Dispose()
		{
		}

		/// <summary>Dispatches an asynchronous message to a synchronization context.</summary>
		/// <param name="d">The <see cref="T:System.Threading.SendOrPostCallback" /> delegate to call.</param>
		/// <param name="state">The object passed to the delegate.</param>
		// Token: 0x06004579 RID: 17785 RVA: 0x0010EF90 File Offset: 0x0010D190
		public override void Post(SendOrPostCallback d, object state)
		{
			WindowsFormsSynchronizationContext.invoke_control.BeginInvoke(d, new object[] { state });
		}

		/// <summary>Dispatches a synchronous message to a synchronization context</summary>
		/// <param name="d">The <see cref="T:System.Threading.SendOrPostCallback" /> delegate to call.</param>
		/// <param name="state">The object passed to the delegate.</param>
		/// <exception cref="T:System.ComponentModel.InvalidAsynchronousStateException">The destination thread no longer exists.</exception>
		// Token: 0x0600457A RID: 17786 RVA: 0x0010EFA8 File Offset: 0x0010D1A8
		public override void Send(SendOrPostCallback d, object state)
		{
			WindowsFormsSynchronizationContext.invoke_control.Invoke(d, new object[] { state });
		}

		/// <summary>Uninstalls the currently installed <see cref="T:System.Windows.Forms.WindowsFormsSynchronizationContext" /> and replaces it with the previously installed context.</summary>
		// Token: 0x0600457B RID: 17787 RVA: 0x0010EFC0 File Offset: 0x0010D1C0
		public static void Uninstall()
		{
			if (WindowsFormsSynchronizationContext.previous_context == null)
			{
				WindowsFormsSynchronizationContext.previous_context = new SynchronizationContext();
			}
			SynchronizationContext.SetSynchronizationContext(WindowsFormsSynchronizationContext.previous_context);
		}

		// Token: 0x04001D4A RID: 7498
		private static bool auto_installed;

		// Token: 0x04001D4B RID: 7499
		private static Control invoke_control = new Control();

		// Token: 0x04001D4C RID: 7500
		private static SynchronizationContext previous_context;
	}
}
