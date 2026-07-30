using System;

namespace UnityEngine
{
	// Token: 0x02000039 RID: 57
	internal struct SliderHandler
	{
		// Token: 0x060003FF RID: 1023 RVA: 0x0000EF2C File Offset: 0x0000D12C
		public SliderHandler(Rect position, float currentValue, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id, GUIStyle thumbExtent = null)
		{
			this.position = position;
			this.currentValue = currentValue;
			this.size = size;
			this.start = start;
			this.end = end;
			this.slider = slider;
			this.thumb = thumb;
			this.thumbExtent = thumbExtent;
			this.horiz = horiz;
			this.id = id;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000EF88 File Offset: 0x0000D188
		public float Handle()
		{
			bool flag = this.slider == null || this.thumb == null;
			float num;
			if (flag)
			{
				num = this.currentValue;
			}
			else
			{
				EventType eventType = this.CurrentEventType();
				switch (eventType)
				{
				case EventType.MouseDown:
					return this.OnMouseDown();
				case EventType.MouseUp:
					return this.OnMouseUp();
				case EventType.MouseMove:
					break;
				case EventType.MouseDrag:
					return this.OnMouseDrag();
				default:
					if (eventType == EventType.Repaint)
					{
						return this.OnRepaint();
					}
					break;
				}
				num = this.currentValue;
			}
			return num;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000F00C File Offset: 0x0000D20C
		private float OnMouseDown()
		{
			Rect rect = this.ThumbSelectionRect();
			bool flag = GUIUtility.HitTest(rect, this.CurrentEvent());
			Rect zero = Rect.zero;
			zero.xMin = Math.Min(this.position.xMin, rect.xMin);
			zero.xMax = Math.Max(this.position.xMax, rect.xMax);
			zero.yMin = Math.Min(this.position.yMin, rect.yMin);
			zero.yMax = Math.Max(this.position.yMax, rect.yMax);
			bool flag2 = this.IsEmptySlider() || (!GUIUtility.HitTest(zero, this.CurrentEvent()) && !flag);
			float num;
			if (flag2)
			{
				num = this.currentValue;
			}
			else
			{
				GUI.scrollTroughSide = 0;
				GUIUtility.hotControl = this.id;
				this.CurrentEvent().Use();
				bool flag3 = flag;
				if (flag3)
				{
					this.StartDraggingWithValue(this.ClampedCurrentValue());
					num = this.currentValue;
				}
				else
				{
					GUI.changed = true;
					bool flag4 = this.SupportsPageMovements();
					if (flag4)
					{
						this.SliderState().isDragging = false;
						GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(250.0);
						GUI.scrollTroughSide = this.CurrentScrollTroughSide();
						num = this.PageMovementValue();
					}
					else
					{
						float num2 = this.ValueForCurrentMousePosition();
						this.StartDraggingWithValue(num2);
						num = this.Clamp(num2);
					}
				}
			}
			return num;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		private float OnMouseDrag()
		{
			bool flag = GUIUtility.hotControl != this.id;
			float num;
			if (flag)
			{
				num = this.currentValue;
			}
			else
			{
				SliderState sliderState = this.SliderState();
				bool flag2 = !sliderState.isDragging;
				if (flag2)
				{
					num = this.currentValue;
				}
				else
				{
					GUI.changed = true;
					this.CurrentEvent().Use();
					float num2 = this.MousePosition() - sliderState.dragStartPos;
					float num3 = sliderState.dragStartValue + num2 / this.ValuesPerPixel();
					num = this.Clamp(num3);
				}
			}
			return num;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000F22C File Offset: 0x0000D42C
		private float OnMouseUp()
		{
			bool flag = GUIUtility.hotControl == this.id;
			if (flag)
			{
				this.CurrentEvent().Use();
				GUIUtility.hotControl = 0;
			}
			return this.currentValue;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000F26C File Offset: 0x0000D46C
		private float OnRepaint()
		{
			bool flag = GUIUtility.HitTest(this.position, this.CurrentEvent());
			this.slider.Draw(this.position, GUIContent.none, this.id, false, flag);
			bool flag2 = !this.IsEmptySlider() && this.currentValue >= Mathf.Min(this.start, this.end) && this.currentValue <= Mathf.Max(this.start, this.end);
			if (flag2)
			{
				bool flag3 = this.thumbExtent != null;
				if (flag3)
				{
					this.thumbExtent.Draw(this.ThumbExtRect(), GUIContent.none, this.id, false, flag);
				}
				this.thumb.Draw(this.ThumbRect(), GUIContent.none, this.id, false, flag);
			}
			bool flag4 = GUIUtility.hotControl != this.id || !flag || this.IsEmptySlider();
			float num;
			if (flag4)
			{
				num = this.currentValue;
			}
			else
			{
				bool flag5 = GUIUtility.HitTest(this.ThumbRect(), this.CurrentEvent());
				if (flag5)
				{
					bool flag6 = GUI.scrollTroughSide != 0;
					if (flag6)
					{
						GUIUtility.hotControl = 0;
					}
					num = this.currentValue;
				}
				else
				{
					GUI.InternalRepaintEditorWindow();
					bool flag7 = SystemClock.now < GUI.nextScrollStepTime;
					if (flag7)
					{
						num = this.currentValue;
					}
					else
					{
						bool flag8 = this.CurrentScrollTroughSide() != GUI.scrollTroughSide;
						if (flag8)
						{
							num = this.currentValue;
						}
						else
						{
							GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(30.0);
							bool flag9 = this.SupportsPageMovements();
							if (flag9)
							{
								this.SliderState().isDragging = false;
								GUI.changed = true;
								num = this.PageMovementValue();
							}
							else
							{
								num = this.ClampedCurrentValue();
							}
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000F43C File Offset: 0x0000D63C
		private EventType CurrentEventType()
		{
			return this.CurrentEvent().GetTypeForControl(this.id);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000F460 File Offset: 0x0000D660
		private int CurrentScrollTroughSide()
		{
			float num = (this.horiz ? this.CurrentEvent().mousePosition.x : this.CurrentEvent().mousePosition.y);
			float num2 = (this.horiz ? this.ThumbRect().x : this.ThumbRect().y);
			return (num > num2) ? 1 : (-1);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000F4CC File Offset: 0x0000D6CC
		private bool IsEmptySlider()
		{
			return this.start == this.end;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000F4EC File Offset: 0x0000D6EC
		private bool SupportsPageMovements()
		{
			return this.size != 0f && GUI.usePageScrollbars;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0000F514 File Offset: 0x0000D714
		private float PageMovementValue()
		{
			float num = this.currentValue;
			int num2 = ((this.start > this.end) ? (-1) : 1);
			bool flag = this.MousePosition() > this.PageUpMovementBound();
			if (flag)
			{
				num += this.size * (float)num2 * 0.9f;
			}
			else
			{
				num -= this.size * (float)num2 * 0.9f;
			}
			return this.Clamp(num);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x0000F580 File Offset: 0x0000D780
		private float PageUpMovementBound()
		{
			bool flag = this.horiz;
			float num;
			if (flag)
			{
				num = this.ThumbRect().xMax - this.position.x;
			}
			else
			{
				num = this.ThumbRect().yMax - this.position.y;
			}
			return num;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x0000F5DC File Offset: 0x0000D7DC
		private Event CurrentEvent()
		{
			return Event.current;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000F5F4 File Offset: 0x0000D7F4
		private float ValueForCurrentMousePosition()
		{
			bool flag = this.horiz;
			float num;
			if (flag)
			{
				num = (this.MousePosition() - this.ThumbRect().width * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
			}
			else
			{
				num = (this.MousePosition() - this.ThumbRect().height * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
			}
			return num;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000F680 File Offset: 0x0000D880
		private float Clamp(float value)
		{
			return Mathf.Clamp(value, this.MinValue(), this.MaxValue());
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000F6A4 File Offset: 0x0000D8A4
		private Rect ThumbSelectionRect()
		{
			return this.ThumbRect();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000F6C0 File Offset: 0x0000D8C0
		private void StartDraggingWithValue(float dragStartValue)
		{
			SliderState sliderState = this.SliderState();
			sliderState.dragStartPos = this.MousePosition();
			sliderState.dragStartValue = dragStartValue;
			sliderState.isDragging = true;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000F6F0 File Offset: 0x0000D8F0
		private SliderState SliderState()
		{
			return (SliderState)GUIUtility.GetStateObject(typeof(SliderState), this.id);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000F71C File Offset: 0x0000D91C
		private Rect ThumbExtRect()
		{
			return new Rect(0f, 0f, this.thumbExtent.fixedWidth, this.thumbExtent.fixedHeight)
			{
				center = this.ThumbRect().center
			};
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000F770 File Offset: 0x0000D970
		private Rect ThumbRect()
		{
			return this.horiz ? this.HorizontalThumbRect() : this.VerticalThumbRect();
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000F798 File Offset: 0x0000D998
		private Rect VerticalThumbRect()
		{
			Rect rect = this.thumb.margin.Remove(this.slider.padding.Remove(this.position));
			float num = ((this.thumb.fixedWidth != 0f) ? this.thumb.fixedWidth : rect.width);
			float num2 = this.ThumbSize();
			float num3 = this.ValuesPerPixel();
			bool flag = this.start < this.end;
			Rect rect2;
			if (flag)
			{
				rect2 = new Rect(rect.x, (this.ClampedCurrentValue() - this.start) * num3 + rect.y, num, this.size * num3 + num2);
			}
			else
			{
				rect2 = new Rect(rect.x, (this.ClampedCurrentValue() + this.size - this.start) * num3 + rect.y, num, this.size * -num3 + num2);
			}
			return rect2;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000F888 File Offset: 0x0000DA88
		private Rect HorizontalThumbRect()
		{
			Rect rect = this.thumb.margin.Remove(this.slider.padding.Remove(this.position));
			float num = ((this.thumb.fixedHeight != 0f) ? this.thumb.fixedHeight : rect.height);
			float num2 = this.ThumbSize();
			float num3 = this.ValuesPerPixel();
			bool flag = this.start < this.end;
			Rect rect2;
			if (flag)
			{
				rect2 = new Rect((this.ClampedCurrentValue() - this.start) * num3 + rect.x, rect.y, this.size * num3 + num2, num);
			}
			else
			{
				rect2 = new Rect((this.ClampedCurrentValue() + this.size - this.start) * num3 + rect.x, rect.y, this.size * -num3 + num2, num);
			}
			return rect2;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000F974 File Offset: 0x0000DB74
		private float ClampedCurrentValue()
		{
			return this.Clamp(this.currentValue);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000F994 File Offset: 0x0000DB94
		private float MousePosition()
		{
			bool flag = this.horiz;
			float num;
			if (flag)
			{
				num = this.CurrentEvent().mousePosition.x - this.position.x;
			}
			else
			{
				num = this.CurrentEvent().mousePosition.y - this.position.y;
			}
			return num;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
		private float ValuesPerPixel()
		{
			bool flag = this.horiz;
			float num;
			if (flag)
			{
				num = (this.position.width - (float)this.slider.padding.horizontal - this.ThumbSize()) / (this.end - this.start);
			}
			else
			{
				num = (this.position.height - (float)this.slider.padding.vertical - this.ThumbSize()) / (this.end - this.start);
			}
			return num;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000FA80 File Offset: 0x0000DC80
		private float ThumbSize()
		{
			bool flag = this.horiz;
			float num;
			if (flag)
			{
				num = ((this.thumb.fixedWidth != 0f) ? this.thumb.fixedWidth : ((float)this.thumb.padding.horizontal));
			}
			else
			{
				num = ((this.thumb.fixedHeight != 0f) ? this.thumb.fixedHeight : ((float)this.thumb.padding.vertical));
			}
			return num;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000FB00 File Offset: 0x0000DD00
		private float MaxValue()
		{
			return Mathf.Max(this.start, this.end) - this.size;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000FB2C File Offset: 0x0000DD2C
		private float MinValue()
		{
			return Mathf.Min(this.start, this.end);
		}

		// Token: 0x0400012C RID: 300
		private readonly Rect position;

		// Token: 0x0400012D RID: 301
		private readonly float currentValue;

		// Token: 0x0400012E RID: 302
		private readonly float size;

		// Token: 0x0400012F RID: 303
		private readonly float start;

		// Token: 0x04000130 RID: 304
		private readonly float end;

		// Token: 0x04000131 RID: 305
		private readonly GUIStyle slider;

		// Token: 0x04000132 RID: 306
		private readonly GUIStyle thumb;

		// Token: 0x04000133 RID: 307
		private readonly GUIStyle thumbExtent;

		// Token: 0x04000134 RID: 308
		private readonly bool horiz;

		// Token: 0x04000135 RID: 309
		private readonly int id;
	}
}
