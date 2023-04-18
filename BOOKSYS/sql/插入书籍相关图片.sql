UPDATE dbo.TBook SET Photo = (SELECT * FROM OPENROWSET(BULK 'E:\C#课程\2020213774 黄雅莉 C#程序设计 课堂作业Lab10&11\BOOKSYS\img\数据结构.JPG', SINGLE_BLOB) AS x)
WHERE BookName = '数据结构及应用算法教程'

UPDATE dbo.TBook SET Photo = (SELECT * FROM OPENROWSET(BULK 'E:\C#课程\2020213774 黄雅莉 C#程序设计 课堂作业Lab10&11\BOOKSYS\img\面向对象软件工程.JPG', SINGLE_BLOB) AS x)
WHERE BookName = '面向对象软件工程'

UPDATE dbo.TBook SET Photo = (SELECT * FROM OPENROWSET(BULK 'E:\C#课程\2020213774 黄雅莉 C#程序设计 课堂作业Lab10&11\BOOKSYS\img\操作系统原理.JPG', SINGLE_BLOB) AS x)
WHERE BookName = '操作系统教程'

UPDATE dbo.TBook SET Photo = (SELECT * FROM OPENROWSET(BULK 'E:\C#课程\2020213774 黄雅莉 C#程序设计 课堂作业Lab10&11\BOOKSYS\img\Visual C#(2008版)应用案例教程.JPG', SINGLE_BLOB) AS x)
WHERE BookName = 'Visual C#(2008版)应用案例教程'

UPDATE dbo.TBook SET Photo = (SELECT * FROM OPENROWSET(BULK 'E:\C#课程\2020213774 黄雅莉 C#程序设计 课堂作业Lab10&11\BOOKSYS\img\计算机组成原理.JPG', SINGLE_BLOB) AS x)
WHERE BookName = '计算机组成原理'

UPDATE dbo.TBook SET Photo = (SELECT * FROM OPENROWSET(BULK 'E:\C#课程\2020213774 黄雅莉 C#程序设计 课堂作业Lab10&11\BOOKSYS\img\高等数学·上册.JPG', SINGLE_BLOB) AS x)
WHERE BookName = '高等数学·上册'

UPDATE dbo.TBook SET Photo = (SELECT * FROM OPENROWSET(BULK 'E:\C#课程\2020213774 黄雅莉 C#程序设计 课堂作业Lab10&11\BOOKSYS\img\高等数学·下册.JPG', SINGLE_BLOB) AS x)
WHERE BookName = '高等数学·下册'