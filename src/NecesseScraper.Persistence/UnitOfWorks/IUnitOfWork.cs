﻿using NecesseScraper.Persistence.Repositories;

namespace NecesseScraper.Persistence.UnitOfWorks;

/// <summary>
///     This UnitOfWork contains all the Repositories used to query the all the tables/collections.
/// </summary>
public interface IUnitOfWork
{
    INecesseVersionRepository NecesseVersions { get; }
}