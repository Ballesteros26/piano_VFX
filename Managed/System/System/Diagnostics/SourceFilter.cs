using System;

namespace System.Diagnostics
{
	/// <summary>Indicates whether a listener should trace a message based on the source of a trace.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001B8 RID: 440
	public class SourceFilter : TraceFilter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SourceFilter" /> class, specifying the name of the trace source. </summary>
		/// <param name="source">The name of the trace source.</param>
		// Token: 0x06000D02 RID: 3330 RVA: 0x0003ECAD File Offset: 0x0003CEAD
		public SourceFilter(string source)
		{
			this.Source = source;
		}

		/// <summary>Determines whether the trace listener should trace the event.</summary>
		/// <returns>true if the trace should be produced; otherwise, false. </returns>
		/// <param name="cache">An object that represents the information cache for the trace event.</param>
		/// <param name="source">The name of the source.</param>
		/// <param name="eventType">One of the enumeration values that identifies the event type. </param>
		/// <param name="id">A trace identifier number.</param>
		/// <param name="formatOrMessage">The format to use for writing an array of arguments or a message to write.</param>
		/// <param name="args">An array of argument objects.</param>
		/// <param name="data1">A trace data object.</param>
		/// <param name="data">An array of trace data objects.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000D03 RID: 3331 RVA: 0x0003ECBC File Offset: 0x0003CEBC
		public override bool ShouldTrace(TraceEventCache cache, string source, TraceEventType eventType, int id, string formatOrMessage, object[] args, object data1, object[] data)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return string.Equals(this.src, source);
		}

		/// <summary>Gets or sets the name of the trace source.</summary>
		/// <returns>The name of the trace source.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0003ECD8 File Offset: 0x0003CED8
		// (set) Token: 0x06000D05 RID: 3333 RVA: 0x0003ECE0 File Offset: 0x0003CEE0
		public string Source
		{
			get
			{
				return this.src;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("source");
				}
				this.src = value;
			}
		}

		// Token: 0x04001028 RID: 4136
		private string src;
	}
}
