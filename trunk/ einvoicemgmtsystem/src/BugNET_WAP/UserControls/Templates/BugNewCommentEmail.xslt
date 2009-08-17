<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method='html' />
  <xsl:param name="Summary" />
  <xsl:param name="Author" />
  <xsl:param name="DateTime" />
  <xsl:param name="DefaultUrl" />
  <xsl:param name="ProjectId" />
  <xsl:param name="IssueId" />
  <xsl:param name="Comment" />
  <xsl:template match="/">
    <html>
      <body>
        <p>
          A new comment has been added to the following issue.
        </p>
        <table border="0">
          <tr>
            <td>Summary:</td>
            <td>
              <b>
                <xsl:value-of select="$Summary" />
              </b>
            </td>
          </tr>
          <tr>
            <td>
              New Comment by <xsl:value-of select="$Author" />
            </td>
            <td>
              on <xsl:value-of select="$DateTime" />
            </td>
          </tr>
          <tr>
            <td>
                <xsl:value-of select="$Comment" />
            </td>
          </tr>
        </table>
        <p>
          <a>
            <xsl:attribute name="href">
              <xsl:value-of select="$DefaultUrl" />Bugs/BugDetail.aspx?pid=<xsl:value-of select="$ProjectId" />&amp;bid=<xsl:value-of select="$IssueId" />
            </xsl:attribute>
            Follow this link to obtain more information on this issue.
          </a>
        </p>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>

