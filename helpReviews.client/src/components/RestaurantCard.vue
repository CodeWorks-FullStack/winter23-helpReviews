<template>
  <div class="component bg-white rounded elevation-1">
    <router-link :to="{ name: 'RestaurantDetails', params: { restaurantId: restaurant.id } }">
      <img class="img-fluid rounded-top" :src="restaurant.imgUrl" alt="">
      <h4 class="p-2 text-dark text-center">{{ restaurant.name }}</h4>
      <div class="d-flex justify-content-between p-3 fw-bold text-info"><span> {{ restaurant.category }}</span> <span>{{
        restaurant.exposure
      }}</span> <i class="mdi mdi-note"></i>{{ restaurant.reviewCount }}</div>
    </router-link>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
export default {
  props: { restaurant: { type: Object, required: true } },
  setup() {
    return {}
  }
};
</script>


<style lang="scss" scoped></style>
