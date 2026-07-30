using System;

namespace System.Windows.Forms.Design
{
	/// <summary>Specifies which types a <see cref="T:System.Windows.Forms.ToolStripItem" /> can appear in. This class cannot be inherited.</summary>
	// Token: 0x02000019 RID: 25
	[AttributeUsage(4)]
	public sealed class ToolStripItemDesignerAvailabilityAttribute : Attribute
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailabilityAttribute" /> class. </summary>
		// Token: 0x060000CD RID: 205 RVA: 0x00004410 File Offset: 0x00002610
		public ToolStripItemDesignerAvailabilityAttribute()
		{
			this.visibility = ToolStripItemDesignerAvailability.None;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailabilityAttribute" /> class with the specified visibility. </summary>
		/// <param name="visibility">A <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailability" /> value specifying the visibility.</param>
		// Token: 0x060000CE RID: 206 RVA: 0x00004420 File Offset: 0x00002620
		public ToolStripItemDesignerAvailabilityAttribute(ToolStripItemDesignerAvailability visibility)
		{
			this.visibility = visibility;
		}

		/// <summary>Gets the visibility of a <see cref="T:System.Windows.Forms.ToolStripItem" />.</summary>
		/// <returns>A <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailability" /> representing the visibility.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000443C File Offset: 0x0000263C
		public ToolStripItemDesignerAvailability ItemAdditionVisibility
		{
			get
			{
				return this.visibility;
			}
		}

		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x060000D1 RID: 209 RVA: 0x00004444 File Offset: 0x00002644
		public override bool Equals(object obj)
		{
			return obj is ToolStripItemDesignerAvailabilityAttribute && this.ItemAdditionVisibility == (obj as ToolStripItemDesignerAvailabilityAttribute).ItemAdditionVisibility;
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060000D2 RID: 210 RVA: 0x00004474 File Offset: 0x00002674
		public override int GetHashCode()
		{
			return (int)this.visibility;
		}

		/// <summary>When overriden in a derived class, indicates whether the value of this instance is the default value for the derived class.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x060000D3 RID: 211 RVA: 0x0000447C File Offset: 0x0000267C
		public override bool IsDefaultAttribute()
		{
			return this.visibility == ToolStripItemDesignerAvailability.None;
		}

		// Token: 0x04000056 RID: 86
		private ToolStripItemDesignerAvailability visibility;

		/// <summary>Specifies the default value of the <see cref="T:System.Windows.Forms.Design.ToolStripItemDesignerAvailabilityAttribute" />. This field is read-only.</summary>
		// Token: 0x04000057 RID: 87
		public static readonly ToolStripItemDesignerAvailabilityAttribute Default = new ToolStripItemDesignerAvailabilityAttribute();
	}
}
