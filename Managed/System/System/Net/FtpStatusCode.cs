using System;

namespace System.Net
{
	/// <summary>Specifies the status codes returned for a File Transfer Protocol (FTP) operation.</summary>
	// Token: 0x02000423 RID: 1059
	public enum FtpStatusCode
	{
		/// <summary>Included for completeness, this value is never returned by servers.</summary>
		// Token: 0x04001BE2 RID: 7138
		Undefined,
		/// <summary>Specifies that the response contains a restart marker reply. The text of the description that accompanies this status contains the user data stream marker and the server marker.</summary>
		// Token: 0x04001BE3 RID: 7139
		RestartMarker = 110,
		/// <summary>Specifies that the service is not available now; try your request later.</summary>
		// Token: 0x04001BE4 RID: 7140
		ServiceTemporarilyNotAvailable = 120,
		/// <summary>Specifies that the data connection is already open and the requested transfer is starting.</summary>
		// Token: 0x04001BE5 RID: 7141
		DataAlreadyOpen = 125,
		/// <summary>Specifies that the server is opening the data connection.</summary>
		// Token: 0x04001BE6 RID: 7142
		OpeningData = 150,
		/// <summary>Specifies that the command completed successfully.</summary>
		// Token: 0x04001BE7 RID: 7143
		CommandOK = 200,
		/// <summary>Specifies that the command is not implemented by the server because it is not needed.</summary>
		// Token: 0x04001BE8 RID: 7144
		CommandExtraneous = 202,
		/// <summary>Specifies the status of a directory.</summary>
		// Token: 0x04001BE9 RID: 7145
		DirectoryStatus = 212,
		/// <summary>Specifies the status of a file.</summary>
		// Token: 0x04001BEA RID: 7146
		FileStatus,
		/// <summary>Specifies the system type name using the system names published in the Assigned Numbers document published by the Internet Assigned Numbers Authority.</summary>
		// Token: 0x04001BEB RID: 7147
		SystemType = 215,
		/// <summary>Specifies that the server is ready for a user login operation.</summary>
		// Token: 0x04001BEC RID: 7148
		SendUserCommand = 220,
		/// <summary>Specifies that the server is closing the control connection.</summary>
		// Token: 0x04001BED RID: 7149
		ClosingControl,
		/// <summary>Specifies that the server is closing the data connection and that the requested file action was successful.</summary>
		// Token: 0x04001BEE RID: 7150
		ClosingData = 226,
		/// <summary>Specifies that the server is entering passive mode.</summary>
		// Token: 0x04001BEF RID: 7151
		EnteringPassive,
		/// <summary>Specifies that the user is logged in and can send commands.</summary>
		// Token: 0x04001BF0 RID: 7152
		LoggedInProceed = 230,
		/// <summary>Specifies that the server accepts the authentication mechanism specified by the client, and the exchange of security data is complete.</summary>
		// Token: 0x04001BF1 RID: 7153
		ServerWantsSecureSession = 234,
		/// <summary>Specifies that the requested file action completed successfully.</summary>
		// Token: 0x04001BF2 RID: 7154
		FileActionOK = 250,
		/// <summary>Specifies that the requested path name was created.</summary>
		// Token: 0x04001BF3 RID: 7155
		PathnameCreated = 257,
		/// <summary>Specifies that the server expects a password to be supplied.</summary>
		// Token: 0x04001BF4 RID: 7156
		SendPasswordCommand = 331,
		/// <summary>Specifies that the server requires a login account to be supplied.</summary>
		// Token: 0x04001BF5 RID: 7157
		NeedLoginAccount,
		/// <summary>Specifies that the requested file action requires additional information.</summary>
		// Token: 0x04001BF6 RID: 7158
		FileCommandPending = 350,
		/// <summary>Specifies that the service is not available.</summary>
		// Token: 0x04001BF7 RID: 7159
		ServiceNotAvailable = 421,
		/// <summary>Specifies that the data connection cannot be opened.</summary>
		// Token: 0x04001BF8 RID: 7160
		CantOpenData = 425,
		/// <summary>Specifies that the connection has been closed.</summary>
		// Token: 0x04001BF9 RID: 7161
		ConnectionClosed,
		/// <summary>Specifies that the requested action cannot be performed on the specified file because the file is not available or is being used.</summary>
		// Token: 0x04001BFA RID: 7162
		ActionNotTakenFileUnavailableOrBusy = 450,
		/// <summary>Specifies that an error occurred that prevented the request action from completing.</summary>
		// Token: 0x04001BFB RID: 7163
		ActionAbortedLocalProcessingError,
		/// <summary>Specifies that the requested action cannot be performed because there is not enough space on the server.</summary>
		// Token: 0x04001BFC RID: 7164
		ActionNotTakenInsufficientSpace,
		/// <summary>Specifies that the command has a syntax error or is not a command recognized by the server.</summary>
		// Token: 0x04001BFD RID: 7165
		CommandSyntaxError = 500,
		/// <summary>Specifies that one or more command arguments has a syntax error.</summary>
		// Token: 0x04001BFE RID: 7166
		ArgumentSyntaxError,
		/// <summary>Specifies that the command is not implemented by the FTP server.</summary>
		// Token: 0x04001BFF RID: 7167
		CommandNotImplemented,
		/// <summary>Specifies that the sequence of commands is not in the correct order.</summary>
		// Token: 0x04001C00 RID: 7168
		BadCommandSequence,
		/// <summary>Specifies that login information must be sent to the server.</summary>
		// Token: 0x04001C01 RID: 7169
		NotLoggedIn = 530,
		/// <summary>Specifies that a user account on the server is required.</summary>
		// Token: 0x04001C02 RID: 7170
		AccountNeeded = 532,
		/// <summary>Specifies that the requested action cannot be performed on the specified file because the file is not available.</summary>
		// Token: 0x04001C03 RID: 7171
		ActionNotTakenFileUnavailable = 550,
		/// <summary>Specifies that the requested action cannot be taken because the specified page type is unknown. Page types are described in RFC 959 Section 3.1.2.3</summary>
		// Token: 0x04001C04 RID: 7172
		ActionAbortedUnknownPageType,
		/// <summary>Specifies that the requested action cannot be performed.</summary>
		// Token: 0x04001C05 RID: 7173
		FileActionAborted,
		/// <summary>Specifies that the requested action cannot be performed on the specified file.</summary>
		// Token: 0x04001C06 RID: 7174
		ActionNotTakenFilenameNotAllowed
	}
}
