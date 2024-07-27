﻿using Eventify.Common.Domain;
using Eventify.Modules.Events.Application.Categories.GetCategories;
using Eventify.Modules.Events.Application.Categories.GetCategory;
using Eventify.Modules.Events.IntegrationTests.Abstractions;
using FluentAssertions;

namespace Eventify.Modules.Events.IntegrationTests.Categories;

public class GetCategoriesTests : BaseIntegrationTest
{
    public GetCategoriesTests(IntegrationTestWebAppFactory factory)
        : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnEmptyCollection_WhenNoCategoriesExist()
    {
        // Arrange
        await CleanDatabaseAsync();

        var query = new GetCategoriesQuery();

        // Act
        Result<IReadOnlyCollection<CategoryResponse>> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task Should_ReturnCategory_WhenCategoryExists()
    {
        // Arrange
        await CleanDatabaseAsync();

        await Sender.CreateCategoryAsync(Faker.Music.Genre());
        await Sender.CreateCategoryAsync(Faker.Music.Genre());

        var query = new GetCategoriesQuery();

        // Act
        Result<IReadOnlyCollection<CategoryResponse>> result = await Sender.Send(query);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(2);
    }
}
