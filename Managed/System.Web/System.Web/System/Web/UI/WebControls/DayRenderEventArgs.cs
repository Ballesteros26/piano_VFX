using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Calendar.DayRender" /> event of the <see cref="T:System.Web.UI.WebControls.Calendar" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000386 RID: 902
	public sealed class DayRenderEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DayRenderEventArgs" /> class using the specified cell and calendar day.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents a cell in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</param>
		/// <param name="day">A <see cref="T:System.Web.UI.WebControls.CalendarDay" /> that represents the day to render in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</param>
		// Token: 0x060022CE RID: 8910 RVA: 0x00059DBF File Offset: 0x00057FBF
		public DayRenderEventArgs(TableCell cell, CalendarDay day)
		{
			this.cell = cell;
			this.day = day;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DayRenderEventArgs" /> class using the specified cell, calendar day, and selection URL.</summary>
		/// <param name="cell">A <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents a cell in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</param>
		/// <param name="day">A <see cref="T:System.Web.UI.WebControls.CalendarDay" /> that represents the day to render in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</param>
		/// <param name="selectUrl">The script used to post the page back to the server when the user selects the date being rendered.</param>
		// Token: 0x060022CF RID: 8911 RVA: 0x00059DD5 File Offset: 0x00057FD5
		public DayRenderEventArgs(TableCell cell, CalendarDay day, string selectUrl)
			: this(cell, day)
		{
			this._selectUrl = selectUrl;
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.TableCell" /> object that represents the cell being rendered in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.TableCell" /> that represents the cell being rendered in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060022D0 RID: 8912 RVA: 0x00059DE6 File Offset: 0x00057FE6
		public TableCell Cell
		{
			get
			{
				return this.cell;
			}
		}

		/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.CalendarDay" /> object that represents the day being rendered in the <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.CalendarDay" /> that represents the day being rendered in the <see cref="T:System.Web.UI.WebControls.Calendar" />.</returns>
		// Token: 0x17000B03 RID: 2819
		// (get) Token: 0x060022D1 RID: 8913 RVA: 0x00059DEE File Offset: 0x00057FEE
		public CalendarDay Day
		{
			get
			{
				return this.day;
			}
		}

		/// <summary>Gets the script used to post the page back to the server when the date being rendered is selected in a <see cref="T:System.Web.UI.WebControls.Calendar" /> control.</summary>
		/// <returns>The script used to post the page back to the server when the date being rendered is selected.</returns>
		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060022D2 RID: 8914 RVA: 0x00059DF6 File Offset: 0x00057FF6
		public string SelectUrl
		{
			get
			{
				return this._selectUrl;
			}
		}

		// Token: 0x0400193D RID: 6461
		private TableCell cell;

		// Token: 0x0400193E RID: 6462
		private CalendarDay day;

		// Token: 0x0400193F RID: 6463
		private string _selectUrl;
	}
}
