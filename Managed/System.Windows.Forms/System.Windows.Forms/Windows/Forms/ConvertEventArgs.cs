using System;

namespace System.Windows.Forms
{
	/// <summary>Provides data for the <see cref="E:System.Windows.Forms.Binding.Format" /> and <see cref="E:System.Windows.Forms.Binding.Parse" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000B1 RID: 177
	public class ConvertEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.ConvertEventArgs" /> class.</summary>
		/// <param name="value">An <see cref="T:System.Object" /> that contains the value of the current property. </param>
		/// <param name="desiredType">The <see cref="T:System.Type" /> of the value. </param>
		// Token: 0x06000AF6 RID: 2806 RVA: 0x0002CF68 File Offset: 0x0002B168
		public ConvertEventArgs(object value, Type desiredType)
		{
			this.object_value = value;
			this.desired_type = desiredType;
		}

		/// <summary>Gets the data type of the desired value.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the desired value.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x0002CF80 File Offset: 0x0002B180
		public Type DesiredType
		{
			get
			{
				return this.desired_type;
			}
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Windows.Forms.ConvertEventArgs" />.</summary>
		/// <returns>The value of the <see cref="T:System.Windows.Forms.ConvertEventArgs" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000AF8 RID: 2808 RVA: 0x0002CF88 File Offset: 0x0002B188
		// (set) Token: 0x06000AF9 RID: 2809 RVA: 0x0002CF90 File Offset: 0x0002B190
		public object Value
		{
			get
			{
				return this.object_value;
			}
			set
			{
				this.object_value = value;
			}
		}

		// Token: 0x04000854 RID: 2132
		private object object_value;

		// Token: 0x04000855 RID: 2133
		private Type desired_type;
	}
}
