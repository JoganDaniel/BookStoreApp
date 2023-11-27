using BookStoreBussiness.IBussiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace BookStoreApplication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        public readonly ICustomerDetailsBusiness customerBusiness;

        public CustomerController(ICustomerDetailsBusiness customerBusiness)
        {
            this.customerBusiness = customerBusiness;
        }

    }
}
