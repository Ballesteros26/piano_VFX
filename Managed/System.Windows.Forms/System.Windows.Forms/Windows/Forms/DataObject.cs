using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace System.Windows.Forms
{
	/// <summary>Implements a basic data transfer mechanism.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200013E RID: 318
	[ClassInterface(0)]
	public class DataObject : IDataObject, IDataObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataObject" /> class.</summary>
		// Token: 0x06001618 RID: 5656 RVA: 0x00051AF8 File Offset: 0x0004FCF8
		public DataObject()
		{
			this.entries = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataObject" /> class and adds the specified object to it.</summary>
		/// <param name="data">The data to store. </param>
		// Token: 0x06001619 RID: 5657 RVA: 0x00051B08 File Offset: 0x0004FD08
		public DataObject(object data)
		{
			this.SetData(data);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataObject" /> class and adds the specified object in the specified format.</summary>
		/// <param name="format">The format of the specified data. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats.</param>
		/// <param name="data">The data to store. </param>
		// Token: 0x0600161A RID: 5658 RVA: 0x00051B18 File Offset: 0x0004FD18
		public DataObject(string format, object data)
		{
			this.SetData(format, data);
		}

		/// <summary>Creates a connection between a data object and an advisory sink. This method is called by an object that supports an advisory sink and enables the advisory sink to be notified of changes in the object's data.</summary>
		/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following: ValueDescriptionS_OKThe advisory connection was created.E_NOTIMPLThis method is not implemented on the data object.DV_E_LINDEXThere is an invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.DV_E_FORMATETCThere is an invalid value for the <paramref name="pFormatetc" /> parameter.OLE_E_ADVISENOTSUPPORTEDThe data object does not support change notification.</returns>
		/// <param name="pFormatetc"> A <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, target device, aspect, and medium that will be used for future notifications.</param>
		/// <param name="advf">One of the <see cref="T:System.Runtime.InteropServices.ComTypes.ADVF" /> values that specifies a group of flags for controlling the advisory connection.</param>
		/// <param name="pAdvSink">A pointer to the <see cref="T:System.Runtime.InteropServices.ComTypes.IAdviseSink" /> interface on the advisory sink that will receive the change notification.</param>
		/// <param name="pdwConnection">When this method returns, contains a pointer to a DWORD token that identifies this connection. You can use this token later to delete the advisory connection by passing it to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.DUnadvise(System.Int32)" />. If this value is zero, the connection was not established. This parameter is passed uninitialized.</param>
		// Token: 0x0600161B RID: 5659 RVA: 0x00051B28 File Offset: 0x0004FD28
		int IDataObject.DAdvise(ref FORMATETC pFormatetc, ADVF advf, IAdviseSink adviseSink, out int connection)
		{
			throw new NotImplementedException();
		}

		/// <summary>Destroys a notification connection that had been previously established.</summary>
		/// <param name="dwConnection">A DWORD token that specifies the connection to remove. Use the value returned by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.DAdvise(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.ADVF,System.Runtime.InteropServices.ComTypes.IAdviseSink,System.Int32@)" /> when the connection was originally established.</param>
		// Token: 0x0600161C RID: 5660 RVA: 0x00051B30 File Offset: 0x0004FD30
		void IDataObject.DUnadvise(int connection)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an object that can be used to enumerate the current advisory connections.</summary>
		/// <returns>This method supports the standard return value E_OUTOFMEMORY, as well as the following:ValueDescriptionS_OKThe enumerator object is successfully instantiated or there are no connections.OLE_E_ADVISENOTSUPPORTEDThis object does not support advisory notifications.</returns>
		/// <param name="enumAdvise">When this method returns, contains an <see cref="T:System.Runtime.InteropServices.ComTypes.IEnumSTATDATA" /> that receives the interface pointer to the new enumerator object. If the implementation sets <paramref name="enumAdvise" /> to null, there are no connections to advisory sinks at this time. This parameter is passed uninitialized.</param>
		// Token: 0x0600161D RID: 5661 RVA: 0x00051B38 File Offset: 0x0004FD38
		int IDataObject.EnumDAdvise(out IEnumSTATDATA enumAdvise)
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates an object for enumerating the <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures for a data object. These structures are used in calls to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> or <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.SetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@,System.Boolean)" />. </summary>
		/// <returns>This method supports the standard return values E_INVALIDARG and E_OUTOFMEMORY, as well as the following:ValueDescriptionS_OKThe enumerator object was successfully created.E_NOTIMPLThe direction specified by the <paramref name="direction" /> parameter is not supported.OLE_S_USEREGRequests that OLE enumerate the formats from the registry.</returns>
		/// <param name="dwDirection">One of the <see cref="T:System.Runtime.InteropServices.ComTypes.DATADIR" /> values that specifies the direction of the data.</param>
		// Token: 0x0600161E RID: 5662 RVA: 0x00051B40 File Offset: 0x0004FD40
		IEnumFORMATETC IDataObject.EnumFormatEtc(DATADIR direction)
		{
			throw new NotImplementedException();
		}

		/// <summary>Provides a standard <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure that is logically equivalent to a more complex structure. Use this method to determine whether two different <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures would return the same data, removing the need for duplicate rendering.</summary>
		/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following: ValueDescriptionS_OKThe returned <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure is different from the one that was passed.DATA_S_SAMEFORMATETCThe <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structures are the same and null is returned in the <paramref name="formatOut" /> parameter.DV_E_LINDEXThere is an invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.DV_E_FORMATETCThere is an invalid value for the <paramref name="pFormatetc" /> parameter.OLE_E_NOTRUNNINGThe application is not running.</returns>
		/// <param name="pformatetcIn">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device that the caller would like to use to retrieve data in a subsequent call such as <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. The <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> member is not significant in this case and should be ignored.</param>
		/// <param name="pformatetcOut">When this method returns, contains a pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure that contains the most general information possible for a specific rendering, making it canonically equivalent to <paramref name="formatetIn" />. The caller must allocate this structure and the <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetCanonicalFormatEtc(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.FORMATETC@)" /> method must fill in the data. To retrieve data in a subsequent call such as <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />, the caller uses the supplied value of <paramref name="formatOut" />, unless the value supplied is null. This value is null if the method returns DATA_S_SAMEFORMATETC. The <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> member is not significant in this case and should be ignored. This parameter is passed uninitialized.</param>
		// Token: 0x0600161F RID: 5663 RVA: 0x00051B48 File Offset: 0x0004FD48
		int IDataObject.GetCanonicalFormatEtc(ref FORMATETC formatIn, out FORMATETC formatOut)
		{
			throw new NotImplementedException();
		}

		/// <summary>Obtains data from a source data object. The <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> method, which is called by a data consumer, renders the data described in the specified <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure and transfers it through the specified <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure. The caller then assumes responsibility for releasing the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure.</summary>
		/// <param name="formatetc">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use when passing the data. It is possible to specify more than one medium by using the Boolean OR operator, allowing the method to choose the best medium among those specified.</param>
		/// <param name="medium">When this method returns, contains a pointer to the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure that indicates the storage medium containing the returned data through its <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.tymed" /> member, and the responsibility for releasing the medium through the value of its <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member. If <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> is null, the receiver of the medium is responsible for releasing it; otherwise, <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> points to the IUnknown interface on the appropriate object so its Release method can be called. The medium must be allocated and filled in by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. This parameter is passed uninitialized.</param>
		/// <exception cref="T:System.OutOfMemoryException">There is not enough memory to perform this operation.</exception>
		// Token: 0x06001620 RID: 5664 RVA: 0x00051B50 File Offset: 0x0004FD50
		void IDataObject.GetData(ref FORMATETC format, out STGMEDIUM medium)
		{
			throw new NotImplementedException();
		}

		/// <summary>Obtains data from a source data object. This method, which is called by a data consumer, differs from the <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> method in that the caller must allocate and free the specified storage medium.</summary>
		/// <param name="formatetc">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use when passing the data. Only one medium can be specified in <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" />, and only the following <see cref="T:System.Runtime.InteropServices.ComTypes.TYMED" /> values are valid: <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ISTORAGE" />, <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_ISTREAM" />, <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_HGLOBAL" />, or <see cref="F:System.Runtime.InteropServices.ComTypes.TYMED.TYMED_FILE" />.</param>
		/// <param name="medium">A <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" />, passed by reference, that defines the storage medium containing the data being transferred. The medium must be allocated by the caller and filled in by <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetDataHere(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" />. The caller must also free the medium. The implementation of this method must always supply a value of null for the <see cref="F:System.Runtime.InteropServices.ComTypes.STGMEDIUM.pUnkForRelease" /> member of the <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure that this parameter points to.</param>
		// Token: 0x06001621 RID: 5665 RVA: 0x00051B58 File Offset: 0x0004FD58
		void IDataObject.GetDataHere(ref FORMATETC format, ref STGMEDIUM medium)
		{
			throw new NotImplementedException();
		}

		/// <summary>Determines whether the data object is capable of rendering the data described in the <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure. Objects attempting a paste or drop operation can call this method before calling <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> to get an indication of whether the operation may be successful.</summary>
		/// <returns>This method supports the standard return values E_INVALIDARG, E_UNEXPECTED, and E_OUTOFMEMORY, as well as the following: ValueDescriptionS_OKA subsequent call to <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.GetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@)" /> would probably be successful.DV_E_LINDEXAn invalid value for <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.lindex" />; currently, only -1 is supported.DV_E_FORMATETCAn invalid value for the <paramref name="pFormatetc" /> parameter.DV_E_TYMEDAn invalid <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.tymed" /> value.DV_E_DVASPECTAn invalid <see cref="F:System.Runtime.InteropServices.ComTypes.FORMATETC.dwAspect" /> value.OLE_E_NOTRUNNINGThe application is not running.</returns>
		/// <param name="formatetc">A pointer to a <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format, medium, and target device to use for the query.</param>
		// Token: 0x06001622 RID: 5666 RVA: 0x00051B60 File Offset: 0x0004FD60
		int IDataObject.QueryGetData(ref FORMATETC format)
		{
			throw new NotImplementedException();
		}

		/// <summary>Transfers data to the object that implements this method. This method is called by an object that contains a data source.</summary>
		/// <param name="pFormatetcIn">A <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" /> structure, passed by reference, that defines the format used by the data object when interpreting the data contained in the storage medium.</param>
		/// <param name="pmedium">A <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" /> structure, passed by reference, that defines the storage medium in which the data is being passed.</param>
		/// <param name="fRelease">true to specify that the data object called, which implements <see cref="M:System.Runtime.InteropServices.ComTypes.IDataObject.SetData(System.Runtime.InteropServices.ComTypes.FORMATETC@,System.Runtime.InteropServices.ComTypes.STGMEDIUM@,System.Boolean)" />, owns the storage medium after the call returns. This means that the data object must free the medium after it has been used by calling the ReleaseStgMedium function. false to specify that the caller retains ownership of the storage medium, and the data object called uses the storage medium for the duration of the call only.</param>
		/// <exception cref="T:System.NotImplementedException">This method does not support the type of the underlying data object.</exception>
		// Token: 0x06001623 RID: 5667 RVA: 0x00051B68 File Offset: 0x0004FD68
		void IDataObject.SetData(ref FORMATETC formatIn, ref STGMEDIUM medium, bool release)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether the data object contains data in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</summary>
		/// <returns>true if the data object contains audio data; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001624 RID: 5668 RVA: 0x00051B70 File Offset: 0x0004FD70
		public virtual bool ContainsAudio()
		{
			return this.GetDataPresent(DataFormats.WaveAudio, true);
		}

		/// <summary>Indicates whether the data object contains data that is in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format or can be converted to that format.</summary>
		/// <returns>true if the data object contains a file drop list; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001625 RID: 5669 RVA: 0x00051B80 File Offset: 0x0004FD80
		public virtual bool ContainsFileDropList()
		{
			return this.GetDataPresent(DataFormats.FileDrop, true);
		}

		/// <summary>Indicates whether the data object contains data that is in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format or can be converted to that format.</summary>
		/// <returns>true if the data object contains image data; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001626 RID: 5670 RVA: 0x00051B90 File Offset: 0x0004FD90
		public virtual bool ContainsImage()
		{
			return this.GetDataPresent(DataFormats.Bitmap, true);
		}

		/// <summary>Indicates whether the data object contains data in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format.</summary>
		/// <returns>true if the data object contains text data; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001627 RID: 5671 RVA: 0x00051BA0 File Offset: 0x0004FDA0
		public virtual bool ContainsText()
		{
			return this.GetDataPresent(DataFormats.UnicodeText, true);
		}

		/// <summary>Indicates whether the data object contains text data in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
		/// <returns>true if the data object contains text data in the specified format; otherwise, false.</returns>
		/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001628 RID: 5672 RVA: 0x00051BB0 File Offset: 0x0004FDB0
		public virtual bool ContainsText(TextDataFormat format)
		{
			if (!Enum.IsDefined(typeof(TextDataFormat), format))
			{
				throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for TextDataFormat", format));
			}
			return this.GetDataPresent(this.TextFormatToDataFormat(format), true);
		}

		/// <summary>Retrieves an audio stream from the data object.</summary>
		/// <returns>A <see cref="T:System.IO.Stream" /> containing audio data or null if the data object does not contain any data in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001629 RID: 5673 RVA: 0x00051BFC File Offset: 0x0004FDFC
		public virtual Stream GetAudioStream()
		{
			return (Stream)this.GetData(DataFormats.WaveAudio, true);
		}

		/// <summary>Returns the data associated with the specified data format.</summary>
		/// <returns>The data associated with the specified format, or null.</returns>
		/// <param name="format">The format of the data to retrieve. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600162A RID: 5674 RVA: 0x00051C10 File Offset: 0x0004FE10
		public virtual object GetData(string format)
		{
			return this.GetData(format, true);
		}

		/// <summary>Returns the data associated with the specified data format, using an automated conversion parameter to determine whether to convert the data to the format.</summary>
		/// <returns>The data associated with the specified format, or null.</returns>
		/// <param name="format">The format of the data to retrieve. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <param name="autoConvert">true to the convert data to the specified format; otherwise, false. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600162B RID: 5675 RVA: 0x00051C1C File Offset: 0x0004FE1C
		public virtual object GetData(string format, bool autoConvert)
		{
			DataObject.Entry entry;
			if (autoConvert)
			{
				entry = DataObject.Entry.FindConvertible(this.entries, format);
			}
			else
			{
				entry = DataObject.Entry.Find(this.entries, format);
			}
			if (entry == null)
			{
				return null;
			}
			return entry.Data;
		}

		/// <summary>Returns the data associated with the specified class type format.</summary>
		/// <returns>The data associated with the specified format, or null.</returns>
		/// <param name="format">A <see cref="T:System.Type" /> representing the format of the data to retrieve. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600162C RID: 5676 RVA: 0x00051C5C File Offset: 0x0004FE5C
		public virtual object GetData(Type format)
		{
			return this.GetData(format.FullName, true);
		}

		/// <summary>Determines whether data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to, the specified format.</summary>
		/// <returns>true if data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to, the specified format; otherwise, false.</returns>
		/// <param name="format">The format to check for. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600162D RID: 5677 RVA: 0x00051C6C File Offset: 0x0004FE6C
		public virtual bool GetDataPresent(string format)
		{
			return this.GetDataPresent(format, true);
		}

		/// <summary>Determines whether this <see cref="T:System.Windows.Forms.DataObject" /> contains data in the specified format or, optionally, contains data that can be converted to the specified format.</summary>
		/// <returns>true if the data is in, or can be converted to, the specified format; otherwise, false.</returns>
		/// <param name="format">The format to check for. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <param name="autoConvert">true to determine whether data stored in this <see cref="T:System.Windows.Forms.DataObject" /> can be converted to the specified format; false to check whether the data is in the specified format. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600162E RID: 5678 RVA: 0x00051C78 File Offset: 0x0004FE78
		public virtual bool GetDataPresent(string format, bool autoConvert)
		{
			if (autoConvert)
			{
				return DataObject.Entry.FindConvertible(this.entries, format) != null;
			}
			return DataObject.Entry.Find(this.entries, format) != null;
		}

		/// <summary>Determines whether data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to, the specified format.</summary>
		/// <returns>true if data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to, the specified format; otherwise, false.</returns>
		/// <param name="format">A <see cref="T:System.Type" /> representing the format to check for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600162F RID: 5679 RVA: 0x00051CA8 File Offset: 0x0004FEA8
		public virtual bool GetDataPresent(Type format)
		{
			return this.GetDataPresent(format.FullName, true);
		}

		/// <summary>Retrieves a collection of file names from the data object. </summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> containing file names or null if the data object does not contain any data that is in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format or can be converted to that format.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001630 RID: 5680 RVA: 0x00051CB8 File Offset: 0x0004FEB8
		public virtual StringCollection GetFileDropList()
		{
			return (StringCollection)this.GetData(DataFormats.FileDrop, true);
		}

		/// <summary>Returns a list of all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with or can be converted to.</summary>
		/// <returns>An array of type <see cref="T:System.String" />, containing a list of all formats that are supported by the data stored in this object.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001631 RID: 5681 RVA: 0x00051CCC File Offset: 0x0004FECC
		public virtual string[] GetFormats()
		{
			return this.GetFormats(true);
		}

		/// <summary>Returns a list of all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with or can be converted to, using an automatic conversion parameter to determine whether to retrieve only native data formats or all formats that the data can be converted to.</summary>
		/// <returns>An array of type <see cref="T:System.String" />, containing a list of all formats that are supported by the data stored in this object.</returns>
		/// <param name="autoConvert">true to retrieve all formats that data stored in this <see cref="T:System.Windows.Forms.DataObject" /> is associated with, or can be converted to; false to retrieve only native data formats. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001632 RID: 5682 RVA: 0x00051CD8 File Offset: 0x0004FED8
		public virtual string[] GetFormats(bool autoConvert)
		{
			return DataObject.Entry.Entries(this.entries, autoConvert);
		}

		/// <summary>Retrieves an image from the data object.</summary>
		/// <returns>An <see cref="T:System.Drawing.Image" /> representing the image data in the data object or null if the data object does not contain any data that is in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format or can be converted to that format.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001633 RID: 5683 RVA: 0x00051CE8 File Offset: 0x0004FEE8
		public virtual Image GetImage()
		{
			return (Image)this.GetData(DataFormats.Bitmap, true);
		}

		/// <summary>Retrieves text data from the data object in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format.</summary>
		/// <returns>The text data in the data object or <see cref="F:System.String.Empty" /> if the data object does not contain data in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001634 RID: 5684 RVA: 0x00051CFC File Offset: 0x0004FEFC
		public virtual string GetText()
		{
			return (string)this.GetData(DataFormats.UnicodeText, true);
		}

		/// <summary>Retrieves text data from the data object in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
		/// <returns>The text data in the data object or <see cref="F:System.String.Empty" /> if the data object does not contain data in the specified format.</returns>
		/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001635 RID: 5685 RVA: 0x00051D10 File Offset: 0x0004FF10
		public virtual string GetText(TextDataFormat format)
		{
			if (!Enum.IsDefined(typeof(TextDataFormat), format))
			{
				throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for TextDataFormat", format));
			}
			return (string)this.GetData(this.TextFormatToDataFormat(format), false);
		}

		/// <summary>Adds a <see cref="T:System.Byte" /> array to the data object in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format after converting it to a <see cref="T:System.IO.Stream" />.</summary>
		/// <param name="audioBytes">A <see cref="T:System.Byte" /> array containing the audio data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="audioBytes" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001636 RID: 5686 RVA: 0x00051D60 File Offset: 0x0004FF60
		public virtual void SetAudio(byte[] audioBytes)
		{
			if (audioBytes == null)
			{
				throw new ArgumentNullException("audioBytes");
			}
			MemoryStream memoryStream = new MemoryStream(audioBytes);
			this.SetAudio(memoryStream);
		}

		/// <summary>Adds a <see cref="T:System.IO.Stream" /> to the data object in the <see cref="F:System.Windows.Forms.DataFormats.WaveAudio" /> format.</summary>
		/// <param name="audioStream">A <see cref="T:System.IO.Stream" /> containing the audio data.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="audioStream" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001637 RID: 5687 RVA: 0x00051D8C File Offset: 0x0004FF8C
		public virtual void SetAudio(Stream audioStream)
		{
			if (audioStream == null)
			{
				throw new ArgumentNullException("audioStream");
			}
			this.SetData(DataFormats.WaveAudio, audioStream);
		}

		/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the object type as the data format.</summary>
		/// <param name="data">The data to store. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001638 RID: 5688 RVA: 0x00051DAC File Offset: 0x0004FFAC
		public virtual void SetData(object data)
		{
			this.SetData(data.GetType(), data);
		}

		/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the specified format and indicating whether the data can be converted to another format.</summary>
		/// <param name="format">The format associated with the data. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <param name="autoConvert">true to allow the data to be converted to another format; otherwise, false. </param>
		/// <param name="data">The data to store. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001639 RID: 5689 RVA: 0x00051DBC File Offset: 0x0004FFBC
		public virtual void SetData(string format, bool autoConvert, object data)
		{
			DataObject.Entry entry = DataObject.Entry.Find(this.entries, format);
			if (entry == null)
			{
				entry = new DataObject.Entry(format, data, autoConvert);
				lock (this)
				{
					if (this.entries == null)
					{
						this.entries = entry;
					}
					else
					{
						DataObject.Entry next = this.entries;
						while (next.next != null)
						{
							next = next.next;
						}
						next.next = entry;
					}
				}
				return;
			}
			entry.Data = data;
		}

		/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the specified format.</summary>
		/// <param name="format">The format associated with the data. See <see cref="T:System.Windows.Forms.DataFormats" /> for predefined formats. </param>
		/// <param name="data">The data to store. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600163A RID: 5690 RVA: 0x00051E60 File Offset: 0x00050060
		public virtual void SetData(string format, object data)
		{
			this.SetData(format, true, data);
		}

		/// <summary>Adds the specified object to the <see cref="T:System.Windows.Forms.DataObject" /> using the specified type as the format.</summary>
		/// <param name="format">A <see cref="T:System.Type" /> representing the format associated with the data. </param>
		/// <param name="data">The data to store. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600163B RID: 5691 RVA: 0x00051E6C File Offset: 0x0005006C
		public virtual void SetData(Type format, object data)
		{
			this.SetData(this.EnsureFormat(format), true, data);
		}

		/// <summary>Adds a collection of file names to the data object in the <see cref="F:System.Windows.Forms.DataFormats.FileDrop" /> format.</summary>
		/// <param name="filePaths">A <see cref="T:System.Collections.Specialized.StringCollection" /> containing the file names.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="filePaths" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600163C RID: 5692 RVA: 0x00051E80 File Offset: 0x00050080
		[MonoInternalNote("Needs additional checks for valid paths, see MSDN")]
		public virtual void SetFileDropList(StringCollection filePaths)
		{
			if (filePaths == null)
			{
				throw new ArgumentNullException("filePaths");
			}
			this.SetData(DataFormats.FileDrop, filePaths);
		}

		/// <summary>Adds an <see cref="T:System.Drawing.Image" /> to the data object in the <see cref="F:System.Windows.Forms.DataFormats.Bitmap" /> format.</summary>
		/// <param name="image">The <see cref="T:System.Drawing.Image" /> to add to the data object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="image" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600163D RID: 5693 RVA: 0x00051EA0 File Offset: 0x000500A0
		public virtual void SetImage(Image image)
		{
			if (image == null)
			{
				throw new ArgumentNullException("image");
			}
			this.SetData(DataFormats.Bitmap, image);
		}

		/// <summary>Adds text data to the data object in the <see cref="F:System.Windows.Forms.TextDataFormat.UnicodeText" /> format.</summary>
		/// <param name="textData">The text to add to the data object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="textData" /> is null or <see cref="F:System.String.Empty" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600163E RID: 5694 RVA: 0x00051EC0 File Offset: 0x000500C0
		public virtual void SetText(string textData)
		{
			if (string.IsNullOrEmpty(textData))
			{
				throw new ArgumentNullException("text");
			}
			this.SetData(DataFormats.UnicodeText, textData);
		}

		/// <summary>Adds text data to the data object in the format indicated by the specified <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</summary>
		/// <param name="textData">The text to add to the data object.</param>
		/// <param name="format">One of the <see cref="T:System.Windows.Forms.TextDataFormat" /> values.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="textData" /> is null or <see cref="F:System.String.Empty" />.</exception>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">
		///   <paramref name="format" /> is not a valid <see cref="T:System.Windows.Forms.TextDataFormat" /> value.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600163F RID: 5695 RVA: 0x00051EF0 File Offset: 0x000500F0
		public virtual void SetText(string textData, TextDataFormat format)
		{
			if (string.IsNullOrEmpty(textData))
			{
				throw new ArgumentNullException("text");
			}
			if (!Enum.IsDefined(typeof(TextDataFormat), format))
			{
				throw new InvalidEnumArgumentException(string.Format("Enum argument value '{0}' is not valid for TextDataFormat", format));
			}
			switch (format)
			{
			case TextDataFormat.Text:
				this.SetData(DataFormats.Text, textData);
				break;
			case TextDataFormat.UnicodeText:
				this.SetData(DataFormats.UnicodeText, textData);
				break;
			case TextDataFormat.Rtf:
				this.SetData(DataFormats.Rtf, textData);
				break;
			case TextDataFormat.Html:
				this.SetData(DataFormats.Html, textData);
				break;
			case TextDataFormat.CommaSeparatedValue:
				this.SetData(DataFormats.CommaSeparatedValue, textData);
				break;
			}
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x00051FBC File Offset: 0x000501BC
		internal string EnsureFormat(string name)
		{
			DataFormats.Format format = DataFormats.Format.Find(name);
			if (format == null)
			{
				format = DataFormats.Format.Add(name);
			}
			return format.Name;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00051FE4 File Offset: 0x000501E4
		internal string EnsureFormat(Type type)
		{
			return this.EnsureFormat(type.FullName);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00051FF4 File Offset: 0x000501F4
		private string TextFormatToDataFormat(TextDataFormat format)
		{
			switch (format)
			{
			default:
				return DataFormats.Text;
			case TextDataFormat.UnicodeText:
				return DataFormats.UnicodeText;
			case TextDataFormat.Rtf:
				return DataFormats.Rtf;
			case TextDataFormat.Html:
				return DataFormats.Html;
			case TextDataFormat.CommaSeparatedValue:
				return DataFormats.CommaSeparatedValue;
			}
		}

		// Token: 0x04000C3C RID: 3132
		private DataObject.Entry entries;

		// Token: 0x0200013F RID: 319
		private class Entry
		{
			// Token: 0x06001643 RID: 5699 RVA: 0x00052040 File Offset: 0x00050240
			internal Entry(string type, object data, bool autoconvert)
			{
				this.type = type;
				this.data = data;
				this.autoconvert = autoconvert;
			}

			// Token: 0x1700053B RID: 1339
			// (get) Token: 0x06001644 RID: 5700 RVA: 0x00052060 File Offset: 0x00050260
			// (set) Token: 0x06001645 RID: 5701 RVA: 0x00052068 File Offset: 0x00050268
			public object Data
			{
				get
				{
					return this.data;
				}
				set
				{
					this.data = value;
				}
			}

			// Token: 0x1700053C RID: 1340
			// (get) Token: 0x06001646 RID: 5702 RVA: 0x00052074 File Offset: 0x00050274
			// (set) Token: 0x06001647 RID: 5703 RVA: 0x0005207C File Offset: 0x0005027C
			public bool AutoConvert
			{
				get
				{
					return this.autoconvert;
				}
				set
				{
					this.autoconvert = value;
				}
			}

			// Token: 0x06001648 RID: 5704 RVA: 0x00052088 File Offset: 0x00050288
			public static int Count(DataObject.Entry entries)
			{
				int num = 0;
				while (entries != null)
				{
					num++;
					entries = entries.next;
				}
				return num;
			}

			// Token: 0x06001649 RID: 5705 RVA: 0x000520B0 File Offset: 0x000502B0
			public static DataObject.Entry Find(DataObject.Entry entries, string type)
			{
				return DataObject.Entry.Find(entries, type, false);
			}

			// Token: 0x0600164A RID: 5706 RVA: 0x000520BC File Offset: 0x000502BC
			public static DataObject.Entry Find(DataObject.Entry entries, string type, bool only_convertible)
			{
				while (entries != null)
				{
					bool flag = true;
					if (only_convertible && !entries.autoconvert)
					{
						flag = false;
					}
					if (flag && string.Compare(entries.type, type, true) == 0)
					{
						return entries;
					}
					entries = entries.next;
				}
				return null;
			}

			// Token: 0x0600164B RID: 5707 RVA: 0x0005210C File Offset: 0x0005030C
			public static DataObject.Entry FindConvertible(DataObject.Entry entries, string type)
			{
				DataObject.Entry entry = DataObject.Entry.Find(entries, type);
				if (entry != null)
				{
					return entry;
				}
				if (type == DataFormats.StringFormat || type == DataFormats.Text || type == DataFormats.UnicodeText)
				{
					for (entry = entries; entry != null; entry = entry.next)
					{
						if (entry.type == DataFormats.StringFormat || entry.type == DataFormats.Text || entry.type == DataFormats.UnicodeText)
						{
							return entry;
						}
					}
				}
				return null;
			}

			// Token: 0x0600164C RID: 5708 RVA: 0x000521B0 File Offset: 0x000503B0
			public static string[] Entries(DataObject.Entry entries, bool convertible)
			{
				ArrayList arrayList = new ArrayList(DataObject.Entry.Count(entries));
				DataObject.Entry entry = entries;
				if (convertible)
				{
					DataObject.Entry entry2 = DataObject.Entry.Find(entries, DataFormats.Text);
					DataObject.Entry entry3 = DataObject.Entry.Find(entries, DataFormats.UnicodeText);
					DataObject.Entry entry4 = DataObject.Entry.Find(entries, DataFormats.StringFormat);
					bool flag = entry2 != null && entry2.AutoConvert;
					bool flag2 = entry3 != null && entry3.AutoConvert;
					bool flag3 = entry4 != null && entry4.AutoConvert;
					if (flag || flag2 || flag3)
					{
						arrayList.Add(DataFormats.StringFormat);
						arrayList.Add(DataFormats.UnicodeText);
						arrayList.Add(DataFormats.Text);
					}
				}
				while (entry != null)
				{
					if (!arrayList.Contains(entry.type))
					{
						arrayList.Add(entry.type);
					}
					entry = entry.next;
				}
				string[] array = new string[arrayList.Count];
				for (int i = 0; i < arrayList.Count; i++)
				{
					array[i] = (string)arrayList[i];
				}
				return array;
			}

			// Token: 0x04000C3D RID: 3133
			private string type;

			// Token: 0x04000C3E RID: 3134
			private object data;

			// Token: 0x04000C3F RID: 3135
			private bool autoconvert;

			// Token: 0x04000C40 RID: 3136
			internal DataObject.Entry next;
		}
	}
}
