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
    COUNT(rv.id) AS reviewCount,
    owner.*
    FROM restaurants rest
    LEFT JOIN reviews rv ON rv.restaurantId = rest.id
    JOIN accounts owner ON rest.ownerId = owner.id
    WHERE rest.id = @id
    GROUP BY rest.id;
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
    COUNT(rv.id) AS reviewCount,
    owner.*
    FROM restaurants rest
    LEFT JOIN reviews rv ON rv.restaurantId = rest.id
    JOIN accounts owner ON rest.ownerId = owner.id
    GROUP BY rest.id;
    ";
    List<Restaurant> restaurants = _db.Query<Restaurant, Profile, Restaurant>(sql, (rest, owner) =>
    {
      rest.Owner = owner;
      return rest;
    }).ToList();
    return restaurants;
  }

  internal List<Restaurant> SearchRestaurants(string search)
  {
    search = $"%{search}%";
    string sql = @"
    SELECT
    rest.*,
    COUNT(rv.id) AS reviewCount,
    owner.*
    FROM restaurants rest
    LEFT JOIN reviews rv ON rv.restaurantId = rest.id
    JOIN accounts owner ON rest.ownerId = owner.id
    WHERE rest.name LIKE @search OR rest.category LIKE @search OR rest.description LIKE @search
    GROUP BY rest.id;
    ";
    List<Restaurant> restaurants = _db.Query<Restaurant, Profile, Restaurant>(sql, (rest, owner) =>
    {
      rest.Owner = owner;
      return rest;
    }, new { search }).ToList();
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
