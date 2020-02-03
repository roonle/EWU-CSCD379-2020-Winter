using AutoMapper;
using SecretSanta.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SecretSanta.Business
{
    public class GiftService : EntityService<Gift>, IGiftService
    {
        public GiftService(ApplicationDbContext applicationDbContext, IMapper mapper)
            : base(applicationDbContext, mapper)
        {
        }

        public override async Task<Gift> FetchByIdAsync(int id) =>
            await ApplicationDbContext.Set<Gift>().Include(nameof(Gift.User)).SingleAsync(item => item.Id == id);

        public override async Task<List<Gift>> FetchAllAsync()
        {
            return await ApplicationDbContext.Gifts.Include(gift => gift.User).ToListAsync();
        }
    }


}
