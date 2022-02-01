using FastDrink.Application.BaseTypes.Commands;
using FastDrink.Application.Common.Models;
using FastDrink.Domain.Common;
using FastDrink.Domain.Entities;
using FastDrink.IntegrationTests.Fixtures;
using FluentAssertions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace FastDrink.IntegrationTests;

public class BaseTypeControllersTest : IntegrationTest
{
    private const string CategoryRootApi = "/api/Category";
    private const string ContainerRootApi = "/api/Container";
    private const string BrandRootApi = "/api/Brand";

    public BaseTypeControllersTest(ApiWebAppFactory fixture) : base(fixture)
    {
    }

    #region GetAll Test
    [Fact]
    public async Task GetAll_Ok()
    {
        await GetAllHelper_Ok<Category>(CategoryRootApi);
        await GetAllHelper_Ok<Container>(ContainerRootApi);
        await GetAllHelper_Ok<Brand>(BrandRootApi);
    }

    private async Task GetAllHelper_Ok<T>(string path) where T : BaseType
    {
        // Act
        var response = await _client.GetAsync(path);
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    #endregion

    #region GetById Test
    [Fact]
    public async Task GetById_Ok()
    {
        await GetByIdHelper_Ok<Category>(CategoryRootApi);
        await GetByIdHelper_Ok<Container>(ContainerRootApi);
        await GetByIdHelper_Ok<Brand>(BrandRootApi);
    }

    private async Task GetByIdHelper_Ok<T>(string path) where T : BaseType
    {
        // Arrange
        await LoginAdmin();
        var category = new CreateBaseTypeCommand<Category>
        {
            Name = "newCategory"
        };
        await _client.PostAsJsonAsync(path, category);

        // Act
        var result = await _client.GetAsync(path + "/1");
        var categoryResult = await result.Content.ReadAsAsync<Category>();

        // Assert

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        categoryResult.Should().NotBeNull();

        // End
        await LogOut();
    }

    [Fact]
    public async Task GetByIdHelper_Not_Found()
    {
        await GetIdHelper_Not_Found<Category>(CategoryRootApi);
        await GetIdHelper_Not_Found<Container>(ContainerRootApi);
        await GetIdHelper_Not_Found<Brand>(BrandRootApi);
    }

    private async Task GetIdHelper_Not_Found<T>(string path) where T : BaseType
    {
        // Arrange
        await LoginAdmin();

        // Act
        var result = await _client.GetAsync(path + "/9999");
        var content = await result.Content.ReadAsStringAsync();

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        content.Should().NotBeNullOrEmpty();
        content.Should().Be($"No exist {typeof(T).Name} with 9999 id.");

        // End
        await LogOut();
    }
    #endregion

    #region Create Test
    [Fact]
    public async Task Create_Ok()
    {
        await CreateHelper_Ok<Category>(CategoryRootApi);
        await CreateHelper_Ok<Container>(ContainerRootApi);
        await CreateHelper_Ok<Brand>(BrandRootApi);
    }

    private async Task CreateHelper_Ok<T>(string path) where T : BaseType
    {
        // Arrange
        await LoginAdmin();
        var entity = new CreateBaseTypeCommand<T>
        {
            Name = "newEntity"
        };

        // Act
        var responseMessage = await _client.PostAsJsonAsync(path, entity);

        // Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // End
        await LogOut();
    }

    [Fact]
    public async Task Create_Unauthorized()
    {
        await CreateHelper_Unauthorized<Category>(CategoryRootApi);
        await CreateHelper_Unauthorized<Container>(ContainerRootApi);
        await CreateHelper_Unauthorized<Brand>(BrandRootApi);
    }

    private async Task CreateHelper_Unauthorized<T>(string path) where T : BaseType
    {
        // Arrange
        var entity = new CreateBaseTypeCommand<T>
        {
            Name = "newEntity"
        };

        // Act
        var responseMessage = await _client.PostAsJsonAsync(path, entity);

        // Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Create_Already_Exist()
    {
        await CreateHelper_Already_Exist<Category>(CategoryRootApi);
        await CreateHelper_Already_Exist<Container>(ContainerRootApi);
        await CreateHelper_Already_Exist<Brand>(BrandRootApi);
    }

    private async Task CreateHelper_Already_Exist<T>(string path) where T : BaseType
    {
        // Arrange
        await LoginAdmin();
        var entity = new CreateBaseTypeCommand<T>
        {
            Name = "newEntity2"
        };
        await _client.PostAsJsonAsync(path, entity);

        // Act
        var responseMessage = await _client.PostAsJsonAsync(path, entity);
        var content = await responseMessage.Content.ReadAsAsync<Result>();

        // Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        content.Errors.Should().Contain($"Already exist a {typeof(T).Name} with the name newEntity2.");

        // End
        await LogOut();
    }
    #endregion

    #region Update Test
    [Fact]
    public async Task Update_Ok()
    {
        await UpdateHelper_Ok<Category>(CategoryRootApi);
        await UpdateHelper_Ok<Container>(ContainerRootApi);
        await UpdateHelper_Ok<Brand>(BrandRootApi);
    }

    private async Task UpdateHelper_Ok<T>(string path) where T : BaseType
    {
        // Arrange
        await LoginAdmin();

        var entityCreated = new CreateBaseTypeCommand<T>
        {
            Name = "newEntity3"
        };
        await _client.PostAsJsonAsync(path, entityCreated);

        var entity = new UpdateBaseTypeCommand<T>
        {
            Id = 1,
            NewName = "updatedEntity"
        };

        // Act
        var responseMessage = await _client.PutAsJsonAsync(path, entity);

        // Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // End
        await LogOut();
    }

    [Fact]
    public async Task Update_Unauthorized()
    {
        await UpdateHelper_Unauthorized<Category>(CategoryRootApi);
        await UpdateHelper_Unauthorized<Container>(ContainerRootApi);
        await UpdateHelper_Unauthorized<Brand>(BrandRootApi);
    }

    private async Task UpdateHelper_Unauthorized<T>(string path) where T : BaseType
    {
        // Arrange
        var entity = new UpdateBaseTypeCommand<T>
        {
            Id = 1,
            NewName = "updatedEntity"
        };

        // Act
        var responseMessage = await _client.PutAsJsonAsync(path, entity);

        // Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Update_Not_Exist()
    {
        await UpdateHelper_Not_Exist<Category>(CategoryRootApi);
        await UpdateHelper_Not_Exist<Container>(ContainerRootApi);
        await UpdateHelper_Not_Exist<Brand>(BrandRootApi);
    }

    private async Task UpdateHelper_Not_Exist<T>(string path) where T : BaseType
    {
        // Arrange
        await LoginAdmin();

        var entity = new UpdateBaseTypeCommand<T>
        {
            Id = 9999,
            NewName = "updatedEntity"
        };

        // Act
        var responseMessage = await _client.PutAsJsonAsync(path, entity);
        var content = await responseMessage.Content.ReadAsAsync<Result>();

        // Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        content.Errors.Should().Contain($"No exist a {typeof(T).Name} with the Id 9999.");

        // End
        await LogOut();
    }
    #endregion

    #region Delete Test
    [Fact]
    public async Task Delete_Ok()
    {
        await DeleteHelper_Ok<Category>(CategoryRootApi);
        await DeleteHelper_Ok<Container>(ContainerRootApi);
        await DeleteHelper_Ok<Brand>(BrandRootApi);
    }

    private async Task DeleteHelper_Ok<T>(string path) where T : BaseType
    {
        // Arrange
        await LoginAdmin();
        var entityCreated = new CreateBaseTypeCommand<Category>
        {
            Name = "newEntity4"
        };
        await _client.PostAsJsonAsync(path, entityCreated);

        entityCreated = new CreateBaseTypeCommand<Category>
        {
            Name = "newEntity5"
        };
        await _client.PostAsJsonAsync(path, entityCreated);

        // Act
        var responseMessage = await _client.DeleteAsync(path + "/2");

        // Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.NoContent);

        // End
        await LogOut();
    }

    [Fact]
    public async Task Delete_Unauthorized()
    {
        await DeleteHelper_Unauthorized<Category>(CategoryRootApi);
        await DeleteHelper_Unauthorized<Container>(ContainerRootApi);
        await DeleteHelper_Unauthorized<Brand>(BrandRootApi);
    }

    private async Task DeleteHelper_Unauthorized<T>(string path) where T : BaseType
    {
        // Arrange
        await LoginAdmin();
        var entityCreated = new CreateBaseTypeCommand<Category>
        {
            Name = "newEntity4"
        };
        await _client.PostAsJsonAsync(path, entityCreated);
        await LogOut();

        // Act
        var responseMessage = await _client.DeleteAsync(path + "/1");

        // Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Delete_Not_Exist()
    {
        await DeleteHelper_Not_Exist<Category>(CategoryRootApi);
        await DeleteHelper_Not_Exist<Container>(ContainerRootApi);
        await DeleteHelper_Not_Exist<Brand>(BrandRootApi);
    }

    private async Task DeleteHelper_Not_Exist<T>(string path) where T : BaseType
    {
        // Arrange
        await LoginAdmin();

        // Act
        var responseMessage = await _client.DeleteAsync(path + "/9999");
        var content = await responseMessage.Content.ReadAsAsync<Result>();

        // Assert
        responseMessage.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        content.Errors.Should().Contain($"No exist a {typeof(T).Name} with the Id 9999.");

        // End
        await LogOut();
    }
    #endregion
}
