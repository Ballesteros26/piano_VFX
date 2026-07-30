using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020003A9 RID: 937
	internal sealed class NfaContentValidator : ContentValidator
	{
		// Token: 0x0600258D RID: 9613 RVA: 0x000E17D1 File Offset: 0x000DF9D1
		internal NfaContentValidator(BitSet firstpos, BitSet[] followpos, SymbolsDictionary symbols, Positions positions, int endMarkerPos, XmlSchemaContentType contentType, bool isOpen, bool isEmptiable)
			: base(contentType, isOpen, isEmptiable)
		{
			this.firstpos = firstpos;
			this.followpos = followpos;
			this.symbols = symbols;
			this.positions = positions;
			this.endMarkerPos = endMarkerPos;
		}

		// Token: 0x0600258E RID: 9614 RVA: 0x000E1804 File Offset: 0x000DFA04
		public override void InitValidation(ValidationState context)
		{
			context.CurPos[0] = this.firstpos.Clone();
			context.CurPos[1] = new BitSet(this.firstpos.Count);
			context.CurrentState.CurPosIndex = 0;
		}

		// Token: 0x0600258F RID: 9615 RVA: 0x000E1840 File Offset: 0x000DFA40
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			BitSet bitSet = context.CurPos[context.CurrentState.CurPosIndex];
			int num = (context.CurrentState.CurPosIndex + 1) % 2;
			BitSet bitSet2 = context.CurPos[num];
			bitSet2.Clear();
			int num2 = this.symbols[name];
			object obj = null;
			errorCode = 0;
			for (int num3 = bitSet.NextSet(-1); num3 != -1; num3 = bitSet.NextSet(num3))
			{
				if (num2 == this.positions[num3].symbol)
				{
					bitSet2.Or(this.followpos[num3]);
					obj = this.positions[num3].particle;
					break;
				}
			}
			if (!bitSet2.IsEmpty)
			{
				context.CurrentState.CurPosIndex = num;
				return obj;
			}
			if (base.IsOpen && bitSet[this.endMarkerPos])
			{
				return null;
			}
			context.NeedValidateChildren = false;
			errorCode = -1;
			return null;
		}

		// Token: 0x06002590 RID: 9616 RVA: 0x000E1920 File Offset: 0x000DFB20
		public override bool CompleteValidation(ValidationState context)
		{
			return context.CurPos[context.CurrentState.CurPosIndex][this.endMarkerPos];
		}

		// Token: 0x06002591 RID: 9617 RVA: 0x000E1944 File Offset: 0x000DFB44
		public override ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			ArrayList arrayList = null;
			BitSet bitSet = context.CurPos[context.CurrentState.CurPosIndex];
			for (int num = bitSet.NextSet(-1); num != -1; num = bitSet.NextSet(num))
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)this.positions[num].particle;
				if (xmlSchemaParticle == null)
				{
					string text = this.symbols.NameOf(this.positions[num].symbol);
					if (text.Length != 0)
					{
						arrayList.Add(text);
					}
				}
				else
				{
					string nameString = xmlSchemaParticle.NameString;
					if (!arrayList.Contains(nameString))
					{
						arrayList.Add(nameString);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x000E19F0 File Offset: 0x000DFBF0
		public override ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
		{
			ArrayList arrayList = new ArrayList();
			BitSet bitSet = context.CurPos[context.CurrentState.CurPosIndex];
			for (int num = bitSet.NextSet(-1); num != -1; num = bitSet.NextSet(num))
			{
				XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)this.positions[num].particle;
				if (xmlSchemaParticle != null)
				{
					ContentValidator.AddParticleToExpected(xmlSchemaParticle, schemaSet, arrayList);
				}
			}
			return arrayList;
		}

		// Token: 0x04001948 RID: 6472
		private BitSet firstpos;

		// Token: 0x04001949 RID: 6473
		private BitSet[] followpos;

		// Token: 0x0400194A RID: 6474
		private SymbolsDictionary symbols;

		// Token: 0x0400194B RID: 6475
		private Positions positions;

		// Token: 0x0400194C RID: 6476
		private int endMarkerPos;
	}
}
