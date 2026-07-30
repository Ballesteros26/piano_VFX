using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering.HighDefinition
{
	// Token: 0x0200014C RID: 332
	public class HDUtils
	{
		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x0004C499 File Offset: 0x0004A699
		internal static HDAdditionalReflectionData s_DefaultHDAdditionalReflectionData
		{
			get
			{
				return ComponentSingleton<HDAdditionalReflectionData>.instance;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000981 RID: 2433 RVA: 0x0004C4A0 File Offset: 0x0004A6A0
		internal static HDAdditionalLightData s_DefaultHDAdditionalLightData
		{
			get
			{
				return ComponentSingleton<HDAdditionalLightData>.instance;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x0004C4A7 File Offset: 0x0004A6A7
		internal static HDAdditionalCameraData s_DefaultHDAdditionalCameraData
		{
			get
			{
				return ComponentSingleton<HDAdditionalCameraData>.instance;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000983 RID: 2435 RVA: 0x0004C4B0 File Offset: 0x0004A6B0
		public static Texture3D clearTexture3D
		{
			get
			{
				if (HDUtils.m_ClearTexture3D == null)
				{
					HDUtils.m_ClearTexture3D = new Texture3D(1, 1, 1, TextureFormat.ARGB32, false)
					{
						name = "Transparent Texture 3D"
					};
					HDUtils.m_ClearTexture3D.SetPixel(0, 0, 0, Color.clear);
					HDUtils.m_ClearTexture3D.Apply();
					RTHandles.Release(HDUtils.m_ClearTexture3DRTH);
					HDUtils.m_ClearTexture3DRTH = null;
				}
				return HDUtils.m_ClearTexture3D;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x0004C515 File Offset: 0x0004A715
		public static RTHandle clearTexture3DRTH
		{
			get
			{
				if (HDUtils.m_ClearTexture3DRTH == null || HDUtils.m_ClearTexture3D == null)
				{
					RTHandles.Release(HDUtils.m_ClearTexture3DRTH);
					HDUtils.m_ClearTexture3DRTH = RTHandles.Alloc(HDUtils.clearTexture3D);
				}
				return HDUtils.m_ClearTexture3DRTH;
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0004C54C File Offset: 0x0004A74C
		public static Material GetBlitMaterial(TextureDimension dimension, bool singleSlice = false)
		{
			HDRenderPipeline hdrenderPipeline = RenderPipelineManager.currentPipeline as HDRenderPipeline;
			if (hdrenderPipeline != null)
			{
				return hdrenderPipeline.GetBlitMaterial(dimension == TextureDimension.Tex2DArray, singleSlice);
			}
			return null;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x0004C574 File Offset: 0x0004A774
		public static RenderPipelineSettings hdrpSettings
		{
			get
			{
				return HDRenderPipeline.currentAsset.currentPlatformRenderPipelineSettings;
			}
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0004C580 File Offset: 0x0004A780
		internal static List<RenderPipelineMaterial> GetRenderPipelineMaterialList()
		{
			Type baseType = typeof(RenderPipelineMaterial);
			return (from t in baseType.Assembly.GetTypes()
				where t.IsSubclassOf(baseType)
				select t).Select(new Func<Type, object>(Activator.CreateInstance)).Cast<RenderPipelineMaterial>().ToList<RenderPipelineMaterial>();
		}

		// Token: 0x06000988 RID: 2440 RVA: 0x0004C5DF File Offset: 0x0004A7DF
		internal static void ResetOverlay()
		{
			HDUtils.s_OverlayLineHeight = -1f;
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x0004C5EC File Offset: 0x0004A7EC
		internal static float GetRuntimeDebugPanelWidth(HDCamera hdCamera)
		{
			float num = (DebugManager.instance.displayRuntimeUI ? 610f : 0f);
			return Mathf.Min((float)hdCamera.actualWidth, num);
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x0004C620 File Offset: 0x0004A820
		internal static void NextOverlayCoord(ref float x, ref float y, float overlayWidth, float overlayHeight, HDCamera hdCamera)
		{
			x += overlayWidth;
			HDUtils.s_OverlayLineHeight = Mathf.Max(overlayHeight, HDUtils.s_OverlayLineHeight);
			if (x + overlayWidth > (float)hdCamera.actualWidth)
			{
				x = 0f;
				y -= HDUtils.s_OverlayLineHeight;
				HDUtils.s_OverlayLineHeight = -1f;
			}
			if (x == 0f)
			{
				x += HDUtils.GetRuntimeDebugPanelWidth(hdCamera);
			}
		}

		// Token: 0x0600098B RID: 2443 RVA: 0x0004C681 File Offset: 0x0004A881
		internal static float ProjectionMatrixAspect(in Matrix4x4 matrix)
		{
			return -matrix.m11 / matrix.m00;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0004C694 File Offset: 0x0004A894
		internal static Matrix4x4 ComputePixelCoordToWorldSpaceViewDirectionMatrix(float verticalFoV, Vector2 lensShift, Vector4 screenSize, Matrix4x4 worldToViewMatrix, bool renderToCubemap, float aspectRatio = -1f)
		{
			aspectRatio = ((aspectRatio < 0f) ? (screenSize.x * screenSize.w) : aspectRatio);
			float num = Mathf.Tan(0.5f * verticalFoV);
			float num2 = (1f - 2f * lensShift.y) * num;
			float num3 = -2f * screenSize.w * num;
			float num4 = (1f - 2f * lensShift.x) * num * aspectRatio;
			float num5 = -2f * screenSize.z * num * aspectRatio;
			if (renderToCubemap)
			{
				num3 = -num3;
				num2 = -num2;
			}
			Matrix4x4 matrix4x = new Matrix4x4(new Vector4(num5, 0f, 0f, 0f), new Vector4(0f, num3, 0f, 0f), new Vector4(num4, num2, -1f, 0f), new Vector4(0f, 0f, 0f, 1f));
			Vector4 vector = new Vector4(0f, 0f, 0f, 1f);
			worldToViewMatrix.SetColumn(3, vector);
			worldToViewMatrix.SetRow(2, -worldToViewMatrix.GetRow(2));
			return Matrix4x4.Transpose(worldToViewMatrix.transpose * matrix4x);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0004C7CC File Offset: 0x0004A9CC
		internal static float ComputZPlaneTexelSpacing(float planeDepth, float verticalFoV, float resolutionY)
		{
			return Mathf.Tan(0.5f * verticalFoV) * (2f / resolutionY) * planeDepth;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0004C7E4 File Offset: 0x0004A9E4
		public static void BlitQuad(CommandBuffer cmd, Texture source, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear)
		{
			HDUtils.s_PropertyBlock.SetTexture(HDShaderIDs._BlitTexture, source);
			HDUtils.s_PropertyBlock.SetVector(HDShaderIDs._BlitScaleBias, scaleBiasTex);
			HDUtils.s_PropertyBlock.SetVector(HDShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			HDUtils.s_PropertyBlock.SetFloat(HDShaderIDs._BlitMipLevel, (float)mipLevelTex);
			cmd.DrawProcedural(Matrix4x4.identity, HDUtils.GetBlitMaterial(source.dimension, false), bilinear ? 3 : 2, MeshTopology.Quads, 4, 1, HDUtils.s_PropertyBlock);
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0004C85C File Offset: 0x0004AA5C
		public static void BlitQuadWithPadding(CommandBuffer cmd, Texture source, Vector2 textureSize, Vector4 scaleBiasTex, Vector4 scaleBiasRT, int mipLevelTex, bool bilinear, int paddingInPixels)
		{
			HDUtils.s_PropertyBlock.SetTexture(HDShaderIDs._BlitTexture, source);
			HDUtils.s_PropertyBlock.SetVector(HDShaderIDs._BlitScaleBias, scaleBiasTex);
			HDUtils.s_PropertyBlock.SetVector(HDShaderIDs._BlitScaleBiasRt, scaleBiasRT);
			HDUtils.s_PropertyBlock.SetFloat(HDShaderIDs._BlitMipLevel, (float)mipLevelTex);
			HDUtils.s_PropertyBlock.SetVector(HDShaderIDs._BlitTextureSize, textureSize);
			HDUtils.s_PropertyBlock.SetInt(HDShaderIDs._BlitPaddingSize, paddingInPixels);
			if (source.wrapMode == TextureWrapMode.Repeat)
			{
				cmd.DrawProcedural(Matrix4x4.identity, HDUtils.GetBlitMaterial(source.dimension, false), bilinear ? 7 : 6, MeshTopology.Quads, 4, 1, HDUtils.s_PropertyBlock);
				return;
			}
			cmd.DrawProcedural(Matrix4x4.identity, HDUtils.GetBlitMaterial(source.dimension, false), bilinear ? 5 : 4, MeshTopology.Quads, 4, 1, HDUtils.s_PropertyBlock);
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0004C92C File Offset: 0x0004AB2C
		public static void BlitTexture(CommandBuffer cmd, RTHandle source, Vector4 scaleBias, float mipLevel, bool bilinear)
		{
			HDUtils.s_PropertyBlock.SetTexture(HDShaderIDs._BlitTexture, source);
			HDUtils.s_PropertyBlock.SetVector(HDShaderIDs._BlitScaleBias, scaleBias);
			HDUtils.s_PropertyBlock.SetFloat(HDShaderIDs._BlitMipLevel, mipLevel);
			cmd.DrawProcedural(Matrix4x4.identity, HDUtils.GetBlitMaterial(TextureXR.dimension, false), bilinear ? 1 : 0, MeshTopology.Triangles, 3, 1, HDUtils.s_PropertyBlock);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0004C994 File Offset: 0x0004AB94
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, float mipLevel = 0f, bool bilinear = false)
		{
			Vector2 vector = new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			HDUtils.BlitTexture(cmd, source, vector, mipLevel, bilinear);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0004C9E3 File Offset: 0x0004ABE3
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, Vector4 scaleBias, float mipLevel = 0f, bool bilinear = false)
		{
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			HDUtils.BlitTexture(cmd, source, scaleBias, mipLevel, bilinear);
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0004C9FC File Offset: 0x0004ABFC
		public static void BlitCameraTexture(CommandBuffer cmd, RTHandle source, RTHandle destination, Rect destViewport, float mipLevel = 0f, bool bilinear = false)
		{
			Vector2 vector = new Vector2(source.rtHandleProperties.rtHandleScale.x, source.rtHandleProperties.rtHandleScale.y);
			CoreUtils.SetRenderTarget(cmd, destination, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			cmd.SetViewport(destViewport);
			HDUtils.BlitTexture(cmd, source, vector, mipLevel, bilinear);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0004CA53 File Offset: 0x0004AC53
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RTHandle colorBuffer, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			CoreUtils.SetRenderTarget(commandBuffer, colorBuffer, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			commandBuffer.SetGlobalVector(HDShaderIDs._RTHandleScale, colorBuffer.rtHandleProperties.rtHandleScale);
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x06000995 RID: 2453 RVA: 0x0004CA88 File Offset: 0x0004AC88
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RTHandle colorBuffer, RTHandle depthStencilBuffer, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			CoreUtils.SetRenderTarget(commandBuffer, colorBuffer, depthStencilBuffer, 0, CubemapFace.Unknown, -1);
			commandBuffer.SetGlobalVector(HDShaderIDs._RTHandleScale, colorBuffer.rtHandleProperties.rtHandleScale);
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x0004CABE File Offset: 0x0004ACBE
		public static void DrawFullScreen(CommandBuffer commandBuffer, Material material, RenderTargetIdentifier[] colorBuffers, RTHandle depthStencilBuffer, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			CoreUtils.SetRenderTarget(commandBuffer, colorBuffers, depthStencilBuffer);
			commandBuffer.SetGlobalVector(HDShaderIDs._RTHandleScale, depthStencilBuffer.rtHandleProperties.rtHandleScale);
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0004CAF1 File Offset: 0x0004ACF1
		public static void DrawFullScreen(CommandBuffer commandBuffer, Rect viewport, Material material, RenderTargetIdentifier destination, MaterialPropertyBlock properties = null, int shaderPassId = 0, int depthSlice = -1)
		{
			CoreUtils.SetRenderTarget(commandBuffer, destination, ClearFlag.None, 0, CubemapFace.Unknown, depthSlice);
			commandBuffer.SetViewport(viewport);
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0004CB19 File Offset: 0x0004AD19
		public static void DrawFullScreen(CommandBuffer commandBuffer, Rect viewport, Material material, RenderTargetIdentifier destination, RTHandle depthStencilBuffer, MaterialPropertyBlock properties = null, int shaderPassId = 0)
		{
			CoreUtils.SetRenderTarget(commandBuffer, destination, depthStencilBuffer, ClearFlag.None, 0, CubemapFace.Unknown, -1);
			commandBuffer.SetViewport(viewport);
			commandBuffer.DrawProcedural(Matrix4x4.identity, material, shaderPassId, MeshTopology.Triangles, 3, 1, properties);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0004CB48 File Offset: 0x0004AD48
		internal static Vector4 GetMouseCoordinates(HDCamera camera)
		{
			Vector2 mousePosition = MousePositionDebug.instance.GetMousePosition(camera.screenSize.y, camera.camera.cameraType == CameraType.SceneView);
			return new Vector4(mousePosition.x, mousePosition.y, RTHandles.rtHandleProperties.rtHandleScale.x * mousePosition.x / camera.screenSize.x, RTHandles.rtHandleProperties.rtHandleScale.y * mousePosition.y / camera.screenSize.y);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0004CBD0 File Offset: 0x0004ADD0
		internal static Vector4 GetMouseClickCoordinates(HDCamera camera)
		{
			Vector2 mouseClickPosition = MousePositionDebug.instance.GetMouseClickPosition(camera.screenSize.y);
			return new Vector4(mouseClickPosition.x, mouseClickPosition.y, RTHandles.rtHandleProperties.rtHandleScale.x * mouseClickPosition.x / camera.screenSize.x, RTHandles.rtHandleProperties.rtHandleScale.y * mouseClickPosition.y / camera.screenSize.y);
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0004CC48 File Offset: 0x0004AE48
		internal static bool IsRegularPreviewCamera(Camera camera)
		{
			if (camera.cameraType == CameraType.Preview)
			{
				HDAdditionalCameraData hdadditionalCameraData;
				camera.TryGetComponent<HDAdditionalCameraData>(out hdadditionalCameraData);
				return hdadditionalCameraData == null || !hdadditionalCameraData.isEditorCameraPreview;
			}
			return false;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0004CC7D File Offset: 0x0004AE7D
		internal static string GetHDRenderPipelinePath()
		{
			return "Packages/com.unity.render-pipelines.high-definition/";
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0004CC84 File Offset: 0x0004AE84
		internal static string GetCorePath()
		{
			return "Packages/com.unity.render-pipelines.core/";
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0004CC8B File Offset: 0x0004AE8B
		internal static int DivRoundUp(int x, int y)
		{
			return (x + y - 1) / y;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0004CC94 File Offset: 0x0004AE94
		internal static bool IsQuaternionValid(Quaternion q)
		{
			return q[0] * q[0] + q[1] * q[1] + q[2] * q[2] + q[3] * q[3] > float.Epsilon;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0004CCEF File Offset: 0x0004AEEF
		internal static bool IsSupportedGraphicDevice(GraphicsDeviceType graphicDevice)
		{
			return graphicDevice == GraphicsDeviceType.Direct3D11 || graphicDevice == GraphicsDeviceType.Direct3D12 || graphicDevice == GraphicsDeviceType.PlayStation4 || graphicDevice == GraphicsDeviceType.XboxOne || graphicDevice == GraphicsDeviceType.XboxOneD3D12 || graphicDevice == GraphicsDeviceType.Metal || graphicDevice == GraphicsDeviceType.Vulkan;
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0004CD15 File Offset: 0x0004AF15
		internal static void CheckRTCreated(RenderTexture rt)
		{
			if (!rt.IsCreated())
			{
				rt.Create();
			}
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x0004CD28 File Offset: 0x0004AF28
		internal static Vector4 ComputeUvScaleAndLimit(Vector2Int viewportResolution, Vector2Int bufferSize)
		{
			Vector2 vector = new Vector2(1f / (float)bufferSize.x, 1f / (float)bufferSize.y);
			Vector2 vector2 = new Vector2((float)viewportResolution.x * vector.x, (float)viewportResolution.y * vector.y);
			Vector2 vector3 = new Vector2(((float)viewportResolution.x - 0.5f) * vector.x, ((float)viewportResolution.y - 0.5f) * vector.y);
			return new Vector4(vector2.x, vector2.y, vector3.x, vector3.y);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x0004CDCC File Offset: 0x0004AFCC
		internal static bool IsOperatingSystemSupported(string os)
		{
			if (SystemInfo.graphicsDeviceType == GraphicsDeviceType.Metal && os.StartsWith("Mac"))
			{
				int num = os.LastIndexOf(" ");
				string[] array = os.Substring(num + 1).Split(new char[] { '.' });
				int num2 = Convert.ToInt32(array[0]);
				int num3 = Convert.ToInt32(array[1]);
				if (num2 < 10 || num3 < 13)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x0004CE34 File Offset: 0x0004B034
		internal static void GetScaleAndBiasForLinearDistanceFade(float fadeDistance, out float scale, out float bias)
		{
			float num = 0.9f * fadeDistance;
			scale = 1f / (fadeDistance - num);
			bias = -num / (fadeDistance - num);
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x0004CE5C File Offset: 0x0004B05C
		internal static float ComputeLinearDistanceFade(float distanceToCamera, float fadeDistance)
		{
			float num;
			float num2;
			HDUtils.GetScaleAndBiasForLinearDistanceFade(fadeDistance, out num, out num2);
			return 1f - Mathf.Clamp01(distanceToCamera * num + num2);
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0004CE83 File Offset: 0x0004B083
		internal static float ComputeWeightedLinearFadeDistance(Vector3 position1, Vector3 position2, float weight, float fadeDistance)
		{
			return HDUtils.ComputeLinearDistanceFade(Vector3.Magnitude(position1 - position2), fadeDistance) * weight;
		}

		// Token: 0x060009A7 RID: 2471 RVA: 0x0004CE99 File Offset: 0x0004B099
		internal static bool PostProcessIsFinalPass()
		{
			return !Debug.isDebugBuild;
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0004CEA4 File Offset: 0x0004B0A4
		internal unsafe static Vector4 ConvertGUIDToVector4(string guid)
		{
			byte[] array = new byte[16];
			for (int i = 0; i < 16; i++)
			{
				array[i] = byte.Parse(guid.Substring(i * 2, 2), NumberStyles.HexNumber);
			}
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			Vector4 vector = *(Vector4*)ptr;
			array2 = null;
			return vector;
		}

		// Token: 0x060009A9 RID: 2473 RVA: 0x0004CF00 File Offset: 0x0004B100
		internal unsafe static string ConvertVector4ToGUID(Vector4 vector)
		{
			StringBuilder stringBuilder = new StringBuilder();
			byte* ptr = (byte*)(&vector);
			for (int i = 0; i < 16; i++)
			{
				stringBuilder.Append(ptr[i].ToString("x2"));
			}
			byte[] array = new byte[16];
			Marshal.Copy((IntPtr)((void*)ptr), array, 0, 16);
			return stringBuilder.ToString();
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x0004CF58 File Offset: 0x0004B158
		public static Color NormalizeColor(Color color)
		{
			Vector4 vector = Vector4.Max(color, Vector4.one * 0.0001f);
			Color color2 = vector;
			color = vector / ColorUtils.Luminance(in color2);
			color.a = 1f;
			return color;
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0004CFA8 File Offset: 0x0004B1A8
		public static void DrawRendererList(ScriptableRenderContext renderContext, CommandBuffer cmd, RendererList rendererList)
		{
			if (!rendererList.isValid)
			{
				throw new ArgumentException("Invalid renderer list provided to DrawRendererList");
			}
			renderContext.ExecuteCommandBuffer(cmd);
			cmd.Clear();
			if (rendererList.stateBlock == null)
			{
				renderContext.DrawRenderers(rendererList.cullingResult, ref rendererList.drawSettings, ref rendererList.filteringSettings);
				return;
			}
			RenderStateBlock value = rendererList.stateBlock.Value;
			renderContext.DrawRenderers(rendererList.cullingResult, ref rendererList.drawSettings, ref rendererList.filteringSettings, ref value);
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0004D02C File Offset: 0x0004B22C
		internal unsafe static string ComputeProbeCameraName(string probeName, int face, string viewerName)
		{
			probeName = probeName ?? string.Empty;
			viewerName = viewerName ?? "null";
			int num = Mathf.Min(probeName.Length, 40);
			int num2 = Mathf.Min(viewerName.Length, 40);
			int num3 = "HDProbe RenderCamera (".Length + num + ": ".Length + 2 + " for viewer '".Length + num2 + "')".Length;
			char* ptr;
			char* ptr2;
			int num4;
			int i;
			checked
			{
				ptr = stackalloc char[unchecked((UIntPtr)num3) * 2];
				ptr2 = ptr;
				num4 = 0;
				i = 0;
			}
			while (i < "HDProbe RenderCamera (".Length)
			{
				*ptr2 = "HDProbe RenderCamera ("[i];
				i++;
				ptr2++;
			}
			i = 0;
			int num5 = Mathf.Min(probeName.Length, 40);
			while (i < num5)
			{
				*ptr2 = probeName[i];
				i++;
				ptr2++;
			}
			num4 += num5;
			i = 0;
			while (i < ": ".Length)
			{
				*ptr2 = ": "[i];
				i++;
				ptr2++;
			}
			int num6 = face * 205 >> 11;
			*(ptr2++) = (char)(num6 + 48);
			*(ptr2++) = (char)(face - num6 * 10 + 48);
			num4 += 2;
			i = 0;
			while (i < " for viewer '".Length)
			{
				*ptr2 = " for viewer '"[i];
				i++;
				ptr2++;
			}
			i = 0;
			num5 = Mathf.Min(viewerName.Length, 40);
			while (i < num5)
			{
				*ptr2 = viewerName[i];
				i++;
				ptr2++;
			}
			num4 += num5;
			i = 0;
			while (i < "')".Length)
			{
				*ptr2 = "')"[i];
				i++;
				ptr2++;
			}
			num4 += "HDProbe RenderCamera (".Length + ": ".Length + " for viewer '".Length + "')".Length;
			return new string(ptr, 0, num4);
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0004D240 File Offset: 0x0004B440
		internal unsafe static string ComputeCameraName(string cameraName)
		{
			int num = Mathf.Min(cameraName.Length, 40);
			int num2 = "HDRenderPipeline::Render ".Length + num;
			char* ptr;
			char* ptr2;
			int num3;
			int i;
			checked
			{
				ptr = stackalloc char[unchecked((UIntPtr)num2) * 2];
				ptr2 = ptr;
				num3 = 0;
				i = 0;
			}
			while (i < "HDRenderPipeline::Render ".Length)
			{
				*ptr2 = "HDRenderPipeline::Render "[i];
				i++;
				ptr2++;
			}
			i = 0;
			int num4 = num;
			while (i < num4)
			{
				*ptr2 = cameraName[i];
				i++;
				ptr2++;
			}
			num3 += num4;
			num3 += "HDRenderPipeline::Render ".Length;
			return new string(ptr, 0, num3);
		}

		// Token: 0x060009AE RID: 2478 RVA: 0x0004D2E0 File Offset: 0x0004B4E0
		internal static float ClampFOV(float fov)
		{
			return Mathf.Clamp(fov, 1E-05f, 179f);
		}

		// Token: 0x060009AF RID: 2479 RVA: 0x0004D2F2 File Offset: 0x0004B4F2
		internal static ulong GetSceneCullingMaskFromCamera(Camera camera)
		{
			return 0UL;
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0004D2F8 File Offset: 0x0004B4F8
		internal static HDAdditionalCameraData TryGetAdditionalCameraDataOrDefault(Camera camera)
		{
			if (camera == null || camera.Equals(null))
			{
				return HDUtils.s_DefaultHDAdditionalCameraData;
			}
			HDAdditionalCameraData hdadditionalCameraData;
			if (camera.TryGetComponent<HDAdditionalCameraData>(out hdadditionalCameraData))
			{
				return hdadditionalCameraData;
			}
			return HDUtils.s_DefaultHDAdditionalCameraData;
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0004D330 File Offset: 0x0004B530
		internal static int GetFormatSizeInBytes(GraphicsFormat format)
		{
			int num;
			if (HDUtils.graphicsFormatSizeCache.TryGetValue(format, out num))
			{
				return num;
			}
			string text = format.ToString();
			int num2 = text.IndexOf('_');
			text = text.Substring(0, (num2 == -1) ? text.Length : num2);
			int num3 = 0;
			foreach (object obj in Regex.Matches(text, "\\d+"))
			{
				Match match = (Match)obj;
				num3 += int.Parse(match.Value);
			}
			num = num3 / 8;
			HDUtils.graphicsFormatSizeCache[format] = num;
			return num;
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0004D3F0 File Offset: 0x0004B5F0
		internal static void DisplayUnsupportedMessage(string msg)
		{
			Debug.LogError(msg);
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0004D3F8 File Offset: 0x0004B5F8
		internal static void DisplayUnsupportedAPIMessage(string graphicAPI = null)
		{
			string operatingSystem = SystemInfo.operatingSystem;
			graphicAPI = graphicAPI ?? SystemInfo.graphicsDeviceType.ToString();
			HDUtils.DisplayUnsupportedMessage(string.Concat(new string[] { "Platform ", operatingSystem, " with device ", graphicAPI, " is not supported with High Definition Render Pipeline, no rendering will occur" }));
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0004D455 File Offset: 0x0004B655
		internal static void DisplayUnsupportedXRMessage()
		{
			HDUtils.DisplayUnsupportedMessage("AR/VR devices are not supported, no rendering will occur");
		}

		// Token: 0x04000F1D RID: 3869
		internal const PerObjectData k_RendererConfigurationBakedLighting = PerObjectData.LightProbe | PerObjectData.LightProbeProxyVolume | PerObjectData.Lightmaps;

		// Token: 0x04000F1E RID: 3870
		internal const PerObjectData k_RendererConfigurationBakedLightingWithShadowMask = PerObjectData.LightProbe | PerObjectData.LightProbeProxyVolume | PerObjectData.Lightmaps | PerObjectData.OcclusionProbe | PerObjectData.OcclusionProbeProxyVolume | PerObjectData.ShadowMask;

		// Token: 0x04000F1F RID: 3871
		private static Texture3D m_ClearTexture3D;

		// Token: 0x04000F20 RID: 3872
		private static RTHandle m_ClearTexture3DRTH;

		// Token: 0x04000F21 RID: 3873
		private static MaterialPropertyBlock s_PropertyBlock = new MaterialPropertyBlock();

		// Token: 0x04000F22 RID: 3874
		private static float s_OverlayLineHeight = -1f;

		// Token: 0x04000F23 RID: 3875
		private static Dictionary<GraphicsFormat, int> graphicsFormatSizeCache = new Dictionary<GraphicsFormat, int>
		{
			{
				GraphicsFormat.R8G8B8A8_UNorm,
				4
			},
			{
				GraphicsFormat.R16G16B16A16_SFloat,
				8
			},
			{
				GraphicsFormat.RGB_BC6H_SFloat,
				1
			}
		};

		// Token: 0x0200028B RID: 651
		internal struct PackedMipChainInfo
		{
			// Token: 0x06000CB2 RID: 3250 RVA: 0x00059F01 File Offset: 0x00058101
			public void Allocate()
			{
				this.mipLevelOffsets = new Vector2Int[15];
				this.mipLevelSizes = new Vector2Int[15];
				this.m_OffsetBufferWillNeedUpdate = true;
			}

			// Token: 0x06000CB3 RID: 3251 RVA: 0x00059F24 File Offset: 0x00058124
			public void ComputePackedMipChainInfo(Vector2Int viewportSize)
			{
				this.textureSize = viewportSize;
				this.mipLevelSizes[0] = viewportSize;
				this.mipLevelOffsets[0] = Vector2Int.zero;
				int num = 0;
				Vector2Int vector2Int = viewportSize;
				do
				{
					num++;
					vector2Int.x = Math.Max(1, vector2Int.x + 1 >> 1);
					vector2Int.y = Math.Max(1, vector2Int.y + 1 >> 1);
					this.mipLevelSizes[num] = vector2Int;
					Vector2Int vector2Int2 = this.mipLevelOffsets[num - 1];
					Vector2Int vector2Int3 = vector2Int2 + this.mipLevelSizes[num - 1];
					Vector2Int vector2Int4 = default(Vector2Int);
					if ((num & 1) != 0)
					{
						vector2Int4.x = vector2Int2.x;
						vector2Int4.y = vector2Int3.y;
					}
					else
					{
						vector2Int4.x = vector2Int3.x;
						vector2Int4.y = vector2Int2.y;
					}
					this.mipLevelOffsets[num] = vector2Int4;
					this.textureSize.x = Math.Max(this.textureSize.x, vector2Int4.x + vector2Int.x);
					this.textureSize.y = Math.Max(this.textureSize.y, vector2Int4.y + vector2Int.y);
				}
				while (vector2Int.x > 1 || vector2Int.y > 1);
				this.mipLevelCount = num + 1;
				this.m_OffsetBufferWillNeedUpdate = true;
			}

			// Token: 0x06000CB4 RID: 3252 RVA: 0x0005A092 File Offset: 0x00058292
			public ComputeBuffer GetOffsetBufferData(ComputeBuffer mipLevelOffsetsBuffer)
			{
				if (this.m_OffsetBufferWillNeedUpdate)
				{
					mipLevelOffsetsBuffer.SetData(this.mipLevelOffsets);
					this.m_OffsetBufferWillNeedUpdate = false;
				}
				return mipLevelOffsetsBuffer;
			}

			// Token: 0x040016DA RID: 5850
			public Vector2Int textureSize;

			// Token: 0x040016DB RID: 5851
			public int mipLevelCount;

			// Token: 0x040016DC RID: 5852
			public Vector2Int[] mipLevelSizes;

			// Token: 0x040016DD RID: 5853
			public Vector2Int[] mipLevelOffsets;

			// Token: 0x040016DE RID: 5854
			private bool m_OffsetBufferWillNeedUpdate;
		}
	}
}
