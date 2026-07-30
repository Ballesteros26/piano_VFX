using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.Serialization;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200016C RID: 364
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/Static-Lighting-Sky.html")]
	[ExecuteAlways]
	[AddComponentMenu("")]
	internal class StaticLightingSky : MonoBehaviour
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0005283C File Offset: 0x00050A3C
		public SkySettings skySettings
		{
			get
			{
				SkySettings skySettings;
				Type type;
				this.GetSkyFromIDAndVolume(this.m_StaticLightingSkyUniqueID, this.m_Profile, out skySettings, out type);
				if (skySettings != null)
				{
					int hashCode = skySettings.GetHashCode();
					if (this.m_LastComputedHash != hashCode)
					{
						this.UpdateCurrentStaticLightingSky();
					}
				}
				else
				{
					this.Reset();
				}
				return this.m_SkySettings;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x0005288C File Offset: 0x00050A8C
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x00052894 File Offset: 0x00050A94
		public VolumeProfile profile
		{
			get
			{
				return this.m_Profile;
			}
			set
			{
				if (value != this.m_Profile)
				{
					this.m_StaticLightingSkyUniqueID = 0;
					if (this.m_Profile == null)
					{
						SkyManager.RegisterStaticLightingSky(this);
					}
					if (value == null)
					{
						SkyManager.UnRegisterStaticLightingSky(this);
					}
				}
				this.m_Profile = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x000528E0 File Offset: 0x00050AE0
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x000528E8 File Offset: 0x00050AE8
		public int staticLightingSkyUniqueID
		{
			get
			{
				return this.m_StaticLightingSkyUniqueID;
			}
			set
			{
				this.m_StaticLightingSkyUniqueID = value;
				this.UpdateCurrentStaticLightingSky();
			}
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000528F8 File Offset: 0x00050AF8
		private void GetSkyFromIDAndVolume(int skyUniqueID, VolumeProfile profile, out SkySettings skySetting, out Type skyType)
		{
			skySetting = null;
			skyType = typeof(SkySettings);
			if (profile != null && skyUniqueID != 0)
			{
				this.m_VolumeSkyList.Clear();
				if (profile.TryGetAllSubclassOf<SkySettings>(typeof(SkySettings), this.m_VolumeSkyList))
				{
					foreach (SkySettings skySettings in this.m_VolumeSkyList)
					{
						if (skyUniqueID == SkySettings.GetUniqueID(skySettings.GetType()))
						{
							skyType = skySettings.GetType();
							skySetting = skySettings;
						}
					}
				}
			}
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000529A0 File Offset: 0x00050BA0
		private void UpdateCurrentStaticLightingSky()
		{
			CoreUtils.Destroy(this.m_SkySettings);
			this.m_SkySettings = null;
			this.m_LastComputedHash = 0;
			Type type;
			this.GetSkyFromIDAndVolume(this.m_StaticLightingSkyUniqueID, this.m_Profile, out this.m_SkySettingsFromProfile, out type);
			if (this.m_SkySettingsFromProfile != null)
			{
				this.m_SkySettings = (SkySettings)ScriptableObject.CreateInstance(type);
				ReadOnlyCollection<VolumeParameter> parameters = this.m_SkySettings.parameters;
				ReadOnlyCollection<VolumeParameter> parameters2 = this.m_SkySettingsFromProfile.parameters;
				if (parameters2 == null)
				{
					return;
				}
				int count = this.m_SkySettings.parameters.Count;
				for (int i = 0; i < count; i++)
				{
					if (parameters2[i].overrideState)
					{
						parameters[i].SetValue(parameters2[i]);
					}
				}
				this.m_LastComputedHash = this.m_SkySettingsFromProfile.GetHashCode();
			}
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00052A74 File Offset: 0x00050C74
		private void OnValidate()
		{
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			if (this.m_Profile == null)
			{
				this.m_StaticLightingSkyUniqueID = 0;
			}
			if (this.profile != null && this.m_SkySettingsFromProfile != null && !this.profile.components.Find((VolumeComponent x) => x == this.m_SkySettingsFromProfile))
			{
				this.m_StaticLightingSkyUniqueID = 0;
			}
			this.UpdateCurrentStaticLightingSky();
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00052AEB File Offset: 0x00050CEB
		private void OnEnable()
		{
			this.UpdateCurrentStaticLightingSky();
			if (this.m_Profile != null)
			{
				SkyManager.RegisterStaticLightingSky(this);
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00052B07 File Offset: 0x00050D07
		private void OnDisable()
		{
			if (this.m_Profile != null)
			{
				SkyManager.UnRegisterStaticLightingSky(this);
			}
			this.Reset();
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x00052B23 File Offset: 0x00050D23
		private void Reset()
		{
			CoreUtils.Destroy(this.m_SkySettings);
			this.m_SkySettings = null;
			this.m_SkySettingsFromProfile = null;
			this.m_LastComputedHash = 0;
		}

		// Token: 0x04000FFF RID: 4095
		[SerializeField]
		private VolumeProfile m_Profile;

		// Token: 0x04001000 RID: 4096
		[SerializeField]
		[FormerlySerializedAs("m_BakingSkyUniqueID")]
		private int m_StaticLightingSkyUniqueID;

		// Token: 0x04001001 RID: 4097
		private int m_LastComputedHash;

		// Token: 0x04001002 RID: 4098
		public SkySettings m_SkySettings;

		// Token: 0x04001003 RID: 4099
		public SkySettings m_SkySettingsFromProfile;

		// Token: 0x04001004 RID: 4100
		private List<SkySettings> m_VolumeSkyList = new List<SkySettings>();
	}
}
