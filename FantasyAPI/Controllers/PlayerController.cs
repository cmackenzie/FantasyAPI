using Microsoft.AspNetCore.Mvc;
using FantasyAPI.Services;
using FantasyAPI.Adapters;
using FantasyAPI.Models.Response;
using FantasyAPI.Models.Request;
using FantasyAPI.Validators;


namespace FantasyAPI.Controllers;

[ApiController]
[Route("player")]
public class PlayerController : ControllerBase
{
    private static readonly PlayerToResponse PLAYER_TO_RESPONSE = new PlayerToResponse();
    private static readonly PlayerSearchToSearchCriteria PLAYER_SEARCH_TO_SEARCH_CRITERIA = new PlayerSearchToSearchCriteria();

    private readonly ILogger<PlayerController> _logger;
    private readonly PlayerService _playerService;

    public PlayerController(PlayerService playerService, ILogger<PlayerController> logger)
    {
        _playerService = playerService;
        _logger = logger;
    }


    [HttpPost("search")]
    public PagedResponse<Player> SearchPlayers(PlayerSearch? search, int offset = 0, int take = 20)
    {
        PlayerSearchValidator.Validate(offset, take, search);

        Services.Models.SearchCriteria searchCriteria
            = PLAYER_SEARCH_TO_SEARCH_CRITERIA.Convert(search);
        searchCriteria.Take = take;
        searchCriteria.Offset = offset;

        var response = _playerService.FindPlayersByCriteria(searchCriteria);
        var items = PLAYER_TO_RESPONSE.Convert(response.Items);

        return PagedResponse<Player>.ToPagedResponse(offset, take, response.Count, items);
        
    }

    [HttpGet("{id}")]
    public Player? GetPlayer(int id)
    {
        return PLAYER_TO_RESPONSE.Convert(_playerService.GetPlayerById(id));
    }
}

