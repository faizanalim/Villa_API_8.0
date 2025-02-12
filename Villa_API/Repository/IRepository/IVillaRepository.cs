﻿using System.Linq.Expressions;
using Villa_API.Models;
using static Villa_API.Repository.IRepository.IRepository;

namespace Villa_API.Repository.IRepository
{
    public interface IVillaRepository : IRepository<Villa>
    {

        Task<Villa> UpdateAsync(Villa entity);
    }
}
