using System;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000104 RID: 260
	internal class HDRenderPipelineRayTracingResources : ScriptableObject
	{
		// Token: 0x040009BD RID: 2493
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/Reflections/RaytracingReflections.raytrace", ReloadAttribute.Package.Root)]
		public RayTracingShader reflectionRaytracingRT;

		// Token: 0x040009BE RID: 2494
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/Reflections/RaytracingReflections.compute", ReloadAttribute.Package.Root)]
		public ComputeShader reflectionRaytracingCS;

		// Token: 0x040009BF RID: 2495
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/RaytracingReflectionFilter.compute", ReloadAttribute.Package.Root)]
		public ComputeShader reflectionBilateralFilterCS;

		// Token: 0x040009C0 RID: 2496
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/Shadows/RaytracingShadow.raytrace", ReloadAttribute.Package.Root)]
		public RayTracingShader shadowRaytracingRT;

		// Token: 0x040009C1 RID: 2497
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/Shadows/RayTracingContactShadow.raytrace", ReloadAttribute.Package.Root)]
		public RayTracingShader contactShadowRayTracingRT;

		// Token: 0x040009C2 RID: 2498
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/Shadows/RaytracingShadow.compute", ReloadAttribute.Package.Root)]
		public ComputeShader shadowRaytracingCS;

		// Token: 0x040009C3 RID: 2499
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/Shadows/RaytracingShadowFilter.compute", ReloadAttribute.Package.Root)]
		public ComputeShader shadowFilterCS;

		// Token: 0x040009C4 RID: 2500
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/RaytracingRenderer.raytrace", ReloadAttribute.Package.Root)]
		public RayTracingShader forwardRaytracing;

		// Token: 0x040009C5 RID: 2501
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/RaytracingFlagMask.shader", ReloadAttribute.Package.Root)]
		public Shader raytracingFlagMask;

		// Token: 0x040009C6 RID: 2502
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/RaytracingLightCluster.compute", ReloadAttribute.Package.Root)]
		public ComputeShader lightClusterBuildCS;

		// Token: 0x040009C7 RID: 2503
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/DebugLightCluster.shader", ReloadAttribute.Package.Root)]
		public Shader lightClusterDebugS;

		// Token: 0x040009C8 RID: 2504
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/DebugLightCluster.compute", ReloadAttribute.Package.Root)]
		public ComputeShader lightClusterDebugCS;

		// Token: 0x040009C9 RID: 2505
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/IndirectDiffuse/RaytracingIndirectDiffuse.raytrace", ReloadAttribute.Package.Root)]
		public RayTracingShader indirectDiffuseRaytracingRT;

		// Token: 0x040009CA RID: 2506
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/IndirectDiffuse/RaytracingIndirectDiffuse.compute", ReloadAttribute.Package.Root)]
		public ComputeShader indirectDiffuseRaytracingCS;

		// Token: 0x040009CB RID: 2507
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/RaytracingAmbientOcclusion.raytrace", ReloadAttribute.Package.Root)]
		public RayTracingShader aoRaytracing;

		// Token: 0x040009CC RID: 2508
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/RayTracingSubSurface.raytrace", ReloadAttribute.Package.Root)]
		public RayTracingShader subSurfaceRayTracing;

		// Token: 0x040009CD RID: 2509
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/Denoising/TemporalFilter.compute", ReloadAttribute.Package.Root)]
		public ComputeShader temporalFilterCS;

		// Token: 0x040009CE RID: 2510
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/Denoising/SimpleDenoiser.compute", ReloadAttribute.Package.Root)]
		public ComputeShader simpleDenoiserCS;

		// Token: 0x040009CF RID: 2511
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/Denoising/DiffuseDenoiser.compute", ReloadAttribute.Package.Root)]
		public ComputeShader diffuseDenoiserCS;

		// Token: 0x040009D0 RID: 2512
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/Denoising/ReflectionDenoiser.compute", ReloadAttribute.Package.Root)]
		public ComputeShader reflectionDenoiserCS;

		// Token: 0x040009D1 RID: 2513
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/Deferred/RaytracingGBuffer.raytrace", ReloadAttribute.Package.Root)]
		public RayTracingShader gBufferRaytracingRT;

		// Token: 0x040009D2 RID: 2514
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/Deferred/RaytracingDeferred.compute", ReloadAttribute.Package.Root)]
		public ComputeShader deferredRaytracingCS;

		// Token: 0x040009D3 RID: 2515
		[Reload("Runtime/RenderPipeline/PathTracing/Shaders/PathTracingMain.raytrace", ReloadAttribute.Package.Root)]
		public RayTracingShader pathTracing;

		// Token: 0x040009D4 RID: 2516
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/Common/RayBinning.compute", ReloadAttribute.Package.Root)]
		public ComputeShader rayBinningCS;

		// Token: 0x040009D5 RID: 2517
		[Reload("Runtime/RenderPipeline/Raytracing/Shaders/CountTracedRays.compute", ReloadAttribute.Package.Root)]
		public ComputeShader countTracedRays;

		// Token: 0x040009D6 RID: 2518
		[Reload("Runtime/RenderPipelineResources/Texture/ReflectionKernelMapping.png", ReloadAttribute.Package.Root)]
		public Texture2D reflectionFilterMapping;
	}
}
