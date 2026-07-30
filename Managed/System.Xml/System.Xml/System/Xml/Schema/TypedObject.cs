using System;
using System.Globalization;

namespace System.Xml.Schema
{
	// Token: 0x02000394 RID: 916
	internal class TypedObject
	{
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x060024F5 RID: 9461 RVA: 0x000DF9CE File Offset: 0x000DDBCE
		public int Dim
		{
			get
			{
				return this.dim;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x060024F6 RID: 9462 RVA: 0x000DF9D6 File Offset: 0x000DDBD6
		public bool IsList
		{
			get
			{
				return this.isList;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x060024F7 RID: 9463 RVA: 0x000DF9DE File Offset: 0x000DDBDE
		public bool IsDecimal
		{
			get
			{
				return this.dstruct.IsDecimal;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x060024F8 RID: 9464 RVA: 0x000DF9EB File Offset: 0x000DDBEB
		public decimal[] Dvalue
		{
			get
			{
				return this.dstruct.Dvalue;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x060024F9 RID: 9465 RVA: 0x000DF9F8 File Offset: 0x000DDBF8
		// (set) Token: 0x060024FA RID: 9466 RVA: 0x000DFA00 File Offset: 0x000DDC00
		public object Value
		{
			get
			{
				return this.ovalue;
			}
			set
			{
				this.ovalue = value;
			}
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060024FB RID: 9467 RVA: 0x000DFA09 File Offset: 0x000DDC09
		// (set) Token: 0x060024FC RID: 9468 RVA: 0x000DFA11 File Offset: 0x000DDC11
		public XmlSchemaDatatype Type
		{
			get
			{
				return this.xsdtype;
			}
			set
			{
				this.xsdtype = value;
			}
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x000DFA1C File Offset: 0x000DDC1C
		public TypedObject(object obj, string svalue, XmlSchemaDatatype xsdtype)
		{
			this.ovalue = obj;
			this.svalue = svalue;
			this.xsdtype = xsdtype;
			if (xsdtype.Variety == XmlSchemaDatatypeVariety.List || xsdtype is Datatype_base64Binary || xsdtype is Datatype_hexBinary)
			{
				this.isList = true;
				this.dim = ((Array)obj).Length;
			}
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x000DFA7C File Offset: 0x000DDC7C
		public override string ToString()
		{
			return this.svalue;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000DFA84 File Offset: 0x000DDC84
		public void SetDecimal()
		{
			if (this.dstruct != null)
			{
				return;
			}
			XmlTypeCode typeCode = this.xsdtype.TypeCode;
			if (typeCode == XmlTypeCode.Decimal || typeCode - XmlTypeCode.Integer <= 12)
			{
				if (this.isList)
				{
					this.dstruct = new TypedObject.DecimalStruct(this.dim);
					for (int i = 0; i < this.dim; i++)
					{
						this.dstruct.Dvalue[i] = Convert.ToDecimal(((Array)this.ovalue).GetValue(i), NumberFormatInfo.InvariantInfo);
					}
				}
				else
				{
					this.dstruct = new TypedObject.DecimalStruct();
					this.dstruct.Dvalue[0] = Convert.ToDecimal(this.ovalue, NumberFormatInfo.InvariantInfo);
				}
				this.dstruct.IsDecimal = true;
				return;
			}
			if (this.isList)
			{
				this.dstruct = new TypedObject.DecimalStruct(this.dim);
				return;
			}
			this.dstruct = new TypedObject.DecimalStruct();
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000DFB6C File Offset: 0x000DDD6C
		private bool ListDValueEquals(TypedObject other)
		{
			for (int i = 0; i < this.Dim; i++)
			{
				if (this.Dvalue[i] != other.Dvalue[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000DFBAC File Offset: 0x000DDDAC
		public bool Equals(TypedObject other)
		{
			if (this.Dim != other.Dim)
			{
				return false;
			}
			if (this.Type != other.Type)
			{
				if (!this.Type.IsComparable(other.Type))
				{
					return false;
				}
				other.SetDecimal();
				this.SetDecimal();
				if (this.IsDecimal && other.IsDecimal)
				{
					return this.ListDValueEquals(other);
				}
			}
			if (this.IsList)
			{
				if (other.IsList)
				{
					return this.Type.Compare(this.Value, other.Value) == 0;
				}
				Array array = this.Value as Array;
				XmlAtomicValue[] array2 = array as XmlAtomicValue[];
				if (array2 != null)
				{
					return array2.Length == 1 && array2.GetValue(0).Equals(other.Value);
				}
				return array.Length == 1 && array.GetValue(0).Equals(other.Value);
			}
			else
			{
				if (!other.IsList)
				{
					return this.Value.Equals(other.Value);
				}
				Array array3 = other.Value as Array;
				XmlAtomicValue[] array4 = array3 as XmlAtomicValue[];
				if (array4 != null)
				{
					return array4.Length == 1 && array4.GetValue(0).Equals(this.Value);
				}
				return array3.Length == 1 && array3.GetValue(0).Equals(this.Value);
			}
		}

		// Token: 0x04001913 RID: 6419
		private TypedObject.DecimalStruct dstruct;

		// Token: 0x04001914 RID: 6420
		private object ovalue;

		// Token: 0x04001915 RID: 6421
		private string svalue;

		// Token: 0x04001916 RID: 6422
		private XmlSchemaDatatype xsdtype;

		// Token: 0x04001917 RID: 6423
		private int dim = 1;

		// Token: 0x04001918 RID: 6424
		private bool isList;

		// Token: 0x02000395 RID: 917
		private class DecimalStruct
		{
			// Token: 0x17000761 RID: 1889
			// (get) Token: 0x06002502 RID: 9474 RVA: 0x000DFCF2 File Offset: 0x000DDEF2
			// (set) Token: 0x06002503 RID: 9475 RVA: 0x000DFCFA File Offset: 0x000DDEFA
			public bool IsDecimal
			{
				get
				{
					return this.isDecimal;
				}
				set
				{
					this.isDecimal = value;
				}
			}

			// Token: 0x17000762 RID: 1890
			// (get) Token: 0x06002504 RID: 9476 RVA: 0x000DFD03 File Offset: 0x000DDF03
			public decimal[] Dvalue
			{
				get
				{
					return this.dvalue;
				}
			}

			// Token: 0x06002505 RID: 9477 RVA: 0x000DFD0B File Offset: 0x000DDF0B
			public DecimalStruct()
			{
				this.dvalue = new decimal[1];
			}

			// Token: 0x06002506 RID: 9478 RVA: 0x000DFD1F File Offset: 0x000DDF1F
			public DecimalStruct(int dim)
			{
				this.dvalue = new decimal[dim];
			}

			// Token: 0x04001919 RID: 6425
			private bool isDecimal;

			// Token: 0x0400191A RID: 6426
			private decimal[] dvalue;
		}
	}
}
