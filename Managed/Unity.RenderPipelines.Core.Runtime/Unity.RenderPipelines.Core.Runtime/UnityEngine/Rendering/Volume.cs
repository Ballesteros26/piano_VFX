using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000063 RID: 99
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/Volumes.html")]
	[ExecuteAlways]
	[AddComponentMenu("Miscellaneous/Volume")]
	public class Volume : MonoBehaviour
	{
		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000C2D4 File Offset: 0x0000A4D4
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0000C368 File Offset: 0x0000A568
		public VolumeProfile profile
		{
			get
			{
				if (this.m_InternalProfile == null)
				{
					this.m_InternalProfile = ScriptableObject.CreateInstance<VolumeProfile>();
					if (this.sharedProfile != null)
					{
						foreach (VolumeComponent volumeComponent in this.sharedProfile.components)
						{
							VolumeComponent volumeComponent2 = Object.Instantiate<VolumeComponent>(volumeComponent);
							this.m_InternalProfile.components.Add(volumeComponent2);
						}
					}
				}
				return this.m_InternalProfile;
			}
			set
			{
				this.m_InternalProfile = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000C371 File Offset: 0x0000A571
		internal VolumeProfile profileRef
		{
			get
			{
				if (!(this.m_InternalProfile == null))
				{
					return this.m_InternalProfile;
				}
				return this.sharedProfile;
			}
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000C38E File Offset: 0x0000A58E
		public bool HasInstantiatedProfile()
		{
			return this.m_InternalProfile != null;
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000C39C File Offset: 0x0000A59C
		private void OnEnable()
		{
			this.m_PreviousLayer = base.gameObject.layer;
			VolumeManager.instance.Register(this, this.m_PreviousLayer);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000C3C0 File Offset: 0x0000A5C0
		private void OnDisable()
		{
			VolumeManager.instance.Unregister(this, base.gameObject.layer);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000C3D8 File Offset: 0x0000A5D8
		private void Update()
		{
			int layer = base.gameObject.layer;
			if (layer != this.m_PreviousLayer)
			{
				VolumeManager.instance.UpdateVolumeLayer(this, this.m_PreviousLayer, layer);
				this.m_PreviousLayer = layer;
			}
			if (this.priority != this.m_PreviousPriority)
			{
				VolumeManager.instance.SetLayerDirty(layer);
				this.m_PreviousPriority = this.priority;
			}
		}

		// Token: 0x04000195 RID: 405
		[Tooltip("When enabled, HDRP applies this Volume to the entire Scene.")]
		public bool isGlobal = true;

		// Token: 0x04000196 RID: 406
		[Tooltip("Sets the Volume priority in the stack. A higher value means higher priority. You can use negative values.")]
		public float priority;

		// Token: 0x04000197 RID: 407
		[Tooltip("Sets the outer distance to start blending from. A value of 0 means no blending and Unity applies the Volume overrides immediately upon entry.")]
		public float blendDistance;

		// Token: 0x04000198 RID: 408
		[Range(0f, 1f)]
		[Tooltip("Sets the total weight of this Volume in the Scene. 0 means no effect and 1 means full effect.")]
		public float weight = 1f;

		// Token: 0x04000199 RID: 409
		public VolumeProfile sharedProfile;

		// Token: 0x0400019A RID: 410
		private int m_PreviousLayer;

		// Token: 0x0400019B RID: 411
		private float m_PreviousPriority;

		// Token: 0x0400019C RID: 412
		private VolumeProfile m_InternalProfile;
	}
}
