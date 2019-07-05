# eoffice-qn-kyso

## Tutorial to use kyso application 

Change some key in app.config

1. Change the host and account to access ftp server : ftpHost, ftpUsername, ftpPassword

2. Change the folder name for using store pdf file in FTP server : ftpServerFolder

3. Change the location of exe file that is currently storing

4. Insert a link like this to the href property in a tag
   '''
   		qoffice-ky-so:trangThaiKySo=[TrangThaiKySoEnum];giaiDoanKySo=[GiaiDoanKySoEnum];noiDung=[noiDung];duThaoId=[duThaoId];chucDanhId=[chucDanhId];id=[id];yKien=[yKien];token=[token]
   '''