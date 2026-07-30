using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200005A RID: 90
	public class BindingCompleteEventArgs : CancelEventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" /> class with the specified binding, error state, and binding context.</summary>
		/// <param name="binding">The binding associated with this occurrence of a <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.BindingCompleteState" /> values.</param>
		/// <param name="context">One of the <see cref="T:System.Windows.Forms.BindingCompleteContext" /> values. </param>
		// Token: 0x06000379 RID: 889 RVA: 0x00012BAC File Offset: 0x00010DAC
		public BindingCompleteEventArgs(Binding binding, BindingCompleteState state, BindingCompleteContext context)
			: this(binding, state, context, string.Empty, null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" /> class with the specified binding, error state and text, and binding context.</summary>
		/// <param name="binding">The binding associated with this occurrence of a <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.BindingCompleteState" /> values.</param>
		/// <param name="context">One of the <see cref="T:System.Windows.Forms.BindingCompleteContext" /> values. </param>
		/// <param name="errorText">The error text or exception message for errors that occurred during the binding.</param>
		// Token: 0x0600037A RID: 890 RVA: 0x00012BC0 File Offset: 0x00010DC0
		public BindingCompleteEventArgs(Binding binding, BindingCompleteState state, BindingCompleteContext context, string errorText)
			: this(binding, state, context, errorText, null, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" /> class with the specified binding, error state and text, binding context, and exception.</summary>
		/// <param name="binding">The binding associated with this occurrence of a <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.BindingCompleteState" /> values.</param>
		/// <param name="context">One of the <see cref="T:System.Windows.Forms.BindingCompleteContext" /> values. </param>
		/// <param name="errorText">The error text or exception message for errors that occurred during the binding.</param>
		/// <param name="exception">The <see cref="T:System.Exception" /> that occurred during the binding.</param>
		// Token: 0x0600037B RID: 891 RVA: 0x00012BD0 File Offset: 0x00010DD0
		public BindingCompleteEventArgs(Binding binding, BindingCompleteState state, BindingCompleteContext context, string errorText, Exception exception)
			: this(binding, state, context, errorText, exception, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" /> class with the specified binding, error state and text, binding context, exception, and whether the binding should be cancelled.</summary>
		/// <param name="binding">The binding associated with this occurrence of a <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event.</param>
		/// <param name="state">One of the <see cref="T:System.Windows.Forms.BindingCompleteState" /> values.</param>
		/// <param name="context">One of the <see cref="T:System.Windows.Forms.BindingCompleteContext" /> values. </param>
		/// <param name="errorText">The error text or exception message for errors that occurred during the binding.</param>
		/// <param name="exception">The <see cref="T:System.Exception" /> that occurred during the binding.</param>
		/// <param name="cancel">true to cancel the binding and keep focus on the current control; false to allow focus to shift to another control.</param>
		// Token: 0x0600037C RID: 892 RVA: 0x00012BE0 File Offset: 0x00010DE0
		public BindingCompleteEventArgs(Binding binding, BindingCompleteState state, BindingCompleteContext context, string errorText, Exception exception, bool cancel)
			: base(cancel)
		{
			this.binding = binding;
			this.state = state;
			this.context = context;
			this.error_text = errorText;
			this.exception = exception;
		}

		/// <summary>Gets the binding associated with this occurrence of a <see cref="E:System.Windows.Forms.Binding.BindingComplete" /> event.</summary>
		/// <returns>The <see cref="T:System.Windows.Forms.Binding" /> associated with this <see cref="T:System.Windows.Forms.BindingCompleteEventArgs" />.</returns>
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00012C10 File Offset: 0x00010E10
		public Binding Binding
		{
			get
			{
				return this.binding;
			}
		}

		/// <summary>Gets the direction of the binding operation.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BindingCompleteContext" /> values. </returns>
		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00012C18 File Offset: 0x00010E18
		public BindingCompleteContext BindingCompleteContext
		{
			get
			{
				return this.context;
			}
		}

		/// <summary>Gets the completion state of the binding operation.</summary>
		/// <returns>One of the <see cref="T:System.Windows.Forms.BindingCompleteState" /> values.</returns>
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00012C20 File Offset: 0x00010E20
		public BindingCompleteState BindingCompleteState
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Gets the text description of the error that occurred during the binding operation.</summary>
		/// <returns>The text description of the error that occurred during the binding operation.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00012C28 File Offset: 0x00010E28
		public string ErrorText
		{
			get
			{
				return this.error_text;
			}
		}

		/// <summary>Gets the exception that occurred during the binding operation.</summary>
		/// <returns>The <see cref="T:System.Exception" /> that occurred during the binding operation.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00012C30 File Offset: 0x00010E30
		public Exception Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00012C38 File Offset: 0x00010E38
		internal void SetErrorText(string error_text)
		{
			this.error_text = error_text;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00012C44 File Offset: 0x00010E44
		internal void SetException(Exception exception)
		{
			this.exception = exception;
		}

		// Token: 0x04000626 RID: 1574
		private Binding binding;

		// Token: 0x04000627 RID: 1575
		private BindingCompleteState state;

		// Token: 0x04000628 RID: 1576
		private BindingCompleteContext context;

		// Token: 0x04000629 RID: 1577
		private string error_text;

		// Token: 0x0400062A RID: 1578
		private Exception exception;
	}
}
