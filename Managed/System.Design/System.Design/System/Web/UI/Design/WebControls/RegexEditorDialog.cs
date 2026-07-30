using System;
using System.ComponentModel;
using System.Windows.Forms;
using Unity;

namespace System.Web.UI.Design.WebControls
{
	/// <summary>Provides a dialog box for editing regular expressions used by the <see cref="T:System.Web.UI.WebControls.RegularExpressionValidator" />.</summary>
	// Token: 0x020000D8 RID: 216
	public partial class RegexEditorDialog : Form
	{
		// Token: 0x0600063F RID: 1599 RVA: 0x00009891 File Offset: 0x00007A91
		public RegexEditorDialog()
		{
		}

		/// <summary>Gets or sets the name of the regular expression to edit.</summary>
		/// <returns>The name of the regular expression.</returns>
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x06000640 RID: 1600 RVA: 0x0000234B File Offset: 0x0000054B
		// (set) Token: 0x06000641 RID: 1601 RVA: 0x00009899 File Offset: 0x00007A99
		public string RegularExpression
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				this.regular_expression = value;
			}
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x0000234B File Offset: 0x0000054B
		protected void CmdHelp_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0000234B File Offset: 0x0000054B
		protected void CmdOK_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x0000234B File Offset: 0x0000054B
		protected void CmdTestValidate_Click(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x0000234B File Offset: 0x0000054B
		protected void LstStandardExpressions_SelectedIndexChanged(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Represents the method that will handle the Activated event of dialog box.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that provides data for the event. </param>
		// Token: 0x06000647 RID: 1607 RVA: 0x0000234B File Offset: 0x0000054B
		protected void RegexTypeEditor_Activated(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0000234B File Offset: 0x0000054B
		protected void TxtExpression_Changed(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.WebControls.RegexEditorDialog" /> class.</summary>
		/// <param name="site">The site for this dialog box. </param>
		// Token: 0x06000649 RID: 1609 RVA: 0x00009519 File Offset: 0x00007719
		public RegexEditorDialog(ISite site)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Represents the method that will handle the Help event of the dialog box.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that provides data for the event.</param>
		// Token: 0x0600064A RID: 1610 RVA: 0x00009519 File Offset: 0x00007719
		protected void cmdHelp_Click(object sender, EventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Represents the method that will handle the OK event of the dialog box.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that provides data for the event. </param>
		// Token: 0x0600064B RID: 1611 RVA: 0x00009519 File Offset: 0x00007719
		protected void cmdOK_Click(object sender, EventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Represents the method that will handle the TestValidate event of the dialog box.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="args">An <see cref="T:System.EventArgs" /> object that provides data for the event. </param>
		// Token: 0x0600064C RID: 1612 RVA: 0x00009519 File Offset: 0x00007719
		protected void cmdTestValidate_Click(object sender, EventArgs args)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Represents the method that will handle the SelectedIndexChanged event of the dialog box.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that provides data for the event. </param>
		// Token: 0x0600064D RID: 1613 RVA: 0x00009519 File Offset: 0x00007719
		protected void lstStandardExpressions_SelectedIndexChanged(object sender, EventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Represents the method that will handle the TextChanged event of the dialog box.</summary>
		/// <param name="sender">The source of the event. </param>
		/// <param name="e">An <see cref="T:System.EventArgs" /> object that provides data for the event. </param>
		// Token: 0x0600064E RID: 1614 RVA: 0x00009519 File Offset: 0x00007719
		protected void txtExpression_TextChanged(object sender, EventArgs e)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000152 RID: 338
		private string regular_expression;
	}
}
