using System;

namespace System.Web.UI
{
	/// <summary>Defines the attribute that indicates whether a control is treated as a visual or non-visual control during design time. This class cannot be inherited.</summary>
	// Token: 0x0200018E RID: 398
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class NonVisualControlAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.NonVisualControlAttribute" /> class.</summary>
		// Token: 0x06000FAE RID: 4014 RVA: 0x0002B559 File Offset: 0x00029759
		public NonVisualControlAttribute()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.NonVisualControlAttribute" /> class, using the specified Boolean value to determine whether the attribute represents a visual or non-visual control. </summary>
		/// <param name="nonVisual">true to initialize the <see cref="T:System.Web.UI.NonVisualControlAttribute" /> to represent a Web control that is not rendered to the client at run time; otherwise, false.</param>
		// Token: 0x06000FAF RID: 4015 RVA: 0x0002B562 File Offset: 0x00029762
		public NonVisualControlAttribute(bool nonVisual)
		{
			this._nonVisual = nonVisual;
		}

		/// <summary>Gets a value indicating whether the control is non-visual.</summary>
		/// <returns>true if the control has been marked as non-visual; otherwise, false. </returns>
		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0002B571 File Offset: 0x00029771
		public bool IsNonVisual
		{
			get
			{
				return this._nonVisual;
			}
		}

		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x06000FB1 RID: 4017 RVA: 0x0002B57C File Offset: 0x0002977C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			NonVisualControlAttribute nonVisualControlAttribute = obj as NonVisualControlAttribute;
			return nonVisualControlAttribute != null && nonVisualControlAttribute.IsNonVisual == this.IsNonVisual;
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000FB2 RID: 4018 RVA: 0x0002B5A9 File Offset: 0x000297A9
		public override int GetHashCode()
		{
			return this._nonVisual.GetHashCode();
		}

		/// <summary>Returns a value indicating whether the current instance is equivalent to a default instance of the <see cref="T:System.Web.UI.NonVisualControlAttribute" /> class.</summary>
		/// <returns>true if the current instance is equivalent to a <see cref="F:System.Web.UI.NonVisualControlAttribute.Default" /> instance of the class; otherwise, false.</returns>
		// Token: 0x06000FB3 RID: 4019 RVA: 0x0002B5B6 File Offset: 0x000297B6
		public override bool IsDefaultAttribute()
		{
			return this.Equals(NonVisualControlAttribute.Default);
		}

		/// <summary>Returns a <see cref="T:System.Web.UI.NonVisualControlAttribute" /> instance that is applied to a Web control to be treated as a non-visual control during design time. This field is read-only.</summary>
		// Token: 0x04001317 RID: 4887
		public static readonly NonVisualControlAttribute NonVisual = new NonVisualControlAttribute(true);

		/// <summary>Gets a <see cref="T:System.Web.UI.NonVisualControlAttribute" /> instance that is applied to a Web control to be treated as a visual control during design time. </summary>
		// Token: 0x04001318 RID: 4888
		public static readonly NonVisualControlAttribute Visual = new NonVisualControlAttribute(false);

		/// <summary>Returns a <see cref="T:System.Web.UI.NonVisualControlAttribute" /> instance that represents the application-defined default value of the attribute. This field is read-only.</summary>
		// Token: 0x04001319 RID: 4889
		public static readonly NonVisualControlAttribute Default = NonVisualControlAttribute.Visual;

		// Token: 0x0400131A RID: 4890
		private bool _nonVisual;
	}
}
