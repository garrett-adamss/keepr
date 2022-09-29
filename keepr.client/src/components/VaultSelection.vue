<template>
 <a class="dropdown-item" href="#">{{vault.name}}</a>
</template>
 
<script>
import { computed } from '@vue/reactivity';
import { AppState } from '../AppState'
export default {
   setup(){
      return {
        vault: computed(()=> AppState.vaults)
      }
   }
}
</script>
 
<style> 
</style>