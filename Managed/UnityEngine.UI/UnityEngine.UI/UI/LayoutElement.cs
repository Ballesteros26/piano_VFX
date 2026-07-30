using System;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000024 RID: 36
	[AddComponentMenu("Layout/Layout Element", 140)]
	[RequireComponent(typeof(RectTransform))]
	[ExecuteAlways]
	public class LayoutElement : UIBehaviour, ILayoutElement, ILayoutIgnorer
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000DB50 File Offset: 0x0000BD50
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000DB58 File Offset: 0x0000BD58
		public virtual bool ignoreLayout
		{
			get
			{
				return this.m_IgnoreLayout;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_IgnoreLayout, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00004C7A File Offset: 0x00002E7A
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000DB6E File Offset: 0x0000BD6E
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000DB76 File Offset: 0x0000BD76
		public virtual float minWidth
		{
			get
			{
				return this.m_MinWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000DB94 File Offset: 0x0000BD94
		public virtual float minHeight
		{
			get
			{
				return this.m_MinHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000DBAA File Offset: 0x0000BDAA
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000DBB2 File Offset: 0x0000BDB2
		public virtual float preferredWidth
		{
			get
			{
				return this.m_PreferredWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000DBC8 File Offset: 0x0000BDC8
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000DBD0 File Offset: 0x0000BDD0
		public virtual float preferredHeight
		{
			get
			{
				return this.m_PreferredHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_PreferredHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000DBE6 File Offset: 0x0000BDE6
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000DBEE File Offset: 0x0000BDEE
		public virtual float flexibleWidth
		{
			get
			{
				return this.m_FlexibleWidth;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FlexibleWidth, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000DC04 File Offset: 0x0000BE04
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000DC0C File Offset: 0x0000BE0C
		public virtual float flexibleHeight
		{
			get
			{
				return this.m_FlexibleHeight;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_FlexibleHeight, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000DC22 File Offset: 0x0000BE22
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000DC2A File Offset: 0x0000BE2A
		public virtual int layoutPriority
		{
			get
			{
				return this.m_LayoutPriority;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_LayoutPriority, value))
				{
					this.SetDirty();
				}
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000DC40 File Offset: 0x0000BE40
		protected LayoutElement()
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000DC9C File Offset: 0x0000BE9C
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetDirty();
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000DCAA File Offset: 0x0000BEAA
		protected override void OnTransformParentChanged()
		{
			this.SetDirty();
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000DCB2 File Offset: 0x0000BEB2
		protected override void OnDisable()
		{
			this.SetDirty();
			base.OnDisable();
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000DCAA File Offset: 0x0000BEAA
		protected override void OnDidApplyAnimationProperties()
		{
			this.SetDirty();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000DCAA File Offset: 0x0000BEAA
		protected override void OnBeforeTransformParentChanged()
		{
			this.SetDirty();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000DCC0 File Offset: 0x0000BEC0
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(base.transform as RectTransform);
		}

		// Token: 0x040000D8 RID: 216
		[SerializeField]
		private bool m_IgnoreLayout;

		// Token: 0x040000D9 RID: 217
		[SerializeField]
		private float m_MinWidth = -1f;

		// Token: 0x040000DA RID: 218
		[SerializeField]
		private float m_MinHeight = -1f;

		// Token: 0x040000DB RID: 219
		[SerializeField]
		private float m_PreferredWidth = -1f;

		// Token: 0x040000DC RID: 220
		[SerializeField]
		private float m_PreferredHeight = -1f;

		// Token: 0x040000DD RID: 221
		[SerializeField]
		private float m_FlexibleWidth = -1f;

		// Token: 0x040000DE RID: 222
		[SerializeField]
		private float m_FlexibleHeight = -1f;

		// Token: 0x040000DF RID: 223
		[SerializeField]
		private int m_LayoutPriority = 1;
	}
}
