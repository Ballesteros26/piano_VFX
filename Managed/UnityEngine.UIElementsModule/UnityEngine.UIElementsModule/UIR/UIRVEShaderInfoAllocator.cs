using System;
using Unity.Collections;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000237 RID: 567
	internal struct UIRVEShaderInfoAllocator
	{
		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x00044AC0 File Offset: 0x00042CC0
		private static int pageWidth
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x00044AD4 File Offset: 0x00042CD4
		private static int pageHeight
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00044AE8 File Offset: 0x00042CE8
		private static Vector2Int AllocToTexelCoord(ref BitmapAllocator32 allocator, BMPAlloc alloc)
		{
			ushort num;
			ushort num2;
			allocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Vector2Int((int)alloc.bitIndex * allocator.entryWidth + (int)num, (int)alloc.pageLine * allocator.entryHeight + (int)num2);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00044B30 File Offset: 0x00042D30
		private static int AllocToConstantBufferIndex(BMPAlloc alloc)
		{
			return (int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x00044B58 File Offset: 0x00042D58
		private static bool AtlasRectMatchesPage(ref BitmapAllocator32 allocator, BMPAlloc defAlloc, RectInt atlasRect)
		{
			ushort num;
			ushort num2;
			allocator.GetAllocPageAtlasLocation(defAlloc.page, out num, out num2);
			return (int)num == atlasRect.xMin && (int)num2 == atlasRect.yMin && allocator.entryWidth * UIRVEShaderInfoAllocator.pageWidth == atlasRect.width && allocator.entryHeight * UIRVEShaderInfoAllocator.pageHeight == atlasRect.height;
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x00044BBC File Offset: 0x00042DBC
		public NativeSlice<Transform3x4> transformConstants
		{
			get
			{
				return this.m_Transforms;
			}
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00044BDC File Offset: 0x00042DDC
		public NativeSlice<Vector4> clipRectConstants
		{
			get
			{
				return this.m_ClipRects;
			}
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x00044BFC File Offset: 0x00042DFC
		public Texture atlas
		{
			get
			{
				bool atlasReallyCreated = this.m_AtlasReallyCreated;
				Texture texture;
				if (atlasReallyCreated)
				{
					texture = this.m_Atlas.atlas;
				}
				else
				{
					texture = (this.m_VertexTexturingEnabled ? UIRenderDevice.defaultShaderInfoTexFloat : UIRenderDevice.defaultShaderInfoTexARGB8);
				}
				return texture;
			}
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060010FB RID: 4347 RVA: 0x00044C3C File Offset: 0x00042E3C
		public bool internalAtlasCreated
		{
			get
			{
				return this.m_AtlasReallyCreated;
			}
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00044C54 File Offset: 0x00042E54
		public bool isReleased
		{
			get
			{
				bool flag;
				if (this.m_AtlasReallyCreated)
				{
					UIRAtlasManager atlas = this.m_Atlas;
					flag = atlas != null && atlas.IsReleased();
				}
				else
				{
					flag = false;
				}
				return flag;
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x00044C84 File Offset: 0x00042E84
		public void Construct()
		{
			this.m_OpacityAllocator = (this.m_ClipRectAllocator = (this.m_TransformAllocator = default(BitmapAllocator32)));
			this.m_TransformAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 3);
			this.m_TransformAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.identityTransformTexel.x, (ushort)UIRVEShaderInfoAllocator.identityTransformTexel.y);
			this.m_ClipRectAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 1);
			this.m_ClipRectAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.infiniteClipRectTexel.x, (ushort)UIRVEShaderInfoAllocator.infiniteClipRectTexel.y);
			this.m_OpacityAllocator.Construct(UIRVEShaderInfoAllocator.pageHeight, 1, 1);
			this.m_OpacityAllocator.ForceFirstAlloc((ushort)UIRVEShaderInfoAllocator.fullOpacityTexel.x, (ushort)UIRVEShaderInfoAllocator.fullOpacityTexel.y);
			this.m_VertexTexturingEnabled = UIRenderDevice.vertexTexturingIsAvailable;
			bool flag = !this.m_VertexTexturingEnabled;
			if (flag)
			{
				int num = 20;
				this.m_Transforms = new NativeArray<Transform3x4>(num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.m_ClipRects = new NativeArray<Vector4>(num, Allocator.Persistent, NativeArrayOptions.UninitializedMemory);
				this.m_Transforms[0] = new Transform3x4
				{
					v0 = UIRVEShaderInfoAllocator.identityTransformRow0Value,
					v1 = UIRVEShaderInfoAllocator.identityTransformRow1Value,
					v2 = UIRVEShaderInfoAllocator.identityTransformRow2Value
				};
				this.m_ClipRects[0] = UIRVEShaderInfoAllocator.infiniteClipRectValue;
			}
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x00044DEC File Offset: 0x00042FEC
		private void ReallyCreateAtlas()
		{
			this.m_Atlas = new UIRAtlasManager(this.m_VertexTexturingEnabled ? RenderTextureFormat.ARGBFloat : RenderTextureFormat.ARGB32, FilterMode.Point, Math.Max(UIRVEShaderInfoAllocator.pageWidth, UIRVEShaderInfoAllocator.pageHeight * 3), 64);
			RectInt rectInt;
			this.m_Atlas.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_TransformAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_TransformAllocator.entryHeight, out rectInt);
			RectInt rectInt2;
			this.m_Atlas.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_ClipRectAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_ClipRectAllocator.entryHeight, out rectInt2);
			RectInt rectInt3;
			this.m_Atlas.AllocateRect(UIRVEShaderInfoAllocator.pageWidth * this.m_OpacityAllocator.entryWidth, UIRVEShaderInfoAllocator.pageHeight * this.m_OpacityAllocator.entryHeight, out rectInt3);
			bool flag = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_TransformAllocator, UIRVEShaderInfoAllocator.identityTransform, rectInt);
			if (flag)
			{
				throw new Exception("Atlas identity transform allocation failed unexpectedly");
			}
			bool flag2 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_ClipRectAllocator, UIRVEShaderInfoAllocator.infiniteClipRect, rectInt2);
			if (flag2)
			{
				throw new Exception("Atlas infinite clip rect allocation failed unexpectedly");
			}
			bool flag3 = !UIRVEShaderInfoAllocator.AtlasRectMatchesPage(ref this.m_OpacityAllocator, UIRVEShaderInfoAllocator.fullOpacity, rectInt3);
			if (flag3)
			{
				throw new Exception("Atlas full opacity allocation failed unexpectedly");
			}
			Texture2D whiteTexel = UIRenderDevice.whiteTexel;
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_TransformAllocator, UIRVEShaderInfoAllocator.identityTransform);
				this.m_Atlas.EnqueueBlit(whiteTexel, vector2Int.x, vector2Int.y, false, UIRVEShaderInfoAllocator.identityTransformRow0Value);
				this.m_Atlas.EnqueueBlit(whiteTexel, vector2Int.x, vector2Int.y + 1, false, UIRVEShaderInfoAllocator.identityTransformRow1Value);
				this.m_Atlas.EnqueueBlit(whiteTexel, vector2Int.x, vector2Int.y + 2, false, UIRVEShaderInfoAllocator.identityTransformRow2Value);
				vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_ClipRectAllocator, UIRVEShaderInfoAllocator.infiniteClipRect);
				this.m_Atlas.EnqueueBlit(whiteTexel, vector2Int.x, vector2Int.y, false, UIRVEShaderInfoAllocator.infiniteClipRectValue);
			}
			Vector2Int vector2Int2 = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_OpacityAllocator, UIRVEShaderInfoAllocator.fullOpacity);
			this.m_Atlas.EnqueueBlit(whiteTexel, vector2Int2.x, vector2Int2.y, false, UIRVEShaderInfoAllocator.fullOpacityValue);
			this.m_AtlasReallyCreated = true;
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x00045040 File Offset: 0x00043240
		public void Dispose()
		{
			bool flag = this.m_Atlas != null;
			if (flag)
			{
				this.m_Atlas.Dispose();
			}
			this.m_Atlas = null;
			bool isCreated = this.m_ClipRects.IsCreated;
			if (isCreated)
			{
				this.m_ClipRects.Dispose();
			}
			bool isCreated2 = this.m_Transforms.IsCreated;
			if (isCreated2)
			{
				this.m_Transforms.Dispose();
			}
			this.m_AtlasReallyCreated = false;
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000450AB File Offset: 0x000432AB
		public void IssuePendingAtlasBlits()
		{
			UIRAtlasManager atlas = this.m_Atlas;
			if (atlas != null)
			{
				atlas.Commit();
			}
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000450C0 File Offset: 0x000432C0
		public BMPAlloc AllocTransform()
		{
			bool flag = !this.m_AtlasReallyCreated;
			if (flag)
			{
				this.ReallyCreateAtlas();
			}
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			BMPAlloc bmpalloc;
			if (vertexTexturingEnabled)
			{
				bmpalloc = this.m_TransformAllocator.Allocate(this.m_Atlas);
			}
			else
			{
				BMPAlloc bmpalloc2 = this.m_TransformAllocator.Allocate(null);
				bool flag2 = UIRVEShaderInfoAllocator.AllocToConstantBufferIndex(bmpalloc2) < this.m_Transforms.Length;
				if (flag2)
				{
					bmpalloc = bmpalloc2;
				}
				else
				{
					this.m_TransformAllocator.Free(bmpalloc2);
					bmpalloc = BMPAlloc.Invalid;
				}
			}
			return bmpalloc;
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x00045140 File Offset: 0x00043340
		public BMPAlloc AllocClipRect()
		{
			bool flag = !this.m_AtlasReallyCreated;
			if (flag)
			{
				this.ReallyCreateAtlas();
			}
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			BMPAlloc bmpalloc;
			if (vertexTexturingEnabled)
			{
				bmpalloc = this.m_ClipRectAllocator.Allocate(this.m_Atlas);
			}
			else
			{
				BMPAlloc bmpalloc2 = this.m_ClipRectAllocator.Allocate(null);
				bool flag2 = UIRVEShaderInfoAllocator.AllocToConstantBufferIndex(bmpalloc2) < this.m_ClipRects.Length;
				if (flag2)
				{
					bmpalloc = bmpalloc2;
				}
				else
				{
					this.m_ClipRectAllocator.Free(bmpalloc2);
					bmpalloc = BMPAlloc.Invalid;
				}
			}
			return bmpalloc;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x000451C0 File Offset: 0x000433C0
		public BMPAlloc AllocOpacity()
		{
			bool flag = !this.m_AtlasReallyCreated;
			if (flag)
			{
				this.ReallyCreateAtlas();
			}
			return this.m_OpacityAllocator.Allocate(this.m_Atlas);
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000451F8 File Offset: 0x000433F8
		public void SetTransformValue(BMPAlloc alloc, Matrix4x4 xform)
		{
			Debug.Assert(alloc.IsValid());
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_TransformAllocator, alloc);
				this.m_Atlas.EnqueueBlit(UIRenderDevice.whiteTexel, vector2Int.x, vector2Int.y, false, xform.GetRow(0));
				this.m_Atlas.EnqueueBlit(UIRenderDevice.whiteTexel, vector2Int.x, vector2Int.y + 1, false, xform.GetRow(1));
				this.m_Atlas.EnqueueBlit(UIRenderDevice.whiteTexel, vector2Int.x, vector2Int.y + 2, false, xform.GetRow(2));
			}
			else
			{
				this.m_Transforms[UIRVEShaderInfoAllocator.AllocToConstantBufferIndex(alloc)] = new Transform3x4
				{
					v0 = xform.GetRow(0),
					v1 = xform.GetRow(1),
					v2 = xform.GetRow(2)
				};
			}
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x00045304 File Offset: 0x00043504
		public void SetClipRectValue(BMPAlloc alloc, Vector4 clipRect)
		{
			Debug.Assert(alloc.IsValid());
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_ClipRectAllocator, alloc);
				this.m_Atlas.EnqueueBlit(UIRenderDevice.whiteTexel, vector2Int.x, vector2Int.y, false, clipRect);
			}
			else
			{
				this.m_ClipRects[UIRVEShaderInfoAllocator.AllocToConstantBufferIndex(alloc)] = clipRect;
			}
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x00045374 File Offset: 0x00043574
		public void SetOpacityValue(BMPAlloc alloc, float opacity)
		{
			Debug.Assert(alloc.IsValid());
			Vector2Int vector2Int = UIRVEShaderInfoAllocator.AllocToTexelCoord(ref this.m_OpacityAllocator, alloc);
			this.m_Atlas.EnqueueBlit(UIRenderDevice.whiteTexel, vector2Int.x, vector2Int.y, false, new Color(1f, 1f, 1f, opacity));
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x000453D1 File Offset: 0x000435D1
		public void FreeTransform(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_TransformAllocator.Free(alloc);
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000453EE File Offset: 0x000435EE
		public void FreeClipRect(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_ClipRectAllocator.Free(alloc);
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x0004540B File Offset: 0x0004360B
		public void FreeOpacity(BMPAlloc alloc)
		{
			Debug.Assert(alloc.IsValid());
			this.m_OpacityAllocator.Free(alloc);
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x00045428 File Offset: 0x00043628
		public Color32 TransformAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num = 0;
			ushort num2 = 0;
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				this.m_TransformAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			}
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x0004549C File Offset: 0x0004369C
		public Color32 ClipRectAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num = 0;
			ushort num2 = 0;
			bool vertexTexturingEnabled = this.m_VertexTexturingEnabled;
			if (vertexTexturingEnabled)
			{
				this.m_ClipRectAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			}
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x00045510 File Offset: 0x00043710
		public Color32 OpacityAllocToVertexData(BMPAlloc alloc)
		{
			Debug.Assert(UIRVEShaderInfoAllocator.pageWidth == 32 && UIRVEShaderInfoAllocator.pageHeight == 8);
			ushort num;
			ushort num2;
			this.m_OpacityAllocator.GetAllocPageAtlasLocation(alloc.page, out num, out num2);
			return new Color32((byte)(num >> 5), (byte)(num2 >> 3), (byte)((int)alloc.pageLine * UIRVEShaderInfoAllocator.pageWidth + (int)alloc.bitIndex), 0);
		}

		// Token: 0x040007A6 RID: 1958
		private UIRAtlasManager m_Atlas;

		// Token: 0x040007A7 RID: 1959
		private BitmapAllocator32 m_TransformAllocator;

		// Token: 0x040007A8 RID: 1960
		private BitmapAllocator32 m_ClipRectAllocator;

		// Token: 0x040007A9 RID: 1961
		private BitmapAllocator32 m_OpacityAllocator;

		// Token: 0x040007AA RID: 1962
		private bool m_AtlasReallyCreated;

		// Token: 0x040007AB RID: 1963
		private bool m_VertexTexturingEnabled;

		// Token: 0x040007AC RID: 1964
		private NativeArray<Transform3x4> m_Transforms;

		// Token: 0x040007AD RID: 1965
		private NativeArray<Vector4> m_ClipRects;

		// Token: 0x040007AE RID: 1966
		internal static readonly Vector2Int identityTransformTexel = new Vector2Int(0, 0);

		// Token: 0x040007AF RID: 1967
		internal static readonly Vector2Int infiniteClipRectTexel = new Vector2Int(0, 32);

		// Token: 0x040007B0 RID: 1968
		internal static readonly Vector2Int fullOpacityTexel = new Vector2Int(32, 32);

		// Token: 0x040007B1 RID: 1969
		internal static readonly Vector4 identityTransformRow0Value = new Vector4(1f, 0f, 0f, 0f);

		// Token: 0x040007B2 RID: 1970
		internal static readonly Vector4 identityTransformRow1Value = new Vector4(0f, 1f, 0f, 0f);

		// Token: 0x040007B3 RID: 1971
		internal static readonly Vector4 identityTransformRow2Value = new Vector4(0f, 0f, 1f, 0f);

		// Token: 0x040007B4 RID: 1972
		internal static readonly Vector4 infiniteClipRectValue = new Vector4(float.MinValue, float.MinValue, float.MaxValue, float.MaxValue);

		// Token: 0x040007B5 RID: 1973
		internal static readonly Vector4 fullOpacityValue = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x040007B6 RID: 1974
		public static readonly BMPAlloc identityTransform;

		// Token: 0x040007B7 RID: 1975
		public static readonly BMPAlloc infiniteClipRect;

		// Token: 0x040007B8 RID: 1976
		public static readonly BMPAlloc fullOpacity;
	}
}
