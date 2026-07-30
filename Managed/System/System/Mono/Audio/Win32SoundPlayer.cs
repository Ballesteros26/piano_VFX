using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Mono.Audio
{
	// Token: 0x0200000B RID: 11
	internal class Win32SoundPlayer : IDisposable
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00002B4B File Offset: 0x00000D4B
		public Win32SoundPlayer(Stream s)
		{
			if (s != null)
			{
				this._buffer = new byte[s.Length];
				s.Read(this._buffer, 0, this._buffer.Length);
				return;
			}
			this._buffer = new byte[0];
		}

		// Token: 0x0600004A RID: 74
		[DllImport("winmm.dll", SetLastError = true)]
		private static extern bool PlaySound(byte[] ptrToSound, UIntPtr hmod, Win32SoundPlayer.SoundFlags flags);

		// Token: 0x1700000D RID: 13
		// (set) Token: 0x0600004B RID: 75 RVA: 0x00002B8B File Offset: 0x00000D8B
		public Stream Stream
		{
			set
			{
				this.Stop();
				if (value != null)
				{
					this._buffer = new byte[value.Length];
					value.Read(this._buffer, 0, this._buffer.Length);
					return;
				}
				this._buffer = new byte[0];
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002BCB File Offset: 0x00000DCB
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BDC File Offset: 0x00000DDC
		~Win32SoundPlayer()
		{
			this.Dispose(false);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C0C File Offset: 0x00000E0C
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposed)
			{
				this.Stop();
				this._disposed = true;
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002C23 File Offset: 0x00000E23
		public void Play()
		{
			Win32SoundPlayer.PlaySound(this._buffer, UIntPtr.Zero, (Win32SoundPlayer.SoundFlags)5U);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002C37 File Offset: 0x00000E37
		public void PlayLooping()
		{
			Win32SoundPlayer.PlaySound(this._buffer, UIntPtr.Zero, (Win32SoundPlayer.SoundFlags)13U);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002C4C File Offset: 0x00000E4C
		public void PlaySync()
		{
			Win32SoundPlayer.PlaySound(this._buffer, UIntPtr.Zero, (Win32SoundPlayer.SoundFlags)6U);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002C60 File Offset: 0x00000E60
		public void Stop()
		{
			Win32SoundPlayer.PlaySound(null, UIntPtr.Zero, Win32SoundPlayer.SoundFlags.SND_SYNC);
		}

		// Token: 0x040006CB RID: 1739
		private byte[] _buffer;

		// Token: 0x040006CC RID: 1740
		private bool _disposed;

		// Token: 0x0200000C RID: 12
		private enum SoundFlags : uint
		{
			// Token: 0x040006CE RID: 1742
			SND_SYNC,
			// Token: 0x040006CF RID: 1743
			SND_ASYNC,
			// Token: 0x040006D0 RID: 1744
			SND_NODEFAULT,
			// Token: 0x040006D1 RID: 1745
			SND_MEMORY = 4U,
			// Token: 0x040006D2 RID: 1746
			SND_LOOP = 8U,
			// Token: 0x040006D3 RID: 1747
			SND_FILENAME = 131072U
		}
	}
}
