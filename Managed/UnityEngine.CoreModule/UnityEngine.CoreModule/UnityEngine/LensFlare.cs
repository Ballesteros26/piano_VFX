using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000FF RID: 255
	[NativeHeader("Runtime/Camera/Flare.h")]
	public sealed class LensFlare : Behaviour
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000B38 RID: 2872
		// (set) Token: 0x06000B39 RID: 2873
		public extern float brightness
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000B3A RID: 2874
		// (set) Token: 0x06000B3B RID: 2875
		public extern float fadeSpeed
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0000F384 File Offset: 0x0000D584
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x0000F39A File Offset: 0x0000D59A
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

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000B3E RID: 2878
		// (set) Token: 0x06000B3F RID: 2879
		public extern Flare flare
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000B41 RID: 2881
		[MethodImpl(4096)]
		private extern void get_color_Injected(out Color ret);

		// Token: 0x06000B42 RID: 2882
		[MethodImpl(4096)]
		private extern void set_color_Injected(ref Color value);
	}
}
