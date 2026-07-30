using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000011 RID: 17
	[NativeHeader("Modules/IMGUI/GUIClip.h")]
	[NativeHeader("Modules/IMGUI/GUIState.h")]
	internal sealed class GUIClip
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000143 RID: 323
		internal static extern bool enabled
		{
			[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetEnabled")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00007244 File Offset: 0x00005444
		internal static Rect visibleRect
		{
			[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetVisibleRect")]
			get
			{
				Rect rect;
				GUIClip.get_visibleRect_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000725C File Offset: 0x0000545C
		internal static Rect topmostRect
		{
			[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetTopMostPhysicalRect")]
			get
			{
				Rect rect;
				GUIClip.get_topmostRect_Injected(out rect);
				return rect;
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00007271 File Offset: 0x00005471
		internal static void Internal_Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIClip.Internal_Push_Injected(ref screenRect, ref scrollOffset, ref renderOffset, resetOffset);
		}

		// Token: 0x06000147 RID: 327
		[MethodImpl(4096)]
		internal static extern void Internal_Pop();

		// Token: 0x06000148 RID: 328
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetCount")]
		[MethodImpl(4096)]
		internal static extern int Internal_GetCount();

		// Token: 0x06000149 RID: 329 RVA: 0x00007280 File Offset: 0x00005480
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetTopRect")]
		internal static Rect GetTopRect()
		{
			Rect rect;
			GUIClip.GetTopRect_Injected(out rect);
			return rect;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007298 File Offset: 0x00005498
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Unclip")]
		private static Vector2 Unclip_Vector2(Vector2 pos)
		{
			Vector2 vector;
			GUIClip.Unclip_Vector2_Injected(ref pos, out vector);
			return vector;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000072B0 File Offset: 0x000054B0
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Unclip")]
		private static Rect Unclip_Rect(Rect rect)
		{
			Rect rect2;
			GUIClip.Unclip_Rect_Injected(ref rect, out rect2);
			return rect2;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000072C8 File Offset: 0x000054C8
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Clip")]
		private static Vector2 Clip_Vector2(Vector2 absolutePos)
		{
			Vector2 vector;
			GUIClip.Clip_Vector2_Injected(ref absolutePos, out vector);
			return vector;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000072E0 File Offset: 0x000054E0
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.Clip")]
		private static Rect Internal_Clip_Rect(Rect absoluteRect)
		{
			Rect rect;
			GUIClip.Internal_Clip_Rect_Injected(ref absoluteRect, out rect);
			return rect;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000072F8 File Offset: 0x000054F8
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.UnclipToWindow")]
		private static Vector2 UnclipToWindow_Vector2(Vector2 pos)
		{
			Vector2 vector;
			GUIClip.UnclipToWindow_Vector2_Injected(ref pos, out vector);
			return vector;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007310 File Offset: 0x00005510
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.UnclipToWindow")]
		private static Rect UnclipToWindow_Rect(Rect rect)
		{
			Rect rect2;
			GUIClip.UnclipToWindow_Rect_Injected(ref rect, out rect2);
			return rect2;
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00007328 File Offset: 0x00005528
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.ClipToWindow")]
		private static Vector2 ClipToWindow_Vector2(Vector2 absolutePos)
		{
			Vector2 vector;
			GUIClip.ClipToWindow_Vector2_Injected(ref absolutePos, out vector);
			return vector;
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00007340 File Offset: 0x00005540
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.ClipToWindow")]
		private static Rect ClipToWindow_Rect(Rect absoluteRect)
		{
			Rect rect;
			GUIClip.ClipToWindow_Rect_Injected(ref absoluteRect, out rect);
			return rect;
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00007358 File Offset: 0x00005558
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetAbsoluteMousePosition")]
		private static Vector2 Internal_GetAbsoluteMousePosition()
		{
			Vector2 vector;
			GUIClip.Internal_GetAbsoluteMousePosition_Injected(out vector);
			return vector;
		}

		// Token: 0x06000153 RID: 339
		[MethodImpl(4096)]
		internal static extern void Reapply();

		// Token: 0x06000154 RID: 340 RVA: 0x00007370 File Offset: 0x00005570
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetUserMatrix")]
		internal static Matrix4x4 GetMatrix()
		{
			Matrix4x4 matrix4x;
			GUIClip.GetMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007385 File Offset: 0x00005585
		internal static void SetMatrix(Matrix4x4 m)
		{
			GUIClip.SetMatrix_Injected(ref m);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00007390 File Offset: 0x00005590
		[FreeFunction("GetGUIState().m_CanvasGUIState.m_GUIClipState.GetParentTransform")]
		internal static Matrix4x4 GetParentMatrix()
		{
			Matrix4x4 matrix4x;
			GUIClip.GetParentMatrix_Injected(out matrix4x);
			return matrix4x;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000073A5 File Offset: 0x000055A5
		internal static void Internal_PushParentClip(Matrix4x4 objectTransform, Rect clipRect)
		{
			GUIClip.Internal_PushParentClip_Injected(ref objectTransform, ref clipRect);
		}

		// Token: 0x06000158 RID: 344
		[MethodImpl(4096)]
		internal static extern void Internal_PopParentClip();

		// Token: 0x06000159 RID: 345 RVA: 0x000073B0 File Offset: 0x000055B0
		internal static void Push(Rect screenRect, Vector2 scrollOffset, Vector2 renderOffset, bool resetOffset)
		{
			GUIClip.Internal_Push(screenRect, scrollOffset, renderOffset, resetOffset);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x000073BD File Offset: 0x000055BD
		internal static void Pop()
		{
			GUIClip.Internal_Pop();
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000073C8 File Offset: 0x000055C8
		public static Vector2 Unclip(Vector2 pos)
		{
			return GUIClip.Unclip_Vector2(pos);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x000073E0 File Offset: 0x000055E0
		public static Rect Unclip(Rect rect)
		{
			return GUIClip.Unclip_Rect(rect);
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000073F8 File Offset: 0x000055F8
		public static Vector2 Clip(Vector2 absolutePos)
		{
			return GUIClip.Clip_Vector2(absolutePos);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00007410 File Offset: 0x00005610
		public static Rect Clip(Rect absoluteRect)
		{
			return GUIClip.Internal_Clip_Rect(absoluteRect);
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00007428 File Offset: 0x00005628
		public static Vector2 UnclipToWindow(Vector2 pos)
		{
			return GUIClip.UnclipToWindow_Vector2(pos);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00007440 File Offset: 0x00005640
		public static Rect UnclipToWindow(Rect rect)
		{
			return GUIClip.UnclipToWindow_Rect(rect);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00007458 File Offset: 0x00005658
		public static Vector2 ClipToWindow(Vector2 absolutePos)
		{
			return GUIClip.ClipToWindow_Vector2(absolutePos);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00007470 File Offset: 0x00005670
		public static Rect ClipToWindow(Rect absoluteRect)
		{
			return GUIClip.ClipToWindow_Rect(absoluteRect);
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00007488 File Offset: 0x00005688
		public static Vector2 GetAbsoluteMousePosition()
		{
			return GUIClip.Internal_GetAbsoluteMousePosition();
		}

		// Token: 0x06000165 RID: 357
		[MethodImpl(4096)]
		private static extern void get_visibleRect_Injected(out Rect ret);

		// Token: 0x06000166 RID: 358
		[MethodImpl(4096)]
		private static extern void get_topmostRect_Injected(out Rect ret);

		// Token: 0x06000167 RID: 359
		[MethodImpl(4096)]
		private static extern void Internal_Push_Injected(ref Rect screenRect, ref Vector2 scrollOffset, ref Vector2 renderOffset, bool resetOffset);

		// Token: 0x06000168 RID: 360
		[MethodImpl(4096)]
		private static extern void GetTopRect_Injected(out Rect ret);

		// Token: 0x06000169 RID: 361
		[MethodImpl(4096)]
		private static extern void Unclip_Vector2_Injected(ref Vector2 pos, out Vector2 ret);

		// Token: 0x0600016A RID: 362
		[MethodImpl(4096)]
		private static extern void Unclip_Rect_Injected(ref Rect rect, out Rect ret);

		// Token: 0x0600016B RID: 363
		[MethodImpl(4096)]
		private static extern void Clip_Vector2_Injected(ref Vector2 absolutePos, out Vector2 ret);

		// Token: 0x0600016C RID: 364
		[MethodImpl(4096)]
		private static extern void Internal_Clip_Rect_Injected(ref Rect absoluteRect, out Rect ret);

		// Token: 0x0600016D RID: 365
		[MethodImpl(4096)]
		private static extern void UnclipToWindow_Vector2_Injected(ref Vector2 pos, out Vector2 ret);

		// Token: 0x0600016E RID: 366
		[MethodImpl(4096)]
		private static extern void UnclipToWindow_Rect_Injected(ref Rect rect, out Rect ret);

		// Token: 0x0600016F RID: 367
		[MethodImpl(4096)]
		private static extern void ClipToWindow_Vector2_Injected(ref Vector2 absolutePos, out Vector2 ret);

		// Token: 0x06000170 RID: 368
		[MethodImpl(4096)]
		private static extern void ClipToWindow_Rect_Injected(ref Rect absoluteRect, out Rect ret);

		// Token: 0x06000171 RID: 369
		[MethodImpl(4096)]
		private static extern void Internal_GetAbsoluteMousePosition_Injected(out Vector2 ret);

		// Token: 0x06000172 RID: 370
		[MethodImpl(4096)]
		private static extern void GetMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000173 RID: 371
		[MethodImpl(4096)]
		private static extern void SetMatrix_Injected(ref Matrix4x4 m);

		// Token: 0x06000174 RID: 372
		[MethodImpl(4096)]
		private static extern void GetParentMatrix_Injected(out Matrix4x4 ret);

		// Token: 0x06000175 RID: 373
		[MethodImpl(4096)]
		private static extern void Internal_PushParentClip_Injected(ref Matrix4x4 objectTransform, ref Rect clipRect);

		// Token: 0x02000012 RID: 18
		internal struct ParentClipScope : IDisposable
		{
			// Token: 0x06000176 RID: 374 RVA: 0x0000749F File Offset: 0x0000569F
			public ParentClipScope(Matrix4x4 objectTransform, Rect clipRect)
			{
				this.m_Disposed = false;
				GUIClip.Internal_PushParentClip(objectTransform, clipRect);
			}

			// Token: 0x06000177 RID: 375 RVA: 0x000074B4 File Offset: 0x000056B4
			public void Dispose()
			{
				bool disposed = this.m_Disposed;
				if (!disposed)
				{
					this.m_Disposed = true;
					GUIClip.Internal_PopParentClip();
				}
			}

			// Token: 0x0400006C RID: 108
			private bool m_Disposed;
		}
	}
}
