using System;
using System.Security;

namespace System.Threading
{
	/// <summary>Provides the functionality to restore the migration, or flow, of the execution context between threads.  </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000473 RID: 1139
	public struct AsyncFlowControl : IDisposable
	{
		// Token: 0x060035D9 RID: 13785 RVA: 0x000C7114 File Offset: 0x000C5314
		[SecurityCritical]
		internal void Setup()
		{
			this.useEC = true;
			Thread currentThread = Thread.CurrentThread;
			this._ec = currentThread.GetMutableExecutionContext();
			this._ec.isFlowSuppressed = true;
			this._thread = currentThread;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.AsyncFlowControl" /> class.</summary>
		// Token: 0x060035DA RID: 13786 RVA: 0x000C714D File Offset: 0x000C534D
		public void Dispose()
		{
			this.Undo();
		}

		/// <summary>Restores the flow of the execution context between threads.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.AsyncFlowControl" /> structure is not used on the thread where it was created.-or-The <see cref="T:System.Threading.AsyncFlowControl" /> structure has already been used to call <see cref="M:System.Threading.AsyncFlowControl.Undo" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060035DB RID: 13787 RVA: 0x000C7158 File Offset: 0x000C5358
		[SecuritySafeCritical]
		public void Undo()
		{
			if (this._thread == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("AsyncFlowControl object can be used only once to call Undo()."));
			}
			if (this._thread != Thread.CurrentThread)
			{
				throw new InvalidOperationException(Environment.GetResourceString("AsyncFlowControl object must be used on the thread where it was created."));
			}
			if (this.useEC)
			{
				if (Thread.CurrentThread.GetMutableExecutionContext() != this._ec)
				{
					throw new InvalidOperationException(Environment.GetResourceString("AsyncFlowControl objects can be used to restore flow only on the Context that had its flow suppressed."));
				}
				ExecutionContext.RestoreFlow();
			}
			this._thread = null;
		}

		/// <summary>Gets a hash code for the current <see cref="T:System.Threading.AsyncFlowControl" /> structure.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Threading.AsyncFlowControl" /> structure.</returns>
		// Token: 0x060035DC RID: 13788 RVA: 0x000C71D0 File Offset: 0x000C53D0
		public override int GetHashCode()
		{
			if (this._thread != null)
			{
				return this._thread.GetHashCode();
			}
			return this.ToString().GetHashCode();
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure. </summary>
		/// <returns>true if <paramref name="obj" /> is an <see cref="T:System.Threading.AsyncFlowControl" /> structure and is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure; otherwise, false. </returns>
		/// <param name="obj">An object to compare with the current structure.</param>
		// Token: 0x060035DD RID: 13789 RVA: 0x000C71F7 File Offset: 0x000C53F7
		public override bool Equals(object obj)
		{
			return obj is AsyncFlowControl && this.Equals((AsyncFlowControl)obj);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Threading.AsyncFlowControl" /> structure is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Threading.AsyncFlowControl" /> structure to compare with the current structure.</param>
		// Token: 0x060035DE RID: 13790 RVA: 0x000C720F File Offset: 0x000C540F
		public bool Equals(AsyncFlowControl obj)
		{
			return obj.useEC == this.useEC && obj._ec == this._ec && obj._thread == this._thread;
		}

		/// <summary>Compares two <see cref="T:System.Threading.AsyncFlowControl" /> structures to determine whether they are equal. </summary>
		/// <returns>true if the two structures are equal; otherwise, false. </returns>
		/// <param name="a">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		/// <param name="b">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		// Token: 0x060035DF RID: 13791 RVA: 0x000C723D File Offset: 0x000C543D
		public static bool operator ==(AsyncFlowControl a, AsyncFlowControl b)
		{
			return a.Equals(b);
		}

		/// <summary>Compares two <see cref="T:System.Threading.AsyncFlowControl" /> structures to determine whether they are not equal. </summary>
		/// <returns>true if the structures are not equal; otherwise, false. </returns>
		/// <param name="a">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		/// <param name="b">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		// Token: 0x060035E0 RID: 13792 RVA: 0x000C7247 File Offset: 0x000C5447
		public static bool operator !=(AsyncFlowControl a, AsyncFlowControl b)
		{
			return !(a == b);
		}

		// Token: 0x04001CB0 RID: 7344
		private bool useEC;

		// Token: 0x04001CB1 RID: 7345
		private ExecutionContext _ec;

		// Token: 0x04001CB2 RID: 7346
		private Thread _thread;
	}
}
