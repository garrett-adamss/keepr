<template>
    <div class="modal fade" id="newVaultModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-l">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body" @submit.prevent="handleSubmit">
                    <form class="row">
                  <h2 class="text-center">Create New Vault</h2>
                  <div class="mb-3">
                     <div class="form-floating m-2">
                        <input v-model="editable.name" type="text" name="title" for="title" id="title" class="form-control" placeholder="Title..." aria-describedby="helpId" style="height: 60px" maxlength="150" required>
                        <label for="title" class="form-label">Title</label>
                     </div>
                     <div class="form-floating m-2">
                        <textarea v-model="editable.description" class="form-control" placeholder="Leave a comment here" id="floatingTextarea2" style="height: 100px" maxlength="300" required></textarea>
                        <label for="floatingTextarea2">Description</label>
                     </div>
                     <div class="form-check m-2 ms-3">
                         <input v-model="editable.private" class="form-check-input" type="checkbox" value="" id="private">
                         <label class="form-check-label" for="private" aria-describedby="privateId">
                             Private? 
                            </label>
                       <small id="privateId" class="form-text text-muted">  Private Vaults can only be seen by you</small>
                     </div>
                     <button type="submit" class="btn btn-primary m-2">Create Vault</button>
                  </div>
                </form>
                </div>
            </div>
        </div>
    </div>
</template>
 
<script>
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import Pop from '../utils/Pop'
import { vaultsService } from '../services/VaultsService'
import { Modal } from 'bootstrap'
import { computed, ref} from '@vue/reactivity'
import { logger } from '../utils/Logger'
export default {
   setup(){
    const route = useRoute()
    const editable = ref({})
      return {
        route,
        editable,
        account: computed(()=> AppState.account),
        profile: computed(()=> AppState.activeProfile),
        async handleSubmit(){
            try {
                //FIXME creates not working
               if(!AppState.account.id){
                Pop.error("You must be signed in to create a vault.")
                throw new Error("You must be signed in to create a vault.");
               }else if(AppState.account.id != route.params.id){
                Pop.error("You can only create keeps on your vault")
                throw new Error("You can only create keeps on your vault");
               }
               editable.value.creatorId = route.params.id
               await vaultsService.createVault(editable.value);
               Modal.getOrCreateInstance("#newVaultModal").hide()
               Pop.success("New vault created")
            }
            catch (error) {
               logger.error(error)
               Pop.toast(error.message, 'error')
            }
        }
 
      }
   }
}
</script>
 
<style> 
</style>