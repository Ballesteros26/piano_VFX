using System;

namespace System.ComponentModel
{
	/// <summary>Specifies when a component property can be bound to an application setting.</summary>
	// Token: 0x020002D3 RID: 723
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class SettingsBindableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.SettingsBindableAttribute" /> class. </summary>
		/// <param name="bindable">true to specify that a property is appropriate to bind settings to; otherwise, false.</param>
		// Token: 0x0600170C RID: 5900 RVA: 0x0005BC55 File Offset: 0x00059E55
		public SettingsBindableAttribute(bool bindable)
		{
			this._bindable = bindable;
		}

		/// <summary>Gets a value indicating whether a property is appropriate to bind settings to. </summary>
		/// <returns>true if the property is appropriate to bind settings to; otherwise, false.</returns>
		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x0005BC64 File Offset: 0x00059E64
		public bool Bindable
		{
			get
			{
				return this._bindable;
			}
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.SettingsBindableAttribute" /> objects are equal.</summary>
		/// <returns>true if <paramref name="obj" /> equals the type and value of this instance; otherwise, false.</returns>
		/// <param name="obj">The value to compare to.</param>
		// Token: 0x0600170E RID: 5902 RVA: 0x0005BC6C File Offset: 0x00059E6C
		public override bool Equals(object obj)
		{
			return obj == this || (obj != null && obj is SettingsBindableAttribute && ((SettingsBindableAttribute)obj).Bindable == this._bindable);
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600170F RID: 5903 RVA: 0x0005BC94 File Offset: 0x00059E94
		public override int GetHashCode()
		{
			return this._bindable.GetHashCode();
		}

		/// <summary>Specifies that a property is appropriate to bind settings to.</summary>
		// Token: 0x040013E6 RID: 5094
		public static readonly SettingsBindableAttribute Yes = new SettingsBindableAttribute(true);

		/// <summary>Specifies that a property is not appropriate to bind settings to.</summary>
		// Token: 0x040013E7 RID: 5095
		public static readonly SettingsBindableAttribute No = new SettingsBindableAttribute(false);

		// Token: 0x040013E8 RID: 5096
		private bool _bindable;
	}
}
