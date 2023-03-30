namespace helpReviews.Services;

public class ReviewsService
{
  private readonly ReviewsRepository _repo;
  private readonly RestaurantsService _restaurantsService;

  public ReviewsService(ReviewsRepository repo, RestaurantsService restaurantsService)
  {
    _repo = repo;
    _restaurantsService = restaurantsService;
  }

  internal Review CreateReview(Review reviewData)
  {
    Review review = _repo.CreateReview(reviewData);
    return review;
  }

  internal Review GetOne(int id)
  {
    Review review = _repo.GetOne(id);
    if (review == null) throw new Exception($"Review at id: {id} does not exist.");
    return review;
  }

  internal List<Review> GetReviewsByRestaurant(int restaurantId, string userId)
  {
    Restaurant restaurant = _restaurantsService.GetOneRestaurant(restaurantId, userId); // handles logic of if the restaurant exists, or is shut down and im not the owner 
    List<Review> reviews = _repo.GetReviewsByRestaurant(restaurantId);
    return reviews;
  }

  internal string RemoveReview(int id, string userId)
  {
    Review review = this.GetOne(id);
    if (review.CreatorId != userId) throw new Exception("You don't own that review, get out!");
    _repo.RemoveReview(id);
    return $"the review, {review.Title} was deleted.";
  }
}
