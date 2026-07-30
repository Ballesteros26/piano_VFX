using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.UI.WebControls
{
	/// <summary>Contains a sequential list of role groups that the <see cref="T:System.Web.UI.WebControls.LoginView" /> control uses to determine which control template to display to users based on their role. This class cannot be inherited.</summary>
	// Token: 0x02000403 RID: 1027
	[Editor("System.Web.UI.Design.WebControls.RoleGroupCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class RoleGroupCollection : CollectionBase
	{
		/// <summary>Gets the role group at the specified index.</summary>
		/// <returns>The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> at the specified index.</returns>
		/// <param name="index">The index of the role group to return. </param>
		// Token: 0x17000E88 RID: 3720
		public RoleGroup this[int index]
		{
			get
			{
				return (RoleGroup)base.List[index];
			}
		}

		/// <summary>Adds a role group to the end of the collection.</summary>
		/// <param name="group">The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to add to the collection. </param>
		// Token: 0x06002D91 RID: 11665 RVA: 0x00078C88 File Offset: 0x00076E88
		public void Add(RoleGroup group)
		{
			base.List.Add(group);
		}

		/// <summary>Indicates whether the collection contains the specified role group.</summary>
		/// <returns>true if the specified role group is a member of the collection; otherwise false.</returns>
		/// <param name="group">The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to look for in the collection. </param>
		// Token: 0x06002D92 RID: 11666 RVA: 0x00078C97 File Offset: 0x00076E97
		public bool Contains(RoleGroup group)
		{
			return base.List.Contains(group);
		}

		/// <summary>Copies all the items from the <see cref="T:System.Web.UI.WebControls.RoleGroupCollection" /> collection to a compatible one-dimensional array of <see cref="T:System.Web.UI.WebControls.RoleGroup" /> objects, starting at the specified index in the target array.</summary>
		/// <param name="array">A zero-based array of <see cref="T:System.Web.UI.WebControls.RoleGroup" /> objects that receives the items copied from the collection.</param>
		/// <param name="index">The position in the target array at which the array starts receiving the copied items.</param>
		// Token: 0x06002D93 RID: 11667 RVA: 0x00078CA8 File Offset: 0x00076EA8
		public void CopyTo(RoleGroup[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentException(global::Locale.GetText("Negative index."), "index");
			}
			if (base.Count <= array.Length - index)
			{
				throw new ArgumentException(global::Locale.GetText("Destination isn't large enough to copy collection."), "array");
			}
			for (int i = 0; i < base.Count; i++)
			{
				array[i + index] = this[i];
			}
		}

		/// <summary>Returns the first role group that contains the specified user account.</summary>
		/// <returns>A <see cref="T:System.Web.UI.WebControls.RoleGroup" /> representing the first role group in the collection that contains the specified user account. If the user is not part of a role group in the collection, it returns null.</returns>
		/// <param name="user">An <see cref="T:System.Security.Principal.IPrincipal" /> that represents the user account to find the role group collection.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="user" /> is null.</exception>
		// Token: 0x06002D94 RID: 11668 RVA: 0x00078D1C File Offset: 0x00076F1C
		public RoleGroup GetMatchingRoleGroup(IPrincipal user)
		{
			if (user == null)
			{
				throw new ArgumentNullException("user");
			}
			if (base.Count > 0)
			{
				foreach (object obj in this)
				{
					RoleGroup roleGroup = (RoleGroup)obj;
					if (roleGroup.ContainsUser(user))
					{
						return roleGroup;
					}
				}
			}
			return null;
		}

		/// <summary>Searches the collection and returns the zero-based index of the first occurrence of the specified <see cref="T:System.Web.UI.WebControls.RoleGroup" />.</summary>
		/// <returns>The zero-based index of the first occurrence of <paramref name="group" /> within the entire <see cref="T:System.Web.UI.WebControls.RoleGroupCollection" />, if found; otherwise, -1.</returns>
		/// <param name="group">The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to locate in the collection. </param>
		// Token: 0x06002D95 RID: 11669 RVA: 0x00078D90 File Offset: 0x00076F90
		public int IndexOf(RoleGroup group)
		{
			return base.List.IndexOf(group);
		}

		/// <summary>Adds a <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to the collection at the specified index.</summary>
		/// <param name="index">The zero-based index at which to insert the role group. </param>
		/// <param name="group">The role group to insert. </param>
		// Token: 0x06002D96 RID: 11670 RVA: 0x00078D9E File Offset: 0x00076F9E
		public void Insert(int index, RoleGroup group)
		{
			base.List.Insert(index, group);
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x00078DAD File Offset: 0x00076FAD
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
		}

		/// <summary>Deletes the first occurrence of the specified role group from the collection.</summary>
		/// <param name="group">The <see cref="T:System.Web.UI.WebControls.RoleGroup" /> to remove from the collection. </param>
		// Token: 0x06002D98 RID: 11672 RVA: 0x00078DB6 File Offset: 0x00076FB6
		public void Remove(RoleGroup group)
		{
			if (group != null && this.Contains(group))
			{
				base.List.Remove(group);
			}
		}
	}
}
