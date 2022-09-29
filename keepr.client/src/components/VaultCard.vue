<template>
    <!-- style="background-image: url({{keep.img}}) -->
<!-- <router-link :to="{name: 'Vault', params: {id:vault.id}}"> -->
    <!--  -->
    <div class="p-2 rounded bg-grey selectable" @click="setActive()">
      <img class="img-fluid" src="https://re-mm-assets.s3.amazonaws.com/product_photo/46460/large_large_Poly_LightBlue_pms291up_1471509902.jpg" />
        <div class="d-flex justify-content-around align-items-center pt-2">
          <h4 class="vault-name"> {{ vault.name }}</h4>
        </div>
    </div>
<!-- </router-link> -->
  </template>
   
  <script>
import { useRouter } from 'vue-router'
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
        const router = useRouter()
          return {
            async setActive(){
                try {
                    await vaultsService.getOne(props.vault.id)
                    router.push({name: 'Vault', params: {vaultId: props.vault.id}})
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