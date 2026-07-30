using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the name of the category in which to group the property or event when displayed in a <see cref="T:System.Windows.Forms.PropertyGrid" /> control set to Categorized mode.</summary>
	// Token: 0x0200023D RID: 573
	[AttributeUsage(AttributeTargets.All)]
	public class CategoryAttribute : Attribute
	{
		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Action category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the action category.</returns>
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x0004DEFE File Offset: 0x0004C0FE
		public static CategoryAttribute Action
		{
			get
			{
				if (CategoryAttribute.action == null)
				{
					CategoryAttribute.action = new CategoryAttribute("Action");
				}
				return CategoryAttribute.action;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Appearance category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the appearance category.</returns>
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x0004DF21 File Offset: 0x0004C121
		public static CategoryAttribute Appearance
		{
			get
			{
				if (CategoryAttribute.appearance == null)
				{
					CategoryAttribute.appearance = new CategoryAttribute("Appearance");
				}
				return CategoryAttribute.appearance;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Asynchronous category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the asynchronous category.</returns>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x0004DF44 File Offset: 0x0004C144
		public static CategoryAttribute Asynchronous
		{
			get
			{
				if (CategoryAttribute.asynchronous == null)
				{
					CategoryAttribute.asynchronous = new CategoryAttribute("Asynchronous");
				}
				return CategoryAttribute.asynchronous;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Behavior category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the behavior category.</returns>
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x0004DF67 File Offset: 0x0004C167
		public static CategoryAttribute Behavior
		{
			get
			{
				if (CategoryAttribute.behavior == null)
				{
					CategoryAttribute.behavior = new CategoryAttribute("Behavior");
				}
				return CategoryAttribute.behavior;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Data category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the data category.</returns>
		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x0004DF8A File Offset: 0x0004C18A
		public static CategoryAttribute Data
		{
			get
			{
				if (CategoryAttribute.data == null)
				{
					CategoryAttribute.data = new CategoryAttribute("Data");
				}
				return CategoryAttribute.data;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Default category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the default category.</returns>
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06001299 RID: 4761 RVA: 0x0004DFAD File Offset: 0x0004C1AD
		public static CategoryAttribute Default
		{
			get
			{
				if (CategoryAttribute.defAttr == null)
				{
					CategoryAttribute.defAttr = new CategoryAttribute();
				}
				return CategoryAttribute.defAttr;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Design category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the design category.</returns>
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x0004DFCB File Offset: 0x0004C1CB
		public static CategoryAttribute Design
		{
			get
			{
				if (CategoryAttribute.design == null)
				{
					CategoryAttribute.design = new CategoryAttribute("Design");
				}
				return CategoryAttribute.design;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the DragDrop category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the drag-and-drop category.</returns>
		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600129B RID: 4763 RVA: 0x0004DFEE File Offset: 0x0004C1EE
		public static CategoryAttribute DragDrop
		{
			get
			{
				if (CategoryAttribute.dragDrop == null)
				{
					CategoryAttribute.dragDrop = new CategoryAttribute("DragDrop");
				}
				return CategoryAttribute.dragDrop;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Focus category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the focus category.</returns>
		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x0600129C RID: 4764 RVA: 0x0004E011 File Offset: 0x0004C211
		public static CategoryAttribute Focus
		{
			get
			{
				if (CategoryAttribute.focus == null)
				{
					CategoryAttribute.focus = new CategoryAttribute("Focus");
				}
				return CategoryAttribute.focus;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Format category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the format category.</returns>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x0004E034 File Offset: 0x0004C234
		public static CategoryAttribute Format
		{
			get
			{
				if (CategoryAttribute.format == null)
				{
					CategoryAttribute.format = new CategoryAttribute("Format");
				}
				return CategoryAttribute.format;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Key category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the key category.</returns>
		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600129E RID: 4766 RVA: 0x0004E057 File Offset: 0x0004C257
		public static CategoryAttribute Key
		{
			get
			{
				if (CategoryAttribute.key == null)
				{
					CategoryAttribute.key = new CategoryAttribute("Key");
				}
				return CategoryAttribute.key;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Layout category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the layout category.</returns>
		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600129F RID: 4767 RVA: 0x0004E07A File Offset: 0x0004C27A
		public static CategoryAttribute Layout
		{
			get
			{
				if (CategoryAttribute.layout == null)
				{
					CategoryAttribute.layout = new CategoryAttribute("Layout");
				}
				return CategoryAttribute.layout;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the Mouse category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the mouse category.</returns>
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060012A0 RID: 4768 RVA: 0x0004E09D File Offset: 0x0004C29D
		public static CategoryAttribute Mouse
		{
			get
			{
				if (CategoryAttribute.mouse == null)
				{
					CategoryAttribute.mouse = new CategoryAttribute("Mouse");
				}
				return CategoryAttribute.mouse;
			}
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.CategoryAttribute" /> representing the WindowStyle category.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.CategoryAttribute" /> for the window style category.</returns>
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x0004E0C0 File Offset: 0x0004C2C0
		public static CategoryAttribute WindowStyle
		{
			get
			{
				if (CategoryAttribute.windowStyle == null)
				{
					CategoryAttribute.windowStyle = new CategoryAttribute("WindowStyle");
				}
				return CategoryAttribute.windowStyle;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CategoryAttribute" /> class using the category name Default.</summary>
		// Token: 0x060012A2 RID: 4770 RVA: 0x0004E0E3 File Offset: 0x0004C2E3
		public CategoryAttribute()
			: this("Default")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CategoryAttribute" /> class using the specified category name.</summary>
		/// <param name="category">The name of the category. </param>
		// Token: 0x060012A3 RID: 4771 RVA: 0x0004E0F0 File Offset: 0x0004C2F0
		public CategoryAttribute(string category)
		{
			this.categoryValue = category;
			this.localized = false;
		}

		/// <summary>Gets the name of the category for the property or event that this attribute is applied to.</summary>
		/// <returns>The name of the category for the property or event that this attribute is applied to.</returns>
		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x0004E108 File Offset: 0x0004C308
		public string Category
		{
			get
			{
				if (!this.localized)
				{
					this.localized = true;
					string localizedString = this.GetLocalizedString(this.categoryValue);
					if (localizedString != null)
					{
						this.categoryValue = localizedString;
					}
				}
				return this.categoryValue;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.CategoryAttribute" />..</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x060012A5 RID: 4773 RVA: 0x0004E141 File Offset: 0x0004C341
		public override bool Equals(object obj)
		{
			return obj == this || (obj is CategoryAttribute && this.Category.Equals(((CategoryAttribute)obj).Category));
		}

		/// <summary>Returns the hash code for this attribute.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060012A6 RID: 4774 RVA: 0x0004E169 File Offset: 0x0004C369
		public override int GetHashCode()
		{
			return this.Category.GetHashCode();
		}

		/// <summary>Looks up the localized name of the specified category.</summary>
		/// <returns>The localized name of the category, or null if a localized name does not exist.</returns>
		/// <param name="value">The identifer for the category to look up. </param>
		// Token: 0x060012A7 RID: 4775 RVA: 0x0004E178 File Offset: 0x0004C378
		protected virtual string GetLocalizedString(string value)
		{
			uint num = global::<PrivateImplementationDetails>.ComputeStringHash(value);
			if (num <= 1062369733U)
			{
				if (num <= 630759034U)
				{
					if (num <= 433860734U)
					{
						if (num != 175614239U)
						{
							if (num == 433860734U)
							{
								if (value == "Default")
								{
									return "Misc";
								}
							}
						}
						else if (value == "Action")
						{
							return "Action";
						}
					}
					else if (num != 521774151U)
					{
						if (num == 630759034U)
						{
							if (value == "DragDrop")
							{
								return "Drag Drop";
							}
						}
					}
					else if (value == "Behavior")
					{
						return "Behavior";
					}
				}
				else if (num <= 723360612U)
				{
					if (num != 676498961U)
					{
						if (num == 723360612U)
						{
							if (value == "Mouse")
							{
								return "Mouse";
							}
						}
					}
					else if (value == "Scale")
					{
						return "Scale";
					}
				}
				else if (num != 822184863U)
				{
					if (num != 1041509726U)
					{
						if (num == 1062369733U)
						{
							if (value == "Data")
							{
								return "Data";
							}
						}
					}
					else if (value == "Text")
					{
						return "Text";
					}
				}
				else if (value == "Appearance")
				{
					return "Appearance";
				}
			}
			else if (num <= 2809814704U)
			{
				if (num <= 1779622119U)
				{
					if (num != 1762750224U)
					{
						if (num == 1779622119U)
						{
							if (value == "Config")
							{
								return "Configurations";
							}
						}
					}
					else if (value == "DDE")
					{
						return "DDE";
					}
				}
				else if (num != 2055433310U)
				{
					if (num != 2368288673U)
					{
						if (num == 2809814704U)
						{
							if (value == "Font")
							{
								return "Font";
							}
						}
					}
					else if (value == "List")
					{
						return "List";
					}
				}
				else if (value == "WindowStyle")
				{
					return "Window Style";
				}
			}
			else if (num <= 3441084684U)
			{
				if (num != 3159863731U)
				{
					if (num == 3441084684U)
					{
						if (value == "Key")
						{
							return "Key";
						}
					}
				}
				else if (value == "Focus")
				{
					return "Focus";
				}
			}
			else if (num != 3799987242U)
			{
				if (num != 3901555439U)
				{
					if (num == 4152902175U)
					{
						if (value == "Layout")
						{
							return "Layout";
						}
					}
				}
				else if (value == "Design")
				{
					return "Design";
				}
			}
			else if (value == "Position")
			{
				return "Position";
			}
			return value;
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060012A8 RID: 4776 RVA: 0x0004E4B6 File Offset: 0x0004C6B6
		public override bool IsDefaultAttribute()
		{
			return this.Category.Equals(CategoryAttribute.Default.Category);
		}

		// Token: 0x04001268 RID: 4712
		private static volatile CategoryAttribute appearance;

		// Token: 0x04001269 RID: 4713
		private static volatile CategoryAttribute asynchronous;

		// Token: 0x0400126A RID: 4714
		private static volatile CategoryAttribute behavior;

		// Token: 0x0400126B RID: 4715
		private static volatile CategoryAttribute data;

		// Token: 0x0400126C RID: 4716
		private static volatile CategoryAttribute design;

		// Token: 0x0400126D RID: 4717
		private static volatile CategoryAttribute action;

		// Token: 0x0400126E RID: 4718
		private static volatile CategoryAttribute format;

		// Token: 0x0400126F RID: 4719
		private static volatile CategoryAttribute layout;

		// Token: 0x04001270 RID: 4720
		private static volatile CategoryAttribute mouse;

		// Token: 0x04001271 RID: 4721
		private static volatile CategoryAttribute key;

		// Token: 0x04001272 RID: 4722
		private static volatile CategoryAttribute focus;

		// Token: 0x04001273 RID: 4723
		private static volatile CategoryAttribute windowStyle;

		// Token: 0x04001274 RID: 4724
		private static volatile CategoryAttribute dragDrop;

		// Token: 0x04001275 RID: 4725
		private static volatile CategoryAttribute defAttr;

		// Token: 0x04001276 RID: 4726
		private bool localized;

		// Token: 0x04001277 RID: 4727
		private string categoryValue;
	}
}
