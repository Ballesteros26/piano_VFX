using System;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000635 RID: 1589
	internal class QilList : QilNode
	{
		// Token: 0x06003EA5 RID: 16037 RVA: 0x00157917 File Offset: 0x00155B17
		public QilList(QilNodeType nodeType)
			: base(nodeType)
		{
			this.members = new QilNode[4];
			this.xmlType = null;
		}

		// Token: 0x17000C8F RID: 3215
		// (get) Token: 0x06003EA6 RID: 16038 RVA: 0x00157934 File Offset: 0x00155B34
		public override XmlQueryType XmlType
		{
			get
			{
				if (this.xmlType == null)
				{
					XmlQueryType xmlQueryType = XmlQueryTypeFactory.Empty;
					if (this.count > 0)
					{
						if (this.nodeType == QilNodeType.Sequence)
						{
							for (int i = 0; i < this.count; i++)
							{
								xmlQueryType = XmlQueryTypeFactory.Sequence(xmlQueryType, this.members[i].XmlType);
							}
						}
						else if (this.nodeType == QilNodeType.BranchList)
						{
							xmlQueryType = this.members[0].XmlType;
							for (int j = 1; j < this.count; j++)
							{
								xmlQueryType = XmlQueryTypeFactory.Choice(xmlQueryType, this.members[j].XmlType);
							}
						}
					}
					this.xmlType = xmlQueryType;
				}
				return this.xmlType;
			}
		}

		// Token: 0x06003EA7 RID: 16039 RVA: 0x001579DB File Offset: 0x00155BDB
		public override QilNode ShallowClone(QilFactory f)
		{
			QilList qilList = (QilList)base.MemberwiseClone();
			qilList.members = (QilNode[])this.members.Clone();
			return qilList;
		}

		// Token: 0x17000C90 RID: 3216
		// (get) Token: 0x06003EA8 RID: 16040 RVA: 0x001579FE File Offset: 0x00155BFE
		public override int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000C91 RID: 3217
		public override QilNode this[int index]
		{
			get
			{
				if (index >= 0 && index < this.count)
				{
					return this.members[index];
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (index >= 0 && index < this.count)
				{
					this.members[index] = value;
					this.xmlType = null;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x06003EAB RID: 16043 RVA: 0x00157A4C File Offset: 0x00155C4C
		public override void Insert(int index, QilNode node)
		{
			if (index < 0 || index > this.count)
			{
				throw new IndexOutOfRangeException();
			}
			if (this.count == this.members.Length)
			{
				QilNode[] array = new QilNode[this.count * 2];
				Array.Copy(this.members, array, this.count);
				this.members = array;
			}
			if (index < this.count)
			{
				Array.Copy(this.members, index, this.members, index + 1, this.count - index);
			}
			this.count++;
			this.members[index] = node;
			this.xmlType = null;
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x00157AE8 File Offset: 0x00155CE8
		public override void RemoveAt(int index)
		{
			if (index < 0 || index >= this.count)
			{
				throw new IndexOutOfRangeException();
			}
			this.count--;
			if (index < this.count)
			{
				Array.Copy(this.members, index + 1, this.members, index, this.count - index);
			}
			this.members[this.count] = null;
			this.xmlType = null;
		}

		// Token: 0x04002847 RID: 10311
		private int count;

		// Token: 0x04002848 RID: 10312
		private QilNode[] members;
	}
}
