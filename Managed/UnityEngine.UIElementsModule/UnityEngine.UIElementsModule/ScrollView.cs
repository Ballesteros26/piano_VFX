using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000EB RID: 235
	public class ScrollView : VisualElement
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0001B008 File Offset: 0x00019208
		// (set) Token: 0x060006B3 RID: 1715 RVA: 0x0001B020 File Offset: 0x00019220
		public bool showHorizontal
		{
			get
			{
				return this.m_ShowHorizontal;
			}
			set
			{
				this.m_ShowHorizontal = value;
				this.UpdateScrollers(this.m_ShowHorizontal, this.m_ShowVertical);
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0001B040 File Offset: 0x00019240
		// (set) Token: 0x060006B5 RID: 1717 RVA: 0x0001B058 File Offset: 0x00019258
		public bool showVertical
		{
			get
			{
				return this.m_ShowVertical;
			}
			set
			{
				this.m_ShowVertical = value;
				this.UpdateScrollers(this.m_ShowHorizontal, this.m_ShowVertical);
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0001B078 File Offset: 0x00019278
		internal bool needsHorizontal
		{
			get
			{
				return this.showHorizontal || this.contentContainer.layout.width - base.layout.width > 0f;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001B0C0 File Offset: 0x000192C0
		internal bool needsVertical
		{
			get
			{
				return this.showVertical || this.contentContainer.layout.height - base.layout.height > 0f;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0001B108 File Offset: 0x00019308
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0001B138 File Offset: 0x00019338
		public Vector2 scrollOffset
		{
			get
			{
				return new Vector2(this.horizontalScroller.value, this.verticalScroller.value);
			}
			set
			{
				bool flag = value != this.scrollOffset;
				if (flag)
				{
					this.horizontalScroller.value = value.x;
					this.verticalScroller.value = value.y;
					this.UpdateContentViewTransform();
				}
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0001B184 File Offset: 0x00019384
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x0001B1A6 File Offset: 0x000193A6
		public float horizontalPageSize
		{
			get
			{
				return this.horizontalScroller.slider.pageSize;
			}
			set
			{
				this.horizontalScroller.slider.pageSize = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060006BC RID: 1724 RVA: 0x0001B1BC File Offset: 0x000193BC
		// (set) Token: 0x060006BD RID: 1725 RVA: 0x0001B1DE File Offset: 0x000193DE
		public float verticalPageSize
		{
			get
			{
				return this.verticalScroller.slider.pageSize;
			}
			set
			{
				this.verticalScroller.slider.pageSize = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060006BE RID: 1726 RVA: 0x0001B1F4 File Offset: 0x000193F4
		private float scrollableWidth
		{
			get
			{
				return this.contentContainer.layout.width - this.contentViewport.layout.width;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0001B230 File Offset: 0x00019430
		private float scrollableHeight
		{
			get
			{
				return this.contentContainer.layout.height - this.contentViewport.layout.height;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0001B269 File Offset: 0x00019469
		private bool hasInertia
		{
			get
			{
				return this.scrollDecelerationRate > 0f;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0001B278 File Offset: 0x00019478
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0001B290 File Offset: 0x00019490
		public float scrollDecelerationRate
		{
			get
			{
				return this.m_ScrollDecelerationRate;
			}
			set
			{
				this.m_ScrollDecelerationRate = Mathf.Max(0f, value);
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0001B2A4 File Offset: 0x000194A4
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0001B2BC File Offset: 0x000194BC
		public float elasticity
		{
			get
			{
				return this.m_Elasticity;
			}
			set
			{
				this.m_Elasticity = Mathf.Max(0f, value);
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0001B2D0 File Offset: 0x000194D0
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0001B2E8 File Offset: 0x000194E8
		public ScrollView.TouchScrollBehavior touchScrollBehavior
		{
			get
			{
				return this.m_TouchScrollBehavior;
			}
			set
			{
				this.m_TouchScrollBehavior = value;
				bool flag = this.m_TouchScrollBehavior == ScrollView.TouchScrollBehavior.Clamped;
				if (flag)
				{
					this.horizontalScroller.slider.clamped = true;
					this.verticalScroller.slider.clamped = true;
				}
				else
				{
					this.horizontalScroller.slider.clamped = false;
					this.verticalScroller.slider.clamped = false;
				}
			}
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001B358 File Offset: 0x00019558
		private void UpdateContentViewTransform()
		{
			Vector3 position = this.contentContainer.transform.position;
			Vector2 scrollOffset = this.scrollOffset;
			position.x = GUIUtility.RoundToPixelGrid(-scrollOffset.x);
			position.y = GUIUtility.RoundToPixelGrid(-scrollOffset.y);
			this.contentContainer.transform.position = position;
			base.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001B3C4 File Offset: 0x000195C4
		public void ScrollTo(VisualElement child)
		{
			bool flag = child == null;
			if (flag)
			{
				throw new ArgumentNullException("child");
			}
			bool flag2 = !this.contentContainer.Contains(child);
			if (flag2)
			{
				throw new ArgumentException("Cannot scroll to a VisualElement that is not a child of the ScrollView content-container.");
			}
			float num = 0f;
			float num2 = 0f;
			bool flag3 = this.scrollableHeight > 0f;
			if (flag3)
			{
				num = this.GetYDeltaOffset(child);
				this.verticalScroller.value = this.scrollOffset.y + num;
			}
			bool flag4 = this.scrollableWidth > 0f;
			if (flag4)
			{
				num2 = this.GetXDeltaOffset(child);
				this.horizontalScroller.value = this.scrollOffset.x + num2;
			}
			bool flag5 = num == 0f && num2 == 0f;
			if (!flag5)
			{
				this.UpdateContentViewTransform();
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001B4A0 File Offset: 0x000196A0
		private float GetXDeltaOffset(VisualElement child)
		{
			float num = this.contentContainer.transform.position.x * -1f;
			Rect worldBound = this.contentViewport.worldBound;
			float num2 = worldBound.xMin + num;
			float num3 = worldBound.xMax + num;
			Rect worldBound2 = child.worldBound;
			float num4 = worldBound2.xMin + num;
			float num5 = worldBound2.xMax + num;
			bool flag = (num4 >= num2 && num5 <= num3) || float.IsNaN(num4) || float.IsNaN(num5);
			float num6;
			if (flag)
			{
				num6 = 0f;
			}
			else
			{
				float deltaDistance = this.GetDeltaDistance(num2, num3, num4, num5);
				num6 = deltaDistance * this.horizontalScroller.highValue / this.scrollableWidth;
			}
			return num6;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001B560 File Offset: 0x00019760
		private float GetYDeltaOffset(VisualElement child)
		{
			float num = this.contentContainer.transform.position.y * -1f;
			Rect worldBound = this.contentViewport.worldBound;
			float num2 = worldBound.yMin + num;
			float num3 = worldBound.yMax + num;
			Rect worldBound2 = child.worldBound;
			float num4 = worldBound2.yMin + num;
			float num5 = worldBound2.yMax + num;
			bool flag = (num4 >= num2 && num5 <= num3) || float.IsNaN(num4) || float.IsNaN(num5);
			float num6;
			if (flag)
			{
				num6 = 0f;
			}
			else
			{
				float deltaDistance = this.GetDeltaDistance(num2, num3, num4, num5);
				num6 = deltaDistance * this.verticalScroller.highValue / this.scrollableHeight;
			}
			return num6;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001B620 File Offset: 0x00019820
		private float GetDeltaDistance(float viewMin, float viewMax, float childBoundaryMin, float childBoundaryMax)
		{
			float num = childBoundaryMax - viewMax;
			bool flag = num < -1f;
			if (flag)
			{
				num = childBoundaryMin - viewMin;
			}
			return num;
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0001B64A File Offset: 0x0001984A
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x0001B652 File Offset: 0x00019852
		public VisualElement contentViewport { get; private set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x0001B65B File Offset: 0x0001985B
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x0001B663 File Offset: 0x00019863
		public Scroller horizontalScroller { get; private set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001B66C File Offset: 0x0001986C
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x0001B674 File Offset: 0x00019874
		public Scroller verticalScroller { get; private set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x0001B680 File Offset: 0x00019880
		public override VisualElement contentContainer
		{
			get
			{
				return this.m_ContentContainer;
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001B698 File Offset: 0x00019898
		public ScrollView()
			: this(ScrollViewMode.Vertical)
		{
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001B6A4 File Offset: 0x000198A4
		public ScrollView(ScrollViewMode scrollViewMode)
		{
			base.AddToClassList(ScrollView.ussClassName);
			this.contentViewport = new VisualElement
			{
				name = "unity-content-viewport"
			};
			this.contentViewport.AddToClassList(ScrollView.viewportUssClassName);
			this.contentViewport.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
			this.contentViewport.RegisterCallback<AttachToPanelEvent>(new EventCallback<AttachToPanelEvent>(this.OnAttachToPanel), TrickleDown.NoTrickleDown);
			this.contentViewport.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(this.OnDetachFromPanel), TrickleDown.NoTrickleDown);
			base.hierarchy.Add(this.contentViewport);
			this.m_ContentContainer = new VisualElement
			{
				name = "unity-content-container"
			};
			this.m_ContentContainer.RegisterCallback<GeometryChangedEvent>(new EventCallback<GeometryChangedEvent>(this.OnGeometryChanged), TrickleDown.NoTrickleDown);
			this.m_ContentContainer.AddToClassList(ScrollView.contentUssClassName);
			this.m_ContentContainer.usageHints = UsageHints.GroupTransform;
			this.contentViewport.Add(this.m_ContentContainer);
			this.SetScrollViewMode(scrollViewMode);
			this.horizontalScroller = new Scroller(0f, 2.1474836E+09f, delegate(float value)
			{
				this.scrollOffset = new Vector2(value, this.scrollOffset.y);
				this.UpdateContentViewTransform();
			}, SliderDirection.Horizontal)
			{
				viewDataKey = "HorizontalScroller",
				visible = false
			};
			this.horizontalScroller.AddToClassList(ScrollView.hScrollerUssClassName);
			base.hierarchy.Add(this.horizontalScroller);
			this.verticalScroller = new Scroller(0f, 2.1474836E+09f, delegate(float value)
			{
				this.scrollOffset = new Vector2(this.scrollOffset.x, value);
				this.UpdateContentViewTransform();
			}, SliderDirection.Vertical)
			{
				viewDataKey = "VerticalScroller",
				visible = false
			};
			this.verticalScroller.AddToClassList(ScrollView.vScrollerUssClassName);
			base.hierarchy.Add(this.verticalScroller);
			this.touchScrollBehavior = ScrollView.TouchScrollBehavior.Clamped;
			base.RegisterCallback<WheelEvent>(new EventCallback<WheelEvent>(this.OnScrollWheel), TrickleDown.NoTrickleDown);
			this.scrollOffset = Vector2.zero;
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001B8BC File Offset: 0x00019ABC
		internal void SetScrollViewMode(ScrollViewMode scrollViewMode)
		{
			base.RemoveFromClassList(ScrollView.verticalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.horizontalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.verticalHorizontalVariantUssClassName);
			base.RemoveFromClassList(ScrollView.scrollVariantUssClassName);
			switch (scrollViewMode)
			{
			case ScrollViewMode.Vertical:
				base.AddToClassList(ScrollView.verticalVariantUssClassName);
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				break;
			case ScrollViewMode.Horizontal:
				base.AddToClassList(ScrollView.horizontalVariantUssClassName);
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				break;
			case ScrollViewMode.VerticalAndHorizontal:
				base.AddToClassList(ScrollView.scrollVariantUssClassName);
				base.AddToClassList(ScrollView.verticalHorizontalVariantUssClassName);
				break;
			}
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001B960 File Offset: 0x00019B60
		private void OnAttachToPanel(AttachToPanelEvent evt)
		{
			bool flag = evt.destinationPanel == null;
			if (!flag)
			{
				bool flag2 = evt.destinationPanel.contextType == ContextType.Player;
				if (flag2)
				{
					this.contentViewport.RegisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
					this.contentViewport.RegisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
					this.contentViewport.RegisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
				}
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001B9E0 File Offset: 0x00019BE0
		private void OnDetachFromPanel(DetachFromPanelEvent evt)
		{
			bool flag = evt.originPanel == null;
			if (!flag)
			{
				bool flag2 = evt.originPanel.contextType == ContextType.Player;
				if (flag2)
				{
					this.contentViewport.UnregisterCallback<PointerDownEvent>(new EventCallback<PointerDownEvent>(this.OnPointerDown), TrickleDown.NoTrickleDown);
					this.contentViewport.UnregisterCallback<PointerMoveEvent>(new EventCallback<PointerMoveEvent>(this.OnPointerMove), TrickleDown.NoTrickleDown);
					this.contentViewport.UnregisterCallback<PointerUpEvent>(new EventCallback<PointerUpEvent>(this.OnPointerUp), TrickleDown.NoTrickleDown);
				}
			}
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001BA60 File Offset: 0x00019C60
		private void OnGeometryChanged(GeometryChangedEvent evt)
		{
			bool flag = evt.oldRect.size == evt.newRect.size;
			if (!flag)
			{
				bool flag2 = this.needsVertical;
				bool flag3 = this.needsHorizontal;
				bool flag4 = evt.layoutPass > 0;
				if (flag4)
				{
					flag2 = flag2 || this.verticalScroller.visible;
					flag3 = flag3 || this.horizontalScroller.visible;
				}
				this.UpdateScrollers(flag3, flag2);
				this.UpdateContentViewTransform();
			}
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001BAEC File Offset: 0x00019CEC
		private static float ComputeElasticOffset(float deltaPointer, float initialScrollOffset, float lowLimit, float hardLowLimit, float highLimit, float hardHighLimit)
		{
			initialScrollOffset = Mathf.Max(initialScrollOffset, hardLowLimit * 0.95f);
			initialScrollOffset = Mathf.Min(initialScrollOffset, hardHighLimit * 0.95f);
			bool flag = initialScrollOffset < lowLimit && hardLowLimit < lowLimit;
			float num;
			float num3;
			if (flag)
			{
				num = lowLimit - hardLowLimit;
				float num2 = (lowLimit - initialScrollOffset) / num;
				num3 = num2 * num / (1f - num2);
				num3 += deltaPointer;
				initialScrollOffset = lowLimit;
			}
			else
			{
				bool flag2 = initialScrollOffset > highLimit && hardHighLimit > highLimit;
				if (flag2)
				{
					num = hardHighLimit - highLimit;
					float num4 = (initialScrollOffset - highLimit) / num;
					num3 = -1f * num4 * num / (1f - num4);
					num3 += deltaPointer;
					initialScrollOffset = highLimit;
				}
				else
				{
					num3 = deltaPointer;
				}
			}
			float num5 = initialScrollOffset - num3;
			bool flag3 = num5 < lowLimit;
			float num6;
			if (flag3)
			{
				num3 = lowLimit - num5;
				initialScrollOffset = lowLimit;
				num = lowLimit - hardLowLimit;
				num6 = 1f;
			}
			else
			{
				bool flag4 = num5 <= highLimit;
				if (flag4)
				{
					return num5;
				}
				num3 = num5 - highLimit;
				initialScrollOffset = highLimit;
				num = hardHighLimit - highLimit;
				num6 = -1f;
			}
			bool flag5 = Mathf.Abs(num3) < Mathf.Epsilon;
			float num7;
			if (flag5)
			{
				num7 = initialScrollOffset;
			}
			else
			{
				float num8 = num3 / (num3 + num);
				num8 *= num;
				num8 *= num6;
				num5 = initialScrollOffset - num8;
				num7 = num5;
			}
			return num7;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001BC1C File Offset: 0x00019E1C
		private void ComputeInitialSpringBackVelocity()
		{
			bool flag = this.touchScrollBehavior != ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_SpringBackVelocity = Vector2.zero;
			}
			else
			{
				bool flag2 = this.scrollOffset.x < this.m_LowBounds.x;
				if (flag2)
				{
					this.m_SpringBackVelocity.x = this.m_LowBounds.x - this.scrollOffset.x;
				}
				else
				{
					bool flag3 = this.scrollOffset.x > this.m_HighBounds.x;
					if (flag3)
					{
						this.m_SpringBackVelocity.x = this.m_HighBounds.x - this.scrollOffset.x;
					}
					else
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				bool flag4 = this.scrollOffset.y < this.m_LowBounds.y;
				if (flag4)
				{
					this.m_SpringBackVelocity.y = this.m_LowBounds.y - this.scrollOffset.y;
				}
				else
				{
					bool flag5 = this.scrollOffset.y > this.m_HighBounds.y;
					if (flag5)
					{
						this.m_SpringBackVelocity.y = this.m_HighBounds.y - this.scrollOffset.y;
					}
					else
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
			}
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001BD7C File Offset: 0x00019F7C
		private void SpringBack()
		{
			bool flag = this.touchScrollBehavior != ScrollView.TouchScrollBehavior.Elastic;
			if (flag)
			{
				this.m_SpringBackVelocity = Vector2.zero;
			}
			else
			{
				Vector2 scrollOffset = this.scrollOffset;
				bool flag2 = scrollOffset.x < this.m_LowBounds.x;
				if (flag2)
				{
					scrollOffset.x = Mathf.SmoothDamp(scrollOffset.x, this.m_LowBounds.x, ref this.m_SpringBackVelocity.x, this.elasticity, float.PositiveInfinity, Time.unscaledDeltaTime);
					bool flag3 = Mathf.Abs(this.m_SpringBackVelocity.x) < 1f;
					if (flag3)
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				else
				{
					bool flag4 = scrollOffset.x > this.m_HighBounds.x;
					if (flag4)
					{
						scrollOffset.x = Mathf.SmoothDamp(scrollOffset.x, this.m_HighBounds.x, ref this.m_SpringBackVelocity.x, this.elasticity, float.PositiveInfinity, Time.unscaledDeltaTime);
						bool flag5 = Mathf.Abs(this.m_SpringBackVelocity.x) < 1f;
						if (flag5)
						{
							this.m_SpringBackVelocity.x = 0f;
						}
					}
					else
					{
						this.m_SpringBackVelocity.x = 0f;
					}
				}
				bool flag6 = scrollOffset.y < this.m_LowBounds.y;
				if (flag6)
				{
					scrollOffset.y = Mathf.SmoothDamp(scrollOffset.y, this.m_LowBounds.y, ref this.m_SpringBackVelocity.y, this.elasticity, float.PositiveInfinity, Time.unscaledDeltaTime);
					bool flag7 = Mathf.Abs(this.m_SpringBackVelocity.y) < 1f;
					if (flag7)
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
				else
				{
					bool flag8 = scrollOffset.y > this.m_HighBounds.y;
					if (flag8)
					{
						scrollOffset.y = Mathf.SmoothDamp(scrollOffset.y, this.m_HighBounds.y, ref this.m_SpringBackVelocity.y, this.elasticity, float.PositiveInfinity, Time.unscaledDeltaTime);
						bool flag9 = Mathf.Abs(this.m_SpringBackVelocity.y) < 1f;
						if (flag9)
						{
							this.m_SpringBackVelocity.y = 0f;
						}
					}
					else
					{
						this.m_SpringBackVelocity.y = 0f;
					}
				}
				this.scrollOffset = scrollOffset;
			}
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001BFF0 File Offset: 0x0001A1F0
		private void ApplyScrollInertia()
		{
			bool flag = this.hasInertia && this.m_Velocity != Vector2.zero;
			if (flag)
			{
				this.m_Velocity *= Mathf.Pow(this.scrollDecelerationRate, Time.unscaledDeltaTime);
				bool flag2 = Mathf.Abs(this.m_Velocity.x) < 1f || (this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic && (this.scrollOffset.x < this.m_LowBounds.x || this.scrollOffset.x > this.m_HighBounds.x));
				if (flag2)
				{
					this.m_Velocity.x = 0f;
				}
				bool flag3 = Mathf.Abs(this.m_Velocity.y) < 1f || (this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic && (this.scrollOffset.y < this.m_LowBounds.y || this.scrollOffset.y > this.m_HighBounds.y));
				if (flag3)
				{
					this.m_Velocity.y = 0f;
				}
				this.scrollOffset += this.m_Velocity * Time.unscaledDeltaTime;
			}
			else
			{
				this.m_Velocity = Vector2.zero;
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001C154 File Offset: 0x0001A354
		private void PostPointerUpAnimation()
		{
			this.ApplyScrollInertia();
			this.SpringBack();
			bool flag = this.m_SpringBackVelocity == Vector2.zero && this.m_Velocity == Vector2.zero;
			if (flag)
			{
				this.m_PostPointerUpAnimation.Pause();
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001C1A8 File Offset: 0x0001A3A8
		private void OnPointerDown(PointerDownEvent evt)
		{
			bool flag = evt.pointerType != PointerType.mouse && evt.isPrimary && this.m_ScrollingPointerId == PointerId.invalidPointerId;
			if (flag)
			{
				IVisualElementScheduledItem postPointerUpAnimation = this.m_PostPointerUpAnimation;
				if (postPointerUpAnimation != null)
				{
					postPointerUpAnimation.Pause();
				}
				this.m_ScrollingPointerId = evt.pointerId;
				this.m_PointerStartPosition = evt.position;
				this.m_StartPosition = this.scrollOffset;
				this.m_Velocity = Vector2.zero;
				this.m_SpringBackVelocity = Vector2.zero;
				this.m_LowBounds = new Vector2(Mathf.Min(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Min(this.verticalScroller.lowValue, this.verticalScroller.highValue));
				this.m_HighBounds = new Vector2(Mathf.Max(this.horizontalScroller.lowValue, this.horizontalScroller.highValue), Mathf.Max(this.verticalScroller.lowValue, this.verticalScroller.highValue));
				evt.StopPropagation();
			}
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001C2C4 File Offset: 0x0001A4C4
		private void OnPointerMove(PointerMoveEvent evt)
		{
			bool flag = evt.pointerId == this.m_ScrollingPointerId;
			if (flag)
			{
				bool flag2 = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Clamped;
				Vector2 vector;
				if (flag2)
				{
					vector = this.m_StartPosition - (new Vector2(evt.position.x, evt.position.y) - this.m_PointerStartPosition);
					vector = Vector2.Max(vector, this.m_LowBounds);
					vector = Vector2.Min(vector, this.m_HighBounds);
				}
				else
				{
					bool flag3 = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic;
					if (flag3)
					{
						Vector2 vector2 = new Vector2(evt.position.x, evt.position.y) - this.m_PointerStartPosition;
						vector.x = ScrollView.ComputeElasticOffset(vector2.x, this.m_StartPosition.x, this.m_LowBounds.x, this.m_LowBounds.x - this.contentViewport.resolvedStyle.width, this.m_HighBounds.x, this.m_HighBounds.x + this.contentViewport.resolvedStyle.width);
						vector.y = ScrollView.ComputeElasticOffset(vector2.y, this.m_StartPosition.y, this.m_LowBounds.y, this.m_LowBounds.y - this.contentViewport.resolvedStyle.height, this.m_HighBounds.y, this.m_HighBounds.y + this.contentViewport.resolvedStyle.height);
					}
					else
					{
						vector = this.m_StartPosition - (new Vector2(evt.position.x, evt.position.y) - this.m_PointerStartPosition);
					}
				}
				bool hasInertia = this.hasInertia;
				if (hasInertia)
				{
					float unscaledDeltaTime = Time.unscaledDeltaTime;
					Vector2 vector3 = (vector - this.scrollOffset) / unscaledDeltaTime;
					this.m_Velocity = Vector2.Lerp(this.m_Velocity, vector3, unscaledDeltaTime * 10f);
				}
				this.scrollOffset = vector;
				evt.currentTarget.CapturePointer(evt.pointerId);
				evt.StopPropagation();
			}
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001C4F8 File Offset: 0x0001A6F8
		private void OnPointerUp(PointerUpEvent evt)
		{
			bool flag = evt.pointerId == this.m_ScrollingPointerId;
			if (flag)
			{
				evt.currentTarget.ReleasePointer(evt.pointerId);
				evt.StopPropagation();
				bool flag2 = this.touchScrollBehavior == ScrollView.TouchScrollBehavior.Elastic || this.hasInertia;
				if (flag2)
				{
					this.ComputeInitialSpringBackVelocity();
					bool flag3 = this.m_PostPointerUpAnimation == null;
					if (flag3)
					{
						this.m_PostPointerUpAnimation = base.schedule.Execute(new Action(this.PostPointerUpAnimation)).Every(30L);
					}
					else
					{
						this.m_PostPointerUpAnimation.Resume();
					}
				}
				this.m_ScrollingPointerId = PointerId.invalidPointerId;
			}
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001C5A4 File Offset: 0x0001A7A4
		private void UpdateScrollers(bool displayHorizontal, bool displayVertical)
		{
			float num = ((this.contentContainer.layout.width > Mathf.Epsilon) ? (this.contentViewport.layout.width / this.contentContainer.layout.width) : 1f);
			float num2 = ((this.contentContainer.layout.height > Mathf.Epsilon) ? (this.contentViewport.layout.height / this.contentContainer.layout.height) : 1f);
			this.horizontalScroller.Adjust(num);
			this.verticalScroller.Adjust(num2);
			this.horizontalScroller.SetEnabled(this.contentContainer.layout.width - this.contentViewport.layout.width > 0f);
			this.verticalScroller.SetEnabled(this.contentContainer.layout.height - this.contentViewport.layout.height > 0f);
			this.contentViewport.style.marginRight = (displayVertical ? this.verticalScroller.layout.width : 0f);
			this.horizontalScroller.style.right = (displayVertical ? this.verticalScroller.layout.width : 0f);
			this.contentViewport.style.marginBottom = (displayHorizontal ? this.horizontalScroller.layout.height : 0f);
			this.verticalScroller.style.bottom = (displayHorizontal ? this.horizontalScroller.layout.height : 0f);
			bool flag = displayHorizontal && this.scrollableWidth > 0f;
			if (flag)
			{
				this.horizontalScroller.lowValue = 0f;
				this.horizontalScroller.highValue = this.scrollableWidth;
			}
			else
			{
				this.horizontalScroller.value = 0f;
			}
			bool flag2 = displayVertical && this.scrollableHeight > 0f;
			if (flag2)
			{
				this.verticalScroller.lowValue = 0f;
				this.verticalScroller.highValue = this.scrollableHeight;
			}
			else
			{
				this.verticalScroller.value = 0f;
			}
			bool flag3 = this.horizontalScroller.visible != displayHorizontal;
			if (flag3)
			{
				this.horizontalScroller.visible = displayHorizontal;
			}
			bool flag4 = this.verticalScroller.visible != displayVertical;
			if (flag4)
			{
				this.verticalScroller.visible = displayVertical;
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001C890 File Offset: 0x0001AA90
		private void OnScrollWheel(WheelEvent evt)
		{
			float value = this.verticalScroller.value;
			bool flag = this.contentContainer.layout.height - base.layout.height > 0f;
			if (flag)
			{
				bool flag2 = evt.delta.y < 0f;
				if (flag2)
				{
					this.verticalScroller.ScrollPageUp(Mathf.Abs(evt.delta.y));
				}
				else
				{
					bool flag3 = evt.delta.y > 0f;
					if (flag3)
					{
						this.verticalScroller.ScrollPageDown(Mathf.Abs(evt.delta.y));
					}
				}
			}
			bool flag4 = this.verticalScroller.value != value;
			if (flag4)
			{
				evt.StopPropagation();
			}
		}

		// Token: 0x04000317 RID: 791
		private bool m_ShowHorizontal;

		// Token: 0x04000318 RID: 792
		private bool m_ShowVertical;

		// Token: 0x04000319 RID: 793
		private static readonly float k_DefaultScrollDecelerationRate = 0.135f;

		// Token: 0x0400031A RID: 794
		private float m_ScrollDecelerationRate = ScrollView.k_DefaultScrollDecelerationRate;

		// Token: 0x0400031B RID: 795
		private static readonly float k_DefaultElasticity = 0.1f;

		// Token: 0x0400031C RID: 796
		private float m_Elasticity = ScrollView.k_DefaultElasticity;

		// Token: 0x0400031D RID: 797
		private ScrollView.TouchScrollBehavior m_TouchScrollBehavior;

		// Token: 0x04000321 RID: 801
		private VisualElement m_ContentContainer;

		// Token: 0x04000322 RID: 802
		public static readonly string ussClassName = "unity-scroll-view";

		// Token: 0x04000323 RID: 803
		public static readonly string viewportUssClassName = ScrollView.ussClassName + "__content-viewport";

		// Token: 0x04000324 RID: 804
		public static readonly string contentUssClassName = ScrollView.ussClassName + "__content-container";

		// Token: 0x04000325 RID: 805
		public static readonly string hScrollerUssClassName = ScrollView.ussClassName + "__horizontal-scroller";

		// Token: 0x04000326 RID: 806
		public static readonly string vScrollerUssClassName = ScrollView.ussClassName + "__vertical-scroller";

		// Token: 0x04000327 RID: 807
		public static readonly string horizontalVariantUssClassName = ScrollView.ussClassName + "--horizontal";

		// Token: 0x04000328 RID: 808
		public static readonly string verticalVariantUssClassName = ScrollView.ussClassName + "--vertical";

		// Token: 0x04000329 RID: 809
		public static readonly string verticalHorizontalVariantUssClassName = ScrollView.ussClassName + "--vertical-horizontal";

		// Token: 0x0400032A RID: 810
		public static readonly string scrollVariantUssClassName = ScrollView.ussClassName + "--scroll";

		// Token: 0x0400032B RID: 811
		private int m_ScrollingPointerId = PointerId.invalidPointerId;

		// Token: 0x0400032C RID: 812
		private Vector2 m_StartPosition;

		// Token: 0x0400032D RID: 813
		private Vector2 m_PointerStartPosition;

		// Token: 0x0400032E RID: 814
		private Vector2 m_Velocity;

		// Token: 0x0400032F RID: 815
		private Vector2 m_SpringBackVelocity;

		// Token: 0x04000330 RID: 816
		private Vector2 m_LowBounds;

		// Token: 0x04000331 RID: 817
		private Vector2 m_HighBounds;

		// Token: 0x04000332 RID: 818
		private IVisualElementScheduledItem m_PostPointerUpAnimation;

		// Token: 0x020000EC RID: 236
		public new class UxmlFactory : UxmlFactory<ScrollView, ScrollView.UxmlTraits>
		{
		}

		// Token: 0x020000ED RID: 237
		public new class UxmlTraits : VisualElement.UxmlTraits
		{
			// Token: 0x060006E7 RID: 1767 RVA: 0x0001CA7C File Offset: 0x0001AC7C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				ScrollView scrollView = (ScrollView)ve;
				scrollView.SetScrollViewMode(this.m_ScrollViewMode.GetValueFromBag(bag, cc));
				scrollView.showHorizontal = this.m_ShowHorizontal.GetValueFromBag(bag, cc);
				scrollView.showVertical = this.m_ShowVertical.GetValueFromBag(bag, cc);
				scrollView.horizontalPageSize = this.m_HorizontalPageSize.GetValueFromBag(bag, cc);
				scrollView.verticalPageSize = this.m_VerticalPageSize.GetValueFromBag(bag, cc);
				scrollView.scrollDecelerationRate = this.m_ScrollDecelerationRate.GetValueFromBag(bag, cc);
				scrollView.touchScrollBehavior = this.m_TouchScrollBehavior.GetValueFromBag(bag, cc);
				scrollView.elasticity = this.m_Elasticity.GetValueFromBag(bag, cc);
			}

			// Token: 0x04000333 RID: 819
			private UxmlEnumAttributeDescription<ScrollViewMode> m_ScrollViewMode = new UxmlEnumAttributeDescription<ScrollViewMode>
			{
				name = "mode",
				defaultValue = ScrollViewMode.Vertical
			};

			// Token: 0x04000334 RID: 820
			private UxmlBoolAttributeDescription m_ShowHorizontal = new UxmlBoolAttributeDescription
			{
				name = "show-horizontal-scroller"
			};

			// Token: 0x04000335 RID: 821
			private UxmlBoolAttributeDescription m_ShowVertical = new UxmlBoolAttributeDescription
			{
				name = "show-vertical-scroller"
			};

			// Token: 0x04000336 RID: 822
			private UxmlFloatAttributeDescription m_HorizontalPageSize = new UxmlFloatAttributeDescription
			{
				name = "horizontal-page-size",
				defaultValue = 20f
			};

			// Token: 0x04000337 RID: 823
			private UxmlFloatAttributeDescription m_VerticalPageSize = new UxmlFloatAttributeDescription
			{
				name = "vertical-page-size",
				defaultValue = 20f
			};

			// Token: 0x04000338 RID: 824
			private UxmlEnumAttributeDescription<ScrollView.TouchScrollBehavior> m_TouchScrollBehavior = new UxmlEnumAttributeDescription<ScrollView.TouchScrollBehavior>
			{
				name = "touch-scroll-type",
				defaultValue = ScrollView.TouchScrollBehavior.Clamped
			};

			// Token: 0x04000339 RID: 825
			private UxmlFloatAttributeDescription m_ScrollDecelerationRate = new UxmlFloatAttributeDescription
			{
				name = "scroll-deceleration-rate",
				defaultValue = ScrollView.k_DefaultScrollDecelerationRate
			};

			// Token: 0x0400033A RID: 826
			private UxmlFloatAttributeDescription m_Elasticity = new UxmlFloatAttributeDescription
			{
				name = "elasticity",
				defaultValue = ScrollView.k_DefaultElasticity
			};
		}

		// Token: 0x020000EE RID: 238
		public enum TouchScrollBehavior
		{
			// Token: 0x0400033C RID: 828
			Unrestricted,
			// Token: 0x0400033D RID: 829
			Elastic,
			// Token: 0x0400033E RID: 830
			Clamped
		}
	}
}
