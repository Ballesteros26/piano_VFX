using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000021 RID: 33
	[NativeHeader("Modules/IMGUI/GUILayoutUtility.bindings.h")]
	public class GUILayoutUtility
	{
		// Token: 0x0600023F RID: 575 RVA: 0x000090D4 File Offset: 0x000072D4
		private static Rect Internal_GetWindowRect(int windowID)
		{
			Rect rect;
			GUILayoutUtility.Internal_GetWindowRect_Injected(windowID, out rect);
			return rect;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000090EA File Offset: 0x000072EA
		private static void Internal_MoveWindow(int windowID, Rect r)
		{
			GUILayoutUtility.Internal_MoveWindow_Injected(windowID, ref r);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000090F4 File Offset: 0x000072F4
		internal static Rect GetWindowsBounds()
		{
			Rect rect;
			GUILayoutUtility.GetWindowsBounds_Injected(out rect);
			return rect;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00009109 File Offset: 0x00007309
		internal static void CleanupRoots()
		{
			GUILayoutUtility.s_SpaceStyle = null;
			GUILayoutUtility.s_StoredLayouts.Clear();
			GUILayoutUtility.s_StoredWindows.Clear();
			GUILayoutUtility.current = new GUILayoutUtility.LayoutCache(-1);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009134 File Offset: 0x00007334
		internal static GUILayoutUtility.LayoutCache SelectIDList(int instanceID, bool isWindow)
		{
			Dictionary<int, GUILayoutUtility.LayoutCache> dictionary = (isWindow ? GUILayoutUtility.s_StoredWindows : GUILayoutUtility.s_StoredLayouts);
			GUILayoutUtility.LayoutCache layoutCache;
			bool flag = !dictionary.TryGetValue(instanceID, ref layoutCache);
			if (flag)
			{
				layoutCache = new GUILayoutUtility.LayoutCache(instanceID);
				dictionary[instanceID] = layoutCache;
			}
			GUILayoutUtility.current.topLevel = layoutCache.topLevel;
			GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
			GUILayoutUtility.current.windows = layoutCache.windows;
			return layoutCache;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000091AC File Offset: 0x000073AC
		internal static void Begin(int instanceID)
		{
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.SelectIDList(instanceID, false);
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				GUILayoutUtility.current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (layoutCache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = layoutCache.topLevel;
				GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
				GUILayoutUtility.current.windows = layoutCache.windows;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00009264 File Offset: 0x00007464
		internal static void BeginContainer(GUILayoutUtility.LayoutCache cache)
		{
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				cache.topLevel = new GUILayoutGroup();
				cache.layoutGroups.Clear();
				cache.layoutGroups.Push(cache.topLevel);
				cache.windows = new GUILayoutGroup();
			}
			GUILayoutUtility.current.topLevel = cache.topLevel;
			GUILayoutUtility.current.layoutGroups = cache.layoutGroups;
			GUILayoutUtility.current.windows = cache.windows;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000092EC File Offset: 0x000074EC
		internal static void BeginWindow(int windowID, GUIStyle style, GUILayoutOption[] options)
		{
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.SelectIDList(windowID, true);
			bool flag = Event.current.type == EventType.Layout;
			if (flag)
			{
				GUILayoutUtility.current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.topLevel.style = style;
				GUILayoutUtility.current.topLevel.windowID = windowID;
				bool flag2 = options != null;
				if (flag2)
				{
					GUILayoutUtility.current.topLevel.ApplyOptions(options);
				}
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (layoutCache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = layoutCache.topLevel;
				GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
				GUILayoutUtility.current.windows = layoutCache.windows;
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00002201 File Offset: 0x00000401
		[Obsolete("BeginGroup has no effect and will be removed", false)]
		public static void BeginGroup(string GroupName)
		{
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00002201 File Offset: 0x00000401
		[Obsolete("EndGroup has no effect and will be removed", false)]
		public static void EndGroup(string groupName)
		{
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000093E0 File Offset: 0x000075E0
		internal static void Layout()
		{
			bool flag = GUILayoutUtility.current.topLevel.windowID == -1;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, Mathf.Min((float)Screen.width / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxWidth));
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxHeight));
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				GUILayoutUtility.LayoutSingleGroup(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000094C8 File Offset: 0x000076C8
		internal static void LayoutFromEditorWindow()
		{
			bool flag = GUILayoutUtility.current.topLevel != null;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, (float)Screen.width / GUIUtility.pixelsPerPoint);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, (float)Screen.height / GUIUtility.pixelsPerPoint);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				Debug.LogError("GUILayout state invalid. Verify that all layout begin/end calls match.");
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000956C File Offset: 0x0000776C
		internal static void LayoutFromContainer(float w, float h)
		{
			bool flag = GUILayoutUtility.current.topLevel != null;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, w);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, h);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				Debug.LogError("GUILayout state invalid. Verify that all layout begin/end calls match.");
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000095F8 File Offset: 0x000077F8
		internal static float LayoutFromInspector(float width)
		{
			bool flag = GUILayoutUtility.current.topLevel != null && GUILayoutUtility.current.topLevel.windowID == -1;
			float num;
			if (flag)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, width);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height / GUIUtility.pixelsPerPoint, GUILayoutUtility.current.topLevel.maxHeight));
				float minHeight = GUILayoutUtility.current.topLevel.minHeight;
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
				num = minHeight;
			}
			else
			{
				bool flag2 = GUILayoutUtility.current.topLevel != null;
				if (flag2)
				{
					GUILayoutUtility.LayoutSingleGroup(GUILayoutUtility.current.topLevel);
				}
				num = 0f;
			}
			return num;
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000096E8 File Offset: 0x000078E8
		internal static void LayoutFreeGroup(GUILayoutGroup toplevel)
		{
			foreach (GUILayoutEntry guilayoutEntry in toplevel.entries)
			{
				GUILayoutGroup guilayoutGroup = (GUILayoutGroup)guilayoutEntry;
				GUILayoutUtility.LayoutSingleGroup(guilayoutGroup);
			}
			toplevel.ResetCursor();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x0000974C File Offset: 0x0000794C
		private static void LayoutSingleGroup(GUILayoutGroup i)
		{
			bool flag = !i.isWindow;
			if (flag)
			{
				float minWidth = i.minWidth;
				float maxWidth = i.maxWidth;
				i.CalcWidth();
				i.SetHorizontal(i.rect.x, Mathf.Clamp(i.maxWidth, minWidth, maxWidth));
				float minHeight = i.minHeight;
				float maxHeight = i.maxHeight;
				i.CalcHeight();
				i.SetVertical(i.rect.y, Mathf.Clamp(i.maxHeight, minHeight, maxHeight));
			}
			else
			{
				i.CalcWidth();
				Rect rect = GUILayoutUtility.Internal_GetWindowRect(i.windowID);
				i.SetHorizontal(rect.x, Mathf.Clamp(rect.width, i.minWidth, i.maxWidth));
				i.CalcHeight();
				i.SetVertical(rect.y, Mathf.Clamp(rect.height, i.minHeight, i.maxHeight));
				GUILayoutUtility.Internal_MoveWindow(i.windowID, i.rect);
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009850 File Offset: 0x00007A50
		[SecuritySafeCritical]
		private static GUILayoutGroup CreateGUILayoutGroupInstanceOfType(Type LayoutType)
		{
			bool flag = !typeof(GUILayoutGroup).IsAssignableFrom(LayoutType);
			if (flag)
			{
				throw new ArgumentException("LayoutType needs to be of type GUILayoutGroup", "LayoutType");
			}
			return (GUILayoutGroup)Activator.CreateInstance(LayoutType);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009894 File Offset: 0x00007A94
		internal static GUILayoutGroup BeginLayoutGroup(GUIStyle style, GUILayoutOption[] options, Type layoutType)
		{
			EventType type = Event.current.type;
			GUILayoutGroup guilayoutGroup;
			if (type != EventType.Layout && type != EventType.Used)
			{
				guilayoutGroup = GUILayoutUtility.current.topLevel.GetNext() as GUILayoutGroup;
				bool flag = guilayoutGroup == null;
				if (flag)
				{
					throw new ExitGUIException("GUILayout: Mismatched LayoutGroup." + Event.current.type);
				}
				guilayoutGroup.ResetCursor();
			}
			else
			{
				guilayoutGroup = GUILayoutUtility.CreateGUILayoutGroupInstanceOfType(layoutType);
				guilayoutGroup.style = style;
				bool flag2 = options != null;
				if (flag2)
				{
					guilayoutGroup.ApplyOptions(options);
				}
				GUILayoutUtility.current.topLevel.Add(guilayoutGroup);
			}
			GUILayoutUtility.current.layoutGroups.Push(guilayoutGroup);
			GUILayoutUtility.current.topLevel = guilayoutGroup;
			return guilayoutGroup;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00009954 File Offset: 0x00007B54
		internal static void EndLayoutGroup()
		{
			bool flag = GUILayoutUtility.current.layoutGroups.Count == 0;
			if (flag)
			{
				Debug.LogError("EndLayoutGroup: BeginLayoutGroup must be called first.");
			}
			else
			{
				GUILayoutUtility.current.layoutGroups.Pop();
				bool flag2 = 0 < GUILayoutUtility.current.layoutGroups.Count;
				if (flag2)
				{
					GUILayoutUtility.current.topLevel = (GUILayoutGroup)GUILayoutUtility.current.layoutGroups.Peek();
				}
				else
				{
					GUILayoutUtility.current.topLevel = new GUILayoutGroup();
				}
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000099DC File Offset: 0x00007BDC
		internal static GUILayoutGroup BeginLayoutArea(GUIStyle style, Type layoutType)
		{
			EventType type = Event.current.type;
			GUILayoutGroup guilayoutGroup;
			if (type != EventType.Layout && type != EventType.Used)
			{
				guilayoutGroup = GUILayoutUtility.current.windows.GetNext() as GUILayoutGroup;
				bool flag = guilayoutGroup == null;
				if (flag)
				{
					throw new ExitGUIException("GUILayout: Mismatched LayoutGroup." + Event.current.type);
				}
				guilayoutGroup.ResetCursor();
			}
			else
			{
				guilayoutGroup = GUILayoutUtility.CreateGUILayoutGroupInstanceOfType(layoutType);
				guilayoutGroup.style = style;
				GUILayoutUtility.current.windows.Add(guilayoutGroup);
			}
			GUILayoutUtility.current.layoutGroups.Push(guilayoutGroup);
			GUILayoutUtility.current.topLevel = guilayoutGroup;
			return guilayoutGroup;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009A8C File Offset: 0x00007C8C
		internal static GUILayoutGroup DoBeginLayoutArea(GUIStyle style, Type layoutType)
		{
			return GUILayoutUtility.BeginLayoutArea(style, layoutType);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00009AA5 File Offset: 0x00007CA5
		internal static GUILayoutGroup topLevel
		{
			get
			{
				return GUILayoutUtility.current.topLevel;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009AB4 File Offset: 0x00007CB4
		public static Rect GetRect(GUIContent content, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(content, style, null);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009AD0 File Offset: 0x00007CD0
		public static Rect GetRect(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(content, style, options);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009AEC File Offset: 0x00007CEC
		private static Rect DoGetRect(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			EventType type = Event.current.type;
			Rect rect;
			if (type != EventType.Layout)
			{
				if (type != EventType.Used)
				{
					GUILayoutEntry next = GUILayoutUtility.current.topLevel.GetNext();
					rect = next.rect;
				}
				else
				{
					rect = GUILayoutUtility.kDummyRect;
				}
			}
			else
			{
				bool isHeightDependantOnWidth = style.isHeightDependantOnWidth;
				if (isHeightDependantOnWidth)
				{
					GUILayoutUtility.current.topLevel.Add(new GUIWordWrapSizer(style, content, options));
				}
				else
				{
					Vector2 vector = new Vector2(0f, 0f);
					bool flag = options != null;
					if (flag)
					{
						foreach (GUILayoutOption guilayoutOption in options)
						{
							GUILayoutOption.Type type2 = guilayoutOption.type;
							if (type2 != GUILayoutOption.Type.maxWidth)
							{
								if (type2 == GUILayoutOption.Type.maxHeight)
								{
									vector.y = (float)guilayoutOption.value;
								}
							}
							else
							{
								vector.x = (float)guilayoutOption.value;
							}
						}
					}
					Vector2 vector2 = style.CalcSizeWithConstraints(content, vector);
					vector2.x = Mathf.Ceil(vector2.x);
					vector2.y = Mathf.Ceil(vector2.y);
					GUILayoutUtility.current.topLevel.Add(new GUILayoutEntry(vector2.x, vector2.x, vector2.y, vector2.y, style, options));
				}
				rect = GUILayoutUtility.kDummyRect;
			}
			return rect;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009C58 File Offset: 0x00007E58
		public static Rect GetRect(float width, float height)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, GUIStyle.none, null);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009C7C File Offset: 0x00007E7C
		public static Rect GetRect(float width, float height, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, style, null);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009C9C File Offset: 0x00007E9C
		public static Rect GetRect(float width, float height, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, GUIStyle.none, options);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009CC0 File Offset: 0x00007EC0
		public static Rect GetRect(float width, float height, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, style, options);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009CE0 File Offset: 0x00007EE0
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, null);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009D04 File Offset: 0x00007F04
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, null);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009D24 File Offset: 0x00007F24
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, options);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009D48 File Offset: 0x00007F48
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, options);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009D68 File Offset: 0x00007F68
		private static Rect DoGetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, GUILayoutOption[] options)
		{
			EventType type = Event.current.type;
			Rect rect;
			if (type != EventType.Layout)
			{
				if (type != EventType.Used)
				{
					rect = GUILayoutUtility.current.topLevel.GetNext().rect;
				}
				else
				{
					rect = GUILayoutUtility.kDummyRect;
				}
			}
			else
			{
				GUILayoutUtility.current.topLevel.Add(new GUILayoutEntry(minWidth, maxWidth, minHeight, maxHeight, style, options));
				rect = GUILayoutUtility.kDummyRect;
			}
			return rect;
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00009DD4 File Offset: 0x00007FD4
		public static Rect GetLastRect()
		{
			EventType type = Event.current.type;
			Rect last;
			if (type != EventType.Layout && type != EventType.Used)
			{
				last = GUILayoutUtility.current.topLevel.GetLast();
			}
			else
			{
				last = GUILayoutUtility.kDummyRect;
			}
			return last;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009E18 File Offset: 0x00008018
		public static Rect GetAspectRect(float aspect)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, null);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009E34 File Offset: 0x00008034
		public static Rect GetAspectRect(float aspect, GUIStyle style)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, null);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009E50 File Offset: 0x00008050
		public static Rect GetAspectRect(float aspect, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, options);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009E6C File Offset: 0x0000806C
		public static Rect GetAspectRect(float aspect, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, options);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009E88 File Offset: 0x00008088
		private static Rect DoGetAspectRect(float aspect, GUILayoutOption[] options)
		{
			EventType type = Event.current.type;
			Rect rect;
			if (type != EventType.Layout)
			{
				if (type != EventType.Used)
				{
					rect = GUILayoutUtility.current.topLevel.GetNext().rect;
				}
				else
				{
					rect = GUILayoutUtility.kDummyRect;
				}
			}
			else
			{
				GUILayoutUtility.current.topLevel.Add(new GUIAspectSizer(aspect, options));
				rect = GUILayoutUtility.kDummyRect;
			}
			return rect;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000267 RID: 615 RVA: 0x00009EF0 File Offset: 0x000080F0
		internal static GUIStyle spaceStyle
		{
			get
			{
				bool flag = GUILayoutUtility.s_SpaceStyle == null;
				if (flag)
				{
					GUILayoutUtility.s_SpaceStyle = new GUIStyle();
				}
				GUILayoutUtility.s_SpaceStyle.stretchWidth = false;
				return GUILayoutUtility.s_SpaceStyle;
			}
		}

		// Token: 0x0600026A RID: 618
		[MethodImpl(4096)]
		private static extern void Internal_GetWindowRect_Injected(int windowID, out Rect ret);

		// Token: 0x0600026B RID: 619
		[MethodImpl(4096)]
		private static extern void Internal_MoveWindow_Injected(int windowID, ref Rect r);

		// Token: 0x0600026C RID: 620
		[MethodImpl(4096)]
		private static extern void GetWindowsBounds_Injected(out Rect ret);

		// Token: 0x04000093 RID: 147
		private static readonly Dictionary<int, GUILayoutUtility.LayoutCache> s_StoredLayouts = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		// Token: 0x04000094 RID: 148
		private static readonly Dictionary<int, GUILayoutUtility.LayoutCache> s_StoredWindows = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		// Token: 0x04000095 RID: 149
		internal static GUILayoutUtility.LayoutCache current = new GUILayoutUtility.LayoutCache(-1);

		// Token: 0x04000096 RID: 150
		internal static readonly Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000097 RID: 151
		private static GUIStyle s_SpaceStyle;

		// Token: 0x02000022 RID: 34
		[DebuggerDisplay("id={id}, groups={layoutGroups.Count}")]
		internal sealed class LayoutCache
		{
			// Token: 0x1700003F RID: 63
			// (get) Token: 0x0600026D RID: 621 RVA: 0x00009F68 File Offset: 0x00008168
			// (set) Token: 0x0600026E RID: 622 RVA: 0x00009F70 File Offset: 0x00008170
			internal int id { get; private set; }

			// Token: 0x0600026F RID: 623 RVA: 0x00009F7C File Offset: 0x0000817C
			internal LayoutCache(int instanceID = -1)
			{
				this.id = instanceID;
				this.layoutGroups.Push(this.topLevel);
			}

			// Token: 0x06000270 RID: 624 RVA: 0x00009FCC File Offset: 0x000081CC
			internal LayoutCache(GUILayoutUtility.LayoutCache other)
			{
				this.id = other.id;
				this.topLevel = other.topLevel;
				this.layoutGroups = other.layoutGroups;
				this.windows = other.windows;
			}

			// Token: 0x06000271 RID: 625 RVA: 0x0000A034 File Offset: 0x00008234
			public void ResetCursor()
			{
				this.windows.ResetCursor();
				this.topLevel.ResetCursor();
				foreach (object obj in this.layoutGroups)
				{
					((GUILayoutGroup)obj).ResetCursor();
				}
			}

			// Token: 0x04000099 RID: 153
			internal GUILayoutGroup topLevel = new GUILayoutGroup();

			// Token: 0x0400009A RID: 154
			internal GenericStack layoutGroups = new GenericStack();

			// Token: 0x0400009B RID: 155
			internal GUILayoutGroup windows = new GUILayoutGroup();
		}
	}
}
