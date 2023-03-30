namespace helpReviews.Repositories;

public class ReviewsRepository
{
  private readonly IDbConnection _db;

  public ReviewsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Review CreateReview(Review reviewData)
  {
    string sql = @"
    INSERT INTO reviews
    (title, body, creatorId, restaurantId)
    VALUES
    (@title, @body, @creatorId, @restaurantId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, reviewData);
    reviewData.Id = id;
    return reviewData;
  }

  internal Review GetOne(int id)
  {
    string sql = @"
    SELECT
    rv.*,
    creator.*
    FROM reviews rv
    JOIN accounts creator ON rv.creatorId = creator.id
    WHERE rv.id = @id;
    ";
    Review review = _db.Query<Review, Profile, Review>(sql, (review, creator) =>
    {
      review.Creator = creator;
      return review;
    }, new { id }).FirstOrDefault();
    return review;
  }

  internal List<Review> GetReviewsByRestaurant(int restaurantId)
  {
    string sql = @"
    SELECT
    rv.*,
    creator.*
    FROM reviews rv
    JOIN accounts creator ON rv.creatorId = creator.id
    WHERE rv.restaurantId = @restaurantId
    ORDER BY rv.createdAt DESC;
    ";
    List<Review> reviews = _db.Query<Review, Profile, Review>(sql, (review, creator) =>
    {
      review.Creator = creator;
      return review;
    }, new { restaurantId }).ToList();
    return reviews;
  }

  internal int RemoveReview(int id)
  {
    string sql = @"
    DELETE FROM reviews
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, new { id });
    return rows;
  }
}
