using System;
using System.Reflection;

namespace System.ComponentModel
{
	/// <summary>Identifies the property tab or tabs to display for the specified class or classes.</summary>
	// Token: 0x020002FB RID: 763
	[AttributeUsage(AttributeTargets.All)]
	public class PropertyTabAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class.</summary>
		// Token: 0x06001899 RID: 6297 RVA: 0x00068BB5 File Offset: 0x00066DB5
		public PropertyTabAttribute()
		{
			this.tabScopes = new PropertyTabScope[0];
			this.tabClassNames = new string[0];
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class using the specified type of tab.</summary>
		/// <param name="tabClass">The type of tab to create. </param>
		// Token: 0x0600189A RID: 6298 RVA: 0x00068BD5 File Offset: 0x00066DD5
		public PropertyTabAttribute(Type tabClass)
			: this(tabClass, PropertyTabScope.Component)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class using the specified tab class name.</summary>
		/// <param name="tabClassName">The assembly qualified name of the type of tab to create. For an example of this format convention, see <see cref="P:System.Type.AssemblyQualifiedName" />. </param>
		// Token: 0x0600189B RID: 6299 RVA: 0x00068BDF File Offset: 0x00066DDF
		public PropertyTabAttribute(string tabClassName)
			: this(tabClassName, PropertyTabScope.Component)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class using the specified type of tab and tab scope.</summary>
		/// <param name="tabClass">The type of tab to create. </param>
		/// <param name="tabScope">A <see cref="T:System.ComponentModel.PropertyTabScope" /> that indicates the scope of this tab. If the scope is <see cref="F:System.ComponentModel.PropertyTabScope.Component" />, it is shown only for components with the corresponding <see cref="T:System.ComponentModel.PropertyTabAttribute" />. If it is <see cref="F:System.ComponentModel.PropertyTabScope.Document" />, it is shown for all components on the document. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="tabScope" /> is not <see cref="F:System.ComponentModel.PropertyTabScope.Document" /> or <see cref="F:System.ComponentModel.PropertyTabScope.Component" />.</exception>
		// Token: 0x0600189C RID: 6300 RVA: 0x00068BEC File Offset: 0x00066DEC
		public PropertyTabAttribute(Type tabClass, PropertyTabScope tabScope)
		{
			this.tabClasses = new Type[] { tabClass };
			if (tabScope < PropertyTabScope.Document)
			{
				throw new ArgumentException(global::SR.GetString("Scope must be PropertyTabScope.Document or PropertyTabScope.Component"), "tabScope");
			}
			this.tabScopes = new PropertyTabScope[] { tabScope };
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> class using the specified tab class name and tab scope.</summary>
		/// <param name="tabClassName">The assembly qualified name of the type of tab to create. For an example of this format convention, see <see cref="P:System.Type.AssemblyQualifiedName" />. </param>
		/// <param name="tabScope">A <see cref="T:System.ComponentModel.PropertyTabScope" /> that indicates the scope of this tab. If the scope is <see cref="F:System.ComponentModel.PropertyTabScope.Component" />, it is shown only for components with the corresponding <see cref="T:System.ComponentModel.PropertyTabAttribute" />. If it is <see cref="F:System.ComponentModel.PropertyTabScope.Document" />, it is shown for all components on the document. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="tabScope" /> is not <see cref="F:System.ComponentModel.PropertyTabScope.Document" /> or <see cref="F:System.ComponentModel.PropertyTabScope.Component" />.</exception>
		// Token: 0x0600189D RID: 6301 RVA: 0x00068C38 File Offset: 0x00066E38
		public PropertyTabAttribute(string tabClassName, PropertyTabScope tabScope)
		{
			this.tabClassNames = new string[] { tabClassName };
			if (tabScope < PropertyTabScope.Document)
			{
				throw new ArgumentException(global::SR.GetString("Scope must be PropertyTabScope.Document or PropertyTabScope.Component"), "tabScope");
			}
			this.tabScopes = new PropertyTabScope[] { tabScope };
		}

		/// <summary>Gets the types of tabs that this attribute uses.</summary>
		/// <returns>An array of types indicating the types of tabs that this attribute uses.</returns>
		/// <exception cref="T:System.TypeLoadException">The types specified by the <see cref="P:System.ComponentModel.PropertyTabAttribute.TabClassNames" /> property could not be found.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x0600189E RID: 6302 RVA: 0x00068C84 File Offset: 0x00066E84
		public Type[] TabClasses
		{
			get
			{
				if (this.tabClasses == null && this.tabClassNames != null)
				{
					this.tabClasses = new Type[this.tabClassNames.Length];
					for (int i = 0; i < this.tabClassNames.Length; i++)
					{
						int num = this.tabClassNames[i].IndexOf(',');
						string text = null;
						string text2;
						if (num != -1)
						{
							text2 = this.tabClassNames[i].Substring(0, num).Trim();
							text = this.tabClassNames[i].Substring(num + 1).Trim();
						}
						else
						{
							text2 = this.tabClassNames[i];
						}
						this.tabClasses[i] = Type.GetType(text2, false);
						if (this.tabClasses[i] == null)
						{
							if (text == null)
							{
								throw new TypeLoadException(global::SR.GetString("Couldn't find type {0}", new object[] { text2 }));
							}
							Assembly assembly = Assembly.Load(text);
							if (assembly != null)
							{
								this.tabClasses[i] = assembly.GetType(text2, true);
							}
						}
					}
				}
				return this.tabClasses;
			}
		}

		/// <summary>Gets the names of the tab classes that this attribute uses.</summary>
		/// <returns>The names of the tab classes that this attribute uses.</returns>
		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x0600189F RID: 6303 RVA: 0x00068D88 File Offset: 0x00066F88
		protected string[] TabClassNames
		{
			get
			{
				if (this.tabClassNames != null)
				{
					return (string[])this.tabClassNames.Clone();
				}
				return null;
			}
		}

		/// <summary>Gets an array of tab scopes of each tab of this <see cref="T:System.ComponentModel.PropertyTabAttribute" />.</summary>
		/// <returns>An array of <see cref="T:System.ComponentModel.PropertyTabScope" /> objects that indicate the scopes of the tabs.</returns>
		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060018A0 RID: 6304 RVA: 0x00068DA4 File Offset: 0x00066FA4
		public PropertyTabScope[] TabScopes
		{
			get
			{
				return this.tabScopes;
			}
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified object.</summary>
		/// <returns>true if <paramref name="other" /> refers to the same <see cref="T:System.ComponentModel.PropertyTabAttribute" /> instance; otherwise, false.</returns>
		/// <param name="other">An object to compare to this instance, or null.</param>
		/// <exception cref="T:System.TypeLoadException">The types specified by the <see cref="P:System.ComponentModel.PropertyTabAttribute.TabClassNames" /> property of the<paramref name=" other" /> parameter could not be found.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060018A1 RID: 6305 RVA: 0x00068DAC File Offset: 0x00066FAC
		public override bool Equals(object other)
		{
			return other is PropertyTabAttribute && this.Equals((PropertyTabAttribute)other);
		}

		/// <summary>Returns a value indicating whether this instance is equal to a specified attribute.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.PropertyTabAttribute" /> instances are equal; otherwise, false.</returns>
		/// <param name="other">A <see cref="T:System.ComponentModel.PropertyTabAttribute" /> to compare to this instance, or null.</param>
		/// <exception cref="T:System.TypeLoadException">The types specified by the <see cref="P:System.ComponentModel.PropertyTabAttribute.TabClassNames" /> property of the <paramref name="other" /> parameter cannot be found.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060018A2 RID: 6306 RVA: 0x00068DC4 File Offset: 0x00066FC4
		public bool Equals(PropertyTabAttribute other)
		{
			if (other == this)
			{
				return true;
			}
			if (other.TabClasses.Length != this.TabClasses.Length || other.TabScopes.Length != this.TabScopes.Length)
			{
				return false;
			}
			for (int i = 0; i < this.TabClasses.Length; i++)
			{
				if (this.TabClasses[i] != other.TabClasses[i] || this.TabScopes[i] != other.TabScopes[i])
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets the hash code for this object.</summary>
		/// <returns>The hash code for the object the attribute belongs to.</returns>
		// Token: 0x060018A3 RID: 6307 RVA: 0x0004C98A File Offset: 0x0004AB8A
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Initializes the attribute using the specified names of tab classes and array of tab scopes.</summary>
		/// <param name="tabClassNames">An array of fully qualified type names of the types to create for tabs on the Properties window. </param>
		/// <param name="tabScopes">The scope of each tab. If the scope is <see cref="F:System.ComponentModel.PropertyTabScope.Component" />, it is shown only for components with the corresponding <see cref="T:System.ComponentModel.PropertyTabAttribute" />. If it is <see cref="F:System.ComponentModel.PropertyTabScope.Document" />, it is shown for all components on the document. </param>
		/// <exception cref="T:System.ArgumentException">One or more of the values in <paramref name="tabScopes" /> is not <see cref="F:System.ComponentModel.PropertyTabScope.Document" /> or <see cref="F:System.ComponentModel.PropertyTabScope.Component" />.-or-The length of the <paramref name="tabClassNames" /> and <paramref name="tabScopes" /> arrays do not match.-or-<paramref name="tabClassNames" /> or <paramref name="tabScopes" /> is null.</exception>
		// Token: 0x060018A4 RID: 6308 RVA: 0x00068E3C File Offset: 0x0006703C
		protected void InitializeArrays(string[] tabClassNames, PropertyTabScope[] tabScopes)
		{
			this.InitializeArrays(tabClassNames, null, tabScopes);
		}

		/// <summary>Initializes the attribute using the specified names of tab classes and array of tab scopes.</summary>
		/// <param name="tabClasses">The types of tabs to create. </param>
		/// <param name="tabScopes">The scope of each tab. If the scope is <see cref="F:System.ComponentModel.PropertyTabScope.Component" />, it is shown only for components with the corresponding <see cref="T:System.ComponentModel.PropertyTabAttribute" />. If it is <see cref="F:System.ComponentModel.PropertyTabScope.Document" />, it is shown for all components on the document. </param>
		/// <exception cref="T:System.ArgumentException">One or more of the values in <paramref name="tabScopes" /> is not <see cref="F:System.ComponentModel.PropertyTabScope.Document" /> or <see cref="F:System.ComponentModel.PropertyTabScope.Component" />.-or-The length of the <paramref name="tabClassNames" /> and <paramref name="tabScopes" /> arrays do not match.-or-<paramref name="tabClassNames" /> or <paramref name="tabScopes" /> is null.</exception>
		// Token: 0x060018A5 RID: 6309 RVA: 0x00068E47 File Offset: 0x00067047
		protected void InitializeArrays(Type[] tabClasses, PropertyTabScope[] tabScopes)
		{
			this.InitializeArrays(null, tabClasses, tabScopes);
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00068E54 File Offset: 0x00067054
		private void InitializeArrays(string[] tabClassNames, Type[] tabClasses, PropertyTabScope[] tabScopes)
		{
			if (tabClasses != null)
			{
				if (tabScopes != null && tabClasses.Length != tabScopes.Length)
				{
					throw new ArgumentException(global::SR.GetString("tabClasses must have the same number of items as tabScopes"));
				}
				this.tabClasses = (Type[])tabClasses.Clone();
			}
			else if (tabClassNames != null)
			{
				if (tabScopes != null && tabClasses.Length != tabScopes.Length)
				{
					throw new ArgumentException(global::SR.GetString("tabClasses must have the same number of items as tabScopes"));
				}
				this.tabClassNames = (string[])tabClassNames.Clone();
				this.tabClasses = null;
			}
			else if (this.tabClasses == null && this.tabClassNames == null)
			{
				throw new ArgumentException(global::SR.GetString("An array of tab type names or tab types must be specified"));
			}
			if (tabScopes != null)
			{
				for (int i = 0; i < tabScopes.Length; i++)
				{
					if (tabScopes[i] < PropertyTabScope.Document)
					{
						throw new ArgumentException(global::SR.GetString("Scope must be PropertyTabScope.Document or PropertyTabScope.Component"));
					}
				}
				this.tabScopes = (PropertyTabScope[])tabScopes.Clone();
				return;
			}
			this.tabScopes = new PropertyTabScope[tabClasses.Length];
			for (int j = 0; j < this.TabScopes.Length; j++)
			{
				this.tabScopes[j] = PropertyTabScope.Component;
			}
		}

		// Token: 0x04001435 RID: 5173
		private PropertyTabScope[] tabScopes;

		// Token: 0x04001436 RID: 5174
		private Type[] tabClasses;

		// Token: 0x04001437 RID: 5175
		private string[] tabClassNames;
	}
}
