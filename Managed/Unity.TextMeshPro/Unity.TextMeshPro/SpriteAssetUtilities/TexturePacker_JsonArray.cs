using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro.SpriteAssetUtilities
{
	// Token: 0x02000073 RID: 115
	public class TexturePacker_JsonArray
	{
		// Token: 0x020000A2 RID: 162
		[Serializable]
		public struct SpriteFrame
		{
			// Token: 0x060005F1 RID: 1521 RVA: 0x00037904 File Offset: 0x00035B04
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"x: ",
					this.x.ToString("f2"),
					" y: ",
					this.y.ToString("f2"),
					" h: ",
					this.h.ToString("f2"),
					" w: ",
					this.w.ToString("f2")
				});
			}

			// Token: 0x04000598 RID: 1432
			public float x;

			// Token: 0x04000599 RID: 1433
			public float y;

			// Token: 0x0400059A RID: 1434
			public float w;

			// Token: 0x0400059B RID: 1435
			public float h;
		}

		// Token: 0x020000A3 RID: 163
		[Serializable]
		public struct SpriteSize
		{
			// Token: 0x060005F2 RID: 1522 RVA: 0x00037988 File Offset: 0x00035B88
			public override string ToString()
			{
				return "w: " + this.w.ToString("f2") + " h: " + this.h.ToString("f2");
			}

			// Token: 0x0400059C RID: 1436
			public float w;

			// Token: 0x0400059D RID: 1437
			public float h;
		}

		// Token: 0x020000A4 RID: 164
		[Serializable]
		public struct Frame
		{
			// Token: 0x0400059E RID: 1438
			public string filename;

			// Token: 0x0400059F RID: 1439
			public TexturePacker_JsonArray.SpriteFrame frame;

			// Token: 0x040005A0 RID: 1440
			public bool rotated;

			// Token: 0x040005A1 RID: 1441
			public bool trimmed;

			// Token: 0x040005A2 RID: 1442
			public TexturePacker_JsonArray.SpriteFrame spriteSourceSize;

			// Token: 0x040005A3 RID: 1443
			public TexturePacker_JsonArray.SpriteSize sourceSize;

			// Token: 0x040005A4 RID: 1444
			public Vector2 pivot;
		}

		// Token: 0x020000A5 RID: 165
		[Serializable]
		public class SpriteDataObject
		{
			// Token: 0x040005A5 RID: 1445
			public List<TexturePacker_JsonArray.Frame> frames;
		}
	}
}
