using System;

namespace System.Diagnostics.CodeAnalysis
{
	/// <summary>Suppresses reporting of a specific static analysis tool rule violation, allowing multiple suppressions on a single code artifact.</summary>
	// Token: 0x02000A8E RID: 2702
	[AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
	[Conditional("CODE_ANALYSIS")]
	public sealed class SuppressMessageAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CodeAnalysis.SuppressMessageAttribute" /> class, specifying the category of the static analysis tool and the identifier for an analysis rule. </summary>
		/// <param name="category">The category for the attribute.</param>
		/// <param name="checkId">The identifier of the analysis tool rule the attribute applies to.</param>
		// Token: 0x06006255 RID: 25173 RVA: 0x0014112D File Offset: 0x0013F32D
		public SuppressMessageAttribute(string category, string checkId)
		{
			this.category = category;
			this.checkId = checkId;
		}

		/// <summary>Gets the category identifying the classification of the attribute.</summary>
		/// <returns>The category identifying the attribute.</returns>
		// Token: 0x170011A9 RID: 4521
		// (get) Token: 0x06006256 RID: 25174 RVA: 0x00141143 File Offset: 0x0013F343
		public string Category
		{
			get
			{
				return this.category;
			}
		}

		/// <summary>Gets the identifier of the static analysis tool rule to be suppressed.</summary>
		/// <returns>The identifier of the static analysis tool rule to be suppressed.</returns>
		// Token: 0x170011AA RID: 4522
		// (get) Token: 0x06006257 RID: 25175 RVA: 0x0014114B File Offset: 0x0013F34B
		public string CheckId
		{
			get
			{
				return this.checkId;
			}
		}

		/// <summary>Gets or sets the scope of the code that is relevant for the attribute.</summary>
		/// <returns>The scope of the code that is relevant for the attribute.</returns>
		// Token: 0x170011AB RID: 4523
		// (get) Token: 0x06006258 RID: 25176 RVA: 0x00141153 File Offset: 0x0013F353
		// (set) Token: 0x06006259 RID: 25177 RVA: 0x0014115B File Offset: 0x0013F35B
		public string Scope
		{
			get
			{
				return this.scope;
			}
			set
			{
				this.scope = value;
			}
		}

		/// <summary>Gets or sets a fully qualified path that represents the target of the attribute.</summary>
		/// <returns>A fully qualified path that represents the target of the attribute.</returns>
		// Token: 0x170011AC RID: 4524
		// (get) Token: 0x0600625A RID: 25178 RVA: 0x00141164 File Offset: 0x0013F364
		// (set) Token: 0x0600625B RID: 25179 RVA: 0x0014116C File Offset: 0x0013F36C
		public string Target
		{
			get
			{
				return this.target;
			}
			set
			{
				this.target = value;
			}
		}

		/// <summary>Gets or sets an optional argument expanding on exclusion criteria.</summary>
		/// <returns>A string containing the expanded exclusion criteria.</returns>
		// Token: 0x170011AD RID: 4525
		// (get) Token: 0x0600625C RID: 25180 RVA: 0x00141175 File Offset: 0x0013F375
		// (set) Token: 0x0600625D RID: 25181 RVA: 0x0014117D File Offset: 0x0013F37D
		public string MessageId
		{
			get
			{
				return this.messageId;
			}
			set
			{
				this.messageId = value;
			}
		}

		/// <summary>Gets or sets the justification for suppressing the code analysis message.</summary>
		/// <returns>The justification for suppressing the message.</returns>
		// Token: 0x170011AE RID: 4526
		// (get) Token: 0x0600625E RID: 25182 RVA: 0x00141186 File Offset: 0x0013F386
		// (set) Token: 0x0600625F RID: 25183 RVA: 0x0014118E File Offset: 0x0013F38E
		public string Justification
		{
			get
			{
				return this.justification;
			}
			set
			{
				this.justification = value;
			}
		}

		// Token: 0x04003104 RID: 12548
		private string category;

		// Token: 0x04003105 RID: 12549
		private string justification;

		// Token: 0x04003106 RID: 12550
		private string checkId;

		// Token: 0x04003107 RID: 12551
		private string scope;

		// Token: 0x04003108 RID: 12552
		private string target;

		// Token: 0x04003109 RID: 12553
		private string messageId;
	}
}
