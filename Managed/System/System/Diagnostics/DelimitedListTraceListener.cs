using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output to a text writer, such as a stream writer, or to a stream, such as a file stream.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020001AD RID: 429
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class DelimitedListTraceListener : TextWriterTraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DelimitedListTraceListener" /> class that writes to the specified output stream. </summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> to receive the output.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06000CA0 RID: 3232 RVA: 0x0003D5E2 File Offset: 0x0003B7E2
		public DelimitedListTraceListener(Stream stream)
			: base(stream)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DelimitedListTraceListener" /> class that writes to the specified output stream and has the specified name. </summary>
		/// <param name="stream">The <see cref="T:System.IO.Stream" /> to receive the output.</param>
		/// <param name="name">The name of the new instance of the trace listener. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="stream" /> is null. </exception>
		// Token: 0x06000CA1 RID: 3233 RVA: 0x0003D601 File Offset: 0x0003B801
		public DelimitedListTraceListener(Stream stream, string name)
			: base(stream, name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DelimitedListTraceListener" /> class that writes to the specified text writer. </summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to receive the output.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is null. </exception>
		// Token: 0x06000CA2 RID: 3234 RVA: 0x0003D621 File Offset: 0x0003B821
		public DelimitedListTraceListener(TextWriter writer)
			: base(writer)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DelimitedListTraceListener" /> class that writes to the specified text writer and has the specified name. </summary>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to receive the output.</param>
		/// <param name="name">The name of the new instance of the trace listener. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="writer" /> is null. </exception>
		// Token: 0x06000CA3 RID: 3235 RVA: 0x0003D640 File Offset: 0x0003B840
		public DelimitedListTraceListener(TextWriter writer, string name)
			: base(writer, name)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DelimitedListTraceListener" /> class that writes to the specified file.  </summary>
		/// <param name="fileName">The name of the file to receive the output.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null. </exception>
		// Token: 0x06000CA4 RID: 3236 RVA: 0x0003D660 File Offset: 0x0003B860
		public DelimitedListTraceListener(string fileName)
			: base(fileName)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DelimitedListTraceListener" /> class that writes to the specified file and has the specified name. </summary>
		/// <param name="fileName">The name of the file to receive the output. </param>
		/// <param name="name">The name of the new instance of the trace listener. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fileName" /> is null. </exception>
		// Token: 0x06000CA5 RID: 3237 RVA: 0x0003D67F File Offset: 0x0003B87F
		public DelimitedListTraceListener(string fileName, string name)
			: base(fileName, name)
		{
		}

		/// <summary>Gets or sets the delimiter for the delimited list.</summary>
		/// <returns>The delimiter for the delimited list.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.DelimitedListTraceListener.Delimiter" /> is set to null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <see cref="P:System.Diagnostics.DelimitedListTraceListener.Delimiter" /> is set to an empty string ("").</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000CA6 RID: 3238 RVA: 0x0003D6A0 File Offset: 0x0003B8A0
		// (set) Token: 0x06000CA7 RID: 3239 RVA: 0x0003D714 File Offset: 0x0003B914
		public string Delimiter
		{
			get
			{
				lock (this)
				{
					if (!this.initializedDelim)
					{
						if (base.Attributes.ContainsKey("delimiter"))
						{
							this.delimiter = base.Attributes["delimiter"];
						}
						this.initializedDelim = true;
					}
				}
				return this.delimiter;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Delimiter");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(global::SR.GetString("Generic_ArgCantBeEmptyString", new object[] { "Delimiter" }));
				}
				lock (this)
				{
					this.delimiter = value;
					this.initializedDelim = true;
				}
				if (this.delimiter == ",")
				{
					this.secondaryDelim = ";";
					return;
				}
				this.secondaryDelim = ",";
			}
		}

		/// <summary>Returns the custom configuration file attribute supported by the delimited trace listener.</summary>
		/// <returns>A string array that contains the single value "delimiter".</returns>
		// Token: 0x06000CA8 RID: 3240 RVA: 0x0003D7B4 File Offset: 0x0003B9B4
		protected internal override string[] GetSupportedAttributes()
		{
			return new string[] { "delimiter" };
		}

		/// <summary>Writes trace information, a formatted array of objects, and event information to the output file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="format">A format string that contains zero or more format items that correspond to objects in the <paramref name="args" /> array.</param>
		/// <param name="args">An array containing zero or more objects to format.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CA9 RID: 3241 RVA: 0x0003D7C4 File Offset: 0x0003B9C4
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			if (args != null)
			{
				this.WriteEscaped(string.Format(CultureInfo.InvariantCulture, format, args));
			}
			else
			{
				this.WriteEscaped(format);
			}
			this.Write(this.Delimiter);
			this.Write(this.Delimiter);
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, a message, and event information to the output file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="message">The trace message to write to the output file or stream.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CAA RID: 3242 RVA: 0x0003D83C File Offset: 0x0003BA3C
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, message))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			this.WriteEscaped(message);
			this.Write(this.Delimiter);
			this.Write(this.Delimiter);
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, a data object, and event information to the output file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">A data object to write to the output file or stream.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CAB RID: 3243 RVA: 0x0003D898 File Offset: 0x0003BA98
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			this.Write(this.Delimiter);
			this.WriteEscaped(data.ToString());
			this.Write(this.Delimiter);
			this.WriteFooter(eventCache);
		}

		/// <summary>Writes trace information, an array of data objects, and event information to the output file or stream.</summary>
		/// <param name="eventCache">A <see cref="T:System.Diagnostics.TraceEventCache" /> object that contains the current process ID, thread ID, and stack trace information.</param>
		/// <param name="source">A name used to identify the output, typically the name of the application that generated the trace event.</param>
		/// <param name="eventType">One of the <see cref="T:System.Diagnostics.TraceEventType" /> values specifying the type of event that has caused the trace.</param>
		/// <param name="id">A numeric identifier for the event.</param>
		/// <param name="data">An array of data objects to write to the output file or stream.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000CAC RID: 3244 RVA: 0x0003D8FC File Offset: 0x0003BAFC
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			this.Write(this.Delimiter);
			if (data != null)
			{
				for (int i = 0; i < data.Length; i++)
				{
					if (i != 0)
					{
						this.Write(this.secondaryDelim);
					}
					this.WriteEscaped(data[i].ToString());
				}
			}
			this.Write(this.Delimiter);
			this.WriteFooter(eventCache);
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0003D984 File Offset: 0x0003BB84
		private void WriteHeader(string source, TraceEventType eventType, int id)
		{
			this.WriteEscaped(source);
			this.Write(this.Delimiter);
			this.Write(eventType.ToString());
			this.Write(this.Delimiter);
			this.Write(id.ToString(CultureInfo.InvariantCulture));
			this.Write(this.Delimiter);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0003D9E4 File Offset: 0x0003BBE4
		private void WriteFooter(TraceEventCache eventCache)
		{
			if (eventCache != null)
			{
				if (base.IsEnabled(TraceOptions.ProcessId))
				{
					this.Write(eventCache.ProcessId.ToString(CultureInfo.InvariantCulture));
				}
				this.Write(this.Delimiter);
				if (base.IsEnabled(TraceOptions.LogicalOperationStack))
				{
					this.WriteStackEscaped(eventCache.LogicalOperationStack);
				}
				this.Write(this.Delimiter);
				if (base.IsEnabled(TraceOptions.ThreadId))
				{
					this.WriteEscaped(eventCache.ThreadId.ToString(CultureInfo.InvariantCulture));
				}
				this.Write(this.Delimiter);
				if (base.IsEnabled(TraceOptions.DateTime))
				{
					this.WriteEscaped(eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
				}
				this.Write(this.Delimiter);
				if (base.IsEnabled(TraceOptions.Timestamp))
				{
					this.Write(eventCache.Timestamp.ToString(CultureInfo.InvariantCulture));
				}
				this.Write(this.Delimiter);
				if (base.IsEnabled(TraceOptions.Callstack))
				{
					this.WriteEscaped(eventCache.Callstack);
				}
			}
			else
			{
				for (int i = 0; i < 5; i++)
				{
					this.Write(this.Delimiter);
				}
			}
			this.WriteLine("");
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0003DB10 File Offset: 0x0003BD10
		private void WriteEscaped(string message)
		{
			if (!string.IsNullOrEmpty(message))
			{
				StringBuilder stringBuilder = new StringBuilder("\"");
				int num = 0;
				int num2;
				while ((num2 = message.IndexOf('"', num)) != -1)
				{
					stringBuilder.Append(message, num, num2 - num);
					stringBuilder.Append("\"\"");
					num = num2 + 1;
				}
				stringBuilder.Append(message, num, message.Length - num);
				stringBuilder.Append("\"");
				this.Write(stringBuilder.ToString());
			}
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0003DB88 File Offset: 0x0003BD88
		private void WriteStackEscaped(Stack stack)
		{
			StringBuilder stringBuilder = new StringBuilder("\"");
			bool flag = true;
			foreach (object obj in stack)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				else
				{
					flag = false;
				}
				string text = obj.ToString();
				int num = 0;
				int num2;
				while ((num2 = text.IndexOf('"', num)) != -1)
				{
					stringBuilder.Append(text, num, num2 - num);
					stringBuilder.Append("\"\"");
					num = num2 + 1;
				}
				stringBuilder.Append(text, num, text.Length - num);
			}
			stringBuilder.Append("\"");
			this.Write(stringBuilder.ToString());
		}

		// Token: 0x0400100E RID: 4110
		private string delimiter = ";";

		// Token: 0x0400100F RID: 4111
		private string secondaryDelim = ",";

		// Token: 0x04001010 RID: 4112
		private bool initializedDelim;
	}
}
