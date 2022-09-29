<template>
    <div class="p-2 rounded bg-grey selectable" @click="setActive()">
      <img class="img-fluid" :src="vaultKeep?.img" />
        <div class="d-flex justify-content-around align-items-center pt-2">
          <h4 class="vaultKeep-name"> {{ vaultKeep?.name }}</h4>
          <div v-if="!profile">
              <img class="profile-img" :src="vaultKeep.creator.picture">
          </div>
        </div>
    </div>
    <VaultKeepModal/>
    
  </template>
   
  <script>
  import { Modal } from 'bootstrap';
  import { keepsService } from '../services/KeepsService';
  import { logger } from '../utils/Logger';
  import Pop from '../utils/Pop';
  import KeepModal from './KeepModal.vue';
  import { AppState } from '../AppState';
  import { computed } from '@vue/reactivity';
import VaultKeepModal from './VaultKeepModal.vue';
  export default {
      props: {
          vaultKeep: {
              type: Object,
              required: true
          },
      },
      setup(props) {
          return {
              profile: computed(() => AppState.activeProfile),
              async setActive() {
                  try {
                      Modal.getOrCreateInstance(document.getElementById("vaultKeepModal")).toggle();
                      await keepsService.getOne(props.vaultKeep.id);
                  }
                  catch (error) {
                      logger.error(error);
                      Pop.toast(error.message, "error");
                  }
              }
          };
      },
      components: { KeepModal, VaultKeepModal }
  }
  </script>
   
  <style>
  .vaultKeep-name {
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