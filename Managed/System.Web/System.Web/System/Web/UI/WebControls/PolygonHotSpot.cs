using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	/// <summary>Defines a polygon-shaped hot spot region in an <see cref="T:System.Web.UI.WebControls.ImageMap" /> control. This class cannot be inherited.</summary>
	// Token: 0x020003F5 RID: 1013
	public sealed class PolygonHotSpot : HotSpot
	{
		/// <summary>Returns a string that represents the coordinates of the vertexes of a <see cref="T:System.Web.UI.WebControls.PolygonHotSpot" /> object.</summary>
		/// <returns>A string that represents the coordinates of the vertexes of a <see cref="T:System.Web.UI.WebControls.PolygonHotSpot" /> object. The default value is an empty string ("").</returns>
		// Token: 0x06002CBA RID: 11450 RVA: 0x00076DCD File Offset: 0x00074FCD
		public override string GetCoordinates()
		{
			return this.Coordinates;
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06002CBB RID: 11451 RVA: 0x00076DD5 File Offset: 0x00074FD5
		protected internal override string MarkupName
		{
			get
			{
				return "poly";
			}
		}

		/// <summary>A string of coordinates that represents the vertexes of a <see cref="T:System.Web.UI.WebControls.PolygonHotSpot" /> object.</summary>
		/// <returns>A string that represents the coordinates of a <see cref="T:System.Web.UI.WebControls.PolygonHotSpot" /> object's vertexes.</returns>
		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x00076DDC File Offset: 0x00074FDC
		// (set) Token: 0x06002CBD RID: 11453 RVA: 0x00076E09 File Offset: 0x00075009
		[DefaultValue("")]
		public string Coordinates
		{
			get
			{
				object obj = base.ViewState["Coordinates"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["Coordinates"] = value;
			}
		}
	}
}
