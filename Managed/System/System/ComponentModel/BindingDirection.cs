using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the template can be bound one way or two ways.</summary>
	// Token: 0x02000236 RID: 566
	public enum BindingDirection
	{
		/// <summary>The template can only accept property values. Used with a generic <see cref="T:System.Web.UI.ITemplate" />.</summary>
		// Token: 0x04001254 RID: 4692
		OneWay,
		/// <summary>The template can accept and expose property values. Used with an <see cref="T:System.Web.UI.IBindableTemplate" />.</summary>
		// Token: 0x04001255 RID: 4693
		TwoWay
	}
}
