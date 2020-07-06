using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public interface ICRUDController<TItemView, TItemCreate, TItemUpdate> where TItemView : class
                                                                          where TItemCreate : class
                                                                          where TItemUpdate : class
    {
        //GET api/{controller}
        //[HttpGet]
        ActionResult<IEnumerable<TItemView>> GetItems();

        //GET api/{controller}/{id}
        //[HttpGet("{id}", Name = "GetItemById")]
        ActionResult<TItemView> GetItemById(int id);

        //DELETE api/{controller}/{id}
        //[HttpDelete("{id}")]
        ActionResult DeleteItem(int id);

        //POST api/{controller}
        //[HttpPost]
        ActionResult<TItemView> CreateItem(TItemCreate itemCreate);

        //PUT api/{controller}/{id}
        //[HttpPut("{id}")]
        public ActionResult UpdateItem(int id, TItemUpdate itemUpdate);
    }
}
