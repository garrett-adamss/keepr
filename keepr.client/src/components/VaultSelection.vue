<template>
 <li><a class="dropdown-item" href="#" @click="addToVault"><p>{{vault.name}}</p></a></li>
</template>
 
<script>
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import { vaultKeepsService } from '../services/VaultKeepsService';
import Pop from '../utils/Pop'
import { router } from '../router';
export default {
   props:{
      vault:{
         type: Object,
         required: true
      }
   },
   setup(props){
      return {
        async addToVault(){
         try {
            logger.log("Vault Id", props.vault.id)
            logger.log("active Keep", AppState.activeKeep.id)
            await vaultKeepsService.createVaultKeep(props.vault.id, AppState.activeKeep.id)
            Pop.success("Added to vault")
            //TODO router.push({ name: 'Vault', params: { id: props.vault.id}})
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