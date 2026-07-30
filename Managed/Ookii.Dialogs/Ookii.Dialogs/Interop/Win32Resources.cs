using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Text;

namespace Ookii.Dialogs.Interop
{
	// Token: 0x02000046 RID: 70
	internal class Win32Resources : IDisposable
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x00009FD0 File Offset: 0x000081D0
		public Win32Resources(string module)
		{
			this._moduleHandle = NativeMethods.LoadLibraryEx(module, IntPtr.Zero, NativeMethods.LoadLibraryExFlags.LoadLibraryAsDatafile);
			bool isInvalid = this._moduleHandle.IsInvalid;
			if (isInvalid)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000A014 File Offset: 0x00008214
		public string LoadString(uint id)
		{
			this.CheckDisposed();
			StringBuilder stringBuilder = new StringBuilder(500);
			bool flag = NativeMethods.LoadString(this._moduleHandle, id, stringBuilder, stringBuilder.Capacity + 1) == 0;
			if (flag)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000A068 File Offset: 0x00008268
		public string FormatString(uint id, params string[] args)
		{
			this.CheckDisposed();
			IntPtr zero = IntPtr.Zero;
			string text = this.LoadString(id);
			NativeMethods.FormatMessageFlags formatMessageFlags = NativeMethods.FormatMessageFlags.FORMAT_MESSAGE_ALLOCATE_BUFFER | NativeMethods.FormatMessageFlags.FORMAT_MESSAGE_FROM_STRING | NativeMethods.FormatMessageFlags.FORMAT_MESSAGE_ARGUMENT_ARRAY;
			IntPtr intPtr = Marshal.StringToHGlobalAuto(text);
			try
			{
				bool flag = NativeMethods.FormatMessage(formatMessageFlags, intPtr, id, 0U, ref zero, 0U, args) == 0U;
				if (flag)
				{
					throw new Win32Exception(Marshal.GetLastWin32Error());
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			string text2 = Marshal.PtrToStringAuto(zero);
			Marshal.FreeHGlobal(zero);
			return text2;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000A0F0 File Offset: 0x000082F0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._moduleHandle.Dispose();
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000A110 File Offset: 0x00008310
		private void CheckDisposed()
		{
			bool isClosed = this._moduleHandle.IsClosed;
			if (isClosed)
			{
				throw new ObjectDisposedException("Win32Resources");
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000A139 File Offset: 0x00008339
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x040000EA RID: 234
		private SafeModuleHandle _moduleHandle;

		// Token: 0x040000EB RID: 235
		private const int _bufferSize = 500;
	}
}
