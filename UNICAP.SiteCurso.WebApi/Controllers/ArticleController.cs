using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Commands.Create;
using UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Commands.Delete;
using UNICAP.SiteCurso.Application.CQRS.ArticleFolder.Commands.Update;
using UNICAP.SiteCurso.Application.CQRS.UserFolder.Commands.Delete;
using UNICAP.SiteCurso.Application.CQRS.UserFolder.Commands.Update;
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
    public class ArticleController(IMediator mediator) : Controller
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Cria um novo artigo.
        /// </summary>
        /// <param name="request">Dados para criação do artigo.</param>
        /// <returns>Resposta da operação de criação.</returns>
        /// <response code="200">Artigo criado com sucesso.</response>
        /// <response code="400">Dados inválidos ou não preenchidos.</response>
        /// <response code="401">Esta funcionalidade requer autenticação.</response>
        /// <response code="500">Ocorreu um erro interno do servidor.</response>
        [HttpPost]
        [Route("CreateArticle")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<Response> Create([FromBody] CreateArticleCommand request)
        {
            return await _mediator.Send(request);
        }

        /// <summary>
        /// Atualiza as informações de um artigo.
        /// </summary>
        /// <param name="request">Dados para atualização do artigo.</param>
        /// <returns>Resposta da operação de atualização.</returns>
        /// <response code="200">Artigo atualizado com sucesso.</response>
        /// <response code="400">Dados inválidos ou não preenchidos.</response>
        /// <response code="401">Esta funcionalidade requer autenticação.</response>
        /// <response code="500">Ocorreu um erro interno do servidor.</response>
        [HttpPut]
        [Route("UpdateArticle")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<Response> Update([FromBody] UpdateArticleCommand request)
        {
            return await _mediator.Send(request);
        }

        /// <summary>
        /// Desativa um artigo.
        /// </summary>
        /// <param name="request">Dados para desativação do artigo.</param>
        /// <returns>Resposta da operação de desativação.</returns>
        /// <response code="200">Artigo inativado com sucesso.</response>
        /// <response code="401">Esta funcionalidade requer autenticação.</response>
        /// <response code="500">Ocorreu um erro interno do servidor.</response>
        [HttpDelete]
        [Route("DeleteArticle")]
        [ProducesResponseType(typeof(Response), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(Response), StatusCodes.Status500InternalServerError)]
        public async Task<Response> Delete([FromQuery] DeleteArticleCommand request)
        {
            return await _mediator.Send(request);
        }
    }
}
