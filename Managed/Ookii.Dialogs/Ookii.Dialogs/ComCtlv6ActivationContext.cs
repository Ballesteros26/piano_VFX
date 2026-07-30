using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Ookii.Dialogs
{
	// Token: 0x02000005 RID: 5
	internal sealed class ComCtlv6ActivationContext : IDisposable
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000215C File Offset: 0x0000035C
		public ComCtlv6ActivationContext(bool enable)
		{
			bool flag = enable && NativeMethods.IsWindowsXPOrLater;
			if (flag)
			{
				bool flag2 = ComCtlv6ActivationContext.EnsureActivateContextCreated();
				if (flag2)
				{
					bool flag3 = !NativeMethods.ActivateActCtx(ComCtlv6ActivationContext._activationContext, out this._cookie);
					if (flag3)
					{
						this._cookie = IntPtr.Zero;
					}
				}
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B4 File Offset: 0x000003B4
		~ComCtlv6ActivationContext()
		{
			this.Dispose(false);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021E8 File Offset: 0x000003E8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021FC File Offset: 0x000003FC
		private void Dispose(bool disposing)
		{
			bool flag = this._cookie != IntPtr.Zero;
			if (flag)
			{
				bool flag2 = NativeMethods.DeactivateActCtx(0U, this._cookie);
				if (flag2)
				{
					this._cookie = IntPtr.Zero;
				}
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002240 File Offset: 0x00000440
		private static bool EnsureActivateContextCreated()
		{
			object contextCreationLock = ComCtlv6ActivationContext._contextCreationLock;
			bool contextCreationSucceeded;
			lock (contextCreationLock)
			{
				bool flag = !ComCtlv6ActivationContext._contextCreationSucceeded;
				if (flag)
				{
					string location = typeof(object).Assembly.Location;
					string text = null;
					string text2 = null;
					bool flag2 = location != null;
					if (flag2)
					{
						text2 = Path.GetDirectoryName(location);
						text = Path.Combine(text2, "XPThemes.manifest");
					}
					bool flag3 = text != null && text2 != null;
					if (flag3)
					{
						ComCtlv6ActivationContext._enableThemingActivationContext = default(NativeMethods.ACTCTX);
						ComCtlv6ActivationContext._enableThemingActivationContext.cbSize = Marshal.SizeOf(typeof(NativeMethods.ACTCTX));
						ComCtlv6ActivationContext._enableThemingActivationContext.lpSource = text;
						ComCtlv6ActivationContext._enableThemingActivationContext.lpAssemblyDirectory = text2;
						ComCtlv6ActivationContext._enableThemingActivationContext.dwFlags = 4U;
						ComCtlv6ActivationContext._activationContext = NativeMethods.CreateActCtx(ref ComCtlv6ActivationContext._enableThemingActivationContext);
						ComCtlv6ActivationContext._contextCreationSucceeded = !ComCtlv6ActivationContext._activationContext.IsInvalid;
					}
				}
				contextCreationSucceeded = ComCtlv6ActivationContext._contextCreationSucceeded;
			}
			return contextCreationSucceeded;
		}

		// Token: 0x0400000B RID: 11
		private IntPtr _cookie;

		// Token: 0x0400000C RID: 12
		private static NativeMethods.ACTCTX _enableThemingActivationContext;

		// Token: 0x0400000D RID: 13
		private static ActivationContextSafeHandle _activationContext;

		// Token: 0x0400000E RID: 14
		private static bool _contextCreationSucceeded;

		// Token: 0x0400000F RID: 15
		private static readonly object _contextCreationLock = new object();
	}
}
