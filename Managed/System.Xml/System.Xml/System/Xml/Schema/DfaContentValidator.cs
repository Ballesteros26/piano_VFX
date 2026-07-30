using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020003A8 RID: 936
	internal sealed class DfaContentValidator : ContentValidator
	{
		// Token: 0x06002587 RID: 9607 RVA: 0x000E15F6 File Offset: 0x000DF7F6
		internal DfaContentValidator(int[][] transitionTable, SymbolsDictionary symbols, XmlSchemaContentType contentType, bool isOpen, bool isEmptiable)
			: base(contentType, isOpen, isEmptiable)
		{
			this.transitionTable = transitionTable;
			this.symbols = symbols;
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000E1611 File Offset: 0x000DF811
		public override void InitValidation(ValidationState context)
		{
			context.CurrentState.State = 0;
			context.HasMatched = this.transitionTable[0][this.symbols.Count] > 0;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000E163C File Offset: 0x000DF83C
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			int num = this.symbols[name];
			int num2 = this.transitionTable[context.CurrentState.State][num];
			errorCode = 0;
			if (num2 != -1)
			{
				context.CurrentState.State = num2;
				context.HasMatched = this.transitionTable[context.CurrentState.State][this.symbols.Count] > 0;
				return this.symbols.GetParticle(num);
			}
			if (base.IsOpen && context.HasMatched)
			{
				return null;
			}
			context.NeedValidateChildren = false;
			errorCode = -1;
			return null;
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000E16CF File Offset: 0x000DF8CF
		public override bool CompleteValidation(ValidationState context)
		{
			return context.HasMatched;
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x000E16DC File Offset: 0x000DF8DC
		public override ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			ArrayList arrayList = null;
			int[] array = this.transitionTable[context.CurrentState.State];
			if (array != null)
			{
				for (int i = 0; i < array.Length - 1; i++)
				{
					if (array[i] != -1)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)this.symbols.GetParticle(i);
						if (xmlSchemaParticle == null)
						{
							string text = this.symbols.NameOf(i);
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
				}
			}
			return arrayList;
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000E1774 File Offset: 0x000DF974
		public override ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
		{
			ArrayList arrayList = new ArrayList();
			int[] array = this.transitionTable[context.CurrentState.State];
			if (array != null)
			{
				for (int i = 0; i < array.Length - 1; i++)
				{
					if (array[i] != -1)
					{
						XmlSchemaParticle xmlSchemaParticle = (XmlSchemaParticle)this.symbols.GetParticle(i);
						if (xmlSchemaParticle != null)
						{
							ContentValidator.AddParticleToExpected(xmlSchemaParticle, schemaSet, arrayList);
						}
					}
				}
			}
			return arrayList;
		}

		// Token: 0x04001946 RID: 6470
		private int[][] transitionTable;

		// Token: 0x04001947 RID: 6471
		private SymbolsDictionary symbols;
	}
}
