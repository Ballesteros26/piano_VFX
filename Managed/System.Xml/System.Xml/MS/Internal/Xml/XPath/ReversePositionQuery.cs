using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200003F RID: 63
	internal sealed class ReversePositionQuery : ForwardPositionQuery
	{
		// Token: 0x060001AB RID: 427 RVA: 0x00006AD3 File Offset: 0x00004CD3
		public ReversePositionQuery(Query input)
			: base(input)
		{
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00006ADC File Offset: 0x00004CDC
		private ReversePositionQuery(ReversePositionQuery other)
			: base(other)
		{
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00006AE5 File Offset: 0x00004CE5
		public override XPathNodeIterator Clone()
		{
			return new ReversePositionQuery(this);
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006AED File Offset: 0x00004CED
		public override int CurrentPosition
		{
			get
			{
				return this.outputBuffer.Count - this.count + 1;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00006B03 File Offset: 0x00004D03
		public override QueryProps Properties
		{
			get
			{
				return base.Properties | QueryProps.Reverse;
			}
		}
	}
}
