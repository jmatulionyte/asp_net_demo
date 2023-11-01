using Azure;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using pingPongAPI.Models;
using pingPongAPI.Repository.IRepository;
using AutoMapper;
using pingPongAPI.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace pingPongAPI.Controllers;

[ApiController]
[Route("api/Player")]
public class PlayerAPIController : ControllerBase
{

    protected APIResponse _response;
    private readonly IPlayerRepository _dbPlayer;
    private readonly IMapper _mapper;

    //dependency injection
    public PlayerAPIController(IPlayerRepository dbPlayer, IMapper mapper)
    {
        _dbPlayer = dbPlayer;
        _mapper = mapper;
        this._response = new();
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    //[ResponseCache(CacheProfileName = "Default30")] //for 30s data is not fetched from DB, caontroller action is not invoked
    public async Task<ActionResult<APIResponse>> GetPlayers()
    {
        try
        {
            IEnumerable<Player> playerList;
            playerList = await _dbPlayer.GetAllAsync();

            //maps from Villa to VillaDTO
            _response.Result = _mapper.Map<List<PlayerDTO>>(playerList);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                 = new List<string>() { ex.ToString() };
        }
        return _response;
    }

    [HttpGet("{id:int}", Name = "GetPlayer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ResponseCache(CacheProfileName = "Default30")] //for 30s data is not fetched from DB, caontroller action is not invoked
    public async Task<ActionResult<APIResponse>> GetPlayer(int id)
    {
        try
        {
            if (id == 0)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Id is 0");
                return BadRequest(_response);
            }
            var player = await _dbPlayer.GetAsync(u => u.Id == id);
            if (player == null)
            {
                _response.StatusCode = HttpStatusCode.NotFound;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Player with this id is not existant");
                return NotFound(_response);
            }
            _response.Result = _mapper.Map<PlayerDTO>(player);
            _response.StatusCode = HttpStatusCode.OK;
            return Ok(_response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                 = new List<string>() { ex.ToString() };
        }
        return _response;
    }

    [HttpPost]
    //[Authorize(Roles = "admin")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<APIResponse>> CreatePlayer([FromBody] PlayerCreateDTO craeteDTO)
    {
        try
        {
            //tries to fetch records with players first and last name
            if (await _dbPlayer.GetAsync
                (u => u.FirstName.ToLower() == craeteDTO.FirstName.ToLower()
                && u.LastName.ToLower() == craeteDTO.LastName.ToLower()) != null)
            {
                ModelState.AddModelError("ErrorMessages", "Player already exist");
                return BadRequest(ModelState);
            }
            if (craeteDTO == null)
            {
                ModelState.AddModelError("ErrorMessages", "No data entered");
                return BadRequest(craeteDTO);
            }

            //map values to original model from dto
            Player player = _mapper.Map<Player>(craeteDTO);

            await _dbPlayer.CreateAsync(player);

            _response.Result = _mapper.Map<PlayerDTO>(player);
            _response.StatusCode = HttpStatusCode.Created;
            //show route where can fetch resource
            return CreatedAtRoute("GetPlayer", new { id = player.Id }, _response);
        }
        catch (Exception ex)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages
                    = new List<string>() { ex.ToString()
                };
        }
        return _response;
    }

}

