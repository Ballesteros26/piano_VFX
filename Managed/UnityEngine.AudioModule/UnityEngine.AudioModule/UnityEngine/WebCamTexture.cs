using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000022 RID: 34
	[NativeHeader("Runtime/Video/BaseWebCamTexture.h")]
	[NativeHeader("Runtime/Video/ScriptBindings/WebCamTexture.bindings.h")]
	[NativeHeader("AudioScriptingClasses.h")]
	public sealed class WebCamTexture : Texture
	{
		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000150 RID: 336
		public static extern WebCamDevice[] devices
		{
			[NativeName("Internal_GetDevices")]
			[StaticAccessor("WebCamTextureBindings", StaticAccessorType.DoubleColon)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00002D60 File Offset: 0x00000F60
		public WebCamTexture(string deviceName, int requestedWidth, int requestedHeight, int requestedFPS)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, requestedWidth, requestedHeight, requestedFPS);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00002D76 File Offset: 0x00000F76
		public WebCamTexture(string deviceName, int requestedWidth, int requestedHeight)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, requestedWidth, requestedHeight, 0);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00002D8B File Offset: 0x00000F8B
		public WebCamTexture(string deviceName)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, deviceName, 0, 0, 0);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00002DA0 File Offset: 0x00000FA0
		public WebCamTexture(int requestedWidth, int requestedHeight, int requestedFPS)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, "", requestedWidth, requestedHeight, requestedFPS);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00002DB9 File Offset: 0x00000FB9
		public WebCamTexture(int requestedWidth, int requestedHeight)
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, "", requestedWidth, requestedHeight, 0);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00002DD2 File Offset: 0x00000FD2
		public WebCamTexture()
		{
			WebCamTexture.Internal_CreateWebCamTexture(this, "", 0, 0, 0);
		}

		// Token: 0x06000157 RID: 343
		[MethodImpl(4096)]
		public extern void Play();

		// Token: 0x06000158 RID: 344
		[MethodImpl(4096)]
		public extern void Pause();

		// Token: 0x06000159 RID: 345
		[MethodImpl(4096)]
		public extern void Stop();

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600015A RID: 346
		public extern bool isPlaying
		{
			[NativeName("IsPlaying")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600015B RID: 347
		// (set) Token: 0x0600015C RID: 348
		[NativeName("Device")]
		public extern string deviceName
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600015D RID: 349
		// (set) Token: 0x0600015E RID: 350
		public extern float requestedFPS
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600015F RID: 351
		// (set) Token: 0x06000160 RID: 352
		public extern int requestedWidth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000161 RID: 353
		// (set) Token: 0x06000162 RID: 354
		public extern int requestedHeight
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000163 RID: 355
		public extern int videoRotationAngle
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000164 RID: 356
		public extern bool videoVerticallyMirrored
		{
			[NativeName("IsVideoVerticallyMirrored")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000165 RID: 357
		public extern bool didUpdateThisFrame
		{
			[NativeName("DidUpdateThisFrame")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00002DEC File Offset: 0x00000FEC
		public Color GetPixel(int x, int y)
		{
			Color color;
			this.GetPixel_Injected(x, y, out color);
			return color;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00002E04 File Offset: 0x00001004
		public Color[] GetPixels()
		{
			return this.GetPixels(0, 0, this.width, this.height);
		}

		// Token: 0x06000168 RID: 360
		[FreeFunction("WebCamTextureBindings::Internal_GetPixels", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern Color[] GetPixels(int x, int y, int blockWidth, int blockHeight);

		// Token: 0x06000169 RID: 361 RVA: 0x00002E2C File Offset: 0x0000102C
		[ExcludeFromDocs]
		public Color32[] GetPixels32()
		{
			return this.GetPixels32(null);
		}

		// Token: 0x0600016A RID: 362
		[FreeFunction("WebCamTextureBindings::Internal_GetPixels32", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern Color32[] GetPixels32([DefaultValue("null")] Color32[] colors);

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00002E48 File Offset: 0x00001048
		// (set) Token: 0x0600016C RID: 364 RVA: 0x00002E82 File Offset: 0x00001082
		public Vector2? autoFocusPoint
		{
			get
			{
				return (this.internalAutoFocusPoint.x < 0f) ? default(Vector2?) : new Vector2?(this.internalAutoFocusPoint);
			}
			set
			{
				this.internalAutoFocusPoint = ((value == null) ? new Vector2(-1f, -1f) : value.Value);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00002EB0 File Offset: 0x000010B0
		// (set) Token: 0x0600016E RID: 366 RVA: 0x00002EC6 File Offset: 0x000010C6
		internal Vector2 internalAutoFocusPoint
		{
			get
			{
				Vector2 vector;
				this.get_internalAutoFocusPoint_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_internalAutoFocusPoint_Injected(ref value);
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600016F RID: 367
		public extern bool isDepth
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000170 RID: 368
		[StaticAccessor("WebCamTextureBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(4096)]
		private static extern void Internal_CreateWebCamTexture([Writable] WebCamTexture self, string scriptingDevice, int requestedWidth, int requestedHeight, int maxFramerate);

		// Token: 0x06000171 RID: 369
		[MethodImpl(4096)]
		private extern void GetPixel_Injected(int x, int y, out Color ret);

		// Token: 0x06000172 RID: 370
		[MethodImpl(4096)]
		private extern void get_internalAutoFocusPoint_Injected(out Vector2 ret);

		// Token: 0x06000173 RID: 371
		[MethodImpl(4096)]
		private extern void set_internalAutoFocusPoint_Injected(ref Vector2 value);
	}
}
