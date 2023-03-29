namespace helpReviews.Repositories;

public class RestaurantsRepository
{
  private readonly IDbConnection _db;

  public RestaurantsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Restaurant GetOneRestaurant(int id)
  {
    // NOTE easy way to increase exposure when getting something, but not the only solution
    // string sql = @"
    // UPDATE restaurants
    // SET
    // exposure = exposure + 1
    // WHERE id = @id;

    // SELECT
    // rest.*,
    // owner.*
    // FROM restaurants rest
    // JOIN accounts owner ON rest.ownerId = owner.id
    // WHERE rest.id = @id;
    // ";
    string sql = @"
    SELECT
    rest.*,
    owner.*
    FROM restaurants rest
    JOIN accounts owner ON rest.ownerId = owner.id
    WHERE rest.id = @id;
    ";
    Restaurant restaurant = _db.Query<Restaurant, Profile, Restaurant>(sql, (rest, owner) =>
    {
      rest.Owner = owner;
      return rest;
    }, new { id }).FirstOrDefault();
    return restaurant;
  }

  internal List<Restaurant> GetRestaurants()
  {
    string sql = @"
    SELECT
    rest.*,
    owner.*
    FROM restaurants rest
    JOIN accounts owner ON rest.ownerId = owner.id;
    ";
    List<Restaurant> restaurants = _db.Query<Restaurant, Profile, Restaurant>(sql, (rest, owner) =>
    {
      rest.Owner = owner;
      return rest;
    }).ToList();
    return restaurants;
  }

  internal int UpdateRestaurant(Restaurant updateData)
  {
    string sql = @"
    UPDATE restaurants
    SET
    name = @name,
    description = @description,
    imgUrl = @imgUrl,
    category = @category,
    shutdown = @shutdown,
    exposure = @exposure
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, updateData);
    return rows;
  }
}
