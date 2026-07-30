using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Web.Services.Description
{
	/// <summary>Enumerates the elements in a <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</summary>
	// Token: 0x0200013C RID: 316
	public class BasicProfileViolationEnumerator : IEnumerator<BasicProfileViolation>, IDisposable, IEnumerator
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Description.BasicProfileViolationEnumerator" /> class.</summary>
		/// <param name="list">The <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" /> to be enumerated using this class.</param>
		// Token: 0x060009AE RID: 2478 RVA: 0x0004350F File Offset: 0x0004170F
		public BasicProfileViolationEnumerator(BasicProfileViolationCollection list)
		{
			this.list = list;
			this.idx = -1;
			this.end = list.Count - 1;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Web.Services.Description.BasicProfileViolationEnumerator" /> class.Releases all resources used by the <see cref="T:System.Web.Services.Description.BasicProfileViolationEnumerator" />. </summary>
		// Token: 0x060009AF RID: 2479 RVA: 0x0000210D File Offset: 0x0000030D
		public void Dispose()
		{
		}

		/// <summary>Enumerates to the next element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</summary>
		/// <returns>false if the end of the collection is reached; otherwise true.</returns>
		// Token: 0x060009B0 RID: 2480 RVA: 0x00043533 File Offset: 0x00041733
		public bool MoveNext()
		{
			if (this.idx >= this.end)
			{
				return false;
			}
			this.idx++;
			return true;
		}

		/// <summary>Gets the current <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> object representing the current element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</returns>
		// Token: 0x17000277 RID: 631
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x00043554 File Offset: 0x00041754
		public BasicProfileViolation Current
		{
			get
			{
				return this.list[this.idx];
			}
		}

		/// <summary>Gets the current element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />. </summary>
		/// <returns>The current element in the collection.</returns>
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x060009B2 RID: 2482 RVA: 0x00043554 File Offset: 0x00041754
		object IEnumerator.Current
		{
			get
			{
				return this.list[this.idx];
			}
		}

		/// <summary>Sets the enumerator to its initial position, which is before the first element in the <see cref="T:System.Web.Services.Description.BasicProfileViolationCollection" />.</summary>
		// Token: 0x060009B3 RID: 2483 RVA: 0x00043567 File Offset: 0x00041767
		void IEnumerator.Reset()
		{
			this.idx = -1;
		}

		// Token: 0x04000595 RID: 1429
		private BasicProfileViolationCollection list;

		// Token: 0x04000596 RID: 1430
		private int idx;

		// Token: 0x04000597 RID: 1431
		private int end;
	}
}
