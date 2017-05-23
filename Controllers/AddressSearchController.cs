using Ninject;
using System.Web.Http;
using System.Web.Http.Cors;
using BizCoverMvc.Infrastructure;
using BizCover.Address.Search.BusinessInterfaces;
using System.Collections.Generic;

namespace BizCover.Address.Search.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AddressSearchController : ApiController
    {
        [Inject]
        protected IBCAddressSearchService _AddressSearchService { get; set; }
        
        [Throttle]
        [HttpGet]
        public IHttpActionResult FilterAddress(string postcode, string locality, string address)
        {
            if (postcode == null || address == null)
                return BadRequest();

            var result = _AddressSearchService.GetFilteredAddressData(postcode, locality, address);
            if (result == null)
                return Json(new List<IBCAddressMatchResult>(0));

            return Json(result);
        }

        [HttpGet]
        public IHttpActionResult IsGnafMatch(string gnafPid, string address)
        {
            if (gnafPid == null)
                return BadRequest();
            
            return Json(_AddressSearchService.IsGnafMatch(gnafPid, address));
        }
    }
}
