using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Unity;

namespace System.Security.Principal
{
	/// <summary>Represents the Windows user prior to an impersonation operation. </summary>
	// Token: 0x0200062F RID: 1583
	[ComVisible(true)]
	public class WindowsImpersonationContext : IDisposable
	{
		// Token: 0x060044C0 RID: 17600 RVA: 0x000F1BDF File Offset: 0x000EFDDF
		internal WindowsImpersonationContext(IntPtr token)
		{
			this._token = WindowsImpersonationContext.DuplicateToken(token);
			if (!WindowsImpersonationContext.SetCurrentToken(token))
			{
				throw new SecurityException("Couldn't impersonate token.");
			}
			this.undo = false;
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Security.Principal.WindowsImpersonationContext" />.</summary>
		// Token: 0x060044C1 RID: 17601 RVA: 0x000F1C0D File Offset: 0x000EFE0D
		[ComVisible(false)]
		public void Dispose()
		{
			if (!this.undo)
			{
				this.Undo();
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Security.Principal.WindowsImpersonationContext" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060044C2 RID: 17602 RVA: 0x000F1C1D File Offset: 0x000EFE1D
		[ComVisible(false)]
		protected virtual void Dispose(bool disposing)
		{
			if (!this.undo)
			{
				this.Undo();
			}
			if (disposing)
			{
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Reverts the user context to the Windows user represented by this object.</summary>
		/// <exception cref="T:System.Security.SecurityException">An attempt is made to use this method for any purpose other than to revert identity to self. </exception>
		// Token: 0x060044C3 RID: 17603 RVA: 0x000F1C36 File Offset: 0x000EFE36
		public void Undo()
		{
			if (!WindowsImpersonationContext.RevertToSelf())
			{
				WindowsImpersonationContext.CloseToken(this._token);
				throw new SecurityException("Couldn't switch back to original token.");
			}
			WindowsImpersonationContext.CloseToken(this._token);
			this.undo = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x060044C4 RID: 17604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CloseToken(IntPtr token);

		// Token: 0x060044C5 RID: 17605
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr DuplicateToken(IntPtr token);

		// Token: 0x060044C6 RID: 17606
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetCurrentToken(IntPtr token);

		// Token: 0x060044C7 RID: 17607
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool RevertToSelf();

		// Token: 0x060044C8 RID: 17608 RVA: 0x0001FB35 File Offset: 0x0001DD35
		internal WindowsImpersonationContext()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04002306 RID: 8966
		private IntPtr _token;

		// Token: 0x04002307 RID: 8967
		private bool undo;
	}
}
