using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine
{
	// Token: 0x02000152 RID: 338
	[NativeHeader("Runtime/Graphics/SparseTexture.h")]
	public sealed class SparseTexture : Texture
	{
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000EDA RID: 3802
		public extern int tileWidth
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000EDB RID: 3803
		public extern int tileHeight
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000EDC RID: 3804
		public extern bool isCreated
		{
			[NativeName("IsInitialized")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000EDD RID: 3805
		[FreeFunction(Name = "SparseTextureScripting::Create", ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void Internal_Create([Writable] SparseTexture mono, int width, int height, GraphicsFormat format, int mipCount);

		// Token: 0x06000EDE RID: 3806
		[FreeFunction(Name = "SparseTextureScripting::UpdateTile", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void UpdateTile(int tileX, int tileY, int miplevel, Color32[] data);

		// Token: 0x06000EDF RID: 3807
		[FreeFunction(Name = "SparseTextureScripting::UpdateTileRaw", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void UpdateTileRaw(int tileX, int tileY, int miplevel, byte[] data);

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00013FBA File Offset: 0x000121BA
		public void UnloadTile(int tileX, int tileY, int miplevel)
		{
			this.UpdateTileRaw(tileX, tileY, miplevel, null);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00013FC8 File Offset: 0x000121C8
		internal bool ValidateSize(int width, int height, GraphicsFormat format)
		{
			bool flag = (ulong)GraphicsFormatUtility.GetBlockSize(format) * (ulong)((long)width / (long)((ulong)GraphicsFormatUtility.GetBlockWidth(format))) * (ulong)((long)height / (long)((ulong)GraphicsFormatUtility.GetBlockHeight(format))) < 65536UL;
			bool flag2;
			if (flag)
			{
				Debug.LogError("SparseTexture creation failed. The minimum size in bytes of a SparseTexture is 64KB.", this);
				flag2 = false;
			}
			else
			{
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00014015 File Offset: 0x00012215
		public SparseTexture(int width, int height, DefaultFormat format, int mipCount)
			: this(width, height, SystemInfo.GetGraphicsFormat(format), mipCount)
		{
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0001402C File Offset: 0x0001222C
		public SparseTexture(int width, int height, GraphicsFormat format, int mipCount)
		{
			bool flag = !base.ValidateFormat(format, FormatUsage.Sample);
			if (!flag)
			{
				bool flag2 = !this.ValidateSize(width, height, format);
				if (!flag2)
				{
					SparseTexture.Internal_Create(this, width, height, format, mipCount);
				}
			}
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00014070 File Offset: 0x00012270
		public SparseTexture(int width, int height, TextureFormat textureFormat, int mipCount)
			: this(width, height, textureFormat, mipCount, false)
		{
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00014080 File Offset: 0x00012280
		public SparseTexture(int width, int height, TextureFormat textureFormat, int mipCount, bool linear)
		{
			bool flag = !base.ValidateFormat(textureFormat);
			if (!flag)
			{
				GraphicsFormat graphicsFormat = GraphicsFormatUtility.GetGraphicsFormat(textureFormat, !linear);
				bool flag2 = !this.ValidateSize(width, height, graphicsFormat);
				if (!flag2)
				{
					SparseTexture.Internal_Create(this, width, height, graphicsFormat, mipCount);
				}
			}
		}
	}
}
