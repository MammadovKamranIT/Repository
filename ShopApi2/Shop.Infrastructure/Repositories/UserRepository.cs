using Microsoft.EntityFrameworkCore;
using Shop.Application.Services.Interfaces;
using Shop.Data;
using Shop.DTO.Customer_DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ShopDbContext _context;

        public UserRepository(ShopDbContext context) => _context = context;

        public async Task<IEnumerable<AvailableUserDto>> GetOrderedByEmailExceptIdsAsync(IEnumerable<string> excludeIds)
        {
            var ids = excludeIds.ToList();
            return await _context.Users
                .Where(u => !ids.Contains(u.Id))
                .OrderBy(u => u.Email)
                .Select(u => new AvailableUserDto
                {
                    Id = u.Id,
                    Email = u.Email!,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                })
                .ToListAsync();
        }

    }
}
