using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Web.Util
{
	// Token: 0x0200011B RID: 283
	internal class DisposableGCHandleRef<T> : IDisposable where T : class, IDisposable
	{
		// Token: 0x06000E15 RID: 3605 RVA: 0x00026446 File Offset: 0x00024646
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		public DisposableGCHandleRef(T t)
		{
			this._handle = GCHandle.Alloc(t);
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000E16 RID: 3606 RVA: 0x0002645F File Offset: 0x0002465F
		public T Target
		{
			[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
			get
			{
				return (T)((object)this._handle.Target);
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x00026471 File Offset: 0x00024671
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		public void Dispose()
		{
			this.Target.Dispose();
			if (this._handle.IsAllocated)
			{
				this._handle.Free();
			}
		}

		// Token: 0x040011B3 RID: 4531
		private GCHandle _handle;
	}
}
