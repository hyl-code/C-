CREATE PROCEDURE Book_Borrow @in_ReaderID char(8), @in_ISBN char(18), @in_BookID char(10),
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
	IF(SELECT Num FROM TReader WHERE ReaderID=@in_ReaderID)=5
	BEGIN
		SET @out_str='读者借书量不能大于5'
		RETURN 0
	END
	IF(SELECT SNum FROM TBook WHERE ISBN=@in_ISBN)=0
	BEGIN
		SET @out_str='图书库存量为0'
		RETURN 0
	END
	IF @in_ISBN IN(SELECT ISBN FROM TLend WHERE ReaderID=@in_ReaderID)
	BEGIN
		SET @out_str='读者已经借过该书'
		RETURN 0
	END
	IF EXISTS(SELECT * FROM TLend WHERE BookID=@in_BookID)
	BEGIN
		SET @out_str='该图书ID已存在'
		RETURN 0
	END
	BEGIN TRAN								/*开始一个事务*/
	INSERT INTO TLend VALUES(@in_BookID, @in_ReaderID, @in_ISBN, GETDATE())
	IF @@ERROR>0							/*如果前面一条SQL语句出错则回滚事务并返回*/
	BEGIN
		ROLLBACK TRAN
		SET @out_str='执行过程中遇到错误'
		RETURN 0
	END
	UPDATE TReader SET Num=Num+1 WHERE ReaderID=@in_ReaderID
	IF @@ERROR>0							/*如果前面一条SQL语句出错则回滚事务并返回*/
	BEGIN
		ROLLBACK TRAN
		SET @out_str='执行过程中遇到错误'
		RETURN 0
	END
	UPDATE TBook SET SNum=SNum-1 WHERE ISBN=@in_ISBN
	IF @@ERROR=0							/*如果所有语句都不出错则结束事务并返回*/
	BEGIN
		COMMIT TRAN
		SET @out_str='借书成功'
		RETURN 1
	END
	ELSE									/*如果执行出错则回滚所有操作并返回*/
	BEGIN
		ROLLBACK TRAN
		SET @out_str='执行过程中遇到错误'
		RETURN 0
	END
END