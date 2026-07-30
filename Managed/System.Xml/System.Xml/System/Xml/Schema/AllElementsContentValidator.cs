using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020003AC RID: 940
	internal sealed class AllElementsContentValidator : ContentValidator
	{
		// Token: 0x06002599 RID: 9625 RVA: 0x000E210A File Offset: 0x000E030A
		public AllElementsContentValidator(XmlSchemaContentType contentType, int size, bool isEmptiable)
			: base(contentType, false, isEmptiable)
		{
			this.elements = new Hashtable(size);
			this.particles = new object[size];
			this.isRequired = new BitSet(size);
		}

		// Token: 0x0600259A RID: 9626 RVA: 0x000E213C File Offset: 0x000E033C
		public bool AddElement(XmlQualifiedName name, object particle, bool isEmptiable)
		{
			if (this.elements[name] != null)
			{
				return false;
			}
			int count = this.elements.Count;
			this.elements.Add(name, count);
			this.particles[count] = particle;
			if (!isEmptiable)
			{
				this.isRequired.Set(count);
				this.countRequired++;
			}
			return true;
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x000E219E File Offset: 0x000E039E
		public override bool IsEmptiable
		{
			get
			{
				return base.IsEmptiable || this.countRequired == 0;
			}
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x000E21B3 File Offset: 0x000E03B3
		public override void InitValidation(ValidationState context)
		{
			context.AllElementsSet = new BitSet(this.elements.Count);
			context.CurrentState.AllElementsRequired = -1;
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x000E21D8 File Offset: 0x000E03D8
		public override object ValidateElement(XmlQualifiedName name, ValidationState context, out int errorCode)
		{
			object obj = this.elements[name];
			errorCode = 0;
			if (obj == null)
			{
				context.NeedValidateChildren = false;
				return null;
			}
			int num = (int)obj;
			if (context.AllElementsSet[num])
			{
				errorCode = -2;
				return null;
			}
			if (context.CurrentState.AllElementsRequired == -1)
			{
				context.CurrentState.AllElementsRequired = 0;
			}
			context.AllElementsSet.Set(num);
			if (this.isRequired[num])
			{
				context.CurrentState.AllElementsRequired = context.CurrentState.AllElementsRequired + 1;
			}
			return this.particles[num];
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x000E2268 File Offset: 0x000E0468
		public override bool CompleteValidation(ValidationState context)
		{
			return context.CurrentState.AllElementsRequired == this.countRequired || (this.IsEmptiable && context.CurrentState.AllElementsRequired == -1);
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x000E2298 File Offset: 0x000E0498
		public override ArrayList ExpectedElements(ValidationState context, bool isRequiredOnly)
		{
			ArrayList arrayList = null;
			foreach (object obj in this.elements)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!context.AllElementsSet[(int)dictionaryEntry.Value] && (!isRequiredOnly || this.isRequired[(int)dictionaryEntry.Value]))
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
					}
					arrayList.Add(dictionaryEntry.Key);
				}
			}
			return arrayList;
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x000E233C File Offset: 0x000E053C
		public override ArrayList ExpectedParticles(ValidationState context, bool isRequiredOnly, XmlSchemaSet schemaSet)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in this.elements)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!context.AllElementsSet[(int)dictionaryEntry.Value] && (!isRequiredOnly || this.isRequired[(int)dictionaryEntry.Value]))
				{
					ContentValidator.AddParticleToExpected(this.particles[(int)dictionaryEntry.Value] as XmlSchemaParticle, schemaSet, arrayList);
				}
			}
			return arrayList;
		}

		// Token: 0x04001956 RID: 6486
		private Hashtable elements;

		// Token: 0x04001957 RID: 6487
		private object[] particles;

		// Token: 0x04001958 RID: 6488
		private BitSet isRequired;

		// Token: 0x04001959 RID: 6489
		private int countRequired;
	}
}
