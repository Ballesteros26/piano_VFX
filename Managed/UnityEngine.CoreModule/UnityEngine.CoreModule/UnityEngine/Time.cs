using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001ED RID: 493
	[StaticAccessor("GetTimeManager()", StaticAccessorType.Dot)]
	[NativeHeader("Runtime/Input/TimeManager.h")]
	public class Time
	{
		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060015D2 RID: 5586
		[NativeProperty("CurTime")]
		public static extern float time
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060015D3 RID: 5587
		[NativeProperty("TimeSinceSceneLoad")]
		public static extern float timeSinceLevelLoad
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060015D4 RID: 5588
		public static extern float deltaTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x060015D5 RID: 5589
		public static extern float fixedTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x060015D6 RID: 5590
		public static extern float unscaledTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060015D7 RID: 5591
		public static extern float fixedUnscaledTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060015D8 RID: 5592
		public static extern float unscaledDeltaTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060015D9 RID: 5593
		public static extern float fixedUnscaledDeltaTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060015DA RID: 5594
		// (set) Token: 0x060015DB RID: 5595
		public static extern float fixedDeltaTime
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060015DC RID: 5596
		// (set) Token: 0x060015DD RID: 5597
		public static extern float maximumDeltaTime
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060015DE RID: 5598
		public static extern float smoothDeltaTime
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060015DF RID: 5599
		// (set) Token: 0x060015E0 RID: 5600
		public static extern float maximumParticleDeltaTime
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060015E1 RID: 5601
		// (set) Token: 0x060015E2 RID: 5602
		public static extern float timeScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x060015E3 RID: 5603
		public static extern int frameCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060015E4 RID: 5604
		[NativeProperty("RenderFrameCount")]
		public static extern int renderedFrameCount
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060015E5 RID: 5605
		[NativeProperty("Realtime")]
		public static extern float realtimeSinceStartup
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x060015E6 RID: 5606
		// (set) Token: 0x060015E7 RID: 5607
		public static extern float captureDeltaTime
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x00023D74 File Offset: 0x00021F74
		// (set) Token: 0x060015E9 RID: 5609 RVA: 0x00023DA6 File Offset: 0x00021FA6
		public static int captureFramerate
		{
			get
			{
				return (Time.captureDeltaTime == 0f) ? 0 : ((int)Mathf.Round(1f / Time.captureDeltaTime));
			}
			set
			{
				Time.captureDeltaTime = ((value == 0) ? 0f : (1f / (float)value));
			}
		}

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x060015EA RID: 5610
		public static extern bool inFixedTimeStep
		{
			[NativeName("IsUsingFixedTimeStep")]
			[MethodImpl(4096)]
			get;
		}
	}
}
