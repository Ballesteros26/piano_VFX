using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Determines how a class or field is displayed in the debugger variable windows.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000A69 RID: 2665
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Delegate, AllowMultiple = true)]
	[ComVisible(true)]
	public sealed class DebuggerDisplayAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggerDisplayAttribute" /> class. </summary>
		/// <param name="value">The string to be displayed in the value column for instances of the type; an empty string ("") causes the value column to be hidden.</param>
		// Token: 0x06006179 RID: 24953 RVA: 0x0014003E File Offset: 0x0013E23E
		public DebuggerDisplayAttribute(string value)
		{
			if (value == null)
			{
				this.value = "";
			}
			else
			{
				this.value = value;
			}
			this.name = "";
			this.type = "";
		}

		/// <summary>Gets the string to display in the value column of the debugger variable windows.</summary>
		/// <returns>The string to display in the value column of the debugger variable.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x0600617A RID: 24954 RVA: 0x00140073 File Offset: 0x0013E273
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		/// <summary>Gets or sets the name to display in the debugger variable windows.</summary>
		/// <returns>The name to display in the debugger variable windows.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x0600617B RID: 24955 RVA: 0x0014007B File Offset: 0x0013E27B
		// (set) Token: 0x0600617C RID: 24956 RVA: 0x00140083 File Offset: 0x0013E283
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the string to display in the type column of the debugger variable windows.</summary>
		/// <returns>The string to display in the type column of the debugger variable windows.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x0600617D RID: 24957 RVA: 0x0014008C File Offset: 0x0013E28C
		// (set) Token: 0x0600617E RID: 24958 RVA: 0x00140094 File Offset: 0x0013E294
		public string Type
		{
			get
			{
				return this.type;
			}
			set
			{
				this.type = value;
			}
		}

		/// <summary>Gets or sets the type of the attribute's target.</summary>
		/// <returns>The attribute's target type.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="P:System.Diagnostics.DebuggerDisplayAttribute.Target" /> is set to null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x06006180 RID: 24960 RVA: 0x001400C6 File Offset: 0x0013E2C6
		// (set) Token: 0x0600617F RID: 24959 RVA: 0x0014009D File Offset: 0x0013E29D
		public Type Target
		{
			get
			{
				return this.target;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.targetName = value.AssemblyQualifiedName;
				this.target = value;
			}
		}

		/// <summary>Gets or sets the type name of the attribute's target.</summary>
		/// <returns>The name of the attribute's target type.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x06006181 RID: 24961 RVA: 0x001400CE File Offset: 0x0013E2CE
		// (set) Token: 0x06006182 RID: 24962 RVA: 0x001400D6 File Offset: 0x0013E2D6
		public string TargetTypeName
		{
			get
			{
				return this.targetName;
			}
			set
			{
				this.targetName = value;
			}
		}

		// Token: 0x040030B2 RID: 12466
		private string name;

		// Token: 0x040030B3 RID: 12467
		private string value;

		// Token: 0x040030B4 RID: 12468
		private string type;

		// Token: 0x040030B5 RID: 12469
		private string targetName;

		// Token: 0x040030B6 RID: 12470
		private Type target;
	}
}
