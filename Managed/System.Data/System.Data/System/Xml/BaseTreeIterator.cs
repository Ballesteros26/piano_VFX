using System;

namespace System.Xml
{
	// Token: 0x0200002A RID: 42
	internal abstract class BaseTreeIterator
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00005E4B File Offset: 0x0000404B
		internal BaseTreeIterator(DataSetMapper mapper)
		{
			this.mapper = mapper;
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000CA RID: 202
		internal abstract XmlNode CurrentNode { get; }

		// Token: 0x060000CB RID: 203
		internal abstract bool Next();

		// Token: 0x060000CC RID: 204
		internal abstract bool NextRight();

		// Token: 0x060000CD RID: 205 RVA: 0x00005E5A File Offset: 0x0000405A
		internal bool NextRowElement()
		{
			while (this.Next())
			{
				if (this.OnRowElement())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005E71 File Offset: 0x00004071
		internal bool NextRightRowElement()
		{
			return this.NextRight() && (this.OnRowElement() || this.NextRowElement());
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005E90 File Offset: 0x00004090
		internal bool OnRowElement()
		{
			XmlBoundElement xmlBoundElement = this.CurrentNode as XmlBoundElement;
			return xmlBoundElement != null && xmlBoundElement.Row != null;
		}

		// Token: 0x0400040A RID: 1034
		protected DataSetMapper mapper;
	}
}
