<template>
  <div class="row justify-content-center my-3">
    <div class="col-10 bg-white rounded elevation-2 py-3">

      <form @submit.prevent="createReview">
        <div class="mb-3">
          <label for="" class="form-label">Title of Review</label>
          <input v-model="editable.title" type="text" class="form-control" name="" id="" aria-describedby="helpId"
            placeholder="">
        </div>
        <div class="mb-3">
          <label for="">review body</label>
          <textarea v-model="editable.body" class="form-control" name="" id=""></textarea>
        </div>
        <div class="text-end">
          <button class="btn btn-info"><i class="mdi mdi-plus"></i> Create Review</button>
        </div>
      </form>

    </div>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted, ref } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { reviewsService } from '../services/ReviewsService.js';
import { useRoute } from 'vue-router';
export default {
  setup() {
    const editable = ref({});
    const route = useRoute()
    return {
      editable,
      async createReview() {
        try {
          logger.log(editable.value)
          editable.value.restaurantId = route.params.restaurantId
          await reviewsService.createReview(editable.value)
          Pop.toast(editable.value.title + ' created', 'success')
          editable.value = {}
        } catch (error) {
          logger.error(error)
          Pop.error(error)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped></style>
