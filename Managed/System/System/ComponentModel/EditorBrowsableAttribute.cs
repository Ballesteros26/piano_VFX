using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that a property or method is viewable in an editor. This class cannot be inherited.</summary>
	// Token: 0x02000269 RID: 617
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
	public sealed class EditorBrowsableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorBrowsableAttribute" /> class with an <see cref="T:System.ComponentModel.EditorBrowsableState" />.</summary>
		/// <param name="state">The <see cref="T:System.ComponentModel.EditorBrowsableState" /> to set <see cref="P:System.ComponentModel.EditorBrowsableAttribute.State" /> to. </param>
		// Token: 0x060013B7 RID: 5047 RVA: 0x00051BF3 File Offset: 0x0004FDF3
		public EditorBrowsableAttribute(EditorBrowsableState state)
		{
			this.browsableState = state;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.EditorBrowsableAttribute" /> class with <see cref="P:System.ComponentModel.EditorBrowsableAttribute.State" /> set to the default state.</summary>
		// Token: 0x060013B8 RID: 5048 RVA: 0x00051C02 File Offset: 0x0004FE02
		public EditorBrowsableAttribute()
			: this(EditorBrowsableState.Always)
		{
		}

		/// <summary>Gets the browsable state of the property or method.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EditorBrowsableState" /> that is the browsable state of the property or method.</returns>
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060013B9 RID: 5049 RVA: 0x00051C0B File Offset: 0x0004FE0B
		public EditorBrowsableState State
		{
			get
			{
				return this.browsableState;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.EditorBrowsableAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x060013BA RID: 5050 RVA: 0x00051C14 File Offset: 0x0004FE14
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			EditorBrowsableAttribute editorBrowsableAttribute = obj as EditorBrowsableAttribute;
			return editorBrowsableAttribute != null && editorBrowsableAttribute.browsableState == this.browsableState;
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040012D1 RID: 4817
		private EditorBrowsableState browsableState;
	}
}
