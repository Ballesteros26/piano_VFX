using System;

namespace System.Threading
{
	/// <summary>Encapsulates and propagates the host execution context across threads. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020004A2 RID: 1186
	[MonoTODO("Useless until the runtime supports it")]
	public class HostExecutionContext : IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.HostExecutionContext" /> class. </summary>
		// Token: 0x060037AD RID: 14253 RVA: 0x000CACD5 File Offset: 0x000C8ED5
		public HostExecutionContext()
		{
			this._state = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.HostExecutionContext" /> class using the specified state. </summary>
		/// <param name="state">An object representing the host execution context state.</param>
		// Token: 0x060037AE RID: 14254 RVA: 0x000CACE4 File Offset: 0x000C8EE4
		public HostExecutionContext(object state)
		{
			this._state = state;
		}

		/// <summary>Creates a copy of the current host execution context.</summary>
		/// <returns>A <see cref="T:System.Threading.HostExecutionContext" /> object representing the host context for the current thread.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037AF RID: 14255 RVA: 0x000CACF3 File Offset: 0x000C8EF3
		public virtual HostExecutionContext CreateCopy()
		{
			return new HostExecutionContext(this._state);
		}

		/// <summary>Gets or sets the state of the host execution context.</summary>
		/// <returns>An object representing the host execution context state.</returns>
		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x060037B0 RID: 14256 RVA: 0x000CAD00 File Offset: 0x000C8F00
		// (set) Token: 0x060037B1 RID: 14257 RVA: 0x000CAD08 File Offset: 0x000C8F08
		protected internal object State
		{
			get
			{
				return this._state;
			}
			set
			{
				this._state = value;
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.HostExecutionContext" /> class.</summary>
		// Token: 0x060037B2 RID: 14258 RVA: 0x000CAD11 File Offset: 0x000C8F11
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, releases the unmanaged resources used by the <see cref="T:System.Threading.WaitHandle" />, and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x060037B3 RID: 14259 RVA: 0x00002194 File Offset: 0x00000394
		public virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x04001D3D RID: 7485
		private object _state;
	}
}
