<template>
 <a class="dropdown-item" href="#" @click="addToVault"><p>{{vault}}</p></a>
</template>
 
<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState'
export default {
   setup(){
      return {
        vault: computed(()=> AppState.vaults),
        async addToVault(){
         
        }
      }
   }
}
</script>
 
<style> 
</style>