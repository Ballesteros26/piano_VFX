using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000034 RID: 52
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule", "Unity.UIElements" })]
	internal class GUILayoutGroup : GUILayoutEntry
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0000D036 File Offset: 0x0000B236
		public override int marginLeft
		{
			get
			{
				return this.m_MarginLeft;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000D03E File Offset: 0x0000B23E
		public override int marginRight
		{
			get
			{
				return this.m_MarginRight;
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0000D046 File Offset: 0x0000B246
		public override int marginTop
		{
			get
			{
				return this.m_MarginTop;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000D04E File Offset: 0x0000B24E
		public override int marginBottom
		{
			get
			{
				return this.m_MarginBottom;
			}
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000D058 File Offset: 0x0000B258
		public GUILayoutGroup()
			: base(0f, 0f, 0f, 0f, GUIStyle.none)
		{
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000D110 File Offset: 0x0000B310
		public GUILayoutGroup(GUIStyle _style, GUILayoutOption[] options)
			: base(0f, 0f, 0f, 0f, _style)
		{
			bool flag = options != null;
			if (flag)
			{
				this.ApplyOptions(options);
			}
			this.m_MarginLeft = _style.margin.left;
			this.m_MarginRight = _style.margin.right;
			this.m_MarginTop = _style.margin.top;
			this.m_MarginBottom = _style.margin.bottom;
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000D218 File Offset: 0x0000B418
		public override void ApplyOptions(GUILayoutOption[] options)
		{
			bool flag = options == null;
			if (!flag)
			{
				base.ApplyOptions(options);
				foreach (GUILayoutOption guilayoutOption in options)
				{
					GUILayoutOption.Type type = guilayoutOption.type;
					switch (type)
					{
					case GUILayoutOption.Type.fixedWidth:
					case GUILayoutOption.Type.minWidth:
					case GUILayoutOption.Type.maxWidth:
						this.m_UserSpecifiedHeight = true;
						break;
					case GUILayoutOption.Type.fixedHeight:
					case GUILayoutOption.Type.minHeight:
					case GUILayoutOption.Type.maxHeight:
						this.m_UserSpecifiedWidth = true;
						break;
					default:
						if (type == GUILayoutOption.Type.spacing)
						{
							this.spacing = (float)((int)guilayoutOption.value);
						}
						break;
					}
				}
			}
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000D2A8 File Offset: 0x0000B4A8
		protected override void ApplyStyleSettings(GUIStyle style)
		{
			base.ApplyStyleSettings(style);
			RectOffset margin = style.margin;
			this.m_MarginLeft = margin.left;
			this.m_MarginRight = margin.right;
			this.m_MarginTop = margin.top;
			this.m_MarginBottom = margin.bottom;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000D2F5 File Offset: 0x0000B4F5
		public void ResetCursor()
		{
			this.m_Cursor = 0;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x0000D300 File Offset: 0x0000B500
		public Rect PeekNext()
		{
			bool flag = this.m_Cursor < this.entries.Count;
			Rect rect;
			if (flag)
			{
				GUILayoutEntry guilayoutEntry = this.entries[this.m_Cursor];
				rect = guilayoutEntry.rect;
			}
			else
			{
				bool flag2 = Event.current.type == EventType.Repaint;
				if (flag2)
				{
					throw new ArgumentException(string.Concat(new object[]
					{
						"Getting control ",
						this.m_Cursor,
						"'s position in a group with only ",
						this.entries.Count,
						" controls when doing ",
						Event.current.rawType,
						"\nAborting"
					}));
				}
				rect = GUILayoutEntry.kDummyRect;
			}
			return rect;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x0000D3C0 File Offset: 0x0000B5C0
		public GUILayoutEntry GetNext()
		{
			bool flag = this.m_Cursor < this.entries.Count;
			GUILayoutEntry guilayoutEntry2;
			if (flag)
			{
				GUILayoutEntry guilayoutEntry = this.entries[this.m_Cursor];
				this.m_Cursor++;
				guilayoutEntry2 = guilayoutEntry;
			}
			else
			{
				bool flag2 = Event.current.type == EventType.Repaint;
				if (flag2)
				{
					throw new ArgumentException(string.Concat(new object[]
					{
						"Getting control ",
						this.m_Cursor,
						"'s position in a group with only ",
						this.entries.Count,
						" controls when doing ",
						Event.current.rawType,
						"\nAborting"
					}));
				}
				guilayoutEntry2 = GUILayoutGroup.none;
			}
			return guilayoutEntry2;
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x0000D488 File Offset: 0x0000B688
		public Rect GetLast()
		{
			bool flag = this.m_Cursor == 0;
			Rect rect;
			if (flag)
			{
				bool flag2 = Event.current.type == EventType.Repaint;
				if (flag2)
				{
					Debug.LogError("You cannot call GetLast immediately after beginning a group.");
				}
				rect = GUILayoutEntry.kDummyRect;
			}
			else
			{
				bool flag3 = this.m_Cursor <= this.entries.Count;
				if (flag3)
				{
					GUILayoutEntry guilayoutEntry = this.entries[this.m_Cursor - 1];
					rect = guilayoutEntry.rect;
				}
				else
				{
					bool flag4 = Event.current.type == EventType.Repaint;
					if (flag4)
					{
						Debug.LogError(string.Concat(new object[]
						{
							"Getting control ",
							this.m_Cursor,
							"'s position in a group with only ",
							this.entries.Count,
							" controls when doing ",
							Event.current.rawType
						}));
					}
					rect = GUILayoutEntry.kDummyRect;
				}
			}
			return rect;
		}

		// Token: 0x060003E8 RID: 1000 RVA: 0x0000D57D File Offset: 0x0000B77D
		public void Add(GUILayoutEntry e)
		{
			this.entries.Add(e);
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x0000D590 File Offset: 0x0000B790
		public override void CalcWidth()
		{
			bool flag = this.entries.Count == 0;
			if (flag)
			{
				this.maxWidth = (this.minWidth = (float)base.style.padding.horizontal);
			}
			else
			{
				int num = 0;
				int num2 = 0;
				this.m_ChildMinWidth = 0f;
				this.m_ChildMaxWidth = 0f;
				this.m_StretchableCountX = 0;
				bool flag2 = true;
				bool flag3 = this.isVertical;
				if (flag3)
				{
					foreach (GUILayoutEntry guilayoutEntry in this.entries)
					{
						guilayoutEntry.CalcWidth();
						bool consideredForMargin = guilayoutEntry.consideredForMargin;
						if (consideredForMargin)
						{
							bool flag4 = !flag2;
							if (flag4)
							{
								num = Mathf.Min(guilayoutEntry.marginLeft, num);
								num2 = Mathf.Min(guilayoutEntry.marginRight, num2);
							}
							else
							{
								num = guilayoutEntry.marginLeft;
								num2 = guilayoutEntry.marginRight;
								flag2 = false;
							}
							this.m_ChildMinWidth = Mathf.Max(guilayoutEntry.minWidth + (float)guilayoutEntry.marginHorizontal, this.m_ChildMinWidth);
							this.m_ChildMaxWidth = Mathf.Max(guilayoutEntry.maxWidth + (float)guilayoutEntry.marginHorizontal, this.m_ChildMaxWidth);
						}
						this.m_StretchableCountX += guilayoutEntry.stretchWidth;
					}
					this.m_ChildMinWidth -= (float)(num + num2);
					this.m_ChildMaxWidth -= (float)(num + num2);
				}
				else
				{
					int num3 = 0;
					foreach (GUILayoutEntry guilayoutEntry2 in this.entries)
					{
						guilayoutEntry2.CalcWidth();
						bool consideredForMargin2 = guilayoutEntry2.consideredForMargin;
						if (consideredForMargin2)
						{
							bool flag5 = !flag2;
							int num4;
							if (flag5)
							{
								num4 = ((num3 > guilayoutEntry2.marginLeft) ? num3 : guilayoutEntry2.marginLeft);
							}
							else
							{
								num4 = 0;
								flag2 = false;
							}
							this.m_ChildMinWidth += guilayoutEntry2.minWidth + this.spacing + (float)num4;
							this.m_ChildMaxWidth += guilayoutEntry2.maxWidth + this.spacing + (float)num4;
							num3 = guilayoutEntry2.marginRight;
							this.m_StretchableCountX += guilayoutEntry2.stretchWidth;
						}
						else
						{
							this.m_ChildMinWidth += guilayoutEntry2.minWidth;
							this.m_ChildMaxWidth += guilayoutEntry2.maxWidth;
							this.m_StretchableCountX += guilayoutEntry2.stretchWidth;
						}
					}
					this.m_ChildMinWidth -= this.spacing;
					this.m_ChildMaxWidth -= this.spacing;
					bool flag6 = this.entries.Count != 0;
					if (flag6)
					{
						num = this.entries[0].marginLeft;
						num2 = num3;
					}
					else
					{
						num2 = (num = 0);
					}
				}
				bool flag7 = base.style != GUIStyle.none || this.m_UserSpecifiedWidth;
				float num5;
				float num6;
				if (flag7)
				{
					num5 = (float)Mathf.Max(base.style.padding.left, num);
					num6 = (float)Mathf.Max(base.style.padding.right, num2);
				}
				else
				{
					this.m_MarginLeft = num;
					this.m_MarginRight = num2;
					num6 = (num5 = 0f);
				}
				this.minWidth = Mathf.Max(this.minWidth, this.m_ChildMinWidth + num5 + num6);
				bool flag8 = this.maxWidth == 0f;
				if (flag8)
				{
					this.stretchWidth += this.m_StretchableCountX + (base.style.stretchWidth ? 1 : 0);
					this.maxWidth = this.m_ChildMaxWidth + num5 + num6;
				}
				else
				{
					this.stretchWidth = 0;
				}
				this.maxWidth = Mathf.Max(this.maxWidth, this.minWidth);
				bool flag9 = base.style.fixedWidth != 0f;
				if (flag9)
				{
					this.maxWidth = (this.minWidth = base.style.fixedWidth);
					this.stretchWidth = 0;
				}
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
		public override void SetHorizontal(float x, float width)
		{
			base.SetHorizontal(x, width);
			bool flag = this.resetCoords;
			if (flag)
			{
				x = 0f;
			}
			RectOffset padding = base.style.padding;
			bool flag2 = this.isVertical;
			if (flag2)
			{
				bool flag3 = base.style != GUIStyle.none;
				if (flag3)
				{
					foreach (GUILayoutEntry guilayoutEntry in this.entries)
					{
						float num = (float)Mathf.Max(guilayoutEntry.marginLeft, padding.left);
						float num2 = x + num;
						float num3 = width - (float)Mathf.Max(guilayoutEntry.marginRight, padding.right) - num;
						bool flag4 = guilayoutEntry.stretchWidth != 0;
						if (flag4)
						{
							guilayoutEntry.SetHorizontal(num2, num3);
						}
						else
						{
							guilayoutEntry.SetHorizontal(num2, Mathf.Clamp(num3, guilayoutEntry.minWidth, guilayoutEntry.maxWidth));
						}
					}
				}
				else
				{
					float num4 = x - (float)this.marginLeft;
					float num5 = width + (float)base.marginHorizontal;
					foreach (GUILayoutEntry guilayoutEntry2 in this.entries)
					{
						bool flag5 = guilayoutEntry2.stretchWidth != 0;
						if (flag5)
						{
							guilayoutEntry2.SetHorizontal(num4 + (float)guilayoutEntry2.marginLeft, num5 - (float)guilayoutEntry2.marginHorizontal);
						}
						else
						{
							guilayoutEntry2.SetHorizontal(num4 + (float)guilayoutEntry2.marginLeft, Mathf.Clamp(num5 - (float)guilayoutEntry2.marginHorizontal, guilayoutEntry2.minWidth, guilayoutEntry2.maxWidth));
						}
					}
				}
			}
			else
			{
				bool flag6 = base.style != GUIStyle.none;
				if (flag6)
				{
					float num6 = (float)padding.left;
					float num7 = (float)padding.right;
					bool flag7 = this.entries.Count != 0;
					if (flag7)
					{
						num6 = Mathf.Max(num6, (float)this.entries[0].marginLeft);
						num7 = Mathf.Max(num7, (float)this.entries[this.entries.Count - 1].marginRight);
					}
					x += num6;
					width -= num7 + num6;
				}
				float num8 = width - this.spacing * (float)(this.entries.Count - 1);
				float num9 = 0f;
				bool flag8 = this.m_ChildMinWidth != this.m_ChildMaxWidth;
				if (flag8)
				{
					num9 = Mathf.Clamp((num8 - this.m_ChildMinWidth) / (this.m_ChildMaxWidth - this.m_ChildMinWidth), 0f, 1f);
				}
				float num10 = 0f;
				bool flag9 = num8 > this.m_ChildMaxWidth;
				if (flag9)
				{
					bool flag10 = this.m_StretchableCountX > 0;
					if (flag10)
					{
						num10 = (num8 - this.m_ChildMaxWidth) / (float)this.m_StretchableCountX;
					}
				}
				int num11 = 0;
				bool flag11 = true;
				foreach (GUILayoutEntry guilayoutEntry3 in this.entries)
				{
					float num12 = Mathf.Lerp(guilayoutEntry3.minWidth, guilayoutEntry3.maxWidth, num9);
					num12 += num10 * (float)guilayoutEntry3.stretchWidth;
					bool consideredForMargin = guilayoutEntry3.consideredForMargin;
					if (consideredForMargin)
					{
						int num13 = guilayoutEntry3.marginLeft;
						bool flag12 = flag11;
						if (flag12)
						{
							num13 = 0;
							flag11 = false;
						}
						int num14 = ((num11 > num13) ? num11 : num13);
						x += (float)num14;
						num11 = guilayoutEntry3.marginRight;
					}
					guilayoutEntry3.SetHorizontal(Mathf.Round(x), Mathf.Round(num12));
					x += num12 + this.spacing;
				}
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000DDDC File Offset: 0x0000BFDC
		public override void CalcHeight()
		{
			bool flag = this.entries.Count == 0;
			if (flag)
			{
				this.maxHeight = (this.minHeight = (float)base.style.padding.vertical);
			}
			else
			{
				int num = 0;
				int num2 = 0;
				this.m_ChildMinHeight = 0f;
				this.m_ChildMaxHeight = 0f;
				this.m_StretchableCountY = 0;
				bool flag2 = this.isVertical;
				if (flag2)
				{
					int num3 = 0;
					bool flag3 = true;
					foreach (GUILayoutEntry guilayoutEntry in this.entries)
					{
						guilayoutEntry.CalcHeight();
						bool consideredForMargin = guilayoutEntry.consideredForMargin;
						if (consideredForMargin)
						{
							bool flag4 = !flag3;
							int num4;
							if (flag4)
							{
								num4 = Mathf.Max(num3, guilayoutEntry.marginTop);
							}
							else
							{
								num4 = 0;
								flag3 = false;
							}
							this.m_ChildMinHeight += guilayoutEntry.minHeight + this.spacing + (float)num4;
							this.m_ChildMaxHeight += guilayoutEntry.maxHeight + this.spacing + (float)num4;
							num3 = guilayoutEntry.marginBottom;
							this.m_StretchableCountY += guilayoutEntry.stretchHeight;
						}
						else
						{
							this.m_ChildMinHeight += guilayoutEntry.minHeight;
							this.m_ChildMaxHeight += guilayoutEntry.maxHeight;
							this.m_StretchableCountY += guilayoutEntry.stretchHeight;
						}
					}
					this.m_ChildMinHeight -= this.spacing;
					this.m_ChildMaxHeight -= this.spacing;
					bool flag5 = this.entries.Count != 0;
					if (flag5)
					{
						num = this.entries[0].marginTop;
						num2 = num3;
					}
					else
					{
						num = (num2 = 0);
					}
				}
				else
				{
					bool flag6 = true;
					foreach (GUILayoutEntry guilayoutEntry2 in this.entries)
					{
						guilayoutEntry2.CalcHeight();
						bool consideredForMargin2 = guilayoutEntry2.consideredForMargin;
						if (consideredForMargin2)
						{
							bool flag7 = !flag6;
							if (flag7)
							{
								num = Mathf.Min(guilayoutEntry2.marginTop, num);
								num2 = Mathf.Min(guilayoutEntry2.marginBottom, num2);
							}
							else
							{
								num = guilayoutEntry2.marginTop;
								num2 = guilayoutEntry2.marginBottom;
								flag6 = false;
							}
							this.m_ChildMinHeight = Mathf.Max(guilayoutEntry2.minHeight, this.m_ChildMinHeight);
							this.m_ChildMaxHeight = Mathf.Max(guilayoutEntry2.maxHeight, this.m_ChildMaxHeight);
						}
						this.m_StretchableCountY += guilayoutEntry2.stretchHeight;
					}
				}
				bool flag8 = base.style != GUIStyle.none || this.m_UserSpecifiedHeight;
				float num5;
				float num6;
				if (flag8)
				{
					num5 = (float)Mathf.Max(base.style.padding.top, num);
					num6 = (float)Mathf.Max(base.style.padding.bottom, num2);
				}
				else
				{
					this.m_MarginTop = num;
					this.m_MarginBottom = num2;
					num6 = (num5 = 0f);
				}
				this.minHeight = Mathf.Max(this.minHeight, this.m_ChildMinHeight + num5 + num6);
				bool flag9 = this.maxHeight == 0f;
				if (flag9)
				{
					this.stretchHeight += this.m_StretchableCountY + (base.style.stretchHeight ? 1 : 0);
					this.maxHeight = this.m_ChildMaxHeight + num5 + num6;
				}
				else
				{
					this.stretchHeight = 0;
				}
				this.maxHeight = Mathf.Max(this.maxHeight, this.minHeight);
				bool flag10 = base.style.fixedHeight != 0f;
				if (flag10)
				{
					this.maxHeight = (this.minHeight = base.style.fixedHeight);
					this.stretchHeight = 0;
				}
			}
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0000E204 File Offset: 0x0000C404
		public override void SetVertical(float y, float height)
		{
			base.SetVertical(y, height);
			bool flag = this.entries.Count == 0;
			if (!flag)
			{
				RectOffset padding = base.style.padding;
				bool flag2 = this.resetCoords;
				if (flag2)
				{
					y = 0f;
				}
				bool flag3 = this.isVertical;
				if (flag3)
				{
					bool flag4 = base.style != GUIStyle.none;
					if (flag4)
					{
						float num = (float)padding.top;
						float num2 = (float)padding.bottom;
						bool flag5 = this.entries.Count != 0;
						if (flag5)
						{
							num = Mathf.Max(num, (float)this.entries[0].marginTop);
							num2 = Mathf.Max(num2, (float)this.entries[this.entries.Count - 1].marginBottom);
						}
						y += num;
						height -= num2 + num;
					}
					float num3 = height - this.spacing * (float)(this.entries.Count - 1);
					float num4 = 0f;
					bool flag6 = this.m_ChildMinHeight != this.m_ChildMaxHeight;
					if (flag6)
					{
						num4 = Mathf.Clamp((num3 - this.m_ChildMinHeight) / (this.m_ChildMaxHeight - this.m_ChildMinHeight), 0f, 1f);
					}
					float num5 = 0f;
					bool flag7 = num3 > this.m_ChildMaxHeight;
					if (flag7)
					{
						bool flag8 = this.m_StretchableCountY > 0;
						if (flag8)
						{
							num5 = (num3 - this.m_ChildMaxHeight) / (float)this.m_StretchableCountY;
						}
					}
					int num6 = 0;
					bool flag9 = true;
					foreach (GUILayoutEntry guilayoutEntry in this.entries)
					{
						float num7 = Mathf.Lerp(guilayoutEntry.minHeight, guilayoutEntry.maxHeight, num4);
						num7 += num5 * (float)guilayoutEntry.stretchHeight;
						bool consideredForMargin = guilayoutEntry.consideredForMargin;
						if (consideredForMargin)
						{
							int num8 = guilayoutEntry.marginTop;
							bool flag10 = flag9;
							if (flag10)
							{
								num8 = 0;
								flag9 = false;
							}
							int num9 = ((num6 > num8) ? num6 : num8);
							y += (float)num9;
							num6 = guilayoutEntry.marginBottom;
						}
						guilayoutEntry.SetVertical(Mathf.Round(y), Mathf.Round(num7));
						y += num7 + this.spacing;
					}
				}
				else
				{
					bool flag11 = base.style != GUIStyle.none;
					if (flag11)
					{
						foreach (GUILayoutEntry guilayoutEntry2 in this.entries)
						{
							float num10 = (float)Mathf.Max(guilayoutEntry2.marginTop, padding.top);
							float num11 = y + num10;
							float num12 = height - (float)Mathf.Max(guilayoutEntry2.marginBottom, padding.bottom) - num10;
							bool flag12 = guilayoutEntry2.stretchHeight != 0;
							if (flag12)
							{
								guilayoutEntry2.SetVertical(num11, num12);
							}
							else
							{
								guilayoutEntry2.SetVertical(num11, Mathf.Clamp(num12, guilayoutEntry2.minHeight, guilayoutEntry2.maxHeight));
							}
						}
					}
					else
					{
						float num13 = y - (float)this.marginTop;
						float num14 = height + (float)base.marginVertical;
						foreach (GUILayoutEntry guilayoutEntry3 in this.entries)
						{
							bool flag13 = guilayoutEntry3.stretchHeight != 0;
							if (flag13)
							{
								guilayoutEntry3.SetVertical(num13 + (float)guilayoutEntry3.marginTop, num14 - (float)guilayoutEntry3.marginVertical);
							}
							else
							{
								guilayoutEntry3.SetVertical(num13 + (float)guilayoutEntry3.marginTop, Mathf.Clamp(num14 - (float)guilayoutEntry3.marginVertical, guilayoutEntry3.minHeight, guilayoutEntry3.maxHeight));
							}
						}
					}
				}
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x0000E600 File Offset: 0x0000C800
		public override string ToString()
		{
			string text = "";
			string text2 = "";
			for (int i = 0; i < GUILayoutEntry.indent; i++)
			{
				text2 += " ";
			}
			text = string.Concat(new object[]
			{
				text,
				base.ToString(),
				" Margins: ",
				this.m_ChildMinHeight,
				" {\n"
			});
			GUILayoutEntry.indent += 4;
			foreach (GUILayoutEntry guilayoutEntry in this.entries)
			{
				text = text + guilayoutEntry + "\n";
			}
			text = text + text2 + "}";
			GUILayoutEntry.indent -= 4;
			return text;
		}

		// Token: 0x040000FD RID: 253
		public List<GUILayoutEntry> entries = new List<GUILayoutEntry>();

		// Token: 0x040000FE RID: 254
		public bool isVertical = true;

		// Token: 0x040000FF RID: 255
		public bool resetCoords = false;

		// Token: 0x04000100 RID: 256
		public float spacing = 0f;

		// Token: 0x04000101 RID: 257
		public bool sameSize = true;

		// Token: 0x04000102 RID: 258
		public bool isWindow = false;

		// Token: 0x04000103 RID: 259
		public int windowID = -1;

		// Token: 0x04000104 RID: 260
		private int m_Cursor = 0;

		// Token: 0x04000105 RID: 261
		protected int m_StretchableCountX = 100;

		// Token: 0x04000106 RID: 262
		protected int m_StretchableCountY = 100;

		// Token: 0x04000107 RID: 263
		protected bool m_UserSpecifiedWidth = false;

		// Token: 0x04000108 RID: 264
		protected bool m_UserSpecifiedHeight = false;

		// Token: 0x04000109 RID: 265
		protected float m_ChildMinWidth = 100f;

		// Token: 0x0400010A RID: 266
		protected float m_ChildMaxWidth = 100f;

		// Token: 0x0400010B RID: 267
		protected float m_ChildMinHeight = 100f;

		// Token: 0x0400010C RID: 268
		protected float m_ChildMaxHeight = 100f;

		// Token: 0x0400010D RID: 269
		protected int m_MarginLeft;

		// Token: 0x0400010E RID: 270
		protected int m_MarginRight;

		// Token: 0x0400010F RID: 271
		protected int m_MarginTop;

		// Token: 0x04000110 RID: 272
		protected int m_MarginBottom;

		// Token: 0x04000111 RID: 273
		private static readonly GUILayoutEntry none = new GUILayoutEntry(0f, 1f, 0f, 1f, GUIStyle.none);
	}
}
