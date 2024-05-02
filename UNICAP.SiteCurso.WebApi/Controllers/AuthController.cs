using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.CQRS.AuthFolder.Commands.SignIn;
using UNICAP.SiteCurso.Application.DTOs.GenericsFolder;

namespace UNICAP.SiteCurso.WebApi.Controllers
{
    /// <summary>
    /// Controlador de autenticação.
    /// Gerencia as operações relacionadas à autenticação de usuários.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class AuthController(IMediator mediator, IHttpContextAccessor httpAccessor) : Controller
    {
        private readonly IMediator _mediator = mediator;
        private readonly IHttpContextAccessor _httpAccessor = httpAccessor;

        /// <summary>
        /// Realiza o login de um usuário.
        /// </summary>
        /// <param name="request">Dados de login do usuário.</param>
        /// <returns>Resposta da operação de login.</returns>
        /// <response code="200">Login bem-sucedido.</response>
        /// <response code="400">Dados inválidos ou não preenchidos.</response>
        /// <response code="401">Credenciais de autenticação inválidas.</response>
        /// <response code="500">Ocorreu um erro interno do servidor.</response>
        [HttpPost]
        [Route("LoginNovo")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public Task<Response> Login([FromBody] SignInCommand request)
        {
            return _mediator.Send(request);
        }
    }
}
