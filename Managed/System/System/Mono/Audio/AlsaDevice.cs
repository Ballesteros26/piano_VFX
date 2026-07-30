using System;
using System.Runtime.InteropServices;

namespace Mono.Audio
{
	// Token: 0x0200000A RID: 10
	internal class AlsaDevice : AudioDevice, IDisposable
	{
		// Token: 0x06000026 RID: 38
		[DllImport("libasound")]
		private static extern int snd_pcm_open(ref IntPtr handle, string pcm_name, int stream, int mode);

		// Token: 0x06000027 RID: 39
		[DllImport("libasound")]
		private static extern int snd_pcm_close(IntPtr handle);

		// Token: 0x06000028 RID: 40
		[DllImport("libasound")]
		private static extern int snd_pcm_drain(IntPtr handle);

		// Token: 0x06000029 RID: 41
		[DllImport("libasound")]
		private static extern int snd_pcm_writei(IntPtr handle, byte[] buf, int size);

		// Token: 0x0600002A RID: 42
		[DllImport("libasound")]
		private static extern int snd_pcm_set_params(IntPtr handle, int format, int access, int channels, int rate, int soft_resample, int latency);

		// Token: 0x0600002B RID: 43
		[DllImport("libasound")]
		private static extern int snd_pcm_state(IntPtr handle);

		// Token: 0x0600002C RID: 44
		[DllImport("libasound")]
		private static extern int snd_pcm_prepare(IntPtr handle);

		// Token: 0x0600002D RID: 45
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params(IntPtr handle, IntPtr param);

		// Token: 0x0600002E RID: 46
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_malloc(ref IntPtr param);

		// Token: 0x0600002F RID: 47
		[DllImport("libasound")]
		private static extern void snd_pcm_hw_params_free(IntPtr param);

		// Token: 0x06000030 RID: 48
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_any(IntPtr handle, IntPtr param);

		// Token: 0x06000031 RID: 49
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_set_access(IntPtr handle, IntPtr param, int access);

		// Token: 0x06000032 RID: 50
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_set_format(IntPtr handle, IntPtr param, int format);

		// Token: 0x06000033 RID: 51
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_set_channels(IntPtr handle, IntPtr param, uint channel);

		// Token: 0x06000034 RID: 52
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_set_rate_near(IntPtr handle, IntPtr param, ref uint rate, ref int dir);

		// Token: 0x06000035 RID: 53
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_set_period_time_near(IntPtr handle, IntPtr param, ref uint period, ref int dir);

		// Token: 0x06000036 RID: 54
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_get_period_size(IntPtr param, ref uint period, ref int dir);

		// Token: 0x06000037 RID: 55
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_set_buffer_size_near(IntPtr handle, IntPtr param, ref uint buff_size);

		// Token: 0x06000038 RID: 56
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_get_buffer_time_max(IntPtr param, ref uint buffer_time, ref int dir);

		// Token: 0x06000039 RID: 57
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_set_buffer_time_near(IntPtr handle, IntPtr param, ref uint BufferTime, ref int dir);

		// Token: 0x0600003A RID: 58
		[DllImport("libasound")]
		private static extern int snd_pcm_hw_params_get_buffer_size(IntPtr param, ref uint BufferSize);

		// Token: 0x0600003B RID: 59
		[DllImport("libasound")]
		private static extern int snd_pcm_sw_params(IntPtr handle, IntPtr param);

		// Token: 0x0600003C RID: 60
		[DllImport("libasound")]
		private static extern int snd_pcm_sw_params_malloc(ref IntPtr param);

		// Token: 0x0600003D RID: 61
		[DllImport("libasound")]
		private static extern void snd_pcm_sw_params_free(IntPtr param);

		// Token: 0x0600003E RID: 62
		[DllImport("libasound")]
		private static extern int snd_pcm_sw_params_current(IntPtr handle, IntPtr param);

		// Token: 0x0600003F RID: 63
		[DllImport("libasound")]
		private static extern int snd_pcm_sw_params_set_avail_min(IntPtr handle, IntPtr param, uint frames);

		// Token: 0x06000040 RID: 64
		[DllImport("libasound")]
		private static extern int snd_pcm_sw_params_set_start_threshold(IntPtr handle, IntPtr param, uint StartThreshold);

		// Token: 0x06000041 RID: 65 RVA: 0x000027F4 File Offset: 0x000009F4
		public AlsaDevice(string name)
		{
			if (name == null)
			{
				name = "default";
			}
			int num = AlsaDevice.snd_pcm_open(ref this.handle, name, 0, 0);
			if (num < 0)
			{
				throw new Exception("no open " + num);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000283C File Offset: 0x00000A3C
		~AlsaDevice()
		{
			this.Dispose(false);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000286C File Offset: 0x00000A6C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x0000287C File Offset: 0x00000A7C
		protected virtual void Dispose(bool disposing)
		{
			if (this.sw_param != IntPtr.Zero)
			{
				AlsaDevice.snd_pcm_sw_params_free(this.sw_param);
			}
			if (this.hw_param != IntPtr.Zero)
			{
				AlsaDevice.snd_pcm_hw_params_free(this.hw_param);
			}
			if (this.handle != IntPtr.Zero)
			{
				AlsaDevice.snd_pcm_close(this.handle);
			}
			this.sw_param = IntPtr.Zero;
			this.hw_param = IntPtr.Zero;
			this.handle = IntPtr.Zero;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002904 File Offset: 0x00000B04
		public override bool SetFormat(AudioFormat format, int channels, int rate)
		{
			uint num = 0U;
			uint num2 = 0U;
			uint num3 = 0U;
			uint num4 = 0U;
			int num5 = 0;
			uint num6 = (uint)rate;
			if (AlsaDevice.snd_pcm_hw_params_malloc(ref this.hw_param) == 0)
			{
				AlsaDevice.snd_pcm_hw_params_any(this.handle, this.hw_param);
				AlsaDevice.snd_pcm_hw_params_set_access(this.handle, this.hw_param, 3);
				AlsaDevice.snd_pcm_hw_params_set_format(this.handle, this.hw_param, (int)format);
				AlsaDevice.snd_pcm_hw_params_set_channels(this.handle, this.hw_param, (uint)channels);
				num5 = 0;
				AlsaDevice.snd_pcm_hw_params_set_rate_near(this.handle, this.hw_param, ref num6, ref num5);
				num5 = 0;
				AlsaDevice.snd_pcm_hw_params_get_buffer_time_max(this.hw_param, ref num4, ref num5);
				if (num4 > 500000U)
				{
					num4 = 500000U;
				}
				if (num4 > 0U)
				{
					num = num4 / 4U;
				}
				num5 = 0;
				AlsaDevice.snd_pcm_hw_params_set_period_time_near(this.handle, this.hw_param, ref num, ref num5);
				num5 = 0;
				AlsaDevice.snd_pcm_hw_params_set_buffer_time_near(this.handle, this.hw_param, ref num4, ref num5);
				AlsaDevice.snd_pcm_hw_params_get_period_size(this.hw_param, ref num2, ref num5);
				this.chunk_size = num2;
				AlsaDevice.snd_pcm_hw_params_get_buffer_size(this.hw_param, ref num3);
				AlsaDevice.snd_pcm_hw_params(this.handle, this.hw_param);
			}
			else
			{
				Console.WriteLine("failed to alloc Alsa hw param struct");
			}
			int num7 = AlsaDevice.snd_pcm_sw_params_malloc(ref this.sw_param);
			if (num7 == 0)
			{
				AlsaDevice.snd_pcm_sw_params_current(this.handle, this.sw_param);
				AlsaDevice.snd_pcm_sw_params_set_avail_min(this.handle, this.sw_param, this.chunk_size);
				AlsaDevice.snd_pcm_sw_params_set_start_threshold(this.handle, this.sw_param, num3);
				AlsaDevice.snd_pcm_sw_params(this.handle, this.sw_param);
			}
			else
			{
				Console.WriteLine("failed to alloc Alsa sw param struct");
			}
			if (this.hw_param != IntPtr.Zero)
			{
				AlsaDevice.snd_pcm_hw_params_free(this.hw_param);
				this.hw_param = IntPtr.Zero;
			}
			if (this.sw_param != IntPtr.Zero)
			{
				AlsaDevice.snd_pcm_sw_params_free(this.sw_param);
				this.sw_param = IntPtr.Zero;
			}
			return num7 == 0;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002AF0 File Offset: 0x00000CF0
		public override int PlaySample(byte[] buffer, int num_frames)
		{
			int num;
			do
			{
				num = AlsaDevice.snd_pcm_writei(this.handle, buffer, num_frames);
				if (num < 0)
				{
					this.XRunRecovery(num);
				}
			}
			while (num < 0);
			return num;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002B1C File Offset: 0x00000D1C
		public override int XRunRecovery(int err)
		{
			int num = 0;
			if (-32 == err)
			{
				num = AlsaDevice.snd_pcm_prepare(this.handle);
			}
			return num;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002B3D File Offset: 0x00000D3D
		public override void Wait()
		{
			AlsaDevice.snd_pcm_drain(this.handle);
		}

		// Token: 0x040006C8 RID: 1736
		private IntPtr handle;

		// Token: 0x040006C9 RID: 1737
		private IntPtr hw_param;

		// Token: 0x040006CA RID: 1738
		private IntPtr sw_param;
	}
}
