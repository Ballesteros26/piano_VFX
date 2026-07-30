using System;
using System.Collections;

namespace System.Web.UI
{
	/// <summary>During the build process, retains information about property entries.</summary>
	// Token: 0x020001ED RID: 493
	public class ObjectPersistData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.ObjectPersistData" /> class. </summary>
		/// <param name="builder">The object for building the control.</param>
		/// <param name="builtObjects">A collection of objects that have been built by this builder.</param>
		// Token: 0x060013D1 RID: 5073 RVA: 0x00035C2B File Offset: 0x00033E2B
		public ObjectPersistData(ControlBuilder builder, IDictionary builtObjects)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets all the property entries for the control being built.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the property entries for the control.</returns>
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00003A1F File Offset: 0x00001C1F
		public ICollection AllPropertyEntries
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a collection of the objects that have been built by the control builder.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing the items that have been built by the control builder.</returns>
		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00003A1F File Offset: 0x00001C1F
		public IDictionary BuiltObjects
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets items that are collection types.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing items of type <see cref="T:System.Collections.ICollection" />.</returns>
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00003A1F File Offset: 0x00001C1F
		public ICollection CollectionItems
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets event entries for the control being built.</summary>
		/// <returns>An <see cref="T:System.Collections.ArrayList" /> containing the event entries.</returns>
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060013D5 RID: 5077 RVA: 0x00003A1F File Offset: 0x00001C1F
		public ICollection EventEntries
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value that indicates whether the persisted data is for a collection.</summary>
		/// <returns>true if this persisted data is for a collection; otherwise, false.</returns>
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x00003A1F File Offset: 0x00001C1F
		public bool IsCollection
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating whether the control created by the control builder object is localized.</summary>
		/// <returns>true if the control created by the control builder object is localized; otherwise, false.</returns>
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00003A1F File Offset: 0x00001C1F
		public bool Localize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the type of the object associated with the persisted properties.</summary>
		/// <returns>The <see cref="T:System.Type" /> of the object being built.</returns>
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x00003A1F File Offset: 0x00001C1F
		public Type ObjectType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the resource key for the control builder object.</summary>
		/// <returns>A <see cref="T:System.String" /> representing the resource key for the control builder.</returns>
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x00003A1F File Offset: 0x00001C1F
		public string ResourceKey
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Adds built objects to a collection.</summary>
		/// <param name="table">A collection for the control builder.</param>
		// Token: 0x060013DA RID: 5082 RVA: 0x00003A1F File Offset: 0x00001C1F
		public void AddToObjectControlBuilderTable(IDictionary table)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the property entries with the specified filter.</summary>
		/// <returns>The property entries with the specified filter.</returns>
		/// <param name="filter">The <see cref="P:System.Web.UI.PropertyEntry.Filter" /> on an expression.</param>
		// Token: 0x060013DB RID: 5083 RVA: 0x00003A1F File Offset: 0x00001C1F
		public IDictionary GetFilteredProperties(string filter)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets all property entries for the specified filter and property name.</summary>
		/// <returns>All property entries for the specified filter and property name.</returns>
		/// <param name="filter">The <see cref="P:System.Web.UI.PropertyEntry.Filter" /> on an expression.</param>
		/// <param name="name">The <see cref="P:System.Web.UI.PropertyEntry.Name" /> on an expression.</param>
		// Token: 0x060013DC RID: 5084 RVA: 0x00003A1F File Offset: 0x00001C1F
		public PropertyEntry GetFilteredProperty(string filter, string name)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns all filtered property entries for a specified property name.</summary>
		/// <returns>All filtered property entries for a specified property name.</returns>
		/// <param name="name">The <see cref="P:System.Web.UI.PropertyEntry.Name" /> on an expression.</param>
		// Token: 0x060013DD RID: 5085 RVA: 0x00003A1F File Offset: 0x00001C1F
		public ICollection GetPropertyAllFilters(string name)
		{
			throw new NotImplementedException();
		}
	}
}
