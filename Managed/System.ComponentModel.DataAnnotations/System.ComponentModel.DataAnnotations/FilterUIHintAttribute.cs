using System;
using System.Collections.Generic;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Represents an attribute that is used to specify the filtering behavior for a column.</summary>
	// Token: 0x02000019 RID: 25
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
	public sealed class FilterUIHintAttribute : Attribute
	{
		/// <summary>Gets the name of the control to use for filtering.</summary>
		/// <returns>The name of the control to use for filtering.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000034F3 File Offset: 0x000016F3
		public string FilterUIHint
		{
			get
			{
				return this._implementation.UIHint;
			}
		}

		/// <summary>Gets the name of the presentation layer that supports this control.</summary>
		/// <returns>The name of the presentation layer that supports this control.</returns>
		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003500 File Offset: 0x00001700
		public string PresentationLayer
		{
			get
			{
				return this._implementation.PresentationLayer;
			}
		}

		/// <summary>Gets the name/value pairs that are used as parameters in the control's constructor.</summary>
		/// <returns>The name/value pairs that are used as parameters in the control's constructor.</returns>
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000098 RID: 152 RVA: 0x0000350D File Offset: 0x0000170D
		public IDictionary<string, object> ControlParameters
		{
			get
			{
				return this._implementation.ControlParameters;
			}
		}

		/// <summary>Returns the unique identifier for this attribute instance.</summary>
		/// <returns>This attribuet instance unique identifier.</returns>
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000099 RID: 153 RVA: 0x0000351A File Offset: 0x0000171A
		public override object TypeId
		{
			get
			{
				return this;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.FilterUIHintAttribute" /> class by using the filter UI hint.</summary>
		/// <param name="filterUIHint">The name of the control to use for filtering.</param>
		// Token: 0x0600009A RID: 154 RVA: 0x0000351D File Offset: 0x0000171D
		public FilterUIHintAttribute(string filterUIHint)
			: this(filterUIHint, null, new object[0])
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.FilterUIHintAttribute" /> class by using the filter UI hint and presentation layer name.</summary>
		/// <param name="filterUIHint">The name of the control to use for filtering.</param>
		/// <param name="presentationLayer">The name of the presentation layer that supports this control.</param>
		// Token: 0x0600009B RID: 155 RVA: 0x0000352D File Offset: 0x0000172D
		public FilterUIHintAttribute(string filterUIHint, string presentationLayer)
			: this(filterUIHint, presentationLayer, new object[0])
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.FilterUIHintAttribute" /> class by using the filter UI hint, presentation layer name, and control parameters.</summary>
		/// <param name="filterUIHint">The name of the control to use for filtering.</param>
		/// <param name="presentationLayer">The name of the presentation layer that supports this control.</param>
		/// <param name="controlParameters">The list of parameters for the control.</param>
		// Token: 0x0600009C RID: 156 RVA: 0x0000353D File Offset: 0x0000173D
		public FilterUIHintAttribute(string filterUIHint, string presentationLayer, params object[] controlParameters)
		{
			this._implementation = new UIHintAttribute.UIHintImplementation(filterUIHint, presentationLayer, controlParameters);
		}

		/// <summary>Returns the hash code for this attribute instance.</summary>
		/// <returns>This attribute insatnce hash code.</returns>
		// Token: 0x0600009D RID: 157 RVA: 0x00003553 File Offset: 0x00001753
		public override int GetHashCode()
		{
			return this._implementation.GetHashCode();
		}

		/// <summary>Returns a value that indicates whether this attribute instance is equal to a specified object.</summary>
		/// <returns>True if the passed object is equal to this attribute instance; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this attribute instance.</param>
		// Token: 0x0600009E RID: 158 RVA: 0x00003560 File Offset: 0x00001760
		public override bool Equals(object obj)
		{
			FilterUIHintAttribute filterUIHintAttribute = obj as FilterUIHintAttribute;
			return filterUIHintAttribute != null && this._implementation.Equals(filterUIHintAttribute._implementation);
		}

		// Token: 0x04000076 RID: 118
		private UIHintAttribute.UIHintImplementation _implementation;
	}
}
