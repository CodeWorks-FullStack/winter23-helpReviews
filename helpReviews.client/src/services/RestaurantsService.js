import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class RestaurantsService {

  async getRestaurants() {
    const res = await api.get('api/restaurants')
    logger.log('[GOT RESTAURANTS]', res.data)
    AppState.restaurants = res.data
  }

  async getActiveRestaurant(restaurantId) {
    const res = await api.get(`api/restaurants/${restaurantId}`)
    logger.log('[GOT ONE RESTAURANT]', res.data)
    AppState.activeRestaurant = res.data
  }

  async updateRestaurant(restaurant) {
    const res = await api.put(`api/restaurants/${restaurant.id}`, restaurant)
    logger.log('[UPDATED RESTAURANT]', res.data)
  }

  async getRestaurantReviews(restaurantId) {
    const res = await api.get(`api/restaurants/${restaurantId}/reviews`)
    logger.log('[RESTAURANT REVIEWS]', res.data)
    AppState.reviews = res.data
  }

  async searchRestaurants(search) {
    const res = await api.get(`api/restaurants/searchExample?search=${search}`)
    logger.log('[GOT RESTAURANTS]', res.data)
    AppState.restaurants = res.data
  }
}

export const restaurantsService = new RestaurantsService()
