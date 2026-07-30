using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x0200004A RID: 74
	public class RDN
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000E160 File Offset: 0x0000C360
		protected internal virtual string RawValue
		{
			get
			{
				return this.rawValue;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000E168 File Offset: 0x0000C368
		public virtual string Type
		{
			get
			{
				return (string)this.types[0];
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000E17C File Offset: 0x0000C37C
		public virtual string[] Types
		{
			get
			{
				string[] array = new string[this.types.Count];
				for (int i = 0; i < this.types.Count; i++)
				{
					array[i] = (string)this.types[i];
				}
				return array;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000E1C5 File Offset: 0x0000C3C5
		public virtual string Value
		{
			get
			{
				return (string)this.values[0];
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
		public virtual string[] Values
		{
			get
			{
				string[] array = new string[this.values.Count];
				for (int i = 0; i < this.values.Count; i++)
				{
					array[i] = (string)this.values[i];
				}
				return array;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000E221 File Offset: 0x0000C421
		public virtual bool Multivalued
		{
			get
			{
				return this.values.Count > 1;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000E234 File Offset: 0x0000C434
		public RDN(string rdn)
		{
			this.rawValue = rdn;
			ArrayList rdns = new DN(rdn).RDNs;
			if (rdns.Count != 1)
			{
				throw new ArgumentException("Invalid RDN: see API documentation");
			}
			RDN rdn2 = (RDN)rdns[0];
			this.types = rdn2.types;
			this.values = rdn2.values;
			this.rawValue = rdn2.rawValue;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000E29D File Offset: 0x0000C49D
		public RDN()
		{
			this.types = new ArrayList();
			this.values = new ArrayList();
			this.rawValue = "";
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000E2C8 File Offset: 0x0000C4C8
		[CLSCompliant(false)]
		public virtual bool equals(RDN rdn)
		{
			if (this.values.Count != rdn.values.Count)
			{
				return false;
			}
			for (int i = 0; i < this.values.Count; i++)
			{
				int num = 0;
				while (num < this.values.Count && (!((string)this.values[i]).ToUpper().Equals(((string)rdn.values[num]).ToUpper()) || !this.equalAttrType((string)this.types[i], (string)rdn.types[num])))
				{
					num++;
				}
				if (num >= rdn.values.Count)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000E38F File Offset: 0x0000C58F
		private bool equalAttrType(string attr1, string attr2)
		{
			if (char.IsDigit(attr1[0]) ^ char.IsDigit(attr2[0]))
			{
				throw new ArgumentException("OID numbers are not currently compared to attribute names");
			}
			return attr1.ToUpper().Equals(attr2.ToUpper());
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000E3C8 File Offset: 0x0000C5C8
		public virtual void add(string attrType, string attrValue, string rawValue)
		{
			this.types.Add(attrType);
			this.values.Add(attrValue);
			this.rawValue += rawValue;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000E3F6 File Offset: 0x0000C5F6
		public override string ToString()
		{
			return this.toString(false);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000E400 File Offset: 0x0000C600
		[CLSCompliant(false)]
		public virtual string toString(bool noTypes)
		{
			int count = this.types.Count;
			string text = "";
			if (count < 1)
			{
				return null;
			}
			if (!noTypes)
			{
				text = this.types[0] + "=";
			}
			text += this.values[0];
			for (int i = 1; i < count; i++)
			{
				text += "+";
				if (!noTypes)
				{
					text = text + this.types[i] + "=";
				}
				text += this.values[i];
			}
			return text;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000E49C File Offset: 0x0000C69C
		public virtual string[] explodeRDN(bool noTypes)
		{
			int count = this.types.Count;
			if (count < 1)
			{
				return null;
			}
			string[] array = new string[this.types.Count];
			if (!noTypes)
			{
				array[0] = this.types[0] + "=";
			}
			string[] array2 = array;
			int num = 0;
			array2[num] += this.values[0];
			for (int i = 1; i < count; i++)
			{
				if (!noTypes)
				{
					string[] array3 = array;
					int num2 = i;
					array3[num2] = array3[num2] + this.types[i] + "=";
				}
				string[] array4 = array;
				int num3 = i;
				array4[num3] += this.values[i];
			}
			return array;
		}

		// Token: 0x040001ED RID: 493
		private ArrayList types;

		// Token: 0x040001EE RID: 494
		private ArrayList values;

		// Token: 0x040001EF RID: 495
		private string rawValue;
	}
}
