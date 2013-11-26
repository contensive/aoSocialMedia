function getContent()
	'
	dim title
	dim image
	dim description
	dim contentType
	dim url
	dim siteName
	dim admins
	dim cs
	dim pageId
	'
	'	add on can be called from other addons this ensures it is only called once per page
	'
	if not cp.utils.encodeBoolean(ccLib.GetViewingProperty("Block Page Open Graph")) then
		pageId = cp.doc.pageId
		set cs = cp.csNew()
		'
		' URL
		'
		url = cp.doc.var("Open Graph URL")
		if URL="" then
			url = cp.content.getPageLink(pageId)
		end if
		call cp.doc.addHeadTag("<meta property=""og:url"" content=""" & url & """ />")
		'
		'	title appears to be required - if not set, use the page name
		'
		title = cp.doc.var("Open Graph Title")
		description = cp.doc.var("Open Graph Description")
		if title = "" then
			if pageId<>0 then
				call cs.Open( "meta content", "(recordid=" & pageId & ")" )
				if cs.ok then
					title = cs.getText("name")
					if title = "" then
						title = cp.doc.pageName
					end if
					'
					if description="" then
						description = cs.getText("metadescription")
					end if
				end if
				call cs.close
			end if
		end if
		if title<>"" then
			title = cp.Utils.ConvertHTML2Text(title)
			call cp.doc.addHeadTag("<meta property=""og:title"" content=""" & title & """ />")
		end if
		if description<>"" then
			description = cp.Utils.ConvertHTML2Text(description)
			call cp.doc.addHeadTag("<meta property=""og:description"" content=""" & description & """ />")
		end if
		'
		'	image is a required tag for Facebook - if not set use the Contensive logo
		'
		image = cp.doc.var("Open Graph Image")
		if image = "" then
			'
			' no image, use page image
			'
			if pageId<>0 then
				if cs.Open( "page content", "id=" & cp.doc.pageId, "imageFilename" ) then
					image = cs.getText("imageFilename")
					if image <> "" then
						image = "http://" & cp.site.Domain & cp.site.filepath & image
					end if
				end if
				call cs.close
			end if
			'
			' no page image, use site image
			'
			if image = "" then
				image = cp.site.GetProperty("pageImageFilename")
				if image <> "" then
					image = "http://" & cp.Site.Domain & cp.site.FilePath & cp.Utils.EncodeUrl(image)
				end if
			end if
		end if
		if image<>"" then
			cp.Utils.EncodeHtml(image)
			call cp.doc.addHeadTag("<meta property=""og:image"" content=""" & image & """ />")
		end if
		'
		' site name
		'
		siteName = cp.doc.var("Open Graph Site Name")
		if siteName="" then
			s = cp.site.getProperty( "facebook site_name" )
		end if
		if siteName="" then
			siteName = cp.site.name
		end if
		call cp.doc.addHeadTag("<meta property=""og:site_name"" content=""" & siteName & """ />")
		'
		'	if content type is not set, pull it from default default
		'		set in Open Graph Settings
		'
		contentType = cp.doc.var("Open Graph Content Type")
		if contentType = "" then
			contentType = "website"
		end if
		call cp.doc.addHeadTag("<meta property=""og:type"" content=""" & contentType & """ />")
		'
		' facebook admins
		'
		admins = cp.doc.var("Open Graph Admins")
		if admins = "" then
			admins = cp.site.getProperty("FACEBOOK ADMIN ID LIST")
		end if
		if admins<>"" then
			call cp.doc.addHeadTag("<meta property=""fb:admins"" content=""" & admins & """ />")
		end if
		'
		'	only allow this addon to run once per page
		'
		Call ccLib.SetViewingProperty("Block Page Open Graph", "true")
	end if
	'
end function



