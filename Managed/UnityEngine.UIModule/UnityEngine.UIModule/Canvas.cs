using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000008 RID: 8
	[NativeHeader("Modules/UI/Canvas.h")]
	[NativeClass("UI::Canvas")]
	[NativeHeader("Modules/UI/UIStructs.h")]
	[RequireComponent(typeof(RectTransform))]
	public sealed class Canvas : Behaviour
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000054 RID: 84 RVA: 0x00002958 File Offset: 0x00000B58
		// (remove) Token: 0x06000055 RID: 85 RVA: 0x0000298C File Offset: 0x00000B8C
		[field: DebuggerBrowsable(0)]
		public static event Canvas.WillRenderCanvases willRenderCanvases;

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000056 RID: 86
		// (set) Token: 0x06000057 RID: 87
		public extern RenderMode renderMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000058 RID: 88
		public extern bool isRootCanvas
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000029C0 File Offset: 0x00000BC0
		public Rect pixelRect
		{
			get
			{
				Rect rect;
				this.get_pixelRect_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600005A RID: 90
		// (set) Token: 0x0600005B RID: 91
		public extern float scaleFactor
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600005C RID: 92
		// (set) Token: 0x0600005D RID: 93
		public extern float referencePixelsPerUnit
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600005E RID: 94
		// (set) Token: 0x0600005F RID: 95
		public extern bool overridePixelPerfect
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000060 RID: 96
		// (set) Token: 0x06000061 RID: 97
		public extern bool pixelPerfect
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000062 RID: 98
		// (set) Token: 0x06000063 RID: 99
		public extern float planeDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000064 RID: 100
		public extern int renderOrder
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000065 RID: 101
		// (set) Token: 0x06000066 RID: 102
		public extern bool overrideSorting
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000067 RID: 103
		// (set) Token: 0x06000068 RID: 104
		public extern int sortingOrder
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000069 RID: 105
		// (set) Token: 0x0600006A RID: 106
		public extern int targetDisplay
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006B RID: 107
		// (set) Token: 0x0600006C RID: 108
		public extern int sortingLayerID
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006D RID: 109
		public extern int cachedSortingLayerValue
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006E RID: 110
		// (set) Token: 0x0600006F RID: 111
		public extern AdditionalCanvasShaderChannels additionalShaderChannels
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000070 RID: 112
		// (set) Token: 0x06000071 RID: 113
		public extern string sortingLayerName
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000072 RID: 114
		public extern Canvas rootCanvas
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000073 RID: 115
		// (set) Token: 0x06000074 RID: 116
		[NativeProperty("Camera", false, TargetType.Function)]
		public extern Camera worldCamera
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000075 RID: 117
		// (set) Token: 0x06000076 RID: 118
		[NativeProperty("SortingBucketNormalizedSize", false, TargetType.Function)]
		public extern float normalizedSortingGridSize
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000077 RID: 119
		// (set) Token: 0x06000078 RID: 120
		[NativeProperty("SortingBucketNormalizedSize", false, TargetType.Function)]
		[Obsolete("Setting normalizedSize via a int is not supported. Please use normalizedSortingGridSize", false)]
		public extern int sortingGridNormalizedSize
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000079 RID: 121
		[Obsolete("Shared default material now used for text and general UI elements, call Canvas.GetDefaultCanvasMaterial()", false)]
		[FreeFunction("UI::GetDefaultUIMaterial")]
		[MethodImpl(4096)]
		public static extern Material GetDefaultCanvasTextMaterial();

		// Token: 0x0600007A RID: 122
		[FreeFunction("UI::GetDefaultUIMaterial")]
		[MethodImpl(4096)]
		public static extern Material GetDefaultCanvasMaterial();

		// Token: 0x0600007B RID: 123
		[FreeFunction("UI::GetETC1SupportedCanvasMaterial")]
		[MethodImpl(4096)]
		public static extern Material GetETC1SupportedCanvasMaterial();

		// Token: 0x0600007C RID: 124 RVA: 0x000029D6 File Offset: 0x00000BD6
		public static void ForceUpdateCanvases()
		{
			Canvas.SendWillRenderCanvases();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000029DF File Offset: 0x00000BDF
		[RequiredByNativeCode]
		private static void SendWillRenderCanvases()
		{
			Canvas.WillRenderCanvases willRenderCanvases = Canvas.willRenderCanvases;
			if (willRenderCanvases != null)
			{
				willRenderCanvases();
			}
		}

		// Token: 0x0600007F RID: 127
		[MethodImpl(4096)]
		private extern void get_pixelRect_Injected(out Rect ret);

		// Token: 0x02000009 RID: 9
		// (Invoke) Token: 0x06000081 RID: 129
		public delegate void WillRenderCanvases();
	}
}
