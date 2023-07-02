﻿using API.DTOs.Universities;
using API.Services;
using API.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/universities")]
[Authorize(Roles = $"{nameof(RoleLevel.Admin)}")]
public class UniversityController : ControllerBase
{
    private readonly UniversityService _service;

    public UniversityController(UniversityService service)
    {
        _service = service;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult GetAll()
    {
        var entities = _service.GetUniversity();

        if (!entities.Any())
        {
            return NotFound(new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<UniversityDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = entities
        });
    }

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var university = _service.GetUniversity(guid);
        if (university == null)
        {
            return NotFound(new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }
        return Ok(new ResponseHandler<UniversityDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data found",
            Data = university
        });
    }

    [HttpPost]
    public IActionResult Create(NewUniversityDto newUniversityDto)
    {
        var createdUniversity = _service.CreateUniversity(newUniversityDto);
        if (createdUniversity is null)
        {
            return BadRequest(new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Status = HttpStatusCode.BadRequest.ToString(),
                Message = "Data not created"
            });
        }
        return Ok(new ResponseHandler<UniversityDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully created",
            Data = createdUniversity
        });
    }

    [HttpPut]
    public IActionResult Update(UniversityDto updateUniversityDto)
    {
        var update = _service.UpdateUniversity(updateUniversityDto);
        if (update is -1)
        {
            return NotFound(new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Id not found"
            });
        }

        if (update is 0)
        {
            return BadRequest(new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Check your data"
            });
        }

        return Ok(new ResponseHandler<UniversityDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully updated"
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var isDeleted = _service.DeleteUniversity(guid);
        if (isDeleted is -1)
        {
            return NotFound(new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "ID not found"
            });
        }
        if (isDeleted is 0)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieving data from the database"
            });
        }

        return Ok(new ResponseHandler<UniversityDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Successfully deleted"
        });
    }

    [HttpGet("by-name/{name}")]
    public IActionResult GetByName(string name)
    {
        var universities = _service.GetUniversity(name);
        if (!universities.Any())
        {
            return NotFound(new ResponseHandler<UniversityDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "No universities found with the given name"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<UniversityDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Universities found",
            Data = universities
        });
    }

}
