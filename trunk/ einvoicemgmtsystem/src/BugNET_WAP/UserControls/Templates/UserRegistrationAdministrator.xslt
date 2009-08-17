<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" >
<xsl:output method='text' />
	<xsl:param name="DateTime" />
	<xsl:param name="Username" />
	<xsl:param name="Email" />
<xsl:template match="/">
Date: <xsl:value-of select="$DateTime" />
Username: <xsl:value-of select="$Username" />
Email: <xsl:value-of select="$Email" />
</xsl:template>
</xsl:stylesheet>