UPDATE dbo.TBook SET Photo = (SELECT * FROM OPENROWSET(BULK 'E:\C#�γ�\2020213774 ������ C#������� ������ҵLab10&11\BOOKSYS\img\���ݽṹ.JPG', SINGLE_BLOB) AS x)
WHERE BookName = '���ݽṹ��Ӧ���㷨�̳�'

UPDATE dbo.TBook SET Photo = (SELECT * FROM OPENROWSET(BULK 'E:\C#�γ�\2020213774 ������ C#������� ������ҵLab10&11\BOOKSYS\img\��������������.JPG', SINGLE_BLOB) AS x)
WHERE BookName = '��������������'

UPDATE dbo.TBook SET Photo = (SELECT * FROM OPENROWSET(BULK 'E:\C#�γ�\2020213774 ������ C#������� ������ҵLab10&11\BOOKSYS\img\����ϵͳԭ��.JPG', SINGLE_BLOB) AS x)
WHERE BookName = '����ϵͳ�̳�'

UPDATE dbo.TBook SET Photo = (SELECT * FROM OPENROWSET(BULK 'E:\C#�γ�\2020213774 ������ C#������� ������ҵLab10&11\BOOKSYS\img\Visual C#(2008��)Ӧ�ð����̳�.JPG', SINGLE_BLOB) AS x)
WHERE BookName = 'Visual C#(2008��)Ӧ�ð����̳�'

UPDATE dbo.TBook SET Photo = (SELECT * FROM OPENROWSET(BULK 'E:\C#�γ�\2020213774 ������ C#������� ������ҵLab10&11\BOOKSYS\img\��������ԭ��.JPG', SINGLE_BLOB) AS x)
WHERE BookName = '��������ԭ��'

UPDATE dbo.TBook SET Photo = (SELECT * FROM OPENROWSET(BULK 'E:\C#�γ�\2020213774 ������ C#������� ������ҵLab10&11\BOOKSYS\img\�ߵ���ѧ���ϲ�.JPG', SINGLE_BLOB) AS x)
WHERE BookName = '�ߵ���ѧ���ϲ�'

UPDATE dbo.TBook SET Photo = (SELECT * FROM OPENROWSET(BULK 'E:\C#�γ�\2020213774 ������ C#������� ������ҵLab10&11\BOOKSYS\img\�ߵ���ѧ���²�.JPG', SINGLE_BLOB) AS x)
WHERE BookName = '�ߵ���ѧ���²�'