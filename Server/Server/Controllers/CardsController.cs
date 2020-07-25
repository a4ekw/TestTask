using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server.Helpers;

namespace Server.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        #region Serialization

        private static List<Card> DeserializeFromJson(JsonHelper helper)
        {
            List<Card> list = JsonConvert.DeserializeObject<List<Card>>(helper.Read("cards.json", "Data"));
            return list;
        }

        private static void SerializeToJson(List<Card> num, JsonHelper helper)
        {
            string jsonString = JsonConvert.SerializeObject(num);
            helper.Write("cards.json", "Data", jsonString);
        }

        #endregion

        [HttpGet]
        public IEnumerable<Card> Get()
        {
            List<Card> list;
            JsonHelper helper = new JsonHelper();
            list = JsonConvert.DeserializeObject<List<Card>>(helper.Read("cards.json", "Data"));
            return list;
        }

        [HttpPost]
        public IActionResult Post(Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            List<Card> list;
            JsonHelper helper = new JsonHelper();
            list = DeserializeFromJson(helper);

            list.Add(card);

            SerializeToJson(list, helper);

            return Ok();
        }

        [HttpPut]
        public IActionResult Put(Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<Card> list;
            JsonHelper helper = new JsonHelper();
            list = JsonConvert.DeserializeObject<List<Card>>(helper.Read("cards.json", "Data"));
            for (int i = 0; i < list.Count; i++)
            {
                if (list.ElementAt(i).Id == card.Id)
                {
                    list.ElementAt(i).Name = card.Name;
                    list.ElementAt(i).Map = card.Map;
                    SerializeToJson(list, helper);
                    break;
                }
                if (i == list.Count - 1)
                    return BadRequest("Element not found");
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            List<Card> list;
            JsonHelper helper = new JsonHelper();
            list = DeserializeFromJson(helper);
            try
            {
                list.RemoveAt(list.FindIndex(x => x.Id == id));
            }
            catch
            {
                return BadRequest();
            }
            SerializeToJson(list, helper);
            return Ok();
        }
    }
}
