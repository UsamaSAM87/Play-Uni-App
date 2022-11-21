using BDX.WebApiLibrary.Data.Repository;
using BDX.WebApiLibrary.DTO;
using Microsoft.AspNetCore.Mvc;
using Play.App.Service.Models;

namespace BDX.WebApiService.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class DTOaController : ControllerBase {
    private readonly IWebApiDtoRepository _repository;

    public DTOaController(IWebApiDtoRepository repository) {
        _repository = repository;
    }

	[HttpPost(Name = "CreateRole")]
	public ActionResult<DTOa> CreateRole(CreaDTOa creaResult) {
		try {
			return Ok(_repository.Create(creaResult));
		} catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpGet(Name = "ReadRole")]
	public ActionResult<List<DTOa>> ReadRole([FromQuery] ReadDTOa ReadResult) {
		try {
			return Ok(_repository.Read(ReadResult));
		} catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpPut(Name = "UpdateRole")]
	public ActionResult<DTOa> UpdateRole(ActionDTOa ActionResult) {
		try {
			return Ok(_repository.Update(ActionResult));
		} catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}

	[HttpDelete(Name = "DeleteRole")]
	public ActionResult DeleteRole(EliminateDTOa eliminateRead) {
		try {
			_repository.Delete(eliminateRead);
			return Ok();
		} catch (Exception ex) {
			return BadRequest(ex.Message);
		}
	}
}

