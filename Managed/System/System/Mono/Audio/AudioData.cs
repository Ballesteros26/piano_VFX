using System;

namespace Mono.Audio
{
	// Token: 0x02000005 RID: 5
	internal abstract class AudioData
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000C RID: 12
		public abstract int Channels { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13
		public abstract int Rate { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14
		public abstract AudioFormat Format { get; }

		// Token: 0x0600000F RID: 15 RVA: 0x000020BF File Offset: 0x000002BF
		public virtual void Setup(AudioDevice dev)
		{
			dev.SetFormat(this.Format, this.Channels, this.Rate);
		}

		// Token: 0x06000010 RID: 16
		public abstract void Play(AudioDevice dev);

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020DA File Offset: 0x000002DA
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000020E2 File Offset: 0x000002E2
		public virtual bool IsStopped
		{
			get
			{
				return this.stopped;
			}
			set
			{
				this.stopped = value;
			}
		}

		// Token: 0x0400069E RID: 1694
		protected const int buffer_size = 4096;

		// Token: 0x0400069F RID: 1695
		private bool stopped;
	}
}
