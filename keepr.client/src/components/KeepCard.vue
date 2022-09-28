<template>
  <!-- style="background-image: url({{keep.img}}) -->
  <div class="p-2 rounded bg-grey" @click="setActive()">
    <img class="img-fluid" :src="keep.img" />
      <div class="d-flex justify-content-around">
        <h4 class=""> {{ keep.name }}</h4>
        <h4>t</h4>
        <!-- <img class="profile-img" :src=""> -->
      </div>
  </div>
  <KeepModal/>
</template>
 
<script>
import { Modal } from 'bootstrap';
import { keepsService } from '../services/KeepsService';
import { profilesService } from '../services/ProfilesService';
import { logger } from '../utils/Logger';
import { onMounted } from '@vue/runtime-core';
import Pop from '../utils/Pop';
import KeepModal from './KeepModal.vue';
export default {
    props: {
        keep: {
            type: Object,
            required: true
        },
        // profile: {
        //     type: Object,
        //     required: true,
        // }
    },
    setup(props) {
        async function getProfile() {
            try {
                // await profilesService.getProfileById(route.params.id)
            }
            catch (error) {
                logger.error(error);
            }
        }
        onMounted(() => {
            getProfile();
        });
        return {
            async setActive() {
                try {
                    Modal.getOrCreateInstance(document.getElementById("keepModal")).toggle();
                    await keepsService.getOne(props.keep.id);
                }
                catch (error) {
                    logger.error(error);
                    Pop.toast(error.message, "error");
                }
            }
        };
    },
    components: { KeepModal }
}
</script>
 
<style>
.rounded {
  position: relative;
}
.keep-name {
  position: absolute;
}
</style>