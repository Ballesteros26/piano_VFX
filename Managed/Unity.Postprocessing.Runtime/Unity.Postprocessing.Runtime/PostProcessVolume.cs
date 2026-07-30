using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000059 RID: 89
	[ExecuteAlways]
	[AddComponentMenu("Rendering/Post-process Volume", 1001)]
	public sealed class PostProcessVolume : MonoBehaviour
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0000D424 File Offset: 0x0000B624
		// (set) Token: 0x06000199 RID: 409 RVA: 0x0000D4B8 File Offset: 0x0000B6B8
		public PostProcessProfile profile
		{
			get
			{
				if (this.m_InternalProfile == null)
				{
					this.m_InternalProfile = ScriptableObject.CreateInstance<PostProcessProfile>();
					if (this.sharedProfile != null)
					{
						foreach (PostProcessEffectSettings postProcessEffectSettings in this.sharedProfile.settings)
						{
							PostProcessEffectSettings postProcessEffectSettings2 = Object.Instantiate<PostProcessEffectSettings>(postProcessEffectSettings);
							this.m_InternalProfile.settings.Add(postProcessEffectSettings2);
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

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000D4C1 File Offset: 0x0000B6C1
		internal PostProcessProfile profileRef
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

		// Token: 0x0600019B RID: 411 RVA: 0x0000D4DE File Offset: 0x0000B6DE
		public bool HasInstantiatedProfile()
		{
			return this.m_InternalProfile != null;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000D4EC File Offset: 0x0000B6EC
		private void OnEnable()
		{
			PostProcessManager.instance.Register(this);
			this.m_PreviousLayer = base.gameObject.layer;
			this.m_TempColliders = new List<Collider>();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000D515 File Offset: 0x0000B715
		private void OnDisable()
		{
			PostProcessManager.instance.Unregister(this);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000D524 File Offset: 0x0000B724
		private void Update()
		{
			int layer = base.gameObject.layer;
			if (layer != this.m_PreviousLayer)
			{
				PostProcessManager.instance.UpdateVolumeLayer(this, this.m_PreviousLayer, layer);
				this.m_PreviousLayer = layer;
			}
			if (this.priority != this.m_PreviousPriority)
			{
				PostProcessManager.instance.SetLayerDirty(layer);
				this.m_PreviousPriority = this.priority;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000D584 File Offset: 0x0000B784
		private void OnDrawGizmos()
		{
			List<Collider> tempColliders = this.m_TempColliders;
			base.GetComponents<Collider>(tempColliders);
			if (this.isGlobal || tempColliders == null)
			{
				return;
			}
			Vector3 lossyScale = base.transform.lossyScale;
			Vector3 vector = new Vector3(1f / lossyScale.x, 1f / lossyScale.y, 1f / lossyScale.z);
			Gizmos.matrix = Matrix4x4.TRS(base.transform.position, base.transform.rotation, lossyScale);
			foreach (Collider collider in tempColliders)
			{
				if (collider.enabled)
				{
					Type type = collider.GetType();
					if (type == typeof(BoxCollider))
					{
						BoxCollider boxCollider = (BoxCollider)collider;
						Gizmos.DrawCube(boxCollider.center, boxCollider.size);
						Gizmos.DrawWireCube(boxCollider.center, boxCollider.size + vector * this.blendDistance * 4f);
					}
					else if (type == typeof(SphereCollider))
					{
						SphereCollider sphereCollider = (SphereCollider)collider;
						Gizmos.DrawSphere(sphereCollider.center, sphereCollider.radius);
						Gizmos.DrawWireSphere(sphereCollider.center, sphereCollider.radius + vector.x * this.blendDistance * 2f);
					}
					else if (type == typeof(MeshCollider))
					{
						MeshCollider meshCollider = (MeshCollider)collider;
						if (!meshCollider.convex)
						{
							meshCollider.convex = true;
						}
						Gizmos.DrawMesh(meshCollider.sharedMesh);
						Gizmos.DrawWireMesh(meshCollider.sharedMesh, Vector3.zero, Quaternion.identity, Vector3.one + vector * this.blendDistance * 4f);
					}
				}
			}
			tempColliders.Clear();
		}

		// Token: 0x04000182 RID: 386
		public PostProcessProfile sharedProfile;

		// Token: 0x04000183 RID: 387
		[Tooltip("Check this box to mark this volume as global. This volume's Profile will be applied to the whole Scene.")]
		public bool isGlobal;

		// Token: 0x04000184 RID: 388
		[Min(0f)]
		[Tooltip("The distance (from the attached Collider) to start blending from. A value of 0 means there will be no blending and the Volume overrides will be applied immediatly upon entry to the attached Collider.")]
		public float blendDistance;

		// Token: 0x04000185 RID: 389
		[Range(0f, 1f)]
		[Tooltip("The total weight of this Volume in the Scene. A value of 0 signifies that it will have no effect, 1 signifies full effect.")]
		public float weight = 1f;

		// Token: 0x04000186 RID: 390
		[Tooltip("The volume priority in the stack. A higher value means higher priority. Negative values are supported.")]
		public float priority;

		// Token: 0x04000187 RID: 391
		private int m_PreviousLayer;

		// Token: 0x04000188 RID: 392
		private float m_PreviousPriority;

		// Token: 0x04000189 RID: 393
		private List<Collider> m_TempColliders;

		// Token: 0x0400018A RID: 394
		private PostProcessProfile m_InternalProfile;
	}
}
