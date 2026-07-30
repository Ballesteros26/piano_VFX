using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000021 RID: 33
	internal abstract class TextureCache
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00003A90 File Offset: 0x00001C90
		protected TextureCache(string cacheName, int sliceSize = 1)
		{
			this.m_CacheName = cacheName;
			this.m_SliceSize = sliceSize;
			this.m_NumTextures = 0;
			this.m_NumMipLevels = 0;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003AC0 File Offset: 0x00001CC0
		public virtual bool IsCreated()
		{
			return true;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003AC3 File Offset: 0x00001CC3
		public string GetCacheName()
		{
			return this.m_CacheName;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003ACB File Offset: 0x00001CCB
		public int GetNumMipLevels()
		{
			return this.m_NumMipLevels;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00003AD4 File Offset: 0x00001CD4
		protected bool AllocTextureArray(int numTextures)
		{
			if (numTextures >= this.m_SliceSize)
			{
				this.m_SliceArray = new TextureCache.SliceEntry[numTextures];
				this.m_SortedIdxArray = new int[numTextures];
				this.m_LocatorInSliceDictionnary = new Dictionary<uint, int>();
				this.m_NumTextures = numTextures / this.m_SliceSize;
				for (int i = 0; i < this.m_NumTextures; i++)
				{
					this.m_SliceArray[i].countLRU = TextureCache.g_MaxFrameCount;
					this.m_SliceArray[i].texId = TextureCache.g_InvalidTexID;
					this.m_SortedIdxArray[i] = i;
				}
			}
			return numTextures >= this.m_SliceSize;
		}

		// Token: 0x06000041 RID: 65
		public abstract Texture GetTexCache();

		// Token: 0x06000042 RID: 66 RVA: 0x00003B6D File Offset: 0x00001D6D
		public uint GetTextureHash(Texture texture)
		{
			return texture.updateCount;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003B78 File Offset: 0x00001D78
		public int ReserveSlice(Texture texture, out bool needUpdate)
		{
			needUpdate = false;
			if (texture == null)
			{
				return -1;
			}
			uint instanceID = (uint)texture.GetInstanceID();
			if (instanceID == TextureCache.g_InvalidTexID)
			{
				return -1;
			}
			int num = -1;
			if (this.m_LocatorInSliceDictionnary.TryGetValue(instanceID, out num))
			{
				uint textureHash = this.GetTextureHash(texture);
				needUpdate |= this.m_SliceArray[num].sliceEntryHash != textureHash;
			}
			else
			{
				bool flag = false;
				int num2 = 0;
				int num3 = 0;
				while (!flag && num2 < this.m_NumTextures)
				{
					num3 = this.m_SortedIdxArray[num2];
					if (this.m_SliceArray[num3].countLRU == 0U)
					{
						num2++;
					}
					else
					{
						flag = true;
					}
				}
				if (flag)
				{
					needUpdate = true;
					if (this.m_SliceArray[num3].texId != TextureCache.g_InvalidTexID)
					{
						this.m_LocatorInSliceDictionnary.Remove(this.m_SliceArray[num3].texId);
					}
					this.m_LocatorInSliceDictionnary.Add(instanceID, num3);
					this.m_SliceArray[num3].texId = instanceID;
					num = num3;
				}
			}
			if (num != -1)
			{
				this.m_SliceArray[num].countLRU = 0U;
			}
			needUpdate |= !this.IsCreated();
			return num;
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003CA5 File Offset: 0x00001EA5
		public bool UpdateSlice(CommandBuffer cmd, int sliceIndex, Texture[] contentArray, uint textureHash)
		{
			this.SetSliceHash(sliceIndex, textureHash);
			return this.TransferToSlice(cmd, sliceIndex, contentArray);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003CB9 File Offset: 0x00001EB9
		public bool UpdateSlice(CommandBuffer cmd, int sliceIndex, Texture texture, uint textureHash)
		{
			this.SetSliceHash(sliceIndex, textureHash);
			this.m_autoContentArray[0] = texture;
			return this.TransferToSlice(cmd, sliceIndex, this.m_autoContentArray);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003CDB File Offset: 0x00001EDB
		public void SetSliceHash(int sliceIndex, uint hash)
		{
			this.m_SliceArray[sliceIndex].sliceEntryHash = hash;
		}

		// Token: 0x06000047 RID: 71
		protected abstract bool TransferToSlice(CommandBuffer cmd, int sliceIndex, Texture[] textureArray);

		// Token: 0x06000048 RID: 72 RVA: 0x00003CF0 File Offset: 0x00001EF0
		public int FetchSlice(CommandBuffer cmd, Texture texture, bool forceReinject = false)
		{
			bool flag = false;
			int num = this.ReserveSlice(texture, out flag);
			bool flag2 = forceReinject || flag;
			if (num != -1 && flag2)
			{
				this.m_autoContentArray[0] = texture;
				this.UpdateSlice(cmd, num, this.m_autoContentArray, this.GetTextureHash(texture));
			}
			return num;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003D38 File Offset: 0x00001F38
		public void NewFrame()
		{
			int num = 0;
			TextureCache.s_TempIntList.Clear();
			for (int i = 0; i < this.m_NumTextures; i++)
			{
				TextureCache.s_TempIntList.Add(this.m_SortedIdxArray[i]);
				if (this.m_SliceArray[this.m_SortedIdxArray[i]].countLRU != 0U)
				{
					num++;
				}
			}
			int num2 = 0;
			int num3 = 0;
			for (int j = 0; j < this.m_NumTextures; j++)
			{
				if (this.m_SliceArray[TextureCache.s_TempIntList[j]].countLRU == 0U)
				{
					this.m_SortedIdxArray[num3 + num] = TextureCache.s_TempIntList[j];
					num3++;
				}
				else
				{
					this.m_SortedIdxArray[num2] = TextureCache.s_TempIntList[j];
					num2++;
				}
			}
			for (int k = 0; k < this.m_NumTextures; k++)
			{
				if (this.m_SliceArray[k].countLRU < TextureCache.g_MaxFrameCount)
				{
					TextureCache.SliceEntry[] sliceArray = this.m_SliceArray;
					int num4 = k;
					sliceArray[num4].countLRU = sliceArray[num4].countLRU + 1U;
				}
			}
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003E44 File Offset: 0x00002044
		public void RemoveEntryFromSlice(Texture texture)
		{
			uint instanceID = (uint)texture.GetInstanceID();
			if (instanceID == TextureCache.g_InvalidTexID)
			{
				return;
			}
			if (!this.m_LocatorInSliceDictionnary.ContainsKey(instanceID))
			{
				return;
			}
			int num = this.m_LocatorInSliceDictionnary[instanceID];
			bool flag = false;
			int num2 = 0;
			while (!flag && num2 < this.m_NumTextures)
			{
				if (this.m_SortedIdxArray[num2] == num)
				{
					flag = true;
				}
				else
				{
					num2++;
				}
			}
			if (!flag)
			{
				return;
			}
			for (int i = 0; i < num2; i++)
			{
				this.m_SortedIdxArray[i + 1] = this.m_SortedIdxArray[i];
			}
			this.m_SortedIdxArray[0] = num;
			this.m_LocatorInSliceDictionnary.Remove(instanceID);
			this.m_SliceArray[num].countLRU = TextureCache.g_MaxFrameCount;
			this.m_SliceArray[num].texId = TextureCache.g_InvalidTexID;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003F0C File Offset: 0x0000210C
		protected int GetNumMips(int width, int height)
		{
			return this.GetNumMips((width > height) ? width : height);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003F1C File Offset: 0x0000211C
		protected int GetNumMips(int dim)
		{
			uint num = (uint)dim;
			int num2 = 0;
			while (num > 0U)
			{
				num2++;
				num >>= 1;
			}
			return num2;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00003F3C File Offset: 0x0000213C
		public static bool isMobileBuildTarget
		{
			get
			{
				return Application.isMobilePlatform;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003F44 File Offset: 0x00002144
		public static TextureFormat GetPreferredHDRCompressedTextureFormat
		{
			get
			{
				TextureFormat textureFormat = TextureFormat.RGBAHalf;
				TextureFormat textureFormat2 = TextureFormat.BC6H;
				if (SystemInfo.SupportsTextureFormat(textureFormat2) && !GraphicsSettings.HasShaderDefine(BuiltinShaderDefine.UNITY_NO_DXT5nm))
				{
					textureFormat = textureFormat2;
				}
				return textureFormat;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003F6A File Offset: 0x0000216A
		public static bool supportsCubemapArrayTextures
		{
			get
			{
				return !GraphicsSettings.HasShaderDefine(BuiltinShaderDefine.UNITY_NO_CUBEMAP_ARRAY);
			}
		}

		// Token: 0x0400007E RID: 126
		protected string m_CacheName;

		// Token: 0x0400007F RID: 127
		protected int m_NumMipLevels;

		// Token: 0x04000080 RID: 128
		protected int m_SliceSize;

		// Token: 0x04000081 RID: 129
		private int m_NumTextures;

		// Token: 0x04000082 RID: 130
		private Dictionary<uint, int> m_LocatorInSliceDictionnary;

		// Token: 0x04000083 RID: 131
		private TextureCache.SliceEntry[] m_SliceArray;

		// Token: 0x04000084 RID: 132
		private int[] m_SortedIdxArray;

		// Token: 0x04000085 RID: 133
		private Texture[] m_autoContentArray = new Texture[1];

		// Token: 0x04000086 RID: 134
		private static uint g_MaxFrameCount = uint.MaxValue;

		// Token: 0x04000087 RID: 135
		private static uint g_InvalidTexID = 0U;

		// Token: 0x04000088 RID: 136
		protected const int k_FP16SizeInByte = 2;

		// Token: 0x04000089 RID: 137
		protected const int k_NbChannel = 4;

		// Token: 0x0400008A RID: 138
		protected const float k_MipmapFactorApprox = 1.33f;

		// Token: 0x0400008B RID: 139
		internal const int k_MaxSupported = 250;

		// Token: 0x0400008C RID: 140
		private static List<int> s_TempIntList = new List<int>();

		// Token: 0x0200018E RID: 398
		private struct SliceEntry
		{
			// Token: 0x040010AC RID: 4268
			public uint texId;

			// Token: 0x040010AD RID: 4269
			public uint countLRU;

			// Token: 0x040010AE RID: 4270
			public uint sliceEntryHash;
		}
	}
}
