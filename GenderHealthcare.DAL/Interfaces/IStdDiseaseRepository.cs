using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GenderHealthcare.DAL.Entities;

namespace GenderHealthcare.DAL.Interfaces
{
    public interface IStdDiseaseRepository
    {
        Task<List<StdDisease>> GetAllAsync();
        Task<StdDisease> GetByIdAsync(string id);
        Task AddAsync(StdDisease entity);
        Task UpdateAsync(StdDisease entity);
    }
}