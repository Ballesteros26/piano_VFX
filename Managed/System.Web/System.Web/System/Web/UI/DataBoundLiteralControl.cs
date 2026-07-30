using System;
using System.ComponentModel;
using System.Security.Permissions;
using System.Text;

namespace System.Web.UI
{
	/// <summary>Retains data-binding expressions and static literal text. This class cannot be inherited.</summary>
	// Token: 0x020001C2 RID: 450
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataBoundLiteralControl : Control, ITextControl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> class. </summary>
		/// <param name="staticLiteralsCount">Defines the size of the array to create for storing static literal strings.</param>
		/// <param name="dataBoundLiteralCount">Defines the size of the array to create for storing data-bound literal strings.</param>
		// Token: 0x06001243 RID: 4675 RVA: 0x000327EE File Offset: 0x000309EE
		public DataBoundLiteralControl(int staticLiteralsCount, int dataBoundLiteralCount)
		{
			this.staticLiteralsCount = staticLiteralsCount;
			this.dataBoundLiterals = new string[dataBoundLiteralCount];
			base.AutoID = false;
		}

		/// <summary>Gets the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> object. </summary>
		/// <returns>A <see cref="T:System.String" /> that represents the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" />.</returns>
		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x00032810 File Offset: 0x00030A10
		public string Text
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = ((this.staticLiterals == null) ? 0 : this.staticLiterals.Length);
				int num2 = this.dataBoundLiterals.Length;
				int num3 = ((num > num2) ? num : num2);
				for (int i = 0; i < num3; i++)
				{
					if (i < num)
					{
						stringBuilder.Append(this.staticLiterals[i]);
					}
					if (i < num2)
					{
						stringBuilder.Append(this.dataBoundLiterals[i]);
					}
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00032889 File Offset: 0x00030A89
		protected override ControlCollection CreateControlCollection()
		{
			return new EmptyControlCollection(this);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00032894 File Offset: 0x00030A94
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				Array array = (Array)savedState;
				if (array.Length == this.dataBoundLiterals.Length)
				{
					array.CopyTo(this.dataBoundLiterals, 0);
				}
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x000328C8 File Offset: 0x00030AC8
		protected internal override void Render(HtmlTextWriter output)
		{
			int num = ((this.staticLiterals == null) ? 0 : this.staticLiterals.Length);
			int num2 = this.dataBoundLiterals.Length;
			int num3 = ((num > num2) ? num : num2);
			for (int i = 0; i < num3; i++)
			{
				if (i < num)
				{
					output.Write(this.staticLiterals[i]);
				}
				if (i < num2)
				{
					output.Write(this.dataBoundLiterals[i]);
				}
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x0003292B File Offset: 0x00030B2B
		protected override object SaveViewState()
		{
			if (this.dataBoundLiterals.Length == 0)
			{
				return null;
			}
			return this.dataBoundLiterals;
		}

		/// <summary>Assigns a string value to an array containing data-bound values.</summary>
		/// <param name="index">The position in an array at which to retain the <paramref name="s" /> parameter value. </param>
		/// <param name="s">A <see cref="T:System.String" /> containing the value for the data-bound expression.</param>
		// Token: 0x06001249 RID: 4681 RVA: 0x0003293E File Offset: 0x00030B3E
		public void SetDataBoundString(int index, string s)
		{
			this.dataBoundLiterals[index] = s;
		}

		/// <summary>Assigns a string value to an array containing static values.</summary>
		/// <param name="index">The position in an array at which to retain the <paramref name="s" /> parameter value.</param>
		/// <param name="s">A <see cref="T:System.String" /> containing the value for the data-bound expression.</param>
		// Token: 0x0600124A RID: 4682 RVA: 0x00032949 File Offset: 0x00030B49
		public void SetStaticString(int index, string s)
		{
			if (this.staticLiterals == null)
			{
				this.staticLiterals = new string[this.staticLiteralsCount];
			}
			this.staticLiterals[index] = s;
		}

		/// <summary>Gets or sets the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that represents the text content of the <see cref="T:System.Web.UI.DataBoundLiteralControl" />.</returns>
		/// <exception cref="T:System.NotSupportedException">An attempt to set the value is made.</exception>
		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x0003296D File Offset: 0x00030B6D
		// (set) Token: 0x0600124C RID: 4684 RVA: 0x00003A01 File Offset: 0x00001C01
		string ITextControl.Text
		{
			get
			{
				return this.Text;
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x0400141B RID: 5147
		private int staticLiteralsCount;

		// Token: 0x0400141C RID: 5148
		private string[] staticLiterals;

		// Token: 0x0400141D RID: 5149
		private string[] dataBoundLiterals;
	}
}
