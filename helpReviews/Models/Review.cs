namespace helpReviews.Models;

public class Review
{
  public int Id { get; set; }
  public string Title { get; set; }
  public string Body { get; set; }
  public string CreatorId { get; set; }
  public int RestaurantId { get; set; }
  public Profile Creator { get; set; }
}
