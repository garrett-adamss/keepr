<template>
   <div class="vault">
      <div class="row align-items-center">
         <div class="col-5 ms-5">
            <h1>{{vault.name}}</h1>
            <h5>Keeps: vault</h5>
         </div>
         <div class="d-flex col-6 justify-content-center">
            <button class="btn btn-lg btn-outline-secondary">Delete Vault</button>
         </div>
      </div>
      <!-- v-for each keep in vault, (keep in a vault keep),
         display keep card-->
      <div v-for="vk in vaultKeeps" :key="vk.id">
         <KeepCard :vaultKeep="vk"/> 
      </div>
      <!-- TODO v-if change the modal  -->
   </div>
</template>
 
<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState'
import { onMounted } from '@vue/runtime-core';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import { vaultsService } from '../services/VaultsService';
import { useRoute } from 'vue-router';
import KeepCard from '../components/KeepCard.vue';
export default {
    setup() {
        const route = useRoute();
        async function getVaultKeeps() {
            try {
                await vaultsService.getVaultKeeps(route.params.id);
            }
            catch (error) {
                logger.error(error);
                Pop.toast(error.message, "error");
            }
        }
        onMounted(() => {
            getVaultKeeps();
        });
        return {
            vaultKeeps: computed(() => AppState.vaultKeeps),
            vault: computed(() => AppState.activeVault),
        };
    },
    components: { KeepCard }
}
</script>
 
<style> 
  .vault{
    margin-top: 10vh;
    margin-left: 9vh;
    margin-right:10vh;
  }
</style>