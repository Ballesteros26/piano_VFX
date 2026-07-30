using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyPropertyChanged.PropertyChanged" /> event.</summary>
	// Token: 0x020002BD RID: 701
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class PropertyChangedEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyChangedEventArgs" /> class.</summary>
		/// <param name="propertyName">The name of the property that changed. </param>
		// Token: 0x06001603 RID: 5635 RVA: 0x00056B7E File Offset: 0x00054D7E
		public PropertyChangedEventArgs(string propertyName)
		{
			this.propertyName = propertyName;
		}

		/// <summary>Gets the name of the property that changed.</summary>
		/// <returns>The name of the property that changed.</returns>
		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x00056B8D File Offset: 0x00054D8D
		public virtual string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04001386 RID: 4998
		private readonly string propertyName;
	}
}
