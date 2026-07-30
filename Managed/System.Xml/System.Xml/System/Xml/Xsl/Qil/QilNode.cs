using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Xml.Xsl.Qil
{
	// Token: 0x02000639 RID: 1593
	internal class QilNode : IList<QilNode>, ICollection<QilNode>, IEnumerable<QilNode>, IEnumerable
	{
		// Token: 0x06003EC8 RID: 16072 RVA: 0x00157D70 File Offset: 0x00155F70
		public QilNode(QilNodeType nodeType)
		{
			this.nodeType = nodeType;
		}

		// Token: 0x06003EC9 RID: 16073 RVA: 0x00157D7F File Offset: 0x00155F7F
		public QilNode(QilNodeType nodeType, XmlQueryType xmlType)
		{
			this.nodeType = nodeType;
			this.xmlType = xmlType;
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06003ECA RID: 16074 RVA: 0x00157D95 File Offset: 0x00155F95
		// (set) Token: 0x06003ECB RID: 16075 RVA: 0x00157D9D File Offset: 0x00155F9D
		public QilNodeType NodeType
		{
			get
			{
				return this.nodeType;
			}
			set
			{
				this.nodeType = value;
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x06003ECC RID: 16076 RVA: 0x00157DA6 File Offset: 0x00155FA6
		// (set) Token: 0x06003ECD RID: 16077 RVA: 0x00157DAE File Offset: 0x00155FAE
		public virtual XmlQueryType XmlType
		{
			get
			{
				return this.xmlType;
			}
			set
			{
				this.xmlType = value;
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x06003ECE RID: 16078 RVA: 0x00157DB7 File Offset: 0x00155FB7
		// (set) Token: 0x06003ECF RID: 16079 RVA: 0x00157DBF File Offset: 0x00155FBF
		public ISourceLineInfo SourceLine
		{
			get
			{
				return this.sourceLine;
			}
			set
			{
				this.sourceLine = value;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x00157DC8 File Offset: 0x00155FC8
		// (set) Token: 0x06003ED1 RID: 16081 RVA: 0x00157DD0 File Offset: 0x00155FD0
		public object Annotation
		{
			get
			{
				return this.annotation;
			}
			set
			{
				this.annotation = value;
			}
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x00157DD9 File Offset: 0x00155FD9
		public virtual QilNode DeepClone(QilFactory f)
		{
			return new QilCloneVisitor(f).Clone(this);
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x00157DE7 File Offset: 0x00155FE7
		public virtual QilNode ShallowClone(QilFactory f)
		{
			return (QilNode)base.MemberwiseClone();
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06003ED4 RID: 16084 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000C9E RID: 3230
		public virtual QilNode this[int index]
		{
			get
			{
				throw new IndexOutOfRangeException();
			}
			set
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual void Insert(int index, QilNode node)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x00010C4A File Offset: 0x0000EE4A
		public virtual void RemoveAt(int index)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x00157DFB File Offset: 0x00155FFB
		public IEnumerator<QilNode> GetEnumerator()
		{
			return new IListEnumerator<QilNode>(this);
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x00157DFB File Offset: 0x00155FFB
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new IListEnumerator<QilNode>(this);
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06003EDB RID: 16091 RVA: 0x0000226C File Offset: 0x0000046C
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x00157E08 File Offset: 0x00156008
		public virtual void Add(QilNode node)
		{
			this.Insert(this.Count, node);
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x00157E18 File Offset: 0x00156018
		public virtual void Add(IList<QilNode> list)
		{
			for (int i = 0; i < list.Count; i++)
			{
				this.Insert(this.Count, list[i]);
			}
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x00157E4C File Offset: 0x0015604C
		public virtual void Clear()
		{
			for (int i = this.Count - 1; i >= 0; i--)
			{
				this.RemoveAt(i);
			}
		}

		// Token: 0x06003EDF RID: 16095 RVA: 0x00157E73 File Offset: 0x00156073
		public virtual bool Contains(QilNode node)
		{
			return this.IndexOf(node) != -1;
		}

		// Token: 0x06003EE0 RID: 16096 RVA: 0x00157E84 File Offset: 0x00156084
		public virtual void CopyTo(QilNode[] array, int index)
		{
			for (int i = 0; i < this.Count; i++)
			{
				array[index + i] = this[i];
			}
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x00157EB0 File Offset: 0x001560B0
		public virtual bool Remove(QilNode node)
		{
			int num = this.IndexOf(node);
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x00157ED4 File Offset: 0x001560D4
		public virtual int IndexOf(QilNode node)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (node.Equals(this[i]))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0400284D RID: 10317
		protected QilNodeType nodeType;

		// Token: 0x0400284E RID: 10318
		protected XmlQueryType xmlType;

		// Token: 0x0400284F RID: 10319
		protected ISourceLineInfo sourceLine;

		// Token: 0x04002850 RID: 10320
		protected object annotation;
	}
}
