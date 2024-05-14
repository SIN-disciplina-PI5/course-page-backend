using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.CQRS.UserFolder.Commands.Create;
using UNICAP.SiteCurso.Application.CQRS.UserFolder.Commands.Delete;
using UNICAP.SiteCurso.Application.CQRS.UserFolder.Commands.Update;
using UNICAP.SiteCurso.Application.CQRS.UserFolder.Queries.GetAll;
using UNICAP.SiteCurso.Application.CQRS.UserFolder.Queries.GetById;
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

        /// <summary>
        /// Atualiza as informações de um usuário.
        /// </summary>
        /// <param name="request">Dados para atualização do usuário.</param>
        /// <returns>Resposta da operação de atualização.</returns>
        /// <response code="200">Usuário atualizado com sucesso.</response>
        /// <response code="400">Dados inválidos ou não preenchidos.</response>
        /// <response code="401">Esta funcionalidade requer autenticação.</response>
        /// <response code="500">Ocorreu um erro interno do servidor.</response>
        [HttpPut]
        [Route("UpdateUser")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<Response> Update([FromBody] UpdateUserCommand request)
        {
            return await _mediator.Send(request);
        }

        /// <summary>
        /// Desativa um usuário.
        /// </summary>
        /// <param name="request">Dados para desativação do usuário.</param>
        /// <returns>Resposta da operação de desativação.</returns>
        /// <response code="200">Usuário inativado com sucesso.</response>
        /// <response code="401">Esta funcionalidade requer autenticação.</response>
        /// <response code="500">Ocorreu um erro interno do servidor.</response>
        [HttpDelete]
        [Route("DeleteUser")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<Response> Delete([FromQuery] DeleteUserCommand request)
        {
            return await _mediator.Send(request);
        }

        /// <summary>
        /// Obtém uma lista de todos os usuários.
        /// </summary>
        /// <param name="withDisabled">Indica se usuários desativados devem ser incluídos na lista.</param>
        /// <param name="withPagination">Indica se deve existir paginação.</param>
        /// <param name="currentPage">Indica se a página atual.</param>
        /// <param name="itensPerPage">Indica a quantidade de itens por página.</param>
        /// <returns>Resposta contendo a lista de todos os usuários.</returns>
        /// <response code="200">Lista de usuários obtida com sucesso.</response>
        /// <response code="400">Dados inválidos ou não preenchidos.</response>
        /// <response code="401">Esta funcionalidade requer autenticação.</response>
        /// <response code="500">Ocorreu um erro interno do servidor.</response>
        [HttpGet]
        [Route("GetAllUsers")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<Response> GetAll([FromQuery] bool withDisabled, bool withPagination, int currentPage, int itensPerPage)
        {
            return await _mediator.Send(new GetAllUserQuery(withDisabled, withPagination, currentPage, itensPerPage));
        }

        /// <summary>
        /// Obtém um usuário pelo seu ID.
        /// </summary>
        /// <param name="request">ID do usuário a ser obtido.</param>
        /// <returns>Resposta contendo o usuário encontrado.</returns>
        /// <response code="200">Usuário encontrado com sucesso.</response>
        /// <response code="401">Esta funcionalidade requer autenticação.</response>
        /// <response code="500">Ocorreu um erro interno do servidor.</response>
        [HttpGet]
        [Route("GetUserById")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<Response> GetById([FromQuery] GetUserByIdQuery request)
        {
            return await _mediator.Send(request);
        }
    }
}
