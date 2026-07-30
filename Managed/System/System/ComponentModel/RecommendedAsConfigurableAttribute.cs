using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that the property can be used as an application setting.</summary>
	// Token: 0x020002C6 RID: 710
	[AttributeUsage(AttributeTargets.Property)]
	[Obsolete("Use System.ComponentModel.SettingsBindableAttribute instead to work with the new settings model.")]
	public class RecommendedAsConfigurableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" /> class.</summary>
		/// <param name="recommendedAsConfigurable">true if the property this attribute is bound to can be used as an application setting; otherwise, false. </param>
		// Token: 0x06001674 RID: 5748 RVA: 0x00057BE7 File Offset: 0x00055DE7
		public RecommendedAsConfigurableAttribute(bool recommendedAsConfigurable)
		{
			this.recommendedAsConfigurable = recommendedAsConfigurable;
		}

		/// <summary>Gets a value indicating whether the property this attribute is bound to can be used as an application setting.</summary>
		/// <returns>true if the property this attribute is bound to can be used as an application setting; otherwise, false.</returns>
		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x00057BF6 File Offset: 0x00055DF6
		public bool RecommendedAsConfigurable
		{
			get
			{
				return this.recommendedAsConfigurable;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x06001676 RID: 5750 RVA: 0x00057C00 File Offset: 0x00055E00
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RecommendedAsConfigurableAttribute recommendedAsConfigurableAttribute = obj as RecommendedAsConfigurableAttribute;
			return recommendedAsConfigurableAttribute != null && recommendedAsConfigurableAttribute.RecommendedAsConfigurable == this.recommendedAsConfigurable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" />.</returns>
		// Token: 0x06001677 RID: 5751 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the value of this instance is the default value for the class.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06001678 RID: 5752 RVA: 0x00057C2D File Offset: 0x00055E2D
		public override bool IsDefaultAttribute()
		{
			return !this.recommendedAsConfigurable;
		}

		// Token: 0x0400139F RID: 5023
		private bool recommendedAsConfigurable;

		/// <summary>Specifies that a property cannot be used as an application setting. This static field is read-only.</summary>
		// Token: 0x040013A0 RID: 5024
		public static readonly RecommendedAsConfigurableAttribute No = new RecommendedAsConfigurableAttribute(false);

		/// <summary>Specifies that a property can be used as an application setting. This static field is read-only.</summary>
		// Token: 0x040013A1 RID: 5025
		public static readonly RecommendedAsConfigurableAttribute Yes = new RecommendedAsConfigurableAttribute(true);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.RecommendedAsConfigurableAttribute" />, which is <see cref="F:System.ComponentModel.RecommendedAsConfigurableAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x040013A2 RID: 5026
		public static readonly RecommendedAsConfigurableAttribute Default = RecommendedAsConfigurableAttribute.No;
	}
}
