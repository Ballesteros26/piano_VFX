using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000A1 RID: 161
	internal class DensityVolumeManager
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x000338FF File Offset: 0x00031AFF
		public static DensityVolumeManager manager
		{
			get
			{
				if (DensityVolumeManager._instance == null)
				{
					DensityVolumeManager._instance = new DensityVolumeManager();
				}
				return DensityVolumeManager._instance;
			}
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00033918 File Offset: 0x00031B18
		private DensityVolumeManager()
		{
			this.volumes = new List<DensityVolume>();
			this.volumeAtlas = new Texture3DAtlas(TextureFormat.Alpha8, DensityVolumeManager.volumeTextureSize);
			Texture3DAtlas texture3DAtlas = this.volumeAtlas;
			texture3DAtlas.OnAtlasUpdated = (Texture3DAtlas.AtlasUpdated)Delegate.Combine(texture3DAtlas.OnAtlasUpdated, new Texture3DAtlas.AtlasUpdated(this.AtlasUpdated));
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00033970 File Offset: 0x00031B70
		public void RegisterVolume(DensityVolume volume)
		{
			this.volumes.Add(volume);
			volume.OnTextureUpdated = (Action)Delegate.Combine(volume.OnTextureUpdated, new Action(this.TriggerVolumeAtlasRefresh));
			if (volume.parameters.volumeMask != null)
			{
				this.volumeAtlas.AddTexture(volume.parameters.volumeMask);
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x000339D4 File Offset: 0x00031BD4
		public void DeRegisterVolume(DensityVolume volume)
		{
			if (this.volumes.Contains(volume))
			{
				this.volumes.Remove(volume);
			}
			volume.OnTextureUpdated = (Action)Delegate.Remove(volume.OnTextureUpdated, new Action(this.TriggerVolumeAtlasRefresh));
			if (volume.parameters.volumeMask != null)
			{
				this.volumeAtlas.RemoveTexture(volume.parameters.volumeMask);
			}
			this.TriggerVolumeAtlasRefresh();
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00033A4D File Offset: 0x00031C4D
		public bool ContainsVolume(DensityVolume volume)
		{
			return this.volumes.Contains(volume);
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00033A5C File Offset: 0x00031C5C
		public List<DensityVolume> PrepareDensityVolumeData(CommandBuffer cmd, HDCamera currentCam, float time)
		{
			bool animateMaterials = currentCam.animateMaterials;
			foreach (DensityVolume densityVolume in this.volumes)
			{
				densityVolume.PrepareParameters(animateMaterials, time);
			}
			if (this.atlasNeedsRefresh)
			{
				this.atlasNeedsRefresh = false;
				this.VolumeAtlasRefresh();
			}
			this.volumeAtlas.GenerateAtlas(cmd);
			return this.volumes;
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00033ADC File Offset: 0x00031CDC
		private void VolumeAtlasRefresh()
		{
			this.volumeAtlas.ClearTextures();
			foreach (DensityVolume densityVolume in this.volumes)
			{
				if (densityVolume.parameters.volumeMask != null)
				{
					this.volumeAtlas.AddTexture(densityVolume.parameters.volumeMask);
				}
			}
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00033B5C File Offset: 0x00031D5C
		public void TriggerVolumeAtlasRefresh()
		{
			this.atlasNeedsRefresh = true;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00033B68 File Offset: 0x00031D68
		private void AtlasUpdated()
		{
			foreach (DensityVolume densityVolume in this.volumes)
			{
				densityVolume.parameters.textureIndex = this.volumeAtlas.GetTextureIndex(densityVolume.parameters.volumeMask);
			}
		}

		// Token: 0x04000681 RID: 1665
		private static DensityVolumeManager _instance = null;

		// Token: 0x04000682 RID: 1666
		public Texture3DAtlas volumeAtlas;

		// Token: 0x04000683 RID: 1667
		private bool atlasNeedsRefresh;

		// Token: 0x04000684 RID: 1668
		public static int volumeTextureSize = 32;

		// Token: 0x04000685 RID: 1669
		private List<DensityVolume> volumes;
	}
}
