<template>
    <!-- Modal -->
    <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-xl">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" title="Close" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
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
                                <div v-if="account.id">
                                <div class="dropdown">
                                    <a class="btn btn-outline-secondary dropdown-toggle" title="Add to Vault" name="Add to Vault" @click="getVaults" href="#"
                                        role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                        Add to Vault
                                    </a>
                                    <ul class="dropdown-menu">
                                        <div v-for="v in vaults" :key="v.id">
                                            <vault-selection :vault="v" />
                                        </div>
                                    </ul>
                                </div>
                                </div>
                                <div v-if="account.id == keep?.creatorId">
                                <i class="mdi mdi-delete-outline selectable" title="Delete" name="delete" style="font-size: 50px" @click="deleteKeep(keep.id)"></i>
                                </div>
                                <router-link v-if="keep" :to="{ name: 'Profile', params: { id: keep?.creatorId } }">
                                    <img data-bs-dismiss="modal" class="profile-img-modal" title="Creator" name="Creator"
                                        :src="keep?.creator.picture" />
                                </router-link>            
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
<!-- <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered modal-xl">
        <div class="modal-content">
            <div class="modal-header">
                    <button type="button" title="Close" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="col-6"> 
                    TEST
                    </div>
                    <div class="col-6 column justify-content-center">
                    <div>
                        keeps
                    </div>
                    <div>
                        name
                    </div>
                    <div>
                        description
                    </div>
                    <div>
                        delete row
                    </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div> -->
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
            async deleteKeep(id) {
                try {
                    logger.log("id", id);
                    const yes = await Pop.confirm("Are you sure you want to delete that?");
                    if (!yes) {
                        return;
                    }
                    Modal.getOrCreateInstance("#keepModal").hide();
                    await keepsService.deleteKeep(id);
                    // Modal.getOrCreateInstance(document.getElementById("keepModal")).hide();
                }
                catch (error) {
                    logger.error(error);
                    Pop.toast(error.message, "error");
                }
            },
        };
    },
    components: { VaultSelection }
}
</script>
 
<style>
/* .modal-body{
    height: 40vh;
} */
.modal-img {
    height: 650px;
    width: 600px;
    object-fit: cover;
}

.modal-sm-img {
    height: 28px;
    width: 28px;
    object-fit: cover;
}

.profile-img-modal {
    height: 80px;
    width: 80px;
    border-radius: 50%;
    object-fit: cover;
    filter: drop-shadow(2px 2px 5px black);
}

.dropdown-menu {
    max-height: 400px;
    overflow-y: auto;
}
.bottom-row{
    margin-top: 15vh;
}
</style>