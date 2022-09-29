<template>
  <div class="vault">
    <div class="row align-items-center">
      <div class="col-5 ms-5">
        <h1>{{ vault?.name }}</h1>
        <div v-if="vaultKeeps.length != 0">
          <h5>Keeps: {{ vaultKeeps.length }}</h5>
        </div>
      </div>
      <div class="d-flex col-6 justify-content-center">
        <button class="btn btn-lg btn-outline-secondary" @click="deleteVault">
          Delete Vault
        </button>
      </div>
    </div>
    <div v-if="vaultKeeps.length != 0">
      <div class="masonry">
        <div v-for="vk in vaultKeeps" :key="vk.id">
          <VaultKeepCard :vaultKeep="vk" />
        </div>
      </div>
    </div>
    <div v-else>
      <h2 class="text-center m-5">Looks like {{ vault?.name }} is empty...</h2>
    </div>
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
import VaultKeepCard from '../components/VaultKeepCard.vue';
import { router } from '../router';
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
    async function getVaultById() {
      try {
        await vaultsService.getOne(route.params.id)
      }
      catch (error) {
        logger.error(error)
        Pop.toast(error.message, 'error')
      }
    }
    onMounted(() => {
      getVaultKeeps();
      getVaultById();
    });
    return {
      vaultKeeps: computed(() => AppState.vaultKeeps),
      vault: computed(() => AppState.activeVault),
      async deleteVault() {
        try {
          const check = await Pop.confirm("Are you sure you want to delete this Vault?");
          if (!check) {
            return;
          } else {
            await vaultsService.deleteVault(route.params.id)
            Pop.success("Vault Deleted")
            router.push({ name: 'Profile', params: { id: AppState.user.id } })
          }
        }
        catch (error) {
          logger.error(error)
          Pop.toast(error.message, 'error')
        }
      }
    };
  },
  components: { KeepCard, VaultKeepCard }
}
</script>
 
<style scoped lang="scss">
.vault {
  margin-top: 10vh;
  margin-left: 9vh;
  margin-right: 10vh;
}
@media only screen and (min-width: 640px) {
  .masonry {
    columns: 300px;
    column-gap: 1em;
    margin-left: 6vh;
    margin-right: 6vh;
    div {
      display: block;
      margin-bottom: 1em;
    }
  }
}
@media only screen and (max-width: 640px) {
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