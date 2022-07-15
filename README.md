# GodelTech.Data

.NET library to access data storage with Unit of Work, Repository and Entity classes

## Overview

`GodelTech.Data` project has interfaces for Unit of Work and Repository pattern.
Implementation of it for [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/) can be found in library [GodelTech.Data.EntityFrameworkCore](https://github.com/GodelTech/GodelTech.Data.EntityFrameworkCore)

## Extensions

You can find Repository extensions using `QueryParameters` and `ISpecification` that allows you create requests easier.

`QueryParameters.cs`
```csharp
public class QueryParameters<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    public FilterRule<TEntity, TKey> Filter { get; set; }

    public SortRule<TEntity, TKey> Sort { get; set; }

    public PageRule Page { get; set; }
}
```

`SpecificationBase.cs`
```csharp
public abstract class SpecificationBase<TEntity, TKey> : CompositeSpecification<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
{
    public override bool IsSatisfiedBy(TEntity candidate) => AsExpression().Compile().Invoke(candidate);

    public abstract Expression<Func<TEntity, bool>> AsExpression();
}
```

## Build

[![Azure DevOps builds (master)](https://img.shields.io/azure-devops/build/GodelTech/19324bbd-9baf-4407-b86d-3e7f0d145399/55/master?style=flat-square)](https://dev.azure.com/GodelTech/OpenSource/_build/latest?definitionId=55&branchName=master)
[![Azure DevOps tests (master)](https://img.shields.io/azure-devops/tests/GodelTech/OpenSource/55/master?style=flat-square)](https://dev.azure.com/GodelTech/OpenSource/_build/latest?definitionId=55&branchName=master)
[![Azure DevOps coverage (master)](https://img.shields.io/azure-devops/coverage/GodelTech/OpenSource/55/master?style=flat-square)](https://dev.azure.com/GodelTech/OpenSource/_build/latest?definitionId=55&branchName=master)
[![Sonar Quality Gate](https://img.shields.io/sonar/quality_gate/GodelTech.Data?server=https%3A%2F%2Fsonarcloud.io&style=flat-square)](https://sonarcloud.io/dashboard?id=GodelTech.Data)
[![Sonar Tech Debt](https://img.shields.io/sonar/tech_debt/GodelTech.Data?server=https%3A%2F%2Fsonarcloud.io&style=flat-square)](https://sonarcloud.io/dashboard?id=GodelTech.Data)
[![Sonar Violations](https://img.shields.io/sonar/violations/GodelTech.Data?format=long&server=https%3A%2F%2Fsonarcloud.io&style=flat-square)](https://sonarcloud.io/dashboard?id=GodelTech.Data)

## Artifacts

[![Azure DevOps builds (Artifacts)](https://img.shields.io/azure-devops/build/GodelTech/19324bbd-9baf-4407-b86d-3e7f0d145399/55/master?stage=Artifacts&style=flat-square)](https://dev.azure.com/GodelTech/OpenSource/_build/latest?definitionId=55&branchName=master)

## NuGet

[![Azure DevOps builds (NuGet)](https://img.shields.io/azure-devops/build/GodelTech/19324bbd-9baf-4407-b86d-3e7f0d145399/55/master?stage=NuGet&style=flat-square)](https://dev.azure.com/GodelTech/OpenSource/_build/latest?definitionId=55&branchName=master)
[![Nuget](https://img.shields.io/nuget/v/GodelTech.Data?style=flat-square)](https://www.nuget.org/packages/GodelTech.Data)
[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/GodelTech.Data?style=flat-square)](https://www.nuget.org/packages/GodelTech.Data)
[![Nuget](https://img.shields.io/nuget/dt/GodelTech.Data?style=flat-square)](https://www.nuget.org/packages/GodelTech.Data)
[![Libraries.io dependency status for specific release](https://img.shields.io/librariesio/release/NuGet/GodelTech.Data/latest?style=flat-square)](https://libraries.io/NuGet/GodelTech.Data)
