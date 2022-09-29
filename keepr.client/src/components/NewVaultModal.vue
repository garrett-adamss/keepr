<template>
    <div class="modal fade" id="newVaultModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered modal-l">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="row">
                  <h2 class="text-center">Create New Vault</h2>
                  <div class="mb-3">
                     <div class="form-floating m-2">
                        <input type="text" name="title" for="title" id="title" class="form-control" placeholder="Title..." aria-describedby="helpId" style="height: 60px" maxlength="150" required>
                        <label for="title" class="form-label">Title</label>
                     </div>
                     <!-- <div class="form-floating m-2">
                        <input type="text" name="image url" for="image url" id="imgUrl" class="form-control" placeholder="Img Url here" aria-describedby="helpId" style="height: 60px" maxlength="300">
                        <label for="imgUrl" class="form-label">Img Url</label>
                     </div> -->
                     <div class="form-check m-2 ms-3">
                         <input class="form-check-input" type="checkbox" value="" id="private">
                         <label class="form-check-label" for="private" aria-describedby="privateId">
                             Private? 
                            </label>
                       <small id="privateId" class="form-text text-muted">  Private Vaults can only be seen by you</small>
                     </div>
                     <button type="submit" class="btn btn-primary m-2">Create Vault</button>
                  </div>
               </div>
                </div>
            </div>
        </div>
    </div>
</template>
 
<script>
export default {
   setup(){
      return {
 
      }
   }
}
</script>
 
<style> 
</style>