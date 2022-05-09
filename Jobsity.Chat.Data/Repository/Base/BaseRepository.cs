using Jobsity.Chat.Data.Context;
using Jobsity.Chat.Domain.Interfaces.Repositories.Base;
using Jobsity.Chat.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Jobsity.Chat.Data.Repository.Base
{
    public abstract class BaseRepository<TModel> : IBaseRepository<TModel> where TModel : BaseModel
    {
        protected readonly ChatContext _context;

        protected BaseRepository(ChatContext context)
        {
            _context = context;
        }

        public async Task<TModel> Create(TModel model)
        {
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task Delete(Guid id)
        {
            _context.Remove(await GetByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TModel>> GetAll()
        {
            return await _context.Set<TModel>().ToListAsync();
        }

        public async Task<TModel> GetByIdAsync(Guid id)
        {
            return await _context.Set<TModel>().FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<TModel> Update(TModel model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
