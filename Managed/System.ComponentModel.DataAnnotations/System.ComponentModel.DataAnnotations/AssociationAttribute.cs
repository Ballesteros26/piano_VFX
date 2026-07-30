using System;
using System.Collections.Generic;

namespace System.ComponentModel.DataAnnotations
{
	/// <summary>Specifies that an entity member represents a data relationship, such as a foreign key relationship.</summary>
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class AssociationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DataAnnotations.AssociationAttribute" /> class.</summary>
		/// <param name="name">The name of the association. </param>
		/// <param name="thisKey">A comma-separated list of the property names of the key values on the <paramref name="thisKey" /> side of the association.</param>
		/// <param name="otherKey">A comma-separated list of the property names of the key values on the <paramref name="otherKey" /> side of the association.</param>
		// Token: 0x06000018 RID: 24 RVA: 0x00002434 File Offset: 0x00000634
		public AssociationAttribute(string name, string thisKey, string otherKey)
		{
			this.name = name;
			this.thisKey = thisKey;
			this.otherKey = otherKey;
		}

		/// <summary>Gets the name of the association.</summary>
		/// <returns>The name of the association.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002451 File Offset: 0x00000651
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the property names of the key values on the ThisKey side of the association.</summary>
		/// <returns>A comma-separated list of the property names that represent the key values on the ThisKey side of the association.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002459 File Offset: 0x00000659
		public string ThisKey
		{
			get
			{
				return this.thisKey;
			}
		}

		/// <summary>Gets the property names of the key values on the OtherKey side of the association.</summary>
		/// <returns>A comma-separated list of the property names that represent the key values on the OtherKey side of the association.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002461 File Offset: 0x00000661
		public string OtherKey
		{
			get
			{
				return this.otherKey;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the association member represents a foreign key.</summary>
		/// <returns>true if the association represents a foreign key; otherwise, false.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002469 File Offset: 0x00000669
		// (set) Token: 0x0600001D RID: 29 RVA: 0x00002471 File Offset: 0x00000671
		public bool IsForeignKey
		{
			get
			{
				return this.isForeignKey;
			}
			set
			{
				this.isForeignKey = value;
			}
		}

		/// <summary>Gets a collection of individual key members that are specified in the <see cref="P:System.ComponentModel.DataAnnotations.AssociationAttribute.ThisKey" /> property.</summary>
		/// <returns>A collection of individual key members that are specified in the <see cref="P:System.ComponentModel.DataAnnotations.AssociationAttribute.ThisKey" /> property.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x0000247A File Offset: 0x0000067A
		public IEnumerable<string> ThisKeyMembers
		{
			get
			{
				return AssociationAttribute.GetKeyMembers(this.ThisKey);
			}
		}

		/// <summary>Gets a collection of individual key members that are specified in the <see cref="P:System.ComponentModel.DataAnnotations.AssociationAttribute.OtherKey" /> property.</summary>
		/// <returns>A collection of individual key members that are specified in the <see cref="P:System.ComponentModel.DataAnnotations.AssociationAttribute.OtherKey" /> property.</returns>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002487 File Offset: 0x00000687
		public IEnumerable<string> OtherKeyMembers
		{
			get
			{
				return AssociationAttribute.GetKeyMembers(this.OtherKey);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002494 File Offset: 0x00000694
		private static string[] GetKeyMembers(string key)
		{
			return key.Replace(" ", string.Empty).Split(new char[] { ',' });
		}

		// Token: 0x04000036 RID: 54
		private string name;

		// Token: 0x04000037 RID: 55
		private string thisKey;

		// Token: 0x04000038 RID: 56
		private string otherKey;

		// Token: 0x04000039 RID: 57
		private bool isForeignKey;
	}
}
