using SecretSanta.Business;
using SecretSanta.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Api.Controllers
{
    //https://localhost/api/Group
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private IGroupService GroupService { get;  }

        public GroupController(IGroupService groupService)
        {
            GroupService = groupService ?? throw new System.ArgumentNullException(nameof(groupService));
        }

        // GET: https://localhost/api/Group
        [HttpGet]
        public async Task<IEnumerable<Group>> Get()
        {
            List<Group> Groups = await GroupService.FetchAllAsync();
            return Groups;
        }

        // GET: api/Group/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Group>> Get(int id)
        {
            if (await GroupService.FetchByIdAsync(id) is Group Group)
            {
                return Ok(Group);
            }
            return NotFound();
        }

        // POST: api/Group
        [HttpPost]
        public async Task<Group> Post(Group value)
        {
            return await GroupService.InsertAsync(value);
        }

        // PUT: api/Group/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Group>> Put(int id, Group value)
        {
            if (await GroupService.UpdateAsync(id, value) is Group Group)
            {
                return Ok(Group);
            }
            return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Group>> Delete(int id)
        {
            if (await GroupService.DeleteAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}