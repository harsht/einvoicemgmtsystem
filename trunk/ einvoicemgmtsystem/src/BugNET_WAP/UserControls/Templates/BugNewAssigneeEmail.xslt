<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
<xsl:output method='html' />
	<xsl:param name="Summary" />
	<xsl:param name="Project" />
	<xsl:param name="Version" />
	<xsl:param name="Priority" />
	<xsl:param name="Type" />
	<xsl:param name="DefaultUrl" />
	<xsl:param name="ProjectId" />
	<xsl:param name="IssueId" />
<xsl:template match="/">
<html>
<body>
<p>
	The following issue has been assigned to you: 
</p>
<table border="0">
<tr><td>Project:</td><td><b><xsl:value-of select="$Project" /></b></td></tr>
<tr><td>Summary:</td><td><b><xsl:value-of select="$Summary" /></b></td></tr>
<tr><td>Version:</td><td><b><xsl:value-of select="$Version" /></b></td></tr>
<tr><td>Priority:</td><td><b><xsl:value-of select="$Priority" /></b></td></tr>
<tr><td>Type:</td><td><b><xsl:value-of select="$Type" /></b></td></tr>
</table>
<p>
	<a>
		<xsl:attribute name="href">
			<xsl:value-of select="$DefaultUrl" />Bugs/BugDetail.aspx?pid=<xsl:value-of select="$ProjectId" />&amp;bid=<xsl:value-of select="$IssueId" />
		</xsl:attribute>
		Follow this link to obtain more information on this issue
	</a>
</p>
</body>
</html>
</xsl:template>
</xsl:stylesheet>