using System;
using System.Collections;
using System.Data;
using System.Security.Permissions;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

namespace System.Web.Handlers
{
	/// <summary>Provides a synchronous HTTP handler that processes requests for tracing information.</summary>
	// Token: 0x02000108 RID: 264
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class TraceHandler : IHttpHandler
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Handlers.TraceHandler" /> class. </summary>
		// Token: 0x06000DAD RID: 3501 RVA: 0x00002050 File Offset: 0x00000250
		[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
		public TraceHandler()
		{
		}

		/// <summary>Processes an HTTP request.</summary>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides access to the current Request and Response instances.</param>
		// Token: 0x06000DAE RID: 3502 RVA: 0x000258C4 File Offset: 0x00023AC4
		void IHttpHandler.ProcessRequest(HttpContext context)
		{
			this.ProcessRequest(context);
		}

		/// <summary>Processes an HTTP request.</summary>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides access to the current Request and Response instances.</param>
		// Token: 0x06000DAF RID: 3503 RVA: 0x000258D0 File Offset: 0x00023AD0
		protected void ProcessRequest(HttpContext context)
		{
			TraceManager traceManager = HttpRuntime.TraceManager;
			if (!traceManager.Enabled || (traceManager.LocalOnly && !context.Request.IsLocal))
			{
				throw new TraceNotAvailableException(traceManager.Enabled);
			}
			HtmlTextWriter htmlTextWriter = new HtmlTextWriter(context.Response.Output);
			if (context.Request.QueryString["clear"] != null)
			{
				traceManager.Clear();
				context.Response.Redirect(context.Request.FilePath);
			}
			string text = context.Request.QueryString["id"];
			int num = -1;
			if (text != null)
			{
				num = int.Parse(text);
			}
			if (num > 0 && num <= traceManager.ItemCount)
			{
				this.RenderItem(traceManager, htmlTextWriter, num);
				return;
			}
			string text2 = context.Server.MapPath(UrlUtils.GetDirectory(context.Request.FilePath));
			this.RenderMenu(traceManager, htmlTextWriter, text2);
		}

		/// <summary>Gets a value indicating whether another request can use the <see cref="T:System.Web.Handlers.TraceHandler" /> instance.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x000259B0 File Offset: 0x00023BB0
		bool IHttpHandler.IsReusable
		{
			get
			{
				return this.IsReusable;
			}
		}

		/// <summary>Gets a value indicating whether another request can use the <see cref="T:System.Web.Handlers.TraceHandler" /> instance.</summary>
		/// <returns>false in all cases.</returns>
		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x00008A69 File Offset: 0x00006C69
		protected bool IsReusable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x000259B8 File Offset: 0x00023BB8
		private void RenderMenu(TraceManager manager, HtmlTextWriter output, string dir)
		{
			output.RenderBeginTag(HtmlTextWriterTag.Html);
			output.RenderBeginTag(HtmlTextWriterTag.Head);
			TraceData.RenderStyleSheet(output);
			output.RenderEndTag();
			this.RenderHeader(output, dir);
			output.RenderBeginTag(HtmlTextWriterTag.Body);
			output.AddAttribute("class", "tracecontent");
			output.RenderBeginTag(HtmlTextWriterTag.Span);
			Table table = TraceData.CreateTable();
			table.Rows.Add(TraceData.AltRow("Requests to the Application"));
			table.Rows.Add(TraceData.SubHeadRow(new string[] { "No", "Time of Request", "File", "Status Code", "Verb", "&nbsp;" }));
			if (manager.TraceData != null)
			{
				for (int i = 0; i < manager.ItemCount; i++)
				{
					int num = i + 1;
					TraceData traceData = manager.TraceData[i];
					TraceData.RenderAltRow(table, i, new string[]
					{
						num.ToString(),
						traceData.RequestTime.ToString(),
						traceData.RequestPath,
						traceData.StatusCode.ToString(),
						traceData.RequestType,
						"<a href=\"Trace.axd?id=" + num + "\" class=\"tinylink\"><b><nobr>View Details</a>"
					});
				}
				table.RenderControl(output);
			}
			output.RenderEndTag();
			output.RenderEndTag();
			output.RenderEndTag();
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00025B14 File Offset: 0x00023D14
		private void RenderHeader(HtmlTextWriter output, string dir)
		{
			Table table = TraceData.CreateTable();
			TableRow tableRow = new TableRow();
			TableRow tableRow2 = new TableRow();
			TableCell tableCell = new TableCell();
			TableCell tableCell2 = new TableCell();
			TableCell tableCell3 = new TableCell();
			TableCell tableCell4 = new TableCell();
			tableCell.Text = "<h1>Application Trace</h1>";
			tableCell2.Text = "[ <a href=\"Trace.axd?clear=1\" class=\"link\">clear current trace</a> ]";
			tableCell2.HorizontalAlign = HorizontalAlign.Right;
			tableCell2.VerticalAlign = VerticalAlign.Bottom;
			tableRow.Cells.Add(tableCell);
			tableRow.Cells.Add(tableCell2);
			tableCell3.Text = "<h2><h2><p>";
			tableCell4.Text = "<b>Physical Directory:</b>" + dir;
			tableRow2.Cells.Add(tableCell3);
			tableRow2.Cells.Add(tableCell4);
			table.Rows.Add(tableRow);
			table.Rows.Add(tableRow2);
			table.RenderControl(output);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00025BE4 File Offset: 0x00023DE4
		private void RenderItem(TraceManager manager, HtmlTextWriter output, int item)
		{
			manager.TraceData[item - 1].Render(output);
		}

		/// <summary>Writes the details of the current system state and page information to the response stream.</summary>
		/// <param name="data">A <see cref="T:System.Data.DataSet" /> object that contains tracing information.</param>
		// Token: 0x06000DB5 RID: 3509 RVA: 0x0000393A File Offset: 0x00001B3A
		[global::System.MonoLimitation("Not implemented, does nothing")]
		protected void ShowDetails(DataSet data)
		{
		}

		/// <summary>Writes the details of recent HTTP request traffic to the response stream.</summary>
		/// <param name="data">A set of <see cref="T:System.Data.DataSet" /> objects that represent the recent HTTP requests the server has processed.</param>
		// Token: 0x06000DB6 RID: 3510 RVA: 0x0000393A File Offset: 0x00001B3A
		[global::System.MonoLimitation("Not implemented, does nothing")]
		protected void ShowRequests(IList data)
		{
		}

		/// <summary>Writes the details of the current Common Language Runtime and ASP.NET build versions that the Web server is using.</summary>
		// Token: 0x06000DB7 RID: 3511 RVA: 0x0000393A File Offset: 0x00001B3A
		[global::System.MonoLimitation("Not implemented, does nothing")]
		protected void ShowVersionDetails()
		{
		}
	}
}
