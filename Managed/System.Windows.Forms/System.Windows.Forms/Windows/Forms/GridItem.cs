using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Implements one row in a <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001A4 RID: 420
	public abstract class GridItem
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.GridItem" /> class. </summary>
		// Token: 0x06001B7C RID: 7036 RVA: 0x0006B210 File Offset: 0x00069410
		protected GridItem()
		{
			this.expanded = false;
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the specified property is expandable to show nested properties.</summary>
		/// <returns>true if the specified property can be expanded; otherwise, false. The default is false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001B7D RID: 7037 RVA: 0x0006B220 File Offset: 0x00069420
		public virtual bool Expandable
		{
			get
			{
				return this.GridItems.Count > 1;
			}
		}

		/// <summary>When overridden in a derived class, gets or sets a value indicating whether the <see cref="T:System.Windows.Forms.GridItem" /> is in an expanded state.</summary>
		/// <returns>false in all cases.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Windows.Forms.GridItem.Expanded" /> property was set to true, but a <see cref="T:System.Windows.Forms.GridItem" /> is not expandable.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06001B7E RID: 7038 RVA: 0x0006B230 File Offset: 0x00069430
		// (set) Token: 0x06001B7F RID: 7039 RVA: 0x0006B238 File Offset: 0x00069438
		public virtual bool Expanded
		{
			get
			{
				return this.expanded;
			}
			set
			{
				this.expanded = value;
			}
		}

		/// <summary>When overridden in a derived class, gets the collection of <see cref="T:System.Windows.Forms.GridItem" /> objects, if any, associated as a child of this <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.GridItemCollection" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001B80 RID: 7040
		public abstract GridItemCollection GridItems { get; }

		/// <summary>When overridden in a derived class, gets the type of this <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.GridItemType" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001B81 RID: 7041
		public abstract GridItemType GridItemType { get; }

		/// <summary>When overridden in a derived class, gets the text of this <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the text associated with this <see cref="T:System.Windows.Forms.GridItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001B82 RID: 7042
		public abstract string Label { get; }

		/// <summary>When overridden in a derived class, gets the parent <see cref="T:System.Windows.Forms.GridItem" /> of this <see cref="T:System.Windows.Forms.GridItem" />, if any.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.GridItem" /> representing the parent of the <see cref="T:System.Windows.Forms.GridItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001B83 RID: 7043
		public abstract GridItem Parent { get; }

		/// <summary>When overridden in a derived class, gets the <see cref="T:System.ComponentModel.PropertyDescriptor" /> that is associated with this <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.PropertyDescriptor" /> associated with this <see cref="T:System.Windows.Forms.GridItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001B84 RID: 7044
		public abstract PropertyDescriptor PropertyDescriptor { get; }

		/// <summary>Gets or sets user-defined data about the <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains data about the <see cref="T:System.Windows.Forms.GridItem" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001B85 RID: 7045 RVA: 0x0006B244 File Offset: 0x00069444
		// (set) Token: 0x06001B86 RID: 7046 RVA: 0x0006B24C File Offset: 0x0006944C
		[DefaultValue(null)]
		[Bindable(true)]
		[TypeConverter(typeof(StringConverter))]
		[Localizable(false)]
		public object Tag
		{
			get
			{
				return this.tag;
			}
			set
			{
				this.tag = value;
			}
		}

		/// <summary>When overridden in a derived class, gets the current value of this <see cref="T:System.Windows.Forms.GridItem" />.</summary>
		/// <returns>The current value of this <see cref="T:System.Windows.Forms.GridItem" />. This can be null.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001B87 RID: 7047
		public abstract object Value { get; }

		/// <summary>When overridden in a derived class, selects this <see cref="T:System.Windows.Forms.GridItem" /> in the <see cref="T:System.Windows.Forms.PropertyGrid" />.</summary>
		/// <returns>true if the selection is successful; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001B88 RID: 7048
		public abstract bool Select();

		// Token: 0x04000F14 RID: 3860
		private bool expanded;

		// Token: 0x04000F15 RID: 3861
		private object tag;
	}
}
