using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000038 RID: 56
	[NativeHeader("Modules/Physics2D/Public/PhysicsMaterial2D.h")]
	public sealed class PhysicsMaterial2D : Object
	{
		// Token: 0x0600046B RID: 1131 RVA: 0x00007295 File Offset: 0x00005495
		public PhysicsMaterial2D()
		{
			PhysicsMaterial2D.Create_Internal(this, null);
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x000072A7 File Offset: 0x000054A7
		public PhysicsMaterial2D(string name)
		{
			PhysicsMaterial2D.Create_Internal(this, name);
		}

		// Token: 0x0600046D RID: 1133
		[NativeMethod("Create_Binding")]
		[MethodImpl(4096)]
		private static extern void Create_Internal([Writable] PhysicsMaterial2D scriptMaterial, string name);

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600046E RID: 1134
		// (set) Token: 0x0600046F RID: 1135
		public extern float bounciness
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000470 RID: 1136
		// (set) Token: 0x06000471 RID: 1137
		public extern float friction
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}
	}
}
