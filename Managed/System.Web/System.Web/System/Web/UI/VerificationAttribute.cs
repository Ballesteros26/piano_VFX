using System;

namespace System.Web.UI
{
	/// <summary>Defines the metadata attribute of a Web content accessibility rule. This class cannot be inherited. </summary>
	// Token: 0x02000247 RID: 583
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property, AllowMultiple = true)]
	public sealed class VerificationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.VerificationAttribute" /> class with accessibility guideline, checkpoint, reporting level, checkpoint priority, and error message.</summary>
		/// <param name="guideline">The accessibility guideline the verification rule describes.</param>
		/// <param name="checkpoint">The checkpoint within the guideline.</param>
		/// <param name="reportLevel">One of the <see cref="T:System.Web.UI.VerificationReportLevel" /> values.</param>
		/// <param name="priority">The priority of the checkpoint.</param>
		/// <param name="message">The message displayed when the verification rule is true.</param>
		// Token: 0x060017FE RID: 6142 RVA: 0x00002C1E File Offset: 0x00000E1E
		public VerificationAttribute(string guideline, string checkpoint, VerificationReportLevel reportLevel, int priority, string message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.VerificationAttribute" /> class with accessibility guideline, checkpoint, reporting level, checkpoint priority, error message, <see cref="T:System.Web.UI.VerificationRule" />.value, and related conditional property.</summary>
		/// <param name="guideline">The accessibility guideline the verification rule describes.</param>
		/// <param name="checkpoint">The checkpoint within the guideline.</param>
		/// <param name="reportLevel">One of the <see cref="T:System.Web.UI.VerificationReportLevel" /> values.</param>
		/// <param name="priority">The priority of the checkpoint.</param>
		/// <param name="message">The message displayed when the verification rule is true.</param>
		/// <param name="rule">One of the <see cref="T:System.Web.UI.VerificationRule" />.values.</param>
		/// <param name="conditionalProperty">The left side of a conditional expression used to verify the accessibility rule.</param>
		// Token: 0x060017FF RID: 6143 RVA: 0x00002C1E File Offset: 0x00000E1E
		public VerificationAttribute(string guideline, string checkpoint, VerificationReportLevel reportLevel, int priority, string message, VerificationRule rule, string conditionalProperty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.VerificationAttribute" /> class with accessibility guideline, checkpoint, reporting level, checkpoint priority, error message, <see cref="T:System.Web.UI.VerificationRule" />.value, related conditional property, a related conditional property value, and a reference guideline URL.</summary>
		/// <param name="guideline">The accessibility guideline the verification rule describes.</param>
		/// <param name="checkpoint">The checkpoint within the guideline.</param>
		/// <param name="reportLevel">One of the <see cref="T:System.Web.UI.VerificationReportLevel" /> values.</param>
		/// <param name="priority">The priority of the checkpoint.</param>
		/// <param name="message">The message displayed when the verification rule is true.</param>
		/// <param name="rule">One of the <see cref="T:System.Web.UI.VerificationRule" />.values.</param>
		/// <param name="conditionalProperty">The left side of a conditional expression used to verify the accessibility rule.</param>
		/// <param name="conditionalOperator">One of the <see cref="T:System.Web.UI.VerificationConditionalOperator" /> values.</param>
		/// <param name="conditionalValue">The right hand side of a conditional expression used to verify the accessibility rule.</param>
		/// <param name="guidelineUrl">A reference URL for <paramref name="guideline" />.</param>
		// Token: 0x06001800 RID: 6144 RVA: 0x00002C1E File Offset: 0x00000E1E
		public VerificationAttribute(string guideline, string checkpoint, VerificationReportLevel reportLevel, int priority, string message, VerificationRule rule, string conditionalProperty, VerificationConditionalOperator conditionalOperator, string conditionalValue, string guidelineUrl)
		{
		}

		/// <summary>Gets the accessibility checkpoint reference in the specified <see cref="P:System.Web.UI.VerificationAttribute.Guideline" /> property.</summary>
		/// <returns>A string representing the checkpoint reference. </returns>
		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string Checkpoint
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the left side of a conditional expression used as part of the verification accessibility checkpoint.</summary>
		/// <returns>The left side of the conditional expression. The default value is an empty string ("").</returns>
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string ConditionalProperty
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the right side of a conditional expression used as part of the verification accessibility checkpoint.</summary>
		/// <returns>The right side of a conditional expression. The default value is an empty string ("").</returns>
		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string ConditionalValue
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the guideline that used for accessibility checking.</summary>
		/// <returns>A string representing the guideline.</returns>
		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string Guideline
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the URL the can be used to get more information on the accessibility guidelines given in the <see cref="P:System.Web.UI.VerificationAttribute.Guideline" /> property.</summary>
		/// <returns>The default value is an empty string ("").</returns>
		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001805 RID: 6149 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string GuidelineUrl
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a message string when the accessibility checkpoint verification rule is true.</summary>
		/// <returns>An error string.</returns>
		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string Message
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the priority of the accessibility checkpoint.</summary>
		/// <returns>An integer representing the priority.</returns>
		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x00003A1F File Offset: 0x00001C1F
		public int Priority
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.VerificationConditionalOperator" /> enumeration value indication how the accessibility checkpoint is verified. </summary>
		/// <returns>One of the <see cref="T:System.Web.UI.VerificationConditionalOperator" /> enumeration values. The default value is <see cref="F:System.Web.UI.VerificationConditionalOperator.Equals" />.</returns>
		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x00003A1F File Offset: 0x00001C1F
		public VerificationConditionalOperator VerificationConditionalOperator
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.VerificationReportLevel" /> enumeration value indicating how the accessibility checkpoint is used.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.VerificationReportLevel" /> enumeration values.</returns>
		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x00003A1F File Offset: 0x00001C1F
		public VerificationReportLevel VerificationReportLevel
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a <see cref="T:System.Web.UI.VerificationRule" /> enumeration value indicating how the accessibility checkpoint is used.</summary>
		/// <returns>One of the <see cref="T:System.Web.UI.VerificationRule" /> enumeration values. The default value is <see cref="F:System.Web.UI.VerificationRule.Required" />.</returns>
		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x00003A1F File Offset: 0x00001C1F
		public VerificationRule VerificationRule
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
