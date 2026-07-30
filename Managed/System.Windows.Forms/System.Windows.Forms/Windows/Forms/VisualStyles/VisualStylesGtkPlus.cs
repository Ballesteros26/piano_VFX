using System;
using System.Drawing;

namespace System.Windows.Forms.VisualStyles
{
	// Token: 0x0200062A RID: 1578
	internal class VisualStylesGtkPlus : IVisualStyles
	{
		// Token: 0x06005004 RID: 20484 RVA: 0x00138498 File Offset: 0x00136698
		public static bool Initialize()
		{
			return GtkPlus.Initialize();
		}

		// Token: 0x17001554 RID: 5460
		// (get) Token: 0x06005005 RID: 20485 RVA: 0x001384A0 File Offset: 0x001366A0
		private static GtkPlus GtkPlus
		{
			get
			{
				return GtkPlus.Instance;
			}
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x001384A8 File Offset: 0x001366A8
		public int UxThemeCloseThemeData(IntPtr hTheme)
		{
			return 0;
		}

		// Token: 0x06005007 RID: 20487 RVA: 0x001384AC File Offset: 0x001366AC
		public int UxThemeDrawThemeParentBackground(IDeviceContext dc, Rectangle bounds, Control childControl)
		{
			return 1;
		}

		// Token: 0x06005008 RID: 20488 RVA: 0x001384B0 File Offset: 0x001366B0
		public int UxThemeDrawThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, Rectangle clipRectangle)
		{
			return (!this.DrawBackground((VisualStylesGtkPlus.ThemeHandle)(int)hTheme, dc, iPartId, iStateId, bounds, clipRectangle, Rectangle.Empty)) ? 1 : 0;
		}

		// Token: 0x06005009 RID: 20489 RVA: 0x001384E4 File Offset: 0x001366E4
		public int UxThemeDrawThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds)
		{
			return this.UxThemeDrawThemeBackground(hTheme, dc, iPartId, iStateId, bounds, bounds);
		}

		// Token: 0x0600500A RID: 20490 RVA: 0x001384F8 File Offset: 0x001366F8
		private bool DrawBackground(VisualStylesGtkPlus.ThemeHandle themeHandle, IDeviceContext dc, int part, int state, Rectangle bounds, Rectangle clipRectangle, Rectangle excludedArea)
		{
			switch (themeHandle)
			{
			case VisualStylesGtkPlus.ThemeHandle.BUTTON:
				switch (part)
				{
				case 1:
				{
					GtkPlusState gtkPlusState;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						break;
					case 5:
						gtkPlusState = GtkPlusState.Normal;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.ButtonPaint(dc, bounds, clipRectangle, state == 5, gtkPlusState);
					return true;
				}
				case 2:
				{
					GtkPlusState gtkPlusState;
					GtkPlusToggleButtonValue gtkPlusToggleButtonValue;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 5:
						gtkPlusState = GtkPlusState.Normal;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					case 6:
						gtkPlusState = GtkPlusState.Hot;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					case 7:
						gtkPlusState = GtkPlusState.Pressed;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					case 8:
						gtkPlusState = GtkPlusState.Disabled;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.RadioButtonPaint(dc, bounds, clipRectangle, gtkPlusState, gtkPlusToggleButtonValue);
					return true;
				}
				case 3:
				{
					GtkPlusState gtkPlusState;
					GtkPlusToggleButtonValue gtkPlusToggleButtonValue;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Unchecked;
						break;
					case 5:
						gtkPlusState = GtkPlusState.Normal;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					case 6:
						gtkPlusState = GtkPlusState.Hot;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					case 7:
						gtkPlusState = GtkPlusState.Pressed;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					case 8:
						gtkPlusState = GtkPlusState.Disabled;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Checked;
						break;
					case 9:
						gtkPlusState = GtkPlusState.Normal;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Mixed;
						break;
					case 10:
						gtkPlusState = GtkPlusState.Hot;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Mixed;
						break;
					case 11:
						gtkPlusState = GtkPlusState.Pressed;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Mixed;
						break;
					case 12:
						gtkPlusState = GtkPlusState.Disabled;
						gtkPlusToggleButtonValue = GtkPlusToggleButtonValue.Mixed;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.CheckBoxPaint(dc, bounds, clipRectangle, gtkPlusState, gtkPlusToggleButtonValue);
					return true;
				}
				case 4:
				{
					GtkPlusState gtkPlusState;
					if (state != 1)
					{
						if (state != 2)
						{
							return false;
						}
						gtkPlusState = GtkPlusState.Disabled;
					}
					else
					{
						gtkPlusState = GtkPlusState.Normal;
					}
					VisualStylesGtkPlus.GtkPlus.GroupBoxPaint(dc, bounds, excludedArea, gtkPlusState);
					return true;
				}
				default:
					return false;
				}
				break;
			case VisualStylesGtkPlus.ThemeHandle.COMBOBOX:
				switch (part)
				{
				case 1:
				{
					GtkPlusState gtkPlusState;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.ComboBoxPaintDropDownButton(dc, bounds, clipRectangle, gtkPlusState);
					return true;
				}
				case 4:
					switch (state)
					{
					case 1:
					case 2:
					case 3:
					case 4:
						VisualStylesGtkPlus.GtkPlus.ComboBoxPaintBorder(dc, bounds, clipRectangle);
						return true;
					default:
						return false;
					}
					break;
				}
				return false;
			case VisualStylesGtkPlus.ThemeHandle.EDIT:
			{
				if (part != 1)
				{
					return false;
				}
				GtkPlusState gtkPlusState;
				switch (state)
				{
				case 1:
				case 2:
				case 3:
				case 5:
				case 6:
				case 7:
					gtkPlusState = GtkPlusState.Normal;
					break;
				case 4:
					gtkPlusState = GtkPlusState.Disabled;
					break;
				default:
					return false;
				}
				VisualStylesGtkPlus.GtkPlus.TextBoxPaint(dc, bounds, excludedArea, gtkPlusState);
				return true;
			}
			case VisualStylesGtkPlus.ThemeHandle.HEADER:
			{
				if (part != 1)
				{
					return false;
				}
				GtkPlusState gtkPlusState;
				switch (state)
				{
				case 1:
					gtkPlusState = GtkPlusState.Normal;
					break;
				case 2:
					gtkPlusState = GtkPlusState.Hot;
					break;
				case 3:
					gtkPlusState = GtkPlusState.Pressed;
					break;
				default:
					return false;
				}
				VisualStylesGtkPlus.GtkPlus.HeaderPaint(dc, bounds, clipRectangle, gtkPlusState);
				return true;
			}
			case VisualStylesGtkPlus.ThemeHandle.PROGRESS:
				switch (part)
				{
				case 1:
				case 2:
					VisualStylesGtkPlus.GtkPlus.ProgressBarPaintBar(dc, bounds, clipRectangle);
					return true;
				case 3:
				case 4:
					VisualStylesGtkPlus.GtkPlus.ProgressBarPaintChunk(dc, bounds, clipRectangle);
					return true;
				default:
					return false;
				}
				break;
			case VisualStylesGtkPlus.ThemeHandle.REBAR:
				if (part != 3)
				{
					return false;
				}
				VisualStylesGtkPlus.GtkPlus.ToolBarPaint(dc, bounds, clipRectangle);
				return true;
			case VisualStylesGtkPlus.ThemeHandle.SCROLLBAR:
				switch (part)
				{
				case 1:
				{
					GtkPlusState gtkPlusState;
					bool flag;
					bool flag2;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						flag = false;
						flag2 = true;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						flag = false;
						flag2 = true;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						flag = false;
						flag2 = true;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						flag = false;
						flag2 = true;
						break;
					case 5:
						gtkPlusState = GtkPlusState.Normal;
						flag = false;
						flag2 = false;
						break;
					case 6:
						gtkPlusState = GtkPlusState.Hot;
						flag = false;
						flag2 = false;
						break;
					case 7:
						gtkPlusState = GtkPlusState.Pressed;
						flag = false;
						flag2 = false;
						break;
					case 8:
						gtkPlusState = GtkPlusState.Disabled;
						flag = false;
						flag2 = false;
						break;
					case 9:
						gtkPlusState = GtkPlusState.Normal;
						flag = true;
						flag2 = true;
						break;
					case 10:
						gtkPlusState = GtkPlusState.Hot;
						flag = true;
						flag2 = true;
						break;
					case 11:
						gtkPlusState = GtkPlusState.Pressed;
						flag = true;
						flag2 = true;
						break;
					case 12:
						gtkPlusState = GtkPlusState.Disabled;
						flag = true;
						flag2 = true;
						break;
					case 13:
						gtkPlusState = GtkPlusState.Normal;
						flag = true;
						flag2 = false;
						break;
					case 14:
						gtkPlusState = GtkPlusState.Hot;
						flag = true;
						flag2 = false;
						break;
					case 15:
						gtkPlusState = GtkPlusState.Pressed;
						flag = true;
						flag2 = false;
						break;
					case 16:
						gtkPlusState = GtkPlusState.Disabled;
						flag = true;
						flag2 = false;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.ScrollBarPaintArrowButton(dc, bounds, clipRectangle, gtkPlusState, flag, flag2);
					return true;
				}
				case 2:
				case 3:
				{
					GtkPlusState gtkPlusState;
					if (!VisualStylesGtkPlus.GetGtkPlusState((SCROLLBARSTYLESTATES)state, out gtkPlusState))
					{
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.ScrollBarPaintThumbButton(dc, bounds, clipRectangle, gtkPlusState, part == 2);
					return true;
				}
				case 4:
				case 5:
				case 6:
				case 7:
				{
					GtkPlusState gtkPlusState;
					if (!VisualStylesGtkPlus.GetGtkPlusState((SCROLLBARSTYLESTATES)state, out gtkPlusState))
					{
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.ScrollBarPaintTrack(dc, bounds, clipRectangle, gtkPlusState, part == 4 || part == 5, part == 5 || part == 7);
					return true;
				}
				default:
					return false;
				}
				break;
			case VisualStylesGtkPlus.ThemeHandle.SPIN:
			{
				GtkPlusState gtkPlusState;
				bool flag3;
				if (part != 1)
				{
					if (part != 2)
					{
						return false;
					}
					flag3 = false;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						break;
					default:
						return false;
					}
				}
				else
				{
					flag3 = true;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Disabled;
						break;
					default:
						return false;
					}
				}
				VisualStylesGtkPlus.GtkPlus.UpDownPaint(dc, bounds, clipRectangle, flag3, gtkPlusState);
				return true;
			}
			case VisualStylesGtkPlus.ThemeHandle.STATUS:
				if (part != 3)
				{
					return false;
				}
				VisualStylesGtkPlus.GtkPlus.StatusBarPaintGripper(dc, bounds, clipRectangle);
				return true;
			case VisualStylesGtkPlus.ThemeHandle.TAB:
			{
				bool flag4;
				switch (part)
				{
				case 1:
					switch (state)
					{
					case 1:
					case 2:
					case 4:
						flag4 = false;
						break;
					case 3:
						flag4 = true;
						break;
					default:
						return false;
					}
					break;
				case 2:
					switch (state)
					{
					case 1:
					case 2:
					case 4:
						flag4 = false;
						break;
					case 3:
						flag4 = true;
						break;
					default:
						return false;
					}
					break;
				case 3:
					switch (state)
					{
					case 1:
					case 2:
					case 4:
						flag4 = false;
						break;
					case 3:
						flag4 = true;
						break;
					default:
						return false;
					}
					break;
				case 4:
					flag4 = false;
					break;
				case 5:
					switch (state)
					{
					case 1:
					case 2:
					case 4:
						flag4 = false;
						break;
					case 3:
						flag4 = true;
						break;
					default:
						return false;
					}
					break;
				case 6:
					switch (state)
					{
					case 1:
					case 2:
					case 4:
						flag4 = false;
						break;
					case 3:
						flag4 = true;
						break;
					default:
						return false;
					}
					break;
				case 7:
					switch (state)
					{
					case 1:
					case 2:
					case 4:
						flag4 = false;
						break;
					case 3:
						flag4 = true;
						break;
					default:
						return false;
					}
					break;
				case 8:
					flag4 = false;
					break;
				case 9:
					VisualStylesGtkPlus.GtkPlus.TabControlPaintPane(dc, bounds, clipRectangle);
					return true;
				default:
					return false;
				}
				VisualStylesGtkPlus.GtkPlus.TabControlPaintTabItem(dc, bounds, clipRectangle, (!flag4) ? GtkPlusState.Normal : GtkPlusState.Pressed);
				return true;
			}
			case VisualStylesGtkPlus.ThemeHandle.TOOLBAR:
			{
				if (part != 1)
				{
					return false;
				}
				GtkPlusState gtkPlusState;
				switch (state)
				{
				case 1:
					gtkPlusState = GtkPlusState.Normal;
					break;
				case 2:
					gtkPlusState = GtkPlusState.Hot;
					break;
				case 3:
					gtkPlusState = GtkPlusState.Pressed;
					break;
				case 4:
					gtkPlusState = GtkPlusState.Disabled;
					break;
				case 5:
				case 6:
					VisualStylesGtkPlus.GtkPlus.ToolBarPaintCheckedButton(dc, bounds, clipRectangle);
					return true;
				default:
					return false;
				}
				VisualStylesGtkPlus.GtkPlus.ToolBarPaintButton(dc, bounds, clipRectangle, gtkPlusState);
				return true;
			}
			case VisualStylesGtkPlus.ThemeHandle.TRACKBAR:
				switch (part)
				{
				case 1:
					if (state != 1)
					{
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.TrackBarPaintTrack(dc, bounds, clipRectangle, true);
					return true;
				case 2:
					if (state != 1)
					{
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.TrackBarPaintTrack(dc, bounds, clipRectangle, false);
					return true;
				case 3:
				{
					GtkPlusState gtkPlusState;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Selected;
						break;
					case 5:
						gtkPlusState = GtkPlusState.Disabled;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.TrackBarPaintThumb(dc, bounds, clipRectangle, gtkPlusState, true);
					return true;
				}
				case 6:
				{
					GtkPlusState gtkPlusState;
					switch (state)
					{
					case 1:
						gtkPlusState = GtkPlusState.Normal;
						break;
					case 2:
						gtkPlusState = GtkPlusState.Hot;
						break;
					case 3:
						gtkPlusState = GtkPlusState.Pressed;
						break;
					case 4:
						gtkPlusState = GtkPlusState.Selected;
						break;
					case 5:
						gtkPlusState = GtkPlusState.Disabled;
						break;
					default:
						return false;
					}
					VisualStylesGtkPlus.GtkPlus.TrackBarPaintThumb(dc, bounds, clipRectangle, gtkPlusState, false);
					return true;
				}
				}
				return false;
			case VisualStylesGtkPlus.ThemeHandle.TREEVIEW:
			{
				if (part != 2)
				{
					return false;
				}
				bool flag5;
				if (state != 1)
				{
					if (state != 2)
					{
						return false;
					}
					flag5 = false;
				}
				else
				{
					flag5 = true;
				}
				VisualStylesGtkPlus.GtkPlus.TreeViewPaintGlyph(dc, bounds, clipRectangle, flag5);
				return true;
			}
			default:
				return false;
			}
		}

		// Token: 0x0600500B RID: 20491 RVA: 0x00138F84 File Offset: 0x00137184
		private static bool GetGtkPlusState(SCROLLBARSTYLESTATES state, out GtkPlusState result)
		{
			switch (state)
			{
			case SCROLLBARSTYLESTATES.SCRBS_NORMAL:
				result = GtkPlusState.Normal;
				break;
			case SCROLLBARSTYLESTATES.SCRBS_HOT:
				result = GtkPlusState.Hot;
				break;
			case SCROLLBARSTYLESTATES.SCRBS_PRESSED:
				result = GtkPlusState.Pressed;
				break;
			case SCROLLBARSTYLESTATES.SCRBS_DISABLED:
				result = GtkPlusState.Disabled;
				break;
			default:
				result = GtkPlusState.Normal;
				return false;
			}
			return true;
		}

		// Token: 0x0600500C RID: 20492 RVA: 0x00138FD8 File Offset: 0x001371D8
		public int UxThemeDrawThemeEdge(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, Edges edges, EdgeStyle style, EdgeEffects effects, out Rectangle result)
		{
			result = Rectangle.Empty;
			return 1;
		}

		// Token: 0x0600500D RID: 20493 RVA: 0x00138FE8 File Offset: 0x001371E8
		public int UxThemeDrawThemeText(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string text, TextFormatFlags textFlags, Rectangle bounds)
		{
			return 1;
		}

		// Token: 0x0600500E RID: 20494 RVA: 0x00138FEC File Offset: 0x001371EC
		public int UxThemeGetThemeBackgroundContentRect(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, out Rectangle result)
		{
			return (!this.GetBackgroundContentRectangle((VisualStylesGtkPlus.ThemeHandle)(int)hTheme, iPartId, iStateId, bounds, out result)) ? 1 : 0;
		}

		// Token: 0x0600500F RID: 20495 RVA: 0x00139010 File Offset: 0x00137210
		private bool GetBackgroundContentRectangle(VisualStylesGtkPlus.ThemeHandle handle, int part, int state, Rectangle bounds, out Rectangle result)
		{
			if (handle == VisualStylesGtkPlus.ThemeHandle.PROGRESS)
			{
				if (part == 1 || part == 2)
				{
					result = VisualStylesGtkPlus.GtkPlus.ProgressBarGetBackgroundContentRectagle(bounds);
					return true;
				}
			}
			result = Rectangle.Empty;
			return false;
		}

		// Token: 0x06005010 RID: 20496 RVA: 0x00139068 File Offset: 0x00137268
		public int UxThemeGetThemeBackgroundExtent(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle contentBounds, out Rectangle result)
		{
			result = Rectangle.Empty;
			return 1;
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x00139078 File Offset: 0x00137278
		public int UxThemeGetThemeBackgroundRegion(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, out Region result)
		{
			result = null;
			return 1;
		}

		// Token: 0x06005012 RID: 20498 RVA: 0x00139080 File Offset: 0x00137280
		public int UxThemeGetThemeBool(IntPtr hTheme, int iPartId, int iStateId, BooleanProperty prop, out bool result)
		{
			result = false;
			return 1;
		}

		// Token: 0x06005013 RID: 20499 RVA: 0x00139088 File Offset: 0x00137288
		public int UxThemeGetThemeColor(IntPtr hTheme, int iPartId, int iStateId, ColorProperty prop, out Color result)
		{
			result = Color.Black;
			return 1;
		}

		// Token: 0x06005014 RID: 20500 RVA: 0x00139098 File Offset: 0x00137298
		public int UxThemeGetThemeEnumValue(IntPtr hTheme, int iPartId, int iStateId, EnumProperty prop, out int result)
		{
			result = 0;
			return 1;
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x001390A0 File Offset: 0x001372A0
		public int UxThemeGetThemeFilename(IntPtr hTheme, int iPartId, int iStateId, FilenameProperty prop, out string result)
		{
			result = null;
			return 1;
		}

		// Token: 0x06005016 RID: 20502 RVA: 0x001390A8 File Offset: 0x001372A8
		public int UxThemeGetThemeInt(IntPtr hTheme, int iPartId, int iStateId, IntegerProperty prop, out int result)
		{
			return (!this.GetInteger((VisualStylesGtkPlus.ThemeHandle)(int)hTheme, iPartId, iStateId, prop, out result)) ? 1 : 0;
		}

		// Token: 0x06005017 RID: 20503 RVA: 0x001390C8 File Offset: 0x001372C8
		private bool GetInteger(VisualStylesGtkPlus.ThemeHandle handle, int part, int state, IntegerProperty property, out int result)
		{
			if (handle == VisualStylesGtkPlus.ThemeHandle.PROGRESS)
			{
				if (part == 3 || part == 4)
				{
					if (property == IntegerProperty.ProgressChunkSize)
					{
						result = ThemeWin32Classic.ProgressBarGetChunkSize();
						return true;
					}
					if (property == IntegerProperty.ProgressSpaceSize)
					{
						result = 2;
						return true;
					}
				}
			}
			result = 0;
			return false;
		}

		// Token: 0x06005018 RID: 20504 RVA: 0x00139138 File Offset: 0x00137338
		public int UxThemeGetThemeMargins(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, MarginProperty prop, out Padding result)
		{
			result = Padding.Empty;
			return 1;
		}

		// Token: 0x06005019 RID: 20505 RVA: 0x00139148 File Offset: 0x00137348
		public int UxThemeGetThemePartSize(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, Rectangle bounds, ThemeSizeType type, out Size result)
		{
			return (!this.GetPartSize((VisualStylesGtkPlus.ThemeHandle)(int)hTheme, dc, iPartId, iStateId, bounds, true, type, out result)) ? 1 : 0;
		}

		// Token: 0x0600501A RID: 20506 RVA: 0x00139178 File Offset: 0x00137378
		public int UxThemeGetThemePartSize(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, ThemeSizeType type, out Size result)
		{
			return (!this.GetPartSize((VisualStylesGtkPlus.ThemeHandle)(int)hTheme, dc, iPartId, iStateId, Rectangle.Empty, false, type, out result)) ? 1 : 0;
		}

		// Token: 0x0600501B RID: 20507 RVA: 0x001391AC File Offset: 0x001373AC
		private bool GetPartSize(VisualStylesGtkPlus.ThemeHandle themeHandle, IDeviceContext dc, int part, int state, Rectangle bounds, bool rectangleSpecified, ThemeSizeType type, out Size result)
		{
			switch (themeHandle)
			{
			case VisualStylesGtkPlus.ThemeHandle.BUTTON:
				if (part == 2)
				{
					result = VisualStylesGtkPlus.GtkPlus.RadioButtonGetSize();
					return true;
				}
				if (part == 3)
				{
					result = VisualStylesGtkPlus.GtkPlus.CheckBoxGetSize();
					return true;
				}
				break;
			default:
				if (themeHandle == VisualStylesGtkPlus.ThemeHandle.TRACKBAR)
				{
					switch (part)
					{
					case 1:
						result..ctor(0, 4);
						return true;
					case 2:
						result..ctor(4, 0);
						return true;
					case 3:
					case 6:
						result = ThemeWin32Classic.TrackBarGetThumbSize();
						if (part == 6)
						{
							int width = result.Width;
							result.Width = result.Height;
							result.Height = width;
						}
						return true;
					}
				}
				break;
			case VisualStylesGtkPlus.ThemeHandle.HEADER:
				if (part == 1)
				{
					result..ctor(0, ThemeWin32Classic.ListViewGetHeaderHeight());
					return true;
				}
				break;
			}
			result = Size.Empty;
			return false;
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x001392C8 File Offset: 0x001374C8
		public int UxThemeGetThemePosition(IntPtr hTheme, int iPartId, int iStateId, PointProperty prop, out Point result)
		{
			result = Point.Empty;
			return 1;
		}

		// Token: 0x0600501D RID: 20509 RVA: 0x001392D8 File Offset: 0x001374D8
		public int UxThemeGetThemeString(IntPtr hTheme, int iPartId, int iStateId, StringProperty prop, out string result)
		{
			result = null;
			return 1;
		}

		// Token: 0x0600501E RID: 20510 RVA: 0x001392E0 File Offset: 0x001374E0
		public int UxThemeGetThemeTextExtent(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string textToDraw, TextFormatFlags flags, Rectangle bounds, out Rectangle result)
		{
			result = Rectangle.Empty;
			return 1;
		}

		// Token: 0x0600501F RID: 20511 RVA: 0x001392F0 File Offset: 0x001374F0
		public int UxThemeGetThemeTextExtent(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, string textToDraw, TextFormatFlags flags, out Rectangle result)
		{
			result = Rectangle.Empty;
			return 1;
		}

		// Token: 0x06005020 RID: 20512 RVA: 0x00139300 File Offset: 0x00137500
		public int UxThemeGetThemeTextMetrics(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, out TextMetrics result)
		{
			result = default(TextMetrics);
			return 1;
		}

		// Token: 0x06005021 RID: 20513 RVA: 0x0013930C File Offset: 0x0013750C
		public int UxThemeHitTestThemeBackground(IntPtr hTheme, IDeviceContext dc, int iPartId, int iStateId, HitTestOptions options, Rectangle backgroundRectangle, IntPtr hrgn, Point pt, out HitTestCode result)
		{
			result = HitTestCode.Bottom;
			return 1;
		}

		// Token: 0x06005022 RID: 20514 RVA: 0x00139314 File Offset: 0x00137514
		public bool UxThemeIsAppThemed()
		{
			return true;
		}

		// Token: 0x06005023 RID: 20515 RVA: 0x00139318 File Offset: 0x00137518
		public bool UxThemeIsThemeActive()
		{
			return true;
		}

		// Token: 0x06005024 RID: 20516 RVA: 0x0013931C File Offset: 0x0013751C
		public bool UxThemeIsThemeBackgroundPartiallyTransparent(IntPtr hTheme, int iPartId, int iStateId)
		{
			return true;
		}

		// Token: 0x06005025 RID: 20517 RVA: 0x00139320 File Offset: 0x00137520
		public bool UxThemeIsThemePartDefined(IntPtr hTheme, int iPartId)
		{
			switch ((int)hTheme)
			{
			case 1:
				switch (iPartId)
				{
				case 1:
				case 2:
				case 3:
				case 4:
					return true;
				default:
					return false;
				}
				break;
			case 2:
				switch (iPartId)
				{
				case 1:
				case 4:
					return true;
				}
				return false;
			case 3:
				return iPartId == 1;
			case 4:
				return iPartId == 1;
			case 5:
				switch (iPartId)
				{
				case 1:
				case 2:
				case 3:
				case 4:
					return true;
				default:
					return false;
				}
				break;
			case 6:
				return iPartId == 3;
			case 7:
				switch (iPartId)
				{
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
					return true;
				default:
					return false;
				}
				break;
			case 8:
				return iPartId == 1 || iPartId == 2;
			case 9:
				return iPartId == 3;
			case 10:
				switch (iPartId)
				{
				case 1:
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				case 8:
				case 9:
					return true;
				default:
					return false;
				}
				break;
			case 11:
				return iPartId == 1;
			case 12:
				switch (iPartId)
				{
				case 1:
				case 2:
				case 3:
				case 6:
					return true;
				}
				return false;
			case 13:
				return iPartId == 2;
			default:
				return false;
			}
		}

		// Token: 0x06005026 RID: 20518 RVA: 0x0013950C File Offset: 0x0013770C
		public IntPtr UxThemeOpenThemeData(IntPtr hWnd, string classList)
		{
			VisualStylesGtkPlus.ThemeHandle themeHandle;
			try
			{
				themeHandle = (VisualStylesGtkPlus.ThemeHandle)((int)Enum.Parse(typeof(VisualStylesGtkPlus.ThemeHandle), classList));
			}
			catch (ArgumentException)
			{
				return IntPtr.Zero;
			}
			return (IntPtr)((int)themeHandle);
		}

		// Token: 0x17001555 RID: 5461
		// (get) Token: 0x06005027 RID: 20519 RVA: 0x0013956C File Offset: 0x0013776C
		public string VisualStyleInformationAuthor
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001556 RID: 5462
		// (get) Token: 0x06005028 RID: 20520 RVA: 0x00139570 File Offset: 0x00137770
		public string VisualStyleInformationColorScheme
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001557 RID: 5463
		// (get) Token: 0x06005029 RID: 20521 RVA: 0x00139574 File Offset: 0x00137774
		public string VisualStyleInformationCompany
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001558 RID: 5464
		// (get) Token: 0x0600502A RID: 20522 RVA: 0x00139578 File Offset: 0x00137778
		public Color VisualStyleInformationControlHighlightHot
		{
			get
			{
				return Color.Black;
			}
		}

		// Token: 0x17001559 RID: 5465
		// (get) Token: 0x0600502B RID: 20523 RVA: 0x00139580 File Offset: 0x00137780
		public string VisualStyleInformationCopyright
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x0600502C RID: 20524 RVA: 0x00139584 File Offset: 0x00137784
		public string VisualStyleInformationDescription
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x0600502D RID: 20525 RVA: 0x00139588 File Offset: 0x00137788
		public string VisualStyleInformationDisplayName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x0600502E RID: 20526 RVA: 0x0013958C File Offset: 0x0013778C
		public string VisualStyleInformationFileName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700155D RID: 5469
		// (get) Token: 0x0600502F RID: 20527 RVA: 0x00139590 File Offset: 0x00137790
		public bool VisualStyleInformationIsSupportedByOS
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700155E RID: 5470
		// (get) Token: 0x06005030 RID: 20528 RVA: 0x00139594 File Offset: 0x00137794
		public int VisualStyleInformationMinimumColorDepth
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700155F RID: 5471
		// (get) Token: 0x06005031 RID: 20529 RVA: 0x00139598 File Offset: 0x00137798
		public string VisualStyleInformationSize
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001560 RID: 5472
		// (get) Token: 0x06005032 RID: 20530 RVA: 0x0013959C File Offset: 0x0013779C
		public bool VisualStyleInformationSupportsFlatMenus
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001561 RID: 5473
		// (get) Token: 0x06005033 RID: 20531 RVA: 0x001395A0 File Offset: 0x001377A0
		public Color VisualStyleInformationTextControlBorder
		{
			get
			{
				return Color.Black;
			}
		}

		// Token: 0x17001562 RID: 5474
		// (get) Token: 0x06005034 RID: 20532 RVA: 0x001395A8 File Offset: 0x001377A8
		public string VisualStyleInformationUrl
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001563 RID: 5475
		// (get) Token: 0x06005035 RID: 20533 RVA: 0x001395AC File Offset: 0x001377AC
		public string VisualStyleInformationVersion
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06005036 RID: 20534 RVA: 0x001395B0 File Offset: 0x001377B0
		public void VisualStyleRendererDrawBackgroundExcludingArea(IntPtr theme, IDeviceContext dc, int part, int state, Rectangle bounds, Rectangle excludedArea)
		{
			this.DrawBackground((VisualStylesGtkPlus.ThemeHandle)(int)theme, dc, part, state, bounds, bounds, excludedArea);
		}

		// Token: 0x0200062B RID: 1579
		private enum S
		{
			// Token: 0x04002D49 RID: 11593
			S_OK,
			// Token: 0x04002D4A RID: 11594
			S_FALSE
		}

		// Token: 0x0200062C RID: 1580
		private enum ThemeHandle
		{
			// Token: 0x04002D4C RID: 11596
			BUTTON = 1,
			// Token: 0x04002D4D RID: 11597
			COMBOBOX,
			// Token: 0x04002D4E RID: 11598
			EDIT,
			// Token: 0x04002D4F RID: 11599
			HEADER,
			// Token: 0x04002D50 RID: 11600
			PROGRESS,
			// Token: 0x04002D51 RID: 11601
			REBAR,
			// Token: 0x04002D52 RID: 11602
			SCROLLBAR,
			// Token: 0x04002D53 RID: 11603
			SPIN,
			// Token: 0x04002D54 RID: 11604
			STATUS,
			// Token: 0x04002D55 RID: 11605
			TAB,
			// Token: 0x04002D56 RID: 11606
			TOOLBAR,
			// Token: 0x04002D57 RID: 11607
			TRACKBAR,
			// Token: 0x04002D58 RID: 11608
			TREEVIEW
		}
	}
}
