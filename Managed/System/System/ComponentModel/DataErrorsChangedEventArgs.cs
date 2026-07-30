using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyDataErrorInfo.ErrorsChanged" /> event.</summary>
	// Token: 0x02000250 RID: 592
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class DataErrorsChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataErrorsChangedEventArgs" /> class.</summary>
		/// <param name="propertyName">The name of the property that has an error.  null or <see cref="F:System.String.Empty" /> if the error is object-level.</param>
		// Token: 0x06001311 RID: 4881 RVA: 0x000508A4 File Offset: 0x0004EAA4
		public DataErrorsChangedEventArgs(string propertyName)
		{
			this.propertyName = propertyName;
		}

		/// <summary>Gets the name of the property that has an error.</summary>
		/// <returns>The name of the property that has an error. null or <see cref="F:System.String.Empty" /> if the error is object-level.</returns>
		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001312 RID: 4882 RVA: 0x000508B3 File Offset: 0x0004EAB3
		public virtual string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04001295 RID: 4757
		private readonly string propertyName;
	}
}
