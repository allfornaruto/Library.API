
## .Net Core 常用命令

```
dotnet tool install --global dotnet-ef

dotnet add package <NuGet程序包>

dotnet build

dotnet run

dotnet restore

dotnet pack

dotnet test

```

## Entity Framework Core 常用命令

```
dotnet ef migrations add <迁移名称>

dotnet ef database update

dotnet ef migrations script

```

## Request Demo

1. 查询所有作者

```
GET
http://localhost:5000/api/authorsDb

```

2. 查询某位作者

```
GET
http://localhost:5000/api/authorsDb/72d5b5f5-3008-49b7-b0d6-cc337f1a3330
```

3. 创建一位作者

```
POST
http://localhost:5000/api/authorsDb
{
	"name":"author12",
	"birthDate": "1990-01-01",
	"email":"author12@qq.com",
	"birthPlace": "香港"
}
```

4. 删除一位作者

```
DELETE
http://localhost:5000/api/authorsDb/04b3eb99-1593-4eb0-ebce-08d807a7e7ba
```

5. 查询某作者的所有书籍

```
GET
http://localhost:5000/api/authorsDb/72d5b5f5-3008-49b7-b0d6-cc337f1a3330/books
```

6. 查询某作者的某本书籍

```
GET
http://localhost:5000/api/authorsDb/72d5b5f5-3008-49b7-b0d6-cc337f1a3330/books/7d8ebda9-2634-4c0f-9469-0695d6132153
```

7. 给某作者新增一本书籍

```
POST
http://localhost:5000/api/authorsDb/72d5b5f5-3008-49b7-b0d6-cc337f1a3330/books
{
	"title": "海底两万里",
	"description": "科幻名著",
	"pages": 300
}
```

8. 完全更新某作者的某一本书籍信息

```
PUT
http://localhost:5000/api/authorsDb/72d5b5f5-3008-49b7-b0d6-cc337f1a3330/books/fd380247-9607-406a-8539-08d8085eb493
{
	"title": "神秘岛",
	"description": "科幻名著",
	"pages": 500
}
```

9. 部分更新某作者的某一本书籍的信息

```
PATCH
http://localhost:5000/api/authorsDb/72d5b5f5-3008-49b7-b0d6-cc337f1a3330/books/fd380247-9607-406a-8539-08d8085eb493
[{
	"op": "replace",
	"path": "/pages",
	"value": 505
}]
```

10. 查询所有作者（分页）

```
GET
http://localhost:5000/api/authorsDb?pageNumber=1&pageSize=2

```

11. 查询符合条件的作者（分页+条件过滤）

```
GET
http://localhost:5000/api/authorsDb?pageNumber=1&pageSize=2&birthPlace=上海

```

12. 搜索符合条件的作者（分页+搜索）

```
GET
http://localhost:5000/api/authorsDb?pageNumber=1&pageSize=1&searchQuery=深圳

```

13. 搜索符合条件的作者（排序）

```
GET
http://localhost:5000/api/authorsDb?sortBy=age

```