namespace helpReviews.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewsController : ControllerBase
{
  private readonly ReviewsService _reviewsService;
  private readonly Auth0Provider _auth;

  public ReviewsController(ReviewsService reviewsService, Auth0Provider auth)
  {
    _reviewsService = reviewsService;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Review>> CreateReview([FromBody] Review reviewData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      reviewData.CreatorId = userInfo.Id;
      Review review = _reviewsService.CreateReview(reviewData);
      review.Creator = userInfo;
      return Ok(review);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> RemoveReview(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _reviewsService.RemoveReview(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
