using Microsoft.AspNetCore.Mvc;
using sunstealer.mvc.odata.Models;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;

// ajm: /odata
// ajm: /odata/$metadata

namespace sunstealer.mvc.odata.Controllers
{
    public class EdmController : ODataController
    {
        private readonly List<Model1> list = new(
            Enumerable.Range(1, 100).Select(i => new Model1
            {
                Dictionary = new()
                {
                    Dictionary = new Dictionary<string, object>() {
                        { $"{10*(i-1)+1}", 666+i },
                        { $"{10*(i-1)+2}", $"Value{10*(i-1)+1}" },
                        { $"{10*(i-1)+3}", new ListEntry() { Key = 10*(i-1)+1, Value = $"Value{10*(i-1)+1}" } }
                    }
                },
                List = new List<ListEntry> ()
                {
                    new ListEntry() { Key = 10*(i-1)+1, Value = $"Value{10*(i-1)+1}" },
                    new ListEntry() { Key = 10*(i-1)+2, Value = $"Value{10*(i-1)+1}" },
                    new ListEntry() { Key = 10*(i-1)+3, Value = $"Value{10*(i-1)+1}" }
                },
                Key = i,
                Value = $"value{i}"
            })
        );

        [HttpDelete]
        [EnableQuery]
        public ActionResult Delete([FromRoute] int key)
        {
            list.RemoveAt(key);
            return Ok();
        }

        [HttpPost]
        [EnableQuery]
        public ActionResult Post([FromBody] Model1 model)
        {
            try
            {
                this.list.RemoveAt(model.Key);
            } 
            catch (Exception e) { }
            this.list.Add(model);
            return Ok();
        }

        [HttpPut]
        [EnableQuery]
        public ActionResult Put([FromRoute] int key, [FromBody] Model1 model)
        {
            this.list[key].Value = model.Value;
            return Ok();
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<Model1>> Get()
        {
            return Ok(this.list);
        }
    }
} 
