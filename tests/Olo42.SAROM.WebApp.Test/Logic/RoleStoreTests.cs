using System;
using System.Threading;
using NUnit.Framework;
using Olo42.SAROM.DataAccess.Contracts;
using Olo42.SAROM.WebApp.Logic;

namespace Olo42.SAROM.WebApp.Tests.Logic
{
  [TestFixture]
  public class RoleStoreTests
  {
    [Test]
    public void CreateAsync_ThrowsNotImplementedException()
    {
      // Arrange
      var roleStore = new RoleStore<Role>();
      var role = new Role();

      // Act
      // Assert
      Assert.That(
        delegate { roleStore.CreateAsync(role, CancellationToken.None); },
        Throws.InstanceOf(typeof(NotImplementedException)));
    }

    [Test]
    public void DeleteAsync_ThrowsNotImplementedException()
    {
      // Arrange
      var roleStore = new RoleStore<Role>();
      var role = new Role();

      // Act
      // Assert
      Assert.That(
        delegate { roleStore.DeleteAsync(role, CancellationToken.None); },
        Throws.InstanceOf(typeof(NotImplementedException)));
    }

    [TestCase("Administrator")]
    [TestCase("User")]
    public void FindById_FindsTheOnlyTwoRoles(string roleId)
    {
      // Arrange
      var roleStore = new RoleStore<Role>();

      // Act
      var result = roleStore.FindByIdAsync(roleId, CancellationToken.None);

      // Assert
      Assert.That(result.Result.Name, Is.EqualTo(roleId));
    }

    [TestCase("Administrator")]
    [TestCase("User")]
    [TestCase("administrator")]
    [TestCase("user")]
    public void FindByName_FindsTheOnlyTwoRoles(string normalizedRoleName)
    {
      // Arrange
      var roleStore = new RoleStore<Role>();

      // Act
      var result =
        roleStore.FindByNameAsync(normalizedRoleName, CancellationToken.None);

      // Assert
      Assert.That(
        result.Result.Name.ToString().ToUpperInvariant(),
        Is.EqualTo(normalizedRoleName.ToUpperInvariant()));
    }

    [TestCase(ERoleName.Administrator)]
    [TestCase(ERoleName.User)]
    public void GetNormalizedRoleName_ReturnsNormalizedRoleName(ERoleName roleName)
    {
      // Arrange
      var roleStore = new RoleStore<Role>();
      var role = new Role { Name = roleName };

      // Act
      var result =
        roleStore.GetNormalizedRoleNameAsync(role, CancellationToken.None);

      // Assert
      Assert.That(
        result.Result, 
        Is.EqualTo(roleName.ToString().ToUpperInvariant()));
    }

    [Test]
    public void GetNormalizedRoleName_ReturnsNullIfRoleIsNull()
    {
      // Arrange
      var roleStore = new RoleStore<Role>();
      Role role = null;

      // Act
      var result =
        roleStore.GetNormalizedRoleNameAsync(role, CancellationToken.None);

      // Assert
      Assert.That(result, Is.Null);
    }

    [Test]
    public void GetRoleId_ReturnsNullIfRoleIsNull()
    {
      // Arrange
      var roleStore = new RoleStore<Role>();
      Role role = null;

      // Act
      var result =
        roleStore.GetRoleIdAsync(role, CancellationToken.None);

      // Assert
      Assert.That(result, Is.Null);
    }

    [TestCase(ERoleName.Administrator)]
    [TestCase(ERoleName.User)]
    public void GetRoleId_ReturnsRoleName(ERoleName roleName)
    {
      // Arrange
      var roleStore = new RoleStore<Role>();
      var role = new Role { Name =  roleName};

      // Act
      var result =
        roleStore.GetRoleIdAsync(role, CancellationToken.None);

      // Assert
      Assert.That(result.Result, Is.EqualTo(roleName));
    }

    [Test]
    public void GetRoleName_ReturnsNullIfRoleIsNull()
    {
      // Arrange
      var roleStore = new RoleStore<Role>();
      Role role = null;

      // Act
      var result =
        roleStore.GetRoleNameAsync(role, CancellationToken.None);

      // Assert
      Assert.That(result, Is.Null);
    }

    [TestCase(ERoleName.Administrator)]
    [TestCase(ERoleName.User)]
    public void GetRoleName_ReturnsRoleName(ERoleName roleName)
    {
      // Arrange
      var roleStore = new RoleStore<Role>();
      var role = new Role { Name =  roleName};

      // Act
      var result =
        roleStore.GetRoleNameAsync(role, CancellationToken.None);

      // Assert
      Assert.That(result.Result, Is.EqualTo(roleName));
    }

    [Test]
    public void SetNormalizedRoleName_ThrowsnotImplementedException()
    {
      // Arrange
      var roleStore = new RoleStore<Role>();
      var role = new Role();
      var normalizedName = "AnyName";

      // Act
      // Assert
      Assert.That(
        () => roleStore.SetNormalizedRoleNameAsync(
        role, normalizedName, CancellationToken.None),
        Throws.InstanceOf(typeof(NotImplementedException)));
    }

    [Test]
    public void Update_ThrowsNotImplementedException()
    {
      // Arrange
      var roleStore = new RoleStore<Role>();
      var role = new Role();

      // Act
      // Assert
      Assert.That(
        () => roleStore.UpdateAsync(role, CancellationToken.None),
        Throws.InstanceOf(typeof(NotImplementedException)));
    }
  }
}