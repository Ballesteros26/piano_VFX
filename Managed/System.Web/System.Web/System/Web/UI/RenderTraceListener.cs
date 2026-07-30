using System;
using System.Collections.Generic;
using System.IO;
using Unity;

namespace System.Web.UI
{
	/// <summary>Provides the abstract base class for an object that monitors as controls are rendering during a page request.</summary>
	// Token: 0x0200078C RID: 1932
	public abstract class RenderTraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.RenderTraceListener" /> class.</summary>
		// Token: 0x06004E50 RID: 20048 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected RenderTraceListener()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Gets a list of functions that are called to instantiate <see cref="T:System.Web.UI.RenderTraceListener" /> objects for each request.</summary>
		/// <returns>A list of functions that are called to instantiate <see cref="T:System.Web.UI.RenderTraceListener" /> objects for each request.</returns>
		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x06004E51 RID: 20049 RVA: 0x0000FAB7 File Offset: 0x0000DCB7
		public static IList<Func<RenderTraceListener>> ListenerFactories
		{
			get
			{
				global::Unity.ThrowStub.ThrowNotSupportedException();
				return 0;
			}
		}

		/// <summary>Called when an object is about to be rendered.</summary>
		/// <param name="writer">An object that can write a sequential series of characters.</param>
		/// <param name="renderedObject">The rendered object.</param>
		// Token: 0x06004E52 RID: 20050 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void BeginRendering(TextWriter writer, object renderedObject)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Called when an object is finished rendering.</summary>
		/// <param name="writer">An object that can write a sequential series of characters.</param>
		/// <param name="renderedObject">The rendered object.</param>
		// Token: 0x06004E53 RID: 20051 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void EndRendering(TextWriter writer, object renderedObject)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes the listener objects.</summary>
		/// <param name="context">The HTTP-specific information about an individual HTTP request.</param>
		// Token: 0x06004E54 RID: 20052 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void Initialize(HttpContext context)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Associates a data object with either the controls or other object that will be rendered.</summary>
		/// <param name="tracedObject">The object to be traced.</param>
		/// <param name="traceDataKey">The key field of a record in a data-bound control.</param>
		/// <param name="traceDataValue">The value of the extracted data.</param>
		// Token: 0x06004E55 RID: 20053 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void SetTraceData(object tracedObject, object traceDataKey, object traceDataValue)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Associates the trace data from one object to another object.</summary>
		/// <param name="source">The object from which to share trace data.</param>
		/// <param name="destination">The object to which trace data is shared. </param>
		// Token: 0x06004E56 RID: 20054 RVA: 0x0000B3E4 File Offset: 0x000095E4
		public virtual void ShareTraceData(object source, object destination)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
