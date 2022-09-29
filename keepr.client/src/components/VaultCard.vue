<template>
    <!-- style="background-image: url({{keep.img}}) -->
<router-link :to="{name: 'Vault', params: {id:vault.id}}">
    <!--  -->
    <div class="p-2 rounded bg-grey selectable" @click="setActive()">
      <!-- unlocked -->
      <img class="img-fluid" src="https://cdn-icons-png.flaticon.com/512/158/158599.png" />
      <!-- locked -->
      <!-- <img class="img-fluid" src="https://www.freeiconspng.com/thumbs/lock-icon/lock-icon-11.png" /> -->
        <div class="d-flex justify-content-around align-items-center pt-2">
          <h4 class="vault-name"> {{ vault.name }}</h4>
        </div>
    </div>
</router-link>
  </template>
   
  <script>
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
  export default {
      props: {
          vault: {
              type: Object,
              required: true
          },
      },
      setup(props) {
          return {
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
  </style>