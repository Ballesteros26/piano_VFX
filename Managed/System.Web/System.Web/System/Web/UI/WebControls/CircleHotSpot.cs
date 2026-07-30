using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines a circular hot spot region in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000350 RID: 848
	public sealed class CircleHotSpot : HotSpot
	{
		/// <summary>Returns a string that represents the x- and y-coordinates of a <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object's center and the length of its radius.</summary>
		/// <returns>A string that represents the x- and y-coordinates of a <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object's center and the length of its radius.</returns>
		// Token: 0x06001F63 RID: 8035 RVA: 0x0004F814 File Offset: 0x0004DA14
		public override string GetCoordinates()
		{
			return string.Concat(new object[] { this.X, ",", this.Y, ",", this.Radius });
		}

		// Token: 0x170009CC RID: 2508
		// (get) Token: 0x06001F64 RID: 8036 RVA: 0x0004F866 File Offset: 0x0004DA66
		protected internal override string MarkupName
		{
			get
			{
				return "circle";
			}
		}

		/// <summary>Gets or sets the distance from the center to the edge of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object.</summary>
		/// <returns>An integer that represents the distance in pixels from the center to the edge of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object. The default is 0.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified value is less than 0. </exception>
		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x0004F86D File Offset: 0x0004DA6D
		// (set) Token: 0x06001F66 RID: 8038 RVA: 0x0004F880 File Offset: 0x0004DA80
		[DefaultValue(0)]
		public int Radius
		{
			get
			{
				return base.ViewState.GetInt("Radius", 0);
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				base.ViewState["Radius"] = value;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the center of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object.</summary>
		/// <returns>The x-coordinate of the center of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object. The default is 0.</returns>
		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06001F67 RID: 8039 RVA: 0x0004F8A2 File Offset: 0x0004DAA2
		// (set) Token: 0x06001F68 RID: 8040 RVA: 0x0004F8B5 File Offset: 0x0004DAB5
		[DefaultValue(0)]
		public int X
		{
			get
			{
				return base.ViewState.GetInt("X", 0);
			}
			set
			{
				base.ViewState["X"] = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of the center of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object.</summary>
		/// <returns>The y-coordinate of the center of the circular region defined by this <see cref="T:System.Web.UI.WebControls.CircleHotSpot" /> object. The default is 0.</returns>
		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06001F69 RID: 8041 RVA: 0x0004F8CD File Offset: 0x0004DACD
		// (set) Token: 0x06001F6A RID: 8042 RVA: 0x0004F8E0 File Offset: 0x0004DAE0
		[DefaultValue(0)]
		public int Y
		{
			get
			{
				return base.ViewState.GetInt("Y", 0);
			}
			set
			{
				base.ViewState["Y"] = value;
			}
		}
	}
}
