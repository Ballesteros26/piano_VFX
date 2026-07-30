using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200014D RID: 333
	internal class PowerOfTwoTextureAtlas : Texture2DAtlas
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x0004D49B File Offset: 0x0004B69B
		public PowerOfTwoTextureAtlas(int size, int mipPadding, GraphicsFormat format, FilterMode filterMode = FilterMode.Point, string name = "", bool useMipMap = true)
			: base(size, size, format, filterMode, true, name, useMipMap)
		{
			this.mipPadding = mipPadding;
			int num = size & (size - 1);
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0004D4C5 File Offset: 0x0004B6C5
		private int GetTexturePadding()
		{
			return (int)Mathf.Pow(2f, (float)this.mipPadding) * 2;
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0004D4DB File Offset: 0x0004B6DB
		private static int PreviousPowerOfTwo(int size)
		{
			if (size <= 0)
			{
				return 0;
			}
			size |= size >> 1;
			size |= size >> 2;
			size |= size >> 4;
			size |= size >> 8;
			size |= size >> 16;
			return size - (size >> 1);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0004D50C File Offset: 0x0004B70C
		private void Blit2DTexturePadding(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true)
		{
			int num = base.GetTextureMipmapCount(texture.width, texture.height);
			int texturePadding = this.GetTexturePadding();
			Vector2 powerOfTwoTextureSize = this.GetPowerOfTwoTextureSize(texture);
			bool flag = texture.filterMode > FilterMode.Point;
			if (!blitMips)
			{
				num = 1;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<HDProfileId>(HDProfileId.BlitTextureInPotAtlas)))
			{
				for (int i = 0; i < num; i++)
				{
					cmd.SetRenderTarget(this.m_AtlasTexture, i);
					HDUtils.BlitQuadWithPadding(cmd, texture, powerOfTwoTextureSize, sourceScaleOffset, scaleOffset, i, flag, texturePadding);
				}
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0004D5AC File Offset: 0x0004B7AC
		public override void BlitTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (base.Is2D(texture))
			{
				this.Blit2DTexturePadding(cmd, scaleOffset, texture, sourceScaleOffset, blitMips);
				base.MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0004D5DB File Offset: 0x0004B7DB
		private void TextureSizeToPowerOfTwo(Texture texture, ref int width, ref int height)
		{
			width = Mathf.NextPowerOfTwo(width);
			height = Mathf.NextPowerOfTwo(height);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0004D5F0 File Offset: 0x0004B7F0
		private Vector2 GetPowerOfTwoTextureSize(Texture texture)
		{
			int width = texture.width;
			int height = texture.height;
			this.TextureSizeToPowerOfTwo(texture, ref width, ref height);
			return new Vector2((float)width, (float)height);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0004D620 File Offset: 0x0004B820
		public override bool AllocateTexture(CommandBuffer cmd, ref Vector4 scaleOffset, Texture texture, int width, int height, int overrideInstanceID = -1)
		{
			if (height != width)
			{
				Debug.LogError(string.Concat(new object[]
				{
					"Can't place ",
					texture,
					" in the atlas ",
					this.m_AtlasTexture.name,
					": Only squared texture are allowed in this atlas."
				}));
				return false;
			}
			this.TextureSizeToPowerOfTwo(texture, ref height, ref width);
			return base.AllocateTexture(cmd, ref scaleOffset, texture, width, height, -1);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0004D688 File Offset: 0x0004B888
		public void ResetRequestedTexture()
		{
			this.m_RequestedTextures.Clear();
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0004D698 File Offset: 0x0004B898
		public bool ReserveSpace(Texture texture)
		{
			this.m_RequestedTextures[texture.GetInstanceID()] = new Vector2Int(texture.width, texture.height);
			Vector4 vector;
			if (!base.IsCached(out vector, texture))
			{
				Vector4 zero = Vector4.zero;
				if (!base.AllocateTextureWithoutBlit(texture, texture.width, texture.height, ref zero))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0004D6F4 File Offset: 0x0004B8F4
		public bool RelayoutEntries()
		{
			List<ValueTuple<int, Vector2Int>> list = new List<ValueTuple<int, Vector2Int>>();
			foreach (KeyValuePair<int, Vector2Int> keyValuePair in this.m_RequestedTextures)
			{
				list.Add(new ValueTuple<int, Vector2Int>(keyValuePair.Key, keyValuePair.Value));
			}
			base.ResetAllocator();
			list.Sort(([TupleElementNames(new string[] { "instanceId", "size" })] ValueTuple<int, Vector2Int> c1, [TupleElementNames(new string[] { "instanceId", "size" })] ValueTuple<int, Vector2Int> c2) => c2.Item2.magnitude.CompareTo(c1.Item2.magnitude));
			bool flag = true;
			Vector4 zero = Vector4.zero;
			foreach (ValueTuple<int, Vector2Int> valueTuple in list)
			{
				bool flag2 = flag;
				int item = valueTuple.Item1;
				Vector2Int vector2Int = valueTuple.Item2;
				int x = vector2Int.x;
				vector2Int = valueTuple.Item2;
				flag = flag2 & this.AllocateTextureWithoutBlit(item, x, vector2Int.y, ref zero);
			}
			return flag;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0004D800 File Offset: 0x0004BA00
		public static long GetApproxCacheSizeInByte(int nbElement, int resolution, bool hasMipmap, GraphicsFormat format)
		{
			return (long)((double)(nbElement * resolution * resolution) * (double)((hasMipmap ? 1.33f : 1f) * (float)HDUtils.GetFormatSizeInBytes(format)));
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0004D824 File Offset: 0x0004BA24
		public static int GetMaxCacheSizeForWeightInByte(int weight, bool hasMipmap, GraphicsFormat format)
		{
			float num = (float)HDUtils.GetFormatSizeInBytes(format) * (hasMipmap ? 1.33f : 1f);
			return PowerOfTwoTextureAtlas.PreviousPowerOfTwo((int)Mathf.Sqrt((float)weight / num));
		}

		// Token: 0x04000F24 RID: 3876
		public int mipPadding;

		// Token: 0x04000F25 RID: 3877
		private const float k_MipmapFactorApprox = 1.33f;

		// Token: 0x04000F26 RID: 3878
		private Dictionary<int, Vector2Int> m_RequestedTextures = new Dictionary<int, Vector2Int>();
	}
}
