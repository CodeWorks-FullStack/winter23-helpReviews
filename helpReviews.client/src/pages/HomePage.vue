<template>
  <div class="container-fluid py-2">
    <section class="bricks">
      <div v-for="r in restaurants">
        <RestaurantCard :restaurant="r" />
      </div>
    </section>
  </div>
</template>

<script>
import { onMounted } from 'vue';
import { restaurantsService } from '../services/RestaurantsService.js';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState.js';

export default {
  setup() {
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
    return {
      restaurants: computed(() => AppState.restaurants)
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
