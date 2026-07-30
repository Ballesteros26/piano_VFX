using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the installer for a type that installs components.</summary>
	// Token: 0x02000292 RID: 658
	[AttributeUsage(AttributeTargets.Class)]
	public class InstallerTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InstallerTypeAttribute" /> class, when given a <see cref="T:System.Type" /> that represents the installer for a component.</summary>
		/// <param name="installerType">A <see cref="T:System.Type" /> that represents the installer for the component this attribute is bound to. This class must implement <see cref="T:System.ComponentModel.Design.IDesigner" />. </param>
		// Token: 0x06001495 RID: 5269 RVA: 0x00052DF3 File Offset: 0x00050FF3
		public InstallerTypeAttribute(Type installerType)
		{
			this._typeName = installerType.AssemblyQualifiedName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InstallerTypeAttribute" /> class with the name of the component's installer type.</summary>
		/// <param name="typeName">The name of a <see cref="T:System.Type" /> that represents the installer for the component this attribute is bound to. This class must implement <see cref="T:System.ComponentModel.Design.IDesigner" />. </param>
		// Token: 0x06001496 RID: 5270 RVA: 0x00052E07 File Offset: 0x00051007
		public InstallerTypeAttribute(string typeName)
		{
			this._typeName = typeName;
		}

		/// <summary>Gets the type of installer associated with this attribute.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of installer associated with this attribute, or null if an installer does not exist.</returns>
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x00052E16 File Offset: 0x00051016
		public virtual Type InstallerType
		{
			get
			{
				return Type.GetType(this._typeName);
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.InstallerTypeAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001498 RID: 5272 RVA: 0x00052E24 File Offset: 0x00051024
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			InstallerTypeAttribute installerTypeAttribute = obj as InstallerTypeAttribute;
			return installerTypeAttribute != null && installerTypeAttribute._typeName == this._typeName;
		}

		/// <summary>Returns the hashcode for this object.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.InstallerTypeAttribute" />.</returns>
		// Token: 0x06001499 RID: 5273 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x040012F0 RID: 4848
		private string _typeName;
	}
}
