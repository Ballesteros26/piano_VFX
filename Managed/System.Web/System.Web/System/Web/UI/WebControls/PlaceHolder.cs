using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Stores dynamically added server controls on the Web page.</summary>
	// Token: 0x020003F3 RID: 1011
	[ControlBuilder(typeof(PlaceHolderControlBuilder))]
	public class PlaceHolder : Control
	{
		/// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
		/// <returns>true to use themes; otherwise, false. The default is false.</returns>
		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06002CB5 RID: 11445 RVA: 0x00076DBC File Offset: 0x00074FBC
		// (set) Token: 0x06002CB6 RID: 11446 RVA: 0x00076DC4 File Offset: 0x00074FC4
		[Browsable(true)]
		public override bool EnableTheming { get; set; }
	}
}
