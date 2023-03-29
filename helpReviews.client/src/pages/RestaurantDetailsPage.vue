<template>
  <div class="container-fluid" v-if="restaurant">
    <section class="row justify-content-center hero-img" :style="`background-image: url(${restaurant.imgUrl})`">
      <div class="shadow col-12  text-center py-2">
        <img :src="restaurant.owner.picture" class="rounded-circle" alt="">
        <h2>{{ restaurant.name }}</h2>
        <h3>{{ restaurant.category }}</h3>
      </div>
    </section>
    <section class="row justify-content-end" v-if="restaurant.ownerId == account.id">
      <button v-if="!restaurant.shutdown" class="btn btn-danger col-4" title="shutdown restaurant"
        @click="shutdownRestaurant">
        <marquee behavior="alternate" direction="left"><i class="mdi mdi-rodent"></i><i class="mdi mdi-bug"></i><i
            class="mdi mdi-door"></i><i class="mdi mdi-arrow-down"></i><i class="mdi mdi-cancel"></i></marquee>
      </button>
      <button v-else class="btn btn-success col-4" title="open restaurant" @click="shutdownRestaurant">
        <marquee behavior="alternate" direction="right"><i class="mdi mdi-pizza"></i><i
            class="mdi mdi-silverware-spoon"></i><i class="mdi mdi-door-open"></i><i
            class="mdi mdi-arrow-u-up-right"></i><i class="mdi mdi-check"></i></marquee>
      </button>
    </section>
  </div>
</template>


<script>
import { AppState } from '../AppState';
import { computed, reactive, onMounted } from 'vue';
import { logger } from '../utils/Logger.js';
import Pop from '../utils/Pop.js';
import { restaurantsService } from '../services/RestaurantsService.js';
import { useRoute, useRouter } from 'vue-router';
export default {
  setup() {
    const route = useRoute()
    const router = useRouter()
    onMounted(() => {
      getActiveRestaurant()
    })
    async function getActiveRestaurant() {
      try {
        await restaurantsService.getActiveRestaurant(route.params.restaurantId)
      } catch (error) {
        logger.error(error)
        Pop.error(error.message)
        router.push({ name: 'Home' })

      }
    }
    return {
      restaurant: computed(() => AppState.activeRestaurant),
      account: computed(() => AppState.account),
      async shutdownRestaurant() {
        try {
          AppState.activeRestaurant.shutdown = !AppState.activeRestaurant.shutdown
          await restaurantsService.updateRestaurant(AppState.activeRestaurant)
        } catch (error) {
          logger.error(error)
          Pop.error(error.message)
        }
      }
    }
  }
};
</script>


<style lang="scss" scoped>
.hero-img {
  background-size: cover;
  background-position: center;
  color: white;
  // text-shadow: 2px 2px 5px black;
}

.shadow {
  filter: drop-shadow(2px 2px 5px black);
}
</style>
