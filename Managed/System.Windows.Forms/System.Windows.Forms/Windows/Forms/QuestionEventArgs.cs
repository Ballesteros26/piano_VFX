using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for events that need a true or false answer to a question.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020002AF RID: 687
	public class QuestionEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QuestionEventArgs" /> class using a default <see cref="P:System.Windows.Forms.QuestionEventArgs.Response" /> property value of false.</summary>
		// Token: 0x06002DD9 RID: 11737 RVA: 0x000B11EC File Offset: 0x000AF3EC
		public QuestionEventArgs()
		{
			this.response = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.QuestionEventArgs" /> class using the specified default value for the <see cref="P:System.Windows.Forms.QuestionEventArgs.Response" /> property.</summary>
		/// <param name="response">The default value of the <see cref="P:System.Windows.Forms.QuestionEventArgs.Response" /> property.</param>
		// Token: 0x06002DDA RID: 11738 RVA: 0x000B11FC File Offset: 0x000AF3FC
		public QuestionEventArgs(bool response)
		{
			this.response = response;
		}

		/// <summary>Gets or sets a value indicating the response to a question represented by the event.</summary>
		/// <returns>true for an affirmative response; otherwise, false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06002DDB RID: 11739 RVA: 0x000B120C File Offset: 0x000AF40C
		// (set) Token: 0x06002DDC RID: 11740 RVA: 0x000B1214 File Offset: 0x000AF414
		public bool Response
		{
			get
			{
				return this.response;
			}
			set
			{
				this.response = value;
			}
		}

		// Token: 0x04001614 RID: 5652
		private bool response;
	}
}
