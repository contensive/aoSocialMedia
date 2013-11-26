function m
	dim cs
	dim imageFilename
	dim s
	dim pageId
	'
	set cs = cp.csNew
	'pageId = cp.doc.pageId
	''m="<div>(" & pageid & ")</div>"
	'call cs.Open( "page content", "id=" & pageId, "imageFilename" )
	'if cs.ok then
	'	imageFilename = cs.getText("imageFilename")
	'	if imageFilename <>"" then
	'		call cp.doc.setField( "Open Graph Image", "http://" & cp.site.Domain & cp.site.filepath & imageFilename)
	'		' call ccLib.SetViewingProperty("Open Graph Image", "http://" & cp.site.Domain & cp.site.filepath & imageFilename)
	'	end if
	'end if
	'call cs.close
	'
	' temp solution - meta content should be moved back to page content
	'
	s = cp.site.getProperty( "facebook site_name" )
	if s="" then
		s = cp.site.name
	end if
	'
	call ccLib.SetViewingProperty("Open Graph Site Name", cp.utils.encodeHtml(s))
	call ccLib.SetViewingProperty("Open Graph Content Type", "website")
	call ccLib.SetViewingProperty("Open Graph URL", cp.content.getPageLink(pageId))
	'
	call cs.Open( "meta content", "(recordid=" & cp.doc.pageId & ")" )
	if cs.ok then
		s = cs.getText("name")
		if s = "" then
			s = cp.doc.pageName
		end if
		call ccLib.SetViewingProperty("Open Graph Title", cStr(s))
		'
		s = cs.getText("metadescription")
		if s="" then
			s = cp.doc.pageName
		end if
		'
		call ccLib.SetViewingProperty("Open Graph Description", cStr(s))
		'
	end if
	call cs.close
	'
end function



