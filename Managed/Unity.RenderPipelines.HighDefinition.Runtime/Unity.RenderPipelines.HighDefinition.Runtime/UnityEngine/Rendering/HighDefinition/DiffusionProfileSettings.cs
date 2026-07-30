using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x020000B3 RID: 179
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/Diffusion-Profile.html")]
	internal sealed class DiffusionProfileSettings : ScriptableObject, IVersionable<DiffusionProfileSettings.Version>
	{
		// Token: 0x17000101 RID: 257
		[Obsolete("Profiles are obsolete, only one diffusion profile per asset is allowed.")]
		internal DiffusionProfile this[int index]
		{
			get
			{
				return this.profile;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x00035B4A File Offset: 0x00033D4A
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x00035B52 File Offset: 0x00033D52
		DiffusionProfileSettings.Version IVersionable<DiffusionProfileSettings.Version>.version
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

		// Token: 0x060006B6 RID: 1718 RVA: 0x00035B5B File Offset: 0x00033D5B
		private void OnEnable()
		{
			if (this.profile == null)
			{
				this.profile = new DiffusionProfile(true);
			}
			this.profile.Validate();
			this.UpdateCache();
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x00035B84 File Offset: 0x00033D84
		internal void UpdateCache()
		{
			if (this.filterKernels == null)
			{
				this.filterKernels = new Vector4[55];
			}
			this.thicknessRemaps = new Vector4(this.profile.thicknessRemap.x, this.profile.thicknessRemap.y - this.profile.thicknessRemap.x, 0f, 0f);
			this.worldScales = new Vector4(this.profile.worldScale, 1f / this.profile.worldScale, 0f, 0f);
			this.shapeParams = this.profile.shapeParam * -0.48089835f;
			this.shapeParams.w = this.profile.maxRadius;
			float num = (this.profile.ior - 1f) / (this.profile.ior + 1f);
			num *= num;
			this.transmissionTintsAndFresnel0 = new Vector4(this.profile.transmissionTint.r * 0.25f, this.profile.transmissionTint.g * 0.25f, this.profile.transmissionTint.b * 0.25f, num);
			this.disabledTransmissionTintsAndFresnel0 = new Vector4(0f, 0f, 0f, num);
			for (int i = 0; i < 55; i++)
			{
				this.filterKernels[i].x = this.profile.filterKernelNearField[i].x;
				this.filterKernels[i].y = this.profile.filterKernelNearField[i].y;
				if (i < 21)
				{
					this.filterKernels[i].z = this.profile.filterKernelFarField[i].x;
					this.filterKernels[i].w = this.profile.filterKernelFarField[i].y;
				}
			}
			this.updateCount++;
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x00035DA6 File Offset: 0x00033FA6
		internal bool HasChanged(int update)
		{
			return update == this.updateCount;
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00035DB4 File Offset: 0x00033FB4
		public void SetDefaultParams()
		{
			this.worldScales = Vector4.one;
			this.shapeParams = Vector4.zero;
			this.transmissionTintsAndFresnel0.w = 0.04f;
			for (int i = 0; i < 55; i++)
			{
				this.filterKernels[i].x = 0f;
				this.filterKernels[i].y = 1f;
				this.filterKernels[i].z = 0f;
				this.filterKernels[i].w = 1f;
			}
		}

		// Token: 0x040006F3 RID: 1779
		[SerializeField]
		private DiffusionProfileSettings.Version m_Version = MigrationDescription.LastVersion<DiffusionProfileSettings.Version>();

		// Token: 0x040006F4 RID: 1780
		[Obsolete("Profiles are obsolete, only one diffusion profile per asset is allowed.")]
		internal DiffusionProfile[] profiles;

		// Token: 0x040006F5 RID: 1781
		private static readonly MigrationDescription<DiffusionProfileSettings.Version, DiffusionProfileSettings> k_Migration = MigrationDescription.New<DiffusionProfileSettings.Version, DiffusionProfileSettings>(new MigrationStep<DiffusionProfileSettings.Version, DiffusionProfileSettings>[] { MigrationStep.New<DiffusionProfileSettings.Version, DiffusionProfileSettings>(DiffusionProfileSettings.Version.DiffusionProfileRework, delegate(DiffusionProfileSettings d)
		{
		}) });

		// Token: 0x040006F6 RID: 1782
		[SerializeField]
		internal DiffusionProfile profile;

		// Token: 0x040006F7 RID: 1783
		[NonSerialized]
		internal Vector4 thicknessRemaps;

		// Token: 0x040006F8 RID: 1784
		[NonSerialized]
		internal Vector4 worldScales;

		// Token: 0x040006F9 RID: 1785
		[NonSerialized]
		internal Vector4 shapeParams;

		// Token: 0x040006FA RID: 1786
		[NonSerialized]
		internal Vector4 transmissionTintsAndFresnel0;

		// Token: 0x040006FB RID: 1787
		[NonSerialized]
		internal Vector4 disabledTransmissionTintsAndFresnel0;

		// Token: 0x040006FC RID: 1788
		[NonSerialized]
		internal Vector4[] filterKernels;

		// Token: 0x040006FD RID: 1789
		[NonSerialized]
		internal int updateCount;

		// Token: 0x02000234 RID: 564
		private enum Version
		{
			// Token: 0x04001472 RID: 5234
			Initial,
			// Token: 0x04001473 RID: 5235
			DiffusionProfileRework
		}
	}
}
