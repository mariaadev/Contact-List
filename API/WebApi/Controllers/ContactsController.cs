using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Models;
using WebApi.Models.Domain;
namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly WebApiDbContext dbContext;

        public ContactsController(WebApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult GetAllContacts()
        {
            //accedemos a la propiedad DbSet que tenemos en el context que representa la tabla de la base de datos
            var contacts = dbContext.Contacts.ToList();
            //convertimos de DbSetCollection a una Lista
            //como es una restful api, enviamos una respuesta ok y los contactos
            return Ok(contacts);
        }   

         [HttpPost]
         public IActionResult AddContact(AddContactRequestDTO request)
         {
            //debemos mapear el dto al Modello Contact(Modelo de Dominio) ya que el contexto está formado por una colección de Contact y si queremos modificar la bbdd debemos hacerlo con contact
            //la abstracción del Dto solo la conocera el cliente aka Angular
            var domainModelContact = new Contact 
            {
                //creamos el id que irá a la base de datos.
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Favorite = request.Favorite
            };
            //el paso de mappear es necesario ya que solo podemos guardar entidades Contact en la base de datos.
            dbContext.Contacts.Add(domainModelContact);
            dbContext.SaveChanges();
            return Ok(domainModelContact);
         }
    }
}
