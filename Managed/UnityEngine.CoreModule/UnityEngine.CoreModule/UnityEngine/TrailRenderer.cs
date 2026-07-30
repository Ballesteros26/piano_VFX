using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000F3 RID: 243
	[NativeHeader("Runtime/Graphics/GraphicsScriptBindings.h")]
	[NativeHeader("Runtime/Graphics/TrailRenderer.h")]
	public sealed class TrailRenderer : Renderer
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000831 RID: 2097 RVA: 0x0000C658 File Offset: 0x0000A858
		[Obsolete("Use positionCount instead (UnityUpgradable) -> positionCount", false)]
		public int numPositions
		{
			get
			{
				return this.positionCount;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x06000832 RID: 2098
		// (set) Token: 0x06000833 RID: 2099
		public extern float time
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000834 RID: 2100
		// (set) Token: 0x06000835 RID: 2101
		public extern float startWidth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000836 RID: 2102
		// (set) Token: 0x06000837 RID: 2103
		public extern float endWidth
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000838 RID: 2104
		// (set) Token: 0x06000839 RID: 2105
		public extern float widthMultiplier
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x0600083A RID: 2106
		// (set) Token: 0x0600083B RID: 2107
		public extern bool autodestruct
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x0600083C RID: 2108
		// (set) Token: 0x0600083D RID: 2109
		public extern bool emitting
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x0600083E RID: 2110
		// (set) Token: 0x0600083F RID: 2111
		public extern int numCornerVertices
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000840 RID: 2112
		// (set) Token: 0x06000841 RID: 2113
		public extern int numCapVertices
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000842 RID: 2114
		// (set) Token: 0x06000843 RID: 2115
		public extern float minVertexDistance
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0000C670 File Offset: 0x0000A870
		// (set) Token: 0x06000845 RID: 2117 RVA: 0x0000C686 File Offset: 0x0000A886
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

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0000C690 File Offset: 0x0000A890
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x0000C6A6 File Offset: 0x0000A8A6
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

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000848 RID: 2120
		[NativeProperty("PositionsCount")]
		public extern int positionCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0000C6B0 File Offset: 0x0000A8B0
		public void SetPosition(int index, Vector3 position)
		{
			this.SetPosition_Injected(index, ref position);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0000C6BC File Offset: 0x0000A8BC
		public Vector3 GetPosition(int index)
		{
			Vector3 vector;
			this.GetPosition_Injected(index, out vector);
			return vector;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600084B RID: 2123
		// (set) Token: 0x0600084C RID: 2124
		public extern float shadowBias
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600084D RID: 2125
		// (set) Token: 0x0600084E RID: 2126
		public extern bool generateLightingData
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600084F RID: 2127
		// (set) Token: 0x06000850 RID: 2128
		public extern LineTextureMode textureMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000851 RID: 2129
		// (set) Token: 0x06000852 RID: 2130
		public extern LineAlignment alignment
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000853 RID: 2131
		[MethodImpl(4096)]
		public extern void Clear();

		// Token: 0x06000854 RID: 2132 RVA: 0x0000C6D3 File Offset: 0x0000A8D3
		public void BakeMesh(Mesh mesh, bool useTransform = false)
		{
			this.BakeMesh(mesh, Camera.main, useTransform);
		}

		// Token: 0x06000855 RID: 2133
		[MethodImpl(4096)]
		public extern void BakeMesh([NotNull] Mesh mesh, [NotNull] Camera camera, bool useTransform = false);

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x0000C6E4 File Offset: 0x0000A8E4
		// (set) Token: 0x06000857 RID: 2135 RVA: 0x0000C6FC File Offset: 0x0000A8FC
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

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0000C708 File Offset: 0x0000A908
		// (set) Token: 0x06000859 RID: 2137 RVA: 0x0000C720 File Offset: 0x0000A920
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

		// Token: 0x0600085A RID: 2138
		[MethodImpl(4096)]
		private extern AnimationCurve GetWidthCurveCopy();

		// Token: 0x0600085B RID: 2139
		[MethodImpl(4096)]
		private extern void SetWidthCurve([NotNull] AnimationCurve curve);

		// Token: 0x0600085C RID: 2140
		[MethodImpl(4096)]
		private extern Gradient GetColorGradientCopy();

		// Token: 0x0600085D RID: 2141
		[MethodImpl(4096)]
		private extern void SetColorGradient([NotNull] Gradient curve);

		// Token: 0x0600085E RID: 2142
		[FreeFunction(Name = "TrailRendererScripting::GetPositions", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern int GetPositions([NotNull] [Out] Vector3[] positions);

		// Token: 0x0600085F RID: 2143
		[FreeFunction(Name = "TrailRendererScripting::SetPositions", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void SetPositions([NotNull] Vector3[] positions);

		// Token: 0x06000860 RID: 2144 RVA: 0x0000C72B File Offset: 0x0000A92B
		[FreeFunction(Name = "TrailRendererScripting::AddPosition", HasExplicitThis = true)]
		public void AddPosition(Vector3 position)
		{
			this.AddPosition_Injected(ref position);
		}

		// Token: 0x06000861 RID: 2145
		[FreeFunction(Name = "TrailRendererScripting::AddPositions", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void AddPositions([NotNull] Vector3[] positions);

		// Token: 0x06000863 RID: 2147
		[MethodImpl(4096)]
		private extern void get_startColor_Injected(out Color ret);

		// Token: 0x06000864 RID: 2148
		[MethodImpl(4096)]
		private extern void set_startColor_Injected(ref Color value);

		// Token: 0x06000865 RID: 2149
		[MethodImpl(4096)]
		private extern void get_endColor_Injected(out Color ret);

		// Token: 0x06000866 RID: 2150
		[MethodImpl(4096)]
		private extern void set_endColor_Injected(ref Color value);

		// Token: 0x06000867 RID: 2151
		[MethodImpl(4096)]
		private extern void SetPosition_Injected(int index, ref Vector3 position);

		// Token: 0x06000868 RID: 2152
		[MethodImpl(4096)]
		private extern void GetPosition_Injected(int index, out Vector3 ret);

		// Token: 0x06000869 RID: 2153
		[MethodImpl(4096)]
		private extern void AddPosition_Injected(ref Vector3 position);
	}
}
