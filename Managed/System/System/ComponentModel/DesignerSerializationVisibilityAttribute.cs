using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the type of persistence to use when serializing a property on a component at design time.</summary>
	// Token: 0x02000263 RID: 611
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class DesignerSerializationVisibilityAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerSerializationVisibilityAttribute" /> class using the specified <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> value.</summary>
		/// <param name="visibility">One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> values. </param>
		// Token: 0x06001390 RID: 5008 RVA: 0x0005193D File Offset: 0x0004FB3D
		public DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility visibility)
		{
			this.visibility = visibility;
		}

		/// <summary>Gets a value indicating the basic serialization mode a serializer should use when determining whether and how to persist the value of a property.</summary>
		/// <returns>One of the <see cref="T:System.ComponentModel.DesignerSerializationVisibility" /> values. The default is <see cref="F:System.ComponentModel.DesignerSerializationVisibility.Visible" />.</returns>
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06001391 RID: 5009 RVA: 0x0005194C File Offset: 0x0004FB4C
		public DesignerSerializationVisibility Visibility
		{
			get
			{
				return this.visibility;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x06001392 RID: 5010 RVA: 0x00051954 File Offset: 0x0004FB54
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = obj as DesignerSerializationVisibilityAttribute;
			return designerSerializationVisibilityAttribute != null && designerSerializationVisibilityAttribute.Visibility == this.visibility;
		}

		/// <summary>Returns the hash code for this object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06001393 RID: 5011 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the attribute is set to the default value; otherwise, false.</returns>
		// Token: 0x06001394 RID: 5012 RVA: 0x00051981 File Offset: 0x0004FB81
		public override bool IsDefaultAttribute()
		{
			return this.Equals(DesignerSerializationVisibilityAttribute.Default);
		}

		/// <summary>Specifies that a serializer should serialize the contents of the property, rather than the property itself. This field is read-only.</summary>
		// Token: 0x040012C5 RID: 4805
		public static readonly DesignerSerializationVisibilityAttribute Content = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Content);

		/// <summary>Specifies that a serializer should not serialize the value of the property. This static field is read-only.</summary>
		// Token: 0x040012C6 RID: 4806
		public static readonly DesignerSerializationVisibilityAttribute Hidden = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Hidden);

		/// <summary>Specifies that a serializer should be allowed to serialize the value of the property. This static field is read-only.</summary>
		// Token: 0x040012C7 RID: 4807
		public static readonly DesignerSerializationVisibilityAttribute Visible = new DesignerSerializationVisibilityAttribute(DesignerSerializationVisibility.Visible);

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.DesignerSerializationVisibilityAttribute.Visible" />, that is, a visual designer uses default rules to generate the value of a property. This static field is read-only.</summary>
		// Token: 0x040012C8 RID: 4808
		public static readonly DesignerSerializationVisibilityAttribute Default = DesignerSerializationVisibilityAttribute.Visible;

		// Token: 0x040012C9 RID: 4809
		private DesignerSerializationVisibility visibility;
	}
}
