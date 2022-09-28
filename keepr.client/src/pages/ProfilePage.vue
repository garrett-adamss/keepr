<template>
  <div class="profile">
 <div class="row">
    <!-- profile information -->
    <img class="col-6 profile-page-img" :src="profile?.picture" alt="Profile Picture">
    <div class="col-6">
      <h1>{{profile?.name}}</h1>
      <h3>Vaults: {{}}</h3>
      <h3>Keeps: {{}}</h3>
    </div>
 </div>
 <div class="row">
    <h1>Vaults <i class="mdi selectable mdi-plus" @click="createVault()"></i></h1>
        <!-- v-for profile vaults -->
    <div class="" v-for="v in vaults" :key="v.id">
    
    </div>
 </div>
 <div class="row">
    <h1>Keeps <i class="mdi selectable mdi-plus" @click="createKeep()"></i></h1>
    <div class="masonry">
        <!-- v-for profile keeps -->
      <div class="" v-for="k in keeps" :key="k.id">
        <KeepCard :keep="k"/>
      </div>
    </div>
 </div>
</div>
</template>
 
<script>
import { computed, onMounted } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { profilesService } from '../services/ProfilesService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
export default {
   setup(){
    const route = useRoute();
    async function getActiveProfile(){
      try {
          await profilesService.getProfileById(route.params.id)        
      }
      catch (error) {
         logger.error(error)
         Pop.toast(error.message, 'error')
      }
    }
    async function getKeeps(){
      try {
         await keepsService.getKeepsByProfileId(route.params.id)
      }
      catch (error) {
         logger.error(error)
         Pop.toast(error.message, 'error')
      }
    }
    async function getVaults(){
      try {
         await vaultsService.getVaultsByProfileId(route.params.id)
      }
      catch (error) {
         logger.error(error)
         Pop.toast(error.message, 'error')
      }
    }
    onMounted(()=> {
      getActiveProfile();
      getKeeps();
      getVaults
    })
      return {
        profile: computed(() => AppState.activeProfile),
        keeps: computed(() => AppState.keeps),
        vaults: computed(() => AppState.vaults)
      }
   }
}
</script>
 
<style scoped lang="scss"> 
  .profile-page-img{
    height: 200px;
    width: 220px;
    object-fit: cover;
  }
  .profile{
    margin-top: 10vh;
    margin-left: 9vh;
  }

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