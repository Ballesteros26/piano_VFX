using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.Experimental;
using UnityEngine.UIElements.StyleSheets;
using UnityEngine.UIElements.UIR;
using UnityEngine.Yoga;

namespace UnityEngine.UIElements
{
	// Token: 0x02000087 RID: 135
	public class VisualElement : Focusable, ITransform, ITransitionAnimations, IExperimentalFeatures, IVisualElementScheduler, IResolvedStyle
	{
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000346 RID: 838 RVA: 0x0000BF74 File Offset: 0x0000A174
		// (set) Token: 0x06000347 RID: 839 RVA: 0x0000BF7C File Offset: 0x0000A17C
		internal bool isCompositeRoot { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000BF88 File Offset: 0x0000A188
		// (set) Token: 0x06000349 RID: 841 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		public string viewDataKey
		{
			get
			{
				return this.m_ViewDataKey;
			}
			set
			{
				bool flag = this.m_ViewDataKey != value;
				if (flag)
				{
					this.m_ViewDataKey = value;
					bool flag2 = !string.IsNullOrEmpty(value);
					if (flag2)
					{
						this.IncrementVersion(VersionChangeType.ViewData);
					}
				}
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600034A RID: 842 RVA: 0x0000BFDC File Offset: 0x0000A1DC
		// (set) Token: 0x0600034B RID: 843 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
		internal bool enableViewDataPersistence { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600034C RID: 844 RVA: 0x0000BFF0 File Offset: 0x0000A1F0
		// (set) Token: 0x0600034D RID: 845 RVA: 0x0000C00D File Offset: 0x0000A20D
		public object userData
		{
			get
			{
				return this.GetPropertyInternal(VisualElement.userDataPropertyKey);
			}
			set
			{
				this.SetPropertyInternal(VisualElement.userDataPropertyKey, value);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600034E RID: 846 RVA: 0x0000C020 File Offset: 0x0000A220
		public override bool canGrabFocus
		{
			get
			{
				bool flag = false;
				for (VisualElement visualElement = this.hierarchy.parent; visualElement != null; visualElement = visualElement.parent)
				{
					bool isCompositeRoot = visualElement.isCompositeRoot;
					if (isCompositeRoot)
					{
						flag |= !visualElement.canGrabFocus;
						break;
					}
				}
				return !flag && this.visible && this.resolvedStyle.display != DisplayStyle.None && this.enabledInHierarchy && base.canGrabFocus;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000C0A0 File Offset: 0x0000A2A0
		public override FocusController focusController
		{
			get
			{
				IPanel panel = this.panel;
				return (panel != null) ? panel.focusController : null;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0000C0C4 File Offset: 0x0000A2C4
		// (set) Token: 0x06000351 RID: 849 RVA: 0x0000C0F4 File Offset: 0x0000A2F4
		public UsageHints usageHints
		{
			get
			{
				return (((this.m_RenderHints & RenderHints.GroupTransform) != RenderHints.None) ? UsageHints.GroupTransform : UsageHints.None) | (((this.m_RenderHints & RenderHints.BoneTransform) != RenderHints.None) ? UsageHints.DynamicTransform : UsageHints.None);
			}
			set
			{
				bool flag = this.panel != null;
				if (flag)
				{
					throw new InvalidOperationException("usageHints cannot be changed once the VisualElement is part of an active visual tree");
				}
				bool flag2 = (value & UsageHints.GroupTransform) > UsageHints.None;
				if (flag2)
				{
					this.m_RenderHints |= RenderHints.GroupTransform;
				}
				else
				{
					this.m_RenderHints &= ~RenderHints.GroupTransform;
				}
				bool flag3 = (value & UsageHints.DynamicTransform) > UsageHints.None;
				if (flag3)
				{
					this.m_RenderHints |= RenderHints.BoneTransform;
				}
				else
				{
					this.m_RenderHints &= ~RenderHints.BoneTransform;
				}
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000352 RID: 850 RVA: 0x0000C16C File Offset: 0x0000A36C
		// (set) Token: 0x06000353 RID: 851 RVA: 0x0000C184 File Offset: 0x0000A384
		internal RenderHints renderHints
		{
			get
			{
				return this.m_RenderHints;
			}
			set
			{
				bool flag = this.panel != null;
				if (flag)
				{
					throw new InvalidOperationException("renderHints cannot be changed once the VisualElement is part of an active visual tree");
				}
				this.m_RenderHints = value;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		public ITransform transform
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000C1C8 File Offset: 0x0000A3C8
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000C1E0 File Offset: 0x0000A3E0
		Vector3 ITransform.position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				bool flag = this.m_Position == value;
				if (!flag)
				{
					this.m_Position = value;
					this.IncrementVersion(VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000C214 File Offset: 0x0000A414
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000C22C File Offset: 0x0000A42C
		Quaternion ITransform.rotation
		{
			get
			{
				return this.m_Rotation;
			}
			set
			{
				bool flag = this.m_Rotation == value;
				if (!flag)
				{
					this.m_Rotation = value;
					this.IncrementVersion(VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000C260 File Offset: 0x0000A460
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000C278 File Offset: 0x0000A478
		Vector3 ITransform.scale
		{
			get
			{
				return this.m_Scale;
			}
			set
			{
				bool flag = this.m_Scale == value;
				if (!flag)
				{
					this.m_Scale = value;
					this.IncrementVersion(VersionChangeType.Layout | VersionChangeType.Transform);
				}
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000C2AC File Offset: 0x0000A4AC
		internal Vector3 ComputeGlobalScale()
		{
			Vector3 scale = this.m_Scale;
			for (VisualElement visualElement = this.hierarchy.parent; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				scale.Scale(visualElement.m_Scale);
			}
			return scale;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600035C RID: 860 RVA: 0x0000C300 File Offset: 0x0000A500
		Matrix4x4 ITransform.matrix
		{
			get
			{
				return Matrix4x4.TRS(this.m_Position, this.m_Rotation, this.m_Scale);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000C329 File Offset: 0x0000A529
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000C331 File Offset: 0x0000A531
		internal bool isLayoutManual { get; private set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000C33C File Offset: 0x0000A53C
		internal float scaledPixelsPerPoint
		{
			get
			{
				return (this.panel == null) ? GUIUtility.pixelsPerPoint : (this.panel as BaseVisualElementPanel).scaledPixelsPerPoint;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000C370 File Offset: 0x0000A570
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000C3F0 File Offset: 0x0000A5F0
		public Rect layout
		{
			get
			{
				Rect layout = this.m_Layout;
				bool flag = this.yogaNode != null && !this.isLayoutManual;
				if (flag)
				{
					layout.x = this.yogaNode.LayoutX;
					layout.y = this.yogaNode.LayoutY;
					layout.width = this.yogaNode.LayoutWidth;
					layout.height = this.yogaNode.LayoutHeight;
				}
				return layout;
			}
			internal set
			{
				bool flag = this.yogaNode == null;
				if (flag)
				{
					this.yogaNode = new YogaNode(null);
				}
				bool flag2 = this.isLayoutManual && this.m_Layout == value;
				if (!flag2)
				{
					Rect layout = this.layout;
					VersionChangeType versionChangeType = (VersionChangeType)0;
					bool flag3 = !Mathf.Approximately(layout.x, value.x) || !Mathf.Approximately(layout.y, value.y);
					if (flag3)
					{
						versionChangeType |= VersionChangeType.Transform;
					}
					bool flag4 = !Mathf.Approximately(layout.width, value.width) || !Mathf.Approximately(layout.height, value.height);
					if (flag4)
					{
						versionChangeType |= VersionChangeType.Size;
					}
					this.m_Layout = value;
					this.isLayoutManual = true;
					IStyle style = this.style;
					style.position = Position.Absolute;
					style.marginLeft = 0f;
					style.marginRight = 0f;
					style.marginBottom = 0f;
					style.marginTop = 0f;
					style.left = value.x;
					style.top = value.y;
					style.right = float.NaN;
					style.bottom = float.NaN;
					style.width = value.width;
					style.height = value.height;
					bool flag5 = versionChangeType > (VersionChangeType)0;
					if (flag5)
					{
						this.IncrementVersion(versionChangeType);
					}
				}
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000C5A4 File Offset: 0x0000A7A4
		public Rect contentRect
		{
			get
			{
				Spacing spacing = new Spacing(this.resolvedStyle.paddingLeft, this.resolvedStyle.paddingTop, this.resolvedStyle.paddingRight, this.resolvedStyle.paddingBottom);
				return this.paddingRect - spacing;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
		protected Rect paddingRect
		{
			get
			{
				Spacing spacing = new Spacing(this.resolvedStyle.borderLeftWidth, this.resolvedStyle.borderTopWidth, this.resolvedStyle.borderRightWidth, this.resolvedStyle.borderBottomWidth);
				return this.rect - spacing;
			}
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0000C64C File Offset: 0x0000A84C
		internal static Rect TransformAlignedRect(Matrix4x4 lhc, Rect rect)
		{
			Vector2 vector = VisualElement.MultiplyMatrix44Point2(lhc, rect.min);
			Vector2 vector2 = VisualElement.MultiplyMatrix44Point2(lhc, rect.max);
			return Rect.MinMaxRect(Math.Min(vector.x, vector2.x), Math.Min(vector.y, vector2.y), Math.Max(vector.x, vector2.x), Math.Max(vector.y, vector2.y));
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
		internal static Vector2 MultiplyMatrix44Point2(Matrix4x4 lhs, Vector2 point)
		{
			Vector2 vector;
			vector.x = lhs.m00 * point.x + lhs.m01 * point.y + lhs.m03;
			vector.y = lhs.m10 * point.x + lhs.m11 * point.y + lhs.m13;
			return vector;
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000C72C File Offset: 0x0000A92C
		internal Rect boundingBox
		{
			get
			{
				bool flag = this.isBoundingBoxDirty;
				if (flag)
				{
					this.UpdateBoundingBox();
					this.isBoundingBoxDirty = false;
				}
				return this.m_BoundingBox;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000C760 File Offset: 0x0000A960
		internal Rect worldBoundingBox
		{
			get
			{
				bool flag = this.isWorldBoundingBoxDirty || this.isBoundingBoxDirty;
				if (flag)
				{
					this.UpdateWorldBoundingBox();
					this.isWorldBoundingBoxDirty = false;
				}
				return this.m_WorldBoundingBox;
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0000C7A0 File Offset: 0x0000A9A0
		internal void UpdateBoundingBox()
		{
			bool flag = float.IsNaN(this.rect.x) || float.IsNaN(this.rect.y) || float.IsNaN(this.rect.width) || float.IsNaN(this.rect.height);
			if (flag)
			{
				this.m_BoundingBox = Rect.zero;
			}
			else
			{
				this.m_BoundingBox = this.rect;
				int count = this.m_Children.Count;
				for (int i = 0; i < count; i++)
				{
					Rect rect = this.m_Children[i].boundingBox;
					rect = this.m_Children[i].ChangeCoordinatesTo(this, rect);
					this.m_BoundingBox.xMin = Math.Min(this.m_BoundingBox.xMin, rect.xMin);
					this.m_BoundingBox.xMax = Math.Max(this.m_BoundingBox.xMax, rect.xMax);
					this.m_BoundingBox.yMin = Math.Min(this.m_BoundingBox.yMin, rect.yMin);
					this.m_BoundingBox.yMax = Math.Max(this.m_BoundingBox.yMax, rect.yMax);
				}
			}
			this.isWorldBoundingBoxDirty = true;
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0000C909 File Offset: 0x0000AB09
		internal void UpdateWorldBoundingBox()
		{
			this.m_WorldBoundingBox = VisualElement.TransformAlignedRect(this.worldTransform, this.boundingBox);
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600036A RID: 874 RVA: 0x0000C924 File Offset: 0x0000AB24
		public Rect worldBound
		{
			get
			{
				Matrix4x4 worldTransform = this.worldTransform;
				return VisualElement.TransformAlignedRect(worldTransform, this.rect);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000C94C File Offset: 0x0000AB4C
		public Rect localBound
		{
			get
			{
				Matrix4x4 matrix = this.transform.matrix;
				Rect layout = this.layout;
				return VisualElement.TransformAlignedRect(matrix, layout);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600036C RID: 876 RVA: 0x0000C978 File Offset: 0x0000AB78
		internal Rect rect
		{
			get
			{
				return new Rect(0f, 0f, this.layout.width, this.layout.height);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000C9B5 File Offset: 0x0000ABB5
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000C9BD File Offset: 0x0000ABBD
		internal bool isWorldTransformDirty { get; set; } = true;

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000C9C6 File Offset: 0x0000ABC6
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000C9CE File Offset: 0x0000ABCE
		internal bool isWorldTransformInverseDirty { get; set; } = true;

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000C9D8 File Offset: 0x0000ABD8
		public Matrix4x4 worldTransform
		{
			get
			{
				bool isWorldTransformDirty = this.isWorldTransformDirty;
				if (isWorldTransformDirty)
				{
					this.UpdateWorldTransform();
				}
				return this.m_WorldTransformCache;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000CA04 File Offset: 0x0000AC04
		internal Matrix4x4 worldTransformInverse
		{
			get
			{
				bool flag = this.isWorldTransformDirty || this.isWorldTransformInverseDirty;
				if (flag)
				{
					this.m_WorldTransformInverseCache = this.worldTransform.inverse;
					this.isWorldTransformInverseDirty = false;
				}
				return this.m_WorldTransformInverseCache;
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0000CA50 File Offset: 0x0000AC50
		private void UpdateWorldTransform()
		{
			bool flag = this.elementPanel != null && !this.elementPanel.duringLayoutPhase;
			if (flag)
			{
				this.isWorldTransformDirty = false;
			}
			Matrix4x4 matrix4x = Matrix4x4.Translate(new Vector3(this.layout.x, this.layout.y, 0f));
			bool flag2 = this.hierarchy.parent != null;
			if (flag2)
			{
				this.m_WorldTransformCache = this.hierarchy.parent.worldTransform * matrix4x * this.transform.matrix;
			}
			else
			{
				this.m_WorldTransformCache = matrix4x * this.transform.matrix;
			}
			this.isWorldTransformInverseDirty = true;
			this.isWorldBoundingBoxDirty = true;
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000374 RID: 884 RVA: 0x0000CB23 File Offset: 0x0000AD23
		// (set) Token: 0x06000375 RID: 885 RVA: 0x0000CB2B File Offset: 0x0000AD2B
		internal bool isWorldClipDirty { get; set; } = true;

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000376 RID: 886 RVA: 0x0000CB34 File Offset: 0x0000AD34
		internal Rect worldClip
		{
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClip;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000CB68 File Offset: 0x0000AD68
		internal Rect worldClipMinusGroup
		{
			get
			{
				bool isWorldClipDirty = this.isWorldClipDirty;
				if (isWorldClipDirty)
				{
					this.UpdateWorldClip();
					this.isWorldClipDirty = false;
				}
				return this.m_WorldClipMinusGroup;
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000CB9C File Offset: 0x0000AD9C
		internal void EnsureWorldTransformAndClipUpToDate()
		{
			bool isWorldTransformDirty = this.isWorldTransformDirty;
			if (isWorldTransformDirty)
			{
				this.UpdateWorldTransform();
			}
			bool isWorldClipDirty = this.isWorldClipDirty;
			if (isWorldClipDirty)
			{
				this.UpdateWorldClip();
				this.isWorldClipDirty = false;
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000CBD8 File Offset: 0x0000ADD8
		private void UpdateWorldClip()
		{
			bool flag = this.hierarchy.parent != null;
			if (flag)
			{
				this.m_WorldClip = this.hierarchy.parent.worldClip;
				bool flag2 = this.hierarchy.parent != this.renderChainData.groupTransformAncestor;
				if (flag2)
				{
					this.m_WorldClipMinusGroup = this.hierarchy.parent.worldClipMinusGroup;
				}
				else
				{
					IPanel panel = this.panel;
					this.m_WorldClipMinusGroup = ((panel != null && panel.contextType == ContextType.Player) ? VisualElement.s_InfiniteRect : GUIClip.topmostRect);
				}
				bool flag3 = this.ShouldClip();
				if (flag3)
				{
					Rect rect = this.SubstractBorderPadding(this.worldBound);
					float num = Mathf.Max(rect.xMin, this.m_WorldClip.xMin);
					float num2 = Mathf.Min(rect.xMax, this.m_WorldClip.xMax);
					float num3 = Mathf.Max(rect.yMin, this.m_WorldClip.yMin);
					float num4 = Mathf.Min(rect.yMax, this.m_WorldClip.yMax);
					float num5 = Mathf.Max(num2 - num, 0f);
					float num6 = Mathf.Max(num4 - num3, 0f);
					this.m_WorldClip = new Rect(num, num3, num5, num6);
					num = Mathf.Max(rect.xMin, this.m_WorldClipMinusGroup.xMin);
					num2 = Mathf.Min(rect.xMax, this.m_WorldClipMinusGroup.xMax);
					num3 = Mathf.Max(rect.yMin, this.m_WorldClipMinusGroup.yMin);
					num4 = Mathf.Min(rect.yMax, this.m_WorldClipMinusGroup.yMax);
					num5 = Mathf.Max(num2 - num, 0f);
					num6 = Mathf.Max(num4 - num3, 0f);
					this.m_WorldClipMinusGroup = new Rect(num, num3, num5, num6);
				}
			}
			else
			{
				this.m_WorldClipMinusGroup = (this.m_WorldClip = ((this.panel != null) ? this.panel.visualTree.rect : VisualElement.s_InfiniteRect));
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000CE04 File Offset: 0x0000B004
		private Rect SubstractBorderPadding(Rect worldRect)
		{
			float m = this.worldTransform.m00;
			float m2 = this.worldTransform.m11;
			worldRect.x += this.resolvedStyle.borderLeftWidth * m;
			worldRect.y += this.resolvedStyle.borderTopWidth * m2;
			worldRect.width -= (this.resolvedStyle.borderLeftWidth + this.resolvedStyle.borderRightWidth) * m;
			worldRect.height -= (this.resolvedStyle.borderTopWidth + this.resolvedStyle.borderBottomWidth) * m2;
			bool flag = this.computedStyle.unityOverflowClipBox == OverflowClipBox.ContentBox;
			if (flag)
			{
				worldRect.x += this.resolvedStyle.paddingLeft * m;
				worldRect.y += this.resolvedStyle.paddingTop * m2;
				worldRect.width -= (this.resolvedStyle.paddingLeft + this.resolvedStyle.paddingRight) * m;
				worldRect.height -= (this.resolvedStyle.paddingTop + this.resolvedStyle.paddingBottom) * m2;
			}
			return worldRect;
		}

		// Token: 0x0600037B RID: 891 RVA: 0x0000CF60 File Offset: 0x0000B160
		internal static Rect ComputeAAAlignedBound(Rect position, Matrix4x4 mat)
		{
			Rect rect = position;
			Vector3 vector = mat.MultiplyPoint3x4(new Vector3(rect.x, rect.y, 0f));
			Vector3 vector2 = mat.MultiplyPoint3x4(new Vector3(rect.x + rect.width, rect.y, 0f));
			Vector3 vector3 = mat.MultiplyPoint3x4(new Vector3(rect.x, rect.y + rect.height, 0f));
			Vector3 vector4 = mat.MultiplyPoint3x4(new Vector3(rect.x + rect.width, rect.y + rect.height, 0f));
			return Rect.MinMaxRect(Mathf.Min(vector.x, Mathf.Min(vector2.x, Mathf.Min(vector3.x, vector4.x))), Mathf.Min(vector.y, Mathf.Min(vector2.y, Mathf.Min(vector3.y, vector4.y))), Mathf.Max(vector.x, Mathf.Max(vector2.x, Mathf.Max(vector3.x, vector4.x))), Mathf.Max(vector.y, Mathf.Max(vector2.y, Mathf.Max(vector3.y, vector4.y))));
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600037C RID: 892 RVA: 0x0000D0BC File Offset: 0x0000B2BC
		// (set) Token: 0x0600037D RID: 893 RVA: 0x0000D0D4 File Offset: 0x0000B2D4
		internal PseudoStates pseudoStates
		{
			get
			{
				return this.m_PseudoStates;
			}
			set
			{
				bool flag = this.m_PseudoStates != value;
				if (flag)
				{
					this.m_PseudoStates = value;
					bool flag2 = (this.triggerPseudoMask & this.m_PseudoStates) != (PseudoStates)0 || (this.dependencyPseudoMask & ~this.m_PseudoStates) > (PseudoStates)0;
					if (flag2)
					{
						this.IncrementVersion(VersionChangeType.StyleSheet);
					}
				}
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600037E RID: 894 RVA: 0x0000D12D File Offset: 0x0000B32D
		// (set) Token: 0x0600037F RID: 895 RVA: 0x0000D135 File Offset: 0x0000B335
		public PickingMode pickingMode { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000380 RID: 896 RVA: 0x0000D140 File Offset: 0x0000B340
		// (set) Token: 0x06000381 RID: 897 RVA: 0x0000D158 File Offset: 0x0000B358
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = this.m_Name == value;
				if (!flag)
				{
					this.m_Name = value;
					this.IncrementVersion(VersionChangeType.StyleSheet);
				}
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000382 RID: 898 RVA: 0x0000D188 File Offset: 0x0000B388
		internal List<string> classList
		{
			get
			{
				return this.m_ClassList;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000D1A0 File Offset: 0x0000B3A0
		internal string fullTypeName
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.m_FullTypeName);
				if (flag)
				{
					this.m_FullTypeName = base.GetType().FullName;
				}
				return this.m_FullTypeName;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000D1D8 File Offset: 0x0000B3D8
		internal string typeName
		{
			get
			{
				bool flag = string.IsNullOrEmpty(this.m_TypeName);
				if (flag)
				{
					Type type = base.GetType();
					bool isGenericType = type.IsGenericType;
					this.m_TypeName = type.Name;
					bool flag2 = isGenericType;
					if (flag2)
					{
						int num = this.m_TypeName.IndexOf('`');
						bool flag3 = num >= 0;
						if (flag3)
						{
							this.m_TypeName = this.m_TypeName.Remove(num);
						}
					}
				}
				return this.m_TypeName;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000D256 File Offset: 0x0000B456
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000D25E File Offset: 0x0000B45E
		internal YogaNode yogaNode { get; private set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000D267 File Offset: 0x0000B467
		internal ComputedStyle sharedStyle
		{
			get
			{
				return this.m_SharedStyle;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000388 RID: 904 RVA: 0x0000D26F File Offset: 0x0000B46F
		internal ComputedStyle computedStyle
		{
			get
			{
				return this.m_Style;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000D277 File Offset: 0x0000B477
		internal bool hasInlineStyle
		{
			get
			{
				return this.m_Style != this.m_SharedStyle;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600038A RID: 906 RVA: 0x0000D28C File Offset: 0x0000B48C
		// (set) Token: 0x0600038B RID: 907 RVA: 0x0000D2A9 File Offset: 0x0000B4A9
		internal float opacity
		{
			get
			{
				return this.resolvedStyle.opacity;
			}
			set
			{
				this.style.opacity = value;
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0000D2C0 File Offset: 0x0000B4C0
		private void ChangeIMGUIContainerCount(int delta)
		{
			for (VisualElement visualElement = this; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				visualElement.imguiContainerDescendantCount += delta;
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0000D2FC File Offset: 0x0000B4FC
		public VisualElement()
		{
			this.m_Children = VisualElement.s_EmptyList;
			this.controlid = (VisualElement.s_NextId += 1U);
			this.hierarchy = new VisualElement.Hierarchy(this);
			this.m_ClassList = VisualElement.s_EmptyClassList;
			this.m_FullTypeName = string.Empty;
			this.m_TypeName = string.Empty;
			this.SetEnabled(true);
			base.focusable = false;
			this.name = string.Empty;
			this.yogaNode = new YogaNode(null);
			this.renderHints = RenderHints.None;
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0000D434 File Offset: 0x0000B634
		protected override void ExecuteDefaultAction(EventBase evt)
		{
			base.ExecuteDefaultAction(evt);
			bool flag = evt == null;
			if (!flag)
			{
				bool flag2 = evt.eventTypeId == EventBase<MouseOverEvent>.TypeId() || evt.eventTypeId == EventBase<MouseOutEvent>.TypeId();
				if (flag2)
				{
					this.UpdateCursorStyle(evt.eventTypeId);
				}
				else
				{
					bool flag3 = evt.eventTypeId == EventBase<MouseEnterEvent>.TypeId();
					if (flag3)
					{
						this.pseudoStates |= PseudoStates.Hover;
					}
					else
					{
						bool flag4 = evt.eventTypeId == EventBase<MouseLeaveEvent>.TypeId();
						if (flag4)
						{
							this.pseudoStates &= ~PseudoStates.Hover;
						}
						else
						{
							bool flag5 = evt.eventTypeId == EventBase<BlurEvent>.TypeId();
							if (flag5)
							{
								this.pseudoStates &= ~PseudoStates.Focus;
							}
							else
							{
								bool flag6 = evt.eventTypeId == EventBase<FocusEvent>.TypeId();
								if (flag6)
								{
									this.pseudoStates |= PseudoStates.Focus;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000D524 File Offset: 0x0000B724
		public sealed override void Focus()
		{
			bool flag = !this.canGrabFocus && this.hierarchy.parent != null;
			if (flag)
			{
				this.hierarchy.parent.Focus();
			}
			else
			{
				base.Focus();
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000D574 File Offset: 0x0000B774
		internal void SetPanel(BaseVisualElementPanel p)
		{
			bool flag = this.panel == p;
			if (!flag)
			{
				List<VisualElement> list = VisualElementListPool.Get(0);
				try
				{
					list.Add(this);
					this.GatherAllChildren(list);
					EventDispatcherGate? eventDispatcherGate = default(EventDispatcherGate?);
					bool flag2 = ((p != null) ? p.dispatcher : null) != null;
					if (flag2)
					{
						eventDispatcherGate = new EventDispatcherGate?(new EventDispatcherGate(p.dispatcher));
					}
					EventDispatcherGate? eventDispatcherGate2 = default(EventDispatcherGate?);
					IPanel panel = this.panel;
					bool flag3 = ((panel != null) ? panel.dispatcher : null) != null && this.panel.dispatcher != ((p != null) ? p.dispatcher : null);
					if (flag3)
					{
						eventDispatcherGate2 = new EventDispatcherGate?(new EventDispatcherGate(this.panel.dispatcher));
					}
					EventDispatcherGate? eventDispatcherGate3 = eventDispatcherGate;
					try
					{
						EventDispatcherGate? eventDispatcherGate4 = eventDispatcherGate2;
						try
						{
							foreach (VisualElement visualElement in list)
							{
								visualElement.ChangePanel(p);
							}
						}
						finally
						{
							if (eventDispatcherGate4 != null)
							{
								eventDispatcherGate4.GetValueOrDefault().Dispose();
							}
						}
					}
					finally
					{
						if (eventDispatcherGate3 != null)
						{
							eventDispatcherGate3.GetValueOrDefault().Dispose();
						}
					}
				}
				finally
				{
					VisualElementListPool.Release(list);
				}
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x0000D730 File Offset: 0x0000B930
		private void ChangePanel(BaseVisualElementPanel p)
		{
			bool flag = this.panel == p;
			if (!flag)
			{
				bool flag2 = this.panel != null;
				if (flag2)
				{
					using (DetachFromPanelEvent pooled = PanelChangedEventBase<DetachFromPanelEvent>.GetPooled(this.panel, p))
					{
						pooled.target = this;
						this.elementPanel.SendEvent(pooled, DispatchMode.Immediate);
					}
					this.UnregisterRunningAnimations();
				}
				IPanel panel = this.panel;
				this.elementPanel = p;
				bool flag3 = this.panel != null;
				if (flag3)
				{
					this.yogaNode.Config = this.elementPanel.yogaConfig;
					this.RegisterRunningAnimations();
					using (AttachToPanelEvent pooled2 = PanelChangedEventBase<AttachToPanelEvent>.GetPooled(panel, p))
					{
						pooled2.target = this;
						this.elementPanel.SendEvent(pooled2, DispatchMode.Default);
					}
				}
				else
				{
					this.yogaNode.Config = YogaConfig.Default;
				}
				this.IncrementVersion(VersionChangeType.Layout | VersionChangeType.StyleSheet | VersionChangeType.Transform);
				bool flag4 = !string.IsNullOrEmpty(this.viewDataKey);
				if (flag4)
				{
					this.IncrementVersion(VersionChangeType.ViewData);
				}
			}
		}

		// Token: 0x06000392 RID: 914 RVA: 0x0000D864 File Offset: 0x0000BA64
		public sealed override void SendEvent(EventBase e)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.SendEvent(e, DispatchMode.Default);
			}
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0000D87B File Offset: 0x0000BA7B
		internal void IncrementVersion(VersionChangeType changeType)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.OnVersionChanged(this, changeType);
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000D892 File Offset: 0x0000BA92
		internal void InvokeHierarchyChanged(HierarchyChangeType changeType)
		{
			BaseVisualElementPanel elementPanel = this.elementPanel;
			if (elementPanel != null)
			{
				elementPanel.InvokeHierarchyChanged(this, changeType);
			}
		}

		// Token: 0x06000395 RID: 917 RVA: 0x0000D8AC File Offset: 0x0000BAAC
		[Obsolete("SetEnabledFromHierarchy is deprecated and will be removed in a future release. Please use SetEnabled instead.")]
		protected internal bool SetEnabledFromHierarchy(bool state)
		{
			return this.SetEnabledFromHierarchyPrivate(state);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		private bool SetEnabledFromHierarchyPrivate(bool state)
		{
			bool enabledInHierarchy = this.enabledInHierarchy;
			if (state)
			{
				bool isParentEnabledInHierarchy = this.isParentEnabledInHierarchy;
				if (isParentEnabledInHierarchy)
				{
					bool enabledSelf = this.enabledSelf;
					if (enabledSelf)
					{
						this.pseudoStates &= ~PseudoStates.Disabled;
						this.RemoveFromClassList(VisualElement.disabledUssClassName);
					}
					else
					{
						this.pseudoStates |= PseudoStates.Disabled;
						this.AddToClassList(VisualElement.disabledUssClassName);
					}
				}
				else
				{
					this.pseudoStates |= PseudoStates.Disabled;
					this.RemoveFromClassList(VisualElement.disabledUssClassName);
				}
			}
			else
			{
				this.pseudoStates |= PseudoStates.Disabled;
				this.EnableInClassList(VisualElement.disabledUssClassName, this.isParentEnabledInHierarchy);
			}
			return enabledInHierarchy != this.enabledInHierarchy;
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000D990 File Offset: 0x0000BB90
		private bool isParentEnabledInHierarchy
		{
			get
			{
				return this.hierarchy.parent == null || this.hierarchy.parent.enabledInHierarchy;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000398 RID: 920 RVA: 0x0000D9C8 File Offset: 0x0000BBC8
		public bool enabledInHierarchy
		{
			get
			{
				return (this.pseudoStates & PseudoStates.Disabled) != PseudoStates.Disabled;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000D9EA File Offset: 0x0000BBEA
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000D9F2 File Offset: 0x0000BBF2
		public bool enabledSelf { get; private set; }

		// Token: 0x0600039B RID: 923 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		public void SetEnabled(bool value)
		{
			bool flag = this.enabledSelf == value;
			if (!flag)
			{
				this.enabledSelf = value;
				this.PropagateEnabledToChildren(value);
			}
		}

		// Token: 0x0600039C RID: 924 RVA: 0x0000DA2C File Offset: 0x0000BC2C
		private void PropagateEnabledToChildren(bool value)
		{
			bool flag = this.SetEnabledFromHierarchyPrivate(value);
			if (flag)
			{
				int count = this.m_Children.Count;
				for (int i = 0; i < count; i++)
				{
					this.m_Children[i].PropagateEnabledToChildren(value);
				}
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000DA78 File Offset: 0x0000BC78
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000DA98 File Offset: 0x0000BC98
		public bool visible
		{
			get
			{
				return this.resolvedStyle.visibility == Visibility.Visible;
			}
			set
			{
				this.style.visibility = (value ? Visibility.Visible : Visibility.Hidden);
			}
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0000DAB3 File Offset: 0x0000BCB3
		public void MarkDirtyRepaint()
		{
			this.IncrementVersion(VersionChangeType.Repaint);
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060003A0 RID: 928 RVA: 0x0000DAC2 File Offset: 0x0000BCC2
		// (set) Token: 0x060003A1 RID: 929 RVA: 0x0000DACA File Offset: 0x0000BCCA
		public Action<MeshGenerationContext> generateVisualContent { get; set; }

		// Token: 0x060003A2 RID: 930 RVA: 0x0000DAD4 File Offset: 0x0000BCD4
		internal void InvokeGenerateVisualContent(MeshGenerationContext mgc)
		{
			bool flag = this.generateVisualContent != null;
			if (flag)
			{
				try
				{
					this.generateVisualContent.Invoke(mgc);
				}
				catch (Exception ex)
				{
					Debug.LogException(ex);
				}
			}
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0000DB20 File Offset: 0x0000BD20
		internal void GetFullHierarchicalViewDataKey(StringBuilder key)
		{
			bool flag = this.parent != null;
			if (flag)
			{
				this.parent.GetFullHierarchicalViewDataKey(key);
			}
			bool flag2 = !string.IsNullOrEmpty(this.viewDataKey);
			if (flag2)
			{
				key.Append("__");
				key.Append(this.viewDataKey);
			}
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000DB78 File Offset: 0x0000BD78
		internal string GetFullHierarchicalViewDataKey()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.GetFullHierarchicalViewDataKey(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0000DBA0 File Offset: 0x0000BDA0
		internal T GetOrCreateViewData<T>(object existing, string key) where T : class, new()
		{
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load persistent data.");
			ISerializableJsonDictionary serializableJsonDictionary = ((this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary());
			bool flag = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			T t;
			if (flag)
			{
				bool flag2 = existing != null;
				if (flag2)
				{
					t = existing as T;
				}
				else
				{
					t = new T();
				}
			}
			else
			{
				string text = key + "__" + typeof(T);
				bool flag3 = !serializableJsonDictionary.ContainsKey(text);
				if (flag3)
				{
					serializableJsonDictionary.Set<T>(text, new T());
				}
				t = serializableJsonDictionary.Get<T>(text);
			}
			return t;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000DC6C File Offset: 0x0000BE6C
		internal T GetOrCreateViewData<T>(ScriptableObject existing, string key) where T : ScriptableObject
		{
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load view data.");
			ISerializableJsonDictionary serializableJsonDictionary = ((this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary());
			bool flag = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			T t;
			if (flag)
			{
				bool flag2 = existing != null;
				if (flag2)
				{
					t = existing as T;
				}
				else
				{
					t = ScriptableObject.CreateInstance<T>();
				}
			}
			else
			{
				string text = key + "__" + typeof(T);
				bool flag3 = !serializableJsonDictionary.ContainsKey(text);
				if (flag3)
				{
					serializableJsonDictionary.Set<T>(text, ScriptableObject.CreateInstance<T>());
				}
				t = serializableJsonDictionary.GetScriptable<T>(text);
			}
			return t;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0000DD3C File Offset: 0x0000BF3C
		internal void OverwriteFromViewData(object obj, string key)
		{
			bool flag = obj == null;
			if (flag)
			{
				throw new ArgumentNullException("obj");
			}
			Debug.Assert(this.elementPanel != null, "VisualElement.elementPanel is null! Cannot load view data.");
			ISerializableJsonDictionary serializableJsonDictionary = ((this.elementPanel == null || this.elementPanel.getViewDataDictionary == null) ? null : this.elementPanel.getViewDataDictionary());
			bool flag2 = serializableJsonDictionary == null || string.IsNullOrEmpty(this.viewDataKey) || !this.enableViewDataPersistence;
			if (!flag2)
			{
				string text = key + "__" + obj.GetType();
				bool flag3 = !serializableJsonDictionary.ContainsKey(text);
				if (flag3)
				{
					serializableJsonDictionary.Set<object>(text, obj);
				}
				else
				{
					serializableJsonDictionary.Overwrite(obj, text);
				}
			}
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x0000DDF8 File Offset: 0x0000BFF8
		internal void SaveViewData()
		{
			bool flag = this.elementPanel != null && this.elementPanel.saveViewData != null && !string.IsNullOrEmpty(this.viewDataKey) && this.enableViewDataPersistence;
			if (flag)
			{
				this.elementPanel.saveViewData();
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x0000DE48 File Offset: 0x0000C048
		internal bool IsViewDataPersitenceSupportedOnChildren(bool existingState)
		{
			bool flag = existingState;
			bool flag2 = string.IsNullOrEmpty(this.viewDataKey) && this != this.contentContainer;
			if (flag2)
			{
				flag = false;
			}
			bool flag3 = this.parent != null && this == this.parent.contentContainer;
			if (flag3)
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000DE9E File Offset: 0x0000C09E
		internal void OnViewDataReady(bool enablePersistence)
		{
			this.enableViewDataPersistence = enablePersistence;
			this.OnViewDataReady();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000062F3 File Offset: 0x000044F3
		internal virtual void OnViewDataReady()
		{
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000DEB0 File Offset: 0x0000C0B0
		public virtual bool ContainsPoint(Vector2 localPoint)
		{
			return this.rect.Contains(localPoint);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000DED4 File Offset: 0x0000C0D4
		public virtual bool Overlaps(Rect rectangle)
		{
			return this.rect.Overlaps(rectangle, true);
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000DEF8 File Offset: 0x0000C0F8
		// (set) Token: 0x060003AF RID: 943 RVA: 0x0000DF10 File Offset: 0x0000C110
		internal bool requireMeasureFunction
		{
			get
			{
				return this.m_RequireMeasureFunction;
			}
			set
			{
				this.m_RequireMeasureFunction = value;
				bool flag = this.m_RequireMeasureFunction && !this.yogaNode.IsMeasureDefined;
				if (flag)
				{
					this.AssignMeasureFunction();
				}
				else
				{
					bool flag2 = !this.m_RequireMeasureFunction && this.yogaNode.IsMeasureDefined;
					if (flag2)
					{
						this.RemoveMeasureFunction();
					}
				}
			}
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000DF70 File Offset: 0x0000C170
		private void AssignMeasureFunction()
		{
			this.yogaNode.SetMeasureFunction((YogaNode node, float f, YogaMeasureMode mode, float f1, YogaMeasureMode heightMode) => this.Measure(node, f, mode, f1, heightMode));
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000DF8B File Offset: 0x0000C18B
		private void RemoveMeasureFunction()
		{
			this.yogaNode.SetMeasureFunction(null);
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000DF9C File Offset: 0x0000C19C
		protected internal virtual Vector2 DoMeasure(float desiredWidth, VisualElement.MeasureMode widthMode, float desiredHeight, VisualElement.MeasureMode heightMode)
		{
			return new Vector2(float.NaN, float.NaN);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000DFC0 File Offset: 0x0000C1C0
		internal YogaSize Measure(YogaNode node, float width, YogaMeasureMode widthMode, float height, YogaMeasureMode heightMode)
		{
			Debug.Assert(node == this.yogaNode, "YogaNode instance mismatch");
			Vector2 vector = this.DoMeasure(width, (VisualElement.MeasureMode)widthMode, height, (VisualElement.MeasureMode)heightMode);
			return MeasureOutput.Make((float)Mathf.RoundToInt(vector.x), (float)Mathf.RoundToInt(vector.y));
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000E010 File Offset: 0x0000C210
		internal void SetSize(Vector2 size)
		{
			Rect layout = this.layout;
			layout.width = size.x;
			layout.height = size.y;
			this.layout = layout;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000E04C File Offset: 0x0000C24C
		private void FinalizeLayout()
		{
			bool hasInlineStyle = this.hasInlineStyle;
			if (hasInlineStyle)
			{
				this.computedStyle.SyncWithLayout(this.yogaNode);
			}
			else
			{
				this.yogaNode.CopyStyle(this.computedStyle.yogaNode);
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000E094 File Offset: 0x0000C294
		internal void SetInlineRule(StyleSheet sheet, StyleRule rule)
		{
			bool flag = this.inlineStyleAccess == null;
			if (flag)
			{
				this.inlineStyleAccess = new InlineStyleAccess(this);
			}
			this.inlineStyleAccess.SetInlineRule(sheet, rule);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000E0CC File Offset: 0x0000C2CC
		internal void SetSharedStyles(ComputedStyle sharedStyle)
		{
			Debug.Assert(sharedStyle.isShared);
			bool flag = sharedStyle == this.m_SharedStyle;
			if (!flag)
			{
				StyleEnum<OverflowInternal> overflow = this.m_Style.overflow;
				StyleLength borderBottomLeftRadius = this.m_Style.borderBottomLeftRadius;
				StyleLength borderBottomRightRadius = this.m_Style.borderBottomRightRadius;
				StyleLength borderTopLeftRadius = this.m_Style.borderTopLeftRadius;
				StyleLength borderTopRightRadius = this.m_Style.borderTopRightRadius;
				StyleFloat borderLeftWidth = this.m_Style.borderLeftWidth;
				StyleFloat borderTopWidth = this.m_Style.borderTopWidth;
				StyleFloat borderRightWidth = this.m_Style.borderRightWidth;
				StyleFloat borderBottomWidth = this.m_Style.borderBottomWidth;
				StyleFloat opacity = this.m_Style.opacity;
				bool hasInlineStyle = this.hasInlineStyle;
				if (hasInlineStyle)
				{
					this.inlineStyleAccess.ApplyInlineStyles(sharedStyle);
				}
				else
				{
					this.m_Style = sharedStyle;
				}
				this.m_SharedStyle = sharedStyle;
				this.FinalizeLayout();
				VersionChangeType versionChangeType = VersionChangeType.Layout | VersionChangeType.Styles | VersionChangeType.Repaint;
				bool flag2 = this.m_Style.overflow != overflow;
				if (flag2)
				{
					versionChangeType |= VersionChangeType.Overflow;
				}
				bool flag3 = borderBottomLeftRadius != this.m_Style.borderBottomLeftRadius || borderBottomRightRadius != this.m_Style.borderBottomRightRadius || borderTopLeftRadius != this.m_Style.borderTopLeftRadius || borderTopRightRadius != this.m_Style.borderTopRightRadius;
				if (flag3)
				{
					versionChangeType |= VersionChangeType.BorderRadius;
				}
				bool flag4 = borderLeftWidth != this.m_Style.borderLeftWidth || borderTopWidth != this.m_Style.borderTopWidth || borderRightWidth != this.m_Style.borderRightWidth || borderBottomWidth != this.m_Style.borderBottomWidth;
				if (flag4)
				{
					versionChangeType |= VersionChangeType.BorderWidth;
				}
				bool flag5 = this.m_Style.opacity != opacity;
				if (flag5)
				{
					versionChangeType |= VersionChangeType.Opacity;
				}
				this.IncrementVersion(versionChangeType);
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000E2C0 File Offset: 0x0000C4C0
		internal void ResetPositionProperties()
		{
			bool flag = !this.hasInlineStyle;
			if (!flag)
			{
				this.style.position = StyleKeyword.Null;
				this.style.marginLeft = StyleKeyword.Null;
				this.style.marginRight = StyleKeyword.Null;
				this.style.marginBottom = StyleKeyword.Null;
				this.style.marginTop = StyleKeyword.Null;
				this.style.left = StyleKeyword.Null;
				this.style.top = StyleKeyword.Null;
				this.style.right = StyleKeyword.Null;
				this.style.bottom = StyleKeyword.Null;
				this.style.width = StyleKeyword.Null;
				this.style.height = StyleKeyword.Null;
				this.FinalizeLayout();
				this.IncrementVersion(VersionChangeType.Layout);
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.GetType().Name,
				" ",
				this.name,
				" ",
				this.layout,
				" world rect: ",
				this.worldBound
			});
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000E420 File Offset: 0x0000C620
		public IEnumerable<string> GetClasses()
		{
			return this.m_ClassList;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000E438 File Offset: 0x0000C638
		public void ClearClassList()
		{
			bool flag = this.m_ClassList.Count > 0;
			if (flag)
			{
				this.m_ClassList = VisualElement.s_EmptyClassList;
				this.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000E470 File Offset: 0x0000C670
		public void AddToClassList(string className)
		{
			bool flag = this.m_ClassList == VisualElement.s_EmptyClassList;
			if (flag)
			{
				List<string> list = new List<string>();
				list.Add(className);
				this.m_ClassList = list;
			}
			else
			{
				bool flag2 = this.m_ClassList.Contains(className);
				if (flag2)
				{
					return;
				}
				bool flag3 = this.m_ClassList.Capacity == this.m_ClassList.Count;
				if (flag3)
				{
					this.m_ClassList.Capacity++;
				}
				this.m_ClassList.Add(className);
			}
			this.IncrementVersion(VersionChangeType.StyleSheet);
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000E504 File Offset: 0x0000C704
		public void RemoveFromClassList(string className)
		{
			bool flag = this.m_ClassList.Remove(className);
			if (flag)
			{
				this.IncrementVersion(VersionChangeType.StyleSheet);
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000E530 File Offset: 0x0000C730
		public void ToggleInClassList(string className)
		{
			bool flag = this.ClassListContains(className);
			if (flag)
			{
				this.RemoveFromClassList(className);
			}
			else
			{
				this.AddToClassList(className);
			}
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000E55C File Offset: 0x0000C75C
		public void EnableInClassList(string className, bool enable)
		{
			if (enable)
			{
				this.AddToClassList(className);
			}
			else
			{
				this.RemoveFromClassList(className);
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000E584 File Offset: 0x0000C784
		public bool ClassListContains(string cls)
		{
			for (int i = 0; i < this.m_ClassList.Count; i++)
			{
				bool flag = this.m_ClassList[i] == cls;
				if (flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000E5D0 File Offset: 0x0000C7D0
		public object FindAncestorUserData()
		{
			for (VisualElement visualElement = this.parent; visualElement != null; visualElement = visualElement.parent)
			{
				bool flag = visualElement.userData != null;
				if (flag)
				{
					return visualElement.userData;
				}
			}
			return null;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000E614 File Offset: 0x0000C814
		internal object GetProperty(PropertyName key)
		{
			VisualElement.CheckUserKeyArgument(key);
			return this.GetPropertyInternal(key);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000E634 File Offset: 0x0000C834
		internal void SetProperty(PropertyName key, object value)
		{
			VisualElement.CheckUserKeyArgument(key);
			this.SetPropertyInternal(key, value);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000E648 File Offset: 0x0000C848
		private object GetPropertyInternal(PropertyName key)
		{
			bool flag = this.m_PropertyBag != null;
			if (flag)
			{
				for (int i = 0; i < this.m_PropertyBag.Count; i++)
				{
					bool flag2 = this.m_PropertyBag[i].Key == key;
					if (flag2)
					{
						return this.m_PropertyBag[i].Value;
					}
				}
			}
			return null;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E6C4 File Offset: 0x0000C8C4
		private static void CheckUserKeyArgument(PropertyName key)
		{
			bool flag = PropertyName.IsNullOrEmpty(key);
			if (flag)
			{
				throw new ArgumentNullException("key");
			}
			bool flag2 = key == VisualElement.userDataPropertyKey;
			if (flag2)
			{
				throw new InvalidOperationException(string.Format("The {0} key is reserved by the system", VisualElement.userDataPropertyKey));
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000E710 File Offset: 0x0000C910
		private void SetPropertyInternal(PropertyName key, object value)
		{
			KeyValuePair<PropertyName, object> keyValuePair = new KeyValuePair<PropertyName, object>(key, value);
			bool flag = this.m_PropertyBag == null;
			if (flag)
			{
				this.m_PropertyBag = new List<KeyValuePair<PropertyName, object>>(1);
				this.m_PropertyBag.Add(keyValuePair);
			}
			else
			{
				for (int i = 0; i < this.m_PropertyBag.Count; i++)
				{
					bool flag2 = this.m_PropertyBag[i].Key == key;
					if (flag2)
					{
						this.m_PropertyBag[i] = keyValuePair;
						return;
					}
				}
				bool flag3 = this.m_PropertyBag.Capacity == this.m_PropertyBag.Count;
				if (flag3)
				{
					this.m_PropertyBag.Capacity++;
				}
				this.m_PropertyBag.Add(keyValuePair);
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000E7E8 File Offset: 0x0000C9E8
		private void UpdateCursorStyle(long eventType)
		{
			bool flag = this.elementPanel != null;
			if (flag)
			{
				bool flag2 = eventType == EventBase<MouseOverEvent>.TypeId() && this.elementPanel.GetTopElementUnderPointer(PointerId.mousePointerId) == this;
				if (flag2)
				{
					this.elementPanel.cursorManager.SetCursor(this.computedStyle.cursor.value);
				}
				else
				{
					bool flag3 = eventType == EventBase<MouseOutEvent>.TypeId();
					if (flag3)
					{
						this.elementPanel.cursorManager.ResetCursor();
					}
				}
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000E870 File Offset: 0x0000CA70
		private VisualElementAnimationSystem GetAnimationSystem()
		{
			bool flag = this.elementPanel != null;
			VisualElementAnimationSystem visualElementAnimationSystem;
			if (flag)
			{
				visualElementAnimationSystem = this.elementPanel.GetUpdater(VisualTreeUpdatePhase.Animation) as VisualElementAnimationSystem;
			}
			else
			{
				visualElementAnimationSystem = null;
			}
			return visualElementAnimationSystem;
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000E8A8 File Offset: 0x0000CAA8
		internal void RegisterAnimation(IValueAnimationUpdate anim)
		{
			bool flag = this.m_RunningAnimations == null;
			if (flag)
			{
				this.m_RunningAnimations = new List<IValueAnimationUpdate>();
			}
			this.m_RunningAnimations.Add(anim);
			VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
			bool flag2 = animationSystem != null;
			if (flag2)
			{
				animationSystem.RegisterAnimation(anim);
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000E8F8 File Offset: 0x0000CAF8
		internal void UnregisterAnimation(IValueAnimationUpdate anim)
		{
			bool flag = this.m_RunningAnimations != null;
			if (flag)
			{
				this.m_RunningAnimations.Remove(anim);
			}
			VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
			bool flag2 = animationSystem != null;
			if (flag2)
			{
				animationSystem.UnregisterAnimation(anim);
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000E93C File Offset: 0x0000CB3C
		private void UnregisterRunningAnimations()
		{
			bool flag = this.m_RunningAnimations != null && this.m_RunningAnimations.Count > 0;
			if (flag)
			{
				VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
				bool flag2 = animationSystem != null;
				if (flag2)
				{
					animationSystem.UnregisterAnimations(this.m_RunningAnimations);
				}
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000E988 File Offset: 0x0000CB88
		private void RegisterRunningAnimations()
		{
			bool flag = this.m_RunningAnimations != null && this.m_RunningAnimations.Count > 0;
			if (flag)
			{
				VisualElementAnimationSystem animationSystem = this.GetAnimationSystem();
				bool flag2 = animationSystem != null;
				if (flag2)
				{
					animationSystem.RegisterAnimations(this.m_RunningAnimations);
				}
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000E9D4 File Offset: 0x0000CBD4
		ValueAnimation<float> ITransitionAnimations.Start(float from, float to, int durationMs, Action<VisualElement, float> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000EA14 File Offset: 0x0000CC14
		ValueAnimation<Rect> ITransitionAnimations.Start(Rect from, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000EA54 File Offset: 0x0000CC54
		ValueAnimation<Color> ITransitionAnimations.Start(Color from, Color to, int durationMs, Action<VisualElement, Color> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000EA94 File Offset: 0x0000CC94
		ValueAnimation<Vector3> ITransitionAnimations.Start(Vector3 from, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000EAD4 File Offset: 0x0000CCD4
		ValueAnimation<Vector2> ITransitionAnimations.Start(Vector2 from, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000EB14 File Offset: 0x0000CD14
		ValueAnimation<Quaternion> ITransitionAnimations.Start(Quaternion from, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged)
		{
			return this.experimental.animation.Start((VisualElement e) => from, to, durationMs, onValueChanged);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000EB54 File Offset: 0x0000CD54
		ValueAnimation<StyleValues> ITransitionAnimations.Start(StyleValues from, StyleValues to, int durationMs)
		{
			return this.Start((VisualElement e) => from, to, durationMs);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000EB88 File Offset: 0x0000CD88
		ValueAnimation<float> ITransitionAnimations.Start(Func<VisualElement, float> fromValueGetter, float to, int durationMs, Action<VisualElement, float> onValueChanged)
		{
			return VisualElement.StartAnimation<float>(ValueAnimation<float>.Create(this, new Func<float, float, float, float>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000EBB8 File Offset: 0x0000CDB8
		ValueAnimation<Rect> ITransitionAnimations.Start(Func<VisualElement, Rect> fromValueGetter, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged)
		{
			return VisualElement.StartAnimation<Rect>(ValueAnimation<Rect>.Create(this, new Func<Rect, Rect, float, Rect>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		ValueAnimation<Color> ITransitionAnimations.Start(Func<VisualElement, Color> fromValueGetter, Color to, int durationMs, Action<VisualElement, Color> onValueChanged)
		{
			return VisualElement.StartAnimation<Color>(ValueAnimation<Color>.Create(this, new Func<Color, Color, float, Color>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000EC18 File Offset: 0x0000CE18
		ValueAnimation<Vector3> ITransitionAnimations.Start(Func<VisualElement, Vector3> fromValueGetter, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged)
		{
			return VisualElement.StartAnimation<Vector3>(ValueAnimation<Vector3>.Create(this, new Func<Vector3, Vector3, float, Vector3>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000EC48 File Offset: 0x0000CE48
		ValueAnimation<Vector2> ITransitionAnimations.Start(Func<VisualElement, Vector2> fromValueGetter, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged)
		{
			return VisualElement.StartAnimation<Vector2>(ValueAnimation<Vector2>.Create(this, new Func<Vector2, Vector2, float, Vector2>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000EC78 File Offset: 0x0000CE78
		ValueAnimation<Quaternion> ITransitionAnimations.Start(Func<VisualElement, Quaternion> fromValueGetter, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged)
		{
			return VisualElement.StartAnimation<Quaternion>(ValueAnimation<Quaternion>.Create(this, new Func<Quaternion, Quaternion, float, Quaternion>(Lerp.Interpolate)), fromValueGetter, to, durationMs, onValueChanged);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x0000ECA8 File Offset: 0x0000CEA8
		private static ValueAnimation<T> StartAnimation<T>(ValueAnimation<T> anim, Func<VisualElement, T> fromValueGetter, T to, int durationMs, Action<VisualElement, T> onValueChanged)
		{
			anim.initialValue = fromValueGetter;
			anim.to = to;
			anim.durationMs = durationMs;
			anim.valueUpdated = onValueChanged;
			anim.Start();
			return anim;
		}

		// Token: 0x060003DB RID: 987 RVA: 0x0000ECE4 File Offset: 0x0000CEE4
		private static void AssignStyleValues(VisualElement ve, StyleValues src)
		{
			IStyle style = ve.style;
			foreach (StyleValue styleValue in src.m_StyleValues.m_Values)
			{
				StylePropertyId id = styleValue.id;
				switch (id)
				{
				case StylePropertyId.Unknown:
					break;
				case StylePropertyId.Color:
					style.color = styleValue.color;
					break;
				case StylePropertyId.FontSize:
					style.fontSize = styleValue.number;
					break;
				default:
					switch (id)
					{
					case StylePropertyId.BackgroundColor:
						style.backgroundColor = styleValue.color;
						break;
					case StylePropertyId.BackgroundImage:
					case StylePropertyId.BorderBottomColor:
					case StylePropertyId.BorderLeftColor:
					case StylePropertyId.BorderRightColor:
					case StylePropertyId.BorderTopColor:
					case StylePropertyId.Cursor:
					case StylePropertyId.Display:
					case StylePropertyId.FlexBasis:
					case StylePropertyId.FlexDirection:
					case StylePropertyId.FlexWrap:
					case StylePropertyId.JustifyContent:
					case StylePropertyId.MaxHeight:
					case StylePropertyId.MaxWidth:
					case StylePropertyId.MinHeight:
					case StylePropertyId.MinWidth:
					case StylePropertyId.Overflow:
					case StylePropertyId.Position:
					case StylePropertyId.TextOverflow:
					case StylePropertyId.UnityBackgroundScaleMode:
					case StylePropertyId.UnityOverflowClipBox:
					case StylePropertyId.UnitySliceBottom:
					case StylePropertyId.UnitySliceLeft:
					case StylePropertyId.UnitySliceRight:
					case StylePropertyId.UnitySliceTop:
					case StylePropertyId.UnityTextOverflowPosition:
						break;
					case StylePropertyId.BorderBottomLeftRadius:
						style.borderBottomLeftRadius = styleValue.number;
						break;
					case StylePropertyId.BorderBottomRightRadius:
						style.borderBottomRightRadius = styleValue.number;
						break;
					case StylePropertyId.BorderBottomWidth:
						style.borderBottomWidth = styleValue.number;
						break;
					case StylePropertyId.BorderLeftWidth:
						style.borderLeftWidth = styleValue.number;
						break;
					case StylePropertyId.BorderRightWidth:
						style.borderRightWidth = styleValue.number;
						break;
					case StylePropertyId.BorderTopLeftRadius:
						style.borderTopLeftRadius = styleValue.number;
						break;
					case StylePropertyId.BorderTopRightRadius:
						style.borderTopRightRadius = styleValue.number;
						break;
					case StylePropertyId.BorderTopWidth:
						style.borderTopWidth = styleValue.number;
						break;
					case StylePropertyId.Bottom:
						style.bottom = styleValue.number;
						break;
					case StylePropertyId.FlexGrow:
						style.flexGrow = styleValue.number;
						break;
					case StylePropertyId.FlexShrink:
						style.flexShrink = styleValue.number;
						break;
					case StylePropertyId.Height:
						style.height = styleValue.number;
						break;
					case StylePropertyId.Left:
						style.left = styleValue.number;
						break;
					case StylePropertyId.MarginBottom:
						style.marginBottom = styleValue.number;
						break;
					case StylePropertyId.MarginLeft:
						style.marginLeft = styleValue.number;
						break;
					case StylePropertyId.MarginRight:
						style.marginRight = styleValue.number;
						break;
					case StylePropertyId.MarginTop:
						style.marginTop = styleValue.number;
						break;
					case StylePropertyId.Opacity:
						style.opacity = styleValue.number;
						break;
					case StylePropertyId.PaddingBottom:
						style.paddingBottom = styleValue.number;
						break;
					case StylePropertyId.PaddingLeft:
						style.paddingLeft = styleValue.number;
						break;
					case StylePropertyId.PaddingRight:
						style.paddingRight = styleValue.number;
						break;
					case StylePropertyId.PaddingTop:
						style.paddingTop = styleValue.number;
						break;
					case StylePropertyId.Right:
						style.right = styleValue.number;
						break;
					case StylePropertyId.Top:
						style.top = styleValue.number;
						break;
					case StylePropertyId.UnityBackgroundImageTintColor:
						style.unityBackgroundImageTintColor = styleValue.color;
						break;
					case StylePropertyId.Width:
						style.width = styleValue.number;
						break;
					default:
						if (id == StylePropertyId.BorderColor)
						{
							style.borderLeftColor = styleValue.color;
							style.borderTopColor = styleValue.color;
							style.borderRightColor = styleValue.color;
							style.borderBottomColor = styleValue.color;
						}
						break;
					}
					break;
				}
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x0000F144 File Offset: 0x0000D344
		private StyleValues ReadCurrentValues(VisualElement ve, StyleValues targetValuesToRead)
		{
			StyleValues styleValues = default(StyleValues);
			IResolvedStyle resolvedStyle = ve.resolvedStyle;
			foreach (StyleValue styleValue in targetValuesToRead.m_StyleValues.m_Values)
			{
				StylePropertyId id = styleValue.id;
				if (id <= StylePropertyId.Color)
				{
					if (id != StylePropertyId.Unknown)
					{
						if (id == StylePropertyId.Color)
						{
							styleValues.color = resolvedStyle.color;
						}
					}
				}
				else
				{
					switch (id)
					{
					case StylePropertyId.BackgroundColor:
						styleValues.backgroundColor = resolvedStyle.backgroundColor;
						break;
					case StylePropertyId.BackgroundImage:
					case StylePropertyId.BorderBottomColor:
					case StylePropertyId.BorderLeftColor:
					case StylePropertyId.BorderRightColor:
					case StylePropertyId.BorderTopColor:
					case StylePropertyId.Cursor:
					case StylePropertyId.Display:
					case StylePropertyId.FlexBasis:
					case StylePropertyId.FlexDirection:
					case StylePropertyId.FlexWrap:
					case StylePropertyId.JustifyContent:
					case StylePropertyId.MaxHeight:
					case StylePropertyId.MaxWidth:
					case StylePropertyId.MinHeight:
					case StylePropertyId.MinWidth:
					case StylePropertyId.Overflow:
					case StylePropertyId.Position:
					case StylePropertyId.TextOverflow:
					case StylePropertyId.UnityBackgroundScaleMode:
					case StylePropertyId.UnityOverflowClipBox:
					case StylePropertyId.UnitySliceBottom:
					case StylePropertyId.UnitySliceLeft:
					case StylePropertyId.UnitySliceRight:
					case StylePropertyId.UnitySliceTop:
					case StylePropertyId.UnityTextOverflowPosition:
						break;
					case StylePropertyId.BorderBottomLeftRadius:
						styleValues.borderBottomLeftRadius = resolvedStyle.borderBottomLeftRadius;
						break;
					case StylePropertyId.BorderBottomRightRadius:
						styleValues.borderBottomRightRadius = resolvedStyle.borderBottomRightRadius;
						break;
					case StylePropertyId.BorderBottomWidth:
						styleValues.borderBottomWidth = resolvedStyle.borderBottomWidth;
						break;
					case StylePropertyId.BorderLeftWidth:
						styleValues.borderLeftWidth = resolvedStyle.borderLeftWidth;
						break;
					case StylePropertyId.BorderRightWidth:
						styleValues.borderRightWidth = resolvedStyle.borderRightWidth;
						break;
					case StylePropertyId.BorderTopLeftRadius:
						styleValues.borderTopLeftRadius = resolvedStyle.borderTopLeftRadius;
						break;
					case StylePropertyId.BorderTopRightRadius:
						styleValues.borderTopRightRadius = resolvedStyle.borderTopRightRadius;
						break;
					case StylePropertyId.BorderTopWidth:
						styleValues.borderTopWidth = resolvedStyle.borderTopWidth;
						break;
					case StylePropertyId.Bottom:
						styleValues.bottom = resolvedStyle.bottom;
						break;
					case StylePropertyId.FlexGrow:
						styleValues.flexGrow = resolvedStyle.flexGrow;
						break;
					case StylePropertyId.FlexShrink:
						styleValues.flexShrink = resolvedStyle.flexShrink;
						break;
					case StylePropertyId.Height:
						styleValues.height = resolvedStyle.height;
						break;
					case StylePropertyId.Left:
						styleValues.left = resolvedStyle.left;
						break;
					case StylePropertyId.MarginBottom:
						styleValues.marginBottom = resolvedStyle.marginBottom;
						break;
					case StylePropertyId.MarginLeft:
						styleValues.marginLeft = resolvedStyle.marginLeft;
						break;
					case StylePropertyId.MarginRight:
						styleValues.marginRight = resolvedStyle.marginRight;
						break;
					case StylePropertyId.MarginTop:
						styleValues.marginTop = resolvedStyle.marginTop;
						break;
					case StylePropertyId.Opacity:
						styleValues.opacity = resolvedStyle.opacity;
						break;
					case StylePropertyId.PaddingBottom:
						styleValues.paddingBottom = resolvedStyle.paddingBottom;
						break;
					case StylePropertyId.PaddingLeft:
						styleValues.paddingLeft = resolvedStyle.paddingLeft;
						break;
					case StylePropertyId.PaddingRight:
						styleValues.paddingRight = resolvedStyle.paddingRight;
						break;
					case StylePropertyId.PaddingTop:
						styleValues.paddingTop = resolvedStyle.paddingTop;
						break;
					case StylePropertyId.Right:
						styleValues.right = resolvedStyle.right;
						break;
					case StylePropertyId.Top:
						styleValues.top = resolvedStyle.top;
						break;
					case StylePropertyId.UnityBackgroundImageTintColor:
						styleValues.unityBackgroundImageTintColor = resolvedStyle.unityBackgroundImageTintColor;
						break;
					case StylePropertyId.Width:
						styleValues.width = resolvedStyle.width;
						break;
					default:
						if (id == StylePropertyId.BorderColor)
						{
							styleValues.borderColor = resolvedStyle.borderLeftColor;
						}
						break;
					}
				}
			}
			return styleValues;
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000F4EC File Offset: 0x0000D6EC
		ValueAnimation<StyleValues> ITransitionAnimations.Start(StyleValues to, int durationMs)
		{
			return this.Start((VisualElement e) => this.ReadCurrentValues(e, to), to, durationMs);
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000F52C File Offset: 0x0000D72C
		private ValueAnimation<StyleValues> Start(Func<VisualElement, StyleValues> fromValueGetter, StyleValues to, int durationMs)
		{
			return VisualElement.StartAnimation<StyleValues>(ValueAnimation<StyleValues>.Create(this, new Func<StyleValues, StyleValues, float, StyleValues>(Lerp.Interpolate)), fromValueGetter, to, durationMs, new Action<VisualElement, StyleValues>(VisualElement.AssignStyleValues));
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000F564 File Offset: 0x0000D764
		ValueAnimation<Rect> ITransitionAnimations.Layout(Rect to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => new Rect(e.resolvedStyle.left, e.resolvedStyle.top, e.resolvedStyle.width, e.resolvedStyle.height), to, durationMs, delegate(VisualElement e, Rect c)
			{
				e.style.left = c.x;
				e.style.top = c.y;
				e.style.width = c.width;
				e.style.height = c.height;
			});
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000F5C8 File Offset: 0x0000D7C8
		ValueAnimation<Vector2> ITransitionAnimations.TopLeft(Vector2 to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => new Vector2(e.resolvedStyle.left, e.resolvedStyle.top), to, durationMs, delegate(VisualElement e, Vector2 c)
			{
				e.style.left = c.x;
				e.style.top = c.y;
			});
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000F62C File Offset: 0x0000D82C
		ValueAnimation<Vector2> ITransitionAnimations.Size(Vector2 to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.layout.size, to, durationMs, delegate(VisualElement e, Vector2 c)
			{
				e.style.width = c.x;
				e.style.height = c.y;
			});
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x0000F690 File Offset: 0x0000D890
		ValueAnimation<float> ITransitionAnimations.Scale(float to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.transform.scale.x, to, durationMs, delegate(VisualElement e, float c)
			{
				e.transform.scale = new Vector3(c, c, c);
			});
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000F6F4 File Offset: 0x0000D8F4
		ValueAnimation<Vector3> ITransitionAnimations.Position(Vector3 to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.transform.position, to, durationMs, delegate(VisualElement e, Vector3 c)
			{
				e.transform.position = c;
			});
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x0000F758 File Offset: 0x0000D958
		ValueAnimation<Quaternion> ITransitionAnimations.Rotation(Quaternion to, int durationMs)
		{
			return this.experimental.animation.Start((VisualElement e) => e.transform.rotation, to, durationMs, delegate(VisualElement e, Quaternion c)
			{
				e.transform.rotation = c;
			});
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000F7BC File Offset: 0x0000D9BC
		public IExperimentalFeatures experimental
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000F7D0 File Offset: 0x0000D9D0
		ITransitionAnimations IExperimentalFeatures.animation
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000F7E3 File Offset: 0x0000D9E3
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000F7EB File Offset: 0x0000D9EB
		public VisualElement.Hierarchy hierarchy { get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000F7F4 File Offset: 0x0000D9F4
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000F7FC File Offset: 0x0000D9FC
		[Obsolete("VisualElement.cacheAsBitmap is deprecated and has no effect")]
		public bool cacheAsBitmap { get; set; }

		// Token: 0x060003EB RID: 1003 RVA: 0x0000F808 File Offset: 0x0000DA08
		internal bool ShouldClip()
		{
			return this.computedStyle.overflow.value > OverflowInternal.Visible;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000F830 File Offset: 0x0000DA30
		public VisualElement parent
		{
			get
			{
				return this.m_LogicalParent;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000F848 File Offset: 0x0000DA48
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000F850 File Offset: 0x0000DA50
		internal BaseVisualElementPanel elementPanel { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000F85C File Offset: 0x0000DA5C
		public IPanel panel
		{
			get
			{
				return this.elementPanel;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000F874 File Offset: 0x0000DA74
		public virtual VisualElement contentContainer
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000F888 File Offset: 0x0000DA88
		public void Add(VisualElement child)
		{
			bool flag = child == null;
			if (!flag)
			{
				bool flag2 = this.contentContainer == this;
				if (flag2)
				{
					this.hierarchy.Add(child);
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					if (contentContainer != null)
					{
						contentContainer.Add(child);
					}
				}
				child.m_LogicalParent = this;
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x0000F8E0 File Offset: 0x0000DAE0
		public void Insert(int index, VisualElement element)
		{
			bool flag = element == null;
			if (!flag)
			{
				bool flag2 = this.contentContainer == this;
				if (flag2)
				{
					this.hierarchy.Insert(index, element);
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					if (contentContainer != null)
					{
						contentContainer.Insert(index, element);
					}
				}
				element.m_LogicalParent = this;
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x0000F938 File Offset: 0x0000DB38
		public void Remove(VisualElement element)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Remove(element);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.Remove(element);
				}
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000F97C File Offset: 0x0000DB7C
		public void RemoveAt(int index)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.RemoveAt(index);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.RemoveAt(index);
				}
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		public void Clear()
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Clear();
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.Clear();
				}
			}
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0000FA04 File Offset: 0x0000DC04
		public VisualElement ElementAt(int index)
		{
			return this[index];
		}

		// Token: 0x170000D0 RID: 208
		public VisualElement this[int key]
		{
			get
			{
				bool flag = this.contentContainer == this;
				VisualElement visualElement;
				if (flag)
				{
					visualElement = this.hierarchy[key];
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					visualElement = ((contentContainer != null) ? contentContainer[key] : null);
				}
				return visualElement;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000FA68 File Offset: 0x0000DC68
		public int childCount
		{
			get
			{
				bool flag = this.contentContainer == this;
				int num;
				if (flag)
				{
					num = this.hierarchy.childCount;
				}
				else
				{
					VisualElement contentContainer = this.contentContainer;
					num = ((contentContainer != null) ? contentContainer.childCount : 0);
				}
				return num;
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0000FAAC File Offset: 0x0000DCAC
		public int IndexOf(VisualElement element)
		{
			bool flag = this.contentContainer == this;
			int num;
			if (flag)
			{
				num = this.hierarchy.IndexOf(element);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				num = ((contentContainer != null) ? contentContainer.IndexOf(element) : (-1));
			}
			return num;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0000FAF4 File Offset: 0x0000DCF4
		public IEnumerable<VisualElement> Children()
		{
			bool flag = this.contentContainer == this;
			IEnumerable<VisualElement> enumerable;
			if (flag)
			{
				enumerable = this.hierarchy.Children();
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				enumerable = ((contentContainer != null) ? contentContainer.Children() : null);
			}
			return enumerable;
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x0000FB38 File Offset: 0x0000DD38
		public void Sort(Comparison<VisualElement> comp)
		{
			bool flag = this.contentContainer == this;
			if (flag)
			{
				this.hierarchy.Sort(comp);
			}
			else
			{
				VisualElement contentContainer = this.contentContainer;
				if (contentContainer != null)
				{
					contentContainer.Sort(comp);
				}
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0000FB7C File Offset: 0x0000DD7C
		public void BringToFront()
		{
			bool flag = this.hierarchy.parent == null;
			if (!flag)
			{
				this.hierarchy.parent.hierarchy.BringToFront(this);
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0000FBC0 File Offset: 0x0000DDC0
		public void SendToBack()
		{
			bool flag = this.hierarchy.parent == null;
			if (!flag)
			{
				this.hierarchy.parent.hierarchy.SendToBack(this);
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0000FC04 File Offset: 0x0000DE04
		public void PlaceBehind(VisualElement sibling)
		{
			bool flag = sibling == null;
			if (flag)
			{
				throw new ArgumentNullException("sibling");
			}
			bool flag2 = this.hierarchy.parent == null || sibling.hierarchy.parent != this.hierarchy.parent;
			if (flag2)
			{
				throw new ArgumentException("VisualElements are not siblings");
			}
			this.hierarchy.parent.hierarchy.PlaceBehind(this, sibling);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0000FC88 File Offset: 0x0000DE88
		public void PlaceInFront(VisualElement sibling)
		{
			bool flag = sibling == null;
			if (flag)
			{
				throw new ArgumentNullException("sibling");
			}
			bool flag2 = this.hierarchy.parent == null || sibling.hierarchy.parent != this.hierarchy.parent;
			if (flag2)
			{
				throw new ArgumentException("VisualElements are not siblings");
			}
			this.hierarchy.parent.hierarchy.PlaceInFront(this, sibling);
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x0000FD0C File Offset: 0x0000DF0C
		public void RemoveFromHierarchy()
		{
			bool flag = this.hierarchy.parent != null;
			if (flag)
			{
				this.hierarchy.parent.hierarchy.Remove(this);
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x0000FD50 File Offset: 0x0000DF50
		public T GetFirstOfType<T>() where T : class
		{
			T t = this as T;
			bool flag = t != null;
			T t2;
			if (flag)
			{
				t2 = t;
			}
			else
			{
				t2 = this.GetFirstAncestorOfType<T>();
			}
			return t2;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x0000FD88 File Offset: 0x0000DF88
		public T GetFirstAncestorOfType<T>() where T : class
		{
			for (VisualElement visualElement = this.hierarchy.parent; visualElement != null; visualElement = visualElement.hierarchy.parent)
			{
				T t = visualElement as T;
				bool flag = t != null;
				if (flag)
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0000FDF4 File Offset: 0x0000DFF4
		public bool Contains(VisualElement child)
		{
			while (child != null)
			{
				bool flag = child.hierarchy.parent == this;
				if (flag)
				{
					return true;
				}
				child = child.hierarchy.parent;
			}
			return false;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000FE40 File Offset: 0x0000E040
		private void GatherAllChildren(List<VisualElement> elements)
		{
			bool flag = this.m_Children.Count > 0;
			if (flag)
			{
				int i = elements.Count;
				elements.AddRange(this.m_Children);
				while (i < elements.Count)
				{
					VisualElement visualElement = elements[i];
					elements.AddRange(visualElement.m_Children);
					i++;
				}
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		public VisualElement FindCommonAncestor(VisualElement other)
		{
			bool flag = other == null;
			if (flag)
			{
				throw new ArgumentNullException("other");
			}
			bool flag2 = this.panel != other.panel;
			VisualElement visualElement;
			if (flag2)
			{
				visualElement = null;
			}
			else
			{
				VisualElement visualElement2 = this;
				int i = 0;
				while (visualElement2 != null)
				{
					i++;
					visualElement2 = visualElement2.hierarchy.parent;
				}
				VisualElement visualElement3 = other;
				int j = 0;
				while (visualElement3 != null)
				{
					j++;
					visualElement3 = visualElement3.hierarchy.parent;
				}
				visualElement2 = this;
				visualElement3 = other;
				while (i > j)
				{
					i--;
					visualElement2 = visualElement2.hierarchy.parent;
				}
				while (j > i)
				{
					j--;
					visualElement3 = visualElement3.hierarchy.parent;
				}
				while (visualElement2 != visualElement3)
				{
					visualElement2 = visualElement2.hierarchy.parent;
					visualElement3 = visualElement3.hierarchy.parent;
				}
				visualElement = visualElement2;
			}
			return visualElement;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000FFB0 File Offset: 0x0000E1B0
		internal VisualElement GetRoot()
		{
			bool flag = this.panel != null;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = this.panel.visualTree;
			}
			else
			{
				VisualElement visualElement2 = this;
				while (visualElement2.m_PhysicalParent != null)
				{
					visualElement2 = visualElement2.m_PhysicalParent;
				}
				visualElement = visualElement2;
			}
			return visualElement;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000FFFC File Offset: 0x0000E1FC
		internal VisualElement GetNextElementDepthFirst()
		{
			bool flag = this.m_Children.Count > 0;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = this.m_Children[0];
			}
			else
			{
				VisualElement visualElement2 = this.m_PhysicalParent;
				VisualElement visualElement3 = this;
				while (visualElement2 != null)
				{
					int i;
					for (i = 0; i < visualElement2.m_Children.Count; i++)
					{
						bool flag2 = visualElement2.m_Children[i] == visualElement3;
						if (flag2)
						{
							break;
						}
					}
					bool flag3 = i < visualElement2.m_Children.Count - 1;
					if (flag3)
					{
						return visualElement2.m_Children[i + 1];
					}
					visualElement3 = visualElement2;
					visualElement2 = visualElement2.m_PhysicalParent;
				}
				visualElement = null;
			}
			return visualElement;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000100BC File Offset: 0x0000E2BC
		internal VisualElement GetPreviousElementDepthFirst()
		{
			bool flag = this.m_PhysicalParent != null;
			VisualElement visualElement2;
			if (flag)
			{
				int i;
				for (i = 0; i < this.m_PhysicalParent.m_Children.Count; i++)
				{
					bool flag2 = this.m_PhysicalParent.m_Children[i] == this;
					if (flag2)
					{
						break;
					}
				}
				bool flag3 = i > 0;
				if (flag3)
				{
					VisualElement visualElement = this.m_PhysicalParent.m_Children[i - 1];
					while (visualElement.m_Children.Count > 0)
					{
						visualElement = visualElement.m_Children[visualElement.m_Children.Count - 1];
					}
					visualElement2 = visualElement;
				}
				else
				{
					visualElement2 = this.m_PhysicalParent;
				}
			}
			else
			{
				visualElement2 = null;
			}
			return visualElement2;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00010184 File Offset: 0x0000E384
		internal VisualElement RetargetElement(VisualElement retargetAgainst)
		{
			bool flag = retargetAgainst == null;
			VisualElement visualElement;
			if (flag)
			{
				visualElement = this;
			}
			else
			{
				VisualElement visualElement2 = retargetAgainst.m_PhysicalParent ?? retargetAgainst;
				while (visualElement2.m_PhysicalParent != null && !visualElement2.isCompositeRoot)
				{
					visualElement2 = visualElement2.m_PhysicalParent;
				}
				VisualElement visualElement3 = this;
				VisualElement visualElement4 = this.m_PhysicalParent;
				while (visualElement4 != null)
				{
					visualElement4 = visualElement4.m_PhysicalParent;
					bool flag2 = visualElement4 == visualElement2;
					if (flag2)
					{
						return visualElement3;
					}
					bool flag3 = visualElement4 != null && visualElement4.isCompositeRoot;
					if (flag3)
					{
						visualElement3 = visualElement4;
					}
				}
				visualElement = this;
			}
			return visualElement;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0001021C File Offset: 0x0000E41C
		public IVisualElementScheduler schedule
		{
			get
			{
				return this;
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00010230 File Offset: 0x0000E430
		IVisualElementScheduledItem IVisualElementScheduler.Execute(Action<TimerState> timerUpdateEvent)
		{
			VisualElement.TimerStateScheduledItem timerStateScheduledItem = new VisualElement.TimerStateScheduledItem(this, timerUpdateEvent)
			{
				timerUpdateStopCondition = ScheduledItem.OnceCondition
			};
			timerStateScheduledItem.Resume();
			return timerStateScheduledItem;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00010260 File Offset: 0x0000E460
		IVisualElementScheduledItem IVisualElementScheduler.Execute(Action updateEvent)
		{
			VisualElement.SimpleScheduledItem simpleScheduledItem = new VisualElement.SimpleScheduledItem(this, updateEvent)
			{
				timerUpdateStopCondition = ScheduledItem.OnceCondition
			};
			simpleScheduledItem.Resume();
			return simpleScheduledItem;
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x00010290 File Offset: 0x0000E490
		public IStyle style
		{
			get
			{
				bool flag = this.inlineStyleAccess == null;
				if (flag)
				{
					this.inlineStyleAccess = new InlineStyleAccess(this);
				}
				return this.inlineStyleAccess;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x000102C1 File Offset: 0x0000E4C1
		public ICustomStyle customStyle
		{
			get
			{
				return this.computedStyle;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x000102C9 File Offset: 0x0000E4C9
		public VisualElementStyleSheetSet styleSheets
		{
			get
			{
				return new VisualElementStyleSheetSet(this);
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000102D4 File Offset: 0x0000E4D4
		internal void AddStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint) as StyleSheet;
			bool flag = styleSheet == null;
			if (flag)
			{
				bool flag2 = !VisualElement.s_InternalStyleSheetPath.IsMatch(sheetPath);
				if (flag2)
				{
					Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
				}
			}
			else
			{
				this.styleSheets.Add(styleSheet);
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x00010344 File Offset: 0x0000E544
		internal bool HasStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint) as StyleSheet;
			bool flag = styleSheet == null;
			bool flag2;
			if (flag)
			{
				Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
				flag2 = false;
			}
			else
			{
				flag2 = this.styleSheets.Contains(styleSheet);
			}
			return flag2;
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000103A4 File Offset: 0x0000E5A4
		internal void RemoveStyleSheetPath(string sheetPath)
		{
			StyleSheet styleSheet = Panel.LoadResource(sheetPath, typeof(StyleSheet), this.scaledPixelsPerPoint) as StyleSheet;
			bool flag = styleSheet == null;
			if (flag)
			{
				Debug.LogWarning(string.Format("Style sheet not found for path \"{0}\"", sheetPath));
			}
			else
			{
				this.styleSheets.Remove(styleSheet);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00010400 File Offset: 0x0000E600
		private StyleFloat ResolveLengthValue(StyleLength styleLength, bool isRow)
		{
			bool flag = styleLength.keyword > StyleKeyword.Undefined;
			StyleFloat styleFloat;
			if (flag)
			{
				styleFloat = styleLength.ToStyleFloat();
			}
			else
			{
				Length value = styleLength.value;
				bool flag2 = value.unit != LengthUnit.Percent;
				if (flag2)
				{
					styleFloat = styleLength.ToStyleFloat();
				}
				else
				{
					VisualElement parent = this.hierarchy.parent;
					bool flag3 = parent == null;
					if (flag3)
					{
						styleFloat = 0f;
					}
					else
					{
						float num = (isRow ? parent.resolvedStyle.width : parent.resolvedStyle.height);
						styleFloat = value.value * num / 100f;
					}
				}
			}
			return styleFloat;
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x000104AC File Offset: 0x0000E6AC
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x000104E0 File Offset: 0x0000E6E0
		public string tooltip
		{
			get
			{
				string text;
				base.TryGetUserArgs<TooltipEvent, string>(new EventCallback<TooltipEvent, string>(VisualElement.OnTooltip), TrickleDown.NoTrickleDown, out text);
				return text ?? string.Empty;
			}
			set
			{
				bool flag = string.IsNullOrEmpty(value);
				if (flag)
				{
					base.UnregisterCallback<TooltipEvent, string>(new EventCallback<TooltipEvent, string>(VisualElement.OnTooltip), TrickleDown.NoTrickleDown);
				}
				else
				{
					base.RegisterCallback<TooltipEvent, string>(new EventCallback<TooltipEvent, string>(VisualElement.OnTooltip), value, TrickleDown.NoTrickleDown);
				}
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00010528 File Offset: 0x0000E728
		private static void OnTooltip(TooltipEvent e, string tooltip)
		{
			VisualElement visualElement = e.currentTarget as VisualElement;
			bool flag = visualElement != null;
			if (flag)
			{
				e.rect = visualElement.worldBound;
			}
			e.tooltip = tooltip;
			e.StopImmediatePropagation();
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x00010566 File Offset: 0x0000E766
		public IResolvedStyle resolvedStyle
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0001056C File Offset: 0x0000E76C
		Align IResolvedStyle.alignContent
		{
			get
			{
				return this.computedStyle.alignContent.value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0001058C File Offset: 0x0000E78C
		Align IResolvedStyle.alignItems
		{
			get
			{
				return this.computedStyle.alignItems.value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x000105AC File Offset: 0x0000E7AC
		Align IResolvedStyle.alignSelf
		{
			get
			{
				return this.computedStyle.alignSelf.value;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x000105CC File Offset: 0x0000E7CC
		Color IResolvedStyle.backgroundColor
		{
			get
			{
				return this.computedStyle.backgroundColor.value;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x000105EC File Offset: 0x0000E7EC
		Color IResolvedStyle.borderBottomColor
		{
			get
			{
				return this.computedStyle.borderBottomColor.value;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0001060C File Offset: 0x0000E80C
		float IResolvedStyle.borderBottomLeftRadius
		{
			get
			{
				return this.computedStyle.borderBottomLeftRadius.value.value;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x00010634 File Offset: 0x0000E834
		float IResolvedStyle.borderBottomRightRadius
		{
			get
			{
				return this.computedStyle.borderBottomRightRadius.value.value;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0001065C File Offset: 0x0000E85C
		float IResolvedStyle.borderBottomWidth
		{
			get
			{
				return this.computedStyle.borderBottomWidth.value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0001067C File Offset: 0x0000E87C
		Color IResolvedStyle.borderLeftColor
		{
			get
			{
				return this.computedStyle.borderLeftColor.value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0001069C File Offset: 0x0000E89C
		float IResolvedStyle.borderLeftWidth
		{
			get
			{
				return this.computedStyle.borderLeftWidth.value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x000106BC File Offset: 0x0000E8BC
		Color IResolvedStyle.borderRightColor
		{
			get
			{
				return this.computedStyle.borderRightColor.value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x000106DC File Offset: 0x0000E8DC
		float IResolvedStyle.borderRightWidth
		{
			get
			{
				return this.computedStyle.borderRightWidth.value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x000106FC File Offset: 0x0000E8FC
		Color IResolvedStyle.borderTopColor
		{
			get
			{
				return this.computedStyle.borderTopColor.value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001071C File Offset: 0x0000E91C
		float IResolvedStyle.borderTopLeftRadius
		{
			get
			{
				return this.computedStyle.borderTopLeftRadius.value.value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x00010744 File Offset: 0x0000E944
		float IResolvedStyle.borderTopRightRadius
		{
			get
			{
				return this.computedStyle.borderTopRightRadius.value.value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0001076C File Offset: 0x0000E96C
		float IResolvedStyle.borderTopWidth
		{
			get
			{
				return this.computedStyle.borderTopWidth.value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0001078C File Offset: 0x0000E98C
		float IResolvedStyle.bottom
		{
			get
			{
				return this.yogaNode.LayoutBottom;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0001079C File Offset: 0x0000E99C
		Color IResolvedStyle.color
		{
			get
			{
				return this.computedStyle.color.value;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x000107BC File Offset: 0x0000E9BC
		DisplayStyle IResolvedStyle.display
		{
			get
			{
				return this.computedStyle.display.value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x000107DC File Offset: 0x0000E9DC
		StyleFloat IResolvedStyle.flexBasis
		{
			get
			{
				return new StyleFloat(this.yogaNode.ComputedFlexBasis);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x000107F0 File Offset: 0x0000E9F0
		FlexDirection IResolvedStyle.flexDirection
		{
			get
			{
				return this.computedStyle.flexDirection.value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00010810 File Offset: 0x0000EA10
		float IResolvedStyle.flexGrow
		{
			get
			{
				return this.computedStyle.flexGrow.value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00010830 File Offset: 0x0000EA30
		float IResolvedStyle.flexShrink
		{
			get
			{
				return this.computedStyle.flexShrink.value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00010850 File Offset: 0x0000EA50
		Wrap IResolvedStyle.flexWrap
		{
			get
			{
				return this.computedStyle.flexWrap.value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00010870 File Offset: 0x0000EA70
		float IResolvedStyle.fontSize
		{
			get
			{
				return this.computedStyle.fontSize.value.value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00010898 File Offset: 0x0000EA98
		float IResolvedStyle.height
		{
			get
			{
				return this.yogaNode.LayoutHeight;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x000108A8 File Offset: 0x0000EAA8
		Justify IResolvedStyle.justifyContent
		{
			get
			{
				return this.computedStyle.justifyContent.value;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x000108C8 File Offset: 0x0000EAC8
		float IResolvedStyle.left
		{
			get
			{
				return this.yogaNode.LayoutX;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x000108D5 File Offset: 0x0000EAD5
		float IResolvedStyle.marginBottom
		{
			get
			{
				return this.yogaNode.LayoutMarginBottom;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x000108E2 File Offset: 0x0000EAE2
		float IResolvedStyle.marginLeft
		{
			get
			{
				return this.yogaNode.LayoutMarginLeft;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x000108EF File Offset: 0x0000EAEF
		float IResolvedStyle.marginRight
		{
			get
			{
				return this.yogaNode.LayoutMarginRight;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x000108FC File Offset: 0x0000EAFC
		float IResolvedStyle.marginTop
		{
			get
			{
				return this.yogaNode.LayoutMarginTop;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x00010909 File Offset: 0x0000EB09
		StyleFloat IResolvedStyle.maxHeight
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.maxHeight, false);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0001091D File Offset: 0x0000EB1D
		StyleFloat IResolvedStyle.maxWidth
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.maxWidth, true);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00010931 File Offset: 0x0000EB31
		StyleFloat IResolvedStyle.minHeight
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.minHeight, false);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00010945 File Offset: 0x0000EB45
		StyleFloat IResolvedStyle.minWidth
		{
			get
			{
				return this.ResolveLengthValue(this.computedStyle.minWidth, true);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0001095C File Offset: 0x0000EB5C
		float IResolvedStyle.opacity
		{
			get
			{
				return this.computedStyle.opacity.value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0001097C File Offset: 0x0000EB7C
		float IResolvedStyle.paddingBottom
		{
			get
			{
				return this.yogaNode.LayoutPaddingBottom;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x00010989 File Offset: 0x0000EB89
		float IResolvedStyle.paddingLeft
		{
			get
			{
				return this.yogaNode.LayoutPaddingLeft;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x00010996 File Offset: 0x0000EB96
		float IResolvedStyle.paddingRight
		{
			get
			{
				return this.yogaNode.LayoutPaddingRight;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x000109A3 File Offset: 0x0000EBA3
		float IResolvedStyle.paddingTop
		{
			get
			{
				return this.yogaNode.LayoutPaddingTop;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x000109B0 File Offset: 0x0000EBB0
		Position IResolvedStyle.position
		{
			get
			{
				return this.computedStyle.position.value;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x000109D0 File Offset: 0x0000EBD0
		float IResolvedStyle.right
		{
			get
			{
				return this.yogaNode.LayoutRight;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x000109E0 File Offset: 0x0000EBE0
		TextOverflow IResolvedStyle.textOverflow
		{
			get
			{
				return this.computedStyle.textOverflow.value;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x00010A00 File Offset: 0x0000EC00
		float IResolvedStyle.top
		{
			get
			{
				return this.yogaNode.LayoutY;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x00010A10 File Offset: 0x0000EC10
		Color IResolvedStyle.unityBackgroundImageTintColor
		{
			get
			{
				return this.computedStyle.unityBackgroundImageTintColor.value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x00010A30 File Offset: 0x0000EC30
		ScaleMode IResolvedStyle.unityBackgroundScaleMode
		{
			get
			{
				return this.computedStyle.unityBackgroundScaleMode.value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00010A50 File Offset: 0x0000EC50
		Font IResolvedStyle.unityFont
		{
			get
			{
				return this.computedStyle.unityFont.value;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x00010A70 File Offset: 0x0000EC70
		FontStyle IResolvedStyle.unityFontStyleAndWeight
		{
			get
			{
				return this.computedStyle.unityFontStyleAndWeight.value;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x00010A90 File Offset: 0x0000EC90
		int IResolvedStyle.unitySliceBottom
		{
			get
			{
				return this.computedStyle.unitySliceBottom.value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00010AB0 File Offset: 0x0000ECB0
		int IResolvedStyle.unitySliceLeft
		{
			get
			{
				return this.computedStyle.unitySliceLeft.value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00010AD0 File Offset: 0x0000ECD0
		int IResolvedStyle.unitySliceRight
		{
			get
			{
				return this.computedStyle.unitySliceRight.value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00010AF0 File Offset: 0x0000ECF0
		int IResolvedStyle.unitySliceTop
		{
			get
			{
				return this.computedStyle.unitySliceTop.value;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00010B10 File Offset: 0x0000ED10
		TextAnchor IResolvedStyle.unityTextAlign
		{
			get
			{
				return this.computedStyle.unityTextAlign.value;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x00010B30 File Offset: 0x0000ED30
		TextOverflowPosition IResolvedStyle.unityTextOverflowPosition
		{
			get
			{
				return this.computedStyle.unityTextOverflowPosition.value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x00010B50 File Offset: 0x0000ED50
		Visibility IResolvedStyle.visibility
		{
			get
			{
				return this.computedStyle.visibility.value;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x00010B70 File Offset: 0x0000ED70
		WhiteSpace IResolvedStyle.whiteSpace
		{
			get
			{
				return this.computedStyle.whiteSpace.value;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x00010B90 File Offset: 0x0000ED90
		float IResolvedStyle.width
		{
			get
			{
				return this.yogaNode.LayoutWidth;
			}
		}

		// Token: 0x0400018F RID: 399
		private static uint s_NextId;

		// Token: 0x04000190 RID: 400
		private static List<string> s_EmptyClassList = new List<string>(0);

		// Token: 0x04000191 RID: 401
		internal static readonly PropertyName userDataPropertyKey = new PropertyName("--unity-user-data");

		// Token: 0x04000192 RID: 402
		public static readonly string disabledUssClassName = "unity-disabled";

		// Token: 0x04000193 RID: 403
		private string m_Name;

		// Token: 0x04000194 RID: 404
		private List<string> m_ClassList;

		// Token: 0x04000195 RID: 405
		private string m_TypeName;

		// Token: 0x04000196 RID: 406
		private string m_FullTypeName;

		// Token: 0x04000197 RID: 407
		private List<KeyValuePair<PropertyName, object>> m_PropertyBag;

		// Token: 0x04000198 RID: 408
		private string m_ViewDataKey;

		// Token: 0x0400019A RID: 410
		private RenderHints m_RenderHints;

		// Token: 0x0400019B RID: 411
		internal Rect lastLayout;

		// Token: 0x0400019C RID: 412
		internal RenderChainVEData renderChainData;

		// Token: 0x0400019D RID: 413
		private Vector3 m_Position = Vector3.zero;

		// Token: 0x0400019E RID: 414
		private Quaternion m_Rotation = Quaternion.identity;

		// Token: 0x0400019F RID: 415
		private Vector3 m_Scale = Vector3.one;

		// Token: 0x040001A1 RID: 417
		private Rect m_Layout;

		// Token: 0x040001A2 RID: 418
		internal bool isBoundingBoxDirty = true;

		// Token: 0x040001A3 RID: 419
		private Rect m_BoundingBox;

		// Token: 0x040001A4 RID: 420
		internal bool isWorldBoundingBoxDirty = true;

		// Token: 0x040001A5 RID: 421
		private Rect m_WorldBoundingBox;

		// Token: 0x040001A8 RID: 424
		private Matrix4x4 m_WorldTransformCache = Matrix4x4.identity;

		// Token: 0x040001A9 RID: 425
		private Matrix4x4 m_WorldTransformInverseCache = Matrix4x4.identity;

		// Token: 0x040001AB RID: 427
		private Rect m_WorldClip = Rect.zero;

		// Token: 0x040001AC RID: 428
		private Rect m_WorldClipMinusGroup = Rect.zero;

		// Token: 0x040001AD RID: 429
		private static readonly Rect s_InfiniteRect = new Rect(-10000f, -10000f, 40000f, 40000f);

		// Token: 0x040001AE RID: 430
		internal PseudoStates triggerPseudoMask;

		// Token: 0x040001AF RID: 431
		internal PseudoStates dependencyPseudoMask;

		// Token: 0x040001B0 RID: 432
		private PseudoStates m_PseudoStates;

		// Token: 0x040001B3 RID: 435
		internal ComputedStyle m_SharedStyle = InitialStyle.Get();

		// Token: 0x040001B4 RID: 436
		internal ComputedStyle m_Style = InitialStyle.Get();

		// Token: 0x040001B5 RID: 437
		internal StyleVariableContext variableContext = StyleVariableContext.none;

		// Token: 0x040001B6 RID: 438
		internal int inheritedStylesHash = 0;

		// Token: 0x040001B7 RID: 439
		internal readonly uint controlid;

		// Token: 0x040001B8 RID: 440
		internal int imguiContainerDescendantCount = 0;

		// Token: 0x040001BB RID: 443
		private bool m_RequireMeasureFunction = false;

		// Token: 0x040001BC RID: 444
		private List<IValueAnimationUpdate> m_RunningAnimations;

		// Token: 0x040001BF RID: 447
		private VisualElement m_PhysicalParent;

		// Token: 0x040001C0 RID: 448
		private VisualElement m_LogicalParent;

		// Token: 0x040001C1 RID: 449
		private static readonly List<VisualElement> s_EmptyList = new List<VisualElement>();

		// Token: 0x040001C2 RID: 450
		private List<VisualElement> m_Children;

		// Token: 0x040001C4 RID: 452
		internal InlineStyleAccess inlineStyleAccess;

		// Token: 0x040001C5 RID: 453
		internal List<StyleSheet> styleSheetList;

		// Token: 0x040001C6 RID: 454
		private static readonly Regex s_InternalStyleSheetPath = new Regex("^instanceId:[-0-9]+$", 8);

		// Token: 0x02000088 RID: 136
		public class UxmlFactory : UxmlFactory<VisualElement, VisualElement.UxmlTraits>
		{
		}

		// Token: 0x02000089 RID: 137
		public class UxmlTraits : UnityEngine.UIElements.UxmlTraits
		{
			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000455 RID: 1109 RVA: 0x00010C21 File Offset: 0x0000EE21
			// (set) Token: 0x06000456 RID: 1110 RVA: 0x00010C29 File Offset: 0x0000EE29
			protected UxmlIntAttributeDescription focusIndex { get; set; } = new UxmlIntAttributeDescription
			{
				name = null,
				obsoleteNames = new string[] { "focus-index", "focusIndex" },
				defaultValue = -1
			};

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000457 RID: 1111 RVA: 0x00010C32 File Offset: 0x0000EE32
			// (set) Token: 0x06000458 RID: 1112 RVA: 0x00010C3A File Offset: 0x0000EE3A
			protected UxmlBoolAttributeDescription focusable { get; set; } = new UxmlBoolAttributeDescription
			{
				name = "focusable",
				defaultValue = false
			};

			// Token: 0x17000114 RID: 276
			// (get) Token: 0x06000459 RID: 1113 RVA: 0x00010C44 File Offset: 0x0000EE44
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
			{
				get
				{
					yield return new UxmlChildElementDescription(typeof(VisualElement));
					yield break;
				}
			}

			// Token: 0x0600045A RID: 1114 RVA: 0x00010C64 File Offset: 0x0000EE64
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				bool flag = ve == null;
				if (flag)
				{
					throw new ArgumentNullException("ve");
				}
				ve.name = this.m_Name.GetValueFromBag(bag, cc);
				ve.viewDataKey = this.m_ViewDataKey.GetValueFromBag(bag, cc);
				ve.pickingMode = this.m_PickingMode.GetValueFromBag(bag, cc);
				bool flag2 = ve.panel == null;
				if (flag2)
				{
					ve.usageHints = this.m_UsageHints.GetValueFromBag(bag, cc);
				}
				int num = 0;
				bool flag3 = this.focusIndex.TryGetValueFromBag(bag, cc, ref num);
				if (flag3)
				{
					ve.tabIndex = ((num >= 0) ? num : 0);
					ve.focusable = num >= 0;
				}
				bool flag4 = this.m_TabIndex.TryGetValueFromBag(bag, cc, ref num);
				if (flag4)
				{
					ve.tabIndex = num;
				}
				bool flag5 = false;
				bool flag6 = this.focusable.TryGetValueFromBag(bag, cc, ref flag5);
				if (flag6)
				{
					ve.focusable = flag5;
				}
				ve.tooltip = this.m_Tooltip.GetValueFromBag(bag, cc);
			}

			// Token: 0x040001C7 RID: 455
			protected UxmlStringAttributeDescription m_Name = new UxmlStringAttributeDescription
			{
				name = "name"
			};

			// Token: 0x040001C8 RID: 456
			private UxmlStringAttributeDescription m_ViewDataKey = new UxmlStringAttributeDescription
			{
				name = "view-data-key"
			};

			// Token: 0x040001C9 RID: 457
			protected UxmlEnumAttributeDescription<PickingMode> m_PickingMode = new UxmlEnumAttributeDescription<PickingMode>
			{
				name = "picking-mode",
				obsoleteNames = new string[] { "pickingMode" }
			};

			// Token: 0x040001CA RID: 458
			private UxmlStringAttributeDescription m_Tooltip = new UxmlStringAttributeDescription
			{
				name = "tooltip"
			};

			// Token: 0x040001CB RID: 459
			private UxmlEnumAttributeDescription<UsageHints> m_UsageHints = new UxmlEnumAttributeDescription<UsageHints>
			{
				name = "usage-hints"
			};

			// Token: 0x040001CD RID: 461
			private UxmlIntAttributeDescription m_TabIndex = new UxmlIntAttributeDescription
			{
				name = "tabindex",
				defaultValue = 0
			};

			// Token: 0x040001CF RID: 463
			private UxmlStringAttributeDescription m_Class = new UxmlStringAttributeDescription
			{
				name = "class"
			};

			// Token: 0x040001D0 RID: 464
			private UxmlStringAttributeDescription m_ContentContainer = new UxmlStringAttributeDescription
			{
				name = "content-container",
				obsoleteNames = new string[] { "contentContainer" }
			};

			// Token: 0x040001D1 RID: 465
			private UxmlStringAttributeDescription m_Style = new UxmlStringAttributeDescription
			{
				name = "style"
			};
		}

		// Token: 0x0200008B RID: 139
		public enum MeasureMode
		{
			// Token: 0x040001D7 RID: 471
			Undefined,
			// Token: 0x040001D8 RID: 472
			Exactly,
			// Token: 0x040001D9 RID: 473
			AtMost
		}

		// Token: 0x0200008C RID: 140
		public struct Hierarchy
		{
			// Token: 0x17000117 RID: 279
			// (get) Token: 0x06000464 RID: 1124 RVA: 0x00010FB8 File Offset: 0x0000F1B8
			public VisualElement parent
			{
				get
				{
					return this.m_Owner.m_PhysicalParent;
				}
			}

			// Token: 0x06000465 RID: 1125 RVA: 0x00010FD5 File Offset: 0x0000F1D5
			internal Hierarchy(VisualElement element)
			{
				this.m_Owner = element;
			}

			// Token: 0x06000466 RID: 1126 RVA: 0x00010FE0 File Offset: 0x0000F1E0
			public void Add(VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot add null child");
				}
				this.Insert(this.childCount, child);
			}

			// Token: 0x06000467 RID: 1127 RVA: 0x00011010 File Offset: 0x0000F210
			public void Insert(int index, VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot insert null child");
				}
				bool flag2 = index > this.childCount;
				if (flag2)
				{
					throw new ArgumentOutOfRangeException("Index out of range: " + index);
				}
				bool flag3 = child == this.m_Owner;
				if (flag3)
				{
					throw new ArgumentException("Cannot insert element as its own child");
				}
				child.RemoveFromHierarchy();
				bool flag4 = this.m_Owner.m_Children == VisualElement.s_EmptyList;
				if (flag4)
				{
					this.m_Owner.m_Children = VisualElementListPool.Get(0);
				}
				bool isMeasureDefined = this.m_Owner.yogaNode.IsMeasureDefined;
				if (isMeasureDefined)
				{
					this.m_Owner.RemoveMeasureFunction();
				}
				this.PutChildAtIndex(child, index);
				int num = child.imguiContainerDescendantCount + (child.isIMGUIContainer ? 1 : 0);
				bool flag5 = num > 0;
				if (flag5)
				{
					this.m_Owner.ChangeIMGUIContainerCount(num);
				}
				child.hierarchy.SetParent(this.m_Owner);
				child.PropagateEnabledToChildren(this.m_Owner.enabledInHierarchy);
				child.InvokeHierarchyChanged(HierarchyChangeType.Add);
				child.IncrementVersion(VersionChangeType.Hierarchy);
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
			}

			// Token: 0x06000468 RID: 1128 RVA: 0x00011140 File Offset: 0x0000F340
			public void Remove(VisualElement child)
			{
				bool flag = child == null;
				if (flag)
				{
					throw new ArgumentException("Cannot remove null child");
				}
				bool flag2 = child.hierarchy.parent != this.m_Owner;
				if (flag2)
				{
					throw new ArgumentException("This visualElement is not my child");
				}
				int num = this.m_Owner.m_Children.IndexOf(child);
				this.RemoveAt(num);
			}

			// Token: 0x06000469 RID: 1129 RVA: 0x000111A4 File Offset: 0x0000F3A4
			public void RemoveAt(int index)
			{
				bool flag = index < 0 || index >= this.childCount;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Index out of range: " + index);
				}
				VisualElement visualElement = this.m_Owner.m_Children[index];
				visualElement.InvokeHierarchyChanged(HierarchyChangeType.Remove);
				this.RemoveChildAtIndex(index);
				int num = visualElement.imguiContainerDescendantCount + (visualElement.isIMGUIContainer ? 1 : 0);
				bool flag2 = num > 0;
				if (flag2)
				{
					this.m_Owner.ChangeIMGUIContainerCount(-num);
				}
				visualElement.hierarchy.SetParent(null);
				bool flag3 = this.childCount == 0;
				if (flag3)
				{
					this.ReleaseChildList();
					this.m_Owner.AssignMeasureFunction();
				}
				BaseVisualElementPanel elementPanel = this.m_Owner.elementPanel;
				if (elementPanel != null)
				{
					elementPanel.OnVersionChanged(visualElement, VersionChangeType.Hierarchy);
				}
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
			}

			// Token: 0x0600046A RID: 1130 RVA: 0x00011288 File Offset: 0x0000F488
			public void Clear()
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					List<VisualElement> list = VisualElementListPool.Copy(this.m_Owner.m_Children);
					this.ReleaseChildList();
					this.m_Owner.yogaNode.Clear();
					this.m_Owner.AssignMeasureFunction();
					foreach (VisualElement visualElement in list)
					{
						visualElement.InvokeHierarchyChanged(HierarchyChangeType.Remove);
						visualElement.hierarchy.SetParent(null);
						visualElement.m_LogicalParent = null;
						BaseVisualElementPanel elementPanel = this.m_Owner.elementPanel;
						if (elementPanel != null)
						{
							elementPanel.OnVersionChanged(visualElement, VersionChangeType.Hierarchy);
						}
					}
					bool flag2 = this.m_Owner.imguiContainerDescendantCount > 0;
					if (flag2)
					{
						int num = this.m_Owner.imguiContainerDescendantCount;
						bool isIMGUIContainer = this.m_Owner.isIMGUIContainer;
						if (isIMGUIContainer)
						{
							num--;
						}
						this.m_Owner.ChangeIMGUIContainerCount(-num);
					}
					VisualElementListPool.Release(list);
					this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				}
			}

			// Token: 0x0600046B RID: 1131 RVA: 0x000113B4 File Offset: 0x0000F5B4
			internal void BringToFront(VisualElement child)
			{
				bool flag = this.childCount > 1;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num >= 0 && num < this.childCount - 1;
					if (flag2)
					{
						this.MoveChildElement(child, num, this.childCount);
					}
				}
			}

			// Token: 0x0600046C RID: 1132 RVA: 0x0001140C File Offset: 0x0000F60C
			internal void SendToBack(VisualElement child)
			{
				bool flag = this.childCount > 1;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num > 0;
					if (flag2)
					{
						this.MoveChildElement(child, num, 0);
					}
				}
			}

			// Token: 0x0600046D RID: 1133 RVA: 0x00011450 File Offset: 0x0000F650
			internal void PlaceBehind(VisualElement child, VisualElement over)
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num < 0;
					if (!flag2)
					{
						int num2 = this.m_Owner.m_Children.IndexOf(over);
						bool flag3 = num2 > 0 && num < num2;
						if (flag3)
						{
							num2--;
						}
						this.MoveChildElement(child, num, num2);
					}
				}
			}

			// Token: 0x0600046E RID: 1134 RVA: 0x000114BC File Offset: 0x0000F6BC
			internal void PlaceInFront(VisualElement child, VisualElement under)
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					int num = this.m_Owner.m_Children.IndexOf(child);
					bool flag2 = num < 0;
					if (!flag2)
					{
						int num2 = this.m_Owner.m_Children.IndexOf(under);
						bool flag3 = num > num2;
						if (flag3)
						{
							num2++;
						}
						this.MoveChildElement(child, num, num2);
					}
				}
			}

			// Token: 0x0600046F RID: 1135 RVA: 0x00011521 File Offset: 0x0000F721
			private void MoveChildElement(VisualElement child, int currentIndex, int nextIndex)
			{
				child.InvokeHierarchyChanged(HierarchyChangeType.Remove);
				this.RemoveChildAtIndex(currentIndex);
				this.PutChildAtIndex(child, nextIndex);
				child.InvokeHierarchyChanged(HierarchyChangeType.Add);
				this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
			}

			// Token: 0x17000118 RID: 280
			// (get) Token: 0x06000470 RID: 1136 RVA: 0x00011554 File Offset: 0x0000F754
			public int childCount
			{
				get
				{
					return this.m_Owner.m_Children.Count;
				}
			}

			// Token: 0x17000119 RID: 281
			public VisualElement this[int key]
			{
				get
				{
					return this.m_Owner.m_Children[key];
				}
			}

			// Token: 0x06000472 RID: 1138 RVA: 0x0001159C File Offset: 0x0000F79C
			public int IndexOf(VisualElement element)
			{
				return this.m_Owner.m_Children.IndexOf(element);
			}

			// Token: 0x06000473 RID: 1139 RVA: 0x000115C0 File Offset: 0x0000F7C0
			public VisualElement ElementAt(int index)
			{
				return this[index];
			}

			// Token: 0x06000474 RID: 1140 RVA: 0x000115DC File Offset: 0x0000F7DC
			public IEnumerable<VisualElement> Children()
			{
				return this.m_Owner.m_Children;
			}

			// Token: 0x06000475 RID: 1141 RVA: 0x000115FC File Offset: 0x0000F7FC
			private void SetParent(VisualElement value)
			{
				this.m_Owner.m_PhysicalParent = value;
				this.m_Owner.m_LogicalParent = value;
				bool flag = value != null;
				if (flag)
				{
					this.m_Owner.SetPanel(this.m_Owner.m_PhysicalParent.elementPanel);
				}
				else
				{
					this.m_Owner.SetPanel(null);
				}
			}

			// Token: 0x06000476 RID: 1142 RVA: 0x0001165C File Offset: 0x0000F85C
			public void Sort(Comparison<VisualElement> comp)
			{
				bool flag = this.childCount > 0;
				if (flag)
				{
					this.m_Owner.m_Children.Sort(comp);
					this.m_Owner.yogaNode.Clear();
					for (int i = 0; i < this.m_Owner.m_Children.Count; i++)
					{
						this.m_Owner.yogaNode.Insert(i, this.m_Owner.m_Children[i].yogaNode);
					}
					this.m_Owner.InvokeHierarchyChanged(HierarchyChangeType.Move);
					this.m_Owner.IncrementVersion(VersionChangeType.Hierarchy);
				}
			}

			// Token: 0x06000477 RID: 1143 RVA: 0x00011704 File Offset: 0x0000F904
			private void PutChildAtIndex(VisualElement child, int index)
			{
				bool flag = index >= this.childCount;
				if (flag)
				{
					this.m_Owner.m_Children.Add(child);
					this.m_Owner.yogaNode.Insert(this.m_Owner.yogaNode.Count, child.yogaNode);
				}
				else
				{
					this.m_Owner.m_Children.Insert(index, child);
					this.m_Owner.yogaNode.Insert(index, child.yogaNode);
				}
			}

			// Token: 0x06000478 RID: 1144 RVA: 0x0001178C File Offset: 0x0000F98C
			private void RemoveChildAtIndex(int index)
			{
				this.m_Owner.m_Children.RemoveAt(index);
				this.m_Owner.yogaNode.RemoveAt(index);
			}

			// Token: 0x06000479 RID: 1145 RVA: 0x000117B4 File Offset: 0x0000F9B4
			private void ReleaseChildList()
			{
				bool flag = this.m_Owner.m_Children != VisualElement.s_EmptyList;
				if (flag)
				{
					List<VisualElement> children = this.m_Owner.m_Children;
					this.m_Owner.m_Children = VisualElement.s_EmptyList;
					VisualElementListPool.Release(children);
				}
			}

			// Token: 0x0600047A RID: 1146 RVA: 0x00011800 File Offset: 0x0000FA00
			public bool Equals(VisualElement.Hierarchy other)
			{
				return other == this;
			}

			// Token: 0x0600047B RID: 1147 RVA: 0x00011820 File Offset: 0x0000FA20
			public override bool Equals(object obj)
			{
				bool flag = obj == null;
				return !flag && obj is VisualElement.Hierarchy && this.Equals((VisualElement.Hierarchy)obj);
			}

			// Token: 0x0600047C RID: 1148 RVA: 0x00011858 File Offset: 0x0000FA58
			public override int GetHashCode()
			{
				return (this.m_Owner != null) ? this.m_Owner.GetHashCode() : 0;
			}

			// Token: 0x0600047D RID: 1149 RVA: 0x00011880 File Offset: 0x0000FA80
			public static bool operator ==(VisualElement.Hierarchy x, VisualElement.Hierarchy y)
			{
				return x.m_Owner == y.m_Owner;
			}

			// Token: 0x0600047E RID: 1150 RVA: 0x000118A0 File Offset: 0x0000FAA0
			public static bool operator !=(VisualElement.Hierarchy x, VisualElement.Hierarchy y)
			{
				return !(x == y);
			}

			// Token: 0x040001DA RID: 474
			private readonly VisualElement m_Owner;
		}

		// Token: 0x0200008D RID: 141
		private abstract class BaseVisualElementScheduledItem : ScheduledItem, IVisualElementScheduledItem, IVisualElementPanelActivatable
		{
			// Token: 0x1700011A RID: 282
			// (get) Token: 0x0600047F RID: 1151 RVA: 0x000118BC File Offset: 0x0000FABC
			// (set) Token: 0x06000480 RID: 1152 RVA: 0x000118C4 File Offset: 0x0000FAC4
			public VisualElement element { get; private set; }

			// Token: 0x1700011B RID: 283
			// (get) Token: 0x06000481 RID: 1153 RVA: 0x000118D0 File Offset: 0x0000FAD0
			public bool isActive
			{
				get
				{
					return this.m_Activator.isActive;
				}
			}

			// Token: 0x06000482 RID: 1154 RVA: 0x000118ED File Offset: 0x0000FAED
			protected BaseVisualElementScheduledItem(VisualElement handler)
			{
				this.element = handler;
				this.m_Activator = new VisualElementPanelActivator(this);
			}

			// Token: 0x06000483 RID: 1155 RVA: 0x00011914 File Offset: 0x0000FB14
			public IVisualElementScheduledItem StartingIn(long delayMs)
			{
				base.delayMs = delayMs;
				return this;
			}

			// Token: 0x06000484 RID: 1156 RVA: 0x00011930 File Offset: 0x0000FB30
			public IVisualElementScheduledItem Until(Func<bool> stopCondition)
			{
				bool flag = stopCondition == null;
				if (flag)
				{
					stopCondition = ScheduledItem.ForeverCondition;
				}
				this.timerUpdateStopCondition = stopCondition;
				return this;
			}

			// Token: 0x06000485 RID: 1157 RVA: 0x0001195C File Offset: 0x0000FB5C
			public IVisualElementScheduledItem ForDuration(long durationMs)
			{
				base.SetDuration(durationMs);
				return this;
			}

			// Token: 0x06000486 RID: 1158 RVA: 0x00011978 File Offset: 0x0000FB78
			public IVisualElementScheduledItem Every(long intervalMs)
			{
				base.intervalMs = intervalMs;
				bool flag = this.timerUpdateStopCondition == ScheduledItem.OnceCondition;
				if (flag)
				{
					this.timerUpdateStopCondition = ScheduledItem.ForeverCondition;
				}
				return this;
			}

			// Token: 0x06000487 RID: 1159 RVA: 0x000119B4 File Offset: 0x0000FBB4
			internal override void OnItemUnscheduled()
			{
				base.OnItemUnscheduled();
				this.isScheduled = false;
				bool flag = !this.m_Activator.isDetaching;
				if (flag)
				{
					this.m_Activator.SetActive(false);
				}
			}

			// Token: 0x06000488 RID: 1160 RVA: 0x000119F1 File Offset: 0x0000FBF1
			public void Resume()
			{
				this.m_Activator.SetActive(true);
			}

			// Token: 0x06000489 RID: 1161 RVA: 0x00011A01 File Offset: 0x0000FC01
			public void Pause()
			{
				this.m_Activator.SetActive(false);
			}

			// Token: 0x0600048A RID: 1162 RVA: 0x00011A14 File Offset: 0x0000FC14
			public void ExecuteLater(long delayMs)
			{
				bool flag = !this.isScheduled;
				if (flag)
				{
					this.Resume();
				}
				base.ResetStartTime();
				this.StartingIn(delayMs);
			}

			// Token: 0x0600048B RID: 1163 RVA: 0x00011A48 File Offset: 0x0000FC48
			public void OnPanelActivate()
			{
				bool flag = !this.isScheduled;
				if (flag)
				{
					this.isScheduled = true;
					base.ResetStartTime();
					this.element.elementPanel.scheduler.Schedule(this);
				}
			}

			// Token: 0x0600048C RID: 1164 RVA: 0x00011A8C File Offset: 0x0000FC8C
			public void OnPanelDeactivate()
			{
				bool flag = this.isScheduled;
				if (flag)
				{
					this.isScheduled = false;
					this.element.elementPanel.scheduler.Unschedule(this);
				}
			}

			// Token: 0x0600048D RID: 1165 RVA: 0x00011AC4 File Offset: 0x0000FCC4
			public bool CanBeActivated()
			{
				return this.element != null && this.element.elementPanel != null && this.element.elementPanel.scheduler != null;
			}

			// Token: 0x040001DC RID: 476
			public bool isScheduled = false;

			// Token: 0x040001DD RID: 477
			private VisualElementPanelActivator m_Activator;
		}

		// Token: 0x0200008E RID: 142
		private abstract class VisualElementScheduledItem<ActionType> : VisualElement.BaseVisualElementScheduledItem
		{
			// Token: 0x0600048E RID: 1166 RVA: 0x00011B01 File Offset: 0x0000FD01
			public VisualElementScheduledItem(VisualElement handler, ActionType upEvent)
				: base(handler)
			{
				this.updateEvent = upEvent;
			}

			// Token: 0x0600048F RID: 1167 RVA: 0x00011B14 File Offset: 0x0000FD14
			public static bool Matches(ScheduledItem item, ActionType updateEvent)
			{
				VisualElement.VisualElementScheduledItem<ActionType> visualElementScheduledItem = item as VisualElement.VisualElementScheduledItem<ActionType>;
				bool flag = visualElementScheduledItem != null;
				return flag && EqualityComparer<ActionType>.Default.Equals(visualElementScheduledItem.updateEvent, updateEvent);
			}

			// Token: 0x040001DE RID: 478
			public ActionType updateEvent;
		}

		// Token: 0x0200008F RID: 143
		private class TimerStateScheduledItem : VisualElement.VisualElementScheduledItem<Action<TimerState>>
		{
			// Token: 0x06000490 RID: 1168 RVA: 0x00011B4B File Offset: 0x0000FD4B
			public TimerStateScheduledItem(VisualElement handler, Action<TimerState> updateEvent)
				: base(handler, updateEvent)
			{
			}

			// Token: 0x06000491 RID: 1169 RVA: 0x00011B58 File Offset: 0x0000FD58
			public override void PerformTimerUpdate(TimerState state)
			{
				bool isScheduled = this.isScheduled;
				if (isScheduled)
				{
					this.updateEvent.Invoke(state);
				}
			}
		}

		// Token: 0x02000090 RID: 144
		private class SimpleScheduledItem : VisualElement.VisualElementScheduledItem<Action>
		{
			// Token: 0x06000492 RID: 1170 RVA: 0x00011B7F File Offset: 0x0000FD7F
			public SimpleScheduledItem(VisualElement handler, Action updateEvent)
				: base(handler, updateEvent)
			{
			}

			// Token: 0x06000493 RID: 1171 RVA: 0x00011B8C File Offset: 0x0000FD8C
			public override void PerformTimerUpdate(TimerState state)
			{
				bool isScheduled = this.isScheduled;
				if (isScheduled)
				{
					this.updateEvent.Invoke();
				}
			}
		}
	}
}
