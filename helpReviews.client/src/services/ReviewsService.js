import { AppState } from "../AppState.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"


class ReviewsService {

  async createReview(reviewData) {
    const res = await api.post('api/reviews', reviewData)
    logger.log('[CREATED REVIEW]', res.data)
    AppState.reviews.push(res.data)
  }

  async removeReview(reviewId) {
    const res = await api.delete(`api/reviews/${reviewId}`)
    logger.log('[DELETED REVIEW]', res.data)
    let deleteIndex = AppState.reviews.findIndex(r => r.id == reviewId)
    AppState.reviews.splice(deleteIndex, 1)
  }
}

export const reviewsService = new ReviewsService()
