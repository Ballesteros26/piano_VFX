using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000127 RID: 295
	[ExecuteAlways]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.render-pipelines.high-definition@8.0/manual/Custom-Pass.html")]
	public class CustomPassVolume : MonoBehaviour
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x00048DBE File Offset: 0x00046FBE
		// (set) Token: 0x060008ED RID: 2285 RVA: 0x00048DC6 File Offset: 0x00046FC6
		public float fadeValue { get; private set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060008EE RID: 2286 RVA: 0x00048DCF File Offset: 0x00046FCF
		private static List<CustomPassInjectionPoint> injectionPoints
		{
			get
			{
				if (CustomPassVolume.m_InjectionPoints == null)
				{
					CustomPassVolume.m_InjectionPoints = Enum.GetValues(typeof(CustomPassInjectionPoint)).Cast<CustomPassInjectionPoint>().ToList<CustomPassInjectionPoint>();
				}
				return CustomPassVolume.m_InjectionPoints;
			}
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00048DFB File Offset: 0x00046FFB
		private void OnEnable()
		{
			this.customPasses.RemoveAll((CustomPass c) => c == null);
			base.GetComponents<Collider>(this.m_Colliders);
			CustomPassVolume.Register(this);
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00048E3A File Offset: 0x0004703A
		private void OnDisable()
		{
			CustomPassVolume.UnRegister(this);
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00048E42 File Offset: 0x00047042
		private void OnDestroy()
		{
			this.CleanupPasses();
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00048E4C File Offset: 0x0004704C
		internal bool Execute(ScriptableRenderContext renderContext, CommandBuffer cmd, HDCamera hdCamera, CullingResults cullingResult, SharedRTManager rtManager, CustomPass.RenderTargets targets)
		{
			bool flag = false;
			if ((hdCamera.volumeLayerMask & (1 << base.gameObject.layer)) == 0)
			{
				return false;
			}
			Shader.SetGlobalFloat(HDShaderIDs._CustomPassInjectionPoint, (float)this.injectionPoint);
			foreach (CustomPass customPass in this.customPasses)
			{
				if (customPass != null && customPass.WillBeExecuted(hdCamera))
				{
					customPass.ExecuteInternal(renderContext, cmd, hdCamera, cullingResult, rtManager, targets, this);
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00048EE8 File Offset: 0x000470E8
		internal bool WillExecuteInjectionPoint(HDCamera hdCamera)
		{
			bool flag = false;
			if ((hdCamera.volumeLayerMask & (1 << base.gameObject.layer)) == 0)
			{
				return false;
			}
			foreach (CustomPass customPass in this.customPasses)
			{
				if (customPass != null && customPass.WillBeExecuted(hdCamera))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00048F60 File Offset: 0x00047160
		internal void CleanupPasses()
		{
			foreach (CustomPass customPass in this.customPasses)
			{
				customPass.CleanupPassInternal();
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00048FB0 File Offset: 0x000471B0
		private static void Register(CustomPassVolume volume)
		{
			CustomPassVolume.m_ActivePassVolumes.Add(volume);
		}

		// Token: 0x060008F6 RID: 2294 RVA: 0x00048FBE File Offset: 0x000471BE
		private static void UnRegister(CustomPassVolume volume)
		{
			CustomPassVolume.m_ActivePassVolumes.Remove(volume);
		}

		// Token: 0x060008F7 RID: 2295 RVA: 0x00048FCC File Offset: 0x000471CC
		internal static void Update(HDCamera camera)
		{
			Vector3 position = camera.volumeAnchor.position;
			CustomPassVolume.m_OverlappingPassVolumes.Clear();
			foreach (CustomPassVolume customPassVolume in CustomPassVolume.m_ActivePassVolumes)
			{
				if ((camera.volumeLayerMask & (1 << customPassVolume.gameObject.layer)) != 0)
				{
					if (customPassVolume.isGlobal)
					{
						customPassVolume.fadeValue = 1f;
						CustomPassVolume.m_OverlappingPassVolumes.Add(customPassVolume);
					}
					else if (customPassVolume.m_Colliders.Count != 0)
					{
						customPassVolume.m_OverlappingColliders.Clear();
						float num = Mathf.Max(float.Epsilon, customPassVolume.fadeRadius * customPassVolume.fadeRadius);
						float num2 = 1E+20f;
						foreach (Collider collider in customPassVolume.m_Colliders)
						{
							MeshCollider meshCollider;
							if (collider && collider.enabled && ((meshCollider = collider as MeshCollider) == null || meshCollider.convex))
							{
								float sqrMagnitude = (collider.ClosestPoint(position) - position).sqrMagnitude;
								num2 = Mathf.Min(num2, sqrMagnitude);
								if (sqrMagnitude <= num)
								{
									customPassVolume.m_OverlappingColliders.Add(collider);
								}
							}
						}
						customPassVolume.fadeValue = 1f - Mathf.Clamp01(Mathf.Sqrt(num2 / num));
						if (customPassVolume.m_OverlappingColliders.Count > 0)
						{
							CustomPassVolume.m_OverlappingPassVolumes.Add(customPassVolume);
						}
					}
				}
			}
			CustomPassVolume.m_OverlappingPassVolumes.Sort(delegate(CustomPassVolume v1, CustomPassVolume v2)
			{
				if (v1.isGlobal && v2.isGlobal)
				{
					return 0;
				}
				if (v1.isGlobal)
				{
					return 1;
				}
				if (v2.isGlobal)
				{
					return -1;
				}
				return CustomPassVolume.<Update>g__GetVolumeExtent|23_1(v1).CompareTo(CustomPassVolume.<Update>g__GetVolumeExtent|23_1(v2));
			});
		}

		// Token: 0x060008F8 RID: 2296 RVA: 0x000491BC File Offset: 0x000473BC
		internal void AggregateCullingParameters(ref ScriptableCullingParameters cullingParameters, HDCamera hdCamera)
		{
			foreach (CustomPass customPass in this.customPasses)
			{
				if (customPass != null && customPass.enabled)
				{
					customPass.InternalAggregateCullingParameters(ref cullingParameters, hdCamera);
				}
			}
		}

		// Token: 0x060008F9 RID: 2297 RVA: 0x0004921C File Offset: 0x0004741C
		internal static CullingResults? Cull(ScriptableRenderContext renderContext, HDCamera hdCamera)
		{
			CullingResults? cullingResults = null;
			CustomPassVolume.Update(hdCamera);
			ScriptableCullingParameters scriptableCullingParameters;
			hdCamera.camera.TryGetCullingParameters(out scriptableCullingParameters);
			scriptableCullingParameters.cullingMask = 0U;
			scriptableCullingParameters.cullingOptions &= CullingOptions.Stereo;
			foreach (CustomPassInjectionPoint customPassInjectionPoint in CustomPassVolume.injectionPoints)
			{
				CustomPassVolume activePassVolume = CustomPassVolume.GetActivePassVolume(customPassInjectionPoint);
				if (activePassVolume != null)
				{
					activePassVolume.AggregateCullingParameters(ref scriptableCullingParameters, hdCamera);
				}
			}
			if (scriptableCullingParameters.cullingMask != 0U && ((ulong)scriptableCullingParameters.cullingMask & (ulong)((long)hdCamera.camera.cullingMask)) != (ulong)scriptableCullingParameters.cullingMask)
			{
				cullingResults = new CullingResults?(renderContext.Cull(ref scriptableCullingParameters));
			}
			return cullingResults;
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x000492E8 File Offset: 0x000474E8
		internal static void Cleanup()
		{
			foreach (CustomPassVolume customPassVolume in CustomPassVolume.m_ActivePassVolumes)
			{
				customPassVolume.CleanupPasses();
			}
		}

		// Token: 0x060008FB RID: 2299 RVA: 0x00049338 File Offset: 0x00047538
		public static CustomPassVolume GetActivePassVolume(CustomPassInjectionPoint injectionPoint)
		{
			foreach (CustomPassVolume customPassVolume in CustomPassVolume.m_OverlappingPassVolumes)
			{
				if (customPassVolume.injectionPoint == injectionPoint)
				{
					return customPassVolume;
				}
			}
			return null;
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x00049394 File Offset: 0x00047594
		public void AddPassOfType(Type passType)
		{
			if (!typeof(CustomPass).IsAssignableFrom(passType))
			{
				Debug.LogError(string.Format("Can't add pass type {0} to the list because it does not inherit from CustomPass.", passType));
				return;
			}
			this.customPasses.Add(Activator.CreateInstance(passType) as CustomPass);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x0004941C File Offset: 0x0004761C
		[CompilerGenerated]
		internal static float <Update>g__GetVolumeExtent|23_1(CustomPassVolume volume)
		{
			float num = 0f;
			foreach (Collider collider in volume.m_OverlappingColliders)
			{
				num += collider.bounds.extents.magnitude;
			}
			return num;
		}

		// Token: 0x04000DB5 RID: 3509
		public bool isGlobal = true;

		// Token: 0x04000DB6 RID: 3510
		[Min(0f)]
		public float fadeRadius;

		// Token: 0x04000DB7 RID: 3511
		[SerializeReference]
		public List<CustomPass> customPasses = new List<CustomPass>();

		// Token: 0x04000DB8 RID: 3512
		public CustomPassInjectionPoint injectionPoint = CustomPassInjectionPoint.BeforeTransparent;

		// Token: 0x04000DBA RID: 3514
		private static HashSet<CustomPassVolume> m_ActivePassVolumes = new HashSet<CustomPassVolume>();

		// Token: 0x04000DBB RID: 3515
		private static List<CustomPassVolume> m_OverlappingPassVolumes = new List<CustomPassVolume>();

		// Token: 0x04000DBC RID: 3516
		private List<Collider> m_Colliders = new List<Collider>();

		// Token: 0x04000DBD RID: 3517
		private List<Collider> m_OverlappingColliders = new List<Collider>();

		// Token: 0x04000DBE RID: 3518
		private static List<CustomPassInjectionPoint> m_InjectionPoints;
	}
}
