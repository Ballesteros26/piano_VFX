using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	public class TextureCurve : IDisposable
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000BC29 File Offset: 0x00009E29
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000BC31 File Offset: 0x00009E31
		public int length { get; private set; }

		// Token: 0x17000077 RID: 119
		public Keyframe this[int index]
		{
			get
			{
				return this.m_Curve[index];
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000BC48 File Offset: 0x00009E48
		public TextureCurve(AnimationCurve baseCurve, float zeroValue, bool loop, in Vector2 bounds)
			: this(baseCurve.keys, zeroValue, loop, in bounds)
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000BC5C File Offset: 0x00009E5C
		public TextureCurve(Keyframe[] keys, float zeroValue, bool loop, in Vector2 bounds)
		{
			this.m_Curve = new AnimationCurve(keys);
			this.m_ZeroValue = zeroValue;
			this.m_Loop = loop;
			Vector2 vector = bounds;
			this.m_Range = vector.magnitude;
			this.length = keys.Length;
			this.SetDirty();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		~TextureCurve()
		{
			this.ReleaseUnityResources();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000BCDC File Offset: 0x00009EDC
		public void Dispose()
		{
			this.ReleaseUnityResources();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000BCEA File Offset: 0x00009EEA
		private void ReleaseUnityResources()
		{
			CoreUtils.Destroy(this.m_Texture);
			this.m_Texture = null;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000BCFE File Offset: 0x00009EFE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetDirty()
		{
			this.m_IsCurveDirty = true;
			this.m_IsTextureDirty = true;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000BD0E File Offset: 0x00009F0E
		private static TextureFormat GetTextureFormat()
		{
			if (SystemInfo.SupportsTextureFormat(TextureFormat.RHalf))
			{
				return TextureFormat.RHalf;
			}
			if (SystemInfo.SupportsTextureFormat(TextureFormat.R8))
			{
				return TextureFormat.R8;
			}
			return TextureFormat.ARGB32;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000BD2C File Offset: 0x00009F2C
		public Texture2D GetTexture()
		{
			if (this.m_IsTextureDirty)
			{
				if (this.m_Texture == null)
				{
					this.m_Texture = new Texture2D(128, 1, TextureCurve.GetTextureFormat(), false, true);
					this.m_Texture.name = "CurveTexture";
					this.m_Texture.hideFlags = HideFlags.HideAndDontSave;
					this.m_Texture.filterMode = FilterMode.Bilinear;
					this.m_Texture.wrapMode = TextureWrapMode.Clamp;
				}
				Color[] array = new Color[128];
				for (int i = 0; i < array.Length; i++)
				{
					array[i].r = this.Evaluate((float)i * 0.0078125f);
				}
				this.m_Texture.SetPixels(array);
				this.m_Texture.Apply(false, false);
				this.m_IsTextureDirty = false;
			}
			return this.m_Texture;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000BDF8 File Offset: 0x00009FF8
		public float Evaluate(float time)
		{
			if (this.m_IsCurveDirty)
			{
				this.length = this.m_Curve.length;
			}
			if (this.length == 0)
			{
				return this.m_ZeroValue;
			}
			if (!this.m_Loop || this.length == 1)
			{
				return this.m_Curve.Evaluate(time);
			}
			if (this.m_IsCurveDirty)
			{
				if (this.m_LoopingCurve == null)
				{
					this.m_LoopingCurve = new AnimationCurve();
				}
				Keyframe keyframe = this.m_Curve[this.length - 1];
				keyframe.time -= this.m_Range;
				Keyframe keyframe2 = this.m_Curve[0];
				keyframe2.time += this.m_Range;
				this.m_LoopingCurve.keys = this.m_Curve.keys;
				this.m_LoopingCurve.AddKey(keyframe);
				this.m_LoopingCurve.AddKey(keyframe2);
				this.m_IsCurveDirty = false;
			}
			return this.m_LoopingCurve.Evaluate(time);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000BEF5 File Offset: 0x0000A0F5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int AddKey(float time, float value)
		{
			int num = this.m_Curve.AddKey(time, value);
			if (num > -1)
			{
				this.SetDirty();
			}
			return num;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000BF0E File Offset: 0x0000A10E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public int MoveKey(int index, in Keyframe key)
		{
			int num = this.m_Curve.MoveKey(index, key);
			this.SetDirty();
			return num;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x0000BF28 File Offset: 0x0000A128
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void RemoveKey(int index)
		{
			this.m_Curve.RemoveKey(index);
			this.SetDirty();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000BF3C File Offset: 0x0000A13C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SmoothTangents(int index, float weight)
		{
			this.m_Curve.SmoothTangents(index, weight);
			this.SetDirty();
		}

		// Token: 0x0400018A RID: 394
		private const int k_Precision = 128;

		// Token: 0x0400018B RID: 395
		private const float k_Step = 0.0078125f;

		// Token: 0x0400018D RID: 397
		[SerializeField]
		private bool m_Loop;

		// Token: 0x0400018E RID: 398
		[SerializeField]
		private float m_ZeroValue;

		// Token: 0x0400018F RID: 399
		[SerializeField]
		private float m_Range;

		// Token: 0x04000190 RID: 400
		[SerializeField]
		private AnimationCurve m_Curve;

		// Token: 0x04000191 RID: 401
		private AnimationCurve m_LoopingCurve;

		// Token: 0x04000192 RID: 402
		private Texture2D m_Texture;

		// Token: 0x04000193 RID: 403
		private bool m_IsCurveDirty;

		// Token: 0x04000194 RID: 404
		private bool m_IsTextureDirty;
	}
}
