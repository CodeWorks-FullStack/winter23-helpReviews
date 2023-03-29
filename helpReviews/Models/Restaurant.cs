namespace helpReviews.Models;

public class Restaurant
{
  public int Id { get; set; }
  public string OwnerId { get; set; }
  public string Name { get; set; }
  public string Description { get; set; }
  public string Category { get; set; }
  public string ImgUrl { get; set; }
  public int? Exposure { get; set; }
  public int ReviewCount { get; set; }
  public bool? Shutdown { get; set; }

  public Profile Owner { get; set; }
}
