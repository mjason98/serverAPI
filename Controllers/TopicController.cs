using System.Linq;
using Microsoft.AspNetCore.Mvc;
using serverAPI.Repositories;
using serverAPI.Dtos;
using serverAPI.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

///// HAY ERROR EN EL CREATE CON EL ID

namespace serverAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TopicController : ControllerBase {
        private IAgendaRepository repository;

        public TopicController (IAgendaRepository _repo){
            this.repository = _repo;
        }

        [HttpGet]
        public async Task<IEnumerable<TopicDto>> GetTopicsAsync(){
            var vs = ( await repository.GetTopicsAsync()).Select(v => v.asDto());
            return vs;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TopicDto>> GetTopicAsync(int id){
            var v = await repository.GetTopicAsync(id);

            if (v is null)
                return NotFound();

            return v.asDto();
        }

        [HttpPost]
        public async Task<ActionResult<TopicDto>> CreateTopicAsync(CreateUpdateTopicDto topic){
            Topic v = new (){
                name = topic.name
            };

            int ide = await repository.CreateTopicAsync(v);
            Topic vIde = v with {
                id = ide
            };
            return CreatedAtAction(nameof(GetTopicAsync), new {id = ide}, vIde.asDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTopicAsync(int id, CreateUpdateTopicDto topicDto){
            var topic = await repository.GetTopicAsync(id);
            if (topic is null) 
                return NotFound();
            topic.name = topicDto.name;
            
            await repository.UpdateTopicAsync(topic);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTopicAsync(int id){
            var existingTopic = await repository.GetTopicAsync(id);
            
            if (existingTopic is null)
                return NotFound();
            
            await repository.DeleteTopicAsync(id);
            return NoContent();
        }
    }  
}