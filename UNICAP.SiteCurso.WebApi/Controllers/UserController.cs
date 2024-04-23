using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.CQRS.UserFolder.Commands.Create;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.WebApi.Controllers
{
    /// <summary>
    /// Controlador de usuários.
    /// Gerencia as operações relacionadas aos usuários do sistema.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    //[Authorize]
    [Route("api/[controller]")]
    public class UserController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Cria um novo usuário.
        /// </summary>
        /// <param name="request">Dados para criação do usuário.</param>
        /// <returns>Resposta da operação de criação.</returns>
        /// <response code="200">Usuário criado com sucesso.</response>
        /// <response code="400">Dados inválidos ou não preenchidos.</response>
        /// <response code="401">Esta funcionalidade requer autenticação.</response>
        /// <response code="500">Ocorreu um erro interno do servidor.</response>
        [HttpPost]
        [Route("CreateUser")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<Response> Create([FromBody] CreateUserCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}
