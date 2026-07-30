using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.U2D
{
	// Token: 0x02000009 RID: 9
	[MovedFrom("UnityEngine.Experimental.U2D")]
	[NativeHeader("Modules/SpriteShape/Public/SpriteShapeUtility.h")]
	public class SpriteShapeUtility
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000022E1 File Offset: 0x000004E1
		[FreeFunction("SpriteShapeUtility::Generate")]
		[NativeThrows]
		public static int[] Generate(Mesh mesh, SpriteShapeParameters shapeParams, ShapeControlPoint[] points, SpriteShapeMetaData[] metaData, AngleRangeInfo[] angleRange, Sprite[] sprites, Sprite[] corners)
		{
			return SpriteShapeUtility.Generate_Injected(mesh, ref shapeParams, points, metaData, angleRange, sprites, corners);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000022F3 File Offset: 0x000004F3
		[FreeFunction("SpriteShapeUtility::GenerateSpriteShape")]
		[NativeThrows]
		public static void GenerateSpriteShape(SpriteShapeRenderer renderer, SpriteShapeParameters shapeParams, ShapeControlPoint[] points, SpriteShapeMetaData[] metaData, AngleRangeInfo[] angleRange, Sprite[] sprites, Sprite[] corners)
		{
			SpriteShapeUtility.GenerateSpriteShape_Injected(renderer, ref shapeParams, points, metaData, angleRange, sprites, corners);
		}

		// Token: 0x06000023 RID: 35
		[MethodImpl(4096)]
		private static extern int[] Generate_Injected(Mesh mesh, ref SpriteShapeParameters shapeParams, ShapeControlPoint[] points, SpriteShapeMetaData[] metaData, AngleRangeInfo[] angleRange, Sprite[] sprites, Sprite[] corners);

		// Token: 0x06000024 RID: 36
		[MethodImpl(4096)]
		private static extern void GenerateSpriteShape_Injected(SpriteShapeRenderer renderer, ref SpriteShapeParameters shapeParams, ShapeControlPoint[] points, SpriteShapeMetaData[] metaData, AngleRangeInfo[] angleRange, Sprite[] sprites, Sprite[] corners);
	}
}
