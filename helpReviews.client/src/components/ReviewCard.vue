<template>
  <div class="row bg-white rounded elevation-2 mt-4">
    <div class="col-4"><img class="img-fluid" :src="review.creator.picture" alt="">
      <p>{{ review.creator.name }}</p>
    </div>
    <div class="col-8">
      <h4>{{ review.title }}</h4>
      <p>{{ review.body }}</p>
    </div>
    <div class="delete-btn" v-if="account.id == review.creatorId" @click="removeReview" title="delete review forever">
      <button class="btn btn-danger rounded-pill"><i class="mdi mdi-delete-forever"></i></button>
    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { reviewsService } from '../services/ReviewsService.js';
export default {
  props: { review: { type: Object, required: true } },
  setup(props) {
    return {
      account: computed(() => AppState.account),
      async removeReview() {
        try {
          if (await Pop.confirm("Feeling Merciful?", "are you sure you want to delete this review? the people must know!", "Yeah Get rid of it", 'question')) {
            await reviewsService.removeReview(props.review.id)
            Pop.toast('deleted review', 'success')
          }
        } catch (error) {
          logger.error(error)
          Pop.error(error)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped>
.row {
  position: relative;
}

.delete-btn {
  position: absolute;
  text-align: right;
  right: -2em;
  top: -.5em
}

img {
  height: 120px;
  width: 120px;
}
</style>
