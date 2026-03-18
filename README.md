# AutoMapperMIT Monorepo

This repository is a monorepo combining the latest MIT-licensed versions (as of March 17, 2026) of the following AutoMapper projects into a single codebase:

| NuGet Package | NuGet |
|---|---|
| [AutoMapperMIT](https://www.nuget.org/packages/AutoMapperMIT) | [![NuGet](https://img.shields.io/nuget/v/AutoMapperMIT.svg)](https://www.nuget.org/packages/AutoMapperMIT) |
| [AutoMapperMIT.Collection](https://www.nuget.org/packages/AutoMapperMIT.Collection) | [![NuGet](https://img.shields.io/nuget/v/AutoMapperMIT.Collection.svg)](https://www.nuget.org/packages/AutoMapperMIT.Collection) |
| [AutoMapperMIT.Collection.EntityFramework](https://www.nuget.org/packages/AutoMapperMIT.Collection.EntityFramework) | [![NuGet](https://img.shields.io/nuget/v/AutoMapperMIT.Collection.EntityFramework.svg)](https://www.nuget.org/packages/AutoMapperMIT.Collection.EntityFramework) |
| [AutoMapperMIT.Collection.EntityFrameworkCore](https://www.nuget.org/packages/AutoMapperMIT.Collection.EntityFrameworkCore) | [![NuGet](https://img.shields.io/nuget/v/AutoMapperMIT.Collection.EntityFrameworkCore.svg)](https://www.nuget.org/packages/AutoMapperMIT.Collection.EntityFrameworkCore) |
| [AutoMapperMIT.Collection.LinqToSQL](https://www.nuget.org/packages/AutoMapperMIT.Collection.LinqToSQL) | [![NuGet](https://img.shields.io/nuget/v/AutoMapperMIT.Collection.LinqToSQL.svg)](https://www.nuget.org/packages/AutoMapperMIT.Collection.LinqToSQL) |
| [AutoMapperMIT.Extensions.ExpressionMapping](https://www.nuget.org/packages/AutoMapperMIT.Extensions.ExpressionMapping) | [![NuGet](https://img.shields.io/nuget/v/AutoMapperMIT.Extensions.ExpressionMapping.svg)](https://www.nuget.org/packages/AutoMapperMIT.Extensions.ExpressionMapping) |

> **Note:** All packages in this repo are published under the **`AutoMapperMIT`** prefix to distinguish them from the original `AutoMapper` NuGet packages.

## Original Repositories

- [AutoMapper/AutoMapper.Archive](https://github.com/AutoMapper/AutoMapper.Archive)
- [AutoMapper/AutoMapper.Collection](https://github.com/AutoMapper/AutoMapper.Collection)
- [AutoMapper/AutoMapper.Collection.EFCore](https://github.com/AutoMapper/AutoMapper.Collection.EFCore)
- [AutoMapper/AutoMapper.Extensions.ExpressionMapping](https://github.com/AutoMapper/AutoMapper.Extensions.ExpressionMapping)

## Target Frameworks

While the original projects only targeted recent frameworks, the packages in this repo have broader support:

- **Main libraries** (AutoMapperMIT, AutoMapperMIT.Collection) — target **.NET Standard 2.0**
- **EF6** (AutoMapperMIT.Collection.EntityFramework) — targets both **.NET 4.6.1** and **.NET Standard 2.1**
- **EF Core** (AutoMapperMIT.Collection.EntityFrameworkCore) — targets **.NET 6.0+** / **EF Core 6.0+**

> **Note:** Testing is performed only on currently supported .NET Core frameworks.

## Documentation

- **AutoMapperMIT** — [README](README.AutoMapper.md) · [Documentation Index](docs/source/index.md)
- **AutoMapperMIT.Collection** and **AutoMapperMIT.Collection.EntityFramework** — [README](README.Collection.md)
- **AutoMapperMIT.Collection.EntityFrameworkCore** — [README](README.EFCore.md)
- **AutoMapperMIT.Extensions.ExpressionMapping** — [README](README.ExpressionMapping.md)

## License

These projects are licensed under the [MIT License](LICENSE.txt).

- Copyright &copy; 2010 Jimmy Bogard
- Copyright &copy; 2015 Tyler Carlson
- Copyright &copy; 2018 AutoMapper

The git histories from all original repositories have been preserved in this monorepo for license traceability.

## Credits

Glory to Jehovah, Lord of Lords and King of Kings, creator of Heaven and Earth, who through his Son Jesus Christ,
has reedemed me to become a child of God. -Shane32
