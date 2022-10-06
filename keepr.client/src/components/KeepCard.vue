<template>
  <!-- :style="`background-image: url(${keep.img})`" -->
  <div class="rounded selectable img-fluid"  @click="setActive()" :title="keep.name" :name="keep.name">
     <img class="img-fluid rounded photo" :src="keep.img" />
      <div class="d-flex justify-content-around align-items-center pt-2 name-row">
        <h4 class="keep-name m-3"> {{ keep.name }}</h4>
        <div v-if="!profile" class="keep-pfp">
            <img class="profile-img m-3" :src="keep.creator.picture">
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
.photo{
    position: relative;
}
.keep-pfp{
    position: absolute;
    bottom: 0em;
    left: 16.5em;
}
.keep-name {
    position: absolute;
    bottom: 0.5em;
    left: 0em;
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