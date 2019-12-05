using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/names")]
    public class NamesController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(new[] {
                new { id = 1, name = "Ulysses Briggs", age = 18, email = "Suspendisse@bibendum.edu" },
                new { id = 2, name = "Boris Contreras", age = 18, email = "vitae.orci.Phasellus@euplacerateget.edu" },
                new { id = 3, name = "Ahmed Snider", age = 27, email = "egestas@metus.com" },
                new { id = 4, name = "Dustin Ortiz", age = 22, email = "ante.Nunc@Namacnulla.edu" },
                new { id = 5, name = "Moana Walls", age = 25, email = "Aenean.eget.magna@felisegetvarius.org" },
                new { id = 6, name = "Isaac Bryan", age = 25, email = "neque.vitae@feugiatLorem.org" },
                new { id = 7, name = "Preston Padilla", age = 22, email = "parturient.montes.nascetur@sem.co.uk" },
                new { id = 8, name = "Phelan Copeland", age = 25, email = "a.facilisis@facilisi.com" },
                new { id = 9, name = "Micah Oliver", age = 28, email = "Donec.sollicitudin.adipiscing@elitpellentesque.edu" },
                new { id = 10, name = "Robin Mccray", age = 25, email = "porttitor.vulputate.posuere@justoProinnon.edu" },
                new { id = 11, name = "Dean Parks", age = 18, email = "scelerisque.neque@eleifendCrassed.co.uk" },
                new { id = 12, name = "Willow Kinney", age = 25, email = "non.ante@magna.com" },
                new { id = 13, name = "Malachi Waller", age = 21, email = "eu@sollicitudin.net" },
                new { id = 14, name = "Zahir Reese", age = 21, email = "enim.consequat@nunc.co.uk" },
                new { id = 15, name = "Keegan Salas", age = 29, email = "varius.Nam@parturientmontesnascetur.ca" },
                new { id = 16, name = "Justine Roach", age = 18, email = "magna.Lorem@erosnec.co.uk" },
                new { id = 17, name = "Philip Vaughan", age = 30, email = "Suspendisse.sagittis@et.net" },
                new { id = 18, name = "Eric Gardner", age = 27, email = "mattis@temporbibendum.edu" },
                new { id = 19, name = "Price Byers", age = 18, email = "ridiculus.mus.Proin@semPellentesqueut.edu" },
                new { id = 20, name = "Drew Simpson", age = 21, email = "felis.eget@malesuadaIntegerid.ca" }
            });
        }
    }
}
