using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Tilemaps
{
	// Token: 0x0200000E RID: 14
	[NativeHeader("Modules/Grid/Public/GridMarshalling.h")]
	[NativeHeader("Modules/Tilemap/TilemapRendererJobs.h")]
	[NativeType(Header = "Modules/Tilemap/Public/TilemapRenderer.h")]
	[RequireComponent(typeof(Tilemap))]
	[NativeHeader("Modules/Tilemap/Public/TilemapMarshalling.h")]
	public sealed class TilemapRenderer : Renderer
	{
		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009E RID: 158 RVA: 0x000029BC File Offset: 0x00000BBC
		// (set) Token: 0x0600009F RID: 159 RVA: 0x000029D2 File Offset: 0x00000BD2
		public Vector3Int chunkSize
		{
			get
			{
				Vector3Int vector3Int;
				this.get_chunkSize_Injected(out vector3Int);
				return vector3Int;
			}
			set
			{
				this.set_chunkSize_Injected(ref value);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x000029DC File Offset: 0x00000BDC
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x000029F2 File Offset: 0x00000BF2
		public Vector3 chunkCullingBounds
		{
			[FreeFunction("TilemapRendererBindings::GetChunkCullingBounds", HasExplicitThis = true)]
			get
			{
				Vector3 vector;
				this.get_chunkCullingBounds_Injected(out vector);
				return vector;
			}
			[FreeFunction("TilemapRendererBindings::SetChunkCullingBounds", HasExplicitThis = true)]
			set
			{
				this.set_chunkCullingBounds_Injected(ref value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A2 RID: 162
		// (set) Token: 0x060000A3 RID: 163
		public extern int maxChunkCount
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A4 RID: 164
		// (set) Token: 0x060000A5 RID: 165
		public extern int maxFrameAge
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A6 RID: 166
		// (set) Token: 0x060000A7 RID: 167
		public extern TilemapRenderer.SortOrder sortOrder
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A8 RID: 168
		// (set) Token: 0x060000A9 RID: 169
		[NativeProperty("RenderMode")]
		public extern TilemapRenderer.Mode mode
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AA RID: 170
		// (set) Token: 0x060000AB RID: 171
		public extern TilemapRenderer.DetectChunkCullingBounds detectChunkCullingBounds
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000AC RID: 172
		// (set) Token: 0x060000AD RID: 173
		public extern SpriteMaskInteraction maskInteraction
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x060000AF RID: 175
		[MethodImpl(4096)]
		private extern void get_chunkSize_Injected(out Vector3Int ret);

		// Token: 0x060000B0 RID: 176
		[MethodImpl(4096)]
		private extern void set_chunkSize_Injected(ref Vector3Int value);

		// Token: 0x060000B1 RID: 177
		[MethodImpl(4096)]
		private extern void get_chunkCullingBounds_Injected(out Vector3 ret);

		// Token: 0x060000B2 RID: 178
		[MethodImpl(4096)]
		private extern void set_chunkCullingBounds_Injected(ref Vector3 value);

		// Token: 0x0200000F RID: 15
		public enum SortOrder
		{
			// Token: 0x0400002E RID: 46
			BottomLeft,
			// Token: 0x0400002F RID: 47
			BottomRight,
			// Token: 0x04000030 RID: 48
			TopLeft,
			// Token: 0x04000031 RID: 49
			TopRight
		}

		// Token: 0x02000010 RID: 16
		public enum Mode
		{
			// Token: 0x04000033 RID: 51
			Chunk,
			// Token: 0x04000034 RID: 52
			Individual
		}

		// Token: 0x02000011 RID: 17
		public enum DetectChunkCullingBounds
		{
			// Token: 0x04000036 RID: 54
			Auto,
			// Token: 0x04000037 RID: 55
			Manual
		}
	}
}
