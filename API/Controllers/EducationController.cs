using API.Contracts;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiController]
[Route("api/education")]
public class EducationController : ControllerBase
{
    private readonly IEducationRepository _repository;

    public EducationController(IEducationRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var educations = _repository.GetAll();

        if (!educations.Any())
        {
            return NotFound();
        }

        return Ok(educations);
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var education = _repository.GetByGuid(guid);
        if (education == null)
        {
            return NotFound();
        }
        return Ok(education);
    }

    [HttpPost]
    public IActionResult Create(Education education)
    {
        var createEducation = _repository.Create(education);
        return Ok(createEducation);
    }

    [HttpPut]
    public IActionResult Update(Education education)
    {
        var isUpdate = _repository.Update(education);
        if (isUpdate)
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _repository.Delete(guid);
        if (!isDeleted)
        {
            return NotFound();
        }
        return Ok();
    }
}

