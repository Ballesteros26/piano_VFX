using System;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x02000100 RID: 256
	internal enum HDProfileId
	{
		// Token: 0x040008F9 RID: 2297
		PushGlobalParameters,
		// Token: 0x040008FA RID: 2298
		CopyDepthBuffer,
		// Token: 0x040008FB RID: 2299
		CopyDepthInTargetTexture,
		// Token: 0x040008FC RID: 2300
		CoarseStencilGeneration,
		// Token: 0x040008FD RID: 2301
		HTileForSSS,
		// Token: 0x040008FE RID: 2302
		RenderSSAO,
		// Token: 0x040008FF RID: 2303
		ResolveStencilBuffer,
		// Token: 0x04000900 RID: 2304
		HorizonSSAO,
		// Token: 0x04000901 RID: 2305
		DenoiseSSAO,
		// Token: 0x04000902 RID: 2306
		UpSampleSSAO,
		// Token: 0x04000903 RID: 2307
		ScreenSpaceShadows,
		// Token: 0x04000904 RID: 2308
		BuildLightList,
		// Token: 0x04000905 RID: 2309
		ContactShadows,
		// Token: 0x04000906 RID: 2310
		BlitToFinalRTDevBuildOnly,
		// Token: 0x04000907 RID: 2311
		Distortion,
		// Token: 0x04000908 RID: 2312
		ApplyDistortion,
		// Token: 0x04000909 RID: 2313
		DepthPrepassForward,
		// Token: 0x0400090A RID: 2314
		DepthPrepassDeferredForDecals,
		// Token: 0x0400090B RID: 2315
		DepthPrepassDeferred,
		// Token: 0x0400090C RID: 2316
		DepthPrepassDeferredIncomplete,
		// Token: 0x0400090D RID: 2317
		TransparentDepthPrepass,
		// Token: 0x0400090E RID: 2318
		GBuffer,
		// Token: 0x0400090F RID: 2319
		GBufferDebug,
		// Token: 0x04000910 RID: 2320
		DBufferRender,
		// Token: 0x04000911 RID: 2321
		DBufferPrepareDrawData,
		// Token: 0x04000912 RID: 2322
		DBufferNormal,
		// Token: 0x04000913 RID: 2323
		DisplayDebugDecalsAtlas,
		// Token: 0x04000914 RID: 2324
		DisplayDebugViewMaterial,
		// Token: 0x04000915 RID: 2325
		DebugViewMaterialGBuffer,
		// Token: 0x04000916 RID: 2326
		SubsurfaceScattering,
		// Token: 0x04000917 RID: 2327
		SsrTracing,
		// Token: 0x04000918 RID: 2328
		SsrReprojection,
		// Token: 0x04000919 RID: 2329
		ForwardEmissive,
		// Token: 0x0400091A RID: 2330
		ForwardOpaque,
		// Token: 0x0400091B RID: 2331
		ForwardOpaqueDebug,
		// Token: 0x0400091C RID: 2332
		ForwardTransparent,
		// Token: 0x0400091D RID: 2333
		ForwardTransparentDebug,
		// Token: 0x0400091E RID: 2334
		ForwardPreRefraction,
		// Token: 0x0400091F RID: 2335
		ForwardPreRefractionDebug,
		// Token: 0x04000920 RID: 2336
		ForwardTransparentDepthPrepass,
		// Token: 0x04000921 RID: 2337
		RenderForwardError,
		// Token: 0x04000922 RID: 2338
		TransparentDepthPostpass,
		// Token: 0x04000923 RID: 2339
		ObjectsMotionVector,
		// Token: 0x04000924 RID: 2340
		CameraMotionVectors,
		// Token: 0x04000925 RID: 2341
		ColorPyramid,
		// Token: 0x04000926 RID: 2342
		DepthPyramid,
		// Token: 0x04000927 RID: 2343
		PostProcessing,
		// Token: 0x04000928 RID: 2344
		AfterPostProcessing,
		// Token: 0x04000929 RID: 2345
		RenderDebug,
		// Token: 0x0400092A RID: 2346
		DisplayLightVolume,
		// Token: 0x0400092B RID: 2347
		ClearBuffers,
		// Token: 0x0400092C RID: 2348
		ClearDepthStencil,
		// Token: 0x0400092D RID: 2349
		ClearStencil,
		// Token: 0x0400092E RID: 2350
		ClearSssLightingBuffer,
		// Token: 0x0400092F RID: 2351
		ClearSSSFilteringTarget,
		// Token: 0x04000930 RID: 2352
		ClearAndCopyStencilTexture,
		// Token: 0x04000931 RID: 2353
		ClearHDRTarget,
		// Token: 0x04000932 RID: 2354
		ClearGBuffer,
		// Token: 0x04000933 RID: 2355
		ClearSsrBuffers,
		// Token: 0x04000934 RID: 2356
		HDRenderPipelineRenderCamera,
		// Token: 0x04000935 RID: 2357
		HDRenderPipelineRenderAOV,
		// Token: 0x04000936 RID: 2358
		HDRenderPipelineAllRenderRequest,
		// Token: 0x04000937 RID: 2359
		CullResultsCull,
		// Token: 0x04000938 RID: 2360
		CustomPassCullResultsCull,
		// Token: 0x04000939 RID: 2361
		UpdateStencilCopyForSSRExclusion,
		// Token: 0x0400093A RID: 2362
		GizmosPrePostprocess,
		// Token: 0x0400093B RID: 2363
		Gizmos,
		// Token: 0x0400093C RID: 2364
		DisplayCookieAtlas,
		// Token: 0x0400093D RID: 2365
		RenderWireFrame,
		// Token: 0x0400093E RID: 2366
		PushToColorPicker,
		// Token: 0x0400093F RID: 2367
		ResolveMSAAColor,
		// Token: 0x04000940 RID: 2368
		ResolveMSAADepth,
		// Token: 0x04000941 RID: 2369
		ConvolveReflectionProbe,
		// Token: 0x04000942 RID: 2370
		ConvolvePlanarReflectionProbe,
		// Token: 0x04000943 RID: 2371
		PreIntegradeWardCookTorrance,
		// Token: 0x04000944 RID: 2372
		FilterCubemapCharlie,
		// Token: 0x04000945 RID: 2373
		FilterCubemapGGX,
		// Token: 0x04000946 RID: 2374
		DisplayPointLightCookieArray,
		// Token: 0x04000947 RID: 2375
		DisplayPlanarReflectionProbeAtlas,
		// Token: 0x04000948 RID: 2376
		BlitTextureInPotAtlas,
		// Token: 0x04000949 RID: 2377
		AreaLightCookieConvolution,
		// Token: 0x0400094A RID: 2378
		UpdateSkyEnvironmentConvolution,
		// Token: 0x0400094B RID: 2379
		RenderSkyToCubemap,
		// Token: 0x0400094C RID: 2380
		UpdateSkyEnvironment,
		// Token: 0x0400094D RID: 2381
		UpdateSkyAmbientProbe,
		// Token: 0x0400094E RID: 2382
		PreRenderSky,
		// Token: 0x0400094F RID: 2383
		RenderSky,
		// Token: 0x04000950 RID: 2384
		OpaqueAtmosphericScattering,
		// Token: 0x04000951 RID: 2385
		InScatteredRadiancePrecomputation,
		// Token: 0x04000952 RID: 2386
		VolumeVoxelization,
		// Token: 0x04000953 RID: 2387
		VolumetricLighting,
		// Token: 0x04000954 RID: 2388
		VolumetricLightingFiltering,
		// Token: 0x04000955 RID: 2389
		PrepareVisibleDensityVolumeList,
		// Token: 0x04000956 RID: 2390
		RaytracingBuildCluster,
		// Token: 0x04000957 RID: 2391
		RaytracingCullLights,
		// Token: 0x04000958 RID: 2392
		RaytracingIntegrateReflection,
		// Token: 0x04000959 RID: 2393
		RaytracingFilterReflection,
		// Token: 0x0400095A RID: 2394
		RaytracingAmbientOcclusion,
		// Token: 0x0400095B RID: 2395
		RaytracingFilterAmbientOcclusion,
		// Token: 0x0400095C RID: 2396
		RaytracingDirectionalLightShadow,
		// Token: 0x0400095D RID: 2397
		RaytracingLightShadow,
		// Token: 0x0400095E RID: 2398
		RaytracingIntegrateIndirectDiffuse,
		// Token: 0x0400095F RID: 2399
		RaytracingFilterIndirectDiffuse,
		// Token: 0x04000960 RID: 2400
		RaytracingDebugOverlay,
		// Token: 0x04000961 RID: 2401
		PrepareLightsForGPU,
		// Token: 0x04000962 RID: 2402
		PushLightDataGlobalParameters,
		// Token: 0x04000963 RID: 2403
		PushShadowGlobalParameters,
		// Token: 0x04000964 RID: 2404
		RenderShadowMaps,
		// Token: 0x04000965 RID: 2405
		RenderMomentShadowMaps,
		// Token: 0x04000966 RID: 2406
		RenderPunctualShadowMaps,
		// Token: 0x04000967 RID: 2407
		RenderDirectionalShadowMaps,
		// Token: 0x04000968 RID: 2408
		RenderAreaShadowMaps,
		// Token: 0x04000969 RID: 2409
		RenderEVSMShadowMaps,
		// Token: 0x0400096A RID: 2410
		RenderEVSMShadowMapsBlur,
		// Token: 0x0400096B RID: 2411
		RenderEVSMShadowMapsCopyToAtlas,
		// Token: 0x0400096C RID: 2412
		LightLoopPushGlobalParameters,
		// Token: 0x0400096D RID: 2413
		TileClusterLightingDebug,
		// Token: 0x0400096E RID: 2414
		DisplayShadows,
		// Token: 0x0400096F RID: 2415
		RenderDeferredLightingCompute,
		// Token: 0x04000970 RID: 2416
		RenderDeferredLightingComputeAsPixel,
		// Token: 0x04000971 RID: 2417
		RenderDeferredLightingSinglePass,
		// Token: 0x04000972 RID: 2418
		RenderDeferredLightingSinglePassMRT,
		// Token: 0x04000973 RID: 2419
		VolumeUpdate,
		// Token: 0x04000974 RID: 2420
		CustomPassVolumeUpdate,
		// Token: 0x04000975 RID: 2421
		XROcclusionMesh,
		// Token: 0x04000976 RID: 2422
		XRMirrorView,
		// Token: 0x04000977 RID: 2423
		XRCustomMirrorView,
		// Token: 0x04000978 RID: 2424
		XRDepthCopy,
		// Token: 0x04000979 RID: 2425
		DownsampleDepth,
		// Token: 0x0400097A RID: 2426
		LowResTransparent,
		// Token: 0x0400097B RID: 2427
		UpsampleLowResTransparent,
		// Token: 0x0400097C RID: 2428
		AlphaCopy,
		// Token: 0x0400097D RID: 2429
		StopNaNs,
		// Token: 0x0400097E RID: 2430
		FixedExposure,
		// Token: 0x0400097F RID: 2431
		DynamicExposure,
		// Token: 0x04000980 RID: 2432
		TemporalAntialiasing,
		// Token: 0x04000981 RID: 2433
		DepthOfField,
		// Token: 0x04000982 RID: 2434
		DepthOfFieldKernel,
		// Token: 0x04000983 RID: 2435
		DepthOfFieldCoC,
		// Token: 0x04000984 RID: 2436
		DepthOfFieldPrefilter,
		// Token: 0x04000985 RID: 2437
		DepthOfFieldPyramid,
		// Token: 0x04000986 RID: 2438
		DepthOfFieldDilate,
		// Token: 0x04000987 RID: 2439
		DepthOfFieldTileMax,
		// Token: 0x04000988 RID: 2440
		DepthOfFieldGatherFar,
		// Token: 0x04000989 RID: 2441
		DepthOfFieldGatherNear,
		// Token: 0x0400098A RID: 2442
		DepthOfFieldPreCombine,
		// Token: 0x0400098B RID: 2443
		DepthOfFieldCombine,
		// Token: 0x0400098C RID: 2444
		MotionBlur,
		// Token: 0x0400098D RID: 2445
		MotionBlurMotionVecPrep,
		// Token: 0x0400098E RID: 2446
		MotionBlurTileMinMax,
		// Token: 0x0400098F RID: 2447
		MotionBlurTileNeighbourhood,
		// Token: 0x04000990 RID: 2448
		MotionBlurTileScattering,
		// Token: 0x04000991 RID: 2449
		MotionBlurKernel,
		// Token: 0x04000992 RID: 2450
		PaniniProjection,
		// Token: 0x04000993 RID: 2451
		Bloom,
		// Token: 0x04000994 RID: 2452
		ColorGradingLUTBuilder,
		// Token: 0x04000995 RID: 2453
		UberPost,
		// Token: 0x04000996 RID: 2454
		FXAA,
		// Token: 0x04000997 RID: 2455
		SMAA,
		// Token: 0x04000998 RID: 2456
		FinalPost,
		// Token: 0x04000999 RID: 2457
		CustomPostProcessBeforePP,
		// Token: 0x0400099A RID: 2458
		CustomPostProcessAfterPP,
		// Token: 0x0400099B RID: 2459
		CustomPostProcessAfterOpaqueAndSky,
		// Token: 0x0400099C RID: 2460
		ContrastAdaptiveSharpen
	}
}
