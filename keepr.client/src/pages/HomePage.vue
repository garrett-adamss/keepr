<template>
  <div class="masonry">
    <div class="" v-for="k in keeps" :key="k.id">
      <KeepCard :keep="k"/>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core';
import { keepsService } from "../services/KeepsService"
import { AppState } from '../AppState';
import KeepCard from '../components/KeepCard.vue';
import Pop from '../utils/Pop';
import { profilesService } from '../services/ProfilesService';
import { logger } from '../utils/Logger';

export default {
    name: "Home",
    setup() {
      async function getKeeps(){
        try {
          logger.log('[AppState.vaults]', AppState.vaults)
          await keepsService.getKeeps();
        }
        catch (error) {
           logger.error(error)
        }
      }
      async function resetActiveProfile(){
        try {
           if(AppState.activeProfile != null){
            await profilesService.clearActive();
            logger.log("active profile cleared", AppState.activeProfile)
           }
        }
        catch (error) {
           logger.error(error)
           Pop.toast(error.message, 'error')
        }
      }
        onMounted(() => {
            getKeeps();
            resetActiveProfile();
        });
        return {
            keeps: computed(() => AppState.keeps),
            profile: computed(() => AppState.activeProfile)
        }
    },
    components: { KeepCard }
}
</script>

<style scoped lang="scss">
  @media only screen and (min-width: 640px){
    .masonry {
      columns: 300px;
      column-gap: 1em;
      div {
        display: block;
        margin-bottom: 1em;
      }
    }
  }
  @media only screen and (max-width: 640px){
    .masonry {
      columns: 199px;
      column-gap: 1em;
      div {
        display: block;
        margin-bottom: 1em;
      }
    }
  }
</style>
