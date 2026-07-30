using System;
using System.Collections.Generic;
using UnityEngine.UI.Collections;

namespace UnityEngine.UI
{
	// Token: 0x02000013 RID: 19
	public class GraphicRegistry
	{
		// Token: 0x060000FB RID: 251 RVA: 0x000064B3 File Offset: 0x000046B3
		protected GraphicRegistry()
		{
			GC.KeepAlive(new Dictionary<Graphic, int>());
			GC.KeepAlive(new Dictionary<ICanvasElement, int>());
			GC.KeepAlive(new Dictionary<IClipper, int>());
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000064E4 File Offset: 0x000046E4
		public static GraphicRegistry instance
		{
			get
			{
				if (GraphicRegistry.s_Instance == null)
				{
					GraphicRegistry.s_Instance = new GraphicRegistry();
				}
				return GraphicRegistry.s_Instance;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000064FC File Offset: 0x000046FC
		public static void RegisterGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null)
			{
				return;
			}
			IndexedSet<Graphic> indexedSet;
			GraphicRegistry.instance.m_Graphics.TryGetValue(c, out indexedSet);
			if (indexedSet != null)
			{
				indexedSet.AddUnique(graphic);
				return;
			}
			indexedSet = new IndexedSet<Graphic>();
			indexedSet.Add(graphic);
			GraphicRegistry.instance.m_Graphics.Add(c, indexedSet);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00006550 File Offset: 0x00004750
		public static void UnregisterGraphicForCanvas(Canvas c, Graphic graphic)
		{
			if (c == null)
			{
				return;
			}
			IndexedSet<Graphic> indexedSet;
			if (GraphicRegistry.instance.m_Graphics.TryGetValue(c, out indexedSet))
			{
				indexedSet.Remove(graphic);
				if (indexedSet.Count == 0)
				{
					GraphicRegistry.instance.m_Graphics.Remove(c);
				}
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000659C File Offset: 0x0000479C
		public static IList<Graphic> GetGraphicsForCanvas(Canvas canvas)
		{
			IndexedSet<Graphic> indexedSet;
			if (GraphicRegistry.instance.m_Graphics.TryGetValue(canvas, out indexedSet))
			{
				return indexedSet;
			}
			return GraphicRegistry.s_EmptyList;
		}

		// Token: 0x04000069 RID: 105
		private static GraphicRegistry s_Instance;

		// Token: 0x0400006A RID: 106
		private readonly Dictionary<Canvas, IndexedSet<Graphic>> m_Graphics = new Dictionary<Canvas, IndexedSet<Graphic>>();

		// Token: 0x0400006B RID: 107
		private static readonly List<Graphic> s_EmptyList = new List<Graphic>();
	}
}
