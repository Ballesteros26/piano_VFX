using System;
using System.Collections.Generic;
using Unity.Profiling;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200022A RID: 554
	internal class TextureBlitter : IDisposable
	{
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x00043933 File Offset: 0x00041B33
		// (set) Token: 0x060010BC RID: 4284 RVA: 0x0004393B File Offset: 0x00041B3B
		private protected bool disposed { protected get; private set; }

		// Token: 0x060010BD RID: 4285 RVA: 0x00043944 File Offset: 0x00041B44
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x00043958 File Offset: 0x00041B58
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					UIRUtility.Destroy(this.m_BlitMaterial);
					this.m_BlitMaterial = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00043998 File Offset: 0x00041B98
		static TextureBlitter()
		{
			for (int i = 0; i < 8; i++)
			{
				TextureBlitter.k_TextureIds[i] = Shader.PropertyToID("_MainTex" + i);
			}
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x000439EC File Offset: 0x00041BEC
		public TextureBlitter(int capacity = 512)
		{
			this.m_PendingBlits = new List<TextureBlitter.BlitInfo>(capacity);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x00043A10 File Offset: 0x00041C10
		public void QueueBlit(Texture src, RectInt srcRect, Vector2Int dstPos, bool addBorder, Color tint)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_PendingBlits.Add(new TextureBlitter.BlitInfo
				{
					src = src,
					srcRect = srcRect,
					dstPos = dstPos,
					border = (addBorder ? 1 : 0),
					tint = tint
				});
			}
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x00043A78 File Offset: 0x00041C78
		public void BlitOneNow(RenderTexture dst, Texture src, RectInt srcRect, Vector2Int dstPos, bool addBorder, Color tint)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				this.m_SingleBlit[0] = new TextureBlitter.BlitInfo
				{
					src = src,
					srcRect = srcRect,
					dstPos = dstPos,
					border = (addBorder ? 1 : 0),
					tint = tint
				};
				this.BeginBlit(dst);
				this.DoBlit(this.m_SingleBlit, 0);
				this.EndBlit();
			}
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x00043AFD File Offset: 0x00041CFD
		public int queueLength
		{
			get
			{
				return this.m_PendingBlits.Count;
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00043B0C File Offset: 0x00041D0C
		public void Commit(RenderTexture dst)
		{
			bool disposed = this.disposed;
			if (disposed)
			{
				DisposeHelper.NotifyDisposedUsed(this);
			}
			else
			{
				bool flag = this.m_PendingBlits.Count == 0;
				if (!flag)
				{
					this.BeginBlit(dst);
					for (int i = 0; i < this.m_PendingBlits.Count; i += 8)
					{
						this.DoBlit(this.m_PendingBlits, i);
					}
					this.EndBlit();
					this.m_PendingBlits.Clear();
				}
			}
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00043B85 File Offset: 0x00041D85
		public void Reset()
		{
			this.m_PendingBlits.Clear();
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00043B94 File Offset: 0x00041D94
		private void BeginBlit(RenderTexture dst)
		{
			bool flag = this.m_BlitMaterial == null;
			if (flag)
			{
				Shader shader = Shader.Find("Hidden/Internal-UIRAtlasBlitCopy");
				this.m_BlitMaterial = new Material(shader);
				this.m_BlitMaterial.hideFlags |= HideFlags.DontSaveInEditor;
			}
			this.m_Viewport = Utility.GetActiveViewport();
			this.m_PrevRT = RenderTexture.active;
			GL.LoadPixelMatrix(0f, (float)dst.width, 0f, (float)dst.height);
			Graphics.SetRenderTarget(dst);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x00043C1C File Offset: 0x00041E1C
		private void DoBlit(IList<TextureBlitter.BlitInfo> blitInfos, int startIndex)
		{
			int num = Mathf.Min(startIndex + 8, blitInfos.Count);
			int i = startIndex;
			int num2 = 0;
			while (i < num)
			{
				Texture src = blitInfos[i].src;
				bool flag = src != null;
				if (flag)
				{
					this.m_BlitMaterial.SetTexture(TextureBlitter.k_TextureIds[num2], src);
				}
				i++;
				num2++;
			}
			this.m_BlitMaterial.SetPass(0);
			GL.Begin(7);
			int j = startIndex;
			int num3 = 0;
			while (j < num)
			{
				TextureBlitter.BlitInfo blitInfo = blitInfos[j];
				float num4 = 1f / (float)blitInfo.src.width;
				float num5 = 1f / (float)blitInfo.src.height;
				float num6 = (float)(blitInfo.dstPos.x - blitInfo.border);
				float num7 = (float)(blitInfo.dstPos.y - blitInfo.border);
				float num8 = (float)(blitInfo.dstPos.x + blitInfo.srcRect.width + blitInfo.border);
				float num9 = (float)(blitInfo.dstPos.y + blitInfo.srcRect.height + blitInfo.border);
				float num10 = (float)(blitInfo.srcRect.x - blitInfo.border) * num4;
				float num11 = (float)(blitInfo.srcRect.y - blitInfo.border) * num5;
				float num12 = (float)(blitInfo.srcRect.xMax + blitInfo.border) * num4;
				float num13 = (float)(blitInfo.srcRect.yMax + blitInfo.border) * num5;
				GL.Color(blitInfo.tint);
				GL.TexCoord3(num10, num11, (float)num3);
				GL.Vertex3(num6, num7, 0f);
				GL.Color(blitInfo.tint);
				GL.TexCoord3(num10, num13, (float)num3);
				GL.Vertex3(num6, num9, 0f);
				GL.Color(blitInfo.tint);
				GL.TexCoord3(num12, num13, (float)num3);
				GL.Vertex3(num8, num9, 0f);
				GL.Color(blitInfo.tint);
				GL.TexCoord3(num12, num11, (float)num3);
				GL.Vertex3(num8, num7, 0f);
				j++;
				num3++;
			}
			GL.End();
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x00043E78 File Offset: 0x00042078
		private void EndBlit()
		{
			Graphics.SetRenderTarget(this.m_PrevRT);
			GL.Viewport(new Rect((float)this.m_Viewport.x, (float)this.m_Viewport.y, (float)this.m_Viewport.width, (float)this.m_Viewport.height));
		}

		// Token: 0x0400076E RID: 1902
		private const int k_TextureSlotCount = 8;

		// Token: 0x0400076F RID: 1903
		private static readonly int[] k_TextureIds = new int[8];

		// Token: 0x04000770 RID: 1904
		private static ProfilerMarker s_CommitSampler = new ProfilerMarker("UIR.TextureBlitter.Commit");

		// Token: 0x04000771 RID: 1905
		private TextureBlitter.BlitInfo[] m_SingleBlit = new TextureBlitter.BlitInfo[1];

		// Token: 0x04000772 RID: 1906
		private Material m_BlitMaterial;

		// Token: 0x04000773 RID: 1907
		private RectInt m_Viewport;

		// Token: 0x04000774 RID: 1908
		private RenderTexture m_PrevRT;

		// Token: 0x04000775 RID: 1909
		private List<TextureBlitter.BlitInfo> m_PendingBlits;

		// Token: 0x0200022B RID: 555
		private struct BlitInfo
		{
			// Token: 0x04000777 RID: 1911
			public Texture src;

			// Token: 0x04000778 RID: 1912
			public RectInt srcRect;

			// Token: 0x04000779 RID: 1913
			public Vector2Int dstPos;

			// Token: 0x0400077A RID: 1914
			public int border;

			// Token: 0x0400077B RID: 1915
			public Color tint;
		}
	}
}
