namespace helpReviews.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
  private readonly RestaurantsService _restaurantsService;
  private readonly Auth0Provider _auth;

  public RestaurantsController(RestaurantsService restaurantsService, Auth0Provider auth)
  {
    _restaurantsService = restaurantsService;
    _auth = auth;
  }

  [HttpGet]
  public async Task<ActionResult<List<Restaurant>>> GetRestaurants()
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      List<Restaurant> restaurants = _restaurantsService.GetRestaurants(userInfo?.Id); // will pass null if not logged in, and the users id if they are logged in.
      return Ok(restaurants);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Restaurant>> GetOneRestaurant(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Restaurant restaurant = _restaurantsService.GetOneRestaurant(id, userInfo?.Id);
      return Ok(restaurant);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPut("{id}")]
  [Authorize]
  public async Task<ActionResult<Restaurant>> UpdateRestaurant([FromBody] Restaurant updateData, int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      updateData.OwnerId = userInfo.Id;
      updateData.Id = id;
      Restaurant restaurant = _restaurantsService.UpdateRestaurant(updateData);
      return Ok(restaurant);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
