using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000392 RID: 914
	internal class SelectorActiveAxis : ActiveAxis
	{
		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x060024EE RID: 9454 RVA: 0x000DF803 File Offset: 0x000DDA03
		public bool EmptyStack
		{
			get
			{
				return this.KSpointer == 0;
			}
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x060024EF RID: 9455 RVA: 0x000DF80E File Offset: 0x000DDA0E
		public int lastDepth
		{
			get
			{
				if (this.KSpointer != 0)
				{
					return ((KSStruct)this.KSs[this.KSpointer - 1]).depth;
				}
				return -1;
			}
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x000DF837 File Offset: 0x000DDA37
		public SelectorActiveAxis(Asttree axisTree, ConstraintStruct cs)
			: base(axisTree)
		{
			this.KSs = new ArrayList();
			this.cs = cs;
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x000DF852 File Offset: 0x000DDA52
		public override bool EndElement(string localname, string URN)
		{
			base.EndElement(localname, URN);
			return this.KSpointer > 0 && base.CurrentDepth == this.lastDepth;
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x000DF878 File Offset: 0x000DDA78
		public int PushKS(int errline, int errcol)
		{
			KeySequence keySequence = new KeySequence(this.cs.TableDim, errline, errcol);
			KSStruct ksstruct;
			if (this.KSpointer < this.KSs.Count)
			{
				ksstruct = (KSStruct)this.KSs[this.KSpointer];
				ksstruct.ks = keySequence;
				for (int i = 0; i < this.cs.TableDim; i++)
				{
					ksstruct.fields[i].Reactivate(keySequence);
				}
			}
			else
			{
				ksstruct = new KSStruct(keySequence, this.cs.TableDim);
				for (int j = 0; j < this.cs.TableDim; j++)
				{
					ksstruct.fields[j] = new LocatedActiveAxis(this.cs.constraint.Fields[j], keySequence, j);
					this.cs.axisFields.Add(ksstruct.fields[j]);
				}
				this.KSs.Add(ksstruct);
			}
			ksstruct.depth = base.CurrentDepth - 1;
			int kspointer = this.KSpointer;
			this.KSpointer = kspointer + 1;
			return kspointer;
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x000DF980 File Offset: 0x000DDB80
		public KeySequence PopKS()
		{
			ArrayList kss = this.KSs;
			int num = this.KSpointer - 1;
			this.KSpointer = num;
			return ((KSStruct)kss[num]).ks;
		}

		// Token: 0x0400190D RID: 6413
		private ConstraintStruct cs;

		// Token: 0x0400190E RID: 6414
		private ArrayList KSs;

		// Token: 0x0400190F RID: 6415
		private int KSpointer;
	}
}
