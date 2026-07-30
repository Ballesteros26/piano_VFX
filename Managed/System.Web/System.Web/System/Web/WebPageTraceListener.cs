using System;
using System.Diagnostics;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web
{
	/// <summary>Provides a listener that directs <see cref="T:System.Diagnostics.Trace" /> messages to ASP.NET Web page outputs. </summary>
	// Token: 0x020000EB RID: 235
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class WebPageTraceListener : TraceListener
	{
		/// <summary>Writes an event message to a Web page or to the ASP.NET trace viewer using the specified system and event data.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process and  thread IDs and stack trace information.</param>
		/// <param name="source">A category name used to organize the output. </param>
		/// <param name="severity">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">A message to write.</param>
		// Token: 0x06000C99 RID: 3225 RVA: 0x00022175 File Offset: 0x00020375
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string message)
		{
			if (HttpContext.Current == null || HttpContext.Current.Trace == null)
			{
				return;
			}
			HttpContext.Current.Trace.Write(source, message);
		}

		/// <summary>Writes a localized event message to a Web page or to the ASP.NET trace viewer using the specified system and event data.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> that contains the current process and thread IDs and stack trace information.</param>
		/// <param name="source">A category name used to organize the output. </param>
		/// <param name="severity">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A format string that contains zero or more format items, which correspond to objects in <paramref name="args" />.</param>
		/// <param name="args">An array of zero or more objects to format.</param>
		// Token: 0x06000C9A RID: 3226 RVA: 0x0002219D File Offset: 0x0002039D
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType severity, int id, string format, params object[] args)
		{
			this.TraceEvent(eventCache, source, severity, id, string.Format(Helpers.InvariantCulture, format, args));
		}

		/// <summary>Writes a message to a Web page or to the ASP.NET trace viewer.</summary>
		/// <param name="message">The message to write.</param>
		// Token: 0x06000C9B RID: 3227 RVA: 0x000221B8 File Offset: 0x000203B8
		public override void Write(string message)
		{
			if (HttpContext.Current == null || HttpContext.Current.Trace == null)
			{
				return;
			}
			HttpContext.Current.Trace.Write(message);
		}

		/// <summary>Writes a category name and a message to a Web page or to the ASP.NET trace viewer.</summary>
		/// <param name="message">The message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06000C9C RID: 3228 RVA: 0x000221DE File Offset: 0x000203DE
		public override void Write(string message, string category)
		{
			if (HttpContext.Current == null || HttpContext.Current.Trace == null)
			{
				return;
			}
			HttpContext.Current.Trace.Write(category, message);
		}

		/// <summary>Writes a message to a Web page or to the ASP.NET trace viewer.</summary>
		/// <param name="message">The message to write.</param>
		// Token: 0x06000C9D RID: 3229 RVA: 0x000221B8 File Offset: 0x000203B8
		public override void WriteLine(string message)
		{
			if (HttpContext.Current == null || HttpContext.Current.Trace == null)
			{
				return;
			}
			HttpContext.Current.Trace.Write(message);
		}

		/// <summary>Writes a category name and a message to a Web page or to the ASP.NET trace viewer.</summary>
		/// <param name="message">The message to write.</param>
		/// <param name="category">A category name used to organize the output.</param>
		// Token: 0x06000C9E RID: 3230 RVA: 0x000221DE File Offset: 0x000203DE
		public override void WriteLine(string message, string category)
		{
			if (HttpContext.Current == null || HttpContext.Current.Trace == null)
			{
				return;
			}
			HttpContext.Current.Trace.Write(category, message);
		}
	}
}
