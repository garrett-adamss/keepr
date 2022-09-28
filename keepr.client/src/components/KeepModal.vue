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
                            <div class="dropdown">
                                <a class="btn btn-outline-secondary dropdown-toggle" href="#" role="button" data-bs-toggle="dropdown" aria-expanded="false">
                                    Add to Vault
                                </a>
                                <ul class="dropdown-menu">
                                    <li><a class="dropdown-item" href="#">Action</a></li>
                                    <li><a class="dropdown-item" href="#">Another action</a></li>
                                    <li><a class="dropdown-item" href="#">Something else here</a></li>
                                </ul>
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
export default {
    setup() {
        return {
            keep: computed(() => AppState.activeKeep),
            profile: computed(() => AppState.account)
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
</style>