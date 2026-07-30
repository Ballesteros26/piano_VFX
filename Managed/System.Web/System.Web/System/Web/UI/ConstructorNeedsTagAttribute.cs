using System;

namespace System.Web.UI
{
	/// <summary>Specifies that a server control needs a tag name in its constructor.</summary>
	// Token: 0x02000156 RID: 342
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ConstructorNeedsTagAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ConstructorNeedsTagAttribute" /> class.</summary>
		// Token: 0x06000F1F RID: 3871 RVA: 0x00002C1E File Offset: 0x00000E1E
		public ConstructorNeedsTagAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ConstructorNeedsTagAttribute" /> class.</summary>
		/// <param name="needsTag">true to add a tag to a control; otherwise, false. </param>
		// Token: 0x06000F20 RID: 3872 RVA: 0x0002B0C3 File Offset: 0x000292C3
		public ConstructorNeedsTagAttribute(bool needsTag)
		{
			this.needsTag = needsTag;
		}

		/// <summary>Indicates whether a control needs a tag name in its constructor. This property is read-only.</summary>
		/// <returns>true if the control needs a tag in its constructor. The default is false.</returns>
		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x0002B0D2 File Offset: 0x000292D2
		public bool NeedsTag
		{
			get
			{
				return this.needsTag;
			}
		}

		// Token: 0x0400122E RID: 4654
		private bool needsTag;
	}
}
