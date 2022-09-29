<template>
    <!-- Modal -->
    <div class="modal fade" id="keepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
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
                            <div class="pt-2">
                                {{ keep?.description }}
                            </div>
                            <div class="">
                            <div class="dropdown">
                                <a class="btn btn-outline-secondary dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    Add to Vault
                                </a>
                                <ul class="dropdown-menu">
                                    <!-- v-for vaults in vault AppState, vault component will get vaults by user id and 
                                    display an <a> tags for each vault, with an @click that add's that keep to that vault
                                    <li v-for ></li> -->
                                    <li><a class="dropdown-item" href="#">Action</a></li>
                                    <li><a class="dropdown-item" href="#">Another action</a></li>
                                    <li><a class="dropdown-item" href="#">Something else here</a></li>
                                </ul>
                            </div>
                                <router-link v-if="keep" :to="{name: 'Profile', params: {id:keep?.creatorId}}">
                                    <img data-bs-dismiss="modal" class="profile-img-modal" :src="keep?.creator.picture">
                                </router-link>
                                <!-- <div v-if="account.id = keep.creatorId"> -->
                                    <i class="mdi mdi-delete" @click="deleteKeep(keep.id)"></i>
                                <!-- </div> -->
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
 
<script>
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { useRouter } from 'vue-router'
import { Modal } from 'bootstrap'
export default {
    setup() {
        const router = useRouter();
        return {
            keep: computed(() => AppState.activeKeep),

            async deleteKeep(id){
                try {
                    logger.log('id', id)
                    const yes = await Pop.confirm("Are you sure you want to delete that?")
                    if(!yes){
                        return
                    }
                    await keepsService.deleteKeep(id)
                    Modal.getOrCreateInstance("#keepModal").hide()
                }
                catch (error) {
                   logger.error(error)
                   Pop.toast(error.message, 'error')
                }
            },
        }
    }
}
</script>
 
<style>
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
</style>