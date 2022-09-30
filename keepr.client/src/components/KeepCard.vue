<template>
  <!-- style="background-image: url({{keep.img}}) -->
  <div class="p-2 rounded bg-grey selectable" @click="setActive()" :title="keep.name" :name="keep.name">
    <img class="img-fluid" :src="keep.img" />
      <div class="d-flex justify-content-around align-items-center pt-2">
        <h4 class="keep-name"> {{ keep.name }}</h4>
        <div v-if="!profile">
            <img class="profile-img" :src="keep.creator.picture">
        </div>
      </div>
  </div>
  <KeepModal/>
</template>
 
<script>
import { Modal } from 'bootstrap';
import { keepsService } from '../services/KeepsService';
import { logger } from '../utils/Logger';
import Pop from '../utils/Pop';
import KeepModal from './KeepModal.vue';
import { AppState } from '../AppState';
import { computed } from '@vue/reactivity';
export default {
    props: {
        keep: {
            type: Object,
            required: true
        },
    },
    setup(props) {
        return {
            profile: computed(() => AppState.activeProfile),
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
/* .rounded {
  position: relative;
} */
.keep-name {
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