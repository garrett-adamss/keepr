<template>
 <div class="row">
    <!-- profile information -->
    <img src="">

 </div>
 <div class="row">
    <h1>Vaults <i class="mdi mdi-plus"></i></h1>
        <!-- v-for profile vaults -->
    <div>

    </div>
 </div>
 <div class="row">
    <h1>Keeps <i class="mdi mdi-plus"></i></h1>
    <div class="masonry">
        <!-- v-for profile keeps -->
        <div>

        </div>
    </div>
 </div>
</template>
 
<script>
export default {
   setup(){
      return {
 
      }
   }
}
</script>
 
<style scoped lang="scss"> 
  @media only screen and (min-width: 640px){
    .masonry {
      columns: 300px;
      column-gap: 1em;
      div {
        display: block;
        margin-bottom: 1em;
      }
    }
  }
  @media only screen and (max-width: 640px){
    .masonry {
      columns: 199px;
      column-gap: 1em;
      div {
        display: block;
        margin-bottom: 1em;
      }
    }
  }
</style>