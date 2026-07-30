using System;
using UnityEngine.Serialization;

namespace UnityEngine.VFX.Utility
{
	// Token: 0x0200000D RID: 13
	[AddComponentMenu("VFX/Property Binders/Audio Spectrum Binder")]
	[VFXBinder("Audio/Audio Spectrum to AttributeMap")]
	internal class VFXAudioSpectrumBinder : VFXBinderBase
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000033 RID: 51 RVA: 0x0000278E File Offset: 0x0000098E
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000279B File Offset: 0x0000099B
		public string CountProperty
		{
			get
			{
				return (string)this.m_CountProperty;
			}
			set
			{
				this.m_CountProperty = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000027A9 File Offset: 0x000009A9
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000027B6 File Offset: 0x000009B6
		public string TextureProperty
		{
			get
			{
				return (string)this.m_TextureProperty;
			}
			set
			{
				this.m_TextureProperty = value;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000027C4 File Offset: 0x000009C4
		public override bool IsValid(VisualEffect component)
		{
			bool flag = this.Mode != VFXAudioSpectrumBinder.AudioSourceMode.AudioSource || this.AudioSource != null;
			bool flag2 = component.HasTexture(this.TextureProperty);
			bool flag3 = component.HasUInt(this.CountProperty);
			return flag && flag2 && flag3;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002808 File Offset: 0x00000A08
		private void UpdateTexture()
		{
			if (this.m_Texture == null || (long)this.m_Texture.width != (long)((ulong)this.Samples))
			{
				this.m_Texture = new Texture2D((int)this.Samples, 1, TextureFormat.RFloat, false);
				this.m_AudioCache = new float[this.Samples];
				this.m_ColorCache = new Color[this.Samples];
			}
			if (this.Mode == VFXAudioSpectrumBinder.AudioSourceMode.AudioListener)
			{
				AudioListener.GetSpectrumData(this.m_AudioCache, 0, this.FFTWindow);
			}
			else
			{
				if (this.Mode != VFXAudioSpectrumBinder.AudioSourceMode.AudioSource)
				{
					throw new NotImplementedException();
				}
				this.AudioSource.GetSpectrumData(this.m_AudioCache, 0, this.FFTWindow);
			}
			int num = 0;
			while ((long)num < (long)((ulong)this.Samples))
			{
				this.m_ColorCache[num] = new Color(this.m_AudioCache[num], 0f, 0f, 0f);
				num++;
			}
			this.m_Texture.SetPixels(this.m_ColorCache);
			this.m_Texture.name = "AudioSpectrum" + this.Samples;
			this.m_Texture.Apply();
		}

		// Token: 0x06000039 RID: 57 RVA: 0x0000292B File Offset: 0x00000B2B
		public override void UpdateBinding(VisualEffect component)
		{
			this.UpdateTexture();
			component.SetTexture(this.TextureProperty, this.m_Texture);
			component.SetUInt(this.CountProperty, this.Samples);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002957 File Offset: 0x00000B57
		public override string ToString()
		{
			return string.Format("Audio Spectrum : '{0} samples' -> {1}", this.m_CountProperty, (this.Mode == VFXAudioSpectrumBinder.AudioSourceMode.AudioSource) ? "AudioSource" : "AudioListener");
		}

		// Token: 0x04000018 RID: 24
		[VFXPropertyBinding(new string[] { "System.UInt32" })]
		[SerializeField]
		[FormerlySerializedAs("m_CountParameter")]
		protected ExposedProperty m_CountProperty = "Count";

		// Token: 0x04000019 RID: 25
		[VFXPropertyBinding(new string[] { "UnityEngine.Texture2D" })]
		[SerializeField]
		[FormerlySerializedAs("m_TextureParameter")]
		protected ExposedProperty m_TextureProperty = "SpectrumTexture";

		// Token: 0x0400001A RID: 26
		public FFTWindow FFTWindow = FFTWindow.BlackmanHarris;

		// Token: 0x0400001B RID: 27
		public uint Samples = 64U;

		// Token: 0x0400001C RID: 28
		public VFXAudioSpectrumBinder.AudioSourceMode Mode;

		// Token: 0x0400001D RID: 29
		public AudioSource AudioSource;

		// Token: 0x0400001E RID: 30
		private Texture2D m_Texture;

		// Token: 0x0400001F RID: 31
		private float[] m_AudioCache;

		// Token: 0x04000020 RID: 32
		private Color[] m_ColorCache;

		// Token: 0x0200002F RID: 47
		public enum AudioSourceMode
		{
			// Token: 0x040000BE RID: 190
			AudioSource,
			// Token: 0x040000BF RID: 191
			AudioListener
		}
	}
}
