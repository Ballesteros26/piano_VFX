using System;

namespace System.Web.UI
{
	/// <summary>Provides data for any events that occur when a user clicks an image-based ASP.NET server control, such as the <see cref="T:System.Web.UI.HtmlControls.HtmlInputImage" /> or <see cref="T:System.Web.UI.WebControls.ImageButton" /> server controls. This class cannot be inherited.</summary>
	// Token: 0x0200018B RID: 395
	public sealed class ImageClickEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ImageClickEventArgs" /> class using the <paramref name="x" /> and <paramref name="y" /> parameters.</summary>
		/// <param name="x">The x-coordinate where the user clicked an image-based ASP.NET server control. </param>
		/// <param name="y">The y-coordinate where the user clicked an image-based ASP.NET server control. </param>
		// Token: 0x06000FA6 RID: 4006 RVA: 0x0002B4F4 File Offset: 0x000296F4
		public ImageClickEventArgs(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ImageClickEventArgs" /> class using the <paramref name="x" />, <paramref name="y" />, <paramref name="xRaw" />, and <paramref name="yRaw" /> parameters.</summary>
		/// <param name="x">The x-coordinate where a user clicked an image-based ASP.NET server control.</param>
		/// <param name="y">The y-coordinate where a user clicked an image-based ASP.NET server control.</param>
		/// <param name="xRaw">The raw x-coordinate where a user clicked an image-based ASP.NET server control.</param>
		/// <param name="yRaw">The raw y-coordinate where a user clicked an image-based ASP.NET server control.</param>
		// Token: 0x06000FA7 RID: 4007 RVA: 0x0002B50A File Offset: 0x0002970A
		public ImageClickEventArgs(int x, int y, double xRaw, double yRaw)
		{
			this.X = x;
			this.Y = y;
			this.XRaw = xRaw;
			this.YRaw = yRaw;
		}

		/// <summary>An integer that represents the x-coordinate where a user clicked an image-based ASP.NET server control.</summary>
		// Token: 0x04001312 RID: 4882
		public int X;

		/// <summary>An integer that represents the y-coordinate where a user clicked an image-based ASP.NET server control.</summary>
		// Token: 0x04001313 RID: 4883
		public int Y;

		/// <summary>An integer that represents the raw x-coordinate where a user clicked an image-based ASP.NET server control.</summary>
		// Token: 0x04001314 RID: 4884
		public double XRaw;

		/// <summary>An integer that represents the raw y-coordinate where a user clicked an image-based ASP.NET server control.</summary>
		// Token: 0x04001315 RID: 4885
		public double YRaw;
	}
}
