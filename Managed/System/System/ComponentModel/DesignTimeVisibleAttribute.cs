using System;

namespace System.ComponentModel
{
	/// <summary>
	///   <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> marks a component's visibility. If <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Yes" /> is present, a visual designer can show this component on a designer.</summary>
	// Token: 0x0200025F RID: 607
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
	public sealed class DesignTimeVisibleAttribute : Attribute
	{
		/// <summary>Creates a new <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> with the <see cref="P:System.ComponentModel.DesignTimeVisibleAttribute.Visible" /> property set to the given value in <paramref name="visible" />.</summary>
		/// <param name="visible">The value that the <see cref="P:System.ComponentModel.DesignTimeVisibleAttribute.Visible" /> property will be set against. </param>
		// Token: 0x06001377 RID: 4983 RVA: 0x00051640 File Offset: 0x0004F840
		public DesignTimeVisibleAttribute(bool visible)
		{
			this.visible = visible;
		}

		/// <summary>Creates a new <see cref="T:System.ComponentModel.DesignTimeVisibleAttribute" /> set to the default value of false.</summary>
		// Token: 0x06001378 RID: 4984 RVA: 0x00002C6F File Offset: 0x00000E6F
		public DesignTimeVisibleAttribute()
		{
		}

		/// <summary>Gets or sets whether the component should be shown at design time.</summary>
		/// <returns>true if this component should be shown at design time, or false if it shouldn't.</returns>
		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0005164F File Offset: 0x0004F84F
		public bool Visible
		{
			get
			{
				return this.visible;
			}
		}

		/// <param name="obj">The object to compare.</param>
		// Token: 0x0600137A RID: 4986 RVA: 0x00051658 File Offset: 0x0004F858
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignTimeVisibleAttribute designTimeVisibleAttribute = obj as DesignTimeVisibleAttribute;
			return designTimeVisibleAttribute != null && designTimeVisibleAttribute.Visible == this.visible;
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x00051685 File Offset: 0x0004F885
		public override int GetHashCode()
		{
			return typeof(DesignTimeVisibleAttribute).GetHashCode() ^ (this.visible ? (-1) : 0);
		}

		/// <summary>Gets a value indicating if this instance is equal to the <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Default" /> value.</summary>
		/// <returns>true, if this instance is equal to the <see cref="F:System.ComponentModel.DesignTimeVisibleAttribute.Default" /> value; otherwise, false.</returns>
		// Token: 0x0600137C RID: 4988 RVA: 0x000516A3 File Offset: 0x0004F8A3
		public override bool IsDefaultAttribute()
		{
			return this.Visible == DesignTimeVisibleAttribute.Default.Visible;
		}

		// Token: 0x040012B4 RID: 4788
		private bool visible;

		/// <summary>Marks a component as visible in a visual designer.</summary>
		// Token: 0x040012B5 RID: 4789
		public static readonly DesignTimeVisibleAttribute Yes = new DesignTimeVisibleAttribute(true);

		/// <summary>Marks a component as not visible in a visual designer.</summary>
		// Token: 0x040012B6 RID: 4790
		public static readonly DesignTimeVisibleAttribute No = new DesignTimeVisibleAttribute(false);

		/// <summary>The default visibility which is Yes.</summary>
		// Token: 0x040012B7 RID: 4791
		public static readonly DesignTimeVisibleAttribute Default = DesignTimeVisibleAttribute.Yes;
	}
}
