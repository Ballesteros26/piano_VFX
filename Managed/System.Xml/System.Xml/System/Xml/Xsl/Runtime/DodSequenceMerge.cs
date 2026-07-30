using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.XPath;

namespace System.Xml.Xsl.Runtime
{
	// Token: 0x020005CF RID: 1487
	[EditorBrowsable(EditorBrowsableState.Never)]
	public struct DodSequenceMerge
	{
		// Token: 0x06003AE3 RID: 15075 RVA: 0x0014CBC2 File Offset: 0x0014ADC2
		public void Create(XmlQueryRuntime runtime)
		{
			this.firstSequence = null;
			this.sequencesToMerge = null;
			this.nodeCount = 0;
			this.runtime = runtime;
		}

		// Token: 0x06003AE4 RID: 15076 RVA: 0x0014CBE0 File Offset: 0x0014ADE0
		public void AddSequence(IList<XPathNavigator> sequence)
		{
			if (sequence.Count == 0)
			{
				return;
			}
			if (this.firstSequence == null)
			{
				this.firstSequence = sequence;
				return;
			}
			if (this.sequencesToMerge == null)
			{
				this.sequencesToMerge = new List<IEnumerator<XPathNavigator>>();
				this.MoveAndInsertSequence(this.firstSequence.GetEnumerator());
				this.nodeCount = this.firstSequence.Count;
			}
			this.MoveAndInsertSequence(sequence.GetEnumerator());
			this.nodeCount += sequence.Count;
		}

		// Token: 0x06003AE5 RID: 15077 RVA: 0x0014CC5C File Offset: 0x0014AE5C
		public IList<XPathNavigator> MergeSequences()
		{
			if (this.firstSequence == null)
			{
				return XmlQueryNodeSequence.Empty;
			}
			if (this.sequencesToMerge == null || this.sequencesToMerge.Count <= 1)
			{
				return this.firstSequence;
			}
			XmlQueryNodeSequence xmlQueryNodeSequence = new XmlQueryNodeSequence(this.nodeCount);
			while (this.sequencesToMerge.Count != 1)
			{
				IEnumerator<XPathNavigator> enumerator = this.sequencesToMerge[this.sequencesToMerge.Count - 1];
				this.sequencesToMerge.RemoveAt(this.sequencesToMerge.Count - 1);
				xmlQueryNodeSequence.Add(enumerator.Current);
				this.MoveAndInsertSequence(enumerator);
			}
			do
			{
				xmlQueryNodeSequence.Add(this.sequencesToMerge[0].Current);
			}
			while (this.sequencesToMerge[0].MoveNext());
			return xmlQueryNodeSequence;
		}

		// Token: 0x06003AE6 RID: 15078 RVA: 0x0014CD1F File Offset: 0x0014AF1F
		private void MoveAndInsertSequence(IEnumerator<XPathNavigator> sequence)
		{
			if (sequence.MoveNext())
			{
				this.InsertSequence(sequence);
			}
		}

		// Token: 0x06003AE7 RID: 15079 RVA: 0x0014CD30 File Offset: 0x0014AF30
		private void InsertSequence(IEnumerator<XPathNavigator> sequence)
		{
			for (int i = this.sequencesToMerge.Count - 1; i >= 0; i--)
			{
				int num = this.runtime.ComparePosition(sequence.Current, this.sequencesToMerge[i].Current);
				if (num == -1)
				{
					this.sequencesToMerge.Insert(i + 1, sequence);
					return;
				}
				if (num == 0 && !sequence.MoveNext())
				{
					return;
				}
			}
			this.sequencesToMerge.Insert(0, sequence);
		}

		// Token: 0x04002676 RID: 9846
		private IList<XPathNavigator> firstSequence;

		// Token: 0x04002677 RID: 9847
		private List<IEnumerator<XPathNavigator>> sequencesToMerge;

		// Token: 0x04002678 RID: 9848
		private int nodeCount;

		// Token: 0x04002679 RID: 9849
		private XmlQueryRuntime runtime;
	}
}
