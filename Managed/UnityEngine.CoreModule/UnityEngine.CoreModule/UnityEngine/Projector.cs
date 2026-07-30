using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000100 RID: 256
	[NativeHeader("Runtime/Camera/Projector.h")]
	public sealed class Projector : Behaviour
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000B43 RID: 2883
		// (set) Token: 0x06000B44 RID: 2884
		public extern float nearClipPlane
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000B45 RID: 2885
		// (set) Token: 0x06000B46 RID: 2886
		public extern float farClipPlane
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000B47 RID: 2887
		// (set) Token: 0x06000B48 RID: 2888
		public extern float fieldOfView
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000B49 RID: 2889
		// (set) Token: 0x06000B4A RID: 2890
		public extern float aspectRatio
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000B4B RID: 2891
		// (set) Token: 0x06000B4C RID: 2892
		public extern bool orthographic
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000B4D RID: 2893
		// (set) Token: 0x06000B4E RID: 2894
		public extern float orthographicSize
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000B4F RID: 2895
		// (set) Token: 0x06000B50 RID: 2896
		public extern int ignoreLayers
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000B51 RID: 2897
		// (set) Token: 0x06000B52 RID: 2898
		public extern Material material
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
