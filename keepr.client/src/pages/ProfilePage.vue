<template>
  <div class="profile">
 <div class="row">
    <!-- profile information -->
    <img class="col-6 profile-page-img" :src="profile?.picture" alt="Profile Picture">
    <div class="col-6">
      <h1>{{profile?.name}}</h1>
      <!-- <h1>{{profile.id}}</h1>
      <h1>{{account.id}}</h1> -->
      <h3>Vaults: {{vaults.length}}</h3>
      <h3>Keeps: {{keeps.length}}</h3>
    </div>
 </div>
 <div class="row mt-4">
   <div v-if="account.id == profile?.id">
    <h1>Vaults <i class="mdi selectable mdi-plus" data-bs-toggle="modal" data-bs-target="#newVaultModal" type="button" for="newVaultModal"></i></h1>
   </div>
   <div v-else>
    <h1>Vaults</h1>
   </div>
  <div class="masonry">
    <!-- v-for profile vaults -->
    <div class="" v-for="v in vaults" :key="v.id">
    <VaultCard :vault="v"/>
    </div>
  </div>
 </div>
 <div class="row mt-4">
    <div v-if="account.id == profile?.id">
      <h1>Keeps <i class="mdi selectable mdi-plus" data-bs-toggle="modal" data-bs-target="#newKeepModal" type="button" for="newKeepModal"></i></h1>
    </div>
    <div v-else>
      <h1>Keeps</h1>
    </div>
    <div class="masonry">
        <!-- v-for profile keeps -->
      <div class="" v-for="k in keeps" :key="k.id">
        <KeepCard :keep="k"/>
      </div>
    </div>
 </div>
</div>
<NewKeepModal/>
<NewVaultModal/>
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
import NewKeepModal from '../components/NewKeepModal.vue'
import NewVaultModal from '../components/NewVaultModal.vue'
import VaultCard from '../components/VaultCard.vue'
export default {
    setup() {
        const route = useRoute();
        async function getActiveProfile() {
            try {
                logger.log("[account]", AppState.account)
                await profilesService.getProfileById(route.params.id);
            }
            catch (error) {
                logger.error(error);
                Pop.toast(error.message, "error");
            }
        }
        async function getKeeps() {
            try {
                await keepsService.getKeepsByProfileId(route.params.id);
            }
            catch (error) {
                logger.error(error);
                Pop.toast(error.message, "error");
            }
        }
        async function getVaults() {
            try {
                await vaultsService.getVaultsByProfileId(route.params.id);
            }
            catch (error) {
                logger.error(error);
                Pop.toast(error.message, "error");
            }
        }
        onMounted(() => {
            getActiveProfile();
            getKeeps();
            getVaults();
        });
        return {
            account: computed(() => AppState.account),
            profile: computed(() => AppState.activeProfile),
            keeps: computed(() => AppState.keeps),
            vaults: computed(() => AppState.vaults),
        };
    },
    components: { NewKeepModal, NewVaultModal, VaultCard }
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
    margin-right:10vh;
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