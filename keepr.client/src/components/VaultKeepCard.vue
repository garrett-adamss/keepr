<template>
    <div class="p-2 ">
      <img class="img-fluid selectable rounded" :title="vaultKeep?.name" :name="vaultKeep?.name" :src="vaultKeep?.img" @click="setActive()"/>
        <div class="d-flex justify-content-around align-items-center pt-2">
          <h4 class="vaultKeep-name"> {{ vaultKeep?.name }}</h4>
          <p class="selectable" @click="removeVaultKeep">Remove</p>
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
import { vaultKeepsService } from '../services/VaultKeepsService';
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
                      logger.log('props.vaultKeep.id', props.vaultKeep)
                      await keepsService.getOne(props.vaultKeep.id);
                  }
                  catch (error) {
                      logger.error(error);
                      Pop.toast(error.message, "error");
                  }
              },
              async removeVaultKeep(){
                  try {
                    logger.log('[VaultKeepId]', props.vaultKeep.vaultKeepId)
                    const yes = await Pop.confirm("Are you sure you want to delete that?");
                    if (!yes) {
                        return;
                    }
                    await vaultKeepsService.removeKeep(props.vaultKeep.vaultKeepId)
                    Pop.success('Keep removed from vault')
                }
                catch (error) {
                   logger.error(error)
                   Pop.toast(error.message, 'error')
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