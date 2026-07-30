using System;
using MS.Internal.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x02000386 RID: 902
	internal class DoubleLinkAxis : Axis
	{
		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x0600247F RID: 9343 RVA: 0x000DE078 File Offset: 0x000DC278
		// (set) Token: 0x06002480 RID: 9344 RVA: 0x000DE080 File Offset: 0x000DC280
		internal Axis Next
		{
			get
			{
				return this.next;
			}
			set
			{
				this.next = value;
			}
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x000DE08C File Offset: 0x000DC28C
		internal DoubleLinkAxis(Axis axis, DoubleLinkAxis inputaxis)
			: base(axis.TypeOfAxis, inputaxis, axis.Prefix, axis.Name, axis.NodeType)
		{
			this.next = null;
			base.Urn = axis.Urn;
			this.abbrAxis = axis.AbbrAxis;
			if (inputaxis != null)
			{
				inputaxis.Next = this;
			}
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x000DE0E1 File Offset: 0x000DC2E1
		internal static DoubleLinkAxis ConvertTree(Axis axis)
		{
			if (axis == null)
			{
				return null;
			}
			return new DoubleLinkAxis(axis, DoubleLinkAxis.ConvertTree((Axis)axis.Input));
		}

		// Token: 0x040018D2 RID: 6354
		internal Axis next;
	}
}
