using System;
using System.Threading;

namespace System
{
	/// <summary>Provides an <see cref="T:System.IProgress`1" /> that invokes callbacks for each reported progress value.</summary>
	/// <typeparam name="T">Specifies the type of the progress report value.</typeparam>
	// Token: 0x020001B1 RID: 433
	public class Progress<T> : IProgress<T>
	{
		/// <summary>Initializes the <see cref="T:System.Progress`1" /> object.</summary>
		// Token: 0x0600120C RID: 4620 RVA: 0x00049A6C File Offset: 0x00047C6C
		public Progress()
		{
			this.m_synchronizationContext = SynchronizationContext.CurrentNoFlow ?? ProgressStatics.DefaultContext;
			this.m_invokeHandlers = new SendOrPostCallback(this.InvokeHandlers);
		}

		/// <summary>Initializes the <see cref="T:System.Progress`1" /> object with the specified callback.</summary>
		/// <param name="handler">A handler to invoke for each reported progress value. This handler will be invoked in addition to any delegates registered with the <see cref="E:System.Progress`1.ProgressChanged" /> event. Depending on the <see cref="T:System.Threading.SynchronizationContext" /> instance captured by the <see cref="T:System.Progress`1" /> at construction, it is possible that this handler instance could be invoked concurrently with itself.</param>
		// Token: 0x0600120D RID: 4621 RVA: 0x00049A9A File Offset: 0x00047C9A
		public Progress(Action<T> handler)
			: this()
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this.m_handler = handler;
		}

		/// <summary>Raised for each reported progress value.</summary>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600120E RID: 4622 RVA: 0x00049AB8 File Offset: 0x00047CB8
		// (remove) Token: 0x0600120F RID: 4623 RVA: 0x00049AF0 File Offset: 0x00047CF0
		public event EventHandler<T> ProgressChanged;

		/// <summary>Reports a progress change.</summary>
		/// <param name="value">The value of the updated progress.</param>
		// Token: 0x06001210 RID: 4624 RVA: 0x00049B28 File Offset: 0x00047D28
		protected virtual void OnReport(T value)
		{
			bool handler = this.m_handler != null;
			EventHandler<T> progressChanged = this.ProgressChanged;
			if (handler || progressChanged != null)
			{
				this.m_synchronizationContext.Post(this.m_invokeHandlers, value);
			}
		}

		/// <summary>Reports a progress change.</summary>
		/// <param name="value">The value of the updated progress.</param>
		// Token: 0x06001211 RID: 4625 RVA: 0x00049B5E File Offset: 0x00047D5E
		void IProgress<T>.Report(T value)
		{
			this.OnReport(value);
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x00049B68 File Offset: 0x00047D68
		private void InvokeHandlers(object state)
		{
			T t = (T)((object)state);
			Action<T> handler = this.m_handler;
			EventHandler<T> progressChanged = this.ProgressChanged;
			if (handler != null)
			{
				handler(t);
			}
			if (progressChanged != null)
			{
				progressChanged(this, t);
			}
		}

		// Token: 0x04000A54 RID: 2644
		private readonly SynchronizationContext m_synchronizationContext;

		// Token: 0x04000A55 RID: 2645
		private readonly Action<T> m_handler;

		// Token: 0x04000A56 RID: 2646
		private readonly SendOrPostCallback m_invokeHandlers;
	}
}
