using AutoMapper;
using SecretSanta.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
    }
}
