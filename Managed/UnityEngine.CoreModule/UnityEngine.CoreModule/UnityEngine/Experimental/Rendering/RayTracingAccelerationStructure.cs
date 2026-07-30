using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Experimental.Rendering
{
	// Token: 0x020003DB RID: 987
	[NativeHeader("Runtime/Export/Graphics/RayTracingAccelerationStructure.bindings.h")]
	[NativeHeader("Runtime/Shaders/RayTracingAccelerationStructure.h")]
	[UsedByNativeCode]
	public sealed class RayTracingAccelerationStructure : IDisposable
	{
		// Token: 0x06002220 RID: 8736 RVA: 0x00039878 File Offset: 0x00037A78
		~RayTracingAccelerationStructure()
		{
			this.Dispose(false);
		}

		// Token: 0x06002221 RID: 8737 RVA: 0x000398AC File Offset: 0x00037AAC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002222 RID: 8738 RVA: 0x000398C0 File Offset: 0x00037AC0
		private void Dispose(bool disposing)
		{
			if (disposing)
			{
				RayTracingAccelerationStructure.Destroy(this);
			}
			this.m_Ptr = IntPtr.Zero;
		}

		// Token: 0x06002223 RID: 8739 RVA: 0x000398E7 File Offset: 0x00037AE7
		public RayTracingAccelerationStructure(RayTracingAccelerationStructure.RASSettings settings)
		{
			this.m_Ptr = RayTracingAccelerationStructure.Create(settings);
		}

		// Token: 0x06002224 RID: 8740 RVA: 0x00039900 File Offset: 0x00037B00
		public RayTracingAccelerationStructure()
		{
			this.m_Ptr = RayTracingAccelerationStructure.Create(new RayTracingAccelerationStructure.RASSettings
			{
				rayTracingModeMask = RayTracingAccelerationStructure.RayTracingModeMask.Everything,
				managementMode = RayTracingAccelerationStructure.ManagementMode.Manual,
				layerMask = -1
			});
		}

		// Token: 0x06002225 RID: 8741 RVA: 0x00039942 File Offset: 0x00037B42
		[FreeFunction("RayTracingAccelerationStructure_Bindings::Create")]
		private static IntPtr Create(RayTracingAccelerationStructure.RASSettings desc)
		{
			return RayTracingAccelerationStructure.Create_Injected(ref desc);
		}

		// Token: 0x06002226 RID: 8742
		[FreeFunction("RayTracingAccelerationStructure_Bindings::Destroy")]
		[MethodImpl(4096)]
		private static extern void Destroy(RayTracingAccelerationStructure accelStruct);

		// Token: 0x06002227 RID: 8743 RVA: 0x0003994B File Offset: 0x00037B4B
		public void Release()
		{
			this.Dispose();
		}

		// Token: 0x06002228 RID: 8744
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::Build", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void Build();

		// Token: 0x06002229 RID: 8745
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::Update", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void Update();

		// Token: 0x0600222A RID: 8746
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::AddInstance", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void AddInstance([NotNull] Renderer targetRenderer, bool[] subMeshMask = null, bool[] subMeshTransparencyFlags = null, bool enableTriangleCulling = true, bool frontTriangleCounterClockwise = false, uint mask = 255U);

		// Token: 0x0600222B RID: 8747
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::UpdateInstanceTransform", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void UpdateInstanceTransform([NotNull] Renderer renderer);

		// Token: 0x0600222C RID: 8748
		[FreeFunction(Name = "RayTracingAccelerationStructure_Bindings::GetSize", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern ulong GetSize();

		// Token: 0x0600222D RID: 8749
		[MethodImpl(4096)]
		private static extern IntPtr Create_Injected(ref RayTracingAccelerationStructure.RASSettings desc);

		// Token: 0x04000CF0 RID: 3312
		internal IntPtr m_Ptr;

		// Token: 0x020003DC RID: 988
		[Flags]
		public enum RayTracingModeMask
		{
			// Token: 0x04000CF2 RID: 3314
			Nothing = 0,
			// Token: 0x04000CF3 RID: 3315
			Static = 2,
			// Token: 0x04000CF4 RID: 3316
			DynamicTransform = 4,
			// Token: 0x04000CF5 RID: 3317
			DynamicGeometry = 8,
			// Token: 0x04000CF6 RID: 3318
			Everything = 14
		}

		// Token: 0x020003DD RID: 989
		public enum ManagementMode
		{
			// Token: 0x04000CF8 RID: 3320
			Manual,
			// Token: 0x04000CF9 RID: 3321
			Automatic
		}

		// Token: 0x020003DE RID: 990
		public struct RASSettings
		{
			// Token: 0x0600222E RID: 8750 RVA: 0x00039955 File Offset: 0x00037B55
			public RASSettings(RayTracingAccelerationStructure.ManagementMode sceneManagementMode, RayTracingAccelerationStructure.RayTracingModeMask rayTracingModeMask, int layerMask)
			{
				this.managementMode = sceneManagementMode;
				this.rayTracingModeMask = rayTracingModeMask;
				this.layerMask = layerMask;
			}

			// Token: 0x04000CFA RID: 3322
			public RayTracingAccelerationStructure.ManagementMode managementMode;

			// Token: 0x04000CFB RID: 3323
			public RayTracingAccelerationStructure.RayTracingModeMask rayTracingModeMask;

			// Token: 0x04000CFC RID: 3324
			public int layerMask;
		}
	}
}
