using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security;
using System.Windows.Forms;

namespace System.Drawing.Design
{
	/// <summary>Encapsulates a <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
	// Token: 0x02000019 RID: 25
	[Serializable]
	public class ToolboxItemContainer : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItemContainer" /> class from a <see cref="T:System.Windows.Forms.IDataObject" />.</summary>
		/// <param name="data">A data object that represents a <see cref="T:System.Drawing.Design.ToolboxItemContainer" />.</param>
		// Token: 0x06000053 RID: 83 RVA: 0x000035CE File Offset: 0x000017CE
		[MonoTODO]
		public ToolboxItemContainer(IDataObject data)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItemContainer" /> class from a <see cref="T:System.Drawing.Design.ToolboxItem" />.</summary>
		/// <param name="item">The <see cref="T:System.Drawing.Design.ToolboxItem" /> for which to create a <see cref="T:System.Drawing.Design.ToolboxItemContainer" />.</param>
		// Token: 0x06000054 RID: 84 RVA: 0x000035CE File Offset: 0x000017CE
		[MonoTODO]
		public ToolboxItemContainer(ToolboxItem item)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Design.ToolboxItemContainer" /> class from a serialization stream.</summary>
		/// <param name="info">The serialization information passed in by the serializer when deserializing the <see cref="T:System.Drawing.Design.ToolboxItemContainer" />.</param>
		/// <param name="context">The streaming context passed in by the serializer when deserializing the <see cref="T:System.Drawing.Design.ToolboxItemContainer" />.</param>
		// Token: 0x06000055 RID: 85 RVA: 0x000035CE File Offset: 0x000017CE
		[MonoTODO]
		protected ToolboxItemContainer(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value indicating whether the underlying toolbox item has been deserialized.</summary>
		/// <returns>true if the underlying toolbox item has been deserialized; otherwise, false.</returns>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000056 RID: 86 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public bool IsCreated
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value indicating if the <see cref="T:System.Drawing.Design.ToolboxItem" /> contained in the <see cref="T:System.Drawing.Design.ToolboxItemContainer" /> is transient.</summary>
		/// <returns>true, if the <see cref="T:System.Drawing.Design.ToolboxItem" /> contained in the <see cref="T:System.Drawing.Design.ToolboxItemContainer" /> is marked as transient; otherwise, false.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000057 RID: 87 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public bool IsTransient
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets an <see cref="T:System.Windows.Forms.IDataObject" /> that describes this <see cref="T:System.Drawing.Design.ToolboxItemContainer" />.</summary>
		/// <returns>An <see cref="T:System.Windows.Forms.IDataObject" /> that describes this <see cref="T:System.Drawing.Design.ToolboxItemContainer" />.</returns>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public virtual IDataObject ToolboxData
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Determines whether two <see cref="T:System.Drawing.Design.ToolboxItemContainer" /> instances are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.Drawing.Design.ToolboxItemContainer" /> is equal to the current <see cref="T:System.Drawing.Design.ToolboxItemContainer" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Drawing.Design.ToolboxItemContainer" /> to compare with the current <see cref="T:System.Drawing.Design.ToolboxItemContainer" />.</param>
		// Token: 0x06000059 RID: 89 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public override bool Equals(object obj)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Drawing.Design.ToolboxItemContainer" />.</returns>
		// Token: 0x0600005A RID: 90 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public override int GetHashCode()
		{
			throw new NotImplementedException();
		}

		/// <summary>Saves the serialization state for the object.</summary>
		/// <param name="info">The serialization information passed in by the serializer when serializing this object.</param>
		/// <param name="context">The streaming context passed in by the serializer when serializing this object.</param>
		// Token: 0x0600005B RID: 91 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		[SecurityCritical]
		protected virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>For a description of this member, see the <see cref="M:System.Drawing.Design.ToolboxItemContainer.GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)" /> method.</summary>
		/// <param name="info">The serialization information passed in by the serializer when serializing this object.</param>
		/// <param name="context">The streaming context passed in by the serializer when serializing this object.</param>
		// Token: 0x0600005C RID: 92 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a collection of <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> objects that represent the current filter for the <see cref="T:System.Drawing.Design.ToolboxItem" />.  </summary>
		/// <returns>A collection of <see cref="T:System.ComponentModel.ToolboxItemFilterAttribute" /> objects. This never returns null.</returns>
		/// <param name="creators">A collection of <see cref="T:System.Drawing.Design.ToolboxItemCreator" /> objects.</param>
		// Token: 0x0600005D RID: 93 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public virtual ICollection GetFilter(ICollection creators)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the <see cref="T:System.Drawing.Design.ToolboxItem" /> contained in the <see cref="T:System.Drawing.Design.ToolboxItemContainer" />.</summary>
		/// <returns>The <see cref="T:System.Drawing.Design.ToolboxItem" /> contained in the <see cref="T:System.Drawing.Design.ToolboxItemContainer" />.</returns>
		/// <param name="creators">A collection of <see cref="T:System.Drawing.Design.ToolboxItemCreator" /> objects.</param>
		// Token: 0x0600005E RID: 94 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public virtual ToolboxItem GetToolboxItem(ICollection creators)
		{
			throw new NotImplementedException();
		}

		/// <summary>Merges the container's filter with the filter from the given item.</summary>
		/// <param name="item">The source of the filter to merge with the container's filter.</param>
		// Token: 0x0600005F RID: 95 RVA: 0x0000359B File Offset: 0x0000179B
		[MonoTODO]
		public void UpdateFilter(ToolboxItem item)
		{
			throw new NotImplementedException();
		}
	}
}
