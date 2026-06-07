using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClothingStore.Domain.Entities;
using ClothingStore.Domain.ValueObjects;
using ClothingStore.Infrastructure.Persistence.Repositories;
using ClothingStore.IntegrationTests.Common;
using FluentAssertions;

namespace ClothingStore.IntegrationTests.Repositories
{
    public  class CartRepoTests: IntegrationTestBase
    {
        [Fact]
        public async Task GetByUserIdAsync_Should_Return_Active_Cart()
        {
            var user = _context.UserProfiles.First();

            var cart = new Cart(user.Id);

            _context.Carts.Add(cart);

            await _context.SaveChangesAsync();

            var repo = new CartRepo(_context);

            var result = await repo.GetByUserIdAsync(
                user.Id,
                CancellationToken.None);

            result.Should().NotBeNull();

            result!.UserId.Should().Be(user.Id);
        }

        [Fact]
        public async Task GetByUserIdAsync_Should_Return_Null_When_User_Has_No_Cart()
        {
            var repo = new CartRepo(_context);

            var result = await repo.GetByUserIdAsync(
                999999,
                CancellationToken.None);

            result.Should().BeNull();
        }

        [Fact]
        public async Task UserHasActiveCartAsync_Should_Return_True_When_Cart_Exists()
        {
            var user = _context.UserProfiles.First();

            _context.Carts.Add(
                new Cart(user.Id));

            await _context.SaveChangesAsync();

            var repo = new CartRepo(_context);

            var result = await repo.UserHasActiveCartAsync(
                user.Id,
                CancellationToken.None);

            result.Should().BeTrue();
        }

        [Fact]
        public async Task UserHasActiveCartAsync_Should_Return_False_When_Cart_Does_Not_Exist()
        {
            var repo = new CartRepo(_context);

            var result = await repo.UserHasActiveCartAsync(
                999999,
                CancellationToken.None);

            result.Should().BeFalse();
        }

        [Fact]
        public async Task GetByPublicIdAsync_Should_Return_Cart()
        {
            var user = _context.UserProfiles.First();

            var cart = new Cart(user.Id);

            _context.Carts.Add(cart);

            await _context.SaveChangesAsync();

            var repo = new CartRepo(_context);

            var result = await repo.GetByPublicIdAsync(
                cart.PublicId,
                CancellationToken.None);

            result.Should().NotBeNull();

            result!.PublicId.Should().Be(cart.PublicId);
        }


        [Fact]
        public async Task GetByPublicIdAsync_Should_Return_Null_For_Invalid_Id()
        {
            var repo = new CartRepo(_context);

            var result = await repo.GetByPublicIdAsync(
                Guid.NewGuid(),
                CancellationToken.None);

            result.Should().BeNull();
        }

    }

}
