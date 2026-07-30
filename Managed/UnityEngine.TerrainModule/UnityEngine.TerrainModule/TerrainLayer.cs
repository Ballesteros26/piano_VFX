using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000012 RID: 18
	[NativeHeader("TerrainScriptingClasses.h")]
	[NativeHeader("Modules/Terrain/Public/TerrainLayerScriptingInterface.h")]
	[UsedByNativeCode]
	[StructLayout(0)]
	public sealed class TerrainLayer : Object
	{
		// Token: 0x06000142 RID: 322 RVA: 0x00003F4C File Offset: 0x0000214C
		public TerrainLayer()
		{
			TerrainLayer.Internal_Create(this);
		}

		// Token: 0x06000143 RID: 323
		[FreeFunction("TerrainLayerScriptingInterface::Create")]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] TerrainLayer layer);

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000144 RID: 324
		// (set) Token: 0x06000145 RID: 325
		public extern Texture2D diffuseTexture
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000146 RID: 326
		// (set) Token: 0x06000147 RID: 327
		public extern Texture2D normalMapTexture
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000148 RID: 328
		// (set) Token: 0x06000149 RID: 329
		public extern Texture2D maskMapTexture
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00003F60 File Offset: 0x00002160
		// (set) Token: 0x0600014B RID: 331 RVA: 0x00003F76 File Offset: 0x00002176
		public Vector2 tileSize
		{
			get
			{
				Vector2 vector;
				this.get_tileSize_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_tileSize_Injected(ref value);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00003F80 File Offset: 0x00002180
		// (set) Token: 0x0600014D RID: 333 RVA: 0x00003F96 File Offset: 0x00002196
		public Vector2 tileOffset
		{
			get
			{
				Vector2 vector;
				this.get_tileOffset_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_tileOffset_Injected(ref value);
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00003FA0 File Offset: 0x000021A0
		// (set) Token: 0x0600014F RID: 335 RVA: 0x00003FB6 File Offset: 0x000021B6
		[NativeProperty("SpecularColor")]
		public Color specular
		{
			get
			{
				Color color;
				this.get_specular_Injected(out color);
				return color;
			}
			set
			{
				this.set_specular_Injected(ref value);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000150 RID: 336
		// (set) Token: 0x06000151 RID: 337
		public extern float metallic
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000152 RID: 338
		// (set) Token: 0x06000153 RID: 339
		public extern float smoothness
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000154 RID: 340
		// (set) Token: 0x06000155 RID: 341
		public extern float normalScale
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00003FC0 File Offset: 0x000021C0
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00003FD6 File Offset: 0x000021D6
		public Vector4 diffuseRemapMin
		{
			get
			{
				Vector4 vector;
				this.get_diffuseRemapMin_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_diffuseRemapMin_Injected(ref value);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00003FE0 File Offset: 0x000021E0
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00003FF6 File Offset: 0x000021F6
		public Vector4 diffuseRemapMax
		{
			get
			{
				Vector4 vector;
				this.get_diffuseRemapMax_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_diffuseRemapMax_Injected(ref value);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00004000 File Offset: 0x00002200
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00004016 File Offset: 0x00002216
		public Vector4 maskMapRemapMin
		{
			get
			{
				Vector4 vector;
				this.get_maskMapRemapMin_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_maskMapRemapMin_Injected(ref value);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00004020 File Offset: 0x00002220
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00004036 File Offset: 0x00002236
		public Vector4 maskMapRemapMax
		{
			get
			{
				Vector4 vector;
				this.get_maskMapRemapMax_Injected(out vector);
				return vector;
			}
			set
			{
				this.set_maskMapRemapMax_Injected(ref value);
			}
		}

		// Token: 0x0600015E RID: 350
		[MethodImpl(4096)]
		private extern void get_tileSize_Injected(out Vector2 ret);

		// Token: 0x0600015F RID: 351
		[MethodImpl(4096)]
		private extern void set_tileSize_Injected(ref Vector2 value);

		// Token: 0x06000160 RID: 352
		[MethodImpl(4096)]
		private extern void get_tileOffset_Injected(out Vector2 ret);

		// Token: 0x06000161 RID: 353
		[MethodImpl(4096)]
		private extern void set_tileOffset_Injected(ref Vector2 value);

		// Token: 0x06000162 RID: 354
		[MethodImpl(4096)]
		private extern void get_specular_Injected(out Color ret);

		// Token: 0x06000163 RID: 355
		[MethodImpl(4096)]
		private extern void set_specular_Injected(ref Color value);

		// Token: 0x06000164 RID: 356
		[MethodImpl(4096)]
		private extern void get_diffuseRemapMin_Injected(out Vector4 ret);

		// Token: 0x06000165 RID: 357
		[MethodImpl(4096)]
		private extern void set_diffuseRemapMin_Injected(ref Vector4 value);

		// Token: 0x06000166 RID: 358
		[MethodImpl(4096)]
		private extern void get_diffuseRemapMax_Injected(out Vector4 ret);

		// Token: 0x06000167 RID: 359
		[MethodImpl(4096)]
		private extern void set_diffuseRemapMax_Injected(ref Vector4 value);

		// Token: 0x06000168 RID: 360
		[MethodImpl(4096)]
		private extern void get_maskMapRemapMin_Injected(out Vector4 ret);

		// Token: 0x06000169 RID: 361
		[MethodImpl(4096)]
		private extern void set_maskMapRemapMin_Injected(ref Vector4 value);

		// Token: 0x0600016A RID: 362
		[MethodImpl(4096)]
		private extern void get_maskMapRemapMax_Injected(out Vector4 ret);

		// Token: 0x0600016B RID: 363
		[MethodImpl(4096)]
		private extern void set_maskMapRemapMax_Injected(ref Vector4 value);
	}
}
