USE [MBOOK]
GO
/****** Object:  StoredProcedure [dbo].[Book_Return]    Script Date: 2023/4/17 20:40:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Book_Return] @in_ReaderID char(8), @in_ISBN char(18), @in_BookID char(10),
	@out_str char(30)OUTPUT
AS
BEGIN
	IF NOT EXISTS(SELECT * FROM TReader WHERE ReaderID=@in_ReaderID)
	BEGIN
		SET @out_str='该读者不存在'
		RETURN 0
	END
	IF NOT EXISTS(SELECT * FROM TBook WHERE ISBN=@in_ISBN)
	BEGIN
		SET @out_str='该图书不存在'
		RETURN 0
	END
	IF NOT EXISTS(SELECT * FROM TLend WHERE ISBN=@in_ISBN)
	BEGIN
		SET @out_str='该图书未被借出'
		RETURN 0
	END
	IF NOT EXISTS(SELECT * FROM TLend WHERE BookID=@in_BookID)
	BEGIN
		SET @out_str='该图书ID不存在'
		RETURN 0
	END
	BEGIN TRAN								/*开始一个事务*/
	DELETE FROM TLend WHERE ISBN=@in_ISBN AND BookID=@in_BookID
	IF @@ERROR>0							/*如果前面一条SQL语句出错则回滚事务并返回*/
	BEGIN
		ROLLBACK TRAN
		SET @out_str='执行过程中遇到错误'
		RETURN 0
	END
	UPDATE TReader SET Num=Num-1 WHERE ReaderID=@in_ReaderID
		IF @@ERROR>0							/*如果前面一条SQL语句出错则回滚事务并返回*/
	BEGIN
		ROLLBACK TRAN
		SET @out_str='执行过程中遇到错误'
		RETURN 0
	END
	UPDATE TBook SET SNum=SNum+1 WHERE ISBN=@in_ISBN
	IF @@ERROR=0							/*如果所有语句都不出错则结束事务并返回*/
	BEGIN
		COMMIT TRAN
		SET @out_str='还书成功'
		RETURN 1
	END
	ELSE									/*如果执行出错则回滚所有操作并返回*/
	BEGIN
		ROLLBACK TRAN
		SET @out_str='执行过程中遇到错误'
		RETURN 0
	END
END