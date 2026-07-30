using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000018 RID: 24
	[NativeConditional("ENABLE_XR")]
	[NativeHeader("Modules/XR/XRPrefix.h")]
	[UsedByNativeCode]
	[NativeType(Header = "Modules/XR/Subsystems/Display/XRDisplaySubsystem.h")]
	public class XRDisplaySubsystem : IntegratedSubsystem<XRDisplaySubsystemDescriptor>
	{
		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060000DD RID: 221 RVA: 0x00004170 File Offset: 0x00002370
		// (remove) Token: 0x060000DE RID: 222 RVA: 0x000041A8 File Offset: 0x000023A8
		[field: DebuggerBrowsable(0)]
		public event Action<bool> displayFocusChanged;

		// Token: 0x060000DF RID: 223 RVA: 0x000041E0 File Offset: 0x000023E0
		[RequiredByNativeCode]
		private void InvokeDisplayFocusChanged(bool focus)
		{
			bool flag = this.displayFocusChanged != null;
			if (flag)
			{
				this.displayFocusChanged.Invoke(focus);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004208 File Offset: 0x00002408
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004228 File Offset: 0x00002428
		[Obsolete("singlePassRenderingDisabled{get;set;} is deprecated. Use textureLayout and supportedTextureLayouts instead.", false)]
		public bool singlePassRenderingDisabled
		{
			get
			{
				return (this.textureLayout & XRDisplaySubsystem.TextureLayout.Texture2DArray) == (XRDisplaySubsystem.TextureLayout)0;
			}
			set
			{
				if (value)
				{
					this.textureLayout = XRDisplaySubsystem.TextureLayout.SeparateTexture2Ds;
				}
				else
				{
					bool flag = (this.supportedTextureLayouts & XRDisplaySubsystem.TextureLayout.Texture2DArray) > (XRDisplaySubsystem.TextureLayout)0;
					if (flag)
					{
						this.textureLayout = XRDisplaySubsystem.TextureLayout.Texture2DArray;
					}
				}
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E2 RID: 226
		public extern bool displayOpaque
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E3 RID: 227
		// (set) Token: 0x060000E4 RID: 228
		public extern bool contentProtectionEnabled
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E5 RID: 229
		// (set) Token: 0x060000E6 RID: 230
		public extern float scaleOfAllViewports
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000E7 RID: 231
		// (set) Token: 0x060000E8 RID: 232
		public extern float scaleOfAllRenderTargets
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000E9 RID: 233
		// (set) Token: 0x060000EA RID: 234
		public extern float zNear
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000EB RID: 235
		// (set) Token: 0x060000EC RID: 236
		public extern float zFar
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000ED RID: 237
		// (set) Token: 0x060000EE RID: 238
		public extern bool sRGB
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000EF RID: 239
		// (set) Token: 0x060000F0 RID: 240
		public extern XRDisplaySubsystem.TextureLayout textureLayout
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000F1 RID: 241
		public extern XRDisplaySubsystem.TextureLayout supportedTextureLayouts
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F2 RID: 242
		// (set) Token: 0x060000F3 RID: 243
		public extern XRDisplaySubsystem.ReprojectionMode reprojectionMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004260 File Offset: 0x00002460
		public void SetFocusPlane(Vector3 point, Vector3 normal, Vector3 velocity)
		{
			this.SetFocusPlane_Injected(ref point, ref normal, ref velocity);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F5 RID: 245
		// (set) Token: 0x060000F6 RID: 246
		public extern bool disableLegacyRenderer
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060000F7 RID: 247
		[MethodImpl(4096)]
		public extern int GetRenderPassCount();

		// Token: 0x060000F8 RID: 248 RVA: 0x00004270 File Offset: 0x00002470
		public void GetRenderPass(int renderPassIndex, out XRDisplaySubsystem.XRRenderPass renderPass)
		{
			bool flag = !this.Internal_TryGetRenderPass(renderPassIndex, out renderPass);
			if (flag)
			{
				throw new IndexOutOfRangeException("renderPassIndex");
			}
		}

		// Token: 0x060000F9 RID: 249
		[NativeMethod("TryGetRenderPass")]
		[MethodImpl(4096)]
		private extern bool Internal_TryGetRenderPass(int renderPassIndex, out XRDisplaySubsystem.XRRenderPass renderPass);

		// Token: 0x060000FA RID: 250 RVA: 0x0000429C File Offset: 0x0000249C
		public void GetCullingParameters(Camera camera, int cullingPassIndex, out ScriptableCullingParameters scriptableCullingParameters)
		{
			bool flag = !this.Internal_TryGetCullingParams(camera, cullingPassIndex, out scriptableCullingParameters);
			if (!flag)
			{
				return;
			}
			bool flag2 = camera == null;
			if (flag2)
			{
				throw new ArgumentNullException("camera");
			}
			throw new IndexOutOfRangeException("cullingPassIndex");
		}

		// Token: 0x060000FB RID: 251
		[NativeHeader("Runtime/Graphics/ScriptableRenderLoop/ScriptableCulling.h")]
		[NativeMethod("TryGetCullingParams")]
		[MethodImpl(4096)]
		private extern bool Internal_TryGetCullingParams(Camera camera, int cullingPassIndex, out ScriptableCullingParameters scriptableCullingParameters);

		// Token: 0x060000FC RID: 252
		[NativeMethod("TryGetAppGPUTimeLastFrame")]
		[MethodImpl(4096)]
		public extern bool TryGetAppGPUTimeLastFrame(out float gpuTimeLastFrame);

		// Token: 0x060000FD RID: 253
		[NativeMethod("TryGetCompositorGPUTimeLastFrame")]
		[MethodImpl(4096)]
		public extern bool TryGetCompositorGPUTimeLastFrame(out float gpuTimeLastFrameCompositor);

		// Token: 0x060000FE RID: 254
		[NativeMethod("TryGetDroppedFrameCount")]
		[MethodImpl(4096)]
		public extern bool TryGetDroppedFrameCount(out int droppedFrameCount);

		// Token: 0x060000FF RID: 255
		[NativeMethod("TryGetFramePresentCount")]
		[MethodImpl(4096)]
		public extern bool TryGetFramePresentCount(out int framePresentCount);

		// Token: 0x06000100 RID: 256
		[NativeMethod("TryGetDisplayRefreshRate")]
		[MethodImpl(4096)]
		public extern bool TryGetDisplayRefreshRate(out float displayRefreshRate);

		// Token: 0x06000101 RID: 257
		[NativeMethod("TryGetMotionToPhoton")]
		[MethodImpl(4096)]
		public extern bool TryGetMotionToPhoton(out float motionToPhoton);

		// Token: 0x06000102 RID: 258
		[NativeMethod(Name = "GetPreferredMirrorViewBlitMode", IsThreadSafe = false)]
		[NativeConditional("ENABLE_XR")]
		[MethodImpl(4096)]
		public extern int GetPreferredMirrorBlitMode();

		// Token: 0x06000103 RID: 259 RVA: 0x000042E0 File Offset: 0x000024E0
		[Obsolete("GetMirrorViewBlitDesc(RenderTexture, out XRMirrorViewBlitDesc) is deprecated. Use GetMirrorViewBlitDesc(RenderTexture, out XRMirrorViewBlitDesc, int) instead.", false)]
		public bool GetMirrorViewBlitDesc(RenderTexture mirrorRt, out XRDisplaySubsystem.XRMirrorViewBlitDesc outDesc)
		{
			return this.GetMirrorViewBlitDesc(mirrorRt, out outDesc, -1);
		}

		// Token: 0x06000104 RID: 260
		[NativeMethod(Name = "QueryMirrorViewBlitDesc", IsThreadSafe = false)]
		[NativeConditional("ENABLE_XR")]
		[MethodImpl(4096)]
		public extern bool GetMirrorViewBlitDesc(RenderTexture mirrorRt, out XRDisplaySubsystem.XRMirrorViewBlitDesc outDesc, int mode);

		// Token: 0x06000105 RID: 261 RVA: 0x000042FC File Offset: 0x000024FC
		[Obsolete("AddGraphicsThreadMirrorViewBlit(CommandBuffer, bool) is deprecated. Use AddGraphicsThreadMirrorViewBlit(CommandBuffer, bool, int) instead.", false)]
		public bool AddGraphicsThreadMirrorViewBlit(CommandBuffer cmd, bool allowGraphicsStateInvalidate)
		{
			return this.AddGraphicsThreadMirrorViewBlit(cmd, allowGraphicsStateInvalidate, -1);
		}

		// Token: 0x06000106 RID: 262
		[NativeMethod(Name = "AddGraphicsThreadMirrorViewBlit", IsThreadSafe = false)]
		[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
		[NativeConditional("ENABLE_XR")]
		[MethodImpl(4096)]
		public extern bool AddGraphicsThreadMirrorViewBlit(CommandBuffer cmd, bool allowGraphicsStateInvalidate, int mode);

		// Token: 0x06000108 RID: 264
		[MethodImpl(4096)]
		private extern void SetFocusPlane_Injected(ref Vector3 point, ref Vector3 normal, ref Vector3 velocity);

		// Token: 0x02000019 RID: 25
		[Flags]
		public enum TextureLayout
		{
			// Token: 0x040000B2 RID: 178
			Texture2DArray = 1,
			// Token: 0x040000B3 RID: 179
			SingleTexture2D = 2,
			// Token: 0x040000B4 RID: 180
			SeparateTexture2Ds = 4
		}

		// Token: 0x0200001A RID: 26
		public enum ReprojectionMode
		{
			// Token: 0x040000B6 RID: 182
			Unspecified,
			// Token: 0x040000B7 RID: 183
			PositionAndOrientation,
			// Token: 0x040000B8 RID: 184
			OrientationOnly,
			// Token: 0x040000B9 RID: 185
			None
		}

		// Token: 0x0200001B RID: 27
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRRenderParameter
		{
			// Token: 0x040000BA RID: 186
			public Matrix4x4 view;

			// Token: 0x040000BB RID: 187
			public Matrix4x4 projection;

			// Token: 0x040000BC RID: 188
			public Rect viewport;

			// Token: 0x040000BD RID: 189
			public Mesh occlusionMesh;

			// Token: 0x040000BE RID: 190
			public int textureArraySlice;
		}

		// Token: 0x0200001C RID: 28
		[NativeHeader("Runtime/Graphics/CommandBuffer/RenderingCommandBuffer.h")]
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		[NativeHeader("Runtime/Graphics/RenderTextureDesc.h")]
		public struct XRRenderPass
		{
			// Token: 0x06000109 RID: 265 RVA: 0x00004320 File Offset: 0x00002520
			[NativeMethod(Name = "XRRenderPassScriptApi::GetRenderParameter", IsFreeFunction = true, HasExplicitThis = true, ThrowsException = true)]
			[NativeConditional("ENABLE_XR")]
			public void GetRenderParameter(Camera camera, int renderParameterIndex, out XRDisplaySubsystem.XRRenderParameter renderParameter)
			{
				XRDisplaySubsystem.XRRenderPass.GetRenderParameter_Injected(ref this, camera, renderParameterIndex, out renderParameter);
			}

			// Token: 0x0600010A RID: 266 RVA: 0x0000432B File Offset: 0x0000252B
			[NativeMethod(Name = "XRRenderPassScriptApi::GetRenderParameterCount", IsFreeFunction = true, HasExplicitThis = true)]
			[NativeConditional("ENABLE_XR")]
			public int GetRenderParameterCount()
			{
				return XRDisplaySubsystem.XRRenderPass.GetRenderParameterCount_Injected(ref this);
			}

			// Token: 0x0600010B RID: 267
			[MethodImpl(4096)]
			private static extern void GetRenderParameter_Injected(ref XRDisplaySubsystem.XRRenderPass _unity_self, Camera camera, int renderParameterIndex, out XRDisplaySubsystem.XRRenderParameter renderParameter);

			// Token: 0x0600010C RID: 268
			[MethodImpl(4096)]
			private static extern int GetRenderParameterCount_Injected(ref XRDisplaySubsystem.XRRenderPass _unity_self);

			// Token: 0x040000BF RID: 191
			private IntPtr displaySubsystemInstance;

			// Token: 0x040000C0 RID: 192
			public int renderPassIndex;

			// Token: 0x040000C1 RID: 193
			public RenderTargetIdentifier renderTarget;

			// Token: 0x040000C2 RID: 194
			public RenderTextureDescriptor renderTargetDesc;

			// Token: 0x040000C3 RID: 195
			public bool shouldFillOutDepth;

			// Token: 0x040000C4 RID: 196
			public int cullingPassIndex;
		}

		// Token: 0x0200001D RID: 29
		[NativeHeader("Runtime/Graphics/RenderTexture.h")]
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRBlitParams
		{
			// Token: 0x040000C5 RID: 197
			public RenderTexture srcTex;

			// Token: 0x040000C6 RID: 198
			public int srcTexArraySlice;

			// Token: 0x040000C7 RID: 199
			public Rect srcRect;

			// Token: 0x040000C8 RID: 200
			public Rect destRect;
		}

		// Token: 0x0200001E RID: 30
		[NativeHeader("Modules/XR/Subsystems/Display/XRDisplaySubsystem.bindings.h")]
		public struct XRMirrorViewBlitDesc
		{
			// Token: 0x0600010D RID: 269 RVA: 0x00004333 File Offset: 0x00002533
			[NativeConditional("ENABLE_XR")]
			[NativeMethod(Name = "XRMirrorViewBlitDescScriptApi::GetBlitParameter", IsFreeFunction = true, HasExplicitThis = true)]
			public void GetBlitParameter(int blitParameterIndex, out XRDisplaySubsystem.XRBlitParams blitParameter)
			{
				XRDisplaySubsystem.XRMirrorViewBlitDesc.GetBlitParameter_Injected(ref this, blitParameterIndex, out blitParameter);
			}

			// Token: 0x0600010E RID: 270
			[MethodImpl(4096)]
			private static extern void GetBlitParameter_Injected(ref XRDisplaySubsystem.XRMirrorViewBlitDesc _unity_self, int blitParameterIndex, out XRDisplaySubsystem.XRBlitParams blitParameter);

			// Token: 0x040000C9 RID: 201
			private IntPtr displaySubsystemInstance;

			// Token: 0x040000CA RID: 202
			public bool nativeBlitAvailable;

			// Token: 0x040000CB RID: 203
			public bool nativeBlitInvalidStates;

			// Token: 0x040000CC RID: 204
			public int blitParamsCount;
		}
	}
}
