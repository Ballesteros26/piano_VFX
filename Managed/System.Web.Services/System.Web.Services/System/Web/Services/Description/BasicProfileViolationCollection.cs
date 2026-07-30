using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace System.Web.Services.Description
{
	/// <summary>Contains a strongly typed collection of <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> objects.</summary>
	// Token: 0x0200013B RID: 315
	public class BasicProfileViolationCollection : CollectionBase, IEnumerable<BasicProfileViolation>, IEnumerable
	{
		/// <summary>Gets or sets the <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> element at a specified index in the collection.</summary>
		/// <returns>A <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> object that exists at the specified index.</returns>
		/// <param name="index">The zero-based index in the collection.</param>
		// Token: 0x17000276 RID: 630
		public BasicProfileViolation this[int index]
		{
			get
			{
				return (BasicProfileViolation)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00043318 File Offset: 0x00041518
		internal int Add(BasicProfileViolation violation)
		{
			BasicProfileViolation basicProfileViolation = (BasicProfileViolation)this.violations[violation.NormativeStatement];
			if (basicProfileViolation == null)
			{
				this.violations[violation.NormativeStatement] = violation;
				return base.List.Add(violation);
			}
			foreach (string text in violation.Elements)
			{
				basicProfileViolation.Elements.Add(text);
			}
			return this.IndexOf(basicProfileViolation);
		}

		// Token: 0x060009A4 RID: 2468 RVA: 0x000433B4 File Offset: 0x000415B4
		internal int Add(string normativeStatement)
		{
			return this.Add(new BasicProfileViolation(normativeStatement));
		}

		// Token: 0x060009A5 RID: 2469 RVA: 0x000433C2 File Offset: 0x000415C2
		internal int Add(string normativeStatement, string element)
		{
			return this.Add(new BasicProfileViolation(normativeStatement, element));
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x000433D1 File Offset: 0x000415D1
		IEnumerator<BasicProfileViolation> IEnumerable<BasicProfileViolation>.GetEnumerator()
		{
			return new BasicProfileViolationEnumerator(this);
		}

		/// <summary>Inserts a <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> to the collection at the specified location.</summary>
		/// <param name="index">The zero-based index in the collection at which to insert the <paramref name="violation" />.</param>
		/// <param name="violation">The <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> to insert.</param>
		// Token: 0x060009A7 RID: 2471 RVA: 0x0000CD59 File Offset: 0x0000AF59
		public void Insert(int index, BasicProfileViolation violation)
		{
			base.List.Insert(index, violation);
		}

		/// <summary>Returns the zero-based index of a specified <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> in the collection.</summary>
		/// <returns>The zero-based index of the specified <see cref="T:System.Web.Services.Description.BasicProfileViolation" />, or -1 if the element was not found in the collection.</returns>
		/// <param name="violation">The <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> to find in the collection.</param>
		// Token: 0x060009A8 RID: 2472 RVA: 0x0000CD68 File Offset: 0x0000AF68
		public int IndexOf(BasicProfileViolation violation)
		{
			return base.List.IndexOf(violation);
		}

		/// <summary>Checks whether the collection contains the specified <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> object.</summary>
		/// <returns>true if the <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> object exists in the collection; otherwise false.</returns>
		/// <param name="violation">The <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> object to locate in the collection.</param>
		// Token: 0x060009A9 RID: 2473 RVA: 0x0000CD76 File Offset: 0x0000AF76
		public bool Contains(BasicProfileViolation violation)
		{
			return base.List.Contains(violation);
		}

		/// <summary>Removes a specified <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> from the collection.</summary>
		/// <param name="violation">The <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> to remove from the collection.</param>
		// Token: 0x060009AA RID: 2474 RVA: 0x0000CD84 File Offset: 0x0000AF84
		public void Remove(BasicProfileViolation violation)
		{
			base.List.Remove(violation);
		}

		/// <summary>Copies the elements from the collection to an array, starting at a specified index of the array.</summary>
		/// <param name="array">An array of type <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> to which to copy the contents of the collection.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		// Token: 0x060009AB RID: 2475 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public void CopyTo(BasicProfileViolation[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		/// <summary>Returns a <see cref="T:System.String" /> representation of the <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> objects in the collection.</summary>
		/// <returns>A <see cref="T:System.String" /> representation of the <see cref="T:System.Web.Services.Description.BasicProfileViolation" /> objects in the collection.</returns>
		// Token: 0x060009AC RID: 2476 RVA: 0x000433DC File Offset: 0x000415DC
		public override string ToString()
		{
			if (base.List.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < base.List.Count; i++)
				{
					BasicProfileViolation basicProfileViolation = this[i];
					if (i != 0)
					{
						stringBuilder.Append(Environment.NewLine);
					}
					stringBuilder.Append(basicProfileViolation.NormativeStatement);
					stringBuilder.Append(": ");
					stringBuilder.Append(basicProfileViolation.Details);
					foreach (string text in basicProfileViolation.Elements)
					{
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append("  -  ");
						stringBuilder.Append(text);
					}
					if (basicProfileViolation.Recommendation != null && basicProfileViolation.Recommendation.Length > 0)
					{
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append(basicProfileViolation.Recommendation);
					}
				}
				return stringBuilder.ToString();
			}
			return string.Empty;
		}

		// Token: 0x04000594 RID: 1428
		private Hashtable violations = new Hashtable();
	}
}
