using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Web.Routing
{
	// Token: 0x020004DD RID: 1245
	internal sealed class ContentPathSegment : PathSegment
	{
		// Token: 0x0600384A RID: 14410 RVA: 0x00097513 File Offset: 0x00095713
		public ContentPathSegment(IList<PathSubsegment> subsegments)
		{
			this.Subsegments = subsegments;
		}

		// Token: 0x17001194 RID: 4500
		// (get) Token: 0x0600384B RID: 14411 RVA: 0x00097522 File Offset: 0x00095722
		public bool IsCatchAll
		{
			get
			{
				return this.Subsegments.Any((PathSubsegment seg) => seg is ParameterSubsegment && ((ParameterSubsegment)seg).IsCatchAll);
			}
		}

		// Token: 0x17001195 RID: 4501
		// (get) Token: 0x0600384C RID: 14412 RVA: 0x0009754E File Offset: 0x0009574E
		// (set) Token: 0x0600384D RID: 14413 RVA: 0x00097556 File Offset: 0x00095756
		public IList<PathSubsegment> Subsegments { get; private set; }
	}
}
