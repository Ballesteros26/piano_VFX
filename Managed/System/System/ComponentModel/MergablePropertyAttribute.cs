using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that this property can be combined with properties belonging to other objects in a Properties window.</summary>
	// Token: 0x020002B5 RID: 693
	[AttributeUsage(AttributeTargets.All)]
	public sealed class MergablePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MergablePropertyAttribute" /> class.</summary>
		/// <param name="allowMerge">true if this property can be combined with properties belonging to other objects in a Properties window; otherwise, false. </param>
		// Token: 0x060015CC RID: 5580 RVA: 0x0005647E File Offset: 0x0005467E
		public MergablePropertyAttribute(bool allowMerge)
		{
			this.allowMerge = allowMerge;
		}

		/// <summary>Gets a value indicating whether this property can be combined with properties belonging to other objects in a Properties window.</summary>
		/// <returns>true if this property can be combined with properties belonging to other objects in a Properties window; otherwise, false.</returns>
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x0005648D File Offset: 0x0005468D
		public bool AllowMerge
		{
			get
			{
				return this.allowMerge;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x060015CE RID: 5582 RVA: 0x00056498 File Offset: 0x00054698
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			MergablePropertyAttribute mergablePropertyAttribute = obj as MergablePropertyAttribute;
			return mergablePropertyAttribute != null && mergablePropertyAttribute.AllowMerge == this.allowMerge;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.MergablePropertyAttribute" />.</returns>
		// Token: 0x060015CF RID: 5583 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x060015D0 RID: 5584 RVA: 0x000564C5 File Offset: 0x000546C5
		public override bool IsDefaultAttribute()
		{
			return this.Equals(MergablePropertyAttribute.Default);
		}

		/// <summary>Specifies that a property can be combined with properties belonging to other objects in a Properties window. This static field is read-only.</summary>
		// Token: 0x04001375 RID: 4981
		public static readonly MergablePropertyAttribute Yes = new MergablePropertyAttribute(true);

		/// <summary>Specifies that a property cannot be combined with properties belonging to other objects in a Properties window. This static field is read-only.</summary>
		// Token: 0x04001376 RID: 4982
		public static readonly MergablePropertyAttribute No = new MergablePropertyAttribute(false);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.MergablePropertyAttribute.Yes" />, that is a property can be combined with properties belonging to other objects in a Properties window. This static field is read-only.</summary>
		// Token: 0x04001377 RID: 4983
		public static readonly MergablePropertyAttribute Default = MergablePropertyAttribute.Yes;

		// Token: 0x04001378 RID: 4984
		private bool allowMerge;
	}
}
