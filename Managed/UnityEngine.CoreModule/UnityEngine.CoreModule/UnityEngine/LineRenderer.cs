using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000F4 RID: 244
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/LineRenderer.h")]
	public sealed class LineRenderer : Renderer
	{
		// Token: 0x0600086A RID: 2154 RVA: 0x0000C735 File Offset: 0x0000A935
		[Obsolete("Use startWidth, endWidth or widthCurve instead.", false)]
		public void SetWidth(float start, float end)
		{
			this.startWidth = start;
			this.endWidth = end;
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0000C748 File Offset: 0x0000A948
		[Obsolete("Use startColor, endColor or colorGradient instead.", false)]
		public void SetColors(Color start, Color end)
		{
			this.startColor = start;
			this.endColor = end;
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0000C75B File Offset: 0x0000A95B
		[Obsolete("Use positionCount instead.", false)]
		public void SetVertexCount(int count)
		{
			this.positionCount = count;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x0600086D RID: 2157 RVA: 0x0000C768 File Offset: 0x0000A968
		// (set) Token: 0x0600086E RID: 2158 RVA: 0x0000C75B File Offset: 0x0000A95B
		[Obsolete("Use positionCount instead (UnityUpgradable) -> positionCount", false)]
		public int numPositions
		{
			get
			{
				return this.positionCount;
			}
			set
			{
				this.positionCount = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x0600086F RID: 2159
		// (set) Token: 0x06000870 RID: 2160
		public extern float startWidth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000871 RID: 2161
		// (set) Token: 0x06000872 RID: 2162
		public extern float endWidth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000873 RID: 2163
		// (set) Token: 0x06000874 RID: 2164
		public extern float widthMultiplier
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000875 RID: 2165
		// (set) Token: 0x06000876 RID: 2166
		public extern int numCornerVertices
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000877 RID: 2167
		// (set) Token: 0x06000878 RID: 2168
		public extern int numCapVertices
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000879 RID: 2169
		// (set) Token: 0x0600087A RID: 2170
		public extern bool useWorldSpace
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x0600087B RID: 2171
		// (set) Token: 0x0600087C RID: 2172
		public extern bool loop
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x0600087D RID: 2173 RVA: 0x0000C780 File Offset: 0x0000A980
		// (set) Token: 0x0600087E RID: 2174 RVA: 0x0000C796 File Offset: 0x0000A996
		public Color startColor
		{
			get
			{
				Color color;
				this.get_startColor_Injected(out color);
				return color;
			}
			set
			{
				this.set_startColor_Injected(ref value);
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x0000C7B6 File Offset: 0x0000A9B6
		public Color endColor
		{
			get
			{
				Color color;
				this.get_endColor_Injected(out color);
				return color;
			}
			set
			{
				this.set_endColor_Injected(ref value);
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000881 RID: 2177
		// (set) Token: 0x06000882 RID: 2178
		[NativeProperty("PositionsCount")]
		public extern int positionCount
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x0000C7C0 File Offset: 0x0000A9C0
		public void SetPosition(int index, Vector3 position)
		{
			this.SetPosition_Injected(index, ref position);
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x0000C7CC File Offset: 0x0000A9CC
		public Vector3 GetPosition(int index)
		{
			Vector3 vector;
			this.GetPosition_Injected(index, out vector);
			return vector;
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000885 RID: 2181
		// (set) Token: 0x06000886 RID: 2182
		public extern float shadowBias
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000887 RID: 2183
		// (set) Token: 0x06000888 RID: 2184
		public extern bool generateLightingData
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000889 RID: 2185
		// (set) Token: 0x0600088A RID: 2186
		public extern LineTextureMode textureMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x0600088B RID: 2187
		// (set) Token: 0x0600088C RID: 2188
		public extern LineAlignment alignment
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x0600088D RID: 2189
		[MethodImpl(4096)]
		public extern void Simplify(float tolerance);

		// Token: 0x0600088E RID: 2190 RVA: 0x0000C7E3 File Offset: 0x0000A9E3
		public void BakeMesh(Mesh mesh, bool useTransform = false)
		{
			this.BakeMesh(mesh, Camera.main, useTransform);
		}

		// Token: 0x0600088F RID: 2191
		[MethodImpl(4096)]
		public extern void BakeMesh([NotNull] Mesh mesh, [NotNull] Camera camera, bool useTransform = false);

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000890 RID: 2192 RVA: 0x0000C7F4 File Offset: 0x0000A9F4
		// (set) Token: 0x06000891 RID: 2193 RVA: 0x0000C80C File Offset: 0x0000AA0C
		public AnimationCurve widthCurve
		{
			get
			{
				return this.GetWidthCurveCopy();
			}
			set
			{
				this.SetWidthCurve(value);
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000892 RID: 2194 RVA: 0x0000C818 File Offset: 0x0000AA18
		// (set) Token: 0x06000893 RID: 2195 RVA: 0x0000C830 File Offset: 0x0000AA30
		public Gradient colorGradient
		{
			get
			{
				return this.GetColorGradientCopy();
			}
			set
			{
				this.SetColorGradient(value);
			}
		}

		// Token: 0x06000894 RID: 2196
		[MethodImpl(4096)]
		private extern AnimationCurve GetWidthCurveCopy();

		// Token: 0x06000895 RID: 2197
		[MethodImpl(4096)]
		private extern void SetWidthCurve([NotNull] AnimationCurve curve);

		// Token: 0x06000896 RID: 2198
		[MethodImpl(4096)]
		private extern Gradient GetColorGradientCopy();

		// Token: 0x06000897 RID: 2199
		[MethodImpl(4096)]
		private extern void SetColorGradient([NotNull] Gradient curve);

		// Token: 0x06000898 RID: 2200
		[FreeFunction(Name = "LineRendererScripting::GetPositions", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern int GetPositions([NotNull] [Out] Vector3[] positions);

		// Token: 0x06000899 RID: 2201
		[FreeFunction(Name = "LineRendererScripting::SetPositions", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetPositions([NotNull] Vector3[] positions);

		// Token: 0x0600089B RID: 2203
		[MethodImpl(4096)]
		private extern void get_startColor_Injected(out Color ret);

		// Token: 0x0600089C RID: 2204
		[MethodImpl(4096)]
		private extern void set_startColor_Injected(ref Color value);

		// Token: 0x0600089D RID: 2205
		[MethodImpl(4096)]
		private extern void get_endColor_Injected(out Color ret);

		// Token: 0x0600089E RID: 2206
		[MethodImpl(4096)]
		private extern void set_endColor_Injected(ref Color value);

		// Token: 0x0600089F RID: 2207
		[MethodImpl(4096)]
		private extern void SetPosition_Injected(int index, ref Vector3 position);

		// Token: 0x060008A0 RID: 2208
		[MethodImpl(4096)]
		private extern void GetPosition_Injected(int index, out Vector3 ret);
	}
}
