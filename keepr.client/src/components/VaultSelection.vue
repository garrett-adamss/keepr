<template>
 <li><a class="dropdown-item" href="#" @click="addToVault"><p>{{vault.name}}</p></a></li>
</template>
 
<script>
import { AppState } from '../AppState'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
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