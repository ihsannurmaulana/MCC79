using API.DTOs.Rooms;
using API.Services;
using API.Utilities.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [ApiController]
    [Route("api/rooms")]
    public class RoomController : ControllerBase
    {
        private readonly RoomService _service;

        public RoomController(RoomService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _service.GetRoom();

            if (entities == null)
            {
                return NotFound(new ResponseHandler<RoomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<RoomDto>>
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
            var room = _service.GetRoom(guid);
            if (room is null)
            {
                return NotFound(new ResponseHandler<RoomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data found",
                Data = room
            });
        }

        [HttpGet("unused")]
        public IActionResult GetUnusedRoom()
        {
            var unusedRooms = _service.GetUnusedRoom();
            if (unusedRooms.Count() == 0)
            {
                return NotFound(new ResponseHandler<UnusedRoomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "all rooms are being used"
                });
            }
            return Ok(new ResponseHandler<IEnumerable<UnusedRoomDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data found",
                Data = unusedRooms
            });
        }
        [HttpPost]
        public IActionResult Create(NewRoomDto newRoomDto)
        {
            var createRoom = _service.CreateRoom(newRoomDto);
            if (createRoom is null)
            {
                return BadRequest(new ResponseHandler<RoomDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Data not created"
                });
            }

            return Ok(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully created",
                Data = createRoom
            });
        }

        [HttpPut]
        public IActionResult Update(RoomDto updateRoomDto)
        {
            var update = _service.UpdateRoom(updateRoomDto);
            if (update is -1)
            {
                return NotFound(new ResponseHandler<RoomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            if (update is 0)
            {
                return BadRequest(new ResponseHandler<RoomDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check your data"
                });
            }
            return Ok(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully updated"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var delete = _service.DeleteRoom(guid);

            if (delete is -1)
            {
                return NotFound(new ResponseHandler<RoomDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            if (delete is 0)
            {
                return BadRequest(new ResponseHandler<RoomDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check connection to database"
                });
            }

            return Ok(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully deleted"
            });
        }
    }
}
