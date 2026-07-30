using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000A0 RID: 160
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/Density-Volume.html")]
	[ExecuteAlways]
	[AddComponentMenu("Rendering/Density Volume")]
	public class DensityVolume : MonoBehaviour, IVersionable<DensityVolume.Version>
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x000337D0 File Offset: 0x000319D0
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x000337D8 File Offset: 0x000319D8
		DensityVolume.Version IVersionable<DensityVolume.Version>.version
		{
			get
			{
				return this.m_Version;
			}
			set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000337E4 File Offset: 0x000319E4
		private void Awake()
		{
			DensityVolume.k_Migration.Migrate(this);
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00033800 File Offset: 0x00031A00
		internal void PrepareParameters(bool animate, float time)
		{
			if (this.previousVolumeMask != this.parameters.volumeMask)
			{
				this.NotifyUpdatedTexure();
				this.previousVolumeMask = this.parameters.volumeMask;
			}
			this.parameters.Update(animate, time);
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0003383E File Offset: 0x00031A3E
		private void NotifyUpdatedTexure()
		{
			if (this.OnTextureUpdated != null)
			{
				this.OnTextureUpdated();
			}
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00033853 File Offset: 0x00031A53
		private void OnEnable()
		{
			DensityVolumeManager.manager.RegisterVolume(this);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00033860 File Offset: 0x00031A60
		private void OnDisable()
		{
			DensityVolumeManager.manager.DeRegisterVolume(this);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00002646 File Offset: 0x00000846
		private void Update()
		{
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0003386D File Offset: 0x00031A6D
		private void OnValidate()
		{
			this.parameters.Constrain();
		}

		// Token: 0x0400067C RID: 1660
		private static readonly MigrationDescription<DensityVolume.Version, DensityVolume> k_Migration = MigrationDescription.New<DensityVolume.Version, DensityVolume>(new MigrationStep<DensityVolume.Version, DensityVolume>[]
		{
			MigrationStep.New<DensityVolume.Version, DensityVolume>(DensityVolume.Version.ScaleIndependent, delegate(DensityVolume data)
			{
				data.parameters.size = data.transform.lossyScale;
				data.parameters.m_EditorAdvancedFade = true;
			}),
			MigrationStep.New<DensityVolume.Version, DensityVolume>(DensityVolume.Version.FixUniformBlendDistanceToBeMetric, delegate(DensityVolume data)
			{
				data.parameters.MigrateToFixUniformBlendDistanceToBeMetric();
			})
		});

		// Token: 0x0400067D RID: 1661
		[SerializeField]
		private DensityVolume.Version m_Version = MigrationDescription.LastVersion<DensityVolume.Version>();

		// Token: 0x0400067E RID: 1662
		public DensityVolumeArtistParameters parameters = new DensityVolumeArtistParameters(Color.white, 10f, 0f);

		// Token: 0x0400067F RID: 1663
		private Texture3D previousVolumeMask;

		// Token: 0x04000680 RID: 1664
		public Action OnTextureUpdated;

		// Token: 0x0200021F RID: 543
		private enum Version
		{
			// Token: 0x040013E9 RID: 5097
			First,
			// Token: 0x040013EA RID: 5098
			ScaleIndependent,
			// Token: 0x040013EB RID: 5099
			FixUniformBlendDistanceToBeMetric
		}
	}
}
