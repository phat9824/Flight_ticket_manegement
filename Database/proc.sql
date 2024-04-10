Create proc proc_logic
@user char(40),
@pass int
as
Begin
	select * from NGUOIDUNG nd where nd.Email = @user and nd.passwordND = @pass
End
