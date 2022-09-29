<template>
   <div class="modal fade" id="newKeepModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered modal-l">
         <div class="modal-content">
            <div class="modal-header">
               <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body" @submit.prevent="handleSubmit">
               <form class="row">
                  <h2 class="text-center">Create New Keep</h2>
                  <div class="mb-3">
                     <div class="form-floating m-2">
                        <input v-model="editable.name" type="text" name="title" for="title" id="title" class="form-control" placeholder="Title..." aria-describedby="helpId" style="height: 60px" maxlength="150">
                        <label for="title" class="form-label">Title</label>
                     </div>
                     <div class="form-floating m-2">
                        <input v-model="editable.img" type="text" name="image url" for="image url" id="img" class="form-control" placeholder="Img Url here" aria-describedby="helpId" style="height: 60px" maxlength="300">
                        <label for="img" class="form-label">Img Url</label>
                     </div>
                     <div class="form-floating m-2">
                        <textarea v-model="editable.description" class="form-control" placeholder="Leave a comment here" id="floatingTextarea2" style="height: 100px" maxlength="300"></textarea>
                        <label for="floatingTextarea2">Description</label>
                     </div>
                     <button type="submit" class="btn btn-primary m-2">Create Keep</button>
                  </div>
               </form>
            </div>
         </div>
      </div>
   </div>
</template>
 
<script>
import { computed, ref } from '@vue/runtime-core'
import { Modal } from 'bootstrap'
import { useRoute } from 'vue-router'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
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
               // logger.log("[route.params]", route.params) // just id
               // logger.log("[appState account]", AppState.account.id) // obj
               if(!AppState.account.id){
                  Pop.error("You must be signed in to create a match.")
                  throw new Error("You must be signed in to create a keep.");
               }else if(AppState.account.id != route.params.id){
                  Pop.error("You can only create keeps on your profile")
                  throw new Error("You can only create keeps on your profile");
               }
               editable.value.creatorId = route.params.id
               await keepsService.createKeep(editable.value);
               Modal.getOrCreateInstance("#newKeepModal").hide()
               Pop.success("New keep created!")
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
textarea{
   resize: none;
}
</style>