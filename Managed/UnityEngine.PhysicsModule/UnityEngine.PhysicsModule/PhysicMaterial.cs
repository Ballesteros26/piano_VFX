using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000017 RID: 23
	[NativeHeader("Modules/Physics/PhysicMaterial.h")]
	public class PhysicMaterial : Object
	{
		// Token: 0x06000050 RID: 80 RVA: 0x0000278A File Offset: 0x0000098A
		public PhysicMaterial()
		{
			PhysicMaterial.Internal_CreateDynamicsMaterial(this, "DynamicMaterial");
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000027A0 File Offset: 0x000009A0
		public PhysicMaterial(string name)
		{
			PhysicMaterial.Internal_CreateDynamicsMaterial(this, name);
		}

		// Token: 0x06000052 RID: 82
		[MethodImpl(4096)]
		private static extern void Internal_CreateDynamicsMaterial([Writable] PhysicMaterial mat, string name);

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000053 RID: 83
		// (set) Token: 0x06000054 RID: 84
		public extern float bounciness
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000055 RID: 85
		// (set) Token: 0x06000056 RID: 86
		public extern float dynamicFriction
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000057 RID: 87
		// (set) Token: 0x06000058 RID: 88
		public extern float staticFriction
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000059 RID: 89
		// (set) Token: 0x0600005A RID: 90
		public extern PhysicMaterialCombine frictionCombine
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600005B RID: 91
		// (set) Token: 0x0600005C RID: 92
		public extern PhysicMaterialCombine bounceCombine
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000027B4 File Offset: 0x000009B4
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000027CC File Offset: 0x000009CC
		[Obsolete("Use PhysicMaterial.bounciness instead (UnityUpgradable) -> bounciness")]
		public float bouncyness
		{
			get
			{
				return this.bounciness;
			}
			set
			{
				this.bounciness = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000027D8 File Offset: 0x000009D8
		// (set) Token: 0x06000060 RID: 96 RVA: 0x0000213F File Offset: 0x0000033F
		[EditorBrowsable(1)]
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public Vector3 frictionDirection2
		{
			get
			{
				return Vector3.zero;
			}
			set
			{
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000027F0 File Offset: 0x000009F0
		// (set) Token: 0x06000062 RID: 98 RVA: 0x0000213F File Offset: 0x0000033F
		[EditorBrowsable(1)]
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public float dynamicFriction2
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002808 File Offset: 0x00000A08
		// (set) Token: 0x06000064 RID: 100 RVA: 0x0000213F File Offset: 0x0000033F
		[EditorBrowsable(1)]
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		public float staticFriction2
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002820 File Offset: 0x00000A20
		// (set) Token: 0x06000066 RID: 102 RVA: 0x0000213F File Offset: 0x0000033F
		[Obsolete("Anisotropic friction is no longer supported since Unity 5.0.", true)]
		[EditorBrowsable(1)]
		public Vector3 frictionDirection
		{
			get
			{
				return Vector3.zero;
			}
			set
			{
			}
		}
	}
}
