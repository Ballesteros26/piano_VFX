using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000207 RID: 519
	[NativeType("Runtime/Graphics/SpriteFrame.h")]
	[ExcludeFromPreset]
	[NativeHeader("Runtime/2D/Common/SpriteDataAccess.h")]
	[NativeHeader("Runtime/2D/Common/ScriptBindings/SpritesMarshalling.h")]
	[NativeHeader("Runtime/Graphics/SpriteUtility.h")]
	public sealed class Sprite : Object
	{
		// Token: 0x060016F7 RID: 5879 RVA: 0x0000BEFE File Offset: 0x0000A0FE
		[RequiredByNativeCode]
		private Sprite()
		{
		}

		// Token: 0x060016F8 RID: 5880
		[MethodImpl(4096)]
		internal extern int GetPackingMode();

		// Token: 0x060016F9 RID: 5881
		[MethodImpl(4096)]
		internal extern int GetPackingRotation();

		// Token: 0x060016FA RID: 5882
		[MethodImpl(4096)]
		internal extern int GetPacked();

		// Token: 0x060016FB RID: 5883 RVA: 0x00025400 File Offset: 0x00023600
		internal Rect GetTextureRect()
		{
			Rect rect;
			this.GetTextureRect_Injected(out rect);
			return rect;
		}

		// Token: 0x060016FC RID: 5884 RVA: 0x00025418 File Offset: 0x00023618
		internal Vector2 GetTextureRectOffset()
		{
			Vector2 vector;
			this.GetTextureRectOffset_Injected(out vector);
			return vector;
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x00025430 File Offset: 0x00023630
		internal Vector4 GetInnerUVs()
		{
			Vector4 vector;
			this.GetInnerUVs_Injected(out vector);
			return vector;
		}

		// Token: 0x060016FE RID: 5886 RVA: 0x00025448 File Offset: 0x00023648
		internal Vector4 GetOuterUVs()
		{
			Vector4 vector;
			this.GetOuterUVs_Injected(out vector);
			return vector;
		}

		// Token: 0x060016FF RID: 5887 RVA: 0x00025460 File Offset: 0x00023660
		internal Vector4 GetPadding()
		{
			Vector4 vector;
			this.GetPadding_Injected(out vector);
			return vector;
		}

		// Token: 0x06001700 RID: 5888 RVA: 0x00025476 File Offset: 0x00023676
		[FreeFunction("SpritesBindings::CreateSpriteWithoutTextureScripting")]
		internal static Sprite CreateSpriteWithoutTextureScripting(Rect rect, Vector2 pivot, float pixelsToUnits, Texture2D texture)
		{
			return Sprite.CreateSpriteWithoutTextureScripting_Injected(ref rect, ref pivot, pixelsToUnits, texture);
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00025483 File Offset: 0x00023683
		[FreeFunction("SpritesBindings::CreateSprite")]
		internal static Sprite CreateSprite(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape)
		{
			return Sprite.CreateSprite_Injected(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref border, generateFallbackPhysicsShape);
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001702 RID: 5890 RVA: 0x00025498 File Offset: 0x00023698
		public Bounds bounds
		{
			get
			{
				Bounds bounds;
				this.get_bounds_Injected(out bounds);
				return bounds;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x000254B0 File Offset: 0x000236B0
		public Rect rect
		{
			get
			{
				Rect rect;
				this.get_rect_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x000254C8 File Offset: 0x000236C8
		public Vector4 border
		{
			get
			{
				Vector4 vector;
				this.get_border_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06001705 RID: 5893
		public extern Texture2D texture
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06001706 RID: 5894
		public extern float pixelsPerUnit
		{
			[NativeMethod("GetPixelsToUnits")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001707 RID: 5895
		public extern float spriteAtlasTextureScale
		{
			[NativeMethod("GetSpriteAtlasTextureScale")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001708 RID: 5896
		public extern Texture2D associatedAlphaSplitTexture
		{
			[NativeMethod("GetAlphaTexture")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x000254E0 File Offset: 0x000236E0
		public Vector2 pivot
		{
			[NativeMethod("GetPivotInPixels")]
			get
			{
				Vector2 vector;
				this.get_pivot_Injected(out vector);
				return vector;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600170A RID: 5898 RVA: 0x000254F8 File Offset: 0x000236F8
		public bool packed
		{
			get
			{
				return this.GetPacked() == 1;
			}
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600170B RID: 5899 RVA: 0x00025514 File Offset: 0x00023714
		public SpritePackingMode packingMode
		{
			get
			{
				return (SpritePackingMode)this.GetPackingMode();
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x0002552C File Offset: 0x0002372C
		public SpritePackingRotation packingRotation
		{
			get
			{
				return (SpritePackingRotation)this.GetPackingRotation();
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x00025544 File Offset: 0x00023744
		public Rect textureRect
		{
			get
			{
				bool flag = this.packed && this.packingMode != SpritePackingMode.Rectangle;
				Rect rect;
				if (flag)
				{
					rect = Rect.zero;
				}
				else
				{
					rect = this.GetTextureRect();
				}
				return rect;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x00025580 File Offset: 0x00023780
		public Vector2 textureRectOffset
		{
			get
			{
				bool flag = this.packed && this.packingMode != SpritePackingMode.Rectangle;
				Vector2 vector;
				if (flag)
				{
					vector = Vector2.zero;
				}
				else
				{
					vector = this.GetTextureRectOffset();
				}
				return vector;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600170F RID: 5903
		public extern Vector2[] vertices
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteVertices", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001710 RID: 5904
		public extern ushort[] triangles
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteIndices", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001711 RID: 5905
		public extern Vector2[] uv
		{
			[FreeFunction("SpriteAccessLegacy::GetSpriteUVs", HasExplicitThis = true)]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06001712 RID: 5906
		[MethodImpl(4096)]
		public extern int GetPhysicsShapeCount();

		// Token: 0x06001713 RID: 5907 RVA: 0x000255BC File Offset: 0x000237BC
		public int GetPhysicsShapePointCount(int shapeIdx)
		{
			int physicsShapeCount = this.GetPhysicsShapeCount();
			bool flag = shapeIdx < 0 || shapeIdx >= physicsShapeCount;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index({0}) is out of bounds(0 - {1})", shapeIdx, physicsShapeCount - 1));
			}
			return this.Internal_GetPhysicsShapePointCount(shapeIdx);
		}

		// Token: 0x06001714 RID: 5908
		[NativeMethod("GetPhysicsShapePointCount")]
		[MethodImpl(4096)]
		private extern int Internal_GetPhysicsShapePointCount(int shapeIdx);

		// Token: 0x06001715 RID: 5909 RVA: 0x0002560C File Offset: 0x0002380C
		public int GetPhysicsShape(int shapeIdx, List<Vector2> physicsShape)
		{
			int physicsShapeCount = this.GetPhysicsShapeCount();
			bool flag = shapeIdx < 0 || shapeIdx >= physicsShapeCount;
			if (flag)
			{
				throw new IndexOutOfRangeException(string.Format("Index({0}) is out of bounds(0 - {1})", shapeIdx, physicsShapeCount - 1));
			}
			Sprite.GetPhysicsShapeImpl(this, shapeIdx, physicsShape);
			return physicsShape.Count;
		}

		// Token: 0x06001716 RID: 5910
		[FreeFunction("SpritesBindings::GetPhysicsShape", ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void GetPhysicsShapeImpl(Sprite sprite, int shapeIdx, List<Vector2> physicsShape);

		// Token: 0x06001717 RID: 5911 RVA: 0x00025664 File Offset: 0x00023864
		public void OverridePhysicsShape(IList<Vector2[]> physicsShapes)
		{
			for (int i = 0; i < physicsShapes.Count; i++)
			{
				Vector2[] array = physicsShapes[i];
				bool flag = array == null;
				if (flag)
				{
					throw new ArgumentNullException(string.Format("Physics Shape at {0} is null.", i));
				}
				bool flag2 = array.Length < 3;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Physics Shape at {0} has less than 3 vertices ({1}).", i, array.Length));
				}
			}
			Sprite.OverridePhysicsShapeCount(this, physicsShapes.Count);
			for (int j = 0; j < physicsShapes.Count; j++)
			{
				Sprite.OverridePhysicsShape(this, physicsShapes[j], j);
			}
		}

		// Token: 0x06001718 RID: 5912
		[FreeFunction("SpritesBindings::OverridePhysicsShapeCount")]
		[MethodImpl(4096)]
		private static extern void OverridePhysicsShapeCount(Sprite sprite, int physicsShapeCount);

		// Token: 0x06001719 RID: 5913
		[FreeFunction("SpritesBindings::OverridePhysicsShape", ThrowsException = true)]
		[MethodImpl(4096)]
		private static extern void OverridePhysicsShape(Sprite sprite, Vector2[] physicsShape, int idx);

		// Token: 0x0600171A RID: 5914
		[FreeFunction("SpritesBindings::OverrideGeometry", HasExplicitThis = true)]
		[MethodImpl(4096)]
		public extern void OverrideGeometry(Vector2[] vertices, ushort[] triangles);

		// Token: 0x0600171B RID: 5915 RVA: 0x00025714 File Offset: 0x00023914
		internal static Sprite Create(Rect rect, Vector2 pivot, float pixelsToUnits, Texture2D texture)
		{
			return Sprite.CreateSpriteWithoutTextureScripting(rect, pivot, pixelsToUnits, texture);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00025730 File Offset: 0x00023930
		internal static Sprite Create(Rect rect, Vector2 pivot, float pixelsToUnits)
		{
			return Sprite.CreateSpriteWithoutTextureScripting(rect, pivot, pixelsToUnits, null);
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x0002574C File Offset: 0x0002394C
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border, bool generateFallbackPhysicsShape)
		{
			bool flag = texture == null;
			Sprite sprite;
			if (flag)
			{
				sprite = null;
			}
			else
			{
				bool flag2 = rect.xMax > (float)texture.width || rect.yMax > (float)texture.height;
				if (flag2)
				{
					throw new ArgumentException(string.Format("Could not create sprite ({0}, {1}, {2}, {3}) from a {4}x{5} texture.", new object[] { rect.x, rect.y, rect.width, rect.height, texture.width, texture.height }));
				}
				bool flag3 = pixelsPerUnit <= 0f;
				if (flag3)
				{
					throw new ArgumentException("pixelsPerUnit must be set to a positive non-zero value.");
				}
				sprite = Sprite.CreateSprite(texture, rect, pivot, pixelsPerUnit, extrude, meshType, border, generateFallbackPhysicsShape);
			}
			return sprite;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00025830 File Offset: 0x00023A30
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, Vector4 border)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, meshType, border, false);
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00025854 File Offset: 0x00023A54
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, meshType, Vector4.zero);
		}

		// Token: 0x06001720 RID: 5920 RVA: 0x00025878 File Offset: 0x00023A78
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, extrude, SpriteMeshType.Tight);
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x00025898 File Offset: 0x00023A98
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit)
		{
			return Sprite.Create(texture, rect, pivot, pixelsPerUnit, 0U);
		}

		// Token: 0x06001722 RID: 5922 RVA: 0x000258B4 File Offset: 0x00023AB4
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot)
		{
			return Sprite.Create(texture, rect, pivot, 100f);
		}

		// Token: 0x06001723 RID: 5923
		[MethodImpl(4096)]
		private extern void GetTextureRect_Injected(out Rect ret);

		// Token: 0x06001724 RID: 5924
		[MethodImpl(4096)]
		private extern void GetTextureRectOffset_Injected(out Vector2 ret);

		// Token: 0x06001725 RID: 5925
		[MethodImpl(4096)]
		private extern void GetInnerUVs_Injected(out Vector4 ret);

		// Token: 0x06001726 RID: 5926
		[MethodImpl(4096)]
		private extern void GetOuterUVs_Injected(out Vector4 ret);

		// Token: 0x06001727 RID: 5927
		[MethodImpl(4096)]
		private extern void GetPadding_Injected(out Vector4 ret);

		// Token: 0x06001728 RID: 5928
		[MethodImpl(4096)]
		private static extern Sprite CreateSpriteWithoutTextureScripting_Injected(ref Rect rect, ref Vector2 pivot, float pixelsToUnits, Texture2D texture);

		// Token: 0x06001729 RID: 5929
		[MethodImpl(4096)]
		private static extern Sprite CreateSprite_Injected(Texture2D texture, ref Rect rect, ref Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, ref Vector4 border, bool generateFallbackPhysicsShape);

		// Token: 0x0600172A RID: 5930
		[MethodImpl(4096)]
		private extern void get_bounds_Injected(out Bounds ret);

		// Token: 0x0600172B RID: 5931
		[MethodImpl(4096)]
		private extern void get_rect_Injected(out Rect ret);

		// Token: 0x0600172C RID: 5932
		[MethodImpl(4096)]
		private extern void get_border_Injected(out Vector4 ret);

		// Token: 0x0600172D RID: 5933
		[MethodImpl(4096)]
		private extern void get_pivot_Injected(out Vector2 ret);
	}
}
