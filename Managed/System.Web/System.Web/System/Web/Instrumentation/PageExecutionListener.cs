using System;
using Unity;

namespace System.Web.Instrumentation
{
	/// <summary>Provides methods that are called before and after a view engine renders output.</summary>
	// Token: 0x0200069F RID: 1695
	public abstract class PageExecutionListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Instrumentation.PageExecutionListener" /> class.</summary>
		// Token: 0x060047BF RID: 18367 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected PageExecutionListener()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Called by a view engine before it renders the output for the specified context.</summary>
		/// <param name="context">The page execution context.</param>
		// Token: 0x060047C0 RID: 18368
		public abstract void BeginContext(PageExecutionContext context);

		/// <summary>Called by a view engine after it renders the output for the specified context.</summary>
		/// <param name="context">The page execution context.</param>
		// Token: 0x060047C1 RID: 18369
		public abstract void EndContext(PageExecutionContext context);
	}
}
