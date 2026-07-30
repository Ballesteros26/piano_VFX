using System;

namespace System.Windows.Forms
{
	/// <summary>Specifies the default docking behavior for a control.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200014A RID: 330
	[AttributeUsage(4)]
	public sealed class DockingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DockingAttribute" /> class. </summary>
		// Token: 0x060016FD RID: 5885 RVA: 0x00055384 File Offset: 0x00053584
		public DockingAttribute()
		{
			this.dockingBehavior = DockingBehavior.Never;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DockingAttribute" /> class with the given docking behavior. </summary>
		/// <param name="dockingBehavior">A <see cref="T:System.Windows.Forms.DockingBehavior" /> value specifying the default behavior.</param>
		// Token: 0x060016FE RID: 5886 RVA: 0x00055394 File Offset: 0x00053594
		public DockingAttribute(DockingBehavior dockingBehavior)
		{
			this.dockingBehavior = dockingBehavior;
		}

		/// <summary>Gets the docking behavior supplied to this attribute.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.DockingBehavior" /> value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x000553B0 File Offset: 0x000535B0
		public DockingBehavior DockingBehavior
		{
			get
			{
				return this.dockingBehavior;
			}
		}

		/// <summary>Compares an arbitrary object with the <see cref="T:System.Windows.Forms.DockingAttribute" /> object for equality.</summary>
		/// <returns>true is <paramref name="obj" /> is equal to this <see cref="T:System.Windows.Forms.DockingAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> against which to compare this <see cref="T:System.Windows.Forms.DockingAttribute" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001701 RID: 5889 RVA: 0x000553B8 File Offset: 0x000535B8
		public override bool Equals(object obj)
		{
			return obj is DockingAttribute && this.dockingBehavior == ((DockingAttribute)obj).DockingBehavior;
		}

		/// <summary>The hash code for this object.</summary>
		/// <returns>An <see cref="T:System.Int32" /> representing an in-memory hash of this object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001702 RID: 5890 RVA: 0x000553E8 File Offset: 0x000535E8
		public override int GetHashCode()
		{
			return this.dockingBehavior.GetHashCode();
		}

		/// <summary>Specifies whether this <see cref="T:System.Windows.Forms.DockingAttribute" /> is the default docking attribute.</summary>
		/// <returns>true is the current <see cref="T:System.Windows.Forms.DockingAttribute" /> is the default; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001703 RID: 5891 RVA: 0x000553FC File Offset: 0x000535FC
		public override bool IsDefaultAttribute()
		{
			return DockingAttribute.Default.Equals(this);
		}

		// Token: 0x04000CA1 RID: 3233
		private DockingBehavior dockingBehavior;

		/// <summary>The default <see cref="T:System.Windows.Forms.DockingAttribute" /> for this control.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000CA2 RID: 3234
		public static readonly DockingAttribute Default = new DockingAttribute();
	}
}
