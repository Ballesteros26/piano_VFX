using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200024F RID: 591
	internal class RenderChainCommand : PoolItem
	{
		// Token: 0x0600115C RID: 4444 RVA: 0x00048A74 File Offset: 0x00046C74
		internal void Reset()
		{
			this.owner = null;
			this.prev = (this.next = null);
			this.closing = false;
			this.type = CommandType.Draw;
			this.state = default(State);
			this.mesh = null;
			this.indexOffset = (this.indexCount = 0);
			this.callback = null;
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00048AD4 File Offset: 0x00046CD4
		internal void ExecuteNonDrawMesh(DrawParams drawParams, float pixelsPerPoint, ref Exception immediateException)
		{
			switch (this.type)
			{
			case CommandType.ImmediateCull:
			{
				bool flag = !RenderChainCommand.RectPointsToPixelsAndFlipYAxis(this.owner.worldBound, pixelsPerPoint).Overlaps(Utility.GetActiveViewport());
				if (flag)
				{
					return;
				}
				break;
			}
			case CommandType.Immediate:
				break;
			case CommandType.PushView:
			{
				ViewTransform viewTransform = new ViewTransform
				{
					transform = this.owner.worldTransform,
					clipRect = RenderChainCommand.RectToClipSpace(this.owner.worldClip)
				};
				drawParams.view.Push(viewTransform);
				GL.modelview = viewTransform.transform;
				return;
			}
			case CommandType.PopView:
				drawParams.view.Pop();
				GL.modelview = drawParams.view.Peek().transform;
				return;
			case CommandType.PushScissor:
			{
				Rect rect = RenderChainCommand.CombineScissorRects(this.owner.worldClip, drawParams.scissor.Peek());
				drawParams.scissor.Push(rect);
				Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(rect, pixelsPerPoint));
				return;
			}
			case CommandType.PopScissor:
			{
				drawParams.scissor.Pop();
				Rect rect2 = drawParams.scissor.Peek();
				bool flag2 = rect2.x == DrawParams.k_UnlimitedRect.x;
				if (flag2)
				{
					Utility.DisableScissor();
				}
				else
				{
					Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(rect2, pixelsPerPoint));
				}
				return;
			}
			default:
				return;
			}
			bool flag3 = immediateException != null;
			if (!flag3)
			{
				Matrix4x4 unityProjectionMatrix = Utility.GetUnityProjectionMatrix();
				bool flag4 = drawParams.scissor.Count > 1;
				bool flag5 = flag4;
				if (flag5)
				{
					Utility.DisableScissor();
				}
				Utility.ProfileImmediateRendererBegin();
				try
				{
					using (new GUIClip.ParentClipScope(this.owner.worldTransform, this.owner.worldClip))
					{
						this.callback.Invoke();
					}
				}
				catch (Exception ex)
				{
					immediateException = ex;
				}
				GL.modelview = drawParams.view.Peek().transform;
				GL.LoadProjectionMatrix(unityProjectionMatrix);
				Utility.ProfileImmediateRendererEnd();
				bool flag6 = flag4;
				if (flag6)
				{
					Utility.SetScissorRect(RenderChainCommand.RectPointsToPixelsAndFlipYAxis(drawParams.scissor.Peek(), pixelsPerPoint));
				}
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00048D24 File Offset: 0x00046F24
		private static Vector4 RectToClipSpace(Rect rc)
		{
			Matrix4x4 deviceProjectionMatrix = Utility.GetDeviceProjectionMatrix();
			Vector3 vector = deviceProjectionMatrix.MultiplyPoint(new Vector3(rc.xMin, rc.yMin, 0f));
			Vector3 vector2 = deviceProjectionMatrix.MultiplyPoint(new Vector3(rc.xMax, rc.yMax, 0f));
			return new Vector4(Mathf.Min(vector.x, vector2.x), Mathf.Min(vector.y, vector2.y), Mathf.Max(vector.x, vector2.x), Mathf.Max(vector.y, vector2.y));
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x00048DC8 File Offset: 0x00046FC8
		private static Rect CombineScissorRects(Rect r0, Rect r1)
		{
			Rect rect = new Rect(0f, 0f, 0f, 0f);
			rect.x = Math.Max(r0.x, r1.x);
			rect.y = Math.Max(r0.y, r1.y);
			rect.xMax = Math.Max(rect.x, Math.Min(r0.xMax, r1.xMax));
			rect.yMax = Math.Max(rect.y, Math.Min(r0.yMax, r1.yMax));
			return rect;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x00048E7C File Offset: 0x0004707C
		private static RectInt RectPointsToPixelsAndFlipYAxis(Rect rect, float pixelsPerPoint)
		{
			float num = (float)Utility.GetActiveViewport().height;
			return new RectInt(0, 0, 0, 0)
			{
				x = Mathf.RoundToInt(rect.x * pixelsPerPoint),
				y = Mathf.RoundToInt(num - rect.yMax * pixelsPerPoint),
				width = Mathf.RoundToInt(rect.width * pixelsPerPoint),
				height = Mathf.RoundToInt(rect.height * pixelsPerPoint)
			};
		}

		// Token: 0x04000851 RID: 2129
		internal VisualElement owner;

		// Token: 0x04000852 RID: 2130
		internal RenderChainCommand prev;

		// Token: 0x04000853 RID: 2131
		internal RenderChainCommand next;

		// Token: 0x04000854 RID: 2132
		internal bool closing;

		// Token: 0x04000855 RID: 2133
		internal CommandType type;

		// Token: 0x04000856 RID: 2134
		internal State state;

		// Token: 0x04000857 RID: 2135
		internal MeshHandle mesh;

		// Token: 0x04000858 RID: 2136
		internal int indexOffset;

		// Token: 0x04000859 RID: 2137
		internal int indexCount;

		// Token: 0x0400085A RID: 2138
		internal Action callback;
	}
}
