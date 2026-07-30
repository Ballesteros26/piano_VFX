using System;

namespace Mono.Audio
{
	// Token: 0x02000009 RID: 9
	internal class AudioDevice
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002798 File Offset: 0x00000998
		private static AudioDevice TryAlsa(string name)
		{
			AudioDevice audioDevice;
			try
			{
				audioDevice = new AlsaDevice(name);
			}
			catch
			{
				audioDevice = null;
			}
			return audioDevice;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000027C4 File Offset: 0x000009C4
		public static AudioDevice CreateDevice(string name)
		{
			AudioDevice audioDevice = AudioDevice.TryAlsa(name);
			if (audioDevice == null)
			{
				audioDevice = new AudioDevice();
			}
			return audioDevice;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000027E2 File Offset: 0x000009E2
		public virtual bool SetFormat(AudioFormat format, int channels, int rate)
		{
			return true;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000027E5 File Offset: 0x000009E5
		public virtual int PlaySample(byte[] buffer, int num_frames)
		{
			return num_frames;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000206B File Offset: 0x0000026B
		public virtual int XRunRecovery(int err)
		{
			return err;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000027E8 File Offset: 0x000009E8
		public virtual void Wait()
		{
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000024 RID: 36 RVA: 0x000027EA File Offset: 0x000009EA
		public uint ChunkSize
		{
			get
			{
				return this.chunk_size;
			}
		}

		// Token: 0x040006C7 RID: 1735
		protected uint chunk_size;
	}
}
