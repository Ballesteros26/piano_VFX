using System;

namespace System.Web.UI.WebControls
{
	/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.CustomValidator.ServerValidate" /> event of the <see cref="T:System.Web.UI.WebControls.CustomValidator" /> control. This class cannot be inherited.</summary>
	// Token: 0x02000306 RID: 774
	public class ServerValidateEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.ServerValidateEventArgs" /> class.</summary>
		/// <param name="value">The value to validate. </param>
		/// <param name="isValid">true to indicate that the value passes validation; otherwise, false. </param>
		// Token: 0x06001BED RID: 7149 RVA: 0x00046297 File Offset: 0x00044497
		public ServerValidateEventArgs(string value, bool isValid)
		{
			this.isValid = isValid;
			this.value = value;
		}

		/// <summary>Gets the value to validate in the custom event handler for the <see cref="E:System.Web.UI.WebControls.CustomValidator.ServerValidate" /> event.</summary>
		/// <returns>The value to validate in the custom event handler for the <see cref="E:System.Web.UI.WebControls.CustomValidator.ServerValidate" /> event.</returns>
		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06001BEE RID: 7150 RVA: 0x000462AD File Offset: 0x000444AD
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Gets or sets whether the value specified by the <see cref="P:System.Web.UI.WebControls.ServerValidateEventArgs.Value" /> property passed validation.</summary>
		/// <returns>true to indicate that the value specified by the <see cref="P:System.Web.UI.WebControls.ServerValidateEventArgs.Value" /> property passed validation; otherwise, false.</returns>
		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x000462B5 File Offset: 0x000444B5
		// (set) Token: 0x06001BF0 RID: 7152 RVA: 0x000462BD File Offset: 0x000444BD
		public bool IsValid
		{
			get
			{
				return this.isValid;
			}
			set
			{
				this.isValid = value;
			}
		}

		// Token: 0x04001759 RID: 5977
		private bool isValid;

		// Token: 0x0400175A RID: 5978
		private string value;
	}
}
