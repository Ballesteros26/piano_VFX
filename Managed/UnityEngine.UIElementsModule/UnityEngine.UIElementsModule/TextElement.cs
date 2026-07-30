using System;
using System.Collections.Generic;
using UnityEngine.TextCore;

namespace UnityEngine.UIElements
{
	// Token: 0x02000066 RID: 102
	public class TextElement : BindableElement, ITextElement, INotifyValueChanged<string>
	{
		// Token: 0x06000250 RID: 592 RVA: 0x0000882C File Offset: 0x00006A2C
		public TextElement()
		{
			base.requireMeasureFunction = true;
			base.AddToClassList(TextElement.ussClassName);
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			base.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			base.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000251 RID: 593 RVA: 0x000088C4 File Offset: 0x00006AC4
		internal TextHandle textHandle
		{
			get
			{
				return this.m_TextHandle;
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000088DC File Offset: 0x00006ADC
		private void OnAttachToPanel(AttachToPanelEvent e)
		{
			this.m_TextHandle.useLegacy = e.destinationPanel.contextType == ContextType.Editor;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000088F8 File Offset: 0x00006AF8
		private void OnGeometryChanged(GeometryChangedEvent e)
		{
			this.UpdateVisibleText();
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00008904 File Offset: 0x00006B04
		// (set) Token: 0x06000255 RID: 597 RVA: 0x0000891C File Offset: 0x00006B1C
		public virtual string text
		{
			get
			{
				return ((INotifyValueChanged<string>)this).value;
			}
			set
			{
				((INotifyValueChanged<string>)this).value = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00008928 File Offset: 0x00006B28
		// (set) Token: 0x06000257 RID: 599 RVA: 0x00008940 File Offset: 0x00006B40
		public bool displayTooltipWhenElided
		{
			get
			{
				return this.m_DisplayTooltipWhenElided;
			}
			set
			{
				bool flag = this.m_DisplayTooltipWhenElided != value;
				if (flag)
				{
					this.m_DisplayTooltipWhenElided = value;
					this.UpdateVisibleText();
					base.MarkDirtyRepaint();
				}
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00008975 File Offset: 0x00006B75
		// (set) Token: 0x06000259 RID: 601 RVA: 0x0000897D File Offset: 0x00006B7D
		public bool isElided { get; private set; }

		// Token: 0x0600025A RID: 602 RVA: 0x00008986 File Offset: 0x00006B86
		private void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			this.UpdateVisibleText();
			mgc.Text(this.m_TextParams, this.m_TextHandle, base.scaledPixelsPerPoint);
			this.m_UpdateTextParams = true;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000089B0 File Offset: 0x00006BB0
		internal string ElideText(string drawText, string ellipsisText, float width, TextOverflowPosition textOverflowPosition)
		{
			Vector2 vector = this.MeasureTextSize(drawText, 0f, VisualElement.MeasureMode.Undefined, 0f, VisualElement.MeasureMode.Undefined);
			bool flag = vector.x <= width || string.IsNullOrEmpty(ellipsisText);
			string text;
			if (flag)
			{
				text = drawText;
			}
			else
			{
				string text2 = ((drawText.Length > 1) ? ellipsisText : drawText);
				Vector2 vector2 = this.MeasureTextSize(text2, 0f, VisualElement.MeasureMode.Undefined, 0f, VisualElement.MeasureMode.Undefined);
				bool flag2 = vector2.x >= width;
				if (flag2)
				{
					text = text2;
				}
				else
				{
					int num = drawText.Length - 1;
					int num2 = -1;
					string text3 = drawText;
					int i = ((textOverflowPosition == TextOverflowPosition.Start) ? 1 : 0);
					int num3 = ((textOverflowPosition == TextOverflowPosition.Start || textOverflowPosition == TextOverflowPosition.Middle) ? num : (num - 1));
					int num4 = (i + num3) / 2;
					while (i <= num3)
					{
						bool flag3 = textOverflowPosition == TextOverflowPosition.Start;
						if (flag3)
						{
							text3 = ellipsisText + drawText.Substring(num4, num - (num4 - 1));
						}
						else
						{
							bool flag4 = textOverflowPosition == TextOverflowPosition.End;
							if (flag4)
							{
								text3 = drawText.Substring(0, num4) + ellipsisText;
							}
							else
							{
								bool flag5 = textOverflowPosition == TextOverflowPosition.Middle;
								if (flag5)
								{
									text3 = drawText.Substring(0, num4 - 1) + ellipsisText + drawText.Substring(num - (num4 - 1));
								}
							}
						}
						vector = this.MeasureTextSize(text3, 0f, VisualElement.MeasureMode.Undefined, 0f, VisualElement.MeasureMode.Undefined);
						bool flag6 = Math.Abs(vector.x - width) < Mathf.Epsilon;
						if (flag6)
						{
							return text3;
						}
						bool flag7 = textOverflowPosition == TextOverflowPosition.Start;
						if (flag7)
						{
							bool flag8 = vector.x > width;
							if (flag8)
							{
								bool flag9 = num2 == num4 - 1;
								if (flag9)
								{
									return ellipsisText + drawText.Substring(num2, num - (num2 - 1));
								}
								i = num4 + 1;
							}
							else
							{
								num3 = num4 - 1;
								num2 = num4;
							}
						}
						else
						{
							bool flag10 = textOverflowPosition == TextOverflowPosition.End || textOverflowPosition == TextOverflowPosition.Middle;
							if (flag10)
							{
								bool flag11 = vector.x > width;
								if (flag11)
								{
									bool flag12 = num2 == num4 - 1;
									if (flag12)
									{
										bool flag13 = textOverflowPosition == TextOverflowPosition.End;
										if (flag13)
										{
											return drawText.Substring(0, num2) + ellipsisText;
										}
										return drawText.Substring(0, num2 - 1) + ellipsisText + drawText.Substring(num - (num2 - 1));
									}
									else
									{
										num3 = num4 - 1;
									}
								}
								else
								{
									i = num4 + 1;
									num2 = num4;
								}
							}
						}
						num4 = (i + num3) / 2;
					}
					text = text3;
				}
			}
			return text;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00008C18 File Offset: 0x00006E18
		private void UpdateTooltip()
		{
			bool flag = this.displayTooltipWhenElided && this.isElided;
			bool flag2 = flag;
			if (flag2)
			{
				bool flag3 = !this.m_WasElided;
				if (flag3)
				{
					bool flag4 = string.IsNullOrEmpty(base.tooltip);
					if (flag4)
					{
						base.tooltip = this.text;
					}
					this.m_WasElided = true;
				}
			}
			else
			{
				bool wasElided = this.m_WasElided;
				if (wasElided)
				{
					bool flag5 = base.tooltip == this.text;
					if (flag5)
					{
						base.tooltip = null;
					}
					this.m_WasElided = false;
				}
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00008CA8 File Offset: 0x00006EA8
		private void UpdateVisibleText()
		{
			MeshGenerationContextUtils.TextParams textParams = MeshGenerationContextUtils.TextParams.MakeStyleBased(this, this.text);
			int hashCode = textParams.GetHashCode();
			bool flag = this.m_UpdateTextParams || hashCode != this.m_PreviousTextParamsHashCode;
			if (flag)
			{
				this.m_TextParams = textParams;
				bool flag2 = this.m_TextParams.textOverflowMode == TextOverflowMode.Ellipsis;
				if (flag2)
				{
					this.m_TextParams.text = this.ElideText(this.m_TextParams.text, TextElement.k_EllipsisText, this.m_TextParams.rect.width, this.m_TextParams.textOverflowPosition);
				}
				this.isElided = this.m_TextParams.textOverflowMode == TextOverflowMode.Ellipsis && this.m_TextParams.text != this.text;
				this.m_PreviousTextParamsHashCode = hashCode;
				this.m_UpdateTextParams = false;
				this.UpdateTooltip();
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008D8C File Offset: 0x00006F8C
		public Vector2 MeasureTextSize(string textToMeasure, float width, VisualElement.MeasureMode widthMode, float height, VisualElement.MeasureMode heightMode)
		{
			return TextElement.MeasureVisualElementTextSize(this, textToMeasure, width, widthMode, height, heightMode, this.m_TextHandle);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00008DB4 File Offset: 0x00006FB4
		internal static Vector2 MeasureVisualElementTextSize(VisualElement ve, string textToMeasure, float width, VisualElement.MeasureMode widthMode, float height, VisualElement.MeasureMode heightMode, TextHandle textHandle)
		{
			float num = float.NaN;
			float num2 = float.NaN;
			Font value = ve.computedStyle.unityFont.value;
			bool flag = textToMeasure == null || value == null;
			Vector2 vector;
			if (flag)
			{
				vector = new Vector2(num, num2);
			}
			else
			{
				Vector3 vector2 = ve.ComputeGlobalScale();
				bool flag2 = vector2.x + vector2.y <= 0f || ve.scaledPixelsPerPoint <= 0f;
				if (flag2)
				{
					vector = Vector2.zero;
				}
				else
				{
					bool flag3 = widthMode == VisualElement.MeasureMode.Exactly;
					if (flag3)
					{
						num = width;
					}
					else
					{
						MeshGenerationContextUtils.TextParams textSettings = TextElement.GetTextSettings(ve, textToMeasure);
						textSettings.wordWrap = false;
						textSettings.richText = false;
						num = Mathf.Ceil(textHandle.ComputeTextWidth(textSettings, ve.scaledPixelsPerPoint));
						bool flag4 = widthMode == VisualElement.MeasureMode.AtMost;
						if (flag4)
						{
							num = Mathf.Min(num, width);
						}
					}
					bool flag5 = heightMode == VisualElement.MeasureMode.Exactly;
					if (flag5)
					{
						num2 = height;
					}
					else
					{
						MeshGenerationContextUtils.TextParams textSettings2 = TextElement.GetTextSettings(ve, textToMeasure);
						textSettings2.wordWrapWidth = num;
						textSettings2.richText = false;
						num2 = Mathf.Ceil(textHandle.ComputeTextHeight(textSettings2, ve.scaledPixelsPerPoint));
						bool flag6 = heightMode == VisualElement.MeasureMode.AtMost;
						if (flag6)
						{
							num2 = Mathf.Min(num2, height);
						}
					}
					vector = new Vector2(num, num2);
				}
			}
			return vector;
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00008F00 File Offset: 0x00007100
		protected internal override Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			return this.MeasureTextSize(this.text, desiredWidth, widthMode, desiredHeight, heightMode);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00008F24 File Offset: 0x00007124
		private static MeshGenerationContextUtils.TextParams GetTextSettings(VisualElement ve, string text)
		{
			ComputedStyle computedStyle = ve.computedStyle;
			return new MeshGenerationContextUtils.TextParams
			{
				rect = ve.contentRect,
				text = text,
				font = computedStyle.unityFont.value,
				fontSize = (int)computedStyle.fontSize.value.value,
				fontStyle = computedStyle.unityFontStyleAndWeight.value,
				fontColor = computedStyle.color.value,
				anchor = computedStyle.unityTextAlign.value,
				wordWrap = (computedStyle.whiteSpace.value == WhiteSpace.Normal),
				wordWrapWidth = ((computedStyle.whiteSpace.value == WhiteSpace.Normal) ? ve.contentRect.width : 0f),
				richText = true,
				textOverflowMode = MeshGenerationContextUtils.TextParams.GetTextOverflowMode(computedStyle),
				textOverflowPosition = computedStyle.unityTextOverflowPosition.value
			};
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00009048 File Offset: 0x00007248
		// (set) Token: 0x06000263 RID: 611 RVA: 0x0000906C File Offset: 0x0000726C
		string INotifyValueChanged<string>.value
		{
			get
			{
				return this.m_Text ?? string.Empty;
			}
			set
			{
				bool flag = this.m_Text != value;
				if (flag)
				{
					bool flag2 = base.panel != null;
					if (flag2)
					{
						using (ChangeEvent<string> pooled = ChangeEvent<string>.GetPooled(this.text, value))
						{
							pooled.target = this;
							((INotifyValueChanged<string>)this).SetValueWithoutNotify(value);
							this.SendEvent(pooled);
						}
					}
					else
					{
						((INotifyValueChanged<string>)this).SetValueWithoutNotify(value);
					}
				}
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000090EC File Offset: 0x000072EC
		void INotifyValueChanged<string>.SetValueWithoutNotify(string newValue)
		{
			bool flag = this.m_Text != newValue;
			if (flag)
			{
				this.m_Text = newValue;
				base.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Repaint);
				bool flag2 = !string.IsNullOrEmpty(base.viewDataKey);
				if (flag2)
				{
					base.SaveViewData();
				}
			}
		}

		// Token: 0x04000130 RID: 304
		public static readonly string ussClassName = "unity-text-element";

		// Token: 0x04000131 RID: 305
		private TextHandle m_TextHandle = TextHandle.New();

		// Token: 0x04000132 RID: 306
		[SerializeField]
		private string m_Text;

		// Token: 0x04000133 RID: 307
		private bool m_DisplayTooltipWhenElided = true;

		// Token: 0x04000135 RID: 309
		internal static readonly string k_EllipsisText = "...";

		// Token: 0x04000136 RID: 310
		private bool m_WasElided;

		// Token: 0x04000137 RID: 311
		private bool m_UpdateTextParams = true;

		// Token: 0x04000138 RID: 312
		private MeshGenerationContextUtils.TextParams m_TextParams;

		// Token: 0x04000139 RID: 313
		private int m_PreviousTextParamsHashCode = int.MaxValue;

		// Token: 0x02000067 RID: 103
		public new class UxmlFactory : UxmlFactory<TextElement, TextElement.UxmlTraits>
		{
		}

		// Token: 0x02000068 RID: 104
		public new class UxmlTraits : BindableElement.UxmlTraits
		{
			// Token: 0x1700008E RID: 142
			// (get) Token: 0x06000267 RID: 615 RVA: 0x00009158 File Offset: 0x00007358
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield break;
				}
			}

			// Token: 0x06000268 RID: 616 RVA: 0x00009177 File Offset: 0x00007377
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				((ITextElement)ve).text = this.m_Text.GetValueFromBag(bag, cc);
			}

			// Token: 0x0400013A RID: 314
			private UxmlStringAttributeDescription m_Text = new UxmlStringAttributeDescription
			{
				name = "text"
			};
		}
	}
}
