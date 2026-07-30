using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines a rectangular hot spot region in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control. This class cannot be inherited.</summary>
	// Token: 0x020003FB RID: 1019
	public sealed class RectangleHotSpot : HotSpot
	{
		/// <summary>Returns a string that represents the x -and y-coordinates of a <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object's top left corner and the x- and y-coordinates of its bottom right corner.</summary>
		/// <returns>A string that represents the x- and y-coordinates of a <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object's top left corner and the x- and y-coordinates of its bottom right corner.</returns>
		// Token: 0x06002D08 RID: 11528 RVA: 0x00077690 File Offset: 0x00075890
		public override string GetCoordinates()
		{
			return string.Concat(new object[] { this.Left, ",", this.Top, ",", this.Right, ",", this.Bottom });
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x06002D09 RID: 11529 RVA: 0x000776F8 File Offset: 0x000758F8
		protected internal override string MarkupName
		{
			get
			{
				return "rect";
			}
		}

		/// <summary>Gets or sets the x-coordinate of the left side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object.</summary>
		/// <returns>The x-coordinate of the left side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object. The default is 0.</returns>
		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x00077700 File Offset: 0x00075900
		// (set) Token: 0x06002D0B RID: 11531 RVA: 0x00077729 File Offset: 0x00075929
		[DefaultValue(0)]
		public int Left
		{
			get
			{
				object obj = base.ViewState["Left"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Left"] = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of the top side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object.</summary>
		/// <returns>The y-coordinate of the top side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object. The default is 0.</returns>
		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x00077744 File Offset: 0x00075944
		// (set) Token: 0x06002D0D RID: 11533 RVA: 0x0007776D File Offset: 0x0007596D
		[DefaultValue(0)]
		public int Top
		{
			get
			{
				object obj = base.ViewState["Top"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Top"] = value;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the right side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object.</summary>
		/// <returns>The x-coordinate of the right side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object. The default is 0.</returns>
		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x00077788 File Offset: 0x00075988
		// (set) Token: 0x06002D0F RID: 11535 RVA: 0x000777B1 File Offset: 0x000759B1
		[DefaultValue(0)]
		public int Right
		{
			get
			{
				object obj = base.ViewState["Right"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Right"] = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of the bottom side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object.</summary>
		/// <returns>The y-coordinate of the bottom side of the rectangular region defined by this <see cref="T:System.Web.UI.WebControls.RectangleHotSpot" /> object. The default is 0.</returns>
		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06002D10 RID: 11536 RVA: 0x000777CC File Offset: 0x000759CC
		// (set) Token: 0x06002D11 RID: 11537 RVA: 0x000777F5 File Offset: 0x000759F5
		[DefaultValue(0)]
		public int Bottom
		{
			get
			{
				object obj = base.ViewState["Bottom"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["Bottom"] = value;
			}
		}
	}
}
