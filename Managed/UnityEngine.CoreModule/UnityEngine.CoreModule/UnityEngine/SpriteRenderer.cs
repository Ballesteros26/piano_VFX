using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000200 RID: 512
	[RequireComponent(typeof(Transform))]
	[NativeType("Runtime/Graphics/Mesh/SpriteRenderer.h")]
	public sealed class SpriteRenderer : Renderer
	{
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060016DA RID: 5850
		internal extern bool shouldSupportTiling
		{
			[NativeMethod("ShouldSupportTiling")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x060016DB RID: 5851
		// (set) Token: 0x060016DC RID: 5852
		public extern Sprite sprite
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x060016DD RID: 5853
		// (set) Token: 0x060016DE RID: 5854
		public extern SpriteDrawMode drawMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x00025388 File Offset: 0x00023588
		// (set) Token: 0x060016E0 RID: 5856 RVA: 0x0002539E File Offset: 0x0002359E
		public Vector2 size
		{
			get
			{
				Vector2 vector;
				this.get_size_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x060016E1 RID: 5857
		// (set) Token: 0x060016E2 RID: 5858
		public extern float adaptiveModeThreshold
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060016E3 RID: 5859
		// (set) Token: 0x060016E4 RID: 5860
		public extern SpriteTileMode tileMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x000253A8 File Offset: 0x000235A8
		// (set) Token: 0x060016E6 RID: 5862 RVA: 0x000253BE File Offset: 0x000235BE
		public Color color
		{
			get
			{
				Color color;
				this.get_color_Injected(out color);
				return color;
			}
			set
			{
				this.set_color_Injected(ref value);
			}
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060016E7 RID: 5863
		// (set) Token: 0x060016E8 RID: 5864
		public extern SpriteMaskInteraction maskInteraction
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060016E9 RID: 5865
		// (set) Token: 0x060016EA RID: 5866
		public extern bool flipX
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060016EB RID: 5867
		// (set) Token: 0x060016EC RID: 5868
		public extern bool flipY
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060016ED RID: 5869
		// (set) Token: 0x060016EE RID: 5870
		public extern SpriteSortPoint spriteSortPoint
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060016EF RID: 5871 RVA: 0x000253C8 File Offset: 0x000235C8
		[NativeMethod(Name = "GetSpriteBounds")]
		internal Bounds Internal_GetSpriteBounds(SpriteDrawMode mode)
		{
			Bounds bounds;
			this.Internal_GetSpriteBounds_Injected(mode, out bounds);
			return bounds;
		}

		// Token: 0x060016F0 RID: 5872 RVA: 0x000253E0 File Offset: 0x000235E0
		internal Bounds GetSpriteBounds()
		{
			return this.Internal_GetSpriteBounds(this.drawMode);
		}

		// Token: 0x060016F2 RID: 5874
		[MethodImpl(4096)]
		private extern void get_size_Injected(out Vector2 ret);

		// Token: 0x060016F3 RID: 5875
		[MethodImpl(4096)]
		private extern void set_size_Injected(ref Vector2 value);

		// Token: 0x060016F4 RID: 5876
		[MethodImpl(4096)]
		private extern void get_color_Injected(out Color ret);

		// Token: 0x060016F5 RID: 5877
		[MethodImpl(4096)]
		private extern void set_color_Injected(ref Color value);

		// Token: 0x060016F6 RID: 5878
		[MethodImpl(4096)]
		private extern void Internal_GetSpriteBounds_Injected(SpriteDrawMode mode, out Bounds ret);
	}
}
