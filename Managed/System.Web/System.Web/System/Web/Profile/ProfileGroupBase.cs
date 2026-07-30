using System;

namespace System.Web.Profile
{
	/// <summary>Provides untyped access to grouped ASP.NET profile property values.</summary>
	// Token: 0x02000500 RID: 1280
	public class ProfileGroupBase
	{
		/// <summary>Gets or sets a grouped profile property value indexed by the property name.</summary>
		/// <returns>The value of the specified grouped profile property.</returns>
		/// <param name="propertyName">The name of the grouped profile property.</param>
		// Token: 0x170011BF RID: 4543
		public object this[string propertyName]
		{
			get
			{
				return this._Parent[this._MyName + propertyName];
			}
			set
			{
				this._Parent[this._MyName + propertyName] = value;
			}
		}

		/// <summary>Gets the value of a grouped profile property.</summary>
		/// <returns>The value of the grouped profile property typed as object.</returns>
		/// <param name="propertyName">The name of the grouped profile property.</param>
		// Token: 0x06003920 RID: 14624 RVA: 0x00099CA5 File Offset: 0x00097EA5
		public object GetPropertyValue(string propertyName)
		{
			return this._Parent[this._MyName + propertyName];
		}

		/// <summary>Sets the value of a grouped profile property.</summary>
		/// <param name="propertyName">The name of the grouped property to set.</param>
		/// <param name="propertyValue">The value to assign to the grouped property.</param>
		// Token: 0x06003921 RID: 14625 RVA: 0x00099CBE File Offset: 0x00097EBE
		public void SetPropertyValue(string propertyName, object propertyValue)
		{
			this._Parent[this._MyName + propertyName] = propertyValue;
		}

		/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileGroupBase" /> class.</summary>
		// Token: 0x06003922 RID: 14626 RVA: 0x00099CD8 File Offset: 0x00097ED8
		public ProfileGroupBase()
		{
			this._Parent = null;
			this._MyName = null;
		}

		/// <summary>Used by ASP.NET to initialize the grouped profile property values and information.</summary>
		/// <param name="parent">The class that inherits <see cref="T:System.Web.Profile.ProfileBase" /> that is assigned to the <see cref="P:System.Web.HttpContext.Profile" /> property.</param>
		/// <param name="myName">The name of the profile property group.</param>
		// Token: 0x06003923 RID: 14627 RVA: 0x00099CEE File Offset: 0x00097EEE
		public void Init(ProfileBase parent, string myName)
		{
			if (this._Parent == null)
			{
				this._Parent = parent;
				this._MyName = myName + ".";
			}
		}

		// Token: 0x04001F0E RID: 7950
		private string _MyName;

		// Token: 0x04001F0F RID: 7951
		private ProfileBase _Parent;
	}
}
