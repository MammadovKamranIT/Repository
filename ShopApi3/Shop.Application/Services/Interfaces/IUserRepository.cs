using Shop.DTO.Customer_DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Application.Services.Interfaces
{
    public interface IUserRepository
    {

        Task<IEnumerable<AvailableUserDto>> GetOrderedByEmailExceptIdsAsync(IEnumerable<string> excludeIds);

    }

}

