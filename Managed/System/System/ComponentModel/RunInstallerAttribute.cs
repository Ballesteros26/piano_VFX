using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should be invoked when the assembly is installed.</summary>
	// Token: 0x020002CF RID: 719
	[AttributeUsage(AttributeTargets.Class)]
	public class RunInstallerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RunInstallerAttribute" /> class.</summary>
		/// <param name="runInstaller">true if an installer should be invoked during installation of an assembly; otherwise, false. </param>
		// Token: 0x060016F9 RID: 5881 RVA: 0x0005BB60 File Offset: 0x00059D60
		public RunInstallerAttribute(bool runInstaller)
		{
			this.runInstaller = runInstaller;
		}

		/// <summary>Gets a value indicating whether an installer should be invoked during installation of an assembly.</summary>
		/// <returns>true if an installer should be invoked during installation of an assembly; otherwise, false.</returns>
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x0005BB6F File Offset: 0x00059D6F
		public bool RunInstaller
		{
			get
			{
				return this.runInstaller;
			}
		}

		/// <summary>Determines whether the value of the specified <see cref="T:System.ComponentModel.RunInstallerAttribute" /> is equivalent to the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.RunInstallerAttribute" /> is equal to the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The object to compare.</param>
		// Token: 0x060016FB RID: 5883 RVA: 0x0005BB78 File Offset: 0x00059D78
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			RunInstallerAttribute runInstallerAttribute = obj as RunInstallerAttribute;
			return runInstallerAttribute != null && runInstallerAttribute.RunInstaller == this.runInstaller;
		}

		/// <summary>Generates a hash code for the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.RunInstallerAttribute" />.</returns>
		// Token: 0x060016FC RID: 5884 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x060016FD RID: 5885 RVA: 0x0005BBA5 File Offset: 0x00059DA5
		public override bool IsDefaultAttribute()
		{
			return this.Equals(RunInstallerAttribute.Default);
		}

		// Token: 0x040013E1 RID: 5089
		private bool runInstaller;

		/// <summary>Specifies that the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should be invoked when the assembly is installed. This static field is read-only.</summary>
		// Token: 0x040013E2 RID: 5090
		public static readonly RunInstallerAttribute Yes = new RunInstallerAttribute(true);

		/// <summary>Specifies that the Visual Studio Custom Action Installer or the Installutil.exe (Installer Tool) should not be invoked when the assembly is installed. This static field is read-only.</summary>
		// Token: 0x040013E3 RID: 5091
		public static readonly RunInstallerAttribute No = new RunInstallerAttribute(false);

		/// <summary>Specifies the default visiblity, which is <see cref="F:System.ComponentModel.RunInstallerAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x040013E4 RID: 5092
		public static readonly RunInstallerAttribute Default = RunInstallerAttribute.No;
	}
}
