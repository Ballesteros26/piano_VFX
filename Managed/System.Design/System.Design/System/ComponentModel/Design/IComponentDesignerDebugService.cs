using System;
using System.Diagnostics;

namespace System.ComponentModel.Design
{
	/// <summary>Provides debugging services in a design-time environment.</summary>
	// Token: 0x02000127 RID: 295
	public interface IComponentDesignerDebugService
	{
		/// <summary>Asserts on a condition inside a design-time environment.</summary>
		/// <param name="condition">true to prevent <paramref name="message" /> from being displayed; otherwise, false.</param>
		/// <param name="message">The message to display.</param>
		// Token: 0x060008C6 RID: 2246
		void Assert(bool condition, string message);

		/// <summary>Logs a failure message inside a design-time environment.</summary>
		/// <param name="message">The message to log.</param>
		// Token: 0x060008C7 RID: 2247
		void Fail(string message);

		/// <summary>Logs a debug message inside a design-time environment.</summary>
		/// <param name="message">The message to log.</param>
		/// <param name="category">The category of <paramref name="message" />.</param>
		// Token: 0x060008C8 RID: 2248
		void Trace(string message, string category);

		/// <summary>Gets or sets the indent level for debug output.</summary>
		/// <returns>The indent level for debug output.</returns>
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060008C9 RID: 2249
		// (set) Token: 0x060008CA RID: 2250
		int IndentLevel { get; set; }

		/// <summary>Gets a collection of trace listeners for monitoring design-time debugging output.</summary>
		/// <returns>A collection of trace listeners </returns>
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060008CB RID: 2251
		TraceListenerCollection Listeners { get; }
	}
}
