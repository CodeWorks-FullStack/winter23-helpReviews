namespace helpReviews.Services;

public class RestaurantsService
{
  private readonly RestaurantsRepository _repo;

  public RestaurantsService(RestaurantsRepository repo)
  {
    _repo = repo;
  }

  internal Restaurant GetOneRestaurant(int id, string userId)
  {
    Restaurant restaurant = _repo.GetOneRestaurant(id);
    if (restaurant == null) throw new Exception($"no restaurant at that id; {id}");
    if (restaurant.OwnerId != userId && restaurant.Shutdown == true) throw new Exception("that my restaurant, I don't know you.");
    { // keeps a user from increasing their own exposure
      if (restaurant.OwnerId != userId)
        restaurant.Exposure++;
      _repo.UpdateRestaurant(restaurant);
    }

    return restaurant;
  }

  internal List<Restaurant> GetRestaurants(string userId)
  {
    List<Restaurant> allRestaurants = _repo.GetRestaurants();
    // include restaurants where you are the owner, OR the restaurant is not shutdown
    List<Restaurant> filteredRestaurants = allRestaurants.FindAll(r => r.OwnerId == userId || r.Shutdown == false);
    return filteredRestaurants;
  }

  internal List<Restaurant> SearchRestaurants(string userId, string search)
  {
    List<Restaurant> allRestaurants = _repo.SearchRestaurants(search);
    // include restaurants where you are the owner, OR the restaurant is not shutdown
    List<Restaurant> filteredRestaurants = allRestaurants.FindAll(r => r.OwnerId == userId || r.Shutdown == false);
    return filteredRestaurants;
  }

  internal Restaurant UpdateRestaurant(Restaurant updateData)
  {
    Restaurant original = this.GetOneRestaurant(updateData.Id, updateData.OwnerId);
    original.Name = updateData.Name == null ? original.Name : updateData.Name; // THIS ONE IS different, but works the same. == null vs != null
    original.Description = updateData.Description != null ? updateData.Description : original.Description;
    original.ImgUrl = updateData.ImgUrl != null ? updateData.ImgUrl : original.ImgUrl;
    original.Category = updateData.Category != null ? updateData.Category : original.Category;
    original.Shutdown = updateData.Shutdown != null ? updateData.Shutdown : original.Shutdown;
    // original.Exposure = updateData.Exposure != null ? updateData.Exposure : original.Exposure;
    _repo.UpdateRestaurant(original);
    return original;
  }
}
