using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that an object has no subproperties capable of being edited. This class cannot be inherited.</summary>
	// Token: 0x02000290 RID: 656
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ImmutableObjectAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ImmutableObjectAttribute" /> class.</summary>
		/// <param name="immutable">true if the object is immutable; otherwise, false. </param>
		// Token: 0x0600148D RID: 5261 RVA: 0x00052D5F File Offset: 0x00050F5F
		public ImmutableObjectAttribute(bool immutable)
		{
			this.immutable = immutable;
		}

		/// <summary>Gets whether the object is immutable.</summary>
		/// <returns>true if the object is immutable; otherwise, false.</returns>
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x00052D75 File Offset: 0x00050F75
		public bool Immutable
		{
			get
			{
				return this.immutable;
			}
		}

		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x0600148F RID: 5263 RVA: 0x00052D80 File Offset: 0x00050F80
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ImmutableObjectAttribute immutableObjectAttribute = obj as ImmutableObjectAttribute;
			return immutableObjectAttribute != null && immutableObjectAttribute.Immutable == this.immutable;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</returns>
		// Token: 0x06001490 RID: 5264 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Indicates whether the value of this instance is the default value.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06001491 RID: 5265 RVA: 0x00052DAD File Offset: 0x00050FAD
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ImmutableObjectAttribute.Default);
		}

		/// <summary>Specifies that an object has no subproperties that can be edited. This static field is read-only.</summary>
		// Token: 0x040012EB RID: 4843
		public static readonly ImmutableObjectAttribute Yes = new ImmutableObjectAttribute(true);

		/// <summary>Specifies that an object has at least one editable subproperty. This static field is read-only.</summary>
		// Token: 0x040012EC RID: 4844
		public static readonly ImmutableObjectAttribute No = new ImmutableObjectAttribute(false);

		/// <summary>Represents the default value for <see cref="T:System.ComponentModel.ImmutableObjectAttribute" />.</summary>
		// Token: 0x040012ED RID: 4845
		public static readonly ImmutableObjectAttribute Default = ImmutableObjectAttribute.No;

		// Token: 0x040012EE RID: 4846
		private bool immutable = true;
	}
}
