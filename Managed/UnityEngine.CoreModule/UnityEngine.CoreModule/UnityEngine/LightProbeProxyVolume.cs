using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000131 RID: 305
	[NativeHeader("Runtime/Camera/LightProbeProxyVolume.h")]
	public sealed class LightProbeProxyVolume : Behaviour
	{
		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000BBC RID: 3004
		public static extern bool isFeatureSupported
		{
			[NativeName("IsFeatureSupported")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x0000F504 File Offset: 0x0000D704
		[NativeName("GlobalAABB")]
		public Bounds boundsGlobal
		{
			get
			{
				Bounds bounds;
				this.get_boundsGlobal_Injected(out bounds);
				return bounds;
			}
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0000F51C File Offset: 0x0000D71C
		// (set) Token: 0x06000BBF RID: 3007 RVA: 0x0000F532 File Offset: 0x0000D732
		[NativeName("BoundingBoxSizeCustom")]
		public Vector3 sizeCustom
		{
			get
			{
				Vector3 vector;
				this.get_sizeCustom_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_sizeCustom_Injected(ref value);
			}
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x0000F53C File Offset: 0x0000D73C
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x0000F552 File Offset: 0x0000D752
		[NativeName("BoundingBoxOriginCustom")]
		public Vector3 originCustom
		{
			get
			{
				Vector3 vector;
				this.get_originCustom_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_originCustom_Injected(ref value);
			}
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000BC2 RID: 3010
		// (set) Token: 0x06000BC3 RID: 3011
		public extern float probeDensity
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000BC4 RID: 3012
		// (set) Token: 0x06000BC5 RID: 3013
		public extern int gridResolutionX
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000BC6 RID: 3014
		// (set) Token: 0x06000BC7 RID: 3015
		public extern int gridResolutionY
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000BC8 RID: 3016
		// (set) Token: 0x06000BC9 RID: 3017
		public extern int gridResolutionZ
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000BCA RID: 3018
		// (set) Token: 0x06000BCB RID: 3019
		public extern LightProbeProxyVolume.BoundingBoxMode boundingBoxMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000BCC RID: 3020
		// (set) Token: 0x06000BCD RID: 3021
		public extern LightProbeProxyVolume.ResolutionMode resolutionMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000BCE RID: 3022
		// (set) Token: 0x06000BCF RID: 3023
		public extern LightProbeProxyVolume.ProbePositionMode probePositionMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000BD0 RID: 3024
		// (set) Token: 0x06000BD1 RID: 3025
		public extern LightProbeProxyVolume.RefreshMode refreshMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000BD2 RID: 3026
		// (set) Token: 0x06000BD3 RID: 3027
		public extern LightProbeProxyVolume.QualityMode qualityMode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0000F55C File Offset: 0x0000D75C
		public void Update()
		{
			this.SetDirtyFlag(true);
		}

		// Token: 0x06000BD5 RID: 3029
		[MethodImpl(4096)]
		private extern void SetDirtyFlag(bool flag);

		// Token: 0x06000BD7 RID: 3031
		[MethodImpl(4096)]
		private extern void get_boundsGlobal_Injected(out Bounds ret);

		// Token: 0x06000BD8 RID: 3032
		[MethodImpl(4096)]
		private extern void get_sizeCustom_Injected(out Vector3 ret);

		// Token: 0x06000BD9 RID: 3033
		[MethodImpl(4096)]
		private extern void set_sizeCustom_Injected(ref Vector3 value);

		// Token: 0x06000BDA RID: 3034
		[MethodImpl(4096)]
		private extern void get_originCustom_Injected(out Vector3 ret);

		// Token: 0x06000BDB RID: 3035
		[MethodImpl(4096)]
		private extern void set_originCustom_Injected(ref Vector3 value);

		// Token: 0x02000132 RID: 306
		public enum ResolutionMode
		{
			// Token: 0x040003ED RID: 1005
			Automatic,
			// Token: 0x040003EE RID: 1006
			Custom
		}

		// Token: 0x02000133 RID: 307
		public enum BoundingBoxMode
		{
			// Token: 0x040003F0 RID: 1008
			AutomaticLocal,
			// Token: 0x040003F1 RID: 1009
			AutomaticWorld,
			// Token: 0x040003F2 RID: 1010
			Custom
		}

		// Token: 0x02000134 RID: 308
		public enum ProbePositionMode
		{
			// Token: 0x040003F4 RID: 1012
			CellCorner,
			// Token: 0x040003F5 RID: 1013
			CellCenter
		}

		// Token: 0x02000135 RID: 309
		public enum RefreshMode
		{
			// Token: 0x040003F7 RID: 1015
			Automatic,
			// Token: 0x040003F8 RID: 1016
			EveryFrame,
			// Token: 0x040003F9 RID: 1017
			ViaScripting
		}

		// Token: 0x02000136 RID: 310
		public enum QualityMode
		{
			// Token: 0x040003FB RID: 1019
			Low,
			// Token: 0x040003FC RID: 1020
			Normal
		}
	}
}
