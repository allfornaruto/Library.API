
## .Net Core 常用命令

```
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
	"name":"author3",
	"age": 25,
	"email":"author3@qq.com",
	"birthPlace": "上海"
}
```