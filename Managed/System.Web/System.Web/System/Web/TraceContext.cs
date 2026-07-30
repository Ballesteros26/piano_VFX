using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Web.UI;

namespace System.Web
{
	/// <summary>Captures and presents execution details about a Web request. This class cannot be inherited.</summary>
	// Token: 0x020000DE RID: 222
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TraceContext
	{
		/// <summary>Raised by the <see cref="T:System.Web.TraceContext" /> object to expose trace messages after all request information is gathered.</summary>
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000BF3 RID: 3059 RVA: 0x0001FDB4 File Offset: 0x0001DFB4
		// (remove) Token: 0x06000BF4 RID: 3060 RVA: 0x0001FDB4 File Offset: 0x0001DFB4
		public event TraceContextEventHandler TraceFinished
		{
			add
			{
				this.events.AddHandler(TraceContext.traceFinishedEvent, value);
			}
			remove
			{
				this.events.AddHandler(TraceContext.traceFinishedEvent, value);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.TraceContext" /> class.</summary>
		/// <param name="context">An <see cref="T:System.Web.HttpContext" /> that contains information about the current Web request. </param>
		// Token: 0x06000BF5 RID: 3061 RVA: 0x0001FDC7 File Offset: 0x0001DFC7
		public TraceContext(HttpContext context)
		{
			this._Context = context;
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000BF6 RID: 3062 RVA: 0x0001FDE8 File Offset: 0x0001DFE8
		internal bool HaveTrace
		{
			get
			{
				return this._haveTrace;
			}
		}

		/// <summary>Gets or sets a value indicating whether tracing is enabled for the current Web request.</summary>
		/// <returns>true if tracing is enabled; otherwise, false. </returns>
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x0001FDF0 File Offset: 0x0001DFF0
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x0001FE0C File Offset: 0x0001E00C
		public bool IsEnabled
		{
			get
			{
				if (!this._haveTrace)
				{
					return this.TraceManager.Enabled;
				}
				return this._Enabled;
			}
			set
			{
				if (value && this.data == null)
				{
					this.data = new TraceData();
				}
				this._haveTrace = true;
				this._Enabled = value;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x0001FE32 File Offset: 0x0001E032
		private TraceManager TraceManager
		{
			get
			{
				if (this._traceManager == null)
				{
					this._traceManager = HttpRuntime.TraceManager;
				}
				return this._traceManager;
			}
		}

		/// <summary>Gets or sets the sorted order in which trace messages should be output to a requesting browser.</summary>
		/// <returns>One of the <see cref="T:System.Web.TraceMode" /> enumeration values. The default is the setting specified by the traceMode attribute in the trace section of a configuration file, whose default is SortByTime.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is not one of the <see cref="T:System.Web.TraceMode" /> enumeration values.</exception>
		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000BFA RID: 3066 RVA: 0x0001FE4D File Offset: 0x0001E04D
		// (set) Token: 0x06000BFB RID: 3067 RVA: 0x0001FE6A File Offset: 0x0001E06A
		public TraceMode TraceMode
		{
			get
			{
				if (this._Mode != TraceMode.Default)
				{
					return this._Mode;
				}
				return this.TraceManager.TraceMode;
			}
			set
			{
				this._Mode = value;
			}
		}

		/// <summary>Writes a trace message to the trace log. All warnings appear in the log as red text.</summary>
		/// <param name="message">The trace message to write to the log. </param>
		// Token: 0x06000BFC RID: 3068 RVA: 0x0001FE73 File Offset: 0x0001E073
		public void Warn(string message)
		{
			this.Write(string.Empty, message, null, true);
		}

		/// <summary>Writes trace information to the trace log, including any user-defined categories and trace messages. All warnings appear in the log as red text.</summary>
		/// <param name="category">The trace category that receives the message. </param>
		/// <param name="message">The trace message to write to the log. </param>
		// Token: 0x06000BFD RID: 3069 RVA: 0x0001FE83 File Offset: 0x0001E083
		public void Warn(string category, string message)
		{
			this.Write(category, message, null, true);
		}

		/// <summary>Writes trace information to the trace log, including any user-defined categories, trace messages, and error information. All warnings appear in the log as red text.</summary>
		/// <param name="category">The trace category that receives the message. </param>
		/// <param name="message">The trace message to write to the log. </param>
		/// <param name="errorInfo">An <see cref="T:System.Exception" /> that contains information about the error. </param>
		// Token: 0x06000BFE RID: 3070 RVA: 0x0001FE8F File Offset: 0x0001E08F
		public void Warn(string category, string message, Exception errorInfo)
		{
			this.Write(category, message, errorInfo, true);
		}

		/// <summary>Writes a trace message to the trace log.</summary>
		/// <param name="message">The trace message to write to the log. </param>
		// Token: 0x06000BFF RID: 3071 RVA: 0x0001FE9B File Offset: 0x0001E09B
		public void Write(string message)
		{
			this.Write(string.Empty, message, null, false);
		}

		/// <summary>Writes trace information to the trace log, including a message and any user-defined categories.</summary>
		/// <param name="category">The trace category that receives the message. </param>
		/// <param name="message">The trace message to write to the log. </param>
		// Token: 0x06000C00 RID: 3072 RVA: 0x0001FEAB File Offset: 0x0001E0AB
		public void Write(string category, string message)
		{
			this.Write(category, message, null, false);
		}

		/// <summary>Writes trace information to the trace log, including any user-defined categories, trace messages, and error information.</summary>
		/// <param name="category">The trace category that receives the message. </param>
		/// <param name="message">The trace message to write to the log. </param>
		/// <param name="errorInfo">An <see cref="T:System.Exception" /> that contains information about the error. </param>
		// Token: 0x06000C01 RID: 3073 RVA: 0x0001FEB7 File Offset: 0x0001E0B7
		public void Write(string category, string message, Exception errorInfo)
		{
			this.Write(category, message, errorInfo, false);
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0001FEC3 File Offset: 0x0001E0C3
		private void Write(string category, string msg, Exception error, bool Warning)
		{
			if (!this.IsEnabled)
			{
				return;
			}
			if (this.data == null)
			{
				this.data = new TraceData();
			}
			this.data.Write(category, msg, error, Warning);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0001FEF4 File Offset: 0x0001E0F4
		internal void SaveData()
		{
			if (this.data == null)
			{
				this.data = new TraceData();
			}
			this.data.TraceMode = this._Context.Trace.TraceMode;
			this.SetRequestDetails();
			if (this._Context.Handler is Page)
			{
				this.data.AddControlTree((Page)this._Context.Handler, this.view_states, this.control_states, this.sizes);
			}
			this.AddCookies();
			this.AddHeaders();
			this.AddServerVars();
			this.TraceManager.AddTraceData(this.data);
			this.data_saved = true;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x0001FF9E File Offset: 0x0001E19E
		internal void SaveViewState(Control ctrl, object vs)
		{
			if (this.view_states == null)
			{
				this.view_states = new Hashtable();
			}
			this.view_states[ctrl] = vs;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0001FFC0 File Offset: 0x0001E1C0
		internal void SaveControlState(Control ctrl, object vs)
		{
			if (this.control_states == null)
			{
				this.control_states = new Hashtable();
			}
			this.control_states[ctrl] = vs;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0001FFE2 File Offset: 0x0001E1E2
		internal void SaveSize(Control ctrl, int size)
		{
			if (this.sizes == null)
			{
				this.sizes = new Hashtable();
			}
			this.sizes[ctrl] = size;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00020009 File Offset: 0x0001E209
		internal void Render(HtmlTextWriter output)
		{
			if (!this.data_saved)
			{
				this.SaveData();
			}
			this.data.Render(output);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00020028 File Offset: 0x0001E228
		private void SetRequestDetails()
		{
			this.data.RequestPath = this._Context.Request.FilePath;
			this.data.SessionID = ((this._Context.Session != null) ? this._Context.Session.SessionID : string.Empty);
			this.data.RequestType = this._Context.Request.RequestType;
			this.data.RequestTime = this._Context.Timestamp;
			this.data.StatusCode = this._Context.Response.StatusCode;
			this.data.RequestEncoding = this._Context.Request.ContentEncoding;
			this.data.ResponseEncoding = this._Context.Response.ContentEncoding;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00020104 File Offset: 0x0001E304
		private void AddCookies()
		{
			foreach (object obj in this._Context.Request.Cookies.Keys)
			{
				string text = (string)obj;
				this.data.AddCookie(text, this._Context.Request.Cookies[text].Value);
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x0002018C File Offset: 0x0001E38C
		private void AddHeaders()
		{
			foreach (object obj in this._Context.Request.Headers.Keys)
			{
				string text = (string)obj;
				this.data.AddHeader(text, this._Context.Request.Headers[text]);
			}
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00020210 File Offset: 0x0001E410
		private void AddServerVars()
		{
			foreach (object obj in this._Context.Request.ServerVariables)
			{
				string text = (string)obj;
				this.data.AddServerVar(text, this._Context.Request.ServerVariables[text]);
			}
		}

		// Token: 0x040010BD RID: 4285
		private static readonly object traceFinishedEvent = new object();

		// Token: 0x040010BE RID: 4286
		private HttpContext _Context;

		// Token: 0x040010BF RID: 4287
		private TraceManager _traceManager;

		// Token: 0x040010C0 RID: 4288
		private bool _Enabled;

		// Token: 0x040010C1 RID: 4289
		private TraceMode _Mode = TraceMode.Default;

		// Token: 0x040010C2 RID: 4290
		private TraceData data;

		// Token: 0x040010C3 RID: 4291
		private bool data_saved;

		// Token: 0x040010C4 RID: 4292
		private bool _haveTrace;

		// Token: 0x040010C5 RID: 4293
		private Hashtable view_states;

		// Token: 0x040010C6 RID: 4294
		private Hashtable control_states;

		// Token: 0x040010C7 RID: 4295
		private Hashtable sizes;

		// Token: 0x040010C8 RID: 4296
		private EventHandlerList events = new EventHandlerList();
	}
}
