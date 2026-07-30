using System;

namespace System.Data.OleDb
{
	/// <summary>Returns information about literals used in text commands, data values, and database objects.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012F RID: 303
	[MonoTODO("OleDb is not implemented.")]
	public enum OleDbLiteral
	{
		/// <summary>A binary literal in a text command. Maps to DBLITERAL_BINARY_LITERAL.</summary>
		// Token: 0x04000A2E RID: 2606
		Binary_Literal = 1,
		/// <summary>A catalog name in a text command. Maps to DBLITERAL_CATALOG_NAME.</summary>
		// Token: 0x04000A2F RID: 2607
		Catalog_Name,
		/// <summary>The character that separates the catalog name from the rest of the identifier in a text command. Maps to DBLITERAL_CATALOG_SEPARATOR.</summary>
		// Token: 0x04000A30 RID: 2608
		Catalog_Separator,
		/// <summary>A character literal in a text command. Maps to DBLITERAL_CHAR_LITERAL.</summary>
		// Token: 0x04000A31 RID: 2609
		Char_Literal,
		/// <summary>A column alias in a text command. Maps to DBLITERAL_COLUMN_ALIAS.</summary>
		// Token: 0x04000A32 RID: 2610
		Column_Alias,
		/// <summary>A column name used in a text command or in a data-definition interface. Maps to DBLITERAL_COLUMN_NAME.</summary>
		// Token: 0x04000A33 RID: 2611
		Column_Name,
		/// <summary>A correlation name (table alias) in a text command. Maps to DBLITERAL_CORRELATION_NAME.</summary>
		// Token: 0x04000A34 RID: 2612
		Correlation_Name,
		/// <summary>The name of a cube in a schema (or the catalog if the provider does not support schemas).</summary>
		// Token: 0x04000A35 RID: 2613
		Cube_Name = 21,
		/// <summary>A cursor name in a text command. Maps to DBLITERAL_CURSOR_NAME.</summary>
		// Token: 0x04000A36 RID: 2614
		Cursor_Name = 8,
		/// <summary>The name of the dimension. If a dimension is part of more than one cube, there is one row for each cube/dimension combination.</summary>
		// Token: 0x04000A37 RID: 2615
		Dimension_Name = 22,
		/// <summary>The character used in a LIKE clause to escape the character returned for the DBLITERAL_LIKE_PERCENT literal. For example, if a percent sign (%) is used to match zero or more characters and this is a backslash (\), the characters "abc\%%" match all character values that start with "abc%". Some SQL dialects support a clause (the ESCAPE clause) that can be used to override this value. Maps to DBLITERAL_ESCAPE_PERCENT_PREFIX.</summary>
		// Token: 0x04000A38 RID: 2616
		Escape_Percent_Prefix = 9,
		/// <summary>The escape character, if any, used to suffix the character returned for the DBLITERAL_LIKE_PERCENT literal. For example, if a percent sign (%) is used to match zero or more characters and percent signs are escaped by enclosing in open and close square brackets, DBLITERAL_ESCAPE_PERCENT_PREFIX is "[", DBLITERAL_ESCAPE_PERCENT_SUFFIX is "]", and the characters "abc[%]%" match all character values that start with "abc%". Providers that do not use a suffix character to escape the DBLITERAL_ESCAPE_PERCENT character do not return this literal value and can set the lt member of the DBLITERAL structure to DBLITERAL_INVALID if requested. Maps to DBLITERAL_ESCAPE_PERCENT_SUFFIX.</summary>
		// Token: 0x04000A39 RID: 2617
		Escape_Percent_Suffix = 29,
		/// <summary>The character used in a LIKE clause to escape the character returned for the DBLITERAL_LIKE_UNDERSCORE literal. For example, if an underscore (_) is used to match exactly one character and this is a backslash (\), the characters "abc\_ _" match all character values that are five characters long and start with "abc_". Some SQL dialects support a clause (the ESCAPE clause) that can be used to override this value. Maps to DBLITERAL_ESCAPE_UNDERSCORE_PREFIX.</summary>
		// Token: 0x04000A3A RID: 2618
		Escape_Underscore_Prefix = 10,
		/// <summary>The character used in a LIKE clause to escape the character returned for the DBLITERAL_LIKE_UNDERSCORE literal. For example, if an underscore (_) is used to match exactly one character and this is a backslash (\), the characters "abc\_ _" match all character values that are five characters long and start with "abc_". Some SQL dialects support a clause (the ESCAPE clause) that can be used to override this value. Maps to DBLITERAL_ESCAPE_UNDERSCORE_SUFFIX.</summary>
		// Token: 0x04000A3B RID: 2619
		Escape_Underscore_Suffix = 30,
		/// <summary>The name of the hierarchy. If the dimension does not contain a hierarchy or has only one hierarchy, the current column contains a null value.</summary>
		// Token: 0x04000A3C RID: 2620
		Hierarchy_Name = 23,
		/// <summary>An index name used in a text command or in a data-definition interface. Maps to DBLITERAL_INDEX_NAME.</summary>
		// Token: 0x04000A3D RID: 2621
		Index_Name = 11,
		/// <summary>An invalid value. Maps to DBLITERAL_INVALID.</summary>
		// Token: 0x04000A3E RID: 2622
		Invalid = 0,
		/// <summary>Name of the cube to which the current level belongs.</summary>
		// Token: 0x04000A3F RID: 2623
		Level_Name = 24,
		/// <summary>The character used in a LIKE clause to match zero or more characters. For example, if this is a percent sign (%), the characters "abc%" match all character values that start with "abc". Maps to DBLITERAL_LIKE_PERCENT.</summary>
		// Token: 0x04000A40 RID: 2624
		Like_Percent = 12,
		/// <summary>The character used in a LIKE clause to match exactly one character. For example, if this is an underscore (_), the characters "abc_" match all character values that are four characters long and start with "abc". Maps to DBLITERAL_LIKE_UNDERSCORE.</summary>
		// Token: 0x04000A41 RID: 2625
		Like_Underscore,
		/// <summary>The name of the member.</summary>
		// Token: 0x04000A42 RID: 2626
		Member_Name = 25,
		/// <summary>A procedure name in a text command. Maps to DBLITERAL_PROCEDURE_NAME.</summary>
		// Token: 0x04000A43 RID: 2627
		Procedure_Name = 14,
		/// <summary>The name of the property.</summary>
		// Token: 0x04000A44 RID: 2628
		Property_Name = 26,
		/// <summary>The character used in a text command as the opening quote for quoting identifiers that contain special characters. Maps to DBLITERAL_QUOTE_PREFIX.</summary>
		// Token: 0x04000A45 RID: 2629
		Quote_Prefix = 15,
		/// <summary>The character used in a text command as the closing quote for quoting identifiers that contain special characters. 1.x providers that use the same character as the prefix and suffix may not return this literal value and can set the member of the DBLITERAL structure to DBLITERAL_INVALID if requested. Maps to DBLITERAL_QUOTE_SUFFIX.</summary>
		// Token: 0x04000A46 RID: 2630
		Quote_Suffix = 28,
		/// <summary>A schema name in a text command. Maps to DBLITERAL_SCHEMA_NAME.</summary>
		// Token: 0x04000A47 RID: 2631
		Schema_Name = 16,
		/// <summary>The character that separates the schema name from the rest of the identifier in a text command. Maps to DBLITERAL_SCHEMA_SEPARATOR.</summary>
		// Token: 0x04000A48 RID: 2632
		Schema_Separator = 27,
		/// <summary>A table name used in a text command or in a data-definition interface. Maps to DBLITERAL_TABLE_NAME.</summary>
		// Token: 0x04000A49 RID: 2633
		Table_Name = 17,
		/// <summary>A text command, such as an SQL statement. Maps to DBLITERAL_TEXT_COMMAND.</summary>
		// Token: 0x04000A4A RID: 2634
		Text_Command,
		/// <summary>A user name in a text command. Maps to DBLITERAL_USER_NAME.</summary>
		// Token: 0x04000A4B RID: 2635
		User_Name,
		/// <summary>A view name in a text command. Maps to DBLITERAL_VIEW_NAME.</summary>
		// Token: 0x04000A4C RID: 2636
		View_Name
	}
}
