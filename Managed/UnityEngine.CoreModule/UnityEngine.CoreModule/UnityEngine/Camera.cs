using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000AB RID: 171
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Runtime/Camera/Camera.h")]
	[NativeHeader("Runtime/GfxDevice/GfxDeviceTypes.h")]
	[NativeHeader("Runtime/Camera/RenderManager.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Misc/GameObjectUtility.h")]
	[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
	[NativeHeader("Runtime/Shaders/Shader.h")]
	[NativeHeader("Runtime/Graphics/RenderTexture.h")]
	public sealed class Camera : Behaviour
	{
		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002F3 RID: 755
		// (set) Token: 0x060002F4 RID: 756
		[NativeProperty("Near")]
		public extern float nearClipPlane
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002F5 RID: 757
		// (set) Token: 0x060002F6 RID: 758
		[NativeProperty("Far")]
		public extern float farClipPlane
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002F7 RID: 759
		// (set) Token: 0x060002F8 RID: 760
		[NativeProperty("VerticalFieldOfView")]
		public extern float fieldOfView
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002F9 RID: 761
		// (set) Token: 0x060002FA RID: 762
		public extern RenderingPath renderingPath
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002FB RID: 763
		public extern RenderingPath actualRenderingPath
		{
			[NativeName("CalculateRenderingPath")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060002FC RID: 764
		[MethodImpl(4096)]
		public extern void Reset();

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002FD RID: 765
		// (set) Token: 0x060002FE RID: 766
		public extern bool allowHDR
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002FF RID: 767
		// (set) Token: 0x06000300 RID: 768
		public extern bool allowMSAA
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000301 RID: 769
		// (set) Token: 0x06000302 RID: 770
		public extern bool allowDynamicResolution
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000303 RID: 771
		// (set) Token: 0x06000304 RID: 772
		[NativeProperty("ForceIntoRT")]
		public extern bool forceIntoRenderTexture
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000305 RID: 773
		// (set) Token: 0x06000306 RID: 774
		public extern float orthographicSize
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000307 RID: 775
		// (set) Token: 0x06000308 RID: 776
		public extern bool orthographic
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000309 RID: 777
		// (set) Token: 0x0600030A RID: 778
		public extern OpaqueSortMode opaqueSortMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600030B RID: 779
		// (set) Token: 0x0600030C RID: 780
		public extern TransparencySortMode transparencySortMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600030D RID: 781 RVA: 0x00005978 File Offset: 0x00003B78
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000598E File Offset: 0x00003B8E
		public Vector3 transparencySortAxis
		{
			get
			{
				Vector3 vector;
				this.get_transparencySortAxis_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_transparencySortAxis_Injected(ref value);
			}
		}

		// Token: 0x0600030F RID: 783
		[MethodImpl(4096)]
		public extern void ResetTransparencySortSettings();

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000310 RID: 784
		// (set) Token: 0x06000311 RID: 785
		public extern float depth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000312 RID: 786
		// (set) Token: 0x06000313 RID: 787
		public extern float aspect
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000314 RID: 788
		[MethodImpl(4096)]
		public extern void ResetAspect();

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00005998 File Offset: 0x00003B98
		public Vector3 velocity
		{
			get
			{
				Vector3 vector;
				this.get_velocity_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000316 RID: 790
		// (set) Token: 0x06000317 RID: 791
		public extern int cullingMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000318 RID: 792
		// (set) Token: 0x06000319 RID: 793
		public extern int eventMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600031A RID: 794
		// (set) Token: 0x0600031B RID: 795
		public extern bool layerCullSpherical
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x0600031C RID: 796
		// (set) Token: 0x0600031D RID: 797
		public extern CameraType cameraType
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600031E RID: 798
		// (set) Token: 0x0600031F RID: 799
		[NativeConditional("UNITY_EDITOR")]
		public extern ulong overrideSceneCullingMask
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000320 RID: 800
		[NativeConditional("UNITY_EDITOR")]
		internal extern ulong sceneCullingMask
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000321 RID: 801
		[FreeFunction("CameraScripting::GetLayerCullDistances", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern float[] GetLayerCullDistances();

		// Token: 0x06000322 RID: 802
		[FreeFunction("CameraScripting::SetLayerCullDistances", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern void SetLayerCullDistances([NotNull] float[] d);

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000323 RID: 803 RVA: 0x000059B0 File Offset: 0x00003BB0
		// (set) Token: 0x06000324 RID: 804 RVA: 0x000059C8 File Offset: 0x00003BC8
		public float[] layerCullDistances
		{
			get
			{
				return this.GetLayerCullDistances();
			}
			set
			{
				bool flag = value.Length != 32;
				if (flag)
				{
					throw new UnityException("Array needs to contain exactly 32 floats for layerCullDistances.");
				}
				this.SetLayerCullDistances(value);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000325 RID: 805
		internal static extern int PreviewCullingLayer
		{
			[FreeFunction("CameraScripting::GetPreviewCullingLayer")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000326 RID: 806
		// (set) Token: 0x06000327 RID: 807
		public extern bool useOcclusionCulling
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000328 RID: 808 RVA: 0x000059F8 File Offset: 0x00003BF8
		// (set) Token: 0x06000329 RID: 809 RVA: 0x00005A0E File Offset: 0x00003C0E
		public Matrix4x4 cullingMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_cullingMatrix_Injected(out matrix4x);
				return matrix4x;
			}
			set
			{
				this.set_cullingMatrix_Injected(ref value);
			}
		}

		// Token: 0x0600032A RID: 810
		[MethodImpl(4096)]
		public extern void ResetCullingMatrix();

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00005A18 File Offset: 0x00003C18
		// (set) Token: 0x0600032C RID: 812 RVA: 0x00005A2E File Offset: 0x00003C2E
		public Color backgroundColor
		{
			get
			{
				Color color;
				this.get_backgroundColor_Injected(out color);
				return color;
			}
			set
			{
				this.set_backgroundColor_Injected(ref value);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600032D RID: 813
		// (set) Token: 0x0600032E RID: 814
		public extern CameraClearFlags clearFlags
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600032F RID: 815
		// (set) Token: 0x06000330 RID: 816
		public extern DepthTextureMode depthTextureMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000331 RID: 817
		// (set) Token: 0x06000332 RID: 818
		public extern bool clearStencilAfterLightingPass
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000333 RID: 819
		[MethodImpl(4096)]
		public extern void SetReplacementShader(Shader shader, string replacementTag);

		// Token: 0x06000334 RID: 820
		[MethodImpl(4096)]
		public extern void ResetReplacementShader();

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000335 RID: 821
		internal extern Camera.ProjectionMatrixMode projectionMatrixMode
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000336 RID: 822
		// (set) Token: 0x06000337 RID: 823
		public extern bool usePhysicalProperties
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00005A38 File Offset: 0x00003C38
		// (set) Token: 0x06000339 RID: 825 RVA: 0x00005A4E File Offset: 0x00003C4E
		public Vector2 sensorSize
		{
			get
			{
				Vector2 vector;
				this.get_sensorSize_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_sensorSize_Injected(ref value);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00005A58 File Offset: 0x00003C58
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00005A6E File Offset: 0x00003C6E
		public Vector2 lensShift
		{
			get
			{
				Vector2 vector;
				this.get_lensShift_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_lensShift_Injected(ref value);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600033C RID: 828
		// (set) Token: 0x0600033D RID: 829
		public extern float focalLength
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600033E RID: 830
		// (set) Token: 0x0600033F RID: 831
		public extern Camera.GateFitMode gateFit
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000340 RID: 832
		[MethodImpl(4096)]
		public extern float GetGateFittedFieldOfView();

		// Token: 0x06000341 RID: 833 RVA: 0x00005A78 File Offset: 0x00003C78
		public Vector2 GetGateFittedLensShift()
		{
			Vector2 vector;
			this.GetGateFittedLensShift_Injected(out vector);
			return vector;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00005A90 File Offset: 0x00003C90
		internal Vector3 GetLocalSpaceAim()
		{
			Vector3 vector;
			this.GetLocalSpaceAim_Injected(out vector);
			return vector;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00005AA8 File Offset: 0x00003CA8
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00005ABE File Offset: 0x00003CBE
		[NativeProperty("NormalizedViewportRect")]
		public Rect rect
		{
			get
			{
				Rect rect;
				this.get_rect_Injected(out rect);
				return rect;
			}
			set
			{
				this.set_rect_Injected(ref value);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000345 RID: 837 RVA: 0x00005AC8 File Offset: 0x00003CC8
		// (set) Token: 0x06000346 RID: 838 RVA: 0x00005ADE File Offset: 0x00003CDE
		[NativeProperty("ScreenViewportRect")]
		public Rect pixelRect
		{
			get
			{
				Rect rect;
				this.get_pixelRect_Injected(out rect);
				return rect;
			}
			set
			{
				this.set_pixelRect_Injected(ref value);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000347 RID: 839
		public extern int pixelWidth
		{
			[FreeFunction("CameraScripting::GetPixelWidth", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000348 RID: 840
		public extern int pixelHeight
		{
			[FreeFunction("CameraScripting::GetPixelHeight", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000349 RID: 841
		public extern int scaledPixelWidth
		{
			[FreeFunction("CameraScripting::GetScaledPixelWidth", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600034A RID: 842
		public extern int scaledPixelHeight
		{
			[FreeFunction("CameraScripting::GetScaledPixelHeight", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600034B RID: 843
		// (set) Token: 0x0600034C RID: 844
		public extern RenderTexture targetTexture
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600034D RID: 845
		public extern RenderTexture activeTexture
		{
			[NativeName("GetCurrentTargetTexture")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600034E RID: 846
		// (set) Token: 0x0600034F RID: 847
		public extern int targetDisplay
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00005AE8 File Offset: 0x00003CE8
		[FreeFunction("CameraScripting::SetTargetBuffers", HasExplicitThis = true)]
		private void SetTargetBuffersImpl(RenderBuffer color, RenderBuffer depth)
		{
			this.SetTargetBuffersImpl_Injected(ref color, ref depth);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00005AF4 File Offset: 0x00003CF4
		public void SetTargetBuffers(RenderBuffer colorBuffer, RenderBuffer depthBuffer)
		{
			this.SetTargetBuffersImpl(colorBuffer, depthBuffer);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00005B00 File Offset: 0x00003D00
		[FreeFunction("CameraScripting::SetTargetBuffers", HasExplicitThis = true)]
		private void SetTargetBuffersMRTImpl(RenderBuffer[] color, RenderBuffer depth)
		{
			this.SetTargetBuffersMRTImpl_Injected(color, ref depth);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00005B0B File Offset: 0x00003D0B
		public void SetTargetBuffers(RenderBuffer[] colorBuffer, RenderBuffer depthBuffer)
		{
			this.SetTargetBuffersMRTImpl(colorBuffer, depthBuffer);
		}

		// Token: 0x06000354 RID: 852
		[MethodImpl(4096)]
		internal extern string[] GetCameraBufferWarnings();

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00005B18 File Offset: 0x00003D18
		public Matrix4x4 cameraToWorldMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_cameraToWorldMatrix_Injected(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00005B30 File Offset: 0x00003D30
		// (set) Token: 0x06000357 RID: 855 RVA: 0x00005B46 File Offset: 0x00003D46
		public Matrix4x4 worldToCameraMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_worldToCameraMatrix_Injected(out matrix4x);
				return matrix4x;
			}
			set
			{
				this.set_worldToCameraMatrix_Injected(ref value);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00005B50 File Offset: 0x00003D50
		// (set) Token: 0x06000359 RID: 857 RVA: 0x00005B66 File Offset: 0x00003D66
		public Matrix4x4 projectionMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_projectionMatrix_Injected(out matrix4x);
				return matrix4x;
			}
			set
			{
				this.set_projectionMatrix_Injected(ref value);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00005B70 File Offset: 0x00003D70
		// (set) Token: 0x0600035B RID: 859 RVA: 0x00005B86 File Offset: 0x00003D86
		public Matrix4x4 nonJitteredProjectionMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_nonJitteredProjectionMatrix_Injected(out matrix4x);
				return matrix4x;
			}
			set
			{
				this.set_nonJitteredProjectionMatrix_Injected(ref value);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600035C RID: 860
		// (set) Token: 0x0600035D RID: 861
		[NativeProperty("UseJitteredProjectionMatrixForTransparent")]
		public extern bool useJitteredProjectionMatrixForTransparentRendering
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00005B90 File Offset: 0x00003D90
		public Matrix4x4 previousViewProjectionMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.get_previousViewProjectionMatrix_Injected(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x0600035F RID: 863
		[MethodImpl(4096)]
		public extern void ResetWorldToCameraMatrix();

		// Token: 0x06000360 RID: 864
		[MethodImpl(4096)]
		public extern void ResetProjectionMatrix();

		// Token: 0x06000361 RID: 865 RVA: 0x00005BA8 File Offset: 0x00003DA8
		[FreeFunction("CameraScripting::CalculateObliqueMatrix", HasExplicitThis = true)]
		public Matrix4x4 CalculateObliqueMatrix(Vector4 clipPlane)
		{
			Matrix4x4 matrix4x;
			this.CalculateObliqueMatrix_Injected(ref clipPlane, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00005BC0 File Offset: 0x00003DC0
		public Vector3 WorldToScreenPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			Vector3 vector;
			this.WorldToScreenPoint_Injected(ref position, eye, out vector);
			return vector;
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00005BDC File Offset: 0x00003DDC
		public Vector3 WorldToViewportPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			Vector3 vector;
			this.WorldToViewportPoint_Injected(ref position, eye, out vector);
			return vector;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x00005BF8 File Offset: 0x00003DF8
		public Vector3 ViewportToWorldPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			Vector3 vector;
			this.ViewportToWorldPoint_Injected(ref position, eye, out vector);
			return vector;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00005C14 File Offset: 0x00003E14
		public Vector3 ScreenToWorldPoint(Vector3 position, Camera.MonoOrStereoscopicEye eye)
		{
			Vector3 vector;
			this.ScreenToWorldPoint_Injected(ref position, eye, out vector);
			return vector;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x00005C30 File Offset: 0x00003E30
		public Vector3 WorldToScreenPoint(Vector3 position)
		{
			return this.WorldToScreenPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00005C4C File Offset: 0x00003E4C
		public Vector3 WorldToViewportPoint(Vector3 position)
		{
			return this.WorldToViewportPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00005C68 File Offset: 0x00003E68
		public Vector3 ViewportToWorldPoint(Vector3 position)
		{
			return this.ViewportToWorldPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00005C84 File Offset: 0x00003E84
		public Vector3 ScreenToWorldPoint(Vector3 position)
		{
			return this.ScreenToWorldPoint(position, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00005CA0 File Offset: 0x00003EA0
		public Vector3 ScreenToViewportPoint(Vector3 position)
		{
			Vector3 vector;
			this.ScreenToViewportPoint_Injected(ref position, out vector);
			return vector;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00005CB8 File Offset: 0x00003EB8
		public Vector3 ViewportToScreenPoint(Vector3 position)
		{
			Vector3 vector;
			this.ViewportToScreenPoint_Injected(ref position, out vector);
			return vector;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00005CD0 File Offset: 0x00003ED0
		internal Vector2 GetFrustumPlaneSizeAt(float distance)
		{
			Vector2 vector;
			this.GetFrustumPlaneSizeAt_Injected(distance, out vector);
			return vector;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00005CE8 File Offset: 0x00003EE8
		private Ray ViewportPointToRay(Vector2 pos, Camera.MonoOrStereoscopicEye eye)
		{
			Ray ray;
			this.ViewportPointToRay_Injected(ref pos, eye, out ray);
			return ray;
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00005D04 File Offset: 0x00003F04
		public Ray ViewportPointToRay(Vector3 pos, Camera.MonoOrStereoscopicEye eye)
		{
			return this.ViewportPointToRay(pos, eye);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00005D24 File Offset: 0x00003F24
		public Ray ViewportPointToRay(Vector3 pos)
		{
			return this.ViewportPointToRay(pos, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00005D40 File Offset: 0x00003F40
		private Ray ScreenPointToRay(Vector2 pos, Camera.MonoOrStereoscopicEye eye)
		{
			Ray ray;
			this.ScreenPointToRay_Injected(ref pos, eye, out ray);
			return ray;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00005D5C File Offset: 0x00003F5C
		public Ray ScreenPointToRay(Vector3 pos, Camera.MonoOrStereoscopicEye eye)
		{
			return this.ScreenPointToRay(pos, eye);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00005D7C File Offset: 0x00003F7C
		public Ray ScreenPointToRay(Vector3 pos)
		{
			return this.ScreenPointToRay(pos, Camera.MonoOrStereoscopicEye.Mono);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00005D96 File Offset: 0x00003F96
		[FreeFunction("CameraScripting::CalculateViewportRayVectors", HasExplicitThis = true)]
		private void CalculateFrustumCornersInternal(Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, [Out] Vector3[] outCorners)
		{
			this.CalculateFrustumCornersInternal_Injected(ref viewport, z, eye, outCorners);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00005DA4 File Offset: 0x00003FA4
		public void CalculateFrustumCorners(Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, Vector3[] outCorners)
		{
			bool flag = outCorners == null;
			if (flag)
			{
				throw new ArgumentNullException("outCorners");
			}
			bool flag2 = outCorners.Length < 4;
			if (flag2)
			{
				throw new ArgumentException("outCorners minimum size is 4", "outCorners");
			}
			this.CalculateFrustumCornersInternal(viewport, z, eye, outCorners);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00005DED File Offset: 0x00003FED
		[NativeName("CalculateProjectionMatrixFromPhysicalProperties")]
		private static void CalculateProjectionMatrixFromPhysicalPropertiesInternal(out Matrix4x4 output, float focalLength, Vector2 sensorSize, Vector2 lensShift, float nearClip, float farClip, float gateAspect, Camera.GateFitMode gateFitMode)
		{
			Camera.CalculateProjectionMatrixFromPhysicalPropertiesInternal_Injected(out output, focalLength, ref sensorSize, ref lensShift, nearClip, farClip, gateAspect, gateFitMode);
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00005E02 File Offset: 0x00004002
		public static void CalculateProjectionMatrixFromPhysicalProperties(out Matrix4x4 output, float focalLength, Vector2 sensorSize, Vector2 lensShift, float nearClip, float farClip, Camera.GateFitParameters gateFitParameters = default(Camera.GateFitParameters))
		{
			Camera.CalculateProjectionMatrixFromPhysicalPropertiesInternal(out output, focalLength, sensorSize, lensShift, nearClip, farClip, gateFitParameters.aspect, gateFitParameters.mode);
		}

		// Token: 0x06000377 RID: 887
		[NativeName("FocalLengthToFieldOfView_Safe")]
		[MethodImpl(4096)]
		public static extern float FocalLengthToFieldOfView(float focalLength, float sensorSize);

		// Token: 0x06000378 RID: 888
		[NativeName("FieldOfViewToFocalLength_Safe")]
		[MethodImpl(4096)]
		public static extern float FieldOfViewToFocalLength(float fieldOfView, float sensorSize);

		// Token: 0x06000379 RID: 889
		[NativeName("HorizontalToVerticalFieldOfView_Safe")]
		[MethodImpl(4096)]
		public static extern float HorizontalToVerticalFieldOfView(float horizontalFieldOfView, float aspectRatio);

		// Token: 0x0600037A RID: 890
		[MethodImpl(4096)]
		public static extern float VerticalToHorizontalFieldOfView(float verticalFieldOfView, float aspectRatio);

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600037B RID: 891
		public static extern Camera main
		{
			[FreeFunction("FindMainCamera")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600037C RID: 892
		public static extern Camera current
		{
			[FreeFunction("GetCurrentCameraPtr")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00005E24 File Offset: 0x00004024
		// (set) Token: 0x0600037E RID: 894 RVA: 0x00005E3A File Offset: 0x0000403A
		public Scene scene
		{
			[FreeFunction("CameraScripting::GetScene", HasExplicitThis = true)]
			get
			{
				Scene scene;
				this.get_scene_Injected(out scene);
				return scene;
			}
			[FreeFunction("CameraScripting::SetScene", HasExplicitThis = true)]
			set
			{
				this.set_scene_Injected(ref value);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600037F RID: 895
		public extern bool stereoEnabled
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000380 RID: 896
		// (set) Token: 0x06000381 RID: 897
		public extern float stereoSeparation
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000382 RID: 898
		// (set) Token: 0x06000383 RID: 899
		public extern float stereoConvergence
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000384 RID: 900
		public extern bool areVRStereoViewMatricesWithinSingleCullTolerance
		{
			[NativeName("AreVRStereoViewMatricesWithinSingleCullTolerance")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000385 RID: 901
		// (set) Token: 0x06000386 RID: 902
		public extern StereoTargetEyeMask stereoTargetEye
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000387 RID: 903
		public extern Camera.MonoOrStereoscopicEye stereoActiveEye
		{
			[FreeFunction("CameraScripting::GetStereoActiveEye", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x00005E44 File Offset: 0x00004044
		public Matrix4x4 GetStereoNonJitteredProjectionMatrix(Camera.StereoscopicEye eye)
		{
			Matrix4x4 matrix4x;
			this.GetStereoNonJitteredProjectionMatrix_Injected(eye, out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00005E5C File Offset: 0x0000405C
		public Matrix4x4 GetStereoViewMatrix(Camera.StereoscopicEye eye)
		{
			Matrix4x4 matrix4x;
			this.GetStereoViewMatrix_Injected(eye, out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600038A RID: 906
		[MethodImpl(4096)]
		public extern void CopyStereoDeviceProjectionMatrixToNonJittered(Camera.StereoscopicEye eye);

		// Token: 0x0600038B RID: 907 RVA: 0x00005E74 File Offset: 0x00004074
		public Matrix4x4 GetStereoProjectionMatrix(Camera.StereoscopicEye eye)
		{
			Matrix4x4 matrix4x;
			this.GetStereoProjectionMatrix_Injected(eye, out matrix4x);
			return matrix4x;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00005E8B File Offset: 0x0000408B
		public void SetStereoProjectionMatrix(Camera.StereoscopicEye eye, Matrix4x4 matrix)
		{
			this.SetStereoProjectionMatrix_Injected(eye, ref matrix);
		}

		// Token: 0x0600038D RID: 909
		[MethodImpl(4096)]
		public extern void ResetStereoProjectionMatrices();

		// Token: 0x0600038E RID: 910 RVA: 0x00005E96 File Offset: 0x00004096
		public void SetStereoViewMatrix(Camera.StereoscopicEye eye, Matrix4x4 matrix)
		{
			this.SetStereoViewMatrix_Injected(eye, ref matrix);
		}

		// Token: 0x0600038F RID: 911
		[MethodImpl(4096)]
		public extern void ResetStereoViewMatrices();

		// Token: 0x06000390 RID: 912
		[FreeFunction("CameraScripting::GetAllCamerasCount")]
		[MethodImpl(4096)]
		private static extern int GetAllCamerasCount();

		// Token: 0x06000391 RID: 913
		[FreeFunction("CameraScripting::GetAllCameras")]
		[MethodImpl(4096)]
		private static extern int GetAllCamerasImpl([NotNull] [Out] Camera[] cam);

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000392 RID: 914 RVA: 0x00005EA4 File Offset: 0x000040A4
		public static int allCamerasCount
		{
			get
			{
				return Camera.GetAllCamerasCount();
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00005EBC File Offset: 0x000040BC
		public static Camera[] allCameras
		{
			get
			{
				Camera[] array = new Camera[Camera.allCamerasCount];
				Camera.GetAllCamerasImpl(array);
				return array;
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00005EE4 File Offset: 0x000040E4
		public static int GetAllCameras(Camera[] cameras)
		{
			bool flag = cameras == null;
			if (flag)
			{
				throw new NullReferenceException();
			}
			bool flag2 = cameras.Length < Camera.allCamerasCount;
			if (flag2)
			{
				throw new ArgumentException("Passed in array to fill with cameras is to small to hold the number of cameras. Use Camera.allCamerasCount to get the needed size.");
			}
			return Camera.GetAllCamerasImpl(cameras);
		}

		// Token: 0x06000395 RID: 917
		[FreeFunction("CameraScripting::RenderToCubemap", HasExplicitThis = true)]
		[MethodImpl(4096)]
		private extern bool RenderToCubemapImpl(Texture tex, [DefaultValue("63")] int faceMask);

		// Token: 0x06000396 RID: 918 RVA: 0x00005F24 File Offset: 0x00004124
		public bool RenderToCubemap(Cubemap cubemap, int faceMask)
		{
			return this.RenderToCubemapImpl(cubemap, faceMask);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00005F40 File Offset: 0x00004140
		public bool RenderToCubemap(Cubemap cubemap)
		{
			return this.RenderToCubemapImpl(cubemap, 63);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00005F5C File Offset: 0x0000415C
		public bool RenderToCubemap(RenderTexture cubemap, int faceMask)
		{
			return this.RenderToCubemapImpl(cubemap, faceMask);
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00005F78 File Offset: 0x00004178
		public bool RenderToCubemap(RenderTexture cubemap)
		{
			return this.RenderToCubemapImpl(cubemap, 63);
		}

		// Token: 0x0600039A RID: 922
		[NativeName("RenderToCubemap")]
		[MethodImpl(4096)]
		private extern bool RenderToCubemapEyeImpl(RenderTexture cubemap, int faceMask, Camera.MonoOrStereoscopicEye stereoEye);

		// Token: 0x0600039B RID: 923 RVA: 0x00005F94 File Offset: 0x00004194
		public bool RenderToCubemap(RenderTexture cubemap, int faceMask, Camera.MonoOrStereoscopicEye stereoEye)
		{
			return this.RenderToCubemapEyeImpl(cubemap, faceMask, stereoEye);
		}

		// Token: 0x0600039C RID: 924
		[FreeFunction("CameraScripting::Render", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void Render();

		// Token: 0x0600039D RID: 925
		[FreeFunction("CameraScripting::RenderWithShader", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void RenderWithShader(Shader shader, string replacementTag);

		// Token: 0x0600039E RID: 926
		[FreeFunction("CameraScripting::RenderDontRestore", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void RenderDontRestore();

		// Token: 0x0600039F RID: 927
		[FreeFunction("CameraScripting::SetupCurrent")]
		[MethodImpl(4096)]
		public static extern void SetupCurrent(Camera cur);

		// Token: 0x060003A0 RID: 928
		[FreeFunction("CameraScripting::CopyFrom", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void CopyFrom(Camera other);

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060003A1 RID: 929
		public extern int commandBufferCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x060003A2 RID: 930
		[MethodImpl(4096)]
		public extern void RemoveCommandBuffers(CameraEvent evt);

		// Token: 0x060003A3 RID: 931
		[MethodImpl(4096)]
		public extern void RemoveAllCommandBuffers();

		// Token: 0x060003A4 RID: 932
		[NativeName("AddCommandBuffer")]
		[MethodImpl(4096)]
		private extern void AddCommandBufferImpl(CameraEvent evt, [NotNull] CommandBuffer buffer);

		// Token: 0x060003A5 RID: 933
		[NativeName("AddCommandBufferAsync")]
		[MethodImpl(4096)]
		private extern void AddCommandBufferAsyncImpl(CameraEvent evt, [NotNull] CommandBuffer buffer, ComputeQueueType queueType);

		// Token: 0x060003A6 RID: 934
		[NativeName("RemoveCommandBuffer")]
		[MethodImpl(4096)]
		private extern void RemoveCommandBufferImpl(CameraEvent evt, [NotNull] CommandBuffer buffer);

		// Token: 0x060003A7 RID: 935 RVA: 0x00005FB0 File Offset: 0x000041B0
		public void AddCommandBuffer(CameraEvent evt, CommandBuffer buffer)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			this.AddCommandBufferImpl(evt, buffer);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00006004 File Offset: 0x00004204
		public void AddCommandBufferAsync(CameraEvent evt, CommandBuffer buffer, ComputeQueueType queueType)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			this.AddCommandBufferAsyncImpl(evt, buffer, queueType);
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00006058 File Offset: 0x00004258
		public void RemoveCommandBuffer(CameraEvent evt, CommandBuffer buffer)
		{
			bool flag = !CameraEventUtils.IsValid(evt);
			if (flag)
			{
				throw new ArgumentException(string.Format("Invalid CameraEvent value \"{0}\".", (int)evt), "evt");
			}
			bool flag2 = buffer == null;
			if (flag2)
			{
				throw new NullReferenceException("buffer is null");
			}
			this.RemoveCommandBufferImpl(evt, buffer);
		}

		// Token: 0x060003AA RID: 938
		[FreeFunction("CameraScripting::GetCommandBuffers", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern CommandBuffer[] GetCommandBuffers(CameraEvent evt);

		// Token: 0x060003AB RID: 939 RVA: 0x000060AC File Offset: 0x000042AC
		[RequiredByNativeCode]
		private static void FireOnPreCull(Camera cam)
		{
			bool flag = Camera.onPreCull != null;
			if (flag)
			{
				Camera.onPreCull(cam);
			}
		}

		// Token: 0x060003AC RID: 940 RVA: 0x000060D4 File Offset: 0x000042D4
		[RequiredByNativeCode]
		private static void FireOnPreRender(Camera cam)
		{
			bool flag = Camera.onPreRender != null;
			if (flag)
			{
				Camera.onPreRender(cam);
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x000060FC File Offset: 0x000042FC
		[RequiredByNativeCode]
		private static void FireOnPostRender(Camera cam)
		{
			bool flag = Camera.onPostRender != null;
			if (flag)
			{
				Camera.onPostRender(cam);
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00002EC3 File Offset: 0x000010C3
		internal void OnlyUsedForTesting1()
		{
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00002EC3 File Offset: 0x000010C3
		internal void OnlyUsedForTesting2()
		{
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00006124 File Offset: 0x00004324
		public unsafe bool TryGetCullingParameters(out ScriptableCullingParameters cullingParameters)
		{
			return Camera.GetCullingParameters_Internal(this, false, out cullingParameters, sizeof(ScriptableCullingParameters));
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00006144 File Offset: 0x00004344
		public unsafe bool TryGetCullingParameters(bool stereoAware, out ScriptableCullingParameters cullingParameters)
		{
			return Camera.GetCullingParameters_Internal(this, stereoAware, out cullingParameters, sizeof(ScriptableCullingParameters));
		}

		// Token: 0x060003B2 RID: 946
		[FreeFunction("ScriptableRenderPipeline_Bindings::GetCullingParameters_Internal")]
		[NativeHeader("Runtime/Export/RenderPipeline/ScriptableRenderPipeline.bindings.h")]
		[MethodImpl(4096)]
		private static extern bool GetCullingParameters_Internal(Camera camera, bool stereoAware, out ScriptableCullingParameters cullingParameters, int managedCullingParametersSize);

		// Token: 0x060003B4 RID: 948
		[MethodImpl(4096)]
		private extern void get_transparencySortAxis_Injected(out Vector3 ret);

		// Token: 0x060003B5 RID: 949
		[MethodImpl(4096)]
		private extern void set_transparencySortAxis_Injected(ref Vector3 value);

		// Token: 0x060003B6 RID: 950
		[MethodImpl(4096)]
		private extern void get_velocity_Injected(out Vector3 ret);

		// Token: 0x060003B7 RID: 951
		[MethodImpl(4096)]
		private extern void get_cullingMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x060003B8 RID: 952
		[MethodImpl(4096)]
		private extern void set_cullingMatrix_Injected(ref Matrix4x4 value);

		// Token: 0x060003B9 RID: 953
		[MethodImpl(4096)]
		private extern void get_backgroundColor_Injected(out Color ret);

		// Token: 0x060003BA RID: 954
		[MethodImpl(4096)]
		private extern void set_backgroundColor_Injected(ref Color value);

		// Token: 0x060003BB RID: 955
		[MethodImpl(4096)]
		private extern void get_sensorSize_Injected(out Vector2 ret);

		// Token: 0x060003BC RID: 956
		[MethodImpl(4096)]
		private extern void set_sensorSize_Injected(ref Vector2 value);

		// Token: 0x060003BD RID: 957
		[MethodImpl(4096)]
		private extern void get_lensShift_Injected(out Vector2 ret);

		// Token: 0x060003BE RID: 958
		[MethodImpl(4096)]
		private extern void set_lensShift_Injected(ref Vector2 value);

		// Token: 0x060003BF RID: 959
		[MethodImpl(4096)]
		private extern void GetGateFittedLensShift_Injected(out Vector2 ret);

		// Token: 0x060003C0 RID: 960
		[MethodImpl(4096)]
		private extern void GetLocalSpaceAim_Injected(out Vector3 ret);

		// Token: 0x060003C1 RID: 961
		[MethodImpl(4096)]
		private extern void get_rect_Injected(out Rect ret);

		// Token: 0x060003C2 RID: 962
		[MethodImpl(4096)]
		private extern void set_rect_Injected(ref Rect value);

		// Token: 0x060003C3 RID: 963
		[MethodImpl(4096)]
		private extern void get_pixelRect_Injected(out Rect ret);

		// Token: 0x060003C4 RID: 964
		[MethodImpl(4096)]
		private extern void set_pixelRect_Injected(ref Rect value);

		// Token: 0x060003C5 RID: 965
		[MethodImpl(4096)]
		private extern void SetTargetBuffersImpl_Injected(ref RenderBuffer color, ref RenderBuffer depth);

		// Token: 0x060003C6 RID: 966
		[MethodImpl(4096)]
		private extern void SetTargetBuffersMRTImpl_Injected(RenderBuffer[] color, ref RenderBuffer depth);

		// Token: 0x060003C7 RID: 967
		[MethodImpl(4096)]
		private extern void get_cameraToWorldMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x060003C8 RID: 968
		[MethodImpl(4096)]
		private extern void get_worldToCameraMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x060003C9 RID: 969
		[MethodImpl(4096)]
		private extern void set_worldToCameraMatrix_Injected(ref Matrix4x4 value);

		// Token: 0x060003CA RID: 970
		[MethodImpl(4096)]
		private extern void get_projectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x060003CB RID: 971
		[MethodImpl(4096)]
		private extern void set_projectionMatrix_Injected(ref Matrix4x4 value);

		// Token: 0x060003CC RID: 972
		[MethodImpl(4096)]
		private extern void get_nonJitteredProjectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x060003CD RID: 973
		[MethodImpl(4096)]
		private extern void set_nonJitteredProjectionMatrix_Injected(ref Matrix4x4 value);

		// Token: 0x060003CE RID: 974
		[MethodImpl(4096)]
		private extern void get_previousViewProjectionMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x060003CF RID: 975
		[MethodImpl(4096)]
		private extern void CalculateObliqueMatrix_Injected(ref Vector4 clipPlane, out Matrix4x4 ret);

		// Token: 0x060003D0 RID: 976
		[MethodImpl(4096)]
		private extern void WorldToScreenPoint_Injected(ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x060003D1 RID: 977
		[MethodImpl(4096)]
		private extern void WorldToViewportPoint_Injected(ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x060003D2 RID: 978
		[MethodImpl(4096)]
		private extern void ViewportToWorldPoint_Injected(ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x060003D3 RID: 979
		[MethodImpl(4096)]
		private extern void ScreenToWorldPoint_Injected(ref Vector3 position, Camera.MonoOrStereoscopicEye eye, out Vector3 ret);

		// Token: 0x060003D4 RID: 980
		[MethodImpl(4096)]
		private extern void ScreenToViewportPoint_Injected(ref Vector3 position, out Vector3 ret);

		// Token: 0x060003D5 RID: 981
		[MethodImpl(4096)]
		private extern void ViewportToScreenPoint_Injected(ref Vector3 position, out Vector3 ret);

		// Token: 0x060003D6 RID: 982
		[MethodImpl(4096)]
		private extern void GetFrustumPlaneSizeAt_Injected(float distance, out Vector2 ret);

		// Token: 0x060003D7 RID: 983
		[MethodImpl(4096)]
		private extern void ViewportPointToRay_Injected(ref Vector2 pos, Camera.MonoOrStereoscopicEye eye, out Ray ret);

		// Token: 0x060003D8 RID: 984
		[MethodImpl(4096)]
		private extern void ScreenPointToRay_Injected(ref Vector2 pos, Camera.MonoOrStereoscopicEye eye, out Ray ret);

		// Token: 0x060003D9 RID: 985
		[MethodImpl(4096)]
		private extern void CalculateFrustumCornersInternal_Injected(ref Rect viewport, float z, Camera.MonoOrStereoscopicEye eye, [Out] Vector3[] outCorners);

		// Token: 0x060003DA RID: 986
		[MethodImpl(4096)]
		private static extern void CalculateProjectionMatrixFromPhysicalPropertiesInternal_Injected(out Matrix4x4 output, float focalLength, ref Vector2 sensorSize, ref Vector2 lensShift, float nearClip, float farClip, float gateAspect, Camera.GateFitMode gateFitMode);

		// Token: 0x060003DB RID: 987
		[MethodImpl(4096)]
		private extern void get_scene_Injected(out Scene ret);

		// Token: 0x060003DC RID: 988
		[MethodImpl(4096)]
		private extern void set_scene_Injected(ref Scene value);

		// Token: 0x060003DD RID: 989
		[MethodImpl(4096)]
		private extern void GetStereoNonJitteredProjectionMatrix_Injected(Camera.StereoscopicEye eye, out Matrix4x4 ret);

		// Token: 0x060003DE RID: 990
		[MethodImpl(4096)]
		private extern void GetStereoViewMatrix_Injected(Camera.StereoscopicEye eye, out Matrix4x4 ret);

		// Token: 0x060003DF RID: 991
		[MethodImpl(4096)]
		private extern void GetStereoProjectionMatrix_Injected(Camera.StereoscopicEye eye, out Matrix4x4 ret);

		// Token: 0x060003E0 RID: 992
		[MethodImpl(4096)]
		private extern void SetStereoProjectionMatrix_Injected(Camera.StereoscopicEye eye, ref Matrix4x4 matrix);

		// Token: 0x060003E1 RID: 993
		[MethodImpl(4096)]
		private extern void SetStereoViewMatrix_Injected(Camera.StereoscopicEye eye, ref Matrix4x4 matrix);

		// Token: 0x040001F8 RID: 504
		public static Camera.CameraCallback onPreCull;

		// Token: 0x040001F9 RID: 505
		public static Camera.CameraCallback onPreRender;

		// Token: 0x040001FA RID: 506
		public static Camera.CameraCallback onPostRender;

		// Token: 0x020000AC RID: 172
		internal enum ProjectionMatrixMode
		{
			// Token: 0x040001FC RID: 508
			Explicit,
			// Token: 0x040001FD RID: 509
			Implicit,
			// Token: 0x040001FE RID: 510
			PhysicalPropertiesBased
		}

		// Token: 0x020000AD RID: 173
		public enum GateFitMode
		{
			// Token: 0x04000200 RID: 512
			Vertical = 1,
			// Token: 0x04000201 RID: 513
			Horizontal,
			// Token: 0x04000202 RID: 514
			Fill,
			// Token: 0x04000203 RID: 515
			Overscan,
			// Token: 0x04000204 RID: 516
			None = 0
		}

		// Token: 0x020000AE RID: 174
		public enum FieldOfViewAxis
		{
			// Token: 0x04000206 RID: 518
			Vertical,
			// Token: 0x04000207 RID: 519
			Horizontal
		}

		// Token: 0x020000AF RID: 175
		public struct GateFitParameters
		{
			// Token: 0x170000C2 RID: 194
			// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000616D File Offset: 0x0000436D
			// (set) Token: 0x060003E3 RID: 995 RVA: 0x00006175 File Offset: 0x00004375
			public Camera.GateFitMode mode { get; set; }

			// Token: 0x170000C3 RID: 195
			// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000617E File Offset: 0x0000437E
			// (set) Token: 0x060003E5 RID: 997 RVA: 0x00006186 File Offset: 0x00004386
			public float aspect { get; set; }

			// Token: 0x060003E6 RID: 998 RVA: 0x0000618F File Offset: 0x0000438F
			public GateFitParameters(Camera.GateFitMode mode, float aspect)
			{
				this.mode = mode;
				this.aspect = aspect;
			}
		}

		// Token: 0x020000B0 RID: 176
		public enum StereoscopicEye
		{
			// Token: 0x0400020B RID: 523
			Left,
			// Token: 0x0400020C RID: 524
			Right
		}

		// Token: 0x020000B1 RID: 177
		public enum MonoOrStereoscopicEye
		{
			// Token: 0x0400020E RID: 526
			Left,
			// Token: 0x0400020F RID: 527
			Right,
			// Token: 0x04000210 RID: 528
			Mono
		}

		// Token: 0x020000B2 RID: 178
		// (Invoke) Token: 0x060003E8 RID: 1000
		public delegate void CameraCallback(Camera cam);
	}
}
