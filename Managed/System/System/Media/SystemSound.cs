using System;
using System.IO;
using Unity;

namespace System.Media
{
	/// <summary>Represents a system sound type.</summary>
	/// <filterpriority>2</filterpriority>
	/// <completionlist cref="T:System.Media.SystemSounds" />
	// Token: 0x02000125 RID: 293
	public class SystemSound
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x000274CD File Offset: 0x000256CD
		internal SystemSound(string tag)
		{
			this.resource = typeof(SystemSound).Assembly.GetManifestResourceStream(tag + ".wav");
		}

		/// <summary>Plays the system sound type.</summary>
		// Token: 0x060007F1 RID: 2033 RVA: 0x000274FA File Offset: 0x000256FA
		public void Play()
		{
			new SoundPlayer(this.resource).Play();
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0000F0CE File Offset: 0x0000D2CE
		internal SystemSound()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000D85 RID: 3461
		private Stream resource;
	}
}
