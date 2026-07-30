using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000026 RID: 38
	[NativeHeader("Modules/Physics2D/AnchoredJoint2D.h")]
	public class AnchoredJoint2D : Joint2D
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600038C RID: 908 RVA: 0x000070B4 File Offset: 0x000052B4
		// (set) Token: 0x0600038D RID: 909 RVA: 0x000070CA File Offset: 0x000052CA
		public Vector2 anchor
		{
			get
			{
				Vector2 vector;
				this.get_anchor_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_anchor_Injected(ref value);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600038E RID: 910 RVA: 0x000070D4 File Offset: 0x000052D4
		// (set) Token: 0x0600038F RID: 911 RVA: 0x000070EA File Offset: 0x000052EA
		public Vector2 connectedAnchor
		{
			get
			{
				Vector2 vector;
				this.get_connectedAnchor_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_connectedAnchor_Injected(ref value);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000390 RID: 912
		// (set) Token: 0x06000391 RID: 913
		public extern bool autoConfigureConnectedAnchor
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x06000393 RID: 915
		[MethodImpl(4096)]
		private extern void get_anchor_Injected(out Vector2 ret);

		// Token: 0x06000394 RID: 916
		[MethodImpl(4096)]
		private extern void set_anchor_Injected(ref Vector2 value);

		// Token: 0x06000395 RID: 917
		[MethodImpl(4096)]
		private extern void get_connectedAnchor_Injected(out Vector2 ret);

		// Token: 0x06000396 RID: 918
		[MethodImpl(4096)]
		private extern void set_connectedAnchor_Injected(ref Vector2 value);
	}
}
