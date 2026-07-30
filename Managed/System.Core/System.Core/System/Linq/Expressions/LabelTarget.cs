using System;
using Unity;

namespace System.Linq.Expressions
{
	/// <summary>Used to represent the target of a <see cref="T:System.Linq.Expressions.GotoExpression" />.</summary>
	// Token: 0x02000281 RID: 641
	public sealed class LabelTarget
	{
		// Token: 0x060012C3 RID: 4803 RVA: 0x0003B47C File Offset: 0x0003967C
		internal LabelTarget(Type type, string name)
		{
			this.Type = type;
			this.Name = name;
		}

		/// <summary>Gets the name of the label.</summary>
		/// <returns>The name of the label.</returns>
		// Token: 0x1700031C RID: 796
		// (get) Token: 0x060012C4 RID: 4804 RVA: 0x0003B492 File Offset: 0x00039692
		public string Name { get; }

		/// <summary>The type of value that is passed when jumping to the label (or <see cref="T:System.Void" /> if no value should be passed).</summary>
		/// <returns>The <see cref="T:System.Type" /> object representing the type of the value that is passed when jumping to the label or <see cref="T:System.Void" /> if no value should be passed</returns>
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0003B49A File Offset: 0x0003969A
		public Type Type { get; }

		/// <summary>Returns a <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.</returns>
		// Token: 0x060012C6 RID: 4806 RVA: 0x0003B4A2 File Offset: 0x000396A2
		public override string ToString()
		{
			if (!string.IsNullOrEmpty(this.Name))
			{
				return this.Name;
			}
			return "UnamedLabel";
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x0000220F File Offset: 0x0000040F
		internal LabelTarget()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}
	}
}
