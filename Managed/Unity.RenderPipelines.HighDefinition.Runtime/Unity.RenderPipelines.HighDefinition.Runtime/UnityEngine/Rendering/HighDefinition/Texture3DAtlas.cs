using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000A2 RID: 162
	internal class Texture3DAtlas
	{
		// Token: 0x06000634 RID: 1588 RVA: 0x00033BE7 File Offset: 0x00031DE7
		private void NotifyAtlasUpdated()
		{
			if (this.OnAtlasUpdated != null)
			{
				this.OnAtlasUpdated();
			}
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x00033BFC File Offset: 0x00031DFC
		public Texture3DAtlas(TextureFormat format, int textureSize)
		{
			this.m_format = format;
			this.m_atlasSize = textureSize;
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x00033C20 File Offset: 0x00031E20
		public void AddTexture(Texture3D tex)
		{
			if (this.m_textures.Contains(tex))
			{
				return;
			}
			if (tex.width != this.m_atlasSize || tex.height != this.m_atlasSize || tex.depth != this.m_atlasSize)
			{
				Debug.LogError(string.Format("3D Texture Atlas: Added texture {4} size {0}x{1}x{2} does not match size of atlas {3}x{3}x{3}", new object[] { tex.width, tex.height, tex.depth, this.m_atlasSize, tex.name }));
				return;
			}
			if (tex.format != this.m_format)
			{
				Debug.LogError(string.Format("3D Texture Atlas: Added texture {2} format {0} does not match format of atlas {1}", tex.format, this.m_format, tex.name));
				return;
			}
			this.m_textures.Add(tex);
			this.m_updateAtlas = true;
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x00033D0A File Offset: 0x00031F0A
		public void RemoveTexture(Texture3D tex)
		{
			if (this.m_textures.Contains(tex))
			{
				this.m_textures.Remove(tex);
				this.m_updateAtlas = true;
			}
		}

		// Token: 0x06000638 RID: 1592 RVA: 0x00033D2E File Offset: 0x00031F2E
		public void ClearTextures()
		{
			this.m_textures.Clear();
			this.m_updateAtlas = true;
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00033D42 File Offset: 0x00031F42
		public int GetTextureIndex(Texture3D tex)
		{
			return this.m_textures.IndexOf(tex);
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00033D50 File Offset: 0x00031F50
		public void GenerateAtlas(CommandBuffer cmd)
		{
			if (!this.m_updateAtlas)
			{
				return;
			}
			if (this.m_textures.Count > 0)
			{
				int num = this.m_atlasSize * this.m_atlasSize * this.m_atlasSize;
				Color[] array = new Color[num * this.m_textures.Count];
				this.m_atlas = new Texture3D(this.m_atlasSize, this.m_atlasSize, this.m_atlasSize * this.m_textures.Count, this.m_format, true);
				for (int i = 0; i < this.m_textures.Count; i++)
				{
					Color[] pixels = this.m_textures[i].GetPixels();
					Array.Copy(pixels, 0, array, num * i, pixels.Length);
				}
				this.m_atlas.SetPixels(array);
				this.m_atlas.Apply();
			}
			else
			{
				this.m_atlas = null;
			}
			this.NotifyAtlasUpdated();
			this.m_updateAtlas = false;
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00033E32 File Offset: 0x00032032
		public Texture3D GetAtlas()
		{
			return this.m_atlas;
		}

		// Token: 0x04000686 RID: 1670
		private List<Texture3D> m_textures = new List<Texture3D>();

		// Token: 0x04000687 RID: 1671
		private Texture3D m_atlas;

		// Token: 0x04000688 RID: 1672
		private TextureFormat m_format;

		// Token: 0x04000689 RID: 1673
		private bool m_updateAtlas;

		// Token: 0x0400068A RID: 1674
		private int m_atlasSize;

		// Token: 0x0400068B RID: 1675
		public Texture3DAtlas.AtlasUpdated OnAtlasUpdated;

		// Token: 0x02000221 RID: 545
		// (Invoke) Token: 0x06000C12 RID: 3090
		public delegate void AtlasUpdated();
	}
}
