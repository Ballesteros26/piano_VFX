using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	/// <summary>Contains information about a single data-binding expression in an ASP.NET server control, which allows rapid-application development (RAD) designers, such as Microsoft Visual Studio, to create data-binding expressions at design time. This class cannot be inherited.</summary>
	// Token: 0x020001BF RID: 447
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class DataBinding
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.DataBinding" /> class.</summary>
		/// <param name="propertyName">The property to bind data to. </param>
		/// <param name="propertyType">The .NET Framework type of the property to bind data to. </param>
		/// <param name="expression">The data-binding expression to be evaluated. </param>
		// Token: 0x06001228 RID: 4648 RVA: 0x00032563 File Offset: 0x00030763
		public DataBinding(string propertyName, Type propertyType, string expression)
		{
			this.propertyName = propertyName;
			this.propertyType = propertyType;
			this.expression = expression;
		}

		/// <summary>Gets or sets the data-binding expression to be evaluated.</summary>
		/// <returns>The data-binding expression to be evaluated.</returns>
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00032580 File Offset: 0x00030780
		// (set) Token: 0x0600122A RID: 4650 RVA: 0x00032588 File Offset: 0x00030788
		public string Expression
		{
			get
			{
				return this.expression;
			}
			set
			{
				this.expression = value;
			}
		}

		/// <summary>Gets the name of the ASP.NET server control property to bind data to.</summary>
		/// <returns>The property to bind data to.</returns>
		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00032591 File Offset: 0x00030791
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		/// <summary>Gets the .NET Framework type of the data-bound ASP.NET server control property.</summary>
		/// <returns>The .NET Framework type of the data-bound property.</returns>
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00032599 File Offset: 0x00030799
		public Type PropertyType
		{
			get
			{
				return this.propertyType;
			}
		}

		/// <summary>Determines whether the specified object is the same instance of the <see cref="T:System.Web.UI.DataBinding" /> class as the current object.</summary>
		/// <returns>true if the data-binding property names match; otherwise, false.</returns>
		/// <param name="obj">The object to compare against the current <see cref="T:System.Web.UI.DataBinding" />. </param>
		// Token: 0x0600122D RID: 4653 RVA: 0x000325A4 File Offset: 0x000307A4
		public override bool Equals(object obj)
		{
			DataBinding dataBinding = obj as DataBinding;
			return dataBinding != null && (dataBinding.Expression == this.expression && dataBinding.PropertyName == this.propertyName) && dataBinding.PropertyType == this.propertyType;
		}

		/// <summary>Retrieves the hash code for an instance of the <see cref="T:System.Web.UI.DataBinding" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600122E RID: 4654 RVA: 0x000325F6 File Offset: 0x000307F6
		public override int GetHashCode()
		{
			return this.propertyName.GetHashCode() + (this.propertyType.GetHashCode() << 1) + (this.expression.GetHashCode() << 2);
		}

		// Token: 0x04001414 RID: 5140
		private string propertyName;

		// Token: 0x04001415 RID: 5141
		private Type propertyType;

		// Token: 0x04001416 RID: 5142
		private string expression;
	}
}
