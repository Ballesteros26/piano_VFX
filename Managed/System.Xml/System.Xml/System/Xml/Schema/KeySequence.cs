using System;
using System.Text;

namespace System.Xml.Schema
{
	// Token: 0x02000396 RID: 918
	internal class KeySequence
	{
		// Token: 0x06002507 RID: 9479 RVA: 0x000DFD33 File Offset: 0x000DDF33
		internal KeySequence(int dim, int line, int col)
		{
			this.dim = dim;
			this.ks = new TypedObject[dim];
			this.posline = line;
			this.poscol = col;
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002508 RID: 9480 RVA: 0x000DFD63 File Offset: 0x000DDF63
		public int PosLine
		{
			get
			{
				return this.posline;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06002509 RID: 9481 RVA: 0x000DFD6B File Offset: 0x000DDF6B
		public int PosCol
		{
			get
			{
				return this.poscol;
			}
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000DFD74 File Offset: 0x000DDF74
		public KeySequence(TypedObject[] ks)
		{
			this.ks = ks;
			this.dim = ks.Length;
			this.posline = (this.poscol = 0);
		}

		// Token: 0x17000765 RID: 1893
		public object this[int index]
		{
			get
			{
				return this.ks[index];
			}
			set
			{
				this.ks[index] = (TypedObject)value;
			}
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000DFDC8 File Offset: 0x000DDFC8
		internal bool IsQualified()
		{
			for (int i = 0; i < this.ks.Length; i++)
			{
				if (this.ks[i] == null || this.ks[i].Value == null)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000DFE04 File Offset: 0x000DE004
		public override int GetHashCode()
		{
			if (this.hashcode != -1)
			{
				return this.hashcode;
			}
			this.hashcode = 0;
			for (int i = 0; i < this.ks.Length; i++)
			{
				this.ks[i].SetDecimal();
				if (this.ks[i].IsDecimal)
				{
					for (int j = 0; j < this.ks[i].Dim; j++)
					{
						this.hashcode += this.ks[i].Dvalue[j].GetHashCode();
					}
				}
				else
				{
					Array array = this.ks[i].Value as Array;
					if (array != null)
					{
						XmlAtomicValue[] array2 = array as XmlAtomicValue[];
						if (array2 != null)
						{
							for (int k = 0; k < array2.Length; k++)
							{
								this.hashcode += ((XmlAtomicValue)array2.GetValue(k)).TypedValue.GetHashCode();
							}
						}
						else
						{
							for (int l = 0; l < ((Array)this.ks[i].Value).Length; l++)
							{
								this.hashcode += ((Array)this.ks[i].Value).GetValue(l).GetHashCode();
							}
						}
					}
					else
					{
						this.hashcode += this.ks[i].Value.GetHashCode();
					}
				}
			}
			return this.hashcode;
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x000DFF74 File Offset: 0x000DE174
		public override bool Equals(object other)
		{
			KeySequence keySequence = (KeySequence)other;
			for (int i = 0; i < this.ks.Length; i++)
			{
				if (!this.ks[i].Equals(keySequence.ks[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000DFFB8 File Offset: 0x000DE1B8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.ks[0].ToString());
			for (int i = 1; i < this.ks.Length; i++)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(this.ks[i].ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400191B RID: 6427
		private TypedObject[] ks;

		// Token: 0x0400191C RID: 6428
		private int dim;

		// Token: 0x0400191D RID: 6429
		private int hashcode = -1;

		// Token: 0x0400191E RID: 6430
		private int posline;

		// Token: 0x0400191F RID: 6431
		private int poscol;
	}
}
