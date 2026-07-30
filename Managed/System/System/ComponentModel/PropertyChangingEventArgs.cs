using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	/// <summary>Provides data for the <see cref="E:System.ComponentModel.INotifyPropertyChanging.PropertyChanging" /> event. </summary>
	// Token: 0x020002BF RID: 703
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class PropertyChangingEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyChangingEventArgs" /> class. </summary>
		/// <param name="propertyName">The name of the property whose value is changing.</param>
		// Token: 0x06001609 RID: 5641 RVA: 0x00056B95 File Offset: 0x00054D95
		public PropertyChangingEventArgs(string propertyName)
		{
			this.propertyName = propertyName;
		}

		/// <summary>Gets the name of the property whose value is changing.</summary>
		/// <returns>The name of the property whose value is changing.</returns>
		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x00056BA4 File Offset: 0x00054DA4
		public virtual string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x04001387 RID: 4999
		private readonly string propertyName;
	}
}
