<template>
<!-- Not Private -->
<div v-if="!vault.isPrivate">
<router-link :to="{name: 'Vault', params: {id:vault.id}}">
    <div class="p-2 rounded bg-grey selectable" :title="vault.name" :name="vault.name" @click="setActive()">
          <img class="img-fluid vault-img" src="https://cdn-icons-png.flaticon.com/512/158/158599.png" />
      <div class="d-flex justify-content-around align-items-center pt-2">
          <h4 class="vault-name"> {{ vault.name }}</h4>
        </div>
    </div>
</router-link>
</div>
<!-- Private -->
<div v-else-if="account.id == vault.creatorId">
<router-link :to="{name: 'Vault', params: {id:vault.id}}">
    <div class="p-2 rounded bg-grey selectable" :title="vault.name" :name="vault.name" @click="setActive()">
      <div class="text-center">
          <img class="img-fluid vault-img" src="https://www.freeiconspng.com/thumbs/lock-icon/lock-icon-11.png" />
      </div>
      <div class="d-flex justify-content-around align-items-center pt-2">
          <h4 class="vault-name"> {{ vault.name }}</h4>
        </div>
    </div>
</router-link>
</div>
  </template>
   
  <script>
import { computed } from '@vue/runtime-core'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
  export default {
      props: {
          vault: {
              type: Object,
              required: true
          },
      },
      setup(props) {
          return {
            account: computed(()=> AppState.account),
            async setActive(){
                try {
                    await vaultsService.getOne(props.vault.id)
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
  .vault-name {
      color: whitesmoke;
      text-shadow: 2px 2px 5px black;
  }
  .profile-img { 
      height: 50px;
      width: 50px; 
      border-radius: 50%; 
      object-fit: cover;
      filter: drop-shadow(2px 2px 5px black);
  }
  .vault-img{
    height:270px;
    width: 265px;
    object-fit: cover;
  }
  </style>