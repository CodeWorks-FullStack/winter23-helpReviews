<template>
  <div class="container-fluid py-2">
    <section>
      <form @submit.prevent="searchRestaurants">
        <input type="text" v-model="search">
        <button><i class="mdi mdi-magnify"></i></button>
      </form>
    </section>
    <section class="bricks">
      <div v-for="r in restaurants">
        <RestaurantCard :restaurant="r" />
      </div>
    </section>
  </div>
</template>

<script>
import { onMounted, ref } from 'vue';
import { restaurantsService } from '../services/RestaurantsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';

export default {
  setup() {
    const search = ref('')
    onMounted(() => {
      getRestaurants()
    })
    async function getRestaurants() {
      try {
        await restaurantsService.getRestaurants()
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }

    }
    async function searchRestaurants() {
      try {
        await restaurantsService.searchRestaurants(search.value)
      } catch (error) {
        logger.error(error)
        Pop.error(error)
      }
    }
    return {
      search,
      restaurants: computed(() => AppState.restaurants),
      searchRestaurants
    }
  }
}
</script>

<style scoped lang="scss">
$gap: .5em;

.bricks {
  columns: 300px;
  column-gap: $gap;

  &>div {
    // &> makes it only target the direct child of bricks, not all the divs inside bricks
    margin-top: $gap;
    display: inline-block;
  }
}
</style>
