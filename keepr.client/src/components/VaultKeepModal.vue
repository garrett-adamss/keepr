<template>
    <!-- Modal -->
    <div class="modal fade" id="vaultKeepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-6">
                            <img class="img-fluid" :src="keep?.img" alt="" />
                        </div>
                        <div class="col-6">
                            <div class="d-flex justify-content-center">
                                <h5 class="pe-2 ps-2">
                                    <img class="modal-sm-img"
                                        src="https://icons.veryicon.com/png/o/internet--web/truckhome/view-number.png" />
                                    {{ keep?.views }}
                                </h5>
                                <h5 class="pe-2 ps-2">
                                    <img class="modal-sm-img"
                                        src="https://upload.wikimedia.org/wikipedia/commons/thumb/d/dc/Los_K_Morales_Logo_2013.svg/1200px-Los_K_Morales_Logo_2013.svg.png" />
                                    {{ keep?.kept }}
                                </h5>
                            </div>
                            <div class="pt-2 text-center">
                                <h1 class="border-bottom">{{ keep?.name }}</h1>
                            </div>
                            <div class="pt-2">{{ keep?.description }}</div>
                            <div class="d-flex align-items-center justify-content-between bottom-row">
                                <router-link v-if="keep" :to="{ name: 'Profile', params: { id: keep?.creatorId } }">
                                    <img data-bs-dismiss="modal" class="profile-img-modal"
                                        :src="keep?.creator.picture" />
                                </router-link>
                                <div>
                                    <!-- <button class="btn btn-outline-secondary" @click="deleteVaultKeep(vaultKeep)">Remove From Vault</button> -->
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
 
<script>
import { computed, onMounted } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { Modal } from 'bootstrap'
import { vaultsService } from '../services/VaultsService'
import VaultSelection from './VaultSelection.vue'
import { vaultKeepsService } from '../services/VaultKeepsService'
export default {
    setup() {
        return {
            vaultKeep: computed(() => AppState.vaultKeeps),
            account: computed(() => AppState.account),
            keep: computed(() => AppState.activeKeep),
            vaults: computed(() => AppState.vaults),
            async getVaults() {
                try {
                    await vaultsService.getVaultsByProfileId(AppState.user.id);
                }
                catch (error) {
                    logger.error(error);
                    Pop.toast(error.message, "error");
                }
            },
            // async deleteVaultKeep(vaultKeepArray) {     
            //     try {
            //         logger.log("vaultKeepArray",vaultKeepArray)
            //         logger.log("active keep",AppState.activeKeep)
                    // const yes = await Pop.confirm("Are you sure you want to delete that?");
                    // if (!yes) {
                    //     return;
                    // }
            //         // await vaultKeepsService.removeKeep(vaultKeepArray)
            //         // Modal.getOrCreateInstance("#vaultKeepModal").hide();

            //     }
            //     catch (error) {
            //         logger.error(error);
            //         Pop.toast(error.message, "error");
            //     }
            // },
        };
    },
    components: { VaultSelection }
}
</script>
 
<style>
/* using styling from keep modal */
</style>