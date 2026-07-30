using System;

namespace UnityEngine.Experimental.TerrainAPI
{
	// Token: 0x02000013 RID: 19
	public struct BrushTransform
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00004040 File Offset: 0x00002240
		public Vector2 brushOrigin { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00004048 File Offset: 0x00002248
		public Vector2 brushU { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00004050 File Offset: 0x00002250
		public Vector2 brushV { get; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00004058 File Offset: 0x00002258
		public Vector2 targetOrigin { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00004060 File Offset: 0x00002260
		public Vector2 targetX { get; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00004068 File Offset: 0x00002268
		public Vector2 targetY { get; }

		// Token: 0x06000172 RID: 370 RVA: 0x00004070 File Offset: 0x00002270
		public BrushTransform(Vector2 brushOrigin, Vector2 brushU, Vector2 brushV)
		{
			float num = brushU.x * brushV.y - brushU.y * brushV.x;
			float num2 = (Mathf.Approximately(num, 0f) ? 1f : (1f / num));
			Vector2 vector = new Vector2(brushV.y, -brushU.y) * num2;
			Vector2 vector2 = new Vector2(-brushV.x, brushU.x) * num2;
			Vector2 vector3 = -brushOrigin.x * vector - brushOrigin.y * vector2;
			this.brushOrigin = brushOrigin;
			this.brushU = brushU;
			this.brushV = brushV;
			this.targetOrigin = vector3;
			this.targetX = vector;
			this.targetY = vector2;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00004134 File Offset: 0x00002334
		public Rect GetBrushXYBounds()
		{
			Vector2 vector = this.brushOrigin + this.brushU;
			Vector2 vector2 = this.brushOrigin + this.brushV;
			Vector2 vector3 = this.brushOrigin + this.brushU + this.brushV;
			float num = Mathf.Min(Mathf.Min(this.brushOrigin.x, vector.x), Mathf.Min(vector2.x, vector3.x));
			float num2 = Mathf.Max(Mathf.Max(this.brushOrigin.x, vector.x), Mathf.Max(vector2.x, vector3.x));
			float num3 = Mathf.Min(Mathf.Min(this.brushOrigin.y, vector.y), Mathf.Min(vector2.y, vector3.y));
			float num4 = Mathf.Max(Mathf.Max(this.brushOrigin.y, vector.y), Mathf.Max(vector2.y, vector3.y));
			return Rect.MinMaxRect(num, num3, num2, num4);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000424C File Offset: 0x0000244C
		public static BrushTransform FromRect(Rect brushRect)
		{
			Vector2 min = brushRect.min;
			Vector2 vector = new Vector2(brushRect.width, 0f);
			Vector2 vector2 = new Vector2(0f, brushRect.height);
			return new BrushTransform(min, vector, vector2);
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00004294 File Offset: 0x00002494
		public Vector2 ToBrushUV(Vector2 targetXY)
		{
			return targetXY.x * this.targetX + targetXY.y * this.targetY + this.targetOrigin;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000042D8 File Offset: 0x000024D8
		public Vector2 FromBrushUV(Vector2 brushUV)
		{
			return brushUV.x * this.brushU + brushUV.y * this.brushV + this.brushOrigin;
		}
	}
}
